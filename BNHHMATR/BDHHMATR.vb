Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO
Imports System
Imports NTSInformatica.CLE__APP
Public Class CLDHHMATR
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

  Public Function CPNELeggiTestord(ByVal StrDitta As String) As DataTable
    Return OpenRecordset("select * from testord where td_tipork ='O' and td_anno='2017' and codditt = " & CStrSQL(StrDitta), DBTIPO.DBAZI)
  End Function

  Public Function CPNELeggiMovord(ByVal StrDitta As String, ByVal strAnno As String, ByVal strSerie As String, ByVal IntNumord As Integer) As DataTable
    Return OpenRecordset("select * from movord where mo_tipork ='O' and codditt = " & CStrSQL(StrDitta) & " and mo_anno = " & CStrSQL(strAnno) & " and mo_serie = " & CStrSQL(strSerie) & " and mo_numord = " & IntNumord, DBTIPO.DBAZI)
  End Function

  Public Function CPNELeggiAnagra(ByVal StrDitta As String, ByVal StrConto As String) As DataTable
    Return OpenRecordset("select * from anagra where an_tipo='C' and an_conto = " & CStrSQL(StrConto) & " and codditt = " & CStrSQL(StrDitta), DBTIPO.DBAZI)
  End Function

  Public Sub CPNECaricaDTVeicoli(ByRef ds As DataSet, intCodCli As Integer)
    Dim strSql As String
    strSql = "Select * from hhveicoli where hh_codcli=" & intCodCli
    OpenRecordset(strSql, CLE__APP.DBTIPO.DBAZI, "HHVEICOLI", ds)
  End Sub
End Class
