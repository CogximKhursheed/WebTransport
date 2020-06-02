<%@ Page Title="Fuel Rate Master" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="FuelRateMaster.aspx.cs" Inherits="WebTransport.FuelRateMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/U-Custom.css" rel="stylesheet" type="text/css" />
    <link href="css/tables.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="center-block" style="width: 100%; margin-top: 30px; display: block;">
        <section class="panel panel-default full_form_container part_purchase_bill_form auto-height-form" style="box-shadow: 0 0px 2px gray; border: none; margin-top: 30px">
            <!--FORM HEADER-->
            <div class="ibox-title">
                <h5><div class="printing-animation icon-size"></div>Rate Master [Fuel] </h5>
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
                                 <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control backlight" AutoPostBack="True"  OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged"> </asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="ddlDateRange" ValidationGroup="save" ErrorMessage="Please Select Date Range." InitialValue="0" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Effective Date <span class="required-field">*</span> </span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:TextBox ID="txtDate" runat="server"   CssClass="input-sm datepicker form-control backlight" MaxLength="10" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDate" ValidationGroup="Save" ErrorMessage="Please Select Date!" CssClass="classValidation" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Pump <span class="required-field">*</span> </span>
                            </div>
                            <div class="control-holder full-width">
                                  <asp:DropDownList ID="drpPump" runat="server" CssClass="form-control backlight" OnSelectedIndexChanged="drpPump_SelectedIndexChanged"  AutoPostBack="true" > </asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpPump" ValidationGroup="Save" ErrorMessage="Please select Pump!" CssClass="classValidation"  SetFocusOnError="true" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>	                                                 
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Fuel/Rate <span class="required-field">*</span> </span>
                            </div>
                            <div class="control-holder full-width">
                                  <asp:DropDownList ID="drpFuel" runat="server" CssClass="form-control backlight" > </asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>&nbsp;</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtFuelRate"  runat="server" Text="0.00" CssClass="form-control backlight" MaxLength="6" onKeyPress="return checkfloat(event, this);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFuelRate" ValidationGroup="Save" ErrorMessage="Please enter Fuel Rate!" CssClass="classValidation" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                                <!--SECOND-FOLD-->
                                <div class="ibox-btn">
                            <div class="pull-right">
                                <div class="display-inline">
                                    <asp:LinkButton ID="lnkbtnNew" runat="server" Visible="false" OnClick="lnkbtnNew_OnClick" CssClass="btn btn-labeled btn-success"><span class="btn-label"><i class="fa fa-plus"></i></span>New</asp:LinkButton>
                                </div>
                                <div class="display-inline">
                                    <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" ValidationGroup="Save" OnClick="lnkbtnSave_OnClick" CssClass="btn btn-labeled btn-primary"><span class="btn-label"><i class="fa fa-save"></i></span>Save</asp:LinkButton>
                                </div>
                                <div class="display-inline">
                                    <asp:LinkButton ID="lnkbtnCancel" runat="server" OnClick="lnkbtnCancel_OnClick" CssClass="btn btn-labeled btn-danger"><span class="btn-label"><i class="fa fa-times"></i></span>Cancel</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                            </div>
                        </div>
                <div class="panel-in-default" id="DivGridShow" runat="server" >
                    <div class="pull-left"><asp:Label ID="lblTotalRecord" runat="server" Text=" Total Record(s):0" Style="font-size: 13px; text-transform: none;"></asp:Label></div>
                     <div class="pull-right"></div>
                     <div class="report" style="overflow-x:Auto; width:100%;">
                          <table style="width:100%">
                                       <tr>
                                       <td>
                         <asp:GridView ID="grdMain" runat="server" CssClass="table-style-white last-row-select" AutoGenerateColumns="false" BorderStyle="None" Width="100%" GridLines="None" AllowPaging="true" OnRowCommand="grdMain_RowCommand">
                             <Columns>
                                  <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="50" HeaderStyle-CssClass="gridHeaderAlignCenter">
                                            <ItemStyle HorizontalAlign="Center" Width="50" />
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>.
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Eff. Date" HeaderStyle-CssClass="gridHeaderAlignLeft" HeaderStyle-Width="50">
                                            <ItemStyle HorizontalAlign="Left" Width="50" />
                                            <ItemTemplate>
                                                <%#Convert.ToDateTime(Eval("FuelRate_Date")).ToString("dd-MMM-yyyy")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Pump Name" HeaderStyle-CssClass="gridHeaderAlignLeft" HeaderStyle-Width="50">
                                            <ItemStyle HorizontalAlign="Left" Width="50" />
                                            <ItemTemplate>
                                                <%#Convert.ToString(Eval("Acnt_Name"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>     
                                  <asp:TemplateField HeaderText="Fuel Rate" HeaderStyle-CssClass="gridHeaderAlignRight" HeaderStyle-Width="20">
                                            <ItemStyle HorizontalAlign="Right" Width="20" />
                                            <ItemTemplate>
                                                <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Fuel_Rate")))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Action" HeaderStyle-Width="15" HeaderStyle-CssClass="gridHeaderAlignLeft">
                                            <ItemStyle HorizontalAlign="Left"  Width="15" />
                                            <ItemTemplate>
                                               <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("FuelRate_Idno") %>' CommandName="cmdedit" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                 <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("FuelRate_Idno") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
		                                  <tr><th rowspan="1" colspan="1" style="width:149px;"> <asp:Label ID="lblcontant" runat="server"></asp:Label></th><th rowspan="1" colspan="1" style="width: 149px;"></th><th rowspan="1" colspan="1" style="width: 120px;text-align:right;">&nbsp;</th><th rowspan="1" colspan="1" style="width: 110px;padding-left:60px;"><asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label>
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
    <asp:HiddenField ID="hidIdno" runat="server" />
    <asp:HiddenField ID="hidmindate" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />


    <script type="text/javascript" language="javascript">

        SetFocus();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function () {
            setDatecontrol();
            SetFocus();
        });

        prm.add_endRequest(function () {
            setDatecontrol();
            SetFocus();
        });


        $(document).ready(function () {
            setDatecontrol();
            SetFocus();
            $('#<%=drpPump.ClientID %>').focus();
        });
        function setDatecontrol() {

            $('#<%=txtDate.ClientID %>').datepicker({
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

    </script>
</asp:Content>
