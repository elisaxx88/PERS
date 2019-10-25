Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports NTSInformatica.CLN__STD
Imports NTSInformatica
Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms
Public Class BNORGSORVBS
  Implements INT__SCRIPT

  Public Function Exec(ByVal strCommand As String, ByRef oApp As Object, ByRef oIn As Object, ByRef oParam As Object) As Object Implements INT__SCRIPT.Exec
    Return Nothing
  End Function
End Class

Public Class BOORGSORVBS
  Implements INT__SCRIPT
  Dim FRXORGSOR As FROORGSOR
  Dim Ocle As CLEORGSOR
  Dim Oclf As CLFORGSOR
  Public bAnnullatoCreaArtVar As Boolean

  Public Function Exec(ByVal strCommand As String, ByRef oApp As Object, ByRef oIn As Object, ByRef oParam As Object) As Object Implements INT__SCRIPT.Exec
    Try
      Select Case strCommand
        Case "InitControls"
          Select Case CType(oIn, Control).Name
            Case "FROORGSOR", "FRMORGSOR"
              FRXORGSOR = CType(oIn, FROORGSOR)
              Oclf = CType(FRXORGSOR.oCleGsor, CLFORGSOR)
              Ocle = FRXORGSOR.oCleGsor
              AddHandler CType(FRXORGSOR.NTSFindControlByName(FRXORGSOR, "tlbCPNEMargine"), NTSBarMenuItem).ItemClick, AddressOf TlbCPNEMargine_ItemClick
              FRXORGSOR.NtsBarManager1.Items("tlbCPNEMargine").ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F2 Or System.Windows.Forms.Keys.Shift)
              AddHandler CType(FRXORGSOR.NTSFindControlByName(FRXORGSOR, "tlbCPNECont"), NTSBarMenuItem).ItemClick, AddressOf TlbCPNECont_ItemClick
              FRXORGSOR.NtsBarManager1.Items("tlbCPNETotFor").ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F4 Or System.Windows.Forms.Keys.Shift)
              AddHandler CType(FRXORGSOR.NTSFindControlByName(FRXORGSOR, "tlbCPNETotFor"), NTSBarMenuItem).ItemClick, AddressOf TlbCPNETotFor_ItemClick
              AddHandler CType(FRXORGSOR.NTSFindControlByName(FRXORGSOR, "TLBCPNERiepilogoDatiTestata"), NTSBarMenuItem).ItemClick, AddressOf TLBCPNERiepilogoDatiTestata_ItemClick
              AddHandler CType(FRXORGSOR.NTSFindControlByName(FRXORGSOR, "TLBCPNEazzerarighe"), NTSBarMenuItem).ItemClick, AddressOf TLBCPNEazzerarighe_ItemClick
              AddHandler CType(FRXORGSOR.NTSFindControlByName(FRXORGSOR, "TLBCPNEriproprighe"), NTSBarMenuItem).ItemClick, AddressOf TLBCPNEriproprighe_ItemClick
              AddHandler CType(FRXORGSOR.NTSFindControlByName(FRXORGSOR, "ckbloccaprzforn"), NTSCheckBox).Click, AddressOf Ckbloccaprzforn_Click
              AddHandler CType(FRXORGSOR.NTSFindControlByName(FRXORGSOR, "TLBCPNEcreatotale"), NTSBarMenuItem).ItemClick, AddressOf TLBCPNEcreatotale_ItemClick
              'AddHandler CType(FRXORGSOR.NTSFindControlByName(FRXORGSOR, "cmdRiepilogoDatiTestata"), NTSButton).Click, AddressOf cmdRiepilogoDatiTestata_Click
              'AddHandler CType(FRXORGSOR.NTSFindControlByName(FRXORGSOR, "cmdazzerarighe"), NTSButton).Click, AddressOf cmdazzerarighe_Click
              'AddHandler CType(FRXORGSOR.NTSFindControlByName(FRXORGSOR, "cmdriproprighe"), NTSButton).Click, AddressOf cmdriproprighe_Click
              'AddHandler CType(FRXORGSOR.NTSFindControlByName(FRXORGSOR, "cmdcreatotale"), NTSButton).Click, AddressOf cmdcreatotale_Click

              FRXORGSOR.NtsBarManager1.Items("tlbCPNEArtVar").ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F5 Or System.Windows.Forms.Keys.Shift)
              AddHandler CType(FRXORGSOR.NTSFindControlByName(FRXORGSOR, "tlbCPNEArtVar"), NTSBarMenuItem).ItemClick, AddressOf TlbCPNEArtVar_ItemClick



              FRXORGSOR.Width = FRXORGSOR.MinimumSize.Width + 200
              FRXORGSOR.MinimumSize = FRXORGSOR.Size
              'FRXORGSOR.pnTestataClforn.Width = FRXORGSOR.pnTestataClforn.Width + 200
              'FRXORGSOR.pnTestataClforn.Width = FRXORGSOR.pnTestataClforn.Width + 200
              FRXORGSOR = CType(oIn, FROORGSOR)
          End Select
      End Select

      Return Nothing
    Catch ex As Exception
      MsgBox(ex.Message)
      Return Nothing
    End Try
  End Function
  Private Sub TLBCPNERiepilogoDatiTestata_ItemClick(sender As System.Object, e As System.EventArgs)
    Try
      'If Oclf.dttET.Rows(0)!et_tipork = "R" Then
      FRXORGSOR.ValidaLastControl()
      Oclf.CPNETotaliPerFornitore()
      Oclf.CPNERedditivita()
      'Oclf.CPNECaricaScadPag()

      Dim formDaLan As New FRMDatiAgg
      formDaLan.Init(FRXORGSOR.oMenu, Nothing, FRXORGSOR.DittaCorrente)
      formDaLan.InitEntity(Ocle)
      formDaLan.ShowDialog()
      formDaLan.Dispose()
      formDaLan = Nothing
      'End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", FRXORGSOR.oApp.InfoError, FRXORGSOR.oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Private Sub TLBCPNEazzerarighe_ItemClick(sender As System.Object, e As System.EventArgs)
    Try
      If FRXORGSOR.oApp.MsgBoxInfoYesNo_DefNo("Azzerare i prezzi di tutte le righe?") = vbYes Then
        Oclf.CPNEAzzeraRighe()
      End If

      'End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", FRXORGSOR.oApp.InfoError, FRXORGSOR.oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Private Sub TLBCPNEriproprighe_ItemClick(sender As System.Object, e As System.EventArgs)
    Try
      If FRXORGSOR.oApp.MsgBoxInfoYesNo_DefNo("Riproporzionare i prezzi di tutte le righe?") = vbYes Then
        Dim sQtaTmp As String = "9999"
        While CDbl(sQtaTmp) = 9999
          sQtaTmp = InputBox("Inserisci l'importo totale", "Mettere 0 per uscire", )
          If InStr(sQtaTmp, ",") > 0 Then
            If Not IsNumeric(Mid(sQtaTmp, 1, InStr(sQtaTmp, ",") - 1)) Then
              sQtaTmp = "9999"
            Else
              If Not IsNumeric(Mid(sQtaTmp, InStr(sQtaTmp, ",") + 1)) Then sQtaTmp = "9999"
            End If
          Else
            If Not IsNumeric(sQtaTmp) Then sQtaTmp = "9999"

          End If

        End While
        If sQtaTmp <> "0" Then
          Oclf.CPNERiproporzionaPrezzoRighe(CDec(sQtaTmp))
        End If

      End If


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", FRXORGSOR.oApp.InfoError, FRXORGSOR.oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Private Sub TLBCPNEcreatotale_ItemClick(sender As System.Object, e As System.EventArgs)
    Try
      If FRXORGSOR.oApp.MsgBoxInfoYesNo_DefNo("Si vuole creare una riga di totale?") = vbYes Then
        Oclf.CPNECreaRigaTotale()
      End If

      'End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", FRXORGSOR.oApp.InfoError, FRXORGSOR.oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Private Sub TlbCPNETotFor_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
    Oclf.CPNETotaliPerFornitore()
    Try
      Dim formDaLan As New FRMTotFor
      formDaLan.Init(FRXORGSOR.oMenu, Nothing, FRXORGSOR.DittaCorrente)
      formDaLan.InitEntity(Ocle)
      formDaLan.ShowDialog()
      formDaLan.Dispose()
      formDaLan = Nothing
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", FRXORGSOR.oApp.InfoError, FRXORGSOR.oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Private Sub TlbCPNEArtVar_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
    Try

      Exit Sub

      bAnnullatoCreaArtVar = False
      Dim formDaLan As New FRMArtVar
      formDaLan.Init(FRXORGSOR.oMenu, Nothing, FRXORGSOR.DittaCorrente)
      formDaLan.InitEntity(Ocle)
      formDaLan.strCodiceBase = ""
      If Not IsNothing(FRXORGSOR.grvRighe.NTSGetCurrentDataRow) Then
        formDaLan.strCodiceBase = FRXORGSOR.grvRighe.NTSGetCurrentDataRow!ec_codart.ToString
      End If
      formDaLan.ShowDialog()
      bAnnullatoCreaArtVar = formDaLan.bAnnullatoCreaArtVar
      formDaLan.Dispose()
      formDaLan = Nothing

      If bAnnullatoCreaArtVar Then Exit Sub


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", FRXORGSOR.oApp.InfoError, FRXORGSOR.oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Private Sub TlbCPNEMargine_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
    Oclf.CPNERedditivita()
    Try
      Dim formDaLan As New FRMRedd
      formDaLan.Init(FRXORGSOR.oMenu, Nothing, FRXORGSOR.DittaCorrente)
      formDaLan.InitEntity(Ocle)
      formDaLan.ShowDialog()
      formDaLan.Dispose()
      formDaLan = Nothing
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", FRXORGSOR.oApp.InfoError, FRXORGSOR.oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Private Sub TlbCPNECont_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
    If Oclf.CPNEElabCont(FRXORGSOR.grvRighe.NTSGetCurrentDataRow) Then
      Try
        Dim formDaLan As New FRMImpRs
        formDaLan.Init(FRXORGSOR.oMenu, Nothing, FRXORGSOR.DittaCorrente)
        formDaLan.InitEntity(Ocle)
        formDaLan.ShowDialog()
        formDaLan.Dispose()
        formDaLan = Nothing
      Catch ex As Exception
        '-------------------------------------------------
        Dim strErr As String = CLN__STD.GestError(ex, Me, "", FRXORGSOR.oApp.InfoError, FRXORGSOR.oApp.ErrorLogFile, True)
        '-------------------------------------------------	
      End Try
    End If
  End Sub
  Private Sub Ckbloccaprzforn_Click(sender As System.Object, e As System.EventArgs)
    Try
      If CType(FRXORGSOR.NTSFindControlByName(FRXORGSOR, "ckbloccaprzforn"), NTSCheckBox).Checked Then

        Oclf.bBloccaPrezzoFornitore = False
      Else
        Oclf.bBloccaPrezzoFornitore = True
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", FRXORGSOR.oApp.InfoError, FRXORGSOR.oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


End Class
