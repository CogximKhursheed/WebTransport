<%@ Page Title="Tax Master" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="TaxMaster.aspx.cs" Inherits="WebTransport.TaxMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div class="row ">
                <div class="col-lg-2">
                </div>
                <div class="col-lg-8">
                    <section class="panel panel-default full_form_container part_purchase_bill_form">
	                  <header class="panel-heading font-bold form_heading">TAX MASTER
	                    </header>
	                  <div class="panel-body">
	                    <form class="bs-example form-horizontal">
	                      <!-- first  section --> 
	                   		<div class="clearfix first_section">
	                        <section class="panel panel-in-default">  
	                          <div class="panel-body">
	                          	<div class="clearfix estimate_fourth_row odd_row">
                                <div class="col-sm-6" style="width:50%">
                                	 <label class="col-sm-3 control-label" style="width: 25%;">Date Range</label>
                                  <div class="col-sm-9" style="width: 73%;">
                                    <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control" AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged">
                                    </asp:DropDownList>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                        ControlToValidate="ddlDateRange" ValidationGroup="save" ErrorMessage="Please Select Date Range."
                                        InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                                    </div>
	                              
	                            </div>
                                <div class="clearfix estimate_fourth_row even_row">
                                <div class="col-sm-6" style="width:50%">
	                                <label class="col-sm-3 control-label" style="width:15%">State<span class="required-field">*</span></label>
	                                <div class="col-sm-9"  style="width: 83%;">
                                     <asp:DropDownList ID="drpState" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpState_SelectedIndexChanged"  TabIndex="2" AutoPostBack="true" >
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpState"
                                        ValidationGroup="Save" ErrorMessage="Please select State!" CssClass="classValidation"  SetFocusOnError="true" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>	                                                 
	                                </div>
	                              </div>
                                  </div>
	                            <div class="clearfix estimate_fourth_row even_row">
	                              <div class="col-sm-6" style="width:50%">
                                  <label class="col-sm-3 control-label"  style="width:25%">Tax Type<span class="required-field">*</span></label>
	                                <div class="col-sm-9"  style="width:73%">
                                     <asp:DropDownList ID="drpdownTaxType" runat="server" CssClass="form-control"  TabIndex="3" AutoPostBack="true" OnSelectedIndexChanged="drpdownTaxType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drpdownTaxType"
                                        ValidationGroup="Save" ErrorMessage="Please select Tax Type!" CssClass="classValidation"
                                        SetFocusOnError="true" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
	                                </div>
	                              </div>
                                   <div class="col-sm-6" style="width:22%">
	                                <label class="col-sm-3 control-label" style="width:30%">Date<span class="required-field">*</span></label>
	                                <div class="col-sm-9" style="width:70%">
                                     <asp:TextBox ID="txtDate" runat="server" CssClass="input-sm datepicker form-control" TabIndex="4" MaxLength="50" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDate"
                                        ValidationGroup="Save" ErrorMessage="Please Select Date!" CssClass="classValidation"
                                        SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
	                                </div>
	                              </div>
                                  <div class="col-sm-6"  style="width:28%">
	                                <label class="col-sm-3 control-label" style="width:46%">Tax Rate(%)<span class="required-field">*</span></label>
	                                <div class="col-sm-9"  style="width:52%">
                                     <asp:TextBox ID="taxRate" runat="server" Text="0.00" CssClass="form-control" MaxLength="6" TabIndex="5" onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                    	                                 
	                                </div>
                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="taxRate"
                                        ValidationGroup="Save" ErrorMessage="Please enter Tax Rate!" CssClass="classValidation"
                                        SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
	                              </div>
	                            </div>
	                            <div class="clearfix estimate_fourth_row even_row">
	                              <div class="col-sm-3" style="width: 23.333333%;">
	                                <label class="control-label">PAN Type </label>
	                                <div>
                                    <asp:DropDownList ID="ddlPANType" runat="server" CssClass="form-control" TabIndex="6" >
                                    </asp:DropDownList>	                                       
	                                </div>
	                              </div>
	                             	<div class="col-sm-3" style="width: 23.333333%;">
	                                <label class="control-label">Lorry From </label>
	                                <div>
                                     <asp:TextBox ID="txtLorryFrom" runat="server" CssClass="form-control" TabIndex="7" MaxLength="8" Style="text-align: right;" Text="0"></asp:TextBox>	                                
	                                </div>
	                              </div>

	                              <div class="col-sm-3" style="width: 23.333333%;">
	                                <label class="control-label">Lorry To</label>
	                                <div>
                                      <asp:TextBox ID="txtlorryto" runat="server" CssClass="form-control"  TabIndex="8"  MaxLength="8" Style="text-align: right;" Text="0"></asp:TextBox><br />
	                               
	                                </div>
	                              </div>
	                              <div class="col-sm-3" style="width: 12%;">
	                                <label class="control-label">Cal. on DF <span class="required-field">*</span></label>
	                                <div style="padding-left:14%">
                                     <asp:CheckBox ID="chkCalOnDF" runat="server" TabIndex="9" />
	                                  
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
	                              <div class="col-lg-3"></div>
	                              <div class="col-lg-6">                                            
	                                <div class="col-sm-6">
                                        <asp:LinkButton ID="lnkbtnSave" runat="server" CssClass="btn full_width_btn btn-s-md btn-success" TabIndex="10" CausesValidation="true" ValidationGroup="Save" OnClick="lnkbtnSave_OnClick"><i class="fa fa-save"></i>Save</asp:LinkButton>
	                                    <asp:HiddenField ID="hidmindate" runat="server" />
                                        <asp:HiddenField ID="hidmaxdate" runat="server" />
	                                </div>
	                                <div class="col-sm-6">
                                          <asp:LinkButton ID="lnkbtnCancel" runat="server" CssClass="btn full_width_btn btn-s-md btn-danger" TabIndex="11" OnClick="lnkbtnCancel_OnClick"><i class="fa fa-close"></i>Cancel</asp:LinkButton>
	                                  
	                                </div>
	                              </div>
	                              <div class="col-lg-3" style="padding-top: 2%"> <b><asp:Label ID="lblTotalRecord" runat="server" CssClass="control-label" Text="T. Record(s): 0" ></asp:Label></b></div>
	                            </div> 
	                          </div>
	                        </section>
	                      </div>
	                      <div class="clearfix third_right">
	                        <section class="panel panel-in-default btns_without_border">                            
	                          <div class="panel-body" id="DivGridShow" runat="server">    
                                <div class="clearfix">
		                          <section class="panel panel-default full_form_container material_search_pop_form">
		                            <div class="panel-body" style="overflow:scroll;">  
                                      <table>
                                       <tr>
                                       <td>
	                                     <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"
                                    Width="100%" GridLines="None" AllowPaging="true" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging"
                                    BorderWidth="0"  OnRowCommand="grdMain_RowCommand" OnRowCreated="grdMain_RowCreated"
                                    OnRowDataBound="grdMain_RowDataBound">
                                   <RowStyle CssClass="odd" />
                                    <AlternatingRowStyle CssClass="even" />                                       
                                    <PagerStyle CssClass="" HorizontalAlign="Right" Height="60" />
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5"  FirstPageText="First" Position="Bottom" LastPageText="Last"/>   
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="50" />
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>.
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="State" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                            <ItemStyle HorizontalAlign="Left" Width="150" />
                                            <ItemTemplate>
                                                <%#Eval("StateNm")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tax Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                            <ItemTemplate>
                                                <%#Convert.ToDateTime(Eval("TaxDate")).ToString("dd-MMM-yyyy")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tax Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                            <ItemStyle HorizontalAlign="Left" Width="150" />
                                            <ItemTemplate>
                                                <%#Eval("TaxNm")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PAN Type" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                            <ItemTemplate>
                                                <%#Eval("PANType")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lorry&nbsp;From" HeaderStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="50">
                                            <ItemStyle HorizontalAlign="Center" Width="50" />
                                            <ItemTemplate>
                                                <%#Eval("LorryFrom")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lorry&nbsp;To" HeaderStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="50">
                                            <ItemStyle HorizontalAlign="Center" Width="50" />
                                            <ItemTemplate>
                                                <%#Eval("LorryTo")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tax Rate(%)" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100">
                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                            <ItemTemplate>
                                                <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("TaxRate")))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cal.On&nbsp;DF" HeaderStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="50">
                                            <ItemStyle HorizontalAlign="Center" Width="50" />
                                            <ItemTemplate>
                                                <%#Eval("CalculateOnDF")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="50" />
                                            <ItemTemplate>
                                               <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("TaxMastId") %>' CommandName="cmdedit" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>

                                                 <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("TaxMastId") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                           
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                    </EmptyDataTemplate>                                 
                                </asp:GridView>
                                        </td>
                                       </tr>
                                        <tr>
                                       <td>
                                       <div class="secondFooterClass"  id="divpaging" runat="server" visible="false">                                                                           
                                        <table class="" id="tblFooterscnd" runat="server" >
		                                  <tr><th rowspan="1" colspan="1" style="width:149px;"> <asp:Label ID="lblcontant" runat="server"></asp:Label></th><th rowspan="1" colspan="1" style="width: 149px;"></th><th rowspan="1" colspan="1" style="width: 120px;text-align:right;">&nbsp;</th><th rowspan="1" colspan="1" style="width: 110px;padding-left:60px;"><asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label>
                                          </th><th rowspan="1" colspan="1" style="width:2px;"></th><th rowspan="1" colspan="1" style="width: 62px;"></th><th rowspan="1" colspan="1" style="width: 63px;"></th></tr>                                  
		                                </tfoot>
                                        </table>

                                       </div>
                                       </td>
                                       </tr>
                                       <tr>
                                       <td>
                                           <br /> &nbsp;
                                       </td>
                                       </tr>
                                       </table>
                                        <br />  
	                          </div>
	                        </section>
	                      </div>    
	                         </div>
	                        </section>
	                      </div>                   
	                    </form>
	                  </div>
	                </section>
                </div>
                <div class="col-lg-2">
                </div>
            </div>
            <asp:HiddenField ID="hidTaxid" runat="server" />
    <script type="text/javascript" language="javascript">

        SetFocus();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function () {
            setDatecontrol();
            SetFocus();
        });

        prm.add_endRequest(function () {
            setDatecontrol();
            SetFocus();
        });


        $(document).ready(function () {
            setDatecontrol();
            SetFocus();
            $('#<%=drpState.ClientID %>').focus();
        });
        function setDatecontrol() {

            $('#<%=txtDate.ClientID %>').datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });

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
