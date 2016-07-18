Public Class frmHDR

    Private oHDR As cHDR
    Private CaracterFinal As String = "*"
    Private ColorTextBox As System.Drawing.Color

    Private Sub frmHDR_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.GotFocus
        If Me.txtHDR.Enabled Then Me.txtHDR.Focus()
        If Not Me.txtHDR.Enabled Then Me.txtBulto.Focus()
    End Sub

    Private Sub frmHDR_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Finalizar()
            Case Keys.F2
                ObtenerPendientes()
            Case Keys.F3
                Salir()
        End Select
    End Sub

    Private Sub frmHDR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ColorTextBox = Me.txtHDR.BackColor
        InicializarControles()
    End Sub

    Private Sub InicializarControles()
        Me.lstBULTO.Visible = False
        Me.lstBULTO.Items.Clear()
        Me.txtBulto.Visible = False
        Me.txtBulto.Text = ""
        Me.lblBulto.Visible = False
        Me.lstHDR.Visible = False
        Me.lstHDR.Items.Clear()
        Me.txtHDR.Focus()
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Salir()
    End Sub

    Private Sub Salir()
        Dim MSG_Salida As String = "¿Desea cancelar salir?, abandonara la operacion en curso."
        Try
            If Me.txtHDR.Text <> "" Then
                If MsgBox(MSG_Salida, MsgBoxStyle.YesNo, Titulo) = MsgBoxResult.Yes Then
                    Me.Close()
                Else
                    If Me.txtHDR.Enabled = True Then Me.txtHDR.Focus()
                    If Me.txtHDR.Enabled = True Then Me.txtBulto.Focus()
                End If
            Else
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub New()

        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        oHDR = New cHDR
        oHDR.Database = oBase
        oHDR.Sound = SNOK

    End Sub

    Private Sub txtHDR_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHDR.GotFocus
        Me.txtHDR.BackColor = Color.GreenYellow
    End Sub

    Private Sub txtHDR_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtHDR.KeyUp
        Dim MyPos As Integer = 0, Continuar As Boolean = False, Lectura As String = ""
        MyPos = InStr(Me.txtHDR.Text, CaracterFinal)
        If MyPos > 0 Then Continuar = True

        If (e.KeyValue = 13) Or (Continuar) Then
            If Me.txtHDR.Text.Trim <> "" Then
                Lectura = Me.txtHDR.Text.Trim
                Me.txtHDR.Text = ""
                ProcesarLecturaHDR(Lectura)
            End If
        End If
    End Sub

    Private Sub ProcesarLecturaHDR(ByVal Lectura As String)
        Dim LecturaHDR As String = "", MsgFinalizacion As String = "La carta de porte se completo. Presione F1 para finalizar."
        Try
            LecturaHDR = Replace(Lectura, CaracterFinal, "")
            If oHDR.ValidarHDR(LecturaHDR, Me.lstHDR) Then
                Me.txtHDR.Text = LecturaHDR
                Me.txtHDR.BackColor = Color.White
                Application.DoEvents()
                oHDR.GetCantLecturas(Me.lstBULTO, Me.txtHDR.Text)
                Me.lstBULTO.Visible = True
                Me.txtHDR.Enabled = False
                Me.lstHDR.Visible = True
                Me.lblBulto.Visible = True
                Me.txtBulto.Visible = True
                Me.txtBulto.Focus()
                If oHDR.VerificarCierreHDR(Me.txtHDR.Text) Then
                    SNOK.PlayOK()
                    MsgBox(MsgFinalizacion, MsgBoxStyle.OkOnly, Titulo)
                End If
            Else
                Me.txtHDR.Text = ""
                Me.txtHDR.Focus()
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
        End Try
    End Sub

    Private Sub txtBulto_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBulto.GotFocus
        Me.txtBulto.BackColor = Color.GreenYellow
    End Sub

    Private Sub txtBulto_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBulto.KeyUp
        Dim MyPos As Integer = 0, Continuar As Boolean = False, Lectura As String
        MyPos = InStr(Me.txtBulto.Text, CaracterFinal)
        If MyPos > 0 Then Continuar = True

        If (e.KeyValue = 13) Or (Continuar) Then
            Lectura = Me.txtBulto.Text
            Me.txtBulto.Text = ""
            If Lectura.Trim <> "" Then
                ProcesarLecturaBULTO(Lectura)
            End If
        End If
    End Sub

    Private Sub ProcesarLecturaBULTO(ByRef Lectura As String)
        Dim LecturaBULTO As String = "", MsgFinalizacion As String = "La carta de porte se completo. Presione F1 para finalizar."
        Try
            LecturaBULTO = Replace(Lectura, CaracterFinal, "")
            If Not oHDR.ValidarLecturaBultos(LecturaBULTO) Then
                Me.txtBulto.Text = ""
                Me.txtBulto.Focus()
                Exit Sub
            Else
                If oHDR.ConfirmarLecturaBulto(Me.txtHDR.Text, LecturaBULTO) Then
                    'Intermitencia
                    oHDR.GetCantLecturas(Me.lstBULTO, Me.txtHDR.Text)
                    Me.lstBULTO.Visible = True
                    Me.txtBulto.Text = ""
                    SNOK.PlayOK()
                    IntermitenciaPantalla(Color.GreenYellow)
                    Me.txtBulto.Focus()
                    Me.txtBulto.BackColor = Color.GreenYellow
                    If oHDR.VerificarCierreHDR(Me.txtHDR.Text) Then
                        SNOK.PlayOK()
                        MsgBox(MsgFinalizacion, MsgBoxStyle.OkOnly, Titulo)
                    End If
                Else
                    Me.txtBulto.Text = ""
                    Me.txtBulto.Focus()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
        End Try
    End Sub

    Private Function IntermitenciaPantalla(ByVal Color As System.Drawing.Color) As Boolean
        Dim backco As System.Drawing.Color, tSleep As Integer = 200, ColorTitulo As System.Drawing.Color
        Dim ctrl As Control
        Try
            backco = Me.BackColor
            ColorTitulo = Me.lblHDR.BackColor
            '1ra. intermitencia
            For Each ctrl In Me.Controls
                ctrl.BackColor = Color
            Next
            Me.BackColor = Color
            Application.DoEvents()
            Threading.Thread.Sleep(tSleep)

            '2da. intermitencia
            For Each ctrl In Me.Controls
                ctrl.BackColor = backco
            Next
            Me.lblHDR.BackColor = ColorTitulo
            Me.BackColor = backco
            Application.DoEvents()
            Threading.Thread.Sleep(tSleep)

            Application.DoEvents()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, Titulo)
        End Try
    End Function

    Private Sub txtBulto_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBulto.LostFocus
        Me.txtBulto.BackColor = ColorTextBox
    End Sub

    Private Sub txtHDR_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHDR.LostFocus
        Me.txtHDR.BackColor = ColorTextBox
    End Sub

    Private Sub btnPendientes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPendientes.Click
        ObtenerPendientes()
    End Sub

    Private Sub ObtenerPendientes()
        Dim F As New frmHDRBultosFaltantes
        Try
            If Me.txtHDR.Text <> "" Then
                F.HojaRuta = Me.txtHDR.Text
                F.HDR = oHDR
                F.ShowDialog()
                Me.txtBulto.Focus()
            Else
                Me.txtHDR.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, Titulo)
        Finally
            F = Nothing
        End Try
    End Sub

    Private Sub btnFinalizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinalizar.Click
        Finalizar()
    End Sub

    Private Sub Finalizar()
        Dim MSG_Finalizacion_NO As String = "Aun quedan pendientes subir bultos al transporte. No es posible finalizar la carta de porte."
        Dim MSG_Finalizacion_ERR As String = "Error al realizar el cierre de la carta de porte : "
        Dim F As New frmError, err As String = ""
        Try
            If Me.txtHDR.Text.Trim <> "" Then
                If Me.txtBulto.Visible Then
                    If Not oHDR.VerificarCierreHDR(Me.txtHDR.Text) Then
                        F.Mensaje = MSG_Finalizacion_NO
                        F.ShowDialog()
                        Me.txtBulto.Focus()
                    Else
                        If oHDR.CierreCartaPorte(Me.txtHDR.Text, err) Then
                            Me.InicializarControles()
                            Me.txtHDR.Focus()
                        Else
                            F.Mensaje = MSG_Finalizacion_ERR & err
                            F.ShowDialog()
                        End If
                    End If
                Else
                    Me.txtHDR.Text = ""
                    Me.txtHDR.Focus()
                End If
            Else
                Me.txtHDR.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, Titulo)
        End Try
    End Sub

    Private Sub lstHDR_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstHDR.GotFocus
        If Me.txtHDR.Enabled Then Me.txtHDR.Focus()
        If Not Me.txtHDR.Enabled Then Me.txtBulto.Focus()
    End Sub

    Private Sub lstBULTO_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstBULTO.GotFocus
        If Me.txtHDR.Enabled Then Me.txtHDR.Focus()
        If Not Me.txtHDR.Enabled Then Me.txtBulto.Focus()
    End Sub

    Private Sub txtHDR_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHDR.TextChanged

    End Sub

    Private Sub txtBulto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBulto.TextChanged

    End Sub
End Class