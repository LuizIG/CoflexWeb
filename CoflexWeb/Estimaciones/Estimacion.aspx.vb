Imports System.Data.SqlClient
Imports System.Globalization
Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Estimacion
    Inherits CoflexWebPage

    Private data As New DataSet
    Private Suma As Double
    Private SumaCotizacion As Double
    Private SumaMargen As Double
    Private SumaMargenPorcentaje As Double
    Private RowsCount As Integer

    Private Status As Integer

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        MyBase.Page_Load(sender, e)
        If Not IsPostBack Then

            Session("Status") = 0

            Me.DDArticulo.Items.Clear()
            Me.DDCliente.Items.Clear()
            Me.DDComponente.Items.Clear()
            Me.DDElemento.Items.Clear()
            Me.DDClienteCotiza.Items.Clear()


            Me.DDArticulo.Items.Insert(0, "Seleccionar")

            Dim r As New Globalization.CultureInfo("es-ES")
            r.NumberFormat.CurrencyDecimalSeparator = "."
            r.NumberFormat.NumberDecimalSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture = r

            Session.Remove("treeView")
            Session.Remove("Margin")
            Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.COMPONENT,, Session("access_token"))
            Dim o = JObject.Parse(jsonResponse)
            Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                Dim detail = o.GetValue("detail").Value(Of JArray)
                Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)
                Table.DefaultView.Sort = "PPN_I asc"
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


            jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.CLIENTS,, Session("access_token"))
            o = JObject.Parse(jsonResponse)
            statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                Dim detail = o.GetValue("detail").Value(Of JArray)
                Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)
                Me.DDClienteCotiza.DataSource = Table
                Me.DDClienteCotiza.DataValueField = "Id"
                Me.DDClienteCotiza.DataTextField = "ClientName"
                Me.DDClienteCotiza.DataBind()
            End If
            Me.DDClienteCotiza.Items.Insert(0, "Seleccionar")
            Me.DDClienteCotiza.Items.Insert(1, "Prospecto")
            Me.DDClienteCotiza.Items(1).Value = "PROSP"


            jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.PROSPECTS,, Session("access_token"))
            o = JObject.Parse(jsonResponse)
            statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                Dim detail = o.GetValue("detail").Value(Of JArray)
                Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)
                Me.DDProspecto.DataSource = Table
                Me.DDProspecto.DataValueField = "Id"
                Me.DDProspecto.DataTextField = "CompanyName"
                Me.DDProspecto.DataBind()
            End If
            Me.DDProspecto.Items.Insert(0, "Seleccionar")

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

                DDClienteCotiza.Enabled = False 'Previene que se cambie el cliente

                Dim QuotationResponse = CoflexWebServices.doGetRequest(CoflexWebServices.QUOTATIONS & "/" & Request.QueryString("q"),, Session("access_token"))

                o = JObject.Parse(QuotationResponse)
                statusCode = o.GetValue("statusCode").Value(Of Integer)
                If (statusCode >= 200 And statusCode < 400) Then
                    Dim detail = o.GetValue("detail").Value(Of JObject)
                    Dim ClientId = detail.GetValue("ClientId").Value(Of String)


                    If ClientId = "PROSP" Then
                        DDProspecto.SelectedValue = detail.GetValue("ProspectId").Value(Of Integer)
                    End If

                    DDClienteCotiza.SelectedValue = ClientId
                    Dim CoflexId = detail.GetValue("CoflexId").Value(Of String)
                    TB_COTIZACION.Text = CoflexId

                End If

                Dim ResponseVersions = CoflexWebServices.getVersion(CoflexWebServices.QUOTATIONS_VERSION & "/" & VersionId, Session("access_token"))
                o = JObject.Parse(ResponseVersions)
                statusCode = o.GetValue("statusCode").Value(Of Integer)
                If (statusCode >= 200 And statusCode < 400) Then
                    Dim detail = o.GetValue("detail").Value(Of JObject)

                    Status = detail.GetValue("Status").Value(Of Integer)

                    Session("Status") = Status

                    Dim versionNumber = detail.GetValue("VersionNumber").Value(Of Integer)
                    TB_VERSION.Text = versionNumber

                    DDEstatus.Items.Clear()

                    Select Case Status
                        Case 0
                            TB_ESTATUS.Text = ABIERTA
                            status_actual.InnerText = ABIERTA
                            DDEstatus.Items.Add(New ListItem(ABIERTA, "0"))
                            DDEstatus.Items.Add(New ListItem(ENVIADA, "1"))
                            DDEstatus.Items.Add(New ListItem(RECHAZADA, "2"))
                            DDEstatus.Items.Add(New ListItem(ACEPTADA, "3"))
                            DDEstatus.Items.Add(New ListItem(CANCELADA, "4"))
                        Case 1
                            TB_ESTATUS.Text = ENVIADA
                            status_actual.InnerText = ENVIADA
                            DDEstatus.Items.Add(New ListItem(ENVIADA, "1"))
                            DDEstatus.Items.Add(New ListItem(ACEPTADA, "3"))
                            DDEstatus.Items.Add(New ListItem(CANCELADA, "4"))
                        Case 2
                            TB_ESTATUS.Text = RECHAZADA
                            status_actual.InnerText = RECHAZADA
                            DDEstatus.Items.Add(New ListItem(RECHAZADA, "2"))
                            DDEstatus.Items.Add(New ListItem(CANCELADA, "4"))
                        Case 3
                            TB_ESTATUS.Text = ACEPTADA
                            status_actual.InnerText = ACEPTADA
                            DDEstatus.Items.Add(New ListItem(ACEPTADA, "3"))

                        Case 4
                            TB_ESTATUS.Text = CANCELADA
                    End Select


                    If Status > 0 Then
                        ButtonAllAgregar.CssClass = "btn btn-primary disabled"
                        Button1.CssClass = "btn btn-primary disabled"
                        Button4.CssClass = "btn btn-primary disabled"
                        BtnRecalcular.CssClass = "btn btn-success disabled"
                        Guardar.CssClass = "btn btn-primary disabled"
                        Versionar.CssClass = "btn btn-primary disabled"

                        ButtonAllAgregar.Enabled = False
                        Button1.Enabled = False
                        Button4.Enabled = False
                        BtnRecalcular.Enabled = False
                        Guardar.Enabled = False
                        Tv_Exchange.Enabled = False
                        If Status >= 3 Then
                            Versionar.Enabled = False
                        End If
                    Else
                        ButtonAllAgregar.CssClass = "btn btn-primary"
                        Button1.CssClass = "btn btn-primary"
                        Button4.CssClass = "btn btn-primary"
                        BtnRecalcular.CssClass = "btn btn-success"
                        Guardar.CssClass = "btn btn-primary"
                        Versionar.CssClass = "btn btn-primary"

                        ButtonAllAgregar.Enabled = True
                        Button1.Enabled = True
                        Button4.Enabled = True
                        BtnRecalcular.Enabled = True
                        Guardar.Enabled = True

                        Tv_Exchange.Enabled = True
                    End If

                    Dim exchange = detail.GetValue("ExchangeRate").Value(Of String)

                    Tv_Exchange.Text = CDbl(exchange).ToString("C4", New CultureInfo("es-MX"))
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

                    If DDCliente.SelectedValue <> "Seleccionar" Then
                        Dim value As String = DDCliente.Items(DDCliente.SelectedIndex).Value

                        jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.ITEM & "?ClientId=" & value,, Session("access_token"))
                        o = JObject.Parse(jsonResponse)
                        statusCode = o.GetValue("statusCode").Value(Of Integer)
                        If (statusCode >= 200 And statusCode < 400) Then
                            Dim detailArray = o.GetValue("detail").Value(Of JArray)
                            Table = JsonConvert.DeserializeObject(Of DataTable)(detailArray.ToString)
                            Me.DDArticulo.DataSource = Table
                            Me.DDArticulo.DataValueField = "PPN_I"
                            Me.DDArticulo.DataTextField = "PPN_I"
                            Me.DDArticulo.DataBind()
                        End If

                        'If Me.TreeView1.Nodes.Count > 0 Then
                        '    DDCliente.Enabled = False
                        'End If

                        Me.DDArticulo.Items.Insert(0, "Seleccionar")
                    Else
                        Dim value As String = DDCliente.Items(DDCliente.SelectedIndex).Value

                        jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.ITEMGROUP,, Session("access_token"))
                        o = JObject.Parse(jsonResponse)
                        statusCode = o.GetValue("statusCode").Value(Of Integer)
                        If (statusCode >= 200 And statusCode < 400) Then
                            Dim detailArray = o.GetValue("detail").Value(Of JArray)
                            Table = JsonConvert.DeserializeObject(Of DataTable)(detailArray.ToString)
                            Me.DDArticulo.DataSource = Table
                            Me.DDArticulo.DataValueField = "PPN_I"
                            Me.DDArticulo.DataTextField = "PPN_I"
                            Me.DDArticulo.DataBind()
                        End If

                        'If Me.TreeView1.Nodes.Count > 0 Then
                        '    DDCliente.Enabled = False
                        'End If

                        Me.DDArticulo.Items.Insert(0, "Seleccionar")
                    End If

                End If

            Else

                jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.EXCHANGE_RATE,, Session("access_token"))
                o = JObject.Parse(jsonResponse)
                statusCode = o.GetValue("statusCode").Value(Of Integer)
                If (statusCode >= 200 And statusCode < 400) Then
                    Dim d = o.GetValue("detail").Value(Of JObject)

                    Dim exchange = d.GetValue("MXN_to_DLLS").Value(Of String)

                    Tv_Exchange.Text = CDbl(exchange).ToString("C4", New CultureInfo("es-MX"))
                End If

                If DDCliente.SelectedValue = "Seleccionar" Then
                    Dim value As String = DDCliente.Items(DDCliente.SelectedIndex).Value

                    jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.ITEMGROUP,, Session("access_token"))
                    o = JObject.Parse(jsonResponse)
                    statusCode = o.GetValue("statusCode").Value(Of Integer)
                    If (statusCode >= 200 And statusCode < 400) Then
                        Dim detailArray = o.GetValue("detail").Value(Of JArray)
                        Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detailArray.ToString)
                        Me.DDArticulo.DataSource = Table
                        Me.DDArticulo.DataValueField = "PPN_I"
                        Me.DDArticulo.DataTextField = "PPN_I"
                        Me.DDArticulo.DataBind()
                    End If

                    'If Me.TreeView1.Nodes.Count > 0 Then
                    '    DDCliente.Enabled = False
                    'End If

                    Me.DDArticulo.Items.Insert(0, "Seleccionar")
                End If
            End If

            If Request.QueryString("r") IsNot Nothing Then
                'Se redirigio por guardar nuevas cotizaciones
                ContinueMethod()

                result_div_ok.Style.Add("display", "block")
                result_div_error.Style.Add("display", "none")
                div_description_ok.InnerText = "La cotización se guardó correctamente"


            End If


            '    If (Session("tables") Is Nothing) Then
            '        data = New DataSet
            '    Else
            '        data = Session("tables")
            '    End If
        End If
    End Sub

    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ContinueMethod()
    End Sub

    Private Sub ContinueMethod()
        If Session("treeView") IsNot Nothing And TreeView1.Nodes.Count > 0 Then
            Me.MultiView1.ActiveViewIndex = 1
            Suma = 0
            SumaCotizacion = 0
            SumaMargen = 0
            SumaMargenPorcentaje = 0
            RowsCount = 0

            Dim dv As New DataView(DirectCast(Session("treeView"), DataTable))
            dv.RowFilter = "Nivel1 = 0 and Nivel2 = 0 and Nivel3 = 0"


            If Not dv.Table.Columns.Contains("UnitaryCost") Then
                dv.Table.Columns.Add("UnitaryCost", GetType(Double))
            End If

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
                        row("Margin") = System.Configuration.ConfigurationManager.AppSettings("Throughput") * 100
                    End If
                    row("UnitaryCost") = CDbl(row("FinalCost")) / CDbl(row("QUANTITY_I"))
                Next

            Else
                If Not dv.Table.Columns.Contains("Margin") Then
                    dv.Table.Columns.Add("Margin", GetType(Double))
                End If
                For Each row In dv
                    row("Margin") = System.Configuration.ConfigurationManager.AppSettings("Throughput") * 100
                    row("UnitaryCost") = CDbl(row("FinalCost")) / CDbl(row("QUANTITY_I"))
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

    Protected Sub treeViewTable(ByVal fDataTable As DataTable)
        If Session("treeView") Is Nothing Then
            Session("treeView") = fDataTable
        Else

            DirectCast(Session("treeView"), DataTable).Merge(fDataTable)

        End If
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


        Try

            If TreeView1.CheckedNodes.Count > 0 Then
                If TreeView1.Nodes.Count > 0 Then
                    For z = 0 To Me.TreeView1.Nodes.Count
                        If z >= TreeView1.Nodes.Count Then
                            Exit For
                        End If
                        Dim myNode = TreeView1.Nodes(z)

                        If myNode.ChildNodes.Count > 0 Then
                            For a = 0 To myNode.ChildNodes.Count
                                If a >= myNode.ChildNodes.Count Then
                                    Exit For
                                End If
                                Dim childNodeA = myNode.ChildNodes(a)
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
                                ''Dim rows = dt2.[Select]("Nivel1 = " & reng("Nivel1") & " and SkuArticulo = '" & reng("SkuArticulo") & "'")
                                Dim rows = dt2.[Select]("SkuArticulo = '" & reng("SkuArticulo") & "'")
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
                'Else
                '    TreeView1.Nodes.Clear()
            End If
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType, "script", "clearCheckBox();", True)

        Catch ex As Exception

            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType, "script", "clearCheckBox();", True)

        End Try
        'If Me.TreeView1.Nodes.Count > 0 Then
        '    DDCliente.Enabled = False
        'Else
        '    DDCliente.Enabled = True
        'End If

    End Sub

    Private Sub TreeView1_SelectedNodeChanged(sender As Object, e As EventArgs) Handles TreeView1.SelectedNodeChanged
        Try

            Dim scTreeView = TreeView1.SelectedNode.Value
            Dim Table As DataTable = DirectCast(Session("treeView"), DataTable)
            Dim dv As New DataView(Table)
            dv.RowFilter = "Id = " & Split(scTreeView, "|")(0) & " and SkuComponente = '" & Split(scTreeView, "|")(2) & "'"

            If Split(scTreeView, "|")(1) = 0 Then
                Me.Label6.Visible = True
                Me.TextBox8.Visible = True
                Me.RadioButton3.Visible = False
                Me.TextBox8.Text = 0
                Me.TextBox3.Enabled = False
            Else
                Me.Label6.Visible = False
                Me.TextBox8.Visible = False
                Me.RadioButton3.Visible = False
                Me.TextBox3.Enabled = True
            End If

            For Each reng As DataRowView In dv
                Me.TextBox1.Text = reng("SkuArticulo")
                Me.TextBox2.Text = reng("SkuComponente")
                Me.TextArea1.Value = reng("ITEMDESC")
                Me.TextBox3.Text = reng("QUANTITY_I")
                Me.TextBox4.Text = reng("UOFM")
                Me.TextBox7.Text = reng("CURRCOST")
                Me.TextBox5.Text = reng("STNDCOST")
                Me.TextBox6.Text = Math.Round(reng("FinalCost"), 2)
                If Math.Round((reng("FinalCost") / reng("QUANTITY_I")), 4) <> reng("STNDCOST") And Math.Round((reng("FinalCost") / reng("QUANTITY_I")), 4) <> reng("CURRCOST").ToString Then
                    Me.TextBox8.Text = Math.Round((reng("FinalCost") / reng("QUANTITY_I")), 2)
                    Me.RadioButton3.Checked = True
                    Me.RadioButton1.Checked = False
                    Me.RadioButton2.Checked = False
                ElseIf reng("RACost") = 1 Then
                    Me.RadioButton1.Checked = True
                    Me.RadioButton2.Checked = False
                    Me.RadioButton3.Checked = False
                ElseIf reng("RBCost") = 1 Then
                    Me.RadioButton2.Checked = True
                    Me.RadioButton1.Checked = False
                    Me.RadioButton3.Checked = False
                End If
            Next

        Catch ex As Exception

        End Try


    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try
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
                            dr2("FinalCost") = dr2("QUANTITY_I") * ((dr2("RACost") * dr2("STNDCOST")) + (dr2("RBCost") * dr2("CURRCOST")))
                            Me.TextBox6.Text = Math.Round(dr("Result"), 2)
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
                            dr2("FinalCost") = dr2("QUANTITY_I") * ((dr2("RACost") * dr2("STNDCOST")) + (dr2("RBCost") * dr2("CURRCOST")))
                            Me.TextBox6.Text = Math.Round(dr("Result"), 2)
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
                            dr2("FinalCost") = dr2("QUANTITY_I") * ((dr2("RACost") * dr2("STNDCOST")) + (dr2("RBCost") * dr2("CURRCOST")))
                            Me.TextBox6.Text = Math.Round(dr("Result"), 2)
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
                            dr2("FinalCost") = dr2("QUANTITY_I") * ((dr2("RACost") * dr2("STNDCOST")) + (dr2("RBCost") * dr2("CURRCOST")))
                            Me.TextBox6.Text = Math.Round(dr("Result"), 2)
                        Next
                    End If

                End If

            Next

            treeViewTable(Table)
            Montos(Table)
            treeViewTable(Table)

            If Split(scTreeView, "|")(1) = 0 Then
                Dim scTreeView22 = TreeView1.SelectedNode.Value
                Dim Table22 As DataTable = DirectCast(Session("treeView"), DataTable)
                Dim rows22 = Table.[Select]("id = " & Split(scTreeView, "|")(0) & " and SkuComponente = '" & Split(scTreeView, "|")(2) & "'")


                For Each reng As DataRow In rows22
                    If Math.Round((reng("FinalCost") / reng("QUANTITY_I")), 4) <> reng("STNDCOST") And Math.Round((reng("FinalCost") / reng("QUANTITY_I")), 4) <> reng("CURRCOST").ToString Then
                        Me.TextBox8.Text = (reng("FinalCost") / reng("QUANTITY_I"))
                        Me.RadioButton3.Checked = True
                        Me.RadioButton1.Checked = False
                        Me.RadioButton2.Checked = False
                    ElseIf reng("RACost") = 1 Then
                        Me.RadioButton1.Checked = True
                        Me.RadioButton2.Checked = False
                        Me.RadioButton3.Checked = False
                    ElseIf reng("RBCost") = 1 Then
                        Me.RadioButton2.Checked = True
                        Me.RadioButton1.Checked = False
                        Me.RadioButton3.Checked = False
                    End If
                Next

            End If

            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType, "script", "clearCheckBox();", True)

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType, "script", "clearCheckBox();", True)
        End Try
    End Sub

    Private Sub Montos(ByVal fDataTable As DataTable)

        Try
            Dim scTreeView = TreeView1.SelectedNode.Value
            Dim Table As DataTable = DirectCast(Session("treeView"), DataTable)
            Dim rows = Table.[Select]("id = " & Split(scTreeView, "|")(0) & " and SkuComponente = '" & Split(scTreeView, "|")(2) & "'")

            For Each dr As DataRow In rows
                If dr("Nivel3") > 0 Then
                    Dim rows3 = Table.[Select]("Nivel1 = " & dr("Nivel1") & " and Nivel2 = " & dr("Nivel2") & " and Nivel3 = 0 and SkuArticulo = '" & dr("SkuArticulo") & "'")
                    For Each dr3 As DataRow In rows3
                        Dim Table2 As DataTable = DirectCast(Session("treeView"), DataTable)
                        Dim rows2 = Table.[Select]("Nivel1 = " & dr("Nivel1") & " and Nivel2 = " & dr("Nivel2") & " and Nivel3 > 0 and SkuArticulo = '" & dr("SkuArticulo") & "'")
                        Dim Suma As Double = 0
                        For Each dr2 As DataRow In rows2
                            Suma += dr2("Result")
                        Next
                        dr3("Result") = Suma
                        dr3("FinalCost") = Suma
                    Next
                End If

                If dr("Nivel2") > 0 Then
                    Dim rows3 = Table.[Select]("Nivel1 = " & dr("Nivel1") & " and Nivel2 = 0 and Nivel3 = 0 and SkuArticulo = '" & dr("SkuArticulo") & "'")
                    For Each dr3 As DataRow In rows3

                        Dim Table2 As DataTable = DirectCast(Session("treeView"), DataTable)
                        Dim rows2 = Table.[Select]("Nivel1 = " & dr("Nivel1") & " and Nivel2 > 0 and Nivel3 = 0 and SkuArticulo = '" & dr("SkuArticulo") & "'")
                        Dim Suma As Double = 0
                        For Each dr2 As DataRow In rows2
                            Suma += dr2("Result")
                        Next
                        dr3("Result") = Suma
                        dr3("FinalCost") = Suma
                    Next
                End If

                If dr("Nivel1") > 0 Then
                    Dim rows3 = Table.[Select]("Nivel1 = 0 and  Nivel2 = 0 and Nivel3 = 0 and SkuArticulo = '" & dr("SkuArticulo") & "'")

                    For Each dr3 As DataRow In rows3
                        Dim Table2 As DataTable = DirectCast(Session("treeView"), DataTable)
                        Dim rows2 = Table.[Select]("Nivel1 > 0 and Nivel2 = 0 and Nivel3 = 0 and SkuArticulo = '" & dr("SkuArticulo") & "'")
                        Dim Suma As Double = 0
                        For Each dr2 As DataRow In rows2
                            Suma += dr2("Result")
                        Next
                        dr3("Result") = Suma
                        dr3("FinalCost") = Suma
                    Next
                End If

                If dr("Nivel1") = 0 Then
                    Dim rows3 = Table.[Select]("Nivel1 = 0 and  Nivel2 = 0 and Nivel3 = 0 and SkuArticulo = '" & dr("SkuArticulo") & "'")

                    For Each dr3 As DataRow In rows3
                        Dim Table2 As DataTable = DirectCast(Session("treeView"), DataTable)
                        Dim rows2 = Table.[Select]("Nivel1 > 0 and Nivel2 = 0 and Nivel3 = 0 and SkuArticulo = '" & dr("SkuArticulo") & "'")
                        Dim Suma As Double = 0
                        For Each dr2 As DataRow In rows2
                            Suma += dr2("Result")
                        Next
                        dr3("Result") = Suma
                        dr3("FinalCost") = Suma
                        Me.TextBox6.Text = Suma
                    Next
                End If

                'If dr("Nivel1") > 0 And dr("Nivel2") > 0 And dr("Nivel3") > 0 Then
                '    Dim rows2 = Table.[Select]("Nivel1 = " & dr("Nivel1") & " and Nivel2 = " & dr("Nivel2") & " and SkuArticulo = '" & dr("SkuArticulo") & "'")
                '    For Each dr2 As DataRow In rows2

                '    Next

                'End If
            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Versionar_Click(sender As Object, e As EventArgs) Handles Versionar.Click

        Try
            'Si no existe la cotizacion, crea una nueva

            If (Session("Status") < 3) Then 'Solo 0
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

                        'Se creo una nueva cotizacion
                        Response.Redirect("Estimacion?q=" & quotationID & "&v=" & versionID & "&r=1")

                    Else

                    End If
                Else
                    Dim ResponseS = doPostRequest(QUOTATIONS_VERSION, CreateQuotationVersion(IdQuotaion).ToString,, Session("access_token"))
                    Dim o = JObject.Parse(ResponseS)
                    Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
                    If (statusCode >= 200 And statusCode < 400) Then
                        Dim QuotationCreated = o.GetValue("detail").Value(Of JObject)
                        'Se creo una nueva version
                        Response.Redirect("Estimacion?q=" & IdQuotaion & "&v=" & QuotationCreated.GetValue("Id").Value(Of Integer) & "&r=2")
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Guardar_Click(sender As Object, e As EventArgs) Handles Guardar.Click

        Try

            If (Session("Status") = 0) Then 'Solo 0
                Dim IdQuotaionVersion As String = Request.QueryString("v")
                If IdQuotaionVersion IsNot Nothing Then
                    Dim Response = doPutRequest(QUOTATIONS_VERSION & "/" & IdQuotaionVersion, CreateQuotationVersion(IdQuotaionVersion).ToString,, Session("access_token"))
                    Dim o = JObject.Parse(Response)
                    Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
                    If (statusCode >= 200 And statusCode < 400) Then
                        Dim QuotationCreated = o.GetValue("detail").Value(Of JObject)
                        result_div_ok.Style.Add("display", "block")
                        result_div_error.Style.Add("display", "none")
                        div_description_ok.InnerText = "La cotización se guardó correctamente"
                    Else
                        result_div_ok.Style.Add("display", "none")
                        result_div_error.Style.Add("display", "block")
                        div_description.InnerText = "Ocurrió un error al guardar la cotización"
                    End If
                Else
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
                        'Se guardo una nueva cotizacion
                        Response.Redirect("Estimacion?q=" & quotationID & "&v=" & versionID & "&r=1")

                    Else
                        result_div_ok.Style.Add("display", "none")
                        result_div_error.Style.Add("display", "block")
                        div_description.InnerText = "Ocurrió un error al guardar la cotización"
                    End If
                End If

            Else
                result_div_ok.Style.Add("display", "none")
                result_div_error.Style.Add("display", "block")
                div_description.InnerText = "Esta cotización ya no puede modificarse"

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Function CreateQuotation() As JObject
        Dim Quotation As New JObject
        Quotation.Add("ClientId", DDClienteCotiza.SelectedValue)

        If (DDProspecto.SelectedValue <> "Seleccionar") Then
            Quotation.Add("ProspectId", CInt(DDProspecto.SelectedValue))
            Quotation.Add("ClientName", DDProspecto.SelectedItem.Text)
        Else
            Quotation.Add("ClientName", DDClienteCotiza.SelectedItem.Text)
        End If
        Quotation.Add("QuotationVersions", CreateQuotationVersion())
        Return Quotation
    End Function

    Private Function CreateQuotationVersion() As JObject
        Dim QuotationVersion As New JObject

        Dim exchange As Double = CDbl(Val(Tv_Exchange.Text.Replace("$", "")))

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



            Dim TBMargin As TextBox = TryCast(row.FindControl("TVMargin"), TextBox)
            Dim TBDescAlt As TextBox = TryCast(row.FindControl("TBDescAlt"), TextBox)

            Item.Add("Sku", sku)
            Item.Add("ItemDescription", reng("ITEMDESC").ToString)
            Item.Add("Quantity", 1.0)
            Item.Add("UM", reng("UOFM").ToString)
            Item.Add("Status", 0)
            Item.Add("ProfitMargin", CDbl(TBMargin.Text) / 100)
            Item.Add("ItemsComponents", CreateItemComponents(sku, "1.0", TBDescAlt.Text))
            ItemArray.Add(Item)
        Next
        Return ItemArray
    End Function

    Private Function CreateItemComponents(ByVal itemSku As String, ByVal cant As String, ByVal descAlt As String) As JArray
        Dim ItemComponentsArray As New JArray
        Dim dv As New DataView(DirectCast(Session("treeView"), DataTable))
        dv.RowFilter = "SkuArticulo = '" & itemSku & "'"
        For Each reng As DataRowView In dv
            Dim Item As New JObject

            If CInt(reng("Nivel1").ToString) = 0 And CInt(reng("Nivel2").ToString) = 0 And CInt(reng("Nivel3").ToString) = 0 Then
                Item.Add("Quantity", CDbl(cant))
                Item.Add("AltDescription", descAlt)
                reng("AltDescription") = descAlt
            Else
                Item.Add("Quantity", CDbl(reng("QUANTITY_I").ToString))
                Item.Add("AltDescription", "")
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
            Item.Add("Shipping", CDbl(reng("Shipping").ToString))
            ItemComponentsArray.Add(Item)
        Next
        Return ItemComponentsArray
    End Function

    Protected Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Me.MultiView1.ActiveViewIndex = 0
    End Sub

    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        Try
            Dim Elemento As New JObject

            With Elemento
                .Add("SkuComponente", Me.txtSkuComponente.Text)
                .Add("IdQuotation", 0)
                .Add("Uofm", Me.txtUofm.Text)
                .Add("ItemDesc", Me.txtItemDesc.Value)
                .Add("StndCost", CDbl(Me.txtStndCost.Text))
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


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.MultiView1.ActiveViewIndex = 2

        txtSkuComponente.Text = ""
        txtItemDesc.InnerText = ""
        txtUofm.Text = ""
        txtStndCost.Text = ""

    End Sub

    Private Sub DDCliente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDCliente.SelectedIndexChanged
        If DDCliente.SelectedValue <> "Seleccionar" Then
            Dim value As String = DDCliente.Items(DDCliente.SelectedIndex).Value

            Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.ITEM & "?ClientId=" & value,, Session("access_token"))
            Dim o = JObject.Parse(jsonResponse)
            Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                Dim detailArray = o.GetValue("detail").Value(Of JArray)
                Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detailArray.ToString)
                Me.DDArticulo.DataSource = Table
                Me.DDArticulo.DataValueField = "PPN_I"
                Me.DDArticulo.DataTextField = "PPN_I"
                Me.DDArticulo.DataBind()
            End If

            'If Me.TreeView1.Nodes.Count > 0 Then
            '    DDCliente.Enabled = False
            'End If

            Me.DDArticulo.Items.Insert(0, "Seleccionar")
        Else
            Dim value As String = DDCliente.Items(DDCliente.SelectedIndex).Value

            Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.ITEMGROUP,, Session("access_token"))
            Dim o = JObject.Parse(jsonResponse)
            Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                Dim detailArray = o.GetValue("detail").Value(Of JArray)
                Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detailArray.ToString)
                Me.DDArticulo.DataSource = Table
                Me.DDArticulo.DataValueField = "PPN_I"
                Me.DDArticulo.DataTextField = "PPN_I"
                Me.DDArticulo.DataBind()
            End If

            'If Me.TreeView1.Nodes.Count > 0 Then
            '    DDCliente.Enabled = False
            'End If

            Me.DDArticulo.Items.Insert(0, "Seleccionar")
        End If

    End Sub

    Private Sub GridSummary_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridSummary.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim txCantidad = TryCast(e.Row.FindControl("TBQuantity"), TextBox)
            Dim txMargin = TryCast(e.Row.FindControl("TVMargin"), TextBox)
            Dim txShipping = TryCast(e.Row.FindControl("TVShipping"), TextBox)
            Dim costoUnitario As Double = CDbl(e.Row.Cells(4).Text.Replace("$", ""))
            Dim cantidad As Double = CDbl(txCantidad.Text.Replace("$", ""))
            Dim margen As Double = CDbl(txMargin.Text.Replace("$", "")) / 100
            Dim shipping As Double = CDbl(txShipping.Text.Replace("$", ""))

            Dim costoTotal As Double = (costoUnitario + (costoUnitario * 0.01)) / (1 - margen)
            Dim costoMargen As Double = (costoUnitario + (costoUnitario * 0.01)) * margen

            If (Session("Status") > 0) Then
                txMargin.Enabled = False
            Else
                txMargin.Enabled = True
            End If


            'Acumulando el monto


            Suma += costoTotal

            SumaMargen += costoMargen
            SumaMargenPorcentaje += margen
            RowsCount = RowsCount + 1

            e.Row.Cells(10).Text = (costoMargen).ToString("C2", New CultureInfo("es-MX"))
            e.Row.Cells(11).Text = (costoTotal).ToString("C2", New CultureInfo("es-MX"))
            e.Row.Cells(12).Text = (costoTotal / CDbl(Tv_Exchange.Text.Replace("$", ""))).ToString("C2", New CultureInfo("es-MX"))

        ElseIf (e.Row.RowType = DataControlRowType.Footer) Then

            '<<<<<<< HEAD
            margen_ganancia.InnerHtml = ""
            'e.Row.Cells(9).Text = (SumaMargenPorcentaje * 100) / RowsCount & "%" '(Suma).ToString("C2", New CultureInfo("es-MX"))
            '=======
            '           margen_ganancia.InnerHtml = "<h4>Margen de Ganancia: " & SumaMargen.ToString("C2", New CultureInfo("es-MX")) & "</h4>"
            e.Row.Cells(9).Text = ((SumaMargenPorcentaje * 100) / RowsCount).ToString("f2") & "%" '(Suma).ToString("C2", New CultureInfo("es-MX"))
            '>>>>>>> bdd0d5ce898f81e71135d6214b1df2f286a35541
            e.Row.Cells(10).Text = SumaMargen.ToString("C2", New CultureInfo("es-MX"))
            e.Row.Cells(11).Text = (Suma).ToString("C2", New CultureInfo("es-MX"))
            e.Row.Cells(12).Text = (Suma / CDbl(Tv_Exchange.Text.Replace("$", ""))).ToString("C2", New CultureInfo("es-MX"))
        End If
    End Sub

    Private Sub BtnRecalcular_Click(sender As Object, e As EventArgs) Handles BtnRecalcular.Click
        Dim dv As New DataView(DirectCast(Session("treeView"), DataTable))
        dv.RowFilter = "Nivel1 = 0 and Nivel2 = 0 and Nivel3 = 0"
        If Not dv.Table.Columns.Contains("Margin") Then
            dv.Table.Columns.Add("Margin", GetType(Double))
        End If
        For Each reng As DataRowView In dv
            For Each renglon As GridViewRow In GridSummary.Rows
                If (reng("SkuArticulo") = renglon.Cells(0).Text) Then
                    Dim TBMargin As TextBox = TryCast(renglon.FindControl("TVMargin"), TextBox)
                    Dim TBShipping As TextBox = TryCast(renglon.FindControl("TVShipping"), TextBox)
                    reng("QUANTITY_I") = CDbl(1)
                    reng("Margin") = CDbl(TBMargin.Text)

                    If (TBShipping.Text = "") Then
                        reng("Shipping") = CDbl("0")
                    Else
                        reng("Shipping") = CDbl(TBShipping.Text.Replace("$", ""))
                    End If


                    reng("FinalCost") = (reng("UnitaryCost"))
                End If
            Next
        Next
        GridSummary.DataSource = dv.ToTable
        GridSummary.DataBind()
    End Sub

    Private Sub btn_accept_Click(sender As Object, e As EventArgs) Handles BTN_ACEPTAR_1.Click
        Dim idQuotation = Request.QueryString("v")
        If idQuotation IsNot Nothing Then
            Dim STATUS = DDEstatus.Items(DDEstatus.SelectedIndex).Value
            Dim responset = doPatchRequest(QUOTATIONS_VERSION & "/" & idQuotation & "?status=" & STATUS, "",, Session("access_token"))
            Dim o = JObject.Parse(responset)
            Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                Me.Status = CInt(STATUS)
                result_div_ok.Style.Add("display", "block")
                result_div_error.Style.Add("display", "none")
                div_description_ok.InnerText = "Se cambió el estatus correctamente"
                Select Case STATUS
                    Case 0
                        TB_ESTATUS.Text = ABIERTA
                    Case 1
                        TB_ESTATUS.Text = ENVIADA
                    Case 2
                        TB_ESTATUS.Text = RECHAZADA
                    Case 3
                        TB_ESTATUS.Text = ACEPTADA
                    Case 4
                        TB_ESTATUS.Text = CANCELADA
                End Select
                Session("Status") = STATUS

                If STATUS > 0 Then
                    ButtonAllAgregar.CssClass = "btn btn-primary disabled"
                    Button1.CssClass = "btn btn-primary disabled"
                    Button4.CssClass = "btn btn-primary disabled"
                    BtnRecalcular.CssClass = "btn btn-success disabled"
                    Guardar.CssClass = "btn btn-primary disabled"

                    ButtonAllAgregar.Enabled = False
                    Button1.Enabled = False
                    Button4.Enabled = False
                    BtnRecalcular.Enabled = False
                    Guardar.Enabled = False

                    Tv_Exchange.Enabled = False

                    If STATUS >= 3 Then
                        Versionar.Enabled = False
                        Versionar.CssClass = "btn btn-primary disabled"
                    End If

                Else
                    ButtonAllAgregar.CssClass = "btn btn-primary"
                    Button1.CssClass = "btn btn-primary"
                    Button4.CssClass = "btn btn-primary"
                    BtnRecalcular.CssClass = "btn btn-success"
                    Guardar.CssClass = "btn btn-primary"

                    Versionar.Enabled = True
                    Versionar.CssClass = "btn btn-primary"

                    ButtonAllAgregar.Enabled = True
                    Button1.Enabled = True
                    Button4.Enabled = True
                    BtnRecalcular.Enabled = True
                    Guardar.Enabled = True

                    Tv_Exchange.Enabled = True
                End If

                ContinueMethod()

            Else
                result_div_ok.Style.Add("display", "none")
                result_div_error.Style.Add("display", "block")
                div_description.InnerText = "Ocurrió un error al guardar el estatus"
            End If
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType, "script", "hideDiv();", True)
        End If
    End Sub

    Protected Sub Imprimir_Click(sender As Object, e As EventArgs) Handles Imprimir.Click

        Me.MultiView1.ActiveViewIndex = 3
        Dim dv As New DataView(DirectCast(Session("treeView"), DataTable))
        dv.RowFilter = "Nivel1 = 0 and Nivel2 = 0 and Nivel3 = 0"
        GridViewCotiza.DataSource = dv.ToTable
        GridViewCotiza.DataBind()
        GridViewCotizaENG.DataSource = dv.ToTable
        GridViewCotizaENG.DataBind()

        If Request.QueryString("v") IsNot Nothing Then
            btnGuardaCotiza.Enabled = True
            btnGuardaCotiza.CssClass = "btn btn-primary"
            Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.QUOTATION_COMMENTS & "/" & Request.QueryString("v"),, Session("access_token"))
            Dim o = JObject.Parse(jsonResponse)
            Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                ''Dim detail = o.GetValue("detail").Value(Of JArray)
                Dim detail = o.GetValue("detail").Value(Of JObject)
                ''Dim StndCost = detail.GetValue("StndCost").Value(Of Double)

                txtAtencionENG.Text = detail.GetValue("Atention").Value(Of String)
                TextAreaENG.Text = detail.GetValue("Configuration").Value(Of String)
                txtOrdenEntregaENG.Text = detail.GetValue("OrderDeliery").Value(Of String)
                txtTerminoEntregaENG.Text = detail.GetValue("DeliveryTerms").Value(Of String)
                txtOferValidaENG.Text = detail.GetValue("ValidOffer").Value(Of String)

                txtAtencion.Text = detail.GetValue("Atention").Value(Of String)
                TextArea.Text = detail.GetValue("Configuration").Value(Of String)
                txtOrdenEntrega.Text = detail.GetValue("OrderDeliery").Value(Of String)
                txtTerminoEntrega.Text = detail.GetValue("DeliveryTerms").Value(Of String)
                txtOferValida.Text = detail.GetValue("ValidOffer").Value(Of String)
            End If

        Else
            btnGuardaCotiza.Enabled = False
            btnGuardaCotiza.CssClass = "btn btn-primary disabled"
        End If

        Me.Label23.Text = Today
        Me.Label41.Text = Today
        Label24.Text = TB_COTIZACION.Text
        Label43.Text = TB_COTIZACION.Text

        If DDClienteCotiza.SelectedValue = "PROSP" Then
            lblDDClienteCotiza.Text = DDProspecto.SelectedItem.ToString
            lblDDClienteCotizaENG.Text = DDProspecto.SelectedItem.ToString
        Else
            lblDDClienteCotiza.Text = DDClienteCotiza.SelectedItem.ToString
            lblDDClienteCotizaENG.Text = DDClienteCotiza.SelectedItem.ToString
        End If
    End Sub

    Protected Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Me.MultiView1.ActiveViewIndex = 1
    End Sub

    Protected Sub TreeView_Click(sender As Object, e As EventArgs) Handles TreeView.Click
        Me.MultiView1.ActiveViewIndex = 4
        Dim Table As DataTable = DirectCast(Session("treeView"), DataTable)
        GridTreeView.DataSource = Table
        GridTreeView.DataBind()

    End Sub

    Protected Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Me.MultiView1.ActiveViewIndex = 1
    End Sub

    Protected Sub ButtonAllAgregar_Click(sender As Object, e As EventArgs) Handles ButtonAllAgregar.Click

        Try

            If Me.DDArticulo.SelectedValue <> "Seleccionar" Then

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
            End If


            If Me.DDComponente.SelectedValue <> "Seleccionar" Then
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


            End If


            If Me.DDElemento.SelectedValue <> "Seleccionar" Then

                If ExistProduct(Me.DDElemento.SelectedValue) Then

                    Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.NEW_COMPONENTES & "/" & Me.DDElemento.SelectedValue,, Session("access_token"))
                    Dim o = JObject.Parse(jsonResponse)
                    Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
                    If (statusCode >= 200 And statusCode < 400) Then
                        Dim detail = o.GetValue("detail").Value(Of JObject)
                        Dim StndCost = detail.GetValue("StndCost").Value(Of Double)
                        detail.Remove("StndCost")
                        detail.Add("STNDCOST", StndCost)
                        Dim Table As DataTable = JsonConvert.DeserializeObject(Of DataTable)("[" & detail.ToString & "]")


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
            End If

            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType, "script", "clearCheckBox();", True)

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType, "script", "clearCheckBox();", True)

        End Try
        'If Me.TreeView1.Nodes.Count > 0 Then
        '    DDCliente.Enabled = False
        'Else
        '    DDCliente.Enabled = True
        'End If

    End Sub

    Protected Sub ButtonBack_Click(sender As Object, e As EventArgs) Handles ButtonBack.Click
        Response.Redirect(Me.Page.AppRelativeVirtualPath & "?" & Me.Page.ClientQueryString)
    End Sub

    Private Sub DDClienteCotiza_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDClienteCotiza.SelectedIndexChanged

        If Me.DDClienteCotiza.SelectedValue = "PROSP" Then
            Me.DDProspecto.Enabled = True
        Else
            Me.DDProspecto.Enabled = False
            Me.DDProspecto.SelectedIndex = 0
        End If

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.MultiView1.ActiveViewIndex = 0
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim Elemento As New JObject

        With Elemento
            .Add("CompanyName", Me.txtProsComName.Text)
            .Add("Address", Me.txtProsAddress.Value)
            .Add("ContactName", Me.txtProsContactName.Text)
            .Add("PhoneNumber", Me.txtProsPhoneNumber.Text)
        End With

        Dim jsonResponse = CoflexWebServices.doPostRequest(CoflexWebServices.PROSPECTS, Elemento.ToString,, Session("access_token"))
        Dim o = JObject.Parse(jsonResponse)
        Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
        If (statusCode >= 200 And statusCode < 400) Then
            '' Me.ErrorMessage.Text = "Usuario Registrado"
        Else
            Dim errorMessage = o.GetValue("errorMessage").Value(Of String)
            ''Me.ErrorMessage.Text = errorMessage
        End If

        Me.DDProspecto.Dispose()

        jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.PROSPECTS,, Session("access_token"))
        o = JObject.Parse(jsonResponse)
        statusCode = o.GetValue("statusCode").Value(Of Integer)
        If (statusCode >= 200 And statusCode < 400) Then
            Dim detail = o.GetValue("detail").Value(Of JArray)
            Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)
            Me.DDProspecto.DataSource = Table
            Me.DDProspecto.DataValueField = "Id"
            Me.DDProspecto.DataTextField = "CompanyName"
            Me.DDProspecto.DataBind()
        End If

        Me.DDProspecto.Items.Insert(0, "Seleccionar")

        Me.MultiView1.ActiveViewIndex = 0
    End Sub

    Protected Sub BtnNuevoPros_Click(sender As Object, e As EventArgs) Handles BtnNuevoPros.Click
        Me.MultiView1.ActiveViewIndex = 5
    End Sub

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If Me.pnlContents.Visible = True Then
            Me.pnlContents.Visible = False
            Me.pnlContents3.Visible = True
            Me.btnEspanol.Visible = False
            Me.btnEnglish.Visible = True

            txtAtencionENG.Text = txtAtencion.Text
            TextAreaENG.Text = TextArea.Text
            txtOrdenEntregaENG.Text = txtOrdenEntrega.Text
            txtTerminoEntregaENG.Text = txtTerminoEntrega.Text
            txtOferValidaENG.Text = txtOferValida.Text
        ElseIf Me.pnlContents3.Visible = True Then
            Me.pnlContents.Visible = True
            Me.pnlContents3.Visible = False
            Me.btnEspanol.Visible = True
            Me.btnEnglish.Visible = False

            txtAtencion.Text = txtAtencionENG.Text
            TextArea.Text = TextAreaENG.Text
            txtOrdenEntrega.Text = txtOrdenEntregaENG.Text
            txtTerminoEntrega.Text = txtTerminoEntregaENG.Text
            txtOferValida.Text = txtOferValidaENG.Text
        End If
    End Sub

    Protected Sub btnGuardaCotiza_Click(sender As Object, e As EventArgs) Handles btnGuardaCotiza.Click

        If Request.QueryString("v") IsNot Nothing Then

            Dim Elemento As New JObject
            With Elemento
                If Me.pnlContents.Visible = True Then
                    .Add("Atention", Me.txtAtencion.Text)
                    .Add("Configuration", Me.TextArea.Text)
                    .Add("OrderDeliery", Me.txtOrdenEntrega.Text)
                    .Add("DeliveryTerms", Me.txtTerminoEntrega.Text)
                    .Add("ValidOffer", Me.txtOferValida.Text)
                Else
                    .Add("Atention", Me.txtAtencionENG.Text)
                    .Add("Configuration", Me.TextAreaENG.Text)
                    .Add("OrderDeliery", Me.txtOrdenEntregaENG.Text)
                    .Add("DeliveryTerms", Me.txtTerminoEntregaENG.Text)
                    .Add("ValidOffer", Me.txtOferValidaENG.Text)
                End If
            End With

            Dim jsonResponse = CoflexWebServices.doPutRequest(CoflexWebServices.QUOTATION_COMMENTS & "/" & Request.QueryString("v"), Elemento.ToString,, Session("access_token"))
            Dim o = JObject.Parse(jsonResponse)
            Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                '' Me.ErrorMessage.Text = "Usuario Registrado"
            Else
                Dim errorMessage = o.GetValue("errorMessage").Value(Of String)
                ''Me.ErrorMessage.Text = errorMessage
            End If
        End If

    End Sub

    Private Sub GridViewCotiza_DataBinding(sender As Object, e As GridViewRowEventArgs) Handles GridViewCotiza.RowDataBound, GridViewCotizaENG.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then


            Dim rowView As DataRowView = DirectCast(e.Row.DataItem, DataRowView)

            ' Retrieve the EventTypeID value for the current row. 
            Dim a As String = Convert.ToString(rowView("AltDescription"))

            Dim textoAlterno = a

            If (textoAlterno IsNot Nothing And textoAlterno <> "") Then
                e.Row.Cells(1).Text = textoAlterno
            End If

        End If
    End Sub

    Private Sub GridTreeView_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridTreeView.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lvl3 = CInt(GridTreeView.DataKeys(e.Row.RowIndex)("Nivel3"))

            Dim articulo As String = e.Row.Cells(1).Text

            If (lvl3 > 0) Then

                articulo = "              - " & articulo

            Else
                Dim lvl2 = CInt(GridTreeView.DataKeys(e.Row.RowIndex)("Nivel2"))

                If (lvl2 > 0) Then
                    articulo = "         - " & articulo
                Else

                    Dim lvl1 = CInt(GridTreeView.DataKeys(e.Row.RowIndex)("Nivel1"))
                    If (lvl1 > 0) Then
                        articulo = "    - " & articulo
                    End If
                End If
            End If

            e.Row.Cells(1).Text = articulo

        End If


    End Sub

    Protected Sub btnEspanol_Click(sender As Object, e As EventArgs) Handles btnEspanol.Click

        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType, "script", "PrintPanel();", True)
    End Sub

    Protected Sub btnEnglish_Click(sender As Object, e As EventArgs) Handles btnEnglish.Click
        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType, "script", "PrintPanel3();", True)
    End Sub
End Class