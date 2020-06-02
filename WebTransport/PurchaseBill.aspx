<%@ Page Language="C#" Title="Purchase bill" AutoEventWireup="true" MasterPageFile="~/Site1.Master"
    CodeBehind="PurchaseBill.aspx.cs" Inherits="WebTransport.PurchaseBill" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-10">
            <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">PURCHASE BILL
                  <span class="view_print"><a href="ManagePurchaseBill.aspx" tabindex="31"><asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a>
                  &nbsp;
                   <asp:LinkButton ID="lnkbtnPrint" runat="server" ToolTip="Click to print" Visible="false" OnClientClick ="return CallPrint('print');"><i class="fa fa-print icon"></i></asp:LinkButton>
                  
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
                              <label class="col-sm-4 control-label" style="width: 30%;">Date Range<span class="required-field">*</span></label>
                              <div class="col-sm-8" style="width: 70%;">
                               <asp:DropDownList ID="ddlDateRange" runat="server" TabIndex="1" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged">
                                 </asp:DropDownList>     
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                                    ControlToValidate="ddlDateRange" ValidationGroup="save" ErrorMessage="Please select date range!"
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>   
                              </div>
                            </div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 21%;">Bill Date</label>
                              <div class="col-sm-4" style="width: 27%;">
                               <asp:TextBox ID="txtBillDate" runat="server" CssClass="input-sm datepicker form-control" MaxLength="6"  TabIndex="2" onchange="Focus()" data-date-format="dd-mm-yyyy"></asp:TextBox>                                                                       
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                    ControlToValidate="txtBillDate" ValidationGroup="save" ErrorMessage="Please select bill date!" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                                 </div>
                              <div class="col-sm-3" style="width: 27%;">
                               <asp:TextBox ID="txtPrefixNo" PlaceHolder="PrefNo." runat="server" CssClass="form-control" Style="text-align: right;"  TabIndex="3"  Enabled="true" MaxLength="15"></asp:TextBox>
                              </div>
                              <div class="col-sm-2" style="width: 25%;">
                                <asp:TextBox ID="txtBillNo" Placeholder="BillNo." runat="server" CssClass="form-control" Style="text-align: right;" TabIndex="4" Enabled="true" MaxLength="9" AutoPostBack="true" OnTextChanged="txtBillNo_OnTextChanged"></asp:TextBox>                           
                              </div>
                              <div class="col-sm-3"></div>
                              <div class="col-sm-8">
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                        ControlToValidate="txtBillNo" ValidationGroup="save" ErrorMessage="Please enter bill number!"
                                        SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                           	</div>
                           	<div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 31%;">Loc.[From]<span class="required-field">*</span></label>
							     <div class="col-sm-4" style="width: 59%;">
                                  <asp:DropDownList ID="ddlFromCity" TabIndex="5" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFromCity_OnSelectedIndexChanged" >
                                </asp:DropDownList>                       
                                <asp:RequiredFieldValidator ID="rfvtxtfromcity" runat="server" Display="Dynamic"
                                    ControlToValidate="ddlFromCity" ValidationGroup="save" ErrorMessage="Please select location!"
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              
                              </div>
                              <div class="col-sm-2" style="width: 10%;">
                                <asp:LinkButton ID="lnkbtnDriverRefresh" runat="server" TabIndex="6" CssClass="btn-sm btn btn-primary acc_home" ToolTip="Update From City" OnClick="lnkbtnDriverRefresh_OnClick"><i class="fa fa-refresh"></i></asp:LinkButton>                              
                              </div>
                            </div>
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-4">
                              <label class="col-sm-4 control-label" style="width: 30%;">Pur. Type</label>
                              <div class="col-sm-8" style="width: 70%;">
                                <asp:DropDownList ID="ddlPurchaseType" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlPurchaseType_SelectedIndexChanged" TabIndex="7">
                                    <asp:ListItem Text="Taxable" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Vat Purchase" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Fuel" Value="3"></asp:ListItem>
                                </asp:DropDownList>       
                              </div>
                            </div>
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 21%;">Bill Type</label>
					            <div class="col-sm-8" style="width: 79%;">
                               
					            <div class="radio" style="display:inline;padding-top: 4px;">
						            <label class="radio-custom">
                                     &nbsp;<asp:RadioButton ID="rdoCredit" TabIndex="8" runat="server" GroupName="Against" /> Credit 
						            </label>
					            </div>
					            <div class="radio" style="display:inline;padding-top: 4px;padding-left:34px">
						            <label class="radio-custom">
                                     <asp:RadioButton ID="rdoCash" runat="server" Checked="true" GroupName="Against" CssClass="by_receipt" TabIndex="9" />Cash
						            </label>
					            </div> 
					            </div>
                            </div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 31%;">Party Name<span class="required-field">*</span></label>
                           		<div class="col-sm-8" style="width: 59%;">
                                 <asp:DropDownList ID="ddlSender" runat="server" CssClass="form-control" TabIndex="10" >
                                </asp:DropDownList>                                                                           
                                <asp:RequiredFieldValidator ID="rfvddlSender" TabIndex="9" runat="server" Display="Dynamic" ControlToValidate="ddlSender"
                                    ValidationGroup="save" ErrorMessage="Please select Party's Name!" InitialValue="0"
                                    SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                            
                              </div>
                              <div class="col-sm-1" style="width: 10%;">
                                <asp:LinkButton ID="lnkbtnRefreshParty" TabIndex="11" runat="server" CssClass="btn-sm btn btn-primary acc_home" ToolTip="Update Sender" OnClick="lnkbtnRefreshParty_OnClick"><i class="fa fa-refresh"></i></asp:LinkButton>  
                               
                              </div>
                           	</div>
                           	
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-4">
                              <label class="col-sm-4 control-label" style="width: 30%;">Lorry No.<span class="required-field">*</span></label>
                              <div class="col-sm-8" style="width: 70%;">
                               <asp:DropDownList ID="ddlLorry" runat="server" CssClass="form-control" TabIndex="12" >
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rvfLorry" runat="server" Display="Dynamic" ControlToValidate="ddlLorry"
                                    ValidationGroup="save" ErrorMessage="Please selectLorry!" InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 21%;">Remark</label>
                              <div class="col-sm-9" style="width: 79%;">
                               <asp:TextBox ID="TxtRemark" PlaceHolder="Enter Remark" runat="server" Style="resize: none" CssClass="form-control" TextMode="MultiLine" MaxLength="200" TabIndex="13"></asp:TextBox>
                              </div>
                           	</div>
                           	<div class="col-sm-2">
                         <asp:LinkButton ID="lnkExcelPop"  runat="server" tooltip="Please Browse file Format (BillNo,Bill Date,Party,Lorry,Item,Qty,Rate,Remark)" TabIndex="15" CssClass="btn full_width_btn btn-sm btn-primary"  data-toggle="modal" data-target="#acc_posting"><i class="fa fa-upload"></i>Import Excel</asp:LinkButton>
                            </div>
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
                              <label class="control-label">Item Name<span class="required-field">*</span></label>
                              <div>
                                <asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control"  TabIndex="16" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPartno" runat="server" ControlToValidate="ddlItemName" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"  ErrorMessage="Select Item Name!" CssClass="classValidation"></asp:RequiredFieldValidator>                
                              </div>
                            </div>
                            <div class="col-sm-2" style="width:10%">
                             	  <label class="control-label">Tyre Size<span class="required-field"></span></label>
                               <div>
                               <asp:DropDownList ID="ddltyresize" runat="server" CssClass="form-control" Enabled="false">
                                </asp:DropDownList>
                               
                               </div>
                            </div>
                           	<div class="col-sm-1" style="width:10%">
                              <label class="control-label">Unit<span class="required-field">*</span></label>
                              <div>
                                <asp:DropDownList ID="ddlunitname" runat="server" CssClass="form-control" TabIndex="17">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvAmnt" runat="server" ControlToValidate="ddlunitname"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"  ErrorMessage="Choose Unit Name!" CssClass="classValidation"></asp:RequiredFieldValidator>

                              </div>
                            </div>
                            <div class="col-sm-1" style="width:10%; display:none">
                              <label class="control-label">Rate Type<span class="required-field">*</span></label>
                              <div>

                                 <asp:DropDownList ID="ddlRateType" runat="server" CssClass="form-control"  TabIndex="18" AutoPostBack="false" onchange="javascript:OnchangeRatetype();">
                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Rate" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Weight" Value="2"></asp:ListItem>
                                </asp:DropDownList> 
                           <%--     <asp:RequiredFieldValidator ID="rfvDdlRate" runat="server" ControlToValidate="ddlRateType" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"
                                    ErrorMessage="Choose Rate Type!" CssClass="classValidation"> </asp:RequiredFieldValidator>--%>
                              </div>
                            </div>
                            <div class="col-sm-1">
                              <label class="control-label">Qty.<span class="required-field">*</span></label>
                              <div>
                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"  MaxLength="6"  Style="text-align: right;" TabIndex="19" AutoPostBack="false" onKeyPress="return checkfloat(event, this);"
                               oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false">0.00</asp:TextBox>
                              </div>
                            </div>
                            <div class="col-sm-1" style="display:none">
                              <label class="control-label">Weight<span class="required-field">*</span></label>
                              <div>
                                <asp:TextBox ID="txtweight" runat="server" CssClass="form-control" style="text-align:right;" MaxLength="30" TabIndex="20"  onKeyPress="return checkfloat(event, this);" oncopy="return false" onpaste="return false" oncut="return false" Text="0.00" oncontextmenu="return false"></asp:TextBox>
                              </div>
                            </div>
                           	<div class="col-sm-1">
                              <label class="control-label">Rate<span class="required-field">*</span></label>
                              <div>
                              <asp:TextBox ID="txtrate" runat="server" Text="0.00" CssClass="form-control" MaxLength="30" TabIndex="21" onKeyPress="return checkfloat(event, this);" oncopy="return false"
                              style="text-align:right;"  onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>                       
                            <asp:RequiredFieldValidator ID="rfvtxtrate" runat="server" ControlToValidate="txtrate"  InitialValue="" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Submit"  ErrorMessage="Enter Rate!" CssClass="classValidation"></asp:RequiredFieldValidator>
                          <asp:CompareValidator ID="CompareValidator1" runat="server" ValueToCompare="0" ControlToValidate="txtrate"  CssClass="classValidation"
                                ErrorMessage="Rate Greater Than 0!" Operator="GreaterThan" Type="Double" ValidationGroup="Submit"></asp:CompareValidator>
                    
                              </div>
                            </div>
                            <div class="col-sm-1">
                              <label class="control-label">Per</label>
                              <div>
                                <asp:DropDownList ID="ddlDivPer" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="%" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Amnt" Value="2"></asp:ListItem>
                                </asp:DropDownList> 
                              </div>
                            </div>
                            <div class="col-sm-1" style="width:10%">
                              <label class="control-label">Disc.</label>
                              <div>
                                  <asp:TextBox ID="txtDivDiscAmnt" runat="server" Text="0.00" CssClass="form-control" MaxLength="30" onKeyPress="return checkfloat(event, this);" oncopy="return false"
                                  style="text-align:right;"  onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>                       
                              </div>
                            </div>
                            <div class="col-sm-1" style="width:10%">
                              <label class="control-label">Oth. Amnt.</label>
                              <div>
                                  <asp:TextBox ID="txtDivOtherAmnt" runat="server" Text="0.00" CssClass="form-control" MaxLength="30" TabIndex="21" onKeyPress="return checkfloat(event, this);" oncopy="return false"
                                  style="text-align:right;"  onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>                       
                              </div>
                            </div>
                            <div class="col-sm-1">
                            <asp:LinkButton ID="lnkbtnSubmitClick" runat="server" OnClick="lnkbtnSubmitClick_OnClick" TabIndex="22" style="padding-bottom:5px;" CssClass="btn full_width_btn btn-sm btn-primary subnew" CausesValidation="true" ValidationGroup="Submit" >Submit</asp:LinkButton>                           
                            </div>
                            <div class="col-sm-1" style="width:10%">
                            <asp:LinkButton ID="lnkbtnNewClick" runat="server" OnClick="lnkbtnNewClick_OnClick" style="padding-bottom:5px;" TabIndex="23" CssClass="btn full_width_btn btn-sm btn-primary subnew" CausesValidation="false"  >New</asp:LinkButton>
                                <asp:HiddenField ID="HidTaxRate" runat="server" />
                                <asp:HiddenField ID="HidTax" runat="server" />
                                 <asp:HiddenField ID="hdnAmount" runat="server" />
                                <asp:HiddenField ID="hdnSGSTPer" runat="server" />
                                <asp:HiddenField ID="hdnCGSTPer" runat="server" />
                                <asp:HiddenField ID="hdnIGSTPer" runat="server" />
                                <asp:HiddenField ID="hdnSGSTAmt" runat="server" />
                                <asp:HiddenField ID="hdnCGSTAmt" runat="server" />
                                <asp:HiddenField ID="hdnIGSTAmt" runat="server" />
                                <asp:HiddenField ID="hdnGSTFlag" runat="server" />
                                <asp:HiddenField ID="hdnStateID" runat="server" />
                                <asp:HiddenField ID="hidDiscValue" runat="server" />
                                </div>
                          </div>  
                        </div>
                      </section>                        
                    </div>

                    <div class="clearfix third_right">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body" style="overflow-x:auto;">     
                         <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"
                                Width="100%" GridLines="None" AllowPaging="false"  BorderWidth="0"  ShowFooter="true" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand"
                                OnRowDataBound="grdMain_RowDataBound" OnRowCreated="grdMain_RowCreated">
                                 <RowStyle CssClass="odd" />
                                <AlternatingRowStyle CssClass="even" />     
                                <Columns>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="400px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                         <asp:LinkButton ID="lnkbtEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("id") %>' CommandName="cmdedit" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>

                                         <asp:LinkButton ID="lnkbtnDelete" CssClass="edit" runat="server" CommandArgument='<%#Eval("id") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" TabIndex="6" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                  
                                            <asp:ImageButton ID="imgstck" runat="server" CommandArgument='<%#Eval("Item_Idno") %>'
                                                CommandName="cmdstck" ImageUrl="~/Images/greenbutton.jpg" Visible="false" ToolTip="Enter Tyre Details" />
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
                                            <asp:Label ID="lblItemName" runat="server" Text='<%#Eval("Item_Name")%>'></asp:Label>
                                            <asp:HiddenField ID="HidItem_Type" Value='<%#Eval("Item_type") %>' runat="server" />
                                            <asp:HiddenField ID="HidItem_Idno" Value='<%#Eval("Item_Idno") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Tyre Size" >
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTyreSize" runat="server" Text='<%#Eval("Tyresize")%>'></asp:Label>
                                            <asp:HiddenField ID="HidTyreSize_Idno" Value='<%#Eval("Tyresize_Idno") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Unit Name" Visible="false">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                            <%#Eval("Unit_Name")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Rate Type" Visible="false">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                            <%#Eval("Rate_Type")%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Quantity">
                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblQty" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Quantity")))%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblQuantity" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Weight" Visible="false">
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
                                      <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" 
                                            HeaderText="SGST">
                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("SGST_Amt")))%>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <strong> <asp:Label ID="lblSGSTAmt" runat="server"></asp:Label></strong>
                                            </FooterTemplate>
                                        </asp:TemplateField> 
                                         <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" 
                                            HeaderText="CGST">
                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("CGST_Amt")))%>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <strong> <asp:Label ID="lblCGSTAmt" runat="server"></asp:Label></strong>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" 
                                            HeaderText="IGST">
                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("IGST_Amt")))%>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <strong> <asp:Label ID="lblIGSTAmt" runat="server"></asp:Label></strong>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="100" HeaderText="VAT" Visible="false" >
                                        <ItemStyle HorizontalAlign="Right" Width="100"  />
                                        <ItemTemplate>
                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Vat")))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblVAT" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="100" HeaderText="ADD VAT" Visible="false">
                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                        <ItemTemplate>
                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Addit_Vat")))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblAddVAT" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="30" HeaderText="Disc.">
                                        <ItemStyle HorizontalAlign="Right" Width="30" />
                                        <ItemTemplate>
                                            <%# String.Format("{0:0,0.00}", Convert.ToDouble(Eval("DivDiscValue") == "" ? 0 : Convert.ToDouble(Eval("DivDiscValue")))) + " " + (Convert.ToString(Eval("strDivDiscType")) == "2" ? "Amnt" : "%") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="50" HeaderText="Dis. Amnt">
                                        <ItemStyle HorizontalAlign="Right" Width="50" />
                                        <ItemTemplate>
                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("DivDiscAmnt") == "" ? 0 : Convert.ToDouble(Eval("DivDiscAmnt"))))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblDivDiscAmnt" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="50" HeaderText="Oth. Amnt">
                                        <ItemStyle HorizontalAlign="Right" Width="50" />
                                        <ItemTemplate>
                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("DivDiscOthAmnt") == "" ? 0 : Convert.ToDouble(Eval("DivDiscOthAmnt"))))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblDivDiscOthAmnt" runat="server"></asp:Label>
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
                                            <asp:Label ID="lblGridTtlAmount" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>

                        </div>
                      </section>
                    </div> 

                    <div class="clearfix">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body">     
                          <div class="clearfix odd_row">
                            <div class="col-sm-3"></div>
                           	<div class="col-sm-3"></div>
                            <div class="col-sm-3"></div>
                           	<div class="col-sm-3">
                              <label class="col-sm-6 control-label">Total Amount</label>
                              <div class="col-sm-6">
                               <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="form-control" MaxLength="7"  Enabled="false" Text="0.00" Style="text-align: right;"
                                onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"
                                onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                              </div>
                            </div>			                              
                          </div>
                           <div class="clearfix even_row">
                            <div class="col-sm-3"></div>
                           	<div class="col-sm-3"></div>
                            <div class="col-sm-3"></div>
                           	<div class="col-sm-3">
                              <label class="col-sm-6 control-label">Other Charges</label>
                              <div class="col-sm-6">
                                   <asp:TextBox ID="txtOtherCharges" runat="server" onchange="javascript:OnchangeFooterCalc();" CssClass="form-control" MaxLength="7"  Text="0.00" Style="text-align: right;"
                                    onpaste="return false" oncut="return false" oncopy="return false" onKeyPress="return checkfloat(event, this);"
                                    TabIndex="27"></asp:TextBox>

                              </div>
                            </div>			                              
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-3"></div>
                           	<div class="col-sm-2"></div>
                            <div class="col-sm-4">
                              <label class="col-sm-4 control-label">Discount Type</label>
                              <div class="col-sm-4">
                                  <asp:DropDownList ID="ddlDiscountType" runat="server" onchange="javascript:OnchangeFooterCalc();" CssClass="form-control" TabIndex="24"  AutoPostBack="false">
                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Amount" Selected="True" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="%" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                              </div>
                              <div class="col-sm-4">
                                  <asp:TextBox ID="txtDiscountPer" onchange="javascript:OnchangeFooterCalc();" runat="server" CssClass="form-control" MaxLength="7" AutoPostBack="false"
                                    Text="0.00" Style="text-align: right;" onpaste="return false" oncut="return false"
                                    oncopy="return false" onKeyPress="return checkfloat(event, this);" TabIndex="25"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                    ControlToValidate="ddlDiscountType" ValidationGroup="save" ErrorMessage="Please select discount type!"
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                           	<div class="col-sm-3">
                              <label class="col-sm-6 control-label">Discount</label>
                              <div class="col-sm-6">
                                <asp:TextBox ID="txtDiscount" ReadOnly="true" runat="server" CssClass="form-control" MaxLength="7"  AutoPostBack="false"
                                Text="0.00" Style="text-align: right;" onpaste="return false" oncut="return false"
                                oncopy="return false" onKeyPress="return checkfloat(event, this);" TabIndex="26"></asp:TextBox>
                              </div>
                            </div>			                              
                          </div>
                         
                          <div class="clearfix even_row">
                            <div class="col-sm-3"></div>
                           	<div class="col-sm-3"></div>
                            <div class="col-sm-3"></div>
                           	<div class="col-sm-3">
                              <label class="col-sm-6 control-label">Rounded Off</label>
                              <div class="col-sm-6">                              
                                <asp:TextBox ID="txtRoundOff" runat="server" CssClass="form-control" MaxLength="7" ReadOnly="true" Text="0.00" Style="text-align: right;"
                            onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"
                            onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                              </div>
                            </div>			                              
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-3"></div>
                           	<div class="col-sm-3"></div>
                            <div class="col-sm-3"></div>
                           	<div class="col-sm-3">
                              <label class="col-sm-6 control-label">Net Amount</label>
                              <div class="col-sm-6">
                                <asp:TextBox ID="txtNetAmnt" runat="server" CssClass="form-control" MaxLength="7" ReadOnly="true" Text="0.00" Style="text-align: right;"
                                onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false" onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                        
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
                          <div class="col-lg-12">
                          <asp:Label ID="lblmessage" runat="server" Font-Bold="true" Visible="false" CssClass="classValidation"
                                            Text=""></asp:Label>
                          </div>

						    <div class="col-lg-2"></div>
						    <div class="col-lg-8">  
                             <div class="col-sm-4">
                              <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false" Visible="false" TabIndex="30" CssClass="btn full_width_btn btn-s-md btn-info" OnClick="lnkbtnNew_OnClick" ><i class="fa fa-file-o"></i>New</asp:LinkButton>  
                             </div>                               
							    <div class="col-sm-4">
                                   <asp:HiddenField ID="HidPBID" runat="server" />
                                    <asp:HiddenField ID="hidmindate" runat="server" />
                                    <asp:HiddenField ID="hidmaxdate" runat="server" />
                                    <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" ValidationGroup="save" CssClass="btn full_width_btn btn-s-md btn-success"  TabIndex="28" OnClick="lnkbtnSave_OnClick" ><i class="fa fa-save"></i>Save</asp:LinkButton> 
								   
							    </div>
							    <div class="col-sm-4">
								  <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" TabIndex="29" CssClass="btn full_width_btn btn-s-md btn-danger" OnClick="lnkbtnCancel_OnClick" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
							    </div>
						    </div>
						    <div class="col-lg-2"></div>
					    </div> 
                    </div>
                      </section>
                    </div>

                    <!-- popup form Iteam Details -->
					<div id="dvStck" class="modal fade">
										  <div class="modal-dialog">
										    <div class="modal-content">
										      <div class="modal-header">
										        <h4 class="popform_header">Item Details </h4>
										      </div>
										      <div class="modal-body">
										        <section class="panel panel-default full_form_container material_search_pop_form">
										          <div class="panel-body">
                                                     <asp:Repeater ID="rptstck" runat="server" OnItemDataBound="rptstck_ItemDataBound">
                                                        <HeaderTemplate>                       
										          	        <div class="clearfix odd_row">
												      		        <div class="col-md-4">Serial No <span class="required-field">*</span></div>
																	<div class="col-md-3" style="display:none">Company <span class="required-field">*</span></div>
																	<div class="col-md-4" >Type <span class="required-field">*</span></div>
																	<div class="col-md-3" style="display:none">Purc. From <span class="required-field">*</span></div>
																</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
																<div class="clearfix even_row">
												      		<div class="col-md-4">
                                                             <asp:TextBox ID="txtserialNo" runat="server" Text='<%# Eval("SerialNo") %>' CssClass="form-control" TabIndex="100" MaxLength="20" placeholder="Enter Serial No"></asp:TextBox>
                                                            <asp:HiddenField ID="hidItemIdno" Value='<%# Eval("ItemIdno") %>' runat="server" />
                                                           <asp:HiddenField ID="Hidtyresizeidno" Value='<%# Eval("tyresizeid") %>' runat="server" />
                                                            <asp:HiddenField ID="hidSerialIdno" Value='<%# Eval("SerlDetl_id") %>' runat="server" />
												      		</div>
																	<div class="col-md-3" style="display:none">
                                                                     <asp:TextBox ID="txtCompanys" runat="server" Text='<%# Eval("CompName") %>' CssClass="form-control"  MaxLength="20" TabIndex="101" placeholder="Company Name"></asp:TextBox>
																	</div>
																	<div class="col-md-4" style="">
																		<asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" TabIndex="102">
                                                                            <asp:ListItem Value="1" Text="New"></asp:ListItem>
                                                                            <asp:ListItem Value="2" Text="Old"></asp:ListItem>
                                                                            <asp:ListItem Value="3" Text="Retrited"></asp:ListItem>
                                                                        </asp:DropDownList>
																	</div>
																	<div class="col-md-3" style="display:none">
                                                                     <asp:TextBox ID="txtPurParty" runat="server" Text='<%# Eval("PurFrom") %>' CssClass="form-control" MaxLength="20" TabIndex="103" placeholder="Pur. Party"></asp:TextBox>
																	
																	</div>
																</div>
														 </ItemTemplate>
                                                        <FooterTemplate>
                                                             &nbsp;
                                                        </FooterTemplate>
                                                    </asp:Repeater>		
																
										
										
									</div>
								</section>
								</div>
								<div class="modal-footer">
								<div class="popup_footer_btn">   
                                   <div class="col-lg-3">      
                                   </div>
                                      <div class="col-lg-3" style="width:21%"> 
                                       <asp:HiddenField ID="hiddenitemid" runat="server" />
                                        <asp:LinkButton ID="lnkbtnStockSubmit" runat="server"  CssClass="btn full_width_btn btn-s-md btn-success" OnClick="lnkbtnStockSubmit_OnClick"  TabIndex="104"><i class="fa fa-save"></i>Save</asp:LinkButton> 
                                      </div>        
                                      <div class="col-lg-3">
                                      <asp:LinkButton ID="lnkbtnClose" runat="server" CssClass="btn btn-dark"><i class="fa fa-times"></i>Close</asp:LinkButton>
                                      </div>
                                     <div class="col-lg-12">   
                                    <div style="float: left; width: 500px; text-align: left;">
                                        <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                    </div>
								</div>
								</div>
							</div>
							</div>
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
                             <asp:FileUpload ID="FileUpload" runat="server"  tooltip="Please Browse file Format (BillNo,Bill Date,Party,Lorry,Item,Qty,Rate,Remark)" Width="200px" TabIndex="14" />
							</div>
                            
							<div class="col-sm-2">
                            <asp:LinkButton ID="lnkbtnUpload" runat="server" TabIndex="15" CssClass="btn full_width_btn btn-sm btn-primary" OnClick="lnkbtnUpload_OnClick" ><i class="fa fa-upload"></i>Upload</asp:LinkButton>
                          
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
                    <table cellpadding="1" cellspacing="0" width="800" border="1" style="font-family: Arial,Helvetica,sans-serif;">
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
                                        <asp:Label ID="lblPrintHeadng" runat="server" Text="Purchase Bill"></asp:Label></strong></h3>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table border="0" width="100%">
                                    <tr>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;
                                            width: 6%;">
                                            <asp:Label ID="lbltxtgrno" Text="Bill No." runat="server"></asp:Label>
                                        </td>
                                        <td style="font-size: 13px; border-right-style: none; width: 1%;">
                                            :
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;
                                            width: 10%;">
                                            <b>
                                                <asp:Label ID="lblGRno" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;
                                            width: 4%;">
                                            <asp:Label ID="lbltxtgrdate" Text="Bill Date" runat="server"></asp:Label>
                                        </td>
                                        <td style="font-size: 13px; border-right-style: none; width: 1%;">
                                            :
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;
                                            width: 15%;">
                                            <b>
                                                <asp:Label ID="lblGrDate" runat="server"></asp:Label></b>
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
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lbltxttocity" Text="Party Name" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td align="left" colspan="7" class="white_bg" valign="top" style="font-size: 13px;
                                            border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblToCity" runat="server"></asp:Label></b>
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
                                                <td style="font-size: 12px" width="27%">
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
                                                <td style="font-size: 12px" align="left" width="10%">
                                                    <strong>Item Rate</strong>
                                                </td>
                                                <td style="font-size: 12px" align="left" width="5%">
                                                    <strong>VAT</strong>
                                                </td>
                                                <td style="font-size: 12px" align="left" width="10%">
                                                    <strong>ADD VAT</strong>
                                                </td>
                                                <td style="font-size: 12px" align="left" width="8%">
                                                    <strong>Amount</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="white_bg" width="10%">
                                                    <%#Container.ItemIndex+1 %>.
                                                </td>
                                                <td class="white_bg" width="27%">
                                                    <%#Eval("Item_Modl")%>
                                                </td>
                                                <td class="white_bg" width="10%">
                                                    <%#Eval("UOM_Name")%>
                                                </td>
                                                <td align="left" class="white_bg" width="10%">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <%#Eval("Qty")%>
                                                </td>
                                                <td class="white_bg" width="10%" align="right">
                                                    <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Tot_Weght")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                </td>
                                                <td class="white_bg" width="10%" align="right">
                                                    <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Item_Rate")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="white_bg" width="5%" align="right">
                                                    <%#String.Format("{0:0.00}", 0)%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="white_bg" width="10%" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#String.Format("{0:0.00}", 0)%></td>
                                                <td class="white_bg" width="8%" align="right">
                                                    <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                        <td class="white_bg" width="10%">
                                            &nbsp;
                                        </td>
                                        <td class="white_bg" width="21%">
                                            &nbsp;
                                        </td>
                                        <td class="white_bg" width="9%" align="center">
                                            <asp:Label ID="lblttl" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                        <td class="white_bg" width="11%" align="left">
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lbltotalqty" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                        <td class="white_bg" width="10%" align="left">
                                            <asp:Label ID="lbltotalWeight" Font-Bold="true" runat="server"></asp:Label>
                                            &nbsp; &nbsp;
                                        </td>
                                        <td class="white_bg" width="10%">
                                            &nbsp;
                                        </td>
                                        <td class="white_bg" width="7%">
                                            &nbsp;
                                        </td>
                                        <td class="white_bg" width="7%">
                                            &nbsp;
                                        </td>
                                        <td class="white_bg" width="15%" align="center">
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            <asp:Label ID="lblTotalAmnt" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" align="left" width="80%">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <p>
                                                            <asp:Label ID="lblremark" runat="server" valign="right"></asp:Label></p>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2" width="20%">
                                            <table>
                                                <tr id="trOtherchrgDiscount" runat="server">
                                                    <td>
                                                        <asp:Label ID="lblbilty" runat="server" Text="Discount" Font-Size="13px" valign="right"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align="right">
                                                        &nbsp;
                                                        <asp:Label ID="lblDiscountValue" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="trOtherchrg" runat="server">
                                                    <td>
                                                        <asp:Label ID="lblcartage" runat="server" Text="Other Charges" Font-Size="13px" valign="right"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align="right">
                                                        &nbsp;
                                                        <asp:Label ID="lblOtherChargesValue" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblTollTax" runat="server" Text="Rounded off" Font-Size="13px" valign="right"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align="right">
                                                        &nbsp;
                                                        <asp:Label ID="lblRoundoffValue" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblservtaxConsigner" runat="server" Text="Net Amount" Font-Size="13px"
                                                            valign="right"></asp:Label>
                                                    </td>
                                                    <td id="ctax" runat="server">
                                                        :
                                                    </td>
                                                    <td align="right">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="lblNetAmountValue" runat="server" Font-Size="13px" valign="right"></asp:Label>
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

        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $("#<%=txtBillDate.ClientID %>").datepicker({
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
    <script language="javascript" type="text/javascript">
        function OnchangeRatetype() {
            if (document.getElementById("<%=ddlRateType.ClientID%>").value == "1") {
                document.getElementById("<%=txtQuantity.ClientID%>").disabled = false;
                document.getElementById("<%=txtweight.ClientID%>").disabled = true;
                document.getElementById("<%=txtQuantity.ClientID%>").value = 1;
                document.getElementById("<%=txtweight.ClientID%>").value = 0;
            }
            else {
                document.getElementById("<%=txtQuantity.ClientID%>").disabled = true;
                document.getElementById("<%=txtweight.ClientID%>").disabled = false;
                document.getElementById("<%=txtweight.ClientID%>").value = 1;
                document.getElementById("<%=txtQuantity.ClientID%>").value = 0;
            }

        }
    </script>
    <script language="javascript" type="text/javascript">
        function OnchangeFooterCalc() {

            var DiscountType = 0;
            var Discount = TotAmnt = OtherCharge = DiscountAmnt = RoundOff = RoundedValue = NetAmnt = 0;

            DiscountType = document.getElementById("<%=ddlDiscountType.ClientID%>").value;
            if (document.getElementById("<%=txtDiscountPer.ClientID%>").value != "") {
                Discount = document.getElementById("<%=txtDiscountPer.ClientID%>").value;
            }
            else {
                Discount = "0.00";
                document.getElementById("<%=txtDiscountPer.ClientID%>").value = "0.00";
            }
            TotAmnt = (document.getElementById("<%=txtTotalAmount.ClientID%>").value).replace(/\,/g, '');

            if ((document.getElementById("<%=txtOtherCharges.ClientID%>").value).replace(/\,/g, '') != "") {

                OtherCharge = (document.getElementById("<%=txtOtherCharges.ClientID%>").value).replace(/\,/g, '');
            }
            else {
                OtherCharge = "0.00";
                document.getElementById("<%=txtOtherCharges.ClientID%>").value = "0.00";
            }
            var txtDiscount = document.getElementById("<%=txtDiscountPer.ClientID%>");

            if (DiscountType == 1) {
                if (parseFloat(TotAmnt) <= parseFloat(Discount)) { PassMessageError("Discount Amount Should be lesser then Total Amount!"); txtDiscount.focus(); Discount = "0.00"; }

                document.getElementById("<%=txtDiscount.ClientID%>").value = Discount;
                NetAmnt = (parseFloat(TotAmnt) + parseFloat(OtherCharge)) - parseFloat(Discount);
                RoundedValue = parseFloat(Math.round(NetAmnt)).toFixed(2);
                document.getElementById("<%=txtRoundOff.ClientID%>").value = parseFloat(RoundedValue - NetAmnt).toFixed(2);
                document.getElementById("<%=txtNetAmnt.ClientID%>").value = RoundedValue;
            }
            else if (DiscountType == 2) {

                DiscountAmnt = parseFloat(((parseFloat(TotAmnt) + parseFloat(OtherCharge)) * parseFloat(Discount)) / 100).toFixed(2);

                if (parseFloat(Discount) >= parseFloat(100)) { PassMessageError("Discount % Should be lesser then 100 %!"); txtDiscount.focus(); DiscountAmnt = "0.00"; }
                if (parseFloat(TotAmnt) <= parseFloat(DiscountAmnt)) { PassMessageError("Discount Amount Should be lesser then Total Amount!"); txtDiscount.focus(); DiscountAmnt = "0.00"; }
                
                document.getElementById("<%=txtDiscount.ClientID%>").value = DiscountAmnt;

                NetAmnt = (parseFloat(TotAmnt) + parseFloat(OtherCharge)) - parseFloat(DiscountAmnt);
                RoundedValue = parseFloat(Math.round(NetAmnt)).toFixed(2);
                document.getElementById("<%=txtRoundOff.ClientID%>").value = parseFloat(RoundedValue - NetAmnt).toFixed(2);
                document.getElementById("<%=txtNetAmnt.ClientID%>").value = RoundedValue;

            }
        }
    </script>

    <script language="javascript" type="text/javascript">
       
        function openModal() {
            $('#dvStck').modal('show');
        }

        function HideStck() {
            $("#dvStck").fadeOut(300);
        }
        function ShowStck() {
            $("#dvStck").fadeIn(300);

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
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