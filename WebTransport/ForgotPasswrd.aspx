<%@ Page Language="C#" AutoEventWireup="true" Title="Forgot password" CodeBehind="ForgotPasswrd.aspx.cs"
    Inherits="WebTransport.ForgotPasswrd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="description" content="">
    <meta name="author" content="">
    <meta name="robots" content="noindex, nofollow">
    <meta name="viewport" content="width=device-width,initial-scale=1,maximum-scale=1.0">
    <link rel="shortcut icon" href="img/favicon.png">
    <link rel="stylesheet" href="css/bootstrap.min-3.3.css">
    <link rel="stylesheet" href="css/plugins-3.4.css">
    <link rel="stylesheet" href="css/main-3.3.css">
    <link rel="stylesheet" href="css/themes-3.1.css">
    <link rel="stylesheet" href="css/custom.css">
    <script src="js/vendor/modernizr-respond.min.js"></script>
</head>
<body>
    <img src="img/home-bg.jpg" alt="Login Full Background" class="full-bg animation-pulseSlow">
    <div id="login-container" class="animation-fadeIn">
        <div class="login-title text-center">
        </div>
        <div class="block push-bit">
            <asp:MultiView ID="multiResetPassword" runat="server">
                <asp:View ID="viewforgot" runat="server">
                    <div style="padding-left: 10px;">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
                    </div>
                    <header class="panel-heading">                 
                      <div style="float:left;">                       
                      <strong>Forgot password</strong></div>
                    </header>
                    <form runat="server" id="formlogin" class="form-horizontal form-bordered form-control-borderless">
                    <div class="form-group">
                        <div class="col-xs-12">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="gi gi-envelope"></i></span>
                                <asp:TextBox ID="txtEmail" runat="server" class="form-control input-lg" MaxLength="50"
                                    placeholder="User email ID" TabIndex="1"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" Display="Dynamic" ForeColor="Red"
                                ControlToValidate="txtEmail" ValidationGroup="Save" ErrorMessage="<br />Please enter email id!"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <asp:Label ID="lblerror" runat="server" ForeColor="Red"></asp:Label>
                    <div class="form-group form-actions">
                        <div class="col-xs-4">
                            <a href="Login.aspx">Login</a>
                        </div>
                        <div class="col-xs-8 text-right">
                            <asp:Button ID="imgbtn" class="btn btn-sm btn-primary" Text="Submit" runat="server"
                                TabIndex="2" ValidationGroup="Save" OnClick="imgbtn_Click1" />
                        </div>
                    </div>
                    </form>
                </asp:View>
                <asp:View ID="view1" runat="server">
                    <header class="panel-heading text-center">
                      <div style="float:left;">
                      <strong>Reset Password</strong></div>
                    </header>
                    <form runat="server" id="form2" class="form-horizontal form-bordered form-control-borderless">
                    <div class="form-group">
                        <div class="col-xs-12">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="gi gi-asterisk"></i></span>
                                <asp:TextBox ID="txtPassword" runat="server" class="form-control input-lg" MaxLength="50" Autocomplete="Off"
                                    TextMode="Password" placeholder="Password" TabIndex="1"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                CssClass="classValidation" SetFocusOnError="true" ControlToValidate="txtPassword"
                                ValidationGroup="Save" ErrorMessage="Please enter password!"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-xs-12">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="gi gi-asterisk"></i></span>
                                <asp:TextBox ID="txtConfirmPswd" runat="server" class="form-control input-lg" MaxLength="50" Autocomplete="Off"
                                    TextMode="Password" placeholder="Confirm Password" TabIndex="2"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                CssClass="classValidation" ControlToValidate="txtConfirmPswd" SetFocusOnError="true"
                                ValidationGroup="Save" ErrorMessage="Please enter confirm password!"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cmpvalidtr" runat="server" CssClass="classValidation" Display="Dynamic"
                                ErrorMessage="password does not match!" SetFocusOnError="true" ValidationGroup="Save"
                                ControlToValidate="txtConfirmPswd" ControlToCompare="txtPassword"></asp:CompareValidator>
                        </div>
                    </div>
                    <div class="form-group form-actions">
                        <div class="col-xs-4">
                            &nbsp;
                        </div>
                        <div class="col-xs-8 text-right">
                            <asp:Button ID="btnResetPassword" class="btn btn-sm btn-success" Text="Reset Password"
                                runat="server" CausesValidation="true" TabIndex="3" ValidationGroup="Save" OnClick="btnResetPassword_OnClick" />
                        </div>
                    </div>
                    </form>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
    <script src="js/vendor/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="js/vendor/bootstrap.min-3.3.js" type="text/javascript"></script>
    <script src="js/plugins-3.4.js" type="text/javascript"></script>
    <script src="js/app-3.3.js" type="text/javascript"></script>
</body>
</html>
