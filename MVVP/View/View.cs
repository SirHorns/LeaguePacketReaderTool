using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LPRT.Interfaces;

namespace LPRT.MVVP.View
{
    public partial class View : Form
    {
        // Declare a variable to store the index of a row being edited.
        // A value of -1 indicates that there is no row currently in edit.
        private int rowInEdit = -1;

        // Declare a variable to indicate the commit scope.
        // Set this value to false to use cell-level commit scope.
        private bool rowScopeCommit = true;
        
        //Reference to the ViewModal;
        private readonly IViewFunctions _viewModal;
        
        public View()
        {
            InitializeComponent();
            _viewModal = new ViewModal.ViewModal(this);
        }

        public DataGridView PacketTimeline
        {
            get => packetTimeline;
        }

        public ComboBox PacketTimelineFilter
        {
            get => packetTimelineFilter;
        }

        public RichTextBox PacketInfoText
        {
            get => packetInfoText;
        }

        public DataGridView PacketInfoTable
        {
            get => packetInfoTable;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //packetTimeline.RowCount = 24;
            // Connect the virtual-mode events to event handlers.
            //packetTimeline.CellValueNeeded += packetTimeline_CellValueNeeded;
            //packetTimeline.CellValuePushed += packetTimeline_CellValuePushed;
            //packetTimeline.NewRowNeeded += packetTimeline_NewRowNeeded;
        }

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
                    _viewModal.LoadPacketFile(openFileDialog.FileName);
                    
                }
            }
            _viewModal.LoadPacketTimeLineFilters();
            _viewModal.LoadPacketTimeLine();
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void PacketTimeLine_CellFocus(object sender, DataGridViewCellEventArgs e)
        {
            LoadPacketInfo(e.RowIndex); 
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void PacketTimeLine_CellCLick(object sender, DataGridViewCellEventArgs e)
        {
            LoadPacketInfo(e.RowIndex); 
        }
        /// <summary>
        /// 
        /// </summary>
        private void PacketTimeLineFilter_ValueChanged(object sender, EventArgs e)
        {
            _viewModal.FilterPacketTimeLine(packetTimelineFilter.Text);
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
    }
}