<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Usuarios.aspx.vb" Inherits="CoflexWeb.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">


        $(document).ready(function () {
            $(".deleteUser").click(function () {
                var idUser = $(this).attr("idUser");
                $("#MainContent_id_delete").val(idUser);
                __doPostBack('MainContent_btn_delete', '');
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
                <asp:BoundField DataField="Id" HeaderText="" HtmlEncode="False" DataFormatString="<a idUser='{0}' class='btn btn-danger deleteUser' role='button'>Eliminar</a>" />
            </Columns>
            <HeaderStyle BackColor="#C0C0C0" />
        </asp:GridView>
    </div>
    <!-- Modal -->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Modal Header</h4>
                </div>
                <div class="modal-body">
                    <p>Some text in the modal.</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="response" runat="server"></div>
    <asp:HiddenField ID="id_delete" runat="server" />
    <asp:Button runat="server" ID="btn_delete" Visible="false" />
</asp:Content>
