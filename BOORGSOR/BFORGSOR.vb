Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp
Imports NTSInformatica.CLE__APP
Imports NTSinformatica

Public Class CLFORGSOR
  Inherits CLEORGSOR
  Dim BPrimo As Boolean = False
  Dim oClhGsor As CLHORGSOR
  Public DTCPNE_OF As DataTable = Nothing
  Public DrMovord As DataRow
  Public intColonneCorpo As Integer
  Public bModificaForzata As Boolean
  Public strSerieOF(0) As String
  Public intAnnoOF(0) As Integer
  Public intNumordOF(0) As Integer
  Dim intIBoundArray As Integer
  Public bBloccaPrezzoFornitore As Boolean
  Public oCleBoll As CLEVEBOLL = Nothing
  Public strTipoDoc As String = "A"
  Public nAnnoDoc As Integer
  Public strSerieDoc As String = " "
  Public lNumTmpDoc As Integer
  Dim dtDataDoc As Date
  Dim decPrezzoDoc As Decimal
  Dim bCPNEUnaSolaRiga As Boolean = False
  Public Overrides Function Init(ByRef App As CLE__APP, ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, strTabella As String, bFiller1 As Boolean, strFiller1 As String, strFiller2 As String) As Boolean
    Init = MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bFiller1, strFiller1, strFiller2)
    If Init Then
      If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "UnaSolaRigaPerRigaOrd", "N", " ", "N") = "S" Then
        bCPNEUnaSolaRiga = True
      End If
    End If
  End Function
  Public Sub CPNEInizializzaPers(ByVal commeca As Integer)
    oClhGsor = CType(oCldGsor, CLHORGSOR)

    Dim Strtipork As String = oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "TiporkOf", "O", " ", "O")

    oClhGsor.CPNEMostrOf(dsShared, commeca, Strtipork)
    'If BPrimo = False Then
    AddHandler dsShared.Tables("CPNE_OF").ColumnChanging, AddressOf BeforeColUpdate_CPNE_OF
    AddHandler dsShared.Tables("CPNE_OF").ColumnChanged, AddressOf AfterColUpdate_CPNE_OF
    DTCPNE_OF = dsShared.Tables("CPNE_OF")
    DTCPNE_OF.Columns("codditt").DefaultValue = ".,"
    DTCPNE_OF.Columns("mo_Riga").DefaultValue = 99999
    If IsNothing(dsShared.Tables("CPNE.marg")) Then
      dsShared.Tables.Add("CPNE.marg")
      dsShared.Tables("CPNE.marg").Columns.Add("xx_art", GetType(String))
      dsShared.Tables("CPNE.marg").Columns.Add("xx_costo", GetType(String))
      dsShared.Tables("CPNE.marg").Columns.Add("xx_ricavo", GetType(String))
      dsShared.Tables("CPNE.marg").Columns.Add("xx_marg", GetType(String))
      dsShared.Tables("CPNE.marg").Columns.Add("xx_diff", GetType(String))
      dsShared.Tables("CPNE.marg").Rows.Add()
      dsShared.Tables("CPNE.marg").Rows(0)!xx_art = "D"
      dsShared.Tables("CPNE.marg").Rows(0)!xx_costo = "0"
      dsShared.Tables("CPNE.marg").Rows(0)!xx_ricavo = "0"
      dsShared.Tables("CPNE.marg").Rows(0)!xx_marg = "0"
      dsShared.Tables("CPNE.marg").Rows(0)!xx_diff = "0"
    End If
    If IsNothing(dsShared.Tables("CPNE.TotFor")) Then
      dsShared.Tables.Add("CPNE.TotFor")
      dsShared.Tables("CPNE.TotFor").Columns.Add("xx_codfor", GetType(String))
      dsShared.Tables("CPNE.TotFor").Columns.Add("xx_desfor", GetType(String))
      dsShared.Tables("CPNE.TotFor").Columns.Add("xx_valfor", GetType(Decimal))
    End If
    BPrimo = True
    'BerPers inizio
    intColonneCorpo = 0
    If dsShared.Tables("TESTA").Rows(0)!et_tipork.ToString = "R" Then
      If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "RigaSingolaCliFor", "N", " ", "N") = "S" Then
        'If bNew Then

        If IsNothing(dsShared.Tables("CORPO").Columns("xx_forriga")) Then
          dsShared.Tables("CORPO").Columns.Add("xx_forriga", GetType(Integer)).Caption = "Riga Forn."
        End If
        If IsNothing(dsShared.Tables("CORPO").Columns("xx_forcod")) Then
          dsShared.Tables("CORPO").Columns.Add("xx_forcod", GetType(Integer)).Caption = "Codice Forn."
        End If
        If IsNothing(dsShared.Tables("CORPO").Columns("xx_fordes")) Then
          dsShared.Tables("CORPO").Columns.Add("xx_fordes", GetType(String)).Caption = "Desc.Forn."
        End If
        If IsNothing(dsShared.Tables("CORPO").Columns("xx_forartcod")) Then
          dsShared.Tables("CORPO").Columns.Add("xx_forartcod", GetType(String)).Caption = "Articolo Forn."
        End If
        If IsNothing(dsShared.Tables("CORPO").Columns("xx_forartdes")) Then
          dsShared.Tables("CORPO").Columns.Add("xx_forartdes", GetType(String)).Caption = "Descr. Articolo Forn."
        End If
        If IsNothing(dsShared.Tables("CORPO").Columns("xx_forartdesint")) Then
          dsShared.Tables("CORPO").Columns.Add("xx_forartdesint", GetType(String)).Caption = "Descr. interna Articolo Forn."
        End If
        If IsNothing(dsShared.Tables("CORPO").Columns("xx_fornote")) Then
          dsShared.Tables("CORPO").Columns.Add("xx_fornote", GetType(String)).Caption = "Note Forn."
        End If
        If IsNothing(dsShared.Tables("CORPO").Columns("xx_forcolli")) Then
          dsShared.Tables("CORPO").Columns.Add("xx_forcolli", GetType(Decimal)).Caption = "Colli Forn."
        End If
        If IsNothing(dsShared.Tables("CORPO").Columns("xx_forquant")) Then
          dsShared.Tables("CORPO").Columns.Add("xx_forquant", GetType(Decimal)).Caption = "Quantità Forn."
        End If
        If IsNothing(dsShared.Tables("CORPO").Columns("xx_forprezzo")) Then
          dsShared.Tables("CORPO").Columns.Add("xx_forprezzo", GetType(Decimal)).Caption = "Prezzo Forn."
        End If
        If IsNothing(dsShared.Tables("CORPO").Columns("xx_forsconto1")) Then
          dsShared.Tables("CORPO").Columns.Add("xx_forsconto1", GetType(Decimal)).Caption = "Sconto1 Forn."
        End If
        If IsNothing(dsShared.Tables("CORPO").Columns("xx_forsconto2")) Then
          dsShared.Tables("CORPO").Columns.Add("xx_forsconto2", GetType(Decimal)).Caption = "Sconto2 Forn."
        End If
        If IsNothing(dsShared.Tables("CORPO").Columns("xx_forsconto3")) Then
          dsShared.Tables("CORPO").Columns.Add("xx_forsconto3", GetType(Decimal)).Caption = "Sconto3 Forn."
        End If
        If IsNothing(dsShared.Tables("CORPO").Columns("xx_forsconto4")) Then
          dsShared.Tables("CORPO").Columns.Add("xx_forsconto4", GetType(Decimal)).Caption = "Sconto4 Forn."
        End If
        If IsNothing(dsShared.Tables("CORPO").Columns("xx_forsconto5")) Then
          dsShared.Tables("CORPO").Columns.Add("xx_forsconto5", GetType(Decimal)).Caption = "Sconto5 Forn."
        End If
        If IsNothing(dsShared.Tables("CORPO").Columns("xx_forsconto6")) Then
          dsShared.Tables("CORPO").Columns.Add("xx_forsconto6", GetType(Decimal)).Caption = "Sconto6 Forn."
        End If
        If IsNothing(dsShared.Tables("CORPO").Columns("xx_forvalriga")) Then
          dsShared.Tables("CORPO").Columns.Add("xx_forvalriga", GetType(Decimal)).Caption = "Valore riga Forn."
        End If
      End If
      intColonneCorpo = dsShared.Tables("CORPO").Columns.Count
      'End If
      If IsNothing(dsShared.Tables("TESTA").Columns("xx_telef")) Then
        dsShared.Tables("TESTA").Columns.Add("xx_telef", GetType(String))
      End If
      If IsNothing(dsShared.Tables("TESTA").Columns("xx_inddestdiv")) Then
        dsShared.Tables("TESTA").Columns.Add("xx_inddestdiv", GetType(String))
      End If
      If IsNothing(dsShared.Tables("TESTA").Columns("xx_teldestdiv")) Then
        dsShared.Tables("TESTA").Columns.Add("xx_teldestdiv", GetType(String))
      End If
      If IsNothing(dsShared.Tables("TESTA").Columns("xx_squadra")) Then
        dsShared.Tables("TESTA").Columns.Add("xx_squadra", GetType(String))
      End If
      If dsShared.Tables("TESTA").Columns.Contains("hh_squadra") Then
        If Not (IsDBNull(dsShared.Tables("TESTA").Rows(0)!hh_squadra)) Then
          If dsShared.Tables("TESTA").Rows(0)!hh_squadra.ToString <> "" Then
            dttET.Rows(0)!xx_squadra = oClhGsor.CPNEDecodificaSquadra(strDittaCorrente, dttET.Rows(0)!hh_squadra.ToString)
          End If
        End If
      End If
      If IsNothing(dsShared.Tables("TESTA").Columns("xx_valresiduo")) Then
        dsShared.Tables("TESTA").Columns.Add("xx_valresiduo", GetType(String))
        dttET.Rows(0)!xx_valresiduo = dttET.Rows(0)!et_totdoc
      End If
    End If
  End Sub
  Public Sub CPNEAggiungiHandlerCPNEScaPag()
    RemoveHandler dsShared.Tables("CPNE.ScaPag").ColumnChanging, AddressOf BeforeColUpdate_CPNEScaPag
    RemoveHandler dsShared.Tables("CPNE.ScaPag").ColumnChanged, AddressOf AfterColUpdate_CPNEScaPag
    AddHandler dsShared.Tables("CPNE.ScaPag").ColumnChanging, AddressOf BeforeColUpdate_CPNEScaPag
    AddHandler dsShared.Tables("CPNE.ScaPag").ColumnChanged, AddressOf AfterColUpdate_CPNEScaPag
  End Sub
  Public Sub CPNEAggCampiAggiuntivi(strNomeCampo As String)
    Dim dTTmp As New DataTable
    Dim StrTmp As String = ""

    If dttET.Rows(0)!et_tipork.ToString = "R" Then
      Select Case strNomeCampo
        Case "et_conto"
          If oCldGsor.ValCodiceDb(dttET.Rows(0)!et_conto.ToString, strDittaCorrente, "anagra", "N", StrTmp, dTTmp) Then
            If dTTmp.Rows.Count > 0 Then
              dttET.Rows(0)!xx_telef = dTTmp.Rows(0)!an_telef
            End If
          End If
        Case "et_coddest"
          If oCldGsor.ValCodiceDb(dttET.Rows(0)!et_coddest.ToString, strDittaCorrente, "DESTDIV", "N", StrTmp, dTTmp, lContoCF.ToString) Then
            If dTTmp.Rows.Count > 0 Then
              dttET.Rows(0)!xx_inddestdiv = dTTmp.Rows(0)!dd_inddest.ToString & vbCr & dTTmp.Rows(0)!dd_capdest.ToString & "     " & dTTmp.Rows(0)!dd_locdest.ToString & "     " & dTTmp.Rows(0)!dd_prodest.ToString
              dttET.Rows(0)!xx_teldestdiv = "Tel.              " & dTTmp.Rows(0)!dd_telef.ToString
            Else
              dttET.Rows(0)!xx_inddestdiv = ""
              dttET.Rows(0)!xx_teldestdiv = ""
            End If
          End If
        Case "hh_squadra"
          If dttET.Rows(0)!hh_squadra.ToString <> "" Then
            dttET.Rows(0)!xx_squadra = oClhGsor.CPNEDecodificaSquadra(strDittaCorrente, dttET.Rows(0)!hh_squadra.ToString)
          End If

      End Select
    End If
  End Sub

  Public Overrides Function AfterColUpdate_CORPO_ec_prezzo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dImpIvato As Double = 0
    Dim dAliqIva As Decimal = 0
    Dim bEsente As Boolean
    'If NTSCDec(e.Row!ec_prezzo) <> 0 And NTSCDec(e.Row!ec_preziva) = 0 Then
    If ocldBase.GetSettingBusDitt(strDittaCorrente, "CPNE", "OPZIONI", "OrdCLiFo", "GestioneCorrispIvaEsclusa", "N", " ", "N") = "S" Then
      If dttET.Rows(0)!et_scorpo.ToString = "S" Then
        dAliqIva = CType(oCldGsor, CLHORGSOR).CPNETrovaAliquotaIva(NTSCInt(e.Row!ec_codiva), bEsente)
        If dAliqIva = 0 Then
          e.Row!ec_preziva = e.Row!ec_prezzo
        Else
          If Not bEsente Then
            dImpIvato = NTSCDec(e.Row!ec_prezzo) + ArrDbl((NTSCDec(e.Row!ec_prezzo) * dAliqIva / 100), 2)
            e.Row!ec_preziva = dImpIvato.ToString
          Else
            e.Row!ec_preziva = e.Row!ec_prezzo
          End If
        End If
        Return MyBase.AfterColUpdate_CORPO_ec_prezzo(sender, e)
      Else
        Return True
      End If
    Else
      Return True
    End If
  End Function

  Public Overrides Sub SettaCondCommerciali(bSettaPrezzo As Boolean, bSettaSconti As Boolean, bSettaProvvigioni As Boolean, dtrEC As DataRow, nClascon As Integer, nClscan As Integer, bDaCodart As Boolean)
    MyBase.SettaCondCommerciali(bSettaPrezzo, bSettaSconti, bSettaProvvigioni, dtrEC, nClascon, nClscan, bDaCodart)
    If bSettaPrezzo Then
      Dim dImpIvato As Double = 0
      Dim dAliqIva As Decimal = 0
      Dim bEsente As Boolean
      If ocldBase.GetSettingBusDitt(strDittaCorrente, "CPNE", "OPZIONI", "OrdCLiFo", "GestioneCorrispIvaEsclusa", "N", " ", "N") = "S" Then
        If dttET.Rows(0)!et_scorpo.ToString = "S" Then

          dAliqIva = CType(oCldGsor, CLHORGSOR).CPNETrovaAliquotaIva(NTSCInt(dtrEC!ec_codiva), bEsente)
          If dAliqIva = 0 Then
            dtrEC!ec_preziva = dtrEC!ec_prezzo
          Else
            If Not bEsente Then
              dImpIvato = NTSCDec(dtrEC!ec_prezzo) + ArrDbl((NTSCDec(dtrEC!ec_prezzo) * dAliqIva / 100), 2)
              dtrEC!ec_preziva = dImpIvato.ToString
            Else
              dtrEC!ec_preziva = dtrEC!ec_prezzo
            End If
          End If
        End If
      End If
    End If
  End Sub

  Public Overrides Sub AfterColUpdate_TESTA(sender As Object, e As System.Data.DataColumnChangeEventArgs)
    MyBase.AfterColUpdate_TESTA(sender, e)
    If dttET.Rows(0)!et_serie.ToString <> oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "SerieNC", "R", " ", "R") Then
      Select Case e.Column.ColumnName.ToString.ToLower
        Case "et_conto", "et_coddest", "hh_squadra"
          CPNEAggCampiAggiuntivi(e.Column.ColumnName.ToString.ToLower)
      End Select
    End If
  End Sub
  Public Overrides Sub BeforeColUpdate_TESTA(sender As Object, e As System.Data.DataColumnChangeEventArgs)
    MyBase.BeforeColUpdate_TESTA(sender, e)
    Select Case e.Column.ColumnName.ToString.ToLower
      Case "hh_squadra"
        If oClhGsor.CPNEControlloSquadra(strDittaCorrente, e.ProposedValue.ToString) = False Then
          ThrowRemoteEvent(New NTSEventArgs("", "Il codice squadra è sbagliato"))
          e.ProposedValue = e.Row!hh_squadra
        End If
    End Select
  End Sub

  Public Sub BeforeColUpdate_CORPO_xx_forcod(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs)
    Dim dTTmp As New DataTable
    Dim StrTmp As String = ""
    If oCldGsor.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "anagra", "N", StrTmp, dTTmp) Then
      If dTTmp.Rows.Count > 0 Then
        If dTTmp.Rows(0)!an_tipo.ToString = "F" Then
          e.Row!xx_fordes = StrTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", "Il codice inserito è un Cliente"))
          e.ProposedValue = e.Row!xx_forcod
          'e.Row!xx_fordes = ""
        End If

      End If


    Else
      ThrowRemoteEvent(New NTSEventArgs("", "Il codice fornitore è sbagliato"))
      e.ProposedValue = e.Row!xx_forcod
    End If

  End Sub
  Public Sub BeforeColUpdate_CORPO_xx_forartcod(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs)
    If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then e.ProposedValue = e.ProposedValue.ToString.ToUpper
    Dim dTTmp As New DataTable
    Dim StrTmp As String = ""
    If e.ProposedValue.ToString <> "" Then
      If oCldGsor.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ARTICO", "S", StrTmp, dTTmp) Then

        e.Row!xx_forartdes = StrTmp
        e.Row!xx_forartdesint = dTTmp.Rows(0)!ar_desint
        e.Row!xx_fornote = dTTmp.Rows(0)!ar_note

        If CInt(dTTmp.Rows(0)!ar_forn) <> 0 Then
          e.Row!xx_forcod = dTTmp.Rows(0)!ar_forn
        Else
          If CInt(dTTmp.Rows(0)!ar_forn2) <> 0 Then
            e.Row!xx_forcod = dTTmp.Rows(0)!ar_forn2
          End If
        End If

        If e.Row!xx_forcod.ToString = "0" Then
          If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "ImpostaFornDaRigaPrec", "S", " ", "S") = "S" Then
            If CInt(DTCPNE_OF.Rows.Count) > 0 Then
              e.Row!xx_forcod = DTCPNE_OF.Rows(DTCPNE_OF.Rows.Count - 1)!xx_cpneofconto
            End If
          End If
        End If

      Else
        ThrowRemoteEvent(New NTSEventArgs("", "Il codice articolo è sbagliato"))
        e.ProposedValue = e.Row!xx_forartcod
      End If
    End If

  End Sub
  Public Overrides Sub AfterColUpdate_CORPO(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs)
    bHasChangesET = True
    e.Row.EndEdit()
    e.Row.EndEdit()


    MyBase.AfterColUpdate_CORPO(sender, e)
    If intColonneCorpo > 0 Then
      If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "RigaSingolaCliFor", "N", " ", "N") = "S" Then
        Select Case e.Column.ColumnName.ToLower
          Case "ec_codart"
            e.Row!xx_forartcod = e.Row!ec_codart
          Case "ec_descr"
            e.Row!xx_forartdes = e.Row!ec_descr
          Case "ec_desint"
            e.Row!xx_forartdesint = e.Row!ec_desint
          Case "ec_note"
            e.Row!xx_fornote = e.Row!ec_note
          Case "ec_colli"
            e.Row!xx_forcolli = e.Row!ec_colli
          Case "ec_quant"
            e.Row!xx_forquant = e.Row!ec_quant
            If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "RiportaDatiCliInRigaSotto", "N", " ", "N") = "S" Then
              If bNew = False Then
                CPNEAggiornaRigaOfDaOp(e.Row, "ec_quant")
              End If
            End If
          Case "ec_prezzo"
            If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "RiportaPrezzoCliInPrezzoFor", "N", " ", "N") = "S" Then
              If bBloccaPrezzoFornitore = False Then
                If dttET.Rows(0)!Et_scorpo.ToString = "S" Then
                  e.Row!xx_forprezzo = e.Row!ec_preziva
                Else
                  e.Row!xx_forprezzo = e.Row!ec_prezzo
                End If
              End If
            End If
          Case "xx_forartcod"
            If e.Row!xx_forartcod.ToString <> "" Then
              CPNEForCalcolaPrezzoSconti(e.Row)
            End If
          Case "xx_forcod"
            If CInt(e.Row!xx_forcod) > 0 Then
              If IsDBNull(e.Row!xx_forriga) Then e.Row!xx_forriga = 0
              If e.Row!xx_forriga.ToString = "" Or CInt(e.Row!xx_forriga.ToString) = 0 Then
                Dim intRigaTop As Integer
                For xx = 0 To DTCPNE_OF.Rows.Count - 1
                  If CInt(DTCPNE_OF.Rows(xx)!mo_riga) > intRigaTop Then
                    intRigaTop = CInt(DTCPNE_OF.Rows(xx)!mo_riga)
                  End If
                Next
                e.Row!xx_forriga = intRigaTop + 1
                Dim strForCod As String = e.Row!xx_forcod.ToString
                e.Row!xx_forartcod = e.Row!ec_codart
                e.Row!xx_forartdes = e.Row!ec_descr
                e.Row!xx_forartdesint = e.Row!ec_desint
                e.Row!xx_fornote = e.Row!ec_note
                e.Row!xx_forcolli = e.Row!ec_colli
                e.Row!xx_forquant = e.Row!ec_quant
                If dttET.Rows(0)!Et_scorpo.ToString = "S" Then
                  e.Row!xx_forprezzo = e.Row!ec_prezziva
                Else
                  e.Row!xx_forprezzo = e.Row!ec_prezzo
                End If

                e.Row!xx_forcod = strForCod
              End If
              CPNEForCalcolaPrezzoSconti(e.Row)
            End If
          Case "xx_forcolli"
            e.Row!xx_forquant = e.Row!xx_forcolli
          Case "xx_forquant", "xx_forprezzo", "xx_forsconto1", "xx_forsconto2", "xx_forsconto3", "xx_forsconto4", "xx_forsconto5", "xx_forsconto6"
            CPNEForCalcolaValoreRigaC(e.Row)

        End Select
      End If
    End If

    Select Case e.Column.ColumnName.ToLower
      Case "ec_prezzo"
        If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "RiportaDatiCliInRigaSotto", "N", " ", "N") = "S" Then
          If bNew = False Then
            If bBloccaPrezzoFornitore = False Then
              CPNEAggiornaRigaOfDaOp(e.Row, "ec_prezzo")
            End If
          End If
        End If
      Case "ec_quant"
        If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "RiportaDatiCliInRigaSotto", "N", " ", "N") = "S" Then
          If bNew = False Then
            CPNEAggiornaRigaOfDaOp(e.Row, "ec_quant")
          End If
        End If
      Case "ec_descr"
        If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "RiportaDatiCliInRigaSotto", "N", " ", "N") = "S" Then
          If bNew = False Then
            CPNEAggiornaRigaOfDaOp(e.Row, "ec_descr")
          End If
        End If
      Case "ec_desint"
        If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "RiportaDatiCliInRigaSotto", "N", " ", "N") = "S" Then
          If bNew = False Then
            CPNEAggiornaRigaOfDaOp(e.Row, "ec_desint")
          End If
        End If
      Case "ec_note"
        If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "RiportaDatiCliInRigaSotto", "N", " ", "N") = "S" Then
          If bNew = False Then
            CPNEAggiornaRigaOfDaOp(e.Row, "ec_note")
          End If
        End If
    End Select

  End Sub
  Private Sub CPNEForCalcolaPrezzoSconti(ByRef drxxx As DataRow)
    Dim DT As New DataTable
    If oCldGsor.ValCodiceDb(drxxx!xx_forartcod.ToString, strDittaCorrente, "artico", "s", "", DT) Then
      If DT.Rows.Count > 0 Then
        With DT.Rows(0)
          ' prezzo
          Dim dPrezzo As Decimal = 0
          Dim dPrelist As Decimal = 0
          Dim nPromo As Integer = 0
          Dim strPrzNet As String = ""
          Dim dDaQuantOut As Decimal = 0
          Dim dAQuantOut As Decimal = 0
          Dim dPerqtaOut As Decimal = 0
          Dim strUnmisOut As String = ""
          Dim nCodTpro As Integer = 0
          Dim nTmp As Integer = 0

          If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "RiportaPrezzoCliInPrezzoFor", "N", " ", "N") = "N" Then


            If CType(oCleComm, CLELBMENU).CercaPrezzo(strDittaCorrente, drxxx!xx_forartcod.ToString, 0, NTSCInt(drxxx!xx_forcod), nVisMemNumList,
                          NTSCStr(IIf(bGestionePrezzi, NTSCStr(!ar_unmis), "")),
                          0, "P", True, nCodTpro, 0, NTSCDate(dttET.Rows(0)!et_datdoc),
                          NTSCInt(dttET.Rows(0)!et_valuta), NTSCDec(drxxx!xx_forquant),
                          dPrezzo, dPrelist, nPromo, strPrzNet, nTmp,
                          dDaQuantOut, dAQuantOut, dPerqtaOut, strUnmisOut, strTipovalOut) Then
              drxxx!xx_forprezzo = dPrezzo

            End If
          End If

          ' sconti

          Dim dScont1 As Decimal = 0
          Dim dScont2 As Decimal = 0
          Dim dScont3 As Decimal = 0
          Dim dScont4 As Decimal = 0
          Dim dScont5 As Decimal = 0
          Dim dScont6 As Decimal = 0

          If CType(oCleComm, CLELBMENU).CercaSconti(strDittaCorrente, drxxx!xx_forartcod.ToString, NTSCInt(drxxx!xx_forcod), CInt(!ar_clascon), nClscan, "P",
                              True, nCodTpro, NTSCDate(dttET.Rows(0)!et_datdoc), NTSCDec(drxxx!xx_forquant), dScont1, dScont2,
                              dScont3, dScont4, dScont5, dScont6, nPromo, "N", nScperqta) Then
            drxxx!xx_forsconto1 = dScont1
            drxxx!xx_forsconto2 = dScont2
            drxxx!xx_forsconto3 = dScont3
            drxxx!xx_forsconto4 = dScont4
            drxxx!xx_forsconto5 = dScont5
            drxxx!xx_forsconto6 = dScont6
          End If

        End With
      End If
    End If
  End Sub
  Private Sub CPNEForCalcolaValoreRigaC(ByRef drxxx As DataRow)
    Dim DT As New DataTable
    Dim dImponibile As Decimal
    If oCldGsor.ValCodiceDb(drxxx!xx_forartcod.ToString, strDittaCorrente, "artico", "s", "", DT) Then
      If DT.Rows.Count > 0 Then
        With DT.Rows(0)
          If drxxx!xx_forprezzo.ToString = "" Then drxxx!xx_forprezzo = 0
          dImponibile = CDec(drxxx!xx_forprezzo)

          drxxx!xx_forvalriga = ArrDbl(NTSCDec(dImponibile) * NTSCDec(drxxx!xx_forquant) /
                                                NTSCDec(!ar_perqta) * (100 - NTSCDec(drxxx!xx_forsconto1)) / 100 *
                                                (100 - NTSCDec(drxxx!xx_forsconto2)) / 100 *
                                                (100 - NTSCDec(drxxx!xx_forsconto3)) / 100 *
                                                (100 - NTSCDec(drxxx!xx_forsconto4)) / 100 *
                                                (100 - NTSCDec(drxxx!xx_forsconto5)) / 100 *
                                                (100 - NTSCDec(drxxx!xx_forsconto6)) / 100, 2)
        End With
      End If
    End If
  End Sub

  Private Sub CPNEForCalcolaValoreRigaF(ByRef drxxx As DataRow)
    Dim DT As New DataTable
    If oCldGsor.ValCodiceDb(drxxx!mo_codart.ToString, strDittaCorrente, "artico", "s", "", DT) Then
      If DT.Rows.Count > 0 Then
        With DT.Rows(0)

          drxxx!mo_valoremm = ArrDbl(NTSCDec(drxxx!mo_prezzo) * NTSCDec(drxxx!mo_quant) /
                                              NTSCDec(!ar_perqta) * (100 - NTSCDec(drxxx!mo_scont1)) / 100 *
                                              (100 - NTSCDec(drxxx!mo_scont2)) / 100 *
                                              (100 - NTSCDec(drxxx!mo_scont3)) / 100 *
                                              (100 - NTSCDec(drxxx!mo_scont4)) / 100 *
                                              (100 - NTSCDec(drxxx!mo_scont5)) / 100 *
                                              (100 - NTSCDec(drxxx!mo_scont6)) / 100, 2)

        End With
      End If
    End If
  End Sub
  Public Overrides Function RecordSalva(ByVal nRow As Integer, ByVal bDelete As Boolean, ByRef dtrDeleted As System.Data.DataRow) As Boolean
    Dim DecQtaEva As Decimal = 0
    Dim strFlevas As String = "C"
    If bInUnload Then Return False
    If bDelete = False And oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "RigaSingolaCliFor", "N", " ", "N") = "S" Then
      If nRow <> -1 Then
        If bNew Then
          If intColonneCorpo > 0 Then
            If CInt(dttEC.Rows(nRow)!xx_forcod) > 0 Or dttEC.Rows(nRow)!xx_forartcod.ToString <> "" Then
              If CInt(dttEC.Rows(nRow)!xx_forcod) = 0 Then
                If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "RigaSingolaCliForNoMsgMancaFornEArt", "S", " ", "S") = "N" Then
                  ThrowRemoteEvent(New NTSEventArgs("", "Manca il codice fornitore"))
                  Return False
                End If

              End If
              If dttEC.Rows(nRow)!xx_forartcod.ToString = "" Then
                If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "RigaSingolaCliForNoMsgMancaFornEArt", "S", " ", "S") = "N" Then
                  ThrowRemoteEvent(New NTSEventArgs("", "Manca il codice articolo"))
                  Return False
                End If
              End If
            End If
          End If
        End If
      End If
    End If

    If nRow < 0 OrElse dttEC.Rows.Count = 0 Then Return True
    'attenzione: in fase di cancellazione riga passa di qui 2 volte: la prima per salvare la riga, la seconda per cancellarla!!!!!
    If nRow > dttEC.Rows.Count Then Return False
    Dim DrEc As DataRow = dttEC.Rows(nRow)
    Dim IntStato As Integer = DrEc.RowState
    RecordSalva = MyBase.RecordSalva(nRow, bDelete, dtrDeleted)
    If RecordSalva Then
      If IntStato = DataRowState.Added Then
        If CInt(DrEc!ec_oqanno) <> 0 Then
          DrMovord = DrEc
          Dim DtRigheDaEl As DataTable = oClhGsor.CPNELeggihhmovoffOf(strDittaCorrente, DrEc!ec_oqtipo.ToString, CInt(DrEc!ec_oqanno), DrEc!ec_oqserie.ToString, CInt(DrEc!ec_oqnum), CInt(DrEc!ec_oqvers), CInt(DrEc!ec_oqriga))
          For i = 0 To DtRigheDaEl.Rows.Count - 1
            Dim drRigheDaEl As DataRow = DtRigheDaEl.Rows(i)
            DTCPNE_OF.Rows.Add()
            Dim DrCPNE_OF As DataRow = DTCPNE_OF.Rows(DTCPNE_OF.Rows.Count - 1)
            'DrCPNE_OF!mo_commeca = 
            'DrCPNE_OF!mo_subcommeca =

            DrCPNE_OF!xx_cpneofconto = drRigheDaEl!hh_conto
            DrCPNE_OF!mo_codart = drRigheDaEl!hh_codart
            DrCPNE_OF!mo_descr = drRigheDaEl!hh_descr
            DrCPNE_OF!mo_desint = drRigheDaEl!hh_desint
            DrCPNE_OF!mo_note = drRigheDaEl!hh_note
            DrCPNE_OF!mo_quant = drRigheDaEl!hh_quant
            DrCPNE_OF!mo_scont1 = drRigheDaEl!hh_scont1
            DrCPNE_OF!mo_scont2 = drRigheDaEl!hh_scont2
            DrCPNE_OF!mo_scont3 = drRigheDaEl!hh_scont3
            DrCPNE_OF!mo_scont4 = drRigheDaEl!hh_scont4
            DrCPNE_OF!mo_scont5 = drRigheDaEl!hh_scont5
            DrCPNE_OF!mo_scont6 = drRigheDaEl!hh_scont6
            DrCPNE_OF!mo_prezzo = drRigheDaEl!hh_prezzo
            'DrCPNE_OF!mo_valoremm =
          Next
        End If
      End If
    End If
  End Function

  Private Sub BeforeColUpdate_CPNE_OF(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If e.Row!codditt.ToString = ".," And e.Column.ColumnName.ToUpper <> "CODDITT" Then

        If e.Row.Table.Select("mo_subcommeca = " & DrMovord!ec_subcommeca.ToString).Length > 0 And bCPNEUnaSolaRiga Then
            e.Row!codditt = "dacancellare!!!!"
            Return
          Else
            e.Row!codditt = "."
            e.Row!td_tipork = " "
            e.Row!td_anno = 0
            e.Row!td_serie = " "
            e.Row!td_numord = 0
            e.Row!xx_cpneofconto = "0"
            e.Row!an_descr1 = ""
            If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "ImpostaFornDaRigaPrec", "S", " ", "S") = "S" Then
              Dim Drs As DataRow() = DTCPNE_OF.Select("mo_subcommeca = " & DrMovord!ec_subcommeca.ToString, "mo_riga")
              If Drs.Length > 1 Then
                e.Row!xx_cpneofconto = Drs(Drs.Length - 2)!xx_cpneofconto
              End If
            End If
            If DTCPNE_OF.Rows.Count = 1 Then
              e.Row!mo_riga = 1
            Else
              e.Row!mo_riga = CInt(DTCPNE_OF.Select("", "mo_riga desc")(1)!mo_riga) + 1
            End If
            e.Row!mo_codart = ""
            e.Row!mo_descr = ""
            e.Row!mo_desint = ""
            If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "ImpostaArtForn", "S", " ", "S") = "S" Then
              e.Row!mo_codart = oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "ImpostaArtFornCodArt", "D", " ", "D")
            End If
            e.Row!mo_quant = 0
            e.Row!mo_prezzo = 0
            e.Row!mo_scont1 = 0
            e.Row!mo_scont2 = 0
            e.Row!mo_scont3 = 0
            e.Row!mo_scont4 = 0
            e.Row!mo_scont5 = 0
            e.Row!mo_scont6 = 0
            e.Row!mo_quaeva = 0
            e.Row!mo_flevas = "C"
            If Not IsNothing(DrMovord) Then
              e.Row!mo_commeca = DrMovord!ec_commeca
              e.Row!mo_subcommeca = DrMovord!ec_subcommeca
            End If
            e.Row!codditt = strDittaCorrente
          End If
        End If
        Dim dTTmp As New DataTable
      Dim StrTmp As String = ""
      Select Case e.Column.ColumnName.ToLower
        Case "xx_cpneofconto"
          If CInt(e.Row!td_numord) = 0 Then
            If oCldGsor.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "anagra", "N", StrTmp, dTTmp) Then
              If dTTmp.Rows.Count > 0 Then
                If dTTmp.Rows(0)!an_tipo.ToString = "F" Then
                  e.Row!an_descr1 = StrTmp
                Else
                  ThrowRemoteEvent(New NTSEventArgs("", "Il codice inserito è un Cliente"))
                  e.ProposedValue = e.Row!xx_cpneofconto
                End If
              Else
                e.Row!an_descr1 = StrTmp
              End If
            Else
              ThrowRemoteEvent(New NTSEventArgs("", "Il conto è sbagliato"))
              e.ProposedValue = e.Row!xx_cpneofconto
            End If
          Else
            ThrowRemoteEvent(New NTSEventArgs("", "La riga è stata già salvata." & vbCrLf & "Non è possibile cambiare il conto"))
            e.ProposedValue = e.Row!xx_cpneofconto
          End If
        Case "mo_codart"
          If oCldGsor.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ARTICO", "S", StrTmp, dTTmp) Then
            If e.ProposedValue.ToString = "" Then
              e.Row!mo_descr = ""
              e.Row!mo_desint = ""
            Else
              e.Row!mo_descr = dTTmp.Rows(0)!ar_descr
              e.Row!mo_desint = dTTmp.Rows(0)!ar_desint
              If e.Row!mo_quant.ToString <> "" Then
                CPNEAggiornaPrzScon(e.ProposedValue.ToString, CDec(e.Row!mo_quant), e.Row)
              End If
            End If
          Else
            ThrowRemoteEvent(New NTSEventArgs("", "Il codice articolo è sbagliato"))
            e.ProposedValue = ""
          End If
        Case "mo_quant"
          '============= 21 05 2019 tolto per richiesta di ALBERTO MONTRESOR, alla modifica della q.tà non azzera il prezzo.
          'CPNEAggiornaPrzScon(e.Row!mo_codart.ToString, CDec(e.ProposedValue), e.Row)
      End Select

    Catch ex As Exception
      '--------------------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '--------------------------------------------------------------
    End Try
  End Sub
  Private Sub CPNEAggiornaPrzScon(ByVal StrCodart As String, ByVal DecQta As Decimal, ByVal dr As DataRow)
    Dim dPrezzo As Double = 0
    Dim dScont1 As Decimal = 0
    Dim dScont2 As Decimal = 0
    Dim dScont3 As Decimal = 0
    Dim dScont4 As Decimal = 0
    Dim dScont5 As Decimal = 0
    Dim dScont6 As Decimal = 0
    Dim nPromo As Integer = 0
    Dim nCodtpro As Integer = 0
    Dim StrNet As String = "M"
    Dim dttAna As New DataTable
    Dim dttArt As New DataTable
    If StrCodart = "" Then
      Return
    End If
    If CInt(dr!xx_cpneofconto) = 0 Then
      Return
    End If
    oCldGsor.ValCodiceDb(StrCodart, strDittaCorrente, "ARTICO", "S", " ", dttArt)

    oCldGsor.ValCodiceDb(dr!xx_cpneofconto.ToString, strDittaCorrente, "ANAGRA", "N", " ", dttAna)
    Dim nClascon As Integer = CInt(dttArt.Rows(0)!ar_clascon)
    Dim nClscan As Integer = NTSCInt(dttAna.Rows(0)!an_clascon)

    CType(oCleComm, CLELBMENU).CercaPrezzo(strDittaCorrente, NTSCStr(StrCodart), 0, CInt(dr!xx_cpneofconto),
                           CInt(dttAna.Rows(0)!an_listino), dttArt.Rows(0)!ar_unmis.ToString, 0, "P", True, 0, 0,
                           NTSCDate(dttET.Rows(0)!et_datdoc), 0,
                           NTSCDec(DecQta), CDec(dPrezzo), 0, 0, "")
    dr!mo_prezzo = dPrezzo
    If CType(oCleComm, CLELBMENU).CercaSconti(strDittaCorrente, StrCodart, NTSCInt(dr!xx_cpneofconto), nClascon, nClscan, "P",
                  True, nCodtpro, NTSCDate(dttET.Rows(0)!et_datdoc), DecQta, dScont1, dScont2,
                  dScont3, dScont4, dScont5, dScont6, nPromo, StrNet, nScperqta) Then
      dr!mo_scont1 = dScont1
      dr!mo_scont2 = dScont2
      dr!mo_scont3 = dScont3
      dr!mo_scont4 = dScont4
      dr!mo_scont5 = dScont5
      dr!mo_scont6 = dScont6
    End If
  End Sub
  Private Sub AfterColUpdate_CPNE_OF(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      bHasChangesET = True
      e.Row.EndEdit()
      e.Row.EndEdit()
      If e.Row!codditt.ToString = "dacancellare!!!!" And e.Column.ColumnName <> "codditt" Then
        ThrowRemoteEvent(New NTSEventArgs("", "E' possibile inserire solo una riga per ogni riga impegno cliente"))
        e.Row.Delete()
      End If
      Select Case e.Column.ColumnName.ToLower
        Case "mo_quant", "mo_prezzo", "mo_scont1", "mo_scont2", "mo_scont3", "mo_scont4", "mo_scont5", "mo_scont6"

          CPNEForCalcolaValoreRigaF(e.Row)

      End Select
    Catch ex As Exception
      '--------------------------------------------------------------

      CLN__STD.GestErr(ex, Me, "")

      '--------------------------------------------------------------
    End Try
  End Sub
  Private Sub AfterColUpdate_CPNEScaPag(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      bHasChangesET = True
      e.Row.EndEdit()
      e.Row.EndEdit()
      Select Case e.Column.ColumnName.ToLower
        Case "hh_numft"
          If CInt(e.Row!hh_numft) <> 0 Then
            e.Row!hh_flgft = "F"
          Else
            e.Row!hh_flgft = ""
          End If
        Case "hh_codpaga"
          If IsDBNull(e.Row!hh_imppaga) Then
            e.Row!hh_imppaga = 0
          End If
          If IsDBNull(e.Row!hh_datpaga) Then
            e.Row!hh_datpaga = "01/01/1900"
          End If
          If IsDBNull(e.Row!hh_flgpaga) Then
            e.Row!hh_flgpaga = 1
          End If
          If IsDBNull(e.Row!hh_numft) Then
            e.Row!hh_numft = 0
          End If
          If IsDBNull(e.Row!hh_dataft) Then
            e.Row!hh_datpaga = "01/01/1900"
          End If
          If IsDBNull(e.Row!hh_flgft) Then
            e.Row!hh_flgft = ""
          End If
        Case "hh_imppaga"
          CPNEAggiornaResiduoOrdine()
          If IsDBNull(e.Row!hh_datpaga) Then
            e.Row!hh_datpaga = "01/01/1900"
          End If
          If IsDBNull(e.Row!hh_flgpaga) Then
            e.Row!hh_flgpaga = 1
          End If
          If IsDBNull(e.Row!hh_numft) Then
            e.Row!hh_numft = 0
          End If
          If IsDBNull(e.Row!hh_dataft) Then
            e.Row!hh_dataft = "01/01/1900"
          End If
          If IsDBNull(e.Row!hh_flgft) Then
            e.Row!hh_flgft = ""
          End If
        Case "hh_flgpaga"
          If Not IsDBNull(e.Row!hh_flgpaga) Then
            If e.Row!hh_flgpaga.ToString = "3" Then
              If IsDBNull(e.Row!hh_autft) Then e.Row!hh_autft = ""
              If e.Row!hh_autft.ToString <> "A" Then
                If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "FtAnticipo", "S", " ", "S") = "S" Then
                  Dim evnt As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, "Si vuole creare la fattura di anticipo?")
                  ThrowRemoteEvent(evnt)
                  If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
                    Dim evnt2 As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, "La data di emissione è quella odierna?")
                    ThrowRemoteEvent(evnt2)
                    If evnt2.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
                      dtDataDoc = CDate(Now)
                    Else
                      If IsDBNull(e.Row!hh_dataft) Then
                        ThrowRemoteEvent(New NTSEventArgs("", "Inserire la data della fattura"))
                        e.Row!hh_flgpaga = 1
                        Return

                      ElseIf CDate(e.Row!hh_dataft) = #01/01/1900# Then
                        ThrowRemoteEvent(New NTSEventArgs("", "Inserire la data della fattura"))
                        e.Row!hh_flgpaga = 1
                        Return
                      Else
                        dtDataDoc = CDate(e.Row!hh_dataft)
                      End If
                    End If
                    If IsDBNull(e.Row!hh_imppaga) Then
                      ThrowRemoteEvent(New NTSEventArgs("", "Inserire l'importo"))
                      e.Row!hh_flgpaga = 1
                      Return
                    ElseIf CDec(e.Row!hh_imppaga) = 0 Then
                      ThrowRemoteEvent(New NTSEventArgs("", "Inserire l'importo"))
                      e.Row!hh_flgpaga = 1
                      Return
                    End If
                    decPrezzoDoc = CDec(e.Row!hh_imppaga)
                    If CPNECreaFatturaAnticipo() Then
                      e.Row!hh_annoft = nAnnoDoc
                      e.Row!hh_serieft = strSerieDoc
                      e.Row!hh_numft = lNumTmpDoc
                      e.Row!hh_dataft = dtDataDoc
                      e.Row!hh_flgft = "F"
                      e.Row!hh_autft = "A"

                      dsShared.Tables("CPNE.ScaPag").AcceptChanges()

                      Dim xx As Integer
                      For xx = 0 To dsShared.Tables("CPNE.ScaPag").Rows.Count - 1
                        If CInt(dsShared.Tables("CPNE.ScaPag").Rows(xx)!hh_riga) = CInt(e.Row!hh_riga) Then
                          Exit For
                        End If
                      Next
                      RecordSalva(xx, True, Nothing)
                      dsShared.Tables("CPNE.ScaPag").AcceptChanges()
                      oClhGsor.AggiornaDatiFtScadPag(strDittaCorrente, dttET.Rows(0)!et_tipork.ToString, CInt(dttET.Rows(0)!et_anno.ToString), dttET.Rows(0)!et_serie.ToString, CInt(dttET.Rows(0)!et_numdoc.ToString), CInt(e.Row!hh_riga), CInt(nAnnoDoc), strSerieDoc, lNumTmpDoc, dtDataDoc)
                      Dim strMsg = "E' stata creata la fattura N. " & lNumTmpDoc
                      If strSerieDoc <> " " Then
                        strMsg += "/" & strSerieDoc
                      End If
                      ThrowRemoteEvent(New NTSEventArgs("", "E' stata creata la fattura N. " & lNumTmpDoc))
                      ' stampa
                      ThrowRemoteEvent(New NTSEventArgs("CPNE.Stampa", "FI"))
                    End If
                  End If
                End If
              End If
            End If
          End If
      End Select

    Catch ex As Exception
      '--------------------------------------------------------------

      CLN__STD.GestErr(ex, Me, "")

      '--------------------------------------------------------------
    End Try
  End Sub
  Private Sub BeforeColUpdate_CPNEScaPag(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If bModificaForzata Then Exit Sub
      If e.Row!hh_riga.ToString = "" Then
        If e.Column.ColumnName.ToLower <> "hh_riga" Then

          If dsShared.Tables("CPNE.ScaPag").Rows.Count > 1 Then
            e.Row!hh_riga = dsShared.Tables("CPNE.ScaPag").Rows.Count + 1
          Else
            e.Row!hh_riga = 1
          End If
        End If
      End If
      Select Case e.Column.ColumnName.ToLower
        Case "hh_codpaga"
          Dim dTTmp As New DataTable
          Dim StrTmp As String = ""
          If oCldGsor.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABPAGA", "N", StrTmp, dTTmp) Then
            e.Row!xx_despaga = StrTmp

          Else
            ThrowRemoteEvent(New NTSEventArgs("", "Il codice pagamento è sbagliato"))
            e.ProposedValue = e.Row!hh_codpaga
          End If
        Case "hh_flgpaga"
          If Not IsDBNull(e.Row!hh_flgpaga) Then
            If e.Row!hh_flgpaga.ToString <> "" Then
              If e.Row!hh_flgpaga.ToString = "3" Then
                If e.ProposedValue.ToString <> "3" Then
                  Dim strMsg As String
                  strMsg = "Si vogliono eliminare i riferimenti di fattura automatica?"
                  Dim evnt As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, strMsg)
                  ThrowRemoteEvent(evnt)
                  If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
                    If e.Row!hh_serieft.ToString <> "" And e.Row!hh_serieft.ToString <> " " Then
                      ThrowRemoteEvent(New NTSEventArgs("", "Ricordarsi di cancellare la fattura N. " & e.Row!hh_numft.ToString & "/" & e.Row!hh_serieft.ToString & " del " & Format(e.Row!hh_dataft, "dd/MM/yyyy")))
                    Else
                      ThrowRemoteEvent(New NTSEventArgs("", "Ricordarsi di cancellare la fattura N. " & e.Row!hh_numft.ToString & " del " & Format(e.Row!hh_dataft, "dd/MM/yyyy")))
                    End If
                    e.Row!hh_annoft = 0
                    e.Row!hh_serieft = ""
                    e.Row!hh_numft = 0
                    e.Row!hh_dataft = "01/01/1900"
                    e.Row!hh_flgft = ""
                    e.Row!hh_autft = ""
                    dsShared.Tables("CPNE.ScaPag").AcceptChanges()
                    Dim xx As Integer
                    For xx = 0 To dsShared.Tables("CPNE.ScaPag").Rows.Count - 1
                      If CInt(dsShared.Tables("CPNE.ScaPag").Rows(xx)!hh_riga) = CInt(e.Row!hh_riga) Then
                        Exit For
                      End If
                    Next
                    RecordSalva(xx, True, Nothing)
                    dsShared.Tables("CPNE.ScaPag").AcceptChanges()
                    oClhGsor.AnnullaDatiFtScadPag(strDittaCorrente, dttET.Rows(0)!et_tipork.ToString, CInt(dttET.Rows(0)!et_anno.ToString), dttET.Rows(0)!et_serie.ToString, CInt(dttET.Rows(0)!et_numdoc.ToString), CInt(e.Row!hh_riga))
                  Else
                    e.ProposedValue = e.Row!hh_flgpaga
                  End If
                End If
              End If
            End If
          End If
          If e.Row!hh_codpaga.ToString <> "" Or e.Row!hh_imppaga.ToString <> "" Then
            If IsDBNull(e.ProposedValue) Then
              ThrowRemoteEvent(New NTSEventArgs("", "Il campor 'FLG(*)' come da Legenda accetta solo i valori 1,2,3"))
              e.ProposedValue = e.Row!hh_flgpaga
            ElseIf e.ProposedValue.ToString = "" Then
              ThrowRemoteEvent(New NTSEventArgs("", "Il campor 'FLG(*)' come da Legenda accetta solo i valori 1,2,3"))
              e.ProposedValue = e.Row!hh_flgpaga
            ElseIf CInt(e.ProposedValue.ToString) < 1 Or CInt(e.ProposedValue.ToString) > 3 Then
              ThrowRemoteEvent(New NTSEventArgs("", "Il campor 'FLG(*)' come da Legenda accetta solo i valori 1,2,3"))
              e.ProposedValue = e.Row!hh_flgpaga
            End If
          End If
        Case "hh_flgft"
          If e.Row!hh_numft.ToString <> "" And e.Row!hh_numft.ToString <> "0" Then
            e.ProposedValue = UCase(e.ProposedValue.ToString)
            If IsDBNull(e.ProposedValue) Then
              ThrowRemoteEvent(New NTSEventArgs("", "Il campor 'FLG(**)' come da Legenda accetta solo i valori F,P"))
              e.ProposedValue = e.Row!hh_flgft
            ElseIf e.ProposedValue.ToString <> "F" And e.ProposedValue.ToString <> "P" Then
              ThrowRemoteEvent(New NTSEventArgs("", "Il campor 'FLG(**)' come da Legenda accetta solo i valori F,P"))
              e.ProposedValue = e.Row!hh_flgft
            End If
          Else
            If CInt(e.Row!hh_numft) = 0 Then
              e.ProposedValue = ""
            Else
              e.ProposedValue = e.Row!hh_flgft
            End If
          End If
      End Select
    Catch ex As Exception
      '--------------------------------------------------------------

      CLN__STD.GestErr(ex, Me, "")

      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overrides Function RecordRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    Dim bAdded As Boolean = dttEC.Rows(nRow).RowState = DataRowState.Added
    Return MyBase.RecordRipristina(nRow, strFilter)
    If bAdded Then
      DrMovord = Nothing
    End If
  End Function

  Public Overrides Sub CorpoOnAddNewRow(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs)
    MyBase.CorpoOnAddNewRow(sender, e)
    If intColonneCorpo > 0 Then
      If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "RigaSingolaCliFor", "N", " ", "N") = "S" Then
        With e.Row
          Dim intRigaTop As Integer
          For xx = 0 To DTCPNE_OF.Rows.Count - 1
            If CInt(DTCPNE_OF.Rows(xx)!mo_riga) > intRigaTop Then
              intRigaTop = CInt(DTCPNE_OF.Rows(xx)!mo_riga)
            End If
          Next
          !xx_forriga = intRigaTop + 1
          !xx_forcod = 0
          !xx_forcolli = 0
          !xx_forquant = 0
          !xx_forprezzo = 0
          !xx_forsconto1 = 0
          !xx_forsconto2 = 0
          !xx_forsconto3 = 0
          !xx_forsconto4 = 0
          !xx_forsconto5 = 0
          !xx_forsconto6 = 0
          !xx_forvalriga = 0
        End With
      End If
    End If
    If e.Row!ec_commeca.ToString <> "" And e.Row!ec_commeca.ToString <> "0" And dttET.Rows(0)!et_tipork.ToString = "R" Then
      oClhGsor.GeneraSottoCommessa(CInt(e.Row!ec_commeca), CInt(e.Row!ec_riga), strDittaCorrente)
      e.Row!ec_subcommeca = e.Row!ec_riga
    End If
  End Sub

  Private Sub CPNEAggiornaRigaOfDaOp(Dr As DataRow, StrCampoOri As String)
    If Not IsNothing(DTCPNE_OF) Then
      Dim drsCPNE_OF As DataRow() = DTCPNE_OF.Select("mo_subcommeca = " & Dr!ec_subcommeca.ToString)
      If drsCPNE_OF.Length > 0 Then
        drsCPNE_OF(0).Item("mo" & Mid(StrCampoOri, 3)) = Dr.Item(StrCampoOri)
      End If
    End If

  End Sub

  Public Overrides Function SalvaOrdine(ByVal strState As String) As Boolean
    Try
      If dttET.Rows(0)!et_tipork.ToString = "R" And dttET.Rows(0)!et_serie.ToString <> oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "SerieNC", "R", " ", "R") Then
        ' controllo che la data di consegna della testata non sia inferiore alla data dell'IC + i gg dell'opzione di registro per l'OF
        Dim dtDataCalcolo As Date = DateAdd(DateInterval.Day, 1 * CDec(oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "GiorniDtConsAntTesta", "60", " ", "60")), CDate(dttET.Rows(0)!et_datdoc))
        If CDate(dttET.Rows(0)!et_datcons) < dtDataCalcolo Then
          ThrowRemoteEvent(New NTSEventArgs("", "La data di consegna non è corretta. Data calcolata: " & dtDataCalcolo & " (data Ordine " & Format(dttET.Rows(0)!et_datdoc, "dd/MM/yyyy") & " gg " & oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "GiorniDtConsAntTesta", "60", " ", "60") & ")"))
          Return False
        End If
        ' controllo codice pagamento
        If dttEC.Rows.Count > 0 Then
          If CInt(dttET.Rows(0)!et_codpaga) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", "Indicare un codice di pagamento diverso da 0 prima di salvare."))
            Return False
          End If
        End If
        ' controllo che la data di consegna delle righe non sia inferiore alla data dell'IC + i gg dell'opzione di registro per l'OF
        If dttEC.Rows.Count > 0 Then
          For i = 0 To dttEC.Rows.Count - 1
            dtDataCalcolo = DateAdd(DateInterval.Day, 1 * CInt(oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "GiorniDtConsAntCorpo", "10", " ", "10")), CDate(dttET.Rows(0)!et_datdoc))
            If CDate(dttEC.Rows(i)!ec_datcons) < dtDataCalcolo Then
              ThrowRemoteEvent(New NTSEventArgs("", "La data di consegna della riga '" & dttEC.Rows(i)!ec_riga.ToString & "  non è corretta. Data calcolata: " & Format(dtDataCalcolo, "dd/MM/yyyy") & " (data Ordine " & Format(dttET.Rows(0)!et_datdoc, "dd/MM/yyyy") & " gg " & oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "GiorniDtConsAntCorpo", "10", " ", "10") & ")"))
              Return False
            End If
          Next
        End If
        Dim bEvase As Boolean = False
        Dim intRigheEvase As Integer = 0
        If strState = "D" Then
          ' controllo righe ord for se evase
          For i = 0 To DTCPNE_OF.Rows.Count - 1
            If DTCPNE_OF.Rows(i)!mo_flevas.ToString = "S" Then
              bEvase = True
              intRigheEvase += 1
              Exit For
            End If
          Next

          If bEvase Then
            Dim strMsg As String
            strMsg = "C'è almeno 1 riga d'ordine fornitore evasa. Si vuole continuare?"
            Dim evnt As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, strMsg)
            ThrowRemoteEvent(evnt)
            If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then
              Return False
            End If
          End If
        End If
      End If

      Dim drt As DataRow = dttET.Rows(0)
      SalvaOrdine = MyBase.SalvaOrdine(strState)
      If drt!et_tipork.ToString = "R" And SalvaOrdine Then
        If Not IsNothing(DTCPNE_OF) Then
          If DTCPNE_OF.Rows.Count > 0 Then
            If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "MessaggioTermineIc", "N", " ", "N") = "S" Then
              ThrowRemoteEvent(New NTSEventArgs("", "Termine Salvataggio impegno cliente"))
            End If
            Dim ds As New DataSet
            Dim Strtipork As String = oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "TiporkOf", "O", " ", "O")
            oClhGsor.CPNEMostrOf(ds, CInt(drt!et_commeca), Strtipork)
            For qq = 0 To DTCPNE_OF.Rows.Count - 1
              With DTCPNE_OF.Rows(qq)
                If CInt(!td_numord) = 0 Then
                  Dim drs As DataRow() = ds.Tables("CPNE_OF").Select("xx_cpneofconto = " & !xx_cpneofconto.ToString)
                  If drs.Length > 0 Then
                    !td_tipork = drs(0)!td_tipork
                    !td_anno = drs(0)!td_anno
                    !td_serie = drs(0)!td_serie
                    !td_numord = drs(0)!td_numord
                  End If
                End If
              End With
            Next
            Dim dtTmp As New DataTable
            dtTmp.Columns.Add("conto", GetType(Integer))
            For qq = 0 To DTCPNE_OF.Rows.Count - 1
              If dtTmp.Select("conto =  " & DTCPNE_OF.Rows(qq)!xx_cpneofconto.ToString).Length = 0 Then
                dtTmp.Rows.Add()
                dtTmp.Rows(dtTmp.Rows.Count - 1)!conto = DTCPNE_OF.Rows(qq)!xx_cpneofconto
              End If
            Next
            For qq = 0 To ds.Tables("CPNE_OF").Rows.Count - 1
              If dtTmp.Select("conto =  " & ds.Tables("CPNE_OF").Rows(qq)!xx_cpneofconto.ToString).Length = 0 Then
                dtTmp.Rows.Add()
                dtTmp.Rows(dtTmp.Rows.Count - 1)!conto = ds.Tables("CPNE_OF").Rows(qq)!xx_cpneofconto
              End If
            Next
            For qq = 0 To dtTmp.Rows.Count - 1
              With dtTmp.Rows(qq)
                SalvaOrdine = CPNEGeneraOf(strState, CInt(!conto), DTCPNE_OF.Select("xx_cpneofconto = " & !conto.ToString), ds.Tables("CPNE_OF").Select("xx_cpneofconto = " & !conto.ToString))
                If SalvaOrdine = False Then
                  Return False
                End If
              End With
            Next

          End If
        End If
        ' gestione tabella pagamenti
        If Not IsNothing(dsShared.Tables("CPNE.ScaPag")) Then
          ' cancella esistente
          oClhGsor.CancellaScadPag(strDittaCorrente, dttET.Rows(0)!et_tipork.ToString, CInt(dttET.Rows(0)!et_anno.ToString), dttET.Rows(0)!et_serie.ToString, CInt(dttET.Rows(0)!et_numdoc.ToString))
          ' crea da datatable
          If strState <> "D" Then
            If dsShared.Tables("CPNE.ScaPag").Rows.Count > 0 Then
              'CPNEAggiornaCampiVuoti("S")
              For xx = 0 To dsShared.Tables("CPNE.ScaPag").Rows.Count - 1
                If IsDBNull(dsShared.Tables("CPNE.ScaPag").Rows(xx)!hh_annoft) Then
                  dsShared.Tables("CPNE.ScaPag").Rows(xx)!hh_annoft = "0"
                End If
                If IsDBNull(dsShared.Tables("CPNE.ScaPag").Rows(xx)!hh_serieft) Then
                  dsShared.Tables("CPNE.ScaPag").Rows(xx)!hh_serieft = " "
                End If
                If IsDBNull(dsShared.Tables("CPNE.ScaPag").Rows(xx)!hh_autft) Then
                  dsShared.Tables("CPNE.ScaPag").Rows(xx)!hh_autft = ""
                End If
                Dim dr As DataRow = dsShared.Tables("CPNE.ScaPag").Rows(xx)

                oClhGsor.CreaScadPag(strDittaCorrente, dttET.Rows(0)!et_tipork.ToString, CInt(dttET.Rows(0)!et_anno.ToString), dttET.Rows(0)!et_serie.ToString, CInt(dttET.Rows(0)!et_numdoc.ToString), CInt(dttET.Rows(0)!et_commeca), dr)
              Next
            End If
          End If
        End If
        If strState <> "D" And dttET.Rows(0)!et_serie.ToString <> oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "SerieNC", "R", " ", "R") Then
          If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "StampaOFAutomatica", "S", " ", "S") = "S" Then
            If DTCPNE_OF.Rows.Count > 0 Then
              If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "StampaOFAutomaticaMSG", "S", " ", "S") = "S" Then
                Dim strMsg As String
                strMsg = "Vuoi stampare l'ordine fornitore?"
                Dim evnt As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, strMsg)
                ThrowRemoteEvent(evnt)
                If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
                  ThrowRemoteEvent(New NTSEventArgs("CPNE.Stampa", "IC"))
                End If
              Else
                ThrowRemoteEvent(New NTSEventArgs("CPNE.Stampa", "IC"))
              End If
            End If
          End If
        End If
      End If
      DrMovord = Nothing
    Catch ex As Exception
      '--------------------------------------------------------------

      CLN__STD.GestErr(ex, Me, "")

      Return False
      '--------------------------------------------------------------
    End Try
  End Function
  Private Sub GestisciEventiEntityBoll(ByVal sender As Object, ByRef e As NTSEventArgs)
    Try
      'gli eventuali messaggi dati da BEVEBOLL tramite la ThrowRemoteEvent li passo a BNVEGNBF
      'solo se non sono messaggi dove viene chiesta una conferma ...
      If InStr(e.Message, "Confermi Prezzo uguale a zero per l'articolo") > 0 Then
        e.RetValue = "YES"
      Else
        ThrowRemoteEvent(e)
      End If

    Catch ex As Exception
      '--------------------------------------------------------------

      CLN__STD.GestErr(ex, Me, "")

      '--------------------------------------------------------------	
    End Try
  End Sub

  Public Function CPNEFtModificabileManuale(ByVal dr As DataRow) As Boolean
    If IsDBNull(dr!hh_numft) Then Return True
    If dr!hh_numft.ToString = "" Then Return True
    If CDec(dr!hh_numft) = 0 Then Return True
    Return False
  End Function
  Public Function CPNEFtModificabile(ByVal dr As DataRow) As Boolean
    If dsShared.Tables("CPNE.ScaPag").Rows.Count > 0 Then
      If IsDBNull(dr!hh_autft) Then Return True
      If dr!hh_autft.ToString = "A" Then
        Return False
      Else
        Return True
      End If
    Else
      Return True
    End If
  End Function
  Private Function CPNEGeneraOf(ByVal strState As String, ByVal IntConto As Integer, ByVal drNew As DataRow(), ByVal drori As DataRow()) As Boolean

    Dim StrTipo As String
    Dim IntAnno As Integer
    Dim StrSerie As String
    Dim IntNumOrd As Integer




    Dim StrStateOrd As String
    Dim dtTmp As New DataTable
    Dim StrTmp As String = ""
    Try
      If drori.Length > 0 Then
        StrTipo = drori(0)!td_tipork.ToString
        IntAnno = CInt(drori(0)!td_anno)
        StrSerie = drori(0)!td_serie.ToString
        IntNumOrd = CInt(drori(0)!td_numord)
      Else
        'StrTipo = "O"
        StrTipo = oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "TiporkOf", "O", " ", "O")
        IntAnno = CInt(dttET.Rows(0)!et_anno)
        StrSerie = oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "SerieOf", dttET.Rows(0)!et_serie.ToString, " ", dttET.Rows(0)!et_serie.ToString)
        IntNumOrd = 0
      End If


      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      Dim oCleOrd As CLEORGSOR
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEVEGNBF", "BEORGSOR", oTmp, strErr, False, "", "") = False Then
        Throw New NTSException(oApp.Tr(Me, 128607611686875000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return False
      End If
      oCleOrd = CType(oTmp, CLEORGSOR)
      '------------------------------------------------
      AddHandler oCleOrd.RemoteEvent, AddressOf GestisciEventiEntityBoll
      If oCleOrd.Init(oApp, oScript, oCleComm, "", False, "", "") = False Then Return False
      If Not oCleOrd.InitExt() Then Return False
      Dim dsBoll As New DataSet
      oCleOrd.ApriOrdine(strDittaCorrente, True, "B", 0, " ", 0, dsBoll)
      oCleOrd.ResetVar()
      oCleOrd.bModuloCRM = False
      oCleOrd.bIsCRMUser = False
      If IntNumOrd = 0 Then
        IntNumOrd = oCleOrd.LegNuma(StrTipo, StrSerie, IntAnno)

        If IntNumOrd = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128699231787692325, "Prima di creare un nuovo documento è necessario attivare la numerazione del documento")))
          Return False
        End If
        oCleOrd.NuovoOrdine(strDittaCorrente, StrTipo, IntAnno, StrSerie, IntNumOrd)
        oCleOrd.bInNuovoDocSilent = True
        StrStateOrd = "N"
        With oCleOrd.dttET.Rows(0)
          !et_datdoc = CDate(Now)
          oCleOrd.dttET.Rows(0)!et_conto = drNew(0)!xx_cpneofconto
          oCleOrd.dttET.Rows(0)!et_tipobf = oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "TipoBfOf", "2", " ", "2")
          oCleOrd.dttET.Rows(0)!et_commeca = dttET.Rows(0)!et_commeca
          If CInt(oCleOrd.dttET.Rows(0)!et_codpaga) = 0 Then
            oCleOrd.dttET.Rows(0)!et_codpaga = oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "CodPagamentoFor", "1", " ", "1")
          End If
          Dim StrTmpRif As String = "Rif. Ord." & dttET.Rows(0)!et_numdoc.ToString & " di " & Mid(dttET.Rows(0)!Xx_conto.ToString, 1, InStr(dttET.Rows(0)!Xx_conto.ToString, vbCrLf) - 1)
          If Len(StrTmpRif) > 50 Then
            StrTmpRif = Mid(StrTmpRif, 1, 50)
          End If
          oCleOrd.dttET.Rows(0)!et_riferim = StrTmpRif
        End With
      Else
        oCleOrd.ApriOrdine(strDittaCorrente, False, StrTipo, IntAnno, StrSerie, IntNumOrd, dsBoll)
        StrStateOrd = "M"
      End If
      For qq = 0 To drori.Length - 1
        Dim drsof As DataRow() = oCleOrd.dttEC.Select("ec_riga = " & drori(qq)!mo_riga.ToString)
        For ww = drsof.Length - 1 To 0 Step -1
          drsof(ww).Delete()
        Next
        oCleOrd.dttEC.AcceptChanges()
      Next



      If strState <> "D" Then
        oCleOrd.dttET.Rows(0)!Et_confermato = dttET.Rows(0)!Et_confermato
        oCleOrd.dttET.Rows(0)!et_datcons = DateAdd(DateInterval.Day, -1 * CInt(oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "GiorniDtConsAntCorpo", "10", " ", "10")), CDate(dttET.Rows(0)!et_datcons))
        For i = 0 To drNew.Length - 1
          With drNew(i)
            oCleOrd.AggiungiRigaCorpo(False, NTSCStr(drNew(i)!mo_codart), 0, CInt(drNew(i)!mo_riga), 0, 0)
            Dim dr As DataRow = oCleOrd.dttEC.Rows(oCleOrd.dttEC.Rows.Count - 1)
            dr!ec_descr = !mo_descr
            dr!ec_desint = !mo_desint
            dr!ec_note = !mo_note
            dr!ec_quant = !mo_quant
            dr!ec_prezzo = !mo_prezzo
            dr!ec_prezzo = !mo_prezzo
            dr!ec_commeca = !mo_commeca
            dr!ec_subcommeca = !mo_subcommeca
            dr!ec_quaeva = !mo_quaeva
            dr!ec_flevas = !mo_flevas
            dr!ec_scont1 = !mo_scont1
            dr!ec_scont2 = !mo_scont2
            dr!ec_scont3 = !mo_scont3
            dr!ec_scont4 = !mo_scont4
            dr!ec_scont5 = !mo_scont5
            dr!ec_scont6 = !mo_scont6
            dr!ec_datcons = DateAdd(DateInterval.Day, -1 * CInt(oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "GiorniDtConsAntCorpo", "10", " ", "10")), CDate(dttET.Rows(0)!et_datcons))
            If Not oCleOrd.RecordSalva(oCleOrd.dttEC.Rows.Count - 1, False, Nothing) Then
              oCleOrd.dttEC.Rows(oCleOrd.dttEC.Rows.Count - 1).Delete()
              Return False
            End If
          End With
        Next
        oCleOrd.CalcolaTotali()
      End If

      If oCleOrd.dttEC.Rows.Count = 0 Then
        If StrStateOrd = "N" Then
          Return True
        Else
          StrStateOrd = "D"
        End If
      End If
      If Not oCleOrd.SalvaOrdine(StrStateOrd) Then
        IntNumOrd = 0
        LogWrite(oApp.Tr(Me, 128843594045134000, "Documento non salvato"), True)
        Return False
      End If

      intIBoundArray = UBound(strSerieOF)
      intIBoundArray = intIBoundArray + 1
      ReDim Preserve strSerieOF(intIBoundArray)
      ReDim Preserve intAnnoOF(intIBoundArray)
      ReDim Preserve intNumordOF(intIBoundArray)

      strSerieOF(intIBoundArray) = oCleOrd.dttET.Rows(0)!et_serie.ToString
      intAnnoOF(intIBoundArray) = CInt(oCleOrd.dttET.Rows(0)!et_anno)
      intNumordOF(intIBoundArray) = CInt(oCleOrd.dttET.Rows(0)!et_numdoc)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------

      CLN__STD.GestErr(ex, Me, "")

      Return False
      '--------------------------------------------------------------
    End Try
  End Function

  Public Sub CPNEDatiAggiuntivi(ByVal strDitta As String, ByVal strTipoDoc As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer)
    CType(oCldGsor, CLHORGSOR).CPNEPulisciDs(dsShared, "CPNE.ScaPag")
    CType(oCldGsor, CLHORGSOR).CPNECaricaScadPag(dsShared, strDitta, strTipoDoc, nAnno, strSerie, lNumdoc)
    CPNEAggiungiHandlerCPNEScaPag()
  End Sub
  Public Sub CPNEAzzeraRighe()
    If dttEC.Rows.Count > 0 Then
      ThrowRemoteEvent(New NTSEventArgs("CPNE.BloccaPrzForn", "true"))
      For xx = 0 To dttEC.Rows.Count - 1
        If dttEC.Rows(xx)!ec_codart.ToString <> oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "CodArtTotale", "TOTALE", " ", "TOTALE") Then
          If CDec(dttEC.Rows(xx)!ec_preziva) > 0 Then
            dttEC.Rows(xx)!ec_preziva = 0
          Else
            dttEC.Rows(xx)!ec_prezzo = 0
          End If
          RecordSalva(xx, True, Nothing)
        Else
          If CInt(dttEC.Rows(xx)!ec_quant) = 0 Then
            dttEC.Rows(xx)!ec_colli = 1
          End If
        End If
      Next
      dttEC.AcceptChanges()
    End If
  End Sub
  Public Sub CPNERiproporzionaPrezzoRighe(decQtaTot As Decimal)
    Dim PrzTotPrec As Decimal = 0
    Dim PrzResiduo As Decimal = decQtaTot
    Dim Perc As Decimal = 0
    Dim intRigheValide As Integer = 0
    Dim intRigheElaborate As Integer = 0
    Dim decPrezzoRiga As Decimal = 0

    ' calcolo il totale attuale delle righe
    If dttEC.Rows.Count > 0 Then
      For xx = 0 To dttEC.Rows.Count - 1
        If dttEC.Rows(xx)!ec_codart.ToString <> oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "CodArtTotale", "TOTALE", " ", "TOTALE") Then
          If CDec(dttEC.Rows(xx)!ec_prezzo) > 0 Or CDec(dttEC.Rows(xx)!ec_preziva) > 0 Then
            'If CDec(dttEC.Rows(xx)!ec_preziva) > 0 Then
            If dttET.Rows(0)!et_scorpo.ToString = "S" Then
              PrzTotPrec += CDec(dttEC.Rows(xx)!ec_preziva) * CDec(dttEC.Rows(xx)!ec_quant)
            Else
              PrzTotPrec += CDec(dttEC.Rows(xx)!ec_valoremm)
            End If
            intRigheValide += 1
          End If
        End If
      Next
    End If

    ' riproporziono
    If dttEC.Rows.Count > 0 Then
      ThrowRemoteEvent(New NTSEventArgs("CPNE.BloccaPrzForn", "true"))
      For xx = 0 To dttEC.Rows.Count - 1
        If dttEC.Rows(xx)!ec_codart.ToString <> oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "CodArtTotale", "TOTALE", " ", "TOTALE") Then
          If CDec(dttEC.Rows(xx)!ec_prezzo) > 0 Or CDec(dttEC.Rows(xx)!ec_preziva) > 0 Then
            If dttET.Rows(0)!et_scorpo.ToString = "S" Then
              decPrezzoRiga = CDec(dttEC.Rows(xx)!ec_preziva) * CDec(dttEC.Rows(xx)!ec_quant)
            Else
              decPrezzoRiga = CDec(dttEC.Rows(xx)!ec_valoremm)
            End If
            intRigheElaborate += 1
            If intRigheElaborate < intRigheValide Then
              Perc = ArrDbl(ArrDbl(decPrezzoRiga, 2) * 100 / PrzTotPrec, 2)
              If dttET.Rows(0)!et_scorpo.ToString = "S" Then
                dttEC.Rows(xx)!ec_preziva = ArrDbl(ArrDbl(decQtaTot * Perc / 100, 2) / CDec(dttEC.Rows(xx)!ec_quant), 2)
              Else
                dttEC.Rows(xx)!ec_prezzo = ArrDbl(ArrDbl(decQtaTot * Perc / 100, 2) / CDec(dttEC.Rows(xx)!ec_quant), 2)
              End If
              PrzResiduo -= ArrDbl(decQtaTot * Perc / 100, 2)
            Else
              If CDec(dttEC.Rows(xx)!ec_preziva) > 0 Then
                dttEC.Rows(xx)!ec_preziva = ArrDbl(PrzResiduo / CDec(dttEC.Rows(xx)!ec_quant), 2)
              Else
                dttEC.Rows(xx)!ec_prezzo = ArrDbl(PrzResiduo / CDec(dttEC.Rows(xx)!ec_quant), 2)
              End If
            End If
            RecordSalva(xx, True, Nothing)
          End If
        End If
      Next
      dttEC.AcceptChanges()
    End If
  End Sub
  Public Function CPNEValidaTotPagamenti() As Boolean
    If IsDBNull(dttET.Rows(0)!xx_valresiduo) Then
      If CDec(dttET.Rows(0)!et_totdoc) > 0 Then
        Return False
      Else
        Return True
      End If
    Else
      If CDec(dttET.Rows(0)!xx_valresiduo) <> 0 Then
        If CDec(dttET.Rows(0)!xx_valresiduo) <> CDec(dttET.Rows(0)!et_totdoc) Then
          Return False
        Else
          Return True
        End If
      Else
        Return True
      End If
    End If
  End Function
  Public Sub CPNEAggiornaResiduoOrdine()
    If dsShared.Tables("CPNE.ScaPag").Rows.Count > 0 Then
      dttET.Rows(0)!xx_valresiduo = dttET.Rows(0)!et_totdoc
      For xx = 0 To dsShared.Tables("CPNE.ScaPag").Rows.Count - 1
        If IsDBNull(dsShared.Tables("CPNE.ScaPag").Rows(xx)!hh_imppaga) Then dsShared.Tables("CPNE.ScaPag").Rows(xx)!hh_imppaga = 0
        dttET.Rows(0)!xx_valresiduo = CDec(dttET.Rows(0)!xx_valresiduo) - CDec(dsShared.Tables("CPNE.ScaPag").Rows(xx)!hh_imppaga)
      Next
    Else
      dttET.Rows(0)!xx_valresiduo = dttET.Rows(0)!et_totdoc
    End If
  End Sub
  Public Sub CPNECreaRigaTotale()
    Dim decImpTot As Decimal = 0
    Dim bEsiste As Boolean = False
    If dttEC.Rows.Count > 0 Then
      For xx = 0 To dttEC.Rows.Count - 1
        If dttEC.Rows(xx)!ec_codart.ToString = oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "CodArtTotale", "TOTALE", " ", "TOTALE") Then
          ThrowRemoteEvent(New NTSEventArgs("", "Esiste già una riga di Totale"))
          bEsiste = True
          Exit For
        End If
        If CDec(dttEC.Rows(xx)!ec_prezzo) > 0 Or CDec(dttEC.Rows(xx)!ec_preziva) > 0 Then
          If dttET.Rows(0)!et_scorpo.ToString = "S" Then
            decImpTot += ArrDbl(CDec(dttEC.Rows(xx)!ec_preziva) * CDec(dttEC.Rows(xx)!ec_quant), 2)
          Else
            decImpTot += ArrDbl(CDec(dttEC.Rows(xx)!ec_prezzo) * CDec(dttEC.Rows(xx)!ec_quant), 2)
          End If
        End If
      Next
      If bEsiste = False Then
        AggiungiRigaCorpo(False, oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "CodArtTotale", "TOTALE", " ", "TOTALE"), 0, 0, NTSCInt(0), NTSCInt(dttET.Rows(0)!et_magaz))
        With dttEC.Rows(dttEC.Rows.Count - 1)
          !ec_colli = 0
          If dttET.Rows(0)!et_scorpo.ToString = "S" Then
            !ec_preziva = decImpTot
          Else
            !ec_prezzo = decImpTot
          End If
        End With
      End If
    End If
  End Sub
  Public Overrides Function ApriOrdine(ByVal strDitta As String, ByVal bNew As Boolean, ByVal strTipoDoc As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, ByRef ds As System.Data.DataSet) As Boolean
    ApriOrdine = MyBase.ApriOrdine(strDitta, bNew, strTipoDoc, nAnno, strSerie, lNumdoc, ds)
    If ApriOrdine Then
      ThrowRemoteEvent(New NTSEventArgs("CPNE.BloccaPrzForn", "False"))
      If dttET.Rows.Count > 0 Then
        CPNEDatiAggiuntivi(strDitta, strTipoDoc, nAnno, strSerie, lNumdoc)
        ReDim Preserve strSerieOF(0)
        ReDim Preserve intAnnoOF(0)
        ReDim Preserve intNumordOF(0)
      End If
    End If
  End Function
  Public Overrides Function NuovoOrdine(ByVal strDitta As String, ByVal strTipoDoc As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    NuovoOrdine = MyBase.NuovoOrdine(strDitta, strTipoDoc, nAnno, strSerie, lNumdoc)
    If NuovoOrdine Then
      ThrowRemoteEvent(New NTSEventArgs("CPNE.BloccaPrzForn", "False"))
      CPNEDatiAggiuntivi(strDitta, strTipoDoc, nAnno, strSerie, lNumdoc)
      ReDim Preserve strSerieOF(0)
      ReDim Preserve intAnnoOF(0)
      ReDim Preserve intNumordOF(0)
    End If
  End Function
  Public Sub CPNERedditivita()
    dsShared.Tables("CPNE.marg").Rows(0)!xx_art = "D"
    dsShared.Tables("CPNE.marg").Rows(0)!xx_ricavo = 0

    dsShared.Tables("CPNE.marg").Rows(0)!xx_costo = 0  ' Beretta 2015-09-21
    For i = 0 To dttEC.Rows.Count - 1
      dsShared.Tables("CPNE.marg").Rows(0)!xx_ricavo = CDec(dsShared.Tables("CPNE.marg").Rows(0)!xx_ricavo) + CDec(dttEC.Rows(i)!ec_valoremm)
      Dim dr As DataRow() = DTCPNE_OF.Select("mo_subcommeca = " & CStrSQL(dttEC.Rows(i)!ec_subcommeca))
      If dr.Length = 0 Then
        If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "ValorizzazioneUltCosto", "S", " ", "S") = "S" Then
          Dim dUltCost As Decimal = 0
          dUltCost = oClhGsor.CPNELeggiUltCos(dttEC.Rows(i)!ec_codart.ToString, strDittaCorrente)
          dsShared.Tables("CPNE.marg").Rows(0)!xx_costo = CDec(dsShared.Tables("CPNE.marg").Rows(0)!xx_costo) + dUltCost
        End If
      End If
    Next
    For i = 0 To DTCPNE_OF.Rows.Count - 1
      Dim dtrEC As DataRow = DTCPNE_OF.Rows(i)
      dsShared.Tables("CPNE.marg").Rows(0)!xx_costo = CDec(dsShared.Tables("CPNE.marg").Rows(0)!xx_costo) + NTSCDec(dtrEC!mo_prezzo) * NTSCDec(dtrEC!mo_quant) *
                                        (100 - NTSCDec(dtrEC!mo_scont1)) / 100 *
                                        (100 - NTSCDec(dtrEC!mo_scont2)) / 100 *
                                        (100 - NTSCDec(dtrEC!mo_scont3)) / 100 *
                                        (100 - NTSCDec(dtrEC!mo_scont4)) / 100 *
                                        (100 - NTSCDec(dtrEC!mo_scont5)) / 100 *
                                        (100 - NTSCDec(dtrEC!mo_scont6)) / 100
    Next
    If oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "ValorizzazioneMargine", "C", " ", "C") = "C" Then
      If CDec(dsShared.Tables("CPNE.marg").Rows(0)!xx_costo) = 0 Then
        dsShared.Tables("CPNE.marg").Rows(0)!xx_marg = "0%"
      Else
        dsShared.Tables("CPNE.marg").Rows(0)!xx_marg = Format((CDec(dsShared.Tables("CPNE.marg").Rows(0)!xx_ricavo) - CDec(dsShared.Tables("CPNE.marg").Rows(0)!xx_costo)) / CDec(dsShared.Tables("CPNE.marg").Rows(0)!xx_costo) * 100, "0.00") & "%"
      End If
    Else
      If CDec(dsShared.Tables("CPNE.marg").Rows(0)!xx_ricavo) = 0 Then
        dsShared.Tables("CPNE.marg").Rows(0)!xx_marg = "0%"
      Else
        dsShared.Tables("CPNE.marg").Rows(0)!xx_marg = Format((CDec(dsShared.Tables("CPNE.marg").Rows(0)!xx_ricavo) - CDec(dsShared.Tables("CPNE.marg").Rows(0)!xx_costo)) / CDec(dsShared.Tables("CPNE.marg").Rows(0)!xx_ricavo) * 100, "0.00") & "%"
      End If
    End If
    dsShared.Tables("CPNE.marg").Rows(0)!xx_diff = CDec(dsShared.Tables("CPNE.marg").Rows(0)!xx_ricavo) - CDec(dsShared.Tables("CPNE.marg").Rows(0)!xx_costo)
    dsShared.Tables("CPNE.marg").Rows(0)!xx_diff = Format(CDec(dsShared.Tables("CPNE.marg").Rows(0)!xx_diff), "#,##0.00")
    dsShared.Tables("CPNE.marg").Rows(0)!xx_ricavo = Format(CDec(dsShared.Tables("CPNE.marg").Rows(0)!xx_ricavo), "#,##0.00")
    dsShared.Tables("CPNE.marg").Rows(0)!xx_costo = Format(CDec(dsShared.Tables("CPNE.marg").Rows(0)!xx_costo), "#,##0.00")

  End Sub
  Public Sub CPNETotaliPerFornitore()
    Dim dttOut As New DataTable
    dttOut = DTCPNE_OF
    dttOut.DefaultView.Sort = "xx_cpneofconto"
    dttOut = dttOut.DefaultView.ToTable()
    Dim strCodOldFor As String = ""
    Dim strDesOldFor As String = ""
    Dim decValore As Decimal = 0
    dsShared.Tables("CPNE.TotFor").Clear()
    For i = 0 To dttOut.Rows.Count - 1
      If dttOut.Rows(i)!xx_cpneofconto.ToString <> strCodOldFor Then

        If i > 0 Then
          dsShared.Tables("CPNE.TotFor").Rows.Add()
          dsShared.Tables("CPNE.TotFor").Rows(dsShared.Tables("CPNE.TotFor").Rows.Count - 1)!xx_codfor = strCodOldFor
          dsShared.Tables("CPNE.TotFor").Rows(dsShared.Tables("CPNE.TotFor").Rows.Count - 1)!xx_desfor = strDesOldFor
          dsShared.Tables("CPNE.TotFor").Rows(dsShared.Tables("CPNE.TotFor").Rows.Count - 1)!xx_valfor = Format(decValore, "#,##0.00")
        End If
        strCodOldFor = dttOut.Rows(i)!xx_cpneofconto.ToString
        strDesOldFor = dttOut.Rows(i)!an_descr1.ToString
        decValore = 0
      End If
      decValore += CDec(dttOut.Rows(i)!mo_valoremm)
    Next
    dsShared.Tables("CPNE.TotFor").Rows.Add()
    dsShared.Tables("CPNE.TotFor").Rows(dsShared.Tables("CPNE.TotFor").Rows.Count - 1)!xx_codfor = strCodOldFor
    dsShared.Tables("CPNE.TotFor").Rows(dsShared.Tables("CPNE.TotFor").Rows.Count - 1)!xx_desfor = strDesOldFor
    dsShared.Tables("CPNE.TotFor").Rows(dsShared.Tables("CPNE.TotFor").Rows.Count - 1)!xx_valfor = Format(decValore, "#,##0.00")
    dsShared.Tables("CPNE.TotFor").AcceptChanges()
  End Sub
  Public Function CPNEElabCont(dr As DataRow) As Boolean
    If IsNothing(dr) Then
      Return False
    Else
      If Not dsShared.Tables.Contains("CPNE_ANOM") Then
        dsShared.Tables.Add("CPNE_ANOM")
        dsShared.Tables("CPNE_ANOM").Columns.Add("xx_descr", GetType(String))
        dsShared.Tables("CPNE_ANOM").Columns.Add("xx_note", GetType(String))
        dsShared.Tables("CPNE_ANOM").Columns.Add("xx_riga", GetType(String))
        dsShared.Tables("CPNE_ANOM").Rows.Add()
      End If
      dsShared.Tables("CPNE_ANOM").Rows(0)!xx_riga = dr!ec_riga
      dsShared.Tables("CPNE_ANOM").Rows(0)!xx_descr = dr!ec_descr
      dsShared.Tables("CPNE_ANOM").Rows(0)!xx_note = dr!ec_note
      Return (True)
    End If
  End Function
  Public Function CPNEGeneraNonConf() As Boolean

    Dim StrTipo As String
    Dim IntAnno As Integer
    Dim StrSerie As String
    Dim IntNumOrd As Integer




    Dim StrStateOrd As String
    Dim dtTmp As New DataTable
    Dim StrTmp As String = ""
    Try



      StrTipo = "R"
      IntAnno = CInt(dttET.Rows(0)!et_anno)
      StrSerie = oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "SerieNC", "R", " ", "R") 'dttET.Rows(0)!et_serie.ToString
      IntNumOrd = CInt(dttET.Rows(0)!et_numdoc)



      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      Dim oCleOrd As CLEORGSOR
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEVEGNBF", "BEORGSOR", oTmp, strErr, False, "", "") = False Then
        Throw New NTSException(oApp.Tr(Me, 128607611686875000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return False
      End If
      oCleOrd = CType(oTmp, CLEORGSOR)
      '------------------------------------------------
      AddHandler oCleOrd.RemoteEvent, AddressOf GestisciEventiEntityBoll
      If oCleOrd.Init(oApp, oScript, oCleComm, "", False, "", "") = False Then Return False
      If Not oCleOrd.InitExt() Then Return False
      Dim dsBoll As New DataSet
      oCleOrd.ApriOrdine(strDittaCorrente, True, "B", 0, " ", 0, dsBoll)
      oCleOrd.ResetVar()
      oCleOrd.bModuloCRM = False
      oCleOrd.bIsCRMUser = False

      If Not CType(oCldGsor, CLHORGSOR).CPNEEsisteOrd(StrTipo, IntAnno, StrSerie, IntNumOrd, strDittaCorrente) Then
        oCleOrd.NuovoOrdine(strDittaCorrente, StrTipo, IntAnno, StrSerie, IntNumOrd)
        oCleOrd.bInNuovoDocSilent = True
        StrStateOrd = "N"



        oCleOrd.dttET.Rows(0)!et_datdoc = dttET.Rows(0)!et_datdoc
        oCleOrd.dttET.Rows(0)!et_conto = dttET.Rows(0)!et_conto
        oCleOrd.dttET.Rows(0)!et_tipobf = oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "TipoBfNonConf", "0", " ", "0")
        oCleOrd.dttET.Rows(0)!et_commeca = dttET.Rows(0)!et_commeca
        oCleOrd.dttET.Rows(0)!et_riferim = dttET.Rows(0)!et_riferim
        CType(oCleOrd, CLFORGSOR).CPNEInizializzaPers(0)
      Else
        oCleOrd.ApriOrdine(strDittaCorrente, False, StrTipo, IntAnno, StrSerie, IntNumOrd, dsBoll)
        StrStateOrd = "M"
        CType(oCleOrd, CLFORGSOR).CPNEInizializzaPers(0)
      End If

      oCleOrd.dttEC.Rows.Add()
      Dim dr As DataRow = oCleOrd.dttEC.Rows(oCleOrd.dttEC.Rows.Count - 1)
      Dim DrIc As DataRow = dttEC.Select("ec_riga = " & dsShared.Tables("CPNE_ANOM").Rows(0)!xx_riga.ToString)(0)
      dr!ec_codart = DrIc!ec_codart
      dr!ec_descr = dsShared.Tables("CPNE_ANOM").Rows(0)!xx_descr
      dr!ec_desint = CInt(oCleOrd.dttET.Rows(0)!et_commeca)
      dr!ec_datcons = Today

      dr!ec_note = dsShared.Tables("CPNE_ANOM").Rows(0)!xx_note
      dr!ec_subcommeca = DrIc!ec_subcommeca
      dr!ec_commeca = 0
      oCleOrd.CalcolaTotali()
      If Not oCleOrd.SalvaOrdine(StrStateOrd) Then
        IntNumOrd = 0
        LogWrite(oApp.Tr(Me, 128843594045134000, "Documento non salvato"), True)
        Return False
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------

      CLN__STD.GestErr(ex, Me, "")

      Return False
      '--------------------------------------------------------------
    End Try
  End Function
  Public Function CPNEValidaGriPagamenti(ByVal nRow As Integer) As Boolean
    If nRow < 0 Then Return True
    If nRow > dsShared.Tables("CPNE.ScaPag").Rows.Count Then Return True
    If dsShared.Tables("CPNE.ScaPag").Rows(nRow)!hh_codpaga.ToString <> "" Or dsShared.Tables("CPNE.ScaPag").Rows(nRow)!hh_imppaga.ToString <> "" Then
      If IsDBNull(dsShared.Tables("CPNE.ScaPag").Rows(nRow)!hh_flgpaga) Then
        ThrowRemoteEvent(New NTSEventArgs("", "Il campor 'FLG(*)' come da Legenda accetta solo i valori 1,2,3"))
        Return False
      ElseIf dsShared.Tables("CPNE.ScaPag").Rows(nRow)!hh_flgpaga.ToString = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", "Il campor 'FLG(*)' come da Legenda accetta solo i valori 1,2,3"))
        Return False
      ElseIf CInt(dsShared.Tables("CPNE.ScaPag").Rows(nRow)!hh_flgpaga.ToString) < 1 Or CInt(dsShared.Tables("CPNE.ScaPag").Rows(nRow)!hh_flgpaga.ToString) > 3 Then
        ThrowRemoteEvent(New NTSEventArgs("", "Il campor 'FLG(*)' come da Legenda accetta solo i valori 1,2,3"))
        Return False
      End If
    End If
    If dsShared.Tables("CPNE.ScaPag").Rows(nRow)!hh_numft.ToString <> "" And dsShared.Tables("CPNE.ScaPag").Rows(nRow)!hh_numft.ToString <> "0" Then
      If IsDBNull(dsShared.Tables("CPNE.ScaPag").Rows(nRow)!hh_flgft) Then
        ThrowRemoteEvent(New NTSEventArgs("", "Il campor 'FLG(**)' come da Legenda accetta solo i valori F,P"))
        Return False
      ElseIf dsShared.Tables("CPNE.ScaPag").Rows(nRow)!hh_flgft.ToString <> "F" And dsShared.Tables("CPNE.ScaPag").Rows(nRow)!hh_flgft.ToString <> "P" Then
        ThrowRemoteEvent(New NTSEventArgs("", "Il campor 'FLG(**)' come da Legenda accetta solo i valori F,P"))
        Return False
      End If
    End If
    Return True
  End Function
  Private Function CPNECreaFatturaAnticipo() As Boolean
    If CPNEGeneraDoc() = False Then
      ThrowRemoteEvent(New NTSEventArgs("", "Si è verificato un errore e la fattura non è stata creata"))
      Return False
    End If
    Return True
  End Function
  Private Function CPNEGeneraDoc() As Boolean
    Try
      nAnnoDoc = Year(dtDataDoc)
      '----------------------------------------------------------------------------------------
      '--- Inizializzo BEVEBOLL
      '----------------------------------------------------------------------------------------
      If Not CPNEInizializzaBeveboll() Then
        Throw (New NTSException("Errore durante l'inizializzazione di BEVEBOLL!" & Chr(13) & "La procedura verrà bloccata"))
        Return False
      End If
      strSerieDoc = UCase(oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "FtAnticipoSerie", " ", " ", " "))
      lNumTmpDoc = oCleBoll.LegNuma(strTipoDoc, strSerieDoc, nAnnoDoc)


      '----------------------------
      'preparo l'ambiente
      Dim ds As New DataSet
      If Not oCleBoll.ApriDoc(oApp.Ditta, False, strTipoDoc, nAnnoDoc, strSerieDoc, lNumTmpDoc, ds) Then Return False
      oCleBoll.bInApriDocSilent = True

      If oCleBoll.dsShared.Tables("TESTA").Rows.Count > 0 Then
        Throw (New NTSException("Controllare le numerazioni delle fatture immediate!" & Chr(13) & "La procedura verrà bloccata"))
        Return False
      End If

      oCleBoll.ResetVar()
      oCleBoll.strVisNoteConto = "N"
      oCleBoll.NuovoDocumento(oApp.Ditta, strTipoDoc, nAnnoDoc, strSerieDoc, lNumTmpDoc)
      oCleBoll.bInNuovoDocSilent = True

      CPNECreaTestataDoc()

      CPNECreaRigaDoc()

      CPNESettaPiedeDoc()

      oCleBoll.bCreaFilePick = False 'non faccio generare il piking dal salvataggio del documento
      If Not oCleBoll.SalvaDocumento("N") Then
        Throw (New NTSException("Errore al salvataggio della fattura immediata N. '" & lNumTmpDoc & "'" & Chr(13) & "La procedura verrà bloccata"))
        Return False
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      CLN__STD.GestErr(ex, Me, "")
      Return False
    End Try
  End Function
  Public Overridable Function CPNECreaTestataDoc() As Boolean
    Try

      With oCleBoll.dttET.Rows(0)
        'faccio scatenare la onaddnew della testata dell'ordine
        !codditt = oApp.Ditta
        !et_conto = dttET.Rows(0)!et_conto
        !et_tipork = strTipoDoc
        !et_anno = nAnnoDoc
        !et_serie = strSerieDoc
        !et_numdoc = lNumTmpDoc
        !et_tipobf = oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "FtAnticipoTipoBF", "7", " ", "7")
        !et_commeca = dttET.Rows(0)!et_commeca
        !et_datdoc = dtDataDoc
        !et_codpaga = oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "FtAnticipoCodPag", "4", " ", "4")
        !et_dtiniz = dtDataDoc


      End With

      If Not oCleBoll.OkTestata Then Return False
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      'If oCldhh.IsInTrans Then oCldhh.AnnullaTrans()
      CLN__STD.GestErr(ex, Me, "")
      Return False
    End Try
  End Function
  Public Overridable Function CPNECreaRigaDoc() As Boolean
    Try
      Dim strDescrArticolo As String = oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "FtAnticipoDesArt", "Acconto vs. ordine N. #N# del #D#", " ", "Acconto vs. ordine N. #N# del #D#")
      Dim dtArtico As New DataTable
      If ocldBase.ValCodiceDb(oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "FtAnticipoCodArt", "D", " ", "D"), strDittaCorrente, "artico", "s", "", dtArtico) = False Then
        Throw (New NTSException("(CreaRigaDoc) Errore nell'apertura di Artico" & Chr(13) & "La procedura verrà bloccata"))
        Return False
      End If
      If dtArtico.Rows.Count <= 0 Then
        Throw (New NTSException("(CreaRigaDoc) Errore nell'apertura di Artico" & Chr(13) & "La procedura verrà bloccata"))
        Return False
      End If

      If InStr(strDescrArticolo, "#N#") > 0 Then
        If dttET.Rows(0)!et_serie.ToString = " " Then
          strDescrArticolo = Replace(strDescrArticolo, "#N#", dttET.Rows(0)!et_numdoc.ToString)
        Else
          strDescrArticolo = Replace(strDescrArticolo, "#N#", dttET.Rows(0)!et_numdoc.ToString & "/" & dttET.Rows(0)!et_serie.ToString)
        End If
      End If
      If InStr(strDescrArticolo, "#D#") > 0 Then
        strDescrArticolo = Replace(strDescrArticolo, "#D#", Format(dttET.Rows(0)!et_datdoc, "dd/MM/yy"))
      End If

      'Creo una nuova riga di corpo setto i principali campi poi setto tutti gli altri
      If Not oCleBoll.AggiungiRigaCorpo(False, NTSCStr(oCldGsor.GetSettingBus("CPNE", "OPZIONI", "OrdCLiFo", "FtAnticipoCodArt", "D", " ", "D")),
                                               NTSCInt("0"),
                                               0,
                                               CInt(oCleBoll.dttET.Rows(0)!et_causale),
                                               CInt(oCleBoll.dttET.Rows(0)!et_magaz)) Then Return False


      With oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1)
        !ec_quant = 1
        If dttET.Rows(0)!et_scorpo.ToString = "S" Then
          !ec_preziva = decPrezzoDoc
        Else
          !ec_prezzo = decPrezzoDoc
        End If
        !ec_descr = Left(strDescrArticolo, 40)
      End With

      If Not oCleBoll.RecordSalva(oCleBoll.dttEC.Rows.Count - 1, False, Nothing) Then
        oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1).Delete()
        Return False
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      'If oCldhh.IsInTrans Then oCldhh.AnnullaTrans()
      'If dbConn.State = ConnectionState.Open Then dbConn.Close()
      CLN__STD.GestErr(ex, Me, "")
      Return False
    End Try

  End Function
  Public Overridable Function CPNESettaPiedeDoc() As Boolean
    Try
      oCleBoll.CalcolaTotali()
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      'If oCldhh.IsInTrans Then oCldhh.AnnullaTrans()
      'If dbConn.State = ConnectionState.Open Then dbConn.Close()
      CLN__STD.GestErr(ex, Me, "")
      Return False
    End Try
  End Function
  Public Overridable Function CPNEInizializzaBeveboll() As Boolean
    Try
      If Not oCleBoll Is Nothing Then Return True
      '------------------------
      'inizializzo BEVEBOLL
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNORGSOR", "BEVEBOLL", oTmp, strErr, False, "", "") = False Then
        Throw New NTSException(oApp.Tr(Me, 128607611686875006, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return False
      End If
      oCleBoll = CType(oTmp, CLEVEBOLL)
      '------------------------------------------------
      AddHandler oCleBoll.RemoteEvent, AddressOf GestisciEventiEntityBoll
      If oCleBoll.Init(oApp, oScript, oCleComm, "", False, "", "") = False Then Return False
      If Not oCleBoll.InitExt() Then Return False
      oCleBoll.bModuloCRM = False
      oCleBoll.bIsCRMUser = False
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      'If oCldhh.IsInTrans Then oCldhh.AnnullaTrans()
      'If dbConn.State = ConnectionState.Open Then dbConn.Close()
      CLN__STD.GestErr(ex, Me, "")
      Return False
    End Try
  End Function


End Class
