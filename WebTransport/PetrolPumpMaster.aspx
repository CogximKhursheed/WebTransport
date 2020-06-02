<%@ Page Title="Petrol Pump Master" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="PetrolPumpMaster.aspx.cs" Inherits="WebTransport.PetrolPumpMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-3">
                </div>
                <div class="col-lg-6">
                    <section class="panel panel-default full_form_container part_purchase_bill_form">
	                  <header class="panel-heading font-bold form_heading">PETROL PUMP MASTER
	                    <span class="view_print"><a href="ManagePetrolPumpMaster.aspx"> <asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a></span>
	                  </header>
	                  <div class="panel-body">
	                    <form class="bs-example form-horizontal">
	                      <!-- first  section --> 
	                      <div class="clearfix first_section">
	                        <section class="panel panel-in-default">  
	                          <div class="panel-body">	                          	
	                            <div class="clearfix odd_row">
	                              	<label class="col-sm-3 control-label">Pump Name<span class="required-field">*</span></label>
	                                <div class="col-sm-9">
                                     <asp:TextBox ID="txtPumpName" runat="server" placeholder="Enter Pump Name." CssClass="form-control" MaxLength="50" TabIndex="1" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPumpNam" runat="server" ControlToValidate="txtPumpName"
                                            ValidationGroup="Save" ErrorMessage="Please Enter Petrol Pump Name!" CssClass="classValidation"
                                            SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>	                                
	                                </div>
	                            </div>
	                            <div class="clearfix even_row">
                              	<label class="col-sm-3 control-label">Company Name<span class="required-field">*</span></label>
                                <div class="col-sm-9">
                                 <asp:DropDownList ID="drpPetrolCompany" runat="server" CssClass="form-control" TabIndex="2">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPetrolCompany" runat="server" ControlToValidate="drpPetrolCompany"
                                    ValidationGroup="Save" ErrorMessage="Please Select Petrol Company Name!"
                                    CssClass="classValidation" SetFocusOnError="true" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                               
                                </div>
	                            </div>
	                            <div class="clearfix odd_row">
	                              	<label class="col-sm-3 control-label">Person Name<span class="required-field">*</span></label>
	                                <div class="col-sm-9">
	                                  <asp:TextBox ID="txtPersonName" runat="server" placeholder="Enter Person Name." CssClass="form-control"   MaxLength="50" TabIndex="3" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPersonName" runat="server" ControlToValidate="txtPersonName" ValidationGroup="Save" ErrorMessage="Please Enter Person Name!" CssClass="classValidation"
                                            SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
	                                </div>
	                            </div>
	                            <div class="clearfix even_row">
	                              	<label class="col-sm-3 control-label">Designation<span class="required-field">*</span></label>
	                                <div class="col-sm-9">
                                       <asp:TextBox ID="txtDesignation" runat="server" placeholder="Enter Designation" CssClass="form-control" MaxLength="50" TabIndex="4"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ControlToValidate="txtDesignation"
                                            ValidationGroup="Save" ErrorMessage="Please Enter Designation!" CssClass="classValidation"
                                            SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
	                                </div>
	                            </div>
	                            <div class="clearfix odd_row">
	                              	<label class="col-sm-3 control-label">Landline No.</label>
	                              	 <div class="col-sm-2">
                                     <asp:TextBox ID="txtLadlineCode" runat="server" placeholder="Code" CssClass="form-control" MaxLength="4" TabIndex="5" ></asp:TextBox>
                                             
	                                </div>
	                                <div class="col-sm-7">
	                                  <asp:TextBox ID="txtLadlineNo" runat="server" placeholder="Enter Ladline Number." CssClass="form-control" MaxLength="7" TabIndex="6" ></asp:TextBox>
	                                </div>
	                            </div>	
	                            <div class="clearfix even_row">
	                              	<label class="col-sm-3 control-label">Mobile No.<span class="required-field">*</span></label>
	                                <div class="col-sm-9">
	                                   <asp:TextBox ID="txtMobileNo" runat="server" placeholder="Enter Mobile" CssClass="form-control" MaxLength="10" TabIndex="7" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvMobileNo" runat="server" ControlToValidate="txtMobileNo" ValidationGroup="Save" ErrorMessage="Please Enter Mobile Number!" CssClass="classValidation" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
	                                </div>
	                            </div>
	                            <div class="clearfix odd_row">
                              	<label class="col-sm-3 control-label">Address</label>
                                <div class="col-sm-9">
                                   <asp:TextBox ID="txtAddress1" runat="server" placeholder="Enter Address Details"  CssClass="form-control" MaxLength="100" TabIndex="8"  ></asp:TextBox>
                                    <asp:TextBox ID="txtAddress2" runat="server" placeholder="Enter Address Details" CssClass="form-control" MaxLength="100" TabIndex="9"></asp:TextBox>
                                   <%-- <textarea id="sbill_txt_area_resize_none" class="form-control parsley-validated" rows="2" data-minwords="3" data-required="true"></textarea>--%>
                                </div>
	                            </div>
	                            <div class="clearfix even_row">
                              	<label class="col-sm-3 control-label">State Name<span class="required-field">*</span></label>
                                <div class="col-sm-9">
                                 <asp:DropDownList ID="drpState" runat="server" CssClass="form-control" TabIndex="10"  AutoPostBack="True" OnSelectedIndexChanged="drpState_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="drpState"
                                        ValidationGroup="Save" ErrorMessage="Please Select State!" CssClass="classValidation"
                                        SetFocusOnError="true" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                    
                                </div>
	                            </div>
	                            <div class="clearfix odd_row  ">
                              	<label class="col-sm-3 control-label">City Name<span class="required-field">*</span> </label>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" TabIndex="11">
                                    </asp:DropDownList>                                   
                                    <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity"
                                        ValidationGroup="Save" ErrorMessage="Please select city name!" CssClass="classValidation"
                                        InitialValue="0" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>                                 
                                </div>
	                            </div>	                            	 
	                             <div class="clearfix even_row  ">
	                              	<label class="col-sm-3 control-label">Active</label>
	                                <div class="col-sm-9">
	                                  <asp:CheckBox ID="chkStatus" runat="server" TabIndex="12" />
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
                                    <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false" 
                                           CssClass="btn full_width_btn btn-s-md btn-info" OnClick="lnkbtnNew_OnClick" 
                                           TabIndex="15" ><i class="fa fa-file-o"></i>New</asp:LinkButton>                                                            	
									</div>                                  
									<div class="col-sm-4">
                                    <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" 
                                            ValidationGroup="Save" CssClass="btn full_width_btn btn-s-md btn-success" 
                                            OnClick="lnkbtnSave_OnClick" TabIndex="13" ><i class="fa fa-save"></i>Save</asp:LinkButton>                      
									</div>
									<div class="col-sm-4">
                                    <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" 
                                            CssClass="btn full_width_btn btn-s-md btn-danger" 
                                            OnClick="lnkbtnCancel_OnClick" TabIndex="14" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
									</div>
	                              </div>
	                              <div class="col-lg-2"></div>
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
            <asp:HiddenField ID="hidPPumpidno" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
