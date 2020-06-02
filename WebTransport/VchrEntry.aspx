<%@ Page Title="Voucher Entry" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="VchrEntry.aspx.cs" Inherits="WebTransport.VchrEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>--%>
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-9">
            <section class="panel panel-default full_form_container part_purchase_bill_form">
                <header class="panel-heading font-bold form_heading" style="text-align: center;">
                	<span style="float: left;">VOUCHER ENTRY</span> <asp:Label ID="lblVchrHdrName" runat="server" Text="PAYMENT VOUCHER"></asp:Label>
                    <span class="view_print"><a href="ManageVchrEntry.aspx"><asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a>
                    <asp:LinkButton ID="lnkbtnPrint" runat="server" ToolTip="Click to print" Visible="false" OnClientClick ="return CallPrint('print');"><i class="fa fa-print icon"></i></asp:LinkButton> 
                    <asp:Label ID="lblInfobtn" Visible="false" runat="server"> <i title="Voucher information" class="btn-info fa fa-info-circle"></i></asp:Label>
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
                              <label class="control-label">Date Range<span class="required-field">*</span></label>
                              <div>
                               <asp:DropDownList ID="ddlDateRange" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged" TabIndex="1">
                                </asp:DropDownList>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddldateRange"
                                CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Year!" InitialValue="0"
                                SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>           
                              </div>
                            </div>
                           	<div class="col-sm-4">
                              <label class="control-label">Voucher Type<span class="required-field">*</span></label>
                              <div>
                                <asp:DropDownList ID="ddlVchrType" runat="server" CssClass="form-control" TabIndex="2"  AutoPostBack="true" OnSelectedIndexChanged="ddlVchrType_SelectedIndexChanged">
                                    <asp:ListItem Text="--Select--" Value="0" Selected="True"> </asp:ListItem>
                                    <asp:ListItem Text="Payment Voucher" Value="1"> </asp:ListItem>
                                    <asp:ListItem Text="Receipt Voucher" Value="2"> </asp:ListItem>
                                    <asp:ListItem Text="Contra Voucher" Value="3"> </asp:ListItem>
                                    <asp:ListItem Text="Journal Voucher" Value="4"> </asp:ListItem>
                                    <asp:ListItem Text="Debit Note Voucher" Value="5"> </asp:ListItem>
                                    <asp:ListItem Text="Credit Note Voucher" Value="6"> </asp:ListItem>
                                </asp:DropDownList>   
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlVchrType"
                                CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select voucher type!" InitialValue="0"
                                SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>           
                              </div>
                            </div>

                            <div class="col-sm-2">
                              <label class="control-label">Voucher Date<span class="required-field">*</span></label>
                              <div>
                               <asp:TextBox ID="txtDate" runat="server" MaxLength="10" CssClass="input-sm datepicker form-control " autocomplete="off" onblur="CopyDate()" TabIndex="3" data-date-format="dd-mm-yyyy" onkeydown = "return DateFormat(this, event.keyCode)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtDate"  SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Please enter date!" Display="Dynamic" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-2">
                              <label class="control-label">Voucher No<span class="required-field">*</span></label>
                              <div>
                               <asp:TextBox ID="txtVchrNo" runat="server" MaxLength="10" CssClass="form-control" autocomplete="off"  ReadOnly="true" Visible="true"  TabIndex="4"></asp:TextBox>
                           
                              </div>
                            </div>
                          </div>	
                          <div class="clearfix even_row">
                            <div class="col-sm-4">
                              <label class="col-sm-5 control-label">Amnt. Type<span class="required-field">*</span></label>
                              <div class="col-sm-7">
                                 <asp:DropDownList ID="ddlVchrModeTyp" runat="server" CssClass="form-control"  AutoPostBack="true" TabIndex="5" OnSelectedIndexChanged="ddlVchrModeTyp_SelectedIndexChanged">
                                    <asp:ListItem Text="Credit" Value="1"> </asp:ListItem>
                                    <asp:ListItem Text="Debit" Value="2"> </asp:ListItem>
                                </asp:DropDownList>         
                              </div>
                            </div>
                           	<div class="col-sm-4">
                              <label class="col-sm-3 control-label">Ledger<span class="required-field">*</span></label>
                              <div class="col-sm-8">
                                 <asp:DropDownList ID="ddlVchrMode" runat="server" CssClass="form-control" TabIndex="6"  AutoPostBack="true" OnSelectedIndexChanged="ddlVchrMode_SelectedIndexChanged">
                                 </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvVchrMode" runat="server" ControlToValidate="ddlVchrMode" ValidationGroup="Save" ErrorMessage="Please select ledger!" CssClass="classValidation"
                                    SetFocusOnError="true" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:Label ID="lblVchrModeCurBal" runat="server" Text="" ForeColor="DarkRed"></asp:Label>                                        
                              </div>
                              <div class="col-sm-1">
                               <asp:LinkButton  ID="lnkPrtyLorry1"  Visible="false" runat="server" ToolTip="Lorry Details" Width="25px" Height="20px"
                                      CssClass="btn-sm btn btn-primary acc_home"
                                      data-target="#party_name_popup" onclick="lnkPrtyLorry1_Click"><i class="fa fa-truck" ></i></asp:LinkButton>
                              </div>
                            </div>
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label">Amnt<span class="required-field">*</span></label>
                              <div class="col-sm-9">
                                <asp:TextBox ID="txtVchrModeAmnt" runat="server" MaxLength="10" CssClass="form-control"  Style="text-align: right;" autocomplete="off"  AutoPostBack="false"  TabIndex="7" OnTextChanged="txtVchrModeAmnt_TextChanged"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvVchrModeAmnt" runat="server" ControlToValidate="txtVchrModeAmnt"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter amount!"
                                    ValidationGroup="Save"></asp:RequiredFieldValidator>                             
                              </div>
                            </div>
                          </div>
                          <div class="clearfix odd_row">
                          <asp:Panel ID="pnl" runat="server" DefaultButton="lnkbtnSubmit">
                            <div class="col-sm-4">
                              <label class="col-sm-5 control-label">Amnt. Type<span class="required-field">*</span></label>
                              <div class="col-sm-7">
                               <asp:DropDownList ID="ddlLdgrTyp" runat="server" CssClass="form-control" Enabled="false" TabIndex="9">
                                    <asp:ListItem Text="Credit" Value="1"> </asp:ListItem>
                                    <asp:ListItem Text="Debit" Value="2"> </asp:ListItem>
                                </asp:DropDownList>       
                              </div>
                            </div>
                           	<div class="col-sm-4">
                              <label class="col-sm-3 control-label">Ledger<span class="required-field">*</span></label>
                              <div class="col-sm-8">
                               <asp:DropDownList ID="ddlLedgrName" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="10" OnSelectedIndexChanged="ddlLedgrName_SelectedIndexChanged">
                                </asp:DropDownList>                                
                                <asp:RequiredFieldValidator ID="rftLedgrName" runat="server" ControlToValidate="ddlLedgrName" CssClass="classValidation" InitialValue="0" Display="Dynamic" ErrorMessage="Please select ledger!"
                                    ValidationGroup="Submit"> </asp:RequiredFieldValidator>
                                <asp:HiddenField ID="hidrowid" runat="server" />
                                <asp:HiddenField ID="HidStockQty" runat="server" />
                                <asp:HiddenField ID="hidAmount" runat="server" />
                                <asp:HiddenField ID="hidCostRowId" runat="server" />
                                 <asp:Label ID="lblLedgrNmCurBal" runat="server" Text="" ForeColor="DarkRed"></asp:Label>
                              </div>
                             <div class="col-sm-1">
                                <asp:LinkButton  ID="lnkPrtyLorry2" Visible="false"  runat="server" ToolTip="Lorry Details" Width="25px" Height="20px"
                                      CssClass="btn-sm btn btn-primary acc_home"
                                      data-target="#party_name_popup1" onclick="lnkPrtyLorry2_Click"><i class="fa fa-truck" ></i></asp:LinkButton>
                              </div>
                            </div>

                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label">Amnt<span class="required-field">*</span></label>
                              <div class="col-sm-9">
                                <asp:TextBox ID="txtLdgrAmnt" runat="server" CssClass="form-control" MaxLength="10"  AutoPostBack="false" Style="text-align: right;" TabIndex="11" OnTextChanged="txtLdgrAmnt_TextChanged"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvQty" runat="server" ControlToValidate="txtLdgrAmnt" CssClass="classValidation" Display="Dynamic" ErrorMessage="Enter amount!" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                              </div>

                            </div>
                            <div class="col-sm-12">
                            <asp:Label ID="lblEnterMsg" runat="server" ForeColor="#FF3300"></asp:Label>
                            </div>
                            </asp:Panel>
                          </div>   
                          
                           <div class="clearfix even_row">
                            <label class="control-label">Entry Narration</label>
                            <div>

                            <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtEntryNarr"
                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000" ServiceMethod="GetNarration" >
                                </ajax:AutoCompleteExtender>
                              <asp:TextBox ID="txtEntryNarr" runat="server" CssClass="form-control"  TabIndex="18" MaxLength="200"></asp:TextBox>
                            </div>
                          </div> 
                       
                           <div class="clearfix even_row">
                             <div class="col-sm-9">
                             </div>
                                <div class="col-sm-3">
                               <asp:LinkButton ID="lnkbtnSubmit" runat="server" CausesValidation="true" ValidationGroup="Submit" style="padding: 2px 6px;" CssClass="btn full_width_btn btn-sm btn-primary" TabIndex="13" OnClick="lnkbtnSubmit_OnClick" >Submit</asp:LinkButton>
                                                                      
                                <asp:ImageButton ID="imgbtnInstDetEdit" runat="server" OnClick="lnklblInstDiv_Click"
                                    TabIndex="14" ToolTip="Edit Inst. Details" Visible="false" ImageUrl="~/Images/Cheque_logo.png"
                                    Height="30px" Width="35px" />
                         
                              </div>
                             <//div>                                           
                        </div>
                      </section>                        
                    </div>
                      <div class="clearfix">
                     <!-- second  section -->	                      
                    <div class="clearfix second_right">
                      <section class="panel panel-in-default btns_without_border">                            
                        <div class="panel-body" style="overflow-x:auto;">     
                          <asp:GridView ID="grdMain" runat="server" AllowPaging="false" AutoGenerateColumns="false" BorderStyle="None" BorderWidth="0" GridLines="None" CssClass="display nowrap dataTable"
                                OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand"  OnRowDataBound="grdMain_RowDataBound" PageSize="50" 
                                ShowFooter="true" Width="100%" TabIndex="15">
                                 <RowStyle CssClass="odd" />
                                <AlternatingRowStyle CssClass="even" />
                                <HeaderStyle CssClass="headerclass"  />
                                <Columns>
                                    <asp:TemplateField HeaderText="Ledger" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Eval("Ledger_Nm")%>
                                        </ItemTemplate>                                    
                                        <FooterTemplate>
                                          <strong> <asp:Label ID="lblTot" runat="server" Text="Total"></asp:Label></strong>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Right">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle Width="80px" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <%#Eval("Amount")%>
                                        </ItemTemplate>                                     
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotAmnt" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle Width="5px" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            &nbsp;
                                        </ItemTemplate>                                    
                                        <FooterTemplate>
                                            &nbsp;
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="InstType" HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%#Eval("Inst_Type")%>
                                            <asp:HiddenField ID="hidLedgerId" runat="server" Value=' <%#Eval("Ledger_Id")%>' />
                                            <asp:HiddenField ID="hidVchrIdNo" runat="server" Value=' <%#Eval("Vchr_IdNo")%>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            &nbsp;
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inst No." HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%#Eval("Inst_No")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            &nbsp;
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inst Date" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Eval("Inst_Date")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            &nbsp;
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Re-Conc&nbsp;Date" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Eval("Bank_Date")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            &nbsp;
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cust Bank" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Eval("Cust_Bank")%>
                                            <asp:HiddenField ID="hidInstTypeId" runat="server" Value=' <%#Eval("InstType_Id")%>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            &nbsp;
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Narration" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Eval("EntryNarr")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            &nbsp;
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemStyle />
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("Id") %>' CommandName="cmdedit" TabIndex="16" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" TabIndex="17" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                      
                                        <asp:HiddenField ID="hidRowId" runat="server" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            &nbsp;
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Allocation">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgCostcenter" runat="server" AlternateText="Details" ToolTip="Allocate Cost Center"
                                                ImageUrl="~/images/Add_256.png" CommandName="cmdcostcenter" CommandArgument='<%#Eval("Id")%>'
                                                TabIndex="18" />
                                            <asp:ImageButton ID="ImgCostRemove" runat="server" AlternateText="Details" ToolTip="Deallocate Cost Center"
                                                ImageUrl="~/images/inactive.png" CommandName="cmdCostRemove" CommandArgument='<%#Eval("Id")%>'
                                                TabIndex="19" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            &nbsp;
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblMsg" runat="server" Text="No Record(s) found." Visible="false"></asp:Label>
                                </EmptyDataTemplate>                                                               
                            </asp:GridView>
                        </div>
                      </section>
                    </div> 

                    <div class="clearfix">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body">     
                          <div class="clearfix odd_row">
                            <label class="control-label">Voucher Narration</label>
                            <div>
                              <asp:TextBox ID="txtVchrNarr" runat="server" CssClass="form-control"  TabIndex="18" TextMode="MultiLine" onkeyup="counterText()" MaxLength="200"
                                    onkeypress=" return Validate(event)" Style="resize: none"></asp:TextBox>
                                <br />
                               
                                <asp:Label ID="lblCounter" runat="server" Text="200 characters left"></asp:Label>
                               
                            	
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
                            <div class="col-lg-3"></div>
                            <div class="col-lg-6">                                            
                              <div class="col-sm-6">
                              <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" ValidationGroup="Save" CssClass="btn full_width_btn btn-s-md btn-success"  TabIndex="19" OnClick="lnkbtnSave_OnClick" ><i class="fa fa-save"></i>Save</asp:LinkButton>                     
                             
                                 <asp:HiddenField ID="HidheadID" runat="server" />
                                    <asp:HiddenField ID="hidHeadDt" runat="server" />
                                    <asp:HiddenField ID="HidTotAmnt" runat="server" />
                                    <asp:HiddenField ID="HidVchrTyp" runat="server" />
                                    <asp:HiddenField ID="hidCurbal" runat="server" />
                                    <asp:HiddenField ID="hidtemp" runat="server" />
                                    <asp:HiddenField ID="hidRcptNo" runat="server" />                            
                              </div>
                              <div class="col-sm-6">                          
                                <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-danger" TabIndex="20" OnClick="lnkbtnCancel_OnClick" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>                              
                              </div>
                            </div>
                            <div class="col-lg-3"></div>
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
    <table border="0" cellpadding="2" cellspacing="0" class="border" width="100%">
        <tr>
            <td class="white_bg " align="center">
                <table border="0" align="center" width="850px" cellpadding="0" cellspacing="0" id="tblAuthorize"
                    runat="server">
                    <tr>
                        <td>
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
                                class="ibdr">
                                <!--Print Code Starts -->
                                <tr style="display: none;">
                                    <td class="white_bg" align="center">
                                        <div id="print" style="font-size: 10px;">
                                            <table cellpadding="1" cellspacing="0" width="800" border="0" style="font-family: Arial,Helvetica,sans-serif;">
                                                <tr style="height: 100px;">
                                                    <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                                                        border-left-style: none; border-right-style: none">
                                                        <div id="header" runat="server">
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
                                                            <asp:Label ID="lblTin" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;<asp:Label
                                                                ID="lblCompTIN" runat="server"></asp:Label>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="3">
                                                        <asp:Repeater ID="rptpaymentvoucher" runat="server" OnItemDataBound="rptpaymentvoucher_ItemDataBound">
                                                            <HeaderTemplate>
                                                                <table width="90%" cellpadding="0" cellspacing="0" style="font-size: 10px;">
                                                                    <tr>
                                                                        <td align="center" colspan="2">
                                                                            <div style="width: 10%; float: left;">
                                                                            </div>
                                                                            <div style="width: 90%; float: left;">
                                                                                <asp:Label ID="lblcomp" runat="server"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" class="white_bg" valign="top" colspan="2" style="font-size: 12px;">
                                                                            <strong>
                                                                                <asp:Label ID="lblPrintHeadng" runat="server" Text=""></asp:Label></strong>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" colspan="2" style="height: 05px;">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left">
                                                                            Voucher No: <strong>
                                                                                <asp:Label ID="lblno" runat="server" Style="padding-left: 20px;"></asp:Label></strong>
                                                                        </td>
                                                                        <td align="right">
                                                                            <span style="padding-right: 05px;">Dated: <strong>
                                                                                <asp:Label ID="lbldated" runat="server"></asp:Label></strong></span>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" colspan="2" style="height: 05px;">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 576px; padding-left: 20px; border-top: 1px solid #B2B2B2; border-bottom: 1px solid #B2B2B2;
                                                                            border-right: 1px solid #B2B2B2;">
                                                                            Particulars
                                                                        </td>
                                                                        <td align="right" style="border-top: 1px solid #B2B2B2; border-bottom: 1px solid #B2B2B2;">
                                                                            <span style="padding-right: 05px;">Amount</span>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" style="border-right: 1px solid #B2B2B2;">
                                                                            <span><strong>Account:</strong></span>
                                                                        </td>
                                                                        <td align="left">
                                                                        </td>
                                                                    </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td align="left" valign="top" style="border-right: 1px solid #B2B2B2;">
                                                                        <span style="padding-left: 20px;">
                                                                            <%#Eval("Ledger_Nm")%></span><br />
                                                                        <span style="padding-left: 20px; font-size: 10px;"><span style="padding-left: 20px;
                                                                            font-size: 10px;">
                                                                            <%# string.IsNullOrEmpty(Convert.ToString(Eval("Inst_Type"))) ? "" : Eval("Inst_Type") + "<br/>"%></span>
                                                                            <span style="padding-left: 20px; font-size: 10px;">
                                                                                <%# string.IsNullOrEmpty(Convert.ToString(Eval("Cust_Bank"))) ? "" : Eval("Cust_Bank") + "<br/>"%>
                                                                            </span><span style="padding-left: 20px; font-size: 10px;">
                                                                                <%# string.IsNullOrEmpty(Convert.ToString(Eval("Inst_Date"))) ? "" : Eval("Inst_Date") + "<br/>"%>
                                                                            </span>
                                                                    </td>
                                                                    <td align="right" valign="top">
                                                                        <span style="padding-right: 05px;"><strong>
                                                                            <%#Convert.ToDouble(Eval("Amount")).ToString("N2")%></strong> </span>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <tr>
                                                                    <td style="height: 25px; border-right: 1px solid #B2B2B2;">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="border-right: 1px solid #B2B2B2; border-top: 1px solid #B2B2B2;">
                                                                        <span><strong>Through:</strong></span><br />
                                                                        <span style="padding-left: 20px;">
                                                                            <asp:Label ID="lblthrough" runat="server"></asp:Label>
                                                                            <br />
                                                                        </span>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="border-right: 1px solid #B2B2B2;">
                                                                        <span><strong>Remark:</strong></span><br />
                                                                        <span style="padding-left: 20px;">
                                                                            <asp:Label ID="lblthroughnarration" runat="server"></asp:Label></span>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="border-right: 1px solid #B2B2B2; border-bottom: 1px solid #B2B2B2;">
                                                                        <span><strong>Amount&nbsp;(in words):</span></strong><br />
                                                                        <span style="padding-left: 20px;">
                                                                            <asp:Label ID="lblamountinwords" runat="server"></asp:Label>
                                                                        </span>
                                                                    </td>
                                                                    <td align="right" style="border-top: 1px solid #B2B2B2; border-bottom: 1px solid #B2B2B2;">
                                                                        <span style="padding-right: 05px;"><strong>Rs.
                                                                            <asp:Label ID="lblrpttotalamount" runat="server"></asp:Label></strong> </span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="height: 10px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblremarks" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="height: 20px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Receiver's Signature
                                                                    </td>
                                                                    <td align="center">
                                                                        Authorised Signature
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="height: 20px;">
                                                                    </td>
                                                                </tr>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                        <asp:Repeater ID="rptJournalContra" runat="server" OnItemCommand="rptJournalContra_ItemCommand"
                                                            OnItemDataBound="rptJournalContra_ItemDataBound">
                                                            <HeaderTemplate>
                                                                <table width="90%" cellpadding="0" cellspacing="0" style="font-size: 10px;">
                                                                    <tr>
                                                                        <td align="center" colspan="3">
                                                                            <div style="width: 10%; float: left;">
                                                                            </div>
                                                                            <div style="width: 90%; float: left;">
                                                                                <asp:Label ID="lblcomp" runat="server"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" colspan="3" style="height: 05px;">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" class="white_bg" valign="top" colspan="3" style="font-size: 12px;">
                                                                            <strong>
                                                                                <asp:Label ID="lblPrintHeadng" runat="server" Text=""></asp:Label></strong>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" colspan="2" style="height: 05px;">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left">
                                                                            Voucher No: <strong>
                                                                                <asp:Label ID="lblno" runat="server" Style="padding-left: 20px;"></asp:Label></strong>
                                                                        </td>
                                                                        <td align="right" colspan="3">
                                                                            <span style="padding-right: 05px;">Dated: <strong>
                                                                                <asp:Label ID="lbldated" runat="server"></asp:Label></strong></span>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" colspan="3" style="height: 05px;">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 500px; padding-left: 20px; border-top: 1px solid #B2B2B2; border-bottom: 1px solid #B2B2B2;
                                                                            border-right: 1px solid #B2B2B2;">
                                                                            Particulars
                                                                        </td>
                                                                        <td align="right" style="width: 150px; border-top: 1px solid #B2B2B2; border-bottom: 1px solid #B2B2B2;
                                                                            border-right: 1px solid #B2B2B2;">
                                                                            <span style="padding-right: 05px;">Debit</span>
                                                                        </td>
                                                                        <td align="right" style="width: 150px; border-top: 1px solid #B2B2B2; border-bottom: 1px solid #B2B2B2;">
                                                                            <span style="padding-right: 05px;">Credit</span>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" style="border-right: 1px solid #B2B2B2;">
                                                                            <asp:Label ID="lblParticulars" runat="server" Style="padding-left: 20px;"></asp:Label><br />
                                                                            <asp:Label ID="lblParticularNarration" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="right" style="border-right: 1px solid #B2B2B2;">
                                                                            <strong>
                                                                                <asp:Label ID="lblDebit" runat="server" Style="padding-right: 05px;"></asp:Label></strong>
                                                                        </td>
                                                                        <td align="right">
                                                                            <strong>
                                                                                <asp:Label ID="lblCredit" runat="server" Style="padding-right: 05px;"></asp:Label></strong>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" style="height: 05px; border-right: 1px solid #B2B2B2;">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td style="border-right: 1px solid #B2B2B2;">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td align="left" valign="top" style="border-right: 1px solid #B2B2B2;">
                                                                        <span style="padding-left: 20px;">
                                                                            <%#Eval("Ledger_Nm")%></span><br />
                                                                        <span style="padding-left: 20px; font-size: 10px;"><span style="padding-left: 20px;
                                                                            font-size: 10px;">
                                                                            <%#Eval("Inst_Type")%></span>
                                                                    </td>
                                                                    <td align="right" valign="top" style="border-right: 1px solid #B2B2B2;">
                                                                        <span style="padding-right: 05px;"><strong>
                                                                            <asp:Label ID="lbldebit" runat="server"></asp:Label></strong> </span>
                                                                    </td>
                                                                    <td align="right" valign="top">
                                                                        <span style="padding-right: 05px;"><strong>
                                                                            <asp:Label ID="lblcredit" runat="server"></asp:Label></strong> </span>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <tr>
                                                                    <td style="height: 25px; border-right: 1px solid #B2B2B2;">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td style="border-right: 1px solid #B2B2B2;">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="border-right: 1px solid #B2B2B2; border-bottom: 1px solid #B2B2B2;">
                                                                        <span><strong></span></strong><br />
                                                                        <span style="padding-left: 20px;">
                                                                            <asp:Label ID="lblamountinwords" runat="server"></asp:Label>
                                                                        </span>
                                                                    </td>
                                                                    <td align="right" style="border-top: 1px solid #B2B2B2; border-bottom: 1px solid #B2B2B2;
                                                                        border-right: 1px solid #B2B2B2;">
                                                                        <span style="padding-right: 05px;"><strong>Rs.
                                                                            <asp:Label ID="lblDebitAmount" runat="server"></asp:Label></strong> </span>
                                                                    </td>
                                                                    <td align="right" style="border-top: 1px solid #B2B2B2; border-bottom: 1px solid #B2B2B2;">
                                                                        <span style="padding-right: 05px;"><strong>Rs.
                                                                            <asp:Label ID="lblCreditAmount" runat="server"></asp:Label></strong> </span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" style="border-right: 1px solid #B2B2B2; height: 10px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <asp:Label ID="lblremarks" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" style="height: 20px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                    </td>
                                                                    <td align="center" colspan="2">
                                                                        Authorised Signature
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" style="height: 20px;">
                                                                    </td>
                                                                </tr>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" class="white_bg" colspan="3" style="font-size: 10px; border-top: 1px solid #B2B2B2;">
                                                        This is a Computer Generated Invoice.
                                                    </td>
                                                </tr>
                                            </table>
                                            <p style="line-height: 12px; font-size: 12px;">
                                                <asp:Label ID="lblGeneratedByName" runat="server"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblLastUpdatedByName" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                    </td>
                                </tr>
                                <!--Print Code Ends Here -->
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <!--client start--->
    <div id="dvInstDet" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content" style="width: 75%">
                <div class="modal-header">
                    <h4 class="popform_header">
                        <asp:Label ID="lblclientname1" runat="server" Text="Instrument Detail - "></asp:Label>
                        <asp:Label ID="lblHeader" Font-Size="Small" runat="server" Text="Instrument Detail"></asp:Label>
                    </h4>
                </div>
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">  
             <div class="panel-body">
                          <div class="clearfix odd_row">
                            <div class="col-sm-6">
                              <label class="control-label">Inst. Type.<span class="required-field">*</span></label>
                              <div>
                                 <asp:DropDownList ID="ddlInstType" runat="server" CssClass="form-control" Width="180px" TabIndex="16"  onchange="javascript:onChangeNEFT();">
                                    <asp:ListItem Text="--Select--" Value="0"> </asp:ListItem>
                                    <asp:ListItem Text="B. Cheque" Value="1"> </asp:ListItem>
                                    <asp:ListItem Text="Cheque" Value="2"> </asp:ListItem>
                                    <asp:ListItem Text="D. Draft" Value="3"> </asp:ListItem>
                                    <asp:ListItem Text="DCN" Value="4"> </asp:ListItem>
                                    <asp:ListItem Text="NEFT/RTGS" Value="5"> </asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvInstType" runat="server" ControlToValidate="ddlInstType"
                                InitialValue="0" CssClass="classValidation" Display="Dynamic" ErrorMessage="Enter inst. type"
                                ValidationGroup="SaveInstDet" SetFocusOnError="true"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-6">
                              <label class="control-label">Inst. No.<span class="required-field">*</span></label>
                              <div>
                                <asp:TextBox ID="txtInstNo" runat="server" CssClass="form-control" MaxLength="6" TabIndex="17"
                                    Text="" Width="180px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvInstNo" runat="server" ControlToValidate="txtInstNo"
                                        SetFocusOnError="true" CssClass="classValidation" Display="Dynamic" ErrorMessage="Enter inst. no."
                                        ValidationGroup="SaveInstDet"></asp:RequiredFieldValidator>

                              </div>
                            </div>
                          </div>  
                           <div class="clearfix even_row">
                           <div class="col-sm-6">
                           <label class="control-label">Inst. Date<span class="required-field">*</span></label>
                           <div>
                            <asp:TextBox ID="txtInstDate" runat="server" MaxLength="10" CssClass="input-sm datepicker form-control " autocomplete="off" onblur="CopyDate()"
                             Width="180px" TabIndex="18"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="rfvInstDate" runat="server" ControlToValidate="txtInstDate"
                            SetFocusOnError="true" ValidationGroup="SaveInstDet" ErrorMessage="Please enter date"
                            Display="Dynamic" CssClass="classValidation"></asp:RequiredFieldValidator>
                           </div>
                           </div>
                           <div class="col-sm-6">
                           <asp:Label ID="lblBankName" runat="server" Text="Bank Name"></asp:Label>
                           <div>
                           <asp:TextBox ID="txtCustBankName" runat="server" CssClass="form-control" MaxLength="50"
                            TabIndex="19" Width="180px"></asp:TextBox>
                            </div>
                           </div>
                          
                           </div> 
                           <div class="clearfix odd_row">
                           <div class="col-sm-5">
                           <asp:HiddenField ID="hidmindate" runat="server" />
                              <asp:HiddenField ID="hidmaxdate" runat="server" />
                           </div>
                           <div class="col-sm-4">
                            <asp:Button ID="btnInstDetOK" runat="server" Text="OK" ValidationGroup="SaveInstDet"
                            Font-Bold="true" ForeColor="White" BackColor="#0066ff" TabIndex="21" Height="26px"
                            Width="70px"
                            OnClick="btnInstDetOK_Click" />
                            <img id="PopupLoaderImageCity" style="display: none;" src="Images/indicator.gif"
                                 alt="Please Wait..." title="Please Wait..." />
                            </div>
                            </div>
                            <div class="clearfix odd_row" id="DivError" visible="false" runat="server">
                                <asp:Label ID="lblErrorMsg" CssClass="classValidation" runat="server"></asp:Label> 
                            </div>
                        </div>
        </section>
                </div>
            </div>
        </div>
    </div>
    <%--<div id="dvInstDet" class="web_dialog black12" style="display: block; width: 500px;
        height: auto;">
        <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="1">
            <tr>
                <td class="web_dialog_title" colspan="2">
                    
                </td>
                <td class="web_dialog_title align_right" colspan="2">
                    <span style="cursor: pointer;" onclick="HideClient()">Close</span>
                </td>
            </tr>
            <tr>
                <td align="left" class="white_bg" nowrap="nowrap" valign="top" width="80px">
                    Inst. Type.
                </td>
                <td align="left" class="white_bg" nowrap="nowrap" valign="top" width="120px">
                   
                    <br />
                    
                </td>
                <td align="left" class="white_bg" nowrap="nowrap" valign="top" width="80px">
                    Inst. No.
                </td>
                <td align="left" class="white_bg" nowrap="nowrap" valign="top">
                   
                    <br />
                    
                </td>
            </tr>
            <tr>
                <td height="5" align="left" valign="bottom" bgcolor="#cccccc" colspan="4">
                </td>
            </tr>
            <tr>
                <td align="left" nowrap="nowrap" valign="top" width="80px" bgcolor="#E8F2FD">
                    Inst. Date
                </td>
                <td align="left" nowrap="nowrap" valign="top" width="120px" bgcolor="#E8F2FD">
                    
                    <br />
                   
                </td>
                <td align="left" nowrap="nowrap" valign="top" width="80px" bgcolor="#E8F2FD">
                    
                </td>
                <td align="left" nowrap="nowrap" valign="top" bgcolor="#E8F2FD">
                    
                </td>
            </tr>
            <tr>
                <td height="5" align="left" valign="bottom" bgcolor="#cccccc" colspan="4">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                </td>
                <td align="left" colspan="2">
                  
                </td>
            </tr>
            <tr>
                <td colspan="2" class="white_bg">
                   
                </td>
            </tr>
        </table>
       
    </div>--%>
    <div id="dvGrdetails" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
									<div class="panel-body">
										<!-- First Row start -->
									<div class="clearfix odd_row">	                                
	                                <div class="col-sm-4">
	                                  <label class="col-sm-5 control-label"></label>
                                    <div class="col-sm-7">
                                    <asp:Label ID="Label2" runat="server" Text="Cost  Details for :"></asp:Label>
                                    </div>
	                                </div>
	                                <div class="col-sm-4">
	                                  <label class="col-sm-5 control-label"></label>
                                    <div class="col-sm-7">
                                       <asp:Label ID="lblCostLedgerName" runat="server" Text=""></asp:Label> 
                                    </div>
	                                </div>
	                                <div class="col-sm-4">
	                                 
	                                </div>
	                              </div> 
                                  <div class="clearfix fourth_right">
                        <section class="panel panel-in-default btns_without_border">                            
                          <div class="panel-body">     
                            <div class="clearfix">
		                          <section class="panel panel-default full_form_container material_search_pop_form">
		                            <div class="panel-body" style="overflow-x:auto;">   
                                   <asp:GridView ID="grdCostdetls" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="display nowrap dataTable"
                                            Width="100%" BorderStyle="None" BorderWidth="0" OnRowDataBound="grdCostdetls_RowDataBound"  OnRowCommand="grdCostdetls_RowCommand">
                                         <RowStyle CssClass="odd" />
                                         <AlternatingRowStyle CssClass="even" />  
                                            <Columns>
                                                <asp:TemplateField HeaderText="Truck No." HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlTruckNo" runat="server" Width="150px" AutoPostBack="true"
                                                            CssClass="form-control" OnSelectedIndexChanged="ddlTruckNo_SelectedIndexChanged" TabIndex="75">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hidTruckIdno" runat="server" Value='<%#Eval("Truck_Idno") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmnt" runat="server" Text='<%#Eval("Amount")%>' Width="90px"
                                                            AutoPostBack="false" CssClass="form-control" TabIndex="76"></asp:TextBox>
                                                        <asp:HiddenField ID="hidCostRowId" runat="server" Value='<%#Eval("Id") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="20px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgAddnew" runat="server" AlternateText="Details" ToolTip="Add Cost Center"
                                                            ImageUrl="~/Images/plus.gif" CommandName="AddNewRow" CommandArgument='<%#Eval("Id")%>'
                                                             />
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
                        <asp:LinkButton ID="lnkbtnCostSubmit" runat="server" CssClass="btn btn-dark" OnClientClick="HideCostCenterdetails('dvGrdetails')"
                            TabIndex="77" OnClick="lnkbtnCostSubmit_OnClick"><i class="fa fa-check"></i>Submit</asp:LinkButton>
                        <div style="float: left;">
                            <asp:Label ID="lblMsgGrid" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--USERPREF SETTING --%>
    <div class="pop-up-parent">
        <div id="infobox" class="pop-up" style="width: 600px; height: auto; display: none;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="popform_header">
                            <i class="fa fa-info-circle"></i><span>Information</span> <i class="fa fa-times">
                            </i>
                        </h4>
                    </div>
                    <div class="modal-body">
                        <section class="panel full_form_container">
								<div class="panel-body">
                                    <div class="col-sm-12">
                                        <div style="font-family:arial">
                                            <div class="col-sm-12">
                                                <div class="col-sm-3"><i class="fa fa-user"></i><b style="font-weight:bold;font-size:14px;display:inline-block;">Generated by: </b> </div>
                                                <div class="col-sm-9"><asp:Label style="padding:0;font-size:14px;display:inline-block;" ID="lblFrmGeneratedByName" runat="server"></asp:Label></div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="col-sm-3"><i class="fa fa-user"></i><b style="font-weight:bold;font-size:14px;display:inline-block;">Last Updated by: </b></div>
                                                <div class="col-sm-9"><asp:Label style="padding:0;font-size:14px;display:inline-block;" ID="lblFrmLastUpdatedByName" runat="server"></asp:Label></div>
                                            </div>
                                        </div>
                                    </div>
            </div>
            </section>
        </div>
    </div>
    </div> </div> </div>
    <div id="party_name_popup" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="popform_header">
                        Lorry Detail
                    </h4>
                </div>
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
			<div class="panel-body" style="overflow:auto;Height:400px">                
                <asp:GridView ID="grdGrdetals" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="display rap dataTable"
                                            Width="100%" BorderStyle="None" BorderWidth="0">
        <HeaderStyle ForeColor="Black" CssClass="linearBg" />
            <Columns>
            <asp:TemplateField HeaderText="Lorry No." HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                <ItemStyle Width="100px" HorizontalAlign="Left" />
                <ItemTemplate>
                    <%#Convert.ToString(Eval("Lorry_No"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Lorry Type" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                <ItemStyle Width="80px" HorizontalAlign="Left" />
                <ItemTemplate>
                    <%#Convert.ToString(Eval("LorryType"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Owner Name" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle Width="150px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <%#Convert.ToString(Eval("OwnerName"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="130px" HeaderText="Pan No">
                <ItemStyle HorizontalAlign="Left" Width="130px" />
                <ItemTemplate>
                    <%#Eval("PanNo")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="130px" HeaderText="Chasis No">
                <ItemStyle HorizontalAlign="Left" Width="130px" />
                <ItemTemplate>
                    <%#Eval("ChasisNo")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="130px" HeaderText="Engine No">
                <ItemStyle HorizontalAlign="Left" Width="130px" />
                <ItemTemplate>
                    <%#Eval("EngineNo")%>
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
                        <button type="submit" class="btn btn-dark" data-dismiss="modal">
                            <i class="fa fa-times"></i>Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="party_name_popup1" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="popform_header">
                        Lorry Detail
                    </h4>
                </div>
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
				<div class="panel-body" style="overflow:auto;Height:400px">                
                <asp:GridView ID="grdGrdetals1" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="display rap dataTable"
                                            Width="100%" BorderStyle="None" BorderWidth="0">
            <HeaderStyle ForeColor="Black" CssClass="linearBg" />
            <Columns>
            <asp:TemplateField HeaderText="Lorry No." HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                <ItemStyle Width="100px" HorizontalAlign="Left" />
                <ItemTemplate>
                    <%#Convert.ToString(Eval("Lorry_No"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Lorry Type" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                <ItemStyle Width="80px" HorizontalAlign="Left" />
                <ItemTemplate>
                    <%#Convert.ToString(Eval("LorryType"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Owner Name" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle Width="150px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <%#Convert.ToString(Eval("OwnerName"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="130px" HeaderText="Pan No">
                <ItemStyle HorizontalAlign="Left" Width="130px" />
                <ItemTemplate>
                    <%#Eval("PanNo")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="130px" HeaderText="Chasis No">
                <ItemStyle HorizontalAlign="Left" Width="130px" />
                <ItemTemplate>
                    <%#Eval("ChasisNo")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="130px" HeaderText="Engine No">
                <ItemStyle HorizontalAlign="Left" Width="130px" />
                <ItemTemplate>
                    <%#Eval("EngineNo")%>
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
                        <button type="submit" class="btn btn-dark" data-dismiss="modal">
                            <i class="fa fa-times"></i>Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--client end  --->
    <%--</ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgBtnExcel" />
        </Triggers>
    </asp:UpdatePanel>--%>
    <script type="text/javascript" language="javascript">

        function counterText() {
            var txtvchrnar = document.getElementById("<%=txtVchrNarr.ClientID %>").value.length;
            var Maxlimit = 200;
            document.getElementById("<%=lblCounter.ClientID %>").innerHTML = Maxlimit - txtvchrnar + '&nbsp;' + 'Characters Left';

        }
        function Validate(key) {
            var txtvchrnar = document.getElementById("<%=txtVchrNarr.ClientID %>").value.length;
            var Maxlimit = 200;
            if ((Maxlimit - txtvchrnar) <= 0) {
                if (key.which == 8) {
                    return true;
                }
                else {
                    key.preventDefault();
                    return false;
                }
            }

        }
        
    </script>
    <script type="text/javascript" language="javascript">
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
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            var VchrDate = $('#<%=txtDate.ClientID %>').val();
            $('#<%=txtDate.ClientID %>').datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
            $('#<%=txtInstDate.ClientID %>').datepicker({
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

           function onChangeNEFT() {

               if ((document.getElementById("<%=ddlInstType.ClientID%>").value == "5") && (document.getElementById("<%=ddlVchrType.ClientID%>").value == "1")) {
                   document.getElementById("<%=rfvInstNo.ClientID%>").style.visibility = "hidden";
                   document.getElementById("<%=rfvInstNo.ClientID%>").enabled = false;
               }
               else {
                   document.getElementById("<%=rfvInstNo.ClientID%>").style.visibility = "visible";
                   document.getElementById("<%=rfvInstNo.ClientID%>").enabled = true;
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
    <script language="javascript" type="text/javascript">
        function HideClient() {
            $("#dvInstDet").fadeOut(300);

        }

//        function ShowClient() {
//            $("#dvInstDet").fadeIn(300);
//        }

        function openGridDetail() {
            $('#dvInstDet').modal('show');
        }

        function ReloadPage() {
            setTimeout('window.location.href = window.location.href', 2000);
        }

        function HideClientForLedger() {
            $("#dvLedger").fadeOut(300);
        }
        function ShowClientForLedger() {
            $("#dvLedger").fadeIn(300);
        }

        function openPartyModal() {
            $('#party_name_popup').modal('show');
        }

        function openPartyModal1() {
            $('#party_name_popup1').modal('show');
        }

        function openModal() {
            $('#dvGrdetails').modal('show');
        }

        function CloseModal() {
            $('#dvGrdetails').Hide();
        }

        function ShowCostcenterDetails() {
            $("#dvGrdetails").fadeIn(300);
        }
        function HideCostCenterdetails() {
            $("#dvGrdetails").fadeOut(300);
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