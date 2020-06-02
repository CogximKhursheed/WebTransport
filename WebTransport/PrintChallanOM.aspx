<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintChallanOM.aspx.cs"
    Inherits="WebTransport.PrintChallanOM" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="print" style="font-size: 13px; display: block;">
        <table cellpadding="1" cellspacing="0" width="100%" border="1" style="font-family: Arial,Helvetica,sans-serif;">
            <tr>
                <td class="white_bg" align="center">
              
                     <table cellpadding="1" cellspacing="0" width="100%" border="1" style="font-family: Arial,Helvetica,sans-serif;">
                        <tr>
                            <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                                border-left-style: none; border-right-style: none">
                                <strong>
                                    <asp:Label ID="lblCompanyname" runat="server" Style="font-size: 18px;"></asp:Label><br />
                                </strong>
                       
                                    <asp:Label ID="lblcompdesc" runat="server" Style="font-size: 18px;"></asp:Label>
                              
                                <asp:Label ID="lblCompAdd1" runat="server"></asp:Label>
                                &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblCompAdd2" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCompCity" runat="server"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblCompState" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblCompCityPin" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCompPhNo" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblFaxNo" Text="FAX No.:" runat="server"></asp:Label>
                                <asp:Label ID="lblCompFaxNo" runat="server"></asp:Label><br />
                                <asp:Label ID="lblTin" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                                    ID="lblCompTIN" runat="server"></asp:Label>
                            </td>
                        </tr>
                       
                        <tr>
                            <td align="center" class="white_bg" valign="top" colspan="4" style="border-left-style: none; border-right-style: none">
                            
                                    <strong>
                                        <asp:Label ID="lblPrintHeadng" runat="server" Font-Size="14px" Text="FREIGHT CUM TRANSIT CHALLAN"></asp:Label></strong>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="4">
                                <table border="0" width="100%">
                                    <tr>
                                        <td align="left"  class="white_bg" valign="top" style="font-size: 13px;border-right-style: none;">
                                            <asp:Label ID="lblchlntext" Text="FCTC No." runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td align="left" class="white_bg" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblChlnno" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lbltxtchlndate" Text="Date" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td align="left" class="white_bg" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblchlnDate" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; 
                                            border-right-style: none">
                                               <asp:Label ID="lbltxttruck" Text="Lorry No." runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </td>
                                         <td>
                                            :
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px;
                                            border-right-style: none">
                                              <b>
                                                <asp:Label ID="lblTrckNo" runat="server"></asp:Label></b>
                                        </td>
                                    </tr>
                                
                                   
                                </table>
                            </td>
                        </tr>
                        
                        <tr>
                                 <td>
                            <table width="100%">
                            <tr>
                            <td style="width:50%;" valign="top">
                            <table border="0" width="100%" class="white_bg" style="border-right:1px solid #484848;">
                            <tr>
                            <td colspan="2" style="border-bottom:1px solid #484848;"> 
                                <strong>&nbsp;Lorry Details:</strong>
                            </td> 
                            </tr>
                            <tr>
                            <td style="font-size: 13px;height:5px;border-right-style: none;width:50%;">
                                <asp:Label ID="Label1" Text="Lorry Owner " runat="server"></asp:Label>
                            </td>
                          <td style="font-size: 13px;height:5px;border-right-style: none">
                             <b> <asp:Label  ID="lblOwnr" runat="server"></asp:Label>  </b>
                            </td>
                            </tr>
                            <tr>
                            <td style="font-size: 13px;height:5px;border-right-style: none;width:50%;"> 
                             <asp:Label ID="Label12" Text="Address" runat="server"></asp:Label>
                            </td>
                            <td style="font-size: 13px;height:5px;border-right-style: none">
                            <b><asp:Label ID="lblowneraddrss" runat="server"></asp:Label></b>
                            </td>
                            </tr>
                             <tr> 
                                <td style="font-size: 13px; border-right-style: none;width:50%;" >
                                    <asp:Label ID="lblownmobile" runat="server" Text="Mobile"></asp:Label>
                                </td>
                                            
                                <td style="font-size: 13px; border-right-style: none" >
                                    <b>
                                        <asp:Label ID="lbltxtownmobile" Text="" runat="server"></asp:Label></b>
                                </td>
                            </tr>
                             <tr> 
                                <td style="font-size: 13px; border-right-style: none;width:50%;" >
                                    <asp:Label ID="lblpanno" runat="server" Text="Pan No."></asp:Label>
                                </td>
                                            
                                <td style="font-size: 13px; border-right-style: none" >
                                    <b>
                                        <asp:Label ID="lbltxtpan" Text="" runat="server"></asp:Label></b>
                                </td>
                            </tr>
                             <tr> 
                                <td style="font-size: 13px; border-right-style: none;width:50%;">
                                    <asp:Label ID="Label5" runat="server" Text="Chasis No."></asp:Label>
                                </td>
                                            
                                <td style="font-size: 13px; border-right-style: none;">
                                    <b>
                                        <asp:Label ID="lblchasisno" Text="" runat="server"></asp:Label></b>
                                               
                                </td>
                            </tr>
                              <tr> 
                                <td style="font-size: 13px; border-right-style: none;width:50%;">
                                <asp:Label ID="lblengineno" runat="server" Text="Engine No."></asp:Label>
                                </td>
                                            
                                <td style="font-size: 13px; border-right-style: none;">
                                    <b><asp:Label ID="lbltxtengineno" Text="" runat="server"></asp:Label></b>
                                </td>
                            </tr>
                             <tr> 
                                <td style="font-size: 13px; border-right-style: none;width:50%;">
                                    <asp:Label ID="lblpermitno" runat="server" Text="Permit No."></asp:Label>
                                </td>
                                            
                                <td style="font-size: 13px; border-right-style: none">
                                    <b>
                                        <asp:Label ID="lbltxtpermit" Text="" runat="server"></asp:Label></b>&nbsp;&nbsp;
                                           
                                         
                                </td>
                            </tr>
                             <tr> 
                                <td style="font-size: 13px; border-right-style: none;width:50%;">
                                 <asp:Label ID="lblpermitvalid" runat="server" Text="Permit Valid Upto"></asp:Label>
                                </td>
                                            
                                <td style="font-size: 13px; border-right-style: none">
                                 <b> <asp:Label ID="lbltxtpermitvalid" Text="" runat="server"></asp:Label></b>
                                </td>
                            </tr>
                             <tr> 
                                <td style="font-size: 13px; border-right-style: none;width:50%;" >
                                    <asp:Label ID="lblmodel" runat="server" Text="Lorry Model"></asp:Label>
                                </td>
                                            
                                <td style="font-size: 13px; border-right-style: none" >
                                    <b>
                                        <asp:Label ID="lbltxtmodel" Text="" runat="server"></asp:Label></b>
                                </td>
                            </tr>
                            </table>
                            </td>
                            <td style="width:50%;" valign="top">
                            <table border="0" width="100%" class="white_bg"> 
                              <tr>
                            <td colspan="2" style="border-bottom:1px solid #484848;height:10px;"> 
                            <strong>Driver Details:</strong>
                            </td> 
                            </tr>
                             <tr id="trDriverName" runat="server">
                            <td style="font-size: 13px; border-right-style: none;width:50%;"> 
                            <asp:Label ID="Label3" Text="Driver Name" runat="server"></asp:Label>
                            </td>
                            <td style="font-size: 13px; border-right-style: none">
                            <b><asp:Label ID="lbldrivername" runat="server"></asp:Label></b>
                            </td>
                            </tr>
                               <tr>
                          <td style="font-size: 13px; border-right-style: none;width:50%;"> 
                            <asp:Label ID="Label6" Text="Address" runat="server"></asp:Label>
                            </td>
                            <td style="font-size: 13px; border-right-style: none">
                           <b><asp:Label ID="lbldriverAddress" runat="server"></asp:Label></b>
                            </td>
                            </tr>
                            <tr> 
                                <td style="font-size: 13px; border-right-style: none;width:50%;" >
                                    <asp:Label ID="lbldrivermobile" runat="server" Text="Mobile"></asp:Label>
                                </td>
                                            
                                <td style="font-size: 13px; border-right-style: none" >
                                    <b>
                                        <asp:Label ID="lblmobtextdriver" Text="" runat="server"></asp:Label></b>
                                </td>
                            </tr>
                            <tr> 
                                <td style="font-size: 13px; border-right-style: none;width:50%;" >
                                    <asp:Label ID="lbldriverlicence" runat="server" Text="License No."></asp:Label>
                                </td>
                                            
                                <td style="font-size: 13px; border-right-style: none" >
                                    <b>
                                        <asp:Label ID="lbltxtdrvlicenceno" Text="" runat="server"></asp:Label></b>
                                       
                                </td>
                            </tr>

                             <tr> 
                                <td style="font-size: 13px; border-right-style: none;width:50%;" >
                                   <asp:Label ID="lblvalidupto" runat="server" Text="License Valid Upto"></asp:Label>
                                </td>
                                            
                                <td style="font-size: 13px; border-right-style: none" >
                                   
                                         <b><asp:Label ID="lbltxtvalidupto" Text="" runat="server"></asp:Label></b>

                                </td>
                            </tr>

                             
                             <tr> 
                                <td style="font-size: 13px; border-right-style: none;width:50%;" >
                                    <asp:Label ID="lblinsured" runat="server" Text="Insured Name"></asp:Label>
                                </td>
                                            
                                <td style="font-size: 13px; border-right-style: none" >
                                    <b>
                                        <asp:Label ID="lbltxtinsured" Text="" runat="server"></asp:Label></b>
                                </td>
                            </tr>
                              <tr> 
                                <td style="font-size: 13px; border-right-style: none;width:50%;" >
                                    <asp:Label ID="lblpolicyno" runat="server" Text="Policy No."></asp:Label>
                                </td>
                                            
                                <td style="font-size: 13px; border-right-style: none" >
                                    <b>
                                        <asp:Label ID="lbltxtpolicyno" Text="" runat="server"></asp:Label></b>
                                       
                                </td>
                            </tr>
                              <tr> 
                                <td style="font-size: 13px; border-right-style: none;width:50%;" >
                                     &nbsp;</td>
                                            
                                <td style="font-size: 13px; border-right-style: none" >
                                
                                        &nbsp;</td>
                            </tr>


                            </table>
                            </td>
                            </tr>
                            </table> 
                            </td> 
                         </tr>
                        
                        <tr>
                            <td colspan="4">
                                <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table1">
                                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                        <HeaderTemplate>
                                            <tr>
                                                
                                                <td style="font-size: 12px;" width="8%">
                                                    <strong>Gr No.</strong>
                                                </td>
                                                <td style="font-size: 12px;" width="8%">
                                                    <strong>GR Dt.</strong>
                                                </td>
                                                  <td style="font-size: 12px;" align="center" width="10%">
                                                    <strong>Pkgs </strong>
                                                </td>
                                                 <td style="font-size: 12px;" align="right" width="10%">
                                                    <strong>Wt.</strong>
                                                </td>
                                             <td class="white_bg" width="4%">
                                                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                  </td>
                                                <td style="font-size: 12px;" width="10%">
                                                    <strong>From</strong>
                                                </td>
                                                
                                                <td style="font-size: 12px;" width="10%">
                                                    <strong>Destination</strong>
                                                </td>
                                               <td style="font-size: 12px;" width="10%">
                                                    <strong>Consigner</strong>
                                                </td>
                                               <div id="hide" runat="server">
                                                <td style="font-size: 12px;" width="10%" align="center">
                                                <asp:Label ID="dr" runat="server">
                                                    </asp:Label>
                                                     <strong>Item</strong>
                                                </td>
                                                <td style="font-size: 12px;" width="10%" align="center">
                                                <asp:Label Visible="false" ID="rashi" runat="server"></asp:Label>
                                                 <strong>Inv. No. </strong>
                                                </td>
                                              
                                                <td style="font-size: 12px;" width="10%" align="right">
                                                <asp:Label ID="totalrashi" runat="server">
                                                </asp:Label>
                                                <strong>Inv. Vlaue</strong>
                                                </td>
                                                </div>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                               
                                                <td class="white_bg" width="8%">
                                                    <%#Eval("GR_No")%>
                                                </td>
                                                <td class="white_bg" width="8%">
                                                      <%#(string.IsNullOrEmpty(Eval("GRRet_Date").ToString()) ? "" : Convert.ToDateTime(Eval("GRRet_Date")).ToString("dd-MM-yyyy"))%>
                                                </td>
                                                   <td class="white_bg" width="10%" align="center">
                                                    <%#Eval("Qty")%>
                                                </td>
                                                <td class="white_bg" width="10%" align="right">
                                                    <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Weight")))%>
                                                </td>
                                                <td class="white_bg" width="4%">
                                                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                  </td>
                                                <td class="white_bg" width="10%">
                                                    <%#(string.IsNullOrEmpty(Eval("FromCity_Eng").ToString())? "" : Convert.ToString(Eval("FromCity_Eng")))%>
                                                </td>
                                              
                                                <td class="white_bg" width="10%">
                                                    <%#Eval("ToCity_Eng")%>
                                                </td>
                                              <td class="white_bg" width="10%">
                                                    <%#Eval("SenderName_Eng")%>
                                                </td>
                                                <td class="white_bg"  width="10%" align="center">
                                                  <%#Eval("Item_Name")%>
                                                  <asp:Label ID="amount" runat="server" Visible="false">
                                                    <%#string.Format("{0:0.00}", Convert.ToDouble(Eval("WithoutUnloading_Amnt")))%></asp:Label>
                                                </td>
                                              
                                                <td class="white_bg"  width="10%" align="center">
                                                  <%#Eval("Ref_No")%>
                                                <asp:Label ID="WagesAmnt" runat="server" Visible="false">
                                                    <%#string.Format("{0:0.00}", Convert.ToDouble(Eval("Wages_Amnt")))%></asp:Label>
                                                </td>
                                                <td class="white_bg"  width="10%" align="right">
                                                <asp:Label ID="Totalamount" runat="server">
                                                    <%#string.Format("{0:0.00}", Convert.ToDouble(Eval("Total_Price")))%>
                                                    </asp:Label>
                                                    
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                             <tr id="footer" runat="server" visible="false">
                                                
                                                <td style="font-size: 12px;" width="7%">
                                                </td>
                                                <td style="font-size: 12px;" width="10%">
                                                </td>
                                                  <td style="font-size: 12px;" align="center" width="5%">
                                                    <asp:Label ID="lbltotalqty" Visible="false" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                                   <td style="font-size: 12px;" align="right" width="5%">
                                                    <asp:Label ID="Label4" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                                <td style="font-size: 12px;" width="10%">
                                                </td>
                                                <td style="font-size: 12px;" width="7%">
                                                </td>
                                                
                                                <td style="font-size: 12px;" width="7%">
                                                    <asp:Label ID="lblttl" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                              
                                                <td style="font-size: 12px;" align="right" width="5%">
                                                    <asp:Label ID="lbltotalWeight" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                                <td style="font-size: 12px;" width="10%" align="left">
                                                </td>
                                                <td style="font-size: 12px;" width="10%" align="center">
                                                </td>
                                                <td style="font-size: 12px;" width="10%" align="right">
                                                    <asp:Label ID="lblAmount" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                                <td style="font-size: 12px;" width="8%" align="right">
                                                    <asp:Label ID="lblWagesAmnt" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                                <td style="font-size: 12px;" width="10%" align="right">
                                                    <asp:Label ID="lblTotalAmnt" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                       
                       <tr>
                            <td align="center" class="white_bg" valign="top" colspan="4" style="border-left-style: none; border-right-style: none">
                            
                                    <strong>
                                        <asp:Label ID="Label2" runat="server" Font-Size="14px" Text="Freight Payment Details"></asp:Label></strong>
                            </td>
                        </tr>
                        <tr>
                        <td colspan="4">    
                            <table border="0" cellspacing="0" style="font-size: 13px" width="100%" id="Table2">
                           <tr>
                               <td style="font-size: 13px; " >Starting Km. </td>
                               <td style="font-size: 13px; " > </td>
                               <td style="font-size: 13px; " > </td> 
                               <td style="font-size: 13px; " > </td>
                               <td style="font-size: 13px; " > </td>
                               <td style="font-size: 13px; " > </td>
                           </tr>
                           <tr>
                               <td style="font-size: 13px;" >Closing Km. </td>
                               <td style="font-size: 13px;" > </td>
                               <td style="font-size: 13px; " > Rate Per Km.</td> 
                               <td style="font-size: 13px;" > </td>
                               <td style="font-size: 13px; " > Freight</td>
                               <td style="font-size: 13px; " align="right"> <asp:Label ID="lbltotalfreight" runat="server" Font-Size="13px" valign="right"></asp:Label></td>
                           </tr>
                           <tr>
                               <td style="font-size: 13px; " >Total Km. </td>
                               <td style="font-size: 13px;" > </td>
                               <td style="font-size: 13px;" >Late D&D </td> 
                               <td style="font-size: 13px;" > </td>
                               <td style="font-size: 13px; " >Less Adv. </td>
                               <td style="font-size: 13px; " align="right">  <asp:Label ID="valuelblAdvanceAmnt" runat="server" Font-Size="13px" valign="right"></asp:Label>  </td>
                           </tr>
                           <tr>
                               <td style="font-size: 13px; " >BKG Dt. </td>
                               <td style="font-size: 13px; "  > <asp:Label ID="lblgrdate" runat="server" Font-Size="13px" valign="right"> </asp:Label></td>
                               <td style="font-size: 13px; " > Rate Amnt.</td> 
                               <td style="font-size: 13px;" > </td>
                               <td style="font-size: 13px; " >Hamali </td>
                               <td style="font-size: 13px; " align="right" > <asp:Label ID="lblhamali" runat="server" Font-Size="13px" valign="right"> </asp:Label></td>
                           </tr>
                           <tr>
                               <td style="font-size: 13px; " >Wt. </td>
                               <td style="font-size: 13px; " > <asp:Label ID="lbltotalweight" runat="server" Font-Size="13px" valign="right"> </asp:Label> </td>
                               <td style="font-size: 13px; " > Freight</td> 
                               <td style="font-size: 13px; "  > <asp:Label ID="lblfreightamount" runat="server" Font-Size="13px" valign="right"> </asp:Label></td>
                               <td style="font-size: 13px; " > Detention</td>
                               <td style="font-size: 13px; " > </td>
                           </tr>
                           <tr>
                               <td style="font-size: 13px; " >Dly Dt. </td>
                               <td style="font-size: 13px;" > </td>
                               <td style="font-size: 13px;" > Loss TDS </td> 
                               <td style="font-size: 13px; "  > <asp:Label ID="valueLblTdsAmnt" runat="server" Font-Size="13px" valign="right"></asp:Label></td>
                               <td style="font-size: 13px; " >Balance Payment </td>
                               <td style="font-size: 13px; " align="right" > <asp:Label ID="valuelblnetTotal" runat="server" Font-Size="13px" valign="right"></asp:Label></td>
                           </tr>

                            </table>
                         </td>
                        </tr>
                       
                        <tr>
                            <td align="left" valign="top" colspan="4">
                                <table width="100%" style="font-size: 12px" border="0" cellspacing="0">
                                    <tr style="line-height: 25px">
                                        <td colspan="9"  style="font-size: 13px" align="left" class="white_bg">
                                            <table width="100%">
                                            <tr>
                                            <td colspan="9" style="height:30px;">
                                             Remarks:एम.पी.,यू.पी.,ए.पी. की बहती मिलने पर बकाया भाढा मीलेगा |
                                            </td>
                                            </tr>
                                            <tr>
                                            <td colspan="9" style="height:30px;">
                                           
                                            </td>
                                            </tr>
                                                   <tr>
                                                    <td align="left" colspan="1" class="white_bg" style="font-size: 13px;text-align:left;">
                                                    Driver's Signature
                                                        </td>
                                                          <td align="left" valign="baseline" colspan="1" class="white_bg" style="font-size: 13px;text-align:center;">
                                                    Operation Clerk
                                                        </td> 
                                                          <td align="left" valign="baseline" colspan="1" class="white_bg" style="font-size: 13px;text-align:right;">
                                                   Accountant
                                                        </td>

                                                        </tr>
                                                    </table>
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
