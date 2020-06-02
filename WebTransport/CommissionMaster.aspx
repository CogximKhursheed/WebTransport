<%@ Page Title="Commission Master" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="CommissionMaster.aspx.cs" EnableEventValidation="false" Inherits="WebTransport.CommissionMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-3">
                </div>
                <div class="col-lg-6">
                    <section class="panel panel-default full_form_container part_purchase_bill_form">
	                  <header class="panel-heading font-bold form_heading">COMMISSION MASTER
	                  
	                  </header>
	                  <div class="panel-body">
	                    <form class="bs-example form-horizontal">
	                      <!-- first  section --> 
	                   		<div class="clearfix second_section">
	                        <section class="panel panel-in-default">  
	                          <div class="panel-body">
	                          	<div class="clearfix odd_row">
	                          		<div class="col-sm-5">
	                                <label class="control-label">Date Range</label>
	                                <div>
	                                  <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control" 
                                                            TabIndex="1">
                                      </asp:DropDownList>
	                                </div>
	                              </div>
	                              <div class="col-sm-3">
	                                <label class="control-label">Date<span class="required-field">*</span></label>
	                                <div>
                                    <asp:TextBox ID="txtDateRate"  runat="server" CssClass="col-sm-7" TabIndex="2"  ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtDateRate" runat="server" ControlToValidate="txtDateRate"
                                                    CssClass="classValidation" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Please Enter Date"
                                                    ValidationGroup="save" InitialValue=""></asp:RequiredFieldValidator>
                                                     <asp:ImageButton ID="imgPreviousDate" ImageUrl="~/images/info.gif" CssClass="col-sm-3"
                                          AlternateText="Previous Date" runat="server" onclick="imgPreviousDate_Click" ></asp:ImageButton>
	                                </div>
                                   
	                              </div>
	                              <div class="col-sm-4">
	                                <label class="control-label">Type<span class="required-field">*</span></label>
	                                <div>
	                                   <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" 
                                            TabIndex="3" onselectedindexchanged="ddlType_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="1">State Wise</asp:ListItem>
                                                    <asp:ListItem Value="2">City Wise</asp:ListItem>
                                                </asp:DropDownList>      
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select City"
                                                            ControlToValidate="ddlType" Display="Dynamic" SetFocusOnError="true" InitialValue="0"
                                                            CssClass="classValidation" ValidationGroup="Submit"></asp:RequiredFieldValidator>       
                                                                
	                                </div>
	                              </div>
	                            </div>
	                            <div class="clearfix even_row">
	                              <div class="col-sm-5">
	                                <label class="control-label">Item Name</label>
	                                <div>
	                                  <asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control" onselectedindexchanged="ddlItemName_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>  
                                            <asp:RequiredFieldValidator ID="rfvddlItmName" runat="server" ControlToValidate="ddlItemName"  
                                                        Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"
                                                        class="classValidation" ErrorMessage="Please Select Item Name"></asp:RequiredFieldValidator>        
	                                </div>
	                              </div>
	                              <div class="col-sm-3">
	                                <label class="control-label">Loc.[From]<span class="required-field">*</span></label>
	                                <div>
	                                  <asp:DropDownList ID="drpBaseCity" runat="server" TabIndex="5" CssClass="form-control" onselectedindexchanged="drpBaseCity_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList> 
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpBaseCity" 
                                                                Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"
                                                                class="classValidation" ErrorMessage="Please Select Base City"></asp:RequiredFieldValidator>                      
	                                </div>
	                              </div>
	                              <div class="col-sm-4">
	                                <label class="control-label">State<span class="required-field">*</span></label>
	                                <div>
	                                  <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" Width="210px" TabIndex="6" onselectedindexchanged="ddlState_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlState" runat="server" ErrorMessage="Please select State"
                                            ControlToValidate="ddlState" Display="Dynamic" SetFocusOnError="true" InitialValue="0"
                                            CssClass="classValidation" ValidationGroup="Submit"></asp:RequiredFieldValidator>                 
	                                </div>
	                              </div>
	                            </div>
	                            <div class="clearfix odd_row">
	                              <div class="col-sm-8">
	                                <label class="col-sm-3 control-label">Commission</label>
	                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtCommsision" runat="server" CssClass="form-control" 
                                            Width="120px" TabIndex="7"
                                        MaxLength="8" Style="text-align: right;" Text="0.00" 
                                            ontextchanged="txtCommsision_TextChanged" AutoPostBack="true"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="rfvItemWghtRate" runat="server" ErrorMessage="Please Enter Weight Rate"
                                        ControlToValidate="txtCommsision" Display="Dynamic" SetFocusOnError="true" InitialValue="0"
                                        CssClass="classValidation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
	                                </div>
	                                <div class="col-sm-3">
                                             <asp:Button ID="btnPreview" class="btn full_width_btn btn-sm btn-primary" 
                                            style="padding: 3px;" TabIndex="8"   Text="Preview"
                                                        ToolTip="Preview"  ValidationGroup="Submit" runat="server" 
                                            onclick="btnPreview_Click" />
                                  </div>
	                              </div>
	                             	<div class="col-sm-4"></div>
	                            </div>                            
	                          </div>
	                        </section>                        
	                      </div>
	                      
	                      <div class="clearfix third_right" style="height:450px; overflow:auto;" id="Grid" runat="server">
	                        <section class="panel panel-in-default btns_without_border">                            
	                          <div class="panel-body">     
						    <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" DataKeyNames="Tocity_Idno" BorderStyle="None" CssClass="display nowrap dataTable"
                                    Width="100%" GridLines="None" EnableViewState="true"  BorderWidth="0"
                                    ShowFooter="true"  OnRowCommand="grdMain_RowCommand"
                                    OnRowDataBound="grdMain_RowDataBound" PageSize="30">
                                    <RowStyle CssClass="odd" />
                                    <AlternatingRowStyle CssClass="even" /> 
                                       <PagerStyle CssClass="" HorizontalAlign="Right" Height="60" />
                                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="5"  FirstPageText="First" Position="Bottom" LastPageText="Last"/>   
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="40" HeaderStyle-CssClass="gridHeaderAlignCenter">
                                                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                                    <ItemStyle Width="40" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Date" HeaderStyle-Width="100" HeaderStyle-CssClass="gridHeaderAlignCenter">
                                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                    <ItemStyle Width="100" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCHDate" runat="server" Text=' <%#Convert.ToString(Eval("CMSDate"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ToCity" HeaderStyle-Width="100" HeaderStyle-CssClass="gridHeaderAlignCenter">
                                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                    <ItemStyle Width="100" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCityName" runat="server" Text=' <%#Convert.ToString(Eval("City_Name"))%>'></asp:Label>
                                                        <asp:HiddenField ID="hidTocity_Idno" runat="server" Value='<%#Convert.ToString(Eval("Tocity_Idno"))%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ItemRate" HeaderStyle-Width="50" HeaderStyle-CssClass="gridHeaderAlignCenter">
                                                    <HeaderStyle HorizontalAlign="Right" Width="50px" />
                                                    <ItemStyle Width="50" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCommissionAmnt" Style="text-align:right;" 
                                                            CssClass="form-control"  Width="100px" runat="server" 
                                                            Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("Comision_Amnt"))) ? "0.00" : (Convert.ToString((Eval("Comision_Amnt")))))%>' 
                                                            ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
 
	                          </div>
	                        </section>
	                      </div> 

	                       <!-- fourth row -->
	                      <div class="clearfix fourth_right">
	                        <section class="panel panel-in-default btns_without_border">                            
	                          <div class="panel-body">     
	                            <div class="clearfix odd_row">
	                              <div class="col-lg-2"></div>
	                              <div class="col-sm-8">                                          
                                    <div class="col-sm-4">                                          
                                 <%-- <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false" Width="100px" CssClass="btn full_width_btn btn-s-md btn-info" OnClick="lnkbtnNew_OnClick" ><i class="fa fa-file-o"></i>New</asp:LinkButton>                                                            	--%>
                                    </div>
	                                <div class="col-sm-4">
	                                  <asp:LinkButton ID="lnkbtnSave" runat="server" TabIndex="8" CausesValidation="true" Width="100px"  ValidationGroup="Save" CssClass="btn full_width_btn btn-s-md btn-success" OnClick="lnkbtnSave_OnClick" ><i class="fa fa-save"></i>Save</asp:LinkButton>                      
	                                </div>
	                                <div class="col-sm-4">
	                                  <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" TabIndex="9" CssClass="btn full_width_btn btn-s-md btn-danger" OnClick="lnkbtnCancel_OnClick" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
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
                <div class="col-lg-3">
                </div>
            </div>
    <div id="Welcome_Msg" class="modal fade">
        <div class="modal-dialog" style="padding-left: 100px; padding-top: 80px">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="popform_header">
                        Previous Commission Date(s)&nbsp;</h4>
                </div>
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
                                  <div class="panel-body">
                                     <!-- First Row start -->
                                    <div class="clearfix odd_row" style="text-align:center; overflow:auto; height:300px;">                                
                                      <asp:GridView ID="grdPreviusDates" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable" Width="100%" GridLines="None">
                                       <RowStyle CssClass="odd" />
                                     <AlternatingRowStyle CssClass="even" /> 
                                       <PagerStyle CssClass="" HorizontalAlign="Right" Height="60" />
                                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="5"  FirstPageText="First" Position="Bottom" LastPageText="Last"/>   
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="40" HeaderStyle-CssClass="gridHeaderAlignCenter">
                                                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                                    <ItemStyle Width="40" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Previous Date" HeaderStyle-Width="40" HeaderStyle-CssClass="gridHeaderAlignCenter">
                                                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                                    <ItemStyle Width="40" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPreviousDate" runat="server" Text=' <%#Convert.ToDateTime(Eval("Date")).ToString("dd-MM-yyyy")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Item" HeaderStyle-Width="40" HeaderStyle-CssClass="gridHeaderAlignCenter">
                                                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                                    <ItemStyle Width="40" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItem" runat="server" Text=' <%#Convert.ToString(Eval("Item_Name"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="From Location" HeaderStyle-Width="40" HeaderStyle-CssClass="gridHeaderAlignCenter">
                                                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                                    <ItemStyle Width="40" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLocation" runat="server" Text=' <%#Convert.ToString(Eval("City_Name"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                      </asp:GridView>
                                      </span>
                                    </div>      
                                  </div>
                                </section>
                </div>
            </div>
        </div>
    </div>

            <asp:HiddenField ID="HidFrmCityIdno" runat="server" />
            <asp:HiddenField ID="dmindate" runat="server" />
            <asp:HiddenField ID="hidmaxdate" runat="server" />
            <asp:HiddenField ID="hidrateid" runat="server" Value="0" />
            <asp:HiddenField ID="Hidrowid" runat="server" Value="" />
            <asp:HiddenField ID="HidInvoiceTyp" runat="server" />
            <asp:HiddenField ID="hidmindate" runat="server" />
            <asp:HiddenField ID="hidsave" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function () {
            setDatecontrol();
            $("#btnClose").click(function (e) {
                HideDialog();
                e.preventDefault();
            });
        });

        prm.add_endRequest(function () {
            setDatecontrol();
        });
        $(document).ready(function () {
            setDatecontrol();
        });

        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $('#<%=txtDateRate.ClientID %>').datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });

        }
        function openModal() {
            $('#Welcome_Msg').modal('show');
        }

        function closeModal() {
            $('#Welcome_Msg').modal('close');
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