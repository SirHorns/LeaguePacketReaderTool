using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json.Nodes;
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
                "Alaska",
                "Arizona",
                "Arkansas",
                "California",
                "Colorado",
                "Connecticut",
                "Delaware",
                "Florida",
                "Georgia"
            };

            List<string> usStates = new List<string>
            {
                "All",
                "Alaska",
                "Arizona",
                "Arkansas",
                "California",
                "Colorado",
                "Connecticut",
                "Delaware",
                "Florida",
                "Georgia"
            };

            comboBox1.DataSource = usStates;
            comboBox1.AutoCompleteCustomSource= data;  
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
                    filePath = openFileDialog.FileName;
                    
                    List<JsonNode>  p =_parser.LoadMatchPackets(filePath);

                    foreach (JsonNode node in p)
                    {
                        PacketNames.Add(node["Packet"]["$type"].ToString());
                        listBox1.Items.Add(node["Packet"]["$type"].ToString());
                    }
                }
            }
        }
    }
}