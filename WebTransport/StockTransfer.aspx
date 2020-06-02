<%@ Page Title="Stock Transfer" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="StockTransfer.aspx.cs" EnableEventValidation="false" Inherits="WebTransport.StockTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="row ">
                <div class="col-lg-1"></div>
                <div class="col-lg-9">
	                <section class="panel panel-default full_form_container quotation_master_form">
	                  <header class="panel-heading font-bold form_heading">Stock Transfer 
                      
	                    <span class="view_print"><a href="ManageStockTransfer.aspx"><asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a>&nbsp;
                        <asp:ImageButton ID="imgPrint" runat="server" AlternateText="Print" ImageUrl="~/images/print.jpeg"
                                                        Visible="false" title="Print" OnClientClick="return CallPrint('print');" Height="16px" />
                        </span>
	                  </header>
	                  <div class="panel-body">
	                    <form class="bs-example form-horizontal">
	                      <!-- first  section --> 
	                      <div class="clearfix first_section">
	                        <section class="panel panel-in-default">  
	                          <div class="panel-body">
	                          	<div class="clearfix odd_row">
	                              <div class="col-sm-5">
	                                <label class="col-sm-4 control-label">Date Range</label>
	                                <div class="col-sm-8">
	                                <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control"
                                        AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged">
                                    </asp:DropDownList>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                    ControlToValidate="ddlDateRange" ValidationGroup="save" ErrorMessage="Please select date range."
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
	                                </div>
	                              </div>
	                             	<div class="col-sm-4">
	                             		<label class="col-sm-6 control-label">Date</label>
	                                <div class="col-sm-6" >
	                                  <asp:TextBox ID="txtDate" runat="server" TabIndex="2" CssClass="input-sm datepicker form-control" onkeydown = "return DateFormat(this, event.keyCode)" ></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic"
                                    ControlToValidate="txtDate" ValidationGroup="save" ErrorMessage="Please select date" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                                       
	                                </div>
	                             	</div>
                                    <div class="col-sm-3">
                                    <label class="col-sm-5 control-label">Issue No.</label>
                                       <div class="col-sm-5" >
                                         <asp:TextBox ID="txtIssueNo" runat="server"  TabIndex="3" CssClass="form-control"
                                                Style="text-align: left;" Enabled="false"></asp:TextBox>   
                                       </div>
                                    </div>
	                            </div>
	                            
	                            <div class="clearfix even_row">
	                              <div class="col-sm-5">
	                                <label class="col-sm-4 control-label">Issuing Loc<span class="required-field">*</span></label>
	                                <div class="col-sm-8">
	                                <asp:DropDownList ID="drpBaseCity" runat="server" TabIndex="4" CssClass="form-control" 
                                        AutoPostBack="true" OnSelectedIndexChanged="drpBaseCity_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvdrpBaseCity" runat="server" ControlToValidate="drpBaseCity"
                                        Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="save"
                                        class="classValidation" ErrorMessage="Select From City."></asp:RequiredFieldValidator>                            
	                                </div>
	                              </div>
	                             	<div class="col-sm-6">
	                                <label class="col-sm-4 control-label">Receiving Loc<span class="required-field">*</span></label>
	                                <div class="col-sm-4">
	                                <asp:DropDownList ID="drpDeliveryPlace" runat="server" TabIndex="5" 
                                            CssClass="form-control" onchange="javascript:Comparecity();"
                                            onselectedindexchanged="drpDeliveryPlace_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvdrpDeliveryPlace" runat="server" ControlToValidate="drpDeliveryPlace"
                                        Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="save"
                                        class="classValidation" ErrorMessage="Select Delivery Place."></asp:RequiredFieldValidator>                           
	                                </div>
	                              </div>
	                            </div>	
	                            <div class="clearfix odd_row">
	                              <div class="col-sm-12">
	                                <label class="col-sm-2 control-label" style="width:108px;">Remarks<span class="required-field">*</span></label>
	                                <div class="col-sm-10">
	                                  <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TabIndex="6"
                                                 Style="resize:none;"></asp:TextBox>
                                        
	                                </div>
	                              </div>
	                             	<div class="col-sm-6"></div>
	                            </div>	

	                          </div>
	                        </section>                        
	                      </div>
	                      
	                      <!-- second  section -->
	                   		<div class="clearfix second_section">
	                        <section class="panel panel-in-default">  
	                          <div class="panel-body">
	                            <div class="clearfix even_row">
	                              <div class="col-sm-2">
	                                <label class="control-label">Item Type<span class="required-field">*</span></label>
	                                <div>
	                                 <asp:DropDownList ID="drpItemType" runat="server" TabIndex="7" AutoPostBack="true"
                                        CssClass="form-control" Height="30px" 
                                            onselectedindexchanged="drpItemType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvdrpItemType" runat="server" ControlToValidate="drpItemType"
                                        Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"
                                        class="classValidation" ErrorMessage="Select Item Type"></asp:RequiredFieldValidator>                      
	                                </div>
	                              </div>
	                              
                                  <div class="col-sm-2">
	                                <label class="control-label">Item Name<span class="required-field">*</span></label>
	                                <div>
	                                 <asp:DropDownList ID="drpItemName" runat="server" TabIndex="7" AutoPostBack="true"
                                        CssClass="form-control" Height="30px" onselectedindexchanged="drpItemName_SelectedIndexChanged" 
                                           >
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpItemName"
                                        Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"
                                        class="classValidation" ErrorMessage="Select Item Name"></asp:RequiredFieldValidator>                      
	                                </div>
	                              </div>

                                  	<div class="col-sm-2">
	                                <label class="control-label">Serial No.<span class="required-field">*</span></label>
                                    
	                                <div>
	                                 <asp:DropDownList ID="drpSerialNo" runat="server" TabIndex="8" AutoPostBack="true"
                                        CssClass="form-control" Height="30" onselectedindexchanged="drpSerialNo_SelectedIndexChanged" 
                                            >
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSerialNo" runat="server" ControlToValidate="drpSerialNo"
                                        Display="Dynamic" InitialValue="0" SetFocusOnError="true" ValidationGroup="Submit"
                                        class="classValidation" ErrorMessage="Select Serial No"></asp:RequiredFieldValidator>
	                                </div>
	                              </div>

                                   <div class="col-sm-2">
	                                <label class="control-label">Tyre Type<span class="required-field">*</span></label>
	                                <div>
	                                   <asp:DropDownList ID="ddlTyreType" runat="server" TabIndex="9"
                                            CssClass="form-control" Height="30">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="New" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Old" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Retreated" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlTyreType" runat="server" ControlToValidate="ddlTyreType"
                                            Display="Dynamic" InitialValue="0" SetFocusOnError="true" ValidationGroup="Submit"
                                            class="classValidation" ErrorMessage="Select Tyre Type"></asp:RequiredFieldValidator>                       
	                                </div>
	                              </div>

                                   <div class="col-sm-1">
	                                <label class="control-label">QTY.<span id="SpanQty" runat="server" class="required-field">*</span></label>
	                                <div>
	                                 <asp:TextBox ID="txtQty" runat="server" TabIndex="10" CssClass="form-control"
                                        MaxLength="7" Style="text-align: right;" Text="0.00" OnTextChanged="txtQty_TextChanged"
                                        AutoPostBack="true"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvQty" runat="server" ControlToValidate="txtQty"
                                            Display="Dynamic" InitialValue="0" SetFocusOnError="true" ValidationGroup="Submit"
                                            class="classValidation" ErrorMessage="Select Qty."></asp:RequiredFieldValidator>                       
	                                </div>
	                              </div>

                                   <div class="col-sm-2" style="width:85px;">	                                
	                               <label class="control-label">Rate<span class="required-field">*</span></label>
	                                <div>
	                                   <asp:TextBox ID="txtRate" runat="server" TabIndex="11"
                                            CssClass="form-control" MaxLength="10" Style="text-align: right;" Text="0.00" AutoPostBack="true"
                                            OnTextChanged="txtRate_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtRate" runat="server" ControlToValidate="txtRate"
                                            Display="Dynamic" SetFocusOnError="true" ValidationGroup="Submit" class="classValidation"
                                            ErrorMessage="Enter Rate "></asp:RequiredFieldValidator>
	                                </div>
	                              </div>
                                   <div class="col-sm-2" style="width:85px;">	                                
	                               <label class="control-label">Amount<span class="required-field">*</span></label>
	                                <div>
	                                   <asp:TextBox ID="txtAmount" runat="server" TabIndex="11" Enabled="false"
                                            CssClass="form-control" MaxLength="10" Style="text-align: right;" Text="0.00" 
                                            ></asp:TextBox>
	                                </div>
	                              </div>
	                            </div>
	                            <div class="clearfix even_row">
	                              
                                  <div class="col-sm-3">
                                  </div>
	                             <div class="col-sm-2"></div>
	                             
                                 <div class="col-sm-2"></div>
	                             <div class="col-sm-2"></div>
	                             <div class="col-sm-3">
	                                <div class="col-sm-6">
                                    <asp:LinkButton ID="lnkbtnSubmit" runat="server" OnClick="lnkbtnSubmit_OnClick" CssClass="btn full_width_btn btn-sm btn-primary subnew" TabIndex="12"  ToolTip="Click to Submit" CausesValidation="true" ValidationGroup="Submit" >Submit</asp:LinkButton>                                   
                                  </div>
                                  <div class="col-sm-6">
                                   <asp:LinkButton ID="lnkbtnNew" runat="server" OnClick="lnkbtnNew_OnClick" CssClass="btn full_width_btn btn-sm btn-primary subnew" TabIndex="13" ToolTip="Click to new" >New</asp:LinkButton>
                                   <asp:HiddenField ID="hiduomid" runat="server" Value="0" />
                                   <asp:HiddenField ID="hidrowid" runat="server" />
                                  </div>
	                              </div>
	                            </div>                            
	                          </div>
	                        </section>                        
	                      </div>

	                      <div class="clearfix third_right">
	                        <section class="panel panel-in-default btns_without_border">                            
	                          <div class="panel-body" style="overflow-x:scrol;">     
                              <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false"  CssClass="display nowrap dataTable"
                                    Width="100%"  EnableViewState="true" AllowPaging="true" 
                                    ShowFooter="true" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand"
                                    OnRowDataBound="grdMain_RowDataBound" PageSize="50">
                                    <RowStyle CssClass="odd" />
                                    <AlternatingRowStyle CssClass="even" />    
                                    <Columns>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="50" CssClass="gridHeaderAlignCenter" />
                                        <ItemTemplate>
                                              <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("id") %>' CommandName="cmdedit" TabIndex="5" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                 <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("id") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" TabIndex="6" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>                                          
                                            </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="40" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Type" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="left">
                                        <ItemStyle Width="50" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblitemname" runat="server" Text='<%#Eval("ITEM_TYPE")%>'></asp:Label>
                                            <asp:HiddenField ID="hditemid" runat="server" Value='<%#Eval("ITEM_TYPEID")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="left">
                                        <ItemStyle Width="50" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblname" runat="server" Text='<%#Eval("ITEM_NAME")%>'></asp:Label>
                                            <asp:HiddenField ID="hdid" runat="server" Value='<%#Eval("ITEM_ID")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Serial" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbluomname" runat="server" Text='<%#Eval("ITEM_SERIAL")%>'></asp:Label>
                                            <asp:HiddenField ID="hiduomid" runat="server" Value='<%#Eval("ITEM_SERIALID")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tyre Type" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                          <asp:Label ID="lbltyretype" runat="server" Text='<%#Eval("TYRE_TYPE")%>'></asp:Label>
                                            <asp:HiddenField ID="hidtyreID" runat="server" Value='<%#Eval("TYRE_TYPEID")%>' />
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblItemTotal" Font-Bold="true" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                     
                                    <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="50" HeaderStyle-CssClass="gridHeaderAlignRight">
                                        <ItemStyle Width="50" CssClass="gridHeaderAlignRight" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblqty" runat="server" Text='<%#Eval("ITEM_QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblqtytotal" Text="TotalQty:" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="50" HeaderStyle-CssClass="gridHeaderAlignRight">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("ITEM_RATE"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="50" HeaderStyle-CssClass="gridHeaderAlignRight">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("ITEM_AMOUNT")))%>
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
	                            <div class="clearfix even_row">
	                             
                                  <div class="col-sm-8"></div>
	                              	<div class="col-sm-4">
			                                <label class="col-sm-6 control-label">Net Amount</label>
			                                <div class="col-sm-6">
			                                <asp:TextBox ID="txtNetAmnt" runat="server" CssClass="form-control" Enabled="true"
                                                placeholder="0" ReadOnly="True" Style="text-align: right;" TabIndex="22" Text="0.00"
                                                Width="80"></asp:TextBox>
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
                                                    <asp:LinkButton ID="lnkbtnAdd" runat="server" CausesValidation="false" TabIndex="16" CssClass="btn full_width_btn btn-s-md btn-info" OnClick="lnkbtnAdd_OnClick" ><i class="fa fa-file-o"></i>New</asp:LinkButton>                                                            	
													</div>                                  
													<div class="col-sm-4">

                                                    <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" TabIndex="14" ValidationGroup="save" CssClass="btn full_width_btn btn-s-md btn-success" OnClick="lnkbtnSave_OnClick" ><i class="fa fa-save"></i>Save</asp:LinkButton>                      
													</div>
													<div class="col-sm-4">
                                                    <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" TabIndex="15" CssClass="btn full_width_btn btn-s-md btn-danger" OnClick="lnkbtnCancel_OnClick" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
													</div>
												</div>
												<div class="col-lg-2">
                                                 <asp:HiddenField ID="hidStckid" runat="server" Value="0" />
                                                <asp:HiddenField ID="hidmindate" runat="server" Value="" />
                                                <asp:HiddenField ID="hidmaxdate" runat="server" Value="" />
                                                <asp:HiddenField ID="hidShrtgRate" runat="server" />
                                                <asp:HiddenField ID="hidShrtgLimit" runat="server" />
                                                <asp:HiddenField ID="HidTbbType" runat="server" />
                                                </div>
											</div> 
										</div>
									</section>
								</div>        
	                    </form>
	                  </div>
	                </section>
                </div>
                <div id="print" style="font-size: 13px;display:none">
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
                                                <asp:Label ID="lblPrintHeadng" runat="server" Text="Stock Transfer"></asp:Label></strong></h3>
                                    </td>
                                </tr>
                             <tr>
                                    <td colspan="4">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                    <asp:Label ID="lbltxtgrno" Text="Issue No." runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                    <b>
                                                        <asp:Label ID="lblGRno" runat="server"></asp:Label></b>
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                    <asp:Label ID="lbltxtgrdate" Text="Issue Date" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                    <b>
                                                        <asp:Label ID="lblGrDate" runat="server"></asp:Label></b>
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                    <asp:Label ID="lbltxtFromcity" Text="From City" runat="server"></asp:Label>
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
                                                <%--<td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
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
                                                    <asp:Label ID="lbltxtsendername" Text="Sender Name" runat="server"></asp:Label>
                                                </td>
                                                <td id="TdlblAgent" runat="server">
                                                    :
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                    <b>
                                                        <asp:Label ID="lblSenderName" runat="server"></asp:Label></b>
                                                </td>--%>
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
                                                        <td style="font-size: 12px" width="20%">
                                                            <strong>Item Type</strong>
                                                        </td>
                                                         <td style="font-size: 12px" width="10%">
                                                            <strong>Item Name</strong>
                                                        </td>
                                                        <td style="font-size: 12px" width="10%">
                                                            <strong>Serial No</strong>
                                                        </td>
                                                        <td style="font-size: 12px" width="10%">
                                                            <strong>Quantity</strong>
                                                        </td>

                                                        <td style="font-size: 12px" align="left" width="10%">
                                                            <strong>Item Rate</strong>
                                                        </td>
                                                        <td style="font-size: 12px" align="left" width="10%">
                                                            <strong>Amount</strong>
                                                        </td>
                                                    </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="white_bg" width="10%">
                                                            <%#Container.ItemIndex+1 %>.
                                                        </td>
                                                        <td class="white_bg" width="30%">
                                                            <%#Eval("ItemType")%>
                                                        </td>
                                                        <td class="white_bg" width="15%">
                                                            <%#Eval("Item_Name")%>
                                                        </td>
                                                        <td class="white_bg" width="15%">
                                                            <%#Eval("SerialNo")%>
                                                        </td>
                                                        <td class="white_bg" width="15%">
                                                            <%#Eval("Qty")%>
                                                        </td>

                                                        <td class="white_bg" width="15%" align="left">
                                                            <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Rate")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td class="white_bg" width="15%" align="left">
                                                            <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                    <td>
                                        <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table2">
                                            <tr>
                                                <td class="white_bg" width="15%">
                                                </td>
                                                <td class="white_bg" width="15%">
                                                </td>
                                                <td class="white_bg" width="32%" align="center">
                                                    <asp:Label ID="lblttl" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                                <td class="white_bg" width="15%" align="left">
                                                    <asp:Label ID="lbltotalqty" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                                <td class="white_bg" width="13.5%">
                                                    <asp:Label ID="lbltotalWeight" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                                <td class="white_bg" width="12.5%">
                                                <asp:Label ID="lblTotalAmnt" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                                <td class="white_bg" width="12.5%" align="center">
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                             <tr>
                                    <td style="width: 100%">
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" align="left" width="40%">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                Remark : <asp:Label ID="lblremark" runat="server" valign="right"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td colspan="2" width="60%">
                                                    <table>
                                                       <%-- <tr>
                                                            <td>
                                                                <asp:Label ID="lblgrossAmnt" runat="server" Text="Gross Amnt." Font-Size="13px" valign="right"></asp:Label>
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="valuelblgrossAmnt" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                            </td>
                                                            <td style="width: 5px">
                                                            </td>
                                                            <td>
                                                                &nbsp;&nbsp;
                                                                <asp:Label ID="lblOthramnt" runat="server" Text="Other Amnt." Font-Size="13px" valign="right"></asp:Label>
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="valuelblOthramnt" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                            </td>
                                                        </tr>--%>
                                                        <tr>
                                                          <%--  <td>
                                                                <asp:Label ID="lblroundoffAmnt" runat="server" Text="RoundOff Amnt." Font-Size="13px"
                                                                    valign="right"></asp:Label>
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="valuelblroundoffAmnt" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                            </td>
                                                            <td style="width: 5px">
                                                            </td>--%>
                                                            <td>
                                                                &nbsp;&nbsp;
                                                                <asp:Label ID="lblNetAmnt" runat="server" Text="Net Amnt." Font-Size="13px" valign="right"></asp:Label>
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="valuelblNetAmnt" runat="server" Font-Size="13px" valign="right"></asp:Label>
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
            </div>

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent1 = "<table width='100%' border='0'></table>";
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=100%,height=100%,toolbar=1,scrollbars=1,status=1');
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
            var QtnDate = $('#<%=txtDate.ClientID %>').val();
            $("#<%=txtDate.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
        }

        function Comparecity() {
            var id = document.getElementById("<%=drpBaseCity.ClientID %>").value;
            var id1= document.getElementById("<%=drpDeliveryPlace.ClientID %>").value;
            if (id == id1) {
                PassMessageError("Issuing Location and Receiving Location can't be same");
            }
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