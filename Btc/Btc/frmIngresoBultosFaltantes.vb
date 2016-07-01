Imports System.Data

Public Class frmIngresoBultosFaltantes

    Private Sub frmIngresoBultosFaltantes_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Cerrar()
        End Select
    End Sub

    Private Sub Cerrar()
        Me.Close()
    End Sub

    Private Sub frmIngresoBultosFaltantes_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GenerarQuery()
    End Sub

    Private Sub GenerarQuery()
        Dim Query As String, Data As New DataSet, strError As String = ""
        Try
            Query = "select * from view_mob_informe_faltantes"
            oBase.LimpiarParametros()
            If Not oBase.GetDataset(Data, Query, cDatabase.TipoComando.Text, strError) Then
                Throw New Exception(strError)
            Else
                Me.DataGrid1.DataSource = Nothing
                Me.DataGrid1.DataSource = Data.Tables(0)
                AutoSizeGrid(Me.DataGrid1, Titulo)
                Me.DataGrid1.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
        Finally
            Data = Nothing
        End Try
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Cerrar()
    End Sub

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

    Private Sub DataGrid1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGrid1.KeyDown
        Try
            DataGrid1.Select(DataGrid1.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DataGrid1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGrid1.KeyUp
        Try
            DataGrid1.Select(DataGrid1.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub
End Class