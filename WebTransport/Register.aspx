<%@ Page Language="C#" AutoEventWireup="true" Title="User register" CodeBehind="Register.aspx.cs"
    Inherits="WebTransport.Register" %>

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
    <style>
        .classvalidationForOTP
        {
            color: red;
            display: inline;
            float: left;
            width: 100%;
        }
    </style>
    <script type="text/javascript">
        function ValidateRadios(oSrc, args) {
            var rpta = false; for (i = 1; i <= 5; i++) {
                if (document.getElementById("chkTermsCondi").checked == true) {
                    rpta = true;

                }

            }

            args.IsValid = rpta;

        }
    </script>
</head>
<body>
    <img src="img/home-bg.jpg" alt="Login Full Background" class="full-bg animation-pulseSlow">
    <div id="login-container" class="animation-fadeIn">
        <div class="login-title text-center">
            <!-- <h1>Transport Management System</h1> -->
        </div>
        <div class="block push-bit">
            <form runat="server" id="formlogin" class="form-horizontal form-bordered form-control-borderless">
            <asp:MultiView ID="multivwRegstr" runat="server">
                <asp:View ID="viewRegist" runat="server">
                    <div runat="server" id="forregister" class="form-horizontal form-bordered form-control-borderless display-block">
                        <div class="form-group">
                            <div class="col-xs-12">
                                <asp:Label ID="lblMainSuccessMsg" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                            </div>
                            <div class="col-xs-6">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="gi gi-user"></i></span>
                                    <asp:TextBox ID="txtOwnerName" runat="server" CssClass="form-control input-lg" placeholder="Firstname"
                                        MaxLength="50" TabIndex="1" Text=""></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtOwnerName"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter name!"
                                    SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-xs-6">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="gi gi-user"></i></span>
                                    <asp:TextBox ID="txtOwnerLast" runat="server" CssClass="form-control input-lg" placeholder="Lastname"
                                        MaxLength="50" TabIndex="2" Text=""></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="gi gi-envelope"></i></span>
                                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control input-lg"
                                        placeholder="Company Name" MaxLength="50" TabIndex="3" Text=""></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCompanyName"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter company name!"
                                    SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="gi gi-envelope"></i></span>
                                    <asp:TextBox ID="txtemailid" runat="server" CssClass="form-control input-lg" placeholder="Email"
                                        MaxLength="50" TabIndex="4" Text=""></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtemailid"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter email ID!"
                                    SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revemailid" runat="server" ControlToValidate="txtemailid"
                                    CssClass="classValidation" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Save"
                                    ErrorMessage="Not a valid email!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="gi gi-asterisk"></i></span>
                                    <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control input-lg"
                                        placeholder="Mobile Number" MaxLength="10" TabIndex="5"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMobileNumber"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter mobile number!"
                                    SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMobileNumber"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Not a valid Mobile No!"
                                    SetFocusOnError="true" ValidationExpression="^[7-9][0-9]{9}$" ValidationGroup="Save"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="gi gi-asterisk"></i></span>
                                    <asp:TextBox ID="txtLoginPassword" runat="server" CssClass="form-control input-lg" TextMode="Password"
                                        placeholder="Password" MaxLength="20" TabIndex="6"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLoginPassword"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter password!"
                                    SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="gi gi-asterisk"></i></span>
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control input-lg"  TextMode="Password"
                                        placeholder="Confirm Password" MaxLength="20" TabIndex="7"></asp:TextBox>
                                </div>
                                <asp:CompareValidator ID="cmpvalidtr" runat="server" CssClass="classValidation" Display="Dynamic"
                                    ErrorMessage="password does not match!" SetFocusOnError="true" ValidationGroup="Save"
                                    ControlToValidate="txtConfirmPassword" ControlToCompare="txtLoginPassword"></asp:CompareValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-6">
                                <a href="#" data-toggle="modal" class="register-terms">Terms</a>
                                <label class="switch switch-primary" id="lblchkbox" runat="server" data-toggle="tooltip"
                                    title="Agree to the terms">
                                    <asp:CheckBox ID="chkTermsCondi" TabIndex="8" runat="server" ValidationGroup="Save" />
                                    <span></span>
                                </label>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="Save"
                                    ErrorMessage="Please select terms condition" CssClass="classValidation" ClientValidationFunction="ValidateRadios"
                                    Display="Dynamic" SetFocusOnError="true" OnServerValidate="validateLoc"></asp:CustomValidator>
                            </div>
                            <div class="col-xs-6 text-right">
                                <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" ValidationGroup="Save"
                                    OnClick="lnkbtnSave_OnClick" TabIndex="9" CssClass="btn btn-sm btn-success"><i class="fa fa-plus"></i>Register Account</asp:LinkButton>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12 text-center">
                                <small>Do you have an account?</small> <a href="Login.aspx" id="linkregister" tabindex="10">
                                    <small>Login</small></a>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="viewMobileVerif" runat="server">
                    <div runat="server" id="Div1" class="form-horizontal form-bordered form-control-borderless display-block">
                        <div class="form-group">
                            <div class="col-xs-12">
                                <asp:Label ID="lblSuccessMsg" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                            </div>
                            <div class="col-xs-12">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="gi gi-user"></i></span>
                                    <asp:TextBox ID="txtMobileOtp" runat="server" CssClass="form-control" AutoComplete="Off"
                                        MaxLength="6" TabIndex="50"></asp:TextBox>
                                    <asp:LinkButton ID="lnkbtnOTPSend" runat="server" TabIndex="51" Visible="false" OnClick="lnkbtnSave_OnClick">Resend OTP</asp:LinkButton>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMobileOtp"
                                    ValidationGroup="OTP" ErrorMessage="Please Enter OTP code!" CssClass="classvalidationForOTP"
                                    SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-7">
                                <asp:Label ID="Label1" runat="server" ForeColor="Red">* Please do not refresh the page.</asp:Label>
                                <a href="Register.aspx">Cancel</a>
                            </div>
                            <div class="col-xs-5 text-right">
                                <div class="col-xs-6">
                                    <asp:LinkButton ID="lnkbtnContinoue" runat="server" CausesValidation="true" ValidationGroup="OTP"
                                        TabIndex="52" CssClass="btn btn-sm btn-success" OnClick="lnkbtnContinoue_OnClick"><i class="fa fa-plus"></i>Register Account</asp:LinkButton>
                                </div>
                                <%--<div class="col-xs-6">
                                    
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
            </form>
        </div>
    </div>
    <script src="js/vendor/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="js/vendor/bootstrap.min-3.3.js" type="text/javascript"></script>
    <script src="js/plugins-3.4.js" type="text/javascript"></script>
    <script src="js/app-3.3.js" type="text/javascript"></script>
    <script src="js/pages/login_1.4.js" type="text/javascript"></script>
</body>
</html>
