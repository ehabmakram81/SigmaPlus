Imports System.Data.SqlClient

Public Class Inventory_form

    Dim factor, items As String

    Private Sub NewMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles NewMenuItem.Click
        TextBox1.Text = next_serial("Serial", "Serial_Details", Label6.Text, Label5.Text)
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        DG1.DataSource = Nothing

        REM==================================== Change Control
        GroupBox1.Enabled = True
        GroupBox2.Enabled = False
        GroupBox3.Enabled = False
        SaveMenuItem.Text = "حفظ"
        ComboBox1.Enabled = False
        TextBox5.Enabled = False
    End Sub

    Private Sub Inventory_form_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Dispose()
    End Sub

    Private Sub Inventory_form_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Call center(Me)
        REM --------------- parameters
        Label5.Text = whno
        Label6.Text = indi
        NewMenuItem_Click(sender, e)
    End Sub
    Sub RetriveDate(code As String)
        REM ================Change Controls
        ComboBox1.Enabled = False
        TextBox5.Enabled = False
        Inventory_Details.CheckBox4.Enabled = False
        Inventory_Details.TextBox2.Enabled = False
        Inventory_Details.BarCode.ReadOnly = True
        Inventory_Details.CheckBox2.Enabled = False
        Inventory_Details.TextBox3.Enabled = False
        Inventory_Details.Lot.ReadOnly = True

        Dim Track As Int16
        REM ===== Retrive Date 
        If code <> "" Then
            arg = "SELECT code, name, aname, UOM, Type1, family, cost, ReOrder, wght, SerialTrack, Active, Remarks FROM items WHERE (code = '" & code & "')"
            Dim cmd2 As SqlCommand = New SqlCommand(arg, conn)
            Dim reader1 As SqlDataReader = cmd2.ExecuteReader()
            If reader1.HasRows = True Then
                reader1.Read()
                TextBox3.Text = reader1("name").ToString
                Track = Val(reader1("SerialTrack").ToString)
                'ComboBox1.Items.Clear()
                'ComboBox1.Items.Add("")
                'ComboBox1.Items.Add(reader1("UOM").ToString)
            End If
            reader1.Close()
        End If
        If TextBox3.Text = "" Then Exit Sub
        Label9.Text = code
        Label10.Text = TextBox3.Text
        Inventory_Details.TextBox4.Text = TextBox1.Text
        Inventory_Details.TextBox5.Text = code
        Inventory_Details.TextBox6.Text = TextBox3.Text
        Inventory_Details.TextBox7.Text = Label5.Text
        Inventory_Details.TextBox8.Text = Label6.Text

        REM ================================ check tracking 
        Select Case Track
            Case 1, 0
                TextBox5.Text = ""
                REM =========================== load UOM
                ' ComboBox1.Items.Clear()
                arg = "SELECT UOM, factor FROM Items_UOM_all WHERE (code = N'" & TextBox2.Text & "')"
                fill_Combo(ComboBox1, arg)
                ComboBox1.Enabled = True
                TextBox5.Enabled = True
                If ComboBox1.Items.Count = 2 Then
                    ComboBox1.SelectedIndex = 1
                    TextBox5.Focus()
                End If
                Exit Sub

            Case 2
                Inventory_Details.CheckBox4.Enabled = True
                Inventory_Details.TextBox2.Enabled = True
                Inventory_Details.BarCode.ReadOnly = False
            Case 3
                Inventory_Details.CheckBox2.Enabled = True
                Inventory_Details.TextBox3.Enabled = True
                Inventory_Details.Lot.ReadOnly = False
            Case 4
                Inventory_Details.CheckBox4.Enabled = True
                Inventory_Details.TextBox2.Enabled = True
                Inventory_Details.BarCode.ReadOnly = False
                Inventory_Details.CheckBox2.Enabled = True
                Inventory_Details.TextBox3.Enabled = True
                Inventory_Details.Lot.ReadOnly = False

        End Select
        Inventory_Details.ShowDialog()
        GroupBox1.Enabled = False
        GroupBox2.Enabled = True
        GroupBox3.Enabled = True
        'TextBox2.Enabled = False
    End Sub


    Private Sub ExitMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExitMenuItem.Click
        Close()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode <> Keys.Enter Then Exit Sub
        REM ===== validat 0 and Space
        If Not Validat_Code(TextBox1.Text) Then
            MsgBox(ErrDisc)
            Exit Sub
        End If
        REM ===== Retrive Date 
        loaddata()

        GroupBox1.Enabled = False
        GroupBox2.Enabled = True
        GroupBox3.Enabled = True
    End Sub

    Public Sub loaddata()       
        arg = "SELECT dbo.Move.Item AS Itemno, dbo.items.name AS ItemName, dbo.Move.UOM, dbo.Move.Qty, dbo.Move.NetQty, dbo.Move.RowID FROM            dbo.Move INNER JOIN dbo.items ON dbo.Move.Item = dbo.items.code  GROUP BY dbo.Move.Item, dbo.Move.UOM, dbo.Move.Qty, dbo.Move.NetQty, dbo.Move.RowID, dbo.Move.Serial, dbo.items.name, dbo.Move.WhNo, dbo.Move.Indi HAVING       (dbo.Move.Serial = '" & TextBox1.Text & "') AND (dbo.Move.Indi = '" & Label6.Text & "') AND (dbo.Move.WhNo = '" & Label5.Text & "')"
        fill_datagrid(DG1, arg)
        DG1.Columns(0).HeaderText = "كود الصنف"
        DG1.Columns(1).HeaderText = "اسم الصنف"
        DG1.Columns(2).HeaderText = "الوحده"
        DG1.Columns(3).HeaderText = "الكميه"
        DG1.Columns(0).Width = 200
        DG1.Columns(1).Width = 310
        DG1.Columns(2).Width = 100
        DG1.Columns(3).Width = 100

        DG1.Columns(4).Visible = False
        DG1.Columns(5).Visible = False
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged_1(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        REM =========================== load UOM

        arg = "SELECT UOM, factor FROM  items_uom WHERE (item = N'" & TextBox2.Text & "') and UOM = '" & ComboBox1.Text & "'"
        Dim cmd3 As SqlCommand = New SqlCommand(arg, conn)
        Dim reader3 As SqlDataReader = cmd3.ExecuteReader()
        If reader3.HasRows = True Then
            reader3.Read()
            factor = reader3("factor").ToString
        End If
        reader3.Close()
        TextBox5.Focus()
    End Sub

    Private Sub TextBox5_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyCode <> Keys.Enter Then Exit Sub
        REM ================= validat
        If items = "" Then
            MsgBox("برجاء إدخل الصنف ")
            Exit Sub
        End If
        If ComboBox1.Text = "" Then
            MsgBox("برجاء اختيار الوحده")
            Exit Sub
        End If
        If TextBox5.Text = "" Then
            MsgBox("برجاء ادخال الكميه")
            Exit Sub
        End If
        If Val(TextBox5.Text) < 1 Then
            MsgBox("برجاء ادخال الكميه")
            Exit Sub
        End If
        'REM ========== Check Dublicat
        For Each row As DataGridViewRow In DG1.Rows
            If row.Cells.Item("UOM").Value = ComboBox1.Text And row.Cells.Item("Itemno").Value = Label9.Text Then

                MsgBox("تم ادخال هذة الوحده من قبل ") : Exit Sub
            End If
        Next
        REM ================= save Commit
        InsertAlltransaction()

        'arg = "INSERT INTO Serial_Details(Indi, Serial, WhNo, ItemNo, UOM, Qty, NetUOMQty, NetQty, user_id, trx_date)"
        'arg = arg & " VALUES('" & Label6.Text & "','" & TextBox1.Text & "' ,'" & Label5.Text & "' ,'" & items & "' ,'" & ComboBox1.Text & "', '" & Val(TextBox5.Text) & "','" & factor & "','" & Val(TextBox5.Text * factor) & "' , '" & user_id & "', getdate())"
        'If conn.State = ConnectionState.Closed Then conn.Open()
        'Dim cmd1 As New System.Data.SqlClient.SqlCommand(arg, conn)
        'cmd1.ExecuteNonQuery()

        'REM ================= save Move
        'arg = "INSERT INTO Move(Indi, Serial, WhNo, Item, UOM, Qty, NetUOMQty, NetQty, user_id, trx_date,Date1)"
        'arg = arg & " VALUES('" & Label6.Text & "','" & TextBox1.Text & "' ,'" & Label5.Text & "' ,'" & items & "' ,'" & ComboBox1.Text & "', '" & Val(TextBox5.Text) & "','" & factor & "','" & Val(TextBox5.Text * factor) & "' , '" & user_id & "', getdate(),'" & DateTimePicker1.Value.ToString("yyyy/MM/dd") & "')"
        'If conn.State = ConnectionState.Closed Then conn.Open()
        'Dim cmd2 As New System.Data.SqlClient.SqlCommand(arg, conn)
        'cmd2.ExecuteNonQuery()

        REM ================== retdata
        loaddata()
        REM ================== change control
        TextBox2.Enabled = True
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox5.Text = ""
        Label10.Text = ""
        Label9.Text = ""
        ComboBox1.Text = ""
        ComboBox1.Visible = False
        TextBox5.Visible = False
        TextBox2.Focus()
    End Sub

    Private Sub DG1_CellDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DG1.CellDoubleClick
        TextBox2.Text = DG1.CurrentRow.Cells("Itemno").Value
        RetriveDate(DG1.CurrentRow.Cells("Itemno").Value)
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar <> Microsoft.VisualBasic.ChrW(Keys.Return) Then Exit Sub

        Call short_ha(TextBox2.Text, "", "items_view")

        If acc_master <> "" Then
            items = acc_master
            TextBox2.Text = acc_master
            TextBox3.Text = acc_name
        Else
            items = TextBox2.Text
        End If
        REM ================Retrive Date
        RetriveDate(items)
    End Sub
    Private Sub DG1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DG1.CellContentClick

    End Sub


    Private Sub TextBox3_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar <> Microsoft.VisualBasic.ChrW(Keys.Return) Then Exit Sub

        Call short_ha("", TextBox3.Text, "items_view")

        TextBox2.Text = acc_master
        TextBox3.Text = acc_name
        If acc_master <> "" Then
            items = acc_master
        Else
            Exit Sub
        End If
        REM ================Retrive Date
        RetriveDate(items)
    End Sub

    Private Sub DeleteItemMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DeleteItemMenuItem.Click
        Dim J = MsgBox(" هل تريد حذف صنف " & DG1.CurrentRow.Cells("Itemno").Value & " ", vbYesNo)
        If J <> 6 Then Exit Sub
        REM ================= Delete Before Save serial History == Details
        arg = "DELETE FROM Serial_Details WHERE Indi = '" & Label6.Text & "' and Serial = '" & TextBox1.Text & "' and WhNo ='" & Label5.Text & "' and ItemNo ='" & DG1.CurrentRow.Cells("Itemno").Value & "' and UOM ='" & DG1.CurrentRow.Cells("UOM").Value & "'"
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim cmd2 As New System.Data.SqlClient.SqlCommand(arg, conn)
        cmd2.ExecuteNonQuery()
        REM ================= Delete Before Save serial History == Details
        arg = "DELETE FROM Move WHERE Indi = '" & Label6.Text & "' and Serial = '" & TextBox1.Text & "' and WhNo ='" & Label5.Text & "' and Item ='" & DG1.CurrentRow.Cells("Itemno").Value & "' and UOM ='" & DG1.CurrentRow.Cells("UOM").Value & "'"
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim cmd5 As New System.Data.SqlClient.SqlCommand(arg, conn)
        cmd5.ExecuteNonQuery()
        If DG1.RowCount = 0 Then
            REM ------------------- Save Header
            arg = " DELETE FROM Invo WHERE (Indi = '" & Label6.Text & "') AND (Serial = '" & TextBox1.Text & "') AND (WhNo = '" & Label5.Text & "')) "
            If conn.State = ConnectionState.Closed Then conn.Open()
            Dim cmd3 As New System.Data.SqlClient.SqlCommand(arg, conn)
            cmd3.ExecuteNonQuery()
        End If
        loaddata()
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening

    End Sub

    Private Sub TextBox2_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub InsertAlltransaction()
        REM === to Commit transaction
        Dim command As SqlCommand = conn.CreateCommand()
        Dim transaction As SqlTransaction
        'Start transaction
        transaction = conn.BeginTransaction("SampleTransaction")
        command.Connection = conn
        command.Transaction = transaction

        Try

            REM ------------------- Save Details
            arg = "INSERT INTO Serial_Details(Indi, Serial, WhNo, ItemNo, UOM, Qty, NetUOMQty, NetQty, user_id, trx_date)"
            arg = arg & " VALUES('" & Label6.Text & "','" & TextBox1.Text & "' ,'" & Label5.Text & "' ,'" & items & "' ,'" & ComboBox1.Text & "', '" & Val(TextBox5.Text) & "','" & factor & "','" & Val(TextBox5.Text * factor) & "' , '" & user_id & "', getdate())"
            command.CommandText = arg
            command.ExecuteNonQuery()

            REM ================= save Move
            Dim arg1 As String = "INSERT INTO Move(Indi, Serial, WhNo, Item, UOM, Qty, NetUOMQty, NetQty, user_id, trx_date,Date1)"
            arg1 = arg1 & " VALUES('" & Label6.Text & "','" & TextBox1.Text & "' ,'" & Label5.Text & "' ,'" & items & "' ,'" & ComboBox1.Text & "', '" & Val(TextBox5.Text) & "','" & factor & "','" & Val(TextBox5.Text * factor) & "' , '" & user_id & "', getdate(),'" & DateTimePicker1.Value.ToString("yyyy/MM/dd") & "')"
            command.CommandText = arg1
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

    Private Sub TextBox5_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox5.TextChanged

    End Sub
End Class