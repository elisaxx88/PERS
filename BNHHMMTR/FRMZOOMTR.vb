Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO
Public Class FRMZOOMTR
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
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Sub InitializeComponent()
    Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
    Me.NtsLabel1 = New NTSInformatica.NTSLabel()
    Me.edxx_matricola = New NTSInformatica.NTSTextBoxStr()
    Me.cmdRicerca = New NTSInformatica.NTSButton()
    Me.grdMatricole = New NTSInformatica.NTSGrid()
    Me.grvMatricole = New NTSInformatica.NTSGridView()
    Me.xx_matricola = New NTSInformatica.NTSGridColumn()
    Me.xx_matrproduttore = New NTSInformatica.NTSGridColumn()
    Me.xx_codart = New NTSInformatica.NTSGridColumn()
    Me.xx_note = New NTSInformatica.NTSGridColumn()
    Me.xx_contocli = New NTSInformatica.NTSGridColumn()
    Me.xx_descr1 = New NTSInformatica.NTSGridColumn()
    Me.xx_datainstallazione = New NTSInformatica.NTSGridColumn()
    Me.xx_dataritiro = New NTSInformatica.NTSGridColumn()
    Me.xx_ubicazione = New NTSInformatica.NTSGridColumn()
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_matricola.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grdMatricole, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvMatricole, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'NtsLabel1
    '
    Me.NtsLabel1.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel1.Location = New System.Drawing.Point(4, 28)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.Size = New System.Drawing.Size(100, 20)
    Me.NtsLabel1.Text = "Matricola"
    Me.NtsLabel1.UseMnemonic = False
    '
    'edxx_matricola
    '
    Me.edxx_matricola.EditValue = ""
    Me.edxx_matricola.Location = New System.Drawing.Point(112, 28)
    Me.edxx_matricola.Name = "edxx_matricola"
    Me.edxx_matricola.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_matricola.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_matricola.Properties.AutoHeight = False
    Me.edxx_matricola.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_matricola.Properties.MaxLength = 65536
    Me.edxx_matricola.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_matricola.Size = New System.Drawing.Size(228, 20)
    '
    'cmdRicerca
    '
    Me.cmdRicerca.Location = New System.Drawing.Point(4, 60)
    Me.cmdRicerca.Name = "cmdRicerca"
    Me.cmdRicerca.Size = New System.Drawing.Size(100, 26)
    Me.cmdRicerca.Text = "Ricerca"
    '
    'grdMatricole
    '
    Me.grdMatricole.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    GridLevelNode1.RelationName = "Level1"
    Me.grdMatricole.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
    Me.grdMatricole.Location = New System.Drawing.Point(4, 100)
    Me.grdMatricole.MainView = Me.grvMatricole
    Me.grdMatricole.Name = "grdMatricole"
    Me.grdMatricole.Size = New System.Drawing.Size(668, 224)
    Me.grdMatricole.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvMatricole})
    '
    'grvMatricole
    '
    Me.grvMatricole.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_matricola, Me.xx_matrproduttore, Me.xx_codart, Me.xx_note, Me.xx_contocli, Me.xx_descr1, Me.xx_datainstallazione, Me.xx_dataritiro, Me.xx_ubicazione})
    Me.grvMatricole.Enabled = True
    Me.grvMatricole.GridControl = Me.grdMatricole
    Me.grvMatricole.Name = "grvMatricole"
    Me.grvMatricole.OptionsCustomization.AllowRowSizing = True
    Me.grvMatricole.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvMatricole.OptionsNavigation.UseTabKey = False
    Me.grvMatricole.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvMatricole.OptionsView.ColumnAutoWidth = False
    Me.grvMatricole.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvMatricole.OptionsView.ShowGroupPanel = False
    '
    'xx_matricola
    '
    Me.xx_matricola.AppearanceCell.Options.UseBackColor = True
    Me.xx_matricola.AppearanceCell.Options.UseTextOptions = True
    Me.xx_matricola.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_matricola.Caption = "Cod. Matricola"
    Me.xx_matricola.Enabled = True
    Me.xx_matricola.FieldName = "xx_matricola"
    Me.xx_matricola.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_matricola.Name = "xx_matricola"
    Me.xx_matricola.Visible = True
    Me.xx_matricola.VisibleIndex = 0
    '
    'xx_matrproduttore
    '
    Me.xx_matrproduttore.AppearanceCell.Options.UseBackColor = True
    Me.xx_matrproduttore.AppearanceCell.Options.UseTextOptions = True
    Me.xx_matrproduttore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_matrproduttore.Caption = "Matr. Produttore"
    Me.xx_matrproduttore.Enabled = True
    Me.xx_matrproduttore.FieldName = "xx_matrproduttore"
    Me.xx_matrproduttore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_matrproduttore.Name = "xx_matrproduttore"
    Me.xx_matrproduttore.Visible = True
    Me.xx_matrproduttore.VisibleIndex = 1
    '
    'xx_codart
    '
    Me.xx_codart.AppearanceCell.Options.UseBackColor = True
    Me.xx_codart.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codart.Caption = "Cod. Articolo"
    Me.xx_codart.Enabled = True
    Me.xx_codart.FieldName = "xx_codart"
    Me.xx_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codart.Name = "xx_codart"
    Me.xx_codart.Visible = True
    Me.xx_codart.VisibleIndex = 2
    '
    'xx_note
    '
    Me.xx_note.AppearanceCell.Options.UseBackColor = True
    Me.xx_note.AppearanceCell.Options.UseTextOptions = True
    Me.xx_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_note.Caption = "Descrizione"
    Me.xx_note.Enabled = True
    Me.xx_note.FieldName = "xx_note"
    Me.xx_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_note.Name = "xx_note"
    Me.xx_note.Visible = True
    Me.xx_note.VisibleIndex = 3
    '
    'xx_contocli
    '
    Me.xx_contocli.AppearanceCell.Options.UseBackColor = True
    Me.xx_contocli.AppearanceCell.Options.UseTextOptions = True
    Me.xx_contocli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_contocli.Caption = "Cod. Clie"
    Me.xx_contocli.Enabled = True
    Me.xx_contocli.FieldName = "xx_contocli"
    Me.xx_contocli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_contocli.Name = "xx_contocli"
    Me.xx_contocli.Visible = True
    Me.xx_contocli.VisibleIndex = 4
    '
    'xx_descr1
    '
    Me.xx_descr1.AppearanceCell.Options.UseBackColor = True
    Me.xx_descr1.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descr1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descr1.Caption = "Cliente"
    Me.xx_descr1.Enabled = True
    Me.xx_descr1.FieldName = "xx_descr1"
    Me.xx_descr1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descr1.Name = "xx_descr1"
    Me.xx_descr1.Visible = True
    Me.xx_descr1.VisibleIndex = 5
    '
    'xx_datainstallazione
    '
    Me.xx_datainstallazione.AppearanceCell.Options.UseBackColor = True
    Me.xx_datainstallazione.AppearanceCell.Options.UseTextOptions = True
    Me.xx_datainstallazione.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_datainstallazione.Caption = "Installazione"
    Me.xx_datainstallazione.Enabled = True
    Me.xx_datainstallazione.FieldName = "xx_datainstallazione"
    Me.xx_datainstallazione.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_datainstallazione.Name = "xx_datainstallazione"
    Me.xx_datainstallazione.Visible = True
    Me.xx_datainstallazione.VisibleIndex = 6
    '
    'xx_dataritiro
    '
    Me.xx_dataritiro.AppearanceCell.Options.UseBackColor = True
    Me.xx_dataritiro.AppearanceCell.Options.UseTextOptions = True
    Me.xx_dataritiro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_dataritiro.Caption = "Ritiro"
    Me.xx_dataritiro.Enabled = True
    Me.xx_dataritiro.FieldName = "xx_dataritiro"
    Me.xx_dataritiro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_dataritiro.Name = "xx_dataritiro"
    Me.xx_dataritiro.Visible = True
    Me.xx_dataritiro.VisibleIndex = 7
    '
    'xx_ubicazione
    '
    Me.xx_ubicazione.AppearanceCell.Options.UseBackColor = True
    Me.xx_ubicazione.AppearanceCell.Options.UseTextOptions = True
    Me.xx_ubicazione.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_ubicazione.Caption = "Ubicazione"
    Me.xx_ubicazione.Enabled = True
    Me.xx_ubicazione.FieldName = "xx_ubicazione"
    Me.xx_ubicazione.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_ubicazione.Name = "xx_ubicazione"
    Me.xx_ubicazione.Visible = True
    Me.xx_ubicazione.VisibleIndex = 8
    '
    'FRMZOOMTR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(684, 435)
    Me.Controls.Add(Me.grdMatricole)
    Me.Controls.Add(Me.cmdRicerca)
    Me.Controls.Add(Me.edxx_matricola)
    Me.Controls.Add(Me.NtsLabel1)
    Me.Name = "FRMZOOMTR"
    Me.Text = "ZOOM MATRICOLE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_matricola.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grdMatricole, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvMatricole, System.ComponentModel.ISupportInitialize).EndInit()
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
  Public oCleHh As CLEHHMMTR
  Public dsHh As New DataSet
  Public oCallParams As CLE__CLDP
  Public dcHh As BindingSource = New BindingSource
  Public dcHhGr As BindingSource = New BindingSource

  Public Overridable Sub Bindcontrols()
    Try

      NTSFormClearDataBinding(Me)
      grvMatricole.NTSSetParam(oMenu, oApp.Tr(Me, 131336363292510816, "Griglia matricole"))


      edxx_matricola.NTSDbField = "XXX.xx_matricola"
      edxx_matricola.NTSSetParam(oMenu, oApp.Tr(Me, 132156089150587556, ""), 0)
      xx_matricola.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131342170236175016, "Codice matricola"), 0, True)
      xx_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131342170236175016, "Codice articolo"), 0, True)
      xx_matrproduttore.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131342170236175016, "Matricola produttore"), 0, True)
      xx_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131342170236331265, "Note"), 0, True)
      xx_descr1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131342170236487524, "Descrizione"), 0, True)
      xx_ubicazione.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131342170236643776, "Ubicazione"), 0, True)
      xx_datainstallazione.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131342170236800025, "Data installazione"), 0, True)
      xx_dataritiro.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131342170236956339, "Data ritiro"), 0, True)
      xx_contocli.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131342170236956339, "Codice cliente"), 0, True)

      NTSFormAddDataBinding(dcHh, Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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
          Case "CPNE.AggGriglia"

            AggGriglia()

        End Select
      End If
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub

  Private Sub AggGriglia()
    Try
      dcHhGr = Nothing
      dcHhGr = New BindingSource()
      dcHhGr.DataSource = dsHh.Tables("dtmatr")

      grdMatricole.DataSource = dcHhGr
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Private Sub FRM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      dsHh = oCleHh.dsShared
      dcHh = Nothing
      dcHh = New BindingSource()
      dcHh.DataSource = dsHh.Tables("XXX")
      oCleHh.CPNEAggiornaRicercaMatricola(edxx_matricola.Text)
      AggGriglia()
      Bindcontrols()

      'proprietà principali
      grvMatricole.NTSAllowDelete = False
      grvMatricole.NTSAllowInsert = False
      grvMatricole.Enabled = False

      GctlSetRoules()
      ' GctlApplicaDefaultValue()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Private Sub cmdRicerca_Click(sender As Object, e As EventArgs) Handles cmdRicerca.Click
    Try
      oCleHh.CPNEAggiornaRicercaMatricola(edxx_matricola.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub



  Private Sub grvMatricole_DoubleClick(sender As Object, e As EventArgs) Handles grvMatricole.DoubleClick
    Try

      If oCleHh.CPNESelezionaMatricola(grvMatricole.NTSGetCurrentDataRow) Then
        oCallParams.strPar1 = grvMatricole.NTSGetCurrentDataRow!xx_matricola.ToString

        Me.Close()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
End Class