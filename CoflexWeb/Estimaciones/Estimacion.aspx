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
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">

            <div id="div_Response"  runat="server"></div>

            <div class="well well-lg">

                <table style="border-style: solid; border-color: #C0C0C0; width: 100%; align-items: center">
                    <tr>
                        <td style="border-right-style: solid; border-color: #C0C0C0; width: 30%; text-align: center">
                            <div style="width: 100%">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label9" runat="server" Text="Cliente"></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDCliente" Width="100%" runat="server" AutoPostBack="True" >
                                            </asp:DropDownList>
                                        </td>
                                        <td rowspan="2" style="vertical-align: bottom;">
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
                        <td style="border-right-style: solid; border-color: #C0C0C0; width: 30%; text-align: center;">
                            <div style="width: 100%">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label11" runat="server" Text="Componente"></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDComponente" runat="server">
                                            </asp:DropDownList>&nbsp;
                                        </td>
                                        <td colspan="2" style="vertical-align: bottom;">
                                            <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="Agregar" />
                                        </td>
                                    </tr>

                                </table>

                            </div>
                        </td>
                        <td style="border-right-style: solid; border-color: #C0C0C0; width: 30%; text-align: center; vertical-align: middle">
                            <div style="width: 100%">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label12" runat="server" Text="Elemento"></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDElemento" runat="server">
                                            </asp:DropDownList>&nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="Button6" runat="server" class="btn btn-primary" Text="Nuevo" />&nbsp;

                                        </td>
                                        <td>
                                            <asp:Button ID="Button7" runat="server" class="btn btn-primary" Text="Agregar" />&nbsp;

                                        </td>
                                    </tr>

                                </table>
                            </div>
                        </td>
                        <td style="width: 10%; text-align: center;">
                            <div style="width: 100%">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button4" runat="server" class="btn btn-primary" Text="Remover" />

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
                        <td>
                            <table style="width: 50%">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text="Tipo de Cambio"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="Tv_Exchange" runat="server">19.05</asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
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
                    <table style="width: 100%">
                        <tr>
                            <td style="vertical-align: top; width: 50%; height:100%; border-style: outset;">
                                <div style="vertical-align: top; height:100%; width: 98%">
                                    <asp:TreeView ID="TreeView1" Width="90%" ShowCheckBoxes="All" ShowExpandCollapse="true" runat="server" ImageSet="Simple">
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
                                </div>
                            </td>
                            <td style="vertical-align: top; width: 50%">
                                <br />
                                <div style="vertical-align: top; width: 100%">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Sku Articulo"></asp:Label>&nbsp;

                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox1" Enabled="false" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="Sku Componente"></asp:Label>&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox2" Enabled="false" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text="Descripción"></asp:Label>&nbsp;

                                            </td>
                                            <td colspan="3">
                                                <textarea id="TextArea1" style="width: 100%" disabled runat="server"></textarea></td>
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
                                                <asp:TextBox ID="TextBox4" Enabled="false" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text="Costo"></asp:Label>&nbsp;

                                            </td>
                                            <td>
                                                
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text="Total"></asp:Label>&nbsp
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox6" Enabled="false" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><asp:Label ID="Label13" runat="server" Text="Tipo de Costo"></asp:Label>&nbsp;</td>
                                            <td>
                                                <asp:RadioButton ID="RadioButton1" GroupName="tipoCosto" runat="server" />&nbsp;<asp:Label ID="Label16" runat="server" Text="Costo Estandar"></asp:Label>&nbsp;<asp:TextBox ID="TextBox5" Enabled="false" runat="server"></asp:TextBox>&nbsp;<br />
                                                <asp:RadioButton ID="RadioButton2" GroupName="tipoCosto" runat="server" />&nbsp;<asp:Label ID="Label19" runat="server" Text="Costo Actual"></asp:Label>&nbsp;<asp:TextBox ID="TextBox7" Enabled="false" runat="server"></asp:TextBox>&nbsp;
                                            </td>
                                            <td>&nbsp;</td>
                                            <td style="text-align: right">
                                                <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="Actualizar" /></td>
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
            <table style="width: 100%">
                <tr>

                    <td colspan="4">
                        <div style="float: left;">
                            <table>
                                <tr>
                                    <td>Margen</td>
                                    <td>
                                        <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox></td>
                                </tr>
                            </table>
                        </div>
                        <div style="float: right;">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="Imprimir" Enabled="false" class="btn btn-primary" runat="server" Text="Imprimir" />
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
                                        <asp:Button ID="Cotizar" class="btn btn-primary" Enabled="false" runat="server" Text="Cotizar" />

                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Button ID="Regresar" class="btn btn-primary" runat="server" Text="Regresar" />

                                    </td>
                                </tr>
                            </table>
                        </div>

                    </td>

                </tr>
            </table>
            <br />
            <asp:GridView ID="GridSummary" Width="100%" AutoGenerateColumns="False" runat="server">
                <Columns>
                    <asp:BoundField ItemStyle-Width="10%" HeaderText="No Articulo" DataField="SkuComponente" />
                    <asp:BoundField ItemStyle-Width="70%" HeaderText="Articulo" DataField="ITEMDESC" />
                    <asp:BoundField ItemStyle-Width="10%" HeaderText="Costo" DataField="RESULT" />
                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Cantidad">
                        <ItemTemplate>
                            <asp:TextBox ID="TBQuantity" Text='<%# Bind("QUANTITY_I") %>' runat="server"></asp:TextBox>
                            <%--<asp:CheckBox ID="chkSelect" Name="chkSelect" runat="server" />--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <%-- <table>
                <tr>
                    <td colspan="4"></td>
                </tr>
                <tr>
                    <td style="width: 10%"><b>No Articulo</b></td>
                    <td style="width: 70%"><b>Articulo</b></td>
                    <td style="width: 10%"><b>Costo</b></td>
                    <td style="width: 10%"><b>Cantidad</b></td>
                </tr>
                <tr>
                    <td>2-PS-E324-RH</td>
                    <td>PAQUETE INSTALACION CALENTADOR 3/4</td>
                    <td>$467.59</td>
                    <td>
                        <asp:TextBox ID="TextBox8" runat="server">1</asp:TextBox></td>
                </tr>
                <tr>
                    <td>1-B1-30-A</td>
                    <td>PB FAUCET CONNECTOR 3/8 COMP X 1/2 FIP</td>
                    <td>$23.99</td>
                    <td>
                        <asp:TextBox ID="TextBox9" runat="server">1</asp:TextBox></td>
                </tr>
                <tr>
                    <td>1-AB-Q60</td>
                    <td>ROTOPLAS ACERO BOILER, PVC, 1/2 FIP 3/4 FIP</td>
                    <td>$46.24</td>
                    <td>
                        <asp:TextBox ID="TextBox10" runat="server">1</asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right"><b>Total</b>&nbsp;</td>
                    <td><b>&nbsp;$537.82</b></td>
                    <td><b>3</b></td>
                </tr>
            </table>--%>
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
                                    <td>
                                        &nbsp;
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
        </asp:View>
    </asp:MultiView>
</asp:Content>
