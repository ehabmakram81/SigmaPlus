Imports System.Data.SqlClient

Public Class Master_Files
    Private Sub Master_Files_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        center(Me)
        ComboBox1.Items.Add("")
        ComboBox1.Items.Add("Stores")
        ComboBox1.Items.Add("UOM")
        ComboBox1.Items.Add("Family")
        ComboBox1.Items.Add("Currencies")
        'NewMenuItem_Click(sender, e)
    End Sub

    Private Sub ExitMenuItem_Click(sender As Object, e As EventArgs) Handles ExitMenuItem.Click
        Close()
    End Sub

    Private Sub NewMenuItem_Click(sender As Object, e As EventArgs) Handles NewMenuItem.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        SaveMenuItem.Text = "حفظ"
        'GroupBox1.Enabled = False
        'ComboBox1.Enabled = True
        ds.Tables(0).Clear()
        LoadDG1()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        'GroupBox1.Enabled = True
        'ComboBox1.Enabled = False
        REM = ========= Load DG1
        LoadDG1()
    End Sub
    Sub LoadDG1()
        If ComboBox1.Text = "" Then Exit Sub
        arg = "select * from " & ComboBox1.Text & ""
        adp = New SqlDataAdapter(arg, conn)
        dt = New DataTable
        ds = New DataSet()
        adp.Fill(ds)
        DG1.DataSource = ds.Tables(0)
    End Sub
    Private Sub SaveMenuItem_Click(sender As Object, e As EventArgs) Handles SaveMenuItem.Click
        If ComboBox1.Text = "" Then
            MsgBox("برجاء ادخال البيان") : Exit Sub
        End If
        If TextBox1.Text = "" Then
            MsgBox("برجاء ادخال الكود") : Exit Sub
        End If
        If TextBox2.Text = "" Then
            MsgBox(" برجاء ادخال الأسم") : Exit Sub
        End If
        REM ===== validat 0 and Space

        If Not Validat_Code(TextBox1.Text) Then
            MsgBox(ErrDisc)
            Exit Sub
        End If
       
        REM ===== validat check code and name 
        For Each row As DataGridViewRow In DG1.Rows
            If row.Cells.Item("code").Value = TextBox1.Text Then
                MsgBox("تم ادخال هذا الكود من قبل") : Exit Sub
            End If
            If row.Cells.Item("name1").Value = TextBox2.Text Then
                MsgBox("تم ادخال هذا الأسم من قبل ") : Exit Sub
            End If
        Next
        REM ===== save 
        If SaveMenuItem.Text = "حفظ" Then
            arg = "INSERT INTO " & ComboBox1.Text & " (code,name) VALUES ('" & UCase(TextBox1.Text) & "','" & TextBox2.Text & "')"
            If conn.State = ConnectionState.Closed Then conn.Open()
            Dim cmd1 As New System.Data.SqlClient.SqlCommand(arg, conn)
            cmd1.ExecuteNonQuery()
        Else
            arg = "UPDATE " & ComboBox1.Text & "  SET code ='" & UCase(TextBox1.Text) & "', Name = '" & TextBox2.Text & "' WHERE (code = N'" & TextBox1.Text & "')"
            If conn.State = ConnectionState.Closed Then conn.Open()
            Dim cmd1 As New System.Data.SqlClient.SqlCommand(arg, conn)
            cmd1.ExecuteNonQuery()
        End If
        'MsgBox("تم الادخال بنجاح")
        REM = ========= Load DG1

        LoadDG1()
        TextBox1.Text = ""
        TextBox2.Text = ""
        SaveMenuItem.Text = "حفظ"
        GroupBox1.Enabled = False
        ComboBox1.Enabled = True

    End Sub

    Private Sub DeleteMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteMenuItem.Click
        Dim J = MsgBox("هل تريد حذف هذا الكود", vbYesNo)
        If J <> 6 Then Exit Sub
        If ComboBox1.Text = "" Then
            MsgBox("برجاء ادخال البيان") : Exit Sub
        End If
        If TextBox1.Text = "" Then
            MsgBox("برجاء ادخال الكود") : Exit Sub
        End If
        If TextBox2.Text = "" Then
            MsgBox(" برجاء ادخال الأسم") : Exit Sub
        End If
        If SaveMenuItem.Text <> "حفظ" Then
            arg = "DELETE FROM " & ComboBox1.Text & " WHERE (Code = '" & TextBox1.Text & "')"
            If conn.State = ConnectionState.Closed Then conn.Open()
            Dim cmd1 As New System.Data.SqlClient.SqlCommand(arg, conn)
            cmd1.ExecuteNonQuery()
            MsgBox("تم الحذف")
            LoadDG1()
            TextBox1.Text = ""
            TextBox2.Text = ""
            SaveMenuItem.Text = "حفظ"
            GroupBox1.Enabled = False
            ComboBox1.Enabled = True
        End If
        REM = ========= Load DG1

    End Sub

    Private Sub DG1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DG1.CellContentClick

    End Sub

    Private Sub DG1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DG1.CellDoubleClick
        TextBox1.Text = DG1.CurrentRow.Cells("code").Value.ToString
        TextBox2.Text = DG1.CurrentRow.Cells("name1").Value.ToString
        SaveMenuItem.Text = "تعديل"
    End Sub
End Class