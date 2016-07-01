Public Class FormDS

    Private DS As New System.Data.DataSet

    Public Property Datos() As System.Data.DataSet
        Get
            Return DS
        End Get
        Set(ByVal value As System.Data.DataSet)
            DS = value
        End Set
    End Property

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub FormDS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.DataGrid1.DataSource = DS.Tables(0)
        AutoSizeGrid(Me.DataGrid1, "Consulta")
        Me.btnSalir.Focus()
    End Sub
End Class