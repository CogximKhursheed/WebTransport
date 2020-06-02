<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Title="User Login"
    Inherits="WebTransport.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
            <!-- <h1>Transport Management System</h1> -->
        </div>
        <div class="block push-bit">
            <form runat="server" id="formlogin" class="form-horizontal form-bordered form-control-borderless">
            <div class="form-group">
                <div class="col-xs-12">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="gi gi-envelope"></i></span>
                        <asp:TextBox ID="txtEmail" runat="server" class="form-control input-lg" MaxLength="50"
                            placeholder="Username" TabIndex="1"></asp:TextBox>
                        <%--	<input type="text" id="login-email" name="login-email" class="form-control input-lg" placeholder="Email">--%>
                    </div>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" Display="Dynamic" ForeColor="Red"
                        ControlToValidate="txtEmail" ValidationGroup="Save" ErrorMessage="<br />Please enter email id!"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <div class="col-xs-12">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="gi gi-asterisk"></i></span>
                        <asp:TextBox ID="txtPassword" runat="server" class="form-control input-lg" TextMode="Password"
                            placeholder="Password" MaxLength="50" TabIndex="2"></asp:TextBox>
                        <%-- <input type="password" id="login-password" name="login-password" class="form-control input-lg" placeholder="Password">--%>
                    </div>
                    <asp:RequiredFieldValidator ID="rfvPAssword" runat="server" ForeColor="Red" Display="Dynamic"
                        ControlToValidate="txtPassword" ValidationGroup="Save" ErrorMessage="<br />Please enter password!"></asp:RequiredFieldValidator>
                </div>
            </div>
            <asp:Label ID="lblerror" runat="server" ForeColor="Red"></asp:Label>
            <div class="form-group form-actions">
                <div class="col-xs-4">
                    <label class="switch switch-primary" data-toggle="tooltip" title="Remember Me?">
                        <input type="checkbox" id="login-remember-me" name="login-remember-me" checked>
                        <span></span>
                    </label>
                </div>
                <div class="col-xs-8 text-right">
                    <asp:Button ID="imgbtn" class="btn btn-sm btn-primary" Text="Login To Dashboard"
                        runat="server" TabIndex="3" ValidationGroup="Save" OnClick="imgbtn_Click1" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-xs-12 text-center">
                    <a href="ForgotPasswrd.aspx" id="link-reminder-login"><small>Forgot password?</small></a>
                    &nbsp;&nbsp;-&nbsp;&nbsp; <a href="Register.aspx" id="link-register-login"><small>New
                        Registration</small></a>
                </div>
            </div>
            </form>
        </div>
        <div style="color: Red; font-weight: bold;">Support nos. 7414853123, 7414856123, 7414857123, 7414858123</div>
    </div>
    <script src="js/vendor/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="js/vendor/bootstrap.min-3.3.js" type="text/javascript"></script>
    <script src="js/plugins-3.4.js" type="text/javascript"></script>
    <script src="js/app-3.3.js" type="text/javascript"></script>
    <script src="js/pages/login_1.4.js" type="text/javascript"></script>
    <script>        $(function () { Login.init(); });</script>
</body>
<!-- END BODY -->
</html>
