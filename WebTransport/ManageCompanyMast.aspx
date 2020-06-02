<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ManageCompanyMast.aspx.cs" Inherits="WebTransport.ManageCompanyMast" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<style>
        .linearBg
        {
            /* fallback */
            background: rgb(178,225,255); /* Old browsers */ /* IE9 SVG, needs conditional override of 'filter' to 'none' */
            background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iI2IyZTFmZiIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiM2NmI2ZmMiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
            background: -moz-linear-gradient(top,  rgba(178,225,255,1) 0%, rgba(102,182,252,1) 100%); /* FF3.6+ */
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(178,225,255,1)), color-stop(100%,rgba(102,182,252,1))); /* Chrome,Safari4+ */
            background: -webkit-linear-gradient(top,  rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* Chrome10+,Safari5.1+ */
            background: -o-linear-gradient(top,  rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* Opera 11.10+ */
            background: -ms-linear-gradient(top,  rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* IE10+ */
            background: linear-gradient(to bottom,  rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* W3C */
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#b2e1ff', endColorstr='#66b6fc',GradientType=0 ); /* IE6-8 */
        }
    </style>

    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <contenttemplate>
            <table border="0" cellpadding="2" cellspacing="0" width="100%">
                <tr>
                    <td align="left" valign="top" class="header_bt_bg">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <%--            <tr>
                                <td width="29">
                                    &nbsp;
                                </td>
                                <td align="left" valign="bottom">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="left" valign="top" style="padding-top: 3px;">
                                                <!-- Breadcrumb -->
                                                <table border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td width="5">
                                                            &nbsp;
                                                        </td>
                                                        <td class="orange12">
                                                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                                            <span><b><a href="Menus.aspx"><span class="orange12">Home</span> </a><span>
                                                                <img src='images/black_arrow.gif' alt="" /></span> <span class="orange12">Master</span>
                                                                <span>
                                                                    <img src='images/black_arrow.gif' alt="" /></span>
                                                                <asp:Label ID="lblbreadcrum" runat="server" Text="Manage Company Master"></asp:Label>
                                                            </b></span>
                                                        </td>
                                                        <td class="gray11">
                                                        </td>
                                                    </tr>
                                                </table>
                                                <!-- Breadcrumb -->
                                            </td>
                                            <td align="left" valign="top">
                                                &nbsp;
                                            </td>
                                            <td align="right" valign="top" style="padding-top: 1px;">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="27">
                                    &nbsp;
                                </td>
                            </tr>--%>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
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
                                                            &nbsp;&nbsp;&nbsp;Manage Company Master
                                                        </td>
                                                       <td height="39" align="right" background="images/grd_top_bg.jpg" class="title06">
                                                            <a href="CompanyMast.aspx">Add Company&nbsp;&nbsp;&nbsp;</a>
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
                                                                   <td align="left" bgcolor="#E8F2FD" class="btn_01" nowrap="nowrap" valign="middle">
                                                                       &nbsp; Company Name&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    </td>
                                                                    <td height="35px" align="left" bgcolor="#E8F2FD" class="btn_01" nowrap="nowrap"  style="width:180px;" valign="middle">
                                                                            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="glow" MaxLength="50" 
                                                                                TabIndex="2" Width="150px"></asp:TextBox>
                                                                    </td>
                                                                    <td height="35px" align="left" bgcolor="#E8F2FD" class="btn_01" nowrap="nowrap" valign="middle">
                                                                        &nbsp;</td>
                                                                    <td height="35px" align="left" bgcolor="#E8F2FD" class="btn_01" nowrap="nowrap"  style="width:180px;" valign="middle">
                                                                        &nbsp;</td>
                                                                    <td height="35px" align="left" bgcolor="#E8F2FD" class="btn_01"nowrap="nowrap" valign="middle" >
                                                                        <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/search_img.jpg" 
                                                                                    onmouseover="mouseOverImage('search')" onmouseout="mouseOutImage('search')" OnClick="btnSearch_Click"
                                                                                    TabIndex="3" />
                                                                    </td>
                                                                   <td height="35px" align="left" bgcolor="#E8F2FD" class="btn_01" nowrap="nowrap" valign="middle">
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
                                                                            Width="100%" GridLines="None" AllowPaging="true" PageSize="25" OnPageIndexChanging="grdMain_PageIndexChanging"
                                                                            HeaderStyle-CssClass="internal_heading" BorderWidth="0" RowStyle-CssClass="bgcolrwhite"
                                                                            AlternatingRowStyle-CssClass="bgcolor2" OnRowCommand="grdMain_RowCommand" 
                                                                            TabIndex="4" onrowdatabound="grdMain_RowDataBound" >
                                                                            <AlternatingRowStyle CssClass="bgcolor2" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                                                    <ItemTemplate>
                                                                                        <%#Container.DataItemIndex+1 %>.
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="Company_Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("CompName")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="Mobile" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("MobileNo")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="State Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="150" />
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("StateName")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="CityName" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="150" />
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("CityName")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="Serv.Tax No." HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("Tin_No")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="PAN No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("PAN_No")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgBtnStatus" runat="server" Width="15px" Height="15px"  CommandArgument='<%#Eval("CompIdno")+"_"+Eval("Status") %>'
                                                                                            CommandName="cmdstatus" ToolTip="Active/Inactive" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgBtnEdit" runat="server" ImageUrl="~/Images/edit_sm.png" CommandArgument='<%#Eval("CompIdno") %>'
                                                                                            CommandName="cmdedit" ToolTip="Edit" />
                                                                                        <%--<asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/delete_sm.png"
                                                                                            OnClientClick="return confirm('Do you want to delete this record ?');" CommandArgument='<%#Eval("CityIdno") %>'
                                                                                            CommandName="cmddelete" ToolTip="Delete" />--%>
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
                        </table>
        </contenttemplate>
            <asp:HiddenField ID="hidcityidno" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">
        function mouseOverImage(ctrlname) {
            if (ctrlname == "search") {
                $("#<%=btnSearch.ClientID %>").attr("src", "images/search_btn.jpg");
            }
        }
        function mouseOutImage(ctrlname) {
            if (ctrlname == "search") {
                $("#<%=btnSearch.ClientID %>").attr("src", "images/search_img.jpg");
            }
        }
    </script>
</asp:Content>
