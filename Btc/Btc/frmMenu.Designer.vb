<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmMenu
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
        Me.lstMenu = New System.Windows.Forms.ListBox
        Me.lblAccion = New System.Windows.Forms.Label
        Me.txtOpcion = New System.Windows.Forms.TextBox
        Me.lblTitulo = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lstMenu
        '
        Me.lstMenu.Items.Add("1) Recepcion de bultos")
        Me.lstMenu.Items.Add("2) Carga por carta de porte")
        Me.lstMenu.Items.Add("3) Salir")
        Me.lstMenu.Location = New System.Drawing.Point(0, 29)
        Me.lstMenu.Name = "lstMenu"
        Me.lstMenu.Size = New System.Drawing.Size(240, 226)
        Me.lstMenu.TabIndex = 0
        '
        'lblAccion
        '
        Me.lblAccion.BackColor = System.Drawing.Color.Transparent
        Me.lblAccion.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Italic)
        Me.lblAccion.Location = New System.Drawing.Point(0, 258)
        Me.lblAccion.Name = "lblAccion"
        Me.lblAccion.Size = New System.Drawing.Size(167, 20)
        Me.lblAccion.Text = "Indique una opcion:"
        Me.lblAccion.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtOpcion
        '
        Me.txtOpcion.Location = New System.Drawing.Point(173, 257)
        Me.txtOpcion.MaxLength = 1
        Me.txtOpcion.Name = "txtOpcion"
        Me.txtOpcion.Size = New System.Drawing.Size(67, 21)
        Me.txtOpcion.TabIndex = 2
        '
        'lblTitulo
        '
        Me.lblTitulo.BackColor = System.Drawing.Color.Silver
        Me.lblTitulo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.Location = New System.Drawing.Point(0, 4)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(240, 20)
        Me.lblTitulo.Text = "Label1"
        Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.txtOpcion)
        Me.Controls.Add(Me.lblAccion)
        Me.Controls.Add(Me.lstMenu)
        Me.Name = "frmMenu"
        Me.Text = "Menu Principal"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstMenu As System.Windows.Forms.ListBox
    Friend WithEvents lblAccion As System.Windows.Forms.Label
    Friend WithEvents txtOpcion As System.Windows.Forms.TextBox
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
End Class
