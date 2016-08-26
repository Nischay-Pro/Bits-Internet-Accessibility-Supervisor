Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text

Public Class Form1
    Public FormData As MetroFramework.Forms.MetroForm
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ini As New IniFile
        ini.Load(My.Application.Info.DirectoryPath & "\config.ini")
        Dim welcomeme As String = Nothing

        If ini.GetKeyValue("Authentication", "Username") = "f2015606" Then
            welcomeme = "My Lord"
        Else
            welcomeme = ini.GetKeyValue("Authentication", "Username")
        End If
        ini.SetKeyValue("Settings", "Version", My.Application.Info.Version.ToString)
        ini.Save(My.Application.Info.DirectoryPath & "\config.ini")
        Label1.Text = "Welcome " & welcomeme
        LoadSettings(True)
        Dim speedrunner As New Threading.Thread(Sub() RunNetworkSpeed(True))
        speedrunner.IsBackground = True
        speedrunner.SetApartmentState(Threading.ApartmentState.STA)
        speedrunner.Start()
        Dim checkupdates As New Threading.Thread(AddressOf UpdateCheck)
        checkupdates.IsBackground = True
        checkupdates.SetApartmentState(Threading.ApartmentState.STA)
        checkupdates.Start()
        Dim recheckaccess1 As New Threading.Thread(AddressOf RecheckAccess)
        recheckaccess1.IsBackground = True
        recheckaccess1.SetApartmentState(Threading.ApartmentState.STA)
        recheckaccess1.Start()
    End Sub
    Dim time As Integer

    Private Sub UpdateCheck()
        Try
            Dim webman As New WebClient
            Dim stringman As String = webman.DownloadString("https://raw.githubusercontent.com/Nischay-Pro/Bits-Internet-Accessibility-Supervisor/master/Bits%20Internet%20Accessibility%20Supervisor/bin/Release/version.txt")
            stringman = stringman.Substring(0, My.Application.Info.Version.ToString.Length)
            If stringman = My.Application.Info.Version.ToString Then
                SetLabelText("No updates are available.", Label5)
            Else
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
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        time += 1
        Timer1.Interval = 1000
        Dim timespan As TimeSpan = TimeSpan.FromSeconds(time)
        Label4.Text = timespan.ToString
    End Sub

    Private Sub RunNetworkSpeed(ByVal First As Boolean)
        If First = True Then
            Dim ini As New IniFile
            ini.Load(My.Application.Info.DirectoryPath & "\config.ini")
            If ini.GetKeyValue("Settings", "Automatic") = "True" Then
                GoTo ad
            Else
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
        End
    End Sub
    Public Sub LoadSettings(ByVal WithNotification As Boolean)
        Dim ini As New IniFile
        ini.Load(My.Application.Info.DirectoryPath & "\config.ini")
        Dim profilename As String = ini.GetKeyValue("Profile", "Profile Name")
        If profilename = "" Then
            settings.Show()
        ElseIf WithNotification = True And Not Environment.CommandLine.Contains("-hidden") Then
            GenerateNotification("Welcome " & profilename & ". You have been logged in successfully.", EventType.Information, 2000)
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

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        If LostInternet = True Then
            Process.Start(Application.ExecutablePath, "-hidden -lostnet")
            End
        End If
    End Sub
    Dim LostInternet As Boolean = False
    Private Sub RecheckAccess()
startman:
        Try
            Dim timespaner As New Stopwatch
            timespaner.Start()
            Dim request As WebRequest = WebRequest.Create("http://www.google.com")
            request.Credentials = CredentialCache.DefaultCredentials
            request.Timeout = 5 * 1000
            Dim response As WebResponse = request.GetResponse()
            timespaner.Stop()
            Dim dataStream As Stream = response.GetResponseStream()
            SetLabelText("Response Time : " & timespaner.ElapsedMilliseconds & " ms", Label9)
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            reader.Close()
            response.Close()
            If responseFromServer.Contains("http://172.16.0.30:8090/httpclient.html") Then
                LostInternet = True
            Else
            End If
            Threading.Thread.Sleep(5000)
            GoTo startman
        Catch ex As Exception
            LostInternet = True
        End Try
    End Sub
End Class
