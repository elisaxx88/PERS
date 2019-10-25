Partial Class FRMHHMATR
  Inherits FRM__CHIL

  <System.Diagnostics.DebuggerNonUserCode()>
  Public Sub New()
    MyBase.New()
  End Sub

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()>
  Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing AndAlso components IsNot Nothing Then
      components.Dispose()
    End If
    MyBase.Dispose(disposing)
  End Sub
  Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
  Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
  Friend WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
  Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
  Friend WithEvents GridColumn1_714_0 As NTSGridColumn
  Friend WithEvents GridColumn1_829_1 As NTSGridColumn
  Friend WithEvents Codice_Commessa As NTSGridColumn
  Friend WithEvents NtsButton1 As NTSButton
  Friend WithEvents Tlb_Seleziona As NTSButton
  Friend WithEvents MATRICOLE As NTSGroupBox
  Friend WithEvents NtsGroupBox2 As NTSGroupBox
  Friend WithEvents grMatricole As NTSGrid
  Friend WithEvents grvMatricole As NTSGridView
  Friend WithEvents rl_matric As NTSGridColumn
  Friend WithEvents rl_dtrilascio As NTSGridColumn
  Friend WithEvents rl_note As NTSGridColumn
  Friend WithEvents rl_tipomatr As NTSGridColumn
  Friend WithEvents rl_dtscadgarven As NTSGridColumn
  Friend WithEvents rl_dtscadgaracq As NTSGridColumn
  Friend WithEvents rl_codart As NTSGridColumn
  Friend WithEvents rl_conto As NTSGridColumn
  Friend WithEvents edxx_codconto As NTSTextBoxStr
  Friend WithEvents NtsLabel2 As NTSLabel
  Friend WithEvents NtsLabel1 As NTSLabel
  Friend WithEvents edxx_matricola As NTSTextBoxStr
  Friend WithEvents Tlb_Ricerca As NTSButton
End Class
