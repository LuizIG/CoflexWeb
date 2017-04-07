Imports System.Collections.Generic
Imports System.Security.Claims
Imports System.Security.Principal
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class SiteMaster
    Inherits MasterPage
    Private Const AntiXsrfTokenKey As String = "__AntiXsrfToken"
    Private Const AntiXsrfUserNameKey As String = "__AntiXsrfUserName"
    Private _antiXsrfTokenValue As String

    Protected Sub master_Page_PreLoad(sender As Object, e As EventArgs)

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'Dim item = HeadLoginView.AnonymousTemplate

        'Dim testDiv = HeadLoginView.FindControl("test1")
        'testDiv.Visible = True
        ''LoginView1.
        'Dim vista = Me.LoginView1.FindControl("Administrador")
        'vista.Visible = True

        'Dim rg As New RoleGroup
        'rg.ContentTemplate
        'rg.Roles(0).

    End Sub

    Protected Sub Unnamed_LoggingOut(sender As Object, e As LoginCancelEventArgs)
        ''Context.GetOwinContext().Authentication.SignOut()
    End Sub

    Private Sub SiteMaster_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender

    End Sub
End Class