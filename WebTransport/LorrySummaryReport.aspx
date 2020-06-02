<%@ Page Title="Lorry Master Report" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" EnableEventValidation="false" CodeBehind="LorrySummaryReport.aspx.cs"
    Inherits="WebTransport.LorrySummaryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .alternate-row-color tr:nth-child(2n) {
            background: #e1fbff;
        }

        .alternate-row-color tr:hover:not(:first-child) {
            cursor: pointer;
            background: #cae7ec !important;
        }

        .form-heading {
            font-weight: bold;
            text-transform: uppercase;
        }

        .u-autocomplete {
            display: inline-block;
            width: 100%;
            position: relative;
        }

            .u-autocomplete input {
                display: inline-block;
                width: 100%;
            }

            .u-autocomplete .u-list-holder {
                display: none;
                position: absolute;
                top: 24px;
                left: 0px;
                width: 100%;
                background: white;
                border: 1px solid silver;
                max-height: 200px;
                overflow-y: auto;
                box-shadow: 2px 1px 3px #c5c5c5;
                z-index: 9999;
            }

            .u-autocomplete .u-list {
                list-style: none;
                padding: 0;
                margin: 0;
            }

            .u-autocomplete .u-list-item {
                padding: 2px 5px;
                font-size: 13px;
                color: #303030;
            }

            .u-autocomplete:hover > .u-list-holder, .u-autocomplete input[type=text]:focus + .u-list-holder, .u-autocomplete input[type=text]:active + .u-list-holder {
                display: block;
            }

            .u-autocomplete .u-list-item:hover, .u-list-item.selected {
                background: skyblue;
                cursor: pointer;
            }

        .loading-img {
            display: none;
            position: absolute;
            top: 5px;
            right: 3px;
            width: 16px;
            height: 16px;
            background-image: url('images/indicator.gif');
            background-size: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 center-block responsive-Sale-bill-container">
                    <div class="ibox float-e-margins maximizing-form">
                        <div class="ibox-title">
                            <div class="col-sm-8">
                                <h5>Lorry Summary Report</h5>
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
                                                <asp:TextBox ID="txtDatefrom" runat="server" CssClass="input-sm datepicker form-control" MaxLength="12" data-date-format="dd-mm-yyyy" TabIndex="2"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDateFrom" runat="server" ControlToValidate="txtDatefrom" SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage=" Please Enter Date!" Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div id="ctl00_ContentPlaceHolder1_UpdatePanel3">
                                                <div class="control-holder full-width">
                                                    <span class="filter-label">Date To </span>
                                                </div>
                                                <div class="control-holder full-width">
                                                    <asp:TextBox ID="txtDateto" runat="server" CssClass="input-sm datepicker form-control" MaxLength="12" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvDateTo" runat="server" ControlToValidate="txtDateto" SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage=" Please Enter Date!" Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Lorry No</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <div class="col-sm-8">
                                                    <div class="u-autocomplete">
                                                        <i class="loading-img"></i>
                                                        <asp:TextBox ID="txtLorryNo" AutoCompleteType="None" onfocus="$(this).next().slideDown(200)" onfocusout="$(this).next().slideUp(200)" PlaceHolder="Lorry Number" runat="server" ClientIDMode="Static" MaxLength="12" TabIndex="2" onkeypress="$(this).next('.loading-img').show();"></asp:TextBox>
                                                        <div class="u-list-holder">
                                                            <ul class="u-list">
                                                                <asp:Repeater ID="rptLorryList" runat="server">
                                                                    <ItemTemplate>
                                                                        <li class="u-list-item" data-lorryid='<%#Eval("Lorry_Idno")%>'><%#Eval("Lorry_No")%> </li>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
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
                                    <table style="width: 100%">
                                        <%if (lblOwnerName.Text != "" || lblDriverName.Text != "")
                                            { %>
                                        <tr>
                                            <td style="background: #eaeaea; border-bottom: 1px solid silver;">
                                                <div class="col-sm-6">
                                                    <b class="col-sm-5">Truck Owner Name: </b>
                                                    <asp:Label CssClass="col-sm-7" ID="lblOwnerName" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <b class="col-sm-5">Truck Driver Name: </b>
                                                    <asp:Label CssClass="col-sm-7" ID="lblDriverName" runat="server"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                        <%} %>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grdReport" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="u-table u-table-green u-table-stripped fixed-header-table bold-last-row row-selection no-wrap-cell" GridLines="None" BorderWidth="0" ShowFooter="true">
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
                                                        <asp:TemplateField HeaderText="GR Date" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" Width="80" />
                                                            <ItemTemplate>
                                                                <%#Eval("Date")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Lorry No." HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("Lorry No")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Destination" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="80" />
                                                            <ItemTemplate>
                                                                <%#Eval("Destination")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("Qty")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("Rate")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TDS" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("TDS Amnt")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Diesel" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("Diesel Amnt")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Commission" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("Commission Amnt")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Parking" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("Parking Chrg")%>
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
                                                <div class="secondFooterClass" id="divpaging" runat="server" visible="false">
                                                    <div class="col-sm-3" style="text-align: left">
                                                        <asp:Label ID="lblcontant" runat="server"></asp:Label>
                                                    </div>
                                                </div>
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
            <asp:HiddenField ID="hidLorryIdno" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hidmindate" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hidmaxdate" runat="server" ClientIDMode="Static" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--SCRIPTS FOR SETTING DATES--%>
    <script>
        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $("#<%=txtDatefrom.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
            $("#<%=txtDateto.ClientID %>").datepicker({
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

    <%--AUTOCOMPLETE DROPDOWN--%>
    <script type="text/jscript">
        $('.u-autocomplete input[type=text]').keyup(function () {
            var searchBox = $(this);
            if (searchBox.val().length > 0) {
                var text = $(this).val();
                $(".u-autocomplete .u-list-item").hide();
                var search = $(".u-autocomplete .u-list-item").filter(function () {
                    return $(this).text().toLowerCase().indexOf(text.toLowerCase()) >= 0;
                });
                search.each(function () {
                    $(this).show();
                });
            }
            else {
                $(".u-autocomplete .u-list-item").show();
            }
            searchBox.next('.loading-img').hide();
        });

        $(".u-autocomplete .u-list-item").click(function () {
            var value = $(this).text();
            var id = $(this).data('lorryid');
            $('#hidLorryIdno').val(id);
            $('.u-autocomplete input[type=text]').val(value);
            $(this).parents('.u-list-holder').hide();
        });
        function animate() {
            $(".loading-img").animate({ path: new $.path.arc(arc_params) }, 100000)
        }
    </script>
</asp:Content>
