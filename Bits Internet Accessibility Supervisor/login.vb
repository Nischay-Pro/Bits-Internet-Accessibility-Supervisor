Imports System.Net

Public Class login
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Timer1.Start()
        If MetroTextBox1.Text <> Nothing And MetroTextBox2.Text <> Nothing Then
            Dim ini As New IniFile
            Log("Loading Configuration")
            If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\config.ini") Then
                Log("Found Configuration")
                ini.Load(My.Application.Info.DirectoryPath & "\config.ini")
                ini.AddSection("Authentication")
                ini.SetKeyValue("Authentication", "Username", MetroTextBox1.Text)
                'ini.SetKeyValue("Authentication", "Password", EncryptString(getMD5Hash(GetMotherBoardID() & GetProcessorId() & GetVolumeSerial()), MetroTextBox2.Text))
                ini.SetKeyValue("Authentication", "Password", MetroTextBox2.Text)
                ini.Save(My.Application.Info.DirectoryPath & "\config.ini")
                CheckLogin()
            Else
                Log("Nope. Couldn't find. Newcomer. But how?")
                ini.AddSection("Authentication")
                ini.SetKeyValue("Authentication", "Username", MetroTextBox1.Text)
                'ini.SetKeyValue("Authentication", "Password", EncryptString(getMD5Hash(GetMotherBoardID() & GetProcessorId() & GetVolumeSerial()), MetroTextBox2.Text))
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
        Log("Checking Credentials")
        MetroTextBox2.Enabled = False
        MetroTextBox1.Enabled = False
        PictureBox2.Enabled = False
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
                Log("Credentials Verified. Your Good to GO.")
                Process.Start(Application.ExecutablePath)
                End
            ElseIf browser.Document.Body.InnerText.Contains("Your data transfer has been exceeded, Please contact the administrator") Then
                Log("Data Transfer Exceeded")
                GenerateNotification2("Your data transfer has exceeded. :(", EventType.Warning, 5000)
            ElseIf browser.Document.Body.InnerText.Contains("The system could not log you on. Make sure your password is correct") Then
                GenerateNotification2("Your credentials were incorrect. Retry again.", EventType.Warning, 5000)
                Log("Invalid Credentials")
            Else
                GenerateNotification2("Server is not responding. Please try again later", EventType.Warning, 5000)
                Log("Server Crash")
            End If
        Else
            GoTo def
        End If
        Timer1.Stop()
        PictureBox2.Enabled = True
        MetroTextBox2.Enabled = True
        MetroTextBox1.Enabled = True
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

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Process.Start(Application.ExecutablePath)
        End
    End Sub

    Private Sub login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class