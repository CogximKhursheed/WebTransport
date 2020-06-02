<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintDurgaLineIndia.aspx.cs" Inherits="WebTransport.PrintDurgaLineIndia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 49px;
        }
        .style2
        {
            width: 243px;
        }
        .style3
        {
            width: 27%;
        }
        .style4
        {
            width: 422px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="print" style="font-size: 13px; display: block;">
        <table cellpadding="1" cellspacing="0" width="1100px" border="1" style="font-family: Arial,Helvetica,sans-serif;">
            <tr>
                <td colspan="5">
                    <table width="100%">
                        <tr id="Header" runat="server">
                            <td align="left" class="style2" valign="top" style="font-size: 13px; border-left-style: none;
                                border-right-style: none">
                                <asp:Label ID="lblTxtPanNo" Text="PAN No " runat="server"></asp:Label>
                                <asp:Label ID="lblPanNo" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="lblsapno" Text="ST No " runat="server"></asp:Label>
                                <asp:Label ID="lblsapvalno" runat="server"></asp:Label>
                                <br />
                                <br />
                                
                                    <br />
                                    <br />
                                           
                            </td>
                             <td align="left" style="padding-left:1px;" class="style1">
                              </td>
                            <td align="center" class="white_bg" valign="top" style="font-size: 14px; border-left-style: none;
                                border-right-style: none">
                                <strong>
                                    <asp:Label ID="lblCompanyname" runat="server" Style="font-size: 20px;"></asp:Label><br />
                                </strong>
                                <asp:Label ID="lblCompAdd1" runat="server"></asp:Label>
                                <asp:Label ID="lblCompAdd2" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCompCity" runat="server"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblCompState" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblCompCityPin" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCompPhNo" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-left-style: none;
                                border-right-style: none">
                                <br />
                                <br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-left-style: none;
                                border-right-style: none">
                                
                            </td>
                        </tr>
                        <tr>
                        <td align="left" class="style2" valign="top" style="font-size: 13px; border-left-style: none; border-right-style: none">
                                <asp:Label ID="PartyName" runat="server" Text="Sender Name"></asp:Label>&nbsp;<asp:Label ID="lblSenderName" runat="server"  Text=""></asp:Label>
                                    <br />
                                    <asp:Label ID="lblAddress" runat="server" Text="Address" ></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="lblPartcity" runat="server"  Text=""></asp:Label>
                            
                                </td>
                                <td align="left" style="padding-left:1px;" class="style1">
                              </td>
                              <td align="center" class="white_bg" valign="top" style="font-size: 14px; border-left-style: none;
                                border-right-style: none"></td>
                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-left-style: none;
                                border-right-style: none">
                                <b>
                                <asp:Label ID="lblinvoiceno" runat="server" Text="Bill No."></asp:Label>&nbsp;&nbsp; &nbsp;<asp:Label ID="valuelblinvoicveno" runat="server"  Text=""></asp:Label>
                                &nbsp;&nbsp;
                                </b>
                                <br />
                                <b><asp:Label ID="lblinvoicedate" runat="server" Text="Bill Date"></asp:Label>
                                </b>:
                                <asp:Label ID="valuelblinvoicedate" runat="server" Text="" Width="50px"></asp:Label>
                                </td>
                         </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="Left" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none; border-right-style: none">
                    <asp:Label ID="Label2" Style="font-size: 14px;" runat="server" Text="Sub:FREIGHT BILL"></asp:Label>
                </td>
            </tr>
            <tr id="trgrtype" runat="server" visible="true">
                <td align="center" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                    border-right-style: none">
                    <strong>
                        <asp:Label ID="lblgrtype" Style="font-size: 14px;" runat="server" Text=""></asp:Label></strong>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table border="1" cellspacing="0" style="font-size: 12px" width="100%" id="Table1">
                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                            <HeaderTemplate>
                                <tr>
                                    <td class="white_bg" style="font-size: 12px" width="8%">
                                        <strong>Date</strong>
                                    </td>
                                    <td style="font-size: 12px" align="left" width="6%">
                                        <strong>Truck No</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>Gr No</strong>
                                    </td>
                                    <td style="font-size: 12px" width="10%">
                                        <strong>From City</strong>
                                    </td>
                                    <td style="font-size: 12px" width="10%">
                                        <strong>To City</strong>
                                    </td>
                                    <td style="font-size: 12px" width="6%">
                                        <strong>Invoice No</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%" align="center">
                                        <strong>PKGS</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>SIZE</strong>
                                    </td>
                                    <td style="font-size: 12px" width="7%">
                                        <strong>UNLOAD Date</strong>
                                    </td>
                                    <td style="font-size: 12px" width="6%" align="right">
                                        <strong>TOTAL AMOUNT</strong>
                                    </td>
                                   </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="white_bg" width="3%">
                                        <%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MM-yyyy")%>
                                    </td>
                                    <td class="white_bg" width="10%" align="left">
                                        <%#Eval("Truck_No")%>
                                    </td>
                                    <td class="white_bg" width="8%" align="left">
                                        <%#Eval("Gr_No")%>&nbsp;
                                    </td>
                                    <td class="white_bg" width="10%">
                                        <%#Eval("From_City")%>
                                    </td>
                                    <td class="white_bg" width="10%">
                                         <%#Eval("To_City")%>
                                    </td>
                                    <td class="white_bg" width="6%">
                                       <%#Eval("Ref_No")%>&nbsp;
                                    </td>
                                    <td class="white_bg" width="5%">
                                        <%#Eval("loaded")%>&nbsp;
                                    </td>
                                    <td class="white_bg" width="6%" align="left">
                                       <%#Eval("Size")%>&nbsp; 
                                    </td>
                                    <td class="white_bg" width="6%" align="left">
                                       <%#Convert.ToDateTime(Eval("Unload_Date")).ToString("dd-MM-yyyy")%>
                                    </td>
                                    <td class="white_bg" width="8%" align="right">
                                     <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Net_Amnt")))%>
                                    </td>
                                    
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td class="white_bg" width="3%">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="10%" align="left">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="8%" align="left">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="10%">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="10%">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="6%">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="5%">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="6%" align="left">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="6%" align="left">
                                        <b>Total :</b>&nbsp;
                                    </td>
                                    <td class="white_bg" width="8%" align="right">
                                        <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                    </td>
                                  </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="4">
                    <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table3">
                       <td class="white_bg" align="left" width="15%">
                                E.& O.E.
                       </td>
                   </table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table width="100%" align="right">
                        <tr>
                            <td colspan="2" align="right" valign="top" class="style3">
                                <table width="100%">
                                    <tr>
                                       <td align="Left" class="white_bg" style="font-size: small" valign="top" colspan="3">
                                           
                                    <asp:Label ID="Label1" runat="server"  Text="Amount [In Words]:" ></asp:Label>       <asp:Label ID="lbltotAmount" runat="server"  ></asp:Label>
                                       </td> 
                                    </tr>
                                    
                                </table>
                            </td>
                            
                            <td colspan="2" width="20%" align="right" valign="top">
                                <table width="100%">
                                    <tr>
                                        <td align="right" class="style4" style="font-size: small" valign="top" 
                                            colspan="3">
                                            <b>
                                                <asp:Label ID="lblcompname" runat="server"></asp:Label><br />
                                                <br />
                                                <br />
                                                <br />
                                                Authorised Signatory&nbsp;</b>
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
      <asp:HiddenField ID="hideimgvalue" runat="server" />
    </form>
</body>
</html>
