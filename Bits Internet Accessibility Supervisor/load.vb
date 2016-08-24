Imports System.Net

Public Class loadman
    Public Loggedin As Boolean = False
    Dim checkstat As Boolean = False
    Dim something As Integer = 0
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        something += 1
        If Loggedin = True Then
            Dim newman As New Form1
            newman.Show()
            Me.Close()
            Timer1.Stop()
        End If
        If openlogin = True Then
            openlogin = False
            Dim logina As New login
            logina.Show()
        End If
        If openlogin = False And something > 20 Then
            Application.Restart()
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

            End Try
        Else
            wait(1000)
            GoTo abc
        End If
def:
        If browser.ReadyState = WebBrowserReadyState.Complete Then
            If browser.Document.Body.InnerText.Contains("You have successfully logged in") Then
                Loggedin = True
            Else
                openlogin = True
                Exit Sub
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
            If IsConnectionAvailable() = False Then
                login.Show()
            Else
                CheckLogin()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function IsConnectionAvailable() As Boolean
        Dim objUrl As New System.Uri("http://www.google.com/")
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
End Class