
<%@ Page Language="C#" Title="Sale Bill" AutoEventWireup="true" MasterPageFile="~/Site1.Master"
    CodeBehind="SaleBill.aspx.cs" Inherits="WebTransport.SaleBill" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-10">
            <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">SALE BILL
                  <span class="view_print"><a href="ManageSaleBill.aspx" ><asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a>
                  &nbsp;
                  <span id="SpanLastPrint" visible="true" runat="server" class="view_print">
            <asp:LinkButton ID="lnkbtnLastPrint" runat="server" 
                ToolTip="Click to Last print" Text="LAST PRINT" 
                onclick="lnkbtnLastPrint_Click"></asp:LinkButton></span>
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
                               <asp:DropDownList ID="ddlDateRange" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="True">
                                 </asp:DropDownList>     
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                                    ControlToValidate="ddlDateRange" ValidationGroup="save" ErrorMessage="Please select date range!"
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>   
                              </div>
                            </div>
                           	<div class="col-sm-4" style="width:30%">
                           		<label class="col-sm-3 control-label" style="width: 26%;">Bill Date</label>
                              <div class="col-sm-4" style="width: 29%;">
                               <asp:TextBox ID="txtBillDate" runat="server" CssClass="input-sm datepicker form-control" MaxLength="6"  onchange="Focus()" data-date-format="dd-mm-yyyy"></asp:TextBox>                                                                       
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" 
                                    ControlToValidate="txtBillDate" ValidationGroup="save" ErrorMessage="Please select bill date!" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                                 </div>
                              <div class="col-sm-3" style="width: 23%;">
                               <asp:TextBox ID="txtPrefixNo" runat="server" PlaceHolder="Pref No" CssClass="form-control" Style="text-align: right;"  Enabled="true" MaxLength="15"></asp:TextBox>
                              </div>
                              <div class="col-sm-2" style="width: 21%;">
                                <asp:TextBox ID="txtBillNo" PlaceHolder="Bill No. " runat="server" CssClass="form-control" Style="text-align: right;" ReadOnly="true"  Enabled="true" MaxLength="9" AutoPostBack="true"></asp:TextBox>
                                                       
                              </div>
                              <div class="col-sm-4">
                              </div>
                              <div class="col-sm-7">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                        ControlToValidate="txtBillNo" ValidationGroup="save" ErrorMessage="Please enter bill number!"
                                        SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>    
                                </div>
                           	</div>
                           	<div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 32%;">Loc.[From]<span class="required-field">*</span></label>
							     <div class="col-sm-4" style="width: 59%;">
                                  <asp:DropDownList ID="ddlFromCity" Width="165px" runat="server" 
                                         CssClass="chzn-select"  AutoPostBack="true" 
                                         OnSelectedIndexChanged="ddlFromCity_SelectedIndexChanged" >
                                </asp:DropDownList>                       
                                <asp:RequiredFieldValidator ID="rfvtxtfromcity" runat="server" Display="Dynamic"
                                    ControlToValidate="ddlFromCity" ValidationGroup="save" ErrorMessage="Please select location!"
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              
                              </div>
                              <div class="col-sm-2" style="width: 9%;">
                                <asp:LinkButton ID="lnkbtnDriverRefresh" runat="server" CssClass="btn-sm btn btn-primary acc_home" ToolTip="Update From City" ><i class="fa fa-refresh"></i></asp:LinkButton>                              
                              </div>
                            </div>
                          </div>
                          <div class="clearfix even_row" >
                            <div class="col-sm-2" style="width:33%">
                              <label class="col-sm-3 control-label" style="width: 31%;">Bill Type</label>
					            <div class="col-sm-8" style="width: 69%;">
					                <div class="radio" style="display:inline;padding-top: 4px;">
                                     <asp:DropDownList ID="ddlBillType" Width="180px" runat="server" CssClass="chzn-select">
                                        <asp:ListItem Text="Choose Bill Type...." Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Credit" Value="1" Selected="true"></asp:ListItem>
                                        <asp:ListItem Text="Cash" Value="2"></asp:ListItem>
                                        </asp:DropDownList> 
                                        <asp:RequiredFieldValidator ID="rfvBilltype" runat="server" Display="Dynamic"
                                            ControlToValidate="ddlBillType" ValidationGroup="save" ErrorMessage="Please Bill Type!"
                                            InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
					                </div>
					            </div>
                            </div>
                            <div class="col-sm-4" style="width: 27%;">
                              <label class="col-sm-3 control-label" style="width: 26%;">Against</label>
					            <div class="col-sm-8" style="width: 74%;">
					                <div class="radio" style="display:inline;padding-top: 4px;">
						                <label class="radio-custom">
                                         &nbsp;<asp:RadioButton 
                                            ID="rdoCounter" AutoPostBack="true" runat="server" Checked="true" 
                                            GroupName="Against"  OnCheckedChanged="rdoCounter_CheckedChanged" />Counter
						                </label>
					                </div>
					                <div class="radio" style="display:inline;padding-top: 4px;padding-left: 5px">
						                <label class="radio-custom">
                                         <asp:RadioButton ID="rdoMTIssue" AutoPostBack="true"  runat="server"  GroupName="Against" 
                                            CssClass="by_receipt" oncheckedchanged="rdoMTIssue_CheckedChanged"/>Matrial Iss.
						                </label>
					                </div> 
                                     
					            </div>
                            </div>
                            <div class="col-sm-1" style="width: 4%;">
                                         <asp:LinkButton ID="lnkbtnContnrDtl" runat="server" 
                                             CssClass="btn btn-sm btn-primary acc_home" onclick="lnkbtnContnrDtl_Click"><i class="fa fa-file"></i></asp:LinkButton>
                                    </div>
                            <div class="col-sm-4">
                                <label class="col-sm-3 control-label" style="width: 30%;">Party Name<span class="required-field">*</span></label>
                                <div class="col-sm-8" style="width: 59%;">
                                    <asp:DropDownList ID="ddlParty" Width="165px" runat="server" CssClass="chzn-select"   >
                                    </asp:DropDownList>                                                                           
                                    <asp:RequiredFieldValidator ID="rfvParty"  runat="server" Display="Dynamic" ControlToValidate="ddlParty"
                                    ValidationGroup="save" ErrorMessage="Please select Party's Name!" InitialValue="0"
                                    SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>

                                </div>
                            <div class="col-sm-1" style="width: 9%;">
                                <asp:LinkButton ID="lnkbtnRefreshParty"  runat="server" CssClass="btn-sm btn btn-primary acc_home" ToolTip="Update Sender" ><i class="fa fa-refresh"></i></asp:LinkButton>  

                            </div>
                            </div>
                          </div>
                          <div class="clearfix odd_row">
                          <div class="col-sm-4">
                              <label class="col-sm-4 control-label" style="width: 30%;">Tax Type</label>
                              <div class="col-sm-8" style="width: 69%;">
                                <asp:DropDownList ID="ddlTaxType" runat="server" CssClass="form-control" >
                                    <asp:ListItem Text="VAT" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="CST" Value="2"></asp:ListItem>
                                </asp:DropDownList>       
                              </div>
                            </div>
                             <div class="col-sm-10" style="width:66%">
                           		<label class="col-sm-3 control-label" style="width: 12%;">Remark</label>
                              <div class="col-sm-10" style="width:88%">
                               <asp:TextBox ID="TxtRemark" runat="server" CssClass="form-control" TextMode="SingleLine" MaxLength="200" ></asp:TextBox>
                              </div>
                           	</div>
                            </div>
                        </div>
                      </section>                        
                    </div>
                    <!-- second  section -->
                 		<div id="DivItems" runat="server" class="clearfix second_section">
                      <section class="panel panel-in-default">  
                        <div class="panel-body">
                          <div class="clearfix even_row">
                            <div class="col-sm-2" style="width:19%">
                              <label class="control-label">Item Name<span class="required-field">*</span></label>
                              <div>
                                <asp:DropDownList ID="ddlSerialNo" runat="server" AutoPostBack="true"  
                                      CssClass="chzn-select" Width="165px" 
                                      onselectedindexchanged="ddlSerialNo_SelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSerialNo" runat="server" ControlToValidate="ddlSerialNo" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"  ErrorMessage="Select Item Name!" CssClass="classValidation"></asp:RequiredFieldValidator>                
                              </div>
                            </div>
                            <div class="col-sm-2" style="width:14%">
                              <label class="control-label">Model Name<span class="required-field">*</span></label>
                              <div>
                               <asp:DropDownList ID="ddlModelName" Enabled="false" Width="120px" runat="server" CssClass="chzn-select" >
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvModelName" runat="server" ControlToValidate="ddlModelName"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"  ErrorMessage="Choose model!" CssClass="classValidation"></asp:RequiredFieldValidator>

                              </div>
                            </div>
                           	<div class="col-sm-2" style="width:10%">
                              <label class="control-label">Tyre Type</label>
                              <div>
                               <asp:DropDownList ID="ddltype" Enabled="false" Width="85px" runat="server" CssClass="chzn-select" >
                                </asp:DropDownList>
                              </div>
                            </div>

                            <div class="col-sm-2" style="width:10%">
                              <label class="control-label">Rate Type<span class="required-field">*</span></label>
                              <div>
                                <asp:DropDownList ID="ddlRateType" AutoPostBack="false" Width="85px" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Choose...." Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Rate" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Weight" Value="2"></asp:ListItem>
                                </asp:DropDownList> 
                                <asp:RequiredFieldValidator ID="rfvDdlRate" runat="server" ControlToValidate="ddlRateType" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"
                                    ErrorMessage="Choose Rate Type!" CssClass="classValidation"> </asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-1" style="width:7%">
                              <label class="control-label">Qty.<span class="required-field">*</span></label>
                              <div>
                                <asp:TextBox ID="txtQuantity" runat="server" Text="0" CssClass="form-control"  
                                      MaxLength="6"  Style="text-align: right;" Enabled="true"  AutoPostBack="true" onKeyPress="return checkfloat(event, this);"
                               oncopy="return false" onpaste="return false" oncut="return false" 
                                      oncontextmenu="return false" ontextchanged="txtQuantity_TextChanged"></asp:TextBox>
                              </div>
                            </div>
                            <div class="col-sm-1" style="width:10%">
                              <label class="control-label">Weight<span id="SpanWeightId" runat="server" class="required-field">*</span></label>
                              <div>
                                <asp:TextBox ID="txtweight" runat="server" CssClass="form-control" style="text-align:right;" MaxLength="30"  onKeyPress="return checkfloat(event, this);" oncopy="return false" onpaste="return false" oncut="return false" Text="0.00" oncontextmenu="return false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvweight" runat="server" ControlToValidate="txtweight"  InitialValue="" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Submit"  ErrorMessage="Enter Weight!" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                           	<div class="col-sm-1" style="width:10%">
                              <label class="control-label">Rate<span id="SpanRateId" runat="server" class="required-field">*</span></label>
                              <div>
                              <asp:TextBox ID="txtrate" runat="server" CssClass="form-control" MaxLength="30" Text="0.00"  onKeyPress="return checkfloat(event, this);" oncopy="return false"
                                    style="text-align:right;"  onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>                       
                             <asp:RequiredFieldValidator ID="rfvtxtrate" runat="server" ControlToValidate="txtrate"  InitialValue="" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Submit"  ErrorMessage="Enter Rate!" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-1" style="width:10%">
                              <label class="control-label">Disc. Type</label>
                              <div>
                              <asp:DropDownList ID="ddlDiscountItemWise" AutoPostBack="false"  Width="85px" runat="server" CssClass="chzn-select"  >
                                    <asp:ListItem Text="Amount" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="% Per." Value="2"></asp:ListItem>
                                </asp:DropDownList> 
                              </div>
                            </div>
                            <div class="col-sm-1" style="width:10%">
                              <label class="control-label">Disc. Amnt</label>
                              <div>
                              <asp:TextBox ID="txtDiscountItem" runat="server" CssClass="form-control" MaxLength="30" Text="0.00"  onKeyPress="return checkfloat(event, this);" oncopy="return false"
                                    style="text-align:right;"  onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>                       
                    
                              </div>
                            </div>
                          </div>  
                           <div class="clearfix even_odd">
                            <div class="col-sm-5">
                            <asp:Label ID="lblStockAss" CssClass="classValidation" Enabled="false" runat="server" Text=""></asp:Label>
                           </div>
                           <div class="col-sm-4">

                           </div>
                           <div class="col-sm-1" style="width:11%">
                              <div >                            
                                <asp:LinkButton ID="lnkbtnSubmitClick" runat="server"   
                                      style="padding-bottom:5px;" 
                                      CssClass="btn full_width_btn btn-sm btn-primary subnew" CausesValidation="true" 
                                      ValidationGroup="Submit" onclick="lnkbtnSubmitClick_Click" >Submit</asp:LinkButton>
                               </div>
                              </div>
                                 <div class="col-sm-1" style="width:11%">
                                
                              <div>
                               <asp:LinkButton ID="lnkbtnNewClick" runat="server"  style="padding-bottom:5px;" 
                                      CssClass="btn full_width_btn btn-sm btn-primary subnew" 
                                      CausesValidation="false" onclick="lnkbtnNewClick_Click" >New</asp:LinkButton>
                                <asp:HiddenField ID="HidTaxRate" runat="server" />
                                <asp:HiddenField ID="HidTax" runat="server" />
                                <asp:HiddenField ID="hidrowid" runat="server" />
                                <asp:HiddenField ID="hidSbillHeadIdno" runat="server" />
                                <asp:HiddenField ID="HideItemType" runat="server" />
                                <asp:HiddenField ID="HideBalanceStock" runat="server" />
                                
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
                                    OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand"
                                    OnRowDataBound="grdMain_RowDataBound" Width="100%" GridLines="None" AllowPaging="false"  BorderWidth="0"  ShowFooter="true">
                                 <RowStyle CssClass="odd" />
                                <AlternatingRowStyle CssClass="even" />     
                                <Columns>
                                    <asp:TemplateField  HeaderText="Action"  HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                         <asp:LinkButton ID="lnkbtEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("id") %>' CommandName="cmdedit" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>

                                         <asp:LinkButton ID="lnkbtnDelete" CssClass="edit" runat="server" CommandArgument='<%#Eval("id") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete"  ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                  
                                            <asp:ImageButton ID="imgstck" runat="server" CommandArgument='<%#Eval("SerialNoIdno") %>'
                                                CommandName="cmdstck" ImageUrl="~/Images/greenbutton.jpg" Visible="false" ToolTip="Enter Tyre Details" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="50" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Issue No.">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                             <%#Eval("MatIssNo")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Item Name">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                             <%#Eval("SerialNo")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Model">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                            <%#Eval("ModelName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Category">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                            <%#Eval("TyreType")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Type">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                            <%#Eval("RateType")%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Qty">
                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblQty" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Quantity")))%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotQuantity" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Weight">
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
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Tax Rate">
                                        <ItemStyle HorizontalAlign="Right" Width="150" />
                                        <ItemTemplate>
                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("TaxRate")))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="100" HeaderText="Tax Amnt">
                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                        <ItemTemplate>
                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("TaxAmnt")))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblVAT" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Discount">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                            <%# (Eval("DiscountValue") +" "+ (Convert.ToString(Eval("DiscountType")) == "1" ? "Amnt" : "%"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="100" HeaderText="Dis. Amnt">
                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                        <ItemTemplate>
                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Discount") == "" ? 0 : Convert.ToDouble(Eval("Discount"))))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblDiscount" runat="server"></asp:Label>
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
                               <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="form-control" MaxLength="7"  Enabled="false" Text="0.00" Style="text-align: right;" OnChange="ComputeCosts();"
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
                              <label class="col-sm-6 control-label">Other Charges</label>
                              <div class="col-sm-6">
                                   <asp:TextBox ID="txtOtherCharges" runat="server" CssClass="form-control" 
                                       MaxLength="7" AutoPostBack="true" Text="0.00" Style="text-align: right;" 
                                    onpaste="return false" oncut="return false" oncopy="return false" onKeyPress="return checkfloat(event, this);"
                                    ontextchanged="txtOtherCharges_TextChanged"></asp:TextBox>

                              </div>
                            </div>			                              
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-3"></div>
                           	<div class="col-sm-1"></div>
                            <div class="col-sm-5">
                              <label class="col-sm-4 control-label">Discount Type</label>
                              <div class="col-sm-4">
                                  <asp:DropDownList ID="ddlDiscountType" Width="110px" Enabled="false" 
                                      runat="server" CssClass="chzn-select"   AutoPostBack="true"
                                      onselectedindexchanged="ddlDiscountType_SelectedIndexChanged">
                                    <asp:ListItem Text="Amount" Selected="True" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="%" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                              </div>
                              <div class="col-sm-4">

                                  <asp:TextBox ID="txtDiscountPer" runat="server" CssClass="form-control" 
                                      MaxLength="7" AutoPostBack="true" Enabled="false"
                                    Text="0.00" Style="text-align: right;" onpaste="return false" oncut="return false"
                                    oncopy="return false" onKeyPress="return checkfloat(event, this);" 

                                      ontextchanged="txtDiscountPer_TextChanged"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                    ControlToValidate="ddlDiscountType" ValidationGroup="save" ErrorMessage="Please select discount type!"
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                           	<div class="col-sm-3">
                              <label class="col-sm-6 control-label">Discount</label>
                              <div class="col-sm-6">
                                <asp:TextBox ID="txtDiscount" runat="server" CssClass="form-control" 
                                      MaxLength="7"  AutoPostBack="false" Enabled="false"
                                Text="0.00" Style="text-align: right;" onpaste="return false" oncut="return false"
                                oncopy="return false" onKeyPress="return checkfloat(event, this);" ></asp:TextBox>
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
                                <asp:TextBox ID="txtRoundOff" runat="server" CssClass="form-control" MaxLength="7" Enabled="false" Text="0.00" Style="text-align: right;" OnChange="ComputeCosts();"
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
                                <asp:TextBox ID="txtNetAmnt" runat="server" CssClass="form-control" MaxLength="7" Enabled="false" Text="0.00" Style="text-align: right;" OnChange="ComputeCosts();"
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
                              <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false" 
                                     Visible="false"  CssClass="btn full_width_btn btn-s-md btn-info" 
                                     onclick="lnkbtnNew_Click"  ><i class="fa fa-file-o"></i>New</asp:LinkButton>  
                                    <asp:HiddenField ID="hidmindate" runat="server" />
                                    <asp:HiddenField ID="hidmaxdate" runat="server" />
                             </div>                               
							    <div class="col-sm-4" id="DivbtnSave" runat="server">
                                    <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" 
                                        ValidationGroup="save" CssClass="btn full_width_btn btn-s-md btn-success"  
                                        onclick="lnkbtnSave_Click" ><i class="fa fa-save"></i>Save</asp:LinkButton> 
								   
							    </div>
							    <div class="col-sm-4">
								  <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" 
                                        CssClass="btn full_width_btn btn-s-md btn-danger" 
                                        onclick="lnkbtnCancel_Click" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
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
        <div class="col-lg-1">
        </div>
    </div>

     <div id="divMaterialIssueSearch" class="modal fade">
							<div class="modal-dialog">
							<div class="modal-content">
								<div class="modal-header">
								<h4 class="popform_header">Matrial Issue Detail</h4>
								</div>
								<div class="modal-body">
								<section class="panel panel-default full_form_container material_search_pop_form">
									<div class="panel-body">
										<!-- First Row start -->
									<div class="clearfix odd_row">	                                
	                                <div class="col-sm-4" style="width:28%">
	                                  <label class="col-sm-5 control-label" style="width:48%">Date From</label>
                                    <div class="col-sm-7" style="width:52%">
                                     <asp:TextBox ID="txtDivDateFrom" runat="server" CssClass="input-sm datepicker form-control"  data-date-format="dd-mm-yyyy"></asp:TextBox>                                     
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtFrm" runat="server" ErrorMessage="Enter From Date!"
                                            Display="Dynamic" CssClass="classValidation" ControlToValidate="txtDivDateFrom" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                    </div>
	                                </div>
	                                <div class="col-sm-4"  style="width:28%">
	                                  <label class="col-sm-5 control-label"  style="width:48%">Date To</label>
                                    <div class="col-sm-7" style="width:52%">
                                       <asp:TextBox ID="txtDivDateTo" runat="server" CssClass="input-sm datepicker form-control" data-date-format="dd-mm-yyyy"></asp:TextBox>                                     
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtTo" runat="server" ErrorMessage="Enter To Date!"  Display="Dynamic" CssClass="classValidation" ControlToValidate="txtDivDateTo" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                    </div>
	                                </div>
                                    <div class="col-sm-4" style="padding: 0;">
	                                  <div class="col-sm-6 prev_fetch">                                      
                                        <asp:LinkButton ID="lnkbtnSearch" runat="server" CssClass="btn full_width_btn btn-sm btn-primary" CausesValidation="true" ValidationGroup="RcptEntrySrch" OnClick="lnkbtnSearch_OnClick"><i class="fa fa-search"></i>Search</asp:LinkButton>	                                
	                                  </div>
	                                  <div class="col-sm-6"> 
	                                     <label id="lblSearchRecords" runat="server" class="control-label">T. Record(s) : 0</label>
	                                  </div>
	                                </div>
	                               </div> 
                                  <div class="clearfix fourth_right">
                                  <section class="panel panel-in-default btns_without_border">                            
                                  <div class="panel-body">     
                                  <div class="clearfix">
		                          <section class="panel panel-default full_form_container material_search_pop_form">
		                            <div class="panel-body">   
                                      <asp:GridView ID="grdMIdetals" runat="server" GridLines="None" AutoGenerateColumns="false"
                                            Width="100%" BorderStyle="None" CssClass="display nowrap dataTable"
                                            BorderWidth="0" >
                                           <RowStyle CssClass="odd" />
                                        <AlternatingRowStyle CssClass="even" />  
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select" HeaderStyle-Width="40px">
                                                     <HeaderStyle Width="40" CssClass="gridHeaderAlignCenter" />
                                                <ItemStyle Width="40" CssClass="gridHeaderAlignCenter" />
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAllCheckboxes(this);"
                                                            CssClass="SACatA" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkId" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issue No." HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("MatIssNo"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date Of issue" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToDateTime(Eval("MatIssDate")).ToString("dd-MMM-yyyy")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <%-- <asp:TemplateField HeaderText="Lorry No." HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%#Eval("LorryNo")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                 <asp:TemplateField HeaderText="Party Name" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%#Eval("PrtyName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="From City" HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Right">
                                                    <ItemStyle Width="20px" HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <%#Eval("CityName")%>
                                                         <asp:HiddenField ID="hidMIIdno" runat="server" Value='<%#Eval("MatIssIdno")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                Records(s) not found.
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                       
                                      
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
                          
                            
                            <asp:LinkButton ID="lnkbtnSubmit" OnClick="lnkbtnSubmit_Click"  runat="server" CssClass="btn btn-dark"><i class="fa fa-check"></i>Ok</asp:LinkButton>
								
                                <asp:LinkButton ID="lnkbtnClose" OnClick="lnkbtnClose_Click" runat="server" CssClass="btn btn-dark"><i class="fa fa-times"></i>Close</asp:LinkButton>
						
                                <div style="float:left;">                              
                                <asp:Label ID="Label3" runat="server" CssClass="redfont"></asp:Label>
                                  </div>
							</div>
							</div>
						</div>
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
                                        <asp:Label ID="lblPrintHeadng" runat="server" Text="Sale Bill"></asp:Label></strong></h3>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table border="0" width="100%">
                                    <tr>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;">
                                            <asp:Label ID="lblBillNoText" Text="Bill No. :" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;">
                                            <b>
                                                <asp:Label ID="lblBillNoval" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;">
                                            <asp:Label ID="txtBillDateText" Text="Bill Date :" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;">
                                            <b>
                                                <asp:Label ID="lblBillDateVal" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;">
                                            <asp:Label ID="lbltxtFromcityText" Text="Location :" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;">
                                            <b>
                                                <asp:Label ID="lblFromCityVal" runat="server"></asp:Label></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lblPartyteText" Text="Party Name :" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblPartyVal" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lblBillTypeText" Text="Bill Type :" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblBillTypeVal" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lblTaxTypeText" Text="Tax Type :" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblTaxTypeVal" runat="server"></asp:Label></b>
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
                                                    <strong>Serial No</strong>
                                                </td>
                                                <td style="font-size: 12px" width="20%">
                                                    <strong>Item</strong>
                                                </td>
                                                <td style="font-size: 12px" width="8%">
                                                    <strong>T.Type</strong>
                                                </td>
                                                <td style="font-size: 12px" align="left" width="8%">
                                                    <strong>R.Type</strong>
                                                </td>
                                                <td style="font-size: 12px" align="left" width="8%">
                                                    <strong>Quantity</strong>
                                                </td>
                                                <td style="font-size: 12px" align="left" width="10%">
                                                    <strong>Rate</strong>
                                                </td>
                                                <td style="font-size: 12px" align="left" width="10%">
                                                    <strong>Weight</strong>
                                                </td>
                                                <td style="font-size: 12px" align="left" width="8%">
                                                    <strong>Dis. Type</strong>
                                                </td>
                                                <td style="font-size: 12px" align="left" width="10%">
                                                    <strong>Discount</strong>
                                                </td>
                                                <td style="font-size: 12px" align="left" width="8%">
                                                    <strong>Tax Rate</strong>
                                                </td>
                                                <td style="font-size: 12px" align="left" width="10%">
                                                    <strong>Tax Amnt</strong>
                                                </td>
                                                <td style="font-size: 12px" align="left" width="15%">
                                                    <strong>Amount</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="white_bg" width="10%">
                                                    <%#Container.ItemIndex+1 %>.
                                                </td>
                                                <td class="white_bg" width="20%">
                                                    <%#Eval("SerialNo")%>
                                                </td>
                                                <td class="white_bg" width="20%">
                                                    <%#Eval("ModelName")%>
                                                </td>
                                                <td align="left" class="white_bg" width="8%">
                                                    <%#Eval("TyreType_Name")%>
                                                </td>
                                                <td align="left" class="white_bg" width="8%">
                                                    <%#Eval("RateType")%>
                                                </td>
                                                <td align="left" class="white_bg" width="8%">
                                                    <%#Eval("Quantity")%>
                                                </td>
                                                <td class="white_bg" width="10%" align="right">
                                                    <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Rate")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="white_bg" width="10%" align="right">
                                                    <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Weight")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td align="left" class="white_bg" width="8%">
                                                    <%#Eval("ItemDiscountType")%>
                                                </td>
                                                <td class="white_bg" width="10%" align="right">
                                                    <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("ItemDiscount")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td align="left" class="white_bg" width="8%">
                                                    <%#Eval("TaxRate")%>
                                                </td>
                                                <td class="white_bg" width="10%" align="right">
                                                    <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("TaxAmnt")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="white_bg" width="15%" align="right">
                                                    <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                        <td class="white_bg" width="10%">
                                            &nbsp;
                                        </td>
                                        <td class="white_bg" width="9%" align="center">
                                            <asp:Label ID="lblttl" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                        <td class="white_bg" width="12%" align="Right">
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lbltotalqty" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                         <td class="white_bg" width="15%">
                                            &nbsp;
                                        </td>
                                        <td class="white_bg" width="10%" align="left">
                                            <asp:Label ID="lbltotalWeight" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                        <td class="white_bg" width="10%" align="left">
                                              <asp:Label ID="lblTotalDiscount" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                        <td class="white_bg" width="7%">
                                            &nbsp;
                                        </td>
                                        <td class="white_bg" width="27%" align="center">
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
        window.onload = function () {

        };

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

        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $("#<%=txtDivDateFrom.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
        }

        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $("#<%=txtDivDateTo.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
        }

        function openModal() {
            $("#divMaterialIssueSearch").fadeIn(300);
        }

        function openGridDetail() {
            $('#divMaterialIssueSearch').modal('show');
        }

        function CheckPromotionalCode() {
            var txtControl = document.getElementById('<%=ddlFromCity.ClientID %>');
            txtControl.focus();
        }



//        function OnchangeRatetype(strid) {
//            document.getElementById("<%=txtQuantity.ClientID%>").value = "1";
//            if (strid == "1") {
//                var ratevalue = document.getElementById("<%=txtrate.ClientID%>").value;

//                document.getElementById("<%=SpanRateId.ClientID%>").style.visibility = "visible";
//                document.getElementById("<%=SpanWeightId.ClientID%>").style.visibility = "hidden";

//                document.getElementById("<%=rfvweight.ClientID%>").style.visibility = "hidden";
//                document.getElementById("<%=rfvtxtrate.ClientID%>").style.visibility = "visible";

//                document.getElementById("<%=rfvtxtrate.ClientID%>").enabled = true;
//                document.getElementById("<%=rfvweight.ClientID%>").enabled = false;


//                document.getElementById("<%=txtrate.ClientID%>").disabled = false;
//                document.getElementById("<%=txtweight.ClientID%>").disabled = true;
//                document.getElementById("<%=txtweight.ClientID%>").value = "0.00";



//            }
//            else if (document.getElementById("<%=ddlRateType.ClientID%>").value == "2") {
//                var weightvalue = document.getElementById("<%=txtweight.ClientID%>").value;
//                document.getElementById("<%=SpanRateId.ClientID%>").style.visibility = "hidden";
//                document.getElementById("<%=SpanWeightId.ClientID%>").style.visibility = "visible";

//                document.getElementById("<%=rfvtxtrate.ClientID%>").style.visibility = "hidden";

//                document.getElementById("<%=rfvweight.ClientID%>").style.visibility = "visible";
//                document.getElementById("<%=rfvtxtrate.ClientID%>").enabled = false;
//                document.getElementById("<%=rfvweight.ClientID%>").enabled = true;


//                document.getElementById("<%=txtrate.ClientID%>").disabled = true;
//                document.getElementById("<%=txtweight.ClientID%>").disabled = false;
//                document.getElementById("<%=txtrate.ClientID%>").value = "0.00";

//            }
//            else {

//            }
//        }

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
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
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
