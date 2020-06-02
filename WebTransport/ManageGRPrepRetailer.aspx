<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ManageGRPrepRetailer.aspx.cs" Inherits="WebTransport.ManageGRPrepRetailer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="row ">
                <div class="col-lg-1">
                </div>
                <div class="col-lg-12">
                    <section class="panel panel-default full_form_container part_purchase_bill_form">
                <header class="panel-heading font-bold">GR PREPARATION RETAILER LIST
                	<span class="view_print"><a href="GRPrepRetailer.aspx" >ADD GR</a>&nbsp;
                        <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ToolTip="Export to excel" ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px" OnClick="imgBtnExcel_Click" Visible="false" /></span>
               	</header>
                <div class="panel-body">
                <form class="bs-example form-horizontal">
                    <!-- first  section --> 
                    <div class="clearfix first_section">
	                    <section class="panel panel-in-default">  
	                        <div class="panel-body">
	                        <div class="clearfix odd_row">
                            <div class="col-sm-5" style="width: 34%">
                                <label class="col-sm-4 control-label" style="width: 24%;">Date Range<span class="required-field">*</span></label>
                                <div class="col-sm-8" style="width: 73%;">
                                <asp:DropDownList ID="ddlDateRange" runat="server" AutoPostBack="True" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged" TabIndex="1" >
                                </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-4" style="width: 30%">
                                <label class="col-sm-5 control-label" style="width: 26%;">Date From</label>
                                <div class="col-sm-7" style="width: 40%;">
                                    <asp:TextBox ID="Datefrom" runat="server" CssClass="input-sm datepicker form-control" MaxLength="6" TabIndex="2" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3" style="width: 30%">
                                <label class="col-sm-5 control-label" style="width:26%">Date To</label>
                                <div class="col-sm-7" style="width:40%">
                                    <asp:TextBox ID="Dateto" runat="server"  CssClass="input-sm datepicker form-control" MaxLength="50" TabIndex="3"></asp:TextBox>
                                </div>
                            </div>
                            </div>
                            <div class="clearfix even_row">
                            <div class="col-sm-5" style="width: 34%">
                                <label class="col-sm-4 control-label" style="width: 24%;">From City</label>
	                            <div class="col-sm-8" style="width: 73%;">
	                            <asp:DropDownList ID="drpCityFrom" runat="server" CssClass="form-control" TabIndex="4" >
                                </asp:DropDownList>
	                            </div>
                            </div>
                            <div class="col-sm-4" style="width: 30%">
                                <label class="col-sm-5 control-label" style="width: 26%;">To City</label>
	                            <div class="col-sm-7" style="width: 74%;">
	                                <asp:DropDownList ID="drpCityTo" runat="server" CssClass="chzn-select" style="width:100%;" TabIndex="5">
                                    </asp:DropDownList>              
	                            </div>
                            </div>
                            <div class="col-sm-3" style="width: 30%">
                                <label class="col-sm-5 control-label" style="width:26%">Deliv. Place</label>
	                            <div class="col-sm-7" style="width:74%">
	                            <asp:DropDownList ID="drpCityDelivery" runat="server" CssClass="chzn-select" style="width:100%;" TabIndex="6" >
                                </asp:DropDownList>
	                            </div>
                            </div>
                            </div>
                            <div class="clearfix odd_row">
                            <div class="col-sm-5" style="width: 34%">
                                <label class="col-sm-4 control-label" style="width: 24%;">GR No. Prefix</label>
                                   <div class="col-sm-8" style="width: 73%;">
	                            <asp:TextBox ID="txtPrefixNum" PlaceHolder="GR No. Prefix" runat="server" CssClass="form-control" MaxLength="50" TabIndex="7"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4" style="width: 30%">
                               <label class="col-sm-4 control-label" style="width: 26%;">GR No.</label>
                                   <div class="col-sm-8" style="width: 74%;">
	                            <asp:TextBox ID="txtGRNo" runat="server" PlaceHolder="Gr Number" CssClass="form-control" MaxLength="50" TabIndex="8"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3" style="width: 30%" >
                                 <label class="col-sm-5 control-label" style="width: 26%;">Sender</label>
	                            <div class="col-sm-7" style="width: 74%;">
	                                <asp:DropDownList ID="ddlSender" runat="server" CssClass="chzn-select" style="width:100%;" TabIndex="9">
                                    </asp:DropDownList>
	                            </div>
                            </div>
                            </div>
                             <div class="clearfix odd_row">
                              <div class="col-sm-5" style="width: 34%">
                                <label class="col-sm-4 control-label" style="width: 24%;">GR No. Prefix</label>
                                   <div class="col-sm-8" style="width: 73%;">
	                            <asp:TextBox ID="txtManNo" PlaceHolder="Manual No. Prefix" runat="server" CssClass="form-control" MaxLength="50" TabIndex="7"></asp:TextBox>
                                </div>
                            </div>                                                 
                                 <div class="col-sm-3" style="width: 40.5%; padding: 0;"> 
                                 <div class="col-sm-4 prev_fetch">
                                  <asp:LinkButton ID="lnkbtnPreview" CssClass="btn full_width_btn btn-sm btn-primary"  TabIndex="10" runat="server" OnClick="lnkbtnPreview_OnClick"><i class="fa fa-search-plus"></i>Preview</asp:LinkButton>
                                  </div>
                                  <div class="col-sm-8"> 
                                  <asp:Label ID="lblTotalRecord" runat="server" Text="T. Record(s) : 0" CssClass="control-label" ></asp:Label>                                   
                                  </div>
                                  </div>
	                        </div>
	                    </section>                        
                    </div>
                    <!-- second row -->
                <div id="gr_Report_form" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="popform_header">
                                    <asp:Label ID="lblGrDetails" runat="server">Gr Report</asp:Label>
                                </h4>
                            </div>
                        <div class="modal-body">
                            <section class="panel panel-default full_form_container material_search_pop_form">
						    <div class="panel-body" style="overflow:auto">
							    
						    <div class="clearfix odd_row">	 
                                <asp:GridView ID="grdReport"  runat="server" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="display nowrap dataTable"
                                    Width="100%" BorderStyle="None" BorderWidth="0"  >
                                    <AlternatingRowStyle CssClass="bgcolor2" />
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50" HeaderText="S.No.">
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle HorizontalAlign="Center" Width="50" />
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>.
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="130" HeaderText="Chln /Inv Date">
                                            <HeaderStyle HorizontalAlign="Left" Width="130px" />
                                            <ItemStyle HorizontalAlign="Left" Width="130" />
                                            <ItemTemplate>
                                                <%#Convert.ToDateTime(Eval("Chln_Date")).ToString("dd-MM-yyyy")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100" HeaderText="Chln No">
                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                            <ItemStyle HorizontalAlign="Center" Width="100" />
                                            <ItemTemplate>
                                                <%#Eval("Chln_No")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100" HeaderText="Adv Amnt">
                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                            <ItemStyle HorizontalAlign="Center" Width="100" />
                                            <ItemTemplate>
                                                <%#Eval("Adv_Amnt")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="130" HeaderText="TDS">
                                            <HeaderStyle HorizontalAlign="Center" Width="130px" />
                                            <ItemStyle HorizontalAlign="Center" Width="130" />
                                            <ItemTemplate>
                                                <%#Eval("TDSTax_Amnt")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100" HeaderText="Net Amnt">
                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                            <ItemStyle HorizontalAlign="Center" Width="100" />
                                            <ItemTemplate>
                                                <%#Eval("Net_Amnt")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100" HeaderText="Delievery">
                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                            <ItemStyle HorizontalAlign="Center" Width="100" />
                                            <ItemTemplate>
                                                <%#Eval("Delivered")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100" HeaderText="Delv Date">
                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                            <ItemTemplate>
                                                <%#Eval("Delvry_Date")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="120" HeaderText="Truck Owner">
                                            <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                            <ItemStyle HorizontalAlign="Left" Width="120" />
                                            <ItemTemplate>
                                                <%#Eval("Lorry_No")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="LblNoRecordFound" runat="server" CssClass="white_bg" Text="Record(s) not found."></asp:Label>
                                    </EmptyDataTemplate>
                                    <HeaderStyle CssClass="internal_heading" />
                                    <PagerStyle CssClass="white_bg" ForeColor="#000" HorizontalAlign="Center" />
                                    <RowStyle CssClass="bgcolrwhite" />
                                </asp:GridView>


                            </div>
                            </div>
                            </section>
                        </div>
                        </div>
                    </div>
            </div>
                     <div class="clearfix fourth_right">
                        <section class="panel panel-in-default btns_without_border">                            
                          <div class="panel-body">     
                            <div class="clearfix">
		                           <section class="panel panel-default full_form_container material_search_pop_form">
		                            <div class="panel-body" style="overflow:auto;">   
                                   <table>
                                       <tr>
                                       <td>  
                                      <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"
                                        Width="100%" GridLines="None" AllowPaging="true" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging"
                                        BorderWidth="0" OnRowCommand="grdMain_RowCommand" TabIndex="4" OnRowDataBound="grdMain_RowDataBound" ShowFooter="true">
                                        <RowStyle CssClass="odd" />
                                        <AlternatingRowStyle CssClass="even" />                                       
                                       <PagerStyle  CssClass="classPager" />
                                         <PagerSettings Mode="NumericFirstLast" PageButtonCount="5"  FirstPageText="First" Position="Bottom" LastPageText="Last"/>   
                                            <Columns>
                                            <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" Width="50" />
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>.
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GR No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                <ItemStyle HorizontalAlign="Center" Width="100" />
                                                <ItemTemplate>
                                                    <%#String.IsNullOrEmpty(Convert.ToString(Eval("GRRet_Pref"))) ? Eval("GrRet_No") :  Eval("GRRet_Pref")+"/"+ Eval("GrRet_No")%>
                                                    <asp:Label ID="lblGridNo" runat="server" Text='<%#Eval("GRRetHead_Idno") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GR Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Convert.ToDateTime(Eval("GRRet_Date")).ToString("dd-MM-yyyy")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GR Type" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("GR_Typ")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sender" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("Sender")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Receiver" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("Receiver")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="From City" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("CityFrom")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Via City" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                            <ItemTemplate>
                                                <%#Eval("CityVia")%>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To City" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("CityTo")%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltotal" Visible="true" Text="Grid Total" Font-Bold="true" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Tran. Detail" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("Lorry_No")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Owner Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("Owner_Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                <ItemStyle HorizontalAlign="Center" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("Qty")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Amount">
                                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Gross_Amnt")))%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblGrossAmnt" Font-Bold="true" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Net Amount">
                                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Net_Amount")))%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblAmount" Font-Bold="true" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                             <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                <ItemTemplate>                                               
                                                <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("GRRetHead_Idno") %>' CommandName="cmdedit" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lnkbtnDelete" CssClass="edit" runat="server" CommandArgument='<%#Eval("GRRetHead_Idno") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lnkbtnCsv" AlternateText="CSV"  CssClass="edit" runat="server" CommandArgument='<%#Eval("GRRetHead_Idno") %>' CommandName="cmdexport" ToolTip="GR Report"><i class="fa fa-file-excel-o"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lnkbtnchallan" runat="server"  CssClass="edit" ToolTip="Challan generated" ><i class="fa fa-copyright"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lnkbtnSold" runat="server"  ToolTip="Invoice generated" ><i class="fa fa-usd"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lnkbtnPay" runat="server"  CssClass="edit" ToolTip="Pay To Driver" CommandArgument='<%#Eval("GRRetHead_Idno") %>' CommandName="Pay" ><i class="fa fa-pied-piper"></i></asp:LinkButton>
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
                                        <div class="col-sm-4" style="text-align:left"><asp:Label ID="lblcontant" runat="server"></asp:Label></div>  
                                        <div class="col-sm-4" style="text-align:right">TOTAL</div>
                                        <div class="col-sm-2" style="text-align:right"><asp:Label ID="lblNetGrossTotalAmount" runat="server"></asp:Label></div>
                                        <div class="col-sm-1" style="text-align:right;width:10%"><asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label></div>
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
                    <td id="trprint" runat="server" visible="false">
                        <asp:GridView ID="grdprint" runat="server" AutoGenerateColumns="false" BorderStyle="None"
                            Width="100%" GridLines="None" AllowPaging="false" PageSize="25" HeaderStyle-CssClass="internal_heading"
                            BorderWidth="0" RowStyle-CssClass="bgcolrwhite" AlternatingRowStyle-CssClass="bgcolor2">
                            <AlternatingRowStyle CssClass="bgcolor2" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>.
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quotation No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                    <ItemTemplate>
                                        <%#Eval("QuHead_No")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                    <ItemTemplate>
                                        <%#Convert.ToDateTime(Eval("QuHead_Date")).ToString("dd-MM-yyyy")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="From City" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                    <ItemTemplate>
                                        <%#Eval("CityFrom")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="From To" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                    <ItemTemplate>
                                        <%#Eval("CityTo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Via City" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                    <ItemTemplate>
                                        <%#Eval("CityVia")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delivery" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                    <ItemTemplate>
                                        <%#Eval("CityDely")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sender" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                    <ItemTemplate>
                                        <%#Eval("Sender")%>
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
                    </td>
                </div>
            </div>
            <asp:HiddenField ID="hidmindate" runat="server" />
            <asp:HiddenField ID="hidmaxdate" runat="server" />
            <asp:HiddenField ID="hidrcptheadidno" runat="server" />
             <asp:HiddenField ID="hidTranTypeIdno" runat="server" />
            <div id="overlay" class="web_dialog_overlay" style="height: auto;">
            </div>
            <script type="text/javascript">                $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
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

        function ShowModalPopup() {
            ShowDialog(true);
        }
        function ShowDialog(modal) {
            $("#overlay").show();
            $("#dialog").fadeIn(300);

            if (modal) {
                $("#overlay").unbind("click");
            }
            else {
                $("#overlay").click(function (e) {
                    HideDialog();
                });
            }
        }

        function HideDialog() {
            $("#overlay").hide();
            $("#dialog").fadeOut(300);
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

        function openModalGrdReport() {
            $('#gr_Report_form').modal('show');
        }

        function ShowModalPopup() {
            ShowDialog(true);
        }
        function ShowDialog(modal) {
            // $("#overlay").show();
            $("#dialog").show();
            $("#dialog").fadeIn(300);

            if (modal) {
                $("#dialog").unbind("click");
            }
            else {
                $("#dialog").click(function (e) {
                    HideDialog();
                });
            }
        }
        function HideDialog() {
            $("#dialog").fadeOut(300);
        }
        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $("#<%=Datefrom.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
            $("#<%=Dateto.ClientID %>").datepicker({
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
<asp:Content ID="Content4" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
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
