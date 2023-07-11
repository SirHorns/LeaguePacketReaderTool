using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using LPRT.View;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LPRT.PacketViewer;

public class MatchReplay : INotifyPropertyChanged
{
    private string _filePath;
    private List<Player> _players;
    public Teams MatchTeams { get; private set; } = new Teams();
    public Dictionary<string, string> NetIdDict { get; } = new();
    private List<string> _packets;
    public List<string> NetIds { get; } = new();

    public string FilePath
    {
        get => _filePath;
        set
        {
            _filePath = value;
            OnPropertyChanged(nameof(PropertyChanges.FILE_PATH));
        }
    }

    public List<Player> Players
    {
        get => _players;
        set => _players = value;
    }

    public List<string> Packets
    {
        get => _packets;
        set
        {
            _packets = value;
            OnPropertyChanged(nameof(PropertyChanges.PACKETS));
        }
    }

    public MatchReplay()
    {
        MatchTeams.PropertyChanged += InternalPropertyChanged;
        PropertyChanged += InternalPropertyChanged;
    }

    public List<string[]> GetPacketInfo(int index)
    {
        List<string[]> data = new List<string[]>();

        using (StringReader sr = new StringReader(Packets[index]))
        using (JsonReader reader = new JsonTextReader(sr))
        {
            var token = PacketSerializer.serializer.Deserialize(reader);
            JObject jobj = token as JObject;

            foreach (KeyValuePair<string, JToken> pair in jobj["Packet"] as JObject)
            {
                var row = new string[2];
                row[0] = pair.Key;
                row[1] = pair.Value.ToString();
                data.Add(row);
            }
        }

        return data;
    }

    public string GetRawPacketInfo(int index)
    {
        var packet = JObject.Parse(Packets[index]).ToString();
        return packet;
    }

    private async void ParseReplayInfo()
    {
        Dictionary<string, List<string>> IPackage; 
        
         IPackage = await Task.Run(() => {
            Dictionary<string, List<string>> package = new() { { "S2C_CreateHero", new List<string>() }, { "NetID", new List<string>() } };
            
            Parallel.ForEach(Packets, packet =>
            {
                var pak = JObject.Parse(packet);
                if (packet.Contains("S2C_CreateHero"))
                {
                   package["S2C_CreateHero"].Add(packet);
                }

                try
                {
                    if(!package["NetID"].Contains(pak["Packet"]["SenderNetID"].ToString()))
                    {
                        package["NetID"].Add(pak["Packet"]["SenderNetID"].ToString());
                    }
                }
                catch (Exception e)
                {
                    // Packet doesn't contain a SenderNetID, so will ignored.
                }
            });
            return package;
        });

         foreach (var packet in IPackage["S2C_CreateHero"])
         {
             var player = JObject.Parse(packet)["Packet"];
             
             MatchTeams.AddPlayer(
                 player["Name"].ToString(),
                 player["NetID"].ToString(),
                 player["ClientID"].ToString(),
                 Boolean.Parse(player["TeamIsOrder"].ToString()), 
                 player["Skin"].ToString(),
                 player["SkinID"].ToString(),
                 Boolean.Parse(player["IsBot"].ToString()));
         }
         
         IPackage["NetID"].Sort();
         
         foreach (var netid in IPackage["NetID"])
         {
             NetIds.Add(netid);
         }
         
         OnPropertyChanged(nameof(PropertyChanges.PLAYERS_UPDATED));
    }


    //PROPERTY-CHANGES
    private void InternalPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(PropertyChanges.PACKETS):
                ParseReplayInfo();
                break;
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    //ඞ Amogus?
}