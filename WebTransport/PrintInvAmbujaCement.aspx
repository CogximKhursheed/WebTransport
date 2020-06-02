<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintInvAmbujaCement.aspx.cs" Inherits="WebTransport.PrintInvAmbujaCement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 74px;
        }
        .style2
        {
            width: 78%;
            height: 117px;
        }
        .style3
        {
            height: 117px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
   
    <div id="PrintAS1" style="font-size: 13px; display: block;">
    
     <table cellpadding="1" cellspacing="0" width="1100px" border="1" style="font-family: Arial,Helvetica,sans-serif;height:70%">
             <tr>
                <td colspan="4">
                <div style="text-align:left;Width:140px; float:left;">
                    <asp:Image ID="imgAmbujacemant" Width="140px" Height="70px" runat="server" Visible="False"></asp:Image>
               </div>
               <div id="Header" runat="server" visible="false" style="text-align:center;">
                    <strong>
                        <asp:Label ID="lblCompanynameAS2" runat="server" Style="font-size: 14px;"></asp:Label><br />
                    </strong>
                    <asp:Label ID="lblCompAdd1AS2" runat="server"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblCompAdd2AS2" runat="server"></asp:Label><br />
                    <asp:Label ID="lblCompCityAS2" runat="server"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblCompStateAS2" runat="server"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblCompCityPinAS2" runat="server"></asp:Label><br />
                    <asp:Label ID="lblCompPhNoAS2" runat="server"></asp:Label>&nbsp;&nbsp;
                    </div>
                </td>
             </tr>
               <tr id="trPanAS2" runat="server">
                         <td align="left" class="white_bg" valign="top" colspan="4" style="font-size: 12px;
                    border-left-style: none; border-right-style: none">
                    <asp:Label ID="lblpanAS2" Text="PAN No.:" runat="server"></asp:Label>
                    <asp:Label ID="lblpanvalAS2" runat="server"></asp:Label>
                    </td>
                    </tr>
                    
               <tr id ="trtinAS2" runat="server">
                         <td align="left" class="white_bg" valign="top" colspan="4" style="font-size: 12px;
                    border-left-style: none; border-right-style: none">

                    <asp:Label ID="lblSerTAS2" runat="server" Text="Service Tax Registration Number:">
                    </asp:Label>&nbsp;&nbsp;<asp:Label  ID="lblvalSerTAS2" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                    
                </td>
            </tr>
                <tr id ="tr1" runat="server">
                         <td align="left" class="white_bg" valign="top" colspan="4" style="font-size: 12px;
                    border-left-style: none; border-right-style: none">

                    <asp:Label ID="Label1" runat="server" Text="Category of Service -Goods Transport Agency/Clearing & forwarding Agency/cargo handling Services">
                    </asp:Label>
                    
                </td>
            </tr>

               <tr>
                <td colspan="5">
                    <table width="100%">
                        <tr>
                            <td width="80%" align="left" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                border-right-style: none">
                                <strong style="text-decoration: underline">
                                    <asp:Label ID="Label84" runat="server" Text="Bill To"></asp:Label></strong>
                                <br />
                                <b>
                                    <asp:Label ID="lblSenderNameAS2" runat="server"></asp:Label></b>
                                <br />
                                <b>
                                    <asp:Label ID="lblsenderaddressAS2" runat="server"></asp:Label></b>
                                <br />
                                <b>
                                    <asp:Label ID="lblsendercityAS2" runat="server"></asp:Label>&nbsp;&nbsp;
                                    <asp:Label ID="lblsenderstateAS2" runat="server"></asp:Label>
                                </b>
                            </td>
                            <td align="center" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                border-right-style: none">
                                <strong style="text-decoration: underline">
                                    <asp:Label ID="Label90" runat="server" Text=""></asp:Label></strong>
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                border-right-style: none">
                                <strong style="text-decoration: underline;">
                                    <asp:Label ID="Label91" runat="server" Text=""></asp:Label></strong>
                            </td>
                            <td align="center" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                border-right-style: none">
                                <table>
                                    <tr>
                                        <td align="left">
                                            <b>
                                                <asp:Label ID="lblinvonoAS2" runat="server" Text="Bill No"></asp:Label></b>
                                            &nbsp;&nbsp;&nbsp;:
                                            <asp:Label ID="valuelblinvoicvenoAS2" runat="server" Text=""></asp:Label>
                                            <br />
                                            <b>
                                                <asp:Label ID="lblinvodateAS2" runat="server" Text="Bill Date"></asp:Label>
                                            </b>:
                                            <asp:Label ID="valuelblinvoicedateAS2" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

               <tr>
                            <td align="center" colspan="5" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                border-right-style: none">
                                 <strong> <asp:Label ID="lblcompanycementAS2"  runat="server" Text="Ambuja Plus"></asp:Label></strong>
                                </td>
           </tr>

               <tr>
                            <td align="left" colspan="5" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                border-right-style: none">
                              Bill For Secondry Transportation of&nbsp; <strong> <asp:Label ID="lblcementcompnay2AS2" runat="server" Text="Ambuja Cement"></asp:Label></strong> &nbsp;from  <strong> <asp:Label ID="lblloctionfromAS2" runat="server" Text=""></asp:Label></strong> &nbsp; Date &nbsp;<strong> <asp:Label ID="lbldatefromAS2" runat="server" Text=""></asp:Label></strong> &nbsp;To <strong> <asp:Label ID="lbldatetoAS2" runat="server" Text=""></asp:Label></strong> 
                             </td>
              </tr>

               <tr>
                            <td align="center" colspan="5" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                border-right-style: none">
                                 <strong>Month of <asp:Label ID="lblformont"  runat="server" Text="FOR MONTH"></asp:Label></strong>
                                </td>
           </tr>

               <tr>
                                          <td width="60%" align="center">
                                         <strong><asp:Label ID="Label18"   runat="server" Text="Particular" Font-Size="13px"></asp:Label></strong>
                                          </td>
                                        <td width="20%" align="center">
                                            <strong> <asp:Label ID="Label36" runat="server" Text="Qty(mt.)" Font-Size="13px"></asp:Label></strong>
                                        </td>
                                      <td width="20%" align="center">
                                            <strong> <asp:Label ID="Label53" runat="server" Text="Amount" Font-Size="13px"></asp:Label></strong>
                                        </td>
             </tr>

               <tr>
                                        
                                          <td width="60%" align="center">
                                          &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <asp:Label ID="Label35"   runat="server" Text="Please Find enclosed herewith our bill for Secondry Transportation Of Ambuja Cement From "></asp:Label><strong> <asp:Label ID="lblPartiloctionfromAS2" runat="server"  ></asp:Label></strong>&nbsp; Date <strong> <asp:Label ID="lbldatefromparti" runat="server"  ></asp:Label></strong> &nbsp;To <strong> <asp:Label ID="lbldatetoparti" runat="server"  ></asp:Label></strong>&nbsp;For<strong> <asp:Label ID="lblQtyparti" runat="server"  ></asp:Label></strong> Mt.
                                          </td>
                                        <td width="20%" align="center">
                                            <strong> <asp:Label ID="lbltotlqtyAS2" runat="server"  ></asp:Label></strong>
                                        </td>
                                      <td width="20%" align="center">
                                            <strong> <asp:Label ID="lbltotlamountAS2" runat="server"  ></asp:Label></strong>
                                        </td>
             </tr>
           
               <tr>
                                          <td width="60%" align="center">
                                          <asp:Label ID="Label52" Font-Size="13px"  runat="server" Text="Net Total"></asp:Label>
                                          </td>
                                        <td width="20%" align="right">
                                            &nbsp;&nbsp;
                                            <asp:Label ID="lblfoterqtytotlAS2" runat="server"  Font-Size="13px" valign="right"></asp:Label>
                                        </td>
                                      <td width="20%" align="right">
                                            &nbsp;&nbsp;
                                            <asp:Label ID="lblfoteramnt" runat="server"  Font-Size="13px" valign="right"></asp:Label>
                                        </td>
          </tr>

               <tr>
                                          <td colspan="5"  align="left">
                                          <asp:Label ID="Label54" Font-Size="8" Font-Bold="true" runat="server" Text="AMT. IN WORDS(Rs.) : "></asp:Label>
                                                        <asp:Label ID="lblamntinwrdAS2" Font-Size="9" Font-Bold="true" runat="server" Text=""></asp:Label>
                                          </td>
                                    
                                      
             </tr>
               
               <tr>
                <td style="width: 100%" colspan="5">
                    <table width="100%" align="right">
                       
                        <tr>
                            <td colspan="3" align="left" width="30%">
                                <table>
                                    <tr>
                                        <td align="left"  width="80%">
                                        1.All Transaction are subject to Jaipur Jurisdiction only
                                                </td>
                                    </tr>
                                    <tr>
                                        <td width="80%" align="left">
                                            <asp:Label ID="Label65" runat="server" Text="Enclosures Original" valign="right"></asp:Label>&nbsp;:
                                            <asp:Label ID="lblenclousergrAS2" Visible="false" Text="GR :" runat="server" valign="right"></asp:Label> &nbsp;
                                           <asp:Label ID="lblValenclousergrAS2" Visible="false" runat="server" valign="right"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td width="80%"  align="left">
                                         <asp:Label ID="Label70" runat="server" Text="Enclosures Original" valign="right"></asp:Label>&nbsp;:<asp:Label ID="lblencloChln" Visible="false" Text="Challan :" runat="server" valign="right"></asp:Label>&nbsp;&nbsp;
                                      <asp:Label ID="lblenclouservalchln" Text="" runat="server" Visible="false" valign="right"></asp:Label>
     
                                    </td>
                                    </tr>

                                </table>
                            </td>
                            <td width="16%" align="center" valign="top">
                            </td>
                            <td colspan="2" width="20%" align="right" valign="top">
                                <table width="100%">
                                  
                                    <tr>
                                        <td align="right" class="white_bg" style="font-size: small" valign="top" colspan="3">
                                            <b>
                                                <asp:Label ID="lblcompanyFooterAS2" runat="server"></asp:Label><br />
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
      
     <table cellpadding="1" cellspacing="0" width="1100px" border="1" style="font-family: Arial,Helvetica,sans-serif;
            border-width: 1px; border-color: #000000;page-break-before: always;">
             <tr id="cmp" runat="server">
                            <td style="font-size:12px;" colspan="4">
                                <asp:Label ID="lblcmpjurisdiction" runat="server"  Style="font-size: 12px;"></asp:Label>
                            </td>
                        </tr> 
            <tr id="trcompnay" runat="server">
           
                <td align="left" class="white_bg" valign="top" colspan="4" style="font-size: 12px;
                    border-left-style: none; border-right-style: none">
                      <asp:Label ID="Label21" runat="server" Text="Firm Name :"></asp:Label>
                        <asp:Label ID="lblCompanynameAS"  runat="server" Style="font-size: 14px;"></asp:Label><br />
                   
                    </td>
                    </tr>
                   
            <tr id="traddress" runat="server">
                     <td align="left" class="white_bg" valign="top" colspan="4" style="font-size: 12px;
                    border-left-style: none; border-right-style: none">
                       <asp:Label ID="Label24" runat="server" Text="Address :"></asp:Label>
                    <asp:Label ID="lblCompAdd1AS" runat="server"></asp:Label>
                    <asp:Label ID="lblCompAdd2AS" runat="server"></asp:Label>
                    <asp:Label ID="lblCompCityAS" runat="server"></asp:Label>
                    <asp:Label ID="lblCompStateAS" runat="server"></asp:Label>
                    <asp:Label ID="lblCompCityPinAS" runat="server"></asp:Label>
                    <asp:Label ID="lblCompPhNoAS" runat="server"></asp:Label>
                    </td>
                    </tr>

            <tr id="trpan" runat="server">
                         <td align="left" class="white_bg" valign="top" colspan="4" style="font-size: 12px;
                    border-left-style: none; border-right-style: none">
                    <asp:Label ID="lblTxtPanNoAS" Text="PAN No.:" runat="server"></asp:Label>
                    <asp:Label ID="lblPanNoAS" runat="server"></asp:Label>
                    </td>
                    </tr>
                    
            <tr id ="trSerTnno" runat="server">
                         <td align="left" class="white_bg" valign="top" colspan="4" style="font-size: 12px;
                    border-left-style: none; border-right-style: none">

                    <asp:Label ID="lblSrvtAS" runat="server" Text="Service Tax Registration Number:"></asp:Label>&nbsp;&nbsp;<asp:Label
                        ID="lblCompSertAS" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                    
                </td>
            </tr>
             <tr id ="tr2" runat="server">
                         <td align="left" class="white_bg" valign="top" colspan="4" style="font-size: 12px;
                    border-left-style: none; border-right-style: none">

                    <asp:Label ID="Label2" runat="server" Text="Category of Service -Goods Transport Agency/Clearing & forwarding Agency/cargo handling Services">
                    </asp:Label>
                    
                </td>
            </tr>
            <tr>
            <td colspan="5"></td>
            </tr>
            <tr>
                <td colspan="5">
                    <table width="100%">
                        <tr>
                            <td width="80%" align="left" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                border-right-style: none">
                                <strong style="text-decoration: underline">
                                    <asp:Label ID="Label80" runat="server" Text="Bill To  "></asp:Label></strong>
                                <br />
                                <b>
                                    <asp:Label ID="lblSenderNameAS" runat="server"></asp:Label></b>
                                <br />
                                <b>
                                    <asp:Label ID="lblsenderaddressAS" runat="server"></asp:Label></b>
                                <br />
                                <b>
                                    <asp:Label ID="lblsendercityAS" runat="server"></asp:Label>&nbsp;&nbsp;
                                    <asp:Label ID="lblsenderstateAS" runat="server"></asp:Label>
                                </b>
                            </td>
                            <td align="center" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                border-right-style: none">
                                <strong style="text-decoration: underline">
                                    <asp:Label ID="Label85" runat="server"></asp:Label></strong>
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                border-right-style: none">
                                <strong style="text-decoration: underline;">
                                    <asp:Label ID="lblPrintHeadngAS" runat="server"></asp:Label></strong>
                            </td>
                            <td align="center" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                border-right-style: none">
                                <table>
                                    <tr>
                                        <td align="left">
                                            <b>
                                                <asp:Label ID="lblinvoicenoAS" runat="server" Text="Bill No"></asp:Label></b>
                                            &nbsp;&nbsp;&nbsp;:
                                            <asp:Label ID="valuelblinvoicvenoAS" runat="server" Text=""></asp:Label>
                                            <br />
                                            <b>
                                                <asp:Label ID="lblinvoicedateAS" runat="server" Text="Bill Date"></asp:Label>
                                            </b>:
                                            <asp:Label ID="valuelblinvoicedateAS" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            
            <tr>
                            <td align="center" colspan="5" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                border-right-style: none">
                                 <strong> <asp:Label ID="lblcementcompnay1"  runat="server" Text="Ambuja Plus"></asp:Label></strong>
                                </td>
           </tr>
            
            <tr>
                            <td align="left" colspan="5" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                border-right-style: none">
                               Bill for Secondry transportation From  &nbsp; <strong> <asp:Label ID="lblcompanynameAS1" runat="server" Text=""></asp:Label></strong> &nbsp;  <strong> <asp:Label ID="lblBilloctionfromAS1" runat="server" Text=""></asp:Label></strong> &nbsp;
                             </td>
              </tr>
             
             <tr>
                            <td align="left" colspan="5" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                border-right-style: none">
                               Transportation Bill of From&nbsp; <strong> <asp:Label ID="lblcementcompnay2" runat="server" Text="Ambuja Cement"></asp:Label></strong> &nbsp; <strong> <asp:Label ID="lblloctionfromAS1" runat="server" Text=""></asp:Label></strong> &nbsp; Date &nbsp;<strong> <asp:Label ID="lbldatefrom" runat="server" Text=""></asp:Label></strong> &nbsp;To <strong> <asp:Label ID="lbldateto" runat="server" Text=""></asp:Label></strong> &nbsp;
                             </td>
              </tr>


            <tr>
                <td colspan="4">
                    <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table8">
                        <asp:Repeater ID="Repeater4" runat="server"  OnItemDataBound="Repeater4_ItemDataBound">
                            <HeaderTemplate>
                                <tr>
                                    <td class="white_bg" style="font-size: 12px" width="3%">
                                        <strong>S.No.</strong>
                                    </td>
                                         
                                    <td class="white_bg" style="font-size: 12px" width="6%">
                                        <strong>Performa</strong>
                                    </td>
                                     <td style="font-size: 12px" width="6%">
                                        <strong>Deleivry</strong>
                                    </td>
                                    <td style="font-size: 12px" width="7%" align="center">
                                        <strong>Date</strong>
                                    </td>
                                   
                                    <td style="font-size: 12px" width="5%">
                                        <strong>Truck No</strong>
                                    </td>
                                     <td style="font-size: 12px" align="center" width="6%">
                                        <strong>LR No.</strong>
                                    </td>
                                    <td style="font-size: 12px" align="left" width="7%">
                                        <strong>Customer's</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>Delivery</strong>
                                    </td>
                                    <td style="font-size: 12px" width="5%">
                                        <strong>Dis.</strong>
                                    </td>
                                      <td style="font-size: 12px"  width="5%">
                                        <strong>Recd.</strong>
                                    </td>
                                    <td style="font-size: 12px"  width="7%">
                                        <strong>Freight</strong>
                                    </td>
                                     <td style="font-size: 12px" width="7%">
                                        <strong>Amount</strong>
                                    </td>
                                    <td style="font-size: 12px" width="7%">
                                        <strong>Shipment</strong>
                                    </td>
                                    <td style="font-size: 12px" width="7%">
                                        <strong>Service Enter</strong>
                                    </td>
                                    <td style="font-size: 12px" width="7%">
                                        <strong>District</strong>
                                    </td>
                                  
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="white_bg" width="3%">
                                        <%#Container.ItemIndex+1 %>.
                                    </td>
                                       <td class="white_bg" width="6%">
                                        <%#Eval("Performa")%>
                                    </td>
                                       <td class="white_bg" width="6%">
                                        <%#Eval("DI_NO")%>
                                    </td>
                                    <td class="white_bg" width="7%">
                                        <%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MM-yyyy")%>
                                    </td>
                                       <td class="white_bg" width="6%">
                                        <%#Eval("LORRY_NO")%>
                                    </td>
                                    <td class="white_bg" width="7%" align="center">
                                        <%#Eval("Gr_No")%>
                                    </td>
                                     <td class="white_bg" width="8%">
                                        <%#Eval("Recivr_Name")%>&nbsp;
                                    </td>
                                    <td class="white_bg" width="5%">
                                        <%#Eval("Delvry_Place")%>&nbsp;
                                    </td>
                                    <td class="white_bg" width="5%">
                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Weight")))%>&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="white_bg" width="5%">
                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Weight")))%>&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                     <td class="white_bg" width="7%">
                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Rate")))%>&nbsp;
                                    </td>
                                     <td class="white_bg" width="7%">
                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%>&nbsp;
                                    </td>
                                    <td class="white_bg" width="7%">
                                        <%#Eval("Shipment_No")%>&nbsp;
                                    </td>
                                    <td class="white_bg" width="8%">
                                        <%#Eval("servicenter")%>&nbsp;
                                    </td>
                                    <td class="white_bg" width="7%">
                                        <%#Eval("district")%>&nbsp;
                                    </td>
                                   
                                </tr>
                            </ItemTemplate>
                             <FooterTemplate>
                          </FooterTemplate>
                                         
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
           
                       <tr>
                            <td colspan="4">
                                <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table9">
                                    <tr>
                                       <td class="white_bg" style="font-size: 12px" width="3%">
                                           Total
                                    </td>
                                        
                                    <td class="white_bg" style="font-size: 12px" width="6%">
                                         
                                    </td>
                                     <td style="font-size: 12px" width="6%">
                                         
                                    </td>
                                    <td style="font-size: 12px" width="7%" align="center">
                                     
                                    </td>
                                   
                                    <td style="font-size: 12px" width="6%">
                                  
                                    </td>
                                     <td style="font-size: 12px" align="center" width="7%">
                                  
                                    </td>
                                    <td style="font-size: 12px" align="left" width="8%">
                                  
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                  
                                    </td>
                                    <td style="font-size: 12px" width="5%">
                                    <asp:Label ID="lbldisfoter" runat="server" ></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                     <td style="font-size: 12px"  width="5%">
                                      <asp:Label ID="lblrecdfoter" runat="server" ></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td style="font-size: 12px"  width="7%">
                                       
                                    </td>
                                     <td style="font-size: 12px" width="8%">
                                        <asp:Label ID="lbltotlamntrfoter" runat="server" ></asp:Label>&nbsp;
                                    </td>
                                    <td style="font-size: 12px" width="7%">
                                  
                                    </td>
                                    <td style="font-size: 12px" width="7%">
                                  
                                    </td>
                                    <td style="font-size: 12px" width="7%">
                                  
                                    </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
            <tr style="border-bottom-style: solid; border-top-width: thin; border-bottom-width: thin;
                                        border-right-width: thin">
                                          <td width="100%" align="left">
                                          <asp:Label ID="Label32" Font-Size="8" Font-Bold="true" runat="server" Text="AMT. IN WORDS(Rs.) : "></asp:Label>
                                                        <asp:Label ID="lblamntword" Font-Size="9" Font-Bold="true" runat="server" Text=""></asp:Label>
                                          </td>
                                      
                                            <asp:Label ID="lblnetAS" runat="server" Text="Net Amnt" Visible="false" Font-Size="13px" valign="right"></asp:Label> <asp:Label ID="lblNetAmntAS" Visible="false" runat="server" Font-Size="13px" Text="" valign="lef"></asp:Label>
                                      
                                      
             </tr>

            <tr>
            <td width="100%" colspan="5">
                      <asp:Label ID="lblremarkAS" runat="server" valign="right" Text="I/we Declare that in terms of notification no.1/2006 services tax dated 01/03/2006,we have not taken cenvat credit of duty on inputs or capitals goods or cenvat credit odf service tax on input service.Used for  providing such taxable service under the provision of cenvat credit rules,2004 and that we have not availed benefit under notification of Government of India in the ministry of finance (Department Of revenue).No.12/2003-service tax dates 20/06/2003(G.S.R.503)(e) dated 20/06/2003)  					"></asp:Label>
                                    
            </td>
            </tr>
         
            <tr>
                <td style="width: 100%" colspan="5">
                    <table width="100%" align="right">
                       
                        <tr>
                            <td colspan="3" align="left" width="30%">
                                <table>
                                    <tr>
                                        <td width="80%">
                                                </td>
                                    </tr>
                                    <tr>
                                        <td width="80%">
                                            <asp:Label ID="lblenclosureAS" runat="server" Text="Encl" valign="right"></asp:Label>&nbsp;:
                                            <asp:Label ID="lblenclousergr" Visible="false" Text="GR No.:" runat="server" valign="right"></asp:Label> &nbsp;
                                           <asp:Label ID="lblvalueencosersAS" Visible="false" runat="server" valign="right"></asp:Label>
                                           

                                        </td>
                                    </tr>
                                    <tr>
                                    <td width="80%">
                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblenclouserchallanAS" Visible="false" Text=":Challan :" runat="server" valign="right"></asp:Label>&nbsp;&nbsp;
                                      <asp:Label ID="lblchallanval" Text="" runat="server" Visible="false" valign="right"></asp:Label>
     
                                    </td>
                                    </tr>

                                </table>
                            </td>
                            <td width="16%" align="center" valign="top">
                            </td>
                            <td colspan="2" width="20%" align="right" valign="top">
                                <table width="100%">
                                  
                                    <tr>
                                        <td align="right" class="white_bg" style="font-size: small" valign="top" colspan="3">
                                            <b>
                                                <asp:Label ID="lblcompnameAS" runat="server"></asp:Label><br />
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
     <asp:HiddenField ID="hideimgvalue" runat="server" />
    </div>
    </form>
</body>
</html>
