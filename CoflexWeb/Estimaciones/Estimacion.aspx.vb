Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Estimacion
    Inherits CoflexWebPage

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsPostBack Then
            Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.ITEM)
            Dim o = JObject.Parse(jsonResponse)
            Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                Dim detail = o.GetValue("detail").Value(Of JArray)
                Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)
                Me.DDArticulo.DataSource = Table
                Me.DDArticulo.DataValueField = "PPN_I"
                Me.DDArticulo.DataTextField = "PPN_I"
                Me.DDArticulo.DataBind()
            End If

            jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.COMPONENT)
            o = JObject.Parse(jsonResponse)
            statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                Dim detail = o.GetValue("detail").Value(Of JArray)
                Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)
                Me.DDComponente.DataSource = Table
                Me.DDComponente.DataValueField = "PPN_I"
                Me.DDComponente.DataTextField = "PPN_I"
                Me.DDComponente.DataBind()
            End If

        End If
    End Sub

    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.MultiView1.ActiveViewIndex = 1
    End Sub

    Protected Sub Regresar_Click(sender As Object, e As EventArgs) Handles Regresar.Click
        Me.MultiView1.ActiveViewIndex = 0

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ''Me.TreeView1.Nodes(0).nodes.Add(Me.TextBox12.Text)
        'Dim newNode As TreeNode = New TreeNode(Me.TextBox12.Text)
        '''Me.TreeView1.SelectedNode.Nodes.Add(newNode)
        'Me.TreeView1.SelectedNode.ChildNodes.Add(newNode)
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Dim newNode As TreeNode = New TreeNode("1-AG-S90-RH : COFLEX GAS ACERO")
        'TreeView1.Nodes.Remove(newNode)
        '' Clears all nodes.
        'TreeView1.Nodes.Clear()
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Dim MyNode As TreeNode = Nothing
        'MyNode.Value = DDComponente.SelectedValue.ToString
        'Me.TreeView1.Nodes.Add(MyNode)

        ''''Dim inner As New TreeNode()
        ''''inner.Value = DDComponente.SelectedValue.ToString
        ''''inner.Text = DDComponente.SelectedValue.ToString
        ''''TreeView1.Nodes.Add(inner)
        '''''TreeView1.Nodes(0).ChildNodes.Add(inner)



        Dim countIndex As Integer = 0
        Dim myNode As TreeNode

        If TreeView1.CheckedNodes.Count > 0 Then
            If TreeView1.Nodes.Count > 0 Then
                For Each myNode In Me.TreeView1.Nodes
                    If myNode.ChildNodes.Count > 0 Then
                        For Each childNodeA As TreeNode In myNode.ChildNodes
                            If childNodeA.Checked Then
                                Dim inner As New TreeNode()
                                inner.Value = DDComponente.SelectedValue.ToString
                                inner.Text = DDComponente.SelectedValue.ToString
                                childNodeA.ChildNodes.Add(inner)
                            Else
                                If childNodeA.ChildNodes.Count > 0 Then
                                    For Each childNodeB As TreeNode In childNodeA.ChildNodes
                                        If childNodeB.Checked Then
                                            Dim innerB As New TreeNode()
                                            innerB.Value = DDComponente.SelectedValue.ToString
                                            innerB.Text = DDComponente.SelectedValue.ToString
                                            childNodeB.ChildNodes.Add(innerB)


                                        End If
                                    Next
                                End If
                            End If
                        Next
                    End If
                    ' Check whether the tree node is checked.
                    If myNode.Checked Then
                        Dim inner As New TreeNode()
                        inner.Value = DDComponente.SelectedValue.ToString
                        inner.Text = DDComponente.SelectedValue.ToString
                        myNode.ChildNodes.Add(inner)
                    Else
                        'Dim inner As New TreeNode()
                        'inner.Value = DDComponente.SelectedValue.ToString
                        'inner.Text = DDComponente.SelectedValue.ToString
                        'TreeView1.Nodes.Add(inner)
                    End If
                Next myNode
                'Else
                '    Dim inner As New TreeNode()
                '    inner.Value = DDComponente.SelectedValue.ToString
                '    inner.Text = DDComponente.SelectedValue.ToString
                '    TreeView1.Nodes.Add(inner)
            End If
        Else
            Dim inner As New TreeNode()
            inner.Value = DDComponente.SelectedValue.ToString
            inner.Text = DDComponente.SelectedValue.ToString
            TreeView1.Nodes.Add(inner)
        End If
    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TreeView1.CheckedNodes.Count > 0 Then
            If TreeView1.Nodes.Count > 0 Then
                For Each myNode As TreeNode In Me.TreeView1.Nodes
                    If myNode.ChildNodes.Count > 0 Then
                        For Each childNodeA As TreeNode In myNode.ChildNodes

                            If childNodeA.Checked Then
                                'Dim inner As New TreeNode()
                                'inner.Value = DDComponente.SelectedValue.ToString
                                'inner.Text = DDComponente.SelectedValue.ToString
                                'childNodeA.ChildNodes.Remove(inner)
                                ''childNodeA.ChildNodes.Remove(childNodeA)
                                myNode.ChildNodes.Remove(childNodeA)
                            Else
                                If childNodeA.ChildNodes.Count > 0 Then
                                    ''For Each childNodeB As TreeNode In childNodeA.ChildNodes

                                    For x = 0 To childNodeA.ChildNodes.Count

                                        Dim childNodeB = childNodeA.ChildNodes(x)
                                        If childNodeB.Checked Then
                                            childNodeA.ChildNodes.Remove(childNodeB)
                                            x = x + 1
                                        End If
                                    Next

                                    '' If childNodeB.Checked Then
                                    'Dim innerB As New TreeNode()
                                    'innerB.Value = DDComponente.SelectedValue.ToString
                                    'innerB.Text = DDComponente.SelectedValue.ToString
                                    'childNodeB.ChildNodes.Remove(innerB)
                                    '' childNodeA.ChildNodes.Remove(childNodeB)




                                    ''childNodeA.ChildNodes.Remove(innerB)
                                    ''End If
                                    ''Next
                                End If
                            End If
                        Next
                    End If
                    ' Check whether the tree node is checked.
                    If myNode.Checked Then
                        Dim inner As New TreeNode()
                        inner.Value = DDComponente.SelectedValue.ToString
                        inner.Text = DDComponente.SelectedValue.ToString
                        myNode.ChildNodes.Remove(inner)

                    Else
                        'Dim inner As New TreeNode()
                        'inner.Value = DDComponente.SelectedValue.ToString
                        'inner.Text = DDComponente.SelectedValue.ToString
                        'TreeView1.Nodes.Add(inner)
                    End If
                Next myNode
                'Else
                '    Dim inner As New TreeNode()
                '    inner.Value = DDComponente.SelectedValue.ToString
                '    inner.Text = DDComponente.SelectedValue.ToString
                '    TreeView1.Nodes.Add(inner)
            End If
            ''TreeView1.Nodes.Clear()
        Else
            ''TreeView1.Nodes.
            TreeView1.Nodes.Clear()
        End If


        For Each Node In TreeView1.CheckedNodes

        Next




    End Sub

    Private Sub TreeView1_SelectedNodeChanged(sender As Object, e As EventArgs) Handles TreeView1.SelectedNodeChanged

    End Sub
End Class