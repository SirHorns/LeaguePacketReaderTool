using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using LPRT.Interfaces;
using LPRT.MVVP.Modal;
using Newtonsoft.Json.Linq;

namespace LPRT.MVVP.ViewModal
{
    public class ViewModal : IViewFunctions, IModalFunctions, INotifyPropertyChanged
    {
        private View.View _view;
        private Modal.Modal _modal;
        private Dictionary<int, JObject> _rawPacketData;
        private List<string> _packetNames;
        
        public ViewModal(View.View view)
        {
            View = view;
            Modal = new Modal.Modal(this);
            Modal.PropertyChanged += Modal_PropertyChanged;
            PropertyChanged += View_PropertyChanged;
        }
        
        private void Modal_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "PacketTypes":
                    PublishPacketFilters();
                    break;
                case "PacketTimelineEntries":
                    PublishPacketTimeLine();
                    break;
            }
        }
        
        private void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FilePath")
            {
                //LoadPacketFilters();
                //LoadPacketTimeLine();
            }
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

        /// <summary>
        /// Called by View.
        /// Tells the Modal to load the json file from the path to parse and store match packet information.
        /// </summary>
        /// <param name="path">Directory path to JSON to load.</param>
        public void Notify_FileSelected(string path)
        {
            _modal.FilePath = path;
            //_modal.GetPacketTimeLine();
        }

        /// <summary>
        /// 
        /// </summary>
        public void PublishPacketTimeLine()
        {
            DataGridView timeLine = _view.PacketTimeline;
            timeLine.Rows.Clear();
            
            foreach (var entry in _modal.PacketTimelineEntries)
            {
                timeLine.Rows.Add(entry.Time, entry.Position, entry.Type);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void PublishPacketFilters()
        {
            ComboBox timeLineFilter = _view.PacketTimelineFilter;
            
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
        /// <param name="filter"></param>
        public void FilterPacketTimeLine(string filter)
        {
            DataGridView timeLine = _view.PacketTimeline;
            
            if (_modal.PacketTypes.Contains(filter) | filter.Equals("All Packets"))
            {
                timeLine.Rows.Clear();
            }
            else
            {
                return;
            }
            
            if (filter.Equals("All Packets"))
            {
                foreach (var entry in _modal.PacketTimelineEntries)
                {
                    timeLine.Rows.Add(entry.Time, entry.Position, entry.Type); 
                }
            }
            else
            {
                foreach (var entry in _modal.PacketTimelineEntries)
                {
                    string type = entry.Type;
                    if (type.Equals(filter))
                    {
                        timeLine.Rows.Add(entry.Time, entry.Position, type);
                    }
                } 
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void LoadPacketInfo(int index)
        {
            DataGridView table = _view.PacketInfoTable;
            RichTextBox rawText = _view.PacketInfoText;
            
            table.Rows.Clear();
            
            foreach (var row in _modal.GetPacketInfo(index))
            {
                table.Rows.Add(row);
            }

            rawText.Text = _modal.GetRawPacketInfo(index);
        }

        public void TimeLineUpdated(bool update)
        {
            if (!update) return;
            
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