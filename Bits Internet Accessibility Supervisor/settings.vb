Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class settings
    Private Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles MetroButton2.Click
        Me.Close()
    End Sub

    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        Dim ini As New IniFile
        ini.Load(My.Application.Info.DirectoryPath & "\config.ini")
        ini.AddSection("Profile")
        ini.AddSection("Settings")
        ini.SetKeyValue("Profile", "Profile Name", MetroTextBox1.Text)
        If MetroRadioButton1.Checked = True Then
            ini.SetKeyValue("Profile", "Gender", "Male")
        End If
        If MetroRadioButton2.Checked = True Then
            ini.SetKeyValue("Profile", "Gender", "Female")
        End If
        If MetroRadioButton3.Checked = True Then
            ini.SetKeyValue("Profile", "Gender", "Stud")
        End If
        If MetroCheckBox1.Checked = True Then
            ini.SetKeyValue("Settings", "Startup", "True")
        Else
            ini.SetKeyValue("Settings", "Startup", "False")
        End If
        If MetroCheckBox2.Checked = True Then
            ini.SetKeyValue("Settings", "Ninja", "True")
        Else
            ini.SetKeyValue("Settings", "Ninja", "False")
        End If
        If MetroCheckBox3.Checked = True Then
            ini.SetKeyValue("Settings", "Automatic", "True")
        Else
            ini.SetKeyValue("Settings", "Automatic", "False")
        End If
        If MetroCheckBox5.Checked = True Then
            ini.SetKeyValue("Settings", "LogoutClose", "True")
        Else
            ini.SetKeyValue("Settings", "LogoutClose", "False")
        End If

        If MetroCheckBox4.Checked = True Then
            ini.SetKeyValue("Settings", "Logs", "True")
            Try
                My.Computer.FileSystem.CreateDirectory(My.Application.Info.DirectoryPath & "\logs")
            Catch ex As Exception
            End Try
        Else
            ini.SetKeyValue("Settings", "Logs", "False")
            Try
                My.Computer.FileSystem.DeleteDirectory(My.Application.Info.DirectoryPath & "\logs", FileIO.DeleteDirectoryOption.DeleteAllContents)
            Catch ex As Exception
            End Try
        End If
        ini.Save(My.Application.Info.DirectoryPath & "\config.ini")
        MetroButton1.Enabled = False
        MetroButton2.Enabled = True
        MetroTabControl1.Focus()
        GenerateNotification2("Settings Saved Successfully.", EventType.Information, 3000)
    End Sub
    Private Sub ChangeDetect()
        MetroButton1.Enabled = True
    End Sub
    Private Sub settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim loadsets As New Threading.Thread(AddressOf LoadAccountsMan)
        loadsets.IsBackground = True
        loadsets.SetApartmentState(Threading.ApartmentState.STA)
        loadsets.Start()
        For Each TabControl As MetroFramework.Controls.MetroTabPage In Me.MetroTabControl1.Controls.OfType(Of MetroFramework.Controls.MetroTabPage)
            For Each Control As MetroFramework.Controls.MetroRadioButton In TabControl.Controls.OfType(Of MetroFramework.Controls.MetroRadioButton)
                AddHandler Control.CheckedChanged, AddressOf ChangeDetect
            Next
            For Each Control As MetroFramework.Controls.MetroTextBox In TabControl.Controls.OfType(Of MetroFramework.Controls.MetroTextBox)
                AddHandler Control.TextChanged, AddressOf ChangeDetect
            Next
            For Each Control As MetroFramework.Controls.MetroCheckBox In TabControl.Controls.OfType(Of MetroFramework.Controls.MetroCheckBox)
                AddHandler Control.CheckedChanged, AddressOf ChangeDetect
            Next
        Next
        Dim ini As New IniFile
        ini.Load(My.Application.Info.DirectoryPath & "\config.ini")
        MetroTextBox1.Text = ini.GetKeyValue("Profile", "Profile Name")
        Dim gender As String = ini.GetKeyValue("Profile", "Gender")
        If gender = "Male" Then
            MetroRadioButton1.Checked = True
        End If
        If gender = "Female" Then
            MetroRadioButton2.Checked = True
        End If
        If gender = "Stud" Then
            MetroRadioButton3.Checked = True
        End If
        If ini.GetKeyValue("Settings", "Startup") = "True" Then
            MetroCheckBox1.Checked = True
            If ini.GetKeyValue("Settings", "Ninja") = "True" Then
                MetroCheckBox2.Checked = True
            Else
                MetroCheckBox2.Checked = False
            End If
        Else
            MetroCheckBox1.Checked = False
            MetroCheckBox2.Enabled = False
        End If
        If ini.GetKeyValue("Settings", "Automatic") = "True" Then
            MetroCheckBox3.Checked = True
        End If
        If ini.GetKeyValue("Settings", "Logs") = "True" Then
            MetroCheckBox4.Checked = True
        End If
        If ini.GetKeyValue("Settings", "LogoutClose") = "True" Then
            MetroCheckBox5.Checked = True
        End If
        MetroLabel5.Text = "Primary Account : " & ini.GetKeyValue("Authentication", "Username").ToLower
        ini.Save(My.Application.Info.DirectoryPath & "\config.ini")
        MetroButton1.Enabled = False
        Label3.Text += " | Build " & My.Application.Info.Version.ToString
    End Sub

    Private Sub MetroCheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles MetroCheckBox1.CheckedChanged
        If MetroCheckBox1.Checked = True Then
            MetroCheckBox2.Enabled = True
            RegKeyAdd(False)
        Else
            RegKeyDelete()
            MetroCheckBox2.Checked = False
            MetroCheckBox2.Enabled = False
        End If
    End Sub
    Private Sub RegKeyAdd(ByVal Hidden As Boolean)
        Try
            Dim processman As New Process
            Dim pathman As String = Path.GetPathRoot(Environment.SystemDirectory)
            processman.StartInfo = New ProcessStartInfo()
            processman.StartInfo.FileName = "cmd"
            processman.StartInfo.RedirectStandardInput = True
            processman.StartInfo.RedirectStandardOutput = True
            processman.StartInfo.RedirectStandardError = True
            processman.StartInfo.CreateNoWindow = True
            processman.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            processman.StartInfo.UseShellExecute = False
            processman.Start()
            Dim writecommand As StreamWriter = processman.StandardInput
            Dim appname As String = Application.ProductName
            Dim apploc As String = Application.ExecutablePath
            If Hidden = True Then
                apploc += " -hidden"
            End If
            writecommand.WriteLine("reg add HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run" & " /V """ & appname & """" & " /t REG_SZ /F /D """ & apploc & """")
            writecommand.Close()
        Catch ex As Exception
            GenerateNotification2("Unable to register entry", EventType.Critical, 5000)
        End Try
    End Sub
    Private Sub RegKeyDelete()
        Try
            Dim processman As New Process
            Dim pathman As String = Path.GetPathRoot(Environment.SystemDirectory)
            processman.StartInfo = New ProcessStartInfo()
            processman.StartInfo.FileName = "cmd"
            processman.StartInfo.RedirectStandardInput = True
            processman.StartInfo.RedirectStandardOutput = True
            processman.StartInfo.RedirectStandardError = True
            processman.StartInfo.CreateNoWindow = True
            processman.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            processman.StartInfo.UseShellExecute = False
            processman.Start()
            Dim writecommand As StreamWriter = processman.StandardInput
            Dim appname As String = Application.ProductName
            Dim apploc As String = Application.ExecutablePath
            writecommand.WriteLine("reg delete HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run" & " /V """ & appname & """" & " /F")
            writecommand.Close()
            processman.WaitForExit()
        Catch ex As Exception
            GenerateNotification2("Unable to deregister entry", EventType.Critical, 5000)
        End Try
    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Process.Start("https://github.com/Nischay-Pro/Bits-Internet-Accessibility-Supervisor")
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Process.Start("https://www.facebook.com/nischay.pro")
    End Sub

    Private Sub MetroButton3_Click(sender As Object, e As EventArgs) Handles MetroButton3.Click
        If MessageBox.Show("Are you sure you want to reset your Settings?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Kill(My.Application.Info.DirectoryPath & "\config.ini")
            Kill(My.Application.Info.DirectoryPath & "\accounts.json")
            Process.Start(Application.ExecutablePath)
            End
        End If
    End Sub

    Private Sub MetroCheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles MetroCheckBox2.CheckedChanged
        If MetroCheckBox2.Checked = True Then
            RegKeyDelete()
            RegKeyAdd(True)
        Else
            RegKeyDelete()
            RegKeyAdd(False)
        End If
    End Sub

    Public Sub LoadAccountsMan()
        SetControl(ListView1, False)
        SetControl(MetroButton4, False)
        SetControl(MetroButton5, False)
        Dim data As String = "{" & """accounts""" & ":[]}"
        If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\accounts.json") Then
            data = My.Computer.FileSystem.ReadAllText(My.Application.Info.DirectoryPath & "\accounts.json")
        Else
            My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\accounts.json", data, False)
        End If
        Dim parsed As JObject = JObject.Parse(data)
        For Each Item As JObject In parsed("accounts").ToArray
            AddListItem(Item("username"), Item("status"), ListView1)
        Next
        SetControl(ListView1, True)
        SetControl(MetroButton4, True)
        If ListView1.Items.Count = 0 Then
            SetControl(MetroButton5, False)
        Else
            SetControl(MetroButton5, True)
        End If
    End Sub

    Public Sub AddData(ObjData As JObject)
        ClearList(ListView1)
        Dim data As String = "{" & """accounts""" & ":[]}"
        If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\accounts.json") Then
            data = My.Computer.FileSystem.ReadAllText(My.Application.Info.DirectoryPath & "\accounts.json")
        Else
            My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\accounts.json", data, False)
        End If
        Dim parsed As JObject = JObject.Parse(data)
        Dim JArrayman As JArray = parsed("accounts")
        JArrayman.Add(ObjData)
        parsed("accounts") = JArrayman
        My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\accounts.json", parsed.ToString, False)
    End Sub
    Public Sub RemoveData(RemoveObj As String)
        ClearList(ListView1)
        Dim data As String = "{" & """accounts""" & ":[]}"
        If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\accounts.json") Then
            data = My.Computer.FileSystem.ReadAllText(My.Application.Info.DirectoryPath & "\accounts.json")
        Else
            My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\accounts.json", data, False)
        End If
        Dim parsed As JObject = JObject.Parse(data)
        Dim JArrayman As JArray = parsed("accounts")
        Dim i As Integer
        Do Until i = JArrayman.Count - 1
            If JArrayman(i)("username") = RemoveObj Then
                JArrayman(i).Remove()
                Exit Do
            End If
            i += 1
        Loop
        'JArrayman.Add(ObjData)
        parsed("accounts") = JArrayman
        My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\accounts.json", parsed.ToString, False)
    End Sub

    Private Sub MetroButton4_Click(sender As Object, e As EventArgs) Handles MetroButton4.Click
        addacct.Show()
    End Sub

    Private Sub MetroButton5_Click(sender As Object, e As EventArgs) Handles MetroButton5.Click
        If ListView1.SelectedItems.Count <> 0 Then
            If ListView1.Items.Count > 1 Then
                RemoveData(ListView1.SelectedItems(0).Text)
                Dim loadsets As New Threading.Thread(AddressOf LoadAccountsMan)
                loadsets.IsBackground = True
                loadsets.SetApartmentState(Threading.ApartmentState.STA)
                loadsets.Start()
            Else
                Kill(My.Application.Info.DirectoryPath & "\accounts.json")
                ListView1.Items.Clear()
                MetroButton5.Enabled = False
            End If
        End If
    End Sub
End Class