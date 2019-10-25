Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO
Imports System.Windows.Forms
Public Class FRMDatiAgg
#Region "Standard"


  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = bsModExtAll
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Private ModuliPtnExt_P As Integer = 0


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
    Dim strErr As String = ""
    Return True
  End Function
  Public Overridable Sub InitControls()
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try

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
  Public Overridable Sub InitEntity(ByRef cleSt As CLEORGSOR)
    oCleHh = cleSt
    AddHandler oCleHh.RemoteEvent, AddressOf GestisciEventiEntity
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

  Public Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMDatiAgg))
    Me.edhh_scpian = New NTSInformatica.NTSTextBoxStr()
    Me.NtsLabel18 = New NTSInformatica.NTSLabel()
    Me.edhh_rispon = New NTSInformatica.NTSTextBoxStr()
    Me.NtsLabel17 = New NTSInformatica.NTSLabel()
    Me.NtsLabel16 = New NTSInformatica.NTSLabel()
    Me.lbxx_tipobf = New NTSInformatica.NTSLabel()
    Me.edet_tipobf = New NTSInformatica.NTSTextBoxNum()
    Me.NtsLabel15 = New NTSInformatica.NTSLabel()
    Me.lbxx_teldestdiv = New NTSInformatica.NTSLabel()
    Me.lbxx_inddestdiv = New NTSInformatica.NTSLabel()
    Me.lbxx_coddest = New NTSInformatica.NTSLabel()
    Me.edet_coddest = New NTSInformatica.NTSTextBoxNum()
    Me.NtsLabel14 = New NTSInformatica.NTSLabel()
    Me.edhh_dacocl = New NTSInformatica.NTSTextBoxData()
    Me.NtsLabel13 = New NTSInformatica.NTSLabel()
    Me.edet_totdoc = New NTSInformatica.NTSTextBoxNum()
    Me.NtsLabel12 = New NTSInformatica.NTSLabel()
    Me.edet_datdoc = New NTSInformatica.NTSTextBoxData()
    Me.NtsLabel11 = New NTSInformatica.NTSLabel()
    Me.lbxx_conto = New NTSInformatica.NTSLabel()
    Me.lbxx_telef = New NTSInformatica.NTSLabel()
    Me.NtsLabel9 = New NTSInformatica.NTSLabel()
    Me.NtsLabel8 = New NTSInformatica.NTSLabel()
    Me.edet_conto = New NTSInformatica.NTSTextBoxNum()
    Me.edet_numdoc = New NTSInformatica.NTSTextBoxNum()
    Me.edet_datcons = New NTSInformatica.NTSTextBoxData()
    Me.NtsLabel7 = New NTSInformatica.NTSLabel()
    Me.NtsLabel6 = New NTSInformatica.NTSLabel()
    Me.edhh_chiedi = New NTSInformatica.NTSTextBoxStr()
    Me.NtsLabel4 = New NTSInformatica.NTSLabel()
    Me.grScaPag = New NTSInformatica.NTSGrid()
    Me.grvScaPag = New NTSInformatica.NTSGridView()
    Me.hh_codpaga = New NTSInformatica.NTSGridColumn()
    Me.xx_despaga = New NTSInformatica.NTSGridColumn()
    Me.hh_imppaga = New NTSInformatica.NTSGridColumn()
    Me.hh_datpaga = New NTSInformatica.NTSGridColumn()
    Me.hh_flgpaga = New NTSInformatica.NTSGridColumn()
    Me.hh_numft = New NTSInformatica.NTSGridColumn()
    Me.hh_serieft = New NTSInformatica.NTSGridColumn()
    Me.hh_dataft = New NTSInformatica.NTSGridColumn()
    Me.hh_flgft = New NTSInformatica.NTSGridColumn()
    Me.lbxx_diff = New NTSInformatica.NTSLabel()
    Me.NtsLabel5 = New NTSInformatica.NTSLabel()
    Me.lbxx_marg = New NTSInformatica.NTSLabel()
    Me.lbxx_ricavo = New NTSInformatica.NTSLabel()
    Me.lbxx_costo = New NTSInformatica.NTSLabel()
    Me.NtsLabel3 = New NTSInformatica.NTSLabel()
    Me.NtsLabel2 = New NTSInformatica.NTSLabel()
    Me.NtsLabel1 = New NTSInformatica.NTSLabel()
    Me.grTotFor = New NTSInformatica.NTSGrid()
    Me.grvTotFor = New NTSInformatica.NTSGridView()
    Me.xx_codfor = New NTSInformatica.NTSGridColumn()
    Me.xx_desfor = New NTSInformatica.NTSGridColumn()
    Me.xx_valfor = New NTSInformatica.NTSGridColumn()
    Me.edxx_valresiduo = New NTSInformatica.NTSTextBoxNum()
    Me.NtsLabel25 = New NTSInformatica.NTSLabel()
    Me.cmdCancellaRiga = New NTSInformatica.NTSButton()
    Me.edhh_ttmont = New NTSInformatica.NTSTextBoxStr()
    Me.NtsLabel22 = New NTSInformatica.NTSLabel()
    Me.ckhh_finitu = New NTSInformatica.NTSRadioButton()
    Me.ckhh_mont = New NTSInformatica.NTSRadioButton()
    Me.edhh_datint = New NTSInformatica.NTSTextBoxData()
    Me.NtsLabel21 = New NTSInformatica.NTSLabel()
    Me.lbxx_squadra = New NTSInformatica.NTSLabel()
    Me.edhh_squadra = New NTSInformatica.NTSTextBoxStr()
    Me.NtsLabel20 = New NTSInformatica.NTSLabel()
    Me.edhh_autoca = New NTSInformatica.NTSTextBoxStr()
    Me.NtsLabel19 = New NTSInformatica.NTSLabel()
    Me.ckhh_finanz = New NTSInformatica.NTSCheckBox()
    Me.ckhh_elettr = New NTSInformatica.NTSCheckBox()
    Me.ckhh_ascens = New NTSInformatica.NTSCheckBox()
    Me.lbxx_codagen2 = New NTSInformatica.NTSLabel()
    Me.edet_codagen2 = New NTSInformatica.NTSTextBoxStr()
    Me.NtsLabel24 = New NTSInformatica.NTSLabel()
    Me.lbxx_codagen = New NTSInformatica.NTSLabel()
    Me.edet_codagen = New NTSInformatica.NTSTextBoxStr()
    Me.NtsLabel23 = New NTSInformatica.NTSLabel()
    Me.NtsFlowLayoutPanel1 = New NTSInformatica.NTSFlowLayoutPanel()
    Me.NtsGroupBox1 = New NTSInformatica.NTSGroupBox()
    Me.NtsGroupBox2 = New NTSInformatica.NTSGroupBox()
    Me.NtsFlowLayoutPanel2 = New NTSInformatica.NTSFlowLayoutPanel()
    Me.NtsGroupBox4 = New NTSInformatica.NTSGroupBox()
    Me.NtsGroupBox3 = New NTSInformatica.NTSGroupBox()
    Me.NtsGroupBox6 = New NTSInformatica.NTSGroupBox()
    Me.NtsPanel3 = New NTSInformatica.NTSPanel()
    Me.NtsLabel10 = New NTSInformatica.NTSLabel()
    Me.NtsPanel1 = New NTSInformatica.NTSPanel()
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edhh_scpian.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edhh_rispon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edet_tipobf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edet_coddest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edhh_dacocl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edet_totdoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edet_datdoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edet_conto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edet_numdoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edet_datcons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edhh_chiedi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grScaPag, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvScaPag, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grTotFor, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvTotFor, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_valresiduo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edhh_ttmont.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckhh_finitu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckhh_mont.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edhh_datint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edhh_squadra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edhh_autoca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckhh_finanz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckhh_elettr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckhh_ascens.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edet_codagen2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edet_codagen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsFlowLayoutPanel1.SuspendLayout()
    CType(Me.NtsGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox1.SuspendLayout()
    CType(Me.NtsGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox2.SuspendLayout()
    Me.NtsFlowLayoutPanel2.SuspendLayout()
    CType(Me.NtsGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox4.SuspendLayout()
    CType(Me.NtsGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox3.SuspendLayout()
    CType(Me.NtsGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox6.SuspendLayout()
    CType(Me.NtsPanel3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel3.SuspendLayout()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'edhh_scpian
    '
    Me.edhh_scpian.Cursor = System.Windows.Forms.Cursors.Default
    Me.edhh_scpian.Location = New System.Drawing.Point(76, 24)
    Me.edhh_scpian.Name = "edhh_scpian"
    Me.edhh_scpian.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edhh_scpian.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edhh_scpian.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edhh_scpian.Properties.MaxLength = 65536
    Me.edhh_scpian.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edhh_scpian.Size = New System.Drawing.Size(188, 20)
    '
    'NtsLabel18
    '
    Me.NtsLabel18.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel18.Location = New System.Drawing.Point(4, 24)
    Me.NtsLabel18.Name = "NtsLabel18"
    Me.NtsLabel18.Size = New System.Drawing.Size(68, 20)
    Me.NtsLabel18.Text = "Scala/Piano"
    '
    'edhh_rispon
    '
    Me.edhh_rispon.Cursor = System.Windows.Forms.Cursors.Default
    Me.edhh_rispon.Location = New System.Drawing.Point(76, 72)
    Me.edhh_rispon.Name = "edhh_rispon"
    Me.edhh_rispon.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edhh_rispon.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edhh_rispon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edhh_rispon.Properties.MaxLength = 65536
    Me.edhh_rispon.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edhh_rispon.Size = New System.Drawing.Size(188, 20)
    '
    'NtsLabel17
    '
    Me.NtsLabel17.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel17.Location = New System.Drawing.Point(4, 72)
    Me.NtsLabel17.Name = "NtsLabel17"
    Me.NtsLabel17.Size = New System.Drawing.Size(56, 20)
    Me.NtsLabel17.Text = "Risponde"
    '
    'NtsLabel16
    '
    Me.NtsLabel16.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel16.Location = New System.Drawing.Point(4, 48)
    Me.NtsLabel16.Name = "NtsLabel16"
    Me.NtsLabel16.Size = New System.Drawing.Size(68, 20)
    Me.NtsLabel16.Text = "Chiedere di"
    '
    'lbxx_tipobf
    '
    Me.lbxx_tipobf.BackColor = System.Drawing.Color.Transparent
    Me.lbxx_tipobf.Location = New System.Drawing.Point(184, 216)
    Me.lbxx_tipobf.Name = "lbxx_tipobf"
    Me.lbxx_tipobf.Size = New System.Drawing.Size(264, 20)
    Me.lbxx_tipobf.Text = "NtsLabel16"
    '
    'edet_tipobf
    '
    Me.edet_tipobf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edet_tipobf.Location = New System.Drawing.Point(88, 216)
    Me.edet_tipobf.Name = "edet_tipobf"
    Me.edet_tipobf.Properties.Appearance.Options.UseTextOptions = True
    Me.edet_tipobf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edet_tipobf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edet_tipobf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edet_tipobf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edet_tipobf.Properties.MaxLength = 65536
    Me.edet_tipobf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edet_tipobf.Size = New System.Drawing.Size(88, 20)
    '
    'NtsLabel15
    '
    Me.NtsLabel15.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel15.Location = New System.Drawing.Point(4, 216)
    Me.NtsLabel15.Name = "NtsLabel15"
    Me.NtsLabel15.Size = New System.Drawing.Size(88, 20)
    Me.NtsLabel15.Text = "Tipo bolla/fatt."
    '
    'lbxx_teldestdiv
    '
    Me.lbxx_teldestdiv.BackColor = System.Drawing.Color.Transparent
    Me.lbxx_teldestdiv.Location = New System.Drawing.Point(4, 144)
    Me.lbxx_teldestdiv.Name = "lbxx_teldestdiv"
    Me.lbxx_teldestdiv.Size = New System.Drawing.Size(172, 20)
    Me.lbxx_teldestdiv.Text = "NtsLabel15"
    '
    'lbxx_inddestdiv
    '
    Me.lbxx_inddestdiv.BackColor = System.Drawing.Color.Transparent
    Me.lbxx_inddestdiv.Location = New System.Drawing.Point(188, 144)
    Me.lbxx_inddestdiv.Name = "lbxx_inddestdiv"
    Me.lbxx_inddestdiv.Size = New System.Drawing.Size(260, 20)
    Me.lbxx_inddestdiv.Text = "NtsLabel15"
    '
    'lbxx_coddest
    '
    Me.lbxx_coddest.BackColor = System.Drawing.Color.Transparent
    Me.lbxx_coddest.Location = New System.Drawing.Point(188, 120)
    Me.lbxx_coddest.Name = "lbxx_coddest"
    Me.lbxx_coddest.Size = New System.Drawing.Size(260, 20)
    Me.lbxx_coddest.Text = "NtsLabel15"
    '
    'edet_coddest
    '
    Me.edet_coddest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edet_coddest.Location = New System.Drawing.Point(88, 120)
    Me.edet_coddest.Name = "edet_coddest"
    Me.edet_coddest.Properties.Appearance.Options.UseTextOptions = True
    Me.edet_coddest.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edet_coddest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edet_coddest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edet_coddest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edet_coddest.Properties.MaxLength = 65536
    Me.edet_coddest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edet_coddest.Size = New System.Drawing.Size(88, 20)
    '
    'NtsLabel14
    '
    Me.NtsLabel14.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel14.Location = New System.Drawing.Point(4, 120)
    Me.NtsLabel14.Name = "NtsLabel14"
    Me.NtsLabel14.Size = New System.Drawing.Size(60, 20)
    Me.NtsLabel14.Text = "Spedire a"
    '
    'edhh_dacocl
    '
    Me.edhh_dacocl.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edhh_dacocl.EditValue = "31/12/2099"
    Me.edhh_dacocl.Location = New System.Drawing.Point(148, 72)
    Me.edhh_dacocl.Name = "edhh_dacocl"
    Me.edhh_dacocl.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edhh_dacocl.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edhh_dacocl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edhh_dacocl.Properties.MaxLength = 65536
    Me.edhh_dacocl.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edhh_dacocl.Size = New System.Drawing.Size(84, 20)
    Me.edhh_dacocl.Tag = ""
    '
    'NtsLabel13
    '
    Me.NtsLabel13.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel13.Location = New System.Drawing.Point(4, 72)
    Me.NtsLabel13.Name = "NtsLabel13"
    Me.NtsLabel13.Size = New System.Drawing.Size(136, 20)
    Me.NtsLabel13.Text = "Data concordata cliente"
    '
    'edet_totdoc
    '
    Me.edet_totdoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edet_totdoc.Enabled = False
    Me.edet_totdoc.Location = New System.Drawing.Point(348, 72)
    Me.edet_totdoc.Name = "edet_totdoc"
    Me.edet_totdoc.Properties.Appearance.ForeColor = System.Drawing.Color.Red
    Me.edet_totdoc.Properties.Appearance.Options.UseForeColor = True
    Me.edet_totdoc.Properties.Appearance.Options.UseTextOptions = True
    Me.edet_totdoc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edet_totdoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edet_totdoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edet_totdoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edet_totdoc.Properties.MaxLength = 65536
    Me.edet_totdoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edet_totdoc.Size = New System.Drawing.Size(100, 20)
    '
    'NtsLabel12
    '
    Me.NtsLabel12.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel12.Location = New System.Drawing.Point(252, 72)
    Me.NtsLabel12.Name = "NtsLabel12"
    Me.NtsLabel12.Size = New System.Drawing.Size(80, 20)
    Me.NtsLabel12.Text = "Valore ordine"
    '
    'edet_datdoc
    '
    Me.edet_datdoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edet_datdoc.Location = New System.Drawing.Point(348, 24)
    Me.edet_datdoc.Name = "edet_datdoc"
    Me.edet_datdoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edet_datdoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edet_datdoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edet_datdoc.Properties.MaxLength = 65536
    Me.edet_datdoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edet_datdoc.Size = New System.Drawing.Size(100, 20)
    '
    'NtsLabel11
    '
    Me.NtsLabel11.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel11.Location = New System.Drawing.Point(252, 24)
    Me.NtsLabel11.Name = "NtsLabel11"
    Me.NtsLabel11.Size = New System.Drawing.Size(88, 20)
    Me.NtsLabel11.Text = "Data contratto"
    '
    'lbxx_conto
    '
    Me.lbxx_conto.BackColor = System.Drawing.Color.Transparent
    Me.lbxx_conto.Location = New System.Drawing.Point(188, 96)
    Me.lbxx_conto.Name = "lbxx_conto"
    Me.lbxx_conto.Size = New System.Drawing.Size(260, 20)
    Me.lbxx_conto.Text = "NtsLabel10"
    '
    'lbxx_telef
    '
    Me.lbxx_telef.BackColor = System.Drawing.Color.Transparent
    Me.lbxx_telef.Location = New System.Drawing.Point(344, 48)
    Me.lbxx_telef.Name = "lbxx_telef"
    Me.lbxx_telef.Size = New System.Drawing.Size(104, 20)
    Me.lbxx_telef.Text = "NtsLabel10"
    '
    'NtsLabel9
    '
    Me.NtsLabel9.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel9.Location = New System.Drawing.Point(296, 48)
    Me.NtsLabel9.Name = "NtsLabel9"
    Me.NtsLabel9.Size = New System.Drawing.Size(28, 20)
    Me.NtsLabel9.Text = "Tel."
    '
    'NtsLabel8
    '
    Me.NtsLabel8.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel8.Location = New System.Drawing.Point(4, 96)
    Me.NtsLabel8.Name = "NtsLabel8"
    Me.NtsLabel8.Size = New System.Drawing.Size(44, 20)
    Me.NtsLabel8.Text = "Cliente"
    '
    'edet_conto
    '
    Me.edet_conto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edet_conto.Location = New System.Drawing.Point(88, 96)
    Me.edet_conto.Name = "edet_conto"
    Me.edet_conto.Properties.Appearance.Options.UseTextOptions = True
    Me.edet_conto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edet_conto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edet_conto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edet_conto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edet_conto.Properties.MaxLength = 65536
    Me.edet_conto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edet_conto.Size = New System.Drawing.Size(88, 20)
    '
    'edet_numdoc
    '
    Me.edet_numdoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edet_numdoc.Enabled = False
    Me.edet_numdoc.Location = New System.Drawing.Point(88, 24)
    Me.edet_numdoc.Name = "edet_numdoc"
    Me.edet_numdoc.Properties.Appearance.Options.UseTextOptions = True
    Me.edet_numdoc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edet_numdoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edet_numdoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edet_numdoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edet_numdoc.Properties.MaxLength = 65536
    Me.edet_numdoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edet_numdoc.Size = New System.Drawing.Size(88, 20)
    '
    'edet_datcons
    '
    Me.edet_datcons.Cursor = System.Windows.Forms.Cursors.Default
    Me.edet_datcons.Location = New System.Drawing.Point(188, 48)
    Me.edet_datcons.Name = "edet_datcons"
    Me.edet_datcons.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edet_datcons.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edet_datcons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edet_datcons.Properties.MaxLength = 65536
    Me.edet_datcons.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edet_datcons.Size = New System.Drawing.Size(100, 20)
    '
    'NtsLabel7
    '
    Me.NtsLabel7.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel7.Location = New System.Drawing.Point(4, 48)
    Me.NtsLabel7.Name = "NtsLabel7"
    Me.NtsLabel7.Size = New System.Drawing.Size(180, 20)
    Me.NtsLabel7.Text = "Data Richiesta Consegna Cliente"
    '
    'NtsLabel6
    '
    Me.NtsLabel6.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel6.Location = New System.Drawing.Point(4, 24)
    Me.NtsLabel6.Name = "NtsLabel6"
    Me.NtsLabel6.Size = New System.Drawing.Size(44, 20)
    Me.NtsLabel6.Text = "Ordine"
    '
    'edhh_chiedi
    '
    Me.edhh_chiedi.Cursor = System.Windows.Forms.Cursors.Default
    Me.edhh_chiedi.Location = New System.Drawing.Point(76, 48)
    Me.edhh_chiedi.Name = "edhh_chiedi"
    Me.edhh_chiedi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edhh_chiedi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edhh_chiedi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edhh_chiedi.Properties.MaxLength = 65536
    Me.edhh_chiedi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edhh_chiedi.Size = New System.Drawing.Size(188, 20)
    '
    'NtsLabel4
    '
    Me.NtsLabel4.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.NtsLabel4.Location = New System.Drawing.Point(4, 3)
    Me.NtsLabel4.Name = "NtsLabel4"
    Me.NtsLabel4.Size = New System.Drawing.Size(784, 20)
    Me.NtsLabel4.Text = "Legenda: (FLG*) 1= da pagare   2= pagato   3= da pagare con fattura"
    '
    'grScaPag
    '
    Me.grScaPag.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grScaPag.Location = New System.Drawing.Point(2, 57)
    Me.grScaPag.MainView = Me.grvScaPag
    Me.grScaPag.Name = "grScaPag"
    Me.grScaPag.Size = New System.Drawing.Size(246, 140)
    Me.grScaPag.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvScaPag})
    '
    'grvScaPag
    '
    Me.grvScaPag.ActiveFilterEnabled = False
    Me.grvScaPag.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.hh_codpaga, Me.xx_despaga, Me.hh_imppaga, Me.hh_datpaga, Me.hh_flgpaga, Me.hh_numft, Me.hh_serieft, Me.hh_dataft, Me.hh_flgft})
    Me.grvScaPag.Enabled = True
    Me.grvScaPag.GridControl = Me.grScaPag
    Me.grvScaPag.Name = "grvScaPag"
    Me.grvScaPag.OptionsCustomization.AllowRowSizing = True
    Me.grvScaPag.OptionsFilter.AllowFilterEditor = False
    Me.grvScaPag.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvScaPag.OptionsNavigation.UseTabKey = False
    Me.grvScaPag.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvScaPag.OptionsView.ColumnAutoWidth = False
    Me.grvScaPag.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvScaPag.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvScaPag.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvScaPag.OptionsView.ShowGroupPanel = False
    '
    'hh_codpaga
    '
    Me.hh_codpaga.AppearanceCell.Options.UseBackColor = True
    Me.hh_codpaga.AppearanceCell.Options.UseTextOptions = True
    Me.hh_codpaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_codpaga.Caption = "Pag"
    Me.hh_codpaga.Enabled = True
    Me.hh_codpaga.FieldName = "hh_codpaga"
    Me.hh_codpaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_codpaga.Name = "hh_codpaga"
    Me.hh_codpaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.hh_codpaga.OptionsFilter.AllowFilter = False
    Me.hh_codpaga.Visible = True
    Me.hh_codpaga.VisibleIndex = 0
    Me.hh_codpaga.Width = 27
    '
    'xx_despaga
    '
    Me.xx_despaga.AppearanceCell.Options.UseBackColor = True
    Me.xx_despaga.AppearanceCell.Options.UseTextOptions = True
    Me.xx_despaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_despaga.Caption = "Descrizione"
    Me.xx_despaga.Enabled = False
    Me.xx_despaga.FieldName = "xx_despaga"
    Me.xx_despaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_despaga.Name = "xx_despaga"
    Me.xx_despaga.OptionsColumn.AllowEdit = False
    Me.xx_despaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_despaga.OptionsColumn.ReadOnly = True
    Me.xx_despaga.OptionsFilter.AllowFilter = False
    Me.xx_despaga.Visible = True
    Me.xx_despaga.VisibleIndex = 1
    Me.xx_despaga.Width = 132
    '
    'hh_imppaga
    '
    Me.hh_imppaga.AppearanceCell.Options.UseBackColor = True
    Me.hh_imppaga.AppearanceCell.Options.UseTextOptions = True
    Me.hh_imppaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_imppaga.Caption = "Importo"
    Me.hh_imppaga.Enabled = False
    Me.hh_imppaga.FieldName = "hh_imppaga"
    Me.hh_imppaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_imppaga.Name = "hh_imppaga"
    Me.hh_imppaga.OptionsColumn.AllowEdit = False
    Me.hh_imppaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.hh_imppaga.OptionsColumn.ReadOnly = True
    Me.hh_imppaga.OptionsFilter.AllowFilter = False
    Me.hh_imppaga.Visible = True
    Me.hh_imppaga.VisibleIndex = 2
    Me.hh_imppaga.Width = 61
    '
    'hh_datpaga
    '
    Me.hh_datpaga.AppearanceCell.Options.UseBackColor = True
    Me.hh_datpaga.AppearanceCell.Options.UseTextOptions = True
    Me.hh_datpaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_datpaga.Caption = "Data"
    Me.hh_datpaga.Enabled = True
    Me.hh_datpaga.FieldName = "hh_datpaga"
    Me.hh_datpaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_datpaga.Name = "hh_datpaga"
    Me.hh_datpaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.hh_datpaga.OptionsFilter.AllowFilter = False
    Me.hh_datpaga.Visible = True
    Me.hh_datpaga.VisibleIndex = 3
    Me.hh_datpaga.Width = 69
    '
    'hh_flgpaga
    '
    Me.hh_flgpaga.AppearanceCell.Options.UseBackColor = True
    Me.hh_flgpaga.AppearanceCell.Options.UseTextOptions = True
    Me.hh_flgpaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_flgpaga.Caption = "FLG (*)"
    Me.hh_flgpaga.Enabled = True
    Me.hh_flgpaga.FieldName = "hh_flgpaga"
    Me.hh_flgpaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_flgpaga.Name = "hh_flgpaga"
    Me.hh_flgpaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.hh_flgpaga.OptionsFilter.AllowFilter = False
    Me.hh_flgpaga.Visible = True
    Me.hh_flgpaga.VisibleIndex = 4
    Me.hh_flgpaga.Width = 50
    '
    'hh_numft
    '
    Me.hh_numft.AppearanceCell.Options.UseBackColor = True
    Me.hh_numft.AppearanceCell.Options.UseTextOptions = True
    Me.hh_numft.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_numft.Caption = "Num.Fat."
    Me.hh_numft.Enabled = False
    Me.hh_numft.FieldName = "hh_numft"
    Me.hh_numft.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_numft.Name = "hh_numft"
    Me.hh_numft.OptionsColumn.AllowEdit = False
    Me.hh_numft.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.hh_numft.OptionsColumn.ReadOnly = True
    Me.hh_numft.OptionsFilter.AllowFilter = False
    Me.hh_numft.Visible = True
    Me.hh_numft.VisibleIndex = 5
    Me.hh_numft.Width = 55
    '
    'hh_serieft
    '
    Me.hh_serieft.AppearanceCell.Options.UseBackColor = True
    Me.hh_serieft.AppearanceCell.Options.UseTextOptions = True
    Me.hh_serieft.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_serieft.Caption = "Serie"
    Me.hh_serieft.Enabled = False
    Me.hh_serieft.FieldName = "hh_serieft"
    Me.hh_serieft.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_serieft.Name = "hh_serieft"
    Me.hh_serieft.OptionsColumn.AllowEdit = False
    Me.hh_serieft.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.hh_serieft.OptionsColumn.ReadOnly = True
    Me.hh_serieft.OptionsFilter.AllowFilter = False
    Me.hh_serieft.Visible = True
    Me.hh_serieft.VisibleIndex = 6
    Me.hh_serieft.Width = 31
    '
    'hh_dataft
    '
    Me.hh_dataft.AppearanceCell.Options.UseBackColor = True
    Me.hh_dataft.AppearanceCell.Options.UseTextOptions = True
    Me.hh_dataft.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_dataft.Caption = "Data Fat."
    Me.hh_dataft.Enabled = False
    Me.hh_dataft.FieldName = "hh_dataft"
    Me.hh_dataft.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_dataft.Name = "hh_dataft"
    Me.hh_dataft.OptionsColumn.AllowEdit = False
    Me.hh_dataft.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.hh_dataft.OptionsColumn.ReadOnly = True
    Me.hh_dataft.OptionsFilter.AllowFilter = False
    Me.hh_dataft.Visible = True
    Me.hh_dataft.VisibleIndex = 7
    Me.hh_dataft.Width = 73
    '
    'hh_flgft
    '
    Me.hh_flgft.AppearanceCell.Options.UseBackColor = True
    Me.hh_flgft.AppearanceCell.Options.UseTextOptions = True
    Me.hh_flgft.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_flgft.Caption = "FLG (**)"
    Me.hh_flgft.Enabled = True
    Me.hh_flgft.FieldName = "hh_flgft"
    Me.hh_flgft.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_flgft.Name = "hh_flgft"
    Me.hh_flgft.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.hh_flgft.OptionsFilter.AllowFilter = False
    Me.hh_flgft.Visible = True
    Me.hh_flgft.VisibleIndex = 8
    Me.hh_flgft.Width = 53
    '
    'lbxx_diff
    '
    Me.lbxx_diff.BackColor = System.Drawing.Color.Transparent
    Me.lbxx_diff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lbxx_diff.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbxx_diff.Location = New System.Drawing.Point(72, 72)
    Me.lbxx_diff.Name = "lbxx_diff"
    Me.lbxx_diff.Size = New System.Drawing.Size(112, 20)
    Me.lbxx_diff.Text = "lbxx_diff"
    '
    'NtsLabel5
    '
    Me.NtsLabel5.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel5.Location = New System.Drawing.Point(4, 72)
    Me.NtsLabel5.Name = "NtsLabel5"
    Me.NtsLabel5.Size = New System.Drawing.Size(52, 20)
    Me.NtsLabel5.Text = "Margine"
    '
    'lbxx_marg
    '
    Me.lbxx_marg.BackColor = System.Drawing.Color.Transparent
    Me.lbxx_marg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lbxx_marg.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbxx_marg.Location = New System.Drawing.Point(72, 96)
    Me.lbxx_marg.Name = "lbxx_marg"
    Me.lbxx_marg.Size = New System.Drawing.Size(112, 20)
    Me.lbxx_marg.Text = "NtsLabel6"
    '
    'lbxx_ricavo
    '
    Me.lbxx_ricavo.BackColor = System.Drawing.Color.Transparent
    Me.lbxx_ricavo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lbxx_ricavo.Location = New System.Drawing.Point(72, 24)
    Me.lbxx_ricavo.Name = "lbxx_ricavo"
    Me.lbxx_ricavo.Size = New System.Drawing.Size(112, 20)
    Me.lbxx_ricavo.Text = "NtsLabel5"
    '
    'lbxx_costo
    '
    Me.lbxx_costo.BackColor = System.Drawing.Color.Transparent
    Me.lbxx_costo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lbxx_costo.Location = New System.Drawing.Point(72, 48)
    Me.lbxx_costo.Name = "lbxx_costo"
    Me.lbxx_costo.Size = New System.Drawing.Size(112, 20)
    Me.lbxx_costo.Text = "NtsLabel4"
    '
    'NtsLabel3
    '
    Me.NtsLabel3.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel3.Location = New System.Drawing.Point(4, 96)
    Me.NtsLabel3.Name = "NtsLabel3"
    Me.NtsLabel3.Size = New System.Drawing.Size(68, 20)
    Me.NtsLabel3.Text = "Margine %"
    '
    'NtsLabel2
    '
    Me.NtsLabel2.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel2.Location = New System.Drawing.Point(4, 24)
    Me.NtsLabel2.Name = "NtsLabel2"
    Me.NtsLabel2.Size = New System.Drawing.Size(76, 20)
    Me.NtsLabel2.Text = "Totale Ricavi"
    '
    'NtsLabel1
    '
    Me.NtsLabel1.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel1.Location = New System.Drawing.Point(4, 48)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.Size = New System.Drawing.Size(72, 20)
    Me.NtsLabel1.Text = "Totale Costi"
    '
    'grTotFor
    '
    Me.grTotFor.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grTotFor.Location = New System.Drawing.Point(2, 21)
    Me.grTotFor.MainView = Me.grvTotFor
    Me.grTotFor.Name = "grTotFor"
    Me.grTotFor.Size = New System.Drawing.Size(220, 353)
    Me.grTotFor.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvTotFor})
    '
    'grvTotFor
    '
    Me.grvTotFor.ActiveFilterEnabled = False
    Me.grvTotFor.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_codfor, Me.xx_desfor, Me.xx_valfor})
    Me.grvTotFor.Enabled = True
    Me.grvTotFor.GridControl = Me.grTotFor
    Me.grvTotFor.Name = "grvTotFor"
    Me.grvTotFor.OptionsCustomization.AllowRowSizing = True
    Me.grvTotFor.OptionsFilter.AllowFilterEditor = False
    Me.grvTotFor.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvTotFor.OptionsNavigation.UseTabKey = False
    Me.grvTotFor.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvTotFor.OptionsView.ColumnAutoWidth = False
    Me.grvTotFor.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvTotFor.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvTotFor.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvTotFor.OptionsView.ShowGroupPanel = False
    '
    'xx_codfor
    '
    Me.xx_codfor.AppearanceCell.Options.UseBackColor = True
    Me.xx_codfor.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codfor.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codfor.Caption = "Codice"
    Me.xx_codfor.Enabled = False
    Me.xx_codfor.FieldName = "xx_codfor"
    Me.xx_codfor.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codfor.Name = "xx_codfor"
    Me.xx_codfor.OptionsColumn.AllowEdit = False
    Me.xx_codfor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codfor.OptionsColumn.ReadOnly = True
    Me.xx_codfor.OptionsFilter.AllowFilter = False
    Me.xx_codfor.Visible = True
    Me.xx_codfor.VisibleIndex = 0
    '
    'xx_desfor
    '
    Me.xx_desfor.AppearanceCell.Options.UseBackColor = True
    Me.xx_desfor.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desfor.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desfor.Caption = "Fornitore"
    Me.xx_desfor.Enabled = False
    Me.xx_desfor.FieldName = "xx_desfor"
    Me.xx_desfor.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desfor.Name = "xx_desfor"
    Me.xx_desfor.OptionsColumn.AllowEdit = False
    Me.xx_desfor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desfor.OptionsColumn.ReadOnly = True
    Me.xx_desfor.OptionsFilter.AllowFilter = False
    Me.xx_desfor.Visible = True
    Me.xx_desfor.VisibleIndex = 1
    Me.xx_desfor.Width = 114
    '
    'xx_valfor
    '
    Me.xx_valfor.AppearanceCell.Options.UseBackColor = True
    Me.xx_valfor.AppearanceCell.Options.UseTextOptions = True
    Me.xx_valfor.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_valfor.Caption = "Valore"
    Me.xx_valfor.Enabled = False
    Me.xx_valfor.FieldName = "xx_valfor"
    Me.xx_valfor.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_valfor.Name = "xx_valfor"
    Me.xx_valfor.OptionsColumn.AllowEdit = False
    Me.xx_valfor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_valfor.OptionsColumn.ReadOnly = True
    Me.xx_valfor.OptionsFilter.AllowFilter = False
    Me.xx_valfor.Visible = True
    Me.xx_valfor.VisibleIndex = 2
    '
    'edxx_valresiduo
    '
    Me.edxx_valresiduo.Enabled = False
    Me.edxx_valresiduo.Location = New System.Drawing.Point(118, 3)
    Me.edxx_valresiduo.Name = "edxx_valresiduo"
    Me.edxx_valresiduo.Properties.Appearance.Options.UseTextOptions = True
    Me.edxx_valresiduo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edxx_valresiduo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_valresiduo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_valresiduo.Properties.AutoHeight = False
    Me.edxx_valresiduo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_valresiduo.Properties.MaxLength = 65536
    Me.edxx_valresiduo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_valresiduo.Size = New System.Drawing.Size(100, 20)
    '
    'NtsLabel25
    '
    Me.NtsLabel25.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel25.Location = New System.Drawing.Point(4, 3)
    Me.NtsLabel25.Name = "NtsLabel25"
    Me.NtsLabel25.Size = New System.Drawing.Size(120, 20)
    Me.NtsLabel25.Text = "Valore residuo ordine"
    '
    'cmdCancellaRiga
    '
    Me.cmdCancellaRiga.Location = New System.Drawing.Point(242, 3)
    Me.cmdCancellaRiga.Name = "cmdCancellaRiga"
    Me.cmdCancellaRiga.Size = New System.Drawing.Size(80, 26)
    Me.cmdCancellaRiga.Text = "Cancella Riga"
    '
    'edhh_ttmont
    '
    Me.edhh_ttmont.Cursor = System.Windows.Forms.Cursors.Default
    Me.edhh_ttmont.Location = New System.Drawing.Point(160, 192)
    Me.edhh_ttmont.Name = "edhh_ttmont"
    Me.edhh_ttmont.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edhh_ttmont.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edhh_ttmont.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edhh_ttmont.Properties.MaxLength = 65536
    Me.edhh_ttmont.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edhh_ttmont.Size = New System.Drawing.Size(188, 20)
    '
    'NtsLabel22
    '
    Me.NtsLabel22.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel22.Location = New System.Drawing.Point(4, 192)
    Me.NtsLabel22.Name = "NtsLabel22"
    Me.NtsLabel22.Size = New System.Drawing.Size(148, 20)
    Me.NtsLabel22.Text = "Tempo Montaggio/Finiture"
    '
    'ckhh_finitu
    '
    Me.ckhh_finitu.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckhh_finitu.Location = New System.Drawing.Point(104, 168)
    Me.ckhh_finitu.Name = "ckhh_finitu"
    Me.ckhh_finitu.NTSPictureChecked = ""
    Me.ckhh_finitu.NTSPictureUnchecked = ""
    Me.ckhh_finitu.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckhh_finitu.Properties.Appearance.Options.UseBackColor = True
    Me.ckhh_finitu.Properties.AutoHeight = False
    Me.ckhh_finitu.Properties.Caption = "Finiture"
    Me.ckhh_finitu.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
    Me.ckhh_finitu.Properties.PictureChecked = CType(resources.GetObject("ckhh_finitu.Properties.PictureChecked"), System.Drawing.Image)
    Me.ckhh_finitu.Properties.PictureUnchecked = CType(resources.GetObject("ckhh_finitu.Properties.PictureUnchecked"), System.Drawing.Image)
    Me.ckhh_finitu.Size = New System.Drawing.Size(80, 20)
    '
    'ckhh_mont
    '
    Me.ckhh_mont.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckhh_mont.Location = New System.Drawing.Point(4, 168)
    Me.ckhh_mont.Name = "ckhh_mont"
    Me.ckhh_mont.NTSPictureChecked = ""
    Me.ckhh_mont.NTSPictureUnchecked = ""
    Me.ckhh_mont.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckhh_mont.Properties.Appearance.Options.UseBackColor = True
    Me.ckhh_mont.Properties.AutoHeight = False
    Me.ckhh_mont.Properties.Caption = "Montaggio"
    Me.ckhh_mont.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
    Me.ckhh_mont.Properties.PictureChecked = CType(resources.GetObject("ckhh_mont.Properties.PictureChecked"), System.Drawing.Image)
    Me.ckhh_mont.Properties.PictureUnchecked = CType(resources.GetObject("ckhh_mont.Properties.PictureUnchecked"), System.Drawing.Image)
    Me.ckhh_mont.Size = New System.Drawing.Size(96, 20)
    '
    'edhh_datint
    '
    Me.edhh_datint.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edhh_datint.Location = New System.Drawing.Point(104, 96)
    Me.edhh_datint.Name = "edhh_datint"
    Me.edhh_datint.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edhh_datint.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edhh_datint.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edhh_datint.Properties.MaxLength = 65536
    Me.edhh_datint.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edhh_datint.Size = New System.Drawing.Size(116, 20)
    '
    'NtsLabel21
    '
    Me.NtsLabel21.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel21.Location = New System.Drawing.Point(4, 96)
    Me.NtsLabel21.Name = "NtsLabel21"
    Me.NtsLabel21.Size = New System.Drawing.Size(96, 20)
    Me.NtsLabel21.Text = "Data Intervento"
    '
    'lbxx_squadra
    '
    Me.lbxx_squadra.BackColor = System.Drawing.Color.Transparent
    Me.lbxx_squadra.Location = New System.Drawing.Point(264, 120)
    Me.lbxx_squadra.Name = "lbxx_squadra"
    Me.lbxx_squadra.Size = New System.Drawing.Size(176, 20)
    Me.lbxx_squadra.Text = "NtsLabel21"
    '
    'edhh_squadra
    '
    Me.edhh_squadra.Cursor = System.Windows.Forms.Cursors.Default
    Me.edhh_squadra.Location = New System.Drawing.Point(76, 120)
    Me.edhh_squadra.Name = "edhh_squadra"
    Me.edhh_squadra.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edhh_squadra.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edhh_squadra.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edhh_squadra.Properties.MaxLength = 65536
    Me.edhh_squadra.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edhh_squadra.Size = New System.Drawing.Size(184, 20)
    '
    'NtsLabel20
    '
    Me.NtsLabel20.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel20.Location = New System.Drawing.Point(4, 120)
    Me.NtsLabel20.Name = "NtsLabel20"
    Me.NtsLabel20.Size = New System.Drawing.Size(52, 20)
    Me.NtsLabel20.Text = "Squadra"
    '
    'edhh_autoca
    '
    Me.edhh_autoca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edhh_autoca.Location = New System.Drawing.Point(76, 144)
    Me.edhh_autoca.Name = "edhh_autoca"
    Me.edhh_autoca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edhh_autoca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edhh_autoca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edhh_autoca.Properties.MaxLength = 65536
    Me.edhh_autoca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edhh_autoca.Size = New System.Drawing.Size(188, 20)
    '
    'NtsLabel19
    '
    Me.NtsLabel19.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel19.Location = New System.Drawing.Point(4, 144)
    Me.NtsLabel19.Name = "NtsLabel19"
    Me.NtsLabel19.Size = New System.Drawing.Size(56, 20)
    Me.NtsLabel19.Text = "Autocarri"
    '
    'ckhh_finanz
    '
    Me.ckhh_finanz.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckhh_finanz.Location = New System.Drawing.Point(184, 216)
    Me.ckhh_finanz.Name = "ckhh_finanz"
    Me.ckhh_finanz.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckhh_finanz.Properties.Appearance.Options.UseBackColor = True
    Me.ckhh_finanz.Properties.AutoHeight = False
    Me.ckhh_finanz.Properties.Caption = "Finanziamento approvato"
    Me.ckhh_finanz.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
    Me.ckhh_finanz.Properties.PictureChecked = CType(resources.GetObject("ckhh_finanz.Properties.PictureChecked"), System.Drawing.Image)
    Me.ckhh_finanz.Properties.PictureUnchecked = CType(resources.GetObject("ckhh_finanz.Properties.PictureUnchecked"), System.Drawing.Image)
    Me.ckhh_finanz.Size = New System.Drawing.Size(176, 20)
    '
    'ckhh_elettr
    '
    Me.ckhh_elettr.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckhh_elettr.Location = New System.Drawing.Point(104, 216)
    Me.ckhh_elettr.Name = "ckhh_elettr"
    Me.ckhh_elettr.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckhh_elettr.Properties.Appearance.Options.UseBackColor = True
    Me.ckhh_elettr.Properties.AutoHeight = False
    Me.ckhh_elettr.Properties.Caption = "Elettricità"
    Me.ckhh_elettr.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
    Me.ckhh_elettr.Properties.PictureChecked = CType(resources.GetObject("ckhh_elettr.Properties.PictureChecked"), System.Drawing.Image)
    Me.ckhh_elettr.Properties.PictureUnchecked = CType(resources.GetObject("ckhh_elettr.Properties.PictureUnchecked"), System.Drawing.Image)
    Me.ckhh_elettr.Size = New System.Drawing.Size(92, 20)
    '
    'ckhh_ascens
    '
    Me.ckhh_ascens.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckhh_ascens.Location = New System.Drawing.Point(4, 216)
    Me.ckhh_ascens.Name = "ckhh_ascens"
    Me.ckhh_ascens.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckhh_ascens.Properties.Appearance.Options.UseBackColor = True
    Me.ckhh_ascens.Properties.AutoHeight = False
    Me.ckhh_ascens.Properties.Caption = "Ascensore"
    Me.ckhh_ascens.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
    Me.ckhh_ascens.Properties.PictureChecked = CType(resources.GetObject("ckhh_ascens.Properties.PictureChecked"), System.Drawing.Image)
    Me.ckhh_ascens.Properties.PictureUnchecked = CType(resources.GetObject("ckhh_ascens.Properties.PictureUnchecked"), System.Drawing.Image)
    Me.ckhh_ascens.Size = New System.Drawing.Size(96, 20)
    '
    'lbxx_codagen2
    '
    Me.lbxx_codagen2.BackColor = System.Drawing.Color.Transparent
    Me.lbxx_codagen2.Location = New System.Drawing.Point(184, 192)
    Me.lbxx_codagen2.Name = "lbxx_codagen2"
    Me.lbxx_codagen2.Size = New System.Drawing.Size(264, 20)
    Me.lbxx_codagen2.Text = "NtsLabel25"
    '
    'edet_codagen2
    '
    Me.edet_codagen2.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edet_codagen2.Location = New System.Drawing.Point(88, 192)
    Me.edet_codagen2.Name = "edet_codagen2"
    Me.edet_codagen2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edet_codagen2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edet_codagen2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edet_codagen2.Properties.MaxLength = 65536
    Me.edet_codagen2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edet_codagen2.Size = New System.Drawing.Size(88, 20)
    '
    'NtsLabel24
    '
    Me.NtsLabel24.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel24.Location = New System.Drawing.Point(4, 192)
    Me.NtsLabel24.Name = "NtsLabel24"
    Me.NtsLabel24.Size = New System.Drawing.Size(68, 20)
    Me.NtsLabel24.Text = "Consulente"
    '
    'lbxx_codagen
    '
    Me.lbxx_codagen.BackColor = System.Drawing.Color.Transparent
    Me.lbxx_codagen.Location = New System.Drawing.Point(184, 168)
    Me.lbxx_codagen.Name = "lbxx_codagen"
    Me.lbxx_codagen.Size = New System.Drawing.Size(264, 20)
    Me.lbxx_codagen.Text = "NtsLabel24"
    '
    'edet_codagen
    '
    Me.edet_codagen.Cursor = System.Windows.Forms.Cursors.Default
    Me.edet_codagen.Location = New System.Drawing.Point(88, 168)
    Me.edet_codagen.Name = "edet_codagen"
    Me.edet_codagen.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edet_codagen.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edet_codagen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edet_codagen.Properties.MaxLength = 65536
    Me.edet_codagen.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edet_codagen.Size = New System.Drawing.Size(88, 20)
    '
    'NtsLabel23
    '
    Me.NtsLabel23.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel23.Location = New System.Drawing.Point(4, 168)
    Me.NtsLabel23.Name = "NtsLabel23"
    Me.NtsLabel23.Size = New System.Drawing.Size(68, 20)
    Me.NtsLabel23.Text = "Arredatore"
    '
    'NtsFlowLayoutPanel1
    '
    Me.NtsFlowLayoutPanel1.AllowDrop = True
    Me.NtsFlowLayoutPanel1.AutoScroll = True
    Me.NtsFlowLayoutPanel1.Controls.Add(Me.NtsGroupBox1)
    Me.NtsFlowLayoutPanel1.Controls.Add(Me.NtsGroupBox2)
    Me.NtsFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
    Me.NtsFlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
    Me.NtsFlowLayoutPanel1.Name = "NtsFlowLayoutPanel1"
    Me.NtsFlowLayoutPanel1.Size = New System.Drawing.Size(486, 260)
    Me.NtsFlowLayoutPanel1.TabIndex = 1
    '
    'NtsGroupBox1
    '
    Me.NtsGroupBox1.AllowDrop = True
    Me.NtsGroupBox1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox1.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox1.Controls.Add(Me.lbxx_tipobf)
    Me.NtsGroupBox1.Controls.Add(Me.lbxx_codagen2)
    Me.NtsGroupBox1.Controls.Add(Me.edet_tipobf)
    Me.NtsGroupBox1.Controls.Add(Me.NtsLabel15)
    Me.NtsGroupBox1.Controls.Add(Me.edet_coddest)
    Me.NtsGroupBox1.Controls.Add(Me.edet_codagen2)
    Me.NtsGroupBox1.Controls.Add(Me.NtsLabel14)
    Me.NtsGroupBox1.Controls.Add(Me.NtsLabel24)
    Me.NtsGroupBox1.Controls.Add(Me.lbxx_coddest)
    Me.NtsGroupBox1.Controls.Add(Me.lbxx_codagen)
    Me.NtsGroupBox1.Controls.Add(Me.lbxx_inddestdiv)
    Me.NtsGroupBox1.Controls.Add(Me.edet_codagen)
    Me.NtsGroupBox1.Controls.Add(Me.NtsLabel23)
    Me.NtsGroupBox1.Controls.Add(Me.edet_datdoc)
    Me.NtsGroupBox1.Controls.Add(Me.NtsLabel6)
    Me.NtsGroupBox1.Controls.Add(Me.edet_numdoc)
    Me.NtsGroupBox1.Controls.Add(Me.NtsLabel11)
    Me.NtsGroupBox1.Controls.Add(Me.lbxx_teldestdiv)
    Me.NtsGroupBox1.Controls.Add(Me.lbxx_telef)
    Me.NtsGroupBox1.Controls.Add(Me.NtsLabel9)
    Me.NtsGroupBox1.Controls.Add(Me.edet_datcons)
    Me.NtsGroupBox1.Controls.Add(Me.edhh_dacocl)
    Me.NtsGroupBox1.Controls.Add(Me.lbxx_conto)
    Me.NtsGroupBox1.Controls.Add(Me.NtsLabel13)
    Me.NtsGroupBox1.Controls.Add(Me.NtsLabel7)
    Me.NtsGroupBox1.Controls.Add(Me.edet_totdoc)
    Me.NtsGroupBox1.Controls.Add(Me.NtsLabel8)
    Me.NtsGroupBox1.Controls.Add(Me.NtsLabel12)
    Me.NtsGroupBox1.Controls.Add(Me.edet_conto)
    Me.NtsGroupBox1.Location = New System.Drawing.Point(4, 4)
    Me.NtsGroupBox1.Name = "NtsGroupBox1"
    Me.NtsGroupBox1.Size = New System.Drawing.Size(456, 248)
    Me.NtsGroupBox1.Text = "DATI ORDINE"
    '
    'NtsGroupBox2
    '
    Me.NtsGroupBox2.AllowDrop = True
    Me.NtsGroupBox2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox2.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox2.Controls.Add(Me.ckhh_finanz)
    Me.NtsGroupBox2.Controls.Add(Me.ckhh_elettr)
    Me.NtsGroupBox2.Controls.Add(Me.ckhh_finitu)
    Me.NtsGroupBox2.Controls.Add(Me.ckhh_ascens)
    Me.NtsGroupBox2.Controls.Add(Me.ckhh_mont)
    Me.NtsGroupBox2.Controls.Add(Me.edhh_ttmont)
    Me.NtsGroupBox2.Controls.Add(Me.NtsLabel22)
    Me.NtsGroupBox2.Controls.Add(Me.NtsLabel17)
    Me.NtsGroupBox2.Controls.Add(Me.edhh_rispon)
    Me.NtsGroupBox2.Controls.Add(Me.edhh_scpian)
    Me.NtsGroupBox2.Controls.Add(Me.NtsLabel18)
    Me.NtsGroupBox2.Controls.Add(Me.edhh_autoca)
    Me.NtsGroupBox2.Controls.Add(Me.NtsLabel19)
    Me.NtsGroupBox2.Controls.Add(Me.lbxx_squadra)
    Me.NtsGroupBox2.Controls.Add(Me.edhh_datint)
    Me.NtsGroupBox2.Controls.Add(Me.edhh_squadra)
    Me.NtsGroupBox2.Controls.Add(Me.NtsLabel20)
    Me.NtsGroupBox2.Controls.Add(Me.NtsLabel21)
    Me.NtsGroupBox2.Controls.Add(Me.NtsLabel16)
    Me.NtsGroupBox2.Controls.Add(Me.edhh_chiedi)
    Me.NtsGroupBox2.Location = New System.Drawing.Point(4, 260)
    Me.NtsGroupBox2.Margin = New System.Windows.Forms.Padding(4)
    Me.NtsGroupBox2.Name = "NtsGroupBox2"
    Me.NtsGroupBox2.Size = New System.Drawing.Size(456, 248)
    Me.NtsGroupBox2.Text = "ULLTERIORI DATI"
    Me.NtsGroupBox2.Tile = True
    Me.NtsGroupBox2.TileIndex = 1
    '
    'NtsFlowLayoutPanel2
    '
    Me.NtsFlowLayoutPanel2.AllowDrop = True
    Me.NtsFlowLayoutPanel2.AutoScroll = True
    Me.NtsFlowLayoutPanel2.Controls.Add(Me.NtsGroupBox4)
    Me.NtsFlowLayoutPanel2.Controls.Add(Me.NtsGroupBox3)
    Me.NtsFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Left
    Me.NtsFlowLayoutPanel2.Location = New System.Drawing.Point(0, 260)
    Me.NtsFlowLayoutPanel2.Name = "NtsFlowLayoutPanel2"
    Me.NtsFlowLayoutPanel2.Size = New System.Drawing.Size(236, 251)
    Me.NtsFlowLayoutPanel2.TabIndex = 37
    '
    'NtsGroupBox4
    '
    Me.NtsGroupBox4.AllowDrop = True
    Me.NtsGroupBox4.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox4.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox4.Controls.Add(Me.grTotFor)
    Me.NtsGroupBox4.Location = New System.Drawing.Point(4, 4)
    Me.NtsGroupBox4.Margin = New System.Windows.Forms.Padding(4)
    Me.NtsGroupBox4.Name = "NtsGroupBox4"
    Me.NtsGroupBox4.Size = New System.Drawing.Size(224, 376)
    Me.NtsGroupBox4.Text = "RIEPILOGO FORNITORI"
    Me.NtsGroupBox4.Tile = True
    Me.NtsGroupBox4.TileIndex = 0
    '
    'NtsGroupBox3
    '
    Me.NtsGroupBox3.AllowDrop = True
    Me.NtsGroupBox3.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox3.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox3.Controls.Add(Me.lbxx_costo)
    Me.NtsGroupBox3.Controls.Add(Me.lbxx_diff)
    Me.NtsGroupBox3.Controls.Add(Me.lbxx_ricavo)
    Me.NtsGroupBox3.Controls.Add(Me.NtsLabel5)
    Me.NtsGroupBox3.Controls.Add(Me.NtsLabel1)
    Me.NtsGroupBox3.Controls.Add(Me.lbxx_marg)
    Me.NtsGroupBox3.Controls.Add(Me.NtsLabel2)
    Me.NtsGroupBox3.Controls.Add(Me.NtsLabel3)
    Me.NtsGroupBox3.Location = New System.Drawing.Point(4, 388)
    Me.NtsGroupBox3.Margin = New System.Windows.Forms.Padding(4)
    Me.NtsGroupBox3.Name = "NtsGroupBox3"
    Me.NtsGroupBox3.Size = New System.Drawing.Size(224, 120)
    Me.NtsGroupBox3.Text = "REDDITIVITÀ"
    Me.NtsGroupBox3.Tile = True
    Me.NtsGroupBox3.TileIndex = 1
    '
    'NtsGroupBox6
    '
    Me.NtsGroupBox6.AllowDrop = True
    Me.NtsGroupBox6.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox6.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox6.Controls.Add(Me.grScaPag)
    Me.NtsGroupBox6.Controls.Add(Me.NtsPanel3)
    Me.NtsGroupBox6.Controls.Add(Me.NtsPanel1)
    Me.NtsGroupBox6.Dock = System.Windows.Forms.DockStyle.Fill
    Me.NtsGroupBox6.Location = New System.Drawing.Point(236, 260)
    Me.NtsGroupBox6.Name = "NtsGroupBox6"
    Me.NtsGroupBox6.Size = New System.Drawing.Size(250, 251)
    Me.NtsGroupBox6.Text = "RIEPILOGO PAGAMENTI"
    '
    'NtsPanel3
    '
    Me.NtsPanel3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel3.Controls.Add(Me.NtsLabel10)
    Me.NtsPanel3.Controls.Add(Me.NtsLabel4)
    Me.NtsPanel3.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.NtsPanel3.Location = New System.Drawing.Point(2, 197)
    Me.NtsPanel3.Name = "NtsPanel3"
    Me.NtsPanel3.Size = New System.Drawing.Size(246, 52)
    '
    'NtsLabel10
    '
    Me.NtsLabel10.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.NtsLabel10.Location = New System.Drawing.Point(4, 27)
    Me.NtsLabel10.Name = "NtsLabel10"
    Me.NtsLabel10.Size = New System.Drawing.Size(784, 20)
    Me.NtsLabel10.Text = "                  (FLG**) F= fattura da pagare   P= fattura pagata"
    '
    'NtsPanel1
    '
    Me.NtsPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel1.Controls.Add(Me.edxx_valresiduo)
    Me.NtsPanel1.Controls.Add(Me.NtsLabel25)
    Me.NtsPanel1.Controls.Add(Me.cmdCancellaRiga)
    Me.NtsPanel1.Dock = System.Windows.Forms.DockStyle.Top
    Me.NtsPanel1.Location = New System.Drawing.Point(2, 21)
    Me.NtsPanel1.Name = "NtsPanel1"
    Me.NtsPanel1.Size = New System.Drawing.Size(246, 36)
    '
    'FRMDatiAgg
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(486, 511)
    Me.Controls.Add(Me.NtsGroupBox6)
    Me.Controls.Add(Me.NtsFlowLayoutPanel2)
    Me.Controls.Add(Me.NtsFlowLayoutPanel1)
    Me.Name = "FRMDatiAgg"
    Me.Text = "RIEPILOGO DATI TESTATA"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edhh_scpian.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edhh_rispon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edet_tipobf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edet_coddest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edhh_dacocl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edet_totdoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edet_datdoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edet_conto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edet_numdoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edet_datcons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edhh_chiedi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grScaPag, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvScaPag, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grTotFor, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvTotFor, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_valresiduo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edhh_ttmont.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckhh_finitu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckhh_mont.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edhh_datint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edhh_squadra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edhh_autoca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckhh_finanz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckhh_elettr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckhh_ascens.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edet_codagen2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edet_codagen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsFlowLayoutPanel1.ResumeLayout(False)
    CType(Me.NtsGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox1.ResumeLayout(False)
    CType(Me.NtsGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox2.ResumeLayout(False)
    Me.NtsFlowLayoutPanel2.ResumeLayout(False)
    CType(Me.NtsGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox4.ResumeLayout(False)
    CType(Me.NtsGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox3.ResumeLayout(False)
    CType(Me.NtsGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox6.ResumeLayout(False)
    CType(Me.NtsPanel3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel3.ResumeLayout(False)
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel1.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
#End Region
  Private components As System.ComponentModel.IContainer
  Public oCleHh As CLEORGSOR
  Public dsHh As New DataSet
  Public oCallParams As CLE__CLDP
  Public dcHhFor As BindingSource = New BindingSource
  Public dctesta As BindingSource = New BindingSource
  Public dcHhRed As BindingSource = New BindingSource
  Public dcHhPag As BindingSource = New BindingSource
  Public dcHhGrFor As BindingSource = New BindingSource
  Public dcHhGrPag As BindingSource = New BindingSource

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano già  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      lbxx_marg.NTSDbField = "CPNE.marg.xx_marg"
      lbxx_ricavo.NTSDbField = "CPNE.marg.xx_ricavo"
      lbxx_costo.NTSDbField = "CPNE.marg.xx_costo"
      lbxx_diff.NTSDbField = "CPNE.marg.xx_diff"


      edet_numdoc.NTSDbField = "TESTA.et_numdoc"
      edet_datcons.NTSDbField = "TESTA.et_datcons"
      edet_totdoc.NTSDbField = "TESTA.et_totdoc"
      edet_conto.NTSDbField = "TESTA.et_conto"
      lbxx_conto.NTSDbField = "TESTA.xx_conto"
      lbxx_telef.NTSDbField = "TESTA.xx_telef"
      edet_datdoc.NTSDbField = "TESTA.et_datdoc"
      edhh_dacocl.NTSDbField = "TESTA.hh_dacocl"
      edet_coddest.NTSDbField = "TESTA.et_coddest"
      lbxx_coddest.NTSDbField = "TESTA.xx_coddest"
      lbxx_inddestdiv.NTSDbField = "TESTA.xx_inddestdiv"
      lbxx_teldestdiv.NTSDbField = "TESTA.xx_teldestdiv"
      edet_tipobf.NTSDbField = "TESTA.et_tipobf"
      lbxx_tipobf.NTSDbField = "TESTA.xx_tipobf"
      edhh_chiedi.NTSDbField = "TESTA.hh_chiedi"
      edhh_rispon.NTSDbField = "TESTA.hh_rispon"
      edhh_squadra.NTSDbField = "TESTA.hh_squadra"
      lbxx_squadra.NTSDbField = "TESTA.xx_squadra"
      edhh_datint.NTSDbField = "TESTA.hh_datint"
      edhh_autoca.NTSDbField = "TESTA.hh_autoca"
      edhh_scpian.NTSDbField = "TESTA.hh_scpian"
      edhh_ttmont.NTSDbField = "TESTA.hh_ttmont"
      edet_codagen.NTSDbField = "TESTA.et_codagen"
      lbxx_codagen.NTSDbField = "TESTA.xx_codagen"
      edet_codagen2.NTSDbField = "TESTA.et_codagen2"
      lbxx_codagen2.NTSDbField = "TESTA.xx_codagen2"
      edxx_valresiduo.NTSDbField = "TESTA.xx_valresiduo"

      NtsFlowLayoutPanel2.NTSSetParam(oMenu, oApp.Tr(Me, 131935059154874217, "Pan1"))
      NtsFlowLayoutPanel1.NTSSetParam(oMenu, oApp.Tr(Me, 131935059155194028, "Pan2"))



      edet_numdoc.NTSSetParam(oMenu, oApp.Tr(Me, 130875777946600846, "Numero Ordine"))
      edet_datcons.NTSSetParam(oMenu, oApp.Tr(Me, 130875777946600846, "Data Consegna"), True)
      edet_totdoc.NTSSetParam(oMenu, oApp.Tr(Me, 130875777946600846, "Valore Ordine"), "#,##0.00")
      edet_conto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130875777946600846, "Cliente"), tabanagrac)
      edet_datdoc.NTSSetParam(oMenu, oApp.Tr(Me, 130875777946600846, "Data Contratto"), True)
      edhh_dacocl.NTSSetParam(oMenu, oApp.Tr(Me, 130875777946600846, "Data Concordata cliente"), True)
      edet_coddest.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130875777946600846, "Spedire a"), tabdestdiv)
      edet_tipobf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130875777946600846, "Tipo bolla/fattura"), tabtpbf)
      edhh_chiedi.NTSSetParam(oMenu, oApp.Tr(Me, 130875777946600846, "Chiedi"), 0, True)
      edhh_rispon.NTSSetParam(oMenu, oApp.Tr(Me, 130875777946600846, "Rispondi"), 0, True)
      edhh_squadra.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130875777946600846, "Squadra"), tabdestdiv, True)
      edhh_datint.NTSSetParam(oMenu, oApp.Tr(Me, 130875777946600846, "Data Intervento"), True)
      edhh_autoca.NTSSetParam(oMenu, oApp.Tr(Me, 130875777946600846, "Autocarri"), 0, True)
      ckhh_finitu.NTSSetParam(oMenu, oApp.Tr(Me, 130880156510216815, "Finiture"), "S")
      ckhh_mont.NTSSetParam(oMenu, oApp.Tr(Me, 130880156510226823, "Montaggio"), "S")
      edhh_scpian.NTSSetParam(oMenu, oApp.Tr(Me, 130875777946600846, "Scala/Piano"), 0, True)
      edhh_ttmont.NTSSetParam(oMenu, oApp.Tr(Me, 130875777946600846, "Tempo Montaggio"), 0, True)
      edet_codagen.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130875777946600846, "Arredatore"), tabcage, True)
      edet_codagen2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130875777946600846, "Consulente"), tabcage, True)
      edxx_valresiduo.NTSSetParam(oMenu, oApp.Tr(Me, 130875777946600846, "Valore Residuo Ordine"), "#,##0.00")

      edhh_squadra.NTSSetParamZoom("ZOOMOPERAT")

      grvScaPag.NTSSetParam(oMenu, oApp.Tr(Me, 130875777944214505, "Pagamenti"))
      grvTotFor.NTSSetParam(oMenu, oApp.Tr(Me, 130875777946580834, "Fornitori"))

      xx_codfor.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130875777946590841, "Codice"), 0, True)

      xx_desfor.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130875777946600845, "Fornitore"), 0, True)
      xx_valfor.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130875777946610762, "Valore"), "#,##0.00", 15)

      'hh_codpaga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130875832004559593, "Pag"), "0", 4, 0, 9999)
      hh_codpaga.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 130132764741973791, "Conto"), tabpaga)
      xx_despaga.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130875832004715846, "Descrizione"), 0, True)
      hh_imppaga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130875832004872085, "Importo"), "#,##0.00", 15)
      hh_datpaga.NTSSetParamDATA(oMenu, oApp.Tr(Me, 130875832005028340, "Data"), True)
      hh_flgpaga.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130875832005184590, "FLG (*)"), 0, True)
      hh_numft.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130875832005340839, "N.Fat."), "0", 9, 0, 999999999)
      hh_dataft.NTSSetParamDATA(oMenu, oApp.Tr(Me, 130875832005497091, "Data"), True)
      hh_flgft.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130875832005809621, "FLG (**)"), 0, True)
      NTSFormAddDataBinding(dctesta, Me)
      NTSFormAddDataBinding(dcHhFor, Me)
      NTSFormAddDataBinding(dcHhRed, Me)
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub

  Public Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)

    If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non è per questa form ...
    MyBase.GestisciEventiEntity(sender, e)
    Try
      If e.TipoEvento.Length < 5 Then Return
      If Mid(e.TipoEvento, 1, 4) = "CPNE" Then
        Select Case e.TipoEvento
          Case ""

        End Select
      End If
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub

  Private Sub FRMDatiAgg_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    Try
      Dim oParam As New CLE__CLDP
      '--------------------------------------------
      'gestione dello zoom:
      'eseguo la Zoom, tanto se per il controllo non deve venir eseguito uno zoom particolare, la routine non fa nulla e viene processato lo zoom standard del controllo, settato con la NTSSetParamZoom
      If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
        If edet_coddest.Focused Then
          SetFastZoom(edet_coddest.Text, oParam)    'abilito la gestione dello zoom veloce
          NTSZOOM.strIn = edet_coddest.Text



          oParam.lContoCF = CType(oCleHh, CLFORGSOR).lContoCF   'passo il conto cliente/fornitore
          NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)


          If NTSZOOM.strIn <> edet_coddest.Text Then edet_coddest.NTSTextDB = NTSZOOM.strIn
          e.Handled = True    'altrimenti anche il controllo riceve l'F5 e la routine ZOOM viene eseguita 2 volte!!!
        End If
        If edhh_squadra.Focused Then

        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try

  End Sub



  Private Sub FRMYYHHHH_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    'lContoCF = CType(oCleHh, CLFORGSOR).lContoCF
    dsHh = oCleHh.dsShared
    dcHhFor = Nothing
    dcHhFor = New BindingSource()
    dcHhFor.DataSource = dsHh.Tables("CPNE.TotFor")
    dctesta = Nothing
    dctesta = New BindingSource()
    dctesta.DataSource = dsHh.Tables("testa")
    dcHhPag = Nothing
    dcHhPag = New BindingSource()
    dcHhPag.DataSource = dsHh.Tables("CPNE.ScaPag")
    dcHhRed = Nothing
    dcHhRed = New BindingSource()
    dcHhRed.DataSource = dsHh.Tables("CPNE.marg")
    AggGriglia()
    Bindcontrols()

    CType(oCleHh, CLFORGSOR).CPNEAggCampiAggiuntivi("et_conto")
    CType(oCleHh, CLFORGSOR).CPNEAggCampiAggiuntivi("et_coddest")
    CType(oCleHh, CLFORGSOR).CPNEAggiornaResiduoOrdine()
    CType(oCleHh, CLFORGSOR).CPNEAggiungiHandlerCPNEScaPag()

    grvTotFor.NTSAllowInsert = False
    grvTotFor.NTSAllowDelete = False
    grvTotFor.NTSAllowUpdate = False
    'grv.....Enabled = False

    If oCleHh.dsShared.Tables("TESTA").Rows(0)!hh_montag.ToString = "S" Then
      ckhh_mont.Checked = True
    Else
      ckhh_mont.Checked = False
    End If

    If oCleHh.dsShared.Tables("TESTA").Rows(0)!hh_finitu.ToString = "S" Then
      ckhh_finitu.Checked = True
    Else
      ckhh_finitu.Checked = False
    End If

    If oCleHh.dsShared.Tables("TESTA").Rows(0)!hh_ascens.ToString = "S" Then
      ckhh_ascens.Checked = True
    Else
      ckhh_ascens.Checked = False
    End If

    If oCleHh.dsShared.Tables("TESTA").Rows(0)!hh_elettr.ToString = "S" Then
      ckhh_elettr.Checked = True
    Else
      ckhh_elettr.Checked = False
    End If

    If oCleHh.dsShared.Tables("TESTA").Rows(0)!hh_finanz.ToString = "S" Then
      ckhh_finanz.Checked = True
    Else
      ckhh_finanz.Checked = False
    End If
    If Not IsNothing(grvScaPag.NTSGetCurrentDataRow()) Then
      If dsHh.Tables("CPNE.ScaPag").Rows.Count > 0 Then
        CPNEImpostaAbilitati(CType(oCleHh, CLFORGSOR).CPNEFtModificabile(grvScaPag.NTSGetCurrentDataRow))
      End If
    End If


    GctlSetRoules()
  End Sub

  Private Sub AggGriglia()
    dcHhGrFor = Nothing
    dcHhGrFor = New BindingSource()
    dcHhGrFor.DataSource = dsHh.Tables("CPNE.TotFor")
    grTotFor.DataSource = dcHhGrFor
    dcHhGrPag = Nothing
    dcHhGrPag = New BindingSource()
    dcHhGrPag.DataSource = dsHh.Tables("CPNE.ScaPag")
    grScaPag.DataSource = dcHhGrPag

  End Sub
  Private Sub grvScaPag_NTSBeforeRowUpdate(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvScaPag.NTSBeforeRowUpdate
    If CType(oCleHh, CLFORGSOR).CPNEValidaGriPagamenti(dcHhGrPag.Position) Then
      e.Allow = True
    Else
      e.Allow = False
    End If
  End Sub
  Private Sub FRMDatiAgg_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    ValidaLastControl()
    If CType(oCleHh, CLFORGSOR).CPNEValidaGriPagamenti(dcHhGrPag.Position) Then
      If CType(oCleHh, CLFORGSOR).CPNEValidaTotPagamenti() Then

      Else
        If oApp.MsgBoxInfoYesNo_DefNo("Attenzione la somma degli importi del Riepilogo Pagamenti non corrisponde al valore dell'ordine" & vbCr & "Si vuole continuare?") = vbYes Then

        Else
          e.Cancel = True
        End If

      End If
    Else
      e.Cancel = True
    End If
  End Sub

  Private Sub cmdCancellaRiga_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancellaRiga.Click
    'Dim dtrDeleted As DataRow = Nothing
    'Dim intRiga As Integer = oCleHh.dsShared.Tables("CPNE.ScaPag").Rows(dcHhGrPag.Position)!hh_riga
    'If Not grvScaPag.NTSDeleteRigaCorrente(dcHhGrPag, True, dtrDeleted) Then Return
    'CType(oCleHh, CLFORGSOR).RecordCancellaCPNEScaPag(intRiga)
    If Not IsNothing(grvScaPag.NTSGetCurrentDataRow()) Then
      Dim bEnabled As Boolean = CType(oCleHh, CLFORGSOR).CPNEFtModificabile(grvScaPag.NTSGetCurrentDataRow)
      If bEnabled Then
        bEnabled = CType(oCleHh, CLFORGSOR).CPNEFtModificabileManuale(grvScaPag.NTSGetCurrentDataRow)
        If bEnabled = False Then
          If oApp.MsgBoxInfoYesNo_DefNo("La riga contiene riferimenti di fattura manuale.Cancellare riga?") = vbNo Then
            Exit Sub
          Else
            grvScaPag.NTSGetCurrentDataRow.Delete()
            dsHh.Tables("CPNE.ScaPag").AcceptChanges()
            CType(oCleHh, CLFORGSOR).CPNEAggiornaResiduoOrdine()
            Exit Sub
          End If
        End If
        If oApp.MsgBoxInfoYesNo_DefNo("Cancellare riga?") = vbYes Then
          grvScaPag.NTSGetCurrentDataRow.Delete()
          dsHh.Tables("CPNE.ScaPag").AcceptChanges()
          CType(oCleHh, CLFORGSOR).CPNEAggiornaResiduoOrdine()
        End If
      Else
        oApp.MsgBoxInfo("Riga non cancellabile. Contiene riferimenti di fattura automatica")
      End If
    End If
  End Sub



  Private Sub ckhh_mont_CheckedChanged(sender As Object, e As System.EventArgs) Handles ckhh_mont.CheckedChanged
    If ckhh_mont.Checked Then
      oCleHh.dsShared.Tables("TESTA").Rows(0)!hh_montag = "S"
    Else
      oCleHh.dsShared.Tables("TESTA").Rows(0)!hh_montag = "N"
    End If

  End Sub

  Private Sub ckhh_finitu_CheckedChanged(sender As Object, e As System.EventArgs) Handles ckhh_finitu.CheckedChanged
    If ckhh_finitu.Checked Then
      oCleHh.dsShared.Tables("TESTA").Rows(0)!hh_finitu = "S"
    Else
      oCleHh.dsShared.Tables("TESTA").Rows(0)!hh_finitu = "N"
    End If
  End Sub

  Private Sub ckhh_ascens_CheckedChanged(sender As Object, e As System.EventArgs) Handles ckhh_ascens.CheckedChanged
    If ckhh_ascens.Checked Then
      oCleHh.dsShared.Tables("TESTA").Rows(0)!hh_ascens = "S"
    Else
      oCleHh.dsShared.Tables("TESTA").Rows(0)!hh_ascens = "N"
    End If
  End Sub

  Private Sub ckhh_elettr_CheckedChanged(sender As Object, e As System.EventArgs) Handles ckhh_elettr.CheckedChanged
    If ckhh_elettr.Checked Then
      oCleHh.dsShared.Tables("TESTA").Rows(0)!hh_elettr = "S"
    Else
      oCleHh.dsShared.Tables("TESTA").Rows(0)!hh_elettr = "N"
    End If
  End Sub

  Private Sub ckhh_finanz_CheckedChanged(sender As Object, e As System.EventArgs) Handles ckhh_finanz.CheckedChanged
    If ckhh_finanz.Checked Then
      oCleHh.dsShared.Tables("TESTA").Rows(0)!hh_finanz = "S"
    Else
      oCleHh.dsShared.Tables("TESTA").Rows(0)!hh_finanz = "N"
    End If
  End Sub
  Public Overridable Sub grvScaPag_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvScaPag.NTSFocusedRowChanged
    If Not IsNothing(grvScaPag.NTSGetCurrentDataRow) Then
      CPNEImpostaAbilitati(CType(oCleHh, CLFORGSOR).CPNEFtModificabile(grvScaPag.NTSGetCurrentDataRow))
    End If
  End Sub
  Private Sub CPNEImpostaAbilitati(bEnabled As Boolean)
    If bEnabled Then
      hh_imppaga.Enabled = True
      'hh_flgpaga.Enabled = True
      'hh_flgft.Enabled = True
      hh_numft.Enabled = True
      hh_serieft.Enabled = True
      hh_dataft.Enabled = True
    Else
      hh_imppaga.Enabled = False
      'hh_flgpaga.Enabled = False
      'hh_flgft.Enabled = False
      hh_numft.Enabled = False
      hh_serieft.Enabled = False
      hh_dataft.Enabled = False
    End If
  End Sub

End Class