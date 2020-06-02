<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintInvJKCementGST.aspx.cs" Inherits="WebTransport.PrintInvJKCementGST" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style>
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <%--HIDDEN FIELDS--%>
    <asp:HiddenField ID="hidIsGST" runat="server" />
    <div id="printShreeCementLtdBeawer" visible="false" runat="server">
         <table  cellpadding="1" cellspacing="0" width="800px" border="0" style="font-family: Arial,Helvetica,sans-serif;
            border-width: 1px; border-color: #000000;">
             <%--MAINTAIN WIDTH--%>
            <tr><td style="width:15%" align="left"><table style="height:1px;" width="600px"></table></td><td><table style=" height:1px;" width="232px"></table></td></tr>
            <%--/MAINTAIN WIDTH--%>
            
            <tr id="header" runat="server">
                <td align="left" style="width:100%;" colspan="2">
                    <table width="100%" style="font-family: Arial,Helvetica,sans-serif;">
                        <tr style="display:none">
                            <td style="font-size: 11px;">
                                
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="font-size: 11px; text-align: right;display:none">
                                <asp:Label ID="lblOwnerPhoneNo" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lblCode1" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center;">
                                <asp:Label style="float:left;position: absolute;text-align: left;left: 17px;font-size: 11px;" ID="lblJurCity" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lblCompName" runat="server" Text="" style="font-weight: bold;font-size: 12px;"></asp:Label>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center;font-size: 11px;">
                                <asp:Label ID="lblCompAddress1" runat="server" Text=""></asp:Label>
                                <%if (lblCompAddress2.Text != "")
                                  { %>
                                  <br />
                                <%} %>
                                <asp:Label style="text-align: center;font-size: 11px;" ID="lblCompAddress2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size: 11px;text-decoration:underline;text-align: center;line-height: 2px;">
                                <b><u>TRANSPORTATION FREIGHT BILL (Basic) : CEMENT-MGR</u></b>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="display:none">
                    <table width="232px" style="font-family: Arial,Helvetica,sans-serif; border: 3px solid black;
                        height: 98px;">
                        <tr>
                            <td style="text-align: left; font-size: 11px">
                                
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; font-size: 11px">
                                Quantity and freight rate varified.
                            </td>
                        </tr>
                        <tr style="text-align: left; font-size: 11px">
                            <td>
                                Bill passed for Rs...............
                            </td>
                        </tr>
                        <tr style="text-align: left; font-size: 11px">
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr style="text-align: right; font-size: 11px">
                            <td>
                                <b>Authorised Signature</b>
                            </td>
                        </tr>
                    </table>
                </td>
               </tr>
            <tr>
                <td colspan="2">
                    <table width="100%">
                    <style>
                    .gap-less td
                    {
                        line-height:10px;
                    }
                    </style>
                        <tr>
                            <td style="width:75%">
                                <table style="font-size: 11px;" class="gap-less">
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="lblCodeSap" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Consignor / Bill To</td>
                                    </tr>
                                    <tr>
                                    <td><asp:Label ID="lblPartyName" runat="server" Text=""></asp:Label><br />
                                            <asp:Label ID="lblPartyAddress1" runat="server" Text=""></asp:Label>
                                            <%if (lblPartyAddress2.Text != "")
                                              { %>
                                            <br />
                                            <%} %>
                                            <asp:Label ID="lblPartyAddress2" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr <%if(lblPartyPanNo.Text == ""){ %>style="display:none;"<%} %>>
                                        <td>
                                            <asp:Label ID="lblPartyPinCode" runat="server" Text=""></asp:Label>
                                            <%if (lblPartyPanNo.Text != "")
                                              { %>
                                              <br />
                                              <%} %>
                                            <asp:Label ID="lblPartyPanNo" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td><asp:Label ID="lblGSTINSender" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr <%if(lblSacCode.Text == ""){ %>style="display:none;"<%} %>>
                                        <td>
                                            <asp:Label ID="lblSacCode" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="vertical-align: top;">
                                <table width="100%" style="font-size: 11px;" class="gap-less">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblBillNo" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblBillDate" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCompPanNo" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="lblGSTINComp" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="lblStatus1" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="display:none">
                                            <asp:Label ID="lblServTaxNo" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--<tr>
                <td colspan="4" align="center" style="font-size: 11px;" >
                <div style="font-weight:bold;width:24.5%;float:left;border: 1px solid #6e6e6e;border-right: none">Place Of Supply</div>
                <div style="font-weight:bold;width:25%;float:left;border: 1px solid #6e6e6e;border-right: none">Rajasthan</div>
                <div style="font-weight:bold;width:25%;float:left;border: 1px solid #6e6e6e;border-right: none">State Code</div>
                <div style="font-weight:bold;width:25%;float:left;border: 1px solid #6e6e6e;border-left: none">8</div>
                </td>
            </tr>--%>
             <tr>
                <td colspan="2" style="position: relative;top: 3px;">
                    <table style="border-collapse: collapse;width: 100%;font-size: 11px;font-weight: bold;text-align:center">
                        <tr>
                            <td style="border:1px solid #969494;width: 42.2%;">Place of supply</td>
                            <td style="border:1px solid #969494;width: 17%;"><asp:Label ID="lblSupplyStateName" runat="server" ></asp:Label></td>
                            <td style="border:1px solid #969494;width: 20.5%;">State Code</td>
                            <td style="border:1px solid #969494;"><asp:Label ID="lblSupplyStateId" runat="server" ></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table border="1" cellspacing="0" cellpadding="0" style="font-size: 11px;" width="100%" id="Table1">
                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                            <HeaderTemplate>
                                <tr>
                                    <td class="white_bg" style="font-size: 11px;" width="3%">
                                        <strong>S.No.</strong>
                                    </td>
                                    <td style="font-size: 11px;" align="left" width="20%">
                                        <strong>Consignee</strong>
                                    </td>
                                    <td style="font-size: 11px;" width="7%" align="left">
                                        <strong>Destination</strong>
                                    </td>
                                    <td style="font-size: 11px;" width="7%" align="left">
                                        <strong>Vehicle No</strong>
                                    </td>
                                    <td style="font-size: 11px;" width="7%" align="left">
                                        <strong>LR No</strong>
                                    </td>
                                    <td style="font-size: 11px;" width="5%" align="left">
                                        <strong>Comm.Inv.No</strong>
                                    </td>
                                    <td style="font-size: 11px;" align="left" width="7%">
                                        <strong>Disp.Date</strong>
                                    </td>
                                     <td style="font-size: 11px;" width="6%" align="right">
                                        <strong>Bags LD</strong>
                                    </td>
                                    <td style="font-size: 11px;" width="6%" align="right">
                                        <strong>Bags UL</strong>
                                    </td>
                                    <%--<td style="font-size: 11px;" width="7%" align="left">
                                        <strong>Grade</strong>
                                    </td>--%>
                                    <td style="font-size: 11px;display:none" width="6%" align="right">
                                        <strong>Dsp.Qty</strong>
                                    </td>
                                   
                                    <td style="font-size: 11px;" width="6%" align="right">
                                        <strong>Rate/MT</strong>
                                    </td>
                                     <%--<td style="font-size: 11px;" width="6%" align="right">
                                        <strong>Weight</strong>
                                    </td>--%>
                                    <td style="font-size: 11px;" width="6%" align="right">
                                        <strong>Freight</strong>
                                    </td>
                                    <td style="font-size: 11px;display:none;" width="8%" align="right">
                                        <strong><asp:Label ID="lblHeadWages" runat="server"></asp:Label></strong>
                                    </td>
                                    <%--<td style="font-size: 11px;" width="6%" align="right">
                                        <strong>Labour</strong>
                                    </td>--%>
                                  <%--  <td style="font-size: 11px;display:none;" width="6%" align="right">
                                        <strong>TotalFrt</strong>
                                    </td>--%>
                                    <asp:Panel ID="pnlVATHeader" runat="server">
                                    <td style="font-size: 11px;display:none" width="6%" align="right">
                                        <strong>S.Tax</strong>
                                    </td>
                                    <td style="font-size: 11px;display:none" width="6%" align="right">
                                        <strong>SBCess</strong>
                                    </td>
                                    <td style="font-size: 11px;display:none" width="6%" align="right">
                                        <strong>KKCess</strong>
                                    </td>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlGSTHeader" runat="server">
                                    <td style="font-size: 11px;display:none" width="6%" align="right">
                                        <strong>SGST</strong>
                                    </td>
                                    <td style="font-size: 11px;display:none" width="6%" align="right">
                                        <strong>CGST</strong>
                                    </td>
                                    <td style="font-size: 11px;display:none" width="6%" align="right">
                                        <strong>IGST</strong>
                                    </td>
                                    </asp:Panel>
                                    <td style="font-size: 11px;" width="6%" align="right">
                                        <strong>Bags Sh</strong>
                                    </td>
                                    <%--<td style="font-size: 11px;" width="6%" align="right">
                                        <strong>Wht</strong>
                                    </td>--%>
                                </tr>
                            </HeaderTemplate>
                             <ItemTemplate>
                                <tr>
                                    <td class="white_bg" style="font-size: 11px;">
                                        <%#Container.ItemIndex+1 %>.
                                    </td>
                                    <td style="font-size: 11px;" align="left">
                                        <%#Eval("Recivr_Name")%>
                                    </td>
                                    <td style="font-size: 11px;" align="left">
                                        <%#Eval("Delvry_Place")%>
                                    </td>
                                    <td style="font-size: 11px;" align="left">
                                        <%#Eval("Lorry_No")%>
                                    </td>
                                    <td style="font-size: 11px;" align="left">
                                        <%#Eval("GR_No")%>
                                    </td>
                                    <td style="font-size: 11px;" align="left">
                                        <%#Eval("EGP_NO")%>
                                    </td>
                                    <td style="font-size: 11px;" align="left">
                                        <%#Convert.ToDateTime(Eval("Chln_Date")).ToString("dd-MM-yyyy")%>
                                    </td>
                                     <td style="font-size: 11px;" align="right">
                                        <%#Eval("Loaded", "{0:F2}")%>
                                    </td>
                                    <td style="font-size: 11px;" align="right">
                                        <%#Eval("UnLoaded", "{0:F2}")%>
                                    </td>
                                    <%--<td style="font-size: 11px;" align="left">
                                        <%#Eval("Grade")%>
                                    </td>--%>
                                    <td style="font-size: 11px;display:none" align="right">
                                        <%#Eval("Weight")%>
                                    </td>
                                   
                                    <td style="font-size: 11px;" align="right">
                                        <%#Eval("Rate", "{0:F2}")%>
                                    </td>
                                  <%--  <td style="font-size: 11px;" align="right">
                                        <%#Eval("Tot_Weght")%>
                                    </td>--%>
                                    <%--<td style="font-size: 11px;" align="right">
                                        <%#Eval("Freight")%>
                                    </td>--%>
                                    <%-- <td style="font-size: 11px;" align="right">
                                        <%#Eval("Wages_Amnt")%>
                                    </td>--%>
                                    <%--<td style="font-size: 11px;" align="right">
                                        <%#Eval("Labour")%>
                                    </td>--%>
                                    <td style="font-size: 11px;" align="right">
                                        <%#Eval("amount","{0:F2}")%>
                                    </td>
                                    <asp:Panel ID="pnlVATContent" runat="server">
                                    <td style="font-size: 11px;" align="right">
                                      <%--  <%#Eval("ServTax_Amnt")%>--%>
                                    </td>
                                    <td style="font-size: 11px;" align="right">
                                      <%--  <%#Eval("SwchBrtTax_Amt")%>--%>
                                    </td>
                                    <td style="font-size: 11px;" align="right">
                                        <%--<%#Eval("KisanKalyan_Amnt")%>--%>
                                    </td>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlGSTContent" runat="server">
                                    <td style="font-size: 11px;display:none" align="center">
                                        <%#Eval("SGST_Per", "{0:F0}")%>%<br /><%#Eval("SGST_Amt")%>
                                    </td>
                                    <td style="font-size: 11px;display:none" align="center">
                                        <%#Convert.ToString(Eval("CGST_Per", "{0:F2}"))%>%<br /><%#Eval("CGST_Amt")%>
                                    </td>
                                    <td style="font-size: 11px;display:none" align="center">
                                        <%#Eval("IGST_Per", "{0:F0}")%>%<br /><%#Eval("IGST_Amt")%>
                                    </td>
                                    </asp:Panel>
                                    <td style="font-size: 11px;" align="right">
                                        <%#Eval("Shortage","{0:F2}")%>
                                    </td>
                                  <%--  <td style="font-size: 11px;" align="right">
                                        <%#Eval("0.00")%>
                                    </td>--%>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td style="font-size: 11px;text-align:right" colspan="7">
                                        <b>Total:</b>
                                    </td>
                                    
                                    <td style="font-size: 11px;" align="right">
                                    <strong>
                                            <asp:Label ID="lblTotalloading" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 11px;" align="right">
                                    <strong>
                                            <asp:Label ID="lblTotalUnloading" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 11px;display:none" align="center">                                        
                                    </td>
                                    <td style="font-size: 11px;" align="right">
                                    <strong>
                                            <asp:Label ID="lblTotalQty" style="display:none" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 11px;display:none" align="right">
                                        
                                    </td>
                                     <td style="font-size: 11px;display:none;" align="right">
                                        <strong>
                                            <asp:Label ID="lblTotalWeight" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 11px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblTotalFreight" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 11px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblTotalShBags" runat="server" ></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 11px;display:none;" align="right">
                                        <strong>
                                            <asp:Label ID="lblTotalFrt" runat="server"></asp:Label></strong>
                                    </td>
                                    <asp:Panel ID="pnlVATFooter" runat="server">
                                    <td style="font-size: 11px;display:none" align="right">
                                        <strong>
                                            <asp:Label ID="lblTotalSTaxAmnt" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 11px;display:none" align="right">
                                        <strong>
                                            <asp:Label ID="lblTotalSBTaxAmnt" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 11px;display:none" align="right">
                                        <strong>
                                            <asp:Label ID="lblTotalKKAmnt" runat="server"></asp:Label></strong>
                                    </td>
                                    </asp:Panel>
                                     <asp:Panel ID="pnlGSTFooter" runat="server">
                                    <td style="font-size: 11px;display:none" align="right">
                                        <strong>
                                            <asp:Label ID="lblSGSTFooter" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 11px;display:none" align="right">
                                        <strong>
                                            <asp:Label ID="lblCGSTFooter" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 11px;display:none" align="right">
                                        <strong>
                                            <asp:Label ID="lblIGSTFooter" runat="server"></asp:Label></strong>
                                    </td>
                                    </asp:Panel>
                                    
                                    <%--<td style="font-size: 11px;">
                                    </td>--%>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" style="font-size: 11px">
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" style="font-size: 11px">
                    <b>
                        <asp:Label ID="lblRupeesWord" runat="server" Text=""></asp:Label></b>
                </td>
            </tr>
            <tr>
            <td>
               <table border="1" cellspacing="0" cellpadding="0" style="font-size: 11px;" width="300px" id="Table2">
                    <%foreach (var a in LstGst)
                      { %>
                      <tr><td>SGST <%=a.SGSTPer %>% </td><td align="right"> <%=a.SGSTAmt.ToString("N2") %></td></tr>
                      <tr><td>CGST <%=a.CGSTPer %>% </td><td align="right"> <%=a.CGSTAmt.ToString("N2")%></td></tr>
                      <tr><td>IGST <%=a.IGSTPer%>% </td> <td align="right"> <%=a.IGSTAmt.ToString("N2")%></td></tr>
                    <%} %>
                </table> 
                <span style="font-size:11px;">GST of freight as applicable will be paid by service recepient under RCM</span>
                <br />
                <div style="margin:4px;"></div>
                <span style="font-size:11px;font-weight:bold;">We hereby certify that input tax credit on inputs, capital goods & input services, used for providing  services of transportation, has not been taken by service provider under the provision of goods and service Tax Act & Rules 2017.</span>
            </td>
            </tr>
            <tr>
                <td valign=top>
                    <table cellpadding="0" cellpadding="0"  class="tableNew" cellspacing="0"  style="font-size: 11px;border-collapse: collapse; font-family: Arial,Helvetica,sans-serif;" width="100%">
                    <tr><td>&nbsp;</td></tr>
                        <tr>
                            <td>Document No: </td>
                            <td>Bill Passed By:</td>
                        </tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr>
                            <td>Debit Note No:</td>
                            <td>Credit Note No:</td>
                        </tr>
                    </table>
                </td>
                <td style="width:20%">
                    <table width="100%">
                        <tr>
                            <td style="font-size: 12px;height:50px;text-align: right;">
                             <b>For <asp:Label ID="lblSignCompName" runat="server"></asp:Label></b>
                            </td>
                        </tr>
                       
                        <tr>
                            <td style="font-size: 12px; text-align: right">
                                <b>Authorised Signatory</b>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>

    </form>
</body>
</html>
