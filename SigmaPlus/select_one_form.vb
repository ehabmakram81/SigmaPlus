Imports System.Data.SqlClient
Public Class select_one_form

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        state = "cancel"
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        REM --------------------- validation
        state = "OK"
        Me.Hide()
    End Sub

    Private Sub select_one_form_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Call center(Me)
        REM ========================= 
        adp = New SqlDataAdapter(arg, conn)
        dt = New DataTable
        ds = New DataSet()
        adp.Fill(ds)
        ComboBox1.DataSource = ds.Tables(0)
        ComboBox1.DisplayMember = "name"
    End Sub
End Class