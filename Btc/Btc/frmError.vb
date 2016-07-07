Public Class frmError

    Private MensajePrincipal As String = ""
    Private Acciones As String = "F1) Salir"

    Public Property Mensaje() As String
        Get
            Return MensajePrincipal
        End Get
        Set(ByVal value As String)
            MensajePrincipal = value
        End Set
    End Property

    Private Sub frmError_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Me.Timer.Enabled = False
                Me.Close()
        End Select
    End Sub

    Private Sub frmError_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = Titulo
        Me.lblMensaje.Text = Mensaje
        Me.lblAcciones.Text = Acciones
        Application.DoEvents()
        Me.Timer.Enabled = True
    End Sub

    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        SNOK.PlayNOK()
        CambiarColorPantalla(Color.Red)
    End Sub


    Private Function CambiarColorPantalla(ByVal Color As System.Drawing.Color) As Boolean
        Dim backco As System.Drawing.Color, tSleep As Integer = 200, lblColor As System.Drawing.Color
        Dim ctrl As Control
        Try
            backco = Me.BackColor
            lblColor = Me.lblAcciones.BackColor

            '1ra. intermitencia
            Me.BackColor = Color
            Me.lblAcciones.BackColor = Color
            Me.lblMensaje.BackColor = Color
            Application.DoEvents()
            Threading.Thread.Sleep(tSleep)

            '2da. intermitencia
            Me.BackColor = backco
            Me.lblAcciones.BackColor = lblColor
            Me.lblMensaje.BackColor = lblColor
            Application.DoEvents()
            Threading.Thread.Sleep(tSleep)

            '3ra. intermitencia
            Me.BackColor = Color
            Me.lblAcciones.BackColor = Color
            Me.lblMensaje.BackColor = Color
            Application.DoEvents()
            Threading.Thread.Sleep(tSleep)

            '4ta. intermitencia
            Me.BackColor = backco
            Me.lblAcciones.BackColor = lblColor
            Me.lblMensaje.BackColor = lblColor
            Application.DoEvents()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, Titulo)
        End Try
    End Function
End Class