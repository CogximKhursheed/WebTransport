<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ManageInvoiceDetails.aspx.cs" Inherits="WebTransport.ManageInvoiceDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-2">
                </div>
                <div class="col-lg-8">
                    <section class="panel panel-default full_form_container part_purchase_bill_form">
                	<header class="panel-heading font-bold">INVOICE DETAILS
                       
                		<span class="view_print"><a href="Invoice.aspx" tabindex="8"></a>&nbsp;
                       <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ToolTip="Export to excel"
                            ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px" OnClick="imgBtnExcel_Click"
                            Visible="false" />
                            <asp:LinkButton ID="lnkprint111" CssClass="fa fa-print icon" Visible="false" runat="server" ToolTip="Print11" AlternateText="Print" title="Print11" Height="16px" Onclick="lnkinvoicePrint_Click"></asp:LinkButton>
                            </span>
               	 	</header>
                	<div class="panel-body">
                    <form class="bs-example form-horizontal">
                      <!-- first  section --> 
                      	<div class="clearfix first_section">
	                        <section class="panel panel-in-default">  
	                          <div class="panel-body">
	                            <div class="clearfix odd_row">
                                <div class="col-sm-5" style="width: 43%">
                                  <label class="col-sm-4 control-label" style="width: 29%;">Date Range<span class="required-field">*</span></label>
                                  <div class="col-sm-8" style="width: 71%;">
                                    <asp:DropDownList ID="ddldateRange" runat="server" AutoPostBack="true" CssClass="form-control"
                                        Height="30px" TabIndex="1"  OnSelectedIndexChanged="ddldateRange_SelectedIndexChanged">
                                    </asp:DropDownList>
                                  </div>
                                </div>
                                <div class="col-sm-4" style="width: 28.5%">
                                  <label class="col-sm-5 control-label" style="width: 39%;">Date From</label>
                                    <div class="col-sm-7" style="width: 61%;">
                                    <asp:TextBox ID="txtReceiptDatefrom" runat="server" CssClass="input-sm datepicker form-control" MaxLength="50"
                                            TabIndex="2"  ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3" style="width: 28.5%">
                                  <label class="col-sm-5 control-label">Date To</label>
                                    <div class="col-sm-7">
                                    <asp:TextBox ID="txtReceiptDateto" runat="server" CssClass="input-sm datepicker form-control" MaxLength="50"
                                            TabIndex="3"  ></asp:TextBox>
                                    </div>
                                </div>
                              </div>
                              <div class="clearfix even_row">
                                <div class="col-sm-5" style="width: 43%">
                                  <label class="col-sm-4 control-label" style="width: 29%;">Invoice No.</label>
	                                <div class="col-sm-8" style="width: 71%;">
	                                  <asp:TextBox ID="txtReceiptNo" runat="server" CssClass="form-control" MaxLength="50" TabIndex="5" ></asp:TextBox>
                                      <span style="color:red">(Eg. 1,2,3 or 1-3)</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtReceiptNo"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Invoice No!"
                                    SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
	                                </div>
                                </div>
                                <div class="col-sm-4" style="width: 28.5%">
                                  <label class="col-sm-5 control-label" style="width: 39%;">Loc.[From]</label>
	                                <div class="col-sm-7" style="width: 61%;">
	                                  <asp:DropDownList ID="ddlFromCity" runat="server" CssClass="form-control"  TabIndex="6"></asp:DropDownList>
	                                </div>
                                </div>
                                  <div class="col-sm-3" style="width: 28.5%">  
                                </div>
                              </div>
                              <div class="clearfix odd_row">
                                <div class="col-sm-5" style="width: 43%">
                                </div>                                
                                <div class="col-sm-4" style="width: 28.5%; ">
                                </div>
                                <div class="col-sm-3" style="width: 28.5%">
                                  <div class="col-sm-5 prev_fetch" style="width: 49%;">
                                  <asp:LinkButton ID="lnkbtnPreview" CssClass="btn full_width_btn btn-sm btn-primary"  TabIndex="7" runat="server" ValidationGroup="Save" OnClick="lnkbtnPreview_OnClick"><i class="fa fa-search-plus"></i>Preview</asp:LinkButton>
                                  </div>
                                  <div class="col-sm-6"> 
                                     <asp:Label ID="lblTotalRecord" runat="server" Text="T. Record(s) : 0" CssClass="control-label" ></asp:Label>                                   
                                  </div>
                                  </div>
                                </div>
                              </div>

	                          
	                        </section>  
                            </form>                      
                      </div>

                      <!-- second row -->
                      <div class="clearfix fourth_right">
                        <section class="panel panel-in-default btns_without_border">                            
                          <div class="panel-body">     
                            <div class="clearfix">
		                          <section class="panel panel-default full_form_container material_search_pop_form">
		                            <div class="panel-body" style="overflow:auto">   
                                     <table width="100%">
                                       <tr>
                                       <td> 
                                      <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"
                                        Width="100%" GridLines="None" AllowPaging="true" PageSize="100" OnPageIndexChanging="grdMain_PageIndexChanging" ShowFooter="true"
                                        BorderWidth="0" >
                                        <RowStyle CssClass="odd" />
                                        <AlternatingRowStyle CssClass="even" />                                       
                                       <PagerStyle  CssClass="classPager" />
                                         <PagerSettings Mode="NumericFirstLast" PageButtonCount="5"  FirstPageText="First" Position="Bottom" LastPageText="Last"/>   
                                         <Columns>
                                        <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle HorizontalAlign="Center" Width="50" />
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>.
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                            <ItemTemplate>
                                               <%-- <%#Convert.ToString(Eval("Inv_prefix")) + Convert.ToString(Eval("Inv_No"))%>--%>
                                               <%#Convert.ToString(Eval("Inv_No"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GrNo" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                            <ItemTemplate>
                                                <%#Eval("GRNo")%>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                          <%--  <FooterTemplate>
                                                <asp:Label ID="lblTotal" Text="Grid Total :" runat="server"></asp:Label>
                                            </FooterTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Net Amount" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100">
                                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                            <ItemStyle HorizontalAlign="Right" Width="100" />
                                            <ItemTemplate>
                                                <%#Convert.ToDouble(Eval("Net_Amnt")).ToString("N2")%>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" ForeColor="Black" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotNeAmnt" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                    </EmptyDataTemplate>
                                    <PagerStyle CssClass="white_bg" ForeColor="#000" HorizontalAlign="Center" />
                                </asp:GridView>
                                        </td>
                                       </tr>
                                        <tr>
                                       <td>
                                       <div class="secondFooterClass"  id="divpaging" runat="server" visible="false">                                                                           
                                        <table class="" id="tblFooterscnd" runat="server">
		                                  <tr><th rowspan="1" colspan="1" style="width:207px;"> <asp:Label ID="lblcontant" runat="server"></asp:Label></th><th rowspan="1" colspan="1" style="width: 149px;"></th><th rowspan="1" colspan="1" style="width: 120px;text-align:right;">Total</th><th rowspan="1" colspan="1" style="width: 110px;padding-left:60px;"><asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label>
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
                      
                         <div id="trprint" runat="server" visible="false">
                        <asp:GridView ID="grdprint" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"
                            Width="100%" GridLines="None" AllowPaging="true" PageSize="10"
                            BorderWidth="0">
                        <RowStyle CssClass="odd" />
                        <AlternatingRowStyle CssClass="even" />                                       
                        <PagerStyle  CssClass="classPager" />
                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="5"  FirstPageText="First" Position="Bottom" LastPageText="Last"/>   
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" Width="50" />
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>.
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                        <ItemStyle HorizontalAlign="Left" Width="100" />
                                        <ItemTemplate>
                                            <%#Eval("Inv_No")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GrNo" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                        <ItemStyle HorizontalAlign="Left" Width="100" />
                                        <ItemTemplate>
                                            <%#Eval("GRNo")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Net Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                        <ItemStyle HorizontalAlign="Left" Width="100" />
                                        <ItemTemplate>
                                            <%#Convert.ToDouble(Eval("Net_Amnt")).ToString("N2")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="internal_heading" />
                                <PagerStyle CssClass="white_bg" ForeColor="#000" HorizontalAlign="Center" />
                                <RowStyle CssClass="bgcolrwhite" />
                            </asp:GridView>
                        </div>
                       
                    </form>
                </div>
              </section>
                </div>
                <div class="col-lg-2">
                    <asp:HiddenField ID="hidrcptheadidno" runat="server" />
                    <asp:HiddenField ID="hidmindate" runat="server" />
                    <asp:HiddenField ID="hidmaxdate" runat="server" />
                    <asp:HiddenField ID="hidAdminApp" runat="server" />
                    <asp:HiddenField ID="hidtext" runat="server" />
                    <asp:HiddenField ID="hidyear" runat="server" />
                    <asp:HiddenField ID="hidcityid" runat="server" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgBtnExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function () {
            setDatecontrol();
        });

        prm.add_endRequest(function () {
            setDatecontrol();
        });

        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();

            $("#<%=txtReceiptDatefrom.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
            $("#<%=txtReceiptDateto.ClientID %>").datepicker({
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