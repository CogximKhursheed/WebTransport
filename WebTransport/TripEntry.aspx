<%@ Page Title="Trip Sheet Entry" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeBehind="TripEntry.aspx.cs" Inherits="WebTransport.TripEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            //WinPrint.close();
            return false;
        }

    </script>
    <script language="javascript" type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function () {
            setDatecontrol();
        });

        prm.add_endRequest(function () {
            setDatecontrol();
        });

        function openPartyModal() {
            $('#gr_delivery_details_form').modal('show');
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

        function Check(textBox, maxLength) {
            if (textBox.value.length > maxLength) {
                textBox.value = textBox.value.substr(0, maxLength);
            }
        }
    </script>
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-10">
            <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">TRIP SHEET ENTRY
                  <span class="view_print"><a id="lnkView" runat="server" href="ManageTripEntry.aspx" tabindex="26">LIST</a>
                  <asp:LinkButton ID="lnkPrint"  runat="server" onclick="lnkPrint_Click" TabIndex="27"><i class="fa fa-print icon"></i></asp:LinkButton></span>
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
                                TabIndex="1"  OnSelectedIndexChanged="ddldateRange_SelectedIndexChanged">
                            </asp:DropDownList>    
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddldateRange"
                                CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select Year!" InitialValue="0"
                                SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>                  
                              </div>
                            </div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 29%;">Date<span class="required-field">*</span></label>
                           		<div class="col-sm-4" style="width: 31%;">
                                 <asp:TextBox ID="txtDate" runat="server" CssClass="input-sm datepicker form-control" data-date-format="dd-mm-yyyy" MaxLength="50"
                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                    TabIndex="2" Text=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select Date!" SetFocusOnError="true"
                                    ValidationGroup="save"></asp:RequiredFieldValidator>                              
                              </div>
                              <div class="col-sm-3" style="width: 40%;">
                              <asp:TextBox ID="txtTripNo" runat="server" CssClass="form-control" MaxLength="7"  oncopy="return false" oncut="return false" 
                                      onDrop="blur();return false;" onpaste="return false" TabIndex="3" Text="" ReadOnly="True"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTripNo"
                                CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Enter Trip No!"
                                SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>                              
                           	</div>
                           	<div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 29%;">Loc.[From]<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 71%;">
                             <asp:DropDownList ID="ddlFromCity" runat="server" CssClass="form-control" TabIndex="4" AutoPostBack="True" OnSelectedIndexChanged="ddlFromCity_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlFromCity"
                                CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select From City!" InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>                           
                              </div>                              
                            </div>
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 29%;">Truck No.<span class="required-field">*</span></label>
                              <div class="col-sm-7">
                               <asp:DropDownList ID="ddlTruckNo" runat="server"  AutoPostBack="true" CssClass="form-control" TabIndex="5"  OnSelectedIndexChanged="ddlTruckNo_SelectedIndexChanged" >
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTruckNo"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select Truck No!" InitialValue="0"  SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>                            
                              </div>
                              <asp:LinkButton runat="server" ID="lnkBtnSearch"   TabIndex="10" CssClass="btn btn-sm btn-primary acc_home"   OnClick="lnkBtnSearch_Click" 
                                      ToolTip="Select Challan / Fuel / Toll Details"><i class="fa fa-file"></i></asp:LinkButton>
                              
                            </div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 29%;">KMS<span class="required-field">*</span></label>
                              <div class="col-sm-3" style="width: 23.66%;">
                                  <asp:TextBox ID="txtStratKms" runat="server" CssClass="form-control" MaxLength="50"
                                                        oncopy="return false" oncut="return false" Placeholder="Start" onDrop="blur();return false;" onpaste="return false"
                                                        style="text-align:right;" onchange="javascript:Calckm();"
                                                        TabIndex="6" Text="" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txtStratKms"
                                                        CssClass="classValidation" Display="Dynamic" ErrorMessage="Start KMS!"
                                                        SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>              
                              </div>
                               <div class="col-sm-3" style="width: 23.66%;">
                                 <asp:TextBox ID="txtEndKms" runat="server" Placeholder="End" CssClass="form-control" MaxLength="50"
                                                        oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                        style="text-align:right;" onchange="javascript:Calckm();"
                                                        TabIndex="7" Text=""></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEndKms"
                                                        CssClass="classValidation" Display="Dynamic" ErrorMessage="End KMS!"
                                                        SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>         
                              </div>
                               <div class="col-sm-3" style="width: 23.66%;">
                                  <asp:TextBox ID="txtKMS" runat="server" CssClass="form-control"  MaxLength="50"
                                                        oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                        TabIndex="8" Text="0.00" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvKMS" runat="server" ControlToValidate="txtKMS"
                                                        CssClass="classValidation" Display="Dynamic" ErrorMessage="Enter KMS!"
                                                        SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>               
                              </div>
                           	</div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 29%;">Driver<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 61%;">
                                <asp:DropDownList ID="ddldriverName" runat="server" CssClass="form-control" TabIndex="9" >
                                </asp:DropDownList>  
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddldriverName"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select Driver!" InitialValue="0"
                                    SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>
                              
                           	</div>
                          </div>
                        </div>
                      </section>                        
                    </div>

                    <div class="clearfix">
						          <!-- first main Column-->
						        	<div class="col-lg-6">
						            <!-- first left row -->
						            <div class="clearfix first_left">
						              <section class="panel panel-in-default">
						                <header class="panel-heading">
						                  <span class="h5">CHALLAN DETAILS</span>
						                </header>
						                <div class="panel-body trip_sheet_entry">						             
                                            <div style="overflow-y:auto;height:150px;">
                                                <asp:GridView ID="grdChlnDetl" runat="server" AutoGenerateColumns="false" Width="100%"
                                                    BorderStyle="Solid" CssClass="display nowrap dataTable" GridLines="None"
                                                    BorderWidth="1"  RowStyle-CssClass="gridAlternateRow" AlternatingRowStyle-CssClass="gridRow"
                                                    ShowFooter="true" onrowdatabound="grdChlnDetl_RowDataBound">
                                                    <RowStyle  CssClass="odd" />
                                                    <AlternatingRowStyle CssClass="even" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr" HeaderStyle-Width="20" HeaderStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                            <ItemStyle Width="20" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>.
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Chln. No" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                            <ItemStyle Width="50" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblChlnNo" runat="server" Text='<%#Convert.ToString(Eval("ChlnNo")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                            <ItemStyle Width="50" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblChlndate" runat="server" Text='<%#Convert.ToDateTime(Eval("Date")).ToString("dd-MM-yyyy") %>'></asp:Label>
                                                                   <asp:HiddenField ID="HidChlnIdno"  Value='<%#Eval("ChlnIdno") %>' runat="server" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            Total
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Total" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                             <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                             <FooterStyle HorizontalAlign="Center" />
                                                            <ItemStyle Width="50" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotal" runat="server" Text='<%#Convert.ToDouble(Eval("Total")).ToString("N2") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblChlnTotal"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Gross" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                             <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                            <ItemStyle Width="50" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGross" runat="server" Text='<%#Convert.ToDouble(Eval("Gross")).ToString("N2") %>'></asp:Label>
                                                            </ItemTemplate>
                                                             <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblFooterGross"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Advance" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                            <ItemStyle Width="50" HorizontalAlign="Left"/>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAdvance" runat="server" Text='<%#Convert.ToDouble(Eval("Advance")).ToString("N2") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblFooterAdvance"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Diesel" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                            <ItemStyle Width="50" HorizontalAlign="Left"/>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesial" runat="server" Text='<%#Convert.ToDouble(Eval("Desial")).ToString("N2") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblFooterDesial"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="lblnorecord" runat="server" Text="No record inserted"></asp:Label>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                           </div>
						                </div>
						              </section>
						            </div> 
						          </div>
						          <!-- second main Column -->
						            <div class="col-lg-6">
						            <!-- first_one left row -->
						            <div class="clearfix first_one_left">
						              <section class="panel panel-in-default">
						                <header class="panel-heading">
						                  <span class="h5">FUEL PURCHASE DETAILS</span>
						                </header>
						                <div class="panel-body trip_sheet_entry">
						               <div style="overflow-y:auto;height:150px;">
                                    <asp:GridView ID="GrdPumpDetl" runat="server" AutoGenerateColumns="false" Width="100%"
                                        BorderStyle="Solid" CssClass="display nowrap dataTable" GridLines="None"
                                        BorderWidth="1"  ShowFooter="true" onrowdatabound="GrdPumpDetl_RowDataBound">
                                            <RowStyle  CssClass="odd" />
                                        <AlternatingRowStyle CssClass="even" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr" HeaderStyle-Width="20" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="20" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>.
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pur. No" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    &nbsp;<asp:Label ID="lblPurNo" runat="server" Text='<%#Convert.ToString(Eval("PbillNo")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pur. Date" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    &nbsp;<asp:Label ID="lblPurDate" runat="server" Text='<%#Convert.ToDateTime(Eval("PbillDate")).ToString("dd-MM-yyyy") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pump Name" HeaderStyle-Width="200" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="200" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    &nbsp;<asp:Label ID="lblGrNo" runat="server" Text='<%#Convert.ToString(Eval("PumpName")) %>'></asp:Label>
                                                </ItemTemplate>
                                                    <FooterTemplate>
                                                Total
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="80" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtFuelAmnt" Enabled="false" Width="80px" Text='<%#Convert.ToDouble(Eval("Amount")).ToString("N2") %>' runat="server"></asp:TextBox>
                                                    <asp:HiddenField ID="HidPumpIdno"  Value='<%#Eval("PumpIdno") %>' runat="server" />
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                        <FooterTemplate>
                                                <asp:Label runat="server" ID="lblFuelTotal"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblnorecord" runat="server" Text="No record inserted"></asp:Label>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    </div> 	
						                </div>
						              </section>
						            </div>
						          </div>
				        		</div>
				   	            <div class="clearfix">
						          <!-- first main Column-->
						        	<div class="col-lg-6">
						            <!-- first left row -->
						            <div class="clearfix first_left">
						              <section class="panel panel-in-default">
						                <header class="panel-heading">
						                  <span class="h5">EXPENSE DETAILS</span>
						                </header>
						                <div class="panel-body trip_sheet_entry">
						                  <table id="trip_sheet_expense_details" class="display nowrap" cellspacing="0" width="100%">
						                  	<div class="dataTables_add_entry">
		                              <div class="clearfix even_row">
		                                <div class="col-sm-5">
		                                  <label class="control-label"> Expense Ledger<span class="required-field">*</span></label>
		                                  <asp:DropDownList ID="ddlExpenese" runat="server" CssClass="form-control" TabIndex="11">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvdrpItemName" runat="server" ControlToValidate="ddlExpenese"
                                                    Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="sub"
                                                    class="classValidation" ErrorMessage="Select Ledger!"></asp:RequiredFieldValidator>
		                                </div>
		                                <div class="col-sm-4">
		                                  <label class="control-label">Amount<span class="required-field">*</span></label>
		                                    <asp:TextBox ID="txtExpAmnt" Text="0.00" runat="server" class="form-control"  style="text-align: right;" TabIndex="13" 
                                    MaxLength="12" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvQty" runat="server" ControlToValidate="txtExpAmnt"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="sub" class="classValidation"
                                    ErrorMessage="Enter Amount!"></asp:RequiredFieldValidator>
		                                </div>
		                                <div class="col-sm-3" > 
                                        <asp:LinkButton runat="server" ID="lnkBtnSubmit"  CssClass="btn full_width_btn btn-sm btn-primary subnew"  TabIndex="14"  ValidationGroup="sub" OnClick="lnkBtnSubmit_Click">Submit</asp:LinkButton>			                            
			                              </div>
		                              </div>                             
		                            </div>
                                    	</table>
												<div style="width:100%;overflow-y:auto;height:100px;">
                                                <asp:GridView ID="GrdExpense" runat="server" AutoGenerateColumns="false" Width="100%"
                                                    BorderStyle="Solid" CssClass="display nowrap dataTable" GridLines="None"
                                                    BorderWidth="1"   ShowFooter="true" onrowcommand="GrdExpense_RowCommand" onrowdatabound="GrdExpense_RowDataBound">
                                                      <RowStyle  CssClass="odd" />
                                                    <AlternatingRowStyle CssClass="even" />
                                                    <Columns>
                                                       <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                                                        <ItemStyle Width="50" HorizontalAlign="Center" />
                                                                                        <ItemTemplate>
                                                                                         <asp:LinkButton ID="lnkBtnDelete" CssClass="delete" runat="server" OnClientClick="return confirm('Do you want to delete this record ?');" CommandArgument='<%#Eval("id") %>'  CommandName="cmddelete" ToolTip="Delete" ><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                                                           <%-- <asp:ImageButton ID="imgbtndelete" runat="server" ImageUrl="~/Images/delete_sm.png"
                                                                                                CommandArgument='<%#Eval("id") %>' CommandName="cmddelete" OnClientClick="return confirm('Do you want to delete this record ?');" />--%>
                                                                                        <%--    <asp:ImageButton ID="imgbtnedit" runat="server" ImageUrl="~/Images/edit_sm.png" OnClientClick="return confirm('Do you want to edit this record ?');"
                                                                                                CommandArgument='<%#Eval("id") %>' CommandName="cmdedit" />--%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="50" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                       <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Ledger">
                                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                                        <ItemTemplate>
                                                            <%#Eval("Acnt_Name")%>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                            Total
                                                            </FooterTemplate>
                                                    </asp:TemplateField>
                                                         <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Amount">
                                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                         <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Amnt")))%>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblExpnTotal"></asp:Label>
                                                            </FooterTemplate>
                                                    </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="lblnorecord" runat="server" Text="No record Inserted"></asp:Label>
                                                    </EmptyDataTemplate>
                                                </asp:GridView></div>
														
						                </div>
						              </section>
						            </div> 
						          </div>
						          <!-- second main Column -->
						            <div class="col-lg-6">
						            <!-- first_one left row -->
						            <div class="clearfix first_one_left">
						              <section class="panel panel-in-default">
						                <header class="panel-heading">
						                  <span class="h5">TOLL DETAILS</span>
						                </header>
						                <div class="panel-body trip_sheet_entry">						                  	
                                        <div style="overflow-y:auto;height:150px;">                                                   
                                          <asp:GridView ID="GrdTollDetl" runat="server" AutoGenerateColumns="false" Width="100%"
                                                    BorderStyle="Solid" CssClass="display nowrap dataTable" GridLines="None"
                                                    BorderWidth="1" ShowFooter="true" onrowdatabound="GrdTollDetl_RowDataBound" >
                                                      <RowStyle  CssClass="odd" />
                                                    <AlternatingRowStyle CssClass="even" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr" HeaderStyle-Width="20" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="20" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                  <%#Container.DataItemIndex+1 %>.
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Toll Name" HeaderStyle-Width="200" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="200" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label ID="lblGrNo" runat="server" Text='<%#Convert.ToString(Eval("TollName")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            Total
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="92" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="92" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGrdate" runat="server" Width="80px" Text='<%#Convert.ToDouble(Eval("Amount")).ToString("N2") %>'></asp:Label>
                                                                    <asp:HiddenField ID="HidTollIdno"  Value='<%#Eval("TollIdno") %>' runat="server" />
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Center" />
                                                               <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTollTotal"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                             
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="lblnorecord" runat="server" Text="No record inserted"></asp:Label>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                                
                                        </div>                           
						                </div>
						              </section>
						            </div>
						          </div>
				        		</div>
                                <div class="clearfix">
                                <section class="panel panel-in-default">                            
                        <div class="panel-body">
                                <div class="clearfix odd_row">
                            <div class="col-sm-3">
                            <div class="col-sm-5 control-label">
                            <asp:Label ID="lblAdvAmnt" runat="server" Text="Pay to Driver" ></asp:Label>
                            </div>
                           
                              <div class="col-sm-7">
                              <asp:TextBox ID="txtAdvAmnt" runat="server" AutoPostBack="false" CssClass="form-control" TabIndex="11"
                                MaxLength="9" oncopy="return false" oncut="return false" onDrop="blur();return false;" 
                                onkeydown="return (event.keyCode!=45);" onKeyPress="return checkfloat(event, this);" style="text-align:right;"
                                onpaste="return false"  onchange="javascript:CalcAmnt();"   Text="0.00"></asp:TextBox>
                              </div>
                            </div>
                            <div class="col-sm-3">
                              <label class="col-sm-5 control-label">Pay. Type</label>
                              <div class="col-sm-7">
                                <asp:DropDownList ID="ddlRcptType" Enabled="false" runat="server" CssClass="form-control" onchange="javascript:OpenText();"  AutoPostBack="false" TabIndex="12" >
                               </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvRcptType" runat="server" ControlToValidate="ddlRcptType"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="save"  ErrorMessage="Select Receipt Type" CssClass="classValidation"></asp:RequiredFieldValidator>
                           
                              </div>
                            </div>
                           	<div class="col-sm-3">
                           		<label class="col-sm-4 control-label">Inst.Detail</label>
                           		<div class="col-sm-3">
                                   <asp:TextBox ID="txtInstNo" runat="server" CssClass="form-control" MaxLength="6"  Style="text-align: right;" TabIndex="13"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvinstno" runat="server" ControlToValidate="txtInstNo"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="" ValidationGroup="save"
                                    ErrorMessage="Enter Inst. No" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                              <div class="col-sm-5">
                                <asp:TextBox ID="txtInstDate" runat="server" CssClass="input-sm datepicker form-control" TabIndex="14" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvinstDate" runat="server" ControlToValidate="txtInstDate"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="" ValidationGroup="save"
                                    ErrorMessage="Enter Inst. Date!" CssClass="classValidation"></asp:RequiredFieldValidator>
                             
                              </div>
                           	</div>
                           	<div class="col-sm-3">
                              <label class="col-sm-5 control-label">Cust. Bank<span class="required-field">*</span></label>
                              <div class="col-sm-7">
                               <asp:DropDownList ID="ddlCusBank" runat="server" CssClass="form-control" TabIndex="15">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCusBank" runat="server" ControlToValidate="ddlCusBank"
                                Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="save"
                                ErrorMessage="Select Cust. Bank!" CssClass="classValidation"></asp:RequiredFieldValidator>                                      
                              </div>
                            </div>
                          </div>
                                </div>
                                </section>
                                </div>

                    <div class="clearfix">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body">
                          <div class="clearfix odd_row">
                            <div class="col-lg-6">
                            	<div class="clearfix even_row">
                                <label class="col-sm-2 control-label">Remarks</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtRemarks" placeholder="Remarks" runat="server" CssClass="form-control parsley-validated" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                      onpaste="return false" Style="resize: none;" TabIndex="19" Text="" TextMode="MultiLine" onKeyUp="javascript:Check(this, 100);" onChange="javascript:Check(this, 100);"></asp:TextBox>
                                
                                </div>
                              </div>
                            </div>
                            <div class="col-lg-6">
                            	<div class="clearfix even_row">
	                              <div class="col-sm-6">
	                                <label class="col-sm-6 control-label">Gross Amount</label>
	                                <div class="col-sm-6">
                                     <asp:TextBox ID="txtGrosstotal" runat="server" CssClass="form-control"  Enabled="false"
                                                                MaxLength="50" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                                onpaste="return false" Style="text-align:right;" TabIndex="20" Text="0.00"></asp:TextBox>
	                                
	                                </div>
	                              </div>
	                             	<div class="col-sm-6">
	                                <label class="col-sm-5 control-label">Insentive</label>
	                                <div class="col-sm-7">
	                                   <asp:TextBox ID="txtInsentive" runat="server" AutoPostBack="true" CssClass="form-control"
                                                                MaxLength="9" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                                onpaste="return false"  Style="text-align:right;"
                                                                TabIndex="21" Text="0.00" ontextchanged="txtInsentive_TextChanged"></asp:TextBox>
	                                </div>
	                              </div>			                              
	                            </div>
	                            <div class="clearfix even_row">
	                              <div class="col-sm-6"></div>
	                             	<div class="col-sm-6">
	                                <label class="col-sm-5 control-label">Net Amount</label>
	                                <div class="col-sm-7">
	                                 <asp:TextBox ID="txtNetAmnt" runat="server" CssClass="form-control" Enabled="false" 
                                                                MaxLength="50" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                                onpaste="return false" Style="text-align:right;" TabIndex="22" Text="0.00"></asp:TextBox>
	                                </div>
	                              </div>
	                            </div>
                            </div>


                          </div>
                        </div>
                      </section>
                    </div>

                     <!-- fourth row -->
                    <div class="clearfix odd_row">
                      <div class="col-lg-2">
                      </div>
                      <div class="col-lg-8">                                        
                       
                        <div class="col-sm-4">
                        <asp:LinkButton runat="server" ID="lnkBtnNew" 
                               CssClass="btn full_width_btn btn-s-md btn-info" onclick="lnkBtnNew_Click" TabIndex="25"><i class="fa fa-file-o"></i>New</asp:LinkButton>
                                                  
                       </div>
                         <div class="col-sm-4">
                        <asp:LinkButton runat="server" ID="lnkBtnSave" 
                                CssClass="btn full_width_btn btn-s-md btn-success" TabIndex="23"  ValidationGroup="save" onclick="lnkBtnSave_Click"><i class="fa fa-save"></i>Save</asp:LinkButton>
                                               
                       </div>
                        <div class="col-sm-4">
                        <asp:LinkButton runat="server" ID="lnkBtnCancel"  
                                CssClass="btn full_width_btn btn-s-md btn-danger" TabIndex="24" onclick="lnkBtnCancel_Click"><i class="fa fa-close"></i>Cancel</asp:LinkButton>
                    
                        </div>
                      </div>
                      <div class="col-lg-2"></div>
                    </div>

                    <!-- popup form GR detail -->
					<div id="gr_delivery_details_form" class="modal fade">
										  <div class="modal-dialog">
										    <div class="modal-content">
										      <div class="modal-header">
										        <h4 class="popform_header">Challan / Fuel / Toll Details </h4>
										      </div>
										      <div class="modal-body">
										        <section class="panel panel-default full_form_container material_search_pop_form">
										          <div class="panel-body">
										             <!-- First Row start -->
										            <div class="clearfix odd_row">	                                
	                                <div class="col-sm-6">
	                                  <label class="col-sm-4 control-label">Date From</label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txtDateFrom" runat="server"  data-date-format="dd-mm-yyyy" CssClass="input-sm datepicker form-control" TabIndex="85"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtFrm" runat="server" ErrorMessage="Enter From Date!"
                                            Display="Dynamic" CssClass="classValidation" ControlToValidate="txtDateFrom" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                      
                                    </div>
	                                </div>
	                                <div class="col-sm-6">
	                                  <label class="col-sm-3 control-label">Date To</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtDateTo" runat="server" CssClass="input-sm datepicker form-control" data-date-format="dd-mm-yyyy" TabIndex="86"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtTo" runat="server" ErrorMessage="Enter To Date!"
                                            Display="Dynamic" CssClass="classValidation" ControlToValidate="txtDateTo" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                    </div>
	                                </div>
	                              </div> 

	                              <div class="clearfix even_row">
	                                <div class="col-sm-6">
	                                	<label class="col-sm-4 control-label">SearchCriteria</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlSearchtyp" runat="server" CssClass="form-control" AutoPostBack="false" TabIndex="87">
                                            <asp:ListItem Value="0">Challan </asp:ListItem>
                                            <asp:ListItem Value="1">Fuel Purchase</asp:ListItem>
                                            <asp:ListItem Value="2">Toll</asp:ListItem>
                                             <asp:ListItem Value="3">GR</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
	                                </div>
	                                <div class="col-sm-6">
	                                	<label class="col-sm-3 control-label">Doc. No.</label>
                                    <div class="col-sm-9">
                                      <asp:TextBox ID="txtDocNo" runat="server" CssClass="form-control"  TabIndex="88"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryPrty" runat="server" ErrorMessage="Please Enter Doc No!"
                                            Display="Dynamic" InitialValue="0" CssClass="classValidation" ControlToValidate="txtDocNo"
                                            ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                    </div>
	                                </div>
	                              </div> 
	                              <div class="clearfix odd_row">
	                                <div class="col-sm-6"></div>
	                                <div class="col-sm-6" style="padding: 0;">
	                                  <div class="col-sm-4 prev_fetch">
                                      <asp:LinkButton runat="server" ID="lnkBtnDetlSearch"   CssClass="btn full_width_btn btn-sm btn-primary" TabIndex="89"
                                              ValidationGroup="RcptEntrySrch" onclick="lnkBtnDetlSearch_Click"><i class="fa fa-search"></i>Search</asp:LinkButton> 
	                                  </div>
	                                  <div class="col-sm-8">  
	                                  </div>
	                                </div>
	                              </div> 
	                              <div class="clearfix">
                                  <div style="overflow-y:auto;height:200px;">
	                                   <asp:GridView ID="grdDocdetals" runat="server" GridLines="None" AutoGenerateColumns="false"
                                            Width="100%" BorderStyle="None" CssClass="display nowrap dataTable"
                                            BorderWidth="0" TabIndex="22">
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
                                                <asp:TemplateField HeaderText="Doc No." HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("DocNo"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Doc. Date" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%# (Convert.ToString(Eval("DocDate"))=="--")?"--":Convert.ToDateTime(Eval("DocDate")).ToString("dd-MMM-yyyy")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Particulars" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOdrNo" runat="server" Text=' <%#Eval("DocType")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="180px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToDouble(Eval("DocAmnt")).ToString("N2")%>
                                                        <asp:HiddenField ID="hidDocIdno" runat="server" Value='<%#Eval("DocIdno")%>' />
                                                        <asp:HiddenField ID="hidDrvidno" runat="server" Value='<%#Eval("Driver_Idno")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <EmptyDataTemplate>
                                                Records(s) not found.
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                        </div>
	                              </div>
	                              
									</div>
								</section>
								</div>
								<div class="modal-footer">
								<div class="popup_footer_btn"> 
                                <asp:LinkButton runat="server" CssClass="btn btn-dark" ID="lnkDocOk"    OnClick="lnkDocOk_Click"><i class="fa fa-check"></i>Ok</asp:LinkButton> 
                                    <asp:LinkButton runat="server" CssClass="btn btn-dark" ID="lnkBtnClose"  OnClick="lnkBtnClose_Click"><i class="fa fa-times"></i>Clear</asp:LinkButton>
									<button type="submit" data-dismiss="modal" class="btn btn-dark" ><i class="fa fa-times"></i>Close</button>
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
    <asp:HiddenField ID="hidid" runat="server" Value="" />
    <asp:HiddenField ID="Hidrowid" runat="server" Value="" />
    <asp:HiddenField ID="hidmindate" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />
    <asp:HiddenField ID="hidepostingMsg" runat="server" />
        <asp:HiddenField ID="HidAdvAmnt" runat="server" />
        <asp:HiddenField ID="HidexpTotal" runat="server" />
        <asp:HiddenField ID="HidActualamnt" runat="server" />
          <asp:HiddenField ID="HidPaytype" runat="server" />
    <table width="100%">
        <tr style="display: none">
        
            <td class="white_bg" align="center">
                <div id="print" style="font-size: 13px;">
                    <table cellpadding="1" cellspacing="0" width="800" border="1">
                     <style>
                        #Table1 {
                            display: table;
                            border-collapse: collapse;
                        }
                        #Table1 td {
                            width: 12% !important;
                            border:1px solid;
                        }
                        #Table1 td:first-child {
                            width: 5% !important;
                            border:1px solid;
                        }
                    </style>
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
                            <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                                border-left-style: none; border-right-style: none">
                                <h3>
                                    <strong>&nbsp;&nbsp;TripSheet of&nbsp; <strong>[&nbsp;<asp:Label ID="lblTruckNo"
                                        runat="server"></asp:Label>&nbsp;]&nbsp; (</strong> From
                                        <asp:Label ID="lblChlnMinDate" runat="server"></asp:Label>
                                        To
                                        <asp:Label ID="lblChlnMaxDate" runat="server"></asp:Label>
                                        of &nbsp;<strong><asp:Label ID="lblLocation" runat="server"></asp:Label>&nbsp; )</strong>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table border="0" width="100%">
                                    <tr>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;">
                                            <asp:Label ID="lbltxtTripNo" Text="Trip No" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td align="left" class="white_bg" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblTripNo" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none"
                                            colspan="2">
                                            <asp:Label ID="lbltxtTripDate" Text="Trip Date" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td align="left" class="white_bg" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblTriDate" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; width: 250px;
                                            border-right-style: none">
                                            <asp:Label ID="lbltxtDriver" Text="Driver :" runat="server"></asp:Label>&nbsp;<b>
                                                <asp:Label ID="lblDriverName" runat="server"></asp:Label></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="Label2" Text="Start Kms" runat="server"></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td align="left" class="white_bg" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblStartKMS" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" style="font-size: 13px; border-right-style: none"
                                            colspan="2">
                                            <asp:Label ID="Label5" runat="server" Text="End KMs"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td align="left" class="white_bg" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblEndKms" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; width: 200px;
                                            border-right-style: none">
                                            <asp:Label ID="Label4" Text="KMs&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;<b><asp:Label
                                                ID="lblFinalKMs" runat="server"></asp:Label></b>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <div id="DivChlnDetl" visible="false" runat="server">
                            <tr>
                                <td colspan="4">
                                    <b>Challan Details</b>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table1">
                                        <asp:Repeater ID="RptChlnDetl" runat="server" OnItemDataBound="RptChlnDetl_ItemDataBound">
                                            <HeaderTemplate>
                                                <tr>
                                                    <td style="font-size: 12px;" align="center" width="10%">
                                                        <strong>Sr.</strong>
                                                    </td>
                                                    <td style="font-size: 12px;" width="10%">
                                                        <strong>Challan No</strong>
                                                    </td>
                                                    <td style="font-size: 12px;" width="10%">
                                                        <strong>Challan Date</strong>
                                                    </td>
                                                    <td style="font-size: 12px;" align="center" width="15%">
                                                        <strong>From City</strong>
                                                    </td>
                                                    <td style="font-size: 12px;" align="center" width="15%">
                                                        <strong>To City</strong>
                                                    </td>
                                                    <td style="font-size: 12px;" align="right" width="20%">
                                                        <strong>Gross Amnt</strong>&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td style="font-size: 12px;" align="right" width="20%">
                                                        <strong>Diesel</strong>&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td style="font-size: 12px;" align="right" width="20%">
                                                        <strong>Advance</strong>&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td style="font-size: 12px;" align="right" width="20%">
                                                        <strong>Net Amnt</strong>&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="white_bg" align="center" width="10%">
                                                        <%#Container.ItemIndex+1 %>.
                                                    </td>
                                                    <td class="white_bg" align="left" width="10%">
                                                        &nbsp;&nbsp;
                                                        <%#Eval("Chln_No")%>
                                                    </td>
                                                    <td class="white_bg" width="10%">
                                                        <%#Convert.ToDateTime(Eval("Chln_Date")).ToString("dd-MM-yyyy")%>
                                                    </td>
                                                    <td class="white_bg" align="center" width="15%">
                                                        <%#Eval("FromCity")%>
                                                    </td>
                                                    <td class="white_bg" align="center" width="15%">
                                                        <%#Eval("ToCity")%>
                                                    </td>
                                                    <td class="white_bg" align="right" width="20%">
                                                        <%#Convert.ToDouble(Eval("Gross_Amnt")).ToString("N2")%>&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td class="white_bg" align="right" width="20%">
                                                        <%#Convert.ToDouble(Eval("Diesel_Amnt")).ToString("N2")%>&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td class="white_bg" align="right" width="20%">
                                                        <%#Convert.ToDouble(Eval("Adv_Amnt")).ToString("N2")%>&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td class="white_bg" align="right" width="20%">
                                                        <%#Convert.ToDouble(Eval("Net_Amnt")).ToString("N2")%>&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <ItemTemplate>
                                                <tr>
                                                    <td class="white_bg" align="center" width="10%">
                                                        &nbsp;
                                                    </td>
                                                    <td class="white_bg" align="left" width="10%">
                                                        &nbsp;
                                                    </td>
                                                    <td class="white_bg" width="10%">
                                                        &nbsp;
                                                    </td>
                                                    <td class="white_bg" align="center" width="15%">
                                                        &nbsp;
                                                    </td>
                                                    <td class="white_bg" align="center" width="15%">
                                                        <b>Total</b> &nbsp;
                                                    </td>
                                                    <td class="white_bg" align="right" width="15%">
                                                        <asp:Label ID="lblChlnGross" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                     <td class="white_bg" align="right" width="20%">
                                                        <asp:Label ID="lblChlnDesial" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td class="white_bg" align="right" width="20%">
                                                        <asp:Label ID="lblAdvTotal" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td class="white_bg" align="right" width="20%">
                                                        <asp:Label ID="lblChlnTotal" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </div>
                        <%--Fuel Deatil --%>
                        <div id="DivFuelDetl" visible="false" runat="server">
                            <tr>
                                <td colspan="4">
                                    <b>Fuel Details</b>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table3">
                                        <asp:Repeater ID="RptFuelDetl" runat="server" OnItemDataBound="RptFuelDetl_ItemDataBound">
                                            <HeaderTemplate>
                                                <tr>
                                                    <td style="font-size: 12px;" align="center" width="10%">
                                                        <strong>Sr.</strong>
                                                    </td>
                                                    <td style="font-size: 12px;" width="10%">
                                                        <strong>Pur. No</strong>
                                                    </td>
                                                    <td style="font-size: 12px;" width="10%">
                                                        <strong>Pur. Date</strong>
                                                    </td>
                                                    <td style="font-size: 12px;" align="center" width="25%">
                                                        <strong>Pump Name</strong>
                                                    </td>
                                                    <td style="font-size: 12px;" width="20%">
                                                        <strong></strong>
                                                    </td>
                                                    <td style="font-size: 12px;" align="right" width="25%">
                                                        <strong>Total Amount </strong>&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="white_bg" align="center" width="10%">
                                                        <%#Container.ItemIndex+1 %>.
                                                    </td>
                                                    <td class="white_bg" width="10%">
                                                        &nbsp;&nbsp;
                                                        <%#Eval("PBillHead_No")%>
                                                    </td>
                                                    <td class="white_bg" width="10%">
                                                        <%#Convert.ToDateTime(Eval("PBillHead_Date")).ToString("dd-MM-yyyy")%>
                                                    </td>
                                                    <td class="white_bg" align="center" width="25%">
                                                        <%#Eval("Acnt_Name")%>
                                                    </td>
                                                    <td class="white_bg" width="20%">
                                                    </td>
                                                    <td class="white_bg" align="right" width="25%">
                                                        <%#Convert.ToDouble(Eval("Net_Amnt")).ToString("N2")%>&nbsp;&nbsp;&nbsp;
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
                                    <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table4">
                                        <tr>
                                            <td class="white_bg" width="5%">
                                            </td>
                                            <td class="white_bg" width="10%">
                                            </td>
                                            <td class="white_bg" width="20%" align="center">
                                            </td>
                                            <td class="white_bg" width="20%">
                                            </td>
                                            <td class="white_bg" width="12%" align="center">
                                                <asp:Label ID="Label6" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                            </td>
                                            <td class="white_bg" width="10%" align="left">
                                            </td>
                                            <td class="white_bg" width="15%" align="right">
                                                <asp:Label ID="lblFuelTotal" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </div>
                        <div id="DivExpenseDetl" visible="false" runat="server">
                            <tr>
                                <td colspan="4">
                                    <b>Expense Details</b>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table5">
                                        <asp:Repeater ID="RptExpenseDetl" runat="server" OnItemDataBound="RptExpenseDetl_ItemDataBound">
                                            <HeaderTemplate>
                                                <tr>
                                                    <td style="font-size: 12px;" align="center" width="20%">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>Sr.</strong>
                                                    </td>
                                                    <td style="font-size: 12px;" width="60%">
                                                        <strong>Expense Name</strong>
                                                    </td>
                                                    <td style="font-size: 12px;" align="right" width="60%">
                                                        <strong>Total Amount</strong>&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="white_bg" align="center" width="20%">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#Container.ItemIndex+1 %>.
                                                    </td>
                                                    <td class="white_bg" width="60%">
                                                        <%#Eval("Acnt_Name")%>
                                                    </td>
                                                    <td class="white_bg" align="right" width="20%">
                                                        <%#Convert.ToDouble(Eval("Amnt")).ToString("N2")%>&nbsp;&nbsp;&nbsp;
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
                                    <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table6">
                                        <tr>
                                            <td class="white_bg" width="5%">
                                            </td>
                                            <td class="white_bg" width="10%">
                                            </td>
                                            <td class="white_bg" width="20%" align="center">
                                            </td>
                                            <td class="white_bg" width="20%">
                                            </td>
                                            <td class="white_bg" width="12%" align="center">
                                                <asp:Label ID="Label8" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                            </td>
                                            <td class="white_bg" width="10%" align="left">
                                            </td>
                                            <td class="white_bg" width="15%" align="right">
                                                <asp:Label ID="lblExpenseTotal" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </div>
                        <div id="DivTollDetl" visible="false" runat="server">
                            <tr>
                                <td colspan="4">
                                    <b>Toll Details</b>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table7">
                                        <asp:Repeater ID="RptTollDetl" runat="server" OnItemDataBound="RptTollDetl_ItemDataBound">
                                            <HeaderTemplate>
                                                <tr>
                                                    <td style="font-size: 12px;" align="center" width="20%">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <strong>Sr.</strong>
                                                    </td>
                                                    <td style="font-size: 12px;" width="60%">
                                                        <strong>Toll Name</strong>
                                                    </td>
                                                    <td style="font-size: 12px;" align="right" width="60%">
                                                        <strong>Total Amount</strong>&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="white_bg" align="center" width="20%">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <%#Container.ItemIndex+1 %>.
                                                    </td>
                                                    <td class="white_bg" width="60%">
                                                        <%#Eval("Tolltax_name")%>
                                                    </td>
                                                    <td class="white_bg" align="right" width="20%">
                                                        <%#Convert.ToDouble(Eval("Amnt")).ToString("N2")%>&nbsp;&nbsp;&nbsp;
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
                                    <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table8">
                                        <tr>
                                            <td class="white_bg" width="5%">
                                            </td>
                                            <td class="white_bg" width="10%">
                                            </td>
                                            <td class="white_bg" width="20%" align="center">
                                            </td>
                                            <td class="white_bg" width="20%">
                                            </td>
                                            <td class="white_bg" width="12%" align="center">
                                                <asp:Label ID="Label10" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                            </td>
                                            <td class="white_bg" width="10%" align="left">
                                            </td>
                                            <td class="white_bg" width="15%" align="right">
                                                <asp:Label ID="lblTollTotal" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </div>
                        <tr>
                            <td style="width: 100%;vertical-align:top;">
                                <table width="100%">
                                    <tr>
                                        <td colspan="3" align="left" width="65%" style="vertical-align:top;">
                                            <table style="vertical-align:top;">
                                                <tr>
                                                   
                                                    <td>
                                                     <asp:Label ID="lblremarkp" runat="server" Font-Size="15px" Text="Remarks"></asp:Label><br />
                                                        </td>
                                                        <td>
                                                        :
                                                        </td>
                                                        <td><div style="word-wrap: break-word; width: 350px; padding-top:15px;">
                                                        <asp:Label ID="lblRemarks" runat="server" Width="350px" Font-Size="13px"></asp:Label></div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="35%">
                                            <table width="100%">
                                                <tr runat="server" id="PrintFooterChlnTot">
                                                    <td align="left">
                                                        <asp:Label ID="lbl1" runat="server" Text="Challan Total" Font-Size="13px"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblValueChlnToatl" runat="server" Font-Size="13px" valign="right"></asp:Label>&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="PrintFooterIncentive">
                                                    <td align="left">
                                                        <asp:Label ID="lbl5" runat="server" Text="Incentive Amount (+)" Font-Size="13px"
                                                            valign="right"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblValueIncentiveAmnt" runat="server" Font-Size="13px" valign="right"></asp:Label>&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="PrintFooterFuelTot">
                                                    <td align="left">
                                                        <asp:Label ID="lbl2" runat="server" Text="Fuel Total (-)" Font-Size="13px" valign="right"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblValueFuelTotal" runat="server" Font-Size="13px" valign="right"></asp:Label>&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="PrintFooterExpTotal">
                                                    <td align="left">
                                                        <asp:Label ID="lbl3" runat="server" Text="Expense Total (-)" Font-Size="13px" valign="right"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblValueExpTotal" runat="server" Font-Size="13px" valign="right"></asp:Label>&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="PrintFooterTollTotal">
                                                    <td align="left">
                                                        <asp:Label ID="lbl4" runat="server" Text="Toll Total (-)" Font-Size="13px" valign="right"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblValueTollTOtal" runat="server" Font-Size="13px" valign="right"></asp:Label>&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <b>
                                                            <asp:Label ID="lbl7" runat="server" Text="Net Total" Font-Size="13px" valign="right"></asp:Label></b>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align="right">
                                                        <b>
                                                            <asp:Label ID="lblFinalTotal" runat="server" Font-Size="13px" valign="right"></asp:Label></b>&nbsp;&nbsp;
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
                                                    <td align="left" class="white_bg" style="font-size: 13px; font-family: Kruti Dev 010"
                                                        width="70%">
                                                        <b>
                                                            <%--उपरोक्त बिलटियों के माफ़िक़ इस चालान का सारा माल सही व सुरक्षित हालत में संभाल लिया
                                                                    हे व सही स्टेशन पर सुरक्षित पहुचाने की व वापिस चालान पर भरपाई लाकर कंपनी को देने
                                                                    की मेरी व मेरे मालिक की पूर्णतया जिम्मेदारी हे |--%>
                                                            Trip Total : &nbsp;&nbsp;<asp:Label ID="lblAmountToword" runat="server" Width="450px"
                                                                Font-Size="13px"></asp:Label>
                                                        </b>
                                                    </td>
                                                    <td align="right" class="white_bg" style="font-size: 13px; font-family: Kruti Dev 010"
                                                        valign="top" width="30%">
                                                        <b>
                                                            <%--<br />
                                                                    <br />
                                                                    हस्ताक्षर चालक&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;हस्ताक्षर बुकिंग क्लर्क &nbsp;</b>--%>
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
        }

        function CalcAmnt() {

            if (document.getElementById("<%=txtAdvAmnt.ClientID %>").value == '') {

                document.getElementById("<%=txtAdvAmnt.ClientID %>").value = "0.00";
            }
             var AdvAmtnt = 0; 
           if (document.getElementById("<%=txtAdvAmnt.ClientID %>").value != "") { AdvAmtnt = document.getElementById("<%=txtAdvAmnt.ClientID %>").value; }

            AdvAmtnt = AdvAmtnt.split(',').join('');
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
                    url: "TripEntry.aspx/ProductList",
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
    <script type="text/javascript">
    function Calckm() {

            if (document.getElementById("<%=txtStratKms.ClientID %>").value == '') {

                document.getElementById("<%=txtStratKms.ClientID %>").value = "0.00";
            }

            if (document.getElementById("<%=txtEndKms.ClientID %>").value == '') {
                document.getElementById("<%=txtEndKms.ClientID %>").value = "0.00";
            }

            var total = 0; var startkm = 0; var endkm = 0; 
            if (document.getElementById("<%=txtStratKms.ClientID %>").value != "") { startkm = document.getElementById("<%=txtStratKms.ClientID %>").value; }
            if (document.getElementById("<%=txtEndKms.ClientID %>").value != "") { endkm = document.getElementById("<%=txtEndKms.ClientID %>").value; }
           
            startkm = startkm.split(',').join('');
            endkm = endkm.split(',').join('');
            document.getElementById("<%=txtKMS.ClientID %>").value = (parseFloat(parseFloat(endkm) - parseFloat(startkm))).toFixed(2);
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
