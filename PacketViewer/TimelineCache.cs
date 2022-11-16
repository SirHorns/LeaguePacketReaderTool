using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LPRT.MVVP.Modal
{
    public class TimelineCache : INotifyPropertyChanged
    {
        /// <summary>
        /// Cache that stores the current filtered timeline. 
        /// </summary>
        private List<ListViewItem> _activeCache;

        /// <summary>
        /// Stores the index of the first item in the cache
        /// </summary>
        private int _firstIndex;
        /// <summary>
        /// Stores the currently selected filter for which packets should be displayed.
        /// </summary>
        private string _filter;

        public string Filter
        {
            get => _filter;
            set {
                _filter = value; 
                OnPropertyChanged();
            }
        }

        private int _cacheSize;

        public List<ListViewItem> Cache
        {
            get => _activeCache;
            set
            {
                _cacheSize = value.Count;
                _activeCache = value;
                OnPropertyChanged(nameof(PropertyChanges.TIMELINE_CACHE));
            }
        }

        /// <summary>
        /// Stores the untampered timeline cached used for creating a new filtered timelineCache
        /// </summary>
        public List<ListViewItem> BackupCache { get; set; }

        public int CacheSize => _cacheSize;

        public TimelineCache()
        {
            PropertyChanged += InternalPropertyChanged;
        }

        public async void PrepareCaches(List<string> packets)
        {
            try
            {
                BackupCache = await Task.Run(() =>
                {
                    int index = 0;
                    List<ListViewItem> tempCache = new List<ListViewItem>();
                    
                    foreach (var packet in packets)
                    {
                        JObject jObject = Serializer.ParsePacket(packet);
                        tempCache.Add(new ListViewItem(new[]
                        {
                            PacketUtilities.ParsePacketName(jObject["Packet"]["$type"].ToString()), 
                            index.ToString(), 
                            jObject["Time"].ToString()
                        }));
                        index++;
                    }

                    return tempCache;
                });
                    
                Cache = BackupCache;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        //Virtual Methods
        public ListViewItem GetTimelineEntry(int itemIndex)
        {
            //Caching is not required but improves performance on large sets.
            //To leave out caching, don't connect the CacheVirtualItems event 
            //and make sure myCache is null.

            //check to see if the requested item is currently in the cache
            if (Cache != null && itemIndex >= _firstIndex && itemIndex < _firstIndex + Cache.Count)
            {
                //A cache hit, so get the ListViewItem from the cache instead of making a new one.
                return Cache[itemIndex - _firstIndex];
            }

            //A cache miss, so create a new ListViewItem and pass it back.
            return new ListViewItem(new[] { "", "", "" });
        }
        
        public void RebuildCache(int startIndex, int endIndex)
        {
            //We've gotten a request to refresh the cache.
            //First check if it's really necessary.
            if (Cache != null && startIndex >= _firstIndex && endIndex <= _firstIndex + Cache.Count)
            {
                //If the newly requested cache is a subset of the old cache, 
                //no need to rebuild everything, so do nothing.
                return;
            }

            //Now we need to rebuild the cache.
            _firstIndex = startIndex;
        }
        
        private void FilterTimeLineCache()
        {
            if(Cache == null) return;
            
            if (Filter.Equals("All_Packets"))
            {
                Cache = BackupCache;
            }
            else
            { 
                List<ListViewItem> temp = new List<ListViewItem>(); 
                foreach (var item in BackupCache) 
                {
                    if (Filter.Equals(item.SubItems[0].Text))
                    {
                        temp.Add(item);
                    } 
                }
                Cache = temp;
            }
        }
        
        
        //PROPERTY-CHANGES
        private void InternalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Filter":
                    FilterTimeLineCache();
                    break;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}