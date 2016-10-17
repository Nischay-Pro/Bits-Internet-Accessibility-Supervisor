Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text
Imports Newtonsoft.Json.Linq

Public Class loadman
    Public Loggedin As Boolean = False
    Dim checkstat As Boolean = False
    Dim something As Integer = 0
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Loggedin = True Then
            If Environment.CommandLine.Contains("hidden") Then
                Log("Background Mode GUI Load Up")
                NotifyIcon1.Visible = True
                Me.Hide()
                Timer1.Stop()
                If Environment.CommandLine.Contains("lostnet") Then
                    Log("Starting Background Network Monitor")
                    Dim recheckaccess1 As New Threading.Thread(AddressOf RecheckAccess)
                    recheckaccess1.IsBackground = True
                    recheckaccess1.SetApartmentState(Threading.ApartmentState.STA)
                    recheckaccess1.Start()
                End If
            Else
                Log("Normal Mode GUI Load Up")
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
    Private Sub CheckLogin(username As String, password As String, secondary As Boolean)
        SetProgress(10, MetroProgressBar1)
        browser = New WebBrowser
        browser.ScriptErrorsSuppressed = True
        browser.Navigate("http://172.16.0.30:8090/httpclient.html")
        SetProgress(30, MetroProgressBar1)
        Dim counterman As Integer = 0
abc:
        If browser.ReadyState = WebBrowserReadyState.Complete Then
            Try
                SetProgress(40, MetroProgressBar1)
                browser.Document.GetElementById("username").SetAttribute("value", username)
                browser.Document.GetElementById("password").SetAttribute("value", password)
                browser.Document.GetElementById("btnSubmit").InvokeMember("click")
                wait(500)
                SetProgress(50, MetroProgressBar1)
            Catch ex As Exception
                If Environment.CommandLine.Contains("lostnet") Then
                    Log("Unable to connect. Switching to Standby Mode")
                    Me.Hide()
                    Timer1.Stop()
                    Timer2.Stop()
                    Log("See you in " & 60 * 1000 * 2 & " milliseconds")
                    For i As Integer = 0 To 120
                        Threading.Thread.Sleep(1000)
                    Next
                    Denotify()
                    Process.Start(Application.ExecutablePath, "-hidden -lostnet")
                    End
                Else
                    Log("Cannot Connect to Cyberoam")
                    MsgBox("Couldn't Connect to Cyberoam. Either Cyberoam is down or your not connected to a Cyberoam Network.", MsgBoxStyle.Exclamation, "Cyberoam Connection Failure")
                    Denotify()
                    End
                End If
            End Try
        Else
            If counterman <> 10 Then
                wait(500)
                counterman += 1
                GoTo abc
            Else
                Log("Cannot Connect to Cyberoam")
                MsgBox("Couldn't Connect to Cyberoam. Either Cyberoam is down or your not connected to a Cyberoam Network.", MsgBoxStyle.Exclamation, "Cyberoam Connection Failure")
                Denotify()
                End
            End If
        End If
