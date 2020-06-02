<%@ Page Title="Item Master" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ItemMast.aspx.cs" Inherits="WebTransport.ItemMast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>--%>
    <div class="row ">
        <div class="col-lg-5 center-block">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5><i class="animated-icon fas fa-gas-pump"></i>Item Master </h5>
                    <div class="title-action">
                        <asp:Label ID="lblViewList" runat="server"><a class="btn-action white-o" href="ManageItemMast.aspx"><i class="fa fa-list"></i>List</a></asp:Label>
                    </div>
                </div>
                <div class="ibox-content main-form scroll-pane">
                    <div class="col-sm-12 no-pad">
                        <div class="form-control-row">
                            <div class="control-container col-sm-6">
                                <label class="control-label">Item Name <span class="required-field">*</span> </label>
                                <asp:TextBox ID="txtItemName" runat="server" class="form-control" MaxLength="50" onDrop="blur();return false;" onpaste="return false" oncut="return false" placeholder="Please enter ItemName" oncopy="return false" OnTextChanged="txtItemName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <span class="red" style="color: #ff0000">
                                    <asp:RequiredFieldValidator ID="rfvItemm" runat="server" ControlToValidate="txtItemName" ValidationGroup="ItemSave" ErrorMessage="Please Enter Item Name!" CssClass="classValidation" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                            <div class="control-container col-sm-6">
                                <label class="control-label">Item Name[Hindi]</label>
                                <asp:TextBox ID="txtItemNameHindi" runat="server" class="form-control" MaxLength="100" Style="font-family: kruti dev,arial,verdana;" onDrop="blur();return false;" placeholder="Please Enter ItemName(Hindi)!" onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-control-row">
                            <div class="control-container col-sm-6">
                                <label class="control-label">Description</label>
                                <asp:TextBox ID="txtDescription" runat="server" class="form-control" MaxLength="100" onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false" placeholder="Please enter Description"></asp:TextBox>
                            </div>
                            <div class="control-container col-sm-5">
                                <label class="control-label">Item Group <span class="required-field">*</span></label>
                                <asp:DropDownList ID="ddlGroupType" runat="server" CssClass="form-control required" AutoPostBack="true" OnSelectedIndexChanged="ddlGroupType_SelectedIndexChanged"></asp:DropDownList>
                                <span class="red" style="color: #ff0000">
                                    <asp:RequiredFieldValidator ID="rfvIGMTypee" runat="server" ControlToValidate="ddlGroupType" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select Item Group!" SetFocusOnError="true" ValidationGroup="ItemSave"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                            <div class="control-container col-sm-1">
                                <label class="control-label">&nbsp;</label>
                                <asp:LinkButton ID="lnkitemgrpSave" runat="server" class="btn btn-sm btn-primary acc_home" ToolTip="Add Item Group" data-toggle="modal" data-target="#dvitmgrp"><img src="Images/plus.gif" style="width:15px;" /></asp:LinkButton>
                                <div id="dvitmgrp" class="modal fade">
                                    <div class="modal-dialog">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <%--<h4 class="popform_header">ADD ITEM GROUP
                                                </h4>--%>
                                                <div class="ibox-title">
                                                    <h4><i class="animated-icon fas fa-gas-pump"></i>Add Item Group </h4>
                                                </div>
                                            </div>
                                            <div class="modal-body">
                                                <section class="panel panel-default full_form_container material_search_pop_form">
				                                            <div class="panel-body">
                                                                <div class="ibox-content main-form scroll-pane">
                                                                    <div class="col-sm-12 no-pad">
                                                                        <div class="form-control-row">
                                                                            <div class="control-container col-sm-12">
                                                                                <label class="control-label">Item Group Type <span class="required-field">*</span> </label>
                                                                                <asp:DropDownList ID="ddlItemGropForPopup" CssClass="form-control required" runat="server"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-control-row">
                                                                            <div class="control-container col-sm-12">
                                                                                <label class="control-label">Group Name <span class="required-field">*</span> </label>
                                                                                <asp:TextBox ID="txtGroupNameForPopup" runat="server" CssClass="form-control" MaxLength="50" ></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtGroupNameForPopup" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Enter Group Name!" SetFocusOnError="true" ValidationGroup="DIVSave"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>         
                                                             </div>
				                                            </section>
                                            </div>
                                            <div class="modal-footer">
                                                <div class="popup_footer_btn">
                                                    <div class="col-lg-12">

                                                        <div class="col-lg-3">
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:LinkButton ID="lnkbtnSaveGroup" runat="server" Text="Save" CssClass="btn btn-dark" OnClick="lnkbtnSaveGroup_OnClick" CausesValidation="true" ValidationGroup="DIVSave"></asp:LinkButton>

                                                        </div>
                                                        <div class="col-sm-3">
                                                            <button type="submit" tabindex="36" class="btn btn-dark" data-dismiss="modal">
                                                                <i class="fa fa-times"></i>Close</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-control-row">
                            <div class="control-container col-sm-6">
                                <label class="control-label">Item Type</label>
                                <asp:DropDownList ID="ddlAddType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAddType_OnSelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="control-container col-sm-3">
                                <label class="control-label">Item MRP</label>
                                <asp:TextBox ID="txtitemmrp" runat="server" class="form-control" Style="text-align: right;" onKeyPress="return checkfloat(event, this);" Text="0.00" MaxLength="10" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" placeholder="Please Enter Item MRP!"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtitemmrp" ValidationGroup="ItemSave" ErrorMessage="Please Enter Item MRP!" CssClass="classValidation" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="control-container col-sm-3">
                                <%--<asp:Label ID="lblPur" class="col-sm-6 control-label" runat="server" Text="Purchase Rate" Font-Bold="true"></asp:Label>--%>
                                <asp:Label ID="lblPur" class="control-label" runat="server" Text="Purchase Rate" Font-Bold="true"></asp:Label>
                                <label class="control-label">&nbsp;</label>
                                <asp:TextBox ID="TxtPurchaseRate" runat="server" class="form-control" Style="text-align: right;" onKeyPress="return checkfloat(event, this);" Text="0.00" MaxLength="10" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" placeholder="Please Enter Purchase Rate!"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPurRate" runat="server" ControlToValidate="TxtPurchaseRate" ValidationGroup="ItemSave" ErrorMessage="Please Enter Purchase Rate!" CssClass="classValidation" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-control-row">
                            <div class="control-container col-sm-2">
                                <label class="control-label">SGST <span class="required-field">*</span> </label>
                                <asp:TextBox ID="txtsgst" runat="server" CssClass="form-control" Text="0.00" placeholder="Enter SGST" Style="text-align: right;" onKeyPress="javascript:GetAlert(this);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtsgst" ValidationGroup="ItemSave" ErrorMessage="Please Enter SGST!" CssClass="classValidation" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="control-container col-sm-2">
                                <label class="control-label">CGST <span class="required-field">*</span> </label>
                                <asp:TextBox ID="txtcgst" runat="server" CssClass="form-control" Text="0.00" placeholder="Enter CGST" Style="text-align: right;" onKeyPress="javascript:GetAlert(this);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtcgst" ValidationGroup="ItemSave" ErrorMessage="Please Enter CGST!" CssClass="classValidation" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="control-container col-sm-2">
                                <label class="control-label">IGST <span class="required-field">*</span> </label>
                                <asp:TextBox ID="txtigst" runat="server" CssClass="form-control" Text="0.00" placeholder="Enter IGST" Style="text-align: right;" onKeyPress="javascript:GetAlert(this);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtigst" ValidationGroup="ItemSave" ErrorMessage="Please Enter IGST!" CssClass="classValidation" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="control-container col-sm-3">
                                <label class="control-label">VAT Tax(%) </label>
                                <asp:TextBox ID="txtVAT" runat="server" CssClass="form-control" MaxLength="5" Text="0.00" placeholder="Enter VAT Tax" Style="text-align: right;" onKeyPress="javascript:GetAlert(this);" AutoPostBack="True" OnTextChanged="txtVAT_TextChanged"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvVAT" runat="server" ControlToValidate="txtVAT" ValidationGroup="ItemSave" ErrorMessage="Please Enter VAT Tax!" CssClass="classValidation" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="control-container col-sm-3">
                                <label class="control-label">CST Tax(%) </label>
                                <asp:TextBox ID="txtCST" runat="server" CssClass="form-control" MaxLength="5" onKeyPress="return checkfloat(event, this);" Text="0.00" oncopy="return false" oncut="return false" Style="text-align: right;" onDrop="blur();return false;" AutoPostBack="true" onpaste="return false" placeholder="Please enter CST Tax" OnTextChanged="txtCST_TextChanged"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCST" runat="server" ControlToValidate="txtCST" ValidationGroup="ItemSave" ErrorMessage="Please Enter CST Tax!" CssClass="classValidation" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-control-row">
                            <div class="control-container col-sm-5">
                                <label class="control-label">Item Unit <span class="required-field">*</span> </label>
                                <asp:DropDownList ID="ddlItemUnit" runat="server" CssClass="form-control required"></asp:DropDownList>
                                <span class="red" style="color: #ff0000">
                                    <asp:RequiredFieldValidator ID="rfvitemunit" runat="server" ControlToValidate="ddlItemUnit" ValidationGroup="ItemSave" ErrorMessage="Please Select Unit!" CssClass="classValidation" SetFocusOnError="true" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                            <div class="control-container col-sm-1">
                                <label class="control-label">&nbsp;</label>
                                <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-sm btn-primary acc_home" ToolTip="Add Item Unit" data-toggle="modal" data-target="#dvitemUnitPopup"><img src="Images/plus.gif" style="width:15px;" /></asp:LinkButton>
                                <div id="dvitemUnitPopup" class="modal fade">
                                    <div class="modal-dialog">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <%--<h4 class="popform_header">ADD ITEM Unit
                                                </h4>--%>
                                                <div class="ibox-title">
                                                    <h4><i class="animated-icon fas fa-gas-pump"></i>Add Item Unit </h4>
                                                </div>
                                            </div>
                                            <div class="modal-body">
                                                <section class="panel panel-default full_form_container material_search_pop_form">
				                                            <div class="panel-body">
                                                                <div class="ibox-content main-form scroll-pane">
                                                                    <div class="col-sm-12 no-pad">
                                                                        <div class="form-control-row">
                                                                            <div class="control-container col-sm-12">
                                                                                <label class="control-label">UOM Name <span class="required-field">*</span> </label>
                                                                                <asp:TextBox ID="txtUOMName" runat="server" CssClass="form-control" MaxLength="30" onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false" ></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rfvUOMName" runat="server" ControlToValidate="txtUOMName" CssClass="classValidation" ValidationGroup="Uomgrp" ErrorMessage="Please Enter UOM Name"  SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>	                               
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>              
                                                             </div>
				                                       </section>
                                            </div>
                                            <div class="modal-footer">
                                                <div class="popup_footer_btn">
                                                    <div class="col-lg-12">

                                                        <div class="col-lg-3">
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:LinkButton ID="lnkbtnUom" runat="server" Text="Save" CssClass="btn btn-dark" OnClick="lnkbtnUom_OnClick" CausesValidation="true" ValidationGroup="Uomgrp"></asp:LinkButton>

                                                        </div>
                                                        <div class="col-sm-3">
                                                            <button type="submit" tabindex="36" class="btn btn-dark" data-dismiss="modal">
                                                                <i class="fa fa-times"></i>Close</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="control-container col-sm-1">
                            </div>
                            <div class="control-container col-sm-5">
                                <label class="control-label">Tyre Type <span class="required-field">*</span></label>
                                <asp:DropDownList ID="ddlTyreType" runat="server" CssClass="form-control required"></asp:DropDownList>
                                <span class="red" style="color: #ff0000">
                                    <asp:RequiredFieldValidator ID="rfvtyre" runat="server" ControlToValidate="ddlTyreType" CssClass="classValidation" SetFocusOnError="true" Display="Dynamic" ErrorMessage="Please enter tyre type!" ValidationGroup="ItemSave" Enabled="true" InitialValue="0"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                        </div>
                        <div class="form-control-row">
                            <div class="control-container col-sm-6">
                                <label class="control-label">Company Name </label>
                                <asp:TextBox ID="txtCompanyName" PlaceHolder="Company Name" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                            </div>
                            <div class="control-container col-sm-6">
                                <label class="control-label">Model </label>
                                <asp:TextBox ID="txtModelName" PlaceHolder="Model" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-control-row">
                            <div class="control-container col-sm-12">
                                <div class="input-container checkbox-container">
                                    <label class="control-label">Active</label>
                                    <asp:CheckBox ID="chkStatus" runat="server" Style="text-align: left;" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="ibox-btn">
                    <div class="pull-right">
                        <div class="display-inline">
                            <asp:LinkButton ID="lnkbtnNew" runat="server" OnClick="lnkbtnNew_OnClick" CssClass="btn btn-labeled btn-success"><span class="btn-label"><i class="fa fa-plus"></i></span>New</asp:LinkButton>
                        </div>
                        <div class="display-inline">
                            <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" ValidationGroup="ItemSave" OnClick="lnkbtnSave_OnClick" CssClass="btn btn-labeled btn-primary"><span class="btn-label"><i class="fa fa-save"></i></span>Save</asp:LinkButton>
                        </div>
                        <div class="display-inline">
                            <asp:LinkButton ID="lnkbtnCancel" runat="server" OnClick="lnkbtnCancel_OnClick" CssClass="btn btn-labeled btn-danger"><span class="btn-label"><i class="fa fa-times"></i></span>Cancel</asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <label class="col-sm-6 control-label" style="font">Update VAT</label>
                        <div class="col-sm-3" style="vertical-align: top">
                            <asp:LinkButton ID="lnkbtnUpdate" runat="server" class="btn btn-sm btn-primary acc_home" ToolTip="Update VAT and CST By Group Wise"
                                data-toggle="modal" data-target="#dvitmgrps"><img src="Images/plus.gif" style="width:15px;" /></asp:LinkButton>
                            <div id="dvitmgrps" class="modal fade">
                                <div class="modal-dialog" style="width: 30%">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <%-- <h4 class="popform_header">UPDATE VAT/CST :
                                            </h4>--%>
                                            <div class="ibox-title">
                                                <h4><i class="animated-icon fas fa-gas-pump"></i>UPDATE VAT/CST :</h4>
                                            </div>
                                        </div>
                                        <div class="modal-body">
                                            <section class="panel panel-default full_form_container material_search_pop_form">
				                                                         <div class="panel-body">
                                                                             <div class="ibox-content main-form scroll-pane">
                                                                                 <div class="col-sm-12 no-pad">
                                                                                     <div class="form-control-row">
                                                                                         <div class="control-container col-sm-12">
                                                                                             <label class="control-label">Group Type <span class="required-field">*</span> </label>
                                                                                             <asp:DropDownList ID="ddlItemGroupForPopup" CssClass="form-control required" runat="server"></asp:DropDownList>
                                                                                             <asp:RequiredFieldValidator ID="rfvItemGropForPopup" runat="server" ControlToValidate="ddlItemGropForPopup" InitialValue="0" CssClass="classValidation" Display="Dynamic" ErrorMessage="Select Group Type!" SetFocusOnError="true" ValidationGroup="DIVUpdate"></asp:RequiredFieldValidator>
                                                                                         </div>
                                                                                     </div>
                                                                                     <div class="form-control-row">
                                                                                         <div class="control-container col-sm-12">
                                                                                             <label class="control-label">Type <span class="required-field">*</span> </label>
                                                                                               <asp:DropDownList ID="ddlTaxType" CssClass="form-control required" runat="server">
                                                                                        <asp:ListItem Text="VAT" Value="1"></asp:ListItem>
                                                                                        <asp:ListItem Text="CST" Value="2"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                         </div>
                                                                                     </div>
                                                                                     <div class="form-control-row">
                                                                                         <div class="control-container col-sm-12">
                                                                                             <label class="control-label">Percentage <span class="required-field">*</span> </label>
                                                                                              <asp:TextBox ID="txtPercentage" runat="server" CssClass="form-control" MaxLength="50" ></asp:TextBox>
                                                                                             <asp:RequiredFieldValidator ID="refPercentage" Text="0.00" runat="server" ControlToValidate="txtPercentage" CssClass="classValidation" Display="Dynamic" ErrorMessage="Percentage Required!" SetFocusOnError="true" ValidationGroup="DIVUpdate"></asp:RequiredFieldValidator>
                                                                                         </div>
                                                                                     </div>
                                                                                 </div>
                                                                             </div>
                                                                           </div>
				                                                        </section>
                                        </div>
                                        <div class="modal-footer">
                                            <div class="popup_footer_btn">
                                                <div class="col-lg-12">

                                                    <div class="col-lg-3">
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <%-- <asp:LinkButton ID=""  runat="server" 
                                                                                    CssClass="btn btn-dark"   CausesValidation="true" ValidationGroup="DIVSave" 
                                                                                    onclick="" ></asp:LinkButton>
                                                                                    <asp:LinkButton ID="lnkbtnSaveGrop" runat="server" Text="Save"  CssClass="btn btn-dark" onclick="lnkbtnSaveGrop_Click">LinkButton</asp:LinkButton>--%>
                                                        <asp:LinkButton ID="lnkbtnSaveGrop" Text="Save"
                                                            ValidationGroup="DIVUpdate" CssClass="btn btn-dark" CausesValidation="true"
                                                            runat="server" OnClick="lnkbtnSaveGrop_Click"></asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <button type="submit" tabindex="36" class="btn btn-dark" data-dismiss="modal">
                                                            <i class="fa fa-times"></i>Close</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="dvAccount" class="web_dialog black12" style="display: none; width: 500px; height: auto;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="popform_header">Add Group
                        </h4>
                    </div>
                    <div class="modal-body">
                        <section class="panel panel-default full_form_container material_search_pop_form">
                                <div class="panel-body">
                                <div class="clearfix odd_row  ">
									<label class="col-sm-3 control-label">Item Group Type<span class="required-field">*</span></label>
									<div class="col-sm-9">
                                      <asp:DropDownList ID="ddlItemType" CssClass="form-control" runat="server"></asp:DropDownList>
									</div>
								</div>	 
								<div class="clearfix odd_row  ">
									<label class="col-sm-3 control-label">Group Name <span class="required-field">*</span></label>
									<div class="col-sm-9">
                                     <asp:TextBox ID="txtGName" runat="server" CssClass="form-control" MaxLength="50" TabIndex="2" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvGName" runat="server" ControlToValidate="txtGName" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Enter Group Name!" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
									</div>
								</div>                  
                                </div>
                                </section>
                    </div>
                    <div class="modal-footer">
                        <div class="popup_footer_btn">
                            <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-dark" CausesValidation="true"
                                OnClick="imgBtnSave_Click" ValidationGroup="SaveClient"><i class="fa fa-check"></i>Ok</asp:LinkButton>
                            <button type="submit" class="btn btn-dark" data-dismiss="modal">
                                <i class="fa fa-times"></i>Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--HIDDEN FIELDS--%>
    <asp:HiddenField ID="hidItemidno" runat="server" />
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>

    <script language="javascript" type="text/javascript">
        SetFocus();
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

        $(document).ready(function () {
            setDatecontrol();
        });

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

        $("#txtVAT").keydown(function () {
            GetAlert();
        });

        function GetAlert() {
            var TestVar = document.getElementById('txtVAT').value;
            alert(TestVar);
        }
    </script>
</asp:Content>
