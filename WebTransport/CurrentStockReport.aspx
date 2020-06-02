<%@ Page Title="CURRENT STOCK REPORT [TYRE]" Language="C#" MasterPageFile="~/Site1.Master"
    EnableEventValidation="false" AutoEventWireup="true" CodeBehind="CurrentStockReport.aspx.cs"
    Inherits="WebTransport.CurrentStockReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12 center-block responsive-Sale-bill-container">
            <div class="ibox float-e-margins maximizing-form">
                <div class="ibox-title">
                    <div class="col-sm-8">
                        <h5>CURRENT STOCK REPORT [TYRE]</h5>
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
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="imgBtnExcel" />
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
                                        <span class="filter-label">Date Range </span>
                                    </div>
                                    <div class="control-holder full-width">
                                        <asp:DropDownList ID="ddlDateRange" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6 pull-left">
                                    <div class="control-holder full-width">
                                        <span class="filter-label">Serial No.</span>
                                    </div>
                                    <div class="control-holder full-width">
                                        <asp:TextBox ID="txtSerialNo" runat="server" PlaceHolder="Serial Number" AutoPostBack="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6 pull-left">
                                    <div class="control-holder full-width">
                                        <span class="filter-label">Item Name</span>
                                    </div>
                                    <div class="control-holder full-width">
                                        <asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6 pull-left">
                                    <div class="control-holder full-width">
                                        <span class="filter-label">Loc.[From]</span>
                                    </div>
                                    <div class="control-holder full-width">
                                        <asp:DropDownList ID="ddlFromCity" runat="server" CssClass="form-control"></asp:DropDownList>
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
                                        <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="u-table u-table-green u-table-stripped fixed-header-table bold-last-row row-selection no-wrap-cell" GridLines="None" BorderWidth="0" ShowFooter="true" AllowPaging="true" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCreated="grdMain_RowCreated" OnRowDataBound="grdMain_RowDataBound">
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
    </div>
    <%--HIDDEN FIELDS--%>
    <asp:HiddenField ID="hidmindate" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />
    <asp:HiddenField ID="hidrcptheadidno" runat="server" />
</asp:Content>
