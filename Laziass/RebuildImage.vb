Public Class RebuildImage

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LazyAss.Abort = False
        LazyAss.MakeCUEBIN()
        Me.Close()
    End Sub

    Private Sub RebuildImage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LazyAss.Abort = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        LazyAss.Abort = False
        LazyAss.MakeCCD()
        Me.Close()
    End Sub

End Class