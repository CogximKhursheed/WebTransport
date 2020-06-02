<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PartyOutstandingReport.aspx.cs" Inherits="WebTransport.PartyOutstandingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 center-block responsive-Sale-bill-container">
                    <div class="ibox float-e-margins maximizing-form">
                        <div class="ibox-title">
                            <div class="col-sm-8">
                                <h5>PARTY OUSTANDING REPORT</h5>
                            </div>
                            <div class="col-sm-4">
                                <div class="title-action">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div id="view_print" runat="server">
                                                <div class="pull-right action-center">
                                                    <div class="fa fa-download"></div>
                                                    <div class="download-option-box">
                                                        <div class="download-option-container">
                                                            <ul>
                                                                <li class="download-excel" data-name="Download excel">
                                                                    <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ToolTip="Export to excel" ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px" OnClick="imgBtnExcel_Click"/>

                                                                    <li class="print-report" data-name="Print Report">
                                                                        <asp:LinkButton ID="lnkbtnPrint" runat="server" ToolTip="Click to print" OnClientClick="return CallPrint('divPrint');"><i class="fa fa-print icon"></i></asp:LinkButton>
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
                                                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="input-sm datepicker form-control" MaxLength="12" data-date-format="dd-mm-yyyy" onkeydown="return DateFormat(this, event.keyCode)"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtDateFrom" SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage=" Please Enter Date!" Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div id="ctl00_ContentPlaceHolder1_UpdatePanel3">
                                                <div class="control-holder full-width">
                                                    <span class="filter-label">Date To </span>
                                                </div>
                                                <div class="control-holder full-width">
                                                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="input-sm datepicker form-control" MaxLength="12" data-date-format="dd-mm-yyyy" onkeydown="return DateFormat(this, event.keyCode)"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDateTo" SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage=" Please Enter Date!" Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Lorry Type</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:DropDownList ID="ddllorrytyp" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddllorrytyp_SelectedIndexChanged">
                                                    <asp:ListItem Text="--Select--" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Own" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Hire" Value="1">
                                                    </asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Party</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:DropDownList ID="ddlParty" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 pull-left">
                                            <div class="control-holder full-width">
                                                <span class="filter-label">Include Op.Balance</span>
                                            </div>
                                            <div class="control-holder full-width">
                                                <asp:CheckBox ID="chkBal" runat="server" />
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
                                    <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="u-table u-table-green u-table-stripped fixed-header-table bold-last-row row-selection no-wrap-cell" GridLines="None" BorderWidth="0" ShowFooter="true" AllowPaging="false" PageSize="25" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowDataBound="grdMain_RowDataBound" OnRowCommand="grdMain_RowCommand">
                                        <PagerStyle CssClass="classPager" />
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" Position="Bottom" LastPageText="Last" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Group" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="Left" Width="80" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl5Group" runat="server"></asp:Label>
                                                    <b>
                                                        <asp:LinkButton ID="lnkBtn5Group" runat="server" CssClass="link" CommandName="cmdshowdetailTB" CommandArgument='<%#Eval("AGRP_IDNO")%>'></asp:LinkButton></b>
                                                    <asp:HiddenField ID="hidAcntIdno" runat="server"></asp:HiddenField>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Group" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="Left" Width="80" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl6SubGroup" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Particular" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="Left" Width="80" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl7Particular" runat="server"></asp:Label>
                                                    <b>
                                                        <asp:LinkButton ID="lnkbtn7Particular" runat="server" CssClass="link" CommandName="cmdshowdetailTB" CommandArgument='<%#Eval("Acnt_Idno")%>'></asp:LinkButton></b>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Op.Debit" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Right">
                                                <ItemStyle HorizontalAlign="Right" Width="80" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl8ODebit" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" Width="80" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lbl8TODebit" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Op.Credit" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Right">
                                                <ItemStyle HorizontalAlign="Right" Width="80" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl9OCredit" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" Width="80" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lbl9TOCredit" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Debit Amount" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                <ItemStyle HorizontalAlign="right" Width="70" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl10DebitA" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Credit Amount" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                                <ItemStyle HorizontalAlign="right" Width="70" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl11CreditA" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                        <EmptyDataTemplate>
                                            <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                                <div style="float: left;">
                                    <asp:CheckBox ID="chkYearOpng" Text="Display Year Opening Bal. only" AutoPostBack="true" CssClass="check" runat="server" Visible="false" OnCheckedChanged="chkYearOpng_CheckedChanged" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <%--<asp:Label ID="lblTotalRecord" runat="server" Text="T. Record(s) : 0" CssClass="control-label record-count"></asp:Label>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-0" style="display: none">
                <tr style="display: none">
                    <td class="white_bg" align="center">
                        <div id="divPrint" style="font-size: 13px;">
                            <table cellpadding="1" cellspacing="0" width="800" border="1" style="font-family: Arial,Helvetica,sans-serif;">
                                <tr style="height: 100px;">
                                    <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px; border-left-style: none; border-right-style: none">
                                        <div id="header1" runat="server">
                                            <strong>
                                                <asp:Label ID="lblCompanyname1" runat="server" Style="font-size: 18px;"></asp:Label><br />
                                            </strong>
                                            <asp:Label ID="Label7" runat="server" Text="(Fleet Owners & Transport Contractor)"></asp:Label><br />
                                            <strong>Head Office :
                                                <asp:Label ID="lblCompAdd3" runat="server"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblCompAdd4" runat="server"></asp:Label>
                                                <asp:Label ID="lblCompCity1" runat="server"></asp:Label>&nbsp;&nbsp;
                                            <asp:Label ID="lblCompState1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblCompCityPin1" runat="server"></asp:Label><br />
                                            </strong>
                                            <asp:Label ID="lblTin1" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;<asp:Label
                                                ID="lblCompTIN1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lbltxtPanNo1" runat="server" Text="PAN NO. :"></asp:Label>&nbsp;&nbsp;<asp:Label
                                                ID="lblPanNo1" runat="server"></asp:Label>
                                        </div>
                                    </td>

                                </tr>

                                <tr>
                                    <td>
                                        <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="tblPrint">
                                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                                <HeaderTemplate>
                                                    <tr>
                                                        <td id="lbGrp" runat="server" style="font-size: 12px" width="20%">
                                                            <strong>Group</strong>
                                                        </td>
                                                        <td style="font-size: 12px" width="10%" id="lbSubGrp" runat="server">
                                                            <strong>Sub Group</strong>
                                                        </td>
                                                        <td style="font-size: 12px" width="10%" id="lbPar" runat="server">
                                                            <strong>Particular</strong>
                                                        </td>
                                                        <td style="font-size: 12px" width="10%" id="lbDebit" runat="server">
                                                            <strong>Debit Amount</strong>
                                                        </td>
                                                        <td style="font-size: 12px" align="left" width="10%" id="lbCredit" runat="server">
                                                            <strong>Credit Amount</strong>
                                                        </td>
                                                        <td class="white_bg" style="font-size: 12px" width="10%"></td>
                                                    </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="white_bg" width="10%" id="lbGrp1" runat="server">
                                                            <%#Eval("Group")%>
                                                        </td>
                                                        <td class="white_bg" width="30%" id="lbSubGrp1" runat="server">
                                                            <%#Eval("SubGroup")%>
                                                        </td>
                                                        <td class="white_bg" width="15%" id="lbPar1" runat="server">
                                                            <%#Eval("Particulars")%>
                                                        </td>
                                                        <td class="white_bg" width="15%" id="lbDebit1" runat="server">
                                                            <%#Eval("DebitAmount")%>
                                                        </td>
                                                        <td class="white_bg" width="15%" id="lbCredit1" runat="server">
                                                            <%#Eval("CreditAmount")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </div>

            <%--HIDDEN FIELDS--%>
            <asp:HiddenField ID="hidacntheadid" runat="server" />
            <asp:HiddenField ID="hidstr" runat="server" />
            <asp:HiddenField ID="hidmindate" runat="server" />
            <asp:HiddenField ID="hidmaxdate" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgBtnExcel" />
            <asp:PostBackTrigger ControlID="lnkbtnPrint" />
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

            $("#<%=txtDateFrom.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy'

            });
            $("#<%=txtDateTo.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
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

        //        function ShowPopup(valDatefrom, valDateTo, valParty, valOpeningBal, valPartyName) {
        //            var answer = window.showModalDialog("AccBLadger.aspx?startdate=" + valDatefrom + "&enddate=" + valDateTo + "&party=" + valParty + "&OpeningBal=" + valOpeningBal + "&PartyName=" + valPartyName, "dialogWidth:500px;dialogHeight:500px;Center:yes");
        //        }

        //        function ShowPopupTB(valPartyID, valDatefrm, valDateTo, valYearId, valPartyName) {
        //            var answer = window.showModalDialog("AccBTrailBal.aspx?party=" + valPartyID + "&Datefrm=" + valDatefrm + "&DateTo=" + valDateTo + "&Year=" + valYearId + "&PartyName=" + valPartyName, "dialogWidth:500px;dialogHeight:500px;Center:yes");
        //        }         
    </script>
    <script type="text/javascript" language="javascript">
        function CallPrint(strid) {
            var DlrNm = Dlr.options[Dlr.selectedIndex].text;
            var FrmDt = document.getElementById('<%= this.txtDateFrom.ClientID %>');
            var ToDt = document.getElementById('<%= this.txtDateTo.ClientID %>');
            var prtContent1 = "<table width='100%' border='0'><tr><td align='center'><strong>ARPL Pvt. Ltd.</strong></td></tr><tr><td align='center'></td></tr><tr><td align='center'></td></tr><tr><td align='center'><strong>" + DlrNm + "<strong></td></tr><tr><td align='center'>" + FrmDt.value + " to " + ToDt.value + "</td></tr><tr><td><div style='border-width:1px;border-color:#000;border-style:solid;'></div></td></tr> </table>";
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=600,height=600,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent1);
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
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
    </script>
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent1 = "<table width='100%' border='0'></table>";
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'left=0,top=0,width=800,height=800,toolbar=1,scrollbars=1,status=1');
            WinPrint.document.write(prtContent1);
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
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
