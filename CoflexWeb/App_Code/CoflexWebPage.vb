Public Class CoflexWebPage
    Inherits System.Web.UI.Page
    Protected Overridable Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("access_token") Is Nothing) Then
            Response.Redirect("~/Acceso.aspx")
        End If
    End Sub
End Class
