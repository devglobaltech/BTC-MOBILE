<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmHDR
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
        Me.lblHDR = New System.Windows.Forms.Label
        Me.txtHDR = New System.Windows.Forms.TextBox
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.txtBulto = New System.Windows.Forms.TextBox
        Me.lblBulto = New System.Windows.Forms.Label
        Me.lst = New System.Windows.Forms.ListBox
        Me.SuspendLayout()
        '
        'lblHDR
        '
        Me.lblHDR.Location = New System.Drawing.Point(4, 4)
        Me.lblHDR.Name = "lblHDR"
        Me.lblHDR.Size = New System.Drawing.Size(233, 20)
        Me.lblHDR.Text = "Ingrese Hoja de Ruta"
        '
        'txtHDR
        '
        Me.txtHDR.Location = New System.Drawing.Point(4, 26)
        Me.txtHDR.Name = "txtHDR"
        Me.txtHDR.Size = New System.Drawing.Size(233, 21)
        Me.txtHDR.TabIndex = 1
        '
        'btnCerrar
        '
        Me.btnCerrar.Location = New System.Drawing.Point(4, 271)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(233, 20)
        Me.btnCerrar.TabIndex = 2
        Me.btnCerrar.Text = "F2) Cerrar"
        '
        'txtBulto
        '
        Me.txtBulto.Location = New System.Drawing.Point(4, 175)
        Me.txtBulto.Name = "txtBulto"
        Me.txtBulto.Size = New System.Drawing.Size(233, 21)
        Me.txtBulto.TabIndex = 4
        '
        'lblBulto
        '
        Me.lblBulto.Location = New System.Drawing.Point(4, 153)
        Me.lblBulto.Name = "lblBulto"
        Me.lblBulto.Size = New System.Drawing.Size(234, 20)
        Me.lblBulto.Text = "Ingrese Bulto"
        '
        'lst
        '
        Me.lst.Location = New System.Drawing.Point(4, 50)
        Me.lst.Name = "lst"
        Me.lst.Size = New System.Drawing.Size(233, 100)
        Me.lst.TabIndex = 6
        '
        'frmHDR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.lst)
        Me.Controls.Add(Me.txtBulto)
        Me.Controls.Add(Me.lblBulto)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.txtHDR)
        Me.Controls.Add(Me.lblHDR)
        Me.Name = "frmHDR"
        Me.Text = "Cargar HDR"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblHDR As System.Windows.Forms.Label
    Friend WithEvents txtHDR As System.Windows.Forms.TextBox
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents txtBulto As System.Windows.Forms.TextBox
    Friend WithEvents lblBulto As System.Windows.Forms.Label
    Friend WithEvents lst As System.Windows.Forms.ListBox
End Class
