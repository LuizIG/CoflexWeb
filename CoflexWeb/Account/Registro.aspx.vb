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

Public Class Registro
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub CreateUser_Click(sender As Object, e As EventArgs)
        Dim role As New JObject
        role.Add("Email", Me.UserName.Text)
        role.Add("Password", Me.UserName.Text)
        role.Add("ConfirmPassword", Me.UserName.Text)
        role.Add("Name", Me.UserName.Text)
        role.Add("PaternalSurname", Me.UserName.Text)
        role.Add("MaternalSurname", Me.UserName.Text)
        role.Add("Roles", "[{""Name"" : ""Administrador""}]")
    End Sub
End Class