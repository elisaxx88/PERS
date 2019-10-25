Imports Word = Microsoft.Office.Interop.Word
Imports Excel = Microsoft.Office.Interop.Excel
Imports VB = Microsoft.VisualBasic

Public Class Frm_Main

    Dim cServer As String = ""
    Dim cDatabase As String = ""
    Dim cUtente As String = ""
    Dim cPassword As String = ""
    Dim bEventoCambioAnnoAbilitato As Boolean = False
    Dim aModelli(50, 1) As String
    Dim bPrivato As Boolean = True

    Private Sub Frm_Main_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim aLista(10, 1) As String
        Dim nAttributi As Integer = 0
        Dim nConta As Integer = 0
        Dim nNrModelli As Integer = 0
        Dim cAppoggio As String = ""

        For nConta = Year(Now) - 5 To 2030
            Call Cmb_Anno.Items.Add("" + nConta.ToString)
        Next
        Cmb_Anno.Text = Year(Now).ToString

        Call ABCA_Util.xmlLeggiElemento("MAIN", "Form", aLista, nAttributi)
        Me.Text = ABCA_Util.ValoreDaLista("Titolo", aLista, nAttributi)
        Txt_TipoDoc.Text = ABCA_Util.ValoreDaLista("TipoDoc", aLista, nAttributi)
        Call ABCA_Util.xmlLeggiElemento("MAIN", "SQL", aLista, nAttributi)
        cServer = ABCA_Util.ValoreDaLista("Server", aLista, nAttributi)
        cDatabase = ABCA_Util.ValoreDaLista("Database", aLista, nAttributi)
        cUtente = ABCA_Util.ValoreDaLista("Utente", aLista, nAttributi)
        cPassword = ABCA_Util.ValoreDaLista("Password", aLista, nAttributi)
        Call ABCA_Util.xmlLeggiElemento("MAIN", "Titoli", aLista, nAttributi)
        For nConta = 0 To nAttributi - 1
            Call Cmb_Titolo.Items.Add("" + aLista(nConta, 1))
        Next
        Call ABCA_Util.xmlLeggiElemento("MAIN", "DEFAULT", aLista, nAttributi)
        Txt_Oggetto.Text = ABCA_Util.ValoreDaLista("Oggetto", aLista, nAttributi)
        Txt_Validita.Text = ABCA_Util.ValoreDaLista("Validita", aLista, nAttributi)
        Txt_ResaMerce.Text = ABCA_Util.ValoreDaLista("ResaMerce", aLista, nAttributi)
        Txt_ResaMerceCod.Text = ABCA_Util.ValoreDaLista("ResaMerceCod", aLista, nAttributi)
        Txt_Garanzia.Text = ABCA_Util.ValoreDaLista("Garanzia", aLista, nAttributi)
        '        Call ABCA_Util.xmlLeggiElemento("MAIN", "MODELLI", aLista, nAttributi)
        If ABCA_Util.xmlLeggiElemento("Main", "MODELLI", aLista, nAttributi) = True Then
            ' recupero le informazioni sui modelli
            nNrModelli = ABCA_Util.ValoreDaLista("NUMERO", aLista, nAttributi)
            For nConta = 1 To nNrModelli
                cAppoggio = "MODELLO_" + Microsoft.VisualBasic.Right("00" & nConta, 2)
                If ABCA_Util.xmlLeggiElemento("Main", cAppoggio, aLista, nAttributi) = True Then
                    aModelli(nConta - 1, 0) = ABCA_Util.ValoreDaLista("NOMEMODELLO", aLista, nAttributi)
                    aModelli(nConta - 1, 1) = ABCA_Util.ValoreDaLista("RICAMBI", aLista, nAttributi)
                    Call Cmb_Modello.Items.Add("" & ABCA_Util.ValoreDaLista("DESCRIZIONE", aLista, nAttributi))
                End If
            Next nConta
            Cmb_Modello.SelectedIndex = 0
        End If

        Call CaricaNrDocumenti()
        bEventoCambioAnnoAbilitato = True
        ' CAMBIO IL FORMATO ALLA CASELLA DATA OFFERTA
        Txt_DataOfferta.Format = DateTimePickerFormat.Custom
        Txt_DataOfferta.CustomFormat = "dd MMMM yyyy"
        Txt_Cliente.Text = ""

    End Sub

    Private Sub CaricaNrDocumenti()
        Dim oSql As New clsABCA_SQL
        Dim nConta As Integer = 0
        Dim cQuery As String = ""

        Try
            Cmb_NrDoc.Items.Clear()
            If oSql.ApriConnessione(cServer, cDatabase, cUtente, cPassword) = True Then
                cQuery = "SELECT TD_NUMORD AS NUMERODOC FROM TESTORD WHERE TD_ANNO = " + Cmb_Anno.Text + " AND TD_SERIE = '" + Txt_TipoDoc.Text + "' ORDER BY TD_NUMORD DESC"
                If oSql.CaricaRecords(cQuery) Then
                    For nConta = 1 To oSql.nNrRecords
                        Call Cmb_NrDoc.Items.Add("" + oSql.RestituisciValore("NUMERODOC", nConta).ToString)
                    Next
                End If
                Call oSql.ChiudiConnessione()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Cmb_Anno_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Anno.SelectedIndexChanged
        If bEventoCambioAnnoAbilitato = True Then
            Call CaricaNrDocumenti()
            Txt_Cliente.Text = ""
        End If
    End Sub

    Private Sub Cmb_NrDoc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_NrDoc.SelectedIndexChanged
        Call InfoCliente()
    End Sub
  Private Sub InfoCliente()
    Dim oSql As New clsABCA_SQL
    Dim nConta As Integer = 0
    Dim cQuery As String = ""
    Try
      If bEventoCambioAnnoAbilitato = True Then
        Txt_NrPreventivo.Text = Cmb_NrDoc.Text & "/" & Cmb_Anno.Text
        If oSql.ApriConnessione(cServer, cDatabase, cUtente, cPassword) = True Then
          '                    If Chk_CliFatt.Checked = True Then
          cQuery = "SELECT TD.TD_ANNO, TD.TD_SERIE, TD.TD_NUMORD, "
          cQuery &= "TD.TD_CONTFATT, ACF.AN_DESCR1 As CLIFATT, ACF.AN_EMAIL As MAILCLIFATT, "
          cQuery &= "TD.TD_CONTO, ACF2.AN_DESCR1 As CLICLIFOR, ACF2.AN_EMAIL As MAILCLIFOR, "
          cQuery &= "TD.TD_ACURADI as TRASPACURA, ISNULL(TTC.tb_desqqtr, '') AS DESCR_TRASPACURA, "
          cQuery &= "' ' AS TEMPI_CONSEGNA "
          cQuery &= "FROM TESTORD TD "
          cQuery &= "INNER JOIN ANAGRA ACF ON TD.TD_CONTFATT = ACF.AN_CONTO "
          cQuery &= "INNER JOIN ANAGRA AS ACF2 ON TD.TD_CONTO = ACF2.AN_CONTO "
          cQuery &= "LEFT OUTER JOIN tabqqtr TTC ON TD.TD_ACURADI = TTC.tb_codqqtr "
          cQuery &= "WHERE TD.TD_TIPORK = 'Q' AND TD.TD_ANNO = " & Cmb_Anno.Text
          cQuery &= " AND TD.TD_SERIE = '" + Txt_TipoDoc.Text + "' AND TD.TD_NUMORD = " & Cmb_NrDoc.Text
          Txt_Cliente.Text = ""
          Txt_Email.Text = ""
          Txt_CorteseAttenzione.Text = ""
          If oSql.CaricaRecords(cQuery) Then
            If Chk_CliFatt.Checked = True Then
              Txt_Cliente.Text = oSql.RestituisciValore("TD_CONTFATT", 1).ToString & " - " & oSql.RestituisciValore("CLIFATT", 1).ToString
              Txt_ClienteDestinatario.Text = oSql.RestituisciValore("TD_CONTO", 1).ToString & " - " & oSql.RestituisciValore("CLICLIFOR", 1).ToString
              Txt_Email.Text = oSql.RestituisciValore("MAILCLIFATT", 1).ToString
              Txt_Contatto_Cerca.zzFiltroRicerca = "og_conto = " & oSql.RestituisciValore("TD_CONTFATT", 1).ToString
            Else
              Txt_Cliente.Text = oSql.RestituisciValore("TD_CONTFATT", 1).ToString & " - " & oSql.RestituisciValore("CLIFATT", 1).ToString
              Txt_ClienteDestinatario.Text = oSql.RestituisciValore("TD_CONTO", 1).ToString & " - " & oSql.RestituisciValore("CLICLIFOR", 1).ToString
              Txt_Email.Text = oSql.RestituisciValore("MAILCLIFOR", 1).ToString
              Txt_Contatto_Cerca.zzFiltroRicerca = "og_conto = " & oSql.RestituisciValore("TD_CONTO", 1).ToString
            End If
          End If
          ' recupero la resa merce e i temi di consegna
          Txt_ResaMerce.Text = oSql.RestituisciValore("DESCR_TRASPACURA", 1).ToString
          Txt_ResaMerceCod.Text = oSql.RestituisciValore("TRASPACURA", 1).ToString
          Txt_TempiConsegna.Text = oSql.RestituisciValore("TEMPI_CONSEGNA", 1).ToString
          Txt_NrPreventivo.Text = Txt_NrPreventivo.Text & "/" & oSql.RestituisciValore("CODCLIFOR", 1).ToString
          Txt_Progressivo.Text = oSql.RestituisciValore("TD_ANNO", 1).ToString & ";" & oSql.RestituisciValore("TD_SERIE", 1).ToString & ";" & oSql.RestituisciValore("TD_NUMORD", 1).ToString
          Call oSql.ChiudiConnessione()
        End If
      End If
    Catch ex As Exception
    End Try
  End Sub

  Private Sub Txt_Appoggio_TextChanged(sender As Object, e As EventArgs) Handles Txt_Appoggio.TextChanged
        If Txt_Appoggio.Text > " " Then
            Txt_CorteseAttenzione.Text = Txt_CorteseAttenzione.Text & " " & Txt_Appoggio.Text
        End If
    End Sub

  Private Sub Btn_Elabora_Click(sender As Object, e As EventArgs) Handles Btn_Elabora.Click
    Dim cNomeModello As String = ""
    Dim cDirModello As String = ""
    Dim cDirArchiviazione As String = ""
    Dim aLista(20, 1) As String
    Dim nAttributi As Integer = 0
    Dim cNomeFilePreventivo As String = ""
    Dim cRicambi As String = "N"
    Dim oSql As New clsABCA_SQL
    Dim cQuery As String = ""

    Cmb_NrDoc.Items.Clear()

    Dim aDoc() As String = Split(Txt_Progressivo.Text, ";")

    If oSql.ApriConnessione(cServer, cDatabase, cUtente, cPassword) = True Then

      cQuery = "UPDATE TESTORD SET TD_ACURADI = "
      If Txt_ResaMerceCod.Text > " " Then
        cQuery &= "'" & Txt_ResaMerceCod.Text & "' "
      Else
        cQuery &= "' ' "
      End If
      cQuery &= "WHERE TD_TIPORK = 'Q' AND "
      cQuery &= "TD_ANNO = " & aDoc(0).ToString & " AND "
      cQuery &= "TD_SERIE = '" & aDoc(1).ToString & "' AND "
      cQuery &= "TD_NUMORD = " & aDoc(2).ToString
      If oSql.EseguiSQL(cQuery) = True Then
        'cQuery = "UPDATE EXTRATESTEDOC Set TEMPI_CONSEGNA = '" + ABCA_Util.PulisciStringa(Txt_TempiConsegna.Text) + "' WHERE IDTESTA = " & Txt_Progressivo.Text
        'Call oSql.EseguiSQL(cQuery)
      End If
      Call oSql.ChiudiConnessione()
    End If

    Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

    InizializzaoWord()

    ' verifico che sia stata compilata tutta la parte relativa al documento
    If Txt_TipoDoc.Text > " " And Cmb_Anno.Text > " " And Cmb_NrDoc.Text > " " Then
      ' tutto ok
      cNomeFilePreventivo = Txt_TipoDoc.Text & "_" & Cmb_Anno.Text & "_" & Microsoft.VisualBasic.Right("00000" & Cmb_NrDoc.Text, 5)
      cNomeFilePreventivo &= "-"
      If Txt_Cliente.Text <> Txt_ClienteDestinatario.Text Then
        cNomeFilePreventivo &= VB.Trim(VB.Right(Txt_Cliente.Text, VB.Len(Txt_Cliente.Text) - 10))
        cNomeFilePreventivo &= "-" & VB.Trim(VB.Right(Txt_ClienteDestinatario.Text, VB.Len(Txt_ClienteDestinatario.Text) - 10))
      Else
        cNomeFilePreventivo &= VB.Trim(VB.Right(Txt_Cliente.Text, VB.Len(Txt_Cliente.Text) - 10))
      End If
      cNomeFilePreventivo = ABCA_Util.PulisciPerNomeFile(cNomeFilePreventivo)
    Else
      ' non lo è e quindi segnalo ed esco
      MsgBox("Completare i dati relativi al tipo documento, anno e numero documento!", vbOKOnly, "Attenzione!!")
      Me.Cursor = System.Windows.Forms.Cursors.Default
      Exit Sub
    End If
    ' se il file xml è completo allora proseguo
    If ABCA_Util.xmlLeggiElemento("Main", "WORD", aLista, nAttributi) = True Then
      ' recupero le informazioni su modello e directory di archiviazione
      cNomeModello = aModelli(Cmb_Modello.SelectedIndex, 0)
      cRicambi = aModelli(Cmb_Modello.SelectedIndex, 1)
      '            cNomeModello = ABCA_Util.ValoreDaLista("nomemodello", aLista, nAttributi)
      cDirModello = ABCA_Util.ValoreDaLista("dirmodello", aLista, nAttributi)
      cDirArchiviazione = ABCA_Util.ValoreDaLista("dirarchiviazione", aLista, nAttributi)
      cNomeFilePreventivo = cNomeFilePreventivo & ABCA_Util.ValoreDaLista("estensione", aLista, nAttributi)
      ' copio il modello attribuendo il nome del preventivo
      If ABCA_Util.CopiaFile(cDirModello, cNomeModello, cDirArchiviazione, cNomeFilePreventivo, False) = False Then
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
      End If
      If Microsoft.VisualBasic.Right(cDirArchiviazione, 1) <> "\" Then
        cDirArchiviazione = cDirArchiviazione & "\"
      End If
      Dim aValori(100, 1) As String
      Dim nValori As Integer = 0
      Dim nParz As Integer = 0
      Call CaricaDatiSostituzioneWord(aValori, nValori)
      ' apro il file word

      Call oWord.Documents.Open(cDirArchiviazione & cNomeFilePreventivo)
      ' sostituisco i valori segnaposto che sono presenti nel documento
      '           oWord.Visible = True
      Call SostituisciSegnaposto(oWord, aValori, nValori)
      Dim aRighe(200, 12) As String
      Call CaricaRigheDoc(aRighe, nValori)
      Dim aTitoli(6) As String
      Call CaricaTitoli(aTitoli)
      ' inizio a creare la/le griglie (ci possono essere delle alternative)
      Dim nConta As Integer = 0
      Dim nIni As Integer = 1
      Dim nEnd As Integer = 0
      Dim nGrigliaSuDoc As Integer = 0
      Dim cAlternativa As String = aRighe(1, 9)
      For nConta = 1 To nValori + 1
        If (cAlternativa <> aRighe(nConta, 9) Or nConta > nValori) And aRighe(nConta, 11) <> "1" Then
          ' mi posiziono sulla griglia
          nGrigliaSuDoc = nGrigliaSuDoc + 1
          oWord.Selection.GoTo(Name:="Griglia" & Microsoft.VisualBasic.Right("00" & nGrigliaSuDoc, 2))
          ' Creo la griglia
          Call CompilaGriglia(aRighe, nIni, nEnd, aTitoli)

          nIni = nConta
        End If
        nEnd = nConta
      Next
      If cRicambi = "S" Then
        oWord.Selection.GoTo(Name:="Ricambi")
        Call CompilaGrigliaRicambi(aRighe, 1, nValori + 1, aTitoli)
      End If
      ' cancello le griglie inutilizzate
      For nConta = nGrigliaSuDoc + 1 To 10
        oWord.Selection.GoTo(Name:="Griglia" & Microsoft.VisualBasic.Right("00" & nConta, 2))
        oWord.Selection.Delete()
      Next
      If Chk_IVA.Checked = True Then
        oWord.Selection.GoTo(Name:="ScrittaIVA")
        oWord.Selection.Delete()
      End If
      oWord.Documents.Save()
      oWord.Visible = True
      'oWord.Documents.Close()
      'oWord = Nothing
      DistruggioWord()
      Call EsportaInExcel(aRighe, nValori)
    Else
      ' altrimenti esco
      MsgBox("Il file di configurazione xml non è completo!", vbOKOnly, "Attenzione!!")
      Exit Sub
      Me.Cursor = System.Windows.Forms.Cursors.Default
    End If
    Me.Cursor = System.Windows.Forms.Cursors.Default

  End Sub
  Private Sub EsportaInExcel(ByRef aRighe(,) As String, ByRef nValori As Integer)
        If Chk_Excel.Checked = True Then

            Dim oXL As Excel.Application
            Dim oWB As Excel.Workbook
            Dim oSheet As Excel.Worksheet
            Dim oRng As Excel.Range

            Dim nConta As Integer = 0

            ' Start Excel and get Application object.
            oXL = CreateObject("Excel.Application")

            ' Get a new workbook.
            oWB = oXL.Workbooks.Add
            oSheet = oWB.ActiveSheet

            ' Add table headers going cell by cell.
            oSheet.Cells(1, 1).Value = "Articolo"
            oSheet.Cells(1, 2).Value = "Descrizione"
            oSheet.Cells(1, 3).Value = "Quantità"
            oSheet.Cells(1, 4).Value = "Sconti"
            oSheet.Cells(1, 5).Value = "Prz Unitario Lordo"
            oSheet.Cells(1, 6).Value = "Tot. Lordo"
            oSheet.Cells(1, 7).Value = "Prz Unitario Netto"
            oSheet.Cells(1, 8).Value = "Tot. Netto"

            For nConta = 1 To nValori
                oSheet.Cells(nConta + 1, 1).Value = aRighe(nConta, 1)
                oSheet.Cells(nConta + 1, 2).Value = aRighe(nConta, 2)
                oSheet.Cells(nConta + 1, 3).Value = Microsoft.VisualBasic.Val(Replace(aRighe(nConta, 3), ",", "."))
                oSheet.Cells(nConta + 1, 4).Value = aRighe(nConta, 5)
                oSheet.Cells(nConta + 1, 5).Value = Microsoft.VisualBasic.Val(Replace(aRighe(nConta, 4), ",", "."))
                oSheet.Cells(nConta + 1, 6).Value = Microsoft.VisualBasic.Val(Replace(aRighe(nConta, 3), ",", ".")) * Microsoft.VisualBasic.Val(Replace(aRighe(nConta, 4), ",", "."))
                oSheet.Cells(nConta + 1, 7).Value = Microsoft.VisualBasic.Val(Replace(aRighe(nConta, 6), ",", "."))
                oSheet.Cells(nConta + 1, 8).Value = Microsoft.VisualBasic.Val(Replace(aRighe(nConta, 7), ",", "."))
            Next

            oXL.Visible = True

            oRng = Nothing
            oSheet = Nothing
            oWB = Nothing
            '            oXL.Quit()
            oXL = Nothing

        End If
    End Sub
    Private Sub CompilaGriglia(aRighe(,) As String, nIni As Integer, nEnd As Integer, aTitoli() As String)

        Dim nConta As Long = 0
        Dim nCella As Long = 0
        Dim nProgrMP As Long = 0
        Dim nTotaleImponibile As Double = 0
        Dim nTotaleIva As Double = 0
        Dim nCadenza As Long = 0
        Dim cAppoggio As String = ""
        Dim nColonna As Long = 0
        Dim nRiga As Long = 0

        ' Inizializzo l'array
        Call InizializzaArrayGriglia()

        ' Definisco il numero di colonne e la loro dimensione
        If aTitoli(0) = "5" Then
            aGriglia.nNrColonne = 5
            aGriglia.nDimensioneColonne(1) = 80
            aGriglia.nDimensioneColonne(2) = 240
            aGriglia.nDimensioneColonne(3) = 50
            aGriglia.nDimensioneColonne(4) = 60
            aGriglia.nDimensioneColonne(5) = 60
            aGriglia.nAltezzaRighe = 12
        Else
            aGriglia.nNrColonne = 6
            aGriglia.nDimensioneColonne(1) = 80
            aGriglia.nDimensioneColonne(2) = 180
            aGriglia.nDimensioneColonne(3) = 50
            aGriglia.nDimensioneColonne(4) = 60
            aGriglia.nDimensioneColonne(5) = 60
            aGriglia.nDimensioneColonne(6) = 60
            aGriglia.nAltezzaRighe = 12
        End If

        ' *****************************************
        ' Creo intestazione colonne
        ' *****************************************
        ' Articolo
        nRiga = 1
        nCella = nCella + 1
        aGriglia.nNrRighe = aGriglia.nNrRighe + 1
        '        aGriglia.nColonna(nCella) = 1
        '        aGriglia.nRiga(nCella) = nRiga
        aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
        aGriglia.nFontDimensione(nRiga, nCella) = 8
        aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Grassetto
        aGriglia.cTesto(nRiga, nCella) = aTitoli(1)
        aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Centro
        aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
        aGriglia.nColoreSfondo(nRiga, nCella) = WdColor.wdColorGray125
        ' Descrizione
        nCella = nCella + 1
        '      aGriglia.nColonna(nCella) = 2
        '        aGriglia.nRiga(nCella) = nRiga
        aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
        aGriglia.nFontDimensione(nRiga, nCella) = 8
        aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Grassetto
        aGriglia.cTesto(nRiga, nCella) = aTitoli(2)
        aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Centro
        aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
        aGriglia.nColoreSfondo(nRiga, nCella) = WdColor.wdColorGray125
        ' Qtà
        nCella = nCella + 1
        '       aGriglia.nColonna(nCella) = 3
        '        aGriglia.nRiga(nCella) = nRiga
        '        aGriglia.nColonneDaUnire(nCella) = 3
        '       aGriglia.nColonneDaUnireLargh(nCella) = 110
        aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
        aGriglia.nFontDimensione(nRiga, nCella) = 8
        aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Grassetto
        aGriglia.cTesto(nRiga, nCella) = aTitoli(3)
        aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Centro
        aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
        aGriglia.nColoreSfondo(nRiga, nCella) = WdColor.wdColorGray125
        ' Prz Listino o Unitario
        nCella = nCella + 1
        '   aGriglia.nColonna(nCella) = 4
        '  aGriglia.nRiga(nCella) = nRiga
        aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
        aGriglia.nFontDimensione(nRiga, nCella) = 8
        aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Grassetto
        aGriglia.cTesto(nRiga, nCella) = aTitoli(4)
        aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Centro
        aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
        aGriglia.nColoreSfondo(nRiga, nCella) = WdColor.wdColorGray125
        ' Sconto o Totale
        nCella = nCella + 1
        '        aGriglia.nColonna(nCella) = 5
        '        aGriglia.nRiga(nCella) = nRiga
        aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
        aGriglia.nFontDimensione(nRiga, nCella) = 8
        aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Grassetto
        aGriglia.cTesto(nRiga, nCella) = aTitoli(5)
        aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Centro
        aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
        aGriglia.nColoreSfondo(nRiga, nCella) = WdColor.wdColorGray125
        If aTitoli(0) = "6" Then
            ' Sconto o Totale
            nCella = nCella + 1
            '            aGriglia.nColonna(nCella) = 6
            '            aGriglia.nRiga(nCella) = nRiga
            aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
            aGriglia.nFontDimensione(nRiga, nCella) = 8
            aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Grassetto
            aGriglia.cTesto(nRiga, nCella) = aTitoli(6)
            aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Centro
            aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
            aGriglia.nColoreSfondo(nRiga, nCella) = WdColor.wdColorGray125
        End If
        ' inizio delle righe
        nCadenza = 0
        nCella = 0
        For nConta = nIni To nEnd
            If aRighe(nConta, 11) <> "1" Then
                nRiga = nRiga + 1
                aGriglia.nNrRighe = nRiga
                ' Articolo
                nCella = nCella + 1
                '            aGriglia.nColonna(nCella) = 1
                '            aGriglia.nRiga(nCella) = nRiga
                aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                aGriglia.nFontDimensione(nRiga, nCella) = 8
                aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                aGriglia.cTesto(nRiga, nCella) = aRighe(nConta, 1)
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Sinistra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
                ' Descrizione
                nCella = nCella + 1
                '            aGriglia.nColonna(nCella) = 2
                '            aGriglia.nRiga(nCella) = nRiga
                aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                aGriglia.nFontDimensione(nRiga, nCella) = 8
                aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                aGriglia.cTesto(nRiga, nCella) = aRighe(nConta, 2)
                If aRighe(nConta, 12) > " " Then
                    aGriglia.cTesto(nRiga, nCella) = aGriglia.cTesto(nRiga, nCella) & " " & aRighe(nConta, 12)
                End If
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Sinistra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
                ' Qtà
                nCella = nCella + 1
                '            aGriglia.nColonna(nCella) = 3
                '            aGriglia.nRiga(nCella) = nRiga
                aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                aGriglia.nFontDimensione(nRiga, nCella) = 8
                aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                aGriglia.cTesto(nRiga, nCella) = AccomodaValori(aRighe(nConta, 3), 1)
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
                ' Prezzo
                nCella = nCella + 1
                '            aGriglia.nColonna(nCella) = 4
                '            aGriglia.nRiga(nCella) = nRiga
                aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                aGriglia.nFontDimensione(nRiga, nCella) = 8
                aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                If aTitoli(0) = "5" Then
                    aGriglia.cTesto(nRiga, nCella) = AccomodaValori(aRighe(nConta, 6), 2)
                Else
                    aGriglia.cTesto(nRiga, nCella) = AccomodaValori(aRighe(nConta, 4), 2)
                End If
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
                ' Qui si differenzia
                If aTitoli(0) = "5" Then
                    ' prezzo totale
                    nCella = nCella + 1
                    '                aGriglia.nColonna(nCella) = 5
                    '                aGriglia.nRiga(nCella) = nRiga
                    aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                    aGriglia.nFontDimensione(nRiga, nCella) = 8
                    aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                    aGriglia.cTesto(nRiga, nCella) = AccomodaValori(aRighe(nConta, 7), 2)
                    aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                    aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
                Else
                    ' sconto
                    nCella = nCella + 1
                    '                aGriglia.nColonna(nCella) = 5
                    '                aGriglia.nRiga(nCella) = nRiga
                    aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                    aGriglia.nFontDimensione(nRiga, nCella) = 8
                    aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                    If aRighe(nConta, 5) > " " Then
                        aGriglia.cTesto(nRiga, nCella) = Microsoft.VisualBasic.Trim(AccomodaValori(aRighe(nConta, 5), 0)) & "%"
                    Else
                        aGriglia.cTesto(nRiga, nCella) = ""
                    End If
                    aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Centro
                    aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
                    ' prezzo totale
                    nCella = nCella + 1
                    '                aGriglia.nColonna(nCella) = 6
                    '                aGriglia.nRiga(nCella) = nRiga
                    aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                    aGriglia.nFontDimensione(nRiga, nCella) = 8
                    aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                    aGriglia.cTesto(nRiga, nCella) = AccomodaValori(aRighe(nConta, 7), 2)
                    aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                    aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
                End If
                If aRighe(nConta, 10) = "" Then
                Else
                    nTotaleIva = nTotaleIva + CalcolaIVA(aRighe(nConta, 10), aRighe(nConta, 7))
                    nTotaleImponibile = nTotaleImponibile + aRighe(nConta, 7)
                End If
                nCella = 0
            End If
        Next nConta
        If Chk_TotalePreventivo.Checked = True Then
            nRiga = nRiga + 1
            If Chk_IVA.Checked = False Then
                nTotaleIva = 0
            End If
            ' Articolo
            nCella = nCella + 1
            '            aGriglia.nColonna(nCella) = 1
            '            aGriglia.nRiga(nCella) = nRiga
            aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
            aGriglia.nFontDimensione(nRiga, nCella) = 8
            aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
            aGriglia.cTesto(nRiga, nCella) = ""
            aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Sinistra
            aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.No
            ' Descrizione
            nCella = nCella + 1
            '            aGriglia.nColonna(nCella) = 2
            '            aGriglia.nRiga(nCella) = nRiga
            aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
            aGriglia.nFontDimensione(nRiga, nCella) = 8
            aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
            aGriglia.cTesto(nRiga, nCella) = ""
            aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Sinistra
            aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.No
            ' Qtà
            nCella = nCella + 1
            '            aGriglia.nColonna(nCella) = 3
            '            aGriglia.nRiga(nCella) = nRiga
            aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
            aGriglia.nFontDimensione(nRiga, nCella) = 8
            aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
            aGriglia.cTesto(nRiga, nCella) = ""
            aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
            aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.No
            ' Prezzo
            nCella = nCella + 1
            '            aGriglia.nColonna(nCella) = 4
            '            aGriglia.nRiga(nCella) = nRiga
            aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
            aGriglia.nFontDimensione(nRiga, nCella) = 8
            aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
            If aTitoli(0) = "5" Then
                aGriglia.cTesto(nRiga, nCella) = "Totale Imponibile"
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
                aGriglia.nColoreSfondo(nRiga, nCella) = WdColor.wdColorGray125
            Else
                aGriglia.cTesto(nRiga, nCella) = ""
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.No
            End If
            ' Qui si differenzia
            If aTitoli(0) = "5" Then
                ' prezzo totale
                nCella = nCella + 1
                '                aGriglia.nColonna(nCella) = 5
                '                aGriglia.nRiga(nCella) = nRiga
                aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                aGriglia.nFontDimensione(nRiga, nCella) = 8
                aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                aGriglia.cTesto(nRiga, nCella) = AccomodaValori(nTotaleImponibile, 2)
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
            Else
                ' sconto
                nCella = nCella + 1
                '                aGriglia.nColonna(nCella) = 5
                '                aGriglia.nRiga(nCella) = nRiga
                aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                aGriglia.nFontDimensione(nRiga, nCella) = 8
                aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                aGriglia.cTesto(nRiga, nCella) = "Totale Imponibile"
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
                aGriglia.nColoreSfondo(nRiga, nCella) = WdColor.wdColorGray125
                ' prezzo totale
                nCella = nCella + 1
                '                aGriglia.nColonna(nCella) = 6
                '                aGriglia.nRiga(nCella) = nRiga
                aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                aGriglia.nFontDimensione(nRiga, nCella) = 8
                aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                aGriglia.cTesto(nRiga, nCella) = AccomodaValori(nTotaleImponibile, 2)
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
            End If
            nCella = 0
        End If
        If Chk_IVA.Checked = True Then
            nRiga = nRiga + 1
            ' Articolo
            nCella = nCella + 1
            '            aGriglia.nColonna(nCella) = 1
            '            aGriglia.nRiga(nCella) = nRiga
            aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
            aGriglia.nFontDimensione(nRiga, nCella) = 8
            aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
            aGriglia.cTesto(nRiga, nCella) = ""
            aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Sinistra
            aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.No
            ' Descrizione
            nCella = nCella + 1
            '            aGriglia.nColonna(nCella) = 2
            '            aGriglia.nRiga(nCella) = nRiga
            aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
            aGriglia.nFontDimensione(nRiga, nCella) = 8
            aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
            aGriglia.cTesto(nRiga, nCella) = ""
            aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Sinistra
            aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.No
            ' Qtà
            nCella = nCella + 1
            '            aGriglia.nColonna(nCella) = 3
            '            aGriglia.nRiga(nCella) = nRiga
            aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
            aGriglia.nFontDimensione(nRiga, nCella) = 8
            aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
            aGriglia.cTesto(nRiga, nCella) = ""
            aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
            aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.No
            ' Prezzo
            nCella = nCella + 1
            '            aGriglia.nColonna(nCella) = 4
            '            aGriglia.nRiga(nCella) = nRiga
            aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
            aGriglia.nFontDimensione(nRiga, nCella) = 8
            aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
            If aTitoli(0) = "5" Then
                aGriglia.cTesto(nRiga, nCella) = "IVA"
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
                aGriglia.nColoreSfondo(nRiga, nCella) = WdColor.wdColorGray125
            Else
                aGriglia.cTesto(nRiga, nCella) = ""
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.No
            End If
            ' Qui si differenzia
            If aTitoli(0) = "5" Then
                ' prezzo totale
                nCella = nCella + 1
                '                aGriglia.nColonna(nCella) = 5
                '                aGriglia.nRiga(nCella) = nRiga
                aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                aGriglia.nFontDimensione(nRiga, nCella) = 8
                aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                aGriglia.cTesto(nRiga, nCella) = AccomodaValori(nTotaleIva, 2)
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
            Else
                ' sconto
                nCella = nCella + 1
                '                aGriglia.nColonna(nCella) = 5
                '                aGriglia.nRiga(nCella) = nRiga
                aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                aGriglia.nFontDimensione(nRiga, nCella) = 8
                aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                aGriglia.cTesto(nRiga, nCella) = "IVA"
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
                aGriglia.nColoreSfondo(nRiga, nCella) = WdColor.wdColorGray125
                ' prezzo totale
                nCella = nCella + 1
                '                aGriglia.nColonna(nCella) = 6
                '                aGriglia.nRiga(nCella) = nRiga
                aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                aGriglia.nFontDimensione(nRiga, nCella) = 8
                aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                aGriglia.cTesto(nRiga, nCella) = AccomodaValori(nTotaleIva, 2)
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
            End If
            nCella = 0
        End If
        If Chk_TotalePreventivo.Checked = True Then
            nRiga = nRiga + 1
            If Chk_IVA.Checked = False Then
                nTotaleIva = 0
            End If
            ' Articolo
            nCella = nCella + 1
            '            aGriglia.nColonna(nCella) = 1
            '            aGriglia.nRiga(nCella) = nRiga
            aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
            aGriglia.nFontDimensione(nRiga, nCella) = 8
            aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
            aGriglia.cTesto(nRiga, nCella) = ""
            aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Sinistra
            aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.No
            ' Descrizione
            nCella = nCella + 1
            '            aGriglia.nColonna(nCella) = 2
            '            aGriglia.nRiga(nCella) = nRiga
            aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
            aGriglia.nFontDimensione(nRiga, nCella) = 8
            aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
            aGriglia.cTesto(nRiga, nCella) = ""
            aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Sinistra
            aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.No
            ' Qtà
            nCella = nCella + 1
            '            aGriglia.nColonna(nCella) = 3
            '            aGriglia.nRiga(nCella) = nRiga
            aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
            aGriglia.nFontDimensione(nRiga, nCella) = 8
            aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
            aGriglia.cTesto(nRiga, nCella) = ""
            aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
            aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.No
            ' Prezzo
            nCella = nCella + 1
            '            aGriglia.nColonna(nCella) = 4
            '            aGriglia.nRiga(nCella) = nRiga
            aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
            aGriglia.nFontDimensione(nRiga, nCella) = 8
            aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
            If aTitoli(0) = "5" Then
                aGriglia.cTesto(nRiga, nCella) = "Totale"
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
                aGriglia.nColoreSfondo(nRiga, nCella) = WdColor.wdColorGray125
            Else
                aGriglia.cTesto(nRiga, nCella) = ""
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.No
            End If
            ' Qui si differenzia
            If aTitoli(0) = "5" Then
                ' prezzo totale
                nCella = nCella + 1
                '                aGriglia.nColonna(nCella) = 5
                '                aGriglia.nRiga(nCella) = nRiga
                aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                aGriglia.nFontDimensione(nRiga, nCella) = 8
                aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                aGriglia.cTesto(nRiga, nCella) = AccomodaValori(nTotaleIva + nTotaleImponibile, 2)
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
            Else
                ' sconto
                nCella = nCella + 1
                '                aGriglia.nColonna(nCella) = 5
                '                aGriglia.nRiga(nCella) = nRiga
                aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                aGriglia.nFontDimensione(nRiga, nCella) = 8
                aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                aGriglia.cTesto(nRiga, nCella) = "Totale"
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
                aGriglia.nColoreSfondo(nRiga, nCella) = WdColor.wdColorGray125
                ' prezzo totale
                nCella = nCella + 1
                '                aGriglia.nColonna(nCella) = 6
                '                aGriglia.nRiga(nCella) = nRiga
                aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                aGriglia.nFontDimensione(nRiga, nCella) = 8
                aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                aGriglia.cTesto(nRiga, nCella) = AccomodaValori(nTotaleIva + nTotaleImponibile, 2)
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
            End If
            nCella = 0
        End If
        aGriglia.nNrRighe = nRiga
        ' ASSEGNO IL NUMERO DI CAMPI COMPILATI
        ' aGriglia.nCampiTotali = nCella
        ' CREO LA GRIGLIA
        Call CreaGriglia()

    End Sub
    '******************************************************************************************************************************************************

    Private Sub CompilaGrigliaRicambi(aRighe(,) As String, nIni As Integer, nEnd As Integer, aTitoli() As String)

        Dim nConta As Long = 0
        Dim nCella As Long = 0
        Dim nProgrMP As Long = 0
        Dim nTotaleImponibile As Double = 0
        Dim nTotaleIva As Double = 0
        Dim nCadenza As Long = 0
        Dim cAppoggio As String = ""
        Dim nColonna As Long = 0
        Dim nRiga As Long = 0

        ' Inizializzo l'array
        Call InizializzaArrayGriglia()

        ' Definisco il numero di colonne e la loro dimensione
        If bPrivato = False Then
            aGriglia.nNrColonne = 6
            aGriglia.nDimensioneColonne(1) = 80
            aGriglia.nDimensioneColonne(2) = 180
            aGriglia.nDimensioneColonne(3) = 50
            aGriglia.nDimensioneColonne(4) = 60
            aGriglia.nDimensioneColonne(5) = 60
            aGriglia.nDimensioneColonne(6) = 60
            aGriglia.nAltezzaRighe = 12
        Else
            aGriglia.nNrColonne = 7
            aGriglia.nDimensioneColonne(1) = 80
            aGriglia.nDimensioneColonne(2) = 160
            aGriglia.nDimensioneColonne(3) = 40
            aGriglia.nDimensioneColonne(4) = 50
            aGriglia.nDimensioneColonne(5) = 50
            aGriglia.nDimensioneColonne(6) = 50
            aGriglia.nDimensioneColonne(7) = 50
            aGriglia.nAltezzaRighe = 12
        End If

        ' *****************************************
        ' Creo intestazione colonne
        ' *****************************************
        ' Articolo
        nRiga = 1
        nCella = nCella + 1
        aGriglia.nNrRighe = aGriglia.nNrRighe + 1
        '        aGriglia.nColonna(nCella) = 1
        '        aGriglia.nRiga(nCella) = nRiga
        aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
        aGriglia.nFontDimensione(nRiga, nCella) = 8
        aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Grassetto
        aGriglia.cTesto(nRiga, nCella) = "Codice"
        aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Centro
        aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
        aGriglia.nColoreSfondo(nRiga, nCella) = WdColor.wdColorGray125
        ' Descrizione
        nCella = nCella + 1
        '      aGriglia.nColonna(nCella) = 2
        '        aGriglia.nRiga(nCella) = nRiga
        aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
        aGriglia.nFontDimensione(nRiga, nCella) = 8
        aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Grassetto
        aGriglia.cTesto(nRiga, nCella) = "Descrizione"
        aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Centro
        aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
        aGriglia.nColoreSfondo(nRiga, nCella) = WdColor.wdColorGray125
        ' Qtà
        nCella = nCella + 1
        '       aGriglia.nColonna(nCella) = 3
        '        aGriglia.nRiga(nCella) = nRiga
        '        aGriglia.nColonneDaUnire(nCella) = 3
        '       aGriglia.nColonneDaUnireLargh(nCella) = 110
        aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
        aGriglia.nFontDimensione(nRiga, nCella) = 8
        aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Grassetto
        aGriglia.cTesto(nRiga, nCella) = "Qtà"
        aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Centro
        aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
        aGriglia.nColoreSfondo(nRiga, nCella) = WdColor.wdColorGray125
        ' Prz Listino o Unitario
        nCella = nCella + 1
        '   aGriglia.nColonna(nCella) = 4
        '  aGriglia.nRiga(nCella) = nRiga
        aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
        aGriglia.nFontDimensione(nRiga, nCella) = 8
        aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Grassetto
        aGriglia.cTesto(nRiga, nCella) = "Prezzo Listino"
        aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Centro
        aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
        aGriglia.nColoreSfondo(nRiga, nCella) = WdColor.wdColorGray125
        ' Sconto o Totale
        nCella = nCella + 1
        '        aGriglia.nColonna(nCella) = 5
        '        aGriglia.nRiga(nCella) = nRiga
        aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
        aGriglia.nFontDimensione(nRiga, nCella) = 8
        aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Grassetto
        aGriglia.cTesto(nRiga, nCella) = "Sconto Riservato"
        aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Centro
        aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
        aGriglia.nColoreSfondo(nRiga, nCella) = WdColor.wdColorGray125
        ' Sconto o Totale
        nCella = nCella + 1
        '            aGriglia.nColonna(nCella) = 6
        '            aGriglia.nRiga(nCella) = nRiga
        aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
        aGriglia.nFontDimensione(nRiga, nCella) = 8
        aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Grassetto
        aGriglia.cTesto(nRiga, nCella) = "Totale Netto"
        aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Centro
        aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
        aGriglia.nColoreSfondo(nRiga, nCella) = WdColor.wdColorGray125
        If bPrivato = True Then
            ' Sconto o Totale
            nCella = nCella + 1
            '            aGriglia.nColonna(nCella) = 6
            '            aGriglia.nRiga(nCella) = nRiga
            aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
            aGriglia.nFontDimensione(nRiga, nCella) = 8
            aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Grassetto
            aGriglia.cTesto(nRiga, nCella) = "Tot. + IVA"
            aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Centro
            aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
            aGriglia.nColoreSfondo(nRiga, nCella) = WdColor.wdColorGray125
        End If
        ' inizio delle righe

        '        CalcolaIVA(aRighe(nConta, 10), aRighe(nConta, 3) * aRighe(nConta, 7))
        '        nTotaleImponibile = nTotaleImponibile + aRighe(nConta, 7)

        nCadenza = 0
        nCella = 0
        For nConta = nIni To nEnd
            If aRighe(nConta, 11) = "1" Then
                nRiga = nRiga + 1
                aGriglia.nNrRighe = nRiga
                ' Articolo
                nCella = nCella + 1
                '            aGriglia.nColonna(nCella) = 1
                '            aGriglia.nRiga(nCella) = nRiga
                aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                aGriglia.nFontDimensione(nRiga, nCella) = 8
                aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                aGriglia.cTesto(nRiga, nCella) = aRighe(nConta, 1)
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Sinistra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
                ' Descrizione
                nCella = nCella + 1
                '            aGriglia.nColonna(nCella) = 2
                '            aGriglia.nRiga(nCella) = nRiga
                aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                aGriglia.nFontDimensione(nRiga, nCella) = 8
                aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                aGriglia.cTesto(nRiga, nCella) = aRighe(nConta, 2)
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Sinistra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
                ' Qtà
                nCella = nCella + 1
                '            aGriglia.nColonna(nCella) = 3
                '            aGriglia.nRiga(nCella) = nRiga
                aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                aGriglia.nFontDimensione(nRiga, nCella) = 8
                aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                aGriglia.cTesto(nRiga, nCella) = AccomodaValori(aRighe(nConta, 3), 1)
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
                ' Prezzo
                nCella = nCella + 1
                '            aGriglia.nColonna(nCella) = 4
                '            aGriglia.nRiga(nCella) = nRiga
                aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                aGriglia.nFontDimensione(nRiga, nCella) = 8
                aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                If aTitoli(0) = "5" Then
                    aGriglia.cTesto(nRiga, nCella) = AccomodaValori(aRighe(nConta, 6), 2)
                Else
                    aGriglia.cTesto(nRiga, nCella) = AccomodaValori(aRighe(nConta, 4), 2)
                End If
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
                ' Qui si differenzia
                ' sconto
                nCella = nCella + 1
                '                aGriglia.nColonna(nCella) = 5
                '                aGriglia.nRiga(nCella) = nRiga
                aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                aGriglia.nFontDimensione(nRiga, nCella) = 8
                aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                If aRighe(nConta, 5) > " " Then
                    aGriglia.cTesto(nRiga, nCella) = Microsoft.VisualBasic.Trim(AccomodaValori(aRighe(nConta, 5), 0)) & "%"
                Else
                    aGriglia.cTesto(nRiga, nCella) = ""
                End If
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Centro
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
                ' prezzo totale
                nCella = nCella + 1
                '                aGriglia.nColonna(nCella) = 6
                '                aGriglia.nRiga(nCella) = nRiga
                aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                aGriglia.nFontDimensione(nRiga, nCella) = 8
                aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                aGriglia.cTesto(nRiga, nCella) = AccomodaValori(aRighe(nConta, 7), 2)
                aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
                If bPrivato = True Then
                    ' prezzo totale
                    nTotaleImponibile = aRighe(nConta, 7)
                    nTotaleImponibile = nTotaleImponibile + CalcolaIVA(aRighe(nConta, 10), aRighe(nConta, 3) * aRighe(nConta, 7))
                    nCella = nCella + 1
                    aGriglia.cFontNome(nRiga, nCella) = "Times New Roman"
                    aGriglia.nFontDimensione(nRiga, nCella) = 8
                    aGriglia.nFontTipo(nRiga, nCella) = enmTipoCarattere.Normale
                    aGriglia.cTesto(nRiga, nCella) = AccomodaValori(nTotaleImponibile, 2)
                    aGriglia.nTestoAllineamento(nRiga, nCella) = enmAllineamento.Destra
                    aGriglia.nLineaContorno(nRiga, nCella) = enmLineaContorno.UpDxDownSx
                End If
                nCella = 0
            End If
        Next nConta
        aGriglia.nNrRighe = nRiga
        ' ASSEGNO IL NUMERO DI CAMPI COMPILATI
        ' aGriglia.nCampiTotali = nCella
        ' CREO LA GRIGLIA
        Call CreaGriglia()

    End Sub

    Private Function CalcolaIVA(nCodIVA As Long, nImponibile As Double) As Double
        Dim oSql As New clsABCA_SQL
        Dim nConta As Integer = 0
        Dim cQuery As String = ""
        Dim nIVA As Double = 0
        Try
            ' APRO LA CONNESSIONE
            Call oSql.ApriConnessione(cServer, cDatabase, cUtente, cPassword)
            ' CREO LA QUERY PER CERCARE L'ID TESTA
            cQuery = "SELECT * FROM TABCIVA WHERE TB_CODCIVA = " & nCodIVA
            ' CARICO I RECORDS
            If oSql.CaricaRecords(cQuery) = True Then
                nIVA = Int((nImponibile * oSql.RestituisciValore("TB_ALIQ", 1)) * 100 + 0.99) / 10000
            End If
        Catch EX As Exception

        End Try
        Return nIVA
    End Function
    Private Sub CaricaTitoli(ByRef aTitoli() As String)
        aTitoli(1) = "Codice"
        aTitoli(2) = "Descrizione"
        aTitoli(3) = "Qtà"
        If Chk_PrezzoFinito.Checked = True Then
            aTitoli(4) = "Prezzo Unitario"
            aTitoli(5) = "Prezzo Finale"
            aTitoli(0) = "5"
        Else
            aTitoli(4) = "Prezzo Listino"
            aTitoli(5) = "Sconto riservato"
            aTitoli(6) = "Prezzo Finale"
            aTitoli(0) = "6"
        End If
    End Sub
  Private Sub CaricaRigheDoc(ByRef aRighe(,) As String, ByRef nValori As Integer)
    Dim oSql As New clsABCA_SQL
    Dim nConta As Integer = 0
    Dim cQuery As String = ""
    Dim dValore As Double = 0

    Try
      ' APRO LA CONNESSIONE
      Call oSql.ApriConnessione(cServer, cDatabase, cUtente, cPassword)
      ' leggo indici documento
      Dim aDoc() As String = Split(Txt_Progressivo.Text, ";")

      cQuery = "SELECT RDO.MO_RIGA AS POSIZIONE, RDO.MO_CODART AS CODART, RDO.MO_DESCR AS DESCRIZIONEART, RDO.MO_QUANT AS QTAPREZZO, "
      cQuery &= "RDO.MO_PREZZO AS PREZZOUNITLORDOEURO, RDO.MO_SCONT1 AS SCONTIESTESI, (RDO.MO_VALORE / RDO.MO_QUANT) AS PREZZOUNITNETTOEURO, "
      cQuery &= "RDO.MO_VALORE AS TOTNETTORIGAEURO, RDO.MO_CODIVA, TCI.TB_ALIQ AS ALIQUOTA, RDO.mo_hhoffalt AS OffertaAlternativa, "
      cQuery &= "0 AS LISTINORICAMBI, ISNULL(RDO.MO_NOTE, '') AS NTT_NOTA "
      cQuery &= "FROM MOVORD RDO "
      cQuery &= "INNER JOIN TABCIVA TCI ON RDO.MO_CODIVA = TCI.TB_CODCIVA "
      cQuery &= "WHERE RDO.MO_TIPORK = 'Q' AND RDO.MO_ANNO = " & aDoc(0).ToString & " AND "
      cQuery &= "RDO.MO_SERIE = '" & aDoc(1).ToString & "' AND "
      cQuery &= "RDO.MO_NUMORD = " & aDoc(2).ToString & " "
      cQuery &= "ORDER BY RDO.MO_RIGA"
      ' CARICO I RECORDS DELLE RIGHE
      If oSql.CaricaRecords(cQuery) = True Then
        ' CARICO I VALORI NELL'ARRAY
        nValori = oSql.nNrRecords
        For nConta = 1 To oSql.nNrRecords
          aRighe(nConta, 1) = TrasformaCodArt(oSql.RestituisciValore("CODART", nConta))
          aRighe(nConta, 2) = oSql.RestituisciValore("DESCRIZIONEART", nConta)
          aRighe(nConta, 3) = oSql.RestituisciValore("QTAPREZZO", nConta)
          aRighe(nConta, 4) = oSql.RestituisciValore("PREZZOUNITLORDOEURO", nConta)
          aRighe(nConta, 5) = oSql.RestituisciValore("SCONTIESTESI", nConta)
          aRighe(nConta, 6) = oSql.RestituisciValore("PREZZOUNITNETTOEURO", nConta)
          aRighe(nConta, 7) = oSql.RestituisciValore("TOTNETTORIGAEURO", nConta)
          If CInt(oSql.RestituisciValore("PREZZOUNITNETTOEURO", nConta)) = 0 Then
            aRighe(nConta, 8) = oSql.RestituisciValore("PREZZOUNITLORDOEURO", nConta)
          Else
            dValore = oSql.RestituisciValore("PREZZOUNITLORDOEURO", nConta)
            dValore = dValore + CDec(dValore * oSql.RestituisciValore("ALIQUOTA", nConta)) / 100
            aRighe(nConta, 8) = dValore
          End If
          aRighe(nConta, 9) = oSql.RestituisciValore("OffertaAlternativa", nConta)
          aRighe(nConta, 10) = oSql.RestituisciValore("MO_CODIVA", nConta)
          aRighe(nConta, 11) = oSql.RestituisciValore("LISTINORICAMBI", nConta)
          aRighe(nConta, 12) = oSql.RestituisciValore("NTT_NOTA", nConta)
        Next
      End If
      Call oSql.ChiudiConnessione()
    Catch ex As Exception

    End Try

  End Sub

  Private Function TrasformaCodArt(cArticolo As String) As String
        Dim cAppoggio As String = ""
        Dim nConta As Integer = 0
        Try
            For nConta = 1 To Len(cArticolo) Step 3
                cAppoggio = cAppoggio + Microsoft.VisualBasic.Mid(cArticolo, nConta, 1)
            Next
            For nConta = 2 To Len(cArticolo) Step 3
                cAppoggio = cAppoggio + Microsoft.VisualBasic.Mid(cArticolo, nConta, 1)
            Next
            For nConta = 3 To Len(cArticolo) Step 3
                cAppoggio = cAppoggio + Microsoft.VisualBasic.Mid(cArticolo, nConta, 1)
            Next
            Return cAppoggio
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Private Sub SostituisciSegnaposto(oWord As Object, aValori(,) As String, nValori As Integer)
        ' sostituisce tutte i segnaposto con le relative descrizioni
        For nConta = 1 To nValori
            oWord.Selection.Find.Execute("" & aValori(nConta, 0), False, False, False, False, False, False, 1, False, "" & aValori(nConta, 1), 2)
        Next

    End Sub

  Private Sub CaricaDatiSostituzioneWord(ByRef aValori(,) As String, ByRef nValori As Integer)
    Dim oSql As New clsABCA_SQL
    Dim nConta As Integer = 0
    Dim cQuery As String = ""
    Dim cBanca As String = ""

    Try
      If Chk_CliFatt.Checked = True Then
        cQuery = "SELECT * FROM ANAGRA WHERE AN_CONTO = " & Microsoft.VisualBasic.Left(Txt_Cliente.Text, 7)
      Else
        cQuery = "SELECT * FROM ANAGRA WHERE AN_CONTO = " & Microsoft.VisualBasic.Left(Txt_ClienteDestinatario.Text, 7)
      End If
      Call oSql.ApriConnessione(cServer, cDatabase, cUtente, cPassword)
      If oSql.CaricaRecords(cQuery) = True Then
        aValori(1, 0) = "<RAGSOC>"
        aValori(1, 1) = oSql.RestituisciValore("AN_DESCR1", 1)
        aValori(2, 0) = "<INDIRIZZO>"
        aValori(2, 1) = oSql.RestituisciValore("AN_INDIR", 1)
        aValori(3, 0) = "<CAP>"
        aValori(3, 1) = oSql.RestituisciValore("AN_CAP", 1)
        aValori(4, 0) = "<LOCALITA>"
        aValori(4, 1) = oSql.RestituisciValore("AN_CITTA", 1)
        aValori(5, 0) = "<PROVINCIA>"
        aValori(5, 1) = oSql.RestituisciValore("AN_PROV", 1)
        aValori(6, 0) = "<TELEFONO>"
        aValori(6, 1) = oSql.RestituisciValore("AN_TELEF", 1)
        aValori(7, 0) = "<FAX>"
        aValori(7, 1) = oSql.RestituisciValore("AN_FAXTLX", 1)
        aValori(8, 0) = "<EMAIL>"
        aValori(8, 1) = Txt_Email.Text
        aValori(9, 0) = "<DATA>"
        aValori(9, 1) = Txt_DataOfferta.Text
        aValori(10, 0) = "<NR_PREVENTIVO>"
        aValori(10, 1) = Txt_NrPreventivo.Text
        aValori(11, 0) = "<ATTENZIONE>"
        aValori(11, 1) = Txt_CorteseAttenzione.Text
        aValori(12, 0) = "<OGGETTO>"
        aValori(12, 1) = Txt_Oggetto.Text
        aValori(13, 0) = "<TITOLO>"
        aValori(13, 1) = Cmb_Titolo.Text
        aValori(14, 0) = "<VALIDITA>"
        aValori(14, 1) = Txt_Validita.Text
        aValori(15, 0) = "<CONSEGNA>"
        aValori(15, 1) = Txt_TempiConsegna.Text
        aValori(16, 0) = "<RESA_MERCE>"
        aValori(16, 1) = Txt_ResaMerce.Text
        aValori(17, 0) = "<GARANZIA>"
        aValori(17, 1) = Txt_Garanzia.Text
        aValori(18, 0) = ""
        aValori(18, 1) = ""
        aValori(19, 0) = ""
        aValori(19, 1) = ""
        aValori(20, 0) = ""
        aValori(20, 1) = ""
        Dim aDoc() As String = Split(Txt_Progressivo.Text, ";")
        cQuery = "SELECT TD_CODPAGA AS CODPAGAMENTO, TD_CODBANC AS CODBANCAINCASSO FROM TESTORD WHERE "
        cQuery &= "td_tipork = 'Q' and td_anno = " & aDoc(0).ToString & " and "
        cQuery &= "td_serie = '" & aDoc(1).ToString & "' and "
        cQuery &= "td_numord = " & aDoc(2).ToString
        'ESERCIZIO = " + Cmb_Anno.Text + " And TIPODOC = '" + Txt_TipoDoc.Text + "' AND NUMERODOC = " & Cmb_NrDoc.Text
        If oSql.CaricaRecords(cQuery) = True Then
          cBanca = oSql.RestituisciValore("CODBANCAINCASSO", 1)
          cQuery = "SELECT tb_despaga as DESCRIZIONE FROM TABPAGA WHERE tb_codpaga = " & oSql.RestituisciValore("CODPAGAMENTO", 1)
          If oSql.CaricaRecords(cQuery) = True Then
            aValori(18, 0) = "<PAGAMENTO>"
            aValori(18, 1) = oSql.RestituisciValore("DESCRIZIONE", 1)
            If cBanca > " " Then
              cQuery = "SELECT tb_desbanc as DSCBANCA, tb_iban as CODICEIBAN FROM tabbanc WHERE tb_codbanc = " & cBanca
              If oSql.CaricaRecords(cQuery) = True Then
                aValori(19, 0) = "<BANCAAPPOGGIO>"
                aValori(19, 1) = oSql.RestituisciValore("DSCBANCA", 1)
                aValori(20, 0) = "<IBAN>"
                aValori(20, 1) = oSql.RestituisciValore("CODICEIBAN", 1)
              End If
            End If
          End If
        End If
        aValori(21, 0) = "<NOTEOFFERTAWORD>"
        aValori(21, 1) = ""
        '                If oSql.CaricaRecords(cQuery) = True Then
        ''If Chk_CliFatt.Checked = True Then
        ''    cQuery = "SELECT NOTEOFFERTAWORD FROM EXTRACLIENTI WHERE CODCONTO = '" & Microsoft.VisualBasic.Left(Txt_Cliente.Text, 7) & "'"
        ''Else
        ''    cQuery = "SELECT NOTEOFFERTAWORD FROM EXTRACLIENTI WHERE CODCONTO = '" & Microsoft.VisualBasic.Left(Txt_ClienteDestinatario.Text, 7) & "'"
        ''End If

        'If oSql.CaricaRecords(cQuery) = True Then
        '    aValori(21, 1) = oSql.RestituisciValore("NOTEOFFERTAWORD", 1)
        '    'End If
        'End If

        ' MAI USATO
        aValori(21, 1) = " "

        aValori(22, 0) = "<PRESSO>"
        aValori(22, 1) = ""
        If Txt_ClienteDestinatario.Text <> Microsoft.VisualBasic.Left(Txt_Cliente.Text, 7) And Chk_Presso.Checked = True Then
          cQuery = "SELECT * FROM ANAGRA WHERE AN_CONTO = " & Microsoft.VisualBasic.Left(Txt_ClienteDestinatario.Text, 7)
          If oSql.CaricaRecords(cQuery) = True Then
            aValori(22, 1) = " presso " & oSql.RestituisciValore("AN_DESCR1", 1) & " di " & oSql.RestituisciValore("AN_CITTA", 1) & " (" & oSql.RestituisciValore("AN_PROV", 1) & ")"
          End If
        End If
        nValori = 22
        ' VERIFICO SE SIA UN UTENTE PRIVATO O STRUTTURA/SOCIETA
        ' ***********************************************************************************************
        ' il dato non è stato convertito e non è usato da tempo
        ' ***********************************************************************************************
        '                cQuery = "SELECT CODSETTORE FROM ANAGRAFICARISERVATICF WHERE ESERCIZIO = " + Cmb_Anno.Text + " AND CODCONTO = '" & Microsoft.VisualBasic.Left(Txt_Cliente.Text, 7) & "'"
        '                If oSql.CaricaRecords(cQuery) = True Then
        '                If oSql.RestituisciValore("CODSETTORE", 1) <> "21" Then
        bPrivato = False
        '                    Else
        '                bPrivato = True
        '            End If
        '            End If
      End If
      Call oSql.ChiudiConnessione()
    Catch ex As Exception

    End Try

  End Sub

  Private Function AccomodaValori(cNumero As String, nDecimali As Integer) As String
        Dim cAppoggio As String = ""
        Dim nValore As Double = 0
        nValore = Val(Replace(cNumero, ",", "."))
        Select Case nDecimali
            Case 0
                cAppoggio = "###,###"
            Case 1
                cAppoggio = "###,###.0"
            Case 2
                cAppoggio = "###,###.00"
            Case 3
                cAppoggio = "###,###.000"
        End Select
        cAppoggio = Format(nValore, cAppoggio)
        Return cAppoggio & "  "
    End Function

    Private Sub Chk_CliFatt_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_CliFatt.CheckedChanged
        Call InfoCliente()
    End Sub

   
End Class
