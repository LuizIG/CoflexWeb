<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Usuarios.aspx.vb" Inherits="CoflexWeb.Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="well well-lg" style="margin-top:32px">
        <asp:TextBox ID="RoleName" runat="server"  placeholder="Nombre"  />
        <asp:Button ID="BtnSend" runat="server"  text="Enviar"  />
        <div class="divider" style="margin-top:16px;margin-bottom:16px"></div>
        <asp:GridView ID="GridUsers" class="table"  runat="server"></asp:GridView>
    </div>
    <div id="response" runat="server"></div>
</asp:Content>
