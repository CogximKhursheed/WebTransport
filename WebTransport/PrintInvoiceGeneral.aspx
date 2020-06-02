<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintInvoiceGeneral.aspx.cs"
    Inherits="WebTransport.PrintInvoiceGeneral" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="print" style="font-size: 11px;">
        <asp:HiddenField ID="hidCheckDupGRNo" runat="server" />
        <table style="width:1024px;">
            <tr id="header" runat="server">
                <td style=" text-align:center; font-size:15px;" colspan="2">
                    <%--Header--%>
                    <%--<b>Tax Invoice</b>--%>
                    <br />
                    <asp:Label ID="lblCompName" Font-Bold="true" Font-Size="Medium" runat="server" Text=""></asp:Label>
                    <br/>
                    <asp:Label style="font-size:12px;" ID="lblCompAddress" runat="server" Text=""></asp:Label>
                    <br/>
                    <asp:Label style="font-size:12px;" ID="lblCompPhone" runat="server" Text=""></asp:Label>
                    <br/>
                    <asp:Label style="font-size:12px;" ID="lblPAN" runat="server" Text=""></asp:Label>
                     &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label style="font-size:12px;" ID="lblGST" runat="server" Text=""></asp:Label>
                    
                </td>
            </tr>
            <%--END--%>
            <%--Sender --%>
            <tr>
                <td style=" text-align:left;">
                            <b><asp:Label ID="lblSenderName" runat="server" Text=""></asp:Label></b>
                            <br />
                            <asp:Label ID="lblSenderAddress" runat="server" Text=""></asp:Label>
                            <%if (lblSenderPAN.Text != "")
                              { %>
                            <br />
                            <asp:Label ID="lblSenderPAN" runat="server" Text=""></asp:Label>
                            <%} if (lblSenderGST.Text != "")
                              { %>
                            <br />
                            <asp:Label ID="lblSenderGST" runat="server" Text=""></asp:Label>
                            <%} %>
                </td>
             
                <td style=" text-align:left;">
             
                    <label >FREIGHT BILL NO:</label>
                        <asp:Label ID="lblBillNo" runat="server" Text=""></asp:Label>
                    <br />
                        <label class="col-sm-4"> BILL DATE :</label>
                        <asp:Label ID="lblBillDate" runat="server" Text=""></asp:Label>
                    <br />
                        <label class="col-sm-4"> State Code :</label>
                        <asp:Label ID="lblSupplyStateId" runat="server" Text=""></asp:Label>
                    <br />
                        <label class="col-sm-4"> Place Of Supply :</label>
                        <asp:Label ID="lblSupplyStateName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <%--END--%>
            <tr>
                <td colspan="2" >
                        <asp:Label ID="Label1"  Font-Bold="true" runat="server" Text="SERVICE DESCRIPTION - FREIGHT AMOUNT CHARGED FOR TRANSPORTATION OF CEMENT (GTA)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <%--GridDetails--%>
                        <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None"
                            CssClass="display nowrap dataTable" Width="100%" GridLines="Both" EnableViewState="true" OnRowDataBound="grdMain_RowDataBound"
                            AllowPaging="false" BorderWidth="0" ShowFooter="true" PageSize="100">
                            <RowStyle CssClass="odd" />
                            <AlternatingRowStyle CssClass="even table-rows" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-CssClass="gridHeaderAlignCenter">
                                    <ItemStyle Width="50" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax Inv No." HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="100" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvNo" runat="server" Text='<%#Convert.ToString(Eval("EGP_No")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax Inv Date" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="50" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvdate" runat="server" Text='<%#Eval("EGP_Date") %>' ClientIDMode="Static"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GR/TR No" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="50" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCheckDupGrNo" runat="server" Text='<%#Convert.ToString(Eval("Gr_No"))%>' Visible=false></asp:Label>
                                        <%#Convert.ToString(Eval("GR_Prefix"))%> <%#Convert.ToString(Eval("Gr_No"))%> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GR Date" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="50" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrDate" runat="server" Text='<%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MM-yyyy") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="DI No" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="50" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <%#Convert.ToString(Eval("DI_No"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Truck No" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="50" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <%#Convert.ToString(Eval("Lorry_No"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Consignee" HeaderStyle-Width="300" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="150" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <%#Convert.ToString(Eval("Consignee"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Destination" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="100" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <%#Convert.ToString(Eval("Destination"))%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblt" runat="server" Text="Total" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weight (MT)" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="center">
                                    <ItemStyle Width="45" HorizontalAlign="center" />
                                    <ItemTemplate>
                                        <%#Convert.ToString(Eval("Weight"))%>
                                    </ItemTemplate>
                                     <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotWeight" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Rate/MT" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="50" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemRate" runat="server" Text='<%#Convert.ToString(Eval("Rate"))%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotRate" runat="server" Visible="false"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Freight" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="50" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label CssClass="freight-amnt" ID="lblAmount" runat="server" Text='<%#Convert.ToDouble(Eval("Amount")).ToString("N2")%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotAmount" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Labour" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="50" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label CssClass="labour-amnt" ID="lblLabour" runat="server" Text='<%#Convert.ToDouble(Eval("Labour")).ToString("N2")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SGST" HeaderStyle-Width="60" HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="100" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label CssClass="sgst-amnt" ID="lblSGST" runat="server" Text='0.00'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalSGST" CssClass="tot-sgst" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="CGST" HeaderStyle-Width="60" HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="100" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label CssClass="cgst-amnt" ID="lblCGST" runat="server" Text='0.00'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalCGST" CssClass="tot-cgst" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IGST" HeaderStyle-Width="60" HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="100" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label CssClass="igst-amnt" ID="lblIGST" runat="server" Text='0.00'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalIGST" CssClass="tot-igst" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Total Amount" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="50" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <span class="gr-type" style="color:transparent;position:absolute;" data-grtype='<%#Eval("STax_Typ") %>'></span>
                                        <asp:Label ID="lblTotalAmt" runat="server" CssClass="flsci-tot" Text=''></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label CssClass="TotaAmntWithTax"  ID="lblTotalAmount" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sht-BG" HeaderStyle-Width="30" HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="100" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblShortageBag" runat="server" Text='<%#Convert.ToDouble(Eval("ShortageBag")).ToString("N2")%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalShortage" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sht-Wt" HeaderStyle-Width="30" HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="100" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblShortageWeight" runat="server" Text='<%#Convert.ToDouble(Eval("ShortageWeight")).ToString("N2")%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lblShortageWeight" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                Records(s) not found.
                            </EmptyDataTemplate>
                        </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    We hereby certify that Input Tax Credit on Inputs, Input Services and Capital Goods used for providing the service of <br /> 
                    transportation has not been taken under the provisions of the CGST/SGST/IGST/UTGST Act -2017.
                    <br />
                    <asp:Label ID="lblJobnerPayableBy" runat="server" Text="GST will be payable by Shree Cement Limited"></asp:Label>
                </td>
                <td>
                <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                <br />
                <br />
                   <asp:Label ID="Label3"  runat="server" Text="Authorised Signatory"></asp:Label>  
                </td>
            </tr>
            <%--ENDDetails--%>
        </table>
        <p style="line-height: 16px;font-size: 12px;">
            <asp:Label ID="lblGeneratedByName" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblLastUpdatedByName" runat="server"></asp:Label>
        </p>
    </div>
    </form>
