<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintSaleInvoice.aspx.cs" Inherits="WebTransport.PrintSaleInvoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Untitled Document</title>
    <style>
        * {
            font-family: Arial;
            white-space: nowrap;
        }
        td {
            height: 35px;
            padding: 5px 2px;
        }
    </style>
</head>

<body>
    <div id="print" runat="server" style="font-size: 13px; display: block;">
        <table width="100%" border="1" cellpadding="0" cellspacing="0" n="n" style="text-align: left; margin: 0 auto;">
            <tr>
                <td colspan="6" style="font-size: 30px; text-align: center;">
                    <span style="font-weight: bold;">
                        <u>
                            <asp:Label ID="lblCompName" runat="server" Style="font-family: 'Times New Roman' !important;"></asp:Label>
                        </u>
                    </span>
                    <br />
                    <span style="font-size: 15px;">
                        <asp:Label ID="lbltr" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lbladdr" runat="server"></asp:Label>
                        <br />
                        Email:
                    <asp:Label ID="lblemail" runat="server"></asp:Label>
                    </span>
                </td>
                <td colspan="4" style="text-align: center;"><strong><span>
                    <asp:Label ID="lblcomp" runat="server"></asp:Label><br />
                    <asp:Label ID="lbladd" runat="server"></asp:Label></span></strong></td>
                <td colspan="3" style="text-align: center;">
                    <strong>
                        <span>Cement Bill No:
                            <strong>
                                <asp:Label ID="lblbillno" runat="server"></asp:Label></strong>
                            <br />
                            Date:
                            <asp:Label ID="lbldate" runat="server"></asp:Label>
                        </span>
                    </strong>
                </td>
            </tr>
            <tr>
                <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                    <HeaderTemplate>
                        <td style="font-size: 13px; font-weight: bold; width: 20px;"><span>Sr.<br />
                            No.</span></td>
                        <td style="font-size: 13px; font-weight: bold; width: 40px;"><span>G.C.<br />
                            No.</span></td>
                        <td style="font-size: 13px; font-weight: bold; width: 85px;"><span>Date</span></td>
                        <td style="font-size: 13px; font-weight: bold; width: 75px;"><span>Challan<br />
                            No.</span></td>
                        <td style="font-size: 13px; font-weight: bold; width: 75px;"><span>Invoice No.</span></td>
                        <td style="font-size: 13px; font-weight: bold; width: 50px;">Truck
                            <br />
                            No.</td>
                        <td style="font-size: 13px; font-weight: bold; width: 75px;">Destination</td>
                        <td style="font-size: 13px; font-weight: bold; width: 75px;">Taluka</td>
                        <td style="font-size: 13px; font-weight: bold; width: 35px;">Rate<br />
                            (INR)</td>
                        <td style="font-size: 13px; font-weight: bold; width: 35px;">Weight</td>
                        <td style="font-size: 13px; font-weight: bold; width: 35px;">Amount</td>
                        <td style="font-size: 13px; font-weight: bold; width: 35px;">Recived<br />
                            Dated</td>
                        <td style="font-size: 13px; font-weight: bold; width: 35px;">Remarks</td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Container.ItemIndex+1 %>
                            </td>
                            <td>
                                <%#Eval("GR_No")%>
                            </td>
                            <td>
                                <%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MM-yyyy")%> 
                            </td>
                            <td>
                                <%#Eval("Chln_No")%>
                            </td>
                            <td>
                                <%#Eval("Tax_InvNo")%>
                            </td>
                            <td>
                                <%#Eval("Lorry_No")%>
                            </td>
                            <td>
                                <%#Eval("Destination")%>
                            </td>
                            <td>
                                <%#Eval("Destination")%>
                            </td>
                            <td>
                                <%#Eval("Item_Rate")%>
                            </td>
                            <td>
                                <%#Eval("Tot_Weght")%>
                            </td>
                            <td>
                                <%#Eval("Amount")%> 
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>
            </tr>
            <tr>
                <td colspan="8" style="text-align: right;">
                    <asp:Label ID="lblword" runat="server"></asp:Label>
                </td>
                <td colspan="2" style="text-align: right">Total Amount(INR)
                </td>
                <td colspan="3" style="padding-bottom: 25px;">
                    <asp:Label ID="lbltotal" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <tr>
                    <th colspan="10" style="border: none; text-align: right;"></th>
                    <th rowspan="1" style="border: 1px medium; text-align: left;"><b>BILL AMOUNT IN RS.</b></th>
                    <th align="right" style="border: 1px medium;"><b>
                        <asp:Label ID="lblbillamnt" runat="server"></asp:Label></b></th>
                </tr>
                <tr>
                    <th colspan="10" style="border: none; text-align: right;"></th>
                    <th align="left" id="thsgstper" runat="server">
                        <asp:Label ID="lblsgstper" Font-Bold="true" runat="server"></asp:Label></th>
                    <th align="right" id="thsgstamnt" runat="server" style="border: 1px medium;">
                        <asp:Label ID="lblsgstamnt" runat="server"></asp:Label></th>
                </tr>
                <tr>
                    <th colspan="10" style="border: none; text-align: right;"></th>
                    <th align="left" id="thcgstper" runat="server">
                        <asp:Label ID="lblcgstper" Font-Bold="true" runat="server"></asp:Label></th>
                    <th align="right" id="thcgstamnt" runat="server" style="border: 1px medium;">
                        <asp:Label ID="lblcgstamnt" runat="server"></asp:Label></th>
                </tr>
                <tr>
                    <th colspan="10" style="border: none; text-align: right;"></th>
                    <th align="left" id="thigstper" runat="server">
                        <asp:Label ID="lbligstper" Font-Bold="true" runat="server"></asp:Label></th>
                    <th align="right" id="thigstamnt" runat="server" style="border: 1px medium;">
                        <asp:Label ID="lbligstamnt" runat="server"></asp:Label></th>
                </tr>
                <tr>
                    <th colspan="10" style="border: none; text-align: right;"></th>
                    <th rowspan="1" style="border: 1px medium; text-align: left;"><b>TOTAL RS.</b></th>
                    <th align="right" style="border: 1px medium;"><b>
                        <asp:Label ID="lblTotalAmount" runat="server"></asp:Label></b>
                    </th>
                </tr>
                <th colspan="12" style="padding: 10px 38px 0px 0px; height: 100px; border: none; text-align: right;">For:
                    <asp:Label ID="lblsign" runat="server"></asp:Label>
                </th>
            </tr>
        </table>
    </div>
</body>
</html>

