Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

'******************************************************
' Contenuto di aCampi:
' aCampi(100,0) = TipoDB
' aCampi(100,1) = Numero campi della tabella
' aCampi(101,0) = Titolo Form
' aCampi(102,0) = Top della form
' aCampi(103,0) = Height form
' aCampi(103,1) = Width form

Public Class cls_ABCAGriglia

    Public Enum EnmTipoDB
        MSSQLServer = 1
        MySql = 2
        Access = 3
    End Enum

    Public Enum tyFormato
        Numero = 1
        ' NO -N9 il numero corrispode ai decimali
        Stringa = 2
        ' S
        Data = 3
        ' D
        DataOra = 4
        ' DH
        DataCorta = 5
        ' DC
        CheckBox = 6
        ' CB
        Button = 7
        ' B
        NrSenzaSeparatori = 8
        ' IN
    End Enum
    Public Enum tyAllineamento
        Nessuno = 0
        Sinistra = 16
        Centro = 32
        Destra = 64
    End Enum
    Public cStringaConnessione As String = ""

    '    Public cFiltroWhereObbligatorio As String = ""
    Private Function QualeAllineamentoStringa(nValore As Integer) As String
        Select Case nValore
            Case 32
                Return "C"
            Case 64
                Return "D"
            Case Else
                Return "S"
        End Select
    End Function
    Private Function QualeAllineamentoNumero(cValore As String) As Integer
        Select Case cValore
            Case "C"
                Return 32
            Case "D"
                Return 64
            Case Else
                Return 16
        End Select
    End Function
    Public Sub GrigliaSalva(ByRef oDGV As DataGridView, cNomeTabella As String)
        Try
            Dim cValori As String = ""
            Dim cAllineamento As String = ""
            Dim cDimensione As String = ""
            Dim cTestoColonne As String = ""
            Dim cTipoCampo As String = ""
            Dim cSolaLettura As String = ""
            Dim nConta As Integer = 0
            If oDGV.rows.Count < 1 Then
                Exit Sub
            End If
            ' *************************************************************************************
            ' SALVO LE COLONNE
            ' *************************************************************************************
            For nConta = 0 To oDGV.columns.Count - 1
                If nConta > 0 Then
                    cDimensione = cDimensione + "|"
                    cTestoColonne = cTestoColonne + "|"
                    cAllineamento = cAllineamento + "|"
                    cTipoCampo = cTipoCampo + "|"
                    cSolaLettura = cSolaLettura + "|"
                End If
                If oDGV.columns(nConta).visible = True Then
                    cDimensione = cDimensione + oDGV.columns(nConta).width.ToString
                Else
                    cDimensione = cDimensione + "0"
                End If
                cTestoColonne = cTestoColonne + oDGV.Columns(nConta).HeaderText()
                cAllineamento = cAllineamento + QualeAllineamentoStringa(oDGV.Columns(nConta).DefaultCellStyle.Alignment())
                cTipoCampo = cTipoCampo + oDGV.Columns(nConta).Tag
                If oDGV.columns(nConta).ReadOnly = True Then
                    cSolaLettura = cSolaLettura + "V"
                Else
                    cSolaLettura = cSolaLettura + "F"
                End If
            Next
            Dim aLista(20, 1) As String
            aLista(0, 0) = "Valori"
            aLista(0, 1) = cDimensione
            Call ABCA_Util.xmlSalvaElemento(cNomeTabella, "COLONNE_WIDTH", aLista, 1)
            aLista(0, 0) = "Valori"
            aLista(0, 1) = cTestoColonne
            Call ABCA_Util.xmlSalvaElemento(cNomeTabella, "COLONNE_NOME", aLista, 1)
            aLista(0, 0) = "Valori"
            aLista(0, 1) = cAllineamento
            Call ABCA_Util.xmlSalvaElemento(cNomeTabella, "COLONNE_ALLINEAMENTO", aLista, 1)
            aLista(0, 0) = "Valori"
            aLista(0, 1) = cTipoCampo
            Call ABCA_Util.xmlSalvaElemento(cNomeTabella, "COLONNE_TIPOCAMPO", aLista, 1)
            aLista(0, 0) = "Valori"
            aLista(0, 1) = cSolaLettura
            Call ABCA_Util.xmlSalvaElemento(cNomeTabella, "COLONNE_SOLALETTURA", aLista, 1)
            'oDGV.Dispose()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub GrigliaCarica(ByRef oDGV As DataGridView, cNomeTabella As String)
        oDGV.RowHeadersWidth = 24
        Try
            Dim nAttributi As Integer = 0
            Dim nFormato As Integer = 0
            Dim nDecimali As Integer = 0
            Dim aLista(20, 1) As String
            '            aLista(0, 0) = "ColonneDimensione"
            '            aLista(1, 0) = "ColonneNome"
            If ABCA_Util.xmlLeggiElemento(cNomeTabella, "COLONNE_TIPOCAMPO", aLista, nAttributi) = True Then
                Dim aNomi() As String = Split(ABCA_Util.ValoreDaLista("VALORI", aLista, nAttributi), "|")
                For nConta = 0 To aNomi.Count - 1
                    If aNomi(nConta) > " " Then
                        Call ConvertiFormato(aNomi(nConta), nFormato, nDecimali)
                        Call FormatColonna(oDGV, nConta, nFormato, nDecimali)
                        oDGV.Columns(nConta).TAG = aNomi(nConta)
                    End If
                Next
            End If
            If ABCA_Util.xmlLeggiElemento(cNomeTabella, "COLONNE_ALLINEAMENTO", aLista, nAttributi) = True Then
                Dim aNomi() As String = Split(ABCA_Util.ValoreDaLista("VALORI", aLista, nAttributi), "|")
                For nConta = 0 To aNomi.Count - 1
                    If aNomi(nConta) > " " Then
                        oDGV.Columns(nConta).DefaultCellStyle.Alignment = QualeAllineamentoNumero("" & aNomi(nConta))
                    End If
                Next
            End If
            If ABCA_Util.xmlLeggiElemento(cNomeTabella, "COLONNE_NOME", aLista, nAttributi) = True Then
                Dim aNomi() As String = Split(ABCA_Util.ValoreDaLista("VALORI", aLista, nAttributi), "|")
                For nConta = 0 To aNomi.Count - 1
                    If aNomi(nConta) > " " Then
                        oDGV.Columns(nConta).HeaderText = aNomi(nConta)
                    End If
                Next
            End If
            If ABCA_Util.xmlLeggiElemento(cNomeTabella, "COLONNE_WIDTH", aLista, nAttributi) = True Then
                Dim aDimensioni() As String = Split(ABCA_Util.ValoreDaLista("VALORI", aLista, nAttributi), "|")
                Dim nConta As Integer = 0
                For nConta = 0 To aDimensioni.Count - 1
                    If Val(aDimensioni(nConta)) = 0 Then
                        oDGV.Columns(nConta).Visible = False
                    Else
                        oDGV.Columns(nConta).Visible = True
                        oDGV.Columns(nConta).Width = Val(aDimensioni(nConta))
                    End If
                Next
            End If
            If ABCA_Util.xmlLeggiElemento(cNomeTabella, "COLONNE_SOLALETTURA", aLista, nAttributi) = True Then
                Dim aDimensioni() As String = Split(ABCA_Util.ValoreDaLista("VALORI", aLista, nAttributi), "|")
                Dim nConta As Integer = 0
                For nConta = 0 To aDimensioni.Count - 1
                    If aDimensioni(nConta) = "F" Then
                        oDGV.Columns(nConta).ReadOnly = False
                    Else
                        oDGV.Columns(nConta).ReadOnly = True
                        oDGV.Columns(nConta).DefaultCellStyle.BackColor = Color.Gainsboro
                    End If
                Next
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub ConvertiFormato(cFormato As String, ByRef nFormato As Integer, ByRef nDecimali As Integer)
        Try
            nDecimali = 0
            Select Case cFormato
                Case "S"
                    nFormato = 2
                Case "D"
                    nFormato = 3
                Case "DC"
                    nFormato = 5
                Case "DH"
                    nFormato = 4
                Case "CB"
                    nFormato = 6
                Case "B"
                    nFormato = 7
                Case "INT"
                    nFormato = 8
                Case Else
                    nFormato = 1
                    nDecimali = Microsoft.VisualBasic.Val(Microsoft.VisualBasic.Right(cFormato, Microsoft.VisualBasic.Len(cFormato) - 1))
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Public Function QueryCreaFiltro(aCampi(,) As String, cRichiestafiltro As String) As String
        Dim nConta As Integer = 0
        Dim cQuery As String = ""
        Dim bTogliOR As Boolean = False
        Try

            For nConta = 0 To Val(aCampi(100, 1))
                If aCampi(nConta, 1) > " " Then
                    Select Case aCampi(nConta, 1)
                        Case 0
                            If aCampi(100, 0) = "MSSQLSERVER" Then
                                cQuery = cQuery & "CONVERT(VARCHAR(10)," & aCampi(nConta, 0) & ", 103) LIKE '%" & cRichiestafiltro & "%' OR "
                                bTogliOR = True
                            ElseIf aCampi(100, 0) = "ACCESS" Then
                                cQuery = cQuery & aCampi(nConta, 0) & " LIKE '%" & cRichiestafiltro & "%' OR "
                                bTogliOR = True
                            ElseIf aCampi(100, 0) = "MYSQL" Then
                                cQuery = cQuery & aCampi(nConta, 0) & " LIKE '%" & cRichiestafiltro & "%' OR "
                                bTogliOR = True
                            End If
                        Case "N0", "N1", "N2", "N3", "N4", "N5", "N6", "N7", "N8", "N9"
                            If aCampi(100, 0) = "MSSQLSERVER" Then
                                cQuery = cQuery & "REPLACE(CONVERT(VARCHAR(30)," & aCampi(nConta, 0) & "), '.',',') LIKE '%" & cRichiestafiltro & "%' OR "
                                bTogliOR = True
                            ElseIf aCampi(100, 0) = "ACCESS" Then
                                cQuery = cQuery & aCampi(nConta, 0) & " LIKE '%" & cRichiestafiltro & "%' OR "
                                bTogliOR = True
                            ElseIf aCampi(100, 0) = "MYSQL" Then
                                cQuery = cQuery & aCampi(nConta, 0) & " LIKE '%" & cRichiestafiltro & "%' OR "
                                bTogliOR = True
                            End If
                        Case "S"
                            If aCampi(100, 0) = "MSSQLSERVER" Then
                                cQuery = cQuery & aCampi(nConta, 0) & " LIKE '%" & cRichiestafiltro & "%' OR "
                                bTogliOR = True
                            ElseIf aCampi(100, 0) = "ACCESS" Then
                                cQuery = cQuery & aCampi(nConta, 0) & " LIKE '%" & cRichiestafiltro & "%' OR "
                                bTogliOR = True
                            ElseIf aCampi(100, 0) = "MYSQL" Then
                                cQuery = cQuery & aCampi(nConta, 0) & " LIKE '%" & cRichiestafiltro & "%' OR "
                                bTogliOR = True
                            End If
                    End Select

                End If
            Next
            If bTogliOR = True Then
                If bTogliOR = True Then
                    cQuery = Microsoft.VisualBasic.Left(cQuery, Len(cQuery) - 3)
                End If
            End If
            cQuery = "(" + cQuery + ")"
        Catch ex As Exception
            cQuery = ""
        End Try
        Return cQuery
    End Function
    Public Sub FormDimensioneSalva(cNomeTabella As String, cTop As String, cLeft As String, cHeight As String, cWidth As String)
        ' vado a dimensionare le colonne
        Try
            Dim cDimensioni As String = ""
            cDimensioni = cTop + "|" + cLeft + "|" + cHeight + "|" + cWidth
            Dim aLista(20, 1) As String
            aLista(0, 0) = "TopLeftHeightWidth"
            aLista(0, 1) = cDimensioni
            Call ABCA_Util.xmlSalvaElemento(cNomeTabella, "FORM", aLista, 1)
        Catch ex As Exception
        End Try
    End Sub
    Public Sub FormDimensioniCarica(cNomeTabella As String, oForm As Object)
        Try
            Dim aLista(20, 1) As String
            Dim nAttributi As Integer = 0
            Call ABCA_Util.xmlLeggiElemento(cNomeTabella, "FORM", aLista, nAttributi)

            Dim aRecord() As String
            aRecord = Split(ABCA_Util.ValoreDaLista("TopLeftHeightWidth", aLista, nAttributi, ""), "|")
            oForm.top = aRecord(0)
            oForm.Left = aRecord(1)
            oForm.height = aRecord(2)
            oForm.width = aRecord(3)
        Catch ex As Exception
        End Try
    End Sub
    Public Sub ColonneDimensioneSalva(ByRef oDGV As DataGridView, cNomeTabella As String)
        Try
            Dim cValori As String = ""
            Dim nConta As Integer = 0
            ' *************************************************************************************
            ' SALVO LE DIMENSIONI DELLE COLONNE
            ' *************************************************************************************
            For nConta = 0 To oDGV.columns.Count - 1
                If nConta > 0 Then
                    cValori = cValori + "|"
                End If
                If oDGV.columns(nConta).visible = True Then
                    cValori = cValori + oDGV.columns(nConta).width.ToString
                Else
                    cValori = cValori + "0"
                End If
            Next
            If cValori = "" Then
                Exit Sub
            End If
            Dim aLista(20, 1) As String
            aLista(0, 0) = "COLONNEDIMENSIONE"
            aLista(0, 1) = cValori
            Call ABCA_Util.xmlSalvaElemento(cNomeTabella, "FORM", aLista, 1)
            ' *************************************************************************************
            ' Salvo il Nome Colonna
            ' *************************************************************************************
            cValori = ""
            For nConta = 0 To oDGV.Columns.Count - 1
                If nConta > 0 Then
                    cValori = cValori + "|"
                End If
                cValori = cValori + oDGV.Columns(nConta).HeaderText()
            Next
            aLista(0, 0) = "COLONNENOME"
            aLista(0, 1) = cValori
            Call ABCA_Util.xmlSalvaElemento(cNomeTabella, "FORM", aLista, 1)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub OLDColonneDimensioneSalva(ByRef oDGV As DataGridView, cNomeTabella As String)
        ' vado a dimensionare le colonne
        Try
            Dim cDimensioni As String = ""
            Dim nConta As Integer = 0
            ' *************************************************************************************
            ' SALVO LE DIMENSIONI DELLE COLONNE
            ' *************************************************************************************
            For nConta = 0 To oDGV.columns.Count - 1
                If nConta > 0 Then
                    cDimensioni = cDimensioni + "|"
                End If
                If oDGV.columns(nConta).visible = True Then
                    cDimensioni = cDimensioni + oDGV.columns(nConta).width.ToString
                Else
                    cDimensioni = cDimensioni + "0"
                End If
            Next
            Dim aLista(20, 1) As String
            aLista(0, 0) = "Valori"
            aLista(0, 1) = cDimensioni
            Call ABCA_Util.xmlSalvaElemento(cNomeTabella, "COLONNE_WIDTH", aLista, 1)
            ' *************************************************************************************
            ' Salvo il Nome Colonna
            ' *************************************************************************************
            cDimensioni = ""
            For nConta = 0 To oDGV.columns.Count - 1
                If nConta > 0 Then
                    cDimensioni = cDimensioni + "|"
                End If
                cDimensioni = cDimensioni + oDGV.Columns(nConta).HeaderText()
            Next
            aLista(0, 0) = "Valori"
            aLista(0, 1) = cDimensioni
            Call ABCA_Util.xmlSalvaElemento(cNomeTabella, "COLONNE_NOME", aLista, 1)

        Catch ex As Exception
        End Try
    End Sub

    Public Sub ColonneDimensioneCarica(ByRef oDGV As DataGridView, cNomeTabella As String, ByRef aCampi(,) As String, ByRef aFormati(,) As Integer)
        ' vado a dimensionare le colonne
        '       Dim nContaCampi As Integer = 0
        Try
            Dim nAttributi As Integer = 0
            Dim aLista(20, 1) As String
            '            aLista(0, 0) = "ColonneDimensione"
            '            aLista(1, 0) = "ColonneNome"
            If ABCA_Util.xmlLeggiElemento(cNomeTabella, "FORM", aLista, nAttributi) = True Then
                Dim aDimensioni() As String = Split(ABCA_Util.ValoreDaLista("ColonneDimensione", aLista, nAttributi), "|")
                Dim nConta As Integer = 0
                aCampi(100, 1) = (aDimensioni.Count - 1).ToString
                For nConta = 0 To aDimensioni.Count - 1
                    If Val(aDimensioni(nConta)) = 0 Then
                        oDGV.Columns(nConta).Visible = False
                        aCampi(nConta, 1) = ""
                        aFormati(nConta, 1) = 0
                    Else
                        oDGV.Columns(nConta).Visible = True
                        oDGV.Columns(nConta).Width = Val(aDimensioni(nConta))
                        aFormati(nConta, 1) = Val(aDimensioni(nConta))
                    End If
                Next
                '   Call NomiColonneAssegna(oDGV, cNomeTabella)
                Dim aNomi() As String = Split(ABCA_Util.ValoreDaLista("ColonneNome", aLista, nAttributi), "|")
                For nConta = 0 To aNomi.Count - 1
                    If aNomi(nConta) > " " Then
                        oDGV.Columns(nConta).HeaderText = aNomi(nConta)
                    End If
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function PosizioneColonna(cNomeCampoDB As String, ByRef oDGV As DataGridView) As Integer
        Dim aLista(200, 1) As String
        cNomeCampoDB = cNomeCampoDB.ToUpper
        If ColonneNomeCampi(oDGV, aLista) = True Then
            For nColonna = 0 To oDGV.ColumnCount - 1
                If aLista(nColonna, 0) = cNomeCampoDB Then
                    Return nColonna
                    Exit Function
                End If
            Next
        End If
        Return -1
    End Function
    Public Function ColonneNomeCampi(ByRef oDGV As DataGridView, ByRef aLista(,) As String) As Boolean
        ' funzione che resttuisce il nome del campo associato alle colonne 
        Try
            Dim nColonna As Integer = 0
            For nColonna = 0 To oDGV.ColumnCount - 1
                aLista(nColonna, 0) = oDGV.Columns(nColonna).DataPropertyName.ToUpper
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function ColonneValoreCampi(ByRef oDGV As DataGridView, ByRef aLista(,) As String, ByVal nRiga As Integer) As Boolean
        ' funzione che resttuisce il valore del campo associato alle colonne 
        Try
            Dim nColonna As Integer = 0
            For nColonna = 0 To oDGV.ColumnCount - 1
                aLista(nColonna, 1) = oDGV.Rows.Item(nRiga).Cells(nColonna).Value.ToString()
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CaricaDatiTabella(ByRef oDGV As DataGridView, cConnessioneDefault As String, cNomeTabella As String, cQuery As String, Optional nTipoDB As enmTipoDB = enmTipoDB.MSSQLServer) As Boolean
        Try
            oDGV.DataBindings.Clear()
            oDGV.DataSource = Nothing
            oDGV.DataMember = Nothing
            oDGV.Rows.Clear()
            oDGV.Refresh()

            If nTipoDB = EnmTipoDB.MSSQLServer Then
                Dim oConnessione As New SqlConnection(cConnessioneDefault)
                Dim oDataAdapter As New SqlDataAdapter(cQuery, oConnessione)
                Dim oDataSet As New DataSet()
                '        oDGV.DataSource = Nothing
                '        oDGV.DataMember = Nothing
                oConnessione.Open()
                oDataAdapter.Fill(oDataSet, cNomeTabella)
                oDGV.DataSource = oDataSet
                oDGV.DataMember = cNomeTabella
            Else
                Dim oConnessione As New MySqlConnection(cConnessioneDefault)
                Dim oDataAdapter As New MySqlDataAdapter(cQuery, oConnessione)
                Dim oDataSet As New DataSet()
                oDGV.DataSource = Nothing
                oDGV.DataMember = Nothing
                oConnessione.Open()
                oDataAdapter.Fill(oDataSet, cNomeTabella)
                oDGV.DataSource = oDataSet
                oDGV.DataMember = cNomeTabella
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function FiltraTabella(ByRef oDGV As DataGridView, cNomeTabella As String, ByRef aCampi(,) As String, ByRef aFormati(,) As Integer, cQuery As String, Optional nTipoDB As EnmTipoDB = EnmTipoDB.MSSQLServer) As Boolean
        ' FUNZIONE CHE CARICA UNA TABELLA E ASSEGNA FORMATO CAMPI E DIMENSIONI
        Try
            ' *************************************************************************
            ' APRO LA CONNESSIOEN E ASSEGNO LA TABELLA
            ' *************************************************************************
            Dim oDataSet As New DataSet()
            If nTipoDB = EnmTipoDB.MSSQLServer Then
                Dim oConnessione As New SqlConnection(oDGV.Tag)
                Dim oDataAdapter As New SqlDataAdapter(cQuery, oConnessione)
                oDGV.DataSource = Nothing
                oDGV.DataMember = Nothing
                oConnessione.Open()
                oDataAdapter.Fill(oDataSet, cNomeTabella)
            Else
                Dim oConnessione As New MySqlConnection(oDGV.Tag)
                Dim oDataAdapter As New MySqlDataAdapter(cQuery, oConnessione)
                oDGV.DataSource = Nothing
                oDGV.DataMember = Nothing
                oConnessione.Open()
                oDataAdapter.Fill(oDataSet, cNomeTabella)
            End If
            oDGV.DataSource = oDataSet
            oDGV.DataMember = cNomeTabella
            Dim nConta As Integer = 0
            ' *************************************************************************
            ' assegno il tipo campo e lo formatto
            ' *************************************************************************
            For nConta = 0 To Val(aCampi(100, 1))
                FormatColonnaDB(oDGV, oDGV.Columns(nConta).DataPropertyName, aFormati(nConta, 0))
                If aFormati(nConta, 1) = 0 Then
                    oDGV.Columns(nConta).Visible = False
                Else
                    oDGV.Columns(nConta).Visible = True
                    oDGV.Columns(nConta).Width = aFormati(nConta, 1)
                End If
            Next
            oDGV.Visible = True
            Return True
        Catch ex As Exception
            oDGV.Visible = True
            Return False
        End Try


    End Function

    Public Function CollegaTabella(ByRef oDGV As DataGridView, cNomeTabella As String, ByRef cSelect As String, ByRef aCampi(,) As String, ByRef aFormati(,) As Integer, ByRef aDescr() As String, _
                                   ByRef aWhere() As String, ByRef aOrder() As String, ByRef cNomeTabellaDB As String, Optional cQuery As String = "") As Boolean
        ' FUNZIONE CHE CARICA UNA TABELLA E ASSEGNA FORMATO CAMPI E DIMENSIONI
        Try
            '       oDGV.visible = False
            Dim cConnessioneDefault As String = ""
            Dim nTipoDB As EnmTipoDB = EnmTipoDB.MSSQLServer
            ' *************************************************************************
            ' ASSEGNO COMUNQUE UN DEFAULT AL TIPO E DIMENSIONE CARATTERE
            ' *************************************************************************
            Dim cFontDefaultNome As String = "Tahoma"
            Dim nFontDefaultDimensione As Integer = 12
            Dim nColonnaBaseDimensione As Integer = 24
            Dim cRigaBaseColore As String = "HFFCCFFFF"
            Dim aLista(20, 1) As String
            Dim nConta As Integer = 0
            Dim nAttributi As Integer = 0
            ' *************************************************************************
            ' CARICO LE IMPOSTAZIONI DI DEFAULT
            ' *************************************************************************
            Call ABCA_Util.xmlLeggiElemento("SEARCH_DEFAULT", "SQL", aLista, nAttributi)
            cConnessioneDefault = ABCA_Util.ValoreDaLista("Connessione", aLista, nAttributi)
            aCampi(100, 0) = ABCA_Util.ValoreDaLista("TipoDB", aLista, nAttributi, "MSSQLSERVER")
            Select Case UCase(aCampi(100, 0))
                Case "MSSQLSERVER"
                    nTipoDB = EnmTipoDB.MSSQLServer
                Case "MYSQL"
                    nTipoDB = EnmTipoDB.MySql
                Case Else
                    nTipoDB = EnmTipoDB.MSSQLServer
            End Select
            Frm_Ricerca.nTipoDBGenerale = nTipoDB
            Call ABCA_Util.xmlLeggiElemento("SEARCH_DEFAULT", "FORM", aLista, nAttributi)
            cFontDefaultNome = ABCA_Util.ValoreDaLista("FontNome", aLista, nAttributi, cFontDefaultNome)
            nFontDefaultDimensione = ABCA_Util.ValoreDaLista("FontDimensione", aLista, nAttributi, nFontDefaultDimensione)
            nColonnaBaseDimensione = ABCA_Util.ValoreDaLista("ColonnaBaseDimensione", aLista, nAttributi, nColonnaBaseDimensione)
            cRigaBaseColore = ABCA_Util.ValoreDaLista("RigaBaseColore", aLista, nAttributi, cRigaBaseColore)

            oDGV.EnableHeadersVisualStyles = False
            oDGV.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(Val("&" + cRigaBaseColore))
            ' *************************************************************************
            ' CARICO LE IMPOSTAZIONI SPECIFICHE DELLA TABELLA RICHIESTA
            ' *************************************************************************
            Call ABCA_Util.xmlLeggiElemento(cNomeTabella, "FORM", aLista, nAttributi)

            Dim aRecord() As String
            aCampi(101, 0) = ABCA_Util.ValoreDaLista("Titolo", aLista, nAttributi, "")
            aRecord = Split(ABCA_Util.ValoreDaLista("TopLeftHeightWidth", aLista, nAttributi, ""), "|")
            aCampi(102, 0) = aRecord(0)
            aCampi(102, 1) = aRecord(1)
            aCampi(103, 0) = aRecord(2)
            aCampi(103, 1) = aRecord(3)

            cFontDefaultNome = ABCA_Util.ValoreDaLista("FontNome", aLista, nAttributi, cFontDefaultNome)
            nFontDefaultDimensione = ABCA_Util.ValoreDaLista("FontDimensione", aLista, nAttributi, nFontDefaultDimensione)
            nColonnaBaseDimensione = ABCA_Util.ValoreDaLista("ColonnaBaseDimensione", aLista, nAttributi, nColonnaBaseDimensione)

            If cQuery = "" Then
                Call ABCA_Util.xmlLeggiElemento(cNomeTabella, "TABELLA", aLista, nAttributi)
                '               MsgBox("griglia " & cNomeTabella)
                For nConta = 1 To 10
                    aDescr(nConta - 1) = ""
                    aWhere(nConta - 1) = ""
                    aOrder(nConta - 1) = ""
                    If ABCA_Util.ValoreDaLista("WhereOrder" & Microsoft.VisualBasic.Right("00" & nConta, 2), aLista, nAttributi, "") > " " Then
                        aRecord = Split(ABCA_Util.ValoreDaLista("WhereOrder" & Microsoft.VisualBasic.Right("00" & nConta, 2), aLista, nAttributi, ""), "|")
                        aWhere(nConta - 1) = Replace((Replace(aRecord(0), "-", "<")), "+", ">")
                        aOrder(nConta - 1) = aRecord(1)
                        aDescr(nConta - 1) = aRecord(2)
                    End If
                Next
                cSelect = ABCA_Util.ValoreDaLista("Select", aLista, nAttributi, "")
                If ABCA_Util.ValoreDaLista("From", aLista, nAttributi, "") > " " Then
                    cQuery = "SELECT " + cSelect + " FROM " + ABCA_Util.ValoreDaLista("From", aLista, nAttributi, "")
                    cNomeTabellaDB = ABCA_Util.ValoreDaLista("From", aLista, nAttributi, "")
                End If
                If aWhere(0) > " " Then
                    cQuery = cQuery + " WHERE " + aWhere(0)
                    If Frm_Ricerca.cFiltroWhere > " " Then
                        cQuery = cQuery + " AND " + Frm_Ricerca.cFiltroWhere
                    End If
                Else
                    If Frm_Ricerca.cFiltroWhere > " " Then
                        cQuery = cQuery + " WHERE " + Frm_Ricerca.cFiltroWhere
                    End If
                End If
                If aOrder(0) > " " Then
                    cQuery = cQuery + " ORDER BY " + aOrder(0)
                End If
            End If

            Call ABCA_Util.xmlLeggiElemento(cNomeTabella, "SQL", aLista, nAttributi)
            cConnessioneDefault = ABCA_Util.ValoreDaLista("Connessione", aLista, nAttributi, cConnessioneDefault)
            aCampi(100, 0) = ABCA_Util.ValoreDaLista("TipoDB", aLista, nAttributi, "" & aCampi(100, 0))

            ' *************************************************************************
            ' APRO LA CONNESSIONE E ASSEGNO LA TABELLA
            ' *************************************************************************
            If Frm_Ricerca.cStringaConnessione > " " Then
                cConnessioneDefault = Frm_Ricerca.cStringaConnessione
            End If
            oDGV.Tag = cConnessioneDefault
            If nTipoDB = EnmTipoDB.MSSQLServer Then
                Dim oConnessione As New SqlConnection(cConnessioneDefault)
                Dim oDataAdapter As New SqlDataAdapter(cQuery, oConnessione)
                Dim oDataSet As New DataSet()
                ' oDGV.DataSource = Nothing
                'oDGV.DataMember = Nothing
                oConnessione.Open()
                oDataAdapter.Fill(oDataSet, cNomeTabella)
                oDGV.DataSource = oDataSet
            Else
                Dim oConnessione As New MySqlConnection(cConnessioneDefault)
                Dim oDataAdapter As New MySqlDataAdapter(cQuery, oConnessione)
                Dim oDataSet As New DataSet()
                oDGV.DataSource = Nothing
                oDGV.DataMember = Nothing
                oConnessione.Open()
                oDataAdapter.Fill(oDataSet, cNomeTabella)
                oDGV.DataSource = oDataSet
            End If
            oDGV.DataMember = cNomeTabella
            oDGV.DefaultCellStyle.Font = New System.Drawing.Font(cFontDefaultNome, nFontDefaultDimensione)
            ' *************************************************************************
            ' assegno il tipo campo e lo formatto
            ' *************************************************************************
            Call ABCA_Util.xmlLeggiElemento(cNomeTabella, "FORM", aLista, nAttributi)
            Dim cTipoCampo() As String = Split(ABCA_Util.ValoreDaLista("COLONNETIPOCAMPO", aLista, nAttributi), "|")
            For nConta = 0 To cTipoCampo.Count - 1
                aCampi(nConta, 0) = oDGV.Columns(nConta).DataPropertyName
                aCampi(nConta, 1) = cTipoCampo(nConta).ToUpper
                Select Case cTipoCampo(nConta).ToUpper
                    Case "S"
                        FormatColonnaDB(oDGV, oDGV.Columns(nConta).DataPropertyName, tyFormato.Stringa)
                        aFormati(nConta, 0) = tyFormato.Stringa
                    Case "D"
                        FormatColonnaDB(oDGV, oDGV.Columns(nConta).DataPropertyName, tyFormato.Data)
                        aFormati(nConta, 0) = tyFormato.Data
                    Case "B"
                        FormatColonnaDB(oDGV, oDGV.Columns(nConta).DataPropertyName, tyFormato.Button)
                        aFormati(nConta, 0) = tyFormato.Button
                    Case "DC"
                        FormatColonnaDB(oDGV, oDGV.Columns(nConta).DataPropertyName, tyFormato.DataCorta)
                        aFormati(nConta, 0) = tyFormato.DataCorta
                    Case "DH"
                        FormatColonnaDB(oDGV, oDGV.Columns(nConta).DataPropertyName, tyFormato.DataOra)
                        aFormati(nConta, 0) = tyFormato.DataOra
                    Case "CB"
                        FormatColonnaDB(oDGV, oDGV.Columns(nConta).DataPropertyName, tyFormato.CheckBox)
                        aFormati(nConta, 0) = tyFormato.CheckBox
                    Case "INT"
                        FormatColonnaDB(oDGV, oDGV.Columns(nConta).DataPropertyName, tyFormato.NrSenzaSeparatori)
                        aFormati(nConta, 0) = tyFormato.NrSenzaSeparatori
                    Case Else
                        FormatColonnaDB(oDGV, oDGV.Columns(nConta).DataPropertyName, tyFormato.Numero, Val(Microsoft.VisualBasic.Right(cTipoCampo(nConta), 1)))
                        aFormati(nConta, 0) = tyFormato.Numero
                        aFormati(nConta, 2) = Val(Microsoft.VisualBasic.Right(cTipoCampo(nConta), 1))
                End Select
            Next
            ' *************************************************************************
            ' CARICO DIMENSIONE COLONNE (solo la prima volta)
            ' *************************************************************************
            Call ColonneDimensioneCarica(oDGV, cNomeTabella, aCampi, aFormati)
            oDGV.RowHeadersWidth = nColonnaBaseDimensione
            oDGV.Visible = True
            Return True
        Catch ex As Exception
            oDGV.Visible = True
            Return False
        End Try


    End Function

    Public Sub SetCarattereGriglia(ByRef oGrid As DataGridView, cFont As String, Optional nDimensioneFont As Integer = 8)
        oGrid.DefaultCellStyle.Font = New System.Drawing.Font(cFont, nDimensioneFont)
    End Sub

    Public Sub FormatColonnaDB(oGrid As DataGridView, cNomeCampo As String, ByVal nFormato As tyFormato, Optional nDecimali As Integer = 0, Optional cTitoloColonna As String = "", Optional nAllineamento As tyAllineamento = 0)
        Dim nColonna As Integer = 0
        Dim nColCampo As Integer = -1
        Try
            For nColonna = 0 To oGrid.ColumnCount - 1
                If oGrid.Columns(nColonna).DataPropertyName.ToUpper = cNomeCampo.ToUpper Then
                    nColCampo = nColonna
                    Exit For
                End If
            Next

            If nColCampo > -1 Then
                Call FormatColonna(oGrid, nColCampo, nFormato, nDecimali, cTitoloColonna, nAllineamento)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub FormatCella(oGrid As DataGridView, ByVal nRow As Integer, ByVal nCol As Integer, ByVal nFormato As tyFormato, Optional nDecimali As Integer = 0)
        Select Case nFormato
            Case tyFormato.NrSenzaSeparatori
                oGrid.Rows.Item(nRow).Cells(nCol).Style.Format = "#0"
            Case tyFormato.Numero
                oGrid.Rows.Item(nRow).Cells(nCol).Style.Format = "n" + nDecimali.ToString
            Case tyFormato.Data
                oGrid.Rows.Item(nRow).Cells(nCol).Style.Format = "dd/MM/yyyy"
            Case tyFormato.DataOra
                oGrid.Rows.Item(nRow).Cells(nCol).Style.Format = "dd/MM/yyyy HH:mm:ss"
            Case tyFormato.DataCorta
                oGrid.Rows.Item(nRow).Cells(nCol).Style.Format = "dd/MM/yy"
        End Select

    End Sub

    Public Sub FormatColonna(oGrid As DataGridView, ByVal nCol As Integer, ByVal nFormato As tyFormato, Optional nDecimali As Integer = 0, Optional cTitoloColonna As String = "", Optional nAllineamento As tyAllineamento = 0)
        Select Case nFormato
            Case tyFormato.Numero
                oGrid.Columns(nCol).DefaultCellStyle.Format = "n" + nDecimali.ToString
                If cTitoloColonna > " " Then
                    oGrid.columns(nCol).headertext = cTitoloColonna
                End If
                If nAllineamento = 0 Then
                    oGrid.Columns(nCol).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                Else
                    oGrid.Columns(nCol).DefaultCellStyle.Alignment = nAllineamento
                End If
            Case tyFormato.Data
                Dim cTestoColonna As String = oGrid.Columns(nCol).HeaderText
                Dim cCampoColonna As String = oGrid.Columns(nCol).DataPropertyName
                Dim oData As New GridCalendario.CalendarColumn
                If cTitoloColonna > " " Then
                    oData.HeaderText = cTitoloColonna
                Else
                    oData.HeaderText = cTestoColonna
                End If
                oData.DataPropertyName = cCampoColonna
                oData.DisplayIndex = nCol
                Call oGrid.Columns.RemoveAt(nCol)
                oGrid.Columns.INSERT(nCol, oData)
                'oGrid.Columns.Add(oData)
                If nAllineamento = 0 Then
                    oGrid.Columns(nCol).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                Else
                    oGrid.Columns(nCol).DefaultCellStyle.Alignment = nAllineamento
                End If
            Case tyFormato.DataOra
                oGrid.Columns(nCol).DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss"
                If cTitoloColonna > " " Then
                    oGrid.columns(nCol).headertext = cTitoloColonna
                End If
                If nAllineamento = 0 Then
                    oGrid.Columns(nCol).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                Else
                    oGrid.Columns(nCol).DefaultCellStyle.Alignment = nAllineamento
                End If
            Case tyFormato.DataCorta
                oGrid.Columns(nCol).DefaultCellStyle.Format = "dd/MM/yy"
                If cTitoloColonna > " " Then
                    oGrid.columns(nCol).headertext = cTitoloColonna
                End If
                If nAllineamento = 0 Then
                    oGrid.Columns(nCol).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                Else
                    oGrid.Columns(nCol).DefaultCellStyle.Alignment = nAllineamento
                End If
            Case tyFormato.Button
                Dim oButton As New DataGridViewButtonColumn
                If cTitoloColonna > " " Then
                    oButton.HeaderText = cTitoloColonna
                Else
                    oButton.HeaderText = oGrid.Columns(nCol).HeaderText
                End If
                oButton.DataPropertyName = oGrid.Columns(nCol).DataPropertyName
                oButton.DisplayIndex = nCol
                Call oGrid.Columns.RemoveAt(nCol)
                oGrid.Columns.INSERT(nCol, oButton)
                If nAllineamento = 0 Then
                    oGrid.Columns(nCol).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Else
                    oGrid.Columns(nCol).DefaultCellStyle.Alignment = nAllineamento
                End If
            Case tyFormato.CheckBox
                Dim oCheck As New DataGridViewCheckBoxColumn
                If cTitoloColonna > " " Then
                    oCheck.HeaderText = cTitoloColonna
                Else
                    oCheck.HeaderText = oGrid.Columns(nCol).HeaderText
                End If
                oCheck.DataPropertyName = oGrid.Columns(nCol).DataPropertyName
                oCheck.DisplayIndex = nCol
                Call oGrid.Columns.RemoveAt(nCol)
                oGrid.Columns.INSERT(nCol, oCheck)
                If nAllineamento = 0 Then
                    oGrid.Columns(nCol).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Else
                    oGrid.Columns(nCol).DefaultCellStyle.Alignment = nAllineamento
                End If
            Case tyFormato.Stringa
                If cTitoloColonna > " " Then
                    oGrid.columns(nCol).headertext = cTitoloColonna
                End If
                oGrid.Columns(nCol).DefaultCellStyle.Alignment = nAllineamento
            Case tyFormato.NrSenzaSeparatori
                oGrid.Columns(nCol).DefaultCellStyle.Format = "#0"
                If cTitoloColonna > " " Then
                    oGrid.Columns(nCol).HeaderText = cTitoloColonna
                End If
                If nAllineamento = 0 Then
                    oGrid.Columns(nCol).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                Else
                    oGrid.Columns(nCol).DefaultCellStyle.Alignment = nAllineamento
                End If
            Case Else
        End Select

    End Sub


