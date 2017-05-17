<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Indicadores.aspx.vb" Inherits="CoflexWeb.Indicadores" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

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

            <div class="well well-lg" style="width: 100%; text-align: center">

                <table style="width: 100%; text-align: center">
                    <tr>
                        <td>
                            <b>&nbsp;<asp:Label ID="LblNoDeCo" runat="server" Text="Número de cotizaciones"></asp:Label></b>&nbsp;
                        </td>
                        <td>
                            <b>&nbsp;<asp:Label ID="LblMoToCo" runat="server" Text="Monto total cotizado"></asp:Label></b>&nbsp;
                        </td>
                        <td>
                            <b>&nbsp;<asp:Label ID="LblMoPrCo" runat="server" Text="Monto promedio de las cotizaciones"></asp:Label></b>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Chart ID="Chart1" runat="server" Palette="None" PaletteCustomColors="DodgerBlue; Maroon; DarkGreen; DarkGoldenrod; DarkSlateGray; Indigo; DimGray">
                                <Series>
                                    <asp:Series Name="Series1"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                        <AxisX>
                                            <MajorGrid Enabled="false" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                        <td>
                            <asp:Chart ID="Chart2" runat="server" Palette="None" PaletteCustomColors="DodgerBlue; Maroon; DarkGreen; DarkGoldenrod; DarkSlateGray; Indigo; DimGray">
                                <Series>
                                    <asp:Series Name="Series1" LabelFormat="C"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                        <AxisX>
                                            <MajorGrid Enabled="false" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                        <td>
                            <asp:Chart ID="Chart3" runat="server" Palette="None" PaletteCustomColors="DodgerBlue; Maroon; DarkGreen; DarkGoldenrod; DarkSlateGray; Indigo; DimGray">
                                <Series>
                                    <asp:Series Name="Series1" LabelFormat="C"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                        <AxisX>
                                            <MajorGrid Enabled="false" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <b>&nbsp;<asp:Label ID="Label1" runat="server" Text="Monto total de costos"></asp:Label></b>&nbsp;
                        </td>
                        <td>
                            <b>&nbsp;<asp:Label ID="Label2" runat="server" Text="Monto total de margen"></asp:Label></b>&nbsp;
                        </td>
                        <td>
                            <b>&nbsp;<asp:Label ID="Label3" runat="server" Text=""></asp:Label></b>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="margin-left: 40px">
                            <asp:Chart ID="Chart4" runat="server" Palette="None" PaletteCustomColors="DodgerBlue; Maroon; DarkGreen; DarkGoldenrod; DarkSlateGray; Indigo; DimGray">
                                <Series>
                                    <asp:Series Name="Series1" LabelFormat="C"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                        <AxisX>
                                            <MajorGrid Enabled="false" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                        <td>
                            <asp:Chart ID="Chart5" runat="server" Palette="None" PaletteCustomColors="DodgerBlue; Maroon; DarkGreen; DarkGoldenrod; DarkSlateGray; Indigo; DimGray">
                                <Series>
                                    <asp:Series Name="Series1" LabelFormat="C"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                        <AxisX>
                                            <MajorGrid Enabled="false" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <b>&nbsp;<asp:Label ID="Label4" runat="server" Text="Cotizaciones mayores del 40% de margen"></asp:Label></b>&nbsp;
                        </td>
                        <td>
                            <b>&nbsp;<asp:Label ID="Label5" runat="server" Text="Cotizaciones entre el 30% y 40% de margen"></asp:Label></b>&nbsp;
                        </td>
                        <td>
                            <b>&nbsp;<asp:Label ID="Label6" runat="server" Text="Cotizaciones entre el 20% y 30% de margen"></asp:Label></b>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Chart ID="Chart7" runat="server" Palette="None" PaletteCustomColors="DodgerBlue; Maroon; DarkGreen; DarkGoldenrod; DarkSlateGray; Indigo; DimGray">
                                <Series>
                                    <asp:Series Name="Series1"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                        <AxisX>
                                            <MajorGrid Enabled="false" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                        <td>
                            <asp:Chart ID="Chart8" runat="server" Palette="None" PaletteCustomColors="DodgerBlue; Maroon; DarkGreen; DarkGoldenrod; DarkSlateGray; Indigo; DimGray">
                                <Series>
                                    <asp:Series Name="Series1"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                        <AxisX>
                                            <MajorGrid Enabled="false" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                        <td>
                            <asp:Chart ID="Chart9" runat="server" Palette="None" PaletteCustomColors="DodgerBlue; Maroon; DarkGreen; DarkGoldenrod; DarkSlateGray; Indigo; DimGray">
                                <Series>
                                    <asp:Series Name="Series1"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                        <AxisX>
                                            <MajorGrid Enabled="false" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <b>&nbsp;<asp:Label ID="Label7" runat="server" Text="Cotizaciones menores al 20% de margen"></asp:Label></b>&nbsp;
                        </td>
                        <td>
                            <b>&nbsp;<asp:Label ID="Label8" runat="server" Text=""></asp:Label></b>&nbsp;
                        </td>
                        <td>
                            <b>&nbsp;<asp:Label ID="Label9" runat="server" Text=""></asp:Label></b>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Chart ID="Chart10" runat="server" Palette="None" PaletteCustomColors="DodgerBlue; Maroon; DarkGreen; DarkGoldenrod; DarkSlateGray; Indigo; DimGray">
                                <Series>
                                    <asp:Series Name="Series1"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                        <AxisX>
                                            <MajorGrid Enabled="false" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
