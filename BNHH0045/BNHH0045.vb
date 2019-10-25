Imports System.ComponentModel
Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMHH0045
  Public oCleHH0045 As CLEHH0045
  Public oCallParams As CLE__CLDP
  Public dsZone As DataSet
  Public dcZone As BindingSource = New BindingSource()

#Region "Dichiarazione Controlli"
  Private components As System.ComponentModel.IContainer
#End Region

#Region "Inizializzazione"
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
    'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNHH0045", "BEHH0045", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 127930271093451112, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleHH0045 = CType(oTmp, CLEHH0045)
    '------------------------------------------------

    AddHandler oCleHH0045.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleHH0045.Init(oApp, oScript, oMenu.oCleComm, "TABZONE", False, "", "") = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.fmAll = New NTSInformatica.NTSGroupBox()
    Me.NtsFlowLayoutPanel1 = New NTSInformatica.NTSFlowLayoutPanel()
    Me.NtsGroupBox2 = New NTSInformatica.NTSGroupBox()
    Me.cbCadenza = New NTSInformatica.NTSComboBox()
    Me.cmdOK = New NTSInformatica.NTSButton()
    Me.cmdAnnulla = New NTSInformatica.NTSButton()
    Me.lbNrRate = New NTSInformatica.NTSLabel()
    Me.edNrRate = New NTSInformatica.NTSTextBoxNum()
    Me.lbCadenza = New NTSInformatica.NTSLabel()
    Me.NtsGroupBox1 = New NTSInformatica.NTSGroupBox()
    Me.lbDataInizio = New NTSInformatica.NTSLabel()
    Me.lbImporto = New NTSInformatica.NTSLabel()
    Me.edDataScadenza = New NTSInformatica.NTSTextBoxData()
    Me.lbDataScadenza = New NTSInformatica.NTSLabel()
    Me.edDataInizio = New NTSInformatica.NTSTextBoxData()
    Me.edImporto = New NTSInformatica.NTSTextBoxNum()
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmAll, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmAll.SuspendLayout()
    Me.NtsFlowLayoutPanel1.SuspendLayout()
    CType(Me.NtsGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox2.SuspendLayout()
    CType(Me.cbCadenza.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNrRate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox1.SuspendLayout()
    CType(Me.edDataScadenza.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataInizio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edImporto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'fmAll
    '
    Me.fmAll.AllowDrop = True
    Me.fmAll.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmAll.Appearance.Options.UseBackColor = True
    Me.fmAll.Controls.Add(Me.NtsFlowLayoutPanel1)
    Me.fmAll.Dock = System.Windows.Forms.DockStyle.Fill
    Me.fmAll.Location = New System.Drawing.Point(0, 0)
    Me.fmAll.Name = "fmAll"
    Me.fmAll.ShowCaption = False
    Me.fmAll.Size = New System.Drawing.Size(298, 244)
    Me.fmAll.Text = "NTSGROUPBOX1"
    '
    'NtsFlowLayoutPanel1
    '
    Me.NtsFlowLayoutPanel1.AllowDrop = True
    Me.NtsFlowLayoutPanel1.AutoScroll = True
    Me.NtsFlowLayoutPanel1.Controls.Add(Me.NtsGroupBox2)
    Me.NtsFlowLayoutPanel1.Controls.Add(Me.NtsGroupBox1)
    Me.NtsFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.NtsFlowLayoutPanel1.Location = New System.Drawing.Point(2, 2)
    Me.NtsFlowLayoutPanel1.Name = "NtsFlowLayoutPanel1"
    Me.NtsFlowLayoutPanel1.Size = New System.Drawing.Size(294, 240)
    Me.NtsFlowLayoutPanel1.TabIndex = 29
    '
    'NtsGroupBox2
    '
    Me.NtsGroupBox2.AllowDrop = True
    Me.NtsGroupBox2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox2.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox2.Controls.Add(Me.cbCadenza)
    Me.NtsGroupBox2.Controls.Add(Me.cmdOK)
    Me.NtsGroupBox2.Controls.Add(Me.cmdAnnulla)
    Me.NtsGroupBox2.Controls.Add(Me.lbNrRate)
    Me.NtsGroupBox2.Controls.Add(Me.edNrRate)
    Me.NtsGroupBox2.Controls.Add(Me.lbCadenza)
    Me.NtsGroupBox2.Dock = System.Windows.Forms.DockStyle.Top
    Me.NtsGroupBox2.Location = New System.Drawing.Point(0, 100)
    Me.NtsGroupBox2.Name = "NtsGroupBox2"
    Me.NtsGroupBox2.Size = New System.Drawing.Size(294, 135)
    Me.NtsGroupBox2.Text = "DATI FATTURAZIONE"
    '
    'cbCadenza
    '
    Me.cbCadenza.EditValue = ""
    Me.cbCadenza.Location = New System.Drawing.Point(128, 48)
    Me.cbCadenza.Name = "cbCadenza"
    Me.cbCadenza.Properties.AutoHeight = False
    Me.cbCadenza.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbCadenza.Properties.DropDownRows = 30
    Me.cbCadenza.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbCadenza.Size = New System.Drawing.Size(128, 20)
    '
    'cmdOK
    '
    Me.cmdOK.Location = New System.Drawing.Point(196, 96)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(60, 26)
    Me.cmdOK.Text = "OK"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Location = New System.Drawing.Point(24, 96)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(60, 26)
    Me.cmdAnnulla.Text = "Annulla"
    '
    'lbNrRate
    '
    Me.lbNrRate.BackColor = System.Drawing.Color.Transparent
    Me.lbNrRate.Location = New System.Drawing.Point(4, 24)
    Me.lbNrRate.Name = "lbNrRate"
    Me.lbNrRate.NTSBordeStyle = NTSInformatica.NTSLabel.NTSBorderStyle.FieldCaption
    Me.lbNrRate.Size = New System.Drawing.Size(124, 20)
    Me.lbNrRate.Text = "Nr Rate"
    Me.lbNrRate.UseMnemonic = False
    '
    'edNrRate
    '
    Me.edNrRate.EditValue = "0"
    Me.edNrRate.Location = New System.Drawing.Point(128, 24)
    Me.edNrRate.Name = "edNrRate"
    Me.edNrRate.Properties.Appearance.Options.UseTextOptions = True
    Me.edNrRate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNrRate.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNrRate.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNrRate.Properties.AutoHeight = False
    Me.edNrRate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edNrRate.Properties.MaxLength = 65536
    Me.edNrRate.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNrRate.Size = New System.Drawing.Size(100, 20)
    '
    'lbCadenza
    '
    Me.lbCadenza.BackColor = System.Drawing.Color.Transparent
    Me.lbCadenza.Location = New System.Drawing.Point(4, 48)
    Me.lbCadenza.Name = "lbCadenza"
    Me.lbCadenza.NTSBordeStyle = NTSInformatica.NTSLabel.NTSBorderStyle.FieldCaption
    Me.lbCadenza.Size = New System.Drawing.Size(124, 20)
    Me.lbCadenza.Text = "Cadenza"
    Me.lbCadenza.UseMnemonic = False
    '
    'NtsGroupBox1
    '
    Me.NtsGroupBox1.AllowDrop = True
    Me.NtsGroupBox1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox1.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox1.Controls.Add(Me.lbDataInizio)
    Me.NtsGroupBox1.Controls.Add(Me.lbImporto)
    Me.NtsGroupBox1.Controls.Add(Me.edDataScadenza)
    Me.NtsGroupBox1.Controls.Add(Me.lbDataScadenza)
    Me.NtsGroupBox1.Controls.Add(Me.edDataInizio)
    Me.NtsGroupBox1.Controls.Add(Me.edImporto)
    Me.NtsGroupBox1.Dock = System.Windows.Forms.DockStyle.Top
    Me.NtsGroupBox1.Location = New System.Drawing.Point(0, 0)
    Me.NtsGroupBox1.Name = "NtsGroupBox1"
    Me.NtsGroupBox1.Size = New System.Drawing.Size(294, 100)
    Me.NtsGroupBox1.Text = "DATI CONTRATTO"
    '
    'lbDataInizio
    '
    Me.lbDataInizio.BackColor = System.Drawing.Color.Transparent
    Me.lbDataInizio.Location = New System.Drawing.Point(4, 48)
    Me.lbDataInizio.Name = "lbDataInizio"
    Me.lbDataInizio.NTSBordeStyle = NTSInformatica.NTSLabel.NTSBorderStyle.FieldCaption
    Me.lbDataInizio.Size = New System.Drawing.Size(124, 20)
    Me.lbDataInizio.Text = "Data inizio"
    Me.lbDataInizio.UseMnemonic = False
    '
    'lbImporto
    '
    Me.lbImporto.BackColor = System.Drawing.Color.Transparent
    Me.lbImporto.Location = New System.Drawing.Point(4, 24)
    Me.lbImporto.Name = "lbImporto"
    Me.lbImporto.NTSBordeStyle = NTSInformatica.NTSLabel.NTSBorderStyle.FieldCaption
    Me.lbImporto.Size = New System.Drawing.Size(124, 20)
    Me.lbImporto.Text = "Importo contratto"
    Me.lbImporto.UseMnemonic = False
    '
    'edDataScadenza
    '
    Me.edDataScadenza.EditValue = "31/12/2019"
    Me.edDataScadenza.Location = New System.Drawing.Point(136, 72)
    Me.edDataScadenza.Name = "edDataScadenza"
    Me.edDataScadenza.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataScadenza.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataScadenza.Properties.AutoHeight = False
    Me.edDataScadenza.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edDataScadenza.Properties.MaxLength = 65536
    Me.edDataScadenza.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataScadenza.Size = New System.Drawing.Size(100, 20)
    '
    'lbDataScadenza
    '
    Me.lbDataScadenza.BackColor = System.Drawing.Color.Transparent
    Me.lbDataScadenza.Location = New System.Drawing.Point(4, 72)
    Me.lbDataScadenza.Name = "lbDataScadenza"
    Me.lbDataScadenza.NTSBordeStyle = NTSInformatica.NTSLabel.NTSBorderStyle.FieldCaption
    Me.lbDataScadenza.Size = New System.Drawing.Size(124, 20)
    Me.lbDataScadenza.Text = "Data scad. contratto"
    Me.lbDataScadenza.UseMnemonic = False
    '
    'edDataInizio
    '
    Me.edDataInizio.EditValue = "31/12/2019"
    Me.edDataInizio.Location = New System.Drawing.Point(136, 48)
    Me.edDataInizio.Name = "edDataInizio"
    Me.edDataInizio.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataInizio.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataInizio.Properties.AutoHeight = False
    Me.edDataInizio.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edDataInizio.Properties.MaxLength = 65536
    Me.edDataInizio.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataInizio.Size = New System.Drawing.Size(100, 20)
    '
    'edImporto
    '
    Me.edImporto.EditValue = "0"
    Me.edImporto.Location = New System.Drawing.Point(136, 24)
    Me.edImporto.Name = "edImporto"
    Me.edImporto.Properties.Appearance.Options.UseTextOptions = True
    Me.edImporto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edImporto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edImporto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edImporto.Properties.AutoHeight = False
    Me.edImporto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
    Me.edImporto.Properties.MaxLength = 65536
    Me.edImporto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edImporto.Size = New System.Drawing.Size(100, 20)
    '
    'FRMHH0045
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScroll = True
    Me.ClientSize = New System.Drawing.Size(298, 244)
    Me.Controls.Add(Me.fmAll)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMHH0045"
    Me.Text = "DUPLICAZIONE SCADENZE CONTRATTO"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmAll, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmAll.ResumeLayout(False)
    Me.NtsFlowLayoutPanel1.ResumeLayout(False)
    CType(Me.NtsGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox2.ResumeLayout(False)
    CType(Me.cbCadenza.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNrRate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox1.ResumeLayout(False)
    CType(Me.edDataScadenza.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataInizio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edImporto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)

    Try
      edImporto.NTSSetParam(oMenu, "Importo Contratto", "#,###.##")
      edNrRate.NTSSetParam(oMenu, "Numero rate")
      cbCadenza.NTSSetParam("Cadenza rate")
      edDataInizio.NTSSetParam(oMenu, "Data inizio", True)
      edDataScadenza.NTSSetParam(oMenu, "Data scadenza Contratto", True)

      Dim dttCadenza As New DataTable()                       ' x stato commessa
      ' stato Commessa
      dttCadenza.Columns.Add("cod", GetType(String))
      dttCadenza.Columns.Add("val", GetType(String))
      dttCadenza.Rows.Add(New Object() {" ", "Seleziona ...."})
      dttCadenza.Rows.Add(New Object() {"M", "Mensile"})
      dttCadenza.Rows.Add(New Object() {"B", "Bimestrale"})
      dttCadenza.Rows.Add(New Object() {"T", "Trimestrale"})
      dttCadenza.Rows.Add(New Object() {"Q", "Quadrimestrale"})
      dttCadenza.Rows.Add(New Object() {"S", "Semestrale"})
      dttCadenza.Rows.Add(New Object() {"A", "Annuale"})
      dttCadenza.AcceptChanges()
      cbCadenza.DataSource = dttCadenza
      cbCadenza.ValueMember = "cod"
      cbCadenza.DisplayMember = "val"

      '-------------------------------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRMHH0045_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      If oCallParams IsNot Nothing Then
        edImporto.Text = oCallParams.dPar1.ToString
      End If
      oCallParams.strOut = ""

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub
#End Region


  Public Overridable Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub

  Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
    Try

      Dim ddatainizio As Date = Date.Parse(edDataInizio.Text)
      Dim ddatascadenza As Date = Date.Parse(edDataScadenza.Text)
      Dim strgiorni As String = DateDiff(DateInterval.Day, ddatainizio, ddatascadenza).ToString

      If ddatascadenza <= ddatainizio Then
        oApp.MsgBoxErr("La data scadenza del contratto non può essere inferiore o uguale alla data inizio contratto")
        edDataScadenza.Focus()
        Exit Sub
      End If

      Dim intGiorni As Integer = 0

      Select Case cbCadenza.Text.ToUpper
        Case "MENSILE"
          intGiorni = 25
        Case "BIMESTRALE"
          intGiorni = 55
        Case "TRIMESTRALE"
          intGiorni = 85
        Case "QUADRIMESTRALE"
          intGiorni = 115
        Case "SEMESTRALE"
          intGiorni = 175
        Case "ANNUALE"
          intGiorni = 360
      End Select

      intGiorni = intGiorni * CInt(edNrRate.Text)

      ddatainizio = DateAdd(DateInterval.Day, intGiorni, ddatainizio)

      If ddatascadenza <= ddatainizio Then
        oApp.MsgBoxErr("La data scadenza del contratto non è corretta in base ai parametri impostati.")
        edDataScadenza.Focus()
        Exit Sub
      End If

      oCallParams.strOut = "#ok#"
      oCallParams.strPar1 = Strings.Replace(Strings.Replace(edImporto.Text, ".", ""), ",", ".")
      oCallParams.strPar2 = edNrRate.Text
      oCallParams.strPar3 = edDataInizio.Text
      oCallParams.strPar4 = edDataScadenza.Text
      oCallParams.strPar5 = cbCadenza.Text
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub


  Private Sub edDataScadenza_Validating(sender As Object, e As CancelEventArgs) Handles edDataScadenza.Validating
    Dim ddatainizio As Date = Date.Parse(edDataInizio.Text)
    Dim ddatascadenza As Date = Date.Parse(edDataScadenza.Text)
    Dim strgiorni As String = DateDiff(DateInterval.Day, ddatainizio, ddatascadenza).ToString

    If ddatascadenza <= ddatainizio Then
      oApp.MsgBoxErr("La data scadenza del contratto non può essere inferiore o uguale alla data inizio contratto")
      edDataScadenza.Focus()
    End If
  End Sub

  Private Sub cbCadenza_Validating(sender As Object, e As CancelEventArgs) Handles cbCadenza.Validating
    Dim intGiorni As Integer = 0

    Select Case cbCadenza.Text.ToUpper
      Case "MENSILE"
        intGiorni = 25
      Case "BIMESTRALE"
        intGiorni = 55
      Case "TRIMESTRALE"
        intGiorni = 85
      Case "QUADRIMESTRALE"
        intGiorni = 115
      Case "SEMESTRALE"
        intGiorni = 175
      Case "ANNUALE"
        intGiorni = 360
    End Select

    Dim ddatainizio As Date = Date.Parse(edDataInizio.Text)
    Dim ddatascadenza As Date = Date.Parse(edDataScadenza.Text)

    intGiorni = intGiorni * CInt(edNrRate.Text)

    ddatainizio = DateAdd(DateInterval.Day, intGiorni, ddatainizio)

    If ddatascadenza <= ddatainizio Then
      oApp.MsgBoxErr("La data scadenza del contratto non è corretta in base ai parametri impostati.")
      edDataScadenza.Focus()
    End If

  End Sub

End Class
