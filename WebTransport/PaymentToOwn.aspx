<%@ Page Title="Payment To Own" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="PaymentToOwn.aspx.cs" Inherits="WebTransport.PaymentToOwn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <asp:UpdatePanel ID="updpnl" runat="server" ViewStateMode="Enabled">
        <ContentTemplate>--%>
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-10">
            <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">PAYMENT TO DRIVER
                  <span class="view_print"><a href="ManagePaymentToOwn.aspx"><asp:Label ID="lblViewList" runat="server" TabIndex="18" Text="LIST"></asp:Label></a>
                &nbsp;
                <asp:LinkButton ID="lnkbtnPrintClick" runat="server" OnClick="lnkbtnPrintClick_OnClick" ToolTip="Click to print" Visible="false"><i class="fa fa-print icon"></i></asp:LinkButton>
                <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Click to print" OnClientClick="return CallPrint('print');" Visible="false"><i class="fa fa-print icon"></i></asp:LinkButton>
                  </span>
                </header>
                <div class="panel-body">
                  <form class="bs-example form-horizontal">
                    <!-- first  section --> 
                    <div class="clearfix first_section">
                      <section class="panel panel-in-default">  
                        <div class="panel-body">
                        	<div class="clearfix odd_row">
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 29%;">Date Range<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 71%;">
                              <asp:DropDownList ID="ddldateRange" runat="server" AutoPostBack="true" CssClass="form-control"
                                 TabIndex="1"  OnSelectedIndexChanged="ddldateRange_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddldateRange"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Year!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>                             
                              </div>
                            </div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 29%;">Pay. No.<span class="required-field">*</span></label>

                              <div class="col-sm-3" style="width: 40%;">
                                <asp:TextBox ID="txtRcptNo" runat="server" CssClass="form-control" MaxLength="50"  oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                    TabIndex="2"  ReadOnly="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRcptNo"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Rcpt no!"
                                    SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>

                              <div class="col-sm-4" style="width: 31%;">
                              <asp:TextBox ID="txtDate" runat="server" CssClass="input-sm datepicker form-control" MaxLength="50" data-date-format="dd-mm-yyyy"
                                oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"  TabIndex="3"  ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"  CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Date!"
                                SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>                               
                              </div>
                           	</div>
                           	<div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 29%;">Loc.[From]<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 71%;">
                                  <asp:DropDownList ID="ddlFromCity" runat="server" CssClass="form-control"  TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="ddlFromCity_SelectedIndexChanged">
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlFromCity"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select From city!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>--%>
                              </div>
                              
                            </div>
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 29%;">Challan<span class="required-field">*</span></label>
                              <div class="col-sm-8" style="width: 71%;">
                                <asp:DropDownList ID="ddlChallan" runat="server" AutoPostBack="true" 
                                      CssClass="form-control" TabIndex="5" 
                                      onselectedindexchanged="ddlChallan_SelectedIndexChanged">
                                </asp:DropDownList>                               
                                <asp:RequiredFieldValidator ID="rfvddlChallan" runat="server" ControlToValidate="ddlChallan"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Challan Details!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>                                           
                              </div>
                             
                            </div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 29%;">Driver</label>

                              <div class="col-sm-3" style="width: 41%;">
                                  <asp:TextBox ID="txtDriverName" runat="server" CssClass="form-control" TabIndex="6" Enabled="false" Style="text-align: left;"></asp:TextBox>                             
                                                             
                              </div>
                              <div class="col-sm-3" style="width: 30%;">
                              <asp:TextBox ID="txtCurrBal" runat="server" CssClass="form-control" Text="0.00" MaxLength="10" TabIndex="7" AutoPostBack="true"  Enabled="false" Style="text-align: right;" onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                              </div>
                           	</div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 29%;">Amount</label>

                              <div class="col-sm-3" style="width: 71%;">
                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" Text="0.00" MaxLength="10" TabIndex="8" Style="text-align: right;" onKeyPress="return checkfloat(event, this);"></asp:TextBox>                           
                              </div>
                           	</div>
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-4">
                            <label class="col-sm-3 control-label" style="width: 29%;">Pay. Type<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 71%;">
                                 <asp:DropDownList ID="ddlRcptTyp" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="9" OnSelectedIndexChanged="ddlRcptTyp_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlRcptTyp" runat="server" ControlToValidate="ddlRcptTyp"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Rcpt Type!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>                                          
                              </div>
                             
                            </div>
                            <div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 29%;">Inst.Details</label>

                              <div class="col-sm-3" style="width: 40%;">
                                <asp:TextBox ID="txtInstNo" runat="server" placeholder="Enter Inst.No." CssClass="form-control"  MaxLength="6" Style="text-align: right;" TabIndex="10"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvinstno" runat="server" ControlToValidate="txtInstNo"
                                Display="Dynamic" SetFocusOnError="true"  ValidationGroup="save"  ErrorMessage="Enter Inst. No!" CssClass="classValidation"></asp:RequiredFieldValidator>                                                           
                          
                              </div>

                              <div class="col-sm-4" style="width: 31%;">
                               <asp:TextBox ID="txtInstDate" runat="server" placeholder="Enter Date" CssClass="input-sm datepicker form-control" TabIndex="11" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvinstDate" runat="server" ControlToValidate="txtInstDate"
                                    Display="Dynamic" SetFocusOnError="true" ValidationGroup="save"  ErrorMessage="Enter Inst. Date!" CssClass="classValidation"></asp:RequiredFieldValidator>
                             
                              </div>
                           	</div>
                           	<div class="col-sm-4">
                           		 <label class="col-sm-3 control-label" style="width: 29%;">Cust.Bank</label>
                              <div class="col-sm-8" style="width: 71%;">

                               <asp:DropDownList ID="ddlCustmerBank" runat="server" CssClass="form-control" TabIndex="12" >
                                </asp:DropDownList>
                              <%--  <asp:RequiredFieldValidator ID="rfvCusBank" runat="server" ControlToValidate="ddlCustmerBank"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Cust Bank!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>--%>
    
                              </div>
                           	</div>                           	
                          </div>

                          <div class="clearfix even_row">
                          	<label class="col-sm-3 control-label" style="width: 9.5%;">Remark</label>
                            <div class="col-sm-9" style="width: 90.5%;">
                                <asp:TextBox ID="TxtRemark" runat="server" placeholder="Enter Remark" CssClass="form-control" MaxLength="200"  oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                 TabIndex="13" TextMode="MultiLine" Style="resize: none"></asp:TextBox>                             
                            </div>
                          </div>

                        </div>
                      </section>                        
                    </div>
                    
                    <!-- second  section -->
                    


                     <!-- fourth row -->
                    <div class="clearfix odd_row">
                      <div class="col-lg-4"></div>
                      <div class="col-lg-4">  
                      <div class="col-sm-4">
                      <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-info" TabIndex="17" OnClick="lnkbtnNew_OnClick" ><i class="fa fa-file-o"></i>New</asp:LinkButton> 
                      </div>                                      
                        <div class="col-sm-4">
                       
                        <asp:HiddenField ID="hidid" runat="server" Value="" />
                        <asp:HiddenField ID="Hidrowid" runat="server" Value="" />
                        <asp:HiddenField ID="hidmindate" runat="server" />
                        <asp:HiddenField ID="hidmaxdate" runat="server" />
                        <asp:HiddenField ID="hidpostingmsg" runat="server" />
                        <asp:HiddenField ID="hidPrintType" runat="server" />
                        <asp:HiddenField ID="hidDriverIdno" runat="server" />
                        <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" ValidationGroup="save" TabIndex="15" CssClass="btn full_width_btn btn-s-md btn-success" OnClick="lnkbtnSave_OnClick" ><i class="fa fa-save"></i>Save</asp:LinkButton>                        
                        </div>
                        <div class="col-sm-4">
                         <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-danger" TabIndex="16" OnClick="lnkbtnCancel_OnClick" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>                        
                        </div>
                      </div>
                      <div class="col-lg-4"></div>
                    </div>

                    <!-- popup form GR detail -->
			         
                     <div class="clearfix third_right">
	                        <section class="panel panel-in-default btns_without_border">                            
	                          <div class="panel-body" id="DivGridShow" runat="server">    
                                <div class="clearfix">
		                          <section class="panel panel-default full_form_container material_search_pop_form">
		                            <div class="panel-body" style="overflow:scroll;">  
                                      <table width="100%">
                                       <tr>
                                       <td>
	                                     <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"
                                    Width="100%" GridLines="None" PageSize="50" BorderWidth="0">
                                   <RowStyle CssClass="odd" />
                                    <AlternatingRowStyle CssClass="even" />                                       
                                    <PagerStyle CssClass="" HorizontalAlign="Right" Height="60" />
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5"  FirstPageText="First" Position="Bottom" LastPageText="Last"/>   
                                    <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50" HeaderText="S.No.">
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" Width="50" />
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>.
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100" HeaderText="Payment No">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("Rcpt_No")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100" HeaderText="Chln No">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="center" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("Chln_No")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100" HeaderText="Date">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Convert.ToDateTime(Eval("Rcpt_date")).ToString("dd-MM-yyyy")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100" HeaderText="From City">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("FromCity")%>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="true" ForeColor="Black" HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltotal" runat="server" Text="Gross Total"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Driver Name">
                                                <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                <ItemStyle HorizontalAlign="Left" Width="50" />
                                                <ItemTemplate>
                                                    <%#Eval("Driver_Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="100" HeaderText="Net Amount">
                                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" Width="50" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotRecvd" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Amnt")))%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="true" ForeColor="Black" HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblFTotRecvd" runat="server"></asp:Label>
                                                </FooterTemplate>
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
    
    </form> </div> </section>
        </div>
        <div class="col-lg-1">
        </div>

        <div id="print" style="font-size: 13px;display:none;">
        <table cellpadding="1" cellspacing="0" width="1100px" border="1" style="font-family: Arial,Helvetica,sans-serif;">
            <tr>
                <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                    border-left-style: none; border-right-style: none">
                    <strong>
                        <asp:Label ID="lblCompanyname" runat="server" Style="font-size: 14px;"></asp:Label><br />
                    </strong>
                    <asp:Label ID="lblCompAdd1" runat="server"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblCompAdd2" runat="server"></asp:Label><br />
                    <asp:Label ID="lblCompCity" runat="server"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblCompState" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblCompCityPin" runat="server"></asp:Label><br /><br />
                    <%--<asp:Label ID="lblCompPhNo" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblFaxNo" Text="FAX No.:" runat="server"></asp:Label>
                    <asp:Label ID="lblCompFaxNo" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblTxtPanNo" Text="PAN No.:" runat="server"></asp:Label>
                    <asp:Label ID="lblPanNo" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="lblTin" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;&nbsp;<asp:Label
                        ID="lblCompTIN" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblcodeno" Text="Code No. :" Visible="false" runat="server" Style="font-size: 14px;"></asp:Label>&nbsp;&nbsp;<asp:Label
                        ID="lblvaluecodeno" runat="server" Visible="false" Style="font-size: 14px;"></asp:Label>--%>
                        <asp:Label ID="Label4" runat="server" Text="Party Bill Freight Payment Details"></asp:Label>
                </td>
            </tr>
            <tr>
                <%--<td colspan="5">
                    <table width="100%">
                        <tr>
                            <td align="left" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                border-right-style: none">
                                <strong style="text-decoration: underline">
                                    </strong>
                                <br />
                                <b>
                                  </b>
                                <br />
                                <b>
                                   </b>
                                <br />
                                <b>
                                   
                                </b>
                            </td>
                            <td align="center" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                border-right-style: none">
                                <strong style="text-decoration: underline">
                                    </strong>
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                border-right-style: none">
                                
                            </td>
                            <td align="center" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none;
                                border-right-style: none">
                                <table>
                                    <tr>
                                        <td align="left">
                                            <b>
                                               
                                            <br />
                                            <b>
                                                
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>--%>
            </tr>
            <tr>
                <td colspan="4">
                    <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table1">
                        <asp:Repeater ID="Repeater1" runat="server" 
                            onitemdatabound="Repeater1_ItemDataBound">

                            <HeaderTemplate>
                                <tr>
                                    <td class="white_bg" style="font-size: 12px" width="3%" style="padding:10px;">
                                        <strong>S.No.</strong>
                                    </td>
                                    <td style="font-size: 12px" width="6%" align="center">
                                        <strong>GR Date</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>GR No.</strong>
                                    </td>
                                    <td style="font-size: 12px" width="5%">
                                        <strong>Truck No</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>Destination</strong>
                                    </td>
                                    <td style="font-size: 12px" align="left" width="6%">
                                        <strong>Actual-Wt</strong>
                                    </td>

                                    <td style="font-size: 12px" align="left" width="8%">
                                        <strong>Frt Rate</strong>
                                    </td>

                                    <td style="font-size: 12px" width="8%">
                                        <strong>Book Freight</strong>
                                    </td>
                                     <td style="font-size: 12px" width="8%" align="center">
                                        <strong>TDS</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>Shortage</strong>
                                    </td>
                                    <td style="font-size: 12px" width="7%">
                                        <strong>Gross Freight</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>Date</strong>
                                    </td>
                                    <td style="font-size: 12px" width="6%">
                                        <strong>Adv Amount</strong>
                                    </td>
                                    <td style="font-size: 12px" width="6%">
                                        <strong>Balance Payment</strong>
                                    </td>
                                    <td style="font-size: 12px" width="5%">
                                        <strong>Less Site-Exp</strong>
                                    </td>
                                    <td style="font-size: 12px" width="5%">
                                        <strong>Add Unloading</strong>
                                    </td>
                                    <td style="font-size: 12px" width="7%">
                                        <strong>Payable Amount</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="white_bg" width="3%">
                                        <%#Container.ItemIndex+1 %>.
                                    </td>
                                    <td class="white_bg" width="5%" style="padding:10px;">
                                        <%#Convert.ToDateTime(Eval("GRDate")).ToString("dd-MM-yyyy")%>
                                    </td>
                                    <td class="white_bg" width="8%" align="center">
                                        <%#Eval("GRNo")%>
                                    </td>
                                    <td class="white_bg" width="8%">
                                        <%#Eval("LorryNo")%>
                                    </td>
                                    <td class="white_bg" width="8%">
                                        <%#Eval("CityName")%>
                                    </td>
                                    <td class="white_bg" width="6%" align="center">
                                        <%#Eval("TotWeight")%>&nbsp;
                                    </td>

                                    <td class="white_bg" width="8%" align="center">
                                        <%#Eval("FrtRate")%>&nbsp;
                                    </td>

                                    <td class="white_bg" width="8%" align="left">
                                        <%#Eval("GrossAmnt")%>&nbsp;
                                    </td>
                                      <td class="white_bg" width="8%" align="center">
                                        <%#Eval("TDS")%>&nbsp;
                                    </td>
                                    <td class="white_bg" width="8%" align="center">
                                        <%#(Eval("ShortageAmount"))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="white_bg" width="7%" align="left">
                                        <%#Eval("GrossFreight")%>
                                    </td>
                                    <td class="white_bg" width="8%" align="left">
                                       <%#Convert.ToDateTime(Eval("ChlnDate")).ToString("dd-MM-yyyy")%>
                                    </td>
                                    <td class="white_bg" width="6%" align="left">
                                        <%#Eval("AdvanceAmount")%>
                                    </td>
                                    <td class="white_bg" width="15%" align="right">
                                       <%#Eval("BalancePayment")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="white_bg" width="15%" align="right">
                                        <%#Eval("Site_Exp")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="white_bg" width="15%" align="right">
                                       <%#Eval("Unloading")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="white_bg" width="15%" align="right">
                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("PayablePayment")))%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                           <%-- <SeparatorTemplate>
                             <tr>
                                    <td >
                                        <hr />
                                    </td>
                                    <td>
                                       <hr />
                                    </td>
                                    <td >
                                        <hr />
                                    </td>
                                    <td >
                                       <hr />
                                    </td>
                                    <td>
                                       <hr />
                                    </td>
                                    <td>
                                       <hr />
                                    </td>

                                    <td>
                                       <hr />
                                    </td>
                                      <td>
                                        <hr />
                                    </td>
                                    <td>
                                        <hr />
                                    </td>
                                    <td>
                                        <hr />
                                    </td>
                                    <td>
                                       <hr />
                                    </td>
                                    <td>
                                       <hr />
                                    </td>
                                    <td>
                                      <hr />
                                    </td>
                                    <td>
                                        <hr />
                                    </td>
                                    <td>
                                     <hr />
                                    </td>
                                    <td>
                                        <hr />
                                    </td>
                                </tr>
                            </SeparatorTemplate>--%>
                            <FooterTemplate>
                            <tr>
                                    <td class="white_bg" width="3%" style="padding:10px;">
                                     
                                    </td>
                                    <td class="white_bg" width="5%">
                                       
                                    </td>
                                    <td class="white_bg" width="8%">
                                       
                                    </td>
                                    <td class="white_bg" width="8%">
                                      <asp:Label ID="lblTot" Font-Bold="true" runat="server" Text="TOTAL"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="8%">
                                       
                                    </td>
                                    <td class="white_bg" width="6%" align="center">
                                       <asp:Label ID="lblFTotWt" Font-Bold="true" runat="server"></asp:Label> &nbsp;
                                    </td>
                                     <td class="white_bg" width="8%">
                                       
                                    </td>
                                    <td class="white_bg" width="8%" align="left">
                                       <asp:Label ID="lblFAmnt" Font-Bold="true" runat="server"></asp:Label>&nbsp;
                                    </td>
                                      <td class="white_bg" width="8%" align="center">
                                        <asp:Label ID="lblFTDS" Font-Bold="true" runat="server"></asp:Label>&nbsp;
                                    </td>
                                    <td class="white_bg" width="8%" align="center">
                                        <asp:Label ID="lblShor" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="white_bg" width="7%" align="left">
                                        <asp:Label ID="lblFGross" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="8%" align="left">
                                      
                                    </td>
                                    <td class="white_bg" width="6%" align="left">
                                        <asp:Label ID="lblFAdv" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="15%" align="right">
                                      <asp:Label ID="lblBal" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="white_bg" width="15%" align="right">
                                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="white_bg" width="15%" align="right">
                                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="white_bg" width="15%" align="right">
                                        <asp:Label ID="lblPayable" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
            <%--<tr>
                <td colspan="4" align="right">
                    <table border="0" cellspacing="0" style="font-size: 12px" width="50%" id="Table2">
                        <tr>
                           <td class="white_bg" width="5%" align="center">
                                <asp:Label ID="Label1" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="5%" align="center">
                                <asp:Label ID="Label2" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="5%" align="center">
                                <asp:Label ID="Label3" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="5%" align="center">
                                <asp:Label ID="Label5" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="5%" align="center">
                                <asp:Label ID="lbltotalWeight" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td align="right" class="white_bg" width="6%" align="right">
                                <asp:Label ID="lblAmount" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="5%" align="right">
                                <asp:Label ID="lblUnloading" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="5%" align="right">
                                <asp:Label ID="lblNetTotAmnt" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="5%" align="right">
                                
                            </td>
                            <td class="white_bg" width="5%" align="right">
                               
                            </td>
                            <td class="white_bg" width="5%" align="right">
                                <asp:Label ID="lblTotSBTax" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>--%>
            <tr>
                <td align="right">
                    <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table3">
                        <tr>
                            <td class="white_bg" width="15%" align="left">
                            Truck Owner/ Party Ledger A/c :
                            </td>
                            <td class="white_bg" width="5%">
                            </td>
                            <td class="white_bg" width="10%">
                            </td>
                            <td class="white_bg" width="9%" align="left">
                            </td>
                            <td class="white_bg" width="10%" align="right">
                                
                            </td>
                            <td align="right" class="white_bg" width="5%">
                               
                            </td>
                            <td class="white_bg" width="4%" align="center">
                                
                            </td>
                            <td class="white_bg" width="9%" align="center">
                                
                            </td>
                        </tr>
                          <tr>
                            <td class="white_bg" width="15%" align="left">
                            </td>
                            <td class="white_bg" width="5%">
                            </td>
                            <td class="white_bg" width="10%">
                            </td>
                            <td class="white_bg" width="9%" align="left">
                            </td>
                            <td class="white_bg" width="10%" align="right">
                                
                            </td>
                            <td align="right" class="white_bg" width="5%">
                               
                            </td>
                            <td class="white_bg" width="4%" align="center">
                                
                            </td>
                            <td class="white_bg" width="9%" align="center">
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="white_bg" width="15%" align="left">
                            Cash/Cheque Amount : Rs.
                            </td>
                            <td class="white_bg" width="5%" align="left">
                            Cheque No.:
                            </td>
                            <td class="white_bg" width="10%" align="left">
                            Dt.
                            </td>
                            <td class="white_bg" width="9%" align="left">
                            Prepared By
                            </td>
                            <td class="white_bg" width="10%" align="left">
                               
                            </td>
                            <td align="left" class="white_bg" width="5%">
                                Checked By 
                            </td>
                            <td class="white_bg" width="4%" align="center">
                                
                            </td>
                            <td class="white_bg" width="9%" align="center">
                                Receiver's Siq
                            </td>
                        </tr>
                        <tr>
                            <td class="white_bg" width="15%">
                            </td>
                            <td class="white_bg" width="15%">
                            </td>
                            <td class="white_bg" width="5%">
                            </td>
                            <td class="white_bg" width="4%" align="left">
                            </td>
                            <td class="white_bg" width="10%" align="right">
                                
                            </td>
                            <td align="right" class="white_bg" width="5%">
                                
                            </td>
                            <td class="white_bg" width="5%" align="center">
                                
                            </td>
                            <td class="white_bg" width="9%" align="center">
                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table width="100%" align="right">
                        <tr>
                            <td colspan="3" align="left" width="30%">
                                <table>
                                    <tr>
                                        <td width="80%">
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="80%">
                                           
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="16%" align="center" valign="top">
                            </td>
                           <%-- <td colspan="2" width="20%" align="right" valign="top">
                                <table width="100%">
                                    <tr style="border-bottom-style: solid; border-top-width: thin; border-bottom-width: thin;
                                        border-right-width: thin">
                                        <td align="right">
                                            &nbsp;&nbsp;
                                            <asp:Label ID="lblnet" runat="server" Text="Net Amnt" Font-Size="13px" valign="right"></asp:Label>
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td align="right" width="8%">
                                            <asp:Label ID="lblNetAmnt" runat="server" Font-Size="13px" valign="lef"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="white_bg" style="font-size: small" valign="top" colspan="3">
                                            <b>
                                                <asp:Label ID="lblcompname" runat="server"></asp:Label><br />
                                                <br />
                                                <br />
                                                Authorised Signatory&nbsp;</b>
                                        </td>
                                    </tr>
                                </table>
                            </td>--%>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </div>
    <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>
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
            $("#<%=txtDate.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });

            $("#<%=txtInstDate.ClientID %>").datepicker({
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
            $('#dvGrdetails').modal('show');
        }

        function CloseModal() {
            $('#dvGrdetails').Hide();
        }

        function HideBillAgainst() {
            $("#dvGrdetails").fadeOut(300);
        }

        function ShowClient() {
            $("#dvGrdetails").fadeIn(300);
        }
        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox

            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
    spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
          elm[i].id != theBox.id) {
                    //elm[i].click();

                    if (elm[i].checked != xState)
                        elm[i].click();
                    //elm[i].checked=xState;

                }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript">
        $(".datepicker").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
            minDate: '<%=hidmindate.Value%>',
            maxDate: '<%=hidmaxdate.Value%>'
        });
        function CallPrint(strid) {
            if (strid == 'print') {
             
                    var prtContent1 = "<table width='100%' border='0'></table>";
                    var prtContent = document.getElementById(strid);
                    var WinPrint = window.open('', '', 'letf=0,top=0,width=800,height=800,toolbar=1,scrollbars=1,status=1');
                    WinPrint.document.write(prtContent1);
                    WinPrint.document.write(prtContent.innerHTML);
                    WinPrint.document.close();
                    WinPrint.focus();
                    WinPrint.print();
                    WinPrint.close();
                    return false;
                }
            }
    </script>
</asp:Content>