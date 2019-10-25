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
  Public Sub CPNERicercaMatricola(ditta As String, ds As DataSet, strmatricola As String)
    Dim StrSql As String = ""
    Try

      StrSql = "SELECT hhmatricole.hh_matricola as xx_matricola, hhmatricole.hh_contocli as xx_contocli, hhmatricole.hh_matrproduttore as xx_matrproduttore"
      StrSql += ", hhmatricole.hh_codart as xx_codart, artico.ar_descr as xx_note, hhmatricole.hh_datainstallazione as xx_datainstallazione"
      StrSql += ", hhmatricole.hh_dataritiro as xx_dataritiro, hhmatricole.hh_ubicazione as xx_ubicazione, anagra.an_descr1 as xx_descr"
      StrSql += " from hhmatricole join anagra on hhmatricole.hh_contocli=anagra.an_conto "
      StrSql += " join artico on hhmatricole.hh_codart=artico.ar_codart "
      StrSql += "where hhmatricole.codditt=" & CStrSQL(ditta)
      If strmatricola <> "" Then
        StrSql += "and hhmatricole.hh_matricola like " & CStrSQL("%" & Replace(strmatricola, "*", "%") & "%")
      End If
      CPNEPulisciDs(ds, "dtmatr")
      OpenRecordset(StrSql, CLE__APP.DBTIPO.DBAZI, "dtmatr", ds)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, StrSql, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub


  Public Sub CPNERicerca(ditta As String, ds As DataSet, strmatricola As String, strarticolo As String)
    Dim StrSql As String = ""
    Try

      StrSql = "SELECT hhmatricole.*, (CASE WHEN hhmatricole.hh_noleggiovendita = 1 THEN 'Vendita' ELSE 'Noleggio' END)  AS xx_noleggiovendita, anagra.an_descr1, artico.ar_descr as xx_descr"
      StrSql += " from hhmatricole join anagra on hhmatricole.hh_contocli=anagra.an_conto "
      StrSql += " join artico on hhmatricole.hh_codart=artico.ar_codart "
      StrSql += "where hhmatricole.codditt=" & CStrSQL(ditta)
      If strmatricola <> "" Then
        StrSql += "and hhmatricole.hh_matricola =" & CStrSQL(strmatricola)
      End If
      If strarticolo <> "" Then
        StrSql += "and hhmatricole.hh_codart = " & CStrSQL(strarticolo)
      End If
      CPNEPulisciDs(ds, "XXX")
      OpenRecordset(StrSql, CLE__APP.DBTIPO.DBAZI, "XXX", ds)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, StrSql, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Sub CPNEUpdateMatricola(ditta As String, strmatricola As String, strmatricolaNuova As String)
    Dim StrSql As String = ""

    StrSql = "update hhmatricole set hhmatricole.hh_matricola = " & CStrSQL(strmatricolaNuova)
    StrSql += " where hhmatricole.codditt=" & CStrSQL(ditta)
    StrSql += " and hhmatricole.hh_matricola=" & CStrSQL(strmatricola)

    Try
      Execute(StrSql, DBTIPO.DBAZI)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, StrSql, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub

End Class
