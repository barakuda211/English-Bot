namespace SoxConvertCs
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
            this.ofdSoxPath = new System.Windows.Forms.OpenFileDialog();
            this.tbSoxPath = new System.Windows.Forms.TextBox();
            this.btnSoxPath = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOutput = new System.Windows.Forms.Button();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnInput = new System.Windows.Forms.Button();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ofdInput = new System.Windows.Forms.OpenFileDialog();
            this.sdOutput = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbSoxPath
            // 
            this.tbSoxPath.Location = new System.Drawing.Point(11, 43);
            this.tbSoxPath.Name = "tbSoxPath";
            this.tbSoxPath.Size = new System.Drawing.Size(367, 20);
            this.tbSoxPath.TabIndex = 0;
            // 
            // btnSoxPath
            // 
            this.btnSoxPath.Location = new System.Drawing.Point(384, 40);
            this.btnSoxPath.Name = "btnSoxPath";
            this.btnSoxPath.Size = new System.Drawing.Size(37, 23);
            this.btnSoxPath.TabIndex = 1;
            this.btnSoxPath.Text = "...";
            this.btnSoxPath.UseVisualStyleBackColor = true;
            this.btnSoxPath.Click += new System.EventHandler(this.btnSoxPath_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnOutput);
            this.groupBox1.Controls.Add(this.tbOutput);
            this.groupBox1.Controls.Add(this.cbType);
            this.groupBox1.Controls.Add(this.btnConvert);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnInput);
            this.groupBox1.Controls.Add(this.tbInput);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnSoxPath);
            this.groupBox1.Controls.Add(this.tbSoxPath);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 241);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sox Convert";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Output Audio File";
            // 
            // btnOutput
            // 
            this.btnOutput.Location = new System.Drawing.Point(384, 165);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(37, 23);
            this.btnOutput.TabIndex = 9;
            this.btnOutput.Text = "...";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // tbOutput
            // 
            this.tbOutput.Location = new System.Drawing.Point(11, 168);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Size = new System.Drawing.Size(367, 20);
            this.tbOutput.TabIndex = 8;
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(11, 122);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(367, 21);
            this.cbType.TabIndex = 7;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(11, 204);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(410, 23);
            this.btnConvert.TabIndex = 6;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Input Audio File";
            // 
            // btnInput
            // 
            this.btnInput.Location = new System.Drawing.Point(384, 82);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(37, 23);
            this.btnInput.TabIndex = 4;
            this.btnInput.Text = "...";
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // tbInput
            // 
            this.tbInput.Location = new System.Drawing.Point(11, 85);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(367, 20);
            this.tbInput.TabIndex = 3;
            this.tbInput.TextChanged += new System.EventHandler(this.tbInput_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sox Path";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 268);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "Sox Convert";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdSoxPath;
        private System.Windows.Forms.TextBox tbSoxPath;
        private System.Windows.Forms.Button btnSoxPath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnInput;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.OpenFileDialog ofdInput;
        private System.Windows.Forms.SaveFileDialog sdOutput;
    }
}

