<%@ Page Title="FREIGHT MEMO REPORT" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" EnableEventValidation="false" CodeBehind="FreightMemoReport.aspx.cs"
    Inherits="WebTransport.FreightMemoReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .linearBg {
            /* fallback */
            background: rgb(178,225,255); /* Old browsers */ /* IE9 SVG, needs conditional override of 'filter' to 'none' */
            background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iI2IyZTFmZiIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiM2NmI2ZmMiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
            background: -moz-linear-gradient(top, rgba(178,225,255,1) 0%, rgba(102,182,252,1) 100%); /* FF3.6+ */
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(178,225,255,1)), color-stop(100%,rgba(102,182,252,1))); /* Chrome,Safari4+ */
            background: -webkit-linear-gradient(top, rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* Chrome10+,Safari5.1+ */
            background: -o-linear-gradient(top, rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* Opera 11.10+ */
            background: -ms-linear-gradient(top, rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* IE10+ */
            background: linear-gradient(to bottom, rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* W3C */
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#b2e1ff', endColorstr='#66b6fc',GradientType=0 ); /* IE6-8 */
        }
    </style>

    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 center-block responsive-Sale-bill-container">
                    <div class="ibox float-e-margins maximizing-form">
                        <div class="ibox-title">
                            <div class="col-sm-8">
                                <h5>FREIGHT MEMO REPORT</h5>
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
                                                <span class="filter-label">To City</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:DropDownList ID="drpToCity" runat="server" CssClass="form-control" AutoPostBack="false"></asp:DropDownList>
                                            </div>
                                        </div>
                                        \
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
                                                <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="u-table u-table-green u-table-stripped fixed-header-table bold-last-row row-selection no-wrap-cell" GridLines="None" BorderWidth="0" ShowFooter="true" AllowPaging="true" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowDataBound="grdMain_RowDataBound">
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
                                                        <asp:TemplateField HeaderText="Date" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" Width="80" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Rcpt_Date"))) ? "" : (Convert.ToDateTime((Eval("Rcpt_Date"))).ToString("dd-MM-yyyy")))%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rcpt No." HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("Rcpt_No")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GR No." HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#Eval("Gr_No")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To City" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="80" />
                                                            <ItemTemplate>
                                                                <%#Eval("City_Name")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Receiver Name" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="80" />
                                                            <ItemTemplate>
                                                                <%#Eval("Acnt_Name")%>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                Total
                                                            </FooterTemplate>
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
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Tot_Weight"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Tot_Weight")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblWeight" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Freight Amount" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Freight_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Freight_Amnt")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblFreightAmnt" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Service Chareges" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Service_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Service_Amnt")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblServiceCharges" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Labour Amount" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Labour_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Labour_Amnt")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblLabourAmnt" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delivery Amount" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Delivery_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Delivery_Amnt")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblDelvryAmnt" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Octrai Amount" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Octrai_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Octrai_Amnt")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblOctraiAmnt" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Damage Amount" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Damage_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Damage_Amnt")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblDamage" runat="server"></asp:Label></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Net Amount" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                            <ItemStyle HorizontalAlign="right" Width="70" />
                                                            <ItemTemplate>
                                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Net_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Net_Amnt")))))%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblNetAmnt" runat="server"></asp:Label></b>
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
                                        <tr>
                                            <td>
                                                <div class="secondFooterClass" id="divpaging" runat="server" visible="false">
                                                    <table class="" id="tblFooterscnd" runat="server">
                                                        <tr>
                                                            <th rowspan="1" colspan="1" style="width: 350px;">
                                                                <asp:Label ID="lblcontant" runat="server"></asp:Label></th>
                                                            <th rowspan="1" colspan="1" style="width: 100px; text-align: left;">Net Total&nbsp;</th>
                                                            <th rowspan="1" colspan="1" style="width: 80px;"></th>
                                                            <th rowspan="1" colspan="1" style="width: 110px; text-align: right">
                                                                <asp:Label ID="lblNetTotalAmountLocGRAmnt" runat="server"></asp:Label></th>
                                                            <th rowspan="1" colspan="1" style="width: 110px;"></th>
                                                            <th rowspan="1" colspan="1" style="width: 110px; text-align: right">
                                                                <asp:Label ID="lblNetTotalAmountCrsngGRAmnt" runat="server"></asp:Label></th>
                                                            <th rowspan="1" colspan="1" style="width: 110px; text-align: right">
                                                                <asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label></th>
                                                        </tr>
                                                    </table>

                                                </div>
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
            <asp:HiddenField ID="hidmindate" runat="server" />
            <asp:HiddenField ID="hidmaxdate" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgBtnExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">
        SetFocus();
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

        $(document).ready(function () {
            setDatecontrol();
        });

        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $('#<%=txtDateFrom.ClientID %>').datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });

            $('#<%=txtDateTo.ClientID %>').datepicker({
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
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
    <script>
        //$(document).ready(function () {
        $(".datepicker").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
            minDate: '<%=hidmindate.Value%>',
            maxDate: '<%=hidmaxdate.Value%>'
        });
        //});
    </script>
</asp:Content>
