<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ResumenEstimaciones.aspx.vb" Inherits="CoflexWeb.ResumenEstimaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="position: relative; right: 0px; width: 100%; text-align: right; margin-top:32px">
        <a href="Estimacion.aspx" class="btn btn-primary" role="button">Nueva Estimacion</a>
    </div>
    <div class="divider" style="margin-top: 16px; margin-bottom: 16px"></div>

    <asp:GridView ID="GridUsers" AutoGenerateColumns="False" class="table" runat="server">
        <Columns>
            <asp:BoundField DataField="QuotationsId" HeaderText="Cotización" />
            <asp:BoundField DataField="User" HeaderText="Vendedor" />
            <asp:BoundField DataField="ClientName" HeaderText="Cliente" />
            <asp:BoundField DataField="QStatus" HeaderText="Estatus Cotización" />
            <asp:BoundField DataField="VersionNumber" HeaderText="Versión" />
            <asp:BoundField DataField="Date" HtmlEncode="False" HeaderText="Fecha" />
            <asp:BoundField DataField="VStatus" HtmlEncode="False" HeaderText="Estatus Versión" />
            <asp:BoundField DataField="ActionEdit" HeaderText="" HtmlEncode="False" />

            </Columns>
        <HeaderStyle BackColor="#C0C0C0" />
    </asp:GridView>

    <div id="div_response" runat="server"></div>
</asp:Content>
