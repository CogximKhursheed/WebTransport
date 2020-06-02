<%@ Page Title="Party payment against challan" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="ChlnAmntPayment.aspx.cs" Inherits="WebTransport.ChlnAmntPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <asp:UpdatePanel ID="updpnl" runat="server" ViewStateMode="Enabled">
        <ContentTemplate>--%>
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-10">
            <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">PARTY PAYMENT AGAINST CHALLAN
                  <span class="view_print"><a href="ManageChlnAmntPayment.aspx"><asp:Label ID="lblViewList" runat="server" TabIndex="18" Text="LIST"></asp:Label></a>
                &nbsp;
                <asp:LinkButton ID="lnkBtnLast" runat="server"  AlternateText="Print" title="Print" Height="16px" onclick="lnkBtnLast_Click">LAST PRINT</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lnkbtnPrint" runat="server" ToolTip="Click to print" Visible="false" OnClientClick ="return CallPrint('print');"><i class="fa fa-print icon"></i></asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="lnkbtnPrintClick" runat="server" OnClick="lnkbtnPrintClick_OnClick" ToolTip="Click to print" Visible="false"><i class="fa fa-print icon"></i></asp:LinkButton>
                <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Click to print" OnClientClick="return CallPrint('print');" Visible="false"><i class="fa fa-print icon"></i></asp:LinkButton>

                <asp:LinkButton ID="print1" runat="server" OnClick="btnPrintClick_OnClick" ToolTip="Click to print1" Visible="false"><i class="fa fa-print icon"></i></asp:LinkButton>
                 <%-- <asp:LinkButton ID="printtt" runat="server" ToolTip="Click to print1" OnClientClick="return CallPrint1('printf');" Visible="false"><i class="fa fa-print icon"></i></asp:LinkButton>--%>
                 </span>
                </header>
                <div class="panel-body">
                  <form class="bs-example form-horizontal">
                    <!-- first  section --> 
                    <div class="clearfix first_section">
                      <section class="panel panel-in-default">  
                        <div class="panel-body">
                        	<div class="clearfix odd_row">
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 29%;">Date Range<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 71%;">
                              <asp:DropDownList ID="ddldateRange" runat="server" AutoPostBack="true" CssClass="form-control"
                                 TabIndex="1"  OnSelectedIndexChanged="ddldateRange_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddldateRange"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Year!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>                             
                              </div>
                            </div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 29%;">Pay. No.<span class="required-field">*</span></label>

                              <div class="col-sm-3" style="width: 40%;">
                                <asp:TextBox ID="txtRcptNo" runat="server" CssClass="form-control" MaxLength="50"  oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                    TabIndex="2"  ReadOnly="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRcptNo"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Rcpt no!"
                                    SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>

                              <div class="col-sm-4" style="width: 31%;">
                              <asp:TextBox ID="txtGRDate" runat="server" CssClass="input-sm datepicker form-control" MaxLength="50" data-date-format="dd-mm-yyyy"
                                oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"  TabIndex="3"  ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGRDate"  CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Date!"
                                SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>                               
                              </div>
                           	</div>
                           	<div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 29%;">Loc.[From]</label>
                              <div class="col-sm-9" style="width: 71%;">
                                  <asp:DropDownList ID="ddlFromCity" runat="server" CssClass="form-control"  TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="ddlFromCity_SelectedIndexChanged">
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlFromCity"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select From city!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>--%>
                              </div>
                              
                            </div>
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 29%;">Party<span class="required-field">*</span></label>
                              <div class="col-sm-8" style="width: 61%;">
                                <asp:DropDownList ID="ddlPartyName" runat="server" AutoPostBack="true" CssClass="chzn-select" style="width:100%;" TabIndex="5" OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged1">
                                </asp:DropDownList>                               
                                <asp:RequiredFieldValidator ID="rfvddlPartyName" runat="server" ControlToValidate="ddlPartyName"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Party Name!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>                                                   
                              </div>
                              <div class="col-sm-1" style="width: 10%;">
                            <asp:LinkButton ID="lnkimgbtnSearch" runat="server" ToolTip="Select Challan Details" TabIndex="6" CssClass="btn btn-sm btn-primary acc_home" OnClick="lnkimgbtnSearch_OnClick"><i class="fa fa-file"></i></asp:LinkButton>
                                                         
                              </div>
                            </div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 29%;">Pay. Type<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 71%;">
                                 <asp:DropDownList ID="ddlRcptTyp" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="7" OnSelectedIndexChanged="ddlRcptTyp_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlRcptTyp" runat="server" ControlToValidate="ddlRcptTyp"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Rcpt Type!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>                                          
                              </div>
                           	</div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 29%;">Inst.Details</label>

                              <div class="col-sm-3" style="width: 40%;">
                                <asp:TextBox ID="txtInstNo" runat="server" placeholder="Enter Inst.No." CssClass="form-control"  MaxLength="6" Style="text-align: right;" TabIndex="8"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvinstno" runat="server" ControlToValidate="txtInstNo"
                                Display="Dynamic" SetFocusOnError="true"  ValidationGroup="save"  ErrorMessage="Enter Inst. No!" CssClass="classValidation"></asp:RequiredFieldValidator>                                                           
                          
                              </div>

                              <div class="col-sm-4" style="width: 31%;">
                               <asp:TextBox ID="txtInstDate" runat="server" CssClass="input-sm datepicker form-control" TabIndex="9" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvinstDate" runat="server" ControlToValidate="txtInstDate"
                                    Display="Dynamic" SetFocusOnError="true" ValidationGroup="save"  ErrorMessage="Enter Inst. Date!" CssClass="classValidation"></asp:RequiredFieldValidator>
                             
                              </div>
                           	</div>
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 29%;">Cust.Bank</label>
                              <div class="col-sm-8" style="width: 71%;">

                               <asp:DropDownList ID="ddlCustmerBank" runat="server" CssClass="form-control" TabIndex="10" >
                                </asp:DropDownList>
                              <%--  <asp:RequiredFieldValidator ID="rfvCusBank" runat="server" ControlToValidate="ddlCustmerBank"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Cust Bank!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>--%>
    
                              </div>
                            </div>
                            <div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 29%;">Location<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 71%;">
                               <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" TabIndex="11">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvLocation" runat="server" Display="Dynamic" ControlToValidate="ddlLocation"
                                    ValidationGroup="save" ErrorMessage="Please Select Delivery Place!" InitialValue="0"
                                    SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>                            
                              </div>
                           	</div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 29%;">Amount</label>

                              <div class="col-sm-3" style="width: 71%;">
                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" Text="0.00" MaxLength="10" TabIndex="12" AutoPostBack="true"  Enabled="false" Style="text-align: right;" onKeyPress="return checkfloat(event, this);" OnTextChanged="txtAmount_TextChanged"></asp:TextBox>                           
                              </div>
                           	</div>                           	
                          </div>

                          <div class="clearfix even_row">
                          	<label class="col-sm-3 control-label" style="width: 9.5%;">Remark</label>
                            <div class="col-sm-9" style="width: 90.5%;">
                                <asp:TextBox ID="TxtRemark" runat="server" placeholder="Enter Remark" CssClass="form-control" MaxLength="200"  oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                 TabIndex="13" TextMode="MultiLine" Style="resize: none"></asp:TextBox>                             
                            </div>
                          </div>

                        </div>
                      </section>                        
                    </div>
                    
                    <!-- second  section -->
                    <div class="clearfix third_right">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body" style="overflow:auto">     
                           <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="display nowrap dataTable"
                            BorderStyle="Solid" GridLines="Both"  BorderWidth="1"  ShowFooter="true" OnDataBound="grdMain_DataBound"
                            OnRowDataBound="grdMain_RowDataBound" ViewStateMode="Enabled" OnSelectedIndexChanged="grdMain_SelectedIndexChanged">
                           <RowStyle CssClass="odd" />
                          <AlternatingRowStyle CssClass="even" />   
                            <Columns>
                                <asp:TemplateField HeaderText="Chln. No." HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                    <ItemStyle Width="50" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrno" runat="server" Text='<%#Convert.ToString(Eval("Chln_No"))%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Challan Date" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                    <ItemStyle Width="50" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrDate" runat="server" Text='<%#Convert.ToDateTime(Eval("Chln_Date")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="From City" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                    <ItemStyle Width="50" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblFromcity" runat="server" Text='<%#Eval("From_City")%>'></asp:Label>
                                        <asp:HiddenField ID="hidGrIdno" runat="server" Value='<%#Eval("Chln_Idno")%>' />
                                        <asp:HiddenField ID="hidFromCityIdno" runat="server" Value='<%#Eval("FromCity_Idno")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Truck No." HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                    <ItemStyle Width="50" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTruckNo" runat="server" Text='<%#Eval("Truck_No")%>'></asp:Label>
                                        <asp:HiddenField ID="hidTruck_Idno" runat="server" Value='<%#Eval("Truck_IDno")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Driver Name" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                    <ItemStyle Width="50" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDriver" runat="server" Text='<%#Eval("Driver_Name")%>'></asp:Label>
                                        <asp:HiddenField ID="hidDriver_Idno" runat="server" Value='<%#Eval("Driver_Idno")%>' />
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left" />
                                    <FooterTemplate>
                                        Total
                                    </FooterTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Adv. Amount" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                    <ItemStyle Width="50" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAdvAmt" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Adv_Amnt")))%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lblAdvAmt" runat="server"></asp:Label>
                                    </FooterTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Diesel Amount" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                    <ItemStyle Width="50" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDisAmount" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Diesel_Amnt")))%>'></asp:Label>
                                    </ItemTemplate>
                                      <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lblDisAmount" runat="server"></asp:Label>
                                    </FooterTemplate>
                                 </asp:TemplateField>
                                  <asp:TemplateField HeaderText="TDS Amount" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                    <ItemStyle Width="50" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTdsAmount" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("TDSTax_Amnt")))%>'></asp:Label>
                                    </ItemTemplate>
                                       <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lblTdsAmount" runat="server"></asp:Label>
                                    </FooterTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Commisison Amount" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                    <ItemStyle Width="50" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblComisionAmount" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Commsn_Amnt")))%>'></asp:Label>
                                    </ItemTemplate>
                                       <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lblComisionAmount" runat="server"></asp:Label>
                                    </FooterTemplate>
                                 </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                    <ItemStyle Width="50" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Amount")))%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Prev.Paid Amount" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                    <ItemStyle Width="50" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotRecvd" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Tot_Recvd")))%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lblFTotRecvd" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Prev.Bal" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                    <ItemStyle Width="50" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurBal" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("cur_Bal")))%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lblFTotCurBal" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Paid Amount" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle Width="100" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRcvdAmnt" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Recv_Amount")))%>'
                                            OnTextChanged="txt_txtRcvdAmnt" AutoPostBack="true" EnableViewState="true" ViewStateMode="Enabled"
                                            MaxLength="10" Width="90px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                         <%--  <EmptyDataTemplate>
                                <asp:Label ID="lblnorecord" runat="server" Text="No record found"></asp:Label>
                            </EmptyDataTemplate>  --%>                    
                        </asp:GridView>

                        </div>
                      </section>
                    </div> 

                    <div class="clearfix">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body">
                          <div class="clearfix even_row">
                            <div class="col-sm-9"></div>
                           	<div class="col-sm-3">
                              <label class="col-sm-5 control-label">Net Amount</label>
                              <div class="col-sm-7">
                               <asp:TextBox ID="txtNetAmnt" runat="server" CssClass="form-control" MaxLength="50" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                                onpaste="return false" Style="text-align:right;" TabIndex="14" ReadOnly="true" Text="0.00"></asp:TextBox>                              
                              </div>
                            </div>			                              
                          </div>
                        </div>
                      </section>
                    </div>

                     <!-- fourth row -->
                    <div class="clearfix odd_row">
                      <div class="col-lg-4"></div>
                      <div class="col-lg-4">  
                      <div class="col-sm-4">
                      <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-info" TabIndex="17" OnClick="lnkbtnNew_OnClick" ><i class="fa fa-file-o"></i>New</asp:LinkButton> 
                      </div>                                      
                        <div class="col-sm-4">
                       
                        <asp:HiddenField ID="hidid" runat="server" Value="" />
                        <asp:HiddenField ID="Hidrowid" runat="server" Value="" />
                        <asp:HiddenField ID="hidmindate" runat="server" />
                        <asp:HiddenField ID="hidmaxdate" runat="server" />
                        <asp:HiddenField ID="hidpostingmsg" runat="server" />
                        <asp:HiddenField ID="hidPrintType" runat="server" />
                        <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" ValidationGroup="save" TabIndex="15" CssClass="btn full_width_btn btn-s-md btn-success" OnClick="lnkbtnSave_OnClick" ><i class="fa fa-save"></i>Save</asp:LinkButton>                        
                        </div>
                        <div class="col-sm-4">
                         <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-danger" TabIndex="16" OnClick="lnkbtnCancel_OnClick" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>                        
                        </div>
                      </div>
                      <div class="col-lg-4"></div>
                    </div>

                    <!-- popup form GR detail -->
			        <div id="dvGrdetails" class="modal fade">
							<div class="modal-dialog">
							<div class="modal-content">
								<div class="modal-header">
								<h4 class="popform_header">Challan Detail </h4>
								</div>
								<div class="modal-body">
								<section class="panel panel-default full_form_container material_search_pop_form">
									<div class="panel-body">
										<!-- First Row start -->
									<div class="clearfix odd_row">	                                
	                                <div class="col-sm-6">
	                                  <label class="col-sm-4 control-label">Date From</label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txtDateFrom" runat="server" CssClass="input-sm datepicker form-control" TabIndex="85" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtFrm" runat="server" ErrorMessage="Enter From Date!"
                                            Display="Dynamic" CssClass="classValidation" ControlToValidate="txtDateFrom" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>

                                    </div>
	                                </div>
	                                <div class="col-sm-6">
	                                  <label class="col-sm-4 control-label">Date To</label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txtDateTo" runat="server" CssClass="input-sm datepicker form-control" TabIndex="86" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtTo" runat="server" ErrorMessage="Enter To Date!"
                                            Display="Dynamic" CssClass="classValidation" ControlToValidate="txtDateTo" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>                                    
                                    </div>
	                                </div>
	                              </div> 

	                              <div class="clearfix even_row">
	                                <div class="col-sm-6">
	                                	<label class="col-sm-4 control-label">Challan No.</label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txtchlnNoSearch" runat="server" CssClass="form-control" TabIndex="87"></asp:TextBox>
                                    </div>
	                                </div>
	                                <div class="col-sm-6" style="padding: 0;">
	                                  <div class="col-sm-4 prev_fetch">
                                   
                                            <asp:LinkButton ID="lnkbtnPreview" CssClass="btn full_width_btn btn-sm btn-primary"  TabIndex="88" runat="server" CausesValidation="true" ValidationGroup="RcptEntrySrch" OnClick="lnkbtnPreview_OnClick"><i class="fa fa-search"></i>Search</asp:LinkButton>
	                                  
	                                  </div>
	                                  <div class="col-sm-8"> 
                                      &nbsp;
	                                  <%--   <label class="control-label">T. Record(s) : </label>--%>
	                                  </div>
	                                </div>
	                              </div> 
	                            
                                   <div class="clearfix third_right">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body" style="overflow:auto; height:400px;">     
	                            <asp:GridView ID="grdGrdetals" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="display nowrap dataTable"
                                    Width="100%" BorderStyle="None" AllowPaging="false" PageSize="100" BorderWidth="0" OnPageIndexChanging="grdGrdetals_PageIndexChanging">
                                 <RowStyle CssClass="odd" />
                                 <AlternatingRowStyle CssClass="even" />    
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" HeaderStyle-Width="40px">
                                            <HeaderStyle Width="40" HorizontalAlign="Center" />
                                            <ItemStyle Width="40" HorizontalAlign="Center" />
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAllCheckboxes(this);"
                                                    CssClass="SACatA" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkId" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Chln. No." HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("Chln_No"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Chln. Date" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToDateTime(Eval("Chln_Date")).ToString("dd-MMM-yyyy")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px" HeaderText="From  City">
                                            <ItemStyle HorizontalAlign="Left" Width="180px" />
                                            <ItemTemplate>
                                                <%#Eval("From_City")%>
                                                <asp:HiddenField ID="hidGrIdno" runat="server" Value='<%#Eval("Chln_Idno")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Truck No." HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("Truck_No"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Driver" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("Driver_Name"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px" HeaderText="Chln. Amnt">
                                            <ItemStyle HorizontalAlign="Left" Width="180px" />
                                            <ItemTemplate>
                                                <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Amount")))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px" HeaderText="Tot.PaidAmnt">
                                            <ItemStyle HorizontalAlign="Left" Width="180px" />
                                            <ItemTemplate>
                                                <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Tot_Recvd")))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px" HeaderText="Cur.Bal">
                                            <ItemStyle HorizontalAlign="Left" Width="180px" />
                                            <ItemTemplate>
                                                <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("cur_Bal")))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px" HeaderText="Paymnt Amnt">
                                            <ItemStyle HorizontalAlign="Left" Width="180px" />
                                            <ItemTemplate>
                                                <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Recv_Amount")))%>
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
                                 <div class="modal-footer">
                                    <div class="popup_footer_btn">
                                        <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-dark" OnClick="lnkbtnOk_OnClick"
                                            TabIndex="89"><i class="fa fa-check"></i>Ok</asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnClose" runat="server" CssClass="btn btn-dark" OnClick="lnkbtnClose_OnClick"
                                            TabIndex="90"><i
                            class="fa fa-times"></i>Close</asp:LinkButton>
            </div>
        </div>
        </div>
    </section>
        </div>
        
    </div>
    </div> </div> 
    
    </form> </div> </section>
        </div>
        <div class="col-lg-1">
        </div>

        <div id="print" style="font-size: 13px;display:none;">
        <table cellpadding="1" cellspacing="0" width="1100px" border="1" style="font-family: Arial,Helvetica,sans-serif;">
            <tr>
                <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                    border-left-style: none; border-right-style: none">
                    <strong>
                        <asp:Label ID="lblCompanyname" runat="server" Style="font-size: 14px;"></asp:Label><br />
                    </strong>
                    <asp:Label ID="lblCompAdd1" runat="server"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblCompAdd2" runat="server"></asp:Label><br />
                    <asp:Label ID="lblCompCity" runat="server"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblCompState" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblCompCityPin" runat="server"></asp:Label><br /><br />
                    <asp:Label ID="Label4" runat="server" Text="Party Bill Freight Payment Details"></asp:Label>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td colspan="4">
                    <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table1">
                        <asp:Repeater ID="Repeater1" runat="server" 
                            onitemdatabound="Repeater1_ItemDataBound">

                            <HeaderTemplate>
                                <tr>
                                    <td class="white_bg" style="font-size: 12px" width="3%" style="padding:10px;">
                                        <strong>S.No.</strong>
                                    </td>
                                    <td style="font-size: 12px" width="6%" align="center">
                                        <strong>GR Date</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>GR No.</strong>
                                    </td>
                                    <td style="font-size: 12px" width="5%">
                                        <strong>Truck No</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>Destination</strong>
                                    </td>
                                    <td style="font-size: 12px" align="left" width="6%">
                                        <strong>Actual-Wt</strong>
                                    </td>

                                    <td style="font-size: 12px" align="left" width="8%">
                                        <strong>Frt Rate</strong>
                                    </td>

                                    <td style="font-size: 12px" width="8%">
                                        <strong>Book Freight</strong>
                                    </td>
                                     <td style="font-size: 12px" width="8%" align="center">
                                        <strong>TDS</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>Shortage</strong>
                                    </td>
                                    <td style="font-size: 12px" width="7%">
                                        <strong>Gross Freight</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>Date</strong>
                                    </td>
                                    <td style="font-size: 12px" width="6%">
                                        <strong>Adv Amount</strong>
                                    </td>
                                    <td style="font-size: 12px" width="6%">
                                        <strong>Balance Payment</strong>
                                    </td>
                                    <td style="font-size: 12px" width="5%">
                                        <strong>Less Site-Exp</strong>
                                    </td>
                                    <td style="font-size: 12px" width="5%">
                                        <strong>Add Unloading</strong>
                                    </td>
                                    <td style="font-size: 12px" width="7%">
                                        <strong>Payable Amount</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="white_bg" width="3%">
                                        <%#Container.ItemIndex+1 %>.
                                    </td>
                                    <td class="white_bg" width="5%" style="padding:10px;">
                                        <%#Convert.ToDateTime(Eval("GRDate")).ToString("dd-MM-yyyy")%>
                                    </td>
                                    <td class="white_bg" width="8%" align="center">
                                        <%#Eval("GRNo")%>
                                    </td>
                                    <td class="white_bg" width="8%">
                                        <%#Eval("LorryNo")%>
                                    </td>
                                    <td class="white_bg" width="8%">
                                        <%#Eval("CityName")%>
                                    </td>
                                    <td class="white_bg" width="6%" align="center">
                                        <%#Eval("TotWeight")%>&nbsp;
                                    </td>

                                    <td class="white_bg" width="8%" align="center">
                                        <%#Eval("FrtRate")%>&nbsp;
                                    </td>

                                    <td class="white_bg" width="8%" align="left">
                                        <%#Eval("GrossAmnt")%>&nbsp;
                                    </td>
                                      <td class="white_bg" width="8%" align="center">
                                        <%#Eval("TDS")%>&nbsp;
                                    </td>
                                    <td class="white_bg" width="8%" align="center">
                                        <%#(Eval("ShortageAmount"))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="white_bg" width="7%" align="left">
                                        <%#Eval("GrossFreight")%>
                                    </td>
                                    <td class="white_bg" width="8%" align="left">
                                       <%#Convert.ToDateTime(Eval("ChlnDate")).ToString("dd-MM-yyyy")%>
                                    </td>
                                    <td class="white_bg" width="6%" align="left">
                                        <%#Eval("AdvanceAmount")%>
                                    </td>
                                    <td class="white_bg" width="15%" align="right">
                                       <%#Eval("BalancePayment")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="white_bg" width="15%" align="right">
                                        <%#Eval("Site_Exp")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="white_bg" width="15%" align="right">
                                       <%#Eval("Unloading")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="white_bg" width="15%" align="right">
                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("PayablePayment")))%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                            <tr>
                                    <td class="white_bg" width="3%" style="padding:10px;">
                                     
                                    </td>
                                    <td class="white_bg" width="5%">
                                       
                                    </td>
                                    <td class="white_bg" width="8%">
                                       
                                    </td>
                                    <td class="white_bg" width="8%">
                                      <asp:Label ID="lblTot" Font-Bold="true" runat="server" Text="TOTAL"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="8%">
                                       
                                    </td>
                                    <td class="white_bg" width="6%" align="center">
                                       <asp:Label ID="lblFTotWt" Font-Bold="true" runat="server"></asp:Label> &nbsp;
                                    </td>
                                     <td class="white_bg" width="8%">
                                       
                                    </td>
                                    <td class="white_bg" width="8%" align="left">
                                       <asp:Label ID="lblFAmnt" Font-Bold="true" runat="server"></asp:Label>&nbsp;
                                    </td>
                                      <td class="white_bg" width="8%" align="center">
                                        <asp:Label ID="lblFTDS" Font-Bold="true" runat="server"></asp:Label>&nbsp;
                                    </td>
                                    <td class="white_bg" width="8%" align="center">
                                        <asp:Label ID="lblShor" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="white_bg" width="7%" align="left">
                                        <asp:Label ID="lblFGross" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="8%" align="left">
                                      
                                    </td>
                                    <td class="white_bg" width="6%" align="left">
                                        <asp:Label ID="lblFAdv" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="15%" align="right">
                                      <asp:Label ID="lblBal" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="white_bg" width="15%" align="right">
                                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="white_bg" width="15%" align="right">
                                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="white_bg" width="15%" align="right">
                                        <asp:Label ID="lblPayable" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table3">
                        <tr>
                            <td class="white_bg" width="15%" align="left">
                            Truck Owner/ Party Ledger A/c :
                            </td>
                            <td class="white_bg" width="5%">
                            </td>
                            <td class="white_bg" width="10%">
                            </td>
                            <td class="white_bg" width="9%" align="left">
                            </td>
                            <td class="white_bg" width="10%" align="right">
                            </td>
                            <td align="right" class="white_bg" width="5%">
                            </td>
                            <td class="white_bg" width="4%" align="center">
                            </td>
                            <td class="white_bg" width="9%" align="center">
                            </td>
                        </tr>
                          <tr>
                            <td class="white_bg" width="15%" align="left">
                            </td>
                            <td class="white_bg" width="5%">
                            </td>
                            <td class="white_bg" width="10%">
                            </td>
                            <td class="white_bg" width="9%" align="left">
                            </td>
                            <td class="white_bg" width="10%" align="right">
                                
                            </td>
                            <td align="right" class="white_bg" width="5%">
                               
                            </td>
                            <td class="white_bg" width="4%" align="center">
                                
                            </td>
                            <td class="white_bg" width="9%" align="center">
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="white_bg" width="15%" align="left">
                            Cash/Cheque Amount : Rs.
                            </td>
                            <td class="white_bg" width="5%" align="left">
                            Cheque No.:
                            </td>
                            <td class="white_bg" width="10%" align="left">
                            Dt.
                            </td>
                            <td class="white_bg" width="9%" align="left">
                            Prepared By
                            </td>
                            <td class="white_bg" width="10%" align="left">
                               
                            </td>
                            <td align="left" class="white_bg" width="5%">
                                Checked By 
                            </td>
                            <td class="white_bg" width="4%" align="center">
                                
                            </td>
                            <td class="white_bg" width="9%" align="center">
                                Receiver's Siq
                            </td>
                        </tr>
                        <tr>
                            <td class="white_bg" width="15%">
                            </td>
                            <td class="white_bg" width="15%">
                            </td>
                            <td class="white_bg" width="5%">
                            </td>
                            <td class="white_bg" width="4%" align="left">
                            </td>
                            <td class="white_bg" width="10%" align="right">
                                
                            </td>
                            <td align="right" class="white_bg" width="5%">
                                
                            </td>
                            <td class="white_bg" width="5%" align="center">
                                
                            </td>
                            <td class="white_bg" width="9%" align="center">
                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table width="100%" align="right">
                        <tr>
                            <td colspan="3" align="left" width="30%">
                                <table>
                                    <tr>
                                        <td width="80%">
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="80%">
                                           
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="16%" align="center" valign="top">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </div>
     <table width="100%">
        <tr style="display: none">
            <td class="white_bg" align="center">
                <div id="printf" style="font-size: 13px;">
    <table width="100%" border="1" cellspacing="0" cellpadding="0" style=" margin:0 auto; text-align:left;">
  <tr>
    <td colspan="4" style="width:80%; padding-bottom: 35px; text-align: center;"><span style="font-size:40px;"><u>Westend Roadlines</u></span></td>
    <td style="padding-bottom: 30px; text-align: center;">Voucher No: <asp:Label ID="lblvoucher" runat="server"></asp:Label> Date: <asp:Label ID="lbldate" runat="server"></asp:Label></td>
  </tr>
  <tr>
    <td colspan="4" style="text-align: center; line-height: 30px;"><b>Detalls</b></td>
    <td style="text-align: center;"><b>Rs.</b></td>
  </tr>
  <tr>
    <td colspan="4" style="line-height: 30px;">Truck No: <asp:Label ID="lblTruckno" runat="server"></asp:Label> ! Truck Owener: <asp:Label ID="lbltruckowner" runat="server"></asp:Label></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td colspan="4" style="line-height: 30px;">Village : <asp:Label ID="lblvillage" runat="server"></asp:Label> ! Talika : <asp:Label ID="lbltalika" runat="server"></asp:Label> ! District Name : <asp:Label ID="lbldist" runat="server"></asp:Label> </td>
    <td> </td>
  </tr>
  <tr>
    <td colspan="4"><b>On Account of Cement - Advance Payment </b></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td colspan="4" style="line-height: 30px;">Rate PMT :<u><asp:Label ID="lblrate" runat="server"></asp:Label> </u>  WT : <asp:Label ID="lblwt" runat="server"></asp:Label> </td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td colspan="4" style="line-height: 30px; text-align: right;">Total Fare :</td>
    <td style="text-align: right;"><asp:Label ID="lbltotal" runat="server"></asp:Label> </td>
  </tr>
  <tr>
    <td colspan="4" style="line-height: 30px; text-align: right;">Payment Detalls : <asp:Label ID="lblpaymentde" runat="server"></asp:Label>  Cash (Advance) : </td>
    <td style="text-align: right;"><asp:Label ID="lblcash" runat="server"></asp:Label> </td>
  </tr>
  <tr>
    <td colspan="4" style="line-height: 30px;">Diesel (PASTA PETROLEUMS):</td>
    <td style="text-align: right;"><asp:Label ID="lbldiesel" runat="server"></asp:Label> </td>
  </tr>
  <tr>
    <td colspan="4" style="line-height: 30px;">Commission : </td>
    <td style="text-align: right;"><asp:Label ID="lblcomssion" runat="server"></asp:Label></td>
  </tr>
  <tr>
    <td colspan="4" style="line-height: 30px;">Total Due : </td>
    <td style="text-align: right;"><asp:Label ID="lbltotaldue" runat="server"></asp:Label></td>
  </tr>
  <tr>
    <td colspan="5" style="text-align: right; padding-top: 35px;">Signature :<u><span style="color: #fff;">__________________</span></u></td>
  </tr>
