<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PJLBillInvoice.aspx.cs" Inherits="WebTransport.PJLBillInvoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html><head>
    <title>Balance-Sheet</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
</head>
<body>
 <div id="print" runat="server" >
    <table width="100%" cellpadding="5" cellspacing="0" align="center" style="border-collapse: collapse;">
        <tbody>
            <tr>
                <th width="50%" colspan="14" align="center" style="font-size: 12px; font-family: arial; border: 1px solid;">
                    <b>
                        <span style="font-size: 20px;">
                           <asp:Label ID="lblCompname" runat="server"></asp:Label>
                        </span>
                    </b>
                </th>
            </tr>
            <tr>
                <td colspan="14" align="center" valign="top" style="font-size: 12px; font-family: arial; border: 1px solid;">
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
                        </b>
                    </span>
                </td>
            </tr>
            <tr>
                <td width="50%" colspan="7" valign="top" align="center" style="font-size: 12px; font-family: arial; text-align: left; border: 1px solid;">
                    <b>
                        GSTIN :- <asp:Label ID="lbgst" runat="server"></asp:Label>
                    </b>
                </td>
                <td width="50%" colspan="7" style="font-size: 12px; font-family: arial; text-align: right; border: 1px solid;">
                    <b>
                        SAC Code:- 996511
                    </b>
                </td>
            </tr>
            <tr>
            <th width="50%" colspan="14" align="center" style="font-size: 20px; font-family: arial; border: 1px solid;">
            BILL DETAILS
            </th>
            </tr>
            <tr>
                <td width="50%" colspan="7" valign="top" align="center" style="font-size: 12px; font-family: arial; text-align: left; border: 1px solid;">
                    Name &amp; Address of Receiver
                </td>
              
            </tr>
            <tr>
                <td colspan="7" align="left" valign="top" style="font-size: 12px; font-family: arial; border: 1px solid;">
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
                <td colspan="7" valign="top" style="font-size: 12px; font-family: arial; text-align: left; border: 1px solid;">
                  <span>BILL NO.-</span>
                    <span style="float:right;">
                       <asp:Label ID="lblbillno" runat="server"></asp:Label>
                    </span>
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
            </tbody>
            </table>
    <table width="100%" cellpadding="7" cellspacing="0" align="center" style="font-family: arial; font-size: 12px; border-collapse: collapse;">
        <thead>
           <tr>
            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
              <HeaderTemplate>
                <th style="font-size:12px;font-family:arial;border:1px solid;">L.R.No.</th> 
                <th style="font-size:12px;font-family:arial;border:1px solid;">L.R.Date</th>    
                <th style="font-size:12px;font-family:arial;border:1px solid;">Order No.</th>  
                <th style="font-size:12px;font-family:arial;border:1px solid;">INVOICE</th> 
                <th style="font-size:12px;font-family:arial;border:1px solid;">INVOICE  DATE</th>    
                <th style="font-size:12px;font-family:arial;border:1px solid;">DI. NO.</th> 
                <th style="font-size:12px;font-family:arial;border:1px solid;">Truck No.</th>      
                <th style="font-size:12px;font-family:arial;border:1px solid;">Destination</th> 
                <th style="font-size:12px;font-family:arial;border:1px solid;">Weight</th>  
                <th style="font-size:12px;font-family:arial;border:1px solid;">Rate</th> 
                <th style="font-size:12px;font-family:arial;border:1px solid;">Amount</th>     
                <th style="font-size:12px;font-family:arial;border:1px solid;">TOLL PLAZA</th>    
                <th style="font-size:12px;font-family:arial;border:1px solid;">U/L</th> 
                <th style="font-size:12px;font-family:arial;border:1px solid;">SHORTEG</th>    
             </HeaderTemplate>   
       <ItemTemplate>
            <tbody>
            <tr>
                <td style="font-size:12px;font-family:arial;border:1px solid;"><%#Eval("Gr_No")%></td>
                <td style="font-size:12px;font-family:arial;border:1px solid;"><%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MM-yyyy")%></td>
                <td style="font-size:12px;font-family:arial;border:1px solid;"><%#Eval("Ordr_No")%></td>
                <td style="font-size:12px;font-family:arial;border:1px solid;"><%#Eval("Inv_No")%></td>
                <td style="font-size:12px;font-family:arial;border:1px solid;"><%#Convert.ToDateTime(Eval("Inv_Date")).ToString("dd-MM-yyyy")%></td>
                <td style="font-size:12px;font-family:arial;border:1px solid;"><%#Eval("DI_NO")%></td>
                <td style="font-size:12px;font-family:arial;border:1px solid;"><%#Eval("LORRY_NO")%></td>
                <td style="font-size:12px;font-family:arial;border:1px solid;"><%#Eval("Delvry_Place")%></td>
                <td style="font-size:12px;font-family:arial;border:1px solid;text-align:right;"><%#Eval("Weight")%></td>
                <td style="font-size:12px;font-family:arial;border:1px solid;text-align:right;"><%#Eval("Rate")%></td>
                <td style="font-size:12px;font-family:arial;border:1px solid;text-align:right;"><%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%></td>
                <td style="font-size:12px;font-family:arial;border:1px solid;text-align:right;"><%#(Eval("TollTax_Amnt"))%></td>
                <td style="font-size:12px;font-family:arial;border:1px solid;text-align:right;"><%#Eval("UL")%></td>
                <td style="font-size:12px;font-family:arial;border:1px solid;text-align:right;"><%#Eval("SHORT_MT")%></td>
            </tr>
            </tbody>
            </ItemTemplate>
          <FooterTemplate>
         </FooterTemplate>
     </asp:Repeater>
 </tr>
 </thead>

            <tr style="border: 1px solid #000;">
                <td colspan="8" style="font-size: 12px; font-family: arial; text-align: right; border: 1px solid;"><b>TOTAL </b></td>
                <td style="border: 1px solid; font-size: 12px; font-family: arial; text-align: right;"><asp:Label ID="lbltweight" runat="server"></asp:Label></td>
                <td style="border: 1px solid; font-size: 12px; font-family: arial; text-align: right;"><asp:Label ID="lbltrate" runat="server"></asp:Label></td>
                <td style="border: 1px solid; font-size: 12px; font-family: arial; text-align: right;"><asp:Label ID="lbltamount" runat="server"></asp:Label></td>
                <td style="border: 1px solid; font-size: 12px; font-family: arial; text-align: right;"><asp:Label ID="lblttoll" runat="server"></asp:Label></td>
                <td style="border: 1px solid; font-size: 12px; font-family: arial; text-align: right;"><asp:Label ID="lbltunloading" runat="server"></asp:Label></td>
                <td style="border: 1px solid; font-size: 12px; font-family: arial; text-align: right;"><asp:Label ID="lbltshorteg" runat="server"></asp:Label></td>
            </tr>
             <tr style="border: 1px solid #000;">
                <td colspan="14" style="font-size: 12px; font-family: arial; text-align: left; border: 1px solid;">
                    Amount In Words-: <asp:Label ID="lblword" runat="server"></asp:Label>
                </td>
               
            </tr>
          <tr style="border: 1px solid #000;">
                <td width="50%" colspan="7" style="font-size: 12px; font-family: arial; text-align: left; border: 1px solid;">
                    Note: -
                    <br>
                    Certified that the Particulars given above are true &amp; Correct.
                    <br>
                </td>
                <td colspan="7" style="font-size: 12px; font-family: arial; text-align: right; border: 1px solid;">
                    For M/S:
                    <br>
                    <b>
                        <asp:Label ID="lblcom" runat="server"></asp:Label>
                    </b>
                    <br>
                  Seal & Sign
                    <br>
                    <br>
                </td>
              
            </tr>
</table>
    </div>
</body></html>