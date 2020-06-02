<%@ Page Title="Whats New" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" EnableEventValidation="false" CodeBehind="WhatsNew.aspx.cs"
    Inherits="WebTransport.WhatsNew" %>

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

        $("#<%=txtDate.ClientID %>").datepicker({
            buttonImageOnly: false,
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
                <div class="col-lg-3">
                </div>
                <div class="col-lg-6">
                    <section class="panel panel-default full_form_container part_purchase_bill_form">
                	<header class="panel-heading font-bold">Whats New
                		
                        &nbsp; <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" TabIndex="4" ToolTip="Export to excel" ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px" OnClick="imgBtnExcel_Click" visible="false"/>
                        
               	 	</header>
                	<div class="panel-body">
                    <form class="bs-example form-horizontal">
                      <!-- first  section --> 
                      	<div class="clearfix first_section">
	                        <section class="panel panel-in-default">  
	                          <div class="panel-body">
	                            <div class="clearfix estimate_second_row odd_row">	                            	
                                <div class="col-sm-6">
                                  <label class="col-sm-5 control-label">Date</label>
                                  <div class="col-sm-7">
                                   <asp:TextBox ID="txtDate" runat="server" CssClass="input-sm datepicker form-control" Placeholder="Date" data-date-format="dd-mm-yyyy" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="2"></asp:TextBox>                                 
                                  </div>
                                </div>
                                <div class="col-sm-6">
                                  <label class="col-sm-5 control-label">Form Name</label>
                                  <div class="col-sm-7">
                                  <asp:DropDownList ID="ddlForm" runat="server" CssClass="form-control" TabIndex="1"></asp:DropDownList>  
                                  <asp:RegularExpressionValidator ID="rfvddlform" runat="server" ErrorMessage="Please select Form Name" ControlToValidate="ddlForm"
                                      CssClass="classValidation" Display="Dynamic" InitialValue="0" SetFocusOnError="true" ValidationGroup="save" ></asp:RegularExpressionValidator>                              
                                  </div>
                                </div>		                                
                                                     
                            	</div>  
                                     <div class="clearfix even_row">                            
                                          <div class="col-sm-11">
                                          <label class="col-sm-3 control-label">Details</label>
                                          <div class="col-sm-7" style="width:100;">
                                          <asp:TextBox ID="txtDetails" runat="server" CssClass="form-control" 
                                          Placeholder="Details" TabIndex="3" TextMode="MultiLine"></asp:TextBox>
                                          </div>
                                  </div>
                                
                                       </div>   
                                       <div class="clearfix">
                                        <section class="panel panel-in-default">                            
                                        <div class="panel-body">  
                                            <div class="clearfix odd_row">
                                            <div class="col-sm-3">
                                               <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" 
                                                    ValidationGroup="save" TabIndex="22" 
                                                    CssClass="btn full_width_btn btn-s-md btn-success" onclick="lnkbtnSave_Click" ><i class="fa fa-save"></i>Save</asp:LinkButton></div>     
                                                    <div class="col-sm-4" style="width:125px">
                                                    <asp:LinkButton ID="lnkbtnExcel" runat="server" 
                                                            CssClass="btn full_width_btn btn-s-md btn-info" onclick="lnkbtnExcel_Click"><i CausesValidation="true" class="fa fa-upload"></i>Export</asp:LinkButton>                 
                                               <asp:HiddenField ID="hidmindate" runat="server" />
                                               <asp:HiddenField ID="hidmaxdate" runat="server" />
                                                </div></div></div></section>
                            </div>                
	                          </div>
	                        </section>                        
                      	</div>

                       <!-- second row -->
                      <div class="clearfix fourth_right">
                        <section class="panel panel-in-default btns_without_border">                            
                          <div class="panel-body" style="overflow-x:auto;">     
                            <div class="clearfix">
		                        
                                      <table>
                                       <tr>
                                       
                                           <td>
                                               <div ID="divpaging" runat="server" class="secondFooterClass" visible="false">
                                                   <table ID="tblFooterscnd" runat="server" class="">
                                                       <tr>
                                                           <th colspan="1" rowspan="1" style="width:149px;">
                                                               <asp:Label ID="lblcontant" runat="server"></asp:Label>
                                                           </th>
                                                           <th colspan="1" rowspan="1" style="width: 129px;">
                                                           </th>
                                                           <th colspan="1" rowspan="1" style="width: 120px;text-align:right;">
                                                               &nbsp;</th>
                                                           <th colspan="1" rowspan="1" style="width: 110px;padding-left:60px;">
                                                               <asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label>
                                                           </th>
                                                           <th colspan="1" rowspan="1" style="width:2px;">
                                                           </th>
                                                           <th colspan="1" rowspan="1" style="width: 62px;">
                                                           </th>
                                                           <th colspan="1" rowspan="1" style="width: 63px;">
                                                           </th>
                                                       </tr>
                                                       </tfoot>
                                                   </table>
                                               </div>
                                           </td>
                                       
                                       </tr>
                                        <tr>
                                       <td>
                                           <br />
                                           &nbsp;
                                       </td>
                                       </tr>
                                       </table>
                                        <br /> 
		                        
		                        </div> 
                          </div>
                        </section>
                      </div>                      
                                          
                    </form>
                </div>
              </section>
                </div>
                <div class="col-lg-3">
                </div>
            </div>
            <asp:HiddenField ID="hidcolridno" runat="server" />
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
</asp:Content>