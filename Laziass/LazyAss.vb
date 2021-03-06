﻿Imports System.ComponentModel
Imports System.IO
Imports System.IO.Compression
Imports BizHawk.Emulation.DiscSystem
Imports DiscTools

Public Class LazyAss

    Public c_os, tProcess, wDir, Arg, DAEPath, std_out, dtl_iso, FileBin, TaskEnd, CdBus, DVDBrand, EnvType As String,
        CMType, LbaRow, TSound, FileToAppend, ExtRip As String, PitStop, Abort, CountPgap, IsRedump As Boolean, PStart, Elapsed As Date,
        execute As New Process, multithread, AppID, task, ntrack, ErrorAbort As Integer, percentage As Double

    Public DiscMode, DiscName, ResultDisc, ConsoleCDType As String

    Public objStreamWriter As StreamWriter

    Public Declare Function OpenDrawerCD Lib "winmm.dll" Alias "mciSendStringA" (ByVal lpstrCommand As String, ByVal lpstrReturnString As String, ByVal uReturnLength As Long, ByVal hwndCallback As Long) As Long

    Public Sub MakeCCD()
        Try
            SaveFileDialog1.Filter = "File CCD|*.ccd"
            SaveFileDialog1.Title = "Set a path and name for CCD file"
            SaveFileDialog1.InitialDirectory = OutputPath.Text & RippedName.Text
            SaveFileDialog1.FileName = Path.GetFileNameWithoutExtension(dtl_iso)

            If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
                OutputPath.Text = Path.GetDirectoryName(SaveFileDialog1.FileName) & "\"
                task = 0
                LogOut.Clear()
                Dim basename As String
                'If OutputPath.Text.Trim = "" Then
                basename = Path.Combine(Path.GetDirectoryName(dtl_iso), Path.GetFileNameWithoutExtension(dtl_iso))
                'Else
                'basename = Path.Combine(Path.Combine(OutputPath.Text, RippedName.Text), Path.GetFileNameWithoutExtension(dtl_iso))
                'End If

                Dim job = New DiscMountJob With {.IN_FromPath = basename & ".cue"}
                job.Run()
                Dim disc = job.OUT_Disc

                If job.OUT_ErrorLevel Then
                    TSound = "Ugh oooh"
                    PlayRandom()
                    MsgBox(job.OUT_Log, "Error loading CUE", vbOK + vbCritical)
                    LogOut.AppendText(vbCrLf & "Unable to generate ccd/img/sub file, a error occur..." & vbCrLf)
                    Abort = True
                    Exit Sub
                End If

                LogOut.AppendText(vbCrLf & "Wait until will be generated ccd/img/sub file..." & vbCrLf)
                CCD_Format.Dump(disc, SaveFileDialog1.FileName)
                'MsgBox("Virtual CCD image created!", vbOKOnly + MsgBoxStyle.Information, "CCD image created!")
                LogOut.AppendText(vbCrLf & Path.GetFileName(SaveFileDialog1.FileName) & "\img\sub created!")
                LogOut.ScrollToCaret()
                'TSound = "Yoolaiyoleihee"
                TSound = "Success"
                PlayRandom()
                disc.Dispose()
            Else
                Abort = True
                Exit Sub
            End If
        Catch ex As Exception
            TSound = "Ugh oooh"
            PlayRandom()
            MsgBox(ex.ToString)
            LogOut.AppendText(vbCrLf & "Unable to generate ccd/img/sub file, a error occur..." & vbCrLf)
            LogOut.ScrollToCaret()
            Abort = True
        Finally
            If Abort = False Then
                MsgBox("CCD/IMG/SUB created!", vbOKOnly + MsgBoxStyle.Information, "Image rebuilded...")
            Else
                Abort = False
            End If
        End Try

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        ExtractWithHawk()
    End Sub

    Public Sub ExtractWithHawk()

        Try
            Dim disc1 = Disc.LoadAutomagic(dtl_iso)
            Dim dpath = OutputPath.Text & RippedName.Text 'Path.GetDirectoryName(SaveFileDialog1.FileName)
            Dim filename = Path.GetFileNameWithoutExtension(dtl_iso)
            TrackExtractor.Extract(disc1, dpath, filename)
        Catch ex As Exception
            'MsgBox(ex.ToString)
        End Try

    End Sub

    Public Sub MakeCUEBIN()

        Try
            SaveFileDialog1.Filter = "CUE File|*.cue"
            SaveFileDialog1.Title = "Set a path and name for CUE file"
            SaveFileDialog1.InitialDirectory = OutputPath.Text & RippedName.Text
            SaveFileDialog1.FileName = Path.GetFileNameWithoutExtension(dtl_iso)

            If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
                OutputPath.Text = Path.GetDirectoryName(SaveFileDialog1.FileName) & "\"
                TaskEnd = "Wrote " & Path.GetFileNameWithoutExtension(SaveFileDialog1.FileName) & ".bin"
                wDir = Application.StartupPath & "\Converter"
                tProcess = Application.StartupPath & "\Converter\binmerge.exe"

                Arg = "-o " & Chr(34) & Path.GetDirectoryName(SaveFileDialog1.FileName) & Chr(34) & " " & Chr(34) & dtl_iso & Chr(34) & " " &
                    Chr(34) & Path.GetFileNameWithoutExtension(SaveFileDialog1.FileName) & Chr(34)

                task = 99999
                LogOut.Clear()
                BackgroundWorker1.RunWorkerAsync()
                BlockAll()
                PStart = Date.Now
            Else
                Abort = True
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
            LogOut.AppendText(vbCrLf & "Unable to generate cue/bin file, a error occur..." & vbCrLf)
            LogOut.ScrollToCaret()
        End Try
        'Abort = True
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectImage.Click
        'dtl_iso = Nothing
        task = 0
        Dim mnt_iso As OpenFileDialog = New OpenFileDialog()
        mnt_iso.Title = "Select Desired CUE File To Rebuild"
        mnt_iso.RestoreDirectory = True
        If TypeRIP.Checked = True Then
            mnt_iso.Title = "Select Virtual CD Game to Mount"
            mnt_iso.Filter = "All supported format (*.mds,*.mdx,*.b5t,*.b6t,*.bwt,*.cue,*.ccd,*.isz,*.nrg,*.cdi,*.iso,*.ecm)|*.mds;*.mdx;*.b5t;*.b6t;*.bwt;*.cue;*.ccd;*.isz;*.nrg;*.cdi;*.iso;*.ecm|All file (*.*)|*.*"
            If mnt_iso.ShowDialog() = DialogResult.OK Then
                task = -1
                RippedName.Text = ""
                dtl_iso = mnt_iso.FileName

                RadioDaemon()

                wDir = Environment.GetFolderPath(EnvType) & "\" & DAEPath

                DaemonType()
                DaemonUnmount()
                DaemonMount()
                Load_Drive()

                If LCase(Path.GetExtension(dtl_iso)) = ".cue" Or LCase(Path.GetExtension(dtl_iso)) = ".ccd" Or LCase(Path.GetExtension(dtl_iso)) = ".iso" Then
                    DetectByDiscTools()
                End If

                Dim StartTime As Date = Now
                Do
                    Application.DoEvents()
                Loop Until (Now - StartTime).TotalMilliseconds > 1000
                'Load_Drive()
                'UNI.SelectedIndex = UNI.Items.Count - 1

                Dim Bcd() As String
                For Each line As String In File.ReadLines(Application.StartupPath & "\DeviceName")
                    If line.Contains("DiscSoft") Then
                        Bcd = line.Split(" ")
                    End If
                Next
                UNI.Text = Bcd(0).Trim

                'If RippedName.Text = "" Then RippedName.Text = (Path.GetFileNameWithoutExtension(dtl_iso)).Trim
            End If
        Else
            If RadioButton4.Checked = True Then
                mnt_iso.Filter = "All supported format (*.mds,*.cue,*.ccd)|*.mds;*.cue;*.ccd"
                mnt_iso.Title = "Select Virtual CD Game"
            Else
                mnt_iso.Title = "Select Virtual CD Game CUE File"
                mnt_iso.Filter = "CUE File (*.cue)|*.cue"
            End If

            If mnt_iso.ShowDialog() = DialogResult.OK Then
                RippedName.Text = ""
                dtl_iso = mnt_iso.FileName
                GetBinaryFromCue()

                If RebuildCUE.Checked = False Then
                    DetectByDiscTools()
                    If LCase(Path.GetExtension(FileBin)) <> ".bin" And RadioButton4.Checked = False Then
                        MsgBox("The cue file point to a not single bin format file!" & vbCrLf &
                             "Use Disco Hawk engine to rip this image", MessageBoxButtons.OK + MessageBoxIcon.Exclamation, "Can't convert this cue")
                        Exit Sub
                    Else
                        If IsRedump = True And RadioButton4.Checked = False Then
                            MsgBox("This seem a standard CUE Redump (multi bin)." & vbCrLf &
                                    "First merge it into a single cue/bin and try again the ripping.", MessageBoxButtons.OK + MessageBoxIcon.Exclamation, "Can't rip this cue")
                            IsRedump = False
                            Exit Sub
                        End If
                    End If
                    'RebuildCUE.Enabled = False
                Else
                    If dtl_iso IsNot Nothing Then ScanBinWav()
                    ExtRip = ".*"
                End If
                If RippedName.Text = "" Then RippedName.Text = (Path.GetFileNameWithoutExtension(dtl_iso)).Trim
            End If
        End If

        'If RebuildCUE.Checked = True And dtl_iso IsNot Nothing Then ScanBinWav()
    End Sub

    Private Sub ScanBinWav()
        Dim countwb As Integer = 0
        Dim countbin As Integer = 0
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(Path.GetDirectoryName(dtl_iso))
            Select Case LCase(Path.GetExtension(foundFile))
                Case ".wav", ".iso"
                    countwb += 1
                Case ".bin"
                    countwb += 1
                    countbin += 1
            End Select
        Next

        If countwb > 3 Then
            'LogOut.Clear()
            OutputPath.Text = ""
            If countbin > 3 Then
                'FixForBinMerge()
                If File.Exists(Path.Combine(Application.StartupPath, "Converter\binmerge.exe")) And IsRedump = True Then
                    RebuildImage.Button1.Enabled = True
                End If
                RebuildImage.ShowDialog()
            Else
                RebuildImage.Button1.Enabled = False
                RebuildImage.ShowDialog()
                'MakeCCD()
            End If

        End If
    End Sub

    Private Sub FixForBinMerge()
        Dim content As String = ""
        Dim content1 As String = ""
        Dim Splitcontent() As String = Nothing
        Using objStreamReader As New StreamReader(dtl_iso)
            content = objStreamReader.ReadToEnd()
            objStreamReader.Dispose()
            objStreamReader.Close()

            content = content.Replace("PREGAP 00:02:00", "INDEX 00 00:00:00")
            Splitcontent = content.Split(vbCrLf)

            For i = 0 To Splitcontent.Length - 1
                If Splitcontent(i).Contains("TRACK 01 MODE2/2352") Then
                    content1 += Splitcontent(i) & Splitcontent(i + 1)
                    i = i + 2
                End If
                If Splitcontent(i).Contains("INDEX 01 00:00:00") Then
                    Splitcontent(i) = Replace(Splitcontent(i), "INDEX 01 00:00:00", "INDEX 01 00:02:00")
                End If
                content1 += Splitcontent(i)
            Next

            content = content1

            Dim objStreamWriter As StreamWriter
            objStreamWriter = File.CreateText(Path.GetDirectoryName(dtl_iso) & "\Redump_" & Path.GetFileName(dtl_iso))
            objStreamWriter.Write(content)
            objStreamWriter.Close()
        End Using
    End Sub

    Private Sub DaemonType()
        If File.Exists(wDir & "\DTCommandLine.exe") Then
            tProcess = "DTCommandLine.exe"
            Exit Sub
        End If

        If File.Exists(wDir & "\DTAgent.exe") = False Then
            tProcess = "DTLite.exe"
        Else
            tProcess = "DTAgent.exe"
        End If
    End Sub

    Private Sub DaemonUnmount()
        Select Case tProcess
            Case "DTLite.exe", "DTAgent.exe"
                Arg = "-unmount_all"
            Case "DTCommandLine.exe"
                Arg = "-U"
        End Select

        StartProcess()
        RippedName.Text = ""

        Dim mRem As MsgBoxResult

        If tProcess = "DTCommandLine.exe" Then
            mRem = MsgBox("Do you want to remove all Virtual Drive?" & vbCrLf &
                "Yes = Unmount/Remove" & vbCrLf &
            "No = Unmount only", vbYesNo + vbInformation, "Remove/Unmount all Virtual Drive...")
        End If

        If mRem = vbYes Then
            Arg = "-R"
            StartProcess()
            RippedName.Text = ""
            Load_Drive()
            UNI.Text = ""
        End If
    End Sub

    Private Sub DaemonMount()
        Select Case tProcess
            Case "DTLite.exe", "DTAgent.exe"
                Arg = "-mount dt, " & Chr(34) & dtl_iso & Chr(34)
            Case "DTCommandLine.exe"
                Arg = "-m -p " & Chr(34) & dtl_iso & Chr(34)
        End Select

        StartProcess()
    End Sub

    Private Sub UNI_SelectedIndexChanged(sender As Object, e As EventArgs) Handles UNI.SelectedIndexChanged
        Dim drive As New DriveInfo(UNI.Text)
        If drive.IsReady = True Then
            EjectUnmountDriveToolStripMenuItem.Enabled = True
            RippedName.Text = drive.VolumeLabel
            If RippedName.Text = "" And String.IsNullOrEmpty(dtl_iso) = False Then RippedName.Text = (Path.GetFileNameWithoutExtension(dtl_iso)).Trim
            SetIdCd()
        Else
            EjectUnmountDriveToolStripMenuItem.Enabled = False
            RippedName.Text = ""
            DVDBrand = ""
            MsgBox("The Drive " & UNI.Text & " is empty!", vbOKOnly + vbCritical, "Drive Empty")
            ToolTip1.SetToolTip(Me.UNI, "")
        End If
    End Sub

    Private Sub UNI_TextChanged(sender As Object, e As EventArgs) Handles UNI.TextChanged
        Dim drive As New DriveInfo(UNI.Text)
        If drive.IsReady = True Then
            RippedName.Text = drive.VolumeLabel
            SetIdCd()
        Else
            RippedName.Text = ""
        End If
    End Sub

    Private Sub SetIdCd()
        Dim CdName() As String
        Dim Brand() As String
        DVDBrand = ""
        'Dim count As Integer = 0
        For Each line As String In File.ReadLines(Application.StartupPath & "\DeviceName")
            If line.Substring(0, 2) = (UNI.Text) Then
                DVDBrand = line.Substring(2, line.Length - 2).Trim
                ToolTip1.SetToolTip(Me.UNI, Replace(line, UNI.Text, "").Trim)
                CdName = line.Split(": ")
            End If
        Next
        Brand = CdName(1).Trim.Split(" ")

        Dim CdId() As String
        For Each line As String In File.ReadLines(Application.StartupPath & "\DeviceList")
            If line.Contains(Brand(1)) Then
                CdId = line.Split(" : ")
                CdBus = CdId(0)
                'If count = UNI.SelectedIndex Then Exit Sub
                'count = count + 1
            End If
        Next line

    End Sub

    Public Sub contr_os()
        Dim arch = System.Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE", EnvironmentVariableTarget.Machine)
        If arch = "AMD64" Then
            c_os = "64"
        Else
            c_os = "32"
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        StartProcess()
    End Sub

    Private Sub RIP_Click(sender As Object, e As EventArgs) Handles RIP.Click
        If RippedName.Text.Trim = "" Or OutputPath.Text = "" Then
            MsgBox("Select input cue and output path", vbOKOnly + vbCritical, "Select input and output")
            Exit Sub
        End If

        ErrorAbort = 0
        PitStop = False
        Abort = False

        If BackgroundWorker1.IsBusy = True Then
            killAbort()
            Exit Sub
        End If

        If TypeRIP.Checked = False Then
            If File.ReadAllLines(dtl_iso).Length < 5 Then
                MsgBox("Nothing to convert in this file!", MessageBoxButtons.OK + MessageBoxIcon.Exclamation, "Not necessary conversion")
                Exit Sub
            End If
        End If

        If Directory.Exists(OutputPath.Text & RippedName.Text) = False Then My.Computer.FileSystem.CreateDirectory(OutputPath.Text & RippedName.Text)
        If File.Exists(OutputPath.Text & RippedName.Text & "\Lba.txt") = True Then My.Computer.FileSystem.DeleteFile(OutputPath.Text & RippedName.Text & "\Lba.txt")

        task = 0
        If TypeRIP.Checked = True Then
            If UNI.Text = "" Or CdBus = "" Then MsgBox("No device selected/detected!", MsgBoxStyle.Exclamation + vbOKOnly, "No Device") : Exit Sub
            CdrDao()
        Else
            If RebuildCUE.Checked = False Then
                If RadioButton6.Checked = True Then
                    bin2iso()
                ElseIf RadioButton5.Checked = True Then
                    bchunk()
                ElseIf RadioButton4.Checked = True Then
                    PStart = Date.Now
                    LogOut.Clear()
                    ExtractWithHawk()
                    task = 0
                End If
            Else
                'rebuild go here
            End If
        End If

        If RadioButton4.Checked = False Then
            LogOut.Clear()
            PStart = Date.Now
        End If

        BackgroundWorker1.RunWorkerAsync()
        BlockAll()
    End Sub

    Private Sub killAbort()
        Abort = True
        BackgroundWorker1.CancelAsync()
        BackgroundWorker1.Dispose()
    End Sub

    Private Sub PlayRandom()

        'Dim a As New MediaPlayer.MediaPlayer
        'Dim rnd1 As New Random()
        'Dim rnd As Integer = rnd1.Next(1, 16)

        'a.FileName = ((Application.StartupPath & "\Sound\" & (rnd.ToString) & ".mp3"))
        'a.Play()

        TaskEnd = "Done."
        tProcess = Application.StartupPath & "\Converter\sox.exe"
        'Arg = " " & (Application.StartupPath & "\Sound\" & (rnd.ToString) & ".mp3") & " -d"
        Arg = " " & Chr(34) & (Application.StartupPath & "\Sound\" & TSound & ".ogg") & Chr(34) & " -d"
        StartProcess()

        'My.Computer.Audio.Play(Application.StartupPath & "\Sound\9.wav")
        'My.Computer.Audio.Play(Application.StartupPath & "\Sound\" & Chr(rnd) & ".wma")
        'My.Computer.Audio.Stop()
        Arg = ""
    End Sub

    Private Sub PopulateList()
        ListAddsFile.Items.Clear()

        Dim InfoPath As String = ""
        If RebuildCUE.Checked = False Then
            InfoPath = OutputPath.Text & RippedName.Text
        Else
            InfoPath = Path.GetDirectoryName(dtl_iso)
        End If

        If RebuildCUE.Checked = True Then ExtRip = ".*"

        Dim ExPath As New IO.DirectoryInfo(InfoPath)
        Dim ExFile() As IO.FileInfo
        Dim ExFileOnFolder As IO.FileInfo
        ExFile = ExPath.GetFiles("*" & ExtRip)

        Dim ExtractAudioFromCue As String = CueExtInside(dtl_iso)

        Try
            For Each ExFileOnFolder In ExFile
                If ExtRip = ".*" Then
                    Dim filetypeonfolder = LCase(ExFileOnFolder.Extension)
                    If filetypeonfolder = ExtractAudioFromCue Or filetypeonfolder = ".iso" Or filetypeonfolder = ".bin" Then 'And filetypeonfolder <> ".bin"
                        ListAddsFile.Items.Add(ExFileOnFolder.Name)
                    End If
                Else
                    ListAddsFile.Items.Add(ExFileOnFolder.Name)
                End If

            Next

            'If CueMode.Text = "MODE1/2048 [PC-CD | PCFX]" Then PatchFile()

            ListAddsFile.SelectedIndex = 0
            percentage = ProgressBar1.Maximum / ListAddsFile.Items.Count
            ProgressBar1.Value = 0
            'LogOut.Clear()

            If PitStop = True Then Exit Sub
            If RippedName.Text <> Path.GetFileNameWithoutExtension(dtl_iso) Then RenameAllFile()
        Catch
        End Try

    End Sub

    Private Function CueExtInside(pathcue As String)
        Dim righe As String() = File.ReadAllLines(pathcue)
        Dim result() As String
        For i = 0 To righe.Length - 1
            If UCase(righe(i)).Contains("FILE ") Then
                If righe(i).Contains("""") Then
                    result = righe(i).Split("""")
                Else
                    result = righe(i).Split(" ")
                End If
                Dim cueext = LCase(Path.GetExtension(Replace(result(1).Trim, """", "")))
                If cueext = ".iso" Or cueext = ".bin" Then
                Else
                    Return (cueext)
                    Exit For
                End If
            End If
        Next
    End Function

    Private Sub RenameAllFile()
        Dim count As Integer

        count = 0
        Dim di As IO.DirectoryInfo = New IO.DirectoryInfo(OutputPath.Text & RippedName.Text)
        For Each File As IO.FileInfo In di.GetFiles()
            count = count + 1
            If File.Name = RippedName.Text & " " & count.ToString("D2") & File.Extension Then
                'File.Delete()
            End If
        Next

        Dim ExPath As New IO.DirectoryInfo(OutputPath.Text & RippedName.Text)
        Dim ExFile() As IO.FileInfo
        Dim ExFileOnFolder As IO.FileInfo

        count = 0
        ExFile = ExPath.GetFiles("*.*")
        Try
            For Each ExFileOnFolder In ExFile
                Select Case (ExFileOnFolder.Extension)
                    Case ".iso", ".wav"
                        count = count + 1
                        My.Computer.FileSystem.RenameFile(ExFileOnFolder.FullName, RippedName.Text & " " & count.ToString("D2") & ExFileOnFolder.Extension)
                        LogOut.AppendText(vbCrLf & ExFileOnFolder.Name & " --RENAMED IN-- " & RippedName.Text & " " & count.ToString("D2") & ExFileOnFolder.Extension)
                        LogOut.ScrollToCaret()
                End Select
            Next
            LogOut.AppendText(vbCrLf & vbCrLf & "All file renamed!")
            LogOut.ScrollToCaret()
            PitStop = True
            ExtRip = ".wav"
            PopulateList()
        Catch
        End Try
    End Sub

    Private Sub CdrDao()
        TaskEnd = "Reading of toc and track data finished successfully."
        wDir = Application.StartupPath & "\Converter"
        tProcess = Application.StartupPath & "\Converter\cdrdao.exe"

        Dim paranoid, CDatabase As String
        If Paranoia.Checked = True Then paranoid = "--paranoia-mode 2 " Else paranoid = ""
        If CDDB.Checked = True Then CDatabase = "--with-cddb " Else CDatabase = ""

        Arg = "read-cd " & CDatabase & "--datafile " & Chr(34) & OutputPath.Text & RippedName.Text & "\" & RippedName.Text & ".bin" & Chr(34) &
        " --driver generic-mmc:0x20000 --device " & CdBus & " --read-raw " & paranoid & Chr(34) & OutputPath.Text & RippedName.Text & "\" & RippedName.Text & ".toc" & Chr(34)

    End Sub

    Private Sub Toc2Cue()

        TaskEnd = "(even with cdrdao itself)."
        wDir = Application.StartupPath & "\Converter"
        tProcess = Application.StartupPath & "\Converter\toc2cue.exe"

        Arg = Chr(34) & OutputPath.Text & RippedName.Text & "\" & RippedName.Text & ".toc" & Chr(34) & " " & Chr(34) & OutputPath.Text & RippedName.Text & "\" & RippedName.Text & ".cue" & Chr(34)

    End Sub

    Private Sub bin2iso()

        TaskEnd = "001024"
        wDir = Application.StartupPath & "\Converter"
        tProcess = Application.StartupPath & "\Converter\bin2iso.exe"

        Arg = Chr(34) & dtl_iso & Chr(34) & " " & Chr(34) & OutputPath.Text & RippedName.Text & Chr(34) & " -nwg"
    End Sub

    Private Sub bchunk()
        TaskEnd = "End of Conversion"
        wDir = Application.StartupPath & "\Converter"
        tProcess = Application.StartupPath & "\Converter\bchunk.exe"

        'Dim CueType As String
        'If CDef.Checked = True Then CueType = ""
        'If CRaw.Checked = True Then CueType = "-r "
        'If CPsx.Checked = True Then CueType = "-r -p "

        Arg = " -r -w " & Chr(34) & FileBin & Chr(34) & " " & Chr(34) & dtl_iso & Chr(34) & " " & Chr(34) & OutputPath.Text & RippedName.Text & "\" & RippedName.Text & " " & Chr(34)
    End Sub

    Private Sub DetectByDiscTools1()

        Try
            Dim Ddisc = DiscInspector.ScanDisc(dtl_iso)
            ResultDisc = Ddisc.DiscTypeString
            'Dim session As String = Ddisc.DiscStructure.Sessions.Count
            Dim mode As String = Replace(Ddisc.DiscViewString, "DiscStreamView_", "")
            mode = UCase(Replace(mode, "_", "/"))
            'CueMode.Text = mode

            Select Case ResultDisc
                Case "PCEngineCD", "PCFX"
                    CueMode.Text = "MODE1/2048 [PC-CD | PCFX]"
                Case "SonyPSX"
                    CueMode.Text = "MODE2/2352 [PSX]"
                Case "SegaSaturn", "SegaCD"
                    CueMode.Text = "MODE1/2352 [SATURN]"
                Case Else
                    CueMode.Text = "MODE1/2048 [OTHER]"
            End Select
        Catch
            CueMode.Text = "MODE1/2048 [OTHER]"
        End Try

    End Sub

    Private Sub DetectByDiscTools()

        Try
            Dim Ddisc = DiscInspector.ScanDiscQuickNoCorrection(dtl_iso)
            ResultDisc = Ddisc.DiscTypeString

            DiscName = Ddisc.Data.GameTitle
            DiscMode = Replace(Ddisc.DiscViewString, "DiscStreamView_", "")
            DiscMode = UCase(Replace(DiscMode, "_", "/"))
            DiscMode = Replace(DiscMode, "FORM1/", "")

            CueMode.Text = UCase(DiscMode) & " (" & ResultDisc & ")"
        Catch
            CueMode.Text = "MODE1/2048 (Generic)"
        End Try

    End Sub

    Private Sub GetBinaryFromCue()
        If dtl_iso Is Nothing Then Exit Sub
        IsRedump = False
        Dim righe As String() = File.ReadAllLines(dtl_iso)
        Dim result As String
        Dim splitRighe() As String
        Dim countbin As Integer = 0

        For i = 0 To righe.Length - 1
            If UCase(righe(i)).Contains("BINARY") And righe(i).Contains(Chr(34)) Then
                countbin += 1
                splitRighe = righe(i).Split(Chr(34))
                If File.Exists(Path.Combine(Path.GetDirectoryName(dtl_iso), splitRighe(1))) And LCase(Path.GetExtension(splitRighe(1))) = ".bin" Then
                    If countbin = 1 Then result = splitRighe(1)
                End If
                'Exit For
            End If
        Next

        If countbin > 1 Then IsRedump = True : Exit Sub
        If result = "" Then Exit Sub

        'Dim word2 As String
        'Dim startPosition As Integer
        'startPosition = result.IndexOf(Chr(34)) + 1
        'word2 = result.Substring(startPosition, result.IndexOf(Chr(34), startPosition) - startPosition)
        'FileBin = Replace(dtl_iso, Path.GetFileName(dtl_iso), "") & word2

        FileBin = Path.Combine(Path.GetDirectoryName(dtl_iso), result)
    End Sub

    Private Sub VBR_CheckedChanged(sender As Object, e As EventArgs) Handles VBR.CheckedChanged
        Select Case Format.Text
            Case "mp3"
                If VBR.Checked = True Then
                    Bitrate.Enabled = False
                    QVBR.Minimum = 1
                    QVBR.Value = 4
                Else
                    Bitrate.Enabled = True
                    QVBR.Minimum = 0
                    QVBR.Value = 2
                End If
            Case "mp4"
                If VBR.Checked = True Then
                    QVBR.Enabled = True
                    Bitrate.Enabled = False
                Else
                    QVBR.Enabled = False
                    Bitrate.Enabled = True
                End If
            Case "aac"
                If VBR.Checked = True Then
                    QVBR.Enabled = True
                    Bitrate.Enabled = False
                Else
                    QVBR.Enabled = False
                    Bitrate.Enabled = True
                End If
            Case "opus"
                QVBR.Enabled = True
            Case Else
                If VBR.Checked = True Then
                    QVBR.Enabled = True
                Else
                    QVBR.Enabled = False
                End If
        End Select

    End Sub

    Private Sub Format_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Format.SelectedIndexChanged
        Select Case Format.Text
            Case "aac"
                Normalize.Enabled = False
                Resampling.Enabled = False
                Bitrate.Enabled = True
                VBR.Checked = False
                VBR.Enabled = True
                QVBR.Enabled = False
                QVBR.Maximum = 500
                QVBR.Minimum = 1
                QVBR.Value = 10
                ToolTip1.SetToolTip(QVBR, "Quality level at variable data rate <1-500> (VBR)" & vbCrLf &
                    "Default quality 10 is around 120 Bitrate(kbit/s)")
            Case "wav"
                Resampling.Enabled = False
                Bitrate.Enabled = False
                VBR.Enabled = False
                QVBR.Enabled = False
                Normalize.Enabled = False
            Case "mp3"
                Normalize.Enabled = True
                Resampling.Enabled = True
                Bitrate.Enabled = True
                VBR.Enabled = True
                VBR.Checked = False
                QVBR.Enabled = True
                QVBR.Maximum = 9
                QVBR.Minimum = 0
                QVBR.Value = 2
            Case "mp4"
                Normalize.Enabled = False
                Resampling.Enabled = False
                Bitrate.Enabled = True
                VBR.Enabled = True
                VBR.Checked = False
                QVBR.Enabled = True
                QVBR.Maximum = 95
                QVBR.Minimum = 0
                QVBR.Value = 50
                ToolTip1.SetToolTip(QVBR, "Quality level at variable data rate <0-95> (VBR)" & vbCrLf &
                                    "Quality 50 is around 160 Bitrate(kbit/s)")
            Case "ape"
                Normalize.Enabled = False
                Resampling.Enabled = False
                Bitrate.Enabled = False
                VBR.Enabled = True
                VBR.Checked = True
                QVBR.Enabled = True
                QVBR.Minimum = 1
                QVBR.Maximum = 5
                QVBR.Value = 2
                ToolTip1.SetToolTip(QVBR, "Compress 1-5 (fastest to insane, default is 2 normal)")
            Case "mpc"
                Normalize.Enabled = False
                Resampling.Enabled = False
                Bitrate.Enabled = False
                VBR.Enabled = True
                VBR.Checked = True
                QVBR.Enabled = True
                QVBR.Minimum = 0
                QVBR.Maximum = 10
                QVBR.Value = 5
                ToolTip1.SetToolTip(QVBR, "By default the encoding quality level is 5 (which gives an encoded rate of approx. 160/170kbps)," & vbCrLf &
                                    "but this can be changed using a number from 0 to 10" & vbCrLf & "0 = highest compression/lowest quality" & vbCrLf & "10 = lowest compression, highest quality")
            Case "ogg"
                Normalize.Enabled = True
                Resampling.Enabled = True
                Bitrate.Enabled = False
                VBR.Enabled = False
                VBR.Checked = False
                QVBR.Enabled = True
                QVBR.Maximum = 10
                QVBR.Minimum = -1
                QVBR.Value = 3
                ToolTip1.SetToolTip(QVBR, "By default the encoding quality level is 3 (which gives an encoded rate of approx. 112kbps)," & vbCrLf &
                                    "but this can be changed using a number from −1 to 10" & vbCrLf & "−1 = highest compression/lowest quality" & vbCrLf & "10 = lowest compression, highest quality")
            Case "opus"
                Normalize.Enabled = False
                Resampling.Enabled = True
                Bitrate.Enabled = True
                VBR.Checked = True
                VBR.Enabled = True
                QVBR.Enabled = True
                QVBR.Maximum = 10
                QVBR.Minimum = 0
                QVBR.Value = 10
                ToolTip1.SetToolTip(QVBR, "Set encoding complexity (0-10, default: 10 (slowest))")
            Case "flac"
                Normalize.Enabled = True
                Resampling.Enabled = True
                Bitrate.Enabled = False
                VBR.Enabled = False
                VBR.Checked = False
                QVBR.Enabled = True
                QVBR.Maximum = 8
                QVBR.Minimum = 0
                QVBR.Value = 8
                ToolTip1.SetToolTip(QVBR, "8 is the default compression level and gives the best (but slowest) compression;" & vbCrLf &
                                    "0 gives the least (but fastest) compression." & vbCrLf & "The compression level is selected using a number from 0 to 8.")
            Case "tak"
                Normalize.Enabled = False
                Resampling.Enabled = False
                Bitrate.Enabled = False
                VBR.Enabled = False
                VBR.Checked = False
                QVBR.Enabled = True
                QVBR.Maximum = 4
                QVBR.Minimum = 0
                QVBR.Value = 2
                ToolTip1.SetToolTip(QVBR, "Compress 0-4 (fastest to strongest, default is 2)")
        End Select
    End Sub

    Private Sub TypeRIP_CheckedChanged(sender As Object, e As EventArgs) Handles TypeRIP.CheckedChanged
        ControlTypeRip()
    End Sub

    Private Sub ControlTypeRip()
        RippedName.Text = ""
        If TypeRIP.Checked = True Then
            If RadioButton5.Checked = True Or RadioButton6.Checked = True Then
            Else
                RadioButton6.Checked = True
            End If
            'BlockAll()
            UNI.Enabled = True
            'RippedName.Enabled = True
            'OutputFolder.Enabled = True
            'OutputPath.Enabled = True
            DetectDaemon()
            'Load_Drive()
            CDDB.Enabled = True
            DumpOnly.Enabled = True
            Paranoia.Enabled = True
            RIP.BackgroundImage = My.Resources.ResourceManager.GetObject("rip")
        Else
            ClearAll()
            DumpOnly.Enabled = False
            DumpOnly.Checked = False
            CDDB.Enabled = False
            CDDB.Checked = False
            Paranoia.Enabled = False
            GroupBox1.Enabled = False
            SelectImage.Enabled = True
            SelectImage.BackgroundImage = My.Resources.ResourceManager.GetObject("CUE")
            UNI.Enabled = False
            GroupBox1.Enabled = False
            ToolTip1.SetToolTip(SelectImage, "Select a Cue file")
        End If
    End Sub

    Private Sub RefreshDriveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshDriveToolStripMenuItem.Click
        Load_Drive()
    End Sub

    Private Sub RebuildCUE_CheckedChanged(sender As Object, e As EventArgs) Handles RebuildCUE.CheckedChanged
        controlRebuild()
    End Sub

    Private Sub AfterConv_CheckedChanged(sender As Object, e As EventArgs) Handles AfterConv.CheckedChanged
        If AfterConv.Checked = True Then
            Panel1.Enabled = True
        Else
            Panel1.Enabled = False
        End If
    End Sub

    Private Sub controlRebuild()
        If RebuildCUE.Checked = True Then
            UNI.Enabled = False
            GroupBox4.Enabled = False
            GroupBox2.Enabled = False
            Format.Enabled = False
            TrimWave.Enabled = False
            CueMode.Enabled = False
            TypeRIP.Checked = False
            TrimWave.Checked = False
            TypeRIP.Enabled = False
            Format.Enabled = False
            Paranoia.Checked = False
            Format.Text = ""
        Else
            TypeRIP.Enabled = True
            CueMode.Enabled = True
            GroupBox4.Enabled = True
            GroupBox2.Enabled = True
            Format.Enabled = True
            TrimWave.Enabled = True
            Format.Enabled = True
            Format.Text = "wav"
        End If
    End Sub

    Private Sub CCSF_CheckedChanged(sender As Object, e As EventArgs) Handles CCSF.CheckedChanged
        If CCSF.Checked = True Then
            CLZMA.Enabled = True
        Else
            CLZMA.Enabled = False
        End If
    End Sub

    Private Sub Button1_Click_3(sender As Object, e As EventArgs) Handles Button1.Click
        FPismo.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        MakeCCD()
    End Sub

    Private Sub EjectUnmountDriveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EjectUnmountDriveToolStripMenuItem.Click
        Try
            If DVDBrand.Contains("DiscSoft Virtual") Then
                wDir = Environment.GetFolderPath(EnvType) & "\" & DAEPath
                DaemonType()
                DaemonUnmount()
            Else
                OpenDrawerCD("set CDAudio door open", Nothing, 127, 0)
            End If
        Catch
        End Try
    End Sub

    Private Sub OutputFolder_Click(sender As Object, e As EventArgs) Handles OutputFolder.Click
        Dim OutFolder As FolderBrowserDialog = New FolderBrowserDialog()
        If OutFolder.ShowDialog() = DialogResult.OK Then
            If Path.Combine(OutFolder.SelectedPath, RippedName.Text) = Path.GetDirectoryName(dtl_iso) Then
                MsgBox("Destination folder must be different from source CUE file folder" & vbCrLf &
                       "Change destination folder or modify output file name", vbOKOnly + MsgBoxStyle.Exclamation, "Select Different Destination")
                RippedName.Select()
                Exit Sub
            End If
            OutputPath.Text = (OutFolder.SelectedPath & "\").Trim
        End If
    End Sub

    Private Sub DumpOnly_CheckedChanged(sender As Object, e As EventArgs) Handles DumpOnly.CheckedChanged
        ControlDumpOnly()
    End Sub

    Private Sub ControlDumpOnly()
        If DumpOnly.Checked = True Then
            CueMode.Enabled = False
            GroupBox2.Enabled = False
            GroupBox4.Enabled = False
            Format.Enabled = False
            TrimWave.Enabled = False
            RebuildCUE.Enabled = False
            Paranoia.Enabled = True
        Else
            CueMode.Enabled = True
            GroupBox2.Enabled = True
            GroupBox4.Enabled = True
            Format.Enabled = True
            TrimWave.Enabled = True
            RebuildCUE.Enabled = True
        End If
    End Sub

    Public Sub StartProcess()
        task = task + 1
        Try
            With execute.StartInfo
                .UseShellExecute = False
                .RedirectStandardOutput = True
                .RedirectStandardError = True
                Select Case tProcess
                    Case "DTLite.exe", "DTAgent.exe", "DTCommandLine.exe" ', "shntool.exe"
                        .UseShellExecute = True
                        .RedirectStandardOutput = False
                        .RedirectStandardError = False
                End Select

                If TaskEnd = "Done." Then
                    .UseShellExecute = True
                    .RedirectStandardOutput = False
                    .RedirectStandardError = False
                End If

                .FileName = tProcess
                .Arguments = Arg
                .WorkingDirectory = wDir
                .CreateNoWindow = True
                .WindowStyle = ProcessWindowStyle.Hidden
            End With

            AppID = execute.Start()

            Dim noExit As Integer = 0

            If TaskEnd <> "Done." Then
                Dim StdOutput As StreamReader
                Select Case Path.GetFileNameWithoutExtension(tProcess)
                    Case "bchunk", "sox", "opusenc", "opusdec", "mpcenc", "mpcdec", "MAC", "bin2iso", "shntool",
                         "cdrdao", "toc2cue", "faac", "faad", "Takc", "neroAacEnc", "neroAacDec", "binmerge", "ptiso"
                        If Path.GetFileNameWithoutExtension(tProcess) = "sox" Then StdOutput = execute.StandardError
                        If Path.GetFileNameWithoutExtension(tProcess) = "MAC" Then StdOutput = execute.StandardError
                        If Path.GetFileNameWithoutExtension(tProcess) = "mpcenc" Then StdOutput = execute.StandardError
                        If Path.GetFileNameWithoutExtension(tProcess) = "mpcdec" Then StdOutput = execute.StandardError
                        If Path.GetFileNameWithoutExtension(tProcess) = "opusenc" Then StdOutput = execute.StandardError
                        If Path.GetFileNameWithoutExtension(tProcess) = "opusdec" Then StdOutput = execute.StandardError
                        If Path.GetFileNameWithoutExtension(tProcess) = "shntool" Then StdOutput = execute.StandardError
                        If Path.GetFileNameWithoutExtension(tProcess) = "cdrdao" Then StdOutput = execute.StandardError
                        If Path.GetFileNameWithoutExtension(tProcess) = "toc2cue" Then StdOutput = execute.StandardError
                        If Path.GetFileNameWithoutExtension(tProcess) = "bchunk" Then StdOutput = execute.StandardOutput
                        If Path.GetFileNameWithoutExtension(tProcess) = "bin2iso" Then StdOutput = execute.StandardOutput
                        If Path.GetFileNameWithoutExtension(tProcess) = "faac" Then StdOutput = execute.StandardOutput
                        If Path.GetFileNameWithoutExtension(tProcess) = "faad" Then StdOutput = execute.StandardError
                        If Path.GetFileNameWithoutExtension(tProcess) = "neroAacEnc" Then StdOutput = execute.StandardError
                        If Path.GetFileNameWithoutExtension(tProcess) = "neroAacDec" Then StdOutput = execute.StandardError
                        If Path.GetFileNameWithoutExtension(tProcess) = "Takc" Then StdOutput = execute.StandardOutput
                        If Path.GetFileNameWithoutExtension(tProcess) = "binmerge" Then StdOutput = execute.StandardOutput
                        If Path.GetFileNameWithoutExtension(tProcess) = "ptiso" Then StdOutput = execute.StandardOutput

                        Do
                            If Abort = True Then Exit Do
                            std_out = StdOutput.ReadLine().Trim & vbCrLf
                            'If std_out.Contains("ERROR: ") Then Exit Do
                            'If std_out.Contains("Failed to read toc-file") Then killAbort()

                            If std_out Is Nothing Then
                                Exit Do
                            Else
                                Invoke(MethodDelegateAddText, std_out)
                            End If
                            If std_out.Contains("ERROR: Cannot open toc file") Then
                                DumpOnly.Checked = True
                                MakeGenericCue()
                                ErrorAbort = 2
                                KillEmAll()
                            ElseIf std_out.Contains("ERROR:") Or std_out.Contains("Mode Error") Then
                                noExit = noExit + 1
                                If noExit > 50 Then
                                    ErrorAbort += 1
                                    KillEmAll()
                                End If
                            End If
                            'Exit Do
                        Loop Until std_out.Contains(TaskEnd)

                        StdOutput.Dispose()
                        StdOutput.Close()
                End Select
            End If

            execute.WaitForExit()
            execute.Close()
            BackgroundWorker1.CancelAsync()
        Catch ex As Exception
            If BackgroundWorker1.IsBusy = False Then
                MsgBox("Unable to start " & tProcess, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Process Error")
            End If
        End Try
    End Sub

    Private Sub MakeGenericCue()
        Dim strimg, C_TRACK As String

        Select Case ResultDisc
            Case "SonyPSX"
                C_TRACK = " MODE2/2352"
            Case "SegaSaturn"
                C_TRACK = " MODE1/2352"
            Case Else
                C_TRACK = " MODE1/2048"
        End Select
        strimg = "FILE " & Chr(34) & RippedName.Text & ".bin" & Chr(34) & " Binary" & vbCrLf & "  TRACK 01" & C_TRACK & vbCrLf & "    INDEX 01 00:00:00"
        My.Computer.FileSystem.WriteAllText(OutputPath.Text & RippedName.Text & "\" & RippedName.Text & ".cue", strimg, False)

        LogOut.AppendText(vbCrLf & vbCrLf & "Wrong TOC, generic CUE generated...")
        LogOut.ScrollToCaret()

        'MsgBox("Unable to create a TOC file, generic CUE generated, all operation will stopped", vbOKOnly + MsgBoxStyle.Exclamation, "Wrong TOC..")
    End Sub

    Private Delegate Sub DelegateAddText(ByVal str As String)

    Private MethodDelegateAddText As New DelegateAddText(AddressOf AddText)

    Private Sub AddText(ByVal str As String)

        'If str.Contains("Track ") Then
        'LbaRow = str
        'rLBA()

        'objStreamWriter = File.AppendText(OutputPath.Text & RippedName.Text & "\Lba.txt")
        'objStreamWriter.Write(str)
        'objStreamWriter.Close()
        'End If
        'If str.Contains(" 1: " & OutputPath.Text) Then
        'objStreamWriter.Dispose()
        'End If

        LogOut.AppendText(str)
        LogOut.ScrollToCaret()
    End Sub

    Private Sub LazyAss_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        multithread = (Environment.ProcessorCount)
        Load_Drive()
        If File.Exists(Application.StartupPath & "\Lazy") = False Then About.ShowDialog()
        If File.Exists(Application.StartupPath & "\Converter\neroAacEnc.exe") = False Then
            Format.Items.Remove("mp4")
        End If
    End Sub

    Private Sub Load_Drive()
        If File.Exists(Application.StartupPath & "\DeviceName") Then File.Delete(Application.StartupPath & "\DeviceName")

        Dim pStart As New System.Diagnostics.Process

        pStart.StartInfo.CreateNoWindow = True
        pStart.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        pStart.StartInfo.FileName = Application.StartupPath & "\Converter\DeviceName.exe"
        pStart.StartInfo.WorkingDirectory = Application.StartupPath & "\Converter\"
        pStart.Start()
        pStart.WaitForExit()

        pStart.StartInfo.CreateNoWindow = True
        pStart.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        pStart.StartInfo.FileName = Application.StartupPath & "\Converter\DetectDevice.bat"
        pStart.StartInfo.WorkingDirectory = Application.StartupPath & "\Converter\"
        pStart.Start()
        pStart.WaitForExit()

        contr_os()
        UNI.Items.Clear()

        For Each drive In IO.DriveInfo.GetDrives()
            If drive.DriveType = IO.DriveType.CDRom Then
                UNI.Items.Add(Replace(drive.ToString(), "\", ""))
            End If
        Next

    End Sub

    Private Sub DetectDaemon()
        EnvType = ""

        Dim DTLsub As Microsoft.Win32.RegistryKey
        Dim DTL As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software")
        Dim returnValue, returncvalue1 As String()
        returnValue = DTL.GetSubKeyNames

        If c_os = "32" Then
            EnvType = Environment.SpecialFolder.ProgramFilesX86
        ElseIf c_os = "64" Then
            EnvType = Environment.SpecialFolder.ProgramFiles
        End If

        For Each key In returnValue
            Select Case key
                Case "Disc Soft", "DT Soft"
                    DTLsub = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\" & key)
                    returncvalue1 = DTLsub.GetSubKeyNames
                    DAEPath = returncvalue1(0).ToString

                    Select Case DAEPath
                        Case "DAEMON Tools Lite"
                            If Directory.Exists(Environment.GetFolderPath(EnvType) & "\" & DAEPath) = True Then
                                RadioButton1.Enabled = True
                                RadioButton1.Checked = True
                                SelectImage.BackgroundImage = My.Resources.ResourceManager.GetObject("lite")
                            End If
                        Case "DAEMON Tools Pro"
                            If Directory.Exists(Environment.GetFolderPath(EnvType) & "\" & DAEPath) = True Then
                                RadioButton2.Enabled = True
                                RadioButton2.Checked = True
                                SelectImage.BackgroundImage = My.Resources.ResourceManager.GetObject("pro")
                            End If
                        Case "DAEMON Tools Ultra"
                            If Directory.Exists(Environment.GetFolderPath(EnvType) & "\" & DAEPath) = True Then
                                RadioButton3.Enabled = True
                                RadioButton3.Checked = True
                                SelectImage.BackgroundImage = My.Resources.ResourceManager.GetObject("ultra")
                            End If
                    End Select

                    'RIP.Enabled = True
                    GroupBox1.Enabled = True
                    SelectImage.Enabled = True
                    ToolTip1.SetToolTip(SelectImage, "Select a Virtual CD Image")
                Case Else
                    'RIP.Enabled = False
                    GroupBox1.Enabled = False
                    SelectImage.Enabled = False
            End Select
        Next

    End Sub

    Private Sub RadioDaemon()

        If RadioButton1.Checked = True Then
            DAEPath = "DAEMON Tools Lite"
            SelectImage.BackgroundImage = My.Resources.ResourceManager.GetObject("lite")
        ElseIf RadioButton2.Checked = True Then
            DAEPath = "DAEMON Tools Pro"
            SelectImage.BackgroundImage = My.Resources.ResourceManager.GetObject("pro")
        ElseIf RadioButton3.Checked = True Then
            DAEPath = "DAEMON Tools Ultra"
            SelectImage.BackgroundImage = My.Resources.ResourceManager.GetObject("ultra")
        End If
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        If Abort = True Then TaskEnd = vbCrLf : Exit Sub

        If BackgroundWorker1.IsBusy = False Then
            BackgroundWorker1.Dispose()

            If TypeRIP.Checked = True Then
                If File.Exists(OutputPath.Text & RippedName.Text & "\" & RippedName.Text & "_backup.cue") Then
                Else
                    If File.Exists(OutputPath.Text & RippedName.Text & "\" & RippedName.Text & ".cue") = False Then
                        Toc2Cue()
                        BackgroundWorker1.RunWorkerAsync()
                        Exit Sub
                    Else
                        Dim Sr As New StreamReader(OutputPath.Text & RippedName.Text & "\" & RippedName.Text & ".cue")
                        Dim Sw As New StreamWriter(OutputPath.Text & RippedName.Text & "\" & RippedName.Text & "_n.cue")
                        Dim Line As String = Sr.ReadLine
                        Do While Not Line Is Nothing
                            If Line.Contains("BINARY") Then Line = "FILE " & Chr(34) & RippedName.Text & ".bin" & Chr(34) & " BINARY"
                            Sw.WriteLine(Line)
                            Line = Sr.ReadLine
                        Loop
                        Sr.Close()
                        Sw.Close()

                        My.Computer.FileSystem.DeleteFile(OutputPath.Text & RippedName.Text & "\" & RippedName.Text & ".cue")
                        My.Computer.FileSystem.RenameFile(OutputPath.Text & RippedName.Text & "\" & RippedName.Text & "_n.cue", RippedName.Text & ".cue")
                        System.IO.File.Copy(OutputPath.Text & RippedName.Text & "\" & RippedName.Text & ".cue", OutputPath.Text & RippedName.Text & "\" & RippedName.Text & "_backup.cue", True)

                        If DumpOnly.Checked = True Then Finished() : Exit Sub
                        task = 0
                        dtl_iso = OutputPath.Text & RippedName.Text & "\" & RippedName.Text & ".cue"
                        GetBinaryFromCue()
                        DetectByDiscTools()
                        If RadioButton6.Checked = True Then
                            bin2iso()
                        Else
                            bchunk()
                        End If
                        BackgroundWorker1.RunWorkerAsync()
                        Exit Sub
                    End If
                End If
            End If

            If Format.Text = "wav" Then
                ExtRip = ".wav"
                PopulateList()
                TrimAudio()
                CreateCue()
                Finished()
                Exit Sub
            End If

            Select Case task
                Case 0
                    ClearAll()
                Case 100000
                    Finished()
                Case 1000
                    My.Computer.FileSystem.DeleteFile(OutputPath.Text & RippedName.Text & "\" & ListAddsFile.SelectedItem)
                    CreateCue()
                    Finished()
                Case Is = 1
                    If RebuildCUE.Checked = False Then ExtRip = ".wav"
                    PopulateList()
                    TrimAudio()
                    AudioConvert()
                Case Is >= 2
                    If RebuildCUE.Checked = False Then My.Computer.FileSystem.DeleteFile(OutputPath.Text & RippedName.Text & "\" & ListAddsFile.SelectedItem)
                    AudioConvert()
            End Select
        End If

    End Sub

    Private Sub Finished()

        If ErrorAbort > 1 Then Exit Sub

        If Panel1.Enabled = True Then
            If CZIP.Checked = True Then
                MakeZip()
            ElseIf CCSF.Checked = True Then
                MakeCfs()
            End If
        End If

        Dim DI As New IO.DirectoryInfo(Path.Combine(OutputPath.Text, RippedName.Text))
        Try
            If DI.GetFiles.GetLength(0) < 2 Then ErrorAbort = 2 : DefError() : Exit Sub
        Catch
        End Try
        'TSound = "Yoolaiyoleihee"
        TSound = "Success"
        'CreateObject("WScript.Shell").Popup("Conversion Done!", 3, "Job Completed...")
        PlayRandom()
        Dim elapsed As Date = Date.Now
        Dim duration As TimeSpan = elapsed.Subtract(PStart)
        LogOut.AppendText(vbCrLf & vbCrLf & "Conversion Done!" & vbCrLf &
                          "Time Elapsed:  " & duration.Duration.ToString)
        If LogSave.Checked = True Then SaveLog()

        MsgBox("Conversion Done!", vbInformation + MsgBoxStyle.OkOnly)

        Try
            If DI.GetFiles.GetLength(0) > 4 Then
                If File.Exists(OutputPath.Text & RippedName.Text & "\" & RippedName.Text & "_backup.cue") And
                TypeRIP.Checked = True Then
                    Dim ConfDelete = MsgBox("Do you want to Delete temporany cue/bin file?", vbYesNo + vbInformation, "Delete temp image...")
                    If ConfDelete = vbYes Then
                        If DumpOnly.Checked = False Then
                            File.Delete(OutputPath.Text & RippedName.Text & "\" & RippedName.Text & ".bin")
                        End If
                        File.Delete(OutputPath.Text & RippedName.Text & "\" & RippedName.Text & "_backup.cue")
                        File.Delete(OutputPath.Text & RippedName.Text & "\" & RippedName.Text & ".toc")
                    End If
                End If
            End If
        Catch
        End Try

        LogOut.ScrollToCaret()
        ClearAll()
        RebuildCUE.Enabled = True
        ControlTypeRip()
        ControlDumpOnly()
        controlRebuild()

    End Sub

    Private Sub MakeZip()
        Dim startPath As String = Path.Combine(OutputPath.Text, RippedName.Text)
        Dim zipPath As String = Path.Combine(OutputPath.Text, RippedName.Text & ".zip")
        ZipFile.CreateFromDirectory(startPath, zipPath, CompressionLevel.Optimal, False)
    End Sub

    Private Sub MakeCfs()
        TaskEnd = "Image creation complete."
        wDir = Application.StartupPath & "\Converter"
        tProcess = Application.StartupPath & "\Converter\ptiso.exe"

        Dim startPath As String = Path.Combine(OutputPath.Text, RippedName.Text)
        Dim csfPath As String = Path.Combine(OutputPath.Text, RippedName.Text & ".cfs")

        Dim Pcfs As String

        If CLZMA.Checked = True Then
            Pcfs = "-z lzma "
        Else
            Pcfs = "-z zip "
        End If

        Arg = " create " & Pcfs & Chr(34) & csfPath & Chr(34) & " " & Chr(34) & startPath & Chr(34)
        StartProcess()
    End Sub

    Private Sub SaveLog()
        If File.Exists(Application.StartupPath & "\LogOut.txt") Then File.Delete(Application.StartupPath & "\LogOut.txt")
        LogOut.SaveFile(Application.StartupPath & "\LogOut.txt",
            RichTextBoxStreamType.PlainText)
    End Sub

    Private Sub ClearAll()
        Format.Enabled = True
        UNI.Enabled = True
        TypeRIP.Enabled = True
        GroupBox2.Enabled = True
        GroupBox3.Enabled = True
        GroupBox4.Enabled = True
        OutputFolder.Enabled = True
        SelectImage.Enabled = True
        RippedName.Enabled = True
        OutputPath.Enabled = True
        ListAddsFile.Items.Clear()
        'LogOut.Clear()
        ProgressBar1.Value = 0
        ToolTip1.SetToolTip(RIP, "Start the conversion")
        RIP.BackgroundImage = My.Resources.ResourceManager.GetObject("rip")
        RebuildCUE.Enabled = True
    End Sub

    Public Sub BlockAll()
        Format.Enabled = False
        UNI.Enabled = False
        TypeRIP.Enabled = False
        GroupBox2.Enabled = False
        GroupBox3.Enabled = False
        GroupBox4.Enabled = False
        OutputFolder.Enabled = False
        SelectImage.Enabled = False
        RippedName.Enabled = False
        OutputPath.Enabled = False
        RIP.BackgroundImage = My.Resources.ResourceManager.GetObject("cancel")
        ToolTip1.SetToolTip(RIP, "Stop the task")
    End Sub

    Private Sub TrimAudio()

        If TrimWave.Checked = True Then
            For i = 0 To ListAddsFile.Items.Count - 1
                TaskEnd = "100% OK"
                wDir = Application.StartupPath & "\Converter"
                tProcess = Application.StartupPath & "\Converter\shntool.exe"
                Arg = "trim -O always " & Chr(34) & OutputPath.Text & RippedName.Text & "\" & ListAddsFile.Items(i) & Chr(34)
                StartProcess()
            Next

            For i = 0 To ListAddsFile.Items.Count - 1
                Dim trimmedfile As String = OutputPath.Text & RippedName.Text & "\" & ListAddsFile.Items(i)
                If File.Exists(Path.GetDirectoryName(trimmedfile) & "\" & Path.GetFileNameWithoutExtension(trimmedfile) & "-trimmed.wav") Then
                    My.Computer.FileSystem.DeleteFile(trimmedfile)
                    My.Computer.FileSystem.RenameFile(Path.GetDirectoryName(trimmedfile) & "\" & Path.GetFileNameWithoutExtension(trimmedfile) & "-trimmed.wav", Path.GetFileName(trimmedfile))
                End If
            Next
        End If

        task = 1
    End Sub

    Private Sub AudioConvert()
        Dim compression, norm, audioin, audiout As String

        wDir = (Application.StartupPath & "\Converter")

        ProgressBar1.Value = percentage * (ListAddsFile.SelectedIndex + 1)
        If task > ListAddsFile.Items.Count Then
            BackgroundWorker1.CancelAsync()
            If RebuildCUE.Checked = False Then
                CreateCue()
            Else
                CueRebuild()
            End If
            Finished()
            Exit Sub
        End If

        ListAddsFile.SelectedIndex = task - 1

        If RebuildCUE.Checked = False Then
            audioin = OutputPath.Text & RippedName.Text & "\" & ListAddsFile.SelectedItem
            audiout = Chr(34) & Replace(audioin, Path.GetExtension(audioin), "." & Format.Text) & Chr(34)

            If Normalize.Checked = True Then norm = "--norm" Else norm = ""
            Select Case Format.Text
                Case "aac"
                    TaskEnd = "(100%)"
                    tProcess = Application.StartupPath & "\Converter\faac.exe"
                    If VBR.Checked = True Then
                        compression = "-q " & QVBR.Value & "0"
                    Else
                        compression = "-b " & Bitrate.Text
                    End If
                    Arg = compression & " -c " & Resampling.Text & " --overwrite --mpeg-vers 4 -o " & audiout & " " & Chr(34) & audioin & Chr(34)
                Case "ape"
                    TaskEnd = "Success..."
                    tProcess = Application.StartupPath & "\Converter\MAC.exe"
                    Arg = Chr(34) & audioin & Chr(34) & " " & audiout & " -c" & QVBR.Value & "000"
                Case "ogg", "flac"
                    TaskEnd = "Processed by SoX"
                    tProcess = Application.StartupPath & "\Converter\sox.exe"
                    compression = QVBR.Value
                    Arg = norm & " -V " & Chr(34) & audioin & Chr(34) & " -r " & Resampling.Text & " -C " & compression & " " & audiout
                Case "tak"
                    TaskEnd = "* real time"
                    tProcess = Application.StartupPath & "\Converter\Takc.exe"
                    compression = QVBR.Value
                    Arg = "-e -p" & QVBR.Value & " -overwrite " & Chr(34) & audioin & Chr(34) & " " & audiout
                Case "mp3"
                    TaskEnd = "Processed by SoX"
                    tProcess = Application.StartupPath & "\Converter\sox.exe"
                    If VBR.Checked = True Then
                        compression = "-" & QVBR.Value
                    Else
                        compression = Bitrate.Text & "." & QVBR.Value
                    End If
                    Arg = norm & " -V " & Chr(34) & audioin & Chr(34) & " -r " & Resampling.Text & " -C " & compression & " " & audiout
                Case "mp4"
                    TaskEnd = " seconds..."
                    tProcess = Application.StartupPath & "\Converter\neroAacEnc.exe"
                    If VBR.Checked = True Then
                        compression = "-q 0." & CInt(QVBR.Value).ToString("D2")
                    Else
                        compression = "-cbr " & Bitrate.Text & "000"
                    End If
                    Arg = compression & " -if " & Chr(34) & audioin & Chr(34) & " -of " & audiout
                Case "mpc"
                    TaskEnd = "100.0"
                    tProcess = Application.StartupPath & "\Converter\mpcenc.exe"
                    Arg = "--verbose --overwrite --quality " & QVBR.Value & ".00 " & Chr(34) & audioin & Chr(34) & " " & audiout
                Case "opus"
                    TaskEnd = "(container+metadata)"
                    tProcess = Application.StartupPath & "\Converter\opusenc.exe"
                    If VBR.Checked = True Then
                        compression = " --vbr"
                    Else
                        compression = " --hard-cbr"
                    End If
                    Arg = "--raw-rate " & Resampling.Text & " --bitrate " & Bitrate.Text & compression & " --comp " & QVBR.Value & " " & Chr(34) & audioin & Chr(34) & " " & audiout
                Case "wav"
                    Exit Sub
            End Select
        Else
            audioin = Path.GetDirectoryName(dtl_iso) & "\" & ListAddsFile.SelectedItem

            Dim outFaudio As String = ""
            If LCase(Path.GetExtension(ListAddsFile.SelectedItem)) = ".wav" Then
                outFaudio = ".bin"
            Else
                outFaudio = ".wav"
            End If

            audiout = OutputPath.Text & RippedName.Text & "\" & Path.GetFileNameWithoutExtension(ListAddsFile.SelectedItem) & outFaudio
            Select Case LCase(Path.GetExtension(ListAddsFile.SelectedItem))
                Case ".iso", ".bin"
                    Dim overcopy As MsgBoxResult
                    If File.Exists(OutputPath.Text & RippedName.Text & "\" & ListAddsFile.SelectedItem) Then
                        overcopy = MsgBox("File " & ListAddsFile.SelectedItem & " already exist" & vbCrLf &
                                          "Do you want to overwrite it?", vbOKCancel + MsgBoxStyle.Exclamation, "File already exist...")
                    Else
                        File.Copy(audioin, OutputPath.Text & RippedName.Text & "\" & ListAddsFile.SelectedItem, True)
                    End If
                    If overcopy = MsgBoxResult.Ok Then
                        File.Copy(audioin, OutputPath.Text & RippedName.Text & "\" & ListAddsFile.SelectedItem, True)
                    End If

                    tProcess = ""
                    Arg = ""
                Case ".wav"
                    TaskEnd = "Processed by SoX"
                    tProcess = Application.StartupPath & "\Converter\sox.exe"
                    'Arg = Chr(34) & audioin & Chr(34) & " --bits 16 --encoding signed-integer --endian little " & Chr(34) & audiout & Chr(34)
                    Arg = " -V " & Chr(34) & audioin & Chr(34) & " −t raw -L " & Chr(34) & audiout & Chr(34)
                Case ".aac"
                    TaskEnd = "real-time."
                    tProcess = Application.StartupPath & "\Converter\faad.exe"
                    Arg = "-o " & Chr(34) & audiout & Chr(34) & " " & Chr(34) & audioin & Chr(34)
                Case ".opus"
                    TaskEnd = "Decoding complete."
                    tProcess = Application.StartupPath & "\Converter\opusdec.exe"
                    Arg = Chr(34) & audioin & Chr(34) & " " & Chr(34) & audiout & Chr(34)
                Case ".ape"
                    TaskEnd = "Success..."
                    tProcess = Application.StartupPath & "\Converter\MAC.exe"
                    Arg = Chr(34) & audioin & Chr(34) & " " & Chr(34) & audiout & Chr(34) & " -d"
                Case ".mpc"
                    TaskEnd = "samples decoded"
                    tProcess = Application.StartupPath & "\Converter\mpcdec.exe"
                    Arg = Chr(34) & audioin & Chr(34) & " " & Chr(34) & audiout & Chr(34)
                Case ".mp3", ".ogg", ".flac"
                    TaskEnd = "Processed by SoX"
                    tProcess = Application.StartupPath & "\Converter\sox.exe"
                    Arg = " -V " & Chr(34) & audioin & Chr(34) & " " & Chr(34) & audiout & Chr(34) & " rate 44100"
                Case ".mp4"
                    TaskEnd = "*************************************************************"
                    tProcess = Application.StartupPath & "\Converter\neroAacDec.exe"
                    Arg = " -if " & Chr(34) & audioin & Chr(34) & " -of " & Chr(34) & audiout & Chr(34)
                Case ".tak"
                    TaskEnd = "* real time"
                    tProcess = Application.StartupPath & "\Converter\Takc.exe"
                    Arg = " -d -overwrite " & Chr(34) & audioin & Chr(34) & " " & Chr(34) & audiout & Chr(34)
            End Select
        End If

        BackgroundWorker1.RunWorkerAsync()
        If ListAddsFile.Items.Count = 1 Then task = 999
    End Sub

    Private Sub CueRebuild()
        Dim content As String = ""
        Dim replacer1, replacer2 As String

        Dim SplitCue() As String = Nothing
        For Each line As String In File.ReadLines(dtl_iso)
            If line.Contains("FILE ") Then
                If line.Split(" ").Count <= 3 And line.Contains("""") = False Then
                    SplitCue = line.Split(" ")
                Else
                    SplitCue = line.Split("""")
                End If
                Select Case LCase(Replace(Path.GetExtension(SplitCue(1)), """", ""))
                    Case ".iso", ".bin"
                    Case ".wav"
                        replacer1 = ".bin"
                        replacer2 = "BINARY"
                        Exit For
                    Case Else
                        replacer1 = ".wav"
                        replacer2 = "WAVE"
                        Exit For
                End Select
            End If
        Next line

        Using objStreamReader As New StreamReader(dtl_iso)
            content = objStreamReader.ReadToEnd()
            objStreamReader.Dispose()
            objStreamReader.Close()

            content = content.Replace(Replace(Path.GetExtension(SplitCue(1)), """", ""), replacer1)
            content = content.Replace(SplitCue(2).Trim, replacer2)

            Dim objStreamWriter As StreamWriter
            objStreamWriter = File.CreateText(OutputPath.Text & RippedName.Text & "\" & Path.GetFileName(dtl_iso))
            objStreamWriter.Write(content)
            objStreamWriter.Close()
        End Using
    End Sub

    Private Sub CreateCue()

        Dim TRACK, aPREGAP, bPREGAP, content, Extension As String, Aswitch, Bswitch, skip As Boolean

        Dim ExPath As New IO.DirectoryInfo(OutputPath.Text & RippedName.Text)
        Dim ExFile() As IO.FileInfo
        Dim ExFileOnFolder As IO.FileInfo

        ExFile = ExPath.GetFiles("*.*")

        aPREGAP = ""
        bPREGAP = ""
        ntrack = 0
        Aswitch = False
        Bswitch = False
        CheckPregap()

        Try

            If File.Exists(OutputPath.Text & RippedName.Text & "\" & RippedName.Text & ".cue") = True Then My.Computer.FileSystem.DeleteFile(OutputPath.Text & RippedName.Text & "\" & RippedName.Text & ".cue")

            For Each ExFileOnFolder In ExFile
                ntrack = ntrack + 1

                If Aswitch = True And aPREGAP = "" Then
                    aPREGAP = "    PREGAP 00:02:00" & vbCrLf
                Else : aPREGAP = ""
                End If

                If Bswitch = True And bPREGAP = "" Then
                    bPREGAP = "    PREGAP 00:03:00" & vbCrLf
                Else : bPREGAP = ""
                End If

                Dim indice As String = "00"
                Dim DoubleIso As Boolean

                Dim ByteExtract As String = "2048"
                If RadioButton4.Checked Then ByteExtract = "2352"

                Extension = LCase(Path.GetExtension(ExFileOnFolder.Name))
                Select Case Extension
                    Case ".iso"
                        Select Case ResultDisc
                            Case "SonyPSX"
                                TRACK = " BINARY" & vbCrLf & "  TRACK " & ntrack.ToString("D2") & " MODE2/2352" & vbCrLf & "    INDEX 01 00:00:00" & vbCrLf
                                If CountPgap = True Then Aswitch = True
                                Bswitch = False
                                aPREGAP = ""
                            Case "PCEngineCD", "PCFX"
                                TRACK = " BINARY" & vbCrLf & "  TRACK " & ntrack.ToString("D2") & " MODE1/" & ByteExtract & vbCrLf & bPREGAP & "    INDEX 01 00:00:00" & vbCrLf
                                Aswitch = True
                                Bswitch = False
                                aPREGAP = ""
                            Case "SegaSaturn"
                                If DoubleIso = False Then
                                    TRACK = " BINARY" & vbCrLf & "  TRACK " & ntrack.ToString("D2") & " MODE1/" & ByteExtract & vbCrLf & "    INDEX 01 00:00:00" & vbCrLf
                                    DoubleIso = True
                                    Dim Ciso As New IO.DirectoryInfo(OutputPath.Text & RippedName.Text)
                                    Dim IsoFile() As IO.FileInfo
                                    IsoFile = ExPath.GetFiles("*.iso")
                                    FileToAppend = ExFileOnFolder.Name
                                    If IsoFile.Count > 1 Then
                                        LogOut.AppendText(vbCrLf & vbCrLf & "Apply the patch to the file '" & FileToAppend & "'...")
                                        LogOut.ScrollToCaret()
                                        AppendEmpty()
                                    End If
                                Else
                                    TRACK = " BINARY" & vbCrLf & "  TRACK " & ntrack.ToString("D2") & " MODE2/" & ByteExtract & vbCrLf & "    INDEX 01 00:00:00" & vbCrLf
                                    DoubleIso = False
                                End If
                                Aswitch = True
                                Bswitch = False
                                aPREGAP = ""
                            Case Else
                                TRACK = " BINARY" & vbCrLf & "  TRACK " & ntrack.ToString("D2") & " MODE1/" & ByteExtract & vbCrLf & "    INDEX 01 00:00:00" & vbCrLf
                        End Select
                    Case ".aac", ".ape", ".mp3", ".mp4", ".mpc", ".ogg", ".opus", ".tak"
                        If ResultDisc = "SonyPSX" Then
                            If CountPgap = True Then aPREGAP = "    PREGAP 00:02:00" & vbCrLf
                            'If CountPgap = True Then
                            'If ntrack = 2 Then
                            'aPREGAP = "    PREGAP 00:02:00" & vbCrLf
                            'ElseIf ntrack > 2 Then
                            'aPREGAP = "    INDEX 00 00:00:00" & vbCrLf
                            'indice = "02"
                            'End If
                            'End If
                        End If
                        TRACK = " " & UCase(Replace(Extension, ".", "")) & vbCrLf & "  TRACK " & ntrack.ToString("D2") & " AUDIO" & vbCrLf & aPREGAP & "    INDEX 01 00:00:00" & vbCrLf
                        Bswitch = True
                        Aswitch = False
                        bPREGAP = ""
                    Case ".flac", ".wav"
                        If ResultDisc = "SonyPSX" Then
                            If CountPgap = True Then aPREGAP = "    PREGAP 00:02:00" & vbCrLf
                            'If CountPgap = True Then
                            'If ntrack = 2 Then
                            'aPREGAP = "    PREGAP 00:02:00" & vbCrLf
                            'ElseIf ntrack > 2 Then
                            'aPREGAP = "    INDEX 00 00:00:00" & vbCrLf
                            'indice = "02"
                            'End If
                            'End If
                        End If
                        TRACK = " WAVE" & vbCrLf & "  TRACK " & ntrack.ToString("D2") & " AUDIO" & vbCrLf & aPREGAP & "    INDEX 01 00:00:00" & vbCrLf
                        Bswitch = True
                        Aswitch = False
                        bPREGAP = ""
                    Case Else
                        ntrack = ntrack - 1
                        skip = True
                End Select

                If skip = False Then
                    objStreamWriter = File.AppendText(OutputPath.Text & RippedName.Text & "\" & RippedName.Text & ".cue")
                    content = "FILE " & Chr(34) & ExFileOnFolder.Name & Chr(34) & TRACK
                    objStreamWriter.Write(content)
                    objStreamWriter.Close()
                End If
                skip = False
            Next
            objStreamWriter.Dispose()
        Catch
        End Try

    End Sub

    Public Sub CheckPregap()
        If ResultDisc = "SonyPSX" Then
            If LCase(Path.GetExtension(dtl_iso)) = ".ccd" Or LCase(Path.GetExtension(dtl_iso)) = ".mds" Then CountPgap = True : Exit Sub
            Dim SplitCue() As String = Nothing
            For Each line As String In File.ReadLines(dtl_iso)
                If line.Contains("PREGAP ") Or IsRedump = True Then
                    CountPgap = True
                    Exit For
                Else
                    CountPgap = False
                End If
            Next line
        End If
    End Sub

    Private Sub rLBA()

        Dim TrackMode, Index, IndexA, IndexB, content, suffix As String

        Dim SplitLba(), _SplitLba_() As String
        SplitLba = Split(LbaRow, "    ")

        TrackMode = SplitLba(0)
        If SplitLba.Length = 2 Then Index = SplitLba(1) Else Index = SplitLba(2)

        If Index.Contains(" 01 ") Then
            _SplitLba_ = Split(Index, " 01 ")
            IndexA = _SplitLba_(0)
            IndexB = _SplitLba_(1)
        Else
            IndexA = Index
            IndexB = ""
        End If

        ntrack = ntrack + 1
        TrackMode = Replace(TrackMode.Trim, ntrack & ":", ntrack.ToString("D2"))
        Select Case True
            Case TrackMode.Trim.Contains("AUDIO")
                Select Case Format.Text
                    Case "aac", "ape", "mp3", "mpc", "ogg", "opus", "tak", "mp4"
                        suffix = UCase(Format.Text)
                    Case "flac", "wav"
                        suffix = UCase(Format.Text) & "E"
                End Select
                'content = "FILE " & Chr(34) & RippedName.Text & " " & ntrack.ToString("D2") & "." & Format.Text & Chr(34) & " " & suffix & vbCrLf
            Case TrackMode.Trim.Contains("MODE")
                content = "FILE " & Chr(34) & RippedName.Text & " " & ntrack.ToString("D2") & ".iso" & Chr(34) & "  BINARY" & vbCrLf
        End Select

        If IndexB.Trim <> "" Then IndexB = UCase("    INDEX 01 " & IndexB.Trim) & vbCrLf : Else IndexA = UCase("    INDEX " & IndexA.Trim) & vbCrLf
        If IndexA.Trim <> "" And IndexB.Trim <> "" Then IndexA = UCase("    INDEX " & IndexA.Trim) & vbCrLf

        content = content & "  " & UCase(TrackMode.Trim) & vbCrLf & IndexA & IndexB

        'MsgBox(TrackMode.Trim)
        'If Index00.Trim <> "" Then MsgBox(Index00.Trim)
        'If Index01.Trim <> "" Then MsgBox("01 " & Index01.Trim)

        objStreamWriter = File.AppendText(OutputPath.Text & RippedName.Text & "\" & RippedName.Text & "_LBA.cue")
        objStreamWriter.Write(content)
        objStreamWriter.Close()

    End Sub

    Private Sub PatchFile()

        Dim xA, xB, xC, xD As Integer

        Using str As New FileStream(OutputPath.Text & RippedName.Text & "\" & ListAddsFile.Items(0), FileMode.Open, FileAccess.Read)
            str.Seek(4, SeekOrigin.Begin)
            xA = str.ReadByte() - 48
            xB = str.ReadByte() - 9
            str.Seek(34, SeekOrigin.Current)
            xC = str.ReadByte() - 48
            xD = str.ReadByte() - 9
            str.Close()
        End Using

        Using str1 As New FileStream(OutputPath.Text & RippedName.Text & "\" & ListAddsFile.Items(0), FileMode.Open, FileAccess.Write)
            str1.Seek(4, SeekOrigin.Begin)
            str1.WriteByte(xA)
            str1.WriteByte(xB)
            str1.Seek(34, SeekOrigin.Current)
            str1.WriteByte(xC)
            str1.WriteByte(xD)
            str1.Close()
        End Using

    End Sub

    Private Sub AppendEmpty()
        Dim prova As String = OutputPath.Text & RippedName.Text & "\" & FileToAppend
        Using str As New FileStream(prova, FileMode.Open, FileAccess.Write)
            str.Seek(0, SeekOrigin.End)
            For i = 0 To 307199
                str.WriteByte(0)
            Next
            str.Close()
        End Using
    End Sub

    Public Sub KillEmAll()

        Dim myProcesses() As Process
        Dim myProcess As Process
        myProcesses = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(tProcess))
        For Each myProcess In myProcesses
            Try
                myProcess.CloseMainWindow()
                If ErrorAbort = 0 Then myProcess.WaitForExit()
                myProcess.Kill()
                myProcess.Close()
            Catch
            End Try
        Next

    End Sub

    Private Sub BackgroundWorker1_Disposed(sender As Object, e As EventArgs) Handles BackgroundWorker1.Disposed
        DefError()
    End Sub

    Private Sub DefError()
        If Abort = True Or ErrorAbort > 0 Then
            'TSound = "Ugh oooh"
            TSound = "Error"
            PlayRandom()
            Dim Cause As String = ""
            If ErrorAbort > 1 Then
                Cause = "Conversion Error..."
            Else
                Cause = "Stopped by User..."
            End If
            MsgBox("Operation aborted!", MessageBoxButtons.OK + MessageBoxIcon.Exclamation, Cause)
            TaskEnd = vbCrLf
            task = 0
            LogOut.AppendText(vbCrLf & vbCrLf & "<<- PROCESS  STOPPED  BY  USER OR A ERROR OCCURRED! ->>")
            LogOut.ScrollToCaret()
            If LogSave.Checked = True Then SaveLog()
            ClearAll()
        End If
    End Sub

End Class