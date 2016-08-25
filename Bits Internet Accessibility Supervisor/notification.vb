Public Class notification
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.Close()
    End Sub

    Private Sub notification_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Focus()
    End Sub
End Class