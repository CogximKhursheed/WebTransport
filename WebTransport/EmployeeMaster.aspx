<%@ Page Title="Employee Master" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="EmployeeMaster.aspx.cs" Inherits="WebTransport.EmployeeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upMaster" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkbtnSave" />
            <asp:PostBackTrigger ControlID="lnkbtnUpload" />
        </Triggers>
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-2">
                </div>
                <div class="col-lg-8">
                    <section class="panel panel-default full_form_container employee_master_form">
                <header class="panel-heading font-bold form_heading">EMPLOYEE MASTER
                  <span class="view_print"><a href="ManageEmployee.aspx"><asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a><%--<a href="#"><i class="fa fa-print icon"></i></a>--%></span>
                </header>
                <div class="panel-body">
                  <form class="bs-example form-horizontal">
                    <div class="clearfix">
						          <!-- first main Column-->
						        	<div class="col-lg-8">
						            <!-- first left row -->
						            <div class="clearfix first_left">
						              <section class="panel panel-in-default">
						                <div class="panel-body">
						                  <div class="clearfix odd_row  ">
						                  	<div class="col-sm-6">
						                  		<label class="col-sm-5 control-label">Emp. Name<span class="required-field">*</span> </label>
	                                            <div class="col-sm-7">
                                                  <asp:TextBox ID="txtEmpName" PlaceHolder="Name" runat="server" CssClass="form-control" MaxLength="40" TabIndex="1" onkeyup="sync()" onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox>                                                
                                                <asp:RequiredFieldValidator ID="rfvEmpName" runat="server" ControlToValidate="txtEmpName"  ValidationGroup="Save" ErrorMessage="Please enter employee name!" CssClass="classValidation"  SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
	                                           
	                                            </div>
						                  	</div>
						                  	<div class="col-sm-6">
						                  		<label class="col-sm-5 control-label">Father'sName</label>
	                                            <div class="col-sm-7">
                                                  <asp:TextBox ID="txtFatherName" PlaceHolder="Father Name" runat="server" CssClass="form-control" MaxLength="40" TabIndex="2" onDrop="blur();return false;" onpaste="return false" oncut="return false"  oncopy="return false"></asp:TextBox>
	                                            
	                                            </div>
						                  	</div>
	                                    </div>
						          <div class="clearfix even_row">
                              	<label class="col-sm-5 control-label" style="width: 21%;">Address </label>
                                <div class="col-sm-7" style="width: 79%;">
                                  <asp:TextBox ID="txtAddress" PlaceHolder="Address" runat="server" CssClass="form-control" MaxLength="186"  TabIndex="3" onDrop="blur();return false;" onpaste="return false" oncut="return false"  oncopy="return false" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                                 
                                </div>
	                            </div>
	                            <div class="clearfix odd_row  ">
						        <div class="col-sm-6">
						            <label class="col-sm-5 control-label">State<span class="required-field">*</span> </label>
	                                <div class="col-sm-7">
                                      <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" TabIndex="4">
                                    </asp:DropDownList>                                                                                
                                    <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="ddlState"
                                        ValidationGroup="Save" ErrorMessage="Please select state name!" CssClass="classValidation"
                                        InitialValue="0" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
	                                </div>
						        </div>
						        <div class="col-sm-6">
						            <label class="col-sm-5 control-label">City<span class="required-field">*</span></label>
	                                <div class="col-sm-7">
                                      <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" TabIndex="5">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity"
                                        ValidationGroup="Save" ErrorMessage="Please select city name!" CssClass="classValidation"
                                        InitialValue="0" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
	                                               
	                                </div>
						        </div>
	                            </div>
	                            <div class="clearfix even_row  ">
						                  	<div class="col-sm-6">
						                  		<label class="col-sm-5 control-label">Pin Code<span class="required-field">*</span> </label>
	                                <div class="col-sm-7">
                                     <asp:TextBox ID="txtPinCode" PlaceHolder="Pincode" runat="server" CssClass="form-control" MaxLength="6" TabIndex="6" onDrop="blur();return false;" onpaste="return false" oncut="return false"
                                        oncopy="return false"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPinCode"
                                        ValidationGroup="Save" ErrorMessage="Please enter 6 digit code!" CssClass="classValidation"
                                       SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>                                                                                     
                                    <asp:RegularExpressionValidator ID="revPinCode" runat="server" ControlToValidate="txtPinCode"
                                        CssClass="classValidation" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Save"
                                        ErrorMessage="Please enter 6 digit code!" ValidationExpression="\d{6}"></asp:RegularExpressionValidator>
	                                
	                                </div>
						                  	</div>
						                  	<div class="col-sm-6">
						                  		<label class="col-sm-5 control-label">Mobile</label>
	                                <div class="col-sm-7">
                                     <asp:TextBox ID="txtMobile" PlaceHolder="Mobile No." runat="server" CssClass="form-control" MaxLength="10"  TabIndex="7" onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox>
	                                 
	                                </div>
						                  	</div>
	                            </div>
	                            <div class="clearfix odd_row  ">
						                  	<div class="col-sm-6">
						                  		<label class="col-sm-5 control-label">Phone<span class="required-field">*</span> </label>
	                                <div class="col-sm-7">
                                     <asp:TextBox ID="txtPhone" PlaceHolder="Phone No." runat="server" CssClass="form-control" MaxLength="12" oncopy="return false"  oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="8"></asp:TextBox>
	                              <asp:RequiredFieldValidator ID="rfvtxtphone" runat="server" ControlToValidate="txtPhone"
                                ValidationGroup="Save" ErrorMessage="Please Enter Phone!" CssClass="classValidation"
                                SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
	                                </div>
						                  	</div>
						                  	<div class="col-sm-6">
						                  		 <label style="visibility:hidden" class="col-sm-5 control-label">User Name<span class="required-field">*</span></label>
                                                <div class="col-sm-7">
                                                <asp:TextBox ID="txtUName" PlaceHolder="User Name" Visible="false" runat="server" CssClass="form-control" TabIndex="9" autocomplete="off" onDrop="blur();return false;" onpaste="return false" oncut="return false"
                                                oncopy="return false" MaxLength="20"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Enabled="false" Visible="false" ControlToValidate="txtUName"
                                                ValidationGroup="Save" ErrorMessage="Please select User Name!" CssClass="classValidation"
                                                SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                              
                                                </div>
						                  	</div>
	                            </div>
						                </div>
						              </section>
						            </div>                        
						             

						          </div>
						          <!-- second main Column -->
						          <div class="col-lg-4">
						            <!-- first_one left row -->
						            <div class="clearfix first_one_left">
						              <section class="panel panel-in-default">
						                <div class="panel-body">
                              <div class="clearfix odd_row">
                              	<div class="col-sm-5">
                                  <asp:FileUpload ID="fuPicture" ToolTip="Size Between 20 to 100 KB. Hight & Width : Between 10 to 300 px." runat="server" />
                              	</div>
	                            </div>
	                            <div class="clearfix even_row">
                              	<div class="col-sm-5">
                                <asp:LinkButton ID="lnkbtnUpload" runat="server" CssClass="btn full_width_btn btn-sm btn-primary" OnClick="btnUpload_Click"><i class="fa fa-upload"></i>Upload</asp:LinkButton>
                               
                              	</div>	                              	
	                            </div>
	                            <div class="clearfix odd_row">
                              	<div style="text-align: center;">
                              	
                                     <asp:Image ID="imgEmp" runat="server" ImageUrl="~/img/placeholder.png" Width="117px" Height="117px" />
                                     <br />
                                        <font size="1" color="red">Size Between 20 to 100 KB.
                                        <br />
                                        Height & Width : Between 10 to 300 px.
                                     </font>
                              	</div>	                              	
	                            </div>
	                           
						                </div>
						              </section>
						            </div>                        

						          </div>

				        		</div>
				        		<!-- second  section -->
                 		<div class="clearfix second_section">
                      <section class="panel panel-in-default">  
                        <div class="panel-body">
                        	<div class="clearfix estimate_fourth_row even_row">
                            <div class="col-sm-4">
                              <label class="col-sm-5 control-label">D.O.B</label>
                              <div class="col-sm-7">
                              <asp:TextBox ID="txtDOB" runat="server" CssClass="input-sm datepicker form-control" TabIndex="10" onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox>
                             
                              </div>
                            </div>
                           	<div class="col-sm-4">
                              <label class="col-sm-3 control-label">D.O.J<span class="required-field">*</span></label>
                              <div class="col-sm-9">
                                 <asp:TextBox ID="txtDOJ" runat="server" CssClass="input-sm datepicker form-control" TabIndex="11"  onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox>                                                         
                                <asp:RequiredFieldValidator ID="rfvDOJ" runat="server" ControlToValidate="txtDOJ"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select joining date!" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                               
                              </div>
                            </div>
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label">D.O.R</label>
                              <div class="col-sm-9">
                                <asp:TextBox ID="txtDOR" runat="server" CssClass="input-sm datepicker form-control" TabIndex="12" onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox>
                                
                              </div>
                            </div>
                          </div>
                          <div class="clearfix estimate_fourth_row odd_row">
                            <div class="col-sm-4">
                              <label class="col-sm-5 control-label">Designation<span class="required-field">*</span></label>
                              <div class="col-sm-7">
                                <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" TabIndex="13" AutoPostBack="true" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDesig" runat="server" ControlToValidate="ddlDesignation"
                                    ValidationGroup="Save" ErrorMessage="Please select designation!" CssClass="classValidation"
                                    InitialValue="0" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                            
                              </div>
                            </div>
                           	<div class="col-sm-4">
                              <label class="col-sm-3 control-label">Gender</label>
								<div class="col-sm-9">
								<div class="radio" style="display:inline;padding-top: 4px;">
									<label class="radio-custom">
                                    <asp:RadioButton ID="RdoBtnMale" runat="server" GroupName="gndr" Checked="true" TabIndex="14" />Male
									</label>
								</div>
								<div class="radio" style="display:inline;padding-top: 4px;">
									<label class="radio-custom">
                                    <asp:RadioButton ID="RdoBtnFemale" runat="server" GroupName="gndr" Text="" TabIndex="15" />Female
									</label>
								</div>
								</div>
                            </div>
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label">Remarks</label>
                              <div class="col-sm-9">
                                <asp:TextBox ID="txtRemarks" PlaceHolder="Remarks"  runat="server" CssClass="form-control" MaxLength="180"  TabIndex="16" onDrop="blur();return false;" onpaste="return false" oncut="return false"
                                                                oncopy="return false" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                             
                              </div>
                            </div>
                          </div>
                          <div class="clearfix estimate_fourth_row even_row">
                            <div class="col-sm-4"> 
                                    <label class="col-sm-5 control-label">Email<span class="required-field">*</span></label>
	                                <div class="col-sm-7">
                                     <asp:TextBox ID="txtEmail" PlaceHolder="Email Id" runat="server" CssClass="form-control" MaxLength="30" oncopy="return false"  oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="17"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter e-mail!" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                                  
                                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                        CssClass="classValidation" Display="Dynamic" ErrorMessage="Not a valid email!" SetFocusOnError="true" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"  ValidationGroup="Save"></asp:RegularExpressionValidator>
	                             
	                                </div>
                            </div>
                           	<div class="col-sm-4">
                              <label class="col-sm-4 control-label">Password</label>
                              <div class="col-sm-8">
                                <asp:TextBox ID="txtPassword" PlaceHolder="Password" runat="server" CssClass="form-control" TextMode="Password"  MaxLength="20" TabIndex="18" onDrop="blur();return false;" onpaste="return false"
                                    oncut="return false" oncopy="return false"></asp:TextBox>
                         
                              </div>
                            </div>

                            <div class="col-sm-4">	                                
                              <div class="col-sm-7">
                               	<label class="col-sm-6 control-label">Computer</label>
                                <div class="col-sm-6">
                                 <asp:DropDownList ID="ddlcomputerstatus" runat="server" CssClass="form-control" TabIndex="19"
                                    Width="75px">
                                    <asp:ListItem Text="Yes" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="1"></asp:ListItem>
                                </asp:DropDownList>                               
                                </div>                              
                              </div>
                              <div class="col-sm-5">
                              	<label class="col-sm-10 control-label">IsActive</label>
                                 <asp:CheckBox ID="chkIsActive" CssClass="col-sm-2" runat="server" Checked="true" TabIndex="20" Text />                                
                              </div>
                            </div>
                          </div>  
                          <div class="clearfix estimate_fourth_row odd_row">
							<label class="col-md-3 control-label" for="example-chosen-multiple" style="width:14.3%;">Location</label>
							<div class="col-md-9" style="width:85.7%; overflow-x: auto;">
                                <asp:CheckBoxList ID="chklistFromcity" runat="server" TabIndex="21" Visible="true"
                                        RepeatColumns="8" RepeatDirection="Horizontal">
                                    </asp:CheckBoxList>
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
                            <div class="col-lg-2"></div>
                            <div class="col-lg-8">
                              <div class="col-sm-4">
                                <asp:LinkButton ID="lnkbtnNew" runat="server" 
                                      CssClass="btn full_width_btn btn-s-md btn-info" OnClick="lnkbtnNew_OnClick" 
                                      TabIndex="24"><i class="fa fa-file-o"></i>New</asp:LinkButton>                             
                              </div>                                  
                              <div class="col-sm-4">
                               <asp:LinkButton ID="lnkbtnSave" runat="server" 
                                      CssClass="btn full_width_btn btn-s-md btn-success" CausesValidation="true" 
                                      ValidationGroup="Save" OnClick="lnkbtnSave_OnClick" TabIndex="22"><i class="fa fa-save"></i>Save</asp:LinkButton> 
                               
                              </div>
                              <div class="col-sm-4">
                               <asp:LinkButton ID="lnkbtnCancel" runat="server" 
                                      CssClass="btn full_width_btn btn-s-md btn-danger" 
                                      OnClick="lnkbtnCancel_OnClick" TabIndex="23"><i class="fa fa-close"></i>Cancel</asp:LinkButton> 
                                
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
                <div class="col-lg-2">
                </div>
            </div>
            <asp:HiddenField ID="hidEmpIdno" runat="server" />
            <asp:HiddenField ID="hidmindate" runat="server" />
            <asp:HiddenField ID="hidmaxdate" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
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
            $('#<%=txtDOB.ClientID %>').datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
            $('#<%=txtDOJ.ClientID %>').datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
            $('#<%=txtDOR.ClientID %>').datepicker({
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