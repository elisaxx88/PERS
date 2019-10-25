Partial Class FRMDatiAgg
  Inherits FRM__CHIL
  Friend WithEvents grTotFor As NTSInformatica.NTSGrid
  Friend WithEvents grvTotFor As NTSInformatica.NTSGridView
  Friend WithEvents xx_codfor As NTSInformatica.NTSGridColumn
  Friend WithEvents xx_desfor As NTSInformatica.NTSGridColumn
  Friend WithEvents xx_valfor As NTSInformatica.NTSGridColumn
  Friend WithEvents lbxx_diff As NTSInformatica.NTSLabel
  Friend WithEvents NtsLabel5 As NTSInformatica.NTSLabel
  Friend WithEvents lbxx_marg As NTSInformatica.NTSLabel
  Friend WithEvents lbxx_ricavo As NTSInformatica.NTSLabel
  Friend WithEvents lbxx_costo As NTSInformatica.NTSLabel
  Friend WithEvents NtsLabel3 As NTSInformatica.NTSLabel
  Friend WithEvents NtsLabel2 As NTSInformatica.NTSLabel
  Friend WithEvents NtsLabel1 As NTSInformatica.NTSLabel
  Friend WithEvents grScaPag As NTSInformatica.NTSGrid
  Friend WithEvents grvScaPag As NTSInformatica.NTSGridView
  Friend WithEvents hh_codpaga As NTSInformatica.NTSGridColumn
  Friend WithEvents xx_despaga As NTSInformatica.NTSGridColumn
  Friend WithEvents hh_imppaga As NTSInformatica.NTSGridColumn
  Friend WithEvents hh_datpaga As NTSInformatica.NTSGridColumn
  Friend WithEvents hh_flgpaga As NTSInformatica.NTSGridColumn
  Friend WithEvents hh_numft As NTSInformatica.NTSGridColumn
  Friend WithEvents hh_flgft As NTSInformatica.NTSGridColumn
  Friend WithEvents NtsLabel4 As NTSInformatica.NTSLabel
  Friend WithEvents hh_dataft As NTSInformatica.NTSGridColumn
  Friend WithEvents edhh_chiedi As NTSInformatica.NTSTextBoxStr
  Friend WithEvents NtsLabel6 As NTSInformatica.NTSLabel
  Friend WithEvents NtsLabel7 As NTSInformatica.NTSLabel
  Friend WithEvents edet_datcons As NTSInformatica.NTSTextBoxData
  Friend WithEvents edet_numdoc As NTSInformatica.NTSTextBoxNum
  Friend WithEvents NtsLabel8 As NTSInformatica.NTSLabel
  Friend WithEvents edet_conto As NTSInformatica.NTSTextBoxNum
  Friend WithEvents lbxx_telef As NTSInformatica.NTSLabel
  Friend WithEvents lbxx_conto As NTSInformatica.NTSLabel
  Friend WithEvents NtsLabel9 As NTSInformatica.NTSLabel
  Friend WithEvents cmdCancellaRiga As NTSInformatica.NTSButton
  Friend WithEvents edet_datdoc As NTSInformatica.NTSTextBoxData
  Friend WithEvents NtsLabel11 As NTSInformatica.NTSLabel
  Friend WithEvents edet_totdoc As NTSInformatica.NTSTextBoxNum
  Friend WithEvents NtsLabel12 As NTSInformatica.NTSLabel
  Friend WithEvents edhh_dacocl As NTSInformatica.NTSTextBoxData
  Friend WithEvents NtsLabel13 As NTSInformatica.NTSLabel
  Friend WithEvents lbxx_coddest As NTSInformatica.NTSLabel
  Friend WithEvents edet_coddest As NTSInformatica.NTSTextBoxNum
  Friend WithEvents NtsLabel14 As NTSInformatica.NTSLabel
  Friend WithEvents lbxx_inddestdiv As NTSInformatica.NTSLabel
  Friend WithEvents lbxx_teldestdiv As NTSInformatica.NTSLabel
  Friend WithEvents edet_tipobf As NTSInformatica.NTSTextBoxNum
  Friend WithEvents NtsLabel15 As NTSInformatica.NTSLabel
  Friend WithEvents lbxx_tipobf As NTSInformatica.NTSLabel
  Friend WithEvents NtsLabel16 As NTSInformatica.NTSLabel
  Friend WithEvents edhh_rispon As NTSInformatica.NTSTextBoxStr
  Friend WithEvents NtsLabel17 As NTSInformatica.NTSLabel
  Friend WithEvents edhh_scpian As NTSInformatica.NTSTextBoxStr
  Friend WithEvents NtsLabel18 As NTSInformatica.NTSLabel
  Friend WithEvents ckhh_finanz As NTSInformatica.NTSCheckBox
  Friend WithEvents ckhh_elettr As NTSInformatica.NTSCheckBox
  Friend WithEvents ckhh_ascens As NTSInformatica.NTSCheckBox
  Friend WithEvents edhh_squadra As NTSInformatica.NTSTextBoxStr
  Friend WithEvents NtsLabel20 As NTSInformatica.NTSLabel
  Friend WithEvents edhh_autoca As NTSInformatica.NTSTextBoxStr
  Friend WithEvents NtsLabel19 As NTSInformatica.NTSLabel
  Friend WithEvents lbxx_squadra As NTSInformatica.NTSLabel
  Friend WithEvents edhh_datint As NTSInformatica.NTSTextBoxData
  Friend WithEvents NtsLabel21 As NTSInformatica.NTSLabel
  Friend WithEvents ckhh_mont As NTSInformatica.NTSRadioButton
  Friend WithEvents NtsLabel22 As NTSInformatica.NTSLabel
  Friend WithEvents ckhh_finitu As NTSInformatica.NTSRadioButton
  Friend WithEvents edhh_ttmont As NTSInformatica.NTSTextBoxStr
  Friend WithEvents lbxx_codagen2 As NTSInformatica.NTSLabel
  Friend WithEvents edet_codagen2 As NTSInformatica.NTSTextBoxStr
  Friend WithEvents NtsLabel24 As NTSInformatica.NTSLabel
  Friend WithEvents lbxx_codagen As NTSInformatica.NTSLabel
  Friend WithEvents edet_codagen As NTSInformatica.NTSTextBoxStr
  Friend WithEvents NtsLabel23 As NTSInformatica.NTSLabel
  Friend WithEvents hh_serieft As NTSInformatica.NTSGridColumn
  Friend WithEvents edxx_valresiduo As NTSInformatica.NTSTextBoxNum
  Friend WithEvents NtsLabel25 As NTSInformatica.NTSLabel
  Friend WithEvents NtsFlowLayoutPanel1 As NTSFlowLayoutPanel
  Friend WithEvents NtsGroupBox1 As NTSGroupBox
  Friend WithEvents NtsGroupBox2 As NTSGroupBox
  Friend WithEvents NtsFlowLayoutPanel2 As NTSFlowLayoutPanel
  Friend WithEvents NtsGroupBox3 As NTSGroupBox
  Friend WithEvents NtsGroupBox4 As NTSGroupBox
  Friend WithEvents NtsGroupBox6 As NTSGroupBox
  Friend WithEvents NtsPanel3 As NTSPanel
  Friend WithEvents NtsLabel10 As NTSLabel
  Friend WithEvents NtsPanel1 As NTSPanel
End Class
