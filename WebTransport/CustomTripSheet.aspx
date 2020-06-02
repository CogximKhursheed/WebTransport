<%@ Page Title="Custom Trip Sheet" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="CustomTripSheet.aspx.cs" Inherits="WebTransport.CustomTripSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="row ">
    <div class="">
        <section class="panel panel-default full_form_container quotation_master_form">
            <header class="panel-heading font-bold form_heading">Custom Trip Sheet
                <block class="pull-right">
                    <a style="font-size:15px; color:white;font-weight:bold;" href="ManageCustomTripSheet.aspx"><b>List</b></a>
                    <a id="lnkbtnPrint" style="font-size:15px; color:white;" class="fa fa-print icon" Visible="false" runat="server" title="Print"></a>
                </block>
            </header>
            <div class="panel-body">
                <form class="bs-example form-horizontal">
                    <!-- first  section -->
                    <div class="clearfix first_section">
                        <section class="panel panel-in-default">
                            <div class="panel-body alternate-rows">
                                <div class="clearfix">
                                    <div class="col-sm-4">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Date Range<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged">
                                            </asp:DropDownList>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                        ControlToValidate="ddlDateRange" ValidationGroup="save" ErrorMessage="Please Select Date Range."
                                        InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Date<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                           <asp:TextBox ID="txtTripDate" runat="server" PlaceHolder="DD-MM-YYYY" CssClass="input-sm datepicker form-control"  onkeydown="return DateFormat(this, event.keyCode)" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-sm-3">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Branch Location<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:DropDownList ID="ddlCompFromCity" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCompFromCity_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                    <label class="col-sm-12 control-label style-label-1 style-label-1">Pref No.</label>
                                    <div class="col-sm-12">
                                    <asp:TextBox ID="txtPref"  runat="server" CssClass="form-control" ToolTip="Pref." ></asp:TextBox>
                                    </div>
                                    </div>
                                     <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Trip No.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                           <asp:TextBox ID="txtTripNo"  runat="server" CssClass="form-control" Style="text-align: right;" ToolTip="Trip Number" Enabled="true" ></asp:TextBox>
                                        </div>
                                    </div>
                                     </div>
                                <div class="clearfix">
                                    <div class="col-sm-4">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Vehicle No.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:DropDownList style="width:100%" ID="ddlTruckNo" CssClass="chzn-select form-control" runat="server" AutoPostBack="false">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Lane<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:DropDownList ID="ddlLane" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Party Name<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:DropDownList ID="ddlSender" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix">
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Driver<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtDriverName" runat="server" CssClass="input-sm form-control"  ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Driver No.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtdriverno" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-sm-4">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Vehicle Size<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtVehicleSize" runat="server" CssClass="input-sm form-control" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="col-sm-4">
                                            <label class="col-sm-12 control-label style-label-1 style-label-1">Start KMS<span class="required-field">*</span></label>
                                            <div class="col-sm-12">
                                                <asp:TextBox ID="txkStartKms"  placeholder="Start Kms" runat="server" CssClass="input-sm form-control"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <label class="col-sm-12 control-label style-label-1 style-label-1">End KMS<span class="required-field">*</span></label>
                                            <div class="col-sm-12">
                                                <asp:TextBox ID="txkEndKms"  placeholder="End Kms" runat="server"  OnTextChanged="txkEndKms_TextChanged" CssClass="input-sm form-control"  ClientIDMode="Static" AutoPostBack="true" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <label class="col-sm-12 control-label style-label-1 style-label-1">Total KMS<span class="required-field">*</span></label>
                                            <div class="col-sm-12">
                                                <asp:TextBox ID="txtTotalKms" placeholder="Total Kms" runat="server"  CssClass="input-sm form-control"  ClientIDMode="Static" Style="text-align: right;"  Enabled=false></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix">
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">DSL Qty.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtdslqty" runat="server" CssClass="input-sm form-control"  ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">DSL Rate<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtdslrate" runat="server" CssClass="input-sm form-control" OnTextChanged="txtdslrate_TextChanged"  ClientIDMode="Static"
                                            oncopy="return false" AutoPostBack="true"
                                        onpaste="return false" oncut="return false" oncontextmenu="return false" onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">DSL Amt.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtdslamt" runat="server" CssClass="input-sm form-control"  ClientIDMode="Static"  Text="0.00" Style="text-align: right;" Enabled=false
                                       onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Cash<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtcash" runat="server" CssClass="input-sm form-control"  ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">DSL Card Amt.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtdslcardamt" runat="server" CssClass="input-sm form-control"  ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">DSL Card Number<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtdslcardrate" runat="server" CssClass="input-sm form-control"  ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix">
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">DSL Card Name<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtdslcardltr" runat="server" CssClass="input-sm form-control"  ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Toll<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txttoll" runat="server" CssClass="input-sm form-control"  ClientIDMode="Static" OnTextChanged="txttoll_TextChanged"  Text="0.00" Style="text-align: right;" AutoPostBack=true
                                    onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Wages<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtwages" runat="server" CssClass="input-sm form-control"  ClientIDMode="Static" OnTextChanged="txtwages_TextChanged"  Text="0.00" Style="text-align: right;" AutoPostBack=true
                                    onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Food Exp.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtfoodexp" runat="server" CssClass="input-sm form-control"  ClientIDMode="Static" OnTextChanged="txtfoodexp_TextChanged"  Text="0.00" Style="text-align: right;" AutoPostBack=true
                                    onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Repair<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtrepair" runat="server" CssClass="input-sm form-control"  ClientIDMode="Static" OnTextChanged="txtrepair_TextChanged"  Text="0.00" Style="text-align: right;" AutoPostBack=true
                                    onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Ex. DSL Amt.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtexdslamt" runat="server" CssClass="input-sm form-control"  ClientIDMode="Static" Text="0.00" Style="text-align: right;" Enabled=false
                                       onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix">
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Ex DSL Ltr.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtexdslltr" runat="server" CssClass="input-sm form-control"  ClientIDMode="Static" Text="0.00" Style="text-align: right;" Enabled=false
                                       onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Total DSL Qty.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txttotaldslqty" runat="server" CssClass="input-sm form-control"  ClientIDMode="Static" Text="0.00" Style="text-align: right;" Enabled=false
                                       onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Total DSL Amt.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txttotaldslamt" runat="server" CssClass="input-sm form-control"  ClientIDMode="Static" Text="0.00" Style="text-align: right;" Enabled=false
                                       onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Milage<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtmilage" runat="server" CssClass="input-sm form-control"  ClientIDMode="Static" Text="0.00" Style="text-align: right;" Enabled=false
                                       onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Adv. in Driver<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtadvdriver" runat="server" CssClass="input-sm form-control" OnTextChanged="txtadvdriver_TextChanged"  ClientIDMode="Static" AutoPostBack=true></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Other<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtother" runat="server" CssClass="input-sm form-control" OnTextChanged="txtother_TextChanged"   ClientIDMode="Static" AutoPostBack=true></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix">
                                    <div class="col-sm-10">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Remark<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtremark" runat="server" CssClass="input-sm form-control"  ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Total Amt.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txttotalamt" runat="server" CssClass="input-sm form-control"  ClientIDMode="Static" Text="0.00" Style="text-align: right;" Enabled=false
                                       onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>
                    <div class="col-sm-12">
                     
                        <div class="col-sm-2 pull-right">
                            <asp:LinkButton ID="lnkbtnCancel" runat="server" CssClass="btn full_width_btn btn-primary" ToolTip="Click to Submit" CausesValidation="true" ValidationGroup="Submit">Cancel</asp:LinkButton>
                        </div>
                        <div class="col-sm-2 pull-right">
                            <asp:LinkButton ID="lnkbtnSubmit" runat="server" OnClick="lnkbtnSubmit_OnClick" CssClass="btn full_width_btn btn-primary" ToolTip="Click to Submit" CausesValidation="true" ValidationGroup="Submit">Save</asp:LinkButton>
                        </div>
                        <div class="col-sm-2 pull-right">
                            <asp:LinkButton ID="lnkbtnNext" runat="server" Visible="false" CssClass="btn full_width_btn btn-primary" OnClick="lnkbtnNext_OnClick" ToolTip="Next Record" CausesValidation="true">Next</asp:LinkButton>
                        </div>
                    </div>
                </form>
            </div>
        </section>
    </div>
</div>
  <!--HIDDEN FIELDS-->
    <asp:HiddenField ID="hidmindate" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />
     <asp:HiddenField ID="hidtripid" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
</asp:Content>
