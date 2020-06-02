<%@ Page Title="Item Group Master" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="ItmGrpMaster.aspx.cs" Inherits="WebTransport.ItmGrpMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-5 center-block">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><i class="animated-icon fas fa-gas-pump"></i>Item Group Master </h5>
                            <div class="title-action">
                                <asp:Label id="lblViewList" runat="server"><a class="btn-action white-o" href="ManageItmGrpMaster.aspx"><i class="fa fa-list"></i>List</a></asp:Label>
                            </div>
                        </div>
                        <div class="ibox-content main-form scroll-pane">
                            <div class="col-sm-12 no-pad">
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Item Group Type</label>
                                        <%--<input name="ctl00$MainContent$txtUOMName" type="text" maxlength="50" id="MainContent_txtUOMName" class="form-control required" autocomplete="off" onkeypress="return allowAlphabetAndNumer(event);" style="z-index: 5; background-color: transparent;">--%>
                                        <asp:DropDownList ID="ddlItemType" CssClass="form-control required" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Group Name</label>
                                        <%--<input name="ctl00$MainContent$txtUOMDesc" type="text" maxlength="100" id="MainContent_txtUOMDesc" class="form-control" autocomplete="off" onkeypress="return allowAlphabetAndNumer(event);" style="z-index: 5; background-color: transparent;">--%>
                                        <asp:TextBox ID="txtGName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvGName" runat="server" ControlToValidate="txtGName" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Enter Group Name!" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
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
            <asp:HiddenField ID="hidIGrpMastidno" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
