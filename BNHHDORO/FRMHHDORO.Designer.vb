
Partial Class FRMHHDORO
  Inherits FRM__CHIL

  <System.Diagnostics.DebuggerNonUserCode()> _
  Public Sub New()
    MyBase.New()
  End Sub

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing AndAlso components IsNot Nothing Then
      components.Dispose()
    End If
    MyBase.Dispose(disposing)
  End Sub
  Friend WithEvents grricerca As NTSInformatica.NTSGrid
  Friend WithEvents grvricerca As NTSInformatica.NTSGridView
  Friend WithEvents NtsPanel1 As NTSPanel
  Friend WithEvents NtsGroupBox1 As NTSGroupBox
  Friend WithEvents NtsGroupBox2 As NTSGroupBox
  Friend WithEvents NtsLabel3 As NTSLabel
  Friend WithEvents CmdRicerca As NTSButton
  Friend WithEvents NtsLabel4 As NTSLabel
  Friend WithEvents NtsLabel2 As NTSLabel
  Friend WithEvents NtsLabel1 As NTSLabel
  Friend WithEvents edxx_adtord As NTSTextBoxData
  Friend WithEvents edxx_adtcons As NTSTextBoxData
  Friend WithEvents edxx_dadtord As NTSTextBoxData
  Friend WithEvents edxx_dadtcons As NTSTextBoxData
  Friend WithEvents CmdElabora As NTSButton
  Friend WithEvents ckxx_soloap As NTSCheckBox
  Friend WithEvents NtsPanel4 As NTSPanel
  Friend WithEvents NtsPanel2 As NTSPanel
  Friend WithEvents NtsPanel3 As NTSPanel
  Friend WithEvents xx_sel As NTSGridColumn
  Friend WithEvents an_descr1 As NTSGridColumn
  Friend WithEvents td_datord As NTSGridColumn
  Friend WithEvents mo_datcons As NTSGridColumn
  Friend WithEvents mo_codart As NTSGridColumn
  Friend WithEvents mo_valoremm As NTSGridColumn
  Friend WithEvents mo_desint As NTSGridColumn
  Friend WithEvents mo_note As NTSGridColumn
  Friend WithEvents mo_anno As NTSGridColumn
  Friend WithEvents mo_serie As NTSGridColumn
  Friend WithEvents mo_numord As NTSGridColumn
  Friend WithEvents mo_riga As NTSGridColumn
  Friend WithEvents mo_flevas As NTSGridColumn
  Friend WithEvents td_riferim As NTSGridColumn
  Friend WithEvents mo_descr As NTSGridColumn
  Friend WithEvents NtsPanel5 As NTSPanel
  Friend WithEvents TlbSelTutto As NTSBarMenuItem
  Friend WithEvents tlbDeselTutto As NTSBarMenuItem
  Friend WithEvents edxx_aconto As NTSTextBoxNum
  Friend WithEvents NtsLabel6 As NTSLabel
  Friend WithEvents edxx_daconto As NTSTextBoxNum
  Friend WithEvents NtsLabel5 As NTSLabel
  Friend WithEvents edxx_dtfatt As NTSTextBoxData
End Class