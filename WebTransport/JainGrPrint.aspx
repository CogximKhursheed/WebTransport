<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JainGrPrint.aspx.cs" Inherits="WebTransport.JainGrPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<form id="form1" runat="server">
<div class="col-lg-0" id="divho" runat="server" visible="false" style="height: 80%;">
    <table cellpadding="1" cellspacing="0" width="100%" border="1" style="font-family: Arial,Helvetica,sans-serif;">
        <tr>
            <td style="width: 20%; font-size: 14px; border-left-style: none; text-align: center;
                border-right-style: none" valign="top">
                <asp:label id="lblJurCity" runat="server" text=""></asp:label>
            </td>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="width: 20%; font-size: 14px; border-left-style: none; border-right-style: none"
                                valign="top">
                                <div style="text-align: left; width: 10px; float: left;">
                                    <asp:image id="ImgLogoJain" width="140px" height="90px" runat="server"></asp:image>
                                </div>
                            </td>
                            <td style="width: 50%; font-size: 12px; text-align: center; border-left-style: none;
                                border-right-style: none" align="center" valign="top">
                                <div id="header1" runat="server" style="text-align: center; width: 500px;">
                                    <strong>
                                        <asp:label id="lblCompanyname1" runat="server" style="font-size: 14px;"></asp:label>
                                        <br />
                                    </strong>
                                    <asp:label id="Label7" runat="server" text="(Fleet Owners & Transport Contractor)"></asp:label>
                                    <br />
                                    <strong>Head Office :
                                        <asp:label id="lblCompAdd3" runat="server"></asp:label>
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:label id="lblCompAdd4" runat="server"></asp:label>
                                        <asp:label id="lblCompCity1" runat="server"></asp:label>
                                        &nbsp;&nbsp;
                                        <asp:label id="lblCompState1" runat="server"></asp:label>
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:label id="lblCompCityPin1" runat="server"></asp:label>
                                        <br />
                                        <b>GSTIN No. :&nbsp;</b><asp:label ID="lblMainCopyCompGSTIN" runat="server"></asp:label>
                                    </strong>
                                </div>
                            </td>
                            <td style="width: 30%;" valign="top">
                                <table width="100%">
                                    <tr>
                                        <td style="font-size: 13px; width: 25%;">
                                            <b>REF No.:</b>
                                        </td>
                                        <td style="font-size: 13px;">
                                            <asp:label id="lblRefNo1" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 13px; width: 25%;">
                                            <b>Booking No.:</b>
                                        </td>
                                        <td style="font-size: 13px;">
                                            <asp:label id="lblShipmentNo1" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="font-size: 13px; vertical-align: bottom; text-align: center;
                                            font-size: 13px;">
                                            <br />
                                            <b>H.O. COPY</b>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="width: 33%;" valign="top">
                                <table border="1" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                    <tr style="height: 50px;">
                                        <td align="center" colspan="2" style="border-bottom: 1px solid #484848; font-size: 13px;">
                                            <strong>CAUTION</strong>
                                        </td>
                                    </tr>
                                    <tr style="height: 52px;">
                                        <td colspan="2" style="font-size: 11px;">
                                            The Consignment will not be detained diverted,rerouted or re-booked without cosignee
                                            bank permission .<br />
                                            It will be delivered at the destination .
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="1" style="width: 100px; font-size: 13px;">
                                            <strong>No:</strong>
                                        </td>
                                        <td align="left" colspan="2" style="width: 150px; font-size: 13px;">
                                            <asp:label id="lblGRno1" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="1" style="width: 100px; font-size: 13px;">
                                            <strong>Date:</strong>
                                        </td>
                                        <td align="left" colspan="2" style="width: 150px; font-size: 13px;">
                                            <asp:label id="lblGrDate1" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="1" style="width: 100px; font-size: 13px;">
                                            <strong>Delivery at:</strong>
                                        </td>
                                        <td align="left" colspan="2" style="width: 150px; font-size: 13px;">
                                            <asp:label id="lbldeliveryat" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 35%;" valign="top">
                                <table border="1" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                    <tr style="height: 51px;">
                                        <td style="font-size: 14px; border-right-style: none; width: 25%;">
                                            <b>
                                                <asp:label id="lbltxtPanNo1" runat="server" text="PAN NO. :"></asp:label>
                                            </b>
                                        </td>
                                        <td style="font-size: 14px; border-right-style: none; width: 20%;">
                                            <asp:label id="lblPanNo1" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                    <tr style="height: 53px;">
                                        <td style="border-bottom: 1px solid #484848; font-size: 14px; width: 25%;">
                                            <b>
                                                <asp:label id="lblTin1" runat="server" text="Serv.Tax No.:"></asp:label>
                                            </b>
                                        </td>
                                        <td style="border-bottom: 1px solid #484848; font-size: 14px; width: 20%;">
                                            <asp:label id="lblCompTIN1" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-bottom: 1px solid #484848; font-size: 13px; width: 25%;">
                                            <b>Tax Paid By</b>
                                        </td>
                                        <td style="border-bottom: 1px solid #484848; font-size: 13px; width: 20%;">
                                            <asp:label id="lbltaxpaidby" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                            <b>Invoice No.</b>
                                        </td>
                                        <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                            <asp:label id="lblInvNoValue1" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                            <b>
                                                <asp:label id="lbltinnotext" runat="server" text="Tin No.:"></asp:label>
                                            </b>
                                        </td>
                                        <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                            <asp:label id="lbltinno1" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 33%;" valign="top">
                                <table border="1" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                    <tr>
                                        <td align="center" colspan="2" style="border-bottom: 0px solid #484848; font-size: 13px;">
                                            <strong>NOTE</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2" style="border-bottom: 1px solid #484848; font-size: 9px;">
                                            The Consignment Covered by this set of special lorry recipt from shall be stored
                                            at the destination under the control of the transport operator and shall be deliverd
                                            to or to the order of the cosignee bank whose name is mentioned in the lorry recipt
                                            .it will under no circumstances delivered to any one without the writeen authority
                                            from the consignee bank or its order endorsed on the consignee copy or on a seprate
                                            letter of authority
                                        </td>
                                    </tr>
                                    <tr style="height: 20px">
                                        <td colspan="1" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848;
                                            font-size: 13px;">
                                            <strong>Lorry No.</strong>
                                        </td>
                                        <td align="left" colspan="2" style="border-bottom: 1px solid #484848; width: 150px;
                                            font-size: 13px;">
                                            <asp:label id="lblLorryNo1" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                    <tr style="height: 20px">
                                        <td colspan="1" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848;
                                            font-size: 13px;">
                                            <strong>FROM</strong>
                                        </td>
                                        <td align="left" colspan="2" style="border-bottom: 1px solid #484848; width: 150px;
                                            font-size: 13px;">
                                            <asp:label id="lblFromCity1" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                    <tr style="height: 20px">
                                        <td colspan="1" style="width: 100px; border-bottom: 1px solid #484848; border-right: 1px solid #484848;
                                            font-size: 13px;">
                                            <strong>TO</strong>
                                        </td>
                                        <td align="left" colspan="2" style="border-bottom: 1px solid #484848; width: 150px;
                                            font-size: 13px;">
                                            <asp:label id="lblJainVia" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                    <tr style="height: 20px">
                                        <td colspan="1" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848;
                                            font-size: 13px;">
                                            <strong>TO</strong>
                                        </td>
                                        <td align="left" colspan="2" style="border-bottom: 1px solid #484848; width: 150px;
                                            font-size: 13px;">
                                            <asp:label id="lblToCity1" runat="server"></asp:label>
                                        </td>
                                </table>
                            </td>
                        </tr>
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
                                        <td align="center" colspan="2" style="border-bottom: 1px solid #484848; font-size: 13px;">
                                            <strong>CONSIGNOR</strong>
                                        </td>
                                    </tr>
                                    <tr id="trConsigneeName1" runat="server">
                                        <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                            <b>
                                                <asp:label id="Label52" runat="server">Name</asp:label>
                                            </b>
                                        </td>
                                        <td style="font-size: 13px; border-right-style: none">
                                            <asp:label id="lblConsigeeName1" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                    <tr style="height: 40px;" valign="top">
                                        <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                            <b>
                                                <asp:label id="Label54" runat="server">Address</asp:label>
                                            </b>
                                        </td>
                                        <td style="font-size: 13px; border-right-style: none">
                                            <asp:label id="lblConsigneeAddress1" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                    <tr valign="bottom">
                                        <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                            <b>
                                                <asp:label id="lblCha2" runat="server"></asp:label>
                                            </b>
                                        </td>
                                        <td style="font-size: 13px; border-right-style: none">
                                            <asp:label id="lblChaName2" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                    <tr id="MainConsigGSTINNo" runat="server">
                                         <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                            <b>GSTIN No.</b>
                                        </td>
                                        <td style="font-size: 13px; border-right-style: none">
                                            <asp:label id="lblMainConsigGSTINNo" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%;" valign="top">
                                <table border="0" width="100%" class="white_bg">
                                    <tr>
                                        <td align="center" colspan="2" style="border-bottom: 1px solid #484848; height: 10px;
                                            font-size: 13px;">
                                            <strong>CONSIGNEE</strong>
                                        </td>
                                    </tr>
                                    <tr id="trConsignorName1" runat="server">
                                        <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                            <b>
                                                <asp:label id="lbltxtName" runat="server">Name</asp:label>
                                            </b>
                                        </td>
                                        <td style="font-size: 13px; border-right-style: none">
                                            <asp:label id="lblConsignorName1" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                    <tr style="height: 40px;" valign="top">
                                        <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                            <b>
                                                <asp:label id="lblTxtAdd" runat="server">Address</asp:label>
                                            </b>
                                        </td>
                                        <td style="font-size: 13px; border-right-style: none">
                                            <asp:label id="lblConsignorAddress1" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                    <tr valign="bottom">
                                        <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                            <b>
                                                <asp:label id="lblFor1" runat="server"></asp:label>
                                            </b>
                                        </td>
                                        <td style="font-size: 13px; border-right-style: none">
                                            <asp:label id="lblForName1" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                    <tr id="MainConsigneeGSTINNo" runat="server">
                                         <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                            <b>GSTIN No.</b>
                                        </td>
                                        <td style="font-size: 13px; border-right-style: none">
                                            <asp:label id="lblMainConsigneeGSTINNo" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="width: 40%;" valign="top">
                                <table border="0" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                    <tr>
                                        <td align="center" style="border-bottom: 1px solid #484848; border-left: 1px solid #484848;
                                            border-top: 1px solid #484848; border-right: 1px solid #484848; font-size: 13px;">
                                            <strong>Packages</strong>
                                        </td>
                                        <td id="divAmntHead" runat="server" align="center" style="border-bottom: 1px solid #484848;
                                            border-left: 0px solid #484848; border-top: 1px solid #484848; border-right: 0px solid #484848;
                                            font-size: 13px;">
                                            <strong>Description (Said to Contain)</strong>
                                        </td>
                                    </tr>
                                    <tr style="height: 32px">
                                        <td align="center" style="border-bottom: 1px solid #484848; border-left: 1px solid #484848;
                                            border-right: 1px solid #484848; font-size: 13px;">
                                        </td>
                                        <td id="divAmntvalue" runat="server" align="left" style="border-bottom: 1px solid #484848;">
                                            <asp:label id="lblDetails1" runat="server" wrap="true"></asp:label>
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2" style="border-bottom: 0px solid #484848; border-right: 0px solid #484848;
                                            font-size: 11px;">
                                            <strong>Declration for cenvat credit:</strong>We hereby certify that we have not
                                            availed credit of duty paid on input or capital goods unser the provisions of cenvat
                                            credit rules 2004 nor we have availed the benefit of notification no.12/2003-ST
                                            dated 20.06.2003
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 60%;" valign="top">
                                <table border="0" width="100%" class="white_bg" style="border-right: 0px solid #484848;">
                                    <tr style="height: 40px">
                                        <td style="width: 220px; border-bottom: 1px solid #484848; border-left: 1px solid #484848;
                                            border-top: 1px solid #484848; border-right: 1px solid #484848; font-size: 13px;">
                                            <strong>Packages</strong>
                                        </td>
                                        <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                            border-right: 1px solid #484848; font-size: 13px;">
                                            <strong>Weight Actual</strong>
                                        </td>
                                        <td style="width: 70px; border-bottom: 1px solid #484848; border-left: 1px solid #484848;
                                            border-top: 1px solid #484848; border-right: 1px solid #484848; font-size: 13px;">
                                            <strong>Rate</strong>
                                        </td>
                                        <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                            border-right: 1px solid #484848; width: 150px; font-size: 13px;">
                                            <strong><b>
                                                <asp:label runat="server" id="AmountType1" text=""></asp:label>
                                            </b></strong>
                                        </td>
                                    </tr>
                                    <tr style="height: 25px; width: 220px">
                                        <td style="border-left: 1px solid #484848; border-bottom: 1px solid #484848; width: 220px;
                                            border-right: 1px solid #484848; font-size: 13px;">
                                            <strong>Container No.</strong><asp:label id="lblJainContainerNo" runat="server"></asp:label>
                                        </td>
                                        <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                            G.Wt.
                                        </td>
                                        <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                            border-right: 1px solid #484848; width: 70px; font-size: 13px;">
                                            Labour Ch.
                                        </td>
                                        <td align="center" style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                        </td>
                                    </tr>
                                    <tr style="height: 25px">
                                        <td style="border-left: 1px solid #484848; font-size: 13px; width: 220px; border-bottom: 1px solid #484848;
                                            border-right: 1px solid #484848;">
                                            <strong>
                                                <asp:label id="Container" visble="false" runat="server">Container No2.</asp:label>
                                            </strong>
                                            <asp:label id="lblContainerNo2" visble="false" runat="server"></asp:label>
                                        </td>
                                        <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                            Cont.Wt.
                                        </td>
                                        <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                            border-right: 1px solid #484848; width: 70px; font-size: 13px;">
                                            Bilty Ch.
                                        </td>
                                        <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                        </td>
                                    </tr>
                                    <tr style="height: 25px; width: 220px;">
                                        <td style="border-left: 1px solid #484848; border-bottom: 1px solid #484848; width: 220px;
                                            border-right: 1px solid #484848; font-size: 13px;">
                                            <strong>Seal No.</strong><asp:label id="lblJainSealNo" runat="server"></asp:label>
                                        </td>
                                        <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                            T.Wt.
                                        </td>
                                        <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                            border-right: 1px solid #484848; width: 70px; font-size: 13px;">
                                            Statist Ch.
                                        </td>
                                        <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                        </td>
                                    </tr>
                                    <tr style="height: 25px">
                                        <td align="left" style="border-left: 1px solid #484848; width: 220px; border-right: 1px solid #484848;
                                            border-bottom: 1px solid #484848; font-size: 13px;">
                                            <strong>
                                                <asp:label id="seal2" visble="false" runat="server">Seal No2.</asp:label>
                                            </strong>
                                            <asp:label id="lblSealNo2" visble="false" runat="server"></asp:label>
                                        </td>
                                        <td style="border-bottom: 0px solid #484848; width: 150px; font-size: 13px;">
                                            &nbsp;
                                        </td>
                                        <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                            border-right: 1px solid #484848; width: 70px; font-size: 13px;">
                                            Detention
                                        </td>
                                        <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                        </td>
                                    </tr>
                                    <tr style="height: 25px">
                                        <td align="center" style="border-left: 1px solid #484848; width: 150px; border-right: 1px solid #484848;
                                            border-bottom: 1px solid #484848;">
                                            <strong>AT OWNER RISK </strong>
                                        </td>
                                        <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                            Net
                                        </td>
                                        <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                            border-right: 1px solid #484848; width: 70px; font-size: 13px;">
                                            G.Total
                                        </td>
                                        <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                        </td>
                                    </tr>
                                    <asp:hiddenfield id="hidPages" runat="server" />
                                    <tr>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td align="left" style="font-size: 11px;">
                                <strong>Note:</strong> We are not responsible for leakage,breaking &amp; damage,Consinger
                                is resposible for contralandor restricted goods
                            </td>
                            <td align="center" style="border-bottom: 1px solid #484848; width: 33%; border-right: 1px solid #484848;
                                border-left: 1px solid #484848; border-top: 1px solid #484848; font-size: 13px;">
                                <strong>
                                    <asp:label id="lblGrtype1" runat="server" text=""></asp:label>
                                </strong>
                            </td>
                            <td align="right" style="width: 33%; vertical-align: top; font-size: 13px;">
                                For: <strong>
                                    <asp:label id="lblauth" runat="server" text="Authorised Signatory"></asp:label>
                                </strong>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </tr>
    </table>
