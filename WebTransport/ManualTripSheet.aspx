<%@ Page Title="Manual Trip Sheet" MaintainScrollPositionOnPostback="true" Language="C#"
    MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ManualTripSheet.aspx.cs"
    Inherits="WebTransport.ManualTripSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .control-label.style-label-1
        {
            margin: 0;
            padding-bottom: 0;
        }
        .alternate-rows:nth-child(even)
        {
            background-color: skyblue;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row ">
        <div class="">
            <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">Manual Trip Sheet
                <blockr class="pull-right" >
                    <a style="font-size:15px; color:white;font-weight:bold;" href="ManageManulTripSheet.aspx"><b>List</b></a>
                    <a id="lnkbtnPrint" style="font-size:15px; color:white;" onclick="$('#PrintManualTrip').modal('show');" class="fa fa-print icon"  Visible="false" runat="server" title="Print"></a>
                </blockr>
                </header>
                <div class="panel-body">
                  <form class="bs-example form-horizontal">
                    <!-- first  section --> 
                    <div class="clearfix first_section">
                      <section class="panel panel-in-default">  
                        <div class="panel-body alternate-rows">
                        	<div class="clearfix">
                              <div class="col-sm-4">
                                  <label class="col-sm-12 control-label style-label-1 style-label-1">Date Range<span class="required-field">*</span></label>
                                  <div class="col-sm-12">
                                    <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control"  >
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                        ControlToValidate="ddlDateRange" ValidationGroup="save" ErrorMessage="Please Select Date Range."
                                        InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                                  </div>
                                </div>
                           	    <div class="col-sm-4">
                                    <div class="col-sm-6">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Trip Date<span class="required-field">*</span></label>
                                        <asp:TextBox ID="txtTripDate" runat="server" PlaceHolder="DD-MM-YYYY" CssClass="input-sm datepicker form-control" MaxLength="10" onkeydown = "return DateFormat(this, event.keyCode)"  ClientIDMode="Static" ></asp:TextBox>
                                    </div>
                                    <div class="col-sm-6">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Trip No.<span class="required-field">*</span></label>
                              	        <asp:TextBox ID="txtTripNo" OnTextChanged="txtTripNo_TextChanged" runat="server" CssClass="form-control" Style="text-align: right;" ToolTip="Trip Number" Enabled="true"   MaxLength="9"></asp:TextBox>
                                    </div>
                           	    </div>
                              <div class="col-sm-4">
                                <label class="col-sm-12 control-label style-label-1 style-label-1">Loc.[From]<span class="required-field">*</span></label>
                                  <div class="col-sm-12">
                                    <asp:DropDownList ID="ddlCompFromCity" runat="server" CssClass="form-control"  
                                     >
                                    </asp:DropDownList>                                   
                                  </div>
                            </div>
                          </div>
                          <div class="clearfix">
                          <div class="col-sm-4">
                                <label class="col-sm-12 control-label style-label-1 style-label-1">Truck No.<span class="required-field">*</span></label>
                                <div class="col-sm-12">
                                    <asp:DropDownList style="width:100%" ID="ddlTruckNo" CssClass="chzn-select form-control" runat="server"  AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                          <div class="col-sm-4">
                           		<label class="col-sm-12 control-label style-label-1 style-label-1">KMS<span class="required-field">*</span></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txkStartKms" onKeyPress="return checkfloat(event, this);" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static" ></asp:TextBox>
                                </div>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txkEndKms" onKeyPress="return checkfloat(event, this);" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static" ></asp:TextBox>
                                </div>
                           	</div>
                            <div class="col-sm-4">
                           		<label class="col-sm-12 control-label style-label-1 style-label-1">Driver<span class="required-field">*</span></label>
                              <div class="col-sm-12">
                                <asp:TextBox ID="txtDriverName" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static" ></asp:TextBox>
                              </div>
                           	</div>
                            </div>
                            <div class="clearfix">
                            <div class="col-sm-4">
                                  <label class="col-sm-12 control-label style-label-1 style-label-1">Party Name<span class="required-field">*</span></label>
                                  <div class="col-sm-12">
                                    <asp:DropDownList ID="ddlSender" runat="server" CssClass="form-control"  >
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                                        ControlToValidate="ddlDateRange" ValidationGroup="save" ErrorMessage="Please Select Date Range."
                                        InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                                  </div>
                            </div>
                              <div class="col-sm-4">
                              <label class="col-sm-12 control-label style-label-1 style-label-1">From City<span class="required-field">*</span></label>
                              <div class="col-sm-12">
                                <asp:DropDownList ID="ddlFromCity" runat="server" CssClass="form-control"  
                                 >
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                    ControlToValidate="ddlFromCity" ValidationGroup="save" ErrorMessage="Please Select From City."
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                              <div class="col-sm-4">
                              <label class="col-sm-12 control-label style-label-1 style-label-1">To City<span class="required-field">*</span></label>
                              <div class="col-sm-12">
                                <asp:DropDownList ID="ddlToCity" runat="server" CssClass="form-control"  
                                 >
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                    ControlToValidate="ddlFromCity" ValidationGroup="save" ErrorMessage="Please Select From City."
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
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
                                <div class="col-sm-3">
                                  <label class="control-label">Item Name<span class="required-field">*</span></label>
                                  <div>
                                    <asp:TextBox ID="txtItemName" runat="server" CssClass="input-sm form-control" MaxLength="10" onkeydown = "return DateFormat(this, event.keyCode)"  ClientIDMode="Static" ></asp:TextBox>
                                  </div>
                                </div>
                           	    <div class="col-sm-1" >
                                  <label class="control-label">Item Size<span class="required-field">*</span></label>
                                  <div>
                                     <asp:TextBox ID="txtItemSize" runat="server" CssClass="form-control"  MaxLength="6"></asp:TextBox>
                                  </div>
                                </div>
                                <div class="col-sm-1" >
                                  <label class="control-label">Rate Type<span class="required-field">*</span></label>
                                  <div>
                                  <asp:DropDownList ID="ddlRateType" AutoPostBack="false" onchange="javascript:OnChangeddlRateType();" runat="server" CssClass="form-control" >
                                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Rate" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Weight" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                  </div>
                                </div>
                                <div class="col-sm-1">
                                  <label class="control-label">Quantity<span class="required-field">*</span></label>
                                  <div>
                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"  MaxLength="6"
                                    Style="text-align: right;" AutoPostBack="false"
                                    onKeyPress="return checkfloat(event, this);" oncopy="return false" onpaste="return false"
                                    oncut="return false" oncontextmenu="return false">1</asp:TextBox>
                                  </div>
                                </div>
                                <div class="col-sm-1">
                                  <label class="control-label">G. Weight<span class="required-field">*</span></label>
                                  <div>
                                    <asp:TextBox ID="txtGweight" runat="server" CssClass="form-control" MaxLength="10"
                                     onKeyPress="return checkfloat(event, this);" oncopy="return false"
                                    onpaste="return false" oncut="return false" oncontextmenu="return false" 
                                          AutoPostBack="false"></asp:TextBox>
                                  </div>
                                </div>
                                 <div class="col-sm-1">
                                  <label class="control-label">A. Weight<span class="required-field">*</span></label>
                                  <div>
                                    <asp:TextBox ID="txtAweight" runat="server" CssClass="form-control" MaxLength="10"
                                     onKeyPress="return checkfloat(event, this);" oncopy="return false"
                                    onpaste="return false" oncut="return false" oncontextmenu="return false" 
                                          AutoPostBack="false"></asp:TextBox>
                                  </div>
                                </div>
                           	    <div class="col-sm-1">
                                  <label class="control-label">Rate<span class="required-field">*</span></label>
                                  <div>
                                    <asp:TextBox ID="txtrate" runat="server" CssClass="form-control" MaxLength="10"
                                        onKeyPress="return checkfloat(event, this);" oncopy="return false"
                                        onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                  </div>
                                </div>
                                <div class="col-sm-2">
                                  <label class="control-label">Total<span class="required-field">*</span></label>
                                  <div>
                                    <asp:TextBox ID="txtTotalAmount" OnTextChanged="txtTotalFreight_TextChanged" runat="server" CssClass="form-control" MaxLength="10"
                                        onKeyPress="return checkfloat(event, this);" oncopy="return false" AutoPostBack="true"
                                        onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                  </div>
                                </div>
                                <div class="col-sm-1">
                                  <label class="control-label"><asp:CheckBox ID="chkFixRate" runat="server" OnCheckedChanged="FixRate_CheckedChanged" AutoPostBack="true"></asp:CheckBox> <span class="required-field">Fix Rate</span></label>
                                  <asp:LinkButton ID="lnkCalculate" OnClick="Calculate_Click" runat="server" style="margin-top:0px;" CssClass="btn full_width_btn btn-sm btn-primary subnew"  ToolTip="Click to Submit" CausesValidation="true" ValidationGroup="Submit" >Calculate</asp:LinkButton>
                                </div>
                                </div>
                                <div class="clearfix odd_row">
                                <div class="col-sm-4">
                                  <label class="control-label">Party Advance</label>
                                  <div>
                                    <asp:TextBox ID="txtAdvance" OnTextChanged="txtAdvance_TextChanged" runat="server" CssClass="form-control"  Text="0.00" Style="text-align: right;" AutoPostBack=true
                                    onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                  </div>
                                </div>
                                <div class="col-sm-4">
                                  <label class="control-label">Party Commission</label>
                                  <div>
                                    <asp:TextBox ID="txtCommission" OnTextChanged="txtCommission_TextChanged" runat="server" CssClass="form-control"  Text="0.00" Style="text-align: right;" AutoPostBack=true
                                    onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                  </div>
                                </div>
                                <div class="col-sm-4">
                                  <label class="control-label">Total Party Adv.</label>
                                  <div>
                                    <asp:TextBox ID="txtTotalPartyAdv" runat="server" CssClass="form-control"  Text="0.00" Style="text-align: right;" Enabled=false
                                    onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                  </div>
                                </div>
                                <div class="col-sm-12">
                                  <div class="col-sm-4">
                           		        <label class="col-sm-12 control-label style-label-1 style-label-1">RTO Challan</label>
                                      <div class="col-sm-12">
                                        <asp:TextBox ID="txtRTOChallan" runat="server" OnTextChanged="txtRTOChallan_TextChanged"  CssClass="form-control"  Text="0.00" Style="text-align: right;" AutoPostBack=true
                                    onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                      </div>
                           	        </div>
                                    <div class="col-sm-4">
                           		        <label class="col-sm-12 control-label style-label-1 style-label-1">Detention</label>
                                      <div class="col-sm-12">
                                        <asp:TextBox ID="txtDetention" runat="server" OnTextChanged="txtDetention_TextChanged"  CssClass="form-control"  Text="0.00" Style="text-align: right;" AutoPostBack=true
                                    onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                      </div>
                           	        </div>
                                 <div class="col-sm-4">
                                  <label class="control-label">Total Freight</label>
                                  <div>
                                    <asp:TextBox ID="txtTotalFreight" runat="server" CssClass="form-control"  Text="0.00" Style="text-align: right;" Enabled=false
                                    onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                  </div>
                                </div>
                                </div>
                                 <div class="col-sm-4">
                                  <label class="control-label">Received</label>
                                  <div>
                                    <asp:TextBox ID="txtReceived" runat="server" OnTextChanged="txtReceived_TextChanged"  CssClass="form-control"  Text="0.00" Style="text-align: right;" AutoPostBack=true
                                    onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                  </div>
                                </div>
                                 <div class="col-sm-4">
                                  <label class="control-label">Rec. Type</label>
                                  <div>
                                  <asp:DropDownList ID="ddlRecType" AutoPostBack="false" onchange="javascript:OnChangeddlRateType();" runat="server" CssClass="form-control" >
                                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Cheque" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                  </div>
                                </div>
                                <div class="col-sm-4">
                                  <label class="control-label">Total Party Balance</label>
                                  <div>
                                    <asp:TextBox ID="txtTotalPartyBalance" runat="server" CssClass="form-control"  Text="0.00" Style="text-align: right;" Enabled=false
                                    onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                  </div>
                                </div>
                            </div>
                            <%--GRIDVIEW--%>
                            <asp:GridView ID="grdMain" runat="server" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="display nowrap dataTable"
                                Width="100%" BorderStyle="None" BorderWidth="0"  PageSize="50">
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
                    <section class="panel panel-in-default">                            
                    <div class="panel-body">     
                        <div class="clearfix even_row">
                            <div class="col-sm-4">
                                <label class="col-sm-12 control-label style-label-1">Driver Cash Advance</label>
                                <div class="col-sm-12">
                                <asp:TextBox ID="txtDriver" runat="server" CssClass="form-control" OnTextChanged="DriverCash_TextChanged"  Text="0.00" Style="text-align: right;" AutoPostBack=true
                                    onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <label class="col-sm-12 control-label style-label-1">Diesel</label>
                                <div class="col-sm-12">
                                <asp:TextBox ID="txtDiesel" runat="server" CssClass="form-control" OnTextChanged="Diesel_TextChanged" Text="0.00" Style="text-align: right;" AutoPostBack=true
                                    onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <label class="col-sm-12 control-label style-label-1">Driver A/C</label>
                                <div class="col-sm-12">
                                <asp:TextBox ID="txtDriverAc" runat="server" CssClass="form-control" OnTextChanged="DriverAc_TextChanged" Text="0.00" Style="text-align: right;" AutoPostBack=true
                                    onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                            <div class="col-sm-4 pull-right">
                                <label class="col-sm-12 control-label style-label-1">Net trip Profit</label>
                                <div class="col-sm-12">
                                <asp:TextBox ID="txtNetTripProfit" runat="server" CssClass="form-control"  Text="0.00" Style="text-align: right;" Enabled=false
                                    onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                </div>
                            </div>  
                            <div class="col-sm-4 pull-right">
                                <label class="col-sm-12 control-label style-label-1">Total Veh. Advance</label>
                                <div class="col-sm-12">
                                <asp:TextBox ID="txtTotalVehAdv" runat="server" CssClass="form-control"  Text="0.00" Style="text-align: right;" Enabled=false
                                    onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                </div>
                            </div>
                            </div>
                    </div>
                </section>
                 <div class="col-sm-12">
                    <div class="col-sm-2 pull-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkbtnCancel_OnClick" CssClass="btn full_width_btn btn-primary"  ToolTip="Click to Submit" CausesValidation="true" ValidationGroup="Submit" >Cancel</asp:LinkButton>
                    </div> 
                    <div class="col-sm-2 pull-right">
                        <asp:LinkButton ID="lnkbtnSubmit" runat="server" OnClick="lnkbtnSubmit_OnClick" CssClass="btn full_width_btn btn-primary"  ToolTip="Click to Submit" CausesValidation="true" ValidationGroup="Submit" >Save</asp:LinkButton>
                    </div>    
                </div>
            </form>
        </div>
            </section>
        </div>
    </div>
    <%--POPUP--%>
    <!-- popup for Amount  -->
    <div id="PrintManualTrip" class="modal fade">
        <div class="modal-dialog" style="width: 25%">
            <div class="modal-header">
                <h4 class="popform_header">
                    Print&nbsp;&nbsp;&nbsp;&nbsp;</h4>
            </div>
            <div class="modal-content">
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
					    <div class="panel-body">  
                            <div class="col-sm-12">
                            <div class="col-sm-12">
                            <span><b>With Vehicle Detail</b></span>
                            <asp:LinkButton ID="lnkwithvehicle" Text="Print" 
                                    class="btn btn-sm btn-primary pull-right" runat="server" TabIndex="45"  
                                    OnClick="lnkWithVehicle_click"></asp:LinkButton>
                                    </div>
                                    <div class="col-sm-12">
                            <span><b>Without Vehicle Detail</b></span>           
                            <asp:LinkButton ID="lnkwithoutvehicle" Text="Print" 
                                    class="btn btn-sm btn-primary pull-right" runat="server"  TabIndex="45" 
                                    OnClick="lnkWithoutVehicle_click"
                                    ></asp:LinkButton>
                                    </div>
                            </div>
                        </div>
                    </section>
                </div>
            </div>
        </div>
    </div>
    <!--HIDDEN FIELDS-->
    <asp:HiddenField ID="hidmindate" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
</asp:Content>
