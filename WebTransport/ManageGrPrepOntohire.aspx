﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ManageGrPrepOntohire.aspx.cs" Inherits="WebTransport.ManageGrPrepOntohire" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/U-Custom.css" rel="stylesheet" type="text/css" />
    <link href="css/tables.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="center-block" style="width: 100%; margin-top: 30px; display: block;">
        <section class="panel panel-default full_form_container part_purchase_bill_form auto-height-form" style="box-shadow: 0 0px 2px gray; border: none; margin-top: 30px">
            <!--FORM HEADER-->
            <div class="ibox-title">
                <h5><div class="printing-animation icon-size"></div>GRPREPONTOHIRE List</h5>
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
                        <a href="GrPrepOntohire.aspx"><i style="font-size: 13px !important;" class="fa fa-plus action-icon-style1"></i></a>
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
                                <asp:TextBox ID="txtReceiptDatefrom" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="50" data-date-format="dd-mm-yyyy"   ></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date To</span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:TextBox ID="txtReceiptDateto" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="50" data-date-format="dd-mm-yyyy"   ></asp:TextBox>
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
                        <asp:GridView ID="grdMain" runat="server" CssClass="table-style-white last-row-select" AutoGenerateColumns="false" BorderStyle="None" Width="100%" GridLines="None" AllowPaging="true" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand" OnRowDataBound="grdMain_RowDataBound">
                            <Columns>
                                 <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" Width="50" />
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>.
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Challan No." HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
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
                                 <asp:TemplateField HeaderText="From City" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("From_City")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Lorry No." HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
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
                                 <asp:TemplateField HeaderText="Net Amount" HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                <ItemTemplate>
                                                    <%# Convert.ToDouble(Eval("Total_Amnt")).ToString("N2")%>
                                                </ItemTemplate>
                                                <FooterStyle ForeColor="Black" Font-Bold="true" HorizontalAlign="Right" Font-Size="Small" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblNetAmnt" Text='0.00' runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" Width="50" />
                                                <ItemTemplate>
                                                      <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("Gr_Idno") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" TabIndex="6" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
