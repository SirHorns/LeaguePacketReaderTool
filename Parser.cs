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
        private Dictionary<int, JObject> _rawData;
        private List<string> _packetNames;
        private string _path = "";

        public Parser()
        {
        }
        public void LoadMatchPackets(string path)
        {
            _rawData = null;
            _rawData = new Dictionary<int, JObject>();
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
                        _rawData.Add(pos, (JObject)t);
                        //Add custom logic here - perhaps a yield return?
                    }

                    pos += 1;
                }
            }
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
            string type;
            string name;
            string[] split;
            
            
            foreach (JObject raw in _rawData.Values)
            {
                type = raw["Packet"]["$type"].ToString();

                split = type.Split('.');
                name = split[split.Length - 1].Split(',')[0];
                
                if (!_packetNames.Contains(name))
                {
                   _packetNames.Add(name); 
                }
            }
            
            _packetNames.Sort();
            
            return _packetNames;
        }

        public List<string> GetPacketTimeLine()
        {
            int pos = 0;
            List<String> newTimeLine = new List<string>();
            foreach (JObject rd in _rawData.Values)
            {
                newTimeLine.Add(pos + " " + rd["Packet"]["$type"]);
                pos += 1;
            }

            return newTimeLine;
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