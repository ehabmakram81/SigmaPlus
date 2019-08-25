Public Class Short_hand_form
    Private Sub Short_hand_form_Disposed(sender As Object, e As System.EventArgs) Handles Me.Disposed
        acc_master = ""
        acc_name = ""
    End Sub
    Private Sub Short_hand_form_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        dg1.Columns(0).Width = 120
        dg1.Columns(1).Width = 295
        TextBox1.Text = acc_master
        TextBox2.Text = acc_name
    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub dg1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg1.CellContentClick

    End Sub

    Private Sub dg1_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles dg1.KeyPress
        If e.KeyChar <> Microsoft.VisualBasic.ChrW(Keys.Return) Then Exit Sub
        If dg1.RowCount - 1 = dg1.CurrentRow.Index Then Exit Sub
        If dg1.CurrentRow.Index = 0 Then Exit Sub

        REM ----------------------------------------------------------------------------
        Dim i = dg1.CurrentRow.Index - 1
        acc_master = dg1.Item(0, i).Value
        acc_name = dg1.Item(1, i).Value

        Close()
    End Sub

    Private Sub dg1_CellDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg1.CellDoubleClick
        If dg1.RowCount - 1 = dg1.CurrentRow.Index Then Exit Sub
        If e.RowIndex < 0 Then Exit Sub
        ' If dg1.CurrentRow.Index = 0 Then Exit Sub
        REM ----------------------------------------------------------------------------
        Dim i = dg1.CurrentRow.Index
        acc_master = dg1.Item(0, i).Value
        acc_name = dg1.Item(1, i).Value
        Close()
    End Sub
End Class