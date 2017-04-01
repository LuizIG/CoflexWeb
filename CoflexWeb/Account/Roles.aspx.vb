Option Explicit Off
Option Strict Off
Option Infer On
Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class Roles
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.ROLES)


        Dim o = JObject.Parse(jsonResponse)
        Dim jon = o.GetValue("statusCode")


        Dim statusCode = jon.Value(Of Integer)


        Try


        Catch ex As Exception
            Me.response.InnerText = jsonResponse & " " & ex.StackTrace
        End Try




    End Sub

End Class