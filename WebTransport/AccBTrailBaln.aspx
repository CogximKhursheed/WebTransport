<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="AccBTrailBaln.aspx.cs" Inherits="WebTransport.AccBTrailBaln" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="updpnl" runat="server">
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
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div id="view_print" runat="server">
                                                <div id="prints" runat="server" visible="true">
                                                    <div class="pull-right action-center">
                                                        <div class="fa fa-download"></div>
                                                        <div class="download-option-box">
                                                            <div class="download-option-container">
                                                                <ul>
                                                                    <li class="download-excel" data-name="Download excel">
                                                                        <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ToolTip="Export to excel" ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px" OnClick="imgBtnExcel_Click" />

                                                                        <li class="print-report" data-name="Print Report">
                                                                            <asp:LinkButton ID="lnkbtnPrint" runat="server" ToolTip="Click to print" OnClientClick="return CallPrint('divPrint');"><i class="fa fa-print icon"></i></asp:LinkButton>
                                                                        </li>
                                                                </ul>
                                                            </div>
                                                            <div class="close-download-box" title="Close download window"></div>
                                                        </div>
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
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="u-table u-table-green u-table-stripped fixed-header-table bold-last-row row-selection no-wrap-cell" GridLines="None" BorderWidth="0" ShowFooter="true" AllowPaging="false" OnRowDataBound="grdMain_RowDataBound" OnRowCommand="grdMain_RowCommand">
                                                    <PagerStyle CssClass="classPager" />
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" Position="Bottom" LastPageText="Last" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40" HeaderText="Particular">
                                                            <ItemStyle HorizontalAlign="Left" Width="40" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblParticular" runat="server"></asp:Label>
                                                                <b>
                                                                    <asp:LinkButton ID="lnkBtnParticular" runat="server" CssClass="link" CommandName="cmdshowdetail"></asp:LinkButton></b>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="left" Width="100" Font-Bold="true" />
                                                            <FooterTemplate>
                                                                Total
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="40" HeaderText="Debit">
                                                            <ItemStyle HorizontalAlign="right" Width="40" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl2Debit" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" Width="70" Font-Bold="true" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblDebit" runat="server" ForeColor="Yellow"></asp:Label>
                                                                <b>
                                                                    <asp:LinkButton ID="lnkBtn2Debit" runat="server" CssClass="link" CommandName="cmdshowdetail1D" ForeColor="Black"></asp:LinkButton></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="40" HeaderText="Credit">
                                                            <ItemStyle HorizontalAlign="right" Width="40" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl3Credit" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" Width="70" Font-Bold="true" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblCredit" runat="server" ForeColor="Yellow"></asp:Label>
                                                                <b>
                                                                    <asp:LinkButton ID="lnkBtn2Credit" runat="server" CssClass="link" CommandName="cmdshowdetail1D" ForeColor="Black"></asp:LinkButton></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="40" HeaderText="Balance">
                                                            <ItemStyle HorizontalAlign="right" Width="40" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl4Balance" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" Width="70" Font-Bold="true" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="secondFooterClass" id="divpaging" runat="server" visible="false">
                                                    <table class="" id="tblFooterscnd" runat="server">
                                                        <tr>
                                                            <th rowspan="1" colspan="1" style="width: 250px;">
                                                                <asp:Label ID="lblcontant" runat="server"></asp:Label></th>
                                                            <th rowspan="1" colspan="1" style="width: 165px;"></th>
                                                            <th rowspan="1" colspan="1" style="width: 250px; text-align: center;">
                                                                <asp:Label ID="lblDebitAmount" runat="server"></asp:Label></th>
                                                            <th rowspan="1" colspan="1" style="width: 100px; padding-left: 78px;">
                                                                <asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label>
                                                            </th>
                                                            <th rowspan="1" colspan="1" style="width: 100px; padding-left: 15px;">
                                                                <asp:Label ID="lblAmountBal" runat="server"></asp:Label>
                                                            </th>
                                                        </tr>
                                                        </tfoot>
                                                    </table>

                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
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
            <%--HIDDEN FIELD--%>

            <%--<table border="0" cellpadding="2" cellspacing="0" class="border" width="100%">
      <table cellpadding="1" cellspacing="1" border="0" class="border1" id="tblAuthorize" width="100%" runat="server">
           
                        <tr>
                        <td class="white_bg " align="center">
                         <table id="tblNoAuthorize" runat="server" visible="false" class="border1">
                            <tr>
                                <td>
                                    You are not authorize for this
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" id="Table1"
                            runat="server">
                            <tr>
                            <td>
                                <table width="850" align="center" cellpadding="1" cellspacing="1" border="0" width="100%">
                                 <tr>
                                    <td height="39" align="left" background="images/grd_top_bg.jpg" class="title06">
                                        <div style="float:left; width: 70%; height: 30px;">
                                            &nbsp;&nbsp;&nbsp;<asp:Label ID="lblHdrNm" runat="server" 
                                                Text="Ledger Report "></asp:Label>
                                            -
                                            <asp:Label ID="lblPartyName" runat="server" Font-Bold="True" ></asp:Label>
                                        </div>
                                       <div id="prints" runat="server" align="right" style="text-align: right; width: 6%;
                                            float:right; height: 30px;" visible="true">
                                            <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ImageUrl="~/Images/Excel_Img.JPG"
                                                OnClick="imgBtnExcel_Click" ToolTip="Export to excel" />
                                            <img id="printRep" runat="server" src="Images/print.jpeg" alt="Print" onclick="javascript:CallPrint('divPrint')"
                                                style="cursor: pointer;" title="Print" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </div>
                                    </td>
                                </tr>
                                    <tr>
                                        <td class="white_bg border" align="left" valign="top" colspan="4">
                                            <table width="100%">
                                                <tr>
                                                    <td class="white_bg" valign="top">
                                                    <div id="divPrint" style="overflow: auto;">
                                                       
                                                       </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                <td class="style1">
                                                    
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
      </table>--%>
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
            var prtHederName = "<table width='100%' border='0'><tr><td align='center'><strong></strong></td></tr><tr><td align='center'></td></tr><tr><td align='center'></td></tr><tr><td align='center'><strong><font size='5'>" + Compname + "</font><strong></td></tr></br><tr><td align='center'><strong>" + Compadd1 + "<strong></td></tr><tr><td align='center'><strong>" + Compadd2 + "<strong></td></tr><tr><td align='center'><strong>" + cityname + "<strong> <strong>" + state + "<strong></td></tr><tr><td align='center'><strong>" + phoneno + "<strong></td></tr></table>";
            var prtContent1 = "<table width='100%' border='0'><tr><td align='center'><strong></strong></td></tr><tr><td align='center'></td></tr><tr><td align='center'></td></tr><tr><td align='center'><strong>" + Unm + "<strong></td></tr><tr><td><div style='border-width:1px;border-color:#000;border-style:solid;'></div></td></tr> </table>";
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=600,height=600,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtHederName);
            WinPrint.document.write(prtContent1);
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
    </script>
</asp:Content>
