Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO
Imports System
Imports NTSInformatica.CLE__APP
Public Class CLDHHMMTR
  Inherits CLD__BASE

  Public Sub CPNEPulisciDs(ByRef ds As DataSet, ByVal StrNomeTab As String)
    Dim str As String = StrNomeTab
    Try
      If ds.Tables.Contains(StrNomeTab) Then
        ds.Tables(StrNomeTab).Clear()
        ds.Tables.Remove(StrNomeTab)
      End If
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, str, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Sub CPNERicerca(ditta As String, ds As DataSet, drxxx As DataRow)
    Dim StrSql As String
    StrSql = "Select 'N' as xx_sel, td_conto, td_tipobf, td_codagen, td_codpaga, td_datord, td_datord, an_descr1, td_riferim, movord.*"
    StrSql += " FROM (testord INNER JOIN movord ON (testord.td_numord = movord.mo_numord) AND (testord.td_serie = movord.mo_serie) AND (testord.td_anno = movord.mo_anno) AND (testord.td_tipork = movord.mo_tipork) AND (testord.codditt = movord.codditt)) INNER JOIN anagra ON (testord.td_conto = anagra.an_conto) AND (testord.codditt = anagra.codditt)"
    StrSql += " where testord.codditt = " & CStrSQL(ditta)
    StrSql += " and testord.td_tipork<>'Q'"
    StrSql += " and td_datord >= " & CDataSQL(CDate(drxxx!xx_dadtord)) & " and td_datord <= " & CDataSQL(CDate(drxxx!xx_adtord))
    StrSql += " and mo_datcons >= " & CDataSQL(CDate(drxxx!xx_dadtcons)) & " and mo_datcons <= " & CDataSQL(CDate(drxxx!xx_adtcons))
    StrSql += " and td_conto >= " & drxxx!xx_daconto.ToString & " and td_conto <= " & drxxx!xx_aconto.ToString
    If drxxx!xx_soloap.ToString = "S" Then
      StrSql += " and mo_flevas = 'C'"
    End If
    CPNEPulisciDs(ds, "Ric")
    ds = OpenRecordset(StrSql, DBTIPO.DBAZI, "Ric", ds)



  End Sub
End Class
