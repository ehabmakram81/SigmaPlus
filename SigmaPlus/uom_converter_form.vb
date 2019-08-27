Public Class uom_converter_form

    Private Sub uom_converter_form_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Call center(Me)
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar <> Microsoft.VisualBasic.ChrW(Keys.Return) Then Exit Sub

        Call short_ha(TextBox2.Text, "", "items_view")
        If acc_master <> "" Then
            TextBox1.Text = acc_master
            TextBox2.Text = acc_name
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar <> Microsoft.VisualBasic.ChrW(Keys.Return) Then Exit Sub

        Call short_ha("", TextBox2.Text, "items_view")

        TextBox1.Text = acc_master
        TextBox2.Text = acc_name

    End Sub

    Private Sub TextBox2_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        REM ---------------------- conversion process
        REM ====================== Validation
        REM ---------------------- check Qnatitties are enough per line
        REM ====================== Saving (lood over all dg1 rows) 
        REM ====================== clear the form 
    End Sub
End Class