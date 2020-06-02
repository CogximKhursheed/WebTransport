<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintShreeCementLtd.aspx.cs" Inherits="WebTransport.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style>
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="printShreeCementLtdBeawer" visible="false" runat="server">
        <table  cellpadding="1" cellspacing="0" width="82%" border="0" style="font-family: Arial,Helvetica,sans-serif;
            border-width: 1px; border-color: #000000;">
             <%--MAINTAIN WIDTH--%>
            <tr><td style="width:15%" align="left"><table style="height:1px;" width="700px"></table></td><td style="padding-left:130px;"><table style=" height:1px;" width="232px"></table></td></tr>
            <%--/MAINTAIN WIDTH--%>
            
            <tr id="header" runat="server">
                <td align="left" style="width:15%;">
                    <table width="700px" style="font-family: Arial,Helvetica,sans-serif; border: 3px solid black;
                        height: 98px;">
                        <tr>
                            <td style="font-size: 12px;">
                                <asp:Label ID="lblJurCity" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="font-size: 12px; text-align: right;">
                                <asp:Label ID="lblOwnerPhoneNo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center;">
                                <u><b>
                                    <asp:Label ID="lblCompName" runat="server" Text=""></asp:Label></b></u> &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center;">
                                <asp:Label ID="lblCompAddress1" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center; font-size: 12px">
                                <asp:Label ID="lblCompAddress2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="padding-left:130px;" >
                    <table width="232px" style="font-family: Arial,Helvetica,sans-serif; border: 3px solid black;
                        height: 98px;">
                        <tr>
                            <td style="text-align: left; font-size: 12px">
                                <asp:Label ID="lblCode1" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; font-size: 12px">
                                Quantity and freight rate varified.
                            </td>
                        </tr>
                        <tr style="text-align: left; font-size: 12px">
                            <td>
                                Bill passed for Rs...............
                            </td>
                        </tr>
                        <tr style="text-align: left; font-size: 12px">
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr style="text-align: right; font-size: 12px">
                            <td>
                                <b>Authorised Signature</b>
                            </td>
                        </tr>
                    </table>
 </td>
               </tr>
            
            <tr>
                <td colspan="2" style="font-size: 12px; height:30px; text-align: center">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="font-size: 12px; height:30px; text-align: center">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="font-size: 12px; text-align: center">
                    <b><u>FREIGHT / UNLOADING BILL FOR DEPOT</u></b>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="100%">
                        <tr>
                            <td style="width:80%">
                                <table style="font-size: 12px;">
                                    <tr>
                                        <td>
                                            Code:  0
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            To: &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPartyName" runat="server" Text=""></asp:Label><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblPartyAddress1" runat="server" Text=""></asp:Label>
                                            <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblPartyAddress2" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="lblPartyPinCode" runat="server" Text=""></asp:Label><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblPartyPanNo" runat="server" Text=""></asp:Label><br />
                                            

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
                            <td>
                                <table width="100%" style="font-size: 12px;">
                                    <tr>
                                        <td>
                                            <b><asp:Label ID="lblBillNo" runat="server" Text=""></asp:Label></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b><asp:Label ID="lblBillDate" runat="server" Text=""></asp:Label></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCompPanNo" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblServTaxNo" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table border="1" cellspacing="0" cellpadding="0" style="font-size: 12px;" width="100%" id="Table1">
                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                            <HeaderTemplate>
                                <tr>
                                    <td class="white_bg" style="font-size: 12px;" width="3%">
                                        <strong>S.No.</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="5%" align="center">
                                        <strong>Ch.No.</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="5%" align="center">
                                        <strong>Gr.No.</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="5%" align="center">
                                        <strong>Veh.No.</strong>
                                    </td>
                                    <td style="font-size: 12px;" align="left" width="7%">
                                        <strong>DispDate</strong>
                                    </td>
                                    <td style="font-size: 12px;" align="left" width="20%">
                                        <strong>Consignee</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="7%" align="left">
                                        <strong>Station</strong>
                                    </td>
                                    <%--<td style="font-size: 12px;" width="7%" align="left">
                                        <strong>Grade</strong>
                                    </td>--%>
                                    <td style="font-size: 12px;" width="6%" align="right">
                                        <strong>DspQty</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="6%" align="right">
                                        <strong>Rate/MT</strong>
                                    </td>
                                     <td style="font-size: 12px;" width="6%" align="right">
                                        <strong>Weight</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="6%" align="right">
                                        <strong>Freight</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="8%" align="right">
                                        <strong><asp:Label ID="lblHeadWages" runat="server"></asp:Label></strong>
                                    </td>
                                    <%--<td style="font-size: 12px;" width="6%" align="right">
                                        <strong>Labour</strong>
                                    </td>--%>
                                    <td style="font-size: 12px;" width="6%" align="right">
                                        <strong>TotalFrt</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="6%" align="right">
                                        <strong>S.Tax</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="6%" align="right">
                                        <strong>SBCess</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="6%" align="right">
                                        <strong>KKCess</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="6%" align="right">
                                        <strong>ShBags</strong>
                                    </td>
                                    <%--<td style="font-size: 12px;" width="6%" align="right">
                                        <strong>Wht</strong>
                                    </td>--%>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="white_bg" style="font-size: 12px;">
                                        <%#Container.ItemIndex+1 %>.
                                    </td>
                                    <td style="font-size: 12px;" align="center">
                                        <%#Eval("Chln_No")%>
                                    </td>
                                    <td style="font-size: 12px;" align="center">
                                        <%#Eval("GR_Prefix")%> <%#Eval("GR_No")%>
                                    </td>
                                    <td style="font-size: 12px;" align="center">
                                        <%#Eval("LORRY_NO")%>
                                    </td>
                                    <td style="font-size: 12px;" align="left">
                                        <%#Convert.ToDateTime(Eval("Chln_Date")).ToString("dd-MM-yyyy")%>
                                    </td>
                                    <td style="font-size: 12px;" align="left">
                                        <%#Eval("Consignee")%>
                                    </td>
                                    <td style="font-size: 12px;" align="left">
                                        <%#Eval("Delvry_Place")%>
                                    </td>
                                    <%--<td style="font-size: 12px;" align="left">
                                        <%#Eval("Grade")%>
                                    </td>--%>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("Dsp_Qty")%>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("Rate")%>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("Tot_Weght")%>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("Freight")%>
                                    </td>
                                     <td style="font-size: 12px;" align="right">
                                        <%#Eval("Wages_Amnt")%>
                                    </td>
                                    <%--<td style="font-size: 12px;" align="right">
                                        <%#Eval("Labour")%>
                                    </td>--%>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("TotalFrt")%>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("ServTax_Amnt")%>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("SwchBrtTax_Amt")%>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("KisanKalyan_Amnt")%>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("Shortage_Qty")%>
                                    </td>
                                  <%--  <td style="font-size: 12px;" align="right">
                                        <%#Eval("0.00")%>
                                    </td>--%>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td style="font-size: 12px;">
                                        
                                    </td>
                                    <td style="font-size: 12px;"  align="left">
                                    </td>
                                    <td style="font-size: 12px;" align="left">
                                    </td>
                                    <td style="font-size: 12px;"  align="left">
                                    </td>
                                    <td style="font-size: 12px;" align="left">
                                    </td>
                                    <td style="font-size: 12px;" align="left">
                                    </td>
                                    <td style="font-size: 12px;" align="center">
                                        <b>Total:</b>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblTotalQty" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        
                                    </td>
                                     <td style="font-size: 12px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblTotalWeight" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblTotalFreight" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblTotalUnloading" runat="server"></asp:Label></strong>
                                    </td>
                                    <%--<td style="font-size: 12px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblTotalLabour" runat="server"></asp:Label></strong>
                                    </td>--%>
                                    <td style="font-size: 12px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblTotalFrt" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblTotalSTaxAmnt" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblTotalSBTaxAmnt" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblTotalKKAmnt" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblTotalShBags" runat="server"></asp:Label></strong>
                                    </td>
                                    <%--<td style="font-size: 12px;">
                                    </td>--%>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" style="font-size: 12px">
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" style="font-size: 12px">
                    <b>
                        <asp:Label ID="lblRupeesWord" runat="server" Text=""></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" style="font-size: 12px">
                </td>
            </tr>
            <tr>
                <td>
                    <table border="1" cellpadding="0" cellpadding="0"  class="tableNew" border="1" cellspacing="0"  style="font-size: 12px; font-family: Arial,Helvetica,sans-serif;" width="100%">
                        <tr>
                            <td style="font-size: 12px; height: 30px; width:23%;" >
                                Service Tax
                            </td>
                            <td style="font-size: 12px; text-align: right; width:34%; height: 30px;">
                                 <b><asp:Label ID="lblDivServTaxTotal" runat="server" Text=""></asp:Label></b>
                            </td>
                            <td style="font-size: 12px; text-align: center;width:43%; height: 30px;" rowspan="4">
                                <b>"We Hereby certify the CEVAT credit on inputs, capital goods and input services,used
                                    for providing the taxable service of transportation has not been taken by the service
                                    provider under the provision of the CENVAT credit Rules 2004."</b>
                            </td>
                        </tr>
                        <tr style="border-color:Black;border-style:solid; border-width:1px">
                            <td style="font-size: 12px; height: 30px;">
                                Swacha Bharat Cess
                            </td>
                            <td style="font-size: 12px; text-align: right; height: 30px;">
                                 <b><asp:Label ID="lblDivSwachBhTotal" runat="server" Text=""></asp:Label></b>
                            </td>
                        </tr>
                        <tr style="border-color:Black;border-style:solid; border-width:1px">
                            <td style="font-size: 12px; height: 30px;">
                                Krisi Kalyan Cess
                            </td>
                            <td style="font-size: 12px; text-align: right; height: 30px;">
                                 <b><asp:Label ID="lblDivKissanTaxTotal" runat="server" Text=""></asp:Label></b>
                            </td>
                        </tr>
                        <tr style="border-color:Black;border-style:solid; border-width:1px">
                            <td style="font-size: 12px; height: 30px;">
                                Sec&Higher Edu.Cess
                            </td>
                            <td style="font-size: 12px; text-align: right; height: 30px;">
                                <b><asp:Label ID="lblDivEduTaxTotal" runat="server" Text="0.00"></asp:Label></b>
                            </td>
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
                            <td style="font-size: 12px; text-align: right;height:15px"></td>
                        </tr>
                         <tr>
                            <td style="font-size: 12px; text-align: right;height:15px"></td>
                        </tr>
                         <tr>
                            <td style="font-size: 12px; text-align: right;height:15px"></td>
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

    <div id="printShreeCementLtdRas" visible="false" runat="server">
        <table cellpadding="1" cellspacing="0" width="82%" border="0" style="font-family: Arial,Helvetica,sans-serif;
            border-width: 1px; border-color: #000000;">
            <%--MAINTAIN WIDTH--%>
            <tr><td style="width:15%" align="left"><table style="height:1px;" width="700px"></table></td><td style="padding-left:130px;"><table style=" height:1px;" width="232px"></table></td></tr>
            <%--/MAINTAIN WIDTH--%>
            
           <tr id="Header1" runat="server">
                <td align="left" style="width:15%">
                    <table width="700px" style="font-family: Arial,Helvetica,sans-serif; border: 3px solid black;
                        height: 98px;">
                        <tr>
                            <td style="font-size: 12px;">
                                <asp:Label ID="lblRasJurCity" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="font-size: 12px; text-align: right;">
                                <asp:Label ID="lblRasOwnerPhoneNo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center;">
                                <u><b>
                                    <asp:Label ID="lblRasCompName" runat="server" Text=""></asp:Label></b></u> &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center;">
                                <asp:Label ID="lblRasCompAddress1" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center; font-size: 12px">
                                <asp:Label ID="lblRasCompAddress2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="padding-left:130px;" >
                    <table width="232px" style="font-family: Arial,Helvetica,sans-serif; border: 3px solid black;
                        height: 98px;">
                        <tr>
                            <td style="text-align: left; font-size: 12px">
                                <asp:Label ID="lblRasCode1" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; font-size: 12px">
                                Quantity and freight rate varified.
                            </td>
                        </tr>
                        <tr style="text-align: left; font-size: 12px">
                            <td>
                                Bill passed for Rs...............
                            </td>
                        </tr>
                        <tr style="text-align: left; font-size: 12px">
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr style="text-align: right; font-size: 12px">
                            <td>
                                <b>Authorised Signature</b>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="font-size: 12px; height:30px; text-align: center">
                </td>
            </tr>
            <tr>
