Imports System.Data
Imports DevExpress.XtraBars
Imports NTSInformatica.CLN__STD
Public Class FROORGSOR
  Inherits FRMORGSOR
  Public WithEvents Tmr1 As New Timer
  Public WithEvents cmdhhCreaRigheScad As NTSinformatica.NTSBarButtonItem
  'Public WithEvents grOrFo As NTSInformatica.NTSGrid
  'Public WithEvents GrvOrFo As NTSInformatica.NTSGridView
  'Public WithEvents xx_cpneofconto As NTSInformatica.NTSGridColumn
  'Public WithEvents an_descr1 As NTSInformatica.NTSGridColumn
  'Public WithEvents mo_riga As NTSInformatica.NTSGridColumn
  'Public WithEvents mo_codart As NTSInformatica.NTSGridColumn
  'Public WithEvents mo_descr As NTSInformatica.NTSGridColumn
  'Public WithEvents mo_desint As NTSInformatica.NTSGridColumn
  'Public WithEvents mo_quant As NTSInformatica.NTSGridColumn
  'Public WithEvents mo_prezzo As NTSInformatica.NTSGridColumn
  'Public WithEvents mo_quaeva As NTSInformatica.NTSGridColumn
  'Public WithEvents mo_flevas As NTSInformatica.NTSGridColumn
  'Public WithEvents mo_commeca As NTSInformatica.NTSGridColumn
  'Public WithEvents mo_subcommeca As NTSInformatica.NTSGridColumn
  'Public WithEvents mo_note As NTSInformatica.NTSGridColumn
  'Public WithEvents mo_scont1 As NTSInformatica.NTSGridColumn
  'Public WithEvents mo_scont2 As NTSInformatica.NTSGridColumn
  'Public WithEvents mo_scont3 As NTSInformatica.NTSGridColumn
  'Public WithEvents mo_scont4 As NTSInformatica.NTSGridColumn
  'Public WithEvents mo_scont5 As NTSInformatica.NTSGridColumn
  'Public WithEvents mo_scont6 As NTSInformatica.NTSGridColumn
  'Public WithEvents mo_valoremm As NTSInformatica.NTSGridColumn
  Dim PnCPNEOrfo As NTSPanel
  Dim grOrFo As NTSInformatica.NTSGrid
  Dim GrvOrFo As NTSInformatica.NTSGridView
  Dim mo_riga As NTSInformatica.NTSGridColumn
  Dim mo_note As NTSInformatica.NTSGridColumn
  Dim xx_cpneofconto As NTSInformatica.NTSGridColumn
  Public Dchh As BindingSource = New BindingSource
  Dim strColoreRigaRossoOF1 As String
  Dim strColoreRigaRossoOF2 As String
  Dim strColoreRigaRossoOF3 As String
  Dim strColoreRigaVerdeOF1 As String
  Dim strColoreRigaVerdeOF2 As String
  Dim strColoreRigaVerdeOF3 As String
  Dim strColoreRigaGialloOF1 As String
  Dim strColoreRigaGialloOF2 As String
  Dim strColoreRigaGialloOF3 As String
  Dim strCampoGrigliaDaColorareOF As String
  Dim ckbloccaprzforn As NTSCheckBox
  Dim ckCPNEhh_Reso As NTSCheckBox
  Dim hh_rifmatr As NTSGridColumn

  Public Overrides Sub FRMORGSOR_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs)
    MyBase.FRMORGSOR_ActivatedFirst(sender, e)


    Dim intheigth As Integer = grRighe.Height
    'GrvOrFo.NTSAllowDelete = True
    'PnCPNEOrfo.Visible = True
    'grTco.Visible = False
    Tmr1.Enabled = False
    Tmr1.Interval = 1000
    'grOrFo.Top = 20
    grOrFo.Left = ckbloccaprzforn.Width + 20
    grOrFo.Width = pnTCO.Width - grOrFo.Left
    ckbloccaprzforn.Left = 0
    ckbloccaprzforn.Top = 0
    ckCPNEhh_Reso.Left = 0
    ckCPNEhh_Reso.Top = 30
    'If fmDocumento.Top <> fmDocTop.Top Then
    '  fmDocumento.Top = fmDocTop.Top
    '  cbTipoDoc.Location = New System.Drawing.Point(4, 24)
    '  edAnnoDoc.Location = New System.Drawing.Point(4, 48)
    '  edSerieDoc.Location = New System.Drawing.Point(80, 48)
    '  edNumDoc.Location = New System.Drawing.Point(132, 48)
    'End If
  End Sub

  'Private Sub CpneInitControls()
  '  Me.grOrFo = New NTSInformatica.NTSGrid()
  '  Me.GrvOrFo = New NTSInformatica.NTSGridView()
  '  Me.xx_cpneofconto = New NTSInformatica.NTSGridColumn()
  '  Me.an_descr1 = New NTSInformatica.NTSGridColumn()
  '  Me.mo_riga = New NTSInformatica.NTSGridColumn()
  '  Me.mo_codart = New NTSInformatica.NTSGridColumn()
  '  Me.mo_descr = New NTSInformatica.NTSGridColumn()
  '  Me.mo_desint = New NTSInformatica.NTSGridColumn()
  '  Me.mo_note = New NTSInformatica.NTSGridColumn()
  '  Me.mo_quant = New NTSInformatica.NTSGridColumn()
  '  Me.mo_prezzo = New NTSInformatica.NTSGridColumn()
  '  Me.mo_quaeva = New NTSInformatica.NTSGridColumn()
  '  Me.mo_flevas = New NTSInformatica.NTSGridColumn()
  '  Me.mo_commeca = New NTSInformatica.NTSGridColumn()
  '  Me.mo_subcommeca = New NTSInformatica.NTSGridColumn()
  '  Me.mo_scont1 = New NTSInformatica.NTSGridColumn()
  '  Me.mo_scont2 = New NTSInformatica.NTSGridColumn()
  '  Me.mo_scont3 = New NTSInformatica.NTSGridColumn()
  '  Me.mo_scont4 = New NTSInformatica.NTSGridColumn()
  '  Me.mo_scont5 = New NTSInformatica.NTSGridColumn()
  '  Me.mo_scont6 = New NTSInformatica.NTSGridColumn()
  '  Me.mo_valoremm = New NTSInformatica.NTSGridColumn()


  '  CType(Me.grOrFo, System.ComponentModel.ISupportInitialize).BeginInit()
  '  CType(Me.GrvOrFo, System.ComponentModel.ISupportInitialize).BeginInit()

  '  PnCPNEOrfo.Controls.Add(Me.grOrFo)

  '  '
  '  'grOrFo
  '  '
  '  '
  '  '
  '  Me.grOrFo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
  '          Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
  '  '
  '  Me.grOrFo.EmbeddedNavigator.Name = ""
  '  Me.grOrFo.Location = New System.Drawing.Point(20, 10)
  '  Me.grOrFo.MainView = Me.GrvOrFo
  '  Me.grOrFo.Name = "grOrFo"
  '  Me.grOrFo.Size = New System.Drawing.Size(100, 100)
  '  Me.grOrFo.TabIndex = 46
  '  Me.grOrFo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GrvOrFo})
  '  'Me.grOrFo.Dock = DockStyle.Fill
  '  '
  '  'GrvOrFo
  '  '
  '  Me.GrvOrFo.ActiveFilterEnabled = False
  '  '
  '  'xx_cpneofconto
  '  '
  '  Me.xx_cpneofconto.AppearanceCell.Options.UseBackColor = True
  '  Me.xx_cpneofconto.AppearanceCell.Options.UseTextOptions = True
  '  Me.xx_cpneofconto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
  '  Me.xx_cpneofconto.Caption = "Conto"
  '  Me.xx_cpneofconto.Enabled = True
  '  Me.xx_cpneofconto.FieldName = "xx_cpneofconto"
  '  Me.xx_cpneofconto.Name = "xx_cpneofconto"
  '  Me.xx_cpneofconto.NTSRepositoryComboBox = Nothing
  '  Me.xx_cpneofconto.NTSRepositoryItemCheck = Nothing
  '  Me.xx_cpneofconto.NTSRepositoryItemMemo = Nothing
  '  Me.xx_cpneofconto.NTSRepositoryItemText = Nothing
  '  Me.xx_cpneofconto.Visible = True
  '  Me.xx_cpneofconto.VisibleIndex = 0
  '  '
  '  'an_descr1
  '  '
  '  Me.an_descr1.AppearanceCell.Options.UseBackColor = True
  '  Me.an_descr1.AppearanceCell.Options.UseTextOptions = True
  '  Me.an_descr1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
  '  Me.an_descr1.Caption = "Des. Conto"
  '  Me.an_descr1.Enabled = True
  '  Me.an_descr1.FieldName = "an_descr1"
  '  Me.an_descr1.Name = "an_descr1"
  '  Me.an_descr1.NTSRepositoryComboBox = Nothing
  '  Me.an_descr1.NTSRepositoryItemCheck = Nothing
  '  Me.an_descr1.NTSRepositoryItemMemo = Nothing
  '  Me.an_descr1.NTSRepositoryItemText = Nothing
  '  Me.an_descr1.Visible = True
  '  Me.an_descr1.VisibleIndex = 1
  '  '
  '  'mo_riga
  '  '
  '  Me.mo_riga.AppearanceCell.Options.UseBackColor = True
  '  Me.mo_riga.AppearanceCell.Options.UseTextOptions = True
  '  Me.mo_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
  '  Me.mo_riga.Caption = "Riga"
  '  Me.mo_riga.Enabled = True
  '  Me.mo_riga.FieldName = "mo_riga"
  '  Me.mo_riga.Name = "mo_riga"
  '  Me.mo_riga.NTSRepositoryComboBox = Nothing
  '  Me.mo_riga.NTSRepositoryItemCheck = Nothing
  '  Me.mo_riga.NTSRepositoryItemMemo = Nothing
  '  Me.mo_riga.NTSRepositoryItemText = Nothing
  '  Me.mo_riga.Visible = True
  '  Me.mo_riga.VisibleIndex = 2
  '  '
  '  'mo_codart
  '  '
  '  Me.mo_codart.AppearanceCell.Options.UseBackColor = True
  '  Me.mo_codart.AppearanceCell.Options.UseTextOptions = True
  '  Me.mo_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
  '  Me.mo_codart.Caption = "Codice Articolo"
  '  Me.mo_codart.Enabled = True
  '  Me.mo_codart.FieldName = "mo_codart"
  '  Me.mo_codart.Name = "mo_codart"
  '  Me.mo_codart.NTSRepositoryComboBox = Nothing
  '  Me.mo_codart.NTSRepositoryItemCheck = Nothing
  '  Me.mo_codart.NTSRepositoryItemMemo = Nothing
  '  Me.mo_codart.NTSRepositoryItemText = Nothing
  '  Me.mo_codart.Visible = True
  '  Me.mo_codart.VisibleIndex = 3
  '  '
  '  'mo_descr
  '  '
  '  Me.mo_descr.AppearanceCell.Options.UseBackColor = True
  '  Me.mo_descr.AppearanceCell.Options.UseTextOptions = True
  '  Me.mo_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
  '  Me.mo_descr.Caption = "Descr. Art."
  '  Me.mo_descr.Enabled = True
  '  Me.mo_descr.FieldName = "mo_descr"
  '  Me.mo_descr.Name = "mo_descr"
  '  Me.mo_descr.NTSRepositoryComboBox = Nothing
  '  Me.mo_descr.NTSRepositoryItemCheck = Nothing
  '  Me.mo_descr.NTSRepositoryItemMemo = Nothing
  '  Me.mo_descr.NTSRepositoryItemText = Nothing
  '  Me.mo_descr.Visible = True
  '  Me.mo_descr.VisibleIndex = 4
  '  '
  '  'mo_desint
  '  '
  '  Me.mo_desint.AppearanceCell.Options.UseBackColor = True
  '  Me.mo_desint.AppearanceCell.Options.UseTextOptions = True
  '  Me.mo_desint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
  '  Me.mo_desint.Caption = "Descr. Int."
  '  Me.mo_desint.Enabled = True
  '  Me.mo_desint.FieldName = "mo_desint"
  '  Me.mo_desint.Name = "mo_desint"
  '  Me.mo_desint.NTSRepositoryComboBox = Nothing
  '  Me.mo_desint.NTSRepositoryItemCheck = Nothing
  '  Me.mo_desint.NTSRepositoryItemMemo = Nothing
  '  Me.mo_desint.NTSRepositoryItemText = Nothing
  '  Me.mo_desint.Visible = True
  '  Me.mo_desint.VisibleIndex = 5
  '  '
  '  'mo_quant
  '  '
  '  Me.mo_quant.AppearanceCell.Options.UseBackColor = True
  '  Me.mo_quant.AppearanceCell.Options.UseTextOptions = True
  '  Me.mo_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
  '  Me.mo_quant.Caption = "Q.ta"
  '  Me.mo_quant.Enabled = True
  '  Me.mo_quant.FieldName = "mo_quant"
  '  Me.mo_quant.Name = "mo_quant"
  '  Me.mo_quant.NTSRepositoryComboBox = Nothing
  '  Me.mo_quant.NTSRepositoryItemCheck = Nothing
  '  Me.mo_quant.NTSRepositoryItemMemo = Nothing
  '  Me.mo_quant.NTSRepositoryItemText = Nothing
  '  Me.mo_quant.Visible = True
  '  Me.mo_quant.VisibleIndex = 6
  '  '
  '  'mo_prezzo
  '  '
  '  Me.mo_prezzo.AppearanceCell.Options.UseBackColor = True
  '  Me.mo_prezzo.AppearanceCell.Options.UseTextOptions = True
  '  Me.mo_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
  '  Me.mo_prezzo.Caption = "Prezzo"
  '  Me.mo_prezzo.Enabled = True
  '  Me.mo_prezzo.FieldName = "mo_prezzo"
  '  Me.mo_prezzo.Name = "mo_prezzo"
  '  Me.mo_prezzo.NTSRepositoryComboBox = Nothing
  '  Me.mo_prezzo.NTSRepositoryItemCheck = Nothing
  '  Me.mo_prezzo.NTSRepositoryItemMemo = Nothing
  '  Me.mo_prezzo.NTSRepositoryItemText = Nothing
  '  Me.mo_prezzo.Visible = True
  '  Me.mo_prezzo.VisibleIndex = 7
  '  '
  '  'mo_quaeva
  '  '
  '  Me.mo_quaeva.AppearanceCell.Options.UseBackColor = True
  '  Me.mo_quaeva.AppearanceCell.Options.UseTextOptions = True
  '  Me.mo_quaeva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
  '  Me.mo_quaeva.Caption = "Q.ta Evasa"
  '  Me.mo_quaeva.Enabled = True
  '  Me.mo_quaeva.FieldName = "mo_quaeva"
  '  Me.mo_quaeva.Name = "mo_quaeva"
  '  Me.mo_quaeva.NTSRepositoryComboBox = Nothing
  '  Me.mo_quaeva.NTSRepositoryItemCheck = Nothing
  '  Me.mo_quaeva.NTSRepositoryItemMemo = Nothing
  '  Me.mo_quaeva.NTSRepositoryItemText = Nothing
  '  Me.mo_quaeva.Visible = True
  '  Me.mo_quaeva.VisibleIndex = 8
  '  '
  '  'mo_flevas
  '  '
  '  Me.mo_flevas.AppearanceCell.Options.UseBackColor = True
  '  Me.mo_flevas.AppearanceCell.Options.UseTextOptions = True
  '  Me.mo_flevas.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
  '  Me.mo_flevas.Caption = "Evas"
  '  Me.mo_flevas.Enabled = True
  '  Me.mo_flevas.FieldName = "mo_flevas"
  '  Me.mo_flevas.Name = "mo_flevas"
  '  Me.mo_flevas.NTSRepositoryComboBox = Nothing
  '  Me.mo_flevas.NTSRepositoryItemCheck = Nothing
  '  Me.mo_flevas.NTSRepositoryItemMemo = Nothing
  '  Me.mo_flevas.NTSRepositoryItemText = Nothing
  '  Me.mo_flevas.Visible = True
  '  Me.mo_flevas.VisibleIndex = 9
  '  '
  '  'mo_commeca
  '  '
  '  Me.mo_commeca.AppearanceCell.Options.UseBackColor = True
  '  Me.mo_commeca.AppearanceCell.Options.UseTextOptions = True
  '  Me.mo_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
  '  Me.mo_commeca.Caption = "Commessa"
  '  Me.mo_commeca.Enabled = True
  '  Me.mo_commeca.FieldName = "mo_commeca"
  '  Me.mo_commeca.Name = "mo_commeca"
  '  Me.mo_commeca.NTSRepositoryComboBox = Nothing
  '  Me.mo_commeca.NTSRepositoryItemCheck = Nothing
  '  Me.mo_commeca.NTSRepositoryItemMemo = Nothing
  '  Me.mo_commeca.NTSRepositoryItemText = Nothing
  '  Me.mo_commeca.Visible = True
  '  Me.mo_commeca.VisibleIndex = 10
  '  '
  '  'mo_subcommeca
  '  '
  '  Me.mo_subcommeca.AppearanceCell.Options.UseBackColor = True
  '  Me.mo_subcommeca.AppearanceCell.Options.UseTextOptions = True
  '  Me.mo_subcommeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
  '  Me.mo_subcommeca.Caption = "Sottocommessa"
  '  Me.mo_subcommeca.Enabled = True
  '  Me.mo_subcommeca.FieldName = "mo_subcommeca"
  '  Me.mo_subcommeca.Name = "mo_subcommeca"
  '  Me.mo_subcommeca.NTSRepositoryComboBox = Nothing
  '  Me.mo_subcommeca.NTSRepositoryItemCheck = Nothing
  '  Me.mo_subcommeca.NTSRepositoryItemMemo = Nothing
  '  Me.mo_subcommeca.NTSRepositoryItemText = Nothing
  '  Me.mo_subcommeca.Visible = True
  '  Me.mo_subcommeca.VisibleIndex = 11
  '  '
  '  'mo_note
  '  '
  '  Me.mo_note.AppearanceCell.Options.UseBackColor = True
  '  Me.mo_note.AppearanceCell.Options.UseTextOptions = True
  '  Me.mo_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
  '  Me.mo_note.Caption = "Note"
  '  Me.mo_note.Enabled = True
  '  Me.mo_note.FieldName = "mo_note"
  '  Me.mo_note.Name = "mo_note"
  '  Me.mo_note.NTSRepositoryComboBox = Nothing
  '  Me.mo_note.NTSRepositoryItemCheck = Nothing
  '  Me.mo_note.NTSRepositoryItemMemo = Nothing
  '  Me.mo_note.NTSRepositoryItemText = Nothing
  '  Me.mo_note.Visible = True
  '  Me.mo_note.VisibleIndex = 12
  '  '
  '  'mo_scont1
  '  '
  '  Me.mo_scont1.AppearanceCell.Options.UseBackColor = True
  '  Me.mo_scont1.AppearanceCell.Options.UseTextOptions = True
  '  Me.mo_scont1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
  '  Me.mo_scont1.Caption = "Sconto1"
  '  Me.mo_scont1.Enabled = True
  '  Me.mo_scont1.FieldName = "mo_scont1"
  '  Me.mo_scont1.Name = "mo_scont1"
  '  Me.mo_scont1.NTSRepositoryComboBox = Nothing
  '  Me.mo_scont1.NTSRepositoryItemCheck = Nothing
  '  Me.mo_scont1.NTSRepositoryItemMemo = Nothing
  '  Me.mo_scont1.NTSRepositoryItemText = Nothing
  '  Me.mo_scont1.Visible = True
  '  Me.mo_scont1.VisibleIndex = 13
  '  '
  '  'mo_scont2
  '  '
  '  Me.mo_scont2.AppearanceCell.Options.UseBackColor = True
  '  Me.mo_scont2.AppearanceCell.Options.UseTextOptions = True
  '  Me.mo_scont2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
  '  Me.mo_scont2.Caption = "Sconto2"
  '  Me.mo_scont2.Enabled = True
  '  Me.mo_scont2.FieldName = "mo_scont2"
  '  Me.mo_scont2.Name = "mo_scont2"
  '  Me.mo_scont2.NTSRepositoryComboBox = Nothing
  '  Me.mo_scont2.NTSRepositoryItemCheck = Nothing
  '  Me.mo_scont2.NTSRepositoryItemMemo = Nothing
  '  Me.mo_scont2.NTSRepositoryItemText = Nothing
  '  Me.mo_scont2.Visible = True
  '  Me.mo_scont2.VisibleIndex = 14
  '  '
  '  'mo_scont3
  '  '
  '  Me.mo_scont3.AppearanceCell.Options.UseBackColor = True
  '  Me.mo_scont3.AppearanceCell.Options.UseTextOptions = True
  '  Me.mo_scont3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
  '  Me.mo_scont3.Caption = "Sconto3"
  '  Me.mo_scont3.Enabled = True
  '  Me.mo_scont3.FieldName = "mo_scont3"
  '  Me.mo_scont3.Name = "mo_scont3"
  '  Me.mo_scont3.NTSRepositoryComboBox = Nothing
  '  Me.mo_scont3.NTSRepositoryItemCheck = Nothing
  '  Me.mo_scont3.NTSRepositoryItemMemo = Nothing
  '  Me.mo_scont3.NTSRepositoryItemText = Nothing
  '  Me.mo_scont3.Visible = True
  '  Me.mo_scont3.VisibleIndex = 14
  '  '
  '  'mo_scont4
  '  '
  '  Me.mo_scont4.AppearanceCell.Options.UseBackColor = True
  '  Me.mo_scont4.AppearanceCell.Options.UseTextOptions = True
  '  Me.mo_scont4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
  '  Me.mo_scont4.Caption = "Sconto4"
  '  Me.mo_scont4.Enabled = True
  '  Me.mo_scont4.FieldName = "mo_scont4"
  '  Me.mo_scont4.Name = "mo_scont4"
  '  Me.mo_scont4.NTSRepositoryComboBox = Nothing
  '  Me.mo_scont4.NTSRepositoryItemCheck = Nothing
  '  Me.mo_scont4.NTSRepositoryItemMemo = Nothing
  '  Me.mo_scont4.NTSRepositoryItemText = Nothing
  '  Me.mo_scont4.Visible = True
  '  Me.mo_scont4.VisibleIndex = 15
  '  '
  '  'mo_scont5
  '  '
  '  Me.mo_scont5.AppearanceCell.Options.UseBackColor = True
  '  Me.mo_scont5.AppearanceCell.Options.UseTextOptions = True
  '  Me.mo_scont5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
  '  Me.mo_scont5.Caption = "Sconto5"
  '  Me.mo_scont5.Enabled = True
  '  Me.mo_scont5.FieldName = "mo_scont5"
  '  Me.mo_scont5.Name = "mo_scont5"
  '  Me.mo_scont5.NTSRepositoryComboBox = Nothing
  '  Me.mo_scont5.NTSRepositoryItemCheck = Nothing
  '  Me.mo_scont5.NTSRepositoryItemMemo = Nothing
  '  Me.mo_scont5.NTSRepositoryItemText = Nothing
  '  Me.mo_scont5.Visible = True
  '  Me.mo_scont5.VisibleIndex = 16
  '  '
  '  'mo_scont6
  '  '
  '  Me.mo_scont6.AppearanceCell.Options.UseBackColor = True
  '  Me.mo_scont6.AppearanceCell.Options.UseTextOptions = True
  '  Me.mo_scont6.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
  '  Me.mo_scont6.Caption = "Sconto6"
  '  Me.mo_scont6.Enabled = True
  '  Me.mo_scont6.FieldName = "mo_scont6"
  '  Me.mo_scont6.Name = "mo_scont6"
  '  Me.mo_scont6.NTSRepositoryComboBox = Nothing
  '  Me.mo_scont6.NTSRepositoryItemCheck = Nothing
  '  Me.mo_scont6.NTSRepositoryItemMemo = Nothing
  '  Me.mo_scont6.NTSRepositoryItemText = Nothing
  '  Me.mo_scont6.Visible = True
  '  Me.mo_scont6.VisibleIndex = 17
  '  '
  '  'mo_valoremm
  '  '
  '  Me.mo_valoremm.AppearanceCell.Options.UseBackColor = True
  '  Me.mo_valoremm.AppearanceCell.Options.UseTextOptions = True
  '  Me.mo_valoremm.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
  '  Me.mo_valoremm.Caption = "Valore riga"
  '  Me.mo_valoremm.Enabled = True
  '  Me.mo_valoremm.FieldName = "mo_valoremm"
  '  Me.mo_valoremm.Name = "mo_valoremm"
  '  Me.mo_valoremm.NTSRepositoryComboBox = Nothing
  '  Me.mo_valoremm.NTSRepositoryItemCheck = Nothing
  '  Me.mo_valoremm.NTSRepositoryItemMemo = Nothing
  '  Me.mo_valoremm.NTSRepositoryItemText = Nothing
  '  Me.mo_valoremm.Visible = True
  '  Me.mo_valoremm.VisibleIndex = 18



  '  Me.GrvOrFo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_cpneofconto, Me.an_descr1, Me.mo_riga, Me.mo_codart, Me.mo_descr, Me.mo_desint, Me.mo_quant, Me.mo_prezzo, Me.mo_quaeva, Me.mo_flevas, Me.mo_commeca, Me.mo_subcommeca, Me.mo_note, Me.mo_scont1, Me.mo_scont2, Me.mo_scont3, Me.mo_scont4, Me.mo_scont5, Me.mo_scont6, Me.mo_valoremm})
  '  Me.GrvOrFo.Enabled = True
  '  Me.GrvOrFo.GridControl = Me.grOrFo
  '  Me.GrvOrFo.Name = "GrvOrFo"
  '  Me.GrvOrFo.NTSAllowDelete = True
  '  Me.GrvOrFo.NTSAllowInsert = True
  '  Me.GrvOrFo.NTSAllowUpdate = True
  '  Me.GrvOrFo.NTSMenuContext = Nothing
  '  Me.GrvOrFo.OptionsCustomization.AllowRowSizing = True
  '  Me.GrvOrFo.OptionsFilter.AllowFilterEditor = False
  '  Me.GrvOrFo.OptionsNavigation.EnterMoveNextColumn = True
  '  Me.GrvOrFo.OptionsNavigation.UseTabKey = False
  '  Me.GrvOrFo.OptionsSelection.EnableAppearanceFocusedRow = False
  '  Me.GrvOrFo.OptionsView.ColumnAutoWidth = False
  '  Me.GrvOrFo.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
  '  Me.GrvOrFo.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
  '  Me.GrvOrFo.OptionsView.ShowGroupPanel = False
  '  Me.GrvOrFo.RowHeight = 14
  '  CType(Me.grOrFo, System.ComponentModel.ISupportInitialize).EndInit()
  '  CType(Me.GrvOrFo, System.ComponentModel.ISupportInitialize).EndInit()
  '  GrvOrFo.NTSSetParam(oMenu, "OrdForn")
  '  xx_cpneofconto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 130132764741973791, "Conto"), tabanagraf)
  '  an_descr1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130132764741983798, "Des. Conto"), 0, True)
  '  mo_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130132764741993790, "Riga"), "0", 9, 0, 999999999)
  '  mo_codart.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 130132764742003783, "Codice Articolo"), tabartico, True)
  '  mo_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130132764742013785, "Descr. Art."), 0, True)
  '  mo_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130132764742013786, "Note"), 0, True)
  '  mo_desint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130132764742023791, "Descr. Int."), 0, True)
  '  mo_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130132764742033807, "Q.ta"), "#,##0.00", 15)
  '  mo_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130132764742043804, "Prezzo"), "#,##0.00", 15)
  '  mo_scont1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130132764742043804, "Prezzo"), oApp.FormatSconti, 6, -100, 100)
  '  mo_scont2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130132764742043804, "Prezzo"), oApp.FormatSconti, 6, -100, 100)
  '  mo_scont3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130132764742043804, "Prezzo"), oApp.FormatSconti, 6, -100, 100)
  '  mo_scont4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130132764742043804, "Prezzo"), oApp.FormatSconti, 6, -100, 100)
  '  mo_scont5.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130132764742043804, "Prezzo"), oApp.FormatSconti, 6, -100, 100)
  '  mo_scont6.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130132764742043804, "Prezzo"), oApp.FormatSconti, 6, -100, 100)
  '  mo_valoremm.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130132764742043804, "Valore riga"), "#,##0.00", 15)


  '  mo_quaeva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130132764742053806, "Q.ta Evasa"), "#,##0.00", 15)
  '  mo_flevas.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130132764742063789, "Evas"), "S", "C")
  '  mo_commeca.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130132764742073786, "Commessa"), "0", 9, 0, 999999999)
  '  mo_subcommeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130132764742083788, "Sottocommessa"), 0, True)


  '  mo_prezzo.NTSSetParamZoom("ZOOMPREZZO")
  '  mo_prezzo.NTSForzaVisZoom = True



  '  Me.grOrFo.Location = New System.Drawing.Point(0, 0)

  'End Sub

  Public Overrides Sub InitControls()
    Try
      MyBase.InitControls()
      PnCPNEOrfo = CType(NTSFindControlByName(Me, "PnCPNEOrfo"), NTSPanel)
      grOrFo = CType(NTSFindControlByName(Me, "grOrFo"), NTSGrid)
      GrvOrFo = CType(NTSFindControlByName(Me, "GrvOrFo"), NTSGridView)
      mo_note = CType(GrvOrFo.Columns("mo_note"), NTSGridColumn)
      mo_riga = CType(GrvOrFo.Columns("mo_riga"), NTSGridColumn)
      xx_cpneofconto = CType(GrvOrFo.Columns("xx_cpneofconto"), NTSGridColumn)
      ckbloccaprzforn = CType(NTSFindControlByName(Me, "ckbloccaprzforn"), NTSCheckBox)
      ckCPNEhh_Reso = CType(NTSFindControlByName(Me, "ckCPNEhh_Reso"), NTSCheckBox)

      AddHandler GrvOrFo.GotFocus, AddressOf GrvOrFo_GotFocus
      AddHandler GrvOrFo.NTSBeforeRowUpdate, AddressOf GrvOrFo_NTSBeforeRowUpdate
      AddHandler GrvOrFo.NTSFocusedRowChanged, AddressOf GrvOrFo_NTSFocusedRowChanged
      AddHandler GrvOrFo.RowCellStyle, AddressOf GrvOrFo_RowCellStyle

      'CpneInitControls()
      Dim strColoreRigaRossoOF As String = oMenu.GetSettingBusDitt(oCleGsor.strDittaCorrente, "CPNE", "OPZIONI", "OrdCLiFo", "ColoreRigaRossoOF", "255,64,64", " ", "255,64,64")
      strColoreRigaRossoOF1 = Mid(strColoreRigaRossoOF, 1, InStr(strColoreRigaRossoOF, ",") - 1)
      strColoreRigaRossoOF = Mid(strColoreRigaRossoOF, InStr(strColoreRigaRossoOF, ",") + 1)
      strColoreRigaRossoOF2 = Mid(strColoreRigaRossoOF, 1, InStr(strColoreRigaRossoOF, ",") - 1)
      strColoreRigaRossoOF3 = Mid(strColoreRigaRossoOF, InStr(strColoreRigaRossoOF, ",") + 1)

      Dim strColoreRigaVerdeOF As String = oMenu.GetSettingBusDitt(oCleGsor.strDittaCorrente, "CPNE", "OPZIONI", "OrdCLiFo", "ColoreRigaVerdeOF", "138,255,138", " ", "138,255,138")
      strColoreRigaVerdeOF1 = Mid(strColoreRigaVerdeOF, 1, InStr(strColoreRigaVerdeOF, ",") - 1)
      strColoreRigaVerdeOF = Mid(strColoreRigaVerdeOF, InStr(strColoreRigaVerdeOF, ",") + 1)
      strColoreRigaVerdeOF2 = Mid(strColoreRigaVerdeOF, 1, InStr(strColoreRigaVerdeOF, ",") - 1)
      strColoreRigaVerdeOF3 = Mid(strColoreRigaVerdeOF, InStr(strColoreRigaVerdeOF, ",") + 1)

      Dim strColoreRigaGialloOF As String = oMenu.GetSettingBusDitt(oCleGsor.strDittaCorrente, "CPNE", "OPZIONI", "OrdCLiFo", "ColoreRigaGialloOF", "255,255,128", " ", "255,255,128")
      strColoreRigaGialloOF1 = Mid(strColoreRigaGialloOF, 1, InStr(strColoreRigaGialloOF, ",") - 1)
      strColoreRigaGialloOF = Mid(strColoreRigaGialloOF, InStr(strColoreRigaGialloOF, ",") + 1)
      strColoreRigaGialloOF2 = Mid(strColoreRigaGialloOF, 1, InStr(strColoreRigaGialloOF, ",") - 1)
      strColoreRigaGialloOF3 = Mid(strColoreRigaGialloOF, InStr(strColoreRigaGialloOF, ",") + 1)

      strCampoGrigliaDaColorareOF = oMenu.GetSettingBusDitt(oCleGsor.strDittaCorrente, "CPNE", "OPZIONI", "OrdCLiFo", "CampoGrigliaDaColorareOF", "an_descr1", " ", "an_descr1")

      'ec_hhrifmatr = CType(NTSFindControlByName(Me, "ec_hhrifmatr"), NTSGridColumn)
      hh_rifmatr = CType(grvRighe.Columns("hh_rifmatr"), NTSGridColumn)
      hh_rifmatr.NTSForzaVisZoom = True
      hh_rifmatr.NTSSetParamZoom("FRMZOOMTR")

    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub

  Public Overrides Sub FRMORGSOR_Load(ByVal sender As Object, ByVal e As System.EventArgs)
    Call MyBase.FRMORGSOR_Load(sender, e)
    Try
      'Inserirsco il bottone per la creazione delle righe scadenza

      Dim tlbhhCreaRigheScad As New NTSinformatica.NTSBarButtonItem
      tlbhhCreaRigheScad.Caption = "Crea Righe Scadenza"
      tlbhhCreaRigheScad.Name = "hhCreaRigheScad"
      tlbhhCreaRigheScad.GlyphPath = (oApp.ChildImageDir & "\hhCreaRigheScad.png")
      tlbhhCreaRigheScad.Visible = True
      tlbhhCreaRigheScad.Id = NtsBarManager1.GetNewItemId
      NtsBarManager1.Items.Add(tlbhhCreaRigheScad)
      NtsBarManager1.Bars(0).ItemLinks.Add(tlbhhCreaRigheScad)
      ' associo ad oggetto
      cmdhhCreaRigheScad = CType(NTSFindControlByName(Me, "hhCreaRigheScad"), NTSBarButtonItem)
      cmdhhCreaRigheScad.Enabled = False

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try

    'CType(oCleGsor, CLFORGSOR).CPNEInizializzaPers()
  End Sub

  Private Sub cmdhhCreaRigheScad_click(sender As Object, e As EventArgs) Handles cmdhhCreaRigheScad.ItemClick
    Try
      Dim dtrEC As System.Data.DataRow = grvRighe.NTSGetCurrentDataRow
      ' NON C'è ARTICOLO E QUINDI ESCO
      If dtrEC!EC_CODART.ToString <= " " Then
        Exit Sub
      End If
      Dim oPar As New CLE__CLDP
      If NTSCDec(dtrEC!ec_hhvalcontratto) > 0 Then
        oPar.dPar1 = NTSCDec(dtrEC!ec_hhvalcontratto)
      Else
        oPar.dPar1 = NTSCDec(dtrEC!ec_valoremm)
      End If

      oMenu.RunChild("NTSInformatica", "FRMHH0045", "", DittaCorrente, , "BNHH0045", oPar, , True, True)

      If oPar.strOut = "#ok#" Then
        ' L'utente ha premuto ok e sono state indicate le info necessarie per la creazione delle righe
        Dim intMaxRiga As Integer
        Dim intRiga As Integer = 0
        Dim intColonna As Integer = 0
        Dim dImportoRata As Double = 0
        Dim dtDataScad As DateTime
        Dim intConta As Integer = 0
        Dim dGiorni As Double = 0


        ' trovo il numero di riga (+ 10 a giro) e il numero massimo di riga raggiunto
        intMaxRiga = dsGsor.Tables("CORPO").Rows.Count - 1
        intRiga = NTSCInt(dsGsor.Tables("CORPO").Rows(intMaxRiga)!ec_riga.ToString)
        ' metto i valori impostati da utente su scadenza contratto e importo contratto
        dtrEC!ec_datcons = NTSCDate(oPar.strPar4)
        dtrEC!ec_hhvalcontratto = NTSCDec(oPar.strPar1)
        dtrEC!ec_flevas = "S"
        ' mi calcolo i valori e le date
        dtDataScad = NTSCDate(oPar.strPar3)
        dImportoRata = NTSCInt((NTSCDec(oPar.strPar1) / NTSCDec(oPar.strPar2) * 100).ToString) / 100
        Select Case oPar.strPar5.ToUpper
          Case "MENSILE"
            dGiorni = 25
          Case "BIMESTRALE"
            dGiorni = 55
          Case "TRIMESTRALE"
            dGiorni = 85
          Case "QUADRIMESTRALE"
            dGiorni = 115
          Case "SEMESTRALE"
            dGiorni = 175
          Case "ANNUALE"
            dGiorni = 360
        End Select

        ' comincio a ciclare le righe
        For intConta = 1 To NTSCInt(oPar.strPar2)
          intRiga += 10
          intMaxRiga += 1

          '    If dsGsor.Tables("CORPO").NewRow Then
          If MyBase.oCleGsor.AggiungiRigaCorpo(False, dtrEC!ec_codart.ToString, 0, intRiga) = True Then
            dsGsor.Tables("CORPO").Rows(intMaxRiga)!ec_descr = dtrEC!ec_descr.ToString
            dsGsor.Tables("CORPO").Rows(intMaxRiga)!ec_colli = NTSCInt(dtrEC!ec_colli.ToString)
            dsGsor.Tables("CORPO").Rows(intMaxRiga)!ec_coleva = 0
            dsGsor.Tables("CORPO").Rows(intMaxRiga)!ec_quant = NTSCInt(dtrEC!ec_quant.ToString)
            dsGsor.Tables("CORPO").Rows(intMaxRiga)!ec_quaeva = 0
            dsGsor.Tables("CORPO").Rows(intMaxRiga)!ec_flevas = "C"
            If intConta = NTSCInt(oPar.strPar2) Then
              dImportoRata = NTSCDec(oPar.strPar1) - (dImportoRata * (NTSCInt(oPar.strPar2) - 1))
            End If
            dsGsor.Tables("CORPO").Rows(intMaxRiga)!ec_prezzo = dImportoRata / NTSCInt(dtrEC!ec_quant.ToString)
            dsGsor.Tables("CORPO").Rows(intMaxRiga)!ec_note = dtrEC!ec_note.ToString

            dsGsor.Tables("CORPO").Rows(intMaxRiga)!ec_controp = dtrEC!ec_controp.ToString
            dsGsor.Tables("CORPO").Rows(intMaxRiga)!ec_codiva = dtrEC!ec_codiva.ToString

            dsGsor.Tables("CORPO").Rows(intMaxRiga)!ec_datcons = dtDataScad
            dtDataScad = DateAdd(DateInterval.Day, dGiorni, dtDataScad)
            dtDataScad = NTSCDate(FineMese(dtDataScad.ToString))
          End If
        Next
      End If

    Catch ex As Exception

    End Try
  End Sub

  Public Overrides Sub grvSeor_KeyDown(sender As Object, e As KeyEventArgs)
    MyBase.grvSeor_KeyDown(sender, e)
    If e.KeyCode = 1 Then
      MsgBox("")
    End If
  End Sub
  Public Overrides Sub tlbApri_ItemClick(sender As Object, e As ItemClickEventArgs)
    MyBase.tlbApri_ItemClick(sender, e)
    If pnTestataTop.Visible Then
      If oMenu.GetSettingBusDitt(DittaCorrente, "CPNE", "OPZIONI", "OrdCLiFo", "GestioneCorrispIvaEsclusa", "N", " ", "N") = "S" Then
        ec_prezzo.Enabled = True
      End If
      If dsGsor.Tables("TESTA").Rows.Count > 0 Then
        If dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString <> "R" Then
          CType(NTSFindControlByName(Me, "TLBcpneRiepilogoDatiTestata"), NTSBarMenuItem).Visible = False
          PnCPNEOrfo.Visible = False
        Else
          CType(NTSFindControlByName(Me, "TLBcpneRiepilogoDatiTestata"), NTSBarMenuItem).Visible = True
          PnCPNEOrfo.Visible = True
        End If

        'If dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString = "O" Then

        If dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString = oCleGsor.oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "TiporkOf", "O", " ", "O") Then

          If CInt(dsGsor.Tables("TESTA").Rows(0)!et_commeca) <> 0 Then
            If oMenu.GetSettingBusDitt(oCleGsor.strDittaCorrente, "CPNE", "OPZIONI", "OrdCLiFo", "DisabilitaModificaOFConCommessa", "S", " ", "S") = "S" Then
              tlbSalva.Enabled = False
              tlbCancella.Enabled = False
            Else
              tlbSalva.Enabled = True
              tlbCancella.Enabled = True
            End If
          Else
            tlbSalva.Enabled = True
            tlbCancella.Enabled = True
          End If

        End If

        CType(oCleGsor, CLFORGSOR).CPNEInizializzaPers(CInt(dsGsor.Tables("TESTA").Rows(0)!et_commeca))
        Dchh.DataSource = dsGsor.Tables("CPNE_OF")
        NTSFormAddDataBinding(Dchh, Me)
        grOrFo.DataSource = Dchh
        TestEsistenze()
      End If
    End If
  End Sub
  Public Overrides Sub tlbNuovo_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
    MyBase.tlbNuovo_ItemClick(sender, e)
    'giorgio'If pnTestataClforn.Visible Then
    If pnTestataTop.Visible Then
      If dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString <> "R" Then
        CType(NTSFindControlByName(Me, "TLBcpneRiepilogoDatiTestata"), NTSBarMenuItem).Visible = False
        PnCPNEOrfo.Visible = False
      Else
        CType(NTSFindControlByName(Me, "TLBcpneRiepilogoDatiTestata"), NTSBarMenuItem).Visible = True
        PnCPNEOrfo.Visible = True
      End If
      CType(oCleGsor, CLFORGSOR).CPNEInizializzaPers(CInt(dsGsor.Tables("TESTA").Rows(0)!et_commeca))
      Dchh.DataSource = dsGsor.Tables("CPNE_OF")
      NTSFormAddDataBinding(Dchh, Me)
      grOrFo.DataSource = Dchh
      If dsGsor.Tables("TESTA").Rows(0)!ET_TIPORK.ToString = "R" Then
        edEt_commeca.Focus()
        tlbGenNumComm_ItemClick(Nothing, Nothing)
        edet_datdoc.Focus()
      End If
      TestEsistenze()
    End If
  End Sub
  Public Overrides Function ApriDocRicerca() As Boolean

    ApriDocRicerca = MyBase.ApriDocRicerca()
    If ApriDocRicerca Then
      If oMenu.GetSettingBusDitt(DittaCorrente, "CPNE", "OPZIONI", "OrdCLiFo", "GestioneCorrispIvaEsclusa", "N", " ", "N") = "S" Then
        ec_prezzo.Enabled = True
      End If
      If dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString <> "R" Then
        CType(NTSFindControlByName(Me, "TLBcpneRiepilogoDatiTestata"), NTSBarMenuItem).Visible = False
        PnCPNEOrfo.Visible = False
      Else
        CType(NTSFindControlByName(Me, "TLBcpneRiepilogoDatiTestata"), NTSBarMenuItem).Visible = True
        PnCPNEOrfo.Visible = True
      End If

      'If dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString = "O" Then
      If dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString = oCleGsor.oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "TiporkOf", "O", " ", "O") Then

        If CInt(dsGsor.Tables("TESTA").Rows(0)!et_commeca) <> 0 Then
          If oMenu.GetSettingBusDitt(oCleGsor.strDittaCorrente, "CPNE", "OPZIONI", "OrdCLiFo", "DisabilitaModificaOFConCommessa", "S", " ", "S") = "S" Then
            tlbSalva.Enabled = False
            tlbCancella.Enabled = False
          Else
            tlbSalva.Enabled = True
            tlbCancella.Enabled = True
          End If
        Else
          tlbSalva.Enabled = True
          tlbCancella.Enabled = True
        End If

      End If
      CType(oCleGsor, CLFORGSOR).CPNEInizializzaPers(CInt(dsGsor.Tables("TESTA").Rows(0)!et_commeca))
      Dchh.DataSource = dsGsor.Tables("CPNE_OF")
      NTSFormAddDataBinding(Dchh, Me)
      grOrFo.DataSource = Dchh
      TestEsistenze()
    End If
  End Function
  'Public Overrides Sub tlbApri_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
  '  MyBase.tlbApri_ItemClick(sender, e)
  '  If oMenu.GetSettingBusDitt(DittaCorrente, "CPNE", "OPZIONI", "OrdCLiFo", "GestioneCorrispIvaEsclusa", "N", " ", "N") = "S" Then
  '    ec_prezzo.Enabled = True
  '  End If
  '  'giorgio'If pnTestataClforn.Visible Then
  '  If pnTestataTop.Visible Then
  '    If dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString <> "R" Then
  '      CType(NTSFindControlByName(Me, "TLBcpneRiepilogoDatiTestata"), NTSBarMenuItem).Visible = False
  '      PnCPNEOrfo.Visible = False
  '    Else
  '      CType(NTSFindControlByName(Me, "TLBcpneRiepilogoDatiTestata"), NTSBarMenuItem).Visible = True
  '      PnCPNEOrfo.Visible = True
  '    End If

  '    If dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString = "O" Then

  '      If CInt(dsGsor.Tables("TESTA").Rows(0)!et_commeca) <> 0 Then
  '        If oMenu.GetSettingBusDitt(oCleGsor.strDittaCorrente, "CPNE", "OPZIONI", "OrdCLiFo", "DisabilitaModificaOFConCommessa", "S", " ", "S") = "S" Then
  '          tlbSalva.Enabled = False
  '          tlbCancella.Enabled = False
  '        Else
  '          tlbSalva.Enabled = True
  '          tlbCancella.Enabled = True
  '        End If
  '      Else
  '        tlbSalva.Enabled = True
  '        tlbCancella.Enabled = True
  '      End If

  '    End If
  '    CType(oCleGsor, CLFORGSOR).CPNEInizializzaPers(CInt(dsGsor.Tables("TESTA").Rows(0)!et_commeca))
  '    Dchh.DataSource = dsGsor.Tables("CPNE_OF_VIS")
  '    NTSFormAddDataBinding(Dchh, Me)
  '    grOrFo.DataSource = Dchh
  '    TestEsistenze()
  '  End If
  'End Sub

  Public Overrides Sub grvRighe_NTSFocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)

    If grvRighe.GetFocusedRowCellValue(ec_riga).ToString = "" Then Return
    If grvRighe.NTSGetCurrentDataRow!ec_hhvalcontratto.ToString = "" Then
      cmdhhCreaRigheScad.Enabled = False
    Else
      cmdhhCreaRigheScad.Enabled = True
    End If

    MyBase.grvRighe_NTSFocusedRowChanged(sender, e)
    Try
      CType(oCleGsor, CLFORGSOR).DrMovord = grvRighe.NTSGetCurrentDataRow
      If IsNothing(grvRighe.NTSGetCurrentDataRow) Then
        mo_riga.FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("mo_subcommeca = -1")
      Else
        If dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString = "R" Then
          GrvOrFo.ClearColumnsFilter()
          mo_riga.FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("(mo_subcommeca = " & CStrSQL(grvRighe.NTSGetCurrentDataRow!ec_subcommeca) & " or codditt = '.,')")
          mo_riga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
          mo_riga.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
          TestEsistenze()
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub

  Private Sub GrvOrFo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    grvRighe.FocusedColumn = ec_descr
  End Sub
  Public Overrides Sub tsGsor_SelectedPageChanged(ByVal sender As Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs)
    MyBase.tsGsor_SelectedPageChanged(sender, e)
    grOrFo.DataSource = Dchh
    Try
      If tsGsor.SelectedTabPageIndex <> 1 Then
        cmdhhCreaRigheScad.Enabled = False
      Else
        'cmdhhCreaRigheScad.Enabled = True
        If grvRighe.NTSGetCurrentDataRow Is Nothing Then Return
        If grvRighe.NTSGetCurrentDataRow!ec_hhvalcontratto.ToString = "" Then
          cmdhhCreaRigheScad.Enabled = False
        Else
          cmdhhCreaRigheScad.Enabled = True
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub
  Private Sub GrvOrFo_NTSBeforeRowUpdate(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs)
    If Not IsNothing(GrvOrFo.NTSGetCurrentDataRow) Then
      If GrvOrFo.NTSGetCurrentDataRow!xx_cpneofconto.ToString = "0" Or GrvOrFo.NTSGetCurrentDataRow!xx_cpneofconto.ToString = "" Then
        oApp.MsgBoxInfo("Prima inserire il codice conto")
        e.Allow = False
        Exit Sub
      End If
      If GrvOrFo.NTSGetCurrentDataRow!mo_codart.ToString = "" Then
        oApp.MsgBoxInfo("Prima inserire il codice articolo")
        e.Allow = False
        Exit Sub
      End If
    End If
    CType(oCleGsor, CLFORGSOR).DTCPNE_OF.AcceptChanges()
  End Sub
  Public Overrides Sub tlbRecordCancella_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
    Dim dtrDeleted As DataRow = Nothing
    Dim nPos As Integer = 0
    Try
      If GrvOrFo.Focused = False Then
        If Not IsNothing(grvRighe.NTSGetCurrentDataRow) Then
          Dim drs As DataRow() = CType(oCleGsor, CLFORGSOR).DTCPNE_OF.Select("mo_subcommeca = " & grvRighe.NTSGetCurrentDataRow!ec_subcommeca.ToString & " and mo_flevas = 'S'")
          If drs.Length > 0 Then
            'If oApp.MsgBoxInfoYesNo_DefNo("Ci sono " & intRigheEvase & " righe d'ordine fornitore evase. Continuare?") = System.Windows.Forms.DialogResult.No Then
            If oApp.MsgBoxInfoYesNo_DefNo("C'è almeno 1 riga d'ordine fornitore evasa. Si vuole continuare?") = System.Windows.Forms.DialogResult.No Then
              Return
            End If
          End If
        End If
      Else
        ValidaLastControl()
        If Not IsNothing(GrvOrFo.NTSGetCurrentDataRow) Then
          If CType(oCleGsor, CLFORGSOR).DTCPNE_OF.Rows(nPos)!mo_flevas.ToString = "S" Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129048480855563965, "Attenzione!!! la riga è già evasa. Non è possibile cancellarla."))
          Else
            oApp.MsgBoxErr(oApp.Tr(Me, 129048480855563965, "Attenzione!!! Non è possibile cancellare la riga. Cancellare l'ordine fornitore corrispondente !"))
            'GrvOrFo.NTSGetCurrentDataRow.Delete()
            Try
              GrvOrFo.NTSGetCurrentDataRow.AcceptChanges()
            Catch ex As Exception

            End Try

          End If
        End If
        Return
      End If
      MyBase.tlbRecordCancella_ItemClick(sender, e)
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try


  End Sub
  Public Overrides Sub tlbRecordRipristina_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
    Try

      If GrvOrFo.Focused Then
        If Not IsNothing(GrvOrFo.NTSGetCurrentDataRow) Then
          GrvOrFo.NTSGetCurrentDataRow.RejectChanges()
        End If
      Else
        MyBase.tlbRecordRipristina_ItemClick(sender, e)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try

  End Sub

  Public Overrides Function RipristinaDocumento() As Boolean
    cmdhhCreaRigheScad.Enabled = False
    Return MyBase.RipristinaDocumento()
  End Function

  Private Sub GrvOrFo_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)

    'Dchh.DataSource = dsGsor.Tables("CPNE_OF")
    'NTSFormAddDataBinding(Dchh, Me)
    'grOrFo.DataSource = Dchh
    TestEsistenze()

    'If GrvOrFo.NTSGetCurrentDataRow Then
    'If Not IsNothing(CType(oCleGsor, CLFORGSOR).DTCPNE_OF) Then
    '    MsgBox("Attenzione si può caricare solo 1 riga !")
    '    Return
    '  End If
    'End If
  End Sub
  Private Sub TestEsistenze()
    If dsGsor.Tables("TESTA").Rows.Count > 0 Then
      If dsGsor.Tables("TESTA").Rows(0)!et_tipork.ToString = "R" Then
        If IsNothing(CType(oCleGsor, CLFORGSOR).DTCPNE_OF) Then
          lbXx_conto.BackColor = Color.Transparent
        Else
          If oCleGsor.dttEC.Select("ec_flevas = 'C'").Length = 0 Then
            lbXx_conto.BackColor = Color.Transparent
          Else
            If CType(oCleGsor, CLFORGSOR).DTCPNE_OF.Select("mo_flevas = 'C'").Length = 0 Then
              If CType(oCleGsor, CLFORGSOR).DTCPNE_OF.Rows.Count = 0 Then
                lbXx_conto.BackColor = Color.Transparent
              Else
                'lbXx_conto.BackColor = Color.Green
                lbXx_conto.BackColor = Color.FromArgb(CInt(strColoreRigaVerdeOF1), CInt(strColoreRigaVerdeOF2), CInt(strColoreRigaVerdeOF3))
              End If
            ElseIf CType(oCleGsor, CLFORGSOR).DTCPNE_OF.Select("mo_quaeva <> 0").Length <> 0 Or CType(oCleGsor, CLFORGSOR).DTCPNE_OF.Select("mo_flevas <> 'C'").Length <> 0 Then
              'lbXx_conto.BackColor = Color.Yellow
              lbXx_conto.BackColor = Color.FromArgb(CInt(strColoreRigaGialloOF1), CInt(strColoreRigaGialloOF2), CInt(strColoreRigaGialloOF3))
            Else
              'lbXx_conto.BackColor = Color.Red
              lbXx_conto.BackColor = Color.FromArgb(CInt(strColoreRigaRossoOF1), CInt(strColoreRigaRossoOF2), CInt(strColoreRigaRossoOF3))
            End If
          End If
        End If
      Else
        lbXx_conto.BackColor = Color.Transparent
      End If
    End If
  End Sub

  Private Sub Tmr1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tmr1.Tick
    Tmr1.Enabled = False
    Tmr1.Interval = 1000
    CType(oCleGsor, CLFORGSOR).DTCPNE_OF.AcceptChanges()
  End Sub

  Public Overrides Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
    If GrvOrFo.Focused Then
      If GrvOrFo.FocusedColumn.Name = "mo_prezzo" Then
        '------------------------------------
        'zoom listini
        Dim oPar As New CLE__CLDP
        Dim dtrRow As DataRow
        oPar.Ditta = DittaCorrente
        oPar.strNomProg = "BNORGSOR"
        oPar.strPar1 = GrvOrFo.NTSGetCurrentDataRow!MO_codart.ToString
        oPar.strPar2 = edet_datdoc.Text
        oPar.dPar1 = 0 'NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_fase)
        oPar.dPar3 = NTSCInt(GrvOrFo.NTSGetCurrentDataRow!xx_cpneofconto)
        oPar.dPar4 = 0        'ritorna il prezzo
        oPar.dPar5 = 0        'ritorna la valuta
        oPar.ctlPar1 = Nothing

        oMenu.RunChild("NTSInformatica", "FRMMGLIST", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)

        '----------------------
        'Esce se annullo la finestra
        If oPar.dPar4 = 0 Then Return

        '----------------------
        'Riporta i prezzi praticati in precedenza
        If NTSCInt(edEt_valuta.Text) <> NTSCInt(oPar.dPar5) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129048480855563965, "Non è possibile riportare il prezzo in quanto il prezzo selezionato riporta una valuta (|" & oPar.dPar5.ToString & "|) diversa da quella del documento corrente (|" & edEt_valuta.Text & "|)."))
          Return
        Else
          'Modifica anche il flag di prezzo netto e le promozioni
          If Not oPar.ctlPar1 Is Nothing Then
            dtrRow = CType(oPar.ctlPar1, DataRow)
            'grvRighe.NTSGetCurrentDataRow!ec_codtpro = dtrRow!lc_codtpro

            'grvRighe.NTSGetCurrentDataRow!ec_flprznet = dtrRow!lc_netto
          End If

          If NTSCInt(edEt_valuta.Text) <> 0 Then
            'grvRighe.NTSGetCurrentDataRow!ec_prezvalc = oPar.dPar4
          Else
            'If nTipoCol = 0 Then
            GrvOrFo.NTSGetCurrentDataRow!mo_prezzo = oPar.dPar4
            'Else
            '  grvRighe.NTSGetCurrentDataRow!ec_preziva = oPar.dPar4
            'End If
          End If

          GrvOrFo.NTSMoveNextColunn()
        End If
        Return
      End If
    End If

    MyBase.tlbZoom_ItemClick(sender, e)

    '========================== RICCARDO 13 06 2019 =============================

    If grvRighe.FocusedColumn.Name = "hh_rifmatr" Then
      If CInt(edEt_conto.Text) = 0 Then
        oApp.MsgBoxInfo("Attenzione: il conto cliente è uguale a 0 !")
      Else
        Dim oCP As New CLE__CLDP
        oCP.dPar1 = CInt(edEt_conto.Text)
        'oCP.strBanc1 = grvRighe.NTSGetCurrentDataRow!ec_codart.ToString

        oMenu.RunChild("NTSInformatica", "FRMHHMMTR", "Matricole", DittaCorrente, "", "BNHHMMTR", oCP, "", True, True)
        If oCP.strBanc1 <> "" Then
          grvRighe.NTSGetCurrentDataRow!hh_rifmatr = oCP.strBanc1
        End If
      End If
    End If
    grvRighe.NTSMoveNextColunn()
    '============================================

  End Sub
  Public Overrides Sub tlbNoteRiga_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
    Dim frmMsgb As FRM__MSGB = Nothing
    Try
      If GrvOrFo.Focused Then
        frmMsgb = CType(NTSNewFormModal("FRM__MSGB"), FRM__MSGB)

        If GrvOrFo.Columns("mo_note").Visible = False Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128557689742844788, "La colonna 'NOTE' non è visibile; impossibile visualizzarne il contenuto"))
          Return
        End If
        If GrvOrFo.NTSGetCurrentDataRow Is Nothing Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128557689491840788, "Posizionarsi su una riga contenente un articolo"))
          Return
        End If
        Me.ValidaLastControl()
        frmMsgb.Init(oMenu, Nothing, DittaCorrente)
        frmMsgb.edMess.Text = GrvOrFo.NTSGetCurrentDataRow!mo_note.ToString
        frmMsgb.Text = "NOTE RIGA"
        frmMsgb.lConto = CInt(GrvOrFo.NTSGetCurrentDataRow!xx_cpneofconto)
        frmMsgb.ShowDialog()
        If frmMsgb.bOk Then GrvOrFo.SetFocusedRowCellValue(mo_note, frmMsgb.edMess.Text)

      Else
        MyBase.tlbNoteRiga_ItemClick(sender, e)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------	
    Finally
      If Not frmMsgb Is Nothing Then frmMsgb.Dispose()
      frmMsgb = Nothing
    End Try
  End Sub
  Public Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    If e.TipoEvento = "" Then
      If InStr(e.Message.ToLower, "ripristinare") <> 0 Then
        Dim q As New NTSEventArgs("", e.Message.ToLower & vbCrLf & "Attuale Salvataggio " & dsGsor.Tables("testa").Rows(0)!et_ultagg.ToString)

        MyBase.GestisciEventiEntity(sender, q)
      Else
        
          MyBase.GestisciEventiEntity(sender, e)

      End If
    Else
    If Mid(e.TipoEvento, 1, 4) = "CPNE" Then
      Select Case e.TipoEvento
          Case "CPNE.Stampa"
            Cursor.Current = Cursors.WaitCursor
            CPNEImpostaStampa(0, e.Message)
            Cursor.Current = Cursors.Default
          Case "CPNE.BloccaPrzForn"
            CPNEBloccaPrzForn(CBool(e.Message))
        End Select
    Else
        MyBase.GestisciEventiEntity(sender, e)
      End If
    End If
  End Sub
  Private Sub CPNEBloccaPrzForn(bSiNo As Boolean)
    If bSiNo Then
      CType(NTSFindControlByName(Me, "ckbloccaprzforn"), NTSCheckBox).Checked = True
      CType(oCleGsor, CLFORGSOR).bBloccaPrezzoFornitore = True
    Else
      CType(NTSFindControlByName(Me, "ckbloccaprzforn"), NTSCheckBox).Checked = False
      CType(oCleGsor, CLFORGSOR).bBloccaPrezzoFornitore = False
    End If
  End Sub

  Public Sub CPNEImpostaStampa(ByVal nDestin As Integer, strTipo As String)
    If strTipo = "IC" Then
      For xx = 1 To UBound(CType(oCleGsor, CLFORGSOR).strSerieOF)
        Dim strFormulaRecordSelection As String = "{testord.td_tipork} = 'O'"
        strFormulaRecordSelection += " and {testord.td_anno} = " & CType(oCleGsor, CLFORGSOR).intAnnoOF(xx)
        strFormulaRecordSelection += " and {testord.td_serie} = '" & CType(oCleGsor, CLFORGSOR).strSerieOF(xx) & "'"
        strFormulaRecordSelection += " and {testord.td_numord} = " & CType(oCleGsor, CLFORGSOR).intNumordOF(xx)
        CPNEEseguiStampa(strFormulaRecordSelection, "BSORGSOR", "REPORTS1", " ", nDestin, "BSORGSOR.rpt", "Stampa Ordine Fornitore")
      Next
    Else
      Dim strFormulaRecordSelection As String = "{testmag.tm_tipork} = '" & CType(oCleGsor, CLFORGSOR).strTipoDoc & "'"
      strFormulaRecordSelection += " and {testmag.tm_anno} = " & CType(oCleGsor, CLFORGSOR).nAnnoDoc
      strFormulaRecordSelection += " and {testmag.tm_serie} = '" & CType(oCleGsor, CLFORGSOR).strSerieDoc & "'"
      strFormulaRecordSelection += " and {testmag.tm_numdoc} = " & CType(oCleGsor, CLFORGSOR).lNumTmpDoc
      If ckEt_scorpo.Checked = True Then
        CPNEEseguiStampa(strFormulaRecordSelection, "BSVEBOLL", "REPORTS2", " ", nDestin, "BSVEFATC.rpt", "Stampa Fattura")
      Else
        CPNEEseguiStampa(strFormulaRecordSelection, "BSVEBOLL", "REPORTS1", " ", nDestin, "BSVEFATI.rpt", "Stampa Fattura")
      End If

    End If

  End Sub
  Public Sub CPNEEseguiStampa(ByVal StrWhere As String, ByVal strKey1 As String, ByVal strKey2 As String, ByVal strTipoDoc As String, ByVal nDestin As Integer, ByVal strNomeRpt As String, ByVal strCaptionWin As String)

    Dim nPjob As Object

    nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, strKey1, strKey2, " ", 1, nDestin, strNomeRpt, False, strCaptionWin, False)
    If Not (nPjob Is Nothing) Then
      'lancio tutti gli eventuali reports (gestisce già il multireport)												
      For i = 1 To UBound(CType(nPjob, Array), 2)
        If StrWhere <> "" Then
          oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), StrWhere))
        End If
        oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next
    End If
  End Sub
  'Public Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
  '  If e.TipoEvento = "CPNE.CAMBIACOLORE" Then
  '    If e.Message = "S" Then

  '    ElseIf e.Message = "N" Then

  '    Else

  '    End If
  '  End If
  '  MyBase.GestisciEventiEntity(sender, e)
  'End Sub
  'Public Overrides Sub tlbSalvaCondPart_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
  '  If GrvOrFo.Focused = False Then
  '    MyBase.tlbSalvaCondPart_ItemClick(sender, e)
  '  Else
  '    Dim oPar As New CLE__CLDP
  '    Dim dttCopa As New DataTable
  '    Dim nCodlavo As Integer = 0
  '    Dim dtrT() As DataRow = Nothing
  '    Dim dPrezzo As Decimal = 0
  '    Try
  '      '------------------------------
  '      'controlli preliminari
  '      Me.ValidaLastControl()

  '      '-------------------------------
  '      'preparo i parametri da passare a vecopa
  '      dttCopa.Columns.Add("conto", GetType(Integer))
  '      dttCopa.Columns.Add("valuta", GetType(Integer))
  '      dttCopa.Columns.Add("agente", GetType(Integer))
  '      dttCopa.Columns.Add("agente2", GetType(Integer))
  '      dttCopa.Columns.Add("datdoc", GetType(String))
  '      dttCopa.Columns.Add("codart", GetType(String))
  '      dttCopa.Columns.Add("fase", GetType(Integer))
  '      dttCopa.Columns.Add("quant", GetType(Decimal))
  '      dttCopa.Columns.Add("prezzo", GetType(Decimal))
  '      dttCopa.Columns.Add("prezvalc", GetType(Decimal))
  '      dttCopa.Columns.Add("prznet", GetType(String))
  '      dttCopa.Columns.Add("perqta", GetType(Integer))
  '      dttCopa.Columns.Add("ump", GetType(String))
  '      dttCopa.Columns.Add("scont1", GetType(Decimal))
  '      dttCopa.Columns.Add("scont2", GetType(Decimal))
  '      dttCopa.Columns.Add("scont3", GetType(Decimal))
  '      dttCopa.Columns.Add("scont4", GetType(Decimal))
  '      dttCopa.Columns.Add("scont5", GetType(Decimal))
  '      dttCopa.Columns.Add("scont6", GetType(Decimal))
  '      dttCopa.Columns.Add("provv", GetType(Decimal))
  '      dttCopa.Columns.Add("provv2", GetType(Decimal))
  '      dttCopa.Columns.Add("codlavo", GetType(Integer))
  '      dttCopa.Columns.Add("codtpro", GetType(Integer))

  '      dPrezzo = NTSCDec(GrvOrFo.NTSGetCurrentDataRow!mo_prezzo)

  '      dttCopa.Rows.Add(dttCopa.NewRow)
  '      With dttCopa.Rows(0)
  '        !conto = NTSCInt(edEt_conto.Text)
  '        !valuta = NTSCInt(edEt_valuta.Text)
  '        !agente = NTSCInt(edEt_codagen.Text)
  '        !agente2 = NTSCInt(edEt_codagen2.Text)
  '        !datdoc = NTSCDate(edet_datdoc.Text).ToShortDateString
  '        !codart = NTSCStr(GrvOrFo.NTSGetCurrentDataRow!mo_codart)
  '        !fase = 0
  '        !quant = NTSCDec(GrvOrFo.NTSGetCurrentDataRow!mo_quant)
  '        !prezzo = dPrezzo
  '        !prezvalc = 0 'NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_prezvalc)
  '        !prznet = NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_flprznet)
  '        !perqta = 1 'NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_perqta)
  '        !ump = NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_ump)
  '        !scont1 = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_scont1)
  '        !scont2 = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_scont2)
  '        !scont3 = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_scont3)
  '        !scont4 = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_scont4)
  '        !scont5 = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_scont5)
  '        !scont6 = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_scont6)
  '        !provv = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_provv)
  '        !provv2 = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_provv2)
  '        !codlavo = nCodlavo
  '        !codtpro = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_codtpro)
  '      End With
  '      dttCopa.AcceptChanges()

  '      oPar.Ditta = DittaCorrente
  '      oPar.strNomProg = "BNORGSOR"
  '      oPar.ctlPar1 = dttCopa
  '      oPar.bPar1 = False             'visualizza la form di FRMVECOPA
  '      oMenu.RunChild("NTSInformatica", "FRMVECOPA", "", DittaCorrente, "", "BNVECOPA", oPar, "", True, True)

  '    Catch ex As Exception
  '      '-------------------------------------------------
  '      CLN__STD.GestErr(ex, Me, "")
  '      '-------------------------------------------------	
  '    End Try
  '  End If
  'End Sub

  Private Sub GrvOrFo_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs)
    If e.Column.FieldName = strCampoGrigliaDaColorareOF Then
      Dim dr As DataRow = GrvOrFo.GetDataRow(e.RowHandle)
      If Not IsNothing(dr) Then
        If Not IsDBNull(dr!mo_quaeva) Then
          If dr!mo_flevas.ToString = "C" And CInt(dr!mo_quaeva) = 0 Then
            'e.Appearance.BackColor = Color.Red
            e.Appearance.BackColor = Color.FromArgb(CInt(strColoreRigaRossoOF1), CInt(strColoreRigaRossoOF2), CInt(strColoreRigaRossoOF3))
          ElseIf dr!mo_flevas.ToString = "C" And CInt(dr!mo_quaeva) <> 0 Then
            e.Appearance.BackColor = Color.FromArgb(CInt(strColoreRigaGialloOF1), CInt(strColoreRigaGialloOF2), CInt(strColoreRigaGialloOF3))
            'e.Appearance.BackColor = Color.Yellow
          Else
            e.Appearance.BackColor = Color.FromArgb(CInt(strColoreRigaVerdeOF1), CInt(strColoreRigaVerdeOF2), CInt(strColoreRigaVerdeOF3))
          End If
          e.Appearance.BackColor2 = e.Appearance.BackColor
        End If
      End If
    End If
    If e.Appearance.ForeColor <> Color.Black Then e.Appearance.ForeColor = Color.Black
    'e.Appearance.BackColor = Color.FromKnownColor(16777215)
  End Sub

End Class
