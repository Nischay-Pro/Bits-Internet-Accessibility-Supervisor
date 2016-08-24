
Imports System.Net

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ini As New IniFile
        ini.Load(My.Application.Info.DirectoryPath & "\config.ini")
        Dim welcomeme As String = Nothing
        If ini.GetKeyValue("Authentication", "Username") = "f2015606" Then
            welcomeme = "My Lord"
        Else
            welcomeme = ini.GetKeyValue("Authentication", "Username")
        End If
        Label1.Text = "Welcome " & welcomeme
    End Sub
    Dim time As Integer
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        time += 1
        Timer1.Interval = 1000
        Dim timespan As TimeSpan = TimeSpan.FromSeconds(time)
        Label4.Text = timespan.ToString
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
End Class
