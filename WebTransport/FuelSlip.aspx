<%@ Page Title="Fuel Slip" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="FuelSlip.aspx.cs" Inherits="WebTransport.FuelSlip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row ">
        <div class="col-lg-2">
        </div>
        <div class="col-lg-8">
            <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">Fuel Slip
                  <span class="view_print"><a href="ManageFuelSlip.aspx" tabindex="18"><asp:Label ID="lblViewList" runat="server" TabIndex="19" Text="LIST"></asp:Label></a> &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lnkBtnLast" class="view_print"  runat="server"  AlternateText="Print" title="Print" Height="16px" onclick="lnkBtnLast_Click">LAST PRINT</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lnkBtnVouchr" class="view_print"  runat="server"  AlternateText="Print" title="Print" Height="16px" onclick="lnkBtnVouchr_Click">VOUCHERS PRINT</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lnkbtnPrint" runat="server" ToolTip="Click to print" Visible="false" OnClientClick ="return CallPrint('print');"><i class="fa fa-print icon"></i></asp:LinkButton>&nbsp;&nbsp;
                        <asp:LinkButton ID="lnkbtnPrintVoucher" runat="server" ToolTip="Click to Voucher print" Visible="false" OnClientClick ="return CallPrintf('printf');"><i class="fa fa-print icon"></i></asp:LinkButton>&nbsp;&nbsp;
                    </span>
                </header>
                <div class="panel-body">
                  <form class="bs-example form-horizontal">
                  <div class="clearfix second_section">
                      <section class="panel panel-in-default">  
                        <div class="panel-body">                          
                          <div class="clearfix odd_row">
                           <div class="col-sm-4">
                              <label class="control-label">Date Range<span class="required-field">*</span></label>
                              <div>
                               <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control" TabIndex="1">
                            </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDateRange"  Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit" ErrorMessage="Select Date Range!" CssClass="classValidation"> </asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-2" style="width:12%">
                              <label class="control-label">Date<span class="required-field">*</span></label>
                              <div>
                               <asp:TextBox ID="txtDate" runat="server" ontextchanged="txtDate_TextChanged" TabIndex="2"  AutoPostBack="true"  CssClass="input-sm datepicker form-control" MaxLength="10" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                    ControlToValidate="txtDate" ValidationGroup="save" ErrorMessage="Please select date!" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-2" style="width:14%">
                              <label class="control-label">Invoice</label>
                              <div>
                               <asp:TextBox ID="txtInvoiceNo" PlaceHolder="Invoice No." runat="server" CssClass="form-control" MaxLength="10"  TabIndex="3"></asp:TextBox>                                                                       
                              </div>
                            </div>
                             <div class="col-sm-4">
                              <label class="control-label">Location<span class="required-field">*</span></label>
                              <div>
                               <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" 
                                      TabIndex="4">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ControlToValidate="ddlLocation" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit" ErrorMessage="Choose Location!" CssClass="classValidation"> </asp:RequiredFieldValidator>
                              </div>
                            </div>
                            </div>
                            <div class="clearfix odd_row">
                            <div class="col-sm-2" style="width:10%">
                              <label class="control-label">Slip No.</label>
                              <div>
                               <asp:TextBox ID="txtSlipNo" AutoPostBack="true"  Text="0" runat="server"  CssClass="form-control"  
                                      TabIndex="5" ontextchanged="txtSlipNo_TextChanged" ></asp:TextBox>                                                                       
                              </div>
                            </div>
                          
                            <div class="col-sm-4" style="width:25%">
                              <label class="control-label">Lorry<span class="required-field">*</span></label>
                              <div>
                               <asp:DropDownList ID="ddlLorry" runat="server" CssClass="form-control" 
                                      AutoPostBack="true"   TabIndex="6" 
                                      onselectedindexchanged="ddlLorry_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPartno" runat="server" ControlToValidate="ddlLorry"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"  ErrorMessage="Select Lorry!" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            
                            <div class="col-sm-4" style="width:25%">
                              <label class="control-label">Driver<span class="required-field">*</span></label>
                              <div>
                               <asp:DropDownList ID="ddlDriver" runat="server" CssClass="form-control" TabIndex="7">
                            </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlDriver" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit" ErrorMessage="Select Driver!" CssClass="classValidation"> </asp:RequiredFieldValidator>
                              </div>
                            </div>
                             <div class="col-sm-3" style="width:33%">
                              <label class="control-label">Petrol Pump<span class="required-field">*</span></label>
                              <div>
                                <asp:DropDownList ID="ddlPPump" onselectedindexchanged="ddlPPump_SelectedIndexChanged" AutoPostBack="true"  runat="server" CssClass="form-control" TabIndex="8">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlPPump" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit" ErrorMessage="Select Petrol Pump!" CssClass="classValidation"> </asp:RequiredFieldValidator>
                              </div>
                            </div>
                            </div>   
                                                
                        </div>
                      </section>                        
                    </div>
                      <section class="panel panel-in-default">                            
                        <div class="panel-body" style="overflow-x:auto;">     
                       <asp:HiddenField ID="hidFuelPrice" runat="server"></asp:HiddenField>
                          <div class="clearfix even_row">
                              <div class="col-sm-4">	                                
                              <label class="control-label">Item Name<span class="required-field">*</span></label>
                              <div>
                                <asp:DropDownList ID="ddlItemName" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" runat="server" CssClass="form-control"  TabIndex="9" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlItemName" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"  ErrorMessage="Select Item Name!" CssClass="classValidation"></asp:RequiredFieldValidator>                
                              </div> 
                             
                            </div>
                             <div class="col-sm-1" style="width: 12%">
                             	  <label class="control-label">Qty</label>
                               <div>
                               <asp:TextBox ID="txtQty" AutoPostBack="true" runat="server" CssClass="form-control" 
                                       Style="text-align:left;" TabIndex="10" Text="0.00" 
                                       ontextchanged="txtQty_TextChanged"></asp:TextBox>
                              
                               </div>
                             </div>
                            <div class="col-sm-2" style="width: 20%">	                                
                              <label class="control-label">Amount</label><span class="required-field">*</span>
                              <div>
                               <asp:TextBox ID="txtRate" Text="0.00" runat="server" CssClass="form-control" AutoPostBack="true"
                                      Style="text-align: left;" TabIndex="11" ontextchanged="txtRate_TextChanged"></asp:TextBox>
                              </div> 
                            </div>
                            <div class="col-sm-4">
                              <div class="col-sm-6">
                                <asp:LinkButton ID="lnkbtnSubmit" runat="server" CssClass="btn full_width_btn btn-sm btn-primary subnew" TabIndex="12" OnClick="lnkbtnSubmit_OnClick" ValidationGroup="Submit" CausesValidation="true" >Submit</asp:LinkButton>                            
                              </div>
                              <div class="col-sm-6">
                              <asp:LinkButton ID="lnkbtnNewClick" runat="server" CssClass="btn full_width_btn btn-sm btn-primary subnew" TabIndex="13" OnClick="lnkbtnNewClick_OnClick" CausesValidation="false" >New</asp:LinkButton>                             
                              </div>
                              </div>
                              <div class="col-sm-12">
                            </div>
                           </div>
                        </div>
                      </section>
                      <div class="clearfix third_right">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body" style="overflow-x:auto;">     
                            <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" Width="100%" GridLines="None" CssClass="display nowrap dataTable"
                            EnableViewState="true" AllowPaging="true"   BorderWidth="0" ShowFooter="true" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand"
                            OnRowDataBound="grdMain_RowDataBound" PageSize="50" OnRowCreated="grdMain_RowCreated">
                            <RowStyle CssClass="odd" />
                            <AlternatingRowStyle CssClass="even" />   
                                <Columns>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="40" HeaderStyle-CssClass="gridHeaderAlignCenter">
                                       <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                        <ItemStyle HorizontalAlign="Center" Width="40" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("id") %>' CommandName="cmdedit"  ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("id") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Location">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbllocation" runat="server" Text='<%#Eval("Location")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="250" HeaderText="Item Name">
                                        <ItemStyle HorizontalAlign="Left" Width="250" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" runat="server" Text='<%#Eval("ItemName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100" HeaderText="Qty">
                                        <ItemStyle HorizontalAlign="Left" Width="100" />
                                        <ItemTemplate>
                                            <%#Eval("Qty")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="150" HeaderText="Amount">
                                        <ItemStyle HorizontalAlign="Right" Width="150" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRate" runat="server" Text='  <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Rate")))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                <EmptyDataTemplate>
                    <asp:Label ID="LblNoRecordFound" runat="server" CssClass="white_bg" 
                        Text="Record(s) not found."></asp:Label>
                </EmptyDataTemplate>                                                             
                        </asp:GridView>
                          
                        </div>
                      </section>
                    </div> 
                    <div class="clearfix third_right">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body" style="overflow-x:auto;">     
                           
                          <div class="clearfix odd_row">
                          <div class="col-sm-8"></div>
                            <label class="col-sm-2 control-label">Net Amount</label>
                              
                            <div class="col-sm-2">
                               <asp:TextBox ID="txtNetAmnt" Enabled="false" Text="0.00"  runat="server"  CssClass="form-control"  TabIndex="14" ></asp:TextBox>                                                                       
                             
                            </div>
                          
                          
                            </div>
                        </div>
                      </section>
                    </div>
                    <div class="clearfix fourth_right">
                      <section class="panel panel-in-default btns_without_border">                            
                        <div class="panel-body">     
                          <div class="clearfix odd_row">
                            <div class="col-lg-2"></div>
                            <div class="col-lg-9"> 
                             <div class="col-sm-3">  
                              <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false" Visible="false" TabIndex="14" CssClass="btn full_width_btn btn-s-md btn-info" OnClick="lnkbtnNew_OnClick" ><i class="fa fa-file-o"></i>New</asp:LinkButton>  
                             </div>                                                    
                              <div class="col-sm-3">
                               <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" ValidationGroup="Save" TabIndex="12" CssClass="btn full_width_btn btn-s-md btn-success" OnClick="lnkbtnSave_OnClick" ><i class="fa fa-save"></i>Save</asp:LinkButton>
                                <asp:HiddenField ID="hidmindate" runat="server" />
                                <asp:HiddenField ID="hidmaxdate" runat="server" />
                                <asp:HiddenField ID="hidpostingmsg" runat="server" />
                                
                              </div>
                              <div class="col-sm-3">
                               <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" TabIndex="13" CssClass="btn full_width_btn btn-s-md btn-danger" OnClick="lnkbtnCancel_OnClick" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
                               
                              </div>
                              <div class="col-sm-3">
                              <asp:LinkButton ID="lnkExcelPop" Height="33px"  runat="server" tooltip="Please Browse file Format (SlipNo,LorryNo,DriverNo,ItemName,Amount)"  CssClass="btn full_width_btn btn-sm btn-primary"  TabIndex="16" data-toggle="modal" data-target="#acc_posting"><i class="fa fa-upload"></i>Import Excel</asp:LinkButton>
                              </div>
                            </div>
                            <div class="col-lg-1"></div>
                          </div> 
                        </div>
                      </section>
                    </div>
                        <div id="acc_posting" class="modal fade" role="dialog">
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
	                                 <label class="col-sm-3 control-label" style="width: 31%;">From Excel</label>
							            <div class="col-sm-6">
                                         <asp:FileUpload ID="FileUpload" runat="server"  tooltip="Please Browse file Format ()" Width="200px" TabIndex="14" />
							            </div> 
							                <div class="col-sm-2">
                                            <asp:LinkButton ID="lnkbtnUpload" runat="server" TabIndex="15" CssClass="btn full_width_btn btn-sm btn-primary" onclick="lnkbtnUpload_Click"><i class="fa fa-upload"></i>Upload</asp:LinkButton>
                                            </div>
	                                        </div>  
							                    </div>

						                    </section>
                                        </div>
                                        <div class="modal-footer">
                                            <div class="popup_footer_btn">
                                                <asp:LinkButton ID="lnkbtnExport" runat="server" TabIndex="15" CssClass="btn btn-sm btn-primary" onclick="lnkbtnExport_Click" ><i class="fa fa-download"></i>Export Excel</asp:LinkButton>
                                                <button type="submit" class="btn btn-sm btn-primary" data-dismiss="modal">
                                                    <i class="fa fa-times"></i>Close</button>
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
     <table width="100%">
        <tr style="display: none">
            <td class="white_bg" align="center">
                <div id="print" style="font-size: 13px;">
                    <table cellpadding="1" cellspacing="0" width="750" border="1" style="font-family: Arial,Helvetica,sans-serif;">
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
                                <asp:Label ID="lblCompPhNo" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblFaxNo" Text="FAX No.:" runat="server"></asp:Label>
                                <asp:Label ID="lblCompFaxNo" runat="server"></asp:Label><br />
                                <asp:Label ID="lblTin" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                                    ID="lblCompTIN" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="white_bg" valign="middle" colspan="4" style="font-size: 14px;
                                border-left-style: none; border-right-style: none;">
                                <h3>
                                    <strong style="text-decoration: underline">
                                        <asp:Label ID="lblPrintHeadng" runat="server" Text="Fuel Slip"></asp:Label></strong></h3>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table border="0" width="100%">
                                    <tr>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;
                                            width: 6%;">
                                            <asp:Label ID="lbltxtslipno" Text="Slip No."  runat="server"></asp:Label>
                                        </td>
                                        <td style="font-size: 13px; border-right-style: none; width: 1%;">
                                            :
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;
                                            width: 10%;">
                                            <b>
                                                <asp:Label ID="lblSlipno" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;
                                            width: 4%;">
                                            <asp:Label ID="lbltxtslipdate" Text="Slip Date" runat="server"></asp:Label>
                                        </td>
                                        <td style="font-size: 13px; border-right-style: none; width: 1%;">
                                            :
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;
                                            width: 15%;">
                                            <b>
                                                <asp:Label ID="lblSlipDate" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;
                                            width: 1%;">
                                            <asp:Label ID="lbltxtFromcity" Text="Location" runat="server"></asp:Label>
                                        </td>
                                        <td style="font-size: 13px; border-right-style: none; width: 1%;">
                                            :
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;
                                            width: 10%;">
                                            <b>
                                                <asp:Label ID="lblFromCity" runat="server"></asp:Label></b>
                                        </td>
                                    </tr>
                                    <tr>
                                      <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;
                                            width: 6%;">
                                            <asp:Label ID="lbltxtlorry" Text="Lorry Name" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            :
                                        </td>
                                       <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;
                                            width: 6%;">
                                            <b>
                                                <asp:Label ID="lbllorry" runat="server"></asp:Label></b>
                                        </td>
                                         <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;
                                            width: 6%;">
                                            <asp:Label ID="lbltxtDriver" Text="Driver Name" runat="server"></asp:Label>
                                        </td>
                                         <td>
                                            :
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;
                                            width: 6%;">
                                            <b>
                                                <asp:Label ID="lblDriver" runat="server"></asp:Label></b>
                                        </td>
                                         <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;
                                            width: 6%;">
                                            <asp:Label ID="lbltxtpump" Text="Pump Name" runat="server"></asp:Label>
                                        </td>
                                         <td>
                                            :
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;
                                            width: 6%;">
                                            <b>
                                                <asp:Label ID="lblpump" runat="server"></asp:Label></b>
                                        </td>
                                    </tr>
                                    <tr>
                                      <td colspan="9" align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;">
                                            <b> <asp:Label ID="lblInvoiceNo" runat="server"></asp:Label></b>
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
                                                <td style="font-size: 12px" width="10%">
                                                    <strong>Item Name</strong>
                                                </td>
                                                <td style="font-size: 12px" width="10%">
                                                    <strong>Quantity</strong>
                                                </td>
                                                <td style="font-size: 12px" align="right" width="8%">
                                                    <strong>Amount</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="white_bg" width="10%">
                                                    <%#Container.ItemIndex+1 %>.
                                                </td>
                                                <td class="white_bg" width="10%">
                                                    <%#Eval("ITEMNAME")%>
                                                </td>
                                                <td class="white_bg" width="10%">
                                                    <%#Eval("QTY")%>
                                                </td>
                                                <td class="white_bg" width="8%" align="right">
                                                    <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("AMOUNT")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <%--<asp:Label ID="lblTotalAmnt" runat="server"></asp:Label>--%>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table2">
                                    <tr>
                                        
                                        <td class="white_bg" width="22%">
                                            &nbsp;
                                        </td>
                                        <td class="white_bg" width="9%" align="right">
                                            &nbsp;<b>Total&nbsp;&nbsp;&nbsp;</b></td>
                                        <td class="white_bg" width="15%" align="left">
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lbltotalqty" Font-Bold="true" runat="server"></asp:Label>
                                            </td>
                                       
                                        <td class="white_bg" width="15%" align="right">
                                            <asp:Label ID="lblTotalAmnt" Font-Bold="true" runat="server"></asp:Label>
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
            </td>
        </tr>
    </table>


     <table width="100%">
        <tr style="display: none">
            <td class="white_bg" align="center">
                <div id="printf" style="font-size: 13px;">
                    <table cellpadding="1" cellspacing="0" width="750" border="1" style="font-family: Arial,Helvetica,sans-serif; text-align:left;">
                     
                    <%--<table width="50%" border="1" cellspacing="0" cellpadding="0" style="margin:0 auto; text-align:left;">--%>
                         <tr>
                            <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                                border-left-style: none; border-right-style: none">
                                <strong>
                                    <asp:Label ID="lblCompanynamef" runat="server" Style="font-size: 18px;"></asp:Label><br />
                                </strong>
                                <asp:Label ID="lblCompAdd1f" runat="server"></asp:Label>
                                &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblCompAdd2f" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCompCityf" runat="server"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblCompStatef" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblCompCityPinf" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCompPhNof" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblFaxNof" Text="FAX No.:" runat="server"></asp:Label>
                                <asp:Label ID="lblCompFaxNof" runat="server"></asp:Label><br />
                                <asp:Label ID="lblTinf" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                                    ID="lblCompTINf" runat="server"></asp:Label>
                            </td>
                        </tr>
      <%--  <tr>
            <td style="font-weight: bold; text-align: center;">
                <span style="font-size:40px;"><u>Westend Roadlines</u></span><br /><br />
                <span style="font-size:14px;">Godown Area, MITHAPUR - 361 345</span>
            </td>
        </tr>--%>
        <tr>
            <td style="font-weight: bold; font-size: 15px; line-height: 30px;">
                <div style="text-align: right; "><span>Date:</span>
                    <b>
                        <asp:Label ID="lbldate" runat="server"></asp:Label></b>
                </div>
                <div style="text-align: left;"><span>No.</span>
                    <b>
                       <asp:Label ID="lblNo" runat="server"></asp:Label></b>
                </div>

            </td>
        </tr>
        <tr>
            <td style="line-height: 30px;">Pump-Name :
                 <b>
                     <asp:Label ID="lblpummp" runat="server"></asp:Label></b>
            </td>
        </tr>
        <tr>
            <td style="line-height: 30px;">Please Fill The Diesel To Truck No :<u><b>
                                                <asp:Label ID="lblTruckNo" runat="server"></asp:Label></b></u>
               &nbsp With G. C. No.<span><u><b>
                   <asp:Label ID="lblGCNo" runat="server"></asp:Label></b></u></span></td>
        </tr>

       <tr>
            <td style="line-height: 30px;">Amount: <u><b><asp:Label ID="lblAmt" runat="server"></asp:Label></b></u></td>
        </tr>
        <tr>
            <td style="text-align: right; padding-top: 35px;">Signature :<u><span style="color: #fff;">____________________________</span></u></td>
        </tr>
                    
    </table>
                </div>
            </td>
        </tr>
    </table>


    <asp:HiddenField ID="hidfuelIdno" runat="server" />
    <asp:HiddenField ID="hidtotrec" runat="server" />

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

    </script>
     <script language="javascript" type="text/javascript">
        function CallPrintf(strid) {
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

    </script>
     <script type="text/javascript" language="javascript">

         SetFocus();
         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_beginRequest(function () {
             setDatecontrol();
             SetFocus();
         });

         prm.add_endRequest(function () {
             setDatecontrol();
             SetFocus();
         });

         function setDatecontrol() {

             $('#<%=txtDate.ClientID %>').datepicker({
                 buttonImageOnly: false,
                 maxDate: 0,
                 changeMonth: true,
                 changeYear: true,
                 dateFormat: 'dd-mm-yy',
                 minDate: mindate,
                 maxDate: maxdate
             });

         }       
    </script>
    <script language="javascript" type="text/javascript">

        function validate(evt) {
            var theEvent = evt || window.event;
            var key = theEvent.keyCode || theEvent.which;
            key = String.fromCharCode(key);
            var regex = /[0-9]|\./;
            if (!regex.test(key)) {
                theEvent.returnValue = false;
                if (theEvent.preventDefault) theEvent.preventDefault();
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