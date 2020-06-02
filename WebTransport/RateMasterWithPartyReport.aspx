<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RateMasterWithPartyReport.aspx.cs" Inherits="WebTransport.RateMasterWithPartyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function () {

            setDatecontrol();
        });

        prm.add_endRequest(function () {

            setDatecontrol();
        });


        function HideBillAgainst() {
            $("#dvGrdetails").fadeOut(300);
        }

        function ShowClient() {
            $("#dvGrdetails").fadeIn(300);
        }
        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                return false;
            }
            else {
                return true;
            }
        }
    </script>
            <div class="row">
                <div class="col-lg-12 center-block responsive-Sale-bill-container">
                    <div class="ibox float-e-margins maximizing-form">
                        <div class="ibox-title">
                            <div class="col-sm-8">
                                <h5>Rate Master Report [Party Wise]</h5>
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
                                                <asp:DropDownList ID="ddlDateRange" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Date From </span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="input-sm datepicker form-control" MaxLength="12" data-date-format="dd-mm-yyyy" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDateTo" runat="server" ControlToValidate="txtDateFrom" SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage=" Please Enter Date!" Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div id="ctl00_ContentPlaceHolder1_UpdatePanel3">
                                                <div class="control-holder full-width">
                                                    <span class="filter-label">Date To </span>
                                                </div>
                                                <div class="control-holder full-width">
                                                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="input-sm datepicker form-control" MaxLength="12" data-date-format="dd-mm-yyyy" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDateTo" SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage=" Please Enter Date!" Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Location</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:DropDownList ID="drpBaseCity" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpBaseCity_SelectedIndexChanged" AutoPostBack="false">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="HidFrmCityIdno" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Party Name</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:DropDownList ID="ddlPartyName" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPartyName" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Preview" class="classValidation" ErrorMessage="Select Party Name!"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Product Name</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" AutoPostBack="true">
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
                                    <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="u-table u-table-green u-table-stripped fixed-header-table bold-last-row row-selection no-wrap-cell" GridLines="None" BorderWidth="0" ShowFooter="true" AllowPaging="true" PageSize="30" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand" OnRowDataBound="grdMain_RowDataBound">
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
                                            <asp:TemplateField HeaderText="Location" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="Left" Width="80" />
                                                <ItemTemplate>
                                                    <%#Convert.ToString(Eval("LocationName"))%>
                                                    <asp:Label ID="lblLocCityIdno" Visible="false" runat="server" Text='<%#Eval("Loc_Idno")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="Left" Width="80" />
                                                <ItemTemplate>
                                                    <%#Convert.ToString(Eval("Item_Name"))%>
                                                    <asp:Label ID="lblItemIdno" Visible="false" runat="server" Text='<%#Eval("Item_Idno")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="80" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRteDate" runat="server" Text='<%#Convert.ToDateTime(Eval("Rate_Date")).ToString("dd-MM-yyyy") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="City From" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="Left" Width="80" />
                                                <ItemTemplate>
                                                    <%#Convert.ToString(Eval("LocationName"))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Via City" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="Left" Width="80" />
                                                <ItemTemplate>
                                                    <%#Convert.ToString(Eval("FromCity"))%>
                                                    <asp:Label ID="lblFromCityIdno" Visible="false" runat="server" Text='<%#Eval("FromCityId")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="City To" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="Left" Width="80" />
                                                <ItemTemplate>
                                                    <%#Convert.ToString(Eval("ToCityName"))%>
                                                    <asp:Label ID="lblCityIdno" Visible="false" runat="server" Text='<%#Eval("ToCity_Idno")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Left" Font-Bold="true" />
                                                <FooterTemplate>
                                                    <b>Total</b>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Weight" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                <ItemStyle HorizontalAlign="right" Width="70" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemWeight" runat="server" Text='<%#Convert.ToString(Eval("Item_Weight"))%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <FooterTemplate>
                                                    <b>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                <ItemStyle HorizontalAlign="right" Width="70" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemRate" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("Item_Rate"))) ? "0.00" : (Convert.ToString((Eval("Item_Rate")))))%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblItemTotalRate" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("Item_Rate"))) ? "0.00" : (Convert.ToString((Eval("Item_Rate")))))%>'></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
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
            </div>
            <%--hidden fields--%>
            <asp:HiddenField ID="hidrateid" runat="server" Value="0" />
            <asp:HiddenField ID="Hidrowid" runat="server" Value="" />
            <asp:HiddenField ID="HidInvoiceTyp" runat="server" />
            <asp:HiddenField ID="dmindate" runat="server" />
            <asp:HiddenField ID="hidmindate" runat="server" />
            <asp:HiddenField ID="hidmaxdate" runat="server" />
            <asp:HiddenField ID="hidSaveType" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
</asp:Content>
