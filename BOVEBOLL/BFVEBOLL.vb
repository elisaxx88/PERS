Public Class CLFVEBOLL
  Inherits CLEVEBOLL
  Public Overrides Function ScriviRigaOrd(ByVal strTipo As String, ByRef dttCorp As System.Data.DataTable, ByRef dtrMovord As System.Data.DataRow, ByRef dtrMovordTc() As System.Data.DataRow, ByVal dSc1 As Decimal, ByVal dSc2 As Decimal, ByVal dScPag As Decimal, ByVal nMagClav As Integer, ByVal bContoLavoroAttivo As Boolean, ByVal dQuaDaEva As Decimal, ByVal strFlevasDaeva As String, ByVal bTuttoIlResiduo As Boolean, ByVal nCodeseOrd As Integer, ByRef dtrSelord As System.Data.DataRow, ByVal bDocumentoU As Boolean, ByVal lRifRigaT As Integer, ByVal bAumentoQta As Boolean) As Boolean
    If dtrMovord!mo_tipork.ToString = "R" And CInt(dtrMovord!mo_commeca) <> 0 Then
      Dim dt As DataTable = CType(oCldBoll, CLHVEBOLL).CPNEMostraOrdFColl(CInt(dtrMovord!mo_commeca), CInt(dtrMovord!mo_subcommeca), strDittaCorrente)
      If dt.Select("mo_flevas = 'C'").Length <> 0 Then
        Dim StrMsg As String = "L'ordine " & dtrMovord!mo_numord.ToString & " riga " & dtrMovord!mo_riga.ToString & " "
        If dt.Select("mo_quaeva <> 0").Length <> 0 Then
          StrMsg += "ha ordini fornitori collegati non ancora evasi totalmente "
        Else
          StrMsg += "ha ordini fornitori collegati non evasi "
        End If
        Dim evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, StrMsg & vbCrLf & "Vuoi importarlo lo stesso?")
        ThrowRemoteEvent(evnt)
        If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then
          Return True
        End If
      End If
    End If

    ScriviRigaOrd = MyBase.ScriviRigaOrd(strTipo, dttCorp, dtrMovord, dtrMovordTc, dSc1, dSc2, dScPag, nMagClav, bContoLavoroAttivo, dQuaDaEva, strFlevasDaeva, bTuttoIlResiduo, nCodeseOrd, dtrSelord, bDocumentoU, lRifRigaT, bAumentoQta)
    '============================ RICCARDO 17 GIUGNO 2019 ===========================
    dttCorp.Rows(dttCorp.Rows.Count -1)!hh_rifmatr = dtrMovord!hh_rifmatr
    '===========================================================================
  End Function
  Public Overrides Function AfterColUpdate_CORPO_ec_codart(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs) As Boolean
    AfterColUpdate_CORPO_ec_codart = MyBase.AfterColUpdate_CORPO_ec_codart(sender, e)
    If Trim(e.ProposedValue.ToString) <> "" Then
      If Trim(dttArti.Rows(0)!ar_ubicaz.ToString) <> "" Then
        e.Row!hh_ubicaz = dttArti.Rows(0)!ar_ubicaz
      End If
    End If
  End Function
  Public Overrides Function OkTestata() As Boolean
    Dim bDDR As Boolean = bDocDaRetail
    bDocDaRetail = True
    OkTestata = MyBase.OkTestata()
    bDocDaRetail = bDDR
  End Function
  'Public Overrides Function ScriviRigaOrd_SalvaRiga(ByVal strTipo As String, ByVal bDocumentoU As Boolean, ByVal dtrMovord As System.Data.DataRow, ByRef dttCorp As System.Data.DataTable, ByRef dtrMovordTc() As System.Data.DataRow, ByRef bAddedTc As Boolean, ByVal bTuttoIlResiduo As Boolean, ByRef dtrSelord As System.Data.DataRow) As Boolean
  '  ScriviRigaOrd_SalvaRiga = MyBase.ScriviRigaOrd_SalvaRiga(strTipo, bDocumentoU, dtrMovord, dttCorp, dtrMovordTc, bAddedTc, bTuttoIlResiduo, dtrSelord)
  '  If ScriviRigaOrd_SalvaRiga Then
  '    With dttEC.Rows(dttEC.Rows.Count - 1)
  '      If !ec_codart <> "" Then
  '        Dim dt As New DataTable
  '        oCldBoll.ValCodiceDb(!ec_codart, strDittaCorrente, "ARTICO", "S", " ", dt)
  '        If Trim(dt.Rows(0)!ar_ubicaz) <> "" Then
  '          dttEC.Rows(dttEC.Rows.Count - 1)!hh_ubicaz = dt.Rows(0)!ar_ubicaz
  '        End If
  '      End If
  '    End With
  '  End If
  'End Function
End Class
