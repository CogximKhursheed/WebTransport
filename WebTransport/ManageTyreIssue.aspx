<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="ManageTyreIssue.aspx.cs" EnableEventValidation="false" Inherits="WebTransport.ManageTyreIssue" %>

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
            <table border="0" cellpadding="2" cellspacing="0" width="100%">
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
                                                            &nbsp;&nbsp;&nbsp;Manage Tyre Issue
                                                        </td>
                                                        <td height="39" align="right" background="images/grd_top_bg.jpg" class="title06">
                                                            <a href="TyreIssue.aspx">Add Tyre Issue &nbsp;&nbsp;&nbsp;</a>
                                                            <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ToolTip="Export to excel"
                                                                ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px" 
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
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="ibdr">
                                                                <tr>
                                                                    <td align="left" bgcolor="#E8F2FD" class="btn_01" nowrap="nowrap" valign="middle">
                                                                        &nbsp; DateRange<span class="redfont">*</span>
                                                                    </td>
                                                                    <td height="35px" align="left" bgcolor="#E8F2FD" class="btn_01" nowrap="nowrap" style="width: 180px;"
                                                                        valign="middle">
                                                                        <asp:DropDownList ID="ddlDateRange" runat="server" AutoPostBack="True" CssClass="glow"
                                                                            OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged" TabIndex="1" Width="210px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td align="left" bgcolor="#E8F2FD" class="btn_01" nowrap="nowrap" valign="middle">
                                                                        &nbsp;&nbsp; Date From
                                                                    </td>
                                                                    <td height="35px" align="left" bgcolor="#E8F2FD" class="btn_01" nowrap="nowrap" style="width: 180px;"
                                                                        valign="middle">
                                                                        <asp:TextBox ID="Datefrom" runat="server" CssClass="glow" MaxLength="6" TabIndex="2"
                                                                            Width="140px"></asp:TextBox>
                                                                    </td>
                                                                    <td height="35px" align="left" bgcolor="#E8F2FD" class="btn_01" nowrap="nowrap" 
                                                                        valign="middle">
                                                                        &nbsp;&nbsp;Date To
                                                                    </td>
                                                                    <td height="35px" align="left" bgcolor="#E8F2FD" class="btn_01" nowrap="nowrap" style="width: 180px;"
                                                                        valign="middle">
                                                                        <asp:TextBox ID="Dateto" runat="server" CssClass="glow" MaxLength="50" TabIndex="3"
                                                                            Width="140px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="35px" align="left" bgcolor="#F5FAFF" class="btn_01" nowrap="nowrap" valign="middle">
                                                                        &nbsp;&nbsp;
                                                                        Location
                                                                    </td>
                                                                    <td height="35px" align="left" bgcolor="#F5FAFF" class="btn_01" nowrap="nowrap" style="width: 180px;"
                                                                        valign="middle">
                                                                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="glow" TabIndex="10" Width="205px"
                                                                            AutoPostBack="true">
                                                                        </asp:DropDownList>
                                                                        <asp:ImageButton ID="ImgBtnLocation" runat="server" ImageUrl="~/images/RefreshNew.jpg"
                                                                            Visible="true" AlternateText="Add" ToolTip="Update From Location" vertical-align="bottom"
                                                                            align="middle" OnClick="ImgBtnLocation_Click" />
                                                                    </td>
                                                                    <td height="30px" align="left" bgcolor="#F5FAFF" class="btn_01" nowrap="nowrap" valign="middle">
                                                                        &nbsp;&nbsp;
                                                                        Truck No.
                                                                    </td>
                                                                    <td height="35px" align="left" bgcolor="#F5FAFF" class="btn_01" nowrap="nowrap" style="width: 180px;"
                                                                        valign="middle">
                                                                        <asp:DropDownList ID="ddlTruckNo" runat="server" CssClass="glow" Width="180px" TabIndex="21">
                                                                        </asp:DropDownList>
                                                                        <asp:ImageButton ID="ImgBtnTruckRefresh" runat="server" ImageUrl="~/images/RefreshNew.jpg"
                                                                                Visible="true" AlternateText="Add" ToolTip="Update Truck No." vertical-align="bottom"
                                                                                align="middle" onclick="ImgBtnTruckRefresh_Click" />
                                                                    </td>
                                                                    <td height="35px" align="left" bgcolor="#F5FAFF" class="btn_01" nowrap="nowrap" valign="middle">