</body>
<script src="Scripts/jquery.js" type="text/javascript"></script>
<script>
    $(document).ready(function () {
        var grType = 0;
        var tot = 0;
        var totsgst = 0;
        var totcgst = 0;
        var totigst = 0;
        $('.dataTable tr').each(function () {
            grType = parseInt($(this).children('td').children('.gr-type').attr("data-grtype"));
            if (grType == "1") {
                var totl =
            (parseFloat($(this).children('td').children('.freight-amnt').text().replace(",", ""))
            + parseFloat($(this).children('td').children('.labour-amnt').text().replace(",", ""))
            + parseFloat($(this).children('td').children('.sgst-amnt').text().replace(",", ""))
            + parseFloat($(this).children('td').children('.cgst-amnt').text().replace(",", ""))
            + parseFloat($(this).children('td').children('.igst-amnt').text().replace(",", "")));
                $(this).children('td').children('.flsci-tot').text(ReplaceNumberWithCommas(totl.toFixed(2)));
            }
            else if (grType == "2" || grType == "3") {
                $(this).children('td').children('.flsci-tot').text
                (ReplaceNumberWithCommas((parseFloat($(this).children('td').children('.freight-amnt').text().replace(",", "")).toFixed(2))));
            }
        });
        //calculate total SGST
        $('.sgst-amnt').each(function () {
            var sgstamnt = $(this).text().replace(",", "");
            totsgst = parseFloat(totsgst) + parseFloat(sgstamnt);
        });
        totsgst = totsgst.toFixed(2);
        $('.tot-sgst').text(ReplaceNumberWithCommas(totsgst));
        //calculate total CGST
        $('.cgst-amnt').each(function () {
            var cgstamnt = $(this).text().replace(",", "");
            totcgst = parseFloat(totcgst) + parseFloat(cgstamnt);
        });
        totcgst = totcgst.toFixed(2);
        $('.tot-cgst').text(ReplaceNumberWithCommas(totcgst));
        //calculate total IGST
        $('.igst-amnt').each(function () {
            var igstamnt = $(this).text().replace(",", "");
            totigst = parseFloat(totigst) + parseFloat(igstamnt);
        });
        totigst = totigst.toFixed(2);
        $('.tot-igst').text(ReplaceNumberWithCommas(totigst));
        //calculate total Total
        $('.flsci-tot').each(function () {
            var newamnt = $(this).text().replace(",", "");
            tot = parseFloat(tot) + parseFloat(newamnt);
            tot = Math.round(parseFloat(tot));
        });
        tot = tot.toFixed(2);
        $('.TotaAmntWithTax').text(ReplaceNumberWithCommas(tot));
    });
    function ReplaceNumberWithCommas(num) {
        //Seperates the components of the number
        var n = num.toString().split(".");
        //Comma-fies the first part
        n[0] = n[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        //Combines the two sections
        return n.join(".");
    }
</script>
</html>
