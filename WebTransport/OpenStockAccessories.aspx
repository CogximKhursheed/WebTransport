<%@ Page Title="Opening Stock [Accessories]" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="OpenStockAccessories.aspx.cs" Inherits="WebTransport.OpenStockAccessories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>--%>
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-10">
            <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">Opening Stock [Accessories]
                  <span class="view_print"><a href="ManageOpenStockAccessories.aspx" tabindex="12"><asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a>
                   
                    </span>
                </header>
                <div class="panel-body">
                  <form class="bs-example form-horizontal">
                  <div class="clearfix second_section">
                      <section class="panel panel-in-default">  
                        <div class="panel-body">                          
                          <div class="clearfix odd_row">
                            <div class="col-sm-3">
                              <label class="control-label">Date Range<span class="required-field">*</span></label>
                              <div>
                              <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control" TabIndex="1" >
                                </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDateRange" TabIndex="1"
                                Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit" ErrorMessage="Choose Date Range!" CssClass="classValidation">
                            </asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-2">
                              <label class="control-label">Loc.[From]<span class="required-field">*</span></label>
                              <div>
                                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control"  TabIndex="2" AutoPostBack="True" 
                                     OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" >
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlLocation"
                                        Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit" ErrorMessage="Select Location!" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            <div class="col-sm-3">	                                
                              <label class="control-label">Accessory Name<span class="required-field">*</span></label>
                              <div>
                                   <asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control"  TabIndex="3" AutoPostBack="True"   OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" >
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPartno" runat="server" ControlToValidate="ddlItemName"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit" ErrorMessage="Select Accessory!" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div> 
                            </div>
                            <div class="col-sm-2">
                               <label class="control-label">Qty<span class="required-field">*</span></label>
                              <div>
                                    <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" onKeyPress="return allowOnlyNumber(event);" MaxLength="20" TabIndex="4" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtQty"
                                Display="Dynamic" SetFocusOnError="true"  ValidationGroup="Submit"  ErrorMessage="Enter Qty!" CssClass="classValidation">
                            </asp:RequiredFieldValidator>
                              </div> 
                            </div>
                            <div class="col-sm-2">
                             <label class="control-label">Rate<span class="required-field">*</span></label>
                              <div>
                                <asp:TextBox ID="txtOpenRate" runat="server" CssClass="form-control" onKeyPress="return allowOnlyFloatNumber(event);" MaxLength="20" Style="text-align:right;" TabIndex="5" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSerialNo" runat="server" ControlToValidate="txtOpenRate"
                            Display="Dynamic" SetFocusOnError="true"  ValidationGroup="Submit" ErrorMessage="Enter Rate!" CssClass="classValidation">
                        </asp:RequiredFieldValidator>
                       </div> 
                            </div>
                           </div> 
                        <div class="clearfix odd_row">
                        <div class="col-sm-4">
                    </div>
                    <div class="col-sm-4">
                        <div class="col-sm-6">
                    <asp:LinkButton ID="lnkbtnSubmit" runat="server" CssClass="btn full_width_btn btn-sm btn-primary subnew" TabIndex="6" OnClick="lnkbtnSubmit_OnClick" ValidationGroup="Submit" CausesValidation="true" >Submit</asp:LinkButton>
                    </div>
                    <div class="col-sm-6">
                    <asp:LinkButton ID="lnkbtnNewClick" runat="server" CssClass="btn full_width_btn btn-sm btn-primary subnew" TabIndex="7" OnClick="lnkbtnNewClick_OnClick" CausesValidation="false" >New</asp:LinkButton>  
                    </div>
                    </div>
                    <div class="col-sm-4">
                     <div class="col-sm-6">
                       <label class="control-label">&nbsp;</label>
                     <asp:LinkButton ID="lnkExcelPop"  runat="server" tooltip="Please Browse file Format (Item,Qty,Rate)" TabIndex="8" CssClass="btn full_width_btn btn-sm btn-primary"  data-toggle="modal" data-target="#acc_posting"><i class="fa fa-upload"></i>Import Excel</asp:LinkButton>
                    </div>
                    <div class="col-sm-6">&nbsp; </div>
                    </div>
                          </div> 
                          <div id="acc_posting" class="modal fade" role="dialog">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h4 class="popform_header">
                                            Upload Excel</h4>
                                        </div>
                                        <div class="modal-body">
                                            <section class="panel panel-default full_form_container material_search_pop_form">
								    <div class="panel-body">
									    <!-- First Row start -->
								    <div class="clearfix odd_row">	       
                            <div class="col-sm-3">                         
	                            <label class="control-label"  >From Excel</label>
                                </div>
							<div class="col-sm-6">
                                <asp:FileUpload ID="FileUpload" runat="server"  tooltip="Please Browse file Format (BillNo,Bill Date,Party,Lorry,Item,Qty,Rate,Remark)" Width="200px" TabIndex="14" />
							</div> 
							<div class="col-sm-2">
                            <asp:LinkButton ID="lnkbtnUpload" runat="server" TabIndex="15" CssClass="btn full_width_btn btn-sm btn-primary" OnClick="lnkbtnUpload_OnClick" ><i class="fa fa-upload"></i>Upload</asp:LinkButton>
                           </div>
	                        </div>  
							    </div>

						    </section>
                        </div>
                        <div class="modal-footer">
                            <div class="popup_footer_btn">
                       
                                <button type="submit" class="btn btn-dark" data-dismiss="modal">
                                    <i class="fa fa-times"></i>Close</button>
                            </div>
                </div>
            </div>
        </div>
                    </div>                           
                        </div>
                      </section>                        
                    </div>

                      <div class="clearfix third_right">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body" style="overflow-x:auto;">     
                           <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"
                                Width="100%" GridLines="Both" AllowPaging="false"  BorderWidth="0" ShowFooter="true" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand"
                                OnRowDataBound="grdMain_RowDataBound" OnRowCreated="grdMain_RowCreated">
                                <RowStyle CssClass="odd" />
                                <AlternatingRowStyle CssClass="even" />   
                                <Columns>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="Center">
                                       <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                        <ItemStyle HorizontalAlign="Center" Width="40" />
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("id") %>' CommandName="cmdedit"  ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                
                                         <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("id") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>  
                                    
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="50" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Item Name">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" runat="server" Text='<%#Eval("ItemName")%>'></asp:Label>
                                                                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Location">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                            <%#Eval("LocName")%>
                                        </ItemTemplate>
                                            <FooterTemplate>
                                            <asp:Label ID="lblR" Text="TOTAL RECORDS" runat="server" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100" HeaderText="Qty">
                                        <ItemStyle HorizontalAlign="Left" Width="100" />
                                        <ItemTemplate>
                                            <%#Eval("Qty")%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <FooterTemplate>
                                                                                      
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100" HeaderText="Rate">
                                        <ItemStyle HorizontalAlign="Left" Width="100" />
                                        <ItemTemplate>
                                            <%#string.Format("{0:0,0.00}", Eval("Rate"))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <FooterTemplate>
                                            &nbsp;&nbsp;<asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
                          
                        </div>
                      </section>
                    </div> 
                    <div class="clearfix fourth_right">
                      <section class="panel panel-in-default btns_without_border">                            
                        <div class="panel-body">     
                          <div class="clearfix odd_row">
                            <div class="col-lg-3"></div>
                            <div class="col-lg-6"> 
                            
                            <div class="col-lg-12">
                            <asp:Label ID="lblmessage" runat="server" Font-Bold="true" Visible="false" CssClass="classValidation"></asp:Label>
                            </div>
                              
                             <div class="col-lg-4">  
                              <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false" visible="false" CssClass="btn full_width_btn btn-s-md btn-info" TabIndex="11" OnClick="lnkbtnNew_OnClick" ><i class="fa fa-file-o"></i>New</asp:LinkButton>
                             </div>                                                    
                              <div class="col-sm-4">
                               <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" ValidationGroup="Save" TabIndex="9" CssClass="btn full_width_btn btn-s-md btn-success" OnClick="lnkbtnSave_OnClick" ><i class="fa fa-save"></i>Save</asp:LinkButton>
                              
                              </div>
                              <div class="col-sm-4">
                               <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" TabIndex="10" CssClass="btn full_width_btn btn-s-md btn-danger" OnClick="lnkbtnCancel_OnClick" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
                              </div>
                            </div>
                            <div class="col-lg-3"></div>
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
    <asp:HiddenField ID="hidstckidno" runat="server" />
    <%--    </ContentTemplate>
    </asp:UpdatePanel>--%>
    <script language="javascript" type="text/javascript">
        function validate(evt) {
            var theEvent = evt || window.event;
            var key = theEvent.keyCode || theEvent.which;
            key = String.fromCharCode(key);
            var regex = /[0-9]|\./;
            if (!regex.test(key)) {
                theEvent.returnValue = false;
                if (theEvent.preventDefault) theEvent.preventDefault();
            }
        } 
    </script>
</asp:Content>
