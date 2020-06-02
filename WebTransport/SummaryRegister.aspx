<%@ Page Title="Summary Register" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="SummaryRegister.aspx.cs" Inherits="WebTransport.SummaryRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-10">
            <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">SUMMARY REGISTER
                  <span class="view_print"><a href="ManageSummaryRegister.aspx" tabindex="23"><asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a>
                 &nbsp;
                 <asp:LinkButton ID="lnkbtnPrintClick"  runat="server" ToolTip="Click to print" Visible="false" OnClientClick="return CallPrint('print');"><i class="fa fa-print icon"></i></asp:LinkButton>
                 
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
                                <asp:DropDownList ID="ddldateRange" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="1" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddldateRange"
                                    CssClass="form-control" Display="Dynamic" ErrorMessage="Please Select Date Range."  InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>                    
                              </div>
                            </div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 33%;">SummaryNo.<span class="required-field">*</span></label>
                           		<div class="col-sm-3" style="width: 36%;">
                                 <asp:TextBox ID="txtRcptNo" runat="server" CssClass="form-control" MaxLength="10" ReadOnly="true" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="2" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRcptNo"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Enter Summary No." SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div> 
                           		<div class="col-sm-4" style="width: 31%;">
                                  <asp:TextBox ID="txtGRDate" runat="server" CssClass="input-sm datepicker form-control" MaxLength="50" oncopy="return false" oncut="return false" TabIndex="3" onDrop="blur();return false;" onpaste="return false" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtGRDate"
                                        CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select Date." SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>                              
                              </div>                                                           
                           	</div>
                           	<div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 29%;">To City<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 61%;">
                               
                                 <asp:DropDownList ID="ddlFromCity" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="4" OnSelectedIndexChanged="ddlFromCity_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlFromCity"
                                        CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select To City."
                                        InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    
                              </div>
                              <div class="col-sm-1" style="width: 10%;">
                            <asp:LinkButton ID="lnkbtnGrAgnst" runat="server" CssClass="btn btn-sm btn-primary acc_home" data-toggle="modal" data-target="#dvGrdetails" OnClick="lnkbtnGrAgnst_OnClick" TabIndex="5"  ToolTip="Gr Detail" Height="23px" ><i class="fa fa-file"></i></asp:LinkButton>
                                
                              </div>
                            </div>
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 29%;">Challan No.</label>
                              <div class="col-sm-8" style="width: 71%;">                              	
                                 <asp:TextBox ID="txtchlnNo" runat="server" AutoPostBack="true" CssClass="form-control" MaxLength="50" ReadOnly="true" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="6"></asp:TextBox>
                              </div>
                              
                            </div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 33%;">Truck No.</label>
                              <div class="col-sm-9" style="width: 67%;">
                               <asp:DropDownList ID="ddltruckno" runat="server" CssClass="form-control" TabIndex="7" >
                                </asp:DropDownList>          
                              </div>
                           	</div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 29%;">Driver</label>

                              <div class="col-sm-9" style="width: 71%;">
                               <asp:DropDownList ID="ddldriver" runat="server" CssClass="form-control" TabIndex="8" >
                                </asp:DropDownList>
                              </div>
                           	</div>
                          </div>

                        </div>
                      </section>                        
                    </div>
                    
                    <!-- second  section -->

                    <div class="clearfix">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body">
                          <div class="clearfix odd_row">
                            <div class="col-sm-3">
                              <label class="col-sm-5 control-label">CrossingTotal</label>
                              <div class="col-sm-7">
                                <asp:TextBox ID="txtCrossing" runat="server" CssClass="form-control" MaxLength="100" oncopy="return false" ReadOnly="true" oncut="return false" onDrop="blur();return false;" style="text-align:right;" onpaste="return false"  TabIndex="9"></asp:TextBox>
                              </div>
                            </div>
                           	<div class="col-sm-3">
                              <label class="col-sm-3 control-label">Way</label>
                              <div class="col-sm-9">
                               <asp:TextBox ID="txtWAy" runat="server" CssClass="form-control" MaxLength="8" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="10"  AutoPostBack="true"  OnTextChanged="txtWAy_TextChanged"></asp:TextBox>
                               
                              </div>
                            </div>
                            <div class="col-sm-3">
                              <label class="col-sm-5 control-label">Freight</label>
                              <div class="col-sm-7">
                               <asp:TextBox ID="txtfreightCharg" runat="server" CssClass="form-control" MaxLength="100" ReadOnly="true" oncopy="return false" oncut="return false" onDrop="blur();return false;" style="text-align:right;"  onpaste="return false" TabIndex="11" ></asp:TextBox>
                                  
                              </div>
                            </div>
                           	<div class="col-sm-3">
                              <label class="col-sm-5 control-label"> OtherCharges</label>
                              <div class="col-sm-7">
                                <asp:TextBox ID="txtotherCharg" runat="server" CssClass="form-control" MaxLength="8" oncopy="return false" style="text-align:right;" AutoPostBack="true" oncut="return false" onDrop="blur();return false;" onpaste="return false"  TabIndex="12"  OnTextChanged="txtotherCharg_TextChanged"></asp:TextBox>
                           
                              </div>
                            </div>			                              
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-9"></div>
                           	<div class="col-sm-3">
                              <label class="col-sm-5 control-label"> Total</label>
                              <div class="col-sm-7">
                                <asp:TextBox ID="txttotal1" runat="server" CssClass="form-control" MaxLength="100" style="text-align:right;" oncopy="return false"  ReadOnly="true" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                 TabIndex="13" ></asp:TextBox>
                               
                              </div>
                            </div>			                              
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-3">
                              <label class="col-sm-5 control-label">Katt</label>
                              <div class="col-sm-7">
                               <asp:TextBox ID="txtKatt" runat="server" CssClass="form-control" MaxLength="8" style="text-align:right;" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="14" AutoPostBack="true"  OnTextChanged="txtKatt_TextChanged"></asp:TextBox>
                              </div>
                            </div>
                           	<div class="col-sm-3">
                              <label class="col-sm-3 control-label">labour</label>
                              <div class="col-sm-9">
                              <asp:TextBox ID="txtlabour" runat="server" CssClass="form-control" MaxLength="8" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="15" style="text-align:right;"
                                  AutoPostBack="true"  OnTextChanged="txtlabour_TextChanged"></asp:TextBox>
                              </div>
                            </div>
                            <div class="col-sm-3">
                              <label class="col-sm-5 control-label">DeliveryAmnt.</label>
                              <div class="col-sm-7">
                                <asp:TextBox ID="txtDelivery" runat="server" CssClass="form-control" MaxLength="8" style="text-align:right;" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="16" AutoPostBack="true"  OnTextChanged="txtDelivery_TextChanged"></asp:TextBox>
                              </div>
                            </div>
                           	<div class="col-sm-3">
                              <label class="col-sm-5 control-label"> Octrai</label>
                              <div class="col-sm-7">
                               <asp:TextBox ID="txtOctrai" runat="server" CssClass="form-control" MaxLength="8" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="17" AutoPostBack="true"  OnTextChanged="txtOctrai_TextChanged" style="text-align:right;"></asp:TextBox>                              
                              </div>
                            </div>			                              
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-9"></div>
                           	<div class="col-sm-3">
                              <label class="col-sm-5 control-label"> Total</label>
                              <div class="col-sm-7">
                                 <asp:TextBox ID="txttotal2" runat="server" CssClass="form-control" MaxLength="100" style="text-align:right;" oncopy="return false" ReadOnly="true" oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="18" ></asp:TextBox>
                              </div>
                            </div>			                              
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-9"></div>
                           	<div class="col-sm-3">
                              <label class="col-sm-5 control-label">Net Total</label>
                              <div class="col-sm-7">
                               <asp:TextBox ID="txtNetTotal" runat="server" CssClass="form-control" style="text-align:right;" MaxLength="100" oncopy="return false" ReadOnly="true" oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="19" ></asp:TextBox>
                               
                              </div>
                            </div>			                              
                          </div>
                        </div>
                      </section>
                    </div>

                     <!-- fourth row -->
                    <div class="clearfix odd_row">
                      <div class="col-lg-3"></div>
                      <div class="col-lg-6">   
                       <div class="col-lg-4">   
                          <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false"  CssClass="btn full_width_btn btn-s-md btn-info" OnClick="lnkbtnNew_OnClick" 
                                                                    TabIndex="22" ><i class="fa fa-file-o"></i>New</asp:LinkButton>   
       
                </div>                            
                        <div class="col-sm-4">
                          <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true"   ValidationGroup="save" CssClass="btn full_width_btn btn-s-md btn-success" 
                             OnClick="lnkbtnSave_OnClick" TabIndex="20" ><i class="fa fa-save"></i>Save</asp:LinkButton>
                         
                        
                        </div>
                        <div class="col-sm-4">
                                           <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" 
                                                                    CssClass="btn full_width_btn btn-s-md btn-danger" 
                                                                    OnClick="lnkbtnCancel_OnClick" TabIndex="21" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
                        
                        </div>
                      </div>
                      <div class="col-lg-3"></div>
                    </div>

                    <!-- popup form GR detail -->
					<div id="dvGrdetails" class="modal fade">
										  <div class="modal-dialog">
										    <div class="modal-content">
										      <div class="modal-header">
										        <h4 class="popform_header">Gr Detail </h4>

                                                 <asp:Label ID="lblmsg2" runat="server" Text="Message - Please select only one GR at a time."
                                                         Visible="false"></asp:Label>
										      </div>
										      <div class="modal-body">
										        <section class="panel panel-default full_form_container material_search_pop_form">
										          <div class="panel-body">
										             <!-- First Row start -->
										            <div class="clearfix odd_row">	                                
	                                <div class="col-sm-4">
	                                  <label class="col-sm-5 control-label">Date From</label>
                                    <div class="col-sm-7">
                                       <asp:TextBox ID="txtDateFromDiv" runat="server" CssClass="input-sm datepicker form-control" TabIndex="50" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtFrm" runat="server" ErrorMessage="Select From Date!"
                                            Display="Dynamic" CssClass="classValidation" ControlToValidate="txtDateFromDiv" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                           </div>
	                                        </div>
	                                        <div class="col-sm-4">
	                                          <label class="col-sm-4 control-label">Date To</label>
                                            <div class="col-sm-8">
                                              <asp:TextBox ID="txtDateToDiv" runat="server" CssClass="input-sm datepicker form-control" TabIndex="51" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvRcptEntryDtTo" runat="server" ErrorMessage="Select To Date!"
                                                    Display="Dynamic" CssClass="classValidation" ControlToValidate="txtDateToDiv" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                     
                                            </div>
	                                        </div>
                                            <div class="col-sm-2">
	                                           <asp:LinkButton ID="lnkbtnSearchClick" runat="server" CssClass="btn full_width_btn btn-sm btn-primary" TabIndex="52" OnClick="lnkbtnSearchClick_OnClick">Search</asp:LinkButton>
	                                        </div>
	                                      </div> 
                                                  <div class="clearfix fourth_right">
                                                <section class="panel panel-in-default btns_without_border">                            
                                                    <div class="panel-body">     
                                                    <div class="clearfix">
		                                                    <section class="panel panel-default full_form_container material_search_pop_form">
		                                                    <div class="panel-body">   
                                                            <div id="selectall" runat="server" style="padding-left: 100px" visible="false">
                                                                    Select All&nbsp;&nbsp;
                                                                    <asp:CheckBox ID="chkSelectAllRows" runat="server" AutoPostBack="true" Visible="false" />
                                                                </div>

                                                                <asp:GridView ID="grdGrdetals" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="display nowrap dataTable"
                                            Width="100%" BorderStyle="None" BorderWidth="0">
                                            <RowStyle CssClass="odd" />
                                            <AlternatingRowStyle CssClass="even" />  
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select" HeaderStyle-Width="40px">                                                  
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAllCheckboxes(this);"
                                                            CssClass="SACatA" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkId" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rcpt No." HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("Reg_No"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToDateTime(Eval("Reg_Date")).ToString("dd-MMM-yyyy")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To City" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%#Eval("To_City")%>
                                                        <asp:HiddenField ID="hidGrIdno" runat="server" Value='<%#Eval("ChlnDelvHead_Idno")%>' />
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
                                                        <div class="col-lg-12">   
                                                        <asp:Label ID="lblmsg" runat="server" Text="Message - Please select only one GR at a time."
                                                        Visible="false" CssClass="classValidation"></asp:Label>
                                                        </div>
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
                                        <asp:HiddenField ID="HidGrAgnstRcptIdno" runat="server" />
                                        <asp:LinkButton ID="lnkbtnDivSubmit" runat="server" CssClass="btn btn-dark" OnClick="lnkbtnDivSubmit_OnClick" TabIndex="4"><i class="fa fa-check"></i>Ok</asp:LinkButton>
                                        &nbsp;&nbsp
									        <button type="submit" class="btn btn-dark" data-dismiss="modal"><i class="fa fa-times"  TabIndex="5"></i>Close</button>
								        </div>
								        </div>
							        </div>
							        </div>
						        </div>                     
                                        
                  </form>
                </div>
              </section>
        </div>
        <div class="col-lg-1">
        </div>
    </div>
    <table border="0" cellpadding="2" cellspacing="0" class="border" width="100%">
        <tr style="display: none">
            <td class="white_bg" align="center">
                <div id="print" style="font-size: 13px;">
                    <table cellpadding="1" cellspacing="0" width="800px" border="1" style="font-family: Arial,Helvetica,sans-serif;">
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
                                <asp:Label ID="lblCompCityPin" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCompPhNo" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblFaxNo" Text="FAX No.:" runat="server"></asp:Label>
                                <asp:Label ID="lblCompFaxNo" runat="server"></asp:Label><br />
                                <asp:Label ID="lblTin" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                                    ID="lblCompTIN" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 13px;
                                border-left-style: none; border-right-style: none">
                                <h3>
                                    <strong style="text-decoration: underline">
                                        <asp:Label ID="lblPrintHeadng" runat="server" Text="Summary Account"></asp:Label></strong></h3>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table border="0" width="100%">
                                    <tr>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lblsummaryNo" Text="Summary No." runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            :
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lblSRno" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lbltxtgrdate" Text="Summary Date" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            :
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lblSrDate" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lbltextTocity" Text="To City" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            :
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lbltooCity" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lbltxttruckno" Text="Truck No." runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            :
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lblvalueTruck" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lbltxtchlnno" Text="Challan No." runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            :
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lblvaluechlnno" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lbltxtdriver" runat="server" Text="Driver"></asp:Label>
                                        </td>
                                        <td id="Tdlbldriver" align="left" class="white_bg" valign="top" style="font-size: 13px;
                                            border-right-style: none" runat="server">
                                            :
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lblvaluedriver" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table border="0" cellspacing="0" style="font-size: 13px" width="100%" id="Table1">
                                    <tr>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; width: 23%;
                                            border-right-style: none">
                                        </td>
                                        <td align="right" class="white_bg" valign="top" style="font-size: 13px; width: 23%;
                                            border-right-style: none">
                                            <b>Rs.</b>
                                        </td>
                                        <td align="center" class="white_bg" valign="top" style="font-size: 13px; width: 6%;
                                            border-right-style: none">
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; width: 23%;
                                            border-right-style: none">
                                        </td>
                                        <td align="right" class="white_bg" valign="top" style="font-size: 13px; width: 23%;
                                            border-right-style: none">
                                            <b>Rs.</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lbltxtcrossing" Text="Crossing" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="right" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lblvaluecrossng" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" class="white_bg" valign="top" style="font-size: 13px; width: 6%;
                                            border-right-style: none">
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lbltxtdelivry" Text="Delivery" runat="server"></asp:Label>
                                            </b>
                                        </td>
                                        <td align="right" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lblvaluedelivery" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lbltxtway" Text="Way Exp." runat="server"></asp:Label></b>
                                        </td>
                                        <td align="right" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lblvalueway" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" class="white_bg" valign="top" style="font-size: 13px; width: 6%;
                                            border-right-style: none">
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lbltxtlabor" Text="Labour" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="right" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lblvaluelabour" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lbltxtFrieght" Text="Frieght" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="right" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lblPrintFrieght" Text="Frieght" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" class="white_bg" valign="top" style="font-size: 13px; width: 6%;
                                            border-right-style: none">
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lbltxtKatt" Text="Katt" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="right" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lblvalueKatt" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lbltxtother" Text="Other Exp." runat="server"></asp:Label>
                                            </b>
                                        </td>
                                        <td align="right" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lblvalueother" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" class="white_bg" valign="top" style="font-size: 13px; width: 6%;
                                            border-right-style: none">
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="Label5" Text="Octrai" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="right" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lblvalueoctrai" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr id="tr1" runat="server" style="border: 1px">
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="Label2" Text="Total" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="right" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="Lbltotal1" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" class="white_bg" valign="top" style="font-size: 13px; width: 6%;
                                            border-right-style: none">
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lbltxttotal2" Text="Total" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="right" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lblvaluetotal2" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="tr2" runat="server" style="border: 1px">
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="Label4" Text="Net Amount" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="right" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lblvaluenetamnt" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" class="white_bg" valign="top" style="font-size: 13px; width: 6%;
                                            border-right-style: none">
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        </td>
                                        <td align="center" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="4">
                                <table width="100%" style="font-size: 13px" border="0" cellspacing="0">
                                    <tr style="line-height: 25px">
                                        <td colspan="9" style="font-size: 13px" align="left" class="white_bg">
                                            <table width="100%">
                                                <tr>
                                                    <td align="left" class="white_bg" style="font-size: 13px" width="50%">
                                                        Note:
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <b>Driver's Signature</b>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td align="right" class="white_bg" style="font-size: 13px" valign="top" width="50%">
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <b>Clerk's Signature&nbsp;</b>
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
    </table>
    <asp:HiddenField ID="hidSummryRegidno" runat="server" />
     <asp:HiddenField ID="hidmindate" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />
    <asp:HiddenField ID="hidchlnIdno" runat="server" />
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent1 = "<table width='100%' border='0'></table>";
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=300,height=300,toolbar=1,scrollbars=1,status=1');
            WinPrint.document.write(prtContent1);
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            return false;
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

        function openModal() {
            $('#dvGrdetails').modal('show');
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

        function ShowClient() {
            $("#dvGrdetails").fadeIn(300);
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
  
