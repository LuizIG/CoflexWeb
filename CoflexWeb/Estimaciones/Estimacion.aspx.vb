Imports System.Data.SqlClient
Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Estimacion
    Inherits CoflexWebPage

    Private data As New DataSet

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

            Session.Remove("treeView")

            '    If (Session("tables") Is Nothing) Then
            '        data = New DataSet
            '    Else
            '        data = Session("tables")
            '    End If
        End If
    End Sub

    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If Session("treeView") IsNot Nothing Then
            Me.MultiView1.ActiveViewIndex = 1

            Dim dv As New DataView(DirectCast(Session("treeView"), DataTable))
            dv.RowFilter = "Nivel1 = 0 and Nivel2 = 0 and Nivel3 = 0"

            Me.GridView1.DataSource = dv.ToTable
            Me.GridView1.DataBind()
        End If
    End Sub

    Protected Sub Regresar_Click(sender As Object, e As EventArgs) Handles Regresar.Click
        Me.MultiView1.ActiveViewIndex = 0

    End Sub

    Protected Function ExistProduct(ByVal CBitem As String)
        For Each myNode In Me.TreeView1.Nodes
            If Split(myNode.value, "|")(2) = CBitem Then
                Return False
            End If
        Next
        Return True
    End Function

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If ExistProduct(Me.DDArticulo.SelectedValue) Then


            Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.ITEM_COMPONENTS & "/" & Me.DDArticulo.SelectedValue)
            Dim o = JObject.Parse(jsonResponse)
            Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                Dim detail = o.GetValue("detail").Value(Of JArray)
                Dim Table As DataTable = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)

                treeViewTable(Table)

                For Each reng As DataRow In Table.Rows
                    If reng("Nivel1") = 0 And reng("Nivel2") = 0 And reng("Nivel3") = 0 Then
                        Dim innerI As New TreeNode()
                        innerI.Value = reng("Id") & "|" & reng("Nivel1") & "|" & reng("SkuComponente")
                        innerI.Text = reng("SkuComponente") & " : " & reng("ITEMDESC")
                        TreeView1.Nodes.Add(innerI)
                    End If
                Next
                'Dim sqlDR As SqlDataAdapter
                'sqlDR.Fill(Table)
                For Each reng As DataRow In Table.Rows
                    If reng("Nivel1") > 0 And reng("Nivel2") = 0 And reng("Nivel3") = 0 Then
                        Dim inner As New TreeNode()
                        inner.Value = reng("Id") & "|" & reng("Nivel1") & "|" & reng("SkuComponente")
                        inner.Text = reng("SkuComponente") & " : " & reng("ITEMDESC")
                        ''TreeView1.Nodes.Add(inner)
                        If TreeView1.Nodes.Count > 1 Then
                            TreeView1.Nodes(TreeView1.Nodes.Count - 1).ChildNodes.Add(inner)
                        Else
                            TreeView1.Nodes(0).ChildNodes.Add(inner)
                        End If
                    End If
                Next

                Dim myNode As TreeNode = TreeView1.Nodes(TreeView1.Nodes.Count - 1)
                ''For Each myNode In Me.TreeView1.Nodes
                For Each childNodeA As TreeNode In myNode.ChildNodes
                    Dim dv As New DataView(Table)
                    dv.RowFilter = "Nivel1 = " & Split(childNodeA.Value, "|")(1)
                    For Each reng As DataRowView In dv
                        If reng("Nivel1") > 0 And reng("Nivel2") > 0 And reng("Nivel3") = 0 Then
                            Dim inner As New TreeNode()
                            inner.Value = reng("Id") & "|" & reng("Nivel2") & "|" & reng("SkuComponente")
                            inner.Text = reng("SkuComponente") & " : " & reng("ITEMDESC")
                            childNodeA.ChildNodes.Add(inner)
                        End If
                    Next
                Next
                '' Next

                ''For Each myNode In Me.TreeView1.Nodes
                For Each childNodeA As TreeNode In myNode.ChildNodes
                    For Each childNodeB As TreeNode In childNodeA.ChildNodes
                        Dim dv As New DataView(Table)
                        dv.RowFilter = "Nivel1 = " & Split(childNodeA.Value, "|")(1) & " and Nivel2 = " & Split(childNodeB.Value, "|")(1)
                        For Each reng As DataRowView In dv
                            If reng("Nivel1") > 0 And reng("Nivel2") > 0 And reng("Nivel3") > 0 Then
                                Dim inner As New TreeNode()
                                inner.Value = reng("Id") & "|" & reng("Nivel3") & "|" & reng("SkuComponente")
                                inner.Text = reng("SkuComponente") & " : " & reng("ITEMDESC")
                                childNodeB.ChildNodes.Add(inner)
                            End If
                        Next
                    Next
                Next
            End If

        End If
    End Sub

    Protected Sub treeViewTable(ByVal fDataTable As DataTable)
        If Session("treeView") Is Nothing Then
            Session("treeView") = fDataTable
        Else
            ''Dim dt1 As DataTable = DirectCast(Session("treeView"), DataTable)

            DirectCast(Session("treeView"), DataTable).Merge(fDataTable)

            ''Dim comparer = New CustomComparer()
            'Dim dtUnion As DataTable = dt1.AsEnumerable().Union(fDataTable.AsEnumerable(), comparer).CopyToDataTable(Of DataRow)()

            ''Dim dt1 = New DataTable()
            ' Replace with Dt1
            ''Dim dt2 = New DataTable()
            ' Replace with Dt2
            ''Dim result As DataTable = dt1.AsEnumerable().Union(fDataTable.AsEnumerable()).OrderBy(Function(d) d.Field(Of String)("table"))
        End If
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ExistProduct(Me.DDComponente.SelectedValue) Then

            Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.DETAIL_COMPONENTS & "/" & Me.DDComponente.SelectedValue)
            Dim o = JObject.Parse(jsonResponse)
            Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                Dim detail = o.GetValue("detail").Value(Of JArray)
                ''Dim Table As DataTable = JsonConvert.DeserializeObject(Of DataTable)("[" & detail.ToString & "]")
                Dim Table As DataTable = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)


                Dim countIndex As Integer = 0
                Dim myNode As TreeNode

                If TreeView1.CheckedNodes.Count > 0 Then
                    If TreeView1.Nodes.Count > 0 Then
                        For Each myNode In Me.TreeView1.Nodes
                            If myNode.ChildNodes.Count > 0 Then
                                For Each childNodeA As TreeNode In myNode.ChildNodes
                                    If childNodeA.Checked Then
                                        Dim inner As New TreeNode()
                                        inner.Value = Table.Rows(0)("Id").ToString() & "|" & Table.Rows(0)("Nivel1") & "|" & Table.Rows(0)("SkuComponente")
                                        ''DDComponente.SelectedValue.ToString()
                                        inner.Text = Table.Rows(0)("SkuComponente") & " : " & Table.Rows(0)("ITEMDESC")
                                        ''DDComponente.SelectedValue.ToString()
                                        childNodeA.ChildNodes.Add(inner)
                                    Else
                                        If childNodeA.ChildNodes.Count > 0 Then
                                            For Each childNodeB As TreeNode In childNodeA.ChildNodes
                                                If childNodeB.Checked Then
                                                    Dim innerB As New TreeNode()
                                                    innerB.Value = Table.Rows(0)("Id").ToString() & "|" & Table.Rows(0)("Nivel1") & "|" & Table.Rows(0)("SkuComponente")
                                                    ''DDComponente.SelectedValue.ToString()
                                                    innerB.Text = Table.Rows(0)("SkuComponente") & " : " & Table.Rows(0)("ITEMDESC")
                                                    ''DDComponente.SelectedValue.ToString()
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
                                inner.Value = Table.Rows(0)("Id").ToString() & "|" & Table.Rows(0)("Nivel1") & "|" & Table.Rows(0)("SkuComponente")
                                ''DDComponente.SelectedValue.ToString()
                                inner.Text = Table.Rows(0)("SkuComponente") & " : " & Table.Rows(0)("ITEMDESC")
                                ''DDComponente.SelectedValue.ToString()
                                myNode.ChildNodes.Add(inner)

                            End If
                        Next myNode
                    End If
                Else

                    Dim inner As New TreeNode()
                    ''inner.Value = DDComponente.SelectedValue.ToString
                    inner.Value = Table.Rows(0)("Id").ToString() & "|" & Table.Rows(0)("Nivel1") & "|" & Table.Rows(0)("SkuComponente")
                    ''inner.Text = DDComponente.SelectedValue.ToString
                    inner.Text = Table.Rows(0)("SkuComponente") & " : " & Table.Rows(0)("ITEMDESC")
                    TreeView1.Nodes.Add(inner)

                    treeViewTable(Table)
                End If
            End If


        End If

    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TreeView1.CheckedNodes.Count > 0 Then
            If TreeView1.Nodes.Count > 0 Then
                For z = 0 To Me.TreeView1.Nodes.Count
                    If z >= TreeView1.Nodes.Count Then
                        Exit For
                    End If
                    Dim myNode = TreeView1.Nodes(z)

                    'Next
                    'For Each myNode As TreeNode In Me.TreeView1.Nodes
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
                        ''                        myNode.ChildNodes.Remove(myNode)
                        TreeView1.Nodes.Remove(myNode)
                        z = z - 1
                    End If
                Next

            End If
        Else
            TreeView1.Nodes.Clear()
        End If


        For Each Node In TreeView1.CheckedNodes

        Next




    End Sub

    Private Sub TreeView1_SelectedNodeChanged(sender As Object, e As EventArgs) Handles TreeView1.SelectedNodeChanged

        Dim scTreeView = TreeView1.SelectedNode.Value
        Dim Table As DataTable = DirectCast(Session("treeView"), DataTable)
        Dim dv As New DataView(Table)
        dv.RowFilter = "Id = " & Split(scTreeView, "|")(0) & " and SkuComponente = '" & Split(scTreeView, "|")(2) & "'"

        For Each reng As DataRowView In dv
            Me.TextBox1.Text = reng("SkuArticulo")
            Me.TextBox2.Text = reng("SkuComponente")
            Me.TextArea1.Value = reng("ITEMDESC")
            Me.TextBox3.Text = reng("QUANTITY_I")
            Me.TextBox4.Text = reng("UOFM")
            Me.TextBox5.Text = reng("CURRCOST")
            Me.TextBox6.Text = reng("RESULT")
        Next




    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim scTreeView = TreeView1.SelectedNode.Value
        Dim Table As DataTable = DirectCast(Session("treeView"), DataTable)
        'Dim dv As New DataView(Table)
        'dv.RowFilter = "Id = " & Split(scTreeView, "|")(0) & " and SkuComponente = '" & Split(scTreeView, "|")(2) & "'"

        For Each dr As DataRow In Table.Rows
            If dr("Id") = Split(scTreeView, "|")(0) And dr("SkuComponente") = Split(scTreeView, "|")(2) Then
                dr("QUANTITY_I") = Me.TextBox3.Text
            End If
        Next

        treeViewTable(Table)

    End Sub
End Class