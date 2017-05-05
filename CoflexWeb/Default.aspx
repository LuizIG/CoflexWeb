<%@ Page Title="" Language="vb" CodeBehind="Default.aspx.vb" Inherits="CoflexWeb.Acceso" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Iniciar Sesión</title>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>
    <style type="text/css">
        input {
            max-width: 1000px !important;
        }

        body {
            padding-top: 0px !important;
        }
    </style>
    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <%--Para obtener más información sobre cómo agrupar scripts en ScriptManager, consulte http://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Scripts de marco--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Name="respond" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <%--Scripts del sitio--%>
            </Scripts>
        </asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
                <div style="text-align: center; height:8px">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel" DynamicLayout="true">
                        <ProgressTemplate>
                            <div class='progress progress-striped active' style='height: 8px;'><div class='progress-bar' role='progressbar' aria-valuenow='100' aria-valuemin='0' aria-valuemax='100' style='width:100%'></div></div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>
                <div id="error_msg" runat="server" style="margin-left: 16px; margin-right: 16px;"></div>
                <div>
                    <div class="col-sm-4"></div>
                    <div class="col-sm-4">
                        <div style="margin: 16px" class="well well-lg">
                            <div style="padding: 16px" class="row">
                                <img style="margin: auto; display: block" src="Images/logo.png" />
                                <hr />
                                <h4 style="margin-top: 8px" class="col-md-12">Iniciar Sesión</h4>

                                <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                                    <p class="text-danger">
                                        <asp:Literal runat="server" ID="FailureText" />
                                    </p>
                                </asp:PlaceHolder>
                                <div style="width: 100%">
                                    <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-12 control-label">Usuario</asp:Label>
                                    <div class="col-md-12">
                                        <asp:TextBox runat="server" Width="100%" ID="UserName" CssClass="form-control" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                            CssClass="text-danger" ErrorMessage="Captura tu correo." />
                                    </div>
                                </div>
                                <div style="width: 100%">
                                    <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-12 control-label">Contraseña</asp:Label>
                                    <div class="col-md-12">
                                        <asp:TextBox Width="100%" runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="Caputra tu contraseña." />
                                    </div>
                                </div>

                                <div style="width: 100%; margin-top: 8px">
                                    <div class="col-md-12">
                                        <asp:Button Width="100%" runat="server" OnClick="LogIn" Text="Entrar" CssClass="btn btn-success" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4"></div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>

