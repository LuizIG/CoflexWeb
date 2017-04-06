Public Class Estimacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.MultiView1.ActiveViewIndex = 1
    End Sub

    Protected Sub Regresar_Click(sender As Object, e As EventArgs) Handles Regresar.Click
        Me.MultiView1.ActiveViewIndex = 0

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ''Me.TreeView1.Nodes(0).nodes.Add(Me.TextBox12.Text)
        Dim newNode As TreeNode = New TreeNode(Me.TextBox12.Text)
        ''Me.TreeView1.SelectedNode.Nodes.Add(newNode)
        Me.TreeView1.SelectedNode.ChildNodes.Add(newNode)
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim newNode As TreeNode = New TreeNode("1-AG-S90-RH : COFLEX GAS ACERO")
        TreeView1.Nodes.Remove(newNode)
        ' Clears all nodes.
        TreeView1.Nodes.Clear()
    End Sub
End Class