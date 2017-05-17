<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Roles.aspx.vb" Inherits="CoflexWeb.Roles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="well well-lg" style="margin-top:32px">
        <div class="divider" style="margin-top:16px;margin-bottom:16px"></div>
        <asp:GridView ID="GridRoles" class="table"  runat="server"></asp:GridView>
    </div>
    <div id="response" runat="server"></div>
</asp:Content>
