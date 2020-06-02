<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DocumentReport.aspx.cs" Inherits="WebTransport.DocumentReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12 center-block responsive-Sale-bill-container">
            <div class="ibox float-e-margins maximizing-form">
                <div class="ibox-title">
                    <div class="col-sm-8">
                        <h5>DOCUMENT REPORT [ADMIN]</h5>
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
                                <div class="col-sm-6 pull-left">
                                    <div class="control-holder full-width">
                                        <span class="filter-label">Date From </span>
                                    </div>
                                    <div class="control-holder full-width">
                                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="input-sm datepicker form-control" MaxLength="12" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtDateFrom" SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage=" Please Enter Date!" Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-6 pull-left">
                                    <div id="ctl00_ContentPlaceHolder1_UpdatePanel3">
                                        <div class="control-holder full-width">
                                            <span class="filter-label">Date To </span>
                                        </div>
                                        <div class="control-holder full-width">
                                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="input-sm datepicker form-control" MaxLength="12" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDateTo" SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage=" Please Enter Date!" Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 pull-left">
                                    <div class="control-holder full-width">
                                        <span class="filter-label">Doucment Type</span>
                                    </div>
                                    <div class="control-holder full-width">
                                        <asp:DropDownList ID="ddlDoucmentType" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="Challan" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="GR" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Invoice" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Voucher" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6 pull-left">
                                    <div class="control-holder full-width">
                                        <span class="filter-label">Employee Name</span>
                                    </div>
                                    <div class="control-holder full-width">
                                        <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6 pull-left">
                                    <div class="control-holder full-width">
                                        <span class="filter-label">Document No</span>
                                    </div>
                                    <div class="control-holder full-width">
                                        <asp:TextBox ID="txtDocumentNo" PlaceHolder="Document Number" runat="server" CssClass="form-control"></asp:TextBox>
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
                            <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="u-table u-table-green u-table-stripped fixed-header-table bold-last-row row-selection no-wrap-cell" GridLines="None" BorderWidth="0" ShowFooter="true" OnRowDataBound="grdMain_RowDataBound" OnRowCommand="grdMain_RowCommand">
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
                                    <asp:TemplateField HeaderText="Emp Name" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left" Width="80" />
                                        <ItemTemplate>
                                            <%#Eval("EmpName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document No." HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" Width="70" />
                                        <ItemTemplate>
                                            <%#Eval("DocNo")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center" Width="80" />
                                        <ItemTemplate>
                                            <%#Convert.ToDateTime(Eval("DocDate")).ToString("dd-MM-yyyy")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left" Width="80" />
                                        <ItemTemplate>
                                            <%#Eval("Location")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Party" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left" Width="80" />
                                        <ItemTemplate>
                                            <%#Eval("PartyName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Net Amount" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                        <ItemStyle HorizontalAlign="right" Width="70" />
                                        <ItemTemplate>
                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Amount")))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
    <script type="text/javascript">
        $(".datepicker").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
        });
    </script>
</asp:Content>
