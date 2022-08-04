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
            
            //Binding the Textbox text property and student class Grade property

            comboBox1.DataBindings.Add(new Binding("Text", _packetNames, "PacketNames"));
            
            
        }

        /*
         * List<string> list = _parser.GetPacketTypes();
            comboBox1.DataSource = list;
            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
            foreach (string s in list)
            {
                autoComplete.Add(s);
            }
            comboBox1.AutoCompleteCustomSource = autoComplete;

            int pos = 0;
            dataGridView2.Rows.Clear();
            foreach (var item in _parser.GetPacketTimeLine())
            {
                dataGridView2.Rows.Add(pos, item);
                pos += 1;
            }
         */
        private void Form1_Load(object sender, EventArgs e)
        { 
            // Creating and setting the
            // properties of ListBo
            //listBox1.Items.Add(123);
            //listBox1.Items.Add(456);
            //listBox1.Items.Add(789);
            
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            
            //comboBox1.DroppedDown = true;
            
            AutoCompleteStringCollection data = new AutoCompleteStringCollection
            {
                "All",
                "Game",
                "Chat"
            };

            List<string> usStates = new List<string>
            {
                "All",
                "Game",
                "Chat"
            };

            //comboBox1.DataSource = usStates;
            //comboBox1.AutoCompleteCustomSource= data;

            PopulateDataGridView();
        }

        private void PopulateDataGridView()
        {

            string[] row0 = { "11/22/1968", "29", "Revolution 9", 
                "Beatles", "The Beatles [White Album]" };
            string[] row1 = { "1960", "6", "Fools Rush In", 
                "Frank Sinatra", "Nice 'N' Easy" };

            //dgPacketContent.Rows.Add(row0);
            //dgPacketContent.Rows.Add(row1);
        }
        
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON (*.json)|*.json*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    //filePath = openFileDialog.FileName;
                    _viewModal.LoadPacketFile(openFileDialog.FileName);
                }
            }
        }
        
        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            List<string> rows = new List<string>();
            string[] row0 = new string[]{};
            string[] row1 = { "1960", "6", "Fools Rush In", 
                "Frank Sinatra", "Nice 'N' Easy" };
            
            dgPacketContent.Rows.Clear();

            /*
             * int pos = 0;
            foreach (var row in _parser.GetPacketInfo(e.RowIndex))
            {
                dgPacketContent.Rows.Add(row);
            }
             */
        }

        private void comboBox1_Validated(object sender, EventArgs e)
        {
            
        }
    }
}