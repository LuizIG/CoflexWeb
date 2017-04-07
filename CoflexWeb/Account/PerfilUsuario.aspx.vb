Imports CoflexWeb.CoflexWeb.Services.Web
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class PerfilUsuario
    Inherits CoflexWebPage

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsPostBack Then
            MyBase.Page_Load(sender, e)
            Dim userId As String = Request.QueryString("id")
            Dim jsonResponse = CoflexWebServices.doGetRequest(CoflexWebServices.USERS & "/" & userId, , Session("access_token"))
            Dim o = JObject.Parse(jsonResponse)
            Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
            If (statusCode >= 200 And statusCode < 400) Then
                Dim detail = o.GetValue("detail").Value(Of JObject)

                Me.UserName.Text = detail.GetValue("Email")
                Me.Name.Text = detail.GetValue("Name")
                Me.LastName.Text = detail.GetValue("PaternalSurname")
                Me.MotherSurname.Text = detail.GetValue("MaternalSurname")

                Dim jsonResponseGV = CoflexWebServices.doGetRequest(CoflexWebServices.ROLES)
                Dim oGV = JObject.Parse(jsonResponseGV)
                Dim statusCodeGV = oGV.GetValue("statusCode").Value(Of Integer)
                If (statusCodeGV >= 200 And statusCodeGV < 400) Then
                    Dim detailGV = oGV.GetValue("detail").Value(Of JArray)
                    Dim TableGV = JsonConvert.DeserializeObject(Of DataTable)(detailGV.ToString)
                    Me.GridRoles.DataSource = TableGV
                    Me.GridRoles.DataBind()
                End If

                Dim Table = JsonConvert.DeserializeObject(Of DataTable)(detail.GetValue("Roles").Value(Of JArray).ToString)
                For Each row As DataRow In Table.Rows
                    ''strDetail = row("Detail")
                    For Each rowCH As GridViewRow In Me.GridRoles.Rows
                        If rowCH.RowType = DataControlRowType.DataRow Then
                            Dim chkRow As CheckBox = TryCast(rowCH.Cells(0).FindControl("chkSelect"), CheckBox)
                            If rowCH.Cells(1).Text = row(0).ToString Then
                                chkRow.Checked = True
                            End If
                        End If
                    Next
                Next row
            Else

            End If
        End If
    End Sub

    Protected Sub BRoles_Click(sender As Object, e As EventArgs) Handles BRoles.Click
        For Each row As GridViewRow In Me.GridRoles.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkSelect"), CheckBox)
                If chkRow.Checked Then
                    Dim Registro As New JObject
                    With Registro
                        .Add("UserId", Request.QueryString("id"))
                        .Add("RoleName", row.Cells(1).Text)
                    End With

                    Dim jsonResponse = CoflexWebServices.doPostRequest(CoflexWebServices.ROLES_ADMIN_ADD, Registro.ToString,, Session("access_token"))
                    Dim o = JObject.Parse(jsonResponse)
                    Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
                    If (statusCode >= 200 And statusCode < 500) Then
                        Me.ErrorMessage.Text = "Roles Registrado"
                    Else
                        Dim errorMessage = o.GetValue("errorMessage").Value(Of String)
                        Me.ErrorMessage.Text = errorMessage
                    End If
                    Registro = Nothing
                Else
                    Dim Registro As New JObject
                    With Registro
                        .Add("UserId", Request.QueryString("id"))
                        .Add("RoleName", row.Cells(1).Text)
                    End With

                    Dim jsonResponse = CoflexWebServices.doPostRequest(CoflexWebServices.ROLES_ADMIN_DEL, Registro.ToString,, Session("access_token"))
                    Dim o = JObject.Parse(jsonResponse)
                    Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
                    If (statusCode >= 200 And statusCode < 500) Then
                        Me.ErrorMessage.Text = "Roles Registrado"
                    Else
                        Dim errorMessage = o.GetValue("errorMessage").Value(Of String)
                        Me.ErrorMessage.Text = errorMessage
                    End If
                    Registro = Nothing
                End If
            End If
        Next
    End Sub

    Protected Sub btn_cambiar_pass_Click(sender As Object, e As EventArgs) Handles btn_cambiar_pass.Click
        Dim Registro As New JObject
        With Registro
            .Add("UserId", Request.QueryString("id"))
            .Add("NewPassword", Me.Password.Text)
            .Add("ConfirmPassword", Me.ConfirmPassword.Text)
        End With

        Dim jsonResponse = CoflexWebServices.doPostRequest(CoflexWebServices.SETPASSWORD, Registro.ToString,, Session("access_token"))
        Dim o = JObject.Parse(jsonResponse)
        Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
        If (statusCode >= 200 And statusCode < 400) Then
            Me.ErrorMessage.Text = "Usuario Registrado"
        Else
            Dim errorMessage = o.GetValue("errorMessage").Value(Of String)
            Me.ErrorMessage.Text = errorMessage
        End If
        Registro = Nothing
    End Sub

    Protected Sub btn_edit_profile_Click(sender As Object, e As EventArgs) Handles btn_edit_profile.Click
        Dim Registro As New JObject
        With Registro
            .Add("IdUsuario", Request.QueryString("id"))
            .Add("Email", Me.UserName.Text)
            .Add("Name", Me.Name.Text)
            .Add("PaternalSurname", Me.LastName.Text)
            .Add("MaternalSurname", Me.MotherSurname.Text)
        End With

        Dim jsonResponse = CoflexWebServices.doPostRequest(CoflexWebServices.USERS_ALT, Registro.ToString,, Session("access_token"))
        Dim o = JObject.Parse(jsonResponse)
        Dim statusCode = o.GetValue("statusCode").Value(Of Integer)
        If (statusCode >= 200 And statusCode < 400) Then
            Me.ErrorMessage.Text = "Usuario Actualizado"
        Else
            Dim errorMessage = o.GetValue("errorMessage").Value(Of String)
            Me.ErrorMessage.Text = errorMessage
        End If
        Registro = Nothing
    End Sub
End Class