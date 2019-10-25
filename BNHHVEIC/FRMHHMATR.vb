Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO
Imports System.Data.OleDb
'Imports Microsoft.Office.Interop
'Imports System.Runtime.InteropServices.Marshal
'Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Drawing.Printing

Public Class FRMHHMATR

#Region "Standard"
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = bsModExtAll
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Private ModuliPtnExt_P As Integer = 0

  Dim IntTipoStampa As Integer = 0 ' 0 = video, 1 = carta
  Public strNomefile1 As String

  Public dcHhMatricole As BindingSource = New BindingSource

  Public bPassatodaSeleziona As Boolean = False

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
    'creo e attivo l'entity e inizializzo la funzione che dovr√† rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNHHMATR", "BEHHMATR", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128271029889882656, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleHh = CType(oTmp, CLEHHMATR)
    oCleHh.AssociaDs(dsHh)
    oCleHh.OMenu = oMenu
    '------------------------------------------------

    AddHandler oCleHh.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleHh.Init(oApp, NTSScript, oMenu.oCleComm, "", False, "", "") = False Then Return False

    Return True
  End Function
  Public Overridable Sub InitControls()
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try

      Catch ex As Exception
        'non gestisco l'errore: se non c'Ë una immagine prendo quella standard
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

  Public Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMHHMATR))
    Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
    Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
    Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog()
    Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
    Me.NtsButton1 = New NTSInformatica.NTSButton()
    Me.Tlb_Seleziona = New NTSInformatica.NTSButton()
    Me.NtsGroupBox2 = New NTSInformatica.NTSGroupBox()
    Me.MATRICOLE = New NTSInformatica.NTSGroupBox()
    Me.grMatricole = New NTSInformatica.NTSGrid()
    Me.grvMatricole = New NTSInformatica.NTSGridView()
    Me.rl_matric = New NTSInformatica.NTSGridColumn()
    Me.rl_dtrilascio = New NTSInformatica.NTSGridColumn()
    Me.rl_note = New NTSInformatica.NTSGridColumn()
    Me.rl_tipomatr = New NTSInformatica.NTSGridColumn()
    Me.rl_dtscadgarven = New NTSInformatica.NTSGridColumn()
    Me.rl_dtscadgaracq = New NTSInformatica.NTSGridColumn()
    Me.rl_codart = New NTSInformatica.NTSGridColumn()
    Me.rl_conto = New NTSInformatica.NTSGridColumn()
    Me.Tlb_Ricerca = New NTSInformatica.NTSButton()
    Me.edxx_matricola = New NTSInformatica.NTSTextBoxStr()
    Me.NtsLabel1 = New NTSInformatica.NTSLabel()
    Me.NtsLabel2 = New NTSInformatica.NTSLabel()
    Me.edxx_codconto = New NTSInformatica.NTSTextBoxStr()
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox2.SuspendLayout()
    CType(Me.MATRICOLE, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.MATRICOLE.SuspendLayout()
    CType(Me.grMatricole, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvMatricole, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_matricola.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_codconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'PrintDialog1
    '
    Me.PrintDialog1.UseEXDialog = True
    '
    'PrintPreviewDialog1
    '
    Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
    Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
    Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
    Me.PrintPreviewDialog1.Enabled = True
    Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
    Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
    Me.PrintPreviewDialog1.Visible = False
    '
    'NtsButton1
    '
    Me.NtsButton1.Image = CType(resources.GetObject("NtsButton1.Image"), System.Drawing.Image)
    Me.NtsButton1.Location = New System.Drawing.Point(4, 24)
    Me.NtsButton1.Name = "NtsButton1"
    Me.NtsButton1.Size = New System.Drawing.Size(40, 44)
    '
    'Tlb_Seleziona
    '
    Me.Tlb_Seleziona.Location = New System.Drawing.Point(592, 24)
    Me.Tlb_Seleziona.Name = "Tlb_Seleziona"
    Me.Tlb_Seleziona.Size = New System.Drawing.Size(76, 26)
    Me.Tlb_Seleziona.Text = "Seleziona"
    '
    'NtsGroupBox2
    '
    Me.NtsGroupBox2.AllowDrop = True
    Me.NtsGroupBox2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox2.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox2.Controls.Add(Me.edxx_codconto)
    Me.NtsGroupBox2.Controls.Add(Me.NtsLabel2)
    Me.NtsGroupBox2.Controls.Add(Me.NtsLabel1)
    Me.NtsGroupBox2.Controls.Add(Me.edxx_matricola)
    Me.NtsGroupBox2.Controls.Add(Me.Tlb_Ricerca)
    Me.NtsGroupBox2.Controls.Add(Me.NtsButton1)
    Me.NtsGroupBox2.Controls.Add(Me.Tlb_Seleziona)
    Me.NtsGroupBox2.Dock = System.Windows.Forms.DockStyle.Top
    Me.NtsGroupBox2.Location = New System.Drawing.Point(0, 0)
    Me.NtsGroupBox2.Name = "NtsGroupBox2"
    Me.NtsGroupBox2.Size = New System.Drawing.Size(684, 132)
    Me.NtsGroupBox2.Text = "FILTRA MATRICOLE"
    '
    'MATRICOLE
    '
    Me.MATRICOLE.AllowDrop = True
    Me.MATRICOLE.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.MATRICOLE.Appearance.Options.UseBackColor = True
    Me.MATRICOLE.Controls.Add(Me.grMatricole)
    Me.MATRICOLE.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.MATRICOLE.Location = New System.Drawing.Point(0, 136)
    Me.MATRICOLE.Name = "MATRICOLE"
    Me.MATRICOLE.Size = New System.Drawing.Size(684, 228)
    Me.MATRICOLE.Text = "MATRICOLE"
    '
    'grMatricole
    '
    Me.grMatricole.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grMatricole.Location = New System.Drawing.Point(2, 21)
    Me.grMatricole.MainView = Me.grvMatricole
    Me.grMatricole.Name = "grMatricole"
    Me.grMatricole.Size = New System.Drawing.Size(680, 205)
    Me.grMatricole.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvMatricole})
    '
    'grvMatricole
    '
    Me.grvMatricole.Appearance.FocusedCell.BackColor = System.Drawing.Color.FloralWhite
    Me.grvMatricole.Appearance.FocusedCell.Options.UseBackColor = True
    Me.grvMatricole.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.rl_matric, Me.rl_dtrilascio, Me.rl_note, Me.rl_tipomatr, Me.rl_dtscadgarven, Me.rl_dtscadgaracq, Me.rl_codart, Me.rl_conto})
    Me.grvMatricole.Enabled = True
    Me.grvMatricole.GridControl = Me.grMatricole
    Me.grvMatricole.Name = "grvMatricole"
    Me.grvMatricole.OptionsCustomization.AllowRowSizing = True
    Me.grvMatricole.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvMatricole.OptionsNavigation.UseTabKey = False
    Me.grvMatricole.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvMatricole.OptionsView.ColumnAutoWidth = False
    Me.grvMatricole.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvMatricole.OptionsView.ShowGroupPanel = False
    '
    'rl_matric
    '
    Me.rl_matric.AppearanceCell.Options.UseBackColor = True
    Me.rl_matric.AppearanceCell.Options.UseTextOptions = True
    Me.rl_matric.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.rl_matric.Caption = "Matricola"
    Me.rl_matric.Enabled = True
    Me.rl_matric.FieldName = "rl_matric"
    Me.rl_matric.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.rl_matric.Name = "rl_matric"
    Me.rl_matric.Visible = True
    Me.rl_matric.VisibleIndex = 0
    '
    'rl_dtrilascio
    '
    Me.rl_dtrilascio.AppearanceCell.Options.UseBackColor = True
    Me.rl_dtrilascio.AppearanceCell.Options.UseTextOptions = True
    Me.rl_dtrilascio.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.rl_dtrilascio.Caption = "Data rilascio"
    Me.rl_dtrilascio.Enabled = True
    Me.rl_dtrilascio.FieldName = "rl_dtrilascio"
    Me.rl_dtrilascio.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.rl_dtrilascio.Name = "rl_dtrilascio"
    Me.rl_dtrilascio.Visible = True
    Me.rl_dtrilascio.VisibleIndex = 1
    '
    'rl_note
    '
    Me.rl_note.AppearanceCell.Options.UseBackColor = True
    Me.rl_note.AppearanceCell.Options.UseTextOptions = True
    Me.rl_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.rl_note.Caption = "Note"
    Me.rl_note.Enabled = True
    Me.rl_note.FieldName = "rl_note"
    Me.rl_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.rl_note.Name = "rl_note"
    Me.rl_note.Visible = True
    Me.rl_note.VisibleIndex = 2
    '
    'rl_tipomatr
    '
    Me.rl_tipomatr.AppearanceCell.Options.UseBackColor = True
    Me.rl_tipomatr.AppearanceCell.Options.UseTextOptions = True
    Me.rl_tipomatr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.rl_tipomatr.Caption = "Tipo matr."
    Me.rl_tipomatr.Enabled = True
    Me.rl_tipomatr.FieldName = "rl_tipomatr"
    Me.rl_tipomatr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.rl_tipomatr.Name = "rl_tipomatr"
    Me.rl_tipomatr.Visible = True
    Me.rl_tipomatr.VisibleIndex = 3
    '
    'rl_dtscadgarven
    '
    Me.rl_dtscadgarven.AppearanceCell.Options.UseBackColor = True
    Me.rl_dtscadgarven.AppearanceCell.Options.UseTextOptions = True
    Me.rl_dtscadgarven.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.rl_dtscadgarven.Caption = "Data scad.Garanzia Ven"
    Me.rl_dtscadgarven.Enabled = True
    Me.rl_dtscadgarven.FieldName = "rl_dtscadgarven"
    Me.rl_dtscadgarven.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.rl_dtscadgarven.Name = "rl_dtscadgarven"
    Me.rl_dtscadgarven.Visible = True
    Me.rl_dtscadgarven.VisibleIndex = 4
    '
    'rl_dtscadgaracq
    '
    Me.rl_dtscadgaracq.AppearanceCell.Options.UseBackColor = True
    Me.rl_dtscadgaracq.AppearanceCell.Options.UseTextOptions = True
    Me.rl_dtscadgaracq.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.rl_dtscadgaracq.Caption = "Data scad.Garanzia Acq"
    Me.rl_dtscadgaracq.Enabled = True
    Me.rl_dtscadgaracq.FieldName = "rl_dtscadgaracq"
    Me.rl_dtscadgaracq.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.rl_dtscadgaracq.Name = "rl_dtscadgaracq"
    Me.rl_dtscadgaracq.Visible = True
    Me.rl_dtscadgaracq.VisibleIndex = 5
    '
    'rl_codart
    '
    Me.rl_codart.AppearanceCell.Options.UseBackColor = True
    Me.rl_codart.AppearanceCell.Options.UseTextOptions = True
    Me.rl_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.rl_codart.Caption = "Cod.Articolo"
    Me.rl_codart.Enabled = True
    Me.rl_codart.FieldName = "rl_codart"
    Me.rl_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.rl_codart.Name = "rl_codart"
    Me.rl_codart.Visible = True
    Me.rl_codart.VisibleIndex = 6
    '
    'rl_conto
    '
    Me.rl_conto.AppearanceCell.Options.UseBackColor = True
    Me.rl_conto.AppearanceCell.Options.UseTextOptions = True
    Me.rl_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.rl_conto.Caption = "Cod.Conto"
    Me.rl_conto.Enabled = True
    Me.rl_conto.FieldName = "rl_conto"
    Me.rl_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.rl_conto.Name = "rl_conto"
    Me.rl_conto.Visible = True
    Me.rl_conto.VisibleIndex = 7
    '
    'Tlb_Ricerca
    '
    Me.Tlb_Ricerca.Location = New System.Drawing.Point(592, 72)
    Me.Tlb_Ricerca.Name = "Tlb_Ricerca"
    Me.Tlb_Ricerca.Size = New System.Drawing.Size(76, 26)
    Me.Tlb_Ricerca.Text = "Ricerca"
    '
    'edxx_matricola
    '
    Me.edxx_matricola.EditValue = ""
    Me.edxx_matricola.Location = New System.Drawing.Point(184, 48)
    Me.edxx_matricola.Name = "edxx_matricola"
    Me.edxx_matricola.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_matricola.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_matricola.Properties.AutoHeight = False
    Me.edxx_matricola.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_matricola.Properties.MaxLength = 65536
    Me.edxx_matricola.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_matricola.Size = New System.Drawing.Size(100, 20)
    '
    'NtsLabel1
    '
    Me.NtsLabel1.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel1.Location = New System.Drawing.Point(84, 48)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.Size = New System.Drawing.Size(100, 20)
    Me.NtsLabel1.Text = "Matricola"
    Me.NtsLabel1.UseMnemonic = False
    '
    'NtsLabel2
    '
    Me.NtsLabel2.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel2.Location = New System.Drawing.Point(84, 72)
    Me.NtsLabel2.Name = "NtsLabel2"
    Me.NtsLabel2.Size = New System.Drawing.Size(100, 20)
    Me.NtsLabel2.Text = "Cod.Conto"
    Me.NtsLabel2.UseMnemonic = False
    '
    'edxx_codconto
    '
    Me.edxx_codconto.EditValue = ""
    Me.edxx_codconto.Location = New System.Drawing.Point(184, 72)
    Me.edxx_codconto.Name = "edxx_codconto"
    Me.edxx_codconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_codconto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_codconto.Properties.AutoHeight = False
    Me.edxx_codconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_codconto.Properties.MaxLength = 65536
    Me.edxx_codconto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_codconto.Size = New System.Drawing.Size(100, 20)
    '
    'FRMHHMATR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(684, 363)
    Me.Controls.Add(Me.MATRICOLE)
    Me.Controls.Add(Me.NtsGroupBox2)
    Me.Name = "FRMHHMATR"
    Me.Text = "VISUALIZZAZIONE MATRICOLE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox2.ResumeLayout(False)
    CType(Me.MATRICOLE, System.ComponentModel.ISupportInitialize).EndInit()
    Me.MATRICOLE.ResumeLayout(False)
    CType(Me.grMatricole, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvMatricole, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_matricola.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_codconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
#End Region

  Private components As System.ComponentModel.IContainer
  Public oCleHh As CLEHHMATR
  Public dsHh As New DataSet
  Public oCallParams As CLE__CLDP
  Public dcHh As BindingSource = New BindingSource
  'Dim exApp As New Excel.Application

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano gi‡† stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 

      '================ riccardo 30 04 2018 ======================
      'NtsFlowLayoutPanel1.NTSSetParam(oMenu, oApp.Tr(Me, 131698361043986767, ""))

      grvMatricole.NTSSetParam(oMenu, oApp.Tr(Me, 131881231461336930, "griglia matricole"))
      rl_matric.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131881274997755926, "Nr.matricola"), 0, True)
      rl_dtrilascio.NTSSetParamDATA(oMenu, oApp.Tr(Me, 131881274997765921, "Data rilascio"), True)
      rl_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131881274997775915, "Note"), 0, True)
      rl_tipomatr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131881274997785909, "Tipo Matricola"), 0, True)
      rl_dtscadgarven.NTSSetParamDATA(oMenu, oApp.Tr(Me, 131881274997795904, "Data scad.garanzia Ven"), True)
      rl_dtscadgaracq.NTSSetParamDATA(oMenu, oApp.Tr(Me, 131881274997805898, "Data scad.garanzia Acq"), True)

      rl_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 132078197722428717, "Cod.Articolo"), 0, True)
      rl_conto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 132078197722438712, "Cod.Conto"), "0", 9, 0, 999999999)
      edxx_codconto.NTSDbField = "XXX.xx_codconto"
      edxx_matricola.NTSDbField = "XXX.xx_matricola"
      edxx_codconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 132078197722988402, "Cod.Conto"), tabanagrac, True)
      edxx_matricola.NTSSetParam(oMenu, oApp.Tr(Me, 132078197723018381, "Matricola"), 50)

      '================================================================

      NTSFormAddDataBinding(dcHh, Me)
      '-------------------------------------------------
      'per agganciare al dataset i vari controlli

    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub

  Private Sub FRMHHMATR_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    If bPassatodaSeleziona = False Then
      oCallParams.strBanc1 = ""
    End If

  End Sub

  Private Sub FRM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    Dim intCodcli As Integer = 0
    Dim strcodart As String = ""

    dsHh = oCleHh.dsShared
    dcHh = Nothing
    dcHh = New BindingSource()
    dcHh.DataSource = dsHh.Tables("XXX")

    Bindcontrols()
    GctlSetRoules()
    GctlApplicaDefaultValue()

    If IsNothing(oCallParams) Then
      'edxx_codCli.Text = "0"
      'CmdSeleziona.Text = "Salva"
    Else
      intCodcli = CInt(oCallParams.dPar1)
      strcodart = oCallParams.strBanc1
    End If

    oCleHh.CPNECaricaDTMatricole(intCodcli, strcodart)

    dcHhMatricole = Nothing
    dcHhMatricole = New BindingSource()
    dcHhMatricole.DataSource = dsHh.Tables("NNMATRICS")
    grMatricole.DataSource = dcHhMatricole

  End Sub

  Private Sub FRMHHMATR_ActivatedFirst(sender As Object, e As EventArgs) Handles Me.ActivatedFirst
    Dim intCodcli As Integer = 0
    'Dim strcodart As String = ""
    'If IsNothing(oCallParams) Then
    '  'edxx_codCli.Text = "0"
    '  'CmdSeleziona.Text = "Salva"
    'Else
    intCodcli = CInt(oCallParams.dPar1)
    edxx_codconto.Text = intCodcli.ToString
    '  strcodart = oCallParams.strBanc1
    '  oCallParams.strPar1 = ""
    'End If
    'ValidaLastControl()
    'oCleHh.CPNECaricaDTMatricole(intCodCli, strcodart)
  End Sub

  Public Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)

    If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non Ë per questa form ...
    MyBase.GestisciEventiEntity(sender, e)
    Try
      If e.TipoEvento.Length < 5 Then Return
      If Mid(e.TipoEvento, 1, 4) = "CPNE" Then
        Select Case e.TipoEvento
          'Case "CPNE.NUMRIGHE"
          '  lbnumrighe.Text = e.Message
          'Me.Refresh()
          'System.Windows.Forms.Application.DoEvents()
        End Select
      End If
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub

  Private Sub NtsButton1_Click_1(sender As Object, e As EventArgs) Handles NtsButton1.Click
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub

  Private Sub Tlb_Seleziona_Click(sender As Object, e As EventArgs) Handles Tlb_Seleziona.Click
    Try

      'passo il numero di matricola selezionato al child chiamante
      If grvMatricole.NTSGetCurrentDataRow IsNot Nothing Then
        oCallParams.strBanc1 = grvMatricole.NTSGetCurrentDataRow!rl_matric.ToString
      Else
        oCallParams.strBanc1 = ""
      End If
      ValidaLastControl()
      bPassatodaSeleziona = True
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub

  Private Sub Tlb_Ricerca_Click(sender As Object, e As EventArgs) Handles Tlb_Ricerca.Click

    If IsNumeric(edxx_codconto.Text) = True Then
    Else
      edxx_codconto.Text = "0"
    End If

    oCleHh.CPNECaricaDTMatricoleconfiltri(CInt(edxx_codconto.Text), edxx_matricola.Text)

    dcHhMatricole = Nothing
    dcHhMatricole = New BindingSource()
    dcHhMatricole.DataSource = dsHh.Tables("NNMATRICS")
    grMatricole.DataSource = dcHhMatricole

  End Sub
End Class



