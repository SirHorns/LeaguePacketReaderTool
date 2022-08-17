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

        public ListView ListView1 => listView1;

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

        #region PlayerInfo

        

        #endregion


        private void packetTimeline_NewRowNeeded(object sender, DataGridViewRowEventArgs e)
        {
            //packetTimeline.Rows.Add(1);
        }
    }
}