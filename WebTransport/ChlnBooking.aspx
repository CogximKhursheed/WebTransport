<%@ Page Title="Challan Booking" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="ChlnBooking.aspx.cs" Inherits="WebTransport.ChlnBooking" %>
      <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = "";
            var Pages = "1";
            var type = "1";
            Pages = document.getElementById("<%=ddlPages.ClientID%>").value;
            type = document.getElementById("<%=ddlPrintType.ClientID%>").value;
            if (type == 2) {
                var prtContent3 = "<p style='page-break-before: always'></p>";
                for (i = 0; i < Pages; i++) {
                    prtContent = prtContent + "<table width='100%' border='0'></table>";
                    if (Pages != 1) {
                        prtContent = prtContent + "<tr><td><strong>" + ((i == 1) ? "[HO Copy]" : (i == 2) ? "[Extra Copy]" : "[Driver Copy]") + "</strong> <a style='float:right'> " + GetDateTime() + "</a></td></tr>";
                    }
                    else if (Pages == 1) {
                        prtContent = prtContent + "<tr><td><a style='float:right'> " + GetDateTime() + "</a></td></tr>";
                    }
                    var prtContent1 = document.getElementById(strid);
                    var prtContent2 = prtContent1.innerHTML;
                    prtContent = prtContent + prtContent2 + ((i < 3) ? prtContent3 : "");
                }
                var WinPrint = window.open('', '', 'left=0,top=0,width=800,height=800,toolbar=1,scrollbars=1,status=1');
                WinPrint.document.write(prtContent);
                WinPrint.document.close();
                WinPrint.focus();
                WinPrint.print();
                return false;
            }
            else if (type == 1) {
                var prtContent3 = "<p></p>";
                for (i = 0; i < Pages; i++) {
                    prtContent = prtContent + "<table width='100%' border='0'></table>";
                    if (Pages != 1) {
                        prtContent = prtContent + "<tr><td><strong>" + ((i == 1) ? "[HO Copy]" : (i == 2) ? "[Extra Copy]" : "[Driver Copy]") + "</strong> <a style='float:right'> " + GetDateTime() + "</a></td></tr>";
                    }
                    else if (Pages == 1) {
                        prtContent = prtContent + "<tr><td><a style='float:right'> " + GetDateTime() + "</a></td></tr>";
                    }
                    var prtContent1 = document.getElementById(strid);
                    var prtContent2 = prtContent1.innerHTML;
                    prtContent = prtContent + prtContent2 + ((i < 3) ? prtContent3 : "");
                }
                var WinPrint = window.open('', '', 'left=0,top=0,width=800,height=800,toolbar=1,scrollbars=1,status=1');
                WinPrint.document.write(prtContent);
                WinPrint.document.close();
                WinPrint.focus();
                WinPrint.print();
                return false;
            }
        }

    </script>
    <script language="javascript" type="text/javascript">
        function CallPrint1(strid) {
            var hide = document.getElementById();
            $("#hide").hide();
            var prtContent1 = "<table width='100%' border='0'></table>";
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=800,height=800,toolbar=1,scrollbars=1,status=1');
            WinPrint.document.write(prtContent1 + "<a style='float:right'>" + GetDateTime() + "</a>");
            WinPrint.document.write(prtContent.innerHTML);
            //WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            //WinPrint.close();
            return false;
        }

    </script>
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
            $("#<%=txtDate.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });

            $("#<%=txtDateFrom.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
            $("#<%=txtDateTo.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
            $("#<%=txtInstDate.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
            $("#<%=txtDlyDate.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
        }

        function openModal() {
            $('#dvGrdetails').modal('show');
        }

        function openFreightDetail() {
            $('#dvFreight').modal('show');
        }
        function openfuelDetail() {
            $('#dvfuel').modal('show');
        }
        function showPrtyBal() {
            $('#divPrtyBlance').modal('show');
        }

        function CloseModal() {
            $('#dvGrdetails').Hide();
        }
        function CloseUpload() {
            $('#Upload_div').Hide();
        }
        function cityviaddl() {
            var id = document.getElementById("<%=ddlDivTocity.ClientID %>").value;
            document.getElementById("<%=ddlViaCity.ClientID %>").value = id;
        }


        function CloseExcelDiv() {
            $('#Excel_Upload').modal('hide');
        }

        function HideBillAgainst() {
            $("#dvGrdetails").fadeOut(300);
        }

        function ShowClient() {
            $("#dvGrdetails").fadeIn(300);
        }
        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox

            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
    spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
          elm[i].id != theBox.id) {
                    //elm[i].click();

                    if (elm[i].checked != xState)
                        elm[i].click();
                    //elm[i].checked=xState;

                }
    }
     
    </script>
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-10">
            <div runat="server" id="PrintLastSavedChln" visible="" style="background: #65c9bf;
                padding: 10px;">
                <b><i style="color: #f9e36b; font-size: 14px;" class="fa fa-info-circle"></i>Do you
                    want to print last saved Challan? Please click on print. </b>
                <asp:Button ID="btnPrintLastSavedGR" CssClass="pull-right" Style="border: 1px solid silver;
                    border-radius: 4px; background: #f6f6f6;" runat="server" Text="Print" OnClick="PrintLastSaved_Click" />
                <asp:DropDownList ID="ddlPagesLastSaved" Style="width: 100px; font-size: 12px; height: 20px;
                    float: right;" runat="server" CssClass="form-control">
                    <asp:ListItem Text="3 Pages" Value="3" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="2 Pages" Value="2"></asp:ListItem>
                    <asp:ListItem Text="1 Page" Value="1"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <section class="panel panel-default full_form_container quotation_master_form">
            <header class="panel-heading font-bold form_heading">CHALLAN BOOKING
            <span class="view_print"><a href="ManageChallanBooking.aspx" tabindex="25">     <asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a>
                &nbsp;
                <asp:LinkButton ID="lnkBtnLast" class="view_print" runat="server"  AlternateText="Print" title="Print" Height="16px" onclick="lnkBtnLast_Click" Visible="false">DRIVER VOUCHER</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
              <a id="lastprint" TabIndex="18" runat="server" href="#Amount"  data-toggle="modal" data-target="#Amount"><i class="fa fa-print icon"></i></a> 
             <asp:LinkButton ID="lnkbtnprintOM" 
                class="btn btn-sm btn-primary" runat="server"  TabIndex="45" 
                onclick="lnkbtnprintOM_Click" ><i class="fa fa-print icon"></i></asp:LinkButton>
                                    
              </span>
            </header>
                <div class="panel-body">
                  <form class="bs-example form-horizontal">
                    <!-- first  section --> 
                    <div class="clearfix first_section">
                      <section class="panel panel-in-default">  
                        <div class="panel-body">
                        	<div class="clearfix odd_row">
                            <div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 29%;">Date Range<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 71%;">
                               <asp:DropDownList ID="ddldateRange" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="1" OnSelectedIndexChanged="ddldateRange_SelectedIndexChanged">
                               </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddldateRange"
                                CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Year!" InitialValue="0"
                                SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                             
                              </div>
                            </div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 20%;">Date<span class="required-field">*</span></label>
                              <div class="col-sm-4" style="width: 40%;">
                               <asp:TextBox ID="txtDate" runat="server" PlaceHolder="DD-MM-YYYY" CssClass="input-sm datepicker form-control"  MaxLength="50" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                TabIndex="2" onkeydown = "return DateFormat(this, event.keyCode)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Date!" SetFocusOnError="true"
                                ValidationGroup="save"></asp:RequiredFieldValidator>
                            <%-- <asp:RangeValidator ID="RangeValidator1" CssClass="classValidation" ValidationGroup="save" runat="server" ErrorMessage="Date Is Invalid" Type="Date" ControlToValidate="txtDate" 
                                      Display="Dynamic" SetFocusOnError="true" ForeColor="Red"></asp:RangeValidator>--%>
                              </div>
                              <div class="col-sm-3" style="width: 40%;">
                               <asp:TextBox ID="txtchallanNo" runat="server" CssClass="form-control" MaxLength="7" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"  TabIndex="3" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtchallanNo"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter challan no!"
                                    SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>
                           	</div>
                           	<div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 27%;">Loc.[From]<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 63%;">
                                <asp:DropDownList ID="ddlFromCity" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="4" OnSelectedIndexChanged="ddlFromCity_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlFromCity"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select From city!" InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                            
                              </div>
                              <div class="col-sm-1" style="width: 10%;">
                               <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/Images/PckLst.png" AlternateText="Search"
                                                        ImageAlign="ABSMiddle" ToolTip="Search" Style="height: 28px" OnClick="imgSearch_Click"
                                                        TabIndex="5" />
                              </div>
                            </div>
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-4">
                            <div class="col-sm-3" style="width: 29%;">
                            <asp:Label ID="Lorrytype" runat="server" Text="Truck No." ></asp:Label>
                            </div>
                              <%--<label class="col-sm-3 control-label" ></label>--%>
                              <div class="col-sm-9" style="width: 61%;">
                                <asp:DropDownList ID="ddlTruckNo" Enabled="false" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="6" OnSelectedIndexChanged="ddlTruckNo_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTruckNo"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Truck No!" InitialValue="0"  SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>
                              <div  class="col-sm-1" style="width: 10%;">
                                <asp:LinkButton ID="lnkbtnPrtyBlance" runat="server" CssClass="btn-sm btn btn-primary acc_home" Visible="false" ToolTip="Party Balance" OnClick="lnkbtnPrtyBlance_OnClick"><i class="fa fa-book"></i></asp:LinkButton>
                              </div>
                            </div>
                           	<div class="col-sm-4">
                           		<label class="col-sm-3 control-label" style="width: 20%;">Owner<span class="required-field">*</span></label>
                              <div class="col-sm-8" style="width: 80%;">
                                <asp:TextBox ID="txtOwnrNme" runat="server" CssClass="form-control" MaxLength="50"  oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                        TabIndex="7"  Enabled="false"></asp:TextBox>
                              <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtOwnrNme"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter owner name!"  SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                        --%>
                              </div>
                           	</div>
                           	<div class="col-sm-4">
                              <label class="col-sm-3 control-label" style="width: 27%;">Driver<span class="required-field">*</span></label>
					            <div class="col-sm-8" style="width: 63%;">
                                <asp:DropDownList ID="ddldriverName" runat="server" CssClass="chzn-select" style="width:100%;"  TabIndex="8" >
                        </asp:DropDownList>
                           <asp:RequiredFieldValidator ID="rfvDriver" runat="server" ControlToValidate="ddldriverName" InitialValue="0"
                            CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select driver!" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                        
					            </div>
					            <div class="col-sm-1" style="width: 10%;">
                                                        
                                <asp:LinkButton ID="lnkbtnDriverRefresh" runat="server" CssClass="btn-sm btn btn-primary acc_home" TabIndex="9" ToolTip="Update Driver" OnClick="lnkbtnDriverRefresh_OnClick"><i class="fa fa-refresh"></i></asp:LinkButton>
                              </div>
                            </div>
                          </div>
                          <div class="clearfix odd_row">
                          <div class="col-sm-4">
                          <div class="col-sm-9">
                           <asp:Label runat="server" ID="Label7" CssClass="col-sm-3 control-label" >M. No.</asp:Label>
                           
                              <div class="col-sm-9" >
                                 <asp:TextBox ID="txtMno" MaxLength="8" runat="server" ></asp:TextBox>
                              </div>
                              </div>

                              <div class="col-sm-3">
                              <asp:ImageButton ID="imgbtnPopup" runat="server" ImageUrl="~/Images/PckLst.png" AlternateText="Search"
                                                        ImageAlign="ABSMiddle" ToolTip="Search" Style="height: 28px" OnClick="imgbtnPopup_Click"
                                                        TabIndex="5" />
                              </div>
                          </div>
                          <div class="col-sm-4">
                           <asp:Label runat="server" ID="Label2" CssClass="col-sm-3 control-label" style="width: 29%;">KMs<span class="required-field">*</span></asp:Label>
                           
                              <div class="col-sm-9" style="width: 71%;">
                                 <asp:TextBox ID="txtKms" runat="server" Enabled="false" style="text-align:right;"></asp:TextBox>
                              </div>
                          </div>
                  <%--        <div class="col-sm-4">
                          <asp:Label runat="server" ID="Label5" CssClass="col-sm-3 control-label" style="width: 29%;">Amount<span class="required-field">*</span></asp:Label>
                           
                              <div class="col-sm-9" style="width: 71%;">
                                <asp:DropDownList ID="ddlamount" AutoPostBack="true" runat="server" CssClass="form-control"  
                                      TabIndex="8" onselectedindexchanged="ddlamount_SelectedIndexChanged" >
                                 <asp:ListItem Text="WithAmount" Value="WithAmount" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="WithoutAmount" Value="WithoutAmount" ></asp:ListItem>
                                </asp:DropDownList>
                              </div>
                          </div>--%>
                            <div class="col-sm-4">

                                <asp:Label  runat="server" ID="lblHireAmnt" Visible="false" CssClass="col-sm-3 control-label" style="width: 29%;">Hire Amount<span class="required-field">*</span></asp:Label>
                                <asp:Label runat="server" ID="lblDelvPlace" CssClass="col-sm-3 control-label" style="width: 29%;">Delv. Place<span class="required-field">*</span></asp:Label>
                           
                              <div class="col-sm-9" style="width: 71%;">
                                 <asp:DropDownList ID="ddlDelvryPlace" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="10" >
                                 </asp:DropDownList>
                             <asp:TextBox ID="txtHireAmnt"  Visible="false" style="width: 81%;" align="Right" runat="server" onkeyup="MoveValue()"  TabIndex="9" CssClass="form-control" MaxLength="11" oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorHireAmnt" runat="server" ControlToValidate="txtHireAmnt" Enabled="false"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Hire Amnt no!"
                                    SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                              </div>
                            </div>
                            </div>
                        </div>
                      </section>                        
                    </div>                    
                    <!-- second  section -->
                    <div class="clearfix third_right">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body" style="   overflow-x: auto;">     
                         <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="display nowrap dataTable"
                                BorderStyle="Solid"   GridLines="Both" BorderWidth="1"   ShowFooter="true" OnDataBound="grdMain_DataBound" OnRowDataBound="grdMain_RowDataBound">
                               <RowStyle CssClass="odd" />
                                    <AlternatingRowStyle CssClass="even" />   
                                <Columns>
                                    <asp:TemplateField HeaderText="GR No./ Inv No." HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="100" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrNo" runat="server" Text='<%#Convert.ToString(Eval("Gr_No")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GR Date" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="50" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrdate" runat="server" Text='<%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MM-yyyy") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Receiver Name" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("Recvr_Name"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sender Name" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("Sender_Name"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To City" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("To_City"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delv. Place" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("Delvplc_Name"))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" Text="Total :" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Via City" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("CityVia_Name"))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <FooterTemplate>
                                            
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("Qty"))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotQty" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weight" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <%#Convert.ToDouble(Eval("Tot_Weght")).ToString()%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotWeigh" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <%#Convert.ToDouble(Eval("Rate")).ToString()%>
                                        </ItemTemplate>                                                                              
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <%#Convert.ToDouble(Eval("WithoutUnloading_Amnt")).ToString("N2")%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblWithoutUnloadingAmnt" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Wages Amnt" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <%#Convert.ToDouble(Eval("Wages_Amnt")).ToString("N2")%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblWagesAmnt" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amount" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <%#Convert.ToDouble(Eval("Amount")).ToString("N2")%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblNetAmnt" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("Remark"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SubTotAmount" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right"
                                        Visible="false">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#Convert.ToDouble(Eval("SubTot_Amnt")).ToString("N2")%>'
                                                ID="lblSubTotAmnt"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblnorecord" runat="server" Text="No record found"></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>

                        </div>
                      </section>
                    </div> 

                    <div class="clearfix">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body">  
                        	<div class="clearfix odd_row">
                            <div class="col-sm-3">
                              <label class="col-sm-5 control-label">Adv. Amnt</label>
                              <div class="col-sm-7">
                              <asp:TextBox ID="txtAdvAmnt" ClientIDMode="Static" runat="server" AutoPostBack="false" CssClass="form-control" TabIndex="11"
                                MaxLength="9" oncopy="return false" oncut="return false" onDrop="blur();return false;" 
                                onkeydown="return (event.keyCode!=45);" onKeyPress="return checkfloat(event, this);" style="text-align:right;"
                                onpaste="return false" OnTextChanged="txtAdvAmnt_TextChanged" onchange="javascript:CalcAmnt();"   Text="0.00"></asp:TextBox>
                              </div>
                            </div>
                            <div class="col-sm-3">
                              <label class="col-sm-5 control-label">Pay. Type</label>
                              <div class="col-sm-7">
                                <asp:DropDownList ID="ddlRcptType" ClientIDMode="Static" Enabled="false" runat="server" CssClass="form-control" onchange="javascript:OpenText();"  AutoPostBack="false" TabIndex="12" OnSelectedIndexChanged="ddlRcptType_SelectedIndexChanged">
                               </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvRcptType" runat="server" ControlToValidate="ddlRcptType"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="save"  ErrorMessage="Select Receipt Type" CssClass="classValidation"></asp:RequiredFieldValidator>
                           
                              </div>
                            </div>
                           	<div class="col-sm-3">
                           		<label class="col-sm-4 control-label">Inst.Detail</label>
                           		<div class="col-sm-3">
                                   <asp:TextBox ID="txtInstNo" runat="server" CssClass="form-control" MaxLength="6"  Style="text-align: right;" TabIndex="13"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvinstno" runat="server" ControlToValidate="txtInstNo"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="" ValidationGroup="save"
                                    ErrorMessage="Enter Inst. No" CssClass="classValidation"></asp:RequiredFieldValidator>
                              </div>
                              <div class="col-sm-5">
                                <asp:TextBox ID="txtInstDate" runat="server" CssClass="input-sm datepicker form-control" TabIndex="14" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvinstDate" runat="server" ControlToValidate="txtInstDate"
                                    Display="Dynamic" SetFocusOnError="true" InitialValue="" ValidationGroup="save"
                                    ErrorMessage="Enter Inst. Date!" CssClass="classValidation"></asp:RequiredFieldValidator>
                             
                              </div>
                           	</div>
                           	<div class="col-sm-3">
                              <label class="col-sm-5 control-label">Cust. Bank<span class="required-field">*</span></label>
                              <div class="col-sm-7">
                               <asp:DropDownList ID="ddlCusBank" runat="server" CssClass="form-control" TabIndex="15">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCusBank" runat="server" ControlToValidate="ddlCusBank"
                                Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="save"
                                ErrorMessage="Select Cust. Bank!" CssClass="classValidation"></asp:RequiredFieldValidator>                                      
                              </div>
                            </div>
                          </div>   
                          <div class="clearfix odd_row">
                            <div class="col-lg-6">
                            	<div class="clearfix even_row">
                                <label class="col-sm-2 control-label">Delv. Inst. / Particular</label>
                                <div class="col-sm-10">
                                  <asp:TextBox ID="txtDelvInstruction" runat="server" CssClass="form-control" Height="64px" MaxLength="200" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                     onpaste="return false" Style="resize: none;" TabIndex="16"   TextMode="MultiLine"></asp:TextBox>
                                	
                                </div>
                                <%--<div class="col-sm-6">
                                  <label class="col-sm-4 control-label">Diesel Account<span class="required-field">*</span></label>
	                                <div class="col-sm-8">
                                       <asp:DropDownList ID="ddlAcntLink" runat="server" CssClass="form-control" >
                                       </asp:DropDownList>
	                                </div>
                                </div>--%>
                                 <div class="col-sm-3">
                                  <asp:LinkButton ID="fuelpopup" runat="server"  class="btn btn-sm btn-primary acc_home" ToolTip="ADD FUEL"  
                                   data-toggle="modal"  data-target="#dvfuel" TabIndex="5"><img src="Images/plus.gif" style="width:15px;" /></asp:LinkButton>
                              </div>
                              </div>
                            </div>
                            <div class="col-lg-6">
                            	<div class="clearfix even_row">
	                              <div class="col-sm-6">
	                                <label class="col-sm-6 control-label">Gross Amount</label>
	                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtGrosstotal" runat="server" CssClass="form-control" Enabled="false"   MaxLength="50" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                     onpaste="return false" Style="text-align:right;"  TabIndex="17" Text="0.00"></asp:TextBox>
	                       
	                                </div>
	                              </div>
	                             	<div class="col-sm-6">
	                                <label class="col-sm-6 control-label">B. C.</label>
	                                <div class="col-sm-6">
                                     <asp:TextBox ID="txtcommission" runat="server" AutoPostBack="false" CssClass="form-control"  MaxLength="9" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                        onpaste="return false" OnTextChanged="txtcommission_TextChanged" onchange="javascript:CalcAmnt();"  Style="text-align:right;"
                                        TabIndex="18" Text="0.00"></asp:TextBox>
	                               
	                                </div>
	                              </div>			                              
	                            </div>
	                            <div class="clearfix even_row">
	                              <div class="col-sm-6">
	                                <label class="col-sm-6 control-label">TDS Amount</label>
	                                <div class="col-sm-4">
                                      <asp:TextBox ID="txtTdsAmnt" runat="server" CssClass="form-control" Enabled="false"   MaxLength="50" oncopy="return false" AutoPostBack="false" oncut="return false"
                                        onDrop="blur();return false;" onpaste="return false" Style="text-align:right;" TabIndex="19"
                                        Text="0.00" OnTextChanged="txtTdsAmnt_TextChanged" onchange="javascript:CalcAmnt();" ></asp:TextBox>
                                        </div>
                                         <div class="col-sm-2">
                                         <asp:LinkButton ID="LnkTDSUpdt" runat="server" Enabled="false" TabIndex="20"
                                                     CssClass="btn-sm btn btn-primary acc_home" ToolTip="Update TDS Amount" 
                                                     onclick="LnkTDSUpdt_Click" ><i class="fa fa-refresh"></i></asp:LinkButton>
	                                	</div>
	                                
	                              </div>

                                <div class="col-sm-6">
                                  <label class="col-sm-6 control-label">Diesel Amount</label>
	                                <div class="col-sm-6">
                                     <asp:TextBox ID="txtDieselAmnt" runat="server" AutoPostBack="false"  OnTextChanged="txtDieselAmnt_TextChanged" onchange="javascript:CalcAmnt();" CssClass="form-control"  MaxLength="10" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                        onpaste="return false" Style="text-align:right;" Text="0.00"></asp:TextBox>
	                               
	                                </div>
                                    </div>
	                             	
	                            </div>
                                  <div class="clearfix even_row">
	                              <div class="col-sm-6">
	                                <label class="col-sm-6 control-label">Rassa Tirpal</label>
	                                <div class="col-sm-6">
                                      <asp:TextBox ID="txtRassaTripal" runat="server" CssClass="form-control"    MaxLength="10" onchange="javascript:CalcAmnt();" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                             onpaste="return false" Style="text-align:right;" TabIndex="21" Text="0.00"></asp:TextBox>
	                               
	                                </div>
	                              </div>	
                                  <div class="col-sm-6">
	                                <label class="col-sm-6 control-label">Net Amount</label>
	                                <div class="col-sm-6">
                                      <asp:TextBox ID="txtNetAmnt" runat="server" CssClass="form-control"  ReadOnly="true"  MaxLength="50" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                                             onpaste="return false" Style="text-align:right;" TabIndex="21" Text="0.00"></asp:TextBox>
	                               
	                                </div>
	                              </div>
                            </div>
                          </div> 
                        </div>
                      </section>
                    </div>
                     <!-- fourth row -->
                    <div class="clearfix fourth_right">
                      <section class="panel panel-in-default btns_without_border">                            
                        <div class="panel-body">     
                          <div class="clearfix odd_row">
                              	<div class="col-sm-2">                                                         
                                <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false" Visible="false" CssClass="btn full_width_btn btn-s-md btn-info" OnClick="lnkbtnNew_OnClick" TabIndex="25"><i class="fa fa-file-o"></i>New</asp:LinkButton>                                                            	
								</div>                                  
								<div class="col-sm-2">
                                <asp:HiddenField ID="hidIsUnassignedLorry" runat="server" Value="" />
                                <asp:HiddenField ID="hidid" runat="server" Value="" />
                                <asp:HiddenField ID="Hidrowid" runat="server" Value="" />
                                <asp:HiddenField ID="HidGrType" runat="server" Value="" />
                                <asp:HiddenField ID="HidGrId" runat="server" Value="" />
                                <asp:HiddenField ID="HidInvoiceTyp" runat="server" />
                                <asp:HiddenField ID="hidmindate" runat="server" />
                                <asp:HiddenField ID="hidmaxdate" runat="server" />
                                <asp:HiddenField ID="hidWorkType" runat="server" />
                                <asp:HiddenField ID="hidpostingmsg" runat="server" />
                                <asp:HiddenField ID="hidOwnerId" runat="server" />
                                <asp:HiddenField ID="hidePanNo" runat="server" />
                                <asp:HiddenField ID="hideStateId" runat="server" />
                                <asp:HiddenField ID="hidTdsTaxPer" runat="server" />
                              <asp:HiddenField ID="hidtrantype" runat="server" />
                                <asp:LinkButton ID="lnkbtnSave" runat="server"  OnClientClick="return confirm('Do you want to save this record with ADVANCE AMOUNT : Rs. '+ $('#txtAdvAmnt').val() +' and PAYMENT TYPE ' + $('#ddlRcptType option:selected').text() + ' ?')" CausesValidation="true" ValidationGroup="save" TabIndex="22" CssClass="btn full_width_btn btn-s-md btn-success" OnClick="lnkbtnSave_OnClick" ><i class="fa fa-save"></i>Save</asp:LinkButton>                      
								</div>
								<div class="col-sm-2">
                                <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" TabIndex="23" CssClass="btn full_width_btn btn-s-md btn-danger" OnClick="lnkbtnCancel_OnClick" ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
								</div>
                                <div class="col-sm-2" id= "divPosting" runat="server">
                                    <button type="button" id="btnAccPost" runat="server" class="btn full_width_btn btn-s-md btn-info" style="height:32px" data-toggle="modal" data-target="#acc_posting"><i class="fa fa-th-list">Acc Posting</i></button>
                                </div>
                                <div class="col-sm-2" id= "divPrtyPmnt" visible="false" runat="server">
                                    <asp:LinkButton ID="lnkbtnPrtyPmnt" runat="server" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-info" OnClick="lnkbtnPrtyPmnt_OnClick" ><i class="fa fa-fast-forward"></i>Pay Agnt. Chln</asp:LinkButton>
                                </div>
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="lnkbtnExcel" runat="server" TabIndex="24"
                                        CssClass="btn full_width_btn btn-s-md btn-info" data-toggle="modal" data-target="#Excel_Upload"><i CausesValidation="true" class="fa fa-upload"></i>Excel Upload</asp:LinkButton>
                                </div>
                                <div  class="col-sm-1"></div>
                            </div>
                        </div>
                      </section>
                      <div id="Upload_div" class="modal fade" role="dialog">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h4 class="popform_header">
                                            Upload Excel</h4>
                                    </div>
                                    <div class="modal-body">
                                    <section class="panel panel-default full_form_container material_search_pop_form">
								            <div class="panel-body">
									            <!-- First Row start -->
								            <div class="clearfix odd_row">	       
                                            <div class="col-sm-2">
	                                            <label class="control-label">From Excel</label>
                                             </div>
							                     <div class="col-sm-3" style="width:48%">
                                                <asp:FileUpload ID="FileUpload"  runat="server"  Width="200px" />
							                </div> 
							                <div class="col-sm-2">
                                                 <asp:LinkButton ID="lnkbtnUpload"  ToolTip="" runat="server" 
                                                     CssClass="btn full_width_btn btn-sm btn-primary" onclick="lnkbtnUpload_Click" ><i class="fa fa-upload"></i>Upload</asp:LinkButton>
                                           </div>
	                                        </div>
                                            <div class="clearfix even_row">
                                            
                                            <div class="col-sm-6">
                                                <span class="required-field"><label id="lblExcelMessage" runat="server" class="control-label"></label></span>
                                            </div>
                                            </div>                                      
                                            </div>
                                            </section>
							            </div>
                                    <div class="modal-footer">
                                         <div class="popup_footer_btn">
                                        <button type="submit" class="btn btn-dark" data-dismiss="modal">
                                            <i class="fa fa-times"></i>Close</button>
                                        </div>
                                        </div>
                                    </div>
                                
                            </div>
                    </div>
                      <div id="Excel_Upload" class="modal fade" role="dialog">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h4 class="popform_header">
                                            Upload Excel</h4>
                                    </div>
                                    <div class="modal-body">
                                    <section class="panel panel-default full_form_container material_search_pop_form">
								            <div class="panel-body">
									            <!-- First Row start -->
								            <div class="clearfix odd_row">	       
                                            <div class="col-sm-2" style="width:70%">
	                                            <label class="control-label">Please export Excel for upload and fill details.</label>
                                             </div>
							                     <div class="col-sm-3" style="width:28%">
                                                 <asp:LinkButton ID="lnkbtnExport" runat="server" 
                                                    CssClass="btn full_width_btn btn-s-md btn-info" onclick="lnkbtnExport_Click" 
                                                        ><i CausesValidation="true" class="fa fa-upload"></i>Export Excel</asp:LinkButton>
							                </div> 
							               
	                                        </div>
                                            <div class="clearfix even_row">
                                            <div class="col-sm-2" style="width:70%">
	                                            <label class="control-label">Please upload Excel data in same format above.</label>
                                             </div>
                                            <div class="col-sm-6" style="width:28%">
                                               <asp:LinkButton ID="lnkbtnExcelUpload" runat="server"
                                                CssClass="btn full_width_btn btn-s-md btn-info" data-toggle="modal" data-target="#Upload_div" data-dismiss="modal"
                                                 ><i CausesValidation="true" class="fa fa-upload"></i>Import Excel</asp:LinkButton>
                                            </div>
                                            </div>                                      
                                            </div>
                                            </section>
							            </div>
                                    <div class="modal-footer">
                                         <div class="popup_footer_btn">
                                        <button type="submit" class="btn btn-dark" data-dismiss="modal">
                                            <i class="fa fa-times"></i>Close</button>
                                        </div>
                                        </div>
                                    </div>
                                
                            </div>
                    </div>
                      <div id="dvGrdetails" class="modal fade" >
							<div class="modal-dialog">
							<div class="modal-content" style="width:800px">
								<div class="modal-header">
								<h4 class="popform_header">GR Detail </h4>
								</div>
								<div class="modal-body">
								<section class="panel panel-default full_form_container material_search_pop_form">
									<div class="panel-body">
										<!-- First Row start -->
									<div class="clearfix odd_row">	                                
	                                <div class="col-sm-4">
	                                  <label class="col-sm-5 control-label">Date From</label>
                                    <div class="col-sm-7">
                                     <asp:TextBox ID="txtDateFrom" runat="server" CssClass="input-sm datepicker form-control"  TabIndex="85" data-date-format="dd-mm-yyyy"></asp:TextBox>                                     
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtFrm" runat="server" ErrorMessage="Enter From Date!"
                                            Display="Dynamic" CssClass="classValidation" ControlToValidate="txtDateFrom" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                     
                                    </div>
	                                </div>
	                                <div class="col-sm-4">
	                                  <label class="col-sm-5 control-label">Date To</label>
                                    <div class="col-sm-7">
                                       <asp:TextBox ID="txtDateTo" runat="server" CssClass="input-sm datepicker form-control" TabIndex="86" data-date-format="dd-mm-yyyy"></asp:TextBox>                                     
                                        <asp:RequiredFieldValidator ID="rfvRcptEntryDtTo" runat="server" ErrorMessage="Enter To Date!"  Display="Dynamic" CssClass="classValidation" ControlToValidate="txtDateTo" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                     
                                    </div>
	                                </div>
	                                <div class="col-sm-4">
	                                  <label class="col-sm-4 control-label">GR Type</label>
	                                  <div class="col-sm-8">
                                      <asp:DropDownList ID="ddlgrtyp" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlgrtyp_SelectedIndexChanged" TabIndex="87">
                                            <asp:ListItem Value="0">Gr Prepration</asp:ListItem>
                                            <asp:ListItem Value="1">Gr Crossing</asp:ListItem>
                                            <asp:ListItem Value="2">Gr Retailer</asp:ListItem>
                                        </asp:DropDownList>   
	                                  </div>
	                                </div>
	                              </div> 

	                              <div class="clearfix even_row">
                                  <div class="col-sm-4">
                                   <asp:Label runat="server" ID="Label4" CssClass="col-sm-5 control-label" Text="To City"></asp:Label>
	                                
		                                <div class="col-sm-7">
                                         <asp:DropDownList ID="ddlDivTocity" onchange="javascript:cityviaddl();" runat="server"  CssClass="chzn-select" style="width: 142px;" TabIndex="88">
                                        </asp:DropDownList>
                                        	                            
		                                </div>
	                                </div>

                                    <div class="col-sm-4">
                                   <asp:Label runat="server" ID="Label1" CssClass="col-sm-5 control-label" Text="Via City"></asp:Label>
	                                
		                                <div class="col-sm-7">
                                         <asp:DropDownList ID="ddlViaCity" runat="server" CssClass="form-control" style="width:100%;" TabIndex="88">
                                        </asp:DropDownList>
                                                           
		                                </div>
	                                </div>

                                    <div class="col-sm-4">
                                  <asp:Label runat="server" ID="lblTrantype"  CssClass="col-sm-4 control-label"  >Tran Type</asp:Label>
                                  <div class="col-sm-8">
                                  <asp:DropDownList ID="ddltrantype" Enabled="false"  runat="server" CssClass="form-control" style="width:100%;" AutoPostBack="true" OnSelectedIndexChanged="ddltrantype_SelectedIndexChanged">
                                  </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please select Party name"
                                            Display="Dynamic" InitialValue="" CssClass="classValidation" ControlToValidate="ddltrantype"
                                            ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                            </div>
                                  </div>
                                   
                                       
	                              </div> 
                                  <div class="clearfix odd_row">
                                  <div class="col-sm-4">
                                   <asp:Label runat="server" ID="lblDelvSerch" CssClass="col-sm-5 control-label" Text=""></asp:Label>
	                                
		                                <div class="col-sm-7">
                                         <asp:DropDownList ID="ddldelvplace" runat="server" CssClass="chzn-select" style="width:142px;" TabIndex="88" Visible="false" >
                                        </asp:DropDownList>
                                       
                                      <asp:TextBox ID="txttruck" runat="server" CssClass="form-control auto-extender" onkeyup="SetContextKey()" onkeydown="return (event.keyCode!=13);" ></asp:TextBox>
                                     <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txttruck" MinimumPrefixLength="1" UseContextKey="false" EnableCaching="true" CompletionSetCount="1" CompletionInterval="500" OnClientItemSelected="ClientItemSelected" ServiceMethod="GetTruckNo">
                                      </asp:AutoCompleteExtender>
                                       <asp:Label runat="server" ID="lbltr" CssClass="classValidation" Text=""></asp:Label>
                         <%--   <asp:RequiredFieldValidator ID="rfvRcptEntryPrty" runat="server" ErrorMessage="Please select truck name"
                                            Display="Dynamic" InitialValue="0" CssClass="classValidation" ControlToValidate="ddldelvplace" 
                                            ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>	      --%>     
		                                </div>
	                                </div>
                                     <div class="col-sm-offset-5 " style="padding: 0;">
	                                  <div class="col-sm-6 prev_fetch">                                      
                                        <asp:LinkButton ID="lnkbtnSearch" runat="server" CssClass="btn full_width_btn btn-sm btn-primary" CausesValidation="true" TabIndex="89" ValidationGroup="RcptEntrySrch" OnClick="lnkbtnSearch_OnClick"><i class="fa fa-search"></i>Search</asp:LinkButton>	                                
	                                  </div>
	                                  <div class="col-sm-6"> 
	                                     <label class="control-label">T. Record(s) : </label>
	                                  </div>
	                                </div>
                                  </div>
                                  <div class="clearfix fourth_right">
                        <section class="panel panel-in-default btns_without_border">                            
                          <div class="panel-body">     
                            <div class="clearfix">
		                          <section class="panel panel-default full_form_container material_search_pop_form">
		                            <div class="panel-body">
                                      <asp:GridView ID="grdGrdetals" runat="server" GridLines="None" AutoGenerateColumns="false"
                                            Width="100%" BorderStyle="None" CssClass="display wrap dataTable"
                                            BorderWidth="0" >
                                           <RowStyle CssClass="odd" />
                                        <AlternatingRowStyle CssClass="even" />  
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select" HeaderStyle-Width="40px">
                                                     <HeaderStyle Width="40" CssClass="gridHeaderAlignCenter" />
                                                <ItemStyle Width="40" CssClass="gridHeaderAlignCenter" />
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAllCheckboxes(this);" TabIndex="90"
                                                            CssClass="SACatA" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkId" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gr No." HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("Gr_No"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gr Date" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MMM-yyyy")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Receiver Name" HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOdrNo" runat="server" Text=' <%#Eval("Recvr_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To City" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="180px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Eval("To_City")%>
                                                        <asp:HiddenField ID="hidGrIdno" runat="server" Value='<%#Eval("Gr_Idno")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Via City" HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Right">
                                                    <ItemStyle Width="20px" HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                           <%#Eval("CityVia_Name")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                Records(s) not found.
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                       
                                      
		                            </div>
		                          </section> 
		                        </div> 
                          </div>
                        </section>
                      </div>
	                              
								</div>
							</section>
							</div>
							<div class="modal-footer">
							<div class="popup_footer_btn"> 
                                <asp:LinkButton ID="lnkbtnSubmit" runat="server" CssClass="btn btn-dark" OnClick="lnkbtnSubmit_OnClick" TabIndex="91"><i class="fa fa-check"></i>Ok</asp:LinkButton>
								<asp:LinkButton ID="lnkbtnCloase" runat="server" CssClass="btn btn-dark" OnClick="lnkbtnCloase_Click" TabIndex="92"><i class="fa fa-times"></i>Close</asp:LinkButton>

						        <div style="float:left;">                              
                                    <asp:Label ID="Label3" runat="server" CssClass="redfont"></asp:Label>
                                </div>
							</div>
							</div>
						</div>
						</div>
					</div>
                    <div id="dvFreight" class="modal fade">
                    <div class="modal-dialog">
							<div class="modal-content" style="width:800px">
								<div class="modal-header">
								<h4 class="popform_header">Other Details</h4>
								</div>
								<div class="modal-body">
								<section class="panel panel-default full_form_container material_search_pop_form">
									<div class="panel-body">
										<!-- First Row start -->
									<div class="clearfix odd_row">	                                
	                                <div class="col-sm-4">
	                                  <asp:Label runat="server" ID="Label21" CssClass="col-sm-4 control-label" Text="Starting KM"></asp:Label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txtstartkm" runat="server" Text="0.00" MaxLength="10"   TabIndex="85" ></asp:TextBox>                                     
                                       
                                     
                                    </div>
	                                </div>
	                                <div class="col-sm-4">
	                                  <asp:Label runat="server" ID="Label19" CssClass="col-sm-4 control-label" Text="Closing KM"></asp:Label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="txtCloseKm" runat="server" Text="0.00" MaxLength="10" TabIndex="86" ></asp:TextBox>                                     
                                       
                                    </div>
	                                </div>
	                                <div class="col-sm-4">
	                                  <asp:Label runat="server" ID="Label18" CssClass="col-sm-4 control-label" Text="Late D&D"></asp:Label>
	                                  <div class="col-sm-8">
                                      <asp:TextBox ID="txtLatecharge" runat="server"  Text="0.00" TabIndex="86" 
                                              onchange="javascript:CalcAmnt();" MaxLength="8"></asp:TextBox> 
	                                  </div>
	                                </div>
	                              </div> 

	                              <div class="clearfix even_row">
                                  <div class="col-sm-4">
                                   <asp:Label runat="server" ID="Label12" CssClass="col-sm-4 control-label" Text="Hamali"></asp:Label>
	                                
		                                <div class="col-sm-8">
                                         <asp:TextBox ID="txtHamaliCharge" runat="server" onchange="javascript:CalcAmnt();" Text="0.00" TabIndex="86" MaxLength="8" ></asp:TextBox> 
                                        	                            
		                                </div>
	                                </div>

                                    <div class="col-sm-4">
                                   <asp:Label runat="server" ID="Label15" CssClass="col-sm-4 control-label" Text="Detention"></asp:Label>
	                                
		                                <div class="col-sm-8">
                                        <asp:TextBox ID="txtDetention" Text="0.00" onchange="javascript:CalcAmnt();" runat="server" TabIndex="86" MaxLength="8" ></asp:TextBox> 
                                                           
		                                </div>
	                                </div>

                                    <div class="col-sm-4">
                                  <asp:Label runat="server" ID="Label17"  CssClass="col-sm-4 control-label" Text="Rate Per KM"  ></asp:Label>
                                  <div class="col-sm-8">
                                 <asp:TextBox ID="txtRatePerKM" Text="0.00" onchange="javascript:Enable();" runat="server" TabIndex="86" MaxLength="8"  ClientIDMode="Static" ></asp:TextBox> 
                                            </div>
                                  </div>
                                   
                                       
	                              </div> 
                                  
                                  <div class="clearfix odd_row">	                                
	                                <div class="col-sm-4">
	                                  <asp:Label runat="server" ID="Label27" CssClass="col-sm-4 control-label" Text="Freight"></asp:Label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txtFreight" runat="server" Text="0.00" onchange="javascript:Enable();"   TabIndex="85" MaxLength="8"  ClientIDMode="Static"></asp:TextBox> 
                                     
                                    </div>
	                                </div>
                                     <div class="col-sm-4">
	                                  <asp:Label runat="server" ID="Label28" CssClass="col-sm-4 control-label" Text="Delv. Date"></asp:Label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txtDlyDate" runat="server"  PlaceHolder="DD-MM-YYYY" CssClass="input-sm datepicker form-control"  MaxLength="50" oncopy="return false" 
                                     oncut="return false" onDrop="blur();return false;" onpaste="return false" onkeydown = "return DateFormat(this, event.keyCode)">
                                     </asp:TextBox>                                      
                                    </div>
	                                </div>
	                              </div>
	                              
								</div>
							</section>
							</div>
							<div class="modal-footer">
							
							</div>
						</div>
						</div>
                    
                    
                    </div>

                    </div>                 
                  </form>
                </div>
              </section>
        </div>
        <div class="col-lg-1">
        </div>
    </div>
    <!-- Print table  -->
    <table width="100%">
        <tr style="display: none">
            <td class="white_bg" align="center">
                <div id="print" style="font-size: 12px">
                    <table cellpadding="1" cellspacing="0" width="900" border="1" style="height:40px;">
                        <tr>
                            <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 12px;
                                border-left-style: none; border-right-style: none">
                                <strong>
                                    <asp:Label ID="lblCompanyname" runat="server" Style="font-size: 18px;"></asp:Label><br />
                                </strong>
                                <asp:Label ID="lblCompAdd1" runat="server"></asp:Label>
                                &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblCompAdd2" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCompCity" runat="server"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblCompState" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblCompCityPin" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCompPhNo" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblFaxNo" Text="FAX No.:" runat="server"></asp:Label>
                                <asp:Label ID="lblCompFaxNo" runat="server"></asp:Label><br />
                                <asp:Label ID="lblTin" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                                    ID="lblCompTIN" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 12px;
                                border-left-style: none; border-right-style: none">
                                <h3>
                                    <strong style="text-decoration: underline">
                                        <asp:Label ID="lblPrintHeadng" runat="server" Text="Goods Receipt"></asp:Label></strong></h3>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table border="0" width="100%">
                                    <tr>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 11px; border-right-style: none;">
                                            <asp:Label ID="lblchlntext" Text="Challan No." runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td align="left" class="white_bg" style="font-size: 11px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblChlnno" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 11px; border-right-style: none"
                                            colspan="2">
                                            <asp:Label ID="lbltxtchlndate" Text="Challan Date" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td align="left" class="white_bg" style="font-size: 11px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblchlnDate" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 11px; width: 200px;
                                            border-right-style: none">
                                            <asp:Label ID="lbltxtownr" Text="Owner Name :" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;<b><asp:Label
                                                ID="lblOwnr" runat="server"></asp:Label></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 11px; border-right-style: none">
                                            <asp:Label ID="lbltxttruck" Text="Vehicle / Truck No." runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td align="left" class="white_bg" style="font-size: 11px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblTrckNo" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" style="font-size: 11px; border-right-style: none"
                                            colspan="2">
                                            <asp:Label ID="lbltxtdriver" Text="Driver Name" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td align="left" class="white_bg" style="font-size: 11px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblDrvrName" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 11px; width: 200px;
                                            border-right-style: none">
                                            <asp:Label ID="Label29" Text="Account :" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;<b><asp:Label
                                                ID="lblPetrolPump" runat="server"></asp:Label></b>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table border="0" cellspacing="0" style="font-size: 10px" width="100%" id="Table1">
                                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                        <HeaderTemplate>
                                            <tr>
                                                <td style="font-size: 10px;" width="5%">
                                                    <strong>S. No.</strong>
                                                </td>
                                                <td style="font-size: 10px;" width="10%">
                                                    <strong>Billty No.</strong>
                                                </td>
                                                <td style="font-size: 10px;" width="15%">
                                                    <strong>Consignor</strong>
                                                </td>
                                                <td style="font-size: 10px;" width="15%">
                                                    <strong>Consignee</strong>
                                                </td>
                                                <td style="font-size: 10px;" width="8%">
                                                    <strong>Location</strong>
                                                </td>
                                                <td style="font-size: 10px;" width="8%">
                                                    <strong>Via</strong>
                                                </td>
                                                <td style="font-size: 10px;" width="8%">
                                                    <strong>Destination</strong>
                                                </td>
                                                <td style="font-size: 10px;" width="5%">
                                                    <strong>DI No.</strong>
                                                </td>
                                                <td style="font-size: 10px;" width="5%">
                                                    <strong>EGP No.</strong>
                                                </td>
                                                <td style="font-size: 10px;" width="5%">
                                                    <strong>Item</strong>
                                                </td>
                                                <td style="font-size: 10px;" align="right" width="5%">
                                                    <strong>Qty</strong>
                                                </td>
                                                <td style="font-size: 10px;" align="right" width="5%">
                                                    <strong>Weight</strong>
                                                </td>
                                                <div id="hide" runat="server">
                                                    <td style="font-size: 10px;" width="10%" align="right">
                                                        <asp:Label ID="dr" runat="server">
                                                    <strong>Rate</strong>
                                                        </asp:Label>
                                                    </td>
                                                    <td style="font-size: 10px;" width="10%" align="left">
                                                        <asp:Label ID="rashi" runat="server">
                                                    <strong>Amt.</strong>
                                                        </asp:Label>
                                                    </td>
                                                    <td style="font-size: 10px;" width="10%" align="right">
                                                        <asp:Label ID="utrai" runat="server">
                                                    <strong>Unloading</strong>
                                                        </asp:Label>
                                                    </td>
                                                    <td style="font-size: 10px;" width="10%" align="right">
                                                        <asp:Label ID="totalrashi" runat="server">
                                                    <strong>Total Amt.</strong>
                                                        </asp:Label>
                                                    </td>
                                                </div>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="white_bg" width="5%">
                                                    <%#Container.ItemIndex+1 %>.
                                                </td>
                                                <td class="white_bg" width="10%">
                                                    <%#Eval("GR_No")%>
                                                </td>
                                                <td class="white_bg" width="15%">
                                                    <%#Eval("SenderName_Eng")%>
                                                </td>
                                                <td class="white_bg" width="15%">
                                                    <%#Eval("RecvrName_Eng")%>
                                                </td>
                                                <td class="white_bg" width="8%">
                                                    <%#(string.IsNullOrEmpty(Eval("FromCity_Eng").ToString())? "" : Convert.ToString(Eval("FromCity_Eng")))%>
                                                </td>
                                                <td class="white_bg" width="8%">
                                                    <%#Eval("CityVia_Name")%>
                                                </td>
                                                <td class="white_bg" width="8%">
                                                    <%#Eval("ToCity_Eng")%>
                                                </td>
                                                <td class="white_bg" width="8%">
                                                    <%#Eval("DINo")%>
                                                </td>
                                                <td class="white_bg" width="5%">
                                                    <%#Eval("EGPNo")%>
                                                </td>                                               
                                                <td class="white_bg" width="5%">
                                                    <%#Eval("ITEM")%>
                                                </td>
                                                <td class="white_bg" width="5%" align="right">
                                                    <%#Eval("Qty")%>
                                                </td>
                                                <td class="white_bg" width="5%" align="right">
                                                    <%#String.Format("{0:0.000}", Convert.ToDouble(Eval("Weight")))%>
                                                </td>
                                                <td class="white_bg" width="10%" align="right">
                                                    <asp:Label ID="rate" runat="server">
                                                    <%#string.Format("{0:0.00}",Convert.ToDouble(Eval("Item_Rate")))%></asp:Label>
                                                </td>
                                                <td class="white_bg" width="10%" align="right">
                                                    <asp:Label ID="amount" runat="server">
                                                    <%#string.Format("{0:0.00}", Convert.ToDouble(Eval("WithoutUnloading_Amnt")))%></asp:Label>
                                                </td>
                                                <td class="white_bg" width="10%" align="right">
                                                    <asp:Label ID="WagesAmnt" runat="server">
                                                    <%#string.Format("{0:0.00}", Convert.ToDouble(Eval("Wages_Amnt")))%></asp:Label>
                                                </td>
                                                <td class="white_bg" width="10%" align="right">
                                                    <asp:Label ID="Totalamount" runat="server">
                                                    <%#string.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%>
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <tr>
                                                <td style="font-size: 10px;" width="5%">
                                                </td>
                                                <td style="font-size: 10px;" width="10%">
                                                </td>
                                                <td style="font-size: 10px;" width="15%">
                                                </td>
                                                <td style="font-size: 10px;" width="15%">
                                                </td>
                                                <td style="font-size: 10px;" width="8%">
                                                </td>
                                                <td style="font-size: 10px;" width="8%">
                                                </td>
                                                <td style="font-size: 10px;" width="8%">
                                                </td>
                                                <td style="font-size: 10px;" width="5%">
                                                </td>
                                                <td style="font-size: 10px;" width="5%">
                                                </td>
                                                <td style="font-size: 10px;" width="5%">
                                                    <asp:Label ID="lblttl" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                                <td style="font-size: 10px;" align="right" width="5%">
                                                    <asp:Label ID="lbltotalqty" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                                <td style="font-size: 10px;" align="right" width="5%">
                                                    <asp:Label ID="lbltotalWeight" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                                <td style="font-size: 10px;" width="10%" align="left">
                                                </td>
                                                <td style="font-size: 10px;" width="10%" align="right">
                                                    <asp:Label ID="lblAmount" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                                <td style="font-size: 10px;" width="10%" align="right">
                                                    <asp:Label ID="lblWagesAmnt" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                                <td style="font-size: 10px;" width="10%" align="right">
                                                    <asp:Label ID="lblTotalAmnt" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                        <div id="showdetl" runat="server">
                            <tr>
                                <td style="font-size: 11px;">
                                    <asp:Label ID="lblPrintDateTime" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="3" align="left" width="75%">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblDeliveryins" runat="server" Text="Remark" valign="left" Font-Size="11px"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbltxtdelivery" runat="server" Font-Size="11px" valign="Left"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td colspan="1" width="25%">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblAdvanceAmnt" runat="server" Text="Advance Amt." Font-Size="11px"
                                                                valign="right"></asp:Label>
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="valuelblAdvanceAmnt" runat="server" Font-Size="11px" valign="right"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblcmmnsn" runat="server" Text="B.C." Font-Size="11px" valign="right"></asp:Label>
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="valuelblcmmnsn" runat="server" Font-Size="11px" valign="right"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblDiesel" runat="server" Text="Diesel Amt." Font-Size="11px" valign="right"></asp:Label>
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDieselAmnt" runat="server" Font-Size="11px" valign="right"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblTdsAmnt" runat="server" Text="TDS Amt." Font-Size="11px" valign="right"></asp:Label>
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="valueLblTdsAmnt" runat="server" Font-Size="11px" valign="right"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label30" runat="server" Text="Rassa Tirpal Amt." Font-Size="11px" valign="right"></asp:Label>
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblRasstripalChrg" runat="server" Font-Size="11px" valign="right"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblnetTotal" runat="server" Text="Net Amt." Font-Size="11px" valign="right"></asp:Label>
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="valuelblnetTotal" runat="server" Font-Size="11px" valign="right"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </div>
                        <tr>
                            <td align="left" valign="top" colspan="4">
                                <table width="100%" style="font-size: 10px" border="0" cellspacing="0">
                                    <tr style="line-height: 25px">
                                        <td colspan="9" style="font-size: 11px" align="left" class="white_bg">
                                            <table width="100%">
                                                <tr>
                                                    <td align="left" class="white_bg" style="font-size: 11px; font-family: Kruti Dev 010"
                                                        width="70%">
                                                        <b>Received the aforesaid goods through billty in proper condition the company 
                                                            promises on time on safe delivery of the goods at the desired Station and takes full responsibility.
                                                        </b>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td align="right" class="white_bg" style="font-size: 11px; font-family: Kruti Dev 010"
                                                        valign="top" width="30%">
                                                        <b>
                                                            <br />
                                                            <br />
                                                            
