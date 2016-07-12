Imports System.Data
Imports MySql.Data.MySqlClient

Public Class cHDR

    Private oBase As cDatabase
    Private Snd As cPlaySound

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

    Public Function ValidarHDR(ByVal HDR As String) As Boolean
        Dim SQL As String = "", MDS As New DataSet, vError As String = ""
        Dim MSG_Error As String = "No existe la hoja de ruta ingresada."
        Try
            SQL = "Select Count(*) from view where hdr='" & HDR & "'"
            If Not oBase.GetDataset(MDS, SQL, cDatabase.TipoComando.Text, vError) Then
                Throw New Exception(vError)
            Else
                If MDS.Tables(0).Rows(0)(0) > 0 Then
                    Return True
                Else
                    Snd.PlayNOK()
                    MsgBox(MSG_Error, MsgBoxStyle.Exclamation, Titulo)
                    Return False
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
        Finally
            MDS = Nothing
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

    Public Function ValidarBultoHDR(ByVal HDR As String, ByVal Bulto As String) As Boolean
        Dim SQL As String = "", MDS As New DataSet, vError As String = ""
        Dim MSG_Error As String = "No existe la hoja de ruta ingresada."
        Try
            SQL = "Select Count(*) from view where hdr='" & HDR & "' and bulto='" & Bulto & "'"
            If Not oBase.GetDataset(MDS, SQL, cDatabase.TipoComando.Text, vError) Then
                Throw New Exception(vError)
            Else
                If MDS.Tables(0).Rows(0)(0) > 0 Then
                    Return True
                Else
                    Snd.PlayNOK()
                    MsgBox(MSG_Error, MsgBoxStyle.Exclamation, Titulo)
                    Return False
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
        Finally
            MDS = Nothing
        End Try
    End Function

    Public Function ConfirmarLecturaBulto(ByVal HDR As String, ByVal Bulto As String) As Boolean
        Try
            'Hoja de ruta.
            'Pedido.
            'Bulto.

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
        Finally

        End Try
    End Function

    Public Function CierreHojaDeRuta(ByVal HDR As String) As Boolean
        Try

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
        Finally

        End Try
    End Function

End Class
