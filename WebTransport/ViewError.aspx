<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewError.aspx.cs" Title="Error"
    Inherits="WebTransport.ViewError" %>

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
   <%-- <img src="img/home-bg.jpg" alt="Login Full Background" class="full-bg animation-pulseSlow">--%>
   <form id="frmErr" runat="server">
   <div class="about-us-page">
        <div class="dash-banner">
            <img src="../img/dashboard-banner.jpg" alt="dashboard banner">
        </div>
    <div class="block full block-alt-noborder" style="height:400px;">
    
            <div class="clearfix">
                <div class="col-md-10 col-md-offset-1 col-lg-8 col-lg-offset-2">
                    <article>
									<div class="clearfix">
									
										<div class="col-md-6">
											<p>
												<b>SORRY</b>, something went wrong due to the following reasons:
                                                <br />
                                                1. Session Timeout <br />
                                                2. Resource that does not exist <br />
                                                3. Bad Request
											</p>
										</div>
									</div>
									<div class="clearfix">
										<div class="widget">
											<div class="widget-advanced widget-advanced-alt">
												<div class="widget-header text-left">
												
													<h4 class="widget-content widget-content-image widget-content-light clearfix">				
														<strong>Please 
                                                       <asp:LinkButton ID="lnklogin" runat="server" 
                                                            onclick="lnklogin_Click">click here</asp:LinkButton>
                                                        to login again.</strong>
													</h4>
												</div>
										
											</div>
										</div>										
									</div>
						            <asp:Label ID="lblErr" runat="server" Text="" Font-Bold="true"></asp:Label>
								</article>
                </div>
            </div>
        </div>
        </div>
        </form>
    <script src="js/vendor/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="js/vendor/bootstrap.min-3.3.js" type="text/javascript"></script>
    <script src="js/plugins-3.4.js" type="text/javascript"></script>
    <script src="js/app-3.3.js" type="text/javascript"></script>
    <script src="js/pages/login_1.4.js" type="text/javascript"></script>
    <script>        $(function () { Login.init(); });</script>
</body>
<!-- END BODY -->
</html>
