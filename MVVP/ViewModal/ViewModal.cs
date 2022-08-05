using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace LPRT.MVVP.ViewModal
{
    public class ViewModal
    {
        private View.View _view;
        private Modal.Modal _modal;
        private Dictionary<int, JObject> _rawPacketData;
        private List<string> _packetNames;
        
        public ViewModal(View.View view)
        {
            _view = view;
            _modal = new Modal.Modal(this);
        }
        
        //View Request Functions
        public void LoadPacketFile(string path)
        {
            _rawPacketData = _modal.LoadMatchPackets(path);
        }

        public void UpdatePacketTimeLine(DataGridView dataGridView)
        {
            int pos = 0;
            dataGridView.Rows.Clear();
            foreach (var item in _modal.GetPacketTimeLine())
            {
                dataGridView.Rows.Add(pos, item);
                pos += 1;
            }
        }

        public void UpdateTimeLineFilter(ComboBox comboBox)
        {
            List<string> list = _modal.GetPacketTypes();
            list.Insert(0, "All Packets");
            comboBox.DataSource = list;
            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
            foreach (string s in list)
            {
                autoComplete.Add(s);
            }
            comboBox.AutoCompleteCustomSource = autoComplete;
        }

        public void GetPacketInfo(DataGridView dataGridView, RichTextBox richTextBox, int index)
        {
            dataGridView.Rows.Clear();

            int pos = 0;
            foreach (var row in _modal.GetPacketInfo(index))
            {
                dataGridView.Rows.Add(row);
            }

            richTextBox.Text = _modal.GetRawPacketInfo(index);
        }

        public void FilterTimeLine(DataGridView dataGridView, string filter)
        {
            int pos = 0;
            if (filter.Equals("All Packets"))
            {
                dataGridView.Rows.Clear();
                foreach (string item in _modal.GetPacketTimeLine())
                {
                        dataGridView.Rows.Add(pos, item);
                        pos += 1; 
                }
            }
            else if (_modal.PacketTypes.Contains(filter))
            {
                dataGridView.Rows.Clear();
                foreach (string item in _modal.GetPacketTimeLine())
                {
                    if (item.Equals(filter))
                    {
                        dataGridView.Rows.Add(pos, item);
                    }
                    pos += 1;  
                }  
            }
        }
    }
}