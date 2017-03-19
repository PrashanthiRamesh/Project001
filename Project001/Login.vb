Imports ADOX
Imports System.Data.OleDb
Imports System.Net.Mail

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
                Dim da As New OleDbDataAdapter(cmd)
                Dim ds As New Data.DataSet
                da.Fill(ds)
                If ds.Tables(0).Rows.Count = 1 Then
                    MessageBox.Show("Logged into Botique Management System")
                    Dim userid = ds.Tables(0).Rows(0)("uid")
                    Dim username = ds.Tables(0).Rows(0)("name")
                    Dim useremail = ds.Tables(0).Rows(0)("email")
                    Dim userphone = ds.Tables(0).Rows(0)("phone")
                    GlobalVariables.setUser(userid, username, useremail, userphone)
                    Dim homeForm As New Home
                    homeForm.Show()
                    Me.Hide()
                Else
                    MessageBox.Show("Invalid name or password!")
                End If
            End Using
        End Using


    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim email, password As String
        email = InputBox("Enter the registered email?", "Password Recovery", " ")

        'TODO: check if it is a 10 digit phone number and not empty
        If email = " " Then
            MessageBox.Show("Please enter a valid email ID")



        End If

        Dim constring As String = GlobalVariables.connectionString
        Using myconnection As New OleDbConnection(constring)
            myconnection.Open()
            Dim sqlQry As String = "SELECT * FROM [user] WHERE ([email]=@email)"
            Using cmd As New OleDbCommand(sqlQry, myconnection)
                cmd.Parameters.AddWithValue("@email", email)

                Dim da As New OleDbDataAdapter(cmd)
                Dim ds As New Data.DataSet
                da.Fill(ds)
                If ds.Tables(0).Rows.Count = 1 Then

                    password = ds.Tables(0).Rows(0)("password")


                    'TODO: send mail
                    Try
                        Dim Smtp_Server As New SmtpClient
                        Dim e_mail As New MailMessage()
                        Smtp_Server.UseDefaultCredentials = False
                        Smtp_Server.Port = 587
                        Smtp_Server.EnableSsl = True
                        Smtp_Server.Host = "smtp.gmail.com"
                        Smtp_Server.TargetName = "STARTTLS/smtp.gmail.com"
                        Smtp_Server.Credentials = New Net.NetworkCredential(GlobalVariables.emailid, GlobalVariables.emailpassword)

                        e_mail = New MailMessage()
                        e_mail.From = New MailAddress(GlobalVariables.emailid)
                        e_mail.To.Add(email)
                        e_mail.Subject = "Botique App Password Recovery"
                        e_mail.IsBodyHtml = False
                        e_mail.Body = "Your Password : " + password
                        Smtp_Server.Send(e_mail)
                        MsgBox("Mail Sent to " + email)

                    Catch error_t As Exception
                        MsgBox(error_t.ToString)
                    End Try


                Else
                    MessageBox.Show("Email not registered !")
                End If
            End Using
        End Using



    End Sub
End Class
