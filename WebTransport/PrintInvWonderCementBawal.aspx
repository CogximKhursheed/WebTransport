<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintInvWonderCementBawal.aspx.cs" Inherits="WebTransport.PrintInvWonderCementBawal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 74px;
        }
        .style3
        {
            height: 117px;
        }
        .style4
        {
            width: 3%;
        }
        .style5
        {
            width: 4%;
        }
        tr.border_bottom td {
     border-bottom:1pt solid black;
   }
    tr.border_top td {
     border-bottom:1pt solid black;
   }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <%--<div id="printNimbaheraHead" runat="server" style="font-size: 13px;">
             
                    </div>--%>
    
    

    <div id="printNimbaheraDetl" runat="server" style="font-size: 13px;">
    <div style="height:5%"></div>
    <table cellpadding="1" cellspacing="0" width="100%" border="0" style="font-family: Arial,Helvetica,sans-serif;height:70%">
                        <tr>
                            <td style="font-size:12px;">
                                <asp:Label ID="lblJurCity" runat="server" Text=""></asp:Label></td>
                            
                            <td style="font-size:12px; text-align:right;">
                                <asp:Label ID="lblCompPhNo" runat="server"></asp:Label>
                                </td>
                        </tr>
                       
                            <tr style="height:120px;">
                            <td colspan="4">
                             <div style="text-align:left;Width:140px; float:left;">
                            <asp:Image ID="imgWondercement1" Width="140px" Height="80px" runat="server" Visible="false"></asp:Image>
                            </div>
                                <%--<td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                                    border-left-style: none; border-right-style: none">--%>
                                   <div id="header" runat="server" align="center" visible="false" class="white_bg" style="font-size: 14px;
                                    border-left-style: none; border-right-style: none">
                                            <strong>
                                                <asp:Label ID="lblCompanyname" runat="server" Style="font-size: 18px;"></asp:Label><br />
                                            </strong>
                                            <asp:Label ID="lblCompAdd1" runat="server"></asp:Label><br />
                                            <asp:Label ID="lblCompAdd2" runat="server"></asp:Label>
                                            <asp:Label ID="lblCompCity" runat="server"></asp:Label>&nbsp;&nbsp;
                                            <asp:Label ID="lblCompState" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                            <br />
                                                FLEET OWNER TRANSPORT CONTRACTOR
                                     </div>
                                      </td>
                            </tr>
                            
                            <tr>
                                <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;text-decoration: underline;
                                    border-left-style: none; border-right-style: none">
                                    
                                    <b>TRANSPORTATION FREIGHT BILL (SECONDARY) : CEMENT FWD WCL</b>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="white_bg" valign="top" colspan="4" style="font-size: 13px;
                                    border-left-style: none; border-right-style: none">
                                    
                                    <asp:Label ID="lblPanNo" runat="server" Text=""></asp:Label>    <br />

                                    
                                    <asp:Label ID="lblstaxno" runat="server" Text=""></asp:Label>    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table border="0" width="100%" style="border-top:1px solid #484848;border-bottom:1px solid #484848;">
                                        <tr>
                                            <td colspan="3" align="left" class="white_bg" style="width:30%;font-size: 12px; border-right-style: none" valign="top" >
                                              
                                            <asp:Label ID="lblSenderName" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblsenderaddress" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblsendercity" runat="server"></asp:Label>&nbsp;&nbsp;  <br />
                                            <asp:Label ID="lblsenderstate" runat="server"></asp:Label>
           
                                            </td>
                                            <td colspan="3" align="left" class="white_bg" style="width:18%;font-size: 12px; border-right-style: none" valign="top">
                                               <asp:Label ID="lblinvoiceno" runat="server" Text="Bill No : "></asp:Label>
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="valuelblinvoicveno" runat="server" Text=""></asp:Label>
                                            <br />
                                           
                                                <asp:Label ID="lblinvoicedate" runat="server" Text="Date : "></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="valuelblinvoicedate" runat="server" Text=""></asp:Label><br />
                                            <asp:Label ID="Label6" runat="server" Text="Type : "></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
                                            <br />
                                            </td>
                                        </tr>
                                           <tr></tr>     
                                           <tr></tr>                                
                                    </table>
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="4">
                                    <table cellspacing="0" style="font-size: 12px; "  width="100%" id="Table1">
                                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                            <HeaderTemplate>
                                                <tr  class="border_bottom border_top" >
                                                
                                                    <td class="white_bg" style="font-size: 12px"  width="10%">
                                                        <strong>S.No.</strong>
                                                    </td>
                                                    <td style="font-size: 12px" width="30%">
                                                        <strong>Particulars</strong>
                                                    </td>
                                                    <td style="font-size: 12px" width="10%">
                                                        <strong>No of LRs</strong>
                                                    </td>
                                                    <td style="font-size: 12px" width="10%">
                                                        <strong>Weight[MT]</strong>
                                                    </td>
                                                    <td style="font-size: 12px" width="10%">
                                                        <strong>Amount</strong>
                                                    </td>
                                                    <td style="font-size: 12px" align="left" width="10%">
                                                        <strong>Remarks</strong>
                                                    </td>
                                                    <td style="font-size: 12px" align="left" width="10%">
                                                        <strong>Short</strong>
                                                    </td>
                                                  
                                                    
                                                </tr>
                                                
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="white_bg" width="10%">
                                                        <%#Container.ItemIndex+1 %>.
                                                    </td>
                                                    <td class="white_bg" width="30%">
                                                        Details of LR as per attached Sheet
                                                    </td>
                                                    <td class="white_bg" width="5%">
                                                        <%#Eval("GR_Count")%>
                                                    </td>
                                                    <td class="white_bg" width="10%">
                                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("TotInvWeight")))%>
                                                    </td>
                                                    <td class="white_bg" width="10%">
                                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("TotInvAmount")))%>
                                                    </td>
                                                    <td class="white_bg" width="10%" align="left">
                                                        
                                                    </td>
                                                    <td class="white_bg" width="10%" align="left">
                                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("TotInvShortage_Qty")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                    <br /><br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table border="0"  cellspacing="0" style="font-size: 12px;border-top:1px solid #484848;border-bottom:1px solid #484848;" width="100%" id="Table2">
                                        <tr>
                                            <td class="white_bg" width="15%">
                                            <b>Total</b>
                                            </td>
                                            <td class="white_bg" width="12%">
                                            </td>
                                            <td class="white_bg" width="14%" align="center">
                                                <asp:Label ID="lblTotGr" Text="" Font-Bold="true" runat="server"></asp:Label>
                                            </td>
                                            <td class="white_bg" width="10%" align="right">
                                                <asp:Label ID="lbltotalWeight" Font-Bold="true" runat="server"></asp:Label>
                                            </td>
                                            <td class="white_bg" width="15%" align="right">
                                                <asp:Label ID="lblTotalAmnt" Font-Bold="true" runat="server"></asp:Label>
                                            </td>
                                            <td class="white_bg" width="13%">
                                            </td>
                                            <td class="white_bg" width="12.5%" align="right">
                                                <asp:Label ID="lblTotalShort" Font-Bold="true" runat="server"></asp:Label>
                                            </td>
                                            
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="left" style="font-size: 12px;">
                                  <%--  <br />RS.&nbsp;&nbsp;<asp:Label ID="lblTowords" runat="server" Font-Bold="True"></asp:Label>&nbsp;Only.--%>
                                </td>
                             </tr>
                             <tr><td colspan="4" style="width:100%">
                             <table style="border:1px solid #000;">
                            <tr>
                                <td colspan="4" style="border-bottom:1px solid #484848;">
                                           <table width="100%"  style="font-size: 12px;">
                                           <tr>
                                           <td width="78%">
                                             Service Tax @ 14% & 0.5% & KKC @ 0.5% on 30% of Freight & To Be paid by Consigner&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                           </td>
                                           <td>
                                            <table width="250px" style="font-size: 12px;">
                                             <tr>
                                             <td style="border-left:1px solid #484848;border-bottom:1px solid #484848;border-right:1px solid #484848;"><b>Total Freight Amount</b></td>
                                             <td align="right" style="border-bottom:1px solid #484848;">
                                                 <asp:Label ID="lblTotFreightAmount" runat="server" Text=""></asp:Label></td>
                                             </tr>
                                             <tr>
                                             <td style="border-bottom:1px solid #484848;border-left:1px solid #484848;border-right:1px solid #484848;"><b>Less Abatement 70%</b></td>
                                             <td align="right" style="border-bottom:1px solid #484848;"><asp:Label ID="lblTotAbatement" runat="server" Text=""></asp:Label></td>
                                             </tr>
                                             <tr>
                                             <td style="border-left:1px solid #484848;border-right:1px solid #484848;"><b>Less Abatement 30%</b></td>
                                             <td align="right"><asp:Label ID="lblTotLessAbate" runat="server" Text=""></asp:Label></td>
                                             </tr>
                                            </table>
                                           </td>
                                           </tr>
                                           </table>
                                         </td>
                            </tr>

                                <tr>
                                <td colspan="4" style="border-bottom:1px solid #484848;">
                                       <table style="font-size: 12px;">
                                           <tr>
                                           <td>
                                             (1) In terms of notification no.08/2015 S.T. dated 01-03-2015 Service Tax has been calculated on a value which is equivalent to 30% of the gross amount charged, from the customers, for providing the taxable service namely 'Transport of Goods by Road' service.
                                           </td>
                                           <td>
                                            <table border="0" width="250px" style="font-size: 12px;">
                                             
                                             <tr>
                                             <td style="width:155px;border-left:1px solid #484848;"><b>Service Tax</b> </td>
                                             <td align="right" style="border-left:1px solid #484848;"><asp:Label ID="lblSerTax" runat="server" Text=""></asp:Label></td>
                                             </tr>
                                             
                                            </table>
                                           </td>
                                           </tr>
                                           </table>
                                </td>
                            </tr>

                                <tr>
                                <td style="font-size: 12px;border-bottom:1px solid #484848;" colspan="4" 
                                        class="style1">
                                      <table width="100%" style="font-size: 13px;">
                                           <tr>
                                           <td>
                                             (2) I/We hereby certified that I/We have not taken the CENVET Credit of Excise duty paid on inputs. Capital goods and of the Service Tax paid on input Services, used for providing such taxable services namely 'Transportation  of Goods by Road' services, under the provisions of the Cenvat Credit Rules, 2004.  
                                           </td>
                                           <td>
                                            <table border="0" width="250px" style="font-size: 12px;">
                                             <tr>
                                             <td style="width:155px;border-left:1px solid #484848;border-right:1px solid #484848;border-bottom:1px solid #484848;"><b>Swatch Bharat Cess </b></td>
                                             <td align="right" style="border-bottom:1px solid #484848;"><asp:Label ID="lblSwatchBharatTax" runat="server" Text=""></asp:Label></td>
                                             </tr>
                                             <tr>
                                             <td style="width:155px;border-left:1px solid #484848;border-right:1px solid #484848;border-bottom:1px solid #484848;"><b>Krishi Kalyan Cess</b> </td>
                                             <td align="right" style="border-bottom:1px solid #484848;"><asp:Label ID="lblKrishiTaxHead" runat="server" Text=""></asp:Label></td>
                                             </tr>
                                             <tr>
                                             <td style="width:155px;border-left:1px solid #484848;border-right:1px solid #484848;"><b>Total Service Tax</b></td>
                                             <td align="right"><asp:Label ID="lblTOTServTax" runat="server" Text=""></asp:Label></td>
                                             </tr>
                                            </table>
                                           </td>
                                           </tr>
                                           </table>
                                </td>
                            </tr>

                                <tr>
                                <td style="font-size: 12px;" colspan="4">
                                     <table style="font-size: 13px;">
                                           <tr>
                                           <td>
                                             (3) I/We also agree to indemnify the company against any payment liability/loss of credit/damage caused to the company in case of my/our default to company with the said declaration.
                                           </td>
                                           <td>
                                            <table border="0" width="250px" style="font-size: 12px;border-left:1px solid #484848;">
                                             <tr>
                                             <td>Person liable to pay service tax & Cess - M/s </td>
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
                            <tr>
                                <td align="left" valign="top" colspan="4">
                                    <table width="100%" style="font-size: 12px" border="0" cellspacing="0">
                                        <tr style="line-height: 25px">
                                            <td colspan="9" style="font-size: 13px" align="left" class="white_bg">
                                                <table width="100%">
                                                    <tr>
                                                        <td align="left" class="white_bg" style="font-size: 13px" width="50%">
                                                            <br />
                                                            <br />
                                                            <br />
                                                            <br />
                                                            <b></b>&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td align="right" class="white_bg" style="font-size: 13px" valign="top" width="50%">
                                                            <br />
                                                            <b>
                                                                <asp:Label ID="lblCompname" runat="server"></asp:Label><br /> </b>
                                                                <br />
                                                                <br />
                                                                Authorised Signatory&nbsp;
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

     
    <table cellpadding="1" cellspacing="0" width="100%" border="0" style="font-family: Arial,Helvetica,sans-serif;
            border-width: 1px; border-color: #000000;page-break-before: always;">
             <tr>
                            <td style="font-size:13px;" class="style3">
                                <asp:Label ID="lblJurCity1" runat="server" Text=""></asp:Label></td>
                            <td class="style4">
                                &nbsp;</td>
                            <td style="font-size:13px; text-align:right;" class="style5">
                                 <asp:Label ID="lblCompPhNo1" runat="server" Text=""></asp:Label></td>
             </tr>
            <tr>
            <td colspan="3">
             <div style="text-align:left;Width:140px; float:left;">
               <asp:Image ID="imgWondercement2" Width="140px" Height="80px" runat="server" Visible="false"></asp:Image>
             </div>
                <div id="Header1" runat="server" visible="false" align="center" class="white_bg" style="font-size: 14px;
                    border-left-style: none; border-right-style: none">
                    &nbsp;&nbsp;&nbsp;&nbsp;
                        <br /> 
                            <asp:Label ID="lblCompanyname1" runat="server" Style="font-size: 21px;" Font-Underline="True"></asp:Label>
                        <br />
                    <asp:Label ID="lblCompAdd3" runat="server"></asp:Label><br />
                    <asp:Label ID="lblCompAdd4" runat="server"></asp:Label>
                    <asp:Label ID="lblCompCity1" runat="server"></asp:Label><asp:Label ID="lblDist" runat="server" Text=""></asp:Label>
                    &nbsp;-&nbsp;
                    <asp:Label ID="lblCompCityPin" runat="server"></asp:Label>
                    &nbsp;&nbsp;
                    <br />
                    <asp:Label ID="lbltxtDis" runat="server" Text="FLEET OWNER TRANSPORT CONTRACTOR"></asp:Label>
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lbldel" runat="server" Text="TRANSPORTATION FREIGHT BILL (SECONDARY) : CEMENT FWD WCL" Font-Underline="True"></asp:Label><br />
                    <br />
                    <br />
                </div>
                 </td>
            </tr>
            <tr>
            <td>
            &nbsp;</td>
            </tr>
            <tr>
                <td colspan="5">
                    <table width="100%">
                        <tr>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-left-style: none;
                                border-right-style: none">
                                
                                    Party :
                                    <asp:Label ID="Labparty" runat="server"></asp:Label>
                                <br />
                                    <asp:Label ID="LabpartyAdd" runat="server"></asp:Label>
                                <br />
                                    <asp:Label ID="LabpartyCity" runat="server"></asp:Label>&nbsp;&nbsp;  <br />
                                    <asp:Label ID="LabpartyState" runat="server"></asp:Label> <br /><br />

                                    From : 
                                    <asp:Label ID="lblFromAdd" runat="server"></asp:Label>
                            </td>
                            <td align="center" class="white_bg" valign="top" style="font-size: 13px; border-left-style: none;
                                border-right-style: none">
                                <strong style="text-decoration: underline"></strong>
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-left-style: none;
                                border-right-style: none">
                                <strong style="text-decoration: underline;"></strong>
                            </td>
                            <td align="right" class="white_bg" valign="top" style="padding-right:40px;font-size: 13px; border-left-style: none;border-right-style: none">
                                <table>
                                    <tr>
                                        <td align="left" valign="middle" style="font-size: 12px;">
                                            <b>
                                                <asp:Label ID="Label16" runat="server" Text="Bill No "></asp:Label>
                                            &nbsp;&nbsp;&nbsp;:
                                            <asp:Label ID="valuelblinvoicveno1" runat="server" Text=""></asp:Label></b>
                                            <br />
                                            
                                                <asp:Label ID="Label18" runat="server" Text="Date  "></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:
                                            <asp:Label ID="valuelblinvoicedate1" runat="server" Text=""></asp:Label><br />
                                            
                                            <asp:Label ID="lblPanNo1" runat="server" Text=""></asp:Label>
                                            <br />
                                          
                                            &nbsp;
                                            <asp:Label ID="lblTanNo" runat="server" Text=""></asp:Label><br />
                                            
                                            <asp:Label ID="lblServNo" runat="server" Text=""></asp:Label>
                                            <br />
                                           
                                                <asp:Label ID="Label26" runat="server" Text="Status"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:
                                            <asp:Label ID="Label27" runat="server" Text="PRIVATE LTD."></asp:Label><br />

                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table border="1" cellspacing="0" style="font-size: 12px" width="100%" id="Table3">
                        <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                            <HeaderTemplate>
                                <tr>
                                    <td class="white_bg" style="font-size: 12px" width="3%">
                                        <strong>S.No.</strong>
                                    </td>
                                    <td style="font-size: 12px" width="6%" align="center">
                                        <strong>Challan No.</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>Challan Date</strong>
                                    </td>
                                    <td style="font-size: 12px" width="5%">
                                        <strong>LR No</strong>
                                    </td>
                                  
                                  
                                    <td style="font-size: 12px" align="left" width="6%">
                                        <strong>Truck No</strong>
                                    </td>
                                    <td style="font-size: 12px" align="left" width="8%">
                                        <strong>Destination</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%" align="left">
                                        <strong>DSP QTY</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>Rate</strong>
                                    </td>
                                    <td style="font-size: 12px" width="7%">
                                        <strong>Freight</strong>
                                    </td>
                                    <td style="font-size: 12px" width="6%">
                                        <strong>U/L Rate</strong>
                                    </td>
                                    <td style="font-size: 12px" width="6%">
                                        <strong>U/L Amnt</strong>
                                    </td>
                                    <td style="font-size:12px" width="6%">
                                    <strong>TotalFrt</strong>
                                    </td>
                                                                   
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="white_bg" width="3%">
                                        <%#Container.ItemIndex+1 %>.
                                    </td>
                                    <td class="white_bg" width="6%">
                                        <%#Eval("Chln_NO")%>
                                    </td>
                                    <td class="white_bg" width="8%">
                                        <%#Convert.ToDateTime(Eval("Chln_Date")).ToString("dd-MM-yyyy")%>
                                    </td>
                                    <td class="white_bg" width="5%">
                                        <%#Eval("GR_No")%>
                                    </td>
                                   
                                    <td class="white_bg" width="6%" align="left">
                                        <%#Eval("Lorry_No")%>&nbsp;
                                    </td>
                                    <td class="white_bg" width="8%" align="center">
                                        <%#(Eval("Delvry_Place"))%>
                                    </td>
                                    <td class="white_bg" width="8%" align="center">
                                        <%#(Eval("Dsp_Qty"))%>
                                    </td>
                                    <td class="white_bg" width="8%" align="center">
                                        <%#Eval("Rate")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="white_bg" width="7%" align="right">
                                        <%#Eval("TotalFrt")%>
                                    </td>
                                    <td class="white_bg" width="6%" align="right">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="6%" align="right">
                                    <%#Eval("Wages_Amnt")%>
                                    </td>
                                    <td class="white_bg" width="6%" align="right">
                                    <%#Eval("TotalFreight")%>
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
                <td colspan="4" align="right">
                    <table border="1" cellspacing="0" style="font-size: 13px" width="100%" id="Table4">
                        <tr>
                            <td class="style5" style="font-size: 13px">
                            
                            </td>
                            <td style="font-size: 13px" width="6%" align="center">
                            </td>
                            <td style="font-size: 13px" width="8%">
                            </td>
                            <td style="font-size: 13px" width="5%">
                            </td>
                            <td style="font-size: 13px" width="6%">
                            </td>
                            <td style="font-size: 12px" align="center" width="8%">
                                <strong>TOTAL</strong>
                            </td>
                            <td style="font-size: 12px" align="right" width="8%">
                             <asp:Label ID="lblWeight" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td style="font-size: 13px" width="8%" align="right">
                                
                            </td>
                            <td style="font-size: 12px" width="7%" align="right">
                            <asp:Label ID="lblTotAmnt" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td style="font-size: 13px" width="6%" align="right">
                                
                            </td>
                            <td style="font-size: 12px" width="6%" align="right">
                            <asp:Label ID="lblulamnt" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td style="font-size: 12px" width="6%" align="right">
                            <asp:Label ID="lbltotfrt" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%" colspan="3">
                    <table width="100%" align="right" border="1" style="font-size: 12px;">
                        <tr>
                            <td colspan="5" align="left">
                                RS.&nbsp;&nbsp;<asp:Label ID="lblTowords1" runat="server" Font-Bold="True"></asp:Label>&nbsp;Only.
                            </td>
                        </tr>

                        <tr>
                            <td colspan="5" align="left" width="43%" style="font-size:13px">
                                <table>
                                <tr>
                                        <td width="80%">
                                          <table border="1" style="font-size:12px">
                                          <tr><td>Service Tax</td><td><asp:Label ID="lblServiceTax" runat="server" Font-Bold="True"></asp:Label></td>
                                          </tr>
                                          <tr><td>Swach Bharat Tax</td><td><asp:Label ID="lblSwachTax" runat="server" Font-Bold="True"></asp:Label></td>
                                          </tr>
                                          <tr><td>Krishi Kalyan Tax</td><td><asp:Label ID="lblKrishiTax" runat="server" Font-Bold="True"></asp:Label></td>
                                          </tr>
                                          </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="80%" align="left" colspan="2" style="font-size:12px">
                                          We shall be solely responsible for non-delivery of any consignment above and shall indemnify to the company &nbsp;<asp:Label ID="lblCompdec" runat="server" Text=""></asp:Label>&nbsp; with full cost of consignment, in case of non-delivery.
                                            <br />
                                         </td>
                                    </tr>
                                    
                                </table>
                            </td>
                            <%--<td width="16%" align="center" valign="top" colspan="2">
                                                    
</td>--%>
                            <td colspan="4" width="20%" align="right" valign="top">
                                <table width="100%">
                                    <tr style="border-bottom-style: solid; border-top-width: thin; border-bottom-width: thin;
                                        border-right-width: thin">
                                        <td align="left" width="25%" style="font-size:12px">
                                        Quantity and Freight rate verify<br />Bill Passed for Rs.
                                        </td>
                                        <td align="right" width="8%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="white_bg" style="font-size:13px" valign="bottom" colspan="3">
                                            
                                                <asp:Label ID="lblForComp" runat="server" Font-Bold="True"></asp:Label><br />
                                                <br /><br /><br />
                                                (Authorised Signatory)&nbsp;
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
