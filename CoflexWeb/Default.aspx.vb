Imports CoflexWeb.CoflexWeb.Services.Wev

Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load


        Me.response.InnerText = CoflexWebServices.doPostRequest(CoflexWebServices.LOGIN, "grant_type=password&username=luis.ibarra0992@gmail.com&password=a")


    End Sub
End Class