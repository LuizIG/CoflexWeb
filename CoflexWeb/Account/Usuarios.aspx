<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Usuarios.aspx.vb" Inherits="CoflexWeb.Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="well well-lg" style="margin-top:32px">
        <asp:TextBox ID="RoleName" runat="server"  placeholder="Nombre"  />
        <asp:Button ID="BtnSend" runat="server"  text="Enviar"  />
        <div class="divider" style="margin-top:16px;margin-bottom:16px"></div>
        <asp:GridView ID="GridUsers" runat="server" AutoGenerateColumns="false" CssClass="Grid"
            DataKeyNames="Id" OnRowDataBound="OnRowDataBound">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <img alt = "" style="cursor: pointer" src="images/plus.png" />
                        <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                            <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                                <Columns>
                                    <asp:BoundField ItemStyle-Width="150px" DataField="OrderId" HeaderText="Order Id" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="OrderDate" HeaderText="Date" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField ItemStyle-Width="150px" DataField="Email" HeaderText="Email" />
                <asp:BoundField ItemStyle-Width="150px" DataField="Name" HeaderText="Nombre" />
                <asp:BoundField ItemStyle-Width="150px" DataField="PaternalSurname" HeaderText="Apellido Paterno" />
                <asp:BoundField ItemStyle-Width="150px" DataField="MaternalSurname" HeaderText="Apellido Materno" />
            </Columns>
        </asp:GridView>
    </div>
    <div id="response" runat="server"></div>
</asp:Content>
