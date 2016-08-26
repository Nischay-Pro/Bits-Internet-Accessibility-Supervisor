Imports System.Net
Imports System.Net.NetworkInformation

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
    End Sub
    Dim time As Integer

    Private Sub UpdateCheck()

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
    Public Shared Function CheckForInternetConnection() As Boolean
        Try
            Using client = New WebClient()
                Using stream = client.OpenRead("http://www.google.com")
                    Return True
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function

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
        ElseIf WithNotification = True Then
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
End Class
