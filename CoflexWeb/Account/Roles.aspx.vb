Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class Roles
    Inherits CoflexWebPage
    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        MyBase.Page_Load(sender, e)
        If Not Me.IsPostBack Then
            GetRolesList()
        End If
    End Sub


    Private Sub AddNewRole(Name As String)
        If Name Is Nothing Or String.Compare(Name, "") = 0 Then
            MsgBox("No se puede dar de alta un nombre de rol vacío")
            Return
        End If

        Dim role As New JObject
        role.Add("Name", Name)

        Dim access_token = Session("access_token")

        Dim response = CoflexWebServices.doPostRequest(CoflexWebServices.ROLES, role.ToString)

        Dim o = JObject.Parse(response)
        Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
        If (statusCode >= 200 And statusCode < 400) Then
            GetRolesList()
        Else
            Dim errorMessage = o.GetValue("errorMessage").Value(Of String)
            Me.response.InnerText = errorMessage
        End If
    End Sub

    Private Sub GetRolesList()
        Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.ROLES, Session("access_token"))
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