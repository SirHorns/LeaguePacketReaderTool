namespace LPRT.MVVP.View
{
    partial class View
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(View));
            this.timelineFilter = new System.Windows.Forms.ComboBox();
            this.packetInfoTable = new System.Windows.Forms.DataGridView();
            this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetInfoText = new System.Windows.Forms.RichTextBox();
            this.packetTimelineList = new System.Windows.Forms.ListView();
            this.type = new System.Windows.Forms.ColumnHeader();
            this.position = new System.Windows.Forms.ColumnHeader();
            this.time = new System.Windows.Forms.ColumnHeader();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.timeline = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.timelinePlayerSelect = new System.Windows.Forms.ComboBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.info = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.playerInfo = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playerList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.advancedFilter = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.packetInfoTable)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.timeline.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.info.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playerInfo)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timelineFilter
            // 
            this.timelineFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.timelineFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.timelineFilter.FormattingEnabled = true;
            this.timelineFilter.Location = new System.Drawing.Point(81, 7);
            this.timelineFilter.Name = "timelineFilter";
            this.timelineFilter.Size = new System.Drawing.Size(318, 21);
            this.timelineFilter.TabIndex = 2;
            this.timelineFilter.SelectedValueChanged += new System.EventHandler(this.PacketTimeLineFilter_ValueChanged);
            // 
            // packetInfoTable
            // 
            this.packetInfoTable.AllowUserToAddRows = false;
            this.packetInfoTable.AllowUserToDeleteRows = false;
            this.packetInfoTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.packetInfoTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.packetInfoTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.packetInfoTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.Key, this.Data });
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.packetInfoTable.DefaultCellStyle = dataGridViewCellStyle2;
            this.packetInfoTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetInfoTable.Location = new System.Drawing.Point(0, 0);
            this.packetInfoTable.Name = "packetInfoTable";
            this.packetInfoTable.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.packetInfoTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.packetInfoTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.packetInfoTable.Size = new System.Drawing.Size(415, 613);
            this.packetInfoTable.TabIndex = 0;
            // 
            // Key
            // 
            this.Key.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Key.FillWeight = 30F;
            this.Key.HeaderText = "KEY";
            this.Key.Name = "Key";
            this.Key.ReadOnly = true;
            this.Key.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Data
            // 
            this.Data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Data.FillWeight = 70F;
            this.Data.HeaderText = "VALUE";
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            // 
            // packetInfoText
            // 
            this.packetInfoText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.packetInfoText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetInfoText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.packetInfoText.Location = new System.Drawing.Point(0, 0);
            this.packetInfoText.Name = "packetInfoText";
            this.packetInfoText.ReadOnly = true;
            this.packetInfoText.Size = new System.Drawing.Size(412, 613);
            this.packetInfoText.TabIndex = 2;
            this.packetInfoText.Text = "";
            // 
            // packetTimelineList
            // 
            this.packetTimelineList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.type, this.position, this.time });
            this.packetTimelineList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetTimelineList.GridLines = true;
            this.packetTimelineList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.packetTimelineList.HideSelection = false;
            this.packetTimelineList.Location = new System.Drawing.Point(3, 3);
            this.packetTimelineList.Name = "packetTimelineList";
            this.packetTimelineList.Size = new System.Drawing.Size(379, 554);
            this.packetTimelineList.TabIndex = 3;
            this.packetTimelineList.UseCompatibleStateImageBehavior = false;
            this.packetTimelineList.View = System.Windows.Forms.View.Details;
            this.packetTimelineList.CacheVirtualItems += new System.Windows.Forms.CacheVirtualItemsEventHandler(this.TimeLine_CacheVirtualItems);
            this.packetTimelineList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.packetTimelineList_ItemSelectionChanged);
            this.packetTimelineList.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.TimeLine_RetrieveVirtualItem);
            this.packetTimelineList.SearchForVirtualItem += new System.Windows.Forms.SearchForVirtualItemEventHandler(this.TimeLine_SearchForVirtualItem);
            this.packetTimelineList.SelectedIndexChanged += new System.EventHandler(this.packetTimelineList_SelectedIndexChanged);
            // 
            // type
            // 
            this.type.Text = "Type";
            this.type.Width = 200;
            // 
            // position
            // 
            this.position.Text = "Position";
            this.position.Width = 80;
            // 
            // time
            // 
            this.time.Text = "Time";
            this.time.Width = 80;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.timeline);
            this.tabControl1.Controls.Add(this.info);
            this.tabControl1.Controls.Add(this.advancedFilter);
            this.tabControl1.Location = new System.Drawing.Point(0, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1252, 651);
            this.tabControl1.TabIndex = 8;
            // 
            // timeline
            // 
            this.timeline.Controls.Add(this.label2);
            this.timeline.Controls.Add(this.tabControl3);
            this.timeline.Controls.Add(this.timelineFilter);
            this.timeline.Controls.Add(this.splitContainer1);
            this.timeline.Location = new System.Drawing.Point(4, 22);
            this.timeline.Name = "timeline";
            this.timeline.Padding = new System.Windows.Forms.Padding(3);
            this.timeline.Size = new System.Drawing.Size(1244, 625);
            this.timeline.TabIndex = 0;
            this.timeline.Text = "Timeline";
            this.timeline.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 21);
            this.label2.TabIndex = 11;
            this.label2.Text = "PacketType:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage3);
            this.tabControl3.Controls.Add(this.tabPage4);
            this.tabControl3.Location = new System.Drawing.Point(8, 34);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(393, 586);
            this.tabControl3.TabIndex = 10;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.packetTimelineList);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(385, 560);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Position";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.comboBox1);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.timelinePlayerSelect);
            this.tabPage4.Controls.Add(this.listView1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(385, 560);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Sender";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "Sent/Received", "Sent", "Received" });
            this.comboBox1.Location = new System.Drawing.Point(225, 11);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "NetClient: ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timelinePlayerSelect
            // 
            this.timelinePlayerSelect.FormattingEnabled = true;
            this.timelinePlayerSelect.Location = new System.Drawing.Point(69, 11);
            this.timelinePlayerSelect.Name = "timelinePlayerSelect";
            this.timelinePlayerSelect.Size = new System.Drawing.Size(150, 21);
            this.timelinePlayerSelect.TabIndex = 1;
            // 
            // listView1
            // 
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(6, 40);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(373, 515);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.VirtualMode = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(407, 6);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.packetInfoTable);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.packetInfoText);
            this.splitContainer1.Size = new System.Drawing.Size(831, 613);
            this.splitContainer1.SplitterDistance = 415;
            this.splitContainer1.TabIndex = 0;
            // 
            // info
            // 
            this.info.Controls.Add(this.tabControl2);
            this.info.Location = new System.Drawing.Point(4, 22);
            this.info.Name = "info";
            this.info.Padding = new System.Windows.Forms.Padding(3);
            this.info.Size = new System.Drawing.Size(1244, 625);
            this.info.TabIndex = 2;
            this.info.Text = "Info";
            this.info.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Location = new System.Drawing.Point(8, 6);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(600, 400);
            this.tabControl2.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.playerInfo);
            this.tabPage1.Controls.Add(this.playerList);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(592, 374);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Player Info";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // playerInfo
            // 
            this.playerInfo.AllowUserToAddRows = false;
            this.playerInfo.AllowUserToDeleteRows = false;
            this.playerInfo.AllowUserToResizeColumns = false;
            this.playerInfo.AllowUserToResizeRows = false;
            this.playerInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.playerInfo.ColumnHeadersVisible = false;
            this.playerInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.Column1, this.Column2 });
            this.playerInfo.Location = new System.Drawing.Point(112, 52);
            this.playerInfo.Name = "playerInfo";
            this.playerInfo.ReadOnly = true;
            this.playerInfo.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.playerInfo.Size = new System.Drawing.Size(254, 316);
            this.playerInfo.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.FillWeight = 50F;
            this.Column1.HeaderText = "column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.FillWeight = 50F;
            this.Column2.HeaderText = "column2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // playerList
            // 
            this.playerList.FormattingEnabled = true;
            this.playerList.Location = new System.Drawing.Point(6, 52);
            this.playerList.Name = "playerList";
            this.playerList.Size = new System.Drawing.Size(100, 316);
            this.playerList.Sorted = true;
            this.playerList.TabIndex = 0;
            this.playerList.SelectedIndexChanged += new System.EventHandler(this.playerList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Players:";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(592, 374);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "NetID Info";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // advancedFilter
            // 
            this.advancedFilter.Location = new System.Drawing.Point(4, 22);
            this.advancedFilter.Name = "advancedFilter";
            this.advancedFilter.Size = new System.Drawing.Size(1244, 625);
            this.advancedFilter.TabIndex = 3;
            this.advancedFilter.Text = "Advanced Filter";
            this.advancedFilter.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.fileToolStripMenuItem1 });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(1264, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.openToolStripMenuItem });
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "View";
            this.Text = "League Packet Reader Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.packetInfoTable)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.timeline.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.info.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.playerInfo)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ComboBox timelinePlayerSelect;

        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.ListView listView1;

        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;

        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;

        private System.Windows.Forms.DataGridView playerInfo;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;

        private System.Windows.Forms.ListBox playerList;

        private System.Windows.Forms.TabPage advancedFilter;

        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;

        private System.Windows.Forms.TabPage info;

        private System.Windows.Forms.SplitContainer splitContainer1;

        private System.Windows.Forms.ColumnHeader position;
        private System.Windows.Forms.ColumnHeader type;

        private System.Windows.Forms.ColumnHeader time;

        private System.Windows.Forms.ListView packetTimelineList;

        private System.Windows.Forms.RichTextBox packetInfoText;

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage timeline;
        private System.Windows.Forms.MenuStrip menuStrip1;

        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
        private System.Windows.Forms.DataGridView packetInfoTable;

        private System.Windows.Forms.ComboBox timelineFilter;

        #endregion
    }
}