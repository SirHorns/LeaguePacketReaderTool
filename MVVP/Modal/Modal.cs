using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LPRT
{
    public class Parser: INotifyPropertyChanged
    {
        private ViewModal _viewModal;
        private List<string> _packetTypes;
        private Dictionary<int, JObject> _rawData;
        private string _filePath = "";

        public Parser(ViewModal viewModal)
        {
            _viewModal = viewModal;
        }
        public Dictionary<int, JObject> LoadMatchPackets(string path)
        {
            _filePath = path;
            _rawData = null;
            _rawData = new Dictionary<int, JObject>();
            int pos = 0;
            
            using (StreamReader sr = new StreamReader(path))
            using (JsonTextReader reader = new JsonTextReader(sr))
            {
                reader.SupportMultipleContent = true;
                var serializer = new JsonSerializer();
                
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        var t = serializer.Deserialize(reader);
                        _rawData.Add(pos, (JObject)t);
                    }
                    pos += 1;
                }
            }

            return _rawData;
        }

        public List<string> GetPacketTypes()
        {
            _packetTypes = new List<string>();
            string type;

            foreach (JObject rd in _rawData.Values)
            {
                type = GetPacketName(rd["Packet"]["$type"].ToString());

                if (!_packetTypes.Contains(type))
                {
                   _packetTypes.Add(type); 
                }
            }
            
            _packetTypes.Sort();
            
            return _packetTypes;
        }

        private string GetPacketName(string packetType)
        {
            string packetName;
            string[] split;
            
            split = packetType.Split('.');
            packetName = split[split.Length - 1].Split(',')[0];
            
            return packetName;
        }

        private List<String> newTimeLine;
        public List<string> GetPacketTimeLine()
        {
            newTimeLine = new List<string>();
            
            foreach (JObject rd in _rawData.Values)
            {
                newTimeLine.Add(GetPacketName(rd["Packet"]["$type"].ToString()));
            }

            return newTimeLine;
        }

        public List<string[]> GetPacketInfo(int pos)
        {
            List<string[]> data = new List<string[]>();
            string[] row;
            
            if (pos < 0)
            {
                row = new string[2];
                row[0] = "POS";
                row[1] = pos.ToString();
                
                return data;
            }
            
            JObject packet = _rawData[pos + 1]["Packet"] as JObject;
            
            
            if (packet == null)
            {
                row = new string[2];
                row[0] = "BAD";
                row[1] = "WOLF";
                
                return data;
            }

            int rowPos = 0;
            foreach (KeyValuePair<string, JToken> pair in packet)
            {
                row = new string[2];
                row[0] = pair.Key;
                row[1] = pair.Value.ToString();
                data.Add(row);
            }
            
            return data;
        }
        public void Parsed(string path)
        {
            using (FileStream fs = File.OpenRead(path))
            using (StreamReader streamReader = new StreamReader(fs, Encoding.UTF8, true, 4096))
            {
            }
        }

        public Dictionary<int, JObject> RawData
        {
            get { return _rawData; }
			
            set { 
                if (_rawData != value) { 
                    _rawData = value;
                    OnPropertyChanged("RawData");
                } 
            } 
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