Partial Public Class Account_Login
    Inherits Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'RegisterHyperLink.NavigateUrl = "Register"
        'OpenAuthLogin.ReturnUrl = Request.QueryString("ReturnUrl")
        Dim returnUrl = HttpUtility.UrlEncode(Request.QueryString("ReturnUrl"))
        'If Not [String].IsNullOrEmpty(returnUrl) Then
        '    RegisterHyperLink.NavigateUrl += "?ReturnUrl=" & returnUrl
        'End If
    End Sub

    Protected Sub LogIn(sender As Object, e As EventArgs)

        'Dim url As String = "http://62.151.178.139/coflexAPI/Token"
        'Dim SyncClient As New WebClient()

        'Dim request As String = "grant_type=password&username=" & Me.UserName.Text & "&password=" & Me.Password.Text
        'MsgBox(request)
        'Dim response = ServiciosCoflex.retrieveDataLOGIN(Encoding.ASCII.GetBytes(request))


        'MsgBox(response)


    End Sub
End Class
