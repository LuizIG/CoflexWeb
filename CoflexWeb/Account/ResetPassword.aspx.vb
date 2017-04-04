Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class ResetPassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub ChangePassword_Click(sender As Object, e As EventArgs)
        Dim Password As New JObject

        With Password
            .Add("OldPassword", Me.CurrentPassword.Text)
            .Add("NewPassword", Me.NewPassword.Text)
            .Add("ConfirmPassword", Me.ConfirmNewPassword.Text)
        End With



        Dim jsonResponse = CoflexWebServices.doPostRequest(CoflexWebServices.CHANGEPASSWORD, Password.ToString,, Session("access_token"))
        Dim o = JObject.Parse(jsonResponse)
        Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
        If (statusCode >= 200 And statusCode < 400) Then
            Me.ErrorMessage.Text = "Cambio de contraseña"
        Else
            Dim errorMessage = o.GetValue("errorMessage").Value(Of String)
            Me.ErrorMessage.Text = errorMessage
        End If

    End Sub
End Class