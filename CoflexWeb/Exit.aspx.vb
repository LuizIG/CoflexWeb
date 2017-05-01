Public Class _Exit
    Inherits CoflexWebPage

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Session.Abandon()
        ''MyBase.Page_Load(sender, e)
        Response.Redirect("~/")
    End Sub

End Class