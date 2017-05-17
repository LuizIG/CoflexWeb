<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ResumenEstimaciones.aspx.vb" Inherits="CoflexWeb.ResumenEstimaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../Content/bootstrap-datepicker.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-datepicker.js"></script>
    <script type="text/javascript">


        var prm = Sys.WebForms.PageRequestManager.getInstance();

        function bindQuery() {
            $('#MainContent_GridQuotations tr td').on('click', function (event) {
                if ($(this).index() == 0) {

                } else {
                    var customerId = $(this).parent().attr("id");
                    var qv = customerId.split(",");
                    window.location.href = "Estimacion.aspx?q=" + qv[0] + "&v=" + qv[1];
                }
            });

            $('.input-daterange').datepicker({
                format: "dd/mm/yyyy"
            });


            $("#btn_reasignar").on("click", function () {
                var idQuotations = [];
                alert("reasignar");
                var x = 0;
                $('#MainContent_GridQuotations > tbody  > tr').each(function () {

                    if ($(this).find('td:eq(0) input').is(':checked')) {
                        alert("isCheched");

                        idQuotations[x] = $(this).attr("id").split(",")[0];
                        x++;
                    }
                });

                if (idQuotations.length > 0) {
                    $("#MainContent_quotations_reasign").val(idQuotations);
                    $("#myModal").modal();
                } else {
                    $("#error_container").css("display", "block");
                    $("#error_description").text("Selecciona la (s) cotizaciones que vas a reasignar.");
                }
            });
        }

        prm.add_endRequest(function () {
            // re-bind your jQuery events here
            bindQuery();
        });

        $(document).ready(function () {
            bindQuery();
        });

        function PrintPanel3() {
            var panel = document.getElementById("<%=pnlContents3.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title></title>');
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
    <h2><%--<%: Title %>.--%></h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <%--<div style="text-align: center; margin-top: 16px;">
            </div>--%>
            <div style="text-align: center; height: 8px; margin-top: 16px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class='progress progress-striped active' style='height: 8px;'>
                            <div class='progress-bar' role='progressbar' aria-valuenow='100' aria-valuemin='0' aria-valuemax='100' style='width: 100%'></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>

            <div id="error_container" style="display: none" class="alert alert-danger alert-dismissable fade in">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong>Ups!</strong>
                <div id="error_description"></div>
            </div>

            <div id="div1" runat="server"></div>

            <div class="well well-lg">
                <table style="border-style: solid; border-color: #C0C0C0; width: 100%;">
                    <tr style="vertical-align: top; background-color: #C0C0C0;">
                                <td style="width: 100%; text-align: left; vertical-align: top;" colspan="8">
                                    <b>&nbsp;<asp:Label ID="Label30" runat="server" Text="Filtro"></asp:Label>
                                    </b>&nbsp;
                                </td>
                                
                            </tr>
                    <tr style="vertical-align: top;">
                        <td style="width: 72px;">
                            <asp:Label ID="Label11" runat="server" Text="Cotización"></asp:Label>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtCotizacion" Style="width: 85px; height:90%" runat="server"></asp:TextBox>&nbsp;
                        </td>

                        <td style="width: 72px;">
                            <asp:Label ID="Label1" runat="server" Text="Vendedor"></asp:Label>&nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="DDVendedor" Style="width: 200px;" runat="server"></asp:DropDownList>&nbsp;

                        </td>
                        <td style="width: 110px;">
                            <asp:Label ID="Label2" runat="server" Text="Cliente"></asp:Label>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtCliente" Style="width: 140px; height:90%" runat="server"></asp:TextBox>&nbsp;
                        </td>
                        <td style="width: 120px;">&nbsp;</td>
                        <td>&nbsp;</td>

                    </tr>
                    <tr>
                        <td style="width: 72px;">
                            <asp:Label ID="Label4" runat="server" Text="Versión"></asp:Label>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtVersion" Style="width: 85px; height:90%" runat="server"></asp:TextBox>&nbsp;
                        </td>
                        <td>
                            <div class="input-daterange input-group" id="datepicker">
                                <asp:Label ID="Label5" runat="server" Text="Fecha"></asp:Label>
                            &nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" Style="width: 87px; height:90%" runat="server"></asp:TextBox>&nbsp;
                                <asp:Label ID="Label7" runat="server" Text="a"></asp:Label>&nbsp;
                                <asp:TextBox ID="TextBox2" Style="width: 87px; height:90%" runat="server"></asp:TextBox>
                            &nbsp;
            <%--</div>--%>

                        </td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Estatus versión"></asp:Label>&nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="DDStatusVersion" Style="width: 140px;" runat="server"></asp:DropDownList>&nbsp;

                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Estatus cotización"></asp:Label>&nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="DDStatusCotiza" Style="width: 100px;" runat="server"></asp:DropDownList>&nbsp;
                        </td>
                    </tr>

                </table>

                <div style="position: relative; right: 0px; width: 100%; text-align: right; margin-top: 16px">
                    <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="Filtrar" />
                    <asp:Button ID="ButtonIndicadores" class="btn btn-primary" runat="server" Text="Indicadores" />&nbsp;
                    <asp:Button ID="ButtonPrintEstim" class="btn btn-primary hidden-print" OnClientClick="return PrintPanel3();" runat="server" Text="Imprimir" />&nbsp;
                    <%--<a href="Estimacion.aspx" class="btn btn-primary" role="button">Nueva Estimacion</a>--%>
                    <a id="btn_reasignar" data-role="button" class="btn btn-primary">Reasignar</a>
                    <%--<asp:Button ID="ButtonReasignar" class="btn btn-primary" runat="server" Text="Reasignar" />&nbsp;--%>
                    <asp:Button ID="ButtonEstimacionGo" class="btn btn-primary" runat="server" Text="Nueva cotización" />
                </div>


                <asp:Panel ID="pnlContents3" runat="server">
                    <table style="width: 100%">
                        <tr class="visible-print-block" style="text-align: left">
                            <td colspan="4" width="50%">
                                <img src="logo.png" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">&nbsp;</td>
                        </tr>
                    </table>
                    <asp:GridView ID="GridQuotations" AutoGenerateColumns="False" class="table" runat="server">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox name="chGBQuot" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                            <asp:BoundField DataField="IdVersion" HeaderText="IdVersion" Visible="false" />
                            <asp:BoundField DataField="CoflexId" HeaderText="Cotización" />
                            <asp:BoundField DataField="vendor" HeaderText="Vendedor" />
                            <asp:BoundField DataField="clientName" HeaderText="Cliente" />
                            <asp:BoundField DataField="QuotationStatusName" HeaderText="Estatus cotización" />
                            <asp:BoundField DataField="versionNumber" HeaderText="Versión" />
                            <asp:BoundField DataField="date" HeaderText="Fecha" />
                            <asp:BoundField DataField="QuotationVersionStatusName" HeaderText="Estatus versión" />
                        </Columns>
                        <HeaderStyle BackColor="#C0C0C0" />
                    </asp:GridView>
                </asp:Panel>

            </div>


            <div id="div2" runat="server"></div>

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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
