Imports System.Data

Public Class frmHDRBultosFaltantes

    Private oHDR As cHDR
    Private vHojaRuta As String = ""

    Public Property HojaRuta() As String
        Get
            Return vHojaRuta
        End Get
        Set(ByVal value As String)
            vHojaRuta = value
        End Set
    End Property

    Public Property HDR() As cHDR
        Get
            Return oHDR
        End Get
        Set(ByVal value As cHDR)
            oHDR = value
        End Set
    End Property
    Private Sub DataGrid1_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGrid1.CurrentCellChanged
        Try
            DataGrid1.Select(DataGrid1.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DataGrid1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.GotFocus
        Try
            DataGrid1.Select(DataGrid1.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DataGrid1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DataGrid1.KeyPress
        Try
            DataGrid1.Select(DataGrid1.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmHDRBultosFaltantes_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Salir()
        End Select
    End Sub

    Private Sub frmHDRBultosFaltantes_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ObtenerQuery()
    End Sub

    Private Sub ObtenerQuery()
        Dim Query As String, Data As New DataSet, strError As String = ""
        Try
            oBase.LimpiarParametros()

            If Not oHDR.GetRestantesHDR(Data, HojaRuta) Then
                Throw New Exception(strError)
            Else
                Me.DataGrid1.DataSource = Nothing
                Me.DataGrid1.DataSource = Data.Tables(0)
                AutoSizeGrid(Me.DataGrid1, Titulo)
                Me.DataGrid1.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
            Me.Close()
        Finally
            Data = Nothing
        End Try
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Salir()
    End Sub

    Private Sub Salir()
        Me.Close()
    End Sub
End Class