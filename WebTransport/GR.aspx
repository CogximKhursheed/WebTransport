<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GR.aspx.cs" Inherits="WebTransport.GR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
           <%-- <div id="print" style="font-size: 13px; background-color: #ffffff;">--%>
                <table width="100%" height="100%" cellspacing="0" cellpadding="0" bgcolor="#eee" style="font-family:Arial">
                    <tr>
                        <td>
                            <table width="900" border="1" cellpadding="0" align="center" cellspacing="0" bgcolor="#fff">
                                <tr class="odd">
                                    <td colspan="3" rowspan="3">Subject to Dwarka Jurisdiction 9909915745 / 98796108481 9879610849 / 9879614245
                                        <br>
                                        <strong style="font-size: 45px; font-weight: bold; display: block; text-align: center; font-family: tim; text-decoration: underline">Westend Roadlines</strong>
                                        <br>
                                        Godown Area, Mithapur - 361345.<br>
                                        Email id: westendsoadlines@gmail.com<br>
                                        <b>PAN</b>:ABTPB3929P I <b>STN:</b>ABTPB3929PSD001
                                    </td>
                                    <td width="100" height="50"><b>TAX Invoice No:</b> <asp:Literal ID="lblTexInvoice" runat="server" > </asp:Literal> </td>
                                    <td width="200" height="50"><b>G.C. No:</b> <asp:Literal ID="lblGRno"  runat="server"></asp:Literal></td>
                                </tr>
                                <tr class="odd">
                                    <td width="100" height="50"><b>EXCISE INVOICE No:</b> <asp:Literal ID="lblExciceInvoice" runat="server"></asp:Literal></td>
                                    <td width="200" height="50"><b>Date:</b> <asp:Label ID="lblGrDate" runat="server"> </asp:Label> </td>
                                </tr>
                                <tr class="odd">
                                    <td colspan="2" width="100" height="50"><b>Order No:</b> <asp:Literal ID="lblOrderNO" runat="server"></asp:Literal></td>
                                </tr>
                                <tr class="odd">
                                    <td height="50"><b>Consignor : <asp:Label ID="lblConsignorName1" Style="font-size: 15px;"   runat="server"></asp:Label> </b></td>
                                    <td height="50" colspan="4">Consignee : <b> <asp:Label ID="lblConsigneeName1" Style="font-size: 15px;"   runat="server"></asp:Label></b> <br />
                                        <b>Address :</b> <asp:Label ID="lblConsigneeAddress1" Style="font-size: 15px;"   runat="server"></asp:Label> </td>
                                </tr>
                                <tr class="odd">
                                    <td width="200" height="50"><b>Receiver City Details </b></td>
                                    <td height="50" width="80"><b>Truck Number</b></td>
                                    <td height="50" width="200"><b>Packets</b></td>
                                    <td height="50" width="100"><b>Weight(Metric Ton)</b></td>
                                    <td height="50" width="200"><b>Remarks</b> <asp:Literal ID="lblRemark" runat="server"> </asp:Literal> </td>
                                </tr>
                                <tr class="odd">
                                    <td height="50"><b>City:</b> <asp:Literal ID="lblDeliverCity" runat="server"></asp:Literal> </td>
                                    <td rowspan="3" height="50"> <asp:Literal ID="lblLorryNo" runat="server"> </asp:Literal></td>
                                    <td rowspan="3" height="50" valign="top"> <asp:Literal ID="lblQty" runat="server"> </asp:Literal> </td>
                                    <td rowspan="3" height="50" valign="top"> <asp:Literal ID="lblWeight" runat="server"> </asp:Literal> <br /> <asp:Literal ID="lblItemName" runat="server"> </asp:Literal> </td>
                                    <td height="50"><b>Total Amount:</b> <asp:Label ID="lblTotalAMT" runat="server"> </asp:Label> </td>
                                </tr>
                                <tr class="odd">
                                    <td height="100"><b>Taluka:</b> <asp:Literal ID="lblDeliverPlace" runat="server"> </asp:Literal> </td>
                                    <td height="100"><b>Advance: </b> <asp:Literal ID="lblAdvance" runat="server"> </asp:Literal> <br />
                                        <b>Fuel Payment:</b> <asp:Literal ID="lblFuelPayment" runat="server"> </asp:Literal> </td>
                                </tr>
                                <tr class="odd">
                                    <td height="50"><b>District: </b> <asp:Literal ID="lblDeliverDistrict" runat="server"> </asp:Literal> </td>
                                    <td height="50"><b>Total Advance:</b> <asp:Literal ID="lblTotAdvance" runat="server"> </asp:Literal> </td>
                                </tr>
                                <tr class="odd">
                                    <td height="100" colspan="2"><b>Truck Owner:</b> <asp:Literal ID="lblLorryOwner" runat="server"> </asp:Literal> </td>
                                    <td height="100" rowspan="5" colspan="3" valign="top" style="line-height: 35px;"><span style="color: rgb(0, 0, 0); font-family: sans-serif; font-size: 15px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">I/We declare that we have not taken credit of Excise Duty paid on inputs or Capital goods or credit of Service Tax paid on input for providing &#39;Transportation of Goods by Road&#39; service under the provision of Cenvat Credit Rules 2004.</span><br style="color: rgb(0, 0, 0); font-family: sans-serif; font-size: 15px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;" />
                                        <span style="color: rgb(0, 0, 0); font-family: sans-serif; font-size: 15px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">I/We also declare that we have no availed the benefit under notification No. 12/2003-ST dated 20/06/2003.</span><br />
                                        <strong style="color: rgb(0, 0, 0); font-family: sans-serif; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; border: 2px solid rgb(0, 0, 0); font-size: 26px; font-weight: 400;">SERVICE TAX TO BE PAID BY CONSIGNOR</strong>
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <span style="border-top: 2px solid #000; font-weight: bold; display: block; text-align: center;">Sign & Stamp for confirmation of delivery of goods in good condition</span>
                                    </td>
                                </tr>
                                <tr class="odd">
                                    <td height="50" colspan="2"><b>Address:</b> <asp:Literal ID="lblLorryAddress" runat="server"> </asp:Literal></td>
                                </tr>
                                <tr class="odd">
                                    <td height="50" colspan="2"><b>Truck Driver:</b> <asp:Literal ID="lblLorryDriver" runat="server"> </asp:Literal>  ()</td>
                                </tr>
                                <tr class="odd">
                                    <td height="200" colspan="2"><span style="color: rgb(0, 0, 0); font-family: sans-serif; font-size: 15px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">1)Truck Owner is responsible to deliever goods in proper condition.</span><br style="color: rgb(0, 0, 0); font-family: sans-serif; font-size: 15px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;" />
                                        <span style="color: rgb(0, 0, 0); font-family: sans-serif; font-size: 15px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">2)Truck Owner is responsible for damage cement.</span><br style="color: rgb(0, 0, 0); font-family: sans-serif; font-size: 15px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;" />
                                        <span style="color: rgb(0, 0, 0); font-family: sans-serif; font-size: 15px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">3)Octroi and ilw charges are to be beared by Receiver Company.</span><br style="color: rgb(0, 0, 0); font-family: sans-serif; font-size: 15px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;" />
                                        <span style="color: rgb(0, 0, 0); font-family: sans-serif; font-size: 15px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">4)Collect the amount in 10th day.</span></td>
                                </tr>
                                <tr class="odd">
                                    <td height="150" colspan="2" valign="bottom" align="center"><b>
                                       
                                        For, West End oadlines</b></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
             <asp:HiddenField ID="hidPages" runat="server" />
        <%--</div>--%>
         <script language="javascript" type="text/javascript">
          function CallPrint(strid) {
              var prtContent = "";
              var Pages = "1";
              Pages = document.getElementById("<%=hidPages.ClientID%>").value;
              var prtContent3 = "<p style='page-break-before: always'></p>";
              for (i = 0; i < Pages; i++) {
                  prtContent = prtContent + "<table width='100%' border='0' CELLPADDING='10'></table>";
                  if (Pages != 1) {
                     //prtContent = prtContent + "<tr><td><strong>" + ((i == 1) ? "[Office Copy]" : (i == 2) ? "[Consignor Copy]" : (i == 3) ? "[Consignee Copy]" : "[Driver Copy]") + "</strong></td></tr>";
                      prtContent = prtContent + "<tr><td><strong> [Office Copy] </strong></td></tr>";
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
