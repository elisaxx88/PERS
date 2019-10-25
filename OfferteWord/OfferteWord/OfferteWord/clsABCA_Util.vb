Imports System.IO
Imports System.Xml
Imports System.Net
Imports System.Data
Imports VB = Microsoft.VisualBasic
Imports System.Drawing.Printing
Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class ABCA_Util
    Public Enum enSalvaCarica
        Salva = 1
        Carica = 2
    End Enum
    Public Enum enFileDir
        File = 1
        Directory = 2
    End Enum

    Private Declare Auto Function GetPrivateProfileString Lib "kernel32.dll" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Private Declare Auto Function WritePrivateProfileString Lib "kernel32.dll" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer

    'Public Shared Function ResizeImage(ByVal oImage As Image, ByVal nHeight As Integer, ByVal nWidth As Integer,
    '                                    Optional ByVal preserveAspectRatio As Boolean = True) As Image

    '    ' Visual Basic
    '    Dim newWidth As Integer
    '    Dim newHeight As Integer
    '    If preserveAspectRatio Then
    '        Dim originalWidth As Integer = oImage.Width
    '        Dim originalHeight As Integer = oImage.Height
    '        Dim percentWidth As Single = CSng(nWidth) / CSng(originalWidth)
    '        Dim percentHeight As Single = CSng(nHeight) / CSng(originalHeight)
    '        Dim percent As Single = If(percentHeight < percentWidth, percentHeight, percentWidth)
    '        newWidth = CInt(originalWidth * percent)
    '        newHeight = CInt(originalHeight * percent)
    '    Else
    '        newWidth = nWidth
    '        newHeight = nHeight
    '    End If
    '    ' Visual Basic
    '    Dim newImage As Image = New Bitmap(newWidth, newHeight)

    '    ' Visual Basic
    '    Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
    '        graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic
    '        graphicsHandle.DrawImage(oImage, 0, 0, newWidth, newHeight)
    '    End Using
    '    Return newImage

    'End Function
    Public Shared Function DataInStringa(strSeparatore As String, bAnnoIn2Cifre As Boolean, Optional dDataDaConvertire As Date = Nothing) As String
        Try
            If Year(dDataDaConvertire) < 1900 = True Then
                dDataDaConvertire = DateTime.Now
            End If
            Dim strAppoggio As String
            strAppoggio = Strings.Right("00" & dDataDaConvertire.Day, 2) & strSeparatore
            strAppoggio &= Strings.Right("00" & dDataDaConvertire.Month, 2) & strSeparatore
            If bAnnoIn2Cifre = True Then
                strAppoggio &= Strings.Right("00" & dDataDaConvertire.Year, 2)
            Else
                strAppoggio &= Strings.Right("00" & dDataDaConvertire.Year, 4)
            End If
            Return strAppoggio
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Shared Function OraInStringa(strSeparatore As String, Optional dDataDaConvertire As Date = Nothing) As String
        Try
            If Year(dDataDaConvertire) < 1900 Then
                dDataDaConvertire = DateTime.Now
            End If
            Dim strAppoggio As String
            strAppoggio = Strings.Right("00" & dDataDaConvertire.Hour, 2) & strSeparatore
            strAppoggio &= Strings.Right("00" & dDataDaConvertire.Minute, 2) & strSeparatore
            strAppoggio &= Strings.Right("00" & dDataDaConvertire.Second, 2)
            Return strAppoggio
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Shared Function LeggiFileXML(ByRef oDS As DataSet, Optional cFileXML As String = "") As Boolean
        If cFileXML <= " " Then
            cFileXML = ABCA_Util.AppPath() & "\Settings.xml"
        End If
        Try
            oDS.Tables.Clear()
            ' Create new FileStream to read schema with.
            Dim fsReadXml As New System.IO.FileStream(cFileXML, System.IO.FileMode.Open)

            ' Create an XmlTextReader to read the file.
            Dim xmlReader As New System.Xml.XmlTextReader(fsReadXml)

            ' Read the XML document into the DataSet.
            oDS.ReadXml(xmlReader, XmlReadMode.ReadSchema)

            ' Close the XmlTextReader
            xmlReader.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function EsisteFileDir(cCosaCercare As String, nTipo As enFileDir, Optional bCreaDirectory As Boolean = False) As Boolean
        Try
            If nTipo = enFileDir.File Then
                Return File.Exists(cCosaCercare)
            Else
                If Directory.Exists(cCosaCercare) = True Then
                    Return True
                Else
                    If bCreaDirectory = True Then
                        Call Directory.CreateDirectory(cCosaCercare)
                        Return Directory.Exists(cCosaCercare)
                    Else
                        Return False
                    End If
                End If
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function DammiIDUnivoco() As String
        Dim cAppoggio As String = ""
        cAppoggio = TrasformaInLettera(VB.DateAndTime.Year(Now) - 2000)
        cAppoggio = cAppoggio & TrasformaInLettera(VB.DateAndTime.Month(Now))
        cAppoggio = cAppoggio & TrasformaInLettera(VB.DateAndTime.Day(Now))
        cAppoggio = cAppoggio & TrasformaInLettera(VB.DateAndTime.Hour(Now))
        cAppoggio = cAppoggio & TrasformaInLettera(VB.DateAndTime.Minute(Now))
        cAppoggio = cAppoggio & TrasformaInLettera(VB.DateAndTime.Second(Now))
        cAppoggio = cAppoggio & TrasformaInLettera(GetRandom)
        cAppoggio = cAppoggio & TrasformaInLettera(GetRandom)
        Return cAppoggio
    End Function
    Public Shared Function PulisciPerNomeFile(ByVal cStringa As String) As String
        cStringa = VB.Replace(cStringa, "*", "")

        cStringa = VB.Replace(cStringa, "*", "")

        cStringa = VB.Replace(cStringa, "\", "")

        cStringa = VB.Replace(cStringa, ":", "")

        cStringa = VB.Replace(cStringa, VB.Chr(34), "")

        cStringa = VB.Replace(cStringa, "<", "")

        cStringa = VB.Replace(cStringa, ">", "")

        cStringa = VB.Replace(cStringa, "?", "")

        cStringa = VB.Replace(cStringa, "/", "")

        Return cStringa

    End Function

    Public Shared Function SQL_Stringa(ByVal cStringa As String, Optional nLunghezza As Integer = -1, Optional bTogliSpazi As Boolean = True) As String
        If nLunghezza <> -1 Then
            cStringa = VB.Left(cStringa, nLunghezza)
        End If
        Return "'" & PulisciStringa(cStringa, bTogliSpazi) & "'"
    End Function

    Public Shared Function SQL_Num(ByVal nNumero As Double) As String
        Return VB.Replace(VB.Replace("" & nNumero, ".", ""), ",", ".")
    End Function

    Public Shared Function UtenteSistema() As String
        Return Environment.UserName
    End Function

    Public Shared Function TrasformaInLettera(ByVal nAsc As Integer) As String
        Select Case nAsc
            Case 26
                Return "1"
            Case 27
                Return "2"
            Case 28
                Return "3"
            Case 29
                Return "4"
            Case 56
                Return "5"
            Case 57
                Return "6"
            Case 58
                Return "7"
            Case 59
                Return "8"
            Case 0 To 25
                Return VB.Chr(nAsc + 65)
            Case 30 To 55
                Return VB.Chr(nAsc + 67)
            Case Else
                Return ""
        End Select
    End Function

    Public Shared Function GetRandom(Optional Min As Integer = 0, Optional Max As Integer = 59) As Integer
        Static Generator As System.Random = New System.Random()
        Return Generator.Next(Min, Max)
    End Function
    Public Shared Function FileAperto(ByVal nomefile As String) As Boolean
        If System.IO.File.Exists(nomefile) Then
            Try
                Dim ObjFs As New System.IO.FileStream(nomefile, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.None)
                ObjFs.Close()
                Return False              ' Il file esiste e non è aperto da altre applicazioni
            Catch ObjEx As Exception
                Return True            ' Il file esiste ma è in uso da almeno un'altra applicazione (il file è in LOCK)
            End Try
        Else
            Return False                 ' Il file non esiste
        End If
    End Function

    Public Shared Function CopiaFile(cDirectorySource As String, cFileSource As String, cDirectoryDestination As String, cFileDestination As String, Optional bSovrascrivi As Boolean = False) As Boolean
        Dim cAppoggio As String = cDirectorySource

        If Microsoft.VisualBasic.Right(cAppoggio, 1) <> "\" Then
            cAppoggio = cAppoggio & "\"
        End If
        cAppoggio = cAppoggio & cFileSource
        If FileAperto(cAppoggio) = True Then
            MsgBox("Il file " & cAppoggio & " risulta essere aperto. Procedere con la chiusura e quindi rieseguire la procedura!")
            Return False
        End If
        cAppoggio = cDirectoryDestination
        If Microsoft.VisualBasic.Right(cAppoggio, 1) <> "\" Then
            cAppoggio = cAppoggio & "\"
        End If
        cAppoggio = cAppoggio & cFileDestination
        If FileAperto(cAppoggio) = True Then
            MsgBox("Il file " & cAppoggio & " risulta essere aperto. Procedere con la chiusura e quindi rieseguire la procedura!")
            Return False
        End If
        If File.Exists(cAppoggio) = True And bSovrascrivi = False Then
            If MsgBox("Il file " & cFileDestination & " esiste già. Vuoi sovrascrivere il file?", MsgBoxStyle.YesNo, "Attenzione!!!") = MsgBoxResult.No Then
                MsgBox("Il file " & cAppoggio & " esiste già. Procedere con la cancellazione e quindi rieseguire la procedura!")
                Return False
            End If
        End If
        File.Copy(Path.Combine(cDirectorySource, cFileSource), Path.Combine(cDirectoryDestination, cFileDestination), True)
        Return True
    End Function
    Public Shared Function LeggiFileIni(ByVal cNomeFileINI As String, ByVal cSezione As String, ByVal cChiave As String, Optional ByVal cValoreDefault As String = "", _
                             Optional ByVal bSegnalaErrore As Boolean = False) As String

        Dim cValoreRitorno As String = VB.Strings.StrDup(255, " ")
        Dim nLunghezzaRisultato As Integer
        Dim cTestoErrore As String

        nLunghezzaRisultato = GetPrivateProfileString(cSezione, cChiave, cValoreDefault, cValoreRitorno, cValoreRitorno.Length, cNomeFileINI)

        If nLunghezzaRisultato = 0 AndAlso bSegnalaErrore Then
            If Not (System.IO.File.Exists(cNomeFileINI)) Then
                cTestoErrore = "Impossibile aprire il file INI" & cNomeFileINI
            Else
                cTestoErrore = "La sezione o la chiave sono errate oppure l’accesso al file non è consentito"
            End If
            Throw New Exception(cTestoErrore)
        End If

        Return cValoreRitorno.Substring(0, nLunghezzaRisultato)

    End Function

    Public Shared Function ScriviFileIni(ByVal cNomeFile As String, ByVal cSezione As String, ByVal cChiave As String, ByVal cValore As String, _
                                  Optional ByVal bSegnalaErrore As Boolean = False) As Boolean

        Dim nLunghezzaRisultato As Integer
        Dim cTestoErrore As String

        nLunghezzaRisultato = WritePrivateProfileString(cSezione, cChiave, cValore, cNomeFile)

        If nLunghezzaRisultato = 0 And bSegnalaErrore Then
            If Not (System.IO.File.Exists(cNomeFile)) Then
                cTestoErrore = "Impossibile aprire il file INI" & cNomeFile
            Else
                cTestoErrore = "Accesso al file non consentito"
            End If
            Throw New Exception(cTestoErrore)
        End If
        If nLunghezzaRisultato = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    Public Shared Function DammiFileTemporaneo(Optional ByVal cDirectoryTemp As String = "", Optional ByVal bDirTemporanea As Boolean = True, _
                                             Optional ByVal cEstensione As String = ".tmp") As String
        Dim dDataOra As Date
        Dim nRandom As New Random
        Dim cFileTemporaneo As String
        dDataOra = Date.Now

        cFileTemporaneo = ""
        If cDirectoryTemp > "" Then
            cFileTemporaneo = cDirectoryTemp
        ElseIf bDirTemporanea = True Then
            cFileTemporaneo = Path.GetTempPath()
        End If
        If cFileTemporaneo > " " And VB.Right(cFileTemporaneo, 1) <> "\" Then
            cFileTemporaneo = cFileTemporaneo & "\"
        End If
        cFileTemporaneo += "" & dDataOra.Year & _
                    VB.Right("00" & dDataOra.Month, 2) & _
                    VB.Right("00" & dDataOra.Day, 2) & "-" & _
                    VB.Right("00" & dDataOra.Hour, 2) & _
                    VB.Right("00" & dDataOra.Minute, 2) & _
                    VB.Right("0000" & dDataOra.Millisecond, 4) & "-" & _
                    VB.Right("0000" & nRandom.Next(1, 10000), 5)
        cFileTemporaneo += cEstensione
        Return cFileTemporaneo
    End Function

  Public Shared Function AppPath(Optional bMettoLaBarra As Boolean = False) As String
    Dim cDirectory As String = ""
    '        Return System.Windows.Forms.Application.StartupPath
    cDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
    cDirectory = Right(cDirectory, cDirectory.Length - 6)
    If Mid(cDirectory, 2, 2) <> ":\" Then
      cDirectory = "\\" & cDirectory
    End If
    If bMettoLaBarra = True Then
      cDirectory &= "\"
    End If
    Return cDirectory
  End Function

  Public Shared Function XmlConfigLeggi(cChiave As String, nElementiLista As Integer, aLista(,) As String, Optional cFileconfig As String = "") As Boolean
        Try
            If cFileconfig = "" Then
        cFileconfig = AppPath() & "\ConfigWORD.xml"
      End If
            Dim oXmlFile As XmlReader
            oXmlFile = XmlReader.Create(cFileconfig, New XmlReaderSettings())
            Dim oDataSet As New DataSet
            Dim oDataView As DataView
            oDataSet.ReadXml(oXmlFile)

            oDataView = New DataView(oDataSet.Tables(0))
            oDataView.Sort = "Sezione_Nome"
            Dim index As Integer = oDataView.Find(cChiave)

            If index = -1 Then
                ' non trovo nulla e quindi restituisco false
                Return False
            Else
                Dim nconta As Integer = 0
                For nconta = 0 To nElementiLista - 1
                    If oDataView.ToTable().Columns.Contains(aLista(nconta, 0)) = True Then
                        aLista(nconta, 1) = oDataView(index)(aLista(nconta, 0)).ToString()
                    Else
                        aLista(nconta, 1) = ""
                    End If
                Next
                oDataSet.Dispose()
                oXmlFile.Close()
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function XmlConfigScrivi(cChiave As String, nElementiLista As Integer, aLista(,) As String, Optional cFileconfig As String = "") As Boolean
        Try
            If cFileconfig = "" Then
        cFileconfig = AppPath() & "\ConfigWORD.xml"
      End If
            Dim oXmlFile As XmlReader
            oXmlFile = XmlReader.Create(cFileconfig, New XmlReaderSettings())
            Dim oDataSet As New DataSet
            Dim oDataView As DataView
            oDataSet.ReadXml(oXmlFile)

            oDataView = New DataView(oDataSet.Tables(0))
            oDataView.AllowNew = True
            oDataView.Sort = "Sezione_Nome"
            Dim index As Integer = oDataView.Find(cChiave)

            If index = -1 Then
                ' non trovo nulla e quindi restituisco false
                Return False
            Else
                Dim nconta As Integer = 0
                For nconta = 0 To nElementiLista - 1
                    oDataView(index)(aLista(nconta, 0)) = aLista(nconta, 1)
                Next
                oXmlFile.Close()
                oDataSet.WriteXml(cFileconfig)
                oDataSet.Dispose()
                Return True
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function xmlSalvaElemento(cSezione As String, cNodo As String, aLista(,) As String, nAttributi As Integer, Optional cFileXml As String = "") As Boolean
        Try
            Dim nConta As Integer = 0
            Dim oXmlDoc As New XmlDocument()
            cNodo = cNodo.ToUpper
            cSezione = cSezione.ToUpper
            For nConta = 0 To nAttributi - 1
                aLista(nConta, 0) = aLista(nConta, 0).ToUpper
            Next
            ' se il file xml non viene passato, uso quello relativo allo standard
            If cFileXml = "" Then
        cFileXml = ABCA_Util.AppPath() & "\ConfigWORD.xml"
      End If
            ' se il file xml non esiste lo creo
            If File.Exists(cFileXml) = False Then
                ' qui creo il file
                Dim oNuovoXml As System.Xml.XmlTextWriter = New System.Xml.XmlTextWriter(cFileXml, System.Text.Encoding.UTF8)
                With oNuovoXml
                    .WriteStartDocument(True)
                    .WriteStartElement("XmlChiavi")
                    .Indentation = 5
                    .IndentChar = VB.Chr(32)
                    .Formatting = System.Xml.Formatting.Indented
                    .WriteEndElement()
                    .WriteEndDocument()
                End With
                oNuovoXml.Flush()
                oNuovoXml.Close()
            End If
            If cSezione > " " Then
                ' CARICO IL FILE
                oXmlDoc.Load(cFileXml)
                Dim oLista As XmlNode = oXmlDoc.SelectSingleNode("//" & cSezione & "/" & cNodo)
                If oLista Is Nothing Then
                    ' NON ESISTE SEZIONE E NODO E QUINDI LI CREO
                    Dim oNodo As XmlNode = oXmlDoc.CreateNode(XmlNodeType.Element, cSezione, Nothing)
                    oXmlDoc.DocumentElement.AppendChild(oNodo)
                    Dim oNodo2 As XmlNode = oXmlDoc.CreateNode(XmlNodeType.Element, cNodo, Nothing)
                    Call oNodo.AppendChild(oNodo2)
                    Dim oAttributo As XmlAttribute = oXmlDoc.CreateAttribute("INIZIALIZZA")
                    ' scorro tutti i nodi (dovrebbe essere uno soltanto)
                    '        For Each oListaNodi As XmlNode In oListaValori
                    ' ciclo tutti i valori passati e, se non esistono li inserisco, altrimenti li aggiorna
                    For nConta = 0 To nAttributi - 1
                        oAttributo = oXmlDoc.CreateAttribute(aLista(nConta, 0))
                        oAttributo.Value = aLista(nConta, 1)
                        oNodo2.Attributes.Append(oAttributo)
                    Next
                Else
                    Dim oAttributo As XmlAttribute = oXmlDoc.CreateAttribute("INIZIALIZZA")
                    ' scorro tutti i nodi (dovrebbe essere uno soltanto)
                    '        For Each oListaNodi As XmlNode In oListaValori
                    ' ciclo tutti i valori passati e, se non esistono li inserisco, altrimenti li aggiorna
                    For nConta = 0 To nAttributi - 1
                        oAttributo = oXmlDoc.CreateAttribute(aLista(nConta, 0))
                        oAttributo.Value = aLista(nConta, 1)
                        oLista.Attributes.Append(oAttributo)
                    Next
                End If
                oXmlDoc.Save(cFileXml)
                oXmlDoc = Nothing
            Else
                ' il file esiste
                oXmlDoc.Load(cFileXml)
                Dim oListaValori As XmlNodeList = oXmlDoc.GetElementsByTagName(cNodo)
                If oListaValori.Count = 0 Then
                    ' il nodo cercato non esiste
                    Dim oNodo As XmlNode = oXmlDoc.CreateNode(XmlNodeType.Element, cNodo, Nothing)
                    oXmlDoc.DocumentElement.AppendChild(oNodo)
                    oXmlDoc.Save(cFileXml)
                    oListaValori = oXmlDoc.GetElementsByTagName(cNodo)
                End If

                'Il nodo esiste e quindi procedo con aggiornamento
                Dim oAttributo As XmlAttribute = oXmlDoc.CreateAttribute("INIZIALIZZA")
                ' scorro tutti i nodi (dovrebbe essere uno soltanto)
                For Each oListaNodi As XmlNode In oListaValori
                    ' ciclo tutti i valori passati e, se non esistono li inserisco, altrimenti li aggiorna
                    For nConta = 0 To nAttributi - 1
                        oAttributo = oXmlDoc.CreateAttribute(aLista(nConta, 0))
                        oAttributo.Value = aLista(nConta, 1)
                        oListaNodi.Attributes.Append(oAttributo)
                    Next
                Next
                oXmlDoc.Save(cFileXml)
                oXmlDoc = Nothing
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function xmlLeggiElemento(cSezione As String, cNodo As String, ByRef aLista(,) As String, ByRef nAttributi As Integer, Optional cFileXml As String = "") As Boolean
        Try
            Dim oXmlDoc As New XmlDocument()
            Dim nConta As Integer = 0
            ' se il file xml non viene passato, uso quello relativo allo standard
            If cFileXml = "" Then
        cFileXml = ABCA_Util.AppPath() & "\ConfigWORD.xml"
      End If
            cNodo = cNodo.ToUpper
            cSezione = cSezione.ToUpper
            '            For nConta = 0 To 

            'Next
            '            aLista(0, 0) = ""
            '            aLista(0, 1) = ""
            If File.Exists(cFileXml) Then
                oXmlDoc.Load(cFileXml)
                If cSezione > " " Then
                    Dim oLista As XmlNode = oXmlDoc.SelectSingleNode("//" & cSezione & "/" & cNodo)
                    For nConta = 0 To oLista.Attributes.Count - 1
                        aLista(nConta, 0) = oLista.Attributes.Item(nConta).Name
                        aLista(nConta, 1) = oLista.Attributes.Item(nConta).Value
                    Next
                    nAttributi = oLista.Attributes.Count
                Else
                    Dim oLista As XmlNodeList = oXmlDoc.GetElementsByTagName(cNodo)
                    For Each oNodo As XmlNode In oLista
                        For nConta = 0 To oNodo.Attributes.Count - 1
                            aLista(nConta, 0) = oNodo.Attributes.Item(nConta).Name
                            aLista(nConta, 1) = oNodo.Attributes.Item(nConta).Value
                        Next
                        nAttributi = oNodo.Attributes.Count
                    Next
                End If
            End If
            oXmlDoc = Nothing
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Shared Function ValoreDaLista(cNome As String, aLista(,) As String, nElementiLista As Integer, Optional cDefault As String = "%%NONINSERITODEFAULT%%") As String

        Dim nConta As Integer = 0
        Dim cRisposta As String = "%%NONTROVATO%%"
        Try
            For nConta = 0 To nElementiLista - 1
                If aLista(nConta, 0).ToUpper = cNome.ToUpper Then
                    cRisposta = aLista(nConta, 1)
                    Exit For
                End If
            Next
            If cDefault <> "%%NONINSERITODEFAULT%%" And cRisposta = "%%NONTROVATO%%" Then
                cRisposta = cDefault
            End If
            Return cRisposta
        Catch
            Return "%%ERRORE%%"
        End Try
    End Function

    Public Shared Function PulisciStringa(cOrigine As String, Optional bTogliSpazi As Boolean = True) As String
        If bTogliSpazi = True Then
            Return VB.Trim(VB.Replace(cOrigine, "'", "`"))
        Else
            Return VB.Replace(cOrigine, "'", "`")
        End If
    End Function

    Public Shared Function DataPerSQL(Optional cData As String = "", Optional bDataOra As Boolean = False, Optional bPerMySQL As Boolean = False) As String
        Dim cAppoggio As String = ""
        If cData = "" Then
            cData = "" & Now
        End If
        cData = VB.Trim(cData)
        Try
            If bPerMySQL = False Then
                cAppoggio = "{ts '"
                cAppoggio = cAppoggio & VB.Trim(VB.Str(VB.Year(CDate(cData)))) & "-"
                cAppoggio = cAppoggio & VB.Right("00" & VB.Trim(VB.Str(VB.Month(CDate(cData)))), 2) & "-"
                cAppoggio = cAppoggio & VB.Right("00" & VB.Trim(VB.Str(VB.Day(CDate(cData)))), 2) & " "
                If bDataOra = False Then
                    cAppoggio = cAppoggio & "00:00:00'}"
                Else
                    cAppoggio = cAppoggio & VB.Right("00" & VB.Trim(VB.Str(VB.Hour(CDate(cData)))), 2) & ":"
                    cAppoggio = cAppoggio & VB.Right("00" & VB.Trim(VB.Str(VB.Minute(CDate(cData)))), 2) & ":"
                    cAppoggio = cAppoggio & VB.Right("00" & VB.Trim(VB.Str(VB.Second(CDate(cData)))), 2) & "'}"
                End If
            Else
                cAppoggio = "'"
                cAppoggio = cAppoggio & VB.Trim(VB.Str(VB.Year(CDate(cData)))) & "-"
                cAppoggio = cAppoggio & VB.Right("00" & VB.Trim(VB.Str(VB.Month(CDate(cData)))), 2) & "-"
                cAppoggio = cAppoggio & VB.Right("00" & VB.Trim(VB.Str(VB.Day(CDate(cData)))), 2) & ""
                If bDataOra = False Then
                    cAppoggio = cAppoggio & "'"
                Else
                    cAppoggio = cAppoggio & " "
                    cAppoggio = cAppoggio & VB.Right("00" & VB.Trim(VB.Str(VB.Hour(CDate(cData)))), 2) & ":"
                    cAppoggio = cAppoggio & VB.Right("00" & VB.Trim(VB.Str(VB.Minute(CDate(cData)))), 2) & ":"
                    cAppoggio = cAppoggio & VB.Right("00" & VB.Trim(VB.Str(VB.Second(CDate(cData)))), 2) & "'"
                End If
            End If
            Return cAppoggio
            Exit Function
        Catch ex As Exception
            Return "NULL"
        End Try

    End Function

    Public Shared Function ScansionaFilesInDirectory(ByVal cDirectory As String, ByVal bSottoDirectory As Boolean, ByRef aFiles() As String, ByRef nNrFiles As Integer, ByVal cEstensione As String) As Boolean
        ' Processa la lista dei files trovati nella directory passata
        Try
            '         nNrFiles = 0
            Dim aFilePresenti As String() = Directory.GetFiles(cDirectory)
            For Each cFileCorrente As String In aFilePresenti
                '              MsgBox(UCase(System.IO.Path.GetExtension(cFileCorrente)))
                If cEstensione = "*" Or "." & UCase(cEstensione) = UCase(System.IO.Path.GetExtension(cFileCorrente)) Then
                    nNrFiles += 1
                    aFiles(nNrFiles) = cFileCorrente
                End If
            Next

            If bSottoDirectory = True Then
                ' Processa tutte le directory trovate nella directory passata alla funzione
                Dim aSottoDirectory As String() = Directory.GetDirectories(cDirectory)
                For Each cSottoDirectory As String In aSottoDirectory
                    ScansionaFilesInDirectory(cSottoDirectory, bSottoDirectory, aFiles, nNrFiles, cEstensione)
                Next
            End If
            Return True
            Exit Function
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function Base64EncodeFileToString(cFile As String) As String

        Try
            Dim aByte As Byte()
            Dim cStreamFile As New IO.FileStream(cFile, IO.FileMode.Open)
            ReDim aByte(CInt(cStreamFile.Length))
            cStreamFile.Read(aByte, 0, CInt(cStreamFile.Length))
            cStreamFile.Close()
            Return System.Convert.ToBase64String(aByte)
        Catch ex As Exception
            Return ""
        End Try

    End Function
    Public Shared Function Base64EncodeStringa(cTesto As String) As String

        Try
            Dim aByteTesto() As Byte = System.Text.Encoding.ASCII.GetBytes(cTesto)
            Return System.Convert.ToBase64String(aByteTesto)
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Public Shared Function Base64DecodeStringa(cTesto As String) As String

        Try
            Dim aByteTesto As Byte() = System.Convert.FromBase64String(cTesto)
            Return System.Text.Encoding.ASCII.GetString(aByteTesto)
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Public Shared Function TrovaIPComputerRemoto(cNomeComputerRemoto As String) As String

        Dim aRemoteIP As IPAddress()
        Dim cIPRemoto As String = ""

        aRemoteIP = Dns.GetHostAddresses(cNomeComputerRemoto)

        cIPRemoto = aRemoteIP(0).ToString

        '        Dim nContaIP As Integer = 0
        '        For nContaIP = 0 To aRemoteIP.Length - 1
        ' If MsgBox("IP = " + aRemoteIP(nContaIP).ToString, MsgBoxStyle.YesNo, "IP del PC " & cNomeComputerRemoto) = MsgBoxResult.Yes Then
        ' cIPRemoto = aRemoteIP(nContaIP).ToString
        ' Exit For
        ' End If
        ' Next
        Return cIPRemoto

    End Function
    Public Shared Sub FormDimensioneSalva(cNomeForm As String, oForm As System.Windows.Forms.Form)
        '**********************************************************************************
        ' Salvo la dimensione della form 
        '**********************************************************************************
        Try
            Dim cDimensioni As String = ""
            cDimensioni = "" & oForm.Top
            cDimensioni &= "|"
            cDimensioni &= oForm.Left
            cDimensioni &= "|"
            cDimensioni &= oForm.Height
            cDimensioni &= "|"
            cDimensioni &= oForm.Width
            Dim aLista(20, 1) As String
            aLista(0, 0) = "TopLeftHeightWidth"
            aLista(0, 1) = cDimensioni
            Call ABCA_Util.xmlSalvaElemento(cNomeForm, "FORM", aLista, 1)
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub FormDimensioniCarica(cNomeForm As String, oForm As System.Windows.Forms.Form)
        '**********************************************************************************
        ' Carico la dimensione della form 
        '**********************************************************************************
        Try
            Dim aLista(20, 1) As String
            Dim nAttributi As Integer = 0
            Call ABCA_Util.xmlLeggiElemento(cNomeForm, "FORM", aLista, nAttributi)
            Dim aRecord() As String
            aRecord = Split(ABCA_Util.ValoreDaLista("TopLeftHeightWidth", aLista, nAttributi, ""), "|")
            oForm.Top = CInt(aRecord(0))
            oForm.Left = CInt(aRecord(1))
            oForm.Height = CInt(aRecord(2))
            oForm.Width = CInt(aRecord(3))
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Function XML_Dataset_Salva(ByRef oDataset As DataSet, Optional cFileXml As String = "") As Boolean
        Try
            If cFileXml = "" Then
        cFileXml = ABCA_Util.AppPath() & "\ConfigWORD.xml"
      End If
            oDataset.WriteXml(cFileXml)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function XML_Dataset_Carica(ByRef oDataset As DataSet, Optional cFileXml As String = "") As Boolean
        Try
            If cFileXml = "" Then
        cFileXml = ABCA_Util.AppPath() & "\ConfigWORD.xml"
      End If
            Dim xmlFile As XmlReader
            xmlFile = XmlReader.Create(cFileXml, New XmlReaderSettings())
            oDataset.ReadXml(xmlFile)
            xmlFile.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function XML_Dataset_CaricaImpostazioni(ByRef aDati(,) As String, ByVal cTabella1 As String, Optional cTabella2 As String = "", Optional cFileXml As String = "") As Boolean
        Try
            Dim oDataset As New DataSet
            Dim nConta As Integer = 0
            Dim cAppoggio As String = ""
            If cFileXml = "" Then
                cFileXml = ABCA_Util.AppPath() & "\Settings.xml"
            End If
            Dim xmlFile As XmlReader
            xmlFile = XmlReader.Create(cFileXml, New XmlReaderSettings())
            oDataset.ReadXml(xmlFile)
            xmlFile.Close()
            Try
                For nConta = 1 To 10
                    If aDati(nConta, 1) > " " Then
                        cAppoggio = oDataset.Tables(cTabella1).Rows(0).Item(aDati(nConta, 1))
                        If cAppoggio > " " Then
                            aDati(nConta, 2) = cAppoggio
                        End If
                    End If
                Next
            Catch ex As Exception
            End Try
            Try
                For nConta = 1 To 10
                    If aDati(nConta, 1) > " " And aDati(nConta, 2) <= " " Then
                        cAppoggio = oDataset.Tables(cTabella2).Rows(0).Item(aDati(nConta, 1))
                        If cAppoggio > " " Then
                            aDati(nConta, 2) = cAppoggio
                        End If
                    End If
                Next
            Catch ex As Exception
            End Try
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function XML_Forms_Posizione(oForm As System.Windows.Forms.Form, cNomeForm As String, eTipoOperazione As enSalvaCarica) As Boolean
        Try
            Dim oDS As New DataSet
            Dim cAppoggio As String
            Dim bDataTableEsiste As Boolean
            bDataTableEsiste = False
            Call XML_Dataset_Carica(oDS, ABCA_Util.AppPath() & "\Settings.xml")
            Select Case eTipoOperazione
                Case enSalvaCarica.Carica
                    cAppoggio = oDS.Tables("POSIZIONIFORMS").Rows(0).Item(cNomeForm)
                    If cAppoggio > " " Then
                        Dim aDimensioni() As String = Split(cAppoggio, "|")
                        oForm.Top = VB.Val(aDimensioni(0))
                        oForm.Left = VB.Val(aDimensioni(1))
                        oForm.Height = VB.Val(aDimensioni(2))
                        oForm.Width = VB.Val(aDimensioni(3))
                    End If
                Case enSalvaCarica.Salva
                    For Each oDataTableParz As DataTable In oDS.Tables
                        'se c'è allora setto la variabile a true
                        If oDataTableParz.TableName.Contains("POSIZIONIFORMS") Then
                            ' qui verifico se esiste la voce
                            Try
                                oDataTableParz.Columns.Add(New DataColumn(cNomeForm, Type.GetType("System.String")))
                                Dim oDataRow As DataRow
                                oDataRow = oDataTableParz.NewRow()
                                oDataRow(0) = ""
                                oDataTableParz.Rows.Add(oDataRow)
                                oDS.Tables.Add(oDataTableParz)
                                bDataTableEsiste = True
                            Catch ex As Exception
                                bDataTableEsiste = True
                            End Try
                            bDataTableEsiste = True
                            Exit For
                        End If
                    Next
                    If bDataTableEsiste = False Then
                        ' non esiste il tatatable allora lo inserisco
                        Dim oDataTable As New DataTable("POSIZIONIFORMS")
                        oDataTable.Columns.Add(New DataColumn(cNomeForm, Type.GetType("System.String")))
                        Dim oDataRow As DataRow
                        oDataRow = oDataTable.NewRow()
                        oDataRow(0) = ""
                        oDataTable.Rows.Add(oDataRow)
                        oDS.Tables.Add(oDataTable)
                    End If
                    cAppoggio = "" & oForm.Top & "|" & oForm.Left & "|" & oForm.Height & "|" & oForm.Width
                    oDS.Tables("POSIZIONIFORMS").Rows(0).Item(cNomeForm) = cAppoggio
                    Call XML_Dataset_Salva(oDS, ABCA_Util.AppPath() & "\Settings.xml")
            End Select
        Catch ex As Exception
        End Try
        Return True
    End Function
End Class
