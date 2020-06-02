<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="ManageAmntRcvdInvc.aspx.cs" EnableEventValidation="false" Inherits="WebTransport.ManageAmntRcvdInvc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/U-Custom.css" rel="stylesheet" type="text/css" />
    <link href="css/tables.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="center-block" style="width: 100%; margin-top: 30px; display: block;">
        <section class="panel panel-default full_form_container part_purchase_bill_form auto-height-form" style="box-shadow: 0 0px 2px gray; border: none; margin-top: 30px">
            <!--FORM HEADER-->
            <div class="ibox-title">
                <h5><div class="printing-animation icon-size"></div>Amount Received Invoice List</h5>
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
                        <a href="AmntAgainstInvoice.aspx"><i style="font-size: 13px !important;" class="fa fa-plus action-icon-style1"></i></a>
                    </div>
                </div>
            </div>
            <div class="ibox-content">
                <div class="">
                            <div class="col-sm-12 no-pad">
                                <!--FIRST-FOLD-->
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date Range <span class="required-field">*</span> </span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:DropDownList ID="ddldateRange" runat="server" AutoPostBack="true" CssClass="form-control backlight" OnSelectedIndexChanged="ddldateRange_SelectedIndexChanged" ></asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date From</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtInvcDatefrom" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="50" ></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date To</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtInvcDateto" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="50" ></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Location From</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:DropDownList ID="ddlFromCity" CssClass="form-control backlight" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Invoice No.</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtRcptNo" runat="server" placeholder="Enter Invoice No." CssClass="form-control backlight" MaxLength="50" ></asp:TextBox>
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
                    <div class="pull-left"><asp:Label ID="lblTotalRecord" runat="server" Text=" Total Record(s):0" Style="font-size: 13px; text-transform: none;"></asp:Label></div>
                     <div class="pull-right"></div>
                    <div class="report" style="overflow-x:Auto; width:100%;">
                           <table>
                                       <tr>
                                       <td>  
                        <asp:GridView ID="grdMain" runat="server" CssClass="table-style-white last-row-select" AutoGenerateColumns="false" BorderStyle="None" Width="100%" GridLines="None" AllowPaging="true" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand"  OnRowDataBound="grdMain_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" Width="50" />
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>.
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Invoice No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("Inv_No")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inv. Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Convert.ToDateTime(Eval("Inv_Date")).ToString("dd-MM-yyyy")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="From City" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("FromCity")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sender Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("SenderName")%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Left" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotal" Text="TOTAL" Font-Bold="true" ForeColor="Black" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Convert.ToDouble(Eval("Net_Amnt")).ToString("N2")%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Left" />
                                                <FooterTemplate>
                                                    <b><asp:Label ID="lblNetAmnt" ForeColor="Black" runat="server"></asp:Label></b>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" Width="50" />
                                                <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("Inv_Idno") %>' CommandName="cmdedit" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                <%if (hidDeleteRight.Value.ToLower() == "true")
                                                    { %>
                                                <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("Inv_Idno") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                <%} %>
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
		                                  <tr>
                                          <th rowspan="1" colspan="1" style="width: 250px;"> <asp:Label ID="lblcontant" runat="server"></asp:Label></th>
                                          <th rowspan="1" colspan="1" style="width: 163px;"></th>
                                          <th rowspan="1" colspan="1" style="width: 100px;text-align:left;">Net Total&nbsp;</th>
                                          <th rowspan="1" colspan="1" style="width: 110px;"><asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label></th>
                                          <th rowspan="1" colspan="1" style="width: 63px;"></th></tr>                                  
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
    <%-- <table border="0" cellpadding="2" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <table width="950" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
                                        class="ibdr">
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td height="39" align="left" background="images/grd_top_bg.jpg" class="title06">
                                                            &nbsp;&nbsp;&nbsp;Manage Amount Received Invoice
                                                        </td>
                                                        <td height="39" align="right" background="images/grd_top_bg.jpg" class="title06">
                                                            <a href="AmntAgainstInvoice.aspx">Add &nbsp;&nbsp;&nbsp;</a>
                                                            <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ToolTip="Export to excel"
                                                                ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px" OnClick="imgBtnExcel_Click"
                                                                Visible="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table border="0" align="center" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <table width="950" border="0" cellpadding="0" cellspacing="0" class="ibdr">
                                                                <tr>
                                                                    <td align="left" bgcolor="#E8F2FD" class="style5" height="40px">
                                                                        <span class="txt"><span class="red" style="color: #ff0000">&nbsp;</span> </span>
                                                                        Date Range<span class="redfont1">*</span>
                                                                    </td>
                                                                    <td align="left" bgcolor="#E8F2FD" height="40px" class="style9" nowrap="nowrap" valign="middle">
                                                                        <asp:DropDownList ID="ddldateRange" runat="server" AutoPostBack="true" CssClass="input_type1"
                                                                            Height="30px" TabIndex="1" Width="200px" OnSelectedIndexChanged="ddldateRange_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td align="left" bgcolor="#E8F2FD" class="style4" height="30px">
                                                                        &nbsp; Date From
                                                                    </td>
                                                                    <td height="35px" align="left" bgcolor="#E8F2FD" class="style6" align="center" nowrap="nowrap"
                                                                        valign="middle">
                                                                        <asp:TextBox ID="txtInvcDatefrom" runat="server" CssClass="glow" MaxLength="50" TabIndex="2"
                                                                            Width="100px"></asp:TextBox>
                                                                    </td>
                                                                    <td height="35px" align="left" bgcolor="#E8F2FD" class="style5" nowrap="nowrap" valign="middle">
                                                                        &nbsp;&nbsp;Date To
                                                                    </td>
                                                                    <td height="35px" align="left" bgcolor="#E8F2FD" class="style9" nowrap="nowrap" valign="middle">
                                                                        <asp:TextBox ID="txtInvcDateto" runat="server" CssClass="glow" MaxLength="50" TabIndex="3"
                                                                            Width="100px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" bgcolor="#F5FAFF" class="style4" nowrap="nowrap" valign="middle">
                                                                        &nbsp; Invoice No
                                                                    </td>
                                                                    <td height="35px" align="left" bgcolor="#F5FAFF" class="style6" nowrap="nowrap" valign="middle">
                                                                        <asp:TextBox ID="txtRcptNo" runat="server" CssClass="glow" MaxLength="50" TabIndex="4"
                                                                            Width="150px"></asp:TextBox>
                                                                    </td>
                                                                    <td height="35px" align="left" bgcolor="#F5FAFF" class="btn_01" nowrap="nowrap" style="width: 180px;"
                                                                        valign="middle">
                                                                        <asp:Label ID="lblTotalRecord" runat="server" Text=" Total Record (s): 0" Style="font-size: 15px;
                                                                            font-weight: bold;"></asp:Label>
                                                                    </td>
                                                                    <td height="35px" align="left" bgcolor="#F5FAFF" colspan="4" class="btn_01" nowrap="nowrap"
                                                                        style="width: 180px;" valign="middle">
                                                                        <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/search_img.jpg"
                                                                            onmouseover="mouseOverImage('search')" onmouseout="mouseOutImage('search')" OnClick="btnSearch_Click"
                                                                            TabIndex="5" />
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
                                                                            ShowFooter="true" Width="100%" GridLines="None" AllowPaging="true" PageSize="25"
                                                                            OnPageIndexChanging="grdMain_PageIndexChanging" BorderWidth="0" AlternatingRowStyle-CssClass="bgcolor2"
                                                                            OnRowCommand="grdMain_RowCommand" TabIndex="4" RowStyle-CssClass="gridAlternateRow"
                                                                            CssClass="ibdr gridBackground internal_heading" HeaderStyle-CssClass="internal_heading"
                                                                            OnRowDataBound="grdMain_RowDataBound" OnRowCreated="grdMain_RowCreated">
                                                                            <HeaderStyle CssClass="linearBg" ForeColor="Black" />
                                                                            <AlternatingRowStyle CssClass="bgcolor2" />
                                                                            <FooterStyle ForeColor="Black" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                                                    <ItemTemplate>
                                                                                        <%#Container.DataItemIndex+1 %>.
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Invoice No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("Inv_No")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Inv. Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <%#Convert.ToDateTime(Eval("Inv_Date")).ToString("dd-MM-yyyy")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="From City" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("FromCity")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Sender Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("SenderName")%>
                                                                                    </ItemTemplate>
                                                                                    <FooterStyle HorizontalAlign="Left" />
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblTotal" Text="TOTAL" Font-Bold="true" ForeColor="Black" runat="server"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Net Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <%#Convert.ToDouble(Eval("Net_Amnt")).ToString("N2")%>
                                                                                    </ItemTemplate>
                                                                                    <FooterStyle HorizontalAlign="Left" />
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblNetAmnt" ForeColor="Black" runat="server"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/delete_sm.png"
                                                                                            OnClientClick="return confirm('Do you want to delete this record ?');" CommandArgument='<%#Eval("Inv_Idno") %>'
                                                                                            CommandName="cmddelete" ToolTip="Delete" />
                                                                                        <asp:ImageButton ID="imgBtnEdit" runat="server" ImageUrl="~/Images/edit_sm.png" CommandArgument='<%#Eval("Inv_Idno") %>'
                                                                                            CommandName="cmdedit" ToolTip="Edit" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                            <EmptyDataTemplate>
                                                                                <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                                                            </EmptyDataTemplate>
                                                                            <PagerStyle CssClass="white_bg" ForeColor="#000" HorizontalAlign="Center" />
                                                                            <RowStyle CssClass="bgcolrwhite" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td id="trprint" runat="server" visible="false">
                                                                        <asp:GridView ID="grdprint" runat="server" AutoGenerateColumns="false" BorderStyle="None"
                                                                            Width="100%" GridLines="None" AllowPaging="false" PageSize="25" HeaderStyle-CssClass="internal_heading"
                                                                            BorderWidth="0" RowStyle-CssClass="bgcolrwhite" AlternatingRowStyle-CssClass="bgcolor2">
                                                                            <AlternatingRowStyle CssClass="bgcolor2" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                                                    <ItemTemplate>
                                                                                        <%#Container.DataItemIndex+1 %>.
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Invoice No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("Inv_No")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <%#Convert.ToDateTime(Eval("Inv_Date")).ToString("dd-MM-yyyy")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                            <EmptyDataTemplate>
                                                                                <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                                                            </EmptyDataTemplate>
                                                                            <HeaderStyle CssClass="internal_heading" />
                                                                            <PagerStyle CssClass="white_bg" ForeColor="#000" HorizontalAlign="Center" />
                                                                            <RowStyle CssClass="bgcolrwhite" />
                                                                        </asp:GridView>
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
    <%--hidden fields--%>
    <asp:HiddenField ID="hidDeleteRight" runat="server" />
    <asp:HiddenField ID="hidrcptheadidno" runat="server" />
    <asp:HiddenField ID="hidmindate" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />


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

            $("#<%=txtInvcDatefrom.ClientID %>").datepicker({
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate,
                changeMonth: true,
                changeYear: true,
                showOn: 'button',
                buttonImage: '../images/calendar.gif'
            });
            $("#<%=txtInvcDateto.ClientID %>").datepicker({
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate,
                changeMonth: true,
                changeYear: true,
                showOn: 'button',
                buttonImage: '../images/calendar.gif'
            });
        }


    </script>
    <style>
        .linearBg {
            /* fallback */
            background: rgb(178,225,255); /* Old browsers */ /* IE9 SVG, needs conditional override of 'filter' to 'none' */
            background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iI2IyZTFmZiIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiM2NmI2ZmMiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
            background: -moz-linear-gradient(top, rgba(178,225,255,1) 0%, rgba(102,182,252,1) 100%); /* FF3.6+ */
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(178,225,255,1)), color-stop(100%,rgba(102,182,252,1))); /* Chrome,Safari4+ */
            background: -webkit-linear-gradient(top, rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* Chrome10+,Safari5.1+ */
            background: -o-linear-gradient(top, rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* Opera 11.10+ */
            background: -ms-linear-gradient(top, rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* IE10+ */
            background: linear-gradient(to bottom, rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* W3C */
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#b2e1ff', endColorstr='#66b6fc',GradientType=0 ); /* IE6-8 */
        }

        .style1 {
            font-family: Calibri;
            font-size: 15px;
            font-weight: normal;
            color: #333333;
            text-decoration: none;
            line-height: 22px;
            text-transform: capitalize;
            width: 171px;
            border-bottom-style: none;
            margin-left: 80px;
        }

        .style14 {
            font-family: Calibri;
            font-size: 15px;
            font-weight: normal;
            color: #333333;
            text-decoration: none;
            line-height: 22px;
            text-transform: capitalize;
            width: 183px;
            border-bottom-style: none;
            margin-left: 80px;
        }

        .style18 {
            font-family: Calibri;
            font-size: 15px;
            font-weight: normal;
            color: #333333;
            text-decoration: none;
            line-height: 22px;
            text-transform: capitalize;
            width: 768px;
            border-bottom-style: none;
            margin-left: 80px;
        }

        .style19 {
            font-family: Calibri;
            font-size: 15px;
            font-weight: normal;
            color: #333333;
            text-decoration: none;
            line-height: 22px;
            text-transform: capitalize;
            width: 559px;
            border-bottom-style: none;
            margin-left: 80px;
        }

        .style22 {
            font-family: Calibri;
            font-size: 15px;
            font-weight: normal;
            color: #333333;
            text-decoration: none;
            line-height: 22px;
            text-transform: capitalize;
            width: 8%;
            border-bottom-style: none;
            margin-left: 80px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
    <script>
        $(".datepicker").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
            minDate: '<%=hidmindate.Value%>',
            maxDate: '<%=hidmaxdate.Value%>'
        });
    </script>
</asp:Content>
