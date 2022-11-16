using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using LPRT.MVVP.Modal;
using LPRT.MVVP.View;
using LPRT.PacketViewer;

namespace LPRT
{
    public class PacketTimelineControl: INotifyPropertyChanged
    {
        private Window Window {get;}
        
        
        private string _selectedFilter;
        private readonly TimelineCache TimelineCache;
        
        public string SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                OnPropertyChanged(nameof(PropertyChanges.TIMELINE_FILTER));
            }
        }

        private List<ListViewItem> Cache
        {
            get => TimelineCache.Cache;
            set
            {
                TimelineCache.Cache = value;
                OnPropertyChanged(nameof(PropertyChanges.TIMELINE_CACHE));
            }
        }

        public PacketTimelineControl(Window window)
        {
            Window = window;
            TimelineCache = new();
            Window.MatchReplay.PropertyChanged += InternalPropertyChanged;
            TimelineCache.PropertyChanged += InternalPropertyChanged;
            PropertyChanged += InternalPropertyChanged;
        }
        
        private void FilterTimeLineCache()
        {
            if(Cache == null) return;
            
            if (SelectedFilter.Equals("All_Packets"))
            {
                Cache = TimelineCache.BackupCache;
            }
            else
            { 
                List<ListViewItem> temp = new List<ListViewItem>(); 
                foreach (var item in TimelineCache.BackupCache) 
                {
                    if (SelectedFilter.Equals(item.SubItems[0].Text))
                    {
                        temp.Add(item);
                    } 
                }
                Cache = temp;
            }
        }

        public ListViewItem GetTimelineEntry(int itemIndex)
        {
            return TimelineCache.GetTimelineEntry(itemIndex);
        }
        
        public void RebuildTimelineCache(int startIndex, int endIndex)
        {
            TimelineCache.RebuildCache(startIndex,endIndex);
        }
        
        public void Reload_PacketTimeline()
        {
            //View.PacketTimeLine.Items.Clear();
            Window.PacketTimeLine.VirtualListSize = Cache.Count;
            Window.PacketTimeLine.VirtualMode = true;
            Window.PacketTimeLine.Refresh();
        }
        
        //Virtual Timeline Calls
        public ListViewItem Request_TimelineEntry(int itemIndex)
        {
            return TimelineCache.GetTimelineEntry(itemIndex);
        }
        public void RebuildCache(int startIndex, int endIndex)
        {
            TimelineCache.RebuildCache(startIndex,endIndex);
        }

        //PROPERTY-CHANGES
        private void InternalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(PropertyChanges.PACKETS):
                    TimelineCache.PrepareCaches(Window.MatchReplay.Packets);
                    break;
                case nameof(PropertyChanges.TIMELINE_CACHE):
                    Reload_PacketTimeline();
                    break;
                case nameof(PropertyChanges.TIMELINE_FILTER):
                    TimelineCache.Filter = SelectedFilter;
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