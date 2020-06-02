<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserRights.aspx.cs" Inherits="WebTransport.UserRights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/U-Custom.css" rel="stylesheet" type="text/css" />
    <link href="css/tables.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="center-block" style="width: 70%; margin-top: 30px; display: block;">
        <section class="panel panel-default full_form_container part_purchase_bill_form auto-height-form" style="box-shadow: 0 0px 2px gray; border: none; margin-top: 30px">
            <!--FORM HEADER-->
            <div class="ibox-title">
                <h5><div class="printing-animation icon-size"></div>User Rights
                    <!-- <span class="view_print"><span class="last_print"><a href="#" class="btn btn-sm btn-primary">VIEW</a></span><a href="manage-item-group-list.html">LIST</a><a href="#"><i class="fa fa-print icon"></i></a></span> -->
                </h5>
            </div>
            <div class="ibox-content">
                <div class="">
                            <div class="col-sm-12 no-pad">
                                <!--FIRST-FOLD-->
                                <div class="col-sm-6 no-pad">
                            <div class="control-label"> 
                                <span>User <span class="required-field">*</span> </span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:DropDownList ID="ddlUser" runat="server" CssClass="form-control backlight"></asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="rfvUser" runat="server" ControlToValidate="ddlUser" ValidationGroup="Search" ErrorMessage="Please Select User!" CssClass="classValidation" SetFocusOnError="true" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>                     
                            </div>
                        </div>
                                <div class="col-sm-6 no-pad">
                            <div class="control-label"> 
                                <span>Type</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control backlight">
                                        <asp:ListItem Text="Form" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Menu" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Report" Value="3"></asp:ListItem>
                                    </asp:DropDownList>     
                            </div>
                        </div>
                                <div class="col-sm-6 no-pad">
                            <div class="control-label"> 
                                <span>&nbsp;</span>
                            </div>
                            <div class="control-holder full-width">
                           <div id="selectall"  runat="server">
	                             	<div class="col-sm-4 pull-right">
                                     <label class="control-label">Select All</label>
                                      <asp:ImageButton ID="imgBtnSelectAllRows" runat="server" Width="15px" Height="15px" ToolTip="Active/Inactive(All)" onclick="imgBtnSelectAllRows_Click" />
	                               <asp:CheckBox ID="chkSelectAllRows" AutoPostBack="true"  runat="server" oncheckedchanged="chkSelectAllRows_CheckedChanged"></asp:CheckBox>
	                              </div>
	                            </div>         
                            </div>
                        </div>
                                <!--SECOND-FOLD-->
                                <div class="col-sm-6 no-pad">
                            <div class="control-holder"> <span class="label-holder">&nbsp;</span></div>
                            <div class="control-holder">
                            <asp:LinkButton ID="lnkBtnPreview" CssClass="btn btn-primary pull-right" runat="server" ValidationGroup="Save"  OnClick="lnkBtnPreview_Click">Search</asp:LinkButton>                                     
                            </div>
                    </div>
                            </div>
                        </div>
                <div class="panel-in-default" >
                    <div class="pull-left"></div>
                    <div class="pull-right"></div>
                    <div class="report" style="overflow-x:Auto; width:100%;">
                        <asp:GridView ID="grdMain" runat="server" CssClass="table-style-white last-row-select" AutoGenerateColumns="false" BorderStyle="None" Width="100%" GridLines="None" AllowPaging="true" PageSize="50" OnRowCommand="grdMain_RowCommand" onrowdatabound="grdMain_RowDataBound" onpageindexchanging="grdMain_PageIndexChanging">
                            <Columns>
                                   <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                                                    <ItemTemplate>
                                                                                        <%#Container.DataItemIndex+1 %>.
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Menu" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="150" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblMenu" runat="server" Text='<%#Eval("FormMenu")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Form Name" HeaderStyle-Width="180" HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="180" />
                                                                                    <ItemTemplate>
                                                                                      <asp:Label ID="lblFormName" runat="server" Text='<%#Eval("FormName")%>'></asp:Label>
                                                                                      <asp:HiddenField ID="hidFrmId" runat="server" Value='<%#Eval("Form_Idno") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Menu" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="180">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="180" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblMenuName" runat="server" Text='<%#Eval("FormName")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Report Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="180">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="180" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblRepName" runat="server" Text='<%#Eval("FormName")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Select All" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgBtnSelectAll" runat="server" Width="15px" Height="15px" CommandArgument='<%#Eval("UserRgt_Idno")+"_"+Eval("ADD")+"_"+Eval("Edit")+"_"+Eval("View")+"_"+Eval("Delete")+"_"+Eval("Print") %>'
                                                                                            CommandName="cmdSelectAll" ToolTip="Active/Inactive(All)" />
                                                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                            <asp:HiddenField ID="hiduserId" runat="server" Value='<%#Eval("UserIdno") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Add" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgBtnAdd" runat="server" Width="15px" Height="15px" CommandArgument='<%#Eval("UserRgt_Idno")+"_"+Eval("ADD") %>'
                                                                                            CommandName="cmdAdd" ToolTip="Active/Inactive" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                                                    <ItemTemplate>
                                                                                         <asp:ImageButton ID="imgBtnEdit" runat="server" Width="15px" Height="15px" CommandArgument='<%#Eval("UserRgt_Idno")+"_"+Eval("Edit") %>'
                                                                                            CommandName="cmdEdit" ToolTip="Active/Inactive" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="View" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                                                    <ItemTemplate>
                                                                                         <asp:ImageButton ID="imgBtnView" runat="server" Width="15px" Height="15px" CommandArgument='<%#Eval("UserRgt_Idno")+"_"+Eval("View") %>'
                                                                                            CommandName="cmdView" ToolTip="Active/Inactive" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgBtnDelete" runat="server" Width="15px" Height="15px" CommandArgument='<%#Eval("UserRgt_Idno")+"_"+Eval("Delete") %>'
                                                                                            CommandName="cmdDelete" ToolTip="Active/Inactive" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Print" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                                                    <ItemTemplate>
                                                                                         <asp:ImageButton ID="imgBtnPrint" runat="server" Width="15px" Height="15px" CommandArgument='<%#Eval("UserRgt_Idno")+"_"+Eval("Print") %>'
                                                                                            CommandName="cmdPrint" ToolTip="Active/Inactive" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                            </Columns>
                             <EmptyDataRowStyle HorizontalAlign="Center" />
                                   <EmptyDataTemplate>
                                       <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                   </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </section>
    </div>

    <script language="javascript" type="text/javascript">
        function mouseOverImage(ctrlname) {
            if (ctrlname == "search") {
                $("#<%=lnkBtnPreview.ClientID %>").attr("src", "images/search_btn.jpg");
            }
        }
        function mouseOutImage(ctrlname) {
            if (ctrlname == "search") {
                $("#<%=lnkBtnPreview.ClientID %>").attr("src", "images/search_img.jpg");
            }
        }
    </script>

    <script src="js/U-Custom.js" type="text/javascript"></script>
    <script src="js/U-Custom.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            LoadControls();

            AutoFormHeight();
        });

        function LoadControls() {
            AnimatedIcon();
        }
        function AutoFormHeight() {
            $('.auto-height-form').height(parseInt($('').height()) - parseInt(40))
        }
    </script>

</asp:Content>
