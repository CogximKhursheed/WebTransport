<%@ Page Title="Account Book" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="AccountBookRpt.aspx.cs" Inherits="WebTransport.AccountBookRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .autoCompleteList {
            background-color: palegoldenrod !important;
            margin: 0px;
            z-index: 100000 !important;
            list-style: none;
            padding: 0;
        }

        .autoCompleteListItem {
            background-color: palegoldenrod !important;
            color: black !important;
            z-index: 100000 !important;
            padding: 2px 5px;
        }

        .autoCompleteSelectedListItem {
            background-color: white !important;
            color: Black !important;
            z-index: 100000 !important;
            padding: 2px 5px;
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
                                <h5>ACCOUNT BOOK</h5>
                            </div>
                            <div class="col-sm-4">
                                <div class="title-action">
                                    <asp:UpdatePanel ID="updpnl" runat="server">
                                        <ContentTemplate>
                                            <div id="view_print" runat="server">
                                                <div id="prints" runat="server" visible="false">
                                                    <div class="pull-right action-center">
                                                        <div class="fa fa-download"></div>
                                                        <div class="download-option-box">
                                                            <div class="download-option-container">
                                                                <ul>
                                                                    <li class="download-excel" data-name="Download excel">
                                                                        <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ToolTip="Export to excel" ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px" OnClick="imgBtnExcel_Click" />

                                                                        <li class="print-report" data-name="Print Report">
                                                                            <asp:LinkButton ID="lnkbtnPrint" runat="server" ToolTip="Click to print" OnClick="lnkbtnPrint_Click"><i class="fa fa-print icon"></i></asp:LinkButton>
                                                                        </li>
                                                                </ul>
                                                            </div>
                                                            <div class="close-download-box" title="Close download window"></div>
                                                        </div>
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
                                                <span class="filter-label">Party<span class="required-field">*</span></span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:HiddenField ID="hfPartNoId" runat="server" />
                                                <%--CssClass="form-control"--%>
                                                <asp:TextBox ID="txtParty" runat="server" CssClass="form-control auto-extender" onkeyup="SetContextKey()" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtParty" MinimumPrefixLength="1" UseContextKey="false" EnableCaching="true" CompletionSetCount="1" CompletionInterval="500" OnClientItemSelected="ClientItemSelected" ServiceMethod="GetPartNo"
                                                    CompletionListCssClass="autoCompleteList"
                                                    CompletionListItemCssClass="autoCompleteListItem"
                                                    CompletionListHighlightedItemCssClass="autoCompleteSelectedListItem">
                                                </asp:AutoCompleteExtender>
                                                <asp:RequiredFieldValidator ID="rfvddlParty" runat="server" ControlToValidate="txtParty"
                                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Select Party Name!" InitialValue="0"
                                                    SetFocusOnError="true" ValidationGroup="Previw"></asp:RequiredFieldValidator>

                                                <asp:CheckBox ID="chkBal" runat="server" Text="Include Op. Balance" />
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Type<span class="required-field">*</span></span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlType_OnSelectedIndexChanged">
                                                    <asp:ListItem Value="1" Text="Ledger Report"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Opening Balance"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Receivable/Payable"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label"></span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <div runat="server">
                                                    <asp:CheckBox ID="chkDayWise" Enabled="true" Text="Datewise" runat="server" />
                                                </div>
                                            </div>
                                        </div>
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
                                                <span class="filter-label">From </span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="input-sm datepicker form-control" MaxLength="12" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvdatefrom" runat="server" ControlToValidate="txtDateFrom" SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage=" Please select from date!" Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div id="ctl00_ContentPlaceHolder1_UpdatePanel3">
                                                <div class="control-holder full-width">
                                                    <span class="filter-label">To</span>
                                                </div>
                                                <div class="control-holder full-width">
                                                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="input-sm datepicker form-control" MaxLength="12" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvdateto" runat="server" ControlToValidate="txtDateTo" SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage=" Please select to date!" Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 pull-right">
                                    <asp:LinkButton ID="lnkbtnPreview" CssClass="btn btn-w-m btn-primary center-block" runat="server" OnClick="lnkbtnPreview_OnClick"><i class="fa fa-search-plus"></i>Preview</asp:LinkButton>
                                </div>
                                <div class="col-sm-12 pull-right">
                                    <asp:LinkButton ID="lnkBtnSetdate" CssClass="btn btn-w-m btn-primary center-block" runat="server" OnClick="lnkBtnSetdate_Click"><i class="fa fa-print icon"></i>Print</asp:LinkButton>
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
                                    <table cellpadding="1" cellspacing="0" width="100%" border="0">
                                        <tr id="CompanyDetails" style="display: none;">

                                            <td align="center" style="font-size: 18px; font-family: Arial,Helvetica,sans-serif;"
                                                class="white_bg" width="300px">&nbsp;<strong><asp:Label ID="lblCompName" runat="server" Text="" Font-Size="18px"></asp:Label></strong><br />
                                                &nbsp;<asp:Label ID="lblAddress" runat="server" Text="" Font-Size="14px"></asp:Label><br />
                                                &nbsp;<asp:Label ID="lblCity" runat="server" Text="" Font-Size="14px"></asp:Label>&nbsp;<asp:Label
                                                    ID="lblState" runat="server" Text="" Font-Size="14px"></asp:Label>&nbsp;<asp:Label
                                                        ID="lblpincode" runat="server" Text="" Font-Size="14px"></asp:Label><br />
                                                &nbsp;<asp:Label ID="Label1" runat="server" Text="Phone No." Font-Size="14px"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;:<asp:Label ID="lblPhone" runat="server" Text="" Font-Size="14px"></asp:Label><br />
                                            </td>
                                        </tr>
                                        <tr id="ledgerheader" style="display: none;">
                                            <td align="center" style="font-size: 18px; font-family: Arial,Helvetica,sans-serif;"
                                                class="white_bg" width="300px">
                                                <strong>
                                                    <asp:Label ID="lblReportType" runat="server" Text="" Font-Size="18px"></asp:Label></strong><br />

                                                <asp:Label ID="lblReport" runat="server" Text="" Font-Size="14px"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblDate" runat="server" Text="" Font-Size="14px"></asp:Label>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="u-table u-table-green u-table-stripped fixed-header-table bold-last-row row-selection no-wrap-cell" GridLines="None" BorderWidth="0" ShowFooter="true" AllowPaging="false" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowDataBound="grdMain_RowDataBound" OnRowCommand="grdMain_RowCommand">
                                                    <PagerStyle CssClass="classPager" />
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" Position="Bottom" LastPageText="Last" />
                                                    <Columns>
                                                        <%------For Ledger Report Start-------------%>
                                                        <asp:TemplateField HeaderText="Particular" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" Width="70" />
                                                            <ItemStyle HorizontalAlign="Left" Width="70" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblParticular" runat="server"></asp:Label>
                                                                <b>
                                                                    <asp:LinkButton ID="lnkBtnParticular" runat="server" CssClass="link" CommandName="cmdshowdetail"></asp:LinkButton></b>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="left" Width="70" />
                                                            <FooterTemplate>
                                                                <strong>Total</strong>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Debit" HeaderStyle-Width="70" HeaderStyle-CssClass="gridHeaderAlignRight">
                                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl2Debit" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Width="200px" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lbl2TDebit" runat="server" ForeColor="Black"></asp:Label></b>
                                                                <b>
                                                                    <asp:LinkButton ID="lnkBtn2Debit" runat="server" CssClass="link" CommandName="cmdshowdetail1D"></asp:LinkButton></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Credit" HeaderStyle-Width="100" HeaderStyle-CssClass="gridHeaderAlignRight">
                                                            <ItemStyle HorizontalAlign="right" Width="100" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl3Credit" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Width="200px" />
                                                            <FooterTemplate>
                                                                <b>
                                                                    <asp:Label ID="lbl3TCredit" runat="server" ForeColor="Black"></asp:Label></b>
                                                                <b>
                                                                    <asp:LinkButton ID="lnkBtn2Credit" runat="server" CssClass="link" CommandName="cmdshowdetail1D"></asp:LinkButton></b>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Balance" HeaderStyle-Width="100" HeaderStyle-CssClass="gridHeaderAlignRight">
                                                            <ItemStyle HorizontalAlign="right" Width="100" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl4Balance" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%----- For Ledger Report End --------------%>
                                                        <%----- For Trial Balance Report Start ------%>
                                                        <asp:TemplateField HeaderText="Group" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" Width="150" />
                                                            <ItemStyle HorizontalAlign="Left" Width="150" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl5Group" runat="server"></asp:Label>
                                                                <b>
                                                                    <asp:LinkButton ID="lnkBtn5Group" runat="server" CssClass="link" CommandName="cmdshowdetailTB"></asp:LinkButton></b>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sub Group" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" Width="150" />
                                                            <ItemStyle HorizontalAlign="Left" Width="150" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl6SubGroup" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Particular" HeaderStyle-Width="130" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" Width="130" />
                                                            <ItemStyle HorizontalAlign="Left" Width="130" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl7Particular" runat="server"></asp:Label>
                                                                <b>
                                                                    <asp:LinkButton ID="lnkbtn7Particular" runat="server" CssClass="link" CommandName="cmdshowdetailTB"
                                                                        CommandArgument='<%#Eval("Acnt_Idno")%>'></asp:LinkButton></b>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Op.Debit" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                                            <HeaderStyle HorizontalAlign="Right" Width="50" />
                                                            <ItemStyle HorizontalAlign="Right" Width="50" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl8ODebit" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Width="200px" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lbl8TODebit" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Op.Credit" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                                            <HeaderStyle HorizontalAlign="Right" Width="50" />
                                                            <ItemStyle HorizontalAlign="Right" Width="50" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl9OCredit" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Width="200px" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lbl9TOCredit" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Debit Amount" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                                            <HeaderStyle HorizontalAlign="Right" Width="50" />
                                                            <ItemStyle HorizontalAlign="Right" Width="50" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl10DebitA" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Width="200px" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lbl10TDebitA" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Credit Amount" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                                            <HeaderStyle HorizontalAlign="Right" Width="50" />
                                                            <ItemStyle HorizontalAlign="Right" Width="50" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl11CreditA" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Width="200px" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lbl11TCreditA" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <%----- For Trial Balance Report End --------%>
                                                        <asp:TemplateField HeaderText="Opening Bal" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                                            <HeaderStyle HorizontalAlign="Right" Width="100" />
                                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl12OpN" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Debit" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                                            <HeaderStyle HorizontalAlign="Right" Width="100" />
                                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl13Dbt" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Credit" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                                            <HeaderStyle HorizontalAlign="Right" Width="100" />
                                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl14Crd" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Balance" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                                            <HeaderStyle HorizontalAlign="Right" Width="100" />
                                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl15Bal" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%----- For Opening Balance Report Start ----%>
                                                        <asp:TemplateField HeaderText="Particulars" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="left">
                                                            <HeaderStyle HorizontalAlign="left" Width="100" />
                                                            <ItemStyle HorizontalAlign="left" Width="100" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl16Ledger" runat="server"></asp:Label>
                                                                <b>
                                                                    <asp:LinkButton ID="lnkbtn16Ledger" runat="server" CssClass="link" CommandName="cmdshowdetailOB"
                                                                        CommandArgument='<%#Eval("Acnt_Idno")%>'></asp:LinkButton></b>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ledger Head" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="left">
                                                            <HeaderStyle HorizontalAlign="left" Width="100" />
                                                            <ItemStyle HorizontalAlign="left" Width="100" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl17LH" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Debit" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                                            <HeaderStyle HorizontalAlign="Right" Width="100" />
                                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl18Dbt" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Credit" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                                            <HeaderStyle HorizontalAlign="Right" Width="100" />
                                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl19Crd" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%---- For Opening Balance Report End -------%>
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
                                                            <th rowspan="1" colspan="1" style="width: 180px;">
                                                                <asp:Label ID="lblcontant" runat="server"></asp:Label></th>
                                                            <th rowspan="1" colspan="1" style="width: 326px;"></th>
                                                            <th rowspan="1" colspan="1" style="width: 100px; text-align: left;">
                                                                <asp:Label ID="lblDebitAmount" runat="server"></asp:Label></th>
                                                            <th rowspan="1" colspan="1" style="width: 281px; text-align: right;">
                                                                <asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label>
                                                            </th>
                                                            <th rowspan="1" colspan="1" style="width: 2px;"></th>
                                                        </tr>
                                                        </tfoot>
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
                                <%--<asp:Label ID="lblTotalRecord" runat="server" Text="T. Record(s) : 0" CssClass="control-label record-count"></asp:Label>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--HIDDEN FIELDS--%>
            <asp:HiddenField ID="hidacntheadid" runat="server" />
            <asp:HiddenField ID="hidstr" runat="server" />
            <asp:HiddenField ID="hidmindate" runat="server" />
            <asp:HiddenField ID="hidmaxdate" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgBtnExcel" />
            <%--<asp:PostBackTrigger ControlID="lnkbtnPrint" />--%>
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

        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $('#<%=txtDateFrom.ClientID %>').datepicker({
                buttonImageOnly: false,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });

            $('#<%=txtDateTo.ClientID %>').datepicker({
                buttonImageOnly: false,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
        }

        function CheckBoxListSelect(cbControl, state) {
            var chkBoxList = document.getElementById(cbControl);
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }

            return false;
        }


        function ShowMessage(value) {
            alert(value);
        }

        function ShowPopup(valDatefrom, valDateTo, valParty, valOpeningBal, valPartyName) {
            //alert(valDatefrom+'  '+valDateTo+'  '+valParty);
            var answer = window.showModalDialog("AccBLadger.aspx?startdate=" + valDatefrom + "&enddate=" + valDateTo + "&party=" + valParty + "&OpeningBal=" + valOpeningBal + "&PartyName=" + valPartyName, "dialogWidth:500px;dialogHeight:500px;Center:yes");
        }

        function ShowPopupTB(valPartyID, valDatefrm, valDateTo, valYearId, valPartyName) {
            //var done = window.open("AccBTrailBal.aspx", "status = 1, top=0, height = 600, width = 800, resizable = 0")
            var answer = window.showModalDialog("AccBTrailBal.aspx?party=" + valPartyID + "&Datefrm=" + valDatefrm + "&DateTo=" + valDateTo + "&Year=" + valYearId + "&PartyName=" + valPartyName, "dialogWidth:500px;dialogHeight:500px;Center:yes");
        }
    </script>
    <script type="text/javascript" language="javascript">
        function CallPrint(strid) {
            var Dlr = document.getElementById('<%= this.txtParty.ClientID %>');
            var DlrNm = Dlr.options[Dlr.selectedIndex].text;
            var a = document.getElementById('<%= this.ddlType.ClientID %>');
            var a2 = a.options[a.selectedIndex].text;
            if (Dlr.options[Dlr.selectedIndex].text == "--Select--") {
                DlrNm = "All Party";
            }
            else {
                DlrNm = Dlr.options[Dlr.selectedIndex].text;
            }
            var FrmDt = document.getElementById('<%= this.txtDateFrom.ClientID %>');
            var ToDt = document.getElementById('<%= this.txtDateTo.ClientID %>');
            var prtContent1 = "<table width='100%' border='0' ></table>";

            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=600,height=600,toolbar=0,scrollbars=0,status=0');
            $("#CompanyDetails").show();
            $("#ledgerheader").show();
            WinPrint.document.write(prtContent1);
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            $("#CompanyDetails").hide();
            $("#ledgerheader").hide();
        }

        var isShift = false;
        var seperator = "-";
        function DateFormat(txt, keyCode) {
            if (keyCode == 16)
                isShift = true;
            //Validate that its Numeric
            if (((keyCode >= 48 && keyCode <= 57) || keyCode == 8 ||
                keyCode <= 37 || keyCode <= 39 ||
                (keyCode >= 96 && keyCode <= 105)) && isShift == false) {
                if ((txt.value.length == 2 || txt.value.length == 5) && keyCode != 8) {
                    txt.value += seperator;
                }
                return true;
            }
            else {
                return false;
            }
        }
        function ShowDate() {
            $('#Date_popup').modal('show');
        }
    </script>
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
    <script type="text/javascript">
        function ClientItemSelected(sender, e) {
            $get("<%=hfPartNoId.ClientID %>").value = e.get_value();
        }
    </script>
</asp:Content>
