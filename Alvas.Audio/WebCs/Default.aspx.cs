using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using Alvas.Audio;

public partial class _Default : System.Web.UI.Page 
{
    private const string AudioDir = @"Audio\";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillFileList();
        }
    }

    private void FillFileList()
    {
        lbFiles.Items.Clear();
        string dir = Server.MapPath(AudioDir);
        foreach (string file in Directory.GetFiles(dir))
        {
            lbFiles.Items.Add(Path.GetFileName(file));
        }
    }

    private void BindFormats(object dataSource)
    {
        lbFormats.DataSource = dataSource;
        lbFormats.DataBind();
    }

    private void FillFormatList(string pathRelative)
    {
        string fileName = Server.MapPath(pathRelative);
        IAudioReader wr = null;
        string ext = Path.GetExtension(fileName).ToLower();
        if (ext != ".wav")
        {
            wr = new DsReader(fileName);
            if (!((DsReader)wr).HasAudio)
            {
                ErrorText(pathRelative);
                return;
            }
            //if (ext == ".mp3")
            //{ 
            //    wr = new Mp3Reader(File.OpenRead(fileName));
            //}
            //else
            //{
            //}
        }
        else
        {
            wr = new WaveReader(File.OpenRead(fileName));
        }
        IntPtr format;
        try
        {
            //using (WaveReader wr = new WaveReader(File.OpenRead(fileName)))
            {
                format = wr.ReadFormat();
                wr.Close();
            }
        }
        catch
        {
            ErrorText(pathRelative);
            return;
        }
        FormatDetails fd = AudioCompressionManager.GetFormatDetails(format);
        fd.ShowFormatTag = true;
        // Set label's text
        lblInfo.Text = 
            //"Filename: " + Path.GetFileName(fileName) + "<br>" +
            "Format: " + fd.ToString();
        lblInfo.Visible = true;
        //FormatTagDetails[] ftdArr = AudioCompressionManager.GetFormatTagList();
        FormatDetails[] fdArr = AudioCompressionManager.GetCompatibleFormatList(format);
        for (int i = 0; i < fdArr.Length; i++)
        {
            fdArr[i].ShowFormatTag = true;
        }
        BindFormats(fdArr);
    }

    private void ErrorText(string pathRelative)
    {
        lblInfo.Text = string.Format("<font color=red>Oops! Please <a href='mailto:mail@alvas.net?Subject={0}'>contact us</a>. We will try to quickly resolve the problem.</font>", pathRelative);
        //"Filename: " + Path.GetFileName(fileName) + "<br>" +
        //"Is invalid";
        lblInfo.Visible = true;
        BindFormats(new string[0]);
    }

    private void Convert()
    {
        string fileName = Server.MapPath(AudioDir + lbFiles.SelectedValue);
        try
        {
            IAudioReader wr = null;
            if (Path.GetExtension(fileName).ToLower() != ".wav")
            {
                wr = new DsReader(fileName);
            }
            else
            {
                wr = new WaveReader(File.OpenRead(fileName));
            }
            //FileStream ms = File.OpenRead(fileName);
            //WaveReader wr = new WaveReader(ms);

            IntPtr format = wr.ReadFormat();
            int ndx = lbFormats.SelectedIndex;
            IntPtr formatPcm = AudioCompressionManager.GetCompatibleFormatList(
                format)[ndx].FormatHandle;
            byte[] dataPcm = AudioCompressionManager.Convert(format, formatPcm, wr.ReadData(), false);
            WaveWriter ww = new WaveWriter(File.Create(fileName + ".wav"),
                AudioCompressionManager.FormatBytes(formatPcm));
            ww.WriteData(dataPcm);
            ww.Close();
            wr.Close();
            lblResult.Text = string.Format("The '{0}' file is converted to '{0}.wav' file successfully.", lbFiles.SelectedValue);
        }
        catch 
        {
            lblResult.Text = "Converting error.";
        }
        FillFileList();
    }

    private void WriteToFile(string strPath, ref byte[] Buffer)
    {
        FileStream newFile = new FileStream(strPath, FileMode.Create);
        newFile.Write(Buffer, 0, Buffer.Length);
        newFile.Close();
    }

    protected void btnConvert_Click(object sender, EventArgs e)
    {
        Convert();
    }

    protected void lbFiles_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnConvert.Enabled = false;
        if (lbFiles.SelectedIndex > -1)
        {
            string pathRelative = AudioDir + lbFiles.SelectedItem.Text;
            HyperLink1.NavigateUrl = pathRelative;
            HyperLink1.Text = "Filename: " + lbFiles.SelectedItem.Text;
            FillFormatList(pathRelative);
        }
    }

    protected void cmdSend_Click(object sender, EventArgs e)
    {
        if (filMyFile.PostedFile != null)
        {
            HttpPostedFile myFile = filMyFile.PostedFile;

            int nFileLen = myFile.ContentLength;

            if (nFileLen > 0)
            {
                byte[] myData = new byte[nFileLen];
                myFile.InputStream.Read(myData, 0, nFileLen);
                string strFilename = Path.GetFileName(myFile.FileName);

                string fileName = Server.MapPath(AudioDir + strFilename);
                WriteToFile(fileName, ref myData);
                FillFileList();
            }
        }
    }

    protected void lbFormats_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnConvert.Enabled = lbFormats.SelectedIndex > -1;
    }
}
