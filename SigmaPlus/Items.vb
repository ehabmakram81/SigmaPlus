Imports System.Data.SqlClient

Public Class Items

    Private Sub NewMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles NewMenuItem.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        ComboBox1.Text = ""
        ComboBox2.Text = ""
        ComboBox3.Text = ""
        ComboBox5.Text = ""
        DG1.ReadOnly = True
        DG1.DataSource = Nothing
        CheckBox1.Checked = True
        REM==================================== Change Control
        GroupBox1.Enabled = True
        GroupBox3.Enabled = False
        SaveMenuItem.Text = "حفظ"
        TextBox1.Focus()
    End Sub

    Private Sub Items_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        center(Me)
        REM =========================== Get item length
        whno = find_setting("ItemsIDLength")
        REM =========================== load Family
        ComboBox2.Items.Clear()
        arg = "SELECT DISTINCT Name FROM Family order by Name"
        fill_Combo(ComboBox2, arg)
        REM =========================== load Type
        ComboBox1.Items.Add("")
        ComboBox1.Items.Add("Cons")
        ComboBox1.Items.Add("Matrial")
        ComboBox1.Items.Add("Product")
        ComboBox1.Items.Add("Tool")
        ComboBox1.Items.Add("subAssy")
        REM =========================== load SerialTrack
        ComboBox3.Items.Add("")
        ComboBox3.Items.Add("Non")
        ComboBox3.Items.Add("Serial")
        ComboBox3.Items.Add("Batch")
        ComboBox3.Items.Add("Serial / Batch")
        REM =========================== load UOM
        ComboBox4.Items.Clear()
        ComboBox5.Items.Clear()
        arg = "SELECT DISTINCT Code FROM UOM order by Code"
        fill_Combo(ComboBox4, arg)
        fill_Combo(ComboBox5, arg)
        NewMenuItem_Click(sender, e)
    End Sub

    Private Sub ExitMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExitMenuItem.Click
        Close()
    End Sub


    Private Sub TextBox1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode <> Keys.Enter Then Exit Sub
        REM ----------------------- check that code lenth is acceptble
        If Len(TextBox1.Text) <> whno Then
            MsgBox("طول الكود غير مقبول") : Exit Sub
        End If
        REM =========================================== validat 0 and Space
        If Not Validat_Code(TextBox1.Text) Then
            MsgBox(ErrDisc) : Exit Sub
        End If
        REM ===== Retrive Date 
        loaddata()
    End Sub

    Sub loaddata()
        If TextBox1.Text <> "" Then
            arg = "SELECT code, name, aname, UOM, Type1, family, cost, ReOrder, wght, SerialTrack, Active, Remarks FROM items WHERE (code = '" & TextBox1.Text & "')"
            Dim cmd2 As SqlCommand = New SqlCommand(arg, conn)
            Dim reader1 As SqlDataReader = cmd2.ExecuteReader()
            If reader1.HasRows = True Then
                reader1.Read()
                TextBox2.Text = reader1("name").ToString
                TextBox3.Text = reader1("aname").ToString
                TextBox4.Text = reader1("cost").ToString
                TextBox5.Text = reader1("Remarks").ToString
                TextBox7.Text = reader1("wght").ToString
                TextBox6.Text = reader1("ReOrder").ToString
                ComboBox5.Text = reader1("UOM").ToString
                ComboBox1.Text = reader1("Type1").ToString
                ComboBox2.Text = reader1("family").ToString
                ComboBox3.SelectedIndex = Val(reader1("SerialTrack").ToString)
                If Val(reader1("Active").ToString) = 0 Then
                    CheckBox1.Checked = True
                Else
                    CheckBox1.Checked = False
                End If
                SaveMenuItem.Text = "تعديل"
            End If
            reader1.Close()
            REM ===== Retrive Date UOM
            fill_datagrid(DG1, "select UOM,factor from items_uom where item = '" & TextBox1.Text & "'")
            DG1.Columns(0).HeaderText = "الوحدة"
            DG1.Columns(1).HeaderText = "factor"
            DG1.Columns(0).Width = 200
            DG1.Columns(1).Width = 150

        End If
        REM==================================== Change Control
        GroupBox1.Enabled = False
        GroupBox3.Enabled = True
        TextBox2.Focus()
    End Sub

    Private Sub DeleteMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DeleteMenuItem.Click
        If TextBox1.Text = "" Then
            MsgBox(" برجاء ادخال كود الصنف")
            Exit Sub
        End If
        If Not Validat_Code(TextBox1.Text) Then
            MsgBox(ErrDisc)
            Exit Sub
        End If
        REM ----------------------- check that code lenth is acceptble
        If Len(TextBox1.Text) <> whno Then
            MsgBox("طول الكود غير مقبول") : Exit Sub
        End If
    End Sub

    Private Sub SaveMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SaveMenuItem.Click
        REM ------------------------------------------------ Validation
        If TextBox1.Text = "" Then
            MsgBox(" برجاء ادخال كود الصنف")
            Exit Sub
        End If
        If TextBox2.Text = "" Then
            MsgBox(" برجاء ادخال أسم الصنف")
            Exit Sub
        End If
        'If TextBox3.Text = "" Then
        '    MsgBox(" برجاء ادخال الأسم الثانى للصنف")
        '    Exit Sub
        'End If
        If ComboBox1.Text = "" Then
            MsgBox(" برجاء ادخال نوع الصنف")
            Exit Sub
        End If
        If ComboBox2.Text = "" Then
            MsgBox(" برجاء ادخال التصنيف")
            Exit Sub
        End If
        If ComboBox3.Text = "" Then
            MsgBox(" برجاء ادخال التتبع")
            Exit Sub
        End If
        If ComboBox5.Text = "" Then
            MsgBox(" برجاء ادخال الوحده")
            Exit Sub
        End If
        If Not Validat_Code(TextBox1.Text) Then
            MsgBox(ErrDisc)
            Exit Sub
        End If
        'If Not IsNumeric(TextBox4.Text) Then
        '    MsgBox(" برجاء ادخال السعر صحيح")
        '    Exit Sub
        'End If
        'If Val(TextBox4.Text) <= 0 Then
        '    MsgBox(" برجاء ادخال السعر ")
        '    Exit Sub
        'End If
        Dim checked As Integer
        If CheckBox1.Checked = True Then
            checked = 0
        Else
            checked = 1
        End If

        REM ===== save 
        InsertAlltransaction()

        'If SaveMenuItem.Text = "حفظ" Then

        '    arg = "INSERT INTO items(code, name, aname, UOM, Type1, family, cost, ReOrder, wght, SerialTrack, Active, Remarks, user_id, trx_date)"
        '    arg = arg & "VALUES ('" & UCase(TextBox1.Text) & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & ComboBox5.Text & "','" & ComboBox1.Text & "','" & ComboBox2.Text & "','" & Val(TextBox4.Text) & "','" & Val(TextBox6.Text) & "','" & Val(TextBox7.Text) & "'  ,'" & Val(ComboBox3.SelectedIndex) & "','" & checked & "','" & TextBox5.Text & "','" & user_id & "', getdate())"
        '    If conn.State = ConnectionState.Closed Then conn.Open()
        '    Dim cmd1 As New System.Data.SqlClient.SqlCommand(arg, conn)
        '    cmd1.ExecuteNonQuery()


        'Else

        '    arg = "UPDATE items SET   UOM = '" & ComboBox5.Text & "', Type1 ='" & ComboBox1.Text & "', family ='" & ComboBox2.Text & "', cost ='" & Val(TextBox4.Text) & "', ReOrder ='" & Val(TextBox6.Text) & "', wght ='" & Val(TextBox7.Text) & "', SerialTrack ='" & Val(ComboBox3.SelectedIndex) & "', Active ='" & checked & "', Remarks ='" & TextBox5.Text & "', user_id ='" & user_id & "', trx_date = getdate() WHERE (code = '" & TextBox1.Text & "')"
        '    If conn.State = ConnectionState.Closed Then conn.Open()
        '    Dim cmd1 As New System.Data.SqlClient.SqlCommand(arg, conn)
        '    cmd1.ExecuteNonQuery()
        'End If
        'REM ===== save UOM
        'If DG1.Rows.Count <> 0 Then
        '    arg = "DELETE FROM items_uom WHERE (item = '" & TextBox1.Text & "')"
        '    If conn.State = ConnectionState.Closed Then conn.Open()
        '    Dim cmd2 As New System.Data.SqlClient.SqlCommand(arg, conn)
        '    cmd2.ExecuteNonQuery()
        '    For i As Integer = 0 To DG1.Rows.Count - 1
        '        arg = " INSERT INTO items_uom (item, UOM, factor) VALUES('" & TextBox1.Text & "','" & DG1.Rows(i).Cells("UOM").Value.ToString & "' , '" & DG1.Rows(i).Cells("factor").Value.ToString & "' )"
        '        If conn.State = ConnectionState.Closed Then conn.Open()
        '        Dim cmd3 As New System.Data.SqlClient.SqlCommand(arg, conn)
        '        cmd3.ExecuteNonQuery()

        '    Next
        'End If
        MsgBox("تم الحفظ")
        NewMenuItem_Click(sender, e)
    End Sub


    Private Sub InsertAlltransaction()
        REM === to Commit transaction
        Dim command As SqlCommand = conn.CreateCommand()
        Dim transaction As SqlTransaction
        ' Start transaction
        transaction = conn.BeginTransaction("SampleTransaction")
        command.Connection = conn
        command.Transaction = transaction
        Try
            REM ===== save 
            If SaveMenuItem.Text = "حفظ" Then

                arg = "INSERT INTO items(code, name, aname, UOM, Type1, family, cost, ReOrder, wght, SerialTrack, Active, Remarks, user_id, trx_date)"
                arg = arg & "VALUES ('" & UCase(TextBox1.Text) & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & ComboBox5.Text & "','" & ComboBox1.Text & "','" & ComboBox2.Text & "','" & Val(TextBox4.Text) & "','" & Val(TextBox6.Text) & "','" & Val(TextBox7.Text) & "'  ,'" & Val(ComboBox3.SelectedIndex) & "','" & checked & "','" & TextBox5.Text & "','" & user_id & "', getdate())"
                command.CommandText = arg
                command.ExecuteNonQuery()
            Else
                arg = "UPDATE items SET   UOM = '" & ComboBox5.Text & "', Type1 ='" & ComboBox1.Text & "', family ='" & ComboBox2.Text & "', cost ='" & Val(TextBox4.Text) & "', ReOrder ='" & Val(TextBox6.Text) & "', wght ='" & Val(TextBox7.Text) & "', SerialTrack ='" & Val(ComboBox3.SelectedIndex) & "', Active ='" & checked & "', Remarks ='" & TextBox5.Text & "', user_id ='" & user_id & "', trx_date = getdate() WHERE (code = '" & TextBox1.Text & "')"
                command.CommandText = arg
                command.ExecuteNonQuery()
            End If
            REM ===== save UOM
            If DG1.Rows.Count <> 0 Then
                Dim arg1 As String = "DELETE FROM items_uom WHERE (item = '" & TextBox1.Text & "')"
                command.CommandText = arg1
                command.ExecuteNonQuery()
                For i As Integer = 0 To DG1.Rows.Count - 1
                    Dim arg2 As String = " INSERT INTO items_uom (item, UOM, factor) VALUES('" & TextBox1.Text & "','" & DG1.Rows(i).Cells("UOM").Value.ToString & "' , '" & DG1.Rows(i).Cells("factor").Value.ToString & "' )"
                    command.CommandText = arg2
                    command.ExecuteNonQuery()

                Next
            End If
            ' Attempt to commit the transaction.
            transaction.Commit()
            Console.WriteLine("Both records are written to database.")

        Catch ex As Exception
            Console.WriteLine("Commit Exception Type: {0}", ex.GetType())
            Console.WriteLine("  Message: {0}", ex.Message)

            ' Attempt to roll back the transaction. 
            Try
                transaction.Rollback()

            Catch ex2 As Exception
                ' This catch block will handle any errors that may have occurred 
                ' on the server that would cause the rollback to fail, such as 
                ' a closed connection.
                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                Console.WriteLine("  Message: {0}", ex2.Message)
            End Try
        End Try

    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox4.SelectedIndexChanged
        If ComboBox4.Text = "" Then Exit Sub
        TextBox8.Focus()
    End Sub

    Sub adduom()
        DG1.AllowUserToAddRows = False
        Dim Value As Integer
        REM ======== check factor int
        If Not Integer.TryParse(TextBox8.Text, Value) Then
            MessageBox.Show("please enter a number!")
            Exit Sub
        End If
        Dim row As DataRow = ds.Tables(0).NewRow
        row.Item(0) = ComboBox4.Text
        row.Item(1) = Val(TextBox8.Text)
        ds.Tables(0).Rows.Add(row)
        DG1.DataSource = ds.Tables(0)
        DG1.EndEdit()
        ComboBox4.Text = ""
        TextBox8.Text = ""

    End Sub

    Private Sub TextBox8_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyDown
        If e.KeyCode <> Keys.Enter Then Exit Sub
        REM ========== vaildat
        If ComboBox4.Text = "" Then
            MsgBox("برجاء ادخال الوحده")
            Exit Sub
        End If

        If TextBox8.Text = "" Then
            MsgBox("برجاء ادخال القيمه")
            Exit Sub
        End If
        If Val(TextBox8.Text) <= 0 Then
            MsgBox(" برجاء ادخال القيمه بشكل صحيح ")
            Exit Sub
        End If
        If ComboBox4.Text = ComboBox5.Text Then
            MsgBox("هذة الوحده الاساسيه لا يمكن اضافتها مره اخرى")
            Exit Sub
        End If
        REM ========== Check Dublicat
        For Each row As DataGridViewRow In DG1.Rows

            If row.Cells.Item("UOM").Value = ComboBox4.Text And row.Cells.Item("factor").Value = TextBox8.Text Then
                MsgBox("تم ادخال هذة الوحده من قبل ") : Exit Sub
            End If
        Next
        REM =========== Add Row
        adduom()
    End Sub
    Private Sub ComboBox5_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox5.SelectedIndexChanged
        For Each row As DataGridViewRow In DG1.Rows
            If row.Cells.Item("UOM").Value = ComboBox5.Text Then
                MsgBox("تم ادخال هذة الوحده من قبل ")
                ComboBox5.Text = ""
                Exit Sub
            End If
        Next
    End Sub
    Private Sub DeleteItemMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DeleteItemMenuItem.Click
        'Dim J = MsgBox(" هل تريد حذف وحده " & DG1.CurrentRow.Cells("UOM").Value & " ", vbYesNo)
        'If J <> 6 Then Exit Sub
        'REM ================= Delete Before Save serial History == Details
        'arg = "DELETE  FROM items_uom WHERE item = '" & TextBox1.Text & "' and UOM = '" & DG1.CurrentRow.Cells("UOM").Value & "'"
        'If conn.State = ConnectionState.Closed Then conn.Open()
        'Dim cmd2 As New System.Data.SqlClient.SqlCommand(arg, conn)
        'cmd2.ExecuteNonQuery()
        'loaddata()
    End Sub
End Class