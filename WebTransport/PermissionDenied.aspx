<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PermissionDenied.aspx.cs" Inherits="WebTransport.PermitionDenied" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="2" cellspacing="0" class="border" width="100%">
                <tr>
                    <td align="left" valign="top" class="header_bt_bg">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="29">
                                    &nbsp;
                                </td>
                                <td align="left" valign="bottom">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="left" valign="top" style="padding-top: 3px;">
                                                <!-- Breadcrumb -->
                                                <table border="0" cellspacing="0" cellpadding="0">
                                                </table>
                                                <!-- Breadcrumb -->
                                            </td>
                                            <td align="left" valign="top">
                                                &nbsp;
                                            </td>
                                            <td align="right" valign="top" style="padding-top: 1px;">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="27">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="white_bg " align="center">
                        <table id="tblNoAuthorize" runat="server" visible="false" class="border1">
                            <tr>
                                <td>
                                    You are not authorize for this
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" id="tblAuthorize"
                            runat="server">
                            <tr>
                                <td>
                                    <table width="750" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
                                        class="ibdr">
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td height="39" align="left" background="images/grd_top_bg.jpg" class="title06">
                                                            &nbsp;&nbsp;&nbsp;Permission Denied
                                                        </td>
                                                       <td height="39" align="right" background="images/grd_top_bg.jpg" class="title06">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td align="left" bgcolor="#F5FAFF" class="glow" colspan="2">
                                        </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="ibdr">
                                                                <tr>
                                                                    <td align="center" valign="top">
                                                                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#CCCCCC"
                                                                            class="ibdr1">
                                                                            <tr>
                                                                            <td align="left" bgcolor="#E8F2FD" class="btn_01" colspan="2">
                                                                            </td>
                                                                            </tr>
                                                                            <tr >
                                                                            <td align="left" bgcolor="#F5FAFF" class="glow" colspan="2" height="80">
                                                                                <span class="txt"><span class="red" style="color: #ff0000">&nbsp;&nbsp;</span> </span>
                                                                                You are not authorize for this form. Kindly contact to your department head.<span class="redfont1"></span>
                                                                            </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
