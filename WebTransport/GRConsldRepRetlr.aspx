<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="GRConsldRepRetlr.aspx.cs" Inherits="WebTransport.GRConsldRepRetlr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

        $("#<%=txtDateFrom.ClientID %>").datepicker({
            buttonImageOnly: false,
            maxDate: 0,
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
            minDate: mindate,
            maxDate: maxdate
        });
        $("#<%=txtDateTo.ClientID %>").datepicker({
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-1">
                </div>
                <div class="col-lg-10">
                    <section class="panel panel-default full_form_container part_purchase_bill_form">
                	<header class="panel-heading font-bold">GR Retailer Combined Report
                		<span class="view_print">
                        <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ToolTip="Export to excel" ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px" OnClick="imgBtnExcel_Click" Visible="false" />
                        </span>
               	 	</header>
                	<div class="panel-body">
                    <form class="bs-example form-horizontal">
                      <!-- first  section --> 
                      	<div class="clearfix first_section">
	                        <section class="panel panel-in-default">  
	                          <div class="panel-body">
	                            <div class="clearfix odd_row">
                                <div class="col-sm-5" style="width: 37%">
                                  <label class="col-sm-4 control-label" style="width: 29%;">Date Range<span class="required-field">*</span></label>
                                  <div class="col-sm-8" style="width: 71%;">
                                    <asp:DropDownList ID="ddlDateRange" runat="server" AutoPostBack="True" 
                                        CssClass="form-control" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged" 
                                        TabIndex="1" Width="210px">
                                    </asp:DropDownList>                            
                                  </div>
                                </div>
                                <div class="col-sm-4" style="width: 28.5%">
                                  <label class="col-sm-5 control-label" style="width: 39%;">Date From</label>
                                    <div class="col-sm-7" style="width: 61%;">
                                      <asp:TextBox ID="txtDateFrom" runat="server" CssClass="input-sm datepicker form-control" MaxLength="12" data-date-format="dd-mm-yyyy" TabIndex="2" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDateFrom" runat="server" ControlToValidate="txtDateFrom"
                                                            SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage="<br /> Please Enter Date!"
                                                            Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-3" style="width: 28.5%">
                                  <label class="col-sm-5 control-label">Date To</label>
                                    <div class="col-sm-7">
                                      <asp:TextBox ID="txtDateTo" runat="server" CssClass="input-sm datepicker form-control" MaxLength="12"
                                                                            TabIndex="3" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDateTo" runat="server" ControlToValidate="txtDateTo"
                                                            SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage="<br /> Please Enter Date!"
                                                            Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                              </div>
                              <div class="clearfix even_row">
                                <div class="col-sm-4" style="width: 37%">
                                  <label class="col-sm-4 control-label" style="width: 29%;">Loc.[From]</label>
	                                <div class="col-sm-8" style="width: 71%;">
	                                <asp:DropDownList ID="drpBaseCity" runat="server" CssClass="form-control" TabIndex="4">
                                                                    </asp:DropDownList>
	                                </div>
                                </div>
                                <div class="col-sm-4" style="width: 28.5%">
                                  <label class="col-sm-5 control-label" style="width: 39%;">Sender</label>
	                                <div class="col-sm-7" style="width: 61%;">
	                                <asp:DropDownList ID="ddlSender" runat="server" CssClass="form-control"   TabIndex="8"></asp:DropDownList>
	                                </div>
                                </div> 
                                <div class="col-sm-4" style="width: 28.5%">
                                  <label class="col-sm-5 control-label" style="width: 39%;">GR No.</label>
                                    <div class="col-sm-7" style="width: 61%;">
                                      <asp:TextBox ID="txtGrNo" runat="server" CssClass="form-control" MaxLength="12"  TabIndex="12" ></asp:TextBox>                                        
                                    </div>
                                </div>                               
                              </div>
                             <div class="clearfix odd_row">
                           <div class="col-sm-8">

                           </div >
                            <div class="col-sm-2 prev_fetch">
                                    <asp:LinkButton ID="lnkbtnPreview" CssClass="btn full_width_btn btn-sm btn-primary"  TabIndex="9" runat="server" OnClick="lnkbtnPreview_OnClick"><i class="fa fa-search-plus"></i>Preview</asp:LinkButton>
                                  </div>
                                  <div class="col-sm-2"> 
                                     <b><asp:Label ID="lblTotalRecord" runat="server" Text="T. Record(s) : 0" CssClass="control-label" ></asp:Label></b>
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
		                            <div class="panel-body" style="overflow:auto">                                   
		                               <!-- First Row start -->  
                                       <table width="100%">
                                       <tr>
                                       <td>  
                                      <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"
                                        Width="100%" GridLines="None" AllowPaging="true" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging"
                                        BorderWidth="0" ShowFooter="true" OnRowCommand="grdMain_RowCommand"  OnRowDataBound="grdMain_RowDataBound">
                                        <RowStyle CssClass="odd" />
                                        <AlternatingRowStyle CssClass="even" />                                       
                                        <PagerStyle  CssClass="classPager" />
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5"  FirstPageText="First" Position="Bottom" LastPageText="Last"/>   
                                        <FooterStyle CssClass="Footerclass" />   
                                         <Columns>
                                            <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="35"  HeaderStyle-HorizontalAlign="center"
                                                Visible="true">                            
                                                <ItemStyle HorizontalAlign="center" Width="35" />
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>.
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="center" Width="35" ForeColor="White" Font-Bold="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GR.Date" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Left">                           
                                                <ItemStyle HorizontalAlign="Left" Width="70" />
                                                <ItemTemplate>
                                                    <%#(string.IsNullOrEmpty(Convert.ToString(Eval("GRRet_Date"))) ? "" : (Convert.ToDateTime((Eval("GRRet_Date"))).ToString("dd-MMM-yyyy")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GR No" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Center">                            
                                                <ItemStyle HorizontalAlign="Center" Width="80" />
                                                <ItemTemplate>
                                                    <%#Eval("GRRet_No")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Manual No" HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="Left">                         
                                                <ItemStyle HorizontalAlign="Left" Width="90" />
                                                <ItemTemplate>
                                                    <%#Eval("Manual_No")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Sender Name " HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="Left">                         
                                                <ItemStyle HorizontalAlign="Left" Width="90" />
                                                <ItemTemplate>
                                                    <%#Eval("Sender")%>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Receiver Name " HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="Left">                         
                                                <ItemStyle HorizontalAlign="Left" Width="90" />
                                                <ItemTemplate>
                                                    <%#Eval("Receiver")%>
                                                </ItemTemplate>
                                                    <FooterTemplate>
                                                    <b>GRID TOTAL</b>
                                                </FooterTemplate>
                                            </asp:TemplateField>                                           
                                            <asp:TemplateField HeaderText="Invoice No" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">                              
                                                <ItemStyle HorizontalAlign="Left" Width="50" />
                                                <ItemTemplate>
                                                    <%#Eval("GRRet_Pref")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice Date" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">                           
                                                <ItemStyle HorizontalAlign="Left" Width="80" />
                                                <ItemTemplate>
                                                    <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Inv_Date"))) ? "" : (Convert.ToDateTime((Eval("Inv_Date"))).ToString("dd-MMM-yyyy")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Ref. No" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">                              
                                                <ItemStyle HorizontalAlign="Left" Width="50" />
                                                <ItemTemplate>
                                                    <%#Eval("Ref_No")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ref. Date" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">                           
                                                <ItemStyle HorizontalAlign="Left" Width="80" />
                                                <ItemTemplate>
                                                    <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Ref_Date"))) ? "" : (Convert.ToDateTime((Eval("Ref_Date"))).ToString("dd-MMM-yyyy")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="From City" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Left">                         
                                                <ItemStyle HorizontalAlign="Left" Width="70" />
                                                <ItemTemplate>
                                                    <%#Eval("From_City")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To City" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Left">                         
                                                <ItemStyle HorizontalAlign="Left" Width="70" />
                                                <ItemTemplate>
                                                    <%#Eval("Delivery_Place")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="Left">                         
                                                <ItemStyle HorizontalAlign="Left" Width="90" />
                                                <ItemTemplate>
                                                    <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Tot_Qty"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Tot_Qty")))))%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b><asp:Label ID="lblQty" runat="server"></asp:Label></b>
                                                </FooterTemplate>
                                            </asp:TemplateField>                                               
                                            <asp:TemplateField HeaderText="Weight" HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="Left">                         
                                                <ItemStyle HorizontalAlign="Left" Width="90" />
                                                <ItemTemplate>
                                                   <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Tot_ChWeight"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Tot_ChWeight")))))%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b><asp:Label ID="lblWeight" runat="server"></asp:Label></b>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">                        
                                                <ItemStyle HorizontalAlign="Left" Width="50"  />
                                                <ItemTemplate>
                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Tot_Rate"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Tot_Rate")))))%>
                                                </ItemTemplate>
                                                    <FooterStyle CssClass="gridHeaderAlignRight" />                                                
                                            </asp:TemplateField> 
                                            <asp:TemplateField  HeaderText="Freight" HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="Left">                         
                                                <ItemStyle HorizontalAlign="Left" Width="90" />
                                                <ItemTemplate>
                                                    <%#Eval("Gross_Amnt")%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b><asp:Label ID="lblgross" runat="server"></asp:Label></b>
                                                </FooterTemplate>
                                            </asp:TemplateField>                                             
                                              <asp:TemplateField HeaderText="Lorry No" HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="Left">                         
                                                <ItemStyle HorizontalAlign="Left" Width="90" />
                                                <ItemTemplate>
                                                    <%#Eval("Lorry_No")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                           
                                              <asp:TemplateField HeaderText="Bill No" HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="Left">                         
                                                <ItemStyle HorizontalAlign="Left" Width="90" />
                                                <ItemTemplate>
                                                    <%#Eval("Inv_No")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                           
                                             <asp:TemplateField HeaderText="Bill Date" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Left">                           
                                                <ItemStyle HorizontalAlign="Left" Width="70" />
                                                <ItemTemplate>
                                                    <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Inv_Date"))) ? "" : (Convert.ToDateTime((Eval("Inv_Date"))).ToString("dd-MMM-yyyy")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lorry Freight" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">                        
                                                <ItemStyle HorizontalAlign="right" Width="50"  />
                                                <ItemTemplate>
                                                <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Lorry_Frieght"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Lorry_Frieght")))))%>
                                                </ItemTemplate>
                                                    <FooterStyle CssClass="gridHeaderAlignRight" />
                                                <FooterTemplate>
                                                    <b><asp:Label ID="lblLorryFre" runat="server"></asp:Label></b>
                                                </FooterTemplate>
                                            </asp:TemplateField>                                               
                                             <asp:TemplateField HeaderText="Challan No" HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="Left">                         
                                                <ItemStyle HorizontalAlign="Left" Width="90" />
                                                <ItemTemplate>
                                                    <%#Eval("Chln_No")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>  
                                             <asp:TemplateField HeaderText="Challan Date" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Left">                           
                                                <ItemStyle HorizontalAlign="Left" Width="70" />
                                                <ItemTemplate>
                                                    <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Chln_Date"))) ? "" : (Convert.ToDateTime((Eval("Chln_Date"))).ToString("dd-MMM-yyyy")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>   
                                             <asp:TemplateField HeaderText="Delivery Date" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Left">                           
                                                <ItemStyle HorizontalAlign="Left" Width="70" />
                                                <ItemTemplate>
                                                    <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Delvry_Date"))) ? "" : (Convert.ToDateTime((Eval("Delvry_Date"))).ToString("dd-MMM-yyyy")))%>
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
                                       <b>
                                        <div class="secondFooterClass"  id="divpaging" runat="server" visible="false">                                                                           
                                        <div class="col-sm-2" style="text-align:left"><asp:Label ID="lblcontant" runat="server"></asp:Label></div>  
                                        <div class="col-sm-2" style="text-align:right"></div>
                                        <div class="col-sm-2" style="text-align:right"><asp:Label ID="lblGrossAmnt" runat="server"></asp:Label></div>
                                        <div class="col-sm-1" style="text-align:right;"><asp:Label ID="lblQty" runat="server"></asp:Label></div>
                                        <div class="col-sm-1" style="text-align:right"><asp:Label ID="lblWeight" runat="server"></asp:Label></div>                                        
                                        <div class="col-sm-1" style="text-align:right"><asp:Label ID="lblLorryFre" runat="server"></asp:Label></div>
                                        <div class="col-sm-1" style="text-align:right"><asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label></div>
                                       </div>
                                       </b>
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
            <asp:HiddenField ID="hidrcptheadidno" runat="server" />
            <asp:HiddenField ID="hidmindate" runat="server" />
            <asp:HiddenField ID="hidmaxdate" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgBtnExcel" />
        </Triggers>
    </asp:UpdatePanel>
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
