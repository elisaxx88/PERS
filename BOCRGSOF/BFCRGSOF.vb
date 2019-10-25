Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.Globalization


Imports System
Imports NTSInformatica

Public Class CLFCRGSOF
  Inherits CLECRGSOF
  Public DsCPNE As New DataSet
  Dim dthhmovoffOf As DataTable
  Public DrHhmovoffOf As DataRow
  Public DrRigaSel As DataRow
  Public _oClhGsof As CLHCRGSOF
  Public Property oClhGsof() As CLHCRGSOF
    Get
      If _oClhGsof Is Nothing Then _oClhGsof = CType(oCldGsof, CLHCRGSOF)
      Return _oClhGsof
    End Get
    Set(ByVal value As CLHCRGSOF)
      _oClhGsof = value
    End Set
  End Property
  Public Overrides Function Init(ByRef App As CLE__APP, ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, strTabella As String, bRemoting As Boolean, strRemoteServer As String, strRemotePort As String) As Boolean
    Init = MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)

  End Function
  Public Overrides Function NuovoOfferta(strDitta As String, strTipoDoc As String, nAnno As Integer, strSerie As String, lNumdoc As Integer, lRevisione As Integer) As Boolean
    NuovoOfferta = MyBase.NuovoOfferta(strDitta, strTipoDoc, nAnno, strSerie, lNumdoc, lRevisione)
    If NuovoOfferta Then
      CPNECollOfferta(strDitta, bNew, strTipoDoc, nAnno, strSerie, lNumdoc, lRevisione)
    End If
  End Function
  Public Overrides Function ApriOfferta(strDitta As String, bNew As Boolean, strTipoDoc As String, nAnno As Integer, strSerie As String, lNumdoc As Integer, lRevisione As Integer, ByRef ds As DataSet) As Boolean
    ApriOfferta = MyBase.ApriOfferta(strDitta, bNew, strTipoDoc, nAnno, strSerie, lNumdoc, lRevisione, ds)
    If ApriOfferta Then
      CPNECollOfferta(strDitta, bNew, strTipoDoc, nAnno, strSerie, lNumdoc, lRevisione)
    End If
  End Function
  Private Sub CPNECollOfferta(strDitta As String, bNew As Boolean, strTipoDoc As String, nAnno As Integer, strSerie As String, lNumdoc As Integer, lRevisione As Integer)
    oClhGsof.CPNELeggihhmovoffOf(DsCPNE, strDitta, bNew, strTipoDoc, nAnno, strSerie, lNumdoc, lRevisione)
    dthhmovoffOf = DsCPNE.Tables("hhmovoffOf")
    AddHandler dthhmovoffOf.ColumnChanging, AddressOf BeforeColUpdate_hhmovoffOf
    AddHandler dthhmovoffOf.ColumnChanged, AddressOf AfterColUpdate_hhmovoffOf
    ThrowRemoteEvent(New NTSEventArgs("CPNE.AggGr", ""))
    oClhGsof.SetTableDefaultValueFromDB("hhmovoffOf", DsCPNE)
    If dttET.Rows.Count > 0 Then
      Dim Dret As DataRow = dttET.Rows(0)
      dthhmovoffOf.Columns("codditt").DefaultValue = strDittaCorrente
      dthhmovoffOf.Columns("hh_tipork").DefaultValue = Dret!et_tipork
      dthhmovoffOf.Columns("hh_anno").DefaultValue = Dret!et_anno
      dthhmovoffOf.Columns("hh_serie").DefaultValue = Dret!et_serie
      dthhmovoffOf.Columns("hh_numord").DefaultValue = Dret!et_numdoc
      dthhmovoffOf.Columns("hh_vers").DefaultValue = Dret!et_vers
    End If
  End Sub
  Public Overrides Function CambiaNumdoc(ByRef ds As DataSet, lNewProgr As Integer, Optional strNewSerie As String = "", Optional nNewAnno As Integer = 0, Optional bTestNumdoc As Boolean = False, Optional nNewVers As Integer = 0) As Boolean
    CambiaNumdoc = MyBase.CambiaNumdoc(ds, lNewProgr, strNewSerie, nNewAnno, bTestNumdoc, nNewVers)
    If CambiaNumdoc Then
      For i = 0 To dthhmovoffOf.Rows.Count - 1
        If dthhmovoffOf.Rows(i).RowState <> DataRowState.Deleted Then
          dthhmovoffOf.Rows(i)!hh_numord = lNewProgr
        End If
      Next
    End If
  End Function

  Public Overrides Function SalvaOfferta(strState As String) As Boolean
    SalvaOfferta = MyBase.SalvaOfferta(strState)
    If SalvaOfferta Then
      If strState = "D" Then
        For i = 0 To dthhmovoffOf.Rows.Count - 1
          dthhmovoffOf.Rows(i).Delete()
        Next
      End If

      '================ RICCARDO 13 05 2019 =====================
      If nAggTipoAggiornamento = 2 Then
        For i = 0 To dthhmovoffOf.Rows.Count - 1
          dthhmovoffOf.Rows(i)!hh_anno = nAggAnno
          dthhmovoffOf.Rows(i)!hh_serie = strAggSerie
          dthhmovoffOf.Rows(i)!hh_numord = lAggNumord
        Next
      End If
      '======================================================
      oCldGsof.ScriviTabellaSemplice(strDittaCorrente, "hhmovoffOf", dthhmovoffOf, "", "", "")
      End If
  End Function

  Private Sub BeforeColUpdate_hhmovoffOf(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If CInt(e.Row!hh_riga) = 0 And e.Column.ColumnName <> "hh_riga" Then
        e.Row!hh_riga = DrRigaSel!ec_riga
        e.Row!hh_ssriga = CInt(dthhmovoffOf.Select("hh_riga = " & e.Row!hh_riga.ToString, "hh_ssriga desc")(0)!hh_ssriga) + 1
      End If
      Dim dTTmp As New DataTable
      Dim StrTmp As String = ""
      Select Case e.Column.ColumnName.ToLower
        Case "hh_codart"
          If oClhGsof.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ARTICO", "S", StrTmp, dTTmp) Then
            If e.ProposedValue.ToString = "" Then
              e.Row!hh_descr = ""
              e.Row!hh_desint = ""
            Else
              e.Row!hh_descr = dTTmp.Rows(0)!ar_descr
              e.Row!hh_desint = dTTmp.Rows(0)!ar_desint
              If e.Row!hh_quant.ToString <> "" Then
                CPNEAggiornaPrzScon(e.ProposedValue.ToString, CDec(e.Row!hh_quant), e.Row)
              End If
            End If
          Else
            ThrowRemoteEvent(New NTSEventArgs("", "Il codice articolo è sbagliato"))
            e.ProposedValue = ""
          End If

        Case "hh_conto"
          If oClhGsof.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "anagra", "N", StrTmp, dTTmp) Then
            If dTTmp.Rows.Count > 0 Then
              If dTTmp.Rows(0)!an_tipo.ToString = "F" Then
                e.Row!xx_descr1 = StrTmp
              Else
                ThrowRemoteEvent(New NTSEventArgs("", "Il codice inserito è un Cliente"))
                e.ProposedValue = e.Row!hh_conto
              End If
            Else
              e.Row!xx_descr1 = StrTmp
            End If
          Else
            ThrowRemoteEvent(New NTSEventArgs("", "Il conto è sbagliato"))
            e.ProposedValue = e.Row!hh_conto
          End If
        Case "hh_quant"
          'CPNEAggiornaPrzScon(e.Row!hh_codart.ToString, CDec(e.ProposedValue), e.Row)
      End Select
    Catch ex As Exception
      '--------------------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '--------------------------------------------------------------
    End Try
  End Sub

  Private Sub CPNEAggiornaPrzScon(ByVal StrCodart As String, ByVal DecQta As Decimal, ByVal dr As DataRow)
    Try
      Dim dPrezzo As Double = 0
      Dim dScont1 As Decimal = 0
      Dim dScont2 As Decimal = 0
      Dim dScont3 As Decimal = 0
      Dim dScont4 As Decimal = 0
      Dim dScont5 As Decimal = 0
      Dim dScont6 As Decimal = 0
      Dim nPromo As Integer = 0
      Dim nCodtpro As Integer = 0
      Dim StrNet As String = "M"
      Dim dttAna As New DataTable
      Dim dttArt As New DataTable
      If StrCodart = "" Then
        Return
      End If
      If CInt(dr!hh_conto) = 0 Then
        Return
      End If
      oClhGsof.ValCodiceDb(StrCodart, strDittaCorrente, "ARTICO", "S", " ", dttArt)

      oClhGsof.ValCodiceDb(dr!hh_conto.ToString, strDittaCorrente, "ANAGRA", "N", " ", dttAna)
      Dim nClascon As Integer = CInt(dttArt.Rows(0)!ar_clascon)
      Dim nClscan As Integer = NTSCInt(dttAna.Rows(0)!an_clascon)

      CType(oCleComm, CLELBMENU).CercaPrezzo(strDittaCorrente, NTSCStr(StrCodart), 0, CInt(dr!hh_conto),
                           CInt(dttAna.Rows(0)!an_listino), dttArt.Rows(0)!ar_unmis.ToString, 0, "P", True, 0, 0,
                           NTSCDate(dttET.Rows(0)!et_datdoc), 0,
                           NTSCDec(DecQta), CDec(dPrezzo), 0, 0, "")
      dr!hh_prezzo = dPrezzo
      If CType(oCleComm, CLELBMENU).CercaSconti(strDittaCorrente, StrCodart, NTSCInt(dr!hh_conto), nClascon, nClscan, "P",
                  True, nCodtpro, NTSCDate(dttET.Rows(0)!et_datdoc), DecQta, dScont1, dScont2,
                  dScont3, dScont4, dScont5, dScont6, nPromo, StrNet, nScperqta) Then
        dr!hh_scont1 = dScont1
        dr!hh_scont2 = dScont2
        dr!hh_scont3 = dScont3
        dr!hh_scont4 = dScont4
        dr!hh_scont5 = dScont5
        dr!hh_scont6 = dScont6
      End If
    Catch ex As Exception
      '--------------------------------------------------------------

      CLN__STD.GestErr(ex, Me, "")

      '--------------------------------------------------------------
    End Try
  End Sub
  Private Sub AfterColUpdate_hhmovoffOf(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      bHasChangesET = True
      e.Row.EndEdit()
      e.Row.EndEdit()

      Select Case e.Column.ColumnName.ToLower
        Case "hh_prezzo", "hh_quant", "hh_scont1", "hh_scont2", "hh_scont3", "hh_scont4", "hh_scont5", "hh_scont6"
          e.Row!hh_valore = ArrDbl(NTSCDec(e.Row!hh_prezzo) * NTSCDec(e.Row!hh_quant) *
                               (100 - NTSCDec(e.Row!hh_scont1)) / 100 *
                               (100 - NTSCDec(e.Row!hh_scont2)) / 100 *
                               (100 - NTSCDec(e.Row!hh_scont3)) / 100 *
                               (100 - NTSCDec(e.Row!hh_scont4)) / 100 *
                               (100 - NTSCDec(e.Row!hh_scont5)) / 100 *
                               (100 - NTSCDec(e.Row!hh_scont6)) / 100, oCldGsof.TrovaNdec(0))
      End Select
    Catch ex As Exception
      '--------------------------------------------------------------

      CLN__STD.GestErr(ex, Me, "")

      '--------------------------------------------------------------
    End Try
  End Sub

  Public Function CPNEValidaRiga(dr As DataRow) As Boolean
    If IsNothing(dr) Then
      Return True
    End If
    If dr.RowState = DataRowState.Unchanged Then
      Return True
    End If
    If IsNothing(dr) Then
      Return True
    End If
    If CInt(dr!hh_conto) = 0 Then
      ThrowRemoteEvent(New NTSEventArgs("", "Prima inserire il codice conto"))
      Return False
    End If
    If dr!hh_codart.ToString = "" Then
      ThrowRemoteEvent(New NTSEventArgs("", "Prima inserire il codice articolo"))
      Return False
    End If
    If Trim(dr!hh_descr.ToString) = "" Then
      ThrowRemoteEvent(New NTSEventArgs("", "Prima inserire la descrizione dell'articolo"))
      Return False
    End If
    CPNECalcolaMargini()
    Return True
  End Function

  Public Sub CPNECalcolaMargini()
    Dim StrRiepilogo As String
    If IsNothing(DrRigaSel) Then
      StrRiepilogo = ""
    Else
      StrRiepilogo = "Tot. ricavo: " & Format(DrRigaSel!ec_valore, "#,##0.00") & "  -  Tot Costo: "
      Dim DecValore As Decimal
      DecValore = 0
      Dim DrshhmovoffOf As DataRow() = dthhmovoffOf.Select("hh_riga = " & DrRigaSel!ec_riga.ToString)
      For i = 0 To DrshhmovoffOf.Length - 1
        Dim DrhhmovoffOf As DataRow = DrshhmovoffOf(i)
        If DrhhmovoffOf.RowState <> DataRowState.Deleted Then
          DecValore += CDec(DrhhmovoffOf!hh_valore)
        End If
      Next
      StrRiepilogo += Format(DecValore, "#,##0.00") & "  -  Tot Margine: " & Format(CDec(DrRigaSel!ec_valore) - DecValore, "#,##0.00")
      ThrowRemoteEvent(New NTSEventArgs("CPNE.Riepilogo", StrRiepilogo))
    End If


  End Sub
  Public Overrides Function CorpoTestPreSalva(ByRef dttCorpo As DataTable, nRow As Integer) As Boolean
    If CPNEValidaRiga(DrHhmovoffOf) Then
      Return MyBase.CorpoTestPreSalva(dttCorpo, nRow)
    Else
      Return False
    End If
  End Function
  Public Overrides Sub BeforeColUpdate_TESTA(sender As Object, e As DataColumnChangeEventArgs)
    MyBase.BeforeColUpdate_TESTA(sender, e)
    If e.Column.ColumnName = "et_vers" Then
      For i = 0 To dthhmovoffOf.Rows.Count - 1
        Dim drhhmovoffOf As DataRow = dthhmovoffOf.Rows(i)
        If drhhmovoffOf.RowState = DataRowState.Deleted Then
          drhhmovoffOf.AcceptChanges()
        Else
          dthhmovoffOf.Rows(i)!hh_vers = e.ProposedValue
          drhhmovoffOf.AcceptChanges()
          drhhmovoffOf.SetAdded()
        End If
      Next
    End If
  End Sub

  Public Sub CPNECancellaRipristinaRiga(ByVal bCancella As Boolean, dr As DataRow)
    If IsNumeric(dr) Then
      ThrowRemoteEvent(New NTSEventArgs("", "Prima posizionarsi su una riga"))
    Else
      If bCancella And dr.RowState = DataRowState.Added Then
        bCancella = False
      End If
      Dim ev As NTSEventArgs
      If bCancella Then
        ev = New NTSEventArgs(ThMsg.MSG_NOYES, "Cancellare la riga?")
      Else
        ev = New NTSEventArgs(ThMsg.MSG_NOYES, "Ripristinare la riga?")
      End If
      ThrowRemoteEvent(ev)
      If ev.RetValue = ThMsg.RETVALUE_YES Then
        If bCancella Then
          dr.Delete()
        Else
          dr.RejectChanges()
        End If
      End If
    End If
  End Sub

End Class
