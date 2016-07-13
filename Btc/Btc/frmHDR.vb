Public Class frmHDR

    Private oHDR As cHDR
    Private CaracterFinal As String = "*"
    Private ColorTextBox As System.Drawing.Color

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
        Try
            Me.Close()
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
        Dim MyPos As Integer = 0, Continuar As Boolean = False
        MyPos = InStr(Me.txtHDR.Text, CaracterFinal)
        If MyPos > 0 Then Continuar = True

        If (e.KeyValue = 13) Or (Continuar) Then
            If Me.txtHDR.Text.Trim <> "" Then
                ProcesarLecturaHDR()
            End If
        End If
    End Sub

    Private Sub ProcesarLecturaHDR()
        Dim LecturaHDR As String = ""
        Try
            LecturaHDR = Replace(Me.txtHDR.Text, CaracterFinal, "")
            If oHDR.ValidarHDR(LecturaHDR, Me.lstHDR) Then
                Me.txtHDR.BackColor = Color.White
                Application.DoEvents()
                Me.txtHDR.Text = LecturaHDR
                Me.txtHDR.Enabled = False
                Me.lstHDR.Visible = True
                Me.lblBulto.Visible = True
                Me.txtBulto.Visible = True
                Me.txtBulto.Focus()
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
        Dim LecturaBULTO As String = ""
        Try
            LecturaBULTO = Replace(Lectura, CaracterFinal, "")
            If Not oHDR.ValidarLecturaBultos(LecturaBULTO) Then
                Me.txtBulto.Text = ""
                Me.txtBulto.Focus()
                Exit Sub
            Else
                If oHDR.ConfirmarLecturaBulto(Me.txtHDR.Text, LecturaBULTO) Then
                    'Intermitencia
                    IntermitenciaPantalla(Color.GreenYellow)
                    Me.txtBulto.Text = ""
                    Me.txtBulto.Focus()
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

            Application.DoEvents()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, Titulo)
        End Try
    End Function

    Private Sub txtBulto_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBulto.LostFocus
        Me.txtBulto.BackColor = ColorTextBox
    End Sub

    Private Sub txtBulto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBulto.TextChanged

    End Sub

    Private Sub txtHDR_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHDR.LostFocus
        Me.txtHDR.BackColor = ColorTextBox
    End Sub

    Private Sub txtHDR_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHDR.TextChanged

    End Sub
End Class