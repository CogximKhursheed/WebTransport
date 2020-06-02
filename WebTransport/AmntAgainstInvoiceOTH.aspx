<%@ Page Title="Amount Received Against Invoice" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="AmntAgainstInvoiceOTH.aspx.cs" Inherits="WebTransport.AmntAgainstInvoiceOTH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:UpdatePanel ID="updpnl" runat="server" ViewStateMode="Enabled">
        <ContentTemplate>--%>
    <div class="row ">
      
        <div class="col-lg-12">
            <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">AMOUNT RECEIVED AGAINST INVOICE [Own to hire]
                <span class="view_print"><a href="ManageAmntRcvdHireInvc.aspx"><asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a>&nbsp;
                <asp:LinkButton ID="lnkbtnPrint" CssClass="fa fa-print icon" Visible="false" runat="server" AlternateText="Print" title="Print" Height="16px" OnClientClick="return CallPrint('print');"></asp:LinkButton>
                </span>
                </header>
                <div class="panel-body">
                  <form class="bs-example form-horizontal">
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                  <ContentTemplate>
                    <!-- first  section --> 
                    <div class="clearfix first_section">
                      <section class="panel panel-in-default">  
                        <div class="panel-body">
                        	<div class="clearfix odd_row">
                            <div class="col-sm-5">
                              <label class="col-sm-4 control-label">Date Range<span class="required-field">*</span></label>
                              <div class="col-sm-8" style="">
                                <asp:DropDownList ID="ddldateRange" runat="server" AutoPostBack="true" CssClass="form-control"
                                    TabIndex="1" OnSelectedIndexChanged="ddldateRange_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddldateRange"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="&lt;br /&gt; Please select Year!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-4 control-label">Receipt No.<span class="required-field">*</span></label>

                              <div class="col-sm-3">
                              	  <asp:TextBox ID="txtRcptNo" runat="server" CssClass="form-control" Height="24px" MaxLength="50"
                                        oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                        TabIndex="2" Text="" Width="110" Enabled="False"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRcptNo"
                                        CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Rcpt no!"
                                        SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>

                              <div class="col-sm-5">
                                <asp:TextBox ID="txtGRDate" runat="server" CssClass="input-sm datepicker form-control" Height="24px" MaxLength="50"
                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                    TabIndex="3" Text=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGRDate"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Date!"
                                    SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>
                           	</div>
                           	<div class="col-sm-3">
                              <label class="col-sm-4 control-label">Loc [From]<span class="required-field">*</span></label>
                              <div class="col-sm-8">
                                <asp:DropDownList ID="ddlFromCity" runat="server" CssClass="form-control" 
                                    TabIndex="4" OnSelectedIndexChanged="ddlFromCity_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlFromCity"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select From city!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>
                              
                            </div>
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-5">
                              <label class="col-sm-4 control-label">Party Name<span class="required-field">*</span></label>
                              <div class="col-sm-7">
                                <asp:DropDownList ID="ddlPartyName" runat="server" AutoPostBack="True" CssClass="chzn-select" style="width:100%"
                                  TabIndex="5" OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlPartyName" runat="server" ControlToValidate="ddlPartyName"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="&lt;br /&gt; Please select Party Name!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>
                              <div class="col-sm-1" >
                                <asp:LinkButton ID="lnkimgSearch" runat="server" TabIndex="6" ToolTip="Select Gr Details" OnClick="lnkimgSearch_Click" class="btn btn-sm btn-primary acc_home" data-toggle="modal" data-target="#gr_details_form"><i class="fa fa-file"></i></asp:LinkButton>
                              </div>
                            </div>
                           	
                            <div class="col-sm-4">
                           		<label class="col-sm-4 control-label">Rcpt. Type<span class="required-field">*</span></label>
                              <div class="col-sm-8">
                                <asp:DropDownList ID="ddlRcptTyp" runat="server" AutoPostBack="true" CssClass="form-control"
                                  TabIndex="7" Width="242px" OnSelectedIndexChanged="ddlRcptTyp_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlRcptTyp" runat="server" ControlToValidate="ddlRcptTyp"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="&lt;br /&gt; Please select Rcpt Type!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>
                           	</div>
                           	<div class="col-sm-3">
                           		<label class="col-sm-4 control-label">Inst.Details</label>

                              <div class="col-sm-4">
                              	<asp:TextBox ID="txtInstNo" runat="server" placeholder="Inst. No." CssClass="form-control" Width="72px" MaxLength="6"
                                    Style="text-align: right;" TabIndex="8"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvinstno" runat="server" ControlToValidate="txtInstNo"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="" ValidationGroup="save"
                                    ErrorMessage="Enter Inst. No" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>

                              <div class="col-sm-4">
                                <asp:TextBox ID="txtInstDate" runat="server"  CssClass="input-sm datepicker form-control" Width="79px" TabIndex="9"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvinstDate" runat="server" ControlToValidate="txtInstDate"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="" ValidationGroup="save"
                                    ErrorMessage="Enter Date" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                           	</div>
                            
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-5">
                              <label class="col-sm-4 control-label" >Cust.Bank<span class="required-field">*</span></label>
                              <div class="col-sm-8" >
                                <asp:DropDownList ID="ddlCustmerBank" runat="server" CssClass="form-control" 
                                    TabIndex="10">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCusBank" runat="server" ControlToValidate="ddlCustmerBank"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Cust Bank!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                           	<div class="col-sm-7">
                           		<label class="col-sm-2 control-label">Remark</label>
                              <div class="col-sm-10">
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
                                        Width="100%" GridLines="Both" EnableViewState="true" AllowPaging="true" BorderWidth="0"
                                        ShowFooter="true" OnPageIndexChanging="grdMain_PageIndexChanging" OnDataBound="grdMain_DataBound"
                                        OnRowDataBound="grdMain_RowDataBound" ViewStateMode="Enabled" PageSize="30">
                                    <RowStyle CssClass="odd" />
                                    <AlternatingRowStyle CssClass="even" />    
                                    <Columns>
                                        <asp:TemplateField HeaderText="Inv. No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrno" runat="server" Text='<%#Convert.ToString(Eval("Inv_No"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. Date" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrDate" runat="server" Text='<%#Convert.ToDateTime(Eval("Inv_Date")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Receiver Name" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("Recivr_Name"))%>
                                                <asp:HiddenField ID="hidRecivrIdno" runat="server" Value='<%#Eval("Recivr_Idno")%>' />
                                                <asp:HiddenField ID="hidGrIdno" runat="server" Value='<%#Eval("Inv_No")%>' />
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <FooterTemplate>
                                                Total
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="90px" />
                                            <ItemStyle Width="50" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Amount")))%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblFAmount" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Prev.Rcvd.Amount" HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="90px" />
                                            <ItemStyle Width="50" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotRecvd" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Tot_Recvd")))%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblFTotRecvd" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Prev.Bal" HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="90px" />
                                            <ItemStyle Width="50" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurBal" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("cur_Bal")))%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblFTotCurBal" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TDS Amount" HeaderStyle-Width="60" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Right" Width="60px" />
                                            <ItemStyle Width="60" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTdsAmnt" TabIndex="12" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("TDS_Amnt")))%>'
                                                    OnTextChanged="txt_txtTdsAmnt" AutoPostBack="true" EnableViewState="true" ViewStateMode="Enabled"
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Debit Note" HeaderStyle-Width="60" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                            <ItemStyle Width="60" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDebitNote" TabIndex="13"  runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Dr_Note")))%>'
                                                    OnTextChanged="txt_txtDebitNote" AutoPostBack="true" EnableViewState="true" ViewStateMode="Enabled"
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Recv.Amount" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                            <ItemStyle Width="80" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRcvdAmnt" TabIndex="14" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Recv_Amount")))%>'
                                                    OnTextChanged="txt_txtRcvdAmnt" AutoPostBack="true" EnableViewState="true" ViewStateMode="Enabled"
                                                    Width="75px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ref No(Vchr)" HeaderStyle-Width="110" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="110px" />
                                            <ItemStyle Width="110" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                            <div style="float:left;">
                                                <asp:TextBox ID="txtRefNo" runat="server" TabIndex="15" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Vchr_Amnt")))%>'
                                                    EnableViewState="true" ViewStateMode="Enabled" Enabled="false" Width="60px" AutoPostBack="True"
                                                    OnTextChanged="txtRefNo_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div style="float:right;width:22px;">
                                                <asp:ImageButton ID="btnVchrRef" TabIndex="16" runat="server" ImageUrl="~/Images/Prty_Add1.png" Width="24px"
                                                     AlternateText="Cal" ToolTip="Vchr Details" OnClick="btnVchrRef_Click"
                                                    Enabled="False" />
                                                    </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Due" HeaderStyle-Width="60" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                            <ItemStyle Width="60" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDue" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Tot_Recvd")))%>'></asp:Label>
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
                                <asp:TextBox ID="txtNetAmnt" runat="server" CssClass="form-control" Enabled="false" style="text-align:right;"
                                        Height="24px" MaxLength="50" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                        onpaste="return false" TabIndex="17" ReadOnly="true" Text="0.00"></asp:TextBox>
                              </div>
                            </div>			                              
                          </div>
                        </div>
                      </section>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                     <!-- fourth row -->
                <div class="clearfix odd_row">
                        <div class="col-lg-3"></div>
                        <div class="col-lg-6">   
                        <div class="col-sm-4">                                       
                        <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-info" OnClick="lnkbtnNew_OnClick" TabIndex="24"><i class="fa fa-file-o"></i>New</asp:LinkButton>
                        </div>
                        <div class="col-sm-4">
                          <asp:LinkButton ID="lnkbtnSave" runat="server" CssClass="btn full_width_btn btn-s-md btn-success" TabIndex="18" OnClick="lnkbtnSave_OnClick" CausesValidation="true" ValidationGroup="save" > <i class="fa fa-save"></i>Save</asp:LinkButton>
                          <asp:HiddenField ID="hidid" runat="server" Value="" />
                          <asp:HiddenField ID="Hidrowid" runat="server" Value="" />
                          <asp:HiddenField ID="hidmindate" runat="server" />
                          <asp:HiddenField ID="hidmaxdate" runat="server" />
                          <asp:HiddenField ID="hidpostingmsg" runat="server" />
                          <asp:HiddenField ID="hidtotVchrAmnt" runat="server" />
                          <asp:HiddenField ID="hidrowindex" runat="server" />
                          <asp:HiddenField ID="hidInvNo" runat="server" />
                          <asp:HiddenField ID="hidgrdindex" runat="server" />
                        </div>
                        <div class="col-sm-4">
                          <asp:LinkButton ID="lnkbtnCancel" runat="server" CssClass="btn full_width_btn btn-s-md btn-danger" TabIndex="19" OnClick="lnkbtnCancel_OnClick" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
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
	                                <asp:LinkButton ID="lnkbtnSearch" class="btn full_width_btn btn-sm btn-primary"  ValidationGroup="RcptEntrySrch" OnClick="lnkbtnSearch_Click"
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
                                     <asp:GridView ID="grdGrdetals" runat="server" AlternatingRowStyle-CssClass="gridRow"
                                        AutoGenerateColumns="false" BorderStyle="None" BorderWidth="0" GridLines="None"
                                        RowStyle-CssClass="gridAlternateRow" Width="100%" OnRowDataBound="grdGrdetals_RowDataBound">
                                        <HeaderStyle CssClass="linearBg" ForeColor="Black" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="40px" HeaderText="Select">
                                                <HeaderStyle HorizontalAlign="Center" Width="40" />
                                                <ItemStyle HorizontalAlign="Center" Width="40" />
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="SACatA" onclick="javascript:SelectAllCheckboxes(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkId" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" HeaderText="Inv. No.">
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemTemplate>
                                                    <%#Convert.ToString(Eval("Inv_No"))%>
                                                    <asp:HiddenField ID="hidGrIdno" runat="server" Value='<%#Eval("Inv_No")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="80px" HeaderText="Inv. Date">
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                <ItemTemplate>
                                                    <%#Convert.ToDateTime(Eval("Inv_Date")).ToString("dd-MMM-yyyy")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px"
                                                HeaderText="Receiver Name">
                                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                <ItemTemplate>
                                                    <%#Convert.ToString(Eval("Recivr_Name"))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px" HeaderText="Inv. Amnt">
                                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Inv_Amount")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px" HeaderText="Tot.RcvdAmnt">
                                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Tot_Recvd")))%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblFTotRecvd" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px" HeaderText="Cur.Bal">
                                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("cur_Bal")))%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblFTotCurBal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px" HeaderText="Recvd.Amnt">
                                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Recv_Amount")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                </section>
                                </div>
	                              
									</div>
								</section>
								</div>
								<div class="modal-footer">
								<div class="popup_footer_btn"> 
									<asp:LinkButton ID="lnkbtnsubmit" OnClick="lnkbtnsubmit_Click" class="btn btn-dark" runat="server">Submit</asp:LinkButton>
                                    <asp:LinkButton ID="lnkbtnClear" OnClick="lnkbtnClear_Click" class="btn btn-dark" runat="server"><i class="fa fa-times"></i>Close</asp:LinkButton>
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
    <div id="Vchr_Details" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="popform_header">
                        Voucher Details</h4>
                </div>
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
			            <div class="panel-body">

                         <div class="clearfix odd_row">	                                
	                                <div class="col-sm-6">
	                                    <label class="col-sm-4 control-label">Date From</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtDateFromSearch" runat="server" CssClass="input-sm datepicker form-control" Width="75px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDtFrom" runat="server" ErrorMessage="Enter Date To"
                                            ControlToValidate="txtDateFromSearch" ValidationGroup="abc"></asp:RequiredFieldValidator>
                                    </div>
	                                </div>
	                                <div class="col-sm-6">
	                                    <label class="col-sm-4 control-label">Date To</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtDateToSearch" runat="server" CssClass="input-sm datepicker form-control" Width="75px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDtTo" runat="server" ErrorMessage="Enter Date From"
                                            ControlToValidate="txtDateToSearch" ValidationGroup="abc"></asp:RequiredFieldValidator>
                                    </div>
	                                </div>
                                    <div class="col-sm-6">
                                    <div class="col-sm-4 prev_fetch">
	                            <asp:LinkButton ID="lnkbtnFetch" class="btn full_width_btn btn-sm btn-primary"  ValidationGroup="abc" OnClick="lnkbtnFetch_Click"
                                                runat="server">Fetch</asp:LinkButton>
	                            </div>
	                            <div class="col-sm-8"> 
	                                <asp:Label ID="Label1" runat="server" Text="T. Record(s) : 0" CssClass="control-label" ></asp:Label>
	                            </div>
	                        </div>
                            </div>
                     
                             <div class="clearfix third_right">
                                  <section class="panel panel-in-default">                            
                                    <div class="panel-body"> 
                                     <asp:GridView ID="grdVchr" OnRowCommand="grdVchr_RowCommand" OnRowDataBound="grdVchr_RowDataBound" ShowFooter="true"
                                            runat="server" AlternatingRowStyle-CssClass="gridRow" AutoGenerateColumns="false" BorderStyle="None" BorderWidth="0" GridLines="None"
                                            RowStyle-CssClass="gridAlternateRow" Width="100%">
                                        <HeaderStyle CssClass="linearBg" ForeColor="Black" />
                                       <Columns>
                                        <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="30">
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle Width="30" />
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vchr No" HeaderStyle-Width="80">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle Width="80" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblVchrId" runat="server" Text='<%#Eval("Vchr_Idno")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vchr Date" HeaderStyle-Width="60">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblVchrdate" runat="server" Text='<%#Convert.ToDateTime(Eval("Vchr_Date")).ToString("dd-MM-yyyy")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                            <ItemStyle Width="80" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblVchrAmnt" runat="server" Text='<%#(string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Acnt_Amnt"))) )%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotAmount" runat="server" Text="Total" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Prev.Rcvd.Amnt" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                            <ItemStyle Width="80" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblRcvdAmnt" runat="server" Text='<%#(string.Format("{0:0,0.00}", Convert.ToDouble(Eval("PrevRecAmnnt"))) )%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Prev.Bal" HeaderStyle-Width="80">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle Width="80" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblBal" runat="server" Text='<%#(string.Format("{0:0,0.00}", Convert.ToDouble(Eval("PrevBal"))) )%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="80">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle Width="100" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAmount" runat="server" Width="80px" OnTextChanged="txtAmount_TextChanged"
                                                    AutoPostBack="true" Text='<%#(string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Bal"))) )%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="center" />
                                            <FooterTemplate>
                                                <asp:TextBox ID="txttotAmnt" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    </asp:GridView>
                                </div>
                                </section>
                                </div>
                        </div>
                </div>
                <div class="modal-footer">
                    <div class="popup_footer_btn">
                        <asp:LinkButton ID="lnkbtnAmntSave" OnClick="lnkbtnAmntSave_Click" class="btn btn-dark"
                            runat="server">Save</asp:LinkButton>
                        <asp:LinkButton ID="LinkButton1" OnClick="lnkbtnClear_Click" class="btn btn-dark"
                            runat="server"><i class="fa fa-times"></i>Close</asp:LinkButton>
                        <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="print" style="font-size: 13px; display: none;">
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
                            <asp:Label ID="Label19" runat="server" Text="Amount Received Against Invoice"></asp:Label></strong></h3>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table border="0" width="100%">
                        <tr>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none"
                                width="10%">
                                <asp:Label ID="lbltxtgrno" Text="Receipt No." runat="server"></asp:Label>
                            </td>
                            <td>
                                :
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <b>
                                    <asp:Label ID="lblGRno" runat="server"></asp:Label></b>
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none"
                                width="10%">
                                <asp:Label ID="lbltxtgrdate" Text="Receipt Date" runat="server"></asp:Label>
                            </td>
                            <td>
                                :
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none"
                                width="10%">
                                <b>
                                    <asp:Label ID="lblGrDate" runat="server"></asp:Label></b>
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none"
                                width="10%">
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
                        <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                            <HeaderTemplate>
                                <tr>
                                    <td class="white_bg" style="font-size: 12px" width="3%">
                                        <strong>S.No.</strong>
                                    </td>
                                    <td style="font-size: 12px" width="5%">
                                        <strong>Invoie No.</strong>
                                    </td>
                                    <td style="font-size: 12px" width="10%">
                                        <strong>Invoie Date</strong>
                                    </td>
                                    <td style="font-size: 12px" width="13%">
                                        <strong>Receiver. Name</strong>
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
                                        <%#Eval("Inv_No")%>
                                    </td>
                                    <td class="white_bg" width="10%">
                                        <%#string.IsNullOrEmpty(Convert.ToString(Eval("Inv_Date"))) ? "" : Convert.ToDateTime(Eval("Inv_Date")).ToString("dd-MM-yyyy")%>
                                    </td>
                                    <td class="white_bg" width="10%">
                                        <%#Eval("Recivr_Name")%>
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
                            <td class="white_bg" width="11%" align="right">
                                <asp:Label ID="Label4" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="5%" align="center">
                            </td>
                            <td class="white_bg" width="10%" align="left">
                                <asp:Label ID="lblPrintTotAmnt" Font-Bold="true" Text="" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="6%" align="center">
                                <asp:Label ID="lblTorPrintRecvd" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="4%" align="center">
                                <asp:Label ID="lblPrintCurBal" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="12.5%" align="right" colspan="3">
                                <asp:Label ID="lblPrintRecvd" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="white_bg" colspan="9" width="15%">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <table align="left" width="100%">
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
                                        <td class="white_bg" width="8%" align="left">
                                            &nbsp;
                                        </td>
                                        <td class="white_bg" width="8%" align="left">
                                            &nbsp;
                                        </td>
                                        <td class="white_bg" width="8%" align="left">
                                            &nbsp;
                                        </td>
                                        <td class="white_bg" width="8%">
                                            &nbsp;
                                        </td>
                                        <td class="white_bg" width="15%" align="right" colspan="2">
                                        </td>
                                        <td class="white_bg" width="20%" align="right" colspan="2">
                                        </td>
                                        <td class="white_bg" width="12.5%">
                                            &nbsp;
                                        </td>
                                        <td class="white_bg" width="12.5%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right" valign="top" width="5%">
                                <asp:Label ID="lblnetamntAtbttm" Font-Bold="true" Text="Net Amnt." runat="server"></asp:Label>
                            </td>
                            <td align="right" valign="top" width="5%">
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
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
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

        });
        $("#<%=txtGRDate.ClientID %>").datepicker({
            dateFormat: 'dd-mm-yy',
            minDate: mindate,
            maxDate: maxdate,
            changeMonth: true,
            changeYear: true,
            showOn: 'button',
            //buttonImage: '../images/calendar.gif'
        });
        $("#<%=txtDateFrom.ClientID %>").datepicker({
            dateFormat: 'dd-mm-yy',
            minDate: mindate,
            maxDate: maxdate,
            changeMonth: true,
            changeYear: true,
            showOn: 'button',
            //buttonImage: '../images/calendar.gif'
        });
        $("#<%=txtDateTo.ClientID %>").datepicker({
            dateFormat: 'dd-mm-yy',
            minDate: mindate,
            maxDate: maxdate,
            changeMonth: true,
            changeYear: true,
            showOn: 'button',
            //buttonImage: '../images/calendar.gif'
        });
        $("#<%=txtInstDate.ClientID %>").datepicker({
            dateFormat: 'dd-mm-yy',
            minDate: mindate,
            maxDate: maxdate,
            changeMonth: true,
            changeYear: true,
            //showOn: 'button',
            //buttonImage: '../images/calendar.gif'
        });
        $("#<%=txtDateFromSearch.ClientID %>").datepicker({
            dateFormat: 'dd-mm-yy',
            minDate: mindate,
            maxDate: maxdate,
            changeMonth: true,
            changeYear: true,
            showOn: 'button',
            //buttonImage: '../images/calendar.gif'
        });
        $("#<%=txtDateToSearch.ClientID %>").datepicker({
            dateFormat: 'dd-mm-yy',
            minDate: mindate,
            maxDate: maxdate,
            changeMonth: true,
            changeYear: true,
            showOn: 'button',
            //buttonImage: '../images/calendar.gif'
        });
    }

    function openModal() {
        $('#gr_details_form').modal('show');
    }

    function CloseModal() {
        $('#gr_details_form').Hide();
    }

    function openpopup() {
        $('#Vchr_Details').modal('show');
    }
    function closepopup() {
        $('#Vchr_Details').modal('hide');
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
    function ShowDialog(modal) {
        $("#overlay").show();
        $("#dialog").fadeIn(300);

        if (modal) {
            $("#overlay").unbind("click");
        }
        else {
            $("#overlay").click(function (e) {
                HideDialog();
            });
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
