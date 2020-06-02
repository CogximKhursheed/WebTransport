<%@ Page Title="Bank Re-Conciliation" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" EnableEventValidation="false" CodeBehind="BankReConciliation.aspx.cs"
    Inherits="WebTransport.BankReConciliation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row ">
        <div class="col-lg-3">
        </div>
        <div class="col-lg-6">
            <section class="panel panel-default full_form_container part_purchase_bill_form">
                <header class="panel-heading font-bold form_heading">BANK RE-CONCILIATION  
                 <span class="view_print">
                  <div id="prints" runat="server"  visible="false">
                    <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ImageUrl="~/Images/Excel_Img.JPG"
                        OnClick="imgBtnExcel_Click" ToolTip="Export to excel" />
                    &nbsp;
                    <asp:LinkButton ID="lnkbtnPrint" runat="server" ToolTip="Click to print" OnClientClick ="return CallPrint('divPrint');"><i class="fa fa-print icon"></i></asp:LinkButton>               
                </div>
                 </span>                
                </header>
                <div class="panel-body">
                  <form class="bs-example form-horizontal">
                    <!-- first  section --> 
                    <div class="clearfix first_section">
                      <section class="panel panel-in-default">  
                        <div class="panel-body">
                        	<div class="clearfix odd_row">
                            <div class="col-sm-7" style="">
                              <label class="col-sm-4 control-label" style="width: 29%;">Date Range<span class="required-field">*</span></label>
                              <div class="col-sm-8" style="width:71%;">
                               <asp:DropDownList ID="ddlDateRange" runat="server"  AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged" TabIndex="1">
                                 </asp:DropDownList>            
                              </div>
                            </div>
                            <div class="col-sm-5" style="">
                              <label class="col-sm-5 control-label" style="width: 38%;">Bank Type<span class="required-field">*</span></label>
                              <div class="col-sm-7" style="width: 62%;">
                                <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control" 
                                      TabIndex="2" >
                               </asp:DropDownList>             
                              </div>
                            </div>
                          </div>
                        	<div class="clearfix even_row">                             
                            <div class="col-sm-7" style="">
                              <label class="col-sm-4 control-label" style="width: 29%;">DateFrom<span class="required-field">*</span></label>
                                <div class="col-sm-7" style="width: 71%;">
                                 <asp:TextBox ID="txtDateFrom" runat="server" CssClass="input-sm datepicker form-control" MaxLength="12"  TabIndex="3" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtDateFrom"
                                    SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage="Please Enter Date!"
                                    Display="Dynamic" CssClass="classValidation"></asp:RequiredFieldValidator>
                                
                                </div>
                            </div>
                            <div class="col-sm-5" style="">
                              <label class="col-sm-5 control-label" style="width: 38%;">DateTo<span class="required-field">*</span></label>
                                <div class="col-sm-7" style="width: 62%;">
                                 <asp:TextBox ID="txtDateTo" runat="server" CssClass="input-sm datepicker form-control" MaxLength="12" TabIndex="4" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDateTo"
                                    SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage="Please Enter Date!"
                                    Display="Dynamic" CssClass="classValidation"></asp:RequiredFieldValidator>
                                
                                </div>
                            </div>
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-7" style="">
                              <label class="col-sm-5 control-label" style="width: 29%;">Bank Date</label>
                              <div class="col-sm-7" style="width: 71%;">
                                <asp:DropDownList ID="ddlBankDate" runat="server" TabIndex="5" CssClass="form-control">
                                    <asp:ListItem Selected="True" Value="0">Both</asp:ListItem>
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:DropDownList>
                              </div>
                            </div>
                           	<div class="col-sm-5" style="">
                              <label class="col-sm-5 control-label" style="width: 38%;">Tran.Type</label>
                              <div class="col-sm-7" style="width: 62%;">
                                <asp:DropDownList ID="ddlTType" runat="server" CssClass="form-control" TabIndex="6">
                                    <asp:ListItem Selected="True" Value="0">Both</asp:ListItem>
                                    <asp:ListItem Value="1">Debit</asp:ListItem>
                                    <asp:ListItem Value="2">Credit</asp:ListItem>
                                </asp:DropDownList>
                              </div>
                            </div>
                          </div>
                          <div class="clearfix even_row">
                          	<div class="col-sm-4"></div>
                            <div class="col-sm-2" style="">
                             <asp:LinkButton ID="lnkbtnPreview" CssClass="btn full_width_btn btn-sm btn-primary"  TabIndex="7" runat="server" CausesValidation="true" ValidationGroup="Previw" OnClick="lnkbtnPreview_OnClick">Preview</asp:LinkButton>
                      
                            </div>
                           	<div class="col-sm-2" style="">                           
                            <asp:LinkButton ID="lnkbtnClear" CssClass="btn full_width_btn btn-sm btn-primary"  TabIndex="8" runat="server"  OnClick="lnkbtnClear_OnClick">Clear</asp:LinkButton>
                        
                           	</div>
                           	<div class="col-sm-4"></div>
                          </div>                                                
                        </div>
                      </section>                        
                    </div>
                     <!-- second  section -->	                      
                    <div class="clearfix second_right">
                      <section class="panel panel-in-default btns_without_border">
                        <div class="panel-body">
                        	<div class="clearfix odd_row">
                             <div id="DivOpeningBal" runat="server" visible="false">
                        		<div class="col-sm-4" style=""></div>
                            <div class="col-sm-8" style="">
                              <label class="col-sm-4 control-label" style="">Opening Balance</label>
                                <div class="col-sm-8" style="">
                                <asp:Label ID="lblOpeningBalance2" runat="server" CssClass="form-control" style="text-align: right;"></asp:Label>
                                 
                                </div>  
                            </div>
                            </div>
                        	</div>  

                         <div id="divPrint" style="overflow: auto;">
                            <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"
                                Width="100%" GridLines="None" ShowFooter="true"  BorderWidth="0"  OnRowCommand="grdMain_RowCommand" OnRowDataBound="grdMain_RowDataBound" TabIndex="10">
                                <RowStyle CssClass="odd" />
                                <AlternatingRowStyle CssClass="even" />  
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="center"
                                        Visible="false">
                                        <HeaderStyle HorizontalAlign="center" Width="50" BackColor="#0066ff" ForeColor="White"
                                            Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="center" Width="50" />
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>.
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="center" Width="70" BackColor="#0066ff" ForeColor="White"
                                            Font-Bold="true" />
                                        <FooterTemplate>
                                            Total
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tran.Date" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" Width="70" BackColor="#0066ff" ForeColor="White"
                                            Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Left" Width="70" />
                                        <ItemTemplate>
                                            <%#(string.IsNullOrEmpty(Convert.ToString(Eval("VCHR_DATE"))) ? "" : (Convert.ToDateTime((Eval("VCHR_DATE"))).ToString("dd-MMM-yyyy")))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Particular" HeaderStyle-Width="120" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" Width="120" BackColor="#0066ff" ForeColor="White"
                                            Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Left" Width="120" />
                                        <ItemTemplate>
                                            <%#Eval("PERTICULAR")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inst No" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" Width="50" BackColor="#0066ff" ForeColor="White"
                                            Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Left" Width="50" />
                                        <ItemTemplate>
                                            <%#Eval("INST_NO")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Debit" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="right">
                                        <HeaderStyle HorizontalAlign="right" Width="50" BackColor="#0066ff" ForeColor="White"
                                            Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="right" Width="50" />
                                        <ItemTemplate>
                                            <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Debit"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Debit")))))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblAmount1" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="right">
                                        <HeaderStyle HorizontalAlign="right" Width="50" BackColor="#0066ff" ForeColor="White"
                                            Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="right" Width="50" />
                                        <ItemTemplate>
                                            <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Credit"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Credit")))))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblAmount2" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bank Date" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                        <HeaderStyle HorizontalAlign="right" Width="70" BackColor="#0066ff" ForeColor="White"
                                            Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="right" Width="70" />
                                        <ItemTemplate>
                                            <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Bank_Date"))) ? "" : (Convert.ToDateTime(Eval("Bank_Date"))).ToString("dd-MM-yyyy") == "01-01-1900" ? "" : (Convert.ToDateTime(Eval("Bank_Date")).ToString("dd-MMM-yyyy")))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" Width="50" BackColor="#0066ff" ForeColor="White"
                                            Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Center" Width="50" />
                                        <ItemTemplate>
                                           <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("VchrDetl_Idno") %>' CommandName="cmdedit" TabIndex="11" ToolTip="Click to Edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                           <%-- <asp:ImageButton ID="imgBtnEdit" runat="server" ImageUrl="~/Images/edit_sm.png" CommandArgument='<%#Eval("VchrDetl_Idno") %>'
                                                CommandName="cmdedit" TabIndex="11" />--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>                             
                                <EmptyDataTemplate>
                                    <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                </EmptyDataTemplate>                           
                            </asp:GridView>
                        </div>

						    <div class="clearfix odd_row">
                            <div id="Calc" runat="server" visible="false">
                        		<div class="col-sm-2" style=""></div>
                            <div class="col-sm-10" style="">
                              <label class="col-sm-6 control-label" style="">Balance as Per Company Book</label>
                                <div class="col-sm-6" style="">
                                 <asp:Label ID="lblClosingBalance2" runat="server" CssClass="form-control" style="text-align: right;"></asp:Label>                                   
                                </div>  
                            </div>
                            <div class="col-sm-2" style=""></div>
                            <div class="col-sm-10" style="">
                              <label class="col-sm-6 control-label" style="">Amount Not Reflected in Bank</label>
                                <div class="col-sm-6" style="">
                                   <asp:Label ID="lblNetBalance2" runat="server" CssClass="form-control" style="text-align: right;"></asp:Label>
                                </div>
                               
                            </div>
                            <div class="col-sm-2" style=""></div>
                            <div class="col-sm-10" style="">
                              <label class="col-sm-6 control-label" style="">Balance as per Bank</label>
                                <div class="col-sm-6" style="">
                                 <asp:Label ID="lblBankBalance2" runat="server" CssClass="form-control" style="text-align: right;"></asp:Label>
                                </div>  
                            </div>

                            </div>
                        	</div> 

                        </div>
                      </section>
                    </div>                     
                                        
                  </form>
                </div>
              </section>
        </div>
        <div class="col-lg-3">
        </div>
    </div>
    <!--client start--->
    <div id="dvGrdetails" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="popform_header">
                        Instrument Date
                    </h4>
                </div>
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
									<div class="panel-body">
										<!-- First Row start -->
									<div class="clearfix odd_row">	                                
	                                <div class="col-sm-4">
	                                  <label class="col-sm-5 control-label"></label>
                                    <div class="col-sm-7">
                                        Inst. Date
                                    </div>
	                                </div>
	                                <div class="col-sm-4">
	                                  <label class="col-sm-5 control-label"></label>
                                    <div class="col-sm-7">
                                       <asp:TextBox ID="txtInstDate" runat="server" MaxLength="15" CssClass="input-sm datepicker form-control"
                                      TabIndex="11" autocomplete="off"></asp:TextBox>
                                    </div>
	                                </div>
	                                <div class="col-sm-4">
	                                 
	                                </div>
	                              </div> 
                                  <div class="clearfix fourth_right">
                        <section class="panel panel-in-default btns_without_border">                            
                          <div class="panel-body">     
                             <img id="PopupLoaderImageCity" style="display: none;" src="Images/indicator.gif"
                                  alt="Please Wait..." title="Please Wait..." />
                          </div>
                        </section>
                      </div>
	                              
								</div>
							</section>
                </div>
                <div class="modal-footer">
                    <div class="popup_footer_btn">
                        <asp:LinkButton ID="lnkbtnSave" runat="server" CssClass="btn btn-dark" OnClientClick="return CheckCustomer();"
                            CausesValidation="true" ValidationGroup="SaveInstDet" TabIndex="12" OnClick="lnkbtnSave_Click"><i class="fa fa-check"></i>Save</asp:LinkButton>
                        <div style="float: left;">
                            <span id="SpnMessageClient" style="display: none; color: Red;"></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--client end  --->
    <asp:HiddenField ID="hidacntheadid" runat="server" />
    <asp:HiddenField ID="hidmindate" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />
    <asp:HiddenField ID="hidTranDate" runat="server" />
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
    <script language="javascript" type="text/javascript">

        function HideClient() {
            $("#dvInstDet").fadeOut(300);
        }
        function ShowClient() {
            $("#dvInstDet").fadeIn(300); var Trandate = $('#<%=hidTranDate.ClientID%>').val();
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $('#<%=txtInstDate.ClientID %>').datepicker({
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
    <script type="text/javascript" language="javascript">
        function CallPrint(strid) {
            var Dlr = document.getElementById('<%= this.ddlBank.ClientID %>');
            var DlrNm = Dlr.options[Dlr.selectedIndex].text;
            if (Dlr.options[Dlr.selectedIndex].text == "--Select--") {
                DlrNm = "All Party";
            }
            else {
                DlrNm = Dlr.options[Dlr.selectedIndex].text;
            }
            var FrmDt = document.getElementById('<%= this.txtDateFrom.ClientID %>');
            var ToDt = document.getElementById('<%= this.txtDateTo.ClientID %>');
            var prtContent1 = "<table width='100%' border='0'><tr><td align='center'><strong></strong></td></tr><tr><td align='center'></td></tr><tr><td align='center'></td></tr><tr><td align='center'><strong>" + DlrNm + "<strong></td></tr><tr><td align='center'>" + FrmDt.value + " to " + ToDt.value + "</td></tr><tr><td><div style='border-width:1px;border-color:#000;border-style:solid;'></div></td></tr> </table>";
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=600,height=600,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent1);
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }

        function openModal() {
            $('#dvGrdetails').modal('show');
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