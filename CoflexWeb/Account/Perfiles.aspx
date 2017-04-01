<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Perfiles.aspx.vb" Inherits="CoflexWeb.Perfiles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%--<%: Title %>.--%></h2>
    <table>
        <tr>
            <td style="width: 50px"><b>Id Rol</b></td>
            <td style="width: 109px"><b>Descripción</b></td>
        </tr>
          <tr>
            <td>1</td>
            <td style="width: 109px">Administrador</td>
        </tr>
          <tr>
            <td>2</td>
            <td style="width: 109px">Gerente</td>
        </tr>
          </tr>
          <tr>
            <td>3</td>
            <td style="width: 109px">Gerente de ventas</td>
        </tr>
    </table>
</asp:Content>
