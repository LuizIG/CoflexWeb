<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Usuarios.aspx.vb" Inherits="CoflexWeb.Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="well well-lg" style="margin-top:32px">
        <div style="position: relative; right: 0px; width:100%; text-align: right;">
            <a href="Registro.aspx" class="btn btn-primary" role="button">Nuevo Usuario</a>
        </div>
        <div class="divider" style="margin-top:16px;margin-bottom:16px"></div>
        <asp:GridView ID="GridUsers" AutoGenerateColumns="False" class="table" runat="server">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id"  HtmlEncode="False" DataFormatString="<a href='PerfilUsuario.aspx?id={0}'>{0}</a>"  />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Name" HeaderText="Nombre" />
                <asp:BoundField DataField="PaternalSurname" HeaderText="Apellido paterno" />
                <asp:BoundField DataField="MaternalSurname" HeaderText="Apellido Materno" />
                <asp:BoundField DataField="RolesString"  HtmlEncode="False" HeaderText="Roles" />            
            </Columns>
            <HeaderStyle BackColor="#C0C0C0" />
        </asp:GridView>
    </div>
    <div id="response" runat="server"></div>
</asp:Content>