</table>
  </div>
            </td>
        </tr>
    </table>
    <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>
    <script language="javascript" type="text/javascript">
        function CallPrintf(strid) {
            var prtContent1 = "<table width='100%' border='0'></table>";
            var prtContent = document.getElementById(strid);
            var WinPrintf = window.open('', '', 'left=0,top=0,width=800,height=800,toolbar=1,scrollbars=1,status=1');
            WinPrintf.document.write(prtContent1);
            WinPrintf.document.write(prtContent.innerHTML);
            WinPrintf.document.close();
            WinPrintf.focus();
            WinPrintf.print();
            WinPrintf.close();
            return false;
        }
         </script>
  <script language="javascript" type="text/javascript">
             function CallPrint1(strid) {
            var prtContent1 = "<table width='100%' border='0'></table>";
            var prtContent = document.getElementById(strid);
            var WinPrint1 = window.open('', '', 'left=0,top=0,width=800,height=800,toolbar=1,scrollbars=1,status=1');
            WinPrint1.document.write(prtContent1);
            WinPrint1.document.write(prtContent.innerHTML);
            WinPrint1.document.close();
            WinPrint1.focus();
            WinPrint1.print();
            WinPrint1.close();
            return false;
        }
        </script>
   
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
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
            $("#<%=txtGRDate.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
            $("#<%=txtGRDate.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
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
            $("#<%=txtInstDate.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
        }

        function openModal() {
            $('#dvGrdetails').modal('show');
        }

        function CloseModal() {
            $('#dvGrdetails').Hide();
        }

        function HideBillAgainst() {
            $("#dvGrdetails").fadeOut(300);
        }

        function ShowClient() {
            $("#dvGrdetails").fadeIn(300);
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript">
        $(".datepicker").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
            minDate: '<%=hidmindate.Value%>',
            maxDate: '<%=hidmaxdate.Value%>'
        });
        function CallPrint(strid) {
            if (strid == 'print') {
             
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
            }
    </script>
</asp:Content>