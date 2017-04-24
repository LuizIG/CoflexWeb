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

    </script>
    <h2><%--<%: Title %>.--%></h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="View1" runat="server">

                    <div id="div_Response" runat="server"></div>

                    <div class="well well-lg">

                        <table style="border-style: solid; border-color: #C0C0C0; width: 100%; align-items: center">
                            <tr style="vertical-align: top;">
                                <td style="width: 30%; text-align: center; vertical-align: top;">
                                    <div style="width: 100%; vertical-align: top;">
                                        <table style="width: 100%;">
                                            <tr style="background-color: #C0C0C0">
                                                <td colspan="3" style="text-align: left;"><b>&nbsp;<asp:Label ID="Label20" runat="server" Text="Articulos"></asp:Label>
                                                </b>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label9" runat="server" Text="Cliente"></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDCliente" Width="100%" runat="server" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                                <td rowspan="2" style="vertical-align: top;">
                                                    <asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="Agregar" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" Text="Articulo"></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDArticulo" Width="100%" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>

                                        </table>
                                    </div>
                                </td>
                                <td style="width: 30%; height: 100%; text-align: center; vertical-align: top;">
                                    <div style="width: 100%; height: 100% vertical-align: top;">
                                        <table style="width: 100%; height: 100%; vertical-align: top;">
                                            <tr style="background-color: #C0C0C0">
                                                <td colspan="3" style="text-align: left;"><b>&nbsp;<asp:Label ID="Label21" runat="server" Text="Componentes"></asp:Label>
                                                </b>&nbsp;</td>
                                            </tr>
                                            <tr style="vertical-align: central;">
                                                <td>
                                                    <asp:Label ID="Label11" runat="server" Text="Componente"></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDComponente" runat="server">
                                                    </asp:DropDownList>&nbsp;
                                                </td>
                                                <td colspan="2" style="vertical-align: central;">
                                                    <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="Agregar" />
                                                </td>
                                            </tr>

                                        </table>

                                    </div>
                                </td>
                                <td style="border-right-style: solid; border-color: #C0C0C0; width: 30%; text-align: center; vertical-align: top">
                                    <div style="width: 100%; vertical-align: top;">
                                        <table style="width: 100%; height: 100%; vertical-align: top;">
                                            <tr style="background-color: #C0C0C0">
                                                <td colspan="4" style="text-align: left;"><b>&nbsp;<asp:Label ID="Label22" runat="server" Text="Elemento"></asp:Label>
                                                </b>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label12" runat="server" Text="Elemento"></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDElemento" runat="server">
                                                    </asp:DropDownList>&nbsp;
                                                </td>
                                                <td style="vertical-align: central;">
                                                    <asp:Button ID="Button6" runat="server" class="btn btn-primary" Text="Nuevo" />&nbsp;

                                                </td>
                                                <td style="vertical-align: central;">
                                                    <asp:Button ID="Button7" runat="server" class="btn btn-primary" Text="Agregar" />&nbsp;

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
                                                    <asp:Button ID="Button5" class="btn btn-primary" runat="server" Text="Continuar" />

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
                                        <div style="width: 100%; height: 35px;">

                                            <table style="width: 100%; text-align: right; height: 37px;">
                                                <tr><td>

                                                    </td></tr>

                                                <tr>
                                                    <td>
                                                        <asp:Button ID="Button4" runat="server" class="btn btn-primary" Text="Remover" />

                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                    <td style="vertical-align: top; width: 50%; height: 100%;">
                                        <br />
                                        <div style="vertical-align: top; width: 100%">
                                            <table class="nav-justified">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text="Sku Articulo"></asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox1" Width="70%" Enabled="false" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text="Sku Componente"></asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox2" Enabled="false" Width="70%" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server" Text="Descripción"></asp:Label>&nbsp;

                                                    </td>
                                                    <td colspan="3">
                                                        <textarea id="TextArea1" style="width: 100%" disabled runat="server"></textarea>
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
                                                    <td>&nbsp;&nbsp;<asp:Label ID="Label19" runat="server" Text="Actual"></asp:Label></td>
                                                    <td colspan="2">
                                                        <asp:RadioButton ID="RadioButton2" GroupName="tipoCosto" runat="server" />
                                                        &nbsp;<asp:TextBox ID="TextBox7" Enabled="false" runat="server"></asp:TextBox>&nbsp;
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;&nbsp;<asp:Label ID="Label6" runat="server" Visible="false" Text="Compuesto"></asp:Label>&nbsp;</td>
                                                    <td colspan="2">
                                                        <asp:RadioButton ID="RadioButton3" Enabled="false" Visible="false" GroupName="tipoCosto" runat="server" />&nbsp;
                                                <asp:TextBox ID="TextBox8" Enabled="false" Visible="false" runat="server"></asp:TextBox>&nbsp;
                                            
                                                    </td>
                                                    <td></td>
                                                </tr>
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
                                        <div style="width: 100%">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="text-align: right;">
                                                        <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="Actualizar" />&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">

                    <div class="well well-lg" style="margin-top: 32px">
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
                                                    <asp:Button ID="Imprimir" class="btn btn-default" runat="server" Text="Imprimir" />
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:Button ID="Guardar" class="btn btn-primary" runat="server" Text="Guardar" />
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:Button ID="Versionar" class="btn btn-primary" runat="server" Text="Versionar" />

                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:Button ID="Cotizar" class="btn btn-default" runat="server" Text="Cotizar" />

                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:Button ID="Regresar" class="btn btn-primary" runat="server" Text="Regresar" />

                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <button type="button" class="btn btn-warning" data-toggle="modal" data-target="#myModal">Cambiar Estatus</button>
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
                                <asp:BoundField ItemStyle-Width="10%" HeaderText="Costo Unitario (Sin Modificar)" Visible="false" DataField="RESULT" ItemStyle-CssClass="t-cost" DataFormatString="${0:###,###,###.0000}" HtmlEncode="False" />
                                <asp:BoundField ItemStyle-Width="10%" HeaderText="Costo Unitario" DataField="UnitaryCost" ItemStyle-CssClass="t-cost" DataFormatString="${0:###,###,###.0000}" HtmlEncode="False" />
                                <asp:BoundField ItemStyle-Width="10%" HeaderText="Unidad de Medida" Visible="false" DataField="UOFM" />

                                <asp:TemplateField ItemStyle-Width="10%" HeaderText="Cantidad">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TBQuantity" Width="70px" Text='<%# Bind("QUANTITY_I") %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField ItemStyle-Width="10%" HeaderText="Costo Cotización" DataField="FinalCost" ItemStyle-CssClass="t-cost" DataFormatString="${0:###,###,###.0000}" HtmlEncode="False" />

                                <asp:TemplateField ItemStyle-Width="10%" HeaderText="Margen (%)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TVMargin" Width="70px" Text='<%# Bind("Margin") %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:BoundField ItemStyle-Width="10%" HeaderText="Precio de Venta (Pesos)" ItemStyle-CssClass="t-cost" DataFormatString="${0:###,###,###.0000}" HtmlEncode="False"></asp:BoundField>
                                <asp:BoundField ItemStyle-Width="10%" HeaderText="Precio de Venta (Dólares)" ItemStyle-CssClass="t-cost" DataFormatString="${0:###,###,###.0000}" HtmlEncode="False"></asp:BoundField>
                            </Columns>
                            <HeaderStyle BackColor="#C0C0C0" />
                            <FooterStyle BackColor="#C0C0C0" />
                        </asp:GridView>
                    </div>



                </asp:View>
                <asp:View ID="View3" runat="server">
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
                                                <asp:Button ID="Button8" class="btn btn-primary" runat="server" Text="Agregar" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>

                    <br />
                    <div style="vertical-align: top; width: 100%">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label14" runat="server" Text="Sku Componente"></asp:Label>

                                </td>
                                <td>
                                    <asp:TextBox ID="txtSkuComponente" Enabled="true" runat="server"></asp:TextBox>
                                    &nbsp;</td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label15" runat="server" Text="Descripción"></asp:Label></td>
                                <td colspan="3">
                                    <textarea id="txtItemDesc" style="width: 100%" runat="server"></textarea></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label17" runat="server" Text="Unidad de Medida"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUofm" Enabled="true" runat="server"></asp:TextBox>
                                    &nbsp;</td>
                                <td>
                                    <asp:Label ID="Label18" runat="server" Text="Costo"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStndCost" Enabled="true" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>



                    <!-- Modal -->


                </asp:View>
            </asp:MultiView>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
