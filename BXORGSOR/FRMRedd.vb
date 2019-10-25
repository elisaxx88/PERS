Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO
Imports System.Windows.Forms
Public Class FRMRedd
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
    Me.NtsLabel3 = New NTSInformatica.NTSLabel()
    Me.lbxx_costo = New NTSInformatica.NTSLabel()
    Me.lbxx_ricavo = New NTSInformatica.NTSLabel()
    Me.lbxx_marg = New NTSInformatica.NTSLabel()
    Me.lbxx_diff = New NTSInformatica.NTSLabel()
    Me.NtsLabel5 = New NTSInformatica.NTSLabel()
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
    Me.NtsLabel1.Location = New System.Drawing.Point(12, 42)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.NTSDbField = ""
    Me.NtsLabel1.Size = New System.Drawing.Size(64, 13)
    Me.NtsLabel1.TabIndex = 0
    Me.NtsLabel1.Text = "Totale Costi"
    Me.NtsLabel1.Tooltip = ""
    Me.NtsLabel1.UseMnemonic = False
    '
    'NtsLabel2
    '
    Me.NtsLabel2.AutoSize = True
    Me.NtsLabel2.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel2.Location = New System.Drawing.Point(12, 10)
    Me.NtsLabel2.Name = "NtsLabel2"
    Me.NtsLabel2.NTSDbField = ""
    Me.NtsLabel2.Size = New System.Drawing.Size(68, 13)
    Me.NtsLabel2.TabIndex = 1
    Me.NtsLabel2.Text = "Totale Ricavi"
    Me.NtsLabel2.Tooltip = ""
    Me.NtsLabel2.UseMnemonic = False
    '
    'NtsLabel3
    '
    Me.NtsLabel3.AutoSize = True
    Me.NtsLabel3.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel3.Location = New System.Drawing.Point(12, 108)
    Me.NtsLabel3.Name = "NtsLabel3"
    Me.NtsLabel3.NTSDbField = ""
    Me.NtsLabel3.Size = New System.Drawing.Size(59, 13)
    Me.NtsLabel3.TabIndex = 2
    Me.NtsLabel3.Text = "Margine %"
    Me.NtsLabel3.Tooltip = ""
    Me.NtsLabel3.UseMnemonic = False
    '
    'lbxx_costo
    '
    Me.lbxx_costo.BackColor = System.Drawing.Color.Transparent
    Me.lbxx_costo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lbxx_costo.Location = New System.Drawing.Point(100, 41)
    Me.lbxx_costo.Name = "lbxx_costo"
    Me.lbxx_costo.NTSDbField = ""
    Me.lbxx_costo.Size = New System.Drawing.Size(112, 21)
    Me.lbxx_costo.TabIndex = 3
    Me.lbxx_costo.Text = "NtsLabel4"
    Me.lbxx_costo.TextAlign = System.Drawing.ContentAlignment.TopCenter
    Me.lbxx_costo.Tooltip = ""
    Me.lbxx_costo.UseMnemonic = False
    '
    'lbxx_ricavo
    '
    Me.lbxx_ricavo.BackColor = System.Drawing.Color.Transparent
    Me.lbxx_ricavo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lbxx_ricavo.Location = New System.Drawing.Point(100, 9)
    Me.lbxx_ricavo.Name = "lbxx_ricavo"
    Me.lbxx_ricavo.NTSDbField = ""
    Me.lbxx_ricavo.Size = New System.Drawing.Size(112, 21)
    Me.lbxx_ricavo.TabIndex = 4
    Me.lbxx_ricavo.Text = "NtsLabel5"
    Me.lbxx_ricavo.TextAlign = System.Drawing.ContentAlignment.TopCenter
    Me.lbxx_ricavo.Tooltip = ""
    Me.lbxx_ricavo.UseMnemonic = False
    '
    'lbxx_marg
    '
    Me.lbxx_marg.BackColor = System.Drawing.Color.Transparent
    Me.lbxx_marg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lbxx_marg.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbxx_marg.Location = New System.Drawing.Point(100, 107)
    Me.lbxx_marg.Name = "lbxx_marg"
    Me.lbxx_marg.NTSDbField = ""
    Me.lbxx_marg.Size = New System.Drawing.Size(112, 21)
    Me.lbxx_marg.TabIndex = 5
    Me.lbxx_marg.Text = "NtsLabel6"
    Me.lbxx_marg.TextAlign = System.Drawing.ContentAlignment.TopCenter
    Me.lbxx_marg.Tooltip = ""
    Me.lbxx_marg.UseMnemonic = False
    '
    'lbxx_diff
    '
    Me.lbxx_diff.BackColor = System.Drawing.Color.Transparent
    Me.lbxx_diff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lbxx_diff.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbxx_diff.Location = New System.Drawing.Point(100, 75)
    Me.lbxx_diff.Name = "lbxx_diff"
    Me.lbxx_diff.NTSDbField = ""
    Me.lbxx_diff.Size = New System.Drawing.Size(112, 21)
    Me.lbxx_diff.TabIndex = 7
    Me.lbxx_diff.Text = "lbxx_diff"
    Me.lbxx_diff.TextAlign = System.Drawing.ContentAlignment.TopCenter
    Me.lbxx_diff.Tooltip = ""
    Me.lbxx_diff.UseMnemonic = False
    '
    'NtsLabel5
    '
    Me.NtsLabel5.AutoSize = True
    Me.NtsLabel5.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel5.Location = New System.Drawing.Point(12, 76)
    Me.NtsLabel5.Name = "NtsLabel5"
    Me.NtsLabel5.NTSDbField = ""
    Me.NtsLabel5.Size = New System.Drawing.Size(45, 13)
    Me.NtsLabel5.TabIndex = 6
    Me.NtsLabel5.Text = "Margine"
    Me.NtsLabel5.Tooltip = ""
    Me.NtsLabel5.UseMnemonic = False
    '
    'FRMRedd
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(226, 175)
    Me.Controls.Add(Me.lbxx_diff)
    Me.Controls.Add(Me.NtsLabel5)
    Me.Controls.Add(Me.lbxx_marg)
    Me.Controls.Add(Me.lbxx_ricavo)
    Me.Controls.Add(Me.lbxx_costo)
    Me.Controls.Add(Me.NtsLabel3)
    Me.Controls.Add(Me.NtsLabel2)
    Me.Controls.Add(Me.NtsLabel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMRedd"
    Me.Text = "REDDITIVITÀ"
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

      lbxx_marg.NTSDbField = "CPNE.marg.xx_marg"
      lbxx_ricavo.NTSDbField = "CPNE.marg.xx_ricavo"
      lbxx_costo.NTSDbField = "CPNE.marg.xx_costo"
      lbxx_diff.NTSDbField = "CPNE.marg.xx_diff"


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
    dcHh.DataSource = dsHh.Tables("CPNE.marg")
    Bindcontrols()
    GctlSetRoules()
  End Sub
End Class