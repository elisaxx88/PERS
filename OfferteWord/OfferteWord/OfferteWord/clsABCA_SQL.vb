Imports System.Data.SqlClient
Imports VB = Microsoft.VisualBasic
'Imports System
'Imports System.Reflection

Public Class clsABCA_SQL
    Dim cStrigaDiConnessione As String
    Dim oCnn As SqlConnection
    Dim oCmd As SqlCommand
    Dim oReader As SqlDataReader
    Dim oDataset As New DataSet
    Dim aCampi(500, 5) As String
    Public nCampi As Integer = 0
    Public nNrRecords As Integer = 0
    Public nNrCampi As Integer = 0
    Public Enum enmTipoData
        Data = 1
        DataOra = 2
    End Enum
    Public Function NomeCampo(nNrCampo As Integer) As String
        Try
            Return aCampi(nNrCampo, 1)
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function ApriConnessione(cServer As String, cDatabase As String, cUtente As String, cPassword As String, Optional cStringaConnessione As String = "") As Boolean
        ' *************************************************************
        ' Funzione che apre la connessione al database
        ' *************************************************************
        If cStringaConnessione > " " Then
            cStrigaDiConnessione = cStringaConnessione
        Else
            cStrigaDiConnessione = "Data Source=" & cServer & ";Initial Catalog=" & cDatabase & ";User ID=" & cUtente & ";Password=" & cPassword
        End If

        oCnn = New SqlConnection(cStrigaDiConnessione)
        Try
            oCnn.Open()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function ChiudiConnessione() As Boolean
        ' *************************************************************
        ' funzione di chiusura del database
        ' *************************************************************
        Try
            If IsNothing(oReader) = True Then
            Else
                If oReader.IsClosed = False Then
                    Call oReader.Close()
                End If
            End If
            oCmd.Dispose()
            oCnn.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function AACaricaRecord(ByRef oDs As DataSet, cQuery As String) As Boolean
        Try
            Dim oAdapter As New SqlDataAdapter
            oAdapter.SelectCommand = New SqlCommand(cQuery, oCnn)
            If oDs.Tables.Count > 0 Then
                Call oDs.Reset() '.Tables(0).Clear()
            End If
            oAdapter.Fill(oDs)
            nNrCampi = oDs.Tables(0).Columns.Count
            nNrRecords = oDs.Tables(0).Rows.Count
            If nNrRecords > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function CaricaRecords(cQuery As String) As Boolean
        Try
            Dim oAdapter As New SqlDataAdapter
            oAdapter.SelectCommand = New SqlCommand(cQuery, oCnn)
            If oDataset.Tables.Count > 0 Then
                Call oDataset.Reset() '.Tables(0).Clear()
            End If
            oAdapter.Fill(oDataset)
            nNrCampi = oDataset.Tables(0).Columns.Count
            nNrRecords = oDataset.Tables(0).Rows.Count
            '            If oDataset.Tables.Contains("tabmastri") Then
            ' MsgBox("c'è")
            ' End If
            If nNrRecords > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function RestituisciValore(cNomecampo As String, nRecord As Integer) As String
        Try
            If nRecord > nNrRecords Then
                Return ""
            End If
            Return oDataset.Tables(0).Rows(nRecord - 1).Item(cNomecampo.ToUpper).ToString
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function CaricaRecordsTop(cQuery As String) As Boolean
        ' funzione che consente di caricare i nomi campi e  le caratteristiche
        Try
            oCmd = New SqlCommand(cQuery, oCnn)
            If IsNothing(oReader) = True Then
            Else
                If oReader.IsClosed = False Then
                    Call oReader.Close()
                End If
            End If
            oReader = oCmd.ExecuteReader()
            Call CaricaNomi()
            Call oReader.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function CaricaNomi() As Boolean
        ' *************************************************************
        ' carico i nomi dei campi, le caratteristiche dei campi e
        ' altre informazioni
        ' *************************************************************
        Dim oSchemaTabella As System.Data.DataTable
        nCampi = 0
        oSchemaTabella = oReader.GetSchemaTable
        For Each myField As DataRow In oSchemaTabella.Rows
            nCampi += 1
            For Each myProperty As DataColumn In oSchemaTabella.Columns
                Select Case myProperty.ColumnName.ToUpper
                    Case "COLUMNNAME"
                        aCampi(nCampi, 1) = myField(myProperty).ToString()
                    Case "DATATYPENAME"
                        aCampi(nCampi, 2) = myField(myProperty).ToString()
                    Case "COLUMNSIZE"
                        aCampi(nCampi, 3) = myField(myProperty).ToString()
                    Case "ALLOWDBNULL"
                        aCampi(nCampi, 4) = myField(myProperty).ToString()
                    Case "COLUMNORDINAL"
                        aCampi(nCampi, 5) = myField(myProperty).ToString()
                End Select
            Next
        Next
        Return True
    End Function
    Public Function PosizioneCampo(cNomeCampo As String) As Integer
        ' restituisce la posizione del campo
        Dim nPosizione As Integer
        cNomeCampo = cNomeCampo.ToUpper
        For nPosizione = 1 To nCampi
            If aCampi(nPosizione, 1) = cNomeCampo Then
                Return nPosizione
            End If
        Next
        Return 0
    End Function

    Public Function EseguiSQL(cQuery As String) As Boolean
        ' esegue un comando di inserimento, aggiornamento o cancellazione
        Try
            Dim oCommand As New SqlCommand
            oCommand.Connection = oCnn
            oCommand.CommandText = cQuery
            oCommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function DataOra(nTipo As enmTipoData, Optional cDataDaTrasformare As String = "") As String
        Dim cAppoggio As String = ""
        Try
            Dim dDataDaTrasformare As Date
            '{ts '2004-10-12 00:00:00'}
            If IsDate(cDataDaTrasformare) = False Then
                dDataDaTrasformare = Date.Now
            Else
                dDataDaTrasformare = DateTime.Parse(cDataDaTrasformare)
            End If

            cAppoggio = "{ts '" & dDataDaTrasformare.Year & "-"
            cAppoggio = cAppoggio & Right("00" & dDataDaTrasformare.Month, 2) & "-"
            cAppoggio = cAppoggio & Right("00" & dDataDaTrasformare.Day, 2) & " "
            If nTipo = 1 Then
                ' solo data
                cAppoggio = cAppoggio & "00:00:00'}"
            Else
                ' date e ora
                cAppoggio = cAppoggio & Right("00" & dDataDaTrasformare.Hour, 2) & ":"
                cAppoggio = cAppoggio & Right("00" & dDataDaTrasformare.Minute, 2) & ":"
                cAppoggio = cAppoggio & Right("00" & dDataDaTrasformare.Second, 2) & "'}"
            End If
        Catch ex As Exception

        End Try

        Return cAppoggio
    End Function
    Public Sub PulisciForm(oForm As Form)
        Dim oCheck As New System.Windows.Forms.CheckBox
        Dim oCombo As New System.Windows.Forms.ComboBox
        Try
            For Each oControl As Control In oForm.Controls
                Select Case TypeName(oControl).ToUpper
                    Case "TEXTBOX"
                        oControl.Text = ""
                    Case "COMBOBOX"
                        oCombo = oControl
                        oCombo.SelectedIndex = 0
                    Case "LABEL"
                        If oControl.Enabled = True Then
                            oControl.Text = "..."
                        End If
                    Case "CHECKBOX"
                        oCheck = oControl
                        oCheck.Checked = False
                End Select
            Next
        Catch ex As Exception
        End Try
    End Sub
    Public Function SalvaRecordsSuArray(ByRef aCampi(,) As String) As Boolean
        Dim nContaMax As Integer = Val(aCampi(0, 0))
        Dim nConta As Integer = 0
        Dim aTabelle(20) As String
        Dim nTabelleMax As Integer = 1
        Dim nTabelle As Integer = 0
        Dim bTrovata As Boolean = False
        ' METTO LA PRIMA TABELLA NELL'ARRAY
        aTabelle(nTabelleMax) = aCampi(1, 0)
        For nConta = 2 To nContaMax
            ' IMPOSTO LA VARIABILE DI VERIFICA A FALSE
            bTrovata = False
            For nTabelle = 1 To nTabelleMax
                ' PASSO TUTTE LE TABELLE
                If aTabelle(nTabelle) = aCampi(nConta, 0) Then
                    ' SE LA TROVO ALLORA IMPORTO LA VARIABILE A TRUE
                    bTrovata = True
                    Exit For
                End If
            Next
            If bTrovata = False Then
                ' LA TABELLA NON E' STATA TROVATA E QUINDI LA AGGIUNGO
                nTabelleMax += 1
                aTabelle(nTabelleMax) = aCampi(nConta, 0)
            End If
        Next
        For nTabelle = 1 To nTabelleMax
            If SalvaRecordsSuArray_Tabella(aCampi, aTabelle(nTabelle)) = False Then
                MsgBox("errore sul salvataggio dati tabella " & aTabelle(nTabelle))
                Return False
            End If
        Next
        Return True
    End Function
    Private Function SalvaRecordsSuArray_Tabella(ByRef aCampi(,) As String, cTabella As String) As Boolean
        Dim cQuery As String = ""
        Dim cWhere As String = ""
        Dim cCampi As String = ""
        Dim cValori As String = ""
        Dim nConta As Integer = 0
        Dim bRisposta As Boolean = False

        ' apro la connessione
        If ApriConnessione("", "", "", "", aCampi(0, 1)) = False Then
            Return False
        End If
        ' Verifico se il record è già presente sul database
        For nConta = 1 To Val(aCampi(0, 0))
            If aCampi(nConta, 0) = cTabella Then
                If aCampi(nConta, 3) = "S" Then
                    cWhere = cWhere & " AND " & aCampi(nConta, 1) & " = " & aCampi(nConta, 2)
                End If
            End If
        Next
        cWhere = Right(cWhere, VB.Len(cWhere) - 4)
        cQuery = "SELECT * FROM " & cTabella & " WHERE " & cWhere
        If CaricaRecords(cQuery) = False Then
            ' il record non è presente e quindi procedo con inserimento
            For nConta = 1 To Val(aCampi(0, 0))
                If aCampi(nConta, 0) = cTabella Then
                    cCampi = cCampi & ", " & aCampi(nConta, 1)
                    cValori = cValori & ", " & aCampi(nConta, 2)
                End If
            Next
            cCampi = Right(cCampi, VB.Len(cCampi) - 2)
            cValori = Right(cValori, VB.Len(cValori) - 2)
            cQuery = "INSERT INTO " & cTabella & " (" & cCampi & ") VALUES (" & cValori & ")"
            bRisposta = EseguiSQL(cQuery)
            Call ChiudiConnessione()
            Return bRisposta
        Else
            ' il record è presente e quindi procedo con l'aggiornamento
            cQuery = ""
            For nConta = 1 To Val(aCampi(0, 0))
                If aCampi(nConta, 0) = cTabella Then
                    If aCampi(nConta, 3) <> "S" Then
                        cQuery = cQuery & ", " & aCampi(nConta, 1) & " = " & aCampi(nConta, 2)
                    End If
                End If
            Next
            cQuery = Right(cQuery, VB.Len(cQuery) - 2)
            cQuery = "UPDATE " & cTabella & " SET " & cQuery & " WHERE " & cWhere
            bRisposta = EseguiSQL(cQuery)
            Call ChiudiConnessione()
            Return bRisposta
        End If
        Call ChiudiConnessione()
        Return False
    End Function
    Public Sub CampiXInsertUpdate(oForm As Form, ByRef aCampi(,) As String)
        Dim nConta As Integer = 0
        Dim nCampiTotali As Integer = 0
        Dim oCheck As New System.Windows.Forms.CheckBox
        Dim oCombo As New System.Windows.Forms.ComboBox
        Dim aValori(20) As String
        Try
            For Each oControl As Control In oForm.Controls
                Select Case TypeName(oControl).ToUpper
                    Case "TEXTBOX"
                        If Left(oControl.Tag, 2) = "*|" Or Left(oControl.Tag, 2) = "!|" Then
                            nCampiTotali = nCampiTotali + 1
                            aValori = Split(oControl.Tag.ToString, "|")
                            aCampi(nCampiTotali, 0) = aValori(1)
                            aCampi(nCampiTotali, 1) = aValori(2)
                            Select Case aValori(3).ToUpper
                                Case "C"
                                    aCampi(nCampiTotali, 2) = "'" & VB.Left(ABCA_Util.PulisciStringa(oControl.Text), Val(aValori(4))) & "'"
                                Case "N"
                                    If oControl.Text.ToString < " " Then
                                        aCampi(nCampiTotali, 2) = "0"
                                    Else
                                        aCampi(nCampiTotali, 2) = VB.Replace(ABCA_Util.PulisciStringa(oControl.Text), ".", "")
                                        aCampi(nCampiTotali, 2) = VB.Replace(aCampi(nCampiTotali, 2), ",", ".")
                                    End If
                                Case Else
                            End Select
                            If Left(oControl.Tag, 2) = "!|" Then
                                aCampi(nCampiTotali, 3) = "S"
                            Else
                                aCampi(nCampiTotali, 3) = "N"
                            End If
                        End If
                    Case "COMBOBOX"
                        If Left(oControl.Tag, 2) = "*|" Or Left(oControl.Tag, 2) = "!|" Then
                            nCampiTotali = nCampiTotali + 1
                            oCombo = oControl
                            aValori = Split(oControl.Tag.ToString, "|")
                            aCampi(nCampiTotali, 0) = aValori(1)
                            aCampi(nCampiTotali, 1) = aValori(2)
                            aCampi(nCampiTotali, 2) = "" & oCombo.SelectedIndex
                            If Left(oControl.Tag, 2) = "!|" Then
                                aCampi(nCampiTotali, 3) = "S"
                            Else
                                aCampi(nCampiTotali, 3) = "N"
                            End If
                        End If
                    Case "CHECKBOX"
                        oCheck = oControl
                        If Left(oCheck.Tag, 2) = "*|" Or Left(oCheck.Tag, 2) = "!|" Then
                            nCampiTotali = nCampiTotali + 1
                            aValori = Split(oCheck.Tag.ToString, "|")
                            aCampi(nCampiTotali, 0) = aValori(1)
                            aCampi(nCampiTotali, 1) = aValori(2)
                            If oCheck.Checked = True Then
                                aCampi(nCampiTotali, 2) = "1"
                            Else
                                aCampi(nCampiTotali, 2) = "0"
                            End If
                            If Left(oCheck.Tag, 2) = "!|" Then
                                aCampi(nCampiTotali, 3) = "S"
                            Else
                                aCampi(nCampiTotali, 3) = "N"
                            End If
                        End If
                    Case "DATETIMEPICKER"
                        If Left(oControl.Tag, 2) = "*|" Or Left(oControl.Tag, 2) = "!|" Then
                            nCampiTotali = nCampiTotali + 1
                            aValori = Split(oControl.Tag.ToString, "|")
                            aCampi(nCampiTotali, 0) = aValori(1)
                            aCampi(nCampiTotali, 1) = aValori(2)
                            Select Case aValori(3).ToUpper
                                Case "D"
                                    aCampi(nCampiTotali, 2) = ABCA_Util.DataPerSQL(oControl.Text, False)
                                Case "DH"
                                    aCampi(nCampiTotali, 2) = ABCA_Util.DataPerSQL(oControl.Text, True)
                                Case Else
                            End Select
                            If Left(oControl.Tag, 2) = "!|" Then
                                aCampi(nCampiTotali, 3) = "S"
                            Else
                                aCampi(nCampiTotali, 3) = "N"
                            End If
                        End If
                    Case Else
                End Select
            Next
            aCampi(0, 0) = "" & nCampiTotali
        Catch ex As Exception
        End Try
    End Sub

    Public Sub AggiornaDatiForm(ByRef oForm As Form, cConnessione As String)
        Dim aCampi(200, 3) As String
        Call CampiXInsertUpdate(oForm, aCampi)
        Dim nContaMax As Integer = Val(aCampi(0, 0))
        Dim nConta As Integer = 0
        Dim aTabelle(20) As String
        Dim nTabelleMax As Integer = 1
        Dim nTabelle As Integer = 0
        Dim bTrovata As Boolean = False
        Dim cWhere As String = ""
        ' METTO LA PRIMA TABELLA NELL'ARRAY
        aTabelle(nTabelleMax) = aCampi(1, 0)
        For nConta = 2 To nContaMax
            ' IMPOSTO LA VARIABILE DI VERIFICA A FALSE
            bTrovata = False
            For nTabelle = 1 To nTabelleMax
                ' PASSO TUTTE LE TABELLE
                If aTabelle(nTabelle) = aCampi(nConta, 0) Then
                    ' SE LA TROVO ALLORA IMPORTO LA VARIABILE A TRUE
                    bTrovata = True
                    Exit For
                End If
            Next
            If bTrovata = False Then
                ' LA TABELLA NON E' STATA TROVATA E QUINDI LA AGGIUNGO
                nTabelleMax += 1
                aTabelle(nTabelleMax) = aCampi(nConta, 0)
            End If
        Next
        For nTabelle = 1 To nTabelleMax
            cWhere = ""
            For nConta = 1 To nContaMax
                If aCampi(nConta, 0) = aTabelle(nTabelle) Then
                    If aCampi(nConta, 3) = "S" Then
                        cWhere = cWhere & " AND " & aCampi(nConta, 1) & " = " & aCampi(nConta, 2)
                    End If
                End If
            Next
            If cWhere > " " Then
                cWhere = Right(cWhere, VB.Len(cWhere) - 4)
                Call AggiornaDatiFormTabella(oForm, aTabelle(nTabelle), cWhere, cConnessione)
            End If
        Next

    End Sub

    Private Sub AggiornaDatiFormTabella(ByRef oForm As Form, cTabella As String, cWhere As String, cConnessione As String)
        Dim cquery As String = ""
        Dim nConta As Integer = 0
        Dim aValori(20) As String
        Dim oCheck As New System.Windows.Forms.CheckBox
        Dim oCombo As New System.Windows.Forms.ComboBox
        If ApriConnessione("", "", "", "", cConnessione) = True Then
            cquery = "SELECT TOP 1 * FROM " & cTabella & " WHERE " & cWhere
            If CaricaRecords(cquery) = True Then
                Try
                    For Each oControl As Control In oForm.Controls
                        If Left(oControl.Tag, 2) = "*|" Or Left(oControl.Tag, 2) = "#|" Then
                            aValori = Split(oControl.Tag.ToString, "|")
                            If aValori(1).ToUpper = cTabella.ToUpper Then
                                Select Case TypeName(oControl).ToUpper
                                    Case "TEXTBOX"
                                        oControl.Text = RestituisciValore(aValori(2), 1)
                                    Case "COMBOBOX"
                                        oCombo = oControl
                                        oCombo.SelectedIndex = RestituisciValore(aValori(2), 1)
                                    Case "DATETIMEPICKER"
                                        oControl.Text = RestituisciValore(aValori(2), 1)
                                    Case "CHECKBOX"
                                        oCheck = oControl
                                        If RestituisciValore(aValori(2), 1) = 0 Then
                                            oCheck.Checked = False
                                        Else
                                            oCheck.Checked = True
                                        End If
                                End Select
                            End If
                        End If
                    Next
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub
    Public Sub CaricaImpostazioniControlliForm(oForm As Form)
        Try
            Dim aLista(20, 1) As String
            Dim nAttributi As Integer = 0
            Dim cAppoggio As String = ""

            Call ABCA_Util.xmlLeggiElemento("MAIN", "FORM", aLista, nAttributi)
            Dim aRecord() As String
            aRecord = Split(ABCA_Util.ValoreDaLista("TopLeftHeightWidth", aLista, nAttributi, ""), "|")
            oForm.Top = aRecord(0)
            oForm.Left = aRecord(1)
            oForm.Height = aRecord(2)
            oForm.Width = aRecord(3)
            oForm.Text = ABCA_Util.ValoreDaLista("TITOLO", aLista, nAttributi, "")
            oForm.Tag = ABCA_Util.ValoreDaLista("MANUTENZIONE", aLista, nAttributi, "")
            For Each oControl As Control In oForm.Controls
                '            aLista(0, 0) = "ColonneDimensione"
                '            aLista(1, 0) = "ColonneNome"
                System.Windows.Forms.Application.DoEvents()
                If IsNothing(oControl.AccessibleDescription) = False And oControl.AccessibleDescription > " " Then
                    cAppoggio = oControl.AccessibleDescription.ToUpper
                Else
                    cAppoggio = oControl.Name.ToUpper
                End If
                If ABCA_Util.xmlLeggiElemento(oForm.Name & "_CONTROLS", cAppoggio, aLista, nAttributi) = True Then
                    Dim aValori() As String = Split(ABCA_Util.ValoreDaLista("POSIZIONE", aLista, nAttributi), "|")
                    If aValori.Count > 1 Then
                        oControl.Location = New System.Drawing.Point(Val(aValori(0)), Val(aValori(1)))
                        oControl.Height = Val(aValori(2))
                        oControl.Width = Val(aValori(3))
                    End If
                    If oControl.AccessibleName = "TXT_RICERCA" Then
                        MettiValoreProprietaPersonalizzata(oControl, "ZZRICERCA", ABCA_Util.ValoreDaLista("ZZRICERCA", aLista, nAttributi, ""))
                        MettiValoreProprietaPersonalizzata(oControl, "ZZTXT_NOMI", ABCA_Util.ValoreDaLista("ZZTXT_NOMI", aLista, nAttributi, ""))
                        MettiValoreProprietaPersonalizzata(oControl, "ZZLBL_NOMI", ABCA_Util.ValoreDaLista("ZZLBL_NOMI", aLista, nAttributi, ""))
                    End If
                    oControl.Tag = ABCA_Util.ValoreDaLista("TAGS", aLista, nAttributi, "")
                    oControl.TabIndex = ABCA_Util.ValoreDaLista("TABINDEX", aLista, nAttributi, "1")
                    If ABCA_Util.ValoreDaLista("TEXT", aLista, nAttributi, "") > "" Then
                        oControl.Text = ABCA_Util.ValoreDaLista("TEXT", aLista, nAttributi, "")
                    End If

                End If
            Next
        Catch ex As Exception
            '            MsgBox("ERRORE")
        End Try
    End Sub
    Public Sub aaaCaricaImpostazioniControlliForm(oForm As Form)
        Try
            Dim aLista(20, 1) As String
            Dim nAttributi As Integer

            Call ABCA_Util.xmlLeggiElemento("MAIN", "FORM", aLista, nAttributi)
            Dim aRecord() As String
            aRecord = Split(ABCA_Util.ValoreDaLista("TopLeftHeightWidth", aLista, nAttributi, ""), "|")
            oForm.Top = aRecord(0)
            oForm.Left = aRecord(1)
            oForm.Height = aRecord(2)
            oForm.Width = aRecord(3)
            oForm.Text = ABCA_Util.ValoreDaLista("TITOLO", aLista, nAttributi, "")
            oForm.Tag = ABCA_Util.ValoreDaLista("MANUTENZIONE", aLista, nAttributi, "")
            For Each oControl As Control In oForm.Controls
                '            aLista(0, 0) = "ColonneDimensione"
                '            aLista(1, 0) = "ColonneNome"
                System.Windows.Forms.Application.DoEvents()
                If ABCA_Util.xmlLeggiElemento(oForm.Name & "_CONTROLS", oControl.Name.ToUpper, aLista, nAttributi) = True Then
                    Dim aValori() As String = Split(ABCA_Util.ValoreDaLista("POSIZIONE", aLista, nAttributi), "|")
                    If aValori.Count > 1 Then
                        oControl.Location = New System.Drawing.Point(Val(aValori(0)), Val(aValori(1)))
                        oControl.Height = Val(aValori(2))
                        oControl.Width = Val(aValori(3))
                    End If
                    oControl.Tag = ABCA_Util.ValoreDaLista("TAGS", aLista, nAttributi, "")
                    oControl.TabIndex = ABCA_Util.ValoreDaLista("TABINDEX", aLista, nAttributi, "1")
                    If ABCA_Util.ValoreDaLista("TEXT", aLista, nAttributi, "") > "" Then
                        oControl.Text = ABCA_Util.ValoreDaLista("TEXT", aLista, nAttributi, "")
                    End If
                    If TypeName(oControl).ToUpper = "TXT_RICERCA" Then
                        MettiValoreProprietaPersonalizzata(oControl, "ZZRICERCA", ABCA_Util.ValoreDaLista("ZZRICERCA", aLista, nAttributi, ""))
                        MettiValoreProprietaPersonalizzata(oControl, "ZZTXT_NOMI", ABCA_Util.ValoreDaLista("ZZTXT_NOMI", aLista, nAttributi, ""))
                        MettiValoreProprietaPersonalizzata(oControl, "ZZLBL_NOMI", ABCA_Util.ValoreDaLista("ZZLBL_NOMI", aLista, nAttributi, ""))
                    End If

                End If
            Next
        Catch ex As Exception
        End Try
    End Sub
    Public Sub SalvaImpostazioniControlliForm(oForm As Form)
        Try
            Dim aLista(20, 1) As String
            aLista(0, 0) = "TOPLEFTHEIGHTWIDTH"
            aLista(0, 1) = oForm.Location.X.ToString & "|" & oForm.Location.Y.ToString _
                            & "|" & oForm.Height.ToString & "|" & oForm.Width.ToString
            Call ABCA_Util.xmlSalvaElemento("MAIN", "FORM", aLista, 1)

            If oForm.Tag = "S" Then
                For Each oControl As Control In oForm.Controls
                    '            aLista(0, 0) = "ColonneDimensione"
                    '            aLista(1, 0) = "ColonneNome"
                    System.Windows.Forms.Application.DoEvents()
                    aLista(0, 0) = "POSIZIONE"
                    aLista(0, 1) = oControl.Location.X.ToString & "|" & oControl.Location.Y.ToString _
                                    & "|" & oControl.Height.ToString & "|" & oControl.Width.ToString
                    aLista(1, 0) = "TAGS"
                    If IsNothing(oControl.Tag) = True Then
                        aLista(1, 1) = ""
                    Else
                        aLista(1, 1) = oControl.Tag.ToString
                    End If
                    aLista(2, 0) = "TEXT"
                    If oControl.AccessibleName.ToUpper = "LABEL" Or oControl.AccessibleName.ToUpper = "CHECKBOX" Then
                        aLista(2, 1) = oControl.Text
                    Else
                        aLista(2, 1) = ""
                    End If
                    aLista(3, 0) = "TABINDEX"
                    aLista(3, 1) = "" & oControl.TabIndex
                    If oControl.AccessibleName.ToUpper <> "TXT_RICERCA" Then
                        Call ABCA_Util.xmlSalvaElemento(oForm.Name & "_CONTROLS", oControl.AccessibleDescription.ToUpper, aLista, 4)
                    Else
                        Dim oControlR As New Txt_Ricerca
                        oControlR = oControl
                        aLista(4, 0) = "ZZRICERCA"
                        aLista(4, 1) = DammiValoreProprietaPersonalizzata(oControlR, "ZZRICERCA")
                        aLista(5, 0) = "ZZTXT_NOMI"
                        aLista(5, 1) = DammiValoreProprietaPersonalizzata(oControlR, "ZZTXT_NOMI")
                        aLista(6, 0) = "ZZLBL_NOMI"
                        aLista(6, 1) = DammiValoreProprietaPersonalizzata(oControlR, "ZZLBL_NOMI")
                        Call ABCA_Util.xmlSalvaElemento(oForm.Name & "_CONTROLS", oControl.AccessibleDescription.ToUpper, aLista, 7)
                        oControlR = Nothing
                    End If
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function DammiValoreProprietaPersonalizzata(oControl As Object, cNomeProprieta As String) As String
        Return oControl.ValoriCampiDammi(cNomeProprieta)
    End Function

    Private Sub MettiValoreProprietaPersonalizzata(oControl As Txt_Ricerca, cNomeProprieta As String, cValori As String)
        Try
            '      Dim oControlMio As New Txt_Ricerca
            '      oControlMio = oControl
            Call oControl.ValoriCampiMetti(cNomeProprieta, cValori)
        Catch ex As Exception
        End Try
    End Sub

End Class
