using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LPRT.MVVP.Modal
{
    public class TimelineCache
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
        private string _timelineFilter;

        private int _cacheSize;

        public TimelineCache()
        {
        }
        
        public List<ListViewItem> Cache
        {
            get => _activeCache;
            set
            {
                _cacheSize = value.Count;
                _activeCache = value;
            }
        }

        /// <summary>
        /// Stores the untampered timeline cached used for creating a new filtered timelineCache
        /// </summary>
        public List<ListViewItem> BackupCache { get; set; }

        public int CacheSize => _cacheSize;
        
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
    }
}