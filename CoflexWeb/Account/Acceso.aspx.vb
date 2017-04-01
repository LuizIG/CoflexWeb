Imports CoflexWeb.CoflexWeb.Services.Wev
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Dynamic
Imports System.Dynamic.ExpandoObject




Public Class Acceso
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub LogIn(sender As Object, e As EventArgs)
        MsgBox(Me.UserName.Text)

        Dim response = CoflexWebServices.doPostRequest(CoflexWebServices.LOGIN, "grant_type=password&username=luis.ibarra0992@gmail.com&password=a")

        ''Dim jsonString = "{""Name"":""Aghilas"",""Company"":""....."",""Entered"":""2012-03-16T00:03:33.245-10:00""}"

        ''Dim jsona As Dynamic = JValue.Parse(jsonString)

        ' values require casting
        'Dim name As String = JValue.Parse(jsonString)
        'Dim company As String = jsona.Company
        'Dim entered As DateTime = json.Entered
        'MsgBox(company)

        Dim json As String = "{""Name"":""Aghilas"",""Company"":""....."",""Entered"":""2012-03-16T00:03:33.245-10:00""}"

        Dim m As LogInUser = JsonConvert.DeserializeObject(Of LogInUser)(json)

        Dim name As String = m.Name

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