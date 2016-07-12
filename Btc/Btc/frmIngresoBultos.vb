Imports System.Data
Imports System.Threading

Public Class frmIngresoBultos

    Private cLN As cRecepcionBultos
    Private Const CaracterFinal As String = "*"
    Private UltimaLectura As String = ""


    Public Sub New()
        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
    End Sub

    Protected Overrides Sub Finalize()
        cLN = Nothing
        MyBase.Finalize()
    End Sub

    Private Sub frmIngresoBultos_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.GotFocus
        Me.txtBultos.Focus()
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
        Dim Msg As String = "¿Desea cerrar?"
        Dim msgSC As String = "¿Desea confirmar los registros antes de salir?"
        If cLN.RegistrosSinConfirmar > 0 Then
            cLN.ConfirmarRegistros()
        End If
        If MsgBox(Msg, MsgBoxStyle.YesNo, Titulo) = MsgBoxResult.Yes Then
            Me.Close()
        Else
            Me.txtBultos.Focus()
        End If

    End Sub

    Private Sub txtBultos_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBultos.GotFocus
        Me.txtBultos.BackColor = Color.Yellow
    End Sub

    Private Sub txtBultos_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBultos.KeyUp

        Dim MyPos As Integer = 0, Continuar As Boolean = False
        MyPos = InStr(Me.txtBultos.Text, CaracterFinal)
        If MyPos > 0 Then Continuar = True

        If (e.KeyValue = 13) Or (Continuar) Then
            If Me.txtBultos.Text.Trim <> "" Then
                procesarLectura()
            End If
        End If
    End Sub

    Private Function CambiarColorPantalla(ByVal Color As System.Drawing.Color) As Boolean
        Dim backco As System.Drawing.Color, tSleep As Integer = 200, ColorTitulo As System.Drawing.Color
        Dim ctrl As Control
        Try
            backco = Me.BackColor
            ColorTitulo = Me.lblTitulo.BackColor
            '1ra. intermitencia
            For Each ctrl In Me.Controls
                ctrl.BackColor = Color
            Next
            Application.DoEvents()
            Threading.Thread.Sleep(tSleep)

            '2da. intermitencia
            For Each ctrl In Me.Controls
                ctrl.BackColor = backco
            Next
            Me.lblTitulo.BackColor = ColorTitulo
            Application.DoEvents()
            Threading.Thread.Sleep(tSleep)

            '3ra. intermitencia
            'For Each ctrl In Me.Controls
            '    ctrl.BackColor = Color
            'Next
            'Application.DoEvents()
            'Threading.Thread.Sleep(tSleep)

            ''4ta. intermitencia
            'For Each ctrl In Me.Controls
            '    ctrl.BackColor = backco
            'Next
            'Me.lblTitulo.BackColor = ColorTitulo
            Application.DoEvents()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, Titulo)
        End Try
    End Function

    Private Sub procesarLectura()
        Dim MsgBultoError As String = "La etiqueta leida no es correcta!" & vbNewLine & "Por favor verique las etiquetas que esta tomando."
        Dim Lectura As String = "", Encontro As Boolean = False, strError As String = "", fError As New frmError
        Try
            Lectura = Replace(Me.txtBultos.Text.Trim.ToUpper, CaracterFinal, "")
            'If Lectura = UltimaLectura Then
            '    Me.txtBultos.Text = ""
            '    Me.txtBultos.Focus()
            '    Exit Sub
            'Else
            '    UltimaLectura = Lectura
            'End If
            If Not cLN.ValidarLecturaBulto(Lectura) Then
                Me.txtBultos.Text = ""
                fError.Mensaje = MsgBultoError
                fError.ShowDialog()
                Exit Try
            Else
                'Aqui debo buscar si esta dentro del array.
                If cLN.InfoLectura(Lectura.ToUpper.Trim, Me.lblDOCK, Me.lst, Encontro) Then
                    If Encontro Then
                        Me.txtBultos.Text = ""
                        Application.DoEvents()
                        SNOK.PlayOK()
                        CambiarColorPantalla(Color.GreenYellow)
                        Me.txtBultos.Focus()
                    End If
                Else
                    Me.txtBultos.Text = ""
                    Me.txtBultos.Focus()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
        Finally
            fError = Nothing
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
            Me.txtBultos.Focus()
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

    Private Sub lst_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lst.GotFocus
        Me.txtBultos.Focus()
    End Sub

    Private Sub lst_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lst.SelectedIndexChanged

    End Sub
End Class