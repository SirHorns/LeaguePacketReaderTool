using System.ComponentModel;
using LPRT.PacketViewer;
using LPRT.Window;

namespace LPRT;

public class EventManager: INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private ToolWindow View { get; set; }

    public EventManager(ToolWindow view)
    {
        View = view;
        PropertyChanged += InternalPropertyChanged;
    }

    public void InternalPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(PropertyChanges.FILE_PATH):
                View.PacketProgressBar.Visible = true;
                break;
            case nameof(PropertyChanges.PLAYERS_UPDATED):
                foreach (var player in View.MatchReplay.MatchTeams.Players)
                {
                    View.PlayerList.Items.Add(player.Username);
                }

                foreach (var netIds in View.MatchReplay.NetIds)
                {
                    View.NetEntityCombobox.Items.Add(netIds);
                }
                break;
        }
    }
}