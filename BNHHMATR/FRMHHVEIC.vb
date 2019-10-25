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

  Public dcHhVeicoli As BindingSource = New BindingSource

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
    Me.Tlb_ricerca = New NTSInformatica.NTSButton()
    Me.NtsGroupBox2 = New NTSInformatica.NTSGroupBox()
    Me.NtsLabel1 = New NTSInformatica.NTSLabel()
    Me.edxx_descr = New NTSInformatica.NTSTextBoxStr()
    Me.lbxx_Cliente = New NTSInformatica.NTSLabel()
    Me.edxx_codCli = New NTSInformatica.NTSTextBoxNum()
    Me.VEICOLI = New NTSInformatica.NTSGroupBox()
    Me.grVeicoli = New NTSInformatica.NTSGrid()
    Me.grvVeicoli = New NTSInformatica.NTSGridView()
    Me.hh_tipo = New NTSInformatica.NTSGridColumn()
    Me.hh_targa = New NTSInformatica.NTSGridColumn()
    Me.hh_dataimmatr = New NTSInformatica.NTSGridColumn()
    Me.hh_telaio = New NTSInformatica.NTSGridColumn()
    Me.hh_nrmotore = New NTSInformatica.NTSGridColumn()
    Me.hh_km = New NTSInformatica.NTSGridColumn()
    Me.hh_note = New NTSInformatica.NTSGridColumn()
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox2.SuspendLayout()
    CType(Me.edxx_descr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_codCli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.VEICOLI, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.VEICOLI.SuspendLayout()
    CType(Me.grVeicoli, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvVeicoli, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.Tlb_Seleziona.Location = New System.Drawing.Point(592, 64)
    Me.Tlb_Seleziona.Name = "Tlb_Seleziona"
    Me.Tlb_Seleziona.Size = New System.Drawing.Size(76, 26)
    Me.Tlb_Seleziona.Text = "Seleziona"
    '
    'Tlb_ricerca
    '
    Me.Tlb_ricerca.Location = New System.Drawing.Point(592, 24)
    Me.Tlb_ricerca.Name = "Tlb_ricerca"
    Me.Tlb_ricerca.Size = New System.Drawing.Size(76, 26)
    Me.Tlb_ricerca.Text = "Ricerca"
    '
    'NtsGroupBox2
    '
    Me.NtsGroupBox2.AllowDrop = True
    Me.NtsGroupBox2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox2.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox2.Controls.Add(Me.NtsLabel1)
    Me.NtsGroupBox2.Controls.Add(Me.edxx_descr)
    Me.NtsGroupBox2.Controls.Add(Me.lbxx_Cliente)
    Me.NtsGroupBox2.Controls.Add(Me.edxx_codCli)
    Me.NtsGroupBox2.Controls.Add(Me.NtsButton1)
    Me.NtsGroupBox2.Controls.Add(Me.Tlb_ricerca)
    Me.NtsGroupBox2.Controls.Add(Me.Tlb_Seleziona)
    Me.NtsGroupBox2.Dock = System.Windows.Forms.DockStyle.Top
    Me.NtsGroupBox2.Location = New System.Drawing.Point(0, 0)
    Me.NtsGroupBox2.Name = "NtsGroupBox2"
    Me.NtsGroupBox2.Size = New System.Drawing.Size(673, 100)
    Me.NtsGroupBox2.Text = "FILTRO"
    '
    'NtsLabel1
    '
    Me.NtsLabel1.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel1.Location = New System.Drawing.Point(112, 48)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.Size = New System.Drawing.Size(100, 20)
    Me.NtsLabel1.Text = "Descrizione"
    Me.NtsLabel1.UseMnemonic = False
    '
    'edxx_descr
    '
    Me.edxx_descr.EditValue = ""
    Me.edxx_descr.Location = New System.Drawing.Point(228, 48)
    Me.edxx_descr.Name = "edxx_descr"
    Me.edxx_descr.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edxx_descr.Properties.Appearance.Options.UseBackColor = True
    Me.edxx_descr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_descr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_descr.Properties.AutoHeight = False
    Me.edxx_descr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_descr.Properties.MaxLength = 65536
    Me.edxx_descr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_descr.Size = New System.Drawing.Size(312, 20)
    '
    'lbxx_Cliente
    '
    Me.lbxx_Cliente.BackColor = System.Drawing.Color.Transparent
    Me.lbxx_Cliente.Location = New System.Drawing.Point(112, 24)
    Me.lbxx_Cliente.Name = "lbxx_Cliente"
    Me.lbxx_Cliente.Size = New System.Drawing.Size(100, 20)
    Me.lbxx_Cliente.Text = "Cod.Cliente"
    Me.lbxx_Cliente.UseMnemonic = False
    '
    'edxx_codCli
    '
    Me.edxx_codCli.EditValue = ""
    Me.edxx_codCli.Location = New System.Drawing.Point(228, 24)
    Me.edxx_codCli.Name = "edxx_codCli"
    Me.edxx_codCli.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_codCli.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_codCli.Properties.AutoHeight = False
    Me.edxx_codCli.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_codCli.Properties.MaxLength = 65536
    Me.edxx_codCli.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_codCli.Size = New System.Drawing.Size(100, 20)
    '
    'VEICOLI
    '
    Me.VEICOLI.AllowDrop = True
    Me.VEICOLI.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.VEICOLI.Appearance.Options.UseBackColor = True
    Me.VEICOLI.Controls.Add(Me.grVeicoli)
    Me.VEICOLI.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.VEICOLI.Location = New System.Drawing.Point(0, 124)
    Me.VEICOLI.Name = "VEICOLI"
    Me.VEICOLI.Size = New System.Drawing.Size(673, 188)
    Me.VEICOLI.Text = "VEICOLI"
    '
    'grVeicoli
    '
    Me.grVeicoli.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grVeicoli.Location = New System.Drawing.Point(2, 21)
    Me.grVeicoli.MainView = Me.grvVeicoli
    Me.grVeicoli.Name = "grVeicoli"
    Me.grVeicoli.Size = New System.Drawing.Size(669, 165)
    Me.grVeicoli.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvVeicoli})
    '
    'grvVeicoli
    '
    Me.grvVeicoli.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.hh_tipo, Me.hh_targa, Me.hh_dataimmatr, Me.hh_telaio, Me.hh_nrmotore, Me.hh_km, Me.hh_note})
    Me.grvVeicoli.Enabled = True
    Me.grvVeicoli.GridControl = Me.grVeicoli
    Me.grvVeicoli.Name = "grvVeicoli"
    Me.grvVeicoli.OptionsCustomization.AllowRowSizing = True
    Me.grvVeicoli.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvVeicoli.OptionsNavigation.UseTabKey = False
    Me.grvVeicoli.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvVeicoli.OptionsView.ColumnAutoWidth = False
    Me.grvVeicoli.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvVeicoli.OptionsView.ShowGroupPanel = False
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
    'hh_targa
    '
    Me.hh_targa.AppearanceCell.Options.UseBackColor = True
    Me.hh_targa.AppearanceCell.Options.UseTextOptions = True
    Me.hh_targa.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_targa.Caption = "Targa"
    Me.hh_targa.Enabled = True
    Me.hh_targa.FieldName = "hh_targa"
    Me.hh_targa.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_targa.Name = "hh_targa"
    Me.hh_targa.Visible = True
    Me.hh_targa.VisibleIndex = 1
    '
    'hh_dataimmatr
    '
    Me.hh_dataimmatr.AppearanceCell.Options.UseBackColor = True
    Me.hh_dataimmatr.AppearanceCell.Options.UseTextOptions = True
    Me.hh_dataimmatr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_dataimmatr.Caption = "Data Immatr."
    Me.hh_dataimmatr.Enabled = True
    Me.hh_dataimmatr.FieldName = "hh_dataimmatr"
    Me.hh_dataimmatr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_dataimmatr.Name = "hh_dataimmatr"
    Me.hh_dataimmatr.Visible = True
    Me.hh_dataimmatr.VisibleIndex = 2
    '
    'hh_telaio
    '
    Me.hh_telaio.AppearanceCell.Options.UseBackColor = True
    Me.hh_telaio.AppearanceCell.Options.UseTextOptions = True
    Me.hh_telaio.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_telaio.Caption = "Telaio"
    Me.hh_telaio.Enabled = True
    Me.hh_telaio.FieldName = "hh_telaio"
    Me.hh_telaio.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_telaio.Name = "hh_telaio"
    Me.hh_telaio.Visible = True
    Me.hh_telaio.VisibleIndex = 3
    '
    'hh_nrmotore
    '
    Me.hh_nrmotore.AppearanceCell.Options.UseBackColor = True
    Me.hh_nrmotore.AppearanceCell.Options.UseTextOptions = True
    Me.hh_nrmotore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_nrmotore.Caption = "Nr.Motore"
    Me.hh_nrmotore.Enabled = True
    Me.hh_nrmotore.FieldName = "hh_nrmotore"
    Me.hh_nrmotore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_nrmotore.Name = "hh_nrmotore"
    Me.hh_nrmotore.Visible = True
    Me.hh_nrmotore.VisibleIndex = 4
    '
    'hh_km
    '
    Me.hh_km.AppearanceCell.Options.UseBackColor = True
    Me.hh_km.AppearanceCell.Options.UseTextOptions = True
    Me.hh_km.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.hh_km.Caption = "Km"
    Me.hh_km.Enabled = True
    Me.hh_km.FieldName = "hh_km"
    Me.hh_km.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.hh_km.Name = "hh_km"
    Me.hh_km.Visible = True
    Me.hh_km.VisibleIndex = 5
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
    Me.hh_note.VisibleIndex = 6
    '
    'FRMHHMATR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(673, 311)
    Me.Controls.Add(Me.VEICOLI)
    Me.Controls.Add(Me.NtsGroupBox2)
    Me.Name = "FRMHHMATR"
    Me.Text = "GESTIONE PARCO VEICOLI CLIENTE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox2.ResumeLayout(False)
    CType(Me.edxx_descr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_codCli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.VEICOLI, System.ComponentModel.ISupportInitialize).EndInit()
    Me.VEICOLI.ResumeLayout(False)
    CType(Me.grVeicoli, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvVeicoli, System.ComponentModel.ISupportInitialize).EndInit()
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
      edxx_codCli.NTSDbField = "tabella.xx_codCli"
      edxx_codCli.NTSSetParamTabe(oMenu, oApp.Tr(Me, 131698361044347020, "Codice cliente"), tabanagrac)

      edxx_descr.NTSDbField = "XXX.xx_descr"
      edxx_descr.NTSSetParam(oMenu, oApp.Tr(Me, 131699185555158623, ""), 40)


      grvVeicoli.NTSSetParam(oMenu, oApp.Tr(Me, 131881231461336930, "griglia veicoli"))
      hh_tipo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131881274997755926, "Tipo"), 0, True)
      hh_targa.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131881274997765921, "Targa"), 0, True)
      hh_dataimmatr.NTSSetParamDATA(oMenu, oApp.Tr(Me, 131881274997775915, "Data Immatr."), True)
      hh_telaio.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131881274997785909, "Telaio"), 0, True)
      hh_nrmotore.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131881274997795904, "Nr.Motore"), 0, True)
      hh_km.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131881274997805898, "Km"), "0", 9, 0, 999999999)
      hh_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131881274997815892, "Note"), 0, True)


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
      oCallParams.dPar1 = CDec(oCallParams.strBanc1)
    End If

  End Sub

  Private Sub FRM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    dsHh = oCleHh.dsShared
    dcHh = Nothing
    dcHh = New BindingSource()
    dcHh.DataSource = dsHh.Tables("XXX")

    Bindcontrols()
    GctlSetRoules()
    GctlApplicaDefaultValue()

    '================== 30 04 2018 ==================
    ' aggiungere il caricamento della griglia con i veicoli
    oCleHh.CPNECaricaDTVeicoli(0)

    dcHhVeicoli = Nothing
    dcHhVeicoli = New BindingSource()
    dcHhVeicoli.DataSource = dsHh.Tables("HHVEICOLI")
    grVeicoli.DataSource = dcHhVeicoli

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
    Me.Close()
  End Sub

  Private Sub Tlb_ricerca_Click(sender As Object, e As EventArgs) Handles Tlb_ricerca.Click

    If IsNumeric(edxx_codCli.Text) = True Then
    Else
      edxx_codCli.Text = "0"
    End If

    edxx_descr.Text = oCleHh.LeggiAnagra(DittaCorrente, CInt(edxx_codCli.Text))

    oCleHh.CPNECaricaDTVeicoli(CInt(edxx_codCli.Text))

    dcHhVeicoli = Nothing
    dcHhVeicoli = New BindingSource()
    dcHhVeicoli.DataSource = dsHh.Tables("HHVEICOLI")
    grVeicoli.DataSource = dcHhVeicoli

  End Sub

  Private Sub Tlb_Seleziona_Click(sender As Object, e As EventArgs) Handles Tlb_Seleziona.Click

    For i = 0 To dsHh.Tables("HHVEICOLI").Rows.Count - 1
      dsHh.Tables("HHVEICOLI").Rows(i)!hh_codcli = CInt(edxx_codCli.Text)
    Next

    oCleHh.SalvaGriglia()

    'passo il numero di telaio selezionato al child chiamante
    oCallParams.dPar1 = CDec(grvVeicoli.NTSGetCurrentDataRow!hh_telaio)
    bPassatodaSeleziona = True
    Me.Close()
  End Sub

  Public Overrides Function NTSGetDataAutocompletamento(ByVal strTabName As String, ByVal strDescr As String,
   ByVal IsCrmUser As Boolean, ByRef dsOut As DataSet) As Boolean
    '----------------------------------------------------------------------------------------------------------- 
    '--- Modifico la funzione standard dell'autocompletamento per fare in modo che da edxx_codCli 
    '--- vengano visti solo clienti o fornitori 
    '----------------------------------------------------------------------------------------------------------- 
    Try
      '--------------------------------------------------------------------------------------------------------- 
      If edxx_codCli.ContainsFocus Then
        'If oCleClie.strTipoConto = "C" Then 
        strTabName = "ANAGRA_CLI"
        'Else 
        '  strTabName = "ANAGRA_FOR" 
        'End If 
        'If oApp.oGvar.bAutoCompleteIgnoraCF Then strTabName = "ANAGRACF" 
      End If
      '--------------------------------------------------------------------------------------------------------- 
      Return MyBase.NTSGetDataAutocompletamento(strTabName, strDescr, IsCrmUser, dsOut)
    Catch ex As Exception
      CLN__STD.GestErr(ex, Me, "")
    End Try
  End Function

End Class



