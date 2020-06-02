<%@ Page Title="Delivery Challan" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="DeliveryChallanDetails.aspx.cs" Inherits="WebTransport.DeliveryChallanDetails" %>

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
            WinPrint.close();
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

        $(document).ready(function () {
            setDatecontrol();
        });

        function openModal() {
            $('#dvGrdetails').modal('show');
        }

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
    <%-- <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>--%>
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-10">
            <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">DELIVERY CHALLAN
                  <span class="view_print"><a href="ManageDeliveryChallan.aspx"><asp:Label ID="lblViewList" TabIndex="19" runat="server" Text="LIST"></asp:Label></a>                        
                    &nbsp;
                    <asp:LinkButton ID="lnkbtnPrint" runat="server" ToolTip="Click to print" Visible="false" TabIndex="20" OnClientClick ="return CallPrint('print');"><i class="fa fa-print icon"></i></asp:LinkButton>

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

                                <asp:DropDownList ID="ddldateRange" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="1" OnSelectedIndexChanged="ddldateRange_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddldateRange"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Year!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 29%;">Date<span class="required-field">*</span></label>
                           		<div class="col-sm-4" style="width: 31%;">
                                 <asp:TextBox ID="txtDate" runat="server" CssClass="input-sm datepicker form-control" MaxLength="50" data-date-format="dd-mm-yyyy"  oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="2" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Date!"
                                SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>
                              <div class="col-sm-3" style="width: 40%;">
                               <asp:TextBox ID="txtchallanNo" runat="server" CssClass="form-control"  
                                      MaxLength="7"  oncopy="return false" oncut="return false" 
                                      onDrop="blur();return false;" onpaste="return false"
                                                                    TabIndex="3" ReadOnly="True" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtchallanNo" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter challan no!"
                                    SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>                           
                              </div>                              
                           	</div>
                           	<div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 29%;">Deliv.Place<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 61%;">
                               <asp:DropDownList ID="ddlDelvryPlace" runat="server" CssClass="form-control" 
                                      TabIndex="4" Enabled="False">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlDelvryPlace"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Delv. city!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>                    
                              </div>
                              <div class="col-sm-1" style="width: 10%;">
                              <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/Images/PckLst.png" AlternateText="Search"
                                                        ImageAlign="ABSMiddle" ToolTip="Search" Style="height: 28px" OnClick="imgSearch_Click"
                                                        TabIndex="5" />
                                <%--<a href="#gr_delivery_details_form" class="btn btn-sm btn-primary acc_home" data-toggle="modal" data-target="#gr_delivery_details_form"><i class="fa fa-file"></i></a>--%>
                              </div>
                            </div>
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 29%;">Truck No.<span class="required-field">*</span></label>
                              <div class="col-sm-8" style="width: 71%;">
                               <asp:DropDownList ID="ddlTruckNo" runat="server" AutoPostBack="true" CssClass="form-control"  TabIndex="6"   OnSelectedIndexChanged="ddlTruckNo_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTruckNo"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Truck No" InitialValue="0"
                                    SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>
                              
                            </div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 29%;">Owner<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 71%;">
                              <asp:TextBox ID="txtOwnrNme" runat="server" CssClass="form-control" MaxLength="50" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                TabIndex="7" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtOwnrNme"
                                CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter owner name!"  SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
   
                              </div>
                           	</div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 29%;">Driver</label>

                              <div class="col-sm-9" style="width: 71%;">

                                  <asp:DropDownList ID="ddldriverName" runat="server" CssClass="form-control" InitialValue="0"  TabIndex="8" >
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddldriverName"
                                        CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Driver!"
                                        InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>
                           	</div>
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 29%;">Transport</label>
                              <div class="col-sm-8" style="width: 71%;">
                                  <asp:DropDownList ID="ddlTransportName" runat="server" CssClass="form-control" TabIndex="9">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtOwnrNme"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Transport name!"
                                    SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-8"></div>                           	
                          </div>

                        </div>
                      </section>                        
                    </div>
                    
                    <!-- second  section -->
                    <div class="clearfix third_right">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body" style="overflow-x:auto;">     
                          <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="display nowrap dataTable"
                            TabIndex="15" BorderStyle="Solid"   GridLines="Both" BorderWidth="1"  ShowFooter="true" OnDataBound="grdMain_DataBound" OnRowDataBound="grdMain_RowDataBound">
                            <RowStyle CssClass="odd" />
                            <AlternatingRowStyle CssClass="even" />
                            <Columns>
                                <asp:TemplateField HeaderText="Gr No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="50" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrNo" runat="server" Text='<%#Convert.ToString(Eval("Gr_No")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gr Date" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="50" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrdate" runat="server" Text='<%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MM-yyyy") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receiver Name" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="50" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <%#Convert.ToString(Eval("Reciver"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sender Name" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="50" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <%#Convert.ToString(Eval("Sender"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To City" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="50" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <%#Convert.ToString(Eval("City_Name"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="50" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <%#Convert.ToString(Eval("Qty"))%>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotQty" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Amount" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="50" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <%#Convert.ToDouble(Eval("Net_Amnt")).ToString("N2")%>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lblNetAmnt" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="100" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <%#Convert.ToString(Eval("Remark"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="lblnorecord" runat="server" Text="No record found"></asp:Label>
                            </EmptyDataTemplate>
                        </asp:GridView>

                        </div>
                      </section>
                    </div> 

                    <div class="clearfix">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body">
                          <div class="clearfix odd_row">
                            <div class="col-lg-6">
                            	<div class="clearfix even_row">
                                <label class="col-sm-2 control-label">Delivery Instruction</label>
                                <div class="col-sm-10">
                                   <asp:TextBox ID="txtDelvInstruction" placeholder="Enter Delivery Instruction" runat="server" CssClass="form-control"  MaxLength="200" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                     onpaste="return false" Style="resize: none;" TabIndex="10" Text="" TextMode="MultiLine"></asp:TextBox>
                                </div>
                              </div>
                            </div>
                            <div class="col-lg-6">
                            	<div class="clearfix even_row">
	                              <div class="col-sm-6">
	                                <label class="col-sm-6 control-label">Gross Amount</label>
	                                <div class="col-sm-6">
                                        <asp:TextBox ID="txtGrosstotal" runat="server" CssClass="form-control" Enabled="false" MaxLength="50" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                          onpaste="return false" Style="text-align:right;" TabIndex="11" Text="0.00"></asp:TextBox>
	                                </div>
	                              </div>
	                             	<div class="col-sm-6">
	                                <label class="col-sm-6 control-label">Katt Amount</label>
	                                <div class="col-sm-6">
                                      <asp:TextBox ID="txtkatt" runat="server" AutoPostBack="true" CssClass="form-control"  MaxLength="9" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                     onpaste="return false" OnTextChanged="txtkatt_TextChanged" Style="text-align:right;"  TabIndex="12" Text="0.00"></asp:TextBox>
	                                </div>
	                              </div>			                              
	                            </div>
	                            <div class="clearfix even_row">
	                              <div class="col-sm-6">
	                                <label class="col-sm-6 control-label">Other Amount</label>
	                                <div class="col-sm-6">
                                      <asp:TextBox ID="txtOtherAmnt" runat="server" CssClass="form-control"  MaxLength="50" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                          Style="text-align:right;" TabIndex="13" Text="0.00" OnTextChanged="txtOtherAmnt_TextChanged"  AutoPostBack="True"></asp:TextBox>
	                                </div>
	                              </div>
	                             	<div class="col-sm-6">
	                                <label class="col-sm-6 control-label">Net Amount</label>
	                                <div class="col-sm-6">
                                     <asp:TextBox ID="txtNetAmnt" runat="server" CssClass="form-control" Enabled="false"   MaxLength="50" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                      onpaste="return false" Style="text-align:right;" TabIndex="14" Text="0.00"></asp:TextBox>	                               
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
                      <div class="col-lg-4"></div>
                      <div class="col-lg-4">       
                      <div class="col-sm-4">
                      <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false" Visible="false" TabIndex="17" CssClass="btn full_width_btn btn-s-md btn-info" OnClick="lnkbtnNew_OnClick" ><i class="fa fa-file-o"></i>New</asp:LinkButton>  
              
                       </div>                
                        <div class="col-sm-4">
                           <asp:HiddenField ID="hidid" runat="server" Value="" />
                        <asp:HiddenField ID="Hidrowid" runat="server" Value="" />
                        <asp:HiddenField ID="HidInvoiceTyp" runat="server" />
                        <asp:HiddenField ID="hidmindate" runat="server" />
                        <asp:HiddenField ID="hidmaxdate" runat="server" />
                        <asp:HiddenField ID="hidWorkType" runat="server" />
                        <asp:HiddenField ID="hidpostingmsg" runat="server" />
                        <asp:HiddenField ID="hidOwnerId" runat="server" />
                        <asp:HiddenField ID="hidTdsTaxPer" runat="server" />
                         <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" ValidationGroup="save" TabIndex="15" CssClass="btn full_width_btn btn-s-md btn-success" OnClick="lnkbtnSave_OnClick" ><i class="fa fa-save"></i>Save</asp:LinkButton>                      
                         
                        </div>
                        <div class="col-sm-4">
                         <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-danger" TabIndex="16" OnClick="lnkbtnCancel_OnClick" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>                       
                        </div>
                      </div>
                      <div class="col-lg-4"></div>
                    </div>

                    <!-- popup form GR detail -->
				    <div id="dvGrdetails" class="modal fade">
										  <div class="modal-dialog">
										    <div class="modal-content">
										      <div class="modal-header">
										        <h4 class="popform_header">Gr Detail </h4>
										      </div>
										      <div class="modal-body">
										        <section class="panel panel-default full_form_container material_search_pop_form">
										          <div class="panel-body">
										             <!-- First Row start -->
										            <div class="clearfix odd_row">	                                
	                                <div class="col-sm-6">
	                                  <label class="col-sm-4 control-label">Date From</label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txtDateFrom" runat="server" CssClass="input-sm datepicker form-control" data-date-format="dd-mm-yyyy" TabIndex="85"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtFrm" runat="server" ErrorMessage="Enter From Date!"
                                            Display="Dynamic" CssClass="classValidation" ControlToValidate="txtDateFrom" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                   
                                    </div>
	                                </div>
	                                <div class="col-sm-6">
	                                  <label class="col-sm-4 control-label">Date To</label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txtDateTo" runat="server" CssClass="input-sm datepicker form-control" data-date-format="dd-mm-yyyy" TabIndex="86"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtTo" runat="server" ErrorMessage="Enter To Date!"
                                            Display="Dynamic" CssClass="classValidation" ControlToValidate="txtDateTo" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                    </div>
	                                </div>
	                              </div> 

	                              <div class="clearfix even_row">
	                                <div class="col-sm-6">
	                                <%--<asp:Label runat="server" CssClass="col-sm-4 control-label" ID="lblDelvSerch" Text="Delv. Place"></asp:Label>--%>
                                    <label class="col-sm-4 control-label">Delv. Place<span class="required-field">*</span></label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="ddldelvplace" runat="server" CssClass="form-control" AutoPostBack="false" TabIndex="87">
                                        </asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="rfvdeliveryplace" runat="server" ControlToValidate="ddldelvplace"
                                        CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Delivery Place !"
                                        InitialValue="0" SetFocusOnError="true" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                    </div>
	                                </div>
	                                <div class="col-sm-6" style="padding: 0;">
	                                  <div class="col-sm-4 prev_fetch">
                                         <asp:LinkButton ID="lnkbtnSearch" runat="server" CssClass="btn full_width_btn btn-sm btn-primary" CausesValidation="true" ValidationGroup="RcptEntrySrch" TabIndex="87" OnClick="lnkbtnSearch_OnClick"><i class="fa fa-search"></i>Search</asp:LinkButton>                                   
	                                  </div>
	                                  <div class="col-sm-8"> 
	                                    <%-- <label class="control-label">T. Record(s) : </label>--%>
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
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkId" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gr No." HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("Gr_No"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gr Date" HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MMM-yyyy")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Receiver Name" HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="200px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%#Eval("Reciver")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sender Name" HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="200px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%#Eval("Sender")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To City" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%#Eval("City_Name")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%#Eval("Qty")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="180px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Eval("Net_Amnt")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Eval("Remark")%>
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
							    <div class="modal-footer">
							    <div class="popup_footer_btn">

                                <asp:LinkButton ID="lnkbtnokClick" runat="server" CssClass="btn btn-dark" OnClick="lnkbtnokClick_OnClick"><i class="fa fa-check"></i>Ok</asp:LinkButton>
				
								    <button type="submit" class="btn btn-dark" data-dismiss="modal"><i class="fa fa-times"></i>Close</button>
                                    <div style="float:left;">
                                     <asp:Label ID="lblmsg" runat="server" Text="" Visible="false" CssClass="redfont"></asp:Label>
                                    </div>
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
    <table width="100%">
        <tr style="display: none">
            <td class="white_bg" align="center">
                <div id="print" style="font-size: 13px;">
                    <table cellpadding="1" cellspacing="0" width="800" border="1">
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
                                PH:
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
                                    <strong style="text-decoration: underline">
                                        <asp:Label ID="lblPrintHeadng" runat="server" Text="Goods Receipt"></asp:Label></strong></h3>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table border="0" width="100%">
                                    <tr>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 14px; width: 75px;
                                            border-right-style: none;">
                                            <asp:Label ID="lblchlntext" Text="Challan No." runat="server"></asp:Label>
                                        </td>
                                        <td style="font-size: 14px; width: 2px;">
                                            :
                                        </td>
                                        <td align="left" class="white_bg" style="font-size: 14px; width: 80px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblChlnno" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="width: 85px; font-size: 14px;
                                            border-right-style: none">
                                            <asp:Label ID="lbltxtchlndate" Text="Challan Date" runat="server"></asp:Label>
                                        </td>
                                        <td style="font-size: 14px; width: 2px;">
                                            :
                                        </td>
                                        <td align="left" class="white_bg" style="font-size: 14px; width: 105px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblchlnDate" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 14px; width: 200px;
                                            border-right-style: none">
                                            <asp:Label ID="lbltxtownr" Text="Owner Name :" runat="server"></asp:Label><b><asp:Label
                                                ID="lblOwnr" runat="server"></asp:Label></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="white_bg" valign="top" style="width: 75px; font-size: 14px;
                                            border-right-style: none">
                                            <asp:Label ID="lbltxttruck" Text="Truck No." runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td style="font-size: 14px; width: 2px;">
                                            :
                                        </td>
                                        <td align="left" class="white_bg" style="font-size: 14px; width: 80px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblTrckNo" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" style="width: 85px; font-size: 14px; border-right-style: none">
                                            <asp:Label ID="lbltxtdriver" Text="Driver" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td style="font-size: 14px; width: 2px;">
                                            :
                                        </td>
                                        <td align="left" class="white_bg" style="font-size: 14px; width: 105px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblDrvrName" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 14px; width: 200px;
                                            border-right-style: none">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table border="0" cellspacing="0" style="font-size: 13px" width="100%" id="Table1">
                                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                        <HeaderTemplate>
                                            <tr>
                                                <td style="font-size: 13px;" width="5%">
                                                    <strong>Sr.No</strong>
                                                </td>
                                                <td style="font-size: 13px;" width="6%">
                                                    <strong>GR No.</strong>
                                                </td>
                                                <td style="font-size: 13px;" width="8%">
                                                    <strong>GR Date</strong>
                                                </td>
                                                <td style="font-size: 13px;" width="16%">
                                                    <strong>Receiver Name</strong>
                                                </td>
                                                <td style="font-size: 13px;" width="16%">
                                                    <strong>Sender Name</strong>
                                                </td>
                                                <td style="font-size: 13px;" align="left" width="5%">
                                                    <strong>To City</strong>
                                                </td>
                                                <td style="font-size: 13px;" align="right" width="6%">
                                                    <strong>Qty</strong>
                                                </td>
                                                <td style="font-size: 13px;" align="right" width="10%">
                                                    <strong>Amount</strong>
                                                </td>
                                                <td style="font-size: 13px;" align="right" width="10%">
                                                    <strong>Remark</strong>
                                                </td>
                                                <td style="font-size: 13px;" align="left" width="1%">
                                                    <strong></strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="white_bg" width="5%">
                                                    <%#Container.ItemIndex+1 %>.
                                                </td>
                                                <td class="white_bg" width="6%">
                                                    <%#Eval("GR_No")%>
                                                </td>
                                                <td class="white_bg" width="8%">
                                                    <%#Convert.ToDateTime(Eval("GR_Date")).ToString("dd-MMM-yyyy")%>
                                                </td>
                                                <td class="white_bg" width="16%">
                                                    <%#Eval("Recvr_Name")%>
                                                </td>
                                                <td class="white_bg" width="16%">
                                                    <%#Eval("Sender_Name")%>
                                                </td>
                                                <td class="white_bg" width="10%" align="left">
                                                    <%#Eval("To_City")%>
                                                </td>
                                                <td class="white_bg" width="6%" align="right">
                                                    <%#Eval("Qty")%>
                                                </td>
                                                <td class="white_bg" width="10%" align="right">
                                                    <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%>
                                                </td>
                                                <td class="white_bg" width="10%" align="right">
                                                    <%#Eval("Remark")%>
                                                </td>
                                                <td class="white_bg" width="1%" align="left">
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
                                <table border="0" cellspacing="0" style="font-size: 13px" width="100%" id="Table2">
                                    <tr>
                                        <td class="white_bg" width="60%">
                                        </td>
                                        <td class="white_bg" width="8%" align="right">
                                            <asp:Label ID="lblttl" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                        <td class="white_bg" width="9%" align="right">
                                            <asp:Label ID="lbltotalqty" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                        <td class="white_bg" width="12%" align="right">
                                            <asp:Label ID="lblTotalAmnt" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td class="white_bg" width="1%" align="right">
                                            &nbsp;
                                        </td>
                                        <td class="white_bg" width="10%" align="right">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <table width="100%">
                                    <tr>
                                        <td colspan="3" align="left" width="44%">
                                            <table>
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="25%">
                                            <table>
                                                <tr>
                                                    <td style="width: 90px;">
                                                        <asp:Label ID="lblGroosAmnt" runat="server" Text="Gross Amount" Font-Size="13px"
                                                            valign="right"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align="right" style="width: 90px;">
                                                        <asp:Label ID="valuelblgrossAmnt" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 90px;">
                                                        <asp:Label ID="lblkatt" runat="server" Text="Katt Amount" Font-Size="13px" valign="right"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align="right" style="width: 90px;">
                                                        <asp:Label ID="valuelblKatt" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 90px;">
                                                        <asp:Label ID="LblOtherAmnt" runat="server" Text="Other Amount" Font-Size="13px"
                                                            valign="right"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align="right" style="width: 90px;">
                                                        <asp:Label ID="valueOtherAmnt" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 90px;">
                                                        <asp:Label ID="lblnetTotal" runat="server" Text="Net Amout" Font-Size="13px" valign="right"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align="right" style="width: 80px;">
                                                        <b>
                                                            <asp:Label ID="valuelblnetTotal" runat="server" Font-Size="13px" valign="right"></asp:Label></b>
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
                                                    <td align="left" class="white_bg" style="font-size: 13px;" width="70%">
                                                        <b></b>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td align="right" class="white_bg" style="font-size: 13px;" valign="top" width="30%">
                                                        <b>
                                                            <br />
                                                            <br />
                                                            Signature &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</b>
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
    <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>
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