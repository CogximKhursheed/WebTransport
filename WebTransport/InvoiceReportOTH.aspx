<%@ Page Title="Invoice Report" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="InvoiceReportOTH.aspx.cs" EnableEventValidation="false"
    Inherits="WebTransport.InvoiceReportOTH" %>

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
                                                <span class="filter-label">Date Range <span class="required-field">*</span></span>
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
                                                <asp:DropDownList ID="ddlRepType" runat="server" CssClass="form-control" Disabled="Disabled">
                                                    <asp:ListItem Text="InvoiceWiseDetails" Value="0"> </asp:ListItem>
                                                    <asp:ListItem Text="GrWiseDetails" Value="1"> </asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Invoice No.</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:TextBox ID="txtInvoiceNo" runat="server" placeHolder="Invoice Number" CssClass="form-control" MaxLength="12"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left" style="display: none;">
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
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Lorry No.</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:TextBox ID="txtLorryNo" runat="server" placeHolder="Invoice Number" CssClass="form-control" MaxLength="12"></asp:TextBox>
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
                                                        <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="center"
                                                            Visible="true">
                                                            <ItemStyle HorizontalAlign="center" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>.
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="center" Width="70" ForeColor="White" Font-Bold="true" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Inv.No" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="center">
                                                            <HeaderStyle HorizontalAlign="center" Width="70" Font-Bold="true" />
                                                            <ItemStyle HorizontalAlign="center" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("Inv_No")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invo.Date" HeaderStyle-Width="120" HeaderStyle-HorizontalAlign="center">
                                                            <HeaderStyle HorizontalAlign="center" Width="120" Font-Bold="true" />
                                                            <ItemStyle HorizontalAlign="center" Width="120" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Inv_Date"))) ? "" : (Convert.ToDateTime((Eval("Inv_Date"))).ToString("dd-MMM-yyyy")))%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--  <asp:TemplateField HeaderText="Lorry No." HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="center">
                                                                    <HeaderStyle HorizontalAlign="center" Width="100" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="center" Width="100" />
                                                                    <ItemTemplate>
                                                                        <%#Eval("Lorry_No")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Lorry No." HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="center">
                                                            <HeaderStyle HorizontalAlign="center" Width="70" Font-Bold="true" />
                                                            <ItemStyle HorizontalAlign="center" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("Lorry_No")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SenderName" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="left">
                                                            <HeaderStyle HorizontalAlign="left" Width="150" Font-Bold="true" />
                                                            <ItemStyle HorizontalAlign="left" Width="150" />
                                                            <ItemTemplate>
                                                                <%#Eval("Party_Name")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="From City" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="center">
                                                            <HeaderStyle HorizontalAlign="center" Width="70" Font-Bold="true" />
                                                            <ItemStyle HorizontalAlign="center" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("From_City")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To City" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="center">
                                                            <HeaderStyle HorizontalAlign="center" Width="70" Font-Bold="true" />
                                                            <ItemStyle HorizontalAlign="center" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("To_City")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="From Date" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="center">
                                                            <HeaderStyle HorizontalAlign="center" Width="70" Font-Bold="true" />
                                                            <ItemStyle HorizontalAlign="center" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("DateFrom"))) ? "" : (Convert.ToDateTime((Eval("DateFrom"))).ToString("dd-MMM-yyyy")))%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Return Date" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="center">
                                                            <HeaderStyle HorizontalAlign="center" Width="70" Font-Bold="true" />
                                                            <ItemStyle HorizontalAlign="center" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("ReturnDate"))) ? "" : (Convert.ToDateTime((Eval("ReturnDate"))).ToString("dd-MMM-yyyy")))%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Gross Amnt" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="center">
                                                            <HeaderStyle HorizontalAlign="center" Width="100" Font-Bold="true" />
                                                            <ItemStyle HorizontalAlign="right" Width="100" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Gross_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Gross_Amnt")))))%>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblGrossAmount" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Net Amnt" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="center">
                                                            <HeaderStyle HorizontalAlign="center" Width="70" Font-Bold="true" />
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Net_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Net_Amnt")))))%>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblNetAmount" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <%-- <asp:TemplateField HeaderText="Gr Amnt" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="center">
                                                                    <HeaderStyle HorizontalAlign="center" Width="100" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="right" Width="100" />
                                                                    <ItemTemplate>
                                                                      <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Gr_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Gr_Amnt")))))%>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <b><asp:Label ID="lblGrAmnt" runat="server"></asp:Label></b>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField><asp:TemplateField HeaderText="Gr No." HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="center">
                                                                    <HeaderStyle HorizontalAlign="center" Width="100" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="center" Width="100" />
                                                                    <ItemTemplate>
                                                                        <%#Eval("Gr_No")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField><asp:TemplateField HeaderText="Chln No" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="center">
                                                                    <HeaderStyle HorizontalAlign="center" Width="100" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="center" Width="100" />
                                                                    <ItemTemplate>
                                                                        <%#Eval("Chln_No")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField><asp:TemplateField HeaderText="Gr Shrtg Amnt" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="center">
                                                                    <HeaderStyle HorizontalAlign="center" Width="100" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="center" Width="100" />
                                                                    <ItemTemplate>
                                                                        <%#Eval("GRshortage")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="center">
                                                                    <HeaderStyle HorizontalAlign="center" Width="100" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="right" Width="100" />
                                                                    <ItemTemplate>
                                                                        <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Tot_Qty"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Tot_Qty")))))%>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblQty" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Weight" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="center">
                                                                    <HeaderStyle HorizontalAlign="center" Width="100" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="right" Width="100" />
                                                                    <ItemTemplate>
                                                                        <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Tot_Weght"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Tot_Weght")))))%>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblWeight" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField><asp:TemplateField HeaderText="Rec.Name " HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="left">
                                                                    <HeaderStyle HorizontalAlign="left" Width="150" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="150" />
                                                                    <ItemTemplate>
                                                                        <%#Eval("Receiver")%>
                                                                    </ItemTemplate>
                                                                    <FooterStyle HorizontalAlign="center" />
                                                                   <FooterTemplate>
                                                                        <b>GRID TOTAL</b>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Bility" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="right">
                                                                    <HeaderStyle HorizontalAlign="right" Width="50" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="right" Width="50" />
                                                                    <ItemTemplate>
                                                                        <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Bilty_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Bilty_Amnt")))))%>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <b><asp:Label ID="lblBiltyAmount" runat="server"></asp:Label></b>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Short.Amnt" HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="center">
                                                                    <HeaderStyle HorizontalAlign="center" Width="90" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="right" Width="90" />
                                                                    <ItemTemplate>
                                                                        <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Short_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Short_Amnt")))))%>
                                                                    </ItemTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <FooterTemplate>
                                                                        <b><asp:Label ID="lblShortAmount" runat="server"></asp:Label></b>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="T.ServTax" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="center">
                                                                    <HeaderStyle HorizontalAlign="center" Width="70" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="right" Width="70" />
                                                                    <ItemTemplate>
                                                                        <%#(string.IsNullOrEmpty(Convert.ToString(Eval("TrServTax_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("TrServTax_Amnt")))))%>
                                                                    </ItemTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <FooterTemplate>
                                                                        <b><asp:Label ID="lblTrServTaxAmount" runat="server"></asp:Label></b>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="C.ServTax" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="center">
                                                                    <HeaderStyle HorizontalAlign="center" Width="70" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="right" Width="70" />
                                                                    <ItemTemplate>
                                                                        <%#(string.IsNullOrEmpty(Convert.ToString(Eval("ConsignrServTax"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("ConsignrServTax")))))%>
                                                                    </ItemTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <FooterTemplate>
                                                                        <b><asp:Label ID="lblConsignrServTaxAmount" runat="server"></asp:Label></b>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Approved" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="left">
                                                                    <HeaderStyle HorizontalAlign="left" Width="150" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="150" />
                                                                    <ItemTemplate>
                                                                        <%#Eval("Approved")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Details" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100">
                                                            <HeaderStyle HorizontalAlign="Center" Width="100px" Font-Bold="true" />
                                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <div style="float: inherit; width: 10%;">
                                                                    <asp:HiddenField ID="hidInvoiceDetails_Idno" runat="server" Value='<%#Eval("ID")%>' />
                                                                    <asp:ImageButton ID="imgBtnInvoiceDetails" class="OnShowToolTip" runat="server" Width="15px"
                                                                        Height="15px" CommandName="cmdInvoiceDetails" OnClientClick="return False" />
                                                                    <div style="z-index: 999; position: absolute; display: none; width: 200px; float: left; margin-left: -200px; margin-top: 10px;"
                                                                        id="dvInvoiceDetails" runat="server">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="false"
                                                                                        Width="100%" GridLines="Both">
                                                                                        <PagerStyle CssClass="classPager" />
                                                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" Position="Bottom" LastPageText="Last" />
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
                                                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                                        <EmptyDataTemplate>
                                                                                            <asp:Label ID="lblnorecord" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
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
                                                        <div class="col-sm-1" style="text-align: left; width: 9%"></div>
                                                        <div class="col-sm-1" style="text-align: left; width: 5%"></div>
                                                        <div class="col-sm-1" style="text-align: center; width: 9%"></div>
                                                        <div class="col-sm-1" style="text-align: center; width: 7%"></div>
                                                        <div class="col-sm-1" style="text-align: center; width: 7%"></div>
                                                        <div class="col-sm-1" style="text-align: left; width: 8%">
                                                            <asp:Label ID="lblGrossAmnt" runat="server"></asp:Label>
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
            <%--HIDDEN FIELDS--%>
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
