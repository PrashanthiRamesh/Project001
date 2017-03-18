Imports ADOX
Imports System.Data.OleDb

Public Class FormLogin
    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim constring As String = GlobalVariables.connectionString
        Using myconnection As New OleDbConnection(constring)
            myconnection.Open()
            Dim sqlQry As String = "SELECT * FROM [user] WHERE ([name]=@name AND [password]=@password)"
            Using cmd As New OleDbCommand(sqlQry, myconnection)
                cmd.Parameters.AddWithValue("@name", TextBox1.Text)
                cmd.Parameters.AddWithValue("@password", TextBox2.Text)
                Dim sdr As OleDbDataReader = cmd.ExecuteReader()
                ' If the record can be queried, it means passing verification, then open another form.   
                If (sdr.Read() = True) Then
                    MessageBox.Show("Logged into Botique Management System")
                    Dim homeForm As New Home
                    homeForm.Show()
                    Me.Hide()
                Else
                    MessageBox.Show("Invalid name or password!")
                End If
            End Using
        End Using


    End Sub


End Class
