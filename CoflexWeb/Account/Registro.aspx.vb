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
        Dim Registro As New JObject
        Dim Roles As New JArray
        Dim Rol As New JObject

        Rol.Add("Name", "Administrador")
        Roles.Add(Rol)

        With Registro
            .Add("Email", Me.UserName.Text)
            .Add("Password", Me.Password.Text)
            .Add("ConfirmPassword", Me.ConfirmPassword.Text)
            .Add("Name", Me.Name.Text)
            .Add("PaternalSurname", Me.LastName.Text)
            .Add("MaternalSurname", Me.MotherSurname.Text)
            .Add("Roles", Roles)
        End With

        ''role.Add("Roles", otro)

        CoflexWebServices.doPostRequest(CoflexWebServices.REGISTER, Registro.ToString)


        ''CoflexWebServices.doPostRequest()

    End Sub
End Class