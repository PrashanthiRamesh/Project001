Imports System.Data.OleDb

Public Class UserRegistration
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Dim name, password, confirm, email, phone, created_time, updated_time As String
        Dim created_at, updated_at As DateTime
        name = TextBox1.Text
        password = TextBox2.Text
        confirm = TextBox3.Text
        email = TextBox4.Text
        phone = TextBox5.Text
        created_at = FormatDateTime(Now, DateFormat.ShortDate)
        created_time = TimeOfDay.ToString("hh:mm:ss tt")
        updated_at = FormatDateTime(Now, DateFormat.ShortDate)
        updated_time = TimeOfDay.ToString("hh:mm:ss tt")


        If (String.IsNullOrEmpty(name) Or String.IsNullOrEmpty(password) Or String.IsNullOrEmpty(confirm) Or String.IsNullOrEmpty(email) Or String.IsNullOrEmpty(phone)) Then
            MsgBox("Please enter all the fields! ")


        ElseIf (Not String.Compare(password, confirm) = 0) Then
            MsgBox("The Passwords do not match! Enter Again!")
        Else
            Dim constring As String = GlobalVariables.connectionString
            Using myconnection As New OleDbConnection(constring)
                myconnection.Open()
                Dim sqlQry As String =
                "INSERT INTO [user] ([name], [password], [email], [phone], [created_at], [created_time], [updated_at], [updated_time]) 
                VALUES (@name, @password, @email, @phone, @created_at, @created_time, @updated_at, updated_time)"
                Using cmd As New OleDbCommand(sqlQry, myconnection)
                    cmd.Parameters.AddWithValue("@name", name)
                    cmd.Parameters.AddWithValue("@password", password)
                    cmd.Parameters.AddWithValue("@email", email)
                    cmd.Parameters.AddWithValue("@phone", phone)
                    cmd.Parameters.AddWithValue("@created_at", created_at)
                    cmd.Parameters.AddWithValue("@created_time", created_time)
                    cmd.Parameters.AddWithValue("@updated_at", updated_at)
                    cmd.Parameters.AddWithValue("@updated_time", updated_time)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            MsgBox("Yay ! Initial Setup Success!")
            Me.Hide()
            Dim formtoshow = New FormLogin
            formtoshow.Show()

        End If

    End Sub
End Class