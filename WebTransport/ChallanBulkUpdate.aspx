<%@ Page Title="Manage Challan Booking" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="ChallanBulkUpdate.aspx.cs" EnableEventValidation="false"
    Inherits="WebTransport.ChallanBulkUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/U-Custom.css" rel="stylesheet" type="text/css" />
    <link href="css/tables.css" rel="stylesheet" type="text/css" />

    <style>
        table {
            position: relative;
        }

        .classPager {
            display: inline-block;
            float: none;
            clear: both;
            width: 100%;
            position: absolute !important;
            bottom: -42px !important;
            background-color: white !important;
        }

            .classPager table, .classPager tr {
                background-color: white !important;
            }

        td {
            position: relative;
        }

        .heading-text {
            display: inline-block;
            font-weight: bold;
            border-bottom: 1px solid;
            width: 100%;
            margin: 5px 0;
            color: gray;
        }

        .fixed-grid {
            height: 200px;
            overflow: auto;
        }

        .label-heading {
            font-size: 12px;
            font-weight: bold;
            padding: 0;
            margin: 0;
        }

        .label-value {
            font-size: 12px;
            font-weight: 400;
            padding: 0;
            margin: 0;
        }

        .mask-sms {
            height: 11px;
            width: 14px;
            background: #fff9;
            display: inline-block;
            position: absolute;
            top: 15px;
            left: 10px;
            cursor: not-allowed;
        }

        .Invert-True {
            display: inline-block;
        }

        .Invert-False {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="center-block" style="width: 100%; margin-top: 30px; display: block;">
        <section class="panel panel-default full_form_container part_purchase_bill_form auto-height-form" style="box-shadow: 0 0px 2px gray; border: none; margin-top: 30px">
             <!--FORM HEADER-->
             <div class="ibox-title">
                <h5><div class="printing-animation icon-size"></div>Challan Bulk Update </h5>
                <div class="pull-right title-action">
                    <div class="action-center">
                        <a href="ChlnBooking.aspx"><i style="font-size: 13px !important;" class="fa fa-plus action-icon-style1"></i></a>
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
                                <asp:DropDownList ID="ddldateRange" runat="server" AutoPostBack="true" CssClass="form-control backlight"   OnSelectedIndexChanged="ddldateRange_SelectedIndexChanged"></asp:DropDownList>   
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date From</span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:TextBox ID="txtReceiptDatefrom" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="50" data-date-format="dd-mm-yyyy"  ></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date To</span>
                            </div>
                            <div class="control-holder full-width">
                                   <asp:TextBox ID="txtReceiptDateto" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="50" data-date-format="dd-mm-yyyy"  ></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Challan No.</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtReceiptNo" runat="server" CssClass="form-control backlight" MaxLength="50"  ></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Loc.[From]</span>
                            </div>
                            <div class="control-holder full-width">
                                  <asp:DropDownList ID="drpCityFrom" runat="server"  CssClass="form-control backlight" ></asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Gr Type</span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:DropDownList ID="ddlGrtype" runat="server"  CssClass="form-control backlight"  AutoPostBack="true" OnSelectedIndexChanged="ddlGrtype_SelectedIndexChanged"  >
                                    <asp:ListItem Text="Gr Prepration" Value="GR"></asp:ListItem>
                                    <asp:ListItem Text="Gr Retailer" Value="GRR"></asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Type</span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:DropDownList ID="ddlTranstype" runat="server" Enabled="false" AutoPostBack="true"  CssClass="form-control backlight" OnSelectedIndexChanged="ddlTranstype_SelectedIndexChanged"  ></asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>
                                    <asp:Label ID="lblTruckNo" runat="server" Text="" ></asp:Label>
                                </span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:DropDownList ID="ddltruckNo" runat="server"  CssClass="form-control backlight"  ></asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>
                                    <asp:Label runat="server" Text="Lorry Party" ></asp:Label>
                                </span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:DropDownList ID="ddlLorryParty" runat="server"  CssClass="form-control chzn-select"></asp:DropDownList>
                            </div>
                        </div>
                                <!--SECOND-FOLD-->
                                <div class="col-sm-12 no-pad">
                            <div class="control-holder"> <span class="label-holder">&nbsp;</span></div>
                            <div class="control-holder">
                            <asp:LinkButton ID="lnkbtnPreview" CssClass="btn btn-primary pull-right" runat="server" ValidationGroup="Save"  OnClick="lnkbtnPreview_OnClick">Search</asp:LinkButton>                                     
                            </div>
                            <div class="control-holder" >
                                <asp:LinkButton ID="lnkUpdateAll" style="margin: 1px;"  CssClass="btn btn-primary pull-right" runat="server" ValidationGroup="Save"  OnClick="lnkUpdateAll_OnClick">Update All</asp:LinkButton>                                     
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
                                <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" Width="50" />
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>.
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ch. No." HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("Chln_No")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Convert.ToDateTime(Eval("Chln_Date")).ToString("dd-MM-yyyy")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="From City" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("FromCity")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Transport Type." HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("Lorry_No")%>
                                                </ItemTemplate>
                                                <FooterStyle ForeColor="Black" Font-Bold="true" HorizontalAlign="Left" Font-Size="Small" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotal" Text='Gross Total :' runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gross Amnt" HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                <ItemTemplate>
                                                    <%# Convert.ToDouble(Eval("Gross_Amnt")).ToString("N2")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Amount" HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                <ItemTemplate>
                                                    <%# Convert.ToDouble(Eval("Net_Amnt")).ToString("N2")%>
                                                </ItemTemplate>
                                                <FooterStyle ForeColor="Black" Font-Bold="true" HorizontalAlign="Right" Font-Size="Small" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblNetAmnt" Text='0.00' runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Commission" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50">
                                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                                <ItemStyle HorizontalAlign="Left" Width="50" />
                                                <ItemTemplate>
                                                    <asp:TextBox style="width: 90px;" ID="txtCommissionAmnt" data-lastvalue='<%#Eval("Commsn_Amnt")%>' onkeyup="IsUpdateComm($(this))" Text='<%#Eval("Commsn_Amnt")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="TDS" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50">
                                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                                <ItemStyle HorizontalAlign="Left" Width="50" />
                                                <ItemTemplate>
                                                    <asp:TextBox style="width: 90px;" ID="txtTDSAmnt" data-lastvalue='<%#Eval("TDSTax_Amnt")%>' onkeyup="IsUpdateTds($(this))" Text='<%#Eval("TDSTax_Amnt")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Update" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" Width="50" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%# Container.DataItemIndex + ";" + Eval("Chln_Idno") %>' CommandName="cmdupdate" TabIndex="5" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                    <div class="clsIsUpdateTds">
                                                        <asp:HiddenField Value="false" ID="hidIsUpdateTds" runat="server"></asp:HiddenField>
                                                    </div>
                                                    <div class="clsIsUpdateComm">
                                                        <asp:HiddenField Value="false" ID="hidIsUpdateComm" runat="server"></asp:HiddenField>
                                                    </div>
                                                    <asp:HiddenField ID="hidChlnidno" Value='<%#Eval("Chln_Idno")%>' runat="server"></asp:HiddenField>
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
		                                  <tr><th rowspan="1" colspan="1" style="width:190px;"> <asp:Label ID="lblcontant" runat="server"></asp:Label></th><th rowspan="1" colspan="1" style="width: 110px;"></th><th rowspan="1" colspan="1" style="width: 420px;text-align:right;">Total</th><th rowspan="1" colspan="1" style="width: 110px;padding-left:60px;"><asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label>
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
    <asp:HiddenField ID="hidrcptheadidno" runat="server" />
    <asp:HiddenField ID="hidmindate" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />
    <asp:HiddenField ID="hidChlnidno" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hidTrackIdno" runat="server" ClientIDMode="Static" />


    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
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
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
    <script type="text/jscript">
        $(".datepicker").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
            minDate: '<%=hidmindate.Value%>',
            maxDate: '<%=hidmaxdate.Value%>'
        });
        function PopUpTrackTruck() {
            $('#PopUpTrackTruck').fadeIn();
        }

        function IsUpdateTds(ele) {
            var newval = ele.val();
            var oldval = ele.data('lastvalue');
            var hidUpdate = ele.parent().parent().children('td:last-child').children('.clsIsUpdateTds').children();
            if (parseFloat(oldval) != parseFloat(newval)) {
                hidUpdate.val('true');
            }
            else {
                hidUpdate.val('false');
            }
        }
        function IsUpdateComm(ele) {
            var newval = ele.val();
            var oldval = ele.data('lastvalue');
            var hidUpdate = ele.parent().parent().children('td:last-child').children('.clsIsUpdateComm').children();
            if (parseFloat(oldval) != parseFloat(newval)) {
                hidUpdate.val('true');
            }
            else {
                hidUpdate.val('false');
            }
        }
    </script>
</asp:Content>
