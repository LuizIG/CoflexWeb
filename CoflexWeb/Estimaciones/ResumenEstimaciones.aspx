<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ResumenEstimaciones.aspx.vb" Inherits="CoflexWeb.ResumenEstimaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div style="position: relative; right: 0px; width: 100%; text-align: right; margin-top:32px">
        <a href="Estimacion.aspx" class="btn btn-primary" role="button">Nueva Estimacion</a>
    </div>
    <div class="divider" style="margin-top: 16px; margin-bottom: 16px"></div>


    <table id="tableQuotations" runat="server" 
			     data-toggle="table"
			     data-search="true"
			     data-filter-control="true" 
			     data-click-to-select="true"
			     data-toolbar="#toolbar">
	    <thead style="background-color:#C0C0C0">
		    <tr>
			    <th data-field="quotation" data-filter-control="input" data-sortable="true">Cotización</th>
			    <th data-field="vendor" data-filter-control="select" data-sortable="true">Vendedor</th>
			    <th data-field="client" data-filter-control="select" data-sortable="true">Cliente</th>
                <th data-field="status" data-filter-control="select" data-sortable="true">Estatus Cotización</th>
                <th data-field="version" data-filter-control="select" data-sortable="true">Versión</th>
                <th data-field="date" data-filter-control="select" data-sortable="true">Fecha</th>
                <th data-field="qversionstatus" data-filter-control="select" data-sortable="true">Estatus Versión</th>
                <th data-field="action" data-sortable="false"></th>
		    </tr>
	    </thead>
    </table>

    <div id="div_response" runat="server"></div>
</asp:Content>
