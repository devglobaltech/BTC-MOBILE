Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Threading

Public Class cRecepcionBultos
    '===================================================
    'Importantes: El booleano me permite que no se pise
    'el hilo que esta ejecutandose. Mientras que cant_
    'sirve para que se sincronize cada 50 registros.
    '===================================================
    Private ThrdEnProgreso As Boolean = False
    Private Cant_Registros_buffer As Long = 0

    '===================================================
    Private oBase As cDatabase
    Private Data As New DataSet
    Private DSLecturas As DataSet
    Private TituloApp As String = ""
    Dim Thrd As New Thread(AddressOf ConfirmarRegistros)
    Private cSndOK As cPlaySound
    Private cSndNOK As cPlaySound


    Public Sub New()
        DSLecturas = New DataSet
        CreateDSLecturas()
    End Sub

    Protected Overrides Sub Finalize()
        Data = Nothing
        DSLecturas = Nothing
        cSndOK = Nothing
        cSndNOK = Nothing
        MyBase.Finalize()
    End Sub

    Public Property CantidadLecturas() As Long
        Get
            Return Cant_Registros_buffer
        End Get
        Set(ByVal value As Long)
            Cant_Registros_buffer = value
        End Set
    End Property

    Public Property SonidoOK() As cPlaySound
        Get
            Return cSndOK
        End Get
        Set(ByVal value As cPlaySound)
            cSndOK = value
        End Set
    End Property

    Public Property SonidoNOK() As cPlaySound
        Get
            Return cSndNOK
        End Get
        Set(ByVal value As cPlaySound)
            cSndNOK = value
        End Set
    End Property

    Private Enum Tbl
        tbl_informacion = 1
    End Enum

    Private Enum View
        idpedido = 0
        nro_remito = 1
        nombre = 2
        direccion = 3
        idcodigo_postal = 4
        idlocalidad = 5
        descripcion = 6
        cantidad_bultos = 7
        codigo_empresa = 8
        idreferencia = 9
        fecha_estimada_entrega = 10
        dock_nom = 11
        zona = 12
        tipo_servicio = 13
        expreso = 14
    End Enum

    Public Property Titulo() As String
        Get
            Return TituloApp
        End Get
        Set(ByVal value As String)
            TituloApp = value
        End Set
    End Property

    Public Property Database() As cDatabase
        Get
            Return oBase
        End Get
        Set(ByVal value As cDatabase)
            oBase = value
        End Set
    End Property

    Public ReadOnly Property RegistrosSinConfirmar() As Int16
        Get
            Return DSLecturas.Tables(0).Rows.Count
        End Get
    End Property

    Public Function LlenarDataset() As Boolean
        Dim strError As String = "", Query As String = "select * from view_mob_confirmar_recepcion_bulto"
        Try
            If Data.Tables.Count > 0 Then
                Data = Nothing
                Data = New DataSet
            End If
            If Not oBase.GetDataset(Data, Query, cDatabase.TipoComando.Text, strError) Then
                Throw New Exception(strError)
            End If
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Titulo)
        End Try
    End Function

    Public Function ValidarLecturaBulto(ByVal Lectura As String) As Boolean
        Dim Search As String = "-", Pos As Integer = 0, Ret As Boolean = False
        Try
            Pos = InStr(Replace(Lectura, " ", ""), Search)
            If Pos <> 0 Then Ret = True
            Return Ret
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Titulo)
        End Try
    End Function

    Public Function InfoLectura(ByVal Lectura As String, ByRef lblDOCK As Label, ByRef Lst As ListBox, ByRef RegEncontrado As Boolean) As Boolean
        Dim MsgEtiErr As String = "La etiqueta escaneada es incorrecta.", fError As New frmError
        Dim SearchRows() As Data.DataRow, idpedido As String = "", bulto As String = ""
        Dim Search As String = "-", Pos As Integer = 0, Lec As String = Replace(Lectura, " ", "")
        Dim RegExistente() As Data.DataRow, vLectura As String = Lectura
        Try
            'PRO#12345678-1* ejemplo.

            Pos = InStr(Lec, Search)
            idpedido = Mid(Lec, 1, Pos - 1)
            bulto = Mid(Lec, Pos + 1, Len(Lec))

            If bulto.Trim = "" Then
                'cSndNOK.PlayNOK()
                Throw New Exception(MsgEtiErr)
            End If

            If Not IsNumeric(bulto) Then
                'cSndNOK.PlayNOK()
                Throw New Exception(MsgEtiErr)
            End If

            'If Not IsNumeric(idpedido) Then
            'cSndNOK.PlayNOK()
            'Throw New Exception(MsgEtiErr)
            'End If

            If Not IsNumeric(idpedido) Then
                SearchRows = Data.Tables(0).Select("idreferencia = '" & idpedido & "'")
                If SearchRows.Length > 0 Then
                    idpedido = SearchRows(0).Item(View.idpedido).ToString
                    vLectura = idpedido & "-" & bulto
                Else
                    Me.LlenarDataset()
                    SearchRows = Data.Tables(0).Select("idreferencia = '" & idpedido & "'")
                    If SearchRows.Length > 0 Then
                        idpedido = SearchRows(0).Item(View.idpedido).ToString
                        vLectura = idpedido & "-" & bulto
                    End If
                End If
            End If

            'Por si paso mucho tiempo desde la ultima descarga del pedido y necesito recuperar los datos otra vez.
            If IsNumeric(idpedido) Then
                SearchRows = Data.Tables(0).Select("idpedido = '" & idpedido & "'")
                If SearchRows.Length = 0 Then
                    Me.LlenarDataset()
                    SearchRows = Data.Tables(0).Select("idpedido = '" & idpedido & "'")
                End If
            End If

            If SearchRows.Length > 0 Then
                If bulto <= SearchRows(0).Item(View.cantidad_bultos) Then
                    Lst.Items.Clear()
                    Lst.Visible = True
                    RegEncontrado = True
                    lblDOCK.Text = "Dock Asignado: " & SearchRows(0).Item(View.dock_nom)
                    Lst.Items.Add("Empresa: " & SearchRows(0).Item(View.codigo_empresa))
                    Lst.Items.Add("Zona: " & SearchRows(0).Item(View.zona))
                    Lst.Items.Add("Nombre: " & SearchRows(0).Item(View.nombre))
                    Lst.Items.Add("Numero de remito: " & SearchRows(0).Item(View.nro_remito)).ToString()
                    Lst.Items.Add("Localidad: " & SearchRows(0).Item(View.idlocalidad))
                    Lst.Items.Add("Referencia: " & SearchRows(0).Item(View.idreferencia))
                    Lst.Items.Add("Expreso: " & SearchRows(0).Item(View.expreso))
                    Lst.Items.Add("")
                    Lst.Items.Add("Cant.Total Bultos: " & SearchRows(0).Item(View.cantidad_bultos))
                    RegExistente = DSLecturas.Tables(0).Select("nro_bulto = '" & Lectura & "'")

                    If RegExistente.Length = 0 Then
                        RegistrosLeidos(vLectura, SearchRows(0).Item(View.idpedido).ToString, SearchRows(0).Item(View.nro_remito).ToString, SearchRows(0).Item(View.cantidad_bultos).ToString)
                    End If

                    SearchRows = DSLecturas.Tables(0).Select("idpedido = '" & idpedido & "'")
                    Lst.Items.Add("Bultos leidos por Ud. sin guardar.: " & SearchRows.Length)
                    Application.DoEvents()
                    If (DSLecturas.Tables(0).Rows.Count >= Cant_Registros_buffer) Then
                        'Shoot the Thread. \m/
                        If Not Me.ThrdEnProgreso Then
                            Thrd.Start()
                        End If

                    End If
                Else
                    'cSndNOK.PlayNOK()
                    Throw New Exception("El numero de bulto tomado es incorrecto.")
                End If
            Else
                'cSndNOK.PlayNOK()
                Throw New Exception("No existe el numero de pedido /referencia " & idpedido)
            End If
            Return True
        Catch ex As Exception
            fError.Mensaje = ex.Message
            fError.ShowDialog()
        Finally
            fError = Nothing
        End Try
    End Function

    Private Function RegistrosLeidos(ByVal nro_bulto As String, ByVal idPedido As String, ByVal nro_remito As String, ByVal cant_bultos As String) As Boolean
        Dim newLectura As DataRow
        Try
            newLectura = DSLecturas.Tables(0).NewRow
            newLectura("idpedido") = idPedido
            newLectura("nro_remito") = nro_remito
            newLectura("nro_bulto") = nro_bulto
            newLectura("cant_bultos") = cant_bultos
            DSLecturas.Tables(0).Rows.Add(newLectura)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Titulo)
        End Try
    End Function

    Private Function CreateDSLecturas() As Boolean
        Dim tblLecturas As New DataTable("LECTURAS")
        Try
            tblLecturas.Columns.Add("idpedido")
            tblLecturas.Columns.Add("nro_remito")
            tblLecturas.Columns.Add("nro_bulto")
            tblLecturas.Columns.Add("cant_bultos")
            DSLecturas.Tables.Add(tblLecturas)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Titulo)
        Finally
            tblLecturas = Nothing
        End Try
    End Function

    Public Function ConfirmarRegistros() As Boolean
        Dim i As Integer = 0, IdPedido As String = "", nro_remito As String = "", nro_bulto As String = ""
        Dim cant_bultos As String = "", Dt As New DataTable

        Try
            If ThrdEnProgreso Then
                Return True
            End If

            ThrdEnProgreso = True
            For i = 0 To DSLecturas.Tables(0).Rows.Count - 1

                IdPedido = DSLecturas.Tables(0).Rows(i)(0)
                nro_remito = DSLecturas.Tables(0).Rows(i)(1)
                nro_bulto = DSLecturas.Tables(0).Rows(i)(2)
                cant_bultos = DSLecturas.Tables(0).Rows(i)(3)
                If InsertLectura(IdPedido, nro_remito, nro_bulto, cant_bultos) Then
                    Try
                        DSLecturas.Tables(0).Rows.RemoveAt(i)
                        If DSLecturas.Tables(0).Rows.Count > 0 Then
                            i = i - 1
                        End If
                        If DSLecturas.Tables(0).Rows.Count = 0 Then
                            Exit For
                        End If
                    Catch ex As Exception
                    End Try
                End If
            Next i
            Return True
        Catch tex As Threading.ThreadAbortException
            ThrdEnProgreso = False
        Catch ex As Exception
            ThrdEnProgreso = False
            'MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Titulo)
        Finally
            Thrd = Nothing
            Thrd = New Thread(AddressOf ConfirmarRegistros)
            ThrdEnProgreso = False
        End Try
    End Function

    Private Function InsertLectura(ByVal idpedido As String, ByVal nro_remito As String, ByVal nro_bulto As String, ByVal cant_bultos As String) As Boolean
        Dim Param As MySqlParameter, strError As String = ""
        Try
            If oBase.VerificarConexion() Then
                oBase.LimpiarParametros()

                Param = New MySqlParameter("p_idpedido", MySqlDbType.Int16)
                Param.Value = idpedido
                Param.Direction = ParameterDirection.Input
                oBase.AgregarParametro(Param)
                Param = Nothing

                Param = New MySqlParameter("p_nro_remito", MySqlDbType.VarChar, 100)
                Param.Value = nro_remito
                Param.Direction = ParameterDirection.Input
                oBase.AgregarParametro(Param)
                Param = Nothing

                Param = New MySqlParameter("p_nro_bulto", MySqlDbType.VarChar, 100)
                Param.Value = nro_bulto
                Param.Direction = ParameterDirection.Input
                oBase.AgregarParametro(Param)
                Param = Nothing

                Param = New MySqlParameter("p_cant_bultos", MySqlDbType.Int16)
                Param.Value = cant_bultos
                Param.Direction = ParameterDirection.Input
                oBase.AgregarParametro(Param)
                Param = Nothing

                If Not oBase.EjecutarSQL("proc_ins_bulto_leido", cDatabase.TipoComando.StoredProcedure, strError) Then
                    Throw New Exception(strError)
                End If
                Return True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Titulo)
        End Try
    End Function

    Public Function ConfirmacionPorTiempo() As Boolean
        Try
            If Not Me.ThrdEnProgreso Then
                If DSLecturas.Tables(0).Rows.Count > 0 Then
                    Thrd.Start()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Titulo)
        Finally
            Thrd = Nothing
            Thrd = New Thread(AddressOf ConfirmarRegistros)
            ThrdEnProgreso = False
        End Try
    End Function

End Class