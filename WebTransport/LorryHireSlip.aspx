<%@ Page Title="LorryHireSlip " Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master"
    EnableEventValidation="false" CodeBehind="LorryHireSlip.aspx.cs" Inherits="WebTransport.LorryHireSlip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function () {

            setDatecontrol();
        });

        prm.add_endRequest(function () {

            setDatecontrol();
        });

        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $('#<%=txtslipdate.ClientID %>').datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
        }
        function HideBillAgainst() {
            $("#dvGrdetails").fadeOut(300);
        }

        function ShowClient() {
            $("#dvGrdetails").fadeIn(300);
        }
        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                return false;
            }
            else {
                return true;
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent1 = "<table width='100%' border='0'></table>";
            var prtContent = document.getElementById(strid);

            var WinPrint = window.open('', '', 'letf=0,top=0,width=800,height=800,toolbar=1,scrollbars=1,status=1');
            WinPrint.document.write(prtContent1);
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            return false;
        }

    </script>
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-10">
            <section class="panel panel-default full_form_container part_purchase_bill_form">
	                 <header class="panel-heading font-bold form_heading">Lorry HireSlip
                     
                      <span class="view_print"><a href="ManageLorryHire.aspx">
                <asp:Label ID="lblViewList" runat="server" Text="View List&nbsp;&nbsp;" 
                    TabIndex="35"></asp:Label></a><asp:LinkButton ID="lnkbtnPrint" runat="server" ToolTip="Click to print" Visible="true" OnClientClick ="return CallPrint('print');"><i class="fa fa-print icon"></i></asp:LinkButton></span>
                     
	                  </header>
	                  <div class="panel-body">
	                    <form class="bs-example form-horizontal">
	                      <!-- first  section --> 

	                      <div class="clearfix first_section">
	                        <section class="panel panel-in-default">  
	                          <div class="panel-body">
                              <div class="clearfix odd_row">
                            <div class="col-sm-5">
                              <label class="col-sm-4 control-label" style="width: 30%;">Date Range<span class="required-field">*</span></label>
                              <div class="col-sm-8" style="width: 60%;">
                               <asp:DropDownList ID="ddlDateRange" runat="server" TabIndex="1" 
                                      CssClass="form-control" AutoPostBack="True" 
                                      onselectedindexchanged="ddlDateRange_SelectedIndexChanged">
                                 </asp:DropDownList>     
                                  <asp:RequiredFieldValidator ID="rfvdaterange" runat="server" Display="Dynamic"
                                    ControlToValidate="ddlDateRange" ValidationGroup="save" ErrorMessage="Please select date range!"
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>   
                              </div>
                            </div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 35%;">Date/SlipNo</label>
                              <div class="col-sm-4" style="width: 35%;">
                                <asp:TextBox ID="txtslipdate" runat="server" 
                                      CssClass="input-sm datepicker form-control" TabIndex="2"  oncopy="return false" 
                                      oncut="return false" onDrop="blur();return false;" onpaste="return false" 
                                      data-date-format="dd-mm-yyyy" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                    ControlToValidate="txtslipdate" ValidationGroup="save" ErrorMessage="Please select Slip date!" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                                 </div>
                              <div class="col-sm-3" style="width: 30%;">
                               <asp:TextBox ID="txtslipno" runat="server"  CssClass="form-control" Style="text-align: right;"  TabIndex="3"  ReadOnly="true"  MaxLength="15"></asp:TextBox>
                              </div>
                           	</div>
                           	<div class="col-sm-3">
                              <label class="col-sm-3 control-label" style="width: 37%;">Loc.[From]<span class="required-field">*</span></label>
							     <div class="col-sm-4" style="width: 63%;">
                                  <asp:DropDownList ID="ddlFromCity" TabIndex="4" runat="server" 
                                         CssClass="form-control" AutoPostBack="true" 
                                         onselectedindexchanged="ddlFromCity_SelectedIndexChanged">
                                </asp:DropDownList>
                                                                          
                                <asp:RequiredFieldValidator ID="rfvtxtfromcity" runat="server" Display="Dynamic"
                                    ControlToValidate="ddlFromCity" ValidationGroup="save" ErrorMessage="Please select location!"
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              
                              </div>
                             
                            </div>
                          </div>
                          <div class="clearfix even_row">
                                     <div class="col-sm-5">
                              <label class="col-sm-4 control-label" style="width: 30%;">Lorry No<span class="required-field">*</span></label>
                              <div class="col-sm-8" style="width: 60%;">
                              <div class="col-sm-11">
                              <asp:DropDownList ID="ddllorryno" runat="server" TabIndex="5" 
                                      CssClass="form-control" AutoPostBack="True" width="190px"
                                      onselectedindexchanged="ddllorryno_SelectedIndexChanged">
                                 </asp:DropDownList>     
                                  <asp:RequiredFieldValidator ID="rfvchlndetl" runat="server" Display="Dynamic"
                                    ControlToValidate="ddllorryno" ValidationGroup="save" ErrorMessage="Please select Lorry No"
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator> 
                              </div>
                               <div class="col-sm-1">
                               <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/Images/PckLst.png" AlternateText="Search"
                                                        ImageAlign="ABSMiddle" ToolTip="Search" Style="height: 28px" OnClick="imgSearch_Click"
                                                        TabIndex="5" /> 
                               </div>
                               </div>
                            </div>
                           
                            <div class="col-sm-5">
                           		<label class="col-sm-3 control-label" style="width: 29%;">Supplied By<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 71%;">
                              <asp:TextBox ID="txtsupliedby" runat="server" CssClass="form-control" 
                                      MaxLength="50" oncopy="return false" oncut="return false" 
                                      onDrop="blur();return false;" onpaste="return false"
                                TabIndex="6" ReadOnly="true"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvsupplyby" runat="server" ControlToValidate="txtsupliedby"
                                CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Supplier details "  SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
   
                              </div>
                           	</div>
                           
                                  </div>
                                  <div class="clearfix odd_row">
                                  <label class="col-sm-3 control-label" style="width: 12%;">Remark<span class="required-field"></span></label>
                                <div class="col-sm-9">
                                   <asp:TextBox ID="txtremark" placeholder="Enter Remark" runat="server" 
                                        CssClass="form-control"  MaxLength="200" oncopy="return false" 
                                        oncut="return false" onDrop="blur();return false;"
                                     onpaste="return false" Style="resize: none;" TabIndex="7" Text="" 
                                        ></asp:TextBox>
                                </div>
                                  </div>
	                          </div>
	                        </section>                        
	                      </div>
                          <!-- Second section -->
	                      <div id="dvGrdetails" class="modal fade" >
							<div class="modal-dialog">
							<div class="modal-content">
								<div class="modal-header">
								<h4 class="popform_header">GR Detail </h4>
								</div>
								<div class="modal-body">
								<section class="panel panel-default full_form_container material_search_pop_form">
									<div class="panel-body">
										<!-- First Row start -->
									<div class="clearfix odd_row">	                                
	                                <div class="col-sm-4">
	                                  <label class="col-sm-5 control-label">Date From</label>
                                    <div class="col-sm-7">
                                     <asp:TextBox ID="txtDateFrom" runat="server" CssClass="input-sm datepicker form-control"  TabIndex="85" data-date-format="dd-mm-yyyy"></asp:TextBox>                                     
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtFrm" runat="server" ErrorMessage="Enter From Date!"
                                            Display="Dynamic" CssClass="classValidation" ControlToValidate="txtDateFrom" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                     
                                    </div>
	                                </div>
	                                <div class="col-sm-4">
	                                  <label class="col-sm-5 control-label">Date To</label>
                                    <div class="col-sm-7">
                                       <asp:TextBox ID="txtDateTo" runat="server" CssClass="input-sm datepicker form-control" TabIndex="86" data-date-format="dd-mm-yyyy"></asp:TextBox>                                     
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtTo" runat="server" ErrorMessage="Enter To Date!"  Display="Dynamic" CssClass="classValidation" ControlToValidate="txtDateTo" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                     
                                    </div>
	                                </div>
	                                <div class="col-sm-4">
	                                  <div class="col-sm-6 prev_fetch">                                      
                                        <asp:LinkButton ID="lnkbtnSearch" runat="server" CssClass="btn full_width_btn btn-sm btn-primary" CausesValidation="true" TabIndex="89" ValidationGroup="RcptEntrySrch" OnClick="lnkbtnSearch_OnClick"><i class="fa fa-search"></i>Search</asp:LinkButton>	                                
	                                  </div>
	                                  <div class="col-sm-6"> 
	                                     <label class="control-label">T. Record(s) : </label>
	                                  </div>
	                                  
	                                </div>
	                              </div> 

                                  <div class="clearfix fourth_right">
                        <section class="panel panel-in-default btns_without_border">                            
                          <div class="panel-body">     
                            <div class="clearfix">
		                          <section class="panel panel-default full_form_container material_search_pop_form">
		                            <div class="panel-body">
                                      <asp:GridView ID="grdGrdetals" runat="server" GridLines="None" AutoGenerateColumns="false"
                                            Width="100%" BorderStyle="None" CssClass="display wrap dataTable"
                                            BorderWidth="0" >
                                           <RowStyle CssClass="odd" />
                                        <AlternatingRowStyle CssClass="even" />  
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select" HeaderStyle-Width="40px">
                                                     <HeaderStyle Width="40" CssClass="gridHeaderAlignCenter" />
                                                <ItemStyle Width="40" CssClass="gridHeaderAlignCenter" />
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAllCheckboxes(this);" TabIndex="90"
                                                            CssClass="SACatA" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkId" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gr No." HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("Gr_No"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gr Date" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MMM-yyyy")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Receiver Name" HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOdrNo" runat="server" Text=' <%#Eval("Recvr_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To City" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="180px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Eval("To_City")%>
                                                        <asp:HiddenField ID="hidGrIdno" runat="server" Value='<%#Eval("Gr_Idno")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Via City" HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Right">
                                                    <ItemStyle Width="20px" HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                           <%#Eval("CityVia_Name")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                Records(s) not found.
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                       
                                      
		                            </div>
		                          </section> 
		                        </div> 
                          </div>
                        </section>
                      </div>
	                              
								</div>
							</section>
							</div>
							<div class="modal-footer">
							<div class="popup_footer_btn"> 
                                <asp:LinkButton ID="lnkbtnSubmit" runat="server" CssClass="btn btn-dark"  TabIndex="91" OnClick="lnkbtnSubmit_OnClick"><i class="fa fa-check"></i>Ok</asp:LinkButton>
								<asp:LinkButton ID="lnkbtnCloase" runat="server" CssClass="btn btn-dark"  TabIndex="92" OnClick="lnkbtnCloase_Click"><i class="fa fa-times"></i>Close</asp:LinkButton>

						        <div style="float:left;">                              
                                    <asp:Label ID="Label3" runat="server" CssClass="redfont"></asp:Label>
                                </div>
							</div>
							</div>
						</div>
						</div>
					</div>
                          <div class="clearfix third_right">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body" style="   overflow-x: auto;">     
                         <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="display nowrap dataTable"
                                BorderStyle="Solid"   GridLines="Both" BorderWidth="1"   ShowFooter="true" OnDataBound="grdMain_DataBound" OnRowDataBound="grdMain_RowDataBound">
                               <RowStyle CssClass="odd" />
                                    <AlternatingRowStyle CssClass="even" />   
                                <Columns>
                                    <asp:TemplateField HeaderText="GR No." HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="100" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrNo" runat="server" Text='<%#Convert.ToString(Eval("Gr_No")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GR Date" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="50" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrdate" runat="server" Text='<%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MM-yyyy") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sender Name" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("Sender_Name"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Receiver Name" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("Recvr_Name"))%> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From City" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                           <%#Convert.ToString(Eval("To_City"))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To City" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("To_City"))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" Text="Total " runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("Qty"))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotQty" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weight" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <%#Convert.ToDouble(Eval("Tot_Weght")).ToString()%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotWeigh" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GR Amount" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#Convert.ToDouble(Eval("SubTot_Amnt")).ToString("N2")%>' ID="lblSubTotAmnt"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                        <asp:Label ID="lblNetAmnt" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblnorecord" runat="server" Text="No record found"></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>

                        </div>
                      </section>
                    </div>
                            <!-- Third  section -->
                             <div class="clearfix second_section">
                           <section class="panel panel-in-default">  
                          <div class="panel-body">
                               <div class="clearfix even_row">
                                <div class="col-sm-3">
                           		<label class="col-sm-6 control-label" >Total Freight<span class="required-field">*</span></label>
                              <div class="col-sm-5">
                               <asp:TextBox ID="txtfreight" runat="server"  
                                      CssClass="form-control" TabIndex="8"  
                                    onKeyPress="return checkfloat(event, this);"   MaxLength="12" Style="text-align: right;" Text="0.00" AutoPostBack="True" ontextchanged="txtfreight_TextChanged"
                                    ></asp:TextBox>
                             </div>
                           	   </div>
                               <div class="col-sm-3">
                               <label class="col-sm-7 control-label" >Other Charges<span class="required-field"></span></label>
                              <div class="col-sm-5" >
                               <asp:TextBox ID="txtothercharges" runat="server"  
                                      CssClass="form-control" TabIndex="8"  
                                    onKeyPress="return checkfloat(event, this);"   MaxLength="8" 
                                      Style="text-align: right;" Text="0.00" AutoPostBack="True" ontextchanged="txtothercharges_TextChanged" 
                                    ></asp:TextBox>
                             </div>
                               </div>
                               <div class="col-sm-3">
                               <label class="col-sm-6 control-label" >Unloadings<span class="required-field"></span></label>
                              <div class="col-sm-5" >
                               <asp:TextBox ID="txtUnloading" runat="server"  
                                      CssClass="form-control" TabIndex="8"  
                                    onKeyPress="return checkfloat(event, this);"   MaxLength="8" 
                                      Style="text-align: right;" Text="0.00" AutoPostBack="True" ontextchanged="txtUnloading_TextChanged"
                                    ></asp:TextBox>
                             </div>
                               </div>
                               <div class="col-sm-3">
                               <label class="col-sm-8 control-label" >Detection Charges<span class="required-field"></span></label>
                              <div class="col-sm-4" >
                               <asp:TextBox ID="txtDetectionCharges" runat="server" CssClass="form-control" 
                                      TabIndex="8" MaxLength="8" Style="text-align: right;" Text="0.00" AutoPostBack="True"
                                      ontextchanged="txtDetectionCharges_TextChanged" ></asp:TextBox>
                             </div></div>
                              </div>
                               <div class="clearfix even_row">
	                               <div class="col-sm-3">
                           		<label class="col-sm-6 control-label" >Advance</label>
                              <div class="col-sm-5">
                            <asp:TextBox ID="txtadvance" runat="server"  Text="0.00" 
                                   onKeyPress="return checkfloat(event, this);"    CssClass="form-control" TabIndex="9"  
                                      MaxLength="12" Style="text-align: right;" AutoPostBack="True" 
                                      ontextchanged="txtadvance_TextChanged" ></asp:TextBox>
                                     
                              </div>
                           	</div>	
                                   <div class="col-sm-3">
                                   <label class="col-sm-7 control-label" >Diesel</label>
                              <div class="col-sm-5">
                            <asp:TextBox ID="txtDiesel" runat="server"  Text="0.00" 
                                   onKeyPress="return checkfloat(event, this);"    CssClass="form-control" TabIndex="9"  
                                      MaxLength="12" Style="text-align: right;" AutoPostBack="True" 
                                      ontextchanged="txtDiesel_TextChanged"  ></asp:TextBox></div>
                                      </div>
                                   <div class="col-sm-3">
                                   <label class="col-sm-6 control-label" >TDS</label>
                              <div class="col-sm-5">
                            <asp:TextBox ID="txtTds" runat="server"  Text="0.00" 
                                   onKeyPress="return checkfloat(event, this);"    CssClass="form-control" TabIndex="9"  
                                      MaxLength="8" Style="text-align: right;" AutoPostBack="True" 
                                      ontextchanged="txtTds_TextChanged"></asp:TextBox></div>
                                      </div>
                                  <div class="col-sm-3">
                           		<label class="col-sm-6 control-label" style="width: 45%;">Net Amount</label>
                              <div class="col-sm-5" style="width: 55%;">
                             <asp:TextBox ID="txtnetamnt" runat="server"  Enabled="false" Text="0.00" 
                                     onKeyPress="return checkfloat(event, this);"  CssClass="form-control" TabIndex="10"  MaxLength="8" 
                                      Style="text-align: right;" ></asp:TextBox>	                               
                           </div>
                           </div>			                              
	                            </div>
	                      </div>
                          </section>
                          </div>
                           <!-- fourth row -->
	                      <div class="clearfix fourth_right">
	                        <section class="panel panel-in-default btns_without_border">                            
	                          <div class="panel-body">     
	                            <div class="clearfix odd_row">
	                              <div class="col-lg-2"></div>
	                              <div class="col-lg-8">   
                                   <div class="col-sm-4">
                                           <asp:LinkButton runat="server" ID="lnkBtnNew" 
                                             CssClass="btn full_width_btn btn-s-md btn-info"  TabIndex="13" 
                                               onclick="lnkBtnNew_Click"><i class="fa fa-file-o"></i>New</asp:LinkButton>
                                                  
                                             </div>
	                                <div class="col-sm-4">
                                     <asp:LinkButton ID="lnkbtnSave" runat="server" 
                                            CssClass="btn full_width_btn btn-s-md btn-success" TabIndex="11" 
                                            CausesValidation="true" ValidationGroup="save" onclick="lnkbtnSave_Click"> <i class="fa fa-save"></i>Save</asp:LinkButton>
	                               <asp:HiddenField ID="hidrateid" runat="server" Value="0" />
                                       <asp:HiddenField ID="hidlorryhireidno" runat="server" Value="" />
                                    <asp:HiddenField ID="Hidrowid" runat="server" Value="" />
                                    <asp:HiddenField ID="HidInvoiceTyp" runat="server" />
                                    <asp:HiddenField ID="dmindate" runat="server" />
                                    <asp:HiddenField ID="hidmindate" runat="server" />
                                        <asp:HiddenField ID="hidmaxdate" runat="server" />
                                      

	                                </div>
	                                <div class="col-sm-4">
                                     <asp:LinkButton ID="lnkbtnCancel" runat="server" 
                                            CssClass="btn full_width_btn btn-s-md btn-danger" TabIndex="12" onclick="lnkbtnCancel_Click" 
                                            ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
	                                </div>
	                              </div>
	                              <div class="col-lg-3"></div>
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
        <table width="100%">
            <tr style="display: none">
                <td class="white_bg" align="center">
                    <div id="print" >
                        <table cellpadding="1" cellspacing="0" width="800px"  style="font-family: Arial,Helvetica,sans-serif; height: 80%">
                            <tr>
                                <td colspan="4">
                                <table cellpadding="1" cellspacing="0" width="800px" >
                                 <tr>
                                 <td style="width:30%"></td>
                                 <td style="text-align:Center"><strong>
                                            <asp:Label ID="lblCompanyname" runat="server" Style="font-size: 14px;"></asp:Label><br />
                                        </strong>
                                        <asp:Label ID="lblCompAdd1" runat="server" Style="font-size: 12px;"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblCompAdd2" runat="server" Style="font-size: 12px;"></asp:Label><br />
                                        &nbsp;<asp:Label ID="lblCompCity" runat="server" Style="font-size: 12px;"></asp:Label>&nbsp;&nbsp;
                                        <asp:Label ID="lblCompState" runat="server" Style="font-size: 12px;"></asp:Label>&nbsp;&nbsp;
                                        <asp:Label ID="lblCompCityPin" runat="server" Style="font-size: 12px;"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblCompPhNo" runat="server" Style="font-size: 12px;"></asp:Label>
                                 </td>
                                 <td style="text-align:right;  "  >
                                 <asp:Label Id="Label1" runat="server" Text="SlipDate:" Font-Bold="true" Style="font-size: 12px;"  ></asp:Label>
                                 <br />
                                 <asp:Label Id="Label2"  runat="server" Text="SlipNo    :" Font-Bold="true" Style="font-size: 12px;"></asp:Label>
                                 </td>
                                 <td  style=" text-align:right">
                                 <asp:Label Id="lblDate" runat="server" Style="font-size: 12px;"></asp:Label>
                                 <br />
                                 <asp:Label Id="lblSlipNo"  runat="server" Style="font-size: 12px;"></asp:Label>
                                     </td>
                                 </tr>
                                   
                            </table>
                                
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table  cellspacing="0"   border="1" style="font-size: 12px" width="100%" id="Table1">
                                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                            <HeaderTemplate>
                                                <tr style="border-top:1px;border-bottom:1px">
                                                    <td class="white_bg" style="font-size: 12px; border-bottom:1px "   width="3%">
                                                        <strong>S.No.</strong>
                                                    </td>
                                                    <td class="white_bg" style="font-size: 12px;  " width="6%">
                                                        <strong>G.R.NO</strong>
                                                    </td>
                                                    <td style="font-size: 12px; " width="6%">
                                                        <strong>Date</strong>
                                                    </td>
                                                    <td style="font-size: 12px; " width="7%" align="center">
                                                        <strong>Lorry No</strong>
                                                    </td>
                                                    <td style="font-size: 12px; " width="5%">
                                                        <strong>From City</strong>
                                                    </td>
                                                    <td style="font-size: 12px; " align="center" width="6%">
                                                        <strong>To City</strong>
                                                    </td>
                                                    <td style="font-size: 12px; " align="center" width="6%">
                                                        <strong>Pkg</strong>
                                                    </td>
                                                     <td style="font-size: 12px; " align="center" width="6%">
                                                        <strong>Weight</strong>
                                                    </td>
                                                    <%--<td style="font-size: 12px" align="left" width="7%">
                                                        <strong>Lorry Hire settled@</strong>
                                                    </td>
                                                    <td style="font-size: 12px" width="8%">
                                                        <strong>Per M.T.for</strong>
                                                    </td>--%>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="white_bg" width="3%">
                                                        <%#Container.ItemIndex+1 %>.
                                                    </td>
                                                    <td class="white_bg" width="6%">
                                                        <%#Eval("Gr_No")%>
                                                    </td>
                                                    <td class="white_bg" width="6%">
                                                        <%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MM-yyyy")%>
                                                    </td>
                                                    <td class="white_bg" width="7%">
                                                        <%#Eval("Lorry_No")%>
                                                    </td>
                                                    <td class="white_bg" width="6%">
                                                        <%#Eval("FROM_CITY")%>
                                                    </td>
                                                    <td class="white_bg" width="7%" align="center">
                                                        <%#Eval("TO_CITY")%>
                                                    </td>
                                                    <td class="white_bg" width="8%" align="Center">
                                                        <%#Eval("Qty")%>&nbsp;
                                                    </td>
                                                    <td class="white_bg" width="5%" align="Center">
                                                        <%#Eval("Tot_Weght")%>&nbsp;
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                <div style="border:1px; border-style:solid;">
                                    <table cellpadding="0" cellspacing="0" width="800px" border="0" style="font-family: Arial,Helvetica,sans-serif; height: 70%">
                                        
                                        <tr>
                                        <td style="width:50%;text-align:center;" colspan="2">
                                        <div style="border:1px; border-style:solid;">
                                            <b>Freight Particular </b>
                                            </div>
                                               </td>
                                            <td style="width:50%;text-align:center; " colspan="2">
                                            <div style="border:1px; border-style:solid;">
                                                <b>Remarks</b>
                                                </div></td>
                                            </tr>
                                     
                                        <tr>
                                            <td style="width:25%;text-align:left;font-size:12px;  ">
                                                <b>Freight:(Lorry Hired)</b></td>
                                            <td style="width:25%; text-align:right; font-size:10px;">
                                           <asp:Label ID="lblFreight" Font-Bold="true" Font-Size="10"  runat="server"></asp:Label></td>
                                                 <td >
                                                 <asp:Label ID="lblRemark" font-size="10px" runat="server" Text=""></asp:Label>
                                                     </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size:12px;  ">
                                                <b>Advanced Paid</b>
                                        </td>
                                            <td style="width:25%;text-align:right;font-size:10px;  ">
                                                <asp:Label ID="lbladvPaid" Font-Bold="true" Font-Size="10"  runat="server"></asp:Label></td>
                                                 <td style=" ">
                                                     &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="font-size:12px;  ">
                                                <b>TDS</b></td>
                                            <td style="width:25%;text-align:right;font-size:10px;  ">
                                                <asp:Label ID="lblTds" Font-Bold="true" Font-Size="10" runat="server"></asp:Label></td>
                                              <td  style=" ">
                                                  &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="font-size:12px;  ">
                                                <b >Total Freight Amount</b></td>
                                            <td style="width:25%;text-align:right;font-size:10px;  ">
                                                <asp:Label ID="lblTotalFreight" Font-Bold="true" Font-Size="10" runat="server"></asp:Label>
                                            </td>
                                              <td style=" ">
                                                  &nbsp;</td>
                                        </tr>
                                    </table>
                                    </div>

                                </td>
                              
                            </tr>
                           
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" align="left" >
                                <div style="border:1px; border-style:solid;">
                                    <table cellpadding="0" cellspacing="0" width="800px"  style="font-family: Arial,Helvetica,sans-serif; height: 70%">
                                        <tr>
                                            <td style="width:30%;text-align:left;font-size:12px;">
                                           Vehicle Owner's Name
                                                </td>
                                            <td colspan="5">
                                                <asp:Label ID="lblOName" Font-Size="10" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width:30%;text-align:left;font-size:12px;">
                                                Vehicle Owner's Address</td>
                                            <td colspan="5">
                                                <asp:Label ID="lblOAddress" Font-Size="10" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width:30%;text-align:left;font-size:12px;">
                                               Vehicle Owner's PAN No</td>
                                            <td colspan="5">
                                                <asp:Label ID="lblOpanno" Font-Size="10" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width:30%;text-align:Left;font-size:12px;">
                                                Engine No.</td>

                                            <td colspan="3">
                                                <asp:Label ID="lblEngineno" Font-Size="10" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                                            <td style="font-size:12px;">
                                                Chassis No.</td>
                                            <td>
                                            <asp:Label ID="lblChasisNo" Font-Size="10" Font-Bold="true" runat="server" Text=""></asp:Label>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td style="width:30%;text-align:Left;font-size:12px;">
                                                Driver's Name & Address</td>
                                            <td colspan="5">
                                                <asp:Label ID="lblDriverAddress" Font-Size="10" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width:30%;text-align:Left;font-size:12px;">
                                                Licence No.</td>
                                            <td class="style1">
                                                <asp:Label ID="lblLicnenceNo" Font-Size="10" Font-Bold="true" runat="server"
                                        Text=""></asp:Label></td>
                                            <td style="font-size:12px";>
                                                Model</td>
                                            <td >
                                                <asp:Label ID="lblmodelno" Font-Size="10" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                                            <td style="font-size:12px";>
                                                Make</td>
                                            <td>
                                                <asp:Label ID="lblMake" Font-Size="10" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width:30%;text-align:Left; font-size:12px">
                                                Insurance Co. & Policy No.</td>
                                            <td class="style1" colspan="5">
                                                <asp:Label ID="lblIns" Font-Size="10" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width:30%;text-align:Left; font-size:12px">
                                                 Fitness Date</td>
                                            <td class="style1">
                                                <asp:Label ID="lblFitness" Font-Size="10" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                                            <td style="font-size:12px;" colspan="2">
                                                <strong>N/P</strong></td>
                                            <td colspan="2">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width:30%;text-align:left;font-size:12px">
                                            Lorry Broker Name
                                               </td>
                                            <td  colspan="5">
                                                <asp:Label ID="lblBrokerName" Font-Size="10" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width:30%;text-align:Left;font-size:12px;">
                                               Lorry Broker Address</td>
                                            <td  colspan="5">
                                                <asp:Label ID="lblAddress" Font-Size="10" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width:30%;text-align:Left; font-size:12px";>
                                                Lorry Broker PAN No.</td>
                                            <td  colspan="5">
                                                <asp:Label ID="lblBPanNo" Font-Size="10" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:Left; font-size:10px;"  colspan="4">
                                                <span >Received the above goods in good condition for safe 
                                                delivery at destination.</span><br />
                                                <span >Also received the relative manifest and documents.</span><br 
                                                     />
                                                <span >I agree to delivery the goods as per the instruction of the 
                                                at destination. </span></td>
                                        </tr>
                                    </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%" colspan="5">
                                
                                    <table width="100%" align="right">
                                    <tr>
                                    <td width="35%" align="Left" >
                                    <br />
                                                <br />
                                                <br />
                                                <b style="font-size:12px">Signature of Deriver/Owner</b>
                                    </td>
                                    <td style="width:35%"></td>
                                    <td style="width:40%">
                                    <table width="100%">
                                                    <tr>
                                                        <td align="right" class="white_bg" style="font-size: 12px" valign="top" >
                                                            <b>
                                                                <br />
                                                                <br />
                                                                <br />
                                                                Signature of issuin Officer&nbsp;</b>
                                                        </td>
                                                    </tr>
                                                </table></td>
                                    </tr>
                                        <tr>
                                            <td colspan="3" align="left" width="100%">
                                                <asp:Label ID="lbltTerms" runat="server" Font-Size="12px" Font-Bold="true" Text="Terms&Conditions:"  valign="right"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblTermsCond" runat="server" Font-Size="10px" Text="" valign="right"></asp:Label>
                                                
                                            </td>
                                            <td width="20%" align="center">
                                            </td>
                                            <td width="16%" align="center" valign="top">
                                            </td>
                                            <td colspan="2" width="20%" align="right" valign="top">
                                                
                                            </td>
                                        </tr>
                                    </table>
                                  
                                </td>
                            </tr>
                        </table>
                    </div>
               <%-- </td>
            </tr>
        </table>
    </div>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
    <script type="text/javascript">
        $(".datepicker").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
            minDate: '<%=hidmindate.Value%>',
            maxDate: '<%=hidmaxdate.Value%>'
        });
        function openModal() {
            $('#dvGrdetails').modal('show');
        }

        function CloseModal() {
            $('#dvGrdetails').Hide();
        }
        function SelectAllCheckboxes(spanChk) {
            // Added as ASPX uses SPAN for checkbox
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
           spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
          elm[i].id != theBox.id) {
                    //elm[i].click();

                    if (elm[i].checked != xState)
                        elm[i].click();
                    //elm[i].checked=xState;

                }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function netamount() {
            var txtfreight = document.getElementById("<%=txtfreight.ClientID %>").value;
            var txtadvance = document.getElementById("<%=txtadvance.ClientID %>").value;
            var txtnetamnt = document.getElementById("<%=txtnetamnt.ClientID %>").value;
            if (txtfreight > 0 && txtfreight > txtadvance) {

                document.getElementById("<%=txtnetamnt.ClientID %>").value = parseFloat(txtfreight - txtadvance).toFixed(2);
            }
            else {

                PassMessage("Freight Amount SHOULD BE Greater Then ADVANCE AMOUNT");
                document.getElementById("<%=txtnetamnt.ClientID %>").value = "0.00";
                document.getElementById("<%=txtadvance.ClientID %>").value = "0.00";
            }

        }
        function netamounta() {

            var txtfreight = document.getElementById("<%=txtfreight.ClientID %>").value;
            var txtadvance = document.getElementById("<%=txtadvance.ClientID %>").value;
            var txtnetamnt = document.getElementById("<%=txtnetamnt.ClientID %>").value;
            if (txtadvance <= txtfreight) {

                document.getElementById("<%=txtnetamnt.ClientID %>").value = parseFloat(txtfreight - txtadvance).toFixed(2);

            }
            else {
                PassMessage("Advance Amount Can't Be Greater Then Freight Amount");
                document.getElementById("<%=txtadvance.ClientID %>").value = "0.00";
                document.getElementById("<%=txtnetamnt.ClientID %>").value = "0.00";

            }
        }

        function SetNumFormt(objddl) {
            if (objddl != "") {

                if (parseFloat(document.getElementById(objddl).value).toFixed(2) == "NaN") {
                    document.getElementById(objddl).value = "0.00";
                }
                else {
                    document.getElementById(objddl).value = parseFloat(document.getElementById(objddl).value).toFixed(2);
                }

            }

        }
        
    </script>
    <script type="text/javascript">
        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $("#<%=txtDateFrom.ClientID %>").datepicker({
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
