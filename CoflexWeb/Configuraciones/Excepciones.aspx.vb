Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Excepciones
    Inherits CoflexWebPage

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        MyBase.Page_Load(sender, e)
        div_error_reorder_grid.Style.Add("display", "none")
        If Not Page.IsPostBack Then
            LoadAvailables()
            LoadExceptions()
        End If

    End Sub

    Private Sub btn_agregar_ServerClick(sender As Object, e As EventArgs) Handles btn_agregar.ServerClick

        Dim Elemento As New JObject
        Try
            If (available_clients.SelectedItem IsNot Nothing And available_clients.SelectedItem.Text <> "Seleccionar") Then

                With Elemento
                    .Add("ClientNotOEM", available_clients.SelectedItem.Value.Trim)
                    .Add("ClientName", available_clients.SelectedItem.Text.Trim)
                End With

                Dim Consulta = CoflexWebServices.doPostRequest(CoflexWebServices.CLIENTSNOTOEM_LOCAL & "/" & ID, Elemento.ToString,, Session("access_token"))

                Dim o = JObject.Parse(Consulta)
                Dim statusCode = o.GetValue("statusCode").Value(Of Integer)

                If (statusCode >= 200 And statusCode < 400) Then
                    LoadAvailables()
                    LoadExceptions()
                Else
                    div_error_reorder_grid.Style.Add("display", "block")
                    div_error_reorder_grid_desc.InnerText = "Ocurrió un error en el servidor."
                End If

            Else
                div_error_reorder_grid.Style.Add("display", "block")
                div_error_reorder_grid_desc.InnerText = "Selecciona un elemento."

            End If
        Catch ex As Exception
            div_error_reorder_grid.Style.Add("display", "block")
            div_error_reorder_grid_desc.InnerText = "Selecciona un elemento."
        End Try


    End Sub

    Protected Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click

        Dim id = id_user.Value

        Dim Consulta = CoflexWebServices.doDeleteRequest(CoflexWebServices.CLIENTSNOTOEM_LOCAL & "/" & id,, Session("access_token"))

        Dim o = JObject.Parse(Consulta)
        Dim statusCode = o.GetValue("statusCode").Value(Of Integer)

        If (statusCode >= 200 And statusCode < 400) Then
            LoadAvailables()
            LoadExceptions()
        Else
            div_error_reorder_grid.Style.Add("display", "block")
            div_error_reorder_grid_desc.InnerText = "Ocurrió un error en el servidor."
        End If

    End Sub

    Private Sub LoadExceptions()

        Dim Consulta = CoflexWebServices.doGetRequest(CoflexWebServices.CLIENTSNOTOEM_LOCAL,, Session("access_token"))
        Dim o = JObject.Parse(Consulta)
        Dim statusCode = o.GetValue("statusCode").Value(Of Integer)

        If (statusCode >= 200 And statusCode < 400) Then
            Dim detail = o.GetValue("detail").Value(Of JArray)

            Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)

            GridAttachment.DataSource = Table
            GridAttachment.DataBind()
        Else

        End If

    End Sub


    Private Sub LoadAvailables()

        Dim Consulta = CoflexWebServices.doGetRequest(CoflexWebServices.CLIENTSNOTOEM,, Session("access_token"))
        Dim o = JObject.Parse(Consulta)
        Dim statusCode = o.GetValue("statusCode").Value(Of Integer)

        If (statusCode >= 200 And statusCode < 400) Then
            Dim detail = o.GetValue("detail").Value(Of JArray)

            Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)

            available_clients.DataSource = Table
            available_clients.DataValueField = "Id"
            available_clients.DataTextField = "ClientName"

            available_clients.DataBind()
        Else

        End If

        available_clients.Items.Insert(0, "Seleccionar")

    End Sub


End Class