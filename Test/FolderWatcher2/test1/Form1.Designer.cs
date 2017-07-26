namespace test1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lookBut = new System.Windows.Forms.Button();
            this.textFile = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.folderDialogSee = new System.Windows.Forms.FolderBrowserDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.createBox = new System.Windows.Forms.CheckBox();
            this.updateBox = new System.Windows.Forms.CheckBox();
            this.seeBox = new System.Windows.Forms.CheckBox();
            this.deleteBox = new System.Windows.Forms.CheckBox();
            this.giveBut = new System.Windows.Forms.Button();
            this.clearBut = new System.Windows.Forms.Button();
            this.endBut = new System.Windows.Forms.Button();
            this.startBut = new System.Windows.Forms.Button();
            this.fileLSystemWatcher = new System.IO.FileSystemWatcher();
            this.GridView = new System.Windows.Forms.DataGridView();
            this.gvOne = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvTwo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvThree = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvFour = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.log = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.fileLSystemWatcher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // lookBut
            // 
            this.lookBut.Location = new System.Drawing.Point(329, 10);
            this.lookBut.Name = "lookBut";
            this.lookBut.Size = new System.Drawing.Size(52, 23);
            this.lookBut.TabIndex = 0;
            this.lookBut.Text = "see";
            this.lookBut.UseVisualStyleBackColor = true;
            this.lookBut.Click += new System.EventHandler(this.lookBut_Click);
            // 
            // textFile
            // 
            this.textFile.Location = new System.Drawing.Point(122, 12);
            this.textFile.Name = "textFile";
            this.textFile.ReadOnly = true;
            this.textFile.Size = new System.Drawing.Size(180, 24);
            this.textFile.TabIndex = 1;
            this.textFile.Text = "";
            this.textFile.TextChanged += new System.EventHandler(this.textFile_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "待监视目录";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "监视类型";
            // 
            // createBox
            // 
            this.createBox.AutoSize = true;
            this.createBox.Location = new System.Drawing.Point(122, 57);
            this.createBox.Name = "createBox";
            this.createBox.Size = new System.Drawing.Size(48, 16);
            this.createBox.TabIndex = 4;
            this.createBox.Text = "创建";
            this.createBox.UseVisualStyleBackColor = true;
            this.createBox.CheckedChanged += new System.EventHandler(this.createBox_CheckedChanged);
            // 
            // updateBox
            // 
            this.updateBox.AutoSize = true;
            this.updateBox.Location = new System.Drawing.Point(189, 57);
            this.updateBox.Name = "updateBox";
            this.updateBox.Size = new System.Drawing.Size(48, 16);
            this.updateBox.TabIndex = 5;
            this.updateBox.Text = "修改";
            this.updateBox.UseVisualStyleBackColor = true;
            this.updateBox.CheckedChanged += new System.EventHandler(this.updateBox_CheckedChanged);
            // 
            // seeBox
            // 
            this.seeBox.AutoSize = true;
            this.seeBox.Location = new System.Drawing.Point(254, 58);
            this.seeBox.Name = "seeBox";
            this.seeBox.Size = new System.Drawing.Size(48, 16);
            this.seeBox.TabIndex = 6;
            this.seeBox.Text = "浏览";
            this.seeBox.UseVisualStyleBackColor = true;
            this.seeBox.CheckedChanged += new System.EventHandler(this.seeBox_CheckedChanged);
            // 
            // deleteBox
            // 
            this.deleteBox.AutoSize = true;
            this.deleteBox.Location = new System.Drawing.Point(317, 57);
            this.deleteBox.Name = "deleteBox";
            this.deleteBox.Size = new System.Drawing.Size(48, 16);
            this.deleteBox.TabIndex = 7;
            this.deleteBox.Text = "删除";
            this.deleteBox.UseVisualStyleBackColor = true;
            this.deleteBox.CheckedChanged += new System.EventHandler(this.deleteBox_CheckedChanged);
            // 
            // giveBut
            // 
            this.giveBut.Location = new System.Drawing.Point(20, 222);
            this.giveBut.Name = "giveBut";
            this.giveBut.Size = new System.Drawing.Size(49, 23);
            this.giveBut.TabIndex = 9;
            this.giveBut.Text = "导出Ex..";
            this.giveBut.UseVisualStyleBackColor = true;
            this.giveBut.Click += new System.EventHandler(this.giveBut_Click);
            // 
            // clearBut
            // 
            this.clearBut.Location = new System.Drawing.Point(75, 222);
            this.clearBut.Name = "clearBut";
            this.clearBut.Size = new System.Drawing.Size(49, 23);
            this.clearBut.TabIndex = 10;
            this.clearBut.Text = "清空";
            this.clearBut.UseVisualStyleBackColor = true;
            this.clearBut.Click += new System.EventHandler(this.clearBut_Click);
            // 
            // endBut
            // 
            this.endBut.Location = new System.Drawing.Point(346, 258);
            this.endBut.Name = "endBut";
            this.endBut.Size = new System.Drawing.Size(49, 23);
            this.endBut.TabIndex = 11;
            this.endBut.Text = "暂停";
            this.endBut.UseVisualStyleBackColor = true;
            this.endBut.Click += new System.EventHandler(this.endBut_Click);
            // 
            // startBut
            // 
            this.startBut.Location = new System.Drawing.Point(291, 258);
            this.startBut.Name = "startBut";
            this.startBut.Size = new System.Drawing.Size(49, 23);
            this.startBut.TabIndex = 12;
            this.startBut.Text = "启动";
            this.startBut.UseVisualStyleBackColor = true;
            this.startBut.Click += new System.EventHandler(this.startBut_Click);
            // 
            // fileLSystemWatcher
            // 
            this.fileLSystemWatcher.EnableRaisingEvents = true;
            this.fileLSystemWatcher.SynchronizingObject = this;
            this.fileLSystemWatcher.Changed += new System.IO.FileSystemEventHandler(this.fileLSystemWatcher_Changed);
            // 
            // GridView
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gvOne,
            this.gvTwo,
            this.gvThree,
            this.gvFour});
            this.GridView.Location = new System.Drawing.Point(31, 79);
            this.GridView.Name = "GridView";
            this.GridView.RowHeadersVisible = false;
            this.GridView.RowTemplate.Height = 23;
            this.GridView.Size = new System.Drawing.Size(364, 133);
            this.GridView.TabIndex = 13;
            this.GridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridView_CellContentClick);
            // 
            // gvOne
            // 
            this.gvOne.HeaderText = "序号";
            this.gvOne.Name = "gvOne";
            // 
            // gvTwo
            // 
            this.gvTwo.HeaderText = "时间";
            this.gvTwo.Name = "gvTwo";
            // 
            // gvThree
            // 
            this.gvThree.HeaderText = "文件名";
            this.gvThree.Name = "gvThree";
            // 
            // gvFour
            // 
            this.gvFour.HeaderText = "操作";
            this.gvFour.Name = "gvFour";
            // 
            // log
            // 
            this.log.AutoSize = true;
            this.log.ForeColor = System.Drawing.Color.Red;
            this.log.Location = new System.Drawing.Point(18, 263);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(185, 12);
            this.log.TabIndex = 14;
            this.log.Text = "1024条时自动导出到桌面-当前 :0";
            this.log.Click += new System.EventHandler(this.log_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 293);
            this.Controls.Add(this.log);
            this.Controls.Add(this.GridView);
            this.Controls.Add(this.startBut);
            this.Controls.Add(this.endBut);
            this.Controls.Add(this.clearBut);
            this.Controls.Add(this.giveBut);
            this.Controls.Add(this.deleteBox);
            this.Controls.Add(this.seeBox);
            this.Controls.Add(this.updateBox);
            this.Controls.Add(this.createBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textFile);
            this.Controls.Add(this.lookBut);
            this.Name = "Form1";
            this.Text = "目录监视器--席自杭";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fileLSystemWatcher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button lookBut;
        private System.Windows.Forms.RichTextBox textFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderDialogSee;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox createBox;
        private System.Windows.Forms.CheckBox updateBox;
        private System.Windows.Forms.CheckBox seeBox;
        private System.Windows.Forms.CheckBox deleteBox;
        private System.Windows.Forms.Button giveBut;
        private System.Windows.Forms.Button clearBut;
        private System.Windows.Forms.Button endBut;
        private System.Windows.Forms.Button startBut;
        private System.IO.FileSystemWatcher fileLSystemWatcher;
        private System.Windows.Forms.DataGridView GridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvOne;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvTwo;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvThree;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvFour;
        private System.Windows.Forms.Label log;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

