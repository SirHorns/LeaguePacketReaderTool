using System;
using System.Windows.Forms;
using LPRT.Interfaces;

namespace LPRT.MVVP.View
{
    public partial class View : Form
    {
        /// <summary>
        /// Reference to the ViewModal
        /// </summary>
        private readonly IViewCommands _viewModal;
        
        public View()
        {
            _viewModal = new ViewModal.ViewModal(this);
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public IViewCommands ViewModal => _viewModal;

        public ComboBox TimelineFilter => timelineFilter;

        public RichTextBox PacketInfoText => packetInfoText;

        public DataGridView PacketInfoTable => packetInfoTable;
        public ListView PacketTimeLine => packetTimelineList;

        /// <summary>
        /// Menu Bar Load Button Functions
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = @"JSON (*.json)|*.json*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _viewModal.Notify_FileSelected(openFileDialog.FileName);
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void PacketTimeLineFilter_ValueChanged(object sender, EventArgs e)
        {
            _viewModal.Notify_FilterSelected(timelineFilter.Text);
        }

        #region PacketTimeLine-ListView
        private void TimeLine_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = ViewModal.Request_TimelineEntry(e.ItemIndex);
        }
        
        private void TimeLine_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
            ViewModal.Request_RebuildCache(e.StartIndex, e.EndIndex);
        }

        private void TimeLine_SearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
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

        private void packetTimelineList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ViewModal.Notify_TimelineEntrySelected(Int32.Parse(PacketTimeLine.SelectedItems[0].SubItems[1].Text));
        }

        private void packetTimelineList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ViewModal.Notify_TimelineEntrySelected(Int32.Parse(e.Item.SubItems[1].Text));
        }
    }
}