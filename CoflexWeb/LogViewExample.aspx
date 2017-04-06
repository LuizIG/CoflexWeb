<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LogViewExample.aspx.vb" Inherits="CoflexWeb.LogViewExample" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Login ID="Login1" runat="server"></asp:Login>
    </div>

        <asp:TreeView ID="TreeView1" runat="server"></asp:TreeView>

        <asp:Button ID="Button1" runat="server" Text="Button" />
    </form>
</body>
</html>
