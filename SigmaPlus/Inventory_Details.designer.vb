﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Inventory_Details
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.DG1 = New System.Windows.Forms.DataGridView()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteItemMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UOM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Lot = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BarCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExpDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NetQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NetUOMQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DG1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(761, 154)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(100, 27)
        Me.ComboBox1.TabIndex = 2
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.TextBox1.Location = New System.Drawing.Point(662, 154)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 27)
        Me.TextBox1.TabIndex = 1
        '
        'TextBox2
        '
        Me.TextBox2.Enabled = False
        Me.TextBox2.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.TextBox2.Location = New System.Drawing.Point(364, 154)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(150, 27)
        Me.TextBox2.TabIndex = 4
        '
        'TextBox3
        '
        Me.TextBox3.Enabled = False
        Me.TextBox3.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.TextBox3.Location = New System.Drawing.Point(513, 154)
        Me.TextBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(150, 27)
        Me.TextBox3.TabIndex = 3
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 128)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(96, 53)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "+"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Font = New System.Drawing.Font("Tahoma", 11.0!)
        Me.CheckBox1.Location = New System.Drawing.Point(761, 271)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(81, 22)
        Me.CheckBox1.TabIndex = 5
        Me.CheckBox1.Text = "Split Qty"
        Me.CheckBox1.UseVisualStyleBackColor = True
        Me.CheckBox1.Visible = False
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Enabled = False
        Me.CheckBox2.Font = New System.Drawing.Font("Tahoma", 11.0!)
        Me.CheckBox2.Location = New System.Drawing.Point(514, 128)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(147, 22)
        Me.CheckBox2.TabIndex = 6
        Me.CheckBox2.Text = "Create Auto Batch"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'DG1
        '
        Me.DG1.AllowUserToAddRows = False
        Me.DG1.AllowUserToDeleteRows = False
        Me.DG1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DG1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.UOM, Me.QTY, Me.Lot, Me.BarCode, Me.ExpDate, Me.NetQty, Me.NetUOMQty})
        Me.DG1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.DG1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.DG1.Location = New System.Drawing.Point(12, 188)
        Me.DG1.Name = "DG1"
        Me.DG1.ReadOnly = True
        Me.DG1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DG1.RowTemplate.Height = 30
        Me.DG1.Size = New System.Drawing.Size(891, 325)
        Me.DG1.TabIndex = 8
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Button2.Location = New System.Drawing.Point(12, 519)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(96, 40)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "Save"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Enabled = False
        Me.CheckBox4.Font = New System.Drawing.Font("Tahoma", 11.0!)
        Me.CheckBox4.Location = New System.Drawing.Point(362, 127)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(148, 22)
        Me.CheckBox4.TabIndex = 10
        Me.CheckBox4.Text = "Create Auto Serial "
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBox8)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.TextBox7)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.TextBox6)
        Me.GroupBox1.Controls.Add(Me.TextBox5)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TextBox4)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Enabled = False
        Me.GroupBox1.Location = New System.Drawing.Point(13, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(890, 118)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        '
        'TextBox8
        '
        Me.TextBox8.Location = New System.Drawing.Point(339, 51)
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TextBox8.Size = New System.Drawing.Size(187, 27)
        Me.TextBox8.TabIndex = 21
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(532, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 19)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "العمليه"
        '
        'TextBox7
        '
        Me.TextBox7.Location = New System.Drawing.Point(595, 51)
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TextBox7.Size = New System.Drawing.Size(198, 27)
        Me.TextBox7.TabIndex = 19
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(799, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 19)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "المخزن"
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(15, 81)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TextBox6.Size = New System.Drawing.Size(511, 27)
        Me.TextBox6.TabIndex = 17
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(532, 81)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TextBox5.Size = New System.Drawing.Size(261, 27)
        Me.TextBox5.TabIndex = 16
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(799, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 19)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = " الصنف"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(595, 16)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TextBox4.Size = New System.Drawing.Size(198, 27)
        Me.TextBox4.TabIndex = 14
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(799, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 19)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "رقم الأذن"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(245, 154)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(120, 27)
        Me.DateTimePicker1.TabIndex = 30
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteItemMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(148, 30)
        '
        'DeleteItemMenuItem
        '
        Me.DeleteItemMenuItem.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.DeleteItemMenuItem.Name = "DeleteItemMenuItem"
        Me.DeleteItemMenuItem.Size = New System.Drawing.Size(147, 26)
        Me.DeleteItemMenuItem.Text = "إلغاء صنف"
        '
        'UOM
        '
        Me.UOM.DataPropertyName = "UOM"
        Me.UOM.HeaderText = "الوحده"
        Me.UOM.Name = "UOM"
        Me.UOM.ReadOnly = True
        '
        'QTY
        '
        Me.QTY.DataPropertyName = "QTY"
        Me.QTY.HeaderText = "الكميه"
        Me.QTY.Name = "QTY"
        Me.QTY.ReadOnly = True
        '
        'Lot
        '
        Me.Lot.DataPropertyName = "batch"
        Me.Lot.HeaderText = "LOT"
        Me.Lot.Name = "Lot"
        Me.Lot.ReadOnly = True
        Me.Lot.Width = 150
        '
        'BarCode
        '
        Me.BarCode.DataPropertyName = "barcode"
        Me.BarCode.HeaderText = "باركود"
        Me.BarCode.Name = "BarCode"
        Me.BarCode.ReadOnly = True
        Me.BarCode.Width = 150
        '
        'ExpDate
        '
        Me.ExpDate.DataPropertyName = "ExpDate"
        Me.ExpDate.HeaderText = "ExpDate"
        Me.ExpDate.Name = "ExpDate"
        Me.ExpDate.ReadOnly = True
        Me.ExpDate.Width = 120
        '
        'NetQty
        '
        Me.NetQty.DataPropertyName = "NQty"
        Me.NetQty.HeaderText = "nqty"
        Me.NetQty.Name = "NetQty"
        Me.NetQty.ReadOnly = True
        '
        'NetUOMQty
        '
        Me.NetUOMQty.DataPropertyName = "NetUOMQty"
        Me.NetUOMQty.HeaderText = "NetUOMQty"
        Me.NetUOMQty.Name = "NetUOMQty"
        Me.NetUOMQty.ReadOnly = True
        Me.NetUOMQty.Visible = False
        '
        'Inventory_Details
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(915, 568)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CheckBox4)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.DG1)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Inventory_Details"
        Me.Text = "Inventory_Details"
        CType(Me.DG1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents DG1 As System.Windows.Forms.DataGridView
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox8 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBox7 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DeleteItemMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UOM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Lot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BarCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExpDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NetQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NetUOMQty As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
