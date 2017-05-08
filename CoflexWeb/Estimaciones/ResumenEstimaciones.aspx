<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ResumenEstimaciones.aspx.vb" Inherits="CoflexWeb.ResumenEstimaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../Content/bootstrap-datepicker.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-datepicker.js"></script>
    <script type="text/javascript">


        $(document).ready(function () {
            $('#MainContent_tableQuotations tr td').on('click', function (event) {
                if ($(this).index() == 0) {

                } else {
                    var customerId = $(this).parent().find("td").eq(1).attr("id");
                    var qv = customerId.split(",");
                    window.location.href = "Estimacion.aspx?q=" + qv[0] + "&v=" + qv[1];
                }
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


            $("#btn_reasignar").on("click", function () {
                var idQuotations = [];
                var x = 0;
                $('#table > tbody  > tr').each(function () {
                    if ($(this).find('td:eq(0) input').is(':checked')) {
                        idQuotations[x] = $(this).find('td:eq(1)').attr("id").split(",")[0];
                        x++;
                    }
                });

                if (idQuotations.length > 0) {
                    $("#MainContent_quotations_reasign").val(idQuotations);
                    $("#myModal").modal();
                } else {
                    alert("no");
                    $("#error_container").css("display", "block");
                    $("#error_description").text("Selecciona la (s) cotizaciones que vas a reasignar.");
                }
            });

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

    <div style="text-align: center; height: 16px; margin-top: 16px;">
      
    </div>
    <div id="div1"  runat="server"></div>

       <div id="error_container" style="display:none" class="alert alert-danger alert-dismissable fade in">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            <strong>Ups!</strong> <div id="error_description"></div>
      </div>

    <div class="well well-lg">


        <div style="position: relative; right: 0px; width: 100%; text-align: right; margin-top: 32px">
            <asp:Button ID="ButtonIndicadores" class="btn btn-primary" runat="server" Text="Indicadores" />&nbsp;

            <a id="btn_reasignar" data-role="button" class="btn btn-primary">Reasignar</a>
            <asp:Button ID="ButtonEstimacionGo" class="btn btn-primary" runat="server" Text="Nueva cotización" />
        </div>

        <div style="width: 400px;" class="input-daterange input-group" id="datepicker">
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
                    </tr>
                </thead>
                <tbody id="tableQuotations" runat="server"></tbody>
            </table>
        </asp:Panel>
        <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Reasignar</h4>
                    </div>
                    <div class="modal-body">
                        <p>Selecciona un vendedor</p>
                        <asp:DropDownList ID="DDUsers" runat="server"></asp:DropDownList>
                    </div>
                    <div class="modal-footer">
                        <asp:Button Text="Cancelar" runat="server" type="button" class="btn btn-danger" data-dismiss="modal" />
                        <asp:Button ID="BTN_ACEPTAR_1" Text="Aceptar" runat="server" type="button" class="btn btn-success" />
                    </div>
                </div>

            </div>
        </div>
        <input type="hidden" id="quotations_reasign" runat="server" />
        <div id="div_response" runat="server"></div>
    </div>

</asp:Content>
