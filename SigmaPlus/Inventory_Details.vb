Imports System.Data.SqlClient

Public Class Inventory_Details
    Dim factor As String

    Private Sub Inventory_form_popup_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Call center(Me)
        loaddata()
    End Sub

    Public Sub loaddata()
        REM =========================== load items UOM
        arg = "SELECT UOM, factor FROM  items_uom WHERE (item = N'" & TextBox5.Text & "')"
        fill_Combo(ComboBox1, arg)
        If ComboBox1.Items.Count = 2 Then
            ComboBox1.SelectedIndex = 1
        End If

        REM =========================== Old items 
        arg = "SELECT UOM, Qty, batch , SerNo as barcode, NetQty as NQty, ExpDate ,NetUOMQty,'Old' Status , RowID FROM  dbo.Serial_Details WHERE (Indi = '" & TextBox8.Text & "') AND (Serial = '" & TextBox4.Text & "') AND (WhNo = '" & TextBox7.Text & "') AND (ItemNo = '" & TextBox5.Text & "') order by RowID"
        fill_datagrid(DG1, arg)
        DG1.Columns(6).Visible = False
        'DG1.Columns(7).Visible = False
        DG1.Columns(8).Visible = False
        TextBox1.Focus()
    End Sub


    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If ComboBox1.Text = "" Then
            MsgBox("برجاء ادخال الوحدة")
            Exit Sub
        End If

        If TextBox1.Text = "" Then
            MsgBox("برجاء ادخال الكميه")
            Exit Sub
        End If
        If TextBox3.Enabled = True Then
            If TextBox3.Text = "" Then
                MsgBox("Batch برجاء ادخال ")
                Exit Sub
            End If
        End If
        If TextBox2.Enabled = True Then
            If TextBox2.Text = "" Then
                MsgBox("Barcode برجاء ادخال ")
                Exit Sub
            End If
        End If
        'If TextBox2.Enabled = True And TextBox3.Enabled = True Then
        '    If TextBox3.Text = "" Then
        '        MsgBox("Batch برجاء ادخال ") : Exit Sub
        '    End If
        '    If TextBox2.Text = "" Then
        '        MsgBox("Barcode برجاء ادخال ") : Exit Sub
        '    End If
        'End If
        'REM ========== Check Dublicat
        For Each row1 As DataGridViewRow In DG1.Rows
            If row1.Cells.Item("UOM").Value = ComboBox1.Text And row1.Cells.Item("Lot").Value = TextBox3.Text And row1.Cells.Item("BarCode").Value = TextBox2.Text Then
                MsgBox("تم ادخال هذة الوحده من قبل ") : Exit Sub
            End If
        Next

        Dim NetUOMQty As Integer
        If TextBox8.Text = "RCV" Then
            NetUOMQty = Val(TextBox1.Text) * 1
        ElseIf TextBox8.Text = "Issue" Then
            NetUOMQty = Val(TextBox1.Text) * -1
        End If

        Dim dd As String = DateTimePicker1.Value.ToString("yyyy/MM/dd")
        REM ================ split Qty
        DG1.AllowUserToAddRows = False
        Dim row As DataRow = ds.Tables(0).NewRow
        row.Item(0) = ComboBox1.Text
        row.Item(1) = Val(TextBox1.Text)
        row.Item(2) = TextBox3.Text
        row.Item(3) = TextBox2.Text
        row.Item(4) = Val(TextBox1.Text * factor)
        row.Item(5) = dd
        row.Item(6) = NetUOMQty
        row.Item(7) = "Insert"
        ds.Tables(0).Rows.Add(row)

        'If CheckBox1.Checked = True Then
        '    DG1.AllowUserToAddRows = False
        '    For i As Integer = 0 To Val(TextBox1.Text) - 1
        '        Dim row As DataRow = ds.Tables(0).NewRow
        '        row.Item(0) = ComboBox1.Text
        '        row.Item(1) = 1
        '        row.Item(2) = TextBox3.Text
        '        row.Item(3) = TextBox2.Text
        '        row.Item(4) = Val(1 * factor)
        '        row.Item(5) = dd
        '        row.Item(6) = NetUOMQty

        '        ds.Tables(0).Rows.Add(row)

        '    Next
        'Else
        '    DG1.AllowUserToAddRows = False
        '    Dim row As DataRow = ds.Tables(0).NewRow
        '    row.Item(0) = ComboBox1.Text
        '    row.Item(1) = Val(TextBox1.Text)
        '    row.Item(2) = TextBox3.Text
        '    row.Item(3) = TextBox2.Text
        '    row.Item(4) = Val(TextBox1.Text * factor)
        '    row.Item(5) = dd
        '    row.Item(6) = NetUOMQty
        '    ds.Tables(0).Rows.Add(row)
        'End If
        REM ================ split Qty

        REM==================================== Change Control
        DG1.EndEdit()
        REM ====== to foucs 
        If TextBox3.Enabled = True Then
            TextBox3.Focus()
        Else
            TextBox2.Focus()
        End If
        TextBox2.Text = ""
        TextBox3.Text = ""
    End Sub
    Sub adduom(to1 As Integer)



    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox4.CheckedChanged

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        If DG1.Rows.Count = 0 Then
            MsgBox("لا يوجد بيانات للحفظ برجاء التاكد")
            Exit Sub
        End If
        InsertAlltransaction()
        'REM ------------------- Save Header
        'arg = " if not exists (SELECT Indi, Serial, WhNo FROM Invo WHERE (Indi = '" & TextBox8.Text & "') AND (Serial = '" & TextBox4.Text & "') AND (WhNo = '" & TextBox7.Text & "')) "
        'arg = arg & " BEGIN "
        'arg = arg & " INSERT INTO Invo (Indi, Serial, WhNo, Date1, Remarks, user_id, trx_date)"
        'arg = arg & " VALUES ('" & TextBox8.Text & "','" & TextBox4.Text & "','" & TextBox7.Text & "','" & Inventory_form.DateTimePicker1.Value.ToString("yyyy/MM/dd") & "','" & Inventory_form.TextBox4.Text & "','" & user_id & "', getdate())"
        'arg = arg & " END"
        'If conn.State = ConnectionState.Closed Then conn.Open()
        'Dim cmd3 As New System.Data.SqlClient.SqlCommand(arg, conn)
        'cmd3.ExecuteNonQuery()
        'REM ================= Delete Before Save serial History == Details
        'arg = "DELETE FROM Serial_Details WHERE Indi = '" & TextBox8.Text & "' and Serial = '" & TextBox4.Text & "' and WhNo ='" & TextBox7.Text & "' and ItemNo ='" & TextBox5.Text & "'"
        'If conn.State = ConnectionState.Closed Then conn.Open()
        'Dim cmd2 As New System.Data.SqlClient.SqlCommand(arg, conn)
        'cmd2.ExecuteNonQuery()
        REM ================= Delete Before Save serial History == Details
        'arg = "DELETE FROM Move WHERE Indi = '" & TextBox8.Text & "' and Serial = '" & TextBox4.Text & "' and WhNo ='" & TextBox7.Text & "' and Item ='" & TextBox5.Text & "'"
        'If conn.State = ConnectionState.Closed Then conn.Open()
        'Dim cmd5 As New System.Data.SqlClient.SqlCommand(arg, conn)
        'cmd5.ExecuteNonQuery()
        ''Dim u As String
        'REM ================= save serial History == Details
        'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 'DG1.Rows.Count - 1
        '    arg = "INSERT INTO Serial_Details(Indi, Serial, WhNo, ItemNo, UOM, batch, SerNo, Qty, NetUOMQty , NetQty, user_id, trx_date)"
        '    arg = arg & " VALUES('" & TextBox8.Text & "','" & TextBox4.Text & "' ,'" & TextBox7.Text & "' ,'" & TextBox5.Text & "' ,'" & DG1.Rows(i).Cells("UOM").Value & "','" & DG1.Rows(i).Cells("lot").Value.ToString & "' ,'" & DG1.Rows(i).Cells("barcode").Value & "' , '" & Val(DG1.Rows(i).Cells("qty").Value) & "','" & Val(DG1.Rows(i).Cells("NetUOMQty").Value) & "','" & Val(DG1.Rows(i).Cells("NetQty").Value.ToString) & "' , '" & user_id & "',getdate())"
        '    If conn.State = ConnectionState.Closed Then conn.Open()
        '    Dim cmd1 As New System.Data.SqlClient.SqlCommand(arg, conn)
        '    cmd1.ExecuteNonQuery()
        'Next
        'REM ================= save Move
        'arg = " INSERT INTO  Move (Indi, Serial, WhNo, Item, UOM, Qty, NetUomQty, NetQty, user_id, trx_date, Date1)"
        'arg = arg & " SELECT Indi, Serial, WhNo, ItemNo, UOM, SUM(Qty) AS Qty, SUM(NetUOMQty) AS NetUOMQty, SUM(NetQty) AS NetQty, user_id, trx_date ,'" & Inventory_form.DateTimePicker1.Value.ToString("yyyy/MM/dd") & "'"
        'arg = arg & " FROM Serial_Details GROUP BY Indi, Serial, WhNo, ItemNo, UOM, user_id, trx_date"
        'arg = arg & "  HAVING (Indi = '" & TextBox8.Text & "') AND (Serial = '" & TextBox4.Text & "') AND (WhNo = '" & TextBox7.Text & "') AND (ItemNo = '" & TextBox5.Text & "')"
        'If conn.State = ConnectionState.Closed Then conn.Open()
        'Dim cmd4 As New System.Data.SqlClient.SqlCommand(arg, conn)
        'cmd4.ExecuteNonQuery()
        REM ============= Refrish DG Inventory Form 
        Inventory_form.loaddata()
        Dispose()
    End Sub

    Private Sub TextBox4_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        arg = "SELECT UOM, factor FROM  items_uom WHERE (item = N'" & TextBox5.Text & "') and UOM = '" & ComboBox1.Text & "'"
        Dim cmd3 As SqlCommand = New SqlCommand(arg, conn)
        Dim reader3 As SqlDataReader = cmd3.ExecuteReader()
        If reader3.HasRows = True Then
            reader3.Read()
            factor = reader3("factor").ToString
        End If
        reader3.Close()
    End Sub

    Private Sub InsertAlltransaction()
        REM ========check serial
        If Inventory_form.DG1.Rows.Count = 0 Then
            Dim newserial As String = next_serial("Serial", "Invo", TextBox8.Text, TextBox7.Text)
            If Inventory_form.TextBox1.Text <> newserial Then
                Inventory_form.TextBox1.Text = newserial
                TextBox4.Text = newserial
                MsgBox(" تم الحفظ برقم مسلسل  " & Inventory_form.TextBox1.Text)
            End If
        End If

        REM === to Commit transaction
        Dim command As SqlCommand = conn.CreateCommand()
        Dim transaction As SqlTransaction
        ' Start transaction
        transaction = conn.BeginTransaction("SampleTransaction")
        command.Connection = conn
        command.Transaction = transaction

        Try
            REM ------------------- Save Header
            arg = " if not exists (SELECT Indi, Serial, WhNo FROM Invo WHERE (Indi = '" & TextBox8.Text & "') AND (Serial = '" & TextBox4.Text & "') AND (WhNo = '" & TextBox7.Text & "')) "
            arg = arg & " BEGIN "
            arg = arg & " INSERT INTO Invo (Indi, Serial, WhNo, Date1, Remarks,Posted, user_id, trx_date, CCTYPE, CCCODE, CCREF)"
            arg = arg & " VALUES ('" & TextBox8.Text & "','" & TextBox4.Text & "','" & TextBox7.Text & "','" & Inventory_form.DateTimePicker1.Value.ToString("yyyy/MM/dd") & "','" & Inventory_form.TextBox4.Text & "','0','" & user_id & "', getdate(),'" & Inventory_form.TextBox8.Text & "','" & Inventory_form.TextBox7.Text & "','" & Inventory_form.TextBox6.Text & "')"
            arg = arg & " END"
            command.CommandText = arg
            command.ExecuteNonQuery()

            'REM ================= Delete Before Save serial History == Details
            'Dim arg1 As String = "DELETE FROM Serial_Details WHERE Indi = '" & TextBox8.Text & "' and Serial = '" & TextBox4.Text & "' and WhNo ='" & TextBox7.Text & "' and ItemNo ='" & TextBox5.Text & "'"
            'command.CommandText = arg1
            'command.ExecuteNonQuery()

            REM ================= Delete Before Save serial History == Details
            Dim arg2 As String = "DELETE FROM Move WHERE Indi = '" & TextBox8.Text & "' and Serial = '" & TextBox4.Text & "' and WhNo ='" & TextBox7.Text & "' and Item ='" & TextBox5.Text & "'"
            command.CommandText = arg2
            command.ExecuteNonQuery()
            'Dim u As String
            REM ================= save serial History == Details
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 'DG1.Rows.Count - 1
                If DG1.Rows(i).Cells("Status").Value <> "Old" Then
                    Dim d As Date = DG1.Rows(i).Cells("ExpDate").Value.ToString
                    If DG1.Rows(i).Cells("Status").Value = "Insert" Then
                        Dim arg3 As String = "INSERT INTO Serial_Details(Indi, Serial, WhNo, ItemNo, UOM, batch, SerNo, Qty, NetUOMQty , NetQty, user_id, trx_date,ExpDate,Posted)"
                        arg3 = arg3 & " VALUES('" & TextBox8.Text & "','" & TextBox4.Text & "' ,'" & TextBox7.Text & "' ,'" & TextBox5.Text & "' ,'" & DG1.Rows(i).Cells("UOM").Value & "','" & DG1.Rows(i).Cells("lot").Value.ToString & "' ,'" & DG1.Rows(i).Cells("barcode").Value & "' , '" & Val(DG1.Rows(i).Cells("qty").Value) & "','" & Val(DG1.Rows(i).Cells("NetUOMQty").Value) & "','" & Val(DG1.Rows(i).Cells("NetQty").Value.ToString) & "' , '" & user_id & "',getdate(),'" & d.ToString("yyyy/MM/dd") & "','0')"
                        command.CommandText = arg3
                        command.ExecuteNonQuery()
                    ElseIf DG1.Rows(i).Cells("Status").Value = "Delete" Then
                        Dim arg1 As String = "DELETE FROM Serial_Details WHERE RowID = '" & DG1.Rows(i).Cells("RowID").Value & "'"
                        command.CommandText = arg1
                        command.ExecuteNonQuery()
                    End If
                End If
            Next
            REM ================= save Move
            Dim arg4 As String = " INSERT INTO  Move (Indi, Serial, WhNo, Item, UOM, Qty, NetUomQty, NetQty, user_id, trx_date, Date1)"
            arg4 = arg4 & " SELECT Indi, Serial, WhNo, ItemNo, UOM, SUM(Qty) AS Qty, SUM(NetUOMQty) AS NetUOMQty, SUM(NetQty) AS NetQty, '" & user_id & "', getdate() ,'" & Inventory_form.DateTimePicker1.Value.ToString("yyyy/MM/dd") & "'"
            arg4 = arg4 & " FROM Serial_Details GROUP BY Indi, Serial, WhNo, ItemNo, UOM "
            arg4 = arg4 & "  HAVING (Indi = '" & TextBox8.Text & "') AND (Serial = '" & TextBox4.Text & "') AND (WhNo = '" & TextBox7.Text & "') AND (ItemNo = '" & TextBox5.Text & "')"
            command.CommandText = arg4
            command.ExecuteNonQuery()

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
                
                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                Console.WriteLine("  Message: {0}", ex2.Message)
            End Try
        End Try

    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar <> Microsoft.VisualBasic.ChrW(Keys.Return) Then Exit Sub
        If TextBox2.Enabled = False Then
            Button1_Click(sender, e)
        Else
            TextBox2.Focus()
        End If
    End Sub


    Private Sub TextBox3_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar <> Microsoft.VisualBasic.ChrW(Keys.Return) Then Exit Sub
        Button1_Click(sender, e)
    End Sub

    Private Sub TextBox2_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub DeleteItemMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DeleteItemMenuItem.Click
        If DG1.CurrentRow.Cells("Status").Value = "Insert" Then
            DG1.Rows.RemoveAt(DG1.CurrentRow.Index)
        ElseIf DG1.CurrentRow.Cells("Status").Value = "Old" Then
            DG1.CurrentRow.Cells("Status").Value = "Delete"
        ElseIf DG1.CurrentRow.Cells("Status").Value = "Delete" Then
            DG1.CurrentRow.Cells("Status").Value = "Old"
        End If

    End Sub
End Class