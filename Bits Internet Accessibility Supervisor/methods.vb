Imports System.Management
Imports System.Net
Imports System.Security.Cryptography

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
    Friend Function GetProcessorId() As String
        Dim strProcessorId As String = String.Empty
        Dim query As New SelectQuery("Win32_processor")
        Dim search As New ManagementObjectSearcher(query)
        Dim info As ManagementObject

        For Each info In search.Get()
            strProcessorId = info("processorId").ToString()
        Next
        Return strProcessorId

    End Function

    Friend Function GetMACAddress() As String

        Dim mc As ManagementClass = New ManagementClass("Win32_NetworkAdapterConfiguration")
        Dim moc As ManagementObjectCollection = mc.GetInstances()
        Dim MACAddress As String = String.Empty
        For Each mo As ManagementObject In moc

            If (MACAddress.Equals(String.Empty)) Then
                If CBool(mo("IPEnabled")) Then MACAddress = mo("MacAddress").ToString()

                mo.Dispose()
            End If
            MACAddress = MACAddress.Replace(":", String.Empty)

        Next
        Return MACAddress
    End Function

    Friend Function GetVolumeSerial(Optional ByVal strDriveLetter As String = "C") As String

        Dim disk As ManagementObject = New ManagementObject(String.Format("win32_logicaldisk.deviceid=""{0}:""", strDriveLetter))
        disk.Get()
        Return disk("VolumeSerialNumber").ToString()
    End Function

    Friend Function GetMotherBoardID() As String

        Dim strMotherBoardID As String = String.Empty
        Dim query As New SelectQuery("Win32_BaseBoard")
        Dim search As New ManagementObjectSearcher(query)
        Dim info As ManagementObject
        For Each info In search.Get()

            strMotherBoardID = info("SerialNumber").ToString()

        Next
        Return strMotherBoardID

    End Function



    Friend Function getMD5Hash(ByVal strToHash As String) As String
        Dim md5Obj As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim bytesToHash() As Byte = System.Text.Encoding.ASCII.GetBytes(strToHash)

        bytesToHash = md5Obj.ComputeHash(bytesToHash)

        Dim strResult As String = ""

        For Each b As Byte In bytesToHash
            strResult += b.ToString("x2")
        Next

        Return strResult
    End Function
    Public NotInheritable Class Simple3Des
        Private TripleDes As New TripleDESCryptoServiceProvider
        Private Function TruncateHash(ByVal key As String, ByVal length As Integer) As Byte()
            Dim sha1 As New SHA1CryptoServiceProvider
            Dim keyBytes() As Byte =
        System.Text.Encoding.Unicode.GetBytes(key)
            Dim hash() As Byte = sha1.ComputeHash(keyBytes)
            ReDim Preserve hash(length - 1)
            Return hash
        End Function
        Sub New(ByVal key As String)
            TripleDes.Key = TruncateHash(key, TripleDes.KeySize \ 8)
            TripleDes.IV = TruncateHash("", TripleDes.BlockSize \ 8)
        End Sub
        Public Function EncryptData(ByVal plaintext As String) As String
            Dim plaintextBytes() As Byte = System.Text.Encoding.Unicode.GetBytes(plaintext)
            Dim ms As New System.IO.MemoryStream
            Dim encStream As New CryptoStream(ms, TripleDes.CreateEncryptor(),
            System.Security.Cryptography.CryptoStreamMode.Write)
            encStream.Write(plaintextBytes, 0, plaintextBytes.Length)
            encStream.FlushFinalBlock()
            Return Convert.ToBase64String(ms.ToArray)
        End Function
        Public Function DecryptData(ByVal encryptedtext As String) As String
            Dim encryptedBytes() As Byte = Convert.FromBase64String(encryptedtext)
            Dim ms As New System.IO.MemoryStream
            Dim decStream As New CryptoStream(ms, TripleDes.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write)
            decStream.Write(encryptedBytes, 0, encryptedBytes.Length)
            decStream.FlushFinalBlock()
            Return System.Text.Encoding.Unicode.GetString(ms.ToArray)
        End Function

    End Class
    Public Function EncryptString(ByVal Password As String, ByVal Data As String)
        Try
            Dim wrapper As New Simple3Des(Password)
            Dim cipherText As String = wrapper.EncryptData(Data)
            Return cipherText
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function DecryptString(ByVal Password As String, ByVal Data As String)
        Try
            Dim wrapper As New Simple3Des(Password)
            Dim cipherText As String = wrapper.DecryptData(Data)
            Return cipherText
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub Log(ByVal Message As String)
        Try
            Dim ini As New IniFile
            ini.Load(My.Application.Info.DirectoryPath & "\config.ini")
            If ini.GetKeyValue("Settings", "Logs") = "True" Then
                Dim clean As String = DateAndTime.Today.ToString("d")
                clean = clean.Replace("-", "")
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\logs\" & clean & ".txt", "[" & DateTime.Now.ToLocalTime & "]" & Message & vbNewLine, True)
            End If
            Dim prevdays As New ListBox
            Dim i As Integer = 0
        Catch ex As Exception
        End Try
    End Sub
End Module
