<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ResumenEstimaciones.aspx.vb" Inherits="CoflexWeb.ResumenEstimaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="position: relative; right: 0px; width: 100%; text-align: right;">
        <a href="Estimacion.aspx" class="btn btn-primary" role="button">Nueva Estimacion</a>
    </div>
    <div class="divider" style="margin-top: 16px; margin-bottom: 16px"></div>

    <asp:TreeView ID="TreeViewQuotation" Width="100%" ShowCheckBoxes="All" ShowExpandCollapse="true" runat="server" ImageSet="Simple">
        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
        <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
        <ParentNodeStyle Font-Bold="False" />
        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
        <DataBindings>
            <asp:TreeNodeBinding DataMember="name" TextField="name" />
            <asp:TreeNodeBinding DataMember="name" TextField="name" />
            <asp:TreeNodeBinding DataMember="name" TextField="name" />
            <asp:TreeNodeBinding DataMember="name" TextField="name" />
        </DataBindings>
    </asp:TreeView>

    <div id="div_response" runat="server"></div>
</asp:Content>
