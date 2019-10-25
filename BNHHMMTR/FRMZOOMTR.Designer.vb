Partial Class FRMZOOMTR
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

  Friend WithEvents NtsLabel1 As NTSLabel
  Friend WithEvents edxx_matricola As NTSTextBoxStr
  Friend WithEvents cmdRicerca As NTSButton
  Friend WithEvents grdMatricole As NTSGrid
  Friend WithEvents grvMatricole As NTSGridView
  Friend WithEvents xx_matricola As NTSGridColumn
  Friend WithEvents xx_matrproduttore As NTSGridColumn
  Friend WithEvents xx_codart As NTSGridColumn
  Friend WithEvents xx_note As NTSGridColumn
  Friend WithEvents xx_contocli As NTSGridColumn
  Friend WithEvents xx_descr1 As NTSGridColumn
  Friend WithEvents xx_datainstallazione As NTSGridColumn
  Friend WithEvents xx_dataritiro As NTSGridColumn
  Friend WithEvents xx_ubicazione As NTSGridColumn
End Class
