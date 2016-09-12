Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text

Public Class Form1
    Public FormData As MetroFramework.Forms.MetroForm
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Log("Normal GUI Mode Loaded")
        Dim ini As New IniFile
        Log("Loading Configuration")
        ini.Load(My.Application.Info.DirectoryPath & "\config.ini")
        Dim welcomeme As String = Nothing
        If ini.GetKeyValue("Authentication", "Username") = "f2015606" Then
            Log("Greetings My Lord")
            welcomeme = "My Lord"
        Else
            welcomeme = ini.GetKeyValue("Authentication", "Username")
        End If
        Log("Reset Version")
        ini.SetKeyValue("Settings", "Version", My.Application.Info.Version.ToString)
        ini.Save(My.Application.Info.DirectoryPath & "\config.ini")
        Label1.Text = "Welcome " & welcomeme
        LoadSettings(True)
        Log("Running Network Speed Test Thread")
        Dim speedrunner As New Threading.Thread(Sub() RunNetworkSpeed(True))
        speedrunner.IsBackground = True
        speedrunner.SetApartmentState(Threading.ApartmentState.STA)
        speedrunner.Start()
        Log("Running Check Updates Thread")
        Dim checkupdates As New Threading.Thread(AddressOf UpdateCheck)
        checkupdates.IsBackground = True
        checkupdates.SetApartmentState(Threading.ApartmentState.STA)
        checkupdates.Start()
        Log("Running Network Access Monitor Thread")
        Dim recheckaccess1 As New Threading.Thread(AddressOf RecheckAccess)
        recheckaccess1.IsBackground = True
        recheckaccess1.SetApartmentState(Threading.ApartmentState.STA)
        recheckaccess1.Start()
    End Sub
    Dim time As Integer

    Private Sub UpdateCheck()
        Try
            Log("Checking Updates")
            If My.Application.OpenForms.OfType(Of settings).Count = 1 Then
                Threading.Thread.Sleep(60000)
            End If
            Dim webman As New WebClient
            Dim stringman As String = webman.DownloadString("https://raw.githubusercontent.com/Nischay-Pro/Bits-Internet-Accessibility-Supervisor/master/Bits%20Internet%20Accessibility%20Supervisor/bin/Release/version.txt")
            stringman = stringman.Substring(0, My.Application.Info.Version.ToString.Length)
            If stringman = My.Application.Info.Version.ToString Then
                SetLabelText("No updates are available.", Label5)
                Log("No Updates")
            Else
                Log("New Updates")
                If Not Environment.CommandLine.Contains("-lostnet") Then
                    If MessageBox.Show("A newer update is available. Would you like to download?", "Newer Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                        Process.Start("https://github.com/Nischay-Pro/Bits-Internet-Accessibility-Supervisor/releases")
                    End If
                End If
                SetLabelText("Newer update is available.", Label5)
            End If
        Catch ex As Exception
            SetLabelText("Couldn't check for updates.", Label5)
        End Try
        Log("Finished Checking Updates")
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        time += 1
        Timer1.Interval = 1000
        Dim timespan As TimeSpan = TimeSpan.FromSeconds(time)
        Label4.Text = timespan.ToString
    End Sub

    Private Sub RunNetworkSpeed(ByVal First As Boolean)
        Log("Started Network Speed Test")
        If First = True Then
            Dim ini As New IniFile
            ini.Load(My.Application.Info.DirectoryPath & "\config.ini")
            If ini.GetKeyValue("Settings", "Automatic") = "True" Then
                GoTo ad
            Else
                Log("Stopped. Network Speed Test. Configuration Denied Access")
                SetLabelText("Network Speed : Click to Calculate", Label6)
            End If
        Else
            SetLabelText("Network Speed : Calculating", Label6)
ad:
            Dim avgamt As Integer = 0
            Dim i As Integer = 0
            Do Until i = 49
                Try
                    avgamt += GetDownloadSpeed()
                Catch ex As Exception
                End Try
                i += 1
                SetLabelText("Network Speed : Calculating " & Math.Round((i / 49 * 100)) & "%", Label6)
            Loop
            SetLabelText("Network Speed : " & FormatFileSize(avgamt / 50) & "ps. Click to Recalculate.", Label6)
            Log("Finished Network Speed Test")
            Log("Result : " & FormatFileSize(avgamt / 50) & "ps.")
        End If
    End Sub


    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.ShowInTaskbar = False
            Me.Hide()
        End If
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.ShowInTaskbar = True
        Me.Show()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        settings.Show()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If LogoutClose = False Then
            Log("Good Bye. User Closed App")
            Denotify()
            End
        Else
            Log("Logging Out and Closing")
            Denotify()
            Process.Start(Application.ExecutablePath, "-logout")
            End
        End If
    End Sub
    Public Sub LoadSettings(ByVal WithNotification As Boolean)
        Log("Loading Configurations")
        Dim ini As New IniFile
        ini.Load(My.Application.Info.DirectoryPath & "\config.ini")
        Dim profilename As String = ini.GetKeyValue("Profile", "Profile Name")
        If profilename = "" Then
            settings.Show()
        ElseIf WithNotification = True And Not Environment.CommandLine.Contains("-hidden") Then
            GenerateNotification("Welcome " & profilename & ". You have been logged in successfully.", EventType.Information, 2000, NotifyIcon1)
        End If
        If ini.GetKeyValue("Settings", "LogoutClose") = "True" Then
            LogoutClose = True
        End If
        Dim gender As String = ini.GetKeyValue("Profile", "Gender")
        If gender = "Male" Then
            PictureBox1.Image = My.Resources.user_1_
        End If
        If gender = "Female" Then
            PictureBox1.Image = My.Resources.user_2_
        End If
        If gender = "Stud" Then
            PictureBox1.Image = My.Resources.professor
        End If
        Label1.Text = "Welcome " & profilename
        Log("Finished Loading Configurations")
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        If Label6.Text.Contains("Click to Recalculate.") Or Label6.Text.Contains("Click to Calculate") Then
            Dim speedrunner As New Threading.Thread(Sub() RunNetworkSpeed(False))
            speedrunner.IsBackground = True
            speedrunner.SetApartmentState(Threading.ApartmentState.STA)
            speedrunner.Start()
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If Application.OpenForms.OfType(Of settings).Count = 1 Then
            LoadSettings(False)
        End If
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Log("Reconnecting")
        Denotify()
        Process.Start(Application.ExecutablePath)
        End
    End Sub

    Private Sub NotifyIcon1_MouseClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseClick
        Me.ShowInTaskbar = True
        Me.Show()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        If Label5.Text = "Newer update is available." Then
            Process.Start("https://github.com/Nischay-Pro/Bits-Internet-Accessibility-Supervisor/releases")
        End If
    End Sub
    Dim threshold As Integer = 0
    Private Sub Denotify()
        NotifyIcon1.Visible = False
        NotifyIcon1.Icon = Nothing
        NotifyIcon1.Dispose()
        NotifyIcon1 = Nothing
    End Sub
    Dim LogoutClose As Boolean = False
    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        If LostInternet = True Then
            threshold += 1
            Log("Threshold Set to " & threshold)
            If threshold = 5 Then
                Log("Threshold Limit Reached. Restoring Network")
                Denotify()
                Process.Start(Application.ExecutablePath, "-hidden -lostnet")
                End
            End If
        End If
    End Sub
    Dim LostInternet As Boolean = False
    Dim respav As New ListBox
    Private Sub RecheckAccess()
startman:
        Try
            Log("Checking Network Access")
            Dim timespaner As New Stopwatch
            timespaner.Start()
            Dim request As WebRequest = WebRequest.Create("http://www.google.com")
            request.Credentials = CredentialCache.DefaultCredentials
            request.Timeout = 7 * 1000
            Dim response As WebResponse = request.GetResponse()
            timespaner.Stop()
            Dim dataStream As Stream = response.GetResponseStream()
            respav.Items.Add(timespaner.ElapsedMilliseconds)
            Dim avgresp As Integer = 0
            For Each Item As Integer In respav.Items
                avgresp += Item
            Next
            avgresp = avgresp / respav.Items.Count
            If respav.Items.Count = 5 Then
                respav.Items.RemoveAt(0)
            End If
            Log("Response Time : " & timespaner.ElapsedMilliseconds)
            SetLabelText("Response Time : " & timespaner.ElapsedMilliseconds & " ms (" & avgresp & ")", Label9)
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            reader.Close()
            response.Close()
            If responseFromServer.Contains("http://172.16.0.30:8090/httpclient.html") Then
                LostInternet = True
            Else
                Log("Threshold set to 0")
                threshold = 0
            End If
            Threading.Thread.Sleep(2500)
        Catch ex As Exception
            Log(ex.Message)
            LostInternet = True
        End Try
        GoTo startman
    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click
        Log("Logging Out")
        Denotify()
        Process.Start(Application.ExecutablePath, "-logout")
        End
    End Sub

    Private Sub Label2_MouseEnter(sender As Object, e As EventArgs) Handles Label2.MouseEnter
        Label2.BackColor = Color.LightGray
    End Sub

    Private Sub Label2_MouseLeave(sender As Object, e As EventArgs) Handles Label2.MouseLeave
        Label2.BackColor = Color.Transparent
    End Sub

    Private Sub Label10_MouseEnter(sender As Object, e As EventArgs) Handles Label10.MouseEnter
        Label10.BackColor = Color.LightGray
    End Sub

    Private Sub Label10_MouseLeave(sender As Object, e As EventArgs) Handles Label10.MouseLeave
        Label10.BackColor = Color.Transparent
    End Sub

    Private Sub Label7_MouseEnter(sender As Object, e As EventArgs) Handles Label7.MouseEnter
        Label7.BackColor = Color.LightGray
    End Sub

    Private Sub Label7_MouseLeave(sender As Object, e As EventArgs) Handles Label7.MouseLeave
        Label7.BackColor = Color.Transparent
    End Sub
End Class