End Class

Public Class GridCalendario

    Public Class CalendarColumn
        Inherits DataGridViewColumn

        Public Sub New()
            MyBase.New(New CalendarCell())
        End Sub

        Public Overrides Property CellTemplate() As DataGridViewCell
            Get
                Return MyBase.CellTemplate
            End Get
            Set(ByVal value As DataGridViewCell)

                ' Ensure that the cell used for the template is a CalendarCell. 
                If (value IsNot Nothing) AndAlso _
                    Not value.GetType().IsAssignableFrom(GetType(CalendarCell)) _
                    Then
                    Throw New InvalidCastException("Must be a CalendarCell")
                End If
                MyBase.CellTemplate = value

            End Set
        End Property

    End Class

    Public Class CalendarCell
        Inherits DataGridViewTextBoxCell

        Public Sub New()
            ' Use the short date format. 
            Me.Style.Format = "d"
        End Sub

        Public Overrides Sub InitializeEditingControl(ByVal rowIndex As Integer, _
            ByVal initialFormattedValue As Object, _
            ByVal dataGridViewCellStyle As DataGridViewCellStyle)

            Try
                ' Set the value of the editing control to the current cell value. 
                MyBase.InitializeEditingControl(rowIndex, initialFormattedValue, _
                    dataGridViewCellStyle)

                Dim ctl As CalendarEditingControl = _
                    CType(DataGridView.EditingControl, CalendarEditingControl)

                ' Use the default row value when Value property is null. 
                If (Me.Value Is Nothing) Then
                    ctl.Value = CType(Me.DefaultNewRowValue, DateTime)
                Else
                    ctl.Value = CType(Me.Value, DateTime)
                End If
            Catch ex As Exception
            End Try
        End Sub

        Public Overrides ReadOnly Property EditType() As Type
            Get
                ' Return the type of the editing control that CalendarCell uses. 
                Return GetType(CalendarEditingControl)
            End Get
        End Property

        Public Overrides ReadOnly Property ValueType() As Type
            Get
                ' Return the type of the value that CalendarCell contains. 
                Return GetType(DateTime)
            End Get
        End Property

        Public Overrides ReadOnly Property DefaultNewRowValue() As Object
            Get
                ' Use the current date and time as the default value. 
                Return DateTime.Now
            End Get
        End Property

    End Class

    Class CalendarEditingControl
        Inherits DateTimePicker
        Implements IDataGridViewEditingControl

        Private dataGridViewControl As System.Windows.Forms.DataGridView
        Private valueIsChanged As Boolean = False
        Private rowIndexNum As Integer

        Public Sub New()
            Me.Format = DateTimePickerFormat.Short
        End Sub

        Public Property EditingControlFormattedValue() As Object _
            Implements IDataGridViewEditingControl.EditingControlFormattedValue

            Get
                Return Me.Value.ToShortDateString()
            End Get

            Set(ByVal value As Object)
                Try
                    ' This will throw an exception of the string is  
                    ' null, empty, or not in the format of a date. 
                    Me.Value = DateTime.Parse(CStr(value))
                Catch
                    ' In the case of an exception, just use the default 
                    ' value so we're not left with a null value. 
                    Me.Value = DateTime.Now
                End Try
            End Set

        End Property

        Public Function GetEditingControlFormattedValue(ByVal context _
            As DataGridViewDataErrorContexts) As Object _
            Implements IDataGridViewEditingControl.GetEditingControlFormattedValue

            Return Me.Value.ToShortDateString()

        End Function

        Public Sub ApplyCellStyleToEditingControl(ByVal dataGridViewCellStyle As  _
            DataGridViewCellStyle) _
            Implements IDataGridViewEditingControl.ApplyCellStyleToEditingControl

            Me.Font = dataGridViewCellStyle.Font
            Me.CalendarForeColor = dataGridViewCellStyle.ForeColor
            Me.CalendarMonthBackground = dataGridViewCellStyle.BackColor

        End Sub

        Public Property EditingControlRowIndex() As Integer _
            Implements IDataGridViewEditingControl.EditingControlRowIndex

            Get
                Return rowIndexNum
            End Get
            Set(ByVal value As Integer)
                rowIndexNum = value
            End Set

        End Property

        Public Function EditingControlWantsInputKey(ByVal key As Keys, _
            ByVal dataGridViewWantsInputKey As Boolean) As Boolean _
            Implements IDataGridViewEditingControl.EditingControlWantsInputKey

            ' Let the DateTimePicker handle the keys listed. 
            Select Case key And Keys.KeyCode
                Case Keys.Left, Keys.Up, Keys.Down, Keys.Right, _
                    Keys.Home, Keys.End, Keys.PageDown, Keys.PageUp

                    Return True

                Case Else
                    Return Not dataGridViewWantsInputKey
            End Select

        End Function

        Public Sub PrepareEditingControlForEdit(ByVal selectAll As Boolean) _
            Implements IDataGridViewEditingControl.PrepareEditingControlForEdit

            ' No preparation needs to be done. 

        End Sub

        Public ReadOnly Property RepositionEditingControlOnValueChange() _
            As Boolean Implements _
            IDataGridViewEditingControl.RepositionEditingControlOnValueChange

            Get
                Return False
            End Get

        End Property

        Public Property EditingControlDataGridView() As System.Windows.Forms.DataGridView _
            Implements IDataGridViewEditingControl.EditingControlDataGridView

            Get
                Return dataGridViewControl
            End Get
            Set(ByVal value As System.Windows.Forms.DataGridView)
                dataGridViewControl = value
            End Set

        End Property

        Public Property EditingControlValueChanged() As Boolean _
            Implements IDataGridViewEditingControl.EditingControlValueChanged

            Get
                Return valueIsChanged
            End Get
            Set(ByVal value As Boolean)
                valueIsChanged = value
            End Set

        End Property

        Public ReadOnly Property EditingControlCursor() As Cursor _
            Implements IDataGridViewEditingControl.EditingPanelCursor

            Get
                Return MyBase.Cursor
            End Get

        End Property

        Protected Overrides Sub OnValueChanged(ByVal eventargs As EventArgs)

            ' Notify the DataGridView that the contents of the cell have changed.
            valueIsChanged = True
            Me.EditingControlDataGridView.NotifyCurrentCellDirty(True)
            MyBase.OnValueChanged(eventargs)

        End Sub

    End Class

End Class

