Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Dynamic
Imports System.Dynamic.ExpandoObject




Public Class Acceso
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub LogIn(sender As Object, e As EventArgs)

        Dim Consulta = CoflexWebServices.doPostRequest(CoflexWebServices.LOGIN, "grant_type=password&username=" & Me.UserName.Text & "&password=" & Me.Password.Text)
        ''Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.ROLES)
        Dim o = JObject.Parse(Consulta)
        Dim statusCode = o.GetValue("statusCode").Value(Of Integer)

        If (statusCode >= 200 And statusCode < 400) Then

            Dim detail = o.GetValue("detail").Value(Of JObject)
            Dim accessToken = detail.GetValue("access_token").Value(Of String)
            Session("access_token") = accessToken
            Session("expire") = False
            Session("roles") = detail.GetValue("Roles").Value(Of String)
            Response.Redirect("~/Default.aspx")
        Else
            Session.Abandon()
            Session("expire") = True
            Dim errorMessage = o.GetValue("errorMessage").Value(Of String)
            Me.FailureText.Text = errorMessage
        End If


        MsgBox(Session("access_token"))

        ''Dim jsonString = "{""Name"":""Aghilas"",""Company"":""....."",""Entered"":""2012-03-16T00:03:33.245-10:00""}"

        ''Dim jsona As Dynamic = JValue.Parse(jsonString)

        ' values require casting
        'Dim name As String = JValue.Parse(jsonString)
        'Dim company As String = jsona.Company
        'Dim entered As DateTime = json.Entered
        'MsgBox(company)

    End Sub
End Class

Public Class LogInUser

    Public Property Name() As String
        Get
            Return m_Username
        End Get
        Set
            m_Username = Value
        End Set
    End Property
    Private m_Username As String

    Public Property Company() As String
        Get
            Return m_Email
        End Get
        Set
            m_Email = Value
        End Set
    End Property
    Private m_Email As String

    Public Property Entered() As [DateTime]
        Get
            Return m_BirthDate
        End Get
        Set
            m_BirthDate = Value
        End Set
    End Property
    Private m_BirthDate As [DateTime]

End Class