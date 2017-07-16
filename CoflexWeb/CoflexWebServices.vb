
Imports System.IO
Imports System.Net
Imports System.Reflection
Imports Newtonsoft.Json.Linq
Imports System.Configuration

Namespace CoflexWeb.Services.Web
    Public Module CoflexWebServices

        Private SERVER_HOST As String = ConfigurationManager.AppSettings("url").ToString

        'Private Const SERVER_HOST As String = "http://localhost:59204/"

        Public Const LOGIN As String = "CoflexAPI/Token"
        Public Const REGISTER As String = "CoflexAPI/api/Account/Register"
        Public Const CHANGEPASSWORD As String = "CoflexAPI/api/Account/ChangePassword"
        Public Const SETPASSWORD As String = "CoflexAPI/api/Account/SetPassword"
        Public Const ROLES_ADMIN_ADD As String = "CoflexAPI/api/Account/Roles/Add"
        Public Const ROLES_ADMIN_DEL As String = "CoflexAPI/api/Account/Roles/Remove"
        Public Const ROLES As String = "CoflexAPI/api/Roles"
        Public Const USERS As String = "CoflexAPI/api/Account"
        Public Const USERS_ALT As String = "CoflexAPI/api/Account/Edit"
        Public Const USERS_ESTATUS As String = "CoflexAPI/api/Account/Enable"
        Public Const QUOTATIONS As String = "CoflexAPI/api/Quotations"
        Public Const QUOTATIONS_VERSION As String = "CoflexAPI/api/QuotationVersions"
        Public Const NEW_COMPONENTES As String = "CoflexAPI/api/NewComponents"
        Public Const LEADERS As String = "CoflexAPI/api/Account/Leaders"
        Public Const USERS_BY_LEADERS As String = "CoflexAPI/api/Account/UsersByLeader"
        Public Const QUOTATIONS_SUMMARY As String = "CoflexAPI/api/QuotationSummary"
        Public Const PROSPECTS As String = "CoflexAPI/api/Prospects"
        Public Const INDICATORS As String = "CoflexAPI/api/Indicator"
        Public Const QUOTATION_COMMENTS As String = "CoflexAPI/api/Comments"
        ''api/Indicator?min={min}&max={max}

        'Enpoints Externos
        Public Const ITEM As String = "CoflexAPIExt/api/Items"
        Public Const ITEM_COMPONENTS As String = "CoflexAPIExt/api/ItemComponents"
        Public Const COMPONENT As String = "CoflexAPIExt/api/Components"
        Public Const DETAIL_COMPONENTS As String = "CoflexAPIExt/api/DetailComponets"
        Public Const CLIENTS As String = "CoflexAPIExt/api/Clients"
        Public Const EXCHANGE_RATE As String = "CoflexAPIExt/api/ExchangeRate/"
        Public Const ITEMGROUP As String = "CoflexAPIExt/api/ItemsGroup/"

        'Estatus
        Public Const ABIERTA As String = "Abierta"
        Public Const ENVIADA As String = "Enviada"
        Public Const CANCELADA As String = "Cancelada"
        Public Const RECHAZADA As String = "Rechazada"
        Public Const ACEPTADA As String = "Aceptada"

        Public Function doPostRequest(url As String, data As String, Optional ByVal contentType As String = "application/json", Optional token As String = "") As String
            Dim request = createRequest(SERVER_HOST & url, "POST", contentType, token)
            sendData(request, data)
            Return getData(request)
        End Function

        Public Function doGetRequest(url As String, Optional ByVal contentType As String = "application/json", Optional token As String = "") As String
            Dim request = createRequest(SERVER_HOST & url, "GET", contentType, token)
            Return getData(request)
        End Function

        Public Function getVersion(url As String, ByVal token As String) As String
            Dim request = createRequest(SERVER_HOST & url, "GET", "application/json", token)
            Return getData(request).Replace("StndCost", "STNDCOST").Replace("CurrCost", "CURRCOST").Replace("Result", "RESULT")
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

        Public Function doPatchRequest(url As String, data As String, Optional ByVal contentType As String = "application/json", Optional token As String = "") As String
            Dim request = createRequest(SERVER_HOST & url, "PATCH", contentType, token)
            sendData(request, data)
            Return getData(request)
        End Function

        Private Function createRequest(ByVal url As String, ByVal method As String, Optional ByVal contentType As String = "application/json", Optional token As String = "") As WebRequest
            Console.WriteLine(url)
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

                Dim ErrorString As String = ""
                Dim errorJSON As JObject
                Try
                    errorJSON = JObject.Parse(resp)
                Catch e As Exception
                    ErrorString = "{
                      'Message': 'Ocurrió un error en el servidor'
                    }"
                    Return "{" _
                    & """statusCode"":" & StatusCode & "," _
                    & """errorMessage"":""Ocurrió un error en el servidor""," _
                    & """detail"":" & ErrorString _
                    & "}"
                End Try



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