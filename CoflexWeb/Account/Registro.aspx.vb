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
        Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.ROLES)
        Dim o = JObject.Parse(jsonResponse)
        Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
        If (statusCode >= 200 And statusCode < 400) Then
            Dim detail = o.GetValue("detail").Value(Of JArray)
            Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)
            Me.GridRoles.DataSource = Table
            Me.GridRoles.DataBind()
            'Else
            '    Dim errorMessage = o.GetValue("errorMessage").Value(Of String)
            '    Me.Response.InnerText = errorMessage
        End If
    End Sub

    Protected Sub CreateUser_Click(sender As Object, e As EventArgs)
        Dim Registro As New JObject
        Dim Roles As New JArray
        Dim Rol As New JObject

        Dim data As String = ""
        For Each row As GridViewRow In Me.GridRoles.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkSelect"), CheckBox)
                If chkRow.Checked Then
                    Rol.Add("Name", "Administrador")

                End If
            End If
        Next

        ''Rol.Add("Name", "Administrador")
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

    Private Sub GridRoles_PreRender(sender As Object, e As EventArgs) Handles GridRoles.PreRender
    End Sub
End Class