<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="CostLedgerVchrDetail.aspx.cs"  Inherits="WebTransport.CostLedgerVchrDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Styles/admin.css" rel="stylesheet" type="text/css" />
    <link href="Styles/style1.css" rel="stylesheet" type="text/css" />

      <div id="page-content">
        <div class="row ">
            <div class="col-lg-1">
            </div>
            <div class="col-lg-11">
                <section class="panel panel-default full_form_container part_purchase_bill_form">
                	<header class="panel-heading font-bold">TRUCK LEDGER REPORT &nbsp;
                	<span class="view_print">&nbsp;
                     <b><asp:Label ID="lblHdrNm" runat="server" Text=" Truck Ledger Report "></asp:Label> - <asp:Label ID="lblLedgerName" runat="server" Font-Bold="True"></asp:Label> - <asp:Label ID="lblTruckNo" runat="server" Font-Bold="True"></asp:Label></b>
                    <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ImageUrl="~/Images/Excel_Img.JPG"
                        OnClick="imgBtnExcel_Click" ToolTip="Export to excel" />
                    <img id="printRep" runat="server" src="Images/print.jpeg" alt="Print" onclick="javascript:CallPrint('divPrint')"
                        style="cursor: pointer;" title="Print" />

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
                                            Width="100%" GridLines="None" AllowPaging="true" PageSize="50" OnRowDataBound="grdMain_RowDataBound" OnRowCommand="grdMain_RowCommand" OnPageIndexChanging="grdMain_PageIndexChanging"
                                            BorderWidth="0" TabIndex="7" ShowFooter="true">
                                             <RowStyle CssClass="odd" />
                                            <AlternatingRowStyle CssClass="even" />                                     
                                            <PagerStyle  CssClass="classPager" />
                                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="5"  FirstPageText="First" Position="Bottom" LastPageText="Last"/>
                                            <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="60" HeaderText="Vchr Date">
                                                <HeaderStyle HorizontalAlign="Left" Width="60" 
                                                    Font-Bold="true" />
                                                <ItemStyle HorizontalAlign="Left" Width="60" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("VCHR_DATE")))?"":(Convert.ToDateTime((Eval("VCHR_DATE"))).ToString("dd-MMM-yyyy")))%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="left" Width="70" 
                                                    Font-Bold="true" />
                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Particular">
                                                <HeaderStyle HorizontalAlign="Left" Width="150" 
                                                    Font-Bold="true" />
                                                <ItemStyle HorizontalAlign="Left" Width="150" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPart" runat="server" Text='<%#Eval("PARTICULAR") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="left" Width="100" 
                                                    Font-Bold="true" />
                                                <FooterTemplate>
                                                    Total :
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Narration">
                                                <HeaderStyle HorizontalAlign="Left" Width="150" 
                                                    Font-Bold="true" />
                                                <ItemStyle HorizontalAlign="Left" Width="150" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNarr" runat="server" Text='<%#Eval("NARR_TEXT") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="left" Width="100" 
                                                    Font-Bold="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="60" HeaderText="Vchr Type">
                                                <HeaderStyle HorizontalAlign="Left" Width="60" 
                                                    Font-Bold="true" />
                                                <ItemStyle HorizontalAlign="Left" Width="60" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVchrType" runat="server" Text='<%#Eval("Vchr_type") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="left" Width="70" 
                                                    Font-Bold="true" />
                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="right" HeaderStyle-Width="60" HeaderText="Debit">
                                                <HeaderStyle HorizontalAlign="Right" Width="60" 
                                                    Font-Bold="true" />
                                                <ItemStyle HorizontalAlign="right" Width="60" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDebit" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("Debit"))) ? "" : (Convert.ToDouble(Eval("Debit"))==0.00?"0.00":(string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Debit"))))) )%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" Width="70" 
                                                    Font-Bold="true" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTDebit" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="right" HeaderStyle-Width="60" HeaderText="Credit">
                                                <HeaderStyle HorizontalAlign="Right" Width="60" 
                                                    Font-Bold="true" />
                                                <ItemStyle HorizontalAlign="right" Width="60" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCredit" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("Credit"))) ? "" : (Convert.ToDouble(Eval("Credit"))==0.00?"0.00":(string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Credit"))))) )%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" Width="70" 
                                                    Font-Bold="true" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTCredit" runat="server"></asp:Label>
                                                </FooterTemplate>
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
    
  <%--  <table border="0" cellpadding="2" cellspacing="0" class="border" width="100%">
        <table cellpadding="1" cellspacing="1" border="0" class="border1" id="tblAuthorize"
            width="100%" runat="server">
            <tr>
                <td class="white_bg " align="center">
                    <table id="tblNoAuthorize" runat="server" visible="false" class="border1">
                        <tr>
                            <td>
                                You are not authorize for this
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" id="Table1"
                        runat="server">
                        <tr>
                            <td>
                                <table width="850" align="center" cellpadding="1" cellspacing="1" border="0" width="100%">
                                    <tr>
                                        <td height="39" align="left" background="images/grd_top_bg.jpg" class="title06">
                                            <div style="float: left; width: 90%; height: 30px;">
                                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblHdrNm" runat="server" Text=" Truck Ledger Report "></asp:Label>
                                                -  <asp:Label ID="lblLedgerName" runat="server" Font-Bold="True"></asp:Label>-
                                                <asp:Label ID="lblTruckNo" runat="server" Font-Bold="True"></asp:Label>
                                            </div>
                                            <div id="prints" runat="server" align="right" style="text-align: right; width: 6%;
                                                float: right; height: 30px;" visible="true">
                                                <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ImageUrl="~/Images/Excel_Img.JPG"
                                                    OnClick="imgBtnExcel_Click" ToolTip="Export to excel" />
                                                <img id="printRep" runat="server" src="Images/print.jpeg" alt="Print" onclick="javascript:CallPrint('divPrint')"
                                                    style="cursor: pointer;" title="Print" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="white_bg border" align="left" valign="top" colspan="4">
                                            <table width="100%">
                                                <tr>
                                                    <td class="white_bg" valign="top">
                                                        <div id="divPrint" style="overflow: auto;">
                                                            <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                BorderStyle="None" CssClass="ibdr gridBackground " GridLines="None" BorderWidth="0"
                                                                RowStyle-CssClass="gridAlternateRow" AlternatingRowStyle-CssClass="gridRow" ShowFooter="true"
                                                                AllowPaging="false" HeaderStyle-CssClass="gridRow" OnRowDataBound="grdMain_RowDataBound"
                                                                OnRowCommand="grdMain_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="60" HeaderText="Vchr Date">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="60" BackColor="#0066ff" ForeColor="White"
                                                                            Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="60" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDate" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("VCHR_DATE")))?"":(Convert.ToDateTime((Eval("VCHR_DATE"))).ToString("dd-MMM-yyyy")))%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle HorizontalAlign="left" Width="70" BackColor="#0066ff" ForeColor="White"
                                                                            Font-Bold="true" />
                                                                        <FooterTemplate>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Particular">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="150" BackColor="#0066ff" ForeColor="White"
                                                                            Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPart" runat="server" Text='<%#Eval("PARTICULAR") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle HorizontalAlign="left" Width="100" BackColor="#0066ff" ForeColor="White"
                                                                            Font-Bold="true" />
                                                                        <FooterTemplate>
                                                                            Total :
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Narration">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="150" BackColor="#0066ff" ForeColor="White"
                                                                            Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblNarr" runat="server" Text='<%#Eval("NARR_TEXT") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle HorizontalAlign="left" Width="100" BackColor="#0066ff" ForeColor="White"
                                                                            Font-Bold="true" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="60" HeaderText="Vchr Type">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="60" BackColor="#0066ff" ForeColor="White"
                                                                            Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="60" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVchrType" runat="server" Text='<%#Eval("Vchr_type") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle HorizontalAlign="left" Width="70" BackColor="#0066ff" ForeColor="White"
                                                                            Font-Bold="true" />
                                                                        <FooterTemplate>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="right" HeaderStyle-Width="60" HeaderText="Debit">
                                                                        <HeaderStyle HorizontalAlign="Right" Width="60" BackColor="#0066ff" ForeColor="White"
                                                                            Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="right" Width="60" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDebit" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("Debit"))) ? "" : (Convert.ToDouble(Eval("Debit"))==0.00?"0.00":(string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Debit"))))) )%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle HorizontalAlign="right" Width="70" BackColor="#0066ff" ForeColor="White"
                                                                            Font-Bold="true" />
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblTDebit" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="right" HeaderStyle-Width="60" HeaderText="Credit">
                                                                        <HeaderStyle HorizontalAlign="Right" Width="60" BackColor="#0066ff" ForeColor="White"
                                                                            Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="right" Width="60" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCredit" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("Credit"))) ? "" : (Convert.ToDouble(Eval("Credit"))==0.00?"0.00":(string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Credit"))))) )%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle HorizontalAlign="right" Width="70" BackColor="#0066ff" ForeColor="White"
                                                                            Font-Bold="true" />
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblTCredit" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <FooterStyle />
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <%#(string.IsNullOrEmpty(Convert.ToString(Eval("VCHR_DATE")))?"":(Convert.ToDateTime((Eval("VCHR_DATE"))).ToString("dd-MMM-yyyy")))%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px;" class="white_bg">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </table>--%>
    
    <script type="text/javascript" language="javascript">
        function CallPrint(strid) {

            var Unm = $('#<%= lblTruckNo.ClientID %>').text(); var Unlm = $('#<%= lblLedgerName.ClientID %>').text();
            var prtContent1 = "<table width='100%' border='0'><tr><td align='center'><strong></strong></td></tr><tr><td align='center'></td></tr><tr><td align='center'></td></tr><tr><td align='center'><strong>" + Unlm + "-" + Unm + "<strong></td></tr><tr><td><div style='border-width:1px;border-color:#000;border-style:solid;'></div></td></tr> </table>";
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
    <script type="text/javascript" language="javascript">
        function ShowPopup(Vchr_Idno, FTyp) {
            var answer = window.showModalDialog("VchrEntry.aspx?VchrIdno=" + Vchr_Idno + "&VchrFrm=" + FTyp,
                         "unadorned:yes;resizable:1;dialogHeight:768px;dialogwidth:1366px;scroll:no;status=no");
        }
        function ShowPopup1(HId, FTyp, ACB) {
            var answer = window.showModalDialog("VATInvoice.aspx?FTyp=1&HId=" + HId + "&VchrFrm=" + FTyp + "&Acb=" + ACB,
            "unadorned:yes;resizable:1;dialogHeight:768px;dialogwidth:1366px;scroll:no;status=no");
        }
        function ShowPopup2(HId, FTyp, ACB) {
            var answer = window.showModalDialog("VATInvoice.aspx?FTyp=3&HId=" + HId + "&VchrFrm=" + FTyp + "&Acb=" + ACB,
            "unadorned:yes;resizable:1;dialogHeight:768px;dialogwidth:1366px;scroll:no;status=no");
        }
        function ShowPopup3(HId, FTyp, ACB) {
            var answer = window.showModalDialog("VATInvoice.aspx?FTyp=2&HId=" + HId + "&VchrFrm=" + FTyp + "&Acb=" + ACB,
            "unadorned:yes;resizable:1;dialogHeight:768px;dialogwidth:1366px;scroll:no;status=no");
        }
        function ShowPopup4(HId, FTyp) {
            var answer = window.showModalDialog("VATInvoiceByFactory.aspx?FTyp=2&HId=" + HId + "&VchrFrm=" + FTyp,
            "unadorned:yes;resizable:1;dialogHeight:768px;dialogwidth:1366px;scroll:no;status=no");
        }
        function ShowPopup5(HId, FTyp) {
            var answer = window.showModalDialog("PurBill.aspx?FTyp=1&HId=" + HId + "&VchrFrm=" + FTyp,
            "unadorned:yes;resizable:1;dialogHeight:768px;dialogwidth:1366px;scroll:no;status=no");
        }
        function ShowPopup6(HId, FTyp) {
            var answer = window.showModalDialog("PayRcptAdjstFrmDlr.aspx?payrecid=" + HId + "&VchrFrm=" + FTyp,
            "unadorned:yes;resizable:1;dialogHeight:768px;dialogwidth:1366px;scroll:no;status=no");
        }
        function ShowPopup7(HId, FTyp) {
            var answer = window.showModalDialog("PayRcptFrmDlr.aspx.aspx?payrecid=" + HId + "&VchrFrm=" + FTyp,
            "unadorned:yes;resizable:1;dialogHeight:768px;dialogwidth:1366px;scroll:no;status=no");
        }
    </script>
</asp:Content>
