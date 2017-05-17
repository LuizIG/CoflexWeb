
Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class ResumenEstimaciones3
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
                                Version.Add("ActionEdit", "Editar")
                            Case 1
                                Version.Add("VStatus", "Propuesta Cerrada")
                                Version.Add("ActionEdit", "Ver")
                            Case 2
                                Version.Add("VStatus", "Propuesta Descartada")
                                Version.Add("ActionEdit", "Ver")
                            Case 3
                                Version.Add("VStatus", "Aceptada")
                                Version.Add("ActionEdit", "Ver")
                            Case 4
                                Version.Add("VStatus", "Cancelada")
                                Version.Add("ActionEdit", "Ver")
                        End Select

                        arrayLimpio.Add(Version)
                    Next
                Next
                Dim Table As DataTable = JsonConvert.DeserializeObject(Of DataTable)(arrayLimpio.ToString)

                Dim index As Integer = 0
                For Each row As DataRow In Table.Rows
                    tableQuotations.InnerHtml &= GetRow(row, index)
                    index = index + 1
                Next

                Response = doGetRequest(USERS_BY_LEADERS,, Session("access_token"))
                o = JObject.Parse(Response)
                statusCode = o.GetValue("statusCode").Value(Of Integer)
                If (statusCode >= 200 And statusCode < 400) Then
                    detail = o.GetValue("detail").Value(Of JArray)

                    For Each Vendedor As JObject In detail
                        Vendedor.Remove("Claims")
                        Vendedor.Remove("Logins")
                        Vendedor.Remove("Roles")
                    Next

                    Table = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)
                    Me.DDUsers.DataSource = Table
                    Me.DDUsers.DataValueField = "Id"
                    Me.DDUsers.DataTextField = "Name"
                    Me.DDUsers.DataBind()
                End If
            End If
        End If
    End Sub


    Private Function GetRow(ByVal row As DataRow, ByVal index As Integer) As String
        Dim rowString As String
        rowString = "<tr>"
        rowString &= "<td class='bs-checkbox'>"
        rowString &= "<input data-index='" & index & "' name='btSelectItem' type='checkbox'>"
        rowString &= "</td>"
        rowString &= "<td id='" & row("QuotationsId").ToString & "," & row("Id").ToString & "'>"
        rowString &= row("CoflexId").ToString
        rowString &= "</td>"
        rowString &= "<td>"
        rowString &= row("User").ToString
        rowString &= "</td>"
        rowString &= "<td>"
        rowString &= row("ClientName").ToString
        rowString &= "</td>"
        rowString &= "<td>"
        rowString &= row("QStatus").ToString
        rowString &= "</td>"
        rowString &= "<td>"
        rowString &= row("VersionNumber").ToString
        rowString &= "</td>"
        rowString &= "<td class='date'>"
        rowString &= row("Date").ToString.Split(" ")(0)
        rowString &= "</td>"
        rowString &= "<td>"
        rowString &= row("VStatus").ToString
        rowString &= "</td>"
        rowString &= "</tr>"
        Return rowString
    End Function

    Protected Sub ButtonEstimacionGo_Click(sender As Object, e As EventArgs) Handles ButtonEstimacionGo.Click
        Response.Redirect("~/Estimaciones/Estimacion.aspx")
    End Sub

    Private Sub BTN_ACEPTAR_1_Click(sender As Object, e As EventArgs) Handles BTN_ACEPTAR_1.Click

        Dim quotations = quotations_reasign.Value.Split(",")

        For Each Q In quotations

            If Q IsNot Nothing And Q <> "" Then

                Dim response = CoflexWebServices.doPutRequest(CoflexWebServices.QUOTATIONS & "/" & Q & "?UserId=" & DDUsers.SelectedValue, "{}",, Session("access_token"))

                Dim o = JObject.Parse(response)
                Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
                If (statusCode >= 200 And statusCode < 400) Then


                End If
            End If

        Next

        Response.Redirect("ResumenEstimaciones.aspx")

    End Sub


End Class





