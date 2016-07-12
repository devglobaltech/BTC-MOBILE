Public Class frmHDR

    Private vHDR As cHDR

    Private Sub frmHDR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub New()

        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        vHDR = New cHDR
        vHDR.Database = oBase
        vHDR.Sound = SNOK

    End Sub
End Class