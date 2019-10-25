Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp
Imports NTSInformatica.CLE__APP
Public Class CLEHHMMTR
  Inherits CLE__BASE
  Public oCldhh As CLDHHMMTR
  Dim strErr As String = ""
  Dim oTmp As Object = Nothing
  Public OMenu As Object
  Public drxxx As DataRow
  Dim IntCPNEAConto As Integer = 9999999
  Public Sub AssociaDs(ByRef ds As DataSet)
    dsShared = ds
  End Sub

  Public Overrides Function Init(ByRef App As CLE__APP,
                           ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String,
                           ByVal bFiller1 As Boolean, ByVal strFiller1 As String,
                           ByVal strFiller2 As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDHHMMTR"

    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, False, "", "")
    oCldhh = CType(MyBase.ocldBase, CLDHHMMTR)
    oCldhh.Init(oApp)



    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEHHMMTR", "BEHHMMTR", oTmp, strErr, False, "", "") = False Then
      Throw New NTSException(oApp.Tr(Me, 128607611686875000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If

    dsShared.Tables.Add("XXX")
    Dim Dtxxx As DataTable = dsShared.Tables("XXX")
    Dtxxx.Columns.Add("xx_dadtord", GetType(Date))
    Dtxxx.Columns.Add("xx_adtord", GetType(Date))
    Dtxxx.Columns.Add("xx_dadtcons", GetType(Date))
    Dtxxx.Columns.Add("xx_adtcons", GetType(Date))
    Dtxxx.Columns.Add("xx_soloap", GetType(String))
    Dtxxx.Columns.Add("xx_daconto", GetType(Integer))
    Dtxxx.Columns.Add("xx_dacontodesc", GetType(String))
    Dtxxx.Columns.Add("xx_aconto", GetType(Integer))
    Dtxxx.Columns.Add("xx_acontodesc", GetType(String))
    Dtxxx.Columns.Add("xx_dtfatt", GetType(Date))
    Dtxxx.Rows.Add()
    drxxx = Dtxxx.Rows(0)
    drxxx!xx_dadtord = #01/01/1900#
    drxxx!xx_adtord = #12/31/2099#
    drxxx!xx_dtfatt = Today
    drxxx!xx_dadtcons = Today
    drxxx!xx_adtcons = Today
    drxxx!xx_soloap = "S"
    drxxx!xx_daconto = 0
    drxxx!xx_aconto = IntCPNEAConto
    AddHandler dsShared.Tables("XXX").ColumnChanging, AddressOf CPNEBeforeColUpdate_XXX
    AddHandler dsShared.Tables("XXX").ColumnChanged, AddressOf CPNEAfterColUpdate_XXX
    CPNERicerca()
    Return True
  End Function

  Public Sub CPNERicerca()
    oCldhh.CPNERicerca(strDittaCorrente, dsShared, drxxx)
    AddHandler dsShared.Tables("Ric").ColumnChanging, AddressOf CPNEBeforeColUpdate_Ric
    AddHandler dsShared.Tables("Ric").ColumnChanged, AddressOf CPNEAfterColUpdate_Ric
    ThrowRemoteEvent(New NTSEventArgs("CPNE.AggGriglia", ""))
  End Sub
  Public Overridable Sub CPNEBeforeColUpdate_Ric(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtXTest As New DataTable
    Dim StrXTest As String = ""

    Select Case e.Column.ColumnName.ToLower
      Case ""
        If oCldhh.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", StrXTest, dtXTest) Then
          If dtXTest.Rows.Count = 0 Then
            e.Row!xx_campo = ""
          Else
            e.Row!xx_campo = StrXTest
            e.Row!xx_campo1 = dtXTest.Rows(0)!Nomecampo
          End If
        Else
          ThrowRemoteEvent(New NTSEventArgs("", "Il campo non è valido"))
          e.ProposedValue = e.Row.Item(e.Column.ColumnName) ' oppure "" a seconda del tipo di campo
          e.Row!xx_campo = ""
        End If
    End Select
  End Sub
  Public Overridable Sub CPNEAfterColUpdate_Ric(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)

    Try
      e.Row.EndEdit()
      e.Row.EndEdit()
      Select Case e.Column.ColumnName.ToLower
        Case ""

      End Select
    Catch ex As Exception
      '--------------------------------------------------------------

      CLN__STD.GestErr(ex, Me, "")

      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub CPNEBeforeColUpdate_XXX(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtXTest As New DataTable
    Dim StrXTest As String = ""
    Select Case e.Column.ColumnName.ToLower
      Case "xx_daconto", "xx_aconto"
        If CInt(e.ProposedValue) = IntCPNEAConto Then
          e.Row.Item(e.Column.ColumnName.ToLower & "desc") = ""
        Else
          If oCldhh.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", StrXTest, dtXTest) Then
            If dtXTest.Rows.Count = 0 Then
              e.Row.Item(e.Column.ColumnName.ToLower & "desc") = ""
            Else
              If dtXTest.Rows(0)!an_tipo.ToString = "C" Then
                e.Row.Item(e.Column.ColumnName.ToLower & "desc") = StrXTest
              Else
                ThrowRemoteEvent(New NTSEventArgs("", "Il conto non è un cliente"))
                e.ProposedValue = e.Row.Item(e.Column.ColumnName)
              End If
            End If
          Else
            ThrowRemoteEvent(New NTSEventArgs("", "Il conto non è valido"))
            e.ProposedValue = e.Row.Item(e.Column.ColumnName)
          End If
        End If
      Case ""
        If oCldhh.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "tabella", "tipocampochiave", StrXTest, dtXTest) Then
          If dtXTest.Rows.Count = 0 Then
            e.Row!xx_campo = ""
          Else
            e.Row!xx_campo = StrXTest
            e.Row!xx_campo1 = dtXTest.Rows(0)!Nomecampo
          End If
        Else
          ThrowRemoteEvent(New NTSEventArgs("", "Il campo non è valido"))
          e.ProposedValue = e.Row.Item(e.Column.ColumnName) ' oppure "" a seconda del tipo di campo
          e.Row!xx_campo = ""
        End If

    End Select
  End Sub
  Public Overridable Sub CPNEAfterColUpdate_XXX(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)

    Try
      e.Row.EndEdit()
      e.Row.EndEdit()
      Select Case e.Column.ColumnName.ToLower
        Case ""
      End Select
    Catch ex As Exception
      '--------------------------------------------------------------

      CLN__STD.GestErr(ex, Me, "")

      '--------------------------------------------------------------
    End Try
  End Sub

  Public Sub CPNECancRiga(ByVal dr As DataRow)
    If IsNothing(dr) Then
      Return
    End If
    Dim Evento As New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, "Sicuro di cancellare la riga?")
    ThrowRemoteEvent(Evento)
    If Evento.RetValue = ThMsg.RETVALUE_NO Then
      Return
    End If
    dr.Delete()
    SalvaGriglia(dr)
  End Sub

  Public Sub CPNERipristinaRiga(ByVal dr As DataRow)
    If IsNothing(dr) Then
      Return
    End If
    Dim Evento As New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, "Sicuro di ripristinare la riga?")
    ThrowRemoteEvent(Evento)
    If Evento.RetValue = ThMsg.RETVALUE_NO Then
      Return
    End If
    dr.RejectChanges()

  End Sub

  Public Function CPNEBeforeUpdateGriglia(ByVal dr As DataRow) As Boolean
    Try
      If IsNothing(dr) Then
        Return True
      End If
      '''test compiati tutti i campi se no return false
      'If dr!.ToString = "" Then
      '  ThrowRemoteEvent(New NTSEventArgs("", "Prima inserire"))
      '  Return False
      'End If

      SalvaGriglia(dr)
    Catch ex As Exception
      If InStr(ex.Message.ToUpper, " PRIMARY ") <> 0 Or InStr(ex.Message.ToUpper, " PRIMARIA ") <> 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", "ATTENZIONE COMBINAZIONE GIA' UTILIZZATA!!!" & vbCrLf & "CAMBIARLA O RIPRISITNARE"))
      Else

        CLN__STD.GestErr(ex, Me, "")

      End If
      Return False
    End Try
    Return True
  End Function
  Private Sub SalvaGriglia(dr As DataRow)
    dr.AcceptChanges()
  End Sub
  Public Function CPNEGeneraDocumento() As Boolean
    Dim StrFatture As String
    StrFatture = ""
    Dim DrsRic As DataRow() = dsShared.Tables("Ric").Select("xx_sel = 'S'", "td_conto, td_tipobf, td_codagen, td_codpaga")
    If DrsRic.Length = 0 Then
      ThrowRemoteEvent(New NTSEventArgs("", "Prima selezionare almeno una riga"))
      Return False
    End If


    Dim oCleDoc As CLEVEBOLL
    Dim dsOrd As New DataSet
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEHHORFO", "BEVEBOLL", oTmp, strErr, False, "", "") = False Then
      Throw New NTSException(oApp.Tr(Me, 128607611686875000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleDoc = CType(oTmp, CLEVEBOLL)
    AddHandler oCleDoc.RemoteEvent, AddressOf GestisciEventiEntityDoc
    If oCleDoc.Init(oApp, oScript, oCleComm, "", False, "", "") = False Then Return False
    If Not oCleDoc.InitExt() Then Return False
    oCleDoc.strDittaCorrente = strDittaCorrente
    oCleDoc.ApriDoc(strDittaCorrente, True, "O", 0, " ", 0, dsOrd)



    For i = 0 To DrsRic.Length - 1
      Dim DrRic As DataRow = DrsRic(i)
      Dim bNuovo As Boolean = False
      If i = 0 Then
        bNuovo = True
      Else
        Dim Dro As DataRow = DrsRic(i - 1)
        If CInt(DrRic!td_conto) <> CInt(Dro!td_conto) Or CInt(DrRic!td_tipobf) <> CInt(DrRic!td_tipobf) Or CInt(DrRic!td_codagen) <> CInt(Dro!td_codagen) Or CInt(DrRic!td_codpaga) <> CInt(Dro!td_codpaga) Then
          If oCleDoc.SalvaDocumento("N") Then '"N" =  NUOVO; "U" = UPDATE; "D" = DELETE
            StrFatture += oCleDoc.dttET.Rows(0)!et_numdoc.ToString & " - "
          Else
            ThrowRemoteEvent(New NTSEventArgs("", "Elaborazione interrotta" & vbCrLf & "Elaborate solo le fatture " & StrFatture & ""))
            Return False
          End If
          bNuovo = True
        End If
      End If
      If bNuovo Then
        Dim strDtacTipork = "A"
        Dim strDtacSerie As String = oCldhh.GetSettingBus("CPNE", "BEHHDORO", "OPZIONI", "SerieFattura", " ", " ", " ")
        Dim nDtacAnno As Integer = Year(CDate(drxxx!xx_dtfatt))
        Dim lNumDoc As Integer = 0
        If lNumDoc = 0 Then
          lNumDoc = oCleDoc.LegNuma(strDtacTipork, strDtacSerie, nDtacAnno)
        End If
        If lNumDoc = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128699231787692325, "Prima di creare un nuovo documento è necessario attivare la numerazione ")))
          Return False
        End If
        oCleDoc.NuovoDocumento(strDittaCorrente, strDtacTipork, nDtacAnno, strDtacSerie, lNumDoc)
        Dim Drt As DataRow = oCleDoc.dttET.Rows(0)
        Drt!et_datdoc = drxxx!xx_dtfatt
        Drt!et_conto = CInt(DrRic!td_conto)
        Drt!et_tipobf = CInt(DrRic!td_tipobf)
        Drt!et_codagen = CInt(DrRic!td_codagen)
        Drt!et_codpaga = CInt(DrRic!td_codpaga)
      End If
      Dim DtRigheDaImport As New DataTable
      oCleDoc.oCldBoll.GetRigaOrdineDaEvadereLikeHLMO(strDittaCorrente, DrRic!mo_tipork.ToString, CInt(DrRic!mo_anno), DrRic!mo_serie.ToString, CInt(DrRic!mo_numord), CInt(DrRic!mo_riga), DtRigheDaImport)
      If DtRigheDaImport.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", "Problema con l'importazione dell'ordine " & DrRic!mo_numord.ToString & " riga " & DrRic!mo_riga.ToString & vbCrLf & "Elaborazione interrotta" & vbCrLf & "Elaborate solo le fatture " & StrFatture & ""))
        Return False
      End If
      oCleDoc.ImportaOrdini("R", DtRigheDaImport)
    Next


    ' SE DEVO APRIRE DOCUMENTO
    ' oCleDoc.ApriDoc(strDittaCorrente, True, strDtacTipork, nDtacAnno, strDtacSerie, lNumDoc, dsOrd)
    If Not oCleDoc.SalvaDocumento("N") Then '"N" =  NUOVO; "U" = UPDATE; "D" = DELETE
      Return False
    End If
    StrFatture += oCleDoc.dttET.Rows(0)!et_numdoc.ToString & " - "
    ThrowRemoteEvent(New NTSEventArgs("", "Fattura/e " & StrFatture & " generata/e !!!!"))
    CPNERicerca()
    Return True
  End Function




  Public Sub GestisciEventiEntityDoc(ByVal sender As Object, ByRef e As NTSEventArgs)
    ThrowRemoteEvent(e)
  End Sub
End Class
