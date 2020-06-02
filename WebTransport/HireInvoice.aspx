<%@ Page Title="Hire Lorry Invoice" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="HireInvoice.aspx.cs" Inherits="WebTransport.HireInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-11">
            <section class="panel panel-default full_form_container quotation_master_form">
        <header class="panel-heading font-bold form_heading">INVOICE [Own To HIRE]
        <span class="view_print">
        <asp:LinkButton ID="lnkBtnLast" class="view_print"  runat="server"  AlternateText="Print" title="Print" Height="16px" onclick="lnkBtnLast_Click">LAST PRINT</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="ManageHireInvoice.aspx" ><asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a>&nbsp;
         <asp:ImageButton ID="ImgPrint2" runat="server" AlternateText="Print General" ImageUrl="~/images/print.jpeg"
            Visible="true" title="Print General" OnClientClick="return CallPrint();"
            Height="16px" />
        </span>
        <asp:HiddenField ID="hidid" runat="server" Value="" />
        <asp:HiddenField ID="hideimgvalue" runat="server" />
        <asp:HiddenField ID="printFormat1" runat="server" />
        <asp:HiddenField ID="iMaxInvIdno1" runat="server" />
        <asp:HiddenField ID="hideUserPref" runat="server" Value="" />
        <asp:HiddenField ID="hidPrintType" runat="server" />
        <asp:HiddenField ID="hidePrintMultipal" runat="server" />
        <asp:HiddenField ID="hidrefno" runat="server" />
        </header>
        <div class="panel-body">
        <form class="bs-example form-horizontal">
        <!-- first  section --> 
        <div class="clearfix first_section">
            <section class="panel panel-in-default">  
            <div class="panel-body">

             <div class="clearfix odd_row">
                <div class="col-sm-4">
                        <label class="col-sm-4 control-label">Date Range<span class="required-field">*</span></label>
                            <div class="col-sm-8">
                            <asp:DropDownList ID="ddldateRange" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddldateRange_SelectedIndexChanged"
                                Width="200px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddldateRange"
                                CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Year!"
                                InitialValue="0" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                            </div>
                    </div>
                <div class="col-sm-2">
                     <label class="col-sm-3 control-label"> Date</label>
                      <div class="col-sm-8">
                                                    <asp:TextBox ID="txtdate" runat="server" CssClass="input-sm datepicker form-control" Width="200"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter Date!"
                                                        Display="Dynamic" CssClass="redfont" ControlToValidate="txtdate" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                      </div>
                      </div>
                <div class="col-sm-3">
                    <label class="col-sm-5 control-label">Invoice No.<span class="required-field">*</span></label>
                           <div class="col-sm-7" >
                            <asp:TextBox ID="txtinvoicNo" Enabled="false" runat="server" PlaceHolder="Invoice No." CssClass="form-control"  MaxLength="7"
                                oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                 Text=""></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtinvoicNo"
                                CssClass="classValidation" Display="Dynamic" ErrorMessage="&lt;br/&gt;Please enter Invoice no!"
                                SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </div>
                </div>  
                               <div class="col-sm-3">
                                         <label class="col-sm-4 control-label">Location<span class="required-field">*</span></label>
                                                <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddllocation" runat="server" CssClass="form-control" >
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddllocation"
                                                        CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Location"
                                                        InitialValue="0" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                </div>
                              
                               </div>
                   
                </div>
                <div class="clearfix even_row">
                 
                       <div class="col-sm-4">
                  <label class="col-sm-4 control-label">Lorry No.<span class="required-field">*</span></label>
                                  <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlTruckNo" CssClass="chzn-select" runat="server" 
                                          Width="200px" >
                                    </asp:DropDownList>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTruckNo"
                                            CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Truck"
                                            InitialValue="0" SetFocusOnError="true" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                  </div>
                </div>
                       <div class="col-sm-1" style="width: 10%; display:none">
                               <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/Images/PckLst.png" AlternateText="Search"
                                                        ImageAlign="ABSMiddle" ToolTip="Search" Style="height: 28px" OnClick="imgSearch_Click"
                                                        TabIndex="5" />
                              </div>
                  <div class="col-sm-7">
                  </div>
                </div>




                </div>
                </section>




