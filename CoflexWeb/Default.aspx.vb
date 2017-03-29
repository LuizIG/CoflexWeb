Imports System.Web.Script.Serialization
Imports CoflexWeb.CoflexWeb.Services.Wev

Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load


        Dim response = CoflexWebServices.doPostRequest(CoflexWebServices.LOGIN, "grant_type=password&username=luis.ibarra0992@gmail.com&password=a")

        Me.response.InnerText = response

        Dim serializer As New JavaScriptSerializer
        Dim jsonMsg = serializer.Serialize(response)


    End Sub
End Class

'{"access_token":"_hfjLn9OlWoHDh8pMrLLcpBLBdBcUdKUTTqURx_T8LuE7ux6copegU6V7uz-oqs-5Va7U-s3CWtpBtN0ttij_lxN5mIz_PCpm_MwjpHZPCjqHD6yhcJiGuCKvhOohZqa5LTkqoqnl86BQX9BAJcC1Ymt6relKLAp66vsGIaGBN4RFyGAbVMy6Zw31HvqyGTXHeuafqJmtJ9AtJ69dis47bz9bGI7Fc61fwMfOpqjksxJPb8U3hk3eSa5aqyui3Qdkapb3m8VM_ngkhJRjalgU3Qm2VJ_1-BIL3jF50GZeuSEfjpacXbHOLcPPYU31bmw9pY6qiJbd9b0Uka8dnpAxSpXKSq3mQ1FFUsgp1OTRrsXk49kCTSLNxrrSpGWkkbNiJNQGdQ02xofEpdvx28n_xwnxrCITesxSeWUYQ8byug39F5iTvCTlEWecUDl3Rg7GmkPW1zbwg5U4V06mFinfYjppH-LoSkfsKx6pqeEoCXYLXLO_oOsQ5fm6CmfsUHu","token_type":"bearer","expires_in":1209599,"userName":"luis.ibarra0992@gmail.com",".issued":"Wed, 29 Mar 2017 00:54:35 GMT",".expires":"Wed, 12 Apr 2017 00:54:35 GMT"}