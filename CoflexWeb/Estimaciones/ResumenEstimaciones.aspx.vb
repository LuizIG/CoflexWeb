
Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class ResumenEstimaciones
    Inherits CoflexWebPage
    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsPostBack Then
            MyBase.Page_Load(sender, e)
            Dim Response = CoflexWebServices.doGetRequest(CoflexWebServices.QUOTATIONS, , Session("access_token"))
            Dim o = JObject.Parse(Response)
            Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                Dim detail = o.GetValue("detail").Value(Of JArray)
                Dim arrayLimpio = New JArray
                For Each Quotation As JObject In detail
                    Dim Client = Quotation.GetValue("AspNetUsersView").Value(Of JObject)
                    Dim UserName As String = Client.GetValue("Name").Value(Of String) & " " & Client.GetValue("PaternalSurname").Value(Of String) & " " & Client.GetValue("MaternalSurname").Value(Of String)
                    Dim Versions = Quotation.GetValue("QuotationVersions").Value(Of JArray)
                    For Each Version As JObject In Versions
                        Version.Remove("Items")
                        Version.Add("ClientName", Quotation.GetValue("ClientName").Value(Of String))
                        Select Case Quotation.GetValue("Status").Value(Of Integer)
                            Case 0
                                Version.Add("QStatus", "Abierta")
                            Case 1
                                Version.Add("QStatus", "Cerrada")
                            Case 2
                                Version.Add("QStatus", "Cancelada")
                        End Select
                        Version.Add("User", UserName)
                        Version.Add("ActionEdit", "<a href='Estimacion.aspx?q=" & Version.GetValue("QuotationsId").Value(Of Integer) & "&v=" & Version.GetValue("Id").Value(Of Integer) & "' class='btn btn-primary' role='button'>Editar</a>")
                        arrayLimpio.Add(Version)
                    Next
                Next
                Dim Table As DataTable = JsonConvert.DeserializeObject(Of DataTable)(arrayLimpio.ToString)
                GridUsers.DataSource = Table
                GridUsers.DataBind()
            End If
        End If
    End Sub

    Private Sub GridUsers_DataBound(sender As Object, e As EventArgs) Handles GridUsers.DataBound
        For i As Integer = GridUsers.Rows.Count - 1 To 1 Step -1
            Dim row As GridViewRow = GridUsers.Rows(i)
            Dim previousRow As GridViewRow = GridUsers.Rows(i - 1)
            For j As Integer = 0 To row.Cells.Count - 1
                If row.Cells(j).Text = previousRow.Cells(j).Text And row.Cells(0).Text = previousRow.Cells(0).Text Then
                    If previousRow.Cells(j).RowSpan = 0 Then
                        If row.Cells(j).RowSpan = 0 Then
                            previousRow.Cells(j).RowSpan += 2
                        Else
                            previousRow.Cells(j).RowSpan = row.Cells(j).RowSpan + 1
                        End If
                        row.Cells(j).Visible = False
                    End If
                End If
            Next
        Next
    End Sub
End Class