Imports System.Web.UI.DataVisualization.Charting
Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Indicadores4

    Inherits CoflexWebPage

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)


        If Not IsPostBack Then
            Dim r As New Globalization.CultureInfo("es-us")
            r.NumberFormat.CurrencyDecimalSeparator = "."
            r.NumberFormat.NumberDecimalSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture = r


            Me.TextBox1.Text = DateTime.Today.AddMonths(-1).AddDays(1 - DateTime.Today.Day)
            Me.TextBox2.Text = DateTime.Today.AddMonths(1).AddDays(-DateTime.Today.Day)

            PrintGraph()

        End If
    End Sub

    Private Sub PrintGraph()

        Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.INDICATORS & "?min=" & Me.TextBox1.Text & "&max=" & Me.TextBox2.Text,, Session("access_token"))
        Dim o = JObject.Parse(jsonResponse)
        Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
        If (statusCode >= 200 And statusCode < 400) Then
            Dim detail = o.GetValue("detail").Value(Of JArray)
            Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)

            Table.Columns.Add()

            ''Número de cotizaciones generadas en el período de tiempo
            Me.Chart1.Series("Series1").XValueMember = "MesLetra"
            Me.Chart1.Series("Series1").YValueMembers = "NCotizaciones"
            Me.Chart1.Series("Series1").AxisLabel = "NCotizaciones"
            Me.Chart1.Series("Series1").SmartLabelStyle.Enabled = True
            Me.Chart1.Series("Series1").IsValueShownAsLabel = True
            Chart1.DataSource = Table
            Chart1.DataBind()

            ''Monto total cotizado en el período
            Me.Chart2.Series("Series1").XValueMember = "MesLetra"
            Me.Chart2.Series("Series1").YValueMembers = "MontoPromedioPesos"
            Me.Chart2.Series("Series1").SmartLabelStyle.Enabled = True
            Me.Chart2.Series("Series1").IsValueShownAsLabel = True
            Chart2.DataSource = Table
            Chart2.DataBind()

            ''Monto promedio de las cotizaciones del período
            Me.Chart3.Series("Series1").XValueMember = "MesLetra"
            Me.Chart3.Series("Series1").YValueMembers = "MPENC"
            Me.Chart3.Series("Series1").SmartLabelStyle.Enabled = True
            Me.Chart3.Series("Series1").IsValueShownAsLabel = True
            Chart3.DataSource = Table
            Chart3.DataBind()

            ''Monto total de costos de las cotizaciones generadas en el período;
            Me.Chart4.Series("Series1").XValueMember = "MesLetra"
            Me.Chart4.Series("Series1").YValueMembers = "MontoTotalCostos"
            Me.Chart4.Series("Series1").SmartLabelStyle.Enabled = True
            Me.Chart4.Series("Series1").IsValueShownAsLabel = True
            Chart4.DataSource = Table
            Chart4.DataBind()

            ''Monto total de margen de las cotizaciones generadas en el período;
            Me.Chart5.Series("Series1").XValueMember = "MesLetra"
            Me.Chart5.Series("Series1").YValueMembers = "MargenPesos"
            Me.Chart5.Series("Series1").SmartLabelStyle.Enabled = True
            Me.Chart5.Series("Series1").IsValueShownAsLabel = True
            Chart5.DataSource = Table
            Chart5.DataBind()

            ''Número de cotizaciones que están por arriba del 40% de margen en el período;
            Me.Chart7.Series("Series1").XValueMember = "MesLetra"
            Me.Chart7.Series("Series1").YValueMembers = "NCMPM40"
            Me.Chart7.Series("Series2").YValueMembers = "NCMPM3040"
            Me.Chart7.Series("Series3").YValueMembers = "NCMPM2030"
            Me.Chart7.Series("Series4").YValueMembers = "NCMPM20"
            Me.Chart7.Series("Series1").SmartLabelStyle.Enabled = True
            Me.Chart7.Series("Series1").IsValueShownAsLabel = True
            Me.Chart7.Series("Series2").IsValueShownAsLabel = True
            Me.Chart7.Series("Series3").IsValueShownAsLabel = True
            Me.Chart7.Series("Series4").IsValueShownAsLabel = True

            'Chart7.Legends.Add(New Legend("Legend2"))
            'Chart7.Legends("Legend2").DockedToChartArea = "ChartArea1"
            'Chart7.Series("Series2").Legend = "Legend2"
            'Chart7.Series("Series2").IsVisibleInLegend = True
            Chart7.Series("Series1").LegendText = "Mayor40%"
            Chart7.Series("Series2").LegendText = "30%a40%"
            Chart7.Series("Series3").LegendText = "20%a30%"
            Chart7.Series("Series4").LegendText = "Menor20%"

            Chart7.DataSource = Table
            Chart7.DataBind()

            '''Número de cotizaciones que están entre el 30% y el 40% de margen en el período;
            'Me.Chart8.Series("Series1").XValueMember = "MesLetra"
            'Me.Chart8.Series("Series1").YValueMembers = "NCMPM3040"
            'Me.Chart8.Series("Series1").SmartLabelStyle.Enabled = True
            'Me.Chart8.Series("Series1").IsValueShownAsLabel = True
            'Chart8.DataSource = Table
            'Chart8.DataBind()

            '''Número de cotizaciones que están entre el 20% y el 30% de margen en el período;
            'Me.Chart9.Series("Series1").XValueMember = "MesLetra"
            'Me.Chart9.Series("Series1").YValueMembers = "NCMPM2030"
            'Me.Chart9.Series("Series1").SmartLabelStyle.Enabled = True
            'Me.Chart9.Series("Series1").IsValueShownAsLabel = True
            'Chart9.DataSource = Table
            'Chart9.DataBind()

            '''Número de cotizaciones que están por debajo del 20% de margen en el período
            'Me.Chart10.Series("Series1").XValueMember = "MesLetra"
            'Me.Chart10.Series("Series1").YValueMembers = "NCMPM20"
            'Me.Chart10.Series("Series1").SmartLabelStyle.Enabled = True
            'Me.Chart10.Series("Series1").IsValueShownAsLabel = True
            'Chart10.DataSource = Table
            'Chart10.DataBind()

        End If
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        PrintGraph()
    End Sub

    Protected Sub ButtonBack_Click(sender As Object, e As EventArgs) Handles ButtonBack.Click
        Response.Redirect("~/Estimaciones/ResumenEstimaciones.aspx")
    End Sub
End Class