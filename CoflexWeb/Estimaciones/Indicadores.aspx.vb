Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Indicadores
    Inherits CoflexWebPage

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim r As New Globalization.CultureInfo("es-ES")
        r.NumberFormat.CurrencyDecimalSeparator = "."
        r.NumberFormat.NumberDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture = r

        '' QuotationsIndicatorProcedure '2017-05-08' , '2017-05-13'
        ''api/Indicator?min={min}&max={max}
        ''QuotationsIndicatorProcedure '2017-05-08' , '2017-05-13'

        Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.INDICATORS & "?min=" & "'2017-05-08'" & "&max=" & "'2017-05-13'" & "&estatus={estatus}&estatusV={estatusV}&vendedor={vendedor}&cliente={cliente}",, Session("access_token"))
        Dim o = JObject.Parse(jsonResponse)
        Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
        If (statusCode >= 200 And statusCode < 400) Then
            Dim detail = o.GetValue("detail").Value(Of JArray)
            Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)
            Table.DefaultView.Sort = "PPN_I asc"
            'Me.DDComponente.DataSource = Table
            'Me.DDComponente.DataValueField = "PPN_I"
            'Me.DDComponente.DataTextField = "PPN_I"
            'Me.DDComponente.DataBind()


            Me.Chart1.Series("Series1").XValueMember = "MINDATE"
            Me.Chart1.Series("Series1").YValueMembers = "NCotizaciones"

            Chart1.DataSource = Table
            Chart1.DataBind()
        End If

    End Sub

End Class