<!--Popup-->
<div id="dvGrdetails" class="modal fade" >
							<div class="modal-dialog">
							<div class="modal-content" style="width:700px">
								<div class="modal-header">
								<h4 class="popform_header">GR Detail </h4>
								</div>
								<div class="modal-body">
								<section class="panel panel-default full_form_container material_search_pop_form">
									<div class="panel-body">
										<!-- First Row start -->
									<div class="clearfix odd_row">	   
                                                              
	                                <div class="col-sm-4">
	                                  <label class="col-sm-5 control-label">Date From</label>
                                    <div class="col-sm-7">
                                     <asp:TextBox ID="txtDateFrm" runat="server" CssClass="input-sm datepicker form-control"  TabIndex="85" data-date-format="dd-mm-yyyy"></asp:TextBox>                                     
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Enter From Date!"
                                            Display="Dynamic" CssClass="classValidation" ControlToValidate="txtDateFrom" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                     
                                    </div>
	                                </div>
	                                <div class="col-sm-4">
	                                  <label class="col-sm-5 control-label">Date To</label>
                                    <div class="col-sm-7">
                                       <asp:TextBox ID="txtDatetwo" runat="server" CssClass="input-sm datepicker form-control" TabIndex="86" data-date-format="dd-mm-yyyy"></asp:TextBox>                                     
                                     
                                    </div>
	                                </div>
                                    <div class="col-sm-4">
                                     <div class="col-sm-6 prev_fetch">                                      
                                        <asp:LinkButton ID="lnkbtnSearch" runat="server" CssClass="btn full_width_btn btn-sm btn-primary" CausesValidation="true" TabIndex="89" ValidationGroup="RcptEntrySrch" OnClick="lnkbtnSearch_OnClick"><i class="fa fa-search"></i>Search</asp:LinkButton>	                                
	                                  </div>
	                                  <div class="col-sm-6"> 
	                                     <label class="control-label">T. Record(s) : </label>
	                                  </div>
                                    
                                    </div>   
	                                
	                              </div> 

	                               
                                  
                                  <div class="clearfix fourth_right">
                        <section class="panel panel-in-default btns_without_border">                            
                          <div class="panel-body">     
                            <div class="clearfix">
		                          <section class="panel panel-default full_form_container material_search_pop_form">
		                            <div class="panel-body">
                                      <asp:GridView ID="grdGrdetals" runat="server" GridLines="None" AutoGenerateColumns="false"
                                            Width="100%" BorderStyle="None" CssClass="display wrap dataTable"
                                            BorderWidth="0" >
                                           <RowStyle CssClass="odd" />
                                        <AlternatingRowStyle CssClass="even" />  
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select" HeaderStyle-Width="40px">
                                                     <HeaderStyle Width="40" CssClass="gridHeaderAlignCenter" />
                                                <ItemStyle Width="40" CssClass="gridHeaderAlignCenter" />
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAllCheckboxes(this);" TabIndex="90"
                                                            CssClass="SACatA" />
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
                                                <asp:TemplateField HeaderText="Gr Date" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MMM-yyyy")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="From City" HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                     <%#Convert.ToString(Eval("From_City"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To City" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="180px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Eval("To_City")%>
                                                        <asp:HiddenField ID="hidGrIdno" runat="server" Value='<%#Eval("Gr_Idno")%>' />
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
                                <asp:LinkButton ID="lnkbtenSubmit" runat="server" CssClass="btn btn-dark"  TabIndex="91" OnClick="lnkbtenSubmit_OnClick"><i class="fa fa-check"></i>Ok</asp:LinkButton>
								<asp:LinkButton ID="lnkbtnCloase" runat="server" CssClass="btn btn-dark"  TabIndex="92"><i class="fa fa-times"></i>Close</asp:LinkButton>

						        <div style="float:left;">                              
                                    <asp:Label ID="Label3" runat="server" CssClass="redfont"></asp:Label>
                                </div>
							</div>
							</div>
						</div>
						</div>
					</div>
                
                </div>
                <div class="clearfix Second_section">
                <section class="panel panel-in-default btns_without_border">                            
	                          <div class="panel-body" style="overflow:auto;">     
                              <asp:GridView ID="GrMainDetail" runat="server" AutoGenerateColumns="false" 
                                      BorderStyle="None" CssClass="display wrap dataTable"
                                    Width="100%" GridLines="Both" EnableViewState="true" AllowPaging="true" BorderWidth="0"
                                    ShowFooter="true" PageSize="30" >
                                    <RowStyle CssClass="odd" />
                                    <AlternatingRowStyle CssClass="even" />    
                                    <Columns>
                                                                                                                      
                                        <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="20px" />
                                            <ItemStyle Width="20px" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Left" />
                                            <FooterTemplate>
                                             
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                     
                                        <asp:TemplateField HeaderText="Gr No." HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("Gr_No"))%>
                                                </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Gr Date" HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                            <ItemStyle Width="40" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrDate" runat="server" Text='<%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Challan No." HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                            <ItemStyle Width="40" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                 <%#Convert.ToString(Eval("Chln_No"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Challan Date" HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                            <ItemStyle Width="40" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblChlnDate" runat="server" Text='<%#Convert.ToDateTime(Eval("Chln_Date")).ToString("dd-MM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Party Name" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("Owner_Name"))%>
                                             
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Location From" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("From_City"))%>
                                                   
                                                </ItemTemplate> 
                                        </asp:TemplateField>

                                          
                                      
                                      <asp:TemplateField HeaderText="To City" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("To_City"))%>
                                                  </ItemTemplate>
                                         
                                        </asp:TemplateField>
                                         
                                          
                                        <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                            <ItemStyle Width="70" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblamnt" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("Total_Amnt"))) ? "" : (Convert.ToString((Eval("Total_Amnt")))))%>'></asp:Label>
                                           
                                            </ItemTemplate>
                                             <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblamnte" runat="server"></asp:Label>
                                                </FooterTemplate>
                                        </asp:TemplateField>                                                                         
                                    </Columns>
                                </asp:GridView>
	                          </div>
	                        </section> 
                  </div>


                
                  <div class="clearfix first_section">
            <section class="panel panel-in-default">  
            <div class="panel-body">


             <div class="clearfix odd_row">
                 
                          <div class="col-sm-4">
                            <label class="col-sm-4 control-label">Party Name<span class="required-field">*</span></label>
                             <div class="col-sm-7">
                                            <asp:DropDownList ID="ddlpartyname" runat="server"   CssClass="chzn-select" Width="200px"  >
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlpartyname"
                                            CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Party"
                                            InitialValue="0" SetFocusOnError="true" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>
                            </div>
                               <div class="col-sm-4">
                                         <label class="col-sm-5 control-label">From City<span class="required-field">*</span></label>
                                                <div class="col-sm-7">
                                                        <asp:DropDownList ID="ddlFromCity" onchange="javascript:cityviaddl();" runat="server" CssClass="form-control" >
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlFromCity"
                                                        CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select From city!"
                                                        InitialValue="0" SetFocusOnError="true" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                </div>
                              
                               </div>
                                <div class="col-sm-4">
                                    <label class="col-sm-4 control-label">City To<span class="required-field">*</span></label>
	                                           <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlToCity"  runat="server"  Width="200px"  CssClass="form-control"   >
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlCity" runat="server" ErrorMessage="Please select city!"
                                                    ControlToValidate="ddlToCity" Display="Dynamic" SetFocusOnError="true" InitialValue="0"
                                                    CssClass="classValidation" ValidationGroup="Submit"></asp:RequiredFieldValidator>	                                
	                               
                                    </div>
                            </div>
                            
                
                </div>
             
             <div class="clearfix even_row">
                    
                              <div class="col-sm-4">
                                    <label class="col-sm-4 control-label">Via City <span class="required-field">*</span></label>
	                                           <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlviacity"  runat="server"  Width="200px"  CssClass="form-control"   >
                                                </asp:DropDownList>                    
	                               
                                    </div>
                            </div>
                            <div class="col-sm-4">
	                                        <label class="col-sm-5 control-label">Date From<span class="required-field">*</span></label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="input-sm datepicker form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvRcptEntryDtFrm" runat="server" ErrorMessage="Enter From Date!" InitialValue=""
                                                        Display="Dynamic" CssClass="redfont" ControlToValidate="txtDateFrom" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                                </div>
                            </div>
                             <div class="col-sm-4">
                                           <label class="col-sm-4 control-label">Return Date</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtDateTo" runat="server"  Width="200px"  CssClass="input-sm datepicker form-control"></asp:TextBox>
                                                </div>     
                             </div>  
               </div>
             
             <div class="clearfix odd_row">
                  <div class="col-sm-4">
                               

                           	</div>
              <%--    <div class="col-sm-4">
	                                <label class="col-sm-5 control-label">Amount<span class="required-field">*</span></label>
		                                      <div class="col-sm-7">
		                                 <asp:TextBox ID="txtamount" runat="server" Width="200px" CssClass="form-control"  Style="text-align: right;" Text="0.00" onKeyPress="return checkfloat(event, this);" onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox>                                                                                           
                                           <asp:RequiredFieldValidator ID="rfvWeight" runat="server" ErrorMessage="Please enter Amount!"
                                            ControlToValidate="txtamount" Display="Dynamic" SetFocusOnError="true" InitialValue="0.00" CssClass="classValidation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
	                               
                                        </div>                               
	                                </div>--%>
              <%--   <div class="col-sm-3">
	                                <div class="col-sm-6">
                                    <asp:LinkButton ID="lnkbtnSubmit" runat="server"   
                                            class="btn full_width_btn btn-sm btn-primary"   
                                            ToolTip="Click to Submit" CausesValidation="true" ValidationGroup="Submit" 
                                            onclick="lnkbtnSubmit_Click" >Submit</asp:LinkButton>                                   
                                  </div>
                                
                                  <div class="col-sm-6">
                                   <asp:LinkButton ID="lnkbtnsumnew" runat="server"  
                                          class="btn full_width_btn btn-sm btn-primary"  
                                          ToolTip="Click to new" onclick="lnkbtnsumnew_Click" >New</asp:LinkButton>
                                  </div>
	              </div>--%>
                 </div>

            </div>
            </section>                        
        </div>
                 <div class="clearfix third_right">
	                        <section class="panel panel-in-default btns_without_border">                            
	                          <div class="panel-body" style="overflow:auto;">     
                              <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" 
                                      BorderStyle="None" CssClass="display wrap dataTable"
                                    Width="100%" GridLines="Both" EnableViewState="true" AllowPaging="true" BorderWidth="0"
                                    ShowFooter="true" PageSize="30" onrowcommand="grdMain_RowCommand" 
                                      onrowdatabound="grdMain_RowDataBound">
                                    <RowStyle CssClass="odd" />
                                    <AlternatingRowStyle CssClass="even" />    
                                    <Columns>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                              <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("id") %>' CommandName="cmdedit"  ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                 <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("id") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete"  ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>                                          
                                            </ItemTemplate>
                                        </asp:TemplateField>                                                                              
                                        <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="20px" />
                                            <ItemStyle Width="20px" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Left" />
                                            <FooterTemplate>
                                                <b>Total</b>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                     
                                        <asp:TemplateField HeaderText="Lorry No." HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("TruckName"))%>
                                                <asp:Label ID="lbltruckidno" Visible="false"  runat="server" Text='<%#Eval("Truck_Idno")%>'></asp:Label>
                                            </ItemTemplate>
                                             <FooterStyle HorizontalAlign="Left" />
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotRecords" runat="server" Visible="false" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Party Name" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("PartyName"))%>
                                                <asp:Label ID="lblCityViaIdno" Visible="false"  runat="server" Text='<%#Eval("Party_Idno")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Location From" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("FrmCityName"))%>
                                                     <asp:Label ID="lblfrmcityidno" Visible="false"  runat="server" Text='<%#Eval("FrmCity_idno")%>'></asp:Label>
                                                </ItemTemplate> 
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Via City" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("ViaCityName"))%>
                                                     <asp:Label ID="lblVaicityidno" Visible="false"  runat="server" Text='<%#Eval("ViaCity_Idno")%>'></asp:Label>
                                                </ItemTemplate> 
                                        </asp:TemplateField>
                                      
                                      <asp:TemplateField HeaderText="To City" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("ToCityName"))%>
                                                   <asp:Label ID="lbltocityidno" Visible="false"  runat="server" Text='<%#Eval("ToCity_Idno")%>'></asp:Label>
                                                </ItemTemplate>
                                         
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Date From" HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                            <ItemStyle Width="40" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbldatefrom" runat="server" Text='<%#Convert.ToDateTime(Eval("From_Date")).ToString("dd-MM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Return Date" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblreturndate" runat="server" Text='<%#string.IsNullOrEmpty(Convert.ToString(Eval("Return_Date")))?"":Convert.ToDateTime(Eval("Return_Date")).ToString("dd-MM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right" Visible="false">
                                            <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                            <ItemStyle Width="70" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblamount" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("Amount"))) ? "" : (Convert.ToString((Eval("Amount")))))%>'></asp:Label>
                                           
                                            </ItemTemplate>
                                             <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                                </FooterTemplate>
                                        </asp:TemplateField>                                                                         
                                    </Columns>
                                </asp:GridView>
	                          </div>
	                        </section>
	                      </div> 

              <!-- new  section -->
        <div class="clearfix second_section" id="DivItemPanel" runat="server">
            <section class="panel panel-in-default">  
                        <div class="panel-body" >
                          <div class="clearfix even_row">
                            <div class="col-sm-2">
                              <label class="control-label">Item Name<span class="required-field">*</span></label>
                              <div>
                                <asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control" >
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPartno" runat="server" ControlToValidate="ddlItemName" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="ValueSubmit"
                                    ErrorMessage="Select Item Name." CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                           	<div class="col-sm-2" style="width:11%">
                              <label class="control-label">Unit<span class="required-field">*</span></label>
                              <div>
                                <asp:DropDownList ID="ddlunitname" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvAmnt" runat="server" ControlToValidate="ddlunitname" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="ValueSubmit"
                                    ErrorMessage="Choose Unit Name." CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-2" style="width:10%">
                              <label class="control-label">Rate Type<span class="required-field">*</span></label>
                              <div>
                              <asp:DropDownList ID="ddlRateType" AutoPostBack="false" onchange="javascript:OnChangeddlRateType();" runat="server" CssClass="form-control" >
                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Qty" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Weight" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                              </div>
                            </div>
                            <div class="col-sm-2" style="width:8%">
                              <label class="control-label">Quantity<span class="required-field">*</span></label>
                              <div>
                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"  MaxLength="6"
                                Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtQuantity_TextChanged"
                                onKeyPress="return checkfloat(event, this);" oncopy="return false" onpaste="return false"
                                oncut="return false" oncontextmenu="return false">1</asp:TextBox>

                              </div>
                            </div>
                            <div class="col-sm-2" style="width:10%" ID="DivWight" runat="server">
                              <label class="control-label">Weight<span class="required-field">*</span></label>
                              <div>
                                <asp:TextBox ID="txtweight" runat="server" CssClass="form-control" MaxLength="10"
                                 onKeyPress="return checkfloat(event, this);" oncopy="return false"
                                onpaste="return false" oncut="return false" oncontextmenu="return false" ></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtweight" Display="Dynamic" SetFocusOnError="true"   ValidationGroup="ValueSubmit"
                                    ErrorMessage="Please Enter Weight!" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                           	<div class="col-sm-2" style="width:10%"  ID="DivRate" runat="server">
                              <label class="control-label">Rate<span class="required-field">*</span></label>
                              <div>
                                <asp:TextBox ID="txtrate" runat="server" CssClass="form-control" MaxLength="10"
                                    onKeyPress="return checkfloat(event, this);" oncopy="return false"
                                    onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtrate" runat="server" ControlToValidate="txtrate" Display="Dynamic" SetFocusOnError="true" ValidationGroup="ValueSubmit" 
                                    ErrorMessage="Enter Rate." CssClass="classValidation"></asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="CvtxtRate" runat="server" ControlToValidate="txtrate" OnServerValidate="CvtxtRate_ServerValidate"
                                    CssClass="classValidation" ErrorMessage="Rate Cannot Be Zero." />
                              </div>
                            </div>
                              <div class="col-sm-2" style="width:10%"  ID="Divunloading" runat="server">
                              <label class="control-label">U/L(%)<span class="required-field">*</span></label>
                              <div>
                                <asp:TextBox ID="txtul" runat="server" CssClass="form-control" Text="0" MaxLength="3"
                                    oncopy="return false"
                                    onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                              </div>
                            </div>
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-9">	                                
                                <label class="col-sm-1 control-label">Detail</label>
                                <div class="col-sm-11">
                                <asp:TextBox ID="txtdetail" PlaceHolder="Enter Detail" runat="server" CssClass="form-control" MaxLength="150"></asp:TextBox><%--DB-Max length 200--%>
                                </div> 
                            </div>
                            <div class="col-sm-3">
                                <div class="col-sm-6">
                                    <asp:LinkButton ID="lnksubmit_1" runat="server" style="margin-top:0px;" OnClick="lnkbtnSubmit_OnClick" CssClass="btn full_width_btn btn-sm btn-primary subnew"  ToolTip="Click to Submit" CausesValidation="true" ValidationGroup="ValueSubmit" >Submit</asp:LinkButton>
                                </div>
                                <div class="col-sm-6">
                                    <asp:LinkButton ID="lnkbtnAdd" runat="server" style="margin-top:0px;" CausesValidation="false"  OnClick="lnkbtnAdd_OnClick" CssClass="btn full_width_btn btn-sm btn-primary subnew" ToolTip="Click to new" >New</asp:LinkButton>
                                </div>
                            </div>
                          </div>                            
                        </div>
                      </section>
        </div>
        <div class="clearfix third_right">
            <section class="panel panel-in-default">                            
                        <div class="panel-body" style="overflow-x:auto">     
                           <asp:GridView ID="grdMain2" runat="server" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="display nowrap dataTable"
                                    Width="100%" BorderStyle="None" BorderWidth="0"  OnPageIndexChanging="grdMain2_PageIndexChanging" OnRowCommand="grdMain2_RowCommand"
                                    OnRowDataBound="grdMain2_RowDataBound" PageSize="50">
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
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="100" HeaderText="U/L(%)">
                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                        <ItemTemplate>
                                         <%#Convert.ToString(Eval("UnloadWeight"))%>
                                        </ItemTemplate>
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
                  	<div class="clearfix odd_row">
                            <div class="col-sm-3">
                              <label class="col-sm-5 control-label">Adv. Amnt</label>
                              <div class="col-sm-7">
                              <asp:TextBox ID="txtAdvAmnt" runat="server" AutoPostBack="false" 
                                      CssClass="form-control" TabIndex="11"
                                MaxLength="9" oncopy="return false" oncut="return false" onDrop="blur();return false;" 
                                onkeydown="return (event.keyCode!=45);" 
                                      onKeyPress="return checkfloat(event, this);" style="text-align:right;"
                                onpaste="return false"  onchange="javascript:CalcAmnt();"   Text="0.00"></asp:TextBox>
                              </div>
                            </div>
                            <div class="col-sm-3">
                              <label class="col-sm-5 control-label">Pay. Type</label>
                              <div class="col-sm-7">
                                <asp:DropDownList ID="ddlRcptType" Enabled="false" runat="server" 
                                      CssClass="form-control" onchange="javascript:OpenText();"  AutoPostBack="false" 
                                      TabIndex="12" >
                               </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvRcptType" runat="server" ControlToValidate="ddlRcptType"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Save"  ErrorMessage="Select Receipt Type" CssClass="classValidation"></asp:RequiredFieldValidator>
                           
                              </div>
                            </div>
                           	<div class="col-sm-3">
                           		<label class="col-sm-4 control-label">Inst.Detail</label>
                           		<div class="col-sm-3">
                                   <asp:TextBox ID="txtInstNo" runat="server" CssClass="form-control" MaxLength="6"  Style="text-align: right;" TabIndex="13"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvinstno" runat="server" ControlToValidate="txtInstNo"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="" ValidationGroup="Save"
                                    ErrorMessage="Enter Inst. No" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                              <div class="col-sm-5">
                                <asp:TextBox ID="txtInstDate" runat="server" CssClass="input-sm datepicker form-control" TabIndex="14" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvinstDate" runat="server" ControlToValidate="txtInstDate"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="" ValidationGroup="Save"
                                    ErrorMessage="Enter Inst. Date!" CssClass="classValidation"></asp:RequiredFieldValidator>
                             
                              </div>
                           	</div>
                           	<div class="col-sm-3">
                              <label class="col-sm-5 control-label">Cust. Bank<span class="required-field">*</span></label>
                              <div class="col-sm-7">
                               <asp:DropDownList ID="ddlCusBank" runat="server" CssClass="form-control" TabIndex="15">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCusBank" runat="server" ControlToValidate="ddlCusBank"
                                Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Save"
                                ErrorMessage="Select Cust. Bank!" CssClass="classValidation"></asp:RequiredFieldValidator>                                      
                              </div>
                            </div>
                          </div>   

                  <div class="clearfix even_row">
                    <div class="col-sm-6">
	                                <label class="col-sm-2 control-label">Remarks</label>
		                                      <div class="col-sm-9">
		                                 <asp:TextBox ID="txtremarks" runat="server" Width="600px" CssClass="form-control"  ></asp:TextBox>
                                        </div>                               
	                                </div>
                         <div class="col-sm-3">
                              <label class="col-sm-5 control-label">Diesel Amnt</label>
                              <div class="col-sm-7">
                              <asp:TextBox ID="txtdieselamt" runat="server" AutoPostBack="false" 
                                      CssClass="form-control" TabIndex="11"
                                MaxLength="9" oncopy="return false" oncut="return false" onDrop="blur();return false;" 
                                onkeydown="return (event.keyCode!=45);" 
                                      onKeyPress="return checkfloat(event, this);" style="text-align:right;"
                                onpaste="return false"  onchange="javascript:CalcAmnt();"   Text="0.00"></asp:TextBox>
                              </div>
                            </div>
                              <div class="col-sm-3">
	                                <label class="col-sm-5 control-label">Total Amount</label>
		                                      <div class="col-sm-7">
		                                 <asp:TextBox ID="txttotalamount" onchange="javascript:CalcAmnt();" ReadOnly="true" Style="text-align: right;" runat="server" Width="200px" CssClass="form-control" ></asp:TextBox>                                                                                           
                                           
                                        </div>                               
	                                </div>
                  </div>

        <div class="clearfix odd_row">
            <div class="col-lg-2"></div>
            <div class="col-lg-10">   
              <div class="col-sm-2">
            </div>
            <div class="col-sm-2">
                        <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false" 
                            CssClass="btn full_width_btn btn-s-md btn-info" onclick="lnkbtnNew_Click" ><i class="fa fa-file-o"></i>New</asp:LinkButton>
                        <asp:HiddenField ID="hidmindate" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hidmaxdate" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hidgrossamnt" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="Hidrowid" runat="server" Value="" />
                             <asp:HiddenField ID="HidHireIdno" runat="server" Value="" />
                   <asp:HiddenField ID="Hidrowidno" runat="server" />
                   
            </div>                                         
          
            <div class="col-sm-2">
                <asp:LinkButton ID="lnkbtnSave" runat="server" 
                    CssClass="btn full_width_btn btn-s-md btn-success"   
                    CausesValidation="true" ValidationGroup="Save" onclick="lnkbtnSave_Click" > <i class="fa fa-save"></i>Save</asp:LinkButton>
             </div>
           
            <div class="col-sm-2">
                <asp:LinkButton ID="lnkbtnCancel" runat="server" 
                    CssClass="btn full_width_btn btn-s-md btn-danger"  
                    onclick="lnkbtnCancel_Click"  ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
            </div>
            </div>
        </div>

        <!--Upadhyay-Puneet-->
        <!--PRINT-GENERAL-->
        <div id="print1" style="font-size: 13px; display: none">
        <style>
            #Table4 tr:nth-child(2n)
            {
                background: #ededed;
            }
        </style>
        <table cellpadding="1" cellspacing="0" width="800" border="1" style="font-family: Arial,Helvetica,sans-serif;">
            <tr>
            <td>
            <table>
            <tr>
            <td>
             <div style="text-align:left;width:140px; float:left;">
                    <asp:Image ID="imgprintGen" Width="140px" Height="90px" runat="server" Visible="false"></asp:Image>
             </div>
                 <div style="text-align:center;float:right;padding-left:116px">
                    <strong>
                        <asp:Label ID="lblCompanyname1" runat="server" Style="font-size: 18px;"></asp:Label><br />
                    </strong>
                    <div style="font-size:11px;">
                        <asp:Label ID="lblCompAdd11" runat="server"></asp:Label>
                        <asp:Label ID="lblCompAdd22" runat="server"></asp:Label><br />
                        <asp:Label ID="lblCompCity1" runat="server"></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="lblCompState1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblCompCityPin1" runat="server"></asp:Label><br />
                        <asp:Label ID="lblCompPhNo1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblFaxNo1" Text="FAX No.:" runat="server"></asp:Label>
                        <asp:Label ID="lblCompFaxNo1" runat="server"></asp:Label><br />
                        <asp:Label ID="lblTin1" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblCompTIN1" runat="server"></asp:Label>
                    </div>
                </div>
            </td>
           </tr>
      </table>
            </td>
            </tr>
            <tr>
                <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;>
                    <h3 style="margin:7px;">
                        <strong style="text-decoration: underline">
                            <asp:Label ID="Label19" runat="server" Text="Invoice"></asp:Label></strong></h3>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table border="0" width="100%">
                        <tr>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <asp:Label ID="lbltxtgrno" Text="Invoice No." runat="server"></asp:Label>
                            </td>
                            <td>:</td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <b><asp:Label ID="lblGRno" runat="server"></asp:Label></b>
                            </td>
                            <td></td>
                            <td></td>
                            <td><span></span></td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <asp:Label ID="lbltxtPartyName" Text="Party Name" runat="server"></asp:Label>
                            </td>
                            <td>:</td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <b><asp:Label Style="text-transform:capitalize" ID="valuelbltxtPartyName" runat="server"></asp:Label></b>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <asp:Label ID="lbltxtgrdate" Text="Invoice Date" runat="server"></asp:Label>
                            </td>
                            <td>:</td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <b><asp:Label ID="lblGrDate" runat="server"></asp:Label></b>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td><span style="font-size: 13px; border-right-style: none">Party Address</span></td>
                            <td>:</td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <asp:Label ID="lblPartyAddress1" runat="server"></asp:Label> <asp:Label ID="lblPartyAddress2" runat="server"></asp:Label>
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
                                    <td class="white_bg" style="font-size: 12px" width="5%">
                                        <strong>S.No.</strong>
                                    </td>
                                    <td style="font-size: 12px" width="5%">
                                        <strong>Inv. No.</strong>
                                    </td>
                            
                                    <td style="font-size: 12px" width="9%">
                                        <strong>Inv. Date</strong>
                                    </td>
                                    <td style="font-size: 12px" width="10%">
                                        <strong>To City</strong>
                                    </td>
                                    <td style="font-size: 12px" width="18%">
                                        <strong>From City</strong>
                                    </td>
                                    <td style="font-size: 12px" width="10%">
                                        <strong>Lorry No.</strong>
                                    </td>
                                    <td style="font-size: 12px" align="right" width="12.5%">
                                        <strong>Total Amnt.</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="white_bg" width="5%">
                                        <%#Container.ItemIndex+1 %>.
                                    </td>
                                    <td class="white_bg" width="5%">
                                        <%#Eval("Inv_No")%>
                                    </td>
                                   
                                    <td class="white_bg" width="9%">
                                        <%#string.IsNullOrEmpty(Convert.ToString(Eval("Inv_Date"))) ? "" : Convert.ToDateTime(Eval("Inv_Date")).ToString("dd-MM-yyyy")%>
                                    </td>
                                    <td class="white_bg" width="10%">
                                        <%#Eval("To_City")%>
                                    </td>
                                    <td class="white_bg" width="18%">
                                        <%#Eval("From_City")%>
                                    </td>
                                    <td class="white_bg" width="5%">
                                        <%#Eval("Lorry_No")%>
                                    </td>
                                    <td class="white_bg" width="12.5%%" align="right">
                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%>
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
                    <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table5">
                        <tr>
                            <td class="white_bg" width="15%">
                            </td>
                            <td class="white_bg" width="15%">
                            </td>
                            <td class="white_bg" width="15%" align="center">
                            </td>
                            <td class="white_bg" width="15%" align="left">
                                <p style="text-align: right;">Advance Amnt.:</p>
                            </td>
                            <td class="white_bg" width="12.5%">
                                
                            </td>
                            <td class="white_bg" width="15%" align="left">
                               
                            </td>
                            <td class="white_bg" width="12.5%">
                                <asp:Label ID="valuelblAdvAmnt" style="font-weight:bold;float: right;" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="white_bg" width="15%">
                            </td>
                            <td class="white_bg" width="15%">
                            </td>
                            <td class="white_bg" width="15%" align="center">
                            </td>
                            <td class="white_bg" width="15%" align="left">
                                <p style="text-align: right;">Net Amnt.:</p>
                            </td>
                            <td class="white_bg" width="12.5%">
                            </td>
                            <td class="white_bg" width="15%">
                                
                            </td>
                            <td class="white_bg" width="12.5%" align="center">
                                <asp:Label ID="valuelblnetamntAtbttm" style="font-weight:bold;float: right;" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="11" class="white_bg" width="100%">
                                <div style="width: 15%;display: inline-block;text-align: right;"></div>
                                <div style="width: 15%;display: inline-block;text-align: right;"></div>
                                <div style="width: 15%;display: inline-block;text-align: right;"></div>
                                <div style="width: 15%;display: inline-block;text-align: right;"><span id="ContentPlaceHolder1_lblttl" style="font-weight:bold;">Total:&nbsp;&nbsp;</span></div>
                                <div style="width: 12.5%;display: inline-block;text-align: left;"></div>
                                <div style="width: 15%;display: inline-block;text-align: right;"></div>
                                <div style="width: 10.2%;display: inline-block;text-align: right;"><asp:Label ID="lblTotalAmnt" style="font-weight:bold;float: right;" runat="server"></asp:Label></div>
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
                                            <br />
                                            <b>
                                                <asp:Label ID="lblCompname1" runat="server"></asp:Label><br />
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
        <!--/PRINT-GENERAL-->
        <!-- popup form GR detail -->
                           
         <asp:HiddenField ID="HidGrIdno" runat="server" />                               
        </form>
        </div>
        </section>
        </div>
    </div>
    <script language="javascript" type="text/javascript">
        function CallPrint() {
            var prtContent1 = "<table width='100%' border='0'></table>";
            var prtContent = document.getElementById("print1");
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
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });


        function openModal() {
            $('#dvGrdetails').modal('show');
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
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function () {
            setDatecontrol();
        });

        prm.add_endRequest(function () {
            setDatecontrol();
            CalcAmnt();
        });

        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $("#<%=txtdate.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });

            $("#<%=txtDateFrm.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });



            $("#<%=txtDateFrom.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
            $("#<%=txtDateTo.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });

            $("#<%=txtDatetwo.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });

        }

        function openGridDetail() {
            $('#gr_details_form').modal('show');
        }

        function CloseModal() {
            $('#gr_details_form').Hide();
        }

        function cityviaddl() {
            var id = document.getElementById("<%=ddlFromCity.ClientID %>").value;
            document.getElementById("<%=ddlToCity.ClientID %>").value = id;
            document.getElementById("<%=ddlviacity.ClientID %>").value = id;
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
    <script type="text/javascript">

        window.onload = function () {

            if (parseFloat(document.getElementById("<%=txtAdvAmnt.ClientID %>").value) > 0) {

                document.getElementById("<%=ddlRcptType.ClientID %>").disabled = false;
                document.getElementById("<%=txtInstNo.ClientID %>").disabled = false;
                document.getElementById("<%=txtInstDate.ClientID %>").disabled = false;
                document.getElementById("<%=ddlCusBank.ClientID %>").disabled = false;

            }
            else {
                document.getElementById("<%=ddlRcptType.ClientID %>").value = document.getElementById("<%=ddlCusBank.ClientID %>").value = "0";
                document.getElementById("<%=txtInstNo.ClientID %>").value = document.getElementById("<%=txtInstDate.ClientID %>").value = "";
                document.getElementById("<%=ddlRcptType.ClientID %>").disabled = true;
                document.getElementById("<%=txtInstNo.ClientID %>").disabled = true;
                document.getElementById("<%=txtInstDate.ClientID %>").disabled = true;
                document.getElementById("<%=ddlCusBank.ClientID %>").disabled = true;

                ValidatorEnable(document.getElementById("<%=rfvCusBank.ClientID %>"), false);
                document.getElementById("<%=rfvCusBank.ClientID%>").style.visibility = "hidden";
                ValidatorEnable(document.getElementById("<%=rfvRcptType.ClientID %>"), false);
                document.getElementById("<%=rfvRcptType.ClientID%>").style.visibility = "hidden";
                ValidatorEnable(document.getElementById("<%=rfvinstno.ClientID %>"), false);
                document.getElementById("<%=rfvinstno.ClientID%>").style.visibility = "hidden";
                ValidatorEnable(document.getElementById("<%=rfvinstDate.ClientID %>"), false);
                document.getElementById("<%=rfvinstDate.ClientID%>").style.visibility = "hidden";

            }
            CalcAmnt();
        }

        function CalcAmnt() {

            if (document.getElementById("<%=txtAdvAmnt.ClientID %>").value == '') {

                document.getElementById("<%=txtAdvAmnt.ClientID %>").value = "0.00";
            }
            if (document.getElementById("<%=txtdieselamt.ClientID %>").value == '') {

                document.getElementById("<%=txtdieselamt.ClientID %>").value = "0.00";
              }
            var Grosstotal = 0; var commission = 0; var TdsAmnt = 0; var DieselAmnt = 0; var Totalamnt; var AdvAmtnt = 0; var total = 0; var damnt = 0;
            if (document.getElementById("<%=hidgrossamnt.ClientID %>").value != "") { Grosstotal = document.getElementById("<%=hidgrossamnt.ClientID %>").value; }
            if (document.getElementById("<%=txtAdvAmnt.ClientID %>").value != "") { AdvAmtnt = document.getElementById("<%=txtAdvAmnt.ClientID %>").value; }
            if (document.getElementById("<%=txtdieselamt.ClientID %>").value != "") { DieselAmnt = document.getElementById("<%=txtdieselamt.ClientID %>").value; }

            AdvAmtnt = AdvAmtnt.split(',').join('');

            total = parseFloat(AdvAmtnt);

            DieselAmnt = DieselAmnt.split(',').join('');

            damnt = parseFloat(DieselAmnt);

            var Total1 = Grosstotal;
            Total1 = Total1.split(',').join('');

            if ((total) > (Total1)) {
                alert("Advance Amount Can't be greater than total amount");
                return false;
            }
            else {
                document.getElementById("<%=txttotalamount.ClientID %>").value = (parseFloat((parseFloat(Total1) - parseFloat(total)) - parseFloat(damnt))).toFixed(2);

            }

            if ((document.getElementById("<%=txttotalamount.ClientID %>").value < 0)) {
                document.getElementById("<%=txttotalamount.ClientID %>").value = "0.00";
            }

            if (parseFloat(document.getElementById("<%=txtAdvAmnt.ClientID %>").value) > 0) {

                OpenText();
                document.getElementById("<%=ddlRcptType.ClientID %>").disabled = false;
                document.getElementById("<%=txtInstNo.ClientID %>").disabled = false;
                document.getElementById("<%=txtInstDate.ClientID %>").disabled = false;
                document.getElementById("<%=ddlCusBank.ClientID %>").disabled = false;
            }
            else {
                document.getElementById("<%=ddlRcptType.ClientID %>").value = document.getElementById("<%=ddlCusBank.ClientID %>").value = "0";
                document.getElementById("<%=txtInstNo.ClientID %>").value = document.getElementById("<%=txtInstDate.ClientID %>").value = "";
                document.getElementById("<%=ddlRcptType.ClientID %>").disabled = true;
                document.getElementById("<%=txtInstNo.ClientID %>").disabled = true;
                document.getElementById("<%=txtInstDate.ClientID %>").disabled = true;
                document.getElementById("<%=ddlCusBank.ClientID %>").disabled = true;

                ValidatorEnable(document.getElementById("<%=rfvCusBank.ClientID %>"), false);
                document.getElementById("<%=rfvCusBank.ClientID%>").style.visibility = "hidden";
                ValidatorEnable(document.getElementById("<%=rfvRcptType.ClientID %>"), false);
                document.getElementById("<%=rfvRcptType.ClientID%>").style.visibility = "hidden";
                ValidatorEnable(document.getElementById("<%=rfvinstno.ClientID %>"), false);
                document.getElementById("<%=rfvinstno.ClientID%>").style.visibility = "hidden";
                ValidatorEnable(document.getElementById("<%=rfvinstDate.ClientID %>"), false);
                document.getElementById("<%=rfvinstDate.ClientID%>").style.visibility = "hidden";

            }

        }
        
    </script>
    <script type="text/javascript">
        function OpenText() {
            // using for current date by salman
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            var today = dd + '-' + mm + '-' + yyyy;

            var cust = $("#<%=ddlRcptType.ClientID%>").val();
            if (cust != 0) {
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    url: "HireInvoice.aspx/ProductList",
                    data: JSON.stringify({ cust: cust }),
                    success: function (msg) {
                        $.each(msg.d, function (index, item) {
                            CalcAmnt();
                            if (item == 4) {
                                document.getElementById("<%=rfvinstno.ClientID %>").disabled = false;
                                document.getElementById("<%=txtInstNo.ClientID %>").disabled = false;
                                document.getElementById("<%=rfvinstDate.ClientID %>").disabled = false;
                                document.getElementById("<%=txtInstDate.ClientID %>").disabled = false;
                                document.getElementById("<%=ddlRcptType.ClientID %>").disabled = false;
                                document.getElementById("<%=ddlCusBank.ClientID %>").disabled = false;
                                document.getElementById("<%=rfvRcptType.ClientID%>").style.visibility = "visible";
                                document.getElementById("<%=rfvRcptType.ClientID %>").disabled = false;
                                document.getElementById("<%=rfvinstno.ClientID%>").style.visibility = "visible";
                                document.getElementById("<%=rfvinstno.ClientID %>").disabled = false;
                                document.getElementById("<%=rfvCusBank.ClientID%>").style.visibility = "visible";
                                document.getElementById("<%=rfvCusBank.ClientID %>").disabled = false;
                                document.getElementById("<%=rfvinstDate.ClientID%>").style.visibility = "visible";
                                document.getElementById("<%=rfvinstDate.ClientID %>").disabled = false;
                                document.getElementById("<%=txtInstDate.ClientID %>").value = today;
                            }
                            else {
                                document.getElementById("<%=rfvinstno.ClientID %>").disabled = true;
                                document.getElementById("<%=txtInstNo.ClientID %>").disabled = true;
                                document.getElementById("<%=rfvinstDate.ClientID %>").disabled = true;
                                document.getElementById("<%=txtInstDate.ClientID %>").disabled = true;
                                document.getElementById("<%=ddlRcptType.ClientID %>").disabled = false;
                                ValidatorEnable(document.getElementById("<%=rfvCusBank.ClientID %>"), false);
                                document.getElementById("<%=rfvCusBank.ClientID%>").style.visibility = "hidden";
                                ValidatorEnable(document.getElementById("<%=rfvRcptType.ClientID %>"), false);
                                document.getElementById("<%=rfvRcptType.ClientID%>").style.visibility = "hidden";
                                ValidatorEnable(document.getElementById("<%=rfvinstno.ClientID %>"), false);
                                document.getElementById("<%=rfvinstno.ClientID%>").style.visibility = "hidden";
                                ValidatorEnable(document.getElementById("<%=rfvinstDate.ClientID %>"), false);
                                document.getElementById("<%=rfvinstDate.ClientID%>").style.visibility = "hidden";
                                document.getElementById("<%=ddlCusBank.ClientID %>").disabled = true;
                                document.getElementById("<%=ddlCusBank.ClientID %>").value = "0";
                                document.getElementById("<%=txtInstNo.ClientID %>").value = "";
                                document.getElementById("<%=txtInstDate.ClientID %>").value = "";
                            }

                        });
                    },
                    failure: function (msg) { }
                });

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
      <script language="javascript" type="text/javascript">
          function OnChangeddlRateType() {
              if (document.getElementById("<%=ddlRateType.ClientID%>").value == "0") {
              }
              else {
                  __doPostBack('RateTypeValue', '');
              }

          }


    </script>
</asp:Content>
