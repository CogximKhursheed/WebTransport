<%@ Page Title="RESET PASSWORD" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="WebTransport.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-5 center-block">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><i class="animated-icon fas fa-gas-pump"></i>Reset Password </h5>
                            <div class="u-msg-box" id="trmsg" runat="server" visible="false">
                                <asp:Label ID="Label1" runat="server" Text="You have not changed your password for more than 30 Days. Please change your password to proceed further."></asp:Label>
                            </div>
                        </div>
                        <div class="ibox-content main-form scroll-pane">
                            <div class="col-sm-12 no-pad">
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Old Password <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txtOldpaswrd" runat="server" CssClass="form-control" MaxLength="20" onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvUOMName" runat="server" ControlToValidate="txtOldpaswrd" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Enter Old Password !" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">New Password <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txtNewPasswrd" TextMode="Password" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNewPaswrd" runat="server" ControlToValidate="txtNewPasswrd" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Enter New Password !" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="Regex5" runat="server" ControlToValidate="txtNewPasswrd" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,10}" ErrorMessage="Password must contain: Minimum 8 and Maximum 10 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character" ForeColor="Red" ValidationGroup="Save" />
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Confirm Password <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txtCnfrmpaswrd" TextMode="Password" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCnfrmPswrd" runat="server" ControlToValidate="txtCnfrmpaswrd" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Enter Confirm Password !" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="comparePasswords" runat="server" ControlToCompare="txtNewPasswrd" CssClass="classValidation" ControlToValidate="txtCnfrmpaswrd" ErrorMessage="New and Confirm Password Not Match." Display="Dynamic" />
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
            <asp:HiddenField ID="hiduomidno" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