&nbsp;                                                                        &nbsp;<asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/search_img.jpg"
                                                                            onmouseover="mouseOverImage('search')" onmouseout="mouseOutImage('search')" OnClick="btnSearch_Click"
                                                                            TabIndex="9" />
                                                                    </td>
                                                                    <td height="35px" align="left" bgcolor="#F5FAFF" class="btn_01" nowrap="nowrap" style="width: 180px;"
                                                                        valign="middle">
                                                                         &nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server" Text=" Total Record (s): 0" Style="font-size: 15px;
                                                                            font-weight: bold;"></asp:Label>
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
                                                                            BorderWidth="0" RowStyle-CssClass="bgcolrwhite" AlternatingRowStyle-CssClass="bgcolor2"
                                                                            OnRowCommand="grdMain_RowCommand" TabIndex="4" OnRowDataBound="grdMain_RowDataBound"
                                                                            ShowFooter="true" OnRowCreated="grdMain_RowCreated" CssClass="ibdr gridBackground internal_heading"
                                                                            HeaderStyle-CssClass="internal_heading">
                                                                            <HeaderStyle CssClass="linearBg" ForeColor="Black" />
                                                                            <AlternatingRowStyle CssClass="bgcolor2" />
                                                                            <FooterStyle ForeColor="Black" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                                                    <ItemTemplate>
                                                                                        <%#Container.DataItemIndex+1 %>.
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Issue No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("MatIss_No")%>
                                                                                        <asp:Label ID="lblGridNo" runat="server" Text='<%#Eval("MatIss_No") %>' Visible="false"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Issue Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <%#Convert.ToDateTime(Eval("MatIss_Date")).ToString("dd-MM-yyyy")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("MatIss_Typ")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="Location" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("CityTo")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                  <asp:TemplateField HeaderText="Truck No." HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("Lorry_No")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                               
                                                                                <asp:TemplateField HeaderText="Net Amount" HeaderStyle-HorizontalAlign="right" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                    <ItemTemplate>
                                                                                       <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Net_Amnt")))%>
                                                                                       
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right">
                                                                                    <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/delete_sm.png"
                                                                                            OnClientClick="return confirm('Do you want to delete this record ?');" CommandArgument='<%#Eval("MatIss_Idno") %>'
                                                                                            CommandName="cmddelete" ToolTip="Delete" />
                                                                                        <asp:ImageButton ID="imgchallan" runat="server" Visible="false" ToolTip="Challan generated"
                                                                                            autopostback="false" ImageUrl="~/images/Challan.jpg" />
                                                                                        <asp:ImageButton ID="imgSold" runat="server" Visible="false" ToolTip="Invoice generated"
                                                                                            ImageUrl="~/Images/Sold.png" />
                                                                                        <asp:ImageButton ID="imgBtnEdit" runat="server" ImageUrl="~/Images/edit_sm.png" CommandArgument='<%#Eval("MatIss_Idno") %>'
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
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                  <%--      <tr>
                                            <td>
                                                &nbsp;
                                               
                                            </td>
                                        </tr>--%>
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
             <asp:HiddenField ID="hidmindate" runat="server" />
            <asp:HiddenField ID="hidmaxdate" runat="server" />
            <asp:HiddenField ID="hidrcptheadidno" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgBtnExcel" />
        </Triggers>
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
        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $("#<%=Datefrom.ClientID %>").datepicker({
                buttonImageOnly: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate,
                changeMonth: true,
                changeYear: true,
                showOn: 'both',
                buttonImage: '../images/calendar.gif'
            });
            $("#<%=Dateto.ClientID %>").datepicker({
                buttonImageOnly: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate,
                changeMonth: true,
                changeYear: true,
                showOn: 'both',
                buttonImage: '../images/calendar.gif'
            });

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