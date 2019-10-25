Imports System.Data
Imports System.Data.Common
Imports NTSInformatica.CLN__STD

Imports System
Public Class CLHCRGSOF
  Inherits CLDCRGSOF
  Public Sub CPNEPulisciDs(ByRef ds As DataSet, ByVal StrNomeTab As String)
    Dim str As String = StrNomeTab
    If ds.Tables.Contains(StrNomeTab) Then
      ds.Tables(StrNomeTab).Clear()
      ds.Tables.Remove(StrNomeTab)
    End If
  End Sub

  Public Sub CPNELeggihhmovoffOf(ByRef ds As DataSet, strDitta As String, bNew As Boolean, strTipoDoc As String, nAnno As Integer, strSerie As String, lNumdoc As Integer, lRevisione As Integer)
    Dim StrSql As String = ""
    Try
      CPNEPulisciDs(ds, "hhmovoffOf")
      StrSql = ""
      StrSql = "Select hhmovoffOf.*"
      StrSql += " , an_descr1 As xx_descr1 "
      StrSql += " from hhmovoffOf inner join anagra on hhmovoffOf.codditt = anagra.codditt And hhmovoffOf.hh_conto = anagra.an_conto "
      StrSql += " where hhmovoffOf.codditt = " & CStrSQL(strDitta)
      If bNew Then
        StrSql += " and hhmovoffOf.codditt <> " & CStrSQL(strDitta)
      Else
        StrSql += " And hhmovoffOf.hh_tipork = " & CStrSQL(strTipoDoc)
        StrSql += " And hhmovoffOf.hh_anno = " & nAnno
        StrSql += " And hhmovoffOf.hh_serie  = " & CStrSQL(strSerie)
        StrSql += " And hhmovoffOf.hh_numord =  " & lNumdoc
        StrSql += " And hhmovoffOf.hh_vers  = " & lRevisione
      End If
      ds = OpenRecordset(StrSql, CLE__APP.DBTIPO.DBAZI, "hhmovoffOf", ds)
    Catch ex As Exception
      '--------------------------------------------------------------
      CLN__STD.GestErr(ex, Me, StrSql)
      '--------------------------------------------------------------
    End Try
  End Sub

End Class