using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LPRT.Interfaces;
using LPRT.MVVP.Modal;

namespace LPRT.MVVP.View
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Reference to the ViewModal
        /// </summary>
        private readonly IViewCommands _viewModal;
        
        public FormMain()
        {
            _viewModal = new ViewModal.ViewModal(this);
            Controls.Add(packetTimeline);
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        #region TimeLine
        public DataGridView PacketTimeline => packetTimeline;

        public ComboBox PacketTimelineFilter => packetTimelineFilter;

        public RichTextBox PacketInfoText => packetInfoText;

        public DataGridView PacketInfoTable => packetInfoTable;

        public ListView ListView1 => packetTimelineList;

        /// <summary>
        /// Menu Bar Load Button Functions
        /// </summary>
        private void MenuBar_ClickLoad(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON (*.json)|*.json*";
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
        private void PacketTimeLine_CellFocus(object sender, DataGridViewCellEventArgs e)
        {
            Request_PacketInfo(e.RowIndex);
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void PacketTimeLine_CellCLick(object sender, DataGridViewCellEventArgs e)
        {
            Request_PacketInfo(e.RowIndex); 
        }
        /// <summary>
        /// 
        /// </summary>
        private void PacketTimeLineFilter_ValueChanged(object sender, EventArgs e)
        {
            _viewModal.Notify_FilterSelected(packetTimelineFilter.Text);
        }
        
        /// <summary>
        /// Notifies the ViewModal that packet info is needed.
        /// </summary>
        /// <param name="index">Position of the packet</param>
        private void Request_PacketInfo(int index)
        {
            if (index < 0 | index > packetTimeline.Rows.Count)
            {
                return;
            }
            
            var value = packetTimeline.Rows[index].Cells[1].Value;

            if (value == null)
            {
                return;
            }
 
            _viewModal.Notify_TimelineEntrySelected(Int32.Parse(value.ToString()));
        }

        private void packetTimeline_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            e.Value = _viewModal.Notify_TimelineEntryNeeded(e.RowIndex, e.ColumnIndex);
        }

        #endregion

        #region PacketTimeLine-ListView
        
        private ListViewItem[] myCache; //array to cache items for the virtual list
        private int firstItem; //stores the index of the first item in the cache
        
        private void TimeLine_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //Caching is not required but improves performance on large sets.
            //To leave out caching, don't connect the CacheVirtualItems event 
            //and make sure myCache is null.

            //check to see if the requested item is currently in the cache
            if (myCache != null && e.ItemIndex >= firstItem && e.ItemIndex < firstItem + myCache.Length)
            {
                //A cache hit, so get the ListViewItem from the cache instead of making a new one.
                e.Item = myCache[e.ItemIndex - firstItem];
            }
            else
            {
                //A cache miss, so create a new ListViewItem and pass it back.
                int x = e.ItemIndex * e.ItemIndex;
                e.Item = new ListViewItem(new [] {x.ToString(),x.ToString(),x.ToString()});
            }
        }
        
        private void TimeLine_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
            //We've gotten a request to refresh the cache.
            //First check if it's really neccesary.
            if (myCache != null && e.StartIndex >= firstItem && e.EndIndex <= firstItem + myCache.Length)
            {
                //If the newly requested cache is a subset of the old cache, 
                //no need to rebuild everything, so do nothing.
                return;
            }

            //Now we need to rebuild the cache.
            firstItem = e.StartIndex;
            int length = e.EndIndex - e.StartIndex + 1; //indexes are inclusive
            myCache = new ListViewItem[length];

            //Fill the cache with the appropriate ListViewItems.
            int x = 0;
            string[] subitems = {
                x.ToString(),
                x.ToString(),
                x.ToString()
            };
            
            for (int i = 0; i < length; i++)
            {
                x = (i + firstItem) * (i + firstItem);
                ListViewItem item = new ListViewItem(subitems);
                myCache[i] =  item;
            }
        }

        private void TimeLine_SearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
        {
            //We've gotten a search request.
            //In this example, finding the item is easy since it's
            //just the square of its index.  We'll take the square root
            //and round.
            double x = 0;
            if (Double.TryParse(e.Text, out x)) //check if this is a valid search
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


        private void packetTimeline_NewRowNeeded(object sender, DataGridViewRowEventArgs e)
        {
            //packetTimeline.Rows.Add(1);
        }

        
    }
}