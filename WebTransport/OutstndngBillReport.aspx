<%@ Page Title="OUTSTANDING BILL REPORT" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="OutstndngBillReport.aspx.cs" Inherits="WebTransport.OutstndngBillReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        SetFocus();
        function SetFocus() {
            $('input[type="text"]').focus(function () {
                $(this).addClass("focus");
            });
            $('input[type="text"]').blur(function () {
                $(this).removeClass("focus");
            });
            $("select").focus(function () {
                $(this).addClass("focus");
            });
            $("select").blur(function () {
                $(this).removeClass("focus");
            });
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_beginRequest(function () {
            SetFocus();
            setDatecontrol();
        });

        prm.add_endRequest(function () {
            SetFocus();
            setDatecontrol();
        });

        $(document).ready(function () {
            setDatecontrol();
        });

        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $('#<%=txtDateFrom.ClientID %>').datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });

            $('#<%=txtDateTo.ClientID %>').datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
        }
        function ShowMessage(value) {
            alert(value);
        }
        function ShowInvoiceDetails(divname) {
            document.getElementById(divname).style.display = 'block';
        }
        function HideInvoiceDetails(divname) {
            document.getElementById(divname).style.display = 'none';
        }
    </script>

    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 center-block responsive-Sale-bill-container">
                    <div class="ibox float-e-margins maximizing-form">
                        <div class="ibox-title">
                            <div class="col-sm-8">
                                <h5>OUTSTANDING BILL REPORT</h5>
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
                                                <span class="filter-label">Loc.[From]</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:DropDownList ID="drpBaseCity" runat="server" CssClass="form-control" AutoPostBack="false"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Party Name</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:DropDownList ID="drpSenderName" runat="server" CssClass="form-control" AutoPostBack="false"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Invoice No.</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:TextBox ID="txtInvoiceNo" runat="server" PlaceHolder="Invoice Number" CssClass="form-control" MaxLength="12"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Prefix No.</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:TextBox ID="txtPrefixNo" runat="server" PlaceHolder="Prefix Number" CssClass="form-control" MaxLength="12"></asp:TextBox>
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
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="u-table u-table-green u-table-stripped fixed-header-table bold-last-row row-selection no-wrap-cell" GridLines="None" BorderWidth="0" ShowFooter="true" AllowPaging="true" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowDataBound="grdMain_RowDataBound">
                                                    <PagerStyle CssClass="classPager" />
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" Position="Bottom" LastPageText="Last" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="center"
                                                            Visible="true">
                                                            <ItemStyle HorizontalAlign="center" Width="80" />
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>.
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="center" Width="80" ForeColor="White" Font-Bold="true" />
                                                            <FooterTemplate>
                                                                <b>GRID TOTAL</b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice No." HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            <ItemTemplate>
                                                                <%#Eval("Inv_No")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice Date" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" Width="100" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Inv_Date"))) ? "" : (Convert.ToDateTime((Eval("Inv_Date"))).ToString("dd-MMM-yyyy")))%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="From City" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                                            <ItemTemplate>
                                                                <%#Eval("From_City")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Party Name" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                                            <ItemTemplate>
                                                                <%#Eval("Sendr_Name")%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>Total:</b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice Amnt" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="100" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Amount"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Amount")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblNetAmnt" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Received" HeaderStyle-Width="108" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="108" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Tot_Recvd"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Tot_Recvd")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblRecivdAmnt" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TDS Amnt" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="100" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("TDS_Amount"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("TDS_Amount")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblTDSAmnt" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Debit Note" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="100" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("DB_Note"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("DB_Note")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblDBNoteAmnt" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Pending Amnt" HeaderStyle-Width="110" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="110" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("cur_Bal"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("cur_Bal")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblPendingAmnt" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>
                                                    <div class="secondFooterClass" id="divpaging" runat="server" visible="false">
                                                        <div class="col-sm-3" style="text-align: left">
                                                            <asp:Label ID="lblcontant" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-2" style="text-align: right; width: 11%">TOTAL</div>
                                                        <div class="col-sm-1" style="text-align: right; width: 24%">
                                                            <asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-1" style="text-align: right; width: 10%">
                                                            <asp:Label ID="lblNetTotalRecivdAmnt" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-1" style="text-align: right; width: 10%">
                                                            <asp:Label ID="lblNetTotalTDSAmnt" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-1" style="text-align: right; width: 9%">
                                                            <asp:Label ID="lblNetTotalDBNoteAmnt" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-1" style="text-align: right; width: 11%">
                                                            <asp:Label ID="lblNetTotallblPendingAmnt" runat="server"></asp:Label>
                                                        </div>

                                                    </div>
                                                </b>
                                            </td>
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
            </div>
            <%--hidden fields--%>
            <asp:HiddenField ID="hidrcptheadidno" runat="server" />
            <asp:HiddenField ID="hidmindate" runat="server" />
            <asp:HiddenField ID="hidmaxdate" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgBtnExcel" />
        </Triggers>
    </asp:UpdatePanel>
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
