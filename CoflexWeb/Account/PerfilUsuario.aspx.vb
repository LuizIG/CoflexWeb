Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class PerfilUsuario
    Inherits CoflexWebPage

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsPostBack Then
            MyBase.Page_Load(sender, e)
            Dim userId As String = Request.QueryString("id")
            Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.USERS & "/" & userId, , Session("access_token"))
            Dim o = JObject.Parse(jsonResponse)
            Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                Dim detail = o.GetValue("detail").Value(Of JObject)

                Me.UserName.Text = detail.GetValue("Email")
                Me.Name.Text = detail.GetValue("Name")
                Me.LastName.Text = detail.GetValue("PaternalSurname")
                Me.MotherSurname.Text = detail.GetValue("MaternalSurname")
                Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.GetValue("Roles").Value(Of JArray).ToString)
                Me.GridRoles.DataSource = Table
                Me.GridRoles.DataBind()
            Else

            End If
        End If
    End Sub
End Class