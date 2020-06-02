<%@ Page Language="C#" Title="Sale Register" AutoEventWireup="true" MasterPageFile="~/Site1.Master"
    CodeBehind="SaleReg.aspx.cs" Inherits="WebTransport.SaleReg" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .RightAlign {
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 center-block responsive-Sale-bill-container">
                    <div class="ibox float-e-margins maximizing-form">
                        <div class="ibox-title">
                            <div class="col-sm-8">
                                <h5>SALE REGISTER</h5>
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
                                                <asp:TextBox ID="Datefrom" runat="server" CssClass="input-sm datepicker form-control" MaxLength="12" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDateFrom" runat="server" ControlToValidate="Datefrom" SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage=" Please Enter Date!" Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
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
                                                <asp:DropDownList ID="drpCityFrom" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Bill Type</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:DropDownList ID="ddlSaleType" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Credit" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Cash" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Against</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:DropDownList ID="ddlAgainst" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Counter" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Matrial Issue" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
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
                                                <span class="filter-label">Bill No.</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:TextBox ID="txtBillNo" PlaceHolder="Enter Bill No" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Vehicle No</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:TextBox ID="txtPerfNo" PlaceHolder="Enter Perf No" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
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
                                    <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="u-table u-table-green u-table-stripped fixed-header-table bold-last-row row-selection no-wrap-cell" GridLines="None" BorderWidth="0" ShowFooter="true" OnRowDataBound="grdMain_RowDataBound" OnRowCreated="grdMain_RowCreated">
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
                                            <asp:TemplateField HeaderText="Perf No." HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                                <ItemStyle HorizontalAlign="Right" Width="70" />
                                                <ItemTemplate>
                                                    <%#Eval("PrefNo")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill No." HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                                <ItemStyle HorizontalAlign="Right" Width="70" />
                                                <ItemTemplate>
                                                    <%#Eval("SbillNo")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill Date" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="80" />
                                                <ItemTemplate>
                                                    <%#Convert.ToDateTime(Eval("Date")).ToString("dd-MM-yyyy")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill Type" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="70" />
                                                <ItemTemplate>
                                                    <%#Eval("SbillType")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Against" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="Left" Width="80" />
                                                <ItemTemplate>
                                                    <%#Eval("Against")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Location" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="Left" Width="80" />
                                                <ItemTemplate>
                                                    <%#Eval("FromLocation")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Party Name" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="Left" Width="80" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartyName" runat="server" ToolTip='<%#Eval("PartyName") %>' Text='<%#(((Eval("PartyName").ToString().Length) > 25) ? Eval("PartyName").ToString().Substring(0, 25).ToString() + "..." : Eval("PartyName").ToString())%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle CssClass="RightAlign" />
                                                <FooterTemplate>
                                                    Total:
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Tax" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                <ItemStyle HorizontalAlign="right" Width="70" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("TotTaxableAmnt")))%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="lbltaxableAmnt" Font-Bold="true" runat="server"></asp:Label></b>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Tax" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                <ItemStyle HorizontalAlign="right" Width="70" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("TotTax")))%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="lbltax" Font-Bold="true" runat="server"></asp:Label></b>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Disc." HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                <ItemStyle HorizontalAlign="right" Width="70" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("DiscAmnt")))%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="lblDiscAmount" Font-Bold="true" runat="server"></asp:Label></b>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other Amnt" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                <ItemStyle HorizontalAlign="right" Width="70" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("OtherAmnt")))%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="lblOtherAmount" Font-Bold="true" runat="server"></asp:Label></b>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Net Amnt" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                <ItemStyle HorizontalAlign="right" Width="70" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("NetAmnt")))%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="lblAmount" Font-Bold="true" runat="server"></asp:Label></b>
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
            <%--HIDDEN FIELDS--%>
            <asp:HiddenField ID="hidmindate" runat="server" />
            <asp:HiddenField ID="hidmaxdate" runat="server" />
            <asp:HiddenField ID="hidrcptheadidno" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgBtnExcel" />
        </Triggers>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">
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

        function ShowModalPopup() {
            ShowDialog(true);
        }
        function ShowDialog(modal) {
            // $("#overlay").show();
            $("#dialog").show();
            $("#dialog").fadeIn(300);

            if (modal) {
                $("#dialog").unbind("click");
                //$("#overlay").unbind("click");
            }
            else {
                //  $("#overlay").click(function (e) {
                $("#dialog").click(function (e) {
                    HideDialog();
                });
            }
        }

        function HideDialog() {
            //   $("#overlay").hide();
            $("#dialog").fadeOut(300);
        }
        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $("#<%=Datefrom.ClientID %>").datepicker({
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
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
