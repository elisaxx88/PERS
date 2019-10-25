Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO
Imports DevExpress.XtraBars

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
    If oCleHh.Init(oApp, NTSScript, oMenu.oCleComm, "", False, "", "") = False Then Return False

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
    Me.NtsPanel1 = New NTSInformatica.NTSPanel()
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.tlbNuovo.Id = 0
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = False
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
    Me.tlbSalva.Visible = False
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
    Me.tlbCancella.Visible = False
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
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNavDoc, True), New DevExpress.XtraBars.LinkPersistInfo(Me.TlbSelTutto), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDeselTutto)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbNavDoc
    '
    Me.tlbNavDoc.Caption = "Navigazione Documentale"
    Me.tlbNavDoc.Id = 104
    Me.tlbNavDoc.Name = "tlbNavDoc"
    Me.tlbNavDoc.NTSIsCheckBox = False
    Me.tlbNavDoc.Visible = True
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
    Me.barDockControlTop.Size = New System.Drawing.Size(995, 35)
    '
    'barDockControlBottom
    '
    Me.barDockControlBottom.CausesValidation = False
    Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.barDockControlBottom.Location = New System.Drawing.Point(0, 388)
    Me.barDockControlBottom.Size = New System.Drawing.Size(995, 0)
    '
    'barDockControlLeft
    '
    Me.barDockControlLeft.CausesValidation = False
    Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
    Me.barDockControlLeft.Location = New System.Drawing.Point(0, 35)
    Me.barDockControlLeft.Size = New System.Drawing.Size(0, 353)
    '
    'barDockControlRight
    '
    Me.barDockControlRight.CausesValidation = False
    Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
    Me.barDockControlRight.Location = New System.Drawing.Point(995, 35)
    Me.barDockControlRight.Size = New System.Drawing.Size(0, 353)
    '
    'NtsPanel1
    '
    Me.NtsPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.NtsPanel1.Location = New System.Drawing.Point(0, 35)
    Me.NtsPanel1.Name = "NtsPanel1"
    Me.NtsPanel1.Size = New System.Drawing.Size(995, 353)
    '
    'FRMHHMMTR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(995, 388)
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


        'ckxx_soloap.NTSSetParam(oMenu, oApp.Tr(Me, 131723177460309382, "Solo Aperti"), "S", "N")
        'edxx_adtord.NTSSetParam(oMenu, oApp.Tr(Me, 131723177460611585, "a data ordine"), False)
        'edxx_adtcons.NTSSetParam(oMenu, oApp.Tr(Me, 131723177460713571, "a data consegna"), False)
        'edxx_dadtord.NTSSetParam(oMenu, oApp.Tr(Me, 131723177460713645, "da data ordine"), False)
        'edxx_dadtcons.NTSSetParam(oMenu, oApp.Tr(Me, 131723177460816776, "da data consegna"), False)
        'edxx_aconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 131729158052706411, "Da Conto"), tabanagrac)
        'edxx_daconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 131729158053018886, "A Conto"), tabanagrac)
        'edxx_dtfatt.NTSSetParam(oMenu, oApp.Tr(Me, 131729186096607772, "Data fattura"), False)

        'grvricerca.NTSSetParam(oMenu, oApp.Tr(Me, 131723177446392896, "Ricerca"))
        'xx_sel.NTSSetParamCHK(oMenu, oApp.Tr(Me, 131723190940602511, "Seleziona"), "S", "N")
        'an_descr1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131723190940662650, "Cliente"), 0, True)
        'td_datord.NTSSetParamDATA(oMenu, oApp.Tr(Me, 131723190940662865, "Data Ord"), True)
        'mo_datcons.NTSSetParamDATA(oMenu, oApp.Tr(Me, 131723190940702900, "Data consegna"), True)
        'mo_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131723190940762930, "Articolo"), 0, True)
        'mo_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131723190940763126, "Desr. Articolo"), 0, True)
        'mo_valoremm.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131723190941267068, "Valore riga"), "#,##0.00", 15)
        'mo_desint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131723190941376149, "Descr Int."), 0, True)
        'mo_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131723190941376810, "note"), 0, True)
        'mo_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131723190941411934, "Anno"), "0", 4, 0, 9999)
        'mo_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131723190941477054, "Serie"), 0, True)
        'mo_numord.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131723190941477705, "Num Ord"), "0", 9, 0, 999999999)
        'mo_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131723190941522818, "Riga"), "0", 9, 0, 999999999)
        'mo_flevas.NTSSetParamCHK(oMenu, oApp.Tr(Me, 131723190941577921, "Evaso"), "S", "C")
        'td_riferim.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131723190941578181, "Rif"), 0, True)
        'grvricerca.NTSMenuContext = tlbStrumenti

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
      'ckxx_soloap.NTSText.NTSDbField = "XXX.xx_soloap"
      'edxx_adtord.NTSDbField = "XXX.xx_adtord"
      'edxx_adtcons.NTSDbField = "XXX.xx_adtcons"
      'edxx_dadtord.NTSDbField = "XXX.xx_dadtord"
      'edxx_dadtcons.NTSDbField = "XXX.xx_dadtcons"
      'edxx_aconto.NTSDbField = "XXX.xx_aconto"
      'edxx_daconto.NTSDbField = "XXX.xx_daconto"
      'edxx_dtfatt.NTSDbField = "XXX.xx_dtfatt"
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
    'grricerca.DataSource = dcHhGr
    'grvricerca.NTSAllowDelete = False
    'grvricerca.NTSAllowInsert = False
    'grvricerca.Enabled = True
    'For Each col As NTSGridColumn In grvricerca.Columns
    '  If col.Name = "xx_sel" Then
    '    col.Enabled = True
    '  Else
    '    col.Enabled = False
    '  End If
    'Next
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

  'Private Sub tlbRecordCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordCancella.ItemClick
  '  oCleHh.CPNECancRiga(grvricerca.NTSGetCurrentDataRow)
  'End Sub

  'Private Sub tlbRecordRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordRipristina.ItemClick
  '  oCleHh.CPNERipristinaRiga(grvricerca.NTSGetCurrentDataRow)
  'End Sub

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

  Private Sub CmdRicerca_Click(sender As Object, e As EventArgs)
    oCleHh.CPNERicerca()
    'If ckxx_soloap.Checked Then
    '  CmdElabora.Enabled = True
    'Else
    '  CmdElabora.Enabled = False
    'End If
  End Sub

  Private Sub FRMHHDORO_ActivatedFirst(sender As Object, e As EventArgs) Handles Me.ActivatedFirst
    CPNEAggGrilgia()

  End Sub

  Private Sub CmdElabora_Click(sender As Object, e As EventArgs)
    If oCleHh.CPNEGeneraDocumento Then

    End If
  End Sub

  Private Sub tlbNavDoc_ItemClick(sender As Object, e As ItemClickEventArgs) Handles tlbNavDoc.ItemClick
    'Dim Dr As DataRow = grvricerca.NTSGetCurrentDataRow
    'If IsNothing(Dr) Then
    '  oApp.MsgBoxInfo("Prima posizionarsi su una riga")
    '  Return
    'End If
    'Dim strParam As String = "APRI;" & Dr!mo_tipork.ToString & ";" &
    '            Dr!mo_anno.ToString & ";" &
    '            Dr!mo_serie.ToString & ";" &
    '            Microsoft.VisualBasic.Right(NTSCInt(Dr!mo_numord.ToString).ToString.PadLeft(9, "0"c), 9) & ";" &
    '            Microsoft.VisualBasic.Right(NTSCInt(Dr!td_conto).ToString.PadLeft(9, "0"c), 9) & ";" &
    '            Microsoft.VisualBasic.Right("          " & NTSCDate(Dr!td_datord.ToString).ToString("dd/MM/yyyy"), 10) &
    '            ";000000000;0000;0000; ;000000000;0000;1"
    'oMenu.RunChild("BS__FLDO", "CLS__FLDO", oApp.Tr(Me, 128154847220982619, "Navigazione Documentale"), DittaCorrente, "", "", Nothing, strParam, True, True)

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

  Private Sub ckxx_soloap_CheckedChanged(sender As Object, e As EventArgs)

  End Sub
End Class