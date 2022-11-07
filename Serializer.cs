using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LPRT.MVVP.Modal;
using LPRT.MVVP.View;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LPRT;

public static class Serializer
{
    private static JsonSerializer JsonSerializer = new();
    
    
    
    public static async Task<List<string>> ParseReplayFile(string filePath)
    {
        List<string> replayPackets = null;
        
        using StreamReader sr = new StreamReader(filePath);
        using JsonReader reader = new JsonTextReader(sr);
        
        try
        {
            replayPackets = await Task.Run(() =>
            {
                List<string> packets = new();
                        
                while (reader.Read())
                { 
                    if (reader.TokenType == JsonToken.StartObject) 
                    {
                        var packet = JsonSerializer.Deserialize(reader);
                        packets.Add(packet.ToString());
                        
                    }
                }
                return packets;
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return replayPackets;
    }
}