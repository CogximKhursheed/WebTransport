<%@ Page Title="Trial Balance Report" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="AccTrailBalGroup.aspx.cs" Inherits="WebTransport.AccTrailBalGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-2">
                </div>
                <div class="col-lg-8">
                    <section class="panel panel-default full_form_container trial_bal_form">
                <header class="panel-heading font-bold form_heading">TRIAL BALANCE REPORT
                  <span class="view_print">
                  <div id="prints" runat="server" visible="false">
                    <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ImageUrl="~/Images/Excel_Img.JPG"
                                                        OnClick="imgBtnExcel_Click" ToolTip="Export to excel" TabIndex="11" />
                                                    &nbsp;
                     <asp:LinkButton ID="lnkbtnPrint" runat="server" ToolTip="Click to print" Visible="false" OnClientClick ="return CallPrint('divPrint');"><i class="fa fa-print icon"></i></asp:LinkButton>
                    </div>                        
                  </span>
                </header>
                <div class="panel-body">
                  <form class="bs-example form-horizontal">
                    <!-- first  section --> 
                    
                     <!-- second  section -->	                      
                    <div class="clearfix second_right">
                      <section class="panel panel-in-default btns_without_border">                            
                        <div class="panel-body">     
                           <div  style="overflow: auto;">
                                <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"
                                    Width="100%" GridLines="None" AllowPaging="false" PageSize="25" OnPageIndexChanging="grdMain_PageIndexChanging"
                                    BorderWidth="0" ShowFooter="false" OnRowDataBound="grdMain_RowDataBound"
                                    OnRowCommand="grdMain_RowCommand" TabIndex="9">
                                     <RowStyle CssClass="odd" />
                                     <AlternatingRowStyle CssClass="even" /> 
                                    <Columns>
                                        <asp:TemplateField HeaderText="Group" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="Left" Width="150" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl5Group" runat="server"></asp:Label>
                                                <b><asp:LinkButton ID="lnkBtn5Group" runat="server" CssClass="link" CommandName="cmdshowdetailTB" CommandArgument='<%#Eval("Acnt_Idno")%>'></asp:LinkButton></b>
                                            </ItemTemplate>                                                                                        
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sub Group" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="Left" Width="150" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl6SubGroup" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Particular" HeaderStyle-Width="130" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="Left" Width="130" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl7Particular" runat="server"></asp:Label>
                                                <b><asp:LinkButton ID="lnkbtn7Particular" runat="server" CssClass="link" CommandName="cmdshowdetailTB" CommandArgument='<%#Eval("Acnt_Idno")%>' ></asp:LinkButton></b>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Op.Debit" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
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
                                            <ItemStyle HorizontalAlign="Right" Width="50" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl10DebitA" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit Amount" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Width="50" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl11CreditA" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>                                  
                                    <EmptyDataTemplate>
                                        <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                    </EmptyDataTemplate>                                  
                                </asp:GridView>
                            </div>
                            <div style="float:left;">

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


        <div class="col-lg-0" style="display: none">
            <tr style="display: none">
                <td class="white_bg" align="center">
                    <div id="divPrint" style="font-size: 13px;">
                        <table cellpadding="1" cellspacing="0" width="800" border="1" style="font-family: Arial,Helvetica,sans-serif;">
                            <tr style="height:100px;">
                                <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                                    border-left-style: none; border-right-style: none";>
                                   <div id="header1" runat="server">
                                            <strong>
                                                <asp:Label ID="lblCompanyname1" runat="server" Style="font-size: 18px;"></asp:Label><br />
                                            </strong>
                                            <asp:Label ID="Label7" runat="server" Text="(Fleet Owners & Transport Contractor)"></asp:Label><br />
                                           <strong> Head Office : <asp:Label ID="lblCompAdd3" runat="server"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblCompAdd4" runat="server"></asp:Label>
                                            <asp:Label ID="lblCompCity1" runat="server"></asp:Label>&nbsp;&nbsp;
                                            <asp:Label ID="lblCompState1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblCompCityPin1" runat="server"></asp:Label><br /></strong>
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
                                                <td style="font-size: 12px" width="20%">
                                                    <strong>Group</strong>
                                                </td>
                                                <td style="font-size: 12px" width="10%">
                                                    <strong>Sub Group</strong>
                                                </td>
                                                <td style="font-size: 12px" width="10%">
                                                    <strong>Particular</strong>
                                                </td>
                                                <td style="font-size: 12px" width="10%">
                                                    <strong>Debit Amount</strong>
                                                </td>
                                                <td style="font-size: 12px" align="left" width="10%">
                                                    <strong>Credit Amount</strong>
                                                </td>
                                                <td class="white_bg" style="font-size: 12px" width="10%">
                                                    
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="white_bg" width="10%">
                                                    <%#Eval("Group")%>
                                                </td>
                                                <td class="white_bg" width="30%">
                                                   <%#Eval("SubGroup")%>
                                                </td>
                                                <td class="white_bg" width="15%">
                                                   <%#Eval("Particulars")%>
                                                </td>
                                                <td class="white_bg" width="15%">
                                                   <%#Eval("DebitAmount")%>
                                                </td>
                                                <td class="white_bg" width="15%">
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

            <asp:HiddenField ID="hidacntheadid" runat="server" />
            <asp:HiddenField ID="hidstr" runat="server" />
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
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
  
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
            var DlrNm = Dlr.options[Dlr.selectedIndex].text;
            var FrmDt = document.getElementById('<%= this.hidmindate.ClientID %>');
            var ToDt = document.getElementById('<%= this.hidmaxdate.ClientID %>');
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
</asp:Content>