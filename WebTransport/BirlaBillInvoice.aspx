<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="BirlaBillInvoice.aspx.cs" Inherits="WebTransport.BirlaBillInvoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>Standard bill FORMATE</title>
</head>
<body>
 <div id="print" runat="server" >
    <table width="100%" cellpadding="5" cellspacing="0" align="center" style="font-family: arial; font-size: 12px; border-collapse: collapse;">
        <tbody>
            <tr>
                <td colspan="10" align="center" style="border: 1px solid;">INVOICE FOR TRANSPORTATION OF GOODS</td>
            </tr>
            <tr>
                <th colspan="10" align="center" style="border: 1px solid;font-size: 30px;"><asp:Label ID="lblCompname" runat="server"></asp:Label></th>
            </tr>
            <tr>
                <td align="center" colspan="10" style="font-size: 17px;border: 1px solid;border-bottom: none;"> Head office :-<asp:Label ID="lblCompAdd1" runat="server"></asp:Label>, <asp:Label ID="lblcity" runat="server"></asp:Label>(<asp:Label ID="lblpin" runat="server"></asp:Label>)<br>STATE:- <asp:Label ID="lblstate" runat="server"></asp:Label><br>MOBILE NO.:-<asp:Label ID="lblmobile" runat="server"></asp:Label></td>
            </tr>
            <%--<tr>
                <th align="center" colspan="10" style="font-size: 15px;border: 1px solid;border-top: none;">BRANCH OFFICE:- <asp:Label ID="lbladd2" runat="server"></asp:Label> Satna, (M.P.)</th>
            </tr>--%>
             <tr>
                <td colspan="6" style="border: 1px solid;">BRANCH OFFICE:- <asp:Label ID="lbladd2" runat="server"></asp:Label></td>
                <td colspan="4" style="border: 1px solid;text-align: right;">SAC CODE:- 996511<asp:Label ID="lblhsn" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="6" style="border: 1px solid;text-align: center;">PAN NO.:- <asp:Label ID="lblpan" runat="server"></asp:Label></td>
                <td colspan="4" width="40%" style="border: 1px solid;text-align: center;"> GSTIN:- <asp:Label ID="lblgstin" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="4" style="border: 1px solid;"><b><asp:Label ID="lblcontname" runat="server"></asp:Label></b></td>
                <td colspan="8" style="border: 1px solid;"><b>BILL NO:- <asp:Label ID="lblbillno" runat="server"></asp:Label></b></td>  
            </tr>
            <tr>
               <td colspan="4" style="border: 1px solid;"><asp:Label ID="lbladd1" runat="server"></asp:Label></td>
                <td colspan="4" width="40%" style="border: 1px solid;"><b>BILL DATE:- <asp:Label ID="lblbilldate" runat="server"></asp:Label></b></td>
            </tr>
            <tr>
                <td colspan="4" style="border: 1px solid;"><asp:Label ID="lblcadd2" runat="server"></asp:Label></td>
                <td colspan="4" width="40%" style="border: 1px solid;"><b>UNIT-<asp:Label ID="lblunit" runat="server"></asp:Label></b></td>
            </tr>
            <tr>
                <td colspan="4" style="border: 1px solid;"><asp:Label ID="lblcity1" runat="server"></asp:Label>, <asp:Label ID="lblst" runat="server"></asp:Label> </td>
            </tr>
            <tr>
                <td colspan="4" style="border: 1px solid;">GSTIN :- <asp:Label ID="lblgst" runat="server"></asp:Label></td>
            </tr>
        </tbody>
    </table>
    <table width="100%" cellpadding="5" cellspacing="0" align="center" style="font-family: arial; font-size: 12px; border-collapse: collapse;">
        <thead>
            <tr>
                <td align="center" colspan="11" style="border: 1px solid;">BILL DETAILS</td>
            </tr>
            <tr>
           <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound" >
           <HeaderTemplate>
                <th width="5%" align="center" style="border: 1px solid;">SR. NO.</th>
                <th style="border: 1px solid;">LR NO.</th>
                <th style="border: 1px solid;">LR DATE</th>
                <th style="border: 1px solid;">TRUCK NO.</th>
                <th style="border: 1px solid;">D.I. NO.</th>
                <th style="border: 1px solid;">DESTINATION</th>
                <th style="border: 1px solid;">QTY MT</th>
                <th style="border: 1px solid;">RATE</th>
                <th style="border: 1px solid;">FREIGHT</th>
              </HeaderTemplate>
                 
     <ItemTemplate>
        <tbody>
         <tr>
            <td style="border: 1px solid;">
                 <%#Container.ItemIndex+1 %>
            </td>
            <td style="border: 1px solid;">
                 <%#Eval("GR_No")%>
            </td>
            <td style="border: 1px solid;">
              <%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MM-yyyy")%> 
            </td>
             <td style="border: 1px solid;">
                <%#Eval("Lorry_No")%>
            </td>
            <td style="border: 1px solid;">
                <%#Eval("DI_NO")%>
            </td>
             
            <td style="border: 1px solid;">
                <%#Eval("Delvry_Place")%>
            </td>
            <td style="border: 1px solid;" align="right">
                <%#Eval("Qty")%>
            </td>
            <td style="border: 1px solid;" align="right">
               <%#Eval("Rate")%>
            </td>
            <td style="border: 1px solid;" align="right">
          <%#Eval("Amount")%>  
            </td>
        </tr>
        </tbody>
            </ItemTemplate>
      <FooterTemplate>
      </FooterTemplate>
  </asp:Repeater>
   </tr>
 </thead>

            <tr>
                <th colspan="6" align="right" style="border: 1px solid;">Total:-</th>
                <th align="right" style="border: 1px solid;"> <asp:Label ID="lbltotqty" runat="server"></asp:Label></th>
                <th style="border: 1px solid;">&nbsp;</th>
                <th align="right" style="border: 1px solid;"> <asp:Label ID="lbltotal" runat="server"></asp:Label></th>
            </tr>
             
    </table>
    
     <table width="100%" cellpadding="5" cellspacing="0" align="center" style="font-family: arial; font-size: 12px; border-collapse: collapse;">
        <thead>  
             <tr>
                <td align="center" colspan="11" style="border: 1px solid;">TOLL DETAILS</td>
            </tr>
            <tr>
           <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound" >
           <HeaderTemplate>
                <th  colspan="3"  align="center" style="border: 1px solid;">Toll Amnt.</th>
                <th  colspan="3" style="border: 1px solid;">Toll Name</th>
                <th  colspan="3"  style="border: 1px solid;">TICKET NO.</th>
                  </HeaderTemplate>   
                   <ItemTemplate>
        <tbody>
              
            <tr>
     
              <td  colspan="3"  style="border: 1px solid;text-align:right;">
                  <%#Eval("Toll_Amt")%>
            </td>
            <td  colspan="3"  style="border: 1px solid;">
                 <%#Eval("Tolltax_name")%>
            </td>
             <td   colspan="3" style="border: 1px solid;text-align:right;">
                 <%#Eval("Ticket_No")%>
            </td>
         
            </tr>
           
            </tbody>     
                </ItemTemplate>
                 <FooterTemplate>
            </FooterTemplate>
             </asp:Repeater> 
              
            </tr>
           
       </thead>
            <tr>
                <th colspan="6" align="right" style="border: 1px solid;">Total:-</th>
                <th style="border: 1px solid;"> <asp:Label ID="lbltot" runat="server"></asp:Label></th> 
                </tr>
                <tr>
                <th colspan="6" align="right" style="border: 1px solid;">Grand Total:-</th>
                <th style="border: 1px solid;"> <asp:Label ID="lblgrandtotal" runat="server"></asp:Label></th> 
             </tr>
    </table>
    </div>
</body>
</html>