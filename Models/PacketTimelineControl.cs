using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

using LPRT.Window;

namespace LPRT
{
    public sealed class PacketTimelineControl: INotifyPropertyChanged
    {
        private ToolWindow Window {get;}
        
        private string _selectedFilter;
        private readonly PacketTimelineCache _packetTimelineCache;
        
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
            get => _packetTimelineCache.Cache;
            set
            {
                _packetTimelineCache.Cache = value;
                OnPropertyChanged(nameof(PropertyChanges.TIMELINE_CACHE));
            }
        }

        public PacketTimelineControl(ToolWindow window)
        {
            Window = window;
            _packetTimelineCache = new PacketTimelineCache();
            Window.MatchReplay.PropertyChanged += InternalPropertyChanged;
            _packetTimelineCache.PropertyChanged += InternalPropertyChanged;
            PropertyChanged += InternalPropertyChanged;
        }

        //Virtual Timeline Calls
        public ListViewItem GetTimelineEntry(int itemIndex)
        {
            return _packetTimelineCache.GetTimelineEntry(itemIndex);
        }
        public void RebuildCache(int startIndex, int endIndex)
        {
            _packetTimelineCache.RebuildCache(startIndex,endIndex);
        }

        //PROPERTY-CHANGES
        private void InternalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(PropertyChanges.PACKETS):
                    _packetTimelineCache.PrepareCaches(Window.MatchReplay.Packets);
                    break;
                case nameof(PropertyChanges.TIMELINE_CACHE):
                    Window.Reload_PacketTimeline(Cache.Count);
                    break;
                case nameof(PropertyChanges.TIMELINE_FILTER):
                    _packetTimelineCache.Filter = SelectedFilter;
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}