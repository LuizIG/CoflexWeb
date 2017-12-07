<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Excepciones.aspx.vb" Inherits="CoflexWeb.Excepciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {
            bindQuery();
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            bindQuery();
        });

        function bindQuery() {
            $(".deleteUser").click(function () {
                var idUser = $(this).attr("idException");
                $("#MainContent_id_user").val(idUser);
                var elm = document.getElementById('<%=btn_delete.ClientID%>');
            elm.click();
                    });
    }



    </script>
    <div class="well well-lg" style="margin-top: 32px">

        <div id="div_error_reorder_grid" runat="server" style="display: none; margin: auto; width:100% !important" class="alert alert-danger alert-dismissable fade in">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            <div runat="server" id="div_error_reorder_grid_desc"></div>
        </div>

        <div style="margin: auto; margin-top:8px">

            <div style="display: inline-block">
                <label>Clientes no OEM</label>
                <br />
                <asp:DropDownList runat="server" ID="available_clients"></asp:DropDownList>
            </div>

            <div style="display: inline-block; margin-left: 8px">
                <button style="margin-bottom: 15px" runat="server" id="btn_agregar" class="btn btn-success">Agregar</button></div>


        </div>
        <div class="divider" style="margin-top: 16px; margin-bottom: 16px"></div>
        <h4>Excepciones</h4>
        <asp:GridView ID="GridAttachment" AutoGenerateColumns="False" class="table" runat="server" DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="ClientNotOEM" HeaderText="Id" />
                <asp:BoundField DataField="ClientName" HeaderText="Nombre" />
                <asp:BoundField ItemStyle-Width="10%" DataField="Id" HeaderText="" HtmlEncode="False" DataFormatString="<a idException='{0}' class='btn btn-danger deleteUser' role='button' href='#'>Eliminar</a>" />
            </Columns>
            <HeaderStyle BackColor="#C0C0C0" />
        </asp:GridView>
    </div>

    <asp:HiddenField ID="id_user" runat="server" />
    <div style="visibility: hidden">
        <asp:Button runat="server" ID="btn_delete" Style="width: 0px; height: 0px" />
    </div>

</asp:Content>
