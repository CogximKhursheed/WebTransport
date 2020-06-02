<%@ Page Title="Manage Trip Entry" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="ManageTripEntry.aspx.cs" EnableEventValidation="false"
    Inherits="WebTransport.ManageTripEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/U-Custom.css" rel="stylesheet" type="text/css" />
    <link href="css/tables.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="center-block" style="width: 100%; margin-top: 30px; display: block;">
        <section class="panel panel-default full_form_container part_purchase_bill_form auto-height-form" style="box-shadow: 0 0px 2px gray; border: none; margin-top: 30px">
            <!--FORM HEADER-->
            <div class="ibox-title">
                <h5><div class="printing-animation icon-size"></div>Manage Trip Entry </h5>
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
                        <a href="TripEntry.aspx"><i style="font-size: 13px !important;" class="fa fa-plus action-icon-style1"></i></a>
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
                                    <asp:DropDownList ID="ddldateRange" runat="server" AutoPostBack="true" CssClass="form-control backlight"  OnSelectedIndexChanged="ddldateRange_SelectedIndexChanged">    </asp:DropDownList>  
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date From</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtReceiptDatefrom" runat="server"  CssClass="input-sm datepicker form-control backlight" data-date-format="dd-mm-yyyy" MaxLength="50"></asp:TextBox>                                
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date To</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtReceiptDateto" runat="server" CssClass="input-sm datepicker form-control backlight" data-date-format="dd-mm-yyyy" MaxLength="50" ></asp:TextBox>                                   
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>From City</span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:DropDownList ID="drpCityFrom" runat="server" TabIndex="4"   CssClass="form-control backlight">  </asp:DropDownList>    
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Truck No</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:DropDownList ID="ddltruckNo" runat="server" CssClass="form-control backlight" ></asp:DropDownList>                           
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Trip No</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtReceiptNo" runat="server" CssClass="form-control backlight" MaxLength="50" ></asp:TextBox>                     
                            </div>
                        </div>
                                <!--SECOND-FOLD-->
                                <div class="col-sm-12 no-pad">
                            <div class="control-holder"> <span class="label-holder">&nbsp;</span></div>
                            <div class="control-holder">
                            <asp:LinkButton ID="lnkBtnPreview" CssClass="btn btn-primary pull-right" runat="server" ValidationGroup="Save"  OnClick="lnkBtnPreview_Click">Search</asp:LinkButton>                                     
                            </div>
                    </div>
                            </div>
                        </div>
                <div class="panel-in-default" >
                    <div class="pull-left"><asp:Label ID="lblTotalRecord" runat="server" Text=" Total Record(s):0" Style="font-size: 13px; text-transform: none;"></asp:Label></div>
                     <div class="pull-right"></div>
                    <div class="report" style="overflow-x:Auto; width:100%;">
                        <div>
                        <asp:GridView ID="grdMain" runat="server" CssClass="table-style-white last-row-select" AutoGenerateColumns="false" BorderStyle="None" Width="100%" GridLines="None" AllowPaging="true" PageSize="35" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand"  OnRowDataBound="grdMain_RowDataBound">
                            <Columns>
                                 <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                        <ItemStyle HorizontalAlign="Center" Width="50" />
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>.
                                                        </ItemTemplate>
                                                                                   
                                                    </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Trip No." HeaderStyle-CssClass="gridHeaderAlignLeft" HeaderStyle-Width="100">
                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                        <ItemStyle HorizontalAlign="Left" Width="100" />
                                                        <ItemTemplate>
                                                            <%#Eval("Trip_No")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="gridHeaderAlignCenter" HeaderStyle-Width="100">
                                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                        <ItemStyle HorizontalAlign="Center" Width="100" />
                                                        <ItemTemplate>
                                                            <%#Convert.ToDateTime(Eval("Trip_Date")).ToString("dd-MM-yyyy")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                 <asp:TemplateField HeaderText="From City" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                        <ItemStyle HorizontalAlign="Left" Width="100" />
                                                        <ItemTemplate>
                                                            <%#Eval("FromCity")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Truck No." HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                        <ItemStyle HorizontalAlign="Left" Width="100" />
                                                        <ItemTemplate>
                                                            <%#Eval("Lorry_No")%>
                                                        </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Left" ForeColor="Black" Font-Bold="true"  Font-Size="Small" />     
                                                            <FooterTemplate >
                                                            <asp:Label ID="lblttlNet" runat="server" >Grid Total</asp:Label>
                                                                </FooterTemplate>   
                                                    </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Net Amount" HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="100">
                                                        <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                        <ItemStyle HorizontalAlign="Right" Width="100" />
                                                        <ItemTemplate>
                                                            <%# Convert.ToDouble(Eval("Net_Amnt")).ToString("N2")%>
                                                        </ItemTemplate>
                                                        <FooterStyle ForeColor="Black" Font-Bold="true" HorizontalAlign="Right" Font-Size="Small" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotalNet" runat="server" ></asp:Label>
                                                            </FooterTemplate>
                                                    </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-CssClass="gridHeaderAlignCenter">
                                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                        <ItemStyle HorizontalAlign="Center" Width="50" />
                                                        <ItemTemplate>
                                                        <asp:LinkButton runat="server" CssClass="edit" ID="lnkBtnEdit"  CommandArgument='<%#Eval("Trip_Idno") %>' CommandName="cmdedit" ToolTip="Edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                            <asp:LinkButton runat="server" CssClass="edit" ID="lnkBtnDelete"   OnClientClick="return confirm('Do you want to delete this record ?');" CommandArgument='<%#Eval("Trip_Idno") %>' CommandName="cmddelete" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                                                   
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                            </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                 <EmptyDataTemplate>
                                     <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                 </EmptyDataTemplate>
                        </asp:GridView>
                            <div class="secondFooterClass"  id="divpaging" runat="server" visible="false">                                                                           
                                        <table class="" id="tblFooterscnd" runat="server" >
		                                  <tr><th rowspan="1" colspan="1" style="width:149px;"> <asp:Label ID="lblcontant" ForeColor="Black" Font-Bold="true"  Font-Size="Small" runat="server"></asp:Label></th><th rowspan="1" colspan="2" style="width: 223px;"></th><th rowspan="1" ForeColor="Black" Font-Bold="true"  Font-Size="Small" colspan="1" style="width: 120px;text-align:right;">Total</th><th rowspan="1" colspan="2" style="width: 110px;padding-left:136px;"><asp:Label ID="lblNetAmnt" ForeColor="Black" Font-Bold="true"  Font-Size="Small" runat="server"></asp:Label>
                                         </th></tr>  </tfoot>
                                        </table>

                                       </div>
                                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <%--hidden fields--%>
    <asp:HiddenField ID="hidmindate" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />


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


    <script language="javascript" type="text/javascript">
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
            $("#<%=txtReceiptDatefrom.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
            $("#<%=txtReceiptDateto.ClientID %>").datepicker({
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
