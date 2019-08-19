Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports System.Data
Imports System.Web
Imports System.IO
Public Class LoginForm1

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        REM ---------------- searching for the username and password
        arg = "select * from users where user_id = '" & UsernameTextBox.Text & "'"
        adp = New SqlDataAdapter(arg, conn)
        dt = New DataTable
        ds = New DataSet()
        adp.Fill(ds)
        REM ------------------- Valuidate the results
        If ds.Tables(0).Rows.Count = 0 Then
            MsgBox("Invalid User name or password ") : Exit Sub
        End If
        If ds.Tables(0).Rows(0)("user_password").ToString() <> PasswordTextBox.Text Then
            MsgBox("Invalid User name or password ") : Exit Sub
        End If
        REM ---------------- update company Name & users data
        user_id = UsernameTextBox.Text
        Main.ToolStripStatusLabel1.Text = user_id
        Main.ToolStripStatusLabel2.Text = "Sigma Industries"
        Main.ToolStripStatusLabel3.Text = "Version : " & Application.ProductVersion
        REM ---------------- log the entry 
        Me.Hide()
        Main.Show()
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub
    
    Private Sub LoginForm1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Call center(Me)
        conn = New SqlConnection("server = GEORGE-ERYAN\SQL2012 ;database = SIGMADB;Integrated Security=SSPI;")
        conn = New SqlConnection("server = DESKTOP-P14JTDU\GEORGE_SQL2012 ;database = SIGMADB;Integrated Security=SSPI;")
        conn = New SqlConnection("Data Source=.;Initial Catalog=SigmaDB;Integrated Security=True")
        conn.Open()



    End Sub

    Private Sub PasswordLabel_Click(sender As System.Object, e As System.EventArgs) Handles PasswordLabel.Click

    End Sub
End Class
