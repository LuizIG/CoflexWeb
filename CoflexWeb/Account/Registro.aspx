<%@ Page Title="" Language="vb" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Registro.aspx.vb" Inherits="CoflexWeb.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%--<%: Title %>.--%></h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="well well-lg" style="margin-top: 64px">
        
        <div class="form-group" style="text-align:right">
            <asp:Button runat="server" OnClick="CreateUser_Click" Text="Registrar" CssClass="btn btn-primary" />
        </div>
        <div class="row">
            <div class="col-sm-4">

                <h4>Información de acceso</h4>

                <asp:ValidationSummary runat="server" CssClass="text-danger" />
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-4 control-label">Usuario</asp:Label>
                    <div class="col-md-8">
                        <asp:TextBox runat="server" ID="UserName" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                            CssClass="text-danger" ErrorMessage="The user name field is required." />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-4 control-label">Contraseña</asp:Label>
                    <div class="col-md-8">
                        <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                            CssClass="text-danger" ErrorMessage="The password field is required." />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-4 control-label">Confirmar contraseña</asp:Label>
                    <div class="col-md-8">
                        <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                            CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                        <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                            CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
                    </div>
                </div>

            </div>
            <div class="col-sm-4">

                <h4>Información del usuario</h4>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="Name" CssClass="col-md-4 control-label">Nombre</asp:Label>
                    <div class="col-md-8">
                        <asp:TextBox runat="server" ID="Name" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Name"
                            CssClass="text-danger" ErrorMessage="The user name field is required." />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="LastName" CssClass="col-md-4 control-label">Apellido Paterno</asp:Label>
                    <div class="col-md-8">
                        <asp:TextBox runat="server" ID="LastName" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="LastName"
                            CssClass="text-danger" ErrorMessage="The user name field is required." />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="MotherSurname" CssClass="col-md-4 control-label">Apellido Materno</asp:Label>
                    <div class="col-md-8" style="left: 0px; top: 0px">
                        <asp:TextBox runat="server" ID="MotherSurname" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="MotherSurname"
                            CssClass="text-danger" ErrorMessage="The user name field is required." />
                    </div>
                </div>

            </div>
            <div class="col-sm-4">

                <h4>Roles</h4>
                <div class="form-group">
                    <div class="col-md-12">
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
                <div style="margin-top:16px" class="form-group">
                    <asp:Label runat="server" AssociatedControlID="Name" CssClass="col-md-4 control-label">Lider</asp:Label>
                    <div class="col-md-8">
                        <asp:DropDownList runat="server" ID="Cb_Leaders" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Name"
                            CssClass="text-danger" ErrorMessage="The user name field is required." />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
