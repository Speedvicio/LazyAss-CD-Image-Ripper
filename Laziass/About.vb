Public Class About
    Dim Contatore As Integer = 60

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Contatore = Contatore - 1
        Label1.Text = Contatore
        If Contatore = 0 Then Timer1.Enabled = False : Label1.Visible = False : Button1.Visible = True
    End Sub

    Private Sub About_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.SelectionStart = 0
        TextBox1.SelectionLength = 0
        Timer1.Enabled = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CheckBox1.Checked = True Then System.IO.File.Create(Application.StartupPath & "\Lazy").Dispose()
        Me.Close()
    End Sub

End Class