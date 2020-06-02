<%@ Page Title="STOCK REPORT [ACCESS.]" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="StockSummaryReport.aspx.cs" Inherits="WebTransport.StockSummaryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

        $("#<%=txtDate.ClientID %>").datepicker({
            buttonImageOnly: false,
            maxDate: 0,
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
            minDate: mindate,
            maxDate: maxdate
        });
        $("#<%=txtDateTO.ClientID %>").datepicker({
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
        <div id="page-content">
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-9">
            <section class="panel panel-default full_form_container part_purchase_bill_form">
                	<header class="panel-heading font-bold">STOCK REPORT-[ACCESSORY] &nbsp;
                	<span class="view_print">&nbsp;
                        <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ToolTip="Export to excel"
                            ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px" Visible="false" TabIndex="5"
                            OnClick="imgBtnExcel_Click" />
                    </span>
               	 	</header>
                	<div class="panel-body">
                    <form class="bs-example form-horizontal">
                      <!-- first  section --> 
                      	<div class="clearfix first_section">
	                        <section class="panel panel-in-default">  
	                          <div class="panel-body">
	                            <div class="clearfix odd_row">
                                <div class="col-sm-4" style="width:40%">
                                  <label class="col-sm-4 control-label" style="width:30%">Date Range<span class="required-field">*</span></label>
                                  <div class="col-sm-8" style="width:69%">
                                  <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged" TabIndex="1">
                                    </asp:DropDownList>
                                  </div>
                                </div>
                                <div class="col-sm-4" style="width:30%">
                                  <label class="col-sm-4 control-label" style="width:40%">Date From<span class="required-field">*</span></label>
                                    <div class="col-sm-7" style="width:55%">
                                    <asp:TextBox ID="txtDate" runat="server" CssClass="input-sm datepicker form-control"  MaxLength="50" data-date-format="dd-mm-yyyy" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                TabIndex="2" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Date!" SetFocusOnError="true"
                                ValidationGroup="save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-4" style="width:30%">
                                  <label class="col-sm-4 control-label" style="width:34%">Date To<span class="required-field">*</span></label>
                                    <div class="col-sm-7" style="width:55%">
                                       <asp:TextBox ID="txtDateTO" runat="server" CssClass="input-sm datepicker form-control" data-date-format="dd-mm-yyyy"  MaxLength="50" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                TabIndex="2" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDateTO"
                                CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Date!" SetFocusOnError="true"
                                ValidationGroup="save"></asp:RequiredFieldValidator>
                                    
                                    </div>
                                </div>
                              </div>
                                <div class="clearfix even_row">
                              <div class="col-sm-4" style="width:40%">
                                 <label class="col-sm-4 control-label" style="width:30%">Location</label>
                                   <div class="col-sm-8" style="width:69%">
                              <asp:DropDownList ID="ddlFromCity" runat="server" CssClass="form-control" TabIndex="4" ></asp:DropDownList>
                              </div>
                              </div>
                              <div class="col-sm-4" style="width:40%">     
                                <label class="col-sm-4 control-label" style="width:30%">Item Name</label>
                                   <div class="col-sm-7" style="width:70%">                             
                                      <asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control" TabIndex="3"></asp:DropDownList>
                                </div>
                                </div> 
                            </div>
                            <div class="clearfix odd_row">
                             <div class="col-sm-8">   
                             </div>
                             <div class="col-sm-4">   
                                 <div class="col-sm-6 prev_fetch"  style="text-align:right">
                                    <asp:LinkButton ID="lnkbtnPreview" CssClass="btn full_width_btn btn-sm btn-primary" ValidationGroup="Previw" TabIndex="5" runat="server" OnClick="lnkbtnPreview_OnClick"><i class="fa fa-search-plus"></i>Preview</asp:LinkButton> &nbsp;
                                  </div>
                                &nbsp;<asp:Label ID="lblTotalRecord" runat="server"  Text="T. Record(s) : 0" CssClass="control-label" ></asp:Label>                               
                                </div>
                            </div>
	                          </div>
	                        </section>                        
                      	</div>

                       <!-- second row -->
                         <div class="clearfix fourth_right">
                        <section class="panel panel-in-default btns_without_border">                            
                          <div class="panel-body">     
                            <div class="clearfix">
		                           <section class="panel panel-default full_form_container material_search_pop_form">
		                            <div class="panel-body" style="overflow:auto;">   
                                   <table>
                                       <tr>
                                       <td>  
                                       <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" 
                                               BorderStyle="None" CssClass="display nowrap dataTable"
                                        Width="100%" GridLines="None" AllowPaging="true" PageSize="50" 
                                               OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCreated="grdMain_RowCreated"
                                        BorderWidth="0" ShowFooter="true" onrowdatabound="grdMain_RowDataBound">
                                        <RowStyle CssClass="odd" />
                                        <AlternatingRowStyle CssClass="even" />                                       
                                        <Columns>
                                        <asp:TemplateField HeaderText="Sr No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" Width="50" />
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>.
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="410px" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="Left" Width="410px" />
                                                <ItemTemplate>
                                                    <%#Eval("Item_Name")%>
                                                </ItemTemplate>
                                                <FooterStyle ForeColor="Black" Font-Bold="true" HorizontalAlign="Left" Font-Size="Small" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotal" Text='Grid Total ' runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Opening" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                <ItemTemplate>
                                                    <%#Eval("OS")%>
                                                </ItemTemplate>
                                                <FooterStyle ForeColor="Black" Font-Bold="true" HorizontalAlign="Center" Font-Size="Small" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblOSTotal" Text="" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Closing" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                <ItemTemplate>
                                                   <%#Eval("CL")%>
                                                </ItemTemplate>
                                                <FooterStyle ForeColor="Black" Font-Bold="true" HorizontalAlign="Center" Font-Size="Small" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblCLTotal" Text="" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                       <PagerStyle  CssClass="classPager" />
                                         <PagerSettings Mode="NumericFirstLast" PageButtonCount="5"  FirstPageText="First" Position="Bottom" LastPageText="Last"/>
                                       
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
		                                  <tr>
                                          <th rowspan="1" colspan="1" style="width: 350px;"> <asp:Label ID="lblcontant" runat="server"></asp:Label></th>
                                          <th rowspan="1" colspan="1" style="width: 110px; text-align:left;">Total&nbsp;</th>
                                   
                                         
                                          <th rowspan="1" colspan="1" style="width: 110px; text-align:right"><asp:Label ID="lblOpenTot" runat="server"></asp:Label></th>
                                          <th rowspan="1" colspan="1" style="width: 120px; text-align:right"><asp:Label ID="lblClosTot" runat="server"></asp:Label></th>
                                          </tr>                                  
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
        <asp:HiddenField ID="hidmindate" runat="server" />
        <asp:HiddenField ID="hidmaxdate" runat="server" />
        <asp:HiddenField ID="hidrcptheadidno" runat="server" />       
        </div>
    </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
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