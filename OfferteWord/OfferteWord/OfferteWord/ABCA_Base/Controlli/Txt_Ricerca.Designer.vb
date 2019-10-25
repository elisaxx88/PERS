<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Txt_Ricerca
    Inherits System.Windows.Forms.UserControl

    'UserControl esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla nell'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Btn_Search = New System.Windows.Forms.Button()
        Me.Btn_Canc = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Btn_Search
        '
        Me.Btn_Search.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Search.ForeColor = System.Drawing.Color.Green
        Me.Btn_Search.Location = New System.Drawing.Point(0, 0)
        Me.Btn_Search.Name = "Btn_Search"
        Me.Btn_Search.Size = New System.Drawing.Size(21, 23)
        Me.Btn_Search.TabIndex = 0
        Me.Btn_Search.Text = "?"
        Me.Btn_Search.UseVisualStyleBackColor = True
        '
        'Btn_Canc
        '
        Me.Btn_Canc.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Canc.ForeColor = System.Drawing.Color.Red
        Me.Btn_Canc.Location = New System.Drawing.Point(24, 0)
        Me.Btn_Canc.Name = "Btn_Canc"
        Me.Btn_Canc.Size = New System.Drawing.Size(21, 23)
        Me.Btn_Canc.TabIndex = 1
        Me.Btn_Canc.Text = "X"
        Me.Btn_Canc.UseVisualStyleBackColor = True
        '
        'Txt_Ricerca
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Btn_Canc)
        Me.Controls.Add(Me.Btn_Search)
        Me.Name = "Txt_Ricerca"
        Me.Size = New System.Drawing.Size(48, 23)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Btn_Search As System.Windows.Forms.Button
    Friend WithEvents Btn_Canc As System.Windows.Forms.Button

End Class
