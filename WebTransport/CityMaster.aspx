<%@ Page Title="City Master" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="CityMaster.aspx.cs" Inherits="WebTransport.CityMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-5 center-block">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><i class="animated-icon fas fa-gas-pump"></i>Location/City Master </h5>
                            <div class="title-action">
                                <asp:Label ID="lblViewList" runat="server"><a class="btn-action white-o" href="ManageCityMaster.aspx"><i class="fa fa-list"></i>List</a></asp:Label>
                            </div>
                        </div>
                        <div class="ibox-content main-form scroll-pane">
                            <div class="col-sm-12 no-pad">
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">State Name <span class="required-field">*</span> </label>
                                        <asp:DropDownList ID="drpState" CssClass="form-control required" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="drpState" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select State !" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">City Name <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txtCityName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCityName" runat="server" ControlToValidate="txtCityName" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Enter City Name !" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Name [Hindi]</label>
                                        <asp:TextBox ID="txtNameHindi" Placeholder="Enter City Name in Hindi" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Abbreviation</label>
                                        <asp:TextBox ID="txtAbbreviation" Placeholder="Enter Abbreviation" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <div class="input-container checkbox-container">
                                            <label class="control-label">Active</label>
                                            <asp:CheckBox ID="chkStatus" CssClass="col-sm-1" runat="server" />
                                        </div>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <div class="input-container checkbox-container">
                                            <label class="control-label">As Location</label>
                                            <asp:CheckBox ID="chkLocation" CssClass="col-sm-1" onclick="ToggleActivation()" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Address1</label>
                                        <asp:TextBox ID="txtadd1" Enabled="false" Placeholder="Enter address" runat="server" CssClass="form-control" MaxLength="40" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Address2</label>
                                        <asp:TextBox ID="txtadd2" Enabled="false" Placeholder="Enter address" runat="server" CssClass="form-control" MaxLength="40" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">GSTIN No.</label>
                                        <asp:TextBox ID="txtGSTIN_No" Enabled="false" Placeholder="Enter GSTIN No" runat="server" CssClass="form-control" MaxLength="20" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">SAC Code</label>
                                        <asp:TextBox ID="txtSACCode" Enabled="false" Placeholder="Enter SAC Code" runat="server" CssClass="form-control" MaxLength="20" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Code sap</label>
                                        <asp:TextBox ID="txtCodeSap" Enabled="false" Placeholder="Enter Code sap" runat="server" CssClass="form-control" MaxLength="20" ClientIDMode="Static"></asp:TextBox>
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
            <%--HIDDEN FIELDS--%>
            <asp:HiddenField ID="hidcityidno" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <style>
        input.active {
            disabled: true !important;
        }
    </style>
    <script>
        (function ($) {
            $.fn.toggleDisabled = function () {
                return this.each(function () {
                    this.disabled = !this.disabled;
                });
            };
        })(jQuery);

        function ToggleActivation() {
            $('#chkLocation').toggleClass('active');
            ShowHide();
        }
        function ShowHide() {
            if ($('#chkLocation.active').length > 0) {
                $('#txtGSTIN_No').removeAttr('disabled');
                $('#txtSACCode').removeAttr('disabled');
                $('#txtCodeSap').removeAttr('disabled');
                $('#txtadd1').removeAttr('disabled');
                $('#txtadd2').removeAttr('disabled');

            }
            else {
                $('#txtGSTIN_No').prop('disabled', true);
                $('#txtSACCode').prop('disabled', true);
                $('#txtCodeSap').prop('disabled', true);
                $('#txtadd1').prop('disabled', true);
                $('#txtadd2').prop('disabled', true);
                $('#txtGSTIN_No').val('');
                $('#txtSACCode').val('');
                $('#txtCodeSap').val('');
                $('#txtadd1').val('');
                $('#txtadd2').val('');

            }
        }
        $(document).ready(function () {
            if ($('#chkLocation:checked').length > 0) { $('#chkLocation').addClass('active'); }
            ShowHide();
        });
    </script>
</asp:Content>
