Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO
Imports DevExpress.XtraBars
Imports DevExpress.XtraGrid.Views.Base

Public Class FRMHHMMTR
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNHHMMTR", "BEHHMMTR", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128271029889882656, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleHh = CType(oTmp, CLEHHMMTR)
    oCleHh.AssociaDs(dsHh)
    oCleHh.OMenu = oMenu
    '------------------------------------------------

    AddHandler oCleHh.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleHh.Init(oApp, NTSScript, oMenu.oCleComm, "HHMATRICOLE", False, "", "") = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMHHMMTR))
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
    Me.NtsPanel1 = New NTSInformatica.NTSPanel()
    Me.NtsTabControl1 = New NTSInformatica.NTSTabControl()
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage()
    Me.CLIENTI = New NTSInformatica.NTSGroupBox()
    Me.grdClienti = New NTSInformatica.NTSGrid()
    Me.grvClienti = New NTSInformatica.NTSGridView()
    Me.hh_contocli = New NTSInformatica.NTSGridColumn()
    Me.an_descr1 = New NTSInformatica.NTSGridColumn()
    Me.hh_datainstallazione = New NTSInformatica.NTSGridColumn()
    Me.hh_dataritiro = New NTSInformatica.NTSGridColumn()
    Me.hh_contratto = New NTSInformatica.NTSGridColumn()
    Me.hh_datascadenzacontratto = New NTSInformatica.NTSGridColumn()
    Me.xx_noleggiovendita = New NTSInformatica.NTSGridColumn()
    Me.hh_datavendita = New NTSInformatica.NTSGridColumn()
    Me.hh_scadenza = New NTSInformatica.NTSGridColumn()
    Me.hh_note = New NTSInformatica.NTSGridColumn()
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage()
    Me.NtsGroupBox1 = New NTSInformatica.NTSGroupBox()
    Me.grdDoctutti = New NTSInformatica.NTSGrid()
    Me.grvDoctutti = New NTSInformatica.NTSGridView()
    Me.hh_tipo = New NTSInformatica.NTSGridColumn()
    Me.hh_numero = New NTSInformatica.NTSGridColumn()
    Me.hh_data = New NTSInformatica.NTSGridColumn()
    Me.hh_codcf = New NTSInformatica.NTSGridColumn()
    Me.hh_ragsoc = New NTSInformatica.NTSGridColumn()
    Me.NtsTabPage3 = New NTSInformatica.NTSTabPage()
    Me.NtsGroupBox2 = New NTSInformatica.NTSGroupBox()
    Me.grdDocint = New NTSInformatica.NTSGrid()
    Me.grvDocint = New NTSInformatica.NTSGridView()
    Me.cmdRicerca = New NTSInformatica.NTSButton()
    Me.chxx_vendutodaterzi = New NTSInformatica.NTSCheckBox()
    Me.chxx_muletto = New NTSInformatica.NTSCheckBox()
    Me.chxx_dismesso = New NTSInformatica.NTSCheckBox()
    Me.chxx_usato = New NTSInformatica.NTSCheckBox()
    Me.edxx_duratagranzia = New NTSInformatica.NTSTextBoxStr()
    Me.NtsLabel5 = New NTSInformatica.NTSLabel()
    Me.lbxx_descr = New NTSInformatica.NTSLabel()
    Me.edxx_articolo = New NTSInformatica.NTSTextBoxStr()
    Me.NtsLabel4 = New NTSInformatica.NTSLabel()
    Me.edxx_matricolaproduttore = New NTSInformatica.NTSTextBoxStr()
    Me.NtsLabel3 = New NTSInformatica.NTSLabel()
    Me.edxx_nuovamatricola = New NTSInformatica.NTSTextBoxStr()
    Me.NtsLabel2 = New NTSInformatica.NTSLabel()
    Me.cmdCambia = New NTSInformatica.NTSButton()
    Me.NtsLabel1 = New NTSInformatica.NTSLabel()
    Me.edxx_matricola = New NTSInformatica.NTSTextBoxStr()
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel1.SuspendLayout()
    CType(Me.NtsTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabControl1.SuspendLayout()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.CLIENTI, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.CLIENTI.SuspendLayout()
    CType(Me.grdClienti, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvClienti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.NtsGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox1.SuspendLayout()
    CType(Me.grdDoctutti, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvDoctutti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage3.SuspendLayout()
    CType(Me.NtsGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox2.SuspendLayout()
    CType(Me.grdDocint, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvDocint, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.chxx_vendutodaterzi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.chxx_muletto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.chxx_dismesso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.chxx_usato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_duratagranzia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_articolo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_matricolaproduttore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_nuovamatricola.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_matricola.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbApri, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbRecordCancella, Me.tlbRecordRipristina, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbStampaPdf, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.TlbSelTutto, Me.tlbDeselTutto})
    Me.NtsBarManager1.MaxItemId = 120
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbApri, False), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbRipristina, False), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbRecordCancella, True), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbRecordRipristina, False), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbStampaPdf, False), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbNuovo.Id = 0
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = True
    '
    'tlbApri
    '
    Me.tlbApri.Caption = "Apri"
    Me.tlbApri.Glyph = CType(resources.GetObject("tlbApri.Glyph"), System.Drawing.Image)
    Me.tlbApri.Id = 1
    Me.tlbApri.Name = "tlbApri"
    Me.tlbApri.Visible = False
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.Id = 2
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.Id = 4
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = False
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 3
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 5
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbRecordCancella
    '
    Me.tlbRecordCancella.Caption = "Cancella riga"
    Me.tlbRecordCancella.Glyph = CType(resources.GetObject("tlbRecordCancella.Glyph"), System.Drawing.Image)
    Me.tlbRecordCancella.Id = 6
    Me.tlbRecordCancella.Name = "tlbRecordCancella"
    Me.tlbRecordCancella.Visible = False
    '
    'tlbRecordRipristina
    '
    Me.tlbRecordRipristina.Caption = "Ripristina riga"
    Me.tlbRecordRipristina.Glyph = CType(resources.GetObject("tlbRecordRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRecordRipristina.Id = 7
    Me.tlbRecordRipristina.Name = "tlbRecordRipristina"
    Me.tlbRecordRipristina.Visible = False
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.TlbSelTutto), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDeselTutto)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'TlbSelTutto
    '
    Me.TlbSelTutto.Caption = "Seleziona tutto"
    Me.TlbSelTutto.Id = 118
    Me.TlbSelTutto.Name = "TlbSelTutto"
    Me.TlbSelTutto.NTSIsCheckBox = False
    Me.TlbSelTutto.Visible = True
    '
    'tlbDeselTutto
    '
    Me.tlbDeselTutto.Caption = "Deselziona Tutto"
    Me.tlbDeselTutto.Id = 119
    Me.tlbDeselTutto.Name = "tlbDeselTutto"
    Me.tlbDeselTutto.NTSIsCheckBox = False
    Me.tlbDeselTutto.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 9
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "StampaVideo"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 10
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbStampaPdf
    '
    Me.tlbStampaPdf.Caption = "StampaPdf"
    Me.tlbStampaPdf.Glyph = CType(resources.GetObject("tlbStampaPdf.Glyph"), System.Drawing.Image)
    Me.tlbStampaPdf.Id = 12
    Me.tlbStampaPdf.Name = "tlbStampaPdf"
    Me.tlbStampaPdf.Visible = False
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 13
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 14
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'barDockControlTop
    '
    Me.barDockControlTop.CausesValidation = False
    Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
    Me.barDockControlTop.Size = New System.Drawing.Size(1015, 35)
    '
    'barDockControlBottom
    '
    Me.barDockControlBottom.CausesValidation = False
    Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.barDockControlBottom.Location = New System.Drawing.Point(0, 511)
    Me.barDockControlBottom.Size = New System.Drawing.Size(1015, 0)
    '
    'barDockControlLeft
    '
    Me.barDockControlLeft.CausesValidation = False
    Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
    Me.barDockControlLeft.Location = New System.Drawing.Point(0, 35)
    Me.barDockControlLeft.Size = New System.Drawing.Size(0, 476)
    '
    'barDockControlRight
    '
    Me.barDockControlRight.CausesValidation = False
    Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
    Me.barDockControlRight.Location = New System.Drawing.Point(1015, 35)
    Me.barDockControlRight.Size = New System.Drawing.Size(0, 476)
    '
    'NtsPanel1
    '
    Me.NtsPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel1.Controls.Add(Me.NtsTabControl1)
    Me.NtsPanel1.Controls.Add(Me.cmdRicerca)
    Me.NtsPanel1.Controls.Add(Me.chxx_vendutodaterzi)
    Me.NtsPanel1.Controls.Add(Me.chxx_muletto)
    Me.NtsPanel1.Controls.Add(Me.chxx_dismesso)
    Me.NtsPanel1.Controls.Add(Me.chxx_usato)
    Me.NtsPanel1.Controls.Add(Me.edxx_duratagranzia)
    Me.NtsPanel1.Controls.Add(Me.NtsLabel5)
    Me.NtsPanel1.Controls.Add(Me.lbxx_descr)
    Me.NtsPanel1.Controls.Add(Me.edxx_articolo)
    Me.NtsPanel1.Controls.Add(Me.NtsLabel4)
    Me.NtsPanel1.Controls.Add(Me.edxx_matricolaproduttore)
    Me.NtsPanel1.Controls.Add(Me.NtsLabel3)
    Me.NtsPanel1.Controls.Add(Me.edxx_nuovamatricola)
    Me.NtsPanel1.Controls.Add(Me.NtsLabel2)
    Me.NtsPanel1.Controls.Add(Me.cmdCambia)
    Me.NtsPanel1.Controls.Add(Me.NtsLabel1)
    Me.NtsPanel1.Controls.Add(Me.edxx_matricola)
    Me.NtsPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.NtsPanel1.Location = New System.Drawing.Point(0, 35)
    Me.NtsPanel1.Name = "NtsPanel1"
    Me.NtsPanel1.Size = New System.Drawing.Size(1015, 476)
    '
    'NtsTabControl1
    '
    Me.NtsTabControl1.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.NtsTabControl1.Location = New System.Drawing.Point(0, 244)
    Me.NtsTabControl1.Name = "NtsTabControl1"
    Me.NtsTabControl1.SelectedTabPage = Me.NtsTabPage1
    Me.NtsTabControl1.Size = New System.Drawing.Size(1015, 232)
    Me.NtsTabControl1.TabIndex = 19
    Me.NtsTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2, Me.NtsTabPage3})
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 10.0!)
    Me.NtsTabPage1.Appearance.Header.Options.UseFont = True
    Me.NtsTabPage1.Controls.Add(Me.CLIENTI)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(1009, 201)
    Me.NtsTabPage1.Text = "Clienti"
    '
    'CLIENTI
    '
    Me.CLIENTI.AllowDrop = True
    Me.CLIENTI.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.CLIENTI.Appearance.Options.UseBackColor = True
    Me.CLIENTI.AutoSize = True
    Me.CLIENTI.Controls.Add(Me.grdClienti)
    Me.CLIENTI.Dock = System.Windows.Forms.DockStyle.Fill
    Me.CLIENTI.Location = New System.Drawing.Point(0, 0)
    Me.CLIENTI.Name = "CLIENTI"
    Me.CLIENTI.Size = New System.Drawing.Size(1009, 201)
    '
    'grdClienti
    '
    Me.grdClienti.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grdClienti.Location = New System.Drawing.Point(2, 21)
    Me.grdClienti.MainView = Me.grvClienti
    Me.grdClienti.Name = "grdClienti"
    Me.grdClienti.Size = New System.Drawing.Size(1005, 178)
    Me.grdClienti.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvClienti})
    '
    'grvClienti
    '
    Me.grvClienti.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.hh_contocli, Me.an_descr1, Me.hh_datainstallazione, Me.hh_dataritiro, Me.hh_contratto, Me.hh_datascadenzacontratto, Me.xx_noleggiovendita, Me.hh_datavendita, Me.hh_scadenza, Me.hh_note})
    Me.grvClienti.Enabled = True
    Me.grvClienti.GridControl = Me.grdClienti
    Me.grvClienti.Name = "grvClienti"
    Me.grvClienti.OptionsCustomization.AllowRowSizing = True
    Me.grvClienti.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvClienti.OptionsNavigation.UseTabKey = False
    Me.grvClienti.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvClienti.OptionsView.ColumnAutoWidth = False
    Me.grvClienti.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvClienti.OptionsView.ShowGroupPanel = False
    '
    'hh_contocli
    '
    Me.hh_contocli.AppearanceCell.Options.UseBackColor = True
    Me.hh_contocli.AppearanceCell.Options.UseTextOptions = True
    Me.hh_contocli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_contocli.Caption = "Cod.Cliente"
    Me.hh_contocli.Enabled = True
    Me.hh_contocli.FieldName = "hh_contocli"
    Me.hh_contocli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_contocli.Name = "hh_contocli"
    Me.hh_contocli.Visible = True
    Me.hh_contocli.VisibleIndex = 0
    '
    'an_descr1
    '
    Me.an_descr1.AppearanceCell.Options.UseBackColor = True
    Me.an_descr1.AppearanceCell.Options.UseTextOptions = True
    Me.an_descr1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.an_descr1.Caption = "Ragione Sociale"
    Me.an_descr1.Enabled = True
    Me.an_descr1.FieldName = "an_descr1"
    Me.an_descr1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.an_descr1.Name = "an_descr1"
    Me.an_descr1.Visible = True
    Me.an_descr1.VisibleIndex = 1
    '
    'hh_datainstallazione
    '
    Me.hh_datainstallazione.AppearanceCell.Options.UseBackColor = True
    Me.hh_datainstallazione.AppearanceCell.Options.UseTextOptions = True
    Me.hh_datainstallazione.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_datainstallazione.Caption = "Data Installazione"
    Me.hh_datainstallazione.Enabled = True
    Me.hh_datainstallazione.FieldName = "hh_datainstallazione"
    Me.hh_datainstallazione.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_datainstallazione.Name = "hh_datainstallazione"
    Me.hh_datainstallazione.Visible = True
    Me.hh_datainstallazione.VisibleIndex = 2
    '
    'hh_dataritiro
    '
    Me.hh_dataritiro.AppearanceCell.Options.UseBackColor = True
    Me.hh_dataritiro.AppearanceCell.Options.UseTextOptions = True
    Me.hh_dataritiro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_dataritiro.Caption = "Data ritiro"
    Me.hh_dataritiro.Enabled = True
    Me.hh_dataritiro.FieldName = "hh_dataritiro"
    Me.hh_dataritiro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_dataritiro.Name = "hh_dataritiro"
    Me.hh_dataritiro.Visible = True
    Me.hh_dataritiro.VisibleIndex = 3
    '
    'hh_contratto
    '
    Me.hh_contratto.AppearanceCell.Options.UseBackColor = True
    Me.hh_contratto.AppearanceCell.Options.UseTextOptions = True
    Me.hh_contratto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_contratto.Caption = "Contratto"
    Me.hh_contratto.Enabled = True
    Me.hh_contratto.FieldName = "hh_contratto"
    Me.hh_contratto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_contratto.Name = "hh_contratto"
    Me.hh_contratto.Visible = True
    Me.hh_contratto.VisibleIndex = 4
    '
    'hh_datascadenzacontratto
    '
    Me.hh_datascadenzacontratto.AppearanceCell.Options.UseBackColor = True
    Me.hh_datascadenzacontratto.AppearanceCell.Options.UseTextOptions = True
    Me.hh_datascadenzacontratto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_datascadenzacontratto.Caption = "Data Scadenza Contratto"
    Me.hh_datascadenzacontratto.Enabled = True
    Me.hh_datascadenzacontratto.FieldName = "hh_datascadenzacontratto"
    Me.hh_datascadenzacontratto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_datascadenzacontratto.Name = "hh_datascadenzacontratto"
    Me.hh_datascadenzacontratto.Visible = True
    Me.hh_datascadenzacontratto.VisibleIndex = 5
    '
    'xx_noleggiovendita
    '
    Me.xx_noleggiovendita.AppearanceCell.Options.UseBackColor = True
    Me.xx_noleggiovendita.AppearanceCell.Options.UseTextOptions = True
    Me.xx_noleggiovendita.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_noleggiovendita.Caption = "Noleggio/Vendita"
    Me.xx_noleggiovendita.Enabled = True
    Me.xx_noleggiovendita.FieldName = "xx_noleggiovendita"
    Me.xx_noleggiovendita.Name = "xx_noleggiovendita"
    Me.xx_noleggiovendita.Visible = True
    Me.xx_noleggiovendita.VisibleIndex = 6
    '
    'hh_datavendita
    '
    Me.hh_datavendita.AppearanceCell.Options.UseBackColor = True
    Me.hh_datavendita.AppearanceCell.Options.UseTextOptions = True
    Me.hh_datavendita.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_datavendita.Caption = "Data Vendita"
    Me.hh_datavendita.Enabled = True
    Me.hh_datavendita.FieldName = "hh_datavendita"
    Me.hh_datavendita.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_datavendita.Name = "hh_datavendita"
    Me.hh_datavendita.Visible = True
    Me.hh_datavendita.VisibleIndex = 7
    '
    'hh_scadenza
    '
    Me.hh_scadenza.AppearanceCell.Options.UseBackColor = True
    Me.hh_scadenza.AppearanceCell.Options.UseTextOptions = True
    Me.hh_scadenza.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_scadenza.Caption = "Scadenza"
    Me.hh_scadenza.Enabled = True
    Me.hh_scadenza.FieldName = "hh_scadenza"
    Me.hh_scadenza.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_scadenza.Name = "hh_scadenza"
    Me.hh_scadenza.Visible = True
    Me.hh_scadenza.VisibleIndex = 8
    Me.hh_scadenza.Width = 99
    '
    'hh_note
    '
    Me.hh_note.AppearanceCell.Options.UseBackColor = True
    Me.hh_note.AppearanceCell.Options.UseTextOptions = True
    Me.hh_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_note.Caption = "Note"
    Me.hh_note.Enabled = True
    Me.hh_note.FieldName = "hh_note"
    Me.hh_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_note.Name = "hh_note"
    Me.hh_note.Visible = True
    Me.hh_note.VisibleIndex = 9
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 10.0!)
    Me.NtsTabPage2.Appearance.Header.Options.UseFont = True
    Me.NtsTabPage2.Controls.Add(Me.NtsGroupBox1)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(1009, 201)
    Me.NtsTabPage2.Text = "Doc. TUTTI"
    '
    'NtsGroupBox1
    '
    Me.NtsGroupBox1.AllowDrop = True
    Me.NtsGroupBox1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox1.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox1.Controls.Add(Me.grdDoctutti)
    Me.NtsGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.NtsGroupBox1.Location = New System.Drawing.Point(0, 0)
    Me.NtsGroupBox1.Name = "NtsGroupBox1"
    Me.NtsGroupBox1.Size = New System.Drawing.Size(1009, 201)
    '
    'grdDoctutti
    '
    Me.grdDoctutti.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grdDoctutti.Location = New System.Drawing.Point(2, 21)
    Me.grdDoctutti.MainView = Me.grvDoctutti
    Me.grdDoctutti.MenuManager = Me.NtsBarManager1
    Me.grdDoctutti.Name = "grdDoctutti"
    Me.grdDoctutti.Size = New System.Drawing.Size(1005, 178)
    Me.grdDoctutti.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDoctutti})
    '
    'grvDoctutti
    '
    Me.grvDoctutti.Appearance.FocusedCell.BackColor = System.Drawing.Color.FloralWhite
    Me.grvDoctutti.Appearance.FocusedCell.Options.UseBackColor = True
    Me.grvDoctutti.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.hh_tipo, Me.hh_numero, Me.hh_data, Me.hh_codcf, Me.hh_ragsoc})
    Me.grvDoctutti.Enabled = True
    Me.grvDoctutti.GridControl = Me.grdDoctutti
    Me.grvDoctutti.Name = "grvDoctutti"
    Me.grvDoctutti.OptionsCustomization.AllowRowSizing = True
    Me.grvDoctutti.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvDoctutti.OptionsNavigation.UseTabKey = False
    Me.grvDoctutti.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvDoctutti.OptionsView.ColumnAutoWidth = False
    Me.grvDoctutti.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvDoctutti.OptionsView.ShowGroupPanel = False
    '
    'hh_tipo
    '
    Me.hh_tipo.AppearanceCell.Options.UseBackColor = True
    Me.hh_tipo.AppearanceCell.Options.UseTextOptions = True
    Me.hh_tipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_tipo.Caption = "Tipo"
    Me.hh_tipo.Enabled = True
    Me.hh_tipo.FieldName = "hh_tipo"
    Me.hh_tipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_tipo.Name = "hh_tipo"
    Me.hh_tipo.Visible = True
    Me.hh_tipo.VisibleIndex = 0
    '
    'hh_numero
    '
    Me.hh_numero.AppearanceCell.Options.UseBackColor = True
    Me.hh_numero.AppearanceCell.Options.UseTextOptions = True
    Me.hh_numero.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_numero.Caption = "Numero"
    Me.hh_numero.Enabled = True
    Me.hh_numero.FieldName = "hh_numero"
    Me.hh_numero.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_numero.Name = "hh_numero"
    Me.hh_numero.Visible = True
    Me.hh_numero.VisibleIndex = 1
    '
    'hh_data
    '
    Me.hh_data.AppearanceCell.Options.UseBackColor = True
    Me.hh_data.AppearanceCell.Options.UseTextOptions = True
    Me.hh_data.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_data.Caption = "Data"
    Me.hh_data.Enabled = True
    Me.hh_data.FieldName = "hh_data"
    Me.hh_data.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_data.Name = "hh_data"
    Me.hh_data.Visible = True
    Me.hh_data.VisibleIndex = 2
    '
    'hh_codcf
    '
    Me.hh_codcf.AppearanceCell.Options.UseBackColor = True
    Me.hh_codcf.AppearanceCell.Options.UseTextOptions = True
    Me.hh_codcf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_codcf.Caption = "Cod. C/F"
    Me.hh_codcf.Enabled = True
    Me.hh_codcf.FieldName = "hh_codcf"
    Me.hh_codcf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_codcf.Name = "hh_codcf"
    Me.hh_codcf.Visible = True
    Me.hh_codcf.VisibleIndex = 3
    '
    'hh_ragsoc
    '
    Me.hh_ragsoc.AppearanceCell.Options.UseBackColor = True
    Me.hh_ragsoc.AppearanceCell.Options.UseTextOptions = True
    Me.hh_ragsoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_ragsoc.Caption = "Ragione sociale"
    Me.hh_ragsoc.Enabled = True
    Me.hh_ragsoc.FieldName = "hh_ragsoc"
    Me.hh_ragsoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_ragsoc.Name = "hh_ragsoc"
    Me.hh_ragsoc.Visible = True
    Me.hh_ragsoc.VisibleIndex = 4
    '
    'NtsTabPage3
    '
    Me.NtsTabPage3.AllowDrop = True
    Me.NtsTabPage3.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 10.0!)
    Me.NtsTabPage3.Appearance.Header.Options.UseFont = True
    Me.NtsTabPage3.Controls.Add(Me.NtsGroupBox2)
    Me.NtsTabPage3.Enable = True
    Me.NtsTabPage3.Name = "NtsTabPage3"
    Me.NtsTabPage3.Size = New System.Drawing.Size(1009, 201)
    Me.NtsTabPage3.Text = "Doc. INT"
    '
    'NtsGroupBox2
    '
    Me.NtsGroupBox2.AllowDrop = True
    Me.NtsGroupBox2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox2.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox2.Controls.Add(Me.grdDocint)
    Me.NtsGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.NtsGroupBox2.Location = New System.Drawing.Point(0, 0)
    Me.NtsGroupBox2.Name = "NtsGroupBox2"
    Me.NtsGroupBox2.Size = New System.Drawing.Size(1009, 201)
    '
    'grdDocint
    '
    Me.grdDocint.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grdDocint.Location = New System.Drawing.Point(2, 21)
    Me.grdDocint.MainView = Me.grvDocint
    Me.grdDocint.MenuManager = Me.NtsBarManager1
    Me.grdDocint.Name = "grdDocint"
    Me.grdDocint.Size = New System.Drawing.Size(1005, 178)
    Me.grdDocint.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDocint})
    '
    'grvDocint
    '
    Me.grvDocint.Appearance.FocusedCell.BackColor = System.Drawing.Color.FloralWhite
    Me.grvDocint.Appearance.FocusedCell.Options.UseBackColor = True
    Me.grvDocint.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.hh_tipo, Me.hh_numero, Me.hh_data, Me.hh_codcf, Me.hh_ragsoc})
    Me.grvDocint.Enabled = True
    Me.grvDocint.GridControl = Me.grdDocint
    Me.grvDocint.Name = "grvDocint"
    Me.grvDocint.OptionsCustomization.AllowRowSizing = True
    Me.grvDocint.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvDocint.OptionsNavigation.UseTabKey = False
    Me.grvDocint.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvDocint.OptionsView.ColumnAutoWidth = False
    Me.grvDocint.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvDocint.OptionsView.ShowGroupPanel = False
    '
    'cmdRicerca
    '
    Me.cmdRicerca.Location = New System.Drawing.Point(816, 204)
    Me.cmdRicerca.Name = "cmdRicerca"
    Me.cmdRicerca.Size = New System.Drawing.Size(156, 40)
    Me.cmdRicerca.Text = "Ricerca"
    '
    'chxx_vendutodaterzi
    '
    Me.chxx_vendutodaterzi.Location = New System.Drawing.Point(676, 172)
    Me.chxx_vendutodaterzi.Name = "chxx_vendutodaterzi"
    Me.chxx_vendutodaterzi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.chxx_vendutodaterzi.Properties.Appearance.Options.UseBackColor = True
    Me.chxx_vendutodaterzi.Properties.Appearance.Options.UseTextOptions = True
    Me.chxx_vendutodaterzi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
    Me.chxx_vendutodaterzi.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.chxx_vendutodaterzi.Properties.AutoHeight = False
    Me.chxx_vendutodaterzi.Properties.Caption = "Vend. Terzi"
    Me.chxx_vendutodaterzi.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
    Me.chxx_vendutodaterzi.Properties.PictureChecked = CType(resources.GetObject("chxx_vendutodaterzi.Properties.PictureChecked"), System.Drawing.Image)
    Me.chxx_vendutodaterzi.Properties.PictureUnchecked = CType(resources.GetObject("chxx_vendutodaterzi.Properties.PictureUnchecked"), System.Drawing.Image)
    Me.chxx_vendutodaterzi.Size = New System.Drawing.Size(100, 20)
    '
    'chxx_muletto
    '
    Me.chxx_muletto.Location = New System.Drawing.Point(436, 172)
    Me.chxx_muletto.Name = "chxx_muletto"
    Me.chxx_muletto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.chxx_muletto.Properties.Appearance.Options.UseBackColor = True
    Me.chxx_muletto.Properties.Appearance.Options.UseTextOptions = True
    Me.chxx_muletto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
    Me.chxx_muletto.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.chxx_muletto.Properties.AutoHeight = False
    Me.chxx_muletto.Properties.Caption = "Muletto"
    Me.chxx_muletto.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
    Me.chxx_muletto.Properties.PictureChecked = CType(resources.GetObject("chxx_muletto.Properties.PictureChecked"), System.Drawing.Image)
    Me.chxx_muletto.Properties.PictureUnchecked = CType(resources.GetObject("chxx_muletto.Properties.PictureUnchecked"), System.Drawing.Image)
    Me.chxx_muletto.Size = New System.Drawing.Size(116, 20)
    '
    'chxx_dismesso
    '
    Me.chxx_dismesso.Location = New System.Drawing.Point(556, 172)
    Me.chxx_dismesso.Name = "chxx_dismesso"
    Me.chxx_dismesso.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.chxx_dismesso.Properties.Appearance.Options.UseBackColor = True
    Me.chxx_dismesso.Properties.Appearance.Options.UseTextOptions = True
    Me.chxx_dismesso.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
    Me.chxx_dismesso.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.chxx_dismesso.Properties.AutoHeight = False
    Me.chxx_dismesso.Properties.Caption = "Dismesso"
    Me.chxx_dismesso.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
    Me.chxx_dismesso.Properties.PictureChecked = CType(resources.GetObject("chxx_dismesso.Properties.PictureChecked"), System.Drawing.Image)
    Me.chxx_dismesso.Properties.PictureUnchecked = CType(resources.GetObject("chxx_dismesso.Properties.PictureUnchecked"), System.Drawing.Image)
    Me.chxx_dismesso.Size = New System.Drawing.Size(116, 20)
    '
    'chxx_usato
    '
    Me.chxx_usato.Location = New System.Drawing.Point(316, 172)
    Me.chxx_usato.Name = "chxx_usato"
    Me.chxx_usato.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.chxx_usato.Properties.Appearance.Options.UseBackColor = True
    Me.chxx_usato.Properties.Appearance.Options.UseTextOptions = True
    Me.chxx_usato.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
    Me.chxx_usato.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.chxx_usato.Properties.AutoHeight = False
    Me.chxx_usato.Properties.Caption = "Usato"
    Me.chxx_usato.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
    Me.chxx_usato.Properties.PictureChecked = CType(resources.GetObject("chxx_usato.Properties.PictureChecked"), System.Drawing.Image)
    Me.chxx_usato.Properties.PictureUnchecked = CType(resources.GetObject("chxx_usato.Properties.PictureUnchecked"), System.Drawing.Image)
    Me.chxx_usato.Size = New System.Drawing.Size(116, 20)
    '
    'edxx_duratagranzia
    '
    Me.edxx_duratagranzia.EditValue = ""
    Me.edxx_duratagranzia.Location = New System.Drawing.Point(144, 172)
    Me.edxx_duratagranzia.Name = "edxx_duratagranzia"
    Me.edxx_duratagranzia.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_duratagranzia.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_duratagranzia.Properties.AutoHeight = False
    Me.edxx_duratagranzia.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_duratagranzia.Properties.MaxLength = 65536
    Me.edxx_duratagranzia.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_duratagranzia.Size = New System.Drawing.Size(156, 20)
    '
    'NtsLabel5
    '
    Me.NtsLabel5.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel5.Location = New System.Drawing.Point(24, 172)
    Me.NtsLabel5.Name = "NtsLabel5"
    Me.NtsLabel5.Size = New System.Drawing.Size(116, 20)
    Me.NtsLabel5.Text = "Durata garanzia"
    Me.NtsLabel5.UseMnemonic = False
    '
    'lbxx_descr
    '
    Me.lbxx_descr.BackColor = System.Drawing.Color.Transparent
    Me.lbxx_descr.Location = New System.Drawing.Point(304, 124)
    Me.lbxx_descr.Name = "lbxx_descr"
    Me.lbxx_descr.Size = New System.Drawing.Size(472, 20)
    Me.lbxx_descr.UseMnemonic = False
    '
    'edxx_articolo
    '
    Me.edxx_articolo.EditValue = ""
    Me.edxx_articolo.Location = New System.Drawing.Point(144, 124)
    Me.edxx_articolo.Name = "edxx_articolo"
    Me.edxx_articolo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_articolo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_articolo.Properties.AutoHeight = False
    Me.edxx_articolo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_articolo.Properties.MaxLength = 65536
    Me.edxx_articolo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_articolo.Size = New System.Drawing.Size(156, 20)
    '
    'NtsLabel4
    '
    Me.NtsLabel4.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel4.Location = New System.Drawing.Point(24, 124)
    Me.NtsLabel4.Name = "NtsLabel4"
    Me.NtsLabel4.Size = New System.Drawing.Size(116, 20)
    Me.NtsLabel4.Text = "Articolo"
    Me.NtsLabel4.UseMnemonic = False
    '
    'edxx_matricolaproduttore
    '
    Me.edxx_matricolaproduttore.EditValue = ""
    Me.edxx_matricolaproduttore.Location = New System.Drawing.Point(144, 76)
    Me.edxx_matricolaproduttore.Name = "edxx_matricolaproduttore"
    Me.edxx_matricolaproduttore.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_matricolaproduttore.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_matricolaproduttore.Properties.AutoHeight = False
    Me.edxx_matricolaproduttore.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_matricolaproduttore.Properties.MaxLength = 65536
    Me.edxx_matricolaproduttore.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_matricolaproduttore.Size = New System.Drawing.Size(448, 20)
    '
    'NtsLabel3
    '
    Me.NtsLabel3.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel3.Location = New System.Drawing.Point(24, 76)
    Me.NtsLabel3.Name = "NtsLabel3"
    Me.NtsLabel3.Size = New System.Drawing.Size(116, 20)
    Me.NtsLabel3.Text = "Matricola produttore"
    Me.NtsLabel3.UseMnemonic = False
    '
    'edxx_nuovamatricola
    '
    Me.edxx_nuovamatricola.EditValue = ""
    Me.edxx_nuovamatricola.Location = New System.Drawing.Point(436, 28)
    Me.edxx_nuovamatricola.Name = "edxx_nuovamatricola"
    Me.edxx_nuovamatricola.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_nuovamatricola.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_nuovamatricola.Properties.AutoHeight = False
    Me.edxx_nuovamatricola.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_nuovamatricola.Properties.MaxLength = 65536
    Me.edxx_nuovamatricola.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_nuovamatricola.Size = New System.Drawing.Size(156, 20)
    '
    'NtsLabel2
    '
    Me.NtsLabel2.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel2.Location = New System.Drawing.Point(332, 28)
    Me.NtsLabel2.Name = "NtsLabel2"
    Me.NtsLabel2.Size = New System.Drawing.Size(100, 20)
    Me.NtsLabel2.Text = "Nuova matricola"
    Me.NtsLabel2.UseMnemonic = False
    '
    'cmdCambia
    '
    Me.cmdCambia.Location = New System.Drawing.Point(620, 28)
    Me.cmdCambia.Name = "cmdCambia"
    Me.cmdCambia.Size = New System.Drawing.Size(156, 26)
    Me.cmdCambia.Text = "Cambia"
    '
    'NtsLabel1
    '
    Me.NtsLabel1.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel1.Location = New System.Drawing.Point(24, 28)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.Size = New System.Drawing.Size(116, 20)
    Me.NtsLabel1.Text = "Matricola"
    Me.NtsLabel1.UseMnemonic = False
    '
    'edxx_matricola
    '
    Me.edxx_matricola.EditValue = ""
    Me.edxx_matricola.Location = New System.Drawing.Point(144, 28)
    Me.edxx_matricola.Name = "edxx_matricola"
    Me.edxx_matricola.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_matricola.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_matricola.Properties.AutoHeight = False
    Me.edxx_matricola.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_matricola.Properties.MaxLength = 65536
    Me.edxx_matricola.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_matricola.Size = New System.Drawing.Size(156, 20)
    '
    'FRMHHMMTR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(1015, 511)
    Me.Controls.Add(Me.NtsPanel1)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMHHMMTR"
    Me.Text = "ANAGRAFICA MATRICOLE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel1.ResumeLayout(False)
    CType(Me.NtsTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabControl1.ResumeLayout(False)
    Me.NtsTabPage1.ResumeLayout(False)
    Me.NtsTabPage1.PerformLayout()
    CType(Me.CLIENTI, System.ComponentModel.ISupportInitialize).EndInit()
    Me.CLIENTI.ResumeLayout(False)
    CType(Me.grdClienti, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvClienti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.NtsGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox1.ResumeLayout(False)
    CType(Me.grdDoctutti, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvDoctutti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage3.ResumeLayout(False)
    CType(Me.NtsGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox2.ResumeLayout(False)
    CType(Me.grdDocint, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvDocint, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.chxx_vendutodaterzi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.chxx_muletto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.chxx_dismesso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.chxx_usato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_duratagranzia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_articolo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_matricolaproduttore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_nuovamatricola.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_matricola.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
  Public oCleHh As CLEHHMMTR
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

        grvClienti.NTSSetParam(oMenu, oApp.Tr(Me, 132088673145756309, "Griglia Clienti"))
        'ck_vendaterzi.NTSSetParam(oMenu, oApp.Tr(Me, 132088673145956194, "Vend.Terzi"), "1", "0")
        'ck_muletto.NTSSetParam(oMenu, oApp.Tr(Me, 132088673145966189, "Muletto"), "1", "0")
        'ck_dismesso.NTSSetParam(oMenu, oApp.Tr(Me, 132088673145976183, "Dismesso"), "1", "0")
        'ck_usato.NTSSetParam(oMenu, oApp.Tr(Me, 132088673145986177, "Usato"), "1", "0")

        Try
          Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
          tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\New.png")
          tlbApri.GlyphPath = (oApp.ChildImageDir & "\open.png")
          tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.png")
          tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.png")
          tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.png")
          tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.png")
          tlbRecordCancella.GlyphPath = (oApp.ChildImageDir & "\recdelete.png")
          tlbRecordRipristina.GlyphPath = (oApp.ChildImageDir & "\recrestore.png")
          tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.png")
          tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.png")
          tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.png")
          tlbStampaPdf.GlyphPath = (oApp.ChildImageDir & "\pdf.png")
          tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.png")
          tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.png")
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

      edxx_matricola.NTSDbField = "XXX.xx_matricola"
      chxx_vendutodaterzi.NTSText.NTSDbField = "XXX.xx_vendutodaterzi"
      chxx_muletto.NTSText.NTSDbField = "XXX.xx_muletto"
      chxx_dismesso.NTSText.NTSDbField = "XXX.xx_dimesso"
      chxx_usato.NTSText.NTSDbField = "XXX.xx_usato"
      edxx_duratagranzia.NTSDbField = "XXX.xx_duratagaranzia"
      edxx_articolo.NTSDbField = "XXX.xx_articolo"
      edxx_matricolaproduttore.NTSDbField = "XXX.xx_matricolaproduttore"
      edxx_nuovamatricola.NTSDbField = "XXX.xx_nuovamatricola"
      lbxx_descr.NTSDbField = "XXX.xx_descr"

      edxx_articolo.NTSSetParamTabe(oMenu, oApp.Tr(Me, 132088673146006166, "Cod.articolo"), tabartico, True)
      'edxx_articolo.NTSSetParam(oMenu, oApp.Tr(Me, 132088673146006166, "Cod.articolo"), 0)
      'edxx_articolo.NTSSetParamZoom("ZOOMARTICO")
      edxx_matricola.NTSSetParam(oMenu, oApp.Tr(Me, 132088673146056137, "Matricola"), 50)
      edxx_matricola.NTSSetParamZoom("FRMZOOMTR")
      chxx_vendutodaterzi.NTSSetParam(oMenu, oApp.Tr(Me, 132157679288462642, "Vend. Terzi"), "1", "0")
      chxx_muletto.NTSSetParam(oMenu, oApp.Tr(Me, 132157679288472634, "Muletto"), "1", "0")
      chxx_dismesso.NTSSetParam(oMenu, oApp.Tr(Me, 132157679288482632, "Dimesso"), "1", "0")
      chxx_usato.NTSSetParam(oMenu, oApp.Tr(Me, 132157679288492630, "Usato"), "1", "0")
      edxx_duratagranzia.NTSSetParam(oMenu, oApp.Tr(Me, 132157679288502621, "duratagaranzia"), 0)
      edxx_matricolaproduttore.NTSSetParam(oMenu, oApp.Tr(Me, 132157679288552579, "Matricola produttore"), 0)
      edxx_nuovamatricola.NTSSetParam(oMenu, oApp.Tr(Me, 132157679288572560, "Nuova matricola"), 0)

      grvClienti.NTSSetParam(oMenu, oApp.Tr(Me, 131881231461336930, "Griglia clienti"))

      NTSFormAddDataBinding(dcHh, Me)
      '-------------------------------------------------
      'per agganciare al dataset i vari controlli

    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub

  Private Sub FRMHHMMTR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    dsHh = oCleHh.dsShared
    dcHh = Nothing
    dcHh = New BindingSource()
    dcHh.DataSource = dsHh.Tables("XXX")
    InitControls()
    Bindcontrols()
    GctlSetRoules()
    GctlApplicaDefaultValue()

    'edxx_matricolaproduttore.Enabled = False
    'edxx_duratagranzia.Enabled = False
    'chxx_dismesso.Enabled = False
    'chxx_muletto.Enabled = False
    'chxx_usato.Enabled = False
    'chxx_vendutodaterzi.Enabled = False

    If Not oCallParams Is Nothing Then
      oCleHh.drxxx!xx_matricola = oCallParams.dPar1
    End If
  End Sub



  Public Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)

    If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non è per questa form ...
    MyBase.GestisciEventiEntity(sender, e)
    Try
      If e.TipoEvento.Length >= 5 Then
        If Mid(e.TipoEvento, 1, 4) = "CPNE" Then
          Select Case e.TipoEvento
            Case "CPNE.AggiornaGrigliaClienti"
              CPNEAggGriglia()
          End Select
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub


  Private Sub CPNEAggGriglia()

    'hhtlav - Esegui bind
    dcHhGr = Nothing
    dcHhGr = New BindingSource()

    'puntatore riga tabella virtuale della griglia
    dcHhGr.DataSource = dsHh.Tables("XXX")
    grdClienti.DataSource = dcHhGr

    'proprietà principali
    grvClienti.NTSAllowDelete = False
    grvClienti.NTSAllowInsert = False
    grvClienti.Enabled = True


  End Sub

  Private Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim ctrlTmp As Control = NTSFindControlForZoom()
    If ctrlTmp Is Nothing Then Return
    Dim oParam As New CLE__CLDP

    If ctrlTmp.Name = edxx_matricola.Name Then
      oMenu.RunChild("NTSInformatica", "FRMZOOMTR", "Zoom matricole", DittaCorrente, "", "BNHHMMTR", oParam, "", True, True)
      oCleHh.drxxx!xx_matricola = oParam.strPar1
      edxx_matricola.Text = oParam.strPar1
    Else
      NTSCallStandardZoom()
    End If
    ValidaLastControl()
  End Sub

  Private Sub tlbRecordCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordCancella.ItemClick
    oCleHh.CPNECancRiga(grvClienti.NTSGetCurrentDataRow)
  End Sub

  Private Sub tlbRecordRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordRipristina.ItemClick
    oCleHh.CPNERipristinaRiga(grvClienti.NTSGetCurrentDataRow)
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

    'StrWhere = "{testord.codditt} = " & CStrSQL(oCleHh.strDittaCorrente) & " And {testord.td_datord} >= " & ConvStrRpt(oCleHh.drxxx!xx_dadtord.ToString) & " And {testord.td_datord} <= " & ConvStrRpt(oCleHh.drxxx!xx_adtord.ToString)
    'StrWhere += " And {movord.mo_datcons} >= " & ConvStrRpt(oCleHh.drxxx!xx_dadtcons.ToString) & " And {movord.mo_datcons} <= " & ConvStrRpt(oCleHh.drxxx!xx_adtcons.ToString)
    'If oCleHh.drxxx!xx_soloap.ToString = "S" Then
    '  StrWhere += " And {movord.mo_flevas} = 'C'"
    'End If

    'Dim nPjob As Object
    'nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSHHDORO", "REPORTS1", " ", 0, nDestin, "BSHHDORO.rpt", False, "Stampa", False)
    'If Not (nPjob Is Nothing) Then
    '  'lancio tutti gli eventuali reports (gestisce già il multireport)												
    '  For i = 1 To UBound(CType(nPjob, Array), 2)
    '    oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), StrWhere))
    '    oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
    '  Next
    'End If
  End Sub



  Private Sub FRMHHMMTR_ActivatedFirst(sender As Object, e As EventArgs) Handles Me.ActivatedFirst

    'oCleHh.CPNERicerca()
    'CPNEAggGriglia()

  End Sub


  Private Sub tlbSalva_ItemClick(sender As Object, e As ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      ValidaLastControl()
      If edxx_matricola.Text = "" Then
        oApp.MsgBoxErr("Prima inserire la matricola")
      Else
        If chxx_vendutodaterzi.Checked Then
          dsHh.Tables("XXX").Rows(0)!hh_vendutodaterzi = 1
        Else
          dsHh.Tables("XXX").Rows(0)!hh_vendutodaterzi = 0
        End If
        If chxx_muletto.Checked Then
          dsHh.Tables("XXX").Rows(0)!hh_muletto = 1
        Else
          dsHh.Tables("XXX").Rows(0)!hh_muletto = 0
        End If

        If chxx_dismesso.Checked Then
          dsHh.Tables("XXX").Rows(0)!hh_dismesso = 1
        Else
          dsHh.Tables("XXX").Rows(0)!hh_dismesso = 0
        End If

        If chxx_usato.Checked Then
          dsHh.Tables("XXX").Rows(0)!hh_usato = 1
        Else
          dsHh.Tables("XXX").Rows(0)!hh_usato = 0
        End If

        oCleHh.CPNEValidaRiga(dsHh.Tables("XXX").Rows(0))

      End If
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub

  Private Sub tlbNuovo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      ValidaLastControl()
      edxx_matricola.Text = ""
      chxx_vendutodaterzi.Checked = False
      chxx_muletto.Checked = False
      chxx_dismesso.Checked = False
      chxx_usato.Checked = False
      edxx_duratagranzia.Text = ""
      edxx_articolo.Text = ""
      edxx_matricolaproduttore.Text = ""
      edxx_nuovamatricola.Text = ""
      lbxx_descr.Text = ""
      oCleHh.CPNESvuotaGriglia()
      CPNEAggGriglia()
    Catch ex As Exception
      '--------------------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '--------------------------------------------------------------
    End Try
  End Sub

  Private Sub grvClienti_NTSFocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles grvClienti.NTSFocusedRowChanged
    Try

      'oCleHh.CPNERowchanged(grvClienti.NTSGetCurrentDataRow)

      Dim drClienti As DataRow
      drClienti = grvClienti.NTSGetCurrentDataRow()

      'oCleHh.CPNEAssociaDTXXX(drClienti)

      edxx_matricola.Text = drClienti!hh_matricola.ToString
      edxx_matricolaproduttore.Text = drClienti!hh_matrproduttore.ToString
      edxx_articolo.Text = drClienti!hh_codart.ToString

      If CInt(drClienti!hh_dismesso) = 0 Then
        chxx_dismesso.Checked = False
      Else
        chxx_dismesso.Checked = True
      End If
      If CInt(drClienti!hh_muletto) = 0 Then
        chxx_muletto.Checked = False
      Else
        chxx_muletto.Checked = True
      End If
      If CInt(drClienti!hh_usato) = 0 Then
        chxx_usato.Checked = False
      Else
        chxx_usato.Checked = True
      End If
      If CInt(drClienti!hh_vendutodaterzi) = 0 Then
        chxx_vendutodaterzi.Checked = False
      Else
        chxx_vendutodaterzi.Checked = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub


  Private Sub cmdRicerca_Click(sender As Object, e As EventArgs) Handles cmdRicerca.Click
    Try
      If edxx_matricola.Text = "" And edxx_articolo.Text = "" Then
        MsgBox("Scrivere il codice della matricola o dell'articolo")
        edxx_matricola.Focus()
      End If

      ' ValidaLastControl()
      oCleHh.CPNERicerca(edxx_matricola.Text, edxx_articolo.Text)
      CPNEAggGriglia()
      Dim drMatricole As DataRow
      drMatricole = grvClienti.NTSGetCurrentDataRow()
      If Not drMatricole Is Nothing Then

        edxx_matricola.Text = drMatricole!hh_matricola.ToString
        edxx_matricolaproduttore.Text = drMatricole!hh_matrproduttore.ToString
        edxx_articolo.Text = drMatricole!hh_codart.ToString
        edxx_duratagranzia.Text = drMatricole!hh_duratagaranzia.ToString
        lbxx_descr.Text = drMatricole!xx_descr.ToString
        If CInt(drMatricole!hh_dismesso) = 0 Then
          chxx_dismesso.Checked = False
        Else
          chxx_dismesso.Checked = True
        End If
        If CInt(drMatricole!hh_muletto) = 0 Then
          chxx_muletto.Checked = False
        Else
          chxx_muletto.Checked = True
        End If
        If CInt(drMatricole!hh_usato) = 0 Then
          chxx_usato.Checked = False
        Else
          chxx_usato.Checked = True
        End If
        If CInt(drMatricole!hh_vendutodaterzi) = 0 Then
          chxx_vendutodaterzi.Checked = False
        Else
          chxx_vendutodaterzi.Checked = True
        End If
      Else
        MsgBox("Nessuna matricola esistente")
      End If
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub

  Private Sub cmdPulisci_Click(sender As Object, e As EventArgs)
    Try
      edxx_matricola.Text = ""
      chxx_vendutodaterzi.Checked = False
      chxx_muletto.Checked = False
      chxx_dismesso.Checked = False
      chxx_usato.Checked = False
      edxx_duratagranzia.Text = ""
      edxx_articolo.Text = ""
      edxx_matricolaproduttore.Text = ""
      edxx_nuovamatricola.Text = ""
      lbxx_descr.Text = ""
      oCleHh.CPNESvuotaGriglia()
      CPNEAggGriglia()
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub

  Private Sub cmdCambia_Click(sender As Object, e As EventArgs) Handles cmdCambia.Click
    Try
      oCleHh.CPNECambiaMatricola(edxx_matricola.Text, edxx_nuovamatricola.Text)
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub

  Private Sub tlbCancella_ItemClick(sender As Object, e As ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      ValidaLastControl()
      oCleHh.CPNECancRiga(dsHh.Tables("XXX").Rows(0))
      edxx_matricola.Text = ""
      chxx_vendutodaterzi.Checked = False
      chxx_muletto.Checked = False
      chxx_dismesso.Checked = False
      chxx_usato.Checked = False
      edxx_duratagranzia.Text = ""
      edxx_articolo.Text = ""
      edxx_matricolaproduttore.Text = ""
      edxx_nuovamatricola.Text = ""
      lbxx_descr.Text = ""
      oCleHh.CPNESvuotaGriglia()
      CPNEAggGriglia()
    Catch ex As Exception
      '--------------------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '--------------------------------------------------------------
    End Try
  End Sub
End Class