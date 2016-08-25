Imports System.Net

Module methods
    Public Sub SetLabelText(ByVal text As String, ByVal Label As Control)
        If Label.InvokeRequired Then
            Label.Invoke(New SetText(AddressOf SetLabelText), text, Label)
        Else
            Label.Text = text
        End If
    End Sub
    Public Delegate Sub SetText(text As String, Label As Control)
    Public Function FormatFileSize(ByVal Size As Long) As String
        Try
            Dim KB As Integer = 1024
            Dim MB As Integer = KB * KB
            Dim GB As Integer = MB * 1024
            ' Return size of file in kilobytes.
            If Size < KB Then
                Return (Size.ToString("D") & " bytes")
            Else
                Select Case Size / KB
                    Case Is < 100
                        Return (Size / KB).ToString("N") & "KB"
                    Case Is < 1000000
                        Return (Size / MB).ToString("N") & "MB"
                    Case Is < 10000000
                        Return (Size / MB / KB).ToString("N") & "GB"
                    Case Is < 10000000
                        Return (Size / GB / MB / KB).ToString("N") & "TB"
                    Case Else
                        Return Size.ToString & "bytes"

                End Select
            End If
        Catch ex As Exception
            Return Size.ToString
        End Try
    End Function
    Public Function GetDownloadSpeed()

        Dim webclinetman As New WebClient
        Dim timestop As New Stopwatch
        timestop.Start()
        webclinetman.DownloadFile("http://speedtest.ftp.otenet.gr/files/test100k.db", My.Application.Info.DirectoryPath & "\data.test")
        timestop.Stop()
        Kill(My.Application.Info.DirectoryPath & "\data.test")
        Return Math.Round((1000000 / (timestop.ElapsedMilliseconds / 1000)), 4)

    End Function
    Public Enum EventType
        Warning
        Information
        Critical
    End Enum
    Public Sub GenerateNotification(ByVal Message As String, ByVal Type As EventType, ByVal Timeout As Integer)
        Dim notman As New notification
        If Type = EventType.Critical Then
            notman.Style = MetroFramework.MetroColorStyle.Red
        End If
        If Type = EventType.Information Then
            notman.Style = MetroFramework.MetroColorStyle.Default
        End If
        If Type = EventType.Warning Then
            notman.Style = MetroFramework.MetroColorStyle.Orange
        End If
        notman.Timer1.Interval = Timeout
        notman.Visible = True
        notman.Label1.Text = Message
        notman.Show()
        Dim x As Integer
        Dim y As Integer
        x = Screen.PrimaryScreen.WorkingArea.Width
        y = Screen.PrimaryScreen.WorkingArea.Height - notman.Height

        Do Until x = Screen.PrimaryScreen.WorkingArea.Width - notman.Width
            x = x - 1
            notman.Location = New Point(x, y)
        Loop
    End Sub
End Module
