<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmIngresoBultos
    Inherits System.Windows.Forms.Form

    'Form invalida a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar con el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnFaltantes = New System.Windows.Forms.Button
        Me.btnSalir = New System.Windows.Forms.Button
        Me.lblTitulo = New System.Windows.Forms.Label
        Me.txtBultos = New System.Windows.Forms.TextBox
        Me.lst = New System.Windows.Forms.ListBox
        Me.lblDOCK = New System.Windows.Forms.Label
        Me.Timer = New System.Windows.Forms.Timer
        Me.lblAT = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnFaltantes
        '
        Me.btnFaltantes.Location = New System.Drawing.Point(0, 250)
        Me.btnFaltantes.Name = "btnFaltantes"
        Me.btnFaltantes.Size = New System.Drawing.Size(240, 20)
        Me.btnFaltantes.TabIndex = 0
        Me.btnFaltantes.Text = "F1) Informar Faltantes"
        '
        'btnSalir
        '
        Me.btnSalir.Location = New System.Drawing.Point(0, 271)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(240, 20)
        Me.btnSalir.TabIndex = 1
        Me.btnSalir.Text = "F2) Cerrar"
        '
        'lblTitulo
        '
        Me.lblTitulo.BackColor = System.Drawing.Color.Black
        Me.lblTitulo.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.lblTitulo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblTitulo.Location = New System.Drawing.Point(0, 1)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(240, 22)
        Me.lblTitulo.Text = "Recepción de Bultos"
        Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtBultos
        '
        Me.txtBultos.Location = New System.Drawing.Point(0, 27)
        Me.txtBultos.Name = "txtBultos"
        Me.txtBultos.Size = New System.Drawing.Size(240, 21)
        Me.txtBultos.TabIndex = 3
        '
        'lst
        '
        Me.lst.Items.Add("")
        Me.lst.Items.Add("")
        Me.lst.Items.Add("Numero de remito: 0000-00001234")
        Me.lst.Items.Add("Cantidad de Bultos recepcionados:")
        Me.lst.Items.Add("3 de 16")
        Me.lst.Location = New System.Drawing.Point(0, 92)
        Me.lst.Name = "lst"
        Me.lst.Size = New System.Drawing.Size(240, 156)
        Me.lst.TabIndex = 4
        Me.lst.Visible = False
        '
        'lblDOCK
        '
        Me.lblDOCK.BackColor = System.Drawing.Color.Transparent
        Me.lblDOCK.Font = New System.Drawing.Font("Tahoma", 10.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.lblDOCK.Location = New System.Drawing.Point(0, 51)
        Me.lblDOCK.Name = "lblDOCK"
        Me.lblDOCK.Size = New System.Drawing.Size(240, 39)
        Me.lblDOCK.Text = "Dock Asignado: ZONA NORTE"
        '
        'Timer
        '
        Me.Timer.Enabled = True
        Me.Timer.Interval = 120000
        '
        'lblAT
        '
        Me.lblAT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblAT.ForeColor = System.Drawing.Color.Red
        Me.lblAT.Location = New System.Drawing.Point(230, 274)
        Me.lblAT.Name = "lblAT"
        Me.lblAT.Size = New System.Drawing.Size(10, 20)
        Me.lblAT.Text = "Guardando..."
        Me.lblAT.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblAT.Visible = False
        '
        'frmIngresoBultos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblAT)
        Me.Controls.Add(Me.lblDOCK)
        Me.Controls.Add(Me.lst)
        Me.Controls.Add(Me.txtBultos)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.btnSalir)
        Me.Controls.Add(Me.btnFaltantes)
        Me.KeyPreview = True
        Me.Name = "frmIngresoBultos"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnFaltantes As System.Windows.Forms.Button
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents txtBultos As System.Windows.Forms.TextBox
    Friend WithEvents lst As System.Windows.Forms.ListBox
    Friend WithEvents lblDOCK As System.Windows.Forms.Label
    Friend WithEvents Timer As System.Windows.Forms.Timer
    Friend WithEvents lblAT As System.Windows.Forms.Label
End Class
