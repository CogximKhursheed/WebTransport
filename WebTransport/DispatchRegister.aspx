﻿<%@ Page Title="Dispatch Register" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="DispatchRegister.aspx.cs" EnableEventValidation="false"
    Inherits="WebTransport.DispatchRegister" %>

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
                                <h5>Dispatch Register</h5>
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
                                                                <%--For Lagre Record Use <httpRuntime maxRequestLength="1073741824"/> in Web.config File--%>
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
                                                <asp:DropDownList ID="drpBaseCity" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Destination</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:DropDownList ID="ddlDestination" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Lorry Type</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:DropDownList ID="ddllorrytype" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="All" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Own" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Hire" Value="1"></asp:ListItem>
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
                                                        <asp:TemplateField HeaderText="GR No." HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("Gr_No")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GR Date" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" Width="80" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Gr_Date"))) ? "" : (Convert.ToDateTime((Eval("Gr_Date"))).ToString("dd-MMM-yyyy")))%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To City" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="80" />
                                                            <ItemTemplate>
                                                                <%#Eval("Delivery_Place")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("Item_Rate")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Lorry No." HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="80" />
                                                            <ItemTemplate>
                                                                <%#Eval("LorryNo")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Owner Name" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="80" />
                                                            <ItemTemplate>
                                                                <%#Eval("OwnerName")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Weight" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("Tot_Weght")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GR Amount" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Amount"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Amount")))))%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Chln Adv Amnt" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("Adv_Amnt")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Diesel Amnt" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("Diesel_Amnt")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TDS" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("TDS_Amnt")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Commission" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Commsn_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Commsn_Amnt")))))%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Shortage Qty" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("Shortage_Qty")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Balance" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("Net_Amnt")%>
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
                                                        <div class="col-sm-2" style="width: 36%; text-align: left">
                                                            <asp:Label ID="lblcontant" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-3" style="width: 11%; text-align: left">TOTAL</div>
                                                        <div class="col-sm-2" style="width: 07%; text-align: left">
                                                            <asp:Label ID="lbltotweight" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-1" style="width: 10%; text-align: left">
                                                            <asp:Label ID="lblGrossAmnt" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-1" style="width: 10%; text-align: left">
                                                            <asp:Label ID="lblSurcharge" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-3" style="width: 07%; text-align: left">
                                                            <asp:Label ID="lblDieselTotal" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-1" style="width: 06%; text-align: left;">
                                                            <asp:Label ID="lblWages" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-1" style="width: 7%; text-align: left">&nbsp;<asp:Label ID="lblServtax" runat="server"></asp:Label></div>
                                                        <div class="col-sm-1" style="width: 5%; text-align: left">
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
