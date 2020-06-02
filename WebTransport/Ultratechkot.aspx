<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ultratechkot.aspx.cs" Inherits="WebTransport.Ultratechkot" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="printUltraTech" visible="false" runat="server">
     <table cellpadding="1" cellspacing="0" width="1100px" border="0" style="font-family: Arial,Helvetica,sans-serif;
            border-width: 1px; border-color: #000000;">
             <tr>
                            <td style="font-size:12px;" class="style3">
                                <asp:Label ID="lblJurCity" runat="server" Text=""></asp:Label></td>
                            <td class="style4">
                                &nbsp;</td>
                            <td style="font-size:12px; text-align:right;" class="style5">
                                 <asp:Label ID="lblOwnerPhoneNo" runat="server" Text=""></asp:Label></td>
                        </tr>
            <tr>
            <td colspan="3">
             <div style="text-align:left;Width:140px; float:left;">
                <asp:Image ID="imgultratech" Width="140px" Height="70px" runat="server" Visible="false"></asp:Image>
                </div>
                <div align="center" id="header" runat="server" class="white_bg"  style="font-size: 14px;
                    border-left-style: none; border-right-style: none">
                    &nbsp;&nbsp;&nbsp;&nbsp;
                        <br /> 
                            <u><asp:Label ID="lblCompName" runat="server" Text=""></asp:Label></u>
                        <br />
                    <asp:Label ID="lblCompAddress1" runat="server" Text=""></asp:Label>,
                    <asp:Label ID="lblCompAddress2" runat="server" Text=""></asp:Label>&nbsp;<br />
                    <asp:Label ID="lbltxtDis" runat="server" Text="FLEET OWNER TRANSPORT CONTRACTOR"></asp:Label>
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lbldel" runat="server" 
                        Text="TRANSPORTATION FREIGHT BILL (Basic) : CEMENT-AC" Font-Underline="True" 
                        style="font-weight: 700"></asp:Label><br />
                </div>
                </td>
            </tr>
            <tr>
            <td colspan="3" style="border:none"></td></tr>
            <tr>
            <td colspan="3" style="border:none"></td></tr>
            <tr>
                <td colspan="5">
                    <table width="100%">
                        <tr>
                            <td align="left" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                border-right-style: none">
                                    <asp:Label ID="Label3" runat="server" Text="Code : "></asp:Label>
                                    0<br /><b>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label1" runat="server" Font-Size="11px" Font-Underline="true" Text="Person Liable to Pay Service Tax:"></asp:Label></b>
                                    <br /><asp:Label ID="Label5" runat="server" Text="Party : "></asp:Label>
                                    <asp:Label ID="lblPartyName" runat="server" Text=""></asp:Label>
                                <br />
                                     <asp:Label ID="lblPartyAddress1" runat="server" Text=""></asp:Label>
                                <br />
                                     <asp:Label ID="lblPartyAddress2" runat="server" Text=""></asp:Label>
                                      <asp:Label ID="lblPartyPinCode" runat="server" Text=""></asp:Label><br />
                                    <asp:Label ID="lblPartyPanNo" runat="server" Text=""></asp:Label><br />

                                    
                            </td>
                            <td align="center" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                border-right-style: none">
                                <strong style="text-decoration: underline"></strong>
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                border-right-style: none">
                                <strong style="text-decoration: underline;"></strong>
                            </td>
                            <td align="right" class="white_bg" valign="top" style="padding-right:40px;font-size: 12px; border-left-style: none;border-right-style: none">
                                <table style="font-size: 12px;">
                                    <tr>
                                        <td align="left" valign="middle">
                                                <b><asp:Label ID="lblBillNo" runat="server" Text=""></asp:Label></b>
                                            <br />
                                            
                                            <b><asp:Label ID="lblBillDate" runat="server" Text=""></asp:Label></b>
                                            <br />
                                            <asp:Label ID="lblCompPanNo" runat="server" Text=""></asp:Label>
                                            <br />
                                            <asp:Label ID="lblServTaxNo" runat="server" Text=""></asp:Label>
                                                 <br />
                                           
                                                <asp:Label ID="Label11" runat="server" Text="Status"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:
                                            <asp:Label ID="Label12" runat="server" Text="PRIVATE LTD."></asp:Label><br />

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
                    <table border="1" cellpadding="0" cellspacing="0" style="font-size: 12px;  " width="100%" id="Table1">
                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                            <HeaderTemplate>
                                <tr style="font-size: 12px;  ">
                                    <td class="white_bg" style="font-size: 12px;  " width="3%">
                                        <strong>S.No.</strong>
                                    </td>
                                    <td style="font-size: 12px;  " width="5%">
                                        <strong>DI No</strong>
                                    </td>
                                    <td style="font-size: 12px;  " width="8%">
                                        <strong>EGP No</strong>
                                    </td>
                                    <td style="font-size: 12px;  " align="left" width="6%">
                                        <strong>Disp Date</strong>
                                    </td>
                                    <td style="font-size: 12px;  " width="8%">
                                        <strong>To</strong>
                                    </td>
                                    <td style="font-size: 12px;  " align="left" width="8%">
                                        <strong>LR No</strong>
                                    </td>
                                    <td style="font-size: 12px;  " width="8%" align="left">
                                        <strong>Vehicle No</strong>
                                    </td>
                                    <td style="font-size: 12px;  " width="8%" align="right">
                                        <strong>DspQty</strong>
                                    </td>
                                    <td style="font-size: 12px;  " width="7%" align="right">
                                        <strong>Rate</strong>
                                    </td>
                                    <td style="font-size: 12px;  " width="8%" align="right">
                                        <strong>Freight</strong>
                                    </td>
                                    <td style="font-size: 12px;  " width="6%" align="center">
                                        <strong>Remark</strong>
                                    </td>
                                              
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="font-size: 12px;  ">
                                    <td style="font-size: 12px;  " class="white_bg" width="3%" align="left">
                                        <%#Container.ItemIndex+1 %>.
                                    </td>
                                    <td style="font-size: 12px;  " class="white_bg" width="8%" align="left">
                                        <%#Eval("DI_NO")%>
                                    </td>
                                    <td style="font-size: 12px;  " class="white_bg" width="8%" align="left"> 
                                        <%#Eval("EGP_NO")%>
                                    </td>
                                    <td style="font-size: 12px;  " class="white_bg" width="6%" align="left">
                                         <%#Convert.ToDateTime(Eval("Chln_Date")).ToString("dd-MM-yyyy")%>
                                    </td>
                                    <td style="font-size: 12px;  " class="white_bg" width="8%">
                                        To <%#Eval("Consignee")%>
                                    </td>
                                    <td  style="font-size: 12px;  " class="white_bg" width="7%" align="left">
                                        <%#Eval("GR_No")%>
                                    </td>
                                    <td  style="font-size: 12px;  " class="white_bg" width="8%" align="left">
                                        <%#Eval("LORRY_NO")%>
                                    </td>
                                    <td style="font-size: 12px;  " class="white_bg" width="8%" align="right">
                                        <%#Convert.ToDouble(Eval("Dsp_Qty")).ToString("N2")%>
                                    </td>
                                    <td  style="font-size: 12px;  " class="white_bg" width="7%" align="right">
                                        <%#Convert.ToDouble(Eval("Rate")).ToString("N2")%>
                                    </td>
                                    <td  style="font-size: 12px;  " class="white_bg" width="8%" align="right">
                                        <%#Convert.ToDouble(Eval("Freight")).ToString("N2")%>
                                    </td>
                                    <td style="font-size: 12px;  " class="white_bg" width="6%" align="right">
                                        &nbsp;
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                             <tr style="font-size: 12px;  ">
                             <td class="white_bg" style="font-size: 12px;  " width="3%">
                            </td>
                            <td style="font-size: 12px;  " width="5%" align="center">
                            </td>
                            <td style="font-size: 12px;  " width="8%">
                            </td>
                            <td style="font-size: 12px;  " width="8%">
                            </td>
                            <td style="font-size: 12px;  " width="7%">
                            </td>
                            <td style="font-size: 12px;  " align="left" width="6%">
                                <strong>TOTAL</strong>
                            </td>
                            <td style="font-size: 12px;  " align="center" width="7%">
                            </td>
                            <td style="font-size: 12px;  " width="8%">
                                
                            </td>
                            <td style="font-size: 12px;  " width="7%" align="right">
                            <strong><asp:Label ID="lblTotalQty" runat="server"></asp:Label></strong>
                            </td>
                            <td style="font-size: 12px;  " width="7%" align="right">
                                <strong><asp:Label ID="lblTotalFreight" runat="server"></asp:Label></strong>
                            </td>
                            <td style="font-size: 12px;  " width="7%" align="right">
                                
                            </td>
                           
                             </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%" colspan="3">
                    <table width="100%" align="right" style="font-size: 12px;">
                        <tr>
                            <td colspan="9" align="left">
                                <strong><asp:Label ID="lblRupeesWord" runat="server" Text=""></asp:Label></strong>.
                            </td>
                        </tr>

                        <tr>
                            <td colspan="5" align="left" width="43%"  valign="top">
                                <table style="font-size: 12px;">
                                    <tr>
                                        <td width="80%" align="justify" colspan="2">
                                            I/We declare that we have not taken credit of Excise Duty paid on inputs or Capital Goods or Credit of Service Tax paid on input services used for providing 'Transportation of Goods by Road' services under the Provision of
                                            Cenvat Credit Rules 2004. I/We also declare that we have not availed the benefit under Notification No. 26/2012-ST dated 20-06-2012.
                                                
                                            <br />   
                                        </td>
                                    </tr> 
                                </table>
                            </td>
                            <td colspan="4" width="20%" align="right" valign="top">
                                <table width="100%">
                                  
                                    <tr>
                                        <td align="right" class="white_bg" style="font-size:13px" valign="bottom" colspan="3">
                                            <b>
                                                <asp:Label ID="lblSignCompName" runat="server" Text=""></asp:Label><br />
                                                <br /><br /><br />
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
      <asp:HiddenField ID="hideimgvalue" runat="server" />
    </form>
</body>
</html>
