<%@ Page Title="Lost Alignment Report" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="LostAlignmentsReport.aspx.cs" EnableEventValidation="false"
    Inherits="WebTransport.LostAlignmentsReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-1">
                </div>
                <div class="col-lg-10">
                    <section class="panel panel-default full_form_container part_purchase_bill_form">
                	<header class="panel-heading font-bold">Lost Alignment Report
                		<span class="view_print">
                         &nbsp;  <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ToolTip="Export to excel"
                                                                ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px" OnClick="imgBtnExcel_Click"
                                                                Visible="false" />
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
                                  <label class="col-sm-4 control-label" style="width: 29%;">Fin. Year<span class="required-field">*</span></label>
                                  <div class="col-sm-8" style="width: 68%;">
                                   <asp:DropDownList ID="ddlDateRange" runat="server" AutoPostBack="true" CssClass="form-control"
                                         TabIndex="1"  OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged">
                                    </asp:DropDownList>                                            
                                  </div>
                                </div>
                                <div class="col-sm-4" style="width: 28.5%">
                                  <label class="col-sm-5 control-label" style="width: 39%;">Date From</label>
                                    <div class="col-sm-7" style="width: 61%;">
                                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="input-sm datepicker form-control" MaxLength="50" data-date-format="dd-mm-yyyy"
                                                                            TabIndex="2"  ></asp:TextBox>
                                 
                                    </div>
                                </div>
                                <div class="col-sm-3" style="width: 28.5%">
                                  <label class="col-sm-5 control-label">Date To</label>
                                    <div class="col-sm-7">
                                     <asp:TextBox ID="txtDateTo" runat="server" CssClass="input-sm datepicker form-control" MaxLength="50" data-date-format="dd-mm-yyyy"
                                                                            TabIndex="3"  ></asp:TextBox>
                                 
                                    </div>
                                </div>
                              </div>
                              <div class="clearfix even_row">
                                <div class="col-sm-5" style="width: 43%">
                                  <label class="col-sm-4 control-label" style="width: 29%;">Lorry No</label>
	                                <div class="col-sm-8" style="width: 68%;">
                                      <asp:DropDownList ID="drpLorryNo" TabIndex="4" runat="server" CssClass="form-control" >
                                    </asp:DropDownList>
                                                                 
	                                </div>
                                </div>
                               
                                 <div class="col-sm-3" style="width: 56.5%; padding: 0;">
                                  <div class="col-sm-6 prev_fetch" style="width: 25%;">
                                    <asp:LinkButton ID="lnkbtnPreview" CssClass="btn full_width_btn btn-sm btn-primary"  TabIndex="6" runat="server" OnClick="lnkbtnPreview_OnClick"><i class="fa fa-search-plus"></i>Preview</asp:LinkButton>
                                  </div>
                                  <div class="col-sm-6" style="width: 30%;"> 
                                     <b><asp:Label ID="lblTotalRecord" runat="server" Text="T. Record(s) : 0" CssClass="control-label" ></asp:Label></b>
                                  </div>
                                
                                 
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
		                            <div class="panel-body" style="overflow-x:auto; padding-bottom:10px;">                                   
		                               <!-- First Row start -->  
                                         <table width="100%">
                                       <tr>
                                       <td>
		                                <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" Width="100%"  AllowPaging="true" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging"
                                        BorderWidth="1"  BorderStyle="Solid" GridLines="None"  OnRowCommand="grdMain_RowCommand" TabIndex="4" CssClass="display nowrap dataTable"
                                        OnRowDataBound="grdMain_RowDataBound" ShowFooter="true" >
                                         <RowStyle CssClass="odd" />
                                        <AlternatingRowStyle CssClass="even" />                                       
                                       <PagerStyle  CssClass="classPager" />
                                         <PagerSettings Mode="NumericFirstLast" PageButtonCount="5"  FirstPageText="First" Position="Bottom" LastPageText="Last"/>   
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="20" HeaderStyle-HorizontalAlign="left" Visible="True">
                                                <HeaderStyle HorizontalAlign="left" Width="20" Font-Bold="true" />
                                                <ItemStyle HorizontalAlign="left" Width="20" />
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>.
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="left">
                                                <HeaderStyle HorizontalAlign="left" Width="50" Font-Bold="true" />
                                                <ItemStyle HorizontalAlign="left" Width="50" />
                                                <ItemTemplate>
                                                    <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Date"))) ? "" : (Convert.ToDateTime((Eval("Date"))).ToString("dd-MMM-yyyy")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Lorry No." HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="left">
                                                <HeaderStyle HorizontalAlign="left" Width="90" Font-Bold="true" />
                                                <ItemStyle HorizontalAlign="left" Width="90" />
                                                <ItemTemplate>
                                                   <asp:Label ID="lblLorry" runat="server" Text=' <%#Eval("LorryNo")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="left">
                                                <HeaderStyle HorizontalAlign="left" Width="90" Font-Bold="true" />
                                                <ItemStyle HorizontalAlign="left" Width="90" />
                                                <ItemTemplate>
                                                    <%#Eval("ItemName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Serial No." HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="left">
                                                <HeaderStyle HorizontalAlign="left" Width="90" Font-Bold="true" />
                                                <ItemStyle HorizontalAlign="left" Width="90" />
                                                <ItemTemplate>
                                                    <%#Eval("SerialNo")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Prev AlignDate" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="left">
                                                <HeaderStyle HorizontalAlign="left" Width="150" Font-Bold="true" />
                                                <ItemStyle HorizontalAlign="left" Width="150" />
                                                <ItemTemplate>
                                                  <asp:Label ID="lblPrevAlignDt" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("PrevAlignDate"))) ? "" : (Convert.ToDateTime((Eval("PrevAlignDate"))).ToString("dd-MMM-yyyy")))%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Align Date" HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="left">
                                                <HeaderStyle HorizontalAlign="left" Width="90" Font-Bold="true" />
                                                <ItemStyle HorizontalAlign="left" Width="90" />
                                                <ItemTemplate>
                                                   <asp:Label ID="lblAlignDt" runat="server" Text=' <%#(string.IsNullOrEmpty(Convert.ToString(Eval("AlignDate"))) ? "" : (Convert.ToDateTime((Eval("AlignDate"))).ToString("dd-MMM-yyyy")))%>'></asp:Label>
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
                                        <div class="col-sm-3" style="text-align:left"><asp:Label ID="lblcontant" runat="server"></asp:Label></div>  
                                        <div class="col-sm-4" style="text-align:right"></div>
                                        <div class="col-sm-1" style="text-align:right; width:9%"><asp:Label ID="lblWeight" runat="server"></asp:Label></div>
                                        <div class="col-sm-1" style="text-align:right; width:11%"><asp:Label ID="lblAmount" runat="server"></asp:Label></div>
                                        <div class="col-sm-1" style="text-align:right; width:6%"><asp:Label ID="lblshrtgQty" runat="server"></asp:Label></div>
                                        <div class="col-sm-1" style="text-align:right; width:7%"><asp:Label ID="lblshrtgAmnt" runat="server"></asp:Label></div>
                                        <div class="col-sm-1" style="text-align:right; width:8%"><asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label></div>
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
                <div class="col-lg-1">
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