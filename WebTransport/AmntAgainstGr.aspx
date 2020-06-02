<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AmntAgainstGr.aspx.cs" Inherits="WebTransport.AmntAgainstGr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="row ">
                <div class="col-lg-1">
                </div>
                <div class="col-lg-10">
                    <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">AMOUNT RECEIVED AGAINST GR
                <span class="view_print"><a href="ManageAmntRcvdGr.aspx"><asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a>&nbsp;
                <asp:LinkButton ID="lnkbtnPrint" CssClass="fa fa-print icon" Visible="false" runat="server" AlternateText="Print" title="Print" Height="16px" OnClientClick="return CallPrint('print');"></asp:LinkButton>
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
                                <asp:DropDownList ID="ddldateRange" runat="server" AutoPostBack="true" CssClass="form-control"
                                    TabIndex="1" OnSelectedIndexChanged="ddldateRange_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddldateRange"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Year!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 29%;">Receipt No.<span class="required-field">*</span></label>

                              <div class="col-sm-3" style="width: 40%;">
                              	  <asp:TextBox ID="txtRcptNo" runat="server" CssClass="form-control" Height="24px" MaxLength="50"
                                        oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                        TabIndex="2" Text="" Width="110" Enabled="False"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRcptNo"
                                        CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Rcpt no!"
                                        SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>

                              <div class="col-sm-4" style="width: 31%;">
                                <asp:TextBox ID="txtGRDate" runat="server" CssClass="input-sm datepicker form-control" Height="24px" MaxLength="50"
                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                    Style="width: 100px;" TabIndex="3" Text=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGRDate"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Date!"
                                    SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>
                           	</div>
                           	<div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 37%;">Location[From]<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 63%;">
                                <asp:DropDownList ID="ddlFromCity" runat="server" CssClass="form-control" 
                                    TabIndex="4"  OnSelectedIndexChanged="ddlFromCity_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlFromCity"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select From city!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>
                              
                            </div>
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 30%;">Party Name<span class="required-field">*</span></label>
                              <div class="col-sm-8" style="width: 58%;">
                                <asp:DropDownList ID="ddlPartyName" runat="server" AutoPostBack="True" CssClass="form-control"
                                     TabIndex="5"  OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlPartyName" runat="server" ControlToValidate="ddlPartyName"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Party Name!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>
                              <div class="col-sm-1" style="width: 10%;">
                                <asp:LinkButton ID="lnkimgSearch" Height="25px" ToolTip="Gr Details"  runat="server" OnClick="lnkimgSearch_Click" TabIndex="6" class="btn btn-sm btn-primary acc_home" ><i class="fa fa-file"></i></asp:LinkButton>
                              </div>
                            </div>
                           	<div class="col-sm-3" style="width:34%">
                           		<label class="col-sm-3 control-label" style="width: 34%;">Rcpt. Type<span class="required-field">*</span></label>
                              <div class="col-sm-5" style="width: 62%;">
                                <asp:DropDownList ID="ddlRcptTyp" runat="server" AutoPostBack="true" CssClass="form-control"
                                     TabIndex="7"  OnSelectedIndexChanged="ddlRcptTyp_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlRcptTyp" runat="server" ControlToValidate="ddlRcptTyp"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Rcpt Type!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>
                           	</div>
                           	<div class="col-sm-4" style="width:31%">
                           		<label class="col-sm-3 control-label" style="width: 37%;">Inst.Details</label>

                              <div class="col-sm-3" style="width: 32%;">
                              	<asp:TextBox ID="txtInstNo" runat="server" CssClass="form-control" Width="72px" MaxLength="6"
                                    Style="text-align: right;" TabIndex="8"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvinstno" runat="server" ControlToValidate="txtInstNo"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="" ValidationGroup="save"
                                    ErrorMessage="Enter Inst. No" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>

                              <div class="col-sm-4" style="width: 31%;">
                                <asp:TextBox ID="txtInstDate" runat="server" CssClass="input-sm datepicker form-control"  TabIndex="9"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvinstDate" runat="server" ControlToValidate="txtInstDate"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="" ValidationGroup="save"
                                    ErrorMessage="<br>Enter Inst. Date" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                           	</div>
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 29%;">Cust.Bank<span class="required-field">*</span></label>
                              <div class="col-sm-8" style="width: 71%;">
                                <asp:DropDownList ID="ddlCustmerBank" runat="server" CssClass="form-control" 
                                    TabIndex="10" >
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCusBank" runat="server" ControlToValidate="ddlCustmerBank"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Cust Bank!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                           	<div class="col-sm-8">
                           		<label class="col-sm-3 control-label" style="width: 14.5%;">Remark</label>
                              <div class="col-sm-9" style="width: 85.5%;">
                                <asp:TextBox ID="TxtRemark" runat="server" CssClass="form-control parsley-validated"  MaxLength="200"
                                        oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                        TabIndex="11" Text="" Width="529px" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                              </div>
                           	</div>
                          </div>

                        </div>
                      </section>                        
                    </div>
                    
                    <!-- second  section -->
                   <div class="clearfix third_right">
                          <section class="panel panel-in-default">                            
                            <div class="panel-body" style="overflow: auto">     
                               <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"
                                        Width="100%" GridLines="None" EnableViewState="true" AllowPaging="true" BorderWidth="0"
                                        ShowFooter="true" OnPageIndexChanging="grdMain_PageIndexChanging" OnDataBound="grdMain_DataBound"
                                        OnRowDataBound="grdMain_RowDataBound" ViewStateMode="Enabled" PageSize="30">
                                    <RowStyle CssClass="odd" />
                                    <AlternatingRowStyle CssClass="even" />    
                                      <Columns>
                                        <asp:TemplateField HeaderText="Gr No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrno" runat="server" Text='<%#Convert.ToString(Eval("Gr_No"))%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gr Date" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrDate" runat="server" Text='<%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Receiver Name" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("Recivr_Name"))%>
                                                <asp:HiddenField ID="hidRecivrIdno" runat="server" Value='<%#Eval("Recivr_Idno")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GR From" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrFrom" runat="server" Text='<%#Eval("GR_From")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To City" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Eval("To_City")%>
                                                <asp:HiddenField ID="hidToCityIdno" runat="server" Value='<%#Eval("ToCity_Idno")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="From City" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Eval("From_City")%>
                                                <asp:HiddenField ID="hidGrIdno" runat="server" Value='<%#Eval("Gr_Idno")%>' />
                                                <asp:HiddenField ID="hidFromCityIdno" runat="server" Value='<%#Eval("FromCity_Idno")%>' />
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal" Text="Total :" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                            <ItemStyle Width="50" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Amount")))%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Prev.Rcvd.Amount" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                            <ItemStyle Width="50" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotRecvd" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Tot_Recvd")))%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblFTotRecvd" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Prev.Bal" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                            <ItemStyle Width="50" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurBal" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("cur_Bal")))%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblFTotCurBal" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Recv.Amount" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                            <ItemStyle Width="100" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRcvdAmnt" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Recv_Amount")))%>'
                                                    OnTextChanged="txt_txtRcvdAmnt" AutoPostBack="true" EnableViewState="true" ViewStateMode="Enabled"
                                                    Width="90px"></asp:TextBox>
                                            </ItemTemplate>
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
                            <div class="col-sm-9"></div>
                           	<div class="col-sm-3">
                              <label class="col-sm-5 control-label">Net Amount</label>
                              <div class="col-sm-7">
                                <asp:TextBox ID="txtNetAmnt" runat="server" CssClass="form-control" Enabled="false"
                                        Height="24px" MaxLength="50" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                        onpaste="return false" TabIndex="13" ReadOnly="true" Text="0.00"></asp:TextBox>
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
                        <div class="col-sm-4">                                       
                        <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-info" OnClick="lnkbtnNew_OnClick" TabIndex="24"><i class="fa fa-file-o"></i>New</asp:LinkButton>
                        </div>
                        <div class="col-sm-4">
                          <asp:LinkButton ID="lnkbtnSave" runat="server" CssClass="btn full_width_btn btn-s-md btn-success" TabIndex="17" OnClick="lnkbtnSave_OnClick" CausesValidation="true" ValidationGroup="save" > <i class="fa fa-save"></i>Save</asp:LinkButton>
                          <asp:HiddenField ID="hidid" runat="server" Value="" />
                            <asp:HiddenField ID="Hidrowid" runat="server" Value="" />
                            <asp:HiddenField ID="hidmindate" runat="server" />
                            <asp:HiddenField ID="hidmaxdate" runat="server" />
                            <asp:HiddenField ID="hidpostingmsg" runat="server" />
                        </div>
                        <div class="col-sm-4">
                          <asp:LinkButton ID="lnkbtnCancel" runat="server" CssClass="btn full_width_btn btn-s-md btn-danger" TabIndex="18" OnClick="lnkbtnCancel_OnClick" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
                        </div>
                      </div>
                <div class="col-lg-3"></div>
            </div>

                    <!-- popup form GR detail -->
						<div id="gr_details_form" class="modal fade">
							<div class="modal-dialog">
							<div class="modal-content">
								<div class="modal-header">
								<h4 class="popform_header">GR Detail </h4>
								</div>
								<div class="modal-body">
								<section class="panel panel-default full_form_container material_search_pop_form">
							<div class="panel-body">
								<!-- First Row start -->
							    <div class="clearfix odd_row">	                                
	                                <div class="col-sm-6">
	                                    <label class="col-sm-4 control-label">Date From</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="input-sm datepicker form-control" TabIndex="85"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtFrm" runat="server" ErrorMessage="Enter From Date!"
                                            Display="Dynamic" CssClass="redfont" ControlToValidate="txtDateFrom" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                    </div>
	                                </div>
	                                <div class="col-sm-6">
	                                    <label class="col-sm-4 control-label">Date To</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtDateTo" runat="server" CssClass="input-sm datepicker form-control" TabIndex="86"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtTo" runat="server" ErrorMessage="Enter To Date!"
                                            Display="Dynamic" CssClass="redfont" ControlToValidate="txtDateTo" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                    </div>
	                                </div>
	                            </div> 
                                <div class="clearfix even_row">
	                            <div class="col-sm-6"></div>
	                            <div class="col-sm-6" style="padding: 0;">
	                                <div class="col-sm-4 prev_fetch">
	                                <asp:LinkButton ID="lnkbtnSearch" class="btn full_width_btn btn-sm btn-primary" TabIndex="87" ValidationGroup="RcptEntrySrch" OnClick="lnkbtnSearch_Click"
                                                    runat="server"><i class="fa fa-search"></i>Search</asp:LinkButton>
	                                </div>
	                                <div class="col-sm-8"> 
	                                    <asp:Label ID="lblTotalRecord" runat="server" Text="T. Record(s) : 0" CssClass="control-label" ></asp:Label>
	                                </div>
	                            </div>
	                            </div> 
	                            <div class="clearfix third_right">
                                  <section class="panel panel-in-default">                            
                                    <div class="panel-body"> 
                                    <div style="overflow:auto;">
                                     <asp:GridView ID="grdGrdetals" runat="server" AlternatingRowStyle-CssClass="gridRow"
                                        AutoGenerateColumns="false" BorderStyle="None" BorderWidth="0" GridLines="None"
                                        RowStyle-CssClass="gridAlternateRow" Width="100%">
                                        <HeaderStyle CssClass="linearBg" ForeColor="Black" />
                                        <Columns>
                                        <asp:TemplateField HeaderText="Select" HeaderStyle-Width="40px">
                                            <HeaderStyle Width="40" HorizontalAlign="Center" />
                                            <ItemStyle Width="40" HorizontalAlign="Center" />
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAllCheckboxes(this);"
                                                    CssClass="SACatA" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkId" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gr No." HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("Gr_No"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gr Date" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MMM-yyyy")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Receiver Name" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="150px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("Recivr_Name"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="130px" HeaderText="GR From ">
                                            <ItemStyle HorizontalAlign="Left" Width="130px" />
                                            <ItemTemplate>
                                                <%#Eval("GR_From")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="130px" HeaderText="From  City">
                                            <ItemStyle HorizontalAlign="Left" Width="130px" />
                                            <ItemTemplate>
                                                <%#Eval("From_City")%>
                                                <asp:HiddenField ID="hidGrIdno" runat="server" Value='<%#Eval("GR_Idno")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="130px" HeaderText="To City">
                                            <ItemStyle HorizontalAlign="Left" Width="130px" />
                                            <ItemTemplate>
                                                <%#Eval("To_City")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"
                                            HeaderText="GR Amnt">
                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                            <ItemTemplate>
                                                <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Amount")))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"
                                            HeaderText="Tot.RcvdAmnt">
                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                            <ItemTemplate>
                                                <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Tot_Recvd")))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"
                                            HeaderText="Cur.Bal">
                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                            <ItemTemplate>
                                                <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("cur_Bal")))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"
                                            HeaderText="Recvd.Amnt">
                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                            <ItemTemplate>
                                                <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Recv_Amount")))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    </asp:GridView>
                                    </div>
                                </div>
                                </section>
                                </div>
	                              
									</div>
								</section>
								</div>
								<div class="modal-footer">
								<div class="popup_footer_btn"> 
									<asp:LinkButton ID="lnkbtnsubmit" OnClick="lnkbtnsubmit_Click" class="btn btn-dark" TabIndex="88" runat="server">Submit</asp:LinkButton>
                                    <asp:LinkButton ID="lnkbtnClear" OnClick="lnkbtnClear_Click" class="btn btn-dark" TabIndex="89" runat="server"><i class="fa fa-times"></i>Close</asp:LinkButton>
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

            <div id="print" style="font-size: 13px; display:none;">
                   <table cellpadding="1" cellspacing="0" width="900" border="1" style="font-family: Arial,Helvetica,sans-serif;">
                    <tr>
                        <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                            border-left-style: none; border-right-style: none">
                            <strong>
                                <asp:Label ID="lblCompanyname1" runat="server" Style="font-size: 18px;"></asp:Label><br />
                            </strong>
                            <asp:Label ID="lblCompAdd11" runat="server"></asp:Label>
                            &nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblCompAdd22" runat="server"></asp:Label><br />
                            <asp:Label ID="lblCompCity1" runat="server"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblCompState1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblCompCityPin1" runat="server"></asp:Label><br />
                            PH:
                            <asp:Label ID="lblCompPhNo1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblFaxNo1" Text="FAX No.:" runat="server"></asp:Label>
                            <asp:Label ID="lblCompFaxNo1" runat="server"></asp:Label><br />
                            <asp:Label ID="lblTin1" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                                ID="lblCompTIN1" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                            border-left-style: none; border-right-style: none">
                            <h3>
                                <strong style="text-decoration: underline">
                                    <asp:Label ID="Label19" runat="server" Text="Amount Received Against Gr"></asp:Label></strong></h3>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table border="0" width="100%">
                                <tr>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none" width="10%">
                                        <asp:Label ID="lbltxtgrno" Text="Receipt No." runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <b>
                                            <asp:Label ID="lblGRno" runat="server"></asp:Label></b>
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none" width="10%">
                                        <asp:Label ID="lbltxtgrdate" Text="Receipt Date" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none" width="10%">
                                        <b>
                                            <asp:Label ID="lblGrDate" runat="server"></asp:Label></b>
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none" width="10%">
                                        <asp:Label ID="lbltxtPartyName" Text="Party Name" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <b>
                                            <asp:Label ID="valuelbltxtPartyName" runat="server"></asp:Label></b>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table4">
                                <asp:Repeater ID="Repeater2" runat="server"  OnItemDataBound="Repeater2_ItemDataBound">
                                    <HeaderTemplate>
                                        <tr>
                                            <td class="white_bg" style="font-size: 12px" width="3%">
                                                <strong>S.No.</strong>
                                            </td>
                                            <td style="font-size: 12px" width="5%">
                                                <strong>GR.No.</strong>
                                            </td>
                                            <td style="font-size: 12px" width="10%">
                                                <strong>GR. Date</strong>
                                            </td>
                                            <td style="font-size: 12px" width="13%">
                                                <strong>Receiver. Name</strong>
                                            </td>
                                            <td style="font-size: 12px" width="12%">
                                                <strong>Gr From</strong>
                                            </td>
                                            <td style="font-size: 12px" width="9%">
                                                <strong>To City</strong>
                                            </td>
                                            <td style="font-size: 12px" align="left" width="10%">
                                                <strong>From City</strong>
                                            </td>
                                            <td style="font-size: 12px" align="right" width="6%">
                                                <strong>Amount</strong>
                                            </td>
                                            <td style="font-size: 12px" align="right" width="12.5%">
                                                <strong>Pre. Recv. Amnt</strong>
                                            </td>
                                            <td style="font-size: 12px" align="center" width="8.5%">
                                                <strong>Pre. Bal.</strong>
                                            </td>
                                            <td style="font-size: 12px" align="right" width="12.5%">
                                                <strong>Recv. Amount.</strong>
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="white_bg" width="3%">
                                                <%#Container.ItemIndex+1 %>.
                                            </td>
                                            <td class="white_bg" width="7%">
                                                <%#Eval("GR_No")%>
                                            </td>
                                            <td class="white_bg" width="10%">
                                                <%#string.IsNullOrEmpty(Convert.ToString(Eval("GR_Date"))) ? "" : Convert.ToDateTime(Eval("GR_Date")).ToString("dd-MM-yyyy")%>
                                            </td>
                                            <td class="white_bg" width="10%">
                                                <%#Eval("Recivr_Name")%>
                                            </td>
                                            <td class="white_bg" width="11%">
                                                <%#Eval("GR_From")%>
                                            </td>
                                            <td class="white_bg" width="5%">
                                                <%#Eval("To_City")%>
                                            </td>
                                                <td class="white_bg" width="5%" align="left">
                                                <%#Eval("From_City")%>
                                            </td>
                                            <td class="white_bg" width="6%" align="right">
                                                <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%>
                                            </td>
                                                <td class="white_bg" width="12.5%%" align="right">
                                                <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Tot_Recvd")))%>
                                            </td>
                                                <td class="white_bg" width="8.5%%" align="center">
                                                <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("cur_Bal")))%>
                                            </td>
                                                <td class="white_bg" width="12.5%%" align="right">
                                                <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Recv_Amount")))%>
                                            </td>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table5">
                                <tr>
                                    <td class="white_bg" width="5%">
                                    </td>
                                    <td class="white_bg" width="10%">
                                    </td>
                                    <td class="white_bg" width="20%" align="right">
                                        <asp:Label ID="lblttl" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="5%" align="right">
                                            <asp:Label ID="Label2" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="6%" align="center">
                                        <asp:Label ID="lblPrintTotAmnt" Font-Bold="true" Text="" runat="server"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="4%" align="center">
                                        <asp:Label ID="lblTorPrintRecvd" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="12.5%" align="left" colspan="3">
                                            <asp:Label ID="lblPrintCurBal" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                                          
                                                      
                                        <td class="white_bg" width="4%%" align="center">
                                        <asp:Label ID="lblPrintRecvd" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="white_bg" colspan="9" width="15%">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="white_bg" width="15%">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="15%">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="15%" align="center">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="15%" align="left">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="12.5%">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="12.5%">
                                        &nbsp;
                                    </td>
                                    <td class="white_bg" width="15%" align="right" colspan="2">
                                        <asp:Label ID="lblnetamntAtbttm" Font-Bold="true" Text="Net Amnt." runat="server"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="20%" align="right" colspan="2" >
                                        <asp:Label ID="valuelblnetamntAtbttm" Font-Bold="true" runat="server"></asp:Label>
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
                                                    <b>Customer Signature</b>&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td align="right" class="white_bg" style="font-size: 13px" valign="top" width="50%">
                                                                     
                                                    <b>
                                                        <asp:Label ID="lblCompname1" runat="server"></asp:Label><br />
                                                                          
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


         <%--   <table width="100%">
                <tr>
                    <td align="left" valign="top" class="header_bt_bg">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="29">
                                    &nbsp;
                                </td>
                                <td align="left" valign="bottom">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="left" valign="top" style="padding-top: 3px;">
                                                <!-- Breadcrumb -->
                                                <table border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td width="5">
                                                            &nbsp;
                                                        </td>
                                                        <td class="orange12">
                                                            <asp:Literal ID="Literal1" runat="server"></asp:Literal><span><b> <a href="Menus.aspx">
                                                                <span class="orange12">Home</span> </a><span>
                                                                    <img src='images/black_arrow.gif' alt="" /></span><asp:Label ID="lblbreadcrum" runat="server"
                                                                        Text=" Amount Received Agnst GR "></asp:Label></b></span>
                                                        </td>
                                                        <td class="gray11">
                                                        </td>
                                                    </tr>
                                                </table>
                                                <!-- Breadcrumb -->
                                            </td>
                                            <td align="left" valign="top">
                                                &nbsp;
                                            </td>
                                            <td align="right" valign="top" style="padding-top: 1px;">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="27">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="980" border="0" align="center" cellpadding="0" cellspacing="0" id="tblAuthorize"
                            runat="server" class="ibdr">
                            <tr>
                                <td>
                                    <table width="978px" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
                                        class="ibdr">
                                        <tr>
                                            <td height="39" align="left" background="images/grd_top_bg.jpg" class="title06">
                                                &nbsp;&nbsp;&nbsp;Amount Received Against GR
                                            </td>
                                            <td height="39" align="right" background="images/grd_top_bg.jpg" class="title06">
                                                <a href="ManageAmntRcvdGR.aspx" tabindex="27">
                                                    <asp:Label ID="lblViewList" runat="server" Text="View List&nbsp;&nbsp;"></asp:Label></a>
                                                     <asp:ImageButton ID="imgPrint" runat="server" AlternateText="Print" ImageUrl="~/images/print.jpeg"
                                                    Visible="false" title="Print" OnClientClick="return CallPrint('print');" Height="16px" />
                                            </td>
                                        </tr>
                                        <table align="center" border="0" cellpadding="0" cellspacing="0" width="975px">
                                            <tr>
                                                <td align="left" bgcolor="#E8F2FD" class="btn_01" height="40px" width="10%">
                                                    <span class="txt"><span class="red" style="color: #ff0000">&nbsp;&nbsp;</span> </span>
                                                    Date Range<span class="redfont1">*</span>
                                                </td>
                                                <td align="left" bgcolor="#E8F2FD" height="40px" valign="middle">
                                                    <asp:DropDownList ID="ddldateRange" runat="server" AutoPostBack="true" CssClass="glow"
                                                        Height="30px" TabIndex="1" Width="190px" OnSelectedIndexChanged="ddldateRange_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddldateRange"
                                                        CssClass="redfont" Display="Dynamic" ErrorMessage="&lt;br /&gt; Please select Year!"
                                                        InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                </td>
                                                <td align="left" bgcolor="#E8F2FD" class="btn_01" height="40px">
                                                    Receipt No<span class="redfont">*</span>
                                                </td>
                                                <td align="left" bgcolor="#E8F2FD" height="40px" valign="middle" width="280px">
                                                    <table width="100%">
                                                        <tr>
                                                            <td valign="middle">
                                                                <asp:TextBox ID="txtRcptNo" runat="server" CssClass="glow" Height="24px" MaxLength="10"
                                                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                                    TabIndex="3" Text="" Width="95px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRcptNo"
                                                                    CssClass="redfont" Display="Dynamic" ErrorMessage="&lt;br/&gt;Please enter Rcpt no!"
                                                                    SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td valign="middle">
                                                                <asp:TextBox ID="txtGRDate" runat="server" CssClass="glow" Height="24px" MaxLength="50"
                                                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                                    Style="width: 100px;" TabIndex="2" Text=""></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGRDate"
                                                                    CssClass="redfont" Display="Dynamic" ErrorMessage="&lt;br/&gt;Please enter Date!"
                                                                    SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="left" bgcolor="#E8F2FD" class="btn_01" height="40px">
                                                    <span class="txt"><span class="red" style="color: #ff0000"></span></span>From City<span
                                                        class="redfont1">*</span>
                                                </td>
                                                <td align="left" bgcolor="#E8F2FD" height="40px" valign="middle" width="220px">
                                                    <asp:DropDownList ID="ddlFromCity" runat="server" CssClass="glow" Height="30px" TabIndex="5"
                                                        Width="180px" OnSelectedIndexChanged="ddlFromCity_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlFromCity"
                                                        CssClass="redfont" Display="Dynamic" ErrorMessage="&lt;br /&gt; Please select From city!"
                                                        InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="1" colspan="9" bgcolor="#C9C9C9">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" bgcolor="#E8F2FD" class="style18" height="40px">
                                                    <span class="txt"><span class="red" style="color: #ff0000">&nbsp;&nbsp;</span> </span>
                                                    Party Name<span class="redfont1">*</span>
                                                </td>
                                                <td align="left" bgcolor="#E8F2FD" height="40px" valign="middle" width="233">
                                                    <asp:DropDownList ID="ddlPartyName" runat="server" AutoPostBack="True" CssClass="glow"
                                                        Height="30px" TabIndex="6" Width="190px">
                                                    </asp:DropDownList>
                                                    <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/Images/PckLst.png" AlternateText="Search"
                                                        ImageAlign="ABSMiddle" ToolTip="Search" Style="height: 28px" OnClick="imgSearch_Click"
                                                        TabIndex="4" />
                                                    <asp:RequiredFieldValidator ID="rfvddlPartyName" runat="server" ControlToValidate="ddlPartyName"
                                                        CssClass="redfont" Display="Dynamic" ErrorMessage="&lt;br /&gt; Please select Party Name!"
                                                        InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                </td>
                                                <td align="left" bgcolor="#E8F2FD" class="btn_01" height="40px" width="1%">
                                                    <span class="txt"><span class="red" style="color: #ff0000"></span></span>Rcpt Type<span
                                                        class="redfont1">*</span>
                                                </td>
                                                <td align="left" bgcolor="#E8F2FD" height="40px" valign="middle">
                                                    <asp:DropDownList ID="ddlRcptTyp" runat="server" AutoPostBack="true" CssClass="glow"
                                                        Height="30px" TabIndex="6" Width="230px" OnSelectedIndexChanged="ddlRcptTyp_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlRcptTyp" runat="server" ControlToValidate="ddlRcptTyp"
                                                        CssClass="redfont" Display="Dynamic" ErrorMessage="&lt;br /&gt; Please select Rcpt Type!"
                                                        InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                </td>
                                                <td align="left" bgcolor="#E8F2FD" width="8%" class="btn_01">
                                                    Inst. Details
                                                </td>
                                                <td align="left" valign="top" bgcolor="#E8F2FD">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtInstNo" runat="server" CssClass="glow" Width="72px" MaxLength="6"
                                                                    Style="text-align: right;" TabIndex="10"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvinstno" runat="server" ControlToValidate="txtInstNo"
                                                                    Display="Dynamic" SetFocusOnError="true" InitialValue="" ValidationGroup="save"
                                                                    ErrorMessage="<br>Enter Inst. No" CssClass="redtext_11px"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtInstDate" runat="server" CssClass="glow" Width="79px" TabIndex="11"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvinstDate" runat="server" ControlToValidate="txtInstDate"
                                                                    Display="Dynamic" SetFocusOnError="true" InitialValue="" ValidationGroup="save"
                                                                    ErrorMessage="<br>Enter Inst. Date" CssClass="redtext_11px"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="1" colspan="9" bgcolor="#C9C9C9">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" bgcolor="#E8F2FD" width="5%" class="btn_01">
                                                    Cust. Bank
                                                </td>
                                                <td align="left" bgcolor="#E8F2FD" height="40px" valign="middle">
                                                    <asp:DropDownList ID="ddlCustmerBank" runat="server" CssClass="glow" Height="30px"
                                                        TabIndex="7" Width="190px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvCusBank" runat="server" ControlToValidate="ddlCustmerBank"
                                                        CssClass="redfont" Display="Dynamic" ErrorMessage="&lt;br /&gt; Please select Cust Bank!"
                                                        InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                </td>
                                                <td align="left" bgcolor="#E8F2FD" class="btn_01" height="40px" rowspan="2" width="8%">
                                                    Remark
                                                </td>
                                                <td valign="middle" align="left" bgcolor="#E8F2FD" height="40px" rowspan="2" colspan="3">
                                                    <asp:TextBox ID="TxtRemark" runat="server" CssClass="glow" Height="50px" MaxLength="200"
                                                        oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                        TabIndex="10" Text="" Width="529px" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                </td>
                            </tr>
                            <tr>
                                <td height="1" colspan="9" bgcolor="#C9C9C9">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="975px" border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td colspan="7" bgcolor="#F5FAFF" class="btn_01">
                                                <asp:Label ID="lblNorecord" Text="" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7" bgcolor="#F5FAFF" class="btn_01">
                                                <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" Width="100%"
                                                    BorderStyle="Solid" CssClass="ibdr gridBackground internal_heading" GridLines="Both"
                                                    BorderWidth="1" RowStyle-CssClass="gridAlternateRow" AlternatingRowStyle-CssClass="gridRow"
                                                    ShowFooter="true" HeaderStyle-CssClass="linearBg" OnDataBound="grdMain_DataBound"
                                                    OnRowDataBound="grdMain_RowDataBound" ViewStateMode="Enabled">
                                                    <HeaderStyle ForeColor="Black" CssClass="linearBg" />
                                                    <AlternatingRowStyle CssClass="gridRow" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Gr No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                            <ItemStyle Width="50" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGrno" runat="server" Text='<%#Convert.ToString(Eval("Gr_No"))%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Gr Date" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                            <ItemStyle Width="50" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGrDate" runat="server" Text='<%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Receiver Name" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <%#Convert.ToString(Eval("Recivr_Name"))%>
                                                                <asp:HiddenField ID="hidRecivrIdno" runat="server" Value='<%#Eval("Recivr_Idno")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GR From" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGrFrom" runat="server" Text='<%#Eval("GR_From")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To City" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <%#Eval("To_City")%>
                                                                <asp:HiddenField ID="hidToCityIdno" runat="server" Value='<%#Eval("ToCity_Idno")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="From City" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <%#Eval("From_City")%>
                                                                <asp:HiddenField ID="hidGrIdno" runat="server" Value='<%#Eval("Gr_Idno")%>' />
                                                                <asp:HiddenField ID="hidFromCityIdno" runat="server" Value='<%#Eval("FromCity_Idno")%>' />
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Left" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotal" Text="Total :" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                            <ItemStyle Width="50" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmount" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Amount")))%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Prev.Rcvd.Amount" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                            <ItemStyle Width="50" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotRecvd" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Tot_Recvd")))%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblFTotRecvd" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Prev.Bal" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                            <ItemStyle Width="50" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCurBal" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("cur_Bal")))%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblFTotCurBal" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Recv.Amount" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                            <ItemStyle Width="100" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRcvdAmnt" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Recv_Amount")))%>'
                                                                    OnTextChanged="txt_txtRcvdAmnt" AutoPostBack="true" EnableViewState="true" ViewStateMode="Enabled"
                                                                    Width="90px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="lblnorecord" runat="server" Text="No record found"></asp:Label>
                                                    </EmptyDataTemplate>
                                                    <RowStyle CssClass="gridAlternateRow" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="1" colspan="9" bgcolor="#C9C9C9">
                                            </td>
                                        </tr>
                                        <table width="975px">
                                            <tr>
                                                <td align="right" bgcolor="#E8F2FD" class="btn_01" colspan="4" valign="top" width="90%">
                                                    Net Amount
                                                </td>
                                                <td align="right" bgcolor="#E8F2FD" class="btn_01" height="40px" style="padding-bottom: 20px"
                                                    valign="bottom">
                                                    <asp:TextBox ID="txtNetAmnt" runat="server" CssClass="input_type1" Enabled="false"
                                                        Height="24px" MaxLength="50" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                        onpaste="return false" ReadOnly="true" Style="width: 100px;" TabIndex="11" Text="0.00"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </td> </tr>
            </table>--%>





            <%--<tr>
                <td>
                    <table width="978px" border="0" align="center" cellpadding="0" cellspacing="0" class="ibdr">
                        <tr>
                            <td>
                                <table width="800" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td colspan="5">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" align="center">
                                            <table>
                                                <tr>
                                                    <td align="center">
                                                        <asp:ImageButton ID="imgBtnNew" runat="server" ImageUrl="~/Images/new_img.jpg" onmouseout="mouseOutImage('New')"
                                                            onmouseover="mouseOverImage('New')" TabIndex="17" Visible="false" OnClick="imgBtnNew_Click" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:ImageButton ID="imgBtnSave" runat="server" ImageUrl="~/Images/save_img.jpg"
                                                            ValidationGroup="save" onmouseover="mouseOverImage('save')" onmouseout="mouseOutImage('save')"
                                                            OnClick="imgBtnSave_Click" TabIndex="15" ViewStateMode="Enabled" />
                                                        <asp:HiddenField ID="hidid" runat="server" Value="" />
                                                        <asp:HiddenField ID="Hidrowid" runat="server" Value="" />
                                                        <asp:HiddenField ID="hidmindate" runat="server" />
                                                        <asp:HiddenField ID="hidmaxdate" runat="server" />
                                                        <asp:HiddenField ID="hidpostingmsg" runat="server" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:ImageButton ID="imgBtnCancel" runat="server" ImageUrl="~/Images/cancel_img.jpg"
                                                            TabIndex="16" onmouseover="mouseOverImage('cancel')" onmouseout="mouseOutImage('cancel')"
                                                            OnClick="imgBtnCancel_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="white_bg" align="center">
                                <div id="print" style="font-size: 13px;">
                                    <table cellpadding="1" cellspacing="0" width="900" border="1" style="font-family: Arial,Helvetica,sans-serif;">
                                        <tr>
                                            <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                                                border-left-style: none; border-right-style: none">
                                                <strong>
                                                    <asp:Label ID="lblCompanyname1" runat="server" Style="font-size: 18px;"></asp:Label><br />
                                                </strong>
                                                <asp:Label ID="lblCompAdd11" runat="server"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblCompAdd22" runat="server"></asp:Label><br />
                                                <asp:Label ID="lblCompCity1" runat="server"></asp:Label>&nbsp;&nbsp;
                                                <asp:Label ID="lblCompState1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblCompCityPin1" runat="server"></asp:Label><br />
                                                PH:
                                                <asp:Label ID="lblCompPhNo1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblFaxNo1" Text="FAX No.:" runat="server"></asp:Label>
                                                <asp:Label ID="lblCompFaxNo1" runat="server"></asp:Label><br />
                                                <asp:Label ID="lblTin1" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                                                    ID="lblCompTIN1" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                                                border-left-style: none; border-right-style: none">
                                                <h3>
                                                    <strong style="text-decoration: underline">
                                                        <asp:Label ID="Label19" runat="server" Text="Amount Received Against Gr"></asp:Label></strong></h3>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table border="0" width="100%">
                                                    <tr>
                                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none" width="10%">
                                                            <asp:Label ID="lbltxtgrno" Text="Receipt No." runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                            <b>
                                                                <asp:Label ID="lblGRno" runat="server"></asp:Label></b>
                                                        </td>
                                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none" width="10%">
                                                            <asp:Label ID="lbltxtgrdate" Text="Receipt Date" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none" width="10%">
                                                            <b>
                                                                <asp:Label ID="lblGrDate" runat="server"></asp:Label></b>
                                                        </td>
                                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none" width="10%">
                                                            <asp:Label ID="lbltxtPartyName" Text="Party Name" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                            <b>
                                                                <asp:Label ID="valuelbltxtPartyName" runat="server"></asp:Label></b>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table4">
                                                    <asp:Repeater ID="Repeater2" runat="server"  OnItemDataBound="Repeater2_ItemDataBound">
                                                        <HeaderTemplate>
                                                            <tr>
                                                                <td class="white_bg" style="font-size: 12px" width="3%">
                                                                    <strong>S.No.</strong>
                                                                </td>
                                                                <td style="font-size: 12px" width="5%">
                                                                    <strong>GR.No.</strong>
                                                                </td>
                                                                <td style="font-size: 12px" width="10%">
                                                                    <strong>GR. Date</strong>
                                                                </td>
                                                                <td style="font-size: 12px" width="13%">
                                                                    <strong>Receiver. Name</strong>
                                                                </td>
                                                                <td style="font-size: 12px" width="12%">
                                                                    <strong>Gr From</strong>
                                                                </td>
                                                                <td style="font-size: 12px" width="9%">
                                                                    <strong>To City</strong>
                                                                </td>
                                                                <td style="font-size: 12px" align="left" width="10%">
                                                                    <strong>From City</strong>
                                                                </td>
                                                                <td style="font-size: 12px" align="right" width="6%">
                                                                    <strong>Amount</strong>
                                                                </td>
                                                                <td style="font-size: 12px" align="right" width="12.5%">
                                                                    <strong>Pre. Recv. Amnt</strong>
                                                                </td>
                                                                <td style="font-size: 12px" align="center" width="8.5%">
                                                                    <strong>Pre. Bal.</strong>
                                                                </td>
                                                                <td style="font-size: 12px" align="right" width="12.5%">
                                                                    <strong>Recv. Amount.</strong>
                                                                </td>
                                                            </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="white_bg" width="3%">
                                                                    <%#Container.ItemIndex+1 %>.
                                                                </td>
                                                                <td class="white_bg" width="7%">
                                                                    <%#Eval("GR_No")%>
                                                                </td>
                                                                <td class="white_bg" width="10%">
                                                                    <%#string.IsNullOrEmpty(Convert.ToString(Eval("GR_Date"))) ? "" : Convert.ToDateTime(Eval("GR_Date")).ToString("dd-MM-yyyy")%>
                                                                </td>
                                                                <td class="white_bg" width="10%">
                                                                    <%#Eval("Recivr_Name")%>
                                                                </td>
                                                                <td class="white_bg" width="11%">
                                                                    <%#Eval("GR_From")%>
                                                                </td>
                                                                <td class="white_bg" width="5%">
                                                                    <%#Eval("To_City")%>
                                                                </td>
                                                                  <td class="white_bg" width="5%" align="left">
                                                                    <%#Eval("From_City")%>
                                                                </td>
                                                                <td class="white_bg" width="6%" align="right">
                                                                    <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%>
                                                                </td>
                                                                 <td class="white_bg" width="12.5%%" align="right">
                                                                    <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Tot_Recvd")))%>
                                                                </td>
                                                                 <td class="white_bg" width="8.5%%" align="center">
                                                                    <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("cur_Bal")))%>
                                                                </td>
                                                                  <td class="white_bg" width="12.5%%" align="right">
                                                                    <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Recv_Amount")))%>
                                                                </td>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table5">
                                                    <tr>
                                                        <td class="white_bg" width="5%">
                                                        </td>
                                                        <td class="white_bg" width="10%">
                                                        </td>
                                                        <td class="white_bg" width="20%" align="right">
                                                            <asp:Label ID="lblttl" Text="" Font-Bold="true" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="white_bg" width="5%" align="right">
                                                             <asp:Label ID="Label2" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="white_bg" width="6%" align="center">
                                                          <asp:Label ID="lblPrintTotAmnt" Font-Bold="true" Text="" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="white_bg" width="4%" align="center">
                                                           <asp:Label ID="lblTorPrintRecvd" Font-Bold="true" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="white_bg" width="12.5%" align="left" colspan="3">
                                                                <asp:Label ID="lblPrintCurBal" Font-Bold="true" runat="server"></asp:Label>
                                                        </td>
                                                          
                                                      
                                                         <td class="white_bg" width="4%%" align="center">
                                                            <asp:Label ID="lblPrintRecvd" Font-Bold="true" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="white_bg" colspan="9" width="15%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="white_bg" width="15%">
                                                            &nbsp;
                                                        </td>
                                                        <td class="white_bg" width="15%">
                                                            &nbsp;
                                                        </td>
                                                        <td class="white_bg" width="15%" align="center">
                                                            &nbsp;
                                                        </td>
                                                        <td class="white_bg" width="15%" align="left">
                                                            &nbsp;
                                                        </td>
                                                        <td class="white_bg" width="12.5%">
                                                            &nbsp;
                                                        </td>
                                                        <td class="white_bg" width="12.5%">
                                                            &nbsp;
                                                        </td>
                                                        <td class="white_bg" width="15%" align="right" colspan="2">
                                                            <asp:Label ID="lblnetamntAtbttm" Font-Bold="true" Text="Net Amnt." runat="server"></asp:Label>
                                                        </td>
                                                        <td class="white_bg" width="20%" align="right" colspan="2" >
                                                            <asp:Label ID="valuelblnetamntAtbttm" Font-Bold="true" runat="server"></asp:Label>
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
                                                                        <b>Customer Signature</b>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    </td>
                                                                    <td align="right" class="white_bg" style="font-size: 13px" valign="top" width="50%">
                                                                     
                                                                        <b>
                                                                            <asp:Label ID="lblCompname1" runat="server"></asp:Label><br />
                                                                          
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
                </td>
            </tr>--%>


            <%--<div id="dvGrdetails" class="web_dialog black12 MydivCenter" style="display: none;
                min-width: 950px; overflow: scroll; height: 250px; left: 35%;">
                <table style="width: 1000px; border: 0px;" cellpadding="3" cellspacing="0">
                    <tr>
                        <td class="web_dialog_title">
                            <asp:Label ID="Label1" runat="server" Text="Gr Details"></asp:Label>
                        </td>
                        <td class="web_dialog_title align_right" style="width: 15%">
                            <span style="cursor: pointer;" onclick="HideBillAgainst('dvGrdetails')">Close</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="white_bg">
                            <table width="100%">
                                <tr>
                                    <td align="left" valign="top" bgcolor="#F5FAFF" class="btn_01" style="width: 70px">
                                        Date From
                                    </td>
                                    <td align="left" valign="top" bgcolor="#F5FAFF" class="btn_01" style="width: 150px">
                                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="glow" Width="80" TabIndex="85"></asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtFrm" runat="server" ErrorMessage="Enter From Date!"
                                            Display="Dynamic" CssClass="redfont" ControlToValidate="txtDateFrom" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="left" valign="top" bgcolor="#F5FAFF" class="btn_01" style="width: 70px">
                                        Date To
                                    </td>
                                    <td align="left" valign="top" bgcolor="#F5FAFF" class="btn_01" style="width: 150px">
                                        <asp:TextBox ID="txtDateTo" runat="server" CssClass="glow" Width="80" TabIndex="86"></asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtTo" runat="server" ErrorMessage="Enter To Date!"
                                            Display="Dynamic" CssClass="redfont" ControlToValidate="txtDateTo" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="left" valign="top" bgcolor="#F5FAFF" class="btn_01">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btncontrol" Width="60"
                                            ValidationGroup="RcptEntrySrch" TabIndex="90" OnClick="btnSearch_Click" />
                                    </td>
                                    <td align="left" valign="top" bgcolor="#F5FAFF" class="btn_01">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" height="1" colspan="6" bgcolor="#C9C9C9">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        <asp:GridView ID="grdGrdetals" runat="server" GridLines="None" AutoGenerateColumns="false"
                                            Width="100%" BorderStyle="None" CssClass="ibdr gridBackground internal_heading"
                                            BorderWidth="0" RowStyle-CssClass="gridAlternateRow" AlternatingRowStyle-CssClass="gridRow">
                                            <HeaderStyle ForeColor="Black" CssClass="linearBg" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select" HeaderStyle-Width="40px">
                                                    <HeaderStyle Width="40" HorizontalAlign="Center" />
                                                    <ItemStyle Width="40" HorizontalAlign="Center" />
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAllCheckboxes(this);"
                                                            CssClass="SACatA" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkId" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gr No." HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("Gr_No"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gr Date" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MMM-yyyy")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Receiver Name" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("Recivr_Name"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="130px" HeaderText="GR From ">
                                                    <ItemStyle HorizontalAlign="Left" Width="130px" />
                                                    <ItemTemplate>
                                                        <%#Eval("GR_From")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="130px" HeaderText="From  City">
                                                    <ItemStyle HorizontalAlign="Left" Width="130px" />
                                                    <ItemTemplate>
                                                        <%#Eval("From_City")%>
                                                        <asp:HiddenField ID="hidGrIdno" runat="server" Value='<%#Eval("GR_Idno")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="130px" HeaderText="To City">
                                                    <ItemStyle HorizontalAlign="Left" Width="130px" />
                                                    <ItemTemplate>
                                                        <%#Eval("To_City")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"
                                                    HeaderText="GR Amnt">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    <ItemTemplate>
                                                        <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Amount")))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"
                                                    HeaderText="Tot.RcvdAmnt">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    <ItemTemplate>
                                                        <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Tot_Recvd")))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"
                                                    HeaderText="Cur.Bal">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    <ItemTemplate>
                                                        <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("cur_Bal")))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"
                                                    HeaderText="Recvd.Amnt">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    <ItemTemplate>
                                                        <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Recv_Amount")))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                Records(s) not found.
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" bgcolor="#F5FAFF" class="btn_01">
                                    </td>
                                    <td align="right" valign="top" bgcolor="#F5FAFF" class="btn_01">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btncontrol" Visible="false"
                                            Width="60" TabIndex="91" OnClick="btnSubmit_Click" />
                                        &nbsp;&nbsp
                                    </td>
                                    <td align="center" valign="top" bgcolor="#F5FAFF" class="btn_01">
                                        <asp:Button ID="BtnClerForPurOdr" runat="server" Text="Clear" CssClass="btncontrol"
                                            OnClick="BtnClerForPurOdr_Click" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="Label3" runat="server" CssClass="redfont"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>--%>
       
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
            $("#<%=txtGRDate.ClientID %>").datepicker({
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate,
                changeMonth: true,
                changeYear: true,
                showOn: 'button',
                buttonImage: '../images/calendar.gif'
            });
            $("#<%=txtGRDate.ClientID %>").datepicker({
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate,
                changeMonth: true,
                changeYear: true,
                showOn: 'button',
                buttonImage: '../images/calendar.gif'
            });
            $("#<%=txtDateFrom.ClientID %>").datepicker({
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate,
                changeMonth: true,
                changeYear: true,
                showOn: 'button',
                buttonImage: '../images/calendar.gif'
            });
            $("#<%=txtDateTo.ClientID %>").datepicker({
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate,
                changeMonth: true,
                changeYear: true,
                showOn: 'button',
                buttonImage: '../images/calendar.gif'
            });
            $("#<%=txtInstDate.ClientID %>").datepicker({
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate,
                changeMonth: true,
                changeYear: true,
                showOn: 'button',
                buttonImage: '../images/calendar.gif'
            });
        }

        function openModal() {
            $('#gr_details_form').modal('show');
        }

        function CloseModal() {
            $('#gr_details_form').Hide();
        }

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
        function CallPrint(strid) {
            var prtContent1 = "<table width='100%' border='0'></table>";
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=800,height=800,toolbar=1,scrollbars=1,status=1');
            WinPrint.document.write(prtContent1);
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            return false;
        }
       
    </script>
    <style>
        .linearBg
        {
            /* fallback */
            background: rgb(178,225,255); /* Old browsers */ /* IE9 SVG, needs conditional override of 'filter' to 'none' */
            background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iI2IyZTFmZiIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiM2NmI2ZmMiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
            background: -moz-linear-gradient(top,  rgba(178,225,255,1) 0%, rgba(102,182,252,1) 100%); /* FF3.6+ */
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(178,225,255,1)), color-stop(100%,rgba(102,182,252,1))); /* Chrome,Safari4+ */
            background: -webkit-linear-gradient(top,  rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* Chrome10+,Safari5.1+ */
            background: -o-linear-gradient(top,  rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* Opera 11.10+ */
            background: -ms-linear-gradient(top,  rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* IE10+ */
            background: linear-gradient(to bottom,  rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* W3C */
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#b2e1ff', endColorstr='#66b6fc',GradientType=0 ); /* IE6-8 */
        }
        .style1
        {
            font-family: Calibri;
            font-size: 15px;
            font-weight: normal;
            color: #333333;
            text-decoration: none;
            line-height: 22px;
            text-transform: capitalize;
            width: 171px;
            border-bottom-style: none;
            margin-left: 80px;
        }
        .style18
        {
            font-family: Calibri;
            font-size: 15px;
            font-weight: normal;
            color: #333333;
            text-decoration: none;
            line-height: 22px;
            text-transform: capitalize;
            width: 768px;
            border-bottom-style: none;
            margin-left: 80px;
        }
    </style>
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