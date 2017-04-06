<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Usuarios.aspx.vb" Inherits="CoflexWeb.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".deleteUser").click(function () {
                var idUser = $(this).attr("idUser");
                $("#MainContent_id_user").val(idUser);
                var elm = document.getElementById('<%=btn_delete.ClientID%>');
                elm.click();
            });
            $(".activateUser").click(function () {
                var idUser = $(this).attr("idUser");
                $("#MainContent_id_user").val(idUser);
                var elm = document.getElementById('<%=btn_activate.ClientID%>');
                elm.click();
            });
        });
    </script>
    <div class="well well-lg" style="margin-top: 32px">
        <div style="position: relative; right: 0px; width: 100%; text-align: right;">
            <a href="Registro.aspx" class="btn btn-primary" role="button">Nuevo Usuario</a>
        </div>
        <div class="divider" style="margin-top: 16px; margin-bottom: 16px"></div>
        <asp:GridView ID="GridUsers" AutoGenerateColumns="False" class="table" runat="server">
            <Columns>
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Name" HeaderText="Nombre" />
                <asp:BoundField DataField="PaternalSurname" HeaderText="Apellido paterno" />
                <asp:BoundField DataField="MaternalSurname" HeaderText="Apellido Materno" />
                <asp:BoundField DataField="RolesString" HtmlEncode="False" HeaderText="Roles" />
                <asp:BoundField DataField="Id" HeaderText="" HtmlEncode="False" DataFormatString="<a class='btn btn-primary' role='button' href='PerfilUsuario.aspx?id={0}'>Editar</a>" />
                <asp:BoundField DataField="ActionButton" HeaderText="" HtmlEncode="False" />
             </Columns>
            <HeaderStyle BackColor="#C0C0C0" />
        </asp:GridView>
    </div>
    <div id="response" runat="server"></div>
    <asp:HiddenField ID="id_user" runat="server" />
    <div style="visibility:hidden">
        <asp:Button runat="server" ID="btn_delete" style="width:0px;height:0px" />
        <asp:Button runat="server" ID="btn_activate" style="width:0px;height:0px" />
    </div>
</asp:Content>
