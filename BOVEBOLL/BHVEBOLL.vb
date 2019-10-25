
Imports NTSInformatica.CLN__STD
Public Class CLHVEBOLL
  Inherits CLDVEBOLL
  Dim bInCheckAcconti As Boolean = False
  Public Function CPNEMostraOrdFColl(ByVal Commessa As Integer, ByVal Sottocommessa As Integer, ByVal ditta As String) As DataTable
    Return OpenRecordset("select * from movord where mo_tipork = 'O' and mo_commeca = " & Commessa & " and mo_subcommeca = " & Sottocommessa & " and codditt = " & CStrSQL(ditta), CLE__APP.DBTIPO.DBAZI)
  End Function
  Public Overrides Function GetWhereHltmAcconti(ByVal strDitta As String, ByVal bDocemesso As Boolean, ByVal nValuta As Integer, ByVal lConto As Integer, _
                                               ByVal strScorpo As String, ByVal lCommeca As Integer, ByVal strDatdoc As String) As String
    GetWhereHltmAcconti = MyBase.GetWhereHltmAcconti(strDitta, bDocemesso, nValuta, lConto, strScorpo, lCommeca, strDatdoc)
    If InStr(GetWhereHltmAcconti, "") <> 0 Then
      GetWhereHltmAcconti = Replace(GetWhereHltmAcconti, "(testmag.tm_tipork = 'A'", "(testmag.tm_tipork = 'C' or testmag.tm_tipork = 'A'")
    End If

  End Function
  Public Overrides Function CheckAcconti(strDitta As String, bDocemesso As Boolean, lConto As Integer, bScorp As Boolean, nValuta As Integer, lCommeca As Integer, strDatdoc As String) As Boolean
    bInCheckAcconti = True
    CheckAcconti = MyBase.CheckAcconti(strDitta, bDocemesso, lConto, bScorp, nValuta, lCommeca, strDatdoc)
    bInCheckAcconti = False
  End Function
  'Public Overrides Function OpenRecordset(strQuery As String, tipoDb As CLE__APP.DBTIPO, strNomeTabella As String, Optional ByRef dsOut As System.Data.DataSet = Nothing, Optional dbConn As System.Data.Common.DbConnection = Nothing) As System.Data.DataSet
  '  If bInCheckAcconti = True Then
  '    strQuery = Replace(strQuery, "tm_tipork = 'A' OR tm_tipork = 'F' OR tm_tipork = 'S'", "tm_tipork = 'A' OR tm_tipork = 'F' OR tm_tipork = 'S' OR tm_tipork = 'C'")
  '  End If
  '  Return MyBase.OpenRecordset(strQuery, tipoDb, strNomeTabella, dsOut, dbConn)
  'End Function
  Public Overrides Function OpenRecordset(strQuery As String, tipoDb As CLE__APP.DBTIPO, Optional dbConn As System.Data.Common.DbConnection = Nothing) As System.Data.DataTable
    If bInCheckAcconti = True Then
      strQuery = Replace(strQuery, "tm_tipork = 'A' OR tm_tipork = 'F' OR tm_tipork = 'S'", "tm_tipork = 'A' OR tm_tipork = 'F' OR tm_tipork = 'S' OR tm_tipork = 'C'")
    End If
    Return MyBase.OpenRecordset(strQuery, tipoDb, dbConn)
  End Function
End Class
