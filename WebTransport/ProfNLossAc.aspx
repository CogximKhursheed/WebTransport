<%@ Page Title="PROFIT LOSS Report" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ProfNLossAc.aspx.cs" EnableEventValidation="false" Inherits="WebTransport.ProfNLossAc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 center-block responsive-Sale-bill-container">
                    <div class="ibox float-e-margins maximizing-form">
                        <div class="ibox-title">
                            <div class="col-sm-8">
                                <h5>PROFIT / LOSS REPORT</h5>
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
                                                                    <%--<asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Excel" ToolTip="Export to Excel" ImageUrl="~/img/Excel_Img.JPG" Style="height: 16px" Visible="false" OnClick="imgBtnExcel_Click" /></span></li>--%>


                                                                    <li class="print-report" data-name="Print Report">
                                                                        <%--<img id="imgPrint" visible="false" src="Images/print.jpeg" alt="Print" onclick="javascript:CallPrint('divPrint')" style="cursor: pointer; margin-bottom: 0px;" title="Print" runat="server" />--%>

                                                                        <asp:ImageButton id="imgPrint" runat="server" AlternateText="Print" ToolTip="Click to excel" ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px"  OnClientClick="return CallPrint('divPrint');" Visible="false" />
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
                                                <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="u-table u-table-green u-table-stripped fixed-header-table bold-last-row row-selection no-wrap-cell" GridLines="None" BorderWidth="0" ShowFooter="true" AllowPaging="True" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand" OnRowDataBound="grdMain_RowDataBound">
                                                    <PagerStyle CssClass="classPager" />
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" Position="Bottom" LastPageText="Last" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="400px" HeaderText="">
                                                            <HeaderStyle HorizontalAlign="Left" Width="400px" BackColor="#0066ff" ForeColor="White"
                                                                Font-Bold="true" />
                                                            <ItemStyle HorizontalAlign="Left" Width="400px" />
                                                            <ItemTemplate>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="200px" HeaderText="Particular">
                                                            <HeaderStyle HorizontalAlign="Left" Width="200px" BackColor="#0066ff" ForeColor="White"
                                                                Font-Bold="true" />
                                                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                            <ItemTemplate>
                                                                <%#(Convert.ToString(Eval("LTyp")) == "1" ? "&nbsp;&nbsp;&nbsp;" : (Convert.ToString(Eval("LTyp")) == "2" ? "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" : (Convert.ToString(Eval("LTyp")) == "10001" ? "&nbsp;&nbsp;&nbsp;&nbsp;" : "")))%>
                                                    &nbsp;&nbsp;<%#(string.IsNullOrEmpty(Convert.ToString(Eval("Item_Name"))) ? "" : Convert.ToString(Eval("Item_Name")))%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"
                                                            HeaderText="Amount">
                                                            <HeaderStyle HorizontalAlign="Right" Width="100px" BackColor="#0066ff" ForeColor="White"
                                                                Font-Bold="true" />
                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOpenAmnt" runat="server" Text='<%#Eval("Open_Amnt")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="18px" HeaderText="">
                                                            <HeaderStyle HorizontalAlign="Left" Width="30px" BackColor="#0066ff" ForeColor="White"
                                                                Font-Bold="true" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbtnPur" CssClass="fa fa-edit icon" runat="server" CommandArgument='<%#Eval("LH_AGRP_IDNO") %>' Visible="false" AlternateText="Edit" CommandName="cmdPurDet" ToolTip="Click to edit"></asp:LinkButton>
                                                                &nbsp;<asp:HiddenField ID="hidPurId" Value='<%#Eval("LH_AGRP_IDNO")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="200px" HeaderText="Particular">
                                                            <HeaderStyle HorizontalAlign="Left" Width="200px" BackColor="#0066ff" ForeColor="White"
                                                                Font-Bold="true" />
                                                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                            <ItemTemplate>
                                                                <%#(Convert.ToString(Eval("RTyp")) == "1" ? "&nbsp;&nbsp;&nbsp;" : (Convert.ToString(Eval("RTyp")) == "2" ? "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" : (Convert.ToString(Eval("RTyp")) == "20006" ? "&nbsp;&nbsp;&nbsp;&nbsp;" : "")))%>
                                                    &nbsp;&nbsp;<%#(string.IsNullOrEmpty(Convert.ToString(Eval("Item_Namee"))) ? "" : Convert.ToString(Eval("Item_Namee")))%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"
                                                            HeaderText="Amount">
                                                            <HeaderStyle HorizontalAlign="Right" Width="100px" BackColor="#0066ff" ForeColor="White"
                                                                Font-Bold="true" />
                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmnt" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="400px" HeaderText="">
                                                            <HeaderStyle HorizontalAlign="Left" Width="400px" BackColor="#0066ff" ForeColor="White"
                                                                Font-Bold="true" />
                                                            <ItemStyle HorizontalAlign="Left" Width="400px" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbtnSale" CssClass="fa fa-usd" runat="server" CommandArgument='<%#Eval("RH_AGRP_IDNO") %>' Visible="false" AlternateText="Edit" CommandName="cmdSaleDet" ToolTip="Open Sale Detail"></i></asp:LinkButton>

                                                                <asp:HiddenField ID="hidSaleId" Value='<%#Eval("RH_AGRP_IDNO")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="" Visible="false">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <%#Eval("LHTyp")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="" Visible="false">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <%#Eval("RHTyp")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="secondFooterClass" id="divpaging" runat="server" visible="false">
                                                    <table class="" id="tblFooterscnd" runat="server">
                                                        <tr>
                                                            <th rowspan="1" colspan="1" style="width: 149px;">
                                                                <asp:Label ID="lblcontant" runat="server"></asp:Label></th>
                                                            <th rowspan="1" colspan="1" style="width: 149px;"></th>
                                                            <th rowspan="1" colspan="1" style="width: 120px; text-align: right;">Total</th>
                                                            <th rowspan="1" colspan="1" style="width: 110px; padding-left: 60px;">
                                                                <asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label>
                                                            </th>
                                                            <th rowspan="1" colspan="1" style="width: 2px;"></th>
                                                            <th rowspan="1" colspan="1" style="width: 62px;"></th>
                                                            <th rowspan="1" colspan="1" style="width: 63px;"></th>
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
                                <asp:Label ID="lblTotalRecord" runat="server" Text="T. Record(s) : 0" CssClass="control-label record-count"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <%--Print Report Header--%>
            <div id="printheader" style="display: none;">
                <td style="text-align: center; font-size: 15px;" colspan="2">
                    <asp:Label ID="lblCompName" Style="display: block; text-align: center;" Font-Bold="true" Font-Size="Medium" runat="server" Text=""></asp:Label>
                    <asp:Label Style="font-size: 12px; display: block; text-align: center;" ID="lblCompAddress" runat="server" Text=""></asp:Label>
                    <asp:Label Style="font-size: 12px; display: block; text-align: center;" ID="lblCompPhone" runat="server" Text=""></asp:Label>
                    <asp:Label Style="font-size: 12px; display: block; text-align: center;" ID="lblPAN" runat="server" Text=""></asp:Label>
                    <asp:Label Style="font-size: 12px; display: block; text-align: center;" ID="lblGST" runat="server" Text=""></asp:Label>

                </td>
            </div>
            <%--HIDDEN FIELDS--%>
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

        function setDatecontrol() {

            $('#<%=Datefrom.ClientID %>').datepicker({
                showOn: "both",
                buttonImage: "../Images/calendar.gif",
                buttonImageOnly: true,

                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy'
            });

            $('#<%=txtDateTo.ClientID %>').datepicker({
                showOn: "both",
                buttonImage: "../Images/calendar.gif",
                buttonImageOnly: true,

                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy'
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

        function ShowPopup() {
            var obj = new Object();
            var answer = window.showModalDialog("ProfNLossAcDetail.aspx", "dialogWidth:500px;dialogHeight:500px;Center:yes");
        }
    </script>
    <script type="text/javascript" language="javascript">
        function CallPrint(strid) {
            var prtContent1 = "<table width='100%' border='0'><tr><td align='center'><strong>Company List<strong></td></tr><tr><td><div style='border-width:1px;border-color:#000;border-style:solid;'></div></td></tr> </table>";
            var prtContent = document.getElementById(strid);
            var prtHeader = document.getElementById('printheader');
            var WinPrint = window.open('', '', 'letf=0,top=0,width=400,height=400,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent1);
            WinPrint.document.write(prtHeader.innerHTML + prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            //WinPrint.close();
        }
        function ShowMessage(value) {
            alert(value);
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
