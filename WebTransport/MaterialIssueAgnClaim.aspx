<%@ Page Language="C#" Title="Claim Received From Customer" AutoEventWireup="true" MasterPageFile="~/Site1.Master"
    CodeBehind="MaterialIssueAgnClaim.aspx.cs" Inherits="WebTransport.MaterialIssueAgnClaim" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-10">
            <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">Material Issue Against Claim
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%--<asp:Label ID="lblSbillNo" runat="server" Text=""></asp:Label>--%>
                    
                    <span class="view_print">
                    <a href="MaterialIssueAgnClaimList.aspx"><asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a>
                    </span>
                   <asp:LinkButton ID="lnkbtnPrint" runat="server" ToolTip="Click to print" Visible="false" OnClientClick ="return CallPrint('print');"><i class="fa fa-print icon"></i></asp:LinkButton>
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
                               <asp:DropDownList ID="ddlDateRange"  runat="server" CssClass="form-control" 
                                      AutoPostBack="True" onselectedindexchanged="ddlDateRange_SelectedIndexChanged">
                                 </asp:DropDownList>     
                                  <asp:RequiredFieldValidator ID="ref" runat="server" Display="Dynamic"
                                    ControlToValidate="ddlDateRange" ValidationGroup="save" ErrorMessage="Please select date range!"
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>   
                              </div>
                            </div>
                           	<div class="col-sm-4" style="width:33%">
                           		<label class="col-sm-3 control-label" style="width: 25%;">Date / No.</label>
                              <div class="col-sm-4" style="width: 27%;">
                               <asp:TextBox ID="txtMatIssClaimDate" runat="server" CssClass="input-sm datepicker form-control" MaxLength="6" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                 </div>
                              <div class="col-sm-3" style="width: 25%;">
                               <asp:TextBox ID="txtPrefixNo" PlaceHolder="Pref No." runat="server" CssClass="form-control" Style="text-align: right;"></asp:TextBox>
                              </div>
                              <div class="col-sm-2" style="width: 23%;">
                                <asp:TextBox ID="txtMatIssNo" PlaceHolder="No." runat="server" CssClass="form-control" Style="text-align: right;" Enabled="false"></asp:TextBox>
                                                       
                              </div>
                              <div class="col-sm-4">
                              </div>
                              <div class="col-sm-7">
                                    <asp:RequiredFieldValidator ID="refMatIssNo" runat="server" Display="Dynamic"
                                        ControlToValidate="txtMatIssNo" ValidationGroup="save" ErrorMessage="Please enter issue number!"
                                        SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>    
                                </div>
                                  <asp:RequiredFieldValidator ID="refMatIssClaimDate" runat="server" Display="Dynamic" 
                                    ControlToValidate="txtMatIssClaimDate" ValidationGroup="save" ErrorMessage="Please select issue date!" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                           	</div>
                           	<div class="col-sm-4" style="width:33%">
                              <label class="col-sm-3 control-label" style="width: 28%;">Loc.[From]<span class="required-field">*</span></label>
							     <div class="col-sm-4" style="width: 63%;">
                                  <asp:DropDownList ID="ddlFromCity" Width="175px" runat="server" AutoPostBack="true"
                                         CssClass="chzn-select" 
                                         onselectedindexchanged="ddlFromCity_SelectedIndexChanged">
                                </asp:DropDownList>                       
                                <asp:RequiredFieldValidator ID="rfvtxtfromcity" runat="server" Display="Dynamic"
                                    ControlToValidate="ddlFromCity" ValidationGroup="save" ErrorMessage="Please select location!"
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                              <div class="col-sm-1" style="width: 9%;">
                               <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/Images/PckLst.png"  
                                      AlternateText="Search" href="#divSearch" 
                                    ImageAlign="ABSMiddle" ToolTip="Search" Style="height: 28px" 
                                      onclick="imgSearch_Click" />
                              </div>
                            </div>
                            
                          </div>
                            <div class="col-sm-4">
                                <label class="col-sm-3 control-label" style="width: 30%;">Party Name<span class="required-field">*</span></label>
                                <div class="col-sm-8" style="width: 70%;">
                                    <asp:DropDownList ID="ddlParty" Enabled="false" Width="198px" runat="server" CssClass="chzn-select">
                                    </asp:DropDownList>                                                                           
                                    <asp:RequiredFieldValidator ID="rfvParty"  runat="server" Display="Dynamic" ControlToValidate="ddlParty"
                                    ValidationGroup="save" ErrorMessage="Please select Party's Name!" InitialValue="0"
                                    SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                          </div>
                        </div>
                        <div class="panel-body" style="overflow-x:auto;">     
                         <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"  
                                    Width="100%" GridLines="None" AllowPaging="false"  BorderWidth="0"  ShowFooter="true">
                                 <RowStyle CssClass="odd" />
                                <AlternatingRowStyle CssClass="even" />     
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="40" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                              <asp:HiddenField ID="hidSerialIdno" runat="server" Value='<%#Eval("SerialIdno")%>' />
                                            <asp:HiddenField ID="hidClaimIdno" runat="server" Value='<%#Eval("ClaimHeadIdno")%>' />
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="70" HeaderText="Serial No.">
                                        <ItemStyle HorizontalAlign="Left" Width="70" />
                                        <ItemTemplate>
                                        <asp:Label ID="lblGridSerialNo" runat="server" Text='<%#Convert.ToString(Eval("SerialNo"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblGridItemName" runat="server" Text='<%#Eval("ItemName")%>'></asp:Label>
                                            </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40" HeaderText="ClaimNo.">
                                        <ItemStyle HorizontalAlign="Left" Width="40" />
                                        <ItemTemplate>
                                        <asp:Label ID="lblGridClaimNo" runat="server" Text='<%#Convert.ToString(Eval("ClaimNo"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="80" HeaderText="Claim Received Date">
                                        <ItemStyle HorizontalAlign="Left" Width="80" />
                                        <ItemTemplate>
                                        <asp:Label ID="lblGridDate" runat="server" Text='<%#Convert.ToDateTime(Eval("CRcvdDate")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>

                        </div>
                    <!-- second  section -->
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
                              <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false"  onclick="lnkbtnNew_Click"
                                     Visible="false"  CssClass="btn full_width_btn btn-s-md btn-info"><i class="fa fa-file-o"></i>New</asp:LinkButton>  
                              
                             </div>                               
							    <div class="col-sm-4">

                                    <asp:HiddenField ID="hidmindate" runat="server" />
                                    <asp:HiddenField ID="hidmaxdate" runat="server" />
                                    <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true"  onclick="lnkbtnSave_Click" ValidationGroup="save" CssClass="btn full_width_btn btn-s-md btn-success"><i class="fa fa-save"></i>Save</asp:LinkButton> 
								    <asp:HiddenField ID="hidid" runat="server" Value="" />
                                    <asp:HiddenField ID="HidHeadIdno" runat="server" Value="" />
							    </div>
                                <div class="col-sm-4">
								  <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false"  onclick="lnkbtnCancel_Click"
                                        CssClass="btn full_width_btn btn-s-md btn-danger"><i class="fa fa-close"></i>Cancel</asp:LinkButton>
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
    </div>
    <!-- Search Div Start -->
    <div id="divSearch" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="popform_header">
                        Search By Claim Number</h4>
                </div>
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
									<div class="panel-body">
										<!-- First Row start -->
									<div class="clearfix odd_row">	                                
	                                <div class="col-sm-4" style="width:31%">
	                                  <label class="col-sm-5 control-label" style="width:48%">Date From</label>
                                    <div class="col-sm-7" style="width:52%">
                                     <asp:TextBox ID="txtDivDateFrom" runat="server" CssClass="input-sm datepicker form-control"  data-date-format="dd-mm-yyyy"></asp:TextBox>                                     
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtFrm" runat="server" ErrorMessage="Enter From Date!"
                                            Display="Dynamic" CssClass="classValidation" ControlToValidate="txtDivDateFrom" ValidationGroup="Search"></asp:RequiredFieldValidator>
                                    </div>
	                                </div>
	                                <div class="col-sm-4"  style="width:27%">
	                                  <label class="col-sm-5 control-label"  style="width:40%">Date To</label>
                                    <div class="col-sm-7" style="width:59%">
                                       <asp:TextBox ID="txtDivDateTo" runat="server" CssClass="input-sm datepicker form-control" data-date-format="dd-mm-yyyy"></asp:TextBox>                                     
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtTo" runat="server" ErrorMessage="Enter To Date!"  Display="Dynamic" CssClass="classValidation" ControlToValidate="txtDivDateTo" ValidationGroup="Search"></asp:RequiredFieldValidator>
                                    </div>
	                                </div>
                                     <div class="col-sm-4"  style="width:41%">
	                                  <label class="col-sm-5 control-label"  style="width:34%">Claim No.</label>
                                    <div class="col-sm-7" style="width:64%">
                                       <asp:TextBox ID="txtCalimNo" PlaceHolder="Claim Number" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
	                                </div>
                                     <div class="col-sm-4"  style="width:59%">
	                                  <label class="col-sm-5 control-label"  style="width:25%">Party Name</label>
                                    <div class="col-sm-7" style="width:75%">
                                    <asp:DropDownList ID="ddlDivPrtyName" Width="232px" runat="server" CssClass="chzn-select">
                                    </asp:DropDownList>
                                    </div>
	                                </div>
                                    <div class="col-sm-4" style="padding: 0;">
	                                  <div class="col-sm-6 prev_fetch">                                      
                                        <asp:LinkButton ID="lnkbtnSearch" runat="server" CssClass="btn full_width_btn btn-sm btn-primary" OnClick="lnkbtnSearch_OnClick" CausesValidation="true"  ValidationGroup="Search"><i class="fa fa-search"></i>Search</asp:LinkButton>	                                
	                                  </div>
	                                  <div class="col-sm-6"> 
	                                     <label id="lblSearchRecords" runat="server" class="control-label">T. Record(s) : 0</label>
	                                  </div>
	                                </div>

	                               </div> 
                                   <div id="DivErrorMsg" runat="server" visible="false" class="clearfix even_row">
                                   <div class="col-sm-1" style="width:2%">
                                   </div>
                                    <div class="col-sm-6"> 
                                        <span class="required-field"><label id="lblDivErrorMsg" runat="server" class="control-label"></label></span>
                                    </div>
                                   </div>
                                  <div class="clearfix fourth_right">
                                  <section class="panel panel-in-default btns_without_border">                            
                                  <div class="panel-body">     
                                  <div class="clearfix">
		                          <section class="panel panel-default full_form_container material_search_pop_form">
		                            <div class="panel-body"  style="overflow-x:auto;" >   
                                      <asp:GridView ID="grdSearchRecords" Visible="false" runat="server" GridLines="None" AutoGenerateColumns="false"
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
                                                 <asp:TemplateField HeaderText="Claim No." HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("ClaimNo"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Party Name" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("PrtyName"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date [Received]" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToDateTime(Eval("HeadDate")).ToString("dd-MMM-yyyy")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Location" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Eval("FromCityName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Company Name" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Eval("CompName")%>
                                                        <asp:HiddenField ID="hidHeadIdno" runat="server" Value='<%#Eval("HeadIdno")%>' />
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
                        <asp:LinkButton ID="lnkbtnSubmit" OnClick="lnkbtnSubmit_Click" runat="server" CssClass="btn btn-dark"><i class="fa fa-check"></i>Ok</asp:LinkButton>
                        <asp:LinkButton ID="lnkbtnClose" runat="server" CssClass="btn btn-dark"><i class="fa fa-times"></i>Close</asp:LinkButton>
                        <div style="float: left;">
                            <asp:Label ID="Label3" runat="server" CssClass="redfont"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Search Div End -->

  <%--  <table width="100%">
        <tr style="display: block">
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
                                        <asp:Label ID="lblPrintHeadng" runat="server" Text="Claim From Customer"></asp:Label></strong></h3>
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
                                    <asp:Repeater ID="Repeater1" runat="server">
                                        <HeaderTemplate>
                                            <tr>
                                                <td class="white_bg" style="font-size: 12px" width="10%">
                                                    <strong>S.No.</strong>
                                                </td>
                                                <td style="font-size: 12px" width="20%">
                                                    <strong>Serial No</strong>
                                                </td>
                                                <td style="font-size: 12px" width="20%">
                                                    <strong>Model Name</strong>
                                                </td>
                                                <td style="font-size: 12px" width="8%">
                                                    <strong>Tyre Type</strong>
                                                </td>
                                                <td style="font-size: 12px" align="left" width="8%">
                                                    <strong>Rate Type</strong>
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
    </table>--%>

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
        window.onload = function () { };
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
            $("#<%=txtMatIssClaimDate.ClientID %>").datepicker({
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

                    if (elm[i].checked != xState)
                        elm[i].click();
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

        function openGridDetail() {
            $('#divSearch').modal('show');
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
