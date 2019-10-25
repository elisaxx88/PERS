Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO
Imports System.Windows.Forms
Public Class FRMArtVar
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
    Me.NtsLabel1 = New NTSInformatica.NTSLabel()
    Me.edxx_codbase = New NTSInformatica.NTSTextBoxStr()
    Me.NtsLabel2 = New NTSInformatica.NTSLabel()
    Me.edxx_codvar = New NTSInformatica.NTSTextBoxStr()
    Me.NtsLabel3 = New NTSInformatica.NTSLabel()
    Me.NtsLabel4 = New NTSInformatica.NTSLabel()
    Me.NtsLabel5 = New NTSInformatica.NTSLabel()
    Me.edxx_Var1 = New NTSInformatica.NTSTextBoxStr()
    Me.edxx_Var2 = New NTSInformatica.NTSTextBoxStr()
    Me.edxx_Var3 = New NTSInformatica.NTSTextBoxStr()
    Me.NtsLabel6 = New NTSInformatica.NTSLabel()
    Me.edxx_desart = New NTSInformatica.NTSTextBoxStr()
    Me.edxx_desv1 = New NTSInformatica.NTSTextBoxStr()
    Me.edxx_desv2 = New NTSInformatica.NTSTextBoxStr()
    Me.edxx_desv3 = New NTSInformatica.NTSTextBoxStr()
    Me.cmdCreaArt = New NTSInformatica.NTSButton()
    Me.cmdAnnulla = New NTSInformatica.NTSButton()
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_codbase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_codvar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_Var1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_Var2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_Var3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_desart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_desv1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_desv2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edxx_desv3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    
    
    
    
    '
    'frmAuto
    '
    
    
    
    
    '
    'NtsLabel1
    '
    Me.NtsLabel1.AutoSize = True
    Me.NtsLabel1.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel1.Location = New System.Drawing.Point(12, 30)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.NTSDbField = ""
    Me.NtsLabel1.Size = New System.Drawing.Size(65, 13)
    Me.NtsLabel1.TabIndex = 0
    Me.NtsLabel1.Text = "Codice Base"
    Me.NtsLabel1.Tooltip = ""
    Me.NtsLabel1.UseMnemonic = False
    '
    'edxx_codbase
    '
    Me.edxx_codbase.Location = New System.Drawing.Point(134, 27)
    Me.edxx_codbase.Name = "edxx_codbase"
    Me.edxx_codbase.NTSDbField = ""
    Me.edxx_codbase.NTSForzaVisZoom = False
    Me.edxx_codbase.NTSOldValue = ""
    Me.edxx_codbase.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_codbase.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_codbase.Properties.AutoHeight = False
    Me.edxx_codbase.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_codbase.Properties.MaxLength = 65536
    Me.edxx_codbase.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_codbase.Size = New System.Drawing.Size(121, 20)
    Me.edxx_codbase.TabIndex = 1
    '
    'NtsLabel2
    '
    Me.NtsLabel2.AutoSize = True
    Me.NtsLabel2.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel2.Location = New System.Drawing.Point(15, 334)
    Me.NtsLabel2.Name = "NtsLabel2"
    Me.NtsLabel2.NTSDbField = ""
    Me.NtsLabel2.Size = New System.Drawing.Size(87, 13)
    Me.NtsLabel2.TabIndex = 2
    Me.NtsLabel2.Text = "Codice a varianti"
    Me.NtsLabel2.Tooltip = ""
    Me.NtsLabel2.UseMnemonic = False
    '
    'edxx_codvar
    '
    Me.edxx_codvar.Location = New System.Drawing.Point(134, 331)
    Me.edxx_codvar.Name = "edxx_codvar"
    Me.edxx_codvar.NTSDbField = ""
    Me.edxx_codvar.NTSForzaVisZoom = False
    Me.edxx_codvar.NTSOldValue = ""
    Me.edxx_codvar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_codvar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_codvar.Properties.AutoHeight = False
    Me.edxx_codvar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_codvar.Properties.MaxLength = 65536
    Me.edxx_codvar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_codvar.Size = New System.Drawing.Size(205, 20)
    Me.edxx_codvar.TabIndex = 3
    '
    'NtsLabel3
    '
    Me.NtsLabel3.AutoSize = True
    Me.NtsLabel3.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel3.Location = New System.Drawing.Point(15, 120)
    Me.NtsLabel3.Name = "NtsLabel3"
    Me.NtsLabel3.NTSDbField = ""
    Me.NtsLabel3.Size = New System.Drawing.Size(53, 13)
    Me.NtsLabel3.TabIndex = 4
    Me.NtsLabel3.Text = "Variante1"
    Me.NtsLabel3.Tooltip = ""
    Me.NtsLabel3.UseMnemonic = False
    '
    'NtsLabel4
    '
    Me.NtsLabel4.AutoSize = True
    Me.NtsLabel4.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel4.Location = New System.Drawing.Point(15, 190)
    Me.NtsLabel4.Name = "NtsLabel4"
    Me.NtsLabel4.NTSDbField = ""
    Me.NtsLabel4.Size = New System.Drawing.Size(53, 13)
    Me.NtsLabel4.TabIndex = 5
    Me.NtsLabel4.Text = "Variante2"
    Me.NtsLabel4.Tooltip = ""
    Me.NtsLabel4.UseMnemonic = False
    '
    'NtsLabel5
    '
    Me.NtsLabel5.AutoSize = True
    Me.NtsLabel5.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel5.Location = New System.Drawing.Point(15, 260)
    Me.NtsLabel5.Name = "NtsLabel5"
    Me.NtsLabel5.NTSDbField = ""
    Me.NtsLabel5.Size = New System.Drawing.Size(53, 13)
    Me.NtsLabel5.TabIndex = 6
    Me.NtsLabel5.Text = "Variante3"
    Me.NtsLabel5.Tooltip = ""
    Me.NtsLabel5.UseMnemonic = False
    '
    'edxx_Var1
    '
    Me.edxx_Var1.Location = New System.Drawing.Point(134, 117)
    Me.edxx_Var1.Name = "edxx_Var1"
    Me.edxx_Var1.NTSDbField = ""
    Me.edxx_Var1.NTSForzaVisZoom = False
    Me.edxx_Var1.NTSOldValue = ""
    Me.edxx_Var1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_Var1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_Var1.Properties.AutoHeight = False
    Me.edxx_Var1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_Var1.Properties.MaxLength = 65536
    Me.edxx_Var1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_Var1.Size = New System.Drawing.Size(100, 20)
    Me.edxx_Var1.TabIndex = 7
    '
    'edxx_Var2
    '
    Me.edxx_Var2.Location = New System.Drawing.Point(134, 187)
    Me.edxx_Var2.Name = "edxx_Var2"
    Me.edxx_Var2.NTSDbField = ""
    Me.edxx_Var2.NTSForzaVisZoom = False
    Me.edxx_Var2.NTSOldValue = ""
    Me.edxx_Var2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_Var2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_Var2.Properties.AutoHeight = False
    Me.edxx_Var2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_Var2.Properties.MaxLength = 65536
    Me.edxx_Var2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_Var2.Size = New System.Drawing.Size(100, 20)
    Me.edxx_Var2.TabIndex = 8
    '
    'edxx_Var3
    '
    Me.edxx_Var3.Location = New System.Drawing.Point(134, 253)
    Me.edxx_Var3.Name = "edxx_Var3"
    Me.edxx_Var3.NTSDbField = ""
    Me.edxx_Var3.NTSForzaVisZoom = False
    Me.edxx_Var3.NTSOldValue = ""
    Me.edxx_Var3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_Var3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_Var3.Properties.AutoHeight = False
    Me.edxx_Var3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_Var3.Properties.MaxLength = 65536
    Me.edxx_Var3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_Var3.Size = New System.Drawing.Size(100, 20)
    Me.edxx_Var3.TabIndex = 9
    '
    'NtsLabel6
    '
    Me.NtsLabel6.AutoSize = True
    Me.NtsLabel6.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel6.Location = New System.Drawing.Point(15, 371)
    Me.NtsLabel6.Name = "NtsLabel6"
    Me.NtsLabel6.NTSDbField = ""
    Me.NtsLabel6.Size = New System.Drawing.Size(61, 13)
    Me.NtsLabel6.TabIndex = 10
    Me.NtsLabel6.Text = "Descrizione"
    Me.NtsLabel6.Tooltip = ""
    Me.NtsLabel6.UseMnemonic = False
    '
    'edxx_desart
    '
    Me.edxx_desart.Location = New System.Drawing.Point(134, 368)
    Me.edxx_desart.Name = "edxx_desart"
    Me.edxx_desart.NTSDbField = ""
    Me.edxx_desart.NTSForzaVisZoom = False
    Me.edxx_desart.NTSOldValue = ""
    Me.edxx_desart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_desart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_desart.Properties.AutoHeight = False
    Me.edxx_desart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_desart.Properties.MaxLength = 65536
    Me.edxx_desart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_desart.Size = New System.Drawing.Size(458, 20)
    Me.edxx_desart.TabIndex = 11
    '
    'edxx_desv1
    '
    Me.edxx_desv1.Location = New System.Drawing.Point(257, 117)
    Me.edxx_desv1.Name = "edxx_desv1"
    Me.edxx_desv1.NTSDbField = ""
    Me.edxx_desv1.NTSForzaVisZoom = False
    Me.edxx_desv1.NTSOldValue = ""
    Me.edxx_desv1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_desv1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_desv1.Properties.AutoHeight = False
    Me.edxx_desv1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_desv1.Properties.MaxLength = 65536
    Me.edxx_desv1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_desv1.Size = New System.Drawing.Size(335, 20)
    Me.edxx_desv1.TabIndex = 12
    '
    'edxx_desv2
    '
    Me.edxx_desv2.Location = New System.Drawing.Point(257, 187)
    Me.edxx_desv2.Name = "edxx_desv2"
    Me.edxx_desv2.NTSDbField = ""
    Me.edxx_desv2.NTSForzaVisZoom = False
    Me.edxx_desv2.NTSOldValue = ""
    Me.edxx_desv2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_desv2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_desv2.Properties.AutoHeight = False
    Me.edxx_desv2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_desv2.Properties.MaxLength = 65536
    Me.edxx_desv2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_desv2.Size = New System.Drawing.Size(335, 20)
    Me.edxx_desv2.TabIndex = 13
    '
    'edxx_desv3
    '
    Me.edxx_desv3.Location = New System.Drawing.Point(257, 253)
    Me.edxx_desv3.Name = "edxx_desv3"
    Me.edxx_desv3.NTSDbField = ""
    Me.edxx_desv3.NTSForzaVisZoom = False
    Me.edxx_desv3.NTSOldValue = ""
    Me.edxx_desv3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edxx_desv3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edxx_desv3.Properties.AutoHeight = False
    Me.edxx_desv3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edxx_desv3.Properties.MaxLength = 65536
    Me.edxx_desv3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edxx_desv3.Size = New System.Drawing.Size(335, 20)
    Me.edxx_desv3.TabIndex = 14
    '
    'cmdCreaArt
    '
    Me.cmdCreaArt.ImagePath = ""
    Me.cmdCreaArt.ImageText = ""
    Me.cmdCreaArt.Location = New System.Drawing.Point(390, 27)
    Me.cmdCreaArt.Name = "cmdCreaArt"
    Me.cmdCreaArt.NTSContextMenu = Nothing
    Me.cmdCreaArt.Size = New System.Drawing.Size(75, 23)
    Me.cmdCreaArt.TabIndex = 15
    Me.cmdCreaArt.Text = "Crea Articolo"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(517, 27)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(75, 23)
    Me.cmdAnnulla.TabIndex = 16
    Me.cmdAnnulla.Text = "Annulla"
    '
    'FRMArtVar
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(629, 409)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdCreaArt)
    Me.Controls.Add(Me.edxx_desv3)
    Me.Controls.Add(Me.edxx_desv2)
    Me.Controls.Add(Me.edxx_desv1)
    Me.Controls.Add(Me.edxx_desart)
    Me.Controls.Add(Me.NtsLabel6)
    Me.Controls.Add(Me.edxx_Var3)
    Me.Controls.Add(Me.edxx_Var2)
    Me.Controls.Add(Me.edxx_Var1)
    Me.Controls.Add(Me.NtsLabel5)
    Me.Controls.Add(Me.NtsLabel4)
    Me.Controls.Add(Me.NtsLabel3)
    Me.Controls.Add(Me.edxx_codvar)
    Me.Controls.Add(Me.NtsLabel2)
    Me.Controls.Add(Me.edxx_codbase)
    Me.Controls.Add(Me.NtsLabel1)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMArtVar"
    Me.Text = "CREAZIONE ARTICOLO CON VARIANTI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_codbase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_codvar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_Var1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_Var2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_Var3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_desart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_desv1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_desv2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edxx_desv3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
