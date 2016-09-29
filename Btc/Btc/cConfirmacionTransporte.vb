Imports System.Data
Imports MySql.Data.MySqlClient

Public Class cConfirmacionTransporte

    Private oBase As cDatabase
    Private Snd As cPlaySound

    Public Property BaseDeDatos() As cDatabase
        Get
            Return oBase
        End Get
        Set(ByVal value As cDatabase)
            oBase = value
        End Set
    End Property

    Public Property Sound() As cPlaySound
        Get
            Return Snd
        End Get
        Set(ByVal value As cPlaySound)
            Snd = value
        End Set
    End Property

    Public Function EstadoCartaPorte(ByVal HDR As String, ByRef PuedeConfirmar As Boolean) As Boolean
        Dim SQL As String = "", vError As String = "", Texto As String = "", Res As Long = 0
        Dim FE As New frmError, MSG_NO_PUEDE_CONFIRMAR As String = "La carta de porte tiene ##CANT_PEDIDO## pedido/s que no cumple/n con las condiciones para ser despachado."
        Dim MSG_NO_PUEDE_CONFIRMAR2 As String = "La carta de porte nro. ##CARTA_PORTE## no existe."
        Try
            '------------------------------------------------------------------------------------
            'Este segmento lo uso para validar que la carta de porte efectivamente exista.
            '------------------------------------------------------------------------------------
            SQL = "SELECT 	COUNT(*) RESULT FROM carta_porte cpo WHERE	cpo.idcarta_porte=" & HDR
            If Not oBase.EjecutarSQLEscalar(Res, SQL, cDatabase.TipoComando.Text, vError) Then
                Snd.PlayNOK()
                Throw New Exception(vError)
            Else
                If Res = 0 Then
                    FE.Mensaje = Replace(MSG_NO_PUEDE_CONFIRMAR2, "##CARTA_PORTE##", HDR)
                    FE.ShowDialog()
                    Return False
                End If
            End If
            SQL = ""
            Res = 0
            '------------------------------------------------------------------------------------
            'La carta existe, valido que no haya un solo pedido en estado incorrecto. Si hay, error...
            '------------------------------------------------------------------------------------
            SQL = "	        SELECT	count(p.idpedido)		 AS idpedido" & vbNewLine
            SQL = SQL & "   FROM 	pedido p JOIN empresa e 		    ON (p.idempresa = e.idempresa)" & vbNewLine
            SQL = SQL & "           INNER JOIN carta_porte_pedido cpp	ON (p.idpedido=cpp.idpedido)" & vbNewLine
            SQL = SQL & "           INNER JOIN carta_porte cpo		    ON (cpp.idcarta_porte=cpo.idcarta_porte)" & vbNewLine
            SQL = SQL & "   WHERE	CONCAT(p.idempresa,'-',p.idstatus) NOT IN(SELECT CONCAT(eo.idempresa,'-',eo.idstatus) FROM evento_operacion eo WHERE idoperacion='MOB003')" & vbNewLine
            SQL = SQL & "           AND cpo.idcarta_porte=" & HDR

            If Not oBase.EjecutarSQLEscalar(Res, SQL, cDatabase.TipoComando.Text, vError) Then
                Snd.PlayNOK()
                Throw New Exception(vError)
            Else
                If Res = 0 Then
                    PuedeConfirmar = True
                ElseIf Res > 0 Then
                    PuedeConfirmar = False
                    FE.Mensaje = Replace(MSG_NO_PUEDE_CONFIRMAR, "##CANT_PEDIDO##", Res)
                    FE.ShowDialog()
                End If
            End If
            Return True
        Catch ex As Exception
            FE.Mensaje = ex.Message
            FE.ShowDialog()
        Finally
            FE = Nothing
        End Try
    End Function

    Public Function ConfirmaCartaPorte(ByVal HDR As String) As Boolean
        Dim Param As MySqlParameter, mstrError As String = "", FE As New frmError
        Try
            If oBase.VerificarConexion() Then
                oBase.LimpiarParametros()

                Param = New MySqlParameter("p_idcarta_porte", MySqlDbType.Int16)
                Param.Value = HDR
                Param.Direction = ParameterDirection.Input
                oBase.AgregarParametro(Param)
                Param = Nothing

                Param = New MySqlParameter("p_idoperacion", MySqlDbType.VarChar, 100)
                Param.Value = "EPROCMOB003"
                Param.Direction = ParameterDirection.Input
                oBase.AgregarParametro(Param)
                Param = Nothing

                If Not oBase.EjecutarSQL("proc_mob_cambio_estado_por_cartaporte", cDatabase.TipoComando.StoredProcedure, mstrError) Then
                    Throw New Exception(mstrError)
                End If
                Return True
            End If
        Catch ex As Exception
            FE.Mensaje = ex.Message
            FE.ShowDialog()
        Finally
            FE = Nothing
            Param = Nothing
        End Try
    End Function

End Class
