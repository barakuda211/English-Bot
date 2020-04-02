using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Alvas.Audio;
using System.Text.RegularExpressions;

namespace AudioExCS
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	internal class frmMain : System.Windows.Forms.Form
	{
        private AudioExCS.DictaphonePanel dictaphonePanel1;
        private AudioExCS.ConvertPanel convertPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private TabPage tabPage2;
        private VoxPanel voxPanel1;
        private TabPage tabPage1;
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new frmMain());
        }

        public frmMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            //CreateAllMp3HeaderFormats(AudioCompressionManager.MpegLayer3FormatTag);
        }

        private static string GetArrayString(byte[] arr)
        {
            int size = arr.Length;
            string[] arrStr = new string[size];
            for (int i = 0; i < size; i++)
            {
                arrStr[i] = arr[i].ToString();
            }
            return string.Join(", ", arrStr);
        }

        private static void CreateAllMp3HeaderFormats(short formatTag)
        {
            //Dictionary<string, string> dict = new Dictionary<string, string>();
            //List<string> dict = new List<string>();
            string res = string.Empty;
            //string path = Application.StartupPath;
            Regex ge = new Regex(@"\W");//[,\f/]
            foreach (FormatDetails var in AudioCompressionManager.GetFormatList(formatTag))
            {
                byte[] arr = AudioCompressionManager.FormatBytes(var.FormatHandle);
                string str = var.ToString();
                string format = ge.Replace(str, "_");
                //dict.Add();
                res += string.Format("private byte[] a{0} = new byte[] {{ {1} }};\n", format, GetArrayString(arr));

                //string fileName = path + @"\" + var + ".bin";
                //using (BinaryWriter bw = new BinaryWriter(File.Create(fileName)))
                //{
                //    byte[] arr = AudioCompressionManager.FormatBytes(var.FormatHandle);
                //    bw.Write(arr, 0, arr.Length);
                //}
            }
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dictaphonePanel1 = new AudioExCS.DictaphonePanel();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.convertPanel1 = new AudioExCS.ConvertPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.voxPanel1 = new AudioExCS.VoxPanel();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(244, 469);
            this.tabControl1.TabIndex = 26;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dictaphonePanel1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(236, 443);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "RecorderEx & PlayerEx";
            // 
            // dictaphonePanel1
            // 
            this.dictaphonePanel1.Location = new System.Drawing.Point(0, 0);
            this.dictaphonePanel1.Name = "dictaphonePanel1";
            this.dictaphonePanel1.Size = new System.Drawing.Size(228, 443);
            this.dictaphonePanel1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.convertPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(236, 443);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Conversion";
            // 
            // convertPanel1
            // 
            this.convertPanel1.Location = new System.Drawing.Point(8, 3);
            this.convertPanel1.Name = "convertPanel1";
            this.convertPanel1.Size = new System.Drawing.Size(227, 245);
            this.convertPanel1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.voxPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
//            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(236, 443);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Text = "Vox";
//            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // voxPanel1
            // 
            this.voxPanel1.Location = new System.Drawing.Point(6, 6);
            this.voxPanel1.Name = "voxPanel1";
            this.voxPanel1.Size = new System.Drawing.Size(227, 250);
            this.voxPanel1.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(244, 469);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Alvas.Audio";
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

	}
}
