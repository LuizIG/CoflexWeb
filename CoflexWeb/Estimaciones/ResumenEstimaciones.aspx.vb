
Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class ResumenEstimaciones
    Inherits CoflexWebPage
    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        MyBase.Page_Load(sender, e)
        If Not IsPostBack Then
            Dim Response = CoflexWebServices.doGetRequest(CoflexWebServices.QUOTATIONS, , Session("access_token"))
            Dim o = JObject.Parse(Response)
            Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                Dim detail = o.GetValue("detail").Value(Of JArray)
                Dim arrayLimpio = New JArray
                For Each Quotation As JObject In detail
                    Dim Client = Quotation.GetValue("AspNetUsersView").Value(Of JObject)
                    Dim UserName As String = Client.GetValue("Name").Value(Of String) & " " & Client.GetValue("PaternalSurname").Value(Of String) & " " & Client.GetValue("MaternalSurname").Value(Of String)
                    Dim Versions = Quotation.GetValue("QuotationVersions").Value(Of JArray)
                    For Each Version As JObject In Versions
                        Version.Remove("Items")
                        Version.Add("ClientName", Quotation.GetValue("ClientName").Value(Of String))
                        Version.Add("CoflexId", Quotation.GetValue("CoflexId").Value(Of String))
                        Select Case Quotation.GetValue("Status").Value(Of Integer)
                            Case 0
                                Version.Add("QStatus", "Abierta")
                            Case 1
                                Version.Add("QStatus", "Cerrada")
                            Case 2
                                Version.Add("QStatus", "Cancelada")
                        End Select
                        Version.Add("User", UserName)

                        Select Case Version.GetValue("Status").Value(Of Integer)
                            Case 0
                                Version.Add("VStatus", "Abierta")
                                Version.Add("ActionEdit", "<a href='Estimacion.aspx?q=" & Version.GetValue("QuotationsId").Value(Of Integer) & "&v=" & Version.GetValue("Id").Value(Of Integer) & "' class='btn btn-primary' role='button'>Editar</a>")
                            Case 1
                                Version.Add("VStatus", "Propuesta Cerrada")
                                Version.Add("ActionEdit", "<a href='Estimacion.aspx?q=" & Version.GetValue("QuotationsId").Value(Of Integer) & "&v=" & Version.GetValue("Id").Value(Of Integer) & "' class='btn btn-primary' role='button'>Ver</a>")
                            Case 2
                                Version.Add("VStatus", "Propuesta Descartada")
                                Version.Add("ActionEdit", "<a href='Estimacion.aspx?q=" & Version.GetValue("QuotationsId").Value(Of Integer) & "&v=" & Version.GetValue("Id").Value(Of Integer) & "' class='btn btn-primary' role='button'>Ver</a>")
                            Case 3
                                Version.Add("VStatus", "Aceptada")
                                Version.Add("ActionEdit", "<a href='Estimacion.aspx?q=" & Version.GetValue("QuotationsId").Value(Of Integer) & "&v=" & Version.GetValue("Id").Value(Of Integer) & "' class='btn btn-primary' role='button'>Ver</a>")
                            Case 4
                                Version.Add("VStatus", "Cancelada")
                                Version.Add("ActionEdit", "<a href='Estimacion.aspx?q=" & Version.GetValue("QuotationsId").Value(Of Integer) & "&v=" & Version.GetValue("Id").Value(Of Integer) & "' class='btn btn-primary' role='button'>Ver</a>")
                        End Select

                        arrayLimpio.Add(Version)
                    Next
                Next
                Dim Table As DataTable = JsonConvert.DeserializeObject(Of DataTable)(arrayLimpio.ToString)

                For Each row As DataRow In Table.Rows
                    tableQuotations.Rows.Add(GetRow(row))
                Next

            End If
        End If
    End Sub


    Private Function GetRow(ByVal row As DataRow) As HtmlTableRow
        Dim rowString As New HtmlTableRow

        Dim cell1 As New HtmlTableCell
        cell1.InnerText = row("CoflexId").ToString

        Dim cell2 As New HtmlTableCell
        cell2.InnerText = row("User").ToString

        Dim cell3 As New HtmlTableCell
        cell3.InnerText = row("ClientName").ToString

        Dim cell4 As New HtmlTableCell
        cell4.InnerText = row("QStatus").ToString

        Dim cell5 As New HtmlTableCell
        cell5.InnerText = row("VersionNumber").ToString

        Dim cell6 As New HtmlTableCell
        cell6.InnerText = row("Date").ToString

        Dim cell7 As New HtmlTableCell
        cell7.InnerText = row("VStatus").ToString

        Dim cell8 As New HtmlTableCell
        cell8.InnerHtml = row("ActionEdit").ToString

        rowString.Cells.Add(cell1)
        rowString.Cells.Add(cell2)
        rowString.Cells.Add(cell3)
        rowString.Cells.Add(cell4)
        rowString.Cells.Add(cell5)
        rowString.Cells.Add(cell6)
        rowString.Cells.Add(cell7)
        rowString.Cells.Add(cell8)

        Return rowString
    End Function

End Class