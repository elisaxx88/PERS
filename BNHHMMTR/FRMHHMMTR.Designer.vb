
Partial Class FRMHHMMTR
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
  Friend WithEvents TlbSelTutto As NTSBarMenuItem
  Friend WithEvents tlbDeselTutto As NTSBarMenuItem
  Friend WithEvents NtsPanel1 As NTSPanel
  Friend WithEvents NtsLabel1 As NTSLabel
  Friend WithEvents edxx_matricola As NTSTextBoxStr
  Friend WithEvents cmdCambia As NTSButton
  Friend WithEvents chxx_vendutodaterzi As NTSCheckBox
  Friend WithEvents chxx_muletto As NTSCheckBox
  Friend WithEvents chxx_dismesso As NTSCheckBox
  Friend WithEvents chxx_usato As NTSCheckBox
  Friend WithEvents edxx_duratagranzia As NTSTextBoxStr
  Friend WithEvents NtsLabel5 As NTSLabel
  Friend WithEvents lbxx_descr As NTSLabel
  Friend WithEvents edxx_articolo As NTSTextBoxStr
  Friend WithEvents NtsLabel4 As NTSLabel
  Friend WithEvents edxx_matricolaproduttore As NTSTextBoxStr
  Friend WithEvents NtsLabel3 As NTSLabel
  Friend WithEvents edxx_nuovamatricola As NTSTextBoxStr
  Friend WithEvents NtsLabel2 As NTSLabel
  Friend WithEvents cmdRicerca As NTSButton
  Friend WithEvents NtsTabControl1 As NTSTabControl
  Friend WithEvents NtsTabPage1 As NTSTabPage
  Friend WithEvents CLIENTI As NTSGroupBox
  Friend WithEvents grdClienti As NTSGrid
  Friend WithEvents grvClienti As NTSGridView
  Friend WithEvents hh_contocli As NTSGridColumn
  Friend WithEvents an_descr1 As NTSGridColumn
  Friend WithEvents hh_datainstallazione As NTSGridColumn
  Friend WithEvents hh_dataritiro As NTSGridColumn
  Friend WithEvents hh_contratto As NTSGridColumn
  Friend WithEvents hh_datascadenzacontratto As NTSGridColumn
  Friend WithEvents xx_noleggiovendita As NTSGridColumn
  Friend WithEvents hh_datavendita As NTSGridColumn
  Friend WithEvents hh_scadenza As NTSGridColumn
  Friend WithEvents hh_note As NTSGridColumn
  Friend WithEvents NtsTabPage2 As NTSTabPage
  Friend WithEvents NtsTabPage3 As NTSTabPage
  Friend WithEvents NtsGroupBox1 As NTSGroupBox
  Friend WithEvents grdDoctutti As NTSGrid
  Friend WithEvents grvDoctutti As NTSGridView
  Friend WithEvents hh_tipo As NTSGridColumn
  Friend WithEvents hh_numero As NTSGridColumn
  Friend WithEvents hh_data As NTSGridColumn
  Friend WithEvents hh_codcf As NTSGridColumn
  Friend WithEvents hh_ragsoc As NTSGridColumn
  Friend WithEvents NtsGroupBox2 As NTSGroupBox
  Friend WithEvents grdDocint As NTSGrid
  Friend WithEvents grvDocint As NTSGridView
End Class