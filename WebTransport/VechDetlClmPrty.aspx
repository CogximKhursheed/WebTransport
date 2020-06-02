<%@ Page Title="Vehicle Details Form" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="VechDetlClmPrty.aspx.cs" Inherits="WebTransport.VechDetlClmPrty" %>

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
                <header class="panel-heading font-bold form_heading">VEHICLE DETAIL : &nbsp;&nbsp;&nbsp;&nbsp;
                <span><b><asp:Label ID="lblAcntName" runat="server"  Visible="false"></asp:Label></b></span>
                </header>
                <div class="panel-body">
                  <form class="bs-example form-horizontal">
                  <div class="clearfix second_section">
                      <section class="panel panel-in-default">  
                        <div class="panel-body">                          
                          <div class="clearfix odd_row">
                           <div class="col-sm-4">
                              <label class="control-label">Date Range<span class="required-field">*</span></label>
                              <div>
                               <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDateRange" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Save" ErrorMessage="Select Date Range!" CssClass="classValidation">
                                 </asp:RequiredFieldValidator>
                              </div>
                            </div>
                           
                           <div class="col-sm-4">
                              <label class="control-label">Loc.[From]<span class="required-field">*</span></label>
                              <div>
                               <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control"  AutoPostBack="true"
                                      TabIndex="2" onselectedindexchanged="ddlLocation_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ControlToValidate="ddlLocation" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Save" ErrorMessage="Select Location!" CssClass="classValidation">
                                 </asp:RequiredFieldValidator>
                              </div>
                            </div>
                                                       
                           <div class="col-sm-4">
                              <label class="control-label">Tyre Name<span class="required-field">*</span></label>
                              <div>
                               <asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPartno" runat="server" ControlToValidate="ddlItemName"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Save"  ErrorMessage="Select Item Name!" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                                
                          </div>   
                           <div class="clearfix even_row">
                              <div class="col-sm-2">	                                
                              <label class="control-label">Serial No<span class="required-field">*</span></label>
                              <div>
                                <asp:TextBox ID="txtSerialNo" runat="server" placeholder="Enter Serial No." CssClass="form-control" MaxLength="18"  onblur="todigit();" oncopy="return false" oncut="return false"   onDrop="blur();return false;" onKeyPress="return checkfloat(event, this);"  onpaste="return false" Text=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSerial" runat="server" ErrorMessage="Enter Serial No!" ControlToValidate="txtSerialNo" Display="Dynamic" 
                                 SetFocusOnError="true" ValidationGroup="Save" CssClass="classValidation" ></asp:RequiredFieldValidator>
                              </div> 
                             
                            </div>
                             <div class="col-sm-3">
                             	  <label class="control-label">Company Name</label>
                               <div>
                               <asp:TextBox ID="txtCompName" placeholder="Enter Company Name" runat="server" CssClass="form-control" Style="text-align:left;" Text=""></asp:TextBox>
                              
                               </div>
                             </div>
                            <div class="col-sm-3">	                                
                              <label class="control-label">Purchase From </label>
                              <div>
                               <asp:TextBox ID="txtPurchaseFrom" placeholder="Purchase From" runat="server" CssClass="form-control"  Style="text-align: left;"></asp:TextBox>
                              </div> 
                            </div>
                            <div class="col-sm-2">	                                
                              <label class="control-label">Type<span class="required-field">*</span></label>
                              <div>
                               <asp:DropDownList ID="ddltype" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddltype" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Save" ErrorMessage="Select Type!" CssClass="classValidation">
                                 </asp:RequiredFieldValidator>
                              </div> 
                            </div>
                            
                            <div class="col-sm-2">
                                	                                
                              <label class="control-label">Rate</label>
                              <div>
                                  <asp:TextBox ID="txtOpenRate" runat="server" CssClass="form-control" MaxLength="8"  onblur="todigit();" oncopy="return false" oncut="return false"   onDrop="blur();return false;" onKeyPress="return allowOnlyFloatNumber(event);"   onpaste="return false" Style="text-align: right;" Text="0.00" ></asp:TextBox>
                              </div> 
                              </div>
                           </div>                     
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
                              <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-info" Visible="false"><i class="fa fa-file-o"></i>New</asp:LinkButton>
                             </div>                                                    
                              <div class="col-sm-4">
                               <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" OnClick="lnkbtnSave_OnClick" ValidationGroup="Save" CssClass="btn full_width_btn btn-s-md btn-success"><i class="fa fa-save"></i>Save</asp:LinkButton>
                                <asp:HiddenField ID="hidmindate" runat="server" />
                                <asp:HiddenField ID="hidmaxdate" runat="server" />
                              </div>
                              <div class="col-sm-4">
                               <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-danger"><i class="fa fa-close"></i>Cancel</asp:LinkButton>
                              </div>
                            </div>
                            <div class="col-lg-3"></div>
                          </div> 
                        </div>
                      </section>
                    </div>
                    <div class="clearfix third_right">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body" style="overflow-x:auto;">     
                            <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" 
                                BorderStyle="None" Width="100%" GridLines="None" CssClass="display nowrap dataTable"
                            EnableViewState="true" AllowPaging="true"   BorderWidth="0" ShowFooter="true" 
                                OnPageIndexChanging="grdMain_PageIndexChanging" PageSize="50" 
                                OnRowCreated="grdMain_RowCreated" onrowcommand="grdMain_RowCommand">
                            <RowStyle CssClass="odd" />
                            <AlternatingRowStyle CssClass="even" />   
                                <Columns>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="40" HeaderStyle-CssClass="gridHeaderAlignCenter">
                                       <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                        <ItemStyle HorizontalAlign="Center" Width="40" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("SerlDetl_id") %>' CommandName="cmdedit"  ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("SerlDetl_id") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Serial No" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="left">
                                        <ItemStyle Width="100" HorizontalAlign="Left" />
                                        <ItemTemplate>                        
                                            <%#Eval("SerialNo")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Tyre Name" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="left">
                                        <ItemStyle Width="100" HorizontalAlign="Left" />
                                        <ItemTemplate>                        
                                            <%#Eval("Item_Name")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50" HeaderText="Type">
                                        <ItemStyle HorizontalAlign="Left" Width="50" />
                                        <ItemTemplate>
                                        <asp:Label ID="lblTypeName" runat="server" Text='<%#Eval("TyreType_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="50" HeaderText="Rate">
                                        <ItemStyle HorizontalAlign="Right" Width="50" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblOpenRate" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("OpenRate")))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Comp Name">
                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompName" runat="server" Text='<%#Eval("CompName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100" HeaderText="Pur. From">
                                        <ItemStyle HorizontalAlign="Left" Width="100" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPurFrom" runat="server" Text='<%#Eval("PurFrom")%>'></asp:Label>
                                        </ItemTemplate>
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
                     </form>
                </div>
              </section>
        </div>
        <div class="col-lg-1">
        </div>
    </div>
    <asp:HiddenField ID="hidstckidno" runat="server" />
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

<asp:Content ID="Content3" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
</asp:Content>
