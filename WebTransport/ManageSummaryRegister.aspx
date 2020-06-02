<%@ Page Title="Summary Register List" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="ManageSummaryRegister.aspx.cs" EnableEventValidation="false"
    Inherits="WebTransport.ManageSummaryRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/U-Custom.css" rel="stylesheet" type="text/css" />
    <link href="css/tables.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="center-block" style="width: 100%; margin-top: 30px; display: block;">
        <section class="panel panel-default full_form_container part_purchase_bill_form auto-height-form" style="box-shadow: 0 0px 2px gray; border: none; margin-top: 30px">
            <!--FORM HEADER-->
            <div class="ibox-title">
                <h5><div class="printing-animation icon-size"></div>Summary Register List </h5>
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
                        <a href="SummaryRegister.aspx"><i style="font-size: 13px !important;" class="fa fa-plus action-icon-style1"></i></a>
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
                                <asp:DropDownList ID="ddlDateRange" runat="server" AutoPostBack="True" CssClass="form-control backlight"  OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged" ></asp:DropDownList> 
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlDateRange" CssClass="form-control" Display="Dynamic" ErrorMessage="Please Select Date Range."  InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>                                 
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date From</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="Datefrom" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="6"  data-date-format="dd-mm-yyyy"></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date To</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="Dateto" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="50"  data-date-format="dd-mm-yyyy" ></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Summary No <span class="required-field">*</span> </span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtsummryno" runat="server" CssClass="form-control backlight" MaxLength="50" ></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>To City</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:DropDownList ID="drpCityTo" runat="server" CssClass="form-control backlight" > </asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Truck No</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:DropDownList ID="drptruckno" runat="server" CssClass="form-control backlight" > </asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Chln. No <span class="required-field">*</span> </span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtCHlnno" runat="server" CssClass="form-control backlight" MaxLength="50" ></asp:TextBox>                                                          
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Driver</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:DropDownList ID="ddlDriver" runat="server" CssClass="form-control backlight" ></asp:DropDownList>
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
                          <table width="100%">
                                       <tr>
                                       <td>
                        <asp:GridView ID="grdMain" runat="server" CssClass="table-style-white last-row-select" AutoGenerateColumns="false" BorderStyle="None" Width="100%" GridLines="None" AllowPaging="true" PageSize="50"  OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand"  OnRowDataBound="grdMain_RowDataBound"  OnRowCreated="grdMain_RowCreated">
                            <Columns>
                                 <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>.
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Summary No" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100">
                                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100" />
                                                    <ItemTemplate>
                                                        <%#Eval("SumReg_No")%>
                                                        <asp:Label ID="lblGridNo" runat="server" Text='<%#Eval("SumReg_No") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Summary Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToDateTime(Eval("SumReg_Date")).ToString("dd-MM-yyyy")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="City To" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                    <ItemTemplate>
                                                        <%#Eval("CityTo")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Challan no." HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                    <ItemTemplate>
                                                        <%#Eval("Chln_no")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Truck No." HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                    <ItemTemplate>
                                                        <%#Eval("Lorry_No")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Driver" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                    <ItemTemplate>
                                                        <%#Eval("driver")%>
                                                    </ItemTemplate>
                                                     <FooterTemplate>
                                                        <asp:Label ID="lbltotal" Visible="true" Text="Grid Total" Font-Bold="true" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                 <%--<asp:TemplateField HeaderText="To City" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                    <ItemTemplate>
                                                        <%#Eval("CityTo")%>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbltotal" Visible="true" Text="Grid Total" Font-Bold="true" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>--%>
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Net Amount">
                                                    <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Right" Width="100" />
                                                    <ItemTemplate>
                                                        <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Net_Amnt")))%>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblAmount" Font-Bold="true" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Right">
                                                    <HeaderStyle HorizontalAlign="Right" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                    <ItemTemplate>
                                                      <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("SumReg_Idno") %>' CommandName="cmdedit"  ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                      <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("SumReg_Idno") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete"  ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>                                                     
                                                        <asp:ImageButton ID="imgchallan" runat="server" Visible="false" ToolTip="Challan generated"  autopostback="false" ImageUrl="~/images/Challan.jpg" />
                                                        <asp:ImageButton ID="imgSold" runat="server" Visible="false" ToolTip="Invoice generated" ImageUrl="~/Images/Sold.png" />
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
		                                  <tr><th rowspan="1" colspan="1" style="width:250px;"> <asp:Label ID="lblcontant" runat="server"></asp:Label></th><th rowspan="1" colspan="1" style="width: 110px;"></th><th rowspan="1" colspan="1" style="width: 50%;text-align:right;">Total</th><th rowspan="1" colspan="1" style=" padding-right: 23px;text-align: right;width: 200px;"><asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label>
                                          </th><th rowspan="1" colspan="1" style="width:2px;"></th><th rowspan="1" colspan="1" style="width: 62px;"></th></tr>                                  
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
    <%--<table border="0" cellpadding="2" cellspacing="0" width="100%">
                <tr>
                    <td class="white_bg " align="center">                       
                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <table width="750" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
                                        class="ibdr">                                       
                                        <tr>
                                            <td>
                                                <table border="0" align="center" cellpadding="0" cellspacing="0">
                                             
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
                                                                                <asp:TemplateField HeaderText="Quotation No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("QuHead_No")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <%#Convert.ToDateTime(Eval("QuHead_Date")).ToString("dd-MM-yyyy")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="From City" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("CityFrom")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="From To" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("CityTo")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Delivery" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("CityDely")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Sender" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("Sender")%>
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
