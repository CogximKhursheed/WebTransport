<%@ Page Title="Consolidated GR Report" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="ConsolidetedGrReport.aspx.cs" EnableEventValidation="false"
    Inherits="WebTransport.ConsolidetedGrReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .RptRight {
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12 center-block responsive-Sale-bill-container">
            <div class="ibox float-e-margins maximizing-form">
                <div class="ibox-title">
                    <div class="col-sm-8">
                        <h5>Consolidated GR Report</h5>
                    </div>
                    <div class="col-sm-4">
                        <div class="title-action">
                            <div id="view_print" runat="server">
                                <div class="pull-right action-center">
                                    <div class="fa fa-download"></div>
                                    <div class="download-option-box">
                                        <div class="download-option-container">
                                            <ul>
                                                <li class="print-report" data-name="Print Report">
                                                    <asp:LinkButton ID="lnkbtnPrint" runat="server" ToolTip="Click to print" Visible="false" OnClientClick="return CallPrint('print');"><i class="fa fa-print icon"></i></asp:LinkButton>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="close-download-box" title="Close download window"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12 fliter-section">
                    <div class="filter-data">
                        <div class="selected-filetrs" onclick="$('.filter').slideToggle();$('.fa-filter').toggleClass('active')">
                        </div>
                        <div class="pull-right">
                            <%--<i class="fa fa-sort filter-icon" onclick="$('.filter').slideUp();$('.sort').slideToggle();$('.fa-filter').removeClass('active');$(this).toggleClass('active');"></i>--%>
                            <i class="fa fa-filter filter-icon" onclick="$('.sort').slideUp();$('.filter').slideToggle();$('.fa-sort').removeClass('active');$(this).toggleClass('active');"></i>
                        </div>
                    </div>
                    <div class="filter" style="width: 400px; display: none;">
                        <div class="panel-body">
                            <!-- First Row start -->
                            <div class="col-sm-12">
                                <div class="col-sm-12 pull-left">
                                    <div class="control-holder full-width">
                                        <span class="filter-label">Date Range </span>
                                    </div>
                                    <div class="control-holder full-width">
                                        <asp:DropDownList ID="ddlDateRange" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6 pull-left">
                                    <div class="control-holder full-width">
                                        <span class="filter-label">Loc.[From]</span>
                                    </div>
                                    <div class="control-holder full-width">
                                        <asp:DropDownList ID="drpBaseCity" runat="server" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpBaseCity"
                                            SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage="<br /> Please Select Location!" InitialValue="0"
                                            Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-6 pull-left">
                                    <div class="control-holder full-width">
                                        <span class="filter-label">GR No</span>
                                    </div>
                                    <div class="control-holder full-width">
                                        <asp:TextBox ID="txtGrNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="errt" runat="server" ControlToValidate="txtGrNo"
                                            SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage="<br /> Please GR No!"
                                            Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 pull-right">
                            <asp:LinkButton ID="lnkbtnPreview" CssClass="btn btn-w-m btn-primary center-block" runat="server" OnClick="lnkbtnPreview_OnClick"><i class="fa fa-search-plus"></i>Preview</asp:LinkButton>
                        </div>
                    </div>
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
                            <div class="col-lg-0" id="tblPrint" runat="server" visible="false">
                                <tr>
                                    <td class="white_bg" align="center">
                                        <div id="print" style="font-size: 13px;">
                                            <table cellpadding="1" cellspacing="0" width="800" border="1" style="font-family: Arial,Helvetica,sans-serif;">
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
                                    <asp:Label ID="lblFaxNo" Text="FAX No.:" runat="server"></asp:Label>
                                                        <asp:Label ID="lblCompFaxNo" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblTin" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;<asp:Label
                                                            ID="lblCompTIN" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lbltxtPanNo" runat="server" Text="PAN NO. :"></asp:Label>&nbsp;&nbsp;<asp:Label
                                        ID="lblPanNo" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px; border-left-style: none; border-right-style: none">
                                                        <h3>
                                                            <strong style="text-decoration: underline">
                                                                <asp:Label ID="lblPrintHeadng" runat="server" Text="Consolidated GR Report"></asp:Label>
                                                            </strong>

                                                        </h3>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <table border="0" width="100%">
                                                            <tr>
                                                                <td align="left" class="white_bg" style="width: 12%; font-size: 13px; border-right-style: none" valign="top">
                                                                    <asp:Label ID="lbltxtgrno" Text="GR. No.  :" runat="server"></asp:Label>
                                                                </td>

                                                                <td align="left" class="white_bg" valign="top" style="width: 22%; font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblGRno" runat="server"></asp:Label></b>
                                                                </td>
                                                                <td align="left" class="white_bg" style="width: 14%; font-size: 13px; border-right-style: none" valign="top">
                                                                    <asp:Label ID="lbltxtgrdate" Text="GR. Date  :" runat="server"></asp:Label>
                                                                </td>

                                                                <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblGrDate" runat="server"></asp:Label></b>
                                                                </td>
                                                                <td align="left" class="white_bg" style="width: 12%; font-size: 13px; border-right-style: none" valign="top">
                                                                    <asp:Label ID="lbltxtFromcity" Text="From City  :" runat="server"></asp:Label>
                                                                </td>

                                                                <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblFromCity" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <asp:Label ID="lbltxttocity" Text="To City  :" runat="server"></asp:Label>
                                                                </td>

                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblToCity" runat="server"></asp:Label></b>
                                                                </td>
                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <asp:Label ID="lblNameSealNo" Text="User  :" runat="server"></asp:Label>
                                                                </td>

                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblUser" runat="server"></asp:Label></b>
                                                                </td>
                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <asp:Label ID="lbltxtGrType" runat="server" Text="GR type      :"></asp:Label>
                                                                </td>

                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblGrType" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <asp:Label ID="lbltxtsendername" Text="Sender Name  :" runat="server"></asp:Label>
                                                                </td>

                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblSenderName" runat="server"></asp:Label></b>
                                                                </td>
                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <asp:Label ID="lbltxtreceivername" Text="Receiver Name  :" runat="server"></asp:Label>
                                                                </td>

                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblRecvrName" runat="server"></asp:Label></b>
                                                                </td>
                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <asp:Label ID="lbltxtLoryType" runat="server" Text="LorryType  :"></asp:Label>
                                                                </td>

                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblLorryType" Text="" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <asp:Label ID="lblNameShipmentno" Text="Lorry No.  :" runat="server"></asp:Label>
                                                                </td>

                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblLorryNo" runat="server"></asp:Label></b>
                                                                </td>
                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <asp:Label ID="lblNameContnrNo" Text="Serv. Tax  :" runat="server"></asp:Label>
                                                                </td>

                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblServTax" runat="server"></asp:Label></b>
                                                                </td>
                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <asp:Label ID="lblNameCntnrSize" Text="Freight  :" runat="server"></asp:Label>
                                                                </td>

                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblFrieght" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6">&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td colspan="4">
                                                        <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table1">
                                                            <asp:Repeater ID="Repeater1" runat="server">
                                                                <HeaderTemplate>
                                                                    <tr>
                                                                        <td class="white_bg" style="font-size: 12px" width="10%">
                                                                            <strong>S.No.</strong>
                                                                        </td>
                                                                        <td style="font-size: 12px" width="20%">
                                                                            <strong>Item Name</strong>
                                                                        </td>
                                                                        <td style="font-size: 12px" width="10%">
                                                                            <strong>Qty</strong>
                                                                        </td>
                                                                        <td style="font-size: 12px" class="RptRight" width="10%">
                                                                            <strong>Weight</strong>
                                                                        </td>
                                                                        <td style="font-size: 12px" class="RptRight" width="10%">
                                                                            <strong>Item Rate</strong>
                                                                        </td>
                                                                        <td style="font-size: 12px" class="RptRight" width="10%">
                                                                            <strong>Amount</strong>&nbsp;&nbsp;
                                                                        </td>

                                                                    </tr>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td class="white_bg" width="10%">
                                                                            <%#Container.ItemIndex+1 %>.
                                                                        </td>
                                                                        <td class="white_bg" width="30%">
                                                                            <%#Eval("Item_Name")%>
                                                                        </td>
                                                                        <td class="white_bg" width="15%">
                                                                            <%#Eval("Qty")%>
                                                                        </td>
                                                                        <td class="white_bg RptRight" width="15%">
                                                                            <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Tot_Weght")))%>
                                                                        </td>
                                                                        <td class="white_bg RptRight" width="15%" style="text-align: right;">
                                                                            <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Item_Rate")))%>
                                                                        </td>
                                                                        <td class="white_bg RptRight" width="15%" style="text-align: right;">
                                                                            <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%>&nbsp;&nbsp;
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
                                                    <td colspan="4">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" id="tdChlnDetailsTable" runat="server">
                                                        <table border="0" width="100%">
                                                            <tr>
                                                                <td align="left" class="white_bg" style="width: 12%; font-size: 13px; border-right-style: none" valign="top">
                                                                    <asp:Label ID="Label1" Text="Chln. No.  :" runat="server"></asp:Label>
                                                                </td>

                                                                <td align="left" class="white_bg" valign="top" style="width: 22%; font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblChlnNo" runat="server"></asp:Label></b>
                                                                </td>
                                                                <td align="left" class="white_bg" style="width: 14%; font-size: 13px; border-right-style: none" valign="top">
                                                                    <asp:Label ID="Label3" Text="Chln. Date  :" runat="server"></asp:Label>
                                                                </td>

                                                                <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblChlnDate" runat="server"></asp:Label></b>
                                                                </td>
                                                                <td align="left" class="white_bg" style="width: 12%; font-size: 13px; border-right-style: none" valign="top">
                                                                    <asp:Label ID="Label5" Text="From City  :" runat="server"></asp:Label>
                                                                </td>

                                                                <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblChlnFromCity" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <asp:Label ID="Label7" Text="Lorry No  :" runat="server"></asp:Label>
                                                                </td>

                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblChlnLorryNo" runat="server"></asp:Label></b>
                                                                </td>
                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <asp:Label ID="Label9" Text="Lorry Type  :" runat="server"></asp:Label>
                                                                </td>

                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblChlnLorryType" runat="server"></asp:Label></b>
                                                                </td>
                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">&nbsp;
                                                                </td>

                                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                                    <b>&nbsp;
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td colspan="6">&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td colspan="2" align="left" width="50%">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblremark" runat="server" valign="right"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td colspan="3" width="30%"></td>
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
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent1 = "<table width='100%' border='0'></table>";
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'left=0,top=0,width=800,height=800,toolbar=1,scrollbars=1,status=1');
            WinPrint.document.write(prtContent1);
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
</asp:Content>
