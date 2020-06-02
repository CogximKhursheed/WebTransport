<%@ Page Title="RateMastFT" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" EnableEventValidation="false" CodeBehind="RateMasterFT.aspx.cs" Inherits="WebTransport.RateMasterFT" %>

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
          $('#<%=txtDateRate.ClientID %>').datepicker({
              buttonImageOnly: false,
              maxDate: 0,
              changeMonth: true,
              changeYear: true,
              dateFormat: 'dd-mm-yy',
              minDate: mindate,
              maxDate: maxdate
          });
      }
      function HideBillAgainst() {
          $("#dvGrdetails").fadeOut(300);
      }

      function ShowClient() {
          $("#dvGrdetails").fadeIn(300);
      }
      function checkDec(el) {
          var ex = /^[0-9]+\.?[0-9]*$/;
          if (ex.test(el.value) == false) {
              return false;
          }
          else {
              return true;
          }
      }
    </script>
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-2">
                </div>
                <div class="col-lg-8">
                    <section class="panel panel-default full_form_container part_purchase_bill_form">
	                 <header class="panel-heading font-bold form_heading">RATE MASTER
                     <span class="view_print">&nbsp;
                      
									<asp:ImageButton ID="imgBtnExcel" runat="server" 
                             AlternateText="Excel" ToolTip="Export to excel" 
                             ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px"  Visible="false" 
                             onclick="imgBtnExcel_Click" /></span>
	                  </header>
	                  <div class="panel-body">
	                    <form class="bs-example form-horizontal">
	                      <!-- first  section --> 
	                      <div class="clearfix first_section">
	                        <section class="panel panel-in-default">  
	                          <div class="panel-body">
	                          	<div class="clearfix estimate_fourth_row odd_row">
	                              <div class="col-sm-4">
	                                <label class="control-label">Date Range</label>
	                                <div>
                                    <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control"  
                                            TabIndex="1"  AutoPostBack="True" 
                                            onselectedindexchanged="ddlDateRange_SelectedIndexChanged"></asp:DropDownList>	                                                  
	                                </div>
	                              </div>
	                             	<div class="col-sm-4">
	                                <label class="control-label">Location[From]<span class="required-field">*</span></label>
	                                <div>
                                      <asp:DropDownList ID="drpBaseCity" runat="server" TabIndex="2" 
                                            CssClass="form-control"  AutoPostBack="true" 
                                            onselectedindexchanged="drpBaseCity_SelectedIndexChanged">
                                         </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvdrpBaseCity" runat="server" ControlToValidate="drpBaseCity"
                                        Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Save"
                                        class="classValidation" ErrorMessage="Select from city!"></asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="HidFrmCityIdno" runat="server" />	                                      
	                                </div>
	                              </div>

	                              <div class="col-sm-4">
	                                <label class="control-label">Lorry Type<span class="required-field">*</span></label>
	                                <div>
                                     <asp:DropDownList ID="ddlLorryType" runat="server" CssClass="form-control" 
                                            AutoPostBack="True"  TabIndex="3" 
                                            onselectedindexchanged="ddlLorryType_SelectedIndexChanged">
                                     </asp:DropDownList>                                                                                             
                                    <asp:RequiredFieldValidator ID="rfvddlItmName" runat="server" ControlToValidate="ddlLorryType" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Save" class="classValidation" ErrorMessage="Select Lorry Type"></asp:RequiredFieldValidator>	                                                        
	                                </div>
	                              </div>
	                            </div>	                                                
	                          </div>
	                        </section>                        
	                      </div>
	                       <!-- second  section -->
	                   		<div class="clearfix second_section">
	                        <section class="panel panel-in-default">  
	                          <div class="panel-body">
	                          	<div class="clearfix estimate_fourth_row even_row">
                                 <div class="col-sm-2">
	                                <label class="control-label">Date</label>
	                                <div>
                                    <asp:TextBox ID="txtDateRate" runat="server" CssClass="input-sm datepicker form-control" TabIndex="4"  oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" data-date-format="dd-mm-yyyy" ></asp:TextBox>
	                                </div>
	                              </div>
                               	<div class="col-sm-3">
                                    <label class="control-label">City To<span class="required-field">*</span></label>
	                                <div>
                                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"  TabIndex="5">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlCity" runat="server" ErrorMessage="Please select city "
                                            ControlToValidate="ddlCity" Display="Dynamic" SetFocusOnError="true" InitialValue="0"
                                            CssClass="classValidation" ValidationGroup="Submit"></asp:RequiredFieldValidator>	                                
	                                </div>
	                              </div>

                                  
                                  <div class="col-sm-2">
                                   
	                                <label class="control-label">Freight Amount<span class="required-field">*</span></label>
	                                <div>
                                    <asp:TextBox ID="txtfrghtAmount" runat="server" CssClass="form-control" TabIndex="6"  MaxLength="8" Style="text-align: right;" Text="0.00" Width="170px"></asp:TextBox>                                                                                              
                                    <asp:RequiredFieldValidator ID="rfvItemRate" runat="server" ErrorMessage="Please enter Freight Amount" ControlToValidate="txtfrghtAmount" Display="Dynamic" ValidationGroup="Submit" SetFocusOnError="true" InitialValue="0" CssClass="classValidation"></asp:RequiredFieldValidator>	                                 
	                                </div>
                                   
                                    </div>
                                   <div class="col-sm-5">
	                                <div class="col-sm-6">
                                    <asp:LinkButton ID="lnkbtnSubmit" runat="server"  
                                            CssClass="btn full_width_btn btn-sm btn-primary subnew" TabIndex="7"  
                                            ToolTip="Click to Submit" CausesValidation="true" ValidationGroup="Submit" 
                                            onclick="lnkbtnSubmit_Click" >Submit</asp:LinkButton>                                   
                                  </div>
                                  <div class="col-sm-6">
                                   <asp:LinkButton ID="lnkbtnNew" runat="server"  
                                          CssClass="btn full_width_btn btn-sm btn-primary subnew" TabIndex="8" 
                                          ToolTip="Click to new" onclick="lnkbtnNew_Click" >New</asp:LinkButton>
                                  </div>
	                              </div>
	                            
	                            </div>
	                                                     
	                          </div>
	                        </section>                        
	                      </div>
	                      
	                      <div class="clearfix third_right">
	                        <section class="panel panel-in-default btns_without_border">                            
	                          <div class="panel-body" style="overflow-x:scroll;">     
                              <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" 
                                      BorderStyle="None" CssClass="display nowrap dataTable"
                                    Width="100%" GridLines="Both" EnableViewState="true" AllowPaging="true" BorderWidth="0"
                                    ShowFooter="true" PageSize="30" onrowdatabound="grdMain_RowDataBound" 
                                      onrowcommand="grdMain_RowCommand" 
                                      onpageindexchanging="grdMain_PageIndexChanging">
                                    <RowStyle CssClass="odd" />
                                    <AlternatingRowStyle CssClass="even" />    
                                    <Columns>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                              <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("id") %>' CommandName="cmdedit" TabIndex="5" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                 <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("id") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" TabIndex="6" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>                                          
                                            </ItemTemplate>
                                        </asp:TemplateField>                                                                              
                                        <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                            <ItemStyle Width="40" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                               <asp:Label ID="lblRteDate" runat="server" Text='<%#Convert.ToDateTime(Eval("Rate_Date")).ToString("dd-MM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                         
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Lorry Type" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("Lorry_Type"))%>
                                                
                                            </ItemTemplate>
                                           <FooterStyle HorizontalAlign="Left" />
                                            <FooterTemplate>
                                                <b>Total</b>
                                            </FooterTemplate>
                                          </asp:TemplateField>
                                          
                                        <asp:TemplateField HeaderText="CityTo" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("City_Name"))%>
                                            </ItemTemplate>
                                             
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotRecords" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="From City" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("City_FName"))%>
                                            </ItemTemplate>
                                        
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Freight Amount" HeaderStyle-Width="50" HeaderStyle-CssClass="gridHeaderAlignRight">
                                            <HeaderStyle HorizontalAlign="Right" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemRate" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("Item_Rate"))) ? "" : (Convert.ToString((Eval("Item_Rate")))))%>'></asp:Label>
                                    
                                            </ItemTemplate>
                                             
                                        </asp:TemplateField>

                                                                           
                                    </Columns>
                                </asp:GridView>
	                          </div>
	                        </section>
	                      </div> 

	                       <!-- fourth row -->
	                      <div class="clearfix fourth_right">
	                        <section class="panel panel-in-default btns_without_border">                            
	                          <div class="panel-body">     
	                            <div class="clearfix odd_row">
	                              <div class="col-lg-3"></div>
	                              <div class="col-lg-6">   
	                                <div class="col-sm-6">
                                     <asp:LinkButton ID="lnkbtnSave" runat="server" 
                                            CssClass="btn full_width_btn btn-s-md btn-success" TabIndex="9" 
                                            CausesValidation="true" ValidationGroup="Save" onclick="lnkbtnSave_Click" > <i class="fa fa-save"></i>Save</asp:LinkButton>
	                               <asp:HiddenField ID="hidrateid" runat="server" Value="0" />
                                    <asp:HiddenField ID="Hidrowid" runat="server" Value="" />
                                    <asp:HiddenField ID="HidInvoiceTyp" runat="server" />
                                    <asp:HiddenField ID="dmindate" runat="server" />
                                    <asp:HiddenField ID="hidmindate" runat="server" />
                                        <asp:HiddenField ID="hidmaxdate" runat="server" />
	                                </div>
	                                <div class="col-sm-6">
                                     <asp:LinkButton ID="lnkbtnCancel" runat="server" 
                                            CssClass="btn full_width_btn btn-s-md btn-danger" TabIndex="10" 
                                            onclick="lnkbtnCancel_Click"><i class="fa fa-close"></i>Cancel</asp:LinkButton>
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
                <div class="col-lg-2">
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgBtnExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
    <script type="text/javascript">
        $(".datepicker").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
            minDate: '<%=hidmindate.Value%>',
            maxDate: '<%=hidmaxdate.Value%>'
        });

     
    </script>
</asp:Content>