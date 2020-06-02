<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RateMasterWithParty.aspx.cs" Inherits="WebTransport.RateMasterWithParty" %>
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
            <div class="row ">
                <div class="col-lg-2">
                </div>
                <div class="col-lg-8">
                    <section class="panel panel-default full_form_container part_purchase_bill_form">
	                  <header class="panel-heading font-bold form_heading"> Rate Master [Party Wise]
                      	<span class="view_print">&nbsp;
                        <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ToolTip="Export to excel" ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px"  Visible="false" /></span>
	                  </header>
	                  <div class="panel-body">
	                    <form class="bs-example form-horizontal">
	                      <!-- first  section --> 
	                      <div class="clearfix first_section">
	                        <section class="panel panel-in-default">  
	                          <div class="panel-body">
	                          	<div class="clearfix estimate_fourth_row odd_row">
	                              <div class="col-sm-3">
	                                <label class="control-label">Date Range</label>
	                                <div>
                                    <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control"  TabIndex="1"   ></asp:DropDownList>	                                                  
	                                </div>
	                              </div>
	                             	<div class="col-sm-3">
	                                <label class="control-label">Location[From]<span class="required-field">*</span></label>
	                                <div>
                                      <asp:DropDownList ID="drpBaseCity" runat="server" TabIndex="2" CssClass="form-control" OnSelectedIndexChanged="drpBaseCity_SelectedIndexChanged" AutoPostBack="true"   >
                                         </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvdrpBaseCity" runat="server" ControlToValidate="drpBaseCity"
                                        Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Save"
                                        class="classValidation" ErrorMessage="Select from city!"></asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="HidFrmCityIdno" runat="server" />	                                      
	                                </div>
	                              </div>

	                              <div class="col-sm-3">
	                                <label class="control-label">Party Name<span class="required-field">*</span></label>
                                    <asp:DropDownList ID="ddlPartyName" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged" AutoPostBack="true" >
                                     </asp:DropDownList>                                                                                             
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPartyName" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Save" class="classValidation" ErrorMessage="Select Party Name!"></asp:RequiredFieldValidator>	                                                        
	                              </div>

                                  <div class="col-sm-3">
	                                <label class="control-label">Product Name<span class="required-field">*</span></label>
	                                <div>
                                     <asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control"  TabIndex="4" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" AutoPostBack="true">
                                     </asp:DropDownList>                                                                                             
                                    <asp:RequiredFieldValidator ID="rfvddlItmName" runat="server" ControlToValidate="ddlItemName" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Save" class="classValidation" ErrorMessage="Select product name!"></asp:RequiredFieldValidator>	                                                        
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
                                    <asp:TextBox ID="txtDateRate" runat="server" CssClass="input-sm datepicker form-control" TabIndex="4"   oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" data-date-format="dd-mm-yyyy" ></asp:TextBox>
	                                </div>
	                              </div>
                                 <div class="col-sm-3">
                                    <label class="control-label">City Via<span class="required-field">*</span></label>
	                                <div>
                                    <asp:DropDownList ID="ddlCityVia" runat="server" CssClass="form-control" 
                                              TabIndex="5" Width="170px">
                                        </asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select city!"
                                            ControlToValidate="ddlCityVia" Display="Dynamic" SetFocusOnError="true" InitialValue="0"
                                            CssClass="classValidation" ValidationGroup="Save"></asp:RequiredFieldValidator>	   
                                        </div>                           
	                              </div>
	                             <div class="col-sm-3">
                                    <label class="control-label">City To<span class="required-field">*</span></label>
	                                <div>
                                    <asp:DropDownList ID="ddlCity"  runat="server" 
                                            CssClass="form-control"  TabIndex="5">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlCity" runat="server" ErrorMessage="Please select city!"
                                            ControlToValidate="ddlCity" Display="Dynamic" SetFocusOnError="true" InitialValue="0"
                                            CssClass="classValidation" ValidationGroup="Save"></asp:RequiredFieldValidator>	                                
	                                </div>
	                              </div>
                                    <div class="col-sm-2">
	                                <label class="control-label" id="lblWeight" runat="server">Weight<span class="required-field">*</span></label>
	                                <div>
                                    <asp:TextBox ID="txt_Weight" runat="server" CssClass="form-control" TabIndex="6"  MaxLength="5" Style="text-align: right;" Text="0.00" Width="100px"></asp:TextBox>                                                                                              
                                    <asp:RequiredFieldValidator ID="rfv_Weight" runat="server" ErrorMessage="Please enter item weight" ControlToValidate="txt_Weight" Display="Dynamic" ValidationGroup="Save" SetFocusOnError="true" InitialValue="0" CssClass="classValidation"></asp:RequiredFieldValidator>	                                 
	                                </div>
                                    </div>
                                 <div class="col-sm-2">
	                                <label class="control-label">Rate<span class="required-field">*</span></label>
	                                <div>
                                    <asp:TextBox ID="txtItemRate" runat="server" CssClass="form-control" TabIndex="6"  MaxLength="8" Style="text-align: right;" Text="0.00" Width="100px"></asp:TextBox>                                                                                              
                                    <asp:RequiredFieldValidator ID="rfvItemRate" runat="server" ErrorMessage="Please enter item rate" ControlToValidate="txtItemRate" Display="Dynamic" ValidationGroup="Save" SetFocusOnError="true" InitialValue="0" CssClass="classValidation"></asp:RequiredFieldValidator>	                                 
	                                </div>
                                    </div>
                                </div>
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
                                        <div class="col-sm-4">
                                    <asp:LinkButton ID="lnkbtnNew"  runat="server" OnClick="lnkbtnNew_OnClick" Visible="false" CssClass="btn full_width_btn btn-s-md btn-info" TabIndex="19" ToolTip="Click to new" >New</asp:LinkButton>
	                                </div>
	                                <div class="col-sm-4">
                                       
                                     <asp:LinkButton ID="lnkbtnSave" runat="server" 
                                            CssClass="btn full_width_btn btn-s-md btn-success" TabIndex="20" 
                                            CausesValidation="true" ValidationGroup="Save" onclick="lnkbtnSave_Click" > <i class="fa fa-save"></i>Save</asp:LinkButton>
	                               <asp:HiddenField ID="hidrateid" runat="server" Value="0" />
                                    <asp:HiddenField ID="Hidrowid" runat="server" Value="" />
                                    <asp:HiddenField ID="HidInvoiceTyp" runat="server" />
                                    <asp:HiddenField ID="dmindate" runat="server" />
                                    <asp:HiddenField ID="hidmindate" runat="server" />
                                        <asp:HiddenField ID="hidmaxdate" runat="server" />
                                        <asp:HiddenField ID="hidSaveType" runat="server" />
	                                </div>
                               
	                                <div class="col-sm-4">
                                     <asp:LinkButton ID="lnkbtnCancel" runat="server" 
                                            CssClass="btn full_width_btn btn-s-md btn-danger" TabIndex="21" 
                                            onclick="lnkbtnCancel_Click" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
	                                </div>
	                              </div>
	                              <div class="col-lg-3"></div>
	                            </div> 
	                          </div>
	                        </section>
	                      </div>        
                          <div class="clearfix third_right">
	                        <section class="panel panel-in-default btns_without_border">                            
	                          <div class="panel-body" style="overflow:auto; height:500px;" id="GridDiv" runat="server">     
                              <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"
                                    Width="100%" GridLines="Both" EnableViewState="true"  BorderWidth="0"   OnRowDataBound="grdMain_RowDataBound"
                                    ShowFooter="true"  PageSize="30 " OnRowCommand="grdMain_RowCommand" OnPageIndexChanging="grdMain_PageIndexChanging" >
                                    <RowStyle CssClass="odd" />
                                    <AlternatingRowStyle CssClass="even" />    
                                    <Columns>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                              <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("PRate_Idno") %>' CommandName="cmdedit"  ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                 <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("PRate_Idno") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete"  ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>                                          
                                            </ItemTemplate>
                                        </asp:TemplateField>                                                                              
                                        <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="40px" />
                                            <ItemStyle Width="40" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblRteDate" runat="server" Text='<%#Convert.ToDateTime(Eval("Rate_Date")).ToString("dd-MM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Location From " HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("LocationName"))%>
                                                <asp:Label ID="lblLocCityIdno" Visible="false"  runat="server" Text='<%#Eval("Loc_Idno")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                      

                                        <asp:TemplateField HeaderText="City From" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("FromCity"))%>
                                                <asp:Label ID="lblFromCityIdno" Visible="false"  runat="server" Text='<%#Eval("FromCityId")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="City To" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("ToCityName"))%>
                                                <asp:Label ID="lblCityIdno" Visible="false"  runat="server" Text='<%#Eval("ToCity_Idno")%>'></asp:Label>
                                            </ItemTemplate>
                                         
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("Item_Name"))%>
                                                <asp:Label ID="lblItemIdno" Visible="false"  runat="server" Text='<%#Eval("Item_Idno")%>'></asp:Label>
                                            </ItemTemplate>
                                           <FooterStyle HorizontalAlign="Left" Font-Bold="true" />
                                            <FooterTemplate>
                                                <b>Total</b>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Weight" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemWeight"  runat="server" Text='<%#Convert.ToString(Eval("Item_Weight"))%>'></asp:Label>
                                            </ItemTemplate>
                                           <FooterStyle HorizontalAlign="Left" Font-Bold="true" />
                                            <FooterTemplate>
                                                
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemRate" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("Item_Rate"))) ? "0.00" : (Convert.ToString((Eval("Item_Rate")))))%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                        <FooterTemplate>
                                              <asp:Label ID="lblItemTotalRate" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("Item_Rate"))) ? "0.00" : (Convert.ToString((Eval("Item_Rate")))))%>'></asp:Label>
                                        </FooterTemplate>
                                        </asp:TemplateField>
                                        
                                      </Columns>
                                </asp:GridView>
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
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
    <script type="text/javascript">
        $(".datepicker").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
            minDate: '<%=hidmindate.Value%>',
            maxDate: '<%=hidmaxdate.Value%>'
        });

        function cityviaddl() {
            var id = document.getElementById("<%=ddlCityVia.ClientID %>").value;
            document.getElementById("<%=ddlCity.ClientID %>").value = id;

        }
    </script>
</asp:Content>
