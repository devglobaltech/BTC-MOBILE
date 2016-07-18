Imports System.Data
Imports MySql.Data.MySqlClient

Public Class cHDR

    Private oBase As cDatabase
    Private Snd As cPlaySound
    Private MDS As DataSet
    Private oBultos As New cRecepcionBultos
    Private TotalBultos As Long = 0

    Private Enum View
        Remitos = 0
        Bultos = 1
        Patente = 2
        Hdr = 3
        idPedido = 4
    End Enum

    Public Property Sound() As cPlaySound
        Get
            Return Snd
        End Get
        Set(ByVal value As cPlaySound)
            Snd = value
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

    Public Function ValidarHDR(ByVal HDR As String, ByRef LstCPO As ListBox) As Boolean
        Dim SQL As String = "", vError As String = "", fError As New frmError
        Dim MSG_Error As String = "No existe la hoja de ruta ingresada."
        Try
            LstCPO.Items.Clear()
            MDS = Nothing
            MDS = New DataSet
            SQL = "Select * from view_mob_carga_hdr where hdr='" & HDR & "'"
            If Not oBase.GetDataset(MDS, SQL, cDatabase.TipoComando.Text, vError) Then
                Throw New Exception(vError)
            Else
                If MDS.Tables.Count > 0 Then
                    If MDS.Tables(0).Rows.Count > 0 Then
                        LstCPO.Items.Add("Inf. Transporte: " & MDS.Tables(0).Rows(0)(View.Patente).ToString)
                        LstCPO.Items.Add("Cantidad total Remitos: " & MDS.Tables(0).Rows.Count)
                        LstCPO.Items.Add("Cantidad total Bultos: " & GetCantidadBultos(MDS))
                        Return True
                    Else
                        fError.Mensaje = MSG_Error
                        fError.ShowDialog()
                        Return False
                    End If
                    fError.Mensaje = MSG_Error
                    fError.ShowDialog()
                    Return False
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
        Finally
            fError = Nothing
        End Try
    End Function

    Private Function GetCantidadBultos(ByVal DS As DataSet) As Long
        Dim i As Long = 0, Acu As Long = 0
        Try
            For i = 0 To DS.Tables(0).Rows.Count - 1
                Acu = Acu + IIf(String.IsNullOrEmpty(DS.Tables(0).Rows(i)(View.Bultos)), 0, CLng(DS.Tables(0).Rows(i)(View.Bultos)))
            Next
            TotalBultos = Acu
            Return Acu
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
        End Try
    End Function

    Public Function MostrarInformacionHDR(ByRef Lst As ListBox, ByVal HDR As String) As Boolean
        Dim DS As New DataSet, SQL As String = "", vError As String = ""
        Try
            Lst.Items.Clear()
            SQL = ""
            If Not oBase.GetDataset(DS, SQL, cDatabase.TipoComando.Text, vError) Then
                Snd.PlayNOK()
                Throw New Exception(vError)
            Else
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        Lst.Items.Add(DS.Tables(0).Rows(0)(0))
                        Lst.Items.Add(DS.Tables(0).Rows(0)(1))
                        Lst.Items.Add(DS.Tables(0).Rows(0)(2))
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
        Finally
            DS = Nothing
        End Try
    End Function

    Public Function ConfirmarLecturaBulto(ByVal HDR As String, ByVal Lectura As String) As Boolean
        Dim MsgEtiErr As String = "La etiqueta escaneada es incorrecta.", fError As New frmError
        Dim SearchRows() As Data.DataRow, idpedido As String = "", bulto As String = ""
        Dim Search As String = "-", Pos As Integer = 0, Lec As String = Replace(Lectura, " ", "")
        Dim RegExistente() As Data.DataRow
        Try
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

            If Not IsNumeric(idpedido) Then
                'cSndNOK.PlayNOK()
                Throw New Exception(MsgEtiErr)
            End If

            SearchRows = MDS.Tables(0).Select("idpedido = '" & idpedido & "'")
            If SearchRows.Length > 0 Then
                If bulto <= SearchRows(0).Item(View.Bultos) Then
                    If GuardarLectura(HDR, idpedido, Lectura) Then
                        Return True
                    End If
                Else
                    Throw New Exception("El numero de bulto tomado es incorrecto.")
                End If
            Else
                Throw New Exception("No existe el numero de pedido " & idpedido)
            End If
        Catch ex As Exception
            fError.Mensaje = ex.Message
            fError.ShowDialog()
        Finally
            fError = Nothing
        End Try
    End Function

    Private Function GuardarLectura(ByVal HojaRuta As String, ByVal IdPedido As String, ByVal Lectura As String) As Boolean
        '-----------------------------------------------------------------
        'PROCEDURE proc_ins_carta_porte_bulto(	p_idcarta_porte	INT(11),
        '					                    p_idpedido	INT(11),
        '                                       p_nro_bulto(VARCHAR(100))
        '					                    )
        '-----------------------------------------------------------------
        Dim Pa As MySqlParameter, strError As String = ""
        Try
            oBase.LimpiarParametros()

            Pa = New MySqlParameter("p_idcarta_porte", MySqlDbType.Int16, 11)
            Pa.Value = HojaRuta
            oBase.AgregarParametro(Pa)
            Pa = Nothing

            Pa = New MySqlParameter("p_idpedido", MySqlDbType.Int16, 11)
            Pa.Value = IdPedido
            oBase.AgregarParametro(Pa)
            Pa = Nothing

            Pa = New MySqlParameter("p_nro_bulto", MySqlDbType.VarChar, 100)
            Pa.Value = Lectura
            oBase.AgregarParametro(Pa)
            Pa = Nothing

            If Not oBase.EjecutarSQL("proc_ins_carta_porte_bulto", cDatabase.TipoComando.StoredProcedure, strError) Then
                Throw New Exception(strError)
            Else
                Return True
            End If
        Catch MSQLex As MySqlException
            MsgBox(MSQLex.Message, MsgBoxStyle.Critical, Titulo)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
        End Try
    End Function

    Public Function ValidarLecturaBultos(ByVal Lectura As String) As Boolean
        Dim MsgBultoError As String = "La etiqueta leida no es correcta!" & vbNewLine & "Por favor verique las etiquetas que esta tomando."
        Dim fError As New frmError
        Try
            If oBultos.ValidarLecturaBulto(Lectura) Then
                Return True
            Else
                fError.Mensaje = MsgBultoError
                fError.ShowDialog()
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
        Finally
            fError = Nothing
        End Try
    End Function

    Public Function GetCantLecturas(ByRef LstLecturas As ListBox, ByVal HDR As String) As Boolean
        Dim DS As New DataSet, SQL As String = "", vError As String = "", Texto As String = ""
        Try
            LstLecturas.Items.Clear()
            SQL = "SELECT COUNT(nro_bulto)AS RES FROM mob_carta_porte_bultos_leidos WHERE idcarta_porte=" & HDR
            If Not oBase.GetDataset(DS, SQL, cDatabase.TipoComando.Text, vError) Then
                Snd.PlayNOK()
                Throw New Exception(vError)
            Else
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        Texto = "SUBIDOS AL TRANSPORTE " & DS.Tables(0).Rows(0)(0) & " DE " & TotalBultos
                        LstLecturas.Items.Add(Texto)
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
        Finally
            DS = Nothing
        End Try
    End Function

    Public Function GetRestantesHDR(ByRef DS As DataSet, ByVal Hdr As String) As Boolean
        Dim SQL As String = "", vError As String = ""
        Try

            SQL = "SELECT  nro_remito, cantidad_bultos as Cant_Bultos, restan AS leidos, idpedido as Pedido FROM view_mob_restantes_hdr where hdr=" & Hdr
            If Not oBase.GetDataset(DS, SQL, cDatabase.TipoComando.Text, vError) Then
                Snd.PlayNOK()
                Throw New Exception(vError)
            Else
                Return True
            End If
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
        End Try
    End Function

    Public Function VerificarCierreHDR(ByVal HDR As String) As Boolean
        Dim DS As New DataSet, SQL As String = "SELECT  COUNT(*) FROM view_mob_restantes_hdr WHERE hdr=" & HDR
        Dim StrError As String = ""
        Try
            oBase.LimpiarParametros()
            If Not oBase.GetDataset(DS, SQL, cDatabase.TipoComando.Text, StrError) Then
                Throw New Exception(StrError)
            Else
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        If DS.Tables(0).Rows(0)(0) = 0 Then
                            Return True
                        End If
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
        Finally
            DS = Nothing
        End Try
    End Function

    Public Function CierreCartaPorte(ByVal HDR As String, Optional ByRef strError As String = "") As Boolean
        Dim Param As MySqlParameter, mstrError As String = ""
        Try
            If oBase.VerificarConexion() Then
                oBase.LimpiarParametros()

                Param = New MySqlParameter("p_idcarta_porte", MySqlDbType.Int16)
                Param.Value = HDR
                Param.Direction = ParameterDirection.Input
                oBase.AgregarParametro(Param)
                Param = Nothing

                If Not oBase.EjecutarSQL("proc_mob_confirmar_expedicion_carta_porte", cDatabase.TipoComando.StoredProcedure, mstrError) Then
                    Throw New Exception(mstrError)
                End If
                Return True
            End If
        Catch ex As Exception
            strError = ex.Message
        Finally
            Param = Nothing
        End Try
    End Function

End Class
