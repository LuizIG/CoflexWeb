Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Usuarios
    Inherits CoflexWebPage

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsPostBack Then
            MyBase.Page_Load(sender, e)
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
                If item.GetValue("Enable").Value(Of Boolean) Then
                    item.Add("ActionButton", "<a idUser='" & item.GetValue("Id").Value(Of String) & "' class='btn btn-danger deleteUser' role='button'>Eliminar</a>")
                Else
                    item.Add("ActionButton", "<a idUser='" & item.GetValue("Id").Value(Of String) & "' class='btn btn-info activateUser' role='button'>Activar</a>")
                End If
            Next
            Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)
            GridUsers.DataSource = Table
            GridUsers.DataBind()
        Else
            Dim errorMessage = o.GetValue("errorMessage").Value(Of String)
            response.InnerText = errorMessage
        End If
    End Sub

    Protected Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click
        OnUserEstatusResult(CoflexWebServices.doPutRequest(CoflexWebServices.USERS_ESTATUS, getUserEstatusObject(False).ToString,, Session("access_token")))
    End Sub
    Protected Sub btn_activate_Click(sender As Object, e As EventArgs) Handles btn_activate.Click
        OnUserEstatusResult(CoflexWebServices.doPutRequest(CoflexWebServices.USERS_ESTATUS, getUserEstatusObject(True).ToString,, Session("access_token")))
    End Sub

    Private Sub OnUserEstatusResult(ByVal jsonResponse As String)
        Dim o = JObject.Parse(jsonResponse)
        Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
        If (statusCode >= 200 And statusCode < 400) Then
            GetUsersList()
        Else
            Dim errorMessage = o.GetValue("errorMessage").Value(Of String)
            MsgBox(errorMessage)
        End If

    End Sub

    Private Function getUserEstatusObject(ByVal estatus As Boolean) As JObject
        Dim user As New JObject
        With user
            .Add("UserId", Me.id_user.Value)
            .Add("Enable", estatus)
        End With
        Return user
    End Function
End Class