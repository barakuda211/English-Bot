using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Alvas.Audio;
using System.Collections.Generic;

namespace CdaCs
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class MainForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxDrives;
        private System.Windows.Forms.Button buttonOpen;

        private System.Windows.Forms.StatusBar statusBar;
        private System.Windows.Forms.GroupBox groupBoxCDCtrls;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelTracks;
        private System.Windows.Forms.ListView listViewTracks;
        private System.Windows.Forms.ColumnHeader columnHeaderTrack;
        private System.Windows.Forms.ColumnHeader columnHeaderSize;
        private System.Windows.Forms.ColumnHeader columnHeaderType;
        private System.Windows.Forms.Button buttonSaveAs;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.ComponentModel.IContainer components;

        private CdDrive cda;
        private bool ripping = false;
        private System.Windows.Forms.ToolTip toolTip1;
        private ColumnHeader columnHeaderTitle;
        private bool m_CancelRipping = false;

        public MainForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxDrives = new System.Windows.Forms.ComboBox();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.groupBoxCDCtrls = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.buttonSaveAs = new System.Windows.Forms.Button();
            this.listViewTracks = new System.Windows.Forms.ListView();
            this.columnHeaderTrack = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderSize = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderType = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderTitle = new System.Windows.Forms.ColumnHeader();
            this.labelTracks = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxCDCtrls.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "CD Drives:";
            // 
            // comboBoxDrives
            // 
            this.comboBoxDrives.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDrives.Location = new System.Drawing.Point(80, 8);
            this.comboBoxDrives.Name = "comboBoxDrives";
            this.comboBoxDrives.Size = new System.Drawing.Size(80, 21);
            this.comboBoxDrives.TabIndex = 2;
            this.comboBoxDrives.SelectedIndexChanged += new System.EventHandler(this.comboBoxDrives_SelectedIndexChanged);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Enabled = false;
            this.buttonOpen.Location = new System.Drawing.Point(176, 8);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonOpen.TabIndex = 3;
            this.buttonOpen.Text = "Open";
            this.toolTip1.SetToolTip(this.buttonOpen, "Open/Close the CD drive");
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 337);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(566, 22);
            this.statusBar.TabIndex = 4;
            // 
            // groupBoxCDCtrls
            // 
            this.groupBoxCDCtrls.Controls.Add(this.progressBar1);
            this.groupBoxCDCtrls.Controls.Add(this.buttonSaveAs);
            this.groupBoxCDCtrls.Controls.Add(this.listViewTracks);
            this.groupBoxCDCtrls.Controls.Add(this.labelTracks);
            this.groupBoxCDCtrls.Controls.Add(this.label2);
            this.groupBoxCDCtrls.Enabled = false;
            this.groupBoxCDCtrls.Location = new System.Drawing.Point(16, 40);
            this.groupBoxCDCtrls.Name = "groupBoxCDCtrls";
            this.groupBoxCDCtrls.Size = new System.Drawing.Size(532, 272);
            this.groupBoxCDCtrls.TabIndex = 5;
            this.groupBoxCDCtrls.TabStop = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(87, 240);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(429, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // buttonSaveAs
            // 
            this.buttonSaveAs.Enabled = false;
            this.buttonSaveAs.Location = new System.Drawing.Point(6, 240);
            this.buttonSaveAs.Name = "buttonSaveAs";
            this.buttonSaveAs.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveAs.TabIndex = 6;
            this.buttonSaveAs.Text = "Save as...";
            this.buttonSaveAs.Click += new System.EventHandler(this.buttonSaveAs_Click);
            // 
            // listViewTracks
            // 
            this.listViewTracks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTrack,
            this.columnHeaderSize,
            this.columnHeaderType,
            this.columnHeaderTitle});
            this.listViewTracks.FullRowSelect = true;
            this.listViewTracks.Location = new System.Drawing.Point(0, 40);
            this.listViewTracks.Name = "listViewTracks";
            this.listViewTracks.Size = new System.Drawing.Size(516, 192);
            this.listViewTracks.TabIndex = 5;
            this.toolTip1.SetToolTip(this.listViewTracks, "Select the track that you want to save");
            this.listViewTracks.UseCompatibleStateImageBehavior = false;
            this.listViewTracks.View = System.Windows.Forms.View.Details;
            this.listViewTracks.EnabledChanged += new System.EventHandler(this.listViewTracks_EnabledChanged);
            this.listViewTracks.SelectedIndexChanged += new System.EventHandler(this.listViewTracks_SelectedIndexChanged);
            // 
            // columnHeaderTrack
            // 
            this.columnHeaderTrack.Text = "Track";
            this.columnHeaderTrack.Width = 62;
            // 
            // columnHeaderSize
            // 
            this.columnHeaderSize.Text = "Size (bytes)";
            this.columnHeaderSize.Width = 105;
            // 
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "Type";
            this.columnHeaderType.Width = 115;
            // 
            // columnHeaderTitle
            // 
            this.columnHeaderTitle.Text = "Title";
            this.columnHeaderTitle.Width = 200;
            // 
            // labelTracks
            // 
            this.labelTracks.Location = new System.Drawing.Point(168, 16);
            this.labelTracks.Name = "labelTracks";
            this.labelTracks.Size = new System.Drawing.Size(88, 16);
            this.labelTracks.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tracks:";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "wav";
            this.saveFileDialog.Filter = "Wave files (*.wav)|*.wav|All files (*.*)|*.*";
            this.saveFileDialog.Title = "Save tract to:";
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(566, 359);
            this.Controls.Add(this.groupBoxCDCtrls);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.comboBoxDrives);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "CDA Demo";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainWindow_Closing);
            this.groupBoxCDCtrls.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new MainForm());
        }

        private void MainWindow_Load(object sender, System.EventArgs e)
        {
            cda = new CdDrive();
            char[] Drives = CdDrive.GetCdDrives();
            foreach (char drive in Drives)
            {
                comboBoxDrives.Items.Add(drive.ToString());
            }
            if (comboBoxDrives.Items.Count > 0)
            {
                comboBoxDrives.SelectedIndex = 0;
            }
        }

        private void UpdateVisualControls()
        {
            buttonOpen.Enabled = !ripping & (comboBoxDrives.SelectedIndex >= 0);
            comboBoxDrives.Enabled = !ripping & (!cda.IsOpened);
            groupBoxCDCtrls.Enabled = !ripping & (cda.IsOpened);
            if (listViewTracks.SelectedIndices.Count > 0)
            {
                int track = listViewTracks.SelectedIndices[0];// +1;
                buttonSaveAs.Enabled = !ripping & cda.IsAudioTrack(track);
            }
            else
            {
                buttonSaveAs.Enabled = false;
            }
        }

        private void comboBoxDrives_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateVisualControls();
        }

        private static Dictionary<string, List<CdInfo>> dict = new Dictionary<string, List<CdInfo>>();

        public static List<CdInfo> GetCdInfo(string key)
        {
            if (!dict.ContainsKey(key))
            {
                dict[key] = CdDrive.GetCdInfo(key);
            }
            return dict[key];
        }

        private void buttonOpen_Click(object sender, System.EventArgs e)
        {
            if (cda.IsOpened)
            {
                cda.Close();
                buttonOpen.Text = "Open";
                statusBar.Text = "CD drive closed";
                listViewTracks.Items.Clear();
            }
            else
            {
                if (cda.Open(comboBoxDrives.Text[0]))
                {
                    statusBar.Text = "CD drive opened";
                    if (cda.IsCdReady())
                    {
                        statusBar.Text += " and ready";
                        if (cda.Refresh())
                        {
                            List<CdInfo> info = GetCdInfo(cda.GetCdQuery());
                            int Tracks = cda.GetTrackCount();
                            for (int i = 0; i < Tracks; i++)
                            {
                                string title = (info.Count == 0) ? "" : info[0].Tracks[i].Title;
                                ListViewItem item = new ListViewItem(new string[] { (i+1).ToString(), cda.TrackSize(i).ToString(), cda.IsAudioTrack(i) ? "audio" : "data", title });
                                listViewTracks.Items.Add(item);
                            }
                        }
                    }
                    buttonOpen.Text = "Close";
                }
                else
                {
                    statusBar.Text = "CD drive could not be opened";
                }
            }
            progressBar1.Value = 0;
            UpdateVisualControls();
        }

        private void listViewTracks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateVisualControls();
        }

        private void listViewTracks_EnabledChanged(object sender, System.EventArgs e)
        {
            UpdateVisualControls();
        }

        private void buttonSaveAs_Click(object sender, System.EventArgs e)
        {
            if (listViewTracks.SelectedIndices.Count > 0)
            {
                int ndx = listViewTracks.SelectedIndices[0];
                string title = string.Format("track{0:00}", ndx + 1);
                saveFileDialog.FileName = string.Format("{0}.wav", title);
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ripping = true;
                    try
                    {
                        statusBar.Text = string.Format("Reading track {0}", ndx + 1);
                        UpdateVisualControls();
                        IntPtr format = CdDrive.GetFormat();
                        WaveWriter ww = new WaveWriter(File.Create(saveFileDialog.FileName),
                            AudioCompressionManager.FormatBytes(format));
                        CdReader cr = cda.GetReader(ndx);
                        int durationInMS = cr.GetDurationInMS();
                        int max = durationInMS / 1000;
                        progressBar1.Minimum = 0;
                        progressBar1.Value = 0;
                        progressBar1.Maximum = max + 1;
                        for (int i = 0; i <= max; i++)
                        {
                            byte[] data = cr.ReadData(i, 1);
                            ww.WriteData(data);
                            progressBar1.Value = i + 1;
                            Application.DoEvents();
                        }
                        cr.Close();
                        ww.Close();
                        DsConvert.ToWma(saveFileDialog.FileName, saveFileDialog.FileName + ".wma",
                            DsConvert.WmaProfile.Stereo128);
                    }
                    finally
                    {
                        ripping = false;
                    }
                }
            }
            UpdateVisualControls();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ripping)
            {
                if (MessageBox.Show("Are you to cancel?", this.Text,
                  MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.m_CancelRipping = true;
                }
                e.Cancel = true;
            }
        }

    }
}
