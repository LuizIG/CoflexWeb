Public Class CoflexWebPage
    Inherits System.Web.UI.Page
    Protected Overridable Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("access_token") Is Nothing) Then
            Response.Redirect("~/")
        End If
    End Sub
    Protected Function GetErrorDiv(ByVal errorMessage As String) As String
        Return "<div class='alert alert-danger alert-dismissable fade in'><a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a><strong>Ups!</strong> " & errorMessage & "</div>"
    End Function
    Protected Function ShowProgressBar() As String
        Return "<div class='progress progress-striped active' style='height: 8px;'><div class='progress-bar' role='progressbar' aria-valuenow='100' aria-valuemin='0' aria-valuemax='100' style='width 100%'></div></div>"
    End Function
End Class
