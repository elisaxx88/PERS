Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO
Public Class FRMHHSTSC
  Public oCleHh As CLFCGSTSC
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
    'creo e attivo l'entity e inizializzo la funzione che dovr√† rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNCGSTSC", "BECGSTSC", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128271029889882656, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleHh = CType(oTmp, CLFCGSTSC)
    ''''''''''''oCleHh.AssociaDs(dsHh)
    oCleHh.OMenu = oMenu
    '------------------------------------------------
    
    '''''''''''AddHandler oCleHh.RemoteEvent, AddressOf GestisciEventiEntity
    '''''''''''If oCleHh.Init(oApp, NTSScript, oMenu.oCleComm, "", False, "", "") = False Then Return False
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
    Me.ckxx_ftcontabilizzate = New NTSInformatica.NTSCheckBox()
    Me.ckxx_ftdacontab = New NTSInformatica.NTSCheckBox()
    Me.ckxx_ddtnoft = New NTSInformatica.NTSCheckBox()
    Me.ckxx_ordininonevasi = New NTSInformatica.NTSCheckBox()
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckxx_ftcontabilizzate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckxx_ftdacontab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckxx_ddtnoft.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckxx_ordininonevasi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    
    
    
    
    '
    'frmAuto
    '
    
    
    
    
    '
    'ckxx_ftcontabilizzate
    '
    Me.ckxx_ftcontabilizzate.EditValue = True
    Me.ckxx_ftcontabilizzate.Location = New System.Drawing.Point(51, 31)
    Me.ckxx_ftcontabilizzate.Name = "ckxx_ftcontabilizzate"
    Me.ckxx_ftcontabilizzate.NTSCheckValue = "S"
    Me.ckxx_ftcontabilizzate.NTSUnCheckValue = "N"
    Me.ckxx_ftcontabilizzate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckxx_ftcontabilizzate.Properties.Appearance.Options.UseBackColor = True
    Me.ckxx_ftcontabilizzate.Properties.AutoHeight = False
    Me.ckxx_ftcontabilizzate.Properties.Caption = "Fatture contabilizzate"
    Me.ckxx_ftcontabilizzate.Size = New System.Drawing.Size(153, 14)
    Me.ckxx_ftcontabilizzate.TabIndex = 0
    '
    'ckxx_ftdacontab
    '
    Me.ckxx_ftdacontab.EditValue = True
    Me.ckxx_ftdacontab.Location = New System.Drawing.Point(51, 72)
    Me.ckxx_ftdacontab.Name = "ckxx_ftdacontab"
    Me.ckxx_ftdacontab.NTSCheckValue = "S"
    Me.ckxx_ftdacontab.NTSUnCheckValue = "N"
    Me.ckxx_ftdacontab.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckxx_ftdacontab.Properties.Appearance.Options.UseBackColor = True
    Me.ckxx_ftdacontab.Properties.AutoHeight = False
    Me.ckxx_ftdacontab.Properties.Caption = "Fatture da contabilizzare"
    Me.ckxx_ftdacontab.Size = New System.Drawing.Size(153, 16)
    Me.ckxx_ftdacontab.TabIndex = 1
    '
    'ckxx_ddtnoft
    '
    Me.ckxx_ddtnoft.EditValue = True
    Me.ckxx_ddtnoft.Location = New System.Drawing.Point(51, 116)
    Me.ckxx_ddtnoft.Name = "ckxx_ddtnoft"
    Me.ckxx_ddtnoft.NTSCheckValue = "S"
    Me.ckxx_ddtnoft.NTSUnCheckValue = "N"
    Me.ckxx_ddtnoft.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckxx_ddtnoft.Properties.Appearance.Options.UseBackColor = True
    Me.ckxx_ddtnoft.Properties.AutoHeight = False
    Me.ckxx_ddtnoft.Properties.Caption = "DDT non fatturati"
    Me.ckxx_ddtnoft.Size = New System.Drawing.Size(139, 16)
    Me.ckxx_ddtnoft.TabIndex = 2
    '
    'ckxx_ordininonevasi
    '
    Me.ckxx_ordininonevasi.EditValue = True
    Me.ckxx_ordininonevasi.Location = New System.Drawing.Point(51, 160)
    Me.ckxx_ordininonevasi.Name = "ckxx_ordininonevasi"
    Me.ckxx_ordininonevasi.NTSCheckValue = "S"
    Me.ckxx_ordininonevasi.NTSUnCheckValue = "N"
    Me.ckxx_ordininonevasi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckxx_ordininonevasi.Properties.Appearance.Options.UseBackColor = True
    Me.ckxx_ordininonevasi.Properties.AutoHeight = False
    Me.ckxx_ordininonevasi.Properties.Caption = "Ordini non evasi"
    Me.ckxx_ordininonevasi.Size = New System.Drawing.Size(153, 20)
    Me.ckxx_ordininonevasi.TabIndex = 3
    '
    'FRMHHSTSC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(267, 222)
    Me.Controls.Add(Me.ckxx_ordininonevasi)
    Me.Controls.Add(Me.ckxx_ddtnoft)
    Me.Controls.Add(Me.ckxx_ftdacontab)
    Me.Controls.Add(Me.ckxx_ftcontabilizzate)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMHHSTSC"
    Me.Text = "SCELTA TIPOLOGIA RATE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckxx_ftcontabilizzate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckxx_ftdacontab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckxx_ddtnoft.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckxx_ordininonevasi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

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
  ''''''''''Public oCleHh As CLFCGSTSC
  Public dsHh As New DataSet
  Public oCallParams As CLE__CLDP
  Public dcHh As BindingSource = New BindingSource

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano gi‡† stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 


      NTSFormAddDataBinding(dcHh, Me)
      '-------------------------------------------------
      'per agganciare al dataset i vari controlli


    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub InitEntity(ByRef cleSt As CLFCGSTSC)
    oCleHh = cleSt
    AddHandler oCleHh.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Private Sub ckxx_ftcontabilizzate_CheckedChanged(sender As Object, e As System.EventArgs) Handles ckxx_ftcontabilizzate.CheckedChanged
    If ckxx_ftcontabilizzate.Checked Then
      oCleHh.bRateDaFtContabilizzate = True
    Else
      oCleHh.bRateDaFtContabilizzate = False
    End If
  End Sub
  Private Sub ckxx_ftdacontab_CheckedChanged(sender As Object, e As System.EventArgs) Handles ckxx_ftdacontab.CheckedChanged
    If ckxx_ftdacontab.Checked Then
      oCleHh.bRateDaFtDaContabilizzare = True
    Else
      oCleHh.bRateDaFtDaContabilizzare = False
    End If
  End Sub
  Private Sub ckxx_ddtnoft_CheckedChanged(sender As Object, e As System.EventArgs) Handles ckxx_ddtnoft.CheckedChanged
    If ckxx_ddtnoft.Checked Then
      oCleHh.bRateDaDdtDaFatturare = True
    Else
      oCleHh.bRateDaDdtDaFatturare = False
    End If
  End Sub
  Private Sub ckxx_ordininonevasi_CheckedChanged(sender As Object, e As System.EventArgs) Handles ckxx_ordininonevasi.CheckedChanged
    If ckxx_ordininonevasi.Checked Then
      oCleHh.bRateDaOrdiniNonEvasi = True
    Else
      oCleHh.bRateDaOrdiniNonEvasi = False
    End If
  End Sub

  Private Sub FRMHHSTSC_ActivatedFirst(sender As Object, e As System.EventArgs) Handles Me.ActivatedFirst
    oCleHh.bRateDaFtContabilizzate = True
    oCleHh.bRateDaFtDaContabilizzare = True
    oCleHh.bRateDaDdtDaFatturare = True
    oCleHh.bRateDaOrdiniNonEvasi = True
  End Sub
End Class