Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO
Imports System.Windows.Forms
Public Class FRMImpRs
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
    Me.NtsLabel2 = New NTSInformatica.NTSLabel()
    Me.EdXx_descr = New NTSInformatica.NTSTextBoxStr()
    Me.EdXx_note = New NTSInformatica.NTSMemoBox()
    Me.CmdAnnulla = New NTSInformatica.NTSButton()
    Me.CmdOk = New NTSInformatica.NTSButton()
    CType(Me.EdXx_descr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.EdXx_note.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsLabel1.Location = New System.Drawing.Point(1, 9)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.NTSDbField = ""
    Me.NtsLabel1.Size = New System.Drawing.Size(61, 13)
    Me.NtsLabel1.TabIndex = 0
    Me.NtsLabel1.Text = "Descrizione"
    Me.NtsLabel1.Tooltip = ""
    Me.NtsLabel1.UseMnemonic = False
    '
    'NtsLabel2
    '
    Me.NtsLabel2.AutoSize = True
    Me.NtsLabel2.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel2.Location = New System.Drawing.Point(1, 28)
    Me.NtsLabel2.Name = "NtsLabel2"
    Me.NtsLabel2.NTSDbField = ""
    Me.NtsLabel2.Size = New System.Drawing.Size(30, 13)
    Me.NtsLabel2.TabIndex = 1
    Me.NtsLabel2.Text = "Note"
    Me.NtsLabel2.Tooltip = ""
    Me.NtsLabel2.UseMnemonic = False
    '
    'EdXx_descr
    '
    Me.EdXx_descr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.EdXx_descr.Cursor = System.Windows.Forms.Cursors.Default
    Me.EdXx_descr.Location = New System.Drawing.Point(79, 6)
    Me.EdXx_descr.Name = "EdXx_descr"
    Me.EdXx_descr.NTSDbField = ""
    Me.EdXx_descr.NTSForzaVisZoom = False
    Me.EdXx_descr.NTSOldValue = ""
    Me.EdXx_descr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.EdXx_descr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.EdXx_descr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.EdXx_descr.Properties.MaxLength = 65536
    Me.EdXx_descr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.EdXx_descr.Size = New System.Drawing.Size(125, 20)
    Me.EdXx_descr.TabIndex = 2
    '
    'EdXx_note
    '
    Me.EdXx_note.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.EdXx_note.Cursor = System.Windows.Forms.Cursors.Default
    Me.EdXx_note.Location = New System.Drawing.Point(4, 44)
    Me.EdXx_note.Name = "EdXx_note"
    Me.EdXx_note.NTSDbField = ""
    Me.EdXx_note.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.EdXx_note.Size = New System.Drawing.Size(200, 175)
    Me.EdXx_note.TabIndex = 3
    '
    'CmdAnnulla
    '
    Me.CmdAnnulla.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.CmdAnnulla.ImageText = ""
    Me.CmdAnnulla.Location = New System.Drawing.Point(4, 226)
    Me.CmdAnnulla.Name = "CmdAnnulla"
    Me.CmdAnnulla.Size = New System.Drawing.Size(75, 32)
    Me.CmdAnnulla.TabIndex = 4
    Me.CmdAnnulla.Text = "&Annulla"
    '
    'CmdOk
    '
    Me.CmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.CmdOk.ImageText = ""
    Me.CmdOk.Location = New System.Drawing.Point(129, 226)
    Me.CmdOk.Name = "CmdOk"
    Me.CmdOk.Size = New System.Drawing.Size(75, 32)
    Me.CmdOk.TabIndex = 5
    Me.CmdOk.Text = "&Conferma"
    '
    'FRMImpRs
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(210, 261)
    Me.Controls.Add(Me.CmdOk)
    Me.Controls.Add(Me.CmdAnnulla)
    Me.Controls.Add(Me.EdXx_note)
    Me.Controls.Add(Me.EdXx_descr)
    Me.Controls.Add(Me.NtsLabel2)
    Me.Controls.Add(Me.NtsLabel1)
    Me.Name = "FRMImpRs"
    Me.Text = "NON CONFORMITÀ"
    CType(Me.EdXx_descr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.EdXx_note.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano già  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      EdXx_note.NTSDbField = "CPNE_ANOM.xx_note"
      EdXx_descr.NTSDbField = "CPNE_ANOM.xx_descr"
      EdXx_note.NTSSetParam(oMenu, oApp.Tr(Me, 130797822648871257, "note"), 0)
      EdXx_descr.NTSSetParam(oMenu, oApp.Tr(Me, 130797822648901225, "Descr"), 0, False)



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
    dcHh.DataSource = dsHh.Tables("CPNE_ANOM")
    Bindcontrols()
    GctlSetRoules()
  End Sub


  Private Sub CmdAnnulla_Click(sender As System.Object, e As System.EventArgs) Handles CmdAnnulla.Click
    Me.Close()
  End Sub

  Private Sub CmdOk_Click(sender As System.Object, e As System.EventArgs) Handles CmdOk.Click
    If CType(oCleHh, CLFORGSOR).CPNEGeneraNonConf() Then
      oApp.MsgBoxInfo("Eseguito!!!!")
      Me.Close()
    End If
  End Sub
End Class