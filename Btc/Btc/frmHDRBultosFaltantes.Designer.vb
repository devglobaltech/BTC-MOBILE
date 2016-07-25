<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmHDRBultosFaltantes
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHDRBultosFaltantes))
        Me.DataGrid1 = New System.Windows.Forms.DataGrid
        Me.btnSalir = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'DataGrid1
        '
        resources.ApplyResources(Me.DataGrid1, "DataGrid1")
        Me.DataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.DataGrid1.Name = "DataGrid1"
        '
        'btnSalir
        '
        resources.ApplyResources(Me.btnSalir, "btnSalir")
        Me.btnSalir.Name = "btnSalir"
        '
        'frmHDRBultosFaltantes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        resources.ApplyResources(Me, "$this")
        Me.ControlBox = False
        Me.Controls.Add(Me.DataGrid1)
        Me.Controls.Add(Me.btnSalir)
        Me.KeyPreview = True
        Me.Name = "frmHDRBultosFaltantes"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents btnSalir As System.Windows.Forms.Button
End Class
