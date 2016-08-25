Imports System.Net
Imports System.Net.NetworkInformation

Public Class loadman
    Public Loggedin As Boolean = False
    Dim checkstat As Boolean = False
    Dim something As Integer = 0
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Loggedin = True Then
            If Environment.CommandLine.Contains("hidden") Then
                NotifyIcon1.Visible = True
                Me.Hide()
                Timer1.Stop()
            Else
                Dim newman As New Form1
                newman.Show()
                Me.Close()
                Timer1.Stop()
            End If
        End If
        If openlogin = True Then
            openlogin = False
            Dim logina As New login
            logina.Show()
        End If
        If My.Application.OpenForms.OfType(Of login).Count = 0 And something > 20 Then
            Application.Restart()
        ElseIf My.Application.OpenForms.OfType(Of login).Count = 1 Then

        Else
            something += 1
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
    Private Sub Browser_NewWindow(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles browser.NewWindow
        e.Cancel = True
    End Sub
    Private WithEvents browser As WebBrowser
    Dim openlogin As Boolean = False
    Dim openmain As Boolean = False
    Private Sub CheckLogin()
        Dim username As String = Nothing
        Dim password As String = Nothing
        Dim ini As New IniFile
        If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\config.ini") Then
            ini.Load(My.Application.Info.DirectoryPath & "\config.ini")
            username = ini.GetKeyValue("Authentication", "Username")
            password = ini.GetKeyValue("Authentication", "Password")
        Else
            openlogin = True
            Exit Sub
        End If
        browser = New WebBrowser
        browser.ScriptErrorsSuppressed = True
        browser.Navigate("http://172.16.0.30:8090/httpclient.html")
abc:
        If browser.ReadyState = WebBrowserReadyState.Complete Then
            Try
                browser.Document.GetElementById("username").SetAttribute("value", username)
                browser.Document.GetElementById("password").SetAttribute("value", password)
                browser.Document.GetElementById("btnSubmit").InvokeMember("click")
                wait(2000)
            Catch ex As Exception
                MsgBox("Couldn't Connect to Cyberoam. Either Cyberoam is down or your not connected to a Cyberoam Network.", MsgBoxStyle.Exclamation, "Cyberoam Connection Failure")
                End
            End Try
        Else
            wait(1000)
            GoTo abc
        End If
def:
        If browser.ReadyState = WebBrowserReadyState.Complete Then
            If browser.Document.Body.InnerText.Contains("You have successfully logged in") Then
                Loggedin = True
            ElseIf browser.Document.Body.InnerText.Contains("Your data transfer has been exceeded, Please contact the administrator") Then
                'GenerateNotification("Your data transfer has exceeded. :(", EventType.Warning, 5000)
                openlogin = True
                Exit Sub
            ElseIf browser.Document.Body.InnerText.Contains("The system could not log you on. Make sure your password is correct") Then
                'GenerateNotification("Your credentials were incorrect. Retry again.", EventType.Warning, 5000)
                openlogin = True
                Exit Sub
            Else
                GenerateNotification("Server is not responding. Please try again later", EventType.Warning, 5000)
            End If
        Else GoTo def
        End If
    End Sub
    Private Sub loadman_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim threada As New Threading.Thread(AddressOf Check)
        threada.SetApartmentState(Threading.ApartmentState.STA)
        threada.IsBackground = True
        threada.Start()
    End Sub

    Private Sub Check()
        Try
            CheckLogin()
        Catch ex As Exception
            GenerateNotification("Something which should not happen happened. :(", EventType.Critical, 5000)
            End
        End Try
    End Sub
    Public Function IsConnectionAvailable() As Boolean
        Dim objUrl As New System.Uri("https://www.google.com")
        Dim objWebReq As System.Net.WebRequest
        objWebReq = System.Net.WebRequest.Create(objUrl)
        objWebReq.Proxy = Nothing
        Dim objResp As System.Net.WebResponse
        Try
            objResp = objWebReq.GetResponse
            objResp.Close()
            objWebReq = Nothing
            Return True
        Catch ex As Exception
            objResp.Close()
            objWebReq = Nothing
            Return False
        End Try
    End Function

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        NotifyIcon1.Visible = False
        Dim newman As New Form1
        newman.Show()
        Me.Close()
    End Sub
End Class