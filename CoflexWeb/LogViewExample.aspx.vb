Public Class LogViewExample
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim newNode As TreeNode = New TreeNode("132")
        '' Me.TreeView1.SelectedNode.Nodes.Add(newNode)
        Me.TreeView1.SelectedNode.ChildNodes.Add(newNode)
    End Sub
End Class