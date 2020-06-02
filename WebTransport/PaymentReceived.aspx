<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="PaymentReceived.aspx.cs" Inherits="WebTransport.PaymentReceived" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <%--<tr>
                    <td align="left" valign="top" class="header_bt_bg">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
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
                                                            <asp:Literal ID="Literal1" runat="server"></asp:Literal><span><b> <a href="Menus.aspx">
                                                                <span class="orange12">Home</span> </a><span>
                                                                    <img src='images/black_arrow.gif' alt="" /></span><asp:Label ID="lblbreadcrum" runat="server"
                                                                        Text=" Amount Received"></asp:Label></b></span>
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
                            </tr>
                        </table>
                    </td>
                </tr>--%>
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
                                                <div style="float: left; width: 50%; text-align: left;">
                                                    Amount Received Against Challan</div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="ibdr">
                                                                <tr>
                                                                    <td align="center" valign="top">
                                                                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#CCCCCC"
                                                                            class="ibdr1">
                                                                            <tr style="width: 60px;">
                                                                                <td colspan="5">
                                                                                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="ibdr">
                                                                                        <tr>
                                                                                            <td bgcolor="#E8F2FD" class="btn_01" style="width: 100px" valign="top">
                                                                                                Challan Detail<span class="redfont">*</span>
                                                                                            </td>
                                                                                            <td valign="top" align="left" bgcolor="#E8F2FD" class="btn_01" colspan="4">
                                                                                                <asp:DropDownList ID="drpChallanDetail" runat="server" CssClass="glow" TabIndex="1"
                                                                                                    Width="250px" OnSelectedIndexChanged="drpChallanDetail_SelectedIndexChanged"
                                                                                                    AutoPostBack="true">
                                                                                                </asp:DropDownList>
                                                                                                <asp:RequiredFieldValidator ID="rfvdrpChallanDetail" runat="server" ControlToValidate="drpChallanDetail"
                                                                                                    Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="save"
                                                                                                    class="red12" ErrorMessage="</br>Select From Challan Detail"></asp:RequiredFieldValidator>
                                                                                            <td bgcolor="#E8F2FD" class="btn_01" valign="top">
                                                                                                &nbsp;&nbsp;DateRange
                                                                                            </td>
                                                                                            <td valign="top" align="left" bgcolor="#E8F2FD" class="btn_01">
                                                                                                <asp:DropDownList ID="ddlDateRange" runat="server" Width="200px" CssClass="glow"
                                                                                                    AutoPostBack="True" TabIndex="2" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td bgcolor="#E8F2FD" class="btn_01" valign="top">
                                                                                                &nbsp;&nbsp;Net Amnt
                                                                                            </td>
                                                                                            <td valign="top" align="left" bgcolor="#E8F2FD">
                                                                                                <asp:TextBox ID="txtNetAmnt" runat="server" CssClass="glow" Width="145px" TabIndex="3"
                                                                                                    MaxLength="8" ReadOnly="True" Style="text-align: right;" Text="0.00" OnTextChanged="txtNetAmnt_TextChanged"></asp:TextBox><br />
                                                                                                        </td>
                                                                                                        </td>
                                                                                                        </tr>
                                                                                                        
                                                                                    
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="9" bgcolor="#E8F2FD" class="btn_01">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td align="left">
                                                                                                Recevied Date<span class="redfont">*</span>
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                SummaryNo<span class="redfont">*</span>
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                ReceivedAmount
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                Remark<span class="redfont">*</span>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td valign="top" align="left">
                                                                                                <asp:TextBox ID="txtRecDate" runat="server" Width="161px" CssClass="glow" TabIndex="4"
                                                                                                    onDrop="blur();return false;" onpaste="return false" oncut="return false" oncopy="return false"
                                                                                                    autocomplete="off"></asp:TextBox><br />
                                                                                                <asp:RequiredFieldValidator ID="rfvtxtRecDate" runat="server" ControlToValidate="txtRecDate"
                                                                                                    CssClass="redtext_11px" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Please Enter Date"
                                                                                                    ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <td valign="top" align="left">
                                                                                                <asp:TextBox ID="txtSummaryNo" runat="server" CssClass="glow" Width="145px" TabIndex="5"
                                                                                                    MaxLength="20" Style="text-align: right;"></asp:TextBox><br />
                                                                                                <asp:RequiredFieldValidator ID="rfvtxtSummaryNo" runat="server" ControlToValidate="txtSummaryNo"
                                                                                                    Display="Dynamic" SetFocusOnError="true" ErrorMessage="Please Enter Summary No"
                                                                                                    CssClass="redtext_11px" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <td valign="top" align="left">
                                                                                                <asp:TextBox ID="txtReceivedAmount" runat="server" CssClass="glow" Width="160px"
                                                                                                    TabIndex="6" MaxLength="8" Style="text-align: right;" Text="0.00" OnTextChanged="txtReceivedAmount_TextChanged"></asp:TextBox>
                                                                                            </td>
                                                                                            <td valign="top" align="center">
                                                                                                <asp:TextBox ID="txtRemark" runat="server" CssClass="glow" Width="145px" MaxLength="50"
                                                                                                    Style="text-align: right;" TabIndex="7" onKeyPress="return checkfloat(event, this);"
                                                                                                    onDrop="blur();return false;" onblur="todigit();" onpaste="return false" oncut="return false"
                                                                                                    oncopy="return false" AutoPostBack="true"></asp:TextBox><br />
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Remark"
                                                                                                    ControlToValidate="txtRemark" CssClass="redtext_11px" Display="Dynamic" SetFocusOnError="true"
                                                                                                    ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Button ID="btnSubmit" runat="server" CssClass="posText" Style="height: 25px;"
                                                                                                    TabIndex="8" Text="Submit" ValidationGroup="Submit" Width="69px" OnClick="btnSubmit_Click"
                                                                                                    ToolTip="Submit" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Button ID="btnNew" runat="server" CssClass="posText" Style="height: 25px;" TabIndex="9"
                                                                                                    Text="New" Width="69px" OnClick="btnNew_Click" ToolTip="New" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <%--   <tr>
                                                                               <%-- <td colspan="8" bgcolor="#E8F2FD" class="btn_01">
                                                                                    <table>
                                                                                        <tr>
                                                                                         
                                                                                          
                                                                                            <td>
                                                                                                <asp:Button ID="btnSubmit" runat="server" CssClass="posText" Style="height: 25px;"
                                                                                                    TabIndex="7" Text="Submit" ValidationGroup="Submit" Width="69px" OnClick="btnSubmit_Click"
                                                                                                    ToolTip="Submit" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Button ID="btnNew" runat="server" CssClass="posText" Style="height: 25px;" TabIndex="8"
                                                                                                    Text="New" Width="69px" OnClick="btnNew_Click" ToolTip="New" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>--%>
                                                                    </td>
                                                                </tr>
                                                                --%>
                                                            </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="1" colspan="7" bgcolor="#C9C9C9">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7" bgcolor="#F5FAFF" class="btn_01">
                                                            <asp:GridView ID="Gridmainhead" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                BorderStyle="Solid" CssClass="ibdr gridBackground internal_heading" GridLines="Both"
                                                                BorderWidth="1" RowStyle-CssClass="gridAlternateRow" AlternatingRowStyle-CssClass="gridRow"
                                                                HeaderStyle-CssClass="linearBg">
                                                                <HeaderStyle CssClass="linearBg" ForeColor="Black" />
                                                                <%--     <FooterStyle ForeColor="Black" />--%>
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="40" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Truck No" HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="left">
                                                                        <ItemStyle Width="40" HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTruck" runat="server" Text='<%#Eval("Truck_No")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Owner Name" HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="40" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOwnrName" runat="server" Text='<%#Eval("Owner_Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle HorizontalAlign="Center" />
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblqtytotal" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Driver Name" HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="left">
                                                                        <ItemStyle Width="40" HorizontalAlign="left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDriverName" runat="server" Text='<%#Eval("Driver_Name")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="To City" HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemStyle Width="40" HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblToCity" runat="server" Text='<%#Eval("To_City")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <asp:Label ID="lblnorecord" runat="server" Text="No record found"></asp:Label>
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td bgcolor="#F5FAFF" colspan="6" height="1">
                                                        &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7" bgcolor="#F5FAFF" class="btn_01">
                                                            <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                BorderStyle="Solid" CssClass="ibdr gridBackground internal_heading" GridLines="Both"
                                                                ShowFooter="true" BorderWidth="1" RowStyle-CssClass="gridAlternateRow" AlternatingRowStyle-CssClass="gridRow"
                                                                HeaderStyle-CssClass="linearBg" OnRowCommand="grdMain_RowCommand" OnRowDataBound="grdMain_RowDataBound">
                                                                <HeaderStyle CssClass="linearBg" ForeColor="Black" />
                                                                <FooterStyle ForeColor="Black" />
                                                                <Columns>
                                                                 <asp:TemplateField HeaderText="Action" HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="imgbtnedit" runat="server" ImageUrl="~/Images/edit_sm.png" CommandArgument='<%#Eval("Id") %>'
                                                                                CommandName="cmdedit" />
                                                                            <asp:ImageButton ID="imgbtndelete" runat="server" ImageUrl="~/Images/delete_sm.png"
                                                                                CommandArgument='<%#Eval("id") %>' CommandName="cmddelete" />
                                                                        </ItemTemplate>
                                                                      
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                        
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Received Date" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="left">
                                                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRecDate" runat="server" Text='<%#Convert.ToDateTime(Eval("Recvng_Date")).ToString("dd-MM-yyyy") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                           <FooterStyle HorizontalAlign="Left" />
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblTotalRecd" Text='<%#Eval("<%#Container.DataItemIndex+1 %>") %>' runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Summary No." HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSmryNo" runat="server" Text='<%#Eval("Sumry_No") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Received Amnt" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Right">
                                                                        <ItemStyle Width="150px" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRecvAmnt" runat="server" Text=' <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Recvng_Amnt")))%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" />
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblTolRecvAmnt" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Remark" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRemrk" runat="server" Text='<%#Eval("Remark")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                          <FooterStyle HorizontalAlign="Left" />
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblPendingAmnt" Text="Pending amnt:" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                   
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <asp:Label ID="lblnorecord" runat="server" Text="No record found"></asp:Label>
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                        </td>
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
                        <table width="900px" border="0" align="center" cellpadding="0" cellspacing="0" class="ibdr">
                            <tr>
                                <td>
                                    <table width="67%" border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td colspan="5">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" align="center">
                                                <table>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:ImageButton ID="imgBtnNew" runat="server" ImageUrl="~/Images/new_img.jpg" onmouseout="mouseOutImage('New')"
                                                                onmouseover="mouseOverImage('New')" TabIndex="4" Visible="false" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:ImageButton ID="imgBtnSave" runat="server" ImageUrl="~/Images/save_img.jpg"
                                                                ValidationGroup="save" onmouseover="mouseOverImage('save')" onmouseout="mouseOutImage('save')"
                                                                OnClick="imgBtnSave_Click" />
                                                            <asp:HiddenField ID="hidPaymentid" runat="server" Value="0" />
                                                            <asp:HiddenField ID="Hidrowid" runat="server" Value="" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/delete_img.jpg"
                                                                onmouseover="mouseOverImage('Delete')" onmouseout="mouseOutImage('Delete')" OnClick="imgBtnDelete_Click" />
                                                        </td>
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
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" align="center" class="style1">
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
                        &nbsp;
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hidmindate" runat="server" />
            <asp:HiddenField ID="hidmaxdate" runat="server" />
            </td> </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            </table> </td> </tr> </table>
        </ContentTemplate>
    </asp:UpdatePanel>
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
            $("#<%=txtRecDate.ClientID %>").datepicker({
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                mindate: mindate,
                maxDate: maxdate,
                changeYear: true,
                showOn: "both",
                buttonImage: '../images/calendar.gif',
                buttonImageOnly: true,
            });


        }
        function mouseOverImage(ctrlname) {
            if (ctrlname == "save") {
                $("#<%=imgBtnSave.ClientID %>").attr("src", "images/save_btn.jpg");
            }
            else if (ctrlname == "cancel") {
                $("#<%=imgBtnDelete.ClientID %>").attr("src", "images/delete_btn.jpg");
            }
        }
        function mouseOutImage(ctrlname) {
            if (ctrlname == "save") {
                $("#<%=imgBtnSave.ClientID %>").attr("src", "images/save_img.jpg");
            }
            else if (ctrlname == "cancel") {
                $("#<%=imgBtnDelete.ClientID %>").attr("src", "images/delete_img.jpg");
            }
        }
     
     
    </script>
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