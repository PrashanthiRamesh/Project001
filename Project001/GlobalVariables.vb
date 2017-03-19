Public Class GlobalVariables

    Public Shared filePath As String = "F:\BotiqueApp\Project001.accdb"
    Public Shared connectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=F:\BotiqueApp\Project001.accdb"
    Public Shared emailid As String = "prashanthiramesh4@gmail.com"
    Public Shared emailpassword As String = "krugerbrent"

    Public Shared uid, uname, uemail, uphone As String

    Public Shared Function setUser(ByVal uuid As String, ByVal uuname As String, ByVal uuemail As String, ByVal uuphone As String)
        uid = uuid
        uname = uuname
        uemail = uuemail
        uphone = uuphone
        Return 0
    End Function

    Public Shared Function removeUser()
        uid = ""
        uname = ""
        uemail = ""
        uphone = ""
        Return 0
    End Function

End Class