</div>
<div class="col-lg-0" id="DivDriver" runat="server" visible="false">
    <table cellpadding="1" cellspacing="0" width="100%" border="1" style="font-family: Arial,Helvetica,sans-serif;
        border-width: 1px; border-color: #000000; page-break-before: always;">
        <tr>
            <td style="width: 20%; font-size: 14px; border-left-style: none; text-align: center;
                border-right-style: none" valign="top">
                <asp:label id="lblJurCityj" runat="server" text=""></asp:label>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 20%; font-size: 14px; border-left-style: none; border-right-style: none"
                            valign="top">
                            <div style="text-align: left; width: 10px; float: left;">
                                <asp:image id="ImgLogoJainj" width="140px" height="90px" runat="server"></asp:image>
                            </div>
                        </td>
                        <td style="width: 50%; font-size: 12px; text-align: center; border-left-style: none;
                            border-right-style: none" align="center" valign="top">
                            <div id="Div2" runat="server" style="text-align: center; width: 500px;">
                                <strong>
                                    <asp:label id="lblCompanyname1j" runat="server" style="font-size: 14px;"></asp:label>
                                    <br />
                                </strong>
                                <asp:label id="Label7j" runat="server" text="(Fleet Owners & Transport Contractor)"></asp:label>
                                <br />
                                <strong>Head Office :
                                    <asp:label id="lblCompAdd3j" runat="server"></asp:label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:label id="lblCompAdd4j" runat="server"></asp:label>
                                    <asp:label id="lblCompCity1j" runat="server"></asp:label>
                                    &nbsp;&nbsp;
                                    <asp:label id="lblCompState1j" runat="server"></asp:label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:label id="lblCompCityPin1j" runat="server"></asp:label>
                                    <br />
                                    <b>GSTIN No. :&nbsp;</b><asp:label ID="lblDriverCopyCompGSTIN" runat="server"></asp:label>
                                </strong>
                            </div>
                        </td>
                        <td style="width: 30%;" valign="top">
                            <table width="100%">
                                <tr>
                                    <td style="font-size: 13px; width: 25%;">
                                        <b>REF No.:</b>
                                    </td>
                                    <td style="font-size: 13px;">
                                        <asp:label id="lblRefNo2" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 13px; width: 25%;">
                                        <b>Booking No.:</b>
                                    </td>
                                    <td style="font-size: 13px;">
                                        <asp:label id="lblShipmentNo2" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="font-size: 13px; vertical-align: bottom; text-align: center;
                                        font-size: 13px;">
                                        <br />
                                        <b>DRIVER COPY </b>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 33%;" valign="top">
                            <table border="1" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                <tr style="height: 50px;">
                                    <td align="center" colspan="2" style="border-bottom: 1px solid #484848; font-size: 13px;">
                                        <strong>CAUTION</strong>
                                    </td>
                                </tr>
                                <tr style="height: 52px;">
                                    <td colspan="2" style="font-size: 11px;">
                                        The Consignment will not be detained diverted,rerouted or re-booked without cosignee
                                        bank permission .<br />
                                        It will be delivered at the destination .
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="1" style="width: 100px; font-size: 13px;">
                                        <strong>No:</strong>
                                    </td>
                                    <td align="left" colspan="2" style="width: 150px; font-size: 13px;">
                                        <asp:label id="lblGRno1j" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="1" style="width: 100px; font-size: 13px;">
                                        <strong>Date:</strong>
                                    </td>
                                    <td align="left" colspan="2" style="width: 150px; font-size: 13px;">
                                        <asp:label id="lblGrDate1j" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="1" style="width: 100px; font-size: 13px;">
                                        <strong>Delivery at:</strong>
                                    </td>
                                    <td align="left" colspan="2" style="width: 150px; font-size: 13px;">
                                        <asp:label id="lbldeliveryatj" runat="server"></asp:label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 35%;" valign="top">
                            <table border="1" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                <tr style="height: 51px;">
                                    <td style="font-size: 15px; border-right-style: none; width: 25%;">
                                        <b>
                                            <asp:label id="lbltxtPanNo1j" runat="server" text="PAN NO. :"></asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 15px; border-right-style: none; width: 20%;">
                                        <asp:label id="lblPanNo1j" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr style="height: 53px;">
                                    <td style="border-bottom: 1px solid #484848; font-size: 15px; width: 25%;">
                                        <b>
                                            <asp:label id="lblTin1j" runat="server" text="Serv.Tax No.:"></asp:label>
                                        </b>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; font-size: 15px; width: 20%;">
                                        <asp:label id="lblCompTIN1j" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-bottom: 1px solid #484848; font-size: 13px; width: 25%;">
                                        <b>Service Tax Paid By</b>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; font-size: 13px; width: 20%;">
                                        <asp:label id="lbltaxpaidbyj" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                        <b>Invoice No.</b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <asp:label id="lblInvNoValue2" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                        <b>
                                            <asp:label id="lbltinnotextj" runat="server" text="Tin No.:"></asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <asp:label id="lbltinno2" runat="server"></asp:label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 33%;" valign="top">
                            <table border="1" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                <tr>
                                    <td align="center" colspan="2" style="border-bottom: 0px solid #484848; font-size: 13px;">
                                        <strong>NOTE</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" style="border-bottom: 1px solid #484848; font-size: 9px;">
                                        The Consignment Covered by this set of special lorry recipt from shall be stored
                                        at the destination under the control of the transport operator and shall be deliverd
                                        to or to the order of the cosignee bank whose name is mentioned in the lorry recipt
                                        .it will under no circumstances delivered to any one without the writeen authority
                                        from the consignee bank or its order endorsed on the consignee copy or on a seprate
                                        letter of authority
                                    </td>
                                </tr>
                                <tr style="height: 20px">
                                    <td colspan="1" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848;
                                        font-size: 13px;">
                                        <strong>Lorry No.</strong>
                                    </td>
                                    <td align="left" colspan="2" style="border-bottom: 1px solid #484848; width: 150px;
                                        font-size: 13px;">
                                        <asp:label id="lblLorryNo1j" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr style="height: 20px">
                                    <td colspan="1" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848;
                                        font-size: 13px;">
                                        <strong>FROM</strong>
                                    </td>
                                    <td align="left" colspan="2" style="border-bottom: 1px solid #484848; width: 150px;
                                        font-size: 13px;">
                                        <asp:label id="lblFromCity1j" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr style="height: 20px">
                                    <td colspan="1" style="width: 100px; border-bottom: 1px solid #484848; border-right: 1px solid #484848;
                                        font-size: 13px;">
                                        <strong>TO</strong>
                                    </td>
                                    <td align="left" colspan="2" style="border-bottom: 1px solid #484848; width: 150px;
                                        font-size: 13px;">
                                        <asp:label id="lblJainViaj" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr style="height: 20px">
                                    <td colspan="1" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848;
                                        font-size: 13px;">
                                        <strong>TO</strong>
                                    </td>
                                    <td align="left" colspan="2" style="border-bottom: 1px solid #484848; width: 150px;
                                        font-size: 13px;">
                                        <asp:label id="lblToCity1j" runat="server"></asp:label>
                                    </td>
                            </table>
                        </td>
                    </tr>
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
                                    <td align="center" colspan="2" style="border-bottom: 1px solid #484848; font-size: 13px;">
                                        <strong>CONSIGNOR</strong>
                                    </td>
                                </tr>
                                <tr id="tr1" runat="server">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>
                                            <asp:label id="Label52j" runat="server">Name</asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblConsigeeName1j" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr style="height: 40px;" valign="top">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>
                                            <asp:label id="Label54j" runat="server">Address</asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblConsigneeAddress1j" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr valign="bottom">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>
                                            <asp:label id="lblCha3" runat="server"></asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblChaName3" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr id="DriverConsigGSTINNo" runat="server">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>GSTIN No.</b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblDriverConsigGSTINNo" runat="server"></asp:label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 50%;" valign="top">
                            <table border="0" width="100%" class="white_bg">
                                <tr>
                                    <td align="center" colspan="2" style="border-bottom: 1px solid #484848; height: 10px;
                                        font-size: 13px;">
                                        <strong>CONSIGNEE</strong>
                                    </td>
                                </tr>
                                <tr id="tr2" runat="server">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>
                                            <asp:label id="lbltxtNamej" runat="server">Name</asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblConsignorName1j" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr style="height: 40px;" valign="top">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>
                                            <asp:label id="lblTxtAddj" runat="server">Address</asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblConsignorAddress1j" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr valign="bottom">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>
                                            <asp:label id="lblFor2" runat="server"></asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblForName2" runat="server"></asp:label>
                                    </td>
                                </tr>
                                 <tr id="DriverConsigneeGSTINNo" runat="server">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>GSTIN No.</b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblDriverConsigneeGSTINNo" runat="server"></asp:label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 40%;" valign="top">
                            <table border="0" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                <tr>
                                    <td align="center" style="border-bottom: 1px solid #484848; border-left: 1px solid #484848;
                                        border-top: 1px solid #484848; border-right: 1px solid #484848; font-size: 13px;">
                                        <strong>Packages</strong>
                                    </td>
                                    <td id="Td1" runat="server" align="center" style="border-bottom: 1px solid #484848;
                                        border-left: 0px solid #484848; border-top: 1px solid #484848; border-right: 0px solid #484848;
                                        font-size: 13px;">
                                        <strong>Description (Said to Contain)</strong>
                                    </td>
                                </tr>
                                <tr style="height: 32px">
                                    <td align="center" style="border-bottom: 1px solid #484848; border-right: 1px solid #484848;
                                        font-size: 13px;">
                                    </td>
                                    <td id="Td2" runat="server" align="left" style="border-bottom: 1px solid #484848;">
                                        <asp:label id="lblDetails2" wrap="true" runat="server"></asp:label>
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" style="border-bottom: 0px solid #484848; border-right: 0px solid #484848;
                                        font-size: 11px;">
                                        <strong>Declration for cenvat credit:</strong>We hereby certify that we have not
                                        availed credit of duty paid on input or capital goods unser the provisions of cenvat
                                        credit rules 2004 nor we have availed the benefit of notification no.12/2003-ST
                                        dated 20.06.2003
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 60%;" valign="top">
                            <table border="0" width="100%" class="white_bg" style="border-right: 0px solid #484848;">
                                <tr style="height: 40px">
                                    <td style="width: 220px; border-bottom: 1px solid #484848; border-left: 1px solid #484848;
                                        border-top: 1px solid #484848; border-right: 1px solid #484848; font-size: 13px;">
                                        <strong>Packages</strong>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                        border-right: 1px solid #484848; font-size: 13px;">
                                        <strong>Weight Actual</strong>
                                    </td>
                                    <td style="width: 70px; border-bottom: 1px solid #484848; border-left: 1px solid #484848;
                                        border-top: 1px solid #484848; border-right: 1px solid #484848; font-size: 13px;">
                                        <strong>Rate</strong>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                        border-right: 1px solid #484848; width: 150px; font-size: 13px;">
                                        <strong><b>
                                            <asp:label runat="server" id="AmountType2" text=""></asp:label>
                                        </b></strong>
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td style="border-left: 1px solid #484848; border-bottom: 1px solid #484848; width: 220px;
                                        border-right: 1px solid #484848; font-size: 13px;">
                                        <strong>Container No.</strong><asp:label id="lblJainContainerNoj" runat="server"></asp:label>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                        G.Wt.
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                        border-right: 1px solid #484848; width: 70px; font-size: 13px;">
                                        Labour Ch.
                                    </td>
                                    <td align="center" style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td style="border-left: 1px solid #484848; font-size: 13px; width: 220px; border-bottom: 1px solid #484848;
                                        border-right: 1px solid #484848;">
                                        <strong>
                                            <asp:label id="Container3" visible="false" runat="server">Container No2.</asp:label>
                                        </strong>
                                        <asp:label id="lblJainContainerNo2" visible="false" runat="server"></asp:label>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                        Cont.Wt.
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                        border-right: 1px solid #484848; width: 70px; font-size: 13px;">
                                        Bilty Ch.
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td style="border-left: 1px solid #484848; border-bottom: 1px solid #484848; width: 150px;
                                        border-right: 1px solid #484848; font-size: 13px;">
                                        <strong>Seal No.</strong>
                                        <asp:label id="lblJainSealNoj" runat="server"></asp:label>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                        T.Wt.
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                        border-right: 1px solid #484848; width: 70px; font-size: 13px;">
                                        Statist Ch.
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td align="left" style="border-left: 1px solid #484848; width: 220px; border-right: 1px solid #484848;
                                        border-bottom: 1px solid #484848; font-size: 13px;">
                                        <strong>
                                            <asp:label id="seal" visible="false" runat="server">Seal No2.</asp:label>
                                        </strong>
                                        <asp:label id="lblJainSealNo2" visible="false" runat="server"></asp:label>
                                    </td>
                                    <td style="border-bottom: 0px solid #484848; width: 150px; font-size: 13px;">
                                        &nbsp;
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                        border-right: 1px solid #484848; width: 70px; font-size: 13px;">
                                        Detention
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td align="center" style="border-left: 1px solid #484848; width: 150px; border-right: 1px solid #484848;
                                        border-bottom: 1px solid #484848;">
                                        <strong>AT OWNER RISK </strong>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                        Net
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                        border-right: 1px solid #484848; width: 70px; font-size: 13px;">
                                        G.Total
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                    </td>
                                </tr>
                                <asp:hiddenfield id="hidPagesj" runat="server" />
                                <tr>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td align="left" style="font-size: 11px;">
                            <strong>Note:</strong> We are not responsible for leakage,breaking &amp; damage,Consinger
                            is resposible for contralandor restricted goods
                        </td>
                        <td align="center" style="border-bottom: 1px solid #484848; width: 33%; border-right: 1px solid #484848;
                            border-left: 1px solid #484848; border-top: 1px solid #484848; font-size: 13px;">
                            <strong>
                                <asp:label id="lblGrtype4" runat="server" text=""></asp:label>
                            </strong>
                        </td>
                        <td align="right" style="width: 33%; vertical-align: top; font-size: 13px;">
                            For: <strong>
                                <asp:label id="lblauthj" runat="server" text="Authorised Signatory"></asp:label>
                            </strong>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
