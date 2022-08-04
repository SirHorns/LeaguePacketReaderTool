using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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

        //VIEW TO VIEWMODAL
        public void LoadPacketFile(string path)
        {
            _rawPacketData = _parser.LoadMatchPackets(path);
        }
        
        //VIEWMODAL TO VIEW
        //MODAL TO VIEWMODAL
        //VIEWMODAL TO MODAL
    }
}