<%@ Page Title="Account Link" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="FrmAcntLink.aspx.cs" Inherits="WebTransport.FrmAcntLink" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
        .panel-header-box
        {
            padding-top: 7px;
            margin-top: 7px;
            position: relative;
        }
        .panel-header
        {
            position: absolute;
            top: -18px;
            left: 10px;
            background: white;
            font-weight: bold;
            font-size: 14px;
        }
        #dvAccount
        {
            position:fixed;
            top:0;
            left:35%;
        }
        .fadeout-back
        {
            display:none;
            background:#00000080;
            position:fixed;
            height:100%;
            width:100%;
            top:0;
            left:0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-1">
                </div>
                <div class="col-lg-10">
                    <section class="panel panel-default full_form_container part_purchase_bill_form">
	                  <header class="panel-heading font-bold form_heading">ACCOUNT LINK
	                  </header>
	                  <div class="panel-body">
	                    <form class="bs-example form-horizontal">
	                      <!-- first  section --> 
	                      <div class="clearfix first_section">
	                        <section class="panel panel-in-default">
	                          <div class="panel-body">
	                          	<div class="clearfix odd_row">
	                              <div class="col-sm-6">
	                                <label class="col-sm-4 control-label">Group Type<span class="required-field">*</span></label>
	                                <div class="col-sm-6">
                                     <asp:DropDownList ID="ddlGroupType" runat="server" CssClass="form-control"  OnSelectedIndexChanged="ddlGroupType_SelectedIndexChanged" AutoPostBack="true">
                                     </asp:DropDownList>                                                                                     
                                        <asp:RequiredFieldValidator ID="rfvIGMType" runat="server" ControlToValidate="ddlGroupType" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select Group Type!" InitialValue="0" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>	                                      
	                                </div>
	                              </div>
                                  	<div class="col-sm-6">
	                                <label class="col-sm-4 control-label">Diesel Account<span class="required-field">*</span></label>
	                                <div class="col-sm-6">
                                       <asp:DropDownList ID="ddlDiesel" runat="server" CssClass="form-control" >
                                       </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDiesel" runat="server" ControlToValidate="ddlDiesel"
                                        SetFocusOnError="true" ValidationGroup="Save" InitialValue="0" ErrorMessage="Required Diesel Account!"
                                        Display="Dynamic" CssClass="classValidation"></asp:RequiredFieldValidator>
	                                                     
	                                </div>
	                                <div class="col-sm-1">
                                     <asp:ImageButton ID="refDiesel" runat="server" ToolTip="Refresh" ImageUrl="~/Images/RefreshNew.jpg"
                                                                                        Width="20px" Height="20px" OnClick="refDiesel_Click" />
	                                </div>
	                                <div class="col-sm-1">
                                     <asp:ImageButton ID="ImgAddDiesel" runat="server" ForeColor="AliceBlue" ToolTip="Add Diesel Account"
                                                                                        ImageUrl="~/Images/plus.gif" Width="15px" OnClientClick="return ShowClient(11,1,9);"/>
	                                </div>
	                              </div>
	                            </div>
	                            <div class="clearfix even_row">
	                              <div class="col-sm-6">
	                                <label class="col-sm-4 control-label">Other Account<span class="required-field">*</span></label>
	                                <div class="col-sm-6">
                                     <asp:DropDownList ID="ddlOthrAmnt" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlOthrAmnt" runat="server" ControlToValidate="ddlOthrAmnt"
                                    SetFocusOnError="true" ValidationGroup="Save" InitialValue="0" ErrorMessage="Please Select Account!"
                                    Display="Dynamic" CssClass="classValidation"></asp:RequiredFieldValidator>
	                                </div>
	                                <div class="col-sm-1">
                                     <asp:ImageButton ID="btnRefresh1" runat="server" ToolTip="Refresh" ImageUrl="~/Images/RefreshNew.jpg"
                                    Width="20px" Height="20px" OnClick="btnRefresh1_Click" />&nbsp;&nbsp;
	                                </div>
	                                <div class="col-sm-1">
                                    <asp:ImageButton ID="imgOthrAcnt" runat="server" ForeColor="AliceBlue" ToolTip="Add Account Head"
                                    ImageUrl="~/Images/plus.gif" Width="15px" OnClientClick="return ShowClient(16,1,1);"
                                    />
	                                </div>
	                              </div>
	                             	<div class="col-sm-6">
	                                <label class="col-sm-4 control-label">Commission<span class="required-field">*</span></label>
	                                <div class="col-sm-6">
                                       <asp:DropDownList ID="ddlComision" runat="server" CssClass="form-control" >
                                       </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlComision" runat="server" ControlToValidate="ddlComision"
                                        SetFocusOnError="true" ValidationGroup="Save" InitialValue="0" ErrorMessage="Please Select Commission!"
                                        Display="Dynamic" CssClass="classValidation"></asp:RequiredFieldValidator>
	                                                     
	                                </div>
	                                <div class="col-sm-1">
                                     <asp:ImageButton ID="btnRefresh2" runat="server" ToolTip="Refresh" ImageUrl="~/Images/RefreshNew.jpg"
                                                                                        Width="20px" Height="20px" OnClick="btnRefresh2_Click" />
	                                </div>
	                                <div class="col-sm-1">
                                     <asp:ImageButton ID="imgCmmn" runat="server" ForeColor="AliceBlue" ToolTip="Add Commission Account"
                                                                                        ImageUrl="~/Images/plus.gif" Width="15px" OnClientClick="return ShowClient(11,1,2);"
                                                                                         />
	                                </div>
	                              </div>                              
	                            </div>	

	                            <div class="clearfix odd_row">
	                              <div class="col-sm-6">
	                                <label class="col-sm-4 control-label">Service Tax<span class="required-field">*</span></label>
	                                <div class="col-sm-6">
                                     <asp:DropDownList ID="ddlservcetax" runat="server" CssClass="form-control" >
                                     </asp:DropDownList>                                                                                   
                                    <asp:RequiredFieldValidator ID="rfvServtax" runat="server" ControlToValidate="ddlservcetax"
                                        SetFocusOnError="true" ValidationGroup="Save" InitialValue="0" ErrorMessage="Please Select Service Tax!"
                                        Display="Dynamic" CssClass="classValidation"></asp:RequiredFieldValidator>
	                                             
	                                </div>
	                                <div class="col-sm-1">
                                     <asp:ImageButton ID="btnRefresh3" runat="server" ToolTip="Refresh" ImageUrl="~/Images/RefreshNew.jpg"
                                                                                        Width="20px" Height="20px" OnClick="btnRefresh3_Click" />
	                                </div>
	                                <div class="col-sm-1">
                                     <asp:ImageButton ID="imgservtax" runat="server" ForeColor="AliceBlue" ToolTip="Add Service Tax Account"
                                    ImageUrl="~/Images/plus.gif" Width="15px" OnClientClick="return ShowClient(10,1,3);"
                                    />
	                                </div>
	                              </div>
	                             	<div class="col-sm-6">
	                                <label class="col-sm-4 control-label">TDS Tax<span class="required-field">*</span></label>
	                                <div class="col-sm-6">
                                     <asp:DropDownList ID="ddlTdsAcnt" runat="server" CssClass="form-control"  >
                                    </asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTdsAcnt"  SetFocusOnError="true" ValidationGroup="Save" InitialValue="0" ErrorMessage="Please Select Tds Tax!"
                                        Display="Dynamic" CssClass="classValidation"></asp:RequiredFieldValidator>
	                                          
	                                </div>
	                                <div class="col-sm-1">
                                     <asp:ImageButton ID="btnRefresh4" runat="server" ToolTip="Refresh" ImageUrl="~/Images/RefreshNew.jpg"  Width="20px" Height="20px" onclick="btnRefresh4_Click"  />
	                                </div>
	                                <div class="col-sm-1">
                                     <asp:ImageButton ID="imgTdstax" runat="server" ForeColor="AliceBlue" ToolTip="Add TDS Account"  ImageUrl="~/Images/plus.gif" Width="15px" OnClientClick="return ShowClient(10,1,4);"
                                     />
	                                </div>
	                              </div>                              
	                            </div>	
                                 <div class="clearfix odd_row">
	                              <div class="col-sm-6">
	                                <label class="col-sm-4 control-label">Swachh Bharat Cess<span class="required-field">*</span></label>
	                                <div class="col-sm-6">
                                     <asp:DropDownList ID="ddlSwachhBharat" runat="server" CssClass="form-control" >
                                     </asp:DropDownList>                                                                                   
                                    <asp:RequiredFieldValidator ID="rfvSwachhBharat" runat="server" ControlToValidate="ddlSwachhBharat"
                                        SetFocusOnError="true" ValidationGroup="Save" InitialValue="0" ErrorMessage="Swachh Bharat Cess Required!"
                                        Display="Dynamic" CssClass="classValidation"></asp:RequiredFieldValidator>
	                                             
	                                </div>
	                                <div class="col-sm-1">
                                     <asp:ImageButton ID="btnRefSwachhBharat" runat="server" ToolTip="Refresh" ImageUrl="~/Images/RefreshNew.jpg"
                                                                                        Width="20px" Height="20px" OnClick="btnRefSwachhBharat_Click" />
	                                </div>
	                                <div class="col-sm-1">
                                     <asp:ImageButton ID="imgSwachhBharat" runat="server" ForeColor="AliceBlue" ToolTip="Add Swachh Bharat Account"
                                    ImageUrl="~/Images/plus.gif" Width="15px" OnClientClick="return ShowClient(10,1,7);"
                                   />
	                                </div>
	                              </div>
	                             	<div class="col-sm-6">
	                                <label class="col-sm-4 control-label">Krishi Kalyan Cess<span class="required-field">*</span></label>
	                                <div class="col-sm-6">
                                     <asp:DropDownList ID="ddlKrishiKalyan" runat="server" CssClass="form-control"  >
                                    </asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="rfvKrishiKalyan" runat="server" ControlToValidate="ddlKrishiKalyan"  SetFocusOnError="true" ValidationGroup="Save" InitialValue="0" ErrorMessage="Krishi Kalyan Cess Required!"
                                        Display="Dynamic" CssClass="classValidation"></asp:RequiredFieldValidator>
	                                          
	                                </div>
	                                <div class="col-sm-1">
                                     <asp:ImageButton ID="btnKrishiKalyan" runat="server" ToolTip="Refresh" ImageUrl="~/Images/RefreshNew.jpg"  Width="20px" Height="20px" onclick="btnKrishiKalyan_Click"  />
	                                </div>
	                                <div class="col-sm-1">
                                     <asp:ImageButton ID="imgbtnKrishiKalyan" runat="server" ForeColor="AliceBlue" ToolTip="Add Krishi Kalyan Account"  ImageUrl="~/Images/plus.gif" Width="15px" OnClientClick="return ShowClient(10,1,8);"
                                     />
	                                </div>
	                              </div>                              
	                            </div>
                                </section>
                                <section class="panel panel-in-default">
	                            <div class="clearfix even_row">
	                              <div class="col-sm-6">
	                                <label class="col-sm-4 control-label">Debit Note(Party)<span class="required-field">*</span></label>
	                                <div class="col-sm-6">
                                      <asp:DropDownList ID="ddlDebitnote" runat="server" CssClass="form-control" >
                                        </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDebitnote"
                                        SetFocusOnError="true" ValidationGroup="Save" InitialValue="0" ErrorMessage="Please Select Debit Note!"
                                        Display="Dynamic" CssClass="classValidation"></asp:RequiredFieldValidator>	                                     
	                                </div>
	                                <div class="col-sm-1">
                                    <asp:ImageButton ID="btnRefresh5" runat="server" ToolTip="Refresh" ImageUrl="~/Images/RefreshNew.jpg"
                                        Width="20px" Height="20px" onclick="btnRefresh5_Click"  />
	                                </div>
	                                <div class="col-sm-1">
                                     <asp:ImageButton ID="imgDebitNote" runat="server" ForeColor="AliceBlue" ToolTip="Add Service Tax Account"
                                        ImageUrl="~/Images/plus.gif" Width="15px" OnClientClick="return ShowClient(11,1,10);" />
	                                </div>
	                              </div>
	                             	<div class="col-sm-6">
	                                <label class="col-sm-4 control-label">TDS Amount(Party)<span class="required-field">*</span></label>
	                                <div class="col-sm-6">
                                      <asp:DropDownList ID="ddlTDSAmnt" runat="server" CssClass="form-control"  >
                                        </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTdsAcnt"
                                        SetFocusOnError="true" ValidationGroup="Save" InitialValue="0" ErrorMessage="Please Select TDS Amount!"
                                        Display="Dynamic" CssClass="classValidation"></asp:RequiredFieldValidator>	                                                   
	                                </div>
	                                <div class="col-sm-1">
                                    <asp:ImageButton ID="btnRefresh6" runat="server" ToolTip="Refresh" ImageUrl="~/Images/RefreshNew.jpg"
                                        Width="20px" Height="20px" onclick="btnRefresh6_Click"   />
	                                </div>
	                                <div class="col-sm-1">
                                    <asp:ImageButton ID="ImgTDSAmnt" runat="server" ForeColor="AliceBlue" ToolTip="Add TDS Account"
                                        ImageUrl="~/Images/plus.gif" Width="15px" OnClientClick="return ShowClient(7,1,6);" />
	                                </div>
	                              </div>                              
	                            </div>	   
                                </section>
                                 <section class="panel panel-in-default">
                                 <div class="clearfix odd_row">
                                 <div class="col-sm-6">
	                                <label class="col-sm-4 control-label">SGST Account<span class="required-field">*</span></label>
	                                <div class="col-sm-6">
                                      <asp:DropDownList ID="ddlSGST" runat="server" CssClass="form-control" >
                                        </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSGST" runat="server" ControlToValidate="ddlSGST"
                                        SetFocusOnError="true" ValidationGroup="Save" InitialValue="0" ErrorMessage="Please Select SGST Account!"
                                        Display="Dynamic" CssClass="classValidation"></asp:RequiredFieldValidator>	                                     
	                                </div>
	                                <div class="col-sm-1">
                                    <asp:ImageButton ID="Imgbtnsgst" runat="server" ToolTip="Refresh" ImageUrl="~/Images/RefreshNew.jpg"
                                        Width="20px" Height="20px" onclick="Imgbtnsgst_Click"  />
	                                </div>
	                                <div class="col-sm-1">
                                     <asp:ImageButton ID="Imgsgstac" runat="server" ForeColor="AliceBlue" ToolTip="Add SGST Tax Account"
                                        ImageUrl="~/Images/plus.gif" Width="15px" OnClientClick="return ShowClient(10,1,12);" />
	                                </div>
	                              </div>
	                             	<div class="col-sm-6">
	                                <label class="col-sm-4 control-label">CGST Account<span class="required-field">*</span></label>
	                                <div class="col-sm-6">
                                      <asp:DropDownList ID="ddlCGST" runat="server" CssClass="form-control"  >
                                        </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCGST" runat="server" ControlToValidate="ddlCGST"
                                        SetFocusOnError="true" ValidationGroup="Save" InitialValue="0" ErrorMessage="Add SGST Tax Account!"
                                        Display="Dynamic" CssClass="classValidation"></asp:RequiredFieldValidator>	                                                   
	                                </div>
	                                <div class="col-sm-1">
                                    <asp:ImageButton ID="Imgbtncgst" runat="server" ToolTip="Refresh" ImageUrl="~/Images/RefreshNew.jpg"
                                        Width="20px" Height="20px" onclick="Imgbtncgst_Click"   />
	                                </div>
	                                <div class="col-sm-1">
                                    <asp:ImageButton ID="Imgcgstac" runat="server" ForeColor="AliceBlue" ToolTip="Add TDS Account"
                                        ImageUrl="~/Images/plus.gif" Width="15px" OnClientClick="return ShowClient(10,1,13);" />
	                                </div>
	                              </div> 
                                 </div>
                                 <div class="clearfix odd_row">
                                 <div class="col-sm-6">
	                                <label class="col-sm-4 control-label">IGST Account<span class="required-field">*</span></label>
	                                <div class="col-sm-6">
                                      <asp:DropDownList ID="ddlIGST" runat="server" CssClass="form-control" >
                                        </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvIGST" runat="server" ControlToValidate="ddlIGST"
                                        SetFocusOnError="true" ValidationGroup="Save" InitialValue="0" ErrorMessage="Please Select IGST Account!"
                                        Display="Dynamic" CssClass="classValidation"></asp:RequiredFieldValidator>	                                     
	                                </div>
	                                <div class="col-sm-1">
                                    <asp:ImageButton ID="imgbtnIgst" runat="server" ToolTip="Refresh" ImageUrl="~/Images/RefreshNew.jpg"
                                        Width="20px" Height="20px" onclick="imgbtnIgst_Click"  />
	                                </div>
	                                <div class="col-sm-1">
                                     <asp:ImageButton ID="imgbtnIgstac" runat="server" ForeColor="AliceBlue" ToolTip="Add IGST Tax Account"
                                        ImageUrl="~/Images/plus.gif" Width="15px" OnClientClick="return ShowClient(10,1,14);" />
	                                </div>
	                              </div>                              
	                            </div>	   
                                </section>
	                          </div>
	                                                
	                      </div>

	                       <!-- fourth row -->
	                      <div class="clearfix fourth_right">
	                        <section class="panel panel-in-default btns_without_border">                            
	                          <div class="panel-body">     
	                            <div class="clearfix odd_row">
	                              <div class="col-lg-3"></div>
	                              <div class="col-lg-6">  
                                   <div class="col-sm-4">  
                                   <asp:Button ID="btnNew" runat="server" CssClass="btn full_width_btn btn-s-md btn-info" Text="New" OnClick="btnNew__OnClick" />
                                   </div>                                        
	                                <div class="col-sm-4">
                                       <asp:Button ID="btnSubmit" runat="server" 
                                            CssClass="btn full_width_btn btn-s-md btn-success" Text="Save" 
                                            CausesValidation="true" ValidationGroup="Save" OnClick="btnSubmit_OnClick" />
	                                 <asp:HiddenField ID="hidAcntLinkidno" runat="server" />
                        <asp:HiddenField ID="hidAcntType" runat="server" />
	                                </div>
	                                <div class="col-sm-4">
                                     <asp:Button ID="btnClose" runat="server" 
                                            CssClass="btn full_width_btn btn-s-md btn-danger" Text="Cancel" 
                                            CausesValidation="false" OnClick="btnCancel_OnClick" />
	                                </div>
	                              </div>
	                              <div class="col-lg-3"></div>
	                            </div> 
	                          </div>
	                        </section>
	                      </div>                      
	                    </form>
	                  </div>
	                </section>
                    <div id="dvAccount" class="web_dialog black12" style="display: none; width: 500px;">
                        <div class="modal-dialog" style="width: 500px">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="popform_header">
                                        Add Account
                                    </h4>
                                </div>
                                <div class="modal-body">
                                    <section class="panel panel-default full_form_container material_search_pop_form">
						<div class="panel-body">
						<div class="clearfix odd_row">	                                
	                <div class="col-sm-8">
                      <div class="col-sm-4">
	                 <asp:Label ID="lblAcnt" runat="server" Text="" CssClass="control-label"></asp:Label> <span class="required-field">*</span>:
                    </div>
                    <div class="col-sm-8">
                    <asp:TextBox ID="txtPurAcntHead" runat="server" autocomplete="off" CssClass="form-control" MaxLength="25"></asp:TextBox>                 
                                   
                    </div>
	                </div>	                
	                </div>                      
                        </div>
					    </section>
                                </div>
                                <div class="modal-footer">
                                    <div class="popup_footer_btn">
                                        <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-dark" CausesValidation="true"
                                            OnClick="imgBtnSave_Click" ValidationGroup="SaveClient" ><i class="fa fa-check"></i>Ok</asp:LinkButton>
                                        <button type="submit" class="btn btn-dark" data-dismiss="modal">
                                            <i class="fa fa-times"></i>Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-2">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
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
        function HideClient() {
            $("#dvAccount").fadeOut(300);
        }
        function ShowClient(id, AcntType, txtId) {
            $("#dvAccount").fadeIn(1000);
            $("#<%=hidAcntLinkidno.ClientID %>").val(id);
            $("#<%=hidAcntType.ClientID %>").val(AcntType);
            if (txtId == 1) {
                document.getElementById('<%=lblAcnt.ClientID%>').innerHTML = 'Others Account'
            }
            else if (txtId == 2) {
                document.getElementById('<%=lblAcnt.ClientID%>').innerHTML = 'Commission Account'
            }
            else if (txtId == 3) {
                document.getElementById('<%=lblAcnt.ClientID%>').innerHTML = 'Service Tax Account'
            }
            else if (txtId == 4) {
                document.getElementById('<%=lblAcnt.ClientID%>').innerHTML = 'TDS Tax'
            }
            else if (txtId == 5) {
                document.getElementById('<%=lblAcnt.ClientID%>').innerHTML = 'Debit Note Account'
            }
            else if (txtId == 6) {
                document.getElementById('<%=lblAcnt.ClientID%>').innerHTML = 'Tds Amount Account (Party)'
            }
            else if (txtId == 7) {
                document.getElementById('<%=lblAcnt.ClientID%>').innerHTML = 'Swachh Bharat Account'
            }
            else if (txtId == 8) {
                document.getElementById('<%=lblAcnt.ClientID%>').innerHTML = 'Krishi Kalyan Account'
            }
            else if (txtId == 9) {
                document.getElementById('<%=lblAcnt.ClientID%>').innerHTML = 'Diesel Account'
            }
            else if (txtId == 12) {
                document.getElementById('<%=lblAcnt.ClientID%>').innerHTML = 'SGST Account'
            }
            else if (txtId == 13) {
                document.getElementById('<%=lblAcnt.ClientID%>').innerHTML = 'CGST Account'
            }
            else if (txtId == 14) {
                document.getElementById('<%=lblAcnt.ClientID%>').innerHTML = 'IGST Account'
            }
            return false;
        }

    </script>
</asp:Content>
