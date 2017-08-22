Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json.Linq

Public Class Adjunto
    Inherits CoflexWebPage

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim x As String = Request.QueryString("id")

        Dim Consulta = doGetRequest(ATTACHMENTS & "/" & x,, Session("access_token"))
        Dim o = JObject.Parse(Consulta)
        Dim statusCode = o.GetValue("statusCode").Value(Of Integer)

        If (statusCode >= 200 And statusCode < 400) Then
            Dim detail = o.GetValue("detail").Value(Of JObject)

            Dim res = detail.GetValue("FileBinary").Value(Of String)


            Dim newBytes() As Byte = Convert.FromBase64String(res)

            Response.ContentType = "application/pdf"
            Response.AddHeader("Content-disposition", "inline")
            Response.BinaryWrite(newBytes)

        Else

        End If


    End Sub

End Class