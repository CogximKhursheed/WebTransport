<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="ManageAmntRcvdGR.aspx.cs" EnableEventValidation="false" Inherits="WebTransport.ManageAmntRcvdGR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style2
        {
            font-family: Calibri;
            font-size: 15px;
            font-weight: normal;
            color: #333333;
            text-decoration: none;
            line-height: 22px;
            text-transform: capitalize;
            width: 183px;
            border-bottom-style: none;
            margin-left: 80px;
        }
        .style3
        {
            font-family: Calibri;
            font-size: 15px;
            font-weight: normal;
            color: #333333;
            text-decoration: none;
            line-height: 22px;
            text-transform: capitalize;
            width: 73px;
            border-bottom-style: none;
            margin-left: 80px;
        }
        .style4
        {
            font-family: Calibri;
            font-size: 15px;
            font-weight: normal;
            color: #333333;
            text-decoration: none;
            line-height: 22px;
            text-transform: capitalize;
            width: 112px;
            border-bottom-style: none;
            margin-left: 80px;
        }
        .style6
        {
            font-family: Calibri;
            font-size: 15px;
            font-weight: normal;
            color: #333333;
            text-decoration: none;
            line-height: 22px;
            text-transform: capitalize;
            width: 185px;
            border-bottom-style: none;
            margin-left: 80px;
        }
        .linearBg
        {
            /* fallback */
            background: rgb(178,225,255); /* Old browsers */ /* IE9 SVG, needs conditional override of 'filter' to 'none' */
            background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iI2IyZTFmZiIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiM2NmI2ZmMiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
            background: -moz-linear-gradient(top,  rgba(178,225,255,1) 0%, rgba(102,182,252,1) 100%); /* FF3.6+ */
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(178,225,255,1)), color-stop(100%,rgba(102,182,252,1))); /* Chrome,Safari4+ */
            background: -webkit-linear-gradient(top,  rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* Chrome10+,Safari5.1+ */
            background: -o-linear-gradient(top,  rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* Opera 11.10+ */
            background: -ms-linear-gradient(top,  rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* IE10+ */
            background: linear-gradient(to bottom,  rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* W3C */
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#b2e1ff', endColorstr='#66b6fc',GradientType=0 ); /* IE6-8 */
        }
        .style7
        {
            font-family: Calibri;
            font-size: 15px;
            font-weight: normal;
            color: #333333;
            text-decoration: none;
            line-height: 22px;
            text-transform: capitalize;
            width: 111px;
            border-bottom-style: none;
            margin-left: 80px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <style>
        .linearBg
        {
            /* fallback */
            background: rgb(178,225,255); /* Old browsers */ /* IE9 SVG, needs conditional override of 'filter' to 'none' */
            background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iI2IyZTFmZiIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiM2NmI2ZmMiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
            background: -moz-linear-gradient(top,  rgba(178,225,255,1) 0%, rgba(102,182,252,1) 100%); /* FF3.6+ */
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(178,225,255,1)), color-stop(100%,rgba(102,182,252,1))); /* Chrome,Safari4+ */
            background: -webkit-linear-gradient(top,  rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* Chrome10+,Safari5.1+ */
            background: -o-linear-gradient(top,  rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* Opera 11.10+ */
            background: -ms-linear-gradient(top,  rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* IE10+ */
            background: linear-gradient(to bottom,  rgba(178,225,255,1) 0%,rgba(102,182,252,1) 100%); /* W3C */
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#b2e1ff', endColorstr='#66b6fc',GradientType=0 ); /* IE6-8 */
        }
        .style1
        {
            font-family: Calibri;
            font-size: 15px;
            font-weight: normal;
            color: #333333;
            text-decoration: none;
            line-height: 22px;
            text-transform: capitalize;
            width: 171px;
            border-bottom-style: none;
            margin-left: 80px;
        }
        .style14
        {
            font-family: Calibri;
            font-size: 15px;
            font-weight: normal;
            color: #333333;
            text-decoration: none;
            line-height: 22px;
            text-transform: capitalize;
            width: 183px;
            border-bottom-style: none;
            margin-left: 80px;
        }
        .style18
        {
            font-family: Calibri;
            font-size: 15px;
            font-weight: normal;
            color: #333333;
            text-decoration: none;
            line-height: 22px;
            text-transform: capitalize;
            width: 768px;
            border-bottom-style: none;
            margin-left: 80px;
        }
        .style19
        {
            font-family: Calibri;
            font-size: 15px;
            font-weight: normal;
            color: #333333;
            text-decoration: none;
            line-height: 22px;
            text-transform: capitalize;
            width: 559px;
            border-bottom-style: none;
            margin-left: 80px;
        }
        .style22
        {
            font-family: Calibri;
            font-size: 15px;
            font-weight: normal;
            color: #333333;
            text-decoration: none;
            line-height: 22px;
            text-transform: capitalize;
            width: 8%;
            border-bottom-style: none;
            margin-left: 80px;
        }
    </style>

        <div id="page-content">
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-9">
            <section class="panel panel-default full_form_container part_purchase_bill_form">
                	<header class="panel-heading font-bold">AMOUNT RECEIVED GR LIST
                		<span class="view_print"><a href="AmntAgainstGr.aspx">ADD</a>&nbsp;
                        <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ToolTip="Export to excel"
                                    ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px" OnClick="imgBtnExcel_Click"
                                    Visible="false" />
                        </span>
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
                                  <asp:DropDownList ID="ddldateRange" runat="server" AutoPostBack="true" CssClass="form-control"
                                        Height="30px" TabIndex="1" Width="200px" OnSelectedIndexChanged="ddldateRange_SelectedIndexChanged">
                                   </asp:DropDownList>
                                  </div>
                                </div>
                                <div class="col-sm-4" style="width: 28.5%">
                                  <label class="col-sm-5 control-label" style="width: 39%;">Date From</label>
                                    <div class="col-sm-7" style="width: 61%;">
                                       <asp:TextBox ID="txtGrDatefrom" runat="server" CssClass="input-sm datepicker form-control" MaxLength="50" TabIndex="2"
                                                                    Width="100px"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3" style="width: 28.5%">
                                  <label class="col-sm-5 control-label">Date To</label>
                                    <div class="col-sm-7">
                                     <asp:TextBox ID="txtGrDateto" runat="server" CssClass="input-sm datepicker form-control" MaxLength="50" TabIndex="3"
                                                                    Width="100px"></asp:TextBox>
                                    </div>
                                </div>
                              </div>
                              <div class="clearfix even_row">
                               <div class="col-sm-5" style="width: 40%">
                                  <label class="col-sm-4 control-label" style="width: 29%;">From City</label>
	                                <div class="col-sm-8" style="width: 71%;">
	                                  <asp:DropDownList ID="drpCityFrom" runat="server" CssClass="form-control" Width="200px" TabIndex="4">
                                      </asp:DropDownList>
	                                </div>
                                </div>
                                <div class="col-sm-5" style="width: 26%">
                                  <label class="col-sm-4 control-label" style="width: 42%;">Receipt No.</label>
	                                <div class="col-sm-8" style="width: 57%;">
	                                  <asp:TextBox ID="txtRcptNo" runat="server" CssClass="form-control" MaxLength="50" TabIndex="5"></asp:TextBox>
	                                </div>
                                </div>
                                
                                 <div class="col-sm-4">                                  
                                  <div class="col-sm-5 prev_fetch">
                                    <asp:LinkButton ID="lnkbtnPreview" CssClass="btn full_width_btn btn-sm btn-primary"  TabIndex="6" runat="server" OnClick="lnkbtnPreview_OnClick"><i class="fa fa-search-plus"></i>Preview</asp:LinkButton> &nbsp;
                                  </div>
                                  <div class="col-sm-7"> 
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
                                   <table>
                                       <tr>
                                       <td>  
                                      <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"
                                        Width="100%" GridLines="None" AllowPaging="true" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging"
                                        BorderWidth="0" OnRowCommand="grdMain_RowCommand"  OnRowDataBound="grdMain_RowDataBound" ShowFooter="true">
                                        <RowStyle CssClass="odd" />
                                        <AlternatingRowStyle CssClass="even" />                                       
                                       <PagerStyle  CssClass="classPager" />
                                         <PagerSettings Mode="NumericFirstLast" PageButtonCount="5"  FirstPageText="First" Position="Bottom" LastPageText="Last"/>
                                          <Columns>
                                                <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>.
                                                    </ItemTemplate>
                                                                                   
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Receipt No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                    <ItemTemplate>
                                                        <%#Eval("Rcpt_No")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToDateTime(Eval("Rcpt_date")).ToString("dd-MM-yyyy")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="From City" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                    <ItemTemplate>
                                                        <%#Eval("FromCity")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Party Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                    <HeaderStyle HorizontalAlign="Left" Width="130px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="130" />
                                                    <ItemTemplate>
                                                        <%#Eval("Acnt_Name")%>
                                                    </ItemTemplate>
                                                        <FooterStyle ForeColor="Black" Font-Bold="true" HorizontalAlign="Left" />
                                                    <FooterTemplate>
                                                       <asp:Label ID="lblTotal" Text="Total :" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Net Amount" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100">
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
                                                    <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("Head_Idno") %>' CommandName="cmdedit" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("Head_Idno") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                    
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
		                                  <tr>
                                          <th rowspan="1" colspan="1" style="width: 250px;"> <asp:Label ID="lblcontant" runat="server"></asp:Label></th>
                                          <th rowspan="1" colspan="1" style="width: 163px;"></th>
                                          <th rowspan="1" colspan="1" style="width: 145px;text-align:left;">Net Total&nbsp;</th>
                                          <th rowspan="1" colspan="1" style="width: 110px;text-align:right"><asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label></th>
                                          <th rowspan="1" colspan="1" style="width: 63px;"></th></tr>                                  
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
        <div class="col-lg-2">
        
        </div>
    </div>
    </div>
    
    <%--<table border="0" cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <table width="800" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
                                class="ibdr">
                                <tr>
                                    <td>
                                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td height="39" align="left" background="images/grd_top_bg.jpg" class="title06">
                                                    &nbsp;&nbsp;&nbsp;Manage Amount Recvd. Agnst. GR
                                                </td>
                                                <td height="39" align="right" background="images/grd_top_bg.jpg" class="title06">
                                                    <a href="AmntAgainstGr.aspx">Add GR&nbsp;&nbsp;&nbsp;</a>
                                                    <asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel" ToolTip="Export to excel"
                                                        ImageUrl="~/Images/Excel_Img.JPG" Style="height: 16px" OnClick="imgBtnExcel_Click"
                                                        Visible="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table border="0" align="center" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <table width="850" border="0" cellpadding="0" cellspacing="0" class="ibdr">
                                                        <tr>
                                                            <td align="left" bgcolor="#E8F2FD" class="style4" height="20px">
                                                                <span class="txt"><span class="red" style="color: #ff0000"></span></span>Date Range<span
                                                                    class="redfont1">*</span>
                                                            </td>
                                                            <td align="left" bgcolor="#E8F2FD" height="40px" class="btn_01" nowrap="nowrap" style="width: 180px;"
                                                                valign="middle">
                                                                <asp:DropDownList ID="ddldateRange" runat="server" AutoPostBack="true" CssClass="glow"
                                                                    Height="30px" TabIndex="1" Width="200px" OnSelectedIndexChanged="ddldateRange_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="center" bgcolor="#E8F2FD" class="btn_01" nowrap="nowrap" valign="middle">
                                                                &nbsp; Date From
                                                            </td>
                                                            <td height="35px" align="left" bgcolor="#E8F2FD" class="style7" align="center" nowrap="nowrap"
                                                                valign="middle">
                                                                <asp:TextBox ID="txtGrDatefrom" runat="server" CssClass="glow" MaxLength="50" TabIndex="2"
                                                                    Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td height="35px" align="center" bgcolor="#E8F2FD" class="style3" nowrap="nowrap"
                                                                valign="middle">
                                                                &nbsp;Date To
                                                            </td>
                                                            <td height="35px" align="left" bgcolor="#E8F2FD" class="btn_01" nowrap="nowrap" style="width: 180px;"
                                                                valign="middle">
                                                                <asp:TextBox ID="txtGrDateto" runat="server" CssClass="glow" MaxLength="50" TabIndex="3"
                                                                    Width="100px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35px" align="left" bgcolor="#E8F2FD" class="style4" nowrap="nowrap" valign="middle">
                                                                From City
                                                            </td>
                                                            <td align="left" bgcolor="#E8F2FD" height="40px" class="btn_01" nowrap="nowrap" style="width: 180px;"
                                                                valign="middle">
                                                                <asp:DropDownList ID="drpCityFrom" runat="server" CssClass="glow" Width="200px" TabIndex="4">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="left" height="35px" bgcolor="#E8F2FD" class="style3" nowrap="nowrap" valign="middle">
                                                                &nbsp; Receipt No
                                                            </td>
                                                            <td height="35px" align="left" bgcolor="#E8F2FD" class="style7" nowrap="nowrap" 
                                                                valign="middle">
                                                                <asp:TextBox ID="txtRcptNo" runat="server" CssClass="glow" MaxLength="50" TabIndex="5"
                                                                    Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td height="35px" align="left" bgcolor="#E8F2FD" class="style3" nowrap="nowrap" valign="middle">
                                                                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/search_img.jpg"
                                                                    onmouseover="mouseOverImage('search')" onmouseout="mouseOutImage('search')" OnClick="btnSearch_Click"
                                                                    TabIndex="6" />
                                                            </td>
                                                            <td height="35px" align="left" bgcolor="#E8F2FD" class="style6" nowrap="nowrap" valign="middle">
                                                                <asp:Label ID="lblTotalRecord" runat="server" Text=" Total Record (s): 0" Style="font-size: 15px;
                                                                    font-weight: bold;"></asp:Label>
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
                                <tr>
                                    <td>
                                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="ibdr">
                                            <tr>
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None"
                                                                    Width="100%" GridLines="None" AllowPaging="true" PageSize="25" OnPageIndexChanging="grdMain_PageIndexChanging"
                                                                    BorderWidth="0" RowStyle-CssClass="bgcolrwhite" HeaderStyle-CssClass="linearBg"
                                                                    AlternatingRowStyle-CssClass="gridRow" OnRowCommand="grdMain_RowCommand" TabIndex="4"
                                                                    OnRowDataBound="grdMain_RowDataBound" CssClass="ibdr gridBackground internal_heading"
                                                                    ShowFooter="true">
                                                                    <HeaderStyle ForeColor ="Black" CssClass="linearBg" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                            <ItemStyle HorizontalAlign="Center" Width="50" />
                                                                            <ItemTemplate>
                                                                                <%#Container.DataItemIndex+1 %>.
                                                                            </ItemTemplate>
                                                                                   
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Receipt No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                            <ItemTemplate>
                                                                                <%#Eval("Rcpt_No")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                            <ItemTemplate>
                                                                                <%#Convert.ToDateTime(Eval("Rcpt_date")).ToString("dd-MM-yyyy")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="From City" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                            <ItemTemplate>
                                                                                <%#Eval("FromCity")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Party Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                            <HeaderStyle HorizontalAlign="Left" Width="130px" />
                                                                            <ItemStyle HorizontalAlign="Left" Width="130" />
                                                                            <ItemTemplate>
                                                                                <%#Eval("Acnt_Name")%>
                                                                            </ItemTemplate>
                                                                                <FooterStyle HorizontalAlign="Left" />
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblTotal" Text="Total :" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Net Amount" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100">
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
                                                                                <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/delete_sm.png"
                                                                                    OnClientClick="return confirm('Do you want to delete this record ?');" CommandArgument='<%#Eval("Head_Idno") %>'
                                                                                    CommandName="cmddelete" ToolTip="Delete" />
                                                                                <asp:ImageButton ID="imgBtnEdit" runat="server" ImageUrl="~/Images/edit_sm.png" CommandArgument='<%#Eval("Head_Idno") %>'
                                                                                    CommandName="cmdedit" ToolTip="Edit" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                    <EmptyDataTemplate>
                                                                        <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                                                    </EmptyDataTemplate>
                                                                    <PagerStyle CssClass="white_bg" ForeColor="#000" HorizontalAlign="Center" />
                                                                </asp:GridView>
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
                                                                        <asp:TemplateField HeaderText="GR No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                            <ItemTemplate>
                                                                                <%#Eval("Rcpt_No")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                            <ItemTemplate>
                                                                                <%#Convert.ToDateTime(Eval("Rcpt_date")).ToString("dd-MM-yyyy")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="From City" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                                                            <ItemTemplate>
                                                                                <%#Eval("FromCity")%>
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
    <asp:HiddenField ID="hidrcptheadidno" runat="server" />
    <asp:HiddenField ID="hidmindate" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />
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

            $("#<%=txtGrDatefrom.ClientID %>").datepicker({
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate,
                changeMonth: true,
                changeYear: true,
                showOn: 'button',
                buttonImage: '../images/calendar.gif'
            });
            $("#<%=txtGrDateto.ClientID %>").datepicker({
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate,
                changeMonth: true,
                changeYear: true,
                showOn: 'button',
                buttonImage: '../images/calendar.gif'
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