Public Class frmConfirmacionTransporte

    Private CaracterFinal As String = "*"
    Private oConfirmacion As New cConfirmacionTransporte

    Private Sub txtHDR_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHDR.GotFocus
        Me.txtHDR.BackColor = Color.GreenYellow
    End Sub

    Private Sub txtHDR_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtHDR.KeyUp
        Dim MyPos As Integer = 0, Continuar As Boolean = False, Lectura As String = ""
        Me.lblUltimaConfirmacion.Text = ""
        MyPos = InStr(Me.txtHDR.Text, CaracterFinal)
        If MyPos > 0 Then Continuar = True

        If (e.KeyValue = 13) Or (Continuar) Then
            If Me.txtHDR.Text.Trim <> "" Then
                Lectura = Me.txtHDR.Text.Trim
                Me.txtHDR.Text = ""
                Me.ProcesarLecturaHDR(Lectura)
            End If
        End If
    End Sub

    Private Sub ProcesarLecturaHDR(ByVal Lectura As String)
        Dim LecturaHDR As String = "", Continua As Boolean = False
        Try
            LecturaHDR = Replace(Lectura, CaracterFinal, "")
            If oConfirmacion.EstadoCartaPorte(LecturaHDR, Continua) Then
                If Continua Then
                    If oConfirmacion.ConfirmaCartaPorte(LecturaHDR) Then
                        SNOK.PlayOK()
                        IntermitenciaPantalla(Color.GreenYellow)
                        lblUltimaConfirmacion.Text = "Se confirmo la carta de porte " & LecturaHDR
                        Me.txtHDR.Text = ""
                        Me.txtHDR.BackColor = Color.GreenYellow
                        Me.txtHDR.Focus()
                    End If
                Else
                    Me.txtHDR.Text = ""
                    Me.txtHDR.Focus()
                End If
            Else
                Me.txtHDR.Text = ""
                Me.txtHDR.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
        End Try
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub frmConfirmacionTransporte_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F3
                Salir()
        End Select
    End Sub

    Private Sub frmConfirmacionTransporte_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = Titulo
        Me.oConfirmacion.BaseDeDatos = oBase
        Me.oConfirmacion.Sound = SNOK
        Me.txtHDR.Focus()
    End Sub

    Private Sub txtHDR_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHDR.TextChanged

    End Sub

    Private Sub btnCerrar_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Salir()
    End Sub

    Private Sub Salir()
        Dim MSG_SALIR As String = "¿Desea salir?"
        If MsgBox(MSG_SALIR, MsgBoxStyle.YesNo, Titulo) = MsgBoxResult.Yes Then
            Me.Close()
        Else
            Me.txtHDR.Focus()
        End If
    End Sub

    Private Function IntermitenciaPantalla(ByVal Color As System.Drawing.Color) As Boolean
        Dim backco As System.Drawing.Color, tSleep As Integer = 200, ColorTitulo As System.Drawing.Color
        Dim ctrl As Control
        Try
            backco = Me.BackColor
            ColorTitulo = Me.lblCartaPorte.BackColor

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
            Me.lblCartaPorte.BackColor = ColorTitulo
            Me.BackColor = backco
            Application.DoEvents()
            Threading.Thread.Sleep(tSleep)

            Application.DoEvents()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, Titulo)
        End Try
    End Function
End Class