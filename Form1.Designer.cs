namespace LPRT
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.tabContoler = new System.Windows.Forms.TabControl();
            this.tabInfo = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPlayers = new System.Windows.Forms.TabPage();
            this.tabMisc = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.tabContoler.SuspendLayout();
            this.tabInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 44);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(213, 498);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(137, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(371, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(12, 7);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(119, 31);
            this.buttonLoad.TabIndex = 3;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // tabContoler
            // 
            this.tabContoler.Controls.Add(this.tabInfo);
            this.tabContoler.Controls.Add(this.tabPlayers);
            this.tabContoler.Controls.Add(this.tabMisc);
            this.tabContoler.Location = new System.Drawing.Point(231, 44);
            this.tabContoler.Name = "tabContoler";
            this.tabContoler.SelectedIndex = 0;
            this.tabContoler.Size = new System.Drawing.Size(441, 498);
            this.tabContoler.TabIndex = 4;
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.dataGridView1);
            this.tabInfo.Location = new System.Drawing.Point(4, 22);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabInfo.Size = new System.Drawing.Size(433, 472);
            this.tabInfo.TabIndex = 0;
            this.tabInfo.Text = "Info";
            this.tabInfo.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.Key, this.Data });
            this.dataGridView1.Location = new System.Drawing.Point(6, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(421, 460);
            this.dataGridView1.TabIndex = 0;
            // 
            // Key
            // 
            this.Key.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Key.HeaderText = "Key";
            this.Key.Name = "Key";
            this.Key.ReadOnly = true;
            // 
            // Data
            // 
            this.Data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            // 
            // tabPlayers
            // 
            this.tabPlayers.Location = new System.Drawing.Point(4, 22);
            this.tabPlayers.Name = "tabPlayers";
            this.tabPlayers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlayers.Size = new System.Drawing.Size(433, 472);
            this.tabPlayers.TabIndex = 1;
            this.tabPlayers.Text = "Players";
            this.tabPlayers.UseVisualStyleBackColor = true;
            // 
            // tabMisc
            // 
            this.tabMisc.Location = new System.Drawing.Point(4, 22);
            this.tabMisc.Name = "tabMisc";
            this.tabMisc.Padding = new System.Windows.Forms.Padding(3);
            this.tabMisc.Size = new System.Drawing.Size(433, 472);
            this.tabMisc.TabIndex = 2;
            this.tabMisc.Text = "Misc";
            this.tabMisc.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(528, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 35);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabContoler);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.comboBox1);
            this.Name = "Form1";
            this.Text = "League Packet Reader Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabContoler.ResumeLayout(false);
            this.tabInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;

        private System.Windows.Forms.DataGridView dataGridView1;

        private System.Windows.Forms.TabPage tabMisc;

        private System.Windows.Forms.TabControl tabContoler;
        private System.Windows.Forms.TabPage tabInfo;
        private System.Windows.Forms.TabPage tabPlayers;

        private System.Windows.Forms.Button buttonLoad;

        private System.Windows.Forms.ComboBox comboBox1;

        private System.Windows.Forms.ListBox listBox1;

        #endregion
    }
}