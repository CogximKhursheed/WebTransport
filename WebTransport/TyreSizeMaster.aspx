<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="TyreSizeMaster.aspx.cs" Inherits="WebTransport.TyreSizeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-5 center-block">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><i class="animated-icon fas fa-gas-pump"></i>Tyre Size Master </h5>
                            <div class="title-action">
                                <asp:Label ID="lblViewList" runat="server"><a class="btn-action white-o" href="TyreSizeMasterList.aspx"><i class="fa fa-list"></i>List</a></asp:Label>
                            </div>
                        </div>
                        <div class="ibox-content main-form scroll-pane">
                            <div class="col-sm-12 no-pad">
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Tyre Size <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txttyresize" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtyresize" runat="server" ControlToValidate="txttyresize" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Tyre Size !" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="ibox-btn">
                            <div class="pull-right">
                                <div class="display-inline">
                                    <asp:LinkButton ID="lnkbtnNew" runat="server" OnClick="lnkbtnNew_Click" CssClass="btn btn-labeled btn-success"><span class="btn-label"><i class="fa fa-plus"></i></span>New</asp:LinkButton>
                                </div>
                                <div class="display-inline">
                                    <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" ValidationGroup="Save" OnClick="lnkbtnSave_Click" CssClass="btn btn-labeled btn-primary"><span class="btn-label"><i class="fa fa-save"></i></span>Save</asp:LinkButton>
                                </div>
                                <div class="display-inline">
                                    <asp:LinkButton ID="lnkbtnCancel" runat="server" OnClick="lnkbtnCancel_Click" CssClass="btn btn-labeled btn-danger"><span class="btn-label"><i class="fa fa-times"></i></span>Cancel</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--HIDDEN FIELDS--%>
            <asp:HiddenField ID="hidTyresizeIdno" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
