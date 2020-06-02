<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintOMCargo.aspx.cs" Inherits="WebTransport.PrintOMCargo1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="print" style="font-size: 13px; background-color:#ffffff;">
        <table cellpadding="1" cellspacing="0" width="1050" border="1" style="font-family: Arial,Helvetica,sans-serif;">
            <tr style="height: 80px; width: 20%">
                <td align="center" class="white_bg" valign="top" colspan="3" style="font-size: 14px;
                    border-left-style: none; border-right-style: none">
                    <%--  <div style="text-align:left;Width:140px; float:left;">
                                        
                                     </div>--%>
                    <div style="float: left; text-align: left; width: 25%; font-size: 12px;">
                        <asp:Label ID="lblTin" runat="server" Text="Reg. No. :"></asp:Label>&nbsp;<asp:Label
                            ID="lblCompTIN" runat="server"></asp:Label><br />
                        <asp:Label ID="lbltxtPanNo" runat="server" Text="PAN NO. :"></asp:Label>
                        <asp:Label ID="lblPanNo" runat="server"></asp:Label>
                    </div>
                    <div style="width: 50%;">
                       <strong>
                       <asp:Label ID="lblCompanyname" runat="server" Style="font-size: 18px;"></asp:Label><br />
                       </strong>                       
                        <asp:Label ID="lblCompDesc" runat="server" Style="font-size: 12px;"></asp:Label> 
                        <asp:Label ID="lblCompAdd1" runat="server" Style="font-size: 12px;"></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblCompAdd2" runat="server" Style="font-size: 12px;"></asp:Label>
                        <asp:Label ID="lblCompCity" runat="server" Style="font-size: 12px;"></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="lblCompState" runat="server" Style="font-size: 12px;"></asp:Label>&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblCompCityPin" runat="server" Style="font-size: 12px;"></asp:Label><br />
                        <asp:Label ID="lblCompPhNo" runat="server" Style="font-size: 12px;"></asp:Label>&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblCompFaxNo" runat="server" Style="font-size: 12px;"></asp:Label><br />
                    </div>
                </td>
            </tr>
        </table>
        <table cellpadding="1" cellspacing="0" width="1050" border="1" style="font-family: Arial,Helvetica,sans-serif;">
            <tr>
                <td colspan="6">
                    <table border="1" width="100%">
                        <tr style="border-bottom: solid 1px #484848; font-size: 12px;">
                            <td style="border-right: solid 1px #484848; width: 25%;">
                                <b>
                                    <asp:Label ID="lblHAtOwn" Text="AT OWNER'S RISK" runat="server"></asp:Label></b>
                            </td>
                            <td style="border-right: solid 1px #484848; width: 10%; display:none;">
                                <b>
                                    <asp:Label ID="lblHMod" Text="MODVAT COPY" runat="server"></asp:Label></b>
                            </td>
                            <td style="border-right: solid 1px #484848; width: 12%;">
                                <b>
                                    <asp:Label ID="lblHCNo" Text="CN. No." runat="server"></asp:Label></b>
                                <asp:Label ID="lblGRno" Style="font-size: 20px;" runat="server"></asp:Label>
                            </td>
                            <td style="border-right: solid 1px #484848; width: 13%;">
                                <b>
                                    <asp:Label ID="lblBooking" Text="BOOKING MODE" runat="server"></asp:Label></b>
                            </td>
                            <td style="border-right: solid 1px #484848; width: 10%;">
                                <b>
                                    <asp:Label ID="lblHDate" Text="DATE" runat="server"></asp:Label></b>
                            </td>
                            <td style="width: 10%; display:none;">
                                <b>
                                    <asp:Label ID="lblHTime" Text="TIME" runat="server"></asp:Label></b>
                            </td>
                            <td style="border-right: solid 1px #484848; width: 20%;display:none;">
                                <b>
                                    <asp:Label ID="lblhDlyIns" Text="DLY. INSTRUCTIONS" runat="server"></asp:Label></b>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="white_bg" valign="top" style="font-size: 12px; border-right: solid 1px #484848;
                                width: 25%; border-bottom: 1px solid #484848;">
                                <asp:Label ID="lblOWNER" runat="server"></asp:Label>
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 12px; border-right: solid 1px #484848;
                                width: 10%; border-bottom: 1px solid #484848;display:none;">
                                <asp:Label ID="lblModvatcpy" runat="server"></asp:Label>
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 12px; border-right: solid 1px #484848;
                                width: 10%; border-bottom: 1px solid #484848;">
                                <b>
                                    <asp:Label ID="Label12" Text="Mn. No." runat="server"></asp:Label></b>
                                <asp:Label ID="lblMNo" Style="font-size: 20px;" runat="server"></asp:Label>
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 12px; border-right: solid 1px #484848;
                                width: 15%; border-bottom: 1px solid #484848;">
                                <asp:Label ID="lblTranTypeP" Text="By Lorry" runat="server"></asp:Label>
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 12px; border-right: solid 1px #484848;
                                width: 10%; border-bottom: 1px solid #484848;">
                                <asp:Label ID="lblGrDate" runat="server"></asp:Label>
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 12px; width: 10%;
                                border-bottom: 1px solid #484848;display:none;">
                                <asp:Label ID="lblGRTime" runat="server"></asp:Label>
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 12px; border-right: solid 1px #484848;
                                width: 20%; border-bottom: 1px solid #484848;display:none;">
                                <asp:Label ID="lblDlyIns" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table cellpadding="1" cellspacing="0" width="1050" border="1" style="font-family: Arial,Helvetica,sans-serif;">
            <tr>
                <td style="width: 38%;" valign="top">
                    <table border="0" width="100%" class="white_bg">
                        <tr>
                            <td>
                                <strong>Consignor:</strong>
                            </td>
                            <td style="font-size: 13px;" colspan="6">
                                <asp:Label ID="lblConsignorName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td style="font-size: 13px;" colspan="4">
                                <asp:Label ID="lblConsignorAddress" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr id="trCS" runat="server">
                            <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                <asp:Label ID="lblCST" runat="server">C.S.T. No.:</asp:Label>
                            </td>
                            <td style="font-size: 13px; border-right-style: none">
                                <asp:Label ID="lblCSTP" runat="server"></asp:Label>
                            </td>
                            <td colspan="2">
                            </td>
                            <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                <asp:Label ID="lblPH" runat="server">Ph.:</asp:Label>
                            </td>
                            <td style="font-size: 13px; border-right-style: none">
                                <asp:Label ID="lblPhP" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trLS" runat="server">
                            <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                <asp:Label ID="lblLST" runat="server">L.S.T. No.:</asp:Label>
                            </td>
                            <td style="font-size: 13px; border-right-style: none">
                                <asp:Label ID="lblLSTP" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trConsigneeName" runat="server">
                            <td colspan="2" style="font-size: 13px; border:1px solid #000000; width: 25%;">
                                <asp:Label ID="Label1" runat="server">From:</asp:Label><asp:Label ID="lblFromCity"
                                    Style="float: right;" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table border="1" width="100%" class="white_bg">
                        <tr style="font-size: 14px; text-align: center; height:25px;">
                            <td colspan="2">
                                <strong>CONSIGNMENT DETAILS</strong>
                            </td>
                        </tr>
                    </table>
                    <table border="1" width="100%" class="white_bg" style="border-bottom-style: none;">
                        <tr style="font-size: 12px; text-align: center;">
                            <td style="width: 30%">
                                <strong>No. of Pkgs.</strong>
                            </td>
                            <td style="width: 20%">
                                <strong>Type Of Packing</strong>
                            </td>
                            <td colspan="2">
                                <strong>Item Desciption</strong>
                            </td>
                        </tr>
                        <tr style="font-size: 12px; text-align: center;">
                            <td style="width: 30%">
                                <strong>Figures</strong>
                            </td>
                            <td style="width: 20%; border:1px solid #000000;">
                                <asp:Label ID="lblUnitP" runat="server"></asp:Label>
                            </td>
                            <td colspan="2" style="border-bottom-style: none;border:1px solid #000000;">
                                <asp:Label ID="lblDetailP" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-size: 12px; text-align: center;">
                            <td style="width: 30%">
                                <asp:Label ID="lblNoPckg" runat="server"></asp:Label>
                                <br />
                            </td>
                            <td style="width: 20%; border-top-style: none;">
                            </td>
                            <td colspan="2" style="border-top-style: none;">
                            </td>
                        </tr>
                        <tr style="font-size: 12px; height:25px;">
                            <td style="width: 30%; text-align: center;">
                                <strong>In words</strong>
                            </td>
                            <td style="text-align: left; border-right-style: none;">
                                <strong>Invoice No.(s)</strong>
                            </td>
                            <td colspan="2" style="text-align: right; border-left-style: none;">
                                <asp:Label Style="text-align: right;" ID="lblRefNoP" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-size: 12px; height:25px;">
                            <td style="width: 30%; border-bottom-style: none; border-top-style: none;">
                            </td>
                            <td style="text-align: left;">
                                <strong>Date</strong>
                            </td>
                            <td colspan="2" style="text-align: right; border-left-style: none;">
                                <asp:Label Style="text-align: right;" ID="lblRefDateP" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-size: 12px;height:25px;">
                            <td style="width: 30%; border-top-style: none;">
                            </td>
                            <td colspan="3">
                                <strong style="text-align: left;">Total Invoice Value</strong><asp:Label Style="float: right;"
                                    ID="lblTotP" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-size: 12px;height:25px;">
                            <td style="width: 30%; text-align: center;border:1px solid #000000;">
                                <strong>Part No.</strong>
                            </td>
                            <td colspan="3" style="text-align: left;">
                                
                            </td>
                        </tr>
                        <tr style="font-size: 12px;height:25px;">
                            <td style="width: 30%;  border:1px solid #000000; text-align: center;">
                               <strong>Quantity</strong>
                            </td>                            
                            <td colspan="3" style="text-align: right; border:1px solid #000000; border-left-style:none;">
                                <asp:Label ID="lblQtP" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-size: 11px; display:none;">
                            <td style="width: 30%; text-align: center;">
                                <strong>Total CFT</strong>
                            </td>
                            <td style="width: 10%; text-align: center;">
                                <strong>Per CFT<br />
                                    (Kgs.)</strong>
                            </td>
                            <td style="width: 20%; text-align: center;">
                                <strong>Actual Weight<br />
                                    (Kgs.)</strong>
                            </td>
                            <td style="width: 40%; text-align: center;">
                                <strong>Charged Weight(Kgs.)</strong>
                            </td>
                        </tr>
                        <tr style="font-size: 12px; height: 30px; display:none;">
                            <td style="width: 30%; text-align: center; border-bottom-style: none;">
                                <asp:Label ID="lblTCFT" runat="server"></asp:Label>
                            </td>
                            <td style="width: 10%; text-align: center; border-bottom-style: none;">
                                <asp:Label ID="lblPCFT" runat="server"></asp:Label>
                            </td>
                            <td style="width: 30%; text-align: center; border-bottom-style: none;">
                                <asp:Label ID="lblActWght" runat="server"></asp:Label>
                            </td>
                            <td style="width: 30%; text-align: center; border-bottom-style: none;">
                                <asp:Label ID="lblChWght" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 38%;" valign="top">
                    <table border="0" width="100%" class="white_bg">
                        <tr>
                            <td>
                                <strong>Consignee:</strong>
                            </td>
                            <td style="font-size: 13px;" colspan="6">
                                <asp:Label ID="lblConsigeeName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td style="font-size: 13px;" colspan="4">
                                <asp:Label ID="lblConsigneeAddress" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr id="trCS1" runat="server">
                            <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                <asp:Label ID="lblCST1" runat="server">C.S.T. No.:</asp:Label>
                            </td>
                            <td style="font-size: 13px; border-right-style: none">
                                <asp:Label ID="lblCSTP1" runat="server"></asp:Label>
                            </td>
                            <td colspan="2">
                            </td>
                            <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                <asp:Label ID="lblPH1" runat="server">Ph.:</asp:Label>
                            </td>
                            <td style="font-size: 13px; border-right-style: none">
                                <asp:Label ID="lblPhP1" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trLS1" runat="server">
                            <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                <asp:Label ID="lblLST1" runat="server">L.S.T. No.:</asp:Label>
                            </td>
                            <td colspan="2" style="font-size: 13px; border-right-style: none">
                                <asp:Label ID="lblLSTP1" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trConsignorName" runat="server">
                            <td colspan="2" style="font-size: 13px; border:1px solid #000000; width: 25%;">
                                <asp:Label ID="Label3" runat="server">To:</asp:Label><asp:Label ID="lblToCity" Style="float: right;"
                                    runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table border="1" width="100%" class="white_bg">
                        <tr style="font-size: 14px; text-align: center;">
                            <td colspan="2">
                                <strong>PAYMENT TERMS</strong>
                            </td>
                        </tr>
                        <tr style="font-size: 12px; text-align: center;">
                            <td>
                                <strong>Freight Mode</strong>
                            </td>
                            <td>
                                <strong>Billing Station</strong>
                            </td>
                        </tr>
                        <tr style="font-size: 12px; text-align: left;">
                            <td style="border-bottom-style: none; border-left-style: none; border-right-style: none;
                                border-top-style: none;">
                                To be Billed
                            </td>
                            <td style="border-bottom-style: none; border-left-style: none; border-right-style: none;
                                border-top-style: none;">
                                <asp:CheckBox ID="chkToBeld" runat="server" />
                            </td>
                        </tr>
                        <tr style="font-size: 12px; text-align: left;">
                            <td style="border-bottom-style: none; border-left-style: none; border-right-style: none;
                                border-top-style: none;">
                                To Pay
                            </td>
                            <td style="border-bottom-style: none; border-left-style: none; border-right-style: none;
                                border-top-style: none;">
                                <asp:CheckBox ID="chkToPay" runat="server" />
                            </td>
                        </tr>
                        <tr style="font-size: 12px; text-align: left;">
                            <td style="border-bottom-style: none; border-left-style: none; border-right-style: none;
                                border-top-style: none;">
                                Paid
                            </td>
                            <td style="border-bottom-style: none; border-left-style: none; border-right-style: none;
                                border-top-style: none;">
                                <asp:CheckBox ID="chkPaid" runat="server" />
                            </td>
                        </tr>
                        <tr style="font-size: 12px; text-align: left;">
                            <td style="border-bottom-style: none; border-left-style: none; border-right-style: none;
                                border-top-style: none;">
                                <asp:Label ID="Label11" runat="server" Text="Label">If paid by Cash/Cheque specify amount</asp:Label>
                            </td>
                        </tr>
                        <tr style="font-size: 12px; text-align: left;">
                            <td style="border-bottom-style: none; border-left-style: none; border-right-style: none;
                                border-top-style: none;">
                                Cheque No/Cash :<asp:Label ID="lblCheP" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-size: 12px; text-align: left;">
                            <td style="border-bottom-style: none; border-left-style: none; border-right-style: none;
                                border-top-style: none;">
                                Date :<asp:Label ID="lblInsDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table border="0" width="100%" class="white_bg">
                        <tr style="font-size: 14px; text-align: center;">
                            <td colspan="2">
                                <strong>Consignment Acknowledgment by Consignee<br />
                                    Received the shipment as per details contained hee in.</strong>
                            </td>
                        </tr>                        
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr style="font-size: 12px; text-align: center;">
                            <td valign="baseline" style="border-bottom-style: none; border-left-style: none;
                                border-right-style: none; border-top-style: none;">
                                <asp:Label ID="Label22" runat="server" Text="Label">Signature   _________________</asp:Label>
                            </td>
                        </tr>
                        <tr style="font-size: 12px; text-align: center;">
                            <td valign="baseline" style="border-bottom-style: none; border-left-style: none;
                                border-right-style: none; border-top-style: none;">
                                <asp:Label ID="Label20" runat="server" Text="Label">Seal of the Company with date</asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 24%;" valign="top">
                    <table border="1" width="100%" class="white_bg">
                        <tr style="font-size: 14px;">
                            <td width="50%">
                                <strong style="text-align: left;">FREIGHT DETAILS</strong>
                            </td>
                            <td>
                                <strong style="text-align: left;">Rs.</strong>
                            </td>
                            <td>
                                <strong style="text-align: left;">P.</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="labRate" Style="font-size: 12px;" runat="server" valign="right">Rate</asp:Label>
                            </td>
                            <td style="text-align: right;">
                                <asp:Label ID="lblRateRs" Style="font-size: 12px;" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblRateP" Style="font-size: 12px;" runat="server" valign="right"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="labFreight" Style="font-size: 12px;" runat="server" valign="right">Gross Amount</asp:Label>
                            </td>
                            <td style="text-align: right;">
                                <asp:Label ID="labFreightRs" Style="font-size: 12px;" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labFreightP" Style="font-size: 12px;" runat="server" valign="right"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="labST" Style="font-size: 12px;" runat="server" valign="right">S. T. Charges</asp:Label>
                            </td>
                            <td style="text-align: right;">
                                <asp:Label ID="labSTR" Style="font-size: 12px;" runat="server">0.00</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labSTP" Style="font-size: 12px;" runat="server" valign="right"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="labSur" Style="font-size: 12px;" runat="server" valign="right">Surcharge&nbsp;&nbsp;&nbsp;@</asp:Label>
                            </td>
                            <td style="text-align: right;">
                                <asp:Label ID="labSurR" Style="font-size: 12px;" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labSurP" Style="font-size: 12px;" runat="server" valign="right"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="labHam" Style="font-size: 12px;" runat="server" valign="right"></asp:Label>
                            </td>
                            <td style="text-align: right;">
                                <asp:Label ID="labHamR" Style="font-size: 12px;" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labHamP" Style="font-size: 12px;" runat="server" valign="right"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="labFOV" Style="font-size: 12px;" runat="server" valign="right"></asp:Label>
                            </td>
                            <td style="text-align: right;">
                                <asp:Label ID="labFOVR" Style="font-size: 12px;" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labFOVP" Style="font-size: 12px;" runat="server" valign="right"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="labCollCha" Style="font-size: 12px;" runat="server" valign="right"></asp:Label>
                            </td>
                            <td style="text-align: right;">
                                <asp:Label ID="labCollChaR" Style="font-size: 12px;" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labCollChaP" Style="font-size: 12px;" runat="server" valign="right"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="labDelCha" Style="font-size: 12px;" runat="server" valign="right"></asp:Label>
                            </td>
                            <td style="text-align: right;">
                                <asp:Label ID="labDelChaR" Style="font-size: 12px;" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labDelChaP" Style="font-size: 12px;" runat="server" valign="right"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="labOctroi" Style="font-size: 12px;" runat="server" valign="right"></asp:Label>
                            </td>
                            <td style="text-align: right;">
                                <asp:Label ID="labOctroiR" Style="font-size: 12px;" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labOctroiP" Style="font-size: 12px;" runat="server" valign="right"></asp:Label>
                            </td>
                        </tr>                        
                        <tr>
                            <td>
                                <asp:Label ID="labDemCha" Style="font-size: 12px;" runat="server" valign="right"></asp:Label>
                            </td>
                            <td style="text-align: right;">
                                <asp:Label ID="labDemChaR" Style="font-size: 12px;" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labDemChaP" Style="font-size: 12px;" runat="server" valign="right"></asp:Label>
                            </td>
                        </tr>                        
                        <tr>
                            <td>
                                <asp:Label ID="labOther" Style="font-size: 12px;" runat="server" valign="right">Others</asp:Label>
                            </td>
                            <td style="text-align: right;">
                                <asp:Label ID="labOtherR" Style="font-size: 12px;" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labOtherP" Style="font-size: 12px;" runat="server" valign="right"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>
                                    <asp:Label ID="labTotal" Style="font-size: 12px;" runat="server" valign="right">Total</asp:Label></b>
                            </td>
                            <td style="text-align: right;">
                                <asp:Label ID="labTotP" Style="font-size: 12px;" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label15" Style="font-size: 12px;" runat="server" valign="right"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>
                                    <asp:Label ID="labSerTax" Style="font-size: 12px;" runat="server" valign="right">Service Tax</asp:Label></b>
                            </td>
                            <td style="text-align: right;">
                                <asp:Label ID="labSerTaxR" Style="font-size: 12px;" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labSerTaxP" Style="font-size: 12px;" runat="server" valign="right"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>
                                    <asp:Label ID="labGTot" Style="font-size: 12px;" runat="server" valign="right">G. Total</asp:Label></b>
                            </td>
                            <td style="text-align: right;">
                                <asp:Label ID="labGTotR" Style="font-size: 12px;" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labGTotP" Style="font-size: 12px;" runat="server" valign="right"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center; border-bottom-style: none;">
                                <b>
                                    <asp:Label ID="Label21" Style="font-size: 12px; border-bottom: solid 1px black" runat="server"
                                        valign="right">Service Tax to be Paid by</asp:Label></b>
                            </td>
                        </tr>
                        <tr style="font-size: 9px;">
                            <td colspan="3" style="border-top-style: none;">
                                <asp:CheckBox ID="chkCNor" runat="server" />C.Nor<asp:CheckBox ID="chkCNee" runat="server" />C.Nee
                                <asp:CheckBox ID="chkGoods" runat="server" />Goods Transport Agency
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table cellpadding="1" cellspacing="0" width="1050" border="1" style="font-family: Arial,Helvetica,sans-serif;">
            <tr style="height: 30px; font-size: 14px;">
                <td style="width: 30%; border-bottom-style: none;" valign="top">
                    <strong>QUALITY & QUANTITY NOT CHECKED</strong>
                </td>
                <td style="width: 40%; border-bottom-style: none;" valign="top">
                    <strong>REMARKS.</strong>
                    <asp:Label ID="lblRemP" runat="server" valign="right"></asp:Label>
                </td>
                <td style="width: 30%; border-bottom-style: none;" valign="top">
                    <asp:Label ID="lblCompname" runat="server" valign="right"></asp:Label>
                </td>
            </tr>
            <tr style="height: 10px; font-size: 14px; border-top-style: none">
                <td style="width: 30%; border-top-style: none;" valign="baseline">
                    Signature of the Consigner of his agent
                </td>
                <td style="width: 40%; border-top-style: none; text-align: center;" valign="top">
                    <asp:Label ID="Label16" runat="server" Font-Bold="true" Text="Do Not PAY CASH TO THE DRIVER"></asp:Label>
                </td>
                <td style="width: 30%; border-top-style: none;" valign="baseline">
                    <span style="font-size: 12px;">Signature</span>
                </td>
            </tr>
        </table>
        <table cellpadding="1" cellspacing="0" id="visiFalse" runat="server" visible="false"
            width="1000" border="1" style="font-family: Arial,Helvetica,sans-serif;">
            <tr>
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
                    <asp:Label ID="lblPrtyTinTxt" runat="server" Text="Tin"></asp:Label>
                </td>
                <td style="font-size: 13px; border-right-style: none">
                    <b>
                        <asp:Label ID="lblConsignorTin" Text="" runat="server"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table1">
                        <tr style="text-align: left; font-size: 18px;">
                            <td colspan="10">
                                <strong style="text-align: left;">Consignment Details:</strong>
                            </td>
                        </tr>
                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                            <HeaderTemplate>
                                <tr>
                                    <td class="white_bg" style="font-size: 12px; border-bottom: 1px solid #484848;" width="10%">
                                        <strong>S.No.</strong>
                                    </td>
                                    <td style="font-size: 12px; border-bottom: 1px solid #484848;" width="15%">
                                        <strong>Item Name</strong>
                                    </td>
                                    <td style="font-size: 12px; border-bottom: 1px solid #484848;" width="10%">
                                        <strong>Unit Name</strong>
                                    </td>
                                    <td style="font-size: 12px; border-bottom: 1px solid #484848;" width="5%">
                                        <strong>CFT</strong>
                                    </td>
                                    <td style="font-size: 12px; border-bottom: 1px solid #484848;" align="left" width="10%">
                                        <strong>Dimension</strong>
                                    </td>
                                    <td style="font-size: 12px; border-bottom: 1px solid #484848;" align="left" width="10%">
                                        <strong>Quantity</strong>
                                    </td>
                                    <td style="font-size: 12px; border-bottom: 1px solid #484848;" width="10%" align="center">
                                        <strong>Weight</strong>
                                    </td>
                                    <div id="HideGrhdr" runat="server">
                                        <td style="font-size: 12px; border-bottom: 1px solid #484848;" align="center" width="10%">
                                            <strong>Item Rate</strong>
                                        </td>
                                        <td style="font-size: 12px; border-bottom: 1px solid #484848;" width="10%" align="right">
                                            <strong>Amount</strong>
                                        </td>
                                    </div>
                                    <td style="font-size: 12px; border-bottom: 1px solid #484848;" align="right" width="10%">
                                        <strong>Detail</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="white_bg" width="10%">
                                        <%#Container.ItemIndex+1 %>.
                                    </td>
                                    <td class="white_bg" width="15%">
                                        <%#Eval("Item_Modl")%>
                                    </td>
                                    <td class="white_bg" width="10%">
                                        <%#Eval("Unit")%>
                                    </td>
                                    <td class="white_bg" width="5%">
                                        <%--<%#Convert.ToDouble(Eval("CFT"))%>--%>
                                        <%-- <%#String.Format("{0:0.000}", string.IsNullOrEmpty(Convert.ToString(Eval("CFT"))) ? 0 : Convert.ToDouble(Eval("CFT")))%>--%>
                                    </td>
                                    <td class="white_bg" align="left" width="10%">
                                       <%-- <%#Eval("Dimension")%>--%>
                                    </td>
                                    <td class="white_bg" align="left" width="10%">
                                        <%#Eval("Qty")%>
                                    </td>
                                    <td class="white_bg" align="center" width="10%">
                                        <%--<%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Ch_Weight")))%>--%>
                                    </td>
                                    <div id="HideGritem" runat="server">
                                        <td class="white_bg" width="10%" align="center">
                                            <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Item_Rate")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td class="white_bg" width="10%" align="right">
                                            <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </div>
                                    <td class="white_bg" width="10%" align="right">
                                        <%#(Eval("Detail"))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td class="white_bg" width="10%">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="15%">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="10%">
                                        <asp:Label ID="lblFTTot" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="5%">
                                        <%--<asp:Label ID="lblTotalCFT" Font-Bold="true" runat="server"></asp:Label>--%>
                                    </td>
                                    <td class="white_bg" width="10%" align="left">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" align="left" width="10%">
                                        <asp:Label ID="lblFTQty" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="10%" align="center">
                                        <%--<asp:Label ID="lblFTtotalWeight" Font-Bold="true" runat="server"></asp:Label>--%>
                                    </td>
                                    <div id="hidfooterdetl" runat="server">
                                        <td class="white_bg" width="10%" align="center">
                                        </td>
                                        <td class="white_bg" width="10%" align="right">
                                            <asp:Label ID="lblFTTotalAmnt" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                    </div>
                                    <td class="white_bg" width="10%" align="right">
                                        &nbsp;
                                    </td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%;" valign="top">
                                <table border="0" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                    <tr>
                                        <td style="width: 100%">
                                            <table width="100%">
                                                <tr runat="server" id="trRemarks">
                                                    <td colspan="2" align="left">
                                                        <tr>
                                                            <td>
                                                                <b>
                                                                    <asp:Label ID="Label10" Style="border-bottom: 1px solid #484848; font-size: 14px;"
                                                                        runat="server" valign="left" Text="Remarks:"></asp:Label></b>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblremark" Style="font-size: 12px;" runat="server" valign="right"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trTnC">
                                                    <td colspan="2" align="left">
                                                        <tr>
                                                            <td>
                                                                <b>
                                                                    <asp:Label ID="Label14" Style="border-bottom: 1px solid #484848; font-size: 14px;"
                                                                        runat="server" valign="left" Text="Terms & Conditions:"></asp:Label></b>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTnCGR" Style="font-size: 12px;" runat="server" valign="right"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" valign="baseline" align="left">
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>
                                                                    <asp:Label ID="Label17" Style="font-size: 14px;" runat="server" valign="left" Text="________________"></asp:Label></b>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label18" runat="server" Style="font-size: 12px;" valign="right" Text="Signature/Seal of the Company"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%;" valign="top">
                                <table border="0" width="100%" class="white_bg">
                                    <tr>
                                        <td colspan="4">
                                            <table border="0" cellspacing="0" style="font-size: 12px" width="100%" runat="server"
                                                id="Table2">
                                                <tr style="text-align: left; font-size: 18px;">
                                                    <td colspan="8" style="border-bottom: 1px solid #484848;">
                                                        <strong style="text-align: left;">Freight Details:</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="20%">
                                                        &nbsp;
                                                    </td>
                                                    <td style="font-size: 12px;" align="right" width="10%">
                                                        <strong>
                                                            <asp:Label ID="lblFOVPrint" runat="server"></asp:Label></strong>
                                                    </td>
                                                    <td style="font-size: 12px;" align="right" width="10%">
                                                        <strong>Surcharge</strong>
                                                    </td>
                                                    <td class="white_bg" style="font-size: 12px;" align="right" width="20%">
                                                        <strong>
                                                            <asp:Label ID="lblOctroiPrint" runat="server"></asp:Label></strong>
                                                    </td>
                                                    <td style="font-size: 12px;" align="right" width="20%">
                                                        <strong>
                                                            <asp:Label ID="lblDemuChargesPrint" runat="server"></asp:Label></strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="20%" style="border-bottom: solid 1px black;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="font-size: 12px; text-align: right; border-bottom: solid 1px black;" width="10%">
                                                        <asp:Label ID="lblFOV" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="font-size: 12px; text-align: right; border-bottom: solid 1px black;" width="10%">
                                                        <asp:Label ID="lblSurchargeP" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="font-size: 12px; text-align: right; border-bottom: solid 1px black;" width="10%">
                                                        <asp:Label ID="lblOctroi" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="font-size: 12px; text-align: right; border-bottom: solid 1px black;" align="left"
                                                        width="10%">
                                                        <asp:Label ID="lblDemurrage" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="20%">
                                                        &nbsp;
                                                    </td>
                                                    <td style="font-size: 12px;" align="right" width="10%">
                                                        <strong>
                                                            <asp:Label ID="lblUnloadingPrint" runat="server"></asp:Label></strong>
                                                    </td>
                                                    <td style="font-size: 12px;" align="right" width="20%">
                                                        <strong>
                                                            <asp:Label ID="lblCollChargePrint" runat="server"></asp:Label></strong>
                                                    </td>
                                                    <td style="font-size: 12px;" align="right" width="20%">
                                                        <strong>
                                                            <asp:Label ID="lblDelChargesPrint" runat="server"></asp:Label></strong>
                                                    </td>
                                                    <td style="font-size: 12px;" align="right" width="20%">
                                                        <strong>Sub Total</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-bottom: solid 1px black;" align="right" width="20%">
                                                        &nbsp;
                                                    </td>
                                                    <td style="font-size: 12px; text-align: right; border-bottom: solid 1px black;" align="left"
                                                        width="10%">
                                                        <asp:Label ID="lblUnloading" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="font-size: 12px; text-align: right; border-bottom: solid 1px black;" width="10%">
                                                        <asp:Label ID="lblCollectionCharges" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="white_bg" style="font-size: 12px; text-align: right; border-bottom: solid 1px black;"
                                                        width="10%">
                                                        <asp:Label ID="lblDeliveryCharges" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="font-size: 12px; text-align: right; border-bottom: solid 1px black;" width="10%">
                                                        <asp:Label ID="lblSubtotalP" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="20%">
                                                        &nbsp;
                                                    </td>
                                                    <td runat="server" id="tdser" style="font-size: 12px;" align="right" width="20%">
                                                        <strong>Serv. Tax</strong>
                                                    </td>
                                                    <td runat="server" id="tdswac" style="font-size: 12px;" align="right" width="20%">
                                                        <strong>SwachhBhrt Tax</strong>
                                                    </td>
                                                    <td runat="server" id="tdKrish" style="font-size: 12px;" align="right" width="20%">
                                                        <strong>Krishi Kalyan Tax</strong>
                                                    </td>
                                                    <td style="font-size: 12px;" align="right" colspan="8">
                                                        <strong>Net Amnt</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="20%">
                                                        &nbsp;
                                                    </td>
                                                    <td style="font-size: 12px; text-align: right;" width="10%">
                                                        <asp:Label ID="lblSerTaxCharge" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="font-size: 12px; text-align: right;" width="10%">
                                                        <asp:Label ID="lblSwchBhrt" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="font-size: 12px; text-align: right;" width="10%">
                                                        <asp:Label ID="lblKrishi" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="font-size: 12px; text-align: right;" colspan="8">
                                                        <asp:Label ID="lblNetAmntP" runat="server"></asp:Label>
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
            <tr>
                <td align="left" valign="top" colspan="4">
                    <table width="100%">
                        <tr>
                            <td align="right" class="white_bg" style="font-size: 13px" width="30%">
                            </td>
                            <td align="right" class="white_bg" style="font-size: 13px" width="10%">
                            </td>
                            <td align="right" class="white_bg" style="font-size: 13px" width="30%">
                                <b>
                                    <asp:Label ID="lblCompname1" runat="server"></asp:Label></b>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="white_bg" style="font-size: 13px; text-align: justify; padding-right: 20px;"
                                width="25%">
                                <asp:Label ID="lblTerms" Font-Size="10px" runat="server"></asp:Label>
                            </td>
                            &nbsp;&nbsp;
                            <td align="left" class="white_bg" style="font-size: 13px; text-align: justify; margin-left: 5px;"
                                width="37%">
                                <asp:Label ID="lblterms1" Font-Size="11px" runat="server"></asp:Label>
                            </td>
                            <td align="right" class="white_bg" style="font-size: 13px" width="10%">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" class="white_bg" style="width: 18%; font-size: 13px; border-right-style: none"
                    valign="top">
                    <asp:Label ID="lbltxtFromcity" runat="server">From City&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                </td>
                <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 13px;
                    border-right-style: none">
                    <b></b>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <asp:Label ID="lbltxttocity" runat="server">To City&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <b></b>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <asp:Label ID="lbltxtdelvryPlace" runat="server">Delivery Place&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <b>
                        <asp:Label ID="lblDelvryPlace" runat="server"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td align="left" class="white_bg" style="width: 18%; font-size: 13px; border-right-style: none"
                    valign="top">
                    <asp:Label ID="lblViaCity" runat="server">Via City&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                </td>
                <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 13px;
                    border-right-style: none">
                    <b>
                        <asp:Label ID="lblValueViaCity" runat="server"></asp:Label>
                    </b>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; width: 18%;
                    border-right-style: none">
                    <asp:Label ID="lblConsName" runat="server">Consignor's Name &nbsp;:</asp:Label>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <b>
                        <asp:Label ID="lblvalConsName" runat="server"></asp:Label></b>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <asp:Label ID="Label9" runat="server">Serv. Tax Paid by&nbsp;:</asp:Label>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <b>
                        <asp:Label ID="lblSerTaxto" runat="server"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <asp:Label ID="Label8" Text="Modvat Copy:" runat="server"></asp:Label>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <b></b>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <asp:Label ID="Label13" runat="server">Tran. Type&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <b></b>
                </td>
            </tr>
            <tr id="tr1" runat="server">
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <asp:Label ID="lblNameShipmentno" runat="server">Shipment No.&nbsp;&nbsp;&nbsp;:</asp:Label>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <b>
                        <asp:Label ID="lblShipmentNo" runat="server"></asp:Label></b>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <asp:Label ID="lblDinNoText" runat="server">DI No.&nbsp;&nbsp;&nbsp;:</asp:Label>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <b>
                        <asp:Label ID="lblDinNo" runat="server"></asp:Label></b>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <asp:Label ID="lblNameContnrNo" Text="Container No.:" runat="server"></asp:Label>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <b>
                        <asp:Label ID="lblContainerNo" runat="server"></asp:Label></b>
                </td>
            </tr>
            <tr id="tr2" runat="server">
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; width: 18%;
                    border-right-style: none">
                    <asp:Label ID="lblEGPNo" runat="server">EGP No.&nbsp;&nbsp;&nbsp;:</asp:Label>
                </td>
                <td align="left" class="white_bg" valign="top" colspan="0" style="font-size: 13px;
                    border-right-style: none">
                    <b>
                        <asp:Label ID="lblEGPNoval" runat="server"></asp:Label>
                    </b>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; width: 18%;
                    border-right-style: none">
                    <asp:Label ID="lblRefNo" runat="server">Ref No.&nbsp;&nbsp;&nbsp;:</asp:Label>
                </td>
                <td align="left" class="white_bg" valign="top" colspan="0" style="font-size: 13px;
                    border-right-style: none">
                    <b>
                        <asp:Label ID="lblrefnoval" runat="server"></asp:Label>
                    </b>
                </td>
                <td align="left" class="white_bg" style="width: 18%; font-size: 13px; border-right-style: none"
                    valign="top">
                    <asp:Label ID="lblOrderNo" runat="server">Order No&nbsp;&nbsp;&nbsp;:</asp:Label>
                </td>
                <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 13px;
                    border-right-style: none">
                    <b>
                        <asp:Label ID="lblOrderNoVal" runat="server"></asp:Label>
                    </b>
                </td>
            </tr>
            <tr id="tr3" runat="server">
                <td align="left" class="white_bg" style="width: 18%; font-size: 13px; border-right-style: none"
                    valign="top">
                    <asp:Label ID="lblFormNo" runat="server">Form No&nbsp;&nbsp;&nbsp;:</asp:Label>
                </td>
                <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 13px;
                    border-right-style: none">
                    <b>
                        <asp:Label ID="lblFormNoVal" runat="server"></asp:Label>
                    </b>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; width: 18%;
                    border-right-style: none">
                    <asp:Label ID="lbltxtagent" runat="server">Agent&nbsp;&nbsp;&nbsp;:</asp:Label>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <b>
                        <asp:Label ID="lblAgent" runat="server"></asp:Label></b>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <asp:Label ID="lblNameSealNo" runat="server">Seal No.&nbsp;&nbsp;&nbsp;:</asp:Label>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <b>
                        <asp:Label ID="lblSealNo" runat="server"></asp:Label></b>
                </td>
            </tr>
            <tr id="tr4" runat="server">
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <asp:Label ID="lblTotItem" runat="server">Total Item Value&nbsp;:</asp:Label>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <b>
                        <asp:Label ID="lblTotItemValue" runat="server"></asp:Label></b>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; width: 18%;
                    border-right-style: none">
                    <asp:Label ID="lblNameContnrType" runat="server">Type&nbsp;&nbsp;&nbsp;:</asp:Label>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <b>
                        <asp:Label ID="lblCntnrType" runat="server"></asp:Label></b>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <asp:Label ID="lblNameCntnrSize" runat="server">Size &nbsp;&nbsp;&nbsp;:</asp:Label>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                    <b>
                        <asp:Label ID="lblContainerSize" runat="server"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td style="font-size: 13px; border-right-style: none; width: 25%;">
                    <asp:Label ID="Label2" runat="server">Address</asp:Label>
                </td>
                <td style="font-size: 13px; border-right-style: none">
                    <b></b>
                </td>
                <td style="font-size: 13px; border-right-style: none; width: 25%;">
                    <asp:Label ID="Label4" runat="server">Address</asp:Label>
                </td>
                <td style="font-size: 13px; border-right-style: none">
                    <b></b>
                </td>
            </tr>
            <tr>
                <td align="left" class="white_bg" style="width: 18%; font-size: 13px; border-right-style: none"
                    valign="top">
                    <asp:Label ID="lbltxtgrno" runat="server">GR. No.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                </td>
                <td align="left" class="white_bg" valign="top" style="width: 22%; font-size: 13px;
                    border-right-style: none">
                    <b></b>
                </td>
                <td align="left" class="white_bg" style="width: 14%; font-size: 13px; border-right-style: none"
                    valign="top">
                    <asp:Label ID="lbltxtgrdate" Text="GR. Date" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    :
                </td>
                <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 13px;
                    border-right-style: none">
                    <b></b>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;
                    width: 15%;">
                    <asp:Label ID="Label6" runat="server">Lorry No &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                </td>
                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;
                    width: 15%;">
                    <b>
                        <asp:Label ID="lblLorryNo" runat="server"></asp:Label></b>
                </td>
            </tr>
        </table>
    </div>
     <asp:HiddenField ID="hidPages" runat="server" />
     <asp:HiddenField ID="HidsRenWages" runat="server" />
     <asp:HiddenField ID="HidsRenCartage" runat="server" />
     <asp:HiddenField ID="HidRenCommission" runat="server" />
     <asp:HiddenField ID="hidRenamePF" runat="server" />
     <asp:HiddenField ID="hidrefrename" runat="server" />
     <asp:HiddenField ID="hidRenameToll" runat="server" />
      <asp:HiddenField ID="HidRenBilty" runat="server" />
      <asp:HiddenField ID="HiddServTaxPer" runat="server" />
      <asp:HiddenField ID="HiddSwachhBrtTaxPer" runat="server" />
      <asp:HiddenField ID="HiddKalyanTaxPer" runat="server" />
      <asp:HiddenField ID="HiddKalyanTax" runat="server" />
      <asp:HiddenField ID="HiddTruckIdno" runat="server" />
      <asp:HiddenField ID="HiddConSize" runat="server" />
      <asp:HiddenField ID="HiddUserPrefCont" runat="server" />
      <asp:HiddenField ID="hidAdvOrdrQty" runat="server" />
      <asp:HiddenField ID="hidAdvOrdrWght" runat="server" />
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
