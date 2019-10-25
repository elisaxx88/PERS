Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp
Imports NTSInformatica.CLE__APP
Public Class CLFCGSTSC
  Inherits CLECGSTSC
  Dim strErr As String = ""
  Dim oTmp As Object = Nothing
  Public OMenu As Object
  Dim drxxx As DataRow
  Public bRateDaFtContabilizzate As Boolean
  Public bRateDaFtDaContabilizzare As Boolean
  Public bRateDaDdtDaFatturare As Boolean
  Public bRateDaOrdiniNonEvasi As Boolean
  Dim dthhScaden As DataTable



  Public Overrides Function Init(ByRef App As CLE__APP, _
                           ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                           ByVal bFiller1 As Boolean, ByVal strFiller1 As String, _
                           ByVal strFiller2 As String) As Boolean
    'If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BHCGSTSC"

    Init = MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, False, "", "")

  End Function





  Public Sub CPNEElaboraFlussi()
    CType(oCldStsc, CLHCGSTSC).CPNEPuliscihhScaden()
    dthhScaden = CType(oCldStsc, CLHCGSTSC).CPNECaricahhScaden()
    ' elaboro gli impegni clienti
    CPNEClienti()

    ' elaboro gli ordini fornitori
    CPNEFornitori()



  End Sub
  Private Sub CPNEClienti()
    Dim strOrdineOld As String = ""
    Dim strOrdineNew As String = ""
    Dim decTotScapag As Decimal = 0
    Dim dtOrdCli As DataTable
    Dim intTipPaga As Integer = 0

    Dim dtScaPag As DataTable = CType(oCldStsc, CLHCGSTSC).CPNECaricaScaPag()
    If dtScaPag.Rows.Count <= 0 Then Exit Sub
    For i = 0 To dtScaPag.Rows.Count - 1
      With dtScaPag.Rows(i)
        strOrdineNew = !hh_anno.ToString & !hh_serie.ToString & !hh_numord.ToString
        If strOrdineNew <> strOrdineOld Then
          If strOrdineOld <> "" Then
            ' verifico se l'importo dell'ordine è completo altrimenti creo la differenza come previsione
            If decTotScapag < CDec(dtOrdCli.Rows(0)!td_totdoc) Then
              'CType(oCldStsc, CLHCGSTSC).CPNECreaRatahhScadenCliPrev(dtOrdCli.Rows(0)!td_datcons.ToString, CDec(dtOrdCli.Rows(0)!td_totdoc) - decTotScapag, dtOrdCli.Rows(0), intTipPaga, "C", "P")
              CPNECreaRatahhScadenCliPrev(dtOrdCli.Rows(0)!td_datcons.ToString, CDec(dtOrdCli.Rows(0)!td_totdoc) - decTotScapag, dtOrdCli.Rows(0), intTipPaga, "C", "P")
            End If
            decTotScapag = 0
          End If
          ' carica dati ordine
          dtOrdCli = CType(oCldStsc, CLHCGSTSC).CPNECaricaDatiOrdineCliente(dtScaPag.Rows(i))
          ' cerco il tipo pagamento
          intTipPaga = CType(oCldStsc, CLHCGSTSC).CPNECaricaTipoPag(CInt(dtOrdCli.Rows(0)!td_codpaga))
          strOrdineOld = strOrdineNew
        End If
        decTotScapag += CDec(dtScaPag.Rows(i)!hh_imppaga)

        If CInt(!hh_flgpaga) = 1 Or (CInt(!hh_flgpaga) = 3 And !hh_flgft.ToString = "F") Then
          If CDec(!hh_imppaga) > 0 Then
            'CType(oCldStsc, CLHCGSTSC).CPNECreaRatahhScadenCli(dtScaPag.Rows(i), dtOrdCli.Rows(0), intTipPaga, "C", "")
            CPNECreaRatahhScadenCli(dtScaPag.Rows(i), dtOrdCli.Rows(0), intTipPaga, "C", "R")
          End If
        End If
      End With

    Next
    ' verifico se l'importo dell'ordine è completo altrimenti creo la differenza come previsione
    If decTotScapag < CDec(dtOrdCli.Rows(0)!td_totdoc) Then
      'CType(oCldStsc, CLHCGSTSC).CPNECreaRatahhScadenCliPrev(dtOrdCli.Rows(0)!td_datcons.ToString, CDec(dtOrdCli.Rows(0)!td_totdoc) - decTotScapag, dtOrdCli.Rows(0), intTipPaga, "C", "P")
      CPNECreaRatahhScadenCliPrev(dtOrdCli.Rows(0)!td_datcons.ToString, CDec(dtOrdCli.Rows(0)!td_totdoc) - decTotScapag, dtOrdCli.Rows(0), intTipPaga, "C", "P")
    End If
  End Sub
  Private Sub CPNECreaRatahhScadenCliPrev(strDatpaga As String, decImppaga As Decimal, dtOrdCli As DataRow, intTipPaga As Integer, strtiporid As String, strtiposeqrid As String)
    dthhScaden.Rows.Add()

    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!codditt = strDittaCorrente
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_conto = CInt(dtOrdCli!td_conto)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_annpar = CInt(dtOrdCli!td_anno)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_alfpar = dtOrdCli!td_serie.ToString
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numpar = CInt(dtOrdCli!td_numord)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numrata = CInt("999")
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_datsca = strDatpaga
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_darave = "D"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_importo = decImppaga
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_importoda = decImppaga
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_datdoc = "01/01/1900"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_alfdoc = " "
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numdoc = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codpaga = CInt(dtOrdCli!td_codpaga)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_tippaga = intTipPaga
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_impfat = decImppaga
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codbanc = CInt(dtOrdCli!td_codbanc)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_abi = CInt(dtOrdCli!td_abi)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cab = CInt(dtOrdCli!td_cab)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_datreg = dtOrdCli!td_datord.ToString
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_commeca = CInt(dtOrdCli!td_commeca)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_subcommeca = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_datscadorig = strDatpaga
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_dtprevip = strDatpaga
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_tiposeqrid = strtiposeqrid
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_tiporid = strtiporid
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_integr = "N"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_causale = 0
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_descr = ""
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_insolu = "N"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_sollec = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codvalu = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_impval = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_impvalda = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codcage = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_controp = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numcc = ""
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cambio = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_coddest = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_bolli = 0
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_salcon = "C"
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_banc1 = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_banc2 = ""
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_flsta = "N"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_fldis = "N"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_flsaldato = "N"
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_dtdist = "01/01/1900"
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_opdist = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_dtsaldato = "01/01/1900"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_speins = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_anneff = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numeff = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_anndist = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numdist = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numreg = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_rgsaldato = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numprot = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_ultagg = "01/01/1900"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_caubloc = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_opnome = "ADMIN"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_alfpro = " "
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_gennac = "N"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_impabb = 0
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cauval = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_tipcvs = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cin = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_prefiban = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_iban = ""
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codstpg = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codnaut = 0
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_desnotaut = ""
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_fldatprbl = "S"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numratarif = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codincdiff = 0
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_key2 = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_swift = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_dtmandrid = "01/01/1900"
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_idmandrid = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cup = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cig = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_riferimpa = ""
    'dthhScaden.AcceptChanges()
    ocldBase.ScriviTabellaSemplice(strDittaCorrente, "hhScaden", dthhScaden, "", "", "")

  End Sub
  Private Sub CPNECreaRatahhScadenCli(drScaPag As DataRow, dtOrdCli As DataRow, intTipPaga As Integer, strtiporid As String, strtiposeqrid As String)
    dthhScaden.Rows.Add()

    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!codditt = strDittaCorrente
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_conto = CInt(dtOrdCli!td_conto)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_annpar = CInt(drScaPag!hh_anno)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_alfpar = drScaPag!hh_serie.ToString
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numpar = CInt(drScaPag!hh_numord)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numrata = CInt(drScaPag!hh_riga)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_datsca = drScaPag!hh_datpaga.ToString
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_darave = "D"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_importo = CDec(drScaPag!hh_imppaga)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_importoda = CDec(drScaPag!hh_imppaga)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_datdoc = drScaPag!hh_dataft.ToString
    If IsDBNull(drScaPag!hh_serieft.ToString) Then
      dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_alfdoc = " "
    ElseIf drScaPag!hh_serieft.ToString = "" Then
      dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_alfdoc = " "
    Else
      dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_alfdoc = drScaPag!hh_serieft.ToString
    End If

    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numdoc = CInt(drScaPag!hh_numft)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codpaga = CInt(dtOrdCli!td_codpaga)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_tippaga = intTipPaga
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_impfat = CDec(drScaPag!hh_imppaga)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codbanc = CInt(dtOrdCli!td_codbanc)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_abi = CInt(dtOrdCli!td_abi)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cab = CInt(dtOrdCli!td_cab)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_datreg = dtOrdCli!td_datord.ToString
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_commeca = CInt(dtOrdCli!td_commeca)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_subcommeca = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_datscadorig = drScaPag!hh_datpaga.ToString
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_dtprevip = drScaPag!hh_datpaga.ToString
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_tiposeqrid = strtiposeqrid
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_tiporid = strtiporid
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_integr = "N"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_causale = 0
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_descr = ""
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_insolu = "N"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_sollec = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codvalu = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_impval = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_impvalda = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codcage = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_controp = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numcc = ""
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cambio = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_coddest = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_bolli = 0
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_salcon = "C"
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_banc1 = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_banc2 = ""
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_flsta = "N"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_fldis = "N"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_flsaldato = "N"
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_dtdist = "01/01/1900"
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_opdist = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_dtsaldato = "01/01/1900"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_speins = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_anneff = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numeff = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_anndist = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numdist = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numreg = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_rgsaldato = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numprot = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_ultagg = "01/01/1900"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_caubloc = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_opnome = "ADMIN"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_alfpro = " "
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_gennac = "N"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_impabb = 0
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cauval = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_tipcvs = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cin = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_prefiban = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_iban = ""
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codstpg = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codnaut = 0
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_desnotaut = ""
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_fldatprbl = "S"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numratarif = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codincdiff = 0
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_key2 = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_swift = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_dtmandrid = "01/01/1900"
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_idmandrid = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cup = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cig = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_riferimpa = ""
    'dthhScaden.AcceptChanges()
    ocldBase.ScriviTabellaSemplice(strDittaCorrente, "hhScaden", dthhScaden, "", "", "")

  End Sub


  Private Sub CPNEFornitori()
    Dim dtOrdFor As DataTable
    Dim intTipPaga As Integer = 0
    Dim intNumRate As Integer = 0
    Dim decImportoRata As Decimal
    Dim intNumRataOrdine As Integer = 0
    Dim intAnnoOF As Integer = 0
    Dim strSerieOF As String = " "
    Dim intNumordOF As Integer = 0

    ThrowRemoteEvent(New NTSEventArgs("CPNE.PannelloFornitori", ""))

    '==============================================================
    ' rate da fatture contabilizzate
    '==============================================================
    If bRateDaFtContabilizzate Then
      CType(oCldStsc, CLHCGSTSC).CPNECaricaRateDaFtContabilizzate()
    End If
    '==============================================================
    ' rate da fatture da contabilizzare
    '==============================================================
    If bRateDaFtDaContabilizzare Then
      Dim dtFtDaContabilizzare As DataTable = CType(oCldStsc, CLHCGSTSC).CPNECaricaFtDaContabilizzare()
      For i = 0 To dtFtDaContabilizzare.Rows.Count - 1
        ' carica dati ordine
        dtOrdFor = CType(oCldStsc, CLHCGSTSC).CPNECaricaDatiOrdineFornitoreDaTestmag(CInt(dtFtDaContabilizzare.Rows(i)!tm_commeca))

        If dtOrdFor.Rows.Count > 0 Then
          intAnnoOF = CInt(dtOrdFor.Rows(0)!td_anno)
          strSerieOF = dtOrdFor.Rows(0)!td_serie.ToString
          intNumordOF = CInt(dtOrdFor.Rows(0)!td_numord)
          ' cerco il tipo pagamento
          intTipPaga = CType(oCldStsc, CLHCGSTSC).CPNECaricaTipoPag(CInt(dtOrdFor.Rows(0)!td_codpaga))
        End If

        ' conto le rate
        intNumRate = 0
        For r = 1 To 8
          If CDec(dtFtDaContabilizzare.Rows(i)("tm_impsca_" & r.ToString)) > 0 Then
            intNumRate += 1
          End If
        Next

        ' creo le rate
        If intNumRate > 0 Then
          For r = 1 To 8
            If CDec(dtFtDaContabilizzare.Rows(i)("tm_impsca_" & r.ToString)) > 0 Then
              decImportoRata = ArrDbl(CDec(dtFtDaContabilizzare.Rows(i)!tm_totdoc) / intNumRate, 2)
              'CType(oCldStsc, CLHCGSTSC).CPNECreaRatahhScadenForDdtEFt(dtFtDaContabilizzare.Rows(i), dtOrdFor.Rows(0), intTipPaga, "F", "F", decImportoRata, decImportoRata * -1, r + 2000, dtFtDaContabilizzare.Rows(i)("tm_datsca_" & r.ToString).ToString)
              intNumRataOrdine += 1
              CPNECreaRatahhScadenForDdtEFt(dtFtDaContabilizzare.Rows(i), intAnnoOF, strSerieOF, intNumordOF, intTipPaga, "F", "F", decImportoRata, decImportoRata * -1, intNumRataOrdine + 3000, dtFtDaContabilizzare.Rows(i)("tm_datsca_" & r.ToString).ToString)
            End If
          Next
        End If
      Next
    End If

    '==============================================================
    ' rate da ddt da fatturare
    '==============================================================
    If bRateDaDdtDaFatturare Then
      Dim dtDdtDaFatturare As DataTable = CType(oCldStsc, CLHCGSTSC).CPNECaricaDdtDaFatturare()
      For i = 0 To dtDdtDaFatturare.Rows.Count - 1
        ' carica dati ordine
        dtOrdFor = CType(oCldStsc, CLHCGSTSC).CPNECaricaDatiOrdineFornitoreDaTestmag(CInt(dtDdtDaFatturare.Rows(i)!tm_commeca))
        If dtOrdFor.Rows.Count > 0 Then
          intAnnoOF = CInt(dtOrdFor.Rows(0)!td_anno)
          strSerieOF = dtOrdFor.Rows(0)!td_serie.ToString
          intNumordOF = CInt(dtOrdFor.Rows(0)!td_numord)
          ' cerco il tipo pagamento
          intTipPaga = CType(oCldStsc, CLHCGSTSC).CPNECaricaTipoPag(CInt(dtOrdFor.Rows(0)!td_codpaga))
        End If
        ' conto le rate
        intNumRate = 0
        For r = 1 To 8
          If CDec(dtDdtDaFatturare.Rows(i)("tm_impsca_" & r.ToString)) > 0 Then
            intNumRate += 1
          End If
        Next

        ' creo le rate
        If intNumRate > 0 Then
          For r = 1 To 8
            If CDec(dtDdtDaFatturare.Rows(i)("tm_impsca_" & r.ToString)) > 0 Then
              decImportoRata = ArrDbl(CDec(dtDdtDaFatturare.Rows(i)!tm_totdoc) / intNumRate, 2)
              'CType(oCldStsc, CLHCGSTSC).CPNECreaRatahhScadenForDdtEFt(dtDdtDaFatturare.Rows(i), dtOrdFor.Rows(0), intTipPaga, "F", "D", decImportoRata, decImportoRata * -1, r + 1000, dtDdtDaFatturare.Rows(i)("tm_datsca_" & r.ToString).ToString)
              intNumRataOrdine += 1
              CPNECreaRatahhScadenForDdtEFt(dtDdtDaFatturare.Rows(i), intAnnoOF, strSerieOF, intNumordOF, intTipPaga, "F", "D", decImportoRata, decImportoRata * -1, intNumRataOrdine + 2000, dtDdtDaFatturare.Rows(i)("tm_datsca_" & r.ToString).ToString)
            End If
          Next
        End If
      Next

      '==============================================================
      ' rate da ordini da evadere
      '==============================================================
    End If
    If bRateDaOrdiniNonEvasi Then
      Dim dtOrdiniNonEvasi As DataTable = CType(oCldStsc, CLHCGSTSC).CPNECaricaOrdiniNonEvasi()
      For i = 0 To dtOrdiniNonEvasi.Rows.Count - 1
        ' carico le scadenze
        Dim dtScadenzeOF As DataTable = CType(oCldStsc, CLHCGSTSC).CPNECaricaScadenzeOrdiniOF(dtOrdiniNonEvasi.Rows(i))
        If dtScadenzeOF.Rows.Count > 0 Then
          ' cerco il tipo pagamento
          intTipPaga = CType(oCldStsc, CLHCGSTSC).CPNECaricaTipoPag(CInt(dtScadenzeOF.Rows(0)!td_codpaga))


          ' conto le rate
          intNumRate = 0
          For r = 1 To 8
            If CDec(dtScadenzeOF.Rows(0)("td_impsca_" & r.ToString)) > 0 Then
              intNumRate += 1
            End If
          Next

          ' creo le rate
          If intNumRate > 0 Then
            For r = 1 To 8
              If CDec(dtScadenzeOF.Rows(0)("td_impsca_" & r.ToString)) > 0 Then
                decImportoRata = ArrDbl(CDec(dtOrdiniNonEvasi.Rows(i)!ValOrdineDaEvadere) / intNumRate, 2)
                'CType(oCldStsc, CLHCGSTSC).CPNECreaRatahhScadenForOrdini(dtOrdiniNonEvasi.Rows(i), intTipPaga, "F", "O", decImportoRata, decImportoRata * -1, r, dtOrdiniNonEvasi.Rows(i)("td_datsca_" & r.ToString).ToString)
                intNumRataOrdine += 1
                CPNECreaRatahhScadenForOrdini(dtOrdiniNonEvasi.Rows(i), dtScadenzeOF.Rows(0), intTipPaga, "F", "O", decImportoRata, decImportoRata * -1, intNumRataOrdine + 1000, dtScadenzeOF.Rows(0)("td_datsca_" & r.ToString).ToString)
              End If
            Next
          End If
        End If
      Next

    End If
  End Sub
  Private Sub CPNECreaRatahhScadenForDdtEFt(dr As DataRow, intAnnoOF As Integer, strSerieOF As String, intNumordOF As Integer, intTipPaga As Integer, strtiporid As String, strtiposeqrid As String, decImportoRatapiu As Decimal, decImportoRatameno As Decimal, intNumRata As Integer, strDatSca As String)
    dthhScaden.Rows.Add()

    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!codditt = strDittaCorrente
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_conto = CInt(dr!tm_conto)

    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_annpar = intAnnoOF
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_alfpar = strSerieOF
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numpar = intNumordOF
 
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numrata = intNumRata
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_datsca = strDatSca
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_darave = "A"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_importo = CDec(decImportoRatameno)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_importoda = CDec(decImportoRatapiu)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_datdoc = dr!tm_datdoc.ToString
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_alfdoc = dr!tm_serie.ToString
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numdoc = CInt(dr!tm_numdoc)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codpaga = CInt(dr!tm_codpaga)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_tippaga = intTipPaga
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_impfat = CDec(dr!tm_totdoc)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codbanc = CInt(dr!tm_codbanc)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_abi = CInt(dr!tm_abi)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cab = CInt(dr!tm_cab)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_datreg = dr!tm_datdoc.ToString
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_commeca = CInt(dr!tm_commeca)
    If IsDBNull(dr!tm_subcommeca.ToString) Then
      dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_subcommeca = 0
    ElseIf dr!tm_subcommeca.ToString = "" Then
      dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_subcommeca = 0
    ElseIf dr!tm_subcommeca.ToString = " " Then
      dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_subcommeca = 0
    Else
      dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_subcommeca = CInt(dr!tm_subcommeca)
    End If

    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_datscadorig = strDatSca.ToString
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_dtprevip = strDatSca.ToString
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_tiposeqrid = strtiposeqrid
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_tiporid = strtiporid
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_integr = "N"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_causale = 0
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_descr = ""
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_insolu = "N"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_sollec = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codvalu = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_impval = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_impvalda = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codcage = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_controp = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numcc = ""
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cambio = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_coddest = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_bolli = 0
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_salcon = "C"
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_banc1 = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_banc2 = ""
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_flsta = "N"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_fldis = "N"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_flsaldato = "N"
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_dtdist = "01/01/1900"
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_opdist = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_dtsaldato = "01/01/1900"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_speins = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_anneff = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numeff = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_anndist = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numdist = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numreg = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_rgsaldato = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numprot = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_ultagg = "01/01/1900"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_caubloc = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_opnome = "ADMIN"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_alfpro = " "
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_gennac = "N"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_impabb = 0
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cauval = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_tipcvs = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cin = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_prefiban = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_iban = ""
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codstpg = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codnaut = 0
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_desnotaut = ""
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_fldatprbl = "S"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numratarif = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codincdiff = 0
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_key2 = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_swift = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_dtmandrid = "01/01/1900"
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_idmandrid = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cup = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cig = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_riferimpa = ""
    'dthhScaden.AcceptChanges()
    ocldBase.ScriviTabellaSemplice(strDittaCorrente, "hhScaden", dthhScaden, "", "", "")

  End Sub
  Private Sub CPNECreaRatahhScadenForOrdini(dr As DataRow, drAltro As DataRow, intTipPaga As Integer, strtiporid As String, strtiposeqrid As String, decImportoRatapiu As Decimal, decImportoRatameno As Decimal, intNumRata As Integer, strDatSca As String)
    dthhScaden.Rows.Add()

    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!codditt = strDittaCorrente
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_conto = CInt(drAltro!td_conto)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_annpar = CInt(dr!td_anno)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_alfpar = dr!td_serie.ToString
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numpar = CInt(dr!td_numord)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numrata = intNumRata
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_datsca = strDatSca
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_darave = "A"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_importo = CDec(decImportoRatameno)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_importoda = CDec(decImportoRatapiu)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_datdoc = drAltro!td_datord.ToString
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_alfdoc = dr!td_serie.ToString
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numdoc = CInt(dr!td_numord)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codpaga = CInt(drAltro!td_codpaga)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_tippaga = intTipPaga
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_impfat = CDec(dr!ValOrdineDaEvadere)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codbanc = CInt(drAltro!td_codbanc)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_abi = CInt(drAltro!td_abi)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cab = CInt(drAltro!td_cab)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_datreg = drAltro!td_datord.ToString
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_commeca = CInt(drAltro!td_commeca)
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_subcommeca = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_datscadorig = strDatSca.ToString
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_dtprevip = strDatSca.ToString
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_tiposeqrid = strtiposeqrid
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_tiporid = strtiporid
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_integr = "N"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_causale = 0
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_descr = ""
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_insolu = "N"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_sollec = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codvalu = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_impval = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_impvalda = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codcage = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_controp = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numcc = ""
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cambio = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_coddest = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_bolli = 0
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_salcon = "C"
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_banc1 = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_banc2 = ""
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_flsta = "N"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_fldis = "N"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_flsaldato = "N"
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_dtdist = "01/01/1900"
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_opdist = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_dtsaldato = "01/01/1900"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_speins = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_anneff = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numeff = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_anndist = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numdist = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numreg = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_rgsaldato = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numprot = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_ultagg = "01/01/1900"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_caubloc = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_opnome = "ADMIN"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_alfpro = " "
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_gennac = "N"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_impabb = 0
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cauval = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_tipcvs = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cin = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_prefiban = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_iban = ""
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codstpg = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codnaut = 0
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_desnotaut = ""
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_fldatprbl = "S"
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_numratarif = 0
    dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_codincdiff = 0
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_key2 = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_swift = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_dtmandrid = "01/01/1900"
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_idmandrid = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cup = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_cig = ""
    'dthhScaden.Rows(dthhScaden.Rows.Count - 1)!sc_riferimpa = ""
    'dthhScaden.AcceptChanges()
    ocldBase.ScriviTabellaSemplice(strDittaCorrente, "hhScaden", dthhScaden, "", "", "")

  End Sub
End Class
