Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO
Imports System
Imports NTSInformatica.CLE__APP

Public Class CLHORGSOR
  Inherits CLDORGSOR
  Public Sub CPNEMostrOf(ByRef ds As DataSet, ByVal IntCommessa As Integer, ByVal StrTipork As String)

    'Dim strTmp As String = "select movord.codditt, td_tipork, td_anno,td_serie, td_numord, td_conto as xx_cpneofconto, an_descr1, mo_riga, mo_codart, mo_descr, mo_desint, mo_quant, mo_prezzo, mo_quaeva, mo_flevas, mo_commeca, mo_subcommeca, mo_note, mo_scont1, mo_scont2, mo_scont3, mo_scont4, mo_scont5, mo_scont6, mo_valoremm  FROM (anagra INNER JOIN testord ON (anagra.codditt = testord.codditt) AND (anagra.an_conto = testord.td_conto)) INNER JOIN movord ON (testord.codditt = movord.codditt) AND (testord.td_numord = movord.mo_numord) AND (testord.td_serie = movord.mo_serie) AND (testord.td_anno = movord.mo_anno) AND (testord.td_tipork = movord.mo_tipork) where mo_commeca =  " & IntCommessa & " and mo_Commeca <> 0 and mo_tipork = 'O'"
    Dim strTmp As String = "select movord.codditt, td_tipork, td_anno,td_serie, td_numord, td_conto as xx_cpneofconto, an_descr1, mo_riga, mo_codart, mo_descr, mo_desint, mo_quant, mo_prezzo, mo_quaeva, mo_flevas, mo_commeca, mo_subcommeca, mo_note, mo_scont1, mo_scont2, mo_scont3, mo_scont4, mo_scont5, mo_scont6, mo_valoremm  FROM (anagra INNER JOIN testord ON (anagra.codditt = testord.codditt) AND (anagra.an_conto = testord.td_conto)) INNER JOIN movord ON (testord.codditt = movord.codditt) AND (testord.td_numord = movord.mo_numord) AND (testord.td_serie = movord.mo_serie) AND (testord.td_anno = movord.mo_anno) AND (testord.td_tipork = movord.mo_tipork) where mo_commeca =  " & IntCommessa & " and mo_Commeca <> 0 and mo_tipork = " & CStrSQL(StrTipork)

    If ds.Tables.Contains("TESTA") Then
      If ds.Tables("TESTA").Rows(0)!et_tipork.ToString <> "R" Then
        'strTmp += "and mo_tipork <> 'O'"
        strTmp += "and mo_tipork <> " & CStrSQL(StrTipork)
      End If
    End If
    Dim dt As DataTable = OpenRecordset(strTmp, DBTIPO.DBAZI)
    If ds.Tables.Contains("CPNE_OF") Then
      ds.Tables("CPNE_OF").Rows.Clear()
    Else
      ds = OpenRecordset(strTmp & " and td_conto = 0 order by mo_subcommeca, mo_riga", DBTIPO.DBAZI, "CPNE_OF", ds)
    End If
    Dim dr As DataRow = Nothing
    For qq = 0 To dt.Rows.Count - 1
      dr = dt.Rows(qq)
      ds.Tables("CPNE_OF").Rows.Add()
      With ds.Tables("CPNE_OF").Rows(ds.Tables("CPNE_OF").Rows.Count - 1)
        For kk = 0 To dt.Columns.Count - 1
          .Item(kk) = dr.Item(kk)
        Next
      End With
    Next
  End Sub
  Public Sub CPNECaricaScadPag(ByRef ds As DataSet, strDitta As String, strtipork As String, intAnno As Integer, strSerie As String, intNumord As Integer)
    Dim strSql As String
    strSql = "Select hh_riga, hh_codpaga, tb_despaga AS xx_despaga, hh_imppaga, hh_datpaga, hh_flgpaga, hh_dataft, hh_flgft,hh_annoft,hh_serieft,hh_numft,hh_autft"
    strSql += " From hhscapag LEFT JOIN tabpaga ON hhscapag.hh_codpaga = tabpaga.tb_codpaga"
    strSql += " where codditt = " & CStrSQL(strDitta) & " and hh_tipork = " & CStrSQL(strtipork) & " and hh_anno = " & intAnno & " and hh_serie = " & CStrSQL(strSerie) & " and hh_numord = " & intNumord
    strSql += " Order by hh_riga"
    ds = OpenRecordset(strSql, DBTIPO.DBAZI, "CPNE.ScaPag", ds)
  End Sub
  Public Sub CancellaScadPag(strDitta As String, strtipork As String, intAnno As Integer, strSerie As String, intNumord As Integer)
    Dim strSql As String
    strSql = "Delete from hhscapag"
    strSql += " where codditt = " & CStrSQL(strDitta) & " and hh_tipork = " & CStrSQL(strtipork) & " and hh_anno = " & intAnno & " and hh_serie = " & CStrSQL(strSerie) & " and hh_numord = " & intNumord
    Execute(strSql, CLE__APP.DBTIPO.DBAZI)
  End Sub
  Public Sub AggiornaDatiFtScadPag(strDitta As String, strtipork As String, intAnno As Integer, strSerie As String, intNumord As Integer, intRiga As Integer, intAnnoDoc As Integer, strSerieDoc As String, intNumDoc As Integer, dtDataDoc As Date)
    Dim strSql As String
    strSql = "Update hhscapag"
    strSql += " set hh_serieft = " & CStrSQL(strSerieDoc)
    strSql += ",hh_annoft = " & intAnnoDoc
    strSql += ", hh_numft = " & intNumDoc
    strSql += ", hh_dataft = " & CDataOraSQL(dtDataDoc)
    strSql += ",hh_flgft = 'F',hh_autft = 'A',hh_flgpaga = 3"
    strSql += " where codditt = " & CStrSQL(strDitta) & " and hh_tipork = " & CStrSQL(strtipork) & " and hh_anno = " & intAnno & " and hh_serie = " & CStrSQL(strSerie) & " and hh_numord = " & intNumord & " and hh_riga = " & intRiga
    Execute(strSql, CLE__APP.DBTIPO.DBAZI)
  End Sub
  Public Sub AnnullaDatiFtScadPag(strDitta As String, strtipork As String, intAnno As Integer, strSerie As String, intNumord As Integer, intRiga As Integer)
    Dim strSql As String
    strSql = "Update hhscapag"
    strSql += " set hh_serieft = " & CStrSQL("")
    strSql += ",hh_annoft = 0"
    strSql += ", hh_numft = 0"
    strSql += ", hh_dataft = " & CDataOraSQL("01/01/1900")
    strSql += ",hh_flgft = '',hh_autft = '',hh_flgpaga = 1"
    strSql += " where codditt = " & CStrSQL(strDitta) & " and hh_tipork = " & CStrSQL(strtipork) & " and hh_anno = " & intAnno & " and hh_serie = " & CStrSQL(strSerie) & " and hh_numord = " & intNumord & " and hh_riga = " & intRiga
    Execute(strSql, CLE__APP.DBTIPO.DBAZI)
  End Sub
  Public Sub CreaScadPag(strDitta As String, strtipork As String, intAnno As Integer, strSerie As String, intNumord As Integer, intcommeca As Integer, dr As DataRow)
    Dim strSql As String
    strSql = "INSERT INTO hhscapag"
    strSql += " (codditt,hh_tipork,hh_anno,hh_serie,hh_numord,hh_riga,hh_commeca,hh_codpaga,hh_imppaga,hh_datpaga,hh_flgpaga,hh_annoft,hh_serieft,hh_numft,hh_dataft,hh_flgft,hh_autft)"
    strSql += " values"
    strSql += " (" & CStrSQL(strDitta) & "," & CStrSQL(strtipork) & "," & intAnno & "," & CStrSQL(strSerie) & "," & intNumord & "," & dr!hh_riga.ToString & ","
    strSql += intcommeca & "," & dr!hh_codpaga.ToString & "," & CDblSQL(dr!hh_imppaga) & "," & CDataSQL(dr!hh_datpaga) & "," & CStrSQL(dr!hh_flgpaga) & "," & dr!hh_annoft.ToString & ","
    strSql += CStrSQL(dr!hh_serieft) & "," & dr!hh_numft.ToString & "," & CDataSQL(dr!hh_dataft) & "," & CStrSQL(dr!hh_flgft) & "," & CStrSQL(dr!hh_autft) & ")"
    Execute(strSql, CLE__APP.DBTIPO.DBAZI)
  End Sub
  Public Sub GeneraSottoCommessa(ByVal IntCommessa As Integer, ByVal IntRiga As Integer, ByVal ditta As String)
    Dim dt As DataTable = OpenRecordset("select * from subcomm where sco_commeca= " & IntCommessa & " and sco_subcommeca = " & IntRiga & " and codditt = " & CStrSQL(ditta), DBTIPO.DBAZI)
    If dt.Rows.Count = 0 Then
      Execute("INSERT INTO subcomm (sco_commeca, sco_subcommeca, codditt)  values (" & IntCommessa & ", " & IntRiga & ", " & CStrSQL(ditta) & ")", DBTIPO.DBAZI)
    End If
  End Sub

  Public Sub CPNEPulisciDs(ByRef ds As DataSet, ByVal StrNomeTab As String)
    Dim str As String = StrNomeTab
    If ds.Tables.Contains(StrNomeTab) Then
      ds.Tables(StrNomeTab).Clear()
      ds.Tables.Remove(StrNomeTab)
    End If
  End Sub
  Public Function CPNEEsisteOrd(StrTipork As String, IntAnno As Integer, StrSerie As String, IntNumOrd As Integer, Ditta As String) As Boolean
    Dim Dt As DataTable = OpenRecordset("select td_tipork from testord where codditt = " & CStrSQL(Ditta) & " and td_tipork = " & CStrSQL(StrTipork) & " and td_anno = " & IntAnno & " and td_serie = " & CStrSQL(StrSerie) & " and td_numord = " & IntNumOrd.ToString, DBTIPO.DBAZI)
    If Dt.Rows.Count = 0 Then
      Return False
    Else
      Return True
    End If
  End Function
  Public Function CPNELeggiUltCos(strCodart As String, Ditta As String) As Decimal
    Dim strSql As String
    Dim Dt As DataTable
    strSql = "SELECT apx_ultcos"
    strSql += " FROM artprox"
    strSql += " WHERE codditt=" & CStrSQL(Ditta) & "  AND apx_codart=" & CStrSQL(strCodart)
    Dt = OpenRecordset(strSql, DBTIPO.DBAZI)
    Return CDec(Dt.Rows(0)!apx_ultcos)
  End Function
  Public Function CPNEControlloSquadra(strDitta As String, strSquadra As String) As Boolean
    Dim strSQL As String = ""
    Dim dtTmp As DataTable
    strSQL = "SELECT top 1 (OpDescont + ' ' + OpDescont2) as xx_operat FROM operat" & _
               " WHERE OpNome = " & CStrSQL(strSquadra)
    dtTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
    If dtTmp.Rows.Count > 0 Then
      Return True
    Else
      Return False
    End If
  End Function
  Public Function CPNEDecodificaSquadra(strDitta As String, strSquadra As String) As String
    Dim strSQL As String = ""
    Dim dtTmp As DataTable
    Dim strdes1 As String = ""
    Dim strdes2 As String = ""
    strSQL = "SELECT top 1 OpDescont ,OpDescont2 FROM operat" & _
               " WHERE OpNome = " & CStrSQL(strSquadra)
    dtTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
    If dtTmp.Rows.Count > 0 Then
      If Not IsDBNull(dtTmp.Rows(0)!OpDescont) Then
        strdes1 = dtTmp.Rows(0)!OpDescont.ToString
      End If
      If Not IsDBNull(dtTmp.Rows(0)!OpDescont2) Then
        strdes2 = dtTmp.Rows(0)!OpDescont2.ToString
      End If
      Return strdes1 & " " & strdes2
    Else
      Return ""
    End If
  End Function
  ' ========================================================================================================
  ' GestioneCorrispIvaEsclusa INIZIO
  ' ========================================================================================================
  Public Function CPNETrovaAliquotaIva(ByVal codiva As Integer, ByRef bEsente As Boolean) As Decimal
    Dim DtTemp As DataTable = Nothing
    Dim StrSQL As String
    bEsente = False
    StrSQL = "SELECT tb_codciva, tb_aliq, tb_tipiva"
    StrSQL += " FROM tabciva"
    StrSQL += " WHERE tb_codciva=" & codiva

    DtTemp = Me.OpenRecordset(StrSQL, CLE__APP.DBTIPO.DBAZI)
    If DtTemp.Rows.Count > 0 Then
      If CInt(DtTemp.Rows(0).Item("tb_tipiva")) = 1 Then
        bEsente = False
      Else
        bEsente = True
      End If
      Return CDec(DtTemp.Rows(0).Item("tb_aliq"))
    Else
      Return 0
    End If
  End Function
  ' ========================================================================================================
  ' GestioneCorrispIvaEsclusa FINE
  ' ========================================================================================================
  Public Function CPNELeggihhmovoffOf(strDitta As String, strTipoDoc As String, nAnno As Integer, strSerie As String, lNumdoc As Integer, lRevisione As Integer, IntRiga As Integer) As DataTable
    Dim StrSql As String = ""
    Try
      StrSql = ""
      StrSql = "Select hhmovoffOf.*"
      StrSql += " , an_descr1 As xx_descr1 "
      StrSql += " from hhmovoffOf inner join anagra on hhmovoffOf.codditt = anagra.codditt And hhmovoffOf.hh_conto = anagra.an_conto "
      StrSql += " where hhmovoffOf.codditt = " & CStrSQL(strDitta)
      StrSql += " And hhmovoffOf.hh_tipork = " & CStrSQL(strTipoDoc)
      StrSql += " And hhmovoffOf.hh_anno = " & nAnno
      StrSql += " And hhmovoffOf.hh_serie  = " & CStrSQL(strSerie)
      StrSql += " And hhmovoffOf.hh_numord =  " & lNumdoc
      StrSql += " And hhmovoffOf.hh_vers  = " & lRevisione
      StrSql += " And hhmovoffOf.hh_riga  = " & IntRiga
      Return OpenRecordset(StrSql, CLE__APP.DBTIPO.DBAZI)
    Catch ex As Exception
      '--------------------------------------------------------------
      CLN__STD.GestErr(ex, Me, StrSql)
      Return Nothing
      '--------------------------------------------------------------
    End Try
  End Function
End Class
