using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LPRT
{
    public partial class Form1 : Form
    {
        private ViewModal _viewModal;
        private List<string> _packetNames;
        
        public Form1()
        {
            InitializeComponent();
            _viewModal = new ViewModal(this);
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
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
            
            _viewModal.UpdatePacketTimeLine(dataGridView2);
            _viewModal.UpdateTimeLineFilter(comboBox1);
        }
        
        private void DataGridView_Focus_TimeLine(object sender, DataGridViewCellEventArgs e)
        {
            _viewModal.GetPacketInfo(dataGridViewPacketContent, e.RowIndex);
        }

        private void comboBox1_Validated(object sender, EventArgs e)
        {
            
        }
    }
}