Signature Driver&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
Signature Booking Clerk &nbsp;</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <p style="line-height: 14px; font-size: 10px;">
                        <asp:Label ID="lblGeneratedByName" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblLastUpdatedByName" runat="server"></asp:Label>
                    </p>
                </div>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr style="display: none">
            <td class="white_bg" align="center">
                <div id="print1" style="font-size: 13px;">
                    <table cellpadding="1" cellspacing="0" width="900" border="1">
                        <tr>
                            <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                                border-left-style: none; border-right-style: none">
                                <strong>
                                    <asp:Label ID="lblCompanyname1" runat="server" Style="font-size: 18px;"></asp:Label><br />
                                </strong>
                                <asp:Label ID="lblCompAdd12" runat="server"></asp:Label>
                                &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblCompAdd22" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCompCity12" runat="server"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblCompState12" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblCompCityPin12" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCompPhNo12" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblFaxNo1" Text="FAX No.:" runat="server"></asp:Label>
                                <asp:Label ID="lblCompFaxNo1" runat="server"></asp:Label><br />
                                <asp:Label ID="lblTin1" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                                    ID="lblCompTIN1" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                                border-left-style: none; border-right-style: none">
                                <h3>
                                    <strong style="text-decoration: underline">
                                        <asp:Label ID="lblPrintHeadng1" runat="server" Text="Goods Receipt"></asp:Label></strong></h3>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table border="0" width="100%">
                                    <tr>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;">
                                            <asp:Label ID="lblchlntext1" Text="Challan No." runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td align="left" class="white_bg" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblChlnno1" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none"
                                            colspan="2">
                                            <asp:Label ID="lbltxtchlndate1" Text="Challan Date" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td align="left" class="white_bg" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblchlnDate1" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; width: 200px;
                                            border-right-style: none">
                                            <asp:Label ID="lbltxtownr1" Text="Owner Name :" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;<b><asp:Label
                                                ID="lblOwnr1" runat="server"></asp:Label></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lbltxttruck1" Text="Vehicle \ Truck No." runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td align="left" class="white_bg" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblTrckNo1" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" style="font-size: 13px; border-right-style: none"
                                            colspan="2">
                                            <asp:Label ID="lbltxtdriver1" Text="Driver Name" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td align="left" class="white_bg" style="font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblDrvrName1" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="font-size: 13px; width: 200px;
                                            border-right-style: none">
                                            <asp:Label ID="Label49" Text="Account :" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;<b><asp:Label
                                                ID="lblPetrolPump1" runat="server"></asp:Label></b>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table22">
                                    <asp:Repeater ID="Repeater22" runat="server" OnItemDataBound="Repeater22_ItemDataBound">
                                        <HeaderTemplate>
                                            <tr>
                                                <td style="font-size: 12px;" width="5%">
                                                    <strong>S. No.</strong>
                                                </td>
                                                <td style="font-size: 12px;" width="10%">
                                                    <strong>Billty No.</strong>
                                                </td>
                                                <td style="font-size: 12px;" width="15%">
                                                    <strong>Consignor</strong>
                                                </td>
                                                <td style="font-size: 12px;" width="15%">
                                                    <strong>Consignee</strong>
                                                </td>
                                                <td style="font-size: 12px;" width="8%">
                                                    <strong>Location</strong>
                                                </td>
                                                <td style="font-size: 12px;" width="8%">
                                                    <strong>Via</strong>
                                                </td>
                                                <td style="font-size: 12px;" width="8%">
                                                    <strong>Destination</strong>
                                                </td>
                                                <td style="font-size: 12px;" width="5%">
                                                    <strong>DI No.</strong>
                                                </td>
                                                <td style="font-size: 12px;" width="5%">
                                                    <strong>EGP No.</strong>
                                                </td>
                                                <td style="font-size: 12px;" width="5%">
                                                    <strong>Item</strong>
                                                </td>
                                                <td style="font-size: 12px;" align="right" width="5%">
                                                    <strong>Qty</strong>
                                                </td>
                                                <td style="font-size: 12px;" align="right" width="5%">
                                                    <strong>Weight</strong>
                                                </td>
                                                <div id="hide1" runat="server">
                                                    <td style="font-size: 12px;" width="10%" align="right">
                                                        <asp:Label ID="dr1" runat="server">
                                                    <strong>Rate</strong>
                                                        </asp:Label>
                                                    </td>
                                                    <td style="font-size: 12px;" width="10%" align="left">
                                                        <asp:Label ID="rashi1" runat="server">
                                                    <strong>Amt.</strong>
                                                        </asp:Label>
                                                    </td>
                                                    <td style="font-size: 12px;" width="10%" align="right">
                                                        <asp:Label ID="utrai1" runat="server">
                                                    <strong>Unloading</strong>
                                                        </asp:Label>
                                                    </td>
                                                    <td style="font-size: 12px;" width="10%" align="right">
                                                        <asp:Label ID="totalrashi1" runat="server">
                                                    <strong>Total Amt.</strong>
                                                        </asp:Label>
                                                    </td>
                                                </div>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="white_bg" width="5%">
                                                    <%#Container.ItemIndex+1 %>.
                                                </td>
                                                <td class="white_bg" width="10%">
                                                    <%#Eval("GR_No")%>
                                                </td>
                                                <td class="white_bg" width="15%">
                                                    <%#Eval("SenderName_Eng")%>
                                                </td>
                                                <td class="white_bg" width="15%">
                                                    <%#Eval("RecvrName_Eng")%>
                                                </td>
                                                <td class="white_bg" width="8%">
                                                    <%#(string.IsNullOrEmpty(Eval("FromCity_Eng").ToString())? "" : Convert.ToString(Eval("FromCity_Eng")))%>
                                                </td>
                                                <td class="white_bg" width="8%">
                                                    <%#Eval("CityVia_Name")%>
                                                </td>
                                                <td class="white_bg" width="8%">
                                                    <%#Eval("ToCity_Eng")%>
                                                </td>
                                                <td class="white_bg" width="8%">
                                                    <%#Eval("DINo")%>
                                                </td>
                                                <td class="white_bg" width="5%">
                                                    <%#Eval("EGPNo")%>
                                                </td>
                                                <td class="white_bg" width="5%">
                                                    <%#Eval("ITEM")%>
                                                </td>
                                                <td class="white_bg" width="5%" align="right">
                                                    <%#Eval("Qty")%>
                                                </td>
                                                <td class="white_bg" width="5%" align="right">
                                                    <%#String.Format("{0:0.000}", Convert.ToDouble(Eval("Weight")))%>
                                                </td>
                                                <td class="white_bg" width="10%" align="right">
                                                    <asp:Label ID="rate1" runat="server">
                                                    <%#string.Format("{0:0.00}",Convert.ToDouble(Eval("Item_Rate")))%></asp:Label>
                                                </td>
                                                <td class="white_bg" width="10%" align="right">
                                                    <asp:Label ID="amount1" runat="server">
                                                    <%#string.Format("{0:0.00}", Convert.ToDouble(Eval("WithoutUnloading_Amnt")))%></asp:Label>
                                                </td>
                                                <td class="white_bg" width="10%" align="right">
                                                    <asp:Label ID="WagesAmnt1" runat="server">
                                                    <%#string.Format("{0:0.00}", Convert.ToDouble(Eval("Wages_Amnt")))%></asp:Label>
                                                </td>
                                                <td class="white_bg" width="10%" align="right">
                                                    <asp:Label ID="Totalamount1" runat="server">
                                                    <%#string.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%>
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <tr>
                                                <td style="font-size: 12px;" width="5%">
                                                </td>
                                                <td style="font-size: 12px;" width="10%">
                                                </td>
                                                <td style="font-size: 12px;" width="15%">
                                                </td>
                                                <td style="font-size: 12px;" width="15%">
                                                </td>
                                                <td style="font-size: 12px;" width="8%">
                                                </td>
                                                <td style="font-size: 12px;" width="8%">
                                                </td>
                                                <td style="font-size: 12px;" width="8%">
                                                </td>
                                                <td style="font-size: 12px;" width="5%">
                                                </td>
                                                <td style="font-size: 12px;" width="5%">
                                                </td>
                                                <td style="font-size: 12px;" width="5%">
                                                    <asp:Label ID="lblttl1" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                                <td style="font-size: 12px;" align="right" width="5%">
                                                    <asp:Label ID="lbltotalqty1" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                                <td style="font-size: 12px;" align="right" width="5%">
                                                    <asp:Label ID="lbltotalWeight1" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                                <td style="font-size: 12px;" width="10%" align="left">
                                                </td>
                                                <td style="font-size: 12px;" width="10%" align="right">
                                                    <asp:Label ID="lblAmount1" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                                <td style="font-size: 12px;" width="10%" align="right">
                                                    <asp:Label ID="lblWagesAmnt1" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                                <td style="font-size: 12px;" width="10%" align="right">
                                                    <asp:Label ID="lblTotalAmnt1" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                        <div id="showdetl1" runat="server">
                            <tr>
                                <td>
                                    <asp:Label ID="lblPrintDateTime1" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="3" align="left" width="75%">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblDeliveryins1" runat="server" Text="Remark" valign="left" Font-Size="13px"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbltxtdelivery1" runat="server" Font-Size="13px" valign="Left"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td colspan="1" width="25%">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblAdvanceAmnt1" runat="server" Text="Advance Amt." Font-Size="13px"
                                                                valign="right"></asp:Label>
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="valuelblAdvanceAmnt1" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblcmmnsn1" runat="server" Text="B.C." Font-Size="13px" valign="right"></asp:Label>
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="valuelblcmmnsn1" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblDiesel1" runat="server" Text="Diesel Amt." Font-Size="13px" valign="right"></asp:Label>
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDieselAmnt1" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblTdsAmnt1" runat="server" Text="TDS Amt." Font-Size="13px" valign="right"></asp:Label>
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="valueLblTdsAmnt1" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label60" runat="server" Text="Rassa Tirpal Amt." Font-Size="13px"
                                                                valign="right"></asp:Label>
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblRasstripalChrg1" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblnetTotal1" runat="server" Text="Net Amt." Font-Size="13px" valign="right"></asp:Label>
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="valuelblnetTotal1" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </div>
                        <tr>
                            <td align="left" valign="top" colspan="4">
                                <table width="100%" style="font-size: 12px" border="0" cellspacing="0">
                                    <tr style="line-height: 25px">
                                        <td colspan="9" style="font-size: 13px" align="left" class="white_bg">
                                            <table width="100%">
                                                <tr>
                                                    <td align="left" class="white_bg" style="font-size: 13px; font-family: Kruti Dev 010"
                                                        width="70%">
                                                        <b>Received the aforesaid goods through billty in proper condition the company 
                                                            promises on time on safe delivery of the goods at the desired Station and takes full responsibility.</b>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td align="right" class="white_bg" style="font-size: 13px; font-family: Kruti Dev 010"
                                                        valign="top" width="30%">
                                                        <b>
                                                            <br />
                                                            <br />
                                                            
