<%@ Page Title="Party Detail Report" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="PartyDetlRep.aspx.cs" EnableEventValidation="false"
    Inherits="WebTransport.PartyDetlRep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function () {
            setDatecontrol();
        });

        prm.add_endRequest(function () {
            setDatecontrol();
        });

        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();

            $("#<%=txtDateFrom.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
            $("#<%=txtDateTo.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
        }
    </script>

    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 center-block responsive-Sale-bill-container">
                    <div class="ibox float-e-margins maximizing-form">
                        <div class="ibox-title">
                            <div class="col-sm-8">
                                <h5>Party Detail Report</h5>
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
                                                                    <asp:ImageButton ID="imgCSV" runat="server" AlternateText="Excel" ToolTip="Export to excel" ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px" OnClick="imgCSV_Click" Visible="false" />
                                                                    <li class="print-report" data-name="Print Report">
                                                                        <asp:LinkButton ID="lnkbtnPrint" runat="server" ToolTip="Click to print" Visible="false" OnClientClick="return CallPrint('print');"><i class="fa fa-print icon"></i></asp:LinkButton>
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
                                                <asp:DropDownList ID="ddlDateRange" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Date From </span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="input-sm datepicker form-control" MaxLength="12" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDateFrom" runat="server" ControlToValidate="txtDateFrom" SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage=" Please Enter Date!" Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div id="ctl00_ContentPlaceHolder1_UpdatePanel3">
                                                <div class="control-holder full-width">
                                                    <span class="filter-label">Date To </span>
                                                </div>
                                                <div class="control-holder full-width">
                                                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="input-sm datepicker form-control" MaxLength="12" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvDateTo" runat="server" ControlToValidate="txtDateTo" SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage=" Please Enter Date!" Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Party Name</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:DropDownList ID="ddlParty" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Location[From]</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:DropDownList ID="drpBaseCity" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Rep Type</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:DropDownList ID="ddlGrAccordng" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="2" Text="All"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Only Confirmed Gr"></asp:ListItem>
                                                </asp:DropDownList>
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
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="u-table u-table-green u-table-stripped fixed-header-table bold-last-row row-selection no-wrap-cell" GridLines="None" BorderWidth="0" ShowFooter="true" AllowPaging="true" PageSize="100" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowDataBound="grdMain_RowDataBound">
                                                    <PagerStyle CssClass="classPager" />
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" Position="Bottom" LastPageText="Last" />
                                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <asp:Label ID="lblTotalRecord" runat="server" Text="T. Record(s) : 0" CssClass="control-label record-count"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-1" style="display: none">
                    <tr style="display: none">
                        <td class="white_bg" align="center">
                            <div id="print" style="font-size: 13px; display: none;">
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
                                        <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 13px; border-left-style: none; border-right-style: none">
                                            <h3>
                                                <div style="text-decoration: underline">
                                                    <asp:Label ID="lblPrintHeadng" runat="server" Text="Party Detail Receipt"></asp:Label>
                                                    (Date From :
                                                    <asp:Label ID="lblDateFrom" runat="server"></asp:Label>
                                                    &nbsp;&nbsp; Date To :
                                                    <asp:Label ID="lblDateto" runat="server"></asp:Label>)
                                                </div>
                                                <h3></h3>
                                                <h3></h3>
                                                <h3></h3>
                                                <h3></h3>
                                                <h3></h3>
                                                <h3></h3>
                                                <h3></h3>
                                                <h3></h3>
                                                <h3></h3>
                                                <h3></h3>
                                            </h3>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <table border="0" width="100%">
                                                <tr>
                                                    <td align="left" width="7%" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                        <b>
                                                            <asp:Label ID="lbltxtgrno" Text="Party Name" runat="server"></asp:Label></b>
                                                    </td>
                                                    <td>:
                                                    </td>
                                                    <td align="left" class="style1" valign="top" style="font-size: 13px; border-right-style: none">
                                                        <asp:Label ID="lblPartyName" runat="server"></asp:Label>
                                                    </td>
                                                    <td align="left" class="style2" valign="top" colspan="2" style="font-size: 13px; border-right-style: none">
                                                        <div id="divLocation" runat="server">
                                                            <b>
                                                                <asp:Label ID="lbltxtgrdate" Text="Location" runat="server"></asp:Label></b>
                                                            :&nbsp;
                                                            <asp:Label ID="lblLocation" runat="server"></asp:Label>
                                                        </div>
                                                    </td>
                                                    <td colspan="4" align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none"></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                        <b>
                                                            <asp:Label ID="lbltxttocity" Text="Address" runat="server"></asp:Label>
                                                        </b>
                                                    </td>
                                                    <td>:
                                                    </td>
                                                    <td align="left" class="white_bg" valign="top" colspan="7" style="font-size: 13px; border-right-style: none">
                                                        <asp:Label ID="lblAddresss" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                        <b>
                                                            <asp:Label ID="lbltxtsendewrname0" Text="City" runat="server"></asp:Label>
                                                        </b>
                                                    </td>
                                                    <td>:
                                                    </td>
                                                    <td align="left" class="style1" valign="top" style="font-size: 13px; border-right-style: none">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblCityname" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <b>&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                                                        <asp:Label ID="lbltxtsendername" Text="State : " runat="server"></asp:Label></b>
                                                                    &nbsp;&nbsp;
                                                                    <asp:Label ID="lblStateName" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="left" class="style2" valign="top" colspan="2" style="font-size: 13px; border-right-style: none">&nbsp;
                                                    </td>
                                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                        <b></b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table1">
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="grdPrintDtl" runat="server" AutoGenerateColumns="false" BorderStyle="None"
                                                            GridLines="Vertical" ShowFooter="false" HeaderStyle-CssClass="gridRow" BorderWidth="0"
                                                            RowStyle-CssClass="gridAlternateRow" AlternatingRowStyle-CssClass="gridRow" CssClass="ibdr gridBackground internal_heading"
                                                            Width="2000px">
                                                            <HeaderStyle CssClass="linearBg" ForeColor="Black" Font-Bold="false" Font-Size="12px" />
                                                            <FooterStyle ForeColor="Black" Font-Bold="true" />
                                                            <RowStyle CssClass="bgcolrwhite" Font-Size="12px" />
                                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                                            <EmptyDataTemplate>
                                                                <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                                            </EmptyDataTemplate>
                                                            <PagerStyle CssClass="white_bg" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="linearBg" ForeColor="Black" />
                                                            <AlternatingRowStyle CssClass="bgcolor2" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table2">
                                                <tr>
                                                    <td class="white_bg" width="15%"></td>
                                                    <td class="white_bg" width="15%"></td>
                                                    <td class="white_bg" width="15%" align="center"></td>
                                                    <td class="white_bg" width="15%" align="left">
                                                        <asp:Label ID="lbltotalqty" Font-Bold="true" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="white_bg" width="12.5%">
                                                        <asp:Label ID="lbltotalWeight" Font-Bold="true" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="white_bg" width="12.5%"></td>
                                                    <td class="white_bg" width="12.5%" align="center">
                                                        <asp:Label ID="lblTotalAmnt" Font-Bold="true" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="white_bg" width="12.5%"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%">&nbsp;
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
            <asp:HiddenField ID="hidrcptheadidno" runat="server" />
            <asp:HiddenField ID="hidmindate" runat="server" />
            <asp:HiddenField ID="hidmaxdate" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgCSV" />
        </Triggers>
    </asp:UpdatePanel>



















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
    <script>
        $(".datepicker").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
            minDate: '<%=hidmindate.Value%>',
            maxDate: '<%=hidmaxdate.Value%>'
        });
    </script>
</asp:Content>
