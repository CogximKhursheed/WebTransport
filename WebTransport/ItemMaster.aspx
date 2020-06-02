<%@ Page Title="Item Master" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="ItemMaster.aspx.cs" Inherits="WebTransport.ItemMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-5 center-block">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><i class="animated-icon fas fa-gas-pump"></i>Item Master </h5>
                            <div class="title-action">
                                <asp:Label ID="lblViewList" runat="server"><a class="btn-action white-o" href="ManageItemMaster.aspx"><i class="fa fa-list"></i>List</a></asp:Label>
                            </div>
                        </div>
                        <div class="ibox-content main-form scroll-pane">
                            <div class="col-sm-12 no-pad">
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Item Name <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txtItemName" ClientIDMode="Static" onkeyup="javascript:Convert()" runat="server" CssClass="form-control" MaxLength="50" onDrop="blur();return false;" AutoComplete="off" onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvItemNamee" runat="server" ControlToValidate="txtItemName" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Enter Item Name !" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Item Name [Hindi] </label>
                                        <asp:TextBox ID="txtItemNameHindi" runat="server" CssClass="form-control" MaxLength="100" onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Description </label>
                                        <asp:TextBox ID="txtDescription" ClientIDMode="Static" runat="server" CssClass="form-control" MaxLength="100" onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">HSNSAC No </label>
                                        <asp:TextBox ID="txtHSNSAC" ClientIDMode="Static" runat="server" CssClass="form-control" MaxLength="8" onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-11">
                                        <label class="control-label">Item Group <span class="required-field">*</span> </label>
                                        <asp:DropDownList ID="ddlGroupType" CssClass="form-control required" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvIGMType" runat="server" ControlToValidate="ddlGroupType" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select Item Group !" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="control-container col-sm-1">
                                        <label class="control-label">&nbsp;</label>
                                        <asp:LinkButton ID="lnkitemgrpSave" runat="server" class="btn btn-sm btn-primary acc_home" ToolTip="Add Item Group" data-toggle="modal" data-target="#dvitmgrp"><img src="Images/plus.gif" style="width:15px;" /></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-11">
                                        <label class="control-label">Item Unit <span class="required-field">*</span> </label>
                                        <asp:DropDownList ID="ddlItemUnit" CssClass="form-control required" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlItemUnit" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select Unit !" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="control-container col-sm-1">
                                        <label class="control-label">&nbsp;</label>
                                        <asp:LinkButton ID="lnkUOM" runat="server" class="btn btn-sm btn-primary acc_home" ToolTip="Add UOM" data-toggle="modal" data-target="#dvuom" TabIndex="7"><img src="Images/plus.gif" style="width:15px;" /></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <div class="input-container checkbox-container">
                                            <label class="control-label">Active</label>
                                            <asp:CheckBox ID="chkStatus" runat="server" />
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
                                    <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" ValidationGroup="Save" OnClick="lnkbtnSave_OnClick" CssClass="btn btn-labeled btn-primary"><span class="btn-label"><i class="fa fa-save"></i></span>Save</asp:LinkButton>
                                </div>
                                <div class="display-inline">
                                    <asp:LinkButton ID="lnkbtnCancel" runat="server" OnClick="lnkbtnCancel_OnClick" CssClass="btn btn-labeled btn-danger"><span class="btn-label"><i class="fa fa-times"></i></span>Cancel</asp:LinkButton>
                                </div>
                            </div>
                        </div>


                        <%--Item Group * Pop_Up--%>
                        <div id="dvitmgrp" class="modal fade">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <%-- <h4 class="popform_header">ADD ITEM GROUP
                                        </h4>--%>
                                        <div class="ibox-title">
                                            <h4><i class="animated-icon fas fa-gas-pump"></i>ADD ITEM GROUP </h4>
                                        </div>
                                    </div>
                                    <div class="modal-body">
                                        <section class="panel panel-default full_form_container material_search_pop_form">
						<div class="panel-body">
                            <div class="ibox-content main-form scroll-pane">
                                <div class="col-sm-12 no-pad">
                                     <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Group Name <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txtGName" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="50" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvGName" runat="server" ControlToValidate="txtGName" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Group Name" SetFocusOnError="true" ValidationGroup="DIVSave"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                    <div class="form-control-row">
                                        <div class="control-container col-sm-12">
                                        <div class="input-container checkbox-container">
                                            <label class="control-label">Active</label>
                                            <asp:CheckBox ID="chkactive" Checked="true" runat="server" />
                                        </div>
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
                                                    <asp:LinkButton ID="lnkgrpsave" runat="server" CausesValidation="true" ValidationGroup="DIVSave" CssClass="btn btn-dark" OnClick="lnkgrpsave_Click"><i class="fa fa-check"></i>Ok</asp:LinkButton>
                                                </div>
                                                <div class="col-sm-3">
                                                    <button type="submit" class="btn btn-dark" data-dismiss="modal"><i class="fa fa-times"></i>Close</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <%--Item Unit * Pop_Up--%>
                        <div id="dvuom" class="modal fade">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <%-- <h4 class="popform_header">ADD UOM
                                        </h4>--%>
                                        <div class="ibox-title">
                                            <h4><i class="animated-icon fas fa-gas-pump"></i>ADD UOM </h4>
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
                                        <asp:TextBox ID="txtUOMNameu" runat="server" AutoComplete="off" ClientIDMode="Static" onkeyup="javascript:Convertu()" CssClass="form-control" MaxLength="10" onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvUOMNameu" runat="server" ControlToValidate="txtUOMNameu" CssClass="classValidation" ValidationGroup="DivUOM" ErrorMessage="Please enter UOM Name!"  SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>	                               
                                    </div>
                                </div>
                                     <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">UOM Name[Hindi]</label>
                                        <asp:TextBox ID="txtNameHindiu" runat="server" CssClass="form-control" MaxLength="50" onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false" ></asp:TextBox>
                                    </div>
                                </div>
                                     <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Description </label>
                                        <asp:TextBox ID="txtDescriptionu" runat="server"  ClientIDMode="Static" CssClass="form-control" MaxLength="50"  onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox>                    
                                    </div>
                                </div>
                                     <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <div class="input-container checkbox-container">
                                            <label class="control-label">Active</label>
                                            <asp:CheckBox ID="chkStatusu"  Checked="true" runat="server" />                      
                                        </div>
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
                                                    <asp:LinkButton ID="lnkuomsave" runat="server" CausesValidation="true" ValidationGroup="DivUOM" CssClass="btn btn-dark" OnClick="lnkuomsave_Click"><i class="fa fa-check"></i>Ok</asp:LinkButton>
                                                </div>
                                                <div class="col-sm-3">
                                                    <button type="submit" class="btn btn-dark" data-dismiss="modal"><i class="fa fa-times"></i>Close</button>
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
            <%--HIDDEN FIELDS--%>
            <asp:HiddenField ID="hidIGrpMastidno" runat="server" />
            <asp:HiddenField ID="hidItemidno" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">
        <script src="/js/jquery-1.12.0.min.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

            function Convert() {
            var item = jQuery("#txtItemName").val();

        jQuery("#txtDescription").val(item);
    }
        function Convertu() {
            var uom = jQuery("#txtUOMNameu").val();

        jQuery("#txtDescriptionu").val(uom);
    }
  
    </script>

    <script language="javascript" type="text/javascript">

              function HideClient() {
                $("#dvitmgrp").fadeOut(300);
}
              function ShowClient() {
                $("#dvitmgrp").fadeIn(1000);

}
    </script>
</asp:Content>
