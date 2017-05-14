<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Estimacion.aspx.vb" Inherits="CoflexWeb.Estimacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript" type="text/javascript">
        function CheckNumeric(e) {

            if (window.event) // IE 
            {
                if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8) {
                    event.returnValue = false;
                    return false;

                }
            }
            else { // Fire Fox
                if ((e.which < 48 || e.which > 57) & e.which != 8) {
                    e.preventDefault();
                    return false;

                }
            }
        }

        var ddlText, ddlValue, ddl;
        function CacheItems() {
            ddlText = new Array();
            ddlValue = new Array();
            ddl = document.getElementById("<%=DDComponente.ClientID %>");
            for (var i = 0; i < ddl.options.length; i++) {
                console.log(ddl.options[i].text + " " + ddl.options[i].value);
                ddlText[ddlText.length] = ddl.options[i].text;
                ddlValue[ddlValue.length] = ddl.options[i].value;
            }
        }
        window.onload = CacheItems;

        function FilterItems(value) {
            ddl.options.length = 0;
            for (var i = 0; i < ddlText.length; i++) {
                if (ddlText[i].toLowerCase().indexOf(value) != -1) {
                    AddItem(ddlText[i], ddlValue[i]);
                }
            }
        }

        function AddItem(text, value) {
            var opt = document.createElement("option");
            opt.text = text;
            opt.value = value;
            ddl.options.add(opt);
        }


        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
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

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            // re-bind your jQuery events here
            ddl = document.getElementById("<%=DDComponente.ClientID %>");
        });
            function PrintPanel2() {
                var panel = document.getElementById("<%=pnlContents2.ClientID %>");
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

            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="View1" runat="server">
                    <div style="text-align: center; height: 8px; margin-top: 16px;">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DynamicLayout="true">
                            <ProgressTemplate>
                                <div class='progress progress-striped active' style='height: 8px;'>
                                    <div class='progress-bar' role='progressbar' aria-valuenow='100' aria-valuemin='0' aria-valuemax='100' style='width: 100%'></div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                    <div id="div_Response" runat="server"></div>

                    <div class="well well-lg">

                        <table style="border-style: solid; border-color: #C0C0C0; width: 100%; align-items: center">
                            <tr style="vertical-align: top; background-color: #C0C0C0;">
                                <td style="width: 50%; text-align: left; vertical-align: top;">
                                    <b>&nbsp;<asp:Label ID="Label30" runat="server" Text="Cliente"></asp:Label>
                                    </b>&nbsp;
                                </td>
                                <td colspan="2" style="width: 50%; text-align: left; vertical-align: top;">
                                    <b>&nbsp;<asp:Label ID="Label31" runat="server" Text="Prospecto"></asp:Label>
                                    </b>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%">&nbsp;<asp:DropDownList ID="DDClienteCotiza" Width="100%" runat="server" AutoPostBack="True">
                                </asp:DropDownList>&nbsp;
                                </td>
                                <td style="width: 40%">&nbsp;<asp:DropDownList ID="DDProspecto" Enabled="false" Width="100%" runat="server" AutoPostBack="True">
                                </asp:DropDownList>&nbsp;
                                </td>
                                <td style="width: 10%; text-align: center">
                                    <asp:Button ID="BtnNuevoPros" runat="server" class="btn btn-primary" Text="Nuevo" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table style="border-style: solid; border-color: #C0C0C0; width: 100%; align-items: center">
                            <tr style="vertical-align: top;">
                                <td style="width: 30%; text-align: center; vertical-align: top;">
                                    <div style="width: 100%; vertical-align: top;">
                                        <table style="width: 100%;">
                                            <tr style="background-color: #C0C0C0">
                                                <td colspan="3" style="text-align: left;"><b>&nbsp;<asp:Label ID="Label20" runat="server" Text="Artículo"></asp:Label>
                                                </b>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:left">
                                                    <asp:Label ID="Label9" runat="server" Text="Cliente"></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDCliente" Width="90%" runat="server" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                                <%-- <td rowspan="2" style="vertical-align: top;">
                                                    <asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="Agregar" />
                                                </td>--%>
                                            </tr>
                                            <tr>
                                                <td  style="text-align:left">
                                                    <asp:Label ID="Label8" runat="server" Text="Artículo"></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDArticulo" Width="90%" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>

                                        </table>
                                    </div>
                                </td>
                                <td style="width: 30%; text-align: center; vertical-align: top;">
                                    <div style="width: 100%; vertical-align: top;">
                                        <table style="width: 100%;">
                                            <tr style="background-color: #C0C0C0">
                                                <td colspan="3" style="text-align: left;"><b>&nbsp;<asp:Label ID="Label21" runat="server" Text="Componentes"></asp:Label>
                                                </b>&nbsp;</td>
                                            </tr>
                                            <tr style="text-align:left">
                                                <td><asp:Label ID="Label35" Width="90%" runat="server" Text="Filtro"></asp:Label>&nbsp;</td>
                                                <td>
                                                    <asp:TextBox Width="90%" ID="txtSearch" runat="server" onkeyup="FilterItems(this.value)"></asp:TextBox>
                                                </td>
                                                <%-- <td rowspan="2" style="vertical-align: central;">
                                                    <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="Agregar" />
                                                </td>--%>
                                            </tr>
                                            <tr>
                                                <td  style="text-align:left">
                                                    <asp:Label ID="Label11" Width="90%" runat="server" Text="Componente"></asp:Label>&nbsp;
                                                </td>
                                                <td  style="text-align:left">
                                                    <asp:DropDownList Width="90%" ID="DDComponente" runat="server">
                                                    </asp:DropDownList>&nbsp;
                                                </td>

                                            </tr>

                                        </table>
                                    </div>
                                </td>
                                <td style="border-right-style: solid; border-color: #C0C0C0; width: 30%; text-align: center; vertical-align: top">
                                    <div style="width: 100%; vertical-align: top;">
                                        <table style="width: 100%; vertical-align: top;">
                                            <tr style="background-color: #C0C0C0">
                                                <td colspan="4" style="text-align: left;"><b>&nbsp;<asp:Label ID="Label22" runat="server" Text="Elemento"></asp:Label>
                                                </b>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td  style="text-align:left">
                                                    <asp:Label ID="Label12" runat="server" Text="Elemento"></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDElemento" Width="90%" runat="server">
                                                    </asp:DropDownList>&nbsp;
                                                </td>
                                                <td style="vertical-align: central;">
                                                    <asp:Button ID="Button6" runat="server" class="btn btn-primary" Text="Nuevo" />&nbsp;

                                                </td>
                                            </tr>

                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <br />

                        <table style="width: 100%">
                            <tr>
                                <td>&nbsp;</td>
                                <td>
                                    <div style="float: right;">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="ButtonAllAgregar" runat="server" class="btn btn-primary" Text="Agregar" />&nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="Actualizar" />&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Button ID="Button4" runat="server" class="btn btn-primary" Text="Remover" />&nbsp;
                                                </td>

                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div>
                            <table class="nav-justified">
                                <tr>
                                    <td style="vertical-align: top; width: 50%; height: 100%; border-style: outset;">
                                        <div style="vertical-align: top; width: 98%">
                                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical">
                                                <asp:TreeView ID="TreeView1" ShowCheckBoxes="All" ShowLines="true" ShowExpandCollapse="true" runat="server" ImageSet="Simple" Height="278px" Width="575px">
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
                                            </asp:Panel>
                                        </div>

                                    </td>
                                    <td>&nbsp;</td>
                                    <td style="vertical-align: top; width: 50%; height: 100%; background-color: #C0C0C0">
                                        <br />
                                        <div style="vertical-align: top; width: 100%;">
                                            <table class="nav-justified">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text="SKU Artículo"></asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox1" Width="70%" Enabled="false" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 120px">
                                                        <asp:Label ID="Label2" runat="server" Text="SKU Componente"></asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox2" Enabled="false" Width="90%" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server" Text="Descripción"></asp:Label>&nbsp;

                                                    </td>
                                                    <td colspan="3">
                                                        <textarea id="TextArea1" style="width: 100%; max-width: 1000px !important" disabled runat="server"></textarea>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" Text="Cantidad"></asp:Label>&nbsp

                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox3" onkeypress="CheckNumeric(event);" runat="server"></asp:TextBox>

                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" Text="Unidad de Medida"></asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox4" Enabled="false" Width="70%" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4"><b>
                                                        <asp:Label ID="Label16" runat="server" Text="Costo"></asp:Label></b>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;&nbsp;<asp:Label ID="Label13" runat="server" Text="Estandar"></asp:Label>&nbsp;</td>
                                                    <td colspan="2">
                                                        <asp:RadioButton ID="RadioButton1" GroupName="tipoCosto" runat="server" />&nbsp;
                                                <asp:TextBox ID="TextBox5" Enabled="false" runat="server"></asp:TextBox>&nbsp;
                                                    </td>
                                                    <td>&nbsp;<br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;&nbsp;<asp:Label ID="Label6" runat="server" Visible="false" Text="Compuesto"></asp:Label>&nbsp;</td>
                                                    <td colspan="2">
                                                        <asp:RadioButton ID="RadioButton3" Enabled="false" Visible="false" GroupName="tipoCosto" runat="server" />&nbsp;
                                                <asp:TextBox ID="TextBox8" Enabled="false" Visible="false" runat="server"></asp:TextBox>&nbsp;
                                            
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr style="visibility: hidden">
                                                    <td>&nbsp;&nbsp;<asp:Label ID="Label19" Visible="false" runat="server" Text="Actual"></asp:Label></td>
                                                    <td colspan="2">
                                                        <asp:RadioButton ID="RadioButton2" Visible="false" GroupName="tipoCosto" runat="server" />
                                                        &nbsp;<asp:TextBox ID="TextBox7" Visible="false" Enabled="false" runat="server"></asp:TextBox>&nbsp;
                                                    </td>
                                                    <td></td>
                                                    </t>
                                                
                                                <tr>
                                                    <td colspan="4">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server" Text="Costo Estimacion"></asp:Label>&nbsp
                                                    </td>
                                                    <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="TextBox6" Enabled="false" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td></td>

                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="text-align: right;"></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="width: 100%; text-align: right; height: 35px;">

                            <table style="width: 100%; text-align: right; height: 37px;">
                                <tr>

                                    <td>
                                        <asp:Button ID="ButtonBack" class="btn btn-primary" runat="server" Text="Cancelar" />&nbsp;
                                        <asp:Button ID="Button5" class="btn btn-primary" runat="server" Text="Continuar" />&nbsp;

                                    </td>
                                </tr>

                            </table>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <div style="text-align: center; height: 8px; margin-top: 16px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DynamicLayout="true">
                            <ProgressTemplate>
                                <div class='progress progress-striped active' style='height: 8px;'>
                                    <div class='progress-bar' role='progressbar' aria-valuenow='100' aria-valuemin='0' aria-valuemax='100' style='width: 100%'></div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                    <div id="div1" runat="server"></div>
                    <div class="well well-lg" >
                        <div id="myModal" class="modal fade" role="dialog">
                            <div class="modal-dialog">

                                <!-- Modal content-->
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Cambiar estatus</h4>
                                    </div>
                                    <div class="modal-body">
                                        <p>Estatus Actual</p>
                                        <div id="status_actual" runat="server"></div>
                                        <select id="DDEstatus" runat="server"></select>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button Text="Cancelar" runat="server" type="button" class="btn btn-danger" data-dismiss="modal" />
                                        <asp:Button ID="BTN_ACEPTAR_1" Text="Aceptar" runat="server" type="button" class="btn btn-success" />
                                    </div>
                                </div>

                            </div>
                        </div>
                        <table style="width: 100%">
                            <tr>

                                <td colspan="4">
                                    <div style="float: left;">
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label10" runat="server" Text="Tipo de Cambio"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="Tv_Exchange" runat="server">19.05</asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="float: right;">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="BtnRecalcular" class="btn btn-success" runat="server" Text="Recalcular" />
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:Button ID="TreeView" class="btn btn-default" runat="server" Text="Imprimir Reporte" />
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:Button ID="Imprimir" class="btn btn-default" runat="server" Text="Imprimir Cotizacion" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <div id="margen_ganancia" runat="server"></div>
                            </tr>
                        </table>
                        <br />
                        <asp:GridView class="table" ID="GridSummary" Width="100%" AutoGenerateColumns="False" runat="server" ShowFooter="True">
                            <Columns>
                                <asp:BoundField ItemStyle-Width="20%" HeaderText="Sku" DataField="SkuComponente" />
                                <asp:BoundField ItemStyle-Width="50%" HeaderText="Descripción" DataField="ITEMDESC" />
                                <asp:BoundField ItemStyle-Width="10%" HeaderText="Costo Unitario (Sin Modificar)" Visible="false" DataField="RESULT" ItemStyle-CssClass="t-cost" DataFormatString="${0:###,###,###.00}" HtmlEncode="False" />
                                <asp:BoundField ItemStyle-Width="10%" HeaderText="Costo Unitario" DataField="UnitaryCost" ItemStyle-CssClass="t-cost" DataFormatString="${0:###,###,###.00}" HtmlEncode="False" />
                                <asp:BoundField ItemStyle-Width="10%" HeaderText="Unidad de Medida" Visible="false" DataField="UOFM" />

                                <asp:TemplateField ItemStyle-Width="10%" HeaderText="Cantidad" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TBQuantity" Width="70px" Text='<%# Bind("QUANTITY_I") %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField ItemStyle-Width="10%" HeaderText="Costo Cotización" Visible="false" DataField="FinalCost" ItemStyle-CssClass="t-cost" DataFormatString="${0:###,###,###.00}" HtmlEncode="False" />

                                <asp:TemplateField ItemStyle-Width="10%" Visible="false" HeaderText="Costo de Flete" ItemStyle-CssClass="t-cost">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TVShipping" Width="70px" Text='<%#DataBinder.Eval(Container.DataItem, "Shipping", "${0:###,###,##0.00}")%>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="10%" HeaderText="Throughput (%)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TVMargin" Width="70px" Text='<%# Bind("Margin") %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:BoundField ItemStyle-Width="10%" HeaderText="Throughput" ItemStyle-CssClass="t-cost" DataFormatString="${0:###,###,###.00}" HtmlEncode="False"></asp:BoundField>
                                <asp:BoundField ItemStyle-Width="10%" HeaderText="Precio de Venta (Pesos)" ItemStyle-CssClass="t-cost" DataFormatString="${0:###,###,###.00}" HtmlEncode="False"></asp:BoundField>
                                <asp:BoundField ItemStyle-Width="10%" HeaderText="Precio de Venta (Dólares)" ItemStyle-CssClass="t-cost" DataFormatString="${0:###,###,###.00}" HtmlEncode="False"></asp:BoundField>
                            </Columns>
                            <HeaderStyle BackColor="#C0C0C0" />
                            <FooterStyle BackColor="#C0C0C0" />
                        </asp:GridView>
                        <div style="width: 100%; text-align: right;">

                            <table style="width: 100%; text-align: right;">
                                <tr>
                                    <td>
                                        <asp:Button ID="Regresar" class="btn btn-primary" runat="server" Text="Regresar" />
                                        
                                        <asp:Button ID="Guardar" class="btn btn-primary" runat="server" Text="Guardar" />
                                       
                                        <asp:Button ID="Versionar" class="btn btn-primary" runat="server" Text="Versionar" />
                                       
                                        <button type="button" class="btn btn-warning" data-toggle="modal" data-target="#myModal">Cambiar Estatus</button>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>



                </asp:View>
                <asp:View ID="View3" runat="server">
                    <div style="text-align: center; height: 8px; margin-top: 16px;">
                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DynamicLayout="true">
                            <ProgressTemplate>
                                <div class='progress progress-striped active' style='height: 8px;'>
                                    <div class='progress-bar' role='progressbar' aria-valuenow='100' aria-valuemin='0' aria-valuemax='100' style='width: 100%'></div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                    <div id="div2" runat="server"></div>

                    <div class="well well-lg" style="vertical-align: top; width: 100%">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label14" runat="server" Text="Sku Componente"></asp:Label>

                                </td>
                                <td></td>
                                <td>
                                    <asp:TextBox ID="txtSkuComponente" Enabled="true" runat="server"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="txtSkuComponente"
                                        CssClass="text-danger" ErrorMessage="Campo requerido." />--%>
                                    &nbsp;</td>
                                <td colspan="3"></td>

                            </tr>
                            <tr style="height: 70px; align-content: center; vertical-align: central">
                                <td>
                                    <asp:Label ID="Label15" runat="server" Text="Descripción"></asp:Label></td>
                                <td>&nbsp;</td>
                                <td colspan="4">
                                    <textarea id="txtItemDesc" style="width: 100%; height: 90%; max-width: 1000px !important" runat="server"></textarea></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label17" runat="server" Text="Unidad de Medida"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtUofm" Enabled="true" runat="server"></asp:TextBox>
                                    &nbsp;</td>
                                <td>
                                    <asp:Label ID="Label18" runat="server" Text="Costo"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtStndCost" Enabled="true" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <div style="float: right">
                                        <table>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Button ID="Button9" class="btn btn-primary" runat="server" Text="Regresar" />
                                                </td>
                                                <td>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Button ID="Button8" class="btn btn-primary" runat="server" Text="Guardar" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:View>
                <asp:View ID="View4" runat="server">

                    <div class="well well-lg">

                        <table style="width: 100%">
                            <tr style="text-align: right">
                                <td colspan="4">
                                    <asp:Button ID="Button11" class="btn btn-primary hidden-print" OnClientClick="return PrintPanel();" runat="server" Text="Imprimir" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="Button10" class="btn btn-primary hidden-print" runat="server" Text="Regresar" />

                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">&nbsp;</td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlContents" runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td width="70%">
                                        <img src="logo.png" class="visible-print-block" />&nbsp;
                                    </td>

                                    <td>&nbsp;
                                    </td>
                                    <td colspan="2" style="border-bottom-color: #003366; border-bottom-width: medium; border-bottom-style: solid; text-align: right; color: #000080;">Cotización
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>

                                    <td>&nbsp;
                                    </td>
                                    <td><b>
                                        <asp:Label ID="Label28" runat="server">Fecha</asp:Label></b>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label23" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>

                                    <td>&nbsp;
                                    </td>
                                    <td><b>
                                        <asp:Label ID="Label29" runat="server">Numero de<br />
                                        Cotización</asp:Label></b>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label24" runat="server">#######</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4"></td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="border-style: solid; border-width: thin thin medium thin; text-align: left; color: #000080; border-top-color: #003366;">

                                        <b>
                                            <asp:Label ID="Label25" ForeColor="Black" runat="server">Villarreal División Equipos<br />Atención a: Carlos López&nbsp;</asp:Label>
                                        </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="border-style: solid; border-width: medium 0px 0px 0px; text-align: left; border-top-color: #003366;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="border-style: solid; border-width: medium 0px 0px 0px; text-align: left; border-top-color: #003366;">
                                        <asp:GridView class="table" ID="GridViewCotiza" Width="100%" AutoGenerateColumns="False" runat="server" ShowFooter="True">
                                            <Columns>
                                                <asp:BoundField ItemStyle-Width="20%" HeaderText="Numero de Parte" DataField="SkuComponente">
                                                    <ItemStyle Width="20%" BorderStyle="Solid" />
                                                </asp:BoundField>
                                                <asp:BoundField ItemStyle-Width="50%" HeaderText="Descripción" DataField="ITEMDESC">
                                                    <ItemStyle Width="60%" />
                                                </asp:BoundField>
                                                <asp:BoundField ItemStyle-Width="20%" DataField="UnitaryCost" HeaderText="Precio Unitario (Pesos)" ItemStyle-CssClass="t-cost" DataFormatString="${0:###,###,###.00}" HtmlEncode="False" FooterText="Precios más IVA">
                                                    <ItemStyle CssClass="t-cost" Width="30%" />
                                                </asp:BoundField>
                                            </Columns>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="border-style: solid; border-width: medium 0px 0px 0px; text-align: left; color: #000080; border-top-color: #003366;">
                                        <table style="width: 100%; color: black">
                                            <tr>
                                                <td style="width: 20%"><b>
                                                    <asp:Label ID="Label26" runat="server">Oferta valida</asp:Label></b></td>
                                                <td>
                                                    <asp:Label ID="Label27" runat="server">30 días</asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>

                </asp:View>
                <asp:View ID="View5" runat="server">
                    <div class="well well-lg">
                        <%--<asp:Panel ID="pnlContents2" runat="server">--%>
                        <table style="width: 100%">
                            <tr style="text-align: right">
                                <td colspan="2" width="50%">
                                    <img src="logo.png" class="visible-print-block" />&nbsp;
                                </td>
                                <td colspan="2">
                                    <asp:Button ID="Button12" class="btn btn-primary hidden-print" OnClientClick="return PrintPanel2();" runat="server" Text="Imprimir" />
                                    &nbsp;
                                        <asp:Button ID="Button13" class="btn btn-primary hidden-print" runat="server" Text="Regresar" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlContents2" runat="server">
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
                            <asp:GridView class="table" ID="GridTreeView" Width="100%" AutoGenerateColumns="False" runat="server" ShowFooter="True" Style="margin-right: 0px">
                                <Columns>
                                    <asp:BoundField HeaderText="Articulo" DataField="SkuArticulo">
                                        <ControlStyle Width="90px" />
                                        <ItemStyle BorderStyle="Solid" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Numero de Parte" DataField="SkuComponente">
                                        <ItemStyle BorderStyle="Solid" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Descripción" DataField="ITEMDESC">
                                        <ItemStyle />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Cantidad" DataField="Quantity_I">
                                        <ItemStyle BorderStyle="Solid" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Unidad" DataField="UofM">
                                        <ItemStyle BorderStyle="Solid" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Costo Estandar" DataField="STNDCOST">
                                        <ItemStyle BorderStyle="Solid" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Costo Actual" DataField="CURRCOST">
                                        <ItemStyle BorderStyle="Solid" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Total" DataField="Result">
                                        <ItemStyle BorderStyle="Solid" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Nivel1" DataField="Nivel1">
                                        <ItemStyle BorderStyle="Solid" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Nivel2" DataField="Nivel2">
                                        <ItemStyle BorderStyle="Solid" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Nivel3" DataField="Nivel3">
                                        <ItemStyle BorderStyle="Solid" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </asp:View>
                <asp:View ID="View6" runat="server">
                    <div style="text-align: center; height: 8px; margin-top: 16px;">
                        <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DynamicLayout="true">
                            <ProgressTemplate>
                                <div class='progress progress-striped active' style='height: 8px;'>
                                    <div class='progress-bar' role='progressbar' aria-valuenow='100' aria-valuemin='0' aria-valuemax='100' style='width: 100%'></div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                    <div id="div3" runat="server"></div>

                    <div class="well well-lg" style="vertical-align: top; width: 100%">
                        <table>
                            <tr>
                                <td style="width: 60px">
                                    <asp:Label ID="Label32" runat="server" Text="Nombre"></asp:Label>

                                </td>
                                <td></td>
                                <td style="width: 350px">
                                    <asp:TextBox ID="txtProsComName" Width="95%" MaxLength="250" Enabled="true" runat="server"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="txtSkuComponente"
                                        CssClass="text-danger" ErrorMessage="Campo requerido." />--%>
                                    &nbsp;</td>

                            </tr>
                            <tr style="height: 70px; align-content: center; vertical-align: central">
                                <td>
                                    <asp:Label ID="Label33" runat="server" Text="Dirección"></asp:Label></td>
                                <td>&nbsp;</td>
                                <td style="width: 350px">
                                    <textarea id="txtProsAddress" style="width: 100%; height: 90%; max-width: 500 !important" runat="server"></textarea></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label36" runat="server" Text="Contacto"></asp:Label>

                                </td>
                                <td></td>
                                <td style="width: 350px">
                                    <asp:TextBox ID="txtProsContactName" MaxLength="250" Width="95%" Enabled="true" runat="server"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="txtSkuComponente"
                                        CssClass="text-danger" ErrorMessage="Campo requerido." />--%>
                                    &nbsp;</td>

                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label34" runat="server" Text="Telefono"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                <td style="width: 350px">
                                    <asp:TextBox ID="txtProsPhoneNumber" MaxLength="20" runat="server"></asp:TextBox>
                                    &nbsp;</td>
                            </tr>
                        </table>
                        <br />
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <div style="float: right">
                                        <table>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="Regresar" />
                                                </td>
                                                <td>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Button ID="Button3" class="btn btn-primary" runat="server" Text="Guardar" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:View>

            </asp:MultiView>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
