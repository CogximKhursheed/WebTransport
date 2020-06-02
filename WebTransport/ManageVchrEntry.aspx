<%@ Page Title="Manage Voucher Entry" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="ManageVchrEntry.aspx.cs" Inherits="WebTransport.ManageVchrEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/U-Custom.css" rel="stylesheet" type="text/css" />
    <link href="css/tables.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="center-block" style="width: 100%; margin-top: 30px; display: block;">
        <section class="panel panel-default full_form_container part_purchase_bill_form auto-height-form" style="box-shadow: 0 0px 2px gray; border: none; margin-top: 30px">
             <!--FORM HEADER-->
            <div class="ibox-title">
                <h5><div class="printing-animation icon-size"></div>Voucher Entry List</h5>
                <div class="pull-right title-action">
                    <div class="action-center">
                        <a href="VchrEntry.aspx"><i style="font-size: 13px !important;" class="fa fa-plus action-icon-style1"></i></a>
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
                                  <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control backlight" AutoPostBack="True" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged">  </asp:DropDownList> 
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date From</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtdatefrom" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="10"  data-date-format="dd-mm-yyyy"></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date To</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtdateto" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="10"   data-date-format="dd-mm-yyyy"></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Voucher No.</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtVchrNo" runat="server" placeholder="Enter Voucher No." CssClass="form-control backlight" MaxLength="10"></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Ledger</span>
                            </div>
                            <div class="control-holder full-width">
                                  <asp:DropDownList ID="ddlLedgrName" runat="server" CssClass="form-control backlight">      </asp:DropDownList> 
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Amount</span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:TextBox ID="txtAmnt" runat="server" placeholder="Enter Amount" CssClass="form-control backlight" MaxLength="10"  style="text-align: right;"></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Type</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:DropDownList ID="ddlVoucherType" runat="server"  CssClass="form-control backlight" >
                                <asp:ListItem Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">Payment</asp:ListItem>
                                <asp:ListItem Value="2">Receipt</asp:ListItem>
                                <asp:ListItem Value="3">Contra</asp:ListItem>
                                <asp:ListItem Value="4">Journal</asp:ListItem>
                                <asp:ListItem Value="5">Debit Note</asp:ListItem>
                                <asp:ListItem Value="6">Credit Note</asp:ListItem>
                                <asp:ListItem Value="7">EDC</asp:ListItem>
                            </asp:DropDownList>
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
                        <asp:GridView ID="grdMain" runat="server" CssClass="table-style-white last-row-select" AutoGenerateColumns="false" BorderStyle="None" Width="100%" GridLines="None" AllowPaging="true" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging"  OnRowCommand="grdMain_RowCommand" OnRowDataBound="grdMain_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                <ItemTemplate>
                                    <%#Eval("SNo")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="80px">
                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                <ItemTemplate>
                                    <%#Convert.ToDateTime(Eval("Vchr_Date")).ToString("dd-MMM-yyyy")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher No." HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="80px">
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                <ItemTemplate>
                                    <%#Eval("Vchr_No")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                <ItemTemplate>
                                    <%#Eval("Vchr_Type")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Particular" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="200px">
                                <ItemStyle HorizontalAlign="Left"  />
                                <ItemTemplate>
                                    <%#Eval("Particular")%>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />     
                                <FooterTemplate>
                                <asp:Label ID="lblttlNet" runat="server" >Grid Total</asp:Label>
                                    </FooterTemplate>  
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100px">
                                <ItemStyle HorizontalAlign="Right"  />
                                <ItemTemplate>
                                    <%# Convert.ToString(Eval("Tot_Amnt")) == "" ? Convert.ToDouble(0).ToString("N2") : Convert.ToDouble(Eval("Tot_Amnt")).ToString("N2")%>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />                                           
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalNet" runat="server" ></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <ItemTemplate>
                                  <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("Vchr_Idno") %>' CommandName="cmdedit"  ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>

                                   <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("Vchr_Idno") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                   
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
		                        <tr><th rowspan="1" colspan="1" style="width:190px;"> <asp:Label ID="lblcontant" runat="server"></asp:Label></th><th rowspan="1" colspan="1" style="width: 70px;"></th><th rowspan="1" colspan="1" style="width: 56%;text-align:right;">Total</th><th rowspan="1" colspan="1" style="width: 100px;padding-left:60px;"><asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label>
                                </th><th rowspan="1" colspan="1" style="width: 62px;"></th></tr>                                  
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
    <asp:HiddenField ID="hidacntheadid" runat="server" />
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

        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $('#<%=txtdatefrom.ClientID %>').datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
            $('#<%=txtdateto.ClientID %>').datepicker({
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
