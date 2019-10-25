Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp
Imports NTSInformatica.CLE__APP
Imports System.IO


Public Class CLEHHMATR
  Inherits CLE__BASE
  Public oCldhh As CLDHHMATR
  Dim strErr As String = ""
  Dim oTmp As Object = Nothing
  Public OMenu As Object
  Dim drxxx As DataRow
  Public table As New DataTable



  Public Sub AssociaDs(ByRef ds As DataSet)
    dsShared = ds
  End Sub

  Public Overrides Function Init(ByRef App As CLE__APP,
                           ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String,
                           ByVal bFiller1 As Boolean, ByVal strFiller1 As String,
                           ByVal strFiller2 As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDHHMATR"

    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, False, "", "")
    oCldhh = CType(MyBase.ocldBase, CLDHHMATR)
    oCldhh.Init(oApp)



    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEHHMATR", "BEHHMATR", oTmp, strErr, False, "", "") = False Then
      Throw New NTSException(oApp.Tr(Me, 128607611686875000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If

    dsShared.Tables.Add("XXX")
    With dsShared.Tables("XXX")
      '.Columns.Add("",GetType())
      .Columns.Add("xx_codCli", GetType(Integer))
      .Columns.Add("xx_descr", GetType(String))
      .Columns.Add("xx_targa", GetType(String))
      .Columns.Add("xx_tipo", GetType(String))
      .Columns.Add("xx_dataimmatr", GetType(String))
      .Columns.Add("xx_telaio", GetType(String))
      .Columns.Add("xx_nrmotore", GetType(String))
      .Columns.Add("xx_km", GetType(Integer))
      .Columns.Add("xx_note", GetType(String))

      .Rows.Add()
      drxxx = .Rows(0)
      With .Rows(0)
        '!=""
        !xx_codCli = 0
        !xx_descr = ""
        !xx_targa = ""
        !xx_tipo = ""
        !xx_dataimmatr = ""
        !xx_telaio = ""
        !xx_nrmotore = ""
        !xx_km = 0
        !xx_note = ""


      End With
    End With
    AddHandler dsShared.Tables("XXX").ColumnChanging, AddressOf CPNEBeforeColUpdate_XXX
    AddHandler dsShared.Tables("XXX").ColumnChanged, AddressOf CPNEAfterColUpdate_XXX

    Return True
  End Function


  Public Overridable Sub CPNEBeforeColUpdate_XXX(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtXTest As New DataTable
    Dim StrXTest As String = ""
    'If e.Row!campo.ToString = "" And e.Column.ColumnName <> "campo" Then
    '  'e.Row!campo = valore
    'End If
    e.ProposedValue = UCase(e.ProposedValue.ToString)
    Select Case e.Column.ColumnName.ToLower
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
          e.ProposedValue = "0" ' oppure "" a seconda del tipo di campo
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
    SalvaGriglia()
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

      SalvaGriglia()
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
  Public Sub SalvaGriglia()
    oCldhh.ScriviTabellaSemplice(strDittaCorrente, "HHVEICOLI", dsShared.Tables("HHVEICOLI"), "", "", "")
  End Sub

  Public Sub Elaborarighe()
    Try

    Catch ex As Exception
      '--------------------------------------------------------------

      CLN__STD.GestErr(ex, Me, "")

      '--------------------------------------------------------------
    End Try


  End Sub

  Public Sub CPNECaricaDTVeicoli(intCodCli As Integer)
    oCldhh.CPNEPulisciDs(dsShared, "HHVEICOLI")
    oCldhh.CPNECaricaDTVeicoli(dsShared, intCodCli)
  End Sub

  Public Function LeggiAnagra(strdittacorrente As String, intCodCli As Integer) As String
    Dim dtAnagra As DataTable
    dtAnagra = oCldhh.CPNELeggiAnagra(strdittacorrente, intCodCli.ToString)
    If dtAnagra.Rows.Count > 0 Then
      Return dtAnagra.Rows(0)!an_descr1.ToString
    End If
  End Function

End Class
