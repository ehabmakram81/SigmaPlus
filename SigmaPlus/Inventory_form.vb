Imports System.Data.SqlClient

Public Class Inventory_form

    Dim factor, items As String

    Private Sub NewMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles NewMenuItem.Click
        TextBox1.Text = next_serial("Serial", "Serial_Details", Label6.Text, Label5.Text)

        'TextSerial.Text = next_serial("PRSerial", "Conprh", "", "C" & Mid(ComSite.Text, 1, 1))
        'TextSerial.Text = "C" & Mid(ComSite.Text, 1, 1) & Mid(Year(Now), 3, 4) & Format(Val(TextSerial.Text), "00000")

        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        ds.Clear()
        REM==================================== Change Control
        GroupBox1.Enabled = True
        GroupBox2.Enabled = False
        GroupBox3.Enabled = False
        SaveMenuItem.Text = "حفظ"

    End Sub

    Private Sub Inventory_form_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Dispose()
    End Sub

    Private Sub Inventory_form_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Call center(Me)


        Label5.Text = whno
        Label6.Text = indi
        NewMenuItem_Click(sender, e)
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode <> Keys.Enter Then Exit Sub
        items = TextBox2.Text
        REM ===== validat 0 and Space
        If Not Validat_Code(TextBox2.Text) Then
            MsgBox(ErrDisc)
            Exit Sub
        End If


        REM ================Retrive Date
        RetriveDate(items)

    End Sub
    Sub RetriveDate(code As String)
        REM ================Change Controls
        ComboBox1.Visible = False
        TextBox5.Visible = False
        Inventory_form_popup.CheckBox4.Enabled = False
        Inventory_form_popup.TextBox2.Enabled = False
        Inventory_form_popup.BarCode.ReadOnly = True
        Inventory_form_popup.CheckBox2.Enabled = False
        Inventory_form_popup.TextBox3.Enabled = False
        Inventory_form_popup.Lot.ReadOnly = True

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
            End If
            reader1.Close()
        End If
        If TextBox3.Text = "" Then Exit Sub
        Inventory_form_popup.TextBox4.Text = TextBox1.Text
        Inventory_form_popup.TextBox5.Text = code
        Inventory_form_popup.TextBox6.Text = TextBox3.Text
        Inventory_form_popup.TextBox7.Text = Label5.Text
        Inventory_form_popup.TextBox8.Text = Label6.Text

        REM ================================ check tracking 
        Select Case Track
            Case 1, 0

                REM =========================== load UOM
                ComboBox1.Items.Clear()
                arg = "SELECT UOM, factor FROM  items_uom WHERE (item = N'" & TextBox2.Text & "')"
                Dim cmd3 As SqlCommand = New SqlCommand(arg, conn)
                Dim reader3 As SqlDataReader = cmd3.ExecuteReader()
                If reader3.HasRows = True Then
                    ComboBox1.Items.Add("")
                    Do While reader3.Read()
                        ComboBox1.Items.Add(reader3("UOM").ToString)
                    Loop
                End If
                reader3.Close()
                ComboBox1.Visible = True
                TextBox5.Visible = True
                Exit Sub

            Case 2
                Inventory_form_popup.CheckBox4.Enabled = True
                Inventory_form_popup.TextBox2.Enabled = True
                Inventory_form_popup.BarCode.ReadOnly = False
            Case 3
                Inventory_form_popup.CheckBox2.Enabled = True
                Inventory_form_popup.TextBox3.Enabled = True
                Inventory_form_popup.Lot.ReadOnly = False
            Case 4
                Inventory_form_popup.CheckBox4.Enabled = True
                Inventory_form_popup.TextBox2.Enabled = True
                Inventory_form_popup.BarCode.ReadOnly = False
                Inventory_form_popup.CheckBox2.Enabled = True
                Inventory_form_popup.TextBox3.Enabled = True
                Inventory_form_popup.Lot.ReadOnly = False

        End Select
        Inventory_form_popup.ShowDialog()
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
        'ds.Clear()
        'arg = "SELECT Serial_Details.ItemNo as Itemno, items.name as ItemName, Serial_Details.UOM as UOM, SUM(Serial_Details.Qty) AS Qty, SUM(Serial_Details.NetQty) AS NetQty FROM Serial_Details INNER JOIN   items ON Serial_Details.ItemNo = items.code  GROUP BY Serial_Details.Indi, Serial_Details.Serial, Serial_Details.WhNo, Serial_Details.ItemNo, Serial_Details.UOM, items.name HAVING (Indi = N'" & Label6.Text & "') AND (Serial = N'" & TextBox1.Text & "') AND (WhNo = N'" & Label5.Text & "')"
        arg = "SELECT       dbo.Move.Item AS Itemno, dbo.items.name AS ItemName, dbo.Move.UOM, dbo.Move.Qty, dbo.Move.NetQty, dbo.Move.RowID FROM            dbo.Move INNER JOIN dbo.items ON dbo.Move.Item = dbo.items.code  GROUP BY dbo.Move.Item, dbo.Move.UOM, dbo.Move.Qty, dbo.Move.NetQty, dbo.Move.RowID, dbo.Move.Serial, dbo.items.name, dbo.Move.WhNo, dbo.Move.Indi HAVING       (dbo.Move.Serial = '" & TextBox1.Text & "') AND (dbo.Move.Indi = '" & Label6.Text & "') AND (dbo.Move.WhNo = '" & Label5.Text & "')"
        adp = New SqlDataAdapter(arg, conn)
        dt = New DataTable
        ds = New DataSet()
        adp.Fill(ds)
        DG1.DataSource = ds.Tables(0)
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
            If row.Cells.Item("UOM").Value = ComboBox1.Text Then
                MsgBox("تم ادخال هذة الوحده من قبل ") : Exit Sub
            End If
        Next
        REM ================= save Serial_Details

        arg = "INSERT INTO Serial_Details(Indi, Serial, WhNo, ItemNo, UOM, Qty, NetUOMQty, NetQty, user_id, trx_date)"
        arg = arg & " VALUES('" & Label6.Text & "','" & TextBox1.Text & "' ,'" & Label5.Text & "' ,'" & items & "' ,'" & ComboBox1.Text & "', '" & Val(TextBox5.Text) & "','" & factor & "','" & Val(TextBox5.Text * factor) & "' , '" & user_id & "', getdate())"
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim cmd1 As New System.Data.SqlClient.SqlCommand(arg, conn)
        cmd1.ExecuteNonQuery()

        REM ================= save Move
        arg = "INSERT INTO Move(Indi, Serial, WhNo, Item, UOM, Qty, NetUOMQty, NetQty, user_id, trx_date,Date1)"
        arg = arg & " VALUES('" & Label6.Text & "','" & TextBox1.Text & "' ,'" & Label5.Text & "' ,'" & items & "' ,'" & ComboBox1.Text & "', '" & Val(TextBox5.Text) & "','" & factor & "','" & Val(TextBox5.Text * factor) & "' , '" & user_id & "', getdate(),'" & DateTimePicker1.Value.ToString("yyyy/MM/dd") & "')"
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim cmd2 As New System.Data.SqlClient.SqlCommand(arg, conn)
        cmd2.ExecuteNonQuery()

        REM ================== retdata
        loaddata()
        REM ================== change control
        TextBox2.Enabled = True
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox5.Text = ""
        ComboBox1.Text = ""
        ComboBox1.Visible = False
        TextBox5.Visible = False

    End Sub

    Private Sub DG1_CellDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DG1.CellDoubleClick
        TextBox2.Text = DG1.CurrentRow.Cells("Item").Value
        RetriveDate(DG1.CurrentRow.Cells("Item").Value)
    End Sub

    Private Sub TextBox2_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox5_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub DG1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DG1.CellContentClick

    End Sub
End Class