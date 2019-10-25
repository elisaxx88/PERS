Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO
Imports DevExpress.XtraBars

Public Class FRMHHDORO
#Region "Standard"
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = bsModExtAll
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Private ModuliPtnExt_P As Integer = 0

  Dim IntTipoStampa As Integer = 0 ' 0 = video, 1 = carta

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    If Ditta <> "" Then
      DittaCorrente = Ditta
    Else
      DittaCorrente = oApp.Ditta
    End If
    Me.GctlTipoDoc = ""

    InitializeComponent()
    Me.MinimumSize = Me.Size

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovrÃ  rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNHHDORO", "BEHHDORO", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128271029889882656, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleHh = CType(oTmp, CLEHHDORO)
    oCleHh.AssociaDs(dsHh)
    oCleHh.OMenu = oMenu
    '------------------------------------------------

    AddHandler oCleHh.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleHh.Init(oApp, NTSScript, oMenu.oCleComm, "", False, "", "") = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMHHDORO))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager()
    Me.tlbMain = New NTSInformatica.NTSBar()
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem()
    Me.tlbApri = New NTSInformatica.NTSBarButtonItem()
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem()
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem()
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem()
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem()
    Me.tlbRecordCancella = New NTSInformatica.NTSBarButtonItem()
    Me.tlbRecordRipristina = New NTSInformatica.NTSBarButtonItem()
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem()
    Me.tlbNavDoc = New NTSInformatica.NTSBarMenuItem()
    Me.TlbSelTutto = New NTSInformatica.NTSBarMenuItem()
    Me.tlbDeselTutto = New NTSInformatica.NTSBarMenuItem()
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem()
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem()
    Me.tlbStampaPdf = New NTSInformatica.NTSBarButtonItem()
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem()
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem()
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
    Me.grricerca = New NTSInformatica.NTSGrid()
    Me.grvricerca = New NTSInformatica.NTSGridView()
    Me.xx_sel = New NTSInformatica.NTSGridColumn()
    Me.an_descr1 = New NTSInformatica.NTSGridColumn()
    Me.td_datord = New NTSInformatica.NTSGridColumn()
    Me.mo_datcons = New NTSInformatica.NTSGridColumn()
    Me.mo_codart = New NTSInformatica.NTSGridColumn()
    Me.mo_descr = New NTSInformatica.NTSGridColumn()
    Me.mo_valoremm = New NTSInformatica.NTSGridColumn()
    Me.mo_desint = New NTSInformatica.NTSGridColumn()
    Me.mo_note = New NTSInformatica.NTSGridColumn()
    Me.mo_anno = New NTSInformatica.NTSGridColumn()
    Me.mo_serie = New NTSInformatica.NTSGridColumn()
    Me.mo_numord = New NTSInformatica.NTSGridColumn()
    Me.mo_riga = New NTSInformatica.NTSGridColumn()
    Me.mo_flevas = New NTSInformatica.NTSGridColumn()
    Me.td_riferim = New NTSInformatica.NTSGridColumn()
    Me.NtsGroupBox1 = New NTSInformatica.NTSGroupBox()
    Me.edxx_aconto = New NTSInformatica.NTSTextBoxNum()
    Me.NtsLabel6 = New NTSInformatica.NTSLabel()
    Me.edxx_daconto = New NTSInformatica.NTSTextBoxNum()
    Me.NtsLabel5 = New NTSInformatica.NTSLabel()
    Me.ckxx_soloap = New NTSInformatica.NTSCheckBox()
    Me.NtsLabel3 = New NTSInformatica.NTSLabel()
    Me.CmdRicerca = New NTSInformatica.NTSButton()
    Me.NtsLabel4 = New NTSInformatica.NTSLabel()
    Me.NtsLabel2 = New NTSInformatica.NTSLabel()
    Me.NtsLabel1 = New NTSInformatica.NTSLabel()
    Me.edxx_adtord = New NTSInformatica.NTSTextBoxData()
    Me.edxx_adtcons = New NTSInformatica.NTSTextBoxData()
    Me.edxx_dadtord = New NTSInformatica.NTSTextBoxData()
    Me.edxx_dadtcons = New NTSInformatica.NTSTextBoxData()
    Me.NtsPanel1 = New NTSInformatica.NTSPanel()
    Me.NtsPanel5 = New NTSInformatica.NTSPanel()
    Me.NtsPanel4 = New NTSInformatica.NTSPanel()
    Me.NtsPanel2 = New NTSInformatica.NTSPanel()
    Me.NtsPanel3 = New NTSInformatica.NTSPanel()
    Me.NtsGroupBox2 = New NTSInformatica.NTSGroupBox()
    Me.edxx_dtfatt = New NTSInformatica.NTSTextBoxData()
    Me.CmdElabora = New NTSInformatica.NTSButton()
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grricerca, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvricerca, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox1.SuspendLayout()
    CType(Me.edxx_aconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_daconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckxx_soloap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_adtord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_adtcons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_dadtord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_dadtcons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel1.SuspendLayout()
    CType(Me.NtsPanel5, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsPanel4, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel4.SuspendLayout()
    CType(Me.NtsPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel2.SuspendLayout()
    CType(Me.NtsPanel3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel3.SuspendLayout()
    CType(Me.NtsGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox2.SuspendLayout()
    CType(Me.edxx_dtfatt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'NtsBarManager1
    '
    Me.NtsBarManager1.AllowCustomization = False
    Me.NtsBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.tlbMain})
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlTop)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlBottom)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlLeft)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlRight)
    Me.NtsBarManager1.Form = Me
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbApri, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbRecordCancella, Me.tlbRecordRipristina, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbStampaPdf, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbNavDoc, Me.TlbSelTutto, Me.tlbDeselTutto})
    Me.NtsBarManager1.MaxItemId = 120
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbNuovo, False), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbApri, False), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbSalva, False), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbRipristina, False), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbCancella, False), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbRecordCancella, True), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbRecordRipristina, False), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbStampaPdf, False), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbNuovo
    '
    Me.tlbNuovo.Caption = "Nuovo"
    Me.tlbNuovo.Glyph = CType(resources.GetObject("tlbNuovo.Glyph"), System.Drawing.Image)
    Me.tlbNuovo.GlyphPath = ""
    Me.tlbNuovo.Id = 0
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = False
    '
    'tlbApri
    '
    Me.tlbApri.Caption = "Apri"
    Me.tlbApri.Glyph = CType(resources.GetObject("tlbApri.Glyph"), System.Drawing.Image)
    Me.tlbApri.GlyphPath = ""
    Me.tlbApri.Id = 1
    Me.tlbApri.Name = "tlbApri"
    Me.tlbApri.Visible = False
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.GlyphPath = ""
    Me.tlbSalva.Id = 2
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = False
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.GlyphPath = ""
    Me.tlbRipristina.Id = 4
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = False
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.GlyphPath = ""
    Me.tlbCancella.Id = 3
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = False
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 5
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbRecordCancella
    '
    Me.tlbRecordCancella.Caption = "Cancella riga"
    Me.tlbRecordCancella.Glyph = CType(resources.GetObject("tlbRecordCancella.Glyph"), System.Drawing.Image)
    Me.tlbRecordCancella.GlyphPath = ""
    Me.tlbRecordCancella.Id = 6
    Me.tlbRecordCancella.Name = "tlbRecordCancella"
    Me.tlbRecordCancella.Visible = False
    '
    'tlbRecordRipristina
    '
    Me.tlbRecordRipristina.Caption = "Ripristina riga"
    Me.tlbRecordRipristina.Glyph = CType(resources.GetObject("tlbRecordRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRecordRipristina.GlyphPath = ""
    Me.tlbRecordRipristina.Id = 7
    Me.tlbRecordRipristina.Name = "tlbRecordRipristina"
    Me.tlbRecordRipristina.Visible = False
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNavDoc, True), New DevExpress.XtraBars.LinkPersistInfo(Me.TlbSelTutto), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDeselTutto)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbNavDoc
    '
    Me.tlbNavDoc.Caption = "Navigazione Documentale"
    Me.tlbNavDoc.GlyphPath = ""
    Me.tlbNavDoc.Id = 104
    Me.tlbNavDoc.Name = "tlbNavDoc"
    Me.tlbNavDoc.NTSIsCheckBox = False
    Me.tlbNavDoc.Visible = True
    '
    'TlbSelTutto
    '
    Me.TlbSelTutto.Caption = "Seleziona tutto"
    Me.TlbSelTutto.GlyphPath = ""
    Me.TlbSelTutto.Id = 118
    Me.TlbSelTutto.Name = "TlbSelTutto"
    Me.TlbSelTutto.NTSIsCheckBox = False
    Me.TlbSelTutto.Visible = True
    '
    'tlbDeselTutto
    '
    Me.tlbDeselTutto.Caption = "Deselziona Tutto"
    Me.tlbDeselTutto.GlyphPath = ""
    Me.tlbDeselTutto.Id = 119
    Me.tlbDeselTutto.Name = "tlbDeselTutto"
    Me.tlbDeselTutto.NTSIsCheckBox = False
    Me.tlbDeselTutto.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.GlyphPath = ""
    Me.tlbStampa.Id = 9
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "StampaVideo"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.GlyphPath = ""
    Me.tlbStampaVideo.Id = 10
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbStampaPdf
    '
    Me.tlbStampaPdf.Caption = "StampaPdf"
    Me.tlbStampaPdf.Glyph = CType(resources.GetObject("tlbStampaPdf.Glyph"), System.Drawing.Image)
    Me.tlbStampaPdf.GlyphPath = ""
    Me.tlbStampaPdf.Id = 12
    Me.tlbStampaPdf.Name = "tlbStampaPdf"
    Me.tlbStampaPdf.Visible = False
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 13
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 14
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'barDockControlTop
    '
    Me.barDockControlTop.CausesValidation = False
    Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
    Me.barDockControlTop.Size = New System.Drawing.Size(547, 35)
    '
    'barDockControlBottom
    '
    Me.barDockControlBottom.CausesValidation = False
    Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.barDockControlBottom.Location = New System.Drawing.Point(0, 403)
    Me.barDockControlBottom.Size = New System.Drawing.Size(547, 0)
    '
    'barDockControlLeft
    '
    Me.barDockControlLeft.CausesValidation = False
    Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
    Me.barDockControlLeft.Location = New System.Drawing.Point(0, 35)
    Me.barDockControlLeft.Size = New System.Drawing.Size(0, 368)
    '
    'barDockControlRight
    '
    Me.barDockControlRight.CausesValidation = False
    Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
    Me.barDockControlRight.Location = New System.Drawing.Point(547, 35)
    Me.barDockControlRight.Size = New System.Drawing.Size(0, 368)
    '
    'grricerca
    '
    Me.grricerca.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grricerca.Location = New System.Drawing.Point(0, 0)
    Me.grricerca.MainView = Me.grvricerca
    Me.grricerca.Name = "grricerca"
    Me.grricerca.Size = New System.Drawing.Size(547, 272)
    Me.grricerca.TabIndex = 4
    Me.grricerca.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvricerca})
    '
    'grvricerca
    '
    Me.grvricerca.ActiveFilterEnabled = False
    Me.grvricerca.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_sel, Me.an_descr1, Me.td_datord, Me.mo_datcons, Me.mo_codart, Me.mo_descr, Me.mo_valoremm, Me.mo_desint, Me.mo_note, Me.mo_anno, Me.mo_serie, Me.mo_numord, Me.mo_riga, Me.mo_flevas, Me.td_riferim})
    Me.grvricerca.Enabled = True
    Me.grvricerca.GridControl = Me.grricerca
    Me.grvricerca.MinRowHeight = 14
    Me.grvricerca.Name = "grvricerca"
    Me.grvricerca.NTSPictureChecked = ""
    Me.grvricerca.NTSPictureUnchecked = ""
    Me.grvricerca.OptionsCustomization.AllowRowSizing = True
    Me.grvricerca.OptionsFilter.AllowFilterEditor = False
    Me.grvricerca.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvricerca.OptionsNavigation.UseTabKey = False
    Me.grvricerca.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvricerca.OptionsView.ColumnAutoWidth = False
    Me.grvricerca.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvricerca.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvricerca.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvricerca.OptionsView.ShowGroupPanel = False
    Me.grvricerca.RowHeight = 14
    '
    'xx_sel
    '
    Me.xx_sel.AppearanceCell.Options.UseBackColor = True
    Me.xx_sel.AppearanceCell.Options.UseTextOptions = True
    Me.xx_sel.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_sel.Caption = "Seleziona"
    Me.xx_sel.Enabled = True
    Me.xx_sel.FieldName = "xx_sel"
    Me.xx_sel.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_sel.Name = "xx_sel"
    Me.xx_sel.Visible = True
    Me.xx_sel.VisibleIndex = 0
    '
    'an_descr1
    '
    Me.an_descr1.AppearanceCell.Options.UseBackColor = True
    Me.an_descr1.AppearanceCell.Options.UseTextOptions = True
    Me.an_descr1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.an_descr1.Caption = "Cliente"
    Me.an_descr1.Enabled = True
    Me.an_descr1.FieldName = "an_descr1"
    Me.an_descr1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.an_descr1.Name = "an_descr1"
    Me.an_descr1.Visible = True
    Me.an_descr1.VisibleIndex = 1
    '
    'td_datord
    '
    Me.td_datord.AppearanceCell.Options.UseBackColor = True
    Me.td_datord.AppearanceCell.Options.UseTextOptions = True
    Me.td_datord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_datord.Caption = "Data Ord"
    Me.td_datord.Enabled = True
    Me.td_datord.FieldName = "td_datord"
    Me.td_datord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_datord.Name = "td_datord"
    Me.td_datord.Visible = True
    Me.td_datord.VisibleIndex = 2
    '
    'mo_datcons
    '
    Me.mo_datcons.AppearanceCell.Options.UseBackColor = True
    Me.mo_datcons.AppearanceCell.Options.UseTextOptions = True
    Me.mo_datcons.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_datcons.Caption = "Data consegna"
    Me.mo_datcons.Enabled = True
    Me.mo_datcons.FieldName = "mo_datcons"
    Me.mo_datcons.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_datcons.Name = "mo_datcons"
    Me.mo_datcons.Visible = True
    Me.mo_datcons.VisibleIndex = 3
    '
    'mo_codart
    '
    Me.mo_codart.AppearanceCell.Options.UseBackColor = True
    Me.mo_codart.AppearanceCell.Options.UseTextOptions = True
    Me.mo_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_codart.Caption = "Articolo"
    Me.mo_codart.Enabled = True
    Me.mo_codart.FieldName = "mo_codart"
    Me.mo_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_codart.Name = "mo_codart"
    Me.mo_codart.Visible = True
    Me.mo_codart.VisibleIndex = 4
    '
    'mo_descr
    '
    Me.mo_descr.AppearanceCell.Options.UseBackColor = True
    Me.mo_descr.AppearanceCell.Options.UseTextOptions = True
    Me.mo_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_descr.Caption = "Desr. Articolo"
    Me.mo_descr.Enabled = True
    Me.mo_descr.FieldName = "mo_descr"
    Me.mo_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_descr.Name = "mo_descr"
    Me.mo_descr.Visible = True
    Me.mo_descr.VisibleIndex = 5
    '
    'mo_valoremm
    '
    Me.mo_valoremm.AppearanceCell.Options.UseBackColor = True
    Me.mo_valoremm.AppearanceCell.Options.UseTextOptions = True
    Me.mo_valoremm.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_valoremm.Caption = "Valore riga"
    Me.mo_valoremm.Enabled = True
    Me.mo_valoremm.FieldName = "mo_valoremm"
    Me.mo_valoremm.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_valoremm.Name = "mo_valoremm"
    Me.mo_valoremm.Visible = True
    Me.mo_valoremm.VisibleIndex = 6
    '
    'mo_desint
    '
    Me.mo_desint.AppearanceCell.Options.UseBackColor = True
    Me.mo_desint.AppearanceCell.Options.UseTextOptions = True
    Me.mo_desint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_desint.Caption = "Descr Int."
    Me.mo_desint.Enabled = True
    Me.mo_desint.FieldName = "mo_desint"
    Me.mo_desint.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_desint.Name = "mo_desint"
    Me.mo_desint.Visible = True
    Me.mo_desint.VisibleIndex = 7
    '
    'mo_note
    '
    Me.mo_note.AppearanceCell.Options.UseBackColor = True
    Me.mo_note.AppearanceCell.Options.UseTextOptions = True
    Me.mo_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_note.Caption = "note"
    Me.mo_note.Enabled = True
    Me.mo_note.FieldName = "mo_note"
    Me.mo_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_note.Name = "mo_note"
    Me.mo_note.Visible = True
    Me.mo_note.VisibleIndex = 8
    '
    'mo_anno
    '
    Me.mo_anno.AppearanceCell.Options.UseBackColor = True
    Me.mo_anno.AppearanceCell.Options.UseTextOptions = True
    Me.mo_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_anno.Caption = "Anno"
    Me.mo_anno.Enabled = True
    Me.mo_anno.FieldName = "mo_anno"
    Me.mo_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_anno.Name = "mo_anno"
    Me.mo_anno.Visible = True
    Me.mo_anno.VisibleIndex = 9
    '
    'mo_serie
    '
    Me.mo_serie.AppearanceCell.Options.UseBackColor = True
    Me.mo_serie.AppearanceCell.Options.UseTextOptions = True
    Me.mo_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_serie.Caption = "Serie"
    Me.mo_serie.Enabled = True
    Me.mo_serie.FieldName = "mo_serie"
    Me.mo_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_serie.Name = "mo_serie"
    Me.mo_serie.Visible = True
    Me.mo_serie.VisibleIndex = 10
    '
    'mo_numord
    '
    Me.mo_numord.AppearanceCell.Options.UseBackColor = True
    Me.mo_numord.AppearanceCell.Options.UseTextOptions = True
    Me.mo_numord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_numord.Caption = "Num Ord"
    Me.mo_numord.Enabled = True
    Me.mo_numord.FieldName = "mo_numord"
    Me.mo_numord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_numord.Name = "mo_numord"
    Me.mo_numord.Visible = True
    Me.mo_numord.VisibleIndex = 11
    '
    'mo_riga
    '
    Me.mo_riga.AppearanceCell.Options.UseBackColor = True
    Me.mo_riga.AppearanceCell.Options.UseTextOptions = True
    Me.mo_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_riga.Caption = "Riga"
    Me.mo_riga.Enabled = True
    Me.mo_riga.FieldName = "mo_riga"
    Me.mo_riga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_riga.Name = "mo_riga"
    Me.mo_riga.Visible = True
    Me.mo_riga.VisibleIndex = 12
    '
    'mo_flevas
    '
    Me.mo_flevas.AppearanceCell.Options.UseBackColor = True
    Me.mo_flevas.AppearanceCell.Options.UseTextOptions = True
    Me.mo_flevas.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_flevas.Caption = "Evaso"
    Me.mo_flevas.Enabled = True
    Me.mo_flevas.FieldName = "mo_flevas"
    Me.mo_flevas.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_flevas.Name = "mo_flevas"
    Me.mo_flevas.Visible = True
    Me.mo_flevas.VisibleIndex = 13
    '
    'td_riferim
    '
    Me.td_riferim.AppearanceCell.Options.UseBackColor = True
    Me.td_riferim.AppearanceCell.Options.UseTextOptions = True
    Me.td_riferim.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_riferim.Caption = "Rif"
    Me.td_riferim.Enabled = True
    Me.td_riferim.FieldName = "td_riferim"
    Me.td_riferim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_riferim.Name = "td_riferim"
    Me.td_riferim.Visible = True
    Me.td_riferim.VisibleIndex = 14
    '
    'NtsGroupBox1
    '
    Me.NtsGroupBox1.AllowDrop = True
    Me.NtsGroupBox1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox1.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox1.Controls.Add(Me.edxx_aconto)
    Me.NtsGroupBox1.Controls.Add(Me.NtsLabel6)
    Me.NtsGroupBox1.Controls.Add(Me.edxx_daconto)
    Me.NtsGroupBox1.Controls.Add(Me.NtsLabel5)
    Me.NtsGroupBox1.Controls.Add(Me.ckxx_soloap)
    Me.NtsGroupBox1.Controls.Add(Me.NtsLabel3)
    Me.NtsGroupBox1.Controls.Add(Me.CmdRicerca)
    Me.NtsGroupBox1.Controls.Add(Me.NtsLabel4)
    Me.NtsGroupBox1.Controls.Add(Me.NtsLabel2)
    Me.NtsGroupBox1.Controls.Add(Me.NtsLabel1)
    Me.NtsGroupBox1.Controls.Add(Me.edxx_adtord)
    Me.NtsGroupBox1.Controls.Add(Me.edxx_adtcons)
    Me.NtsGroupBox1.Controls.Add(Me.edxx_dadtord)
    Me.NtsGroupBox1.Controls.Add(Me.edxx_dadtcons)
    Me.NtsGroupBox1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsGroupBox1.Dock = System.Windows.Forms.DockStyle.Left
    Me.NtsGroupBox1.Location = New System.Drawing.Point(0, 0)
    Me.NtsGroupBox1.Name = "NtsGroupBox1"
    Me.NtsGroupBox1.Size = New System.Drawing.Size(452, 96)
    Me.NtsGroupBox1.TabIndex = 9
    Me.NtsGroupBox1.Text = "FILTRI DI RICERCA"
    Me.NtsGroupBox1.Tile = False
    Me.NtsGroupBox1.TileIndex = -1
    '
    'edxx_aconto
    '
    Me.edxx_aconto.EditValue = "0"
    Me.edxx_aconto.Location = New System.Drawing.Point(260, 72)
    Me.edxx_aconto.Name = "edxx_aconto"
    Me.edxx_aconto.Properties.Appearance.Options.UseTextOptions = True
    Me.edxx_aconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edxx_aconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_aconto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_aconto.Properties.AutoHeight = False
    Me.edxx_aconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_aconto.Properties.MaxLength = 65536
    Me.edxx_aconto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_aconto.Size = New System.Drawing.Size(84, 20)
    Me.edxx_aconto.TabIndex = 16
    '
    'NtsLabel6
    '
    Me.NtsLabel6.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel6.Location = New System.Drawing.Point(176, 72)
    Me.NtsLabel6.Name = "NtsLabel6"
    Me.NtsLabel6.NTSBordeStyle = NTSInformatica.NTSLabel.NTSBorderStyle.NotSet
    Me.NtsLabel6.Size = New System.Drawing.Size(80, 20)
    Me.NtsLabel6.TabIndex = 15
    Me.NtsLabel6.Text = "A Conto"
    Me.NtsLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.NtsLabel6.Tooltip = ""
    Me.NtsLabel6.UseMnemonic = False
    '
    'edxx_daconto
    '
    Me.edxx_daconto.EditValue = "0"
    Me.edxx_daconto.Location = New System.Drawing.Point(88, 72)
    Me.edxx_daconto.Name = "edxx_daconto"
    Me.edxx_daconto.Properties.Appearance.Options.UseTextOptions = True
    Me.edxx_daconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edxx_daconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_daconto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_daconto.Properties.AutoHeight = False
    Me.edxx_daconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_daconto.Properties.MaxLength = 65536
    Me.edxx_daconto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_daconto.Size = New System.Drawing.Size(84, 20)
    Me.edxx_daconto.TabIndex = 14
    '
    'NtsLabel5
    '
    Me.NtsLabel5.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel5.Location = New System.Drawing.Point(4, 72)
    Me.NtsLabel5.Name = "NtsLabel5"
    Me.NtsLabel5.NTSBordeStyle = NTSInformatica.NTSLabel.NTSBorderStyle.NotSet
    Me.NtsLabel5.Size = New System.Drawing.Size(80, 20)
    Me.NtsLabel5.TabIndex = 13
    Me.NtsLabel5.Text = "Da Conto"
    Me.NtsLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.NtsLabel5.Tooltip = ""
    Me.NtsLabel5.UseMnemonic = False
    '
    'ckxx_soloap
    '
    Me.ckxx_soloap.Location = New System.Drawing.Point(348, 24)
    Me.ckxx_soloap.Name = "ckxx_soloap"
    Me.ckxx_soloap.NTSBordeStyle = NTSInformatica.NTSCheckBox.NTSBorderStyle.NotSet
    Me.ckxx_soloap.NTSPictureChecked = ""
    Me.ckxx_soloap.NTSPictureUnchecked = ""
    Me.ckxx_soloap.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckxx_soloap.Properties.Appearance.Options.UseBackColor = True
    Me.ckxx_soloap.Properties.Appearance.Options.UseTextOptions = True
    Me.ckxx_soloap.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
    Me.ckxx_soloap.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.ckxx_soloap.Properties.AutoHeight = False
    Me.ckxx_soloap.Properties.Caption = "Solo Aperti"
    Me.ckxx_soloap.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
    Me.ckxx_soloap.Properties.PictureChecked = CType(resources.GetObject("ckxx_soloap.Properties.PictureChecked"), System.Drawing.Image)
    Me.ckxx_soloap.Properties.PictureUnchecked = CType(resources.GetObject("ckxx_soloap.Properties.PictureUnchecked"), System.Drawing.Image)
    Me.ckxx_soloap.Size = New System.Drawing.Size(100, 20)
    Me.ckxx_soloap.TabIndex = 11
    '
    'NtsLabel3
    '
    Me.NtsLabel3.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel3.Location = New System.Drawing.Point(176, 48)
    Me.NtsLabel3.Name = "NtsLabel3"
    Me.NtsLabel3.NTSBordeStyle = NTSInformatica.NTSLabel.NTSBorderStyle.NotSet
    Me.NtsLabel3.Size = New System.Drawing.Size(80, 20)
    Me.NtsLabel3.TabIndex = 8
    Me.NtsLabel3.Text = "A Data Cons"
    Me.NtsLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.NtsLabel3.Tooltip = ""
    Me.NtsLabel3.UseMnemonic = False
    '
    'CmdRicerca
    '
    Me.CmdRicerca.ImageText = ""
    Me.CmdRicerca.Location = New System.Drawing.Point(348, 48)
    Me.CmdRicerca.Name = "CmdRicerca"
    Me.CmdRicerca.Size = New System.Drawing.Size(100, 44)
    Me.CmdRicerca.TabIndex = 10
    Me.CmdRicerca.Text = "&Ricerca"
    '
    'NtsLabel4
    '
    Me.NtsLabel4.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel4.Location = New System.Drawing.Point(176, 24)
    Me.NtsLabel4.Name = "NtsLabel4"
    Me.NtsLabel4.NTSBordeStyle = NTSInformatica.NTSLabel.NTSBorderStyle.NotSet
    Me.NtsLabel4.Size = New System.Drawing.Size(80, 20)
    Me.NtsLabel4.TabIndex = 7
    Me.NtsLabel4.Text = "A Data Ord"
    Me.NtsLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.NtsLabel4.Tooltip = ""
    Me.NtsLabel4.UseMnemonic = False
    '
    'NtsLabel2
    '
    Me.NtsLabel2.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel2.Location = New System.Drawing.Point(4, 48)
    Me.NtsLabel2.Name = "NtsLabel2"
    Me.NtsLabel2.NTSBordeStyle = NTSInformatica.NTSLabel.NTSBorderStyle.NotSet
    Me.NtsLabel2.Size = New System.Drawing.Size(80, 20)
    Me.NtsLabel2.TabIndex = 6
    Me.NtsLabel2.Text = "Da Data Cons"
    Me.NtsLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.NtsLabel2.Tooltip = ""
    Me.NtsLabel2.UseMnemonic = False
    '
    'NtsLabel1
    '
    Me.NtsLabel1.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel1.Location = New System.Drawing.Point(4, 24)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.NTSBordeStyle = NTSInformatica.NTSLabel.NTSBorderStyle.NotSet
    Me.NtsLabel1.Size = New System.Drawing.Size(80, 20)
    Me.NtsLabel1.TabIndex = 5
    Me.NtsLabel1.Text = "Da Data Ord"
    Me.NtsLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.NtsLabel1.Tooltip = ""
    Me.NtsLabel1.UseMnemonic = False
    '
    'edxx_adtord
    '
    Me.edxx_adtord.EditValue = "01/01/1900"
    Me.edxx_adtord.Location = New System.Drawing.Point(260, 24)
    Me.edxx_adtord.Name = "edxx_adtord"
    Me.edxx_adtord.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_adtord.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_adtord.Properties.AutoHeight = False
    Me.edxx_adtord.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_adtord.Properties.MaxLength = 65536
    Me.edxx_adtord.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_adtord.Size = New System.Drawing.Size(84, 20)
    Me.edxx_adtord.TabIndex = 4
    '
    'edxx_adtcons
    '
    Me.edxx_adtcons.EditValue = "01/01/1900"
    Me.edxx_adtcons.Location = New System.Drawing.Point(260, 48)
    Me.edxx_adtcons.Name = "edxx_adtcons"
    Me.edxx_adtcons.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_adtcons.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_adtcons.Properties.AutoHeight = False
    Me.edxx_adtcons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_adtcons.Properties.MaxLength = 65536
    Me.edxx_adtcons.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_adtcons.Size = New System.Drawing.Size(84, 20)
    Me.edxx_adtcons.TabIndex = 3
    '
    'edxx_dadtord
    '
    Me.edxx_dadtord.EditValue = "01/01/1900"
    Me.edxx_dadtord.Location = New System.Drawing.Point(88, 24)
    Me.edxx_dadtord.Name = "edxx_dadtord"
    Me.edxx_dadtord.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_dadtord.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_dadtord.Properties.AutoHeight = False
    Me.edxx_dadtord.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_dadtord.Properties.MaxLength = 65536
    Me.edxx_dadtord.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_dadtord.Size = New System.Drawing.Size(84, 20)
    Me.edxx_dadtord.TabIndex = 2
    '
    'edxx_dadtcons
    '
    Me.edxx_dadtcons.EditValue = "01/01/1900"
    Me.edxx_dadtcons.Location = New System.Drawing.Point(88, 48)
    Me.edxx_dadtcons.Name = "edxx_dadtcons"
    Me.edxx_dadtcons.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_dadtcons.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_dadtcons.Properties.AutoHeight = False
    Me.edxx_dadtcons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_dadtcons.Properties.MaxLength = 65536
    Me.edxx_dadtcons.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_dadtcons.Size = New System.Drawing.Size(84, 20)
    Me.edxx_dadtcons.TabIndex = 1
    '
    'NtsPanel1
    '
    Me.NtsPanel1.AllowDrop = True
    Me.NtsPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel1.Controls.Add(Me.NtsPanel5)
    Me.NtsPanel1.Controls.Add(Me.NtsPanel4)
    Me.NtsPanel1.Controls.Add(Me.NtsPanel2)
    Me.NtsPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.NtsPanel1.FreeLayout = False
    Me.NtsPanel1.Location = New System.Drawing.Point(0, 35)
    Me.NtsPanel1.Manager = Nothing
    Me.NtsPanel1.Name = "NtsPanel1"
    Me.NtsPanel1.NTSAutoScroll = False
    Me.NtsPanel1.Size = New System.Drawing.Size(547, 368)
    Me.NtsPanel1.TabIndex = 10
    '
    'NtsPanel5
    '
    Me.NtsPanel5.AllowDrop = True
    Me.NtsPanel5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel5.Dock = System.Windows.Forms.DockStyle.Top
    Me.NtsPanel5.FreeLayout = False
    Me.NtsPanel5.Location = New System.Drawing.Point(0, 96)
    Me.NtsPanel5.Manager = Nothing
    Me.NtsPanel5.Name = "NtsPanel5"
    Me.NtsPanel5.NTSAutoScroll = False
    Me.NtsPanel5.Size = New System.Drawing.Size(547, 4)
    Me.NtsPanel5.TabIndex = 14
    '
    'NtsPanel4
    '
    Me.NtsPanel4.AllowDrop = True
    Me.NtsPanel4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel4.Controls.Add(Me.grricerca)
    Me.NtsPanel4.Dock = System.Windows.Forms.DockStyle.Fill
    Me.NtsPanel4.FreeLayout = False
    Me.NtsPanel4.Location = New System.Drawing.Point(0, 96)
    Me.NtsPanel4.Manager = Nothing
    Me.NtsPanel4.Name = "NtsPanel4"
    Me.NtsPanel4.NTSAutoScroll = False
    Me.NtsPanel4.Size = New System.Drawing.Size(547, 272)
    Me.NtsPanel4.TabIndex = 13
    '
    'NtsPanel2
    '
    Me.NtsPanel2.AllowDrop = True
    Me.NtsPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel2.Controls.Add(Me.NtsPanel3)
    Me.NtsPanel2.Controls.Add(Me.NtsGroupBox2)
    Me.NtsPanel2.Dock = System.Windows.Forms.DockStyle.Top
    Me.NtsPanel2.FreeLayout = False
    Me.NtsPanel2.Location = New System.Drawing.Point(0, 0)
    Me.NtsPanel2.Manager = Nothing
    Me.NtsPanel2.Name = "NtsPanel2"
    Me.NtsPanel2.NTSAutoScroll = False
    Me.NtsPanel2.Size = New System.Drawing.Size(547, 96)
    Me.NtsPanel2.TabIndex = 12
    '
    'NtsPanel3
    '
    Me.NtsPanel3.AllowDrop = True
    Me.NtsPanel3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel3.Controls.Add(Me.NtsGroupBox1)
    Me.NtsPanel3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.NtsPanel3.FreeLayout = False
    Me.NtsPanel3.Location = New System.Drawing.Point(0, 0)
    Me.NtsPanel3.Manager = Nothing
    Me.NtsPanel3.Name = "NtsPanel3"
    Me.NtsPanel3.NTSAutoScroll = False
    Me.NtsPanel3.Size = New System.Drawing.Size(455, 96)
    Me.NtsPanel3.TabIndex = 12
    '
    'NtsGroupBox2
    '
    Me.NtsGroupBox2.AllowDrop = True
    Me.NtsGroupBox2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox2.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox2.Controls.Add(Me.edxx_dtfatt)
    Me.NtsGroupBox2.Controls.Add(Me.CmdElabora)
    Me.NtsGroupBox2.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsGroupBox2.Dock = System.Windows.Forms.DockStyle.Right
    Me.NtsGroupBox2.Location = New System.Drawing.Point(456, 0)
    Me.NtsGroupBox2.Name = "NtsGroupBox2"
    Me.NtsGroupBox2.Size = New System.Drawing.Size(92, 96)
    Me.NtsGroupBox2.TabIndex = 11
    Me.NtsGroupBox2.Tag = ""
    Me.NtsGroupBox2.Text = "DATA FATT."
    Me.NtsGroupBox2.Tile = False
    Me.NtsGroupBox2.TileIndex = -1
    '
    'edxx_dtfatt
    '
    Me.edxx_dtfatt.EditValue = "01/01/1900"
    Me.edxx_dtfatt.Location = New System.Drawing.Point(4, 24)
    Me.edxx_dtfatt.Name = "edxx_dtfatt"
    Me.edxx_dtfatt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_dtfatt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_dtfatt.Properties.AutoHeight = False
    Me.edxx_dtfatt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_dtfatt.Properties.MaxLength = 65536
    Me.edxx_dtfatt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_dtfatt.Size = New System.Drawing.Size(84, 20)
    Me.edxx_dtfatt.TabIndex = 17
    '
    'CmdElabora
    '
    Me.CmdElabora.Enabled = False
    Me.CmdElabora.ImageText = ""
    Me.CmdElabora.Location = New System.Drawing.Point(4, 48)
    Me.CmdElabora.Name = "CmdElabora"
    Me.CmdElabora.Size = New System.Drawing.Size(84, 44)
    Me.CmdElabora.TabIndex = 0
    Me.CmdElabora.Text = "Elabora"
    '
    'FRMHHDORO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(547, 403)
    Me.Controls.Add(Me.NtsPanel1)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMHHDORO"
    Me.Text = "GENERA FATTURE DA ORDINI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grricerca, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvricerca, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox1.ResumeLayout(False)
    CType(Me.edxx_aconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_daconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckxx_soloap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_adtord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_adtcons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_dadtord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_dadtcons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel1.ResumeLayout(False)
    CType(Me.NtsPanel5, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsPanel4, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel4.ResumeLayout(False)
    CType(Me.NtsPanel2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel2.ResumeLayout(False)
    CType(Me.NtsPanel3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel3.ResumeLayout(False)
    CType(Me.NtsGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox2.ResumeLayout(False)
    CType(Me.edxx_dtfatt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub




  Public ReadOnly Property Moduli() As Integer
    Get
      Return Moduli_P
    End Get
  End Property
  Public ReadOnly Property ModuliExt() As Integer
    Get
      Return ModuliExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliSup() As Integer
    Get
      Return ModuliSup_P
    End Get
  End Property
  Public ReadOnly Property ModuliSupExt() As Integer
    Get
      Return ModuliSupExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtn() As Integer
    Get
      Return ModuliPtn_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtnExt() As Integer
    Get
      Return ModuliPtnExt_P
    End Get
  End Property
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbApri As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRecordCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRecordRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbNavDoc As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaPdf As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents NtsBar1 As NTSInformatica.NTSBar
  Public WithEvents NtsBar2 As NTSInformatica.NTSBar
#End Region

  Private components As System.ComponentModel.IContainer
  Public oCleHh As CLEHHDORO
  Public dsHh As New DataSet
  Public oCallParams As CLE__CLDP
  Public dcHh As BindingSource = New BindingSource
  Public dcHhGr As BindingSource = New BindingSource

  Public Overridable Sub InitControls()
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try


        ckxx_soloap.NTSSetParam(oMenu, oApp.Tr(Me, 131723177460309382, "Solo Aperti"), "S", "N")
        edxx_adtord.NTSSetParam(oMenu, oApp.Tr(Me, 131723177460611585, "a data ordine"), False)
        edxx_adtcons.NTSSetParam(oMenu, oApp.Tr(Me, 131723177460713571, "a data consegna"), False)
        edxx_dadtord.NTSSetParam(oMenu, oApp.Tr(Me, 131723177460713645, "da data ordine"), False)
        edxx_dadtcons.NTSSetParam(oMenu, oApp.Tr(Me, 131723177460816776, "da data consegna"), False)
        edxx_aconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 131729158052706411, "Da Conto"), tabanagrac)
        edxx_daconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 131729158053018886, "A Conto"), tabanagrac)
        edxx_dtfatt.NTSSetParam(oMenu, oApp.Tr(Me, 131729186096607772, "Data fattura"), False)

        grvricerca.NTSSetParam(oMenu, oApp.Tr(Me, 131723177446392896, "Ricerca"))
        xx_sel.NTSSetParamCHK(oMenu, oApp.Tr(Me, 131723190940602511, "Seleziona"), "S", "N")
        an_descr1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131723190940662650, "Cliente"), 0, True)
        td_datord.NTSSetParamDATA(oMenu, oApp.Tr(Me, 131723190940662865, "Data Ord"), True)
        mo_datcons.NTSSetParamDATA(oMenu, oApp.Tr(Me, 131723190940702900, "Data consegna"), True)
        mo_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131723190940762930, "Articolo"), 0, True)
        mo_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131723190940763126, "Desr. Articolo"), 0, True)
        mo_valoremm.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131723190941267068, "Valore riga"), "#,##0.00", 15)
        mo_desint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131723190941376149, "Descr Int."), 0, True)
        mo_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131723190941376810, "note"), 0, True)
        mo_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131723190941411934, "Anno"), "0", 4, 0, 9999)
        mo_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131723190941477054, "Serie"), 0, True)
        mo_numord.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131723190941477705, "Num Ord"), "0", 9, 0, 999999999)
        mo_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131723190941522818, "Riga"), "0", 9, 0, 999999999)
        mo_flevas.NTSSetParamCHK(oMenu, oApp.Tr(Me, 131723190941577921, "Evaso"), "S", "C")
        td_riferim.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131723190941578181, "Rif"), 0, True)
        grvricerca.NTSMenuContext = tlbStrumenti

        Try
          Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
          tlbNuovo.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\New.png")
          tlbApri.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\open.png")
          tlbSalva.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\save.png")
          tlbCancella.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\delete.png")
          tlbRipristina.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\restore.png")
          tlbZoom.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\zoom.png")
          tlbRecordCancella.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\recdelete.png")
          tlbRecordRipristina.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\recrestore.png")
          'tlbSelRigheOrdini.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\doc.png")
          'tlbNotaPrel.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\ordini_2.png")
          'tlbSelOrdini.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\ordini.png")
          'tlbDettaglioTCO.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\tc.png")
          tlbStrumenti.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\options.png")
          tlbStampa.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\print.png")
          tlbStampaVideo.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\prnscreen.png")
          tlbStampaPdf.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\pdf.png")
          tlbGuida.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\help.png")
          tlbEsci.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\exit.png")
        Catch ex As Exception
          'non gestisco l'errore: se non c'è una immagine prendo quella standard
        End Try
        tlbMain.NTSSetToolTip()
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      Me.Cursor = System.Windows.Forms.Cursors.Default
      '-------------------------------------------------
      'completo le informazioni dei controlli
      '-------------------------------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano già  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      ckxx_soloap.NTSText.NTSDbField = "XXX.xx_soloap"
      edxx_adtord.NTSDbField = "XXX.xx_adtord"
      edxx_adtcons.NTSDbField = "XXX.xx_adtcons"
      edxx_dadtord.NTSDbField = "XXX.xx_dadtord"
      edxx_dadtcons.NTSDbField = "XXX.xx_dadtcons"
      edxx_aconto.NTSDbField = "XXX.xx_aconto"
      edxx_daconto.NTSDbField = "XXX.xx_daconto"
      edxx_dtfatt.NTSDbField = "XXX.xx_dtfatt"
      NTSFormAddDataBinding(dcHh, Me)
      '-------------------------------------------------
      'per agganciare al dataset i vari controlli


    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub

  Private Sub FRMHHDORO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    dsHh = oCleHh.dsShared
    dcHh = Nothing
    dcHh = New BindingSource()
    dcHh.DataSource = dsHh.Tables("XXX")
    InitControls()
    Bindcontrols()
    GctlSetRoules()
    GctlApplicaDefaultValue()
  End Sub
  Public Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)

    If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non è per questa form ...
    MyBase.GestisciEventiEntity(sender, e)
    Try
      If e.TipoEvento.Length >= 5 Then
        If Mid(e.TipoEvento, 1, 4) = "CPNE" Then
          Select Case e.TipoEvento
            Case "CPNE.AggGriglia"
              CPNEAggGrilgia()
          End Select
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub
  Private Sub CPNEAggGrilgia()
    dcHhGr = Nothing
    dcHhGr = New BindingSource()
    dcHhGr.DataSource = dsHh.Tables("ric")
    grricerca.DataSource = dcHhGr
    grvricerca.NTSAllowDelete = False
    grvricerca.NTSAllowInsert = False
    grvricerca.Enabled = True
    For Each col As NTSGridColumn In grvricerca.Columns
      If col.Name = "xx_sel" Then
        col.Enabled = True
      Else
        col.Enabled = False
      End If
    Next
  End Sub
  Private Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    If 1 = 2 Then 'zoom custom su campo
      'zoom custom
    ElseIf 1 = 3 Then 'zoom custom su altro campo
      'zoom custom
    Else
      NTSCallStandardZoom()
    End If

  End Sub

  Private Sub tlbRecordCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordCancella.ItemClick
    oCleHh.CPNECancRiga(grvricerca.NTSGetCurrentDataRow)
  End Sub

  Private Sub tlbRecordRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordRipristina.ItemClick
    oCleHh.CPNERipristinaRiga(grvricerca.NTSGetCurrentDataRow)
  End Sub

  Private Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
  Private Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Stampa(1)
  End Sub

  Private Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Stampa(0)
  End Sub
  Private Sub Stampa(ByVal nDestin As Integer)
    Dim StrWhere As String

    StrWhere = "{testord.codditt} = " & CStrSQL(oCleHh.strDittaCorrente) & " And {testord.td_datord} >= " & ConvStrRpt(oCleHh.drxxx!xx_dadtord.ToString) & " And {testord.td_datord} <= " & ConvStrRpt(oCleHh.drxxx!xx_adtord.ToString)
    StrWhere += " And {movord.mo_datcons} >= " & ConvStrRpt(oCleHh.drxxx!xx_dadtcons.ToString) & " And {movord.mo_datcons} <= " & ConvStrRpt(oCleHh.drxxx!xx_adtcons.ToString)
    If oCleHh.drxxx!xx_soloap.ToString = "S" Then
      StrWhere += " And {movord.mo_flevas} = 'C'"
    End If

    Dim nPjob As Object
    nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSHHDORO", "REPORTS1", " ", 0, nDestin, "BSHHDORO.rpt", False, "Stampa", False)
    If Not (nPjob Is Nothing) Then
      'lancio tutti gli eventuali reports (gestisce già il multireport)												
      For i = 1 To UBound(CType(nPjob, Array), 2)
        oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), StrWhere))
        oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next
    End If
  End Sub

  Private Sub CmdRicerca_Click(sender As Object, e As EventArgs) Handles CmdRicerca.Click
    oCleHh.CPNERicerca()
    If ckxx_soloap.Checked Then
      CmdElabora.Enabled = True
    Else
      CmdElabora.Enabled = False
    End If
  End Sub

  Private Sub FRMHHDORO_ActivatedFirst(sender As Object, e As EventArgs) Handles Me.ActivatedFirst
    CPNEAggGrilgia()

  End Sub

  Private Sub CmdElabora_Click(sender As Object, e As EventArgs) Handles CmdElabora.Click
    If oCleHh.CPNEGeneraDocumento Then

    End If
  End Sub

  Private Sub tlbNavDoc_ItemClick(sender As Object, e As ItemClickEventArgs) Handles tlbNavDoc.ItemClick
    Dim Dr As DataRow = grvricerca.NTSGetCurrentDataRow
    If IsNothing(Dr) Then
      oApp.MsgBoxInfo("Prima posizionarsi su una riga")
      Return
    End If
    Dim strParam As String = "APRI;" & Dr!mo_tipork.ToString & ";" &
                Dr!mo_anno.ToString & ";" &
                Dr!mo_serie.ToString & ";" &
                Microsoft.VisualBasic.Right(NTSCInt(Dr!mo_numord.ToString).ToString.PadLeft(9, "0"c), 9) & ";" &
                Microsoft.VisualBasic.Right(NTSCInt(Dr!td_conto).ToString.PadLeft(9, "0"c), 9) & ";" &
                Microsoft.VisualBasic.Right("          " & NTSCDate(Dr!td_datord.ToString).ToString("dd/MM/yyyy"), 10) &
                ";000000000;0000;0000; ;000000000;0000;1"
    oMenu.RunChild("BS__FLDO", "CLS__FLDO", oApp.Tr(Me, 128154847220982619, "Navigazione Documentale"), DittaCorrente, "", "", Nothing, strParam, True, True)

  End Sub



  Private Sub TlbSelTutto_ItemClick(sender As Object, e As ItemClickEventArgs) Handles TlbSelTutto.ItemClick
    ValidaLastControl()
    For i = 0 To dsHh.Tables("Ric").Rows.Count - 1
      dsHh.Tables("Ric").Rows(i)!xx_sel = "S"
    Next
  End Sub

  Private Sub tlbDeselTutto_ItemClick(sender As Object, e As ItemClickEventArgs) Handles tlbDeselTutto.ItemClick
    ValidaLastControl()
    For i = 0 To dsHh.Tables("Ric").Rows.Count - 1
      dsHh.Tables("Ric").Rows(i)!xx_sel = "N"
    Next
  End Sub

  Private Sub ckxx_soloap_CheckedChanged(sender As Object, e As EventArgs) Handles ckxx_soloap.CheckedChanged

  End Sub
End Class