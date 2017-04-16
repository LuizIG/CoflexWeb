
Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class ResumenEstimaciones
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim Response = CoflexWebServices.doGetRequest(CoflexWebServices.QUOTATIONS)
            Dim o = JObject.Parse(Response)
            Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                Dim detail = o.GetValue("detail").Value(Of JArray)

                Dim arrayLimpio = New JArray

                For Each Quotation As JObject In detail
                    Quotation.Remove("QuotationVersions")
                    arrayLimpio.Add(Quotation)
                Next

                Dim Table As DataTable = JsonConvert.DeserializeObject(Of DataTable)(arrayLimpio.ToString)


                For x As Integer = 0 To Table.Rows.Count - 1
                    Dim innerI As New TreeNode()
                    innerI.Value = Table.Rows(x)("Id")
                    innerI.Text = Table.Rows(x)("ClientName")
                    TreeViewQuotation.Nodes.Add(innerI)

                    Dim ResponseVersion = CoflexWebServices.doGetRequest(CoflexWebServices.QUOTATIONS_VERSION & "?QuotationsId=" & Table.Rows(x)("Id").ToString)
                    Dim oVersion = JObject.Parse(ResponseVersion)
                    Dim statusCodeVersion = oVersion.GetValue("statusCode").Value(Of Integer)
                    If (statusCodeVersion >= 200 And statusCodeVersion < 400) Then

                        Dim detailVersion = oVersion.GetValue("detail").Value(Of JArray)

                        Dim arrayLimpioVersion = New JArray

                        For Each QuotationVersion As JObject In detailVersion
                            QuotationVersion.Remove("Items")
                            arrayLimpioVersion.Add(QuotationVersion)
                        Next


                        Dim TableVersion As DataTable = JsonConvert.DeserializeObject(Of DataTable)(arrayLimpioVersion.ToString)

                        For y As Integer = 0 To TableVersion.Rows.Count - 1
                            Dim innerIV As New TreeNode()
                            innerIV.Value = TableVersion.Rows(y)("Id")
                            innerIV.Text = "Version " & TableVersion.Rows(y)("VersionNumber")
                            TreeViewQuotation.Nodes(x).ChildNodes.Add(innerIV)
                        Next

                    End If

                Next

            End If
        End If
    End Sub

    Private Sub TreeViewQuotation_SelectedNodeChanged(sender As Object, e As EventArgs) Handles TreeViewQuotation.SelectedNodeChanged
        Dim scTreeView = TreeViewQuotation.SelectedNode

        If scTreeView.Parent IsNot Nothing Then

            Dim idQuotation = scTreeView.Parent.Value
            Dim idQuotationVersion = scTreeView.Value


            Response.Redirect("Estimacion.aspx?q=" & idQuotation & "&v=" & idQuotationVersion)

        End If


    End Sub
End Class