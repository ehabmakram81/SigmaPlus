Imports System.Data.SqlClient

Public Class Inventory_form_popup
    Dim factor As String

    Private Sub Inventory_form_popup_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Call center(Me)

        REM =========================== load items UOM
        ComboBox1.Items.Clear()
        arg = "SELECT UOM, factor FROM  items_uom WHERE (item = N'" & TextBox5.Text & "')"
        Dim cmd3 As SqlCommand = New SqlCommand(arg, conn)
        Dim reader3 As SqlDataReader = cmd3.ExecuteReader()
        If reader3.HasRows = True Then
            ComboBox1.Items.Add("")
            Do While reader3.Read()
                ComboBox1.Items.Add(reader3("UOM").ToString)
            Loop
        End If
        reader3.Close()

        loaddata()
    End Sub

    Public Sub loaddata()
        'ds1.Clear()
        'arg = "SELECT  dbo.Serial_Details.UOM, dbo.Serial_Details.Qty, dbo.Serial_Details.batch, dbo.Serial_Details.SerNo as barcode , dbo.Serial_Details.NetQty as NQty FROM dbo.Serial_Details INNER JOIN   dbo.items ON dbo.Serial_Details.ItemNo = dbo.items.code GROUP BY dbo.Serial_Details.Indi, dbo.Serial_Details.Serial, dbo.Serial_Details.WhNo, dbo.Serial_Details.ItemNo, dbo.Serial_Details.UOM, dbo.items.name, dbo.Serial_Details.batch, dbo.Serial_Details.SerNo, dbo.Serial_Details.Qty, dbo.Serial_Details.NetQty, dbo.Serial_Details.RowID HAVING (Indi = N'" & TextBox8.Text & "') AND (Serial = N'" & TextBox4.Text & "') AND (WhNo = N'" & TextBox7.Text & "') AND (ItemNo = N'" & TextBox5.Text & "')"
        arg = "SELECT UOM, Qty, batch , SerNo as barcode, NetQty as NQty, ExpDate ,NetUOMQty FROM  dbo.Serial_Details WHERE (Indi = '" & TextBox8.Text & "') AND (Serial = '" & TextBox4.Text & "') AND (WhNo = '" & TextBox7.Text & "') AND (ItemNo = '" & TextBox5.Text & "')"
        adp1 = New SqlDataAdapter(arg, conn)
        dt1 = New DataTable
        ds1 = New DataSet()
        adp1.Fill(ds1)
        DG1.DataSource = ds1.Tables(0)
        DG1.Columns(6).Visible = False

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
        'REM ========== Check Dublicat
        'For Each row As DataGridViewRow In DG1.Rows
        '    If row.Cells.Item("UOM").Value = ComboBox1.Text Then
        '        MsgBox("تم ادخال هذة الوحده من قبل ") : Exit Sub
        '    End If
        'Next

        Dim dd As String = DateTimePicker1.Value.ToString("yyyy/MM/dd")
        REM ================ split Qty
        If CheckBox1.Checked = True Then
            DG1.AllowUserToAddRows = False
            For i As Integer = 0 To Val(TextBox1.Text) - 1
                DG1.AllowUserToAddRows = False
                Dim row As DataRow = ds1.Tables(0).NewRow
                row.Item(0) = ComboBox1.Text
                row.Item(1) = 1
                row.Item(2) = TextBox3.Text
                row.Item(3) = TextBox2.Text
                row.Item(4) = Val(1 * factor)
                row.Item(5) = dd
                row.Item(6) = factor
                ds1.Tables(0).Rows.Add(row)

            Next
        Else
            DG1.AllowUserToAddRows = False
            Dim row As DataRow = ds1.Tables(0).NewRow
            row.Item(0) = ComboBox1.Text
            row.Item(1) = Val(TextBox1.Text)
            row.Item(2) = TextBox3.Text
            row.Item(3) = TextBox2.Text
            row.Item(4) = Val(TextBox1.Text * factor)
            row.Item(5) = dd
            row.Item(6) = factor
            ds1.Tables(0).Rows.Add(row)
        End If
        REM ================ split Qty

        REM==================================== Change Control
        DG1.EndEdit()
        ComboBox1.Text = ""
        TextBox1.Text = ""
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

        REM ------------------- Save Header
        arg = " if not exists (SELECT Indi, Serial, WhNo FROM Invo WHERE (Indi = '" & TextBox8.Text & "') AND (Serial = '" & TextBox4.Text & "') AND (WhNo = '" & TextBox7.Text & "')) "
        arg = arg & " BEGIN "
        arg = arg & " INSERT INTO Invo (Indi, Serial, WhNo, Date1, Remarks, user_id, trx_date)"
        arg = arg & " VALUES ('" & TextBox8.Text & "','" & TextBox4.Text & "','" & TextBox7.Text & "','" & Inventory_form.DateTimePicker1.Value.ToString("yyyy/MM/dd") & "','" & Inventory_form.TextBox4.Text & "','" & user_id & "', getdate())"
        arg = arg & " END"
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim cmd3 As New System.Data.SqlClient.SqlCommand(arg, conn)
        cmd3.ExecuteNonQuery()
        REM ================= Delete Before Save serial History == Details
        arg = "DELETE FROM Serial_Details WHERE Indi = '" & TextBox8.Text & "' and Serial = '" & TextBox4.Text & "' and WhNo ='" & TextBox7.Text & "' and ItemNo ='" & TextBox5.Text & "'"
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim cmd2 As New System.Data.SqlClient.SqlCommand(arg, conn)
        cmd2.ExecuteNonQuery()
        REM ================= Delete Before Save serial History == Details
        arg = "DELETE FROM Move WHERE Indi = '" & TextBox8.Text & "' and Serial = '" & TextBox4.Text & "' and WhNo ='" & TextBox7.Text & "' and Item ='" & TextBox5.Text & "'"
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim cmd5 As New System.Data.SqlClient.SqlCommand(arg, conn)
        cmd5.ExecuteNonQuery()
        'Dim u As String
        REM ================= save serial History == Details
        For i As Integer = 0 To ds1.Tables(0).Rows.Count - 1 'DG1.Rows.Count - 1
            arg = "INSERT INTO Serial_Details(Indi, Serial, WhNo, ItemNo, UOM, batch, SerNo, Qty, NetUOMQty , NetQty, user_id, trx_date)"
            arg = arg & " VALUES('" & TextBox8.Text & "','" & TextBox4.Text & "' ,'" & TextBox7.Text & "' ,'" & TextBox5.Text & "' ,'" & DG1.Rows(i).Cells("UOM").Value & "','" & DG1.Rows(i).Cells("lot").Value.ToString & "' ,'" & DG1.Rows(i).Cells("barcode").Value & "' , '" & Val(DG1.Rows(i).Cells("qty").Value) & "','" & Val(DG1.Rows(i).Cells("NetUOMQty").Value) & "','" & Val(DG1.Rows(i).Cells("NetQty").Value.ToString) & "' , '" & user_id & "',getdate())"
            If conn.State = ConnectionState.Closed Then conn.Open()
            Dim cmd1 As New System.Data.SqlClient.SqlCommand(arg, conn)
            cmd1.ExecuteNonQuery()
        Next
        REM ================= save Move
        arg = " INSERT INTO  Move (Indi, Serial, WhNo, Item, UOM, Qty, NetUomQty, NetQty, user_id, trx_date, Date1)"
        arg = arg & " SELECT Indi, Serial, WhNo, ItemNo, UOM, SUM(Qty) AS Qty, SUM(NetUOMQty) AS NetUOMQty, SUM(NetQty) AS NetQty, user_id, trx_date ,'" & Inventory_form.DateTimePicker1.Value.ToString("yyyy/MM/dd") & "'"
        arg = arg & " FROM Serial_Details GROUP BY Indi, Serial, WhNo, ItemNo, UOM, user_id, trx_date"
        arg = arg & "  HAVING (Indi = '" & TextBox8.Text & "') AND (Serial = '" & TextBox4.Text & "') AND (WhNo = '" & TextBox7.Text & "') AND (ItemNo = '" & TextBox5.Text & "')"
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim cmd4 As New System.Data.SqlClient.SqlCommand(arg, conn)
        cmd4.ExecuteNonQuery()
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
        REM === to Commit transaction
        'Dim command As SqlCommand = conn.CreateCommand()
        'Dim transaction As SqlTransaction

        '' Start transaction
        'transaction = conn.BeginTransaction("SampleTransaction")
        'command.Connection = conn
        'command.Transaction = transaction

        'Try
        '    command.CommandText = _
        '      "Insert into Region (RegionID, RegionDescription) VALUES (100, 'Description')"
        '    command.ExecuteNonQuery()
        '    command.CommandText = _
        '      "Insert into Region (RegionID, RegionDescription) VALUES (101, 'Description')"

        '    command.ExecuteNonQuery()

        '    ' Attempt to commit the transaction.
        '    transaction.Commit()
        '    Console.WriteLine("Both records are written to database.")

        'Catch ex As Exception
        '    Console.WriteLine("Commit Exception Type: {0}", ex.GetType())
        '    Console.WriteLine("  Message: {0}", ex.Message)

        '    ' Attempt to roll back the transaction. 
        '    Try
        '        transaction.Rollback()

        '    Catch ex2 As Exception
        '        ' This catch block will handle any errors that may have occurred 
        '        ' on the server that would cause the rollback to fail, such as 
        '        ' a closed connection.
        '        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
        '        Console.WriteLine("  Message: {0}", ex2.Message)
        '    End Try
        ' End Try

    End Sub
   

End Class