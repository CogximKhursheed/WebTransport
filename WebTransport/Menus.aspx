<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Menus.aspx.cs" Inherits="WebTransport.Menus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="black12">
                <tr>
                    <td align="left" valign="top">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="29">
                                    &nbsp;
                                </td>
                                <td align="left" valign="bottom" class="heading_bg">
                                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                        <tr>
                                            <td class="orange13">
                                                Dashboard
                                            </td>
                                            <td align="right">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="5">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="27">
                                </td>
                            </tr>
                            <tr>
                                <td width="29">
                                    &nbsp;
                                </td>
                                <td align="left" valign="bottom">
                                <div style="text-align:center; ">
                                      <img width="690px" height="340px" src="Images/gate-1.jpg" alt="Group BLG" />
                                    </div>
                                      <!--birthday,anivarsary and joining start-->
                                          <div id="dvDOBDOA" runat="server" style="position: fixed; bottom: 0; right: 0px;
                                    width: 175px; z-index: 9999; min-height: 150px; height: auto; background-color: #D3D3D3;
                                    float: right;" class="border1 black12link">
                                    <div class="fl" style="background-color: #fff; width: 100%">
                                        <div style="width: 70%; text-align: left; background-color: #fff;" class="fl black11">
                                            <asp:Label ID="lblindiandatetime" runat="server"></asp:Label>
                                        </div>
                                        <div class="fr" style="background-color: #fff;">
                                            <img src="images/close.jpg" alt="Close" style="border: 0px; cursor: pointer; margin-left: 10px;"
                                                onclick="HideDOBDOA()" /></div>
                                    </div>
                                    <div id="divBirthday" runat="server">
                                        <div class="fl web_dialog_title" style="width: 100%">
                                            <asp:Label ID="lblbirthday" runat="server" ForeColor="White"></asp:Label>
                                        </div>
                                        &nbsp;
                                        <div class="fl" style="min-height: 50px; height: auto;">
                                            <asp:Repeater ID="rptdob" runat="server">
                                                <ItemTemplate>
                                                    <div style="width: 100px;" class="black11">
                                                        <%#Eval("user_name") %></div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                    &nbsp;
                                    <div id="divAnivarsary" runat="server">
                                        <div class="fl  web_dialog_title" style="width: 100%">
                                            <asp:Label ID="lblaniversary" runat="server" ForeColor="White"></asp:Label></div>
                                        &nbsp;
                                        <div class="fl" style="min-height: 50px; height: auto;">
                                            <asp:Repeater ID="rptdoa" runat="server">
                                                <ItemTemplate>
                                                    <div style="width: 100px;" class="black11">
                                                        <%#Eval("user_name") %></div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                    &nbsp;
                                    <div id="divCompletionYearmyd" runat="server">
                                        <div class="fl  web_dialog_title" style="width: 100%">
                                            <asp:Label ID="lblCompletionYearmyd" runat="server" ForeColor="White"></asp:Label></div>
                                        &nbsp;
                                        <div class="fl" style="min-height: 50px; height: auto;">
                                            <asp:Repeater ID="rptcymyd" runat="server">
                                                <ItemTemplate>
                                                    <div class="black11">
                                                        <%#Eval("user_name") %>&nbsp;<%#Eval("Message") %></div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                                      <!--birthday,anivarsary and joining end-->
                                    
                                </td>
                                <td width="27">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
     <script language="javascript" type="text/javascript">
         function hidemsg() {
             $("#dvmsg").fadeOut("slow");
         }
         function HideDOBDOA() {
             $("#<%=dvDOBDOA.ClientID %>").hide(2000);
         }
    </script>
</asp:Content>
