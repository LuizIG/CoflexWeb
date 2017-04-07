﻿Imports System.Data.SqlClient
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
                ''Me.DDArticulo.DataBind()
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
        Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.ITEMCOMPONENTS & "/" & Me.DDArticulo.SelectedValue)
        Dim o = JObject.Parse(jsonResponse)
        Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
        If (statusCode >= 200 And statusCode < 400) Then
            Dim detail = o.GetValue("detail").Value(Of JArray)
            Dim Table As DataTable = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)


            Dim innerI As New TreeNode()
            innerI.Value = DDArticulo.SelectedValue.ToString
            innerI.Text = DDArticulo.SelectedValue.ToString
            TreeView1.Nodes.Add(innerI)
            'Dim sqlDR As SqlDataAdapter
            'sqlDR.Fill(Table)
            For Each reng As DataRow In Table.Rows
                If reng("Nivel1") > 0 And reng("Nivel2") = 0 And reng("Nivel3") = 0 Then
                    Dim inner As New TreeNode()
                    inner.Value = reng("Nivel1")
                    inner.Text = reng("SkuComponente")
                    TreeView1.Nodes.Add(inner)
                    TreeView1.Nodes(0).ChildNodes.Add(inner)
                End If
            Next

            Dim myNode As TreeNode
            For Each myNode In Me.TreeView1.Nodes
                For Each childNodeA As TreeNode In myNode.ChildNodes
                    Dim dv As New DataView(Table)
                    dv.RowFilter = "Nivel1 = " & childNodeA.Value
                    For Each reng As DataRowView In dv
                        If reng("Nivel1") > 0 And reng("Nivel2") > 0 And reng("Nivel3") = 0 Then
                            Dim inner As New TreeNode()
                            inner.Value = reng("Nivel2")
                            inner.Text = reng("SkuComponente")
                            childNodeA.ChildNodes.Add(inner)
                        End If
                    Next
                Next
            Next

            For Each myNode In Me.TreeView1.Nodes
                For Each childNodeA As TreeNode In myNode.ChildNodes
                    For Each childNodeB As TreeNode In childNodeA.ChildNodes
                        Dim dv As New DataView(Table)
                        dv.RowFilter = "Nivel1 = " & childNodeA.Value & " and Nivel2 = " & childNodeB.Value
                        For Each reng As DataRowView In dv
                            If reng("Nivel1") > 0 And reng("Nivel2") > 0 And reng("Nivel3") > 0 Then
                                Dim inner As New TreeNode()
                                inner.Value = reng("Nivel3")
                                inner.Text = reng("SkuComponente")
                                childNodeB.ChildNodes.Add(inner)
                            End If
                        Next
                    Next
                Next
            Next


        End If
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
                        For a = 0 To myNode.ChildNodes.Count
                            If a >= myNode.ChildNodes.Count Then
                                Exit For
                            End If
                            Dim childNodeA = myNode.ChildNodes(a)
                            'Next
                            'For Each childNodeA As TreeNode In myNode.ChildNodes
                            If childNodeA.Checked Then
                                myNode.ChildNodes.Remove(childNodeA)
                                a = a - 1
                            Else
                                If childNodeA.ChildNodes.Count > 0 Then
                                    For b = 0 To childNodeA.ChildNodes.Count
                                        If b >= childNodeA.ChildNodes.Count Then
                                            Exit For
                                        End If
                                        Dim childNodeB = childNodeA.ChildNodes(b)
                                        If childNodeB.Checked Then
                                            childNodeA.ChildNodes.Remove(childNodeB)
                                            b = b - 1

                                        Else
                                            If childNodeB.ChildNodes.Count > 0 Then
                                                For c = 0 To childNodeB.ChildNodes.Count
                                                    If c >= childNodeB.ChildNodes.Count Then
                                                        Exit For
                                                    End If
                                                    Dim childNodeC = childNodeB.ChildNodes(c)
                                                    If childNodeC.Checked Then
                                                        childNodeB.ChildNodes.Remove(childNodeC)
                                                        c = c - 1
                                                    End If
                                                Next
                                            End If

                                        End If
                                    Next
                                End If
                            End If
                        Next
                    End If
                    ' Check whether the tree node is checked.
                    If myNode.Checked Then
                        'Dim inner As New TreeNode()
                        'inner.Value = DDComponente.SelectedValue.ToString
                        'inner.Text = DDComponente.SelectedValue.ToString
                        ''myNode.ChildNodes.Remove(inner)
                        TreeView1.Nodes.Remove(myNode)
                    End If
                Next myNode

            End If
        Else
            TreeView1.Nodes.Clear()
        End If


        For Each Node In TreeView1.CheckedNodes

        Next




    End Sub

    Private Sub TreeView1_SelectedNodeChanged(sender As Object, e As EventArgs) Handles TreeView1.SelectedNodeChanged

    End Sub
End Class