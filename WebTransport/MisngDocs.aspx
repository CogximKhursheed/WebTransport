<%@ Page Title=" Fleet Booking Report" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" EnableEventValidation="false" CodeBehind="MisngDocs.aspx.cs"
    Inherits="WebTransport.MisngDocs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-10">
            <section class="panel panel-default full_form_container part_purchase_bill_form">
                	<header class="panel-heading font-bold">Trace Missing Documents
                		<span class="view_print">&nbsp;  
                 </span>
               	 	</header>
                	<div class="panel-body">
                    <form class="bs-example form-horizontal">
                      <!-- first  section --> 
                      	<div class="clearfix first_section">
	                        <section class="panel panel-in-default">  
	                          <div class="panel-body">
	                            <div class="clearfix odd_row">
	                            	<div class="col-sm-5">
                                  <label class="col-sm-3 control-label">DateRange</label>
                                  <div class="col-sm-9">
                                     <asp:DropDownList ID="ddlDateRange" runat="server"  CssClass="form-control"
                                         TabIndex="1" >
                                    </asp:DropDownList>
                                  </div>
                                </div>
                                <div class="col-sm-3">
                                  <label class="col-sm-4 control-label">Category</label>
                                  <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"  TabIndex="2">
                                        <asp:ListItem Text="Advance Booking" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Quotation" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="GR Preparation" Value="3" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Dispatch Challan" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Invoice" Value="4"></asp:ListItem>
                                    </asp:DropDownList>     
                                  </div>
                                </div>
                                <div class="col-sm-2">
                                  <label class="col-sm-3 control-label">From</label>
                                  <div class="col-sm-9">
                                    <asp:TextBox ID="txtFrom" CssClass="form-control" runat="server"></asp:TextBox>
                                  </div>
                                </div>	
                                <div class="col-sm-2">
                                  <label class="col-sm-2 control-label">To</label>
                                  <div class="col-sm-10">
                                    <asp:TextBox ID="txtTo" CssClass="form-control" runat="server"></asp:TextBox>
                                  </div>
                                </div>                                
                            	</div>
                                <div class="clearfix odd_row">
                                 <div class="col-sm-12">
                                 <div class="col-sm-offset-10 prev_fetch">
                                  <asp:LinkButton runat="server" ID="lnkBtnPreview" 
                                          CssClass="btn full_width_btn btn-sm btn-primary" 
                                          onclick="lnkBtnPreview_Click" TabIndex="8" ><i class="fa fa-search-plus"></i>Preview</asp:LinkButton>
                                          
                                  </div>
                                  </div>
                            	</div>
	                          </div>
	                        </section>                        
                      	</div>

                       <!-- second row -->
                      <div class="clearfix fourth_right" style="overflow: auto;">
                        <section class="panel panel-in-default btns_without_border">                            
                          <div class="panel-body">     
                            <div class="clearfix">
		                          <section class="panel panel-default full_form_container material_search_pop_form">
		                            <div class="panel-body">                                   
		                               <!-- First Row start -->  
                                       <asp:GridView ID="grdMain"  runat="server" AutoGenerateColumns="false" BorderStyle="None"  GridLines="None" BorderWidth="0" ShowFooter="true" CssClass="display rap dataTable"
                                                AllowPaging="false">
                                                <RowStyle CssClass="odd" />
                                                <AlternatingRowStyle CssClass="even" />
                                                <Columns>
                                                 <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                    <ItemTemplate>
                                                        <b><%#Container.DataItemIndex+1 %>.</b>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150"  HeaderText="Location">                                                     
                                                        <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLoc" runat="server" Text='<%#(String.IsNullOrEmpty(Eval("CityName").ToString()) ? "NA" : Eval("CityName"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Doc No. [Min-Max]">
                                                        <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRange" runat="server" Text='<%#string.Concat(Eval("MINId")," - ",Eval("MAXId"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left"  HeaderText="Missing No.">                                                      
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true"/>
                                                        <ItemTemplate>
                                                          <asp:Label ID="lblMissingNo" runat="server" Text='<%#string.IsNullOrEmpty(Eval("MissingNo").ToString())?"NA" :  (((Eval("MissingNo").ToString().Length) > 100) ? (Eval("MissingNo").ToString().Substring(1, 100).ToString() + "...."):  (Eval("MissingNo").ToString().Substring(1,(Eval("MissingNo").ToString().Length)-1).ToString()))%>' ToolTip='<%#Eval("MissingNo")%>'></asp:Label>
                                                          </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns> 
                                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                        <EmptyDataTemplate>
                                            <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" ></asp:Label>
                                        </EmptyDataTemplate>                                                            
                                            </asp:GridView>
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
    </div>
    <asp:HiddenField ID="hidmindate" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />
    <script language="javascript" type="text/javascript">
        SetFocus();
        function SetFocus() {
            $('input[type="text"]').focus(function () {
                $(this).addClass("focus");
            });
            $('input[type="text"]').blur(function () {
                $(this).removeClass("focus");
            });
            $("select").focus(function () {
                $(this).addClass("focus");
            });
            $("select").blur(function () {
                $(this).removeClass("focus");
            });
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_beginRequest(function () {
            SetFocus();
            setDatecontrol();
        });

        prm.add_endRequest(function () {
            SetFocus();
            setDatecontrol();
        });


        function ShowMessage(value) {
            alert(value);
        }
    </script>
    <script language="javascript" type="text/javascript">
        cal()
        {

        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
</asp:Content>
