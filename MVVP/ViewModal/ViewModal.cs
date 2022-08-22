using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using LPRT.Interfaces;
using LPRT.MVVP.Modal;
using Newtonsoft.Json.Linq;

namespace LPRT.MVVP.ViewModal
{
    public class ViewModal : IViewCommands, IModalCommands, INotifyPropertyChanged
    {
        private View.View _view;
        private Modal.Modal _modal;
        
        private string _packetFilter;
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        public ViewModal(View.View view)
        {
            View = view;
            Modal = new Modal.Modal();
            
            Modal.PropertyChanged += PropertyChanged_Modal;
            PropertyChanged += PropertyChanged_View;
        }
        
        private View.View View
        {
            get => _view;
            set => _view = value;
        }
        private Modal.Modal Modal
        {
            get => _modal;
            set => _modal = value;
        }
        private string PacketFilter
        {
            get => _packetFilter;
            set => _packetFilter = value;
        }
        
        
        //FROMVIEW
        public void Notify_FileSelected(string path)
        {
            _modal.FilePath = path;
        }
        public void Notify_FilterSelected(string filter)
        {
            PacketFilter = filter;
            Modal.TimelineFilter = filter;
        }
        public void Notify_TimelineEntrySelected(int index)
        {
            DataGridView table = View.PacketInfoTable;
            RichTextBox rawText = View.PacketInfoText;
            
            table.Rows.Clear();
            
            foreach (var row in Modal.Publish_PacketInfo(index))
            {
                table.Rows.Add(row);
            }

            rawText.Text = Modal.Publish_RawPacketInfo(index);
        }
        
        public ListViewItem Request_TimelineEntry(int itemIndex)
        {
            return Modal.Publish_TimelineEntry(itemIndex);
        }
        
        public void Request_RebuildCache(int startIndex, int endIndex)
        {
            Modal.Publish_CacheRebuild(startIndex,endIndex);
        }
        
        
        //FROMMODAL
        public void Publish_PacketFilters()
        {
            ComboBox timeLineFilter = View.TimelineFilter;
            
            List<string> packetTypes = Modal.PacketTypes;
            if (packetTypes == null) packetTypes = new List<string>();
            packetTypes.Insert(0, "All Packets");
            timeLineFilter.DataSource = packetTypes;
            
            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
            foreach (string s in packetTypes)
            {
                autoComplete.Add(s);
            }
            timeLineFilter.AutoCompleteCustomSource = autoComplete;
        }

        public void Reload_PacketTimeline()
        {
            View.PacketTimeLine.Items.Clear();
            View.PacketTimeLine.VirtualListSize = Modal.TimeLineSize;
            View.PacketTimeLine.VirtualMode = true;
            View.PacketTimeLine.Refresh();
        }


        //PROPERTY-CHANGES
        private void PropertyChanged_Modal(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "PacketTypes":
                    Publish_PacketFilters();
                    break;
                case "PacketTimeline":
                    break;
                case "FilteredPacketTimeline":
                    break;
                case "TimelineCache":
                    Reload_PacketTimeline();
                    break;
                case "FilteredPacketTimeLine":
                    //Reload_PacketTimeline();
                    break;
                default:
                    break;
            }
        }
        
        private void PropertyChanged_View(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "FilePath":
                    break;
                default:
                    break;
            }
        }
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}