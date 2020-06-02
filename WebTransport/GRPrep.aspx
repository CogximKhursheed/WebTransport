<%@ Page Title="Gr Prepration" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="GRPrep.aspx.cs" Inherits="WebTransport.GRPrep" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row ">
        <div class="col-lg-12">
            <div runat="server" id="PrintLastSavedGR" visible="" style="background:#65c9bf; padding:10px;">
                <b><i style="color: #f9e36b;font-size: 14px;" class="fa fa-info-circle"> </i> Do you want to print last saved GR? Please click on print. </b> 
                <asp:Button ID="btnPrintLastSavedGR" CssClass="pull-right" style="border: 1px solid silver;border-radius: 4px;background: #f6f6f6;" runat="server" Text="Print" OnClick="PrintLastSaved_Click" />
                <asp:DropDownList ID="ddlCopyPages" runat="server" style="width: 100px;font-size: 12px;height: 20px;" CssClass="form-control pull-right">
                    <asp:ListItem Text="4 Pages" Value="4" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="3 Pages" Value="3"></asp:ListItem>
                    <asp:ListItem Text="2 Pages" Value="2"></asp:ListItem>
                    <asp:ListItem Text="1 Pages" Value="1"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">GR PREPARATION / CHALLAN
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Lblchllnno" runat="server" Text=""></asp:Label>
                  
                    <span class="view_print">  
                        <asp:Panel ID="panelPrint" runat="server" style="display: inline-block;">
                            <asp:LinkButton ID="lnkBtnLast" class="view_print"  runat="server"  AlternateText="Print" title="Print" Height="16px" Onclick="lnkBtnLast_Click">LAST PRINT</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lnkbtnPrint" CssClass="fa fa-print icon" Visible="false" runat="server" ToolTip="Print" AlternateText="Print" title="Print" Height="16px" Onclick="lnkbtnPrint_Click"></asp:LinkButton>
                            <asp:LinkButton ID="lnkJainPrint" CssClass="fa fa-print icon" Visible="false" runat="server" ToolTip="JainPrint" AlternateText="JainPrint" title="JainPrint" Height="16px" Onclick="lnkJainPrint_Click"></asp:LinkButton>
                            <asp:LinkButton ID="lnkOMCargo" CssClass="fa fa-print icon" Visible="false" runat="server" ToolTip="OMPrint" AlternateText="OMPrint" title="OMPrint" Height="16px" Onclick="lnkOMCargo_Click"></asp:LinkButton>
                            <asp:LinkButton ID="lnkCapitalLogistic" Visible="false" CssClass="fa fa-print icon" runat="server" ToolTip="Capital logistic print" AlternateText="Print" title="Capital Logistic Print" Height="16px" Onclick="lnkbtnPrintCapitalLogistic_Click"></asp:LinkButton>
                            <asp:LinkButton ID="lnkKajariaLogistic" Visible="false" CssClass="fa fa-print icon" runat="server" ToolTip="Kajaria logistic print" AlternateText="Print" title="Kajaria Logistic Print" Height="16px" Onclick="lnkbtnPrintKajariaLogistic_Click"></asp:LinkButton>
                        </asp:Panel>
                        <asp:Panel ID="panelView" runat="server" style="display: inline-block;">
                            <a href="ManageGRPrep.aspx"><asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a>&nbsp;
                        </asp:Panel>
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
                              <label class="col-sm-3 control-label" style="width: 27%;">Date Range<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 63%;">
                                <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged">
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                    ControlToValidate="ddlDateRange" ValidationGroup="save" ErrorMessage="Please Select Date Range."
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                           	<div class="col-sm-4" style="width: 30%">
                           		<label class="col-sm-3 control-label" style="width: 30%;">GR Date<span class="required-field">*</span></label>
                              <div class="col-sm-4" style="width: 28%;">
                                <asp:TextBox ID="txtGRDate" runat="server" PlaceHolder="DD-MM-YYYY" CssClass="input-sm datepicker form-control" MaxLength="10" onkeydown = "return DateFormat(this, event.keyCode)" AutoPostBack="True" ClientIDMode="Static" OnTextChanged="txtGRDate_TextChanged"></asp:TextBox>
                              </div>
                              <div class="col-sm-3" style="width: 22%;">
                              	<asp:TextBox ID="txtPrefixNo"  runat="server" PlaceHolder="Pref No." CssClass="form-control"  ToolTip="Prefix GR"  MaxLength="18"></asp:TextBox>
                              </div>
                              <div class="col-sm-2" style="width: 18%;">
                              	<asp:TextBox ID="txtGRNo" runat="server" CssClass="form-control" Style="text-align: right;" ToolTip="GR Number" Enabled="true" AutoPostBack="true"  MaxLength="9" OnTextChanged="txtGRNo_TextChanged"></asp:TextBox>
                              </div>
                           	</div>
                          <div class="col-sm-4" style="width: 35%">
                              <label class="col-sm-3 control-label" style="width: 15%;">Against</label>
							    <div class="col-sm-9" style="width: 75%;">
							    <div class="radio" style="display:inline;padding-top: 4px;">
								    <label class="radio-custom" >
								    <asp:RadioButton ID="RDbDirect" runat="server" GroupName="Against"
                                        onchange="javascript:OnchangeGrAgnst('1')" />Direct
                                        
								    </label>
								    <label class="radio-custom" >
								    <asp:RadioButton ID="RDbRecpt" runat="server" GroupName="Against" CssClass="by_receipt" 
                                        onchange="javascript:OnchangeGrAgnst('2')" />By Receipt
								    </label>
                                      <label class="radio-custom" >
								    <asp:RadioButton ID="rdbAdvanceOrder" runat="server" GroupName="Against" 
                                        onchange="javascript:OnchangeGrAgnst('3')" CssClass="by_receipt" 
                                        />Adv. Order
								    </label>
							    </div>
							    </div>
                                <div class="col-sm-1" style="width: 10%;">
                                <asp:LinkButton ID="lnkbtnGrAgain" runat="server" OnClick="lnkbtnGrAgain_Click" Height="22px" class="btn btn-sm btn-primary acc_home" ><i class="fa fa-file"></i></asp:LinkButton>
                                </div>
                            </div>
                           
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 27%;">GR Type<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 63%;">
                                <asp:DropDownList ID="ddlGRType" runat="server" CssClass="form-control" AutoPostBack="True" onchange="javascript:OnChangeGRType();"
                                    OnSelectedIndexChanged="ddlGRType_SelectedIndexChanged">
                                    <asp:ListItem Text="Paid GR" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="TBB GR" Value="2" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="To Pay GR" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                              </div>
                            </div>
                               <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 27%;">Loc.[From]<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 61%;">
                                <asp:DropDownList ID="ddlFromCity" runat="server" CssClass="form-control"  
                                    OnSelectedIndexChanged="ddlFromCity_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvtxtfromcity" runat="server" Display="Dynamic"
                                    ControlToValidate="ddlFromCity" ValidationGroup="save" ErrorMessage="Please Select From City."
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                              <div class="col-sm-1" style="width: 10%;">
                              <span id="SpanFromCityRefresh" runat="server" visible="true">
                                <asp:LinkButton ID="lnkBtnFromCity" runat="server" OnClick="lnkBtnFromCity_Click"  class="btn-sm btn btn-primary acc_home"><i class="fa fa-refresh"></i></asp:LinkButton>
                              </span>
                              </div>
                            </div>
                            <div class="col-sm-4" style="width: 30%">
                           		<label class="col-sm-3 control-label" style="width: 27%;">To City<span class="required-field">*</span></label>
                           		<div class="col-sm-8" style="width: 63%;">
                                <asp:DropDownList ID="ddlToCity" runat="server" OnSelectedIndexChanged="ToCity_SelectedIndexChanged" CssClass="form-control"  onchange="javascript:cityviaddl();" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlToCity" runat="server" Display="Dynamic" ControlToValidate="ddlToCity"
                                    ValidationGroup="save" ErrorMessage="Please Select To City." InitialValue="0"
                                    SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                              <div class="col-sm-1" style="width: 10%;">
                              <span id="SpanToCityRefresh" runat="server"  visible="true">
                                <asp:LinkButton ID="lnkBtnTocity" runat="server" OnClick="lnkBtnTocity_Click" class="btn-sm btn btn-primary acc_home" ><i class="fa fa-refresh"></i></asp:LinkButton>
                              </span>
                              </div>
                           	</div>
                          </div>
                          <div class="clearfix odd_row">
                          <div class="col-sm-4">
                            <label class="col-sm-3 control-label" style="width: 27%;">To State</label>    
                            <div class="col-sm-8" style="width: 61%;">
							<asp:TextBox ID="txtToState" runat="server" CssClass="form-control" PlaceHolder="" AutoComplete="off" Enabled="false"></asp:TextBox>
                            </div>
                          </div>
                          <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 27%;">City[Via]<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 61%;">
                                <asp:DropDownList ID="ddlCityVia" runat="server" CssClass="form-control" >
                                </asp:DropDownList>
                              </div>
                              <div class="col-sm-1" style="width: 10%;">
                              <span id="SpanCityViaRefresh" runat="server" visible="true">
                                <asp:LinkButton ID="lnkBtnCityvia" runat="server"  
                                      class="btn-sm btn btn-primary acc_home"  
                                      onclick="lnkBtnCityvia_Click"><i class="fa fa-refresh"></i></asp:LinkButton>
                              </span>
                              </div>
                            </div>
                           	<div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 25%;">Deliv. Place<span class="required-field">*</span></label>
							<div class="col-sm-8" style="width: 56%;">
							<asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" >
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvLocation" runat="server" Display="Dynamic" ControlToValidate="ddlLocation"
                                ValidationGroup="save" ErrorMessage="Please Select Delivery Place." InitialValue="0"
                                SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtLoc" MaxLength="20" runat="server"  CssClass="form-control" PlaceHolder="Enter Location" Visible="false"></asp:TextBox> 
                                <asp:RequiredFieldValidator ID="rfvTxtLoc" runat="server" Display="Dynamic"
                                    ControlToValidate="txtLoc" ValidationGroup="save" ErrorMessage="Please Enter From City."
                                    SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
							</div>
							<div class="col-sm-1" style="width: 10%;">
                            <span id="SpanDelvryPlaceRefresh" runat="server" visible="true">
                                <asp:LinkButton ID="lnkBtnDelvryPlace" runat="server" OnClick="lnkBtnDelvryPlace_Click" class="btn-sm btn btn-primary acc_home" ><i class="fa fa-refresh"></i></asp:LinkButton>
                            </span>
                                </div>
                            </div>
                            
                          </div>
                          <div class="clearfix even_row">
                          <div class="col-sm-4" style="width: 33.4%"> 
                           		<label class="col-sm-3 control-label" style="width: 27%;">Tax Paid by</label>
                              <div class="col-sm-9" style="width: 61%;">
                                <asp:DropDownList ID="ddlSrvcetax" runat="server" CssClass="form-control" >
                                    <asp:ListItem Text="Transporter" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Consigner" Value="2"></asp:ListItem>
                                     <asp:ListItem Text="Consignee" Value="3" ></asp:ListItem>
                                </asp:DropDownList>
                              </div>
                           	</div>
                           <div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 26%;">Billing Prty<span class="required-field">*</span></label>
                              <div class="col-sm-8" style="width: 61%;">
                                <asp:DropDownList ID="ddlSender" runat="server" CssClass="form-control" onchange="javascript:consignor(this);">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtSender" runat="server" CssClass="glow form-control auto-extender" onkeyup="SetContextKey()" onkeydown="return (event.keyCode!=13);" TabIndex="5"></asp:TextBox>
                                     <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSender" MinimumPrefixLength="1" UseContextKey="false" EnableCaching="true" CompletionSetCount="1" CompletionInterval="500" OnClientItemSelected="ClientItemSelected" ServiceMethod="GetSenderNo">
                                                                        </asp:AutoCompleteExtender>
                                <asp:RequiredFieldValidator ID="rfvddlSender"  runat="server" Display="Dynamic" ControlToValidate="ddlSender"
                                    ValidationGroup="save" ErrorMessage="Please Select Sender's Name." InitialValue="0"
                                    SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                              <div class="col-sm-1" style="width: 10%;">
                              <span id="SpanSenderRefresh" runat="server"  visible="true">
                                <asp:LinkButton ID="lnkBtnSender" runat="server" OnClick="lnkBtnSender_Click" class="btn-sm btn btn-primary acc_home" ><i class="fa fa-refresh"></i></asp:LinkButton>
                                </span>
                              </div>
                           	</div>
                            
                            <div class="col-sm-4" style="width:32%;">
                           		<label class="col-sm-3 control-label" style="width:25%;">Consigner<span class="required-field">*</span></label>
                              <div class="col-sm-8" style="width:59%;">
                                 <asp:TextBox ID="txtconsnr" runat="server" CssClass="form-control" MaxLength="80" Placeholder="Enter Consignor Name"
                                    ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvconsn" runat="server" ControlToValidate="txtconsnr"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Enter Consigner Name" 
                                    SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator> 
                              </div>
                             <%-- <div class="col-sm-1" style="width: 10%;">
                              <span id="Span2" runat="server"  visible="true">
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkBtnSender_Click" class="btn-sm btn btn-primary acc_home" ><i class="fa fa-refresh"></i></asp:LinkButton>
                                </span>
                              </div>--%>
                           	</div>
                          </div>
                          
                          <div class="clearfix odd_row">
                          <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 27%;">Consignee<span class="required-field">*</span></label>
							    <div class="col-sm-8" style="width: 63%;">
						            <asp:DropDownList ID="ddlReceiver" runat="server" Width="97%"  CssClass="chzn-select" >
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlReceiver" runat="server" Display="Dynamic"
                                        ControlToValidate="ddlReceiver" ValidationGroup="save" ErrorMessage="Please Select Receiver's Name."
                                        InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
							    </div>
							    <div class="col-sm-1" style="width: 10%;">
                                <span id="SpanReceiverRefresh" runat="server"  visible="true">
                                    <asp:LinkButton ID="lnkBtnReceiver" runat="server" OnClick="lnkBtnReceiver_Click" class="btn-sm btn btn-primary acc_home"><i class="fa fa-refresh"></i></asp:LinkButton>
                                </span>
                              </div>
                            </div>
                          <div class="col-sm-4">
                                <asp:Label ID="lblTruckNo" class="col-sm-3 control-label" style="width: 27%;font-weight:600" runat="server">Truck No.<span class="required-field">*</span></asp:Label>
                               
                                  <div class="col-sm-8" style="Width:60%">
                                    <asp:DropDownList style="width:100%" ID="ddlTruckNo" CssClass="chzn-select" runat="server"  AutoPostBack="false" OnSelectedIndexChanged="ddlTruckNo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                
                                  </div>

                                  <div class="col-sm-1" style="width: 10%;">
                                    <asp:LinkButton ID="lnkbtnTruuckRefresh" runat="server" ToolTip="Update Truck No." OnClick="lnkbtnTruuckRefresh_Click" class="btn-sm btn btn-primary acc_home"><i class="fa fa-refresh"></i></asp:LinkButton>
                                  </div>
                           	</div>
                          	<div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 24%;">Recpt Type</label>
							<div class="col-sm-8" style="width: 58%;">
							<asp:DropDownList ID="ddlRcptType" runat="server" CssClass="form-control"  OnSelectedIndexChanged="ddlRcptType_SelectedIndexChanged"
                                AutoPostBack="true" ClientIDMode="Static">
                            </asp:DropDownList>
							</div>
                            <div class="col-sm-1" style="width: 8%;">
                            <i class="btn btn-sm btn-primary acc_home" onclick="if($('#ddlRcptType').val() == 57)$('#type_popup').modal('show');"><i class="fa fa-file"></i></i>
                               <asp:LinkButton ID="lnkLorryType" Visible="false" runat="server" ToolTip="Details" Enabled="false" 
                                      CssClass="btn-sm btn btn-primary acc_home" type="button" data-toggle="modal" 
                                      data-target="#type_popup" TabIndex="17"><i class="fa fa-file"></i></asp:LinkButton>
                              </div>
                            </div>
                             
                          </div>
                            <div class="clearfix even_row">
                                <div class="col-sm-4">
                                    <label class="col-sm-3 control-label" style="width:27%;">Manual No</label>
                                    <div class="col-sm-8" style="width:52%;">
                                        <asp:TextBox ID="txtManualNo" runat="server" CssClass="form-control" PlaceHolder="Manual GR No"  Enabled="true"></asp:TextBox>     
                                    </div>
                                    <div style="width:12%;float:left">
                                        <div id="UpdateManNo" visible="false" runat="server" class="col-sm-1" style="width: 10%;">
                                            <asp:LinkButton ID="lnkbtnManNoUpdate" runat="server" ToolTip="Update Manual No." OnClick="lnkbtnManNoUpdate_Click" class="btn-sm btn btn-primary acc_home"><i class="fa fa-check"></i></asp:LinkButton>
                                        </div>
                                        <div class="col-sm-1" style="width:10%;float:right">
                                            <span id="Span1" runat="server" visible="true">
                                                <asp:LinkButton ID="lnkChlnGen" runat="server"  
                                                    class="btn btn-sm btn-primary acc_home" ToolTip="Generate Payment again Challan" 
                                                    onclick="lnkChlnGen_Click"><i class="fa fa-file"></i></asp:LinkButton>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                 <div class="col-sm-4">
                                    <label class="col-sm-4 control-label" style="width:27%">Goods Value</label>
                                    <div class="col-sm-8" style="width:69%">
                                        <asp:TextBox ID="txtTotItemPrice" runat="server" CssClass="form-control"
                                            Text="0.00" Style="text-align: right;"
                                            onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"
                                            onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                          </div>
                          </section>

                    <section class="panel panel-in-default"> 
                    <div class="collapse-header"><h3>More details (Click here) <i class="fa fa-minus" aria-hidden="true"></i></h3></div> 
                        <div class="panel-body">
                          <div class="clearfix even_row">
                          <div class="col-sm-3">
                            <b><asp:Label ID="lblDelvNo" class="col-sm-3 control-label" style="width: 27%;" runat="server" Text="DI No."></asp:Label></b>
                              <div class="col-sm-9" style="width: 63%;">
                                <asp:TextBox ID="txtDelvNo" runat="server" PlaceHolder="Enter Delivery Number" CssClass="form-control" MaxLength="19"
                                            OnTextChanged="txtDelvNo_TextChanged" ></asp:TextBox>       
                              </div>
                              <div id="UpdateDiNo" visible="false" runat="server" class="col-sm-1" style="width: 10%;">
                                    <asp:LinkButton ID="lnkbtnDiNoUpdate" runat="server" ToolTip="Update DiNo." OnClick="lnkbtnDiNoUpdate_Click" class="btn-sm btn btn-primary acc_home"><i class="fa fa-check"></i></asp:LinkButton>
                                  </div>
                            </div>
                             <div class="col-sm-3">
                            <b><asp:Label ID="lblDelvDate" class="col-sm-3 control-label" style="width: 27%;" runat="server" Text="DI Date"></asp:Label></b>
                              <div class="col-sm-9" style="width: 63%;">
                                   <asp:TextBox ID="txtDIDate"  PlaceHolder="DD-MM-YYYY" CssClass="input-sm datepicker form-control" MaxLength="10" onkeydown = "return DateFormat(this, event.keyCode)" ClientIDMode="Static"  runat="server" AutoComplete="off"></asp:TextBox> 
                              </div>
                           </div>
                           	<div class="col-sm-3">
                           		<label class="col-sm-3 control-label" style="width: 27%;">EGP No.</label>
                              <div class="col-sm-9" style="width: 61%;">
                                <asp:TextBox ID="TxtEGPNo" runat="server" PlaceHolder="Enter EGP Number" CssClass="form-control" MaxLength="20"   OnTextChanged="TxtEGPNo_TextChanged"></asp:TextBox>
                              </div>
                               <div id="UpdateEGPNo" visible="false" runat="server" class="col-sm-1" style="width: 10%;">
                                    <asp:LinkButton ID="lnkbtnEgpNoUpdate" runat="server" ToolTip="Update EGP No." OnClick="lnkbtnEgpNoUpdate_Click" class="btn-sm btn btn-primary acc_home"><i class="fa fa-check"></i></asp:LinkButton>
                                  </div>
                           	</div>
                             <div class="col-sm-3">
                                <label class="col-sm-3 control-label" style="width: 23%;">EGP Date</label>
								<div class="col-sm-1" style="width: 57%;">
								<asp:TextBox ID="txtEGPDate" PlaceHolder="DD-MM-YYYY" CssClass="input-sm datepicker form-control" MaxLength="10" onkeydown = "return DateFormat(this, event.keyCode)" ClientIDMode="Static"  runat="server" AutoComplete="off"></asp:TextBox>
								</div>
                            </div>
                            </div>
                            <div class="clearfix odd_row">
                        <div class="col-sm-3">
                            <label class="col-sm-3 control-label"  style="width:27%;">Order No.</label>
                            <div class="col-sm-9" style="width:63%;">
                                <asp:TextBox ID="txtOrderNo" runat="server" CssClass="form-control" PlaceHolder="Enter Order No" MaxLength="50" Enabled="true"></asp:TextBox>
                                </div>
                                <div id="UpdateOrdrNo" visible="false" runat="server" class="col-sm-1" style="width: 10%;">
                                    <asp:LinkButton ID="lnkbtnOrdrNoUpdate" runat="server" ToolTip="Update Order No." OnClick="lnkbtnOrdrNoUpdate_Click" class="btn-sm btn btn-primary acc_home"><i class="fa fa-check"></i></asp:LinkButton>
                                    
                                  </div>
                            </div>
                            <div class="col-sm-3">
                            <label class="col-sm-1 control-label"  style="width:27%;">Form No.</label>
                            <div class="col-sm-4" style="width: 61%;">
                                <asp:TextBox ID="txtFromNo" runat="server" CssClass="form-control" PlaceHolder="Enter Form No" MaxLength="50" Enabled="true"></asp:TextBox>
                                </div>
                                <div id="UpdateFormNo" visible="false" runat="server" class="col-sm-1" style="width: 10%;">
                                    <asp:LinkButton ID="lnkbtnFormNoUpdate" runat="server" ToolTip="Update Form No." OnClick="lnkbtnFormNoUpdate_Click" class="btn-sm btn btn-primary acc_home"><i class="fa fa-check"></i></asp:LinkButton>
                                  </div>
                            </div>
                             <div class="col-sm-3">
                              <b><asp:Label ID="lblrefrename" class="col-sm-3 control-label" style="width:23%;" runat="server" Text="Ref. No."></asp:Label></b>
                            <div class="col-sm-4" style="width:61%;margin-left: 4%;">
                            <asp:TextBox ID="txtRefNo" runat="server" CssClass="form-control" PlaceHolder="Enter Ref No" MaxLength="100" Enabled="true"></asp:TextBox>     
                            </div>
                            <div id="UpdateInvNo" visible="false" runat="server" class="col-sm-1" style="width: 10%;">
                                    <asp:LinkButton ID="lnkbtnInvNoUpdate" runat="server" ToolTip="Update DiNo." OnClick="lnkbtnInvNoUpdate_Click" class="btn-sm btn btn-primary acc_home"><i class="fa fa-check"></i></asp:LinkButton>
                                  </div>
                            </div>
                            <div class="col-sm-3" >
                            <label class="col-sm-1 control-label"  style="width:27%;">From K.M.</label>
                            <div class="col-sm-4" style="width: 63%;">
                                <asp:TextBox ID="txtFromKm" runat="server" CssClass="form-control" PlaceHolder="Enter From K.M." MaxLength="7" Enabled="true"></asp:TextBox>
                                </div>   
                                <div class="col-sm-1" style="width: 10%;">
                                <i class="btn btn-sm btn-primary acc_home" onclick="$('#dvKM').modal('show');"><i class="fa fa-file"></i></i>
                                 <asp:LinkButton ID="lnkBtnKM" runat="server" Visible="false" CssClass="btn btn-sm btn-primary acc_home" OnClick="lnkBtnKM_OnClick"><i class="fa fa-file"></i></asp:LinkButton>
                                 </div>                             
                            </div>
                        </div>
                            <div class="clearfix even_row">
                             <div class="col-sm-3">
                              <label class="col-sm-3 control-label" style="width: 27%;">Agent</label>
                              <div class="col-sm-9" style="width: 61%;">
                                <asp:DropDownList ID="ddlParty" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                              </div>
                              <div class="col-sm-1" style="width: 10%;">
                              <span id="SpanAgentRefresh" runat="server" visible="true">
                                <asp:LinkButton ID="lnkBtnAgent" runat="server" OnClick="lnkBtnAgent_Click" class="btn-sm btn btn-primary acc_home"><i class="fa fa-refresh"></i></asp:LinkButton>
                              </span>
                              </div>
                            </div>
                            <div class="col-sm-3">
                              <label class="col-sm-3 control-label" style="width: 23%;">Type</label>
                              <div class="col-sm-9" style="width: 57%;margin-left:4%">
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                    <asp:ListItem Text="Item Wise" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Fixed Amount" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                              </div>
                            </div>
                            <div class="col-sm-3" id="DivAmount" visible="true" runat="server" style="width:16%">
                            <label class="col-sm-3 control-label" style="width:28%;">Amnt</label>
                            <div class="col-sm-9" style="width:67%;">
                                <asp:TextBox ID="txtFixedAmount" MaxLength="20" runat="server"  CssClass="form-control" PlaceHolder="Enter Amount"></asp:TextBox>     
                            </div>
                            </div>
                            <div class="col-sm-3">
                                <label class="col-sm-3 control-label" style="width: 27%;">Shipmt. No</label>
								<div class="col-sm-1" style="width: 63%;">
								<asp:TextBox ID="txtshipment" runat="server" CssClass="form-control" PlaceHolder="Enter Shipment Number" AutoComplete="off" MaxLength="20"></asp:TextBox>
								</div>
                                 <div class="col-sm-1" style="width: 10%;">
                                 <i class="btn btn-sm btn-primary acc_home" onclick="$('#dvContainerdetails').modal('show');"><i class="fa fa-file"></i></i>
                                 <asp:LinkButton ID="lnkbtnContnrDtl" Visible="false" runat="server" CssClass="btn btn-sm btn-primary acc_home" OnClick="lnkbtnContnrDtl_OnClick"><i class="fa fa-file"></i></asp:LinkButton>
                                 </div>
                            </div>
                                   <div class="col-sm-3">
                                <label class="col-sm-3 control-label" style="width: 27%;">Tax Inv. No.</label>
								<div class="col-sm-1" style="width: 61%;">
								<asp:TextBox ID="txtTaxInvoiceNo" runat="server" CssClass="form-control" PlaceHolder="Enter Tax Invoice Number" AutoComplete="off" MaxLength="20"></asp:TextBox>
								</div>
                            </div>
                          </div>
                        <div class="clearfix odd_row">
                       <div class="col-sm-3">
                                <label class="col-sm-3 control-label" style="width: 27%;">Tax Inv. Date</label>
								<div class="col-sm-1" style="width: 61%;margin-left: -2px;">
								<asp:TextBox ID="txtInvDate" runat="server" CssClass="input-sm datepicker form-control"  PlaceHolder="DD-MM-YYYY" onkeydown = "return DateFormat(this, event.keyCode)" ClientIDMode="Static"  AutoComplete="off" MaxLength="20"></asp:TextBox>
								</div>
                            </div>
                            <div class="col-sm-3">
                                <label class="col-sm-3 control-label" style="width: 23%;">Exc. Inv. No.</label>
								<div class="col-sm-1" style="width: 57%;margin-left:4%">
								<asp:TextBox ID="txtExcInvoceNO" runat="server" CssClass="form-control" PlaceHolder="Enter Excise Invoice Number" AutoComplete="off" MaxLength="20"></asp:TextBox>
								</div>
                            </div>
                            <div class="col-sm-3">
                                <label class="col-sm-3 control-label" style="width: 27%;">E-Way Bill No.</label>
								<div class="col-sm-1" style="width: 63%;">
								<asp:TextBox ID="txtEWayBillNo" runat="server" CssClass="form-control" PlaceHolder="Enter E-Way Bill Number" AutoComplete="off" MaxLength="20"></asp:TextBox>
								</div>
                            </div>
                            </div>
                             <div class="clearfix even_row">
                        <div class="12">
                            <label class="col-sm-3 control-label" style="width:9%;">Remarks</label>
                            <div class="col-sm-8" style="width:84.5%;">
                            <asp:TextBox ID="TxtRemark" runat="server" CssClass="form-control" PlaceHolder="Enter Remarks" MaxLength="100" Enabled="true"></asp:TextBox>     
                            </div>
                             <div id="RemarkDiv" visible="false" runat="server" class="col-sm-1" style="width: 4%;">
                                    <asp:LinkButton ID="lnkRemark" runat="server" ToolTip="Update Remark." OnClick="lnkbtnRemarkUpdate_Click" class="btn-sm btn btn-primary acc_home"><i class="fa fa-check"></i></asp:LinkButton>
                                  </div>
                            </div>
                        </div>

                        </div>
                      </section>
        </div>
        <!-- second  section -->
        <div class="clearfix second_section" id="DivItemPanel" runat="server">
            <section class="panel panel-in-default">  
                        <div class="panel-body" >
                          <div class="clearfix even_row">
                            <div class="col-sm-2">
                              <label class="control-label">Item Name<span class="required-field">*</span></label>
                              <div>
                                <asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control"  OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPartno" runat="server" ControlToValidate="ddlItemName" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="ValueSubmit"
                                    ErrorMessage="Select Item Name." CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div id="divGrade" runat="server" class="col-sm-2" style="width:12%">
                              <label class="control-label">Item Grade<span class="required-field">*</span></label>
                              <div>
                                <asp:DropDownList ID="ddlItemGrade" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rvfItemGrade" runat="server" ControlToValidate="ddlItemGrade" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="ValueSubmit"
                                    ErrorMessage="Choose Item Grade" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                           	<div class="col-sm-2" style="width:11%">
                              <label class="control-label">Unit<span class="required-field">*</span></label>
                              <div>
                                <asp:DropDownList ID="ddlunitname" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvAmnt" runat="server" ControlToValidate="ddlunitname" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="ValueSubmit"
                                    ErrorMessage="Choose Unit Name." CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-2" style="width:10%">
                              <label class="control-label">Rate Type<span class="required-field">*</span></label>
                              <div>
                              <asp:DropDownList ID="ddlRateType" AutoPostBack="false" onchange="javascript:OnChangeddlRateType();" runat="server" CssClass="form-control" >
                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Qty" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Weight" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                              </div>
                            </div>
                            <div class="col-sm-2" style="width:8%">
                              <label class="control-label">Quantity<span class="required-field">*</span></label>
                              <div>
                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"  MaxLength="6"
                                Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtQuantity_TextChanged"
                                onKeyPress="return checkfloat(event, this);" oncopy="return false" onpaste="return false"
                                oncut="return false" oncontextmenu="return false">1</asp:TextBox>

                              </div>
                            </div>
                            <div class="col-sm-2" style="width:10%" ID="DivWight" runat="server">
                              <label class="control-label">Weight<span class="required-field">*</span></label>
                              <div>
                                <asp:TextBox ID="txtweight" runat="server" CssClass="form-control" MaxLength="10"
                                 onKeyPress="return checkfloat(event, this);" oncopy="return false"
                                onpaste="return false" oncut="return false" oncontextmenu="return false" 
                                      ontextchanged="txtweight_TextChanged" AutoPostBack="false"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="rfvWeight" runat="server" ControlToValidate="txtweight" Display="Dynamic" SetFocusOnError="true"   ValidationGroup="ValueSubmit"
                                    ErrorMessage="Please Enter Weight!" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                           	<div class="col-sm-2" style="width:10%"  ID="DivRate" runat="server">
                              <label class="control-label">Rate<span class="required-field">*</span></label>
                              <div>
                                <asp:TextBox ID="txtrate" runat="server" CssClass="form-control" MaxLength="10"
                                    onKeyPress="return checkfloat(event, this);" oncopy="return false"
                                    onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtrate" runat="server" ControlToValidate="txtrate" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Submit"
                                    ErrorMessage="Enter Rate." CssClass="classValidation"></asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="CvtxtRate" runat="server" ControlToValidate="txtrate" OnServerValidate="CvtxtRate_ServerValidate"
                                    CssClass="classValidation" ErrorMessage="Rate Cannot Be Zero." />
                              </div>
                            </div>
                              <div class="col-sm-2" style="width:10%"  ID="Divunloading" runat="server">
                              <label class="control-label">U/L(%)<span class="required-field">*</span></label>
                              <div>
                                <asp:TextBox ID="txtul" runat="server" CssClass="form-control" Text="0" MaxLength="3"
                                    oncopy="return false"
                                    onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                              </div>
                            </div>
                            <div class="col-sm-2" style="width:10%" id="DivCommission" runat="server" visible="false">
                              <label class="control-label">Commission</label>
                              <div>
                                <asp:TextBox ID="txtItmCommission" runat="server" Text="0.00" CssClass="form-control" MaxLength="10"
                                    onKeyPress="return checkfloat(event, this);" Width="102px" oncopy="return false"
                                    onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                              </div>
                            </div>
                            <div class="col-sm-1" style="width: 2%" id="DivCommissionUpdatebtn" runat="server" visible="false">
                            <label class="control-label">&nbsp;</label>
                              <div style="padding-top:30%">
                                <asp:ImageButton ID="imgComUpdate" runat="server" ForeColor="AliceBlue"   ToolTip="Update Commission in Master" 
                                    ImageUrl="~/Images/plus.gif" Width="15px" onclick="imgComUpdate_Click" />
                              </div>
                              </div>
                                 <div class="col-sm-2" style="width:10%" id="Div1" runat="server">
                              <asp:Label ID="lblPrevBalVal" runat="server" class="control-label" Text="Prev Bal" Visible="false"></asp:Label>
                              <div>
                                <asp:TextBox ID="txtPrevBal" runat="server" Text="0.00" CssClass="form-control" MaxLength="10" Enabled="false" Visible="false"
                                    onKeyPress="return checkfloat(event, this);" Width="102px" oncopy="return false"
                                    onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                              </div>
                            </div>
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-9">	                                
                                <label class="col-sm-1 control-label">Detail</label>
                                <div class="col-sm-11">
                                <asp:TextBox ID="txtdetail" PlaceHolder="Enter Detail" runat="server" CssClass="form-control" MaxLength="150"></asp:TextBox><%--DB-Max length 200--%>
                                </div> 
                            </div>
                            <div class="col-sm-3">
                                <div class="col-sm-6">
                                    <asp:LinkButton ID="lnkbtnSubmit" runat="server" style="margin-top:0px;" OnClick="lnkbtnSubmit_OnClick" CssClass="btn full_width_btn btn-sm btn-primary subnew"  ToolTip="Click to Submit" CausesValidation="true" ValidationGroup="Submit" >Submit</asp:LinkButton>
                                </div>
                                <div class="col-sm-6">
                                    <asp:LinkButton ID="lnkbtnAdd" runat="server" style="margin-top:0px;" CausesValidation="false"  OnClick="lnkbtnAdd_OnClick" CssClass="btn full_width_btn btn-sm btn-primary subnew" ToolTip="Click to new" >New</asp:LinkButton>
                                </div>
                            </div>
                          </div>                            
                        </div>
                      </section>
        </div>
        <div class="clearfix third_right">
            <section class="panel panel-in-default">                            
                        <div class="panel-body" style="overflow-x:auto">     
                           <asp:GridView ID="grdMain" runat="server" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="display nowrap dataTable"
                                    Width="100%" BorderStyle="None" BorderWidth="0"  OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand"
                                    OnRowDataBound="grdMain_RowDataBound" PageSize="50">
                                    <RowStyle CssClass="odd" />
                                    <AlternatingRowStyle CssClass="even" />    
                                <Columns>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle CssClass="gridHeaderAlignCenter" />
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("id") %>' CommandName="cmdedit" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("id") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-CssClass="gridHeaderAlignCenter">
                                        <ItemStyle Width="50" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignLeft" HeaderStyle-Width="150" HeaderText="Item Name">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                            <%#Eval("Item_Name")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignLeft" HeaderStyle-Width="150" HeaderText="Item Grade">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                            <%#Eval("Grade_Name")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignLeft" HeaderStyle-Width="150" HeaderText="Unit Name">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                            <%#Eval("Unit_Name")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignLeft" HeaderStyle-Width="150" HeaderText="Rate Type">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                            <%#Eval("Rate_Type")%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="100" HeaderText="Quantity">
                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                        <ItemTemplate>
                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Quantity")))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblQuantity" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="100" HeaderText="Weight">
                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                        <ItemTemplate>
                                            <%#String.Format("{0:0,0.000}", Convert.ToDouble(Eval("Weight")=="" ? 0:Convert.ToDouble(Eval("Weight"))))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblWeight" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" Visible="false" HeaderStyle-Width="100" HeaderText="Prev Qty">
                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                        <ItemTemplate>
                                            <%#(string.IsNullOrEmpty(Convert.ToString(Eval("PREV_QTY"))) ? "0" : (Convert.ToDouble((Eval("PREV_QTY"))).ToString("N2")))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblPrevRcvd" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" Visible="false" HeaderStyle-Width="100" HeaderText="Prev Bal">
                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                        <ItemTemplate>
                                            <%#(string.IsNullOrEmpty(Convert.ToString(Eval("PREV_BAL"))) ? "0" : (Convert.ToDouble((Eval("PREV_BAL"))).ToString("N2")))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblPrevBal" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="100" HeaderText="Rate">
                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                        <ItemTemplate>
                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Rate")))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblRate" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="100" HeaderText="Amount">
                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                        <ItemTemplate>
                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Amount")))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="100" HeaderText="U/L(%)">
                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                        <ItemTemplate>
                                         <%#Convert.ToString(Eval("UnloadWeight"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignLeft" HeaderStyle-Width="90" HeaderText="Detail">
                                        <ItemStyle HorizontalAlign="Left" Width="90" />
                                        <ItemTemplate>
                                            <%#Eval("Detail")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                </asp:GridView>
                        </div>
                      </section>
        </div>
        <div class="clearfix" id="GrDetails" runat="server">
            <section class="panel panel-in-default">                            
                        <div class="panel-body">     
                          <div class="clearfix even_row">
                            <div class="col-sm-3">
                              <label class="col-sm-5 control-label">GrossAmnt</label>
                              <div class="col-sm-7">
                                <asp:TextBox ID="txtGrossAmnt" runat="server" CssClass="form-control"  Text="0.00" Style="text-align: right;" Enabled="False" OnTextChanged="txtGrossAmnt_TextChanged"
                                    onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                              </div>
                            </div>
                           	<div class="col-sm-3">
                              <label class="col-sm-4 control-label">Cartage</label>
                              <div class="col-sm-8">
                                <asp:TextBox ID="txtCartage" runat="server" CssClass="form-control" Text="0.00"  MaxLength="7" Enabled="true" AutoPostBack="true" Style="text-align: right;"
                                    onKeyPress="return checkfloat(event, this);" OnTextChanged="txtCartage_TextChanged"
                                    oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                              </div>
                            </div>
                            <div class="col-sm-3">
                              <label class="col-sm-4 control-label">Total</label>
                              <div class="col-sm-8">
                                <asp:TextBox ID="txtTotalAmnt" runat="server" CssClass="form-control"  Text="0.00"
                                    Style="text-align: right;" Enabled="False" MaxLength="10" onKeyPress="return checkfloat(event, this);" OnTextChanged="txtTotalAmnt_TextChanged"></asp:TextBox>
                              </div>
                            </div>
                           	<div class="col-sm-3">
                              <label class="col-sm-6 control-label">Surcharge</label>
                              <div class="col-sm-6">
                                <asp:TextBox ID="txtSurchrge" runat="server" CssClass="form-control" Text="0.00" AutoPostBack="true" Style="text-align: right;" Enabled="false" MaxLength="8"
                                    onKeyPress="return checkfloat(event, this);" OnTextChanged="txtSurchrge_TextChanged"
                                    oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                              </div>
                            </div>			                              
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-3">
                              <label class="col-sm-5 control-label">Commission</label>
                              <div class="col-sm-7">
                                <asp:TextBox ID="txtCommission" runat="server" CssClass="form-control"  Text="0.00" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"
                                    MaxLength="7" AutoPostBack="true" Enabled="true" Style="text-align: right;"
                                    onKeyPress="return checkfloat(event, this);" OnTextChanged="txtCommission_TextChanged"></asp:TextBox>
                              </div>
                            </div>
                           	<div class="col-sm-3">
                              <label class="col-sm-4 control-label">Bilty</label>
                              <div class="col-sm-8">
                                <asp:TextBox ID="txtBilty" runat="server" CssClass="form-control" Text="0.00"
                                    oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"
                                    MaxLength="7" AutoPostBack="true" Style="text-align: right;" Enabled="true"
                                    onKeyPress="return checkfloat(event, this);" OnTextChanged="txtBilty_TextChanged"></asp:TextBox>
                              </div>
                            </div>
                            <div class="col-sm-3">
                            <b><asp:Label ID="lbltxtwages" class="col-sm-4 control-label" runat="server" Text="Wages"></asp:Label></b>
                              <div class="col-sm-8">
                                <asp:TextBox ID="txtWages" runat="server" CssClass="form-control"  MaxLength="7"
                                    Enabled="true" AutoPostBack="true" Text="0.00" Style="text-align: right;"
                                    onDrop="blur();return false;" onpaste="return false"
                                    oncontextmenu="return false" oncut="return false" oncopy="return false" onKeyPress="return checkfloat(event, this);"
                                    OnTextChanged="txtWages_TextChanged"></asp:TextBox>
                              </div>
                            </div>
                           	<div class="col-sm-3">
                              <b><asp:Label ID="lbltxtPF" class="col-sm-6 control-label" runat="server" Text="PF"></asp:Label></b>
                              <div class="col-sm-6">
                                <asp:TextBox ID="txtPF" runat="server" CssClass="form-control"  MaxLength="7" AutoPostBack="true"
                                    Text="0.00" Enabled="true" Style="text-align: right;" 
                                    oncontextmenu="return false" onDrop="blur();return false;" onpaste="return false"
                                    oncut="return false" oncopy="return false" onKeyPress="return checkfloat(event, this);"
                                    OnTextChanged="txtPF_TextChanged"></asp:TextBox>
                              </div>
                            </div>			                              
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-3">
                              <b><asp:Label ID="lbltxtTolltax" class="col-sm-5 control-label" runat="server" Text="Toll Tax"></asp:Label></b>
                              <div class="col-sm-7">
                                <asp:TextBox ID="txtTollTax" runat="server" CssClass="form-control"  Text="0.00"
                                        oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"
                                        MaxLength="7" AutoPostBack="true" Style="text-align: right;" Enabled="true"
                                        onKeyPress="return checkfloat(event, this);" OnTextChanged="txtTollTax_TextChanged"></asp:TextBox>
                              </div>
                            </div>
                           	<div class="col-sm-3">
                              <label class="col-sm-4 control-label">Sub Total</label>
                              <div class="col-sm-8">
                                <asp:TextBox ID="txtSubTotal" runat="server" CssClass="form-control"  Text="0.00"
                                    AutoPostBack="true" Style="text-align: right;" Enabled="False"
                                    onKeyPress="return checkfloat(event, this);" OnTextChanged="txtSubTotal_TextChanged"></asp:TextBox>
                              </div>
                            </div>
                            <div class="col-sm-3">
                                <asp:Panel runat="server" ID="pnlServiceTax">
                                    <label class="col-sm-4 control-label">Serv. Tax</label>
                                    <div class="col-sm-8">
                                    <asp:TextBox ID="txtServTax" runat="server" CssClass="form-control"  MaxLength="7"
                                            Enabled="false" Text="0.00" Style="text-align: right;"
                                            onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"
                                            onKeyPress="return checkfloat(event, this);" OnTextChanged="txtServTax_TextChanged"></asp:TextBox>
                                    </div>
                                </asp:Panel>
                                 <asp:Panel runat="server" ID="pnlSGST">
                                    <label class="col-sm-4 control-label">SGST</label>
                                      <div class="col-sm-8">
                                        <asp:TextBox ID="txtSGSTAmnt" OnTextChanged="SGST_Changed" runat="server" CssClass="form-control" MaxLength="7"
                                            Text="0.00" Style="text-align: right;"
                                            onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"
                                            onKeyPress="return checkfloat(event, this);" AutoPostBack="True"></asp:TextBox>
                                    </div>
                                </asp:Panel>
                            </div>
                           	<div class="col-sm-3">
                                <asp:Panel runat="server" ID="pnlSwatchBharatTax">
                                    <label class="col-sm-6 control-label">SwachhBhrt Tax</label>
                                    <div class="col-sm-6">
                                    <asp:TextBox ID="txtSwchhBhartTx" runat="server" CssClass="form-control"  MaxLength="7"
                                            Enabled="false" Text="0.00" Style="text-align: right;"
                                            onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"
                                            onKeyPress="return checkfloat(event, this);" OnTextChanged="txtSwchhBhartTx_OnTextChanged"></asp:TextBox>
                                    </div>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlCGST">
                                    <label class="col-sm-6 control-label">CGST</label>
                                      <div class="col-sm-6">
                                        <asp:TextBox ID="txtCGSTAmnt" OnTextChanged="CGST_Changed" runat="server" CssClass="form-control" MaxLength="7"
                                            Text="0.00" Style="text-align: right;"
                                            onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"
                                            onKeyPress="return checkfloat(event, this);" AutoPostBack="True"></asp:TextBox>
                                    </div>
                                </asp:Panel>
                            </div>			                              
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-3">
                                <asp:Panel runat="server" ID="pnlKrishiTax">
                                    <label class="col-sm-5 control-label">Krishi Kalyan Tax</label>
                                      <div class="col-sm-7">
                                        <asp:TextBox ID="txtkalyan" runat="server" CssClass="form-control" MaxLength="7"
                                            Enabled="false" Text="0.00" Style="text-align: right;"
                                            onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"
                                            onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                    </div>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlIGST">
                                    <label class="col-sm-5 control-label">IGST</label>
                                      <div class="col-sm-7">
                                        <asp:TextBox ID="txtIGSTAmnt" OnTextChanged="IGST_Changed" runat="server" CssClass="form-control" MaxLength="7"
                                            Text="0.00" Style="text-align: right;"
                                            onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"
                                            onKeyPress="return checkfloat(event, this);" AutoPostBack="True"></asp:TextBox>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="col-sm-3"></div>
                            <div class="col-sm-3">
                              <label class="col-sm-4 control-label">RoundOff</label>
                              <div class="col-sm-8">
                                <asp:TextBox ID="TxtRoundOff" runat="server" CssClass="form-control" MaxLength="7"
                                    Enabled="false" Text="0.00" Style="text-align: right;"
                                    onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"
                                    onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                              </div>
                            </div>
                           	<div class="col-sm-3">
                              <label class="col-sm-6 control-label">Net Amount</label>
                              <div class="col-sm-6">
                                <asp:TextBox ID="txtNetAmnt" runat="server" CssClass="form-control" MaxLength="7"
                                        Enabled="false" Text="0.00" Style="text-align: right;"
                                        onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"
                                        onKeyPress="return checkfloat(event, this);" OnTextChanged="txtNetAmnt_TextChanged"></asp:TextBox>
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
                            <div class="col-lg-offset-2">    
                              <div class="col-sm-2">
                              <asp:LinkButton ID="LnkBtnNew" runat="server" 
                                      class="btn full_width_btn btn-s-md btn-info"  
                                      onclick="LnkBtnNew_Click" ><i class="fa fa-th-list"></i>New</asp:LinkButton>
                              </div>                                     
                              <div class="col-sm-2" id="DivSave" runat="server">
                                <asp:LinkButton ID="lnkbtnSave" runat="server" CssClass="btn full_width_btn btn-s-md btn-success" OnClick="lnkbtnSave_OnClick" CausesValidation="true" ValidationGroup="save" > <i class="fa fa-save"></i>Save</asp:LinkButton>
                                <asp:HiddenField ID="GrRestrictDate" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hidToStateIdno" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hidToStateName" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hidpbillid" runat="server" />
                                    <asp:HiddenField ID="hidDelvIdno" runat="server" />
                                    <asp:HiddenField ID="hidGRHeadIdno" runat="server" />
                                    <asp:HiddenField ID="hidpostingmsg" runat="server" />
                              </div>
                              <div class="col-sm-2">
                                 <asp:LinkButton ID="lnkbtnCancel" runat="server" CssClass="btn full_width_btn btn-s-md btn-danger"  OnClick="lnkbtnCancel_OnClick" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
                                <asp:HiddenField ID="hidmindate" runat="server" />
                                <asp:HiddenField ID="hidmaxdate" runat="server" />
                                <asp:HiddenField ID="hidShrtgRate" runat="server" OnValueChanged="hidShrtgRate_ValueChanged" />
                                <asp:HiddenField ID="hidShrtgLimit" runat="server" />
                                <asp:HiddenField ID="hidShrtgLimitOther" runat="server" />
                                <asp:HiddenField ID="hidShrtgRateOther" runat="server" />
                                <asp:HiddenField ID="HidGrAgnstRcptIdno" runat="server" />
                                <asp:HiddenField ID="hidTBBType" runat="server" />
                                <asp:HiddenField ID="HiddSurchgPer" runat="server" />
                                <asp:HiddenField ID="HiddWagsAmnt" runat="server" />
                                <asp:HiddenField ID="HiddBiltyAmnt" runat="server" />
                                <asp:HiddenField ID="HiddTolltax" runat="server" />
                                <asp:HiddenField ID="HiddServTaxValid" runat="server" />
                                <asp:HiddenField ID="Hiditruckcitywise" runat="server" />
                                <asp:HiddenField ID="HidiFromCity" runat="server" />
                                <asp:HiddenField ID="HidsRenWages" runat="server" />
                                <asp:HiddenField ID="HiddServTaxPer" runat="server" />
                                <asp:HiddenField ID="HiddSwachhBrtTaxPer" runat="server" />
                                <asp:HiddenField ID="HiddKalyanTax" runat="server" />
                                <asp:HiddenField ID="HiddTruckIdno" runat="server" />
                                <asp:HiddenField ID="HiddConSize" runat="server" />
                                <asp:HiddenField ID="HiddUserPrefCont" runat="server" />
                                <asp:HiddenField ID="hidAdvOrdrQty" runat="server" />
                                <asp:HiddenField ID="hidAdvOrdrWght" runat="server" />
                                <asp:HiddenField ID="hidRenamePF" runat="server" />
                                <asp:HiddenField ID="hidrefrename" runat="server" />
                                <asp:HiddenField ID="hidRenameToll" runat="server" />
                                <asp:LinkButton ID="lnkbtn" runat="server" Text="" OnClick="lnkbtn_Click"></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnAtSave" runat="server" Text="" OnClick="lnkbtnAtSave_Click"></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnAtSave1" runat="server" Text="" OnClick="lnkbtnAtSave1_Click"></asp:LinkButton>
                                <asp:HiddenField ID="HidePF" runat="server" />
                                <asp:HiddenField ID="HideTolltax" runat="server" />
                                <asp:HiddenField ID="hidDelPlace" runat="server" />
                                <%--Upadhyay--%>
                                <asp:HiddenField ID="hidGstType" runat="server" />
                                <asp:HiddenField ID="hidSGSTPer" runat="server" />
                                <asp:HiddenField ID="hidCGSTPer" runat="server" />
                                <asp:HiddenField ID="hidIGSTPer" runat="server" />
                                <asp:HiddenField ID="hidGSTCessPer" runat="server" />
                                <asp:HiddenField ID="HidGstCalgr" runat="server" />
                                <asp:HiddenField ID="hidSGSTAmt" runat="server" />
                                <asp:HiddenField ID="hidCGSTAmt" runat="server" />
                                <asp:HiddenField ID="hidIGSTAmt" runat="server" />
                                <asp:HiddenField ID="hidGSTCessAmt" runat="server" />
                              </div>
                            <div id="DivExcelUpload" runat="server" class="col-sm-2">
                              <asp:LinkButton ID="lnkbtnExcelUpload" runat="server" CssClass="btn full_width_btn btn-s-md btn-info"   data-toggle="modal" data-target="#Upload_div"><i  CausesValidation="true" class="fa fa-upload"></i>Import Excel</asp:LinkButton>
                           </div>
                              <div class="col-sm-2" id= "divPosting" runat="server">
                                <button type="button" id="btnAccPost" runat="server" class="btn full_width_btn btn-s-md btn-info" style="height:32px" data-toggle="modal" data-target="#acc_posting"><i class="fa fa-th-list">Acc Posting</i></button>
                              </div>
                             <div class="col-sm-1">
                                <div title="User Preference setting" class="btn-setting"></div>
                            </div>
                            </div>
                          </div> 
                          <div class="clearfix odd_row">
                              <div class="col-lg-12">
                                <asp:Label ID="lblmessage" runat="server" Font-Bold="true" Visible="false" CssClass="classValidation"
                                            Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                      </section>
        </div>
        <!-- For Container Details Popup -->
        <div id="Upload_div" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="popform_header">
                            Upload Excel</h4>
                    </div>
                    <div class="modal-body">
                        <section class="panel panel-default full_form_container material_search_pop_form">
								            <div class="panel-body">
									            <!-- First Row start -->
								            <div class="clearfix odd_row">	       
                                            <div class="col-sm-2">
	                                            <label class="control-label">From Excel</label>
                                             </div>
							                     <div class="col-sm-3" style="width:48%">
                                                <asp:FileUpload ID="FileUpload"  runat="server"  Width="200px" />
							                </div> 
							                <div class="col-sm-2">
                                                 <asp:LinkButton ID="lnkbtnUpload"  ToolTip="PerfNo,GRNo,GRDate,GrType,FromCity,ToCity,TruckNo, ItemName,Unit,RateType,Qty,Weight,Rate,ItemDetails, Sender,Receiver,DeliveryPlace,ViaCity,DlNo,EGPNo, ShipmentNo,Remark,FixedAmount,ReceiptType,InstNo,InstDate,CustBank,RefNo,OrderNo,FormNo" runat="server" CssClass="btn full_width_btn btn-sm btn-primary" OnClick="lnkbtnUpload_OnClick" ><i class="fa fa-upload"></i>Upload</asp:LinkButton>
                                           </div>
	                                        </div>
                                            <div class="clearfix even_row">
                                            <div class="col-sm-2">
                                                <label class="control-label">Type</label>
                                            </div>
                                            <div class="col-sm-3" style="width:28%">
                                            <asp:DropDownList ID="ddlExcelUploadTypeWise" Width="130px" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Item Wise" Value="1" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Fixed Amount" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                            </div> 
                                            <div class="col-sm-6">
                                                <span class="required-field"><label id="lblExcelMessage" runat="server" class="control-label"></label></span>
                                            </div>
                                            </div>                                      
                                            </div>
                                            </section>
                    </div>
                    <div class="modal-footer">
                        <div class="popup_footer_btn">
                            <button type="submit" class="btn btn-dark" data-dismiss="modal">
                                <i class="fa fa-times"></i>Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="type_popup" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="popform_header">
                            Receipt Details</h4>
                    </div>
                    <div class="modal-body">
                        <section class="panel panel-default full_form_container material_search_pop_form">
										          <div class="panel-body">
										            <div class="clearfix odd_row">	                                
	                                <div class="col-sm-4">
	                                 <label class="col-sm-3 control-label" style="width: 27%;">Inst.No.</label>
                                     <div class="col-sm-9" style="width: 63%;">
                                           <asp:TextBox ID="txtInstNo" runat="server" CssClass="form-control" MaxLength="6" Placeholder="Inst. Number">0</asp:TextBox>
                                           <asp:RequiredFieldValidator ID="rfvinstno" runat="server" ControlToValidate="txtInstNo"
                                                        CssClass="classValidation" Display="Dynamic" ErrorMessage="Enter Inst. No." 
                                                        SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>     
                                       </div>
	                                </div>
	                               <div class="col-sm-4" style="width: 30%">
                           		      <label class="col-sm-3 control-label" style="width: 37%;">Inst.Date</label>
                                        <div class="col-sm-9" style="width: 63%;">
                                             <asp:TextBox ID="txtInstDate" runat="server" CssClass="input-sm datepicker form-control" ></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="rfvinstdate" runat="server" ControlToValidate="txtInstDate"
                                                            CssClass="classValidation" Display="Dynamic" ErrorMessage="Enter Inst. Date" 
                                                            SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                                        </div>
                           	         </div>
                                      <div class="col-sm-4" style="width: 33%">
                                           <label class="col-sm-3 control-label" style="width: 40%;">Cust.Bank</label>
							               <div class="col-sm-9" style="width: 60%;">
							                         <asp:DropDownList ID="ddlcustbank" runat="server" CssClass="form-control" >
                                                     </asp:DropDownList>
                                             <asp:RequiredFieldValidator ID="rfvddlcustbank" runat="server" ControlToValidate="ddlcustbank"
                                               CssClass="classValidation" Display="Dynamic" ErrorMessage="Select Cust. Bank" InitialValue="0"
                                               SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                                              <asp:HiddenField ID="hidrowid" runat="server" />
                                              <asp:HiddenField ID="hidratetype" runat="server" />
							             </div>
                                       </div>

	                              </div>	                             	                              
										          </div>
										        </section>
                    </div>
                    <div class="modal-footer">
                        <div class="popup_footer_btn">
                            <button type="submit" class="btn btn-dark" data-dismiss="modal">
                                <i class="fa fa-check"></i>OK</button>
                            <button type="submit" class="btn btn-dark" data-dismiss="modal">
                                <i class="fa fa-times"></i>Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="dvContainerdetails" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="popform_header">
                            Container Detail
                        </h4>
                    </div>
                    <div class="modal-body">
                        <section class="panel panel-default full_form_container material_search_pop_form">
										          <div class="panel-body">
										             <!-- First Row start -->
										            <div class="clearfix odd_row">	                                
	                                                    <div class="col-sm-6">
	                                                      <label class="col-sm-5 control-label">Container No 1</label>
                                                        <div class="col-sm-7">
                                                           <asp:TextBox ID="txtContainrNo" PlaceHolder="Container Number" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                                           
                                                        </div>
	                                                    </div>
                                                         <div class="col-sm-6">
	                                                        <label class="col-sm-4 control-label">Seal No 1</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtContainerSealNo" PlaceHolder="Container Seal No" runat="server" CssClass="form-control" MaxLength="15" ></asp:TextBox>     
                                                        </div>
	                                                    </div>
                                            
	                                                    </div> 
										            <div class="clearfix even_row">	                                
	                                                    <div class="col-sm-6">
	                                                      <label class="col-sm-5 control-label">Container No 2</label>
                                                        <div class="col-sm-7">
                                                           <asp:TextBox ID="txtContainrNo2" PlaceHolder="Container Number" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                                           
                                                        </div>
	                                                    </div>
                                                         <div class="col-sm-6">
	                                                        <label class="col-sm-4 control-label">Seal No 2</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtContainerSealNo2" PlaceHolder="Container Seal No" runat="server" CssClass="form-control" MaxLength="15" ></asp:TextBox>     
                                                        </div>
	                                                    </div>
                                            
	                                                    </div> 
                                                    

                                                        <div class="clearfix odd_row">	                                
	                                            <div class="col-sm-6">
	                                                <label class="col-sm-5 control-label">Type</label>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddlContainerType" runat="server" CssClass="form-control" ></asp:DropDownList>
                                                        </div>
	                                                    </div>
	                                                   <div class="col-sm-6">
	                                                        <label class="col-sm-4 control-label">Size</label>
                                                        <div class="col-sm-8">                                                                
                                                            <asp:DropDownList ID="ddlContainerSize" runat="server"  CssClass="form-control"></asp:DropDownList>
                                     
                                                        </div>
	                                                    </div>
                                                        </div>
                                                        <div class="clearfix even_row">	                                
	                                                    <div class="col-sm-6">
	                                                      <label class="col-sm-5 control-label">Port</label>
                                                        <div class="col-sm-7">
                                                           <asp:TextBox ID="txtPortNum" PlaceHolder="Port" runat="server" CssClass="form-control" MaxLength="15" ></asp:TextBox>
                                                        </div>
	                                                    </div>
	                                                    <div class="col-sm-6">
	                                                <label class="col-sm-5 control-label">Type</label>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddlTypeI" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTypeI_OnSelectedIndexChanged" >
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Import" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Export" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                        </div>
                                            
	                                                    </div> 
                                                        <div class="clearfix odd_row">	                                
	                                            <div class="col-sm-6"> 
	                                               <asp:Label ID="lblTypeI" runat="server" Text="Select"   CssClass="col-sm-5"></asp:Label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtNameI"  runat="server" CssClass="form-control" MaxLength="100" ></asp:TextBox>
                                                        </div>
	                                                    </div> 
                                                  
									        </div>
								        </section>
                    </div>
                    <div class="modal-footer">
                        <div class="popup_footer_btn">
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <asp:LinkButton ID="lnkbtnContainerSubmit" runat="server" CssClass="btn btn-dark"
                                OnClick="lnkbtnContainerSubmit_OnClick"><i class="fa fa-check"></i>Ok</asp:LinkButton>
                            &nbsp;&nbsp
                            <asp:LinkButton ID="lnkbtnClose" runat="server" CssClass="btn btn-dark" OnClick="lnkbtnClose_OnClick"><i class="fa fa-times"></i>Close</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="dvKM" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="popform_header">
                            Kilometer Details</h4>
                    </div>
                    <div class="modal-body">
                        <section class="panel panel-default full_form_container material_search_pop_form">
										          <div class="panel-body">
										             <!-- First Row start -->
										            <div class="clearfix odd_row">	                                
	                                                    <div class="col-sm-6">
	                                                      <label class="col-sm-4 control-label">To K.M.</label>
                                                        <div class="col-sm-8">
                                                           <asp:TextBox ID="txtToKM" PlaceHolder="To Kilometer" runat="server" CssClass="form-control" onchange="CalcKM();" ClientIDMode="Static" MaxLength="7"></asp:TextBox>
                                                           
                                                        </div>
	                                                    </div>
                                                         <div class="col-sm-6">
	                                                        <label class="col-sm-4 control-label">Total K.M.</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtTotKM" runat="server" CssClass="form-control" Text="0" MaxLength="7" ClientIDMode="Static" Enabled="false"></asp:TextBox>     
                                                        </div>
	                                                    </div>
                                            
	                                                    </div> 
										           
								        </section>
                    </div>
                    <div class="modal-footer">
                        <div class="popup_footer_btn">
                            <asp:LinkButton ID="lnkbtnKMSubmit" runat="server" CssClass="btn btn-dark"><i class="fa fa-check"></i>Ok</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </form>
    </div>
    </section>
        </div>
        <div class="col-lg-0" style="display: none">
            <tr style="display: block">
                <td class="white_bg" align="center">
                    <div id="print" style="font-size: 13px;">
                        <table cellpadding="1" cellspacing="0" width="800" border="1" style="font-family: Arial,Helvetica,sans-serif;">
                            <tr style="height: 120px; width: 20%">
                                <td align="center" class="white_bg" valign="top" colspan="3" style="font-size: 14px; border-left-style: none; border-right-style: none">
                                    <div style="text-align: left; width: 140px; float: left;">
                                        <asp:Image ID="imgLogoShow" Width="140px" Height="90px" runat="server"></asp:Image>
                                    </div>
                                    <div id="header" runat="server" style="text-align: center; width: 650px; float: center;">
                                        <strong>
                                            <asp:Label ID="lblCompanyname" runat="server" Style="font-size: 18px;"></asp:Label><br />
                                        </strong>
                                        <asp:Label ID="lblCompAdd1" runat="server"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblCompAdd2" runat="server"></asp:Label><br />
                                        <asp:Label ID="lblCompCity" runat="server"></asp:Label>&nbsp;&nbsp;
                                        <asp:Label ID="lblCompState" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblCompCityPin" runat="server"></asp:Label><br />
                                        <asp:Label ID="lblCompPhNo" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblFaxNo" Text="FAX No.:" runat="server"></asp:Label>
                                        <asp:Label ID="lblCompFaxNo" runat="server"></asp:Label><br />
                                        <asp:Label ID="lblTin" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;<asp:Label
                                            ID="lblCompTIN" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                              <asp:Label ID="lblCompGST" runat="server" Text="GSTIN NO:"></asp:Label>&nbsp;<asp:Label
                                            ID="lblCompGSTIN" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lbltxtPanNo" runat="server" Text="PAN NO. :"></asp:Label>&nbsp;&nbsp;<asp:Label
                                            ID="lblPanNo" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px; border-left-style: none; border-right-style: none">
                                    <h3>
                                        <strong style="text-decoration: underline">
                                            <asp:Label ID="lblPrintHeadng" runat="server" Text="Goods Receipt"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblTypeOfGr" runat="server" Text=""></asp:Label>
                                        </strong>
                                    </h3>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table border="0" width="100%">
                                        <tr>
                                            <td align="left" class="white_bg" style="width: 18%; font-size: 13px; border-right-style: none"
                                                valign="top">
                                                <asp:Label ID="lbltxtgrno" runat="server">GR. No.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="width: 22%; font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblGRno" runat="server"></asp:Label></b>
                                            </td>
                                            <td align="left" class="white_bg" style="width: 14%; font-size: 13px; border-right-style: none"
                                                valign="top">
                                                <asp:Label ID="lbltxtgrdate" Text="GR. Date" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblGrDate" runat="server"></asp:Label></b>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <asp:Label ID="Label6" runat="server">Lorry No &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblLorryNo" runat="server"></asp:Label></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="white_bg" style="width: 18%; font-size: 13px; border-right-style: none"
                                                valign="top">
                                                <asp:Label ID="lbltxtFromcity" runat="server">From City&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblFromCity" runat="server"></asp:Label></b>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <asp:Label ID="lbltxttocity" runat="server">To City&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblToCity" runat="server"></asp:Label></b>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <asp:Label ID="lbltxtdelvryPlace" runat="server">Delivery Place&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblDelvryPlace" runat="server"></asp:Label></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="white_bg" style="width: 18%; font-size: 13px; border-right-style: none"
                                                valign="top">
                                                <asp:Label ID="lblViaCity" runat="server">Via City&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblValueViaCity" runat="server"></asp:Label>
                                                </b>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; width: 18%; border-right-style: none">
                                                <asp:Label ID="lblConsName" runat="server">Consignor's Name &nbsp;:</asp:Label>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblvalConsName" runat="server"></asp:Label></b>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <asp:Label ID="lblNameCntnrSize" runat="server">Size &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblContainerSize" runat="server"></asp:Label></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <asp:Label ID="lblNameShipmentno" runat="server">Shipment No. &nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblShipmentNo" runat="server"></asp:Label></b>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <asp:Label ID="lblNameContnrNo" Text="Container No.:" runat="server"></asp:Label>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblContainerNo" runat="server"></asp:Label></b>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <asp:Label ID="lblNameSealNo" runat="server">Seal No. &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblSealNo" runat="server"></asp:Label></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <asp:Label ID="lblDinNoText" runat="server">DI No. &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblDinNo" runat="server"></asp:Label></b>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; width: 18%; border-right-style: none">
                                                <asp:Label ID="lblEGPNo" runat="server">EGP No.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" colspan="0" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblEGPNoval" runat="server"></asp:Label>
                                                </b>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; width: 18%; border-right-style: none">
                                                <asp:Label ID="lblRefNo" runat="server">Ref No.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" colspan="0" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblrefnoval" runat="server"></asp:Label>
                                                </b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="font-size: 13px;">
                                                <asp:Label ID="lblEWayBillNo" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="white_bg" style="width: 18%; font-size: 13px; border-right-style: none"
                                                valign="top">
                                                <asp:Label ID="lblOrderNo" runat="server">Order No&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblOrderNoVal" runat="server"></asp:Label>
                                                </b>
                                            </td>
                                            <td align="left" class="white_bg" style="width: 18%; font-size: 13px; border-right-style: none"
                                                valign="top">
                                                <asp:Label ID="lblFormNo" runat="server">Form No&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblFormNoVal" runat="server"></asp:Label>
                                                </b>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; width: 18%; border-right-style: none">
                                                <asp:Label ID="lbltxtagent" runat="server">Agent &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblAgent" runat="server"></asp:Label></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; width: 18%; border-right-style: none">
                                                <asp:Label ID="lblNameContnrType" runat="server">Type &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblCntnrType" runat="server"></asp:Label></b>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <asp:Label ID="lblTotItem" runat="server">Total Item Value &nbsp;:</asp:Label>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblTotItemValue" runat="server"></asp:Label></b>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%;" valign="top">
                                                <table border="0" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                                    <tr>
                                                        <td colspan="2" style="border-bottom: 1px solid #484848;">
                                                            <strong>Sender's Details:</strong>
                                                        </td>
                                                    </tr>
                                                    <tr id="trConsigneeName" runat="server">
                                                        <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                                            <asp:Label ID="Label1" runat="server">Name</asp:Label>
                                                        </td>
                                                        <td style="font-size: 13px; border-right-style: none">
                                                            <b>
                                                                <asp:Label ID="lblConsigeeName" runat="server"></asp:Label>
                                                                <b /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                                            <asp:Label ID="Label2" runat="server">Address</asp:Label>
                                                        </td>
                                                        <td style="font-size: 13px; border-right-style: none">
                                                            <b>
                                                                <asp:Label ID="lblConsigneeAddress" runat="server"></asp:Label></b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                                            <asp:Label ID="Label5" runat="server" Text="Tin"></asp:Label>
                                                        </td>
                                                        <td style="font-size: 13px; border-right-style: none">
                                                            <b>
                                                                <asp:Label ID="lblConsigneeTin" Text="" runat="server"></asp:Label></b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                                            <asp:Label ID="lblPrtyGST" runat="server" Text="GSTIN NO"></asp:Label>
                                                        </td>
                                                        <td style="font-size: 13px; border-right-style: none">
                                                            <b>
                                                                <asp:Label ID="lblPrtyGSTIN" Text="" runat="server"></asp:Label></b>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 50%;" valign="top">
                                                <table border="0" width="100%" class="white_bg">
                                                    <tr>
                                                        <td colspan="2" style="border-bottom: 1px solid #484848; height: 10px;">
                                                            <strong>Receiver's Details:</strong>
                                                        </td>
                                                    </tr>
                                                    <tr id="trConsignorName" runat="server">
                                                        <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                                            <asp:Label ID="Label3" runat="server">Name</asp:Label>
                                                        </td>
                                                        <td style="font-size: 13px; border-right-style: none">
                                                            <b>
                                                                <asp:Label ID="lblConsignorName" runat="server"></asp:Label></b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                                            <asp:Label ID="Label4" runat="server">Address</asp:Label>
                                                        </td>
                                                        <td style="font-size: 13px; border-right-style: none">
                                                            <b>
                                                                <asp:Label ID="lblConsignorAddress" runat="server"></asp:Label></b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                                            <asp:Label ID="lblPrtyTinTxt" runat="server" Text="Tin"></asp:Label>
                                                        </td>
                                                        <td style="font-size: 13px; border-right-style: none">
                                                            <b>
                                                                <asp:Label ID="lblConsignorTin" Text="" runat="server"></asp:Label></b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                                            <asp:Label ID="lblConsignerGSTINHead" runat="server" Text="GSTIN NO"></asp:Label>
                                                        </td>
                                                        <td style="font-size: 13px; border-right-style: none">
                                                            <b>
                                                                <asp:Label ID="lblConsignerGSTINValue" Text="" runat="server"></asp:Label></b>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table1">
                                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                            <HeaderTemplate>
                                                <tr>
                                                    <td class="white_bg" style="font-size: 12px" width="10%">
                                                        <strong>S.No.</strong>
                                                    </td>
                                                    <td style="font-size: 12px" width="15%">
                                                        <strong>Item Name</strong>
                                                    </td>
                                                    <td style="font-size: 12px" width="15%">
                                                        <strong>Item Grade</strong>
                                                    </td>
                                                    <td style="font-size: 12px" width="10%">
                                                        <strong>Unit Name</strong>
                                                    </td>
                                                    <td style="font-size: 12px" width="5%">
                                                        <strong>Quantity</strong>
                                                    </td>
                                                    <td style="font-size: 12px" width="10%">
                                                        <strong>Weight</strong>
                                                    </td>
                                                    <div id="HideGrhdr" runat="server">
                                                        <td style="font-size: 12px" align="left" width="10%">
                                                            <strong>Item Rate</strong>
                                                        </td>
                                                        <td style="font-size: 12px" align="left" width="10%">
                                                            <strong>Amount</strong>
                                                        </td>
                                                    </div>
                                                    <td style="font-size: 12px" width="20%">
                                                        <strong>Detail</strong>
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="white_bg" width="10%">
                                                        <%#Container.ItemIndex+1 %>.
                                                    </td>
                                                    <td class="white_bg" width="25%">
                                                        <%#Eval("Item_Modl")%>
                                                    </td>
                                                     <td class="white_bg" width="25%">
                                                        <%#Eval("Grade_Name")%>
                                                    </td>
                                                    <td class="white_bg" width="15%">
                                                        <%#Eval("UOM_Name")%>
                                                    </td>
                                                    <td class="white_bg" width="5%">
                                                        <%#Eval("Qty")%>
                                                    </td>
                                                    <td class="white_bg" width="15%">
                                                        <%#String.Format("{0:0.000}", Convert.ToDouble(Eval("Tot_Weght")))%>
                                                    </td>
                                                    <div id="HideGritem" runat="server">
                                                        <td class="white_bg" width="15%" align="left">
                                                            <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Item_Rate")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td class="white_bg" width="15%" align="left">
                                                            <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </div>
                                                    <td class="white_bg" width="15%" align="left">
                                                        <%#(Eval("Detail"))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <tr>
                                                    <td class="white_bg" width="10%">&nbsp;
                                                    </td>
                                                    <td class="white_bg" width="10%">&nbsp;
                                                    </td>
                                                    <td class="white_bg" width="30%">&nbsp;
                                                    </td>
                                                    <td class="white_bg" width="15%">
                                                        <asp:Label ID="lblFTTot" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="white_bg" width="15%">
                                                        <asp:Label ID="lblFTQty" Font-Bold="true" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="white_bg" width="15%">
                                                        <asp:Label ID="lblFTtotalWeight" Font-Bold="true" runat="server"></asp:Label>
                                                    </td>
                                                    <div id="hidfooterdetl" runat="server">
                                                        <td class="white_bg" width="15%" align="left"></td>
                                                        <td class="white_bg" width="15%" align="left">
                                                            <asp:Label ID="lblFTTotalAmnt" Font-Bold="true" runat="server"></asp:Label>
                                                        </td>
                                                    </div>
                                                    <td class="white_bg" width="15%" align="right">&nbsp;
                                                    </td>
                                                </tr>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" align="left" width="50%">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblremark" runat="server" valign="right"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td colspan="3" width="34%">
                                                <table id="lstInfoDiv" runat="server">
                                                    <tr>
                                                        <td>&nbsp;
                                                            <asp:Label ID="lblGrossAmnt" runat="server" Text="Gross Amnt" Font-Size="13px" valign="right"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="valuelblGrossAmnt" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                        <td style="width: 5px"></td>
                                                        <td>&nbsp;
                                                            <asp:Label ID="lblcommission" runat="server" Text="Commission" Font-Size="13px" valign="right"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="valuelblcommission" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                            <asp:Label ID="lblTotal" runat="server" Font-Size="13px" valign="right">Total</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="valuelblTotal" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                        <td style="width: 5px"></td>
                                                        <td>&nbsp;
                                                            <asp:Label ID="lblbilty" runat="server" Font-Size="13px" valign="right">Bilty</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="valuelblbilty" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                            <asp:Label ID="lblcartage" runat="server" Font-Size="13px" valign="right">Cartage</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="valuelblcartage" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                        <td style="width: 5px"></td>
                                                        <td>&nbsp;
                                                            <asp:Label ID="lblsurcharge" runat="server" Font-Size="13px" valign="right">Surcharge</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="valuelblsurcharge" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                            <asp:Label ID="lblwages" runat="server" Text="" Font-Size="13px" valign="right"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="valuelblwages" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                        <td style="width: 5px"></td>
                                                        <td>&nbsp;
                                                            <asp:Label ID="lblPFAmnt" runat="server" Font-Size="13px" valign="right">PF</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="valuelblPFAmnt" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                            <asp:Label ID="lblTollTax" runat="server" Font-Size="13px" valign="right">TollTax</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="valuelblTollTax" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                        <td style="width: 5px"></td>
                                                        <td>&nbsp;
                                                            <asp:Label ID="lblserviceTax" runat="server" Font-Size="13px" valign="right">S.Tax</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="valuelblservceTax" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                            <asp:Label ID="lblservtaxConsigner" runat="server" Font-Size="13px" valign="right">C.Tax:</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="valuelblservtaxConsigner" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                        </td>
                                                        <td style="width: 5px"></td>
                                                        <td>&nbsp;
                                                            <asp:Label ID="lblSwachhBhrtTax" runat="server" Font-Size="13px" valign="right">SwachhBhrt Tax:</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="valuelblSwachhBhrtTax" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                            <asp:Label ID="lblKisanKalyanTax" runat="server" Font-Size="13px" valign="right">Kisan Tax</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="ValueKisanKalyanTax" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                        <td style="width: 5px"></td>
                                                        <td>&nbsp; <strong>
                                                            <asp:Label ID="lblNetAmnt" runat="server" Font-Size="13px" valign="right">Net Amnt</asp:Label></strong>
                                                        </td>
                                                        <td align="right">
                                                            <strong>
                                                                <asp:Label ID="valuelblnetAmnt" runat="server" Font-Size="13px"></asp:Label></strong>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" colspan="4">
                                    <table width="100%" style="font-size: 12px" border="0" cellspacing="0">
                                        <tr style="line-height: 25px">
                                            <td colspan="9" style="font-size: 13px; position: relative;" align="left" class="white_bg">
                                                <table width="100%">
                                                    <tr>
                                                        <td align="left" class="white_bg" style="font-size: 13px; text-align: justify; padding-right: 20px;"
                                                            width="25%">
                                                            <asp:Label ID="lblTerms" Font-Size="10px" runat="server"></asp:Label>
                                                        </td>
                                                        &nbsp;&nbsp;
                                                        <td align="left" class="white_bg" style="font-size: 13px; text-align: justify; margin-left: 5px;"
                                                            width="37%">
                                                            <asp:Label ID="lblterms1" Font-Size="11px" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="right" class="white_bg" style="font-size: 13px" width="10%"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" class="white_bg" style="font-size: 13px" width="30%"></td>
                                                        <td align="right" class="white_bg" style="font-size: 13px" width="10%"></td>
                                                        <td align="right" class="white_bg" style="font-size: 13px" width="30%">
                                                            <b>
                                                                <asp:Label ID="lblCompname" runat="server"></asp:Label></b>
                                                        </td>
                                                    </tr>
                                                    <%--<tr>
                                                        <td align="left" class="white_bg" style="font-size: 13px" width="50%">
                                                            <br />
                                                            <br />
                                                            <br />
                                                            <br />
                                                            <b>Customer Signature</b>&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td align="right" class="white_bg" style="font-size: 13px" valign="top" width="50%">
                                                            <br />
                                                            <b>
                                                                <asp:Label ID="lblCompname" runat="server"></asp:Label><br />
                                                                <br />
                                                                <br />
                                                                Authorised Signatory&nbsp;</b>
                                                        </td>
                                                    </tr>--%>
                                                    <%--<tr>
                                                    <td align="left" class="white_bg" style="font-size: 13px" >
                                                            <b><asp:Label ID="lblTerm" Text="Terms&Condition :" runat="server"></asp:Label></b>
                                                            <asp:Label ID="lblTerms" runat="server"></asp:Label>
                                                    </td>
                                                   
                                                    </tr>--%>
                                                </table>
                                                <p style="line-height: 12px; font-size: 12px;">
                                                    <asp:Label ID="lblGeneratedByName" runat="server"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblLastUpdatedByName" runat="server"></asp:Label>
                                                </p>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </div>
        <div class="col-lg-0" style="display: none;">
            <tr style="display: none">
                <td class="white_bg" align="center">
                    <div id="Jainprint" style="font-size: 13px;">
                        <table cellpadding="1" cellspacing="0" width="800" border="1" style="font-family: Arial,Helvetica,sans-serif;">
                            <tr style="height: 100px;">
                                <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px; border-left-style: none; border-right-style: none">
                                    <div style="text-align: left; width: 140px; float: left;">
                                        <asp:Image ID="ImgLogoJain" Width="140px" Height="90px" runat="server"></asp:Image>
                                    </div>
                                    <div id="header1" runat="server" style="text-align: center; width: 650px; float: center;">
                                        <strong>
                                            <asp:Label ID="lblCompanyname1" runat="server" Style="font-size: 18px;"></asp:Label><br />
                                        </strong>
                                        <asp:Label ID="Label7" runat="server" Text="(Fleet Owners & Transport Contractor)"></asp:Label><br />
                                        <strong>Head Office :
                                            <asp:Label ID="lblCompAdd3" runat="server"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblCompAdd4" runat="server"></asp:Label>
                                            <asp:Label ID="lblCompCity1" runat="server"></asp:Label>&nbsp;&nbsp;
                                            <asp:Label ID="lblCompState1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblCompCityPin1" runat="server"></asp:Label><br />
                                        </strong>
                                        <asp:Label ID="lblTin1" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;<asp:Label
                                            ID="lblCompTIN1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lbltxtPanNo1" runat="server" Text="PAN NO. :"></asp:Label>&nbsp;&nbsp;<asp:Label
                                            ID="lblPanNo1" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%;" valign="top">
                                                <table border="0" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                                    <tr>
                                                        <td align="center" colspan="2" style="border-bottom: 1px solid #484848; font-size: 13px;">
                                                            <strong>DECLARATION</strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="border-bottom: 1px solid #484848; font-size: 11px;">
                                                            <small>I/We declare that we have not taken credit of Excise Duty paid on capital goods
                                                                or credit of tax paid on input services used for providing "Transportation
                                                                of Goods by Road" service under the provision of credit Rules 2004. I/We also declare
                                                                that we have not availed the benefit under Notificatiuon 08/2015-ST Date 01-03-2015.
                                                            </small>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="font-size: 10px">
                                                            <strong>Person Liable to pay Tax Consignor</strong>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 50%;" valign="top">
                                                <table border="0" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                                    <tr>
                                                        <td colspan="1" style="border-bottom: 1px solid #484848; width: 150px; border-right: 1px solid #484848; font-size: 13px;">
                                                            <strong>PARTY ORDER NO</strong>
                                                        </td>
                                                        <td colspan="1" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848; font-size: 13px;">
                                                            <strong>DATE</strong>
                                                        </td>
                                                        <td align="center" colspan="2" style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                                            <asp:Label ID="lblGrDate1" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 20px">
                                                        <td colspan="1" style="width: 150px; border-right: 1px solid #484848; font-size: 13px;">
                                                            <strong></strong>
                                                        </td>
                                                        <td colspan="1" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848; font-size: 13px;">
                                                            <strong>LR NO.</strong>
                                                        </td>
                                                        <td align="center" colspan="2" style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                                            <asp:Label ID="lblGRno1" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 20px">
                                                        <td colspan="1" style="width: 150px; border-right: 1px solid #484848;">
                                                            <strong></strong>
                                                        </td>
                                                        <td colspan="1" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848; font-size: 13px;">
                                                            <strong>FROM</strong>
                                                        </td>
                                                        <td align="center" colspan="2" style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                                            <asp:Label ID="lblFromCity1" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 20px">
                                                        <td colspan="1" style="width: 150px; border-right: 1px solid #484848;">
                                                            <strong></strong>
                                                        </td>
                                                        <td colspan="1" style="width: 100px; border-bottom: 1px solid #484848; border-right: 1px solid #484848; font-size: 13px;">
                                                            <strong>TO</strong>
                                                        </td>
                                                        <td align="center" colspan="2" style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                                            <asp:Label ID="lblToCity1" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 20px">
                                                        <td colspan="1" style="width: 150px; border-right: 1px solid #484848;">
                                                            <strong></strong>
                                                        </td>
                                                        <td colspan="1" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848; font-size: 13px;">
                                                            <strong>VIA</strong>
                                                        </td>
                                                        <td align="center" colspan="2" style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                                            <asp:Label ID="lblJainVia" runat="server"></asp:Label>
                                                        </td>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%;" valign="top">
                                                <table border="0" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                                    <tr>
                                                        <td align="center" colspan="2" style="border-bottom: 1px solid #484848; font-size: 13px;">
                                                            <strong>CONSIGNOR</strong>
                                                        </td>
                                                    </tr>
                                                    <tr id="trConsigneeName1" runat="server">
                                                        <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                                            <b>
                                                                <asp:Label ID="Label52" runat="server">Name</asp:Label></b>
                                                        </td>
                                                        <td style="font-size: 13px; border-right-style: none">
                                                            <asp:Label ID="lblConsigeeName1" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 60px;" valign="top">
                                                        <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                                            <b>
                                                                <asp:Label ID="Label54" runat="server">Address</asp:Label></b>
                                                        </td>
                                                        <td style="font-size: 13px; border-right-style: none">
                                                            <asp:Label ID="lblConsigneeAddress1" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 50%;" valign="top">
                                                <table border="0" width="100%" class="white_bg">
                                                    <tr>
                                                        <td align="center" colspan="2" style="border-bottom: 1px solid #484848; height: 10px; font-size: 13px;">
                                                            <strong>CONSIGNEE</strong>
                                                        </td>
                                                    </tr>
                                                    <tr id="trConsignorName1" runat="server">
                                                        <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                                            <b>
                                                                <asp:Label ID="lbltxtName" runat="server">Name</asp:Label></b>
                                                        </td>
                                                        <td style="font-size: 13px; border-right-style: none">
                                                            <asp:Label ID="lblConsignorName1" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 60px;" valign="top">
                                                        <td style="font-size: 13px; border-right-style: none; width: 20%;">
                                                            <b>
                                                                <asp:Label ID="lblTxtAdd" runat="server">Address</asp:Label></b>
                                                        </td>
                                                        <td style="font-size: 13px; border-right-style: none">
                                                            <asp:Label ID="lblConsignorAddress1" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%;" valign="top">
                                                <table border="0" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                                    <tr>
                                                        <td align="center" style="border-bottom: 1px solid #484848; border-right: 1px solid #484848; font-size: 13px;">
                                                            <strong>Freight Detail</strong>
                                                        </td>
                                                        <td id="divAmntHead" runat="server" align="center" style="border-bottom: 1px solid #484848; font-size: 13px;">
                                                            <strong>Invoice Value</strong>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 32px">
                                                        <td align="center" style="border-bottom: 1px solid #484848; border-right: 1px solid #484848; font-size: 13px;">
                                                            <asp:Label ID="lblGRType" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                        <td id="divAmntvalue" runat="server" align="center" style="border-bottom: 1px solid #484848;">
                                                            <asp:Label ID="lblNetValue" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <strong><small>Acknowledge Consignee with Signature & Stamp</small></strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2"></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 50%;" valign="top">
                                                <table border="0" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                                    <tr>
                                                        <td colspan="1" style="border-bottom: 1px solid #484848; width: 150px; border-right: 1px solid #484848; font-size: 13px;">
                                                            <strong>Sap Delivery No.</strong>
                                                        </td>
                                                        <td colspan="1" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848; font-size: 13px;">
                                                            <strong></strong>
                                                        </td>
                                                        <td colspan="2" style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                                            <strong><b>Truck/Trailor No.</b></strong>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 32px">
                                                        <td colspan="1" style="border-bottom: 1px solid #484848; width: 150px; border-right: 1px solid #484848; font-size: 13px;">
                                                            <strong>Invoice No.</strong>
                                                        </td>
                                                        <td align="center" colspan="1" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848; font-size: 13px;">
                                                            <asp:Label ID="lblInvNoValue" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="center" colspan="2" style="border-bottom: 1px solid #484848; width: 150px; font-size: 13px;">
                                                            <asp:Label ID="lblLorryNo1" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 20px">
                                                        <td colspan="1" style="border-bottom: 1px solid #484848; width: 150px; border-right: 1px solid #484848; font-size: 13px;">
                                                            <strong>Excise Gate Pass No.</strong>
                                                        </td>
                                                        <td colspan="3" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848; font-size: 13px;"></td>
                                                    </tr>
                                                    <tr id="DivJainShipNo" runat="server" style="height: 20px">
                                                        <td colspan="1" style="border-left: 1px solid #484848; border-bottom: 1px solid #484848; width: 150px; border-right: 1px solid #484848; font-size: 13px;">
                                                            <strong>Shipment No.</strong>
                                                        </td>
                                                        <td colspan="3" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848; font-size: 13px;">
                                                            <asp:Label ID="lblJainShipNo" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="DivJainContainerNo" runat="server" style="height: 20px">
                                                        <td colspan="1" style="border-left: 1px solid #484848; border-bottom: 1px solid #484848; width: 150px; border-right: 1px solid #484848; font-size: 13px;">
                                                            <strong>Container No.</strong>
                                                        </td>
                                                        <td colspan="3" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848; font-size: 13px;">
                                                            <asp:Label ID="lblJainContainerNo" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="DivJainSealNo" runat="server" style="height: 20px">
                                                        <td colspan="1" style="border-left: 1px solid #484848; border-bottom: 1px solid #484848; width: 150px; border-right: 1px solid #484848; font-size: 13px;">
                                                            <strong>Seal No.</strong>
                                                        </td>
                                                        <td colspan="3" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848; font-size: 13px;">
                                                            <asp:Label ID="lblJainSealNo" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="DivJainOrderNo" runat="server" style="height: 20px">
                                                        <td colspan="1" style="border-left: 1px solid #484848; border-bottom: 1px solid #484848; width: 150px; border-right: 1px solid #484848; font-size: 13px;">
                                                            <strong>Order No.</strong>
                                                        </td>
                                                        <td colspan="3" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848; font-size: 13px;">
                                                            <asp:Label ID="lblJainOrderNo" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="DivJainFormNo" runat="server" style="height: 20px">
                                                        <td colspan="1" style="border-left: 1px solid #484848; border-bottom: 1px solid #484848; width: 150px; border-right: 1px solid #484848; font-size: 13px;">
                                                            <strong>Form No.</strong>
                                                        </td>
                                                        <td colspan="3" style="border-bottom: 1px solid #484848; width: 100px; border-right: 1px solid #484848; font-size: 13px;">
                                                            <asp:Label ID="lblJainFormNo" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 20px">
                                                        <td align="center" colspan="2" style="border-left: 1px solid #484848; font-size: 13px; width: 150px; border-bottom: 1px solid #484848; border-right: 1px solid #484848;">
                                                            <strong>Product</strong>
                                                        </td>
                                                        <td align="center" colspan="2" style="font-size: 13px; width: 100px; border-right: 1px solid #484848; border-bottom: 1px solid #484848;">
                                                            <strong>Quantity(MT)</strong>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 20px">
                                                        <td align="center" colspan="2" style="border-left: 1px solid #484848; width: 150px; border-right: 1px solid #484848; border-bottom: 1px solid #484848;">
                                                            <asp:Label ID="lblItemName" runat="server" Font-Size="13px"></asp:Label>
                                                        </td>
                                                        <td align="center" colspan="2" style="width: 100px; border-right: 1px solid #484848; border-bottom: 1px solid #484848;">
                                                            <asp:Label ID="lblItemQty" Font-Size="13px" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="bottom" colspan="4" style="height: 60px; border-left: 1px solid #484848;">
                                                            <%--<asp:Label ID="lblCompname1" runat="server" Text="Authorised Signatory"></asp:Label>--%>
                                                            <small>
                                                                <asp:Label ID="lblauth" runat="server" Text="Authorised Signatory"></asp:Label></small>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </div>
        <div id="dvPrtyDet" class="web_dialog black12" style="display: none; width: 680px; height: auto; font-size: 8pt; top: 33%; margin-top: -110px; position: fixed; z-index: 50;">
            <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="1" class="ibdr1">
                <tr>
                    <td class="web_dialog_title" colspan="5">
                        <asp:Label ID="lblDvPrtyName" runat="server" Text="Communication Detail"></asp:Label>
                    </td>
                    <td class="web_dialog_title align_right">
                        <span style="cursor: pointer;" onclick="HideClient('dvPrtyDet')">Close</span>
                    </td>
                </tr>
                <tr>
                    <td align="left" bgcolor="#E8F2FD" valign="middle" width="90px" height="25px">Contact To<span class="redfont1">*</span>
                    </td>
                    <td align="left" bgcolor="#E8F2FD" valign="middle" colspan="2">
                        <asp:TextBox ID="txtconperson" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvtxtconperson" runat="server" ErrorMessage="Please enter Contact Person"
                            Display="Dynamic" CssClass="redfont" ControlToValidate="txtconperson" ValidationGroup="SavePrtyDetl"></asp:RequiredFieldValidator>
                    </td>
                    <td align="left" bgcolor="#E8F2FD" valign="middle" width="80px">Party E-Mail
                    </td>
                    <td align="left" bgcolor="#E8F2FD" valign="middle" colspan="2">
                        <asp:TextBox ID="txtemail" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="1" colspan="6" bgcolor="#C9C9C9"></td>
                </tr>
                <tr>
                    <td align="left" bgcolor="#F5FAFF" valign="middle" width="90px" height="25px">Address[1]<span class="redfont1">*</span>
                    </td>
                    <td align="left" bgcolor="#F5FAFF" valign="middle" colspan="2">
                        <asp:TextBox ID="txtadd1" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvTxtAddrs" runat="server" Display="Dynamic" ControlToValidate="txtadd1"
                            ValidationGroup="SavePrtyDetl" ErrorMessage="Enter Address" SetFocusOnError="true"
                            CssClass="redtext_11px"></asp:RequiredFieldValidator>
                    </td>
                    <td align="left" bgcolor="#F5FAFF" valign="middle" width="80px">Address[2]
                    </td>
                    <td align="left" bgcolor="#F5FAFF" valign="middle" colspan="2">
                        <span class="red" style="color: #ff0000">
                            <asp:TextBox ID="txtadd2" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td height="1" colspan="6" bgcolor="#C9C9C9"></td>
                </tr>
                <tr>
                    <td align="left" bgcolor="#E8F2FD" valign="middle" width="90px" height="25px">State<span class="redfont1">*</span>
                    </td>
                    <td align="left" bgcolor="#E8F2FD" valign="middle" width="130px">
                        <asp:DropDownList ID="ddlstate" runat="server" Height="22px" Width="120px" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVState" runat="server" Display="Dynamic" ControlToValidate="ddlstate"
                            ValidationGroup="SavePrtyDetl" ErrorMessage="Select State" InitialValue="0" SetFocusOnError="true"
                            CssClass=""></asp:RequiredFieldValidator>
                    </td>
                    <td align="left" bgcolor="#E8F2FD" valign="middle" width="80px">City Name
                    </td>
                    <td align="left" bgcolor="#E8F2FD" valign="middle" width="130px">
                        <span class="red" style="color: #ff0000">
                            <asp:DropDownList ID="ddlcity" runat="server" Height="22px" Width="120px">
                            </asp:DropDownList>
                        </span>
                    </td>
                    <td align="left" bgcolor="#E8F2FD" valign="middle" width="80px">City Area
                    </td>
                    <td align="left" bgcolor="#E8F2FD" valign="middle">
                        <asp:DropDownList ID="ddlcityarea" runat="server" Height="22px" Width="120px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td height="1" colspan="6" bgcolor="#C9C9C9"></td>
                </tr>
                <tr>
                    <td align="left" bgcolor="#F5FAFF" valign="middle" height="25px">District
                    </td>
                    <td align="left" bgcolor="#F5FAFF" valign="middle">
                        <asp:DropDownList ID="ddlDistrict" runat="server" Height="22px" Width="120px">
                        </asp:DropDownList>
                    </td>
                    <td align="left" bgcolor="#F5FAFF" valign="middle">Tehsil
                    </td>
                    <td align="left" bgcolor="#F5FAFF" valign="middle">
                        <span class="red" style="color: #ff0000">
                            <asp:DropDownList ID="ddlTehsil" runat="server" Height="22px" Width="120px">
                            </asp:DropDownList>
                        </span>
                    </td>
                    <td align="left" bgcolor="#F5FAFF" valign="middle">Post
                    </td>
                    <td align="left" bgcolor="#F5FAFF" valign="middle">
                        <asp:DropDownList ID="ddlPost" runat="server" Height="22px" Width="120px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td height="1" colspan="6" bgcolor="#C9C9C9"></td>
                </tr>
                <tr>
                    <td align="left" bgcolor="#E8F2FD" valign="middle" height="25px">Pin Code
                    </td>
                    <td align="left" bgcolor="#E8F2FD" valign="middle">
                        <span class="red" style="color: #ff0000">
                            <asp:TextBox ID="txtpinno" runat="server" MaxLength="6" Width="120px" Style="text-align: right;"></asp:TextBox>
                        </span>
                    </td>
                    <td align="left" bgcolor="#E8F2FD" valign="middle">Phone(O)
                    </td>
                    <td align="left" bgcolor="#E8F2FD" valign="middle" width="130px">
                        <span class="red" style="color: #ff0000">
                            <asp:TextBox ID="txtphoneno" runat="server" MaxLength="11" Width="110px" Style="text-align: right;"></asp:TextBox>
                        </span>
                    </td>
                    <td align="left" bgcolor="#E8F2FD" valign="middle">Phone(R)
                    </td>
                    <td align="left" bgcolor="#E8F2FD" valign="middle">
                        <asp:TextBox ID="txtPhoneNoRes" runat="server" MaxLength="11" Width="120px" Style="text-align: right;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="1" colspan="6" bgcolor="#C9C9C9"></td>
                </tr>
                <tr>
                    <td align="left" bgcolor="#F5FAFF" valign="middle" height="25px">Mobile No.<span class="redfont1">*</span>
                    </td>
                    <td align="left" bgcolor="#F5FAFF" valign="middle">
                        <span class="red" style="color: #ff0000">
                            <asp:TextBox ID="txtmobileno" runat="server" MaxLength="10" Width="120px" Style="text-align: right;"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvMobile" runat="server" Display="Dynamic" ControlToValidate="txtmobileno"
                                ValidationGroup="SavePrtyDetl" ErrorMessage="Enter Mobile" SetFocusOnError="true"
                                CssClass="redtext_11px"></asp:RequiredFieldValidator>
                            <br />
                            <asp:RegularExpressionValidator ID="revCntPrsnNo1" runat="server" ControlToValidate="txtmobileno"
                                SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Not a valid Mobile No"
                                CssClass="redfont" Display="Dynamic" ValidationExpression="^[7-9][0-9]{9}$"></asp:RegularExpressionValidator>
                        </span>
                    </td>
                    <td align="left" bgcolor="#F5FAFF" valign="middle">Fax No.
                    </td>
                    <td align="left" bgcolor="#F5FAFF" valign="middle">
                        <span class="red" style="color: #ff0000">
                            <asp:TextBox ID="txtfaxno" runat="server" MaxLength="11" Width="110px" Style="text-align: right;"></asp:TextBox>
                        </span>
                    </td>
                    <td align="left" bgcolor="#F5FAFF" valign="middle">Regn. Place
                    </td>
                    <td align="left" bgcolor="#F5FAFF" valign="middle">
                        <span class="red" style="color: #ff0000">
                            <asp:TextBox ID="txtRegnPlace" runat="server" MaxLength="40" Width="120px" Style="text-align: right;"></asp:TextBox>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td height="1" colspan="6" bgcolor="#C9C9C9"></td>
                </tr>
                <tr>
                    <td align="left" bgcolor="#E8F2FD" valign="middle" height="25px">
                        TIN No.
                    </td>
                    <td align="left" bgcolor="#E8F2FD" valign="middle">
                        <span class="red" style="color: #ff0000">
                            <asp:TextBox ID="txtTinno" runat="server" MaxLength="15" Width="120px" Style="text-align: right;"></asp:TextBox>
                        </span>
                    </td>
                    <td align="left" bgcolor="#E8F2FD" valign="middle">
                        Birthday
                    </td>
                    <td align="left" bgcolor="#E8F2FD" valign="middle">
                        <span class="red" style="color: #ff0000">
                            <asp:TextBox ID="txtDOB" runat="server" MaxLength="12" Width="80px"></asp:TextBox>
                        </span>
                    </td>
                    <td align="left" bgcolor="#E8F2FD" valign="middle">
                        Anniversary
                    </td>
                    <td align="left" bgcolor="#E8F2FD" valign="middle">
                        <span class="red" style="color: #ff0000">
                            <asp:TextBox ID="txtDOA" runat="server" MaxLength="12" Width="80px"></asp:TextBox>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td height="1" colspan="6" bgcolor="#C9C9C9">
                    </td>
                </tr>
                <tr>
                    <td align="left" bgcolor="#ccffff" valign="middle" height="20px" colspan="6">
                        <span class="txt"><span class="red" style="color: #ff0000">&nbsp;&nbsp;</span> </span>
                        <strong>Temporary Address [If any]</strong>
                    </td>
                </tr>
                <tr>
                    <td height="1" colspan="6" bgcolor="#C9C9C9">
                    </td>
                </tr>
                <tr>
                    <td align="left" bgcolor="#F5FAFF" valign="middle" height="25px">
                        Address1
                    </td>
                    <td align="left" bgcolor="#F5FAFF" valign="middle" colspan="5">
                        <asp:TextBox ID="txtTempAddr1" runat="server" MaxLength="100" Width="550px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="1" colspan="6" bgcolor="#C9C9C9">
                    </td>
                </tr>
                <tr>
                    <td align="left" bgcolor="#E8F2FD" valign="middle" height="25px">
                        Address2
                    </td>
                    <td align="left" bgcolor="#E8F2FD" valign="middle" colspan="2">
                        <asp:TextBox ID="txtTempAddr2" runat="server" MaxLength="50" Width="190px"></asp:TextBox>
                    </td>
                    <td align="left" bgcolor="#E8F2FD" valign="middle">
                        Address3
                    </td>
                    <td align="left" bgcolor="#E8F2FD" valign="middle" colspan="2">
                        <asp:TextBox ID="txtTempAddr3" runat="server" MaxLength="30" Width="180px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="1" colspan="6" bgcolor="#C9C9C9">
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="3">
                        &nbsp;
                    </td>
                    <td align="left" colspan="3">
                        <asp:ImageButton ID="btnPrtyDetSave" runat="server" ImageUrl="~/Images/save_img.jpg"
                            ValidationGroup="SavePrtyDetl" OnClientClick="return CheckCustomer();" OnClick="btnPrtyDetSave_Click" />
                        &nbsp;&nbsp;
                        <asp:Button ID="BtnClerForPurOdr" runat="server" Text="Clear & Close" CssClass="button-class"
                            OnClick="BtnClerForPurOdr_Click" />
                        <span id="SpnMessageClient" style="display: none;" class="redtext"></span>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" class="white_bg">
                        <img id="PopupLoaderImageCity" style="display: none;" src="Images/indicator.gif"
                            alt="Please Wait..." title="Please Wait..." />
                    </td>
                </tr>
                <tr>
                    <td height="1" colspan="6" bgcolor="#C9C9C9">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <!-- popup form GR detail -->
        <div id="gr_details_form" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="popform_header">
                            <asp:Label ID="lblGrDetails" runat="server">Receipt Detail</asp:Label>
                        </h4>
                    </div>
                    <div class="modal-body">
                        <section class="panel panel-default full_form_container material_search_pop_form">
										          <div class="panel-body">
										             <!-- First Row start -->
										            <div class="clearfix odd_row">	                                
	                                <div class="col-sm-4">
	                                  <label class="col-sm-5 control-label">Date From</label>
                                    <div class="col-sm-7">
                                       <asp:TextBox ID="txtDateFromDiv" runat="server" CssClass="input-sm datepicker form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvRcptEntryDtFrm" runat="server" ErrorMessage="Enter From Date"
                                        Display="Dynamic" CssClass="classValidation" ControlToValidate="txtDateFromDiv" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                    </div>
	                                </div>
	                                <div class="col-sm-4">
	                                  <label class="col-sm-5 control-label">Date To</label>
                                    <div class="col-sm-7">
                                       <asp:TextBox ID="txtDateToDiv" runat="server" CssClass="input-sm datepicker form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtTo" runat="server" ErrorMessage="Enter To Date"
                                            Display="Dynamic" CssClass="classValidation" ControlToValidate="txtDateToDiv" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                    </div>
	                                </div>
	                                <div class="col-sm-4">
	                                  <label id="lblSender" runat="server" class="col-sm-4 control-label">Receiver<span class="required-field">*</span></label>
	                                  <div class="col-sm-8">
	                                     <asp:DropDownList ID="ddlRecvrDiv" runat="server" CssClass="form-control" AutoPostBack="false">
                                        </asp:DropDownList>
                                        
	                                  </div>
                                      <asp:RequiredFieldValidator ID="rfvddlRecvrDiv" runat="server" ErrorMessage="Select Receiver name"
                                            Display="Dynamic" InitialValue="0" CssClass="classValidation" ControlToValidate="ddlRecvrDiv" ValidationGroup="GRDetailsSrch"></asp:RequiredFieldValidator>                            
	                                </div>
	                              </div> 
	                              <div class="clearfix even_row">
	                                <div class="col-sm-4">
	                                  <label class="col-sm-5 control-label">To City<span class="required-field">*</span></label>
		                                <div class="col-sm-7">
		                                  <asp:DropDownList ID="ddltocityDiv" runat="server" CssClass="form-control" AutoPostBack="false">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddltocityDiv" runat="server" ErrorMessage="Select To City" Display="Dynamic" InitialValue="0" CssClass="classValidation" ControlToValidate="ddltocityDiv"
                                            ValidationGroup="GRDetailsSrch"></asp:RequiredFieldValidator>                           
		                                </div>
	                                </div>
	                                <div class="col-sm-4">
	                                  <label class="col-sm-5 control-label">Deliv.Place<span class="required-field">*</span></label>
		                                <div class="col-sm-7">
		                                  <asp:DropDownList ID="ddldelvplaceDIv" runat="server" CssClass="form-control" 
                                            AutoPostBack="false">
                                        </asp:DropDownList>
		                                </div>
                                        <div class="col-sm-3"></div>
                                        <div class="col-sm-9">
                                        <asp:RequiredFieldValidator ID="rfvddldelvplaceDIv" runat="server" ErrorMessage="Select Delivery Place"
                                            Display="Dynamic" InitialValue="0" CssClass="classValidation" ControlToValidate="ddldelvplaceDIv"
                                            ValidationGroup="GRDetailsSrch"></asp:RequiredFieldValidator>                           
                                            </div>
	                                </div>
                                
	                                <div class="col-sm-4" style="padding: 0;">
	                                  <div class="col-sm-6 prev_fetch">
	                                     <asp:LinkButton ID="lnkbtnSearch" class="btn full_width_btn btn-sm btn-primary" CausesValidation="true"  ValidationGroup="GRDetailsSrch" OnClick="lnkbtnSearch_Click"
                                                             runat="server">Search</asp:LinkButton>    
	                                  </div>
	                                  <div class="col-sm-6"> 
	                                     
	                                  </div>
	                                </div>
	                              </div> 
	                                <asp:GridView ID="grdGrdetals" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="display nowrap dataTable"
                                    Width="100%" BorderStyle="None" BorderWidth="0"  OnRowDataBound="grdGrdetals_RowDataBound" OnRowEditing="grdGrdetals_RowEditing">
                                     <RowStyle CssClass="odd" />
                                    <AlternatingRowStyle CssClass="even" />   
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" HeaderStyle-Width="40px">
                                            <HeaderStyle Width="40" CssClass="gridHeaderAlignCenter" />
                                            <ItemStyle Width="40" CssClass="gridHeaderAlignCenter" />
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAllCheckboxes(this);"
                                                    CssClass="SACatA" OnCheckedChanged="chkSelectAllRows_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkId" runat="server" OnCheckedChanged="chkId_CheckedChanged" CssClass="gridHeaderAlignCenter" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rcpt No." HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("Rcpt_No"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Ref.No." HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("Ref_No"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToDateTime(Eval("Rcpt_Date")).ToString("dd-MMM-yyyy")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To City" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%#Eval("ToCity")%>
                                                <asp:HiddenField ID="hidGrIdno" runat="server" Value='<%#Eval("RcptGoodHead_Idno")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sender No." HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="180px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Eval("Sender_No")%>
                                                <asp:Label ID="lblSenderNo" runat="server" Value='<%#Eval("Sender_No")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Agent Name" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="180px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Eval("Agnt_Name")%>
                                                <asp:Label ID="lblAgentname" runat="server" Value='<%#Eval("Agnt_Name")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="20px" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                &nbsp;
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        Records(s) not found.
                                    </EmptyDataTemplate>
                                </asp:GridView>
						    </div>
                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                            <asp:Label ID="lblmsg2" runat="server" Text="Message - Please select only one GR at a time."
                                Visible="false"></asp:Label>
                                <asp:Label ID="lblmsg" runat="server" Text="Message - Please select only one GR at a time."
                                    Visible="false" CssClass="redfont"></asp:Label>
					    </section>
                    </div>
                    <div class="modal-footer">
                        <div class="popup_footer_btn">
                            <asp:LinkButton ID="lnkbtndivsubmit" OnClick="lnkbtndivsubmit_Click" class="btn btn-dark"
                                runat="server">Submit</asp:LinkButton>
                            <button type="submit" class="btn btn-dark" data-dismiss="modal">
                                <i class="fa fa-times"></i>Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- popup for Amount  -->
        <div id="Amount" class="modal fade">
            <div class="modal-dialog" style="width: 25%">
                <div class="modal-header">
                    <h4 class="popform_header">Print&nbsp;&nbsp;&nbsp;&nbsp;</h4>
                </div>
                <div class="modal-content">
                    <div class="modal-body">
                        <section class="panel panel-default full_form_container material_search_pop_form">
								        <div class="panel-body">  
                                        <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlPage" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="4 Pages" Value="4" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="3 Pages" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="2 Pages" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="1 Pages" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                       </div>
                                       <div class="col-sm-9">
                                        <asp:LinkButton ID="lnkwithamount" Text="With Amount" 
                                                class="btn btn-sm btn-primary" runat="server" TabIndex="45"  
                                                OnClick="lnkWithamount_click" ></asp:LinkButton>
                                       
                                        <asp:LinkButton ID="lnkwithoutamount" Text="Without Amount" 
                                                class="btn btn-sm btn-primary" runat="server"  TabIndex="45" 
                                               OnClick="lnkwithoutamount_Click" ></asp:LinkButton>
                                    
                                        </div>
                                        
                                       </div>
                                        </section>
                    </div>
                </div>
            </div>
        </div>
        <!-- popup form Acc Posting -->
        <div id="acc_posting" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="popform_header">Acc Posting</h4>
                    </div>
                    <div class="modal-body">
                        <section class="panel panel-default full_form_container material_search_pop_form">
								    <div class="panel-body">
									    <!-- First Row start -->
								    <div class="clearfix odd_row">	                                
	                        <div class="col-sm-4">
	                            <label class="col-sm-3 control-label">From</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtIdFrom" runat="server" CssClass="form-control" Width="100px" c oncopy="return false"
                                        onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDivFrom" runat="server" ControlToValidate="txtIdFrom"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="From Required." 
                                    SetFocusOnError="true" ValidationGroup="Acc"></asp:RequiredFieldValidator> 
                            </div>
	                        </div>
	                        <div class="col-sm-4">
	                            <label class="col-sm-2 control-label">To</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtIdTo" runat="server" CssClass="form-control" Width="100px" oncopy="return false"
                                        onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDivTo" runat="server" ControlToValidate="txtIdTo"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="To Required." 
                                    SetFocusOnError="true" ValidationGroup="Acc"></asp:RequiredFieldValidator> 
                            </div>
	                        </div>
	                        <div class="col-sm-4" style="padding: 0;">
	                            <div class="col-sm-1 prev_fetch">
	                            </div>
	                            <div class="col-sm-12"> 
	                            <asp:Label ID="lblPostingLeft" runat="server"></asp:Label>
	                            </div>
	                        </div>
	                        </div> 	                              
	                              
							    </div>
						    </section>
                    </div>
                    <div class="modal-footer">
                        <div class="popup_footer_btn">
                            <asp:LinkButton ID="lnkbtnAccPosting" ValidationGroup="Acc" OnClick="lnkbtnAccPosting_Click"
                                class="btn btn-dark" runat="server">Acc Posting</asp:LinkButton>
                            <button type="submit" class="btn btn-dark" data-dismiss="modal">
                                <i class="fa fa-times"></i>Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--USERPREF SETTING --%>
        <div class="pop-up-parent">
            <div id="PopUpSetting" class="pop-up" style="width: 600px; height: auto; display: none;">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="popform_header">
                                <i class="img-setting"></i><span>Goods Receipt Setting</span> <i class="fa fa-times"></i>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <section class="panel full_form_container">
								<div class="panel-body">
                                    <div class="col-sm-4">
                                        <asp:CheckBox ID="chkSendSMSOnGRSave" runat="server" Text="Send SMS (GR Save)"></asp:CheckBox>
                                    </div>
                                </div>
                            </section>
                        </div>
                        <hr />
                        <div class="clearfix fifth_right panel">
                            <section class="panel panel-in-default btns_without_border">
                                <asp:LinkButton ID="lnkBtnSaveUserPref" runat="server" CssClass="pop-up-button btn btn-s-md btn-success center-block" TabIndex="17" OnClick="lnkBtnSaveUserPref_OnClick" CausesValidation="true" ><i class="fa fa-save"></i> Save</asp:LinkButton>
                            </section>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfSenderNoId" runat="server" />
        <asp:HiddenField ID="hidLastgrId" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hidInstantPrintPara" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hidPages" runat="server" />
        <asp:HiddenField ID="hidGrtyp" runat="server" />
        <asp:HiddenField ID="hidFlagMoreDetail" runat="server" ClientIDMode="Static" />
    </div>
    <%--CUSTOM SCRIPT More detail slidedoen box--%>
    <script>
        $(document).ready(function () {
            if ($('#hidFlagMoreDetail').val() == "1") {
                $('.collapse-header + div').show();
                $('.collapse-header > h3').empty("i");
                $('.collapse-header > h3').append("More details (Click here) <i class=\"fa fa-minus\" aria-hidden=\"true\"></i>");
            } else {
                $('.collapse-header + div').hide();
                $('.collapse-header > h3').empty("i");
                $('.collapse-header > h3').append("More details (Click here) <i class=\"fa fa-plus\" aria-hidden=\"true\"></i>");
            }
        });
        $('.collapse-header').click(function () {
            $('.collapse-header + div').slideToggle(300);
            if ($('.collapse-header > h3').html() == "More details (Click here) <i class=\"fa fa-plus\" aria-hidden=\"true\"></i>") {
                $('.collapse-header > h3').empty("i");
                $('.collapse-header > h3').append("More details (Click here) <i class=\"fa fa-minus\" aria-hidden=\"true\"></i>");
                $('#hidFlagMoreDetail').val("1");
            }
            else {
                $('.collapse-header > h3').empty("i");
                $('.collapse-header > h3').append("More details (Click here) <i class=\"fa fa-plus\" aria-hidden=\"true\"></i>");
                $('#hidFlagMoreDetail').val("0");
            }
        });

    </script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = "";
            var Pages = "1";
            Pages = document.getElementById("<%=hidPages.ClientID%>").value;
            var prtContent3 = "<p style='page-break-before: always'></p>";
            for (i = 0; i < Pages; i++) {
                prtContent = prtContent + "<table width='100%' border='0'></table>";
                if (Pages != 1) {
                    prtContent = prtContent + "<tr><td><strong style='font-size: 15px;'>" + ((i == 1) ? "[Office Copy]" : (i == 2) ? "[Consignor Copy]" : (i == 3) ? "[Consignee Copy]" : "[Driver Copy]") + "</strong></td></tr>";
                }
                var prtContent1 = document.getElementById(strid);
                var prtContent2 = prtContent1.innerHTML;
                prtContent = prtContent + prtContent2 + ((i < 3) ? prtContent3 : "");
            }
            var WinPrint = window.open('', '', 'left=0,top=0,width=800,height=800,toolbar=1,scrollbars=1,status=1');
            WinPrint.document.write(prtContent);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            //WinPrint.close();
            return false;
        }
       
    </script>
    <script language="javascript" type="text/javascript">
        function Divopen() {
            $('#Amount').modal('show');
        }
    </script>
    <script language="javascript" type="text/javascript">
        window.onload = function () {
            OnChangeGRType();
            var y;
            if (document.getElementById("<%=RDbDirect.ClientID%>").checked == true) {
                y = "1";
            }
            else if (document.getElementById("<%=RDbRecpt.ClientID%>").checked == true) {
                y = "2";
            }
            else {
                y = "3";
            }
            OnchangeGrAgnst(y);
        };



        function OnchangeGrAgnst(strid) {
            var x = strid;
            if (x == "1") {
                document.getElementById("<%=DivExcelUpload.ClientID %>").style.display = "block";
                document.getElementById("<%=lnkbtnGrAgain.ClientID%>").style.visibility = "hidden";
                document.getElementById("<%=ddlGRType.ClientID %>").disabled = false;
                document.getElementById("<%=DivItemPanel.ClientID %>").style.display = "block";
                //document.getElementById("<%=ddlSender.ClientID %>").disabled = false;
                document.getElementById("<%=ddlReceiver.ClientID %>").disabled = false;
                document.getElementById("<%=ddlFromCity.ClientID %>").disabled = false;
                document.getElementById("<%=ddlToCity.ClientID %>").disabled = false;
                document.getElementById("<%=ddlLocation.ClientID %>").disabled = false;
                document.getElementById("<%=ddlDateRange.ClientID %>").disabled = false;
            }
            else {
                if (x == "2") {
                    document.getElementById("<%=lblSender.ClientID %>").innerHTML = "Receiver";
                    document.getElementById("<%=lblGrDetails.ClientID %>").innerHTML = "Receipt Detail";
                    document.getElementById("<%=ddlGRType.ClientID %>").disabled = false;
                    document.getElementById("<%=DivItemPanel.ClientID %>").style.display = "block";

                    //document.getElementById("<%=ddlSender.ClientID %>").disabled = true;
                    document.getElementById("<%=ddlReceiver.ClientID %>").disabled = true;
                    document.getElementById("<%=ddlFromCity.ClientID %>").disabled = true;
                    document.getElementById("<%=ddlToCity.ClientID %>").disabled = true;
                    document.getElementById("<%=ddlLocation.ClientID %>").disabled = true;
                    document.getElementById("<%=ddlDateRange.ClientID %>").disabled = true;
                }
                else {
                    document.getElementById("<%=lblSender.ClientID %>").innerHTML = "Party";
                    document.getElementById("<%=lblGrDetails.ClientID %>").innerHTML = "Advance Order";
                    document.getElementById("<%=ddlGRType.ClientID %>").selectedIndex = "1";
                    document.getElementById("<%=ddlGRType.ClientID %>").disabled = true;
                    document.getElementById("<%=DivItemPanel.ClientID %>").style.display = "block";
                    document.getElementById("<%=txtItmCommission.ClientID %>").disabled = true;
                    document.getElementById("<%=txtrate.ClientID %>").disabled = true;
                }
                document.getElementById("<%=DivExcelUpload.ClientID %>").style.display = "none";
                document.getElementById("<%=lnkbtnGrAgain.ClientID%>").style.visibility = "visible";
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
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

        //$(document).ready(function () {
        //    setDatecontrol();
        //});
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
            $("#<%=txtEGPDate.ClientID %>").datepicker({
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
            $("#<%=txtDateFromDiv.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate

            });
            $("#<%=txtDateToDiv.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate

            });
        }

        function ShowModalPopup() {
            ShowDialog(true);
        }
        function ShowDialog(modal) {
            // $("#overlay").show();
            $("#dialog").show();
            $("#dialog").fadeIn(300);

            if (modal) {
                $("#dialog").unbind("click");
                //$("#overlay").unbind("click");
            }
            else {
                //  $("#overlay").click(function (e) {
                $("#dialog").click(function (e) {
                    HideDialog();
                });
            }
        }


        function HideDialog() {
            //   $("#overlay").hide();
            $("#dialog").fadeOut(300);
        }

        function Focus() {
            $("#txtGRNo").focus();
        }

      
    </script>
    <script language="javascript" type="text/javascript">
        function HideClient(dvNm) {
            $("#" + dvNm).fadeOut(300);
        }

        function ShowClient(dvNm) {
            $("#" + dvNm).fadeIn(300);
        }

        function ReloadPage() {
            setTimeout('window.location.href = window.location.href', 2000);
        }

        function HideBillAgainst() {
            $("#dvGrdetails").fadeOut(300);
        }

        function openModal() {
            $('#dvContainerdetails').modal('show');
        }
        function openModalKM() {
            $('#dvKM').modal('show');
        }
        function openModalForChlnGen() {
            $('#dvChlnGenrateDetl').modal('show');
        }

        function openModalGrdDtls() {
            $('#gr_details_form').modal('show');
        }

        function ShowClient() {
            $("#dvGrdetails").fadeIn(300);
        }

        function closepopup() {
            $('#Upload_div').modal('hide');
        }


        function ShowConfirm() {
            var ans = confirm('Entered GR No.is not in sequence. Do you want to continue?');

            if (ans == false) {
                var btn = document.getElementById('<%=lnkbtn.ClientID%>');
                btn.click();
            }
        }

        function ShowConfirmAtSave() {
            var ans = confirm('Entry already made with this GR. No. Do you want to regenerate it?');

            if (ans == true) {
                var btnsav1 = document.getElementById('<%=lnkbtnAtSave1.ClientID%>');
                btnsav1.click();
            }
            else if (ans == false) {
                var btnsav = document.getElementById('<%=lnkbtnAtSave.ClientID%>');
                btnsav.click();
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function OnChangeddlRateType() {
            if (document.getElementById("<%=ddlRateType.ClientID%>").value == "0") {
            }
            else {
                __doPostBack('RateTypeValue', '');
            }

        }

        function openGridDetail() {
            $('#gr_details_form').modal('show');
        }
    
    </script>
    <script language="javascript" type="text/javascript">

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

        function OnChangeGRType() {
            if (document.getElementById("<%=ddlGRType.ClientID%>").value == "1") {
            }

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

        function cityviaddl() {
            var id = document.getElementById("<%=ddlToCity.ClientID %>").value;
            document.getElementById("<%=ddlLocation.ClientID %>").value = id;
            document.getElementById("<%=ddlCityVia.ClientID %>").value = id;
        }

        function consignor(ddlSender) {
            var id = ddlSender.options[ddlSender.selectedIndex].innerHTML;
            document.getElementById("<%=txtconsnr.ClientID %>").value = id;
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
    <script type="text/javascript">
        function RedirectToChallan(strGrIdno) {
            window.open('ChlnBooking.aspx?GrHeadIdno=' + strGrIdno, '_blank');
        }
        function RedirectToCapitalLogistic(str) {
            window.open(str);
        }
    </script>
    <script type="text/javascript">

        function CalcKM() {

            var FromKM = 0; var ToKM = 0; var Tot;
            if (document.getElementById("<%=txtFromKm.ClientID %>").value != "") { FromKM = document.getElementById("<%=txtFromKm.ClientID %>").value; }
            if (document.getElementById("<%=txtToKM.ClientID %>").value != "") { ToKM = document.getElementById("<%=txtToKM.ClientID %>").value; }

            Tot = (parseFloat(ToKM) - parseFloat(FromKM));
            if (Tot > 0) {
                $('#txtTotKM').val((parseFloat(ToKM) - parseFloat(FromKM)));
            }
            else {
                PassMessageError('From K.M. cannot be greater than To K.M.');
                $('#txtToKM').val('0');
                $('#txtTotKM').val('0');
            }
        }
    </script>
    <script type="text/javascript">
        function ClientItemSelected(sender, e) {
            $get("<%=hfSenderNoId.ClientID %>").value = e.get_value();
        }         
    </script>
</asp:Content>
