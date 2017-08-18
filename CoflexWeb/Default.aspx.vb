Imports System.IO
Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json.Linq
Public Class Acceso
    Inherits CoflexWebPage
    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If (Session("access_token") IsNot Nothing) Then
            Response.Redirect("Main.aspx")
        End If
        Page.Form.Attributes.Add("enctype", "multipart/form-data")
    End Sub

    Protected Sub LogIn(sender As Object, e As EventArgs)
        Dim Consulta = CoflexWebServices.doPostRequest(CoflexWebServices.LOGIN, "grant_type=password&username=" & Me.UserName.Text & "&password=" & Me.Password.Text, "application/x-www-form-urlencoded")
        Dim o = JObject.Parse(Consulta)
        Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
        If (statusCode >= 200 And statusCode < 400) Then
            Dim detail = o.GetValue("detail").Value(Of JObject)
            Dim accessToken = detail.GetValue("access_token").Value(Of String)
            Session("access_token") = accessToken
            Session("expire") = False
            Session("roles") = detail.GetValue("Roles").Value(Of String)
            Response.Redirect("~/Main.aspx")
        Else
            Session.Abandon()
            Session("expire") = True
            Dim errorMessage = o.GetValue("errorMessage").Value(Of String)
            error_msg.InnerHtml = GetErrorDiv(errorMessage)
        End If
    End Sub

    Private Sub file_uploader_Load(sender As Object, e As EventArgs) Handles btnUpload.Click
        If FileUploadControl.HasFile Then
            Try
                For Each file As HttpPostedFile In FileUploadControl.PostedFiles

                    If file.ContentType = "application/pdf" Then
                        Dim filename As String = Path.GetFileName(file.FileName)
                        MsgBox(filename)
                        Dim fileData As Byte() = Nothing
                        Using binaryReader = New BinaryReader(file.InputStream)
                            fileData = binaryReader.ReadBytes(file.ContentLength)
                        End Using

                        Dim str = Convert.ToBase64String(fileData)

                        Dim Elemento As New JObject
                        With Elemento
                            .Add("QuotationsId", 1077)
                            .Add("FileName", filename)
                            .Add("FileBinary", str)
                            .Add("Comments", "Comentarios")
                        End With
                        '1077

                        Dim Consulta = CoflexWebServices.doPostRequest(CoflexWebServices.ATTACHMENTS, Elemento.ToString)
                        Dim o = JObject.Parse(Consulta)
                        Dim statusCode = o.GetValue("statusCode").Value(Of Integer)

                        If (statusCode >= 200 And statusCode < 400) Then
                            Dim detail = o.GetValue("detail").Value(Of JArray)
                        Else

                        End If


                    End If

                Next
            Catch ex As Exception
                MsgBox(ex.StackTrace)
            End Try
        End If

    End Sub
End Class

