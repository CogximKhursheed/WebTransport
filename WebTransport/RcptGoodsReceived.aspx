<%@ Page Title="Receipt For Goods Received" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="RcptGoodsReceived.aspx.cs" EnableEventValidation="false"
    Inherits="WebTransport.RcptGoodsReceived" %>

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

            $("#<%=txtReceiptDate.ClientID %>").datepicker({
                buttonImageOnly: false,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate,
                changeMonth: true,
                changeYear: true,
                focus: true
            });
        }
        
    </script>
    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-2">
                </div>
                <div class="col-lg-8">
                    <section class="panel panel-default full_form_container part_purchase_bill_form">
	                  <header class="panel-heading font-bold form_heading">RECEIPT FOR GOODS RECEIVED
	                    <span class="view_print"><a href="ManageReceiptGoodsReceived.aspx" tabindex="21"><asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a>&nbsp;
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
	                              <div class="col-sm-6">
	                                <label class="col-sm-4 control-label">Date Range<span class="required-field">*</span></label>
	                                <div class="col-sm-8">
	                                  <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control"
                                                     AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged">
                                      </asp:DropDownList>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDateRange"
                                            Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="save"
                                            class="classValidation" ErrorMessage="Select Date Range."></asp:RequiredFieldValidator>    
	                                </div>
	                              </div>
	                             	<div class="col-sm-6">
                                      <label class="col-sm-4 control-label">Location[From]<span class="required-field">*</span></label>
	                                <div class="col-sm-8">
	                                   <asp:DropDownList ID="drpBaseCity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpBaseCity_OnSelectedIndexChanged" CssClass="form-control" TabIndex="2" >
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvdrpBaseCity" runat="server" ControlToValidate="drpBaseCity"
                                            Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="save"
                                            class="classValidation" ErrorMessage="Select From City."></asp:RequiredFieldValidator>                     
	                                </div>
                                    </div>
	                            </div>
	                            <div class="clearfix even_row">
	                              <div class="col-sm-6">
	                                <label class="col-sm-4 control-label">Date</label>
	                                <div class="col-sm-4">
	                                  <asp:TextBox ID="txtReceiptDate" runat="server" TabIndex="3" CssClass="input-sm datepicker form-control" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtReceiptDate" runat="server" ControlToValidate="txtReceiptDate"
                                                Display="Dynamic" SetFocusOnError="true" ValidationGroup="save" class="classValidation"
                                                ErrorMessage="Enter Receipt Date."></asp:RequiredFieldValidator>                           
	                                </div>
	                                <div class="col-sm-4" style="padding: 0">
		                                
		                                <div class="col-sm-12">
		                                   <asp:TextBox ID="txtReceiptNo" runat="server" TabIndex="4" CssClass="form-control"
                                        ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtReceiptNo" runat="server" ControlToValidate="txtReceiptNo"
                                        Display="Dynamic" SetFocusOnError="true" ValidationGroup="save" class="classValidation"
                                        ErrorMessage="Enter Receipt No."></asp:RequiredFieldValidator> 
		                                </div>
		                              </div>
	                              </div>
	                             	<div class="col-sm-6">
	                             <label class="col-sm-4 control-label">To City<span class="required-field">*</span></label>
	                                <div class="col-sm-8">
	                                   <asp:DropDownList ID="drpToCity" runat="server" CssClass="form-control" TabIndex="5" onchange="javascript:cityviaddl();" >
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvdrpToCity" runat="server" ControlToValidate="drpToCity"
                                            Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="save"
                                            class="classValidation" ErrorMessage="Select To City."></asp:RequiredFieldValidator>                           
	                                </div>
	                              </div>
	                            </div>
	                            <div class="clearfix odd_row">
	                              <div class="col-sm-6">
	                                    <label class="col-sm-4 control-label">City Via<span class="required-field">*</span></label>
	                                <div class="col-sm-8">
	                                 <asp:DropDownList ID="drpCityVia" runat="server" CssClass="form-control" TabIndex="6">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvdrpCityVia" runat="server" ControlToValidate="drpCityVia"
                                        Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="save" 
                                        class="classValidation" ErrorMessage="Select Delivery Place."></asp:RequiredFieldValidator>                      
	                                </div>
	                              </div>
	                             	<div class="col-sm-6">
	                                <label class="col-sm-4 control-label">Delivery Place<span class="required-field">*</span></label>
	                                <div class="col-sm-8">
	                                 <asp:DropDownList ID="drpDeliveryPlace" runat="server" CssClass="form-control" TabIndex="6" >
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvdrpDeliveryPlace" runat="server" ControlToValidate="drpDeliveryPlace"
                                        Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="save"
                                        class="classValidation" ErrorMessage="Select Delivery Place."></asp:RequiredFieldValidator>                      
	                                </div>
	                              </div>
	                            </div>	
	                            <div class="clearfix even_row">
	                              <div class="col-sm-6">
	                                <label class="col-sm-4 control-label">Sender<span class="required-field">*</span></label>
	                                <div class="col-sm-8">
	                                  <asp:DropDownList ID="drpSender" TabIndex="8" runat="server" CssClass="form-control" >
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvdrpSender" runat="server" ControlToValidate="drpSender"
                                            Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="save"
                                           CssClass="classValidation" ErrorMessage="Select Sender Name."></asp:RequiredFieldValidator>                          
	                                </div>
	                              </div>
	                             	<div class="col-sm-6">
	                                <label class="col-sm-4 control-label">Sender No.<span class="required-field">*</span></label>
	                                <div class="col-sm-8">
	                                 <asp:TextBox ID="txtSenderNo" runat="server" CssClass="form-control" TabIndex="9"
                                                                                        MaxLength="10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSenderNo"
                                        Display="Dynamic" SetFocusOnError="true" ValidationGroup="save" CssClass="classValidation"
                                        ErrorMessage="Please Enter Sender No."></asp:RequiredFieldValidator>                          
	                                </div>
	                              </div>
	                            </div>	
	                            <div class="clearfix odd_row">
	                             
	                             	<div class="col-sm-6">
	                                <label class="col-sm-4 control-label">Receiver<span class="required-field">*</span></label>
	                                <div class="col-sm-8">
	                                   <asp:DropDownList ID="drpReceiver" runat="server" CssClass="form-control" TabIndex="7" >
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvdrpReceiver" runat="server" ControlToValidate="drpReceiver"
                                            Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="save"
                                            CssClass="classValidation" ErrorMessage="Select Receiver Name."></asp:RequiredFieldValidator>                           
	                                </div>
	                              </div>
                                   <div class="col-sm-6">
	                                  <label class="col-sm-4 control-label">Agent Name</label>
	                                <div class="col-sm-8">
	                                  <asp:DropDownList ID="drpAgentName" runat="server" CssClass="form-control" TabIndex="10" >
                                     </asp:DropDownList>                            
	                                </div>
	                              </div>
	                            </div>	

	                          </div>
	                        </section>                        
	                      </div>
	                      
	                      <div class="clearfix third_right">
	                        <section class="panel panel-in-default">                            
	                          <div class="panel-body">     
	                      
	                            <div class="dataTables_add_entry">
                                  <div class="clearfix estimate_first1_row even_row">
                                    <div class="col-sm-2">
                                      <label class="control-label">ItemName<span class="required-field">*</span></label>
                                      <asp:DropDownList ID="drpItemName" runat="server" CssClass="form-control" TabIndex="11" AutoPostBack="true"
                                       OnSelectedIndexChanged="drpItemName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvdrpItemName" runat="server" ControlToValidate="drpItemName"
                                        Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="sub"
                                        class="classValidation" ErrorMessage="Select Item Name."></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="control-label">UnitName<span class="required-field">*</span></label>
                                        <asp:DropDownList ID="drpUnitName" runat="server" CssClass="form-control" TabIndex="12" >
                                        </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvUnitName" runat="server" ControlToValidate="drpUnitName"
                                        Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="sub"
                                        class="classValidation" ErrorMessage="Select Unit Name."></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-sm-1">
                                      <label class="control-label">Qty.<span class="required-field">*</span></label>
                                      <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" TabIndex="13" 
                                        Style="text-align: right;" MaxLength="5"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvQty" runat="server" ControlToValidate="txtQty"
                                        Display="Dynamic" SetFocusOnError="true" ValidationGroup="sub" CssClass="classValidation"
                                        ErrorMessage="Enter Qty "></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-sm-1">
                                      <label class="control-label">Weight<span class="required-field">*</span></label>
                                      <asp:TextBox ID="txtWeight" runat="server" CssClass="form-control"  TabIndex="14"
                                        Style="text-align: right;" MaxLength="7"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvWeight" runat="server" ControlToValidate="txtWeight"
                                        Display="Dynamic" SetFocusOnError="true" ValidationGroup="sub" CssClass="classValidation"
                                        ErrorMessage="Enter Weight."></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-sm-3">
                                      <label class="control-label">Remark</label>
                                       <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TabIndex="15" ></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
			                                <div class="col-sm-6">
		                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="16"  class="btn full_width_btn btn-sm btn-primary subnew"
                                                            ValidationGroup="sub"  OnClick="btnSubmit_Click" />
		                                  </div>
		                                  <div class="col-sm-6">
		                                    <asp:Button ID="btnNew" runat="server" Text="New" class="btn full_width_btn btn-sm btn-primary subnew" 
                                                TabIndex="17" OnClick="btnNew_Click" />
                                            <asp:HiddenField ID="hidrowid" runat="server" />
		                                  </div>
			                              </div>
                                  </div>
                                 
                                </div>
                        
                                 <div class="dataTables_add_entry" style="overflow-x:auto;">
                                   <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"
                                        Width="100%" GridLines="None" EnableViewState="true" AllowPaging="true" BorderWidth="0"
                                        ShowFooter="true" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand"
                                        OnRowDataBound="grdMain_RowDataBound" PageSize="30">
                                        <RowStyle CssClass="odd" />
                                        <AlternatingRowStyle CssClass="even" />    
                                        <Columns>
                                         <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">                                          
                                            <ItemTemplate>
                                                  <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("id") %>' CommandName="cmdedit" TabIndex="5" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                     <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("id") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" TabIndex="6" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>                                          
                                                </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="Center">
                                         
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item" HeaderStyle-Width="120" HeaderStyle-HorizontalAlign="left">
                                        
                                            <ItemTemplate>
                                                <asp:Label ID="lblitemname" runat="server" Text='<%#Eval("ITEM_NAME")%>'></asp:Label>
                                                <asp:HiddenField ID="hditemid" runat="server" Value='<%#Eval("ITEM_ID")%>' />
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="left" />
                                            <FooterTemplate>
                                               <strong> Total</strong>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                         
                                            <ItemTemplate>
                                                <asp:Label ID="lblqty" runat="server" Text='<%#Eval("ITEM_QTY") %>'></asp:Label>
                                            </ItemTemplate>
                                          
                                            <FooterTemplate>
                                                <asp:Label ID="lblqtytotal" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UOM" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lbluomname" runat="server" Text='<%#Eval("UOM_NAME")%>'></asp:Label>
                                                <asp:HiddenField ID="hiduomid" runat="server" Value='<%#Eval("UOM_IDNO")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Weight" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                          
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("ITEM_WEIGHT")) == "0" ? "0" : Convert.ToString(Eval("ITEM_WEIGHT"))%>
                                            </ItemTemplate>
                                         
                                            <FooterTemplate>
                                                <asp:Label ID="lblWeighttotal" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remark" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                        
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("ITEM_REMARK"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    </asp:GridView>
                                   
                                    </div>
                              
                                </div>
	                        </section>
	                       <!-- fourth row -->
	                        <div class="clearfix fourth_right">
										<section class="panel panel-in-default btns_without_border">                            
											<div class="panel-body">     
												<div class="clearfix odd_row">
													<div class="col-lg-2"></div>
													<div class="col-lg-8">
														<div class="col-sm-4">                                                         
                                                        <asp:LinkButton ID="lnkbtnNew" runat="server" TabIndex="20" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-info" OnClick="lnkbtnNew_OnClick" ><i class="fa fa-file-o"></i>New</asp:LinkButton>                                                            	
                                                        
														</div>                                  
														<div class="col-sm-4">
                                                        <asp:LinkButton ID="lnkbtnSave" runat="server" TabIndex="18" CausesValidation="true" ValidationGroup="save" CssClass="btn full_width_btn btn-s-md btn-success" OnClick="lnkbtnSave_OnClick" ><i class="fa fa-save"></i>Save</asp:LinkButton>                      
														</div>
														<div class="col-sm-4">
                                                        <asp:LinkButton ID="lnkbtnCancel" runat="server" TabIndex="19" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-danger" OnClick="lnkbtnCancel_OnClick" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
														</div>
													</div>
													<div class="col-lg-2">
                                                      <asp:HiddenField ID="hiduomid" runat="server" Value="0" />
                                                      <asp:HiddenField ID="hidgoodsreceivedid" runat="server" Value="0" />
                                                      <asp:HiddenField ID="hidmindate" runat="server" Value="" />
                                                      <asp:HiddenField ID="hidmaxdate" runat="server" Value="" />
                                                      </div>
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
            </div>
            <div id="print" style="font-size: 13px; display: none;">
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
                                    <asp:Label ID="lblPrintHeadng" runat="server" Text="Receipt for Goods Received"></asp:Label></strong></h3>
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
                                        <asp:Label ID="lbltxtagent" runat="server" Text="Agent"></asp:Label>
                                    </td>
                                    <td id="TdlblAgent" runat="server">
                                        :
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <b>
                                            <asp:Label ID="lblAgent" runat="server"></asp:Label></b>
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
                                        <asp:Label ID="lbltxtreceivername" Text="Receiver Name" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <b>
                                            <asp:Label ID="lblRecvrName" runat="server"></asp:Label></b>
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
                                            <td style="font-size: 12px" width="20%">
                                                <strong>Item Name</strong>
                                            </td>
                                            <td style="font-size: 12px" width="10%">
                                                <strong>Unit Name</strong>
                                            </td>
                                            <td style="font-size: 12px" width="10%">
                                                <strong>Quantity</strong>
                                            </td>
                                            <td style="font-size: 12px" width="10%">
                                                <strong>Weight</strong>
                                            </td>
                                            <td style="font-size: 12px" width="20%">
                                                <strong>Remark</strong>
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="white_bg" width="10%">
                                                <%#Container.ItemIndex+1 %>.
                                            </td>
                                            <td class="white_bg" width="30%">
                                                <%#Eval("Item_Modl")%>
                                            </td>
                                            <td class="white_bg" width="15%">
                                                <%#Eval("UOM_Name")%>
                                            </td>
                                            <td class="white_bg" width="15%">
                                                <%#Eval("Qty")%>
                                            </td>
                                            <td class="white_bg" width="15%">
                                                <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Tot_Weght")))%>
                                            </td>
                                            <td class="white_bg" width="15%" align="right">
                                                <%#(Eval("Remark"))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                    <td class="white_bg" width="15%" align="center">
                                        <asp:Label ID="lblttl" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="10%">
                                    </td>
                                    <td class="white_bg" width="15%" align="left">
                                        <asp:Label ID="lbltotalqty" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="12.5%">
                                        <asp:Label ID="lbltotalWeight" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="12.5%">
                                    </td>
                                    <td class="white_bg" width="12.5%" align="center">
                                        <asp:Label ID="lblTotalAmnt" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="12.5%">
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
        </ContentTemplate>
    </asp:UpdatePanel>
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

        function cityviaddl() {
            var id = document.getElementById("<%=drpToCity.ClientID %>").value;
            document.getElementById("<%=drpCityVia.ClientID %>").value = id;

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