<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintOmInvoice.aspx.cs"
    Inherits="WebTransport.PrintOmInvoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #Table1 tr:first-child td
        {
            font-family: sans-serif;
            font-size: 11px !important;
            font-weight: 300;
            text-align: center;
        }
        *S.Tax.No.
        {
            font-family: sans-serif;
        }
        
        tr#bill_detail td
        {
            font-size: 11px !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:HiddenField ID="hidIsGST" runat="server" />
    <div id="print" runat="server" style="font-size: 13px; display: block;">
        <table width="100%">
            <tr style="display: block">
                <td class="white_bg" align="center">
                    <div id="Div1" style="font-size: 13px;">
                        <table cellpadding="1" cellspacing="0" width="100%" border="1" style="border-collapse: collapse;">
                            <tr>
                                <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 13px;
                                    border-left-style: none; border-right-style: none">
                                    <asp:Image ID="imgLogoShow" Style="width: 230px;" runat="server" ImageUrl="~/img/OMLOGO.png">
                                    </asp:Image><br />
                                    <asp:Label ID="lblCompDesc" runat="server" Style="font-size: 13px;"></asp:Label>
                                    <asp:Image ID="Image1" Height="5px" Style="width: 230px;" runat="server" ImageUrl="~/img/line.jpg">
                                    </asp:Image>
                                    <br />
                                    <asp:Label ID="lblCompAdd1" runat="server" Style="font-size: 12px;"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblCompAdd2" runat="server" Style="font-size: 12px;"></asp:Label>
                                    <asp:Label ID="lblCompCity" runat="server" Style="font-size: 12px;"></asp:Label>&nbsp;&nbsp;
                                    <asp:Label ID="lblCompState" runat="server" Style="font-size: 12px;"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblCompCityPin" runat="server" Style="font-size: 12px;"></asp:Label><br />
                                    <asp:Label ID="lblCompPhNo" runat="server" Style="font-size: 12px;"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <%-- <asp:Label ID="lblFaxNo" Text="FAX No.:" runat="server" Style="font-size: 12px;"></asp:Label>
                                            <asp:Label ID="lblCompFaxNo" runat="server" Style="font-size: 12px;"></asp:Label><br />--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="white_bg" valign="top" colspan="4" style="border-left-style: none;
                                    border-right-style: none; border-top-style: none; border-bottom-style: none">
                                    <strong style="text-decoration: underline;">
                                        <asp:Label ID="lblPrintHeadng" runat="server" Font-Size="18px" Text="INVOICE"></asp:Label></strong>
                                </td>
                            </tr>
                            <tr id="bill_detail">
                                <td align="left" colspan="4" style="border-bottom-style: none;font-size:11px;border:none;padding: 0;">
                                    <table border="0" width="100%" style="border-collapse: collapse;">
                                        <tr>
                                            <td style="border: 1px solid black; width: 50%;border-left:0">
                                                <table style="width:100%">
                                                    <tr style="width: 100%;" align="left">
                                                        <td align="left" valign="top" style="font-size: 11px; border: none;width:65px">
                                                            <b>GSTIN No.</b>
                                                        </td>
                                                        <td align="left" valign="top" style="font-size: 11px; border: none;">
                                                            :
                                                            <asp:Label ID="lblCompGSTIN" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="width: 100%;" align="left">
                                                        <td align="left" valign="top" style="font-size: 11px; border: none;">
                                                            <b>Bill No.</b>
                                                        </td>
                                                        <td align="left" valign="top" style="font-size: 11px; border: none;">
                                                            :
                                                            <asp:Label ID="valuelblinvoicveno" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="top" style="font-size: 11px; border: none;">
                                                            <b>Bill Date</b>
                                                        </td>
                                                        <td align="left" valign="top" style="font-size: 11px; border: none;">
                                                            :
                                                            <asp:Label ID="valuelblinvoicedate" runat="server" Text="" Width="50px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="top" style="font-size: 11px; border: none;">
                                                            <b>Reg.No.</b>
                                                        </td>
                                                        <td align="left" valign="top" style="font-size: 11px; border: none;">
                                                            :
                                                            <asp:Label ID="lblCompTIN" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="top" style="font-size: 11px; border: none;">
                                                            <b>PAN No.</b>
                                                        </td>
                                                        <td align="left" valign="top" style="font-size: 11px; border: none;">
                                                            :
                                                            <asp:Label ID="lblPanNo" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 50%; border: 1px solid black; width: 50%;border-right:0" align="left" valign="top">
                                                <table width="100%">
                                                    <tr>
                                                        <td align="left" style="font-size: 11px; border-right-style: none; border-left-style: none; width: 65px">
                                                            <b>Mode</b>
                                                        </td>
                                                        <td align="left" style="font-size: 11px; border-right-style: none">
                                                            :
                                                            <asp:Label ID="lblMode" runat="server" Font-Size="11px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="font-size: 11px; border-right-style: none; border-left-style: none; width: 65px">
                                                            <b>Lorry No.</b>
                                                        </td>
                                                        <td align="left" style="font-size: 11px; border-right-style: none">
                                                            :
                                                            <asp:Label ID="lblLorryNo" runat="server" Font-Size="11px" Text="Mode"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="font-size: 11px; border-right-style: none; border-left-style: none; width: 65px">
                                                            <b>Date</b>
                                                        </td>
                                                        <td align="left" style="font-size: 11px; border-right-style: none">
                                                            :
                                                            <asp:Label ID="lblSupplyDate" runat="server" Font-Size="11px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td align="left" style="font-size: 11px; border-right-style: none; border-left-style: none; width: 65px">
                                                            <b>Del.From</b>
                                                        </td>
                                                        <td align="left" style="font-size: 11px; border-right-style: none">
                                                            :
                                                            <asp:Label ID="origion" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="font-size: 11px; border-right-style: none; border-left-style: none; width: 65px">
                                                            <b>Del.To</b>
                                                        </td>
                                                        <td align="left" style="font-size: 11px; border-right-style: none">
                                                            :
                                                            <asp:Label ID="lbldesc" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top" style="border: 1px solid #000000;background:#dadada;font-weight:bold; text-align:center;font-size: 11px;">
                                                Billing Party
                                            </td>
                                            <td align="left" valign="top" style="border: 1px solid #000000;background:#dadada;font-weight:bold; text-align:center;font-size: 11px;">
                                                Shipping Party
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid black; width: 50%;">
                                                <table>
                                                      <tr>
                                                        <td align="left" valign="top" style="font-size: 11px; border: none; width: 65px;">
                                                            <b>Name</b>
                                                        </td>
                                                        <td align="left" valign="top" style="font-size: 11px; border: none;" colspan="3">
                                                            :
                                                            <asp:Label ID="lblCustomer" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="font-size: 11px; border-right-style: none">
                                                            <b>Address</b>
                                                        </td>
                                                        <td align="left" style="font-size: 11px; border-right-style: none">
                                                            :
                                                            <asp:Label ID="Address" runat="server"></asp:Label>
                                                            <asp:Label ID="lblsendercity" runat="server"></asp:Label>
                                                            <asp:Label ID="lblsenderstate" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="font-size: 11px; border-right-style: none">
                                                            <b>GSTIN No.</b>
                                                        </td>
                                                         <td align="left" style="font-size: 11px; border-right-style: none">
                                                            :<asp:Label ID="lblSenderGSTIN" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="border: 1px solid black; width: 50%;border-right: 0;">
                                                <table>
                                                   
                                                    <tr>
                                                        <td align="left" valign="top" style="font-size: 11px; border: none; width: 65px;">
                                                            <b>Name</b>
                                                        </td>
                                                        <td align="left" valign="top" style="font-size: 11px; border: none;">
                                                            :
                                                            <asp:Label ID="lblCname" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="font-size: 11px; border-right-style: none">
                                                            <b>Address</b>
                                                        </td>
                                                        <td align="left" style="font-size: 11px; border-right-style: none">
                                                            :
                                                            <asp:Label ID="Label3" runat="server"></asp:Label>
                                                            <asp:Label ID="Label4" runat="server"></asp:Label>
                                                            <asp:Label ID="Label5" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="font-size: 11px; border-right-style: none">
                                                            <b>GSTIN No.</b>
                                                        </td>
                                                         <td align="left" style="font-size: 11px; border-right-style: none">
                                                            :<asp:Label ID="lblReceiverGSTIN" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="border-top-style: none; border-bottom-style: none;padding: 0;">
                                    <table border="1" cellspacing="0" style="font-size: 11px;border:0" width="100%" id="Table1">
                                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                            <HeaderTemplate>
                                                <tr>
                                                    <td style="font-size: 11px;font-weight:bold;" width="8%" align="center">
                                                        <strong>Date</strong>
                                                    </td>
                                                    <%--<td class="white_bg" style="font-size: 12px" width="3%">
                                                        <strong>S.No.</strong>
                                                    </td>--%>
                                                    <td style="font-size: 11px;font-weight:bold;" align="left" width="5%">
                                                        <strong>Cn. No./M No.</strong>
                                                    </td>
                                                    <td style="font-size: 11px;font-weight:bold;" width="8%">
                                                        <strong>Inv. No.</strong>
                                                    </td>
                                                    <td style="font-size: 11px;font-weight:bold;" width="5%">
                                                        <strong>Qty</strong>
                                                    </td>
                                                    <td style="font-size: 11px" width="5%">
                                                        <strong>Weight</strong>
                                                    </td>
                                                    <td style="font-size: 11px;font-weight:bold;" width="5%">
                                                        <strong>Rate</strong>
                                                    </td>
                                                    <td style="font-size: 11px;font-weight:bold;" width="5%">
                                                        <strong>Freight</strong>
                                                    </td>
                                                    <td style="font-size: 11px;font-weight:bold;" width="6%">
                                                        <strong>St Charg.</strong>
                                                    </td>
                                                    <td style="font-size: 11px;font-weight:bold;" align="left" width="6%">
                                                        <strong>Del Charg.</strong>
                                                    </td>
                                                    <td style="font-size: 11px;font-weight:bold;" align="left" width="6%">
                                                        <strong>Det. Charg.</strong>
                                                    </td>
                                                    <td style="font-size: 11px;font-weight:bold;" align="left" width="6%">
                                                        <strong>Hamali Charg.</strong>
                                                    </td>
                                                    <td style="font-size: 11px;font-weight:bold;" width="8%">
                                                        <strong>Other Charg.</strong>
                                                    </td>
                                                    <asp:Panel ID="pnlVat" runat="server">
                                                        <td style="font-size: 11px;font-weight:bold;" width="7%">
                                                            <strong>Ser Tax</strong>
                                                        </td>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlGST" runat="server">
                                                        <td style="font-size: 11px;font-weight:bold;" width="7%;">
                                                            <strong>SGST</strong>
                                                        </td>
                                                        <td style="font-size: 11px;font-weight:bold;" width="7%">
                                                            <strong>CGST</strong>
                                                        </td>
                                                        <td style="font-size: 11px;font-weight:bold;" width="7%">
                                                            <strong>IGST</strong>
                                                        </td>
                                                    </asp:Panel>
                                                    <td style="font-size: 11px;font-weight:bold;" width="6%">
                                                        <strong>Total</strong>
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="white_bg" width="5%" align="left">
                                                        <%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MM-yyyy")%>
                                                    </td>
                                                    <%--<td class="white_bg" width="3%">
                                                        <%#Container.ItemIndex+1 %>.
                                                    </td>--%>
                                                    <td class="white_bg" width="5%" align="left">
                                                        <%#Eval("GR_No")%>
                                                        /
                                                        <%#Eval("M_No")%>
                                                    </td>
                                                    <td class="white_bg" width="8%" align="left">
                                                        <%#Eval("Inv_No")%>&nbsp;
                                                    </td>
                                                    <td class="white_bg" width="5%" align="right">
                                                        <asp:Label ID="lblgrdQty" runat="server" Text='<%#Eval("Qty")%>'></asp:Label>
                                                    </td>
                                                    <td class="white_bg" width="5%">
                                                        <%#Eval("Weight")%>
                                                    </td>
                                                    <td class="white_bg" width="5%" align="right">
                                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Rate")))%>
                                                    </td>
                                                    <td class="white_bg" width="6%" align="right">
                                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%>
                                                    </td>
                                                    <td class="white_bg" width="6%" align="right">
                                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Stcharg_Amnt")))%>
                                                    </td>
                                                    <td class="white_bg" width="6%" align="right">
                                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Del_Charges")))%>
                                                    </td>
                                                    <td class="white_bg" width="6%" align="right">
                                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Deten_Ch")))%>
                                                    </td>
                                                    <td class="white_bg" width="6%" align="right">
                                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("HAMALI_CHARGES")))%>
                                                    </td>
                                                    <td class="white_bg" width="8%" align="right">
                                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Octrai_Charges")))%>
                                                    </td>
                                                    <asp:Panel ID="pnlVATValue" runat="server">
                                                        <td class="white_bg" width="7%" align="right">
                                                            <asp:Label ID="lblgrdVAT" runat="server" Text='<%#String.Format("{0:0.00}", Convert.ToDouble(Eval("ServTax_Amnt")))%>'></asp:Label>
                                                        </td>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlGSTValues" runat="server">
                                                        <td class="white_bg" width="7%" align="right">
                                                            <asp:Label ID="lblgrdSGST" runat="server" Text=' <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("SGST_Amt")))%>'></asp:Label>
                                                        </td>
                                                        <td class="white_bg" width="7%" align="right">
                                                            <asp:Label ID="lblgrdCGST" runat="server" Text='<%#String.Format("{0:0.00}", Convert.ToDouble(Eval("CGST_Amt")))%>'></asp:Label>
                                                        </td>
                                                        <td class="white_bg" width="7%" align="right">
                                                            <asp:Label ID="lblgrdIGST" runat="server" Text='<%#String.Format("{0:0.00}", Convert.ToDouble(Eval("IGST_Amt")))%>'></asp:Label>
                                                        </td>
                                                    </asp:Panel>
                                                    <td class="white_bg" width="7%" align="right">
                                                        <asp:Label ID="lblRowTotal" runat="server" Text='<%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Total_Amnt")))%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                            <%--<tr>
                                                    <td class="white_bg" width="3%">
                                                        &nbsp;
                                                    </td>
                                                    <td class="white_bg" width="5%" align="left">
                                                        &nbsp;
                                                    </td>
                                                    <td class="white_bg" width="8%" align="left">
                                                        &nbsp;
                                                    </td>
                                                    <td class="white_bg" width="5%">
                                                        <asp:Label ID="lblQty" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="white_bg" width="5%">
                                                        &nbsp;
                                                    </td>
                                                    <td class="white_bg" width="6%">
                                                    </td>
                                                    <asp:Panel ID="pnlVATFoot2" runat="server">
                                                        <td class="white_bg" width="5%">
                                                            &nbsp;
                                                        </td>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlGSTFoot2" runat="server">
                                                        <td class="white_bg" width="5%">
                                                            &nbsp;
                                                        </td>
                                                        <td class="white_bg" width="5%">
                                                            &nbsp;
                                                        </td>
                                                        <td class="white_bg" width="5%">
                                                            &nbsp;
                                                        </td>
                                                    </asp:Panel>
                                                    <td class="white_bg" width="5%">
                                                        &nbsp;
                                                    </td>
                                                    <td class="white_bg" width="6%" align="left" style="font-family: sans-serif;font-size:11px;">
                                                        
                                                    </td>
                                                    <td class="white_bg" width="6%" align="left">
                                                        &nbsp;
                                                    </td>
                                                    <td class="white_bg" width="6%" align="left">
                                                        &nbsp;
                                                    </td>
                                                    <td class="white_bg" width="8%" align="left">
                                                    </td>
                                                    <td class="white_bg" width="8%" align="left">
                                                    <b>Extra :</b>&nbsp;
                                                    </td>
                                                    <td class="white_bg" width="7%" align="right" style="font-size:11px;">
                                                        <asp:Label ID="lblExtraAmnt" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td class="white_bg" width="3%">
                                                       Tatal:
                                                    </td>
                                                    <td class="white_bg" width="5%" align="left">
                                                        &nbsp;
                                                    </td>
                                                    <td class="white_bg" width="8%" align="left">
                                                        &nbsp;
                                                    </td>
                                                    <td class="white_bg" width="5%" align=right>
                                                        <asp:Label ID="lblTotalQty" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="white_bg" width="5%">
                                                        &nbsp;
                                                    </td>
                                                    <td class="white_bg" width="6%">
                                                    </td>
                                                    <td class="white_bg" width="6%" align="left">
                                                        &nbsp;
                                                    </td>
                                                     <td class="white_bg" width="5%">
                                                        &nbsp;
                                                    </td>
                                                    <td class="white_bg" width="6%" align="left">
                                                        &nbsp;
                                                    </td>
                                                    <td class="white_bg" width="8%" align="left">
                                                    </td>
                                                    <td class="white_bg" width="8%" align="left">
                                                    </td>
                                                    <td class="white_bg" width="6%" align="left" style="font-family: sans-serif;font-size:11px;">
                                                        <b></b>
                                                    </td>
                                                     <asp:Panel ID="pnlVATFoot1" runat="server" align="right">
                                                        <td class="white_bg" width="5%">
                                                            <asp:Label ID="lblTotalVAT" runat="server"></asp:Label>
                                                        </td>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlGSTFoot1" runat="server" align="right"> 
                                                        <td class="white_bg" width="5%" align="right">
                                                            <asp:Label ID="lblTotalSGST" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="white_bg" width="5%" align="right">
                                                            <asp:Label ID="lblTotalCGST" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="white_bg" width="5%" align="right">
                                                            <asp:Label ID="lblTotalIGST" runat="server"></asp:Label>
                                                        </td>
                                                    </asp:Panel>
                                                    <td class="white_bg" width="7%" align="right" style="font-size:11px;">
                                                        <asp:Label ID="lblNetAmntMinusExtra" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <asp:HiddenField ID="hidTotalQty" runat="server" />
                            <asp:HiddenField ID="hidTotalVAT" runat="server" />
                            <asp:HiddenField ID="hidTotalSGST" runat="server" />
                            <asp:HiddenField ID="hidTotalCGST" runat="server" />
                            <asp:HiddenField ID="hidTotalIGST" runat="server" />
                            <div id="showdetl" runat="server">
                                <tr>
                                    <td style="width: 59%" style="border-top-style: none;border-right:0;">
                                        <table width="100%">
                                            <tr>
                                                <td colspan="3" align="left" width="80%" style="font-weight: bold;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDeliveryins" runat="server" Text="Invoice in Word" valign="left"
                                                                    Font-Size="11px"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblrsinword" runat="server" Font-Size="11px" valign="Left"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td colspan="1" width="25%">

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="border-left:0;font-size:11px;">
                                        <div style="display:inline-block;width:100%">
                                            <b style="width:30%;float:left">Extra </b>:
                                            <asp:Label style="width:50%;float:right;display: block;text-align: right;" ID="lblExtraAmnt" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div style="display:inline-block;width:100%">
                                            <b style="width:30%;float:left;">Net Amount</b>:
                                            <asp:Label style="width:50%;float:right;display: block;text-align: right;" ID="lblNetAmnt" runat="server"></asp:Label>
                                        </div>                                        
                                    </td>
                                   <%-- <td>
                                        <tr>
                                            <td style="font-size:11px;text-align:right"><b>SGCT rate: </b></td>
                                        </tr>
                                        <tr>
                                            <td style="font-size:11px;text-align:right"><b>CGCT rate: </b></td>
                                        </tr>
                                        <tr>
                                            <td style="font-size:11px;text-align:right"><b>IGCT rate: </b></td>
                                        </tr>
                                    </td>--%>
                                </tr>
                            </div>
                            
                            <tr>
                                <td style="font-size: 11px;width:67%;font-weight:bold;border-right: none;" align="left" valign="top">
                                    Note: Certified that the particulars given above are true and correct
                                </td>
                                <td style="font-size: 11px;border-right: 0;border-left: 0;font-weight:bold;" align="left" valign="top">
                                    
                                </td>
                            </tr>
                            
                               <tr>
                                <td style="font-size: 11px;background:#dadada;font-weight:bold;border-right: none;" align="left" valign="top">
                                    Terms And Condition
                                </td>
                                <td style="font-size: 11px;border-right: 0;background:#dadada;text-align:right;border-left:0;font-size: 11px;" align="left" valign="top">
                                    <b><asp:Label ID="lblCompName" runat="server"></asp:Label></b>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px;border-right: 0;" align="left;border-right:0" valign="top" rowspan="2">
                                   
                                </td>
                                <td style="font-size: 11px;height:40px;vertical-align: bottom;;border-left:0;border-bottom:0;padding: 15px 5px 5px 5px;" align="left">
                                    <%--<b style="font-size: 11px;width: 40%;float: left;">Signature:</b> <b style="float:left;border-bottom:1px solid #363636;font-size: 11px;width: 60%;">&nbsp;</b>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px;border-right: 0;border:0;font-size: 11px;text-align:right;font-weight:bold;" align="left" valign="top">
                                    Autorized Signatory
                                </td>
                            </tr>
                            <%--<tr>
                                <td align="left" valign="top" colspan="4">
                                    <table width="100%" style="font-size: 11px" border="0" cellspacing="0">
                                        <tr style="line-height: 25px">
                                            <td colspan="9" style="font-size: 11px" align="left" class="white_bg">
                                                <table width="100%">
                                                   
                                                    <tr>
                                                        <td align="left" class="style6" style="font-size: 11px; font-weight:bold;text-align: left;" colspan="4">
                                                            Name:
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="baseline" colspan="1" class="white_bg" style="font-size: 11px;font-weight:bold;text-align: left;">
                                                            Designation:
                                                        </td>
                                                        
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>--%>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hideimgvalue" runat="server" />
    <asp:HiddenField ID="hidPages" runat="server" />
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = "";
            var Pages = "1";
            Pages = document.getElementById("<%=hidPages.ClientID%>").value;
            var prtContent3 = "<p style='page-break-before: always'></p>";
            for (i = 0; i < Pages; i++) {
                prtContent = prtContent + "<table width='100%' border='0'></table>";
                if (Pages != 1) {
                    prtContent = prtContent + "<tr><td><strong>" + ((i == 1) ? "[Extra Copy]" : (i == 2) ? "[H.O. Copy]" : (i == 3) ? "[Party Copy]" : "[Party Copy]") + "</strong></td></tr>";
                }
                var prtContent1 = document.getElementById(strid);
                var prtContent2 = prtContent1.innerHTML;
                prtContent = prtContent + prtContent2 + ((i < 3) ? prtContent3 : "");
            }


            var WinPrint = window.open('', '', 'left=0,top=0,width=700px,height=450px,toolbar=1,scrollbars=1,status=1');
            WinPrint.document.write(prtContent);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            return false;
        }
       
    </script>
    </form>
</body>
</html>
