Imports System.Data.SqlClient
Imports VB = Microsoft.VisualBasic

Public Class clsABCA_DB
    Public cStrigaDiConnessione As String
    Dim oCnn As SqlConnection
    Dim oReader As SqlDataReader
    Dim oCmd As SqlCommand
    Dim oDataset As New DataSet
    ' caratteristiche dei campi e numero dei campi per tabella (vengono popolati con caricarecordstop
    Dim aCampi(20, 500, 6) As String
    Dim nCampi(20) As Integer
    ' qui vengono salvati il numero dei campi e dei records del datatable
    Dim nNrCampiDataTable(20) As Integer
    Dim nNrRecordsDataTable(20) As Integer

    Public Enum enmTipoData
        Data = 1
        DataOra = 2
    End Enum

    Public Function ConnessioneAttiva() As Boolean
        Try
            If oCnn.State = ConnectionState.Open Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function nNrCampi(cTabella As String, Optional ByRef nIndiceTabella As Integer = 9999) As Integer
        If nIndiceTabella = 9999 Then
            nIndiceTabella = nNrTabella(cTabella)
        End If
        Try
            Return nNrCampiDataTable(nIndiceTabella)
        Catch ex As Exception
            Return -1
        End Try
    End Function
    Public Function nNrRecords(cTabella As String, Optional ByRef nIndiceTabella As Integer = 9999) As Integer
        If nIndiceTabella = 9999 Then
            nIndiceTabella = nNrTabella(cTabella)
        End If
        Try
            Return nNrRecordsDataTable(nIndiceTabella)
        Catch ex As Exception
            Return -1
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
            If IsNothing(oCmd) = True Then
            Else
                oCmd.Dispose()
            End If
            oCnn.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function CaricaRecordsTop(cQuery As String, cTabella As String) As Boolean
        ' funzione che consente di caricare i nomi campi e  le caratteristiche
        '        Dim nIndiceTabella As Integer = nNrTabella(cTabella)
        Try
            oCmd = New SqlCommand(cQuery, oCnn)
            If IsNothing(oReader) = True Then
            Else
                If oReader.IsClosed = False Then
                    Call oReader.Close()
                End If
            End If
            oReader = oCmd.ExecuteReader()
            Call CaricaNomi(cTabella)
            Call oReader.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function CaricaNomi(cTabella As String, Optional ByRef nIndiceTabella As Integer = 9999) As Boolean
        ' *************************************************************
        ' carico i nomi dei campi, le caratteristiche dei campi e
        ' altre informazioni
        ' *************************************************************

        If nIndiceTabella = 9999 Then
            nIndiceTabella = nNrTabella(cTabella)
        End If
        If nIndiceTabella = -1 Then
            nIndiceTabella = 20
        End If
        Dim oSchemaTabella As System.Data.DataTable
        nCampi(nIndiceTabella) = 0
        oSchemaTabella = oReader.GetSchemaTable
        For Each myField As DataRow In oSchemaTabella.Rows
            nCampi(nIndiceTabella) += 1
            For Each myProperty As DataColumn In oSchemaTabella.Columns
                Select Case myProperty.ColumnName.ToUpper
                    Case "COLUMNNAME"
                        aCampi(nIndiceTabella, nCampi(nIndiceTabella), 1) = myField(myProperty).ToString()
                    Case "DATATYPENAME"
                        aCampi(nIndiceTabella, nCampi(nIndiceTabella), 2) = myField(myProperty).ToString()
                        Select Case aCampi(nIndiceTabella, nCampi(nIndiceTabella), 2).ToUpper
                            Case "CHAR", "NCHAR", "NTEXT", "NVARCHAR", "TEXT", "VARCHAR"
                                aCampi(nIndiceTabella, nCampi(nIndiceTabella), 6) = "C"
                            Case "DATE"
                                aCampi(nIndiceTabella, nCampi(nIndiceTabella), 6) = "D"
                            Case "DATETIME", "SMALLDATETIME"
                                aCampi(nIndiceTabella, nCampi(nIndiceTabella), 6) = "DH"
                            Case Else
                                aCampi(nIndiceTabella, nCampi(nIndiceTabella), 6) = "N"
                        End Select
                    Case "COLUMNSIZE"
                        aCampi(nIndiceTabella, nCampi(nIndiceTabella), 3) = myField(myProperty).ToString()
                    Case "ALLOWDBNULL"
                        aCampi(nIndiceTabella, nCampi(nIndiceTabella), 4) = myField(myProperty).ToString()
                    Case "COLUMNORDINAL"
                        aCampi(nIndiceTabella, nCampi(nIndiceTabella), 5) = myField(myProperty).ToString()
                End Select
            Next
        Next
        Return True
    End Function

    Public Function NomeCampo(cTabella As String, nNrCampo As Integer, Optional ByRef nIndiceTabella As Integer = 9999) As String
        If nIndiceTabella = 9999 Then
            nIndiceTabella = nNrTabella(cTabella)
        End If
        If nIndiceTabella = -1 Then
            nIndiceTabella = 20
        End If
        Try
            Return aCampi(nIndiceTabella, nNrCampo, 1)
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function nNrTabella(cTabella As String) As Integer
        Dim nNrTabellaTrovato As Integer = -1
        Try
            For nIndice As Integer = 0 To oDataset.Tables.Count - 1
                If oDataset.Tables(nIndice).TableName.ToUpper = cTabella.ToUpper Then
                    nNrTabellaTrovato = nIndice
                End If
            Next
        Catch ex As Exception
        End Try
        Return nNrTabellaTrovato
    End Function

    Public Function CaricaRecords(cQuery As String, cNomeTabellaDS As String, Optional ByRef oDS As DataSet = Nothing, Optional bCaricaNomi As Boolean = False) As Boolean
        Dim bRisposta As Boolean = False
        Try
            Dim oAdapter As New SqlDataAdapter
            oAdapter.SelectCommand = New SqlCommand(cQuery, oCnn)
            Dim nTabellaIndice As Integer = nNrTabella(cNomeTabellaDS)
            If nTabellaIndice > -1 Then
                Call oDataset.Tables(nTabellaIndice).Clear()
            End If
            If IsNothing(oDS) = True Then
                oAdapter.Fill(oDataset, cNomeTabellaDS)
                nTabellaIndice = nNrTabella(cNomeTabellaDS)
                nNrCampiDataTable(nTabellaIndice) = oDataset.Tables(cNomeTabellaDS).Columns.Count
                nNrRecordsDataTable(nTabellaIndice) = oDataset.Tables(cNomeTabellaDS).Rows.Count
                If bCaricaNomi = True Then
                    Call CaricaRecordsTop(cQuery, cNomeTabellaDS)
                End If
                If nNrRecordsDataTable(nTabellaIndice) > 0 Then
                    bRisposta = True
                Else
                    bRisposta = False
                End If
            Else
                oAdapter.Fill(oDS, cNomeTabellaDS)
                bRisposta = True
            End If
        Catch ex As Exception
        End Try
        Return bRisposta
    End Function

    Public Function RestituisciValore(cNomecampo As String, nRecord As Integer, cTabella As String, Optional ByRef nIndiceTabella As Integer = 9999, Optional ByRef bNull As Boolean = False) As String
        Try
            If nIndiceTabella = 9999 Then
                nIndiceTabella = nNrTabella(cTabella)
            End If
            If nRecord > nNrRecords(cTabella, nIndiceTabella) Then
                Return ""
            End If
            If oDataset.Tables(nIndiceTabella).Rows(nRecord - 1).Item(cNomecampo.ToUpper) Is DBNull.Value Then
                bNull = True
            End If
            Return "" & oDataset.Tables(nIndiceTabella).Rows(nRecord - 1).Item(cNomecampo.ToUpper).ToString
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function PosizioneCampo(cNomeCampo As String, cTabella As String, Optional ByRef nIndiceTabella As Integer = 9999) As Integer
        ' restituisce la posizione del campo
        Dim nPosizione As Integer
        cNomeCampo = cNomeCampo.ToUpper
        If nIndiceTabella = 9999 Then
            nIndiceTabella = nNrTabella(cTabella)
        End If
        If nIndiceTabella > -1 Then
            For nPosizione = 1 To nCampi(nIndiceTabella)
                If aCampi(nIndiceTabella, nPosizione, 1) = cNomeCampo Then
                    Return nPosizione
                End If
            Next
            Return 0
        Else
            Return 0
        End If
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

    Public Function SalvaRecordsSuArray(ByRef aCampi(,) As String) As Boolean
        Dim nContaMax As Integer = VB.Val(aCampi(0, 0))
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
        If CaricaRecords(cQuery, cTabella) = False Then
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
    Public Function DuplicaRecord(cQuery As String, cTabellaInsert As String, cNomeCampoCodice As String, cCodiceNuovo As String, ByVal cSiglaTabellaMemoria As String) As Boolean
        Try
            Dim nConta As Integer = 0
            Dim cQueryInsert As String = ""
            Dim cQueryValues As String = ""
            Dim cIstruzioneInsert As String = ""
            Dim nNrTabellaInIndice As Integer = 0
            Dim bDBNull As Boolean = False
            Dim cAppoggio As String = ""
            Dim cTabellaMemoria As String = cTabellaInsert & cSiglaTabellaMemoria

            Call CaricaRecords(cQuery, cTabellaMemoria, , True)
            nNrTabellaInIndice = nNrTabella(cTabellaMemoria)
            For nConta = 1 To nNrCampi(cTabellaMemoria, nNrTabellaInIndice)
                If aCampi(nNrTabellaInIndice, nConta, 2) = "timestamp" Then
                Else
                    If cQueryInsert > " " Then
                        cQueryInsert &= ", "
                        cQueryValues &= ", "
                    End If
                    cQueryInsert &= aCampi(nNrTabellaInIndice, nConta, 1)
                    If aCampi(nNrTabellaInIndice, nConta, 1).ToUpper = cNomeCampoCodice.ToUpper Then
                        Select Case aCampi(nNrTabellaInIndice, nConta, 6)
                            Case "C"
                                cQueryValues &= ABCA_Util.SQL_Stringa(cCodiceNuovo)
                            Case "D"
                                cQueryValues &= ABCA_Util.DataPerSQL(cCodiceNuovo)
                            Case "DH"
                                cQueryValues &= ABCA_Util.DataPerSQL(cCodiceNuovo, True)
                            Case Else
                                cQueryValues &= ABCA_Util.SQL_Num(cCodiceNuovo)
                        End Select
                    Else
                        cAppoggio = RestituisciValore("" & aCampi(nNrTabellaInIndice, nConta, 1), 1, "", nNrTabellaInIndice, bDBNull)
                        If bDBNull = True Then
                            cQueryValues &= "NULL"
                            bDBNull = False
                        Else
                            Select Case aCampi(nNrTabellaInIndice, nConta, 6)
                                Case "C"
                                    cQueryValues &= ABCA_Util.SQL_Stringa(cAppoggio, -1, False)
                                Case "D"
                                    cQueryValues &= ABCA_Util.DataPerSQL(cAppoggio)
                                Case "DH"
                                    cQueryValues &= ABCA_Util.DataPerSQL(cAppoggio, True)
                                Case Else
                                    cQueryValues &= ABCA_Util.SQL_Num(cAppoggio)
                            End Select
                        End If
                    End If
                End If
            Next
            cIstruzioneInsert = "INSERT INTO " & cTabellaInsert & "(" & cQueryInsert & ") VALUES (" & cQueryValues & ")"
            Return EseguiSQL(cIstruzioneInsert)
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