<div class="col-lg-0" id="Divconsigor" runat="server" visible="false">
    <table cellpadding="1" cellspacing="0" width="100%" border="1" style="font-family: Arial,Helvetica,sans-serif;
        border-width: 1px; border-color: #000000; page-break-before: always;">
        <tr>
            <td style="width: 20%; font-size: 14px; border-left-style: none; text-align: center;
                border-right-style: none" valign="top">
                <asp:label id="lblJurCityc" runat="server" text=""></asp:label>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 20%; font-size: 14px; border-left-style: none; border-right-style: none"
                            valign="top">
                            <div style="text-align: left; width: 10px; float: left;">
                                <asp:image id="ImgLogoJainc" width="140px" height="90px" runat="server"></asp:image>
                            </div>
                        </td>
                        <td style="width: 50%; font-size: 12px; text-align: center; border-left-style: none;
                            border-right-style: none" align="center" valign="top">
                            <div id="Div3" runat="server" style="text-align: center; width: 500px;">
                                <strong>
                                    <asp:label id="lblCompanyname1c" runat="server" style="font-size: 14px;"></asp:label>
                                    <br />
                                </strong>
                                <asp:label id="Label7c" runat="server" text="(Fleet Owners & Transport Contractor)"></asp:label>
                                <br />
                                <strong>Head Office :
                                    <asp:label id="lblCompAdd3c" runat="server"></asp:label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:label id="lblCompAdd4c" runat="server"></asp:label>
                                    <asp:label id="lblCompCity1c" runat="server"></asp:label>
                                    &nbsp;&nbsp;
                                    <asp:label id="lblCompState1c" runat="server"></asp:label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:label id="lblCompCityPin1c" runat="server"></asp:label>
                                    <br />
                                    <b>GSTIN No. :&nbsp;</b><asp:label ID="lblConsignerCopyCompGSTIN" runat="server"></asp:label>
                                </strong>
                            </div>
                        </td>
                        <td style="width: 30%;" valign="top">
                            <table width="100%">
                                <tr>
                                    <td style="font-size: 13px; width: 25%;">
                                        <b>REF No.:</b>
                                    </td>
                                    <td style="font-size: 13px;">
                                        <asp:label id="lblRefNo3" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 13px; width: 25%;">
                                        <b>Booking No.:</b>
                                    </td>
                                    <td style="font-size: 13px;">
                                        <asp:label id="lblShipmentNo3" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="font-size: 13px; vertical-align: bottom; text-align: center;
                                        font-size: 13px;">
                                        <br />
                                        <b>CONSINGNOR COPY </b>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 33%;" valign="top">
                            <table border="1" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                <tr style="height: 50px;">
                                    <td align="center" colspan="2" style="border-bottom: 1px solid #484848; font-size: 13px;">
                                        <strong>CAUTION</strong>
                                    </td>
                                </tr>
                                <tr style="height: 52px;">
                                    <td colspan="2" style="font-size: 11px;">
                                        The Consignment will not be detained diverted,rerouted or re-booked without cosignee
                                        bank permission .<br />
                                        It will be delivered at the destination .
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="1" style="width: 100px; font-size: 13px;">
                                        <strong>No:</strong>
                                    </td>
                                    <td align="left" colspan="2" style="width: 150px; font-size: 13px;">
                                        <asp:label id="lblGRno1c" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="1" style="width: 100px; font-size: 13px;">
                                        <strong>Date:</strong>
                                    </td>
                                    <td align="left" colspan="2" style="width: 150px; font-size: 13px;">
                                        <asp:label id="lblGrDate1c" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="1" style="width: 100px; font-size: 13px;">
                                        <strong>Delivery at:</strong>
                                    </td>
                                    <td align="left" colspan="2" style="width: 150px; font-size: 13px;">
                                        <asp:label id="lbldeliveryatc" runat="server"></asp:label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 35%;" valign="top">
                            <table border="1" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                <tr style="height: 51px;">
                                    <td style="font-size: 15px; border-right-style: none; width: 25%;">
                                        <b>
                                            <asp:label id="lbltxtPanNo1c" runat="server" text="PAN NO. :"></asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 15px; border-right-style: none; width: 20%;">
                                        <asp:label id="lblPanNo1c" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr style="height: 53px;">
                                    <td style="border-bottom: 1px solid #484848; font-size: 15px; width: 25%;">
                                        <b>
                                            <asp:label id="lblTin1c" runat="server" text="Serv.Tax No.:"></asp:label>
                                        </b>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; font-size: 15px; width: 20%;">
                                        <asp:label id="lblCompTIN1c" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-bottom: 1px solid #484848; font-size: 13px; width: 25%;">
                                        <b>Service Tax Paid By</b>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; font-size: 13px; width: 20%;">
                                        <asp:label id="lbltaxpaidbyc" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>Invoice No.</b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <asp:label id="lblInvNoValue3" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                        <b>
                                            <asp:label id="lbltinnotextc" runat="server" text="Tin No.:"></asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <asp:label id="lbltinno3" runat="server"></asp:label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 33%;" valign="top">
                            <table border="1" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                <tr>
                                    <td align="center" colspan="2" style="border-bottom: 0px solid #484848; font-size: 13px;">
                                        <strong>NOTE</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" style="border-bottom: 1px solid #484848; font-size: 9px;">
                                        The Consignment Covered by this set of special lorry recipt from shall be stored
                                        at the destination under the control of the transport operator and shall be deliverd
                                        to or to the order of the cosignee bank whose name is mentioned in the lorry recipt
                                        .it will under no circumstances delivered to any one without the writeen authority
                                        from the consignee bank or its order endorsed on the consignee copy or on a seprate
                                        letter of authority
                                    </td>
                                </tr>
                                <tr style="height: 20px">
                                    <td colspan="1" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848;
                                        font-size: 13px;">
                                        <strong>Lorry No.</strong>
                                    </td>
                                    <td align="left" colspan="2" style="border-bottom: 1px solid #484848; width: 150px;
                                        font-size: 13px;">
                                        <asp:label id="lblLorryNo1c" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr style="height: 20px">
                                    <td colspan="1" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848;
                                        font-size: 13px;">
                                        <strong>FROM</strong>
                                    </td>
                                    <td align="left" colspan="2" style="border-bottom: 1px solid #484848; width: 150px;
                                        font-size: 13px;">
                                        <asp:label id="lblFromCity1c" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr style="height: 20px">
                                    <td colspan="1" style="width: 100px; border-bottom: 1px solid #484848; border-right: 1px solid #484848;
                                        font-size: 13px;">
                                        <strong>TO</strong>
                                    </td>
                                    <td align="left" colspan="2" style="border-bottom: 1px solid #484848; width: 150px;
                                        font-size: 13px;">
                                        <asp:label id="lblJainViac" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr style="height: 20px">
                                    <td colspan="1" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848;
                                        font-size: 13px;">
                                        <strong>TO</strong>
                                    </td>
                                    <td align="left" colspan="2" style="border-bottom: 1px solid #484848; width: 150px;
                                        font-size: 13px;">
                                        <asp:label id="lblToCity1c" runat="server"></asp:label>
                                    </td>
                            </table>
                        </td>
                    </tr>
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
                                    <td align="center" colspan="2" style="border-bottom: 1px solid #484848; font-size: 13px;">
                                        <strong>CONSIGNOR</strong>
                                    </td>
                                </tr>
                                <tr id="tr3" runat="server">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>
                                            <asp:label id="Label52c" runat="server">Name</asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblConsigeeName1c" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr style="height: 40px;" valign="top">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>
                                            <asp:label id="Label54c" runat="server">Address</asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblConsigneeAddress1c" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr valign="bottom">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>
                                            <asp:label id="lblCha4" runat="server"></asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblChaName4" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr id="ConsignerConsigGSTINNo" runat="server">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>GSTIN No.</b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblConsignerConsigGSTINNo" runat="server"></asp:label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 50%;" valign="top">
                            <table border="0" width="100%" class="white_bg">
                                <tr>
                                    <td align="center" colspan="2" style="border-bottom: 1px solid #484848; height: 10px;
                                        font-size: 13px;">
                                        <strong>CONSIGNEE</strong>
                                    </td>
                                </tr>
                                <tr id="tr4" runat="server">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>
                                            <asp:label id="lbltxtNamec" runat="server">Name</asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblConsignorName1c" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr style="height: 40px;" valign="top">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>
                                            <asp:label id="lblTxtAddc" runat="server">Address</asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblConsignorAddress1c" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr valign="bottom">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>
                                            <asp:label id="lblFor3" runat="server"></asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblForName3" runat="server"></asp:label>
                                    </td>
                                </tr>
                                 <tr id="ConsignerConsigneeGSTINNo" runat="server">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>GSTIN No.</b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblConsignerConsigneeGSTINNo" runat="server"></asp:label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 40%;" valign="top">
                            <table border="0" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                <tr>
                                    <td align="center" style="border-bottom: 1px solid #484848; border-left: 1px solid #484848;
                                        border-top: 1px solid #484848; border-right: 1px solid #484848; font-size: 13px;">
                                        <strong>Packages</strong>
                                    </td>
                                    <td id="Td3" runat="server" align="center" style="border-bottom: 1px solid #484848;
                                        border-left: 0px solid #484848; border-top: 1px solid #484848; border-right: 0px solid #484848;
                                        font-size: 13px;">
                                        <strong>Description (Said to Contain)</strong>
                                    </td>
                                </tr>
                                <tr style="height: 32px">
                                    <td align="center" style="border-bottom: 1px solid #484848; border-right: 1px solid #484848;
                                        font-size: 13px;">
                                    </td>
                                    <td id="Td4" runat="server" align="left" style="border-bottom: 1px solid #484848;">
                                        <asp:label id="lblDetails3" wrap="true" runat="server"></asp:label>
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" style="border-bottom: 0px solid #484848; border-right: 0px solid #484848;
                                        font-size: 11px;">
                                        <strong>Declration for cenvat credit:</strong>We hereby certify that we have not
                                        availed credit of duty paid on input or capital goods unser the provisions of cenvat
                                        credit rules 2004 nor we have availed the benefit of notification no.12/2003-ST
                                        dated 20.06.2003
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 60%;" valign="top">
                            <table border="0" width="100%" class="white_bg" style="border-right: 0px solid #484848;">
                                <tr style="height: 40px">
                                    <td style="width: 220px; border-bottom: 1px solid #484848; border-left: 1px solid #484848;
                                        border-top: 1px solid #484848; border-right: 1px solid #484848; font-size: 13px;">
                                        <strong>Packages</strong>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                        border-right: 1px solid #484848; font-size: 13px;">
                                        <strong>Weight Actual</strong>
                                    </td>
                                    <td style="width: 70px; border-bottom: 1px solid #484848; border-left: 1px solid #484848;
                                        border-top: 1px solid #484848; border-right: 1px solid #484848; font-size: 13px;">
                                        <strong>Rate</strong>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                        border-right: 1px solid #484848; width: 150px; font-size: 13px;">
                                        <strong><b>
                                            <asp:label runat="server" id="AmountType3" text=""></asp:label>
                                        </b></strong>
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td style="border-left: 1px solid #484848; border-bottom: 1px solid #484848; width: 220px;
                                        border-right: 1px solid #484848; font-size: 13px;">
                                        <strong>Container No.</strong><asp:label id="lblJainContainerNoc" runat="server"></asp:label>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                        G.Wt.
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                        border-right: 1px solid #484848; width: 70px; font-size: 13px;">
                                        Labour Ch.
                                    </td>
                                    <td align="center" style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td style="border-left: 1px solid #484848; font-size: 13px; width: 220px; border-bottom: 1px solid #484848;
                                        border-right: 1px solid #484848;">
                                        <strong>
                                            <label id="container5" visible="false" runat="server">
                                                Container No2.</label></strong><asp:label id="lblContainerNoc1" visible="false" runat="server"></asp:label>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                        Cont.Wt.
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                        border-right: 1px solid #484848; width: 70px; font-size: 13px;">
                                        Bilty Ch.
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td style="border-left: 1px solid #484848; border-bottom: 1px solid #484848; width: 220px;
                                        border-right: 1px solid #484848; font-size: 13px;">
                                        <strong>Seal No.</strong><asp:label id="lblJainSealNoc" runat="server"></asp:label>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                        T.Wt.
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                        border-right: 1px solid #484848; width: 70px; font-size: 13px;">
                                        Statist Ch.
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td align="left" style="border-left: 1px solid #484848; width: 220px; border-right: 1px solid #484848;
                                        border-bottom: 1px solid #484848; font-size: 13px;">
                                        <strong>
                                            <label id="seal3" visible="false" runat="server">
                                                Seal No2.</label></strong><asp:label id="lblJainSealNoc1" visible="false" runat="server"></asp:label>
                                    </td>
                                    <td style="border-bottom: 0px solid #484848; width: 150px; font-size: 13px;">
                                        &nbsp;
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                        border-right: 1px solid #484848; width: 70px; font-size: 13px;">
                                        Detention
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td align="center" style="border-left: 1px solid #484848; width: 150px; border-right: 1px solid #484848;
                                        border-bottom: 1px solid #484848;">
                                        <strong>AT OWNER RISK </strong>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                        Net
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                        border-right: 1px solid #484848; width: 70px; font-size: 13px;">
                                        G.Total
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                    </td>
                                </tr>
                                <asp:hiddenfield id="hidPagesc" runat="server" />
                                <tr>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td align="left" style="font-size: 11px;">
                            <strong>Note:</strong> We are not responsible for leakage,breaking &amp; damage,Consinger
                            is resposible for contralandor restricted goods
                        </td>
                        <td align="center" style="border-bottom: 1px solid #484848; width: 33%; border-right: 1px solid #484848;
                            border-left: 1px solid #484848; border-top: 1px solid #484848; font-size: 13px;">
                            <strong>
                                <asp:label id="lblGrtype2" runat="server" text=""></asp:label>
                            </strong>
                        </td>
                        <td align="right" style="width: 33%; vertical-align: top; font-size: 13px;">
                            For: <strong>
                                <asp:label id="lblauthc" runat="server" text="Authorised Signatory"></asp:label>
                            </strong>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
