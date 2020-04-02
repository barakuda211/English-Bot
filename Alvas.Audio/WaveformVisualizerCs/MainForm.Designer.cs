namespace WaveformVisualizerCs
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ilMain = new System.Windows.Forms.ImageList(this.components);
            this.tbMain = new System.Windows.Forms.ToolBar();
            this.tbbOpen = new System.Windows.Forms.ToolBarButton();
            this.tbbPlay = new System.Windows.Forms.ToolBarButton();
            this.tbbStop = new System.Windows.Forms.ToolBarButton();
            this.pMain = new System.Windows.Forms.Panel();
            this.ofdAudio = new System.Windows.Forms.OpenFileDialog();
            this.pgMain = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // ilMain
            // 
            this.ilMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilMain.ImageStream")));
            this.ilMain.TransparentColor = System.Drawing.Color.Red;
            this.ilMain.Images.SetKeyName(0, "openHS.png");
            this.ilMain.Images.SetKeyName(1, "");
            this.ilMain.Images.SetKeyName(2, "");
            // 
            // tbMain
            // 
            this.tbMain.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.tbMain.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbbOpen,
            this.tbbPlay,
            this.tbbStop});
            this.tbMain.DropDownArrows = true;
            this.tbMain.ImageList = this.ilMain;
            this.tbMain.Location = new System.Drawing.Point(0, 0);
            this.tbMain.Name = "tbMain";
            this.tbMain.ShowToolTips = true;
            this.tbMain.Size = new System.Drawing.Size(751, 28);
            this.tbMain.TabIndex = 6;
            this.tbMain.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbMain_ButtonClick);
            // 
            // tbbOpen
            // 
            this.tbbOpen.Enabled = false;
            this.tbbOpen.ImageIndex = 0;
            this.tbbOpen.Name = "tbbOpen";
            // 
            // tbbPlay
            // 
            this.tbbPlay.Enabled = false;
            this.tbbPlay.ImageIndex = 1;
            this.tbbPlay.Name = "tbbPlay";
            // 
            // tbbStop
            // 
            this.tbbStop.Enabled = false;
            this.tbbStop.ImageIndex = 2;
            this.tbbStop.Name = "tbbStop";
            // 
            // pMain
            // 
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Location = new System.Drawing.Point(0, 28);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(526, 375);
            this.pMain.TabIndex = 7;
            // 
            // ofdAudio
            // 
            this.ofdAudio.DefaultExt = "wav";
            // 
            // pgMain
            // 
            this.pgMain.Dock = System.Windows.Forms.DockStyle.Right;
            this.pgMain.Location = new System.Drawing.Point(526, 28);
            this.pgMain.Name = "pgMain";
            this.pgMain.Size = new System.Drawing.Size(225, 375);
            this.pgMain.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 403);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.pgMain);
            this.Controls.Add(this.tbMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "WaveformVisualizer example";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList ilMain;
        private System.Windows.Forms.ToolBar tbMain;
        private System.Windows.Forms.Panel pMain;
        private System.Windows.Forms.OpenFileDialog ofdAudio;
        private System.Windows.Forms.ToolBarButton tbbOpen;
        private System.Windows.Forms.ToolBarButton tbbPlay;
        private System.Windows.Forms.ToolBarButton tbbStop;
        private System.Windows.Forms.PropertyGrid pgMain;
    }
}

