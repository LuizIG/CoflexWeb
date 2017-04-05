Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Usuarios
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GetUsersList()
        End If
    End Sub

    Private Sub GetUsersList()
        Dim accessToken As String = Session("access_token")
        Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.USERS,, accessToken)
        Dim o = JObject.Parse(jsonResponse)
        Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
        If (statusCode >= 200 And statusCode < 400) Then
            Dim detail = o.GetValue("detail").Value(Of JArray)
            Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)
            Me.GridUsers.DataSource = Table
            Me.GridUsers.DataBind()
        Else
            Dim errorMessage = o.GetValue("errorMessage").Value(Of String)
            Me.response.InnerText = errorMessage
        End If
    End Sub

    Private Sub GridUsers_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles GridUsers.SelectedIndexChanging
        Dim row = GridUsers.SelectedRow

        MsgBox(row.Cells(0).Text)

    End Sub
End Class