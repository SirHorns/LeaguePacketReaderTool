using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Nodes;

namespace LPRT
{
    public class Parser
    {
        private IDictionary<String, String> _packetNames;

        public Parser()
        {
            _packetNames = new Dictionary<string, string>();
        }
        public List<JsonNode> LoadMatchPackets(string path)
        {
            JsonNode document;
            JsonNode rootNode;
            List<JsonNode> patkets = new List<JsonNode>();
            
            using (StreamReader reader = new StreamReader(path))
            {
                document = JsonNode.Parse(reader.ReadToEnd());
                rootNode = document.Root;
            }

            foreach (JsonNode var in document.AsArray())
            {
                patkets.Add(var);
            }
            
            return patkets;
        }
    }
}