<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="UserPreference.aspx.cs" Inherits="WebTransport.UserPreference" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-content">
        <div class="row ">
            <div class="col-lg-11">
                <section class="panel panel-default full_form_container part_purchase_bill_form">
								<header class="panel-heading font-bold form_heading">USER PREFERENCE&nbsp;
								</header>
								<div class="panel-body" style="height:">
									<form class="bs-example form-horizontal">
										<!-- first  section --> 
										<div class="clearfix first_section">
											<section class="panel panel-in-default" style="border:none;">  
												<div class="panel-body">	
                                                <div class="clearfix first_section">
	                                            <section class="panel panel-in-default">                                                 
	                                                <div class="panel-body">
	                                                <div class="clearfix estimate_second_row odd_row">
                                                      <div class="clearfix even_row">
                                                        <div class="col-sm-3">
															<label class="col-sm-5 control-label">Type</label>
															<div class="col-sm-7">
																<asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" Enabled="false" TabIndex="1">
                                                                    <asp:ListItem Text="City Wise" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Truck Wise" Value="2"></asp:ListItem>
                                                                </asp:DropDownList>
															</div>
														</div>
														<div class="col-sm-3">
															<label class="col-sm-6 control-label">Amount Rcpt.</label>
															<div class="col-sm-6">
																<asp:DropDownList ID="ddlamntrcptagnst" runat="server" CssClass="form-control" TabIndex="2">
                                                                    <asp:ListItem Text="Agnst GR" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Agnst Invoice" Value="2"></asp:ListItem>
                                                                </asp:DropDownList>                             
															</div>
														</div>
														<div class="col-sm-5">
                                                        <label class="col-sm-5 control-label">Inv Gen Type</label>
                                                            <div class="col-sm-7">
                                                            <asp:DropDownList ID="ddlInvGen" Enabled="false" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Text="[TBB GR]" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="[TBB GR] + [To Pay GR]" Value="2"></asp:ListItem>
                                                                </asp:DropDownList> 
                                                            </div>
                                                      </div> 
                                                      <div class="clearfix"></div>
                                                        <div class="col-sm-3">
															<label class="col-sm-5 control-label">TBB Rate</label>
															<div class="col-sm-7">
                                                            <div class="col-sm-2">
																<asp:CheckBox ID="chkTbbRate" runat="server" AutoPostBack="true" OnCheckedChanged="chkTbbRate_OnCheckedChanged" TabIndex="13" />
                                                                </div>
                                                                <div class="col-sm-10">
                                                                <asp:DropDownList ID="ddlRateInvoGr" runat="server" CssClass="form-control" TabIndex="14">
                                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Rate till Inv. Date" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Rate till Gr Date" Value="2"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlRateInvoGr"
                                                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select rate date"
                                                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                </div>
															</div>
														</div> 
                                                        <div class="col-sm-3">
															<label class="col-sm-10 control-label">Disable Challan Entry</label>
															<div class="col-sm-2">
																<asp:CheckBox ID="chkdisChln" runat="server" TabIndex="19" />
															</div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <label class="col-sm-8 control-label">Weight Wise Rate</label>
                                                        <div class="col-sm-3">
                                                        <asp:CheckBox ID="chkWeightWiseRate" onclick="ToggleWeightWiseRate()" runat="server" TabIndex="26" />
                                                        </div>
                                                    </div>  
                                                    <div class="col-sm-3">
                                                        <label class="col-sm-8 control-label">Less Chln Amnt(Invoice)</label>
                                                        <div class="col-sm-3">
                                                        <asp:CheckBox ID="chkLessChlnAmntInv" runat="server" TabIndex="26" ClientIDMode="Static" />
                                                        </div>
                                                    </div>  
                                                      <div class="clearfix"></div>
                                                        <div class="col-sm-4">
															<label class="col-sm-5 control-label">Serv. Tax (Valid)</label>
															<div class="col-sm-7">
															<asp:TextBox ID="txtServTaxValid" runat="server" AutoPostBack="true" CssClass="form-control"
                                                                Height="24px" MaxLength="8" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                                onpaste="return false" OnTextChanged="txtServTaxValid_TextChanged" 
                                                                TabIndex="11" Text="0.00"></asp:TextBox>                           
															</div>
														</div>
                                                        <div class="col-sm-3">
															<label class="col-sm-6 control-label">S.Tax% [PAN]</label>
															<div class="col-sm-6">
															<asp:TextBox ID="TxtStaxPan" runat="server" AutoPostBack="true" CssClass="form-control"
                                                                Height="24px" MaxLength="5" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                                onpaste="return false" OnTextChanged="TxtStaxPan_TextChanged" TabIndex="12" Text="0.00"></asp:TextBox>                      
															</div>
														</div>
                                                        <div class="col-sm-5">
															 <label class="col-sm-5 control-label">Logo Required</label>
                                                             <div class="col-sm-3">
                                                                    <asp:CheckBox ID="chkLogoReq" onclick="Logo();" ToolTip="Logo Required" runat="server"/>&nbsp;&nbsp;
                                                                     <span id="SpanLogoReq" runat="server" visible="true">
                                                                <asp:LinkButton ID="lnkUploadLogo" runat="server"  class="btn btn-sm btn-primary acc_home" ToolTip="" 
                                                                    data-toggle="modal"  data-target="#DivLogoUpload"><i class="fa fa-cloud-upload"></i></asp:LinkButton>
                                                                </span>
                                                                </div>
                                                                 <div class="col-sm-4" id="DivImag" runat="server">
                                                                <asp:Image ID="imgLogoShow" Width="100px" Height="75px" runat="server"></asp:Image>
                                                            </div>
														</div>
                                                        <div class="col-sm-2">
															<label class="col-sm-10 control-label">Type Del. Place</label>
															<div class="col-sm-2">
																<asp:CheckBox ID="chkDelPlace" runat="server" TabIndex="19" />
															</div>
                                                    </div>
                                                        <div class="clearfix"></div>                                                       
                                                      </div>                                                      
                                                    </div>
                                                    </div>
                                                    </section>
                                                    </div>	
                                                <div class="clearfix first_section">
	                                            <section class="panel panel-in-default">                                                 
	                                                <div class="panel-body">
	                                                <div class="clearfix estimate_second_row odd_row">
                                                      <div class="clearfix odd_row">
                                                        <div class="col-sm-3">
															<label class="col-sm-6 control-label">GR Print</label>
															<div class="col-sm-6">
																<asp:DropDownList ID="ddlGRPrint" runat="server" CssClass="form-control" TabIndex="21">
                                                                    <asp:ListItem Text="General" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Jain Bulk" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="Prem Sharda" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="Om Cargo" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Text="DH LOGISTICS " Value="6"></asp:ListItem>
                                                                    <asp:ListItem Text="Kajaria Logistics" Value="7"></asp:ListItem>
                                                                    <asp:ListItem Text="Westend Roadlines" Value="8"></asp:ListItem>
                                                                </asp:DropDownList> 
															</div>
														</div>
														<div class="col-sm-3">
															<label class="col-sm-6 control-label">Rate Editable</label>
															<div class="col-sm-3">
																<asp:CheckBox ID="chkGRRate" runat="server" TabIndex="16" />
															</div>
														</div>
														<div class="col-sm-3">
                                                        <label class="col-sm-11 control-label">GR Print Without Header</label>
															<div class="col-sm-1">
																<asp:CheckBox ID="chkheader" runat="server" TabIndex="20" />
															</div>
                                                      </div> 
                                                      <div class="col-sm-3">
                                                       <label class="col-sm-6 control-label">Item Grade Req</label>
															<div class="col-sm-3">
																<asp:CheckBox ID="chkGradeReq" runat="server" TabIndex="22" />
															</div>
                                                      </div>
                                                      <div class="clearfix"></div>
                                                         <div class="col-sm-3">
															<label class="col-sm-6 control-label">Surcharge(%)</label>
															<div class="col-sm-6">
															<asp:TextBox ID="txtSurchage" runat="server" AutoPostBack="true" CssClass="form-control"
                                                                Height="24px" MaxLength="5" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                                onpaste="return false" OnTextChanged="txtSurchage_TextChanged" 
                                                                TabIndex="7" Text="0.00"></asp:TextBox>
															</div>
														</div>
														<div class="col-sm-3">
															<label class="col-sm-6 control-label">Bilty Charge</label>
															<div class="col-sm-6">
															<asp:TextBox ID="txtBilty" runat="server" AutoPostBack="true" CssClass="form-control"
                                                                Height="24px" MaxLength="8" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                                onpaste="return false" OnTextChanged="txtBilty_TextChanged" TabIndex="8" Text="0.00"></asp:TextBox>
															</div>
														</div>
														<div class="col-sm-3">
                                                        <label class="col-sm-6 control-label">Wages Amount</label>
															<div class="col-sm-6">
															<asp:TextBox ID="txtWages" runat="server" AutoPostBack="true" CssClass="form-control"
                                                                Height="24px" MaxLength="8" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                                onpaste="return false" OnTextChanged="txtWages_TextChanged" 
                                                                TabIndex="9" Text="0.00"></asp:TextBox>                      
															</div>
                                                      </div> 
                                                      <div class="col-sm-3">
                                                      <label class="col-sm-6 control-label">Toll Tax</label>
															<div class="col-sm-6">
															<asp:TextBox ID="txtTollTax" runat="server" AutoPostBack="true" CssClass="form-control"
                                                                    Height="24px" MaxLength="8" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                                    onpaste="return false" OnTextChanged="txtTollTax_TextChanged" TabIndex="10" Text="0.00"></asp:TextBox>  
															</div>
                                                      </div>
                                                      <div class="clearfix"></div>
                                                        
                                                       <div class="col-sm-3">
															<label class="col-sm-6 control-label">Rename Wages</label>
															<div class="col-sm-6">
																<asp:TextBox ID="txtRenameWages" runat="server" CssClass="form-control" Height="24px"
                                                                    MaxLength="10" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                                    onpaste="return false" TabIndex="3" ></asp:TextBox>                            
															</div>
														</div>
														<div class="col-sm-3">
															<label class="col-sm-6 control-label">Rename PF</label>
															<div class="col-sm-6">
																<asp:TextBox ID="txtPFChanges" runat="server" CssClass="form-control" Height="24px"
                                                                    MaxLength="18" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                                    onpaste="return false" TabIndex="23" ></asp:TextBox>                            
															</div>
														</div>
														<div class="col-sm-3">
                                                        <label class="col-sm-6 control-label">Rename TollTax</label>
															<div class="col-sm-6">
																<asp:TextBox ID="txtTollRename" runat="server" CssClass="form-control" Height="24px"
                                                                    MaxLength="18" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                                    onpaste="return false" TabIndex="24" ></asp:TextBox>                            
															</div>
                                                      </div> 
                                                      <div class="col-sm-3">
                                                       <label class="col-sm-6 control-label">Rename Ref No.</label>
															<div class="col-sm-6">
																<asp:TextBox ID="txtrefrename" runat="server" CssClass="form-control" Height="24px"
                                                                    MaxLength="18" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                                    onpaste="return false" TabIndex="24" ></asp:TextBox>                            
															</div>
                                                      </div>
                                                        <div class="clearfix"></div>
                                                         <div class="col-sm-3">
															<label class="col-sm-6 control-label">Rename Cartage</label>
															<div class="col-sm-6">
																<asp:TextBox ID="txtCartageRename" runat="server" CssClass="form-control" Height="24px"
                                                                    MaxLength="10" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                                    onpaste="return false" TabIndex="3" ></asp:TextBox>                            
															</div>
														</div>
														<div class="col-sm-3">
															<label class="col-sm-6 control-label">Rename Commission</label>
															<div class="col-sm-6">
																<asp:TextBox ID="txtComRename" runat="server" CssClass="form-control" Height="24px"
                                                                    MaxLength="18" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                                    onpaste="return false" TabIndex="23" ></asp:TextBox>                            
															</div>
														</div>
														<div class="col-sm-3">
                                                        <label class="col-sm-6 control-label">Rename Bilty</label>
															<div class="col-sm-6">
																<asp:TextBox ID="txtBiltyRename" runat="server" CssClass="form-control" Height="24px"
                                                                    MaxLength="18" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                                    onpaste="return false" TabIndex="24" ></asp:TextBox>                            
															</div>
                                                      </div> 
                                                      <div class="col-sm-3">
                                                        <label class="col-sm-6 control-label">Rename S.T.Charges</label>
															<div class="col-sm-6">
																<asp:TextBox ID="txtStCharge" runat="server" CssClass="form-control" Height="24px"
                                                                    MaxLength="18" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                                    onpaste="return false" TabIndex="24" ></asp:TextBox>                            
															</div>
                                                      </div>
                                                        
                                                      <div class="clearfix">
                                                      <div class="col-sm-3">
                                                        <label class="col-sm-11 control-label">GR Retailer Required</label>
															<div class="col-sm-1">
																<asp:CheckBox ID="chkRateReq" runat="server" TabIndex="21" />
															</div>
                                                      </div>
                                                          <div class="col-sm-3">
                                                        <label class="col-sm-11 control-label">GST Cal. GR </label>
															<div class="col-sm-1">
																<asp:CheckBox ID="ChkGstCalGr"  runat="server" TabIndex="22" />
															</div>
                                                      </div>
                                                      </div>                                                     
                                                      </div>                                                      
                                                    </div>
                                                    </div>
                                                    </section>
                                                    </div>
                                                <div class="clearfix first_section">
	                                            <section class="panel panel-in-default">                                                 
	                                                <div class="panel-body">
	                                                <div class="clearfix estimate_second_row odd_row">
                                                      <div class="clearfix even_row">
                                                      <div class="col-sm-12">
                                                        <div class="col-sm-3">
															<label class="col-sm-6 control-label">Challan Print</label>
															<div class="col-sm-6">
																<asp:DropDownList ID="ddlChallanPrint" runat="server" CssClass="form-control" TabIndex="5">
                                                                    <asp:ListItem Text="Only To Pay" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="All GR" Value="2"></asp:ListItem>
                                                                </asp:DropDownList>
															</div>
														</div>
                                                        
														<div class="col-sm-3">
															<label class="col-sm-6 control-label">TDS Editable</label>
															<div class="col-sm-6">
																<asp:CheckBox ID="chkTDS" runat="server" TabIndex="15" />
															</div>
														</div>
														<div class="col-sm-3">
                                                       <label class="col-sm-11 control-label">Excel Upload(Challan)</label>
															<div class="col-sm-1">
																<asp:CheckBox ID="chkChlnUpload" runat="server" TabIndex="17" />
															</div>
                                                      </div> 
                                                      <div class="col-sm-3">
                                                       <label class="col-sm-11 control-label">Amount Without GST</label>
															<div class="col-sm-1">
																<asp:CheckBox ID="ChkWithoutGst" runat="server" TabIndex="17" />
															</div>
                                                      </div>
                                                      </div>
                                                      <div class="col-sm-3">
                                                            <div class="col-sm-6">
                                                                <b>Less Challan Amnt(Invoice)</b>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <asp:CheckBox ID="chkRequireToPayAdvance" runat="server"></asp:CheckBox>
                                                            </div>
                                                        </div>
                                                      <div class="clearfix"></div>                                                                                                       
                                                      </div>                                                      
                                                    </div>
                                                    </div>
                                                    </section>
                                                    </div>
                                                <div class="clearfix first_section">
	                                            <section class="panel panel-in-default">                                                 
	                                                <div class="panel-body">
	                                                <div class="clearfix estimate_second_row odd_row">
                                                      <div class="clearfix odd_row">
                                                        <div class="col-sm-3">
															<label class="col-sm-6 control-label">Invoie Print</label>
															<div class="col-sm-6">
																<asp:DropDownList ID="ddlInvoicePrt" runat="server" CssClass="form-control" TabIndex="4">
                                                                    <asp:ListItem Text="General" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Arawali" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="Daulat" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="Jain Bulk" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Text="OM Cargo" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="Westend Roadlines" Value="6"></asp:ListItem>
                                                                    <asp:ListItem Text="Shiv Shakti Roadlines" Value="7"></asp:ListItem>
                                                                </asp:DropDownList>   
															</div>
														</div>
														<div class="col-sm-6">
																<label class="col-sm-3 control-label">Admin Appr.</label>
															<div class="col-sm-2">
																<asp:CheckBox ID="chkAdminApp" runat="server" TabIndex="19" />
															</div>
														</div>
														<div class="col-sm-3">
                                                        <label class="col-sm-5 control-label" style="visibility:hidden">Serv. Tax(%)</label>
															<div class="col-sm-7">
																<asp:TextBox ID="txtServTax" runat="server" Visible="false" AutoPostBack="true" CssClass="form-control"
                                                                    Height="24px" MaxLength="5" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                                    onpaste="return false" OnTextChanged="txtServTax_TextChanged" TabIndex="6" Text="0.00"></asp:TextBox>
															</div>                                                    
                                                      </div>                                                       
                                                      <div class="clearfix"></div>                                                                                                      
                                                      </div>                                                      
                                                    </div>
                                                    </div>
                                                    </section>
                                                    </div>
                                                <div class="clearfix first_section">
	                                            <section class="panel panel-in-default">                                                 
	                                                <div class="panel-body">
	                                                <div class="clearfix estimate_second_row odd_row">
                                                      <div class="clearfix even_row">
                                                       <div class="col-sm-3">
															<label class="col-sm-6 control-label">Pay Agst Chln Print</label>
															<div class="col-sm-6">
																<asp:DropDownList ID="ddlPayChln" runat="server" CssClass="form-control" TabIndex="18">
                                                                    <asp:ListItem Text="General" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Daulat" Value="2"></asp:ListItem>
                                                                </asp:DropDownList>   
															</div>
														</div>
													  <div class="col-sm-3">
															<label class="col-sm-6 control-label">Container Wise Rate</label>
															<div class="col-sm-3">
																<asp:CheckBox ID="chkContRate" runat="server" TabIndex="17" />
															</div>
														</div>
													  <div class="col-sm-3">
                                                       <label class="col-sm-11 control-label">Shrtg Rate Editable in Chln Confirm.</label>
															<div class="col-sm-1">
																<asp:CheckBox ID="chkShrtgEdit" runat="server" TabIndex="18" />
															</div>
                                                      </div> 
                                                      <div class="col-sm-3">
                                                       <label class="col-sm-7 control-label" style="font">Counter SBill Req.</label>
                                                                 <div class="col-sm-3" style="width:3%; vertical-align:top">
																    <asp:CheckBox ID="chkCntrSBillReq" ToolTip="Sale Bill Save Only in Matrial Issue Condition (Counter Case Disable)" runat="server"/>
															    </div>
                                                      </div>
                                                      <div class="clearfix"></div>
                                                      <div class="col-sm-3">
                                                      <label class="col-sm-12 control-label">Term&Condition1</label>
                                                      </div>
                                                      <div class="col-sm-3">
                                                      <label class="col-sm-12 control-label">Term&Condition2</label>
                                                      </div>
                                                      <div class="col-sm-3">
                                                        <label class="col-sm-12 control-label">Term&Con(Lorry Hire Slip)</label>
                                                      </div> 
                                                       <div class="col-sm-3">
                                                        <label class="col-sm-12 control-label">Term&Con(GR Retailer)</label>
                                                      </div>                                                                                                      
                                                      <div class="clearfix"></div>
                                                      <div class="col-sm-3">                                                      
                                                      <div class="col-sm-10">
                                                            <asp:TextBox ID="txtterms" runat="server"  TextMode="MultiLine" CssClass="form-control" MaxLength="800" Style="width: 400px; resize:none; height:88px;"  
                                                                    rows="5"  TabIndex="24" ></asp:TextBox> 
                                                            </div>
                                                      </div>                                                      
                                                      <div class="col-sm-3">
                                                          <div class="col-sm-10">
                                                            <asp:TextBox ID="txtterms2" runat="server"  TextMode="MultiLine" CssClass="form-control" MaxLength="800" Style="width: 400px; resize:none; height:88px;"  
                                                                    rows="5" 
                                                                    TabIndex="24" ></asp:TextBox>
                                                            </div></div>
                                                      <div class="col-sm-3">
                                                     <div class="col-sm-10">
                                                            <asp:TextBox ID="txttermshireslip" runat="server"  TextMode="MultiLine" CssClass="form-control" MaxLength="800" Style="width: 400px; resize:none; height:88px;"  
                                                                    rows="5"
                                                                    TabIndex="24" ></asp:TextBox>
                                                            </div></div>
                                                             <div class="col-sm-3">
                                                     <div class="col-sm-10">
                                                            <asp:TextBox ID="txtGRRetTnC" runat="server"  TextMode="MultiLine" CssClass="form-control" MaxLength="800" Style="width: 400px; resize:none; height:88px;"  
                                                                    rows="5"
                                                                    TabIndex="24" ></asp:TextBox>
                                                            </div></div>
                                                      <div class="clearfix"></div>
                                                      </div>                                                      
                                                    </div>
                                                    </div>
                                                    </section>
                                                    </div>                                                                  
												</div>
											</section>                        
										</div>
										<!-- second  section -->
										<div class="clearfix fourth_right">
											<section class="panel panel-in-default btns_without_border">                            
												<div class="panel-body">     
													<div class="clearfix even_row">
														<div class="col-lg-2"></div>
														<div class="col-lg-7">  
                                                            <div class="col-sm-4">                                                         
                                                                <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-info" Visible="false" TabIndex="19" OnClick="lnkbtnNew_OnClick" ><i class="fa fa-file-o"></i>New</asp:LinkButton>
															</div>                                
															<div class="col-sm-4">
																<asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" ValidationGroup="Save" CssClass="btn full_width_btn btn-s-md btn-success" TabIndex="17" OnClick="lnkbtnSave_OnClick" ><i class="fa fa-save"></i>Save</asp:LinkButton>                      
															</div>
															<div class="col-sm-4">
														        <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-danger" TabIndex="18" OnClick="lnkbtnCancel_OnClick" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
															</div>
														</div>
														<div class="col-lg-3" style="visibility:hidden;">                                                        
                                                        <label class="col-sm-6 control-label" style="font">Update VAT</label>
                                                        <div class="col-sm-3" style="vertical-align:top">
															    <asp:LinkButton ID="lnkbtnUpdate" runat="server"  class="btn btn-sm btn-primary acc_home" ToolTip="Update VAT and CST By Group Wise" 
                                                                    data-toggle="modal"  data-target="#dvitmgrp"><img src="Images/plus.gif" style="width:15px;" /></asp:LinkButton>
                                                                    <div id="dvitmgrp" class="modal fade">
                                                                       <div class="modal-dialog"   style="width:30%">
                                                                          <div class="modal-content">
                                                                          <div class="modal-header">
                                                                            <h4 class="popform_header">
                                                                                UPDATE VAT/CST :
                                                                            </h4>
                                                                          </div>
                                                                          <div class="modal-body">
                                                                        <section class="panel panel-default full_form_container material_search_pop_form">
				                                                         <div class="panel-body">
						                                                    <div class="clearfix odd_row  ">
							                                                    <label class="col-sm-3 control-label">Group Type <span class="required-field">*</span></label>
							                                                    <div class="col-sm-9">
                                                                                    <asp:DropDownList ID="ddlItemGropForPopup" CssClass="form-control" runat="server"></asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="rfvItemGropForPopup" runat="server"
                                                                                             ControlToValidate="ddlItemGropForPopup" InitialValue="0" CssClass="classValidation" Display="Dynamic" 
                                                                                           ErrorMessage="Select Group Type!" SetFocusOnError="true" ValidationGroup="DIVSave"></asp:RequiredFieldValidator>
							                                                    </div>
						                                                    </div>	
                                                                            <div class="clearfix odd_row  ">
							                                                    <label class="col-sm-3 control-label">Type <span class="required-field">*</span></label>
							                                                    <div class="col-sm-9">
                                                                                    <asp:DropDownList ID="ddlTaxType" CssClass="form-control" runat="server">
                                                                                        <asp:ListItem Text="VAT" Value="1"></asp:ListItem>
                                                                                        <asp:ListItem Text="CST" Value="2"></asp:ListItem>
                                                                                    </asp:DropDownList>
							                                                    </div>
						                                                    </div> 
						                                                    <div class="clearfix odd_row  ">
							                                                    <label class="col-sm-3 control-label">Percentage <span class="required-field">*</span></label>
							                                                    <div class="col-sm-9">
                                                                                    <asp:TextBox ID="txtPercentage" runat="server" CssClass="form-control" MaxLength="50" TabIndex="2" ></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="refPercentage" runat="server" ControlToValidate="txtPercentage" 
                                                                                        CssClass="classValidation" Display="Dynamic" ErrorMessage="Percentage Required!" 
                                                                                        SetFocusOnError="true" ValidationGroup="DIVSave"></asp:RequiredFieldValidator>
									                 
							                                                    </div>
						                                                    </div>	                        
                                                                           </div>
				                                                        </section>
                                                                       </div>
                                                                          <div class="modal-footer">
                                                                        <div class="popup_footer_btn">
                                                                        <div class="col-lg-12">
                                    
                                                                        <div class="col-lg-3">
                                                                        </div>
								                                            <div class="col-sm-3"> 
                                                                            <asp:LinkButton ID="lnkbtnSaveGroup"  runat="server" Text="Save" CssClass="btn btn-dark" OnClick="lnkbtnSaveGroup_OnClick"  CausesValidation="true" ValidationGroup="DIVSave" >
                                                                            </asp:LinkButton>
 
                                                                            </div>
                                                                        <div class="col-sm-3">
                                                                            <button type="submit" tabindex="36" class="btn btn-dark" data-dismiss="modal">
                                                                            <i class="fa fa-times"></i>Close</button>
							                                            </div>
                                                                        </div>
                                                                        </div>
                                                                    </div>
                                                                          </div>
                                                                       </div>
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
            <div class="col-lg-1">
                <asp:HiddenField ID="hidUserPrefidno" runat="server" />
            </div>
        </div>
    </div>
    <div id="DivLogoUpload" class="modal fade">
        <div class="modal-dialog" style="width: 40%">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="popform_header">Logo Upload
                    </h4>
                </div>
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
				                        <div class="panel-body">
                                        <div class="col-lg-12">
                                        <div class="col-sm-8">
                                        <asp:FileUpload ID="flupPic" onchange="readURL(this);" runat="server" />
                                            <br />
                                            <asp:LinkButton ID="lnkbtnUpload" OnClick="lnkbtnUpload_Click" runat="server" Text="Upload" CssClass="btn btn-dark"  CausesValidation="true" ></asp:LinkButton>
                                        </div> 
                                        
                                        <div class="col-lg-4">
                                        <img id="blah" src="" alt=""/>
                                        </div> 
                                        <div class="col-sm-12">
                                            <span class="required-field"><asp:Label ID="lblError" runat="server"></asp:Label></span>
                                        </div>
                                        </div>
                                                      
                                        </div>
                                        
				                    </section>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidToggleWeightWiseRate" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hideimgvalue" runat="server" />
    <asp:HiddenField ID="Hidebytes" runat="server" />
    <script language="javascript" type="text/javascript">

        function Logo() {
            var check = document.getElementById("<%=chkLogoReq.ClientID%>");
            var result = document.getElementById("<%=hideimgvalue.ClientID %>").value;
            if (check.checked) {
                document.getElementById("<%=SpanLogoReq.ClientID %>").style.visibility = "visible";
                document.getElementById("<%=DivImag.ClientID %>").style.visibility = "visible";
                document.getElementById("<%=imgLogoShow.ClientID %>").setAttribute("src", result);
            }
            else {
                document.getElementById("<%=hideimgvalue.ClientID %>").value = "";
                document.getElementById("<%=SpanLogoReq.ClientID %>").style.visibility = "hidden";
                document.getElementById("<%=DivImag.ClientID %>").style.visibility = "hidden";
                document.getElementById("<%=imgLogoShow.ClientID %>").setAttribute("src", result);
            }
        }

        function hide() {
            var result1 = document.getElementById("<%=hideimgvalue.ClientID %>").value;
            document.getElementById("<%=SpanLogoReq.ClientID %>").style.visibility = "hidden";
            document.getElementById("<%=imgLogoShow.ClientID %>").setAttribute("src", result1);
            document.getElementById("<%=DivImag.ClientID %>").style.visibility = "hidden";
        }

        function ValidateExtension() {
            var allowedFiles = [".jpg", ".jpeg", ".png"];
            var fileUpload = document.getElementById("<%=flupPic.ClientID%>");
            var lblError = document.getElementById("<%=lblError.ClientID%>");
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            if (!regex.test(fileUpload.value.toLowerCase())) {
                lblError.innerHTML = "*** Please upload files having extensions: " + allowedFiles.join(', ') + " only.";
                return false;
            }
            else {
                lblError.innerHTML = "";
                return true;
            }
        }

        function readURL(input) {

            var fileUpload = document.getElementById("<%=flupPic.ClientID%>");
            if (ValidateExtension()) {
                if (input.files && input.files[0]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $('#blah')
                    .attr('src', e.target.result)
                    .width(140)
                    .height(90);
                    };
                    reader.readAsDataURL(input.files[0]);
                }
            }
        }
        //WeightWiseRateActive toggle function
        $(document).ready(function () {
            ToggleWeightWiseRate();
        });
        function ToggleWeightWiseRate() {
            if ($('#hidToggleWeightWiseRate').val() == "0") {
                $('#hidToggleWeightWiseRate').val('1');
                //$('#chkLessChlnAmntInv').attr('checked', true);
                $('#chkLessChlnAmntInv').removeAttr("disabled");
            }
            else {
                $('#hidToggleWeightWiseRate').val('0');
                $('#chkLessChlnAmntInv').attr('checked', false);
                $('#chkLessChlnAmntInv').attr("disabled", "disabled");
            }
            
        }
    </script>
</asp:Content>
