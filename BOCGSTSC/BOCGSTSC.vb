Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO
Public Class FROCGSTSC
  Inherits FRMCGSTSC

  Public Overrides Sub tlbStampa_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
    CType(oCleStsc, CLFCGSTSC).CPNEElaboraFlussi()
    MyBase.tlbStampa_ItemClick(sender, e)
  End Sub
  Public Overrides Sub tlbStampaVideo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
    CType(oCleStsc, CLFCGSTSC).CPNEElaboraFlussi()
    MyBase.tlbStampaVideo_ItemClick(sender, e)
  End Sub

  Public Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    If e.TipoEvento = "" Then
      'If InStr(e.Message.ToLower, "ripristinare") <> 0 Then
      'Dim q As New NTSEventArgs("", e.Message.ToLower & vbCrLf & "Attuale Salvataggio " & dsGsor.Tables("testa").Rows(0)!et_ultagg)

      '  MyBase.GestisciEventiEntity(sender, q)
      'Else

      MyBase.GestisciEventiEntity(sender, e)

      'End If
    Else
      If Mid(e.TipoEvento, 1, 4) = "CPNE" Then
        Select Case e.TipoEvento
          Case "CPNE.PannelloFornitori"
            Dim formDaLan As New FRMHHSTSC
            formDaLan.Init(oMenu, Nothing, DittaCorrente)
            formDaLan.InitEntity(CType(oCleStsc, CLFCGSTSC))
            formDaLan.ShowDialog()
            formDaLan.Dispose()
            formDaLan = Nothing


        End Select
      Else
        MyBase.GestisciEventiEntity(sender, e)
      End If
    End If
  End Sub
End Class