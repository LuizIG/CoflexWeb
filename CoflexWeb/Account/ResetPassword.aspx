<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ResetPassword.aspx.vb" Inherits="CoflexWeb.ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <section id="passwordForm">


            <div class="form-horizontal">
                <h4></h4>
                <hr />
                <p class="text-danger">
                    <asp:Literal runat="server" ID="ErrorMessage" />
                </p>
                <asp:ValidationSummary runat="server" ShowModelStateErrors="true" CssClass="text-danger" />
                <div class="form-group">
                    <asp:Label runat="server" ID="CurrentPasswordLabel" AssociatedControlID="CurrentPassword" CssClass="col-md-2 control-label">Current password</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="CurrentPassword" TextMode="Password" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="CurrentPassword"
                            CssClass="text-danger" ErrorMessage="The current password field is required."
                            ValidationGroup="ChangePassword" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" ID="NewPasswordLabel" AssociatedControlID="NewPassword" CssClass="col-md-2 control-label">New password</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="NewPassword" TextMode="Password" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="NewPassword"
                            CssClass="text-danger" ErrorMessage="The new password is required."
                            ValidationGroup="ChangePassword" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" ID="ConfirmNewPasswordLabel" AssociatedControlID="ConfirmNewPassword" CssClass="col-md-2 control-label">Confirm new password</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="ConfirmNewPassword" TextMode="Password" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmNewPassword"
                            CssClass="text-danger" Display="Dynamic" ErrorMessage="Confirm new password is required."
                            ValidationGroup="ChangePassword" />
                        <asp:CompareValidator runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword"
                            CssClass="text-danger" Display="Dynamic" ErrorMessage="The new password and confirmation password do not match."
                            ValidationGroup="ChangePassword" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-offset-2 col-md-10">
                        <asp:Button runat="server" Text="Cambiar contraseña" class="btn btn-primary" ValidationGroup="ChangePassword" OnClick="ChangePassword_Click" />
                    </div>
                </div>
            </div>

        </section>
    </div>
</asp:Content>
