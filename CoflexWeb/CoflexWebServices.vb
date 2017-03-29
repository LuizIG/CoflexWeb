
Imports System.IO
Imports System.Net
Namespace CoflexWeb.Services.Wev
    Public Module CoflexWebServices

        Private Const SERVER_HOST As String = "http://62.151.178.139/CoflexAPI/"

        Public Const LOGIN As String = "Token"
        Public Const REGISTER As String = "api/Account/Register"
        Public Const ROLES As String = "api/Roles"

        ''' <summary>
        ''' Controla las peticiones POST
        ''' </summary>
        ''' <param name="url"></param>
        ''' <param name="data">String que se enviara</param>
        ''' <returns></returns>
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
                Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
                Dim dataStream As Stream = response.GetResponseStream()
                Dim reader As New StreamReader(dataStream)
                Dim responseFromServer As String = reader.ReadToEnd()
                reader.Close()
                dataStream.Close()
                response.Close()
                Return responseFromServer
            Catch ex As WebException
                Dim resp = New StreamReader(ex.Response.GetResponseStream()).ReadToEnd()
                Return resp
            End Try
        End Function
    End Module
End Namespace