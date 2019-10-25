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
    Try

      Return OpenRecordset("select * from testord where td_tipork ='O' and td_anno='2017' and codditt = " & CStrSQL(StrDitta), DBTIPO.DBAZI)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Function CPNELeggiMovord(ByVal StrDitta As String, ByVal strAnno As String, ByVal strSerie As String, ByVal IntNumord As Integer) As DataTable
    Try

      Return OpenRecordset("select * from movord where mo_tipork ='O' and codditt = " & CStrSQL(StrDitta) & " and mo_anno = " & CStrSQL(strAnno) & " and mo_serie = " & CStrSQL(strSerie) & " and mo_numord = " & IntNumord, DBTIPO.DBAZI)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Function CPNELeggiAnagra(ByVal StrDitta As String, ByVal StrConto As String) As DataTable
    Try

      Return OpenRecordset("select * from anagra where an_tipo='C' and an_conto = " & CStrSQL(StrConto) & " and codditt = " & CStrSQL(StrDitta), DBTIPO.DBAZI)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Sub CPNECaricaDTMatricole(ByRef ds As DataSet, intCodCli As Integer, strcodart As String)
    Dim strSql As String
    Try
      'strSql = "Select * from nnmatrics where rl_codart=" & CStrSQL(strcodart) & " and rl_conto=" & intCodCli
      strSql = "Select * from nnmatrics where rl_conto=" & intCodCli
      OpenRecordset(strSql, CLE__APP.DBTIPO.DBAZI, "NNMATRICS", ds)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSql, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Sub CPNECaricaDTMatricoleconfiltri(ByRef ds As DataSet, intCodconto As Integer, strmatricola As String)
    Dim strSql As String = ""
    Try
      If strmatricola = "" And intCodconto = 0 Then
        strSql = "Select * from nnmatrics"
      Else
        If strmatricola = "" And intCodconto <> 0 Then
          strSql = "Select * from nnmatrics where rl_conto=" & intCodconto
        End If
        If strmatricola <> "" And intCodconto = 0 Then
          strSql = "Select * from nnmatrics where rl_matric=" & CStrSQL(strmatricola)
        End If
        If strmatricola <> "" And intCodconto <> 0 Then
          strSql = "Select * from nnmatrics where rl_matric=" & CStrSQL(strmatricola) & " and rl_conto=" & intCodconto
        End If
      End If
      'strSql = "Select * from nnmatrics where rl_matric=" & CStrSQL(strmatricola) & " and rl_conto=" & intCodconto
      OpenRecordset(strSql, CLE__APP.DBTIPO.DBAZI, "NNMATRICS", ds)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSql, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub
End Class
