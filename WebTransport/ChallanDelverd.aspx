<%@ Page Title="Delivery Register" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="ChallanDelverd.aspx.cs" Inherits="WebTransport.ChallanDelverd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
        }
    </script>
    <%--<asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>--%>
            <div class="row ">
                <div class="col-lg-1">
                </div>
                <div class="col-lg-10">
                    <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">DELIVERY REGISTER
                  <span class="view_print"><a href="ManageDeliveryRegister.aspx" tabindex="13" style="color: White;"><asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a>
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
                               <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control"
                                    AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged">
                                </asp:DropDownList>   
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                    ControlToValidate="ddlDateRange" ValidationGroup="Save" ErrorMessage="Please Select Date Dange."
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>                   
                              </div>
                            </div>
                           	<div class="col-sm-8">
                                <b><asp:Label ID="lblRcptDtNo" runat="server" Text="Receipt Date" CssClass="col-sm-3 control-label" style="width: 15%;"><span class="required-field">*</span></asp:Label></b>
                              <div class="col-sm-9" style="width:83%">

                              <div class="col-sm-4" style="width:20%">
                                <asp:TextBox ID="txtDate" runat="server" MaxLength="10"  CssClass="input-sm datepicker form-control" autocomplete="off" data-date-format="dd-mm-yyyy"
                                   AutoPostBack="true" TabIndex="2" onblur="CopyDate()"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtDate"
                                    SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Please Enter Date."
                                    Display="Dynamic" CssClass="classValidation"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-sm-6" id="DivRcptNo" runat="server" style="width: 17%">
                                      <asp:TextBox ID="txtRcptNo" runat="server" MaxLength="10" CssClass="form-control" 
                                        TabIndex="3" autocomplete="off"  ReadOnly="true" Visible="true"></asp:TextBox>
                                    </div>
                                 <div class="col-sm-4" style="width:48%">
                                    <label class="col-sm-3 control-label" style="width: 25%;">To City<span class="required-field">*</span></label>
                                    <div class="col-sm-9" style="width: 68%;">
                                    <asp:DropDownList ID="ddlToCity" runat="server" CssClass="chzn-select" style="width:100%;" TabIndex="4"
                                     AutoPostBack="true"  OnSelectedIndexChanged="ddlToCity_SelectedIndexChanged" >
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvFactory" runat="server" ControlToValidate="ddlToCity"
                                        ValidationGroup="Submit" ErrorMessage="Select To City." CssClass="classValidation"
                                        SetFocusOnError="true" Display="Dynamic"  InitialValue="0"></asp:RequiredFieldValidator>                            
                                    </div>
                            </div>
                                  </div>
                            
                           	
                          </div>
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 30%;">Challan No.<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 70%;">
                               <asp:DropDownList ID="ddlChallanDetl" runat="server" CssClass="form-control"
                                    TabIndex="5" AutoPostBack="true"  OnSelectedIndexChanged="ddlChallanDetl_SelectedIndexChanged">
                                </asp:DropDownList>     
                                      <asp:RequiredFieldValidator ID="rfvItemName" runat="server"  ControlToValidate="ddlChallanDetl"
                                    ValidationGroup="Submit" ErrorMessage="Select Challan No." CssClass="classValidation"
                                    SetFocusOnError="true" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>                      
                              </div>
                            </div>
                           	<div class="col-sm-4">
                                <asp:LinkButton ID="lnkSubmit" 
                                    CssClass="col-sm-4 btn full_width_btn btn-sm btn-primary" runat="server"  TabIndex="6"
                                    style="width: 33%;" ValidationGroup="Submit" onclick="lnkSubmit_Click"> <i class="fa fa-check"></i> Submit</asp:LinkButton>
                              <div class="col-sm-8" style="width: 67%;"></div>
                           	</div>
                           	<div class="col-sm-4"></div>
                          </div>

                        </div>
                      </section>                        
                    </div>
                    
                    <!-- second  section -->
                    <div class="clearfix third_right">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body">     
         
                        <div style="overflow-x:auto">
                           <asp:GridView ID="grdMain" runat="server" CssClass="display nowrap dataTable" AutoGenerateColumns="false" BorderStyle="None"
                                    Width="100%" GridLines="None" AllowPaging="false" OnPageIndexChanging="grdMain_PageIndexChanging"
                                     BorderWidth="0" ShowFooter="true"  OnRowCommand="grdMain_RowCommand"
                                    OnRowDataBound="grdMain_RowDataBound">
                                      <RowStyle CssClass="odd" />
                                    <AlternatingRowStyle CssClass="even" />      
                                            <Columns>
                                                <asp:TemplateField HeaderText="Gr  No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="70px">
                                                    <ItemStyle HorizontalAlign="Left" Width="70px" />
                                                    <ItemTemplate>
                                                        <%#Eval("Gr_no")%>
                                                        <asp:HiddenField ID="hidGridno" runat="server" Value=' <%#Eval("Gr_Idno")%>' />
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" BackColor="ActiveBorder" />
                                                    <FooterTemplate>
                                                        &nbsp;
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="120px">
                                                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                    <ItemTemplate>
                                                        <%#Eval("Item_Name")%>
                                                        <asp:HiddenField ID="hidItemidno" runat="server" Value=' <%#Eval("Item_Idno")%>' />
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" BackColor="ActiveBorder" />
                                                    <FooterTemplate>
                                                        &nbsp;
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="70px">
                                                    <ItemStyle HorizontalAlign="Left" Width="70px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBox" runat="server" Text=' <%#Eval("UOM_Name")%>'></asp:Label>
                                                        <asp:HiddenField ID="hidUnitidno" runat="server" Value=' <%#Eval("Unit_Idno")%>' />
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" BackColor="ActiveBorder" />
                                                    <FooterTemplate>
                                                        &nbsp;
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate Type" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px">
                                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrateType" runat="server" Text='<%#Eval("Rate_Type")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Left" BackColor="ActiveBorder" />
                                                    <FooterTemplate>
                                                        Total:
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-Width="60px">
                                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                    <ItemTemplate>
                                                     <asp:Label ID="lblItemRate" runat="server" Text='<%#Eval("ItemRate")%>'></asp:Label>
                                                    </ItemTemplate>
                                                      <FooterStyle HorizontalAlign="Right" BackColor="ActiveBorder" />
                                                    <FooterTemplate>
                                                        
                                                    </FooterTemplate>
                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delivery Place" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-Width="60px">
                                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                    <ItemTemplate>
                                                     <asp:Label ID="lblDelivery" runat="server" Text='<%#Eval("DelvryPlce_Idno")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="60px">
                                                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQty" runat="server" Text='<%#Eval("Qty")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" BackColor="ActiveBorder" />
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotIssueQty" runat="server" Font-Bold="true"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Weight" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="60px">
                                                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWeight" runat="server" Text='<%# String.Format("{0:0,0.00}", Convert.ToDouble(Eval("weight")))%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" BackColor="ActiveBorder" />
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotWeight" runat="server" Font-Bold="true"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="60px">
                                                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmount" runat="server" Width="40px" Text='<%#Eval("Amount")%>'> </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" BackColor="ActiveBorder" />
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotAmount" runat="server" Font-Bold="true"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="R. Qty" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="70">
                                                    <ItemStyle HorizontalAlign="Right" Width="70" />
                                                    <ItemTemplate>
                                                        <div style="text-align: right;">
                                                            <asp:TextBox ID="txtRecQty" runat="server" Width="70px" Text='<%#Eval("Delv_Qty")%>' AutoPostBack="true"  OnTextChanged="txtRecQty_OnTextChanged"
                                                                CssClass="form-control" MaxLength="9"> </asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" BackColor="ActiveBorder" />
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotRcptQty" runat="server" Font-Bold="true" Visible="true"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="R. Weight" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="70">
                                                    <ItemStyle HorizontalAlign="Right" Width="70" />
                                                    <ItemTemplate>
                                                        <div style="text-align: right;">
                                                            <asp:TextBox ID="txtRecWT" runat="server" Width="70px" Text='<%#Eval("Delv_WT")%>' AutoPostBack="true"  OnTextChanged="txtRecWT_OnTextChanged"

                                                                CssClass="form-control" MaxLength="9"> </asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" BackColor="ActiveBorder" />
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotRcptWT" runat="server" Font-Bold="true" Visible="true"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="R. Amount" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="70">
                                                    <ItemStyle HorizontalAlign="Right" Width="70" />
                                                    <ItemTemplate>
                                                        <div style="text-align: right;">
                                                            <asp:TextBox ID="txtRecAmt" runat="server" Width="70px"  Enabled="false" Text='<%#Eval("Rec_Amount")%>'
                                                                CssClass="form-control" MaxLength="9"> </asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" BackColor="ActiveBorder" />
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblRecAmt" runat="server" Font-Bold="true" Visible="true"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="&nbsp;Remark" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRemark" runat="server" Width="95px" Text='<%#Eval("Remark")%>'
                                                            CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" BackColor="ActiveBorder" />
                                                    <FooterTemplate>
                                                        &nbsp;
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemIdno" runat="server" Text='<%#Eval("Item_Idno")%>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                                        
                                            <PagerStyle CssClass="white_bg" HorizontalAlign="Center" />
                                        </asp:GridView>
                        </div>
                        </div>
                      </section>
                    </div> 

                    <div class="clearfix">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body">   
	                        <div class="clearfix even_row">
	                        	<div class="col-sm-4">
	                            <label class="col-sm-5 control-label">Local GR Amount</label>
	                            <div class="col-sm-7">
	                            
                                   <asp:TextBox ID="txtLocGrAmnt" PlaceHolder="0.00" runat="server" CssClass="form-control" style="text-align:right;"
                                    TabIndex="7"   Enabled="False" onKeyPress="return checkfloat(event, this);" ></asp:TextBox>
	                            </div>
	                          </div>
	                          <div class="col-sm-4">
	                            <label class="col-sm-6 control-label">Outside GR Amount</label>
	                            <div class="col-sm-6">
	                            
                                <asp:TextBox ID="txtoutGrAmnt" PlaceHolder="0.00" runat="server" CssClass="form-control" 
                                    TabIndex="8"  Style="text-align: right;" Enabled="False"
                                    onKeyPress="return checkfloat(event, this);" ></asp:TextBox>
	                            </div>
	                          </div>
	                         	<div class="col-sm-4">
	                            <label class="col-sm-5 control-label">Net Amount</label>
	                            <div class="col-sm-7">
	       

                                   <asp:TextBox ID="txtNetAmnt" PlaceHolder="0.00" runat="server" CssClass="form-control" 
                                            TabIndex="9"  Style="text-align: right;" Enabled="False"
                                            onKeyPress="return checkfloat(event, this);" ></asp:TextBox>
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
                            <div class="col-lg-3"></div>
                            <div class="col-lg-6">  
                             <div class="col-sm-4">
                                <asp:LinkButton runat="server" ID="lnkBtnNew"  CssClass="btn full_width_btn btn-s-md btn-info" TabIndex="12" OnClick="lnkBtnNew_Click"><i class="fa fa-file-o"></i>New</asp:LinkButton>                         
                              </div>                                       
                              <div class="col-sm-4">
                                <asp:LinkButton runat="server" ID="lnkBtnSave" CssClass="btn full_width_btn btn-s-md btn-success" TabIndex="10" ValidationGroup="Save" OnClick="lnkBtnSave_Click"><i class="fa fa-save"></i>Save</asp:LinkButton>                         
                              </div>
                              <div class="col-sm-4">
                               <asp:LinkButton runat="server" ID="lnkBtnCancel" CssClass="btn full_width_btn btn-s-md btn-danger" TabIndex="11" OnClick="lnkBtnCancel_Click"><i class="fa fa-close"></i>Cancel</asp:LinkButton>                         
                              </div>
                            </div>
                            <div class="col-lg-4"></div>
                          </div> 
                        </div>
                      </section>
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
	                                <div class="col-sm-4">
	                                  <label class="col-sm-5 control-label">Date From</label>
                                    <div class="col-sm-7">
                                      <input class="input-sm datepicker form-control" size="16" type="text" value="25-02-2013" data-date-format="dd-mm-yyyy">
                                    </div>
	                                </div>
	                                <div class="col-sm-4">
	                                  <label class="col-sm-5 control-label">Date To</label>
                                    <div class="col-sm-7">
                                      <input class="input-sm datepicker form-control" size="16" type="text" value="26-02-2013" data-date-format="dd-mm-yyyy">
                                    </div>
	                                </div>
	                                <div class="col-sm-4">
	                                  <label class="col-sm-4 control-label">GR No.</label>
	                                  <div class="col-sm-8">
	                                    <select name="account" class="form-control">
	                                      <option>--select--</option>
	                                      <option>02</option>
	                                    </select>                             
	                                  </div>
	                                </div>
	                              </div> 

	                              <div class="clearfix even_row">
	                                <div class="col-sm-4">
	                                  <label class="col-sm-5 control-label">Truck No.</label>
		                                <div class="col-sm-7">
		                                  <select name="account" class="form-control">
		                                    <option>--select--</option>
		                                    <option>Shree Cement</option>
		                                  </select>                             
		                                </div>
	                                </div>
	                                <div class="col-sm-4" style="padding: 0;">
	                                  <div class="col-sm-6 prev_fetch">
	                                    <a class="btn full_width_btn btn-sm btn-primary"><i class="fa fa-search"></i>Search</a>
	                                  </div>
	                                  <div class="col-sm-6"> 
	                                     <label class="control-label">T. Record(s) : </label>
	                                  </div>
	                                </div>
	                              </div> 
	                              
										          </div>
										        </section>
										      </div>
										      <div class="modal-footer">
										        <div class="popup_footer_btn"> 
										          <button type="submit" class="btn btn-dark" ><i class="fa fa-check"></i>Ok</button>
										          <button type="submit" class="btn btn-dark" data-dismiss="modal"><i class="fa fa-times"></i>Close</button>
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
            <asp:HiddenField ID="hidMtrlRcptid" runat="server" />
            <asp:HiddenField ID="hidMtrlTrnsfDt" runat="server" />
            <asp:HiddenField ID="hidmindate" runat="server" />
            <asp:HiddenField ID="hidmaxdate" runat="server" />
        <<%--/ContentTemplate>
    </asp:UpdatePanel>--%>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
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
