using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LPRT.Interfaces;

namespace LPRT.MVVP.View
{
    public partial class View : Form
    {
        /// <summary>
        /// Reference to the ViewModal
        /// </summary>
        private readonly IViewFunctions _viewModal;
        
        public View()
        {
            _viewModal = new ViewModal.ViewModal(this);
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            PacketTimeline.RowCount = 26;
        }

        public DataGridView PacketTimeline => packetTimeline;

        public ComboBox PacketTimelineFilter => packetTimelineFilter;

        public RichTextBox PacketInfoText => packetInfoText;

        public DataGridView PacketInfoTable => packetInfoTable;
        
        /// <summary>
        /// 
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

            //_viewModal.LoadPacketTimeLineFilters();
            //_viewModal.LoadPacketTimeLine();

        }
        
        /// <summary>
        /// 
        /// </summary>
        private void PacketTimeLine_CellFocus(object sender, DataGridViewCellEventArgs e)
        {
            //LoadPacketInfo(e.RowIndex); 
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void PacketTimeLine_CellCLick(object sender, DataGridViewCellEventArgs e)
        {
            //LoadPacketInfo(e.RowIndex); 
        }
        /// <summary>
        /// 
        /// </summary>
        private void PacketTimeLineFilter_ValueChanged(object sender, EventArgs e)
        {
            //_viewModal.FilterPacketTimeLine(packetTimelineFilter.Text);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        private void LoadPacketInfo(int rowIndex)
        {
            if (rowIndex < 0)
            {
                return;
            }
            
            var value = packetTimeline.Rows[rowIndex].Cells[1].Value;

            if (value == null)
            {
                return;
            }
 
            _viewModal.LoadPacketInfo(Int32.Parse(value.ToString()));
        }

        private void packetTimeline_NewRowNeeded(object sender, DataGridViewRowEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}