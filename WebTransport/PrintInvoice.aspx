<%@ Page Title="Invoice" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="PrintInvoice.aspx.cs" Inherits="WebTransport.PrintInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" align="center">
        <tr>
            <td class="white_bg " align="center">
                <table id="tblNoAuthorize" runat="server" visible="false" class="border1">
                    <tr>
                        <td>
                            You are not authorize for this
                        </td>
                    </tr>
                </table>
                <table width="100%" border="1" align="center" cellpadding="0" cellspacing="0" id="tblAuthorize"
                    runat="server" class="ibdr">
                    <tr>
                        <td>
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
                                <tr>
                                    <td height="39" align="left" background="images/grd_top_bg.jpg" class="title06">
                                        &nbsp;&nbsp;&nbsp;Invoice Generation
                                    </td>
                                    <td height="39" align="right" background="images/grd_top_bg.jpg" class="title06">
                                        <a href="ManageInvoice.aspx" tabindex="27" style="color: #FFFF00; font-family: 25px;
                                            line-height: 30px; font-weight: bold;">
                                            <asp:Label ID="lblViewList" runat="server" Text="View List&nbsp;&nbsp;"></asp:Label></a>
                                        <asp:ImageButton ID="imgPrint" runat="server" AlternateText="Print" ImageUrl="~/images/print.jpeg"
                                            Visible="false" title="Print" OnClientClick="return CallPrint('print');" Height="16px" />
                                        <asp:ImageButton ID="ImgPrint1" runat="server" AlternateText="Print General" ImageUrl="~/images/print.jpeg"
                                            Visible="false" title="Print General" OnClientClick="return CallPrint('print1');"
                                            Height="16px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td align="left" bgcolor="#E8F2FD" class="btn_01" height="40px" width="7%">
                                                </td>
                                                <td align="center" bgcolor="#E8F2FD" class="btn_01" height="40px" width="5%">
                                                    Invoice No<span class="redfont">*</span>
                                                </td>
                                                <td align="left" bgcolor="#E8F2FD" class="btn_01" height="40px" width="15%">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtInvPreIx" runat="server" CssClass="glow" Height="24px" MaxLength="8"
                                                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                                    TabIndex="2" Text="" Width="41px" AutoPostBack="true"></asp:TextBox>
                                                                &nbsp;
                                                                <asp:TextBox ID="txtinvoicNo" runat="server" CssClass="glow" Height="24px" MaxLength="7"
                                                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                                    TabIndex="3" Text="" Width="68px"></asp:TextBox>
                                                            </td>
                                                            <td align="left" bgcolor="#E8F2FD" class="btn_01" height="40px" valign="middle">
                                                                <asp:TextBox ID="txtDate" runat="server" CssClass="glow" Height="24px" MaxLength="50"
                                                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                                    Style="width: 75px;" TabIndex="6" Text=""></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="left" bgcolor="#E8F2FD" class="btn_01" height="40px" width="5%">
                                                    <span class="txt"><span class="red" style="color: #ff0000">&nbsp;&nbsp;</span> </span>
                                                    Sender Name<span class="redfont1">*</span>
                                                </td>
                                                <td align="left" bgcolor="#E8F2FD" height="40px" valign="middle" width="1%">
                                                    <asp:DropDownList ID="ddlSenderName" runat="server" AutoPostBack="true" CssClass="glow"
                                                        Height="30px" TabIndex="5" Width="200px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td colspan="7" bgcolor="#F5FAFF" class="btn_01">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="7" bgcolor="#F5FAFF" class="btn_01">
                                                    <div id="print" runat="server" style="font-size: 13px;">
                                                        <div id="header" style="background-color: White;">
                                                        </div>
                                                        <table cellpadding="1" cellspacing="0" width="1100px" border="0" style="font-family: Arial,Helvetica,sans-serif;
                                                            border-width: 1px; border-color: #000000;">
                                                            <tr>
                                                                <td width="44%" align="left">
                                                                    <asp:Label ID="lblTin" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;&nbsp;<asp:Label
                                                                        ID="lblCompTIN" runat="server"></asp:Label><br />
                                                                    <asp:Label ID="lblTxtPanNo" Text="PAN NO : " runat="server" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:Label ID="lblPanNo" runat="server" Font-Bold="True"></asp:Label><br />
                                                                    <asp:Label ID="lbltxtCat" Text="Category : " runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:Label ID="lblCategory" runat="server" Text="Goods Transport Agency"></asp:Label>
                                                                </td>
                                                                <td class="style3" align="center">
                                                                    <asp:Label ID="lblCompPhNo" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                           <tr>
                                                             <td colspan="3">
                                                             <div style="text-align:left;padding-top:25px;width :140px; float:left;">
                                                                    <asp:Image ID="imglogo" runat="server"  Width="140px" Height="90px"  Visible="false"/>
                                                                    </div>
                                                                <div align="center" class="white_bg"  style="font-size: 14px; text-align:center; 
                                                                    border-left-style: none; border-right-style: none">
                                                                     <asp:Label ID="lblInv" runat="server" Text="INVOICE" Font-Overline="False" Font-Size="Medium"
                                                                        Font-Underline="True"></asp:Label><br />
                                                                    <strong style="padding-top:10px;">
                                                                        <br /> 
                                                                            <asp:Label ID="lblCompanyname" runat="server" Style="font-size: 21px;" Font-Underline="True"></asp:Label>
                                                                        <br />
                                                                    </strong>
                                                                    <asp:Label ID="lblCompAdd1" runat="server"></asp:Label><br />
                                                                    <asp:Label ID="lblCompAdd2" runat="server"></asp:Label>
                                                                    <asp:Label ID="lblCompCity" runat="server"></asp:Label>
                                                                    &nbsp;-&nbsp;
                                                                    <asp:Label ID="lblCompCityPin" runat="server"></asp:Label>
                                                                    &nbsp;&nbsp;
                                                                    <asp:Label ID="lblFaxNo" Text="FAX No:" runat="server"></asp:Label>
                                                                    <asp:Label ID="lblCompFaxNo" runat="server"></asp:Label><br />
                                                                    <asp:Label ID="lbltxtDis" runat="server" Text="Authorized Transporter - "></asp:Label>
                                                                    <asp:Label ID="lblDist" runat="server" Text=""></asp:Label><br />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:Label ID="lbldel" runat="server" Text="DOOR DELIVERY" Font-Bold="True" Font-Underline="True"></asp:Label>
                                                                </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="left" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                                                                border-right-style: none">
                                                                                <strong style="text-decoration: underline">
                                                                                    <asp:Label ID="Label5" runat="server" Text="To  "></asp:Label></strong>
                                                                                <br />
                                                                                <b>
                                                                                    <asp:Label ID="lblSenderName" runat="server"></asp:Label></b>
                                                                                <br />
                                                                                <b>
                                                                                    <asp:Label ID="lblsenderaddress" runat="server"></asp:Label></b>
                                                                                <br />
                                                                                <b>
                                                                                    <asp:Label ID="lblsendercity" runat="server"></asp:Label>&nbsp;&nbsp;
                                                                                    <asp:Label ID="lblsenderstate" runat="server"></asp:Label>
                                                                                </b>
                                                                            </td>
                                                                            <td align="center" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                                                                border-right-style: none">
                                                                                <strong style="text-decoration: underline"></strong>
                                                                            </td>
                                                                            <td align="left" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                                                                border-right-style: none">
                                                                                <strong style="text-decoration: underline;"></strong>
                                                                            </td>
                                                                            <td align="center" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                                                                border-right-style: none">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td align="center" valign="middle">
                                                                                            <b>
                                                                                                <asp:Label ID="lblinvoiceno" runat="server" Text="Invoice No"></asp:Label></b>
                                                                                            &nbsp;&nbsp;&nbsp;:
                                                                                            <asp:Label ID="valuelblinvoicveno" runat="server" Text=""></asp:Label>
                                                                                            <br />
                                                                                            <b>&nbsp;
                                                                                                <asp:Label ID="lblinvoicedate" runat="server" Text="Invoice Date"></asp:Label>
                                                                                            </b>:
                                                                                            <asp:Label ID="valuelblinvoicedate" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table border="1" cellspacing="0" style="font-size: 12px" width="100%" id="Table1">
                                                                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                                                            <HeaderTemplate>
                                                                                <tr>
                                                                                    <td class="white_bg" style="font-size: 12px" width="3%">
                                                                                        <strong>S.No.</strong>
                                                                                    </td>
                                                                                    <td style="font-size: 12px" width="6%" align="center">
                                                                                        <strong>SHIPMENT NO</strong>
                                                                                    </td>
                                                                                    <td style="font-size: 12px" width="8%">
                                                                                        <strong>GR NO.</strong>
                                                                                    </td>
                                                                                    <td style="font-size: 12px" width="5%">
                                                                                        <strong>DELIVERY NO</strong>
                                                                                    </td>
                                                                                    <td style="font-size: 12px" width="8%">
                                                                                        <strong>DATE</strong>
                                                                                    </td>
                                                                                    <td style="font-size: 12px" align="left" width="6%">
                                                                                        <strong>LORRY NO</strong>
                                                                                    </td>
                                                                                    <td style="font-size: 12px" align="left" width="8%">
                                                                                        <strong>DESPATCH QTY</strong>
                                                                                    </td>
                                                                                    <td style="font-size: 12px" width="8%" align="left">
                                                                                        <strong>DELIVERED QTY</strong>
                                                                                    </td>
                                                                                    <td style="font-size: 12px" width="8%">
                                                                                        <strong>RATE QTY</strong>
                                                                                    </td>
                                                                                    <td style="font-size: 12px" width="7%">
                                                                                        <strong>AMOUNT</strong>
                                                                                    </td>
                                                                                    <td style="font-size: 12px" width="8%">
                                                                                        <strong>DESTINATION</strong>
                                                                                    </td>
                                                                                    <td style="font-size: 12px" width="6%">
                                                                                        <strong>REMARKS</strong>
                                                                                    </td>
                                                                                    <%-- <td style="font-size: 12px" width="8%">
                                                            <strong>Net Amount</strong>
                                                        </td>
                                                        <td style="font-size: 12px" width="5%">
                                                            <strong>Shortage</strong>
                                                        </td>
                                                        <td style="font-size: 12px" width="5%">
                                                            <strong>Serv. Tax</strong>
                                                        </td>--%>
                                                                                </tr>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td class="white_bg" width="3%">
                                                                                        <%#Container.ItemIndex+1 %>.
                                                                                    </td>
                                                                                    <td class="white_bg" width="5%">
                                                                                        <%#Eval("Shipment_No")%>
                                                                                    </td>
                                                                                    <td class="white_bg" width="8%">
                                                                                        <%#Eval("Gr_No")%>
                                                                                    </td>
                                                                                    <td class="white_bg" width="8%">
                                                                                        <%#Eval("DI_NO")%>
                                                                                    </td>
                                                                                    <td class="white_bg" width="8%">
                                                                                        <%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MM-yyyy")%>
                                                                                    </td>
                                                                                    <td class="white_bg" width="6%" align="left">
                                                                                        <%#Eval("LORRY_NO")%>&nbsp;
                                                                                    </td>
                                                                                    <td class="white_bg" width="7%" align="center">
                                                                                        <%#(Eval("Weight"))%>
                                                                                    </td>
                                                                                    <td class="white_bg" width="8%" align="center">
                                                                                        <%#(Eval("Del_Qty"))%>
                                                                                    </td>
                                                                                    <td class="white_bg" width="8%" align="center">
                                                                                        <%#Eval("Rate")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                    </td>
                                                                                    <td class="white_bg" width="7%" align="left">
                                                                                        <%#Eval("Net_Amnt")%>
                                                                                    </td>
                                                                                    <td class="white_bg" width="8%" align="left">
                                                                                        <%#Eval("Delvry_Place")%>
                                                                                    </td>
                                                                                    <td class="white_bg" width="6%" align="left">
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                            </FooterTemplate>
                                                                        </asp:Repeater>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" align="right">
                                                                    <table border="1" cellspacing="0" style="font-size: 12px" width="100%" id="Table2">
                                                                        <tr>
                                                                            <td class="white_bg" style="font-size: 12px" width="3%">
                                                                            </td>
                                                                            <td style="font-size: 12px" width="5%" align="center">
                                                                            </td>
                                                                            <td style="font-size: 12px" width="8%">
                                                                            </td>
                                                                            <td style="font-size: 12px" width="8%">
                                                                            </td>
                                                                            <td style="font-size: 12px" width="7%">
                                                                            </td>
                                                                            <td style="font-size: 12px" align="center" width="6%">
                                                                                <strong>TOTAL</strong>
                                                                            </td>
                                                                            <td style="font-size: 12px" align="center" width="7%">
                                                                                <asp:Label ID="lbltotalWeight" Font-Bold="true" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td style="font-size: 12px" width="8%" align="center">
                                                                                <asp:Label ID="lblWeight" Font-Bold="true" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td style="font-size: 12px" width="7%">
                                                                            </td>
                                                                            <td style="font-size: 12px" width="7%" align="center">
                                                                                <asp:Label ID="lblTotAmnt" Font-Bold="true" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td style="font-size: 12px" width="7%">
                                                                            </td>
                                                                            <td style="font-size: 12px" width="6%">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%" colspan="3">
                                                                    <table width="100%" align="right" border="1">
                                                                        <tr>
                                                                            <td colspan="5" align="left">
                                                                                RS.&nbsp;&nbsp;<asp:Label ID="lblTowords" runat="server" Font-Bold="True"></asp:Label>&nbsp;Only.
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" colspan="3">
                                                                                <asp:Label ID="Label1" runat="server" Text="Add: Service Tax @" Font-Size="Small"></asp:Label>
                                                                                <asp:Label ID="lblSerTax" runat="server" Font-Size="Small"></asp:Label>
                                                                            </td>
                                                                            <td width="5%">
                                                                            </td>
                                                                            <td align="center" width="5%">
                                                                                <asp:Label ID="lblServ" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" colspan="3">
                                                                                <asp:Label ID="Label2" runat="server" Text="Add: Swatch Bharat Cess [SBC] @" Font-Size="Small"></asp:Label>
                                                                                <asp:Label ID="lblSTax" runat="server" Text="" Font-Size="Small"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:Label ID="lblSwach" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" colspan="3">
                                                                                Service Tax to be paid by
                                                                                <asp:Label ID="lblSender" runat="server" Text=""></asp:Label><asp:Label ID="lblsendcity"
                                                                                    runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:Label ID="lbltxtTot" runat="server" Text="TOTAL"></asp:Label>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:Label ID="lblTot" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="lblnet" runat="server" Text="GR TOTAL" Font-Size="13px" valign="right"
                                                                                    Font-Bold="True"></asp:Label>
                                                                                :&nbsp;&nbsp;&nbsp;<asp:Label ID="lblNetAmnt" runat="server" Font-Size="13px" valign="lef"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="5" align="left" width="43%">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td width="80%" align="left" colspan="2">
                                                                                            <asp:Label ID="lbldec" runat="server" Text="DECLARATION" Font-Bold="True"></asp:Label><br />
                                                                                            <asp:Label ID="lblremark" runat="server" valign="right" Text="In terms the notification No.26/2012 Associated Amended dated 20/06/2012, Service Tax is calculated on a value which is equivalent to 30% of the gross amount charged from the customer for providing the taxable services, and no credit of duty on inputs or capital goods for providing such taxable services has been taken by us under the provisions of CENVAT Credit Rules, 2004"
                                                                                                Font-Size="Small"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="80%">
                                                                                            <%-- <asp:Label ID="lblenclosure" runat="server" Text="Enclosures" valign="right"></asp:Label>&nbsp;:&nbsp;<asp:Label
                                                                    ID="lblvalueencosers" runat="server" valign="right"></asp:Label>--%>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <%--<td width="16%" align="center" valign="top" colspan="2">
                                                    
                                                </td>--%>
                                                                            <td colspan="4" width="20%" align="right" valign="top">
                                                                                <table width="100%">
                                                                                    <tr style="border-bottom-style: solid; border-top-width: thin; border-bottom-width: thin;
                                                                                        border-right-width: thin">
                                                                                        <td align="left" width="25%">
                                                                                        </td>
                                                                                        <td align="left">
                                                                                        </td>
                                                                                        <td align="right" width="8%">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="center" class="white_bg" style="font-size: small" valign="top" colspan="3">
                                                                                            <b>
                                                                                                <asp:Label ID="lblcompname" runat="server" Font-Bold="True"></asp:Label><br />
                                                                                                <br />
                                                                                                <br />
                                                                                                (Authorised Signatory)&nbsp;</b>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" bgcolor="#E8F2FD" class="btn_01" height="40px">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" bgcolor="#E8F2FD" class="btn_01">
                                                            </td>
                                                            <td align="left" bgcolor="#E8F2FD" class="btn_01" height="40px" valign="middle">
                                                                &nbsp;
                                                            </td>
                                                            <td align="left" bgcolor="#E8F2FD" class="btn_01">
                                                            </td>
                                                            <td align="left" bgcolor="#E8F2FD" class="btn_01" height="40px" valign="middle">
                                                                &nbsp;
                                                            </td>
                                                            <td align="left" bgcolor="#E8F2FD" class="btn_01">
                                                            </td>
                                                            <td align="left" bgcolor="#E8F2FD" class="btn_01" height="40px" valign="middle">
                                                                &nbsp;
                                                            </td>
                                                            <td align="left" bgcolor="#E8F2FD" class="btn_01">
                                                            </td>
                                                            <td align="left" bgcolor="#E8F2FD" class="btn_01" height="40px" valign="middle">
                                                                &nbsp;
                                                            </td>
                                                            <td align="left" bgcolor="#E8F2FD" class="btn_01">
                                                            </td>
                                                            <td align="right" bgcolor="#E8F2FD" class="btn_01" height="40px" valign="middle">
                                                                &nbsp;
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
    <asp:HiddenField ID="hideimgvalue" runat="server" />
    <script type="text/javascript">
        window.print();
        window.onfocus = function () { window.close(); }
        function printDiv(print) {
            document.getElementById('header').style.display = 'none';
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <style type="text/css">
        .style1
        {
            width: 35%;
            background: #fff;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #2F2F2F;
        }
        .style2
        {
            width: 35%;
            background: #fff;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #2F2F2F;
        }
        .style3
        {
            width: 30%;
            background: #fff;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #2F2F2F;
        }
    </style>
</asp:Content>
