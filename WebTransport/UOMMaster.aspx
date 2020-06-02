<%@ Page Title="UOM Master" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="UOMMaster.aspx.cs" Inherits="WebTransport.UOMMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-5 center-block">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><i class="animated-icon fas fa-gas-pump"></i>UOM MASTER</h5>
                            <div class="title-action">
                                <asp:Label ID="lblViewList" runat="server"><a class="btn-action white-o" href="ManageUOMMaster.aspx"><i class="fa fa-list"></i>List</a></asp:Label>
                            </div>
                        </div>
                        <div class="ibox-content main-form scroll-pane">
                            <div class="col-sm-12 no-pad">
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">UOM Name <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txtUOMName" runat="server" CssClass="form-control" MaxLength="10" onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false" OnTextChanged="txtUOMName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvUOMName" runat="server" ControlToValidate="txtUOMName" CssClass="classValidation" ValidationGroup="Save" ErrorMessage="Please Enter UOM Name" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">UOM Name[Hindi]</label>
                                        <asp:TextBox ID="txtNameHindi" runat="server" CssClass="form-control" MaxLength="50" onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Description</label>
                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" MaxLength="50" onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox>
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
            <%--HIDDEN FIELDS--%>
            <asp:HiddenField ID="hiduomidno" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
