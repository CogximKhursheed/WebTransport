<%@ Page Title="Designation Master" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="DesignationMaster.aspx.cs" Inherits="WebTransport.DesignationMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-5 center-block">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><i class="animated-icon fas fa-gas-pump"></i>Designation Master </h5>
                            <div class="title-action">
                                <asp:Label ID="lblViewList" runat="server"><a class="btn-action white-o" href="ManageDesignationMaster.aspx"><i class="fa fa-list"></i>List</a></asp:Label>
                            </div>
                        </div>
                        <div class="ibox-content main-form scroll-pane">
                            <div class="col-sm-12 no-pad">
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Designation <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ControlToValidate="txtDesignation" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Enter Designation!" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
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
                    </div>
                </div>

                <div class="col-lg-6">
                    <div id="dvDesigRights" class="web_dialog black12" style="display: none; width: 200%; height: auto;">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="popform_header">User Rights
                                    </h4>
                                </div>
                                <div class="modal-body">
                                    <section class="panel panel-default full_form_container material_search_pop_form">
						<div class="panel-body">
						<div class="clearfix odd_row">	                                
	                    <div class="col-sm-8">
                          <div class="col-sm-12" style="text-align:center;">
	                       <asp:Label ID="lblAlert" runat="server" Text=""></asp:Label>
                        </div>
                    
	                    </div>	                
	                     </div>                      
                        </div>
					    </section>
                                </div>
                                <div class="modal-footer">
                                    <div class="popup_footer_btn">
                                        <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-dark" CausesValidation="true"
                                            OnClick="btnOk_Click" TabIndex="45"><i class="fa fa-check"></i>Ok</asp:LinkButton>
                                        <button type="submit" class="btn btn-dark" data-dismiss="modal">
                                            <i class="fa fa-times"></i>Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--HIDDEN FIELDS--%>
            <asp:HiddenField ID="hiddesignidno" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">
        function HideClient() {
            $("#dvDesigRights").fadeOut(300);
        }
        function ShowDesigRights(SourceID) {
            $("#dvDesigRights").fadeIn(1000);
            return false;
        }
    </script>
</asp:Content>
