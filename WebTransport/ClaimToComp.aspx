<%@ Page Title="Claim Send/Received" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="ClaimToComp.aspx.cs" Inherits="WebTransport.ClaimToComp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-10">
            <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">CLAIM SEND / RECEIVED
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <span class="view_print">
                    <a href="ClaimToCompList.aspx"><asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a>
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
                                      AutoPostBack="True" 
                                      onselectedindexchanged="ddlDateRange_SelectedIndexChanged" >
                                 </asp:DropDownList>     
                                  <asp:RequiredFieldValidator ID="ref" runat="server" Display="Dynamic"
                                    ControlToValidate="ddlDateRange" ValidationGroup="save" ErrorMessage="Please select date range!"
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>   
                              </div>
                            </div>
                           	<div class="col-sm-4" style="width:33%">
                           		<label class="col-sm-3 control-label" style="width: 25%;">Date / No.</label>
                              <div class="col-sm-4" style="width: 27%;">
                               <asp:TextBox ID="txtClaimDate" runat="server" CssClass="input-sm datepicker form-control" MaxLength="6" data-date-format="dd-mm-yyyy"></asp:TextBox>                                                                       
                                <asp:RequiredFieldValidator ID="refClaimDate" runat="server" Display="Dynamic" 
                                    ControlToValidate="txtClaimDate" ValidationGroup="save" ErrorMessage="Please select Claim date!" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                                 </div>
                              <div class="col-sm-3" style="width: 25%;">
                               <asp:TextBox ID="txtPrefixNo" PlaceHolder="Pref No." runat="server" CssClass="form-control" Style="text-align: right;"></asp:TextBox>
                              </div>
                              <div class="col-sm-2" style="width: 23%;">
                                <asp:TextBox ID="txtClaimNo" PlaceHolder="No." runat="server" CssClass="form-control" Style="text-align: right;" Enabled="false"></asp:TextBox>
                                                       
                              </div>
                              <div class="col-sm-4">
                              </div>
                              <div class="col-sm-7">
                                    <asp:RequiredFieldValidator ID="refClaimNo" runat="server" Display="Dynamic"
                                        ControlToValidate="txtClaimNo" ValidationGroup="save" ErrorMessage="Please enter Claim number!"
                                        SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>    
                                </div>
                           	</div>
                           	<div class="col-sm-4" style="width:33%">
                              <label class="col-sm-3 control-label" style="width: 26%;">Claim Type</label>
					            <div class="col-sm-8" style="width: 74%;">
					                <div class="radio" style="display:inline;padding-top: 4px;">
						                <label class="radio-custom">
                                         &nbsp;<asp:RadioButton  
                                            ID="rdoAgnSend" AutoPostBack="true" runat="server"
                                            GroupName="Against" oncheckedchanged="rdoAgnSend_CheckedChanged"/>Submit to <br /> Company</label>
					                </div>
					                <div class="radio" style="display:inline;padding-top: 4px;padding-left: 15px">
						                <label class="radio-custom"><asp:RadioButton ID="rdoAgnReceived" 
                                            AutoPostBack="true"  runat="server"  GroupName="Against"  CssClass="by_receipt" 
                                            oncheckedchanged="rdoAgnReceived_CheckedChanged"/>Recvd. From <br />Company</label>
					                </div> 
                                     
					            </div>
                            </div>
                            
                          </div>
                          <div class="col-sm-4">
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

                            <div class="col-sm-4">
                                <label class="col-sm-3 control-label" style="width: 25%;">Company<span class="required-field">*</span></label>
                                    <div class="col-sm-8" style="width: 75%;">
                                        <asp:DropDownList ID="ddlCompanyName" Enabled="false" Width="212px" runat="server" CssClass="chzn-select">
                                        </asp:DropDownList>                                                                           
                                        <asp:RequiredFieldValidator ID="rfvCompanyName"  runat="server" Display="Dynamic" ControlToValidate="ddlCompanyName"
                                        ValidationGroup="save" ErrorMessage="Please select Company Name!" InitialValue="0"
                                        SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>

                                    </div>
                            </div>
                          </div>
                        </div>
                        <div class="panel-body" style="overflow-x:auto;">     
                         <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"  
                                    Width="100%" GridLines="None" OnRowDataBound="grdMain_RowDataBound" AllowPaging="false"  BorderWidth="0"  ShowFooter="true">
                                 <RowStyle CssClass="odd" />
                                <AlternatingRowStyle CssClass="even" />     
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="50" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="hidSerialIdno" runat="server" Value='<%#Eval("SerialIdno")%>' />
                                            <asp:HiddenField ID="hidClaimIdno" runat="server" Value='<%#Eval("ClaimHeadIdno")%>' />
                                            <asp:HiddenField ID="HidClaimDetailsIdno" runat="server" Value='<%#Eval("ClaimDetailsIdno")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Serial No.">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                        <asp:Label ID="lblGridSerialNo" runat="server" Text='<%#Convert.ToString(Eval("SerialNo"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%#Eval("ItemName")%>
                                            </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Claim No.">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                        <asp:Label ID="lblGridClaimNo" runat="server" Text='<%#Convert.ToString(Eval("ClaimNo"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50" HeaderText="SBill No.">
                                        <ItemStyle HorizontalAlign="Left" Width="50" />
                                        <ItemTemplate>
                                        <asp:Label ID="lblGridSbillNo" runat="server" Text='<%#string.IsNullOrEmpty(Convert.ToString(Eval("SbillNo")))? "N/A" : Convert.ToString(Eval("SbillNo"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Sale Date">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                        <asp:Label ID="lblGridClaimDate" runat="server" Text='<%#Convert.ToDateTime(Eval("CBillDate")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Defect as per Customer" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                        <ItemStyle Width="100" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGridDefectNo" runat="server" Text='<%#Convert.ToString(Eval("DefectRemark"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Vehicle Details" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                        <ItemStyle Width="100" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                           <asp:Label ID="lblGridVehDetlNo" runat="server" Text='<%#Convert.ToString(Eval("VehDetl"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                        <asp:DropDownList ID="ddlStatus" Width="100px" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Submitted" Selected="True" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Accepted" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Rejected" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="ReSubmitted" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                           <asp:Label ID="lblStatus" Visible="false" runat="server" Text='<%#Convert.ToString(Eval("Status"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Remark" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" Placeholder="Remarks" Text='<%#Convert.ToString(Eval("Remark"))%>' runat="server" EnableViewState="true" ViewStateMode="Enabled"
                                                 MaxLength="100"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="New Serial No." HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewSerialNo" Placeholder="New Serial No." Text='<%#Convert.ToString(Eval("NewSerialNo"))%>' runat="server" EnableViewState="true" ViewStateMode="Enabled"
                                                 MaxLength="100" Width="150px"></asp:TextBox>
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
                              <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false" 
                                     Visible="false"  CssClass="btn full_width_btn btn-s-md btn-info"><i class="fa fa-file-o"></i>New</asp:LinkButton> 
                                      <asp:HiddenField ID="hidmindate" runat="server" />
                                    <asp:HiddenField ID="hidmaxdate" runat="server" /> 
                                    <asp:HiddenField ID="hidid" runat="server" Value="" />
                                    <asp:HiddenField ID="HidClaimHeadIdno" runat="server" Value="" />
                              
                             </div>                               
							    <div class="col-sm-4" id="DivSave" runat="server">
                                    <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true"  
                                        ValidationGroup="save" CssClass="btn full_width_btn btn-s-md btn-success" 
                                        onclick="lnkbtnSave_Click"><i class="fa fa-save"></i>Save</asp:LinkButton> 
							    </div>
                                <div class="col-sm-4">
								  <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" 
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
                        Search</h4>
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
                                       <asp:TextBox ID="txtSBillNo" PlaceHolder="Claim No." runat="server" CssClass="form-control"></asp:TextBox>
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
                                        <asp:LinkButton ID="lnkbtnSearch" runat="server" 
                                              CssClass="btn full_width_btn btn-sm btn-primary" CausesValidation="true"  
                                              ValidationGroup="Search" onclick="lnkbtnSearch_Click"><i class="fa fa-search"></i>Search</asp:LinkButton>	                                
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
		                            <div class="panel-body" style="overflow:scroll;">   
                                      <asp:GridView ID="grdSearchRecords" Visible="false" runat="server" 
                                            GridLines="None" AutoGenerateColumns="false"
                                            Width="100%" BorderStyle="None" CssClass="display nowrap dataTable"
                                            BorderWidth="0">
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
                                                        <asp:HiddenField ID="HidClaimIdno" runat="server" Value='<%#Eval("ClaimIdno")%>' />
                                                        <%--<asp:LinkButton Text="Select" ID="lnkSelect" CommandName="Select" CommandArgument='<%#Eval("ClaimIdno")%>' runat="server"></asp:LinkButton>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Serial No." HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%#Eval("SerialNo")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Claim No." HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("ClaimNo"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sale Bill No." HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("SbillNo"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date [Sale Bill]" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToDateTime(Eval("ClaimDate")).ToString("dd-MMM-yyyy")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="CityName" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%#Eval("CityName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Party Name" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%#Eval("PartyName")%>
                                                        <asp:HiddenField ID="hidSerialIdno" runat="server" Value='<%#Eval("SerialIdno")%>' />
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
                        <asp:LinkButton ID="lnkbtnSubmit" runat="server" CssClass="btn btn-dark" OnClick="lnkbtnSubmit_Click"><i class="fa fa-check"></i>Ok</asp:LinkButton>
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
            $("#<%=txtClaimDate.ClientID %>").datepicker({
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
