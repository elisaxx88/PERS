Partial Public Class FRMHH0045
  Inherits FRM__CHIL

  <System.Diagnostics.DebuggerNonUserCode()>
  Public Sub New()
    MyBase.New()
  End Sub

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()>
  Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing AndAlso components IsNot Nothing Then
      components.Dispose()
    End If
    MyBase.Dispose(disposing)
  End Sub

  Public WithEvents fmAll As NTSGroupBox
    Public WithEvents lbDataScadenza As NTSLabel
    Public WithEvents lbNrRate As NTSLabel
    Public WithEvents lbImporto As NTSLabel
    Public WithEvents cmdAnnulla As NTSButton
    Public WithEvents edImporto As NTSTextBoxNum
    Public WithEvents edDataInizio As NTSTextBoxData
    Public WithEvents lbDataInizio As NTSLabel
    Public WithEvents cbCadenza As NTSComboBox
    Public WithEvents lbCadenza As NTSLabel
    Public WithEvents edNrRate As NTSTextBoxNum
    Public WithEvents cmdOK As NTSButton
    Public WithEvents edDataScadenza As NTSTextBoxData
  Friend WithEvents NtsGroupBox2 As NTSGroupBox
  Friend WithEvents NtsGroupBox1 As NTSGroupBox
  Friend WithEvents NtsFlowLayoutPanel1 As NTSFlowLayoutPanel
End Class
