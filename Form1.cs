using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LPRT
{
    public partial class Form1 : Form
    {
        private readonly Parser _parser = new Parser();
        private List<string> PacketNames = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        { 
            // Creating and setting the
            // properties of ListBo
            listBox1.ForeColor = Color.Purple;
            //listBox1.Items.Add(123);
            //listBox1.Items.Add(456);
            //listBox1.Items.Add(789);
            
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            
            comboBox1.DroppedDown = true;
            
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

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;
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
                    _parser.LoadMatchPackets(openFileDialog.FileName);
                }
            }

            List<string> list = _parser.GetPacketTypes();
            comboBox1.DataSource = list;
            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
            foreach (string s in list)
            {
                autoComplete.Add(s);
            }
            comboBox1.AutoCompleteCustomSource = autoComplete;

            foreach (var item in _parser.GetPacketTimeLine())
            {
                listBox1.Items.Add(item);
            }
            
        }

        private void PopulateDataGridView()
        {

            string[] row0 = { "11/22/1968", "29", "Revolution 9", 
                "Beatles", "The Beatles [White Album]" };
            string[] row1 = { "1960", "6", "Fools Rush In", 
                "Frank Sinatra", "Nice 'N' Easy" };

            dataGridView1.Rows.Add(row0);
            dataGridView1.Rows.Add(row1);

            

        }
    }
}