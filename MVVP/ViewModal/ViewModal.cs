using System.Collections.Generic;
using System.Windows.Forms;
using LPRT.Interfaces;
using Newtonsoft.Json.Linq;

namespace LPRT.MVVP.ViewModal
{
    public class ViewModal : IViewFunctions, IModalFunctions
    {
        private View.View _view;
        private Modal.MatchPacketModal _matchPacketModal;
        private Dictionary<int, JObject> _rawPacketData;
        private List<string> _packetNames;
        
        public ViewModal(View.View view)
        {
            _view = view;
            _matchPacketModal = new Modal.MatchPacketModal(this);
        }
        
        
        /// <summary>
        /// Called by View.
        /// Tells the Modal to load the json file from the path to parse and store match packet information.
        /// </summary>
        /// <param name="path">Directory path to JSON to load.</param>
        public void LoadPacketFile(string path)
        {
            _rawPacketData = _matchPacketModal.LoadMatchPackets(path);
            _matchPacketModal.GetPacketTimeLine();
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadPacketTimeLine()
        {
            DataGridView timeLine = _view.PacketTimeline;
            timeLine.Rows.Clear();
            
            foreach (var entry in _matchPacketModal.PacketTimeLineEntries)
            {
                timeLine.Rows.Add(entry.Time, entry.Position, entry.Type);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadPacketTimeLineFilters()
        {
            ComboBox timeLineFilter = _view.PacketTimelineFilter;
            
            List<string> packetTypes = _matchPacketModal.GetPacketTypes();
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
            
            if (_matchPacketModal.PacketTypes.Contains(filter) | filter.Equals("All Packets"))
            {
                timeLine.Rows.Clear();
            }
            else
            {
                return;
            }
            
            if (filter.Equals("All Packets"))
            {
                foreach (var entry in _matchPacketModal.GetPacketTimeLine())
                {
                    timeLine.Rows.Add(entry.Time, entry.Position, entry.Type); 
                }
            }
            else
            {
                foreach (var entry in _matchPacketModal.GetPacketTimeLine())
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
            
            foreach (var row in _matchPacketModal.GetPacketInfo(index))
            {
                table.Rows.Add(row);
            }

            rawText.Text = _matchPacketModal.GetRawPacketInfo(index);
        }
    }
}