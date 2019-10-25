Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO
Imports System
Imports NTSInformatica.CLE__APP
Public Class CLHCGSTSC
  Inherits CLDCGSTSC

  Public Function CPNECaricaScaPag() As DataTable
    Dim strSql As String
    Dim dt As DataTable
    strSql = "Select *"
    strSql += " From hhscapag"
    strSql += " Order by hh_tipork,hh_anno,hh_serie,hh_numord,hh_riga"
    dt = OpenRecordset(strSql, DBTIPO.DBAZI)
    Return dt
  End Function
  
  'Public Sub CPNECreaRatahhScadenCli(dr As DataRow, dtOrdCli As DataRow, intTipPaga As Integer, strtiporid As String, strtiposeqrid As String)
  '  Dim strSql As String
  '  strSql = "INSERT INTO hhscaden"
  '  strSql += " (sc_conto,sc_annpar,sc_alfpar,sc_numpar,sc_numrata,sc_datsca,sc_darave,sc_importo,sc_importoda,sc_datdoc,sc_alfdoc,sc_numdoc,sc_codpaga,sc_tippaga,sc_impfat"
  '  strSql += ",sc_codbanc,sc_abi,sc_cab,sc_datreg,sc_commeca,sc_subcommeca,sc_datscadorig,sc_dtprevip,sc_tiposeqrid,sc_tiporid,sc_integr,sc_causale,sc_insolu,sc_sollec,sc_codvalu,sc_impval,sc_impvalda,sc_codcage,sc_controp,sc_cambio,sc_coddest,sc_bolli,sc_flsta,sc_fldis,sc_flsaldato,sc_speins)"
  '  strSql += " values"
  '  strSql += " (" & CInt(dtOrdCli!td_conto) & "," & CInt(dr!hh_anno) & "," & CStrSQL(dr!hh_serie) & "," & CInt(dr!hh_numord) & "," & CInt(dr!hh_riga) & "," & CDataSQL(dr!hh_datpaga.ToString) & ","
  '  strSql += "'D'" & "," & CDblSQL(dr!hh_imppaga.ToString) & "," & CDblSQL(dr!hh_imppaga.ToString) & "," & CDataSQL(dr!hh_dataft.ToString) & "," & CStrSQL(dr!hh_serieft) & "," & CInt(dr!hh_numft) & ","
  '  strSql += CInt(dtOrdCli!td_codpaga) & "," & intTipPaga & "," & CDblSQL(dr!hh_imppaga.ToString) & "," & CInt(dtOrdCli!td_codbanc) & "," & CInt(dtOrdCli!td_abi) & "," & CInt(dtOrdCli!td_cab)
  '  strSql += "," & CDataSQL(dtOrdCli!td_datord.ToString) & "," & CInt(dtOrdCli!td_commeca) & ",0" & "," & CDataSQL(dr!hh_datpaga.ToString) & "," & CDataSQL(dr!hh_datpaga.ToString) & "," & CStrSQL(strtiposeqrid) & "," & CStrSQL(strtiporid) & ",'N',0,'N',0,0,0,0,0,0,0,0,0,'','','N',0)"
  '  Execute(strSql, CLE__APP.DBTIPO.DBAZI)
  'End Sub
  'Public Sub CPNECreaRatahhScadenCliPrev(strDatpaga As String, decImppaga As Decimal, dtOrdCli As DataRow, intTipPaga As Integer, strtiporid As String, strtiposeqrid As String)
  '  Dim strSql As String
  '  strSql = "INSERT INTO hhscaden"
  '  strSql += " (sc_conto,sc_annpar,sc_alfpar,sc_numpar,sc_numrata,sc_datsca,sc_darave,sc_importo,sc_importoda,sc_datdoc,sc_alfdoc,sc_numdoc,sc_codpaga,sc_tippaga,sc_impfat"
  '  strSql += ",sc_codbanc,sc_abi,sc_cab,sc_datreg,sc_commeca,sc_subcommeca,sc_datscadorig,sc_dtprevip,sc_tiposeqrid,sc_tiporid,sc_integr)"
  '  strSql += " values"
  '  strSql += " (" & CInt(dtOrdCli!td_conto) & "," & CInt(dtOrdCli!td_anno) & "," & CStrSQL(dtOrdCli!td_serie) & "," & CInt(dtOrdCli!td_numord) & "," & "999" & "," & CDataSQL(strDatpaga) & ","
  '  strSql += "'D'" & "," & CDblSQL(decImppaga.ToString) & "," & CDblSQL(decImppaga.ToString) & "," & CDataSQL("01/01/1900") & "," & CStrSQL(" ") & "," & "0" & ","
  '  strSql += CInt(dtOrdCli!td_codpaga) & "," & intTipPaga & "," & CDblSQL(decImppaga.ToString) & "," & CInt(dtOrdCli!td_codbanc) & "," & CInt(dtOrdCli!td_abi) & "," & CInt(dtOrdCli!td_cab)
  '  strSql += "," & CDataSQL(dtOrdCli!td_datord.ToString) & "," & CInt(dtOrdCli!td_commeca) & ",0" & "," & CDataSQL(strDatpaga) & "," & CDataSQL(strDatpaga) & "," & CStrSQL(strtiposeqrid) & "," & CStrSQL(strtiporid) & ")"
  '  Execute(strSql, CLE__APP.DBTIPO.DBAZI)
  'End Sub
  'Public Sub CPNECreaRatahhScadenForDdtEFt(dr As DataRow, dtOrdCli As DataRow, intTipPaga As Integer, strtiporid As String, strtiposeqrid As String, decImportoRatapiu As Decimal, decImportoRatameno As Decimal, intNumRata As Integer, strDatSca As String)
  '  Dim strSql As String
  '  strSql = "INSERT INTO hhscaden"
  '  strSql += " (sc_conto,sc_annpar,sc_alfpar,sc_numpar,sc_numrata,sc_datsca,sc_darave,sc_importo,sc_importoda,sc_datdoc,sc_alfdoc,sc_numdoc,sc_codpaga,sc_tippaga,sc_impfat"
  '  strSql += ",sc_codbanc,sc_abi,sc_cab,sc_datreg,sc_commeca,sc_subcommeca,sc_datscadorig,sc_dtprevip,sc_tiposeqrid,sc_tiporid,sc_integr)"
  '  strSql += " values"
  '  strSql += " (" & CInt(dr!tm_conto) & "," & CInt(dtOrdCli!td_anno) & "," & CStrSQL(dtOrdCli!td_serie) & "," & CInt(dtOrdCli!td_numord) & "," & intNumRata & "," & CDataSQL(strDatSca) & ","
  '  strSql += "'A'" & "," & CDblSQL(decImportoRatameno.ToString) & "," & CDblSQL(decImportoRatapiu.ToString) & "," & CDataSQL(dr!tm_datdoc.ToString) & "," & CStrSQL(dr!tm_serie) & "," & CInt(dr!tm_numdoc) & ","
  '  strSql += CInt(dr!tm_codpaga) & "," & intTipPaga & "," & CDblSQL(dr!tm_totdoc.ToString) & "," & CInt(dr!tm_codbanc) & "," & CInt(dr!tm_abi) & "," & CInt(dr!tm_cab)
  '  strSql += "," & CDataSQL(dr!tm_datdoc.ToString) & "," & CInt(dr!tm_commeca) & "," & CInt(dr!tm_subcommeca) & "," & CDataSQL(strDatSca) & "," & CDataSQL(strDatSca) & "," & CStrSQL(strtiposeqrid) & "," & CStrSQL(strtiporid) & ")"
  '  Execute(strSql, CLE__APP.DBTIPO.DBAZI)
  'End Sub
  'Public Sub CPNECreaRatahhScadenForOrdini(dr As DataRow, intTipPaga As Integer, strtiporid As String, strtiposeqrid As String, decImportoRatapiu As Decimal, decImportoRatameno As Decimal, intNumRata As Integer, strDatSca As String)
  '  Dim strSql As String
  '  strSql = "INSERT INTO hhscaden"
  '  strSql += " (sc_conto,sc_annpar,sc_alfpar,sc_numpar,sc_numrata,sc_datsca,sc_darave,sc_importo,sc_importoda,sc_datdoc,sc_alfdoc,sc_numdoc,sc_codpaga,sc_tippaga,sc_impfat"
  '  strSql += ",sc_codbanc,sc_abi,sc_cab,sc_datreg,sc_commeca,sc_subcommeca,sc_datscadorig,sc_dtprevip,sc_tiposeqrid,sc_tiporid,sc_integr)"
  '  strSql += " values"
  '  strSql += " (" & CInt(dr!td_conto) & "," & CInt(dr!td_anno) & "," & CStrSQL(dr!td_serie) & "," & CInt(dr!td_numord) & "," & intNumRata & "," & CDataSQL(strDatSca) & ","
  '  strSql += "'A'" & "," & CDblSQL(decImportoRatameno.ToString) & "," & CDblSQL(decImportoRatapiu.ToString) & "," & CDataSQL(dr!td_datord.ToString) & "," & CStrSQL(dr!td_serie) & "," & CInt(dr!td_numord) & ","
  '  strSql += CInt(dr!td_codpaga) & "," & intTipPaga & "," & CDblSQL(dr!td_totdoc.ToString) & "," & CInt(dr!td_codbanc) & "," & CInt(dr!td_abi) & "," & CInt(dr!td_cab)
  '  strSql += "," & CDataSQL(dr!td_datord.ToString) & "," & CInt(dr!td_commeca) & ",0" & "," & CDataSQL(strDatSca) & "," & CDataSQL(strDatSca) & "," & CStrSQL(strtiposeqrid) & "," & CStrSQL(strtiporid) & ")"
  '  Execute(strSql, CLE__APP.DBTIPO.DBAZI)
  'End Sub
  Public Sub CPNEPuliscihhScaden()
    Dim strSql As String
    strSql = "delete from hhscaden"
    Execute(strSql, CLE__APP.DBTIPO.DBAZI)
  End Sub
  Public Function CPNECaricahhScaden() As DataTable
    Dim strSql As String
    Dim dt As DataTable
    strSql = "Select *"
    strSql += " From hhScaden"
    strSql += " where codditt = 'CP999'"
    dt = OpenRecordset(strSql, DBTIPO.DBAZI)
    Return dt
  End Function
  Public Function CPNECaricaDatiOrdineCliente(dr As DataRow) As DataTable
    Dim strSql As String
    Dim dt As DataTable
    strSql = "Select td_conto,td_codpaga,td_codbanc,td_abi,td_cab,td_datord, td_totdoc, td_anno,td_serie,td_numord,td_datcons,td_commeca"
    strSql += " From testord"
    strSql += " where td_tipork = 'R' and td_anno = " & CInt(dr!hh_anno) & " and td_serie = " & CStrSQL(dr!hh_serie) & " and  td_numord = " & CInt(dr!hh_numord)
    dt = OpenRecordset(strSql, DBTIPO.DBAZI)
    Return dt
  End Function
  Public Function CPNECaricaDatiOrdineFornitoreDaTestmag(intCommessa As Integer) As DataTable
    Dim strSql As String
    Dim dt As DataTable
    strSql = "Select td_conto,td_codpaga,td_codbanc,td_abi,td_cab,td_datord, td_totdoc, td_anno,td_serie,td_numord,td_datcons,td_commeca"
    strSql += " From testord"
    strSql += " where td_commeca = " & intCommessa
    dt = OpenRecordset(strSql, DBTIPO.DBAZI)
    Return dt
  End Function
  Public Function CPNECaricaTipoPag(intCodPaga As Integer) As Integer
    Dim strSql As String
    Dim dt As DataTable
    strSql = "Select tb_tippaga"
    strSql += " From tabpaga"
    strSql += " where tb_codpaga = " & intCodPaga
    dt = OpenRecordset(strSql, DBTIPO.DBAZI)
    If dt.Rows.Count > 0 Then
      Return CInt(dt.Rows(0)!tb_tippaga)
    Else
      Return 0
    End If
  End Function
  Public Sub CPNECaricaRateDaFtContabilizzate()
    Dim strSql As String
    strSql = "INSERT INTO hhscaden(scaden.codditt,[sc_conto],[sc_annpar],[sc_alfpar],[sc_numpar],[sc_numrata],[sc_datsca],[sc_causale],[sc_descr],[sc_darave],[sc_importo],[sc_importoda]"
    strSql += ",[sc_datdoc],[sc_alfdoc],[sc_numdoc],[sc_insolu],[sc_codpaga],[sc_tippaga],[sc_codbanc],[sc_sollec],[sc_codvalu],[sc_impval],[sc_impvalda],[sc_codcage],[sc_abi]"
    strSql += ",[sc_cab],[sc_numcc],[sc_controp],[sc_cambio],[sc_impfat],[sc_salcon],[sc_banc1],[sc_banc2],[sc_flsta],[sc_fldis],[sc_flsaldato],[sc_dtdist],[sc_opdist]"
    strSql += ",[sc_dtsaldato],[sc_coddest],[sc_bolli],[sc_speins],[sc_anneff],[sc_numeff],[sc_anndist],[sc_numdist],[sc_datreg],[sc_numreg],[sc_rgsaldato],[sc_integr]"
    strSql += ",[sc_numprot],[sc_ultagg],[sc_caubloc],[sc_opnome],[sc_commeca],[sc_subcommeca],[sc_alfpro],[sc_gennac],[sc_impabb],[sc_cauval],[sc_tipcvs],[sc_cin]"
    strSql += ",[sc_prefiban],[sc_iban],[sc_codstpg],[sc_codnaut],[sc_desnotaut],[sc_dtprevip],[sc_fldatprbl],[sc_numratarif],[sc_datscadorig],[sc_codincdiff],[sc_key2]"
    strSql += ",[sc_swift],[sc_tiposeqrid],[sc_tiporid],[sc_dtmandrid],[sc_idmandrid],[sc_cup],[sc_cig],[sc_riferimpa])"

    strSql += " Select scaden.codditt,[sc_conto],[sc_annpar],[sc_alfpar],[sc_numpar],[sc_numrata],[sc_datsca],[sc_causale],[sc_descr],[sc_darave],[sc_importo],[sc_importoda]"
    strSql += ",[sc_datdoc],[sc_alfdoc],[sc_numdoc],[sc_insolu],[sc_codpaga],[sc_tippaga],[sc_codbanc],[sc_sollec],[sc_codvalu],[sc_impval],[sc_impvalda],[sc_codcage],[sc_abi]"
    strSql += ",[sc_cab],[sc_numcc],[sc_controp],[sc_cambio],[sc_impfat],[sc_salcon],[sc_banc1],[sc_banc2],[sc_flsta],[sc_fldis],[sc_flsaldato],[sc_dtdist],[sc_opdist]"
    strSql += ",[sc_dtsaldato],[sc_coddest],[sc_bolli],[sc_speins],[sc_anneff],[sc_numeff],[sc_anndist],[sc_numdist],[sc_datreg],[sc_numreg],[sc_rgsaldato],[sc_integr]"
    strSql += ",[sc_numprot],[sc_ultagg],[sc_caubloc],[sc_opnome],[sc_commeca],[sc_subcommeca],[sc_alfpro],[sc_gennac],[sc_impabb],[sc_cauval],[sc_tipcvs],[sc_cin]"
    strSql += ",[sc_prefiban],[sc_iban],[sc_codstpg],[sc_codnaut],[sc_desnotaut],[sc_dtprevip],[sc_fldatprbl],[sc_numratarif],[sc_datscadorig],[sc_codincdiff],[sc_key2]"
    strSql += ",[sc_swift],[sc_tiposeqrid],[sc_tiporid],[sc_dtmandrid],[sc_idmandrid],[sc_cup],[sc_cig],[sc_riferimpa]"

    strSql += " FROM scaden LEFT JOIN anagra ON scaden.sc_conto = anagra.an_conto"
    strSql += " where sc_flsaldato <> 'S' and an_tipo = 'F'"
    Execute(strSql, CLE__APP.DBTIPO.DBAZI)
  End Sub
  Public Function CPNECaricaFtDaContabilizzare() As DataTable
    Dim dt As DataTable
    Dim strSql As String
    strSql = "Select *"
    strSql += " FROM testmag"
    strSql += " WHERE (tm_tipork='L' Or tm_tipork='K') AND tm_flcont<>'S' "
    dt = OpenRecordset(strSql, DBTIPO.DBAZI)
    Return dt
  End Function
  Public Function CPNECaricaDdtDaFatturare() As DataTable
    Dim dt As DataTable
    Dim strSql As String
    strSql = "Select *"
    strSql += " FROM testmag INNER JOIN tabtpbf ON testmag.tm_tipobf = tabtpbf.tb_codtpbf"
    strSql += " WHERE tm_tipork='M' AND tm_flfatt<>'S' AND tb_flresocl = 'S'"
    dt = OpenRecordset(strSql, DBTIPO.DBAZI)
    Return dt
  End Function
  Public Function CPNECaricaOrdiniNonEvasi() As DataTable
    Dim dt As DataTable
    Dim strSql As String
    strSql = "Select td_anno,td_serie,td_numord,Sum([mo_valore]*(100+[tabciva].[tb_aliq])/100) AS ValOrdineDaEvadere"
    strSql += " FROM (testord INNER JOIN movord ON (testord.td_numord = movord.mo_numord) AND (testord.td_serie = movord.mo_serie) AND (testord.td_anno = movord.mo_anno) AND (testord.td_tipork = movord.mo_tipork)) INNER JOIN tabciva ON movord.mo_codiva = tabciva.tb_codciva"
    strSql += " WHERE td_tipork='O' AND td_flevas<>'S' AND mo_flevas<>'S'"
    strSql += " GROUP BY td_anno,td_serie,td_numord,td_codpaga"
    dt = OpenRecordset(strSql, DBTIPO.DBAZI)
    Return dt
  End Function
  Public Function CPNECaricaScadenzeOrdiniOF(drOrdine As DataRow) As DataTable
    Dim dt As DataTable
    Dim strSql As String
    strSql = "Select td_conto,td_codpaga,td_datord,td_codbanc,td_abi,td_cab,td_commeca,td_impsca_1,td_impsca_2,td_impsca_3,td_impsca_4,td_impsca_5,td_impsca_6,td_impsca_7,td_impsca_8"
    strSql += ",td_datsca_1,td_datsca_2,td_datsca_3,td_datsca_4,td_datsca_5,td_datsca_6,td_datsca_7,td_datsca_8"
    strSql += " FROM testord"
    strSql += " WHERE td_tipork='O' AND td_anno = " & CInt(drOrdine!td_anno.ToString) & " and td_serie = " & CStrSQL(drOrdine!td_serie.ToString)
    strSql += " and td_numord = " & CInt(drOrdine!td_numord.ToString)
    dt = OpenRecordset(strSql, DBTIPO.DBAZI)
    Return dt
  End Function
End Class
