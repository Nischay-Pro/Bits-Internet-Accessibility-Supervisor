Public Class login
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If MetroTextBox1.Text <> Nothing And MetroTextBox2.Text <> Nothing Then
            Dim ini As New IniFile
            If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\config.ini") Then
                ini.Load(My.Application.Info.DirectoryPath & "\config.ini")
                ini.AddSection("Authentication")
                ini.SetKeyValue("Authentication", "Username", MetroTextBox1.Text)
                ini.SetKeyValue("Authentication", "Password", MetroTextBox2.Text)
                ini.Save(My.Application.Info.DirectoryPath & "\config.ini")
                CheckLogin()
            Else
                ini.AddSection("Authentication")
                ini.SetKeyValue("Authentication", "Username", MetroTextBox1.Text)
                ini.SetKeyValue("Authentication", "Password", MetroTextBox2.Text)
                ini.Save(My.Application.Info.DirectoryPath & "\config.ini")
                CheckLogin()
            End If
        End If
    End Sub
    Private Sub Browser_NewWindow(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles browser.NewWindow
        e.Cancel = True
    End Sub
    Private WithEvents browser As WebBrowser
    Private Sub CheckLogin()
        browser = New WebBrowser
        browser.ScriptErrorsSuppressed = True
        browser.Navigate("http://172.16.0.30:8090/httpclient.html")
abc:
        If browser.ReadyState = WebBrowserReadyState.Complete Then
            Try
                browser.Document.GetElementById("username").SetAttribute("value", MetroTextBox1.Text)
                browser.Document.GetElementById("password").SetAttribute("value", MetroTextBox2.Text)
                browser.Document.GetElementById("btnSubmit").InvokeMember("click")
                wait(2000)
            Catch ex As Exception

            End Try
        Else
            wait(1000)
            GoTo abc
        End If
def:
        If browser.ReadyState = WebBrowserReadyState.Complete Then
            If browser.Document.Body.InnerText.Contains("You have successfully logged in") Then
                loadman.Loggedin = True
                Me.Close()
            ElseIf browser.Document.Body.InnerText.Contains("Your data transfer has been exceeded, Please contact the administrator") Then
                GenerateNotification("Your data transfer has exceeded. :(", EventType.Warning, 5000)
            ElseIf browser.Document.Body.InnerText.Contains("The system could not log you on. Make sure your password is correct") Then
                GenerateNotification("Your credentials were incorrect. Retry again.", EventType.Warning, 5000)
            Else
                GenerateNotification("Server is not responding. Please try again later", EventType.Warning, 5000)
            End If
        Else
            GoTo def
        End If
    End Sub
    Private Sub wait(ByVal interval As Integer)
        Dim sw As New Stopwatch
        sw.Start()
        Do While sw.ElapsedMilliseconds < interval
            Application.DoEvents()
        Loop
        sw.Stop()
    End Sub

    Private Sub login_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        End
    End Sub
End Class