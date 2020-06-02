<%@ Page Title="Due Date Lorry Report" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" EnableEventValidation="false" CodeBehind="DueDateLorryReport.aspx.cs"
    Inherits="WebTransport.DueDateLorryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-9">
            <section class="panel panel-default full_form_container part_purchase_bill_form">
                	<header class="panel-heading font-bold">DUE DATE LORRY DETAILS &nbsp;
                	<span class="view_print">&nbsp;
                            <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ImageUrl="~/Images/CSV.png"
                                                OnClick="imgBtnExcel_Click" Visible="false" ToolTip="Export to excel" />
                    </span>
               	 	</header>
                	<div class="panel-body">
                    <form class="bs-example form-horizontal">
                      <!-- first  section --> 
                     

                       <!-- second row -->
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
                                        Width="100%" GridLines="None" AllowPaging="true" PageSize="10" 
                                        BorderWidth="0"  OnRowDataBound="grdMain_RowDataBound" OnRowCommand="grdMain_RowCommand" ShowFooter="true">
                                        <RowStyle CssClass="odd" />
                                        <AlternatingRowStyle CssClass="even" />                                       
                                       <PagerStyle  CssClass="classPager" />
                                         <PagerSettings Mode="NumericFirstLast" PageButtonCount="5"  FirstPageText="First" Position="Bottom" LastPageText="Last"/>
                                         <Columns>
                                            <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="20" HeaderStyle-HorizontalAlign="center">
                                                <HeaderStyle HorizontalAlign="center" Width="20" Font-Bold="true" />
                                                <ItemStyle HorizontalAlign="center" Width="20" />
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>.
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="center" Width="20" Font-Bold="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lorry No." HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="90" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                   <%#Convert.ToString(Eval("Lorry_No"))%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fitness Date" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="70" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                     <%# (string.IsNullOrEmpty(Convert.ToString(Eval("Fitness_Date"))) ? "" : ((Eval("Fitness_Date"))))%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ins Valid Date" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="100" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Ins_Valid_Date"))) ? "" : ((Eval("Ins_Valid_Date"))))%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="RC Date" HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="90" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%#(string.IsNullOrEmpty(Convert.ToString(Eval("RC_Date"))) ? "" : ((Eval("RC_Date"))))%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="N.Permit Date" HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="90" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Nat_Permit_Date"))) ? "" : ((Eval("Nat_Permit_Date"))))%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="A.Permit Date" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="70" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%#(string.IsNullOrEmpty(Convert.ToString(Eval("Auth_Permit_Date"))) ? "" : ((Eval("Auth_Permit_Date"))))%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemTemplate> 
                                                <asp:LinkButton ID="lmkBtnEdit" class="edit" runat="server" CommandArgument='<%#Eval("Lorry_Idno") %>'
                                                    CommandName="cmdedit" ToolTip="Edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                                           
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
                                        <div class="col-sm-3" style="text-align:left"><asp:Label ID="lblcontant" runat="server"></asp:Label></div>  
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
        </div>
    </div>
</asp:Content>
