<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintManualTrip.aspx.cs" Inherits="WebTransport.PrintManualTrip" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<div id="PrintPage">
    <form id="form1" runat="server">
    
        <style>
            table td {
                border: 1px solid;
            }
            table {
                width: 100%;
                border-collapse: collapse;
            }
        </style>
        <table id="printmanualtrip">
            <tbody>
                <tr id="header">
                    <td>
                        <h2 style="margin:0;padding:0;font-weight:bold;text-align:center"><b>SUBHAM TRANSPOT TRAILOR</b></h1>
                        <h3 style="margin:0;padding:0;font-weight:400 ;text-align:center"><asp:Label ID="lblCompDescr" runat="server" Text=""></asp:Label></h1>
                        <center><asp:Label ID="lblAddress" runat="server" Text="--NO VALUE--"></asp:Label></center>
                        <center><asp:Label ID="lblPhoneNo" runat="server" Text="--NO VALUE--"></asp:Label></center>
                        <center><asp:Label ID="lblEmail" runat="server" Text="--NO VALUE--"></asp:Label></center>
                        <center><asp:Label ID="lblPanNo" runat="server" Text="--NO VALUE--"></asp:Label></center>
                    </td>
                </tr>
                <tr id="heading"><td><h2 style="margin:0;padding:0;font-weight:bold;text-align:center"><u>TRIP SHEET</u></h2></td></tr>
                <tr id="tripdetail">
                    <table>
                        <tbody>
                            <tr>
                                <td>TRIP. No.</td>
                                <td>
                                    <asp:Label ID="lblTripNo" runat="server" Text="--NO VALUE--"></asp:Label></td>
                                <td>TRIP. Date</td>
                                <td>
                                    <asp:Label ID="lblTripDate" runat="server" Text="--NO VALUE--"></asp:Label></td>
                                <td>Lorry No.</td>
                                <td>
                                    <asp:Label ID="lblTruckNo" runat="server" Text="--NO VALUE--"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>FROM</td>
                                <td>
                                    <asp:Label ID="lblFromCity" runat="server" Text="--NO VALUE--"></asp:Label></td>
                                <td>TO</td>
                                <td>
                                    <asp:Label ID="lblToCity" runat="server" Text="--NO VALUE--"></asp:Label></td>
                                <td>DRIVER</td>
                                <td>
                                    <asp:Label ID="lblDriverName" runat="server" Text="--NO VALUE--"></asp:Label></td>
                            </tr>
                        </tbody>
                    </table>
                </tr>
                <tr><td><center><b>Party Details</b></center></td></tr>
                <tr id="partydetail">
                    <table>
                        <tbody>
                            <tr>
                                <td>PARTY NAME</td>
                                <td colspan="2"><asp:Label ID="lblSender" runat="server" Text="--NO VALUE--"></asp:Label></td>
                                <td>K.M. FROM</td>
                                <td><asp:Label ID="lblStartKms" runat="server" Text="--NO VALUE--"></asp:Label></td>
                                <td>K.M. TO</td>
                                <td><asp:Label ID="lblEndKms" runat="server" Text="--NO VALUE--"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>S.NO.</td>
                                <td>ITEM NAME</td>
                                <td>UNIT NAME</td>
                                <td>QUANTITY</td>
                                <td>WEIGHT</td>
                                <td>FREIGHT RATE</td>
                                <td>FREIGHT AMOUNT</td>
                            </tr>
                             <tr>
                                <td><asp:Label ID="lblSNo" runat="server" Text="1"></asp:Label></td>
                                <td><asp:Label ID="lblItemName" runat="server" Text="--NO VALUE--"></asp:Label></td>
                                <td><asp:Label ID="lblItemSize" runat="server" Text="--NO VALUE--"></asp:Label></td>
                                <td><asp:Label ID="lblQuantity" runat="server" Text="--NO VALUE--"></asp:Label></td>
                                <td><asp:Label ID="lblGweight" runat="server" Text="--NO VALUE--"></asp:Label></td>
                                <td><asp:Label ID="lblrate" runat="server" Text="--NO VALUE--"></asp:Label></td>
                                <td><asp:Label ID="lblTotalAmount" runat="server" Text="--NO VALUE--"></asp:Label></td>
                            </tr>
                             <tr>
                                <td>RTO CHALLAN</td>
                                <td><asp:Label ID="lblRTOChallan" runat="server" Text="--NO VALUE--"></asp:Label></td>
                                <td>DETENTION</td>
                                <td><asp:Label ID="lblDetention" runat="server" Text="--NO VALUE--"></asp:Label></td>
                                <td>&nbsp;</td>
                                <td>Freight Total</td>
                                <td><asp:Label ID="lblTotalFreight" runat="server" Text="--NO VALUE--"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>PARTY ADVANCE</td>
                                <td><asp:Label ID="lblAdvance" runat="server" Text="--NO VALUE--"></asp:Label></td>
                                <td>COMMISSION</td>
                                <td><asp:Label ID="lblCommission" runat="server" Text="--NO VALUE--"></asp:Label></td>
                                <td>&nbsp;</td>
                                <td>TOTAL PARTY ADVANCE</td>
                                <td><asp:Label ID="lblTotalPartyAdv" runat="server" Text="--NO VALUE--"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>TOTAL PARTY BALANCE</td>
                                <td><asp:Label ID="lblTotalPartyBalance" runat="server" Text="--NO VALUE--"></asp:Label></td>
                            </tr>
                        </tbody>
                    </table>
                </tr>
                <%if (PrintType == 1)
                  { %>
                <tr><td><center><b>Vehicle Details</b></center></td></tr>
                <tr id="vecicledetail">
                      <table>
                        <tbody>
                            <tr>
                                <td>V. ADVANCE</td>
                                <td><asp:Label ID="lblDriver" runat="server" Text="--NO VALUE--"></asp:Label></td>
                                <td>DIESEL</td>
                                <td><asp:Label ID="lblDiesel" runat="server" Text="--NO VALUE--"></asp:Label></td>
                                <td>DRIVER</td>
                                <td><asp:Label ID="lblDriverAc" runat="server" Text="--NO VALUE--"></asp:Label></td>
                                <td>TOTAL V. ADV.</td>
                                <td><asp:Label ID="lblTotalVehAdv" runat="server" Text="--NO VALUE--"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>TRIP BALANCE</td>
                                <td><asp:Label ID="lblNetTripProfit" runat="server" Text="--NO VALUE--"></asp:Label></td>
                            </tr>
                        </tbody>
                    </table>
                </tr>
                <%} %>
            </tbody>
        </table>
    
    <script src="js/jquery-1.12.0.min.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            CallPrint();
        });
        function CallPrint() {
//            var prtContent = "";

//            var prtContent1 = document.getElementById("PrintPage");
//            var prtContent2 = prtContent1.innerHTML;
//            prtContent = prtContent2;
//            var WinPrint = window.open('', '', 'left=0,top=0,width=700px,height=450px,toolbar=1,scrollbars=1,status=1');
//            WinPrint.document.write(prtContent2);
//            WinPrint.document.close();
//            WinPrint.focus();
//            WinPrint.print();
//            WinPrint.close();
//            return false;
        }
    </script>
    </form>
    </div>
</body>
</html>
