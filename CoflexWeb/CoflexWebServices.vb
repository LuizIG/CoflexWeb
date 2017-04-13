
Imports System.IO
Imports System.Net
Imports System.Reflection
Imports Newtonsoft.Json.Linq

Namespace CoflexWeb.Services.Web
    Public Module CoflexWebServices

        Private Const SERVER_HOST As String = "http://62.151.178.139/CoflexAPI/"
        'Private Const SERVER_HOST As String = "http://localhost/"
        'Private Const SERVER_HOST As String = "http://localhost/coflexAPI"

        Public Const LOGIN As String = "Token"
        Public Const REGISTER As String = "api/Account/Register"
        Public Const CHANGEPASSWORD As String = "api/Account/ChangePassword"
        Public Const SETPASSWORD As String = "api/Account/SetPassword"
        Public Const ROLES_ADMIN_ADD As String = "api/Account/Roles/Add"
        Public Const ROLES_ADMIN_DEL As String = "api/Account/Roles/Remove"
        Public Const ROLES As String = "api/Roles"
        Public Const USERS As String = "api/Account"
        Public Const USERS_ALT As String = "api/Account/Edit"
        Public Const USERS_ESTATUS As String = "api/Account/Enable"
        Public Const ITEM As String = "api/Items"
        Public Const ITEM_COMPONENTS As String = "api/ItemComponents"
        Public Const COMPONENT As String = "api/Components"
        'Public Const ITEMCOMPONENTS As String = "api/ItemComponents"


        Public itemsLists As New ItemsComponentsCollection

        Public Function doPostRequest(url As String, data As String, Optional ByVal contentType As String = "application/json", Optional token As String = "") As String
            Dim request = createRequest(SERVER_HOST & url, "POST", contentType, token)
            sendData(request, data)
            Return getData(request)
        End Function

        Public Function doGetRequest(url As String, Optional ByVal contentType As String = "application/json", Optional token As String = "") As String
            Dim request = createRequest(SERVER_HOST & url, "GET", contentType, token)
            Return getData(request)
        End Function

        Public Function doDeleteRequest(url As String, Optional ByVal contentType As String = "application/json", Optional token As String = "") As String
            Dim request = createRequest(SERVER_HOST & url, "DELETE", contentType, token)
            Return getData(request)
        End Function


        Public Function doPutRequest(url As String, data As String, Optional ByVal contentType As String = "application/json", Optional token As String = "") As String
            Dim request = createRequest(SERVER_HOST & url, "PUT", contentType, token)
            sendData(request, data)
            Return getData(request)
        End Function

        Private Function createRequest(ByVal url As String, ByVal method As String, Optional ByVal contentType As String = "application/json", Optional token As String = "") As WebRequest
            Dim request As WebRequest = WebRequest.Create(url)
            request.Method = method
            request.ContentType = contentType
            If Not Strings.StrComp(token, "") = 0 Then
                request.Headers.Add("Authorization", "bearer " & token)
            End If
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
                If (responseFromServer Is Nothing Or String.Compare(responseFromServer, "") = 0) Then
                    responseFromServer = "{}"
                End If
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

                Dim errorJSON = JObject.Parse(resp)
                Dim ErrorString As String = ""
                Try
                    ErrorString = errorJSON.GetValue("error_description").Value(Of String)
                Catch e As Exception

                End Try

                Try
                    ErrorString = errorJSON.GetValue("Message").Value(Of String)
                Catch e As Exception

                End Try

                Dim responseStatus = "{" _
                    & """statusCode"":" & StatusCode & "," _
                    & """errorMessage"":""" & ErrorString & """," _
                    & """detail"":" & resp _
                    & "}"
                Return responseStatus
            End Try
        End Function
    End Module
End Namespace