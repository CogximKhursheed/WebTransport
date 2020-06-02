<%@ Page Title="Material Issue" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="MaterialIssue.aspx.cs" EnableEventValidation="false" Inherits="WebTransport.MaterialIssue" %>

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

            $("#<%=txtGRDate.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });

            $("#<%=txtAlignDate.ClientID %>").datepicker({
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
    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-1">
                </div>
                <div class="col-lg-10">
                    <section class="panel panel-default full_form_container employee_master_form">
                <header class="panel-heading font-bold form_heading">MATERIAL ISSUE
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <span id="SpaMatrialIssue" runat="server" visible="false">
                  <asp:Label ID="lblMatrialIssue" runat="server" Text="Matrial Issue No. :" Font-Size="18px"></asp:Label>
                  <asp:Label ID="lblMatrialIssueVal" runat="server" Font-Size="18px"></asp:Label>
                  </span>
                
                  <span class="view_print"><a href="ManageMaterialIssue.aspx">LIST</a>
                  <asp:LinkButton ID="lnkPrint" runat="server" Visible="false" title="Print" OnClientClick="return CallPrint('print');"><i class="fa fa-print icon"></i></asp:LinkButton>
                  <asp:ImageButton ID="imgPDF" runat="server" AlternateText="PDF" ImageUrl="~/images/pdf.jpeg"
                                                Visible="false" title="PDF" Height="16px" />
                  </a>
                  </span>
                </header>
                <div class="panel-body">
                  <form class="bs-example form-horizontal">
                    <div class="clearfix"> 
						        	<div class="col-lg-8"> 
						            <div class="clearfix first_left">
						              <section class="panel panel-in-default">
						                <div class="panel-body">
						                  <div class="clearfix odd_row">
						                  	<div class="col-sm-6">
						                  		<label class="col-sm-5 control-label" style="width: 30%;">Date Range<span class="required-field">*</span> </label>
	                                        <div class="col-sm-7" style="width: 70%;">
	                                              <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control"  AutoPostBack="True" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged" >
                                                 </asp:DropDownList> 
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="ddlDateRange"
                                                    ValidationGroup="save" ErrorMessage="Select Date Range!" SetFocusOnError="true"
                                                    CssClass="classValidation"></asp:RequiredFieldValidator>                              
	                                        </div>
						                  	</div>
						                  	<div class="col-sm-6">
						                  		<label class="col-sm-5 control-label" style="width: 21%;">Date<span class="required-field">*</span></label>
						                  		<div class="col-sm-5" style="width: 30%;"> 
                                       <asp:TextBox ID="txtGRDate" runat="server" class="input-sm datepicker form-control" MaxLength="6"  onchange="Focus()" ></asp:TextBox>                          
                                        <asp:RequiredFieldValidator ID="rfvtxtGRDate" runat="server" Display="Dynamic" ControlToValidate="txtGRDate"
                                            ValidationGroup="save" ErrorMessage="Enter Date!" SetFocusOnError="true"
                                            CssClass="classValidation"></asp:RequiredFieldValidator>          
	                                </div>
	                                <div class="col-sm-2" style="width: 46%;">
                                         <asp:TextBox ID="txtGRNo" runat="server" class="form-control" Style="text-align: right;"
                                                 Enabled="true" AutoPostBack="true" MaxLength="9"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtGRNo"
                                            ValidationGroup="save" ErrorMessage="Enter no!" SetFocusOnError="true"
                                            CssClass="classValidation"></asp:RequiredFieldValidator>           
	                                </div>
						                  	</div>
	                            </div>
						                  
	                            <div class="clearfix even_row">
						             <div class="col-sm-6">
						                  <label class="col-sm-5 control-label" style="width: 30%;">Loc.[From]<span class="required-field">*</span> </label>
	                                      <div class="col-sm-7" style="width: 60%;">
	                                        <asp:DropDownList ID="ddlLocation" runat="server" class="form-control" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList> 
                                       
                                            <asp:RequiredFieldValidator ID="rfvtxtfromcity" runat="server" Display="Dynamic"
                                            ControlToValidate="ddlLocation" ValidationGroup="save" ErrorMessage="Please Select From Location!"
                                            InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>                   
	                                    </div>
	                                    <div class="col-sm-1" style="width: 10%;">
	                                	    <asp:LinkButton ID="lnkBtnLocation" runat="server" ToolTip="Update From Location"    Height="23px"
                                                class="btn-sm btn btn-primary acc_home" OnClick="lnkBtnLocation_Click"><i class="fa fa-refresh"></i></asp:LinkButton>                               
	                              	    </div>
						            </div>
                                     <div class="col-sm-6">
						                <label class="col-sm-5 control-label" style="width: 21%;">Type</label>
                                        <div class="col-sm-7" style="width: 76%;">
					                    <div class="radio" style="display:inline;padding-top: 4px;">
						                    <label class="radio-custom">
                                             &nbsp;<asp:RadioButton ID="rdoMIssue"  Checked="true" runat="server"  AutoPostBack="true"
                                                GroupName="Against" oncheckedchanged="rdoMIssue_CheckedChanged"/>Issue 
						                    </label>
					                    </div>
					                    <div class="radio" style="display:inline;padding-top: 4px; padding-left:10px">
						                    <label class="radio-custom">
                                             <asp:RadioButton ID="rdoSaleIssue" runat="server" GroupName="Against"  AutoPostBack="true"
                                                CssClass="by_receipt" oncheckedchanged="rdoSaleIssue_CheckedChanged"/>Issue To Sale
						                    </label>
					                    </div> 
                                        </div>
                                        </div>
                                 </div>
                                     <div class="clearfix odd_row">
                                       
                                    <div class="col-sm-4" >
						                <label class="col-sm-5 control-label" style="width: 30%;">TruckNo.</label>
	                                    <div class="col-sm-7" style="width: 57%;">
	                                     <asp:DropDownList ID="ddlTruckNo" runat="server" class="form-control"  AutoPostBack="True" OnSelectedIndexChanged="ddlTruckNo_SelectedIndexChanged">
                                         </asp:DropDownList>   
	                                    </div>
	                                    <div class="col-sm-1" style="width: 10%;">
	                                	     <asp:LinkButton ID="lnkTruckRefresh" runat="server" ToolTip="Update Truck No." Height="23px"
                                                class="btn-sm btn btn-primary acc_home" OnClick="lnkTruckRefresh_Click"><i class="fa fa-refresh"></i></asp:LinkButton>                             
	                              	    </div>    	
	                                 </div>
                                      <div class="col-sm-2" >
                                          <label class="col-sm-5 control-label" style="width: 39%;">Fitment km</label>
                                             <div class="col-sm-7" style="width: 92%;">
	                                    <asp:TextBox ID="txtfitmentkm" runat="server" PlaceHolder="Fitment Km" class="form-control" ></asp:TextBox>     
	                                    </div>
                                       </div>

                                        <div class="col-sm-6">
						                <label class="col-sm-5 control-label" style="width: 21%;">Previous<br>
                                            Receiver
                                            </label>
	                                    <div class="col-sm-7" style="width: 76%;">
	                                        <asp:DropDownList ID="ddlReciver" Enabled="false" runat="server" class="form-control">
                                            </asp:DropDownList>                            
	                                    </div>
						            </div>
                                     </div>
	                                 <div class="clearfix even_row">
                                         <div class="col-sm-6">
                                            <label class="col-sm-5 control-label" style="width: 30%;">Party Name <span id="DivPartyName" runat="server" class="required-field">*</span> </label>
                                            <div class="col-sm-5" style="width: 70%;"> 
                                                <asp:DropDownList ID="ddlPartyName" runat="server" class="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvPartyName" runat="server" Display="Dynamic"
                                                    ControlToValidate="ddlPartyName" ValidationGroup="save" ErrorMessage="Party Required!"
                                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>   
	                                        </div>
                                         </div>
                                         <div class="col-sm-6">
						                    <label class="col-sm-5 control-label"  style="width: 21%;">Owner</label>
						                    <div class="col-sm-5" style="width: 76%;"> 
                                                <asp:TextBox ID="txtOwner" runat="server" PlaceHolder="Owner Name" class="form-control" ></asp:TextBox>                                 
	                                        </div>
						                 </div>
	                                  </div>
                                     <div class="clearfix odd_row">
                                     <div class="col-sm-6">
						                  	<label class="col-sm-5 control-label" style="width: 30%;">Driver</label>
	                                        <div class="col-sm-7" style="width: 70%;">
	                                            <asp:DropDownList ID="ddlDriver" runat="server" class="form-control">
                                                </asp:DropDownList>                                 
	                                        </div>
						                </div>
                                        <div class="col-sm-6">
                              	        <label class="col-sm-5 control-label" style="width: 21%;">Remark</label>
                                            <div class="col-sm-7" style="width: 76%;"> 
                                            <asp:TextBox ID="txtRemarkhead" runat="server" autocomplete="off" class="form-control parsley-validated"  MaxLength="30" placeholder="Enter Remark"  Style="resize: none" TextMode="MultiLine"></asp:TextBox>
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
						                <div class="panel-body" style="overflow-y:auto; height: 230px;">
                                               <asp:GridView ID="grdPrvIssue"  runat="server" AutoGenerateColumns="false" BorderStyle="None"
                                    Width="100%" GridLines="None" AllowPaging="True" CssClass="display nowrap dataTable"  BorderWidth="0"   PageSize="3" 
                                    OnPageIndexChanging="grdPrvIssue_PageIndexChanging">
                                  <RowStyle CssClass="odd" />
                                  <AlternatingRowStyle CssClass="even" />   
                                    <Columns>
                                    <asp:TemplateField HeaderText="Date" HeaderStyle-Width="30%" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="30%" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%# string.Format("{0:dd/MM/yyyy}",Eval("Date"))%>  
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item" HeaderStyle-Width="70%" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="70%" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                    <%#Eval("Item")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                    </EmptyDataTemplate>
                                  
                                </asp:GridView>
	                           
						                </div>
						              </section>
						            </div>
						          </div>
				        		</div>
				        		<!-- second  section -->
                 		<div class="clearfix third_right">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body">   
                          	<div class="dataTables_add_entry">
                              <div class="clearfix estimate_first1_row even_row">
                                <div class="col-sm-3">
                                               <label class="control-label">
                                               Issued Item Name<span class="required-field">*</span></label>
                                               <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="true" 
                                                 class="form-control" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" >
                                             </asp:DropDownList>                                         
                                              <asp:RequiredFieldValidator ID="rfvPartno" runat="server" ControlToValidate="ddlItemName" CssClass="classValidation" Display="Dynamic" 
                                                 ErrorMessage="Select Item Name!" InitialValue="0" SetFocusOnError="true" 
                                                 ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-2">
                             	  <label class="control-label">Tyre Size<span class="required-field"></span></label>
                               <div>
                               <asp:DropDownList ID="ddltyresize" runat="server" AutoPostBack="true" 
                               OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged"
                               class="form-control" Enabled="false">
                                </asp:DropDownList>
                               </div>
                            </div>
                                 <div class="col-sm-1" style="width:6%" id="DivReport" runat="server">
                                             <label class="control-label">Report</label>
                                           <asp:LinkButton ID="lnkbtnReport" runat="server" ToolTip="Material Issue History Report" 
                                                Height="23px" class="btn-sm btn btn-primary acc_home" data-toggle="modal" 
                                                data-target="#Upload_div" onclick="lnkbtnReport_Click"><i class="fa fa-search-plus"></i></asp:LinkButton>  
                                </div>
                                <div class="col-sm-1">
                                            <label class="control-label">
                                            Is.Qty<span class="required-field">*</span></label>
                                            <asp:TextBox ID="txtQuantity" runat="server" AutoComplete="off" AutoPostBack="false" class="form-control" MaxLength="6" 
                                                oncontextmenu="return false" oncopy="return false" oncut="return false"  onKeyPress="return checkfloat(event, this);" onpaste="return false" 
                                                Style="text-align: right;">1</asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtQuantity" runat="server" 
                                                ControlToValidate="txtQuantity" CssClass="classValidation" Display="Dynamic" 
                                                ErrorMessage="Enter Quantity!"   SetFocusOnError="true" 
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-1" style="width:10%">
                                            <label class="control-label">
                                            Is.Weight</label>
                                            <asp:TextBox ID="txtweight" runat="server" AutoComplete="off" 
                                                class="form-control" MaxLength="30" oncontextmenu="return false" 
                                                oncopy="return false" oncut="return false" Style="text-align: right"
                                                onKeyPress="return checkfloat(event, this);" onpaste="return false" 
                                                placeholder="Enter Weight" Text="0.00" ></asp:TextBox>
                              
                                </div>
                                <div class="col-sm-1" style="width:10%">
                                 
                                        <label class="control-label">
                                        Is.Rate<span class="required-field">*</span></label>
                                        <asp:TextBox ID="txtrate" runat="server" AutoComplete="off" 
                                            class="form-control" MaxLength="30" oncontextmenu="return false" 
                                            oncopy="return false" oncut="return false"  Style="text-align: right"
                                            onKeyPress="return checkfloat(event, this);" onpaste="return false" 
                                            placeholder="Enter Rate" Text="0.00"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtrate" runat="server" 
                                            ControlToValidate="txtrate" CssClass="classValidation" Display="Dynamic" 
                                            ErrorMessage="Enter Rate!" SetFocusOnError="true" 
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                       <asp:CompareValidator ID="CompareValidator1" runat="server" ValueToCompare="0" ControlToValidate="txtrate"  CssClass="classValidation"
                                ErrorMessage="Can't 0!" Operator="GreaterThan" Type="Double" ValidationGroup="Submit"></asp:CompareValidator>
                               
                                </div>
                                <div class="col-sm-2" style="width:12%">
                                            <label class="control-label">
                                            Is.Serial No</label>
                                            <asp:DropDownList ID="ddlSerialNo" runat="server"  CssClass="form-control" AutoPostBack="true"  onselectedindexchanged="ddlSerialNo_SelectedIndexChanged">
                                             </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSerialNo" runat="server" 
                                                ControlToValidate="ddlSerialNo" CssClass="classValidation" Display="Dynamic" 
                                                ErrorMessage="Select SerialNo!" InitialValue="0" SetFocusOnError="true" 
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-2">
	                                <div class="col-sm-6" style="width:33%">
                                         <label class="control-label">
                                            NSD</label>
                                       <asp:TextBox ID="txtNSD" runat="server" AutoComplete="off" Text="0.00"
                                                class="form-control" MaxLength="6" oncontextmenu="return false" 
                                                oncopy="return false" oncut="return false" onpaste="return false"></asp:TextBox>
                                  </div>
                                  <div class="col-sm-6">
                                    <label class="control-label">
                                            PSI</label>
                                        <asp:TextBox ID="txtPSI" runat="server" AutoComplete="off" Text="0.00"
                                                class="form-control" MaxLength="6" oncontextmenu="return false" 
                                                oncopy="return false" oncut="return false" onpaste="return false" ></asp:TextBox>
                                     
                                  </div>
                                     
	                              </div>
                                 
                              </div>
                              <div class="clearfix estimate_first1_row even_row">
                                <div class="col-sm-1" style="width:12%">
                                            <label class="control-label">
                                            Is.Type<span class="required-field" id="SpanIsType" runat="server">*</span></label>
                                            <asp:DropDownList ID="ddlTType" runat="server"  CssClass="form-control">
                                             </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                ControlToValidate="ddlTType" CssClass="classValidation" Display="Dynamic" 
                                                ErrorMessage="Select SerialNo!" InitialValue="0" SetFocusOnError="true" 
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>
                                  <div class="col-sm-1" style="width:13%">
                                    <label class="control-label" >
                                            Tyre Align<span class="required-field" id="SpanTyreAlign" runat="server">*</span></label>
                                        <asp:DropDownList ID="ddlAlign" AutoPostBack="true" runat="server"  
                                          CssClass="form-control" onselectedindexchanged="ddlAlign_SelectedIndexChanged">
                                               <asp:ListItem Text="Yes" Value="1"> </asp:ListItem>
                                           <asp:ListItem Text="No" Value="2"> </asp:ListItem>
                                          
                                             </asp:DropDownList>
                                     
                                  </div>
                                   <div class="col-sm-1" style="width:14.5%">
                                    <label class="control-label">
                                            Prev AlignDate<span class="required-field" id="SpanPrevAlignDate" runat="server">*</span></label>
                                        <asp:TextBox ID="txtPrevAlignDate" runat="server" AutoComplete="off" class="input-sm datepicker form-control"
                                                 MaxLength="6" oncontextmenu="return false" Enabled="false"
                                                oncopy="return false" oncut="return false" onpaste="return false" ></asp:TextBox>
                                     
                                  </div>
                                   <div class="col-sm-1" style="width:10%">
                                    <label class="control-label">
                                            N.AlignDate<span class="required-field" id="SpanAlignDate" runat="server">*</span></label>
                                        <asp:TextBox ID="txtAlignDate" runat="server" AutoComplete="off" class="input-sm datepicker form-control"
                                                 MaxLength="6" oncontextmenu="return false" 
                                                oncopy="return false" oncut="return false" onpaste="return false" ></asp:TextBox>
                                     
                                  </div>
                               <div class="col-sm-2">
                             	  <label class="control-label">Tyre Position<span class="required-field"></span></label>
                               <div>
                               <asp:DropDownList ID="ddltyreposition" runat="server"
                               class="form-control" Enabled="false">
                                </asp:DropDownList>
                               </div>
                            </div>
                                  </div>
                              </div></div></section>
                               <section class="panel panel-in-default">                            
                        <div class="panel-body">   
                          	<div class="dataTables_add_entry">
                             <div class="clearfix estimate_first1_row even_row">
                                <div class="col-sm-3">
                                               <label class="control-label">
                                            Rcpt.Serial No<span id="SpanRcptSrNo" runat="server" visible="true" class="required-field"></span></label>
                                            <asp:DropDownList ID="ddlRSerialNo" runat="server"  CssClass="form-control">
                                             </asp:DropDownList>
      
                                </div>
                                <div class="col-sm-1">
                                   <label class="control-label">
                                            Rcpt.NSD</label>
                                       <asp:TextBox ID="txtRNSD" runat="server" AutoComplete="off" Text="0.00"
                                                class="form-control" MaxLength="6" oncontextmenu="return false" 
                                                oncopy="return false" oncut="return false" onpaste="return false"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                     <label class="control-label">
                                            Rcpt.PSI</label>
                                        <asp:TextBox ID="txtRPSI" runat="server" AutoComplete="off" Text="0.00"
                                                class="form-control" MaxLength="6" oncontextmenu="return false" 
                                                oncopy="return false" oncut="return false" onpaste="return false"></asp:TextBox>       
                              
                                </div>
                                 <div class="col-sm-1" style="width:10%">
                                            <label class="control-label">
                                           Rcpt.Type</label>
                                            <asp:DropDownList ID="ddlRType" runat="server"  CssClass="form-control">
                                             </asp:DropDownList>
                                            
                                </div>
                                <div class="col-sm-1" style="width:10%">
                                     <label class="control-label">
                                            Rcpt.Price</label>
                                        <asp:TextBox ID="txtPrice" runat="server" AutoComplete="off" Text="0.00" 
                                                class="form-control" MaxLength="6" oncontextmenu="return false" 
                                                oncopy="return false" oncut="return false" Style="text-align: right" 
                                                onKeyPress="return checkfloat(event, this);" onpaste="return false" 
                                                placeholder="Enter Price" ></asp:TextBox>       
                              
                                </div>
                                <div class="col-sm-1">
                                          
                                </div>
                                
                                <div class="col-sm-3">
	                               <div class="col-sm-6">
                                       
                                        <asp:LinkButton ID="lnkbtnSubmit" runat="server" OnClick="lnkbtnSubmit_OnClick" CssClass="btn full_width_btn btn-sm btn-primary subnew"  ToolTip="Click to Submit" CausesValidation="true" ValidationGroup="Submit">Submit</asp:LinkButton>
                                  </div>
                                  <div class="col-sm-6">
                                        <asp:LinkButton ID="lnkbtnAdd" runat="server" OnClick="lnkbtnAdd_OnClick" CssClass="btn full_width_btn btn-sm btn-primary subnew" 
                                        ToolTip="Click to new" >New</asp:LinkButton>
                                     
                                  </div>
	                              </div>
                                <div class="col-sm-1">

                                </div>
                              </div>
                            </div></div></section>

                            <section class="panel panel-in-default">                            
                        <div class="panel-body">  
								<div class="dataTables_add_entry" style="overflow-x:auto;">
                                   <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"
                                        Width="100%" GridLines="None" EnableViewState="true" AllowPaging="false" BorderWidth="0"
                                        ShowFooter="true" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand" OnRowCreated="grdMain_RowCreated"
                                        OnRowDataBound="grdMain_RowDataBound">
                                        <RowStyle CssClass="odd" />
                                        <AlternatingRowStyle CssClass="even" />    
                                        <Columns>
                                         <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="50" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                  <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("id") %>' CommandName="cmdedit" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                  <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("id") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>                                          
                                                </ItemTemplate>
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="50" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Item Name">
                                                <ItemStyle HorizontalAlign="Left" Width="200" />
                                                <ItemTemplate>
                                                    <%#Eval("Item_Name")%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotal" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                             <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Tyre size">
                                                <ItemStyle HorizontalAlign="Left" Width="200" />
                                                <ItemTemplate>
                                                    <%#Eval("Tyresize")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Serial No">
                                                <ItemStyle HorizontalAlign="Left" Width="200" />
                                                <ItemTemplate>
                                                    <%#Eval("SerialNo")%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    
                                                </FooterTemplate>
                                            </asp:TemplateField>      
                                              <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="NSD">
                                                <ItemStyle HorizontalAlign="Left" Width="200" />
                                                <ItemTemplate>
                                                    <%#Eval("NSD")%>
                                                </ItemTemplate>
                                             
                                            </asp:TemplateField>     
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="PSI">
                                                <ItemStyle HorizontalAlign="Left" Width="200" />
                                                <ItemTemplate>
                                                    <%#Eval("PSI")%>
                                                </ItemTemplate>
                                             
                                            </asp:TemplateField>  

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Align">
                                                <ItemStyle HorizontalAlign="Left" Width="200" />
                                                <ItemTemplate>
                                                    <%#Eval("Align")%>
                                                </ItemTemplate>
                                             
                                            </asp:TemplateField> 

                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Prev AlignDate">
                                                <ItemStyle HorizontalAlign="Left" Width="200" />
                                                <ItemTemplate>
                                                    <%#Eval("PrevAlignDate")%>
                                                </ItemTemplate>
                                             
                                            </asp:TemplateField> 

                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Align Date">
                                                <ItemStyle HorizontalAlign="Left" Width="200" />
                                                <ItemTemplate>
                                                    <%#Eval("AlignDate")%>
                                                </ItemTemplate>
                                             
                                            </asp:TemplateField> 

                                              <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="T.Type">
                                                <ItemStyle HorizontalAlign="Left" Width="200" />
                                                <ItemTemplate>
                                                    <%#Convert.ToString(Eval("TType")) == "1" ? "New" : (Convert.ToString(Eval("TType")) == "2" ? "Old" : (Convert.ToString(Eval("TType")=="3"? "Retrited":"")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                              
                                            <asp:TemplateField  HeaderStyle-CssClass="gridHeaderAlignCenter" HeaderStyle-Width="100" HeaderText="Quantity">
                                                <ItemStyle CssClass="gridHeaderAlignCenter" Width="100" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Quantity")))%>
                                                </ItemTemplate>
                                                <FooterStyle  CssClass="gridHeaderAlignCenter" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblQuantity" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignCenter" HeaderStyle-Width="100" HeaderText="Weight">
                                                <ItemStyle CssClass="gridHeaderAlignCenter" Width="100" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Weight")=="" ? 0:Convert.ToDouble(Eval("Weight"))))%>
                                                </ItemTemplate>
                                                <FooterStyle CssClass="gridHeaderAlignCenter"  />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblWeight" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField  HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="100" HeaderText="R.Serial No">
                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("RSerialNo")%>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="R.NSD">
                                                <ItemStyle HorizontalAlign="Left" Width="200" />
                                                <ItemTemplate>
                                                    <%#Eval("RNSD")%>
                                                </ItemTemplate>
                                             
                                            </asp:TemplateField>     
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="R.PSI">
                                                <ItemStyle HorizontalAlign="Left" Width="200" />
                                                <ItemTemplate>
                                                    <%#Eval("RPSI")%>
                                                </ItemTemplate>
                                             
                                            </asp:TemplateField>  
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="R.T.Type">
                                                <ItemStyle HorizontalAlign="Left" Width="200" />
                                                <ItemTemplate>
                                                <%#Convert.ToString(Eval("RTType")) == "1" ? "New" : (Convert.ToString(Eval("RTType")) == "2" ? "Old" : (Convert.ToString(Eval("RTType") == "3" ? "Retrited" : "")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>   
                                            <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="100" HeaderText="Item Rate">
                                                <ItemStyle HorizontalAlign="right" Width="100" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Rate")))%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblRate" align="right" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="100" HeaderText="Price">
                                                <ItemStyle HorizontalAlign="right" Width="100" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("RPrice")))%>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField> 
                                                                          
                                            <asp:TemplateField  HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="100" HeaderText="Amount">
                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Amount")))%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                                    
                                    </Columns>
                                    </asp:GridView>
                                    </div>
						
                        </div>
                      </section>
                    </div>
                     <div id="Upload_div" class="modal fade" role="dialog">
                            <div class="modal-dialog" style="width:775px">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h4 class="popform_header">
                                            Material Issue History for Item : <asp:Label ID="lblItemeRep" runat="server" Text=""></asp:Label></h4>
                                    </div>
                                    <div class="modal-body" style="width:775px">
                                    <section class="panel panel-default full_form_container material_search_pop_form">
								            <div class="panel-body" style="overflow-y:auto;height:300px">
									            <!-- First Row start -->
								           
                                            <div class="clearfix even_row">
                                             <asp:GridView ID="grdMatHis" runat="server" AutoGenerateColumns="false" 
                                                    BorderStyle="None" CssClass="display nowrap dataTable"
                                        Width="100%" GridLines="None" EnableViewState="true" AllowPaging="True" 
                                                    BorderWidth="0" PageSize="50"
                                        ShowFooter="true" onpageindexchanging="grdMatHis_PageIndexChanging">
                                        <RowStyle CssClass="odd" />
                                        <AlternatingRowStyle CssClass="even" />    
                                        <Columns>
                                       
                                       <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="50" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Date">
                                                <ItemStyle HorizontalAlign="Left" Width="200" />
                                                <ItemTemplate>
                                                   <asp:Label ID="lblMatdate" runat="server" Text='<%#Convert.ToDateTime(Eval("Date")).ToString("dd-MM-yyyy") %>'></asp:Label>
                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Serial No.">
                                                <ItemStyle HorizontalAlign="Left" Width="200" />
                                                <ItemTemplate>
                                                    <%#Eval("Serial")%>
                                                </ItemTemplate>
                                             
                                            </asp:TemplateField>        
                                              <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="NSD">
                                                <ItemStyle HorizontalAlign="Left" Width="200" />
                                                <ItemTemplate>
                                                    <%#Eval("NSD")%>
                                                </ItemTemplate>
                                             
                                            </asp:TemplateField>     
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="PSI">
                                                <ItemStyle HorizontalAlign="Left" Width="200" />
                                                <ItemTemplate>
                                                    <%#Eval("PSI")%>
                                                </ItemTemplate>
                                             
                                            </asp:TemplateField>  

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Alignment">
                                                <ItemStyle HorizontalAlign="Left" Width="200" />
                                                <ItemTemplate>
                                                    <%#Eval("Align")%>
                                                </ItemTemplate>
                                             
                                            </asp:TemplateField> 

                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Align Date">
                                                <ItemStyle HorizontalAlign="Left" Width="200" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMatAlignDate" runat="server" Text='<%#Convert.ToDateTime(Eval("AlignDate")).ToString("dd-MM-yyyy") %>'></asp:Label>
                                                </ItemTemplate>
                                             
                                            </asp:TemplateField> 

                                                   
                                    </Columns>
                                    </asp:GridView>
                                            </div>                                      
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
                    <div class="clearfix">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body">   
	                        <div class="clearfix even_row">
	                        	<div class="col-sm-8"></div>
	                         	<div class="col-sm-4">
	                            <label class="col-sm-5 control-label">Net Amount</label>
	                            <div class="col-sm-7">
                                   <asp:TextBox ID="txtNetAmnt" runat="server" class="form-control" MaxLength="7"   Enabled="false" Text="0.00" align="right" OnChange="ComputeCosts();"
                                            onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"
                                            onKeyPress="return checkfloat(event, this);"></asp:TextBox>
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
                            <div class="col-lg-2">
                             <asp:Label ID="lblmessage" runat="server" Font-Bold="true" Visible="false" CssClass="redfont1"
                                            Text=""></asp:Label>
                            </div>
                            <div class="col-lg-8">
                              <div class="col-sm-4">
                                <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-info" OnClick="lnkbtnNew_OnClick" ><i class="fa fa-file-o"></i>New</asp:LinkButton>  

                              </div>                                  
                              <div class="col-sm-4" id="DivSaveBtn" runat="server">
                               <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" ValidationGroup="save" CssClass="btn full_width_btn btn-s-md btn-success" OnClick="lnkbtnSave_OnClick" ><i class="fa fa-save"></i>Save</asp:LinkButton>                          
                              </div>
                              <div class="col-sm-4">
                                <asp:LinkButton ID="lnkbtnCancel" runat="server"  CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-danger" OnClick="lnkbtnCancel_OnClick" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
                                 <asp:HiddenField ID="hidmindate" runat="server" />
                                        <asp:HiddenField ID="hidmaxdate" runat="server" />
                                        <asp:HiddenField ID="HidiFromCity" runat="server" />
                                        <asp:HiddenField ID="hidTBBType" runat="server" />
                                        <asp:HiddenField ID="hidrowid" runat="server" />
                                        <asp:HiddenField ID="hidpostingmsg" runat="server" />
                                        <asp:HiddenField ID="hidGRHeadIdno" runat="server" />
                                        <asp:LinkButton ID="lnkbtn" runat="server" Text=""></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnAtSave" runat="server" Text=""></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnAtSave1" runat="server" Text=""></asp:LinkButton>
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
                <div class="col-lg-1" style="display: none">
                    <tr style="display: none">
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
                                    <%#Eval("Rate_Type")%>
                                    <tr>
                                        <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                                            border-left-style: none; border-right-style: none">
                                            <h3>
                                                <strong style="text-decoration: underline">
                                                    <asp:Label ID="lblPrintHeadng" runat="server"></asp:Label></strong></h3>
                                        </td>
                                    </tr>
                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Rate")))%>
                                    <tr>
                                        <td colspan="4">
                                            <table border="0" width="100%">
                                                <tr>
                                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                        Mat.Issue No.
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                        <asp:Label ID="lblTransno" runat="server"></asp:Label>
                                                    </td>
                                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                        Tran. Date
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                        <asp:Label ID="lblTranDate" runat="server"></asp:Label>
                                                    </td>
                                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                        Location
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                        <asp:Label ID="lblLoation" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                        <asp:Label ID="lblIssueType" Text="Type" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                        <asp:Label ID="lblType" runat="server"></asp:Label>
                                                    </td>
                                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                        Truck No.
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                        <asp:Label ID="lblTruckNo" runat="server"></asp:Label>
                                                    </td>
                                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                        Issue To
                                                    </td>
                                                    <td id="TdlblAgent" runat="server">
                                                        :
                                                    </td>
                                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                        <asp:Label ID="lblIssueTo" runat="server"></asp:Label>
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
                                                                <strong>Item Name</strong>
                                                            </td>
                                                            <td style="font-size: 12px" width="10%">
                                                                <strong>Unit Name</strong>
                                                            </td>
                                                            <td style="font-size: 12px" width="10%">
                                                                <strong>Quantity</strong>
                                                            </td>
                                                            <td style="font-size: 12px" width="10%">
                                                                <strong>Weight</strong>
                                                            </td>
                                                            <td style="font-size: 12px" align="left" width="10%">
                                                                <strong>Item Rate</strong>
                                                            </td>
                                                            <td style="font-size: 12px" align="left" width="10%">
                                                                <strong>Amount</strong>
                                                            </td>
                                                            <td style="font-size: 12px" width="20%">
                                                                <strong>Detail</strong>
                                                            </td>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="white_bg" width="10%">
                                                                <%#Container.ItemIndex+1 %>.
                                                            </td>
                                                            <td class="white_bg" width="30%">
                                                                <%#Eval("Item_Modl")%>
                                                            </td>
                                                            <td class="white_bg" width="15%">
                                                                <%#Eval("UOM_Name")%>
                                                            </td>
                                                            <td class="white_bg" width="15%">
                                                                <%#Eval("Qty")%>
                                                            </td>
                                                            <td class="white_bg" width="15%">
                                                                <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Tot_Weght")))%>
                                                            </td>
                                                            <td class="white_bg" width="15%" align="left">
                                                                <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Item_Rate")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                            <td class="white_bg" width="15%" align="left">
                                                                <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                            <td class="white_bg" width="15%" align="right">
                                                                <%#(Eval("Detail"))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <%--<asp:Label ID="lblTotalAmnt" runat="server"></asp:Label>--%>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table2">
                                                <tr>
                                                    <td class="white_bg" width="15%">
                                                    </td>
                                                    <td class="white_bg" width="15%">
                                                    </td>
                                                    <td class="white_bg" width="15%" align="center">
                                                        <asp:Label ID="lblttl" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="white_bg" width="15%" align="left">
                                                        <asp:Label ID="lbltotalqty" Font-Bold="true" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="white_bg" width="12.5%">
                                                        <asp:Label ID="lbltotalWeight" Font-Bold="true" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="white_bg" width="12.5%">
                                                    </td>
                                                    <td class="white_bg" width="12.5%" align="center">
                                                        <asp:Label ID="lblTotalAmnt" Font-Bold="true" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="white_bg" width="12.5%">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%">
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="2" align="left" width="50%">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblremark" runat="server" valign="right"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td colspan="2" width="50%">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblcommission" runat="server" Text="Commission" Font-Size="13px" valign="right"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="valuelblcommission" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                                </td>
                                                                <td style="width: 5px">
                                                                </td>
                                                                <td>
                                                                    &nbsp;&nbsp;
                                                                    <asp:Label ID="lblbilty" runat="server" Text="Bilty" Font-Size="13px" valign="right"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="valuelblbilty" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblcartage" runat="server" Text="Cartage" Font-Size="13px" valign="right"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="valuelblcartage" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                                </td>
                                                                <td style="width: 5px">
                                                                </td>
                                                                <td>
                                                                    &nbsp;&nbsp;
                                                                    <asp:Label ID="lblsurcharge" runat="server" Text="Surcharge" Font-Size="13px" valign="right"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="valuelblsurcharge" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblwages" runat="server" Text="Wages" Font-Size="13px" valign="right"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="valuelblwages" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                                </td>
                                                                <td style="width: 5px">
                                                                </td>
                                                                <td>
                                                                    &nbsp;&nbsp;
                                                                    <asp:Label ID="lblPFAmnt" runat="server" Text="PF" Font-Size="13px" valign="right"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="valuelblPFAmnt" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblTollTax" runat="server" Text="TollTax" Font-Size="13px" valign="right"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="valuelblTollTax" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                                </td>
                                                                <td style="width: 5px">
                                                                </td>
                                                                <td>
                                                                    &nbsp;&nbsp;
                                                                    <asp:Label ID="lblserviceTax" runat="server" Text="S.Tax" Font-Size="13px" valign="right"></asp:Label>
                                                                </td>
                                                                <td id="stax" runat="server">
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="valuelblservceTax" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblservtaxConsigner" runat="server" Text="C.Tax" Font-Size="13px"
                                                                        valign="right"></asp:Label>
                                                                </td>
                                                                <td id="ctax" runat="server">
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="valuelblservtaxConsigner" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                                </td>
                                                                <td style="width: 5px">
                                                                </td>
                                                                <td>
                                                                    &nbsp;&nbsp;
                                                                    <asp:Label ID="lblNetAmnt" runat="server" Text="Net Amnt" Font-Size="13px" valign="right"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="valuelblnetAmnt" runat="server" Font-Size="13px" valign="right"></asp:Label>
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
                                                                    <br />
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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
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
        function ShowModalPopup() {
            ShowDialog(true);
        }
        function ShowDialog(modal) {
            // $("#overlay").show();
            $("#dialog").show();
            $("#dialog").fadeIn(300);

            if (modal) {
                $("#dialog").unbind("click");
                //$("#overlay").unbind("click");
            }
            else {
                //  $("#overlay").click(function (e) {
                $("#dialog").click(function (e) {
                    HideDialog();
                });
            }
        }

        function HideDialog() {
            //   $("#overlay").hide();
            $("#dialog").fadeOut(300);
        }

        function Focus() {
            $("#txtGRNo").focus();
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

        function ShowConfirm() {
            var ans = confirm('Entered Material Issue No. is not in sequence. Do you want to continue?');

            if (ans == false) {
                var btn = document.getElementById('<%=lnkbtn.ClientID%>');
                btn.click();
            }
        }

        function ShowConfirmAtSave() {
            var ans = confirm('Entry already made with this Material Issue. No. Do you want to regenerate it?');

            if (ans == true) {
                var btnsav1 = document.getElementById('<%=lnkbtnAtSave1.ClientID%>');
                btnsav1.click();
            }
            else if (ans == false) {
                var btnsav = document.getElementById('<%=lnkbtnAtSave.ClientID%>');
                btnsav.click();
            }
        }


    </script>
    <script language="javascript" type="text/javascript">
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
