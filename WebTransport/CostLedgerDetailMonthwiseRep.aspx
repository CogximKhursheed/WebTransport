<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" EnableEventValidation = "false"
    CodeBehind="CostLedgerDetailMonthwiseRep.aspx.cs" Inherits="WebTransport.CostLedgerDetailMonthwiseRep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .linearBg
        {
            /* fallback */
            background: rgb(178,225,255); /* Old browsers */ /* IE9 SVG, needs conditional override of 'filter' to 'none' */
            background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iI2IyZTFmZiIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiM2NmI2ZmMiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
            background: -moz-linear-gradient(top,  rgba(178,225,255,1) 0%, rgba(102,182,252,1) 100%); /* FF3.6+ */
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(178,225,255,1)), color-stop(100%,rgba(102,182,252,1))); /* Chrome,Safari4+ */
            background: -webkit-linear-gradient(top,  rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* Chrome10+,Safari5.1+ */
            background: -o-linear-gradient(top,  rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* Opera 11.10+ */
            background: -ms-linear-gradient(top,  rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* IE10+ */
            background: linear-gradient(to bottom,  rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* W3C */
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#b2e1ff', endColorstr='#66b6fc',GradientType=0 ); /* IE6-8 */
        }
    </style>

    <div id="page-content">
        <div class="row ">
            <div class="col-lg-1">
            </div>
            <div class="col-lg-9">
                <section class="panel panel-default full_form_container part_purchase_bill_form">
                	<header class="panel-heading font-bold">COST BREAKUP REPORT LEDGER DETAIL REPORT&nbsp;
                	<span class="view_print">&nbsp;
                     <b><asp:Label ID="lblLedgerName" runat="server"></asp:Label>-<asp:Label ID="lblTruckNo" runat="server"></asp:Label></b>
                     &nbsp;&nbsp;&nbsp;
                   <asp:ImageButton ID="imgBtnExcel" runat="server" Height="16px" Width="16px" AlternateText="Excel"
                        ImageUrl="~/Images/CSV.png" OnClick="imgBtnExcel_Click" ToolTip="Export to excel" />
                    &nbsp;<img id="printRep" runat="server" src="Images/print.jpeg" alt="Print" onclick="javascript:CallPrint('divPrint')"
                        style="cursor: pointer;" title="Print" visible="true" />

                    </span>
               	 	</header>
                	<div class="panel-body">
                    <form class="bs-example form-horizontal">
                      <!-- first  section --> 
                      
                         <div class="clearfix fourth_right">
                        <section class="panel panel-in-default btns_without_border">                            
                          <div class="panel-body">     
                            <div id="divPrint" class="clearfix">
		                           <section class="panel panel-default full_form_container material_search_pop_form">
		                            <div class="panel-body" style="overflow:auto;">   
                                   
                                         <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"
                                            Width="100%" GridLines="None" AllowPaging="true" PageSize="50" OnRowCommand="grdMain_RowCommand" OnPageIndexChanging="grdMain_PageIndexChanging"
                                            BorderWidth="0" TabIndex="7" OnRowDataBound="grdMain_RowDataBound" ShowFooter="true">
                                             <RowStyle CssClass="odd" />
                                            <AlternatingRowStyle CssClass="even" />                                     
                                            <PagerStyle  CssClass="classPager" />
                                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="5"  FirstPageText="First" Position="Bottom" LastPageText="Last"/>
                                              <Columns>
                                                <asp:TemplateField HeaderText="Particular" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="70" 
                                                        Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="Left" Width="70" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblParticular" runat="server"></asp:Label>
                                                        <b>
                                                            <asp:LinkButton ID="lnkBtnParticular" runat="server" CssClass="link" CommandName="cmdshowdetail"></asp:LinkButton></b>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="left" Width="70" 
                                                        Font-Bold="true" />
                                                    <FooterTemplate>
                                                        Total
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Debit" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="right">
                                                    <HeaderStyle HorizontalAlign="right" Width="100" 
                                                        Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="right" Width="100" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl2Debit" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Width="200px" 
                                                        Font-Bold="true" />
                                                    <FooterTemplate>
                                                        <b>
                                                            <asp:Label ID="lbl2TDebit" runat="server"></asp:Label></b>
                                                        <b>
                                                            <asp:LinkButton ID="lnkBtn2Debit" runat="server" CssClass="link" CommandName="cmdshowdetail1D"></asp:LinkButton></b>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Credit" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="right">
                                                    <HeaderStyle HorizontalAlign="right" Width="100" 
                                                        Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="right" Width="100" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl3Credit" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Width="200px" 
                                                        Font-Bold="true" />
                                                    <FooterTemplate>
                                                        <b>
                                                            <asp:Label ID="lbl3TCredit" runat="server"></asp:Label></b>
                                                        <b>
                                                            <asp:LinkButton ID="lnkBtn2Credit" runat="server" CssClass="link" CommandName="cmdshowdetail1D"></asp:LinkButton></b>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Balance" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="right">
                                                    <HeaderStyle HorizontalAlign="right" Width="100" 
                                                        Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="right" Width="100" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl4Balance" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>

                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                            <EmptyDataTemplate>
                                            <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                            </EmptyDataTemplate>                                   
                                        </asp:GridView>
                                        <br /> 
		                            </div>
		                          </section> 
		                        </div> 
                          </div>
                        </section>
                      </div>                      
                                          
                    </form>
                </div>
              </section>
            </div>
            <div class="col-lg-2">
            </div>
        </div>
    </div>

      

            <asp:HiddenField ID="hidacntheadid" runat="server" />
            <asp:HiddenField ID="hidstr" runat="server" />
            <asp:HiddenField ID="hidmindate" runat="server" />
            <asp:HiddenField ID="hidmaxdate" runat="server" />
        
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

            var Unm = $('#<%= lblTruckNo.ClientID %>').text(); var ULnm = $('#<%= lblLedgerName.ClientID %>').text();
            var prtContent1 = "<table width='100%' border='0'><tr><td align='center'><strong></strong></td></tr><tr><td align='center'></td></tr><tr><td align='center'></td></tr><tr><td align='center'><strong>" + ULnm + "-"+Unm+ "<strong></td></tr><tr><td><div style='border-width:1px;border-color:#000;border-style:solid;'></div></td></tr> </table>";
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=600,height=600,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent1);
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
    </script>
</asp:Content>
