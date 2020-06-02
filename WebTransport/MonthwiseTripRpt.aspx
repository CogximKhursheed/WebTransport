<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
EnableEventValidation="false"
 CodeBehind="MonthwiseTripRpt.aspx.cs" Inherits="WebTransport.MonthwiseTripRpt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row ">
            <div class="col-lg-1"></div>
            <div class="col-lg-10">
              <section class="panel panel-default full_form_container part_purchase_bill_form">
                	<header class="panel-heading font-bold">CONSOLIDATED TRIP REPORT
                		<span class="view_print">&nbsp;  
                <asp:ImageButton ID="imgBtnExcel" 
                    runat="server" AlternateText="Excel" ToolTip="Export to excel"
                            ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px" 
                            Visible="false" onclick="imgBtnExcel_Click"  />  </span>
               	 	</header>
                	<div class="panel-body">
                    <form class="bs-example form-horizontal">
                      <!-- first  section --> 
                      	<div class="clearfix first_section">
	                        <section class="panel panel-in-default">  
	                          <div class="panel-body">
	                            <div class="clearfix odd_row">
	                            	<div class="col-sm-5">
                                  <label class="col-sm-4 control-label">DateRange</label>
                                  <div class="col-sm-8">
                                     <asp:DropDownList ID="ddlDateRange" runat="server" AutoPostBack="True" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged" TabIndex="1" >
                                    </asp:DropDownList>
                                  </div>
                                </div>
                                <div class="col-sm-3">
                                  <label class="col-sm-5 control-label">Month</label>
                                  <div class="col-sm-7">
                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control"  TabIndex="5">
                                    <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Febuary" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="Octomber" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                    </asp:DropDownList>     
                                  </div>
                                </div>		                                
                                <div class="col-sm-4">                                  
                                  <label class="col-sm-5 control-label">Truck No.<span class="required-field">*</span></label>
                                  <div class="col-sm-7">
                                     <asp:DropDownList ID="ddlTruckNo" runat="server" CssClass="form-control"  TabIndex="5"></asp:DropDownList>     
                                  </div>
                                </div>
                            
                            	</div>
                            	<div class="clearfix even_row">
	                            	<div class="col-sm-4">
                          
                                </div>
                                <div class="col-sm-4">
                                
                                </div>		                                
                                <div class="col-sm-4">
                                 <div class="col-sm-5"> 
                                  </div>
                                   <div class="col-sm-7 prev_fetch">
                                  <asp:LinkButton runat="server" ID="lnkBtnPreview" 
                                          CssClass="btn full_width_btn btn-sm btn-primary" 
                                          onclick="lnkBtnPreview_Click" TabIndex="8" ><i class="fa fa-search-plus"></i>Preview</asp:LinkButton>
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
		                            <div class="panel-body">                                   
		                               <!-- First Row start -->  
		                            
                                        <div style="overflow-x:auto;">
                                        <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="false" BorderStyle="None"
                                            Width="100%" GridLines="None" ShowFooter="true"
                                            CssClass="display nowrap dataTable" BorderWidth="0" 
                                           OnRowCommand="grdMain_RowCommand" TabIndex="10"
                                            OnPageIndexChanging="grdMain_PageIndexChanging" PageSize="50" 
                                            onrowdatabound="grdMain_RowDataBound">
                                                     
                                            <FooterStyle ForeColor="Black" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="35" HeaderStyle-HorizontalAlign="center"
                                                    Visible="true">
                                                    <ItemStyle HorizontalAlign="center" Width="35" />
                                                    <ItemTemplate>
                                                         <%#Eval("SrNo")%>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="center" Width="35" ForeColor="White" Font-Bold="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sender" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" Width="80" />
                                                    <ItemTemplate>
                                                        <%#Eval("Acnt_Name")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gr No" HeaderStyle-Width="80"  Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" Width="80" />
                                                    <ItemTemplate>
                                                    <%#Eval("Gr_No")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inv. No" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" Width="80" />
                                                    <ItemTemplate>
                                                        <%#Eval("Chln_No")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inv. Date" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" Width="80" />
                                                    <ItemTemplate>
                                                     <%#Eval("Gr_Date")%>
                                                      <%--   <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Gr_Date"))) ? "" : (Convert.ToDateTime((Eval("Gr_Date"))).ToString("dd-MMM-yyyy")))%>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="From City" HeaderStyle-Width="250" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" Width="250" />
                                                    <ItemTemplate>
                                                        <%#Eval("FromCity")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To City" HeaderStyle-Width="90" FooterStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" Width="90" />
                                                    <ItemTemplate>
                                                        <%#Eval("ToCity")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="From Date" HeaderStyle-Width="250" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" Width="250" />
                                                    <ItemTemplate>
                                                        <%#string.IsNullOrEmpty(Convert.ToString(Eval("DateFrom")))?"":Convert.ToDateTime(Eval("DateFrom")).ToString("dd-MM-yy")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Return Date" HeaderStyle-Width="90" FooterStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" Width="90" />
                                                    <ItemTemplate>
                                                        <%#string.IsNullOrEmpty(Convert.ToString(Eval("ReturnDate"))) ? "" : Convert.ToDateTime(Eval("ReturnDate")).ToString("dd-MM-yy")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Net Amnt"   >
                                                    <ItemStyle CssClass="gridHeaderAlignRight" Width="100px" />
                                                    <HeaderStyle  CssClass="gridHeaderAlignRight" Width="100px" />
                                                    <ItemTemplate>
                                                       <%#Eval("Net_Amnt")%>  
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                       
                                        
                                          
                                            </Columns>
                                            <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                         
                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                            <EmptyDataTemplate>
                                                <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                            </EmptyDataTemplate>
                                            <PagerStyle CssClass="white_bg" HorizontalAlign="Center" />
                                           


                                        </asp:GridView>
                                            </div>
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
            <div class="col-lg-1"></div>
          </div>
    
    <asp:HiddenField ID="hidmindate" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />
    <script language="javascript" type="text/javascript">
        SetFocus();
        function SetFocus() {
            $('input[type="text"]').focus(function () {
                $(this).addClass("focus");
            });
            $('input[type="text"]').blur(function () {
                $(this).removeClass("focus");
            });
            $("select").focus(function () {
                $(this).addClass("focus");
            });
            $("select").blur(function () {
                $(this).removeClass("focus");
            });
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_beginRequest(function () {
            SetFocus();
            setDatecontrol();
        });

        prm.add_endRequest(function () {
            SetFocus();
            setDatecontrol();
        });


        function ShowMessage(value) {
            alert(value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
    
</asp:Content>
