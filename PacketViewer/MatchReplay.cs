using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using log4net;
using LPRT.Logging;
using LPRT.MVVP.Modal;
using LPRT.MVVP.View;

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
        var parsedPackets = await Serializer.ParseReplayFile(FilePath);
        if (parsedPackets != null)
        {
            Packets = parsedPackets;
        }
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