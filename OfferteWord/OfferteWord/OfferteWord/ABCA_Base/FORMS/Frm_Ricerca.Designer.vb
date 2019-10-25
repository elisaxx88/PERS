<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Ricerca
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
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
        Me.DGV = New System.Windows.Forms.DataGridView()
        Me.Txt_Ricerca = New System.Windows.Forms.TextBox()
        Me.Cmb_Filtri = New System.Windows.Forms.ComboBox()
        Me.Lbl_Ricerca = New System.Windows.Forms.Label()
        Me.Lbl_Filtri = New System.Windows.Forms.Label()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGV
        '
        Me.DGV.AllowUserToAddRows = False
        Me.DGV.AllowUserToDeleteRows = False
        Me.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV.Location = New System.Drawing.Point(10, 55)
        Me.DGV.Name = "DGV"
        Me.DGV.Size = New System.Drawing.Size(558, 302)
        Me.DGV.TabIndex = 0
        Me.DGV.TabStop = False
        '
        'Txt_Ricerca
        '
        Me.Txt_Ricerca.Location = New System.Drawing.Point(10, 28)
        Me.Txt_Ricerca.Name = "Txt_Ricerca"
        Me.Txt_Ricerca.Size = New System.Drawing.Size(239, 20)
        Me.Txt_Ricerca.TabIndex = 0
        '
        'Cmb_Filtri
        '
        Me.Cmb_Filtri.FormattingEnabled = True
        Me.Cmb_Filtri.Location = New System.Drawing.Point(441, 28)
        Me.Cmb_Filtri.Name = "Cmb_Filtri"
        Me.Cmb_Filtri.Size = New System.Drawing.Size(127, 21)
        Me.Cmb_Filtri.TabIndex = 1
        '
        'Lbl_Ricerca
        '
        Me.Lbl_Ricerca.AutoSize = True
        Me.Lbl_Ricerca.Location = New System.Drawing.Point(7, 7)
        Me.Lbl_Ricerca.Name = "Lbl_Ricerca"
        Me.Lbl_Ricerca.Size = New System.Drawing.Size(44, 13)
        Me.Lbl_Ricerca.TabIndex = 16
        Me.Lbl_Ricerca.Text = "Ricerca"
        '
        'Lbl_Filtri
        '
        Me.Lbl_Filtri.AutoSize = True
        Me.Lbl_Filtri.Location = New System.Drawing.Point(438, 7)
        Me.Lbl_Filtri.Name = "Lbl_Filtri"
        Me.Lbl_Filtri.Size = New System.Drawing.Size(25, 13)
        Me.Lbl_Filtri.TabIndex = 17
        Me.Lbl_Filtri.Text = "Filtri"
        '
        'Frm_Ricerca
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(575, 370)
        Me.Controls.Add(Me.Lbl_Filtri)
        Me.Controls.Add(Me.Lbl_Ricerca)
        Me.Controls.Add(Me.Txt_Ricerca)
        Me.Controls.Add(Me.Cmb_Filtri)
        Me.Controls.Add(Me.DGV)
        Me.Name = "Frm_Ricerca"
        Me.Text = "Frm_Ricerca"
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DGV As System.Windows.Forms.DataGridView
    Friend WithEvents Txt_Ricerca As System.Windows.Forms.TextBox
    Friend WithEvents Cmb_Filtri As System.Windows.Forms.ComboBox
    Friend WithEvents Lbl_Ricerca As System.Windows.Forms.Label
    Friend WithEvents Lbl_Filtri As System.Windows.Forms.Label
End Class
