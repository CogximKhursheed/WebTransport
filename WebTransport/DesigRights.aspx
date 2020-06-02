<%@ Page Title="Designation Rights" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="DesigRights.aspx.cs" Inherits="WebTransport.DesigRights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/U-Custom.css" rel="stylesheet" type="text/css" />
    <link href="css/tables.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="center-block" style="width: 70%; margin-top: 30px; display: block;">
        <section class="panel panel-default full_form_container part_purchase_bill_form auto-height-form" style="box-shadow: 0 0px 2px gray; border: none; margin-top: 30px">
            <!--FORM HEADER-->
            <div class="ibox-title">
                <h5><div class="printing-animation icon-size"></div>Designation Rights  </h5>
            </div>
            <div class="ibox-content">
                <div class="">
                            <div class="col-sm-12 no-pad">
                                <!--FIRST-FOLD-->
                                <div class="col-sm-6 no-pad">
                            <div class="control-label"> 
                                <span>Design. Name</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:DropDownList ID="ddlDesign" runat="server" CssClass="form-control backlight"  OnSelectedIndexChanged="ddlDesign_SelectedIndexChanged"></asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="rfvDesign" runat="server" ControlToValidate="ddlDesign" ValidationGroup="Search" ErrorMessage="Please Select Designation!" CssClass="classValidation" SetFocusOnError="true" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
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
                                <!--SECOND-FOLD-->
                                <div class="col-sm-12 no-pad">
                            <div class="control-holder"> <span class="label-holder">&nbsp;</span></div>
                            <div class="control-holder">
                            <asp:LinkButton ID="lnkbtnPreview" CssClass="btn btn-primary pull-right" runat="server" ValidationGroup="Save"  OnClick="lnkbtnPreview_OnClick">Search</asp:LinkButton>                                     
                            </div>
                    </div>
                            </div>
                        </div>
                <div class="panel-in-default" >
                     <div class="pull-right"></div>
                    <div class="report" style="overflow-x:Auto; width:100%;">
                        <table width="100%">
                                       <tr>
                                       <td>        
		                               <!-- First Row start -->  
                                        <div id="selectall" runat="server" style="padding-left:395px">
                                            Select All&nbsp;&nbsp;
                                            <asp:ImageButton ID="imgBtnSelectAllRows" runat="server" Width="15px" Height="15px" ToolTip="Active/Inactive(All)" onclick="imgBtnSelectAllRows_Click" />
                                            <asp:CheckBox ID="chkSelectAllRows" runat="server" AutoPostBack="true" oncheckedchanged="chkSelectAllRows_CheckedChanged" />     
                                        </div>
                        <asp:GridView ID="grdMain" runat="server" CssClass="table-style-white last-row-select" AutoGenerateColumns="false" BorderStyle="None" Width="100%" GridLines="None" AllowPaging="true" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand" OnRowDataBound="grdMain_RowDataBound">
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
                                                        <asp:ImageButton ID="imgBtnSelectAll" runat="server" Width="15px" Height="15px" CommandArgument='<%#Eval("DesigRghts_Idno")+"_"+Eval("ADD")+"_"+Eval("Edit")+"_"+Eval("View")+"_"+Eval("Delete")+"_"+Eval("Print") %>'
                                                            CommandName="cmdSelectAll" ToolTip="Active/Inactive(All)" />
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                            <asp:HiddenField ID="hiddesigId" runat="server" Value='<%#Eval("DesigId") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Add" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50">
                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgBtnAdd" runat="server" Width="15px" Height="15px" CommandArgument='<%#Eval("DesigRghts_Idno")+"_"+Eval("ADD") %>'
                                                            CommandName="cmdAdd" ToolTip="Active/Inactive" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50">
                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                    <ItemTemplate>
                                                            <asp:ImageButton ID="imgBtnEdit" runat="server" Width="15px" Height="15px" CommandArgument='<%#Eval("DesigRghts_Idno")+"_"+Eval("Edit") %>'
                                                            CommandName="cmdEdit" ToolTip="Active/Inactive" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50">
                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                    <ItemTemplate>
                                                            <asp:ImageButton ID="imgBtnView" runat="server" Width="15px" Height="15px" CommandArgument='<%#Eval("DesigRghts_Idno")+"_"+Eval("View") %>'
                                                            CommandName="cmdView" ToolTip="Active/Inactive" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50">
                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgBtnDelete" runat="server" Width="15px" Height="15px" CommandArgument='<%#Eval("DesigRghts_Idno")+"_"+Eval("Delete") %>'
                                                            CommandName="cmdDelete" ToolTip="Active/Inactive" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Print" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50">
                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                    <ItemTemplate>
                                                            <asp:ImageButton ID="imgBtnPrint" runat="server" Width="15px" Height="15px" CommandArgument='<%#Eval("DesigRghts_Idno")+"_"+Eval("Print") %>'
                                                            CommandName="cmdPrint" ToolTip="Active/Inactive" />
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
                    </div>
                </div>
            </div>
        </section>
    </div>


    <%--         <table border="0" cellpadding="2" cellspacing="0" width="100%">                
                <tr>
                    <td class="white_bg " align="center">
                        <table id="tblNoAuthorize" runat="server" visible="false" class="border1">
                            <tr>
                                <td>
                                    You are not authorize for this
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <table width="750" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
                                        class="ibdr">
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td height="39" align="left" background="images/grd_top_bg.jpg" class="title06">
                                                            &nbsp;&nbsp;&nbsp;Designation Rights
                                                        </td>
                                                       <td height="39" align="right" background="images/grd_top_bg.jpg" class="title06">
                                                       </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                       <tr>
                                            <td>
                                                <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                    <td> &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td >
                                                            <table width="100%"  border="0" cellpadding="0" cellspacing="0" class="ibdr">
                                                            <tr>
                                                                   <td align="left" bgcolor="#E8F2FD" class="btn_01" nowrap="nowrap" valign="middle" style="width:125px;">
                                                                       &nbsp;&nbsp; Designation
                                                                   </td>
                                                                    <td height="35px" align="left" bgcolor="#E8F2FD" class="btn_01" nowrap="nowrap" valign="middle" style="width:225px;">
                                                                    
                                                                    </td>
                                                                    <td height="35px" align="left" bgcolor="#E8F2FD" class="btn_01" nowrap="nowrap" valign="middle" style="width:60px;">
                                                                        Type
                                                                    </td>
                                                                    <td height="35px" align="left" bgcolor="#E8F2FD" class="btn_01" nowrap="nowrap" valign="middle" style="width:170px;">
                                                                     
                                                                    </td>
                                                                    <td height="35px" align="left" bgcolor="#E8F2FD" class="btn_01"nowrap="nowrap" valign="middle" colspan="2">
                                                                     
                                                                    </td>
                                                                  </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="ibdr">
                                                    <tr>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td class="btn_01" nowrap="nowrap" valign="middle" style="width:80px;">
                                                                    <div id="selectall" runat="server" style="padding-left:350px">
                                                                        Select All&nbsp;&nbsp;
                                                                       
                                                                    </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                   
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>--%>


    <script language="javascript" type="text/javascript">
        function mouseOverImage(ctrlname) {
            if (ctrlname == "search") {
                $("#<%=lnkbtnPreview.ClientID %>").attr("src", "images/search_btn.jpg");
            }
        }
        function mouseOutImage(ctrlname) {
            if (ctrlname == "search") {
                $("#<%=lnkbtnPreview.ClientID %>").attr("src", "images/search_img.jpg");
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
