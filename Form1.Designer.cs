
namespace CopyToDestination
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.Source = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Destination = new System.Windows.Forms.TextBox();
            this.GetTheResult = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.fileTransfer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.showPercent = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.listBox = new System.Windows.Forms.ListBox();
            this.browseSource = new System.Windows.Forms.Button();
            this.browseDest = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cancelTheOperation = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.folderBrowserDialogEx1 = new Ionic.Utils.FolderBrowserDialogEx();
            this.btnHelp = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(202, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source";
            // 
            // Source
            // 
            this.Source.Location = new System.Drawing.Point(290, 100);
            this.Source.Name = "Source";
            this.Source.ReadOnly = true;
            this.Source.Size = new System.Drawing.Size(184, 20);
            this.Source.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(202, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Destination";
            // 
            // Destination
            // 
            this.Destination.Location = new System.Drawing.Point(290, 129);
            this.Destination.Name = "Destination";
            this.Destination.ReadOnly = true;
            this.Destination.Size = new System.Drawing.Size(184, 20);
            this.Destination.TabIndex = 4;
            // 
            // GetTheResult
            // 
            this.GetTheResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GetTheResult.Location = new System.Drawing.Point(290, 165);
            this.GetTheResult.Name = "GetTheResult";
            this.GetTheResult.Size = new System.Drawing.Size(72, 28);
            this.GetTheResult.TabIndex = 6;
            this.GetTheResult.Text = "Copy";
            this.GetTheResult.UseVisualStyleBackColor = true;
            this.GetTheResult.Click += new System.EventHandler(this.GetTheResult_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileTransfer,
            this.toolStripStatusLabel1,
            this.showPercent,
            this.ProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 369);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(751, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // fileTransfer
            // 
            this.fileTransfer.Name = "fileTransfer";
            this.fileTransfer.Size = new System.Drawing.Size(279, 17);
            this.fileTransfer.Text = "Please Select a File/Folder To Copy To Destination...";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(355, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // showPercent
            // 
            this.showPercent.Name = "showPercent";
            this.showPercent.Size = new System.Drawing.Size(0, 17);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(0, 245);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(751, 121);
            this.listBox.TabIndex = 7;
            // 
            // browseSource
            // 
            this.browseSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseSource.Location = new System.Drawing.Point(480, 97);
            this.browseSource.Name = "browseSource";
            this.browseSource.Size = new System.Drawing.Size(60, 23);
            this.browseSource.TabIndex = 2;
            this.browseSource.Text = "Browse";
            this.browseSource.UseVisualStyleBackColor = true;
            this.browseSource.Click += new System.EventHandler(this.browseSource_Click);
            // 
            // browseDest
            // 
            this.browseDest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseDest.Location = new System.Drawing.Point(480, 127);
            this.browseDest.Name = "browseDest";
            this.browseDest.Size = new System.Drawing.Size(60, 23);
            this.browseDest.TabIndex = 5;
            this.browseDest.Text = "Browse";
            this.browseDest.UseVisualStyleBackColor = true;
            this.browseDest.Click += new System.EventHandler(this.browseDest_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(285, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(196, 26);
            this.label3.TabIndex = 9;
            this.label3.Text = "CopyToDestination";
            // 
            // cancelTheOperation
            // 
            this.cancelTheOperation.Enabled = false;
            this.cancelTheOperation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelTheOperation.Location = new System.Drawing.Point(368, 165);
            this.cancelTheOperation.Name = "cancelTheOperation";
            this.cancelTheOperation.Size = new System.Drawing.Size(75, 28);
            this.cancelTheOperation.TabIndex = 10;
            this.cancelTheOperation.Text = "Cancel";
            this.cancelTheOperation.UseVisualStyleBackColor = true;
            this.cancelTheOperation.Click += new System.EventHandler(this.cancelTheOperation_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(110, 21);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // folderBrowserDialogEx1
            // 
            this.folderBrowserDialogEx1.Description = "";
            this.folderBrowserDialogEx1.DontIncludeNetworkFoldersBelowDomainLevel = false;
            this.folderBrowserDialogEx1.NewStyle = true;
            this.folderBrowserDialogEx1.RootFolder = System.Environment.SpecialFolder.Desktop;
            this.folderBrowserDialogEx1.SelectedPath = "";
            this.folderBrowserDialogEx1.ShowBothFilesAndFolders = false;
            this.folderBrowserDialogEx1.ShowEditBox = true;
            this.folderBrowserDialogEx1.ShowFullPathInEditBox = true;
            this.folderBrowserDialogEx1.ShowNewFolderButton = true;
            // 
            // btnHelp
            // 
            this.btnHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.Location = new System.Drawing.Point(691, 12);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(48, 32);
            this.btnHelp.TabIndex = 12;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 391);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cancelTheOperation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.browseDest);
            this.Controls.Add(this.browseSource);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.GetTheResult);
            this.Controls.Add(this.Destination);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Source);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "CopyToDestination";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Source;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Destination;
        private System.Windows.Forms.Button GetTheResult;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel fileTransfer;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.ToolStripStatusLabel showPercent;
        private Ionic.Utils.FolderBrowserDialogEx folderBrowserDialogEx1;
        private System.Windows.Forms.Button browseSource;
        private System.Windows.Forms.Button browseDest;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cancelTheOperation;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnHelp;
    }
}

