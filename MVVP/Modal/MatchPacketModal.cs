using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LPRT.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LPRT.MVVP.Modal
{
    public class MatchPacketModal
    {
        private IModalFunctions _viewModal;
        private List<string> _packetTypes;
        private string _filePath = "";
        private Dictionary<int, JObject> _rawData;
        private List<PacketTimeLineEntry> _packetTimeLineEntries;

        public MatchPacketModal(IModalFunctions viewModal)
        {
            _viewModal = viewModal;
        }
        /// <summary>
        /// 
        /// </summary>
        public List<string> PacketTypes
        {
            get => _packetTypes;
        }
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<int, JObject> RawData
        {
            get => _rawData;
        }
        /// <summary>
        /// 
        /// </summary>
        public string FilePath
        {
            get => _filePath;
        }
        /// <summary>
        /// 
        /// </summary>
        public List<PacketTimeLineEntry> PacketTimeLineEntries
        {
            get => _packetTimeLineEntries;
        }
        
        public Dictionary<int, JObject> LoadMatchPackets(string path)
        {
            _filePath = path;
            _rawData = null;

            //Thread hThread = new Thread(() => { _rawData = ThreadLoad(path); });
            //hThread.Start();
            //hThread.Join();

            _rawData = ThreadLoad(path).Result;
            
            return _rawData;
        }
        
        private async Task<Dictionary<int, JObject>> ThreadLoad(string path)
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
                        //result.Add(pos, (JObject)t);
                    }
                    pos += 1;
                }
            }

            return result;
        }
        
        JsonSerializer serializer = new JsonSerializer();
        private async Task<JToken> ParseJsonToken( JsonReader reader)
        {
            //await Task.Delay(new Random().Next(1000, 5000));
            var token = serializer.Deserialize<JToken>(reader);
            return token;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packetType"></param>
        /// <returns></returns>
        private string GetPacketName(string packetType)
        {
            string packetName;
            string[] split;
            
            split = packetType.Split('.');
            packetName = split[split.Length - 1].Split(',')[0];
            
            return packetName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<PacketTimeLineEntry> GetPacketTimeLine()
        {
            _packetTimeLineEntries = null;
            _packetTimeLineEntries = new List<PacketTimeLineEntry>();
            
            int index = 0;
            foreach (JObject rd in _rawData.Values)
            {
                _packetTimeLineEntries.Add(new PacketTimeLineEntry(
                    rd["Time"].ToString(),
                    index.ToString(),
                    GetPacketName(rd["Packet"]["$type"].ToString())));
                index++ ;
            }

            return _packetTimeLineEntries;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public List<string[]> GetPacketInfo(int index)
        {
            List<string[]> data = new List<string[]>();
            string[] row;
            
            var packet = _rawData[index + 1]["Packet"] as JObject;
            
            if (packet == null)
            {
                row = new string[2];
                row[0] = "BAD";
                row[1] = "WOLF";
                
                return data;
            }
            
            foreach (KeyValuePair<string, JToken> pair in packet)
            {
                row = new string[2];
                row[0] = pair.Key;
                row[1] = pair.Value.ToString();
                data.Add(row);
            }
            
            return data;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetRawPacketInfo(int index)
        {
            var packet = _rawData[index + 1]["Packet"] as JObject;
            return packet == null ? "{\"BADW0LF\": \"\"}" : packet.ToString();
        }

        public void GetPacketByType()
        {
            
        }
        
        
    }
}