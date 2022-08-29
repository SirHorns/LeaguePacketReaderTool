using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using LPRT.Interfaces;
using LPRT.MVVP.Modal;
using LPRT.MVVP.View;

namespace LPRT.MVVP.ViewModal
{
    public class ViewModal : IViewCommands, IModalCommands, INotifyPropertyChanged
    {
        private View.View _view;
        private Modal.Modal _modal;
        
        private string _packetFilter;
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        public ViewModal(View.View view)
        {
            View = view;
            Modal = new Modal.Modal();
            
            Modal.PropertyChanged += PropertyChanged_Modal;
            PropertyChanged += PropertyChanged_View;
        }
        
        private View.View View
        {
            get => _view;
            set => _view = value;
        }
        private Modal.Modal Modal
        {
            get => _modal;
            set => _modal = value;
        }
        private string PacketFilter
        {
            get => _packetFilter;
            set => _packetFilter = value;
        }


        #region Event-From-View

        public void Notify_FileSelected(string path)
        {
            _modal.FilePath = path;
        }
        public void Notify_FilterSelected(string filter)
        {
            PacketFilter = filter;
            Modal.TimelineFilter = filter;
        }
        public void Notify_TimelineEntrySelected(int index)
        {
            DataGridView table = View.PacketInfoTable;
            RichTextBox rawText = View.PacketInfoText;
            
            table.Rows.Clear();
            
            foreach (var row in Modal.GetPacketInfo(index))
            {
                table.Rows.Add(row);
            }

            rawText.Text = Modal.GetRawPacketInfo(index);
        }
        public void Notify_PlayerSelected(string username)
        {
            Modal.SelectedPlayer = username;
        }

        #endregion
  
        
        #region VirtualCalls-Timeline

        public ListViewItem Request_TimelineEntry(int itemIndex)
        {
            return Modal.GetTimelineEntry(itemIndex);
        }
        
        public void Request_RebuildCache(int startIndex, int endIndex)
        {
            Modal.RebuildTimelineCache(startIndex,endIndex);
        }

        #endregion

        #region VirtualCalls-TimelineNetID

        

        #endregion
        
        //FROMMODAL
        public void Publish_Players()
        {
            View.PlayerList.Items.Clear();
            foreach (var player in Modal.MatchTeams.Players)
            {
                View.PlayerList.Items.Add(player.Username);
            }

            View.PlayerList.SelectedIndex = 0;
        }
        public void Publish_PlayerInfo()
        {
            //TODO: Add components to display player info.
            Player p = Modal.MatchTeams.GetPlayerByUserName(Modal.SelectedPlayer);
            View.PlayerInfo.Rows.Clear();
            View.PlayerInfo.Rows.Add("Username", p.Username);
            View.PlayerInfo.Rows.Add("ClientId", p.ClientId);
            View.PlayerInfo.Rows.Add("NetId", p.NetId);
            View.PlayerInfo.Rows.Add("Team", p.Team);
            View.PlayerInfo.Rows.Add("Champion", p.Champion);
            View.PlayerInfo.Rows.Add("SkinId", p.SkinId);
        }
        
        public void Reload_PacketTimeline()
        {
            //View.PacketTimeLine.Items.Clear();
            View.PacketTimeLine.VirtualListSize = Modal.TimeLineSize;
            View.PacketTimeLine.VirtualMode = true;
            View.PacketTimeLine.Refresh();
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