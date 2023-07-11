using System;
using System.ComponentModel;
using System.Windows.Forms;
using LPRT.PacketViewer;

namespace LPRT.Window
{
    public partial class ToolWindow : Form, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ToolWindow()
        {
            var eventManager = new EventManager(this);
            TimeLineControl = new PacketTimelineControl(this);
            MatchReplay.PropertyChanged += eventManager.InternalPropertyChanged;
            TimeLineControl.PropertyChanged += eventManager.InternalPropertyChanged;
            PropertyChanged += eventManager.InternalPropertyChanged;

            InitializeComponent();
        }
        
        /// <summary>
        /// Triggered when window is loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Load(object sender, EventArgs e)
        {
            var autoComplete = new AutoCompleteStringCollection();
            foreach (var item in PacketUtilities.PacketTypes)
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

        // Menu Bar Load Button Functions
        
        private async void Open_JSON(object sender, EventArgs e)
        {
           var path =AssetLoader.OpenFileDialog();
           if (path == null)
           {
               return;
           } 
           MatchReplay.FilePath = path;
            MatchReplay.Packets =  await PacketUtilities.AsyncLoadFile(path);
        }
        private async void Open_LRF(object sender, EventArgs e)
        {
            var path =AssetLoader.OpenFileDialog();
            if (path == null)
            {
                return;
            } 
            MatchReplay.FilePath = path;
            MatchReplay.Packets =  await PacketUtilities.AsyncLoadFile(path);
        }
        private async void Open_Serialized(object sender, EventArgs e)
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
            if (!double.TryParse(e.Text, out var x)) //check if this is a valid search
            {
                return;
            }
            x = Math.Sqrt(x);
            x = Math.Round(x);
            e.Index = (int)x;
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
        private void Change_TimelineSelection(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var index = int.Parse(e.Item.SubItems[1].Text);
            PacketInfoTable.Rows.Clear();
            
            foreach (object[] row in MatchReplay.GetPacketInfo(index))
            {
                PacketInfoTable.Rows.Add(row);
            }

            PacketInfoText.Text = MatchReplay.GetRawPacketInfo(index);
        }

        public void Reload_PacketTimeline(int count)
        {
            PacketTimeLine.Items.Clear();
            PacketTimeLine.VirtualListSize = count;
            PacketTimeLine.VirtualMode = true;
            PacketTimeLine.Refresh();
            PacketProgressBar.Visible = false;
        }
    }
}