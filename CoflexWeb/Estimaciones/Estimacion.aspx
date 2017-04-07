<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Estimacion.aspx.vb" Inherits="CoflexWeb.Estimacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%--<%: Title %>.--%></h2>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <div class="row">
                <table style="width: 50%">
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Cliente"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DDCliente" runat="server">
                                <asp:ListItem Value="1">Usuario Ejemplo 1</asp:ListItem>
                                <asp:ListItem Value="1">Usuario Ejemplo 2</asp:ListItem>
                                <asp:ListItem Value="1">Usuario Ejemplo 3</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="Tipo de Cambio"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="TextBox7" runat="server">19.05</asp:TextBox>
                        </td>
                    </tr>

                </table>
                <br />
                <table style="width: 100%">
                    <tr>
                        <td>
                            <div style="float: left;">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Text="Articulo"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDArticulo" runat="server">
                                                
                                                <asp:ListItem>2-PS-E324-RH</asp:ListItem>
                                                <asp:ListItem>1-AB-Q60</asp:ListItem>
                                                
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="Button1" runat="server" Text="Agregar" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="Button2" runat="server" Text="Remover" />
                                        </td>
                                         <td>
                                            <asp:Label ID="Label11" runat="server" Text="Articulo"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDComponente" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="Button3" runat="server" Text="Agregar" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="Button4" runat="server" Text="Remover" />
                                        </td>
                                    </tr>
                                </table>

                            </div>
                        </td>
                        <td>
                            <div style="float: right;">
                                <table>
                                    <tr>
                                        <td>
                                            <%--<asp:Button ID="Button3" runat="server" Text="Guardar" />--%>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <%--<asp:Button ID="Button4" runat="server" Text="Enviar" />--%>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="Button5" runat="server" Text="Continuar" />

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
                            <td style="vertical-align: top; width: 50%; border-style: outset;">
                                <div style="vertical-align: top; width: 100%">
                                    <asp:TreeView ID="TreeView1" Width="100%" ShowCheckBoxes="All" ShowExpandCollapse="true"  runat="server" ImageSet="Simple">
                                          <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                          <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
                                          <ParentNodeStyle Font-Bold="False" />
                                          <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
                                    </asp:TreeView>
                                </div>
                            </td>
                            <td style="vertical-align: top; width: 50%">
                                <div style="vertical-align: top; width: 100%">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Sku Articulo"></asp:Label>

                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox1" runat="server">2-PS-E324</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="Sku Componente"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox2" runat="server">2-PS-E324</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text="Descripción"></asp:Label></td>
                                            <td colspan="3">
                                                <textarea id="TextArea1" style="width: 100%" runat="server">PAQUETE INSTALACION CALENTADOR PALOMA 3/4</textarea></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="Cantidad"></asp:Label>

                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox3" runat="server">1</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text="Unidad de Medida"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox4" runat="server">PZ</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text="Costo"></asp:Label>

                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox5" runat="server">467.59</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text="Total"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox6" runat="server">467.59</asp:TextBox>
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
            <table style="width: 100%">
                <tr>

                    <td colspan="4">
                        <div style="float: left;">
                            <table>
                                    <tr>
                                        <td>Margen</td><td>
                                            <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox></td></tr></table>
                            </div>
                          <div style="float: right;">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Imprimir" runat="server" Text="Imprimir" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="Guardar" runat="server" Text="Guardar" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="Versionar" runat="server" Text="Versionar" />

                                        </td> <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="Cotizar" runat="server" Text="Cotizar" />

                                        </td>
                                         <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="Regresar" runat="server" Text="Regresar" />

                                        </td>
                                    </tr>
                                </table>
                            </div>

                    </td>

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
                <tr><td colspan="4">&nbsp;</td></tr>
                <tr><td colspan="2" style="text-align:right"><b>Total</b>&nbsp;</td>
                    <td><b>&nbsp;$537.82</b></td>
                    <td><b>3</b></td>
                </tr>
            </table>
        </asp:View>

    </asp:MultiView>
</asp:Content>
