<%@ Page Title="Ledger Report" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="AccBLedger.aspx.cs" Inherits="WebTransport.AccBLedger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .RightAlign {
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 center-block responsive-Sale-bill-container">
                    <div class="ibox float-e-margins maximizing-form">
                        <div class="ibox-title">
                            <div class="col-sm-8">
                                <h5>Ledger Report
                                    <asp:Label ID="lblPartyName" runat="server" Font-Bold="True"></asp:Label>
                                </h5>
                            </div>
                            <div class="col-sm-4">
                                <div class="title-action">
                                    <asp:UpdatePanel ID="updpnl" runat="server">
                                        <ContentTemplate>
                                            <div id="view_print" runat="server">
                                                <div class="pull-right action-center">
                                                    <div class="fa fa-download"></div>
                                                    <div class="download-option-box">
                                                        <div class="download-option-container">
                                                            <ul>
                                                                <li class="download-excel" data-name="Download excel">
                                                                    <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ToolTip="Export to excel" ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px" OnClick="imgBtnExcel_Click" Visible="true" />

                                                                    <li class="print-report" data-name="Print Report">
                                                                        <asp:LinkButton ID="lnkbtnPrint" runat="server" ToolTip="Click to print" Visible="true" OnClientClick="return CallPrint('divPrint');"><i class="fa fa-print icon"></i></asp:LinkButton>
                                                                    </li>
                                                            </ul>
                                                        </div>
                                                        <div class="close-download-box" title="Close download window"></div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 fliter-section">
                            <div class="sort" style="width: 200px; display: none;">
                                <div class="panel-body">
                                    <!-- First Row start -->
                                    <div class="col-sm-12">
                                        <ul class="sort-list">
                                            <li>
                                                <label>
                                                    <span class="sort-name">Date</span>
                                                    <asp:RadioButton ID="rbdDate" GroupName="OrderBy" runat="server" />
                                                    <span><i class="asc fa fa-sort-amount-asc"></i><i class="desc fa fa-sort-amount-desc"></i></span>
                                                </label>
                                            </li>
                                            <li>
                                                <label>
                                                    <span class="sort-name">Payment Range</span>
                                                    <asp:RadioButton ID="rbdOil" GroupName="OrderBy" runat="server" />
                                                    <span><i class="asc fa fa-sort-alpha-asc"></i><i class="desc fa fa-sort-alpha-desc"></i></span>
                                                </label>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-sm-12 pull-right">
                                    <asp:LinkButton ID="lnkSort" class="btn btn-w-m btn-primary center-block" runat="server"> <i class="fa fa-search"></i> Sort</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="ibox-content auto-form-height scroll-pane">
                            <div class="table-responsive nowrap dataTable slim-scrollbar fixed-header-table-scroll" style="max-height: calc(100vh - 230px);">
                                <div id="divPrint">
                                    <%-- <table  width="100%">
                                       <tr>
                                       <td>--%>
                                    <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="u-table u-table-green u-table-stripped fixed-header-table bold-last-row row-selection no-wrap-cell" GridLines="None" BorderWidth="0" ShowFooter="true" AllowPaging="false" PageSize="50" OnRowDataBound="grdMain_RowDataBound" OnRowCommand="grdMain_RowCommand">
                                        <PagerStyle CssClass="classPager" />
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" Position="Bottom" LastPageText="Last" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20" HeaderText="Vchr Date">
                                                <ItemStyle HorizontalAlign="Left" Width="20" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("VCHR_DATE")))?"":(Convert.ToDateTime((Eval("VCHR_DATE"))).ToString("dd-MMM-yyyy")))%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="left" Width="20" Font-Bold="true" />
                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="5" HeaderText="Vchr No"
                                                Visible="false">
                                                <ItemStyle HorizontalAlign="center" Width="5" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblType" runat="server" Text='<%#Eval("VCHR_NO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="left" Width="5" Font-Bold="true" />
                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Particular">
                                                <ItemStyle HorizontalAlign="Left" Width="150" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPart" runat="server" Text='<%#Eval("PARTICULAR") %>'></asp:Label>
                                                    <asp:HiddenField ID="hidDoc" Value='<%#Eval("DOC_IDNO")%>' runat="server" />
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="left" Width="150" Font-Bold="true" />
                                                <FooterTemplate>
                                                    Closing Balance
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10" HeaderText="Vchr Type">
                                                <ItemStyle HorizontalAlign="Left" Width="10" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVchrType" runat="server"></asp:Label>
                                                    <b>
                                                        <asp:LinkButton ID="lnkBtnVchrType" runat="server" CssClass="link" CommandName="showdetail"></asp:LinkButton></b>
                                                    <asp:HiddenField ID="hidVchrIdno" Value='<%#Eval("VCHR_IDNO")%>' runat="server" />
                                                    <asp:HiddenField ID="hidVchrFrm" Value='<%#Eval("Vchr_Frm")%>' runat="server" />
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Left" Width="20" Font-Bold="true" />
                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="200" HeaderText="Narration">
                                                <ItemStyle HorizontalAlign="Left" Width="200" Wrap="true" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNarr" runat="server" Text='<%#Eval("NARR_TEXT") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="left" Width="200" Font-Bold="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="RightAlign" HeaderStyle-Width="40" HeaderText="Debit">
                                                <ItemStyle HorizontalAlign="right" Width="40" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDebit" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("Debit"))) ? "0.00" : (Convert.ToDouble(Eval("Debit"))==0.00 ? "0.00":(string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Debit"))))) )%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle CssClass="RightAlign" Width="40" Font-Bold="true" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTDebit" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="RightAlign" HeaderStyle-Width="40" HeaderText="Credit">
                                                <ItemStyle HorizontalAlign="right" Width="40" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCredit" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("Credit"))) ? "0.00" : (Convert.ToDouble(Eval("Credit"))==0.00? "0.00":(string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Credit"))))) )%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle CssClass="RightAlign" Width="40" Font-Bold="true" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTCredit" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="RightAlign" HeaderStyle-Width="40" HeaderText="Balance">
                                                <ItemStyle HorizontalAlign="right" Width="40" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBal" runat="server" Text='<%#Eval("Balance") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle CssClass="RightAlign" Width="40" Font-Bold="true" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblBalance" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <%-- </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            <div class="secondFooterClass"  id="divpaging" runat="server" visible="false">                                                                           
                                            <table class="" id="tblFooterscnd" runat="server" >
		                                        <tr><th rowspan="1" colspan="1" style="width:250px;"> <asp:Label ID="lblcontant" runat="server"></asp:Label></th><th rowspan="1" colspan="1" style="width: 70px;"></th><th rowspan="1" colspan="1" style="width: 350px;text-align:right;"><asp:Label ID="lblDebitAmount" runat="server"></asp:Label></th><th rowspan="1" colspan="1" style="width: 100px; padding-left: 40px;"><asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label>
                                                </th><th rowspan="1" colspan="1" style="width: 100px; padding-left: 15px;"><asp:Label ID="lblAmountBal" runat="server"></asp:Label>
                                                </th></tr>                                  
		                                    </tfoot>
                                            </table>

                                            </div>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td>
                                                <br /> &nbsp;
                                            </td>
                                            </tr>
                                            </table>
                                            <br /> --%>
                                </div>
                                <div style="float: left;">
                                    <%#(string.IsNullOrEmpty(Convert.ToString(Eval("VCHR_DATE")))?"":(Convert.ToDateTime((Eval("VCHR_DATE"))).ToString("dd-MMM-yyyy")))%>
                                </div>
                                <div style="float: left; display: none;">
                                    <table cellpadding="1" cellspacing="0" width="900" border="1">
                                        <tr>

                                            <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px; border-left-style: none; border-right-style: none">
                                                <strong>
                                                    <asp:Label ID="lblCompanyname" runat="server" Style="font-size: 18px;"></asp:Label><br />
                                                </strong>
                                                <asp:Label ID="lblCompAdd1" runat="server"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="lblCompAdd2" runat="server"></asp:Label><br />
                                                <asp:Label ID="lblCompCity" runat="server"></asp:Label>&nbsp;&nbsp;
                                                        <asp:Label ID="lblCompState" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="lblCompCityPin" runat="server"></asp:Label><br />
                                                <asp:Label ID="lblCompPhNo" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                      
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgBtnExcel" />
            <asp:PostBackTrigger ControlID="lnkbtnPrint" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function CallPrint(strid) {

            var Unm = $('#<%= lblPartyName.ClientID %>').text();
            var Compname = $('#<%= lblCompanyname.ClientID %>').text();
            var Compadd1 = $('#<%= lblCompAdd1.ClientID %>').text();
            var Compadd2 = $('#<%= lblCompAdd2.ClientID %>').text();
            var cityname = $('#<%= lblCompCity.ClientID %>').text();
            var state = $('#<%= lblCompState.ClientID %>').text();
            var phoneno = $('#<%= lblCompPhNo.ClientID %>').text();
            var prtHederName = "<html><head><style> .RightAlign { text-align:right;} </style></head>";
            prtHederName = prtHederName + "<table width='100%' style='font-size:11px;' border='0'><tr><td align='center'><strong></strong></td></tr><tr><td align='center'></td></tr><tr><td align='center'></td></tr><tr><td align='center'><strong><font style='font-size:14px'>" + Compname + "</font><strong></td></tr></br><tr><td align='center'><strong>" + Compadd1 + "<strong></td></tr><tr><td align='center'><strong>" + Compadd2 + "<strong></td></tr><tr><td align='center'><strong>" + cityname + "<strong> <strong>" + state + "<strong></td></tr><tr><td align='center'><strong>" + phoneno + "<strong></td></tr></table>";
            var prtContent1 = "<table width='100%' style='font-size:11px;' border='0'><tr><td align='center'><strong></strong></td></tr><tr><td align='center'></td></tr><tr><td align='center'></td></tr><tr><td align='center'><strong>" + Unm + "<strong></td></tr><tr><td><div style='border-width:1px;border-color:#000;border-style:solid;'></div></td></tr> </table>";
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=600,height=600,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtHederName);
            WinPrint.document.write(prtContent1);
            var divstr = "" + (prtContent.innerHTML).toString();
            divstr = divstr.replace("border-width:0px;border-style:None;width:100%;border-collapse:collapse;", "border-width:0px;border-style:None;font-size:11px;width:100%;border-collapse:collapse;");
            WinPrint.document.write(divstr);
            WinPrint.document.write('</body></html>');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
    </script>
    <script type="text/javascript" language="javascript">
        function ShowPopup(Vchr_Idno, FTyp) {
            var answer = window.showModalDialog("VchrEntry.aspx?VchrIdno=" + Vchr_Idno + "&VchrFrm=" + FTyp,
                "unadorned:yes;resizable:1;dialogHeight:768px;dialogwidth:1366px;scroll:no;status=no");
        }
        function ShowPopup1(HId, FTyp, ACB) {
            var answer = window.showModalDialog("VATInvoice.aspx?FTyp=1&HId=" + HId + "&VchrFrm=" + FTyp + "&Acb=" + ACB,
                "unadorned:yes;resizable:1;dialogHeight:768px;dialogwidth:1366px;scroll:no;status=no");
        }
        function ShowPopup2(HId, FTyp, ACB) {
            var answer = window.showModalDialog("VATInvoice.aspx?FTyp=3&HId=" + HId + "&VchrFrm=" + FTyp + "&Acb=" + ACB,
                "unadorned:yes;resizable:1;dialogHeight:768px;dialogwidth:1366px;scroll:no;status=no");
        }
        function ShowPopup3(HId, FTyp, ACB) {
            var answer = window.showModalDialog("VATInvoice.aspx?FTyp=2&HId=" + HId + "&VchrFrm=" + FTyp + "&Acb=" + ACB,
                "unadorned:yes;resizable:1;dialogHeight:768px;dialogwidth:1366px;scroll:no;status=no");
        }
        function ShowPopup4(HId, FTyp) {
            var answer = window.showModalDialog("VATInvoiceByFactory.aspx?FTyp=2&HId=" + HId + "&VchrFrm=" + FTyp,
                "unadorned:yes;resizable:1;dialogHeight:768px;dialogwidth:1366px;scroll:no;status=no");
        }
        function ShowPopup5(HId, FTyp) {
            var answer = window.showModalDialog("PurBill.aspx?FTyp=1&HId=" + HId + "&VchrFrm=" + FTyp,
                "unadorned:yes;resizable:1;dialogHeight:768px;dialogwidth:1366px;scroll:no;status=no");
        }
        function ShowPopup6(HId, FTyp) {
            var answer = window.showModalDialog("PayRcptAdjstFrmDlr.aspx?payrecid=" + HId + "&VchrFrm=" + FTyp,
                "unadorned:yes;resizable:1;dialogHeight:768px;dialogwidth:1366px;scroll:no;status=no");
        }
        function ShowPopup7(HId, FTyp) {
            var answer = window.showModalDialog("PayRcptFrmDlr.aspx?payrecid=" + HId + "&VchrFrm=" + FTyp,
                "unadorned:yes;resizable:1;dialogHeight:768px;dialogwidth:1366px;scroll:no;status=no");
        }
    </script>
</asp:Content>
