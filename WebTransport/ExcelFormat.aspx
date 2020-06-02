<%@ Page Title="Excel Format" Language="C#" MasterPageFile="~/Site1.Master"
    EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ExcelFormat.aspx.cs"
    Inherits="WebTransport.ExcelFormat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>--%>
            <div class="row ">
                <div class="col-lg-2">
                </div>
                <div class="col-lg-8">
                    <section class="panel panel-default full_form_container part_purchase_bill_form">
                	<header class="panel-heading font-bold">EXCEL FORMAT
                		</span>
               	 	</header>
                	<div class="panel-body">
                    <form class="bs-example form-horizontal">
                      <!-- first  section --> 
                    	<div class="clearfix first_section">
                        <section class="panel panel-in-default">  
                          <div class="panel-body">
                            <div class="clearfix odd_row">
                        
                              <div class="col-sm-4" style="width:100%">
                                <label class="col-sm-4 control-label" style="width: 100%;text-align:center;">EXCEL FORMAT</label>
                               
                              </div>
                           
                            </div>
                          </div>
                        </section>                        
                    	</div>

                       <!-- second row -->
                      <div class="clearfix fourth_right">
                        <section class="panel panel-in-default btns_without_border">  <div class="panel-body" style="overflow-x:auto;">    
                            <table>
                            <tr>
                            <td> 
                     <asp:GridView ID="grdMain" runat="server" AllowPaging="True"  AutoGenerateColumns="False"  BorderStyle="None" BorderWidth="0px" GridLines="None" OnPageIndexChanging="grdMain_PageIndexChanging" 
                             CssClass="display nowrap dataTable"   OnRowCommand="grdMain_RowCommand" OnRowDataBound="grdMain_RowDataBound"   PageSize="35" TabIndex="6" Width="100%">
                                    <RowStyle CssClass="odd" />
                                        <AlternatingRowStyle CssClass="even" />                                       
                                       <PagerStyle  CssClass="classPager" />
                                         <PagerSettings Mode="NumericFirstLast" PageButtonCount="5"  FirstPageText="First" Position="Bottom" LastPageText="Last"/>                                         
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50" 
                                        HeaderText="Sr.No">
                                      
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>.
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100" HeaderText="Menu Name">                                      
                                        <ItemTemplate>
                                            <%#Eval("Menu_Form")%>
                                             <asp:HiddenField  ID="hidexcelid" runat="server" Value='<%#Eval("ExlFormat_Idno") %>'/>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>                    
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="80" HeaderText="Form Name">
                                       <ItemTemplate>
                                                <%#Eval("Form_Name")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Get Excel" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Center">                                  
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="imgBtnGetExcel"   ImageUrl="~/Images/Excel_Img.JPG" ToolTip="Get Excel Format" CommandName="GetExcel" CommandArgument='<%#Container.DataItemIndex%>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="LblNoRecordFound" runat="server" CssClass="white_bg" 
                                        Text="Record(s) not found."></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                        </tr>
                        <tr>
                        <td>
                        <div class="secondFooterClass"  id="divpaging" runat="server" visible="false">                                                                           
                        <table class="" id="tblFooterscnd" runat="server" >
		                    <tr><th rowspan="1" colspan="1" style="width:300px;"> <asp:Label ID="lblcontant" runat="server"></asp:Label></th><th rowspan="1" colspan="1" style="width: 149px;"></th><th rowspan="1" colspan="1" style="width: 300px;text-align:right;"></th><th rowspan="1" colspan="1" style="width: 210px; text-align: right;"><asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label>
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
                                          
                    </form>
                </div>
              </section>
                </div>
                <div class="col-lg-2">
                </div>
            </div>
            <asp:HiddenField ID="hidDrividno" runat="server" />
   
</asp:Content>
