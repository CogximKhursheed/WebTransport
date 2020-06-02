<%@ Page Title="Invoice Report" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="InvoiceReport.aspx.cs" EnableEventValidation="false"
    Inherits="WebTransport.InvoiceReport" %>

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

        $(document).ready(function () {
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
                                <h5>Invoice Report</h5>
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
                                                                        <asp:LinkButton ID="lnkBtn" runat="server" ToolTip="Click to print" Visible="false" OnClick="lnkBtn_Click"><i class="fa fa-print icon"></i></asp:LinkButton>
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
                                                <span class="filter-label">Sender Name</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:DropDownList ID="drpSenderName" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Loc.[From]</span>
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
                                                <asp:DropDownList ID="ddlRepType" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="InvoiceWiseDetails" Value="0"> </asp:ListItem>
                                                    <asp:ListItem Text="GrWiseDetails" Value="1"> </asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Invoice No</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:TextBox ID="txtInvoiceNo" runat="server" placeHolder="Invoice Number" CssClass="form-control" MaxLength="12"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Invoice Type</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:DropDownList ID="ddlInvType" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="All" Value="-1"> </asp:ListItem>
                                                    <asp:ListItem Text="Approved" Value="1"> </asp:ListItem>
                                                    <asp:ListItem Text="Unapproved" Value="0"> </asp:ListItem>
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
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="u-table u-table-green u-table-stripped fixed-header-table bold-last-row row-selection no-wrap-cell" GridLines="None" BorderWidth="0" ShowFooter="true" AllowPaging="true" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand" OnRowDataBound="grdMain_RowDataBound">
                                                    <PagerStyle CssClass="classPager" />
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" Position="Bottom" LastPageText="Last" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="center"
                                                            Visible="true">
                                                            <ItemStyle HorizontalAlign="center" Width="40" />
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>.
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="center" Width="40" ForeColor="White" Font-Bold="true" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Inv.No" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("Inv_No")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invo.Date" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" Width="80" />
                                                            <ItemTemplate>
                                                                <%#Eval("Inv_Date")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Gr No." HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("Gr_No")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Gr Date" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Gr_Date"))) ? "" : (Convert.ToDateTime((Eval("Gr_Date"))).ToString("dd-MMM-yyyy")))%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Lorry No." HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("Lorry_No")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Gr Amnt" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Gr_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Gr_Amnt")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblGrAmnt" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Chln No" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="50" />
                                                            <ItemTemplate>
                                                                <%#Eval("Chln_No")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Gr Shrtg Amnt" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("GRshortage")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Tot_Qty"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Tot_Qty")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblQty" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Weight" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Tot_Weght"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Tot_Weght")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblWeight" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="From City" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="50" />
                                                            <ItemTemplate>
                                                                <%#Eval("City_Name")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sender Name" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="50" />
                                                            <ItemTemplate>
                                                                <%#Eval("SenderName")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rec.Name" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="50" />
                                                            <ItemTemplate>
                                                                <%#Eval("Receiver")%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="center" />
                                                            <FooterTemplate>
                                                                <b>GRID TOTAL</b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Gross Amnt" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="50" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Gross_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Gross_Amnt")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblGrossAmount" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bility" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="right">

                                                            <ItemStyle HorizontalAlign="right" Width="50" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Bilty_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Bilty_Amnt")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblBiltyAmount" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Short.Amnt" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Short_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Short_Amnt")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblShortAmount" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="T.ServTax" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("TrServTax_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("TrServTax_Amnt")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblTrServTaxAmount" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="C.ServTax" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("ConsignrServTax"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("ConsignrServTax")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblConsignrServTaxAmount" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tr SGST" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("TrSGST_Amt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("TrSGST_Amt")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblSGSTAmount" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tr CGST" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("TrCGST_Amt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("TrCGST_Amt")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblCGSTAmount" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tr IGST" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("TrIGST_Amt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("TrIGST_Amt")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblIGSTAmount" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="C SGST" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("ConSGST_Amt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("ConSGST_Amt")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblCSGSTAmount" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="C CGST" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("ConCGST_Amt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("ConCGST_Amt")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblCCGSTAmount" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="C IGST" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("ConIGST_Amt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("ConIGST_Amt")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblCIGSTAmount" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Net Amnt" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Net_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Net_Amnt")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblNetAmount" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Approved" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="50" />
                                                            <ItemTemplate>
                                                                <%#Eval("Approved")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Details" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" Width="100px" Font-Bold="true" />
                                                            <ItemStyle HorizontalAlign="Center" Width="50" />
                                                            <ItemTemplate>
                                                                <div style="float: inherit; width: 10%;">
                                                                    <asp:HiddenField ID="hidInvoiceDetails_Idno" runat="server" Value='<%#Eval("ID")%>' />
                                                                    <asp:ImageButton ID="imgBtnInvoiceDetails" class="OnShowToolTip" runat="server" Width="15px"
                                                                        Height="15px" CommandName="cmdInvoiceDetails" OnClientClick="return False" />
                                                                    <div style="z-index: 999; position: absolute; display: none; width: 200px; float: left; margin-left: -400px; margin-top: 10px;"
                                                                        id="dvInvoiceDetails" runat="server">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="false"
                                                                                        Width="100%" GridLines="Both">
                                                                                        <RowStyle CssClass="RowStyle" />
                                                                                        <AlternatingRowStyle CssClass="AlternateRowStyle" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="Center">
                                                                                                <ItemStyle Width="90" HorizontalAlign="Center" />
                                                                                                <ItemTemplate>
                                                                                                    <%#Convert.ToString(Eval("ItemName")) %>
                                                                                                </ItemTemplate>
                                                                                                <FooterStyle HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="UNIT" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                                                                <ItemStyle Width="50" HorizontalAlign="Center" />
                                                                                                <ItemTemplate>
                                                                                                    <%#Convert.ToString(Eval("Unit")) %>
                                                                                                </ItemTemplate>
                                                                                                <FooterStyle HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>
                                                                                             <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                                                                <ItemStyle Width="50" HorizontalAlign="Center" />
                                                                                                <ItemTemplate>
                                                                                                    <%#Convert.ToString(Eval("Qty")) %>
                                                                                                </ItemTemplate>
                                                                                                <FooterStyle HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Weight" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                                                                <ItemStyle Width="50" HorizontalAlign="Center" />
                                                                                                <ItemTemplate>
                                                                                                    <%#Convert.ToString(Eval("Weight")) %>
                                                                                                </ItemTemplate>
                                                                                                <FooterStyle HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                                                                <ItemStyle Width="50" HorizontalAlign="Center" />
                                                                                                <ItemTemplate>
                                                                                                    <%#Convert.ToString(Eval("Rate")) %>
                                                                                                </ItemTemplate>
                                                                                                <FooterStyle HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                                                                <ItemStyle Width="50" HorizontalAlign="Center" />
                                                                                                <ItemTemplate>
                                                                                                    <%#Convert.ToString(Eval("Amount")) %>
                                                                                                </ItemTemplate>
                                                                                                <FooterStyle HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <EmptyDataTemplate>
                                                                                            <asp:Label ID="lblnorecord" runat="server" Text="No record found"></asp:Label>
                                                                                        </EmptyDataTemplate>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
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
                                                        <div class="col-sm-3" style="text-align: right; width: 18%; padding-right: 35px;">TOTAL</div>
                                                        <div class="col-sm-1" style="text-align: left; width: 9%">
                                                            <asp:Label ID="lblGrossAmnt" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-1" style="text-align: left; width: 5%">
                                                            <asp:Label ID="lblBilAmnt" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-1" style="text-align: center; width: 9%">
                                                            <asp:Label ID="lblShrtAmnt" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-1" style="text-align: center; width: 7%">
                                                            <asp:Label ID="lblTrServTax" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-1" style="text-align: center; width: 7%">
                                                            <asp:Label ID="lblConServtax" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-1" style="text-align: left; width: 8%">
                                                            <asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label>
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
            <asp:HiddenField ID="hidPrintType" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgBtnExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="Print" runat="server">
        <ContentTemplate>
            <div id="print" style="display: none;">
                <style>
                    th, td {
                        white-space: nowrap;
                    }
                </style>
                <table width="100%" cellpadding="5" cellspacing="0" border="1" style="border-collapse: collapse">
                    <thead>
                        <tr>
                            <asp:Repeater ID="Repeater1" runat="server">
                                <HeaderTemplate>
                                    <th style="font-size: 12px; font-family: arial; border: 1px solid;">L.R.No.</th>
                                    <th style="font-size: 12px; font-family: arial; border: 1px solid;">L.R.Date</th>
                                    <th style="font-size: 12px; font-family: arial; border: 1px solid;">Truck No.</th>
                                    <th style="font-size: 12px; font-family: arial; border: 1px solid;">Order No.</th>
                                    <th style="font-size: 12px; font-family: arial; border: 1px solid;">INVOICE .NO</th>
                                    <th style="font-size: 12px; font-family: arial; border: 1px solid;">INV.  DT.</th>
                                    <th style="font-size: 12px; font-family: arial; border: 1px solid;">DI. NO.</th>
                                    <th style="font-size: 12px; font-family: arial; border: 1px solid;">PARTY</th>
                                    <th style="font-size: 12px; font-family: arial; border: 1px solid;">Destination</th>
                                    <th style="font-size: 12px; font-family: arial; border: 1px solid;">MT</th>
                                    <th style="font-size: 12px; font-family: arial; border: 1px solid;">Rate</th>
                                    <th style="font-size: 12px; font-family: arial; border: 1px solid;">TOLL</th>
                                    <th style="font-size: 12px; font-family: arial; border: 1px solid;">U/L</th>
                                    <th style="font-size: 12px; font-family: arial; border: 1px solid;">SHORT MT</th>
                                    <th style="font-size: 12px; font-family: arial; border: 1px solid;">COMM</th>
                                    <th style="font-size: 12px; font-family: arial; border: 1px solid;">AMT.</th>
                                    <th style="font-size: 12px; font-family: arial; border: 1px solid;">ADV </th>
                                    <th style="font-size: 12px; font-family: arial; border: 1px solid;">DEISEL</th>
                                    <th style="font-size: 12px; font-family: arial; border: 1px solid;">OWNER NAME</th>
                                    <th style="font-size: 12px; font-family: arial; border: 1px solid;">PAN  </th>
                                    <th style="font-size: 12px; font-family: arial; border: 1px solid;">BILL NO.</th>
                                    <th style="font-size: 12px; font-family: arial; border: 1px solid;">REMARK</th>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tbody>
                                        <tr>
                                            <td style="font-size: 12px; font-family: arial; border: 1px solid;"><%#Eval("Gr_No")%></td>
                                            <td style="font-size: 12px; font-family: arial; border: 1px solid;"><%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MM-yyyy")%></td>
                                            <td style="font-size: 12px; font-family: arial; border: 1px solid;"><%#Eval("LORRY_NO")%></td>
                                            <td style="font-size: 12px; font-family: arial; border: 1px solid;"><%#Eval("Ordr_No")%></td>
                                            <td style="font-size: 12px; font-family: arial; border: 1px solid;"><%#Eval("Inv_No")%></td>
                                            <td style="font-size: 12px; font-family: arial; border: 1px solid;"><%#Convert.ToDateTime(Eval("Inv_Date")).ToString("dd-MM-yyyy")%></td>
                                            <td style="font-size: 12px; font-family: arial; border: 1px solid;"><%#Eval("DI_NO")%></td>
                                            <td style="font-size: 12px; font-family: arial; border: 1px solid;"><%#Eval("Recivr_Name")%></td>
                                            <td style="font-size: 12px; font-family: arial; border: 1px solid;"><%#Eval("Delvry_Place")%></td>
                                            <td style="font-size: 12px; font-family: arial; border: 1px solid;"><%#Eval("Qty")%></td>
                                            <td style="font-size: 12px; font-family: arial; border: 1px solid;"><%#Eval("Rate")%></td>
                                            <td style="font-size: 12px; font-family: arial; border: 1px solid;"><%#(Eval("TollTax_Amnt"))%></td>
                                            <td style="font-size: 12px; font-family: arial; border: 1px solid;"><%#Eval("UL")%></td>
                                            <td style="font-size: 12px; font-family: arial; border: 1px solid;"><%#Eval("SHORT_MT")%></td>
                                            <td style="font-size: 12px; font-family: arial; border: 1px solid;"><%#Eval("COMM")%></td>
                                            <td style="font-size: 12px; font-family: arial; border: 1px solid;"><%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%></td>
                                            <td style="font-size: 12px; font-family: arial; border: 1px solid;"><%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Adv")))%></td>
                                            <td style="font-size: 12px; font-family: arial; border: 1px solid;"><%#String.Format("{0:0.00}", Convert.ToDouble(Eval("DIESEL")))%></td>
                                            <td style="font-size: 12px; font-family: arial; border: 1px solid;"><%#Eval("Owner_Name")%></td>
                                            <td style="font-size: 12px; font-family: arial; border: 1px solid;"><%#Eval("PAN")%></td>
                                            <td style="font-size: 12px; font-family: arial; border: 1px solid;"><%#Eval("Bill_No")%></td>
                                            <td style="font-size: 12px; font-family: arial; border: 1px solid;"><%#Eval("REMARK")%></td>
                                        </tr>
                                    </tbody>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </asp:Repeater>
                        </tr>
                    </thead>
                </table>
            </div>
        </ContentTemplate>
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
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            if (strid == 'print') {
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
        }
    </script>
</asp:Content>
