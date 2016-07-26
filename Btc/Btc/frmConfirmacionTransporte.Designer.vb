<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmConfirmacionTransporte
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
        Me.lblCartaPorte = New System.Windows.Forms.Label
        Me.txtHDR = New System.Windows.Forms.TextBox
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.lblUltimaConfirmacion = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblCartaPorte
        '
        Me.lblCartaPorte.Location = New System.Drawing.Point(4, 18)
        Me.lblCartaPorte.Name = "lblCartaPorte"
        Me.lblCartaPorte.Size = New System.Drawing.Size(233, 20)
        Me.lblCartaPorte.Text = "Carta de porte"
        '
        'txtHDR
        '
        Me.txtHDR.Location = New System.Drawing.Point(4, 41)
        Me.txtHDR.Name = "txtHDR"
        Me.txtHDR.Size = New System.Drawing.Size(233, 21)
        Me.txtHDR.TabIndex = 1
        '
        'btnCerrar
        '
        Me.btnCerrar.Location = New System.Drawing.Point(4, 271)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(233, 20)
        Me.btnCerrar.TabIndex = 3
        Me.btnCerrar.Text = "F3) Cerrar"
        '
        'lblUltimaConfirmacion
        '
        Me.lblUltimaConfirmacion.Location = New System.Drawing.Point(4, 80)
        Me.lblUltimaConfirmacion.Name = "lblUltimaConfirmacion"
        Me.lblUltimaConfirmacion.Size = New System.Drawing.Size(233, 49)
        '
        'frmConfirmacionTransporte
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblUltimaConfirmacion)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.txtHDR)
        Me.Controls.Add(Me.lblCartaPorte)
        Me.KeyPreview = True
        Me.Name = "frmConfirmacionTransporte"
        Me.Text = "frmConfirmacionTransporte"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblCartaPorte As System.Windows.Forms.Label
    Friend WithEvents txtHDR As System.Windows.Forms.TextBox
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents lblUltimaConfirmacion As System.Windows.Forms.Label
End Class