<div class="col-lg-0" id="DivCosignee" runat="server" visible="false">
    <table cellpadding="1" cellspacing="0" width="100%" border="1" style="font-family: Arial,Helvetica,sans-serif;
        border-width: 1px; border-color: #000000; page-break-before: always;">
        <tr>
            <td style="width: 20%; font-size: 14px; border-left-style: none; text-align: center;
                border-right-style: none" valign="top">
                <asp:label id="lblJurCityd" runat="server" text=""></asp:label>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 20%; font-size: 14px; border-left-style: none; border-right-style: none"
                            valign="top">
                            <div style="text-align: left; width: 10px; float: left;">
                                <asp:image id="ImgLogoJaind" width="140px" height="90px" runat="server"></asp:image>
                            </div>
                        </td>
                        <td style="width: 50%; font-size: 12px; text-align: center; border-left-style: none;
                            border-right-style: none" align="center" valign="top">
                            <div id="Div4" runat="server" style="text-align: center; width: 500px;">
                                <strong>
                                    <asp:label id="lblCompanyname1d" runat="server" style="font-size: 14px;"></asp:label>
                                    <br />
                                </strong>
                                <asp:label id="Label7d" runat="server" text="(Fleet Owners & Transport Contractor)"></asp:label>
                                <br />
                                <strong>Head Office :
                                    <asp:label id="lblCompAdd3d" runat="server"></asp:label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:label id="lblCompAdd4d" runat="server"></asp:label>
                                    <asp:label id="lblCompCity1d" runat="server"></asp:label>
                                    &nbsp;&nbsp;
                                    <asp:label id="lblCompState1d" runat="server"></asp:label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:label id="lblCompCityPin1d" runat="server"></asp:label>
                                    <br />
                                    <b>GSTIN No. :&nbsp;</b><asp:label ID="lblConsigneeCopyCompGSTIN" runat="server"></asp:label>
                                </strong>
                            </div>
                        </td>
                        <td style="width: 30%;" valign="top">
                            <table width="100%">
                                <tr>
                                    <td style="font-size: 13px; width: 25%;">
                                        <b>REF No.:</b>
                                    </td>
                                    <td style="font-size: 13px;">
                                        <asp:label id="lblRefNo4" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 13px; width: 25%;">
                                        <b>Booking No.:</b>
                                    </td>
                                    <td style="font-size: 13px;">
                                        <asp:label id="lblShipmentNo4" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="font-size: 13px; vertical-align: bottom; text-align: center;
                                        font-size: 13px;">
                                        <br />
                                        <b>CONSINGNEE COPY </b>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 33%;" valign="top">
                            <table border="1" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                <tr style="height: 50px;">
                                    <td align="center" colspan="2" style="border-bottom: 1px solid #484848; font-size: 13px;">
                                        <strong>CAUTION</strong>
                                    </td>
                                </tr>
                                <tr style="height: 52px;">
                                    <td colspan="2" style="font-size: 11px;">
                                        The Consignment will not be detained diverted,rerouted or re-booked without cosignee
                                        bank permission .<br />
                                        It will be delivered at the destination .
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="1" style="width: 100px; font-size: 13px;">
                                        <strong>No:</strong>
                                    </td>
                                    <td align="left" colspan="2" style="width: 150px; font-size: 13px;">
                                        <asp:label id="lblGRno1d" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="1" style="width: 100px; font-size: 13px;">
                                        <strong>Date:</strong>
                                    </td>
                                    <td align="left" colspan="2" style="width: 150px; font-size: 13px;">
                                        <asp:label id="lblGrDate1d" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="1" style="width: 100px; font-size: 13px;">
                                        <strong>Delivery at:</strong>
                                    </td>
                                    <td align="left" colspan="2" style="width: 150px; font-size: 13px;">
                                        <asp:label id="lbldeliveryatd" runat="server"></asp:label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 35%;" valign="top">
                            <table border="1" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                <tr style="height: 51px;">
                                    <td style="font-size: 15px; border-right-style: none; width: 25%;">
                                        <b>
                                            <asp:label id="lbltxtPanNo1d" runat="server" text="PAN NO. :"></asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 15px; border-right-style: none; width: 20%;">
                                        <asp:label id="lblPanNo1d" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr style="height: 53px;">
                                    <td style="border-bottom: 1px solid #484848; font-size: 15px; width: 25%;">
                                        <b>
                                            <asp:label id="lblTin1d" runat="server" text="Serv.Tax No.:"></asp:label>
                                        </b>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; font-size: 15px; width: 20%;">
                                        <asp:label id="lblCompTIN1d" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-bottom: 1px solid #484848; font-size: 13px; width: 25%;">
                                        <b>Service Tax Paid By</b>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; font-size: 13px; width: 20%;">
                                        <asp:label id="lbltaxpaidbyd" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>Invoice No.</b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <asp:label id="lblInvNoValue4" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                        <b>
                                            <asp:label id="lbltinnotextd" runat="server" text="Tin No.:"></asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <asp:label id="lbltinno4" runat="server"></asp:label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 33%;" valign="top">
                            <table border="1" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                <tr>
                                    <td align="center" colspan="2" style="border-bottom: 0px solid #484848; font-size: 13px;">
                                        <strong>NOTE</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" style="border-bottom: 1px solid #484848; font-size: 9px;">
                                        The Consignment Covered by this set of special lorry recipt from shall be stored
                                        at the destination under the control of the transport operator and shall be deliverd
                                        to or to the order of the cosignee bank whose name is mentioned in the lorry recipt
                                        .it will under no circumstances delivered to any one without the writeen authority
                                        from the consignee bank or its order endorsed on the consignee copy or on a seprate
                                        letter of authority
                                    </td>
                                </tr>
                                <tr style="height: 20px">
                                    <td colspan="1" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848;
                                        font-size: 13px;">
                                        <strong>Lorry No.</strong>
                                    </td>
                                    <td align="left" colspan="2" style="border-bottom: 1px solid #484848; width: 150px;
                                        font-size: 13px;">
                                        <asp:label id="lblLorryNo1d" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr style="height: 20px">
                                    <td colspan="1" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848;
                                        font-size: 13px;">
                                        <strong>FROM</strong>
                                    </td>
                                    <td align="left" colspan="2" style="border-bottom: 1px solid #484848; width: 150px;
                                        font-size: 13px;">
                                        <asp:label id="lblFromCity1d" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr style="height: 20px">
                                    <td colspan="1" style="width: 100px; border-bottom: 1px solid #484848; border-right: 1px solid #484848;
                                        font-size: 13px;">
                                        <strong>TO</strong>
                                    </td>
                                    <td align="left" colspan="2" style="border-bottom: 1px solid #484848; width: 150px;
                                        font-size: 13px;">
                                        <asp:label id="lblJainViad" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr style="height: 20px">
                                    <td colspan="1" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848;
                                        font-size: 13px;">
                                        <strong>TO</strong>
                                    </td>
                                    <td align="left" colspan="2" style="border-bottom: 1px solid #484848; width: 150px;
                                        font-size: 13px;">
                                        <asp:label id="lblToCity1d" runat="server"></asp:label>
                                    </td>
                            </table>
                        </td>
                    </tr>
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
                                    <td align="center" colspan="2" style="border-bottom: 1px solid #484848; font-size: 13px;">
                                        <strong>CONSIGNOR</strong>
                                    </td>
                                </tr>
                                <tr id="tr5" runat="server">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>
                                            <asp:label id="Label52d" runat="server">Name</asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblConsigeeName1d" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr style="height: 40px;" valign="top">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>
                                            <asp:label id="Label54d" runat="server">Address</asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblConsigneeAddress1d" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr valign="bottom">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>
                                            <asp:label id="lblCha1" runat="server"></asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblChaName1" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr id="ConsigneeConsigGSTINNo" runat="server">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>GSTIN No.</b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblConsigneeConsigGSTINNo" runat="server"></asp:label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 50%;" valign="top">
                            <table border="0" width="100%" class="white_bg">
                                <tr>
                                    <td align="center" colspan="2" style="border-bottom: 1px solid #484848; height: 10px;
                                        font-size: 13px;">
                                        <strong>CONSIGNEE</strong>
                                    </td>
                                </tr>
                                <tr id="tr6" runat="server">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>
                                            <asp:label id="lbltxtNamed" runat="server">Name</asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblConsignorName1d" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr style="height: 40px;" valign="top">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>
                                            <asp:label id="lblTxtAddd" runat="server">Address</asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblConsignorAddress1d" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr valign="bottom">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>
                                            <asp:label id="lblFor4" runat="server"></asp:label>
                                        </b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblForName4" runat="server"></asp:label>
                                    </td>
                                </tr>
                                 <tr id="ConsigneeConsigneeGSTINNo" runat="server">
                                    <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                        <b>GSTIN No.</b>
                                    </td>
                                    <td style="font-size: 13px; border-right-style: none">
                                        <asp:label id="lblConsigneeConsigneeGSTINNo" runat="server"></asp:label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 40%;" valign="top">
                            <table border="0" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                <tr>
                                    <td align="center" style="border-bottom: 1px solid #484848; border-left: 1px solid #484848;
                                        border-top: 1px solid #484848; border-right: 1px solid #484848; font-size: 13px;">
                                        <strong>Packages</strong>
                                    </td>
                                    <td id="Td5" runat="server" align="center" style="border-bottom: 1px solid #484848;
                                        border-left: 0px solid #484848; border-top: 1px solid #484848; border-right: 0px solid #484848;
                                        font-size: 13px;">
                                        <strong>Description (Said to Contain)</strong>
                                    </td>
                                </tr>
                                <tr style="height: 32px">
                                    <td align="center" style="border-bottom: 1px solid #484848; border-right: 1px solid #484848;
                                        font-size: 13px;">
                                    </td>
                                    <td id="Td6" runat="server" align="left" style="border-bottom: 1px solid #484848;">
                                        <asp:label id="lblDetails4" wrap="true" runat="server"></asp:label>
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" style="border-bottom: 0px solid #484848; border-right: 0px solid #484848;
                                        font-size: 11px;">
                                        <strong>Declration for cenvat credit:</strong>We hereby certify that we have not
                                        availed credit of duty paid on input or capital goods unser the provisions of cenvat
                                        credit rules 2004 nor we have availed the benefit of notification no.12/2003-ST
                                        dated 20.06.2003
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 60%;" valign="top">
                            <table border="0" width="100%" class="white_bg" style="border-right: 0px solid #484848;">
                                <tr style="height: 40px">
                                    <td style="width: 220px; border-bottom: 1px solid #484848; border-left: 1px solid #484848;
                                        border-top: 1px solid #484848; border-right: 1px solid #484848; font-size: 13px;">
                                        <strong>Packages</strong>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                        border-right: 1px solid #484848; font-size: 13px;">
                                        <strong>Weight Actual</strong>
                                    </td>
                                    <td style="width: 70px; border-bottom: 1px solid #484848; border-left: 1px solid #484848;
                                        border-top: 1px solid #484848; border-right: 1px solid #484848; font-size: 13px;">
                                        <strong>Rate</strong>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                        border-right: 1px solid #484848; width: 150px; font-size: 13px;">
                                        <strong><b>
                                            <asp:label runat="server" id="AmountType4" text="Label"></asp:label>
                                        </b></strong>
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td style="border-left: 1px solid #484848; border-bottom: 1px solid #484848; width: 220px;
                                        border-right: 1px solid #484848; font-size: 13px;">
                                        <strong>Container No.</strong><asp:label id="lblJainContainerNod" runat="server"></asp:label>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                        G.Wt.
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                        border-right: 1px solid #484848; width: 70px; font-size: 13px;">
                                        Labour Ch.
                                    </td>
                                    <td align="center" style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td style="border-left: 1px solid #484848; font-size: 13px; width: 220px; border-bottom: 1px solid #484848;
                                        border-right: 1px solid #484848;">
                                        <strong>
                                            <label id="Container4" visible="false" runat="server">
                                                Container No2.</label></strong><asp:label id="lblContainerNod1" visible="false" runat="server"></asp:label>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                        Cont.Wt.
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                        border-right: 1px solid #484848; width: 70px; font-size: 13px;">
                                        Bilty Ch.
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td style="border-left: 1px solid #484848; border-bottom: 1px solid #484848; width: 220px;
                                        border-right: 1px solid #484848; font-size: 13px;">
                                        <strong>Seal No.</strong>
                                        <asp:label id="lblJainSealNod" runat="server"></asp:label>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                        T.Wt.
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                        border-right: 1px solid #484848; width: 70px; font-size: 13px;">
                                        Statist Ch.
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td align="left" style="border-left: 1px solid #484848; width: 220px; border-right: 1px solid #484848;
                                        border-bottom: 1px solid #484848; font-size: 13px;">
                                        <strong>
                                            <label id="Seal4" visible="false" runat="server">
                                                Seal No2.</label></strong>
                                        <asp:label id="lblJainSealNod1" visible="false" runat="server"></asp:label>
                                    </td>
                                    <td style="border-bottom: 0px solid #484848; width: 150px; font-size: 13px;">
                                        &nbsp;
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                        border-right: 1px solid #484848; width: 70px; font-size: 13px;">
                                        Detention
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td align="center" style="border-left: 1px solid #484848; width: 150px; border-right: 1px solid #484848;
                                        border-bottom: 1px solid #484848;">
                                        <strong>AT OWNER RISK </strong>
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                        Net
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; border-left: 1px solid #484848; border-top: 1px solid #484848;
                                        border-right: 1px solid #484848; width: 70px; font-size: 13px;">
                                        G.Total
                                    </td>
                                    <td style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                    </td>
                                </tr>
                                <asp:hiddenfield id="hidPagesd" runat="server" />
                                <tr>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td align="left" style="font-size: 11px;">
                            <strong>Note:</strong> We are not responsible for leakage,breaking &amp; damage,Consinger
                            is resposible for contralandor restricted goods
                        </td>
                        <td align="center" style="border-bottom: 1px solid #484848; width: 33%; border-right: 1px solid #484848;
                            border-left: 1px solid #484848; border-top: 1px solid #484848; font-size: 13px;">
                            <strong>
                                <asp:label id="lblGrtype3" runat="server" text=""></asp:label>
                            </strong>
                        </td>
                        <td align="right" style="width: 33%; vertical-align: top; font-size: 13px;">
                            For: <strong>
                                <asp:label id="lblauthd" runat="server" text="Authorised Signatory"></asp:label>
                            </strong>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
</form>
</html>
