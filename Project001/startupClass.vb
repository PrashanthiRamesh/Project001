Imports System.IO

Public Class startupClass

    Public Shared Sub Main()

        'The form that we will end up showing
        Dim formToShow As System.Windows.Forms.Form = Nothing

        Dim s = New SplashScreen1()
        s.Show()
        'Do processing here or thread.sleep to illustrate the concept
        System.Threading.Thread.Sleep(3000)


        Dim accessFilePath = GlobalVariables.filePath

        If File.Exists(accessFilePath) Then
            formToShow = New FormLogin
        Else
            CreateAccessDatabase(GlobalVariables.filePath)
            CreateAccessTables(GlobalVariables.filePath)
            MsgBox("Database and Tables created")
            formToShow = New UserRegistration
        End If

        'Show the form, and keep it open until it's explicitly closed.
        s.Close()

        Application.Run(formToShow)

    End Sub

    Public Shared Function CreateAccessDatabase(ByVal DatabaseFullPath As String) As Boolean
        Dim bAns As Boolean
        Dim cat As New ADOX.Catalog()
        Try
            cat.Create(GlobalVariables.connectionString)
            cat = Nothing

        Catch Excep As System.Runtime.InteropServices.COMException
            bAns = False

        Finally
            cat = Nothing
        End Try
        Return bAns
    End Function


    Public Shared Function CreateAccessTables(ByVal DatabaseFullPath As String) As Boolean

        Dim tables_name As String() = {
            "user",
            "customer"
        }

        Dim tables_query As String() = {
            "CREATE TABLE [user] ([uid] COUNTER, [name] TEXT(50), [password] TEXT(50), [email] TEXT(50), [phone] TEXT(50), created_at DATETIME, created_time TEXT(20), updated_at DATETIME, updated_time TEXT(20))",
            "CREATE TABLE [customer] ([Field1] TEXT(10), [Field2] TEXT(10))"
        }



        For i = 0 To UBound(tables_name)
            Dim con As New OleDb.OleDbConnection(GlobalVariables.connectionString)
            con.Open()
            'Get database schema
            Dim dbSchema As DataTable = con.GetOleDbSchemaTable(OleDb.OleDbSchemaGuid.Tables, New Object() {Nothing, Nothing, tables_name(i), "TABLE"})
            con.Close()

            ' If the table exists, the count = 1
            If dbSchema.Rows.Count > 0 Then
                ' do whatever you want to do if the table exists
            Else
                'do whatever you want to do if the table does not exist
                ' e.g. create a table
                Dim cmd As New OleDb.OleDbCommand(tables_query(i), con)
                con.Open()
                cmd.ExecuteNonQuery()
                'MessageBox.Show("Table Created Successfully")
                con.Close()
            End If
        Next

        Return 0
    End Function

End Class
