<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="PartyDetailReportArv.aspx.cs" Inherits="WebTransport.PartyDetailReportArv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #grdMain td,#grdMain th
        {
            font-size: 11px;
            padding: 4px;
            border: 1px solid #8c8c8c !important;
        }
        .input-group {
            border: none !important;
        }
        .main-form
        {
            margin-top:10px;
        }
        .bottom-division
        {
            border-bottom: 1px solid silver;
            margin: 5px 0;
        }
            
    </style>
    <link href="css/JumpingFrog-Placeholder.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row ">
        <div class="col-lg-12">
            <section class="panel panel-default full_form_container part_purchase_bill_form main-form">
                	<header class="panel-heading font-bold">Party Detail Report <span style="font-family:Times New Roman">II</span> <small style="font-weight:300">(Party account details)</small>
                    	<span class="view_print">
                             <asp:ImageButton ID="imgBtnExcel" runat="server" Visible="false" AlternateText="Excel" ImageUrl="~/Images/CSV.png" OnClick="imgBtnExcel_Click" ToolTip="Export to excel" />
                             <asp:LinkButton ID="lnkbtnPrint" runat="server" title="Print" ToolTip="Click to print" Visible="false" CssClass="fa fa-print icon" OnClientClick ="return CallPrint('print');"></asp:LinkButton>
                        </span>
               	 	</header>
                	<div class="panel-body">
                    <form class="bs-example form-horizontal">
                      <!-- first  section --> 
                      	<div class="clearfix first_section">
	                        <section class="pane">  
	                          <div class="panel-body bottom-division">
	                            <div class="col-sm-6 clm">
                              <div class="col-sm-6">
                                  <div class="input-container">
                                        <div class="input-group virtual-placeholder" data-placeholder-text="Financial Year">
                                                                                <asp:DropDownList ID="ddlDateRange" runat="server" AutoPostBack="true" CssClass="form-control placeholder"
                                         TabIndex="1"  OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged">
                                    </asp:DropDownList> 
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                    ControlToValidate="ddlDateRange" ValidationGroup="save" ErrorMessage="Please select date range."
                                    InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>    
                                        </div>
                                    </div>
                                       </div>
                                       <div class="col-sm-6" style="display:none">
	                              <div class="input-container">
                                        <div class="input-group virtual-placeholder" data-placeholder-text="Location">
                                     <asp:DropDownList ID="drpBaseCity" runat="server" CssClass="form-control placeholder" TabIndex="5">
                                    </asp:DropDownList>  
	                                  </div>                       
	                            </div>  
                                </div>
                                <div class="col-sm-6">
                             
                                    <div class="input-container">
                                        <div class="input-group virtual-placeholder" data-placeholder-text="Party">
                                      <asp:DropDownList ID="ddlParty" runat="server" CssClass="form-control placeholder" TabIndex="4" >
                                    </asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlParty"
                                        CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select Party" InitialValue="0"
                                        SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                                        </div>                         
	                                </div>
                                 </div>
                                  </div>
                                   
                               <div class="col-sm-6 clm">
                               <div class="col-sm-6">
                                    <div class="input-container">
                                        <div class="input-group virtual-placeholder" data-placeholder-text="Date From">
                                             <asp:TextBox ID="txtDateFrom" runat="server" CssClass="input-sm datepicker form-control placeholder" MaxLength="12"  TabIndex="2"  ></asp:TextBox>
                                        </div>
                                    </div>
                                     </div>
                                 <div class="col-sm-6">
                                    <div class="input-container">
                                        <div class="input-group virtual-placeholder" data-placeholder-text="Date To">
                                     <asp:TextBox ID="txtDateTo" runat="server" CssClass="input-sm datepicker form-control placeholder" MaxLength="12"
                                                                            TabIndex="3"  ></asp:TextBox>
                                 
                                    </div>
                                    </div>
                                </div>
                              <div class="col-sm-6">
                                 <asp:Label ID="lblTotalRecord" style="margin-top:20px;display:block;" runat="server" Text="T. Record(s) : 0" CssClass="control-label full_width_btn" Visible="true" ></asp:Label>                                   
                              </div>
                              <div class="col-sm-6">
                                  <asp:LinkButton ID="lnkbtnPreview" CssClass="btn full_width_btn btn-sm btn-primary" style="margin-top:20px;" CausesValidation="true" ValidationGroup="save" TabIndex="7" runat="server" OnClick="lnkbtnPreview_OnClick"><i class="fa fa-search-plus"></i>Preview</asp:LinkButton>
                              </div>
	                        </section>                        
                      	</div>

                       <!-- second row -->
                        <div class="clearfix fourth_right">
                        <section class="btns_without_border">                            
                          <div class="panel-body">     
                            <div class="clearfix">
		                           <section class="full_form_container material_search_pop_form">
		                            <div class="" style="overflow:auto;height:350px;">   
                                   <table>
                                       <tr>
                                       <td>  
                                       <asp:GridView ID="grdMain" ClientIDMode="Static" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"
                                        Width="100%" GridLines="None" AllowPaging="false" PageSize="50"  OnRowDataBound="grdMain_RowDataBound"
                                        BorderWidth="0" ShowFooter="true">
                                        <RowStyle CssClass="odd" />
                                        <AlternatingRowStyle CssClass="even" />                                       
                                       <PagerStyle  CssClass="classPager" />
                                         <PagerSettings Mode="NumericFirstLast" PageButtonCount="5"  FirstPageText="First" Position="Bottom" LastPageText="Last"/>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No" HeaderStyle-HorizontalAlign="center"
                                            Visible="true">
                                            <HeaderStyle HorizontalAlign="center" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>.
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%#Eval("Chln_Date")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="C.No." HeaderStyle-HorizontalAlign="center">
                                            <HeaderStyle HorizontalAlign="center" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <%#Eval("Chln_No")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gr No." HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Eval("GrNo")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="From  City" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Eval("FromCity")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="To  City" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Eval("ToCity")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lorry" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                              <asp:Label ID="lblT" runat="server" Text='  <%#Eval("LorryNo")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                           <asp:TemplateField HeaderText="Item Name" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Eval("ItemName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="Total Weight" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                             <asp:Label ID="lblTotalAmnt" runat="server" Text='<%#Eval("Weight")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left"  Visible=false>
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                 <asp:Label ID="lblTotalAt" runat="server" Text='<%#Eval("Qty")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Rate" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                               <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("Rate")%>'></asp:Label> 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%#Eval("Amount")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Advance" HeaderStyle-HorizontalAlign="right" >
                                            <HeaderStyle HorizontalAlign="right" Font-Bold="true"/>
                                            <ItemStyle HorizontalAlign="right" />
                                            <ItemTemplate>
                                                <%#Eval("Advance")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Diesel Amount" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Eval("Diesel_Amnt")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Comission" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%#Eval("Commission")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="TDS" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%#Eval("TDS")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="ReceivedWeight" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%#Eval("ReceivedWeight")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Shortage" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%#Eval("Shortage")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shortage Amount" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%#Eval("ShortageAmt")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Loading" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                            <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received Amount" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                            <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Right"  />
                                            <ItemTemplate>
                                                <%#Eval("ReceivedAmnt")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Bal. Amt." HeaderStyle-HorizontalAlign="Left" Visible="false">
                                            <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%#Eval("BalanceAmount")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bal. Amt. W. Shortg." HeaderStyle-HorizontalAlign="Left" Visible="false">
                                            <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%#Eval("BalanceAmountWithShortage")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Net Total" HeaderStyle-HorizontalAlign="Left" >
                                            <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%#Eval("RowNet_Total")%>
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
                                        <div class="col-sm-2" style="text-align:left;width:25%"><asp:Label ID="lblcontant" runat="server"></asp:Label></div>  
                                        <div class="col-sm-1">TOTAL &nbsp;&nbsp;&nbsp;</div>
                                        <div class="col-sm-1" style="width:6%;text-align:left"><asp:Label ID="lblTotOpening" runat="server"></asp:Label></div>
                                        <div class="col-sm-1" style="width:7%;text-align:left"><asp:Label ID="lblTotPur" runat="server"></asp:Label></div>
                                         <div class="col-sm-1" style="width:10.5%;text-align:left"><asp:Label ID="lblTotRecv" runat="server"></asp:Label></div>
                                        <div class="col-sm-1" style="width:7%;text-align:left"><asp:Label ID="lblTotal" runat="server"></asp:Label></div>
                                        <div class="col-sm-1" style="width:7%;text-align:left"><asp:Label ID="lblTotSale" runat="server"></asp:Label></div>
                                        <div class="col-sm-1" style="width:8%;text-align:left"><asp:Label ID="lblTotIssue" runat="server"></asp:Label></div>
                                        <div class="col-sm-1" style="width:12.5%;text-align:left"><asp:Label ID="lblTotTran" runat="server"></asp:Label></div>
                                        <div class="col-sm-1" style="width:8%;text-align:left"><asp:Label ID="lblBalance" runat="server"></asp:Label></div>
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
    <div class="col-lg-1" style="display: none">
        <tr style="display: none">
            <td class="white_bg" align="center">
                <div id="print" style="font-size: 13px; display: block;">
                    <table cellpadding="1" cellspacing="0" width="800" border="1" style="font-family: Arial,Helvetica,sans-serif;">
                        <tr>
                            <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                                border-left-style: none; border-right-style: none">
                                <strong>
                                    <asp:Label ID="lblCompanyname" runat="server" Style="font-size: 18px;"></asp:Label><br />
                                </strong>
                                <asp:Label ID="lblCompAdd1" runat="server"></asp:Label>
                                &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblCompAdd2" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCompCity" runat="server"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblCompState" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblCompCityPin" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCompPhNo" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblFaxNo" Text="FAX No.:" runat="server"></asp:Label>
                                <asp:Label ID="lblCompFaxNo" runat="server"></asp:Label><br />
                                <asp:Label ID="lblTin" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;<asp:Label
                                    ID="lblCompTIN" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbltxtPanNo" runat="server" Text="PAN NO. :"></asp:Label>&nbsp;&nbsp;<asp:Label
                                    ID="lblPanNo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 13px;
                                border-left-style: none; border-right-style: none">
                                <h3>
                                    <div style="text-decoration: underline">
                                        <asp:Label ID="lblPrintHeadng" runat="server" Text="Party Detail Receipt"></asp:Label>
                                        (Date From :
                                        <asp:Label ID="lblDateFrom" runat="server"></asp:Label>
                                        &nbsp;&nbsp; Date To :
                                        <asp:Label ID="lblDateto" runat="server"></asp:Label>)
                                    </div>
                                    <h3>
                                    </h3>
                                    <h3>
                                    </h3>
                                    <h3>
                                    </h3>
                                    <h3>
                                    </h3>
                                    <h3>
                                    </h3>
                                    <h3>
                                    </h3>
                                    <h3>
                                    </h3>
                                    <h3>
                                    </h3>
                                    <h3>
                                    </h3>
                                    <h3>
                                    </h3>
                                </h3>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table border="0" style="font-size:10px;" width="100%">
                                    <tr>
                                        <td align="left" width="7%" class="white_bg" valign="top" style="font-size: 10px;
                                            border-right-style: none">
                                            <b>
                                                <asp:Label ID="lbltxtgrno" Text="Party Name" runat="server"></asp:Label></b>
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td align="left" class="style1" valign="top" style="font-size: 10px; border-right-style: none">
                                            <asp:Label ID="lblPartyName" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="style2" valign="top" colspan="2" style="font-size: 10px;
                                            border-right-style: none">
                                            <div id="divLocation" runat="server">
                                                <b>
                                                    <asp:Label ID="lbltxtgrdate" Text="" runat="server"></asp:Label></b>
                                                :&nbsp;
                                                <asp:Label ID="lblLocation" runat="server"></asp:Label>
                                            </div>
                                        </td>
                                        <td colspan="4" align="left" class="white_bg" valign="top" style="font-size: 10px;
                                            border-right-style: none">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 10px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lbltxttocity" Text="Address" runat="server"></asp:Label>
                                            </b>
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td align="left" class="white_bg" valign="top" colspan="7" style="font-size: 10px;
                                            border-right-style: none">
                                            <asp:Label ID="lblAddresss" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 10px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lbltxtsendewrname0" Text="City" runat="server"></asp:Label>
                                            </b>
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td align="left" class="style1" valign="top" style="font-size: 10px; border-right-style: none">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblCityname" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <b>&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                                            <asp:Label ID="lbltxtsendername" Text="State : " runat="server"></asp:Label></b>
                                                        &nbsp;&nbsp;
                                                        <asp:Label ID="lblStateName" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="left" class="style2" valign="top" colspan="2" style="font-size: 13px;
                                            border-right-style: none">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <b></b>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table border="0" cellspacing="0" style="font-size: 8px ; font-style: normal" width="100%" id="Table1">
                                  
                                <style>
                                .dataTable td, th {
                                    white-space: nowrap;
                                    font-size:13px;
                                }
                                .dataTable td:nth-child(12) {
                                    white-space: unset !important;
                                }
                                tr.no-border-td:not(:first-child) td
                                {
                                    border: 0px solid transparent;
                                    border-bottom: 1px solid black;
                                }   
                                tr.no-border-td:first-child td
                                {
                                    border: 2px solid red;
                                }  
                                .dataTable td:nth-child(13) {
                                    white-space: unset;
                                }
                                </style>
                                    <tr>
                                        <td> <%--custom-table-style--%>
                                            <asp:GridView ID="grdPrintDtl" runat="server" AutoGenerateColumns="false" CssClass="display nowrap dataTable"
                                                Width="100%" GridLines="Both" AllowPaging="false" PageSize="50"  ShowFooter="False">
                                                <RowStyle CssClass="odd" />
                                                <AlternatingRowStyle CssClass="even" />
                                                <PagerStyle CssClass="classPager" />
                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First"
                                                    Position="Bottom" LastPageText="Last" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No" HeaderStyle-HorizontalAlign="center"
                                                        Visible="True">
                                                        <HeaderStyle HorizontalAlign="center" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="center" />
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>.
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <%#Eval("Chln_Date")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="C.No." HeaderStyle-HorizontalAlign="center"
                                                        Visible="True">
                                                        <HeaderStyle HorizontalAlign="center" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="center" />
                                                        <ItemTemplate>
                                                            <%#Eval("Chln_No")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Gr No." HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <%#Eval("GrNo")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name" HeaderStyle-HorizontalAlign="Left"
                                                        Visible="false">
                                                        <HeaderStyle HorizontalAlign="Left" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <%#Eval("ItemName")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="From  City" HeaderStyle-HorizontalAlign="Left"
                                                        Visible="true">
                                                        <HeaderStyle HorizontalAlign="Left" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <%#Eval("FromCity")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="To  City" HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <%#Eval("ToCity")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Lorry" HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblT" runat="server" Text='  <%#Eval("LorryNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Weight" HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <%#Eval("Weight")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left"
                                                        Visible="false">
                                                        <HeaderStyle HorizontalAlign="Left" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <%#Eval("Qty")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate" HeaderStyle-HorizontalAlign="Right">
                                                        <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <%#Eval("Rate")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <%#Eval("Amount")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Advance" HeaderStyle-HorizontalAlign="right">
                                                        <HeaderStyle HorizontalAlign="right" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <ItemTemplate>
                                                            <%#Eval("Advance")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Diesel Amt" HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <%#Eval("Diesel_Amnt")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Comission" HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <%#Eval("Commission")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TDS" HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <%#Eval("TDS")%>
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ReceivedWeight" HeaderStyle-HorizontalAlign="Left">
                                                     <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <%#Eval("ReceivedWeight")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Srtg" HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <%#Eval("Shortage")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Srtg Amnt" HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <%#Eval("ShortageAmt")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loading" HeaderStyle-HorizontalAlign="Left"
                                                        Visible="false">
                                                        <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Received Amount" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <%#Eval("ReceivedAmnt")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bal. Amt." Visible="false"  HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <%#Eval("BalanceAmount")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unloading" Visible="true"  HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <%#Eval("Wages_Amnt")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bal. Amt." HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <%#Eval("RowNet_Total")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                                <EmptyDataTemplate>
                                                    <asp:Label ID="LblNoRecordFound1" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table2">
                                    <tr>
                                        <td class="white_bg" width="15%">
                                        </td>
                                        <td class="white_bg" width="15%">
                                        </td>
                                        <td class="white_bg" width="15%" align="center">
                                        </td>
                                        <td class="white_bg" width="15%" align="left">
                                            <asp:Label ID="lbltotalqty" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                        <td class="white_bg" width="12.5%">
                                            <asp:Label ID="lbltotalWeight" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                        <td class="white_bg" width="12.5%">
                                        </td>
                                        <td class="white_bg" width="12.5%" align="center">
                                            <asp:Label ID="lblTotalAmnt" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                        <td class="white_bg" width="12.5%">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="4">
                                <table width="100%" style="font-size: 12px" border="0" cellspacing="0">
                                    <tr style="line-height: 25px">
                                        <td colspan="9" style="font-size: 13px" align="left" class="white_bg">
                                            <table width="100%">
                                                <tr>
                                                    <td align="left" class="white_bg" style="font-size: 13px" width="50%">
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <b>Customer Signature</b>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td align="right" class="white_bg" style="font-size: 13px" valign="top" width="50%">
                                                        <br />
                                                        <b>
                                                            <asp:Label ID="lblCompname" runat="server"></asp:Label><br />
                                                            <br />
                                                            <br />
                                                            Authorised Signatory&nbsp;</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </div>
    <asp:HiddenField ID="hidrcptheadidno" runat="server" />
    <asp:HiddenField ID="hidmindate" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />
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
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent1 = "<table width='100%' border='0'></table>";
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'left=0,top=0,width=800,height=800,toolbar=1,scrollbars=1,status=1');
            WinPrint.document.write(prtContent1);
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            //WinPrint.close();
            return false;
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
        <script>
            $(document).ready(function () {
                var count = 0;
                $('.custom-table-style tr td:nth-child(2)').each(function () {
                    if ($(this).text().length == "118") {
                        count++;
                        if (count > 1) {
                            $(this).parent("tr").addClass("no-border-td");
                            $(this).prev('td').text('');
                            $('.no-border-td td:nth-child(12)').attr("colspan", "8");
                            $('.no-border-td td:nth-child(12)').nextAll("td").remove();
                        }
                    }
                });
            });
        </script>
    <script src="js/JumpingFrog-Placeholder.js" type="text/javascript"></script>
</asp:Content>
