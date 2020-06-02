<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="GrPrepOntohire.aspx.cs" Inherits="WebTransport.GrPrepOntohire" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-1">
                </div>
                <div class="col-lg-10">
              <section class="panel panel-default full_form_container quotation_master_form">
              <header class="panel-heading font-bold form_heading">GR PREP[OWN TO HIRE]
                             <span class="view_print">   <a href="ManageGrPrepOntohire.aspx" >     <asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a> </span>
              </header>
                <div class="panel-body">
                  <form class="bs-example form-horizontal">
                    <!-- first  section --> 
                    <div class="clearfix first_section">
                      <section class="panel panel-in-default">  
                        <div class="panel-body">
                        	<div class="clearfix odd_row">
                            <div class="col-sm-5">
                             <label class="col-sm-4 control-label" style="width: 29%;">Date Range<span class="required-field">*</span></label>
                              <div class="col-sm-8" style="width: 71%;">
                               <asp:DropDownList ID="ddldateRange" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="1" >
                               </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddldateRange"
                                CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Year!" InitialValue="0"
                                SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>

                             </div>
                          </div>
                            <div class="col-sm-5">
                            <label class="col-sm-4 control-label" >Select Excel</label>
                            <div class="col-sm-8">
                           <asp:FileUpload ID="FileUpload"  runat="server"  Width="180px" />
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="FileUpload" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Please select excel file!" ValidationGroup="main" ></asp:RequiredFieldValidator>
                              </div>
                           </div>
                             
                           	<div class="col-sm-2">
                             <asp:LinkButton ID="lnkbtnUpload"  ToolTip="" runat="server" 
                                                     CssClass="btn full_width_btn btn-s-md btn-info" onclick="lnkbtnUpload_Click" ><i class="fa fa-upload"></i>Upload</asp:LinkButton>
                                </div>
                            
                             </div>
                             <div class="clearfix even_row">
                             <div class="col-sm-4"></div>
                                 <div class="col-sm-5"></div>
                             <div class="col-sm-3">
                                <asp:LinkButton ID="lnkbtnExport" runat="server" 
                                                    CssClass="btn full_width_btn btn-s-md btn-info" onclick="lnkbtnExport_Click" 
                                                        ><i CausesValidation="true" class="fa fa-upload"></i>Export Excel</asp:LinkButton>
                   </div>
                             
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
            <asp:HiddenField ID="hidMtrlRcptid" runat="server" />
            <asp:HiddenField ID="hidMtrlTrnsfDt" runat="server" />
            <asp:HiddenField ID="hidmindate" runat="server" />
            <asp:HiddenField ID="hidmaxdate" runat="server" />
        </ContentTemplate>
   
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
</asp:Content>
