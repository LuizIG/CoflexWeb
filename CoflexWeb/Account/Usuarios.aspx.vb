Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Usuarios
    Inherits CoflexWebPage

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        MyBase.Page_Load(sender, e)
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
            Dim x = detail.Count
            For i As Integer = 0 To detail.Count - 1
                Dim item = DirectCast(detail(i), JObject)
                Dim roles = item.GetValue("Roles").Value(Of JArray)
                Dim rolesString As String = ""
                For j As Integer = 0 To roles.Count - 1
                    Dim roleItem = DirectCast(roles(j), JObject)
                    rolesString += roleItem.GetValue("Name").Value(Of String) & "<br/>"
                Next
                item.Add("RolesString", rolesString)
            Next
            Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)
            Me.GridUsers.DataSource = Table
            Me.GridUsers.DataBind()
        Else
            Dim errorMessage = o.GetValue("errorMessage").Value(Of String)
            Me.response.InnerText = errorMessage
        End If
    End Sub
End Class