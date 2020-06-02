<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PJLTaxInvoice.aspx.cs" Inherits="WebTransport.PJLTaxInvoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html><head>
    <title>TAX INVOICE</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
</head>
<body>
 <div id="print" runat="server" >
    <table width="100%" cellpadding="5" cellspacing="0" align="center" style="border-collapse: collapse;">
        <tbody>
            <tr>
                <th width="50%" colspan="6" align="center" style="font-size: 12px; font-family: arial; border: 1px solid;">
                    <b>
                        <span style="font-size: 12px;">
                            INVOICE FOR TRANSPORTATION OF GOODS
                        </span>
                    </b>
                </th>
            </tr>
            <tr>
                <th width="50%" colspan="6" align="center" style="font-size: 12px; font-family: arial; border: 1px solid;">
                    <b>
                        <span style="font-size: 20px;">
                           <asp:Label ID="lblCompname" runat="server"></asp:Label>
                        </span>
                    </b>
                </th>
            </tr>
            <tr>
                <td colspan="6" align="center" valign="top" style="font-size: 12px; font-family: arial; border: 1px solid;">
                    <span style="font-size: 12px;">

                        <b>
                            <span>
                                Head office :- <asp:Label ID="lblCompAdd1" runat="server"></asp:Label>, <asp:Label ID="lblcity" runat="server"></asp:Label> (<asp:Label ID="lblpin" runat="server"></asp:Label>)
                            </span>
                            <br>
                            <span>
                                STATE :- <asp:Label ID="lblstate" runat="server"></asp:Label> 
                            </span>
                            <br>
                            <span>Mob :- <asp:Label ID="lblmobile" runat="server"></asp:Label></span>
                            <br>
                            <span>
                                PAN  NO :- <asp:Label ID="lblpan" runat="server"></asp:Label>
                            </span>
                            <br>
                            <span>
                                GSTIN :- <asp:Label ID="lblgstin" runat="server"></asp:Label>
                            </span>
                        </b>
                    </span>
                </td>
            </tr>
            <tr>
                <td width="50%" colspan="3" valign="top" align="center" style="font-size: 12px; font-family: arial; text-align: left; border: 1px solid;">
                    <b>
                        GSTIN :- <asp:Label ID="lbgst" runat="server"></asp:Label>
                    </b>
                </td>
                <td width="50%" colspan="3" style="font-size: 12px; font-family: arial; text-align: right; border: 1px solid;">
                    <b>
                        SAC Code:- 996511
                    </b>
                </td>
            </tr>
            <tr>
                <td width="50%" colspan="3" valign="top" align="center" style="font-size: 12px; font-family: arial; text-align: left; border: 1px solid;">
                    Name &amp; Address of Receiver
                </td>
                <td width="50%" colspan="3" style="font-size: 12px; font-family: arial; text-align: right; border: 1px solid;">
                   TOLL CHARGES BILL
                </td>
            </tr>
            <tr>
                <td colspan="3" align="left" valign="top" style="font-size: 12px; font-family: arial; border: 1px solid;">
                    <b><asp:Label ID="lblcontname" runat="server"></asp:Label></b>
                    <br>
                   <asp:Label ID="lbladd1" runat="server"></asp:Label>
                    <br>
                   <asp:Label ID="lblcadd2" runat="server"></asp:Label>
                    <br>
                    Distt-<asp:Label ID="lbldis" runat="server"></asp:Label> -  <asp:Label ID="diccode" runat="server"></asp:Label>
                    <br>
                    State-<asp:Label ID="lblst" runat="server"></asp:Label>
                    <br>
                    <b>GSTIN :-<asp:Label ID="lblgst" runat="server"></asp:Label></b>
                </td>
                <td colspan="3" valign="top" style="font-size: 12px; font-family: arial; text-align: left; border: 1px solid;">
                    <span>BILL NO.-</span>
                    <span style="float:right;">
                       <asp:Label ID="lblbillno" runat="server"></asp:Label>
                    </span>
                    <br>
                    <span>
                        BILL DATE.-
                    </span>
                    <span style="float:right;"><asp:Label ID="lblbilldate" runat="server"></asp:Label></span>
                    <br>
                    <span>
                        Tax Payable under RCM- :
                    </span>
                    <span style="float:right;">NO</span>
                    <br>
                    <span>
                        Mode of Transportation- :
                    </span>
                    <span style="float:right;">TRUCK</span>
                    <br>
                    <span>
                        Distribution Channel-:
                    </span>
                    <span style="float:right;">DEPOT</span>
                    <br>
                    <span>Unit- :</span>
                    <span style="float:right;"><asp:Label ID="lblunit" runat="server"></asp:Label></span>
                </td>
            </tr>
           <tr>
            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound" >
              <HeaderTemplate>
                <th colspan="3" align="left" style="font-size: 12px; font-family: arial; max-width: 10px; border: 1px solid;">
                    Description of Goods/Services                           
                </th>
                <th align="left" style="font-size: 12px; font-family: arial; border: 1px solid;">
                    HSN/code    
                </th>
                <th align="right" style="font-size: 12px; font-family: arial; border: 1px solid;">
                    QTY  
                </th>
                <th align="right" style="font-size: 12px; font-family: arial; border: 1px solid;">
                    Total Freight (Rs.)
                </th>
        </HeaderTemplate>   
       <ItemTemplate>
 
            <tr>
                <td colspan="3" align="left" style="font-size: 12px; font-family: arial; border: 1px solid;">
                   Cement Transportation By Road  
                </td>
                <td align="left" style="font-size: 12px; font-family: arial; border: 1px solid;">
                 996511
                </td>
                <td align="right" style="font-size: 12px; font-family: arial; border: 1px solid;">
                     <%#Eval("Qty")%>
                </td>
                <td align="right" style="font-size: 12px; font-family: arial; border: 1px solid;text-align:right;">
                      <%#Eval("AMOUNT")%>
                </td>
            </tr>
             </ItemTemplate>
              <FooterTemplate>
         </FooterTemplate>
     </asp:Repeater>
 </tr>
            <tr style="border: 1px solid #000;">
                <td colspan="5" style="font-size: 12px; font-family: arial; text-align: right; border: 1px solid;"><b>TOTAL TAXABLE VALUE  </b></td>
                <td style="border: 1px solid; font-size: 12px; font-family: arial; text-align: right;"><asp:Label ID="lbltaxvalue" runat="server"></asp:Label></td>
            </tr>
          <tr style="border: 1px solid #000;">
                <td width="50%" colspan="3" style="font-size: 12px; font-family: arial; text-align: left; border: 1px solid;">
                    Declaration: -
                    <br>
                    Certified that the Particulars given above are true &amp; Correct.
                    <br>
                </td>
                <td id ="igst" runat="server" colspan="2" align="right" valign="bottom" style="font-size: 12px; font-family: arial; border: 1px solid;">
                    IGST     @12%
                </td>
                <td valign="bottom" style="font-size: 12px; font-family: arial; text-align: right; border: 1px solid;"><asp:Label ID="lblamt" runat="server"></asp:Label></td>
            </tr>
            <tr style="border: 1px solid #000;">
                <td colspan="4" style="font-size: 12px; font-family: arial; text-align: left; border: 1px solid;">
                    In Words-: <asp:Label ID="lblword" runat="server"></asp:Label>
                </td>
                <td align="right" valign="bottom" style="font-size: 12px; font-family: arial; border: 1px solid;">
                    TOTAL BILL AMOUNT
                </td>
                <td valign="bottom" style="font-size: 12px; font-family: arial; text-align: right; border: 1px solid;"><asp:Label ID="lbltotalamnt" runat="server"></asp:Label></td>
            </tr>
            <tr style="border: 1px solid #000;">
                <td colspan="6" style="font-size: 12px; font-family: arial; text-align: right; border: 1px solid;">
                    For M/S:
                    <br>
                    <b>
                        <asp:Label ID="lblcom" runat="server"></asp:Label>
                    </b>
                    <br>
                    Authorised Signatory
                    <br>
                    <br>
                </td>
            </tr>
        </tbody>
    </table>

    </div>
</body></html>