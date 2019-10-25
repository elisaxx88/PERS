Imports Word = Microsoft.Office.Interop.Word

Module Abca_GrigliaDoc

    Public Enum enmTipoCarattere
        Normale = 0
        Grassetto = 1
        Corsivo = 10
        GrassettoCorsivo = 11
        Sottolineato = 20
        SottolineatoCorsivo = 21
        SottolineatoGrassetto = 22
        SottolineatoGrassettoCorsivo = 23
    End Enum

    Public Enum enmAllineamento
        Sinistra = 0
        Destra = 2
        Centro = 1
    End Enum

    Public Enum enmLineaContorno
        No = 0
        Up = 1
        dx = 10
        UpDx = 11
        Down = 100
        UpDown = 101
        DxDown = 110
        UpDxDown = 111
        Sx = 1000
        UpSx = 1001
        DxSx = 1010
        UpDxSx = 1011
        DownSx = 1100
        UpDownSx = 1101
        DxDownSx = 1110
        UpDxDownSx = 1111
    End Enum

    Public Structure TyGriglia
        '        Public nCampiTotali As Long
        Public nNrColonne As Long
        Public nNrRighe As Long
        Public nAltezzaRighe As Long
        Public nDimensioneColonne() As Long
        Public cTesto(,) As String
        Public nTestoAllineamento(,) As enmAllineamento
        Public cFontNome(,) As String
        Public nFontDimensione(,) As Long
        Public nFontColore(,) As Long
        Public nFontTipo(,) As enmTipoCarattere
        Public nColoreSfondo(,) As Microsoft.Office.Interop.Word.WdColor
        Public nLineaContorno(,) As enmLineaContorno
        Public nLineaContornoColore(,) As Microsoft.Office.Interop.Word.WdColor

        Public nColonna() As Long
        Public nRiga() As Long
        Public nColonneDaUnire() As Long
        Public nColonneDaUnireLargh() As Long
    End Structure

    Public aGriglia As TyGriglia
    Public WithEvents oWord As New Word.Application

    Public Sub InizializzaoWord()
        oWord = New Word.Application
    End Sub

    Public Sub DistruggioWord()
        oWord = Nothing
    End Sub

    Public Sub InizializzaArrayGriglia()
        Dim nContaR As Long
        Dim nContaC As Long

        ReDim aGriglia.nDimensioneColonne(50)
        ReDim aGriglia.cTesto(500, 50)
        ReDim aGriglia.nTestoAllineamento(500, 50)
        ReDim aGriglia.cFontNome(500, 50)
        ReDim aGriglia.nFontDimensione(500, 50)
        ReDim aGriglia.nFontColore(500, 50)
        ReDim aGriglia.nFontTipo(500, 50)
        ReDim aGriglia.nColoreSfondo(500, 50)
        ReDim aGriglia.nLineaContorno(500, 50)
        ReDim aGriglia.nLineaContornoColore(500, 50)


        ReDim aGriglia.nColonna(500)
        ReDim aGriglia.nRiga(500)
        ReDim aGriglia.nColonneDaUnire(500)
        ReDim aGriglia.nColonneDaUnireLargh(500)


        For nContaR = 1 To 500
            For nContaC = 1 To 50
                aGriglia.cFontNome(nContaR, nContaC) = ""
                aGriglia.cTesto(nContaR, nContaC) = ""
                aGriglia.nColoreSfondo(nContaR, nContaC) = WdColor.wdColorWhite
                aGriglia.nFontColore(nContaR, nContaC) = WdColor.wdColorBlack
                aGriglia.nFontDimensione(nContaR, nContaC) = 10
                aGriglia.nFontTipo(nContaR, nContaC) = enmTipoCarattere.Normale
                aGriglia.nLineaContorno(nContaR, nContaC) = enmLineaContorno.No
                aGriglia.nLineaContornoColore(nContaR, nContaC) = WdColor.wdColorBlack
                aGriglia.nTestoAllineamento(nContaR, nContaC) = enmAllineamento.Sinistra
                '                aGriglia.nColonneDaUnire(nContaR) = 0
                '                aGriglia.nColonneDaUnireLargh(nContaR) = 0
            Next nContaC
        Next nContaR
        aGriglia.nNrColonne = 0
        aGriglia.nNrRighe = 0
    End Sub

    Public Sub CreaGriglia()

        Dim nConta As Long
        Dim nProgr_R As Long = 0
        Dim nProgr_C As Long = 0
        Dim oTabella As Microsoft.Office.Interop.Word.Table ' Word.Table


        ' Creo la tabella
        oTabella = oWord.ActiveDocument.Tables.Add(oWord.Selection.Range, aGriglia.nNrRighe, aGriglia.nNrColonne)
        
        ' Assegno la larghezza colonne alla tabella
        For nConta = 1 To aGriglia.nNrColonne
            oTabella.Cell(0, nConta).Width = aGriglia.nDimensioneColonne(nConta)
            oTabella.Cell(0, nConta).Height = aGriglia.nAltezzaRighe
            oTabella.Cell(0, nConta).VerticalAlignment = 1
        Next nConta
        'oWord.Visible = True
        For nProgr_R = 1 To aGriglia.nNrRighe
            For nProgr_C = 1 To aGriglia.nNrColonne
                ' seleziono la cella
                oTabella.Cell(nProgr_R, nProgr_C).Select()
                ' resetto il formato
                oWord.Selection.Style = oWord.ActiveDocument.Styles("Nessuna spaziatura")
                ' devo fare unione di celle
                If aGriglia.nColonneDaUnire(nConta) > 0 Then
                    ' oWord.Selection.MoveRight(Unit:=1, Count:=aGriglia.nColonneDaUnire(nConta), Extend:=1)
                    ' oWord.Selection.Style = oWord.ActiveDocument.Styles("Nessuna spaziatura")
                    ' oTabella.Cell(aGriglia.nRiga(nConta), nConta).VerticalAlignment = 1
                    ' oWord.Selection.Cells.Merge()
                    ' oTabella.Cell(aGriglia.nRiga(nConta), nConta).Width = aGriglia.nColonneDaUnireLargh(nConta)
                Else
                    oTabella.Cell(nProgr_R, nProgr_C).Width = aGriglia.nDimensioneColonne(nProgr_C)
                    oTabella.Cell(nProgr_R, nProgr_C).Height = aGriglia.nAltezzaRighe
                    oTabella.Cell(nProgr_R, nProgr_C).VerticalAlignment = 1
                End If

                ' assegno il testo
                oWord.Selection.TypeText(Text:=aGriglia.cTesto(nProgr_R, nProgr_C))


                ' seleziono tutto il testo
                oWord.Selection.HomeKey(Unit:=5)
                oWord.Selection.EndKey(Unit:=5, Extend:=1)

                ' nome font da usare
                oWord.Selection.Font.Name = aGriglia.cFontNome(nProgr_R, nProgr_C)
                ' dimensione del font
                oWord.Selection.Font.Size = aGriglia.nFontDimensione(nProgr_R, nProgr_C)
                ' allineamento del testo
                oWord.Selection.ParagraphFormat.Alignment = aGriglia.nTestoAllineamento(nProgr_R, nProgr_C)
                'verifico che non ci sia già il bold o italic o sottolineato selezionato
                If oWord.Selection.Font.Bold <> 0 Then
                    oWord.Selection.Font.Bold = 9999998
                End If
                If oWord.Selection.Font.Italic <> 0 Then
                    oWord.Selection.Font.Italic = 9999998
                End If
                If oWord.Selection.Font.Underline <> 0 Then
                    oWord.Selection.Font.Underline = 0
                End If
                '            SottolineatoCorsivo = 21
                '    SottolineatoGrassetto = 22
                '    SottolineatoGrassettoCorsivo = 23

                ' testo in grassetto
                If aGriglia.nFontTipo(nProgr_R, nProgr_C) = enmTipoCarattere.Grassetto Or aGriglia.nFontTipo(nProgr_R, nProgr_C) = enmTipoCarattere.GrassettoCorsivo _
                        Or aGriglia.nFontTipo(nProgr_R, nProgr_C) = enmTipoCarattere.SottolineatoGrassetto Or aGriglia.nFontTipo(nProgr_R, nProgr_C) = enmTipoCarattere.SottolineatoGrassettoCorsivo Then
                    oWord.Selection.Font.Bold = 9999998
                End If
                ' testo in corsivo
                If aGriglia.nFontTipo(nProgr_R, nProgr_C) = enmTipoCarattere.Corsivo Or aGriglia.nFontTipo(nProgr_R, nProgr_C) = enmTipoCarattere.GrassettoCorsivo _
                        Or aGriglia.nFontTipo(nProgr_R, nProgr_C) = enmTipoCarattere.SottolineatoCorsivo Or aGriglia.nFontTipo(nProgr_R, nProgr_C) = enmTipoCarattere.SottolineatoGrassettoCorsivo Then
                    oWord.Selection.Font.Italic = 9999998
                End If
                ' testo sottolineato
                If aGriglia.nFontTipo(nProgr_R, nProgr_C) = enmTipoCarattere.Sottolineato Or aGriglia.nFontTipo(nProgr_R, nProgr_C) = enmTipoCarattere.SottolineatoCorsivo _
                        Or aGriglia.nFontTipo(nProgr_R, nProgr_C) = enmTipoCarattere.SottolineatoGrassetto Or aGriglia.nFontTipo(nProgr_R, nProgr_C) = enmTipoCarattere.SottolineatoGrassettoCorsivo Then
                    oWord.Selection.Font.Underline = 1
                End If
                ' colore del testo
                If aGriglia.nFontColore(nProgr_R, nProgr_C) <> -1 Then
                    oWord.Selection.Font.Color = aGriglia.nFontColore(nProgr_R, nProgr_C)
                End If
                ' colore dello sfondo
                If aGriglia.nColoreSfondo(nProgr_R, nProgr_C) <> -1 Then
                    oWord.Selection.Shading.BackgroundPatternColor = aGriglia.nColoreSfondo(nProgr_R, nProgr_C)
                End If
                ' linea a contorno
                If aGriglia.nLineaContorno(nProgr_R, nProgr_C) <> enmLineaContorno.No Then
                    ' alto
                    Select Case aGriglia.nLineaContorno(nProgr_R, nProgr_C)
                        Case 1, 11, 101, 111, 1001, 1011, 1101, 1111
                            oWord.Selection.Borders(-1).LineStyle = 1
                            oWord.Selection.Borders(-1).Color = aGriglia.nLineaContornoColore(nProgr_R, nProgr_C)
                    End Select
                    ' destra
                    Select Case aGriglia.nLineaContorno(nProgr_R, nProgr_C)
                        Case 10, 11, 110, 111, 1010, 1011, 1110, 1111
                            oWord.Selection.Borders(-4).LineStyle = 1
                            oWord.Selection.Borders(-4).Color = aGriglia.nLineaContornoColore(nProgr_R, nProgr_C)
                    End Select
                    ' basso
                    Select Case aGriglia.nLineaContorno(nProgr_R, nProgr_C)
                        Case 100, 101, 110, 111, 1100, 1101, 1110, 1111
                            oWord.Selection.Borders(-3).LineStyle = 1
                            oWord.Selection.Borders(-3).Color = aGriglia.nLineaContornoColore(nProgr_R, nProgr_C)
                    End Select
                    ' sinistra
                    Select Case aGriglia.nLineaContorno(nProgr_R, nProgr_C)
                        Case 1000, 1001, 1010, 1011, 1100, 1101, 1110, 1111
                            oWord.Selection.Borders(-2).LineStyle = 1
                            oWord.Selection.Borders(-2).Color = aGriglia.nLineaContornoColore(nProgr_R, nProgr_C)
                    End Select
                End If

                ' assegno il testo
                '        oWord.Selection.typetext Text:=aGriglia.cTesto(nConta)

                ' If nConta < aGriglia.nCampiTotali Then
                ' Call oWord.Selection.MoveRight(12, 1)
                ' End If

            Next nProgr_C
        Next nProgr_R
        oTabella = Nothing

    End Sub



End Module
