Imports System.Data.SqlClient
Public Class more_info

    Private Sub more_info_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        arg = "select * from items "
        fill_datagrid(dg1, arg)
        'adp = New SqlDataAdapter(arg, conn)
        'dt = New DataTable
        'ds = New DataSet()
        'adp.Fill(ds)
        'dg1.DataSource = ds.Tables(0)
    End Sub

    Private Sub more_info_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
        dg1.Width = Me.Width - 75
        dg1.Height = Me.Height - 75
    End Sub
End Class