<%@ Page Title="Challan Report" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="ChlnRpt.aspx.cs" EnableEventValidation="false" Inherits="WebTransport.ChlnRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-1">
                </div>
                <div class="col-lg-10">
                    <section class="panel panel-default full_form_container part_purchase_bill_form">
                	<header class="panel-heading font-bold">Challan Report
                		<span class="view_print">
                        <a href="" ></a><asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ToolTip="Export to excel" ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px" OnClick="imgBtnExcel_Click" Visible="false" />
                         &nbsp;
                     <asp:LinkButton ID="lnkbtnPrint" runat="server" ToolTip="Click to print" Visible="false"  OnClick="lnkbtnPrint_Click"><i class="fa fa-print icon"></i></asp:LinkButton>                    
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
                                      <asp:TextBox ID="txtDateFrom" runat="server" CssClass="input-sm datepicker form-control" MaxLength="12"
                                                                          data-date-format="dd-mm-yyyy" TabIndex="2" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDateFrom" runat="server" ControlToValidate="txtDateFrom"
                                                            SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage=" Please Enter Date!"
                                                            Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-3" style="width: 28.5%">
                                  <label class="col-sm-5 control-label">Date To</label>
                                    <div class="col-sm-7">
                                      <asp:TextBox ID="txtDateTo" runat="server" CssClass="input-sm datepicker form-control" MaxLength="12"
                                                                            TabIndex="3" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDateTo" runat="server" ControlToValidate="txtDateTo"
                                                            SetFocusOnError="true" ValidationGroup="Previw" ErrorMessage=" Please Enter Date!"
                                                            Display="Dynamic" CssClass="redfont"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                              </div>
                              <div class="clearfix even_row">
                                <div class="col-sm-5" style="width: 37%">
                                  <label class="col-sm-4 control-label" style="width: 29%;">Loc.[From]</label>
	                                <div class="col-sm-8" style="width: 66%;">
	                                <asp:DropDownList ID="drpBaseCity" runat="server" CssClass="form-control"  TabIndex="4">
                                                                    </asp:DropDownList>
	                                </div>
                                </div>
                                <div class="col-sm-4" style="width: 28.5%">
                                  <label class="col-sm-5 control-label" style="width: 39%;">Lorry No.</label>
	                                <div class="col-sm-7" style="width: 61%;">
	                                <asp:DropDownList ID="ddlTruckNo" runat="server" CssClass="form-control" TabIndex="5">
                                                                    </asp:DropDownList>
                                                                    
	                                </div>
                                </div>
                                 <div class="col-sm-4" style="width: 28.5%">
                                  <label class="col-sm-5 control-label" style="width: 39%;">Lorry Type</label>
	                                <div class="col-sm-7" style="width: 61%;">
	                                 <asp:DropDownList ID="ddllorrytype" runat="server" CssClass="form-control" TabIndex="6">
                                     <asp:ListItem Text="All" Value="2"></asp:ListItem>
                                     <asp:ListItem Text="Own" Value="0"></asp:ListItem>
                                     <asp:ListItem Text="Hire" Value="1"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    
	                                </div>
                                </div>
                               
                              </div>
                               <div class="clearfix odd_row">
                                <div>
                              <div class="col-sm-5" style="width: 37%">
                               <label class="col-sm-4 control-label" style="width: 29%;">Party</label>
	                                <div class="col-sm-8" style="width: 66%;">
	                                 <asp:DropDownList ID="ddlPartyName" runat="server" CssClass="form-control" TabIndex="7">
                                     </asp:DropDownList>                        
	                                </div>
                              </div>
                              </div>
                              <div>
                              <div class="col-sm-4" style="width: 28.5%">
                               <label class="col-sm-5 control-label" style="width: 39%;">Report Type</label>
	                                <div class="col-sm-7" style="width: 61%;">
	                                 <asp:DropDownList ID="ddlReportType" runat="server" CssClass="form-control" TabIndex="8">
                                      <asp:ListItem Text="Chln Wise Summary" Value="0"></asp:ListItem>
                                     <asp:ListItem Text="GR Wise Summary" Value="1"></asp:ListItem>
                                     </asp:DropDownList>
                                                                    
	                                </div>
                              </div>
                              </div>
                              <div><div class="col-sm-4" style="width: 34.5%">
                               <label class="col-sm-4 control-label" style="width: 32%;">Destination</label>
	                                <div class="col-sm-8" style="width: 68%;">
                                    <div class="col-sm-9" style="width: 75%;">
	                                 <asp:DropDownList ID="ddlDestination" runat="server" CssClass="form-control" TabIndex="9">                                     
                                     </asp:DropDownList> 
                                     </div>
                                      <label class="col-sm-2 control-label" style="width: 15%;">Rate</label>
	                                <div class="col-sm-1" style="width: 5%;">
	                                <asp:CheckBox ID="CheckBox1" runat="server" TabIndex="6"></asp:CheckBox>                                                                    
	                                </div>                                                                   
	                                </div>
                              </div></div>
                              </div>
                              <div class="clearfix even_row">
                            <%-- <div class="col-sm-3" style="width: 28.5%; padding-left:5";>
                                  <div class="col-sm-6 prev_fetch">
                                  
                                    <asp:LinkButton ID="lnkbtnPreview" CssClass="btn full_width_btn btn-sm btn-primary"  TabIndex="9" runat="server" OnClick="lnkbtnPreview_OnClick"><i class="fa fa-search-plus"></i>Preview</asp:LinkButton>
                                  </div>
                                  <div class="col-sm-6"> 
                                     <b><asp:Label ID="lblTotalRecord" runat="server" Text="T. Record(s) : 0" CssClass="control-label" ></asp:Label></b>
                                  </div>
                                </div>--%>
                                <div class="col-lg-5">
                              </div>
                              <div class="col-sm-2 prev_fetch">
                                  
                                    <asp:LinkButton ID="lnkbtnPreview" CssClass="btn full_width_btn btn-sm btn-primary"  TabIndex="10" runat="server" OnClick="lnkbtnPreview_OnClick"><i class="fa fa-search-plus"></i>Preview</asp:LinkButton>
                                  </div>
                                  <div class="col-sm-3"> 
                                     <b><asp:Label ID="lblTotalRecord" runat="server" Text="T. Record(s) : 0" CssClass="control-label" ></asp:Label></b>
                                  </div>
                              <div class="col-lg-5">
                              </div>
                                </div>
	                          </div>
	                        </section>                        
                      	</div>

                       
		                <div class="panel-body" id="divPrint" style="overflow:auto;height:500px">  
                          <table>
                          <tr id="CompanyDetails" style=" display:none;">
                                       
                                            <td align="center" style="font-size: 18px; font-family: Arial,Helvetica,sans-serif;"
                                                class="white_bg" width="300px">
                                                &nbsp;<strong><asp:Label ID="lblCompName" runat="server" Text="" Font-Size="18px"></asp:Label></strong><br />
                                                &nbsp;<asp:Label ID="lblAddress" runat="server" Text="" Font-Size="14px"></asp:Label><br />
                                                &nbsp;<asp:Label ID="lblCity" runat="server" Text="" Font-Size="14px"></asp:Label>&nbsp;<asp:Label
                                                    ID="lblState" runat="server" Text="" Font-Size="14px"></asp:Label>&nbsp;<asp:Label
                                                        ID="lblpincode" runat="server" Text="" Font-Size="14px"></asp:Label><br />
                                                &nbsp;<asp:Label ID="lblPhone" Visible="false" runat="server" Text="" Font-Size="14px"></asp:Label><br />
                                                 <asp:Label  ID="lblDate" style="text-align:right" runat="server" Font-Size="14px"></asp:Label>
                                            </td>
                                            
                             <%--<td align="center" style="font-size: 18px; font-family: Arial,Helvetica,sans-serif;"
                                                class="white_bg" width="300px" >
                                                <asp:Label  ID="lblDate" runat="server" Font-Size="14px"></asp:Label>
                                                </td>--%>
                             
                                            <caption>
                                                <br />
                                            </caption>
                           
                            
                            
                            </tr>
                            <tr>
                            <td>     
                             
                            <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable" GridLines="None" BorderWidth="0" ShowFooter="true" OnRowCommand="grdMain_RowCommand"  OnRowDataBound="grdMain_RowDataBound">
                            <RowStyle CssClass="odd" />
                            <AlternatingRowStyle CssClass="even" />                                       
                            <PagerStyle  CssClass="classPager" />
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5"  FirstPageText="First" Position="Bottom" LastPageText="Last"/>   
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="center"
                                        Visible="true">
                                        <ItemStyle HorizontalAlign="center" Width="40" />
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>.
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="center" Width="40" ForeColor="White" Font-Bold="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center" Width="80" />
                                        <ItemTemplate>
                                            <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Chln_Date"))) ? "" : (Convert.ToDateTime((Eval("Chln_Date"))).ToString("dd-MMM-yyyy")))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Chln. No." HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" Width="70" />
                                        <ItemTemplate>
                                             <%#Eval("Chln_No")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GR. No" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" Width="70" />
                                        <ItemTemplate>
                                           <%#Eval("Gr_No")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GR Date" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" Width="70" />
                                        <ItemTemplate>
                                            <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Gr_Date"))) ? "" : (Convert.ToDateTime((Eval("Gr_Date"))).ToString("dd-MMM-yyyy")))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GR. Amnt" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" Width="80" />
                                        <ItemTemplate>
                                            <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Gr_Amnt"))) ? "" : Convert.ToString((Convert.ToDouble((Eval("Gr_Amnt"))).ToString("N2"))))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left" Width="80" />
                                        <ItemTemplate>
                                            <%#Eval("City_Name")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Destination" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left" Width="80" />
                                        <ItemTemplate>
                                            <%#Eval("To_City")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Transporter Name" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left" Width="80" />
                                        <ItemTemplate>
                                            <%#Eval("PartyName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Truck No." HeaderStyle-Width="120" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" Width="120" />
                                        <ItemTemplate>
                                            <%#Eval("Lorry_No")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Truck Type" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center" Width="70" />
                                        <ItemTemplate>
                                            <%#string.IsNullOrEmpty(Eval("Lorry_Type").ToString() == "0" ? "OWN" : "Hire") ? "" : Eval("Lorry_Type").ToString() == "0" ? "OWN" : "Hire"%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="DI No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">                         
                                        <ItemStyle HorizontalAlign="Left" Width="50" />
                                        <ItemTemplate>
                                            <%#Eval("DI_NO")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EGP No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">                         
                                        <ItemStyle HorizontalAlign="Left" Width="50" />
                                        <ItemTemplate>
                                            <%#Eval("EGP_NO")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">                         
                                        <ItemStyle HorizontalAlign="Left" Width="50" />
                                        <ItemTemplate>
                                            <%#Eval("Inv_No")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Receiver Name" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">                         
                                        <ItemStyle HorizontalAlign="Left" Width="50" />
                                        <ItemTemplate>
                                            <%#Eval("RecieverName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="E-Way Bill No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">                         
                                        <ItemStyle HorizontalAlign="Left" Width="50" />
                                        <ItemTemplate>
                                            <%#Eval("EWay_BillNO")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="E-Way Bill No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">                         
                                        <ItemStyle HorizontalAlign="Left" Width="50" />
                                        <ItemTemplate>
                                            <%#Eval("EWay_BillNO")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Rate" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                        <ItemStyle HorizontalAlign="right" Width="70" />
                                        <ItemTemplate>
                                            <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Item_Rate"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Item_Rate")))))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Diesel Amount" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                        <ItemStyle HorizontalAlign="right" Width="70" />
                                        <ItemTemplate>
                                            <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Diesel_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Diesel_Amnt")))))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="right" />
                                        <FooterTemplate>
                                            <b><asp:Label ID="lblDieselamnt" runat="server"></asp:Label></b>
                                        </FooterTemplate>
                                     </asp:TemplateField>  
                                            <asp:TemplateField HeaderText="Tot Weight" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                        <ItemStyle HorizontalAlign="right" Width="70" />
                                        <ItemTemplate>
                                            <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Tot_Weight"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Tot_Weight")))))%>
                                        </ItemTemplate>
                                    <FooterStyle HorizontalAlign="right" />
                                        <FooterTemplate>
                                            <b><asp:Label ID="lblFtWeight" runat="server"></asp:Label></b>
                                        </FooterTemplate>
                                    </asp:TemplateField>                       
                                        <asp:TemplateField HeaderText="Adv. Amount" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                        <ItemStyle HorizontalAlign="right" Width="70" />
                                        <ItemTemplate>
                                            <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Adv_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Adv_Amnt")))))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="right" />
                                        <FooterTemplate>
                                            <b><asp:Label ID="lblAmount" runat="server"></asp:Label></b>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gross.Amnt" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="right">

                                        <ItemStyle HorizontalAlign="right" Width="50" />
                                        <ItemTemplate>
                                            <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Gross_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Gross_Amnt")))))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="right" />
                                        <FooterTemplate>
                                            <b><asp:Label ID="lblgross" runat="server"></asp:Label></b>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TDS Amnt." HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                        <ItemStyle HorizontalAlign="right" Width="70" />
                                        <ItemTemplate>
                                        <%#(string.IsNullOrEmpty(Convert.ToString(Eval("TDS_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("TDS_Amnt")))))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="right" />
                                        <FooterTemplate>
                                            <b><asp:Label ID="lblTDSTax" runat="server"></asp:Label></b>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="B. C." HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                        <ItemStyle HorizontalAlign="right" Width="70" />
                                        <ItemTemplate>
                                            <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Comm_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Comm_Amnt")))))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <b><asp:Label ID="lblFTCommi" runat="server"></asp:Label></b>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Net Amnt" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                        <ItemStyle HorizontalAlign="right" Width="70" />
                                        <ItemTemplate>
                                            <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Net_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Net_Amnt")))))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <b><asp:Label ID="lblNetAmnt" runat="server"></asp:Label></b>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shrt. Qty" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                        <ItemStyle HorizontalAlign="right" Width="70" />
                                        <ItemTemplate>
                                            <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Shortage_Qty"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Shortage_Qty")))))%>
                                        </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <b><asp:Label ID="lblFTShrtQty" runat="server"></asp:Label></b>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shrt. Amnt" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="right">
                                        <ItemStyle HorizontalAlign="right" Width="70" />
                                        <ItemTemplate>
                                            <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Shortage_Amount"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Shortage_Amount")))))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <b><asp:Label ID="lblShrtAmnt" runat="server"></asp:Label></b>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Owner Name" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left" Width="80" />
                                        <ItemTemplate>
                                            <%#Eval("Owner_Name")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pan No" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left" Width="80" />
                                        <ItemTemplate>
                                            <%#Eval("Pan_No")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Recvd Amnt" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left" Width="80" />
                                        <ItemTemplate>
                                                 <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Recvd_Amnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("Recvd_Amnt")))))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Balance Amnt" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left" Width="80" />
                                        <ItemTemplate>
                                                 <%#(string.IsNullOrEmpty(Convert.ToString(Eval("BalanceAmnt"))) ? "0.00" : (string.Format("{0:0,0.00}", Convert.ToDouble(Eval("BalanceAmnt")))))%>
                                     
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
                                <br /> &nbsp;
                            </td>
                            </tr>
                            </table>
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
    <%--<script type="text/javascript" language="javascript">
        function Call() {
            var contents = $("#divPrint").html();
            var frame1 = $('<iframe />');
            frame1[0].name = "frame1";
            $("body").append(frame1);
            var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
            frameDoc.document.open();
            //Create a new HTML document.
            frameDoc.document.write('<html><head><title>Chalan Report Print</title>');
            //Append the external CSS file.
            frameDoc.document.write('<link rel="stylesheet" href="css/bootstrap.min-3.3.css">');
            frameDoc.document.write('<link rel="stylesheet" href="js/datatables/jquery.dataTables.min.css">');
            //frameDoc.document.write('<link rel="stylesheet" href="js/datatables/jquery.dataTables.min.js">');
            frameDoc.document.write('</head>');
            frameDoc.document.write('<body> <div id="page-content"> <div class="dash-banner"> <div class="row"> <div class="col-lg-1"> </div> <div class="col-lg-10"> <div class="panel-body">');
            //Append the DIV contents.
            frameDoc.document.write(contents);
            frameDoc.document.write('</div> </div> </div> </div> </div></body></html>');

            var s = contents;
            frameDoc.document.close();
            setTimeout(function () {
                window.frames["frame1"].focus();
                window.frames["frame1"].print();
                frame1.remove();
            }, 500);
        }
    </script>--%>
     <script type="text/javascript" language="javascript">
         function CallPrint(strid) {
             var FrmDt = document.getElementById('<%= this.txtDateFrom.ClientID %>');
             var ToDt = document.getElementById('<%= this.txtDateTo.ClientID %>');
             var prtContent1 = "<table width='100%' border='0' ></table>";

             var prtContent = document.getElementById(strid);
             var WinPrint = window.open('', '', 'letf=0,top=0,width=800,height=600,toolbar=0,scrollbars=0,status=0');
             $("#CompanyDetails").show();
             WinPrint.document.write(prtContent1);
             WinPrint.document.write(prtContent.innerHTML);
             WinPrint.document.close();
             WinPrint.focus();
             WinPrint.print();
             WinPrint.close();
             $("#CompanyDetails").hide();
         }
         var isShift = false;
         var seperator = "-";
         function DateFormat(txt, keyCode) {
             if (keyCode == 16)
                 isShift = true;
             //Validate that its Numeric
             if (((keyCode >= 48 && keyCode <= 57) || keyCode == 8 ||
         keyCode <= 37 || keyCode <= 39 ||
         (keyCode >= 96 && keyCode <= 105)) && isShift == false) {
                 if ((txt.value.length == 2 || txt.value.length == 5) && keyCode != 8) {
                     txt.value += seperator;
                 }
                 return true;
             }
             else {
                 return false;
             }
         }
         


         
    </script>
</asp:Content>