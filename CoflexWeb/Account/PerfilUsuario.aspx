<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PerfilUsuario.aspx.vb" Inherits="CoflexWeb.PerfilUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <div style="margin-top:32px" class="row">
        <div class="col-lg-6">
            <h4>Editar Usuario</h4>
            <asp:ValidationSummary runat="server" CssClass="text-danger" />
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="UserName" Enabled="false" CssClass="col-md-2 control-label">Usuario</asp:Label>
                    <asp:TextBox runat="server" ID="UserName" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                        CssClass="text-danger" ErrorMessage="The user name field is required." />
            </div>

            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Name" CssClass="col-md-2 control-label">Nombre</asp:Label>
                    <asp:TextBox runat="server" ID="Name" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Name"
                        CssClass="text-danger" ErrorMessage="The user name field is required." />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="LastName" CssClass="col-md-2 control-label">Apellido Paterno</asp:Label>
                    <asp:TextBox runat="server" ID="LastName" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="LastName"
                        CssClass="text-danger" ErrorMessage="The user name field is required." />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="MotherSurname" CssClass="col-md-2 control-label">Apellido Materno</asp:Label>
                    <asp:TextBox runat="server" ID="MotherSurname" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="MotherSurname"
                        CssClass="text-danger" ErrorMessage="The user name field is required." />
            </div>

            <asp:Button id="btn_edit_profile" class="btn btn-primary" role="button" runat="server" Text="Editar Usuario" />
        </div>
        <div class="col-lg-6">
            <h4>Reestablecer contraseña</h4>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Contraseña</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                        CssClass="text-danger" ErrorMessage="The password field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Confirmar contraseña</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                        CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                    <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                        CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
                </div>
            </div>
            <asp:Button id="btn_cambiar_pass" class="btn btn-primary" role="button" runat="server" Text="Cambiar Contraseña" />
        </div>
    </div>

    <div style="margin-top:32px" class="row">
        <h4>Cambiar Roles</h4>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="GridRoles" CssClass="col-md-2 control-label">Roles</asp:Label>
            <div class="col-md-offset-2 col-md-10">
                <asp:GridView ID="GridRoles" BorderColor="White" runat="server" ShowHeader="false" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" Name="chkSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
