using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Alvas.Audio;

namespace RecordPlay
{
    public partial class FormatDialog : Form
    {
        public FormatDialog(bool isMp3)
        {
            InitializeComponent();
            _isMp3 = isMp3;
        }

        private bool _isMp3;

        private IntPtr format;
        public IntPtr Format
        {
            get { return format; }
        }

        private void EnumFormatTags()
        {
            FormatTagDetails[] fdArr = AudioCompressionManager.GetFormatTagList();
            if (!_isMp3)
            {
                cbFormatTag.DataSource = fdArr;
            }
            else
            {
                cbFormatTag.Enabled = false;
                for (int i = 0; i < fdArr.Length; i++)
                {
                    FormatTagDetails ftd = fdArr[i];
                    if (ftd.FormatTag == AudioCompressionManager.MpegLayer3FormatTag)
                    {
                        cbFormatTag.DataSource = fdArr;
                        cbFormatTag.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void EnumFormats()
        {
            int formatTag = ((FormatTagDetails)cbFormatTag.SelectedValue).FormatTag;
            cbFormat.DataSource = AudioCompressionManager.GetFormatList(formatTag);
        }

        private void OpenNewDialog_Load(object sender, EventArgs e)
        {
            EnumFormatTags();
        }

        private void cbFormatTag_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnumFormats();
        }

        private void cbFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormatDetails pafd = (FormatDetails)cbFormat.SelectedItem;
            format = pafd.FormatHandle;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = //(string.IsNullOrEmpty(fileName) |
                (format == IntPtr.Zero) ? DialogResult.None : DialogResult.OK;
        }

    }
}