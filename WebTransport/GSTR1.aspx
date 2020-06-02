<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="GSTR1.aspx.cs" Inherits="WebTransport.GSTR1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .fix-header + table {
            width: 1882px !important;
            overfloaw: auto !important;
        }

        #headergrdMain {
            width: 100% !important;
        }

        #grdMain {
            width: 100% !important;
        }

        table {
            border-collapse: collapse;
            border: 1px solid #ccc;
            border-collapse: collapse;
        }

            table th {
                background-color: #F7F7F7;
                color: #333;
                font-weight: normal;
                padding: 0 10px 0 10px;
            }

            table th, table td {
                border: 1px solid #ccc;
            }

            table.dataTable tbody tr:last-child {
                background: silver;
                font-weight: bold;
            }

            table.dataTable.hover tbody tr:last-child:hover, table.dataTable.display tbody tr:last-child:hover {
                background: silver !important;
            }

            table.dataTable tbody tr:last-child td span {
                font-weight: bold !important;
            }

            table.dataTable {
                display: block;
                max-height: 300px;
            }


        .classPager {
            position: absolute;
            top: 0;
            bottom: 0;
            background: white !important;
            display: inline-block;
            background: white !important;
            height: 50px;
            box-shadow: 0 0 4px gray;
        }

            .classPager td {
                background: white !important;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12 center-block responsive-Sale-bill-container">
            <div class="ibox float-e-margins maximizing-form">
                <div class="ibox-title">
                    <div class="col-sm-8">
                        <h5>GSTR-1</h5>
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
                                                            <asp:ImageButton ID="lnkbtnExport2Excel" runat="server" AlternateText="Excel" ToolTip="Export to excel" ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px" OnClick="lnkbtnExport2Excel_Click" Visible="true" />
                                                            <%--<asp:LinkButton ID="lnkbtnExport2Excel" OnClick="lnkbtnExport2Excel_Click" Title="Export to excel" class="btn btn-sm btn-primary pull-right" runat="server"  TabIndex="45"><i class="fa fa-file-excel-o"></i></asp:LinkButton>--%>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="close-download-box" title="Close download window"></div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lnkbtnExport2Excel" />
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
                                        <asp:DropDownList ID="ddlDateRange" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlDateRange" SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage="Please select Year!" Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-6 pull-left">
                                    <div class="control-holder full-width">
                                        <span class="filter-label">Loc.[From]</span>
                                    </div>
                                    <div class="control-holder full-width">
                                        <asp:DropDownList ID="ddlFromCity" runat="server" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlFromCity" SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage=" Please select From city!" Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-6 pull-left">
                                    <div class="control-holder full-width">
                                        <span class="filter-label">Month<span class="required-field">*</span></span>
                                    </div>
                                    <div class="control-holder full-width">
                                        <asp:DropDownList ID="ddlMonthRange" runat="server" AutoPostBack="true" CssClass="form-control">
                                            <asp:ListItem Value="0">All</asp:ListItem>
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlMonthRange" SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage="Please select From city!" Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 pull-right">
                            <asp:LinkButton ID="lnkbtnPreview" CssClass="btn btn-w-m btn-primary center-block" runat="server" OnClick="lnkbtnPreview_Click"><i class="fa fa-search-plus"></i>Preview</asp:LinkButton>
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
                                        <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="u-table u-table-green u-table-stripped fixed-header-table bold-last-row row-selection no-wrap-cell" GridLines="None" BorderWidth="0" ShowFooter="true" AllowPaging="true" PageSize="50">
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
                                                <asp:TemplateField HeaderText="Sender" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" Width="80" />
                                                    <ItemTemplate>
                                                        <%#Eval("Sender") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GSTIN No" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" Width="80" />
                                                    <ItemTemplate>
                                                        <%#Eval("GSTIN No")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="State Name" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" Width="80" />
                                                    <ItemTemplate>
                                                        <%#Eval("State Name")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inv No" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                                    <ItemStyle HorizontalAlign="Right" Width="70" />
                                                    <ItemTemplate>
                                                        <%#Eval("Inv No")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inv Date" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" Width="80" />
                                                    <ItemTemplate>
                                                        <%#Eval("Inv Date")%>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <FooterTemplate>
                                                        <b>
                                                            <asp:Label Font-Bold="true" runat="server" Text="Total"></asp:Label></b>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Taxable Value" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                    <ItemStyle HorizontalAlign="right" Width="70" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTaxableValue" runat="server" Text='<%#Eval("Taxable Value")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="right" />
                                                    <FooterTemplate>
                                                        <b>
                                                            <asp:Label Font-Bold="true" ID="lblTaxableTotalValue" runat="server" Text='0'></asp:Label></b>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IGST Rate" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                    <ItemStyle HorizontalAlign="right" Width="70" />
                                                    <ItemTemplate>
                                                        <%#Eval("IGST Rate")%>%
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IGST Amount" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                    <ItemStyle HorizontalAlign="right" Width="70" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIGSTValue" runat="server" Text='<%#Eval("IGST Amt")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="right" />
                                                    <FooterTemplate>
                                                        <b>
                                                            <asp:Label Font-Bold="true" ID="lblIGSTTotalValue" runat="server" Text='0'></asp:Label></b>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SGST Rate" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                    <ItemStyle HorizontalAlign="right" Width="70" />
                                                    <ItemTemplate>
                                                        <%#Eval("SGST Rate")%>%
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SGST Amount" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                    <ItemStyle HorizontalAlign="right" Width="70" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSGSTValue" runat="server" Text='<%#Eval("SGST Amt")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="right" />
                                                    <FooterTemplate>
                                                        <b>
                                                            <asp:Label Font-Bold="true" ID="lblSGSTTotalValue" runat="server" Text='0'></asp:Label></b>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CGST Rate" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                    <ItemStyle HorizontalAlign="right" Width="70" />
                                                    <ItemTemplate>
                                                        <%#Eval("CGST Rate")%>%
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CGST Amount" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                    <ItemStyle HorizontalAlign="right" Width="70" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCGSTValue" runat="server" Text='<%#Eval("CGST Amt")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="right" />
                                                    <FooterTemplate>
                                                        <b>
                                                            <asp:Label Font-Bold="true" ID="lblCGSTTotalValue" runat="server" Text='0'></asp:Label></b>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UGST Rate" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                    <ItemStyle HorizontalAlign="right" Width="70" />
                                                    <ItemTemplate>
                                                        <%#Eval("UGST Rate")%>%
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UGST Amount" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                    <ItemStyle HorizontalAlign="right" Width="70" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUGSTValue" runat="server" Text='<%#Eval("UGST Amt")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="right" />
                                                    <FooterTemplate>
                                                        <b>
                                                            <asp:Label Font-Bold="true" ID="lblUGSTTotalValue" runat="server" Text='0'></asp:Label></b>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cess Rate" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                    <ItemStyle HorizontalAlign="right" Width="70" />
                                                    <ItemTemplate>
                                                        <%#Eval("Cess Rate")%>%
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cess Amount" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                    <ItemStyle HorizontalAlign="right" Width="70" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGSTCessValue" runat="server" Text='<%#Eval("Cess Amt")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="right" />
                                                    <FooterTemplate>
                                                        <b>
                                                            <asp:Label Font-Bold="true" ID="lblGSTCessTotalValue" runat="server" Text='0'></asp:Label></b>
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
                            </table>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <%--<asp:Label ID="lblTotalRecord" runat="server" Text="T. Record(s) : 0" CssClass="control-label record-count"></asp:Label>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
    <script src="Scripts/ScrollableTablePlugin_1.0_min.js" type="text/javascript"></script>
    <script>    
        $(document).load(function () {
            $('table.dataTable tr:last-child td:last-child').each(function () {
                //alert($(this).width);
                //            $(this).css("width", $(this).width());
                //            $(this).css("float", "Left");
                //            $(this).css("display", "inline-block");
                //            var ele = $(this);
                //            ele.changeElementType('span');
                //$('.datatable-container .fix-header').append(ele);
                // $('table.dataTable').insertAfter('.datatable-container .fix-header');
            });

        });

        $.fn.changeElementType = function (newType) {
            var attrs = {};

            $.each(this[0].attributes, function (idx, attr) {
                attrs[attr.nodeName] = attr.nodeValue;
            });

            this.replaceWith(function () {
                return $("<" + newType + "/>", attrs).append($(this).contents());
            });
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('#grdMain').Scrollable({
                ScrollHeight: 300
            });
        });
    </script>
</asp:Content>
