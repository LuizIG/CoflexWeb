
Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class ResumenEstimaciones
    Inherits CoflexWebPage
    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        MyBase.Page_Load(sender, e)
        If Not IsPostBack Then
            Dim Response = CoflexWebServices.doGetRequest(CoflexWebServices.QUOTATIONS_SUMMARY, , Session("access_token"))
            Dim o = JObject.Parse(Response)
            Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                Dim detail = o.GetValue("detail").Value(Of JArray)

                Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)
                Table.DefaultView.Sort = "date desc"
                ViewState("CurrentTable") = Table
                Me.GridQuotations.DataSource = Table
                Me.GridQuotations.DataBind()

                GridQuotations.EmptyDataText = "No se encontraron cotizaciones"

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

                    Me.DDVendedor.DataSource = Table
                    Me.DDVendedor.DataValueField = "Name"
                    Me.DDVendedor.DataTextField = "Name"
                    Me.DDVendedor.DataBind()
                    Me.DDVendedor.Items.Insert(0, New ListItem("Seleccionar", ""))


                    'CASE dbo.QuotationVersions.Status WHEN 0 THEN 'Abierta' WHEN 1 THEN 'Propuesta Cerrada' WHEN 2 THEN 'Propuesta Descartada' WHEN 3 THEN 'Aceptada' WHEN 4 THEN 'Cancelada' END
                    DDStatusVersion.Items.Add(New ListItem("Seleccionar", ""))
                    DDStatusVersion.Items.Add(New ListItem("Abierta", 0))
                    DDStatusVersion.Items.Add(New ListItem("Propuesta Cerrada", 1))
                    DDStatusVersion.Items.Add(New ListItem("Propuesta Descartada", 2))
                    DDStatusVersion.Items.Add(New ListItem("Aceptada", 3))
                    DDStatusVersion.Items.Add(New ListItem("Cancelada", 4))

                    DDStatusCotiza.Items.Add(New ListItem("Seleccionar", ""))
                    DDStatusCotiza.Items.Add(New ListItem("Abierta", 0))
                    DDStatusCotiza.Items.Add(New ListItem("Cerrada", 1))
                    DDStatusCotiza.Items.Add(New ListItem("Cancelada", 2))

                    'Llenamos los filtros
                End If
            End If
        End If
    End Sub

    Protected Sub ButtonEstimacionGo_Click(sender As Object, e As EventArgs) Handles ButtonEstimacionGo.Click
        Response.Redirect("/Estimaciones/Estimacion.aspx")
    End Sub

    Private Sub BTN_ACEPTAR_1_Click(sender As Object, e As EventArgs) Handles BTN_ACEPTAR_1.Click

        Dim quotations = quotations_reasign.Value.Split(", ")

        For Each Q In quotations

            If Q IsNot Nothing And Q <> "" Then

                Dim response = CoflexWebServices.doPutRequest(CoflexWebServices.QUOTATIONS & "/" & Q & "?UserId=" & DDUsers.SelectedValue, "{}",, Session("access_token"))

                Dim o = JObject.Parse(response)
                Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
                If (statusCode >= 200 And statusCode < 400) Then


                End If
            End If

        Next

        Response.Redirect(Request.RawUrl)

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            Dim Qstring As String = "   "

            If Me.txtCotizacion.Text <> "" Then
                ''rows2 = dt.[Select]("CoflexId like '" & Me.txtCotizacion.Text & "%'")
                Qstring = Qstring + "CoflexId like '" & Me.txtCotizacion.Text & "%' and"
            End If

            If Me.DDVendedor.SelectedValue <> "" Then
                ''rows2 = dt.[Select]("vendor like '%" & Me.txtVendedor.Text & "%'")
                Qstring = Qstring + " vendor like '%" & Me.DDVendedor.SelectedValue & "%' and"
            End If

            If Me.txtCliente.Text <> "" Then
                ''rows2 = dt.[Select]("clientName like '%" & Me.txtCliente.Text & "%'")
                Qstring = Qstring + " clientName like '%" & Me.txtCliente.Text & "%' and"
            End If

            If Me.DDStatusCotiza.SelectedValue <> "" Then
                ''rows2 = dt.[Select]("clientName like '%" & Me.txtCliente.Text & "%'")
                Qstring = Qstring + " status = " & Me.DDStatusCotiza.SelectedValue & " and"
            End If

            If Me.txtVersion.Text <> "" Then
                ''rows2 = dt.[Select]("clientName like '%" & Me.txtCliente.Text & "%'")
                Qstring = Qstring + " VersionNumber = '" & Me.txtVersion.Text & "' and"
            End If

            If Me.TextBox1.Text <> "" And Me.TextBox2.Text <> "" Then
                ''rows2 = dt.[Select]("clientName like '%" & Me.txtCliente.Text & "%'")
                Qstring = Qstring + " Date >= '" & Me.TextBox1.Text & "' and Date <= '" & Me.TextBox2.Text & "' and"
            End If

            If Me.DDStatusVersion.SelectedValue <> "" Then
                ''rows2 = dt.[Select]("clientName like '%" & Me.txtCliente.Text & "%'")
                Qstring = Qstring + " VStatus = " & Me.DDStatusVersion.SelectedValue & " and"
            End If

            Qstring = Left(Qstring, Qstring.Length - 3)
            Dim rows2 = dt.[Select](Qstring)

            If (rows2.Count > 0) Then
                Dim dt1 As DataTable = rows2.CopyToDataTable()
                dt1.DefaultView.Sort = "date desc"
                Me.GridQuotations.DataSource = dt1
                Me.GridQuotations.DataBind()
            Else
                Dim dt1 As DataTable = dt.Clone
                Me.GridQuotations.DataSource = dt1
                Me.GridQuotations.DataBind()
            End If
        End If
    End Sub

    Private Sub GridQuotations_DataBound(sender As Object, e As GridViewRowEventArgs) Handles GridQuotations.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then


            Dim rowView As DataRowView = DirectCast(e.Row.DataItem, DataRowView)

            ' Retrieve the EventTypeID value for the current row. 


            Dim q As Integer = Convert.ToInt32(rowView("Id"))
            Dim v As Integer = Convert.ToInt32(rowView("IdVersion"))
            e.Row.Attributes("id") = q & "," & v
        End If
    End Sub

    Protected Sub ButtonIndicadores_Click(sender As Object, e As EventArgs) Handles ButtonIndicadores.Click
        Response.Redirect("/Estimaciones/Indicadores.aspx")
    End Sub

End Class





