using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using LPRT.MVVP.Modal;
using LPRT.PacketViewer;

namespace LPRT.MVVP.View
{
    public partial class Window : Form, INotifyPropertyChanged
    {
        private readonly List<string> _packetFilters = PacketUtilities.PacketTypes;

        public Window()
        {
            TimeLineControl = new(this);
            MatchReplay.PropertyChanged += InternalPropertyChanged;
            TimeLineControl.PropertyChanged += InternalPropertyChanged;
            PropertyChanged += InternalPropertyChanged;
            
            InitializeComponent();
        }
        
        private void Window_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
            foreach (var item in _packetFilters)
            {
                timelineFilter.Items.Add(item);
                autoComplete.Add(item);
            }

            timelineFilter.AutoCompleteCustomSource = autoComplete;
            timelineFilter.SelectedIndex = 0;
        }

        //Window Components
        public RichTextBox PacketInfoText => packetInfoText;
        public DataGridView PacketInfoTable => packetInfoTable;
        public ListView PacketTimeLine => packetTimelineList;
        public ProgressBar PacketProgressBar => progressBar1;
        public ComboBox NetEntityCombobox => timelineNetEntity;
        public DataGridView PlayerInfo { get; private set; }
        public ListBox PlayerList => playerList;
        
        //Variables
        public MatchReplay MatchReplay { get; } = new();
        
        //Controls
        private PacketTimelineControl TimeLineControl {get;}

        /// <summary>
        /// Menu Bar Load Button Functions
        /// </summary>
        private async void OpenFile(object sender, EventArgs e)
        {
           var path =AssetLoader.OpenFileDialog();
           if (path == null)
           {
               return;
           } 
           MatchReplay.FilePath = path;
            MatchReplay.Packets =  await PacketUtilities.AsyncLoadFile(path);
        }
        

        #region PacketTimeLine
        private void PacketTimeLine_FilterChanged(object sender, EventArgs e)
        {
            TimeLineControl.SelectedFilter = timelineFilter.Text;
        }
        private void PacketTimeLine_GetVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = TimeLineControl.GetTimelineEntry(e.ItemIndex);
        }
        private void PacketTimeLine_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
             TimeLineControl.RebuildCache(e.StartIndex, e.EndIndex);
        }
        private void PacketTimeLine_SearchVirtualItems(object sender, SearchForVirtualItemEventArgs e)
        {
            //We've gotten a search request.
            //In this example, finding the item is easy since it's
            //just the square of its index.  We'll take the square root
            //and round.
            if (Double.TryParse(e.Text, out var x)) //check if this is a valid search
            {
                x = Math.Sqrt(x);
                x = Math.Round(x);
                e.Index = (int)x;
            }
            //If e.Index is not set, the search returns null.
            //Note that this only handles simple searches over the entire
            //list, ignoring any other settings.  Handling Direction, StartIndex,
            //and the other properties of SearchForVirtualItemEventArgs is up
            //to this handler.
        }
        #endregion

        /// <summary>
        /// Handles the selected packet in the time.
        /// </summary>
        private void packetTimelineList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            int index = Int32.Parse(e.Item.SubItems[1].Text);
            
            
            PacketInfoTable.Rows.Clear();
            
            foreach (object[] row in MatchReplay.GetPacketInfo(index))
            {
                PacketInfoTable.Rows.Add(row);
            }

            PacketInfoText.Text = MatchReplay.GetRawPacketInfo(index);
        }
        private void playerList_SelectedIndexChanged(object sender, EventArgs e) { }
        private void timelinePlayerSelect_SelectedValueChanged(object sender, EventArgs e) { }
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e) { }
        
        public void Reload_PacketTimeline(int count)
        {
            PacketTimeLine.Items.Clear();
            PacketTimeLine.VirtualListSize = count;
            PacketTimeLine.VirtualMode = true;
            PacketTimeLine.Refresh();
            PacketProgressBar.Visible = false;
        }
        
        //PROPERTY-CHANGES
        private void InternalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(PropertyChanges.FILE_PATH):
                    PacketProgressBar.Visible = true;
                    break;
                case nameof(PropertyChanges.PLAYERS_UPDATED):
                    foreach (var player in MatchReplay.MatchTeams.Players)
                    {
                        PlayerList.Items.Add(player.Username);
                    }

                    foreach (var netids in MatchReplay.NetIds)
                    {
                        NetEntityCombobox.Items.Add(netids);
                    }
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}