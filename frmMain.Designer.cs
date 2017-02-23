namespace FontAssistant
{
    partial class frmMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssUniteTTC = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssAFDKO = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssTtfName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstLogs = new System.Windows.Forms.ListView();
            this.colFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tpSplit = new System.Windows.Forms.TabPage();
            this.tpCombine = new System.Windows.Forms.TabPage();
            this.tpGetXml = new System.Windows.Forms.TabPage();
            this.tpModify = new System.Windows.Forms.TabPage();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tssUniteTTC,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.tssAFDKO,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel5,
            this.tssTtfName,
            this.toolStripStatusLabel6,
            this.tssStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 693);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1107, 37);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(137, 32);
            this.toolStripStatusLabel1.Text = "UniteTTC：";
            // 
            // tssUniteTTC
            // 
            this.tssUniteTTC.ForeColor = System.Drawing.Color.Green;
            this.tssUniteTTC.Name = "tssUniteTTC";
            this.tssUniteTTC.Size = new System.Drawing.Size(65, 32);
            this.tssUniteTTC.Text = "找到";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(21, 32);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(115, 32);
            this.toolStripStatusLabel3.Text = "AFDKO：";
            // 
            // tssAFDKO
            // 
            this.tssAFDKO.ForeColor = System.Drawing.Color.Green;
            this.tssAFDKO.Name = "tssAFDKO";
            this.tssAFDKO.Size = new System.Drawing.Size(65, 32);
            this.tssAFDKO.Text = "找到";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(21, 32);
            this.toolStripStatusLabel4.Text = "|";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(172, 32);
            this.toolStripStatusLabel5.Text = "ttfname3_zh：";
            // 
            // tssTtfName
            // 
            this.tssTtfName.ForeColor = System.Drawing.Color.Green;
            this.tssTtfName.Name = "tssTtfName";
            this.tssTtfName.Size = new System.Drawing.Size(65, 32);
            this.tssTtfName.Text = "找到";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(316, 32);
            this.toolStripStatusLabel6.Spring = true;
            this.toolStripStatusLabel6.Text = " ";
            // 
            // tssStatus
            // 
            this.tssStatus.Name = "tssStatus";
            this.tssStatus.Size = new System.Drawing.Size(115, 33);
            this.tssStatus.Text = "准备就绪";
            this.tssStatus.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstLogs);
            this.splitContainer1.Panel1Collapsed = true;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabMain);
            this.splitContainer1.Size = new System.Drawing.Size(1107, 693);
            this.splitContainer1.SplitterDistance = 369;
            this.splitContainer1.TabIndex = 2;
            // 
            // lstLogs
            // 
            this.lstLogs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFileName,
            this.colOp,
            this.colStatus});
            this.lstLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLogs.Location = new System.Drawing.Point(0, 0);
            this.lstLogs.Name = "lstLogs";
            this.lstLogs.Size = new System.Drawing.Size(369, 693);
            this.lstLogs.TabIndex = 0;
            this.lstLogs.UseCompatibleStateImageBehavior = false;
            this.lstLogs.View = System.Windows.Forms.View.Details;
            // 
            // colFileName
            // 
            this.colFileName.Text = "文件";
            // 
            // colOp
            // 
            this.colOp.Text = "操作";
            this.colOp.Width = 111;
            // 
            // colStatus
            // 
            this.colStatus.Text = "当前状态";
            this.colStatus.Width = 111;
            // 
            // tabMain
            // 
            this.tabMain.AllowDrop = true;
            this.tabMain.Controls.Add(this.tpSplit);
            this.tabMain.Controls.Add(this.tpCombine);
            this.tabMain.Controls.Add(this.tpGetXml);
            this.tabMain.Controls.Add(this.tpModify);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1107, 693);
            this.tabMain.TabIndex = 0;
            this.tabMain.Tag = "";
            this.tabMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.tabMain_DragDrop);
            this.tabMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.tabMain_DragEnter);
            // 
            // tpSplit
            // 
            this.tpSplit.Location = new System.Drawing.Point(8, 39);
            this.tpSplit.Name = "tpSplit";
            this.tpSplit.Padding = new System.Windows.Forms.Padding(3);
            this.tpSplit.Size = new System.Drawing.Size(718, 646);
            this.tpSplit.TabIndex = 0;
            this.tpSplit.Tag = "ttc/otc";
            this.tpSplit.Text = "字体拆分";
            this.tpSplit.UseVisualStyleBackColor = true;
            // 
            // tpCombine
            // 
            this.tpCombine.Location = new System.Drawing.Point(8, 39);
            this.tpCombine.Name = "tpCombine";
            this.tpCombine.Padding = new System.Windows.Forms.Padding(3);
            this.tpCombine.Size = new System.Drawing.Size(718, 646);
            this.tpCombine.TabIndex = 1;
            this.tpCombine.Tag = "ttf/otf";
            this.tpCombine.Text = "字体合并";
            this.tpCombine.UseVisualStyleBackColor = true;
            // 
            // tpGetXml
            // 
            this.tpGetXml.Location = new System.Drawing.Point(8, 39);
            this.tpGetXml.Name = "tpGetXml";
            this.tpGetXml.Padding = new System.Windows.Forms.Padding(3);
            this.tpGetXml.Size = new System.Drawing.Size(718, 646);
            this.tpGetXml.TabIndex = 2;
            this.tpGetXml.Tag = "ttf/otf";
            this.tpGetXml.Text = "提取信息";
            this.tpGetXml.UseVisualStyleBackColor = true;
            // 
            // tpModify
            // 
            this.tpModify.Location = new System.Drawing.Point(8, 39);
            this.tpModify.Name = "tpModify";
            this.tpModify.Padding = new System.Windows.Forms.Padding(3);
            this.tpModify.Size = new System.Drawing.Size(1091, 646);
            this.tpModify.TabIndex = 3;
            this.tpModify.Tag = "ttf/otf";
            this.tpModify.Text = "修改信息";
            this.tpModify.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 730);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.DoubleBuffered = true;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FontAssistant";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tssUniteTTC;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel tssAFDKO;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel tssTtfName;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel tssStatus;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lstLogs;
        private System.Windows.Forms.ColumnHeader colFileName;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ColumnHeader colOp;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tpSplit;
        private System.Windows.Forms.TabPage tpCombine;
        private System.Windows.Forms.TabPage tpGetXml;
        private System.Windows.Forms.TabPage tpModify;
    }
}

