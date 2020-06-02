<%@ Page Title="Freight Memo" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="FreightMemo.aspx.cs" Inherits="WebTransport.FreightMemo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <script language="javascript" type="text/javascript">
       function CallPrint(strid) {
           var prtContent1 = "<table width='100%' border='0'></table>";
           var prtContent = document.getElementById(strid);
           var WinPrint = window.open('', '', 'letf=0,top=0,width=300,height=300,toolbar=1,scrollbars=1,status=1');
           WinPrint.document.write(prtContent1);
           WinPrint.document.write(prtContent.innerHTML);
           WinPrint.document.close();
           WinPrint.focus();
           WinPrint.print();
           WinPrint.close();
           return false;
       }


       function openModal() {
           $('#dvGrdetails').modal('show');
       }
    </script>
   
<%--    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>--%>
        <div class="row ">
            <div class="col-lg-1"></div>
            <div class="col-lg-10">
              <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">FREIGHT MEMO
                  <span class="view_print"><a href="ManageFreightMemo.aspx" tabindex="21"><asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a>
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
                              <label class="col-sm-3 control-label" style="width: 29%;">Date Range<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 71%;">
                                <asp:DropDownList ID="ddldateRange" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="1"  OnSelectedIndexChanged="ddldateRange_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddldateRange"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Year!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 29%;">Recpt. No.<span class="required-field">*</span></label>

                              <div class="col-sm-3" style="width: 40%;">
                                 <asp:TextBox ID="txtRcptNo" runat="server" CssClass="form-control"  MaxLength="10" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                TabIndex="2"  Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRcptNo"
                                CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Rcpt no!"
                                SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                              </div>

                              <div class="col-sm-4" style="width: 31%;">
                                <asp:TextBox ID="txtGRDate" runat="server" CssClass="input-sm datepicker form-control" data-date-format="dd-mm-yyyy" MaxLength="50"  oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                TabIndex="3" Text=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtGRDate"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Date!"
                                    SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                              </div>
                           	</div>
                           	<div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 29%;">To City<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 61%;">
                                <asp:DropDownList ID="ddlToCity" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="4" OnSelectedIndexChanged="ddlToCity_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlToCity"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select To city!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                              </div>
                              <div class="col-sm-1" style="width: 10%;">
                               <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/Images/PckLst.png" AlternateText="Search" ImageAlign="ABSMiddle" ToolTip="Search" Style="height: 28px" TabIndex="5" 
                                onclick="imgSearch_Click" />
                               <%-- <a href="#challan_details_form" class="btn btn-sm btn-primary acc_home" data-toggle="modal" data-target="#challan_details_form"><i class="fa fa-file"></i></a>--%>
                              </div>                              
                            </div>
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-3">
                              <label class="col-sm-3 control-label" style="width: 39%;">GR No.<span class="required-field">*</span></label>
                              <div class="col-sm-8" style="width: 61%;">
                               <asp:TextBox ID="txtGrNo" runat="server" AutoPostBack="true" CssClass="form-control" MaxLength="50" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                    TabIndex="6" Enabled="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvItemNamee" runat="server" ControlToValidate="txtGrNo" CssClass="classValidation" Display="Dynamic" 
                                     ErrorMessage="Please Enter Gr No.!" SetFocusOnError="true"  ValidationGroup="Save"></asp:RequiredFieldValidator>      
                              </div>
                              
                            </div>
                           	<div class="col-sm-3">
                           		<label class="col-sm-3 control-label" style="width: 39%;">Receiver</label>
                              <div class="col-sm-9" style="width: 61%;">
                                <asp:DropDownList ID="ddlRecivr" runat="server" CssClass="form-control"  TabIndex="7" Enabled="false">
                                 </asp:DropDownList>        
                              </div>
                           	</div>
                           	<div class="col-sm-3">
                           		<label class="col-sm-3 control-label" style="width: 39%;">Qty</label>
                              <div class="col-sm-3" style="width: 61%;">
                                <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" MaxLength="100" oncopy="return false"  oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="8" Enabled="false"></asp:TextBox>
                              </div>
                           	</div>
                           	<div class="col-sm-3">
                           		<label class="col-sm-3 control-label" style="width: 39%;">Weight</label>
                              <div class="col-sm-3" style="width: 61%;">
                                  <asp:TextBox ID="txtWeight" runat="server" CssClass="form-control" MaxLength="100" oncopy="return false"  oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="9" Text="0.00" Enabled="false"></asp:TextBox>
                                                                          
                              </div>
                           	</div>
                          </div>

                          <div class="clearfix even_row">
                            <div class="col-sm-3">
                              <label class="col-sm-3 control-label" style="width: 39%;">Freight</label>
                              <div class="col-sm-8" style="width: 61%;">
                            <asp:TextBox ID="txtFreight" runat="server" CssClass="form-control" MaxLength="100" oncopy="return false"  oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="10" Text="0.00" Enabled="false"></asp:TextBox>
                              </div>
                              
                            </div>
                           	<div class="col-sm-3">
                           		<label class="col-sm-3 control-label" style="width: 39%;">Serv.Charges</label>
                              <div class="col-sm-9" style="width: 61%;">
                               <asp:TextBox ID="txtServiceCharg" runat="server" CssClass="form-control" MaxLength="100" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                              TabIndex="11"  Text="0.00"  OnTextChanged="txtServiceCharg_TextChanged" AutoPostBack="true"></asp:TextBox>
                              </div>
                           	</div>
                           	<div class="col-sm-3">
                           		<label class="col-sm-3 control-label" style="width: 39%;">labr.Charges</label>
                              <div class="col-sm-3" style="width: 61%;">
                               <asp:TextBox ID="txtLabbrCharg" runat="server" CssClass="form-control" MaxLength="100" oncopy="return false"
                                  oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="12"  Text="0.00" OnTextChanged="txtLabbrCharg_TextChanged" AutoPostBack="true"></asp:TextBox>
                              </div>
                           	</div>
                           	<div class="col-sm-3">
                           		<label class="col-sm-3 control-label" style="width: 39%;">Deliv.Amnt.</label>
                              <div class="col-sm-3" style="width: 61%;">
                                <asp:TextBox ID="txtDelvAmnt" runat="server" CssClass="form-control" MaxLength="100" oncopy="return false"  oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="13"
                                 Text="0.00" OnTextChanged="txtDelvAmnt_TextChanged" AutoPostBack="true"></asp:TextBox>
                              </div>
                           	</div>
                          </div>

                          <div class="clearfix odd_row">
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 29%;">OctraiAmnt.</label>
                              <div class="col-sm-8" style="width: 71%;">
                               <asp:TextBox ID="txtOctraiAmnt" runat="server" CssClass="form-control" MaxLength="100" oncopy="return false"  oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="14"
                                Text="0.00" OnTextChanged="txtOctraiAmnt_TextChanged" AutoPostBack="true"></asp:TextBox>
                              </div>
                            </div>
                            <div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 29%;">Damage</label>
                              <div class="col-sm-9" style="width: 71%;">
                                <asp:TextBox ID="txtDamage" runat="server" CssClass="form-control" MaxLength="100" oncopy="return false"  oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="15"
                                 Text="0.00" OnTextChanged="txtDamage_TextChanged" AutoPostBack="true"></asp:TextBox>
                              </div>
                           	</div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 29%;">Net Total</label>

                              <div class="col-sm-3" style="width: 71%;">
                                 <asp:TextBox ID="txtNetTotal" runat="server" CssClass="form-control" MaxLength="100" oncopy="return false"  oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="16"
                                 Text="0.00" Enabled="false"></asp:TextBox>
                              </div>
                           	</div>                           	
                          </div>

                          <div class="clearfix even_row">
                          	<label class="col-sm-3 control-label" style="width: 9.5%;">Remark</label>
                            <div class="col-sm-9" style="width: 90.5%;">
                              <asp:TextBox ID="txtremark" runat="server" Placeholder="Enter Remark" CssClass="form-control" MaxLength="100" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="17" ></asp:TextBox>
                            
                            </div>
                          </div>

                        </div>
                      </section>                        
                    </div>
                    
                     <!-- fourth row -->
                    <div class="clearfix odd_row">
                      <div class="col-lg-4"></div>
                      <div class="col-lg-4">   
                       <div class="col-sm-4">
                        <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-info" OnClick="lnkbtnNew_OnClick" TabIndex="20" ><i class="fa fa-file-o"></i>New</asp:LinkButton>
                       </div>                                     
                        <div class="col-sm-4">
                       

                           <asp:HiddenField ID="hidmindate" runat="server" />
                            <asp:HiddenField ID="hidmaxdate" runat="server" />
                                <asp:HiddenField ID="hidGrIdno" runat="server" />
                        <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" ValidationGroup="Save" CssClass="btn full_width_btn btn-s-md btn-success" TabIndex="18" OnClick="lnkbtnSave_OnClick" ><i class="fa fa-save"></i>Save</asp:LinkButton>  
                          
                        </div>
                        <div class="col-sm-4">
                        <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-danger" OnClick="lnkbtnCancel_OnClick" TabIndex="19" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
                        
                        </div>
                      </div>
                      <div class="col-lg-4"></div>
                    </div>

                    <!-- popup form GR detail -->
					<div id="dvGrdetails" class="modal fade">
										  <div class="modal-dialog">
										    <div class="modal-content">
										      <div class="modal-header">
										        <h4 class="popform_header">GR Detail&nbsp;&nbsp;  <asp:Label ID="lblmsg2" runat="server" Text="Message - Please select only one GR at a time."
                                                    Visible="false"></asp:Label> </h4>
                                               

										      </div>
										      <div class="modal-body">
										        <section class="panel panel-default full_form_container material_search_pop_form">
										          <div class="panel-body">
										             <!-- First Row start -->
										            <div class="clearfix odd_row">	                                
	                                <div class="col-sm-6">
	                                  <label class="col-sm-4 control-label">Date From</label>
                                    <div class="col-sm-8">

                                      <asp:TextBox ID="txtDateFromDiv" runat="server"  CssClass="input-sm datepicker form-control" data-date-format="dd-mm-yyyy" TabIndex="90"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtFrm" runat="server" ErrorMessage="Enter From Date!"
                                            Display="Dynamic" CssClass="redfont" ControlToValidate="txtDateFromDiv" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                    </div>
	                                </div>
	                                <div class="col-sm-6">
	                                  <label class="col-sm-4 control-label">Date To</label>
                                    <div class="col-sm-8">

                                      <asp:TextBox ID="txtDateToDiv" runat="server" CssClass="input-sm datepicker form-control" data-date-format="dd-mm-yyyy"  TabIndex="91"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtTo" runat="server" ErrorMessage="Enter To Date!"
                                            Display="Dynamic" CssClass="redfont" ControlToValidate="txtDateToDiv" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>

                                    </div>
	                                </div>
	                              </div> 

	                              <div class="clearfix even_row">
	                                <div class="col-sm-6">
	                                	<label class="col-sm-4 control-label">GR No.</label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtSrchGrNo" runat="server" TabIndex="92" Placeholder="Enter Gr No." CssClass="form-control" MaxLength="10" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                           ></asp:TextBox>

                                    </div>
	                                </div>
	                                <div class="col-sm-6" style="padding: 0;">
	                                  <div class="col-sm-4 prev_fetch">                               
                                    <asp:LinkButton ID="lnkbtnSearch" runat="server"  TabIndex="93" CssClass="btn full_width_btn btn-sm btn-primary" CausesValidation="true" ValidationGroup="GRDetailsSrch" OnClick="lnkbtnSearch_OnClick"><i class="fa fa-search"></i>Search</asp:LinkButton>                                       
	                                  </div>
	                                  <div class="col-sm-8"> 
	                                
	                                  </div>
	                                </div>
	                              </div> 
	                              <div class="clearfix">
                                   <section class="panel panel-default full_form_container material_search_pop_form">
		                            <div class="panel-body" style="overflow-x:auto;">   
                                     <asp:GridView ID="grdGrdetals" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="display nowrap dataTable"
                                            Width="100%" BorderStyle="None" BorderWidth="0" >
                                            <RowStyle CssClass="odd" />
                                             <AlternatingRowStyle CssClass="even" />   
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select" HeaderStyle-Width="40px">
                                                    <HeaderStyle Width="40" HorizontalAlign="Center" />
                                                    <ItemStyle Width="40" HorizontalAlign="Center" />
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAllCheckboxes(this);"
                                                            CssClass="SACatA" />
                                                        <%--  <asp:LinkButton ID="lnk" runat="server" Text="Select" CssClass="link" CommandName="cmdselect"
                                                            CommandArgument='<%#Eval("ID") %>'></asp:LinkButton>--%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkId" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gr No." HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("Gr_No"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Gr Date" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToDateTime(Eval("GR_Date")).ToString("dd-MMM-yyyy")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gr From" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%#Eval("GR_From")%>
                                                        <%-- <asp:Label ID="lbltocity" runat="server" Text=' <%#Eval("ToCity")%>'></asp:Label>--%>
                                                        <asp:HiddenField ID="hidGrIdno" runat="server" Value='<%#Eval("GR_Idno")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Receiver Name" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="180px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Eval("Recvr_Name")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="From City" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="180px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Eval("From_City")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Right">
                                                    <ItemStyle Width="20px" HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        &nbsp;
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
								<div class="modal-footer">
								<div class="popup_footer_btn"> 
                                    <asp:LinkButton ID="lnkbtnSubmitok" runat="server" CssClass="btn btn-dark" OnClick="lnkbtnSubmitok_OnClick" ><i class="fa fa-check"></i>Ok</asp:LinkButton>
									
									<button type="submit" id="btnbtnClear" runat="server" class="btn btn-dark" data-dismiss="modal"><i class="fa fa-times"></i>Close</button>
								</div>
								</div>
							</div>
							</div>
						</div>          
                  </form>
                </div>
              </section>
            </div>
            <div class="col-lg-1"></div>
          </div>
         
            <table>
           <tr style="display: none">
                    <td class="white_bg" align="center">
                        <div id="print" style="font-size: 10px;">
                            <table cellpadding="1" cellspacing="0" width="300px" border="1" style="font-family: Arial,Helvetica,sans-serif;">
                                <tr>
                                    <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 10px;
                                        border-left-style: none; border-right-style: none">
                                        <strong>
                                            <asp:Label ID="lblCompanyname" runat="server" Style="font-size: 10px;"></asp:Label><br />
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
                                    <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 10px;
                                        border-left-style: none; border-right-style: none">
                                        <h3>
                                            <strong style="text-decoration: underline">
                                                <asp:Label ID="lblPrintHeadng" runat="server" Text="Freight Memo"></asp:Label></strong></h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    <asp:Label ID="lbltextrecp" Text="Receipt No." runat="server"></asp:Label>
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    :
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    <asp:Label ID="lblrecpt" runat="server"></asp:Label>
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    <asp:Label ID="lbltextgrdate" Text="GR Date" runat="server"></asp:Label>
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    :
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    <asp:Label ID="lblGRDate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    <asp:Label ID="lbltextBilty" Text="BilltyNo" runat="server"></asp:Label>
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    :
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    <asp:Label ID="lblBillty" runat="server"></asp:Label>
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    <asp:Label ID="lbltxtQty" Text="Qty" runat="server"></asp:Label>
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    :
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    <asp:Label ID="lblQty" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    <asp:Label ID="lbltxtfrmCity" Text="From City" runat="server"></asp:Label>
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    :
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    <asp:Label ID="lblfromcity" runat="server"></asp:Label>
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    <asp:Label ID="lbltxtweight" runat="server" Text="Weight"></asp:Label>
                                                </td>
                                                <td id="Tdlbldriver" align="left" class="white_bg" valign="top" style="font-size: 9px;
                                                    border-right-style: none" runat="server">
                                                    :
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    <asp:Label ID="lblweight" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                         
                                        </table>
                                        <table border="0" width="100%">
                                             <tr>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none;width:50px;">
                                                    <asp:Label ID="Label6" Text="Receiver" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;:                                                </td>
                                               
                                          
                                               <td align="left" class="white_bg"  valign="top" style="font-size: 9px; border-right-style: none">
                                                      <asp:Label ID="lblPrty"  runat="server"></asp:Label>
                                                </td>
                                             
                                            </tr>
                                        </table>
                                          <table  width="100%">
                                             <tr>
                                                <td align="left"class="white_bg" valign="top" style="font-size: 9px; border-right-style: none;width:100%;">
                                                   Throught                                     
                                                    </td>
       
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table border="0" cellspacing="0" style="font-size: 9px" width="100%" id="Table1">
                                            <tr>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; width: 23%;
                                                    border-right-style: none">
                                                </td>
                                                <td align="right" class="white_bg" valign="top" style="font-size: 9px; width: 23%;
                                                    border-right-style: none">
                                                  
                                                </td>
                                                <td align="center" class="white_bg" valign="top" style="font-size: 9px; width: 6%;
                                                    border-right-style: none">
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; width: 23%;
                                                    border-right-style: none;"right" class="white_bg" valign="top" style="font-size: 9px; width: 23%;
                                                    border-right-style: none">
                                                    <b>Rs.</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    &nbsp;</td>
                                                <td align="right" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    &nbsp;</td>
                                                <td align="center" class="white_bg" valign="top" style="font-size: 9px; width: 6%;
                                                    border-right-style: none">
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    <b>
                                                        <asp:Label ID="lbltxtfreight" Text="Freight" runat="server"></asp:Label>
                                                    </b>
                                                </td>
                                                <td align="right" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    <asp:Label ID="lblValueFreight" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    &nbsp;</td>
                                                <td align="right" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    &nbsp;</td>
                                                <td align="center" class="white_bg" valign="top" style="font-size: 9px; width: 6%;
                                                    border-right-style: none">
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    <b>
                                                        <asp:Label ID="lbltextSC" Text="S.C" runat="server"></asp:Label></b>
                                                </td>
                                                <td align="right" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    <asp:Label ID="lblvalueSC" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    &nbsp;</td>
                                                <td align="right" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    &nbsp;</td>
                                                <td align="center" class="white_bg" valign="top" style="font-size: 9px; width: 6%;
                                                    border-right-style: none">
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    <b>
                                                        <asp:Label ID="lbltxtLabour" Text="Labour" runat="server"></asp:Label></b>
                                                </td>
                                                <td align="right" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    <asp:Label ID="lblValuelabour" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    &nbsp;</td>
                                                <td align="right" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    &nbsp;</td>
                                                <td align="center" class="white_bg" valign="top" style="font-size: 9px; width: 6%;
                                                    border-right-style: none">
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                <b><asp:Label ID="Label7" Text="D.C" runat="server"></asp:Label></b>
                                                </td>
                                                <td align="right" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                   <asp:Label ID="lblValueDC" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    &nbsp;</td>
                                                <td align="right" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    &nbsp;</td>
                                                <td align="center" class="white_bg" valign="top" style="font-size: 9px; width: 6%;
                                                    border-right-style: none">
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                <b><asp:Label ID="Label5" Text="Others" runat="server"></asp:Label></b>
                                                </td>
                                                <td align="right" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                   <asp:Label ID="lblValueOthers" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                                                     <tr>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    &nbsp;</td>
                                                <td align="right" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    &nbsp;</td>
                                                <td align="center" class="white_bg" valign="top" style="font-size: 9px; width: 6%;
                                                    border-right-style: none">
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                <b><asp:Label ID="Label8" Text="Damage" runat="server"></asp:Label></b>
                                                </td>
                                                <td align="right" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                   <asp:Label ID="lblvalueDamage" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="5">
                                                    <hr style="width:100%" />
                                                </td>
                                            </tr>
                                            <tr id="tr1" runat="server" style="border: 1px">
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    &nbsp;</td>
                                                <td align="right" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    &nbsp;</td>
                                                <td align="center" class="white_bg" valign="top" style="font-size: 9px; width: 6%;
                                                    border-right-style: none">
                                                </td>
                                                <td align="left" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    <b>
                                                        <asp:Label ID="lbltxttotal2" Text="Total" runat="server"></asp:Label></b>
                                                </td>
                                                <td align="right" class="white_bg" valign="top" style="font-size: 9px; border-right-style: none">
                                                    <asp:Label ID="lblvaluetotal2" runat="server"></asp:Label>
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
            
            <asp:HiddenField ID="hidItemidno" runat="server" />
      <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
    <script language="javascript" type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_beginRequest(function () {
            SetFocus();
            setDatecontrol();
        });

        prm.add_endRequest(function () {
            SetFocus();
            setDatecontrol();
        });

        $(document).ready(function () {
            setDatecontrol();
        });

        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $("#<%=txtGRDate.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });


            $("#<%=txtDateFromDiv.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
            $("#<%=txtDateToDiv.ClientID %>").datepicker({
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
    <script>
        //$(document).ready(function () {
        $(".datepicker").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
            minDate: '<%=hidmindate.Value%>',
            maxDate: '<%=hidmaxdate.Value%>'
        });
        //});
    </script>
</asp:Content>
