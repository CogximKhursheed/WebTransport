<%@ Page Title="Annexure Report" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AnnexureReport.aspx.cs" EnableEventValidation="false" Inherits="WebTransport.AnnexureReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-lg-12 center-block responsive-Sale-bill-container">
            <div class="ibox float-e-margins maximizing-form">
                <div class="ibox-title">
                    <div class="col-sm-8">
                        <h5>Annexure Report</h5>
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
                                                            <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ToolTip="Export to excel" ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px" OnClick="imgBtnExcel_Click" Visible="false" />

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
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="imgBtnExcel" />
                                    <asp:PostBackTrigger ControlID="lnkbtnPrint" />
                                </Triggers>
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
                                        <span class="filter-label">Date Range <span class="required-field">*</span> </span>
                                    </div>
                                    <div class="control-holder full-width">
                                        <asp:DropDownList ID="ddlDateRange" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6 pull-left">
                                    <div class="control-holder full-width">
                                        <span class="filter-label">Bill No/Inv. No</span>
                                    </div>
                                    <div class="control-holder full-width">
                                        <asp:TextBox ID="txtBillNo" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtBillNo_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6 pull-left">
                                    <div class="control-holder full-width">
                                        <span class="filter-label">Annexure No.</span>
                                    </div>
                                    <div class="control-holder full-width">
                                        <asp:DropDownList ID="ddlAnnexure" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlAnnexure_SelectedIndexChanged"></asp:DropDownList>
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
                            <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="true" BorderStyle="None" CssClass="u-table u-table-green u-table-stripped fixed-header-table bold-last-row row-selection no-wrap-cell" GridLines="None" BorderWidth="0" ShowFooter="true" AllowPaging="true" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand" OnRowDataBound="grdMain_RowDataBound">
                                <PagerStyle CssClass="classPager" />
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" Position="Bottom" LastPageText="Last" />
                                <Columns>
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
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
                        <style type="text/css">
                            tr.table-cell th {
                                border: 1px solid #575454;
                            }

                            tr.table-cell td {
                                border: 1px solid #575454;
                            }
                            /* tr.table-border td { border-top: 2px solid #575454; border-bottom: 2px solid #575454; }*/

                            table {
                                border-collapse: collapse;
                            }

                            * {
                                font-family: arial;
                                font-size: 12px;
                            }
                        </style>
                        <table width="100%" cellpadding="5" cellspacing="0" align="center">
                            <tbody>
                                <tr>
                                    <th colspan="16">
                                        <label style="font-size: 20px;">
                                            <asp:Label ID="lblCompanyname" runat="server" Style="font-size: 20px !important;"></asp:Label>
                                        </label>
                                        <label>
                                            <br>
                                            Head office :-
                                                    <asp:Label ID="lblCompAdd1" runat="server"></asp:Label>
                                            &nbsp
                                                    <asp:Label ID="lblCompAdd2" runat="server"></asp:Label>,
                                                    <asp:Label ID="lblCompCity" runat="server"></asp:Label>
                                            &nbsp
                                                    <asp:Label ID="lblCompCityPin" runat="server"></asp:Label>
                                            <br>
                                            STATE :-
                                                    <asp:Label ID="lblCompState" runat="server"></asp:Label>
                                            <br>
                                            Mob :-
                                                    <asp:Label ID="lblCompPhNo" runat="server"></asp:Label>
                                            <br>
                                            PAN NO :-
                                                    <asp:Label ID="lblPanNo" runat="server"></asp:Label>
                                        </label>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="8">
                                        <label style="text-align: center; display: block;">
                                            GSTIN :- 
                                                    <asp:Label ID="lblGSTNNo" runat="server"></asp:Label></label>
                                    </td>
                                    <td colspan="8">
                                        <label style="text-align: center; display: block;">
                                            SAC CODE :-
                                                    <asp:Label ID="LblSACCode" runat="server"></asp:Label></label>
                                    </td>
                                </tr>
                                <tr>
                                    <th style="border-bottom: 1px solid #000;" colspan="16" align="center">Bill Details</th>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <b>Name &amp; Address of Receiver</b>
                                        <br>
                                        <label>
                                            <b>
                                                <asp:Label ID="lblcontname" runat="server"></asp:Label></b></label>
                                        <br />
                                        <asp:Label ID="lbladd1" runat="server"></asp:Label><br />
                                        <asp:Label ID="lblcadd2" runat="server"></asp:Label><br />
                                        Distt-<asp:Label ID="lbldis" runat="server"></asp:Label>
                                        - 
                                                <asp:Label ID="diccode" runat="server"></asp:Label><br />
                                        State-<asp:Label ID="lblst" runat="server"></asp:Label><br />
                                        <b>GSTIN :-<asp:Label ID="lblgst" runat="server"></asp:Label></b>
                                    </td>
                                    <td colspan="6">
                                        <label>
                                            Annexure no.:-
                                                    <asp:Label ID="lblAnnexNo" runat="server"></asp:Label></label>
                                        <br>
                                        <label>
                                            Unit :-
                                                    <asp:Label ID="lblUnit" runat="server"></asp:Label></label>
                                        <br>
                                        <label>DEPOT</label>
                                    </td>
                                </tr>
                                <tr class="table-cell">
                                    <th style="text-align: left;">Sr.No</th>
                                    <th style="text-align: left;">L.R. No.</th>
                                    <th style="text-align: left;">L.R. Date</th>
                                    <th style="text-align: left;">Order No.</th>
                                    <th style="text-align: left;">Invoice No.</th>
                                    <th style="text-align: left;">Invoice Date</th>
                                    <th style="text-align: left;">DI. No.</th>
                                    <th style="text-align: left;">Truck No.</th>
                                    <th style="text-align: left;" colspan="2">Destination</th>
                                    <th style="text-align: right;">Weight</th>
                                    <th style="text-align: right;">Rate</th>
                                    <th style="text-align: right;">Amount</th>
                                    <th style="text-align: right;">Toll Plaza </th>
                                    <th style="text-align: right;">U/L</th>
                                    <th style="text-align: right;">Shorteg</th>
                                </tr>
                                <%int count1 = 0;  %>
                                <%foreach (var a in LstDsoReport)
                                    { %>
                                <%count1++; %>
                                <tr class="table-cell">
                                    <td align="left"><%=a.SrNo%></td>
                                    <td align="left"><%=a.LRNo%></td>
                                    <td align="left"><%=a.LRDate%></td>
                                    <td align="left"><%=a.OrderNo%></td>
                                    <td align="left"><%=a.InvoiceNo%></td>
                                    <td align="left"><%=a.InvoiceDate%></td>
                                    <td align="left"><%=a.DINo%></td>
                                    <td align="left"><%=a.TruckNo%></td>
                                    <td align="left" colspan="2"><%=a.Destination%></td>
                                    <td align="right"><%=a.Weight%></td>
                                    <td align="right"><%=a.Rate%></td>
                                    <td align="right"><%=a.Amount%></td>
                                    <td align="right"><%=a.TollPlaza%></td>
                                    <td align="right"><%=a.UL%></td>
                                    <td align="right"><%=a.Shorteg%></td>
                                </tr>
                                <%} %>
                                <tr style="border: 1px solid #000;">
                                    <th colspan="8"></th>
                                    <th style="text-align: left; border: 1px solid;" colspan="2">
                                        <asp:Label ID="LblTotal" runat="server" Text="Total :"></asp:Label></th>
                                    <th style="text-align: right; border: 1px solid;">
                                        <asp:Label ID="Lbltotalweight" runat="server"></asp:Label></th>
                                    <th style="text-align: right; border: 1px solid;">
                                        <asp:Label ID="Lbltotalrate" runat="server"></asp:Label></th>
                                    <th style="text-align: right; border: 1px solid;">
                                        <asp:Label ID="Lbltotalamt" runat="server"></asp:Label></th>
                                    <th style="text-align: right; border: 1px solid;">
                                        <asp:Label ID="Lbltollplaza" runat="server"></asp:Label></th>
                                    <th style="text-align: right; border: 1px solid;">
                                        <asp:Label ID="LblUL" runat="server"></asp:Label></th>
                                    <th style="text-align: right; border: 1px solid;">
                                        <asp:Label ID="Lblshorteg" runat="server"></asp:Label></th>
                                </tr>
                                <tr>
                                    <td colspan="16">
                                        <label>
                                            <asp:Label ID="lblword" runat="server"></asp:Label></label></td>
                                </tr>
                                <tr>
                                    <td colspan="8">
                                        <label><b>Note:- Certified that the Particulars given above are true and correct.</b></label></td>
                                    <td colspan="8" align="right">
                                        <asp:Label ID="lblCompname" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="16"></td>
                                </tr>
                                <tr>
                                    <td colspan="16"></td>
                                </tr>
                                <tr>
                                    <td colspan="16"></td>
                                </tr>
                                <tr>
                                    <td colspan="16" align="right">
                                        <label>(Authorised Signatory)</label></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </td>
            </tr>
        </div>
    </div>
    <%--HIDDEN FIELDS--%>
    <asp:HiddenField ID="hidrcptheadidno" runat="server" />
    <asp:HiddenField ID="hidmindate" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
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

            <%--$("#<%=txtDateFrom.ClientID %>").datepicker({
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
            });--%>
        }
    </script>
    <script>
        $(".datepicker").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
            minDate: '<%=hidmindate.Value%>',
            maxDate: '<%=hidmaxdate.Value%>'
        });
    </script>
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
            //WinPrint.close();
            return false;
        }
    </script>
</asp:Content>
