<%@ Page Title="Opening Stock [Tyre]" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="OpenStock.aspx.cs" Inherits="WebTransport.OpenStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>--%>
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-10">
            <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">Opening Stock [Tyre]
                  <span class="view_print"><a href="ManageOpenStock.aspx" tabindex="15"><asp:Label ID="lblViewList" runat="server" TabIndex="25" Text="LIST"></asp:Label></a>
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
                               <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control" TabIndex="1">
                            </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDateRange" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit" ErrorMessage="Select Date Range!" CssClass="classValidation">
                                 </asp:RequiredFieldValidator>
                              </div>
                            </div>
                           
                           <div class="col-sm-3">
                              <label class="control-label">Loc.[From]<span class="required-field">*</span></label>
                              <div>
                               <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" 
                                      TabIndex="2" AutoPostBack="True"  OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged"  >
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ControlToValidate="ddlLocation" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit" ErrorMessage="Choose Location!" CssClass="classValidation">
                                 </asp:RequiredFieldValidator>
                              </div>
                            </div>
                                                       
                           <div class="col-sm-3">
                              <label class="control-label">Tyre Name<span class="required-field">*</span></label>
                              <div>
                               <asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control"   TabIndex="3" AutoPostBack="True"   OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" >
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPartno" runat="server" ControlToValidate="ddlItemName"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"  ErrorMessage="Select Item Name!" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                           <div class="col-sm-3">
                             	  <label class="control-label">Tyre Size</label>
                               <div>
                               <asp:DropDownList ID="ddltyresize" runat="server" CssClass="form-control"   TabIndex="4">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddltyresize"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"  ErrorMessage="Select Tyre Size!" CssClass="classValidation"></asp:RequiredFieldValidator>
                               </div>
                            </div>   
                          </div>   
                           <div class="clearfix even_row">             
                              <div class="col-sm-2">	                                
                              <label class="control-label">Serial No<span class="required-field">*</span></label>
                              <div>
                                <asp:TextBox ID="txtSerialNo" runat="server" placeholder="Enter Serial No." CssClass="form-control" MaxLength="18"  onblur="todigit();" oncopy="return false" oncut="return false"   onDrop="blur();return false;" onKeyPress="return checkfloat(event, this);"  onpaste="return false"  TabIndex="5" Text=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSerial" runat="server" ErrorMessage="Enter Serial No!" ControlToValidate="txtSerialNo" Display="Dynamic" 
                                 SetFocusOnError="true" ValidationGroup="Submit" CssClass="classValidation" ></asp:RequiredFieldValidator>
                              </div> 
                             
                            </div>
                             <div class="col-sm-3">
                             	  <label class="control-label">Company Name</label>
                               <div>
                               <asp:TextBox ID="txtCompName" placeholder="Enter Company Name" runat="server" CssClass="form-control" Style="text-align:left;" TabIndex="5" Text=""></asp:TextBox>
                              
                               </div>
                             </div>
                            <div class="col-sm-3">	                                
                              <label class="control-label">Purchase From </label>
                              <div>
                               <asp:TextBox ID="txtPurchaseFrom" placeholder="Purchase From" runat="server" CssClass="form-control"  Style="text-align: left;" TabIndex="6"></asp:TextBox>
                              </div> 
                            </div>
                            <div class="col-sm-2">	                                
                              <label class="control-label">Type</label>
                              <div>
                               <asp:DropDownList ID="ddltype" runat="server" CssClass="form-control" TabIndex="7">
                                    <asp:ListItem Text="New" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Old" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Retrieted" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                              </div> 
                            </div>
                            
                            <div class="col-sm-2">
                                	                                
                              <label class="control-label">Opening Rate<span class="required-field">*</span></label>
                              <div>
                                  <asp:TextBox ID="txtOpenRate" runat="server" CssClass="form-control" MaxLength="8"  onblur="todigit();" oncopy="return false" oncut="return false"   onDrop="blur();return false;" onKeyPress="return allowOnlyFloatNumber(event);"   onpaste="return false" Style="text-align: right;" TabIndex="8" Text="0.00" ></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="rfvRate" runat="server" ControlToValidate="txtOpenRate" 
                                    Display="Dynamic" SetFocusOnError="true" ValidationGroup="Submit"  ErrorMessage="Enter Rate!" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div> 
                           
                              </div>
                              <div class="col-sm-12">
                             <div class="col-sm-4">
                              </div>
                              <div class="col-sm-4">
                              <div class="col-sm-6">
                                <asp:LinkButton ID="lnkbtnSubmit" runat="server" CssClass="btn full_width_btn btn-sm btn-primary subnew" TabIndex="9" OnClick="lnkbtnSubmit_OnClick" ValidationGroup="Submit" CausesValidation="true" >Submit</asp:LinkButton>                            
                              </div>
                              <div class="col-sm-6">
                              <asp:LinkButton ID="lnkbtnNewClick" runat="server" CssClass="btn full_width_btn btn-sm btn-primary subnew" TabIndex="10" OnClick="lnkbtnNewClick_OnClick" CausesValidation="false" >New</asp:LinkButton>                             
                              </div>
                              </div>
                              <div class="col-sm-2">
                                 <label class="control-label">&nbsp;</label>
                                <asp:LinkButton ID="lnkExcelPop"  runat="server" tooltip="Please Browse file Format (TyreName,SerialNo,CompanyName, PurchaseFrom, OpeningRate,Type)"  CssClass="btn full_width_btn btn-sm btn-primary"  TabIndex="11" data-toggle="modal" data-target="#acc_posting"><i class="fa fa-upload"></i>Import Excel</asp:LinkButton>
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
	                                 <label class="col-sm-3 control-label" style="width: 31%;">From Excel</label>
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
                           </div>                     
                        </div>
                      </section>                        
                    </div>

                      <div class="clearfix third_right">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body" style="overflow-x:auto;">     
                          
                            <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" Width="100%" GridLines="None" CssClass="display nowrap dataTable"
                            EnableViewState="true" AllowPaging="true"   BorderWidth="0" ShowFooter="true" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand"
                            OnRowDataBound="grdMain_RowDataBound" PageSize="50" OnRowCreated="grdMain_RowCreated">
                            <RowStyle CssClass="odd" />
                            <AlternatingRowStyle CssClass="even" />   
                                <Columns>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="40" HeaderStyle-CssClass="gridHeaderAlignCenter">
                                       <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                        <ItemStyle HorizontalAlign="Center" Width="40" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("id") %>' CommandName="cmdedit"  ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("id") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Serial No" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="left">
                                        <ItemStyle Width="100" HorizontalAlign="Left" />
                                        <ItemTemplate>                        
                                            <%#Eval("SerialNo")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Comp Name">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" runat="server" Text='<%#Eval("CompName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100" HeaderText="Tyre Name">
                                        <ItemStyle HorizontalAlign="Left" Width="100" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTyreName" runat="server" Text='<%#Eval("TyreName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100" HeaderText="Tyre Size">
                                        <ItemStyle HorizontalAlign="Left" Width="100" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTyreSize" runat="server" Text='<%#Eval("TyreSize")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100" HeaderText="Type">
                                        <ItemStyle HorizontalAlign="Left" Width="100" />
                                        <ItemTemplate>
                                            <%#Eval("Type")%>
                                        </ItemTemplate>
                                            <FooterTemplate>
                                            <asp:Label ID="lblR" Text="TOTAL RECORDS" runat="server" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100" HeaderText="Pur From">
                                        <ItemStyle HorizontalAlign="Left" Width="100" />
                                        <ItemTemplate>
                                            <%#Eval("PurFrom")%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <FooterTemplate>
                                            &nbsp;&nbsp;<asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="150" HeaderText="Opening Rate">
                                        <ItemStyle HorizontalAlign="Right" Width="150" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblOpenRate" runat="server" Text='  <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("OpenRate")))%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lbltotAmount" Text="" runat="server" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                <EmptyDataTemplate>
                    <asp:Label ID="LblNoRecordFound" runat="server" CssClass="white_bg" 
                        Text="Record(s) not found."></asp:Label>
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
                              <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-info" TabIndex="14" Visible="false" OnClick="lnkbtnNew_OnClick" ><i class="fa fa-file-o"></i>New</asp:LinkButton>
                             </div>                                                    
                              <div class="col-sm-4">
                               <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" ValidationGroup="Save" TabIndex="12" CssClass="btn full_width_btn btn-s-md btn-success" OnClick="lnkbtnSave_OnClick" ><i class="fa fa-save"></i>Save</asp:LinkButton>
                                <asp:HiddenField ID="hidmindate" runat="server" />
                                <asp:HiddenField ID="hidmaxdate" runat="server" />
                              </div>
                              <div class="col-sm-4">
                               <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" TabIndex="13" CssClass="btn full_width_btn btn-s-md btn-danger" OnClick="lnkbtnCancel_OnClick" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
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
    <asp:HiddenField ID="hidtotrec" runat="server" />
    <%--   </ContentTemplate>
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
