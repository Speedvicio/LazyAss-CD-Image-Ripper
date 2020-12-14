Public Class FPismo

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Process.Start(Application.StartupPath & "\Converter\ptiso.exe", "convert -g")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Process.Start(Application.StartupPath & "\Converter\ptiso.exe", "create -g")
    End Sub

End Class