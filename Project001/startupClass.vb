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
            "customer",
            "bill",
            "order"
        }

        Dim tables_query As String() = {
            "CREATE TABLE [user] ([uid] COUNTER, [name] TEXT(50), [password] TEXT(50), [email] TEXT(50), [phone] TEXT(50), created_on DATETIME, created_time TEXT(20), updated_on DATETIME, updated_time TEXT(20))",
            "CREATE TABLE [customer] ([cid] COUNTER, [cname] TEXT(50) NOT NULL, [cphone] TEXT(50) NOT NULL, [credit] TEXT(50), [cmeasurements] TEXT(100), [pending_flag] BIT , created_on DATETIME, created_time TEXT(20), updated_on DATETIME, updated_time TEXT(20))",
            "CREATE TABLE [bill] ([bid] COUNTER, [cname] TEXT(50) NOT NULL, [cphone] TEXT(50) NOT NULL, [delivery_date] DATETIME, [delivery_time] TEXT(20), [total_amount] TEXT(50) , created_on DATETIME, created_time TEXT(20), updated_on DATETIME, updated_time TEXT(20))",
            "CREATE TABLE [order] ([oid] COUNTER, [bid] NUMBER, [item] TEXT(50), [price] TEXT(50), [quantity] NUMBER, [pending_flag] BIT, [total_quantity] NUMBER, created_on DATETIME, created_time TEXT(20), updated_on DATETIME, updated_time TEXT(20) )"
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
