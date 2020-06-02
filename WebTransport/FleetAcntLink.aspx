<%@ Page Title="Fleet Account Link" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="FleetAcntLink.aspx.cs" Inherits="WebTransport.FleetAcntLink" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-0">
                </div>
                <div class="col-lg-12">
                    <section class="panel panel-default full_form_container part_purchase_bill_form">
	                  <header class="panel-heading font-bold form_heading">FLEET ACCOUNT LINK	                   
	                  </header>
	                  <div class="panel-body">
	                    <form class="bs-example form-horizontal">
	                      <!-- first  section --> 
	                      <div class="clearfix first_section">
	                        <section class="panel panel-in-default">  
	                          <div class="panel-body">
	                            <div class="clearfix even_row">
	                              <div class="col-sm-6" style="width:50%">
	                                <label class="col-sm-3 control-label" style="width:25%">Purchase A/c (VAT)</label>
	                                <div class="col-sm-5" style="width:60%">
                                         <asp:DropDownList ID="ddlVatPur" runat="server" CssClass="form-control" TabIndex="1" >
                                        </asp:DropDownList> 
	                                </div>
	                                <div class="col-sm-1" style="width:5%">
                                     <asp:ImageButton ID="btnRefresh1" runat="server" ToolTip="Refresh" ImageUrl="~/Images/RefreshNew.jpg"
                                    Width="20px" Height="20px" OnClick="btnRefresh1_Click" />
	                                </div>
	                                <div class="col-sm-1"  style="width:5%">
                                    <asp:ImageButton ID="imgPurAcnt" runat="server" ForeColor="AliceBlue" ToolTip="Add Account Head"
                                    ImageUrl="~/Images/plus.gif" Width="15px" OnClientClick="return ShowClient(20,1,1);"
                                    OnClick="imgOthrAcnt_Click" />
	                                  
	                                </div>
	                              </div>
	                             	<div class="col-sm-6" style="width:50%">
	                                <label class="col-sm-5 control-label" style="width:25%">Purchase A/c (CST)</label>
	                                <div class="col-sm-5" style="width:60%">
                                       <asp:DropDownList ID="ddlCSTPur" runat="server" CssClass="form-control"  TabIndex="2" >
                                       </asp:DropDownList> 	                                                     
	                                </div>
	                                <div class="col-sm-1" style="width:5%">
                                     <asp:ImageButton ID="btnRefresh2" runat="server" ToolTip="Refresh" ImageUrl="~/Images/RefreshNew.jpg"
                                                                                        Width="20px" Height="20px" OnClick="btnRefresh2_Click" />
	                                  
	                                </div>
	                                <div class="col-sm-1" style="width:5%">
                                     <asp:ImageButton ID="imgCmmn" runat="server" ForeColor="AliceBlue" ToolTip="Add Commission Account"
                                                                                        ImageUrl="~/Images/plus.gif" Width="15px" OnClientClick="return ShowClient(20,1,2);"
                                                                                        OnClick="imgCmmn_Click" />
	                                  
	                                </div>
	                              </div>                              
	                            </div>	
                                <div class="clearfix even_row">
	                              <div class="col-sm-6" style="width:50%">
	                                <label class="col-sm-5 control-label" style="width:25%">Sale A/c (VAT)</label>
	                                <div class="col-sm-5" style="width:60%">
                                     <asp:DropDownList ID="ddlVatSale" runat="server" CssClass="form-control" TabIndex="1" >
                                     </asp:DropDownList> 
	                                </div>
	                                <div class="col-sm-1" style="width:5%">
                                     <asp:ImageButton ID="imgRefreshVatSale" runat="server" ToolTip="Refresh" ImageUrl="~/Images/RefreshNew.jpg"
                                    Width="20px" Height="20px" OnClick="btnRefresh1_Click" />
	                                </div>
	                                <div class="col-sm-1" style="width:5%">
                                    <asp:ImageButton ID="ImgAddVatSale" runat="server" ForeColor="AliceBlue" ToolTip="Add Account Head"
                                    ImageUrl="~/Images/plus.gif" Width="15px" OnClientClick="return ShowClient(21,1,8);"
                                    OnClick="imgOthrAcnt_Click" />
	                                  
	                                </div>
	                              </div>
	                             	<div class="col-sm-6" style="width:50%">
	                                <label class="col-sm-5 control-label" style="width:25%">Sale A/c (CST)</label>
	                                <div class="col-sm-5" style="width:60%">
                                       <asp:DropDownList ID="ddlCstSale" runat="server" CssClass="form-control"  TabIndex="2" >
                                       </asp:DropDownList> 	                                                     
	                                </div>
	                                <div class="col-sm-1" style="width:5%">
                                     <asp:ImageButton ID="imgRefreshCsrSale" runat="server" ToolTip="Refresh" ImageUrl="~/Images/RefreshNew.jpg"
                                                                                        Width="20px" Height="20px" OnClick="btnRefresh2_Click" />
	                                  
	                                </div>
	                                <div class="col-sm-1" style="width:5%">
                                     <asp:ImageButton ID="ImgAddCstSale" runat="server" ForeColor="AliceBlue" ToolTip="Add Commission Account"
                                                                                        ImageUrl="~/Images/plus.gif" Width="15px" OnClientClick="return ShowClient(21,1,9);"
                                                                                        OnClick="imgCmmn_Click" />
	                                  
	                                </div>
	                              </div>                              
	                            </div>	
	                            <div class="clearfix odd_row">
	                              <div class="col-sm-6" style="width:50%">
	                                <label class="col-sm-5 control-label" style="width:25%">Vat A/c</label>
	                                <div class="col-sm-5" style="width:60%">
                                     <asp:DropDownList ID="ddlVat" runat="server" CssClass="form-control"  TabIndex="3">
                                     </asp:DropDownList>                                                                                   
	                                             
	                                </div>
	                                <div class="col-sm-1" style="width:5%">
                                     <asp:ImageButton ID="btnRefresh3" runat="server" ToolTip="Refresh" ImageUrl="~/Images/RefreshNew.jpg"
                                                                                        Width="20px" Height="20px" OnClick="btnRefresh3_Click" />
	                                </div>
	                                <div class="col-sm-1" style="width:5%">
                                     <asp:ImageButton ID="imgservtax" runat="server" ForeColor="AliceBlue" ToolTip="Add Service Tax Account"
                                    ImageUrl="~/Images/plus.gif" Width="15px" OnClientClick="return ShowClient(10,1,3);"
                                    OnClick="imgservtax_Click" />
	                                
	                                </div>
	                              </div>
	                             	<div class="col-sm-6" style="width:50%">
	                                <label class="col-sm-5 control-label" style="width:25%">CST A/c</label>
	                                <div class="col-sm-5" style="width:60%">
                                     <asp:DropDownList ID="ddlCST" runat="server" CssClass="form-control"  
                                            TabIndex="4" >
                                    </asp:DropDownList>
	                                </div>
	                                <div class="col-sm-1" style="width:5%">
                                     <asp:ImageButton ID="btnRefresh4" runat="server" ToolTip="Refresh" ImageUrl="~/Images/RefreshNew.jpg"  Width="20px" Height="20px" onclick="btnRefresh4_Click"  />
	                              
	                                </div>
	                                <div class="col-sm-1" style="width:5%">
                                     <asp:ImageButton ID="imgTdstax" runat="server" ForeColor="AliceBlue" ToolTip="Add TDS Account"  ImageUrl="~/Images/plus.gif" Width="15px" OnClientClick="return ShowClient(10,1,4);"
                                     OnClick="imgTdstax_Click" />
	                                 
	                                </div>
	                              </div>                              
	                            </div>	
	                            <div class="clearfix even_row">
	                              <div class="col-sm-6" style="width:50%">
	                                <label class="col-sm-5 control-label" style="width:25%">Discount A/c</label>
	                                <div class="col-sm-5" style="width:60%">
                                      <asp:DropDownList ID="ddlDiscount" runat="server" CssClass="form-control" 
                                            TabIndex="5" >
                                        </asp:DropDownList>
	                                </div>
	                                <div class="col-sm-1" style="width:5%">
                                    <asp:ImageButton ID="btnRefresh5" runat="server" ToolTip="Refresh" ImageUrl="~/Images/RefreshNew.jpg"
                                        Width="20px" Height="20px" onclick="btnRefresh5_Click"  />
	                              
	                                </div>
	                                <div class="col-sm-1" style="width:5%">
                                     <asp:ImageButton ID="imgDebitNote" runat="server" ForeColor="AliceBlue" ToolTip="Add Service Tax Account"
                                        ImageUrl="~/Images/plus.gif" Width="15px" OnClientClick="return ShowClient(11,1,5);" />
	                                     
	                                </div>
	                              </div>
	                             	<div class="col-sm-6" style="width:50%">
	                                <label class="col-sm-5 control-label" style="width:25%">Other Chrgs. A/c</label>
	                                <div class="col-sm-5" style="width:60%">
                                      <asp:DropDownList ID="ddlOtherChrg" runat="server" CssClass="form-control"  
                                            TabIndex="6" >
                                        </asp:DropDownList>
	                                </div>
	                                <div class="col-sm-1" style="width:5%">
                                    <asp:ImageButton ID="btnRefresh6" runat="server" ToolTip="Refresh" ImageUrl="~/Images/RefreshNew.jpg"
                                        Width="20px" Height="20px" onclick="btnRefresh6_Click"   />
	                            
	                                </div>
	                                <div class="col-sm-1" style="width:5%">
                                    <asp:ImageButton ID="ImgTDSAmnt" runat="server" ForeColor="AliceBlue" ToolTip="Add TDS Account"
                                        ImageUrl="~/Images/plus.gif" Width="15px" OnClientClick="return ShowClient(16,1,6);" />
	                                </div>
	                              </div>                              
	                            </div>	   
                                <div class="clearfix odd_row">
	                              <div class="col-sm-6" style="width:50%">
	                                <label class="col-sm-5 control-label" style="width:25%">Cash A/c</label>
	                                <div class="col-sm-5" style="width:60%">
                                      <asp:DropDownList ID="ddlCash" runat="server" CssClass="form-control" 
                                            TabIndex="7" >
                                        </asp:DropDownList>
	                                </div>
	                                <div class="col-sm-1" style="width:5%">
                                    <asp:ImageButton ID="btnRefresh7" runat="server" ToolTip="Refresh" ImageUrl="~/Images/RefreshNew.jpg"
                                        Width="20px" Height="20px" onclick="btnRefresh7_Click" />
	                                </div>
	                                <div class="col-sm-1" style="width:5%">
                                     <asp:ImageButton ID="ImageButton2" runat="server" ForeColor="AliceBlue" ToolTip="Add Service Tax Account"
                                        ImageUrl="~/Images/plus.gif" Width="15px" OnClientClick="return ShowClient(22,3,7);" />
	                                </div>
	                              </div>
	                                <div class="col-sm-6" style="width:50%">
	                                <label class="col-sm-5 control-label" style="width:25%">Toll Account</label>
	                                <div class="col-sm-5" style="width:60%">
                                      <asp:DropDownList ID="ddlTollAcc" runat="server" CssClass="form-control" 
                                            TabIndex="5" >
                                        </asp:DropDownList>
	                                </div>
	                                <div class="col-sm-1" style="width:5%">
                                    <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Refresh" ImageUrl="~/Images/RefreshNew.jpg"
                                        Width="20px" Height="20px" onclick="RefreshTollAcc_Click"  />
	                              
	                                </div>
	                                <div class="col-sm-1" style="width:5%">
                                     <asp:ImageButton ID="ImageButton3" runat="server" ForeColor="AliceBlue" ToolTip="Add Toll Account"
                                        ImageUrl="~/Images/plus.gif" Width="15px" OnClientClick="return ShowClient(11,1,10);" />
	                                     
	                                </div>
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
	                              <div class="col-lg-3"></div>
	                              <div class="col-lg-6">  
                                   <div class="col-sm-4">  
                                   <asp:Button ID="btnNew" runat="server" CssClass="btn full_width_btn btn-s-md btn-info" Text="New" OnClick="btnNew__OnClick" />
                                   </div>                                        
	                                <div class="col-sm-4">
                                       <asp:Button ID="btnSubmit" runat="server" 
                                            CssClass="btn full_width_btn btn-s-md btn-success" Text="Save" 
                                            CausesValidation="true" ValidationGroup="Save" OnClick="btnSubmit_OnClick" 
                                            TabIndex="8" />
	                                 <asp:HiddenField ID="hidAcntLinkidno" runat="server" />
                        <asp:HiddenField ID="hidAcntType" runat="server" />
	                                </div>
	                                <div class="col-sm-4">
                                     <asp:Button ID="btnClose" runat="server" 
                                            CssClass="btn full_width_btn btn-s-md btn-danger" Text="Cancel" 
                                            CausesValidation="false" OnClick="btnCancel_OnClick" TabIndex="9" />
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
                    <div id="dvAccount" class="web_dialog black12" style="display: none; width: 500px;
                        height: auto;">
                        <div class="modal-dialog">
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
	                <div class="col-sm-12">
                      <div class="col-sm-4">
	                 <asp:Label ID="lblAcnt" runat="server" Text="" CssClass="control-label"></asp:Label> <span class="required-field">*</span>:
                    </div>
                    <div class="col-sm-8">
                    <asp:TextBox ID="txtPurAcntHead" runat="server" autocomplete="off" CssClass="form-control" MaxLength="25"></asp:TextBox>                 
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPurAcntHead"
                                        SetFocusOnError="true" ValidationGroup="Save" InitialValue="0" ErrorMessage="Please Enter "
                                        Display="Dynamic" CssClass="classValidation"></asp:RequiredFieldValidator>	                 
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
                document.getElementById('<%=lblAcnt.ClientID%>').innerHTML = 'VAT Purchase Account'
            }
            else if (txtId == 2) {
                document.getElementById('<%=lblAcnt.ClientID%>').innerHTML = 'CST Purchase Account'
            }
            else if (txtId == 3) {
                document.getElementById('<%=lblAcnt.ClientID%>').innerHTML = 'VAT Account'
            }
            else if (txtId == 4) {
                document.getElementById('<%=lblAcnt.ClientID%>').innerHTML = 'CST Tax'
            }
            else if (txtId == 5) {
                document.getElementById('<%=lblAcnt.ClientID%>').innerHTML = 'Discount Account'
            }
            else if (txtId == 6) {
                document.getElementById('<%=lblAcnt.ClientID%>').innerHTML = 'Other Charges Account'
            }
            else if (txtId == 7) {
                document.getElementById('<%=lblAcnt.ClientID%>').innerHTML = 'Cash Account'
            }
            else if (txtId == 8) {
                document.getElementById('<%=lblAcnt.ClientID%>').innerHTML = 'VAT Sale Account'
            }
            else if (txtId == 9) {
                document.getElementById('<%=lblAcnt.ClientID%>').innerHTML = 'CST Sale Account'
            }
            else if (txtId == 10) {
                document.getElementById('<%=lblAcnt.ClientID%>').innerHTML = 'Toll Account'
            }
            return false;
        }

    </script>
</asp:Content>
