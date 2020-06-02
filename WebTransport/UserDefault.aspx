<%@ Page Title="User Default" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="UserDefault.aspx.cs" Inherits="WebTransport.UserDefault" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-5 center-block">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><i class="animated-icon fas fa-gas-pump"></i>User Default</h5>
                        </div>
                        <div class="ibox-content main-form scroll-pane">
                            <div class="col-sm-12 no-pad">
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Date Range <span class="required-field">*</span> </label>
                                        <asp:DropDownList ID="ddlDateRange" CssClass="form-control required" runat="server" AutoPostBack="false"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDateRange" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select DateRange !" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">User Name <span class="required-field">*</span> </label>
                                        <asp:DropDownList ID="drpUserNm" CssClass="form-control required" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpUserNm_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlPartyName" runat="server" ControlToValidate="drpUserNm" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select User Name !" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Loc.[From] <span class="required-field">*</span> </label>
                                        <asp:DropDownList ID="ddlFromCity" CssClass="form-control required" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlFromCity" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select From City !" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">State <span class="required-field">*</span> </label>
                                        <asp:DropDownList ID="ddlState" CssClass="form-control required" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">City <span class="required-field">*</span> </label>
                                        <asp:DropDownList ID="ddlCity" CssClass="form-control required" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Sender <span class="required-field">*</span> </label>
                                        <asp:DropDownList ID="ddlSender" CssClass="form-control required" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Item Name <span class="required-field">*</span> </label>
                                        <asp:DropDownList ID="ddlItemName" CssClass="form-control required" runat="server" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Unit <span class="required-field">*</span> </label>
                                        <asp:DropDownList ID="ddlunitname" CssClass="form-control required" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Tax Paid By <span class="required-field">*</span> </label>
                                        <asp:DropDownList ID="ddlTaxPaidBy" runat="server" CssClass="form-control required">
                                            <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Transporter" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Consigner" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Consignee" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">GR Type <span class="required-field">*</span> </label>
                                        <asp:DropDownList ID="ddlGRType" runat="server" CssClass="form-control required" AutoPostBack="True">
                                            <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Paid GR" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="TBB GR" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="To Pay GR" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="ibox-btn">
                            <div class="pull-right">
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
            <%--HIDDEN FIELDS--%>
            <asp:HiddenField ID="hidid" runat="server" />
            <asp:HiddenField ID="hidUsrDefaultID" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>

    <script language="javascript" type="text/javascript">
        function ShowMessage(value) {
            alert(value);
        }
    </script>
</asp:Content>