Signature Driver&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
Signature Booking Clerk &nbsp;</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <p style="line-height: 16px; font-size: 12px;">
                        <asp:Label ID="lblGeneratedByName1" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblLastUpdatedByName1" runat="server"></asp:Label>
                    </p>
                </div>
            </td>
        </tr>
    </table>
    <!-- popup form for Amount  -->
    <div id="Amount" class="modal fade">
        <div class="modal-dialog" style="width: 50%">
            <div class="modal-header">
                <h4 class="popform_header">
                    Print&nbsp;&nbsp;&nbsp;&nbsp;</h4>
            </div>
            <div class="modal-content">
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
                        <div class="panel-body">   
                        <div class="col-sm-12">
                                       <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlPages" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="3 Pages" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="2 Pages" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="1 Pages" Value="1" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                       </div>
                                        <div class="col-sm-9">
                                      <div class="col-sm-6">
                            <asp:LinkButton ID="lnkwithamount" Text="With Amount" 
                                class="btn btn-sm btn-primary" runat="server" TabIndex="45" 
                                OnClientClick ="return CallPrint('print');" ></asp:LinkButton>
                                       
                            <asp:LinkButton ID="lnkwithoutamount" Text="Without Amount" 
                                class="btn btn-sm btn-primary" runat="server"  TabIndex="45"  
                                onclick="lnkwithoutamount_Click" ></asp:LinkButton>
                                    </div>
                                      <div class="col-sm-4">  
                                    <span  class="col-sm-6">Print Date and time </span> 
                                    <asp:CheckBox CssClass="col-sm-6" runat="server" ID="chkPrint2DateTime" onclick="ToggleDatePrint()" ClientIDMode="Static" />                                                          
                            </div>
                            <div class="col-sm-2">
                             <span  class="col-sm-6">Print</span> 
                            <asp:DropDownList ID="ddlPrintType" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Same Page" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Different Page" Value="2" Selected="True"></asp:ListItem>                                            
                                        </asp:DropDownList>
                            </div>
                             </div>
                             </div>
                        </div>
                    </section>
                </div>
                <div class="modal-footer">
                    <div class="popup_footer_btn">
                        <div class="col-sm-4">
                        </div>
                        <div class="col-sm-3">
                            <button type="submit" class="btn btn-dark" data-dismiss="modal">
                                <i class="fa fa-times"></i>Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Popup for Posting -->
    <div id="acc_posting" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="popform_header">
                        Acc Posting</h4>
                </div>
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
								                <div class="panel-body">
									                <!-- First Row start -->
								                <div class="clearfix odd_row">	                                
	                                    <div class="col-sm-4">
	                                        <label class="col-sm-3 control-label">From</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtIdFrom" runat="server" CssClass="form-control" Width="100px" c oncopy="return false"
                                                    onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDivFrom" runat="server" ControlToValidate="txtIdFrom"
                                                CssClass="classValidation" Display="Dynamic" ErrorMessage="From Required." 
                                                SetFocusOnError="true" ValidationGroup="Acc"></asp:RequiredFieldValidator> 
                                        </div>
	                                    </div>
	                                    <div class="col-sm-4">
	                                        <label class="col-sm-2 control-label">To</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtIdTo" runat="server" CssClass="form-control" Width="100px" oncopy="return false"
                                                    onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDivTo" runat="server" ControlToValidate="txtIdTo"
                                                CssClass="classValidation" Display="Dynamic" ErrorMessage="To Required." 
                                                SetFocusOnError="true" ValidationGroup="Acc"></asp:RequiredFieldValidator> 
                                        </div>
	                                    </div>
	                                    <div class="col-sm-4" style="padding: 0;">
	                                        <div class="col-sm-1 prev_fetch">
	                                        </div>
	                                        <div class="col-sm-12"> 
	                                        <asp:Label ID="lblPostingLeft" runat="server"></asp:Label>
	                                        </div>
	                                    </div>
	                                    </div>
							        </div>
						        </section>
                </div>
                <div class="modal-footer">
                    <div class="popup_footer_btn">
                        <asp:LinkButton ID="lnkbtnAccPosting" OnClick="lnkbtnAccPosting_Click" ValidationGroup="Acc"
                            class="btn btn-dark" runat="server">Acc Posting</asp:LinkButton>
                        <button type="submit" class="btn btn-dark" data-dismiss="modal">
                            <i class="fa fa-times"></i>Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="divPrtyBlance" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="popform_header">
                        Party Balance</h4>
                </div>
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
		                <div class="panel-body">
			            <!-- First Row start -->
		                    <div class="clearfix odd_row">	       
                                <div class="col-sm-6">
	                                <asp:Label ID="lblBalPrtyName" runat="server"></asp:Label>
                                </div>           
                                <div class="col-sm-6">
	                                <asp:Label ID="lblPrtyBal" runat="server"></asp:Label>
                                </div>           
	                        </div>                                      
                        </div>
                    </section>
                </div>
            </div>
        </div>
    </div>
    <div id="Amount1" class="modal fade">
        <div class="modal-dialog" style="width: 25%">
            <div class="modal-header">
                <h4 class="popform_header">
                    Print&nbsp;&nbsp;&nbsp;&nbsp;</h4>
            </div>
            <div class="modal-content">
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
						<div class="panel-body">  
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlPage" runat="server" CssClass="form-control">                                            
                                    <asp:ListItem Text="3 Pages" Value="3" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="2 Pages" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="1 Page" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-6">
                                <asp:LinkButton ID="lnkBtnPrint" Text="Print" 
                                    class="btn btn-sm btn-primary" runat="server" TabIndex="45" onclick="lnkBtnPrint_Click" >
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-12">
                                <label class="col-sm-12">
                                    <span  class="col-sm-6">Print Date and time </span> 
                                    <asp:CheckBox CssClass="col-sm-6" runat="server" ID="chkPrintDateTime" onclick="ToggleDatePrint()" ClientIDMode="Static" />
                                </label>
                            </div>
                        </div>
                    </section>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-0" style="display: none">
        <tr style="display: block">
            <td class="white_bg" align="center">
                <div id="printOm" style="font-size: 13px; display: block;">
                    <table cellpadding="1" cellspacing="0" width="100%" border="1" style="font-family: Arial,Helvetica,sans-serif;">
                        <tr>
                            <td class="white_bg" align="center">
                                <table cellpadding="1" cellspacing="0" width="100%" border="1" style="font-family: Arial,Helvetica,sans-serif;">
                                    <tr>
                                        <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                                            border-left-style: none; border-right-style: none">
                                            <asp:Image ID="imgLogoShow" Style="width: 230px;" runat="server" ImageUrl="~/img/OMLOGO.png">
                                            </asp:Image><br />
                                            <asp:Label ID="lblCompDesc" runat="server" Style="font-size: 12px;"></asp:Label>
                                            <asp:Image ID="Image1" Height="3px" Style="width: 230px;" runat="server" ImageUrl="~/img/line.jpg">
                                            </asp:Image>
                                            <br />
                                            <asp:Label ID="lblCompAdd3" runat="server" Style="font-size: 12px;"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblCompAdd4" runat="server" Style="font-size: 12px;"></asp:Label>
                                            <asp:Label ID="lblCompCity1" runat="server" Style="font-size: 12px;"></asp:Label>&nbsp;&nbsp;
                                            <asp:Label ID="lblCompState1" runat="server" Style="font-size: 12px;"></asp:Label>&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblCompCityPin1" runat="server" Style="font-size: 12px;"></asp:Label><br />
                                            <asp:Label ID="lblCompPhNo1" runat="server" Style="font-size: 12px;"></asp:Label>&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblFaxNo2" runat="server" Style="font-size: 12px;"></asp:Label><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="white_bg" valign="top" colspan="4" style="border-left-style: none;
                                            border-right-style: none">
                                            <strong>
                                                <asp:Label ID="Label13" runat="server" Font-Size="14px" Text="FREIGHT CUM TRANSIT CHALLAN"></asp:Label></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <table border="0" width="100%">
                                                <tr>
                                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;"
                                                        width="120px">
                                                        <asp:Label ID="Label14" Text="FCTC No." runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 0px;">
                                                        :
                                                    </td>
                                                    <td align="left" class="white_bg" style="font-size: 13px; border-right-style: none;
                                                        width: 321px;">
                                                        <b>
                                                            <asp:Label ID="lblChlnnoO" runat="server"></asp:Label></b>
                                                    </td>
                                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;"
                                                        width="80px">
                                                        <asp:Label ID="Label11" Text="Manual No." runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 0px;">
                                                        :
                                                    </td>
                                                    <td align="left" class="white_bg" style="font-size: 13px; border-right-style: none;
                                                        width: 180px;">
                                                        <b>
                                                            <asp:Label ID="lblMno" runat="server"></asp:Label></b>
                                                    </td>
                                                    <td align="right" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                        <asp:Label ID="Label16" Text="Date" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 0px;">
                                                        :
                                                    </td>
                                                    <td align="right" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;
                                                        width: 75px;">
                                                        <b>
                                                            <asp:Label ID="lblchlnDateo" runat="server"></asp:Label></b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <table border="0" width="100%">
                                                <tr>
                                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;"
                                                        width="120px">
                                                        <asp:Label ID="Label6" Text="Issuing Branch" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 0px;">
                                                        :
                                                    </td>
                                                    <td align="left" class="white_bg" style="font-size: 13px; border-right-style: none;
                                                        width: 321px;">
                                                        <b>
                                                            <asp:Label ID="lblIssueBranch" runat="server"></asp:Label></b>
                                                    </td>
                                                    <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;"
                                                        width="80px">
                                                        <asp:Label ID="Label8" Text="To" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 0px;">
                                                        :
                                                    </td>
                                                    <td align="left" class="white_bg" style="font-size: 13px; border-right-style: none;
                                                        width: 180px;">
                                                        <b>
                                                            <asp:Label ID="lbltoBranch" runat="server"></asp:Label></b>
                                                    </td>
                                                    <td align="right" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                                        <asp:Label ID="Label10" Text="Lorry No." runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 0px;">
                                                        :
                                                    </td>
                                                    <td align="right" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none;
                                                        width: 75px;">
                                                        <b>
                                                            <asp:Label ID="lblTrckNoO" runat="server"></asp:Label></b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 50%;" valign="top">
                                                        <table border="0" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                                            <tr>
                                                                <td colspan="2" style="border-bottom: 1px solid #484848;">
                                                                    <strong>&nbsp;Lorry Details:</strong>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 13px; height: 5px; border-right-style: none; width: 50%;">
                                                                    <asp:Label ID="Label20" Text="Lorry Owner " runat="server"></asp:Label>
                                                                </td>
                                                                <td style="font-size: 13px; height: 5px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblOwnrO" runat="server"></asp:Label>
                                                                    </b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 13px; height: 5px; border-right-style: none; width: 50%;">
                                                                    <asp:Label ID="Label22" Text="Address" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="font-size: 13px; height: 5px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblowneraddrss" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 13px; border-right-style: none; width: 50%;">
                                                                    <asp:Label ID="lblownmobile" runat="server" Text="Mobile"></asp:Label>
                                                                </td>
                                                                <td style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lbltxtownmobile" Text="" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 13px; border-right-style: none; width: 50%;">
                                                                    <asp:Label ID="lblpanno" runat="server" Text="Pan No."></asp:Label>
                                                                </td>
                                                                <td style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lbltxtpan" Text="" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 13px; border-right-style: none; width: 50%;">
                                                                    <asp:Label ID="Label23" runat="server" Text="Chasis No."></asp:Label>
                                                                </td>
                                                                <td style="font-size: 13px; border-right-style: none;">
                                                                    <b>
                                                                        <asp:Label ID="lblchasisno" Text="" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 13px; border-right-style: none; width: 50%;">
                                                                    <asp:Label ID="lblengineno" runat="server" Text="Engine No."></asp:Label>
                                                                </td>
                                                                <td style="font-size: 13px; border-right-style: none;">
                                                                    <b>
                                                                        <asp:Label ID="lbltxtengineno" Text="" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 13px; border-right-style: none; width: 50%;">
                                                                    <asp:Label ID="lblpermitno" runat="server" Text="Permit No."></asp:Label>
                                                                </td>
                                                                <td style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lbltxtpermit" Text="" runat="server"></asp:Label></b>&nbsp;&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 13px; border-right-style: none; width: 50%;">
                                                                    <asp:Label ID="lblpermitvalid" runat="server" Text="Permit Valid Upto"></asp:Label>
                                                                </td>
                                                                <td style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lbltxtpermitvalid" Text="" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 13px; border-right-style: none; width: 50%;">
                                                                    <asp:Label ID="lblmodel" runat="server" Text="Lorry Model"></asp:Label>
                                                                </td>
                                                                <td style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lbltxtmodel" Text="" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 50%;" valign="top">
                                                        <table border="0" width="100%" class="white_bg">
                                                            <tr>
                                                                <td colspan="2" style="border-bottom: 1px solid #484848; height: 10px;">
                                                                    <strong>Driver Details:</strong>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 13px; border-right-style: none; width: 50%;">
                                                                    <asp:Label ID="lblBroker" Text="Broker Name " runat="server"></asp:Label>
                                                                </td>
                                                                <td style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblBrokerName" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 13px; border-right-style: none; width: 50%;">
                                                                    <asp:Label ID="Label5" Text="Address " runat="server"></asp:Label>
                                                                </td>
                                                                <td style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblBrokerAddress" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr id="trDriverName" runat="server">
                                                                <td style="font-size: 13px; border-right-style: none; width: 50%;">
                                                                    <asp:Label ID="Label24" Text="Driver Name" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lbldrivername" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 13px; border-right-style: none; width: 50%;">
                                                                    <asp:Label ID="Label25" Text="Address" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lbldriverAddress" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 13px; border-right-style: none; width: 50%;">
                                                                    <asp:Label ID="lbldrivermobile" runat="server" Text="Mobile"></asp:Label>
                                                                </td>
                                                                <td style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblmobtextdriver" Text="" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 13px; border-right-style: none; width: 50%;">
                                                                    <asp:Label ID="lbldriverlicence" runat="server" Text="License No."></asp:Label>
                                                                </td>
                                                                <td style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lbltxtdrvlicenceno" Text="" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 13px; border-right-style: none; width: 50%;">
                                                                    <asp:Label ID="lblvalidupto" runat="server" Text="License Valid Upto"></asp:Label>
                                                                </td>
                                                                <td style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lbltxtvalidupto" Text="" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 13px; border-right-style: none; width: 50%;">
                                                                    <asp:Label ID="lblinsured" runat="server" Text="Insured Name"></asp:Label>
                                                                </td>
                                                                <td style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lbltxtinsured" Text="" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 13px; border-right-style: none; width: 50%;">
                                                                    <asp:Label ID="lblpolicyno" runat="server" Text="Policy No."></asp:Label>
                                                                </td>
                                                                <td style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lbltxtpolicyno" Text="" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 13px; border-right-style: none; width: 50%;">
                                                                    <asp:Label ID="lblOwnerMobile" runat="server" Text="Owner's Mobile No."></asp:Label>
                                                                </td>
                                                                <td style="font-size: 13px; border-right-style: none">
                                                                    <b>
                                                                        <asp:Label ID="lblOwnerMobileNo" Text="" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 13px; border-right-style: none; width: 50%;">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="font-size: 13px; border-right-style: none">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="white_bg" valign="top" colspan="4" style="border-left-style: none;
                                            border-right-style: none">
                                            <strong>
                                                <asp:Label ID="Label9" runat="server" Font-Size="14px" Text="Consignment Details"></asp:Label></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table2">
                                                <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <tr>
                                                            <td style="font-size: 12px;" width="8%">
                                                                <strong>Manual No.</strong>
                                                            </td>
                                                            <td style="font-size: 12px;" width="8%">
                                                                <strong>GR Dt.</strong>
                                                            </td>
                                                            <td style="font-size: 12px;" align="center" width="10%">
                                                                <strong>Pkgs </strong>
                                                            </td>
                                                            <td style="font-size: 12px;" align="right" width="10%">
                                                                <strong>Wt.</strong>
                                                            </td>
                                                            <td class="white_bg" width="4%">
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                            <td style="font-size: 12px;" width="10%">
                                                                <strong>Destination</strong>
                                                            </td>
                                                            <td style="font-size: 12px;" width="10%">
                                                                <strong>Consignor</strong>
                                                            </td>
                                                            <div id="hide" runat="server">
                                                                <td style="font-size: 12px;" width="10%" align="center">
                                                                    <asp:Label ID="dr" runat="server">
                                                                    </asp:Label>
                                                                    <strong>Item</strong>
                                                                </td>
                                                                <td style="font-size: 12px;" width="10%" align="center">
                                                                    <asp:Label Visible="false" ID="rashi" runat="server"></asp:Label>
                                                                    <strong>Inv. No. </strong>
                                                                </td>
                                                                <td style="font-size: 12px;" width="10%" align="right">
                                                                    <asp:Label ID="totalrashi" runat="server">
                                                                    </asp:Label>
                                                                    <strong>Inv. Value</strong>
                                                                </td>
                                                            </div>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="white_bg" width="8%">
                                                                <%#Eval("MN_No")%>
                                                            </td>
                                                            <td class="white_bg" width="8%">
                                                                <%#(string.IsNullOrEmpty(Eval("GRRet_Date").ToString()) ? "" : Convert.ToDateTime(Eval("GRRet_Date")).ToString("dd-MM-yyyy"))%>
                                                            </td>
                                                            <td class="white_bg" width="10%" align="center">
                                                                <%#Eval("Qty")%>
                                                            </td>
                                                            <td class="white_bg" width="10%" align="right">
                                                                <%#Eval("Weight")%>
                                                            </td>
                                                            <td class="white_bg" width="4%">
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                            <td class="white_bg" width="10%">
                                                                <%#Eval("Delv_City")%>
                                                            </td>
                                                            <td class="white_bg" width="10%">
                                                                <%#Eval("SenderName_Eng")%>
                                                            </td>
                                                            <td class="white_bg" width="10%" align="center">
                                                                <%#Eval("Item_Name")%>
                                                                <asp:Label ID="amount" runat="server" Visible="false">
                                                    <%#string.Format("{0:0.00}", Convert.ToDouble(Eval("WithoutUnloading_Amnt")))%></asp:Label>
                                                            </td>
                                                            <td class="white_bg" width="10%" align="center">
                                                                <%#Eval("Ref_No")%>
                                                                <asp:Label ID="WagesAmnt" runat="server" Visible="false">
                                                    <%#string.Format("{0:0.00}", Convert.ToDouble(Eval("Wages_Amnt")))%></asp:Label>
                                                            </td>
                                                            <td class="white_bg" width="10%" align="right">
                                                                <asp:Label ID="Totalamount" runat="server">
                                                    <%#string.Format("{0:0.00}", Convert.ToDouble(Eval("Total_Price")))%>
                                                                </asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <tr id="footer" runat="server" visible="true">
                                                            <td style="font-size: 12px; border-top: black solid 2px;" width="7%;">
                                                                <asp:Label ID="lblttl" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="font-size: 12px; border-top: black solid 2px;" width="10%">
                                                            </td>
                                                            <td style="font-size: 12px; border-top: black solid 2px;" align="center" width="5%">
                                                                <asp:Label ID="lbltotalqty" Visible="true" Font-Bold="true" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="font-size: 12px; border-top: black solid 2px;" align="right" width="5%">
                                                                <asp:Label ID="lbltotalWeight" Font-Bold="true" Visible="true" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="font-size: 12px; border-top: black solid 2px;" width="10%">
                                                            </td>
                                                            <td style="font-size: 12px; border-top: black solid 2px;" width="7%">
                                                            </td>
                                                            <td style="font-size: 12px; border-top: black solid 2px;" width="7%">
                                                            </td>
                                                            <td style="font-size: 12px; border-top: black solid 2px;" align="right" width="5%">
                                                                <asp:Label ID="Label4" Font-Bold="true" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="font-size: 12px; border-top: black solid 2px;" width="10%" align="left">
                                                            </td>
                                                            <td style="font-size: 12px; border-top: black solid 2px;" width="10%" align="right">
                                                                <asp:Label ID="lblTotalAmnt" Font-Bold="true" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="font-size: 12px; border-top: black solid 2px;" width="10%" align="right">
                                                            </td>
                                                            <td style="font-size: 12px;" width="0%" align="right">
                                                                <asp:Label ID="lblAmount" Font-Bold="true" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                            <td style="font-size: 12px;" width="0%" align="right">
                                                                <asp:Label ID="lblWagesAmnt" Font-Bold="true" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="white_bg" valign="top" colspan="4" style="border-left-style: none;
                                            border-right-style: none">
                                            <strong>
                                                <asp:Label ID="Label26" runat="server" Font-Size="14px" Text="Freight Payment Details"></asp:Label></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <table border="0" cellspacing="0" style="font-size: 13px" width="100%" id="Table3">
                                                <tr>
                                                    <td style="font-size: 13px;">
                                                        Starting Km.
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                        <asp:Label ID="lblStartKM" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 13px;">
                                                        Closing Km.
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                        <asp:Label ID="lblCloseKm" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                        Rate Per Km.
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                        <asp:Label ID="lblRatePKm" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                        Freight
                                                    </td>
                                                    <td style="font-size: 13px;" align="right">
                                                        <asp:Label ID="lbltotalfreight" runat="server" Visible="true" Font-Size="13px" valign="right"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 13px;">
                                                        Total Km.
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                        <asp:Label ID="lblTotalKM" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                        Late D&D
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                        <asp:Label ID="lblLateDnD" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                        Less Adv.
                                                    </td>
                                                    <td style="font-size: 13px;" align="right">
                                                        <asp:Label ID="valuelblAdvanceAmntO" Visible="true" runat="server" Font-Size="13px"
                                                            valign="right"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 13px;">
                                                        BKG Dt.
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                        <asp:Label ID="lblgrdate" Visible="true" runat="server" Font-Size="13px" valign="right"> </asp:Label>
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                        Rate Amnt.
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                        <asp:Label ID="lblRateAmnt" Visible="true" runat="server" Font-Size="13px" valign="right"> </asp:Label>
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                        Hamali
                                                    </td>
                                                    <td style="font-size: 13px;" align="right">
                                                        <asp:Label ID="lblhamali" runat="server" Font-Size="13px" valign="right"> </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 13px;">
                                                        Wt.
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                        <asp:Label ID="lbltotalweight" runat="server" Visible="true" Font-Size="13px" valign="right"> </asp:Label>
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                        Freight
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                        <asp:Label ID="lblfreightamount" runat="server" Visible="true" Font-Size="13px" valign="right"> </asp:Label>
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                        Detention
                                                    </td>
                                                    <td style="font-size: 13px; text-align: right;">
                                                        <asp:Label ID="lblDetention" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 13px;">
                                                        Dly Dt.
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                        <asp:Label ID="lblDlyDate" runat="server" Visible="true" Font-Size="13px" valign="right"> </asp:Label>
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                        Less TDS
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                        <asp:Label ID="valueLblTdsAmntO" runat="server" Font-Size="13px" valign="right"></asp:Label>
                                                    </td>
                                                    <td style="font-size: 13px;">
                                                        Balance Payment
                                                    </td>
                                                    <td style="font-size: 13px;" align="right">
                                                        <asp:Label ID="valuelblnetTotalO" Visible="true" runat="server" Font-Size="13px"
                                                            valign="right"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" colspan="4">
                                            <table width="100%" style="font-size: 12px" border="0" cellspacing="0">
                                                <tr style="line-height: 25px">
                                                    <td colspan="9" style="font-size: 13px" align="left" class="white_bg">
                                                        <table width="100%">
                                                            <tr>
                                                                <td colspan="9" style="height: 30px;">
                                                                    Remarks:एम.पी.,यू.पी.,ए.पी. की बहती मिलने पर बकाया भाढा मीलेगा |
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="9" style="height: 30px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" colspan="1" class="white_bg" style="font-size: 13px; text-align: left;">
                                                                    Driver's Signature
                                                                </td>
                                                                <td align="left" valign="baseline" colspan="1" class="white_bg" style="font-size: 13px;
                                                                    text-align: center;">
                                                                    Operation Clerk
                                                                </td>
                                                                <td align="left" valign="baseline" colspan="1" class="white_bg" style="font-size: 13px;
                                                                    text-align: right;">
                                                                    Accountant
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </div>

    <table width="100%">
        <tr style="display: none">
            <td class="white_bg" align="center">
                <div id="printf" style="font-size: 13px;">
                    <table width="100%" border="1" cellspacing="0" cellpadding="0" style="margin: 0 auto; text-align: left;">
                        <tr>
                            <td colspan="4" style="width: 80%; padding-bottom: 35px; text-align: center;"><span style="font-size: 40px;"><u><asp:Label ID="lblCompName" runat="server"></asp:Label></u></span><br />
                                Head office :- <asp:Label ID="lblAdd1" runat="server"></asp:Label>, <asp:Label ID="lblcity" runat="server"></asp:Label><br />
                                STATE:- <asp:Label ID="lblstate" runat="server"></asp:Label> (CODE :-<asp:Label ID="bllgstcd" runat="server"></asp:Label>)<br />
                                MOBILE NO.:- <asp:Label ID="lblmobile" runat="server"></asp:Label>
                            </td>
                            <td style="padding-bottom: 30px; text-align: center;">Challan No:
                                <asp:Label ID="lblchallan" runat="server"></asp:Label><br />
                                Date:
                                <asp:Label ID="lbldate" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center; line-height: 30px;"><b>Payment Voucher</b></td>
                            <td style="text-align: right;"><b>Rs.</b></td>
                        </tr>
                        <tr>
                            <td colspan="4" style="line-height: 30px;">Truck No:
                                <asp:Label ID="lblTruckno" runat="server"></asp:Label>
                                ! Truck Owner:
                                <asp:Label ID="lbltruckowner" runat="server"></asp:Label></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="4" style="line-height: 30px;">Village :
                                <asp:Label ID="lblvillage" runat="server"></asp:Label>
                                ! Talika :
                                <asp:Label ID="lbltalika" runat="server"></asp:Label>
                                ! District Name :
                                <asp:Label ID="lbldist" runat="server"></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="4"><b>On Account of Cement - Advance Payment </b></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="4" style="line-height: 30px;">Rate PMT :<u><asp:Label ID="lblrate" runat="server"></asp:Label>
                            </u>WT :
                                <asp:Label ID="lblwt" runat="server"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="4" style="line-height: 30px; text-align: right;">Total Fare :</td>
                            <td style="text-align: right;">
                                <asp:Label ID="lbltotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="line-height: 30px; text-align: right;">Payment Detalls :
                                <asp:Label ID="lblpaymentde" runat="server"></asp:Label>
                                Cash (Advance) : </td>
                            <td style="text-align: right;">
                                <asp:Label ID="lblcash" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="line-height: 30px;">Diesel:</td>
                            <td style="text-align: right;">
                                <asp:Label ID="Lbldesel" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="line-height: 30px;">Commission : </td>
                            <td style="text-align: right;">
                                <asp:Label ID="lblcomssion" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="4" style="line-height: 30px;">Total Due : </td>
                            <td style="text-align: right;">
                                <asp:Label ID="lbltotaldue" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="5" style="text-align: right; padding-top: 35px;">Signature :<u><span style="color: #fff;">__________________</span></u></td>
                        </tr>
                      
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <!-- popup form Fuel detail -->

    <div id="dvfuel" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="popform_header">Fuel  Detail </h4>
                </div>
               <%-- <asp:UpdatePanel ID="uphg" runat="server">
                    <ContentTemplate>--%>
                        <div class="modal-body">
                            <section class="panel panel-default full_form_container material_search_pop_form">
									<div class="panel-body">
										<!-- First Row start -->
									<div class="clearfix odd_row">	                                
	                                <div class="col-sm-6">
	                                    <label class="col-sm-4 control-label">Petrol Pump</label>
                                        <div class="col-sm-8">
                                            <asp:HiddenField ID="hidrowidno" runat="server" />
                                           <asp:DropDownList ID="ddlacntname" runat="server" CssClass="form-control" ></asp:DropDownList>
                                             <span class="red" style="color: #ff0000">
                                            <asp:RequiredFieldValidator ID="rfvacntname" runat="server" ControlToValidate="ddlacntname"
                                            CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select Account Name!"
                                            InitialValue="0" SetFocusOnError="true" ValidationGroup="savef" ></asp:RequiredFieldValidator> </span>  
                                        </div>
	                                </div>
                                    <div class="col-sm-6">
	                                    <label class="col-sm-4 control-label">Item Name</label>
                                        <div class="col-sm-8">
                                           <asp:DropDownList ID="ddlitemname" runat="server" CssClass="form-control" ></asp:DropDownList>
                                            <span class="red" style="color: #ff0000">
                                            <asp:RequiredFieldValidator ID="rfvitemname" runat="server" ControlToValidate="ddlitemname"
                                            CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select Item Name!"
                                            InitialValue="0" SetFocusOnError="true" ValidationGroup="savef"></asp:RequiredFieldValidator> </span> 
                                        </div>
	                                </div>
                                    </div>
                                     <div class="clearfix even_row">	     
	                                <div class="col-sm-2"> 
	                                    <label class="col-sm-4 control-label">QTY</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtQTY" runat="server" Text="0" CssClass="form-control" style="text-align:right" onchange="javascript:CalcfuelAmnt();" onkeyup="mul()" ></asp:TextBox> 
                                            <%-- <span class="red" style="color: #ff0000">
                                            <asp:RequiredFieldValidator ID="rfvqty" runat="server" ControlToValidate="txtQTY"
                                            CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter QTY!"
                                            InitialValue="0" SetFocusOnError="true" ValidationGroup="savef"></asp:RequiredFieldValidator> </span> --%>
                                        </div>
	                                </div>
                                    <div class="col-sm-3">
	                                    <label class="col-sm-4 control-label">RATE</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtrate" runat="server" Text="0" CssClass="form-control" style="text-align:right" OnTextChanged="txtrate_TextChanged" onchange="javascript:CalcfuelAmnt();"  onkeyup="mul()" ></asp:TextBox>
                                            <%-- <span class="red" style="color: #ff0000">
                                            <asp:RequiredFieldValidator ID="rfvrate" runat="server" ControlToValidate="txtrate"
                                            CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter rate !"
                                            InitialValue="0" SetFocusOnError="true" ValidationGroup="savef"></asp:RequiredFieldValidator> </span> --%>
                                        </div>
	                                </div>
                                    <div class="col-sm-4">
	                                	<label class="col-sm-5 control-label">Amount</label>
                                    <div class="col-sm-7">
                                     <asp:TextBox ID="txtamount" runat="server" CssClass="form-control"  Text="0"  onchange="javascript:CalcfuelAmnt();"></asp:TextBox>
                                    </div>
	                                </div>
	                                <div class="col-sm-3">
	                                <div class="col-sm-12 prev_fetch">
                                    <asp:LinkButton ID="lnksub" CssClass="btn full_width_btn btn-sm btn-primary"  runat="server" CausesValidation="true" ValidationGroup="savef" OnClick="lnkbtnsubmt_Click" >Submit</asp:LinkButton>
	                               </div>
	                                </div>
	                              </div> 
	                             <div class="clearfix third_right">
                      <section class="panel panel-in-default">                            
                        <div class="panel-body" style="overflow:auto; height:400px;">     
	                            <asp:GridView ID="grdmainFuel" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="display nowrap dataTable"
                                    Width="100%" BorderStyle="None" AllowPaging="false" PageSize="100" BorderWidth="0" OnRowCommand="grdmainFuel_RowCommand" OnRowDataBound="grdmainFuel_RowDataBound">
                                 <RowStyle CssClass="odd" />
                                 <AlternatingRowStyle CssClass="even" />    
                                    <Columns>
                                       <asp:TemplateField HeaderText="Account Name " HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                               <asp:Label ID="lblacntname" runat="server" Text='<%#Eval("acnt_name")%>'></asp:Label>
                                                 <asp:HiddenField ID="hidacntId" runat="server" Value='<%#Eval("acnt_idno")%>' />
                                                 <asp:HiddenField ID="hidChlnIdno"  runat="server" Value='<%#Eval("Chln_Idno")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                              <asp:Label ID="lblitemname" runat="server" Text='<%#Eval("itemname")%>'></asp:Label>
                                              <asp:HiddenField ID="hidItemIdno" runat="server" Value='<%#Eval("itemidno")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px" HeaderText="QTY">
                                            <ItemStyle HorizontalAlign="Left" Width="180px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblqty" runat="server" Text='<%#Eval("Qty")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px" HeaderText="Rate">
                                            <ItemStyle HorizontalAlign="Left" Width="180px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblrate" runat="server" Text='<%#Eval("Rate")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px" HeaderText="Amount">
                                            <ItemStyle HorizontalAlign="Left" Width="180px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblamnt" runat="server" Text='<%#Eval("Amt")%>'></asp:Label>
                                            </ItemTemplate>
                                          <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblamt" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="40" 
                                            HeaderText="Action">
                                            <ItemStyle HorizontalAlign="Center" Width="40" />
                                            <ItemTemplate>  
                                           <asp:LinkButton ID="LinkEdit" class="fa fa-edit icon" CommandName="cmdeditfuel" CommandArgument='<%#Eval("id")%>'
                                                                AlternateText="Edit" ToolTip="Edit"  runat="server"></asp:LinkButton>

                                           <asp:LinkButton ID="LinkDelete" class="fa fa-trash-o" CommandName="cmddeletefuel" CommandArgument='<%#Eval("id")%>'
                                                                AlternateText="Delete" ToolTip="Delete" OnClientClick="return confirm('Do you want to delete this record ?');"  runat="server"></asp:LinkButton>
                                          </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        Records(s) not found.
                                    </EmptyDataTemplate>
                                </asp:GridView>
	                              </div>	                              
								 </section>
                                 </div>
                                 <div class="modal-footer">
                                    <div class="popup_footer_btn">
                                       <%-- <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-dark"  OnClick="lnkbtnOk_Click"
                                            TabIndex="89"><i class="fa fa-check"></i>Save</asp:LinkButton>--%>
                                        <asp:LinkButton ID="lnkbtnClose" runat="server" CssClass="btn btn-dark" data-dismiss="modal"><i class="fa fa-times"></i>Close</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </section>
                        </div> 
                      <asp:HiddenField ID="hidfuelId" runat="server" />
                   <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfTruckNoId" runat="server" />
    <asp:HiddenField ID="hidToggleDatePrint" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hidLastChlnId" runat="server" />
    <asp:HiddenField ID="hidPages" runat="server" />
    <asp:HiddenField ID="hdnType" runat="server" />
    <asp:HiddenField ID="hideimgvalue" runat="server" />
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
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

        var isShift = false;
        var seperator = "-";
        function DateFormat(txt, keyCode) {
            if (keyCode == 16)
                isShift = true;
            //Validate that its Numeric
            if (((keyCode >= 48 && keyCode <= 57) || keyCode == 8 ||
         keyCode <= 37 || keyCode <= 39 || keyCode == 32 ||
         (keyCode >= 96 && keyCode <= 105)) && isShift == false) {
                if ((txt.value.length == 2 || txt.value.length == 5) && keyCode != 8) {
                    txt.value += seperator;
                }
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        window.onload = function () {
            if (parseFloat(document.getElementById("<%=txtAdvAmnt.ClientID %>").value) > 0) {
                document.getElementById("<%=ddlRcptType.ClientID %>").disabled = false;
                document.getElementById("<%=txtInstNo.ClientID %>").disabled = false;
                document.getElementById("<%=txtInstDate.ClientID %>").disabled = false;
                document.getElementById("<%=ddlCusBank.ClientID %>").disabled = false;
            }
            else {
                document.getElementById("<%=ddlRcptType.ClientID %>").value = document.getElementById("<%=ddlCusBank.ClientID %>").value = "0";
                document.getElementById("<%=txtInstNo.ClientID %>").value = document.getElementById("<%=txtInstDate.ClientID %>").value = "";
                document.getElementById("<%=ddlRcptType.ClientID %>").disabled = true;
                document.getElementById("<%=txtInstNo.ClientID %>").disabled = true;
                document.getElementById("<%=txtInstDate.ClientID %>").disabled = true;
                document.getElementById("<%=ddlCusBank.ClientID %>").disabled = true;

                ValidatorEnable(document.getElementById("<%=rfvCusBank.ClientID %>"), false);
                document.getElementById("<%=rfvCusBank.ClientID%>").style.visibility = "hidden";
                ValidatorEnable(document.getElementById("<%=rfvRcptType.ClientID %>"), false);
                document.getElementById("<%=rfvRcptType.ClientID%>").style.visibility = "hidden";
                ValidatorEnable(document.getElementById("<%=rfvinstno.ClientID %>"), false);
                document.getElementById("<%=rfvinstno.ClientID%>").style.visibility = "hidden";
                ValidatorEnable(document.getElementById("<%=rfvinstDate.ClientID %>"), false);
                document.getElementById("<%=rfvinstDate.ClientID%>").style.visibility = "hidden";
            }
        }
        function MoveValue()
        {
            var src =  document.getElementById('<%= txtHireAmnt.ClientID%>');
            var dest = document.getElementById('<%= txtGrosstotal.ClientID%>');
            if($(src).is(":visible")){ 
                dest.value = src.value;
            }
            else { src.value == "0.00"; }
            CalcAmnt();
        }
        function CalcAmnt() {

            if (document.getElementById("<%=txtAdvAmnt.ClientID %>").value == '') {

                document.getElementById("<%=txtAdvAmnt.ClientID %>").value = "0.00";
            }

            if (document.getElementById("<%=txtRassaTripal.ClientID %>").value == '') {
                document.getElementById("<%=txtRassaTripal.ClientID %>").value = "0.00";
            }

            if (document.getElementById("<%=txtDieselAmnt.ClientID %>").value == '') {
                document.getElementById("<%=txtDieselAmnt.ClientID %>").value = "0.00";
            }

            if (document.getElementById("<%=txtcommission.ClientID %>").value == '') {
                document.getElementById("<%=txtcommission.ClientID %>").value = "0.00";
            }


            var Grosstotal = 0; var commission = 0; var TdsAmnt = 0; var DieselAmnt = 0; var Totalamnt; var AdvAmtnt = 0; var LateAmnt = 0; var Hamali = 0; var Detention = 0; var total = 0;
            var RassaTripal = 0;
            if (document.getElementById("<%=txtGrosstotal.ClientID %>").value != "") { Grosstotal = document.getElementById("<%=txtGrosstotal.ClientID %>").value; }
            if (document.getElementById("<%=txtcommission.ClientID %>").value != "") { commission = document.getElementById("<%=txtcommission.ClientID %>").value; }
            if (document.getElementById("<%=txtTdsAmnt.ClientID %>").value != "") { TdsAmnt = document.getElementById("<%=txtTdsAmnt.ClientID %>").value; }
            if (document.getElementById("<%=txtAdvAmnt.ClientID %>").value != "") { AdvAmtnt = document.getElementById("<%=txtAdvAmnt.ClientID %>").value; }
            if (document.getElementById("<%=txtDieselAmnt.ClientID %>").value != "") { DieselAmnt = document.getElementById("<%=txtDieselAmnt.ClientID %>").value; }
            if (document.getElementById("<%=txtLatecharge.ClientID %>").value != "") { LateAmnt = document.getElementById("<%=txtLatecharge.ClientID %>").value; }
            if (document.getElementById("<%=txtHamaliCharge.ClientID %>").value != "") { Hamali = document.getElementById("<%=txtHamaliCharge.ClientID %>").value; }
            if (document.getElementById("<%=txtDetention.ClientID %>").value != "") { Detention = document.getElementById("<%=txtDetention.ClientID %>").value; }
            if (document.getElementById("<%=txtRassaTripal.ClientID %>").value != "") { RassaTripal = document.getElementById("<%=txtRassaTripal.ClientID %>").value; }

            RassaTripal = RassaTripal.split(',').join('');

            commission = commission.split(',').join('');
            AdvAmtnt = AdvAmtnt.split(',').join('');

            TdsAmnt = TdsAmnt.split(',').join('');
            DieselAmnt = DieselAmnt.split(',').join('');

            total = (parseFloat(commission) + parseFloat(AdvAmtnt) + parseFloat(TdsAmnt) + parseFloat(DieselAmnt) + parseFloat(LateAmnt)+parseFloat(RassaTripal));


            Grosstotal = Grosstotal.split(',').join('');
            //alert(Grosstotal);
            var Total1 = (parseFloat(Grosstotal) + parseFloat(Detention) + parseFloat(Hamali));
            //alert(Total1);


            //Total1 = Total1.split(',').join('');
            document.getElementById("<%=txtNetAmnt.ClientID %>").value = (parseFloat(parseFloat(Total1) - parseFloat(total))).toFixed(2);


            if ((document.getElementById("<%=txtNetAmnt.ClientID %>").value < 0)) {
                document.getElementById("<%=txtNetAmnt.ClientID %>").value = "0.00";
            }

            if (parseFloat(document.getElementById("<%=txtAdvAmnt.ClientID %>").value) > 0) {

                OpenText();
                document.getElementById("<%=ddlRcptType.ClientID %>").disabled = false;
                document.getElementById("<%=txtInstNo.ClientID %>").disabled = false;
                document.getElementById("<%=txtInstDate.ClientID %>").disabled = false;
                document.getElementById("<%=ddlCusBank.ClientID %>").disabled = false;
            }
            else {
                document.getElementById("<%=ddlRcptType.ClientID %>").value = document.getElementById("<%=ddlCusBank.ClientID %>").value = "0";
                document.getElementById("<%=txtInstNo.ClientID %>").value = document.getElementById("<%=txtInstDate.ClientID %>").value = "";
                document.getElementById("<%=ddlRcptType.ClientID %>").disabled = true;
                document.getElementById("<%=txtInstNo.ClientID %>").disabled = true;
                document.getElementById("<%=txtInstDate.ClientID %>").disabled = true;
                document.getElementById("<%=ddlCusBank.ClientID %>").disabled = true;

                ValidatorEnable(document.getElementById("<%=rfvCusBank.ClientID %>"), false);
                document.getElementById("<%=rfvCusBank.ClientID%>").style.visibility = "hidden";
                ValidatorEnable(document.getElementById("<%=rfvRcptType.ClientID %>"), false);
                document.getElementById("<%=rfvRcptType.ClientID%>").style.visibility = "hidden";
                ValidatorEnable(document.getElementById("<%=rfvinstno.ClientID %>"), false);
                document.getElementById("<%=rfvinstno.ClientID%>").style.visibility = "hidden";
                ValidatorEnable(document.getElementById("<%=rfvinstDate.ClientID %>"), false);
                document.getElementById("<%=rfvinstDate.ClientID%>").style.visibility = "hidden";

            }

        }
        
    </script>
    <script type="text/javascript">
        function Enable() {
            if ((parseFloat(document.getElementById("<%=txtRatePerKM.ClientID %>").value) == 0) && (parseFloat(document.getElementById("<%=txtFreight.ClientID %>").value) == 0)) {
                document.getElementById("<%=txtRatePerKM.ClientID %>").removeAttribute("disabled");
                document.getElementById("<%=txtFreight.ClientID %>").removeAttribute("disabled");
            }
            if (parseFloat(document.getElementById("<%=txtRatePerKM.ClientID %>").value) > 0) {
                document.getElementById("<%=txtFreight.ClientID %>").setAttribute("disabled", true);
                document.getElementById("<%=txtRatePerKM.ClientID %>").removeAttribute("disabled");
            }
            else if (parseFloat(document.getElementById("<%=txtFreight.ClientID %>").value) > 0) {
                document.getElementById("<%=txtRatePerKM.ClientID %>").setAttribute("disabled", true);
                document.getElementById("<%=txtFreight.ClientID %>").removeAttribute("disabled");
            }
        }
    </script>
    <script type="text/javascript">
        function OpenText() {
            // using for current date by salman
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            var today = dd + '-' + mm + '-' + yyyy;

            var cust = $("#<%=ddlRcptType.ClientID%>").val();
            if (cust != 0) {
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    url: "ChlnBooking.aspx/ProductList",
                    data: JSON.stringify({ cust: cust }),
                    success: function (msg) {
                        $.each(msg.d, function (index, item) {
                            CalcAmnt();
                            if (item == 4) {
                                document.getElementById("<%=rfvinstno.ClientID %>").disabled = false;
                                document.getElementById("<%=txtInstNo.ClientID %>").disabled = false;
                                document.getElementById("<%=rfvinstDate.ClientID %>").disabled = false;
                                document.getElementById("<%=txtInstDate.ClientID %>").disabled = false;
                                document.getElementById("<%=ddlRcptType.ClientID %>").disabled = false;
                                document.getElementById("<%=ddlCusBank.ClientID %>").disabled = false;
                                document.getElementById("<%=rfvRcptType.ClientID%>").style.visibility = "visible";
                                document.getElementById("<%=rfvRcptType.ClientID %>").disabled = false;
                                document.getElementById("<%=rfvinstno.ClientID%>").style.visibility = "visible";
                                document.getElementById("<%=rfvinstno.ClientID %>").disabled = false;
                                document.getElementById("<%=rfvCusBank.ClientID%>").style.visibility = "visible";
                                document.getElementById("<%=rfvCusBank.ClientID %>").disabled = false;
                                document.getElementById("<%=rfvinstDate.ClientID%>").style.visibility = "visible";
                                document.getElementById("<%=rfvinstDate.ClientID %>").disabled = false;
                                document.getElementById("<%=txtInstDate.ClientID %>").value = today;
                            }
                            else {
                                document.getElementById("<%=rfvinstno.ClientID %>").disabled = true;
                                document.getElementById("<%=txtInstNo.ClientID %>").disabled = true;
                                document.getElementById("<%=rfvinstDate.ClientID %>").disabled = true;
                                document.getElementById("<%=txtInstDate.ClientID %>").disabled = true;
                                document.getElementById("<%=ddlRcptType.ClientID %>").disabled = false;
                                ValidatorEnable(document.getElementById("<%=rfvCusBank.ClientID %>"), false);
                                document.getElementById("<%=rfvCusBank.ClientID%>").style.visibility = "hidden";
                                ValidatorEnable(document.getElementById("<%=rfvRcptType.ClientID %>"), false);
                                document.getElementById("<%=rfvRcptType.ClientID%>").style.visibility = "hidden";
                                ValidatorEnable(document.getElementById("<%=rfvinstno.ClientID %>"), false);
                                document.getElementById("<%=rfvinstno.ClientID%>").style.visibility = "hidden";
                                ValidatorEnable(document.getElementById("<%=rfvinstDate.ClientID %>"), false);
                                document.getElementById("<%=rfvinstDate.ClientID%>").style.visibility = "hidden";
                                document.getElementById("<%=ddlCusBank.ClientID %>").disabled = true;
                                document.getElementById("<%=ddlCusBank.ClientID %>").value = "0";
                                document.getElementById("<%=txtInstNo.ClientID %>").value = "";
                                document.getElementById("<%=txtInstDate.ClientID %>").value = "";
                            }

                        });
                    },
                    failure: function (msg) { }
                });

            }
        }
    
    
    </script>
    <script language="javascript" type="text/javascript">
        function Divopen() {
            $('#Amount1').modal('show');
        }
    </script>
    <script language="javascript" type="text/javascript">
        function CallPrintOM(strid) {
            var prtContent = "";
            var Pages = "1";
            Pages = document.getElementById("<%=hidPages.ClientID%>").value;
            var prtContent3 = "<p style='page-break-before: always'></p>";
            for (i = 0; i < Pages; i++) {
                prtContent = prtContent + "<table width='100%' border='0'></table>";
                if (Pages != 1) {
                    prtContent = prtContent + "<tr><td><strong>" + ((i == 1) ? "[HO Copy]" : (i == 2) ? "[Extra Copy]" : "[Driver Copy]") + "</strong> <a style='float:right'>" + GetDateTime() + "</a></td></tr>";
                }
                var prtContent1 = document.getElementById(strid);
                var prtContent2 = prtContent1.innerHTML;
                prtContent = prtContent + prtContent2 + ((i < 3) ? prtContent3 : "");
            }
            var WinPrint = window.open('', '', 'left=0,top=0,width=800,height=800,toolbar=1,scrollbars=1,status=1');
            WinPrint.document.write(prtContent);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            //WinPrint.close();
            return false;
        }

        function ToggleDatePrint() {
            if ($('#hidToggleDatePrint').val() == "1") {
                $('#hidToggleDatePrint').val('0');
                $('.print-date-time').hide();
            }
            else {
                $('#hidToggleDatePrint').val('1');
                $('.print-date-time').show();
            }
        }
        function GetDateTime() {
            var currentdate = new Date();
            var datetime = "Print at: " + currentdate.getDate() + "/"
                + (currentdate.getMonth() + 1) + "/"
                + currentdate.getFullYear() + " - "
                + currentdate.getHours() + ":"
                + currentdate.getMinutes() + ":"
                + currentdate.getSeconds();
            if ($('#hidToggleDatePrint').val() == "1") {
                return datetime;
            }
            else {
                return "";
            }

        }     
    </script>
    <script type="text/javascript">
        function ClientItemSelected(sender, e) {
            $get("<%=hfTruckNoId.ClientID %>").value = e.get_value();
        }         
    </script>
    <script language="javascript" type="text/javascript">
        function CallPrintf(strid) {
            var prtContent1 = "<table width='100%' border='0'></table>";
            var prtContent = document.getElementById(strid);
            var WinPrintf = window.open('', '', 'left=0,top=0,width=800,height=800,toolbar=1,scrollbars=1,status=1');
            WinPrintf.document.write(prtContent1);
            WinPrintf.document.write(prtContent.innerHTML);
            //WinPrintf.document.close();
            WinPrintf.focus();
            WinPrintf.print();
            //WinPrintf.close();
            return false;
        }
    </script>

      <script type="text/javascript" language="javascript">
          function mul() {
              var txtFirstNo = document.getElementById('<%=txtQTY.ClientID %>').value;
              var txtSecondNo = document.getElementById('<%=txtrate.ClientID%>').value;
              var result = parseInt(txtFirstNo) * parseInt(txtSecondNo);
              if (!isNaN(result)) {
                  document.getElementById('<%=txtamount.ClientID%>').value = result;
              }
          }
    </script>

</asp:Content>
