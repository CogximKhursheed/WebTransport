<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeaderControl.ascx.cs"
    Inherits="WebTransport.Controls.HeaderControl" %>
<header class="navbar navbar-default">
    <ul class="nav navbar-nav-custom">
        <li>
            <div id="TopMenuSearchBox" class="hanging-bottom virtual-placeholder no-arrow">
                <input type="text" class="autocomplete-search form-control ele-hanger input-cross" placeholder="Search anything..." name="account">
                <select name="trends[]" class="ele-hanging form-control trends" size="5" style="z-index: 999; min-width: 250px; width: 100%; background: white !important; display: none;"></select>
            </div>
        </li>
        <li class="list-divider-line"></li>
        <li style="text-transform: uppercase; line-height: 50px;    color: white;">
            <b>
                <asp:Label ID="lblCompName" runat="server"></asp:Label></b>
        </li>
    </ul>

    <ul class="nav navbar-nav-custom pull-right">
        <li>
            <a id="lnkSubmitTicket" class="call-us" href="javascript:__doPostBack('ctl00$lnkSubmitTicket','')"><span class="call-us-circle"><i class="fa fa-phone"></i></span>Support</a>
        </li>
        <li style="padding: 16px;">
            
        </li>
        <li class="dropdown">
            <a href="javascript:void(0)" class="dropdown-toggle" data-toggle="dropdown" style="font-weight: bold;color: #ffffff;">
                <asp:Label ID="lblusername" runat="server"></asp:Label>
                <asp:Image style="height: 30px;
    width: 30px;
    margin: 10px;
    border-radius: 20px;
    border: 1px solid silver;" ID="imgEmp" runat="server" src="https://taskexchange.cochrane.org/assets/default-profile-bfeeabd02c3b38305b18e4c2345fd54dbbd1a0a7bf403a31f08fca4fada50449.png" Width="50px" Height="50px" alt="avatar" />

                <%-- <img ID="imgEmp" runat="server" src="img/placeholders/avatars/avatar2.jpg" alt="avatar">--%>
            </a>
            <ul class="dropdown-menu dropdown-custom dropdown-menu-right">
                <span class="arrow top"></span>
                <li class="dropdown-header text-center">
                    <asp:Label ID="lblDatabaseName" runat="server"></asp:Label></li>
                <li>
                    <a href="~/About.aspx" id="hrfAbt" runat="server"><i class="fa fa-user fa-fw pull-right"></i>About Us</a>
                    <a href="~/LogOut.aspx" id="hrfLogin" runat="server"><i class="fa fa-ban fa-fw pull-right"></i>Logout</a>
                </li>
                <li style="    background: #f1f1f1;
    border-top: 1px solid #e0e0e0;
    padding: 8px 10px 8px 14px;
    break-after: avoid;
    color: #4b504c;
    font-family: monospace;
    font-weight: bold;
    font-size: 11px;
    white-space: nowrap;"><asp:Label ID="lblLastLoginDate" runat="server"></asp:Label></li>
            </ul>
        </li>
    </ul>
</header>
