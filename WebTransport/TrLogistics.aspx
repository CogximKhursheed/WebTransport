<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrLogistics.aspx.cs" Inherits="WebTransport.TrLogistics" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
                
        .style4
        {
            height: 155px;
        }
        
        .style6
        {
            width: 100%;
        }
                
        .style7
        {
            width: 388px;
        }
                
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
                    <div id="print" style="font-size: 13px;">
                        <table cellpadding="1" cellspacing="0" width="800" border="1" style="font-family: Arial,Helvetica,sans-serif;">
                            <tr style="height: 120px; width: 20%">
                                <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                                    border-left-style: none; border-right-style: none">
                                    <div style="text-align: left; width: 140px; float: left;">
                                        <asp:Image ID="imgLogoShow" Width="140px" Height="90px" runat="server"></asp:Image>
                                    </div>
                                    <div id="header" runat="server" style="text-align: center; width: 650px; float: center;">
                                        <strong>
                                            <asp:Label ID="lblCompanyname" runat="server" Style="font-size: 18px;"></asp:Label><br />
                                        </strong>
                                        <asp:Label ID="lblCompDesc" runat="server" Style="font-size: 12px;"></asp:Label> 
                                        &nbsp; 
                                        <asp:Label ID="lblCompAdd1" runat="server"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblCompAdd2" runat="server"></asp:Label><br />
                                        <asp:Label ID="lblCompCity" runat="server"></asp:Label>&nbsp;&nbsp;
                                        <asp:Label ID="lblCompState" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblCompCityPin" runat="server"></asp:Label><br />
                                        <asp:Label ID="lblCompPhNo" runat="server"></asp:Label>&nbsp;
                                        <asp:Label ID="lblEmail" Text="" runat="server"></asp:Label>&nbsp;
                                        <asp:Label ID="lblFaxNo" Text="FAX No.:" runat="server"></asp:Label>
                                        <asp:Label ID="lblCompFaxNo" runat="server"></asp:Label><br />
                                        <asp:Label ID="lblTin" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;
                                        <asp:Label ID="lblCompTIN" runat="server"></asp:Label>&nbsp;<br />
                                         <asp:Label ID="lblCompGST" runat="server" Text="GSTIN NO:"></asp:Label>&nbsp;<asp:Label
                                            ID="lblCompGSTIN" runat="server"></asp:Label>&nbsp;&nbsp;
                                        <asp:Label ID="lbltxtPanNo" runat="server" Text="PAN NO. :"></asp:Label>&nbsp;<asp:Label
                                            ID="lblPanNo" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                                    border-left-style: none; border-right-style: none">
                                    <h3>
                                        <strong style="text-decoration: underline">
                                            <asp:Label ID="lblPrintHeadng" runat="server" Text="Goods Receipt"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblTypeOfGr" runat="server" Text="[To Pay GR]"></asp:Label>
                                        </strong>
                                    </h3>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table border="0" width="100%">
                                        <tr>
                                            <td style="width: 265px; text-align: left;">
                                                <table id="Table1" style="width: 99%">
                                                    <tr>
                                                        <td><asp:Label ID="lbltxtgrno" runat="server">GR. No.</asp:Label></td>
                                                        <td>:</td>
                                                        <td><b><asp:Label ID="lblGRno" runat="server"></asp:Label></b></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="lbltxtFromcity" runat="server">From City</asp:Label></td>
                                                        <td>:</td>
                                                        <td><b><asp:Label ID="lblFromCity" runat="server"></asp:Label></b></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="lblViaCity" runat="server">Via City</asp:Label></td>
                                                        <td>:</td>
                                                        <td><b><asp:Label ID="lblValueViaCity" runat="server"></asp:Label></b></td>
                                                    </tr>
                                                    <tr id="trShipNo" runat="server">
                                                        <td><asp:Label ID="lblNameShipmentno" runat="server">Shipment No.</asp:Label></td>
                                                        <td>:</td>
                                                        <td><b><asp:Label ID="lblShipmentNo" runat="server"></asp:Label></b></td>
                                                    </tr>
                                                    <tr id="trDinNo" runat="server">
                                                        <td><asp:Label ID="lblDinNoText" runat="server">DI No.</asp:Label></td>
                                                        <td>:</td>
                                                        <td><b><asp:Label ID="lblDinNo" runat="server"></asp:Label></b></td>
                                                    </tr>
                                                    <tr id="trOrderNo" runat="server">
                                                        <td><asp:Label ID="lblOrderNo" runat="server">Order No</asp:Label></td>
                                                        <td>:</td>
                                                        <td><b><asp:Label ID="lblOrderNoVal" runat="server"></asp:Label></b></td>
                                                    </tr>
                                                    <tr id="trCntrType" runat="server">
                                                        <td><asp:Label ID="lblNameContnrType" runat="server">Type</asp:Label></td>
                                                        <td>:</td>
                                                        <td><b><asp:Label ID="lblCntnrType" runat="server"></asp:Label></b></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 265px; text-align: left;">
                                                <table id="Table2" style="width: 99%">
                                                    <tr>
                                                        <td><asp:Label ID="lbltxtgrdate" Text="GR. Date" runat="server"></asp:Label></td>
                                                        <td>:</td>
                                                        <td><b><asp:Label ID="lblGrDate" runat="server"></asp:Label></b></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="lbltxttocity" runat="server">To City</asp:Label></td>
                                                        <td>:</td>
                                                        <td><b><asp:Label ID="lblToCity" runat="server"></asp:Label></b></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="lblConsName" runat="server">Consignor's Name</asp:Label></td>
                                                        <td>:</td>
                                                        <td><b><asp:Label ID="lblvalConsName" runat="server"></asp:Label></b></td>
                                                    </tr>
                                                    <tr id="trContainerNo" runat="server">
                                                        <td><asp:Label ID="lblNameContnrNo" Text="Container No.:" runat="server"></asp:Label></td>
                                                        <td>:</td>
                                                        <td><b><asp:Label ID="lblContainerNo" runat="server"></asp:Label></b></td>
                                                    </tr>
                                                    <tr id="trEGPNo" runat="server">
                                                        <td><asp:Label ID="lblEGPNo" runat="server">EGP No.</asp:Label></td>
                                                        <td>:</td>
                                                        <td><b><asp:Label ID="lblEGPNoval" runat="server"></asp:Label></b></td>
                                                    </tr>
                                                    <tr id="trFormNo" runat="server">
                                                        <td><asp:Label ID="lblFormNo" runat="server">Form No</asp:Label></td>
                                                        <td>:</td>
                                                        <td><b><asp:Label ID="lblFormNoVal" runat="server"></asp:Label></b></td>
                                                    </tr>
                                                    <tr id="trTotItem" runat="server">
                                                        <td><asp:Label ID="lblTotItem" runat="server">Goods Value &nbsp;:</asp:Label></td>
                                                        <td>:</td>
                                                        <td><b><asp:Label ID="lblTotItemValue" runat="server"></asp:Label></b></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 265px; text-align: left;">
                                                <table id="Table3" style="width: 99%">
                                                    <tr id="trLorry" runat="server">
                                                        <td><asp:Label ID="Label6" runat="server">Lorry No.</asp:Label></td>
                                                        <td>:</td>
                                                        <td><b><asp:Label ID="lblLorryNo" runat="server"></asp:Label></b></td>
                                                    </tr>
                                                    <tr id="trDeliveryPlace" runat="server">
                                                        <td><asp:Label ID="lbltxtdelvryPlace" runat="server">Delivery Place</asp:Label></td>
                                                        <td>:</td>
                                                        <td><b><asp:Label ID="lblDelvryPlace" runat="server"></asp:Label></b></td>
                                                    </tr>
                                                    <tr id="trsize" runat="server">
                                                        <td><asp:Label ID="lblNameCntnrSize" runat="server">Size</asp:Label></td>
                                                        <td>:</td>
                                                        <td><b><asp:Label ID="lblContainerSize" runat="server"></asp:Label></b></td>
                                                    </tr>
                                                    <tr id="trsealno" runat="server">
                                                        <td><asp:Label ID="lblNameSealNo" runat="server">Seal No.</asp:Label></td>
                                                        <td>:</td>
                                                        <td><b><asp:Label ID="lblSealNo" runat="server"></asp:Label></b></td>
                                                    </tr>
                                                    <tr id="trRefNo" runat="server">
                                                        <td><asp:Label ID="lblRefNo" runat="server">Ref No.</asp:Label></td>
                                                        <td>:</td>
                                                        <td><b><asp:Label ID="lblrefnoval" runat="server"></asp:Label></b></td>
                                                    </tr>
                                                    <tr id="trAgent" runat="server">
                                                        <td><asp:Label ID="lbltxtagent" runat="server">Agent</asp:Label></td>
                                                        <td>:</td>
                                                        <td><b><asp:Label ID="lblAgent" runat="server"></asp:Label></b></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%;" valign="top">
                                                <table border="0" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                                    <tr>
                                                        <td colspan="2" style="border-bottom: 1px solid #484848;">
                                                            <strong>Sender's Details:</strong>
                                                        </td>
                                                    </tr>
                                                    <tr id="trConsigneeName" runat="server">
                                                        <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                                            <asp:Label ID="Label1" runat="server">Name</asp:Label>
                                                        </td>
                                                        <td style="font-size: 13px; border-right-style: none">
                                                            <b>
                                                                <asp:Label ID="lblConsigeeName" runat="server"></asp:Label>
                                                                <b />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                                            <asp:Label ID="Label2" runat="server">Address</asp:Label>
                                                        </td>
                                                        <td style="font-size: 13px; border-right-style: none">
                                                            <b>
                                                                <asp:Label ID="lblConsigneeAddress" runat="server"></asp:Label></b>
                                                        </td>
                                                    </tr>
                                                    <tr id="trTINConsignee" runat="server">
                                                        <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                                            <asp:Label ID="Label5" runat="server" Text="Tin"></asp:Label>
                                                        </td>
                                                        <td style="font-size: 13px; border-right-style: none">
                                                            <b>
                                                                <asp:Label ID="lblConsigneeTin" Text="" runat="server"></asp:Label></b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                                            <asp:Label ID="lblPrtyGST" runat="server" Text="GSTIN NO"></asp:Label>
                                                        </td>
                                                        <td style="font-size: 13px; border-right-style: none">
                                                            <b>
                                                                <asp:Label ID="lblPrtyGSTIN" Text="" runat="server"></asp:Label></b>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 50%;" valign="top">
                                                <table border="0" width="100%" class="white_bg">
                                                    <tr>
                                                        <td colspan="2" style="border-bottom: 1px solid #484848; height: 10px;">
                                                            <strong>Receiver's Details:</strong>
                                                        </td>
                                                    </tr>
                                                    <tr id="trConsignorName" runat="server">
                                                        <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                                            <asp:Label ID="Label3" runat="server">Name</asp:Label>
                                                        </td>
                                                        <td style="font-size: 13px; border-right-style: none">
                                                            <b>
                                                                <asp:Label ID="lblConsignorName" runat="server"></asp:Label></b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                                            <asp:Label ID="Label4" runat="server">Address</asp:Label>
                                                        </td>
                                                        <td style="font-size: 13px; border-right-style: none">
                                                            <b>
                                                                <asp:Label ID="lblConsignorAddress" runat="server"></asp:Label></b>
                                                        </td>
                                                    </tr>
                                                    <tr id="trTINConsigner" runat="server">
                                                        <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                                            <asp:Label ID="lblPrtyTinTxt" runat="server" Text="Tin"></asp:Label>
                                                        </td>
                                                        <td style="font-size: 13px; border-right-style: none">
                                                            <b>
                                                                <asp:Label ID="lblConsignorTin" Text="" runat="server"></asp:Label></b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                                            <asp:Label ID="lblConsignerGSTINHead" runat="server" Text="GSTIN NO"></asp:Label>
                                                        </td>
                                                        <td style="font-size: 13px; border-right-style: none">
                                                            <b>
                                                                <asp:Label ID="lblConsignerGSTINValue" Text="" runat="server"></asp:Label></b>
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
                                    <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table1">
                                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                            <HeaderTemplate>
                                                <tr>
                                                    <td class="white_bg" style="font-size: 12px" width="10%">
                                                        <strong>S.No.</strong>
                                                    </td>
                                                    <td style="font-size: 12px" width="20%">
                                                        <strong>Item Name</strong>
                                                    </td>
                                                    <td style="font-size: 12px" width="10%">
                                                        <strong>Unit Name</strong>
                                                    </td>
                                                    <td style="font-size: 12px" width="10%">
                                                        <strong>Quantity</strong>
                                                    </td>
                                                    <td style="font-size: 12px" width="10%">
                                                        <strong>Weight</strong>
                                                    </td>
                                                    <div id="HideGrhdr" runat="server">
                                                        <td style="font-size: 12px" align="left" width="10%">
                                                            <strong>Frght Rate</strong>
                                                        </td>
                                                        <td style="font-size: 12px" align="left" width="10%">
                                                            <strong>Amount</strong>
                                                        </td>
                                                    </div>
                                                    <td style="font-size: 12px" width="20%">
                                                        <strong>Detail</strong>
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="white_bg" width="10%">
                                                        <%#Container.ItemIndex+1 %>.
                                                    </td>
                                                    <td class="white_bg" width="30%">
                                                        <%#Eval("Item_Modl")%>
                                                    </td>
                                                    <td class="white_bg" width="15%">
                                                        <%#Eval("UOM_Name")%>
                                                    </td>
                                                    <td class="white_bg" width="15%">
                                                        <%#Eval("Qty")%>
                                                    </td>
                                                    <td class="white_bg" width="15%">
                                                        <%#String.Format("{0:0.000}", Convert.ToDouble(Eval("Tot_Weght")))%>
                                                    </td>
                                                    <div id="HideGritem" runat="server">
                                                        <td class="white_bg" width="15%" align="left">
                                                            <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Item_Rate")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td class="white_bg" width="15%" align="left">
                                                            <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </div>
                                                    <td class="white_bg" width="15%" align="left">
                                                        <%#(Eval("Detail"))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <tr>
                                                    <td class="white_bg" width="10%">
                                                        &nbsp;
                                                    </td>
                                                    <td class="white_bg" width="30%">
                                                        &nbsp;
                                                    </td>
                                                    <td class="white_bg" width="15%">
                                                        <asp:Label ID="lblFTTot" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="white_bg" width="15%">
                                                        <asp:Label ID="lblFTQty" Font-Bold="true" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="white_bg" width="15%">
                                                        <asp:Label ID="lblFTtotalWeight" Font-Bold="true" runat="server"></asp:Label>
                                                    </td>
                                                    <div id="hidfooterdetl" runat="server">
                                                        <td class="white_bg" width="15%" align="left">
                                                        </td>
                                                        <td class="white_bg" width="15%" align="left">
                                                            <asp:Label ID="lblFTTotalAmnt" Font-Bold="true" runat="server"></asp:Label>
                                                        </td>
                                                    </div>
                                                    <td class="white_bg" width="15%" align="right">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table style="height: 190px; width: 100%">
                                        <tr>
                                            <td style="text-align: left; width: 470px;">
                                                <table width="100%">
                                                <tr>
                                                   </tr>
                                                    <tr>
                                                        <td>
                                                            
                                                        </td>
                                                    </tr>
                                                   <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                   </tr>
                                                   <tr>
                                                   <td     >&nbsp;</td>
                                                   </tr>
                                                </table>
                                            </td>
                                            <td style="text-align: left; width: 200px;">
                                            <table id="lst" runat="server" style="float: right; width: 175px;">
                                                    <tr id="cart" runat="server">
                                                 <td >
                                                            <asp:Label ID="lblCart" runat="server" Text="Cartage" Font-Size="13px" valign="right"></asp:Label></td>
                                                        <td align="right">
                                                        <asp:Label ID="lblCartage" runat="server"  Font-Size="13px" valign="right"></asp:Label>
                                                        </td>
                                                </tr>
                                                    <tr id="Comis" runat="server">
                                                      <td>
                                                            <asp:Label ID="lblComis" runat="server" Text="Commission" Font-Size="13px" valign="right"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblComiss" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="Unloding" runat="server">
                                                     <td>
                                                           <asp:Label ID="lblUnloading" runat="server" Text="Unloading" Font-Size="13px" valign="right"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblunchrg" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>    
                                                    </tr>
                                                    <tr id="Delchrg" runat="server">
                                                       <td >
                                                            <asp:Label ID="lblDelChrg" runat="server" Text="Delivery Chrg" Font-Size="13px" valign="right"></asp:Label>
                                                            </td>
                                                        <td align="right">
                                                        <asp:Label ID="lbldlvchrg" runat="server"  Font-Size="13px" valign="right"></asp:Label>
                                                        </td>
                                                     
                                                    </tr>
                                                    <tr id="SGST" runat="server">
                                                     <td>
                                                         <asp:Label ID="lblSGST" runat="server" Text="SGST" Font-Size="13px" valign="right"></asp:Label>
                                                         &nbsp;
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSGSTAmnt" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    <td>&nbsp;</td>
                                                      <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                    <td>&nbsp;</td>
                                                      <td>&nbsp;</td>
                                                    </tr>
                                                     <tr>
                                                    <td>&nbsp;</td>
                                                      <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                    <td>&nbsp;</td>
                                                      <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                    <td>&nbsp;</td>
                                                      <td>&nbsp;</td>
                                                    </tr>
                                            </table>
                                            </td>
                                            <td style="text-align: left; width: 230px;">
                                                <table id="lstInfoDiv" runat="server" style="float: right;" VAlign="top">
                                                    <tr id="Surcharg" runat="server">
                                                        <td >
                                                            <asp:Label ID="lblSurchar" runat="server" Text="Surcharge" Font-Size="13px" valign="right"></asp:Label></td>
                                                        <td align="right">
                                                        <asp:Label ID="lblSurcharg" runat="server"  Font-Size="13px" valign="right"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="Bilty" runat="server">
                                                    <td>
                                                        <asp:Label ID="lblbilte" runat="server" Text="Bilty" Font-Size="13px" valign="right"></asp:Label></td>
                                                        <td align="right">
                                                        <asp:Label ID="lblBilt" runat="server"  Font-Size="13px" valign="right"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="colChrg" runat="server">
                                                    <td  style="width:100px" >
                                                           <asp:Label ID="lblcollchrg" runat="server" Text="Collection Chrg" Font-Size="13px" valign="right"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblcolchrg" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                        </tr>
                                                    <tr id="SubTot" runat="server">
                                                     <td>
                                                           <asp:Label ID="lblSubTot" runat="server" Text="Freight Total" Font-Size="13px" valign="right"></asp:Label>&nbsp;
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSubTotal" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="CGST" runat="server">
                                                     <td>
                                                     <asp:Label ID="lblCGST" runat="server" Text="CGST" Font-Size="13px" valign="right">
                                                     </asp:Label>
                                                           
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblCGSTAmnt" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="advamnt" runat="server">
                                                    <td>
                                                         
                                                            <asp:Label ID="lblcommission" runat="server" Text="Adv. Amnt" Font-Size="13px" valign="right"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lbladvamnt" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="DieselAmnt" runat="server">
                                                        <td>
                                                            
                                                            <asp:Label ID="lblbilty" runat="server" Font-Size="13px" valign="right">Diesel Amnt</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="valuelblDieselAmnt" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="BC" runat="server"> 
                                                        <td>
                                                            
                                                            <asp:Label ID="lblsurcharge" runat="server" Font-Size="13px" valign="right">B. C.</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="valuelblBc" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                              <asp:Label ID="lblround" runat="server"   Text="Round Amnt" Font-Size="13px" valign="right"></asp:Label></td>
                                                        <td align="right">
                                                             <asp:Label ID="lblRoundAmnt" runat="server"   Font-Size="13px" valign="right"></asp:Label></td>
                                                    </tr>
                                                     <tr>
                                                    <td><asp:Label ID="lblNetAmnt" runat="server"  Font-Bold="true" Text="Freight Balance" Font-Size="13px" valign="right"></asp:Label></td>
                                                      <td align="right"><asp:Label ID="lblNetAmount" runat="server" Font-Size="13px"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <%--<asp:Label ID="lblFixRemark" Font-Size="14px" Font-Bold="true" Text="Risk By : Owner Risk" runat="server"></asp:Label>--%>
                                                <asp:Label ID="lblremark" runat="server" valign="right"></asp:Label>
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
                                                        <td align="left" class="white_bg" style="font-size: 13px; text-align: justify; padding-right: 20px;"
                                                            width="99%" colspan="3">
                                                            <asp:Label ID="lblTerms" Font-Size="10px" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:Label ID="lblterms1" Font-Size="11px" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" class="white_bg" style="font-size: 13px" width="30%">
                                                        </td>
                                                        <td align="right" class="white_bg" style="font-size: 13px" width="10%">
                                                        </td>
                                                        <td align="right" class="white_bg" style="font-size: 13px" width="30%">
                                                            <b>
                                                                <asp:Label ID="lblCompname" runat="server"></asp:Label></b>
                                                        </td>
                                                    </tr>
                                                    <%--<tr>
                                                        <td align="left" class="white_bg" style="font-size: 13px" width="50%">
                                                            <br />
                                                            <br />
                                                            <br />
                                                            <br />
                                                            <b>Customer Signature</b>&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td align="right" class="white_bg" style="font-size: 13px" valign="top" width="50%">
                                                            <br />
                                                            <b>
                                                                <asp:Label ID="lblCompname" runat="server"></asp:Label><br />
                                                                <br />
                                                                <br />
                                                                Authorised Signatory&nbsp;</b>
                                                        </td>
                                                    </tr>--%>
                                                    <%--<tr>
                                                    <td align="left" class="white_bg" style="font-size: 13px" >
                                                            <b><asp:Label ID="lblTerm" Text="Terms&Condition :" runat="server"></asp:Label></b>
                                                            <asp:Label ID="lblTerms" runat="server"></asp:Label>
                                                    </td>
                                                   
                                                    </tr>--%>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
          <asp:HiddenField ID="hidPages" runat="server" />
           <asp:HiddenField ID="hidHeadIdno" runat="server" />
    </div>
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = "";
            var Pages = "1";
            Pages = document.getElementById("<%=hidPages.ClientID%>").value;
            var prtContent3 = "<p style='page-break-before: always'></p>";
            for (i = 0; i < Pages; i++) {
                prtContent = prtContent + "<table width='100%' border='0'></table>";
                if (Pages != 1) {
                    prtContent = prtContent + "<tr><td><strong>" + ((i == 1) ? "[Office Copy]" : (i == 2) ? "[Consignor Copy]" : (i == 3) ? "[Consignee Copy]" : "[Driver Copy]") + "</strong></td></tr>";
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
