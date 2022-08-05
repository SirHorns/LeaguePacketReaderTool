using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using LPRT.Annotations;
using Newtonsoft.Json.Linq;

namespace LPRT
{
    
    public class ViewModal: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Form1 _view;
        private Parser _parser;
        private Dictionary<int, JObject> _rawPacketData;
        private List<string> _packetNames;

        public Dictionary<int, JObject> PacketRawPacketData
        {
            get
            {
                return _rawPacketData;
            }
            set
            {
                _rawPacketData = value;
                OnPropertyChanged("_rawPacketData");
            }
        }
        
        public List<string> PacketNames
        {
            get
            {
                return _packetNames;
            }
            set
            {
                _packetNames = value;
                OnPropertyChanged("PacketNames");
            }
        }

        public ViewModal(Form1 view)
        {
            _view = view;
            _parser = new Parser(this);
        }


        public void LoadPacketFile(string path)
        {
            _rawPacketData = _parser.LoadMatchPackets(path);
        }

        public void UpdatePacketTimeLine(DataGridView dataGridView)
        {
            int pos = 0;
            dataGridView.Rows.Clear();
            foreach (var item in _parser.GetPacketTimeLine())
            {
                dataGridView.Rows.Add(pos, item);
                pos += 1;
            }
        }

        public void UpdateTimeLineFilter(ComboBox comboBox)
        {
            List<string> list = _parser.GetPacketTypes();
            comboBox.DataSource = list;
            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
            foreach (string s in list)
            {
                autoComplete.Add(s);
            }
            comboBox.AutoCompleteCustomSource = autoComplete;
        }

        public void GetPacketInfo(DataGridView dataGridView, int index)
        {
            //List<string> rows = new List<string>();
            
            dataGridView.Rows.Clear();

            int pos = 0;
            foreach (var row in _parser.GetPacketInfo(index))
            {
                dataGridView.Rows.Add(row);
            }
        }
        
        //VIEWMODAL TO VIEW
        //MODAL TO VIEWMODAL
        //VIEWMODAL TO MODAL
    }
}