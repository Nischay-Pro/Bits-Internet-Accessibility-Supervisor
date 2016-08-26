<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class settings
    Inherits MetroFramework.Forms.MetroForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(settings))
        Me.MetroTabControl1 = New MetroFramework.Controls.MetroTabControl()
        Me.MetroTabPage1 = New MetroFramework.Controls.MetroTabPage()
        Me.MetroButton3 = New MetroFramework.Controls.MetroButton()
        Me.MetroLabel2 = New MetroFramework.Controls.MetroLabel()
        Me.MetroRadioButton3 = New MetroFramework.Controls.MetroRadioButton()
        Me.MetroRadioButton2 = New MetroFramework.Controls.MetroRadioButton()
        Me.MetroRadioButton1 = New MetroFramework.Controls.MetroRadioButton()
        Me.MetroTextBox1 = New MetroFramework.Controls.MetroTextBox()
        Me.MetroLabel1 = New MetroFramework.Controls.MetroLabel()
        Me.MetroTabPage2 = New MetroFramework.Controls.MetroTabPage()
        Me.MetroCheckBox3 = New MetroFramework.Controls.MetroCheckBox()
        Me.MetroCheckBox2 = New MetroFramework.Controls.MetroCheckBox()
        Me.MetroCheckBox1 = New MetroFramework.Controls.MetroCheckBox()
        Me.MetroTabPage3 = New MetroFramework.Controls.MetroTabPage()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.MetroLabel4 = New MetroFramework.Controls.MetroLabel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.MetroLabel3 = New MetroFramework.Controls.MetroLabel()
        Me.MetroButton1 = New MetroFramework.Controls.MetroButton()
        Me.MetroButton2 = New MetroFramework.Controls.MetroButton()
        Me.MetroTabControl1.SuspendLayout()
        Me.MetroTabPage1.SuspendLayout()
        Me.MetroTabPage2.SuspendLayout()
        Me.MetroTabPage3.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MetroTabControl1
        '
        Me.MetroTabControl1.Controls.Add(Me.MetroTabPage1)
        Me.MetroTabControl1.Controls.Add(Me.MetroTabPage2)
        Me.MetroTabControl1.Controls.Add(Me.MetroTabPage3)
        Me.MetroTabControl1.Location = New System.Drawing.Point(23, 63)
        Me.MetroTabControl1.Name = "MetroTabControl1"
        Me.MetroTabControl1.SelectedIndex = 0
        Me.MetroTabControl1.Size = New System.Drawing.Size(436, 354)
        Me.MetroTabControl1.TabIndex = 0
        Me.MetroTabControl1.UseSelectable = True
        '
        'MetroTabPage1
        '
        Me.MetroTabPage1.Controls.Add(Me.MetroButton3)
        Me.MetroTabPage1.Controls.Add(Me.MetroLabel2)
        Me.MetroTabPage1.Controls.Add(Me.MetroRadioButton3)
        Me.MetroTabPage1.Controls.Add(Me.MetroRadioButton2)
        Me.MetroTabPage1.Controls.Add(Me.MetroRadioButton1)
        Me.MetroTabPage1.Controls.Add(Me.MetroTextBox1)
        Me.MetroTabPage1.Controls.Add(Me.MetroLabel1)
        Me.MetroTabPage1.HorizontalScrollbarBarColor = True
        Me.MetroTabPage1.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroTabPage1.HorizontalScrollbarSize = 10
        Me.MetroTabPage1.Location = New System.Drawing.Point(4, 38)
        Me.MetroTabPage1.Name = "MetroTabPage1"
        Me.MetroTabPage1.Size = New System.Drawing.Size(428, 312)
        Me.MetroTabPage1.TabIndex = 0
        Me.MetroTabPage1.Text = "Profile"
        Me.MetroTabPage1.VerticalScrollbarBarColor = True
        Me.MetroTabPage1.VerticalScrollbarHighlightOnWheel = False
        Me.MetroTabPage1.VerticalScrollbarSize = 10
        '
        'MetroButton3
        '
        Me.MetroButton3.Location = New System.Drawing.Point(106, 117)
        Me.MetroButton3.Name = "MetroButton3"
        Me.MetroButton3.Size = New System.Drawing.Size(177, 23)
        Me.MetroButton3.TabIndex = 7
        Me.MetroButton3.Text = "Revoke Cyberoam Profile"
        Me.MetroButton3.UseSelectable = True
        '
        'MetroLabel2
        '
        Me.MetroLabel2.AutoSize = True
        Me.MetroLabel2.FontSize = MetroFramework.MetroLabelSize.Small
        Me.MetroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Bold
        Me.MetroLabel2.Location = New System.Drawing.Point(20, 73)
        Me.MetroLabel2.Name = "MetroLabel2"
        Me.MetroLabel2.Size = New System.Drawing.Size(55, 15)
        Me.MetroLabel2.TabIndex = 16
        Me.MetroLabel2.Text = "Gender :"
        '
        'MetroRadioButton3
        '
        Me.MetroRadioButton3.AutoSize = True
        Me.MetroRadioButton3.Location = New System.Drawing.Point(258, 73)
        Me.MetroRadioButton3.Name = "MetroRadioButton3"
        Me.MetroRadioButton3.Size = New System.Drawing.Size(47, 15)
        Me.MetroRadioButton3.TabIndex = 15
        Me.MetroRadioButton3.Text = "Stud"
        Me.MetroRadioButton3.UseSelectable = True
        '
        'MetroRadioButton2
        '
        Me.MetroRadioButton2.AutoSize = True
        Me.MetroRadioButton2.Location = New System.Drawing.Point(182, 73)
        Me.MetroRadioButton2.Name = "MetroRadioButton2"
        Me.MetroRadioButton2.Size = New System.Drawing.Size(61, 15)
        Me.MetroRadioButton2.TabIndex = 14
        Me.MetroRadioButton2.Text = "Female"
        Me.MetroRadioButton2.UseSelectable = True
        '
        'MetroRadioButton1
        '
        Me.MetroRadioButton1.AutoSize = True
        Me.MetroRadioButton1.Location = New System.Drawing.Point(122, 73)
        Me.MetroRadioButton1.Name = "MetroRadioButton1"
        Me.MetroRadioButton1.Size = New System.Drawing.Size(49, 15)
        Me.MetroRadioButton1.TabIndex = 13
        Me.MetroRadioButton1.Text = "Male"
        Me.MetroRadioButton1.UseSelectable = True
        '
        'MetroTextBox1
        '
        '
        '
        '
        Me.MetroTextBox1.CustomButton.Image = Nothing
        Me.MetroTextBox1.CustomButton.Location = New System.Drawing.Point(173, 1)
        Me.MetroTextBox1.CustomButton.Name = ""
        Me.MetroTextBox1.CustomButton.Size = New System.Drawing.Size(21, 21)
        Me.MetroTextBox1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.MetroTextBox1.CustomButton.TabIndex = 1
        Me.MetroTextBox1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.MetroTextBox1.CustomButton.UseSelectable = True
        Me.MetroTextBox1.CustomButton.Visible = False
        Me.MetroTextBox1.Lines = New String(-1) {}
        Me.MetroTextBox1.Location = New System.Drawing.Point(122, 25)
        Me.MetroTextBox1.MaxLength = 32767
        Me.MetroTextBox1.Name = "MetroTextBox1"
        Me.MetroTextBox1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.MetroTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.MetroTextBox1.SelectedText = ""
        Me.MetroTextBox1.SelectionLength = 0
        Me.MetroTextBox1.SelectionStart = 0
        Me.MetroTextBox1.ShortcutsEnabled = True
        Me.MetroTextBox1.Size = New System.Drawing.Size(195, 23)
        Me.MetroTextBox1.TabIndex = 12
        Me.MetroTextBox1.UseSelectable = True
        Me.MetroTextBox1.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroTextBox1.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'MetroLabel1
        '
        Me.MetroLabel1.AutoSize = True
        Me.MetroLabel1.FontSize = MetroFramework.MetroLabelSize.Small
        Me.MetroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Bold
        Me.MetroLabel1.Location = New System.Drawing.Point(20, 31)
        Me.MetroLabel1.Name = "MetroLabel1"
        Me.MetroLabel1.Size = New System.Drawing.Size(86, 15)
        Me.MetroLabel1.TabIndex = 11
        Me.MetroLabel1.Text = "Profile Name :"
        '
        'MetroTabPage2
        '
        Me.MetroTabPage2.Controls.Add(Me.MetroCheckBox3)
        Me.MetroTabPage2.Controls.Add(Me.MetroCheckBox2)
        Me.MetroTabPage2.Controls.Add(Me.MetroCheckBox1)
        Me.MetroTabPage2.HorizontalScrollbarBarColor = True
        Me.MetroTabPage2.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroTabPage2.HorizontalScrollbarSize = 10
        Me.MetroTabPage2.Location = New System.Drawing.Point(4, 38)
        Me.MetroTabPage2.Name = "MetroTabPage2"
        Me.MetroTabPage2.Size = New System.Drawing.Size(428, 312)
        Me.MetroTabPage2.TabIndex = 1
        Me.MetroTabPage2.Text = "Application Control"
        Me.MetroTabPage2.VerticalScrollbarBarColor = True
        Me.MetroTabPage2.VerticalScrollbarHighlightOnWheel = False
        Me.MetroTabPage2.VerticalScrollbarSize = 10
        '
        'MetroCheckBox3
        '
        Me.MetroCheckBox3.AutoSize = True
        Me.MetroCheckBox3.Location = New System.Drawing.Point(19, 100)
        Me.MetroCheckBox3.Name = "MetroCheckBox3"
        Me.MetroCheckBox3.Size = New System.Drawing.Size(224, 15)
        Me.MetroCheckBox3.TabIndex = 4
        Me.MetroCheckBox3.Text = "Enable Automatic Network Speed Test"
        Me.MetroCheckBox3.UseSelectable = True
        '
        'MetroCheckBox2
        '
        Me.MetroCheckBox2.AutoSize = True
        Me.MetroCheckBox2.Location = New System.Drawing.Point(19, 46)
        Me.MetroCheckBox2.Name = "MetroCheckBox2"
        Me.MetroCheckBox2.Size = New System.Drawing.Size(127, 15)
        Me.MetroCheckBox2.TabIndex = 3
        Me.MetroCheckBox2.Text = "Start in Background"
        Me.MetroCheckBox2.UseSelectable = True
        '
        'MetroCheckBox1
        '
        Me.MetroCheckBox1.AutoSize = True
        Me.MetroCheckBox1.Location = New System.Drawing.Point(19, 25)
        Me.MetroCheckBox1.Name = "MetroCheckBox1"
        Me.MetroCheckBox1.Size = New System.Drawing.Size(125, 15)
        Me.MetroCheckBox1.TabIndex = 2
        Me.MetroCheckBox1.Text = "Start with Windows"
        Me.MetroCheckBox1.UseSelectable = True
        '
        'MetroTabPage3
        '
        Me.MetroTabPage3.Controls.Add(Me.Label3)
        Me.MetroTabPage3.Controls.Add(Me.MetroLabel4)
        Me.MetroTabPage3.Controls.Add(Me.PictureBox1)
        Me.MetroTabPage3.Controls.Add(Me.MetroLabel3)
        Me.MetroTabPage3.HorizontalScrollbarBarColor = True
        Me.MetroTabPage3.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroTabPage3.HorizontalScrollbarSize = 10
        Me.MetroTabPage3.Location = New System.Drawing.Point(4, 38)
        Me.MetroTabPage3.Name = "MetroTabPage3"
        Me.MetroTabPage3.Size = New System.Drawing.Size(428, 312)
        Me.MetroTabPage3.TabIndex = 2
        Me.MetroTabPage3.Text = "About"
        Me.MetroTabPage3.VerticalScrollbarBarColor = True
        Me.MetroTabPage3.VerticalScrollbarHighlightOnWheel = False
        Me.MetroTabPage3.VerticalScrollbarSize = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Malgun Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.Label3.Location = New System.Drawing.Point(39, 232)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(158, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Built with 💖 by Nischay Pro"
        '
        'MetroLabel4
        '
        Me.MetroLabel4.AutoSize = True
        Me.MetroLabel4.FontSize = MetroFramework.MetroLabelSize.Small
        Me.MetroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.MetroLabel4.Location = New System.Drawing.Point(128, 163)
        Me.MetroLabel4.Name = "MetroLabel4"
        Me.MetroLabel4.Size = New System.Drawing.Size(256, 30)
        Me.MetroLabel4.TabIndex = 14
        Me.MetroLabel4.Text = "Click on OctoCat is view this project on Github " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(and contribute too)"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = Global.Bits_Internet_Accessibility_Supervisor.My.Resources.Resources.github
        Me.PictureBox1.Location = New System.Drawing.Point(42, 146)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(64, 64)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 13
        Me.PictureBox1.TabStop = False
        '
        'MetroLabel3
        '
        Me.MetroLabel3.AutoSize = True
        Me.MetroLabel3.FontSize = MetroFramework.MetroLabelSize.Small
        Me.MetroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.MetroLabel3.Location = New System.Drawing.Point(14, 16)
        Me.MetroLabel3.Name = "MetroLabel3"
        Me.MetroLabel3.Size = New System.Drawing.Size(384, 75)
        Me.MetroLabel3.TabIndex = 12
        Me.MetroLabel3.Text = "Bits Internet Accessibility Supervisor is an OpenSource project aimed to " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "simpli" &
    "fy the login process of Cyberroam Clients in Bits Hyderabad" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Campus." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Created " &
    "using .net Technologies."
        '
        'MetroButton1
        '
        Me.MetroButton1.Enabled = False
        Me.MetroButton1.Location = New System.Drawing.Point(238, 423)
        Me.MetroButton1.Name = "MetroButton1"
        Me.MetroButton1.Size = New System.Drawing.Size(110, 23)
        Me.MetroButton1.TabIndex = 5
        Me.MetroButton1.Text = "Apply"
        Me.MetroButton1.UseSelectable = True
        '
        'MetroButton2
        '
        Me.MetroButton2.Location = New System.Drawing.Point(354, 423)
        Me.MetroButton2.Name = "MetroButton2"
        Me.MetroButton2.Size = New System.Drawing.Size(110, 23)
        Me.MetroButton2.TabIndex = 6
        Me.MetroButton2.Text = "Close"
        Me.MetroButton2.UseSelectable = True
        '
        'settings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(482, 460)
        Me.Controls.Add(Me.MetroButton2)
        Me.Controls.Add(Me.MetroButton1)
        Me.Controls.Add(Me.MetroTabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "settings"
        Me.Resizable = False
        Me.Text = "Settings"
        Me.TopMost = True
        Me.MetroTabControl1.ResumeLayout(False)
        Me.MetroTabPage1.ResumeLayout(False)
        Me.MetroTabPage1.PerformLayout()
        Me.MetroTabPage2.ResumeLayout(False)
        Me.MetroTabPage2.PerformLayout()
        Me.MetroTabPage3.ResumeLayout(False)
        Me.MetroTabPage3.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents MetroTabControl1 As MetroFramework.Controls.MetroTabControl
    Friend WithEvents MetroTabPage1 As MetroFramework.Controls.MetroTabPage
    Friend WithEvents MetroTabPage2 As MetroFramework.Controls.MetroTabPage
    Friend WithEvents MetroButton1 As MetroFramework.Controls.MetroButton
    Friend WithEvents MetroButton2 As MetroFramework.Controls.MetroButton
    Friend WithEvents MetroCheckBox2 As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents MetroCheckBox1 As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents MetroLabel2 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroRadioButton3 As MetroFramework.Controls.MetroRadioButton
    Friend WithEvents MetroRadioButton2 As MetroFramework.Controls.MetroRadioButton
    Friend WithEvents MetroRadioButton1 As MetroFramework.Controls.MetroRadioButton
    Friend WithEvents MetroTextBox1 As MetroFramework.Controls.MetroTextBox
    Friend WithEvents MetroLabel1 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroTabPage3 As MetroFramework.Controls.MetroTabPage
    Friend WithEvents MetroLabel3 As MetroFramework.Controls.MetroLabel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents MetroLabel4 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Label3 As Label
    Friend WithEvents MetroButton3 As MetroFramework.Controls.MetroButton
    Friend WithEvents MetroCheckBox3 As MetroFramework.Controls.MetroCheckBox
End Class
