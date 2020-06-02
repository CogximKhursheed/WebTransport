<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="MiscMaster.aspx.cs" Inherits="WebTransport.MiscMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <contenttemplate>
            <div id="page-content">
                <div class="row ">
                    <div class="col-lg-2">
                    </div>
                    <div class="col-lg-6">
                        <section class="panel panel-default full_form_container part_purchase_bill_form">
								<header class="panel-heading font-bold form_heading">MISCELLANEOUS MASTER
									<span class="view_print"><a href="ManageMiscMaster.aspx">
                                    <asp:Label ID="lblViewList" runat="server" Text="LIST" 
                            TabIndex="12"></asp:Label></a></span>
								</header>
								<div class="panel-body">
									<form class="bs-example form-horizontal">
										<!-- first  section --> 
										<div class="clearfix first_section">
											<section class="panel panel-in-default">  
												<div class="panel-body">
													<div class="clearfix odd_row  ">
														<label class="col-sm-4 control-label">Transportaion Type<span class="required-field">*</span></label>
														<div class="col-sm-8">                                                      
															   <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" AutoPostBack="True" onchange="javascript:OnChangeGRType();"                                     >
                                                                    <asp:ListItem Text="--Select One--" Value="0" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="By Air" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="By Train" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="By Bus" Value="3"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvType"  runat="server" Display="Dynamic" ControlToValidate="ddlType"
                                                                ValidationGroup="save" ErrorMessage="Please Select Transportaion Type." InitialValue="0"
                                                                 SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
														</div>
													</div>
													<div class="clearfix even_row  ">
														<label class="col-sm-4 control-label">Name<span class="required-field">*</span></label>
														<div class="col-sm-8">
															<asp:TextBox ID="txtName" runat="server" CssClass="form-control" MaxLength="30"  TabIndex="2" onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false" >
                                                            </asp:TextBox>
                                                             <asp:RequiredFieldValidator ID="rfvName"  runat="server" Display="Dynamic" ControlToValidate="txtName"
                                                                ValidationGroup="save" ErrorMessage="Please Enter Name." SetFocusOnError="true" CssClass="classValidation">
                                                                </asp:RequiredFieldValidator>
														</div>
													</div>
                                                    <div class="clearfix odd_row  ">
														<label class="col-sm-4 control-label">Party Name<span class="required-field">*</span></label>
														<div class="col-sm-8">
															<asp:DropDownList ID="ddlParty" runat="server" CssClass="form-control" MaxLength="100"  TabIndex="2" onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false" >
                                                            </asp:DropDownList>
                                                             <asp:RequiredFieldValidator IsD="rfvtxtParty"  runat="server" Display="Dynamic" ControlToValidate="ddlParty"
                                                                ValidationGroup="save" ErrorMessage="Please Enter Party Name." SetFocusOnError="true" CssClass="classValidation">
                                                                </asp:RequiredFieldValidator>
														</div>
													</div>
													<div class="clearfix even_row  ">
														<label class="col-sm-3 control-label">Active</label>
														<div class="col-sm-9">
															<asp:CheckBox ID="chkStatus" runat="server" TabIndex="8" />                            
														</div>
													</div>                            
												</div>
											</section>                        
										</div>
										<!-- second  section -->
										<div class="clearfix fourth_right">
											<section class="panel panel-in-default btns_without_border">                            
												<div class="panel-body">     
													<div class="clearfix odd_row">
														<div class="col-lg-2"></div>
														<div class="col-lg-8">
															<div class="col-sm-4">                                                         
                                                            <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false" 
                                                                    CssClass="btn full_width_btn btn-s-md btn-info" 
                                                                    TabIndex="11" ><i class="fa fa-file-o"></i>New</asp:LinkButton>                                                            	
															</div>                                  
															<div class="col-sm-4">
                                                            <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" 
                                                                    ValidationGroup="save" CssClass="btn full_width_btn btn-s-md btn-success" 
                                                                     TabIndex="9" onclick="lnkbtnSave_Click" ><i class="fa fa-save"></i>Save</asp:LinkButton>                      
															</div>
															<div class="col-sm-4">
                                                            <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" 
                                                                    CssClass="btn full_width_btn btn-s-md btn-danger" 
                                                                     TabIndex="10" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
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
                               <asp:HiddenField ID="hidGrpMastidno" runat="server" />
                    </div>
                    <div class="col-lg-2">
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hidMiscidno" runat="server" />
       </contenttemplate>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
</asp:Content>
