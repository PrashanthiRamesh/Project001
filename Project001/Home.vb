Public Class Home
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        GlobalVariables.removeUser()
        Dim loginForm = New FormLogin
        loginForm.Show()
        Me.Hide()
    End Sub
End Class