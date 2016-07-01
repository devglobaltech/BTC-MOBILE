<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Form1
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
        Me.Lst = New System.Windows.Forms.ListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtAccion = New System.Windows.Forms.TextBox
        Me.Timer1 = New System.Windows.Forms.Timer
        Me.SuspendLayout()
        '
        'Lst
        '
        Me.Lst.Location = New System.Drawing.Point(0, 3)
        Me.Lst.Name = "Lst"
        Me.Lst.Size = New System.Drawing.Size(240, 198)
        Me.Lst.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(3, 219)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(131, 20)
        Me.Label1.Text = "Seleccione Accion: "
        Me.Label1.Visible = False
        '
        'txtAccion
        '
        Me.txtAccion.Location = New System.Drawing.Point(140, 218)
        Me.txtAccion.MaxLength = 2
        Me.txtAccion.Name = "txtAccion"
        Me.txtAccion.Size = New System.Drawing.Size(97, 21)
        Me.txtAccion.TabIndex = 2
        Me.txtAccion.Visible = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 10000
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.Controls.Add(Me.txtAccion)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Lst)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Lst As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAccion As System.Windows.Forms.TextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer

End Class
