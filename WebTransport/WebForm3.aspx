<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="WebTransport.WebForm3" %>

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
                    <a style="font-size:15px; color:white;font-weight:bold;" href="ManageManulTripSheet.aspx"><b>List</b></a>
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
                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="col-sm-6">
                                            <label class="col-sm-12 control-label style-label-1 style-label-1">Date<span class="required-field">*</span></label>
                                            <asp:TextBox ID="txtTripDate" runat="server" PlaceHolder="DD-MM-YYYY" CssClass="input-sm datepicker form-control" MaxLength="10" onkeydown="return DateFormat(this, event.keyCode)" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                            <label class="col-sm-12 control-label style-label-1 style-label-1">Trip No.<span class="required-field">*</span></label>
                                            <asp:TextBox ID="txtTripNo" runat="server" CssClass="form-control" Style="text-align: right;" ToolTip="Trip Number" Enabled="true" MaxLength="9"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Branch Location<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:DropDownList ID="ddlCompFromCity" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
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
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>LUCKNOW-DEORIA-SALIMPUR</asp:ListItem>
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
                                            <asp:TextBox ID="txtDriverName" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Driver No.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="TextBox8" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-sm-4">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Vehicle Size<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtVehicleSize" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="col-sm-4">
                                            <label class="col-sm-12 control-label style-label-1 style-label-1">Start KMS<span class="required-field">*</span></label>
                                            <div class="col-sm-12">
                                                <asp:TextBox ID="txkStartKms" placeholder="Start Kms" onKeyPress="return checkfloat(event, this);" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <label class="col-sm-12 control-label style-label-1 style-label-1">End KMS<span class="required-field">*</span></label>
                                            <div class="col-sm-12">
                                                <asp:TextBox ID="txkEndKms" placeholder="End Kms" onKeyPress="return checkfloat(event, this);" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <label class="col-sm-12 control-label style-label-1 style-label-1">Total KMS<span class="required-field">*</span></label>
                                            <div class="col-sm-12">
                                                <asp:TextBox ID="tctTotalKms" placeholder="Total Kms" onKeyPress="return checkfloat(event, this);" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix">
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">DSL Qty.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="TextBox9" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">DSL Rate<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="TextBox10" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">DSL Amt.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="TextBox11" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Cash<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="TextBox12" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">DSL Card Amt.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="TextBox13" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">DSL Card Rate<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="TextBox14" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix">
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">DSL Card Ltr.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="TextBox15" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Toll<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="TextBox16" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Dihadi<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="TextBox17" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Food Exp.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="TextBox18" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Repair<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="TextBox19" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Ex. DSL Amt.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="TextBox20" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix">
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Ex DSL Ltr.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="TextBox27" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Total DSL Qty.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="TextBox21" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Total DSL Amt.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="TextBox22" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Milage<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="TextBox23" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Adv. in Driver<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="TextBox24" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Other<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="TextBox25" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix">
                                    <div class="col-sm-10">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Remark<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="TextBox26" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="col-sm-12 control-label style-label-1 style-label-1">Total Amt.<span class="required-field">*</span></label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="TextBox28" runat="server" CssClass="input-sm form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>
                    <div class="col-sm-12">
                        <div class="col-sm-2 pull-right">
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn full_width_btn btn-primary" ToolTip="Click to Submit" CausesValidation="true" ValidationGroup="Submit">Cancel</asp:LinkButton>
                        </div>
                        <div class="col-sm-2 pull-right">
                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn full_width_btn btn-primary" ToolTip="Click to Submit" CausesValidation="true" ValidationGroup="Submit">Save</asp:LinkButton>
                        </div>
                    </div>
                </form>
            </div>
        </section>
    </div>
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
</asp:Content>
