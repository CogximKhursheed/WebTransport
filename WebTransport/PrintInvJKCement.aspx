<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintInvJKCement.aspx.cs"
    Inherits="WebTransport.PrintInvJKCement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="print" style="font-size: 13px; display: block;">
        <table cellpadding="1" cellspacing="0" width="1100px" border="1" style="font-family: Arial,Helvetica,sans-serif;">
            <tr id="Header" runat="server" visible="false">
                <td colspan="5">
              
                    <table width="100%">
                        <tr>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-left-style: none;
                                border-right-style: none">
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
                             <td align="left" style="float:left; padding-left:1px;">
                                 <asp:Image ID="imgjkcement" Width="140px" Height="90px" runat="server" Visible="false"></asp:Image>
                       
                        </td>
                            <td align="center" class="white_bg" valign="top" style="font-size: 14px; border-left-style: none;
                                border-right-style: none">
                                <strong>
                                    <asp:Label ID="lblCompanyname" runat="server" Style="font-size: 20px;"></asp:Label><br />
                                </strong>
                                <asp:Label ID="lblCompAdd1" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCompAdd2" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCompCity" runat="server"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblCompState" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblCompCityPin" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCompPhNo" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-left-style: none;
                                border-right-style: none">
                                <asp:Label ID="lblsapno" Text="SAP No.:" runat="server"></asp:Label>
                                <asp:Label ID="lblsapvalno" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="lblTxtPanNo" Text="PAN No.:" runat="server"></asp:Label>
                                <asp:Label ID="lblPanNo" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="lblTin" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;&nbsp;<asp:Label
                                    ID="lblCompTIN" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-left-style: none;
                                border-right-style: none">
                                <b>
                                    <asp:Label ID="lblinvoiceno" runat="server" Text="Bill No"></asp:Label></b>
                                &nbsp;&nbsp;&nbsp;:
                                <asp:Label ID="valuelblinvoicveno" runat="server" Text=""></asp:Label>
                                <br />
                                <b>
                                    <asp:Label ID="lblinvoicedate" runat="server" Text="Bill Date"></asp:Label>
                                </b>:
                                <asp:Label ID="valuelblinvoicedate" runat="server" Text="" Width="50px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
            <td colspan="5"></td>
            </tr>
            <tr>
                <td align="center" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                    border-right-style: none">
                    <asp:Label ID="Label2" Style="font-size: 14px;" runat="server" Text="(FREIGHT REIMBURSMENT BILL)"></asp:Label>
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
                                    <td class="white_bg" style="font-size: 12px" width="3%">
                                        <strong>S.No.</strong>
                                    </td>
                                    <td style="font-size: 12px" align="left" width="17%">
                                        <strong>Consignee Name</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>Destination</strong>
                                    </td>
                                    <td style="font-size: 12px" width="5%">
                                        <strong>Truck No</strong>
                                    </td>
                                    <td style="font-size: 12px" width="5%">
                                        <strong>GR No.</strong>
                                    </td>
                                    <td style="font-size: 12px" width="6%">
                                        <strong>Invoice No</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%" align="center">
                                        <strong>Date</strong>
                                    </td>
                                    <td style="font-size: 12px" align="left" width="6%">
                                        <strong>Bags Loaded</strong>
                                    </td>
                                    <td style="font-size: 12px" align="left" width="6%">
                                        <strong>Bags Un-Loaded</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>Qty(Mt.)</strong>
                                    </td>
                                    <td style="font-size: 12px" width="7%">
                                        <strong>Rate</strong>
                                    </td>
                                    <td style="font-size: 12px" width="6%">
                                        <strong>Freight</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>Shortage</strong>
                                    </td>
                                    <td style="font-size: 12px" width="10%">
                                        <strong>Freight Cir No.</strong>
                                    </td>
                                    <td style="font-size: 12px" width="10%">
                                        <strong>Total</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="white_bg" width="3%">
                                        <%#Container.ItemIndex+1 %>.
                                    </td>
                                    <td class="white_bg" width="17%" align="left">
                                        <%#Eval("Recivr_Name")%>&nbsp;
                                    </td>
                                    <td class="white_bg" width="8%" align="left">
                                        <%#Eval("Delvry_Place")%>&nbsp;
                                    </td>
                                    <td class="white_bg" width="5%">
                                        <%#Eval("LORRY_NO")%>
                                    </td>
                                    <td class="white_bg" width="5%">
                                        <%#Eval("Gr_No")%>
                                    </td>
                                    <td class="white_bg" width="6%">
                                        <%#Eval("Ref_No")%>
                                    </td>
                                    <td class="white_bg" width="5%">
                                        <%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MM-yyyy")%>
                                    </td>
                                    <td class="white_bg" width="6%" align="left">
                                        <%#Eval("loaded")%>&nbsp;
                                    </td>
                                    <td class="white_bg" width="6%" align="left">
                                        <%#Eval("unloaded")%>&nbsp;
                                    </td>
                                    <td class="white_bg" width="8%" align="left">
                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Weight")))%>
                                    </td>
                                    <td class="white_bg" width="7%" align="left">
                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Rate")))%>
                                    </td>
                                    <td class="white_bg" width="6%" align="left">
                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("amount")))%>
                                    </td>
                                    <td class="white_bg" width="8%" align="right">
                                        <%#Eval("Shortage")%>&nbsp;
                                    </td>
                                    <td class="white_bg" width="10%" align="left">
                                        <%#Eval("cirno")%>
                                    </td>
                                    <td class="white_bg" width="10%" align="right">
                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Net_Amnt")))%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td class="white_bg" width="3%">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="17%" align="left">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="8%" align="left">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="5%">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="5%">
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
                                    <td class="white_bg" width="8%" align="left">
                                        <asp:Label ID="lblqtymt" runat="server"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="7%" align="left">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="6%" align="left">
                                        <asp:Label ID="lblfreight" runat="server"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="8%" align="right">
                                        <asp:Label ID="lblshortage" runat="server"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="10%" align="left">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="10%" align="right">
                                        <asp:Label ID="lbltotlamntgrid" runat="server"></asp:Label>
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
                        <tr>
                            <td class="white_bg" width="15%">
                            </td>
                            <td class="white_bg" width="5%">
                            </td>
                            <td class="white_bg" width="15%">
                            </td>
                            
                            <td class="white_bg" width="10%" align="left">
                                <asp:Label ID="lbltxtCStax" Font-Bold="true" runat="server" Text="C.Service Tax"></asp:Label>
                            </td>
                            <td align="center" class="white_bg" width="10px">
                                <asp:Label ID="valuetxtCStax" runat="server"></asp:Label>
                            </td>
                             <td class="white_bg" width="4%" align="left">
                                &nbsp;
                            </td>
                            <td class="white_bg" width="8%" style="border: 1px" align="left">
                                <asp:Label ID="lbltxtctax" Font-Bold="true" runat="server" Text="T.Service Tax"></asp:Label>
                            </td>
                            <td class="white_bg" width="5%" align="right">
                                <asp:Label ID="valuelbltxtctax" runat="server"></asp:Label>
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="white_bg" align="left" width="15%">
                                E.& O.E.
                            </td>
                            <td class="white_bg" width="5%">
                            </td>
                            <td class="white_bg" width="15%">
                            </td>
                            
                            <td class="white_bg" width="10%" align="left">
                                <asp:Label ID="Label3" Font-Bold="true" runat="server" Text="C.Swatch Bharat Cess"></asp:Label>
                            </td>
                            <td align="center" class="white_bg" width="10px">
                                <asp:Label ID="valueCCSBTax" runat="server"></asp:Label>
                            </td>
                            
                            <td class="white_bg" width="4%" align="left">
                                &nbsp;
                            </td>
                            <td class="white_bg" width="8%" align="left">
                                <asp:Label ID="Label10" Font-Bold="true" runat="server" Text="T.Swatch Bharat Cess"></asp:Label>
                            </td>
                            <td class="white_bg" width="5%" align="right">
                                <asp:Label ID="valueCSBTax" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="white_bg" align="left" colspan="2" width="15%">
                                <asp:Label ID="lblenclosure" runat="server" Text="Enclosures" valign="right"></asp:Label>&nbsp;:&nbsp;<asp:Label
                                    ID="lblvalueencosers" runat="server" valign="right"></asp:Label>
                            </td>
                            <td class="white_bg" width="15%">
                            </td>
                            <td class="white_bg" width="10%" align="left">
                                <asp:Label ID="Label4" Font-Bold="true" runat="server" Text="C.Krishi Kalyan Cess"></asp:Label>
                            </td>
                            <td align="center" class="white_bg" width="10px">
                                <asp:Label ID="ValueCCKisanTax" runat="server"></asp:Label>
                            </td>

                            
                          <td class="white_bg" width="4%" align="left">
                            &nbsp;
                            </td>
                           <td class="white_bg" width="8%" align="left">
                                <asp:Label ID="Label1" Font-Bold="true" runat="server" Text="T.Krishi Kalyan Cess"></asp:Label>
                            </td>
                            <td class="white_bg" width="5%" align="right">
                                <asp:Label ID="ValueCKisanTax" runat="server"></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <td class="white_bg" align="left" width="15%">
                            </td>
                            <td class="white_bg" width="5%">
                            </td>
                            <td class="white_bg" width="15%">
                            </td>
                            <td class="white_bg" width="4%" align="left">
                            </td>
                            <td class="white_bg" width="10%" align="right">
                                &nbsp;
                            </td>
                            <td align="right" class="white_bg" width="5%">
                                &nbsp;
                            </td>
                            <td class="white_bg" width="8%" align="left">
                                <strong>
                                    <asp:Label ID="lblnet" runat="server" Text="Total:" Font-Size="13px" valign="left"></asp:Label></strong>
                            </td>
                            <td class="white_bg" width="5%" align="right">
                                <asp:Label ID="lblNetAmnt" runat="server" Font-Size="13px" valign="lef"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table width="100%" align="right">
                        <tr>
                            <td colspan="3" align="left" width="30%">
                                <table>
                                    <tr>
                                        <td width="80%">
                                            <asp:Label ID="lblremark" runat="server" valign="right" Text="we hereby certify that cenvat credit on input, capital goods and input services,used for providing the taxable service of transportration, has not been taken  under the cenvat credit rules,2004					"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="80%">
                                            <br />
                                            <b>Bill Checked By</b>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="16%" align="center" valign="top">
                                <b>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    Authorized Signature </b>
                            </td>
                            <td colspan="2" width="20%" align="right" valign="top">
                                <table width="100%">
                                    <tr>
                                        <td align="right" class="white_bg" style="font-size: small" valign="top" colspan="3">
                                            <b>
                                                <asp:Label ID="lblcompname" runat="server"></asp:Label><br />
                                                <br />
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
