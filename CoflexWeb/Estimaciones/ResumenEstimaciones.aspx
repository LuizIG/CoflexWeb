<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ResumenEstimaciones.aspx.vb" Inherits="CoflexWeb.ResumenEstimaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        var $table = $('#mainTable');

        //$table.bootstrapTable();
        alert("alerta");

    </script>

    <div style="position: relative; right: 0px; width: 100%; text-align: right; margin-top:32px">
        <a href="Estimacion.aspx" class="btn btn-primary" role="button">Nueva Estimacion</a>
    </div>
    <div class="divider" style="margin-top: 16px; margin-bottom: 16px"></div>

<table id="table" 
			 data-toggle="table"
			 data-search="true"
			 data-filter-control="true" 
			 data-show-export="true"
			 data-click-to-select="true"
			 data-toolbar="#toolbar">
	<thead>
		<tr>
			<th data-field="state" data-checkbox="true"></th>
			<th data-field="prenom" data-filter-control="input" data-sortable="true">Prénom</th>
			<th data-field="date" data-filter-control="select" data-sortable="true">Date</th>
			<th data-field="examen" data-filter-control="select" data-sortable="true">Examen</th>
			<th data-field="note" data-sortable="true">Note</th>
		</tr>
	</thead>
	<tbody>
		<tr>
			<td class="bs-checkbox "><input data-index="0" name="btSelectItem" type="checkbox"></td>
			<td>Valérie</td>
			<td>01/09/2015</td>
			<td>Français</td>
			<td>12/20</td>
		</tr>
		<tr>
			<td class="bs-checkbox "><input data-index="1" name="btSelectItem" type="checkbox"></td>
			<td>Eric</td>
			<td>05/09/2015</td>
			<td>Philosophie</td>
			<td>8/20</td>
		</tr>
		<tr>
			<td class="bs-checkbox "><input data-index="2" name="btSelectItem" type="checkbox"></td>
			<td>Valentin</td>
			<td>05/09/2015</td>
			<td>Philosophie</td>
			<td>4/20</td>
		</tr>
		<tr>
			<td class="bs-checkbox "><input data-index="3" name="btSelectItem" type="checkbox"></td>
			<td>Valérie</td>
			<td>05/09/2015</td>
			<td>Philosophie</td>
			<td>10/20</td>
		</tr>
		<tr>
			<td class="bs-checkbox "><input data-index="4" name="btSelectItem" type="checkbox"></td>
			<td>Eric</td>
			<td>01/09/2015</td>
			<td>Français</td>
			<td>14/20</td>
		</tr>
		<tr>
			<td class="bs-checkbox "><input data-index="5" name="btSelectItem" type="checkbox"></td>
			<td>Valérie</td>
			<td>07/09/2015</td>
			<td>Mathématiques</td>
			<td>19/20</td>
		</tr>
		<tr>
			<td class="bs-checkbox "><input data-index="6" name="btSelectItem" type="checkbox"></td>
			<td>Valentin</td>
			<td>01/09/2015</td>
			<td>Français</td>
			<td>11/20</td>
		</tr>
		<tr>
			<td class="bs-checkbox "><input data-index="7" name="btSelectItem" type="checkbox"></td>
			<td>Eric</td>
			<td>01/10/2015</td>
			<td>Philosophie</td>
			<td>8/20</td>
		</tr>
		<tr>
			<td class="bs-checkbox "><input data-index="8" name="btSelectItem" type="checkbox"></td>
			<td>Valentin</td>
			<td>07/09/2015</td>
			<td>Mathématiques</td>
			<td>14/20</td>
		</tr>
		<tr>
			<td class="bs-checkbox "><input data-index="9" name="btSelectItem" type="checkbox"></td>
			<td>Valérie</td>
			<td>01/10/2015</td>
			<td>Philosophie</td>
			<td>12/20</td>
		</tr>
		<tr>
			<td class="bs-checkbox "><input data-index="10" name="btSelectItem" type="checkbox"></td>
			<td>Eric</td>
			<td>07/09/2015</td>
			<td>Mathématiques</td>
			<td>14/20</td>
		</tr>
		<tr>
		<td class="bs-checkbox "><input data-index="11" name="btSelectItem" type="checkbox"></td>
			<td>Valentin</td>
			<td>01/10/2015</td>
			<td>Philosophie</td>
			<td>10/20</td>
		</tr>
	</tbody>
</table>

    <asp:GridView ID="GridUsers" AutoGenerateColumns="False" class="table" 			 
             data-toggle="table"
			 data-search="true"
			 data-filter-control="true" 
			 data-show-export="true" runat="server">
        <Columns>
            <asp:BoundField DataField="CoflexId" HeaderText="Cotización"  />
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
