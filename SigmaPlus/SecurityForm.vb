Imports System.Data.SqlClient

Public Class SecurityForm

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Call center(Me)
        dg1.AllowUserToAddRows = False
        REM ----------------------- loading users
        arg = "select user_id, user_name from users order by user_id "
        fill_datagrid(dg1, arg)
        'adp = New SqlDataAdapter(arg, conn)
        'dt = New DataTable
        'ds = New DataSet()
        'adp.Fill(ds)
        'dg1.DataSource = ds.Tables(0)

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        REM ---------------------------------------------------------- Validate
        If TextBox4.Text = "" Then
            MsgBox("Invalid user id") : Exit Sub
        End If
        If TextBox1.Text = "" Then
            MsgBox("Invalid user name") : Exit Sub
        End If
        If TextBox2.Text <> TextBox3.Text Then
            MsgBox("Passwords are not matched") : Exit Sub
        End If
        REM ----------------------------------------------------------- save
        arg = "insert into users (user_id, user_name, user_password) values ('" & TextBox4.Text & "', '" & TextBox1.Text & "', '" & TextBox2.Text & "')"
        Dim cmd As SqlCommand = New SqlCommand(arg, conn)
        cmd.ExecuteNonQuery()
        REM ------------------------------------------------------- clear form 
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        REM ------------------------------------------------------- update data grid

    End Sub
End Class