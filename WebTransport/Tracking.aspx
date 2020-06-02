<%@ Page Title="Tracking Form" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="Tracking.aspx.cs" Inherits="WebTransport.Tracking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
    <div class="row ">
        <div class="col-lg-1"></div>
        <div class="col-lg-10">
            <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">Tracking Form
                    <span class="view_print"><a href="TrackingList.aspx" tabindex="27"><asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a>                  
                  <%--<a href="#"><i class="fa fa-print icon"></i></a>--%>
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
                           	  <label class="col-sm-3 control-label" style="width: 29%;">Vehicle No.<span class="required-field">*</span></label>
                           		<div class="col-sm-9" style="width: 71%;">
                                <asp:DropDownList ID="DdlVehicleNo" runat="server" CssClass="chzn-select" style="width:100%;" TabIndex="1"  onchange="javascript:vehicleviaddl();">   
                                 </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="Rfvvehicle" runat="server" Display="Dynamic" ControlToValidate="DdlVehicleNo"
                                    ValidationGroup="save" ErrorMessage="Please Select Vehicle Number" InitialValue="0"
                                    SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div> 	                          
                           	</div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 26%;">Date<span class="required-field">*</span></label>
                              <div class="col-sm-4" style="width: 50%;">
                              <asp:TextBox ID="txtDate" runat="server" CssClass="input-sm datepicker form-control"   MaxLength="12"  oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" data-date-format="dd-mm-yyyy" TabIndex="2" ></asp:TextBox>                                                   
                              </div>
                           	</div>
                           	<div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 17%;">Lane<span class="required-field">*</span></label>
                                   <div class="col-sm-9" style="width: 75%;">
                               <asp:DropDownList ID="DdlLane" runat="server" CssClass="form-control"  AutoPostBack="true" style="width:100%;" OnSelectedIndexChanged="DdlLane_SelectedIndexChanged" TabIndex="3"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RFVLane" runat="server" Display="Dynamic"
                                ControlToValidate="DdlLane" ValidationGroup="save" ErrorMessage="Please Select Lane!"
                                InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                          </div>
                            <div class="clearfix even_row">
                              	<div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 25%;">From City<span class="required-field">*</span></label>
                                   <div class="col-sm-9" style="width: 75%;">
                               <asp:DropDownList ID="DdlFromCity" runat="server" CssClass="form-control"  AutoPostBack="true" style="width:100%;" OnSelectedIndexChanged="DdlFromCity_SelectedIndexChanged" TabIndex="4"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RFVfromcity" runat="server" Display="Dynamic"
                                ControlToValidate="DdlFromCity" ValidationGroup="save" ErrorMessage="Please Select City From!"
                                InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                              <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 25%;">To City<span class="required-field">*</span></label>
                                   <div class="col-sm-9" style="width: 75%;">
                               <asp:DropDownList ID="DdlToCity" runat="server" CssClass="form-control"  AutoPostBack="true" style="width:100%;" OnSelectedIndexChanged="DdlToCity_SelectedIndexChanged" TabIndex="5"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="Rfvtocity" runat="server" Display="Dynamic"
                                ControlToValidate="DdlToCity" ValidationGroup="save" ErrorMessage="Please Select City To!"
                                InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                                  <div class="col-sm-4">
                           	  <label class="col-sm-3 control-label" style="width: 26%;">From Loc.<span class="required-field">*</span></label>
                           		<div class="col-sm-8" style="width: 65%;">
                                <asp:DropDownList ID="DdlFromLoc" runat="server" CssClass="chzn-select" style="width:100%;" TabIndex="6"  onchange="javascript:locviaddl();">   
                                 </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVDdlFromLoc" runat="server" Display="Dynamic" ControlToValidate="DdlFromLoc"
                                    ValidationGroup="save" ErrorMessage="Please Select From Location" InitialValue="0"
                                    SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div> 	                          
                           	</div>
                               </div>
                            <div class="clearfix odd_row">
                                <div class="col-sm-5">
                           	  <label class="col-sm-3 control-label" style="width: 31%;">Company Name<span class="required-field">*</span></label>
                           		<div class="col-sm-9" style="width: 50%;">
                                <asp:DropDownList ID="DdlCompName" runat="server" CssClass="chzn-select" style="width:100%;" TabIndex="7"  onchange="javascript:compviaddl();">   
                                 </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlCompName" runat="server" Display="Dynamic" ControlToValidate="DdlCompName"
                                    ValidationGroup="save" ErrorMessage="Please Select Company Name" InitialValue="0"
                                    SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div> 	                          
                           	</div>
                           </div>
                        </div>
                      </section>                        
                    </div>
                    <!-- second  section -->
                 		<div class="clearfix second_section">
                      <section class="panel panel-in-default">  
                        <div class="panel-body">
                          <div class="clearfix even_row">
                            <div class="col-sm-2" style="text-align: center;">
                              <label class="control-label">From City<span class="required-field">*</span></label>
                                <div>
                                    <asp:HiddenField ID="hidDdlFrom" runat="server" />
                                    <asp:DropDownList ID="DdlFromCty" runat="server" CssClass="chzn-select" style="width:100%;"  TabIndex="12" OnSelectedIndexChanged="DdlFromCty_SelectedIndexChanged">
                            </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="DdlFromCty"
                                Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"
                                ErrorMessage="Select From City!" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-2" style="text-align: center;">
                              <label class="control-label">To City<span class="required-field">*</span></label>
                                <div>
                                    <asp:HiddenField ID="hidtocity" runat="server" />
                                     <asp:DropDownList ID="DdlToCty" runat="server" CssClass="chzn-select" style="width:100%;"  TabIndex="13" OnSelectedIndexChanged="DdlToCty_SelectedIndexChanged">
                                </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvto" runat="server" ControlToValidate="DdlToCty"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"
                                    ErrorMessage="Select To City!" CssClass="classValidation"></asp:RequiredFieldValidator>        
                              </div>
                            </div>
                            <div class="col-sm-1" style="text-align: center;">
                              <label class="control-label">Leg<span class="required-field">*</span></label>
                                <div>
                               <asp:TextBox ID="txtLeg" runat="server" CssClass="form-control" Style="text-align: center;" Placeholder="Leg" TabIndex="14"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFVLeg" runat="server" ControlToValidate="txtLeg"
                                    Display="Dynamic" SetFocusOnError="true" ValidationGroup="Submit" ErrorMessage="Enter Leg!"
                                    CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                           	<div class="col-sm-1" style="text-align: center;">
                              <label class="control-label">ETA<span class="required-field">*</span></label>
                              <div>
                               <asp:TextBox ID="txtETA" runat="server" CssClass="form-control" Style="text-align: right;" TabIndex="15"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFVETA" runat="server" ControlToValidate="txtETA"
                                    Display="Dynamic" SetFocusOnError="true" ValidationGroup="Submit" ErrorMessage="Enter ETA!"
                                    CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-1" style="text-align: center;">
                              <label class="control-label">ATA<span class="required-field">*</span></label>
                               <div>
                               <asp:TextBox ID="txtATA" runat="server"  CssClass="form-control" Style="text-align: right;" TabIndex="16"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFVATA" runat="server" ControlToValidate="txtATA"
                                    Display="Dynamic" SetFocusOnError="true" ValidationGroup="Submit" ErrorMessage="Enter ATA!"
                                    CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-1" style="text-align: center;">
                              <label class="control-label">ETD<span class="required-field">*</span></label>
                                <div>
                               <asp:TextBox ID="txtETD" runat="server"  CssClass="form-control" Style="text-align: right;" TabIndex="17"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFVETD" runat="server" ControlToValidate="txtETD"
                                    Display="Dynamic" SetFocusOnError="true" ValidationGroup="Submit" ErrorMessage="Enter ETD!"
                                    CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-1" style="text-align: center;">
                              <label class="control-label">ATD<span class="required-field">*</span></label>
                                <div>
                               <asp:TextBox ID="txtATD" runat="server"  CssClass="form-control" Style="text-align: right;" TabIndex="18"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFVATD" runat="server" ControlToValidate="txtATD"
                                    Display="Dynamic" SetFocusOnError="true" ValidationGroup="Submit" ErrorMessage="Enter ATD!"
                                    CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                           	<div class="col-sm-1" style="text-align: center;">
                              <label class="control-label">TATInHrs<span class="required-field">*</span></label>
                                    <div>
                               <asp:TextBox ID="txtTAThrs" runat="server"  CssClass="form-control" Style="text-align: right;" TabIndex="19"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTAThrs"
                                    Display="Dynamic" SetFocusOnError="true" ValidationGroup="Submit" ErrorMessage="Enter TAT in Hrs!"
                                    CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-1" style="text-align: center;">
                              <label class="control-label">DelayInHrs<span class="required-field">*</span></label>
                                <div>
                               <asp:TextBox ID="Txtdelayhrs" runat="server"  CssClass="form-control" Style="text-align: right;" TabIndex="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="Rfvdelayhrs" runat="server" ControlToValidate="Txtdelayhrs"
                                    Display="Dynamic" SetFocusOnError="true" ValidationGroup="Submit" ErrorMessage="Enter Delay in Hrs!"
                                    CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-1" style="text-align: center;">
                              <label class="control-label">Remakrs<span class="required-field">*</span></label>
                                <div>
                               <asp:TextBox ID="txtremark" runat="server"  CssClass="form-control" Style="text-align: center;" Placeholder="Remarks" TabIndex="21"></asp:TextBox>
                              </div>
                            </div>
                              </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-3 pull-right">
                              <div class="col-sm-6">     
                                 <asp:LinkButton ID="lnkbtnSubmit" runat="server" OnClick="lnkbtnSubmit_OnClick" CssClass="btn full_width_btn btn-sm btn-primary subnew" TabIndex="22"  ToolTip="Click to Submit" CausesValidation="true" ValidationGroup="Submit" >Submit</asp:LinkButton> 
                             </div>
                              <div class="col-sm-6">
                             <asp:LinkButton ID="lnkbtnNew" runat="server" OnClick="lnkbtnNew_OnClick" CssClass="btn full_width_btn btn-sm btn-primary subnew" TabIndex="23"  ToolTip="Click to New" >New</asp:LinkButton> 
                          </div>
                            </div>
                            </div>
                        </div>
                      </section>                        
                    </div>
                    <!-- third  section -->
                    <div class="clearfix third_right">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body" style=" overflow-x: auto;">     
                          <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None"  Width="100%" GridLines="Both" CssClass="display nowrap dataTable"
                           AllowPaging="false" BorderWidth="0"  ShowFooter="true" OnRowCommand="grdMain_RowCommand" OnRowCreated="grdMain_RowCreated">
                            <RowStyle CssClass="odd" />
                            <AlternatingRowStyle CssClass="even" />                           
                            <Columns>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="50" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("Id") %>' CommandName="cmdedit" TabIndex="5" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" TabIndex="6" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="From City" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="50" HorizontalAlign="Center"/>
                                    <ItemTemplate>
                                        <%#Eval("FromCity")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To City" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="50" HorizontalAlign="Center"/>
                                    <ItemTemplate>
                                        <%#Eval("ToCity")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Leg" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="50" HorizontalAlign="Center"/>
                                    <ItemTemplate>
                                        <%#Eval("Leg")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="70" HeaderText="ETA">
                                    <ItemStyle HorizontalAlign="Center" Width="70" />
                                    <ItemTemplate>
                                       <%#Eval("ETA")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="70" HeaderText="ATA">
                                    <ItemStyle HorizontalAlign="Center" Width="70" />
                                    <ItemTemplate>
                                        <%#Eval("ATA")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="90" HeaderText="ETD">
                                    <ItemStyle HorizontalAlign="Center" Width="90" />
                                    <ItemTemplate>
                                        <%#Eval("ETD")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="90" HeaderText="ATD">
                                    <ItemStyle HorizontalAlign="Center" Width="90" />
                                    <ItemTemplate>
                                        <%#Eval("ATD")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150" HeaderText="TAT in Hrs">
                                    <ItemStyle HorizontalAlign="Center" Width="150" />
                                    <ItemTemplate>
                                        <%#Eval("TAT_in_hrs")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150" HeaderText="Delay in Hrs">
                                    <ItemStyle HorizontalAlign="Center" Width="150" />
                                    <ItemTemplate>
                                        <%#Eval("Delay_in_hrs")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100" HeaderText="Remarks">
                                    <ItemStyle HorizontalAlign="Center" Width="100" />
                                    <ItemTemplate>
                                       <%#Eval("Remarks")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                                <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        </div>
                      </section>
                    </div> 
                     <!-- fourth row -->
                    <div class="clearfix fourth_right">
                      <section class="panel panel-in-default btns_without_border">                            
                        <div class="panel-body">     
                          <div class="clearfix odd_row">
                            <div class="col-lg-12" style="float:right">                                         
                              <div class="col-sm-3" >                              
                              <asp:LinkButton ID="lnkbtnMnNew" runat="server" CausesValidation="false" Visible="False" CssClass="btn full_width_btn btn-s-md btn-info" OnClick="lnkbtnMnNew_OnClick" TabIndex="26"  ><i class="fa fa-file-o"></i>New</asp:LinkButton> 
                              </div>
                              <div class="col-sm-3">                          
                                <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" ValidationGroup="save" CssClass="btn full_width_btn btn-s-md btn-success" OnClick="lnkbtnSave_OnClick" TabIndex="24" ><i class="fa fa-save"></i>Save</asp:LinkButton>  
                                  <asp:HiddenField ID="hidid" runat="server" Value="" />
                                  <asp:HiddenField ID="Hidrowid" runat="server" />
                                  <asp:HiddenField ID="hidmindate" runat="server" />
                                  <asp:HiddenField ID="hidmaxdate" runat="server" />
                                  </div>
                              <div class="col-sm-3">
                              <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-danger" OnClick="lnkbtnCancel_OnClick" TabIndex="25"  ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
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
        <div class="col-lg-1">
        </div>
    </div>
    <script type="text/javascript">                $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript">
        function compviaddl() {
            var id = document.getElementById("<%=DdlCompName.ClientID %>").value;
        }
        function vehicleviaddl() {
            var id = document.getElementById("<%=DdlVehicleNo.ClientID %>").value;
        }
        function locviaddl() {
            var id = document.getElementById("<%=DdlFromLoc.ClientID %>").value;
        }
        </script>
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
