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
        private View.FormMain _formMain;
        private Modal.Modal _modal;
        
        private List<string> _packetTypes;
        private string _packetFilter;
        private bool _cellNeeded;
        
        
        public ViewModal(View.FormMain formMain)
        {
            FormMain = formMain;
            Modal = new Modal.Modal(this);
            _cellNeeded = false;
            
            Modal.PropertyChanged += PropertyChanged_Modal;
            PropertyChanged += PropertyChanged_View;
        }
        
        private View.FormMain FormMain
        {
            get => _formMain;
            set => _formMain = value;
        }
        private Modal.Modal Modal
        {
            get => _modal;
            set => _modal = value;
        }

        public string PacketFilter
        {
            get => _packetFilter;
            set => _packetFilter = value;
        }

        /// <summary>
        /// 
        /// </summary>
        private void PropertyChanged_Modal(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "PacketTypes":
                    Publish_PacketFilters();
                    break;
                case "PacketTimeline":
                    Publish_PacketTimeLine();
                    break;
                case "FilteredPacketTimeline":
                    //Publish_FilteredPacketTimeLine();
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void PropertyChanged_View(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FilePath")
            {
                //LoadPacketFilters();
                //LoadPacketTimeLine();
            }
        }

        
        
        
        /// <summary>
        /// Tells the Modal that the a file directory path has been selected from the UI.
        /// </summary>
        /// <param name="path">Directory path to JSON to load.</param>
        public void Notify_FileSelected(string path)
        {
            _modal.FilePath = path;
        }
        
        /// <summary>
        /// Notifies the ViewModal that a filter has been selected.
        /// </summary>
        /// <param name="filter">Packet Type selected to filter the Packet Timeline</param>
        public void Notify_FilterSelected(string filter)
        {
            PacketFilter = filter;
            _modal.PacketFilter = filter;
            //_modal.GetPacketTimeLine();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Publish_PacketFilters()
        {
            ComboBox timeLineFilter = _formMain.PacketTimelineFilter;
            
            List<string> packetTypes = _modal.PacketTypes;
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
        
        /// <summary>
        /// 
        /// </summary>
        public void Publish_PacketTimeLine()
        {
            DataGridView timeLine = null;
            timeLine.Rows.Clear();
            foreach (var entry in _modal.PacketTimeline)
            {
                //timeLine.Rows.Add(entry.Time, entry.Position, entry.Type);
                //FormMain.ListView1.Items.Add(new ListViewItem(new []{entry.Time,entry.Position,entry.Type}));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Publish_FilteredPacketTimeLine()
        {
            DataGridView timeLine = null;
            
            timeLine.Rows.Clear();
            
            if (PacketFilter.Equals("All Packets"))
            {
                foreach (var entry in _modal.PacketTimeline)
                {
                    timeLine.Rows.Add(entry.Time, entry.Position, entry.Type);
                } 
            }
            else
            {
                foreach (var entry in _modal.FilteredPacketTimeline)
                {
                    timeLine.Rows.Add(entry.Time, entry.Position, entry.Type);
                } 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void Notify_TimelineEntrySelected(int index)
        {
            DataGridView table = _formMain.PacketInfoTable;
            RichTextBox rawText = _formMain.PacketInfoText;
            
            table.Rows.Clear();
            
            foreach (var row in _modal.GetPacketInfo(index))
            {
                table.Rows.Add(row);
            }

            rawText.Text = _modal.GetRawPacketInfo(index);
        }

        public void Notify_TimelineEntryNeeded()
        {
            //throw new NotImplementedException();
        }

        private int _cachedTimelineIndex = -1;
        private PacketTimeLineEntry _cachedTimeLineEntry;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ListViewItem Notify_TimelineEntryNeeded(int itemIndex)
        {
            return _modal.GetTimelineEntryValue(itemIndex);
            
            
            //return new PacketTimeLineEntry("", "", "");
        }

        public void Notify_CacheRebuild(int startIndex, int endIndex)
        {
            _modal.RebuildCache(startIndex,endIndex);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

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