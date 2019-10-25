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
  Friend WithEvents Tlb_ricerca As NTSButton
  Friend WithEvents VEICOLI As NTSGroupBox
  Friend WithEvents NtsGroupBox2 As NTSGroupBox
  Friend WithEvents lbxx_Cliente As NTSLabel
  Friend WithEvents edxx_codCli As NTSTextBoxNum
  Friend WithEvents edxx_descr As NTSTextBoxStr
  Friend WithEvents NtsLabel1 As NTSLabel
  Friend WithEvents grVeicoli As NTSGrid
  Friend WithEvents grvVeicoli As NTSGridView
  Friend WithEvents hh_tipo As NTSGridColumn
  Friend WithEvents hh_targa As NTSGridColumn
  Friend WithEvents hh_dataimmatr As NTSGridColumn
  Friend WithEvents hh_telaio As NTSGridColumn
  Friend WithEvents hh_nrmotore As NTSGridColumn
  Friend WithEvents hh_km As NTSGridColumn
  Friend WithEvents hh_note As NTSGridColumn
End Class