#End Region
  Private components As System.ComponentModel.IContainer
  Public oCleHh As CLEORGSOR
  Public dsHh As New DataSet
  Public oCallParams As CLE__CLDP
  Public dcHh As BindingSource = New BindingSource
  Public dcHhGr As BindingSource = New BindingSource
  Public bAnnullatoCreaArtVar As Boolean
  Public strCodiceBase As String

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano già  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '      edxx_desv3.NTSDbField = "tabella.xx_desv3"
      '      edxx_desv2.NTSDbField = "tabella.xx_desv2"
      '      edxx_desv1.NTSDbField = "tabella.xx_desv1"
      '      edxx_desart.NTSDbField = "tabella.xx_desart"
      '      edxx_Var3.NTSDbField = "tabella.xx_var3"
      '      edxx_Var2.NTSDbField = "tabella.xx_var2"
      '      edxx_Var1.NTSDbField = "tabella.xx_var1"
      '      edxx_codvar.NTSDbField = "tabella.xx_codvar"
      '      edxx_codbase.NTSDbField = "tabella.xx_codbase"
      'edxx_desv3.NTSSetParam(oMenu, oApp.Tr(Me, 131013258266931073, "
      'edxx_desv2.NTSSetParam(oMenu, oApp.Tr(Me, 131013258266941077, "
      'edxx_desv1.NTSSetParam(oMenu, oApp.Tr(Me, 131013258266951091, "
      'edxx_desart.NTSSetParam(oMenu, oApp.Tr(Me, 131013258266961092, "
      'edxx_Var3.NTSSetParam(oMenu, oApp.Tr(Me, 131013258266981106, "
      'edxx_Var2.NTSSetParam(oMenu, oApp.Tr(Me, 131013258266991113, "
      'edxx_Var1.NTSSetParam(oMenu, oApp.Tr(Me, 131013258267001120, "
      'edxx_codvar.NTSSetParam(oMenu, oApp.Tr(Me, 131013258267041152, "
      'edxx_codbase.NTSSetParam(oMenu, oApp.Tr(Me, 131013258267061166, "


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
    dcHh.DataSource = dsHh.Tables("CPNE.ZoomVarianti")
    AggGriglia()
    Bindcontrols()

    'grv.....NTSAllowInsert = False
    'grv.....NTSAllowDelete = False 
    'grv.....Enabled = False 


    GctlSetRoules()
  End Sub

  Private Sub AggGriglia()
    dcHhGr = Nothing
    dcHhGr = New BindingSource()
    dcHhGr.DataSource = dsHh.Tables("CPNE.ZoomVarianti")
    'gr......DataSource = dcHhGr
  End Sub

  
  Private Sub cmdAnnulla_Click(sender As System.Object, e As System.EventArgs) Handles cmdAnnulla.Click
    bAnnullatoCreaArtVar = True
    Me.Close()
  End Sub

  Private Sub cmdCreaArt_Click(sender As Object, e As System.EventArgs) Handles cmdCreaArt.Click



    Me.Close()
  End Sub
End Class