<%@ Page Title=" Account head" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="AcntHeadMaintenace.aspx.cs" Inherits="WebTransport.AcntHeadMaintenace" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-5 center-block">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><i class="animated-icon fas fa-gas-pump"></i>Account Group</h5>
                            <div class="title-action">
                                <asp:Label ID="lblViewList" runat="server"><a class="btn-action white-o" href="ManageAcntHeadMaintenace.aspx"><i class="fa fa-list"></i>List</a></asp:Label>
                            </div>
                        </div>
                        <div class="ibox-content main-form scroll-pane">
                            <div class="col-sm-12 no-pad">
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Main Head Group <span class="required-field">*</span> </label>
                                        <asp:DropDownList ID="drpMHGrp" CssClass="form-control required" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvMHGrp" runat="server" ControlToValidate="drpMHGrp" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select Main Head Group!" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Account Group</label>
                                        <asp:TextBox ID="txtAcntGrp" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAcntGrp" runat="server" ControlToValidate="txtAcntGrp" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Enter Account Group!" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
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
                                    <asp:LinkButton ID="btnNew" runat="server" OnClick="btnNew_OnClick" CssClass="btn btn-labeled btn-success"><span class="btn-label"><i class="fa fa-plus"></i></span>New</asp:LinkButton>
                                </div>
                                <div class="display-inline">
                                    <asp:LinkButton ID="btnSavegroup" runat="server" CausesValidation="true" ValidationGroup="Save" OnClick="btnSavegroup_OnClick" CssClass="btn btn-labeled btn-primary"><span class="btn-label"><i class="fa fa-save"></i></span>Save</asp:LinkButton>
                                </div>
                                <div class="display-inline">
                                    <asp:LinkButton ID="btnCancle" runat="server" OnClick="btnCancle_OnClick" CssClass="btn btn-labeled btn-danger"><span class="btn-label"><i class="fa fa-times"></i></span>Cancel</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--HIDDEN FIELDS--%>
            <asp:HiddenField ID="hidaheadidno" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
