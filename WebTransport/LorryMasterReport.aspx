<%@ Page Title="Lorry Master Report" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" EnableEventValidation="false" CodeBehind="LorryMasterReport.aspx.cs"
    Inherits="WebTransport.LorryMasterReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12 center-block responsive-Sale-bill-container">
            <div class="ibox float-e-margins maximizing-form">
                <div class="ibox-title">
                    <div class="col-sm-8">
                        <h5>LORRY MASTER REPORT</h5>
                    </div>
                    <div class="col-sm-4">
                        <div class="title-action">
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
                                        <span class="filter-label">Lorry </span>
                                    </div>
                                    <div class="control-holder full-width">
                                        <asp:DropDownList ID="ddlLorryType" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="All" Value="-1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Own" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Hire" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6 pull-left">
                                    <div class="control-holder full-width">
                                        <span class="filter-label">Date From </span>
                                    </div>
                                    <div class="control-holder full-width">
                                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="input-sm datepicker form-control" MaxLength="12" value="01-04-2014" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDateFrom" runat="server" ControlToValidate="txtDateFrom" SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage=" Please Enter Date!" Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-6 pull-left">
                                    <div id="ctl00_ContentPlaceHolder1_UpdatePanel3">
                                        <div class="control-holder full-width">
                                            <span class="filter-label">Date To </span>
                                        </div>
                                        <div class="control-holder full-width">
                                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="input-sm datepicker form-control" MaxLength="12" value="01-04-2015" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvDateTo" runat="server" ControlToValidate="txtDateTo" SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage=" Please Enter Date!" Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 pull-left">
                                    <div class="control-holder full-width">
                                        <span class="filter-label">Party Name</span>
                                    </div>
                                    <div class="control-holder full-width">
                                        <asp:DropDownList ID="ddlPartyName" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6 pull-left">
                                    <div class="control-holder full-width">
                                        <span class="filter-label">Lorry</span>
                                    </div>
                                    <div class="control-holder full-width">
                                        <asp:TextBox ID="txtLorryNo" PlaceHolder="Lorry Number" runat="server" CssClass="form-control" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6 pull-left">
                                    <div class="control-holder full-width">
                                        <span class="filter-label">Pan Catgry.</span>
                                    </div>
                                    <div class="control-holder full-width">
                                        <asp:DropDownList ID="ddlPanNo" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Not Received" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6 pull-left">
                                    <div class="control-holder full-width">
                                        <span class="filter-label">Pan No.</span>
                                    </div>
                                    <div class="control-holder full-width">
                                        <asp:TextBox ID="txtPanNumber" PlaceHolder="Pan Number" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
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
                                        <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="u-table u-table-green u-table-stripped fixed-header-table bold-last-row row-selection no-wrap-cell" GridLines="None" BorderWidth="0" ShowFooter="true" AllowPaging="true" PageSize="100" OnPageIndexChanging="grdMain_PageIndexChanging">
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
                                                <asp:TemplateField HeaderText="Lorry Type" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" Width="70" />
                                                    <ItemTemplate>
                                                        <%#(Eval("LorryType"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Party Name" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" Width="80" />
                                                    <ItemTemplate>
                                                        <%#(Eval("PartyName"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lorry No." HeaderStyle-Width="120" HeaderStyle-HorizontalAlign="Right">
                                                    <ItemStyle HorizontalAlign="Right" Width="120" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("LorryNo"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Creation Date" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" Width="80" />
                                                    <ItemTemplate>
                                                        <%# Eval("CreationDate")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Driver Name" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" Width="80" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("Driver_Name"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lorry Make" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" Width="80" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("LorryMake"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Chassis No." HeaderStyle-Width="120" HeaderStyle-HorizontalAlign="Right">
                                                    <ItemStyle HorizontalAlign="Right" Width="120" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("ChasisNo"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Engine No." HeaderStyle-Width="120" HeaderStyle-HorizontalAlign="Right">
                                                    <ItemStyle HorizontalAlign="Right" Width="120" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("EngineNo"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pan No." HeaderStyle-Width="120" HeaderStyle-HorizontalAlign="Right">
                                                    <ItemStyle HorizontalAlign="Right" Width="120" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("PanNo"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Owner Name" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" Width="80" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("OwnerName"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="D.F." HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" Width="80" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("DF"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created By Employee" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" Width="80" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("Emp_Name"))%>
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
    <script language="javascript" type="text/javascript">
        function setDatecontrol() {
            $("#<%=txtDateFrom.ClientID %>").datepicker({
                buttonImageOnly: false,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy'
            });
            $("#<%=txtDateTo.ClientID %>").datepicker({
                buttonImageOnly: false,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy'
            });
        }
    </script>
</asp:Content>
