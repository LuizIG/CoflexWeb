Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports System
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Dynamic
Imports System.Dynamic.ExpandoObject


Public Partial Class Account_Register
    Inherits Page
    Protected Sub CreateUser_Click(sender As Object, e As EventArgs)
        'Dim manager = New UserManager()
        'Dim user = New ApplicationUser() With {.UserName = userName.Text}
        'Dim result = manager.Create(user, Password.Text)
        'If result.Succeeded Then
        '    IdentityHelper.SignIn(manager, user, isPersistent:=False)
        '    IdentityHelper.RedirectToReturnUrl(Request.QueryString("ReturnUrl"), Response)
        'Else
        '    ErrorMessage.Text = result.Errors.FirstOrDefault()
        'End If

        Dim role As New JObject

    End Sub
End Class
