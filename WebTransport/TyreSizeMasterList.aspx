<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="TyreSizeMasterList.aspx.cs" Inherits="WebTransport.TyreSizeMasterList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-2">
                </div>
                <div class="col-lg-8">
                    <section class="panel panel-default full_form_container part_purchase_bill_form">
                	<header class="panel-heading font-bold">Tyre Size LIST
                		<span class="view_print"><a href="TyreSizeMaster.aspx"  >ADD </a> &nbsp;</span>
               	 	</header>
                	<div class="panel-body">
                    <form class="bs-example form-horizontal">
                      <!-- first  section --> 
                      	<div class="clearfix first_section">
	                        <section class="panel panel-in-default">  
	                          <div class="panel-body">
                            <div class="clearfix odd_row">
                              <div class="col-sm-4">
                                <label class="col-sm-4 control-label">Tyre Size</label>
                                <div class="col-sm-8">
                                 <asp:TextBox ID="txttyresize" TabIndex="2"  CssClass="form-control" runat="server" MaxLength="125"></asp:TextBox>
                                </div>
                              </div>
                              <div class="col-sm-4">	                                                      
                                <div class="col-sm-5 prev_fetch">
                                  <asp:LinkButton ID="lnkbtnPreview" 
                                        CssClass="btn full_width_btn btn-sm btn-primary"  TabIndex="3" 
                                        runat="server" onclick="lnkbtnPreview_Click" ><i class="fa fa-search-plus"></i>Preview</asp:LinkButton>
                                </div>
                                <div class="col-sm-7"> 
                                    <asp:Label ID="lblTotalRecord" runat="server" Text="T. Record(s) : 0" CssClass="control-label" ></asp:Label>
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
		                            <div class="panel-body" style="overflow-x:auto;">   
                                     <table>
                                       <tr>
                                       <td> 
                                      <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" 
                                               BorderStyle="None" CssClass="display nowrap dataTable"
                                        Width="100%" GridLines="None" AllowPaging="true" PageSize="50" 
                                        BorderWidth="0" TabIndex="7" onpageindexchanging="grdMain_PageIndexChanging" 
                                               onrowcommand="grdMain_RowCommand" onrowdatabound="grdMain_RowDataBound" >
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
                                                <asp:TemplateField HeaderText="Tyre Size" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="250px">
                                                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                    <ItemTemplate>
                                                        <%#Eval("TyreSize")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                                                               
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="70" />
                                                    <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("TyreSize_Idno") %>' CommandName="cmdEdit" TabIndex="7" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("TyreSize_Idno") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" TabIndex="8" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                                          
                    </form>
                </div>
              </section>
                </div>
                <div class="col-lg-2">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
