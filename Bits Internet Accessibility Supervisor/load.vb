Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text

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
                If Environment.CommandLine.Contains("lostnet") Then
                    Dim recheckaccess1 As New Threading.Thread(AddressOf RecheckAccess)
                    recheckaccess1.IsBackground = True
                    recheckaccess1.SetApartmentState(Threading.ApartmentState.STA)
                    recheckaccess1.Start()
                End If
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
    Public Shared Function GetUnixTimestamp() As Double
        Dim val = (DateTime.Now - New DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds
        Dim rep As String = Math.Round(val, 3)
        rep = rep.Replace(".", "")
        Return rep
    End Function
    Private Sub CheckLogin()
        If Environment.CommandLine.Contains("hidden") Then
            Try
                Dim webman As New WebClient
                Dim stringman As String = webman.DownloadString("https://raw.githubusercontent.com/Nischay-Pro/Bits-Internet-Accessibility-Supervisor/master/Bits%20Internet%20Accessibility%20Supervisor/bin/Release/version.txt")
                stringman = stringman.Substring(0, My.Application.Info.Version.ToString.Length)
                If stringman = My.Application.Info.Version.ToString Then
                Else
                    If Not Environment.CommandLine.Contains("-lostnet") Then
                        If MessageBox.Show("A newer update is available. Would you like to download?", "Newer Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                            Process.Start("https://github.com/Nischay-Pro/Bits-Internet-Accessibility-Supervisor/releases")
                        End If
                    End If
                End If
            Catch ex As Exception
            End Try
        End If
        Dim username As String = Nothing
        Dim password As String = Nothing
        Dim ini As New IniFile
        If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\config.ini") Then
            ini.Load(My.Application.Info.DirectoryPath & "\config.ini")
            username = ini.GetKeyValue("Authentication", "Username")
            Try
                'password = DecryptString(getMD5Hash(GetMotherBoardID() & GetProcessorId() & GetVolumeSerial()), ini.GetKeyValue("Authentication", "Password"))
                password = ini.GetKeyValue("Authentication", "Password")
            Catch ex As Exception
                openlogin = True
                Exit Sub
            End Try
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
                If Environment.CommandLine.Contains("lostnet") Then
                    Me.Hide()
                    Threading.Thread.Sleep(60 * 1000 * 15)
                    Process.Start(Application.ExecutablePath, "-hidden -lostnet")
                    End
                Else
                    MsgBox("Couldn't Connect to Cyberoam. Either Cyberoam is down or your not connected to a Cyberoam Network.", MsgBoxStyle.Exclamation, "Cyberoam Connection Failure")
                    End
                End If
            End Try
        Else
            wait(1000)
            GoTo abc
        End If
def:
        If browser.ReadyState = WebBrowserReadyState.Complete Then
            If browser.Document.Body.InnerText.Contains("You have successfully logged in") Then
                Loggedin = True
                If Environment.CommandLine.Contains("-lostnet") Then
                    GenerateNotification("Net has been restored. :)", EventType.Warning, 5000)
                End If
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
        My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\version.txt", My.Application.Info.Version.ToString, False)
        Dim threada As New Threading.Thread(AddressOf Check)
        threada.SetApartmentState(Threading.ApartmentState.STA)
        threada.IsBackground = True
        threada.Start()
    End Sub

    Private Sub Check()
        Try
            CheckLogin()
        Catch ex As Exception
            GenerateNotification("Something wrong happened. :(", EventType.Critical, 5000)
            wait(5000)
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
            objResp = Nothing
            objResp.Close()
            objWebReq = Nothing
            Return False
        End Try
    End Function

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        NotifyIcon1.Visible = False
        Dim newman As New Form1
        newman.Show()
        Close()
    End Sub

    Private Sub NotifyIcon1_MouseClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseClick
        NotifyIcon1.Visible = False
        Dim newman As New Form1
        newman.Show()
        Close()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If LostInternet = True Then
            Process.Start(Application.ExecutablePath, "-hidden -lostnet")
            End
        End If
    End Sub
    Dim LostInternet As Boolean = False
    Private Sub RecheckAccess()
startman:
        Try
            Dim request As WebRequest = WebRequest.Create("http://www.google.com")
            request.Credentials = CredentialCache.DefaultCredentials
            request.Timeout = 5 * 1000
            Dim response As WebResponse = request.GetResponse()
            Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
            Dim dataStream As Stream = response.GetResponseStream()
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