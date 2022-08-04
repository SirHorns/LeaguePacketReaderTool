using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LPRT
{
    public class Parser
    {
        private ViewModal _viewModal;
        private List<string> _packetNames;
        private string _path = "";

        public Parser(ViewModal viewModal)
        {
            _viewModal = viewModal;
        }
        public Dictionary<int, JObject> LoadMatchPackets(string path)
        {
            Dictionary<int, JObject> rawPacketData = new Dictionary<int, JObject>();
            _path = path;
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
                        rawPacketData.Add(pos, (JObject)t);
                        //Add custom logic here - perhaps a yield return?
                    }

                    pos += 1;
                }
            }

            return rawPacketData;
        }
        
        public string LoadMatchPackets1(string path)
        {
            JArray document;

            using (StreamReader sr = new StreamReader(path))
            {
                document = (JArray)JsonConvert.DeserializeObject(sr.ReadToEnd());
            }

            return document.First["RawID"].Value<string>();
        }
        
        public List<string> GetPacketTypes()
        {
            _packetNames = new List<string>();
            string packetName;

            foreach (JObject raw in _rawData.Values)
            {
                packetName = GetPacketName(raw["Packet"]["$type"].ToString());

                if (!_packetNames.Contains(packetName))
                {
                   _packetNames.Add(packetName); 
                }
            }
            
            _packetNames.Sort();
            
            return _packetNames;
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
    }
}