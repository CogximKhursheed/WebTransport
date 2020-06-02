<%@ Page Title="Driver Master" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="DriverMaster.aspx.cs" Inherits="WebTransport.DriverMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-5 center-block">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><i class="animated-icon fas fa-gas-pump"></i>Driver Master</h5>
                            <div class="title-action">
                                <asp:Label ID="lblViewList" runat="server"><a class="btn-action white-o" href="ManageDriverMaster.aspx"><i class="fa fa-list"></i>List</a></asp:Label>
                            </div>
                        </div>
                        <div class="ibox-content main-form scroll-pane">
                            <div class="col-sm-12 no-pad">
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Driver Name <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txtDriverName" autocomplete="off" runat="server" CssClass="form-control" MaxLength="30" placeholder="Enter Driver Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvColrName" runat="server" ControlToValidate="txtDriverName" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Driver name !" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Name[Hindi]</label>
                                        <asp:TextBox ID="txtDRVHindi" autocomplete="off" runat="server" CssClass="form-control" MaxLength="30" placeholder="Enter Name In Hindi"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">License No.</label>
                                        <asp:TextBox ID="txtlicense" autocomplete="off" runat="server" CssClass="form-control" MaxLength="30" placeholder="Enter License No."></asp:TextBox>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Expiry Date</label>
                                        <asp:TextBox ID="txtExpiryDate" autocomplete="off" runat="server" CssClass="input-sm datepicker form-control" MaxLength="10" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" onchange="Focus()"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">License Authority</label>
                                        <asp:TextBox ID="txtauthority" autocomplete="off" runat="server" CssClass="form-control" MaxLength="30" placeholder="Enter License Auhtority"></asp:TextBox>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Account No.</label>
                                        <asp:TextBox ID="txtaccountno" autocomplete="off" runat="server" CssClass="form-control" MaxLength="50" placeholder="Enter Account No."></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Guarantor</label>
                                        <asp:DropDownList ID="Drpgurenter" CssClass="form-control required" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <div class="input-container checkbox-container">
                                            <label class="control-label">Verified</label>
                                            <asp:CheckBox ID="chkVarified" runat="server" />
                                        </div>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <div class="input-container checkbox-container">
                                            <label class="control-label">Active</label>
                                            <asp:CheckBox ID="chkStatus" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="ibox-btn">
                            <div class="pull-right">
                                <div class="display-inline">
                                    <asp:LinkButton ID="lnkbtnNew" runat="server" OnClick="lnkbtnNew_OnClick" CssClass="btn btn-labeled btn-success"><span class="btn-label"><i class="fa fa-plus"></i></span>New</asp:LinkButton>
                                </div>
                                <div class="display-inline">
                                    <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" ValidationGroup="Save" OnClick="lnkbtnSave_OnClick" CssClass="btn btn-labeled btn-primary"><span class="btn-label"><i class="fa fa-save"></i></span>Save</asp:LinkButton>
                                </div>
                                <div class="display-inline">
                                    <asp:LinkButton ID="lnkbtnCancel" runat="server" OnClick="lnkbtnCancel_OnClick" CssClass="btn btn-labeled btn-danger"><span class="btn-label"><i class="fa fa-times"></i></span>Cancel</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--HIDEEN FIELDS--%>
            <asp:HiddenField ID="hidDrividno" runat="server" />
            <asp:HiddenField ID="hidmindate" runat="server" />
            <asp:HiddenField ID="hidmaxdate" runat="server" />
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

        function ShowModalPopup() {
            ShowDialog(true);
        }
        function ShowDialog(modal) {
            // $("#overlay").show();
            $("#dialog").show();
            $("#dialog").fadeIn(300);

            if (modal) {
                $("#dialog").unbind("click");
                //$("#overlay").unbind("click");
            }
            else {
                //  $("#overlay").click(function (e) {
                $("#dialog").click(function (e) {
                    HideDialog();
                });
            }
        }

        function HideDialog() {
            //   $("#overlay").hide();
            $("#dialog").fadeOut(300);
        }

        function Focus() {
            $("#txtExpiryDate").focus();
        }

        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $("#<%=txtExpiryDate.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
        }
    </script>
</asp:Content>
