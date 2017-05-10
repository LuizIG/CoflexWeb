﻿
Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class ResumenEstimaciones2
    Inherits CoflexWebPage
    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        MyBase.Page_Load(sender, e)
        If Not IsPostBack Then
            Dim Response = CoflexWebServices.doGetRequest(CoflexWebServices.QUOTATIONS_SUMMARY, , Session("access_token"))
            Dim o = JObject.Parse(Response)
            Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                Dim detail = o.GetValue("detail").Value(Of JArray)

                Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.ToString)
                ViewState("CurrentTable") = Table
                Me.GridQuotations.DataSource = Table
                Me.GridQuotations.DataBind()

            End If
        End If
    End Sub

    Protected Sub ButtonEstimacionGo_Click(sender As Object, e As EventArgs) Handles ButtonEstimacionGo.Click
        Response.Redirect("/Estimaciones/Estimacion.aspx")
    End Sub

    Private Sub BTN_ACEPTAR_1_Click(sender As Object, e As EventArgs) Handles BTN_ACEPTAR_1.Click

        Dim quotations = quotations_reasign.Value.Split(",")

        For Each Q In quotations

            If Q IsNot Nothing And Q <> "" Then

                Dim response = CoflexWebServices.doPutRequest(CoflexWebServices.QUOTATIONS & "/" & Q & "?UserId=" & DDUsers.SelectedValue, "{}",, Session("access_token"))

                Dim o = JObject.Parse(response)
                Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
                If (statusCode >= 200 And statusCode < 400) Then


                End If
            End If

        Next

        Response.Redirect("ResumenEstimaciones.aspx")

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            Dim Qstring As String = "   "

            If Me.txtCotizacion.Text <> "" Then
                ''rows2 = dt.[Select]("CoflexId like '" & Me.txtCotizacion.Text & "%'")
                Qstring = Qstring + "CoflexId like '" & Me.txtCotizacion.Text & "%' and"
            End If

            If Me.txtVendedor.Text <> "" Then
                ''rows2 = dt.[Select]("vendor like '%" & Me.txtVendedor.Text & "%'")
                Qstring = Qstring + " vendor like '%" & Me.txtVendedor.Text & "%' and"
            End If

            If Me.txtCliente.Text <> "" Then
                ''rows2 = dt.[Select]("clientName like '%" & Me.txtCliente.Text & "%'")
                Qstring = Qstring + " clientName like '%" & Me.txtCliente.Text & "%' and"
            End If

            If Me.txtStatusCotiza.Text <> "" Then
                ''rows2 = dt.[Select]("clientName like '%" & Me.txtCliente.Text & "%'")
                Qstring = Qstring + " status = '" & Me.txtStatusCotiza.Text & "' and"
            End If

            If Me.txtVersion.Text <> "" Then
                ''rows2 = dt.[Select]("clientName like '%" & Me.txtCliente.Text & "%'")
                Qstring = Qstring + " VersionNumber = '" & Me.txtVersion.Text & "' and"
            End If

            If Me.txtFecIni.Text <> "" And Me.txtFecFin.Text <> "" Then
                ''rows2 = dt.[Select]("clientName like '%" & Me.txtCliente.Text & "%'")
                Qstring = Qstring + " Date >= '" & Me.txtFecIni.Text & "' and Date <= '" & Me.txtFecFin.Text & "' and"
            End If

            If Me.txtStatusVersion.Text <> "" Then
                ''rows2 = dt.[Select]("clientName like '%" & Me.txtCliente.Text & "%'")
                Qstring = Qstring + " VStatus = '" & Me.txtStatusVersion.Text & "' and"
            End If

            Qstring = Left(Qstring, Qstring.Length - 3)
            Dim rows2 = dt.[Select](Qstring)

            Dim dt1 As DataTable = rows2.CopyToDataTable()
            Me.GridQuotations.DataSource = dt1
            Me.GridQuotations.DataBind()

        End If


    End Sub
End Class





