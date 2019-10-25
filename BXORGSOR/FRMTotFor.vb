Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO
Imports System.Windows.Forms
Public Class FRMTotFor
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
    Me.NtsPanel1 = New NTSInformatica.NTSPanel()
    Me.grTotFor = New NTSInformatica.NTSGrid()
    Me.grvTotFor = New NTSInformatica.NTSGridView()
    Me.xx_codfor = New NTSInformatica.NTSGridColumn()
    Me.xx_desfor = New NTSInformatica.NTSGridColumn()
    Me.xx_valfor = New NTSInformatica.NTSGridColumn()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel1.SuspendLayout()
    CType(Me.grTotFor, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvTotFor, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    
    
    
    
    '
    'frmAuto
    '
    
    
    
    
    '
    'NtsPanel1
    '
    Me.NtsPanel1.AllowDrop = True
    Me.NtsPanel1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsPanel1.Appearance.Options.UseBackColor = True
    Me.NtsPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel1.Controls.Add(Me.grTotFor)
    Me.NtsPanel1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsPanel1.Location = New System.Drawing.Point(3, 3)
    Me.NtsPanel1.Name = "NtsPanel1"
    Me.NtsPanel1.Size = New System.Drawing.Size(350, 159)
    Me.NtsPanel1.TabIndex = 0
    Me.NtsPanel1.Text = "NtsPanel1"
    '
    'grTotFor
    '
    '
    '
    '
    Me.grTotFor.EmbeddedNavigator.Name = ""
    Me.grTotFor.Location = New System.Drawing.Point(4, 4)
    Me.grTotFor.MainView = Me.grvTotFor
    Me.grTotFor.Name = "grTotFor"
    Me.grTotFor.Size = New System.Drawing.Size(340, 152)
    Me.grTotFor.TabIndex = 0
    Me.grTotFor.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvTotFor})
    '
    'grvTotFor
    '
    Me.grvTotFor.ActiveFilterEnabled = False
    Me.grvTotFor.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_codfor, Me.xx_desfor, Me.xx_valfor})
    Me.grvTotFor.Enabled = True
    Me.grvTotFor.GridControl = Me.grTotFor
    Me.grvTotFor.Name = "grvTotFor"
    Me.grvTotFor.NTSAllowDelete = True
    Me.grvTotFor.NTSAllowInsert = True
    Me.grvTotFor.NTSAllowUpdate = True
    Me.grvTotFor.NTSMenuContext = Nothing
    Me.grvTotFor.OptionsCustomization.AllowRowSizing = True
    Me.grvTotFor.OptionsFilter.AllowFilterEditor = False
    Me.grvTotFor.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvTotFor.OptionsNavigation.UseTabKey = False
    Me.grvTotFor.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvTotFor.OptionsView.ColumnAutoWidth = False
    Me.grvTotFor.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvTotFor.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvTotFor.OptionsView.ShowGroupPanel = False
    Me.grvTotFor.RowHeight = 14
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
    Me.xx_codfor.NTSRepositoryComboBox = Nothing
    Me.xx_codfor.NTSRepositoryItemCheck = Nothing
    Me.xx_codfor.NTSRepositoryItemMemo = Nothing
    Me.xx_codfor.NTSRepositoryItemText = Nothing
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
    Me.xx_desfor.Enabled = True
    Me.xx_desfor.FieldName = "xx_desfor"
    Me.xx_desfor.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desfor.Name = "xx_desfor"
    Me.xx_desfor.NTSRepositoryComboBox = Nothing
    Me.xx_desfor.NTSRepositoryItemCheck = Nothing
    Me.xx_desfor.NTSRepositoryItemMemo = Nothing
    Me.xx_desfor.NTSRepositoryItemText = Nothing
    Me.xx_desfor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desfor.OptionsFilter.AllowFilter = False
    Me.xx_desfor.Visible = True
    Me.xx_desfor.VisibleIndex = 1
    Me.xx_desfor.Width = 163
    '
    'xx_valfor
    '
    Me.xx_valfor.AppearanceCell.Options.UseBackColor = True
    Me.xx_valfor.AppearanceCell.Options.UseTextOptions = True
    Me.xx_valfor.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_valfor.Caption = "Valore"
    Me.xx_valfor.Enabled = True
    Me.xx_valfor.FieldName = "xx_valfor"
    Me.xx_valfor.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_valfor.Name = "xx_valfor"
    Me.xx_valfor.NTSRepositoryComboBox = Nothing
    Me.xx_valfor.NTSRepositoryItemCheck = Nothing
    Me.xx_valfor.NTSRepositoryItemMemo = Nothing
    Me.xx_valfor.NTSRepositoryItemText = Nothing
    Me.xx_valfor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_valfor.OptionsFilter.AllowFilter = False
    Me.xx_valfor.Visible = True
    Me.xx_valfor.VisibleIndex = 2
    '
    'FRMTotFor
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(356, 165)
    Me.Controls.Add(Me.NtsPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMTotFor"
    Me.Text = "RIEPILOGO FORNITORI"
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel1.ResumeLayout(False)
    CType(Me.grTotFor, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvTotFor, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
#End Region
  Private components As System.ComponentModel.IContainer
  Public oCleHh As CLEORGSOR
  Public dsHh As New DataSet
  Public oCallParams As CLE__CLDP
  Public dcHh As BindingSource = New BindingSource
  Public dcHhGr As BindingSource = New BindingSource

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano già  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)


      NTSFormAddDataBinding(dcHh, Me)
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

  Private Sub FRMYYHHHH_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    dsHh = oCleHh.dsShared
    dcHh = Nothing
    dcHh = New BindingSource()
    dcHh.DataSource = dsHh.Tables("CPNE.TotFor")
    AggGriglia()
    Bindcontrols()

    grvTotFor.NTSAllowInsert = False
    grvTotFor.NTSAllowDelete = False
    grvTotFor.NTSAllowUpdate = False
    'grv.....Enabled = False 


    GctlSetRoules()
  End Sub

  Private Sub AggGriglia()
    dcHhGr = Nothing
    dcHhGr = New BindingSource()
    dcHhGr.DataSource = dsHh.Tables("CPNE.TotFor")
    grTotFor.DataSource = dcHhGr
  End Sub
End Class