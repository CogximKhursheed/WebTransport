<%@ Page Title="Rate Master" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" 
    CodeBehind="RateMaster.aspx.cs" EnableEventValidation="false" Inherits="WebTransport.RateMaster" %>

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
	                  <header class="panel-heading font-bold form_heading"> <asp:Label ID="lbltxt" runat="server"></asp:Label>
                      	<span class="view_print">&nbsp;
                        <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ToolTip="Export to excel" ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px" OnClick="imgBtnExcel_Click" Visible="false" /></span>
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
                                    <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control"  TabIndex="1"  AutoPostBack="True" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged"></asp:DropDownList>	                                                  
	                                </div>
	                              </div>
	                             	<div class="col-sm-4">
	                                <label class="control-label">Location[From]<span class="required-field">*</span></label>
	                                <div>
                                      <asp:DropDownList ID="drpBaseCity" runat="server" TabIndex="2" CssClass="form-control"  AutoPostBack="true" OnSelectedIndexChanged="drpBaseCity_SelectedIndexChanged">
                                         </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvdrpBaseCity" runat="server" ControlToValidate="drpBaseCity"
                                        Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Save"
                                        class="classValidation" ErrorMessage="Select from city!"></asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="HidFrmCityIdno" runat="server" />	                                      
	                                </div>
	                              </div>

	                              <div class="col-sm-4">
	                                <label class="control-label">Product Name<span class="required-field">*</span></label>
	                                <div>
                                     <asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control" AutoPostBack="True"  TabIndex="3" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged">
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
                                    <asp:TextBox ID="txtDateRate" runat="server" CssClass="input-sm datepicker form-control" TabIndex="4"  oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" data-date-format="dd-mm-yyyy" ></asp:TextBox>
	                                </div>
	                              </div>
                                  	<div class="col-sm-2">
                                    <label class="control-label">City Via</label>
	                                <div>
                                    <asp:DropDownList ID="ddlCityVia" runat="server" CssClass="form-control" 
                                              TabIndex="5" Width="170px" AutoPostBack="true"
                                            onselectedindexchanged="ddlCityVia_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        </div>                           
	                              </div>
	                             	<div class="col-sm-2">
                                    <label class="control-label">City To</label>
	                                <div>
                                    <asp:DropDownList ID="ddlCity" AutoPostBack="true" runat="server" 
                                            CssClass="form-control"  TabIndex="5" 
                                            OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" >
                                        </asp:DropDownList>
                                       <%-- <asp:RequiredFieldValidator ID="rfvddlCity" runat="server" ErrorMessage="Please select city!"
                                            ControlToValidate="ddlCity" Display="Dynamic" SetFocusOnError="true" InitialValue="0"
                                            CssClass="classValidation" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>	                                
	                                </div>
	                              </div>
                                 
                                    <div class="col-sm-2">
	                                <label class="control-label">IRate[Qty]<span class="required-field">*</span></label>
	                                <div>
                                    <asp:TextBox ID="txtItemRate" runat="server" CssClass="form-control" TabIndex="6"  MaxLength="8" Style="text-align: right;" Text="0.00" Width="100px"></asp:TextBox>                                                                                              
                                    <asp:RequiredFieldValidator ID="rfvItemRate" runat="server" ErrorMessage="Please enter item rate" ControlToValidate="txtItemRate" Display="Dynamic" ValidationGroup="Submit" SetFocusOnError="true" InitialValue="0.00" CssClass="classValidation"></asp:RequiredFieldValidator>	                                 
	                                </div>
                                    </div>
                                      <div class="col-sm-2">
	                                <label class="control-label">IRate[Weight]<span class="required-field">*</span></label>
	                                <div>
                                      <asp:TextBox ID="txtItemWighRate" runat="server" CssClass="form-control"  TabIndex="7" MaxLength="8" Style="text-align: right;" Text="0.00" Width="120px"></asp:TextBox><br />
                                        <asp:RequiredFieldValidator ID="rfvItemWghtRate" runat="server" ErrorMessage="Please enter weight rate!"
                                            ControlToValidate="txtItemWighRate" Display="Dynamic" SetFocusOnError="true" InitialValue="0.00" CssClass="classValidation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
	                               
	                                </div>
	                              </div>
                                   

                                   <div class="col-sm-2">
	                                <label class="control-label">Qty Shrtg Limit</label>
	                                <div>
                                    <asp:TextBox ID="txtQtyShrLimit" runat="server" CssClass="form-control"  MaxLength="8" Style="text-align: right;" Text="0.00" TabIndex="8"   onKeyPress="return checkfloat(event, this);" onDrop="blur();return false;" onblur="todigit();"  onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox><br />
	                              
	                                </div>
	                              </div>

	                            
	                            </div>
	                            <div class="clearfix estimate_fourth_row even_row">
	                              
	                             	<div class="col-sm-3">
	                                <label class="control-label">Qty Shrtg Rate</label>
	                                <div>
                                      <asp:TextBox ID="txtQtyShrtgRate" runat="server" CssClass="form-control" TabIndex="9"  MaxLength="8" Style="text-align: right;" Text="0.00"></asp:TextBox>
	                                
	                                </div>
	                              </div>

	                              <div class="col-sm-3">
	                                <label class="control-label">Wght Shrtg Limit</label>
	                                <div>
                                     <asp:TextBox ID="txtWghtShgLimit" runat="server" CssClass="form-control"  TabIndex="10" MaxLength="8" Style="text-align: right;" Text="0.00"></asp:TextBox>	                              
	                                </div>
	                              </div>
	                              <div class="col-sm-3">
	                                <label class="control-label">Wght Shrtg Rate</label>
	                                <div>
                                       <asp:TextBox ID="txtWghtShgRate" runat="server" CssClass="form-control" TabIndex="11"  MaxLength="8" Style="text-align: right;" Text="0.00"></asp:TextBox>	                               
	                                </div>
	                              </div>

                                  <div class="col-sm-3">
                                  <div class="col-sm-6">
                                    <asp:Label ID="lblContSize" runat="server" Text="Cont.Size" class="control-label" Font-Bold="true"></asp:Label>
	                                <div>
                                        <asp:DropDownList ID="drpConSize" runat="server" CssClass="form-control"  TabIndex="12"  >
                                        </asp:DropDownList>
	                                </div>
                                    </div>
                                     <div class="col-sm-6">
                                    <asp:Label ID="lblContWght" runat="server" Text="Cont.Weight" class="control-label" Font-Bold="true"></asp:Label>
	                                <div>
                                      <asp:TextBox ID="txtConWght" runat="server" CssClass="form-control" TabIndex="13"  MaxLength="8" Style="text-align: right;" Text="0.00"></asp:TextBox>	                               
	                                </div>
                                    </div>
	                              </div>

	                            </div>
	                            <div class="clearfix estimate_fourth_row even_row">
	                              <div class="col-sm-3">
	                                <label class="control-label">Item Rate 2</label>
	                                <div>
                                    <asp:TextBox ID="txtItemRate2" runat="server" CssClass="form-control"  MaxLength="8" Style="text-align: right;" Text="0.00" TabIndex="14" onKeyPress="return checkfloat(event, this);" onDrop="blur();return false;" onblur="todigit();" onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox>	                           
	                                </div>
	                              </div>
	                             	<div class="col-sm-3">
	                                <label class="control-label">Item Rate 3</label>
	                                <div>
                                     <asp:TextBox ID="txtItemRate3" runat="server" CssClass="form-control" TabIndex="15"  MaxLength="8" Style="text-align: right;" Text="0.00" onKeyPress="return checkfloat(event, this);"  onDrop="blur();return false;" onblur="todigit();" onpaste="return false" oncut="return false"  oncopy="return false"></asp:TextBox>	                               
	                                </div>
	                              </div>

	                              <div class="col-sm-3">	                                
	                                <div class="col-sm-4">
	                                 	<label class="control-label">I.RateT</label>
		                                <div>
                                         <asp:DropDownList ID="ddlItemratetYP" runat="server" CssClass="form-control" TabIndex="16">
                                            <asp:ListItem Text="Type-1" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Type-2" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Type-3" Value="3"></asp:ListItem>
                                        </asp:DropDownList>	
                                         <asp:RequiredFieldValidator ID="rfvddlItemratetYP" runat="server" ControlToValidate="ddlItemratetYP"
                                            CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Item Rate Type" InitialValue="0" SetFocusOnError="true" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="HiItemRateType" runat="server" />	                                
		                                </div>                              
	                                </div>
	                                <div class="col-sm-3">
	                                	<label class="control-label">KMS</label>
		                                <div>
		                                 <asp:TextBox ID="txtKMS" runat="server" CssClass="form-control"  MaxLength="8" ReadOnly="true" TabIndex="17" onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox>                                                                                           
                                            <asp:HiddenField ID="HidDistanceMastId" runat="server" />
		                                </div>                               
	                                </div>
                                    <div class="col-sm-5">
	                                	<label class="control-label" id="lblWeight" runat="server">Weight<span class="required-field">*</span></label>
		                                <div>
		                                 <asp:TextBox ID="txtWeight" runat="server" CssClass="form-control"  MaxLength="8"  TabIndex="18" Style="text-align: right;" Text="0.00" onKeyPress="return checkfloat(event, this);" onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox>                                                                                           
                                           <asp:RequiredFieldValidator ID="rfvWeight" runat="server" ErrorMessage="Please enter weight!"
                                            ControlToValidate="txtWeight" Display="Dynamic" SetFocusOnError="true" InitialValue="0.00" CssClass="classValidation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
	                               
                                        </div>                               
	                                </div>
                                    <div class="col-sm-5">
	                                	<label class="control-label" id="lblRateKM" runat="server">Rate[KM]<span class="required-field">*</span></label>
		                                <div>
		                                 <asp:TextBox ID="txtItemRateKM" runat="server" CssClass="form-control"  MaxLength="8"  TabIndex="18" Style="text-align: right;" Text="0.00" onKeyPress="return checkfloat(event, this);" onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox>                                                                                           
                                           <asp:RequiredFieldValidator ID="rfvtxtItemRateKM" runat="server" ErrorMessage="Please enter Rate[KM]!"
                                            ControlToValidate="txtItemRateKM" Display="Dynamic" SetFocusOnError="true" InitialValue="0.00" CssClass="classValidation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>                               
	                                </div>
	                              </div>
	                              <div class="col-sm-3">
	                                <div class="col-sm-6">
                                    <asp:LinkButton ID="lnkbtnSubmit" runat="server" OnClick="lnkbtnSubmit_OnClick" CssClass="btn full_width_btn btn-sm btn-primary subnew" TabIndex="18"  ToolTip="Click to Submit" CausesValidation="true" ValidationGroup="Submit" >Submit</asp:LinkButton>                                   
                                  </div>
                                  <div class="col-sm-6">
                                   <asp:LinkButton ID="lnkbtnNew" runat="server" OnClick="lnkbtnNew_OnClick" CssClass="btn full_width_btn btn-sm btn-primary subnew" TabIndex="19" ToolTip="Click to new" >New</asp:LinkButton>
                                  </div>
	                              </div>
	                            </div>                            
	                          </div>
	                        </section>                        
	                      </div>
	                      
	                      <div class="clearfix third_right">
	                        <section class="panel panel-in-default btns_without_border">                            
	                          <div class="panel-body" style="overflow-x:scroll;">     
                              <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"
                                    Width="100%" GridLines="Both" EnableViewState="true" AllowPaging="true" BorderWidth="0"
                                    ShowFooter="true" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand"
                                    OnRowDataBound="grdMain_RowDataBound" PageSize="30">
                                    <RowStyle CssClass="odd" />
                                    <AlternatingRowStyle CssClass="even" />    
                                    <Columns>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                              <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("id") %>' CommandName="cmdedit"  ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                 <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("id") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete"  ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>                                          
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
                                                <asp:Label ID="lblCityIdno" Visible="false"  runat="server" Text='<%#Eval("ToCity_Idno")%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotRecords" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CityVia" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("CityVia_Name"))%>
                                                <asp:Label ID="lblCityViaIdno" Visible="false"  runat="server" Text='<%#Eval("Cityvia_Idno")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="I.Rate" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemRate" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("Item_Rate"))) ? "" : (Convert.ToString((Eval("Item_Rate")))))%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate2" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                            <ItemStyle Width="70" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemRate2" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("Item_Rate2"))) ? "" : (Convert.ToString((Eval("Item_Rate2")))))%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate3" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                            <ItemStyle Width="70" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemRate3" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("Item_Rate3"))) ? "" : (Convert.ToString((Eval("Item_Rate3")))))%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IR.Type" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemRateType" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("ItemRate_Type"))) ? "" : (Convert.ToString((Eval("ItemRate_Type")))))%>'></asp:Label>
                                                <asp:HiddenField ID="HiItemRateType" runat="server" Value=' <%#Convert.ToString(Eval("ItemRateType_Idno"))%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IW.Rate" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemWghtRate" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("Item_WghtRate"))) ? "" : (Convert.ToString((Eval("Item_WghtRate")))))%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IKM.Rate" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="50px" />
                                            <ItemStyle Width="50" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemKMRate" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("ItemRate_KM"))) ? "" : (Convert.ToString((Eval("ItemRate_KM")))))%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QS.Limit" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                            <ItemStyle Width="70" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblQtyShrtgLimit" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("QtyShrtg_Limit"))) ? "" : (Convert.ToString((Eval("QtyShrtg_Limit")))))%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QS.Rate" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                            <ItemStyle Width="70" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblQtyShrtgRate" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("QtyShrtg_Rate"))) ? "" : (Convert.ToString((Eval("QtyShrtg_Rate")))))%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="WS.Limit" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                            <ItemStyle Width="70" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblWghtShrtgLimit" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("WghtShrtg_Limit"))) ? "" : (Convert.ToString((Eval("WghtShrtg_Limit")))))%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="WS.Rate" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                            <ItemStyle Width="70" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblWghtShrtgRate" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("WghtShrtg_Rate"))) ? "" : (Convert.ToString((Eval("WghtShrtg_Rate")))))%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Distance" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                            <ItemStyle Width="70" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDistn" runat="server" Text='<%#Eval("Dist_Km")%>'></asp:Label>
                                                <asp:HiddenField ID="HiDistanceIdno" runat="server" Value=' <%#Convert.ToString(Eval("DistanceIdno"))%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Cont Size" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                            <ItemStyle Width="70" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblConSize" runat="server" Text='<%#Eval("ConSize")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>      
                                        <asp:TemplateField HeaderText="Cont Weight" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                            <ItemStyle Width="70" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblConWeight" runat="server" Text='<%#Eval("ConWeight")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                                                         

                                        <asp:TemplateField HeaderText="Weight" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                            <ItemStyle Width="70" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblWeight" runat="server" Text='<%#Eval("Item_Weight")%>'></asp:Label>
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
                                     <asp:LinkButton ID="lnkbtnSave" runat="server" CssClass="btn full_width_btn btn-s-md btn-success" TabIndex="20" OnClick="lnkbtnSave_OnClick" CausesValidation="true" ValidationGroup="Save" > <i class="fa fa-save"></i>Save</asp:LinkButton>
	                               <asp:HiddenField ID="hidrateid" runat="server" Value="0" />
                                    <asp:HiddenField ID="Hidrowid" runat="server" Value="" />
                                    <asp:HiddenField ID="HidInvoiceTyp" runat="server" />
                                    <asp:HiddenField ID="dmindate" runat="server" />
                                    <asp:HiddenField ID="hidmindate" runat="server" />
                                        <asp:HiddenField ID="hidmaxdate" runat="server" />
	                                </div>
	                                <div class="col-sm-6">
                                     <asp:LinkButton ID="lnkbtnCancel" runat="server" CssClass="btn full_width_btn btn-s-md btn-danger" TabIndex="21" OnClick="lnkbtnCancel_OnClick" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
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

        function cityviaddl() {
            var id = document.getElementById("<%=ddlCityVia.ClientID %>").value;
            document.getElementById("<%=ddlCity.ClientID %>").value = id;
         
        }
    </script>
</asp:Content>