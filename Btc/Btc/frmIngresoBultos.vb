Imports System.Data
Imports System.Threading

Public Class frmIngresoBultos

    Private cLN As cRecepcionBultos

    'Private Trd As Thread

    Public Sub New()
        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub

    Protected Overrides Sub Finalize()
        cLN = Nothing
        MyBase.Finalize()
    End Sub

    Private Sub frmIngresoBultos_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Faltantes()
            Case Keys.F2
                SalirApp()
        End Select
    End Sub

    Private Sub frmIngresoBultos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        cLN = New cRecepcionBultos
        cLN.CantidadLecturas = CantidadLecturas
        cLN.SonidoNOK = SNOK
        cLN.Database = oBase
        cLN.Titulo = Titulo
        Me.Timer.Interval = TiempoGuardado

        Me.Text = Titulo & "- Ing. Bultos."
        cLN.LlenarDataset()
        Me.lblDOCK.Text = ""
        Me.lst.Items.Clear()
        Me.txtBultos.Focus()
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        SalirApp()
    End Sub

    Private Sub SalirApp()
        Dim Msg As String = "¿Desea cerrar y salir de la aplicacion?"
        Dim msgSC As String = "¿Desea confirmar los registros antes de salir?"
        If cLN.RegistrosSinConfirmar > 0 Then
            cLN.ConfirmarRegistros()
        End If
        If MsgBox(Msg, MsgBoxStyle.YesNo, Titulo) = MsgBoxResult.Yes Then Application.Exit()
    End Sub

    Private Sub txtBultos_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBultos.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtBultos.Text.Trim <> "" Then
                procesarLectura()
            End If
        End If
    End Sub

    Private Sub procesarLectura()
        Dim MsgBultoError As String = "La etiqueta leida no es correcta!" & vbNewLine & "Por favor verique las etiquetas que esta tomando."
        Dim Lectura As String = "", Encontro As Boolean = False, strError As String = ""
        Try
            Lectura = Me.txtBultos.Text.Trim.ToUpper
            If Not cLN.ValidarLecturaBulto(Lectura) Then
                SNOK.PlayNOK()
                Me.txtBultos.Text = ""
                MsgBox(MsgBultoError, MsgBoxStyle.Exclamation, Titulo)
            Else
                'Aqui debo buscar si esta dentro del array.
                If cLN.InfoLectura(Me.txtBultos.Text.Trim.ToUpper, Me.lblDOCK, Me.lst, Encontro) Then
                    If Encontro Then
                        Me.txtBultos.Text = ""
                        Me.txtBultos.Focus()
                    End If
                Else
                    Me.txtBultos.Text = ""
                    Me.txtBultos.Focus()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
        End Try
    End Sub

    Private Sub btnFaltantes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFaltantes.Click
        Faltantes()
    End Sub

    Private Sub Faltantes()
        Dim f As New frmIngresoBultosFaltantes
        Try
            If cLN.RegistrosSinConfirmar > 0 Then
                cLN.ConfirmarRegistros()
            End If
            f.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
        Finally
            f = Nothing
        End Try
    End Sub

    Private Sub txtBultos_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBultos.TextChanged

    End Sub

    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        Me.lblAT.Visible = True
        cLN.ConfirmacionPorTiempo()
        Me.lblAT.Visible = False
    End Sub

End Class