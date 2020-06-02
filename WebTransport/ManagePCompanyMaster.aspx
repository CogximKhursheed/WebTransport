<%@ Page Title="Manage Petrol Company Master" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ManagePCompanyMaster.aspx.cs" Inherits="WebTransport.ManagePCompanyMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/U-Custom.css" rel="stylesheet" type="text/css" />
    <link href="css/tables.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="center-block" style="width: 50%; margin-top: 30px; display: block;">
        <section class="panel panel-default full_form_container part_purchase_bill_form auto-height-form" style="box-shadow: 0 0px 2px gray; border: none; margin-top: 30px">
             <!--FORM HEADER-->
            <div class="ibox-title">
                <h5><div class="printing-animation icon-size"></div>Petrol Company Master List </h5>
                <div class="pull-right title-action">
                    <div class="action-center">
		                <!--DOWNLOAD OPTIONS-->
                        <div class="fa fa-download"></div>
                        <div class="download-option-box">
                            <div class="download-option-container">
                                <ul>
                                    <li class="download-excel" data-name="Download excel"><asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel"  ToolTip="Export to excel" ImageUrl="~/images/Excel_Img.JPG" style="height: 100%" onclick="imgBtnExcel_Click" Visible="false"/></li>
                                </ul>
                            </div>
                            <div class="close-download-box" title="Close download window"></div>
                        </div>
                        <a href="PetrolCompanyMaster.aspx"><i style="font-size: 13px !important;" class="fa fa-plus action-icon-style1"></i></a>
                    </div>
                </div>
            </div>
            <div class="ibox-content">
                <div class="">
                            <div class="col-sm-12 no-pad">
                                <!--FIRST-FOLD-->
                                <div class="col-sm-6 no-pad">
                            <div class="control-label"> 
                                <span>Petrol Company Name</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtPetrolCompanyName" placeholder="Enter Company Name" runat="server" CssClass="form-control backlight" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                                <!--SECOND-FOLD-->
                                <div class="col-sm-6 no-pad">
                            <div class="control-holder"> <span class="label-holder">&nbsp;</span></div>
                            <div class="control-holder">
                            <asp:LinkButton ID="lnkbtnPreview" CssClass="btn btn-primary pull-right" runat="server" ValidationGroup="Save"  OnClick="lnkbtnPreview_OnClick">Search</asp:LinkButton>                                     
                            </div>
                    </div>
                            </div>
                        </div>
                <div class="panel-in-default" >
                    <div class="pull-left"><asp:Label ID="lblTotalRecord" runat="server" Text=" Total Record(s):0" Style="font-size: 13px; text-transform: none;"></asp:Label></div>
                    <div class="pull-right"></div>
                    <div class="report" style="overflow-x:Auto; width:100%;">
                         <table width="100%">
                                       <tr>
                                       <td> 
                        <asp:GridView ID="grdMain" runat="server" CssClass="table-style-white last-row-select" AutoGenerateColumns="false" BorderStyle="None" Width="100%" GridLines="None" AllowPaging="true" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand" OnRowDataBound="grdMain_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="50" HeaderStyle-CssClass="gridHeaderAlignCenter">
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" Width="50" />
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>.
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="300">
                                                <HeaderStyle HorizontalAlign="Left" Width="300px" />
                                                <ItemStyle HorizontalAlign="Left" Width="300" />
                                                <ItemTemplate>
                                                    <%#Eval("PCompName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active" HeaderStyle-CssClass="gridHeaderAlignCenter" HeaderStyle-Width="30">
                                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgBtnStatus" runat="server" Width="15px" Height="15px" CommandArgument='<%#Eval("PCompIdno")+"_"+Eval("Status") %>'
                                                        CommandName="cmdstatus" ToolTip="Active/Inactive" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" >
                                               <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                 <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("PCompIdno") %>' CommandName="cmdedit" TabIndex="5" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                          
                                                 <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("PCompIdno") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" TabIndex="6" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
    <%--hidden fields--%>
    <asp:HiddenField ID="hidcityidno" runat="server" />

    <%--<table border="0" cellpadding="2" cellspacing="0" width="100%">
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
                                    <table width="700" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
                                        class="ibdr">
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td height="39" align="left" background="images/grd_top_bg.jpg" class="title06">
                                                            &nbsp;&nbsp;&nbsp;Manage Petrol Company Master
                                                        </td>
                                                       <td height="39" align="right" background="images/grd_top_bg.jpg" class="title06">
                                                            <a href="PetrolCompanyMaster.aspx">Add Petrol Company&nbsp;&nbsp;&nbsp;</a>
                                                            <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel"  ToolTip="Export to excel" ImageUrl="~/Images/Excel_Img.JPG" 
                                                                style="height: 16px" onclick="imgBtnExcel_Click"  Visible ="false"/>
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
                                                                    <td height="35px" align="left" bgcolor="#E8F2FD" class="btn_01" nowrap="nowrap" valign="middle">
                                                                       &nbsp;&nbsp; Petrol Company Name 
                                                                    </td>
                                                                    <td height="35px" align="left" bgcolor="#E8F2FD" class="btn_01" nowrap="nowrap"  style="width:180px;" valign="middle">
                                                                        <asp:TextBox ID="txtPetrolCompanyName" runat="server" CssClass="glow" MaxLength="50" TabIndex="2" Width="250px"></asp:TextBox>
                                                                    </td>
                                                                    <td height="35px" align="left" bgcolor="#E8F2FD" class="btn_01"nowrap="nowrap" valign="middle" >
                                                                        &nbsp; &nbsp;<asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/search_img.jpg" 
                                                                                    onmouseover="mouseOverImage('search')" onmouseout="mouseOutImage('search')" OnClick="btnSearch_Click"
                                                                                    TabIndex="3" />
                                                                    </td>
                                                                   <td height="35px" colspan="3" align="left" bgcolor="#E8F2FD" class="btn_01" nowrap="nowrap" valign="middle">
                                                                        <asp:Label ID="lblTotalRecord" runat="server" Text=" Total Record (s): 0" Style="font-size: 15px;
                                                                            font-weight: bold;"></asp:Label>
                                                                    </td>
                                                                     <td height="35px" align="left" bgcolor="#E8F2FD" class="btn_01" nowrap="nowrap" valign="middle">
                                                                    &nbsp;
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
                                                                    <td>
                                                                        <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None"
                                                                            Width="100%" GridLines="None" AllowPaging="true" PageSize="35" OnPageIndexChanging="grdMain_PageIndexChanging"
                                                                            HeaderStyle-CssClass="internal_heading" BorderWidth="0" RowStyle-CssClass="bgcolrwhite"
                                                                            AlternatingRowStyle-CssClass="bgcolor2" OnRowCommand="grdMain_RowCommand" 
                                                                            TabIndex="4" onrowdatabound="grdMain_RowDataBound" >
                                                                          <HeaderStyle ForeColor ="Black"  CssClass="linearBg" />
                                                                            <AlternatingRowStyle CssClass="bgcolor2" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                                                    <ItemTemplate>
                                                                                        <%#Container.DataItemIndex+1 %>.
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Company Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="150" />
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("PCompName")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="Active" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgBtnStatus" runat="server" Width="15px" Height="15px" CommandArgument='<%#Eval("PCompIdno")+"_"+Eval("Status") %>'
                                                                                            CommandName="cmdstatus" ToolTip="Active/Inactive" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                                                    <ItemTemplate>
                                                                                        
                                                                                        <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/delete_sm.png"
                                                                                            OnClientClick="return confirm('Do you want to delete this record ?');" CommandArgument='<%#Eval("PCompIdno") %>'
                                                                                            CommandName="cmddelete" ToolTip="Delete" />
                                                                                            <asp:ImageButton ID="imgBtnEdit" runat="server" ImageUrl="~/Images/edit_sm.png" CommandArgument='<%#Eval("PCompIdno") %>'
                                                                                            CommandName="cmdedit" ToolTip="Edit" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                            <EmptyDataTemplate>
                                                                                <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                                                            </EmptyDataTemplate>
                                                                          
                                                                            <PagerStyle CssClass="white_bg" ForeColor="#000" HorizontalAlign="Center" />
                                                                            
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
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