<td colspan="2" style="font-size: 12px; height:30px; text-align: center">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="font-size: 12px; text-align: center">
                    <b><u>TRANSPORTATION FREIGHT BILL (
                    <%if (RASUn == 1)
                      { %>
                    Unloading
                    <%}
                      else
                      { %>
                    Basic
                    <%} %>
                    ) : CEMENT FWD BNG</u></b>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="100%">
                        <tr>
                            <td style="width:80%">
                                <table style="font-size: 12px;">
                                    <tr>
                                        <td>
                                            Code:  0
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            To: &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblRasPartyName" runat="server" Text=""></asp:Label><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblRasPartyAddress1" runat="server" Text=""></asp:Label>
                                            <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblRasPartyAddress2" runat="server" Text=""></asp:Label>
                                              <asp:Label ID="lblRasPartyPinCode" runat="server" Text=""></asp:Label><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblRasPartyPanNo" runat="server" Text=""></asp:Label><br />
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
                            <td>
                                <table width="100%" style="font-size: 12px;">
                                    <tr>
                                        <td>
                                            <b><asp:Label ID="lblRasBillNo" runat="server" Text=""></asp:Label></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b><asp:Label ID="lblRasBillDate" runat="server" Text=""></asp:Label></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblRasCompPanNo" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblRasServTaxNo" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table border="1" cellpadding="0" cellspacing="0" style="font-size: 12px;" width="100%" id="Table2">
                        <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                            <HeaderTemplate>
                                <tr>
                                    <td class="white_bg" style="font-size: 12px;" width="3%">
                                        <strong>S.No.</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="5%" align="center">
                                        <strong>LRNo</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="10%" align="center">
                                        <strong>DINo</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="10%" align="center">
                                        <strong>Vehicle No</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="10%" align="center">
                                        <strong>EGP No</strong>
                                    </td>
                                    <td style="font-size: 12px;" align="center" width="7%">
                                        <strong>DispDate</strong>
                                    </td>
                                    <td style="font-size: 12px;" align="center" width="20%">
                                        <strong>Consignee</strong>
                                    </td>
                                    <%if (RASUn == 1)
                                      { %>
                                    <td style="font-size: 12px;" width="5%" align="center">
                                        <strong>Ch.No.</strong>
                                    </td>
                                    <%} %>
                                    <td style="font-size: 12px;" width="7%" align="center">
                                        <strong>Station</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="7%" align="center">
                                        <strong>Grade</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="8%" align="right">
                                        <strong>DspQty</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="8%" align="right">
                                        <strong>Rate/MT</strong>
                                    </td>
                                    <%--<td style="font-size: 12px;" width="8%" align="right">
                                        <strong>Weight</strong>
                                    </td>--%>
                                    <td style="font-size: 12px;" width="8%" align="right">
                                        <strong>Freight</strong>
                                    </td>
                                    <%--<td style="font-size: 12px;" width="8%" align="right">
                                        <strong><asp:Label ID="lblRasHeadWages" runat="server"></asp:Label></strong>
                                    </td>--%>
                                    <td style="font-size: 12px;" width="8%" align="right">
                                        <strong>Labour</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="8%" align="right">
                                        <strong>TotalFrt</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="8%" align="right">
                                        <strong>S.Tax</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="8%" align="right">
                                        <strong>SBCess</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="8%" align="right">
                                        <strong>KKCess</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="8%" align="right">
                                        <strong>ShBags</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="6%" align="right">
                                        <strong>Wht</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="white_bg" style="font-size: 12px;">
                                        <%#Container.ItemIndex+1 %>.
                                    </td>
                                    <td style="font-size: 12px;" align="center">
                                        <%#Eval("GR_No")%>
                                    </td>
                                    <td style="font-size: 12px;" align="center">
                                        <%#Eval("DI_NO")%>
                                    </td>
                                    <td style="font-size: 12px;" align="center">
                                        <%#Eval("LORRY_NO")%>
                                    </td>
                                    <td style="font-size: 12px;" align="center">
                                        <%#Eval("EGP_NO")%>
                                    </td>
                                    <td style="font-size: 12px;" align="center">
                                        <%#Convert.ToDateTime(Eval("Chln_Date")).ToString("dd-MM-yyyy")%>
                                    </td>
                                    <td style="font-size: 12px;" align="center">
                                        <%#Eval("Consignee")%>
                                    </td>
                                    <%if (RASUn == 1)
                                      { %>
                                    <td style="font-size: 12px;" align="center">
                                        <%#Eval("Chln_No")%>
                                    </td>
                                    <%} %>
                                    <td style="font-size: 12px;" align="center">
                                        <%#Eval("Delvry_Place")%>
                                    </td>
                                    <td style="font-size: 12px;" align="center">
                                        <%#Eval("Grade")%>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("Dsp_Qty")%>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("Rate")%>
                                    </td>
                                    <%--<td style="font-size: 12px;" align="right">
                                        <%#Eval("Tot_Weght")%>
                                    </td>--%>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("Freight")%>
                                    </td>
                                  <%--  <td style="font-size: 12px;" align="right">
                                        <%#Eval("Wages_Amnt")%>
                                    </td>--%>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("Labour")%>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("TotalFrt")%>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("ServTax_Amnt")%>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("SwchBrtTax_Amt")%>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("KisanKalyan_Amnt")%>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("Shortage_Qty")%>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        &nbsp;
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td style="font-size: 12px;">
                                        
                                    </td>
                                    <td style="font-size: 12px;" align="center">
                                    </td>
                                    <td style="font-size: 12px;" align="center">
                                    </td>
                                    <td style="font-size: 12px;">
                                    </td>
                                    <td style="font-size: 12px;">
                                    </td>
                                    <td style="font-size: 12px;" align="left">
                                    </td>
                                    <td style="font-size: 12px;" align="left">
                                    </td>
                                    <td style="font-size: 12px;">
                                    </td>
                                    <%if (RASUn == 1)
                                       { %>
                                    <td style="font-size: 12px;" align="right">
                                       
                                    </td>
                                    <%} %>
                                    <td style="font-size: 12px;" align="center">
                                            <b>Total:</b>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                         <strong>
                                            <asp:Label ID="lblRasTotalQty" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                       
                                    </td>
                                   <%-- <td style="font-size: 12px;" align="right">
                                       <strong>
                                            <asp:Label ID="lblRasTotalWeight" runat="server"></asp:Label></strong>
                                    </td>--%>
                                    <td style="font-size: 12px;" align="right">
                                     <strong>
                                            <asp:Label ID="lblRasTotalFreight" runat="server"></asp:Label></strong>
                                    </td>
                                   <%-- <td style="font-size: 12px;"  align="right">
                                       <strong>
                                            <asp:Label ID="lblRasUnloading" runat="server"></asp:Label></strong>
                                    </td>--%>
                                    <td style="font-size: 12px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblRasTotalLabour" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblRasTotalFrt" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblRasTotalSTaxAmnt" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblRasTotalSBTaxAmnt" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblRasTotalKKAmnt" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblRasTotalShBags" runat="server"></asp:Label></strong>
                                    </td>
                                    <%--<td style="font-size: 12px;" align="right">
                                    </td>--%>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" style="font-size: 12px">
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" style="font-size: 12px">
                    <b>
                        <asp:Label ID="lblRasRupeesWord" runat="server" Text=""></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" style="font-size: 12px">
                </td>
            </tr>
            <tr>
            <td style="64%">
                    <table border="1" cellspacing="0" cellpadding="0"  style="font-size: 12px; font-family: Arial,Helvetica,sans-serif;" width="100%">
                        <tr style=" ">
                            <td style="font-size: 12px;width:23%; height: 30px;">
                                Service Tax
                            </td>
                            <td style="font-size: 12px; text-align: right; width:15%; height: 30px;">
                                 <b><asp:Label ID="lblRasDivServTaxTotal" runat="server" Text=""></asp:Label></b>
                            </td>
                            <td style="font-size: 12px; text-align: center;width:43%; height: 30px;" rowspan="4">
                                <b>"We Hereby certify the CEVAT credit on inputs, capital goods and input services,used
                                    for providing the taxable service of transportation has not been taken by the service
                                    provider under the provision of the CENVAT credit Rules 2004."</b>
                            </td>
                        </tr>
                        <tr style="border-color:Black;border-style:solid; border-width:1px">
                            <td style="font-size: 12px; height: 30px;">
                                Swacha Bharat Cess
                            </td>
                            <td style="font-size: 12px; text-align: right; height: 30px;">
                                 <b><asp:Label ID="lblRasDivSwachBhTotal" runat="server" Text=""></asp:Label></b>
                            </td>
                        </tr>
                        <tr style="border-color:Black;border-style:solid; border-width:1px">
                            <td style="font-size: 12px; height: 30px;">
                                Krisi Kalyan Cess
                            </td>
                            <td style="font-size: 12px; text-align: right; height: 30px;">
                                 <b><asp:Label ID="lblRasDivKissanTaxTotal" runat="server" Text=""></asp:Label></b>
                            </td>
                        </tr>
                        <tr style="border-color:Black;border-style:solid; border-width:1px">
                            <td style="font-size: 12px; height: 30px;">
                                Sec&Higher Edu.Cess
                            </td>
                            <td style="font-size: 12px; text-align: right; height: 30px;">
                                <b><asp:Label ID="lblRasDivEduTaxTotal" runat="server" Text="0.00"></asp:Label></b>
                            </td>
                        </tr>
                    </table>
                </td>
                
                <td style="width:20%">
                    <table width="100%">
                        <tr>
                            <td style="font-size: 12px;height:50px;text-align: right;">
                            <b>For <asp:Label ID="lblRasSignCompName" runat="server" Text=""></asp:Label></b>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12px; text-align: right;height:15px"></td>
                        </tr>
                         <tr>
                            <td style="font-size: 12px; text-align: right;height:15px"></td>
                        </tr>
                         <tr>
                            <td style="font-size: 12px; text-align: right;height:15px"></td>
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

    <div id="printShreeCementLtdJobner" visible="false" runat="server">
        <table cellpadding="1" cellspacing="0" width="82%" border="0" style="font-family: Arial,Helvetica,sans-serif;
            border-width: 1px; border-color: #000000;">
            <%--MAINTAIN WIDTH--%>
            <tr><td style="width:15%" align="left"><table style="height:1px;" width="700px"></table></td><td style="padding-left:130px;"><table style=" height:1px;" width="232px"></table></td></tr>
            <%--/MAINTAIN WIDTH--%>
            
            <tr id="Header2" runat="server">
                <td align="left" style="width:15%">
                    <table width="700px" style="font-family: Arial,Helvetica,sans-serif; border: 3px solid black;
                        height: 98px;">
                        <tr>
                            <td style="font-size: 12px;">
                                <asp:Label ID="lblJobnerJurCity" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="font-size: 12px; text-align: right;">
                                <asp:Label ID="lblJobnerOwnerPhoneNo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center;">
                                <u><b>
                                    <asp:Label ID="lblJobnerCompName" runat="server" Text=""></asp:Label></b></u> &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center;">
                                <asp:Label ID="lblJobnerCompAddress1" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center; font-size: 12px">
                                <asp:Label ID="lblJobnerCompAddress2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="padding-left:130px;" >
                    <table width="232px" style="font-family: Arial,Helvetica,sans-serif; border: 3px solid black;
                        height: 98px;">
                        <tr>
                            <td style="text-align: left; font-size: 12px">
                                <asp:Label ID="lblJobnerCode1" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; font-size: 12px">
                                Quantity and freight rate varified.
                            </td>
                        </tr>
                        <tr style="text-align: left; font-size: 12px">
                            <td>
                                Bill passed for Rs...............
                            </td>
                        </tr>
                        <tr style="text-align: left; font-size: 12px">
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr style="text-align: right; font-size: 12px">
                            <td>
                                <b>Authorised Signature</b>
                            </td>
                        </tr>
                    </table>
