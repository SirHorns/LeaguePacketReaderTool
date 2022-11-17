using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LPRT;

public static class PacketSerializer
{
    public static JsonSerializer serializer { get; } = new();

    public static async Task<List<string>> ParseReplayFile(string filePath)
    {
        Debug.WriteLine("Parsing Replay Json file.");
        List<string> replayPackets = null;
        
        using (StreamReader sr = new StreamReader(filePath))
        using (JsonReader reader = new JsonTextReader(sr))
        {
            try
            {
                replayPackets = await Task.Run(() =>
                {
                    List<string> jsonList = new List<string>();
                        
                    while (reader.Read())
                    { 
                        if (reader.TokenType == JsonToken.StartObject) 
                        {
                            //JObject packet = JsonSerializer.Deserialize<JObject>(reader);
                            jsonList.Add(serializer.Deserialize<JObject>(reader)?.ToString());
                        }
                    }
                        
                    return jsonList;
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        return replayPackets;
    }
    
    public static async Task<JObject> AsyncParsePacket(string packet)
    {
        JObject jObject = null;
        using (StreamReader sr = new StreamReader(packet))
        using (JsonReader reader = new JsonTextReader(sr))
        {
            jObject = await Task.Run(() =>
            {
                JObject job = null;
                while (reader.Read())
                { 
                    if (reader.TokenType == JsonToken.StartObject) 
                    {
                        //JObject packet = JsonSerializer.Deserialize<JObject>(reader);
                        job = serializer.Deserialize<JObject>(reader);
                    }
                }
                        
                return job;
            });
        }

        return jObject;
    }
    public static JObject ParsePacket(string packet)
    {
        return JObject.Parse(packet);

        JObject jObject = null;
        
        using StreamReader sr = new StreamReader(packet);
        using JsonReader reader = new JsonTextReader(sr);
        while (reader.Read())
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                //JObject packet = JsonSerializer.Deserialize<JObject>(reader);
                jObject = serializer.Deserialize<JObject>(reader);
            }
        }

        return jObject;
    }
    
}