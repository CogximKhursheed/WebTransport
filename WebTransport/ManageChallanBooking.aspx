<%@ Page Title="Manage Challan Booking" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="ManageChallanBooking.aspx.cs" EnableEventValidation="false"
    Inherits="WebTransport.ManageChallanBooking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/U-Custom.css" rel="stylesheet" type="text/css" />
    <link href="css/tables.css" rel="stylesheet" type="text/css" />

    <style>
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
                <h5><div class="printing-animation icon-size"></div>Challan Booking List </h5>
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
                                  <asp:TextBox ID="txtReceiptDatefrom" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="50" data-date-format="dd-mm-yyyy"   ></asp:TextBox>
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
                                <asp:TextBox ID="txtReceiptNo" runat="server" CssClass="form-control backlight" MaxLength="50" ></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Loc.[From]</span>
                            </div>
                            <div class="control-holder full-width">
                                   <asp:DropDownList ID="drpCityFrom" runat="server"  CssClass="form-control backlight" > </asp:DropDownList>
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
                                  <asp:DropDownList ID="ddlTranstype" runat="server" Enabled="false" AutoPostBack="true"  CssClass="form-control backlight" OnSelectedIndexChanged="ddlTranstype_SelectedIndexChanged" >      </asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>
                                    <asp:Label ID="lblTruckNo" runat="server" Text="" ></asp:Label>
                                </span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:DropDownList ID="ddltruckNo" runat="server"  CssClass="chzn-select" visible="false" > </asp:DropDownList>
                                 <asp:TextBox ID="txttruck" runat="server" CssClass="form-control auto-extender" onkeyup="SetContextKey()" onkeydown="return (event.keyCode!=13);" ></asp:TextBox>
                                 <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txttruck" MinimumPrefixLength="1" UseContextKey="false" EnableCaching="true" CompletionSetCount="1" CompletionInterval="500" OnClientItemSelected="ClientItemSelected" ServiceMethod="GetTruckNo">     </asp:AutoCompleteExtender>
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
                                                    <%#Eval("FromCity")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                                  <asp:TemplateField HeaderText="Tran. Type" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("TransportType")%>
                                                </ItemTemplate>
                                                <FooterStyle ForeColor="Black" Font-Bold="true" HorizontalAlign="Left" Font-Size="Small" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTranType" Text='' runat="server"></asp:Label>
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
                                                    <asp:Label ID="lblNetAmnt" Text='0.00' runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" Width="50" />
                                                <ItemTemplate>
                                                 <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("Chln_Idno")+"-"+Eval("Gr_Type") %>' CommandName="cmdedit" TabIndex="5" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                 <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("Chln_Idno") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" TabIndex="6" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                 <asp:LinkButton ID="lnkTruckLoc" CssClass="addTruckLoc" runat="server" CommandArgument='<%#Eval("Chln_Idno") %>' CommandName="cmdAddTruckLoc" TabIndex="6" ToolTip="Click to add truck location"><i class="fa fa-truck"></i></asp:LinkButton>
                                                <%-- <a id="lnkTracking" data-chlnid='<%#Eval("Chln_Idno") %>' title="Click to add truck location"><i class="fa fa-truck"></i></a>--%>
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
    <asp:HiddenField ID="hfTruckNoId" runat="server" />
    <asp:HiddenField ID="hidrcptheadidno" runat="server" />
    <asp:HiddenField ID="hidmindate" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />
    <asp:HiddenField ID="hidChlnidno" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hidTrackIdno" runat="server" ClientIDMode="Static" />


    <div id="PopUpTrackTruck" class="clsPopUpTrackTruck modal fade in" style="background: #00000080;">
        <div class="modal-dialog col-sm-6 center-block" style="margin-top: 20px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="popform_header"><i class="fa fa-truck"></i>Lorry Tracking <i class="fa fa-close pull-right" onclick="clear_form_elements('clsPopUpTrackTruck');$('#PopUpTrackTruck').fadeOut();"></i></h4>
                </div>
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
						<div class="panel-body">  
                            <span class="heading-text">Challan Details</span>
                            <div class="col-sm-4">
                                <label class="col-sm-12 label-heading">Challan No.</label>
                                <label><asp:Label ID="lblChlnNo" CssClass="control-label col-sm-12 label-value"  runat="server"> </asp:Label></label>
                            </div>
                            <div class="col-sm-4">
                                <label class="col-sm-12 label-heading">Challan Date</label>
                                <label><asp:Label ID="lblChlnDate" CssClass="control-label col-sm-12 label-value"  runat="server"> </asp:Label></label>
                            </div>
                            <div class="col-sm-4">
                                <label class="col-sm-12 label-heading">Lorry No.</label>
                                <label><asp:Label ID="lblLorryno" CssClass="control-label col-sm-12 label-value"  runat="server"> </asp:Label></label>
                            </div>
                            <div class="col-sm-4">
                                <label class="col-sm-12 label-heading">Party Name</label>
                                <label><asp:Label ID="lblPartyName" CssClass="control-label col-sm-12 label-value"  runat="server"> </asp:Label></label>
                            </div>
                            <div class="col-sm-4">
                                <label class="col-sm-12 label-heading">Loc. From</label>
                                <label><asp:Label ID="lblLocFrm" CssClass="control-label col-sm-12 label-value"  runat="server"> </asp:Label></label>
                            </div>
                            <div class="col-sm-4">
                                <label class="col-sm-12 label-heading">Loc. To</label>
                                <label><asp:Label ID="lblLocTo" CssClass="control-label col-sm-12 label-value"  runat="server"> </asp:Label></label>
                            </div>
                            <span class="heading-text">Add new entry</span>
                            <div class="col-sm-6">
                                <label class="col-sm-12">Truck location</label>
                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                             <div class="col-sm-2">
                                <label class="col-sm-12">Date</label>
                                <asp:TextBox ID="txtTruckCurrDate" runat="server" CssClass="input-sm datepicker form-control" MaxLength="50" data-date-format="dd-mm-yyyy" TabIndex="3"  ></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                <label class="col-sm-12">Time</label>
                                <asp:TextBox ID="txtTruckCurrTime" CssClass="form-control col-sm-12" runat="server" type="time" value="00:00" /></asp:TextBox>
                                <%--<asp:TextBox ID="txtTruckCurrTime" CssClass="form-control col-sm-12" runat="server"></asp:TextBox>--%>
                            </div>
                            <div class="col-sm-1">
                                <label class="col-sm-12">SMS</label>
                                <asp:CheckBox ID="chkTrackingSMS" runat="server"></asp:CheckBox>
                            </div>
                              <div class="col-sm-12">
                            <hr />
                            <div class="pull-right">
                                <asp:LinkButton ID="lnkSave" CssClass="btn btn-sm btn-primary" OnClick="AddTrackingLocation" runat="server">Add</asp:LinkButton>
                               <%-- <a id="lnkSave" class="btn btn-sm btn-primary" >Save</a>--%>
                                <a id="lnkCancel" class="btn btn-sm btn-primary" onclick="clear_form_elements('clsPopUpTrackTruck');$('#PopUpTrackTruck').fadeOut();">Cancel</a>
                            </div>
                            </div>
                            <span class="heading-text">Truck Previous locations</span>
                            <div class="fixed-grid">
		                        <asp:GridView ID="grdTruckPrevLoc" runat="server" AutoGenerateColumns="false" Width="100%"  AllowPaging="true" PageSize="10"
                                BorderWidth="1"  BorderStyle="Solid" GridLines="None" TabIndex="4" CssClass="display nowrap dataTable"
                                ShowFooter="true" OnRowCommand="grdTruckPrevLoc_RowCommand">
                                     <RowStyle CssClass="odd" />
                                        <AlternatingRowStyle CssClass="even" />                                       
                                       <PagerStyle  CssClass="classPager" />
                                         <PagerSettings Mode="NumericFirstLast" PageButtonCount="5"  FirstPageText="First" Position="Bottom" LastPageText="Last"/>   
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="20" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                                <ItemStyle HorizontalAlign="Left" Width="50" />
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>.
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tracking Id" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                                <ItemStyle HorizontalAlign="Left" Width="50" />
                                                <ItemTemplate>
                                                    <%#Eval("LTrackLoc_Idno") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="City Name" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                                <ItemStyle HorizontalAlign="Left" Width="50" />
                                                <ItemTemplate>
                                                    <%#Eval("City_Name") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                                <ItemStyle HorizontalAlign="Left" Width="50" />
                                                <ItemTemplate>
                                                    <%#Convert.ToDateTime(Eval("Track_Date")).ToString("dd/MM/yyyy") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SMS Sent" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                                <ItemStyle HorizontalAlign="Left" Width="50" />
                                                <ItemTemplate>
                                                    <i class='mask-sms Invert-<%#Eval("SMS_Sent") %>'></i>
                                                    <asp:LinkButton ID="lnkSendSMS"  CommandArgument='<%#Eval("LTrackLoc_Idno")%>' CommandName="cmdSendSMS" runat="server"><i class="fa fa-envelope"> </i></asp:LinkButton> 
                                                    <asp:CheckBox ID="chkGrdSMSSent" Checked='<%#Eval("SMS_Sent") %>' Enabled='false' runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                                <ItemStyle HorizontalAlign="Left" Width="50" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEditTracking" CommandArgument='<%#Eval("LTrackLoc_Idno")%>' CommandName="cmdedit" runat="server"><i class="fa fa-pencil-square-o"> </i> </asp:LinkButton>
                                                    <asp:LinkButton ID="lnkDeleteTracking" CommandArgument='<%#Eval("LTrackLoc_Idno")%>' CommandName="cmddelete" runat="server"><i class="fa fa-trash"> </i></asp:LinkButton> 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                        <EmptyDataTemplate>
                                            <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                        </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                          
                        </div>
                    </section>
                </div>
            </div>
        </div>
    </div>
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
    <script>
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
        //CLEAR ALL FIELDS WITHIN SPECIFIC CLASS
        function clear_form_elements(class_name) {
            jQuery("." + class_name).find(':input').each(function () {
                switch (this.type) {
                    case 'password':
                    case 'text':
                    case 'textarea':
                    case 'file':
                    case 'select-one':
                    case 'select-multiple':
                    case 'date':
                    case 'number':
                    case 'tel':
                    case 'email':
                        jQuery(this).val('');
                        break;
                    case 'checkbox':
                    case 'radio':
                        this.checked = false;
                        break;
                }
            });
        }
    </script>
    <script type="text/javascript">
        function ClientItemSelected(sender, e) {
            $get("<%=hfTruckNoId.ClientID %>").value = e.get_value();
        }
    </script>
</asp:Content>
