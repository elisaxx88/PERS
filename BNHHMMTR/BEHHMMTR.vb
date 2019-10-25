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

  Public dtmatr As DataTable
  Public dtxxx As DataTable

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
    Dtxxx.Columns.Add("xx_matricola", GetType(String))
    Dtxxx.Columns.Add("xx_matricolaproduttore", GetType(String))
    Dtxxx.Columns.Add("xx_articolo", GetType(String))
    Dtxxx.Columns.Add("xx_duratagaranzia", GetType(String))
    Dtxxx.Columns.Add("xx_usato", GetType(String))
    Dtxxx.Columns.Add("xx_muletto", GetType(String))
    Dtxxx.Columns.Add("xx_dimesso", GetType(String))
    Dtxxx.Columns.Add("xx_vendutodaterzi", GetType(String))
    Dtxxx.Columns.Add("xx_descr", GetType(String))
    Dtxxx.Columns.Add("xx_noleggiovendita", GetType(String))
    Dtxxx.Rows.Add()
    drxxx = Dtxxx.Rows(0)
    drxxx!xx_matricola = ""
    drxxx!xx_matricolaproduttore = ""
    drxxx!xx_articolo = ""
    drxxx!xx_duratagaranzia = ""
    drxxx!xx_usato = ""
    drxxx!xx_muletto = ""
    drxxx!xx_dimesso = ""
    drxxx!xx_vendutodaterzi = ""
    drxxx!xx_descr = ""
    drxxx!xx_noleggiovendita = ""

    dsShared.Tables.Add("dtmatr")
    dsShared.Tables("dtmatr").Columns.Add("xx_matricola", GetType(String))
    dsShared.Tables("dtmatr").Columns.Add("xx_matrproduttore", GetType(String))
    dsShared.Tables("dtmatr").Columns.Add("xx_codart", GetType(String))
    dsShared.Tables("dtmatr").Columns.Add("xx_note", GetType(String))
    dsShared.Tables("dtmatr").Columns.Add("xx_contocli", GetType(String))
    dsShared.Tables("dtmatr").Columns.Add("xx_descr1", GetType(String))
    dsShared.Tables("dtmatr").Columns.Add("xx_datainstallazione", GetType(String))
    dsShared.Tables("dtmatr").Columns.Add("xx_dataritiro", GetType(String))
    dsShared.Tables("dtmatr").Columns.Add("xx_ubicazione", GetType(String))
    dtmatr = dsShared.Tables("dtmatr")


    'AddHandler dsShared.Tables("XXX").ColumnChanging, AddressOf CPNEBeforeColUpdate_XXX
    'AddHandler dsShared.Tables("XXX").ColumnChanged, AddressOf CPNEAfterColUpdate_XXX
    'CPNERicerca()
    Return True
  End Function

  'Public Sub CPNERicerca(strMatricola As String)
  '  oCldhh.CPNERicerca(strDittaCorrente, dsShared, drxxx, strMatricola)
  '  'AddHandler dsShared.Tables("Ric").ColumnChanging, AddressOf CPNEBeforeColUpdate_Ric
  '  'AddHandler dsShared.Tables("Ric").ColumnChanged, AddressOf CPNEAfterColUpdate_Ric
  '  ThrowRemoteEvent(New NTSEventArgs("CPNE.AggGriglia", ""))
  'End Sub
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

  'Public Overridable Sub CPNEBeforeColUpdate_XXX(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
  '  Dim dtXTest As New DataTable
  '  Dim StrXTest As String = ""
  '  Select Case e.Column.ColumnName.ToLower
  '    Case "xx_daconto", "xx_aconto"
  '      If CInt(e.ProposedValue) = IntCPNEAConto Then
  '        e.Row.Item(e.Column.ColumnName.ToLower & "desc") = ""
  '      Else
  '        If oCldhh.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", StrXTest, dtXTest) Then
  '          If dtXTest.Rows.Count = 0 Then
  '            e.Row.Item(e.Column.ColumnName.ToLower & "desc") = ""
  '          Else
  '            If dtXTest.Rows(0)!an_tipo.ToString = "C" Then
  '              e.Row.Item(e.Column.ColumnName.ToLower & "desc") = StrXTest
  '            Else
  '              ThrowRemoteEvent(New NTSEventArgs("", "Il conto non è un cliente"))
  '              e.ProposedValue = e.Row.Item(e.Column.ColumnName)
  '            End If
  '          End If
  '        Else
  '          ThrowRemoteEvent(New NTSEventArgs("", "Il conto non è valido"))
  '          e.ProposedValue = e.Row.Item(e.Column.ColumnName)
  '        End If
  '      End If
  '    Case ""
  '      If oCldhh.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "tabella", "tipocampochiave", StrXTest, dtXTest) Then
  '        If dtXTest.Rows.Count = 0 Then
  '          e.Row!xx_campo = ""
  '        Else
  '          e.Row!xx_campo = StrXTest
  '          e.Row!xx_campo1 = dtXTest.Rows(0)!Nomecampo
  '        End If
  '      Else
  '        ThrowRemoteEvent(New NTSEventArgs("", "Il campo non è valido"))
  '        e.ProposedValue = e.Row.Item(e.Column.ColumnName) ' oppure "" a seconda del tipo di campo
  '        e.Row!xx_campo = ""
  '      End If

  '  End Select
  'End Sub



  Public Sub CPNERipristinaRiga(ByVal dr As DataRow)
    If IsNothing(dr) Then
      Return
    End If
    Dim Evento As New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, "Sicuro di ripristinare la matricola?")
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

  Public Sub GestisciEventiEntityDoc(ByVal sender As Object, ByRef e As NTSEventArgs)
    ThrowRemoteEvent(e)
  End Sub

  Public Overrides Function Salva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not TestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      If strActLog <> "-1" Then
        bResult = ocldBase.ScriviTabellaSemplice(strDittaCorrente, strNomeTabella, dsShared.Tables("HHMATRICOLE"), "", "", "")
      Else
        bResult = ocldBase.ScriviTabellaSemplice(strDittaCorrente, strNomeTabella, dsShared.Tables("HHMATRICOLE"),
                  strActLogProg, strActLogNomOggLog, strActLogDesLog)
      End If

      Return bResult
    Catch ex As Exception
      '--------------------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      Return False
      '--------------------------------------------------------------
    End Try
  End Function

  Public Sub CPNECancRiga(ByVal dr As DataRow)
    Try
      If IsNothing(dr) Then
        Return
      End If
      Dim Evento As New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, "Sicuro di cancellare la matricola?")
      ThrowRemoteEvent(Evento)
      If Evento.RetValue = ThMsg.RETVALUE_NO Then
        Return
      End If
      dr.Delete()
      oCldhh.ScriviTabellaSemplice(strDittaCorrente, "hhmatricole", dr.Table, "", "", "")

    Catch ex As Exception
      '--------------------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Function CPNEAggiornaRicercaMatricola(edxx_matricola As String) As Boolean

    Try
      oCldhh.CPNERicercaMatricola(strDittaCorrente, dsShared, edxx_matricola)
      dtmatr = dsShared.Tables("dtmatr")

      'Evento grafico per poter aggiornare la griglia
      ThrowRemoteEvent(New NTSEventArgs("CPNE.AggGriglia", ""))

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      Return False
      '-------------------------------------------------
    End Try
  End Function

  Public Function CPNEValidaRiga(dr As DataRow) As Boolean
    Try
      Dim ev As New NTSEventArgs(ThMsg.MSG_YESNO, "Vuoi salvare?")
      ThrowRemoteEvent(ev)
      If ev.RetValue = ThMsg.RETVALUE_NO Then
        Return False
      End If
      CPNEValidaRigaMatricola(dr)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      'ThrowRemoteEvent(New NTSEventArgs("", ex.ToString))
      Return False
      '--------------------------------------------------------------
    End Try
  End Function

  Public Function CPNEValidaRigaMatricola(dr As DataRow) As Boolean
    Try
      If IsNothing(dr) Then
        Return False
      Else

        oCldhh.ScriviTabellaSemplice(strDittaCorrente, "hhmatricole", dr.Table, "", "", "")
        Return True

      End If
    Catch ex As Exception
      '--------------------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      'ThrowRemoteEvent(New NTSEventArgs("", ex.ToString))
      Return False
      '--------------------------------------------------------------
    End Try
  End Function


  Public Function CPNERicerca(edxx_matricola As String, edxx_articolo As String) As Boolean

    Try
      oCldhh.CPNERicerca(strDittaCorrente, dsShared, edxx_matricola, edxx_articolo)
      'Aggiungi handlers per validazione dei dati (dt)
      AddHandler dsShared.Tables("XXX").ColumnChanging, AddressOf CPNEBeforeColUpdate_XXX
      AddHandler dsShared.Tables("XXX").ColumnChanged, AddressOf CPNEAfterColUpdate_XXX
      dtxxx = dsShared.Tables("XXX")

      'Aggiornamento griglia (rebinding)
      ThrowRemoteEvent(New NTSEventArgs("CPNE.AggiornaGrigliaClienti", ""))
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      Return False
      '-------------------------------------------------
    End Try
  End Function

  Public Function CPNESvuotaGriglia() As Boolean

    Try
      oCldhh.CPNEPulisciDs(dsShared, "XXX")


      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      Return False
      '-------------------------------------------------
    End Try
  End Function


  Public Overridable Sub CPNEBeforeColUpdate_XXX(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)

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

  Public Function CPNECambiaMatricola(edxx_matricola As String, edxx_nuovamatricola As String) As Boolean
    Try
      oCldhh.CPNEUpdateMatricola(strDittaCorrente, edxx_matricola, edxx_nuovamatricola)
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      Return False
      '-------------------------------------------------
    End Try

  End Function


  Public Function CPNESelezionaMatricola(currentRow As DataRow) As Boolean
    Try

      drxxx!xx_matricola = currentRow!xx_matricola.ToString

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      Return False
      '-------------------------------------------------
    End Try
  End Function


End Class
