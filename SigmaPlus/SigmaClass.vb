Imports System.Data.SqlClient


Public Module SigmaClass
    Public db
    Public sp, sp1, sp2 As ADODB.Recordset
    Public arg, ErrDisc As String
    Public ds, ds1 As New DataSet
    Public dt, dt1 As New DataTable
    Public adp, adp1 As New SqlDataAdapter
    Public conn As SqlConnection
    Public user_id As String
    Public state, indi, whno As String

    Public Sub center(ByVal form1 As Form)
        REM ----------------------------- By George to center the form
        Dim x = (Screen.PrimaryScreen.Bounds.Width - form1.Width) / 2
        Dim y = (Screen.PrimaryScreen.Bounds.Height - form1.Height) / 2
        form1.Location = New Point(x, y)
        REM ----------------------------- By George to check form Security
    End Sub

    Public Sub select_one(ByVal title1 As String, q1 As String)
        REM ------------------------------------
        select_one_form.Text = title1
        arg = q1
        select_one_form.ShowDialog()
    End Sub
   Public Function Validat_Code(ByVal value As String)
        ErrDisc = ""
        If Left(value, 1) = "0" Then
            ErrDisc = "لا يمكن البدايه بصفر" : Validat_Code = False : Exit Function
        End If
        If InStr(value, " ") Then
            ErrDisc = "برجاء حذف المسافه من الكود" : Validat_Code = False : Exit Function
        End If
        Validat_Code = True
    End Function
    Public Function next_serial(co, file1, indi, whno)
        REM ===================================================
        REM =========== Find the Next Serial for the Doc.
        REM ===================================================

        arg = "select isnull(max(" & co & "),0) as m1 from " & file1
        If indi <> "" Then
            arg = arg & " where indi = '" & indi & "'"
        End If
        If whno <> "" Then
            arg = arg & " and whno = '" & whno & "'"
        End If
        Dim cmd As SqlCommand = New SqlCommand(arg, conn)
        Dim reader1 As SqlDataReader = cmd.ExecuteReader()
        next_serial = 1
        While reader1.Read
            next_serial = reader1(0).ToString + 1
        End While
        reader1.Close()
        cmd = Nothing

    End Function
    Public Function find_setting(label)
        Static arg
        arg = "select value from setup where  name = '" & label & "'"
        Dim cmd1 As SqlCommand = New SqlCommand(arg, conn)
        Dim reader1 As SqlDataReader = cmd1.ExecuteReader()
        If reader1.HasRows = True Then
            reader1.Read()
            find_setting = reader1("Value")
        Else
            MsgBox("Please Check The Software Setting - " & label)
            find_setting = ""
        End If
        reader1.Close()
    End Function

    Public Sub doc_log(serial, document, action, reason)
        REM =================== logging all users activities
        arg = "insert into doc_log(serial, doc, action, user_id, trx_date,Remarks) values ('" & serial & "', '" & document
        arg = arg & "','" & action & "','" & user_id & "',getdate(),'" & reason & "','Win')"
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim cmd2 As New System.Data.SqlClient.SqlCommand(arg, conn)
        cmd2.ExecuteNonQuery()
    End Sub


    Public Sub select_Combo(dropdown As ComboBox, q1 As String)
        REM ------------------------------------
        'select_one_form.Text = title1
        'arg = q1
        'select_one_form.ShowDialog()

        REM ========================= 
        adp = New SqlDataAdapter(q1, conn)
        dt = New DataTable
        ds = New DataSet()
        adp.Fill(ds)
        dropdown.DataSource = ds.Tables(0)
        dropdown.DisplayMember = "name"
    End Sub

End Module