</td>
            </tr>
            <tr>
                <td colspan="2" style="font-size: 12px; height:30px; text-align: center">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="font-size: 12px; height:30px; text-align: center">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="font-size: 12px; text-align: center">
                    <b><u>TRANSPORTATION FREIGHT BILL (Unloading) : CEMENT FWD BNG</u></b>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="100%">
                        <tr>
                            <td style="width:80%">
                                <table style="font-size: 12px;">
                                    <tr>
                                        <td>
                                            Code:  0
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            To: &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblJobnerPartyName" runat="server" Text=""></asp:Label><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblJobnerPartyAddress1" runat="server" Text=""></asp:Label>
                                            <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblJobnerPartyAddress2" runat="server" Text=""></asp:Label>
                                              <asp:Label ID="lblJobnerPartyPinCode" runat="server" Text=""></asp:Label><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblJobnerPartyPanNo" runat="server" Text=""></asp:Label><br />
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
                            <td>
                                <table width="100%" style="font-size: 12px;">
                                    <tr>
                                        <td>
                                            <b><asp:Label ID="lblJobnerBillNo" runat="server" Text=""></asp:Label></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b><asp:Label ID="lblJobnerBillDate" runat="server" Text=""></asp:Label></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblJobnerCompPanNo" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblJobnerServTaxNo" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table border="1" cellpadding="0" cellspacing="0" style="font-size: 12px;" width="100%" id="Table3">
                        <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater3_ItemDataBound">
                            <HeaderTemplate>
                                <tr>
                                    <td class="white_bg" style="font-size: 12px;" width="3%">
                                        <strong>S.No.</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="5%" align="center">
                                        <strong>GRNo</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="10%" align="center">
                                        <strong>EGP No</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="10%" align="center">
                                        <strong>DINo</strong>
                                    </td>
                                    <td style="font-size: 12px;" align="center" width="7%">
                                        <strong>Date</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="10%" align="center">
                                        <strong>Truck No</strong>
                                    </td>
                                    <td style="font-size: 12px;" align="center" width="40%">
                                        <strong>Consignee</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="7%" align="center">
                                        <strong>Destination</strong>
                                    </td>
                                    <%--<td style="font-size: 12px;" width="5%" align="center">
                                        <strong>Ch.No.</strong>
                                    </td>--%>
                                  <%--  <td style="font-size: 12px;" width="7%" align="center">
                                        <strong>Grade</strong>
                                    </td>--%>
                                    <%--<td style="font-size: 12px;" width="8%" align="right">
                                        <strong>DspQty</strong>
                                    </td>--%>
                                    <td style="font-size: 12px;" width="8%" align="right">
                                        <strong>Weight(MT)</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="8%" align="right">
                                        <strong>Rate/MT</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="8%" align="right">
                                        <strong>Freight</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="8%" align="right">
                                        <strong><asp:Label ID="lblJobnerHeadWages" runat="server"></asp:Label></strong>
                                    </td>
                                    <%--<td style="font-size: 12px;" width="8%" align="right">
                                        <strong>Labour</strong>
                                    </td>--%>
                                    <td style="font-size: 12px;" width="8%" align="right">
                                        <strong>TotalFrt</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="8%" align="right">
                                        <strong>S.Tax</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="8%" align="right">
                                        <strong>SBCess</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="8%" align="right">
                                        <strong>KKCess</strong>
                                    </td>
                                    <td style="font-size: 12px;" width="8%" align="right">
                                        <strong>ShBags</strong>
                                    </td>
                                    <%--<td style="font-size: 12px;" width="6%" align="right">
                                        <strong>Wht</strong>
                                    </td>--%>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="white_bg" style="font-size: 12px;">
                                        <%#Container.ItemIndex+1 %>.
                                    </td>
                                    <td style="font-size: 12px;" align="center">
                                        <%#Eval("GR_No")%>
                                    </td>
                                    <td style="font-size: 12px;" align="center">
                                        <%#Eval("EGP_NO")%>
                                    </td>
                                    <td style="font-size: 12px;" align="center">
                                        <%#Eval("DI_NO")%>
                                    </td>
                                     <td style="font-size: 12px;" align="center">
                                        <%#Convert.ToDateTime(Eval("Chln_Date")).ToString("dd-MM-yyyy")%>
                                    </td>
                                    <td style="font-size: 12px;" align="center">
                                        <%#Eval("LORRY_NO")%>
                                    </td>
                                    <td style="font-size: 12px;" align="center">
                                        <%#Eval("Consignee")%>
                                    </td>
                                    <%--<td style="font-size: 12px;" align="center">
                                        <%#Eval("Chln_No")%>
                                    </td>--%>
                                    <td style="font-size: 12px;" align="center">
                                        <%#Eval("Delvry_Place")%>
                                    </td>
                                   <%-- <td style="font-size: 12px;" align="center">
                                        <%#Eval("Grade")%>
                                    </td>--%>
                                    <%--<td style="font-size: 12px;" align="right">
                                        <%#Eval("Dsp_Qty")%>
                                    </td>--%>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("Tot_Weght")%>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("Rate")%>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("Freight")%>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("Wages_Amnt")%>
                                    </td>
                                    <%--<td style="font-size: 12px;" align="right">
                                        <%#Eval("Labour")%>
                                    </td>--%>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("TotalFrt")%>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("ServTax_Amnt")%>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("SwchBrtTax_Amt")%>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("KisanKalyan_Amnt")%>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <%#Eval("Shortage_Qty")%>
                                    </td>
                                   <%-- <td style="font-size: 12px;" align="right">
                                        <%#Eval("")%>
                                    </td>--%>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td style="font-size: 12px;">
                                        
                                    </td>
                                    <td style="font-size: 12px;" align="center">
                                    </td>
                                    <td style="font-size: 12px;" align="center">
                                    </td>
                                    <td style="font-size: 12px;">
                                    </td>
                                    <td style="font-size: 12px;">
                                    </td>
                                    <td style="font-size: 12px;" align="left">
                                    </td>
                                    <td style="font-size: 12px;" align="left">
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <b>Total:</b>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                       <strong>
                                            <asp:Label ID="lblJobnerTotalWeight" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                           
                                    </td>
                                    
                                    <%--<td style="font-size: 12px;" align="right">
                                         <strong>
                                            <asp:Label ID="lblJobnerTotalQty" runat="server"></asp:Label></strong>
                                    </td>--%>
                                    <%--<td style="font-size: 12px;" align="right">
                                    </td>--%>
                                    <td style="font-size: 12px;" align="right">
                                     <strong>
                                            <asp:Label ID="lblJobnerTotalFreight" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 12px;"  align="right">
                                       <strong>
                                            <asp:Label ID="lblJobnerUnloading" runat="server"></asp:Label></strong>
                                    </td>
                                    <%--<td style="font-size: 12px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblJobnerTotalLabour" runat="server"></asp:Label></strong>
                                    </td>--%>
                                    <td style="font-size: 12px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblJobnerTotalFrt" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblJobnerTotalSTaxAmnt" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblJobnerTotalSBTaxAmnt" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblJobnerTotalKKAmnt" runat="server"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 12px;" align="right">
                                        <strong>
                                            <asp:Label ID="lblJobnerTotalShBags" runat="server"></asp:Label></strong>
                                    </td>
                                    <%--<td style="font-size: 12px;" align="right">
                                    </td>--%>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" style="font-size: 12px">
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" style="font-size: 12px">
                    <b>
                        <asp:Label ID="lblJobnerRupeesWord" runat="server" Text=""></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" style="font-size: 12px">
                </td>
            </tr>
            <tr>
            <td style="64%">
                    <table border="1" cellspacing="0" cellpadding="0"  style="font-size: 12px; font-family: Arial,Helvetica,sans-serif;" width="100%">
                        <tr style=" ">
                            <td style="font-size: 12px;width:23%; height: 30px;">
                                Service Tax
                            </td>
                            <td style="font-size: 12px; text-align: right; width:34%; height: 30px;">
                                 <b><asp:Label ID="lblJobnerDivServTaxTotal" runat="server" Text=""></asp:Label></b>
                            </td>
                            <td style="font-size: 12px; text-align: center;width:43%; height: 30px;" rowspan="4">
                                <b>"We Hereby certify the CEVAT credit on inputs, capital goods and input services,used
                                    for providing the taxable service of transportation has not been taken by the service
                                    provider under the provision of the CENVAT credit Rules 2004."</b>
                            </td>
                        </tr>
                        <tr style="border-color:Black;border-style:solid; border-width:1px">
                            <td style="font-size: 12px; height: 30px;">
                                Swacha Bharat Cess
                            </td>
                            <td style="font-size: 12px; text-align: right; height: 30px;">
                                 <b><asp:Label ID="lblJobnerDivSwachBhTotal" runat="server" Text=""></asp:Label></b>
                            </td>
                        </tr>
                        <tr style="border-color:Black;border-style:solid; border-width:1px">
                            <td style="font-size: 12px; height: 30px;">
                                Krisi Kalyan Cess
                            </td>
                            <td style="font-size: 12px; text-align: right; height: 30px;">
                                 <b><asp:Label ID="lblJobnerDivKissanTaxTotal" runat="server" Text=""></asp:Label></b>
                            </td>
                        </tr>
                        <%--<tr style="border-color:Black;border-style:solid; border-width:1px">
                            <td style="font-size: 12px; height: 30px;">
                                Sec&Higher Edu.Cess
                            </td>
                            <td style="font-size: 12px; text-align: right; height: 30px;">
                                <b><asp:Label ID="lblJobnerDivEduTaxTotal" runat="server" Text="0.00"></asp:Label></b>
                            </td>
                        </tr>--%>
                    </table>
                </td>
                
                <td style="width:20%">
                    <table width="100%">
                        <tr>
                            <td style="font-size: 12px;height:50px;text-align: right;">
                                <b>For <asp:Label ID="lblJobnerSignCompName" runat="server" Text=""></asp:Label></b>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12px; text-align: right;height:15px"></td>
                        </tr>
                         <tr>
                            <td style="font-size: 12px; text-align: right;height:15px"></td>
                        </tr>
                         <tr>
                            <td style="font-size: 12px; text-align: right;height:15px"></td>
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
