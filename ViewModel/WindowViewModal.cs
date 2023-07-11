using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LPRT.MVVP.Modal;
using LPRT.MVVP.View;

namespace LPRT.MVVP.ViewModal
{
    public class WindowViewModal :  INotifyPropertyChanged
    {
        private View.Window _window;
        private Modal.Modal _modal;
        
        private string _packetFilter;
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        public WindowViewModal(View.Window window)
        {
            Window = window;
            Modal = new Modal.Modal();
            
            Modal.PropertyChanged += PropertyChanged_Modal;
            PropertyChanged += PropertyChanged_View;
        }
        
        private View.Window Window
        {
            get => _window;
            set => _window = value;
        }
        private Modal.Modal Modal
        {
            get => _modal;
            set => _modal = value;
        }
        
        #region Event-From-View
        //Timelinne
        public void SelectedFile(string path)
        {
            _modal.FilePath = path;
        }
        public void SelectedNetID(string filter)
        {
            
        }

        public void SelectSentRecieve(string status)
        {
            
        }
        //
        public void SelectedPlayer(string username)
        {
            Modal.SelectedPlayer = username;
        }

        #endregion
        
        //FROMMODAL
        public void Publish_Players()
        {
            Window.PlayerList.Items.Clear();
            foreach (var player in Modal.MatchTeams.Players)
            {
                Window.PlayerList.Items.Add(player.Username);
            }

            Window.PlayerList.SelectedIndex = 0;
        }
        public void Publish_PlayerInfo()
        {
            //TODO: Add components to display player info.
            Player p = Modal.MatchTeams.GetPlayerByUserName(Modal.SelectedPlayer);
            Window.PlayerInfo.Rows.Clear();
            Window.PlayerInfo.Rows.Add("Username", p.Username);
            Window.PlayerInfo.Rows.Add("ClientId", p.ClientId);
            Window.PlayerInfo.Rows.Add("NetId", p.NetId);
            Window.PlayerInfo.Rows.Add("Team", p.Team);
            Window.PlayerInfo.Rows.Add("Champion", p.Champion);
            Window.PlayerInfo.Rows.Add("SkinId", p.SkinId);
        }
        
        public void Reload_PacketTimeline()
        {
            //View.PacketTimeLine.Items.Clear();
            Window.PacketTimeLine.VirtualListSize = Modal.TimeLineSize;
            Window.PacketTimeLine.VirtualMode = true;
            Window.PacketTimeLine.Refresh();
        }


        //PROPERTY-CHANGES
        private void PropertyChanged_Modal(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(PropertyChanges.TIMELINE_CACHE):
                    Reload_PacketTimeline();
                    break;
                case nameof(PropertyChanges.TEAMS):
                    Publish_Players();
                    break;
                case nameof(PropertyChanges.PLAYER_SELECTED):
                    Publish_PlayerInfo();
                    break;
                default:
                    break;
            }
        }
        
        private void PropertyChanged_View(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "FilePath":
                    break;
                default:
                    break;
            }
        }
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}