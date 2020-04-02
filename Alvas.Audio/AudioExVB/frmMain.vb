Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.IO
Imports Alvas.Audio

Namespace AudioExCS
	''' <summary>
	''' Summary description for Form1.
	''' </summary>
	Friend Class frmMain
		Inherits System.Windows.Forms.Form
		Private dictaphonePanel1 As AudioExCS.DictaphonePanel
		Private convertPanel1 As AudioExCS.ConvertPanel
		Private tabControl1 As System.Windows.Forms.TabControl
		Private tabPage3 As System.Windows.Forms.TabPage
		Private tabPage2 As TabPage
		Private voxPanel1 As VoxPanel
		Private tabPage1 As TabPage
		Private components As System.ComponentModel.IContainer

		''' <summary>
		''' The main entry point for the application.
		''' </summary>
        <STAThread()> _
        Public Shared Sub Main()
            Application.Run(New frmMain)
        End Sub

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()
        End Sub

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
            Me.tabControl1 = New System.Windows.Forms.TabControl
            Me.tabPage3 = New System.Windows.Forms.TabPage
            Me.dictaphonePanel1 = New AudioExCS.DictaphonePanel
            Me.tabPage1 = New System.Windows.Forms.TabPage
            Me.convertPanel1 = New AudioExCS.ConvertPanel
            Me.tabPage2 = New System.Windows.Forms.TabPage
            Me.voxPanel1 = New AudioExCS.VoxPanel
            Me.tabControl1.SuspendLayout()
            Me.tabPage3.SuspendLayout()
            Me.tabPage1.SuspendLayout()
            Me.tabPage2.SuspendLayout()
            Me.SuspendLayout()
            ' 
            ' tabControl1
            ' 
            Me.tabControl1.Controls.Add(Me.tabPage3)
            Me.tabControl1.Controls.Add(Me.tabPage1)
            Me.tabControl1.Controls.Add(Me.tabPage2)
            Me.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tabControl1.Location = New System.Drawing.Point(0, 0)
            Me.tabControl1.Name = "tabControl1"
            Me.tabControl1.SelectedIndex = 0
            Me.tabControl1.Size = New System.Drawing.Size(244, 469)
            Me.tabControl1.TabIndex = 26
            ' 
            ' tabPage3
            ' 
            Me.tabPage3.Controls.Add(Me.dictaphonePanel1)
            Me.tabPage3.Location = New System.Drawing.Point(4, 22)
            Me.tabPage3.Name = "tabPage3"
            Me.tabPage3.Size = New System.Drawing.Size(236, 443)
            Me.tabPage3.TabIndex = 2
            Me.tabPage3.Text = "RecorderEx & PlayerEx"
            ' 
            ' dictaphonePanel1
            ' 
            Me.dictaphonePanel1.Location = New System.Drawing.Point(0, 0)
            Me.dictaphonePanel1.Name = "dictaphonePanel1"
            Me.dictaphonePanel1.Size = New System.Drawing.Size(228, 443)
            Me.dictaphonePanel1.TabIndex = 0
            ' 
            ' tabPage1
            ' 
            Me.tabPage1.Controls.Add(Me.convertPanel1)
            Me.tabPage1.Location = New System.Drawing.Point(4, 22)
            Me.tabPage1.Name = "tabPage1"
            Me.tabPage1.Size = New System.Drawing.Size(236, 443)
            Me.tabPage1.TabIndex = 3
            Me.tabPage1.Text = "Conversion"
            ' 
            ' convertPanel1
            ' 
            Me.convertPanel1.Location = New System.Drawing.Point(8, 3)
            Me.convertPanel1.Name = "convertPanel1"
            Me.convertPanel1.Size = New System.Drawing.Size(227, 245)
            Me.convertPanel1.TabIndex = 0
            ' 
            ' tabPage2
            ' 
            Me.tabPage2.Controls.Add(Me.voxPanel1)
            Me.tabPage2.Location = New System.Drawing.Point(4, 22)
            Me.tabPage2.Name = "tabPage2"
            '            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            Me.tabPage2.Size = New System.Drawing.Size(236, 443)
            Me.tabPage2.TabIndex = 4
            Me.tabPage2.Text = "Vox"
            '            this.tabPage2.UseVisualStyleBackColor = true;
            ' 
            ' voxPanel1
            ' 
            Me.voxPanel1.Location = New System.Drawing.Point(6, 6)
            Me.voxPanel1.Name = "voxPanel1"
            Me.voxPanel1.Size = New System.Drawing.Size(227, 250)
            Me.voxPanel1.TabIndex = 0
            ' 
            ' frmMain
            ' 
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(244, 469)
            Me.Controls.Add(Me.tabControl1)
            Me.Icon = DirectCast((resources.GetObject("$this.Icon")), System.Drawing.Icon)
            Me.Name = "frmMain"
            Me.Text = "Alvas.Audio"
            Me.tabControl1.ResumeLayout(False)
            Me.tabPage3.ResumeLayout(False)
            Me.tabPage1.ResumeLayout(False)
            Me.tabPage2.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
#End Region

    End Class
End Namespace
