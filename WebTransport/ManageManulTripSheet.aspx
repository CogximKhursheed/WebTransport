<%@ Page Title="Manage GR Preparation" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="ManageManulTripSheet.aspx.cs" EnableEventValidation="false" Inherits="WebTransport.ManageManulTripSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/U-Custom.css" rel="stylesheet" type="text/css" />
    <link href="css/tables.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="center-block" style="width: 100%; margin-top: 30px; display: block;">
        <section class="panel panel-default full_form_container part_purchase_bill_form auto-height-form" style="box-shadow: 0 0px 2px gray; border: none; margin-top: 30px">
            <!--FORM HEADER-->
            <div class="ibox-title">
                <h5><div class="printing-animation icon-size"></div>Manual Trip Sheet List </h5>
                <div class="pull-right title-action">
                    <div class="action-center">
		                <!--DOWNLOAD OPTIONS-->
                        <div id="prints" runat="server">
                        <div class="fa fa-download"></div>
                        <div class="download-option-box">
                            <div class="download-option-container">
                                <ul>
                                    <li class="download-excel" data-name="Download excel"><asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel"  ToolTip="Export to excel" ImageUrl="~/images/Excel_Img.JPG" style="height: 100%" onclick="imgBtnExcel_Click" Visible="false"/></li>
                                </ul>
                            </div>
                            <div class="close-download-box" title="Close download window"></div>
                        </div>
                         </div>
                        <a href="ManualTripSheet.aspx"><i style="font-size: 13px !important;" class="fa fa-plus action-icon-style1"></i></a>
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
                                 <asp:DropDownList ID="ddlDateRange" runat="server" AutoPostBack="True" CssClass="form-control backlight" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date From</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="Datefrom" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="6"  ></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date To</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="Dateto" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="50" ></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Trip No.</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtTripNo" runat="server" PlaceHolder="Gr Number" CssClass="form-control backlight" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Sender</span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:DropDownList ID="ddlSender" runat="server" CssClass="chzn-select" style="width:100%;"> </asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Location City</span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:DropDownList ID="drpCityFrom" runat="server" CssClass="form-control backlight" >  </asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Truck No.</span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:DropDownList ID="ddlLorry_No" runat="server" CssClass="form-control backlight"> </asp:DropDownList>
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
                        <asp:GridView ID="grdMain" runat="server" CssClass="table-style-white last-row-select" AutoGenerateColumns="false" BorderStyle="None" Width="100%" GridLines="None" AllowPaging="true" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand" OnRowDataBound="grdMain_RowDataBound">
                            <Columns>
                                 <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" Width="50" />
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>.
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="GR No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                <ItemStyle HorizontalAlign="Center" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("Trip_No")%>
                                                    <asp:Label ID="lblGridNo" runat="server" Text='<%#Eval("Trip_Idno") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="GR Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Convert.ToDateTime(Eval("Trip_Date")).ToString("dd-MM-yyyy")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Party" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("Acnt_Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="From City" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("City_Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Truck No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("Lorry_No")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Driver" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                <ItemStyle HorizontalAlign="Center" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("Driver_Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Total KM" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                <ItemStyle HorizontalAlign="Center" Width="100" />
                                                <ItemTemplate>
                                                <%#Convert.ToDouble(string.IsNullOrEmpty(Convert.ToString(Eval("EndKms"))) ? "0" : (Convert.ToDouble((Eval("EndKms"))).ToString("N2"))) - Convert.ToDouble(string.IsNullOrEmpty(Convert.ToString(Eval("StartKms"))) ? "0" : (Convert.ToDouble((Eval("StartKms"))).ToString("N2")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                <ItemStyle HorizontalAlign="Center" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("Quantity")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Gross Weight">
                                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Gross_Weight")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Freight_Amnt">
                                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Freight_Amnt")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="TotalParty_Adv">
                                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("TotalParty_Adv")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="TotalParty_Bal">
                                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("TotalParty_Bal")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="TotalVeh_Amnt">
                                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("TotalVeh_Amnt")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Net Amount">
                                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("NetTrip_Profit")))%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                             <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                <ItemTemplate>                                               
                                                <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("Trip_Idno") %>' CommandName="cmdedit" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lnkbtnDelete" CssClass="edit" runat="server" CommandArgument='<%#Eval("Trip_Idno") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                                       <b>
                                        <div class="secondFooterClass"  id="divpaging" runat="server" visible="false">                                                                           
                                        <div class="col-sm-4" style="text-align:left"><asp:Label ID="lblcontant" runat="server"></asp:Label></div>  
                                        <div class="col-sm-4" style="text-align:right">TOTAL</div>
                                        <div class="col-sm-2" style="text-align:right"><asp:Label ID="lblNetGrossTotalAmount" runat="server"></asp:Label></div>
                                        <div class="col-sm-1" style="text-align:right;width:10%"><asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label></div>
                                       </div>
                                       </b>
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
    <asp:HiddenField ID="hidmindate" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />
    <asp:HiddenField ID="hidrcptheadidno" runat="server" />


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



    <script type="text/javascript">                $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
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

        function ShowModalPopup() {
            ShowDialog(true);
        }
        function ShowDialog(modal) {
            $("#overlay").show();
            $("#dialog").fadeIn(300);

            if (modal) {
                $("#overlay").unbind("click");
            }
            else {
                $("#overlay").click(function (e) {
                    HideDialog();
                });
            }
        }

        function HideDialog() {
            $("#overlay").hide();
            $("#dialog").fadeOut(300);
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

        function openModalGrdReport() {
            $('#gr_Report_form').modal('show');
        }

        function ShowModalPopup() {
            ShowDialog(true);
        }
        function ShowDialog(modal) {
            // $("#overlay").show();
            $("#dialog").show();
            $("#dialog").fadeIn(300);

            if (modal) {
                $("#dialog").unbind("click");
            }
            else {
                $("#dialog").click(function (e) {
                    HideDialog();
                });
            }
        }
        function HideDialog() {
            $("#dialog").fadeOut(300);
        }
        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $("#<%=Datefrom.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
            $("#<%=Dateto.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
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
