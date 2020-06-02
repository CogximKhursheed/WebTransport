<%@ Page Title="Material Issue" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="TyreIssue.aspx.cs" Inherits="WebTransport.TyreIssue" %>

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
    <table width="100%">
        <tr>
            <td class="white_bg " align="center">
                <table id="tblNoAuthorize" runat="server" visible="false" class="border1">
                    <tr>
                        <td>
                            You are not authorize for this
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" id="tblAuthorize"
                    runat="server">
                    <tr>
                        <td>
                            <table width="900px" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
                                class="ibdr">
                                <tr>
                                    <td height="39" align="left" background="images/grd_top_bg.jpg" class="title06">
                                        &nbsp;&nbsp;&nbsp;
                                        <div style="float: left; width: 35%; text-align: left;">
                                            &nbsp;&nbsp;Tyre Issue
                                        </div>
                                        <div style="float: left; width: 22%; text-align: left;">
                                        </div>
                                        <div style="float: right; width: 35%; text-align: right;">
                                            <a href="ManageTyreIssue.aspx">
                                                <asp:Label ID="lblViewList" runat="server" Text="View List&nbsp;&nbsp; " TabIndex="18"></asp:Label></a>
                                            <asp:ImageButton ID="imgPDF" runat="server" AlternateText="PDF" ImageUrl="~/images/pdf.jpeg"
                                                Visible="false" title="PDF" TabIndex="48" Height="16px" />&nbsp;
                                            <asp:ImageButton ID="imgPrint" runat="server" AlternateText="Print" OnClientClick="return CallPrint('print');"
                                                ImageUrl="~/images/print.jpeg" Visible="false" TabIndex="49" title="Print" Height="16px" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td height="15" align="left" valign="bottom" class="txt01">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="ibdr">
                                                        <tr>
                                                            <td align="center" valign="top">
                                                                <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" bgcolor="#CCCCCC"
                                                                    class="ibdr1">
                                                                   <tr>
                                                                   <td style="width:70%">
                                                                   <table width="100%" align="left">
                                                                    <tr>
                                                                       <td align="left" bgcolor="#E8F2FD" class="btn_01" width="90px">
                                                                            &nbsp;Date<span class="red12"> *</span>
                                                                        </td>
                                                                        <td align="left" bgcolor="#E8F2FD" class="btn_01" width="210px">
                                                                            <asp:TextBox ID="txtGRDate" runat="server" CssClass="glow" MaxLength="6" Width="85px"
                                                                                TabIndex="1" onchange="Focus()" OnTextChanged="txtGRDate_TextChanged"></asp:TextBox>
                                                                            &nbsp;
                                                                            <asp:TextBox ID="txtGRNo" runat="server" CssClass="glow" Style="text-align: right;"
                                                                                Width="50px" Enabled="true" AutoPostBack="true" TabIndex="3" MaxLength="9"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvtxtGRDate" runat="server" Display="Dynamic" ControlToValidate="txtGRDate"
                                                                                ValidationGroup="save" ErrorMessage="<br />Enter Date" InitialValue="0" SetFocusOnError="true"
                                                                                CssClass="redtext_11px"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td bgcolor="#E8F2FD" class="btn_01" width="90px" height="30px">
                                                                            &nbsp;Date&nbsp;Range
                                                                        </td>
                                                                        <td align="left" bgcolor="#E8F2FD" class="btn_01" width="140px" height="30px">
                                                                            <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="glow" Width="210px"
                                                                                AutoPostBack="True" TabIndex="2">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                     
                                                                     
                                                                    </tr>
                                                                    <tr><td height="1" colspan="6" bgcolor="#C9C9C9"></td></tr>
                                                                    <tr>
                                                                        <td align="left" valign="top" bgcolor="#E8F2FD" class="btn_01">
                                                                            &nbsp;Truck No.
                                                                        </td>
                                                                        <td align="left" bgcolor="#E8F2FD" class="btn_01" valign="top" height="30px">
                                                                            <asp:DropDownList ID="ddlTruckNo" runat="server" CssClass="glow" Width="185px" TabIndex="4"
                                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlTruckNo_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                            <asp:ImageButton ID="ImgBtnTruuckRefresh" runat="server" ImageUrl="~/images/RefreshNew.jpg"
                                                                                Visible="true" AlternateText="Add" ToolTip="Update Truck No." vertical-align="bottom"
                                                                                align="middle" OnClick="ImgBtnTruuckRefresh_Click" />
                                                                        </td>
                                                                        <td align="left" bgcolor="#E8F2FD" class="btn_01" valign="top">
                                                                            &nbsp;Driver<span class="red12"> *</span>
                                                                        </td>
                                                                        <td align="left" valign="top" bgcolor="#E8F2FD" class="btn_01" height="30px">
                                                                            <asp:DropDownList ID="ddlDriver" runat="server" CssClass="glow" TabIndex="5" 
                                                                                Width="187px">
                                                                            </asp:DropDownList>
                                                                            <br />
                                                                            <asp:RequiredFieldValidator ID="rfvDriver" runat="server" Display="Dynamic" ControlToValidate="ddlDriver"
                                                                                ValidationGroup="save" ErrorMessage="Please Select Driver " InitialValue="0"
                                                                                SetFocusOnError="true" CssClass="redtext_11px"></asp:RequiredFieldValidator>
                                                                            <%-- <asp:RequiredFieldValidator ID="rfvddlToCity" runat="server" Display="Dynamic" ControlToValidate="ddlReciver"
                                                                                ValidationGroup="save" ErrorMessage="Please Select To " InitialValue="0"
                                                                                SetFocusOnError="true" CssClass="redtext_11px"></asp:RequiredFieldValidator>--%>
                                                                           
                                                                           
                                                                            
                                                                        </td>
                                                                      
                                                                    </tr>
                                                                    <tr><td height="1" colspan="6" bgcolor="#C9C9C9"></td></tr>
                                                                    <tr>
                                                                        <td align="left" valign="top" bgcolor="#E8F2FD" class="btn_01">
                                                                           Location<span class="red12"> *</span>
                                                                        </td>
                                                                        <td align="left" bgcolor="#E8F2FD" class="btn_01" valign="top" height="30px">
                                                                           <asp:DropDownList ID="ddlLocation" runat="server" CssClass="glow" TabIndex="6" Width="187px"
                                                                                OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true">
                                                                            </asp:DropDownList>
                                                                            <asp:ImageButton ID="ImgBtnLocation" runat="server" ImageUrl="~/images/RefreshNew.jpg"
                                                                                Visible="true" AlternateText="Add" ToolTip="Update From Location" vertical-align="bottom"
                                                                                align="middle" OnClick="ImgBtnLocation_Click" />
                                                                            <br />
                                                                            <asp:RequiredFieldValidator ID="rfvtxtfromcity" runat="server" Display="Dynamic"
                                                                                ControlToValidate="ddlLocation" ValidationGroup="save" ErrorMessage="Please select From Location"
                                                                                InitialValue="0" SetFocusOnError="true" CssClass="redtext_11px"></asp:RequiredFieldValidator>
                                                                       
                                                                        </td>
                                                                        <td align="left" bgcolor="#E8F2FD" class="btn_01" valign="top">
                                                                               &nbsp;Issuing To<%--<span class="red12"> *</span>--%>
                                                                           
                                                                        </td>
                                                                        <td align="left" valign="top" bgcolor="#E8F2FD" class="btn_01" height="30px">
                                                                          <asp:DropDownList ID="ddlReciver" Enabled="false" runat="server" CssClass="glow"
                                                                                TabIndex="7" Width="187px">
                                                                            </asp:DropDownList>
                                                                            <%--<asp:ImageButton ID="ImgBtnIssueTo" runat="server" ImageUrl="~/images/RefreshNew.jpg"
                                                                                Visible="true" AlternateText="Add" ToolTip="Update To Issuing" vertical-align="bottom"
                                                                                align="middle" OnClick="ImgBtnIssueTo_Click" />--%>
                                                                            <br />
                                                                            <%-- <asp:RequiredFieldValidator ID="rfvddlToCity" runat="server" Display="Dynamic" ControlToValidate="ddlReciver"
                                                                                ValidationGroup="save" ErrorMessage="Please Select To " InitialValue="0"
                                                                                SetFocusOnError="true" CssClass="redtext_11px"></asp:RequiredFieldValidator>--%>
                                                                        </td>
                                                                      
                                                                    </tr>
                                                                    <tr><td height="1" colspan="6" bgcolor="#C9C9C9"></td></tr>
                                                                    <tr>
                                                                    <td  colspan="6" bgcolor="#E8F2FD" class="btn_01">
                                                                                &nbsp;Remark&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtRemarkhead" runat="server" autocomplete="off" CssClass="glow"
                                                                                    Width="490px" MaxLength="30" placeholder="Enter Remark" TabIndex="8"></asp:TextBox>
                                                                            </td>
                                                                 
                                                                          
                                                                  
                                                                  </tr>
                                                                   </table>
                                                                  </td>
                                                                  <td width="30%" align="left" valign="top">
                                                                  <asp:GridView ID="grdPrvIssue"  runat="server" AutoGenerateColumns="false" BorderStyle="None"
                                                                                Width="100%" GridLines="Both" AllowPaging="True" HeaderStyle-CssClass="gridRow"
                                                                                BorderWidth="0" RowStyle-CssClass="gridAlternateRow"  PageSize="4" AlternatingRowStyle-CssClass="gridRow"
                                                                          onpageindexchanging="grdPrvIssue_PageIndexChanging">
                                                                                <HeaderStyle CssClass="linearBg" ForeColor="Black" />
                                                                                <Columns>
                                                                              <asp:TemplateField HeaderText="Date" HeaderStyle-Width="30%" HeaderStyle-HorizontalAlign="Center">
                                                                                        <ItemStyle Width="30%" HorizontalAlign="Center" />
                                                                                        <ItemTemplate>
                                                                                            <%# string.Format("{0:dd/MM/yyyy}",Eval("Date"))%>  
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Item" HeaderStyle-Width="70%" HeaderStyle-HorizontalAlign="Center">
                                                                                        <ItemStyle Width="70%" HorizontalAlign="Center" />
                                                                                        <ItemTemplate>
                                                                                             <%#Eval("Item")%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                                <EmptyDataTemplate>
                                                                                    <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                                                                </EmptyDataTemplate>
                                                                                <PagerStyle CssClass="white_bg" HorizontalAlign="Center" />
                                                                            </asp:GridView>
                                                                  </td>
                                                                    </tr>
                                                                    <tr><td height="1" colspan="7" bgcolor="#C9C9C9"> </td> </tr>
                                                                    <tr>
                                                                        <td colspan="7" rowspan="" bgcolor="#d1efee" class="btn_01">
                                                                            <table width="100%" align="center">
                                                                                <tr>
                                                                                    <td align="left" width="250px">
                                                                                        &nbsp;Item Name<span class="red12"> *</span>
                                                                                    </td>
                                                                                    <td align="left" width="100px">
                                                                                        &nbsp;Serial No<span class="red12">*</span>
                                                                                    </td>
                                                                                    <td align="left" width="100px">
                                                                                        &nbsp;Weight<span class="red12">*</span>
                                                                                    </td>
                                                                                    <td align="left" width="100px">
                                                                                        &nbsp;Rate<span class="red12">*</span>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" valign="top">
                                                                                        <asp:DropDownList ID="ddlItemName" runat="server" CssClass="glow" Width="250px" TabIndex="9"
                                                                                            AutoPostBack="true" 
                                                                                            onselectedindexchanged="ddlItemName_SelectedIndexChanged">
                                                                                        </asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator ID="rfvPartno" runat="server" ControlToValidate="ddlItemName"
                                                                                            Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"
                                                                                            ErrorMessage="<br>Select Item Name" CssClass="redtext_11px"></asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                    <td align="left" valign="top">
                                                                                       <%-- <asp:TextBox ID="txtQuantity" runat="server" CssClass="glow" Width="100px" MaxLength="6"
                                                                                            AutoComplete="off" Style="text-align: right;" TabIndex="10" AutoPostBack="false"
                                                                                            onKeyPress="return checkfloat(event, this);" oncopy="return false" onpaste="return false"
                                                                                            oncut="return false" oncontextmenu="return false">1</asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="rfvtxtQuantity" runat="server" ControlToValidate="txtQuantity"
                                                                                            Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"
                                                                                            ErrorMessage="<br>Enter Quantity" CssClass="redtext_11px"></asp:RequiredFieldValidator>--%>
                                                                                                 <asp:DropDownList ID="ddlSerialNo" runat="server" CssClass="glow" 
                                                                                            Width="220px" TabIndex="10"
                                                                                            AutoPostBack="true" 
                                                                                            onselectedindexchanged="ddlSerialNo_SelectedIndexChanged">
                                                                                        </asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator ID="rfvSerialNo" runat="server" ControlToValidate="ddlSerialNo"
                                                                                            Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"
                                                                                            ErrorMessage="<br>Select Searial No!" CssClass="redtext_11px"></asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                    <td align="left" valign="top">
                                                                                        <asp:TextBox ID="txtweight" runat="server" CssClass="glow" MaxLength="30" TabIndex="11"
                                                                                            Text="0.00" AutoComplete="off" placeholder="Enter Weight" Width="100px" onKeyPress="return checkfloat(event, this);"
                                                                                            oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="rfvtxtweight" runat="server" ControlToValidate="txtweight"
                                                                                            Display="Dynamic" SetFocusOnError="true" ValidationGroup="Submit" ErrorMessage="<br>Enter Bank!"
                                                                                            CssClass="redtext_11px"></asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                    <td align="left" valign="top">
                                                                                        <asp:TextBox ID="txtrate" runat="server" CssClass="glow" MaxLength="30" Text="0.00"
                                                                                            TabIndex="12" AutoComplete="off" placeholder="Enter Rate" Width="100px" onKeyPress="return checkfloat(event, this);"
                                                                                            oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                                                                        <br />
                                                                                        <asp:RequiredFieldValidator ID="rfvtxtrate" runat="server" ControlToValidate="txtrate"
                                                                                            InitialValue="" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Submit"
                                                                                            ErrorMessage="Enter Rate!" CssClass="redtext_11px"></asp:RequiredFieldValidator>&nbsp;
                                                                                        <asp:CustomValidator ID="CvtxtRate" runat="server" ControlToValidate="txtrate" CssClass="redtext_11px"
                                                                                            ErrorMessage="Rate cannot Be Zero" />
                                                                                    </td>
                                                                                    <td align="right" valign="top" width="100px">
                                                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="posText" Height="30"
                                                                                            Width="80px" ValidationGroup="Submit" TabIndex="13" OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                    </td>
                                                                                    <td align="left" valign="top">
                                                                                        <asp:Button ID="btnNew" runat="server" Text="New" CssClass="posText" Height="30"
                                                                                            Width="80px" TabIndex="14" OnClick="btnNew_Click" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="1" colspan="7" bgcolor="#C9C9C9">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="7" bgcolor="#F5FAFF" class="btn_01">
                                                                            <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None"
                                                                                Width="100%" GridLines="Both" AllowPaging="True" HeaderStyle-CssClass="gridRow"
                                                                                BorderWidth="0" RowStyle-CssClass="gridAlternateRow" AlternatingRowStyle-CssClass="gridRow"
                                                                                ShowFooter="true" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand"
                                                                                OnRowDataBound="grdMain_RowDataBound" OnRowCreated="grdMain_RowCreated">
                                                                                <HeaderStyle CssClass="linearBg" ForeColor="Black" />
                                                                                <FooterStyle ForeColor="Black" />
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                                                        <ItemStyle Width="50" HorizontalAlign="Center" />
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="imgbtndelete" runat="server" ImageUrl="~/Images/delete_sm.png"
                                                                                                CommandArgument='<%#Eval("id") %>' CommandName="cmddelete" OnClientClick="return confirm('Do you want to delete this record ?');" />
                                                                                            <asp:ImageButton ID="imgbtnedit" runat="server" ImageUrl="~/Images/edit_sm.png" OnClientClick="return confirm('Do you want to edit this record ?');"
                                                                                                CommandArgument='<%#Eval("id") %>' CommandName="cmdedit" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                                                        <ItemStyle Width="50" HorizontalAlign="Center" />
                                                                                        <ItemTemplate>
                                                                                            <%#Container.DataItemIndex+1 %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Item Name">
                                                                                        <ItemStyle HorizontalAlign="Left" Width="200" />
                                                                                        <ItemTemplate>
                                                                                            <%#Eval("Item_Name")%>
                                                                                        </ItemTemplate>
                                                                                        <FooterStyle HorizontalAlign="Center" />
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblTotal" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Serial No">
                                                                                        <ItemStyle HorizontalAlign="Left" Width="200" />
                                                                                        <ItemTemplate>
                                                                                            <%#Eval("StckDetl_No")%>
                                                                                        </ItemTemplate>
                                                                                        <FooterStyle HorizontalAlign="Center" />
                                                                                        <FooterTemplate>
                                                                                          
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150" HeaderText="Unit Name">
                                                                                        <ItemStyle HorizontalAlign="Left" Width="150" />
                                                                                        <ItemTemplate>
                                                                                            <%#Eval("Unit_Name")%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>--%>
                                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Quantity">
                                                                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                                                                        <ItemTemplate>
                                                                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Quantity")))%>
                                                                                        </ItemTemplate>
                                                                                        <FooterStyle HorizontalAlign="Right" />
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblQuantity" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Weight">
                                                                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                                                                        <ItemTemplate>
                                                                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Weight")=="" ? 0:Convert.ToDouble(Eval("Weight"))))%>
                                                                                        </ItemTemplate>
                                                                                        <FooterStyle HorizontalAlign="Right" />
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblWeight" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="right" HeaderStyle-Width="100" HeaderText="Item Rate">
                                                                                        <ItemStyle HorizontalAlign="right" Width="100" />
                                                                                        <ItemTemplate>
                                                                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Rate")))%>
                                                                                        </ItemTemplate>
                                                                                        <FooterStyle HorizontalAlign="Right" />
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblRate" align="right" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Mrp">
                                                                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                                                                        <ItemTemplate>

                                                                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Rate")))%>
                                                                                        </ItemTemplate>
                                                                                        <FooterStyle HorizontalAlign="Right" />
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblRate" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>--%>
                                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Amount">
                                                                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                                                                        <ItemTemplate>
                                                                                            <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Amount")))%>
                                                                                        </ItemTemplate>
                                                                                        <FooterStyle HorizontalAlign="Right" />
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="90" HeaderText="Remark">
                                                                                        <ItemStyle HorizontalAlign="Left" Width="90" />
                                                                                        <ItemTemplate>
                                                                                            <%#Eval("Detail")%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>--%>
                                                                                </Columns>
                                                                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                                <EmptyDataTemplate>
                                                                                    <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                                                                </EmptyDataTemplate>
                                                                                <PagerStyle CssClass="white_bg" HorizontalAlign="Center" />
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="7" bgcolor="#E8F2FD" class="btn_01" align="left">
                                                                            <table>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="1" colspan="7" bgcolor="#C9C9C9">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="7" bgcolor="#F5FAFF" class="btn_01" align="left">
                                                                            <table border="0" width="100%">
                                                                                <tr>
                                                                                    <td align="right" width="500px">
                                                                                        Net Amount
                                                                                    </td>
                                                                                    <td align="right" width="90px">
                                                                                        <asp:TextBox ID="txtNetAmnt" runat="server" CssClass="glow" Width="114" MaxLength="7"
                                                                                            TabIndex="44" Enabled="false" Text="0.00" align="right" OnChange="ComputeCosts();"
                                                                                            onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"
                                                                                            onKeyPress="return checkfloat(event, this);"></asp:TextBox>
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
                                      
                              
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="900px" border="0" align="center" cellpadding="0" cellspacing="0" class="ibdr"
                    style="background-color: White">
                    <tr>
                        <td>
                            <table width="65%" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        &nbsp;
                                        <asp:Label ID="lblmessage" runat="server" Font-Bold="true" Visible="false" CssClass="redfont1"
                                            Text=""></asp:Label>
                                    </td>
                                </tr>
                    <tr>
                        <td colspan="5" align="center">
                            <table>
                                <tr>
                                    <td align="center">
                                        <asp:ImageButton ID="imgBtnNew" runat="server" ImageUrl="~/Images/new_img.jpg" onmouseout="mouseOutImage('New')"
                                            onmouseover="mouseOverImage('New')" TabIndex="15" Visible="false" OnClick="imgBtnNew_Click" />
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="imgBtnSave" runat="server" ImageUrl="~/Images/save_img.jpg"
                                            ValidationGroup="save" onmouseover="mouseOverImage('save')" onmouseout="mouseOutImage('save')"
                                            OnClientClick=" return KeyUpEvent()" TabIndex="16" OnClick="imgBtnSave_Click" />
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="imgBtnCancel" runat="server" ImageUrl="~/Images/cancel_img.jpg"
                                            TabIndex="17" onmouseover="mouseOverImage('cancel')" onmouseout="mouseOutImage('cancel')"
                                            OnClick="imgBtnCancel_Click" />
                                        <asp:HiddenField ID="hidmindate" runat="server" />
                                        <asp:HiddenField ID="hidmaxdate" runat="server" />
                                        <asp:HiddenField ID="HidiFromCity" runat="server" />
                                        <asp:HiddenField ID="hidTBBType" runat="server" />
                                        <asp:HiddenField ID="hidrowid" runat="server" />
                                        <asp:HiddenField ID="hidpostingmsg" runat="server" />
                                        <asp:HiddenField ID="hidGRHeadIdno" runat="server" />
                                        <asp:LinkButton ID="lnkbtn" runat="server" Text=""></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnAtSave" runat="server" Text=""></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnAtSave1" runat="server" Text=""></asp:LinkButton>
                                    </td>
                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Weight")=="" ? 0:Convert.ToDouble(Eval("Weight"))))%>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr id="dvAccpost" runat="server" visible="false">
                        <td align="right" bgcolor="#F5FAFF" class="btn_01" style="height: 30px">
                            &nbsp;
                        </td>
                        <td align="right" bgcolor="#F5FAFF" class="btn_01" style="height: 30px">
                        </td>
                        <td align="right" bgcolor="#F5FAFF" class="btn_01" style="height: 15px">
                            &nbsp;
                        </td>
                        <td align="right" bgcolor="#F5FAFF" class="btn_01" style="height: 30px">
                        </td>
                        <td align="right" bgcolor="#F5FAFF" class="btn_01" style="height: 30px">
                            <asp:Label ID="lblPostingLeft" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="5" align="center" class="style1">
            </td>
        </tr>
    </table>
    </td> </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr style="display: none">
        <td class="white_bg" align="center">
            <div id="print" style="font-size: 13px;">
                <table cellpadding="1" cellspacing="0" width="800" border="1" style="font-family: Arial,Helvetica,sans-serif;">
                    <tr>
                        <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                            border-left-style: none; border-right-style: none">
                            <strong>
                                <asp:Label ID="lblCompanyname" runat="server" Style="font-size: 18px;"></asp:Label><br />
                            </strong>
                            <asp:Label ID="lblCompAdd1" runat="server"></asp:Label>
                            &nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblCompAdd2" runat="server"></asp:Label><br />
                            <asp:Label ID="lblCompCity" runat="server"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblCompState" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblCompCityPin" runat="server"></asp:Label><br />
                            <asp:Label ID="lblCompPhNo" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblFaxNo" Text="FAX No.:" runat="server"></asp:Label>
                            <asp:Label ID="lblCompFaxNo" runat="server"></asp:Label><br />
                            <asp:Label ID="lblTin" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                                ID="lblCompTIN" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <%#Eval("Rate_Type")%>
                    <tr>
                        <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                            border-left-style: none; border-right-style: none">
                            <h3>
                                <strong style="text-decoration: underline">
                                    <asp:Label ID="lblPrintHeadng" runat="server"></asp:Label></strong></h3>
                        </td>
                    </tr>
                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Rate")))%>
                    <tr>
                        <td colspan="4">
                            <table border="0" width="100%">
                                <tr>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        Mat.Issue No.
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <asp:Label ID="lblTransno" runat="server"></asp:Label>
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        Tran. Date
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <asp:Label ID="lblTranDate" runat="server"></asp:Label>
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        Location
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <asp:Label ID="lblLoation" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <asp:Label ID="lblIssueType" Text="Type" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <asp:Label ID="lblType" runat="server"></asp:Label>
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        Truck No.
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <asp:Label ID="lblTruckNo" runat="server"></asp:Label>
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        Issue To
                                    </td>
                                    <td id="TdlblAgent" runat="server">
                                        :
                                    </td>
                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                        <asp:Label ID="lblIssueTo" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table1">
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <HeaderTemplate>
                                        <tr>
                                            <td class="white_bg" style="font-size: 12px" width="10%">
                                                <strong>S.No.</strong>
                                            </td>
                                            <td style="font-size: 12px" width="20%">
                                                <strong>Item Name</strong>
                                            </td>
                                            <td style="font-size: 12px" width="10%">
                                                <strong>Unit Name</strong>
                                            </td>
                                            <td style="font-size: 12px" width="10%">
                                                <strong>Quantity</strong>
                                            </td>
                                            <td style="font-size: 12px" width="10%">
                                                <strong>Weight</strong>
                                            </td>
                                            <td style="font-size: 12px" align="left" width="10%">
                                                <strong>Item Rate</strong>
                                            </td>
                                            <td style="font-size: 12px" align="left" width="10%">
                                                <strong>Amount</strong>
                                            </td>
                                            <td style="font-size: 12px" width="20%">
                                                <strong>Detail</strong>
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="white_bg" width="10%">
                                                <%#Container.ItemIndex+1 %>.
                                            </td>
                                            <td class="white_bg" width="30%">
                                                <%#Eval("Item_Modl")%>
                                            </td>
                                            <td class="white_bg" width="15%">
                                                <%#Eval("UOM_Name")%>
                                            </td>
                                            <td class="white_bg" width="15%">
                                                <%#Eval("Qty")%>
                                            </td>
                                            <td class="white_bg" width="15%">
                                                <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Tot_Weght")))%>
                                            </td>
                                            <td class="white_bg" width="15%" align="left">
                                                <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Item_Rate")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td class="white_bg" width="15%" align="left">
                                                <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td class="white_bg" width="15%" align="right">
                                                <%#(Eval("Detail"))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <%--<asp:Label ID="lblTotalAmnt" runat="server"></asp:Label>--%>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table2">
                                <tr>
                                    <td class="white_bg" width="15%">
                                    </td>
                                    <td class="white_bg" width="15%">
                                    </td>
                                    <td class="white_bg" width="15%" align="center">
                                        <asp:Label ID="lblttl" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="15%" align="left">
                                        <asp:Label ID="lbltotalqty" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="12.5%">
                                        <asp:Label ID="lbltotalWeight" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="12.5%">
                                    </td>
                                    <td class="white_bg" width="12.5%" align="center">
                                        <asp:Label ID="lblTotalAmnt" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td class="white_bg" width="12.5%">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <table width="100%">
                                <tr>
                                    <td colspan="2" align="left" width="50%">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblremark" runat="server" valign="right"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td colspan="2" width="50%">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblcommission" runat="server" Text="Commission" Font-Size="13px" valign="right"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="valuelblcommission" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                </td>
                                                <td style="width: 5px">
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                    <asp:Label ID="lblbilty" runat="server" Text="Bilty" Font-Size="13px" valign="right"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="valuelblbilty" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblcartage" runat="server" Text="Cartage" Font-Size="13px" valign="right"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="valuelblcartage" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                </td>
                                                <td style="width: 5px">
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                    <asp:Label ID="lblsurcharge" runat="server" Text="Surcharge" Font-Size="13px" valign="right"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="valuelblsurcharge" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblwages" runat="server" Text="Wages" Font-Size="13px" valign="right"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="valuelblwages" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                </td>
                                                <td style="width: 5px">
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                    <asp:Label ID="lblPFAmnt" runat="server" Text="PF" Font-Size="13px" valign="right"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="valuelblPFAmnt" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTollTax" runat="server" Text="TollTax" Font-Size="13px" valign="right"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="valuelblTollTax" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                </td>
                                                <td style="width: 5px">
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                    <asp:Label ID="lblserviceTax" runat="server" Text="S.Tax" Font-Size="13px" valign="right"></asp:Label>
                                                </td>
                                                <td id="stax" runat="server">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="valuelblservceTax" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblservtaxConsigner" runat="server" Text="C.Tax" Font-Size="13px"
                                                        valign="right"></asp:Label>
                                                </td>
                                                <td id="ctax" runat="server">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="valuelblservtaxConsigner" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                </td>
                                                <td style="width: 5px">
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                    <asp:Label ID="lblNetAmnt" runat="server" Text="Net Amnt" Font-Size="13px" valign="right"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="valuelblnetAmnt" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="4">
                            <table width="100%" style="font-size: 12px" border="0" cellspacing="0">
                                <tr style="line-height: 25px">
                                    <td colspan="9" style="font-size: 13px" align="left" class="white_bg">
                                        <table width="100%">
                                            <tr>
                                                <td align="left" class="white_bg" style="font-size: 13px" width="50%">
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <b>Customer Signature</b>&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td align="right" class="white_bg" style="font-size: 13px" valign="top" width="50%">
                                                    <br />
                                                    <b>
                                                        <asp:Label ID="lblCompname" runat="server"></asp:Label><br />
                                                        <br />
                                                        <br />
                                                        Authorised Signatory&nbsp;</b>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    </table>
    <div id="dvPrtyDet" class="web_dialog black12" style="display: none; width: 680px;
        height: auto; font-size: 8pt; top: 33%; margin-top: -110px; position: fixed;
        z-index: 50;">
        <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="1" class="ibdr1">
            <tr>
                <td class="web_dialog_title" colspan="5">
                    <asp:Label ID="lblDvPrtyName" runat="server" Text="Communication Detail"></asp:Label>
                </td>
                <td class="web_dialog_title align_right">
                    <span style="cursor: pointer;" onclick="HideClient('dvPrtyDet')">Close</span>
                </td>
            </tr>
            <tr>
                <td align="left" bgcolor="#E8F2FD" valign="middle" width="90px" height="25px">
                    Contact To<span class="redfont1">*</span>
                </td>
                <td align="left" bgcolor="#E8F2FD" valign="middle" colspan="2">
                    <asp:TextBox ID="txtconperson" runat="server" MaxLength="50" TabIndex="46" Width="200px"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvtxtconperson" runat="server" ErrorMessage="Please enter Contact Person!"
                        Display="Dynamic" CssClass="redfont" ControlToValidate="txtconperson" ValidationGroup="SavePrtyDetl"></asp:RequiredFieldValidator>
                </td>
                <td align="left" bgcolor="#E8F2FD" valign="middle" width="80px">
                    Party E-Mail
                </td>
                <td align="left" bgcolor="#E8F2FD" valign="middle" colspan="2">
                    <asp:TextBox ID="txtemail" runat="server" MaxLength="50" TabIndex="47" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="1" colspan="6" bgcolor="#C9C9C9">
                </td>
            </tr>
            <tr>
                <td align="left" bgcolor="#F5FAFF" valign="middle" width="90px" height="25px">
                    Address[1]<span class="redfont1">*</span>
                </td>
                <td align="left" bgcolor="#F5FAFF" valign="middle" colspan="2">
                    <asp:TextBox ID="txtadd1" runat="server" MaxLength="50" TabIndex="48" Width="200px"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvTxtAddrs" runat="server" Display="Dynamic" ControlToValidate="txtadd1"
                        ValidationGroup="SavePrtyDetl" ErrorMessage="Enter Address" SetFocusOnError="true"
                        CssClass="redtext_11px"></asp:RequiredFieldValidator>
                </td>
                <td align="left" bgcolor="#F5FAFF" valign="middle" width="80px">
                    Address[2]
                </td>
                <td align="left" bgcolor="#F5FAFF" valign="middle" colspan="2">
                    <span class="red" style="color: #ff0000">
                        <asp:TextBox ID="txtadd2" runat="server" MaxLength="50" TabIndex="49" Width="200px"></asp:TextBox>
                    </span>
                </td>
            </tr>
            <tr>
                <td height="1" colspan="6" bgcolor="#C9C9C9">
                </td>
            </tr>
            <tr>
                <td align="left" bgcolor="#E8F2FD" valign="middle" width="90px" height="25px">
                    State<span class="redfont1">*</span>
                </td>
                <td align="left" bgcolor="#E8F2FD" valign="middle" width="130px">
                    <asp:DropDownList ID="ddlstate" runat="server" Height="22px" Width="120px" TabIndex="50"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="RFVState" runat="server" Display="Dynamic" ControlToValidate="ddlstate"
                        ValidationGroup="SavePrtyDetl" ErrorMessage="Select State" InitialValue="0" SetFocusOnError="true"
                        CssClass="redtext_11px"></asp:RequiredFieldValidator>
                </td>
                <td align="left" bgcolor="#E8F2FD" valign="middle" width="80px">
                    City Name
                </td>
                <td align="left" bgcolor="#E8F2FD" valign="middle" width="130px">
                    <span class="red" style="color: #ff0000">
                        <asp:DropDownList ID="ddlcity" runat="server" Height="22px" Width="120px" TabIndex="51">
                        </asp:DropDownList>
                    </span>
                </td>
                <td align="left" bgcolor="#E8F2FD" valign="middle" width="80px">
                    City Area
                </td>
                <td align="left" bgcolor="#E8F2FD" valign="middle">
                    <asp:DropDownList ID="ddlcityarea" runat="server" Height="22px" Width="120px" TabIndex="52">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td height="1" colspan="6" bgcolor="#C9C9C9">
                </td>
            </tr>
            <tr>
                <td align="left" bgcolor="#F5FAFF" valign="middle" height="25px">
                    District
                </td>
                <td align="left" bgcolor="#F5FAFF" valign="middle">
                    <asp:DropDownList ID="ddlDistrict" runat="server" Height="22px" Width="120px" TabIndex="53">
                    </asp:DropDownList>
                </td>
                <td align="left" bgcolor="#F5FAFF" valign="middle">
                    Tehsil
                </td>
                <td align="left" bgcolor="#F5FAFF" valign="middle">
                    <span class="red" style="color: #ff0000">
                        <asp:DropDownList ID="ddlTehsil" runat="server" Height="22px" Width="120px" TabIndex="53">
                        </asp:DropDownList>
                    </span>
                </td>
                <td align="left" bgcolor="#F5FAFF" valign="middle">
                    Post
                </td>
                <td align="left" bgcolor="#F5FAFF" valign="middle">
                    <asp:DropDownList ID="ddlPost" runat="server" Height="22px" Width="120px" TabIndex="54">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td height="1" colspan="6" bgcolor="#C9C9C9">
                </td>
            </tr>
            <tr>
                <td align="left" bgcolor="#E8F2FD" valign="middle" height="25px">
                    Pin Code
                </td>
                <td align="left" bgcolor="#E8F2FD" valign="middle">
                    <span class="red" style="color: #ff0000">
                        <asp:TextBox ID="txtpinno" runat="server" MaxLength="6" Width="120px" Style="text-align: right;"
                            TabIndex="55"></asp:TextBox>
                    </span>
                </td>
                <td align="left" bgcolor="#E8F2FD" valign="middle">
                    Phone(O)
                </td>
                <td align="left" bgcolor="#E8F2FD" valign="middle" width="130px">
                    <span class="red" style="color: #ff0000">
                        <asp:TextBox ID="txtphoneno" runat="server" MaxLength="11" Width="110px" TabIndex="56"
                            Style="text-align: right;"></asp:TextBox>
                    </span>
                </td>
                <td align="left" bgcolor="#E8F2FD" valign="middle">
                    Phone(R)
                </td>
                <td align="left" bgcolor="#E8F2FD" valign="middle">
                    <asp:TextBox ID="txtPhoneNoRes" runat="server" MaxLength="11" Width="120px" TabIndex="57"
                        Style="text-align: right;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="1" colspan="6" bgcolor="#C9C9C9">
                </td>
            </tr>
            <tr>
                <td align="left" bgcolor="#F5FAFF" valign="middle" height="25px">
                    Mobile No.<span class="redfont1">*</span>
                </td>
                <td align="left" bgcolor="#F5FAFF" valign="middle">
                    <span class="red" style="color: #ff0000">
                        <asp:TextBox ID="txtmobileno" runat="server" MaxLength="10" TabIndex="58" Width="120px"
                            Style="text-align: right;"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvMobile" runat="server" Display="Dynamic" ControlToValidate="txtmobileno"
                            ValidationGroup="SavePrtyDetl" ErrorMessage="Enter Mobile" SetFocusOnError="true"
                            CssClass="redtext_11px"></asp:RequiredFieldValidator>
                        <br />
                        <asp:RegularExpressionValidator ID="revCntPrsnNo1" runat="server" ControlToValidate="txtmobileno"
                            SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Not a valid Mobile No!"
                            CssClass="redfont" Display="Dynamic" ValidationExpression="^[7-9][0-9]{9}$"></asp:RegularExpressionValidator>
                    </span>
                </td>
                <td align="left" bgcolor="#F5FAFF" valign="middle">
                    Fax No.
                </td>
                <td align="left" bgcolor="#F5FAFF" valign="middle">
                    <span class="red" style="color: #ff0000">
                        <asp:TextBox ID="txtfaxno" runat="server" MaxLength="11" TabIndex="59" Width="110px"
                            Style="text-align: right;"></asp:TextBox>
                    </span>
                </td>
                <td align="left" bgcolor="#F5FAFF" valign="middle">
                    Regn. Place
                </td>
                <td align="left" bgcolor="#F5FAFF" valign="middle">
                    <span class="red" style="color: #ff0000">
                        <asp:TextBox ID="txtRegnPlace" runat="server" MaxLength="40" Width="120px" TabIndex="60"
                            Style="text-align: right;"></asp:TextBox>
                    </span>
                </td>
            </tr>
            <tr>
                <td height="1" colspan="6" bgcolor="#C9C9C9">
                </td>
            </tr>
            <tr>
                <td align="left" bgcolor="#E8F2FD" valign="middle" height="25px">
                    TIN No.
                </td>
                <td align="left" bgcolor="#E8F2FD" valign="middle">
                    <span class="red" style="color: #ff0000">
                        <asp:TextBox ID="txtTinno" runat="server" MaxLength="15" Width="120px" TabIndex="61"
                            Style="text-align: right;"></asp:TextBox>
                    </span>
                </td>
                <td align="left" bgcolor="#E8F2FD" valign="middle">
                    Birthday
                </td>
                <td align="left" bgcolor="#E8F2FD" valign="middle">
                    <span class="red" style="color: #ff0000">
                        <asp:TextBox ID="txtDOB" runat="server" TabIndex="62" MaxLength="12" Width="80px"></asp:TextBox>
                    </span>
                </td>
                <td align="left" bgcolor="#E8F2FD" valign="middle">
                    Anniversary
                </td>
                <td align="left" bgcolor="#E8F2FD" valign="middle">
                    <span class="red" style="color: #ff0000">
                        <asp:TextBox ID="txtDOA" runat="server" TabIndex="63" MaxLength="12" Width="80px"></asp:TextBox>
                    </span>
                </td>
            </tr>
            <tr>
                <td height="1" colspan="6" bgcolor="#C9C9C9">
                </td>
            </tr>
            <tr>
                <td align="left" bgcolor="#ccffff" valign="middle" height="20px" colspan="6">
                    <span class="txt"><span class="red" style="color: #ff0000">&nbsp;&nbsp;</span> </span>
                    <strong>Temporary Address [If any]</strong>
                </td>
            </tr>
            <tr>
                <td height="1" colspan="6" bgcolor="#C9C9C9">
                </td>
            </tr>
            <tr>
                <td align="left" bgcolor="#F5FAFF" valign="middle" height="25px">
                    Address1
                </td>
                <td align="left" bgcolor="#F5FAFF" valign="middle" colspan="5">
                    <asp:TextBox ID="txtTempAddr1" runat="server" MaxLength="100" TabIndex="64" Width="550px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="1" colspan="6" bgcolor="#C9C9C9">
                </td>
            </tr>
            <tr>
                <td align="left" bgcolor="#E8F2FD" valign="middle" height="25px">
                    Address2
                </td>
                <td align="left" bgcolor="#E8F2FD" valign="middle" colspan="2">
                    <asp:TextBox ID="txtTempAddr2" runat="server" MaxLength="50" TabIndex="65" Width="190px"></asp:TextBox>
                </td>
                <td align="left" bgcolor="#E8F2FD" valign="middle">
                    Address3
                </td>
                <td align="left" bgcolor="#E8F2FD" valign="middle" colspan="2">
                    <asp:TextBox ID="txtTempAddr3" runat="server" MaxLength="30" TabIndex="66" Width="180px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="1" colspan="6" bgcolor="#C9C9C9">
                </td>
            </tr>
            <tr>
                <td align="right" colspan="3">
                    &nbsp;
                </td>
                <td align="left" colspan="3">
                    <asp:ImageButton ID="btnPrtyDetSave" runat="server" ImageUrl="~/Images/save_img.jpg"
                        TabIndex="67" ValidationGroup="SavePrtyDetl" OnClientClick="return CheckCustomer();" />
                    &nbsp;&nbsp;
                    <asp:Button ID="BtnClerForPurOdr" runat="server" Text="Clear & Close" CssClass="button-class" />
                    <span id="SpnMessageClient" style="display: none;" class="redtext"></span>
                </td>
            </tr>
            <tr>
                <td colspan="6" class="white_bg">
                    <img id="PopupLoaderImageCity" style="display: none;" src="Images/indicator.gif"
                        alt="Please Wait..." title="Please Wait..." />
                </td>
            </tr>
            <tr>
                <td height="1" colspan="6" bgcolor="#C9C9C9">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div id="dvGrdetails" align="center" class="web_dialog black12 MydivCenter" style="display: none;
        min-width: 800px; overflow: scroll; height: 250px; left: 36%;">
        <table style="width: 800px; border: 0px;" cellpadding="3" cellspacing="0">
            <tr>
                <td class="web_dialog_title">
                    <asp:Label ID="Label1" runat="server" Text="Gr Details"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblmsg2" runat="server" Text="Message - Please select only one GR at a time."
                        Visible="false"></asp:Label>
                </td>
                <td class="web_dialog_title align_right" style="width: 15%">
                    <span style="cursor: pointer;" onclick="HideBillAgainst('dvGrdetails')">Close</span>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="white_bg">
                    <table width="100%">
                        <tr>
                            <td align="left" valign="top" bgcolor="#E8F2FD" class="btn_01" style="width: 70px">
                                Date From
                            </td>
                            <td align="left" valign="top" bgcolor="#E8F2FD" class="btn_01" style="width: 145px">
                                <asp:TextBox ID="txtDateFromDiv" runat="server" CssClass="glow" Width="120" TabIndex="68"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvRcptEntryDtFrm" runat="server" ErrorMessage="Enter From Date!"
                                    Display="Dynamic" CssClass="redfont" ControlToValidate="txtDateFromDiv" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" valign="top" bgcolor="#E8F2FD" class="btn_01" style="width: 70px">
                                Date To
                            </td>
                            <td align="left" valign="top" bgcolor="#E8F2FD" class="btn_01" style="width: 150px">
                                <asp:TextBox ID="txtDateToDiv" runat="server" CssClass="glow" Width="120" TabIndex="69"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvRcptEntryDtTo" runat="server" ErrorMessage="Enter To Date!"
                                    Display="Dynamic" CssClass="redfont" ControlToValidate="txtDateToDiv" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                            </td>
                            <td align="center" valign="top" bgcolor="#E8F2FD" class="btn_01">
                                Receiver<span class="red12">*</span>
                            </td>
                            <td align="left" valign="top" bgcolor="#E8F2FD" class="btn_01">
                                <asp:DropDownList ID="ddlRecvrDiv" runat="server" CssClass="glow" Width="150px" AutoPostBack="false"
                                    TabIndex="70">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvddlRecvrDiv" runat="server" ErrorMessage="Select Receiver name"
                                    Display="Dynamic" InitialValue="0" CssClass="redfont" ControlToValidate="ddlRecvrDiv"
                                    ValidationGroup="GRDetailsSrch"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" bgcolor="#F5FAFF" class="btn_01" style="width: 50px">
                                &nbsp; To City<span class="red12">*</span>
                            </td>
                            <td align="left" valign="top" bgcolor="#F5FAFF" class="btn_01">
                                <asp:DropDownList ID="ddltocityDiv" runat="server" CssClass="glow" Width="150px"
                                    AutoPostBack="false" TabIndex="71">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvddltocityDiv" runat="server" ErrorMessage="Select To City ."
                                    Display="Dynamic" InitialValue="0" CssClass="redfont" ControlToValidate="ddltocityDiv"
                                    ValidationGroup="GRDetailsSrch"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" valign="top" bgcolor="#F5FAFF" class="btn_01" style="width: 50px">
                                Deliv.Place<span class="red12">*</span>
                            </td>
                            <td align="left" valign="top" bgcolor="#F5FAFF" class="btn_01">
                                <asp:DropDownList ID="ddldelvplaceDIv" runat="server" CssClass="glow" Width="150px"
                                    AutoPostBack="false" TabIndex="72">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvddldelvplaceDIv" runat="server" ErrorMessage="Select Delivery Place."
                                    Display="Dynamic" InitialValue="0" CssClass="redfont" ControlToValidate="ddldelvplaceDIv"
                                    ValidationGroup="GRDetailsSrch"></asp:RequiredFieldValidator>
                            </td>
                            <td align="center" valign="top" bgcolor="#F5FAFF" class="btn_01" style="width: 20px">
                            </td>
                            <td align="center" valign="top" bgcolor="#F5FAFF" class="btn_01">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btncontrol" Width="60"
                                    ValidationGroup="GRDetailsSrch" TabIndex="73" />
                            </td>
                        </tr>
                        <tr>
                            <td class="btn_01" nowrap="nowrap" valign="middle" style="width: 80px;" colspan="7">
                                <div id="selectall" runat="server" style="padding-left: 100px" visible="false">
                                    Select All&nbsp;&nbsp;
                                    <asp:CheckBox ID="chkSelectAllRows" runat="server" AutoPostBack="true" Visible="false" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:GridView ID="grdGrdetals" runat="server" GridLines="None" AutoGenerateColumns="false"
                                    Width="100%" BorderStyle="None" BorderWidth="0" RowStyle-CssClass="gridAlternateRow"
                                    AlternatingRowStyle-CssClass="gridRow" />
                                <headerstyle cssclass="linearBg" forecolor="Black" />
                                <columns>
                                        <asp:TemplateField HeaderText="Select" HeaderStyle-Width="40px">
                                            <HeaderStyle Width="40" HorizontalAlign="Center" />
                                            <ItemStyle Width="40" HorizontalAlign="Center" />
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAll" runat="server" 
                                                    CssClass="SACatA" />
                                                  <%--  onclick="javascript:SelectAllCheckboxes(this);"--%>
                                                <%--  <asp:LinkButton ID="lnk" runat="server" Text="Select" CssClass="link" CommandName="cmdselect"
                                                            CommandArgument='<%#Eval("ID") %>'></asp:LinkButton>--%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkId" runat="server"  />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rcpt No." HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("Rcpt_No"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Convert.ToDateTime(Eval("Rcpt_Date")).ToString("dd-MMM-yyyy")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To City" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%#Eval("ToCity")%>
                                                <%-- <asp:Label ID="lbltocity" runat="server" Text=' <%#Eval("ToCity")%>'></asp:Label>--%>
                                                <asp:HiddenField ID="hidGrIdno" runat="server" Value='<%#Eval("RcptGoodHead_Idno")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sender No." HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="180px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Eval("Sender_No")%>
                                                <asp:Label ID="lblSenderNo" runat="server" Value='<%#Eval("Sender_No")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Agent Name" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="180px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%#Eval("Agnt_Name")%>
                                                <asp:Label ID="lblAgentname" runat="server" Value='<%#Eval("Agnt_Name")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="20px" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                &nbsp;
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </columns>
                                <emptydatatemplate>
                                        Records(s) not found.
                                    </emptydatatemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" bgcolor="#E8F2FD" class="btn_01" style="width: 100px"
                                colspan="2">
                            </td>
                            <td align="left" valign="top" bgcolor="#E8F2FD" class="btn_01" style="width: 100px"
                                colspan="5">
                                <asp:Button ID="btndivsubmit" runat="server" Text="Submit" CssClass="btncontrol"
                                    Visible="true" Width="60" TabIndex="74" />
                                <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Amount")))%>&nbsp;&nbsp
                                <asp:Button ID="btnClear" runat="server" Text="Clear & Close" CssClass="btncontrol" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" bgcolor="#F5FAFF" class="btn_01" style="width: 100px"
                                colspan="7">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Label ID="lblmsg" runat="server" Text="Message - Please select only one GR at a time."
                        Visible="false" CssClass="redfont"></asp:Label>
                    <asp:Label ID="Label3" runat="server" CssClass="redfont"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent1 = "<table width='100%' border='0'></table>";
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'left=0,top=0,width=800,height=800,toolbar=1,scrollbars=1,status=1');
            WinPrint.document.write(prtContent1);
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            return false;
        }

    </script>
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

        function ShowModalPopup() {
            ShowDialog(true);
        }
        function ShowDialog(modal) {
            // $("#overlay").show();
            $("#dialog").show();
            $("#dialog").fadeIn(300);

            if (modal) {
                $("#dialog").unbind("click");
                //$("#overlay").unbind("click");
            }
            else {
                //  $("#overlay").click(function (e) {
                $("#dialog").click(function (e) {
                    HideDialog();
                });
            }
        }

        function HideDialog() {
            //   $("#overlay").hide();
            $("#dialog").fadeOut(300);
        }

        function Focus() {
            $("#txtGRNo").focus();
        }

        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $("#<%=txtGRDate.ClientID %>").datepicker({
                buttonImageOnly: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate,
                changeMonth: true,
                changeYear: true,
                showOn: 'both',
                buttonImage: '../images/calendar.gif',
                focus: true

            });

        }
    </script>
    <script language="javascript" type="text/javascript">
        function HideClient(dvNm) {
            $("#" + dvNm).fadeOut(300);
        }

        function ShowClient(dvNm) {
            $("#" + dvNm).fadeIn(300);
        }

        function ReloadPage() {
            setTimeout('window.location.href = window.location.href', 2000);
        }

        function HideBillAgainst() {
            $("#dvGrdetails").fadeOut(300);
        }

        function ShowClient() {
            $("#dvGrdetails").fadeIn(300);
        }

        function ShowConfirm() {
            var ans = confirm('Entered Material Issue No. is not in sequence. Do you want to continue?');

            if (ans == false) {
                var btn = document.getElementById('<%=lnkbtn.ClientID%>');
                btn.click();
            }
        }

        function ShowConfirmAtSave() {
            var ans = confirm('Entry already made with this Material Issue. No. Do you want to regenerate it?');

            if (ans == true) {
                var btnsav1 = document.getElementById('<%=lnkbtnAtSave1.ClientID%>');
                btnsav1.click();
            }
            else if (ans == false) {
                var btnsav = document.getElementById('<%=lnkbtnAtSave.ClientID%>');
                btnsav.click();
            }
        }


    </script>
    <script language="javascript" type="text/javascript">

        function mouseOverImage(ctrlname) {
            if (ctrlname == "save") {
                $("#<%=imgBtnSave.ClientID %>").attr("src", "images/save_btn.jpg");
            }
            else if (ctrlname == "cancel") {
                $("#<%=imgBtnCancel.ClientID %>").attr("src", "images/cancel_btn.jpg");
            }
            else if (ctrlname == "New") {
                $("#<%=imgBtnNew.ClientID %>").attr("src", "images/new_btn.jpg");
            }
        }
        function mouseOutImage(ctrlname) {
            if (ctrlname == "save") {
                $("#<%=imgBtnSave.ClientID %>").attr("src", "images/save_img.jpg");
            }
            else if (ctrlname == "cancel") {
                $("#<%=imgBtnCancel.ClientID %>").attr("src", "images/cancel_img.jpg");
            }
            else if (ctrlname == "New") {
                $("#<%=imgBtnNew.ClientID %>").attr("src", "images/new_img.jpg");
            }
        }
        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox

            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
    spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
          elm[i].id != theBox.id) {
                    //elm[i].click();

                    if (elm[i].checked != xState)
                        elm[i].click();
                    //elm[i].checked=xState;

                }
        }

        function CallPrint(strid) {
            var prtContent1 = "<table width='100%' border='0'></table>";
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=800,height=800,toolbar=1,scrollbars=1,status=1');
            WinPrint.document.write(prtContent1);
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            return false;
        }
    </script>
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