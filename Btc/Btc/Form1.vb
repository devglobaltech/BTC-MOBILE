Imports System.Threading

Public Class Form1

    Private Sub Add(ByVal ad As String, Optional ByVal TSleep As Integer = 1000)
        Me.Lst.Items.Add(ad)
        Application.DoEvents()
        Threading.Thread.Sleep(TSleep)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim Ad As String = "", vError As String = ""
        If Not oBase.EstadoConexion Then
            Me.Lst.Items.Clear()
            Add("------------------------------------------------------")
            Add("-Creacion de instancia")
            Add("-Inicializacion de objeto")
            Add("-Connection String")
            'oBase.ConnectionString = "Server=192.168.1.155;Database=test;Uid=test;Pwd=test;" 'LOCAL \\SEBAS
            oBase.ConnectionString = "Server=172.16.1.50;Database=test;Uid=fohet;Pwd=homero;" 'Servidor de BD S.C.
            'oBase.ConnectionString = "Server=7.141.6.99;Database=test;Uid=fohet;Pwd=homero;" 'Servidor de BD S.C.

            Add("-Conectando a la base de datos...")
            If Not oBase.IniciarConexion(vError) Then
                Add("-" & vError)
            Else
                Add("-Conexion OK...")
                Threading.Thread.Sleep(4000)
                Me.Lst.Items.Clear()
                Add("------------------------------------------------------")
                Add("1. Prueba Select", 50)
                Add("2. Ins Procedure", 45)
                Add("3. Ins Text", 40)
                Add("S. Salir", 35)
                Add("------------------------------------------------------")
                Me.Label1.Visible = True
                Me.txtAccion.Visible = True
                Me.txtAccion.Focus()
                Timer1.Enabled = False
            End If

        End If
    End Sub

    Private Sub txtAccion_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAccion.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtAccion.Text.Trim <> "" Then
                Select Case Me.txtAccion.Text.Trim.ToUpper
                    Case "1"
                        Accion1()
                    Case "2"
                        Accion2()
                    Case "3"
                        Accion3()
                    Case "S"
                        Application.Exit()
                    Case Else
                        Me.txtAccion.Text = ""
                End Select
                Me.txtAccion.Text = ""
            End If
        End If
    End Sub

    Private Sub Accion2()
        Dim SQL As String = "", vError As String = "", rz As String = ""
        Dim Pa As MySql.Data.MySqlClient.MySqlParameter
        Try
            If Not oBase.LimpiarParametros(vError) Then Throw New Exception(vError)
            rz = InputBox("ingrese razon social", "Ingreso Datos.")
            If rz.Trim = "" Then Exit Sub
            Pa = New MySql.Data.MySqlClient.MySqlParameter("p_razon_social", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100)
            Pa.Value = rz.Trim.ToUpper
            If Not oBase.AgregarParametro(Pa, vError) Then Throw New Exception(vError)

            If Not oBase.EjecutarSQL("ins_clientes", cDatabase.TipoComando.StoredProcedure, vError) Then Throw New Exception(vError)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical,Titulo)
        End Try
    End Sub

    Private Sub Accion3()
        Dim SQL As String = "", vError As String = "", rz As String = ""
        Dim Pa As MySql.Data.MySqlClient.MySqlParameter
        Try
            If Not oBase.LimpiarParametros(vError) Then Throw New Exception(vError)

            rz = InputBox("ingrese razon social", "Ingreso Datos.")
            If rz.Trim = "" Then Exit Sub
            SQL = "insert into clientes(razon_social)values('" & rz.ToUpper.Trim & "');"

            If Not oBase.EjecutarSQL(SQL, cDatabase.TipoComando.Text, vError) Then Throw New Exception(vError)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
        End Try
    End Sub

    Private Sub Accion1()
        Dim Ds As New System.Data.DataSet, SQL As String = "", _
            vError As String = "", Res As New FormDS

        Try
            SQL = "select * from clientes"
            If oBase.GetDataset(Ds, SQL, cDatabase.TipoComando.Text, vError) Then
                Res.Datos = Ds
                Res.ShowDialog()
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
        Finally
            Ds = Nothing
            Res = Nothing
        End Try
    End Sub

 
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
