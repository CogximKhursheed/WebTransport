<%@ Page Title="Advance Booking [For GR]" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="AdvBookGR.aspx.cs" Inherits="WebTransport.AdvBookGR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-10">
            <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">Advance Booking[For GR]
                <asp:Label ID="Lblchllnno" runat="server" Text=""></asp:Label>
                  <span class="view_print"> <asp:LinkButton ID="lnkBtnLast"  class="view_print"  runat="server"  AlternateText="Print" title="Print" Height="16px" onclick="lnkBtnLast_Click">LAST PRINT</i></asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="ManageAdvBookGr.aspx"><asp:Label ID="lblViewList" runat="server" TabIndex="30" Text="LIST"></asp:Label></a>&nbsp;                   
                    <asp:LinkButton ID="lnkbtnPrint" CssClass="fa fa-print icon" Visible="false" TabIndex="31" runat="server" AlternateText="Print" title="Print" Height="16px" OnClick="lnkbtnPrint_Click"></asp:LinkButton>
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
                              <label class="col-sm-3 control-label" style="width: 30%;">Date Range<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 70%;">
                                <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control" AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged">
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                    ControlToValidate="ddlDateRange" ValidationGroup="save" ErrorMessage="Please select date range"
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 32%;">Loc.[From]<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 68%;">
                                <asp:DropDownList ID="drpBaseCity" runat="server" CssClass="form-control" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="drpBaseCity_OnSelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvtxtfromcity" runat="server" Display="Dynamic"
                                    ControlToValidate="drpBaseCity" ValidationGroup="save" ErrorMessage="Please select location"
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                             
                            </div>
                           
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 26%;">Date<span class="required-field">*</span></label>
                              <div class="col-sm-4" style="width: 38%;">
                                <asp:TextBox ID="txtOrderDate" runat="server" CssClass="input-sm datepicker form-control"   TabIndex="3" onchange="Focus()" onkeydown = "return DateFormat(this, event.keyCode)"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic"
                                    ControlToValidate="txtOrderDate" ValidationGroup="save" ErrorMessage="Please select date" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>                             
                              <div class="col-sm-2" style="width: 36%;">
                              	<asp:TextBox ID="txtOrderNo" runat="server" CssClass="form-control" Style="text-align: right;" Enabled="false"  TabIndex="4" MaxLength="9"  ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                                    ControlToValidate="txtOrderNo" ValidationGroup="save" ErrorMessage="Please enter order no" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                           	</div>
                           
                          </div>
                          <div class="clearfix even_row">
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 30%;">Stuff. Date</label>
                              <div class="col-sm-4" style="width: 35%;">
                                <asp:TextBox ID="txtRecDate" runat="server" CssClass="input-sm datepicker form-control" MaxLength="15"  TabIndex="5" onchange="Focus()"></asp:TextBox>
                              </div> 
                               <div class="col-sm-4" style="width: 35%;">
                                <asp:DropDownList ID="ddlGRType" runat="server" CssClass="form-control" AutoPostBack="True" Enabled="false"
                                    OnSelectedIndexChanged="ddlGRType_SelectedIndexChanged" TabIndex="6" >
                                    <asp:ListItem Text="Paid GR" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="TBB GR" Value="2" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="To Pay GR" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                               </div>
                           	</div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 32%;">Ref No</label>
                              <div class="col-sm-8" style="width: 68%;">
                                 <asp:TextBox ID="txtReffrnceNumber" runat="server" Placeholder="Reference Order/Number" CssClass="form-control" MaxLength="15" TabIndex="7" ></asp:TextBox>                                
                              </div>                            
                           	</div>
                           	<div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 26%;">Truck No </label>
							    <div class="col-sm-8" >
						         <asp:DropDownList ID="ddlTruckNo" runat="server" CssClass="chzn-select" style="width: 74%;" TabIndex="8">
                                    </asp:DropDownList>
							    </div>							   
                            </div>
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 30%;">Party<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width:70%;">
                                <asp:DropDownList ID="ddlParty" runat="server"  CssClass="chzn-select" style="width:100%;"  TabIndex="9" onchange="javascript:consignor(this);">
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="ddlParty"
                                    ValidationGroup="save" ErrorMessage="Please Select party" InitialValue="0"
                                    SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>                             
                            </div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width:33%;">Consr. Name</label>
                            <div class="col-sm-5" style="width: 67%;">
                              <asp:TextBox ID="txtconsnr" runat="server" CssClass="form-control" Placeholder="Enter Consignor Name" TabIndex="10"
                                    ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvconsn" runat="server" ControlToValidate="txtconsnr"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Enter Consignor Name" 
                                    SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator> 
                            </div>                          
                           	</div>
                           	<div class="col-sm-4">
                              	<label class="col-sm-3 control-label" style="width:27%;">Agent</label>
                            <div class="col-sm-5" style="width: 73%;">
                             <asp:DropDownList ID="ddlAgent" runat="server" CssClass="form-control" TabIndex="11">
                                </asp:DropDownList>
                            </div>						
                          </div>
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-4">
                           	  <label class="col-sm-3 control-label" style="width: 30%;">To City<span class="required-field">*</span></label>
                           		<div class="col-sm-8" style="width: 70%;">
                                <asp:DropDownList ID="ddlToCity" runat="server" CssClass="chzn-select" style="width:100%;" TabIndex="12" onchange="javascript:cityviaddl();">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlToCity" runat="server" Display="Dynamic" ControlToValidate="ddlToCity"
                                    ValidationGroup="save" ErrorMessage="Please Select To City" InitialValue="0"
                                    SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div> 	                          
                           	</div>
                          	<div class="col-sm-4">
                                <label class="col-sm-3 control-label" style="width: 32%;">Dlv. Place<span class="required-field">*</span></label>
							<div class="col-sm-8" style="width: 68%;">
							<asp:DropDownList ID="ddlLocation" runat="server"  TabIndex="13" >
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvLocation" runat="server" Display="Dynamic" ControlToValidate="ddlLocation"
                                ValidationGroup="save" ErrorMessage="Please Select Delivery Place" InitialValue="0"
                                SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
							</div>
                            </div>
                        <div class="col-sm-4">
                            <label class="col-sm-3 control-label" style="width: 27%;">City[Via]<span class="required-field">*</span></label>
                           		<div class="col-sm-8" style="width: 73%;">
                                <asp:DropDownList ID="ddlviacity" runat="server" CssClass="form-control" TabIndex="14"  >
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCityVia" runat="server" Display="Dynamic" ControlToValidate="ddlviacity"
                                    ValidationGroup="save" ErrorMessage="Please Select City Via" InitialValue="0"
                                    SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div> 
                          </div>
                          </div>
                          <div class="clearfix odd_row">
                             <div class="col-sm-4">
                             <label class="col-sm-3 control-label" style="width: 32%;">Shipment No.</label>
								<div class="col-sm-1" style="width: 58%;">
								<asp:TextBox ID="txtshipment" style="text-align:right" runat="server"  Placeholder="Shipment Number" CssClass="form-control"  AutoComplete="off"  MaxLength="20" TabIndex="15"></asp:TextBox>
								</div>
                                 <div class="col-sm-1" style="width: 10%;">
                                 <asp:LinkButton ID="lnkbtnContnrDtl" runat="server" CssClass="btn btn-sm btn-primary acc_home" TabIndex="16" OnClick="lnkbtnContnrDtl_OnClick"><i class="fa fa-file"></i></asp:LinkButton>
                                 </div>
                          </div>
                          <div class="col-sm-8">
                                <label class="col-sm-3 control-label" style="width:10%;">Remarks</label>
                                <div class="col-sm-9" style="width: 90%;">
                                <asp:TextBox ID="TxtRemark" runat="server" CssClass="form-control" Autocomplete="Off" Placeholder="Remarks" MaxLength="200" TabIndex="17" ></asp:TextBox>     
                                </div>
                              </div>
                          </div>
                        </div>
                      </section>                        
                    </div>
                     <!-- fourth row -->
               
                    <div class="clearfix second_section">
                      <section class="panel panel-in-default">  
                        <div class="panel-body">
                          <div class="clearfix even_row">
                            <div class="col-sm-2">
                              <label class="control-label">Item Name<span class="required-field">*</span></label>
                              <div>
                                <asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control"  TabIndex="18" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPartno" runat="server" ControlToValidate="ddlItemName" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="ValueSubmit"
                                    ErrorMessage="Select Item Name" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                           	<div class="col-sm-2">
                              <label class="control-label">Unit<span class="required-field">*</span></label>
                              <div>
                                <asp:DropDownList ID="ddlunitname" runat="server" CssClass="form-control" AutoPostBack="true" TabIndex="19" OnSelectedIndexChanged="ddlunitname_SelectedIndexChanged">
                                </asp:DropDownList>                            
                                <asp:RequiredFieldValidator ID="rfvAmnt" runat="server" ControlToValidate="ddlunitname" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="ValueSubmit"
                                    ErrorMessage="Choose Unit Name" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>

                            <div class="col-sm-2">
                              <label class="control-label">Rate Type<span class="required-field">*</span></label>
                              <div>
                                <asp:DropDownList ID="ddlRateType" runat="server" CssClass="form-control"  AutoPostBack="true" TabIndex="20" OnSelectedIndexChanged="ddlRateType_SelectedIndexChanged">
                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Rate" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Weight" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlRateType" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="ValueSubmit"
                                    ErrorMessage="Please select rate type" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-2">
                              <label class="control-label">Quantity<span class="required-field">*</span></label>
                              <div>
                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"  MaxLength="6"
                                Style="text-align: right;" TabIndex="21" onKeyPress="return checkfloat(event, this);" oncopy="return false" onpaste="return false"
                                oncut="return false" oncontextmenu="return false">1</asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" SetFocusOnError="true"   ValidationGroup="ValueSubmit"
                                    ErrorMessage="Please enter quantity" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-2">
                              <label class="control-label">Weight</label>
                              <div>
                                <asp:TextBox ID="txtweight" style="text-align:right" PlaceHolder="0.00" Text="0.00" runat="server" CssClass="form-control" MaxLength="30" TabIndex="22" OnTextChanged="txtweight_OnTextChanged" AutoPostBack="true"
                                 onKeyPress="return checkfloat(event, this);" oncopy="return false"
                                onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvWeight" runat="server" ControlToValidate="txtweight" Display="Dynamic" SetFocusOnError="true"   ValidationGroup="ValueSubmit"
                                    ErrorMessage="Please Enter Weight!" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                           	<div class="col-sm-2">
                              <label class="control-label">Rate<span class="required-field">*</span></label>
                              <div>
                                <asp:TextBox ID="txtrate" PlaceHolder="0.00" runat="server" CssClass="form-control" MaxLength="30" TabIndex="22"
                                    onKeyPress="return checkfloat(event, this);" oncopy="return false" Style="text-align:right;"
                                    onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtrate" runat="server" ControlToValidate="txtrate" Display="Dynamic" SetFocusOnError="true" ValidationGroup="ValueSubmit"
                                    ErrorMessage="Enter Rate" CssClass="classValidation"></asp:RequiredFieldValidator>
                               </div>
                            </div>
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-9">	                                
                             &nbsp;
                            </div>
                            <div class="col-sm-3">
                              <div class="col-sm-6">
                                <asp:HiddenField ID="hidrowid" runat="server" />
                                <asp:HiddenField ID="hidTBBType" runat="server" />
                                 <asp:HiddenField ID="hidratetype" runat="server" />
                                <asp:LinkButton ID="lnkbtnSubmit" runat="server" OnClick="lnkbtnSubmit_OnClick" CssClass="btn full_width_btn btn-sm btn-primary " TabIndex="22"  ToolTip="Click to Submit" CausesValidation="true" ValidationGroup="ValueSubmit" >Submit</asp:LinkButton>
                              </div>
                              <div class="col-sm-6">
                                <asp:LinkButton ID="lnkbtnAdd" runat="server" OnClick="lnkbtnAdd_OnClick" CssClass="btn full_width_btn btn-sm btn-primary " TabIndex="23" ToolTip="Click to new" >New</asp:LinkButton>
                              </div>
                            </div>
                          </div>                            
                        </div>
                      </section>                        
                    </div>

                     <div class="clearfix third_right">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body" style="overflow-x:auto;">     
                           <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"
                                    Width="100%" GridLines="None" EnableViewState="true" AllowPaging="true" BorderWidth="0" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand"
                                    OnRowDataBound="grdMain_RowDataBound" 
                                    ShowFooter="true"    PageSize="30">
                                    <RowStyle CssClass="odd" />
                                    <AlternatingRowStyle CssClass="even" />    
                                <Columns>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="50" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("id") %>' CommandName="cmdedit" TabIndex="5" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("id") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" TabIndex="6" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="50" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Item Name">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                            <%#Eval("Item_Name")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Unit Name">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                            <%#Eval("Unit_Name")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Rate Type">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                            <%#Eval("Rate_Type")%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField   HeaderStyle-Width="100" HeaderText="Quantity">
                                        <ItemStyle HorizontalAlign="Center" Width="100" />
                                        <ItemTemplate>
                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Quantity")))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblQuantity" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="100" HeaderText="Weight">
                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                        <ItemTemplate>
                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Weight")=="" ? 0:Convert.ToDouble(Eval("Weight"))))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblWeight" runat="server"></asp:Label>
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
                                </Columns>
                                </asp:GridView>
                        </div>
                      </section>
                    </div> 

                      <div class="clearfix">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body"> 
                        <div class="clearfix odd_row">
                            <div class="col-sm-6">
                               <div class="col-sm-7">
                             &nbsp;
                            </div>
                            </div>
                            <div class="col-sm-3">
                               &nbsp;
                            </div>
                           	<div class="col-sm-3">
                              <label class="col-sm-6 control-label">GrossAmnt</label>
                              <div class="col-sm-6">
                              <asp:TextBox ID="txtGrossAmnt" runat="server" CssClass="form-control"  Text="0.00"
                                    TabIndex="24" Style="text-align: right;" Enabled="False"  
                                    onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                              </div>
                            </div>			                              
                          </div> 
                        <div class="clearfix even_row">
                            <div class="col-sm-6">
                               <div class="col-sm-7">
                             &nbsp;
                            </div>
                            </div>
                            <div class="col-sm-3">
                             &nbsp;
                            </div>
                           	<div class="col-sm-3">
                              <label class="col-sm-6 control-label">RoundOff</label>
                              <div class="col-sm-6">
                               <asp:TextBox ID="TxtRoundOff" runat="server" CssClass="form-control" MaxLength="7"
                                    TabIndex="25" Enabled="false" Text="0.00" Style="text-align: right;" OnChange="ComputeCosts();"
                                    onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"
                                    onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                              </div>
                            </div>			                              
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-6">
                               <div class="col-sm-7">
                            &nbsp;
                            </div>
                            </div>
                            <div class="col-sm-3">
                             &nbsp;
                            </div>
                           	<div class="col-sm-3">
                              <label class="col-sm-6 control-label">Net Amount</label>
                              <div class="col-sm-6">
                                <asp:TextBox ID="txtNetAmnt" runat="server" CssClass="form-control" MaxLength="7"
                                        TabIndex="26" Enabled="false" Text="0.00" Style="text-align: right;" OnChange="ComputeCosts();"
                                        onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"
                                        onKeyPress="return checkfloat(event, this);"  ></asp:TextBox>
                              </div>
                            </div>			                              
                          </div>
                        </div>
                      </section>
                    </div>

                          <div class="clearfix fourth_right">
                      <section class="panel panel-in-default btns_without_border">                            
                        <div class="panel-body">     
                          <div class="clearfix odd_row">
                            <div class="col-lg-2">
                            <asp:Label ID="lblmessage" runat="server" Font-Bold="true" Visible="false" CssClass="classValidation"
                                        Text=""></asp:Label>
                            </div>
                            <div class="col-lg-8">  
                             <div class="col-sm-4">
                              <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false" 
                                                                    CssClass="btn full_width_btn btn-s-md btn-info" OnClick="lnkbtnNew_OnClick" 
                                                                    TabIndex="29" ><i class="fa fa-file-o"></i>New</asp:LinkButton>         
                              </div>                                       
                              <div class="col-sm-4" id="DivSaveButton" runat="server">
                              <asp:LinkButton ID="lnkbtnSave" runat="server" CssClass="btn full_width_btn btn-s-md btn-success" TabIndex="27" OnClick="lnkbtnSave_OnClick" CausesValidation="true" ValidationGroup="save" > <i class="fa fa-save"></i>Save</asp:LinkButton>                               
                                
                              </div>
                              <div class="col-sm-4">
                               <asp:LinkButton ID="lnkbtnCancel" runat="server" CssClass="btn full_width_btn btn-s-md btn-danger" TabIndex="28" OnClick="lnkbtnCancel_OnClick" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
                                 <asp:HiddenField ID="hidOrderIdno" runat="server" />
                                <asp:HiddenField ID="hidmindate" runat="server" />
                                <asp:HiddenField ID="hidmaxdate" runat="server" />
                         
                              </div>                             
                            </div>
                            <div class="col-lg-2"></div>
                          </div> 
                        </div>
                      </section>
                    </div>
                    <!-- For Container Details Popup -->

                    <div id="dvContainerdetails" class="modal fade">
										  <div class="modal-dialog">
										    <div class="modal-content">
										      <div class="modal-header">
										        <h4 class="popform_header">Container Detail </h4>
										      </div>
										      <div class="modal-body">
										        <section class="panel panel-default full_form_container material_search_pop_form">
										          <div class="panel-body">
										             <!-- First Row start -->
										            <div class="clearfix odd_row">	                                
	                                                    <div class="col-sm-6">
	                                                      <label class="col-sm-5 control-label">Container No 1</label>
                                                        <div class="col-sm-7">
                                                           <asp:TextBox ID="txtContainrNo" runat="server" CssClass="form-control" Placeholder="Container Number" MaxLength="15" TabIndex="50" ></asp:TextBox>
                                                        </div>
	                                                    </div>
                                                                <div class="col-sm-6">
	                                                        <label class="col-sm-4 control-label">Seal No 1</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtContainerSealNo" runat="server" CssClass="form-control" Placeholder="Seal Number" MaxLength="15" TabIndex="53" ></asp:TextBox>
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
                                                    <asp:DropDownList ID="ddlContainerType" runat="server" CssClass="form-control"  TabIndex="52"></asp:DropDownList>
                                                        </div>
	                                                    </div>
	                                              <div class="col-sm-6">
	                                                        <label class="col-sm-4 control-label">Size</label>
                                                        <div class="col-sm-8">                                                                
                                                            <asp:DropDownList ID="ddlContainerSize" runat="server" CssClass="form-control" TabIndex="51"></asp:DropDownList>
                                                        </div>
	                                                    </div>
                                                        </div> 
                                                        <div class="clearfix even_row">	                                
	                                            <div class="col-sm-6">
	                                                <label class="col-sm-5 control-label">BKG Date From</label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtBKGDateFrom" runat="server" CssClass="input-sm datepicker form-control" MaxLength="10"  onchange="Focus()"></asp:TextBox>
                                                        </div>
	                                                    </div>
	                                                    <div class="col-sm-6">
	                                                        <label class="col-sm-4 control-label">BKG Date To</label>
                                                        <div class="col-sm-8">
                                                        <asp:TextBox ID="txtBKGDateTo" runat="server" CssClass="input-sm datepicker form-control" MaxLength="10"  onchange="Focus()"></asp:TextBox>               
                                                        </div>
	                                                    </div>
                                                        </div> 
                                                <div class="clearfix odd_row">	                                
	                                                 <div class="col-sm-6">
	                                                    <label class="col-sm-5 control-label">Port</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtPortNum" runat="server" CssClass="form-control" Placeholder="Port" MaxLength="15" TabIndex="54" ></asp:TextBox>
                                                        </div>
	                                                    </div>
	                                                   <div class="col-sm-6">
	                                                <label class="col-sm-4 control-label">Type</label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlTypeI" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTypeI_OnSelectedIndexChanged" >
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Import" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Export" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                        </div>
	                                                    </div> 
                                                        </div>
                                                             <div class="clearfix even_row">	                                
	                                            <div class="col-sm-6"> 
	                                               <asp:Label ID="lblTypeI" runat="server"  Font-Bold="true" Text="Select"   CssClass="col-sm-5"></asp:Label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtNameI"  runat="server" CssClass="form-control" MaxLength="100" ></asp:TextBox>
                                                        </div>
	                                                    </div> 
                                                  
									        </div>

                                                  
									        </div>
								        </section>
								        </div>
								        <div class="modal-footer">
								        <div class="popup_footer_btn"> 
                                       
                                        <asp:LinkButton ID="lnkbtnContainerSubmit" runat="server" TabIndex="55" CssClass="btn btn-dark" OnClick="lnkbtnContainerSubmit_OnClick"><i class="fa fa-check"></i>Ok</asp:LinkButton>
                                        &nbsp;&nbsp
                                         <asp:LinkButton ID="lnkbtnClose" runat="server" CssClass="btn btn-dark"  TabIndex="56" OnClick="lnkbtnClose_OnClick"><i class="fa fa-times"></i>Close</asp:LinkButton>
									     
								        </div>
								        </div>
							        </div>
							        </div>
						        </div>

                    <div id="print" style="font-size: 13px;display:none; text-align:center; width:800px">
                <table cellpadding="1" cellspacing="0" width="100%" border="1" style="font-family: Arial,Helvetica,sans-serif;">
                    <tr>
                        <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                            border-left-style: none; border-right-style: none">
                            <strong>
                                <asp:Label ID="lblCompanyname" runat="server" Style="font-size: 18px;"></asp:Label><br />
                            </strong>
                            <asp:Label ID="lblCompAdd1" runat="server"></asp:Label>
                            &nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblCompAdd2" runat="server"></asp:Label><br />
                            <asp:Label ID="lblCompCity" runat="server"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblCompState" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblCompCityPin" runat="server"></asp:Label><br />
                            PH:
                            <asp:Label ID="lblCompPhNo" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblFaxNo" Text="FAX No.:" runat="server"></asp:Label>
                            <asp:Label ID="lblCompFaxNo" runat="server"></asp:Label><br />
                            <asp:Label ID="lblTin" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                                ID="lblCompTIN" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                            border-left-style: none; border-right-style: none">
                            <h3>
                                <strong style="text-decoration: underline">
                                    <asp:Label ID="lblPrintHeadng" runat="server" Text="Advance Booking [For GR]"></asp:Label></strong></h3>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table border="0" width="100%">
                                <tr>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <asp:Label ID="lbltxtgrno" Text="Receipt No." runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <b>
                                            <asp:Label ID="lblGRno" runat="server"></asp:Label></b>
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <asp:Label ID="lbltxtgrdate" Text="Receipt Date" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <b>
                                            <asp:Label ID="lblGrDate" runat="server"></asp:Label></b>
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <asp:Label ID="lbltxtFromcity" Text="Loc.[From]" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <b>
                                            <asp:Label ID="lblFromCity" runat="server"></asp:Label></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <asp:Label ID="lbltxttocity" Text="To City" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <b>
                                            <asp:Label ID="lblToCity" runat="server"></asp:Label></b>
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <asp:Label ID="lbltxtdelvryPlace" Text="Delivery Place" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <b>
                                            <asp:Label ID="lblDelvryPlace" runat="server"></asp:Label></b>
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                      &nbsp;
                                    </td>
                                    <td id="TdlblAgent" runat="server">
                                      &nbsp;
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                   &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <asp:Label ID="lbltxtsendername" Text="Sender Name" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <b>
                                            <asp:Label ID="lblSenderName" runat="server"></asp:Label></b>
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                         <asp:Label ID="lblConsign" Text="Consignor Name" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                       :
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <b>
                                            <asp:Label ID="lblValueConsign" runat="server"></asp:Label></b>
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
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
                                            <td style="font-size: 12px" width="16%">
                                                <strong>Item Name</strong>
                                            </td>
                                            <td style="font-size: 12px" width="10%">
                                                <strong>Unit Name</strong>
                                            </td>
                                            <td style="font-size: 12px" width="10%">
                                                <strong>Quantity</strong>
                                            </td>
                                            <td style="font-size: 12px" width="15%" align="right">
                                                <strong>Weight</strong>
                                            </td>
                                            <div id="DivRepHead" runat="server">
                                                <td style="font-size: 12px" width="20%" align="right">
                                                    <strong>Rate</strong>&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td style="font-size: 12px" width="20%" align="right">
                                                    <strong>Amount</strong>
                                                </td>
                                            </div>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="white_bg" width="10%">
                                                <%#Container.ItemIndex+1 %>.
                                            </td>
                                            <td class="white_bg" width="16%">
                                                <%#Eval("Item_Modl")%>
                                            </td>
                                            <td class="white_bg" width="15%">
                                                <%#Eval("UOM_Name")%>
                                            </td>
                                            <td class="white_bg" width="15%">
                                               <%#Eval("Quantity")%>
                                            </td>
                                            <td class="white_bg" width="15%" align="right">
                                                <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Tot_Weght")))%>
                                            </td>
                                            <div id="DivRepdetails" runat="server">
                                                <td class="white_bg" width="20%" align="right">
                                                   <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Rate")))%> &nbsp;&nbsp;
                                                </td>
                                                 <td class="white_bg" width="20%" align="right">
                                                   <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%>
                                                </td>
                                             </div>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    <tr>
                                            <td class="white_bg" width="10%">
                                                
                                            </td>
                                            <td class="white_bg" width="16%">
                                                
                                            </td>
                                            <td class="white_bg" width="15%">
                                                <strong>Total</strong>
                                            </td>
                                            <td class="white_bg" width="15%">
                                               <asp:Label ID="lblFTQty" Font-Bold="true" runat="server"></asp:Label>
                                            </td>
                                            <td class="white_bg" width="15%" align="right">
                                               <asp:Label ID="lblFTWeight" Font-Bold="true" runat="server"></asp:Label>
                                            </td>
                                            <div id="DivRepFooter" runat="server">
                                            <td class="white_bg" width="20%" align="right">
                                            </td>
                                             <td class="white_bg" width="20%" align="right">
                                               <asp:Label ID="lblFtTotal" Font-Bold="true" runat="server"></asp:Label>
                                            </td>
                                            </div>
                                        </tr>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                    <tr id="trAmount" runat="server">
                        <td>
                            <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table2">
                                <tr>
                                    <td class="white_bg" width="15%">
                                    </td>
                                  
                                    <td class="white_bg" width="15%" align="center">
                                       &nbsp;
                                    </td>
                                    <td class="white_bg" width="10%">
                                    &nbsp;
                                    </td>
                                    <td class="white_bg" width="15%" align="left">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="12.5%">
                                       &nbsp;
                                    </td>
                                    <td class="white_bg" width="13.5%">
                                  <strong>  Round off</strong>
                                    </td>
                                    <td class="white_bg" width="12.5%" align="right">
                                        <asp:Label ID="lblRoundOff" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                  
                                </tr>
                                <tr>
                                    <td class="white_bg" width="15%">
                                    &nbsp;
                                    </td>                                  
                                    <td class="white_bg" width="15%" align="center">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="10%">
                                    &nbsp;
                                    </td>
                                    <td class="white_bg" width="15%" align="left">
                                       &nbsp;
                                    </td>
                                    <td class="white_bg" width="12.5%">
                                      &nbsp;
                                    </td>
                                    <td class="white_bg" width="13.5%">
                                   <strong> Net Amount</strong>
                                    </td>
                                    <td class="white_bg" width="12.5%" align="right">
                                        <asp:Label ID="lblNetAmountrpt" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                  
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="4">
                            <table width="100%" style="font-size: 12px" border="0" cellspacing="0">
                                <tr style="line-height: 25px">
                                    <td colspan="9" style="font-size: 13px" align="left" class="white_bg">
                                        <table width="100%">
                                            <tr>
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
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
                    </form>
                </div>
              </section>
        </div>
    </div>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
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

        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $("#<%=txtOrderDate.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate

            });

            $("#<%=txtRecDate.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate

            });

            $("#<%=txtBKGDateFrom.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate

            });
            $("#<%=txtBKGDateTo.ClientID %>").datepicker({
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

        function consignor(ddlParty) {
            var id = ddlParty.options[ddlParty.selectedIndex].innerHTML;
            document.getElementById("<%=txtconsnr.ClientID %>").value = id;
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


        function openModal() {
            $('#dvContainerdetails').modal('show');
        }

        function cityviaddl() {
            var id = document.getElementById("<%=ddlToCity.ClientID %>").value;
            document.getElementById("<%=ddlLocation.ClientID %>").value = id;
            document.getElementById("<%=ddlviacity.ClientID %>").value = id;
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
    <div id="Amount" class="modal fade">
        <div class="modal-dialog" style="width: 20%">
            <div class="modal-header">
                <h4 class="popform_header">
                    Print&nbsp;&nbsp;&nbsp;&nbsp;</h4>
            </div>
            <div class="modal-content">
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
					<div class="panel-body"> 
                   <%-- <div class="col-sm-3">
                        <asp:DropDownList ID="ddlPage" runat="server" CssClass="form-control">
                            <asp:ListItem Text="4 Pages" Value="4" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="1 Pages" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                        </div> --%>
                <div class="col-sm-12">
                    <asp:LinkButton ID="lnkwithamount" Text="With Amount" 
                            class="btn btn-sm btn-primary" runat="server"
                            OnClick="lnkWithamount_click" ></asp:LinkButton>
                    <asp:LinkButton ID="lnkwithoutamount" Text="Without Amount" 
                            class="btn btn-sm btn-primary" runat="server" 
                            OnClick="lnkwithoutamount_Click" ></asp:LinkButton>
                 </div>
                    </div>
                    </section>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidPages" runat="server" />
    <script language="javascript" type="text/javascript">
        function Divopen() {
            $('#Amount').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
    <script>
        //$(document).ready(function () {
        $(".datepicker").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
            minDate: '<%=hidmindate.Value%>',
            maxDate: '<%=hidmaxdate.Value%>'
        });
        //});
    </script>
</asp:Content>
