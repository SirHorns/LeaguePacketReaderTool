using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LPRT.MVVP.Modal
{
    public class Modal
    {
        private ViewModal.ViewModal _viewModal;
        private List<string> _packetTypes;
        private Dictionary<int, JObject> _rawData;
        private string _filePath = "";

        public Modal(ViewModal.ViewModal viewModal)
        {
            _viewModal = viewModal;
        }
        public Dictionary<int, JObject> LoadMatchPackets(string path)
        {
            _filePath = path;
            _rawData = null;
            
            
            Thread hThread = new Thread(() => { _rawData = ThreadLoad(path); });

            hThread.Start();
            hThread.Join();
            
            return _rawData;
        }
        
        private Dictionary<int, JObject> ThreadLoad(string path)
        {
            Dictionary<int, JObject> result = new Dictionary<int, JObject>();
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
                        result.Add(pos, (JObject)t);
                    }
                    pos += 1;
                }
            }

            return result;
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

        public string GetRawPacketInfo(int index)
        {
            JObject packet = _rawData[index + 1]["Packet"] as JObject;
            return packet.ToString();
        }
        public void Parsed(string path)
        {
            using (FileStream fs = File.OpenRead(path))
            using (StreamReader streamReader = new StreamReader(fs, Encoding.UTF8, true, 4096))
            {
            }
        }

        public List<string> PacketTypes
        {
            get => _packetTypes;
            set => _packetTypes = value;
        }
    }
}