Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json.Linq
Public Class Acceso
    Inherits CoflexWebPage
    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If (Session("access_token") IsNot Nothing) Then
            Response.Redirect("Main.aspx")
        End If
    End Sub

    Protected Sub LogIn(sender As Object, e As EventArgs)
        Dim Consulta = CoflexWebServices.doPostRequest(CoflexWebServices.LOGIN, "grant_type=password&username=" & Me.UserName.Text & "&password=" & Me.Password.Text, "application/x-www-form-urlencoded")
        Dim o = JObject.Parse(Consulta)
        Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
        If (statusCode >= 200 And statusCode < 400) Then
            Dim detail = o.GetValue("detail").Value(Of JObject)
            Dim accessToken = detail.GetValue("access_token").Value(Of String)
            Session("access_token") = accessToken
            Session("expire") = False
            Session("roles") = detail.GetValue("Roles").Value(Of String)
            Response.Redirect("~/Main.aspx")
        Else
            Session.Abandon()
            Session("expire") = True
            Dim errorMessage = o.GetValue("errorMessage").Value(Of String)
            error_msg.InnerHtml = GetErrorDiv(errorMessage)
        End If
    End Sub
End Class

