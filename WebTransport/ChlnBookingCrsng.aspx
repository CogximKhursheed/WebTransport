<%@ Page Title="Challan Booking Crossing" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="ChlnBookingCrsng.aspx.cs" Inherits="WebTransport.ChlnBookingCrsng" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            $("#<%=txtDate.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
            $("#<%=txtGRDate.ClientID %>").datepicker({
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
            <div class="row ">
                <div class="col-lg-1">
                </div>
                <div class="col-lg-10">
                    <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">CHALLAN CROSSING
                  <span class="view_print"><a href="ManageChallanBCrsng.aspx" tabindex="27"><asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a>                  
                  <%--<a href="#"><i class="fa fa-print icon"></i></a>--%>
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
                              <asp:DropDownList ID="ddldateRange" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="1" OnSelectedIndexChanged="ddldateRange_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddldateRange"
                                CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Year!"
                                InitialValue="0" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>                                                     
                              </div>
                            </div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 20%;">Date<span class="required-field">*</span></label>
                              <div class="col-sm-4" style="width: 40%;">
                              <asp:TextBox ID="txtDate" runat="server" CssClass="input-sm datepicker form-control"   MaxLength="12"  oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" data-date-format="dd-mm-yyyy" TabIndex="2" ></asp:TextBox>                                                   
                                
                              </div>
                              <div class="col-sm-3" style="width: 40%;">
                               <asp:TextBox ID="txtchallanNo" runat="server" CssClass="form-control"  MaxLength="50" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                        onpaste="return false" TabIndex="3" ReadOnly="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Date!"
                                    SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtchallanNo"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter challan no!"
                                    SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>                               
                              </div>
                           	</div>
                           	<div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 39%;">TransportName<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 61%;">
                                <asp:DropDownList ID="ddlTranspoter" runat="server"  CssClass="chzn-select" style="width:100%;" TabIndex="4">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlTranspoter"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Transporter!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 29%;">Truck No.<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 71%;">
                              <asp:DropDownList ID="ddlTruckNo" runat="server" AutoPostBack="true" CssClass="chzn-select" style="width:100%;" TabIndex="5"  OnSelectedIndexChanged="ddlTruckNo_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTruckNo"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Truck No!" InitialValue="0"
                                    SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>                        
                              </div>
                            </div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 20%;">Owner<span class="required-field">*</span></label>
                              <div class="col-sm-8" style="width: 80%;">
                                 <asp:TextBox ID="txtOwnrNme" runat="server" CssClass="form-control"   MaxLength="50" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="6" ReadOnly="true" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtOwnrNme"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter owner name!"
                                    SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>
                           	</div>
                           	<div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 39%;">Challan/ref<span class="required-field">*</span></label>
							    <div class="col-sm-8" style="width: 61%;">
                                 <asp:TextBox ID="txtchlnRef" runat="server" CssClass="form-control"    MaxLength="50" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                    onpaste="return false" TabIndex="7"  ></asp:TextBox>                                                
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtchlnRef"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Enter Ref No!" SetFocusOnError="true"
                                    ValidationGroup="save"></asp:RequiredFieldValidator>
							    </div>
                            </div>
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 29%;">Loc.[From]<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 71%;">

                               <asp:DropDownList ID="DdlfromcityHead" runat="server" CssClass="form-control" TabIndex="8" AutoPostBack="true" OnSelectedIndexChanged="DdlfromcityHead_SelectedIndexChanged">
                                                    </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvtxtfromcity" runat="server" Display="Dynamic"
                                ControlToValidate="DdlfromcityHead" ValidationGroup="save" ErrorMessage="Please select City!"
                                InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                                     
                              </div>
                            </div>
                           	<div class="col-sm-8"></div>
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
                              <label class="control-label">GR No.<span class="required-field">*</span></label>
                              <div>
                               <asp:TextBox ID="txtGrNo" runat="server" CssClass="form-control"  MaxLength="6"  Style="text-align: right;" TabIndex="9" AutoPostBack="false" onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtGrNo"
                                    Display="Dynamic" SetFocusOnError="true" ValidationGroup="Submit" ErrorMessage="Enter GR no!"
                                    CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                           	<div class="col-sm-2">
                              <label class="control-label">GR Date<span class="required-field">*</span></label>
                              <div>
                               <asp:TextBox ID="txtGRDate" runat="server" CssClass="input-sm datepicker form-control" MaxLength="30" TabIndex="10" onKeyPress="return checkfloat(event, this);" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtGRDate"
                                    Display="Dynamic" SetFocusOnError="true" ValidationGroup="Submit" ErrorMessage="Please select Date!"
                                    CssClass="classValidation"></asp:RequiredFieldValidator>
                              
                              </div>
                            </div>

                            <div class="col-sm-1">
                              <label class="control-label">Type<span class="required-field">*</span></label>
                              <div>
                                <asp:DropDownList ID="ddlGrType" runat="server" CssClass="form-control" TabIndex="11">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Paid GR" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="TBB GR" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="To Pay GR" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlGrType"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"
                                    ErrorMessage="Select GR type!" CssClass="classValidation"></asp:RequiredFieldValidator>                                   
                              </div>
                            </div>
                            <div class="col-sm-1">
                              <label class="control-label">FromCity<span class="required-field">*</span></label>
                              <div>
                              <asp:DropDownList ID="ddlfromCity" runat="server" CssClass="chzn-select" style="width:100%;"  TabIndex="12">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlfromCity"
                                Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"
                                ErrorMessage="Select From City!" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-1">
                              <label class="control-label">ToCity<span class="required-field">*</span></label>
                              <div>
                                <asp:DropDownList ID="ddltoCity" runat="server" CssClass="chzn-select" style="width:100%;" TabIndex="13">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPartno" runat="server" ControlToValidate="ddltoCity"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"
                                    ErrorMessage="Select To City!" CssClass="classValidation"></asp:RequiredFieldValidator>                                             
                              </div>
                            </div>
                           	<div class="col-sm-2">
                              <label class="control-label">Sender<span class="required-field">*</span></label>
                              <div>
                               <asp:DropDownList ID="ddlSenderName" runat="server" CssClass="chzn-select" style="width:100%;" TabIndex="14">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvAmnt" runat="server" ControlToValidate="ddlSenderName"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"
                                    ErrorMessage="Select Sender Name!" CssClass="classValidation"></asp:RequiredFieldValidator>

                              </div>
                            </div>
                            <div class="col-sm-2">
                              <label class="control-label">Receiver<span class="required-field">*</span></label>
                              <div>
                               <asp:DropDownList ID="ddlReciverName" runat="server" CssClass="chzn-select" style="width:100%;"  TabIndex="15">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlReciverName"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"
                                    ErrorMessage="Select Reciver Name!" CssClass="classValidation"></asp:RequiredFieldValidator>                         
                              </div>
                            </div>
                            <div class="col-sm-1">
                              <label class="control-label">Qty<span class="required-field">*</span></label>
                              <div>
                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"   MaxLength="6" Style="text-align: right;" TabIndex="16" AutoPostBack="false" onKeyPress="return checkfloat(event, this);">1</asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtQuantity" runat="server" ControlToValidate="txtQuantity"
                                    Display="Dynamic" SetFocusOnError="true" ValidationGroup="Submit" ErrorMessage="Enter Quantity!"
                                    CssClass="classValidation"></asp:RequiredFieldValidator>                            
                              </div>
                            </div>
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-2">
                              <label class="control-label">Weight<span class="required-field">*</span></label>
                              <div>
                                <asp:TextBox ID="txtweight" runat="server" CssClass="form-control" MaxLength="6" Style="text-align: right;" TabIndex="17" AutoPostBack="false" onKeyPress="return checkfloat(event, this);">1</asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtweight"
                                    Display="Dynamic" SetFocusOnError="true" ValidationGroup="Submit" ErrorMessage="Enter Weight!"
                                    CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-2">
                              <label class="control-label">Amount<span class="required-field">*</span></label>
                              <div>
                              <asp:TextBox ID="txtAmount" runat="server" AutoPostBack="false" CssClass="form-control" MaxLength="30"
                            TabIndex="18" onKeyPress="return checkfloat(event, this);">0.00</asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtAmount"
                                Display="Dynamic" SetFocusOnError="true" ValidationGroup="Submit" ErrorMessage="Enter Rate!"
                                CssClass="classValidation"></asp:RequiredFieldValidator>                                
                              </div>
                            </div>
                            <div class="col-sm-5">	                                
                              <label class="control-label">Details<span class="required-field">*</span></label>
                              <div>
                               <asp:TextBox ID="txtdetail" runat="server" CssClass="form-control" MaxLength="30" TabIndex="19"></asp:TextBox>                             </div> 
                            </div>
                            <div class="col-sm-3">
                              <div class="col-sm-6">     
                                 <asp:LinkButton ID="lnkbtnSubmit" runat="server" OnClick="lnkbtnSubmit_OnClick" CssClass="btn full_width_btn btn-sm btn-primary subnew" TabIndex="20"  ToolTip="Click to Submit" CausesValidation="true" ValidationGroup="Submit" >Submit</asp:LinkButton> 
                             
                              </div>
                              <div class="col-sm-6">
                             <asp:LinkButton ID="lnkbtnNew" runat="server" OnClick="lnkbtnNew_OnClick" CssClass="btn full_width_btn btn-sm btn-primary subnew" TabIndex="21"  ToolTip="Click to New" >New</asp:LinkButton> 
                          
                              </div>
                            </div>
                          </div>                            
                        </div>
                      </section>                        
                    </div>

                    <!-- third  section -->
                    <div class="clearfix third_right">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body" style=" overflow-x: auto;">     
                          <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None"  Width="100%" GridLines="Both" CssClass="display nowrap dataTable"
                           AllowPaging="false" BorderWidth="0"  ShowFooter="true" OnRowCommand="grdMain_RowCommand"  OnRowCreated="grdMain_RowCreated" OnRowDataBound="grdMain_RowDataBound">
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
                                <asp:TemplateField HeaderText="GR No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="50" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%#Eval("GR_No")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="70" HeaderText="GR Date">
                                    <ItemStyle HorizontalAlign="Left" Width="70" />
                                    <ItemTemplate>
                                        <%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MM-yyyy")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="70" HeaderText="GR.Type">
                                    <ItemStyle HorizontalAlign="Left" Width="70" />
                                    <ItemTemplate>
                                        <%#Eval("Gr_Type")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="90" HeaderText="From City">
                                    <ItemStyle HorizontalAlign="Left" Width="90" />
                                    <ItemTemplate>
                                        <%#Eval("FromCity")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="90" HeaderText="To City">
                                    <ItemStyle HorizontalAlign="Left" Width="90" />
                                    <ItemTemplate>
                                        <%#Eval("ToCity")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Sender">
                                    <ItemStyle HorizontalAlign="Left" Width="150" />
                                    <ItemTemplate>
                                        <%#Eval("SenderName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Reciver">
                                    <ItemStyle HorizontalAlign="Left" Width="150" />
                                    <ItemTemplate>
                                        <%#Eval("ReciverName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Qty">
                                    <ItemStyle HorizontalAlign="Right" Width="100" />
                                    <ItemTemplate>
                                        <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Qty")))%>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lblQty" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="90" HeaderText="Weight">
                                    <ItemStyle HorizontalAlign="Right" Width="90" />
                                    <ItemTemplate>
                                        <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Weight") == "" ? 0 : Convert.ToDouble(Eval("Weight"))))%>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lblWeight" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="90" HeaderText="Amount">
                                    <ItemStyle HorizontalAlign="Right" Width="90" />
                                    <ItemTemplate>
                                        <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Amount")))%>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="90" HeaderText="Details">
                                    <ItemStyle HorizontalAlign="center" Width="90" />
                                    <ItemTemplate>
                                        <%#Eval("Detail")%>
                                    </ItemTemplate>
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
                          <div class="clearfix">
                            <div class="col-lg-6">
                            	<div class="clearfix odd_row"></div>
                            	<div class="clearfix even_row"></div>
                            </div>
                            <div class="col-lg-6">
                            	<div class="clearfix odd_row">
	                              <div class="col-sm-6">
	                                <label class="col-sm-6 control-label">Gross Amount</label>
	                                <div class="col-sm-6">
                                     <asp:TextBox ID="txtgrossAmnt" runat="server" CssClass="form-control" Text="0.00"  TabIndex="38" Style="text-align: right;" ReadOnly="true" MaxLength="10" onKeyPress="return checkfloat(event, this);"></asp:TextBox>                                     
	                                </div>
	                              </div>
	                             	<div class="col-sm-6">
	                                <label class="col-sm-6 control-label">Katt Amount</label>
	                                <div class="col-sm-6">
                                     <asp:TextBox ID="txtKattAmnt" runat="server" CssClass="form-control" Text="0.00" TabIndex="22" AutoPostBack="true" Style="text-align: right;"  MaxLength="10"
                                                                        onKeyPress="return checkfloat(event, this);" OnTextChanged="txtKattAmnt_TextChanged">0.00</asp:TextBox>
	                            
	                                </div>
	                              </div>			                              
	                            </div>
	                            <div class="clearfix even_row">
	                              <div class="col-sm-6"></div>
	                             	<div class="col-sm-6">
	                                <label class="col-sm-6 control-label">Net Amount</label>
	                                <div class="col-sm-6">
                                    
                                     <asp:TextBox ID="txtNetAmnt" runat="server" CssClass="form-control" MaxLength="7" TabIndex="23" ReadOnly="true" Text="0.00" Style="text-align: right;" OnChange="ComputeCosts();"
                                                                        onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox>
	                              
	                                </div>
	                              </div>
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
                              <asp:LinkButton ID="lnkbtnMnNew" runat="server" CausesValidation="false" Visible="false" CssClass="btn full_width_btn btn-s-md btn-info" OnClick="lnkbtnMnNew_OnClick" TabIndex="26"  ><i class="fa fa-file-o"></i>New</asp:LinkButton> 
                              </div>
                              <div class="col-sm-4">                          
                                <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" ValidationGroup="save" CssClass="btn full_width_btn btn-s-md btn-success" OnClick="lnkbtnSave_OnClick" TabIndex="24" ><i class="fa fa-save"></i>Save</asp:LinkButton>  
                             
                                
                                <asp:HiddenField ID="hidid" runat="server" Value="" />
                                <asp:HiddenField ID="Hidrowid" runat="server" Value="" />
                                <asp:HiddenField ID="hidWorkType" runat="server" />
                                <asp:HiddenField ID="hidOwnerId" runat="server" />
                                <asp:HiddenField ID="hidmindate" runat="server" />
                                <asp:HiddenField ID="hidmaxdate" runat="server" />
                                <asp:HiddenField ID="hidBaseCity" runat="server" />                           
                              </div>
                              <div class="col-sm-4">
                              <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-danger" OnClick="lnkbtnCancel_OnClick" TabIndex="25"  ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
                               
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
            <script type="text/javascript">                $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
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