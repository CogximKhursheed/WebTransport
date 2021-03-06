﻿<%@ Page Title="Grade Master" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="GradeMaster.aspx.cs" Inherits="WebTransport.GradeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-5 center-block">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><i class="animated-icon fas fa-gas-pump"></i>Grade Master </h5>
                            <div class="title-action">
                                <asp:Label ID="lblViewList" runat="server"><a class="btn-action white-o" href="GradeMasterList.aspx"><i class="fa fa-list"></i>List</a></asp:Label>
                            </div>
                        </div>
                        <div class="ibox-content main-form scroll-pane">
                            <div class="col-sm-12 no-pad">
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Grade Name <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txtGrdName" placeholder="Enter Grade Name" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvGName" runat="server" ControlToValidate="txtGrdName" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Enter Grade Name !" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="form-control-row">
                                        <div class="control-container col-sm-12">
                                            <label class="control-label">Abbreviation</label>
                                            <asp:TextBox ID="txtAbbreviation" placeholder="Enter Abbreviation" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                        </div>
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
            <asp:HiddenField ID="hidIGrpMastidno" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
