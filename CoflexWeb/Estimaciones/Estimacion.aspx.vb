﻿Imports System.Data.SqlClient
Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Estimacion
    Inherits CoflexWebPage

    Private data As New DataSet

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsPostBack Then
            MyBase.Page_Load(sender, e)
            Session.Remove("treeView")
            Session.Remove("Margin")
            Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.COMPONENT,, Session("access_token"))
            Dim o = JObject.Parse(jsonResponse)
            Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                Dim detail = o.GetValue("detail").Value(Of JArray)
                Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)
                Me.DDComponente.DataSource = Table
                Me.DDComponente.DataValueField = "PPN_I"
                Me.DDComponente.DataTextField = "PPN_I"
                Me.DDComponente.DataBind()
            End If

            Me.DDComponente.Items.Insert(0, "Seleccionar")

            jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.CLIENTS,, Session("access_token"))
            o = JObject.Parse(jsonResponse)
            statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                Dim detail = o.GetValue("detail").Value(Of JArray)
                Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)
                Me.DDCliente.DataSource = Table
                Me.DDCliente.DataValueField = "Id"
                Me.DDCliente.DataTextField = "ClientName"
                Me.DDCliente.DataBind()
            End If

            Me.DDCliente.Items.Insert(0, "Seleccionar")

            jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.NEW_COMPONENTES,, Session("access_token"))
            o = JObject.Parse(jsonResponse)
            statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                Dim detail = o.GetValue("detail").Value(Of JArray)
                Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)
                Me.DDElemento.DataSource = Table
                Me.DDElemento.DataValueField = "Id"
                Me.DDElemento.DataTextField = "SkuComponente"
                Me.DDElemento.DataBind()
            End If

            Me.DDElemento.Items.Insert(0, "Seleccionar")

            Dim VersionId As String = Request.QueryString("v")

            If VersionId IsNot Nothing Then
                Dim ResponseVersions = CoflexWebServices.doGetRequest(CoflexWebServices.QUOTATIONS_VERSION & "/" & VersionId,, Session("access_token"))
                o = JObject.Parse(ResponseVersions)
                statusCode = o.GetValue("statusCode").Value(Of Integer)
                If (statusCode >= 200 And statusCode < 400) Then
                    Dim detail = o.GetValue("detail").Value(Of JObject)

                    Tv_Exchange.Text = detail.GetValue("ExchangeRate").Value(Of String)
                    Dim items = detail.GetValue("Items").Value(Of JArray)

                    Dim Table As DataTable
                    Dim TableMargin As New DataTable

                    TableMargin.Columns.Add("sku", GetType(String))
                    TableMargin.Columns.Add("margin", GetType(Double))

                    For Each item As JObject In items

                        Dim rowMargin = TableMargin.NewRow
                        rowMargin("sku") = item.GetValue("Sku").Value(Of String)
                        rowMargin("margin") = item.GetValue("ProfitMargin").Value(Of Double)
                        TableMargin.Rows.Add(rowMargin)

                        Dim itemComponents = item.GetValue("ItemsComponents").Value(Of JArray)
                        Dim arrayConParent As New JArray
                        For Each itemComponent As JObject In itemComponents
                            itemComponent.Add("SkuArticulo", item.GetValue("Sku").Value(Of String))
                            itemComponent.Add("Parent", "")
                            itemComponent.Remove("ItemsId")
                            arrayConParent.Add(itemComponent)
                        Next

                        Table = JsonConvert.DeserializeObject(Of DataTable)(arrayConParent.ToString)

                        Try

                            Table.Columns("SkuComponent").ColumnName = "SkuComponente"
                            Table.Columns("ItemDescription").ColumnName = "ITEMDESC"
                            Table.Columns("Quantity").ColumnName = "QUANTITY_I"
                            Table.Columns("UM").ColumnName = "UOFM"
                            Table.Columns("Lvl1").ColumnName = "Nivel1"
                            Table.Columns("Lvl2").ColumnName = "Nivel2"
                            Table.Columns("Lvl3").ColumnName = "Nivel3"
                        Catch ex As Exception
                            MsgBox(ex.StackTrace)
                        End Try

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


                    Next

                    Session("Margin") = TableMargin


                End If
            Else

            End If




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

            Dim VersionId As String = Request.QueryString("v")
            If (VersionId IsNot Nothing) Then
                'Ir por el margen

                If Not dv.Table.Columns.Contains("Margin") Then
                    dv.Table.Columns.Add("Margin", GetType(Double))
                End If

                Dim dvMargin As New DataView(DirectCast(Session("Margin"), DataTable))

                For Each row In dv
                    dvMargin.RowFilter = "sku = '" & row("SkuArticulo") & "'"
                    If dvMargin.Count > 0 Then
                        row("Margin") = dvMargin(0)("margin") * 100
                    Else
                        row("Margin") = 0.35 * 100
                    End If
                Next

            Else
                dv.Table.Columns.Add("Margin", GetType(Double))
                For Each row In dv
                    row("Margin") = 0.35 * 100
                Next

            End If

            Me.GridSummary.DataSource = dv.ToTable
            Me.GridSummary.DataBind()
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


            Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.ITEM_COMPONENTS & "/" & Me.DDArticulo.SelectedValue,, Session("access_token"))
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
        Me.DDArticulo.SelectedIndex = 0
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

            Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.DETAIL_COMPONENTS & "/" & Me.DDComponente.SelectedValue,, Session("access_token"))
            Dim o = JObject.Parse(jsonResponse)
            Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                Dim detail = o.GetValue("detail").Value(Of JArray)
                Dim Table As DataTable = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)


                Dim countIndex As Integer = 0
                Dim myNode As TreeNode

                If TreeView1.CheckedNodes.Count > 0 Then
                    If TreeView1.Nodes.Count > 0 Then
                        For Each myNode In Me.TreeView1.Nodes
                            If myNode.ChildNodes.Count > 0 Then
                                For Each childNodeA As TreeNode In myNode.ChildNodes
                                    If childNodeA.Checked Then

                                        For Each dr As DataRow In Table.Rows
                                            If dr("Id") = Table.Rows(0)("Id").ToString() And dr("SkuComponente") = Table.Rows(0)("SkuComponente") Then
                                                dr("SkuArticulo") = Split(myNode.Value, "|")(2)
                                                dr("Nivel1") = Split(childNodeA.Value, "|")(1)

                                                If childNodeA.ChildNodes.Count = 0 Then
                                                    dr("Nivel2") = 1
                                                Else
                                                    Dim dt2 As DataTable = DirectCast(Session("treeView"), DataTable)
                                                    Dim valMax = FindMaxDataTableValue(dt2, "Nivel2") + 1
                                                    dr("Nivel2") = valMax ''childNodeA.ChildNodes.Count + 1
                                                End If
                                            End If
                                        Next

                                        Dim inner As New TreeNode()
                                        inner.Value = Table.Rows(0)("Id").ToString() & "|" & Table.Rows(0)("Nivel2").ToString() & "|" & Table.Rows(0)("SkuComponente")
                                        inner.Text = Table.Rows(0)("SkuComponente") & " : " & Table.Rows(0)("ITEMDESC")
                                        childNodeA.ChildNodes.Add(inner)
                                        treeViewTable(Table)

                                    Else
                                        If childNodeA.ChildNodes.Count > 0 Then
                                            For Each childNodeB As TreeNode In childNodeA.ChildNodes
                                                If childNodeB.Checked Then

                                                    For Each dr As DataRow In Table.Rows
                                                        If dr("Id") = Table.Rows(0)("Id").ToString() And dr("SkuComponente") = Table.Rows(0)("SkuComponente") Then
                                                            dr("SkuArticulo") = Split(myNode.Value, "|")(2)
                                                            dr("Nivel1") = Split(childNodeA.Value, "|")(1)
                                                            dr("Nivel2") = Split(childNodeB.Value, "|")(1)
                                                            If childNodeB.ChildNodes.Count = 0 Then
                                                                dr("Nivel3") = 1
                                                            Else
                                                                Dim dt2 As DataTable = DirectCast(Session("treeView"), DataTable)
                                                                Dim valMax = FindMaxDataTableValue(dt2, "Nivel3") + 1
                                                                dr("Nivel3") = valMax ''dr("Nivel3") = childNodeB.ChildNodes.Count + 1
                                                            End If
                                                        End If
                                                    Next

                                                    Dim innerB As New TreeNode()
                                                    innerB.Value = Table.Rows(0)("Id").ToString() & "|" & Table.Rows(0)("Nivel3") & "|" & Table.Rows(0)("SkuComponente")
                                                    innerB.Text = Table.Rows(0)("SkuComponente") & " : " & Table.Rows(0)("ITEMDESC")
                                                    childNodeB.ChildNodes.Add(innerB)
                                                    treeViewTable(Table)

                                                End If
                                            Next
                                        End If
                                    End If
                                Next
                            End If
                            If myNode.Checked Then

                                For Each dr As DataRow In Table.Rows
                                    If dr("Id") = Table.Rows(0)("Id").ToString() And dr("SkuComponente") = Table.Rows(0)("SkuComponente") Then

                                        dr("SkuArticulo") = Split(myNode.Value, "|")(2)

                                        If myNode.ChildNodes.Count = 0 Then
                                            dr("Nivel1") = 1
                                        Else
                                            Dim dt2 As DataTable = DirectCast(Session("treeView"), DataTable)
                                            Dim valMax = FindMaxDataTableValue(dt2, "Nivel1") + 1
                                            dr("Nivel1") = valMax ''dr("Nivel1") = myNode.ChildNodes.Count + 1
                                        End If
                                    End If
                                Next

                                Dim inner As New TreeNode()
                                inner.Value = Table.Rows(0)("Id").ToString() & "|" & Table.Rows(0)("Nivel1") & "|" & Table.Rows(0)("SkuComponente")
                                inner.Text = Table.Rows(0)("SkuComponente") & " : " & Table.Rows(0)("ITEMDESC")
                                myNode.ChildNodes.Add(inner)
                                treeViewTable(Table)

                            End If
                        Next myNode
                    End If
                Else
                    Dim inner As New TreeNode()
                    inner.Value = Table.Rows(0)("Id").ToString() & "|" & Table.Rows(0)("Nivel1") & "|" & Table.Rows(0)("SkuComponente")
                    inner.Text = Table.Rows(0)("SkuComponente") & " : " & Table.Rows(0)("ITEMDESC")
                    TreeView1.Nodes.Add(inner)
                    treeViewTable(Table)
                End If
            End If


        End If

        Me.DDComponente.SelectedIndex = 0
    End Sub

    Private Function FindMaxDataTableValue(ByVal dt As DataTable, ByVal Nivel As String) As Integer
        Dim currentValue As Integer, maxValue As Integer
        Dim dv As DataView = dt.DefaultView
        For c As Integer = 0 To dt.Columns.Count - 1
            dv.Sort = dt.Columns(c).ColumnName + " DESC"
            currentValue = CInt(dv(0).Item(Nivel))
            If currentValue > maxValue Then maxValue = currentValue
        Next
        Return maxValue
    End Function

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

                                Dim Table As DataTable = DirectCast(Session("treeView"), DataTable)
                                Dim dv As New DataView(Table)
                                dv.RowFilter = "Id = " & Split(childNodeA.Value, "|")(0) & " and SkuComponente = '" & Split(childNodeA.Value, "|")(2) & "'"
                                For Each reng As DataRowView In dv
                                    Dim dt2 As DataTable = DirectCast(Session("treeView"), DataTable)
                                    Dim rows = dt2.[Select]("Nivel1 = " & reng("Nivel1") & " and SkuArticulo = '" & reng("SkuArticulo") & "'")
                                    For Each row In rows
                                        row.Delete()
                                    Next
                                    treeViewTable(dt2)
                                Next


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

                                            Dim Table As DataTable = DirectCast(Session("treeView"), DataTable)
                                            Dim dv As New DataView(Table)
                                            dv.RowFilter = "Id = " & Split(childNodeB.Value, "|")(0) & " and SkuComponente = '" & Split(childNodeB.Value, "|")(2) & "'"
                                            For Each reng As DataRowView In dv
                                                Dim dt2 As DataTable = DirectCast(Session("treeView"), DataTable)
                                                Dim rows = dt2.[Select]("Nivel1 = " & reng("Nivel1") & " and Nivel2 = " & reng("Nivel2") & " and SkuArticulo = '" & reng("SkuArticulo") & "'")
                                                For Each row In rows
                                                    row.Delete()
                                                Next
                                                treeViewTable(dt2)
                                            Next


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

                                                        Dim Table As DataTable = DirectCast(Session("treeView"), DataTable)
                                                        Dim dv As New DataView(Table)
                                                        dv.RowFilter = "Id = " & Split(childNodeC.Value, "|")(0) & " and SkuComponente = '" & Split(childNodeC.Value, "|")(2) & "'"
                                                        For Each reng As DataRowView In dv
                                                            Dim dt2 As DataTable = DirectCast(Session("treeView"), DataTable)
                                                            Dim rows = dt2.[Select]("Nivel1 = " & reng("Nivel1") & " and Nivel2 = " & reng("Nivel2") & " and Nivel3 = " & reng("Nivel3") & " and SkuArticulo = '" & reng("SkuArticulo") & "'")
                                                            For Each row In rows
                                                                row.Delete()
                                                            Next
                                                            treeViewTable(dt2)
                                                        Next


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

                        Dim Table As DataTable = DirectCast(Session("treeView"), DataTable)
                        Dim dv As New DataView(Table)
                        dv.RowFilter = "Id = " & Split(myNode.Value, "|")(0) & " and SkuComponente = '" & Split(myNode.Value, "|")(2) & "'"
                        For Each reng As DataRowView In dv
                            Dim dt2 As DataTable = DirectCast(Session("treeView"), DataTable)
                            Dim rows = dt2.[Select]("Nivel1 = " & reng("Nivel1") & " and SkuArticulo = '" & reng("SkuArticulo") & "'")
                            For Each row In rows
                                row.Delete()
                            Next
                            treeViewTable(dt2)
                        Next
                        TreeView1.Nodes.Remove(myNode)
                        z = z - 1
                    End If
                Next

            End If
        Else
            TreeView1.Nodes.Clear()
        End If


        'For Each Node In TreeView1.CheckedNodes

        'Next




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
            Me.TextBox7.Text = reng("CURRCOST")
            Me.TextBox5.Text = reng("STNDCOST")
            Me.TextBox6.Text = reng("RESULT")
            If reng("RACost") = 1 Then
                Me.RadioButton1.Checked = True
                Me.RadioButton2.Checked = False
            ElseIf reng("RBCost") = 1 Then
                Me.RadioButton2.Checked = True
                Me.RadioButton1.Checked = False
            End If
        Next




    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim scTreeView = TreeView1.SelectedNode.Value
        Dim Table As DataTable = DirectCast(Session("treeView"), DataTable)
        Dim rows = Table.[Select]("id = " & Split(scTreeView, "|")(0) & " and SkuComponente = '" & Split(scTreeView, "|")(2) & "'")

        For Each dr As DataRow In rows
            If dr("Id") = Split(scTreeView, "|")(0) And dr("SkuComponente") = Split(scTreeView, "|")(2) Then
                dr("QUANTITY_I") = Me.TextBox3.Text

                If dr("Nivel1") = 0 Then
                    Dim rows2 = Table.[Select]("SkuArticulo = '" & dr("SkuArticulo") & "'")
                    For Each dr2 As DataRow In rows2
                        If Me.RadioButton1.Checked Then
                            dr2("RACost") = 1
                            dr2("RBCost") = 0
                        ElseIf Me.RadioButton2.Checked Then
                            dr2("RACost") = 0
                            dr2("RBCost") = 1
                        End If
                        dr2("Result") = dr2("QUANTITY_I") * ((dr2("RACost") * dr2("STNDCOST")) + (dr2("RBCost") * dr2("CURRCOST")))
                        Me.TextBox6.Text = dr("Result")
                    Next
                ElseIf dr("Nivel1") > 0 And dr("Nivel2") = 0 And dr("Nivel3") = 0 Then
                    Dim rows2 = Table.[Select]("Nivel1 = " & dr("Nivel1") & " and SkuArticulo = '" & dr("SkuArticulo") & "'")
                    For Each dr2 As DataRow In rows2
                        If Me.RadioButton1.Checked Then
                            dr2("RACost") = 1
                            dr2("RBCost") = 0
                        ElseIf Me.RadioButton2.Checked Then
                            dr2("RACost") = 0
                            dr2("RBCost") = 1
                        End If
                        dr2("Result") = dr2("QUANTITY_I") * ((dr2("RACost") * dr2("STNDCOST")) + (dr2("RBCost") * dr2("CURRCOST")))
                        Me.TextBox6.Text = dr("Result")
                    Next
                ElseIf dr("Nivel1") > 0 And dr("Nivel2") > 0 And dr("Nivel3") = 0 Then
                    Dim rows2 = Table.[Select]("Nivel1 = " & dr("Nivel1") & " and Nivel2 = " & dr("Nivel2") & " and SkuArticulo = '" & dr("SkuArticulo") & "'")
                    For Each dr2 As DataRow In rows2
                        If Me.RadioButton1.Checked Then
                            dr2("RACost") = 1
                            dr2("RBCost") = 0
                        ElseIf Me.RadioButton2.Checked Then
                            dr2("RACost") = 0
                            dr2("RBCost") = 1
                        End If
                        dr2("Result") = dr2("QUANTITY_I") * ((dr2("RACost") * dr2("STNDCOST")) + (dr2("RBCost") * dr2("CURRCOST")))
                        Me.TextBox6.Text = dr("Result")
                    Next
                ElseIf dr("Nivel1") > 0 And dr("Nivel2") > 0 And dr("Nivel3") > 0 Then
                    Dim rows2 = Table.[Select]("Nivel1 = " & dr("Nivel1") & " and Nivel2 = " & dr("Nivel2") & " and Nivel3 = " & dr("Nivel3") & " and SkuArticulo = '" & dr("SkuArticulo") & "'")
                    For Each dr2 As DataRow In rows2
                        If Me.RadioButton1.Checked Then
                            dr2("RACost") = 1
                            dr2("RBCost") = 0
                        ElseIf Me.RadioButton2.Checked Then
                            dr2("RACost") = 0
                            dr2("RBCost") = 1
                        End If
                        dr2("Result") = dr2("QUANTITY_I") * ((dr2("RACost") * dr2("STNDCOST")) + (dr2("RBCost") * dr2("CURRCOST")))
                        Me.TextBox6.Text = dr("Result")
                    Next
                End If

            End If

        Next

        Montos(Table)
        treeViewTable(Table)

    End Sub

    Private Sub Montos(ByVal fDataTable As DataTable)
        Dim scTreeView = TreeView1.SelectedNode.Value
        Dim Table As DataTable = DirectCast(Session("treeView"), DataTable)
        Dim rows = Table.[Select]("id = " & Split(scTreeView, "|")(0) & " and SkuComponente = '" & Split(scTreeView, "|")(2) & "'")

        For Each dr As DataRow In rows
            If dr("Nivel1") > 0 And dr("Nivel2") > 0 And dr("Nivel3") > 0 Then
                Dim rows2 = Table.[Select]("Nivel1 = " & dr("Nivel1") & " and Nivel2 = " & dr("Nivel2") & " and SkuArticulo = '" & dr("SkuArticulo") & "'")
                For Each dr2 As DataRow In rows2

                Next

            End If
        Next

    End Sub

    Private Sub Suma()

    End Sub

    Private Sub Versionar_Click(sender As Object, e As EventArgs) Handles Versionar.Click

        'Si no existe la cotizacion, crea una nueva

        Dim IdQuotaion As String = Request.QueryString("q")
        If IdQuotaion Is Nothing Then
            Dim ResponseServer = doPostRequest(QUOTATIONS, CreateQuotation.ToString,, Session("access_token"))
            Console.Write(Response)
            Dim o = JObject.Parse(ResponseServer)
            Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then

                Dim QuotationCreated = o.GetValue("detail").Value(Of JObject)

                Dim QuotationVersionCreated = QuotationCreated.GetValue("QuotationVersions").Value(Of JArray)

                Dim version As JObject = QuotationVersionCreated(0)

                Dim quotationID As String = version.GetValue("QuotationsId").Value(Of Integer) & ""
                Dim versionID As String = version.GetValue("Id").Value(Of Integer) & ""

                Response.Redirect("Estimacion?q=" & quotationID & "&v=" & versionID)

            Else

            End If
        Else
            Dim Response = doPostRequest(QUOTATIONS_VERSION, CreateQuotationVersion(IdQuotaion).ToString,, Session("access_token"))
            Console.Write(Response)
        End If

        'Si no existe la version, crea una nueva
        If Request.QueryString("v") Is Nothing Then

        End If
    End Sub

    Private Sub Guardar_Click(sender As Object, e As EventArgs) Handles Guardar.Click
        Dim IdQuotaionVersion As String = Request.QueryString("v")
        If IdQuotaionVersion IsNot Nothing Then

            Dim Response = doPutRequest(QUOTATIONS_VERSION & "/" & IdQuotaionVersion, CreateQuotationVersion(IdQuotaionVersion).ToString)
            Dim o = JObject.Parse(Response)
            Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then

                Dim QuotationCreated = o.GetValue("detail").Value(Of JObject)

            Else

            End If
            Console.Write(Response)
        Else

        End If
    End Sub

    Private Function CreateQuotation() As JObject
        Dim Quotation As New JObject
        Quotation.Add("ClientId", DDCliente.SelectedValue)
        Quotation.Add("ClientName", DDCliente.SelectedItem.Text)
        Quotation.Add("QuotationVersions", CreateQuotationVersion())
        Return Quotation
    End Function

    Private Function CreateQuotationVersion() As JObject
        Dim QuotationVersion As New JObject

        Dim exchange As Double = CDbl(Val(Tv_Exchange.Text))

        QuotationVersion.Add("ExchangeRate", exchange)
        QuotationVersion.Add("UseStndCost", True)
        QuotationVersion.Add("ItemsBindingModel", CreateItems())
        Return QuotationVersion
    End Function

    Private Function CreateQuotationVersion(ByVal idQuotation As String) As JObject
        Dim QuotationVersionId = CreateQuotationVersion()
        QuotationVersionId.Add("IdQuotaions", CInt(idQuotation))
        Return QuotationVersionId
    End Function

    Private Function CreateItems() As JArray
        Dim ItemArray As New JArray
        Dim dv As New DataView(DirectCast(Session("treeView"), DataTable))

        Dim dvNew As New DataView(DirectCast(GridSummary.DataSource, DataTable))

        dv.RowFilter = "Nivel1 = 0 and Nivel2 = 0 and Nivel3 = 0"
        For Each reng As DataRowView In dv
            Dim Item As New JObject
            Dim sku As String = reng("SkuArticulo")

            Dim row

            For Each gridRow In GridSummary.Rows

                If gridRow.Cells(0).Text = sku Then
                    row = gridRow
                End If

            Next

            Dim TBCantidad As TextBox = TryCast(row.FindControl("TBQuantity"), TextBox)
            Dim TBMargin As TextBox = TryCast(row.FindControl("TVMargin"), TextBox)

            Item.Add("Sku", sku)
            Item.Add("ItemDescription", reng("ITEMDESC").ToString)
            Item.Add("Quantity", CDbl(TBCantidad.Text))
            Item.Add("UM", reng("UOFM").ToString)
            Item.Add("Status", 0)
            Item.Add("ProfitMargin", CDbl(TBMargin.Text) / 100)
            Item.Add("ItemsComponents", CreateItemComponents(sku, TBCantidad.Text))
            ItemArray.Add(Item)
        Next
        Return ItemArray
    End Function

    Private Function CreateItemComponents(ByVal itemSku As String, ByVal cant As String) As JArray
        Dim ItemComponentsArray As New JArray
        Dim dv As New DataView(DirectCast(Session("treeView"), DataTable))
        dv.RowFilter = "SkuArticulo = '" & itemSku & "'"
        For Each reng As DataRowView In dv
            Dim Item As New JObject

            If CInt(reng("Nivel1").ToString) = 0 And CInt(reng("Nivel2").ToString) = 0 And CInt(reng("Nivel3").ToString) = 0 Then
                Item.Add("Quantity", CDbl(cant))
            Else
                Item.Add("Quantity", CDbl(reng("QUANTITY_I").ToString))
            End If

            Item.Add("SkuComponent", reng("SkuComponente").ToString)
            Item.Add("ItemDescription", reng("ITEMDESC").ToString)

            Item.Add("UM", reng("UOFM").ToString)
            Item.Add("StndCost", CDbl(IIf(reng("STNDCOST").ToString Is Nothing Or reng("STNDCOST").ToString = "", "0", reng("STNDCOST").ToString)))
            Item.Add("CurrCost", CDbl(IIf(reng("CURRCOST").ToString Is Nothing Or reng("STNDCOST").ToString = "", "0", reng("CURRCOST").ToString)))
            Item.Add("Result", CDbl(IIf(reng("RESULT").ToString Is Nothing Or reng("STNDCOST").ToString = "", "0", reng("RESULT").ToString)))
            Item.Add("Lvl1", CInt(reng("Nivel1").ToString))
            Item.Add("Lvl2", CInt(reng("Nivel2").ToString))
            Item.Add("Lvl3", CInt(reng("Nivel3").ToString))
            Item.Add("RACost", CInt(reng("RACost").ToString))
            Item.Add("RBCost", CInt(reng("RBCost").ToString))
            Item.Add("FinalCost", CDbl(reng("FinalCost").ToString))
            ItemComponentsArray.Add(Item)
        Next
        Return ItemComponentsArray
    End Function

    Protected Sub Cotizar_Click(sender As Object, e As EventArgs) Handles Cotizar.Click

    End Sub

    Protected Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Me.MultiView1.ActiveViewIndex = 0
    End Sub

    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim Elemento As New JObject

        With Elemento
            .Add("SkuComponente", Me.txtSkuComponente.Text)
            .Add("IdQuotation", 14)
            .Add("Uofm", Me.txtUofm.Text)
            .Add("ItemDesc", Me.txtItemDesc.Value)
            .Add("StndCost", Me.txtStndCost.Text)
        End With

        Dim jsonResponse = CoflexWebServices.doPostRequest(CoflexWebServices.NEW_COMPONENTES, Elemento.ToString,, Session("access_token"))
        Dim o = JObject.Parse(jsonResponse)
        Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
        If (statusCode >= 200 And statusCode < 400) Then
            '' Me.ErrorMessage.Text = "Usuario Registrado"
        Else
            Dim errorMessage = o.GetValue("errorMessage").Value(Of String)
            ''Me.ErrorMessage.Text = errorMessage
        End If

        Me.DDElemento.Dispose()

        jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.NEW_COMPONENTES,, Session("access_token"))
        o = JObject.Parse(jsonResponse)
        statusCode = o.GetValue("statusCode").Value(Of Integer)
        If (statusCode >= 200 And statusCode < 400) Then
            Dim detail = o.GetValue("detail").Value(Of JArray)
            Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)
            Me.DDElemento.DataSource = Table
            Me.DDElemento.DataValueField = "Id"
            Me.DDElemento.DataTextField = "SkuComponente"
            Me.DDElemento.DataBind()
        End If

        Me.DDElemento.Items.Insert(0, "Seleccionar")

        Me.MultiView1.ActiveViewIndex = 0

    End Sub

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If ExistProduct(Me.DDElemento.SelectedValue) Then

            Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.NEW_COMPONENTES & "/" & Me.DDElemento.SelectedValue,, Session("access_token"))
            Dim o = JObject.Parse(jsonResponse)
            Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                Dim detail = o.GetValue("detail").Value(Of JArray)
                Dim Table As DataTable = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)


                Dim countIndex As Integer = 0
                Dim myNode As TreeNode

                If TreeView1.CheckedNodes.Count > 0 Then
                    If TreeView1.Nodes.Count > 0 Then
                        For Each myNode In Me.TreeView1.Nodes
                            If myNode.ChildNodes.Count > 0 Then
                                For Each childNodeA As TreeNode In myNode.ChildNodes
                                    If childNodeA.Checked Then

                                        For Each dr As DataRow In Table.Rows
                                            If dr("Id") = Table.Rows(0)("Id").ToString() And dr("SkuComponente") = Table.Rows(0)("SkuComponente") Then
                                                dr("SkuArticulo") = Split(myNode.Value, "|")(2)
                                                dr("Nivel1") = Split(childNodeA.Value, "|")(1)

                                                If childNodeA.ChildNodes.Count = 0 Then
                                                    dr("Nivel2") = 1
                                                Else
                                                    Dim dt2 As DataTable = DirectCast(Session("treeView"), DataTable)
                                                    Dim valMax = FindMaxDataTableValue(dt2, "Nivel2") + 1
                                                    dr("Nivel2") = valMax ''childNodeA.ChildNodes.Count + 1
                                                End If
                                            End If
                                        Next

                                        Dim inner As New TreeNode()
                                        inner.Value = Table.Rows(0)("Id").ToString() & "|" & Table.Rows(0)("Nivel2").ToString() & "|" & Table.Rows(0)("SkuComponente")
                                        inner.Text = Table.Rows(0)("SkuComponente") & " : " & Table.Rows(0)("ITEMDESC")
                                        childNodeA.ChildNodes.Add(inner)
                                        treeViewTable(Table)

                                    Else
                                        If childNodeA.ChildNodes.Count > 0 Then
                                            For Each childNodeB As TreeNode In childNodeA.ChildNodes
                                                If childNodeB.Checked Then

                                                    For Each dr As DataRow In Table.Rows
                                                        If dr("Id") = Table.Rows(0)("Id").ToString() And dr("SkuComponente") = Table.Rows(0)("SkuComponente") Then
                                                            dr("SkuArticulo") = Split(myNode.Value, "|")(2)
                                                            dr("Nivel1") = Split(childNodeA.Value, "|")(1)
                                                            dr("Nivel2") = Split(childNodeB.Value, "|")(1)
                                                            If childNodeB.ChildNodes.Count = 0 Then
                                                                dr("Nivel3") = 1
                                                            Else
                                                                Dim dt2 As DataTable = DirectCast(Session("treeView"), DataTable)
                                                                Dim valMax = FindMaxDataTableValue(dt2, "Nivel3") + 1
                                                                dr("Nivel3") = valMax ''dr("Nivel3") = childNodeB.ChildNodes.Count + 1
                                                            End If
                                                        End If
                                                    Next

                                                    Dim innerB As New TreeNode()
                                                    innerB.Value = Table.Rows(0)("Id").ToString() & "|" & Table.Rows(0)("Nivel3") & "|" & Table.Rows(0)("SkuComponente")
                                                    innerB.Text = Table.Rows(0)("SkuComponente") & " : " & Table.Rows(0)("ITEMDESC")
                                                    childNodeB.ChildNodes.Add(innerB)
                                                    treeViewTable(Table)

                                                End If
                                            Next
                                        End If
                                    End If
                                Next
                            End If
                            If myNode.Checked Then

                                For Each dr As DataRow In Table.Rows
                                    If dr("Id") = Table.Rows(0)("Id").ToString() And dr("SkuComponente") = Table.Rows(0)("SkuComponente") Then

                                        dr("SkuArticulo") = Split(myNode.Value, "|")(2)

                                        If myNode.ChildNodes.Count = 0 Then
                                            dr("Nivel1") = 1
                                        Else
                                            Dim dt2 As DataTable = DirectCast(Session("treeView"), DataTable)
                                            Dim valMax = FindMaxDataTableValue(dt2, "Nivel1") + 1
                                            dr("Nivel1") = valMax ''dr("Nivel1") = myNode.ChildNodes.Count + 1
                                        End If
                                    End If
                                Next

                                Dim inner As New TreeNode()
                                inner.Value = Table.Rows(0)("Id").ToString() & "|" & Table.Rows(0)("Nivel1") & "|" & Table.Rows(0)("SkuComponente")
                                inner.Text = Table.Rows(0)("SkuComponente") & " : " & Table.Rows(0)("ITEMDESC")
                                myNode.ChildNodes.Add(inner)
                                treeViewTable(Table)

                            End If
                        Next myNode
                    End If
                Else
                    Dim inner As New TreeNode()
                    inner.Value = Table.Rows(0)("Id").ToString() & "|" & Table.Rows(0)("Nivel1") & "|" & Table.Rows(0)("SkuComponente")
                    inner.Text = Table.Rows(0)("SkuComponente") & " : " & Table.Rows(0)("ITEMDESC")
                    TreeView1.Nodes.Add(inner)
                    treeViewTable(Table)
                End If
            End If


        End If

        Me.DDElemento.SelectedIndex = 0
    End Sub

    Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.MultiView1.ActiveViewIndex = 2
    End Sub

    Private Sub DDCliente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDCliente.SelectedIndexChanged
        Dim value As String = DDCliente.Items(DDCliente.SelectedIndex).Value

        Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.ITEM & "?ClientId=" & value,, Session("access_token"))
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

    End Sub
End Class