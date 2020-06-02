<%@ Page Title="View Profile" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="EditProfile.aspx.cs" Inherits="WebTransport.EditProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-5 center-block">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><i class="animated-icon fas fa-gas-pump"></i>View Profile </h5>
                        </div>
                        <div class="ibox-content main-form scroll-pane">
                            <div class="col-sm-12 no-pad">
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">User Name <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter User Name !" SetFocusOnError="true" ValidationGroup="user"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Father's Name <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFatherName" runat="server" ControlToValidate="txtFatherName" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter father name !" SetFocusOnError="true" ValidationGroup="user"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Email <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Email !" SetFocusOnError="true" ValidationGroup="user"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" CssClass="classValidation" Display="Dynamic" SetFocusOnError="true" ValidationGroup="user" ErrorMessage="Not a valid email!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Password <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter password !" SetFocusOnError="true" ValidationGroup="user"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Address  </label>
                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Style="resize: none;" MaxLength="200"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter address !" SetFocusOnError="true" ValidationGroup="user"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">State <span class="required-field">*</span> </label>
                                        <asp:DropDownList ID="ddlState" CssClass="form-control required" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlState" runat="server" ControlToValidate="ddlState" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter State !" SetFocusOnError="true" ValidationGroup="user"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">City <span class="required-field">*</span> </label>
                                        <asp:DropDownList ID="ddlCity" CssClass="form-control required" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCity" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter city !" SetFocusOnError="true" ValidationGroup="user"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Phone </label>
                                        <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Mobile <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ControlToValidate="txtMobile" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter mobile !" SetFocusOnError="true" ValidationGroup="user"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMobile" SetFocusOnError="true" ValidationGroup="user" ErrorMessage="Not a valid Mobile No!" CssClass="classValidation" Display="Dynamic" ValidationExpression="^[7-9][0-9]{9}$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">D.O.B </label>
                                        <asp:TextBox ID="txtDOB" runat="server" CssClass="input-sm datepicker form-control " autocomplete="off" onblur="CopyDate()" MaxLength="10"></asp:TextBox>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">D.O.J </label>
                                        <asp:TextBox ID="txtDOJ" runat="server" CssClass="input-sm datepicker form-control" MaxLength="10" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Designation </label>
                                        <asp:DropDownList ID="ddlDesig" CssClass="form-control required" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Gender </label>
                                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control required">
                                            <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Computer </label>
                                        <asp:DropDownList ID="ddlComputerUser" runat="server" CssClass="form-control required">
                                            <asp:ListItem Text="Yes" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <div class="input-container checkbox-container">
                                            <%--<label class="col-md-4 control-label" for="example-chosen-multiple" style="width:17%;">Location</label>--%>
                                            <asp:Label ID="lblloc" class="col-md-4 control-label" for="example-chosen-multiple" runat="server" Style="width: 17%;" Text="Location" Font-Bold="true"></asp:Label>
                                            <asp:CheckBoxList ID="chklistFromcity" runat="server" RepeatColumns="8" RepeatDirection="Horizontal"></asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--HIDDEN FIELDS--%>
            <asp:HiddenField ID="hidmindate" runat="server" />
            <asp:HiddenField ID="hidmaxdate" runat="server" />
            <asp:HiddenField ID="hidPass" runat="server" />
            <asp:HiddenField ID="hid" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">
        SetFocus();
        function SetFocus() {
            $('input[type="text"]').focus(function () {
                $(this).addClass("focus");
            });
            $('input[type="text"]').blur(function () {
                $(this).removeClass("focus");
            });
            $("select").focus(function () {
                $(this).addClass("focus");
            });
            $("select").blur(function () {
                $(this).removeClass("focus");
            });
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_beginRequest(function () {
            SetFocus();
            setDatecontrol();
        });

        prm.add_endRequest(function () {
            SetFocus();
            setDatecontrol();
        });

        $(document).ready(function () {
            setDatecontrol();
        });

        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $('#<%=txtDOB.ClientID %>').datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });

            $('#<%=txtDOJ.ClientID %>').datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
        }
        function ShowMessage(value) {
            alert(value);
        }
        function ShowInvoiceDetails() {
            document.getElementById("dvInvoiceDetails").style.display = 'block';
        }
        function HideInvoiceDetails() {
            document.getElementById("dvInvoiceDetails").style.display = 'none';
        }
    </script>
</asp:Content>
