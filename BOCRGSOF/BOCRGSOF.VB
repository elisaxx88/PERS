Imports System.Data
Imports NTSInformatica.CLN__STD

Imports System
Imports DevExpress.XtraGrid.Views.Base
Imports NTSInformatica
Imports DevExpress.XtraBars

Public Class FROCRGSOF
  Inherits FRMCRGSOF
  Public _oClfGsof As CLFCRGSOF
  Dim grXOrFo As NTSGrid
  Dim grvXOrFo As NTSGridView
  Dim DchhXOrFo As New BindingSource
  Dim hh_ssriga As NTSGridColumn
  Dim xx_descr1 As NTSGridColumn
  Public Property oClfGsof() As CLFCRGSOF
    Get
      If _oClfGsof Is Nothing Then _oClfGsof = CType(oCleGsof, CLFCRGSOF)
      Return _oClfGsof
    End Get
    Set(ByVal value As CLFCRGSOF)
      _oClfGsof = value
    End Set
  End Property
  Public Overrides Sub InitControls()
    MyBase.InitControls()
    GeneraECollegaOggettiARuntime()
  End Sub
  Private Sub GeneraECollegaOggettiARuntime()
    Try
      grXOrFo = CType(NTSFindControlByName(Me, "grXOrFo"), NTSGrid)
      grvXOrFo = CType(NTSFindControlByName(Me, "grvXOrFo"), NTSGridView)
      AddHandler grXOrFo.GotFocus, AddressOf grxXOrFo_GotFocus
      AddHandler grvXOrFo.GotFocus, AddressOf grxXOrFo_GotFocus
      AddHandler grvXOrFo.NTSFocusedRowChanged, AddressOf grvXOrFo_NTSFocusedRowChanged
      AddHandler grvXOrFo.NTSBeforeRowUpdate, AddressOf grvXOrFo_NTSBeforeRowUpdate
      hh_ssriga = CType(grvXOrFo.Columns("hh_ssriga"), NTSGridColumn)
      xx_descr1 = CType(grvXOrFo.Columns("xx_descr1"), NTSGridColumn)
      xx_descr1.Enabled = False
    Catch ex As Exception
      MsgBox(ex.Message & vbCrLf & ex.StackTrace)
    End Try
  End Sub

  Private Sub grvXOrFo_NTSFocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs)
    Try
      oClfGsof.DrHhmovoffOf = grvXOrFo.NTSGetCurrentDataRow
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
    '-------------------------------------------------
    End Try

  End Sub

  Private Sub grxXOrFo_GotFocus(sender As Object, e As EventArgs)
    Try
      If edEt_codlead.Text = "0" Then
        edEt_codlead.Focus()
      Else
        If IsNothing(grvRighe.NTSGetCurrentDataRow) Then
          grvRighe.Focus()
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub

  Public Overrides Sub GestisciEventiEntity(sender As Object, ByRef e As NTSEventArgs)
    Try
      MyBase.GestisciEventiEntity(sender, e)
      If Mid(e.TipoEvento, 1, 5).ToUpper = "CPNE." Then
        Select Case e.TipoEvento
          Case "CPNE.AggGr"
            DchhXOrFo.DataSource = oClfGsof.DsCPNE.Tables("hhmovoffOf")
            grXOrFo.DataSource = DchhXOrFo
          Case "CPNE.Riepilogo"
            fmDocTop.Text = e.Message
        End Select
      End If
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub
  Public Sub grvXOrFo_NTSBeforeRowUpdate(sender As Object, e As RowAllowEventArgs)
    Try
      If Not oClfGsof.CPNEValidaRiga(grvXOrFo.NTSGetCurrentDataRow) Then
        e.Allow = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub
  Public Overrides Sub grvRighe_NTSBeforeRowUpdate(sender As Object, e As RowAllowEventArgs)
    Try
      If oClfGsof.CPNEValidaRiga(grvXOrFo.NTSGetCurrentDataRow) Then
        MyBase.grvRighe_NTSBeforeRowUpdate(sender, e)
      Else
        e.Allow = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub

  Public Overrides Sub grvRighe_NTSFocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs)
    Try
      MyBase.grvRighe_NTSFocusedRowChanged(sender, e)
      oClfGsof.DrRigaSel = grvRighe.NTSGetCurrentDataRow
      If IsNothing(grvRighe.NTSGetCurrentDataRow) Then
        hh_ssriga.FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("hh_riga = -999")
      Else
        hh_ssriga.FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("hh_riga = " & grvRighe.NTSGetCurrentDataRow!ec_riga.ToString & " or hh_riga = 0")
      End If
      oClfGsof.CPNECalcolaMargini()
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub

  Public Overrides Sub tlbZoom_ItemClick(sender As Object, e As ItemClickEventArgs)
    Try
      If grXOrFo.Focused Or grvXOrFo.Focused Then
        NTSCallStandardZoom()
      Else
        MyBase.tlbZoom_ItemClick(sender, e)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub
  Public Overrides Sub tlbRecordCancella_ItemClick(sender As Object, e As ItemClickEventArgs)
    Try
      If grXOrFo.Focused Or grvXOrFo.Focused Then
        oClfGsof.CPNECancellaRipristinaRiga(True, grvXOrFo.NTSGetCurrentDataRow)
      Else
        MyBase.tlbRecordCancella_ItemClick(sender, e)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub
  Public Overrides Sub tlbRecordRipristina_ItemClick(sender As Object, e As ItemClickEventArgs)
    Try
      If grXOrFo.Focused Or grvXOrFo.Focused Then
        oClfGsof.CPNECancellaRipristinaRiga(False, grvXOrFo.NTSGetCurrentDataRow)
      Else
        MyBase.tlbRecordRipristina_ItemClick(sender, e)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      CLN__STD.GestErr(ex, Me, "")
      '-------------------------------------------------
    End Try
  End Sub
  Public Overrides Sub tlbNuovo_ItemClick(sender As Object, e As ItemClickEventArgs)
    fmDocTop.Text = ""
    MyBase.tlbNuovo_ItemClick(sender, e)
  End Sub
End Class

