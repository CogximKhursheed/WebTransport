<%@ Page Title="Ledger Accounts" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="LedgerMaster.aspx.cs" Inherits="WebTransport.LedgerMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-10">
            <section class="panel panel-default full_form_container ledger_form">
                <header class="panel-heading font-bold form_heading">LEDGER ACCOUNTS
                  <span class="view_print"><a href="ManageLedgerAccount.aspx"><asp:Label ID="lblViewList" runat="server" Text="LIST" TabIndex="25"></asp:Label></a>
               
                  </span>
                </header>
                <div class="panel-body">
                  <form class="bs-example form-horizontal">
                    <div class="clearfix">
						        	<div class="col-lg-6">
						            <!-- first left row -->
						            <div class="clearfix first_left">
						              <section class="panel panel-in-default">
						                <header class="panel-heading">
						                  <span class="h5">PARTY DETAILS</span>
						                </header>
						                <div class="panel-body">
						                  <div class="clearfix odd_row  ">
	                              	<label class="col-sm-5 control-label" style="width: 36%;">Party Name <span class="required-field">*</span></label>
	                              	<div class="col-sm-2" style="width: 20%;">
                                     <asp:DropDownList ID="ddlTitle" runat="server" CssClass="form-control"  TabIndex="1">
                                     </asp:DropDownList>
	                              	</div>
	                                <div class="col-sm-5" style="width: 44%;">
                                    <asp:TextBox ID="txtAccountPrtyName" runat="server" PlaceHolder="Party Name" CssClass="form-control"  TabIndex="2" OnTextChanged="txtAccountPrtyName_TextChanged"></asp:TextBox>
                                                         
	                                </div>
                                    <div class="col-sm-4" style="width: 37%;"></div>
                                    <div class="col-sm-5" style="width: 62%;">
                                    <asp:RequiredFieldValidator ID="rfvAcntPrtyNm" runat="server" ControlToValidate="txtAccountPrtyName"
                                        ValidationGroup="Save" ErrorMessage="Please enter Party Name " CssClass="classValidation"
                                        SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>	         
                                    </div>
	                            </div>
						            <div class="clearfix even_row">
	                              	    <label class="col-sm-5 control-label" style="width: 36%;">Party Name[Hindi] </label>
	                                    <div class="col-sm-7" style="width: 64%;">
                                          <asp:TextBox ID="txtAccountPrtyNameHindi" PlaceHolder="Party Name (Hindi)" runat="server" CssClass="form-control" TabIndex="3" OnTextChanged="txtAccountPrtyName_TextChanged"></asp:TextBox>
	                               
	                                    </div>
	                                </div>
                                       <div class="clearfix even_row">
	                              	    <label class="col-sm-5 control-label" style="width: 36%;">Short Name</label>
	                                    <div class="col-sm-7" style="width: 64%;">
                                          <asp:TextBox ID="txtShortName" PlaceHolder="Short Name" runat="server" MaxLength="30" CssClass="form-control" TabIndex="3"></asp:TextBox>
	                               
	                                    </div>
	                                </div>
	                                    <div class="clearfix odd_row">
                                        <label class="col-sm-5 control-label" style="width: 36%;">Account Type<span class="required-field">*</span></label>
                                        <div class="col-sm-6" style="width: 50%;">
                                          <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="form-control" TabIndex="4" AutoPostBack="False" onchange="javascript:OnChangeDriver();">
                                         </asp:DropDownList>                               
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAccountType"
                                            ValidationGroup="Save" ErrorMessage="Please Select Account Type"
                                            CssClass="classValidation" SetFocusOnError="true" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>                                                        
                                        </div> 
                                        <div class="col-sm-1" id="DivbtnDriver" visible="true" runat="server" style="width: 7%;">
                                             <asp:ImageButton ID="imgbtnDriver" runat="server" ImageUrl="~/Images/Prty_Add1.png"
                                                Enabled="false" AlternateText="Add" ToolTip="Driver Detail" OnClick="imgbtnDriver_OnClick" />
                                        </div>
                                        <div class="col-sm-1" id="DivlnkClaimDetails" visible="false" runat="server" style="width: 7%;">
                                            <asp:LinkButton ID="lnkClaimDetails" onchange=""  runat="server" class="btn-sm btn btn-primary acc_home"><i class="fa fa-dot-circle-o"></i></asp:LinkButton>
                                        </div>
                                      </div>

                              <div class="clearfix even_row">
                                <label class="col-sm-5 control-label" style="width: 36%;">AccountSubGroup<span class="required-field">*</span></label>
                                <div class="col-sm-7" style="width: 64%;">
                                  <asp:DropDownList ID="ddlAccountSubGroup" runat="server" CssClass="form-control"  TabIndex="5" onchange="javascript:onChangeAccSubGrp();">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RfvAcntSubgrp" runat="server" ControlToValidate="ddlAccountSubGroup"
                                        Display="Dynamic" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Please Select Account Sub Group"
                                        CssClass="classValidation" InitialValue="0"></asp:RequiredFieldValidator>
                                                          
                                </div> 
                              </div>
                                 <div class="clearfix odd_row">
                                <label class="col-sm-5 control-label" style="width: 36%;">Date Range<span class="required-field">*</span></label>
                                <div class="col-sm-7" style="width: 64%;">
                                   <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control" TabIndex="6">
                                    </asp:DropDownList>
                                                          
                                </div> 
                              </div>
                              <div class="clearfix even_row">
	                              	<label class="col-sm-5 control-label" style="width: 36%;">Opening Balance </label>
	                                <div class="col-sm-3" style="width: 23%; text-align: right;">
                                    <asp:TextBox ID="txtOpBal" runat="server" CssClass="form-control" Style="text-align: right;" Text="0.0"  MaxLength="14" onKeyPress="return checkfloat(event, this);"
                                        TabIndex="7"></asp:TextBox>	                                   
	                                </div>
	                                <div class="col-sm-4" style="width: 41%;">
	                                	<label class="col-sm-5 control-label" style="width: 50%;">Bal. Type</label>
		                                <div class="col-sm-6" style="width: 50%;">
                                         <asp:DropDownList ID="ddlBalanceType" runat="server" CssClass="form-control" TabIndex="8">
                                            <asp:ListItem Text="Cr" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Dr" Value="2"></asp:ListItem>
                                        </asp:DropDownList>                        
		                                </div> 
	                                </div>
	                            </div>
	                            <div class="clearfix odd_row">
	                              	<label class="col-sm-5 control-label" style="width: 36%;">Agent Comission </label>
	                                <div class="col-sm-3" style="width: 23%;">
                                     <asp:TextBox ID="txtagntCommision" runat="server" PlaceHolder="0.00" CssClass="form-control" MaxLength="14"  onKeyPress="return checkfloat(event, this);" Style="text-align: right;"  TabIndex="9" Text="0.0" ></asp:TextBox>
	                                    
	                                </div>
	                                <div class="col-sm-4" style="width: 41%;">
	                               		<label class="col-sm-10 control-label" style="width: 89%;">Servi.Tax Exempt</label>
                                    <asp:CheckBox ID="chkServExmpt" CssClass="col-sm-2" TabIndex="10" style="width: 11%;" runat="server"  />                                    
                                  </div>
	                            </div>

                                <div class="clearfix even_row">
                                <label class="col-sm-5 control-label" style="width: 36%;">Company Name<span id="SpanMandComp" runat="server" class="required-field">*</span></label>
                                <div class="col-sm-7" style="width: 57%;">
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control"  TabIndex="11">
                                </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvpetroCompany" runat="server" ControlToValidate="ddlCompany"
                                Display="Dynamic" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Please Select Petrol Company"
                                CssClass="classValidation" InitialValue="0"></asp:RequiredFieldValidator>
                                                          
                                </div> 
                                <div class="col-sm-1" style="width: 7%;">
                                  <span id="SpanCompanyRefresh" runat="server" visible="true">
                                    <asp:LinkButton ID="lnkBtnCompany" OnClick="lnkBtnCompany_Click" runat="server"  class="btn-sm btn btn-primary acc_home"><i class="fa fa-refresh"></i></asp:LinkButton>
                                  </span>
                                </div>
                              </div>
                              <div class="clearfix even_row">
                                <label class="col-sm-5 control-label" style="width: 36%;">Principal Company <span id="Span1" runat="server" class="required-field">*</span></label>
                                <div class="col-sm-7" style="width: 57%;">
                                    <asp:DropDownList ID="ddlPrincComp" runat="server" CssClass="form-control"  TabIndex="12">
                                    </asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="rvfPrincComp" runat="server" ControlToValidate="ddlPrincComp"
                                Display="Dynamic" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Please Select Company"
                                CssClass="classValidation" InitialValue="0"></asp:RequiredFieldValidator>       
                                </div> 
                              </div>
                                   <div id="DivDetenation"  runat="server" class="clearfix odd_row">
	                              	<label class="col-sm-5 control-label" style="width: 37%;">Detention Plant Charges </label>
	                                <div class="col-sm-3" style="width: 18%; text-align: right;">
                                    <asp:TextBox ID="txtdetenPlantchrg" runat="server" CssClass="form-control" Style="text-align: right;" Text="0.0"  MaxLength="14" onKeyPress="return checkfloat(event, this);"
                                        TabIndex="7"></asp:TextBox>	                                   
	                                </div>
	                                <div class="col-sm-4" style="width: 45%;">
                                    <label class="col-sm-5 control-label" style="width: 47%;">Port Charges </label>
	                                <div class="col-sm-3" style="width: 43%; text-align: right;">
                                    <asp:TextBox ID="txtdetenPortchrg" ToolTip="Detention Port Chrages" runat="server" CssClass="form-control" Style="text-align: right;" Text="0.0"  MaxLength="14" onKeyPress="return checkfloat(event, this);"
                                        TabIndex="7"></asp:TextBox>	                                   
	                                </div>
	                                </div>
	                            </div>
                                  <div id="DivContainer"  runat="server" class="clearfix even_row">
                                    	<label class="col-sm-5 control-label" style="width: 36%;">Container Charges </label>
	                                <div class="col-sm-3" style="width: 19%; text-align: right;">
                                    <asp:TextBox ID="txtcontainerchrg" runat="server" CssClass="form-control" Style="text-align: right;" Text="0.0"  MaxLength="14" onKeyPress="return checkfloat(event, this);"
                                        TabIndex="7"></asp:TextBox>	                                   
	                                </div>
                                    </div>
                                <div class="clearfix odd_row">
	                              
                              <div class="col-sm-4" style="width: 20%;">
	                               		<label class="col-sm-10 control-label" style="width: 79%;">Active</label>
                                         <div class="col-sm-1" style="margin-top:-5px;">
                                         <asp:CheckBox ID="chkStatus" CssClass="col-sm-2" style="width: 21%;" runat="server" Checked="True" TabIndex="13" />
                                         </div>
                                  
                                  </div>
                                  <div class="col-sm-6">
                                  <asp:LinkButton ID="lnkDocHolder" runat="server" 
                                      CssClass="btn full_width_btn btn-sm btn-primary" data-toggle="modal" 
                                      data-target="#document_holder_popup" TabIndex="26" ><i class="fa fa-upload"></i>Document</asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2">
                                   <div class="col-sm-2"> 
                              <asp:Label ID="TotalDocumentAdd" runat="server" CssClass="col-sm-8 control-label"  Text="0"></asp:Label></div>
                            </div>
                                  </div>
						                </div>
						              </section>
						            </div>                        
						             

						          </div>
						          <!-- second main Column -->
						          <div class="col-lg-6">
						            <!-- first_one left row -->
						            <div class="clearfix first_one_left">
						              <section class="panel panel-in-default">
						                <header class="panel-heading">
						                  <span class="h5">COMMUNICATION DETAILS</span>
						                </header>
						                <div class="panel-body">
						                  <div class="clearfix odd_row  ">
	                              	<label class="col-sm-5 control-label" style="width: 22%;">Cont. Person</label>
	                                <div class="col-sm-5" style="width: 78%;">
                                     <asp:TextBox ID="txtcontPrsn" runat="server" PlaceHolder="Contact Person Name" CssClass="form-control" MaxLength="100" TabIndex="13" ></asp:TextBox>	                                   
	                                </div>
	                            </div>

						                  <div class="clearfix even_row">
	                              	<label class="col-sm-5 control-label" style="width: 22%;">Cont.Mobile </label>
	                                <div class="col-sm-7" style="width: 25%;">
	                                    <asp:TextBox ID="txtContMob" runat="server" Placeholder="Start 7,8,9" CssClass="form-control"  MaxLength="10" TabIndex="14"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="rfvContMob" runat="server" ControlToValidate="txtContMob"
                                        SetFocusOnError="true" ValidationGroup="user" ErrorMessage="Invalid Mobile No"
                                        CssClass="classValidation" Display="Dynamic"  ValidationExpression="^[7-9][0-9]{9}$"></asp:RegularExpressionValidator>                      
	                                </div>
	                                <div class="col-sm-4" style="width: 53%;">
	                                	<label class="col-sm-5 control-label" style="width: 30%;">Category</label>
	                                <div class="col-sm-3" style="width: 70%; text-align: right;">
                                     <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" TabIndex="15">
                                        </asp:DropDownList>
	                                </div>
	                                </div>
	                            </div>
	                            <div class="clearfix odd_row">
                               <label class="col-sm-5 control-label" style="width: 22%;">Email</label>
		                        <div class="col-sm-6" style="width: 78%;">
		                            <asp:TextBox ID="txtContEmail" runat="server" PlaceHolder="Email Id" CssClass="form-control"  MaxLength="30" TabIndex="16"></asp:TextBox>
		                        </div> 
                              </div>
                             
                              <div class="clearfix even_row">
                                 <label class="col-sm-5 control-label" style="width: 22%;">Address 1</label>
                                <div class="col-sm-6" style="width: 78%;">
                                  <asp:TextBox ID="txtAddress1" runat="server" PlaceHolder="First Address Line" onkeypress="return checkForComma();" CssClass="form-control"  MaxLength="100" TabIndex="17"></asp:TextBox>
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Display="Dynamic"  ControlToValidate="txtAddress1" ValidationExpression="^[A-Za-z0-9\s]*$" runat="server" ErrorMessage="Comma not Allowed"></asp:RegularExpressionValidator>
                                </div> 
                              </div>
                               <div class="clearfix odd_row">
                               <label class="col-sm-5 control-label" style="width: 22%;">Address 2</label>
                                <div class="col-sm-7" style="width: 78%;"> 
                                   <asp:TextBox ID="txtAddress2" runat="server" PlaceHolder="Second Address Line" CssClass="form-control" MaxLength="100" TabIndex="18"></asp:TextBox>
                                </div>
                              </div>
                               <div class="clearfix even_row">
	                              <label class="col-sm-5 control-label" style="width: 22%;">State<span class="required-field">*</span></label>
                                <div class="col-sm-7" style="width: 78%;">
                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" TabIndex="19" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="ddlState"
                                        CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select state" InitialValue="0" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </div> 
	                            </div>
                              <div class="clearfix odd_row">
	                              	<label class="col-sm-5 control-label" style="width: 22%;">City <span class="required-field">*</span></label>
	                                <div class="col-sm-3" style="width: 35%;">
                                     <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" TabIndex="20">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity"
                                            CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select City"
                                            InitialValue="0" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
	                                
	                                </div>
                                    <div class="col-sm-1" style="width: 7%;">
                                      <span id="SpanCityRefresh" runat="server" visible="true">
                                        <asp:LinkButton ID="lnkBtnCity" OnClick="lnkBtnCity_Click" runat="server" class="btn-sm btn btn-primary acc_home"><i class="fa fa-refresh"></i></asp:LinkButton>
                                      </span>
                                  </div>
                                  <div class="col-sm-3" style="width: 30%;">
                                     <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" TabIndex="20">
                                        </asp:DropDownList>
	                                </div>
                                    <div class="col-sm-1" style="width: 5%;">
                                      <span id="SpanDistrictrefresh" runat="server" visible="true">
                                        <asp:LinkButton ID="linkbtn" OnClick="lnkBtnDistrict_Click" runat="server" class="btn-sm btn btn-primary acc_home"><i class="fa fa-refresh"></i></asp:LinkButton>
                                      </span>
                                  </div>
	                                
	                            </div>
	                            <div class="clearfix even_row">
	                              <label class="col-sm-5 control-label" style="width: 22%;">Pin Code </label>
	                                <div class="col-sm-3" style="width: 25%; text-align: right;">
                                      <asp:TextBox ID="txtPinCode" runat="server" PlaceHolder="PinCode" CssClass="form-control" MaxLength="6"  TabIndex="21"></asp:TextBox>
	                                </div>
	                                <div class="col-sm-4" style="width: 53%;">
	                                	<label class="col-sm-5 control-label" style="width: 30%;">Fax No</label>
		                                <div class="col-sm-6" style="width: 70%;">
                                          <asp:TextBox ID="txtFax" runat="server" PlaceHolder="Fax Number" CssClass="form-control" MaxLength="10" TabIndex="22"></asp:TextBox>
		                                </div> 
	                                </div>
	                            </div>
                                 <div class="clearfix odd_row">
                                 	
	                                <div class="col-sm-4" style="width: 47%;">
	                                	<label class="col-sm-5 control-label" style="width: 48%;">Tin No</label>
		                                <div class="col-sm-6" style="width: 52%;">
                                           <asp:TextBox ID="txtTin" runat="server" PlaceHolder="Tin Number" CssClass="form-control" MaxLength="15" TabIndex="23"></asp:TextBox>
		                                </div> 
	                                </div>
                                     <div class="col-sm-4" style="width: 53%;">
	                                	<label class="col-sm-5 control-label" style="width: 31%;">PAN No</label>
		                                <div class="col-sm-6" style="width: 68%;">
                                           <asp:TextBox ID="txtPanNo" runat="server" PlaceHolder="Pan Number" CssClass="form-control" MaxLength="10" TabIndex="24"></asp:TextBox>
		                                </div> 
	                                </div>
                                 </div>
                                   <div class="clearfix even_row" id="GST" runat="server">
                               <label class="col-sm-3 control-label" style="width: 22%;">GST No.</label>
		                        <div class="col-sm-6" STYLE="width: 77%;">
		                            <asp:TextBox ID="txtGST" runat="server"  PlaceHolder="GST No" CssClass="form-control validation-input" MaxLength="16" ClientIDMode="Static"></asp:TextBox>
                                    <span class="validation-tip">Please enter value between 15 - 16 characters.</span>
		                        </div> 
                              </div>
                                 <div class="clearfix even_row" id="DivAccDetl" runat="server">
                               <label class="col-sm-5 control-label" style="width: 22%;">Account No.</label>
		                        <div class="col-sm-6" style="width: 68%;">
		                            <asp:TextBox ID="txtAccount" runat="server" onkeypress="keyEvent()" PlaceHolder="Account No" CssClass="form-control"  MaxLength="30" TabIndex="16"></asp:TextBox>
		                        </div> 
                                 <div id="Div1" class="col-sm-1" runat="server" style="width: 7%;">
                                       <a class="btn-sm btn btn-primary acc_home" onclick="openAccountDiv()"><i class="fa fa-home"></i></a>
                                        </div>
                              </div>
                              <div class="clearfix even_row">
                               <label class="col-sm-5 control-label" style="width: 22%;">C.S.T. No.</label>
                               <div class="col-sm-6" style="width: 68%;">
		                            <asp:TextBox ID="txtcstNo" runat="server" onkeypress="keyEvent()" PlaceHolder="C.S.T No." CssClass="form-control"  MaxLength="30" TabIndex="16"></asp:TextBox>
		                        </div> 
                              
                              </div>
                              <div class="clearfix even_row">
                               <label class="col-sm-5 control-label" style="width: 22%;">L.S.T. No.</label>
                               <div class="col-sm-6" style="width: 68%;">
		                            <asp:TextBox ID="txtLSTNo" runat="server" onkeypress="keyEvent()" PlaceHolder="L.S.T. No." CssClass="form-control"  MaxLength="30" TabIndex="16"></asp:TextBox>
		                        </div> 
                              
                              </div>
						                </div>
						              </section>
						            </div>                        

						          </div>

				        		</div>

                     <!-- fourth row -->
                    <div class="clearfix fourth_right">
                      <section class="panel panel-in-default btns_without_border">                            
                        <div class="panel-body">     
                          <div class="clearfix odd_row">
                            <div class="col-lg-2"></div>
                            <div class="col-lg-8">
                             <div class="col-sm-4">                                                         
                                <asp:LinkButton ID="lnkbtnNew" runat="server"
                                     CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-info" 
                                     TabIndex="25" onclick="lnkbtnNew_Click"><i class="fa fa-file-o"></i>New</asp:LinkButton>                                                            	
						        </div>                                  
						        <div class="col-sm-4">
                                   <asp:HiddenField ID="hidmindate" runat="server" />
                                   <asp:HiddenField ID="hidmaxdate" runat="server" />
                                   <asp:HiddenField ID="hiddriverPopulate" runat="server" />
                                <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" ValidationGroup="Save" CssClass="btn full_width_btn btn-s-md btn-success" TabIndex="26" OnClick="lnkbtnSave_OnClick" ViewStateMode="Inherit" ClientIDMode="Static"><i class="fa fa-save"></i>Save</asp:LinkButton>                      
						        </div>
						        <div class="col-sm-4">
                                <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-danger" OnClick="lnkbtnCancel_OnClick" TabIndex="27" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
						        </div>
                            </div>
                            <div class="col-lg-2"></div>
                          </div> 
                        </div>
                      </section>
                    </div>                      
                    
                    <!-- popup form GR detail -->
					

                  </form>
                </div>
              </section>
        </div>
        <div class="col-lg-1">
        </div>
    </div>
    <div id="Driver_details_form" class="modal fade">
							<div class="modal-dialog">
							<div class="modal-content">
								<div class="modal-header">
								<h4 class="popform_header">Driver </h4>
								</div>
								<div class="modal-body">
								<section class="panel panel-default full_form_container material_search_pop_form">
									<div class="panel-body">
									<div class="clearfix odd_row">	                                
	                <div class="col-sm-6">
	                    <label class="col-sm-4 control-label">S/O</label>
                    <div class="col-sm-8">
                    <asp:TextBox ID="txtDriverSOF" PlaceHolder="S/O"  autocomplete="off" runat="server" CssClass="form-control"  MaxLength="30" TabIndex="30" ></asp:TextBox>
                                   
                    </div>
	                </div>
	                <div class="col-sm-6">
	                    <label class="col-sm-4 control-label">Address</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtDriverAddress" PlaceHolder="Address Line" autocomplete="off" runat="server" CssClass="form-control"   MaxLength="30" TabIndex="31"></asp:TextBox>
                                    
                    </div>
	                </div>
	                </div>
	                <div class="clearfix even_row">	                                
	                <div class="col-sm-6">
	                    <label class="col-sm-4 control-label">Mobile No.1</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtDriverMobileNo1" runat="server" PlaceHolder="First Mobile Number" CssClass="form-control" autocomplete="off" MaxLength="10" TabIndex="32"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDriverMobileNo1"
                            SetFocusOnError="true" ValidationGroup="SaveDriver" ErrorMessage="<br/>Invalid Mobile No"
                            CssClass="classValidation" Display="Dynamic" ValidationExpression="^[7-9][0-9]{9}$"></asp:RegularExpressionValidator>
                                               
                    </div>
	                </div>
	                <div class="col-sm-6">
	                    <label class="col-sm-4 control-label">Mobile No.2</label>
                    <div class="col-sm-8">
                    <asp:TextBox ID="txtDriverMobileNo2" runat="server" PlaceHolder="Second Mobile Number" CssClass="form-control" autocomplete="off"  MaxLength="10" TabIndex="33"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDriverMobileNo2"
                        SetFocusOnError="true" ValidationGroup="SaveDriver" ErrorMessage="Invalid Mobile No"
                        CssClass="classValidation" Display="Dynamic" ValidationExpression="^[7-9][0-9]{9}$"></asp:RegularExpressionValidator>
                                   
                    </div>
	                </div>
	                </div>
	                <div class="clearfix odd_row">	                                
	                <div class="col-sm-6">
	                    <label class="col-sm-4 control-label">License No.<span class="required-field">*</span></label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtlicense" PlaceHolder="License Number" autocomplete="off" runat="server" CssClass="form-control" MaxLength="30"
                        TabIndex="34" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLicense" runat="server" ControlToValidate="txtlicense"
                        CssClass="classValidation" SetFocusOnError="true" Display="Dynamic" ErrorMessage="Please enter License No."
                        ValidationGroup="SaveDriver"></asp:RequiredFieldValidator>
                    </div>
	                </div>
	                <div class="col-sm-6">
	                    <label class="col-sm-4 control-label">Lic.Exp.Date<span class="required-field">*</span></label>
                    <div class="col-sm-4">
                    <asp:TextBox ID="txtExpiryDate" runat="server"  PlaceHolder="Exp. Date" CssClass="input-sm datepicker form-control" MaxLength="10" autocomplete="off"
                    oncopy="return false" oncut="return false" onDrop="blur();return false;"  onpaste="return false" onchange="Focus()"  TabIndex="35"></asp:TextBox>
                                    
                    </div>
                    
                    <div class="col-sm-4">	    
                        <asp:CheckBox ID="chkVarified" CssClass="col-sm-2" runat="server" TabIndex="36" />                           			                                 
	                    <label class="col-sm-10 control-label">Verified</label>
	                    </div>
                    <div class="col-sm-4">
                    </div>
                    <div class="col-sm-8">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtExpiryDate"
                        CssClass="classValidation" SetFocusOnError="true" Display="Dynamic" ErrorMessage="Please select expiry date"
                        ValidationGroup="SaveDriver"></asp:RequiredFieldValidator>
	                </div>
	                </div>
	                <div class="clearfix even_row">	                                
	                <div class="col-sm-6">
	                    <label class="col-sm-4 control-label">Lic.Authority</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtauthority" PlaceHolder="Lic. Authority" autocomplete="off" runat="server" CssClass="form-control"  MaxLength="30" TabIndex="37" ></asp:TextBox>
                                         
                    </div>
	                </div>
	                <div class="col-sm-6">
	                    <label class="col-sm-4 control-label">Guarantor</label>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="Drpgurenter" runat="server" CssClass="form-control"  TabIndex="38">
                        </asp:DropDownList>
                    </div>
	                </div>
	                </div>
	                <div class="clearfix odd_row">	                                
	                <div class="col-sm-6">
	                    <label class="col-sm-4 control-label">Haz.Licence</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtDriverHazardousL" PlaceHolder="Haz. Licence" runat="server" autocomplete="off" CssClass="form-control" MaxLength="10" TabIndex="39"></asp:TextBox>
                    </div>
	                </div>
	                <div class="col-sm-6">
	                    <label class="col-sm-4 control-label"> Haz.Exp Date</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtHazardousExpiryDate" runat="server" CssClass="input-sm datepicker form-control" MaxLength="10" autocomplete="off" oncopy="return false" oncut="return false" onDrop="blur();return false;" onchange="Focus()" PlaceHolder="Haz.Exp Date" TabIndex="40"></asp:TextBox>                                      
                    </div>
	                </div>
	                </div>
							<div class="clearfix even_row">	                                
	                <div class="col-sm-6">
	                    <label class="col-sm-4 control-label">Account No.</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtaccountno" runat="server" PlaceHolder="Account Number" autocomplete="off" CssClass="form-control"  MaxLength="50" TabIndex="41" ></asp:TextBox>
                    </div>
	                </div>
	                <div class="col-sm-6">
	                    <label class="col-sm-4 control-label"> RTGS Code</label>
                    <div class="col-sm-8">
                    <asp:TextBox ID="txtDriverRTGC" runat="server" CssClass="form-control" PlaceHolder="RTGS Code"  MaxLength="15" TabIndex="42"></asp:TextBox>
                    </div>
	                </div>
	                </div>
	                <div class="clearfix odd_row">	                                
	                <div class="col-sm-6">
	                    <label class="col-sm-4 control-label">Bank Name</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtDriverBankName" PlaceHolder="Bank Name" autocomplete="off" runat="server" CssClass="form-control" TabIndex="43"></asp:TextBox>
                                       
                    </div>
	                </div>
	                <div class="col-sm-6">
	                    <label class="col-sm-4 control-label">Bank Address</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtDriverBankAddrs" PlaceHolder="Bank Address" runat="server" CssClass="form-control"  TabIndex="44"></asp:TextBox>
                                    
                    </div>
	                </div>
	                </div>                       
									</div>
								</section>
								</div>
								<div class="modal-footer">
								<div class="popup_footer_btn"> 
                                             
                          
                             <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-dark" OnClick="lnkbtnOk_OnClick" CausesValidation="true" ValidationGroup="SaveDriver" TabIndex="45"><i class="fa fa-check"></i>Ok</asp:LinkButton>
                             <button type="submit" class="btn btn-dark" data-dismiss="modal"><i class="fa fa-times"></i>Close</button>
									<%--<asp:LinkButton ID="lnkbtnClose" runat="server" CssClass="btn btn-dark" OnClick="lnkbtnClose_OnClick" TabIndex="46"><i class="fa fa-times"></i>Close</asp:LinkButton>--%>
										       
								</div>
								</div>
							</div>
							</div>
						</div>
                        <!-- Account Details -->
           <div id="AccountDetlDiv" class="modal fade">
					<div class="modal-dialog">
			     	<div class="modal-content">
					<div class="modal-header">
				    <h4 class="popform_header">Account Details </h4>
					</div>
							<div class="modal-body">
							<section class="panel panel-default full_form_container material_search_pop_form">
							<div class="clearfix odd_row">
                               <label class="col-sm-5 control-label" style="width: 22%;">Bank Name</label>
		                        <div class="col-sm-6" style="width: 68%;">
		                            <asp:TextBox ID="txtBankName" runat="server" PlaceHolder="Bank Name" CssClass="form-control"  MaxLength="30" TabIndex="16"></asp:TextBox>
		                        </div> 
                             </div>
                                 <div class="clearfix even_row">
                               <label class="col-sm-5 control-label" style="width: 22%;">Branch Name</label>
		                        <div class="col-sm-6" style="width: 68%;">
		                            <asp:TextBox ID="txtBranchName" runat="server" PlaceHolder="Branch Name" CssClass="form-control"  MaxLength="30" TabIndex="16"></asp:TextBox>
		                        </div> 
                                 </div>
                                 <div class="clearfix odd_row">
                               <label class="col-sm-5 control-label" style="width: 22%;">Ifsc No</label>
		                        <div class="col-sm-6" style="width: 68%;">
		                            <asp:TextBox ID="txtIfscNo" runat="server" PlaceHolder="Ifsc No" CssClass="form-control"  MaxLength="30" TabIndex="16"></asp:TextBox>
		                        </div> 
                                 </div></section>
								</div>
								<div class="modal-footer">
								<div class="popup_footer_btn"> 
                                <a class="btn btn-dark" onclick="OnOk();">OK</a>
                                <a class="btn btn-dark" onclick="OnReset();">Reset</a>    
								</div>
								</div>
							</div>
							</div>
						</div>
                          <!-- popup form Document holder -->
										<div id="document_holder_popup" class="modal fade">
										  <div class="modal-dialog">
										    <div class="modal-content">
										      <div class="modal-header">
										        <h4 class="popform_header">Document Holder </h4>
										      </div>
										      <div class="modal-body">
										        <section class="panel panel-default full_form_container material_search_pop_form">
										          <div class="panel-body">
										            <!-- first main Column-->
											        	<div class="col-lg-8" style="padding: 0;  width: 64.7%;     margin-left: 0.15%; margin-right: 0.15%;">
											            <!-- first left row -->
											            <div class="clearfix first_left">
											              <section class="panel panel-in-default">
											                <div class="panel-body">
											                  <div class="clearfix odd_row">
											                  	<label class="col-sm-3 control-label">Doc. Name</label>
					                                <div class="col-sm-9">
					                                        <asp:TextBox ID="txtDocName" runat="server" CssClass="form-control" Height="24px"
                                            MaxLength="100" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                            onpaste="return false" placeholder="Enter Doc Name" TabIndex="27" Text=""></asp:TextBox>
					                                </div>
						                            </div>
											                  <div class="clearfix even_row">
					                              	<label class="col-sm-3 control-label">Remark</label>
					                                <div class="col-sm-9">				                                 
                                                    <asp:TextBox ID="txtDocRemark" runat="server" CssClass="form-control parsley-validated" Style="resize: none"
                                      MaxLength="100" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                            onpaste="return false" placeholder="Enter Remark" TabIndex="28" Text="" TextMode="MultiLine"></asp:TextBox>
					                                </div>
						                            </div>

											                </div>
											              </section>
											            </div> 
											          </div>
											          <!-- second main Column -->
											          <div class="col-lg-4" style="padding: 0; width: 34.7%; margin-left: 0.15%; margin-right: 0.15%;">
											            <!-- first_one left row -->
											            <div class="clearfix first_one_left">
											              <section class="panel panel-in-default">
											                <div class="panel-body">
											                	<div class="clearfix odd_row">
					                              	<div style="text-align: center;"> 
                                                         <asp:Image ID="imgEmp" runat="server" ImageUrl="img/placeholder.png" width="117px" height="117px" />
                                                          <asp:Label ID="lblimgError" runat="server" ForeColor="Red"></asp:Label>
					                              	</div>	                              	
						                            </div>
					                              <div class="clearfix even_row"> 
                                                      <asp:FileUpload ID="fuPicture" runat="server" onchange="ShowImagePreview(this);"  TabIndex="29" />
						                            </div>
											                </div>
											              </section>
											            </div>                        

											          </div>                          
										          </div>
										        </section>
										      </div>
                                              <div class="modal-body">
                                               <section class="panel panel-default full_form_container material_search_pop_form">
										          <div class="panel-body">
                                                  <asp:GridView ID="grdDocHolder" runat="server" AutoGenerateColumns="false" BorderStyle="None"
                                            OnPageIndexChanging="grdDocHolder_PageIndexChanging" Width="100%" GridLines="None"
                                            AllowPaging="true" PageSize="25" CssClass="display nowrap" OnRowCommand="grdDocHolder_RowCommand"
                                            BorderWidth="0" RowStyle-CssClass="even" AlternatingRowStyle-CssClass="odd"
                                            TabIndex="6" OnRowDataBound="grdDocHolder_RowDataBound">
                                            <HeaderStyle ForeColor="Black" CssClass="linearBg" />
                                            <AlternatingRowStyle CssClass="bgcolor2" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>.
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Document Name" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="150">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150" />
                                                    <ItemTemplate>
                                                        <%#Eval("DocName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150" />
                                                    <ItemTemplate>
                                                        <%#Eval("Remark")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Image" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink1" class="preview" NavigateUrl='<%#Eval("Image")%>' runat="server"
                                                            Target="_blank">
                                                            <asp:Image Width="100" ID="Image1" ImageUrl='<%#Eval("Image")%>' runat="server" />
                                                        </asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/delete_sm.png"
                                                            CommandArgument='<%# Container.DataItemIndex%>' CommandName="cmddelete" ToolTip="Delete" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                            <EmptyDataTemplate>
                                                <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                            </EmptyDataTemplate>
                                            <PagerStyle CssClass="white_bg" ForeColor="#000" HorizontalAlign="Center" />
                                        </asp:GridView>
                                                   </div>
										        </section>
                                              </div>
										      <div class="modal-footer">
										        <div class="popup_footer_btn"> 
                                                    <asp:LinkButton ID="btnAdd" CssClass="btn btn-dark" runat="server" data-dismiss="modal"><i class="fa fa-check"></i>Ok</asp:LinkButton>
										          <button type="submit" class="btn btn-dark" data-dismiss="modal"><i class="fa fa-times"></i>Close</button>
										        </div>
										      </div>
										    </div>
										  </div>
										</div>
    <asp:HiddenField ID="hidexpid" runat="server" />
    <asp:HiddenField ID="hidValidGSTIN" runat="server" />
    <asp:HiddenField ID="hidid" runat="server" />
    <script language="javascript" type="text/javascript">
        function EnabaleDisable() {
            var InvNo = document.getElementById("<%=hiddriverPopulate.ClientID%>").value;

            if (document.getElementById("<%=ddlAccountType.ClientID%>").value == "9") {
                document.getElementById("<%=imgbtnDriver.ClientID %>").disabled = false;
            }
            else {
                document.getElementById("<%=imgbtnDriver.ClientID %>").disabled = true;
            }
            if ((document.getElementById("<%=ddlAccountType.ClientID%>").value == "3") || (document.getElementById("<%=ddlAccountType.ClientID%>").value == "1")) {
                document.getElementById("<%=txtcontPrsn.ClientID %>").disabled = true;
                document.getElementById("<%=txtContMob.ClientID %>").disabled = true;
                document.getElementById("<%=txtContEmail.ClientID %>").disabled = true;
                document.getElementById("<%=txtAddress1.ClientID %>").disabled = true;
                document.getElementById("<%=txtAddress2.ClientID %>").disabled = true;
                document.getElementById("<%=ddlState.ClientID %>").disabled = true;
                document.getElementById("<%=ddlCity.ClientID %>").disabled = true;
                document.getElementById("<%=ddlDistrict.ClientID %>").disabled = true;
                document.getElementById("<%=SpanCityRefresh.ClientID%>").style.visibility = "hidden";
                document.getElementById("<%=txtPinCode.ClientID %>").disabled = true;
                document.getElementById("<%=txtTin.ClientID %>").disabled = true;
                document.getElementById("<%=txtFax.ClientID %>").disabled = true;
                document.getElementById("<%=txtagntCommision.ClientID %>").disabled = true;
                document.getElementById("<%=ddlCategory.ClientID %>").disabled = true;
                document.getElementById("<%=rfvState.ClientID%>").style.visibility = "hidden";
                document.getElementById("<%=rfvState.ClientID%>").enabled = false;
                document.getElementById("<%=rfvCity.ClientID%>").style.visibility = "hidden";
                document.getElementById("<%=rfvCity.ClientID%>").enabled = false;
                document.getElementById("<%=rfvpetroCompany.ClientID%>").style.visibility = "hidden";
                document.getElementById("<%=rfvpetroCompany.ClientID%>").enabled = false;
                document.getElementById("<%=SpanMandComp.ClientID %>").style.visibility = "hidden";
                document.getElementById("<%=SpanDistrictrefresh.ClientID%>").style.visibility = "hidden";
            }
            else if ((document.getElementById("<%=ddlAccountType.ClientID%>").value == "4")) {
                document.getElementById("<%=txtagntCommision.ClientID %>").disabled = true;
                document.getElementById("<%=txtcontPrsn.ClientID %>").disabled = false;
                document.getElementById("<%=txtContMob.ClientID %>").disabled = false;
                document.getElementById("<%=txtContEmail.ClientID %>").disabled = false;
                document.getElementById("<%=txtAddress1.ClientID %>").disabled = false;
                document.getElementById("<%=txtAddress2.ClientID %>").disabled = false;
                document.getElementById("<%=ddlState.ClientID %>").disabled = false;
                document.getElementById("<%=ddlCity.ClientID %>").disabled = false;
                document.getElementById("<%=ddlDistrict.ClientID %>").disabled = false;
                document.getElementById("<%=SpanCityRefresh.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=SpanDistrictrefresh.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=txtPinCode.ClientID %>").disabled = false;
                document.getElementById("<%=txtTin.ClientID %>").disabled = false;
                document.getElementById("<%=txtFax.ClientID %>").disabled = false;
                document.getElementById("<%=ddlCategory.ClientID %>").disabled = false;
                document.getElementById("<%=rfvState.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=rfvState.ClientID%>").enabled = true;
                document.getElementById("<%=rfvCity.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=rfvCity.ClientID%>").enabled = true;
                document.getElementById("<%=rfvpetroCompany.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=SpanMandComp.ClientID %>").style.visibility = "visible";
                document.getElementById("<%=rfvpetroCompany.ClientID%>").enabled = true;
                document.getElementById("<%=ddlDateRange.ClientID%>").disabled = false;
                document.getElementById("<%=ddlBalanceType.ClientID%>").disabled = false;
                document.getElementById("<%=txtOpBal.ClientID%>").disabled = false;
            }
            else {
                document.getElementById("<%=txtcontPrsn.ClientID %>").disabled = false;
                document.getElementById("<%=txtContMob.ClientID %>").disabled = false;
                document.getElementById("<%=txtContEmail.ClientID %>").disabled = false;
                document.getElementById("<%=txtAddress1.ClientID %>").disabled = false;
                document.getElementById("<%=txtAddress2.ClientID %>").disabled = false;
                document.getElementById("<%=ddlState.ClientID %>").disabled = false;
                document.getElementById("<%=ddlCity.ClientID %>").disabled = false;
                document.getElementById("<%=ddlDistrict.ClientID %>").disabled = false;
                document.getElementById("<%=txtPinCode.ClientID %>").disabled = false;
                document.getElementById("<%=txtTin.ClientID %>").disabled = false;
                document.getElementById("<%=txtFax.ClientID %>").disabled = false;
                document.getElementById("<%=txtagntCommision.ClientID %>").disabled = false;
                document.getElementById("<%=SpanCityRefresh.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=ddlCategory.ClientID %>").disabled = false;
                document.getElementById("<%=rfvState.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=rfvState.ClientID%>").enabled = true;
                document.getElementById("<%=rfvCity.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=rfvCity.ClientID%>").enabled = true;
                document.getElementById("<%=rfvpetroCompany.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=SpanMandComp.ClientID %>").style.visibility = "visible";
                document.getElementById("<%=rfvpetroCompany.ClientID%>").enabled = true;
                document.getElementById("<%=ddlDateRange.ClientID%>").disabled = false;
                document.getElementById("<%=ddlBalanceType.ClientID%>").disabled = false;
                document.getElementById("<%=txtOpBal.ClientID%>").disabled = false;
            }
            if (document.getElementById("<%=ddlAccountType.ClientID%>").value == "10") {

                document.getElementById("<%=ddlCompany.ClientID %>").disabled = false;
                document.getElementById("<%=rfvpetroCompany.ClientID %>").enabled = true;
                document.getElementById("<%=SpanMandComp.ClientID %>").style.visibility = "visible";
                document.getElementById("<%=SpanCompanyRefresh.ClientID %>").style.visibility = "visible";
            }
            else {
                document.getElementById("<%=ddlCompany.ClientID %>").disabled = true;
                document.getElementById("<%=rfvpetroCompany.ClientID %>").enabled = false;
                document.getElementById("<%=SpanMandComp.ClientID %>").style.visibility = "hidden";
                document.getElementById("<%=SpanCompanyRefresh.ClientID %>").style.visibility = "hidden";
            }
            if (document.getElementById("<%=ddlAccountType.ClientID%>").value == "2") {
                document.getElementById("<%=DivAccDetl.ClientID %>").style.display = "block";
                document.getElementById("<%=DivDetenation.ClientID %>").style.display = "block";
                document.getElementById("<%=DivContainer.ClientID %>").style.display = "block";
                document.getElementById("<%=GST.ClientID %>").style.display = "block";
            }
            else {
                document.getElementById("<%=GST.ClientID %>").style.display = "none";
                document.getElementById("<%=txtAccount.ClientID %>").disabled = true;
                document.getElementById("<%=DivAccDetl.ClientID %>").style.display = "none";
                document.getElementById("<%=DivDetenation.ClientID %>").style.display = "none";
                document.getElementById("<%=DivContainer.ClientID %>").style.display = "none";

            }

            if ((document.getElementById("<%=ddlAccountType.ClientID%>").value == "2") || (document.getElementById("<%=ddlAccountType.ClientID%>").value == "12")) {
                document.getElementById("<%=chkServExmpt.ClientID %>").disabled = false;
            }
            else {
                document.getElementById("<%=chkServExmpt.ClientID %>").disabled = true;
                document.getElementById("<%=chkServExmpt.ClientID %>").checked = false;
            }

            if (parseInt(InvNo, 0) <= 0) {
                document.getElementById("<%=ddlPrincComp.ClientID%>").disabled = false;
            }
            else {
                document.getElementById("<%=ddlPrincComp.ClientID%>").disabled = true;
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        function OnChangeDriver() {
            document.getElementById("<%=lnkDocHolder.ClientID %>").style.visibility = "Visible";
            document.getElementById("<%=TotalDocumentAdd.ClientID %>").style.visibility = "Visible";
            if (document.getElementById("<%=ddlAccountType.ClientID%>").value == "9") {
                document.getElementById("<%=imgbtnDriver.ClientID %>").disabled = false;
                document.getElementById("<%=txtagntCommision.ClientID%>").value = "0.00";
            }
            else {
                document.getElementById("<%=imgbtnDriver.ClientID %>").disabled = true;
                document.getElementById("<%=txtagntCommision.ClientID%>").value = "0.00";
            }
            if ((document.getElementById("<%=ddlAccountType.ClientID%>").value == "3") || (document.getElementById("<%=ddlAccountType.ClientID%>").value == "1")) {
                document.getElementById("<%=lnkDocHolder.ClientID %>").style.visibility = "hidden";
                document.getElementById("<%=TotalDocumentAdd.ClientID %>").style.visibility = "hidden";
                document.getElementById("<%=txtcontPrsn.ClientID %>").disabled = true;
                document.getElementById("<%=txtcontPrsn.ClientID %>").value = "";
                document.getElementById("<%=txtContMob.ClientID %>").disabled = true;
                document.getElementById("<%=txtContMob.ClientID %>").value = "";
                document.getElementById("<%=txtContEmail.ClientID %>").disabled = true;
                document.getElementById("<%=txtContEmail.ClientID %>").value = "";
                document.getElementById("<%=txtAddress1.ClientID %>").disabled = true;
                document.getElementById("<%=txtAddress1.ClientID %>").value = "";
                document.getElementById("<%=txtAddress2.ClientID %>").disabled = true;
                document.getElementById("<%=txtAddress2.ClientID %>").value = "";
                document.getElementById("<%=ddlState.ClientID %>").disabled = true;
                document.getElementById("<%=ddlState.ClientID %>").value = 0;
                document.getElementById("<%=ddlCity.ClientID %>").disabled = true;
                document.getElementById("<%=ddlCity.ClientID %>").value = 0;
                document.getElementById("<%=ddlDistrict.ClientID %>").disabled = true;
                document.getElementById("<%=ddlDistrict.ClientID %>").value = 0;


                
                document.getElementById("<%=txtagntCommision.ClientID%>").value = "0.00";
                document.getElementById("<%=SpanCityRefresh.ClientID%>").style.visibility = "hidden";

                document.getElementById("<%=txtPinCode.ClientID %>").disabled = true;
                document.getElementById("<%=txtPinCode.ClientID %>").value = "";
                document.getElementById("<%=txtTin.ClientID %>").disabled = true;
                document.getElementById("<%=txtTin.ClientID %>").value = "";
                document.getElementById("<%=txtFax.ClientID %>").disabled = true;
                document.getElementById("<%=txtFax.ClientID %>").value = "";
                document.getElementById("<%=txtagntCommision.ClientID %>").disabled = true;
                document.getElementById("<%=txtagntCommision.ClientID %>").value = "";

                document.getElementById("<%=ddlCategory.ClientID %>").disabled = true;
                document.getElementById("<%=ddlCategory.ClientID %>").value = 0;
                document.getElementById("<%=rfvState.ClientID%>").style.visibility = "hidden";
                document.getElementById("<%=rfvState.ClientID%>").enabled = false;
                document.getElementById("<%=rfvCity.ClientID%>").style.visibility = "hidden";
                document.getElementById("<%=rfvCity.ClientID%>").enabled = false;
                document.getElementById("<%=rfvpetroCompany.ClientID%>").style.visibility = "hidden";
                document.getElementById("<%=rfvpetroCompany.ClientID%>").enabled = false;
                document.getElementById("<%=SpanMandComp.ClientID %>").style.visibility = "hidden";
                document.getElementById("<%=SpanDistrictrefresh.ClientID%>").style.visibility = "hidden";
            }
            else if ((document.getElementById("<%=ddlAccountType.ClientID%>").value == "4")) {
                document.getElementById("<%=txtagntCommision.ClientID %>").disabled = true;
                document.getElementById("<%=txtagntCommision.ClientID %>").value = "";
                document.getElementById("<%=txtagntCommision.ClientID%>").value = "0.00";
                document.getElementById("<%=txtcontPrsn.ClientID %>").disabled = false;
                document.getElementById("<%=txtContMob.ClientID %>").disabled = false;
                document.getElementById("<%=txtContEmail.ClientID %>").disabled = false;
                document.getElementById("<%=txtAddress1.ClientID %>").disabled = false;
                document.getElementById("<%=txtAddress2.ClientID %>").disabled = false;
                document.getElementById("<%=ddlState.ClientID %>").disabled = false;
                document.getElementById("<%=ddlCity.ClientID %>").disabled = false;
                document.getElementById("<%=ddlDistrict.ClientID %>").disabled = false;

                document.getElementById("<%=SpanCityRefresh.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=SpanDistrictrefresh.ClientID%>").style.visibility = "visible";

                document.getElementById("<%=txtPinCode.ClientID %>").disabled = false;
                document.getElementById("<%=txtTin.ClientID %>").disabled = false;
                document.getElementById("<%=txtFax.ClientID %>").disabled = false;

                document.getElementById("<%=ddlCategory.ClientID %>").disabled = false;
                document.getElementById("<%=rfvState.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=rfvState.ClientID%>").enabled = true;
                document.getElementById("<%=rfvCity.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=rfvCity.ClientID%>").enabled = true;
                document.getElementById("<%=rfvpetroCompany.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=SpanMandComp.ClientID %>").style.visibility = "visible";
                document.getElementById("<%=rfvpetroCompany.ClientID%>").enabled = true;
                document.getElementById("<%=ddlDateRange.ClientID%>").disabled = false;
                document.getElementById("<%=ddlBalanceType.ClientID%>").disabled = false;
                document.getElementById("<%=txtOpBal.ClientID%>").disabled = false;
            }
            else {
                document.getElementById("<%=txtcontPrsn.ClientID %>").disabled = false;
                document.getElementById("<%=txtContMob.ClientID %>").disabled = false;
                document.getElementById("<%=txtContEmail.ClientID %>").disabled = false;
                document.getElementById("<%=txtAddress1.ClientID %>").disabled = false;
                document.getElementById("<%=txtAddress2.ClientID %>").disabled = false;
                document.getElementById("<%=ddlState.ClientID %>").disabled = false;
                document.getElementById("<%=ddlCity.ClientID %>").disabled = false;
                document.getElementById("<%=ddlDistrict.ClientID %>").disabled = false;
                
                document.getElementById("<%=txtagntCommision.ClientID%>").value = "0.00";
                document.getElementById("<%=txtPinCode.ClientID %>").disabled = false;
                document.getElementById("<%=txtTin.ClientID %>").disabled = false;
                document.getElementById("<%=txtFax.ClientID %>").disabled = false;
                document.getElementById("<%=txtagntCommision.ClientID %>").disabled = false;
                document.getElementById("<%=SpanCityRefresh.ClientID%>").style.visibility = "visible";

                document.getElementById("<%=ddlCategory.ClientID %>").disabled = false;
                document.getElementById("<%=rfvState.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=rfvState.ClientID%>").enabled = true;
                document.getElementById("<%=rfvCity.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=rfvCity.ClientID%>").enabled = true;
                
                document.getElementById("<%=rfvpetroCompany.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=SpanMandComp.ClientID %>").style.visibility = "visible";
                document.getElementById("<%=rfvpetroCompany.ClientID%>").enabled = true;
                document.getElementById("<%=ddlDateRange.ClientID%>").disabled = false;
                document.getElementById("<%=ddlBalanceType.ClientID%>").disabled = false;
                document.getElementById("<%=txtOpBal.ClientID%>").disabled = false;
            }
            if (document.getElementById("<%=ddlAccountType.ClientID%>").value == "10") {

                document.getElementById("<%=ddlCompany.ClientID %>").disabled = false;
                document.getElementById("<%=rfvpetroCompany.ClientID %>").enabled = true;
                document.getElementById("<%=SpanMandComp.ClientID %>").style.visibility = "visible";
                document.getElementById("<%=txtagntCommision.ClientID%>").value = "0.00";
                document.getElementById("<%=SpanCompanyRefresh.ClientID %>").style.visibility = "visible";
            }
            else {
                document.getElementById("<%=ddlCompany.ClientID %>").disabled = true;
                document.getElementById("<%=rfvpetroCompany.ClientID %>").enabled = false;
                document.getElementById("<%=SpanMandComp.ClientID %>").style.visibility = "hidden";
                document.getElementById("<%=txtagntCommision.ClientID%>").value = "0.00";
                document.getElementById("<%=SpanCompanyRefresh.ClientID %>").style.visibility = "hidden";
            }

            if (document.getElementById("<%=ddlAccountType.ClientID%>").value == "2") {
                document.getElementById("<%=ddlPrincComp.ClientID %>").disabled = false;
                document.getElementById("<%=txtAccount.ClientID %>").disabled = false;
                document.getElementById("<%=DivAccDetl.ClientID %>").style.display = "block";
                document.getElementById("<%=DivDetenation.ClientID %>").style.display = "block";
                document.getElementById("<%=DivContainer.ClientID %>").style.display = "block";
                document.getElementById("<%=GST.ClientID %>").style.display = "block";
            }
            else {
                document.getElementById("<%=ddlPrincComp.ClientID %>").disabled = true;
                document.getElementById("<%=txtAccount.ClientID %>").disabled = true;
                document.getElementById("<%=DivAccDetl.ClientID %>").style.display = "none";
                document.getElementById("<%=DivDetenation.ClientID %>").style.display = "none";
                document.getElementById("<%=DivContainer.ClientID %>").style.display = "none";
                document.getElementById("<%=GST.ClientID %>").style.display = "none";
            }

            if ((document.getElementById("<%=ddlAccountType.ClientID%>").value == "2") || (document.getElementById("<%=ddlAccountType.ClientID%>").value == "12")) {
                document.getElementById("<%=chkServExmpt.ClientID %>").disabled = false;
                document.getElementById("<%=txtagntCommision.ClientID%>").value = "0.00";
            }
            else {
                document.getElementById("<%=chkServExmpt.ClientID %>").disabled = true;
                document.getElementById("<%=chkServExmpt.ClientID %>").checked = false;
                document.getElementById("<%=txtagntCommision.ClientID%>").value = "0.00";
            }
        }
       function OnOk() {
           $('#AccountDetlDiv').modal('hide');
       }
        function onChangeAccSubGrp() {

            if ((document.getElementById("<%=ddlAccountType.ClientID%>").value == "1")) {
                if ((document.getElementById("<%=ddlAccountSubGroup.ClientID%>").value == "11") || (document.getElementById("<%=ddlAccountSubGroup.ClientID%>").value == "16")
                || (document.getElementById("<%=ddlAccountSubGroup.ClientID%>").value == "20") || (document.getElementById("<%=ddlAccountSubGroup.ClientID%>").value == "21")) {
                    document.getElementById("<%=ddlDateRange.ClientID%>").disabled = true;
                    document.getElementById("<%=ddlBalanceType.ClientID%>").disabled = true;
                    document.getElementById("<%=txtOpBal.ClientID%>").disabled = true;
                    document.getElementById("<%=txtOpBal.ClientID%>").value = "0.0";
                }
                else {
                    document.getElementById("<%=ddlDateRange.ClientID%>").disabled = false;
                    document.getElementById("<%=ddlBalanceType.ClientID%>").disabled = false;
                    document.getElementById("<%=txtOpBal.ClientID%>").disabled = false;
                }

            }
            else {
                document.getElementById("<%=ddlDateRange.ClientID%>").disabled = false;
                document.getElementById("<%=ddlBalanceType.ClientID%>").disabled = false;
                document.getElementById("<%=txtOpBal.ClientID%>").disabled = false;
            }
        }

        function ShowClient() {
            $("#Driver_details_form").fadeIn(1000);
            return false;
        }

        function HideDialog() {
            //   $("#overlay").hide();
            $("#Driver_details_form").fadeOut(300);
        }
             
    </script>
    <script language="javascript" type="text/javascript">
        SetFocus();
        $(document).ready(function () {


        })

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function () {
            SetFocus();

        });

        prm.add_endRequest(function () {
            SetFocus();
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function () {
            SetFocus();
            setDatecontrol();
        });

        prm.add_endRequest(function () {
            SetFocus();
            setDatecontrol();
        });


        $(document).ready(function () {
            setDatecontrol();
        });

        function setDatecontrol() {


        }
        function Focus() {
            $("#txtExpiryDate").focus();
        }

        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $("#<%=txtExpiryDate.ClientID %>").datepicker({
                buttonImageOnly: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate,
                changeMonth: true,
                changeYear: true,
                showOn: 'both',
                buttonImage: '../images/calendar.gif',
                focus: true

            });

            $("#<%=txtHazardousExpiryDate.ClientID %>").datepicker({
                buttonImageOnly: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate,
                changeMonth: true,
                changeYear: true,
                showOn: 'both',
                buttonImage: '../images/calendar.gif',
                focus: true

            });
        }
        $('#txtGST').keyup(function () {
            CheckGSTLength();
        });
        function CheckGSTLength() {
            var len = $('#txtGST').val().length;
            if (len != 0 && (len < 15 || len > 16)) {
                $('.validation-input').next('.validation-tip').show();
                setTimeout(function () { $('.validation-input').next('.validation-tip').hide(); }, 5000);
                $('#hidValidGSTIN').val('false');
            }
            else {
                $('#txtGST').next('.validation-tip').hide();
                $('#hidValidGSTIN').val('true');
            }

        }
    </script>
    <script language="javascript" type="text/javascript">

        function CheckParty() {
            __doPostBack('lnkParty', '');
        }
        function OnReset() {
            document.getElementById("<%=txtBankName.ClientID%>").value = "";
            document.getElementById("<%=txtBranchName.ClientID%>").value = "";
            document.getElementById("<%=txtIfscNo.ClientID%>").value = "";
        }
      

        function openModal() {
            $('#Driver_details_form').modal('show');
        }

        function CloseModal() {
            $('#Driver_details_form').Hide();
        }
        function openAccountDiv() {
            $('#AccountDetlDiv').modal('show');
        }
        function checkForComma(event) {
            if (event.charCode == 44) {
                return false;
            } else {
                return true;
            }
        }
        function ShowImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgEmp.ClientID%>').prop('src', e.target.result)
                        .width(120)
                        .height(110);
                };

                if (document.getElementById("<%=txtDocName.ClientID%>").value == "") {

                }
                else {


                    CheckParty();
                }
                reader.readAsDataURL(input.files[0]);

            }
        }
    </script>
</asp:Content>
