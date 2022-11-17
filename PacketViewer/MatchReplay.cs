using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using LPRT.MVVP.Modal;
using LPRT.MVVP.View;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LPRT.PacketViewer;

public class MatchReplay: INotifyPropertyChanged
{

    private string _filePath;
    private List<Player> _players;
    private List<string> _packets;

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
        PropertyChanged += InternalPropertyChanged;
    }

    private async void LoadFile()
    {
        var parsedPackets = await PacketSerializer.ParseReplayFile(FilePath);
        if (parsedPackets != null)
        {
            Packets = parsedPackets;
        }
    }
    
    public List<string[]> GetPacketInfo(int index)
    {
        index -= 1;
        List<string[]> data = new List<string[]>();

        using (StringReader sr  = new StringReader(Packets[index]))
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
        index -= 1;
        var packet = JObject.Parse(Packets[index]).ToString();
        return packet;
    }
    

    //PROPERTY-CHANGES
    private void InternalPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(PropertyChanges.FILE_PATH):
                LoadFile();
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