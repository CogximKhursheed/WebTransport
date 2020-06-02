<%@ Page Title="Voucher Checklist Report" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="VchrChkList.aspx.cs" Inherits="WebTransport.VchrChkList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-9">
            <section class="panel panel-default full_form_container part_purchase_bill_form">
                <header class="panel-heading font-bold">Voucher Checklist Report
                	<span class="view_print"> &nbsp;
                        <%--<asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ToolTip="Export to excel" ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px" OnClick="imgBtnExcel_Click" Visible="false" /></span>--%>
               	</header>
                <div class="panel-body">
                <form class="bs-example form-horizontal">
                    <!-- first  section --> 
                    <div class="clearfix first_section">
	                    <section class="panel panel-in-default">  
	                        <div class="panel-body">
	                        <div class="clearfix odd_row">
                            <div class="col-sm-5" style="width: 40%">
                                <label class="col-sm-4 control-label" style="width: 29%;">Date Range<span class="required-field">*</span></label>
                                <div class="col-sm-8" style="width: 71%;">
                                <asp:DropDownList ID="ddlDateRange" runat="server" AutoPostBack="True" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged">
                                </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-4" style="width: 25%">
                                <label class="col-sm-5 control-label" style="width: 39%;">Date From</label>
                                <div class="col-sm-7" style="width: 61%;">
                                    <asp:TextBox ID="Datefrom" runat="server" CssClass="input-sm datepicker form-control" MaxLength="10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDatefrom" runat="server" ControlToValidate="Datefrom"
                                        Display="Dynamic" SetFocusOnError="true" ValidationGroup="Prev"
                                        ErrorMessage="Date Required." CssClass="classValidation"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-3" style="width: 30%">
                                <label class="col-sm-5 control-label">Date To</label>
                                <div class="col-sm-7">
                                    <asp:TextBox ID="Dateto" runat="server" CssClass="input-sm datepicker form-control" MaxLength="10"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="rfvDateto" runat="server" ControlToValidate="Dateto"
                                            Display="Dynamic" SetFocusOnError="true" ValidationGroup="Prev"
                                            ErrorMessage="Date Required." CssClass="classValidation"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            </div>
                            <div class="clearfix odd_row">
                            <div class="col-sm-offset-8">
                                <div class="col-sm-5 prev_fetch">
                                  <asp:LinkButton ID="lnkbtnPreview" CssClass="btn full_width_btn btn-sm btn-primary" ValidationGroup="Prev" runat="server" OnClick="lnkbtnPreview_OnClick"><i class="fa fa-search-plus"></i>Preview</asp:LinkButton>
                                  </div>
                                  <div class="col-sm-5"> 
                                  <asp:Label ID="lblTotalRecord" runat="server" Text="T. Record(s) : 0" CssClass="control-label" ></asp:Label>                                   
                                  </div>
                            </div>
                            </div>
	                        </div>
	                    </section>                        
                    </div>
                    <!-- second row -->
                     <div class="clearfix fourth_right">
                        <section class="panel panel-in-default btns_without_border">                            
                          <div class="panel-body">     
                            <div class="clearfix">
		                           <section class="panel panel-default full_form_container material_search_pop_form">
		                            <div class="panel-body" style="overflow:auto;">
                                      <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" OnRowDataBound="grdMain_RowDataBound" BorderStyle="None" CssClass="display nowrap dataTable" Width="100%" GridLines="None" ShowFooter="true" BorderWidth="0">
                                        <RowStyle CssClass="odd" />
                                        <AlternatingRowStyle CssClass="even" />                                       
                                       <PagerStyle  CssClass="classPager" />
                                         <PagerSettings Mode="NumericFirstLast" PageButtonCount="5"  FirstPageText="First" Position="Bottom" LastPageText="Last"/>   
                                            <Columns>
                                            <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-CssClass="gridHeaderAlignCenter">
                                                <HeaderStyle CssClass="gridHeaderAlignCenter" Width="50px" />
                                                <ItemStyle CssClass="gridHeaderAlignCenter" Width="50" />
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>.
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Voucher No" HeaderStyle-CssClass="gridHeaderAlignCenter"  HeaderStyle-Width="50">    
                                            <ItemStyle CssClass="gridHeaderAlignCenter" />                                         
                                                <ItemTemplate>
                                                    <%#Eval("VCHR_NO")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Voucher Date" HeaderStyle-CssClass="gridHeaderAlignLeft" HeaderStyle-Width="100">
                                                <HeaderStyle CssClass="gridHeaderAlignLeft"  Width="100" />
                                                <ItemStyle CssClass="gridHeaderAlignLeft"  Width="100" />
                                                <ItemTemplate>
                                                    <%#Convert.ToDateTime(Eval("VCHR_DATE")).ToString("dd-MM-yyyy")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Vchr Type" HeaderStyle-CssClass="gridHeaderAlignRight"  HeaderStyle-Width="100">    
                                            <ItemStyle CssClass="gridHeaderAlignRight" />                                         
                                                <ItemTemplate>
                                                    <%#Eval("VCHR_TYPE")%>                                                 
                                                </ItemTemplate>
                                                <FooterStyle CssClass="gridHeaderAlignRight" />
                                                <FooterTemplate>
                                                <b>Total :</b>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight"  HeaderStyle-Width="100" HeaderText="Debit Amount">
                                               <ItemStyle CssClass="gridHeaderAlignRight" />
                                                <ItemTemplate>
                                                     <%#Convert.ToDouble(Eval("DEBIT")).ToString("N2")%>
                                                </ItemTemplate>  
                                                <FooterStyle CssClass="gridHeaderAlignRight" />
                                                <FooterTemplate>
                                                <b><asp:Label ID="lblNetDEBIT" runat="server"></asp:Label></b>
                                                </FooterTemplate>
                                            </asp:TemplateField>    
                                             <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight"  HeaderStyle-Width="100" HeaderText="Credit Amount">
                                               <ItemStyle CssClass="gridHeaderAlignRight" />
                                                <ItemTemplate>
                                                     <%#Convert.ToDouble(Eval("CREDIT")).ToString("N2")%>
                                                </ItemTemplate>  
                                                  <FooterStyle CssClass="gridHeaderAlignRight" />
                                                <FooterTemplate>
                                                <b><asp:Label ID="lblNetCREDIT" runat="server"></asp:Label></b>
                                                </FooterTemplate>
                                            </asp:TemplateField>    
                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                        <EmptyDataTemplate>
                                            <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" ></asp:Label>
                                        </EmptyDataTemplate>                                   
                                    </asp:GridView>
                                        <br /> 
		                            </div>
		                          </section> 
		                        </div> 
                          </div>
                        </section>
                      </div>                 
                </form>
            </div>
            </section>
        </div>
    </div>
    <asp:HiddenField ID="hidmindate" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />
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

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_beginRequest(function () {
            SetFocus();
            setDatecontrol();
        });

        prm.add_endRequest(function () {
            SetFocus();
            setDatecontrol();
        });

        $(document).ready(function () {
            setDatecontrol();
        });

        function ShowModalPopup() {
            ShowDialog(true);
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
<asp:Content ID="Content4" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
    <script>
        //$(document).ready(function () {
        $(".datepicker").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
            minDate: '<%=hidmindate.Value%>',
            maxDate: '<%=hidmaxdate.Value%>'
        });
        //});
    </script>
</asp:Content>
