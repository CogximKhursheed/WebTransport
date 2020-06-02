<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DayWiseBalReport.aspx.cs" Inherits="WebTransport.DayWiseBalReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link href="Styles/admin.css" rel="stylesheet" type="text/css" />
    <link href="Styles/style1.css" rel="stylesheet" type="text/css" />

    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="2" cellspacing="0" class="border" width="100%">
                <table cellpadding="1" cellspacing="1" border="0" class="border1" id="tblAuthorize" width="100%" runat="server">

                    <tr>
                        <td class="white_bg " align="center">
                            <table id="tblNoAuthorize" runat="server" visible="false" class="border1">
                                <tr>
                                    <td>You are not authorize for this
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" id="Table1"
                                runat="server">
                                <tr>
                                    <td>
                                        <table width="850" align="center" cellpadding="1" cellspacing="1" border="0">
                                            <tr>
                                                <td height="39" align="left" background="images/grd_top_bg.jpg" class="title06">
                                                    <div style="float: left; width: 70%; height: 30px;">
                                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="lblHdrNm" runat="server"
                                                            Text="Day Wise Balance Report "></asp:Label>
                                                        -
                                            <asp:Label ID="lblPartyName" runat="server" Font-Bold="True"></asp:Label>
                                                    </div>
                                                    <div id="prints" runat="server" align="right" style="text-align: right; width: 16%; float: right; height: 30px;"
                                                        visible="true">
                                                        <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ImageUrl="~/Images/Excel_Img.JPG"
                                                            OnClick="imgBtnExcel_Click" ToolTip="Export to excel" />
                                                        <img id="printRep" runat="server" src="Images/print.jpeg" alt="Print" onclick="javascript:CallPrint('divPrint')"
                                                            style="cursor: pointer;" title="Print" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="white_bg border" align="left" valign="top" colspan="4">
                                                    <table width="100%">
                                                        <tr>
                                                            <td class="white_bg" valign="top">
                                                                <div id="divPrint" style="overflow: auto;">
                                                                    <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false"
                                                                        Width="100%" BorderStyle="None" CssClass="ibdr gridBackground "
                                                                        GridLines="None" BorderWidth="0" RowStyle-CssClass="gridAlternateRow"
                                                                        AlternatingRowStyle-CssClass="gridRow" ShowFooter="true"
                                                                        AllowPaging="false" HeaderStyle-CssClass="gridRow"
                                                                        OnRowDataBound="grdMain_RowDataBound" OnRowCommand="grdMain_RowCommand">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40" HeaderText="Particular">
                                                                                <HeaderStyle HorizontalAlign="Left" Width="40" BackColor="#0066ff" ForeColor="White" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Left" Width="40" />
                                                                                <ItemTemplate>
                                                                                    <%-- <%#Eval("Perti")%>--%>
                                                                                    <asp:Label ID="lblParticular" runat="server"></asp:Label>
                                                                                    <b>
                                                                                        <asp:LinkButton ID="lnkBtnParticular" runat="server" CssClass="link" CommandName="cmdshowdetail"></asp:LinkButton></b>
                                                                                </ItemTemplate>
                                                                                <FooterStyle HorizontalAlign="left" Width="40" BackColor="#0066ff" ForeColor="White" Font-Bold="true" />
                                                                                <FooterTemplate>
                                                                                    Total
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Narration">
                                                                               <HeaderStyle HorizontalAlign="Left" Width="150" BackColor="#0066ff" ForeColor="White" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Left" Width="150" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNarr" runat="server" Text='<%#Eval("NARR_TEXT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                               <FooterStyle HorizontalAlign="Left" Width="150" BackColor="#0066ff" ForeColor="White" Font-Bold="true" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="left" HeaderStyle-Width="40" HeaderText="Debit">
                                                                                <HeaderStyle HorizontalAlign="left" Width="40" BackColor="#0066ff" ForeColor="White" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="left" Width="40" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbl2Debit" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle HorizontalAlign="left" Width="40" BackColor="#0066ff" ForeColor="White" Font-Bold="true" />
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblDebit" runat="server" ForeColor="Yellow"></asp:Label>
                                                                                    <b>
                                                                                        <asp:LinkButton ID="lnkBtn2Debit" runat="server" CssClass="link" CommandName="cmdshowdetail1D" ForeColor="Yellow"></asp:LinkButton></b>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="left" HeaderStyle-Width="40" HeaderText="Credit">
                                                                                <HeaderStyle HorizontalAlign="left" Width="40" BackColor="#0066ff" ForeColor="White" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="left" Width="40" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbl3Credit" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle HorizontalAlign="left" Width="40" BackColor="#0066ff" ForeColor="White" Font-Bold="true" />
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblCredit" runat="server" ForeColor="Yellow"></asp:Label>
                                                                                    <b>
                                                                                        <asp:LinkButton ID="lnkBtn2Credit" runat="server" CssClass="link" CommandName="cmdshowdetail1D" ForeColor="Yellow"></asp:LinkButton></b>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="right" HeaderStyle-Width="40" HeaderText="Balance">
                                                             <HeaderStyle HorizontalAlign="right" Width="40" BackColor="#0066ff" ForeColor="White" Font-Bold="true" />
                                                                <ItemStyle HorizontalAlign="right" Width="40"/>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl4Balance" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="right" Width="70" BackColor="#0066ff" ForeColor="White"   Font-Bold="true" />
                                                            </asp:TemplateField>--%>
                                                                        </Columns>
                                                                        <FooterStyle />
                                                                    </asp:GridView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style1">
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("VCHR_DATE")))?"":(Convert.ToDateTime((Eval("VCHR_DATE"))).ToString("dd-MMM-yyyy")))%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div style="float: left; display: none;">
                                                                    <table cellpadding="1" cellspacing="0" width="900" border="1">
                                                                        <tr>

                                                                            <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px; border-left-style: none; border-right-style: none">
                                                                                <strong>
                                                                                    <asp:Label ID="lblCompanyname" runat="server" Style="font-size: 18px;"></asp:Label><br />
                                                                                </strong>
                                                                                <asp:Label ID="lblCompAdd1" runat="server"></asp:Label>
                                                                                &nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="lblCompAdd2" runat="server"></asp:Label><br />
                                                                                <asp:Label ID="lblCompCity" runat="server"></asp:Label>&nbsp;&nbsp;
                                                        <asp:Label ID="lblCompState" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="lblCompCityPin" runat="server"></asp:Label><br />
                                                                                <asp:Label ID="lblCompPhNo" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                      
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
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
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgBtnExcel" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function CallPrint(strid) {

            var Unm = $('#<%= lblPartyName.ClientID %>').text();
             var Compname = $('#<%= lblCompanyname.ClientID %>').text();
             var Compadd1 = $('#<%= lblCompAdd1.ClientID %>').text();
             var Compadd2 = $('#<%= lblCompAdd2.ClientID %>').text();
             var cityname = $('#<%= lblCompCity.ClientID %>').text();
             var state = $('#<%= lblCompState.ClientID %>').text();
             var phoneno = $('#<%= lblCompPhNo.ClientID %>').text();
            var prtHederName = "<table width='100%' border='0'><tr><td align='center'><strong></strong></td></tr><tr><td align='center'></td></tr><tr><td align='center'></td></tr><tr><td align='center'><strong><font size='5'>" + Compname + "</font><strong></td></tr></br><tr><td align='center'><strong>" + Compadd1 + "<strong></td></tr><tr><td align='center'><strong>" + Compadd2 + "<strong></td></tr><tr><td align='center'><strong>" + cityname + "<strong> <strong>" + state + "<strong></td></tr><tr><td align='center'><strong>" + phoneno + "<strong></td></tr></table>";
            var prtContent1 = "<table width='100%' border='0'><tr><td align='center'><strong></strong></td></tr><tr><td align='center'></td></tr><tr><td align='center'></td></tr><tr><td align='center'><strong>" + Unm + "<strong></td></tr><tr><td><div style='border-width:1px;border-color:#000;border-style:solid;'></div></td></tr> </table>";
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=600,height=600,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtHederName);
            WinPrint.document.write(prtContent1);
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
    </script>

</asp:Content>
