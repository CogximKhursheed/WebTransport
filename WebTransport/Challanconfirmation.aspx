<%@ Page Title="Challan Confirmation" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="Challanconfirmation.aspx.cs" EnableEventValidation="false"
    Inherits="WebTransport.Challanconfirmation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-10">
            <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">CHALLAN CONFIRMATION
               <span class="view_print">
                </span>
                 </header>
                <div class="panel-body">
                  <form class="bs-example form-horizontal">
                    <!-- first  section --> 
                    <div class="clearfix first_section">
                      <section class="panel panel-in-default">  
                        <div class="panel-body">
                        	<div class="clearfix odd_row">
                            <div class="col-sm-4" style="width:39%">
                              <label class="col-sm-3 control-label" style="width:30%" >Date Range<span class="required-field">*</span></label>
                              <div class="col-sm-6" style="width:70%">
                                <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control"
                                    AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged">
                                </asp:DropDownList>
                              </div>
                            </div>
                           	<div class="col-sm-3" style="width:25%">
                           		<label class="col-sm-3 control-label" style="width:30%">Date<span class="required-field">*</span></label>
                              <div class="col-sm-5" style="width:65%">
                                <asp:TextBox ID="txtDate" runat="server"  TabIndex="2" PlaceHolder="DD-MM-YYYY" CssClass= "input-sm datepicker form-control" data-date-format="dd-mm-yyyy" onkeydown = "return DateFormat(this, event.keyCode)"></asp:TextBox>
                              <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Date!" SetFocusOnError="true"
                                ValidationGroup="save"></asp:RequiredFieldValidator>--%>
                              <%-- <asp:RangeValidator ID="RangeValidator1" CssClass="classValidation" ValidationGroup="save" runat="server" ErrorMessage="Date Is Invalid" Type="Date" ControlToValidate="txtDate" 
                                      Display="Dynamic" SetFocusOnError="true" ForeColor="Red"></asp:RangeValidator>--%>
                              </div>
                           	</div>
                           	<div class="col-sm-4" style="width:35%">
                              <label class="col-sm-3 control-label" style="width: 39%;">Location[From]<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 61%;">
                                <asp:DropDownList ID="ddlFromCity" runat="server" AutoPostBack="true" CssClass="form-control"
                                    TabIndex="3"  OnSelectedIndexChanged="ddlFromCity_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                              <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlFromCity"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select From city!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>--%>
                              </div>
                            </div>
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-4" style="margin: 8px 0;width:23%">
                                <span class="col-sm-4">Gr Type: </span>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlGrtype" Enabled="True"  runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="GrType_electedIndexChanged">
                                   <asp:ListItem Text="GR Prepation" Value="GR"></asp:ListItem>
                                   <asp:ListItem Text="GR Retailer" Value="GRR"></asp:ListItem>
                                   </asp:DropDownList> 
                                </div>
                            </div>
                            <div class="col-sm-1">
                               <div class="radio">
								    <label class="radio-custom" >
								    <asp:RadioButton ID="rdbtnGrNo" runat="server" TabIndex="4"
                                        Checked="true" AutoPostBack="true" GroupName="Challanrdb" OnCheckedChanged="rdbtnGrNo_CheckedChanged" />&nbsp;<b>By GR No.</b> 
								    </label>
							    </div>                              	
                            </div>
                            <div class="col-sm-2"  style="margin: 8px 0;">
                                <asp:TextBox ID="txtGrNo" runat="server" MaxLength="15" CssClass="form-control" 
                                  TabIndex="5" Style="text-align: right;" Enabled="false" onDrop="blur();"></asp:TextBox> 
                                   
                            </div>
                            <div class="col-sm-1">
                                <div class="radio">
								    <label for="radio-custom">
								    <asp:RadioButton ID="rdbtnChallanDetail" runat="server" TabIndex="7" AutoPostBack="true" GroupName="Challanrdb" OnCheckedChanged="rdbtnChallanDetail_CheckedChanged" />&nbsp;<b>Challan Details</b>
								    </label>
							    </div>                              	
                            </div>
                           	<div class="col-sm-2" style="margin: 8px 0;width: 30%;">
                            <asp:TextBox ID="txtChlnDetailAutoComplete" CssClass="form-control" onfocusout="ChallanOnFocusOut($(this))" onfocus="ChallanOnFocus($(this))" ClientIDMode="Static" runat="server"></asp:TextBox>

							<%--<asp:DropDownList ID="drpChallanDetail" runat="server" CssClass="form-control" TabIndex="8" OnSelectedIndexChanged="drpChallanDetail_SelectedIndexChanged"
                                    AutoPostBack="true" Enabled="false">
                                </asp:DropDownList>                               
                                <asp:RequiredFieldValidator ID="rfvdrpChallanDetail" runat="server" ControlToValidate="drpChallanDetail"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="save"
                                    class="classValidation" ErrorMessage="Select From Challan Detail"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="col-sm-1">
                                 <asp:LinkButton ID="lnktollNumber" runat="server"  class="btn btn-sm btn-primary acc_home" ToolTip="Add Toll Number" Visible="false" 
                                   data-toggle="modal"  data-target="#dvtollNumber" TabIndex="5"><img src="Images/plus.gif" style="width:15px;" /></asp:LinkButton>
	                         </div>
                            <div class="col-sm-1 prev_fetch" style="float: right;margin: 10px;">
                                <asp:LinkButton ID="lnkbtnSearch" class="btn full_width_btn btn-sm btn-primary" TabIndex="6"  ValidationGroup="save" OnClick="lnkbtnSearch_OnClick" runat="server">Search</asp:LinkButton>
                            </div>
                            <div class="col-sm-2" style="float: right; margin: 10px;">
                                <asp:CheckBox ID="chkRate" runat="server" Text="Default Rate"></asp:CheckBox>
                            </div>
                          </div>
                         </div>
                      </section>
        </div>
        <!-- third  section -->
        <div class="clearfix third_right">
            <section class="panel panel-in-default btns_without_border">                            
	                    <div class="panel-body" style="overflow-x:auto;">     
                        <asp:GridView ID="Gridmainhead" runat="server" AutoGenerateColumns="false"  CssClass="display nowrap dataTable" GridLines="None"
                            Width="100%" EnableViewState="true" AllowPaging="true"
                            ShowFooter="true" PageSize="30">
                            <RowStyle CssClass="odd" />
                            <AlternatingRowStyle CssClass="even" />    
                           <Columns>
                                <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="40" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Truck No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="left">
                                    <ItemStyle Width="50" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblitemname" runat="server" Text='<%#Eval("Truck_No")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Owner Name" HeaderStyle-Width="50" HeaderStyle-CssClass="gridHeaderAlignLeft">
                                    <ItemStyle Width="50" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty" runat="server" Text='<%#Eval("Owner_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <FooterTemplate>
                                        <asp:Label ID="lblqtytotal" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Driver Name" HeaderStyle-Width="50" HeaderStyle-CssClass="gridHeaderAlignLeft">
                                    <ItemStyle Width="50" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbluomname" runat="server" Text='<%#Eval("Driver_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="From City" HeaderStyle-Width="50" HeaderStyle-CssClass="gridHeaderAlignLeft">
                                    <ItemStyle Width="50" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblFromCity" runat="server" Text='<%#Eval("From_City")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delivery Instruction" HeaderStyle-Width="40" HeaderStyle-CssClass="gridHeaderAlignLeft">
                                    <ItemStyle Width="40" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <%#Convert.ToString(Eval("Delvry_Instrc"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
	                    </div>
	                </section>
        </div>
        <!-- fourth row -->
        <div class="clearfix third_right">
            <section class="panel panel-in-default btns_without_border">                            
	                          <div class="panel-body" style="overflow-x:auto;">     
                              <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" CssClass="display nowrap dataTable"
                                    Width="100%"  EnableViewState="true" AllowPaging="true" GridLines="None"
                                    ShowFooter="true"  OnRowCommand="grdMain_RowCommand"
                                    OnRowDataBound="grdMain_RowDataBound" PageSize="30">
                                    <RowStyle CssClass="odd" />
                                    <AlternatingRowStyle CssClass="even" />    
                                   <Columns>
                                    <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="40" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deliverd" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="left">
                                        <ItemStyle Width="30" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDelvrd" TabIndex="6" runat="server" Checked='<%#Convert.ToBoolean(Eval("Delivered")) %>'
                                                CommandName="cmdedit" OnCheckedChanged="chkDelvrd_CheckedChanged" AutoPostBack="true"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delivery Date" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="250px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:TextBox Width="90px" ID="txtdelvDate" TabIndex="7" runat="server" CssClass="input-sm datepicker form-control" Text='<%# Convert.IsDBNull(Eval("Delr_date"))? "" :Convert.ToDateTime(Eval("Delr_date")).ToString("dd-MM-yyyy") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblqtytotal" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shrtg" HeaderStyle-Width="30" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="30" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkShrtg" runat="server" TabIndex="8" Checked='<%#Convert.ToBoolean(Eval("Shrtg")) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From Km." HeaderStyle-Width="30" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="30" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                        <asp:TextBox Width="70px" ID="txtFromKm" TabIndex="9" runat="server"  Text='<%#Eval("From_KM")%>' OnTextChanged="txtFromKm_TextChanged" AutoPostBack="true"  ></asp:TextBox>
                                       </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Km." HeaderStyle-Width="30" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="30" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                        <asp:TextBox Width="70px" ID="txtToKm" TabIndex="9" runat="server"   Text='<%#Eval("To_KM")%>'  OnTextChanged="txtToKm_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                       </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Km." HeaderStyle-Width="30" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="30" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                        <asp:TextBox Width="70px" ID="txttotalKm" TabIndex="9" runat="server"  Text='<%#Eval("Tot_KM")%>' ></asp:TextBox>
                                       </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remark" HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="40" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemrk" Width="150px" TabIndex="10" runat="server" Text='<%#Eval("remark") %>' AutoPostBack="False"
                                                CssClass="form-control" MaxLength="100" TextMode="MultiLine"
                                                Style="resize: none;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GR No." HeaderStyle-Width="140" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="140" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrNo" runat="server" Text='<%#Eval("Gr_No")%>'></asp:Label>
                                            <asp:HiddenField ID="hidGrIdno" runat="server" Value='<%#Eval("Gr_Idno")%>' />
                                            <asp:HiddenField ID="hidInvIdno" runat="server" Value='<%#Eval("Inv_Idno")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="DI No." HeaderStyle-Width="30" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="30" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                        <asp:Label  ID="lblDINO"  runat="server"  Text='<%#Eval("DI_NO")%>' ></asp:Label>
                                       </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Tax Inv. No." HeaderStyle-Width="30" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="30" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                        <asp:Label  ID="lblTaxInvNo"  runat="server"  Text='<%#Eval("Tax_InvNo")%>' ></asp:Label>
                                       </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GR Date" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="80" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrDate" runat="server" Text='<%#Convert.ToDateTime(Eval("GR_Date")).ToString("dd-MM-yyyy") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Consignee" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRecvrName" runat="server" Text='<%#Eval("Recvr_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Consigner" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSenderName" runat="server" Text='<%#Eval("Sender_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Via City" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblViaCity" runat="server" Text='<%#Eval("Via_City")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To City" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblToCity" runat="server" Text='<%#Eval("To_City")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <b><asp:Label ID="lblTotal" Text="Total :" runat="server"></asp:Label></b>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="80 px" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="80 px" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblQty" runat="server" Text='<%#Eval("Qty")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <b><asp:Label ID="lblTotqy" runat="server"></asp:Label></b>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weight" HeaderStyle-Width="80 px" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="80 px" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbluomname" runat="server" Text='<%#Eval("Tot_Weght")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <FooterTemplate>
                                            <b><asp:Label ID="lblTotWeight" runat="server"></asp:Label></b>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Adv. Amount" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAdvAmount" runat="server" Text='<%#Eval("Adv_Amnt")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <b><asp:Label ID="lblAdvAmnt" runat="server"></asp:Label></b>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Diesel Amount" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDieselAmount" runat="server" Text='<%#Eval("Diesel_Amnt")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <b><asp:Label ID="lblDieselAmnt" runat="server"></asp:Label></b>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <b><asp:Label ID="lblNetAmnt" runat="server"></asp:Label></b>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                </asp:GridView>

	                          </div>
	                        </section>
        </div>
        <!-- fifth row -->
        <div class="clearfix fifth_right">
            <section class="panel panel-in-default btns_without_border">                            
                        <div class="panel-body">     
                          <div class="clearfix odd_row">
                            <div class="col-lg-3"></div>
                            <div class="col-lg-6">   
                            	<div class="col-sm-4">
                                <asp:LinkButton ID="lnkbtnNewMain" runat="server" Visible="false"  CssClass="btn full_width_btn btn-s-md btn-info" TabIndex="11" OnClick="lnkbtnNewMain_OnClick" ><i class="fa fa-file-o"></i>New</asp:LinkButton>
                              </div>                                         
                              <div class="col-sm-4">
                                <asp:LinkButton ID="lnkbtnSave" runat="server" CssClass="btn full_width_btn btn-s-md btn-success" TabIndex="12" OnClick="lnkbtnSave_OnClick" CausesValidation="true" ValidationGroup="save" > <i class="fa fa-save"></i>Save</asp:LinkButton>
                                <asp:HiddenField ID="hidChallanconfirmid" runat="server" Value="0" />
                                <asp:HiddenField ID="hidmindate" runat="server" Value="" />
                                <asp:HiddenField ID="hidmaxdate" runat="server" Value="" />
                                <asp:HiddenField ID="hidmindt" runat="server" Value="" />
                                <asp:HiddenField ID="hidRateEdit" runat="server" Value="" />
                                <asp:HiddenField ID="hidChallanIdno" runat="server" Value="" />
                              </div>
                              <div class="col-sm-4">
                                <asp:LinkButton ID="lnkbtnCancel" runat="server" CssClass="btn full_width_btn btn-s-md btn-danger" TabIndex="13" OnClick="lnkbtnCancel_OnClick" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
                              </div>
                            </div>
                            <div class="col-lg-3">
                                <button type="button" id="btnAccPost" runat="server" class="btn full_width_btn btn-s-md btn-info"  style="height: 32px" data-toggle="modal" data-target="#acc_posting">  <i class="fa fa-th-list">Acc Posting</i></button>
                            </div>
                          </div>
                        </div>
                      </section>
        </div>
        <div id="acc_posting" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="popform_header">
                        Acc Posting</h4>
                </div>
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
								    <div class="panel-body">
									    <!-- First Row start -->
								    <div class="clearfix odd_row">	                                
	                        <div class="col-sm-5">
	                            <label class="col-sm-3 control-label">From <span class="required-field">*</span></label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtIdFrom" runat="server" CssClass="form-control" Width="100px" c oncopy="return false"
                                        onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDivFrom" runat="server" ControlToValidate="txtIdFrom"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="From Required." 
                                    SetFocusOnError="true" ValidationGroup="Acc"></asp:RequiredFieldValidator> 
                            </div>
	                        </div>
	                        <div class="col-sm-4">
	                            <label class="col-sm-2 control-label">To <span class="required-field">*</span></label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtIdTo" runat="server" CssClass="form-control" Width="100px" oncopy="return false"
                                        onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDivTo" runat="server" ControlToValidate="txtIdTo"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="To Required." 
                                    SetFocusOnError="true" ValidationGroup="Acc"></asp:RequiredFieldValidator> 
                            </div>
	                        </div>
	                        <div class="col-sm-3" style="padding: 0;">
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
                        <asp:LinkButton ID="lnkbtnAccPosting"  OnClick="lnkbtnAccPosting_Click" ValidationGroup="Acc" 
                            class="btn btn-dark" runat="server">Acc Posting</asp:LinkButton>
                        <button type="submit" class="btn btn-dark" data-dismiss="modal">
                            <i class="fa fa-times"></i>Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
        <!-- popup form GR detail -->
        <div id="dvGrdetails" class="modal fade">
            <div class="modal-dialog" style="width: 50%">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="popform_header">
                            GR Details</h4>
                    </div>
                    <div class="modal-body" style="overflow-x: auto;">
                        <asp:GridView ID="grdGrdetals" runat="server" AutoGenerateColumns="false" CssClass="display nowrap dataTable"
                            Width="100%" OnRowDataBound="grdGrdetals_RowDataBound" EnableViewState="true"
                            AllowPaging="true" GridLines="None" ShowFooter="true" PageSize="30">
                            <RowStyle CssClass="odd" />
                            <AlternatingRowStyle CssClass="even" />
                            <Columns>
                                <asp:TemplateField HeaderText="GR No." HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrNo" runat="server" Text='<%#Convert.ToString(Eval("Gr_No"))%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GR Date" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MMM-yyyy")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="160px" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="180px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblOdrNo" runat="server" Text=' <%#Eval("Item_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Name" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <%#Eval("Unit_Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate Type" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRateType" runat="server" Text=' <%#Eval("Rate_Type")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblQty" runat="server" Text='<%#Eval("Qty")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Load Weight" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblWeghtKg" runat="server" Text='<%#Eval("Weght_Kg")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRate" runat="server" Text='<%#Eval("Rate")%>'></asp:Label>
                                        <asp:HiddenField ID="hidShortageGrIdno" runat="server" Value='<%#Eval("Gr_Idno")%>' />
                                        <asp:HiddenField ID="hidShrtgLimit" runat="server" Value='<%#Eval("Shrtg_Limit")%>' />
                                        <asp:HiddenField ID="hidShrtgRate" runat="server" Value='<%#Eval("Shrtg_Rate")%>' />
                                        <asp:HiddenField ID="hidShrtgLimitOther" runat="server" Value='<%#Eval("Shrtg_Limit_Other")%>' />
                                        <asp:HiddenField ID="hidShrtgRateOther" runat="server" Value='<%#Eval("Shrtg_Rate_Other")%>' />
                                        <asp:HiddenField ID="hidRateType" runat="server" Value='<%#Eval("RateType_Idno")%>' />
                                        <asp:HiddenField ID="hidShrtgItemRate" runat="server" Value='<%#Eval("ItemShrtg_Rate")%>' />
                                        <asp:HiddenField ID="hidShrtgLimitDefault" runat="server" Value='<%#Eval("Shrtg_Limit")%>' />
                                        <asp:HiddenField ID="hidShrtgRateDefault" runat="server" Value='<%#Eval("Shrtg_Rate")%>' />
                                        <asp:HiddenField ID="hidUnloadWeight" runat="server" Value='<%#Eval("UnloadWeight")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="180px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <%#Eval("Amount")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shrtg Type" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlShrtgType" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlShrtgType_SelectedIndexChanged">
                                            <asp:ListItem Text="Rate" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Weight" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shrtg Limit" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtShrtgLim" Text='<%#Eval("Shrtg_Limit")%>' runat="server" Width="60px"
                                            Style="text-align: right;" CssClass="form-control" OnTextChanged="txtShrtgLim_changed"
                                            AutoPostBack="true" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shrtg Rate" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtShrtgRate" Text='<%#Eval("Shrtg_Rate")%>' runat="server" Width="60px"
                                            Style="text-align: right;" CssClass="form-control" OnTextChanged="txtShrtgRate_changed"
                                            AutoPostBack="true" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Unload Weight" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtUnloadWeight" Text='<%#Eval("UnloadWeight")%>' runat="server" Width="60px" OnTextChanged="txtUnloadWeight_TextChanged"
                                            Style="text-align: right;" CssClass="form-control" 
                                            AutoPostBack="true" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Shortage" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtShortage" Text='<%#Eval("ShrtgType")%>' runat="server" Width="60px"
                                            Style="text-align: right;" CssClass="form-control" OnTextChanged="txtShortage_changed"
                                            AutoPostBack="true" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rcpt Total" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDiff" Text='<%#Eval("shortage_Diff")%>' runat="server" Width="60px"
                                            Style="text-align: right;" CssClass="form-control" OnTextChanged="txtDiff_changed"
                                            AutoPostBack="true" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                        <asp:HiddenField ID="hidGrIdno" runat="server" Value='<%#Eval("Gr_Idno")%>' />
                                        <asp:HiddenField ID="HidShrtgInvIdno" runat="server" Value='<%#Eval("Inv_Idno")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shrtg. Amnt." HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="200px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblShortageAmnt" runat="server" Text='<%#Eval("shortage_Amount")%>'
                                            Width="60px" Style="text-align: right;" CssClass="form-control" AutoPostBack="false"
                                            onkeydown="return (event.keyCode!=13);"></asp:TextBox><asp:HiddenField ID="hidGrDetlIdno"
                                                runat="server" Value='<%#Eval("GrDetl_Idno")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="clearfix fifth_right">
                        <section class="panel panel-in-default btns_without_border">                            
                                    <div class="panel-body">     
                                      <div class="clearfix odd_row">
                                        <div class="col-lg-5"></div>
                                        <div class="col-lg-6">   
                            	            <div class="col-sm-4">
									            <asp:LinkButton ID="lnkBtnSaveShortage" runat="server" CssClass="btn full_width_btn btn-s-md btn-success" TabIndex="17" OnClick="lnkBtnSaveShortage_OnClick" CausesValidation="true" > <i class="fa fa-save"></i>Save</asp:LinkButton>
                                            </div>
                                        </div>
                                        </div>
                                      </div>
                                   </section>
                    </div>
                </div>
                </form>
            </div>
            </section>
        </div>
        <div class="col-lg-1">
        </div>
    </div>
    <!-- popup form toll detail -->

    <div id="dvtollNumber" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="popform_header">Toll Tax  Detail </h4>
                </div>
               <%-- <asp:UpdatePanel ID="uphg" runat="server">
                    <ContentTemplate>--%>
                        <div class="modal-body">
                            <section class="panel panel-default full_form_container material_search_pop_form">
									<div class="panel-body">
										<!-- First Row start -->
									<div class="clearfix odd_row">	                                
	                                <div class="col-sm-8">
	                                    <label class="col-sm-4 control-label">Toll Tax Name</label>
                                        <div class="col-sm-8">
                                            <asp:HiddenField ID="hidrowid" runat="server" />
                                            <asp:HiddenField ID="hidtollTaxID" runat="server" />
                                            <asp:TextBox ID="txtTollTax" runat="server" CssClass="glow form-control auto-extender" onkeyup="SetContextKey()" onkeydown="return (event.keyCode!=13);" AutoPostBack="true" OnTextChanged="txtTollTax_TextChanged"  TabIndex="5"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtTollTax" MinimumPrefixLength="1" UseContextKey="false" EnableCaching="true" CompletionSetCount="1" CompletionInterval="500" OnClientItemSelected="ClientItemSelected" ServiceMethod="GettollTaxNumber">
                                            </asp:AutoCompleteExtender>
                                        </div>
	                                </div>
	                                <div class="col-sm-4">
	                                    <label class="col-sm-4 control-label">Toll Amount</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txttollAmnt" runat="server" Text="0" CssClass="form-control" TabIndex="2" ></asp:TextBox>
                                        </div>
	                                </div>
	                              </div> 
                                <div class="clearfix even_row">
	                                <div class="col-sm-6">
	                                	<label class="col-sm-4 control-label">Ticket No</label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txtTktNo" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                    </div>
	                                </div>
	                                <div class="col-sm-6" style="padding: 0;">
	                                  <div class="col-sm-4 prev_fetch">
                                    <asp:LinkButton ID="lnkbtnsubmit" CssClass="btn full_width_btn btn-sm btn-primary"  TabIndex="88" runat="server" CausesValidation="true" ValidationGroup="RcptEntrySrch" OnClick="lnkbtnsubmit_Click" >Submit</asp:LinkButton>
	                               </div>
	                                  <div class="col-sm-8"> 
                                      &nbsp;
	                                </div>
	                                </div>
	                              </div> 
	                             <div class="clearfix third_right">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body" style="overflow:auto; height:400px;">     
	                            <asp:GridView ID="grdmaintoll" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="display nowrap dataTable"
                                    Width="100%" BorderStyle="None" AllowPaging="false" PageSize="100" BorderWidth="0" OnRowCommand="grdmaintoll_RowCommand">
                                 <RowStyle CssClass="odd" />
                                 <AlternatingRowStyle CssClass="even" />    
                                    <Columns>
                                       <asp:TemplateField HeaderText="Toll Tax Number" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                               <asp:Label ID="lbltollNo" runat="server" Text='<%#Eval("Toll_Name")%>'></asp:Label>
                                                 <asp:HiddenField ID="hidTollId" runat="server" Value='<%#Eval("Toll_Idno")%>' />
                                                  <asp:HiddenField ID="hidChallanIdno"  runat="server" Value='<%#Eval("Chln_Idno")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount " HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                              <asp:Label ID="lblamount" runat="server" Text='<%#Eval("Toll_Amt")%>'></asp:Label>
                                                  <asp:HiddenField ID="hidGrIdno" runat="server" Value='<%#Eval("Gr_Idno")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px" HeaderText="Ticket No">
                                            <ItemStyle HorizontalAlign="Left" Width="180px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblticket" runat="server" Text='<%#Eval("Ticket_No")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="40" 
                                            HeaderText="Action">
                                            <ItemStyle HorizontalAlign="Center" Width="40" />
                                            <ItemTemplate>  
                                           <asp:LinkButton ID="LinkEdit" class="fa fa-edit icon" CommandName="cmdedittoll" CommandArgument='<%#Eval("id")%>'
                                                                AlternateText="Edit" ToolTip="Edit"  runat="server"></asp:LinkButton>

                                           <asp:LinkButton ID="LinkDelete" class="fa fa-trash-o" CommandName="cmddeletetoll" CommandArgument='<%#Eval("id")%>'
                                                                AlternateText="Delete" ToolTip="Delete" OnClientClick="return confirm('Do you want to delete this record ?');"  runat="server"></asp:LinkButton>
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
                                 <div class="modal-footer">
                                    <div class="popup_footer_btn">
                                        <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-dark"  OnClick="lnkbtnOk_Click"
                                            TabIndex="89"><i class="fa fa-check"></i>Save</asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnClose" runat="server" CssClass="btn btn-dark" data-dismiss="modal"><i class="fa fa-times"></i>Close</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </section>
                        </div>
                       <asp:HiddenField ID="hidGrIdno" runat="server" />
                      <asp:HiddenField ID="hidTollId" runat="server" />
                   <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hidChallanDetail" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hidModalStatus" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hidpostingmsg" runat="server" />
    <asp:HiddenField ID="ChallanNumber" runat="server" />
    <asp:HiddenField ID="hidPrintType" runat="server" />

    <script language="javascript" type="text/javascript">

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function () {

            setDatecontrol();
        });

        prm.add_endRequest(function () {

            setDatecontrol();
        });

        $(document).ready(function () {
            setDatecontrol();
        });

        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            var mindt = $('#<%=hidmindt.ClientID%>').val();

            $("#<%=txtDate.ClientID %>").date({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindt,
                maxDate: maxdate
            });

            $("input[type=text][id*=txtdelvDate]").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindt
            });
        }
        function datecontrol() {
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            var mindt = $('#<%=hidmindt.ClientID%>').val();

            $("input[type=text][id*=txtdelvDate]").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindt
            });
        }
        dvGrdetails

        function openModal() {
            $('#dvGrdetails').modal('show');
        }

        function CloseModal() {
            $('#dvGrdetails').Hide();
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
        var a = $('#hidModalStatus').val();
        if (parseInt(a) == 1 || parseInt(a) < 0) {
            openModal();
        }
        //$(document).ready(function () {
        $(".datepicker").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
            minDate: '<%=hidmindt.Value%>',
            maxDate: '<%=hidmaxdate.Value%>'
        });
        $(".datepicker").date({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
            minDate: '<%=hidmindate.Value%>',
            maxDate: '<%=hidmaxdate.Value%>'
        });
        //});
        function ChallanOnFocusOut(ele) {
            if ($('#hidChallanDetail').val() != '' && ele.val() == '') ele.val($('#hidChallanDetail').val());
        }

        function ChallanOnFocus(ele) {
            if (ele.val() != '') {
                $('#hidChallanDetail').val(ele.val());
                ele.val('');
            }
        }

        function openModalT() {
            $('#dvtollNumber').modal('show');
        }
        function CloseModalT() {
            $('#dvtollNumber').Hide();
        }

        $(document).load(function () {
            var a = $('#hidModalStatus').val();
            if (parseInt(a) == 1 || parseInt(a) < 1) {
                openModal();
            }
        });
    </script>
    <script type="text/javascript">
        function ClientItemSelected(sender, e) {
            $get("<%=hidtollTaxID.ClientID %>").value = e.get_value();
        }
    </script>
    
</asp:Content>