def:
        SetProgress(80, MetroProgressBar1)
        If browser.ReadyState = WebBrowserReadyState.Complete Then
            If browser.Document.Body.InnerText.Contains("You have successfully logged in") Then
                Log("Logged in")
                SetProgress(100, MetroProgressBar1)
                CheckUpdates()
                Loggedin = True
                If Environment.CommandLine.Contains("-lostnet") Then
                    Log("Restored Net")
                    'GenerateNotification("Net has been restored. :)", EventType.Warning, 5000)
                End If
                If Environment.CommandLine.Contains("-logout") Then
                    SetProgress(90, MetroProgressBar1)
                    Log("Logging off")
                    browser.Document.GetElementById("btnSubmit").InvokeMember("click")
                    wait(500)
                    If browser.Document.Body.InnerText.Contains("You have successfully logged off") Then
                        Log("Logged off")
                        NotifyIcon1.Visible = False
                        NotifyIcon1.Dispose()
                        Denotify()
                        End
                    End If
                End If
            ElseIf browser.Document.Body.InnerText.Contains("Your data transfer has been exceeded, Please contact the administrator") Then
                Log("Data Transfer Exceeded")
                'GenerateNotification("Your data transfer has exceeded. :(", EventType.Warning, 5000)
                openlogin = True
                Exit Sub
            ElseIf browser.Document.Body.InnerText.Contains("The system could not log you on. Make sure your password is correct") Then
                Log("Incorrect Credentials")
                'GenerateNotification("Your credentials were incorrect. Retry again.", EventType.Warning, 5000)
                openlogin = True
                Exit Sub
            Else
                Log("Server Crashed")
                GenerateNotification2("Server is not responding. Please try again later", EventType.Warning, 5000)
            End If
        Else GoTo def
        End If
    End Sub
    Private Sub CheckUpdates()
        Log("Checking Updates")
        If Environment.CommandLine.Contains("hidden") Then
            Try
                Dim webman As New WebClient
                Dim stringman As String = webman.DownloadString("https://raw.githubusercontent.com/Nischay-Pro/Bits-Internet-Accessibility-Supervisor/master/Bits%20Internet%20Accessibility%20Supervisor/bin/Release/version.txt")
                stringman = stringman.Substring(0, My.Application.Info.Version.ToString.Length)
                If stringman = My.Application.Info.Version.ToString Then
                    Log("No New Updates")
                Else
                    Log("New Updates")
                    If Not Environment.CommandLine.Contains("-lostnet") Then
                        If MessageBox.Show("A newer update is available. Would you like to download?", "Newer Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                            Process.Start("https://github.com/Nischay-Pro/Bits-Internet-Accessibility-Supervisor/releases")
                        End If
                    End If
                End If
            Catch ex As Exception
                Log(ex.Message)
            End Try
        End If
        Log("Checked for updates")
    End Sub
    Private Sub loadman_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Log("Started Application")
        Log("Application Version : " & My.Application.Info.Version.ToString)
        Log("Parameters Loaded")
        Log(Environment.CommandLine)
        My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\version.txt", My.Application.Info.Version.ToString, False)
        Dim threada As New Threading.Thread(AddressOf Check)
        threada.SetApartmentState(Threading.ApartmentState.STA)
        threada.IsBackground = True
        threada.Start()
        MetroProgressBar1.Visible = True
    End Sub

    Private Sub Check()
        Try
            Log("Logging in")
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
            CheckLogin(username, password, False)
        Catch ex As Exception
            Log(ex.Message)
            GenerateNotification2("Something wrong happened. :(", EventType.Critical, 5000)
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

    Private Sub LoadfromSecondary()
        Dim Data As String = Nothing
        If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\accounts.json") Then
            Data = My.Computer.FileSystem.ReadAllText(My.Application.Info.DirectoryPath & "\accounts.json")
        End If
        Dim parsed As JObject = JObject.Parse(Data)
        Dim JArrayman As JArray = parsed("accounts")
        For Each Item As JObject In JArrayman
            If Item("status") = "Not Verified" Then
                CheckLogin(Item("username"), Item("password"), True)
            End If
        Next
    End Sub

    Private Sub UpdateSecondary(username As String)
        Dim Data As String = Nothing
        If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\accounts.json") Then
            Data = My.Computer.FileSystem.ReadAllText(My.Application.Info.DirectoryPath & "\accounts.json")
        End If
        Dim parsed As JObject = JObject.Parse(Data)
        Dim JArrayman As JArray = parsed("accounts")
        For Each Item As JObject In JArrayman
            If Item("username") = username Then
                Item("status") = "Currently Active"
                Dim milliseconds = CLng(DateTime.UtcNow.Subtract(New DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds)
                Item("timestamp") = milliseconds
            ElseIf Item("status") = "Not Verified" Then
            Else
                Item("status") = "Not Active"
            End If
        Next
    End Sub
    Private Sub LimitSecondary(username As String)

        Dim Data As String = Nothing
        If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\accounts.json") Then
            Data = My.Computer.FileSystem.ReadAllText(My.Application.Info.DirectoryPath & "\accounts.json")
        End If
        Dim parsed As JObject = JObject.Parse(Data)
        Dim JArrayman As JArray = parsed("accounts")
        For Each Item As JObject In JArrayman
            If Item("username") = username Then
                Item("status") = "Data Exceeded"
                Dim milliseconds = CLng(DateTime.UtcNow.Subtract(New DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds)
                Item("timestamp") = milliseconds
            End If
        Next
    End Sub
    Private Sub InvalidSecondary(username As String)
        Dim Data As String = Nothing
        If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\accounts.json") Then
            Data = My.Computer.FileSystem.ReadAllText(My.Application.Info.DirectoryPath & "\accounts.json")
        End If
        Dim parsed As JObject = JObject.Parse(Data)
        Dim JArrayman As JArray = parsed("accounts")
        For Each Item As JObject In JArrayman
            If Item("username") = username Then
                Item("status") = "Invalid Credentials"
                Dim milliseconds = CLng(DateTime.UtcNow.Subtract(New DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds)
                Item("timestamp") = milliseconds
            End If
        Next
    End Sub

    Private Sub Denotify()
        NotifyIcon1.Visible = False
        NotifyIcon1.Icon = Nothing
        NotifyIcon1.Dispose()
        NotifyIcon1 = Nothing
    End Sub
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
            threshold += 1
            Log("Threshold now at " & threshold)
            If threshold = 5 Then
                Log("Reached Threshold Limit. Restoring Network Access.")
                Denotify()
                Process.Start(Application.ExecutablePath, "-hidden -lostnet")
                End
            End If
        End If
    End Sub
    Dim threshold As Integer = 0
    Dim LostInternet As Boolean = False
    Private Sub RecheckAccess()
startman:
        Log("Checking Network Access")
        Try
            Dim request As WebRequest = WebRequest.Create("http://www.google.com")
            request.Credentials = CredentialCache.DefaultCredentials
            request.Timeout = 7 * 1000
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
                Log("Threshold Set to 0")
                threshold = 0
            End If
            Threading.Thread.Sleep(2500)
        Catch ex As Exception
            LostInternet = True
        End Try
        GoTo startman
    End Sub
End Class