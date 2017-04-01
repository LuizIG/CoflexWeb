Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class Roles
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.ROLES)
        Dim o = JObject.Parse(jsonResponse)
        Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
        If (statusCode >= 200 And statusCode < 400) Then
            Dim detail = o.GetValue("detail").Value(Of JArray)
            Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)
            Me.GridRoles.DataSource = Table
            Me.GridRoles.DataBind()
        Else
            Dim errorMessage = o.GetValue("errorMessage").Value(Of String)
            Me.response.InnerText = errorMessage
        End If
    End Sub
End Class