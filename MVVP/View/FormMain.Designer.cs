namespace LPRT.MVVP.View
{
    partial class FormMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.packetTimelineFilter = new System.Windows.Forms.ComboBox();
            this.packetInfoTable = new System.Windows.Forms.DataGridView();
            this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetInfoText = new System.Windows.Forms.RichTextBox();
            this.packetTimeline = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxTimeLine = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Time = new System.Windows.Forms.ColumnHeader();
            this.Position = new System.Windows.Forms.ColumnHeader();
            this.Type = new System.Windows.Forms.ColumnHeader();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.packetInfoTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.packetTimeline)).BeginInit();
            this.groupBoxTimeLine.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // packetTimelineFilter
            // 
            this.packetTimelineFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.packetTimelineFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.packetTimelineFilter.FormattingEnabled = true;
            this.packetTimelineFilter.Location = new System.Drawing.Point(6, 19);
            this.packetTimelineFilter.Name = "packetTimelineFilter";
            this.packetTimelineFilter.Size = new System.Drawing.Size(337, 21);
            this.packetTimelineFilter.TabIndex = 2;
            this.packetTimelineFilter.SelectedValueChanged += new System.EventHandler(this.PacketTimeLineFilter_ValueChanged);
            // 
            // packetInfoTable
            // 
            this.packetInfoTable.AllowUserToAddRows = false;
            this.packetInfoTable.AllowUserToDeleteRows = false;
            this.packetInfoTable.AllowUserToResizeColumns = false;
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
            this.packetInfoTable.Location = new System.Drawing.Point(386, 46);
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
            this.packetInfoTable.Size = new System.Drawing.Size(427, 561);
            this.packetInfoTable.TabIndex = 0;
            // 
            // Key
            // 
            this.Key.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Key.HeaderText = "KEY";
            this.Key.Name = "Key";
            this.Key.ReadOnly = true;
            this.Key.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Key.Width = 53;
            // 
            // Data
            // 
            this.Data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Data.HeaderText = "VALUE";
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            // 
            // packetInfoText
            // 
            this.packetInfoText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.packetInfoText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.packetInfoText.Location = new System.Drawing.Point(819, 46);
            this.packetInfoText.Name = "packetInfoText";
            this.packetInfoText.ReadOnly = true;
            this.packetInfoText.Size = new System.Drawing.Size(407, 561);
            this.packetInfoText.TabIndex = 2;
            this.packetInfoText.Text = "";
            // 
            // packetTimeline
            // 
            this.packetTimeline.AllowUserToAddRows = false;
            this.packetTimeline.AllowUserToDeleteRows = false;
            this.packetTimeline.AllowUserToResizeColumns = false;
            this.packetTimeline.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.packetTimeline.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.packetTimeline.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.packetTimeline.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.Column3, this.Column1, this.Column2 });
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.packetTimeline.DefaultCellStyle = dataGridViewCellStyle6;
            this.packetTimeline.Location = new System.Drawing.Point(6, 46);
            this.packetTimeline.Name = "packetTimeline";
            this.packetTimeline.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.packetTimeline.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.packetTimeline.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.packetTimeline.Size = new System.Drawing.Size(374, 561);
            this.packetTimeline.TabIndex = 6;
            this.packetTimeline.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PacketTimeLine_CellCLick);
            this.packetTimeline.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.PacketTimeLine_CellFocus);
            this.packetTimeline.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.packetTimeline_CellValueNeeded);
            this.packetTimeline.NewRowNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.packetTimeline_NewRowNeeded);
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column3.HeaderText = "Time";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 36;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column1.HeaderText = "Pos.";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 34;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Type";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // groupBoxTimeLine
            // 
            this.groupBoxTimeLine.Controls.Add(this.packetInfoText);
            this.groupBoxTimeLine.Controls.Add(this.packetInfoTable);
            this.groupBoxTimeLine.Controls.Add(this.packetTimeline);
            this.groupBoxTimeLine.Controls.Add(this.packetTimelineFilter);
            this.groupBoxTimeLine.Location = new System.Drawing.Point(6, 6);
            this.groupBoxTimeLine.Name = "groupBoxTimeLine";
            this.groupBoxTimeLine.Size = new System.Drawing.Size(1232, 613);
            this.groupBoxTimeLine.TabIndex = 7;
            this.groupBoxTimeLine.TabStop = false;
            this.groupBoxTimeLine.Text = "Packet Timeline";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1252, 651);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBoxTimeLine);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1244, 625);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "TimeLine";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1244, 625);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Player Info";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.Time, this.Position, this.Type });
            this.listView1.Location = new System.Drawing.Point(69, 43);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(414, 400);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Time
            // 
            this.Time.Text = "Time";
            this.Time.Width = 120;
            // 
            // Position
            // 
            this.Position.Text = "Position";
            this.Position.Width = 90;
            // 
            // Type
            // 
            this.Type.Text = "Type";
            this.Type.Width = 200;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.fileToolStripMenuItem });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.loadToolStripMenuItem });
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.MenuBar_ClickLoad);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "League Packet Reader Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.packetInfoTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.packetTimeline)).EndInit();
            this.groupBoxTimeLine.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ColumnHeader Time;
        private System.Windows.Forms.ColumnHeader Position;
        private System.Windows.Forms.ColumnHeader Type;

        private System.Windows.Forms.ListView listView1;

        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;

        private System.Windows.Forms.RichTextBox packetInfoText;

        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.MenuStrip menuStrip1;

        private System.Windows.Forms.GroupBox groupBoxTimeLine;

        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
        private System.Windows.Forms.DataGridView packetInfoTable;

        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;

        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;

        private System.Windows.Forms.DataGridView packetTimeline;

        private System.Windows.Forms.ComboBox packetTimelineFilter;

        #endregion
    }
}