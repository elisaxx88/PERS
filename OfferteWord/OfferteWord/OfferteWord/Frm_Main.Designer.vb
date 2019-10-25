<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Main
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla nell'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.Lbl_NrDoc = New System.Windows.Forms.Label()
    Me.Cmb_NrDoc = New System.Windows.Forms.ComboBox()
    Me.Lbl_anno = New System.Windows.Forms.Label()
    Me.Cmb_Anno = New System.Windows.Forms.ComboBox()
    Me.Lbl_TipoDoc = New System.Windows.Forms.Label()
    Me.Txt_TipoDoc = New System.Windows.Forms.TextBox()
    Me.Chk_PrezzoFinito = New System.Windows.Forms.CheckBox()
    Me.Chk_IVA = New System.Windows.Forms.CheckBox()
    Me.Chk_TotalePreventivo = New System.Windows.Forms.CheckBox()
    Me.Btn_Elabora = New System.Windows.Forms.Button()
    Me.Lbl_NrPreventivo = New System.Windows.Forms.Label()
    Me.Txt_NrPreventivo = New System.Windows.Forms.TextBox()
    Me.Txt_DataOfferta = New System.Windows.Forms.DateTimePicker()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Txt_Oggetto = New System.Windows.Forms.TextBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Cmb_Titolo = New System.Windows.Forms.ComboBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Txt_Validita = New System.Windows.Forms.TextBox()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.Txt_Garanzia = New System.Windows.Forms.TextBox()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.Txt_Email = New System.Windows.Forms.TextBox()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.Txt_CorteseAttenzione = New System.Windows.Forms.TextBox()
    Me.Txt_Cliente = New System.Windows.Forms.TextBox()
    Me.Txt_Appoggio = New System.Windows.Forms.TextBox()
    Me.Chk_CliFatt = New System.Windows.Forms.CheckBox()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.Txt_TempiConsegna = New System.Windows.Forms.TextBox()
    Me.Txt_ClienteDestinatario = New System.Windows.Forms.TextBox()
    Me.Chk_Presso = New System.Windows.Forms.CheckBox()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.Cmb_Modello = New System.Windows.Forms.ComboBox()
    Me.Txt_ResaMerce = New System.Windows.Forms.TextBox()
    Me.Txt_ResaMerceCod = New System.Windows.Forms.TextBox()
    Me.Chk_Excel = New System.Windows.Forms.CheckBox()
    Me.Txt_Progressivo = New System.Windows.Forms.TextBox()
    Me.RTB = New System.Windows.Forms.RichTextBox()
    Me.Txt_Resa_Cerca = New OfferteWord.Txt_Ricerca()
    Me.Txt_Contatto_Cerca = New OfferteWord.Txt_Ricerca()
    Me.SuspendLayout()
    '
    'Lbl_NrDoc
    '
    Me.Lbl_NrDoc.AutoSize = True
    Me.Lbl_NrDoc.Location = New System.Drawing.Point(27, 93)
    Me.Lbl_NrDoc.Name = "Lbl_NrDoc"
    Me.Lbl_NrDoc.Size = New System.Drawing.Size(43, 13)
    Me.Lbl_NrDoc.TabIndex = 34
    Me.Lbl_NrDoc.Text = "Nr Doc."
    '
    'Cmb_NrDoc
    '
    Me.Cmb_NrDoc.FormattingEnabled = True
    Me.Cmb_NrDoc.Location = New System.Drawing.Point(128, 90)
    Me.Cmb_NrDoc.Name = "Cmb_NrDoc"
    Me.Cmb_NrDoc.Size = New System.Drawing.Size(76, 21)
    Me.Cmb_NrDoc.TabIndex = 33
    '
    'Lbl_anno
    '
    Me.Lbl_anno.AutoSize = True
    Me.Lbl_anno.Location = New System.Drawing.Point(27, 62)
    Me.Lbl_anno.Name = "Lbl_anno"
    Me.Lbl_anno.Size = New System.Drawing.Size(32, 13)
    Me.Lbl_anno.TabIndex = 36
    Me.Lbl_anno.Text = "Anno"
    '
    'Cmb_Anno
    '
    Me.Cmb_Anno.FormattingEnabled = True
    Me.Cmb_Anno.Location = New System.Drawing.Point(128, 57)
    Me.Cmb_Anno.Name = "Cmb_Anno"
    Me.Cmb_Anno.Size = New System.Drawing.Size(76, 21)
    Me.Cmb_Anno.TabIndex = 35
    '
    'Lbl_TipoDoc
    '
    Me.Lbl_TipoDoc.AutoSize = True
    Me.Lbl_TipoDoc.Location = New System.Drawing.Point(27, 26)
    Me.Lbl_TipoDoc.Name = "Lbl_TipoDoc"
    Me.Lbl_TipoDoc.Size = New System.Drawing.Size(84, 13)
    Me.Lbl_TipoDoc.TabIndex = 38
    Me.Lbl_TipoDoc.Text = "Tipo Documento"
    '
    'Txt_TipoDoc
    '
    Me.Txt_TipoDoc.Location = New System.Drawing.Point(128, 23)
    Me.Txt_TipoDoc.Name = "Txt_TipoDoc"
    Me.Txt_TipoDoc.Size = New System.Drawing.Size(49, 21)
    Me.Txt_TipoDoc.TabIndex = 37
    '
    'Chk_PrezzoFinito
    '
    Me.Chk_PrezzoFinito.AutoSize = True
    Me.Chk_PrezzoFinito.Location = New System.Drawing.Point(341, 27)
    Me.Chk_PrezzoFinito.Name = "Chk_PrezzoFinito"
    Me.Chk_PrezzoFinito.Size = New System.Drawing.Size(87, 17)
    Me.Chk_PrezzoFinito.TabIndex = 39
    Me.Chk_PrezzoFinito.Text = "Prezzo Finito"
    Me.Chk_PrezzoFinito.UseVisualStyleBackColor = True
    '
    'Chk_IVA
    '
    Me.Chk_IVA.AutoSize = True
    Me.Chk_IVA.Location = New System.Drawing.Point(341, 57)
    Me.Chk_IVA.Name = "Chk_IVA"
    Me.Chk_IVA.Size = New System.Drawing.Size(77, 17)
    Me.Chk_IVA.TabIndex = 40
    Me.Chk_IVA.Text = "Includi IVA"
    Me.Chk_IVA.UseVisualStyleBackColor = True
    '
    'Chk_TotalePreventivo
    '
    Me.Chk_TotalePreventivo.AutoSize = True
    Me.Chk_TotalePreventivo.Location = New System.Drawing.Point(454, 25)
    Me.Chk_TotalePreventivo.Name = "Chk_TotalePreventivo"
    Me.Chk_TotalePreventivo.Size = New System.Drawing.Size(92, 17)
    Me.Chk_TotalePreventivo.TabIndex = 41
    Me.Chk_TotalePreventivo.Text = "Mostra Totale"
    Me.Chk_TotalePreventivo.UseVisualStyleBackColor = True
    '
    'Btn_Elabora
    '
    Me.Btn_Elabora.Location = New System.Drawing.Point(552, 23)
    Me.Btn_Elabora.Name = "Btn_Elabora"
    Me.Btn_Elabora.Size = New System.Drawing.Size(60, 32)
    Me.Btn_Elabora.TabIndex = 42
    Me.Btn_Elabora.Text = "Elabora"
    Me.Btn_Elabora.UseVisualStyleBackColor = True
    '
    'Lbl_NrPreventivo
    '
    Me.Lbl_NrPreventivo.AutoSize = True
    Me.Lbl_NrPreventivo.Location = New System.Drawing.Point(27, 196)
    Me.Lbl_NrPreventivo.Name = "Lbl_NrPreventivo"
    Me.Lbl_NrPreventivo.Size = New System.Drawing.Size(73, 13)
    Me.Lbl_NrPreventivo.TabIndex = 44
    Me.Lbl_NrPreventivo.Text = "Nr Preventivo"
    '
    'Txt_NrPreventivo
    '
    Me.Txt_NrPreventivo.Location = New System.Drawing.Point(128, 193)
    Me.Txt_NrPreventivo.Name = "Txt_NrPreventivo"
    Me.Txt_NrPreventivo.Size = New System.Drawing.Size(107, 21)
    Me.Txt_NrPreventivo.TabIndex = 43
    '
    'Txt_DataOfferta
    '
    Me.Txt_DataOfferta.CustomFormat = """dd MMMM yyyy"""
    Me.Txt_DataOfferta.Location = New System.Drawing.Point(341, 193)
    Me.Txt_DataOfferta.Name = "Txt_DataOfferta"
    Me.Txt_DataOfferta.Size = New System.Drawing.Size(174, 21)
    Me.Txt_DataOfferta.TabIndex = 45
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(27, 306)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(47, 13)
    Me.Label1.TabIndex = 47
    Me.Label1.Text = "Oggetto"
    '
    'Txt_Oggetto
    '
    Me.Txt_Oggetto.Location = New System.Drawing.Point(128, 303)
    Me.Txt_Oggetto.Name = "Txt_Oggetto"
    Me.Txt_Oggetto.Size = New System.Drawing.Size(484, 21)
    Me.Txt_Oggetto.TabIndex = 46
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(305, 196)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(30, 13)
    Me.Label2.TabIndex = 48
    Me.Label2.Text = "Data"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(27, 344)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(59, 13)
    Me.Label3.TabIndex = 50
    Me.Label3.Text = "Titolo inizio"
    '
    'Cmb_Titolo
    '
    Me.Cmb_Titolo.FormattingEnabled = True
    Me.Cmb_Titolo.Location = New System.Drawing.Point(128, 339)
    Me.Cmb_Titolo.Name = "Cmb_Titolo"
    Me.Cmb_Titolo.Size = New System.Drawing.Size(138, 21)
    Me.Cmb_Titolo.TabIndex = 49
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(338, 342)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(78, 13)
    Me.Label4.TabIndex = 52
    Me.Label4.Text = "Validità offerta"
    '
    'Txt_Validita
    '
    Me.Txt_Validita.Location = New System.Drawing.Point(439, 339)
    Me.Txt_Validita.Name = "Txt_Validita"
    Me.Txt_Validita.Size = New System.Drawing.Size(107, 21)
    Me.Txt_Validita.TabIndex = 51
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(27, 380)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(63, 13)
    Me.Label5.TabIndex = 54
    Me.Label5.Text = "Resa merce"
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(27, 420)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(85, 13)
    Me.Label6.TabIndex = 56
    Me.Label6.Text = "Durata Garanzia"
    '
    'Txt_Garanzia
    '
    Me.Txt_Garanzia.Location = New System.Drawing.Point(128, 417)
    Me.Txt_Garanzia.Name = "Txt_Garanzia"
    Me.Txt_Garanzia.Size = New System.Drawing.Size(173, 21)
    Me.Txt_Garanzia.TabIndex = 55
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Location = New System.Drawing.Point(27, 233)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(35, 13)
    Me.Label7.TabIndex = 59
    Me.Label7.Text = "E-Mail"
    '
    'Txt_Email
    '
    Me.Txt_Email.Location = New System.Drawing.Point(128, 230)
    Me.Txt_Email.Name = "Txt_Email"
    Me.Txt_Email.Size = New System.Drawing.Size(387, 21)
    Me.Txt_Email.TabIndex = 58
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(27, 270)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(49, 13)
    Me.Label8.TabIndex = 61
    Me.Label8.Text = "Alla C.A."
    '
    'Txt_CorteseAttenzione
    '
    Me.Txt_CorteseAttenzione.Location = New System.Drawing.Point(128, 267)
    Me.Txt_CorteseAttenzione.Name = "Txt_CorteseAttenzione"
    Me.Txt_CorteseAttenzione.Size = New System.Drawing.Size(387, 21)
    Me.Txt_CorteseAttenzione.TabIndex = 60
    '
    'Txt_Cliente
    '
    Me.Txt_Cliente.BackColor = System.Drawing.SystemColors.Info
    Me.Txt_Cliente.Enabled = False
    Me.Txt_Cliente.Location = New System.Drawing.Point(213, 90)
    Me.Txt_Cliente.Name = "Txt_Cliente"
    Me.Txt_Cliente.Size = New System.Drawing.Size(385, 21)
    Me.Txt_Cliente.TabIndex = 62
    '
    'Txt_Appoggio
    '
    Me.Txt_Appoggio.BackColor = System.Drawing.SystemColors.Info
    Me.Txt_Appoggio.Enabled = False
    Me.Txt_Appoggio.Location = New System.Drawing.Point(540, 193)
    Me.Txt_Appoggio.Name = "Txt_Appoggio"
    Me.Txt_Appoggio.Size = New System.Drawing.Size(62, 21)
    Me.Txt_Appoggio.TabIndex = 63
    Me.Txt_Appoggio.Visible = False
    '
    'Chk_CliFatt
    '
    Me.Chk_CliFatt.AutoSize = True
    Me.Chk_CliFatt.Checked = True
    Me.Chk_CliFatt.CheckState = System.Windows.Forms.CheckState.Checked
    Me.Chk_CliFatt.Location = New System.Drawing.Point(604, 94)
    Me.Chk_CliFatt.Name = "Chk_CliFatt"
    Me.Chk_CliFatt.Size = New System.Drawing.Size(44, 17)
    Me.Chk_CliFatt.TabIndex = 64
    Me.Chk_CliFatt.Text = "Usa"
    Me.Chk_CliFatt.UseVisualStyleBackColor = True
    '
    'Label9
    '
    Me.Label9.AutoSize = True
    Me.Label9.Location = New System.Drawing.Point(27, 456)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(97, 13)
    Me.Label9.TabIndex = 66
    Me.Label9.Text = "Tempi Attiv./Cons."
    '
    'Txt_TempiConsegna
    '
    Me.Txt_TempiConsegna.Location = New System.Drawing.Point(128, 453)
    Me.Txt_TempiConsegna.Name = "Txt_TempiConsegna"
    Me.Txt_TempiConsegna.Size = New System.Drawing.Size(484, 21)
    Me.Txt_TempiConsegna.TabIndex = 65
    '
    'Txt_ClienteDestinatario
    '
    Me.Txt_ClienteDestinatario.BackColor = System.Drawing.SystemColors.Info
    Me.Txt_ClienteDestinatario.Enabled = False
    Me.Txt_ClienteDestinatario.Location = New System.Drawing.Point(213, 117)
    Me.Txt_ClienteDestinatario.Name = "Txt_ClienteDestinatario"
    Me.Txt_ClienteDestinatario.Size = New System.Drawing.Size(385, 21)
    Me.Txt_ClienteDestinatario.TabIndex = 67
    '
    'Chk_Presso
    '
    Me.Chk_Presso.AutoSize = True
    Me.Chk_Presso.Checked = True
    Me.Chk_Presso.CheckState = System.Windows.Forms.CheckState.Checked
    Me.Chk_Presso.Location = New System.Drawing.Point(228, 27)
    Me.Chk_Presso.Name = "Chk_Presso"
    Me.Chk_Presso.Size = New System.Drawing.Size(73, 17)
    Me.Chk_Presso.TabIndex = 68
    Me.Chk_Presso.Text = "Presso ..."
    Me.Chk_Presso.UseVisualStyleBackColor = True
    '
    'Label10
    '
    Me.Label10.AutoSize = True
    Me.Label10.Location = New System.Drawing.Point(27, 159)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(43, 13)
    Me.Label10.TabIndex = 70
    Me.Label10.Text = "Modello"
    '
    'Cmb_Modello
    '
    Me.Cmb_Modello.FormattingEnabled = True
    Me.Cmb_Modello.Location = New System.Drawing.Point(128, 154)
    Me.Cmb_Modello.Name = "Cmb_Modello"
    Me.Cmb_Modello.Size = New System.Drawing.Size(474, 21)
    Me.Cmb_Modello.TabIndex = 69
    '
    'Txt_ResaMerce
    '
    Me.Txt_ResaMerce.Location = New System.Drawing.Point(128, 377)
    Me.Txt_ResaMerce.Name = "Txt_ResaMerce"
    Me.Txt_ResaMerce.Size = New System.Drawing.Size(287, 21)
    Me.Txt_ResaMerce.TabIndex = 53
    '
    'Txt_ResaMerceCod
    '
    Me.Txt_ResaMerceCod.Location = New System.Drawing.Point(481, 380)
    Me.Txt_ResaMerceCod.Name = "Txt_ResaMerceCod"
    Me.Txt_ResaMerceCod.Size = New System.Drawing.Size(60, 21)
    Me.Txt_ResaMerceCod.TabIndex = 72
    Me.Txt_ResaMerceCod.Visible = False
    '
    'Chk_Excel
    '
    Me.Chk_Excel.AutoSize = True
    Me.Chk_Excel.Location = New System.Drawing.Point(454, 57)
    Me.Chk_Excel.Name = "Chk_Excel"
    Me.Chk_Excel.Size = New System.Drawing.Size(80, 17)
    Me.Chk_Excel.TabIndex = 73
    Me.Chk_Excel.Text = "Valori Excel"
    Me.Chk_Excel.UseVisualStyleBackColor = True
    '
    'Txt_Progressivo
    '
    Me.Txt_Progressivo.Location = New System.Drawing.Point(552, 380)
    Me.Txt_Progressivo.Name = "Txt_Progressivo"
    Me.Txt_Progressivo.Size = New System.Drawing.Size(96, 21)
    Me.Txt_Progressivo.TabIndex = 74
    Me.Txt_Progressivo.Visible = False
    '
    'RTB
    '
    Me.RTB.Location = New System.Drawing.Point(603, 341)
    Me.RTB.Name = "RTB"
    Me.RTB.Size = New System.Drawing.Size(35, 27)
    Me.RTB.TabIndex = 75
    Me.RTB.Text = ""
    Me.RTB.Visible = False
    '
    'Txt_Resa_Cerca
    '
    Me.Txt_Resa_Cerca.Location = New System.Drawing.Point(421, 377)
    Me.Txt_Resa_Cerca.Name = "Txt_Resa_Cerca"
    Me.Txt_Resa_Cerca.Size = New System.Drawing.Size(54, 31)
    Me.Txt_Resa_Cerca.TabIndex = 71
    Me.Txt_Resa_Cerca.zzFiltroRicerca = ""
    Me.Txt_Resa_Cerca.zzLbl_Nomi = "TXT_RESAMERCE|DESCRIZIONE"
    Me.Txt_Resa_Cerca.zzRicerca = "TABTRASPACURA"
    Me.Txt_Resa_Cerca.zzTxt_Nomi = "TXT_RESAMERCECOD|CODICE"
    '
    'Txt_Contatto_Cerca
    '
    Me.Txt_Contatto_Cerca.Location = New System.Drawing.Point(540, 230)
    Me.Txt_Contatto_Cerca.Name = "Txt_Contatto_Cerca"
    Me.Txt_Contatto_Cerca.Size = New System.Drawing.Size(54, 31)
    Me.Txt_Contatto_Cerca.TabIndex = 57
    Me.Txt_Contatto_Cerca.zzFiltroRicerca = ""
    Me.Txt_Contatto_Cerca.zzLbl_Nomi = "TXT_EMAIL|EMAIL"
    Me.Txt_Contatto_Cerca.zzRicerca = "TABELLAPERSONALE"
    Me.Txt_Contatto_Cerca.zzTxt_Nomi = "TXT_CORTESEATTENZIONE|NOME|TXT_APPOGGIO|COGNOME"
    '
    'Frm_Main
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(654, 495)
    Me.Controls.Add(Me.RTB)
    Me.Controls.Add(Me.Txt_Progressivo)
    Me.Controls.Add(Me.Chk_Excel)
    Me.Controls.Add(Me.Txt_ResaMerceCod)
    Me.Controls.Add(Me.Txt_Resa_Cerca)
    Me.Controls.Add(Me.Label10)
    Me.Controls.Add(Me.Cmb_Modello)
    Me.Controls.Add(Me.Chk_Presso)
    Me.Controls.Add(Me.Txt_ClienteDestinatario)
    Me.Controls.Add(Me.Label9)
    Me.Controls.Add(Me.Txt_TempiConsegna)
    Me.Controls.Add(Me.Chk_CliFatt)
    Me.Controls.Add(Me.Txt_Appoggio)
    Me.Controls.Add(Me.Txt_Cliente)
    Me.Controls.Add(Me.Label8)
    Me.Controls.Add(Me.Txt_CorteseAttenzione)
    Me.Controls.Add(Me.Label7)
    Me.Controls.Add(Me.Txt_Email)
    Me.Controls.Add(Me.Txt_Contatto_Cerca)
    Me.Controls.Add(Me.Label6)
    Me.Controls.Add(Me.Txt_Garanzia)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.Txt_ResaMerce)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.Txt_Validita)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.Cmb_Titolo)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.Txt_Oggetto)
    Me.Controls.Add(Me.Txt_DataOfferta)
    Me.Controls.Add(Me.Lbl_NrPreventivo)
    Me.Controls.Add(Me.Txt_NrPreventivo)
    Me.Controls.Add(Me.Btn_Elabora)
    Me.Controls.Add(Me.Chk_TotalePreventivo)
    Me.Controls.Add(Me.Chk_IVA)
    Me.Controls.Add(Me.Chk_PrezzoFinito)
    Me.Controls.Add(Me.Lbl_TipoDoc)
    Me.Controls.Add(Me.Txt_TipoDoc)
    Me.Controls.Add(Me.Lbl_anno)
    Me.Controls.Add(Me.Cmb_Anno)
    Me.Controls.Add(Me.Lbl_NrDoc)
    Me.Controls.Add(Me.Cmb_NrDoc)
    Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Name = "Frm_Main"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Lbl_NrDoc As System.Windows.Forms.Label
    Friend WithEvents Cmb_NrDoc As System.Windows.Forms.ComboBox
    Friend WithEvents Lbl_anno As System.Windows.Forms.Label
    Friend WithEvents Cmb_Anno As System.Windows.Forms.ComboBox
    Friend WithEvents Lbl_TipoDoc As System.Windows.Forms.Label
    Friend WithEvents Txt_TipoDoc As System.Windows.Forms.TextBox
    Friend WithEvents Chk_PrezzoFinito As System.Windows.Forms.CheckBox
    Friend WithEvents Chk_IVA As System.Windows.Forms.CheckBox
    Friend WithEvents Chk_TotalePreventivo As System.Windows.Forms.CheckBox
    Friend WithEvents Btn_Elabora As System.Windows.Forms.Button
    Friend WithEvents Lbl_NrPreventivo As System.Windows.Forms.Label
    Friend WithEvents Txt_NrPreventivo As System.Windows.Forms.TextBox
    Friend WithEvents Txt_DataOfferta As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Txt_Oggetto As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Cmb_Titolo As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Txt_Validita As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Txt_Garanzia As System.Windows.Forms.TextBox
    Friend WithEvents Txt_Contatto_Cerca As OfferteWord.Txt_Ricerca
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Txt_Email As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Txt_CorteseAttenzione As System.Windows.Forms.TextBox
    Friend WithEvents Txt_Cliente As System.Windows.Forms.TextBox
    Friend WithEvents Txt_Appoggio As System.Windows.Forms.TextBox
    Friend WithEvents Chk_CliFatt As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Txt_TempiConsegna As System.Windows.Forms.TextBox
    Friend WithEvents Txt_ClienteDestinatario As System.Windows.Forms.TextBox
    Friend WithEvents Chk_Presso As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Cmb_Modello As System.Windows.Forms.ComboBox
    Friend WithEvents Txt_Resa_Cerca As OfferteWord.Txt_Ricerca
    Friend WithEvents Txt_ResaMerce As System.Windows.Forms.TextBox
    Friend WithEvents Txt_ResaMerceCod As System.Windows.Forms.TextBox
    Friend WithEvents Chk_Excel As System.Windows.Forms.CheckBox
    Friend WithEvents Txt_Progressivo As System.Windows.Forms.TextBox
    Friend WithEvents RTB As System.Windows.Forms.RichTextBox

End Class
