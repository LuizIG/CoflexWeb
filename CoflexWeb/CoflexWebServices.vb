
Imports System.IO
Imports System.Net
Imports System.Reflection
Imports Newtonsoft.Json.Linq

Namespace CoflexWeb.Services.Web
    Public Module CoflexWebServices

        Private Const SERVER_HOST As String = "http://62.151.178.139/CoflexAPI/"

        Public Const LOGIN As String = "Token"
        Public Const REGISTER As String = "api/Account/Register"
        Public Const ROLES As String = "api/Roles"

        Public Function doPostRequest(url As String, data As String, Optional ByVal contentType As String = "application/json") As String
            Dim request = createRequest(SERVER_HOST & url, "POST", contentType)
            sendData(request, data)
            Return getData(request)
        End Function

        Public Function doGetRequest(url As String, Optional ByVal contentType As String = "application/json") As String
            Dim request = createRequest(SERVER_HOST & url, "GET", contentType)
            Return getData(request)
        End Function

        Public Function doDeleteRequest(url As String, Optional ByVal contentType As String = "application/json") As String
            Dim request = createRequest(SERVER_HOST & url, "DELETE", contentType)
            Return getData(request)
        End Function


        Public Function doPutRequest(url As String, data As String, Optional ByVal contentType As String = "application/json") As String
            Dim request = createRequest(SERVER_HOST & url, "PUT", contentType)
            sendData(request, data)
            Return getData(request)
        End Function

        Private Function createRequest(ByVal url As String, ByVal method As String, Optional ByVal contentType As String = "application/json") As WebRequest
            Dim request As WebRequest = WebRequest.Create(url)
            request.Method = method
            request.ContentType = contentType
            Return request
        End Function


        Private Sub sendData(ByRef request As WebRequest, ByVal data As String)
            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(data)
            request.ContentLength = byteArray.Length
            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()
        End Sub


        Private Function getData(ByRef request As WebRequest) As String

            Try
                Dim response As WebResponse = request.GetResponse()
                Console.WriteLine(CType(response, HttpWebResponse).StatusCode)
                Dim dataStream As Stream = response.GetResponseStream()
                Dim reader As New StreamReader(dataStream)
                Dim responseFromServer As String = reader.ReadToEnd()
                reader.Close()
                dataStream.Close()
                response.Close()


                Dim responseStatus = "{" _
                    & """statusCode"":" & CType(response, HttpWebResponse).StatusCode & "," _
                    & """errorMessage"":""""," _
                    & """detail"":" & responseFromServer _
                    & "}"



                Return responseStatus
            Catch ex As WebException
                Dim resp = New StreamReader(ex.Response.GetResponseStream()).ReadToEnd()

                Dim StatusCode As Integer = 9999

                If ex.Status = WebExceptionStatus.ProtocolError Then
                    Dim response = TryCast(ex.Response, HttpWebResponse)
                    If response IsNot Nothing Then
                        StatusCode = CInt(response.StatusCode)
                        ' no http status code available
                    Else
                    End If
                    ' no http status code available
                Else
                End If

                Dim responseStatus = "{" _
                    & """statusCode"":" & StatusCode & "," _
                    & """errorMessage"":""Error""," _
                    & """detail"":" & resp _
                    & "}"
                Return responseStatus
            End Try
        End Function
    End Module
End Namespace