Public Class Txt_Ricerca

    Private Lbl_Nomi_S As String = ""
    Private Txt_Nomi_S As String = ""
    Private c_Ricerca_S As String = ""
    Private c_FiltroRicerca As String = ""

    Public Event RecordSelezionato As EventHandler

    Public Sub PopolaValori(cConnessione As String, Optional cWhereBis As String = "")
        Try
            Dim oSql As New clsABCA_DB
            Dim oDS As New DataSet
            Dim aCampi() As String
            Dim cValoreCampoDB As String = ""
            Dim nConta As Integer = 0
            Dim cQuery As String = ""
            Dim cWhere As String
            Dim cApice As String = ""
            Dim cValorePrecedente As String = ""
            Dim aValori(20) As String
            aValori = Split(Txt_Nomi_S, "|")

            Dim cNomeCampo As String = aValori(1)
            Dim cNomeControllo As String = aValori(0)
            Dim cValorecampo As String = Me.Parent.Controls(aValori(0)).Text
            cApice = Me.Parent.Controls(cNomeControllo).Tag
            aValori = Split(cApice, "|")
            If aValori(3) = "C" Then
                cApice = "'"
            Else
                cApice = ""
            End If
            cWhere = cNomeCampo & " = " & cApice & cValorecampo & cApice
            If cWhereBis > " " Then
                cWhere = cWhere & " AND " & cWhereBis
            End If
            If oSql.ApriConnessione("", "", "", "", cConnessione) = True Then
                Dim aLista(20, 1) As String
                Dim nAttributi As Integer = 0

                Call ABCA_Util.xmlLeggiElemento(c_Ricerca_S, "TABELLA", aLista, nAttributi)
                cQuery = "SELECT " & ABCA_Util.ValoreDaLista("Select", aLista, nAttributi, "")
                cQuery = cQuery & " FROM " & ABCA_Util.ValoreDaLista("From", aLista, nAttributi, "")
                cQuery = cQuery & " WHERE " & cWhere
                If oSql.CaricaRecords(cQuery, "XTabellaRicerca", oDS, False) = True Then
                    If Txt_Nomi_S > "" Then
                        aCampi = Split(Txt_Nomi_S, "|")
                        If aCampi.Count Mod 2 > 0 Then
                            MsgBox("La proprietà zzTxt_Nomi ha un numero di parametri dispari. Vanno inseriti come 'Txt_NomeCampo|NomeCampoSuDB'")
                        ElseIf aCampi.Count > 0 Then
                            For nConta = 0 To aCampi.Count - 1 Step 2
                                If InStr(aCampi(nConta + 1).ToUpper, "#", CompareMethod.Binary) > 0 Then
                                    Me.Parent.Controls(aCampi(nConta)).Text = Replace(aCampi(nConta + 1), "#", "")
                                Else
                                    Me.Parent.Controls(aCampi(nConta)).Text = oDS.Tables(0).Rows(0).Item(aCampi(nConta + 1).ToUpper).ToString
                                End If
                            Next
                        End If
                    End If
                    If Lbl_Nomi_S > "" Then
                        aCampi = Split(Lbl_Nomi_S, "|")
                        If aCampi.Count Mod 2 > 0 Then
                            MsgBox("La proprietà zzLbl_Nomi ha un numero di parametri dispari. Vanno inseriti come 'Lbl_NomeCampo|NomeCampoSuDB'")
                        ElseIf aCampi.Count > 0 Then
                            For nConta = 0 To aCampi.Count - 1 Step 2
                                If InStr(aCampi(nConta + 1).ToUpper, "#", CompareMethod.Binary) > 0 Then
                                    Me.Parent.Controls(aCampi(nConta)).Text = Replace(aCampi(nConta + 1), "#", "")
                                Else
                                    Me.Parent.Controls(aCampi(nConta)).Text = oDS.Tables(0).Rows(0).Item(aCampi(nConta + 1).ToUpper).ToString
                                End If
                            Next
                        End If
                    End If

                End If
            End If
        Catch ex As Exception

        End Try

    End Sub
 
    Public Sub ValoriCampiMetti(cCampo As String, cValori As String)
        Select Case cCampo.ToUpper
            Case "ZZRICERCA"
                zzRicerca = cValori
            Case "ZZLBL_NOMI"
                zzLbl_Nomi = cValori
            Case "ZZTXT_NOMI"
                zzTxt_Nomi = cValori
        End Select
    End Sub
    Public Function ValoriCampiDammi(cCampo As String) As String
        Select Case cCampo.ToUpper
            Case "ZZRICERCA"
                Return c_Ricerca_S
            Case "ZZLBL_NOMI"
                Return Lbl_Nomi_S
            Case "ZZTXT_NOMI"
                Return Txt_Nomi_S
            Case Else
                Return ""
        End Select
    End Function
    Public Sub Btn_Search_Click(sender As Object, e As EventArgs) Handles Btn_Search.Click
        If c_FiltroRicerca > " " Then
            Frm_Ricerca.cFiltroWhere = c_FiltroRicerca
        Else
            Frm_Ricerca.cFiltroWhere = ""
        End If
        Frm_Ricerca.Apri_Ricerca(c_Ricerca_S)
        Frm_Ricerca.ShowDialog()
        Call PopolaValoriTxtLabel()
    End Sub

    Private Sub PopolaValoriTxtLabel(Optional bRaiseEvent As Boolean = True)
        Try
            Dim nConta As Integer = 0
            Dim cAppoggio As String = ""
            For nConta = 0 To Frm_Ricerca.nNrCampi
                cAppoggio = cAppoggio & Frm_Ricerca.aValoriRitorno(nConta, 1)
            Next
            cAppoggio = Microsoft.VisualBasic.Trim(cAppoggio)
            Dim aCampi() As String
            If Frm_Ricerca.nNrCampi > 0 And cAppoggio > " " Then
                If Txt_Nomi_S > "" Then
                    aCampi = Split(Txt_Nomi_S, "|")
                    If aCampi.Count Mod 2 > 0 Then
                        MsgBox("La proprietà zzTxt_Nomi ha un numero di parametri dispari. Vanno inseriti come 'Txt_NomeCampo|NomeCampoSuDB'")
                    ElseIf aCampi.Count > 0 Then
                        For nConta = 0 To aCampi.Count - 1 Step 2
                            If InStr(aCampi(nConta + 1).ToUpper, "#", CompareMethod.Binary) > 0 Then
                                Me.Parent.Controls(aCampi(nConta)).Text = Replace(aCampi(nConta + 1), "#", "")
                            Else
                                Me.Parent.Controls(aCampi(nConta)).Text = DammiValore(aCampi(nConta + 1))
                            End If
                        Next
                    End If
                End If
                If Lbl_Nomi_S > "" Then
                    aCampi = Split(Lbl_Nomi_S, "|")
                    If aCampi.Count Mod 2 > 0 Then
                        MsgBox("La proprietà zzLbl_Nomi ha un numero di parametri dispari. Vanno inseriti come 'Lbl_NomeCampo|NomeCampoSuDB'")
                    ElseIf aCampi.Count > 0 Then
                        For nConta = 0 To aCampi.Count - 1 Step 2
                            If InStr(aCampi(nConta + 1).ToUpper, "#", CompareMethod.Binary) > 0 Then
                                Me.Parent.Controls(aCampi(nConta)).Text = Replace(aCampi(nConta + 1), "#", "")
                            Else
                                Me.Parent.Controls(aCampi(nConta)).Text = DammiValore(aCampi(nConta + 1))
                            End If
                        Next
                    End If
                End If
                If bRaiseEvent = True Then
                    RaiseEvent RecordSelezionato(Me, Nothing)
                End If
            End If
        Catch ex As Exception
        End Try

    End Sub
    Private Function DammiValore(cCampo As String)
        Dim nCampi As Integer = 0
        cCampo = cCampo.ToUpper
        For nCampi = 0 To Frm_Ricerca.nNrCampi - 1
            If Frm_Ricerca.aValoriRitorno(nCampi, 0) = cCampo Then
                Return Frm_Ricerca.aValoriRitorno(nCampi, 1)
            End If
        Next
        Return ""
    End Function

    Property zzLbl_Nomi() As String
        Get
            Return Lbl_Nomi_S
        End Get
        Set(ByVal cLbl_Nomi As String)
            Lbl_Nomi_S = cLbl_Nomi
        End Set
    End Property

    Property zzTxt_Nomi() As String
        Get
            Return Txt_Nomi_S
        End Get
        Set(ByVal cTxt_Nomi As String)
            Txt_Nomi_S = cTxt_Nomi
        End Set
    End Property

    Property zzRicerca() As String
        Get
            Return c_Ricerca_S
        End Get
        Set(ByVal cRicerca As String)
            c_Ricerca_S = cRicerca
        End Set
    End Property

    Property zzFiltroRicerca() As String
        Get
            Return c_FiltroRicerca
        End Get
        Set(ByVal cFiltroRicerca As String)
            c_FiltroRicerca = cFiltroRicerca
        End Set
    End Property

    Private Sub Btn_Canc_Click(sender As Object, e As EventArgs) Handles Btn_Canc.Click
        Try
            Dim nConta As Integer = 0
            Dim aCampi() As String
            If Frm_Ricerca.nNrCampi > 0 Then
                If Txt_Nomi_S > "" Then
                    aCampi = Split(Txt_Nomi_S, "|")
                    If aCampi.Count Mod 2 > 0 Then
                        MsgBox("La proprietà zzTxt_Nomi ha un numero di parametri dispari. Vanno inseriti come 'Txt_NomeCampo|NomeCampoSuDB'")
                    ElseIf aCampi.Count > 0 Then
                        For nConta = 0 To aCampi.Count - 1 Step 2
                            Me.Parent.Controls(aCampi(nConta)).Text = ""
                        Next
                    End If
                End If
                If Lbl_Nomi_S > "" Then
                    aCampi = Split(Lbl_Nomi_S, "|")
                    If aCampi.Count Mod 2 > 0 Then
                        MsgBox("La proprietà zzLbl_Nomi ha un numero di parametri dispari. Vanno inseriti come 'Lbl_NomeCampo|NomeCampoSuDB'")
                    ElseIf aCampi.Count > 0 Then
                        For nConta = 0 To aCampi.Count - 1 Step 2
                            Me.Parent.Controls(aCampi(nConta)).Text = ""
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class
