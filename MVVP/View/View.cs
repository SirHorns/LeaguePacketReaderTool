using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LPRT.MVVP.View
{
    public partial class View : Form
    {
        private ViewModal.ViewModal _viewModal;
        public View()
        {
            InitializeComponent();
            _viewModal = new ViewModal.ViewModal(this);
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void MenuItem_Click_Load(object sender, EventArgs e)
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
            
            _viewModal.UpdateTimeLineFilter(comboBox1);
            _viewModal.UpdatePacketTimeLine(packetTimeline);
            
        }
        
        private void DataGridView_Focus_TimeLine(object sender, DataGridViewCellEventArgs e)
        {
            LoadPacketInfo(e.RowIndex);
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            _viewModal.FilterTimeLine( packetTimeline, comboBox1.Text);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadPacketInfo(e.RowIndex);
        }

        private void LoadPacketInfo(int index)
        {
            if (index >= 0)
            {
              _viewModal.GetPacketInfo(dataGridViewPacketContent, richTextBox1,Int32.Parse(packetTimeline.Rows[index].Cells[1].Value.ToString()));  
            }
        }
    }
}