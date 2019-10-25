Public Class Frm_Ricerca

    Public Enum EnmTipoDB
        MSSQLServer = 1
        MySql = 2
        Access = 3
    End Enum


    Private cNomeTabella As String = ""
    Private cNomeTabellaDB As String = ""
    Private cSelect As String = ""
    Private aCampi(103, 1) As String
    Private aFormati(100, 2) As Integer
    Private aDescr(10) As String
    Private aWhere(10) As String
    Private aOrder(10) As String
    Private bPrimoPassaggio As Boolean = True
    Private nHeightMeno As Integer = 0
    Private nWidthMeno As Integer = 0
    Private bSalvaDimensioni As Boolean = False
    Private nTop As Integer = 0
    Private nLeft As Integer = 0
    Private nWidth As Integer = 0
    Private nHeight As Integer = 0
    Public aValoriRitorno(200, 1) As String
    Public nNrCampi As Integer = 0
    Public cFiltroWhere As String = ""
    Public nTipoDBGenerale As EnmTipoDB = EnmTipoDB.MSSQLServer
    Public cStringaConnessione As String = ""


    Private Sub Txt_Ricerca_TextChanged(sender As Object, e As EventArgs) Handles Txt_Ricerca.TextChanged
        Dim nColonnaNow As Integer = 0
        Dim aNomiColonne(100) As String
        For nColonnaNow = 0 To DGV.ColumnCount - 1
            aNomiColonne(nColonnaNow) = DGV.Columns(nColonnaNow).HeaderText
        Next
        Call FiltraGriglia(nTipoDBGenerale)
        For nColonnaNow = 0 To DGV.ColumnCount - 1
            DGV.Columns(nColonnaNow).HeaderText = aNomiColonne(nColonnaNow)
        Next
    End Sub

    Private Sub FiltraGriglia(Optional nTipoDB As EnmTipoDB = EnmTipoDB.MSSQLServer)
        Dim oGriglia As New cls_ABCAGriglia
        Dim cQuery As String = ""
        If Txt_Ricerca.Text > " " Then
            cQuery = oGriglia.QueryCreaFiltro(aCampi, Txt_Ricerca.Text)
            If cFiltroWhere > " " Then
                If cQuery > " " Then
                    cQuery = cQuery & " AND "
                End If
                cQuery = cQuery & cFiltroWhere
            End If
            cQuery = "SELECT " + cSelect + " FROM " + cNomeTabellaDB + " WHERE " + cQuery
            If cFiltroWhere > " " Then
                cQuery = cQuery & " AND " & cFiltroWhere
            End If
            If aOrder(Cmb_Filtri.SelectedIndex) > " " Then
                cQuery = cQuery + " AND " + aWhere(Cmb_Filtri.SelectedIndex)
            End If
            If aOrder(Cmb_Filtri.SelectedIndex) > " " Then
                cQuery = cQuery + " ORDER BY " + aOrder(Cmb_Filtri.SelectedIndex)
            End If
        Else
            cQuery = "SELECT " + cSelect + " FROM " + cNomeTabellaDB
            If aOrder(Cmb_Filtri.SelectedIndex) > " " Then
                cQuery = cQuery + " WHERE " + aWhere(Cmb_Filtri.SelectedIndex)
                If cFiltroWhere > " " Then
                    cQuery = cQuery & " AND " & cFiltroWhere
                End If
            Else
                If cFiltroWhere > " " Then
                    cQuery = cQuery & " WHERE " & cFiltroWhere
                End If
            End If
            If aOrder(Cmb_Filtri.SelectedIndex) > " " Then
                cQuery = cQuery + " ORDER BY " + aOrder(Cmb_Filtri.SelectedIndex)
            End If
        End If
        Call oGriglia.FiltraTabella(DGV, cNomeTabella, aCampi, aFormati, cQuery, nTipoDB)
    End Sub

    Private Sub Cmb_Filtri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filtri.SelectedIndexChanged
        If bPrimoPassaggio = True Then
            bPrimoPassaggio = False
        Else
            Dim nColonnaNow As Integer = 0
            Dim aNomiColonne(100) As String
            For nColonnaNow = 0 To DGV.ColumnCount - 1
                aNomiColonne(nColonnaNow) = DGV.Columns(nColonnaNow).HeaderText
            Next
            Call FiltraGriglia(nTipoDBGenerale)
            For nColonnaNow = 0 To DGV.ColumnCount - 1
                DGV.Columns(nColonnaNow).HeaderText = aNomiColonne(nColonnaNow)
            Next
        End If
        Call Txt_Ricerca.Focus()
    End Sub

    Public Function Apri_Ricerca(cTabella As String) As Boolean
        Dim oGriglia As New cls_ABCAGriglia
        Dim nConta As Integer = 0
        Dim bFiltroAttiva As Boolean = False
        Txt_Ricerca.Text = ""
        cNomeTabella = cTabella.ToUpper
        For nConta = 0 To 100
            aValoriRitorno(nConta, 0) = ""
            aValoriRitorno(nConta, 1) = ""
        Next
        '        oGriglia.cFiltroWhereObbligatorio = cFiltroWhere
        Call oGriglia.CollegaTabella(DGV, cNomeTabella, cSelect, aCampi, aFormati, aDescr, aWhere, aOrder, cNomeTabellaDB)
        Me.Text = aCampi(101, 0)
        nTop = Val(aCampi(102, 0))
        nLeft = Val(aCampi(102, 1))
        nHeight = Val(aCampi(103, 0))
        nWidth = Val(aCampi(103, 1))
        '        Call RidimensionaForm()
        Cmb_Filtri.Items.Clear()
        For nConta = 0 To 10
            If aDescr(nConta) > " " Then
                Cmb_Filtri.Items.Add(aDescr(nConta))
                bFiltroAttiva = True
            End If
        Next
        If bFiltroAttiva = True Then
            Cmb_Filtri.SelectedIndex = 0
        End If
        Return True
    End Function

    Private Sub Frm_Ricerca_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Dim oGriglia As New cls_ABCAGriglia
        Call oGriglia.ColonneDimensioneSalva(DGV, cNomeTabella.ToUpper)
        Call oGriglia.FormDimensioneSalva(cNomeTabella.ToUpper, Int(sender.Top).ToString, Int(sender.Left).ToString, Int(sender.Height).ToString, Int(sender.Width).ToString)
    End Sub

    Private Sub Frm_Ricerca_Load(sender As Object, e As EventArgs) Handles Me.Load
        nHeightMeno = 408 - 302
        nWidthMeno = 591 - 558
        Call RidimensionaForm()
        Me.ActiveControl = Txt_Ricerca
    End Sub

    Private Sub Frm_Ricerca_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        nTop = Me.Top
        nLeft = Me.Left
        nWidth = Me.Width
        nHeight = Me.Height
        Call RidimensionaForm()
    End Sub
    Private Sub RidimensionaForm()
        Me.Top = nTop
        Me.Left = nLeft
        Me.Width = nWidth
        Me.Height = nHeight
        If Me.Height - nHeightMeno > 0 Then
            DGV.Height = Me.Height - nHeightMeno
        End If
        If Me.Width - nWidthMeno > 0 Then
            DGV.Width = Me.Width - nWidthMeno
        End If
    End Sub

    Private Sub DGV_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellClick
        Call Txt_Ricerca.Focus()
    End Sub

    
    Private Sub DGV_DoubleClick(sender As Object, e As EventArgs) Handles DGV.DoubleClick
        Dim oGriglia As New cls_ABCAGriglia
        Call oGriglia.ColonneNomeCampi(DGV, aValoriRitorno)
        Call oGriglia.ColonneValoreCampi(DGV, aValoriRitorno, DGV.CurrentCellAddress.Y)
        nNrCampi = DGV.ColumnCount
        Me.Close()
    End Sub

    Private Sub DGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellContentClick

    End Sub

    'Private Sub DGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellContentClick
    ' 'System.Windows.Forms.SendKeys.Send("{TAB}")
    '     Call Txt_Ricerca.Focus()
    ' End Sub

End Class