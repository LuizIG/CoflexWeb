<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ResumenEstimaciones.aspx.vb" Inherits="CoflexWeb.ResumenEstimaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../Content/bootstrap-datepicker.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-datepicker.js"></script>
    <script type="text/javascript">


        $(document).ready(function () {
            $('#MainContent_tableQuotations tr').on('click', function (event) {
                var customerId = $(this).find("td").eq(1).attr("id");
                var qv = customerId.split(",");
                window.location.href = "Estimacion.aspx?q=" + qv[0] + "&v=" + qv[1];
            });

            $('.input-daterange').datepicker({
                format: "dd/mm/yyyy",

            }).on("hide", function (e) {

                $('#table > tbody  > tr').each(function () {

                    var dateInRowStr = $(this).find('td:eq(6)').text();
                    var dateInRow = new Date(dateInRowStr.split("/")[2], dateInRowStr.split("/")[1] - 1, dateInRowStr.split("/")[0]);

                    var dateInitStr = $("#init_date").val();
                    var dateInit = new Date(dateInitStr.split("/")[2], dateInitStr.split("/")[1] - 1, dateInitStr.split("/")[0]);

                    var dateLastStr = $("#last_date").val();
                    var dateLast = new Date(dateLastStr.split("/")[2], dateLastStr.split("/")[1] - 1, dateLastStr.split("/")[0]);


                    if (dateInRow >= dateInit && dateInRow <= dateLast) {
                        $(this).show();
                    } else {
                        $(this).hide();
                    }
                });
            });;
        });

         function PrintPanel3() {
                var panel = document.getElementById("<%=pnlContents3.ClientID %>");
                var printWindow = window.open('', '', 'height=400,width=800');
                printWindow.document.write('<html><head><title>DIV Contents</title>');
                printWindow.document.write('</head><body >');
                printWindow.document.write(panel.innerHTML);
                printWindow.document.write('</body></html>');
                printWindow.document.close();
                setTimeout(function () {
                    printWindow.print();
                }, 500);
                return false;
         }

    </script>



    <div style="position: relative; right: 0px; width: 100%; text-align: right; margin-top: 32px">
        <asp:Button ID="ButtonIndicadores" class="btn btn-primary" runat="server" Text="Indicadores" />&nbsp;
        <asp:Button ID="ButtonPrintEstim" class="btn btn-primary hidden-print" OnClientClick="return PrintPanel3();" runat="server" Text="Imprimir" />&nbsp;
        <%--<a href="Estimacion.aspx" class="btn btn-primary" role="button">Nueva Estimacion</a>--%>
        <asp:Button ID="ButtonReasignar" class="btn btn-primary" runat="server" Text="Reasignar" />&nbsp;
        <asp:Button ID="ButtonEstimacionGo" class="btn btn-primary" runat="server" Text="Nueva Estimacion" />
    </div>
    <%--<div class="divider" style="margin-top: 16px; margin-bottom: 16px"></div>--%>

    <div style="width:400px;" class="input-daterange input-group" id="datepicker">
        <input id="init_date" type="text" class="input-sm form-control" name="start" />
        <span class="input-group-addon">a</span>
        <input id="last_date" type="text" class="input-sm form-control" name="end" />
    </div>

    <asp:Panel ID="pnlContents3" runat="server">
        <table style="width: 100%">
            <tr style="text-align: right">
                <td colspan="2" width="50%">
                    <img src="logo.png" class="visible-print-block" />&nbsp;
                </td>
            </tr>
        </table>
        <table id="table"
            data-toggle="table"
            data-filter-control="true"
            data-click-to-select="false"
            data-toolbar="#toolbar">
            <thead style="background-color: #C0C0C0">
                <tr>
                    <th data-field="state" data-checkbox="true"></th>
                    <th data-field="quotation" data-filter-control="input" data-sortable="true">Cotización</th>
                    <th data-field="vendor" data-filter-control="select" data-sortable="true">Vendedor</th>
                    <th data-field="client" data-filter-control="select" data-sortable="true">Cliente</th>
                    <th data-field="status" data-filter-control="select" data-sortable="true">Estatus Cotización</th>
                    <th data-field="version" data-filter-control="select" data-sortable="true">Versión</th>
                    <th data-field="date" data-filter-control="select" data-sortable="true">Fecha</th>
                    <th data-field="qversionstatus" data-filter-control="select" data-sortable="true">Estatus Versión</th>
                    <%--<th data-field="action" data-sortable="false">Acción</th>--%>
                </tr>
            </thead>
            <tbody id="tableQuotations" runat="server"></tbody>
        </table>
    </asp:Panel>
    <div id="div_response" runat="server"></div>
</asp:Content>
