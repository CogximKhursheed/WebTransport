<%@ Page Title="Gr Prepration" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="GRPrepRetailer.aspx.cs" Inherits="WebTransport.GRPrepRetailer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function SetNumFormt(objddl) {
            if (objddl != "") {
                if (parseFloat(document.getElementById(objddl).value).toFixed(2) == "NaN") {
                    document.getElementById(objddl).value = "0.00";
                }
                else {
                    document.getElementById(objddl).value = parseFloat(document.getElementById(objddl).value).toFixed(2);
                }
            }
        }

        function ChangeOnAgainst() {
            if ((document.getElementById("<%=RDbDirect.ClientID%>").checked)) {
                document.getElementById("<%=lnkbtnGrAgain.ClientID%>").style.visibility = "hidden";
                document.getElementById("<%=ddlGRType.ClientID %>").disabled = false;
                this.OnGrtypeSelect();
            }
            else {
                document.getElementById("<%=lnkbtnGrAgain.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=ddlGRType.ClientID %>").value = "2";
                document.getElementById("<%=ddlGRType.ClientID %>").disabled = true;
                this.OnGrtypeSelect();
            }            
        }

        function OnGrtypeSelect() {
            var value = document.getElementById("<%=ddlGRType.ClientID %>").value;
            if (parseInt(value) > 1) {
                document.getElementById("<%=ddlRcptType.ClientID %>").disabled = true;
                document.getElementById("<%=lnkLorryType.ClientID %>").style.visibility = "hidden";
            }
            else {
                document.getElementById("<%=ddlRcptType.ClientID %>").disabled = false;
                document.getElementById("<%=lnkLorryType.ClientID %>").style.visibility = "visible";
            }
        }

        function SelectSameCity() {
            var val = document.getElementById("<%=ddlToCity.ClientID %>").value;
            if (val != '') {
                document.getElementById("<%=ddlLocation.ClientID %>").value = val;
                document.getElementById("<%=ddlCityVia.ClientID %>").value = val;
            }
        }

        function OnSenderChange(ddlSender) {
            var val = document.getElementById("<%=ddlSender.ClientID %>").value;
            if (parseInt(val) > 0) {
                var id = ddlSender.options[ddlSender.selectedIndex].innerHTML;
                document.getElementById("<%=txtconsnr.ClientID %>").value = id;
            }
            else {
                document.getElementById("<%=txtconsnr.ClientID %>").value = "";
            }
        }

        function OnChangeRec() {
            var val = document.getElementById("<%=ddlRcptType.ClientID %>").value;
            if (parseInt(val) == 57) {
                document.getElementById("<%=lnkLorryType.ClientID %>").disabled = false;                
            }
            else {
                document.getElementById("<%=lnkLorryType.ClientID %>").disabled = true;
            }
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
    <style type="text/css">
        
  tr.noBorder td {
  border-bottom: 0;

}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-lg-12">
        <section class="panel panel-default full_form_container part_purchase_bill_form">
                <header class="panel-heading font-bold form_heading">GR RETAILER
                    <span class="view_print">  <asp:LinkButton ID="lnkBtnLast" 
            class="view_print"  runat="server"  AlternateText="Print" title="Print" 
            Height="16px" onclick="lnkBtnLast_Click">LAST PRINT</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="ManageGRPrepRetailer.aspx"><asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a>&nbsp;
                    <asp:LinkButton ID="lnkbtnPrint" CssClass="fa fa-print icon" Visible="false" runat="server" ToolTip="Print" AlternateText="Print" title="Print" Height="16px" Onclick="lnkbtnPrint_Click"></asp:LinkButton>                    
                  </span></header>
                <div class="panel-body">
                  <form class="bs-example form-horizontal">
                    <div class="clearfix first_section">
                      <section class="panel panel-in-default">  
                        <div class="panel-body">
                          <div class="clearfix odd_row">
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label">Date Range</label>
                              <div class="col-sm-8">
                              <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged">
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="rfvDateRange" runat="server" Display="Dynamic"
                                    ControlToValidate="ddlDateRange" ValidationGroup="save" ErrorMessage="Please Select Date Range."
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label">GR Date<span class="required-field">*</span></label>
                              <div class="col-sm-9">
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtGRDate" OnTextChanged="txtGRDate_TextChanged" onkeypress="return notAllowAnything(event);" runat="server" PlaceHolder="DD-MM-YYYY" CssClass="input-sm datepicker form-control" MaxLength="10" onkeydown = "return DateFormat(this, event.keyCode)" AutoPostBack="True"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtPrefixNo" onkeypress="return allowAlphabetAndNumer(event);"  runat="server" PlaceHolder="Pref No." CssClass="form-control"  ToolTip="Prefix GR"  MaxLength="5"></asp:TextBox>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtGRNo" onkeypress="return allowOnlyNumber(event);" runat="server" CssClass="form-control" Style="text-align: right;" ToolTip="GR Number" Enabled="true"  AutoPostBack="true"  MaxLength="9" OnTextChanged="txtGRNo_TextChanged"></asp:TextBox>
                                </div>
                              </div>
                            </div>
                             <div class="col-sm-4">
                              <label class="col-sm-3 control-label">Against<span class="required-field"> *</span></label>
                              <div class="col-sm-9">
                              <div class="col-sm-5">
                                <label class="radio-custom" style="padding-left:10px">
								    <asp:RadioButton ID="RDbDirect" onchange="javascript:ChangeOnAgainst()" Checked="true" runat="server" GroupName="Against"/><span style="padding-left:2px">Direct</span>
								</label>
                              </div>
                              <div class="col-sm-5">  
                                <label class="radio-custom">
								    <asp:RadioButton ID="rdbAdvanceOrder" visible="false" onchange="javascript:ChangeOnAgainst()" runat="server" GroupName="Against" CssClass="by_receipt"/><%--<span style="padding-left:2px">Adv. Order</span>--%>
								</label>
                                </div>
                                <div class="col-sm-2">
                                <asp:LinkButton ID="lnkbtnGrAgain" visible="false" runat="server" ToolTip="Select Order Details" CssClass="btn-sm btn btn-primary acc_home" type="button" data-toggle="modal"  data-target="#gr_details_form"><i class="fa fa-file"></i></asp:LinkButton>
                                </div>
                              </div>
                            </div>
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-4">
                                <label class="col-sm-3 control-label">Sender<span class="required-field"> *</span></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlSender" runat="server" onchange="javascript:OnSenderChange(this)" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlSender"  runat="server" Display="Dynamic" ControlToValidate="ddlSender"
                                        ValidationGroup="save" ErrorMessage="Please Select Sender's Name." InitialValue="0"
                                        SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                              <div class="col-sm-1">
                              <span id="SpanSenderRefresh" runat="server"  visible="true">
                                <asp:LinkButton ID="lnkBtnSender" runat="server" class="btn-sm btn btn-primary acc_home"><i class="fa fa-refresh"></i></asp:LinkButton>
                                 <asp:HiddenField ID="hidGRHeadIdno" runat="server" />
                                </span>
                              </div>
                            </div>
                           
                             <div class="col-sm-4">
                              <label class="col-sm-3 control-label">Receiver<span class="required-field">*</span></label>
							    <div class="col-sm-8">
						            <asp:DropDownList ID="ddlReceiver" runat="server" CssClass="form-control" >
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlReceiver" runat="server" Display="Dynamic"
                                        ControlToValidate="ddlReceiver" ValidationGroup="save" ErrorMessage="Please Select Receiver's Name."
                                        InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
							    </div>
							    <div class="col-sm-1">
                                <span id="SpanReceiverRefresh" runat="server"  visible="true">
                                    <asp:LinkButton ID="lnkBtnReceiver" runat="server" class="btn-sm btn btn-primary acc_home"><i class="fa fa-refresh"></i></asp:LinkButton>
                                </span>
                              </div>
                            </div>
                             <div class="col-sm-4">
                              <label class="col-sm-3 control-label">From Loc.<span class="required-field"> *</span></label>
                              <div class="col-sm-8">
                                 <asp:DropDownList ID="ddlFromCity" runat="server" CssClass="form-control"
                                 OnSelectedIndexChanged="ddlFromCity_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvtxtfromcity" runat="server" Display="Dynamic"
                                    ControlToValidate="ddlFromCity" ValidationGroup="save" ErrorMessage="Please Select From City."
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                              <div class="col-sm-1">
                              <span id="SpanFromCityRefresh" runat="server">
                                <asp:LinkButton ID="lnkBtnFromCity" runat="server" class="btn-sm btn btn-primary acc_home"><i class="fa fa-refresh"></i></asp:LinkButton>
                              </span>
                              </div>
                            </div>
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-4">
                                <label class="col-sm-3 control-label">To City<span class="required-field"> *</span></label>
                                <div class="col-sm-8">
                                <asp:DropDownList ID="ddlToCity" runat="server" CssClass="form-control" OnSelectedIndexChanged="ToCity_SelectedIndexChanged" onchange="javascript:SelectSameCity()" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlToCity" runat="server" Display="Dynamic" ControlToValidate="ddlToCity"
                                    ValidationGroup="save" ErrorMessage="Please Select To City." InitialValue="0"
                                    SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                              <div class="col-sm-1">
                              <span id="SpanToCityRefresh" runat="server"  visible="true">
                                <asp:LinkButton ID="lnkBtnTocity" runat="server" class="btn-sm btn btn-primary acc_home" ><i class="fa fa-refresh"></i></asp:LinkButton>                              </span>
                              </div>
                            </div>
                            <div class="col-sm-4">
                                    <label class="col-sm-3 control-label">To State</label>    
                                  <div class="col-sm-8">
								    <asp:TextBox ID="txtToState" runat="server" CssClass="form-control" PlaceHolder="Enter Shipment Number" AutoComplete="off" Enabled="false"></asp:TextBox>
                                  </div>
                                </div>
                             <div class="col-sm-4">
                                <label class="col-sm-3 control-label">Del. Place<span class="required-field"> *</span></label>
                                <div class="col-sm-8">
							    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" >
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvLocation" runat="server" Display="Dynamic" ControlToValidate="ddlLocation"
                                    ValidationGroup="save" ErrorMessage="Please Select Delivery Place." InitialValue="0"
                                    SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
							    </div>
							    <div class="col-sm-1">
                                <span id="SpanDelvryPlaceRefresh" runat="server" visible="true">
                                    <asp:LinkButton ID="lnkBtnDelvryPlace" runat="server" class="btn-sm btn btn-primary acc_home" ><i class="fa fa-refresh"></i></asp:LinkButton>
                                </span>
                                </div>
                            </div>
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-4">
                                <label class="col-sm-3 control-label">Via City<span class="required-field"> *</span></label>
                              <div class="col-sm-8">
                                <asp:DropDownList ID="ddlCityVia" runat="server" CssClass="form-control" >
                                </asp:DropDownList>
                              </div>
                              <div class="col-sm-1">
                              <span id="SpanCityViaRefresh" runat="server" visible="true">
                                <asp:LinkButton ID="lnkBtnCityvia" runat="server" class="btn-sm btn btn-primary acc_home"><i class="fa fa-refresh"></i></asp:LinkButton>
                              </span>
                              </div>
                            </div>
                             <div class="col-sm-4">
                              <label class="col-sm-3 control-label">Gr Type</label>
                              <div class="col-sm-8">
                                <asp:DropDownList ID="ddlGRType" onchange="javascript:OnGrtypeSelect()" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Paid GR" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="TBB GR" Value="2" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="To Pay GR" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                              </div>
                            </div>
                             <div class="col-sm-4">
                                <label class="col-sm-3 control-label">Recp. Type</label>
                                <div class="col-sm-8">
							        <asp:DropDownList ID="ddlRcptType" runat="server" OnSelectedIndexChanged="ddlRcptType_SelectedIndexChanged"
                                AutoPostBack="true" CssClass="form-control">
                                    </asp:DropDownList>
							    </div>
                                <div class="col-sm-1">
                                   <asp:LinkButton ID="lnkLorryType" runat="server" ToolTip="Details"
                                          CssClass="btn-sm btn btn-primary acc_home" type="button" data-toggle="modal" 
                                          data-target="#type_popup"><i class="fa fa-file"></i></asp:LinkButton>
                              </div>
                            </div>
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-4">
                                <b><asp:Label ID="lblDelvNo" class="col-sm-3 control-label" runat="server" Text="DI No."></asp:Label></b>
                              <div class="col-sm-7">
                                <asp:TextBox ID="txtDelvNo" onkeypress="return allowAlphabetAndNumer(event);" runat="server" PlaceHolder="Enter Delivery Number" CssClass="form-control" MaxLength="19"></asp:TextBox>     
                              </div>
                              <div class="col-sm-1">
                                <button type="button" id="btnOtherDetails" runat="server" class="btn btn-sm btn-primary acc_home"  data-toggle="modal" data-target="#other_Details"><i class="fa fa-file"></i></button>
                              </div>
                              <div class="col-sm-1" id="UpdateDiNo" runat="server">
                                <asp:LinkButton ID="lnkbtnDiNoUpdate" runat="server" ToolTip="Update DiNo." class="btn-sm btn btn-primary acc_home"><i class="fa fa-check"></i></asp:LinkButton>
                              </div>
                            </div>
                             <div class="col-sm-4">
                                <label class="col-sm-3 control-label">Consignor<span class="required-field"> *</span></label>
                                <div class="col-sm-8">
                                 <asp:TextBox ID="txtconsnr" onkeypress="return allowOnlyAlphabet(event);" runat="server" CssClass="form-control" MaxLength="80" Placeholder="Enter Consignor Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvconsn" runat="server" ControlToValidate="txtconsnr"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Enter Consignor Name." 
                                    SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator> 
                              </div>
                            </div>
                             <div class="col-sm-4">
                                <label class="col-sm-3 control-label">Agent</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlParty" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                              </div>
                              <div class="col-sm-1">
                              <span id="SpanAgentRefresh" runat="server" visible="true">
                                <asp:LinkButton ID="lnkBtnAgent" runat="server" class="btn-sm btn btn-primary acc_home"><i class="fa fa-refresh"></i></asp:LinkButton>
                              </span>
                              </div>
                            </div>
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-4">
                                <label class="col-sm-3 control-label">Manual No</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtManualNo" onkeypress="return allowAlphabetAndNumer(event);" runat="server" CssClass="form-control" PlaceHolder="Manual GR No" Enabled="true"></asp:TextBox>     
                                </div>
                                 <div id="UpdateManNo" runat="server" class="col-sm-1">
                                    <asp:LinkButton ID="lnkbtnManNoUpdate" runat="server" ToolTip="Update Manual No." class="btn-sm btn btn-primary acc_home"><i class="fa fa-check"></i></asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <label class="col-sm-3 control-label">Tran. Type</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlTranType" CssClass="form-control" runat="server" AutoPostBack="false" onchange="javascript:OnFillTran();">                                                                      
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="rfvTranType" runat="server" Display="Dynamic" ControlToValidate="ddlTranType"
                                    ValidationGroup="save" ErrorMessage="Please Select Transportation Type." InitialValue="0"
                                    SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>--%>
							    </div>
                           </div>                            
                           <div class="col-sm-4">
                                <asp:Label ID="lblTruckNo" class="col-sm-3 control-label" runat="server"><span class="required-field">*</span></asp:Label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlTruckNo" CssClass="form-control" ValidationGroup="save" runat="server">
                                    </asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="rfvTruck" runat="server" ControlToValidate="ddlTruckNo"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select Tran. No." 
                                    SetFocusOnError="true" InitialValue="0" ValidationGroup="save"></asp:RequiredFieldValidator> 
                                </div>
                                <div class="col-sm-1">
                                    <asp:LinkButton ID="lnkbtnTruuckRefresh" runat="server" ToolTip="Update Truck No." class="btn-sm btn btn-primary acc_home"><i class="fa fa-refresh"></i></asp:LinkButton>
                                </div>
                            </div>                                
                            </div>
                            <div class="clearfix odd_row">
                               <div class="col-sm-4">
                            <label class="col-sm-3 control-label">Tax Paid By</label>
                              <div class="col-sm-8">
                                <asp:DropDownList ID="ddlSrvcetax" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Transporter" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Consigner" Value="2" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Consignee" Value="3" ></asp:ListItem>
                                </asp:DropDownList>
                              </div>
                            </div>
                            </div>
                            <div class="clearfix even_row">
                            <div class="col-sm-4">
                                <label class="col-sm-3 control-label">Shipmt. No</label>    
                              <div class="col-sm-8">
								<asp:TextBox ID="txtshipment" onkeypress="return allowAlphabetAndNumer(event);"  runat="server" CssClass="form-control" PlaceHolder="Enter Shipment Number" AutoComplete="off" MaxLength="20"></asp:TextBox>
                              </div>
                              <div class="col-sm-1">
                              <asp:LinkButton ID="lnkbtnContnrDtl" runat="server" ToolTip="Shipment Details"
                                          CssClass="btn-sm btn btn-primary acc_home" type="button" data-toggle="modal" 
                                          data-target="#dvContainerdetails"><i class="fa fa-file"></i></asp:LinkButton>
                                 </div>
                            </div>
                            <div class="col-sm-4">
                             <label class="col-sm-3 control-label">Tot. Price</label>
                             <div class="col-sm-9">
                                <asp:TextBox ID="txtTotItemPrice" runat="server" CssClass="form-control" Text="0.00" Style="text-align: right;" 
                                        onDrop="blur();return false;"  onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                              </div>
                            </div>
                             <div class="col-sm-4">                           
                             <label class="col-sm-3">Type&nbsp;</label>
                              <div class="col-sm-4">
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                    <asp:ListItem Text="Item Wise" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Fixed Amount" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                              </div>                           
                             <div class="col-sm-5" id="DivAmount" visible="true" runat="server">
                             <label class="col-sm-3 control-label">Amnt</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtFixedAmount" MaxLength="20" runat="server"  
                                    CssClass="form-control" PlaceHolder="Enter Amount" 
                                    ontextchanged="txtFixedAmount_TextChanged" onKeyPress="return checkfloat(event, this);" AutoPostBack="true"></asp:TextBox>     
                            </div>
                             </div>
                            </div>
                          </div>
                         <div class="clearfix odd_row">                          
                            <label class="col-sm-1 control-label">Remarks</label>
                              <div class="col-sm-7">
                                <asp:TextBox ID="TxtRemark" runat="server" CssClass="form-control" PlaceHolder="Enter Remarks" MaxLength="100" Enabled="true"></asp:TextBox>
                              </div>
                               <label class="col-sm-1 control-label">Modvat</label>
                              <div class="col-sm-3">
                              <asp:CheckBox ID="chkModvat" runat="server" Checked="false"></asp:CheckBox>
                              </div>
                          </div>
                        </div>
                      </section>
    </div>
    <div class="clearfix second_section" id="DivItemPanel" runat="server">
        <section class="panel panel-in-default"> 
                        <div class="panel-body">
                        <div class="clearfix even_row">
                            <div class="col-sm-2">
                                <label class="control-label">Item Name<span class="required-field"> *</span></label>
                                <div>
                                <asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control" AutoPostBack="true"
                                        onselectedindexchanged="ddlItemName_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ControlToValidate="ddlItemName" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"
                                    ErrorMessage="Please Select Item Name." CssClass="classValidation"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-1">
                                <label class="control-label">Unit<span class="required-field"> *</span></label>
                                <div>
                                <asp:DropDownList ID="ddlunitname" runat="server" CssClass="form-control">
                                </asp:DropDownList>                                
                                <asp:RequiredFieldValidator ID="rfvUnit" runat="server" ControlToValidate="ddlunitname" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"
                                    ErrorMessage="Please Select Unit Name." CssClass="classValidation"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-1">
                                <label class="control-label">Rate Type<span class="required-field">*</span></label>
                                <div>
                                <asp:DropDownList ID="ddlRateType" runat="server" CssClass="form-control" >
                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Rate" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Weight" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvRateType" runat="server" ControlToValidate="ddlRateType" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"
                                    ErrorMessage="Select" CssClass="classValidation"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-1">
                                <label class="control-label">Quantity<span class="required-field">*</span></label>
                                <div>
                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" Text="1" 
                                        MaxLength="6" ontextchanged="txtQuantity_TextChanged1" ></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="rfvQty" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" SetFocusOnError="true"   ValidationGroup="Submit"
                                    ErrorMessage="Please Enter Ch. Weight!" CssClass="classValidation"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                             <div class="col-sm-2">
                                <label class="control-label">Dimension</label>
                                <div>
                                    <asp:TextBox ID="txtDimension" runat="server" CssClass="form-control" MaxLength="10" ></asp:TextBox>
                                     <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" SetFocusOnError="true"   ValidationGroup="Submit"
                                    ErrorMessage="Please Enter Ch. Weight!" CssClass="classValidation"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="col-sm-1">
                            <label class="control-label">Per CFT</label>  
                            <div>
                               <asp:TextBox ID="txtCFT" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                               </div>
                             </div>
                            <div class="col-sm-1">
                                <label class="control-label">Ch. Weight<span class="required-field">*</span></label>
                                <div>
                                <asp:TextBox ID="txtweight" Text="0.00" runat="server" ValidationGroup="Submit" CssClass="form-control" MaxLength="10" onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvWeight" runat="server" ControlToValidate="txtweight" Display="Dynamic" SetFocusOnError="true"   ValidationGroup="Submit"
                                    ErrorMessage="Please Enter Ch. Weight!" CssClass="classValidation"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <label class="control-label">Act. Weight<span class="required-field">*</span></label>
                                <div>
                                    <asp:TextBox ID="txtActWeight" Text="0.00" runat="server" CssClass="form-control" ValidationGroup="Submit" MaxLength="10" onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvActWeight" runat="server" ControlToValidate="txtrate" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Submit"
                                    ErrorMessage="Please Enter Act. Weight" CssClass="classValidation"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-1">
                                <label class="control-label">Rate<span class="required-field">*</span></label>
                                <div>
                                    <asp:TextBox ID="txtrate" Text="0.00" runat="server" CssClass="form-control" MaxLength="10" ValidationGroup="Submit"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtrate" runat="server" ControlToValidate="txtrate" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Submit"
                                    ErrorMessage="Enter Rate." CssClass="classValidation"></asp:RequiredFieldValidator>
                                </div>
                            </div>                                                     
                        </div>
                           <div class="col-sm-2" style="width:10%" id="DivCommission" runat="server" visible="false">
                              <label class="control-label">Commission</label>
                              <div>
                                <asp:TextBox ID="txtItmCommission" runat="server" Text="0.00" CssClass="form-control" MaxLength="10"
                                    onKeyPress="return checkfloat(event, this);" Width="102px" oncopy="return false"
                                    onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                              </div>
                            </div>
                            <div class="col-sm-1" style="width: 2%" id="DivCommissionUpdatebtn" runat="server" visible="false">
                            <label class="control-label">&nbsp;</label>
                              <div style="padding-top:30%">
                                <asp:ImageButton ID="imgComUpdate" runat="server" ForeColor="AliceBlue"   ToolTip="Update Commission in Master" 
                                    ImageUrl="~/Images/plus.gif" Width="15px" onclick="imgComUpdate_Click" />
                              </div>
                              </div>
                        <div class="clearfix odd_row">
                                <label class="col-sm-1 control-label">Enter Details</label>
                                <div class="col-sm-5">
                                <asp:TextBox ID="txtdetail" PlaceHolder="Enter Detail" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                    </div>
                                    <label class="col-sm-1 control-label">Packet No.</label>
                                <div class="col-sm-1">
                                <asp:TextBox ID="txtPacketNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                </div>  
                                <label class="col-sm-1 control-label">Row No.</label>
                                <div class="col-sm-1">
                                <asp:TextBox ID="txtRowAdd" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                </div>                                    
                                    <div class="col-sm-1">
                                        <asp:LinkButton ID="lnkbtnSubmit" runat="server" OnClick="lnkbtnSubmit_OnClick" CssClass="btn full_width_btn btn-sm btn-primary subnew"
                                            ToolTip="Click to Submit"  style="margin-top:0px;"  CausesValidation="true" ValidationGroup="Submit">Submit</asp:LinkButton>
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:LinkButton ID="lnkbtnAdd"  style="margin-top:0px;" OnClick="lnkbtnAdd_OnClick"  runat="server" CausesValidation="false" CssClass="btn full_width_btn btn-sm btn-primary subnew"
                                            ToolTip="Click to new">New</asp:LinkButton>
                                    </div>   
                        </div>
                    </div>
                    </section>
    </div>
    <div class="clearfix third_right">
        <section class="panel panel-in-default">                            
                        <div class="panel-body">     
                            <asp:GridView ID="grdMain" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="display rap dataTable"
                                    Width="100%" BorderStyle="None" BorderWidth="0" OnRowCommand="grdMain_RowCommand">
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
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignLeft" HeaderStyle-Width="20" HeaderText="Unit">
                                        <ItemStyle HorizontalAlign="Left" Width="20" />
                                        <ItemTemplate>
                                            <%#Eval("Unit_Name")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignLeft" HeaderStyle-Width="10" HeaderText="Type">
                                        <ItemStyle HorizontalAlign="Left" Width="10" />
                                        <ItemTemplate>
                                            <%#Eval("Rate_Type")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="20" HeaderText="Qty">
                                        <ItemStyle CssClass="gridHeaderAlignRight" Width="20" />
                                        <ItemTemplate>
                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Quantity")))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignLeft" HeaderStyle-Width="10" HeaderText="Dimension">
                                        <ItemStyle HorizontalAlign="Left" Width="10" />
                                        <ItemTemplate>
                                            <%#Eval("Dimension")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignLeft" HeaderStyle-Width="10" HeaderText="CFT">
                                        <ItemStyle HorizontalAlign="Left" Width="10" />
                                        <ItemTemplate>
                                            <%#Eval("CFT")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="150" HeaderText="Ch. Weight">
                                        <ItemStyle CssClass="gridHeaderAlignRight" Width="150" />
                                        <ItemTemplate>
                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Ch_Weight")))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="150" HeaderText="Act. Weight">
                                        <ItemStyle CssClass="gridHeaderAlignRight" Width="150" />
                                        <ItemTemplate>
                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Act_Weight")))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="150" HeaderText="Rate">
                                        <ItemStyle CssClass="gridHeaderAlignRight" Width="150" />
                                        <ItemTemplate>                                         
                                            <%#String.Format("{0:0,0.00}",string.IsNullOrEmpty(Convert.ToString(Eval("Item_Rate"))) ? 0 : Convert.ToDouble(Eval("Item_Rate")))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="150" HeaderText="Amount">
                                        <ItemStyle CssClass="gridHeaderAlignRight" Width="150" />
                                        <ItemTemplate>
                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Amount")))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignLeft" HeaderStyle-Width="10" HeaderText="Details">
                                        <ItemStyle HorizontalAlign="Left" Width="10" />
                                        <ItemTemplate>
                                            <%#Eval("Detail")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignLeft" HeaderStyle-Width="100" HeaderText="Packet No">
                                        <ItemStyle CssClass="gridHeaderAlignLeft" Width="100" />
                                        <ItemTemplate>
                                            <%#(Convert.ToInt64(Eval("Packet_No")) > 0) ? Convert.ToString(Convert.ToString(Eval("Packet_No"))) : ""%>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                </Columns>
                                </asp:GridView>
                        </div>
                        
                        </section>
    </div>
    <div class="clearfix" id="GrDetails" runat="server">
        <section class="panel panel-in-default">                            
                        <div class="panel-body">     
                          <div class="clearfix even_row">
                            <div class="col-sm-3">
                              <label class="col-sm-5 control-label">GrossAmnt</label>
                              <div class="col-sm-7">
                                <asp:TextBox ID="txtGrossAmnt" runat="server" CssClass="form-control"  
                                      Text="0.00" Style="text-align: right;" Enabled="False"
                                    onKeyPress="return checkfloat(event, this);" 
                                      ontextchanged="txtGrossAmnt_TextChanged"></asp:TextBox>
                              </div>
                            </div>
                           	<div class="col-sm-3">                              
                              <asp:Label ID="lblCar" class="col-sm-4 control-label" runat="server" Text=""></asp:Label>
                              <div class="col-sm-8">
                                <asp:TextBox ID="txtCartage" runat="server" CssClass="form-control" Text="0.00"  
                                      MaxLength="7" Enabled="true" AutoPostBack="true" Style="text-align: right;"
                                    onKeyPress="return checkfloat(event, this);" 
                                      ontextchanged="txtCartage_TextChanged"></asp:TextBox>
                              </div>
                            </div>
                            <div class="col-sm-3">
                              <label class="col-sm-4 control-label">Total</label>
                              <div class="col-sm-8">
                                <asp:TextBox ID="txtTotalAmnt" runat="server" CssClass="form-control"  Text="0.00"
                                    Style="text-align: right;" Enabled="False" MaxLength="10" 
                                      onKeyPress="return checkfloat(event, this);" 
                                      ontextchanged="txtTotalAmnt_TextChanged"></asp:TextBox>
                              </div>
                            </div>
                           	<div class="col-sm-3">
                              <label class="col-sm-4 control-label">Surcharge</label>
                              <div class="col-sm-8">
                                <asp:TextBox ID="txtSurchrge" runat="server" CssClass="form-control" Text="0.00" 
                                      Style="text-align: right;" Enabled="false" MaxLength="8"
                                    onKeyPress="return checkfloat(event, this);" 
                                      ontextchanged="txtSurchrge_TextChanged"></asp:TextBox>
                              </div>
                            </div>			                              
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-3">
                              <%--<label class="col-sm-5 control-label">Commission</label>--%>
                              <asp:Label ID="lblCom" class="col-sm-5 control-label" runat="server" Text=""></asp:Label>
                              <div class="col-sm-7">
                                <asp:TextBox ID="txtCommission" runat="server" CssClass="form-control"  
                                      Text="0.00" MaxLength="7" AutoPostBack="true"  Enabled="true" Style="text-align: right;"
                                    onKeyPress="return checkfloat(event, this);" 
                                      ontextchanged="txtCommission_TextChanged"></asp:TextBox>
                              </div>
                            </div>
                           	<div class="col-sm-3">                              
                               <asp:Label ID="lblBil" class="col-sm-4 control-label" runat="server" Text=""></asp:Label>
                              <div class="col-sm-8">
                                <asp:TextBox ID="txtBilty" runat="server" CssClass="form-control" Text="0.00" 
                                      MaxLength="7" Style="text-align: right;" AutoPostBack="true" Enabled="true" 
                                      onKeyPress="return checkfloat(event, this);" 
                                      ontextchanged="txtBilty_TextChanged"></asp:TextBox>
                              </div>
                            </div>
                            <div class="col-sm-3">
                            <b><asp:Label ID="lbltxtwages" class="col-sm-4 control-label" runat="server" Text="Wages"></asp:Label></b>
                              <div class="col-sm-8">
                                <asp:TextBox ID="txtWages" runat="server" CssClass="form-control"  MaxLength="7"
                                    Enabled="true" Text="0.00" Style="text-align: right;"
                                    onDrop="blur();return false;" AutoPostBack="true" onpaste="return false" 
                                      onKeyPress="return checkfloat(event, this);" 
                                      ontextchanged="txtWages_TextChanged"></asp:TextBox>
                              </div>
                            </div>
                           	<div class="col-sm-3">
                              <b><asp:Label ID="lbltxtPF" class="col-sm-4 control-label" runat="server" Text="PF"></asp:Label></b>
                              <div class="col-sm-8">
                                <asp:TextBox ID="txtPF" runat="server" CssClass="form-control"  MaxLength="7" 
                                      Text="0.00" Enabled="true" AutoPostBack="true" Style="text-align: right;" 
                                      onKeyPress="return checkfloat(event, this);" ontextchanged="txtPF_TextChanged"></asp:TextBox>
                              </div>
                            </div>			                              
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-3">
                              <b><asp:Label ID="lbltxtTolltax" class="col-sm-5 control-label" runat="server" Text="Toll Tax"></asp:Label></b>
                              <div class="col-sm-7">
                                <asp:TextBox ID="txtTollTax" runat="server" CssClass="form-control"  Text="0.00" 
                                      MaxLength="7" AutoPostBack="true" Style="text-align: right;" Enabled="true"
                                        onKeyPress="return checkfloat(event, this);" 
                                      ontextchanged="txtTollTax_TextChanged"></asp:TextBox>
                              </div>
                            </div>
                             <div class="col-sm-3">
                              <b><asp:Label ID="lblStCharges" class="col-sm-4 control-label" runat="server" Text="ST.Charges"></asp:Label></b>
                              <div class="col-sm-8">
                                <asp:TextBox ID="txtSTCharges" runat="server" CssClass="form-control"  Text="0.00" 
                                      MaxLength="7" AutoPostBack="true" Style="text-align: right;" Enabled="true"
                                        onKeyPress="return checkfloat(event, this);" 
                                      ontextchanged="txtSTCharges_TextChanged"></asp:TextBox>
                              </div>
                            </div>
                           	<div class="col-sm-3">
                              <label class="col-sm-4 control-label">Sub Total</label>
                              <div class="col-sm-8">
                                <asp:TextBox ID="txtSubTotal" runat="server" CssClass="form-control"  Text="0.00" 
                                      Style="text-align: right;" Enabled="False"
                                    onKeyPress="return checkfloat(event, this);" 
                                      ontextchanged="txtSubTotal_TextChanged"></asp:TextBox>
                              </div>
                            </div>
                            <%--Service Tax Value & Percentage--%>
                            <asp:Panel ID="pnlServiceTax" runat="server">
                            <div class="col-sm-3">
                              <label class="col-sm-4 control-label">Serv. Tax</label>
                              <div class="col-sm-4">
                                <asp:TextBox ID="txtServPer" runat="server" CssClass="form-control"  
                                      MaxLength="5" 
                                    Text="0" Style="text-align: right;"  onDrop="blur();return false;" 
                                      onKeyPress="return allowPositive(event, this);" ontextchanged="txtServPer_TextChanged" AutoPostBack="true" ></asp:TextBox>
                              </div>
                              <div class="col-sm-4">
                                <asp:TextBox ID="txtServTax" runat="server" CssClass="form-control"  
                                      MaxLength="7" Enabled="false"
                                    Text="0.00" Style="text-align: right;" onDrop="blur();return false;" 
                                      onKeyPress="return checkfloat(event, this);" 
                                      ontextchanged="txtServTax_TextChanged"></asp:TextBox>
                              </div>
                            </div>	
                            </asp:Panel>	                              
                          </div>
                          <div class="clearfix odd_row">
                            <%--Out State GST Value & Percentage--%>
                            <asp:HiddenField ID="GrRestrictDate" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hidToStateIdno" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hidToStateName" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hidSGSTPer" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hidCGSTPer" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hidIGSTPer" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hidGstType" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hidGSTCessPer" runat="server" />

                            <%--<asp:HiddenField ID="hidSGSTAmt" runat="server" />
                            <asp:HiddenField ID="hidCGSTAmt" runat="server" />
                            <asp:HiddenField ID="hidIGSTAmt" runat="server" />
                            <asp:HiddenField ID="hidGSTCessAmt" runat="server" />--%>
                          <asp:Panel ID="pnlIsStateGST" runat="server">
                              <div class="col-sm-3">
                                  <label class="col-sm-5 control-label">SGST</label>
                                  <div class="col-sm-3">
                                    <asp:TextBox ID="txtSGSTPercent" OnTextChanged="GST_TextChanged" runat="server" CssClass="form-control"  
                                          MaxLength="5" 
                                        Text="0" Style="text-align: right;"  onDrop="blur();return false;" 
                                          onKeyPress="return allowPositive(event, this);" AutoPostBack="True"></asp:TextBox>
                                  </div>
                                  <div class="col-sm-4">
                                    <asp:TextBox ID="txtSGSTAmount" runat="server" CssClass="form-control"  
                                          MaxLength="7" Enabled="false"
                                        Text="0.00" Style="text-align: right;" onDrop="blur();return false;" 
                                          onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                  </div>
                                </div>
                                <div class="col-sm-3">
                                  <label class="col-sm-4 control-label">CGST</label>
                                  <div class="col-sm-3">
                                    <asp:TextBox ID="txtCGSTPercent" OnTextChanged="GST_TextChanged" runat="server" CssClass="form-control"  
                                          MaxLength="5" 
                                        Text="0" Style="text-align: right;"  onDrop="blur();return false;" 
                                          onKeyPress="return allowPositive(event, this);" AutoPostBack="True"></asp:TextBox>
                                  </div>
                                  <div class="col-sm-5">
                                    <asp:TextBox ID="txtCGSTAMount" runat="server" CssClass="form-control"  
                                          MaxLength="7" Enabled="false"
                                        Text="0.00" Style="text-align: right;" onDrop="blur();return false;" 
                                          onKeyPress="return checkfloat(event, this);" 
                                          ontextchanged="txtServTax_TextChanged"></asp:TextBox>
                                  </div>
                                </div>
                          </asp:Panel>
                            <%--In State GST Value & Percentage--%>
                          <asp:Panel ID="pnlOutStateGST" runat="server">
                            <div class="col-sm-3">
                              <label class="col-sm-5 control-label">IGST</label>
                              <div class="col-sm-2">
                                <asp:TextBox ID="txtIGSTPercent" OnTextChanged="GST_TextChanged" runat="server" CssClass="form-control"  
                                      MaxLength="5" 
                                    Text="0" Style="text-align: right;"  onDrop="blur();return false;" 
                                      onKeyPress="return allowPositive(event, this);" AutoPostBack="True"></asp:TextBox>
                              </div>
                              <div class="col-sm-5">
                                <asp:TextBox ID="txtIGSTAmount" runat="server" CssClass="form-control"  
                                      MaxLength="7" Enabled="false"
                                    Text="0.00" Style="text-align: right;" onDrop="blur();return false;" 
                                      onKeyPress="return checkfloat(event, this);" 
                                      ontextchanged="txtServTax_TextChanged"></asp:TextBox>
                              </div>
                            </div>
                          </asp:Panel>
                            <%--Addition TAX Value & Percentage--%>
                          <asp:Panel ID="pnlAdditionTax" runat="server">
                                <div class="col-sm-3">
                                <label class="col-sm-5 control-label">K. Kalyan Tax</label>
                                  <div class="col-sm-7">
                                    <asp:TextBox ID="txtkalyan" runat="server" CssClass="form-control" MaxLength="7" Enabled="false" Text="0.00" Style="text-align: right;"
                                        onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                  </div>
                                </div>
                               <div class="col-sm-3">
                                  <label class="col-sm-4 control-label">S.  Bhrt Tax</label>
                                  <div class="col-sm-8">
                                    <asp:TextBox ID="txtSwchhBhartTx" runat="server" CssClass="form-control"  
                                          MaxLength="7" Enabled="false" 
                                        Text="0.00" Style="text-align: right;" 
                                          onKeyPress="return checkfloat(event, this);" 
                                          ontextchanged="txtSwchhBhartTx_TextChanged"></asp:TextBox>
                                  </div>
                                </div>
                            </asp:Panel>	
                            <div class="col-sm-3">
                              <label class="col-sm-4 control-label">RoundOff</label>
                              <div class="col-sm-8">
                                <asp:TextBox ID="TxtRoundOff" runat="server" CssClass="form-control" MaxLength="7"
                                    Enabled="false" Text="0.00" Style="text-align: right;" onDrop="blur();return false;" onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                              </div>
                            </div>
                           	<div class="col-sm-3">
                              <label class="col-sm-4 control-label">Net Amnt</label>
                              <div class="col-sm-8">
                                <asp:TextBox ID="txtNetAmnt" runat="server" CssClass="form-control" MaxLength="7" 
                                      Enabled="false" Text="0.00" Style="text-align: right;"
                                        onKeyPress="return checkfloat(event, this);" 
                                      ontextchanged="txtNetAmnt_TextChanged"></asp:TextBox>
                              </div>
                            </div>			                              
                          </div>
                        </div>
                      </section>
    </div>
    <div class="clearfix fourth_right">
        <section class="panel panel-in-default btns_without_border">                            
                        <div class="panel-body">     
                          <div class="clearfix odd_row">
                            <div class="col-lg-2"></div>
                            <div class="col-lg-8">
                            <div class="col-sm-4">                                                         
                            <asp:LinkButton ID="lnkbtnNew" runat="server" Visible="false" CausesValidation="false"  
                                    CssClass="btn full_width_btn btn-s-md btn-info" onclick="lnkbtnNew_Click1"><i class="fa fa-file-o"></i>New</asp:LinkButton>                                                            	
					        </div>                                  
					        <div class="col-sm-4">
                            <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" 
                                    ValidationGroup="save" CssClass="btn full_width_btn btn-s-md btn-success" 
                                    onclick="lnkbtnSave_Click"><i class="fa fa-save"></i>Save</asp:LinkButton>                      
					        </div>
					        <div class="col-sm-4">
                            <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" 
                                    CssClass="btn full_width_btn btn-s-md btn-danger" onclick="lnkbtnCancel_Click" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
					        </div>
                            </div>
                            <div class="col-lg-2">
                            <div class="col-sm-1 pull-right">
                                <div title="User Preference setting" class="btn-setting"></div>
                            </div>
                            </div>
                          </div> 
                        </div>
                      </section>
    </div>
    </form> </div> </section>
    </div>
    <div id="other_Details" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="popform_header">
                        Other Details</h4>
                </div>
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
                        <div class="panel-body">
                            <div class="clearfix odd_row">
                                <div class="col-sm-6">
                                    <div class="col-sm-4">
                                        <label class="col-sm-12 control-label">EGP No.</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="TxtEGPNo" onkeypress="return allowAlphabetAndNumer(event);" runat="server" PlaceHolder="Enter EGP Number" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                    </div>
                                    <div id="UpdateEGPNo" runat="server" class="col-sm-1">
                                        <asp:LinkButton ID="lnkbtnEgpNoUpdate" runat="server" ToolTip="Update EGP No." class="btn-sm btn btn-primary acc_home"><i class="fa fa-check"></i></asp:LinkButton>
                                  </div>
                                </div>
                                 <div class="col-sm-6">
                                    <div class="col-sm-4">
                                        <label class="col-sm-12 control-label">Order No.</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtOrderNo" onkeypress="return allowAlphabetAndNumer(event);" runat="server" CssClass="form-control" PlaceHolder="Enter Order No" MaxLength="50" Enabled="true"></asp:TextBox>
                                    </div>
                                    <div id="UpdateOrdrNo" runat="server" class="col-sm-1">
                                        <asp:LinkButton ID="lnkbtnOrdrNoUpdate" runat="server" ToolTip="Update Order No." class="btn-sm btn btn-primary acc_home"><i class="fa fa-check"></i></asp:LinkButton>
                                    </div>
                                </div>                                
                            </div>
                              <div class="clearfix even_row">
                               <div class="col-sm-6">
                                    <div class="col-sm-4">
                                        <b><asp:Label ID="lblrefrename" class="col-sm-12 control-label" runat="server" Text="Ref. No."></asp:Label></b>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtRefNo" onkeypress="return allowAlphabetAndNumer(event);" runat="server" CssClass="form-control" PlaceHolder="Enter Ref No" MaxLength="100" Enabled="true"></asp:TextBox>
                                    </div>     
                                    <div id="UpdateInvNo" runat="server" class="col-sm-1">
                                        <asp:LinkButton ID="lnkbtnInvNoUpdate" runat="server" ToolTip="Update Ref. No" class="btn-sm btn btn-primary acc_home"><i class="fa fa-check"></i></asp:LinkButton>
                                    </div>                                
                                </div>
                                <div class="col-sm-6">
                                    <div class="col-sm-4">
                                        <b><asp:Label ID="lblRefDate" class="col-sm-12 control-label" runat="server" Text="Ref. Date"></asp:Label></b>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtRefDate" onkeypress="return notAllowAnything(event);" runat="server" PlaceHolder="DD-MM-YYYY" CssClass="input-sm datepicker form-control" Enabled="true"></asp:TextBox>
                                    </div>                                                                       
                                </div>                                
                            </div>
                            <div class="clearfix odd_row">
                            <div class="col-sm-6">
                                    <div class="col-sm-4">
                                        <label class="col-sm-12 control-label">Form No.</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtFromNo" onkeypress="return allowAlphabetAndNumer(event);" runat="server" CssClass="form-control" PlaceHolder="Enter Form No" MaxLength="50" Enabled="true"></asp:TextBox>
                                    </div>
                                    <div id="UpdateFormNo" runat="server" class="col-sm-1">
                                    <asp:LinkButton ID="lnkbtnFormNoUpdate" runat="server" ToolTip="Update Form No." class="btn-sm btn btn-primary acc_home"><i class="fa fa-check"></i></asp:LinkButton>
                                  </div>
                                </div>
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
    <div id="type_popup" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="popform_header">
                        Receipt Details</h4>
                </div>
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
                        <div class="panel-body">
                            <div class="clearfix even_row">
                                <div class="col-sm-12">
                                    <label class="col-sm-6 control-label">Enter Instrument Number </label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtInstNo" runat="server" CssClass="form-control" MaxLength="6" 
                                            Placeholder="Instrument Number"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvinstno" runat="server" ControlToValidate="txtInstNo"
                                            CssClass="classValidation" Display="Dynamic" ErrorMessage="Enter Inst. No." SetFocusOnError="true"
                                            ValidationGroup="save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix odd_row">
                                <div class="col-sm-12">
                                    <label class="col-sm-6 control-label">Enter Instrument Date</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtInstDate" runat="server" CssClass="input-sm datepicker form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvinstdate" runat="server" ControlToValidate="txtInstDate"
                                            CssClass="classValidation" Display="Dynamic" ErrorMessage="Enter Inst. Date"
                                            SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix even_row">
                                <div class="col-sm-12">
                                    <label class="col-sm-6 control-label">
                                        Enter Customer Bank Name</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlcustbank" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlcustbank" runat="server" ControlToValidate="ddlcustbank"
                                            CssClass="classValidation" Display="Dynamic" ErrorMessage="Select Cust. Bank"
                                            InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            
                        </div>
                    </section>
                </div>
                <div class="modal-footer">
                    <div class="popup_footer_btn">
                        <button type="submit" class="btn btn-dark" data-dismiss="modal">
                            <i class="fa fa-check"></i>OK</button>
                        <button type="submit" class="btn btn-dark" data-dismiss="modal">
                            <i class="fa fa-times"></i>Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="dvContainerdetails" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="popform_header">
                        Container Detail
                    </h4>
                </div>
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
                        <div class="panel-body">
                            <!-- First Row start -->
                            <div class="clearfix odd_row">
                                <div class="col-sm-6">
                                    <label class="col-sm-5 control-label">
                                        Container No 1</label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txtContainrNo" PlaceHolder="Container Number" runat="server" CssClass="form-control"
                                            MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <label class="col-sm-4 control-label">
                                        Seal No 1</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtContainerSealNo" PlaceHolder="Container Seal No" runat="server"
                                            CssClass="form-control" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix even_row">
                                <div class="col-sm-6">
                                    <label class="col-sm-5 control-label">
                                        Container No 2</label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txtContainrNo2" PlaceHolder="Container Number" runat="server" CssClass="form-control"
                                            MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <label class="col-sm-4 control-label">
                                        Seal No 2</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtContainerSealNo2" PlaceHolder="Container Seal No" runat="server"
                                            CssClass="form-control" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix odd_row">
                                <div class="col-sm-6">
                                    <label class="col-sm-5 control-label">
                                        Type</label>
                                    <div class="col-sm-7">
                                        <asp:DropDownList ID="ddlContainerType" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <label class="col-sm-4 control-label">
                                        Size</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlContainerSize" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix even_row">
                                <div class="col-sm-6">
                                    <label class="col-sm-5 control-label">
                                        Port</label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txtPortNum" PlaceHolder="Port" runat="server" CssClass="form-control"
                                            MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <label class="col-sm-5 control-label">
                                        Type</label>
                                    <div class="col-sm-7">
                                        <asp:DropDownList ID="ddlTypeI" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Import" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Export" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="clearfix odd_row">
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblTypeI" runat="server" Text="Select" CssClass="col-sm-5"></asp:Label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtNameI" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="popup_footer_btn">
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                     <asp:HiddenField ID="hidTBBType" runat="server" />
                                <asp:HiddenField ID="HiddSurchgPer" runat="server" />
                                <asp:HiddenField ID="HiddWagsAmnt" runat="server" />
                                <asp:HiddenField ID="HiddBiltyAmnt" runat="server" />
                                <asp:HiddenField ID="HiddTolltax" runat="server" />
                                <asp:HiddenField ID="HiddServTaxValid" runat="server" />
                                <asp:HiddenField ID="Hiditruckcitywise" runat="server" />
                                <asp:HiddenField ID="HidiFromCity" runat="server" />
                                <asp:HiddenField ID="HidsRenWages" runat="server" />
                                <asp:HiddenField ID="HidsRenCartage" runat="server" />
                                <asp:HiddenField ID="HidRenCommission" runat="server" />
                                <asp:HiddenField ID="HidRenBilty" runat="server" />
                                <asp:HiddenField ID="HidStcharge" runat="server" />
                                <asp:HiddenField ID="HiddServTaxPer" runat="server" />
                                <asp:HiddenField ID="HiddSwachhBrtTaxPer" runat="server" />
                                <asp:HiddenField ID="HiddKalyanTaxPer" runat="server" />
                                <asp:HiddenField ID="HiddKalyanTax" runat="server" />
                                <asp:HiddenField ID="HiddTruckIdno" runat="server" />
                                <asp:HiddenField ID="HiddConSize" runat="server" />
                                <asp:HiddenField ID="HiddUserPrefCont" runat="server" />
                                <asp:HiddenField ID="hidAdvOrdrQty" runat="server" />
                                <asp:HiddenField ID="hidAdvOrdrWght" runat="server" />
                                <asp:HiddenField ID="hidRenamePF" runat="server" />
                                <asp:HiddenField ID="hidrefrename" runat="server" />
                                <asp:HiddenField ID="hidRenameToll" runat="server" />
                                    <asp:LinkButton ID="lnkbtnContainerSubmit" runat="server" CssClass="btn btn-dark"><i class="fa fa-check"></i>Ok</asp:LinkButton>
                                    &nbsp;&nbsp
                                    <button type="submit" class="btn btn-dark" data-dismiss="modal">
                                        <i class="fa fa-times"></i>Close</button>
                                </div>
                            </div>
                        </div>
                    </section>
                </div>
            </div>
        </div>
    </div>
    <div id="gr_details_form" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="popform_header">
                        <asp:Label ID="lblGrDetails" runat="server">Select Order Detail</asp:Label>
                    </h4>
                </div>
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
                        <div class="panel-body">
                            <!-- First Row start -->
                            <div class="clearfix odd_row">
                                <div class="col-sm-4">
                                    <label class="col-sm-5 control-label">
                                        Date From</label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txtDateFromDiv" runat="server" CssClass="input-sm datepicker form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtFrm" runat="server" ErrorMessage="Enter From Date"
                                            Display="Dynamic" CssClass="classValidation" ControlToValidate="txtDateFromDiv"
                                            ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <label class="col-sm-5 control-label">
                                        Date To</label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txtDateToDiv" runat="server" CssClass="input-sm datepicker form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtTo" runat="server" ErrorMessage="Enter To Date"
                                            Display="Dynamic" CssClass="classValidation" ControlToValidate="txtDateToDiv"
                                            ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <label id="lblSender" runat="server" class="col-sm-4 control-label">
                                        Receiver<span class="required-field">*</span></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlRecvrDiv" runat="server" CssClass="form-control" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>
                                    <asp:RequiredFieldValidator ID="rfvddlRecvrDiv" runat="server" ErrorMessage="Select Receiver name"
                                        Display="Dynamic" InitialValue="0" CssClass="classValidation" ControlToValidate="ddlRecvrDiv"
                                        ValidationGroup="GRDetailsSrch"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix even_row">
                                <div class="col-sm-4">
                                    <label class="col-sm-5 control-label">
                                        To City<span class="required-field">*</span></label>
                                    <div class="col-sm-7">
                                        <asp:DropDownList ID="ddltocityDiv" runat="server" CssClass="form-control" AutoPostBack="false">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddltocityDiv" runat="server" ErrorMessage="Select To City"
                                            Display="Dynamic" InitialValue="0" CssClass="classValidation" ControlToValidate="ddltocityDiv"
                                            ValidationGroup="GRDetailsSrch"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <label class="col-sm-5 control-label">
                                        Deliv.Place<span class="required-field">*</span></label>
                                    <div class="col-sm-7">
                                        <asp:DropDownList ID="ddldelvplaceDIv" runat="server" CssClass="form-control" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-9">
                                        <asp:RequiredFieldValidator ID="rfvddldelvplaceDIv" runat="server" ErrorMessage="Select Delivery Place"
                                            Display="Dynamic" InitialValue="0" CssClass="classValidation" ControlToValidate="ddldelvplaceDIv"
                                            ValidationGroup="GRDetailsSrch"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-4" style="padding: 0;">
                                    <div class="col-sm-6 prev_fetch">
                                        <asp:LinkButton ID="lnkbtnSearch" class="btn full_width_btn btn-sm btn-primary" CausesValidation="true"
                                            ValidationGroup="GRDetailsSrch" runat="server">Search</asp:LinkButton>
                                    </div>
                                    <div class="col-sm-6">
                                    </div>
                                </div>
                            </div>
                            <%--<asp:GridView ID="grdGrdetals" runat="server" GridLines="None" AutoGenerateColumns="false"
                                CssClass="display nowrap dataTable" Width="100%" BorderStyle="None" BorderWidth="0"
                                OnRowDataBound="grdGrdetals_RowDataBound" OnRowEditing="grdGrdetals_RowEditing">
                                <RowStyle CssClass="odd" />
                                <AlternatingRowStyle CssClass="even" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Select" HeaderStyle-Width="40px">
                                        <HeaderStyle Width="40" CssClass="gridHeaderAlignCenter" />
                                        <ItemStyle Width="40" CssClass="gridHeaderAlignCenter" />
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAllCheckboxes(this);"
                                                CssClass="SACatA"/>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkId" runat="server"  CssClass="gridHeaderAlignCenter" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rcpt No." HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("Rcpt_No"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ref.No." HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("Ref_No"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Convert.ToDateTime(Eval("Rcpt_Date")).ToString("dd-MMM-yyyy")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To City" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%#Eval("ToCity")%>
                                            <asp:HiddenField ID="hidGrIdno" runat="server" Value='<%#Eval("RcptGoodHead_Idno")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sender No." HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="180px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Eval("Sender_No")%>
                                            <asp:Label ID="lblSenderNo" runat="server" Value='<%#Eval("Sender_No")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Agent Name" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="180px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Eval("Agnt_Name")%>
                                            <asp:Label ID="lblAgentname" runat="server" Value='<%#Eval("Agnt_Name")%>' />
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
                            </asp:GridView>--%>
                        </div>
                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                        <asp:Label ID="lblmsg2" runat="server" Text="Message - Please select only one GR at a time."
                            Visible="false"></asp:Label>
                        <asp:Label ID="lblmsg" runat="server" Text="Message - Please select only one GR at a time."
                            Visible="false" CssClass="redfont"></asp:Label>
                    </section>
                </div>
                <div class="modal-footer">
                    <div class="popup_footer_btn">
                        <asp:LinkButton ID="lnkbtndivsubmit" class="btn btn-dark" runat="server">Submit</asp:LinkButton>
                        <button type="submit" class="btn btn-dark" data-dismiss="modal">
                            <i class="fa fa-times"></i>Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="Amount" class="modal fade">
        <div class="modal-dialog" style="width: 25%">
            <div class="modal-header">
                <h4 class="popform_header">
                    Print&nbsp;&nbsp;&nbsp;&nbsp;</h4>
            </div>
            <div class="modal-content">
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
								        <div class="panel-body">  
                                        <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlPage" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="4 Pages" Value="4" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="3 Pages" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="2 Pages" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="1 Page" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                       </div>
                                       <div class="col-sm-9">
                                        <asp:LinkButton ID="lnkwithamount" Text="With Amount" 
                                                class="btn btn-sm btn-primary" runat="server" TabIndex="45" onclick="lnkwithamount_Click"  
                                                ></asp:LinkButton>
                                       
                                        <asp:LinkButton ID="lnkwithoutamount" Text="Without Amount" 
                                                class="btn btn-sm btn-primary" runat="server"  TabIndex="45" onclick="lnkwithoutamount_Click" 
                                                ></asp:LinkButton>
                                    
                                        </div>
                                       </div>
                                        </section>
                </div>
            </div>
        </div>
    </div>
     <div class="col-lg-0" style="display:none">
            <tr style="display: block">
                <td class="white_bg" align="center">
                    <div id="print" style="font-size: 13px;">
                        <table cellpadding="1" cellspacing="0" width="1050" border="1" style="font-family: Arial,Helvetica,sans-serif;">
                            <tr style="height:80px; width:20%">
                                <td align="center" class="white_bg" valign="top" colspan="3" style="font-size: 14px;
                                    border-left-style: none; border-right-style: none">
                                   <%--  <div style="text-align:left;Width:140px; float:left;">
                                        
                                     </div>--%>
                                             <div style="float:left; text-align:left; width:25%; font-size:12px;">
                                             <asp:Label ID="lblTin" runat="server" Text="Reg. No. :"></asp:Label>&nbsp;<asp:Label
                                                ID="lblCompTIN" runat="server"></asp:Label><br />
                                            <asp:Label ID="lbltxtPanNo" runat="server" Text="PAN NO. :"></asp:Label>                                            
                                            <asp:Label ID="lblPanNo" runat="server"></asp:Label>
                                                </div>                                            
                                             <div style="width:50%;float:left">
                                                 <asp:Image ID="imgLogoShow"  runat="server" ImageUrl="~/img/OMLOGO.png" 
                                                     Height="37px" Width="230px" ></asp:Image><br />
                                            <asp:Label ID="lblCompDesc" runat="server" Style="font-size: 12px;"></asp:Label>
                                            <asp:Image ID="imgline"  runat="server" ImageUrl="~/img/line.jpg" 
                                                     Height="2px" Width="230px"  ></asp:Image>
                                                     <br />
                                            <asp:Label ID="lblCompAdd1" runat="server" Style="font-size: 12px;"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblCompAdd2" runat="server" Style="font-size: 12px;"></asp:Label>
                                            <asp:Label ID="lblCompCity" runat="server" Style="font-size: 12px;"></asp:Label>&nbsp;&nbsp;
                                            <asp:Label ID="lblCompState" runat="server" Style="font-size: 12px;"></asp:Label>&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblCompCityPin" runat="server" Style="font-size: 12px;"></asp:Label><br />
                                            <asp:Label ID="lblCompPhNo" runat="server" Style="font-size: 12px;"></asp:Label>&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblCompFaxNo" runat="server" Style="font-size: 12px;"></asp:Label><br />
                                            </div>
                                        <div style="float:left; text-align:right; width:25%; font-size:12px;">
                                        <asp:Label ID="lblCompGSTINNo" runat="server" Text="GSTIN No. :"></asp:Label>&nbsp;<asp:Label
                                                ID="lblCompGSTINNoValue" runat="server"></asp:Label>
                                        </div>
                                </td>
                            </tr>                            
                        </table>
                        <table cellpadding="1" cellspacing="0" width="1050" border="1" style="font-family:Arial,Helvetica,sans-serif;">
                        <tr>
                                <td colspan="6">
                                    <table border="1" width="100%">
                                    <tr style="border-bottom:solid 1px #484848; font-size:12px;">
                                    <td style="border-right:solid 1px #484848; width:25%;">
                                    <b><asp:Label ID="lblHAtOwn" Text="AT OWNER'S RISK" runat="server"></asp:Label></b>
                                    </td>
                                    <td style="border-right:solid 1px #484848; width:10%;">
                                    <b><asp:Label ID="lblHMod" Text="MODVAT COPY" runat="server"></asp:Label></b>
                                    </td>
                                     <td style="border-right:solid 1px #484848; width:12%;">
                                    <b><asp:Label ID="lblHCNo" Text="CN. No." runat="server"></asp:Label></b>
                                    <asp:Label ID="lblGRno" style="font-size:20px;" runat="server"></asp:Label>
                                    </td>
                                     <td style="border-right:solid 1px #484848; width:13%;">
                                    <b><asp:Label ID="lblBooking" Text="BOOKING MODE" runat="server"></asp:Label></b>
                                    </td>                                     
                                     <td style="border-right:solid 1px #484848; width:10%;">
                                    <b><asp:Label ID="lblHDate" Text="DATE" runat="server"></asp:Label></b>
                                    </td>
                                     <td style="width:10%;">
                                    <b><asp:Label ID="lblHTime" Text="TIME" runat="server"></asp:Label></b>
                                    </td>
                                     <td style="border-right:solid 1px #484848;width:20%;">
                                    <b><asp:Label ID="lblhDlyIns" Text="DLY. INSTRUCTIONS" runat="server"></asp:Label></b>
                                    </td>                                                                       
                                    </tr>
                                    <tr>
                                        <td align="left" class="white_bg" valign="top"  style="font-size: 12px;
                                     border-right:solid 1px #484848; width:25%;border-bottom:1px solid #484848;">
                                          <asp:Label ID="lblOWNER" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" valign="top"  style="font-size: 12px;
                                     border-right:solid 1px #484848; width:10%;border-bottom:1px solid #484848;">
                                          <asp:Label ID="lblModvatcpy" runat="server"></asp:Label>
                                        </td>
                                         <td align="left" class="white_bg" valign="top"  style="font-size: 12px;
                                            border-right:solid 1px #484848; width:10%;border-bottom:1px solid #484848;"> 
                                            <b><asp:Label ID="Label12" Text="Mn. No." runat="server"></asp:Label></b>
                                    <asp:Label ID="lblMNo" style="font-size:20px;" runat="server"></asp:Label>                                        
                                        </td>
                                        <td align="left" class="white_bg" valign="top"  style="font-size: 12px;
                                    border-right:solid 1px #484848; width:15%;border-bottom:1px solid #484848;">
                                          <asp:Label ID="lblTranTypeP" runat="server"></asp:Label>
                                        </td>                                       
                                        <td align="left" class="white_bg" valign="top"  style="font-size: 12px;
                                    border-right:solid 1px #484848; width:10%;border-bottom:1px solid #484848;">
                                         <asp:Label ID="lblGrDate" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" valign="top"  style="font-size: 12px;
                                    width:10%;border-bottom:1px solid #484848;">
                                          <asp:Label ID="lblGRTime" runat="server"></asp:Label>
                                        </td>
                                         <td align="left" class="white_bg" valign="top"  style="font-size: 12px;
                                     border-right:solid 1px #484848; width:20%;border-bottom:1px solid #484848;">
                                          <asp:Label ID="lblDlyIns" runat="server"></asp:Label>
                                        </td>                                    
                                    </tr>                 
                                    </table>
                                </td>
                            </tr>
                        </table>
                         <table cellpadding="1" cellspacing="0" width="1050" border="1" style="font-family:Arial,Helvetica,sans-serif;">
                          <tr>
                            <td style="width:38%;" valign="top">
                            <table border="0" width="100%" class="white_bg">
                            <tr>
                            <td> 
                            <strong>Consignor:</strong>
                            </td> 
                            <td style="font-size: 13px;"colspan="6">
                            <asp:Label ID="lblConsignorName" runat="server"></asp:Label>                            
                            </td>
                            </tr>                            
                            <tr>
                            <td>
                            &nbsp;
                            </td>
                            <td style="font-size: 13px;" colspan="4">
                            <asp:Label ID="lblConsignorAddress" runat="server"></asp:Label>
                            </td>
                            </tr>
                             <tr>
                            <td>
                            &nbsp;
                            </td>
                            </tr>
                            <tr id="trCS" runat="server">
                            <td style="font-size: 13px; border-right-style: none;width:25%;">
                                 <asp:Label ID="lblCST" runat="server">C.S.T. No.:</asp:Label>
                            </td>
                            <td style="font-size: 13px; border-right-style: none">
                               <asp:Label ID="lblCSTP" runat="server"></asp:Label>  
                            </td>
                            <td colspan="2">                            
                            </td>
                           <td style="font-size: 13px; border-right-style: none;width:25%;">
                                 <asp:Label ID="lblPH" runat="server">Ph.:</asp:Label>
                            </td>
                            <td style="font-size: 13px; border-right-style: none">
                               <asp:Label ID="lblPhP" runat="server"></asp:Label>
                            </td>
                            </tr>  
                            <tr id="trLS" runat="server">
                            <td style="font-size: 13px; border-right-style: none;width:25%;">
                                 <asp:Label ID="lblLST" runat="server">L.S.T. No.:</asp:Label>
                            </td>
                            <td style="font-size: 13px; border-right-style: none">
                               <asp:Label ID="lblLSTP" runat="server"></asp:Label>
                            </td>
                            </tr>  
                            <tr>
                                <td style="font-size: 13px; border-right-style: none;width:25%;"><asp:Label ID="lblConsignerGSTINLabel" runat="server">GSTIN No.:</asp:Label></td>
                                <td style="font-size: 13px; border-right-style: none;width:25%;"><asp:Label ID="lblConsignerGSTINValue" runat="server"></asp:Label></td>
                            </tr>
                            <tr id="trConsigneeName" runat="server">
                            <td colspan="2" style="font-size: 13px; border:1px solid #000000;width:25%;">
                                 <asp:Label ID="Label1" runat="server">From:</asp:Label><asp:Label ID="lblFromCity" style="float:right;" runat="server"></asp:Label>
                            </td>                           
                            </tr>                                                
                            </table>
                            <table border="1" width="100%" class="white_bg" >
                            <tr style="font-size:14px;text-align:center;">
                            <td colspan="2"> 
                            <strong>CONSIGNMENT DETAILS</strong>
                            </td> 
                            </tr>                            
                            </table>
                            <table border="1" width="100%" class="white_bg" style="border-bottom-style:none;" >                            
                            <tr style="font-size:12px;text-align:center;">
                            <td style="width:30%">
                            <strong>No. of Pkgs.</strong>
                            </td>
                            <td style="width:20%">
                            <strong>Type Of Packing</strong>
                            </td>
                            <td colspan="2">
                            <strong>Item Desciption</strong>
                            </td>
                            </tr>   
                            <tr style="font-size:12px;text-align:center;">
                            <td style="width:30%">
                            <strong>Figures</strong>
                            </td>
                            <td style="width:20%;border-bottom-style:none;">
                                <asp:Label ID="lblUnitP" runat="server" ></asp:Label>
                            </td>
                            <td colspan="2" style="border-bottom-style:none;">
                            <asp:Label ID="lblDetailP" runat="server"></asp:Label>
                            </td>
                            </tr> 
                            <tr style="font-size:12px;text-align:center;">
                            <td style="width:30%">                            
                             <asp:Label ID="lblNoPckg" runat="server" ></asp:Label>
                            <br />                                                                                                         
                            </td>
                            <td style="width:20%;border-top-style:none;">
                            </td>
                             <td colspan="2" style="border-top-style:none;">
                            </td>
                            </tr>  
                            <tr style="font-size:12px;">
                            <td style="width:30%;text-align:center;">
                            <strong>In words</strong>
                            </td>                            
                            <td  style="text-align:left; border-right-style:none;">
                            <strong>Invoice No.(s)</strong>
                            </td>
                            <td colspan="2" style="text-align:right;border-left-style:none;">
                            <asp:Label style="text-align:right;" ID="lblRefNoP" runat="server" ></asp:Label>
                            </td>
                            </tr> 
                            <tr style="font-size:12px;">
                            <td style="width:30%;border-bottom-style:none;border-top-style:none;">                            
                            </td>                            
                            <td style="text-align:left;">
                            <strong>Date</strong>
                            </td>
                            <td colspan="2" style="text-align:right;border-left-style:none;">
                            <asp:Label style="text-align:right;" ID="lblRefDateP" runat="server" ></asp:Label>
                            </td>
                            </tr> 
                            <tr style="font-size:12px;">
                            <td style="width:30%;border-top-style:none;">                            
                            </td>                            
                            <td  colspan="3">
                            <strong style="text-align:left;">Total Invoice Value</strong><asp:Label style="float:right;" ID="lblTotP" runat="server" ></asp:Label>
                            </td>                           
                            </tr> 
                            <tr style="font-size:12px;">
                            <td style="width:30%;text-align:center;">
                            <strong>Diminution</strong>
                            </td>                            
                            <td colspan="3" style="text-align:left;border-right-style:none;">
                            <strong>Part No.</strong>
                            </td>
                            </tr>  
                            <tr style="font-size:12px;">
                            <td style="width:30%;border-bottom-style:none;border-top-style:none; text-align:center;">  
                            <asp:Label ID="lblDimP" runat="server" ></asp:Label>                          
                            </td>                            
                            <td  style="text-align:left;border-right-style:none;">
                            <strong>Quantity</strong>
                            </td>
                            <td colspan="2" style="text-align:right;border-left-style:none;">
                            <asp:Label ID="lblQtP" runat="server" ></asp:Label>
                            </td>
                            </tr> 
                             <tr style="font-size:11px;">
                            <td style="width:30%;text-align:center;">
                            <strong>Total CFT</strong>
                            </td>                            
                            <td style="width:10%;text-align:center;">
                            <strong>Per CFT<br />(Kgs.)</strong>
                            </td>
                            <td style="width:20%;text-align:center;">
                            <strong>Actual Weight<br />(Kgs.)</strong>
                            </td> 
                            <td style="width:40%;text-align:center;">
                            <strong>Charged Weight(Kgs.)</strong>
                            </td> 
                            </tr> 
                            <tr style="font-size:12px; height:30px;" >
                            <td style="width:30%;text-align:center; border-bottom-style:none; " >
                            <asp:Label ID="lblTCFT" runat="server" ></asp:Label>                             
                            </td>                            
                            <td style="width:10%;text-align:center; border-bottom-style:none; ">
                            <asp:Label ID="lblPCFT" runat="server" ></asp:Label>                           
                            </td>
                            <td style="width:30%;text-align:center; border-bottom-style:none; ">
                            <asp:Label ID="lblActWght" runat="server" ></asp:Label>                            
                            </td> 
                            <td style="width:30%;text-align:center; border-bottom-style:none; " >
                            <asp:Label ID="lblChWght" runat="server" ></asp:Label>                                                    
                            </td> 
                            </tr>                                                        
                            </table>
                            </td>
                            <td style="width:38%;" valign="top">
                            <table border="0" width="100%" class="white_bg"> 
                              <tr>
                            <td> 
                            <strong>Consignee:</strong>
                            </td>
                            <td style="font-size: 13px;"colspan="6">
                            <asp:Label ID="lblConsigeeName" runat="server"></asp:Label> 
                            </td> 
                            </tr>
                             <tr>
                               <td>
                            &nbsp;
                            </td>
                          <td  style="font-size: 13px;" colspan="4">
                            <asp:Label ID="lblConsigneeAddress" runat="server"></asp:Label>
                            </td>
                            </tr>
                            <tr>
                            <td>
                            &nbsp;
                            </td>
                            </tr>                            
                            <tr id="trCS1" runat="server">
                            <td style="font-size: 13px; border-right-style: none;width:25%;">
                                 <asp:Label ID="lblCST1" runat="server">C.S.T. No.:</asp:Label>
                            </td>
                            <td style="font-size: 13px; border-right-style: none">
                               <asp:Label ID="lblCSTP1" runat="server"></asp:Label>
                            </td>
                            <td colspan="2">                            
                            </td>
                           <td style="font-size: 13px; border-right-style: none;width:25%;">
                                 <asp:Label ID="lblPH1" runat="server">Ph.:</asp:Label>
                            </td>
                            <td style="font-size: 13px; border-right-style: none">
                               <asp:Label ID="lblPhP1" runat="server"></asp:Label>
                            </td>
                            </tr>  
                            <tr id="trLS1" runat="server">
                            <td style="font-size: 13px; border-right-style: none;width:25%;">
                                 <asp:Label ID="lblLST1" runat="server">L.S.T. No.:</asp:Label>
                            </td>
                            <td colspan="2" style="font-size: 13px; border-right-style: none">
                               <asp:Label ID="lblLSTP1" runat="server"></asp:Label>
                            </td>
                            </tr>
                            <tr>
                                <td style="font-size: 13px; border-right-style: none;width:25%;"><asp:Label ID="lblConsigneeGSTINLabel" runat="server">GSTIN No.:</asp:Label></td>
                                <td style="font-size: 13px; border-right-style: none;width:25%;"><asp:Label ID="lblConsigneeGSTINValue" runat="server"></asp:Label></td>
                            </tr>
                             <tr id="trConsignorName" runat="server">
                            <td colspan="2" style="font-size: 13px; border:1px solid #000000;width:25%;"> 
                            <asp:Label ID="Label3" runat="server">To:</asp:Label><asp:Label ID="lblToCity" style="float:right;" runat="server"></asp:Label>
                            </td>                            
                            </tr>
                            </table>
                             <table border="1" width="100%" class="white_bg">
                            <tr style="font-size:14px;text-align:center;">
                            <td colspan="2"> 
                            <strong>PAYMENT TERMS</strong>
                            </td> 
                            </tr>
                            <tr style="font-size:12px;text-align:center;">
                            <td>
                            <strong>Freight Mode</strong>
                            </td>
                            <td>
                            <strong>Billing Station</strong>
                            </td>
                            </tr>
                            <tr style="font-size:12px;text-align:left;">
                            <td style="border-bottom-style:none;border-left-style:none;border-right-style:none;border-top-style:none;">
                            To be Billed
                            </td>
                            <td style="border-bottom-style:none;border-left-style:none;border-right-style:none;border-top-style:none;">
                                <asp:CheckBox ID="chkToBeld" runat="server" />
                            </td>
                            </tr>
                            <tr style="font-size:12px;text-align:left;">
                            <td style="border-bottom-style:none;border-left-style:none;border-right-style:none;border-top-style:none;">
                            To Pay
                            </td>
                            <td style="border-bottom-style:none;border-left-style:none;border-right-style:none;border-top-style:none;">
                            <asp:CheckBox ID="chkToPay" runat="server" />
                            </td>
                            </tr>
                            <tr style="font-size:12px;text-align:left;">
                            <td style="border-bottom-style:none;border-left-style:none;border-right-style:none;border-top-style:none;">
                            Paid
                            </td>
                            <td style="border-bottom-style:none;border-left-style:none;border-right-style:none;border-top-style:none;">
                            <asp:CheckBox ID="chkPaid" runat="server" />
                            </td>
                            </tr>
                            <tr style="font-size:12px;text-align:left;">
                            <td style="border-bottom-style:none;border-left-style:none;border-right-style:none;border-top-style:none;">
                                <asp:Label ID="Label11" runat="server" Text="Label">If paid by Cash/Cheque specify amount</asp:Label>
                                </td>
                            </tr>
                            <tr style="font-size:12px;text-align:left;">
                            <td style="border-bottom-style:none;border-left-style:none;border-right-style:none;border-top-style:none;">
                                Cheque No/Cash :<asp:Label ID="lblCheP" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr style="font-size:12px;text-align:left;">
                            <td style="border-bottom-style:none;border-left-style:none;border-right-style:none;border-top-style:none;">
                                Date :<asp:Label ID="lblInsDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            </table>
                            <table border="0" width="100%" class="white_bg">
                            <tr style="font-size:14px;text-align:center;">
                            <td colspan="2"> 
                            <strong>Consignment Acknowledgment by Consignee<br />Received the shipment as per details contained hee in.</strong>
                            </td> 
                            </tr>                            
                             <tr>
                            <td>
                            <br />                                                       
                            </td>                            
                            </tr>  
                              <tr>
                            <td>
                            <br />                                                       
                            </td>                            
                            </tr>                                                 
                            <tr style="font-size:12px;text-align:center;">
                            <td valign="baseline" style="border-bottom-style:none;border-left-style:none;border-right-style:none;border-top-style:none;">
                                <asp:Label ID="Label22" runat="server" Text="Label">Signature   _________________</asp:Label>
                                </td>
                            </tr>
                            <tr  style="font-size:12px;text-align:center;">
                            <td valign="baseline" style="border-bottom-style:none;border-left-style:none;border-right-style:none;border-top-style:none;">
                                <asp:Label ID="Label20" runat="server" Text="Label">Seal of the Company with date</asp:Label>
                                </td>
                            </tr>
                            </table>
                            </td>
                            <td style="width:24%;"  valign="top">
                            <table border="1" width="100%" class="white_bg">
                            <tr style="font-size:14px;">
                            <td width="50%">
                                   <strong style="text-align:left;">FREIGHT DETAILS</strong>
                                   </td>
                            <td >
                                   <strong style="text-align:left;">Rs.</strong>
                                   </td>
                            <td>
                                   <strong style="text-align:left;">P.</strong>
                                   </td>
                            </tr>
                             <tr>
                            <td>
                                    <asp:Label ID="labRate" style="font-size:12px;" runat="server" valign="right">Rate</asp:Label>
                                   </td>
                            <td style="text-align:right;">
                                   <asp:Label ID="lblRateRs" style="font-size:12px;" runat="server"></asp:Label>
                                   </td>
                            <td>
                                   <asp:Label ID="lblRateP" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            </tr>
                            <tr>
                            <td>
                                    <asp:Label ID="labFreight" style="font-size:12px;" runat="server" valign="right">Freight</asp:Label>
                                   </td>
                            <td style="text-align:right;">
                                   <asp:Label ID="labFreightRs" style="font-size:12px;" runat="server"></asp:Label>
                                   </td>
                            <td>
                                   <asp:Label ID="labFreightP" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            </tr>
                            <tr>
                            <td>
                                    <asp:Label ID="labST" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            <td style="text-align:right;">
                                   <asp:Label ID="labSTR" style="font-size:12px;" runat="server"></asp:Label>
                                   </td>
                            <td>
                                   <asp:Label ID="labSTP" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            </tr>
                            <tr>
                            <td>
                                    <asp:Label ID="labSur" style="font-size:12px;" runat="server" valign="right">Surcharge&nbsp;&nbsp;&nbsp;@</asp:Label>
                                   </td>
                            <td style="text-align:right;">
                                   <asp:Label ID="labSurR" style="font-size:12px;" runat="server" ></asp:Label>
                                   </td>
                            <td>
                                   <asp:Label ID="labSurP" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            </tr>
                            <tr>
                            <td>
                                    <asp:Label ID="labHam" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            <td style="text-align:right;">
                                   <asp:Label ID="labHamR" style="font-size:12px;" runat="server" ></asp:Label>
                                   </td>
                            <td>
                                   <asp:Label ID="labHamP" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            </tr>
                            <tr>
                            <td>
                                    <asp:Label ID="labFOV" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            <td style="text-align:right;">
                                   <asp:Label ID="labFOVR" style="font-size:12px;" runat="server"></asp:Label>
                                   </td>
                            <td>
                                   <asp:Label ID="labFOVP" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            </tr>
                            <tr>
                            <td>
                                    <asp:Label ID="labCollCha" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            <td style="text-align:right;">
                                   <asp:Label ID="labCollChaR" style="font-size:12px;" runat="server"></asp:Label>
                                   </td>
                            <td>
                                   <asp:Label ID="labCollChaP" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            </tr>
                            <tr>
                            <td>
                                    <asp:Label ID="labDelCha" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            <td style="text-align:right;">
                                   <asp:Label ID="labDelChaR" style="font-size:12px;" runat="server"></asp:Label>
                                   </td>
                            <td>
                                   <asp:Label ID="labDelChaP" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            </tr>
                            <tr>
                            <td>
                                    <asp:Label ID="labOctroi" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            <td style="text-align:right;">
                                   <asp:Label ID="labOctroiR" style="font-size:12px;" runat="server"></asp:Label>
                                   </td>
                            <td>
                                   <asp:Label ID="labOctroiP" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            </tr>
                            <tr>
                            <td>
                                    <asp:Label ID="labOctSer" style="font-size:12px;" runat="server" valign="right">Octroi Service Charges&nbsp;&nbsp;@</asp:Label>
                                   </td>
                            <td style="text-align:right;">
                                   <asp:Label ID="labOctSerR" style="font-size:12px;" runat="server" ></asp:Label>
                                   </td>
                            <td>
                                   <asp:Label ID="labOctSerP" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            </tr>
                            <tr>
                            <td>
                                    <asp:Label ID="labDemCha" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            <td style="text-align:right;">
                                   <asp:Label ID="labDemChaR" style="font-size:12px;" runat="server"></asp:Label>
                                   </td>
                            <td>
                                   <asp:Label ID="labDemChaP" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            </tr>
                            <tr>
                            <td>
                                    <asp:Label ID="labHamaliDel" style="font-size:12px;" runat="server" valign="right">Hamali (Delivery)</asp:Label>
                                   </td>
                            <td style="text-align:right;">
                                   <asp:Label ID="labHamaliDelR" style="font-size:12px;" runat="server"></asp:Label>
                                   </td>
                            <td>
                                   <asp:Label ID="labHamaliDelP" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            </tr>
                            <tr>
                            <td>
                                    <asp:Label ID="labOther" style="font-size:12px;" runat="server" valign="right">Others</asp:Label>
                                   </td>
                            <td style="text-align:right;">
                                   <asp:Label ID="labOtherR" style="font-size:12px;" runat="server" ></asp:Label>
                                   </td>
                            <td>
                                   <asp:Label ID="labOtherP" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            </tr>                                                    
                             <tr>
                            <td>
                                    <b><asp:Label ID="labTotal" style="font-size:12px;" runat="server" valign="right">Total</asp:Label></b>
                                   </td>
                            <td style="text-align:right;">
                                   <asp:Label ID="labTotP" style="font-size:12px;" runat="server"></asp:Label>
                                   </td>
                            <td>
                                   <asp:Label ID="Label15" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            </tr>
                             <tr>
                            <td style="text-align:right;">
                            <b><asp:Label ID="labSerTax" style="font-size:12px;" runat="server" valign="right">Service Tax&nbsp;&nbsp; @</asp:Label></b> 
                            </td>
                            <td style="text-align:right;">
                                   <asp:Label ID="labSerTaxR" style="font-size:12px;" runat="server"></asp:Label>
                                   </td>
                            <td>
                                   <asp:Label ID="labSerTaxP" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            </tr>
                            <asp:Panel ID="pnlCGST" runat="server"> 
                            <tr>
                                <td style="text-align:right;">
                                    <b><asp:Label ID="lalCGST" style="font-size:12px;" runat="server" valign="right">CGST @</asp:Label></b> 
                                </td>
                                <td style="text-align:right;">
                                    <asp:Label ID="labCGSTValue" style="font-size:12px;" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                            </tr>
                            </asp:Panel>
                            <tr>
                            <td>
                                    <b><asp:Label ID="labGTot" style="font-size:12px;" runat="server" valign="right">G. Total</asp:Label></b> 
                                   </td>
                            <td style="text-align:right;">
                                   <asp:Label ID="labGTotR" style="font-size:12px;" runat="server"></asp:Label>
                                   </td>
                            <td>
                                   <asp:Label ID="labGTotP" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                   </td>
                            </tr>
                            <tr>
                            <td colspan="3" style="text-align:center;border-bottom-style:none;">
                                    <b><asp:Label ID="Label21" style="font-size:12px;border-bottom:solid 1px black" runat="server" valign="right">Service Tax to be Paid by</asp:Label></b> 
                                   </td>                           
                            </tr>
                            <tr style="font-size:9px;">
                            <td colspan="3" style="border-top-style:none;">
                                <asp:CheckBox ID="chkCNor" runat="server" />C.Nor<asp:CheckBox ID="chkCNee" runat="server" />C.Nee <asp:CheckBox ID="chkGoods" runat="server" />Goods Transport Agency
                                </td>                                
                            </tr>
                            </table>
                            </td>
                            </tr>                                        
                         </table>
                         <table cellpadding="1" cellspacing="0" width="1050" border="1" style="font-family: Arial,Helvetica,sans-serif;">
                            <tr style="height:30px;font-size:14px;">
                                <td style="width:30%;border-bottom-style: none;" valign="top">
                                <strong>QUALITY & QUANTITY NOT CHECKED</strong>
                                </td>
                                <td style="width:40%;border-bottom-style: none;" valign="top">
                                <strong>REMARKS.</strong> <asp:Label ID="lblRemP" runat="server" valign="right"  ></asp:Label>
                                </td>
                                <td style="width:30%;border-bottom-style: none;" valign="top">
                                For:
                                <strong><asp:Label ID="lblcompany" runat="server" Font-Bold="true" Text="" ></asp:Label></strong>
                                </td>
                            </tr> 
                                 <tr style="height:10px;font-size:14px; border-top-style:none">
                                <td style="width:30%;border-top-style: none;" valign="baseline">
                                Signature of the Consigner of his agent
                                </td>
                                <td style="width:40%;border-top-style: none; text-align:center;" valign="top">
                                
                                <asp:Label ID="Label16" runat="server" Font-Bold="true" Text="Do Not PAY CASH TO THE DRIVER" ></asp:Label>
                                </td>
                                <td style="width:30%;border-top-style: none;" valign="baseline">
                                <span style="font-size:12px;">Signature</span>
                                </td>
                            </tr>                      
                        </table>
                        <table cellpadding="1" cellspacing="0" id="visiFalse" runat="server" visible="false" width="1000" border="1" style="font-family:Arial,Helvetica,sans-serif;">                       
                         <tr> 
                                <td style="font-size: 13px; border-right-style: none;width:25%;">
                                    <asp:Label ID="Label5" runat="server" Text="Tin"></asp:Label>
                                </td>
                                            
                                <td style="font-size: 13px; border-right-style: none">
                                    <b>
                                        <asp:Label ID="lblConsigneeTin" Text="" runat="server"></asp:Label></b>
                                </td>
                            </tr>
                            <tr> 
                                <td style="font-size: 13px; border-right-style: none;width:25%;" >
                                    <asp:Label ID="lblPrtyTinTxt" runat="server" Text="Tin"></asp:Label>
                                </td>
                                            
                                <td style="font-size: 13px; border-right-style: none" >
                                    <b>
                                        <asp:Label ID="lblConsignorTin" Text="" runat="server"></asp:Label></b>
                                </td>
                            </tr>
                            <tr>                            
                             <td colspan="4">
                                   <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table1">
                                   <tr style="text-align:left;font-size:18px;">
                                   <td colspan="10">
                                   <strong style="text-align:left;">Consignment Details:</strong>
                                   </td>
                                   </tr>
                                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                            <HeaderTemplate>
                                                <tr>
                                                    <td class="white_bg" style="font-size: 12px;border-bottom:1px solid #484848;" width="10%">
                                                        <strong>S.No.</strong>
                                                    </td>
                                                    <td style="font-size: 12px;border-bottom:1px solid #484848;" width="15%">
                                                        <strong>Item Name</strong>
                                                    </td>
                                                    <td style="font-size: 12px;border-bottom:1px solid #484848;" width="10%">
                                                        <strong>Unit Name</strong>
                                                    </td>
                                                     <td style="font-size: 12px;border-bottom:1px solid #484848;" width="5%">
                                                            <strong>CFT</strong>
                                                        </td>
                                                         <td style="font-size: 12px;border-bottom:1px solid #484848;" align="left" width="10%">
                                                            <strong>Dimension</strong>
                                                        </td>
                                                    <td style="font-size: 12px;border-bottom:1px solid #484848;" align="left" width="10%">
                                                        <strong>Quantity</strong>
                                                    </td>
                                                        <td style="font-size: 12px;border-bottom:1px solid #484848;" width="10%" align="center">
                                                            <strong>Weight</strong>
                                                        </td>                                                        
                                                    <div id="HideGrhdr" runat="server">
                                                        <td style="font-size: 12px;border-bottom:1px solid #484848;" align="center" width="10%">
                                                            <strong>Item Rate</strong>
                                                        </td>
                                                        <td style="font-size: 12px;border-bottom:1px solid #484848;"  width="10%" align="right">
                                                            <strong>Amount</strong>
                                                        </td>
                                                    </div>
                                                    <td style="font-size: 12px;border-bottom:1px solid #484848;" align="right" width="10%">
                                                        <strong>Detail</strong>
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="white_bg" width="10%">
                                                        <%#Container.ItemIndex+1 %>.
                                                    </td>
                                                    <td class="white_bg" width="15%">
                                                        <%#Eval("Item_Modl")%>
                                                    </td>
                                                    <td class="white_bg" width="10%">
                                                        <%#Eval("Unit")%>
                                                    </td>
                                                    <td class="white_bg" width="5%">                                      
                                                         <%#Convert.ToDouble(Eval("CFT"))%>                    
                                                           <%-- <%#String.Format("{0:0.000}", string.IsNullOrEmpty(Convert.ToString(Eval("CFT"))) ? 0 : Convert.ToDouble(Eval("CFT")))%>--%>
                                                        </td>
                                                         <td class="white_bg" align="left" width="10%">
                                                         <%#Eval("Dimension")%>                                                            
                                                        </td>
                                                    <td class="white_bg" align="left" width="10%">
                                                        <%#Eval("Qty")%>
                                                    </td>
                                                        <td class="white_bg" align="center" width="10%">
                                                            <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Ch_Weight")))%>
                                                        </td>                                                         
                                                    <div id="HideGritem" runat="server">
                                                        <td class="white_bg"  width="10%" align="center">
                                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Item_Rate")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td class="white_bg" width="10%" align="right">
                                                            <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </div>
                                                    <td class="white_bg" width="10%" align="right">
                                                        <%#(Eval("Detail"))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                              <tr>
                                                    <td class="white_bg" width="10%">
                                                       &nbsp; 
                                                    </td>
                                                     <td class="white_bg" width="15%">
                                                       &nbsp; 
                                                    </td>                                                    
                                                    <td class="white_bg" width="10%">
                                                        <asp:Label ID="lblFTTot" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="white_bg" width="5%">
                                                        <asp:Label ID="lblTotalCFT" Font-Bold="true" runat="server"></asp:Label>
                                                    </td> 
                                                     <td class="white_bg" width="10%" align="left">
                                                       &nbsp; 
                                                    </td> 
                                                    <td class="white_bg" align="left" width="10%">
                                                        <asp:Label ID="lblFTQty"  Font-Bold="true" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="white_bg" width="10%" align="center">
                                                        <asp:Label ID="lblFTtotalWeight" Font-Bold="true" runat="server"></asp:Label>
                                                    </td>
                                                                                                       
                                                    <div id="hidfooterdetl" runat="server">
                                                        <td class="white_bg" width="10%" align="center">
                                                        </td>                                                        
                                                        <td class="white_bg" width="10%" align="right">
                                                            <asp:Label ID="lblFTTotalAmnt" Font-Bold="true" runat="server"></asp:Label>
                                                        </td>
                                                    </div>
                                                    <td class="white_bg" width="10%" align="right">
                                                    &nbsp;
                                                    </td>
                                                </tr>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                            <td>
                            <table width="100%">
                            <tr>
                            <td style="width:50%;" valign="top">
                            <table border="0" width="100%" class="white_bg" style="border-right:1px solid #484848;">
                            <tr>
                                <td style="width: 100%">
                                    <table width="100%">
                                        <tr runat="server" id="trRemarks">
                                            <td colspan="2" align="left">                                                
                                                    <tr>
                                                    <td>
                                                            <b><asp:Label ID="Label10" style="border-bottom:1px solid #484848; font-size:14px;" runat="server" valign="left" Text="Remarks:"></asp:Label></b>
                                                        </td>                                                        
                                                    </tr>
                                                    <tr>
                                                    <td>
                                                            <asp:Label ID="lblremark" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                                        </td>
                                                    </tr>                                                
                                            </td>                                            
                                        </tr>
                                         <tr runat="server" id="trTnC">
                                            <td colspan="2" align="left">                                                
                                                    <tr>
                                                    <td>
                                                            <b><asp:Label ID="Label14" style="border-bottom:1px solid #484848; font-size:14px;" runat="server" valign="left" Text="Terms & Conditions:"></asp:Label></b>
                                                        </td>                                                        
                                                    </tr>
                                                    <tr>
                                                    <td>
                                                            <asp:Label ID="lblTnCGR" style="font-size:12px;" runat="server" valign="right"></asp:Label>
                                                        </td>
                                                    </tr>                                                
                                            </td>                                            
                                        </tr>
                                         <tr>
                                            <td colspan="2" valign="baseline" align="left">                                                
                                                    <tr>
                                                     <td>
                                                    &nbsp;
                                                    </td>
                                                    </tr>
                                                    <tr>
                                                    <td>
                                                            <b><asp:Label ID="Label17" style="font-size:14px;" runat="server" valign="left" Text="________________"></asp:Label></b>
                                                        </td>                                                        
                                                    </tr>                                                                                                      
                                                    <tr>
                                                    <td>
                                                            <asp:Label ID="Label18" runat="server" style="font-size:12px;" valign="right" Text="Signature/Seal of the Company"></asp:Label>
                                                        </td>
                                                    </tr>                                                
                                            </td>                                            
                                        </tr>                                       
                                    </table>
                                </td>
                            </tr>
                            </table>
                            </td>
                            <td style="width:50%;" valign="top">
                            <table border="0" width="100%" class="white_bg" >
                            <tr>                            
                             <td colspan="4">
                                   <table border="0" cellspacing="0" style="font-size: 12px" width="100%" runat="server" id="Table2">
                                   <tr style="text-align:left;font-size:18px;">
                                   <td colspan="8" style="border-bottom:1px solid #484848;">
                                   <strong style="text-align:left;">Freight Details:</strong>
                                   </td>
                                   </tr>
                                      <tr>     
                                      <td align="right" width="20%">
                                      &nbsp;
                                      </td>
                                                  <td style="font-size: 12px;" align="right" width="10%">
                                                            <strong><asp:Label ID="lblFOVPrint" runat="server"></asp:Label></strong>
                                                        </td> 
                                                    <td style="font-size: 12px;" align="right" width="10%">
                                                        <strong>Surcharge</strong>
                                                    </td>
                                                   
                                                      <td class="white_bg" style="font-size: 12px;" align="right" width="20%">
                                                        <strong><asp:Label ID="lblOctroiPrint" runat="server"></asp:Label></strong>
                                                    </td>
                                                     <td style="font-size: 12px;" align="right" width="20%">
                                                        <strong><asp:Label ID="lblDemuChargesPrint" runat="server"></asp:Label></strong>
                                                    </td>                                                       
                                                </tr>
                                           <tr>  
                                            <td align="right" width="20%" style="border-bottom:solid 1px black;">
                                      &nbsp;
                                      </td>                                     
                                                    <td style="font-size: 12px;text-align:right; border-bottom:solid 1px black;" width="10%">
                                                        <asp:Label ID="lblFOV" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="font-size: 12px;text-align:right; border-bottom:solid 1px black;" width="10%">
                                                        <asp:Label ID="lblSurchargeP" runat="server"></asp:Label>
                                                    </td>
                                                        <td style="font-size: 12px;text-align:right; border-bottom:solid 1px black;" width="10%">                                                            
                                                              <asp:Label ID="lblOctroi" runat="server"></asp:Label>
                                                        </td>                                                    
                                                        <td style="font-size: 12px;text-align:right; border-bottom:solid 1px black;" align="left" width="10%">                                                            
                                                            <asp:Label ID="lblDemurrage" runat="server"></asp:Label>
                                                        </td>                                                                                                          
                                                    
                                                </tr>
                                                  <tr>
                                                    <td align="right" width="20%">
                                                         &nbsp;
                                                         </td>
                                                         <td style="font-size: 12px;" align="right" width="10%">
                                                            <strong><asp:Label ID="lblUnloadingPrint" runat="server"></asp:Label></strong>
                                                        </td> 
                                                        <td style="font-size: 12px;" align="right" width="20%">
                                                            <strong><asp:Label ID="lblCollChargePrint" runat="server"></asp:Label></strong>
                                                        </td> 
                                                  <td style="font-size: 12px;" align="right" width="20%">
                                                        <strong><asp:Label ID="lblDelChargesPrint" runat="server"></asp:Label></strong>
                                                    </td>
                                                    <td style="font-size: 12px;" align="right" width="20%">
                                                        <strong>Sub Total</strong>
                                                    </td>                                                                                                      
                                                        </tr>
                                                         <tr>
                                                          <td style="border-bottom:solid 1px black;" align="right" width="20%">
                                                         &nbsp;
                                                         </td>
                                                         <td style="font-size: 12px;text-align:right; border-bottom:solid 1px black;" align="left" width="10%">                                                            
                                                            <asp:Label ID="lblUnloading" runat="server"></asp:Label>
                                                        </td> 
                                                         <td style="font-size: 12px;text-align:right;border-bottom:solid 1px black;" width="10%">                                                        
                                                         <asp:Label ID="lblCollectionCharges" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="white_bg" style="font-size: 12px;text-align:right;border-bottom:solid 1px black;" width="10%">                                                        
                                                        <asp:Label ID="lblDeliveryCharges" runat="server"></asp:Label>
                                                    </td>
                                                      <td style="font-size: 12px;text-align:right;border-bottom:solid 1px black;" width="10%">
                                                        <asp:Label ID="lblSubtotalP" runat="server"></asp:Label>
                                                    </td>                                                        
                                                        </tr>
                                                        <tr>
                                                         <td align="right" width="20%">
                                                         &nbsp;
                                                         </td>
                                                        <td runat="server" id="tdser" style="font-size: 12px;" align="right" width="20%">
                                                        <strong>Serv. Tax</strong>
                                                    </td>
                                                    <td runat="server" id="tdswac" style="font-size: 12px;" align="right" width="20%">
                                                        <strong>SwachhBhrt Tax</strong>
                                                    </td>                                                             
                                                        <td runat="server" id="tdKrish" style="font-size: 12px;" align="right" width="20%">
                                                            <strong>Krishi Kalyan Tax</strong>
                                                        </td> 
                                                        <td style="font-size: 12px;" align="right" colspan="8">
                                                        <strong>Net Amnt</strong>
                                                    </td>
                                                        </tr>
                                                        <tr>
                                                         <td align="right" width="20%">
                                                         &nbsp;
                                                         </td>
                                                         <td style="font-size: 12px;text-align:right;" width="10%">
                                                        <asp:Label ID="lblSerTaxCharge" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="font-size: 12px;text-align:right;" width="10%">
                                                    <asp:Label ID="lblSwchBhrt" runat="server"></asp:Label>                                                        
                                                    </td>     
                                                         <td style="font-size: 12px;text-align:right;" width="10%">
                                                            <asp:Label ID="lblKrishi" runat="server"></asp:Label>
                                                        </td>   
                                                        <td style="font-size: 12px;text-align:right;" colspan="8">
                                                         <asp:Label ID="lblNetAmntP" runat="server"></asp:Label>
                                                    </td>
                                                        </tr>
                                               
                                    </table>
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
                                    <table width="100%">
                                                <tr>
                                                <td align="right" class="white_bg"  style="font-size: 13px" width="30%"></td>
                                                <td align="right" class="white_bg"  style="font-size: 13px" width="10%"></td>
                                                <td align="right" class="white_bg" style="font-size: 13px" width="30%">
                                                            <b>
                                                                <asp:Label ID="lblCompname" runat="server"></asp:Label></b>
                                                    </td></tr>
                                                <tr>
                                                <td align="left"  class="white_bg"   style="font-size: 13px; text-align: justify; padding-right:20px;" width="25%" >
                                                            <asp:Label ID="lblTerms" Font-Size="10px" runat="server"></asp:Label>
                                                    </td>&nbsp;&nbsp;
                                                    <td align="left" class="white_bg" style="font-size: 13px; text-align: justify; margin-left:5px;" width="37%">
                                                            <asp:Label ID="lblterms1" Font-Size="11px" runat="server"></asp:Label>
                                                    </td>
                                                    <td align="right" class="white_bg"  style="font-size: 13px" width="10%"></td>                                                    
                                                </tr>
                                                                                                   
                                                    </table>
                                </td>
                            </tr>
                             <tr>
                                          <td align="left" class="white_bg" style="width:18%;font-size: 13px; border-right-style: none" valign="top">
                                                <asp:Label ID="lbltxtFromcity"  runat="server">From City&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            
                                            <td align="left" class="white_bg" valign="top" style="width:20%;font-size: 13px; border-right-style: none">
                                                <b>
                                                    </b>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <asp:Label ID="lbltxttocity" runat="server">To City&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    </b>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <asp:Label ID="lbltxtdelvryPlace" runat="server">Delivery Place&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                           
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblDelvryPlace" runat="server"></asp:Label></b>
                                            </td>
                                            
                                        </tr>                                       
                                        <tr>
                                            <td align="left" class="white_bg" style="width:18%;font-size: 13px; border-right-style: none" valign="top">
                                                <asp:Label ID="lblViaCity"  runat="server">Via City&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            
                                            <td align="left" class="white_bg" valign="top" style="width:20%;font-size: 13px; border-right-style: none">
                                                        <b> <asp:Label ID="lblValueViaCity" runat="server"></asp:Label> </b>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px;width:18%; border-right-style: none">
                                                <asp:Label ID="lblConsName" runat="server">Consignor's Name &nbsp;:</asp:Label>
                                            </td>
                                            
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                              <b>
                                                    <asp:Label ID="lblvalConsName" runat="server"></asp:Label></b>
                                            </td>
                                             <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <asp:Label ID="Label9" runat="server">Serv. Tax Paid by&nbsp;:</asp:Label>
                                            </td>
                                           
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblSerTaxto" runat="server"></asp:Label></b>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <asp:Label ID="Label8" Text="Modvat Copy:" runat="server"></asp:Label>
                                            </td>
                                            
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    </b>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <asp:Label ID="Label13" runat="server">Tran. Type&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    </b>
                                            </td>                                            
                                        </tr>
                                        <tr id="tr1" runat="server">
                                         <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <asp:Label ID="lblNameShipmentno" runat="server">Shipment No.&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblShipmentNo" runat="server"></asp:Label></b>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                               <asp:Label ID="lblDinNoText"  runat="server">DI No.&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                               <b><asp:Label ID="lblDinNo" runat="server"></asp:Label></b>
                                            </td>                                            
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <asp:Label ID="lblNameContnrNo" Text="Container No.:" runat="server"></asp:Label>
                                            </td>
                                            
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblContainerNo" runat="server"></asp:Label></b>
                                            </td>     
                                        </tr>
                                        <tr id="tr2" runat="server">                                            
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px;width:18%; border-right-style: none">
                                              <asp:Label ID="lblEGPNo" runat="server">EGP No.&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" colspan="0" style="font-size: 13px; border-right-style: none">
                                              <b> <asp:Label ID="lblEGPNoval" runat="server"></asp:Label> </b>
                                            </td>
                                             <td align="left" class="white_bg" valign="top" style="font-size: 13px;width:18%; border-right-style: none">
                                                <asp:Label ID="lblRefNo" runat="server">Ref No.&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" colspan="0" style="font-size: 13px; border-right-style: none">
                                              <b> <asp:Label ID="lblrefnoval" runat="server"></asp:Label> </b>
                                            </td>
                                             <td align="left" class="white_bg" style="width:18%;font-size: 13px; border-right-style: none" valign="top">
                                                <asp:Label ID="lblOrderNo"  runat="server">Order No&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                        <td align="left" class="white_bg" valign="top" style="width:20%;font-size: 13px; border-right-style: none">
                                                        <b> <asp:Label ID="lblOrderNoVal" runat="server"></asp:Label> </b>
                                            </td>
                                        </tr>                                       
                                        <tr id="tr3" runat="server">
                                         <td align="left" class="white_bg" style="width:18%;font-size: 13px; border-right-style: none" valign="top">
                                                <asp:Label ID="lblFormNo"  runat="server">Form No&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            
                                            <td align="left" class="white_bg" valign="top" style="width:20%;font-size: 13px; border-right-style: none">
                                                        <b> <asp:Label ID="lblFormNoVal" runat="server"></asp:Label> </b>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px;width:18%; border-right-style: none">
                                                <asp:Label ID="lbltxtagent" runat="server" >Agent&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                           
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblAgent" runat="server"></asp:Label></b>
                                            </td>
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <asp:Label ID="lblNameSealNo" runat="server">Seal No.&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblSealNo" runat="server"></asp:Label></b>
                                            </td>
                                        </tr>
                                        <tr id="tr4" runat="server">
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                              <asp:Label ID="lblTotItem" runat="server">Total Item Value&nbsp;:</asp:Label>
                                            </td>                                            
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                               <b>
                                                    <asp:Label ID="lblTotItemValue" runat="server"></asp:Label></b>
                                            </td>
                                         <td align="left" class="white_bg" valign="top" style="font-size: 13px;width:18%; border-right-style: none">
                                                <asp:Label ID="lblNameContnrType" runat="server">Type&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                           
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblCntnrType" runat="server"></asp:Label></b>
                                            </td>                                         
                                          <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <asp:Label ID="lblNameCntnrSize" runat="server">Size &nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                           
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                <b>
                                                    <asp:Label ID="lblContainerSize" runat="server"></asp:Label></b>
                                            </td>
                                        </tr>  
                                        <tr>                                       
                            <td style="font-size: 13px; border-right-style: none;width:25%;"> 
                             <asp:Label ID="Label2" runat="server">Address</asp:Label>
                            </td>
                            <td style="font-size: 13px; border-right-style: none">
                            <b></b>
                            </td> 
                            <td style="font-size: 13px; border-right-style: none;width:25%;"> 
                            <asp:Label ID="Label4" runat="server">Address</asp:Label>
                            </td>
                            <td style="font-size: 13px; border-right-style: none">
                           <b></b>
                            </td>           
                            </tr>    
                            <tr>                                                       
                                            <td align="left" class="white_bg" style="width:18%;font-size: 13px; border-right-style: none" valign="top" >
                                                <asp:Label ID="lbltxtgrno" runat="server">GR. No.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                            
                                            <td align="left" class="white_bg"  valign="top" style="width:22%;font-size: 13px; border-right-style: none">
                                                <b>
                                                    </b>
                                            </td>
                                            <td align="left" class="white_bg" style="width:14%;font-size: 13px; border-right-style: none" valign="top">
                                                <asp:Label ID="lbltxtgrdate"  Text="GR. Date" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
                                            </td>
                                            
                                            <td align="left" class="white_bg" valign="top" style="width:20%;font-size: 13px; border-right-style: none">
                                                <b>
                                                    </b>
                                            </td>
                                             <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;width:15%;">
                                                <asp:Label ID="Label6"  runat="server">Lorry No &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                            </td>
                                           
                                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;width:15%;">
                                                <b>
                                                    <asp:Label ID="lblLorryNo" runat="server"></asp:Label></b>
                                            </td>                                              
                                        </tr>    
                        </table>
                    </div>
                </td>
            </tr>
        </div>
        <div class="col-lg-0" style="display: NONE;">
            <tr style="display: none">
                <td class="white_bg" align="center">
                    <div id="Jainprint" style="font-size: 13px;">
                        <table cellpadding="1" cellspacing="0" width="1000" border="1" style="font-family: Arial,Helvetica,sans-serif;">
                            <tr style="height:100px;">
                                <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                                    border-left-style: none; border-right-style: none";>
                                    <div style="text-align:left;Width:140px; float:left;">
                                        <asp:Image ID="ImgLogoJain" Width="140px" Height="90px" runat="server"></asp:Image>
                                     </div>
                                   <div id="header1" runat="server" style="text-align:center; Width:650px; float:center;">
                                            <strong>
                                                <asp:Label ID="lblCompanyname1" runat="server" Style="font-size: 18px;"></asp:Label><br />
                                            </strong>
                                            <asp:Label ID="Label7" runat="server" Text="(Fleet Owners & Transport Contractor)"></asp:Label><br />
                                           <strong> Head Office : <asp:Label ID="lblCompAdd3" runat="server"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblCompAdd4" runat="server"></asp:Label>
                                            <asp:Label ID="lblCompCity1" runat="server"></asp:Label>&nbsp;&nbsp;
                                            <asp:Label ID="lblCompState1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblCompCityPin1" runat="server"></asp:Label><br /></strong>
                                            <asp:Label ID="lblTin1" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;<asp:Label
                                                ID="lblCompTIN1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lbltxtPanNo1" runat="server" Text="PAN NO. :"></asp:Label>&nbsp;&nbsp;<asp:Label
                                                ID="lblPanNo1" runat="server"></asp:Label>
                                     </div>
                                </td>
                                
                            </tr>
                            <tr>
                                <td>
                                        <table width="100%">
                                        <tr>
                                        <td style="width:50%;" valign="top">
                                          <table border="0" width="100%" class="white_bg" style="border-right:1px solid #484848;">
                                            <tr>
                                            <td align="center" colspan="2" style="border-bottom:1px solid #484848;font-size: 13px;"> 
                                            <strong>DECLARATION</strong>
                                            </td> 
                                            </tr>
                                            <tr>
                                            <td colspan="2" style="border-bottom:1px solid #484848;font-size: 11px;">
                                             <small> I/We declare that we have not taken credit of Excise Duty paid on capital goods or credit of service tax paid on input services used for providing "Transportation of Goods by Road" service under the provision of credit Rules 2004. I/We also declare that we have not availed the benefit under Notificatiuon 08/2015-ST Date 01-03-2015. 
                                             </small>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td colspan="2" style="font-size:10px";> <strong> Person Liable to pay Service Tax Consignor</strong></td>
                                            </tr>
                                            </table>
                                        </td>
                                        <td style="width:50%;" valign="top">
                                        <table border="0" width="100%" class="white_bg" style="border-right:1px solid #484848;">
                                            <tr>
                                            <td colspan="1" style="border-bottom:1px solid #484848;width:150px;border-right:1px solid #484848;font-size: 13px;"> 
                                            <strong>PARTY ORDER NO</strong> 
                                            </td> 
                                            <td colspan="1" style="border-bottom:1px solid #484848;width:100px;border-right:1px solid #484848;font-size: 13px;"> 
                                             <strong> DATE</strong> 
                                            </td> 
                                            <td align="center" colspan="2" style="border-bottom:1px solid #484848;width:150px;font-size: 13px;"> 
                                             <asp:Label ID="lblGrDate1" runat="server"></asp:Label>
                                            </td>
                                            </tr>
                                            <tr style="height:20px">
                                            <td colspan="1" style="width:150px;border-right:1px solid #484848;font-size: 13px;"> 
                                            <strong></strong> 
                                            </td> 
                                            <td colspan="1" style="border-bottom:1px solid #484848;width:100px;border-right:1px solid #484848;font-size: 13px;"> 
                                             <strong> LR NO.</strong> 
                                            </td> 
                                            <td align="center" colspan="2" style="border-bottom:1px solid #484848;width:150px;font-size: 13px;"> 
                                             <asp:Label ID="lblGRno1" runat="server"></asp:Label>
                                            </td>
                                            </tr>
                                             <tr style="height:20px">
                                            <td colspan="1" style="width:150px;border-right:1px solid #484848;"> 
                                            <strong></strong> 
                                            </td> 
                                            <td colspan="1" style="border-bottom:1px solid #484848;width:100px;border-right:1px solid #484848;font-size: 13px;"> 
                                             <strong> FROM</strong> 
                                            </td> 
                                            <td align="center" colspan="2" style="border-bottom:1px solid #484848;width:150px;font-size: 13px;"> 
                                             <asp:Label ID="lblFromCity1" runat="server"></asp:Label>
                                            </td>
                                            </tr>
                                             <tr style="height:20px">
                                            <td colspan="1" style="width:150px;border-right:1px solid #484848;"> 
                                            <strong></strong> 
                                            </td> 
                                            <td colspan="1" style="width:100px;border-bottom:1px solid #484848;border-right:1px solid #484848;font-size: 13px;"> 
                                             <strong> TO</strong> 
                                            </td> 
                                            <td align="center" colspan="2" style="border-bottom:1px solid #484848;width:150px;font-size: 13px;"> 
                                             <asp:Label ID="lblToCity1" runat="server"></asp:Label>
                                            </td>
                                            </tr>
                                              <tr style="height:20px">
                                            <td colspan="1" style="width:150px;border-right:1px solid #484848;"> 
                                            <strong></strong> 
                                            </td> 
                                            <td colspan="1" style="border-bottom:1px solid #484848;width:100px;border-right:1px solid #484848;font-size: 13px;"> 
                                             <strong> VIA</strong> 
                                            </td> 
                                            <td align="center" colspan="2" style="border-bottom:1px solid #484848;width:150px;font-size: 13px;"> 
                                             <asp:Label ID="lblJainVia" runat="server"></asp:Label>
                                            </td>
                                            </table>
                                        </td>
                                        </tr>
                                        </table>
                                </td>
                            </tr>
                            
                            <tr>
                            <td>
                            <table width="100%">
                            <tr>
                            <td style="width:50%;" valign="top">
                            <table border="0" width="100%" class="white_bg" style="border-right:1px solid #484848;">
                            <tr>
                            <td align="center" colspan="2" style="border-bottom:1px solid #484848;font-size: 13px;"> 
                            <strong>CONSIGNOR</strong>
                            </td> 
                            </tr>
                            <tr id="trConsigneeName1" runat="server">
                            <td style="font-size: 13px; border-right-style: none;width:20%;">
                                 <b><asp:Label ID="Label52" runat="server">Name</asp:Label></b>
                            </td>
                            <td style="font-size: 13px; border-right-style: none">
                               <asp:Label ID="lblConsigeeName1" runat="server"></asp:Label>  
                            </td>
                            </tr>
                            <tr style="height:60px;" valign="top">
                            <td style="font-size: 13px; border-right-style: none;width:20%;"> 
                             <b><asp:Label ID="Label54" runat="server">Address</asp:Label></b>
                            </td>
                            <td style="font-size: 13px; border-right-style: none">
                            <asp:Label ID="lblConsigneeAddress1" runat="server"></asp:Label>
                            </td>
                            </tr>
                            </table>
                            </td>
                            <td style="width:50%;" valign="top">
                            <table border="0" width="100%" class="white_bg"> 
                              <tr>
                            <td align="center" colspan="2" style="border-bottom:1px solid #484848;height:10px;font-size: 13px;"> 
                            <strong>CONSIGNEE</strong>
                            </td> 
                            </tr>
                             <tr id="trConsignorName1" runat="server">
                            <td style="font-size: 13px; border-right-style: none;width:20%;"> 
                            <b><asp:Label ID="lbltxtName" runat="server">Name</asp:Label></b>
                            </td>
                            <td style="font-size: 13px; border-right-style: none">
                            <asp:Label ID="lblConsignorName1" runat="server"></asp:Label>
                            </td>
                            </tr>
                               <tr style="height:60px;" valign="top">
                            <td style="font-size: 13px; border-right-style: none;width:20%;"> 
                            <b><asp:Label ID="lblTxtAdd" runat="server">Address</asp:Label></b>
                            </td>
                            <td style="font-size: 13px; border-right-style: none">
                           <asp:Label ID="lblConsignorAddress1" runat="server"></asp:Label>
                            </td>
                            </tr>
                            </table>
                            </td>
                            </tr>
                            </table> 
                            </td> 
                            </tr>
                            
                            <tr>
                                <td>
                                        <table width="100%">
                                        <tr>
                                        <td style="width:50%;" valign="top">
                                          <table border="0" width="100%" class="white_bg" style="border-right:1px solid #484848;">
                                            <tr>
                                            <td align="center" style="border-bottom:1px solid #484848;border-right:1px solid #484848;font-size: 13px;"> 
                                            <strong>Freight Detail</strong>
                                            </td> 
                                            <td id="divAmntHead" runat="server" align="center" style="border-bottom:1px solid #484848;font-size: 13px;"> 
                                            <strong>Invoice Value</strong>
                                            </td> 
                                            </tr>
                                            <tr style="height:32px">
                                            <td align="center" style="border-bottom:1px solid #484848;border-right:1px solid #484848;font-size: 13px;">
                                              <asp:Label ID="lblGRType" runat="server" Font-Size="13px"></asp:Label>
                                            </td>
                                             <td id="divAmntvalue" runat="server" align="center" style="border-bottom:1px solid #484848;">
                                                <asp:Label ID="lblNetValue" runat="server" Font-Size="13px"></asp:Label>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td  colspan="2"> <strong> <small>Acknowledge Consignee with Signature & Stamp</small></strong></td>
                                            </tr>
                                            <tr><td colspan="2"></td> </tr>
                                            <tr><td colspan="2"></td> </tr>
                                            <tr><td colspan="2"></td> </tr>
                                            </table>
                                        </td>
                                        <td style="width:50%;" valign="top">
                                        <table border="0" width="100%" class="white_bg" style="border-right:1px solid #484848;">
                                            <tr>
                                            <td colspan="1" style="border-bottom:1px solid #484848;width:150px;border-right:1px solid #484848;font-size: 13px;"> 
                                            <strong>Sap Delivery No.</strong> 
                                            </td> 
                                            <td colspan="1" style="border-bottom:1px solid #484848;width:100px;border-right:1px solid #484848;font-size: 13px;"> 
                                             <strong> </strong> 
                                            </td> 
                                            <td colspan="2" style="border-bottom:1px solid #484848;width:150px;font-size: 13px;"> 
                                             <strong> <b>Truck/Trailor No.</b></strong> 
                                            </td>
                                            </tr>
                                            <tr style="height:32px">
                                            <td colspan="1" style="border-bottom:1px solid #484848;width:150px;border-right:1px solid #484848;font-size: 13px;"> 
                                            <strong>Invoice No.</strong> 
                                            </td> 
                                            <td align="center" colspan="1" style="border-bottom:1px solid #484848;width:100px;border-right:1px solid #484848;font-size: 13px;"> 
                                             <asp:Label ID="lblInvNoValue" runat="server"></asp:Label>
                                            </td> 
                                            <td align="center" colspan="2" style="border-bottom:1px solid #484848;width:150px;font-size: 13px;"> 
                                               <asp:Label ID="lblLorryNo1" runat="server"></asp:Label>
                                            </td>
                                            </tr>
                                             <tr style="height:20px">
                                            <td colspan="1" style="border-bottom:1px solid #484848;width:150px;border-right:1px solid #484848;font-size: 13px;"> 
                                            <strong>Excise Gate Pass No.</strong> 
                                            </td> 
                                            <td colspan="3" style="border-bottom:1px solid #484848;width:100px;border-right:1px solid #484848;font-size: 13px;">  
                                            </td> 
                                            </tr>
                                           <tr id="DivJainShipNo" runat="server" style="height:20px">
                                                <td colspan="1" style="border-left:1px solid #484848;border-bottom:1px solid #484848;width:150px;border-right:1px solid #484848;font-size: 13px;"> 
                                                <strong>Shipment No.</strong> 
                                                </td> 
                                                <td colspan="3" style="border-bottom:1px solid #484848;width:100px;border-right:1px solid #484848;font-size: 13px;">  
                                                    <asp:Label ID="lblJainShipNo" runat="server" ></asp:Label>
                                                </td> 
                                            </tr>
                                             <tr id="DivJainContainerNo" runat="server" style="height:20px">
                                                <td colspan="1" style="border-left:1px solid #484848;border-bottom:1px solid #484848;width:150px;border-right:1px solid #484848;font-size: 13px;"> 
                                                <strong>Container No.</strong> 
                                                </td> 
                                                <td colspan="3" style="border-bottom:1px solid #484848;width:100px;border-right:1px solid #484848;font-size: 13px;">  
                                                    <asp:Label ID="lblJainContainerNo" runat="server" ></asp:Label>
                                                </td> 
                                            </tr>
                                             <tr id="DivJainSealNo" runat="server" style="height:20px">
                                                <td colspan="1" style="border-left:1px solid #484848;border-bottom:1px solid #484848;width:150px;border-right:1px solid #484848;font-size: 13px;"> 
                                                <strong>Seal No.</strong> 
                                                </td> 
                                                <td colspan="3" style="border-bottom:1px solid #484848;width:100px;border-right:1px solid #484848;font-size: 13px;">  
                                                    <asp:Label ID="lblJainSealNo" runat="server" ></asp:Label>
                                                </td> 
                                            </tr>
                                             <tr id="DivJainOrderNo" runat="server" style="height:20px">
                                                <td colspan="1" style="border-left:1px solid #484848;border-bottom:1px solid #484848;width:150px;border-right:1px solid #484848;font-size: 13px;"> 
                                                <strong>Order No.</strong> 
                                                </td> 
                                                <td colspan="3" style="border-bottom:1px solid #484848;width:100px;border-right:1px solid #484848;font-size: 13px;">  
                                                    <asp:Label ID="lblJainOrderNo" runat="server" ></asp:Label>
                                                </td> 
                                            </tr>
                                             <tr  id="DivJainFormNo" runat="server" style="height:20px">
                                                <td colspan="1" style="border-left:1px solid #484848;border-bottom:1px solid #484848;width:150px;border-right:1px solid #484848;font-size: 13px;"> 
                                                <strong>Form No.</strong> 
                                                </td> 
                                                <td colspan="3" style="border-bottom:1px solid #484848;width:100px;border-right:1px solid #484848;font-size: 13px;">  
                                                    <asp:Label ID="lblJainFormNo" runat="server" ></asp:Label>
                                                </td> 
                                            </tr>
                                             <tr style="height:20px">
                                            <td align="center" colspan="2" style="border-left:1px solid #484848;font-size: 13px;width:150px;border-bottom:1px solid #484848;border-right:1px solid #484848;"> 
                                            <strong>Product</strong> 
                                            </td> 
                                            <td align="center" colspan="2" style="font-size: 13px;width:100px;border-right:1px solid #484848;border-bottom:1px solid #484848;"> 
                                             <strong>Quantity(MT)</strong> 
                                            </td> 
                                            </tr>
                                             <tr style="height:20px">
                                            <td align="center" colspan="2" style="border-left:1px solid #484848;width:150px;border-right:1px solid #484848;border-bottom:1px solid #484848;"> 
                                                <asp:Label ID="lblItemName" runat="server" Font-Size="13px"></asp:Label> 
                                            </td> 
                                            <td align="center" colspan="2" style="width:100px;border-right:1px solid #484848;border-bottom:1px solid #484848;"> 
                                             <asp:Label ID="lblItemQty" Font-Size="13px" runat="server"></asp:Label> 
                                            </td> 
                                            </tr>
                                            <tr>
                                            <td align="center" valign="bottom" colspan="4" style="height:60px;border-left:1px solid #484848;">
                                            <%--<asp:Label ID="lblCompname1" runat="server" Text="Authorised Signatory"></asp:Label>--%>
                                            <small><asp:Label ID="lblauth" runat="server" Text="Authorised Signatory"></asp:Label></small>
                                            
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
            <%--USERPREF SETTING --%>
        <div class="pop-up-parent">
            <div id="PopUpSetting" class="pop-up" style="width: 600px; height: auto; display: none;">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="popform_header">
                                <i class="img-setting"></i><span>Goods Receipt Setting</span> <i class="fa fa-times">
                                </i>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <section class="panel full_form_container">
								<div class="panel-body">
                                    <div class="col-sm-4">
                                        <asp:CheckBox ID="chkSendSMSOnGRSave" runat="server" Text="Send SMS (GR Save)"></asp:CheckBox>
                                    </div>
                                </div>
                            </section>
                        </div>
                        <hr />
                        <div class="clearfix fifth_right panel">
                            <section class="panel panel-in-default btns_without_border">
                                <asp:LinkButton ID="lnkBtnSaveUserPref" runat="server" CssClass="pop-up-button btn btn-s-md btn-success center-block" TabIndex="17" OnClick="lnkBtnSaveUserPref_OnClick" CausesValidation="true" ><i class="fa fa-save"></i> Save</asp:LinkButton>
                            </section>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    <asp:HiddenField ID="hidmindate" runat="server" />
    <asp:HiddenField ID="hidPages" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />
    <%---Used in Grid---%>
    <asp:HiddenField ID="hidrowid" runat="server" />
    <asp:HiddenField ID="hidratetype" runat="server" />
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
        function Divopen() {
            $('#Amount').modal('show');
        }
    </script>
    <script language="javascript" type="text/javascript">
        function OnFillTran() {
//            if (document.getElementById("<%=ddlTranType.ClientID%>").value == "0") {
//            }
//            else {
                __doPostBack('TranTypeValue', '');
//            }

        }
    </script>
    <script language="javascript" type="text/javascript">
        function Divopen() {
            $('#Amount').modal('show');
        }
    </script>
    <script language="javascript" type="text/javascript">
        function SetFocus() {
            $('input[type="text"]').focus(function () {
                $(this).addClass("focus");
            });
            $('input[type="text"]').blur(function () {
                $(this).removeClass("focus");
            });
            $("select").focus(function () {
                $(this).addClass("focus");
            });
            $("select").blur(function () {
                $(this).removeClass("focus");
            });
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_beginRequest(function () {
            SetFocus();
            setDatecontrol();
        });

        prm.add_endRequest(function () {
            SetFocus();
            setDatecontrol();
        });

        //$(document).ready(function () {
        //    setDatecontrol();
        //});
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

            $("#<%=txtInstDate.ClientID %>").datepicker({
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

        function openModal() {
            $('#dvContainerdetails').modal('show');
        }

        function openModalForChlnGen() {
            $('#dvChlnGenrateDetl').modal('show');
        }

        function openModalGrdDtls() {
            $('#gr_details_form').modal('show');
        }

        function ShowClient() {
            $("#dvGrdetails").fadeIn(300);
        }

        function closepopup() {
            $('#Upload_div').modal('hide');
        }


        

      
    </script>
    <script language="javascript" type="text/javascript">
        function OnChangeddlRateType() {
            if (document.getElementById("<%=ddlTranType.ClientID%>").value == "0") {
            }
            else {
                __doPostBack('TranTypeValue', '');
            }

        }

        function openGridDetail() {
            $('#gr_details_form').modal('show');
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

        function OnChangeGRType() {
            if (document.getElementById("<%=ddlGRType.ClientID%>").value == "1") {
                document.getElementById("<%=lnkLorryType.ClientID %>").disabled = false;
            }
            else {
                document.getElementById("<%=lnkLorryType.ClientID %>").disabled = true;
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = "";
            var Pages = "1";
            Pages = document.getElementById("<%=hidPages.ClientID%>").value;
            var prtContent3 = "<p style='page-break-before: always'></p>";
            for (i = 0; i < Pages; i++) {
                prtContent = prtContent + "<table width='100%' border='0'></table>";
                if (Pages != 1) {
                    prtContent = prtContent + "<tr><td><strong>" + ((i == 1) ? "[Office Copy]" : (i == 2) ? "[Consignor Copy]" : (i == 3) ? "[Consignee Copy]" : "[Driver Copy]") + "</strong></td></tr>";
                }
                var prtContent1 = document.getElementById(strid);
                var prtContent2 = prtContent1.innerHTML;
                prtContent = prtContent + prtContent2 + ((i < 3) ? prtContent3 : "");
            }
            var WinPrint = window.open('', '', 'left=0,top=0,width=800,height=800,toolbar=1,scrollbars=1,status=1');
            WinPrint.document.write(prtContent);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            //WinPrint.close();
            return false;
        }
       
    </script>
    <script language="javascript" type="text/javascript">
        function OnPerChange() {
            $("#slidenavErr").css("z-index", "9999");
            var Per = document.getElementById("<%=txtServPer.ClientID%>").value;
            var subTot = document.getElementById("<%=txtSubTotal.ClientID%>").value;
           
            var num = Number(subTot.replace(/[^0-9\.]+/g, ""))
            if (Per <= 100) {

                var tax = ((parseFloat(num)) * (parseFloat(Per) / 100)).toFixed(2);

              
                document.getElementById("<%=txtServTax.ClientID%>").value = parseFloat(tax).toFixed(2);
                var sTax = document.getElementById("<%=txtServTax.ClientID%>").value;
                var sTtl = document.getElementById("<%=txtSubTotal.ClientID%>").value;

                sTax = sTax.split(',').join('');
                sTtl = sTtl.split(',').join('');

                var nTtl = (parseFloat(parseFloat(sTax) + parseFloat(sTtl))).toFixed(2);
                document.getElementById("<%=txtNetAmnt.ClientID%>").value = parseFloat(nTtl).toFixed(2);
                
                
            }
            //txtServTax txtSubTotal = txtNetAmnt
            else {
                
                PassMessageError('Percentage cannot be grater than 100');
            }
        }
    </script>
</asp:Content>
