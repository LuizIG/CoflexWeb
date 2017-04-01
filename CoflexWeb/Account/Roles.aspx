<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Roles.aspx.vb" Inherits="CoflexWeb.Roles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="response" runat="server"></div>

    <asp:GridView ID="GridRoles" runat="server">
        <Columns>
            <asp:BoundField HeaderText="Id" Visible="False" />
            <asp:BoundField HeaderText="Rol" />
        </Columns>
    </asp:GridView>

</asp:Content>
