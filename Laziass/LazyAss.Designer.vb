<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LazyAss
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla nell'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LazyAss))
        Me.UNI = New System.Windows.Forms.ComboBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RippedName = New System.Windows.Forms.TextBox()
        Me.ListAddsFile = New System.Windows.Forms.ListBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Format = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Normalize = New System.Windows.Forms.CheckBox()
        Me.Turbo = New System.Windows.Forms.CheckBox()
        Me.QVBR = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Bitrate = New System.Windows.Forms.ComboBox()
        Me.VBR = New System.Windows.Forms.CheckBox()
        Me.Resampling = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.OutputPath = New System.Windows.Forms.TextBox()
        Me.TypeRIP = New System.Windows.Forms.CheckBox()
        Me.LogOut = New System.Windows.Forms.RichTextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.RebuildCUE = New System.Windows.Forms.CheckBox()
        Me.TrimWave = New System.Windows.Forms.CheckBox()
        Me.CDDB = New System.Windows.Forms.CheckBox()
        Me.Paranoia = New System.Windows.Forms.CheckBox()
        Me.DumpOnly = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.VGap = New System.Windows.Forms.NumericUpDown()
        Me.CueMode = New System.Windows.Forms.ComboBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.LogSave = New System.Windows.Forms.CheckBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.RadioButton5 = New System.Windows.Forms.RadioButton()
        Me.RadioButton6 = New System.Windows.Forms.RadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.OutputFolder = New System.Windows.Forms.Button()
        Me.RIP = New System.Windows.Forms.Button()
        Me.SelectImage = New System.Windows.Forms.Button()
        Me.RefreshDriveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EjectUnmountDriveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.QVBR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.VGap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'UNI
        '
        Me.UNI.ContextMenuStrip = Me.ContextMenuStrip1
        Me.UNI.Enabled = False
        Me.UNI.FormattingEnabled = True
        Me.UNI.Location = New System.Drawing.Point(158, 14)
        Me.UNI.Name = "UNI"
        Me.UNI.Size = New System.Drawing.Size(58, 21)
        Me.UNI.Sorted = True
        Me.UNI.TabIndex = 32
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshDriveToolStripMenuItem, Me.EjectUnmountDriveToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(186, 48)
        '
        'RippedName
        '
        Me.RippedName.Location = New System.Drawing.Point(96, 41)
        Me.RippedName.Name = "RippedName"
        Me.RippedName.Size = New System.Drawing.Size(229, 20)
        Me.RippedName.TabIndex = 34
        Me.ToolTip1.SetToolTip(Me.RippedName, "You can change the suggested output file name")
        '
        'ListAddsFile
        '
        Me.ListAddsFile.Enabled = False
        Me.ListAddsFile.FormattingEnabled = True
        Me.ListAddsFile.Location = New System.Drawing.Point(12, 67)
        Me.ListAddsFile.Name = "ListAddsFile"
        Me.ListAddsFile.Size = New System.Drawing.Size(313, 173)
        Me.ListAddsFile.Sorted = True
        Me.ListAddsFile.TabIndex = 35
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 13)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "Output Name:"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(331, 297)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(126, 20)
        Me.ProgressBar1.TabIndex = 39
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(222, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "Format:"
        '
        'Format
        '
        Me.Format.FormattingEnabled = True
        Me.Format.Items.AddRange(New Object() {"aac", "ape", "flac", "mp3", "mp4", "mpc", "ogg", "opus", "tak", "wav"})
        Me.Format.Location = New System.Drawing.Point(267, 14)
        Me.Format.Name = "Format"
        Me.Format.Size = New System.Drawing.Size(58, 21)
        Me.Format.Sorted = True
        Me.Format.TabIndex = 41
        Me.Format.Text = "wav"
        Me.ToolTip1.SetToolTip(Me.Format, "Select Audio Conversion Format")
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButton3)
        Me.GroupBox1.Controls.Add(Me.RadioButton2)
        Me.GroupBox1.Controls.Add(Me.RadioButton1)
        Me.GroupBox1.Enabled = False
        Me.GroupBox1.Location = New System.Drawing.Point(331, 52)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(155, 51)
        Me.GroupBox1.TabIndex = 42
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Daemon Tool Version"
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Enabled = False
        Me.RadioButton3.Location = New System.Drawing.Point(100, 19)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(47, 17)
        Me.RadioButton3.TabIndex = 2
        Me.RadioButton3.TabStop = True
        Me.RadioButton3.Text = "&Ultra"
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Enabled = False
        Me.RadioButton2.Location = New System.Drawing.Point(55, 19)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(41, 17)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "&Pro"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Enabled = False
        Me.RadioButton1.Location = New System.Drawing.Point(9, 19)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(42, 17)
        Me.RadioButton1.TabIndex = 0
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "&Lite"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Normalize)
        Me.GroupBox2.Controls.Add(Me.Turbo)
        Me.GroupBox2.Controls.Add(Me.QVBR)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Bitrate)
        Me.GroupBox2.Controls.Add(Me.VBR)
        Me.GroupBox2.Controls.Add(Me.Resampling)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Location = New System.Drawing.Point(331, 109)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(155, 131)
        Me.GroupBox2.TabIndex = 43
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Audio Options"
        '
        'Normalize
        '
        Me.Normalize.AutoSize = True
        Me.Normalize.Location = New System.Drawing.Point(77, 100)
        Me.Normalize.Name = "Normalize"
        Me.Normalize.Size = New System.Drawing.Size(72, 17)
        Me.Normalize.TabIndex = 48
        Me.Normalize.Text = "&Normalize"
        Me.ToolTip1.SetToolTip(Me.Normalize, "Normalize volume audio of extracted and compressed track")
        Me.Normalize.UseVisualStyleBackColor = True
        '
        'Turbo
        '
        Me.Turbo.AutoSize = True
        Me.Turbo.Enabled = False
        Me.Turbo.Location = New System.Drawing.Point(9, 100)
        Me.Turbo.Name = "Turbo"
        Me.Turbo.Size = New System.Drawing.Size(54, 17)
        Me.Turbo.TabIndex = 47
        Me.Turbo.Text = "&Turbo"
        Me.Turbo.UseVisualStyleBackColor = True
        Me.Turbo.Visible = False
        '
        'QVBR
        '
        Me.QVBR.Location = New System.Drawing.Point(77, 73)
        Me.QVBR.Maximum = New Decimal(New Integer() {9, 0, 0, 0})
        Me.QVBR.Name = "QVBR"
        Me.QVBR.Size = New System.Drawing.Size(69, 20)
        Me.QVBR.TabIndex = 46
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 49)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 45
        Me.Label5.Text = "Bitrate:"
        '
        'Bitrate
        '
        Me.Bitrate.FormattingEnabled = True
        Me.Bitrate.Items.AddRange(New Object() {"32", "48", "64", "80", "96", "112", "128", "160", "192", "224", "256", "320"})
        Me.Bitrate.Location = New System.Drawing.Point(77, 46)
        Me.Bitrate.Name = "Bitrate"
        Me.Bitrate.Size = New System.Drawing.Size(69, 21)
        Me.Bitrate.TabIndex = 44
        Me.Bitrate.Text = "128"
        '
        'VBR
        '
        Me.VBR.AutoSize = True
        Me.VBR.Location = New System.Drawing.Point(9, 74)
        Me.VBR.Name = "VBR"
        Me.VBR.Size = New System.Drawing.Size(48, 17)
        Me.VBR.TabIndex = 43
        Me.VBR.Text = "&VBR"
        Me.VBR.UseVisualStyleBackColor = True
        '
        'Resampling
        '
        Me.Resampling.FormattingEnabled = True
        Me.Resampling.Items.AddRange(New Object() {"8000", "11025", "12000", "16000", "22050", "24000", "32000", "44100", "48000"})
        Me.Resampling.Location = New System.Drawing.Point(77, 19)
        Me.Resampling.Name = "Resampling"
        Me.Resampling.Size = New System.Drawing.Size(69, 21)
        Me.Resampling.TabIndex = 42
        Me.Resampling.Text = "44100"
        Me.ToolTip1.SetToolTip(Me.Resampling, "Sample Rate")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 38
        Me.Label4.Text = "Resampling:"
        '
        'OutputPath
        '
        Me.OutputPath.Location = New System.Drawing.Point(331, 269)
        Me.OutputPath.Name = "OutputPath"
        Me.OutputPath.Size = New System.Drawing.Size(126, 20)
        Me.OutputPath.TabIndex = 44
        '
        'TypeRIP
        '
        Me.TypeRIP.AutoSize = True
        Me.TypeRIP.Location = New System.Drawing.Point(68, 16)
        Me.TypeRIP.Name = "TypeRIP"
        Me.TypeRIP.Size = New System.Drawing.Size(84, 17)
        Me.TypeRIP.TabIndex = 46
        Me.TypeRIP.Text = "Drive Letter:"
        Me.ToolTip1.SetToolTip(Me.TypeRIP, "Select a Cd Drive")
        Me.TypeRIP.UseVisualStyleBackColor = True
        '
        'LogOut
        '
        Me.LogOut.BackColor = System.Drawing.Color.Black
        Me.LogOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LogOut.ForeColor = System.Drawing.Color.Chartreuse
        Me.LogOut.Location = New System.Drawing.Point(12, 329)
        Me.LogOut.Name = "LogOut"
        Me.LogOut.ReadOnly = True
        Me.LogOut.Size = New System.Drawing.Size(474, 164)
        Me.LogOut.TabIndex = 48
        Me.LogOut.Text = "Log"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.RebuildCUE)
        Me.GroupBox3.Controls.Add(Me.TrimWave)
        Me.GroupBox3.Controls.Add(Me.CDDB)
        Me.GroupBox3.Controls.Add(Me.Paranoia)
        Me.GroupBox3.Controls.Add(Me.DumpOnly)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.VGap)
        Me.GroupBox3.Controls.Add(Me.CueMode)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 246)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(313, 77)
        Me.GroupBox3.TabIndex = 53
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "CD Image Options"
        '
        'RebuildCUE
        '
        Me.RebuildCUE.AutoSize = True
        Me.RebuildCUE.Location = New System.Drawing.Point(188, 25)
        Me.RebuildCUE.Name = "RebuildCUE"
        Me.RebuildCUE.Size = New System.Drawing.Size(119, 17)
        Me.RebuildCUE.TabIndex = 62
        Me.RebuildCUE.Text = "&Rebuild ripped CUE"
        Me.ToolTip1.SetToolTip(Me.RebuildCUE, "Rebuild a ripped CUE from a converted CD image." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "The result file will be a cue/" &
        "iso wave or bin file, " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "you can also create a Clone CD file in ccd/img/sub forma" &
        "t.")
        Me.RebuildCUE.UseVisualStyleBackColor = True
        '
        'TrimWave
        '
        Me.TrimWave.AutoSize = True
        Me.TrimWave.Location = New System.Drawing.Point(235, 51)
        Me.TrimWave.Name = "TrimWave"
        Me.TrimWave.Size = New System.Drawing.Size(72, 17)
        Me.TrimWave.TabIndex = 57
        Me.TrimWave.Text = "Trim &Wav"
        Me.ToolTip1.SetToolTip(Me.TrimWave, "Reduce the sixe of extracted wave by removing un necessary empty value." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Any game" &
        " not work with this option enabled.")
        Me.TrimWave.UseVisualStyleBackColor = True
        '
        'CDDB
        '
        Me.CDDB.AutoSize = True
        Me.CDDB.Enabled = False
        Me.CDDB.Location = New System.Drawing.Point(248, 51)
        Me.CDDB.Name = "CDDB"
        Me.CDDB.Size = New System.Drawing.Size(56, 17)
        Me.CDDB.TabIndex = 61
        Me.CDDB.Text = "&CDDB"
        Me.ToolTip1.SetToolTip(Me.CDDB, "Perform only Dump of Cd without extract/convert.")
        Me.CDDB.UseVisualStyleBackColor = True
        Me.CDDB.Visible = False
        '
        'Paranoia
        '
        Me.Paranoia.AutoSize = True
        Me.Paranoia.Enabled = False
        Me.Paranoia.Location = New System.Drawing.Point(6, 25)
        Me.Paranoia.Name = "Paranoia"
        Me.Paranoia.Size = New System.Drawing.Size(68, 17)
        Me.Paranoia.TabIndex = 54
        Me.Paranoia.Text = "&Paranoia"
        Me.ToolTip1.SetToolTip(Me.Paranoia, "Sets the correction mode for digital  audio  extraction. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Perform overlapped rea" &
        "ding to avoid jitter with  additional  checks  of the read audio data")
        Me.Paranoia.UseVisualStyleBackColor = True
        '
        'DumpOnly
        '
        Me.DumpOnly.AutoSize = True
        Me.DumpOnly.Enabled = False
        Me.DumpOnly.Location = New System.Drawing.Point(92, 25)
        Me.DumpOnly.Name = "DumpOnly"
        Me.DumpOnly.Size = New System.Drawing.Size(78, 17)
        Me.DumpOnly.TabIndex = 60
        Me.DumpOnly.Text = "&Dump Only"
        Me.ToolTip1.SetToolTip(Me.DumpOnly, "Perform only Dump of Cd without extract/convert.")
        Me.DumpOnly.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(232, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 59
        Me.Label1.Text = "Gap:"
        Me.Label1.Visible = False
        '
        'VGap
        '
        Me.VGap.Enabled = False
        Me.VGap.Location = New System.Drawing.Point(268, 51)
        Me.VGap.Maximum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.VGap.Name = "VGap"
        Me.VGap.Size = New System.Drawing.Size(39, 20)
        Me.VGap.TabIndex = 58
        Me.VGap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.VGap, "Set the Gap value on CUE in value INDEX 01 00:00:00" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Usefull on any PSX game (Vib" &
        " Ribbon want a value set to 0) " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        Me.VGap.Value = New Decimal(New Integer() {2, 0, 0, 0})
        Me.VGap.Visible = False
        '
        'CueMode
        '
        Me.CueMode.FormattingEnabled = True
        Me.CueMode.Items.AddRange(New Object() {"MODE1/2048 [OTHER]", "MODE1/2048 [PC-CD | PCFX]", "MODE2/2352 [PSX]", "MODE1/2352 [SATURN]"})
        Me.CueMode.Location = New System.Drawing.Point(6, 50)
        Me.CueMode.Name = "CueMode"
        Me.CueMode.Size = New System.Drawing.Size(198, 21)
        Me.CueMode.TabIndex = 56
        Me.CueMode.Text = "MODE1/2352 [SATURN]"
        Me.ToolTip1.SetToolTip(Me.CueMode, "Select the source/final CUE file type")
        '
        'LogSave
        '
        Me.LogSave.AutoSize = True
        Me.LogSave.Location = New System.Drawing.Point(414, 499)
        Me.LogSave.Name = "LogSave"
        Me.LogSave.Size = New System.Drawing.Size(72, 17)
        Me.LogSave.TabIndex = 55
        Me.LogSave.Text = "Save &Log"
        Me.LogSave.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.RadioButton5)
        Me.GroupBox4.Controls.Add(Me.RadioButton6)
        Me.GroupBox4.Location = New System.Drawing.Point(331, 7)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(155, 39)
        Me.GroupBox4.TabIndex = 56
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Extract By"
        '
        'RadioButton5
        '
        Me.RadioButton5.AutoSize = True
        Me.RadioButton5.Location = New System.Drawing.Point(88, 16)
        Me.RadioButton5.Name = "RadioButton5"
        Me.RadioButton5.Size = New System.Drawing.Size(61, 17)
        Me.RadioButton5.TabIndex = 1
        Me.RadioButton5.Text = "b&chunk"
        Me.RadioButton5.UseVisualStyleBackColor = True
        '
        'RadioButton6
        '
        Me.RadioButton6.AutoSize = True
        Me.RadioButton6.Checked = True
        Me.RadioButton6.Location = New System.Drawing.Point(9, 16)
        Me.RadioButton6.Name = "RadioButton6"
        Me.RadioButton6.Size = New System.Drawing.Size(58, 17)
        Me.RadioButton6.TabIndex = 0
        Me.RadioButton6.TabStop = True
        Me.RadioButton6.Text = "&bin2iso"
        Me.RadioButton6.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(332, 247)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 13)
        Me.Label6.TabIndex = 57
        Me.Label6.Text = "Output Folder:"
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "ccd"
        Me.SaveFileDialog1.RestoreDirectory = True
        '
        'OutputFolder
        '
        Me.OutputFolder.BackgroundImage = Global.LazyAss.My.Resources.Resources.folder
        Me.OutputFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.OutputFolder.Location = New System.Drawing.Point(463, 267)
        Me.OutputFolder.Name = "OutputFolder"
        Me.OutputFolder.Size = New System.Drawing.Size(23, 23)
        Me.OutputFolder.TabIndex = 45
        Me.ToolTip1.SetToolTip(Me.OutputFolder, "Select the output folder for converted file")
        Me.OutputFolder.UseVisualStyleBackColor = True
        '
        'RIP
        '
        Me.RIP.BackgroundImage = Global.LazyAss.My.Resources.Resources.rip
        Me.RIP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.RIP.Location = New System.Drawing.Point(463, 296)
        Me.RIP.Name = "RIP"
        Me.RIP.Size = New System.Drawing.Size(23, 23)
        Me.RIP.TabIndex = 36
        Me.ToolTip1.SetToolTip(Me.RIP, "Start the conversion")
        Me.RIP.UseVisualStyleBackColor = True
        '
        'SelectImage
        '
        Me.SelectImage.BackgroundImage = Global.LazyAss.My.Resources.Resources.CUE
        Me.SelectImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SelectImage.Location = New System.Drawing.Point(15, 12)
        Me.SelectImage.Name = "SelectImage"
        Me.SelectImage.Size = New System.Drawing.Size(23, 23)
        Me.SelectImage.TabIndex = 33
        Me.ToolTip1.SetToolTip(Me.SelectImage, "Select a CUE file")
        Me.SelectImage.UseVisualStyleBackColor = True
        '
        'RefreshDriveToolStripMenuItem
        '
        Me.RefreshDriveToolStripMenuItem.Image = Global.LazyAss.My.Resources.Resources.update
        Me.RefreshDriveToolStripMenuItem.Name = "RefreshDriveToolStripMenuItem"
        Me.RefreshDriveToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.RefreshDriveToolStripMenuItem.Text = "&Refresh Drive"
        '
        'EjectUnmountDriveToolStripMenuItem
        '
        Me.EjectUnmountDriveToolStripMenuItem.Enabled = False
        Me.EjectUnmountDriveToolStripMenuItem.Image = Global.LazyAss.My.Resources.Resources.cd_rom
        Me.EjectUnmountDriveToolStripMenuItem.Name = "EjectUnmountDriveToolStripMenuItem"
        Me.EjectUnmountDriveToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.EjectUnmountDriveToolStripMenuItem.Text = "&Eject/Unmount Drive"
        '
        'LazyAss
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(498, 521)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.LogSave)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.LogOut)
        Me.Controls.Add(Me.TypeRIP)
        Me.Controls.Add(Me.OutputFolder)
        Me.Controls.Add(Me.OutputPath)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Format)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.RIP)
        Me.Controls.Add(Me.ListAddsFile)
        Me.Controls.Add(Me.RippedName)
        Me.Controls.Add(Me.SelectImage)
        Me.Controls.Add(Me.UNI)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "LazyAss"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LazyAss CD - Image Ripper"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.QVBR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.VGap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SelectImage As System.Windows.Forms.Button
    Friend WithEvents UNI As System.Windows.Forms.ComboBox
    Friend WithEvents RippedName As TextBox
    Friend WithEvents ListAddsFile As ListBox
    Friend WithEvents RIP As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Label3 As Label
    Friend WithEvents Format As ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents QVBR As NumericUpDown
    Friend WithEvents Label5 As Label
    Friend WithEvents Bitrate As ComboBox
    Friend WithEvents VBR As CheckBox
    Friend WithEvents Resampling As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Turbo As CheckBox
    Friend WithEvents OutputPath As TextBox
    Friend WithEvents OutputFolder As Button
    Friend WithEvents TypeRIP As CheckBox
    Friend WithEvents LogOut As RichTextBox
    Friend WithEvents Normalize As CheckBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents CueMode As ComboBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents TrimWave As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents VGap As NumericUpDown
    Friend WithEvents DumpOnly As CheckBox
    Friend WithEvents Paranoia As CheckBox
    Friend WithEvents LogSave As CheckBox
    Friend WithEvents CDDB As CheckBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents RefreshDriveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EjectUnmountDriveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents RadioButton5 As RadioButton
    Friend WithEvents RadioButton6 As RadioButton
    Friend WithEvents RebuildCUE As CheckBox
    Friend WithEvents Label6 As Label
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
End Class
