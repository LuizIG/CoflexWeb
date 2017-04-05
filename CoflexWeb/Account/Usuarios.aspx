<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Usuarios.aspx.vb" Inherits="CoflexWeb.Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="well well-lg" style="margin-top:32px">
        <asp:TextBox ID="RoleName" runat="server"  placeholder="Nombre"  />
        <asp:Button ID="BtnSend" runat="server"  text="Enviar"  />
        <div class="divider" style="margin-top:16px;margin-bottom:16px"></div>
            <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Promotor" />
            <asp:BoundField DataField="Email" HeaderText="Solicitante" />
            <asp:BoundField DataField="Name" HeaderText="RFC" />
            <asp:BoundField DataField="PaternalSurname" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="MaternalSurname" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="Roles" HeaderText="Fecha" />            
                <asp:TemplateField HeaderText="Conversión a Prospecto">
                <ItemTemplate>
                    <b><a target='_parent' style="color:black;" href='default2.aspx?code=<%#Eval("id")%>'> <%#Eval("reResultado")%> </a></b>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Calculadora Financiera">
                <ItemTemplate>
                    <b><a target='_parent' style="color:black;" href='default4.aspx?code=<%#Eval("id")%>'> <%#Eval("reResultadoEF")%> </a></b>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField  HeaderText="Resultado" ItemStyle-Font-Bold="true"   />
        </Columns>
        <HeaderStyle BackColor="#C0C0C0" />
    </asp:GridView>
    </div>
    <div id="response" runat="server"></div>
</asp:Content>
