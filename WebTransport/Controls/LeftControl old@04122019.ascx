.<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftControl.ascx.cs"
    Inherits="WebTransport.Controls.LeftControl" %>
<div id="sidebar">
    <div id="sidebar-scroll">
        <div class="sidebar-content">
            <a href="~/DashBoard.aspx" id="hrfDshBrdssdf" runat="server" class="sidebar-brand logo">
                <img src="img/logo.png" class="cogxim-logo"><img src="img/transport-logo.png" class="transport-logo"
                    style="max-height: 50px; margin-top: -5px;">
            </a>
            <ul class="sidebar-nav">
                <li class="aside-header">
                    <div class="btn-group other-projects">
                        <button type="button" class="btn btn-sm btn-dark.dk btn-icon" title="New project">
                            <i class="fa fa-plus"></i>
                        </button>
                        <div class="btn-group hidden-nav-xs">
                            <button type="button" class="btn btn-sm dropdown-toggle testDrop" data-toggle="dropdown">
                                Other Projects <span class="caret"></span>
                            </button>
                            <ul class="dropdown-menu text-left">
                                <span class="arrow top"></span>
                                <li><a target="_blank" href="http://cogxim.com/petrogenius.htm">Petro Genius</a></li>
                                <li><a target="_blank" href="http://cogxim.com/payroll_solution.htm">Payroll Solution</a></li>
                                <li><a target="_blank" href="http://cogxim.com/automobile.htm">Automobile Management</a></li>
                                <li><a target="_blank" href="http://cogxim.com/finance.htm">Vaanijaya</a></li>
                            </ul>
                        </div>
                    </div>
                </li>
                <li><a href="~/DashBoard.aspx" id="hrfDshBrds" runat="server"><i class="icon-monitor sidebar-nav-icon">
                </i><span class="sidebar-nav-mini-hide">Dashboard</span></a> </li>
                <li><a href="#" class="sidebar-nav-menu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                </i><i class="icon-cal sidebar-nav-icon"></i><span class="sidebar-nav-mini-hide">Masters</span></a>
                    <ul>
                        <li><a href="#" class="sidebar-nav-submenu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                        </i>Item</a>
                            <ul>
                                <li><a href="~/ItemMaster.aspx" id="hrfItem" runat="server">Item Master</a> </li>
                                <li><a href="~/ItmGrpMaster.aspx" id="hrfItemGrp" runat="server">Item Group Master</a> </li>
                                <li><a href="~/UOMMaster.aspx" id="hrfUom" runat="server">UOM Master</a> </li>
                                <li><a href="~/LaneMaster.aspx" id="hrfLaneMaster" runat="server">Lane Master</a> </li>
                            </ul>
                        </li>
                        <li><a href="#" class="sidebar-nav-submenu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                        </i>Item Rate</a>
                            <ul>
                                <li><a href="~/RateMaster.aspx?FTyp=IR" id="hrfRateIR" runat="server">Rate Master</a> </li>
                                <li><a href="~/RateMaster.aspx?FTyp=TBB" id="hrfRateTBB" runat="server">Rate Master[TBB]</a> </li>
                                <li><a href="~/RateMasterWithParty.aspx" id="hrfRateMasterWithParty" runat="server">Rate Master[Party]</a> </li>
                                <li><a href="~/RateMaster.aspx?FTyp=IK" id="hrfRateIK" runat="server">Rate Master[Item Katt]</a> </li>
                            </ul>
                        </li>
                        <li><a href="#" class="sidebar-nav-submenu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                        </i>Account</a>
                            <ul>
                                <li><a href="~/LedgerMaster.aspx" id="hrfLedger" runat="server">Ledger Master</a></li>
                                <li><a href="~/FrmAcntLink.aspx" id="hrfFrmLnk" runat="server">Account Link</a> </li>
                                <li><a href="~/AcntHeadMaintenace.aspx" id="hrfAcntHd" runat="server">Account Group </a></li>
                                <li><a href="~/AcntSubGrpMaster.aspx" id="hrfAcntSubGrp" runat="server">Account Sub-Group </a></li>
                                <li><a href="~/TaxMaster.aspx" id="A1" runat="server">Tax Master </a></li>
                            </ul>
                        </li>
                        <li><a href="#" class="sidebar-nav-submenu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                        </i>Employee</a>
                            <ul>
                                <li><a href="~/EmployeeMaster.aspx" id="hrfemp" runat="server">Employee Master</a> </li>
                                <li><a href="~/EditProfile.aspx" id="hrfEditP" runat="server">Edit Profile</a> </li>
                                <li><a href="~/DesignationMaster.aspx" id="hrfDesigN" runat="server">Designation Master</a> </li>
                                <li><a href="~/DesigRights.aspx" id="hrfDesi" runat="server">Designation Right</a> </li>
                            </ul>
                        </li>
                        <li><a href="#" class="sidebar-nav-submenu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                        </i>Others</a>
                            <ul>
                                <li><a href="~/CityMaster.aspx" id="hrfCityM" runat="server">Location/City Master</a> </li>
                                <li><a href="~/DistrictMaster.aspx" id="hrfDistrictMaster" runat="server">District Master</a> </li>
                                <li><a href="~/LorryMaster.aspx" id="hrfLryM" runat="server">Lorry Master</a> </li>
                                <li><a href="~/DriverMaster.aspx" id="hrfDrivr" runat="server">Driver Master</a> </li>
                                <li><a href="~/CommissionMaster.aspx" id="hrfCmsn" runat="server">Commission Master</a> </li>
                                <li><a href="~/TitleMaster.aspx" id="hrfTTL" runat="server">Title Master</a> </li>
                                <li><a href="~/NarrationMast.aspx" id="hrfNrntn" runat="server">Narration Master</a> </li>
                                <li><a href="~/DistanceMast.aspx" id="hrfDist" runat="server">Distance Master</a> </li>
                                <li><a href="~/TolTaxMaster.aspx" id="hrfTollms" runat="server">Toll Tax Master</a> </li>
                                <li><a href="~/PetrolCompanyMaster.aspx" id="hrfCmpnym" runat="server">Petrol Company Master</a> </li>
                                <li><a href="~/ContainerSizeMaster.aspx" id="ConSize" runat="server">Container Size</a> </li>
                                <li><a href="~/ContainerTypeMaster.aspx" id="ConType" runat="server">Container Type</a> </li>
                                <li><a href="~/CategoryMaster.aspx" id="catmast" runat="server">Category Master</a> </li>
                                <li><a href="~/LowRateMaster.aspx" id="hrfLowRateMaster" runat="server">Low Rate Master</a> </li>
                                <li><a href="~/GradeMaster.aspx" id="hrfGradeMaster" runat="server">Item Grade Master</a> </li>
                            </ul>
                        </li>
                        <li id="liRetailMast" runat="server"><a href="#" class="sidebar-nav-submenu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                        </i>Retailer</a>
                        <ul>
                            <li><a href="~/MiscMaster.aspx" id="hrfMiscMaster" runat="server">Miscllaneous Master</a> </li>
                            <li><a href="#" class="sidebar-nav-submenu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                            </i>Item Rate</a>
                                <ul>
                                    <li><a href="~/RateMasterRetail.aspx?FTyp=IR" id="hrfRateMasterRetailIR" runat="server">Rate Master</a> </li>
                                    <li><a href="~/RateMasterRetail.aspx?FTyp=TBB" id="hrfRateMasterRetailTBB" runat="server">Rate Master[TBB]</a> </li>
                                    <li><a href="~/RateMasterRetailerParty.aspx" id="hrfRateMasterRetailerParty" runat="server">Rate Master[Party]</a> </li>
                                </ul>
                            </li>
                        </ul>
                        </li>
                    </ul>
                </li>
                <li><a href="#" class="sidebar-nav-menu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                </i><i class="icon-globe sidebar-nav-icon"></i><span class="sidebar-nav-mini-hide">Booking
                    Entry</span></a>
                    <ul>
                        <li><a href="~/AdvBookGR.aspx" id="hrfAdvBokng" runat="server">Advance Booking[For GR]</a></li>
                        <li><a href="~/RcptGoodsReceived.aspx" id="hrfGdsrpt" runat="server">Goods Receipt</a></li>
                        <li><a href="~/Quotation.aspx" id="hrfQuatation" runat="server">Quotation</a> </li>
                        <li><a href="~/GRPrep.aspx" id="hrfGrpr" runat="server">GR Prep./Challan</a> </li>
                        <li id="liRetailBookEntry" runat="server"><a href="#" class="sidebar-nav-submenu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                        </i>Retailer</a>
                        <ul>
                            <li><a href="~/GRPrepRetailer.aspx" id="hrfGRPrepRetailer" runat="server">GR Prep./Challan</a> </li>
                        </ul>
                        </li>
                        <li><a href="~/UpdateGrDetail.aspx" id="hrfUpdateGrDetail" visible="false" runat="server">Update Gr Detail</a></li>
                        <%--<li><a href="~/RateMasterFT.aspx" id="A7" runat="server">Rate Master</a></li>--%>
                        <li><a href="~/LorryHireSlip.aspx" id="hrfLorryHireSlip" runat="server">Lorry HireSlip</a></li>

                        <li><a href="#" class="sidebar-nav-submenu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                        </i>Dispatch Challan</a>
                            <ul>
                                <li><a href="~/ChlnBooking.aspx" id="hrfChlnBkng" runat="server">Dispatch Challan</a> </li>
                                <li><a href="~/Challanconfirmation.aspx" id="hrfChlnConfrm" runat="server">Challan Confirmation</a> </li>
                                <li><a href="~/ChlnBookingCrsng.aspx" id="hrfChlnBokngCrsng" runat="server">Challan Crossing</a> </li>
                            </ul>
                        </li>
                        <li><a href="#" class="sidebar-nav-submenu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                        </i>Invoice Generation</a>
                            <ul>
                                <li><a href="~/Invoice.aspx" id="hrfInvoc" runat="server">Invoice</a> </li>
                            </ul>
                        </li>
                        <li><a href="~/ManageInvoiceDetails.aspx" id="hrfCoverNote" runat="server">Cover Note</a></li>
                        <li><a href="#" class="sidebar-nav-submenu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                        </i>Amount Received</a>
                            <ul>
                                <li id="LiAmntAgainstGr" runat="server"><a href="~/AmntAgainstGr.aspx" id="hrfAgnstGr" runat="server">Against GR</a></li>
                                <li id="LiAmntAgainstInvoice" runat="server"><a href="~/AmntAgainstInvoice.aspx" id="hrfAgnstInvo" runat="server">Against Invoice</a></li>
                            </ul>
                        </li>
                        <li><a href="#" class="sidebar-nav-submenu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                        </i>Payment [Against Challan]</a>
                            <ul>
                                <li><a href="~/ChlnAmntPayment.aspx" id="hrfAgnstCHln" runat="server">To Party</a> </li>
                                <li><a href="~/PaymentToOwn.aspx" id="A2" runat="server">To Driver</a> </li>
                            </ul>
                        </li>
                        <li><a href="#" class="sidebar-nav-submenu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                        </i>Own To Hire [Lorry]</a>
                        <ul>
                            <li><a href="#" class="sidebar-nav-submenu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                            </i>Entry</a>
                                <ul>
                                    <li><a href="~/GrPrepOntohire.aspx" id="hrfGrPrepOntohire" runat="server">GR Details</a> </li>
                                    <li><a href="~/HireInvoice.aspx" id="hrfHireInvoice" runat="server">Invoice Generation</a> </li>
                                    <li><a href="~/AmntAgainstInvoiceOTH.aspx" id="hrfAmntAgainstInvoiceOTH" runat="server">Amount Received</a> </li>
                                    <li><a href="~/TripSheetOwnToHire.aspx" id="hrfTripSheetOwnToHire" runat="server">Trip Sheet</a> </li>
                                </ul>
                            </li>
                            <li><a href="#" class="sidebar-nav-submenu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                            </i>Reports</a>
                                <ul>
                                    <li><a href="~/InvoiceReportOTH.aspx" id="hrfInvoiceReportOTH" runat="server">Invoice Report</a> </li>
                                </ul>
                            </li>
                         </ul>
                        </li>
                    </ul>
                </li>
                <li><a href="#" class="sidebar-nav-menu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                </i><i class="icon-apps sidebar-nav-icon"></i><span class="sidebar-nav-mini-hide">Delivery</span></a>
                    <ul>
                        <li><a href="~/ChallanDelverd.aspx" id="hrfChlnDlRp" runat="server">Delivery Register</a></li>
                        <li><a href="~/SummaryRegister.aspx" id="hrfSumReg" runat="server">Summary Register</a></li>
                        <li><a href="~/FreightMemo.aspx" id="hrfFrightMmo" runat="server">Freight Memo </a> </li>
                        <li><a href="~/DeliveryChallanDetails.aspx" id="hrfDeliChDtl" runat="server">Delivery Challan</a> </li>
                    </ul>
                </li>
                <li><a href="#" class="sidebar-nav-menu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                </i><i class="icon-chart sidebar-nav-icon"></i><span class="sidebar-nav-mini-hide">Accounts</span></a>
                    <ul>
                        <li><a href="~/VchrEntry.aspx" id="hrfVchrentry" runat="server">Voucher Entry</a>
                        </li>
                        <li><a href="~/BankReConciliation.aspx" id="hrfBnkRec" runat="server">Bank ReConciliation</a>
                        </li>
                        <li><a href="#" class="sidebar-nav-submenu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                        </i>Reports</a>
                            <ul>
                                <li><a href="~/DayBookReport.aspx" id="hrDayBook" runat="server">Day Book</a> </li>
                                <li><a href="~/AccountBookRpt.aspx" id="hrfacbkrpt" runat="server">Account Book</a> </li>
                                <li><a href="~/AccTrailBal.aspx" id="hrfAcTrilBal" runat="server">Trial Balance</a> </li>
                                <li><a href="~/PartyOutstandingReport.aspx" id="hrfPartyOutstandingReport" runat="server">Party Oustanding</a> </li>
                                <li><a href="~/TdsReport.aspx" id="hrfTdsRpt" runat="server">TDS Report</a> </li> 
                                <li><a href="~/ProfNLossAc.aspx" id="hrfPfroftLos" runat="server">Profit & Loss</a> </li>
                                <li><a href="~/BalanceSheetAc.aspx" id="hrfBalSht" runat="server">Balance Sheet</a> </li>
                                <li><a href="~/AcntPostChkLst.aspx" id="A5" runat="server">A/c Post Check List</a> </li>
                                <li><a href="~/GSTR1.aspx" id="hrfGSTR1" runat="server">GSTR-1</a> </li>
                            </ul>
                        </li>
                        <li><a href="#" class="sidebar-nav-submenu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                        </i>Cost Center Reports</a>
                            <ul>
                                <li><a href="~/CostCategorySumryRep.aspx" id="hrfCostSmmry" runat="server">Cost Category
                                    Summary</a> </li>
                                <li><a href="~/CostLedgerRep.aspx" id="hrfCostldgr" runat="server">Cost Breakup Ledger
                                    Wise</a> </li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li><a href="#" class="sidebar-nav-menu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                </i><i class="icon-map sidebar-nav-icon"></i><span class="sidebar-nav-mini-hide">Reports</span></a>
                    <ul>
                        <li><a href="~/AdvBookGrReport.aspx" id="hrfAdvBookRep" runat="server">Adv. Booking Reports</a></li>
                        <li><a href="~/GrReport.aspx" id="GrRep" runat="server">GR Reports</a></li>
                        <li><a href="~/GRConsldRepRetlr.aspx" id="hrfGRConsldRepRetlr" runat="server">GR Report [Retailer]</a></li>
                        <li><a href="~/ConsolidetedGrReport.aspx" id="A3" runat="server">Consolidated GR Report</a></li>
                        <li><a href="~/PendingGRforInvoiceRep.aspx" id="PenGrRep" runat="server">Pending GR Report</a> </li>
                        <li><a href="~/ChlnRpt.aspx" id="ChlnRep" runat="server">Challan Reports</a> </li>
                        <li><a href="~/ChallanConfirmationRep.aspx" id="ChlnConfirmRep" runat="server">Challan Confirmation Report</a> </li>
                        <li><a href="~/ShortageReport.aspx" id="ShrtgRep" runat="server">Shortage Report</a> </li>
                        <li><a href="~/InvoiceReport.aspx" id="InvRep" runat="server">Invoice Report</a> </li>
                        <li><a href="~/DailyReport.aspx" id="hrfDailyReport" runat="server">Daily Report</a> </li>
                        <li><a href="~/PartyDetailReport.aspx" id="hrfPartyDetailReport" runat="server">Party Detail Report [I]</a> </li>
                        <li><a href="~/PartyDetailReportArv.aspx" id="hrfPartyDetlRep" runat="server">Party Detail Report [II]</a> </li>
                        <%--<li><a href="~/PartyDetlRep.aspx" id="hrfPartyDetlRep" runat="server">Party Detail Report [II]</a> </li>--%>
                        <li><a href="~/PartyClosingBalanceReport.aspx" id="hrfPartyClosingBalanceReport" runat="server">Party Closing Balances[PDR]</a> </li>
                        <li><a href="~/OutstndngBillReport.aspx" id="hrfOutStndngRpt" runat="server">Outstanding Report</a> </li>
                        <li><a href="~/LorryMasterReport.aspx" id="hrfLoryRpt" runat="server">Lorry Master Report</a> </li>
                        <li><a href="~/LorryPenDtRep.aspx" id="hrfDueDateRpt" runat="server">Due Date Report[Lorry]</a></li>
                        <li><a href="~/DispatchRegister.aspx" id="hrfDispRegRep" runat="server">Dispatch Register</a></li>
                        <li><a href="~/DueAlignmentsReport.aspx" id="hrfDueAlignRep" runat="server">Due Alignments</a></li>
                        <li><a href="~/RateMasterWithPartyReport.aspx" id="hrfRateMasterWithPartyRep" runat="server">Rate Master Report[Party]</a> </li>
                        <li><a href="~/DocumentReport.aspx" id="hrfDocumentReport" runat="server">Document Report</a></li>
                        <li><a href="~/LorrySummaryReport.aspx" id="hrfLorrySummaryReport" runat="server">Lorry Summary Report</a></li>
                        
                    </ul>
                </li>
                <li><a href="#" class="sidebar-nav-menu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                </i><i class="icon-sheet sidebar-nav-icon"></i><span class="sidebar-nav-mini-hide">Delivery
                    Reports</span></a>
                    <ul>
                        <li><a href="~/DeliveryRegisterReport.aspx" id="hrfDelivReg" runat="server">Delivery
                            Register Report</a> </li>
                        <li><a href="~/SummaryRegReport.aspx" id="hrfSumRep" runat="server">Summary Register
                            Report</a> </li>
                        <li><a href="~/FreightMemoReport.aspx" id="hrfFright" runat="server">Freight Memo Report</a>
                        </li>
                        <li><a href="~/DeliveryChallanRpt.aspx" id="hrfDeliRpt" runat="server">Delivery Challan
                            Report</a> </li>
                    </ul>
                </li>
                <li id="liFleetMgmt" runat="server"><a href="#" class="sidebar-nav-menu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                </i><i class="icon-cross sidebar-nav-icon"></i><span class="sidebar-nav-mini-hide">Fleet Mgmt.</span></a>
                    <ul>
                        <li><a href="#" class="sidebar-nav-submenu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                        </i>Master</a>
                            <ul>
                                <li><a href="~/ItemMast.aspx" id="hrfFleetitemMst" runat="server">Item Master</a> </li>
                                <li><a href="~/FleetAcntLink.aspx" id="hrfAcntLink" runat="server">Account Link</a> </li>
                                <li><a href="~/EmployeeSalry.aspx" id="hrfEmployeeSalry" runat="server">Employee Salary</a> </li>
                                <li><a href="~/ItmGrpMaster.aspx" id="hrfFleetItmGrpMast" runat="server">Item Group Master</a> </li>
                                <li><a href="~/UOMMaster.aspx" id="hrfFleetUOMMast" runat="server">UOM Master</a> </li>
                                <li><a href="~/TyrePositionMast.aspx" id="hrfFleetTyrPosMast" runat="server">Tyre Position Master</a> </li>
                                <li><a href="~/TyreMaster.aspx" id="hrfTyreMstr" runat="server">Tyre Type Master</a> </li>
                                <li><a href="~/ContainerSizeMaster.aspx" id="hrfContnrSizmast" runat="server">Container Size Master</a> </li>
                                <li><a href="~/ContainerTypeMaster.aspx" id="hrfContnrTypmast" runat="server">Container Type Master</a> </li>
                                <li><a href="~/FuelRateMaster.aspx" id="A6" runat="server">Rate Master [Fuel]</a> </li>
                            </ul>
                        </li>
                        <li><a href="#" class="sidebar-nav-submenu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                        </i>Entry</a>
                            <ul>
                                <li><a href="~/OpenStockAccessories.aspx" id="hrfOS" runat="server">Opening Stock-Accessories</a></li>
                                <li><a href="~/OpenStock.aspx" id="hrfOSTyr" runat="server">Opening Stock-Tyre</a></li>
                                <li><a href="~/PurchaseBill.aspx" id="hrfPurchBl" runat="server">Purchase bill</a></li>
                                <li><a href="~/SaleBill.aspx" id="hrfSaleBill" runat="server">Sale bill</a></li>
                                <li><a href="~/MaterialIssue.aspx" id="hrfMatIsue" runat="server">Material Issue</a></li>
                                <li><a href="~/TripEntry.aspx" id="hrfTripSht" runat="server">Trip Sheet</a></li>
                                <li><a href="~/ManualTripSheet.aspx" id="hrfManualTripSheet" runat="server">Manual Trip Sheet</a></li>
                                <li><a href="~/WebForm3.aspx" id="hrfManualTripSheet2" runat="server">Manual Trip Sheet[II]</a></li>
                                <li><a href="~/FuelSlip.aspx" id="hrfuelSlip" runat="server">Fuel Slip</a></li>
                                <li><a href="~/StockTransfer.aspx" id="hrStckTran" runat="server">Stock Transfer</a></li>
                                <li><a href="~/Tracking.aspx" id="hrTracking" runat="server">Tracking</a></li>
                            </ul>
                        </li>
                        <li><a href="#" class="sidebar-nav-submenu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                        </i>Claim</a>
                            <ul>
                                <li><a href="~/ClaimFrm.aspx" id="hrfClaimRecFrmCust" runat="server">Claim Receive[From Customer]</a></li>
                                <li><a href="~/ClaimToComp.aspx" id="hrfClaimToComp" runat="server">Claim Send/Reveive</a></li>
                                <li><a href="~/MaterialIssueAgnClaim.aspx" id="hrfMaterialIssueAgnClaim" runat="server">Material Issue Against Claim</a></li>
                            </ul>
                        </li>
                        <li><a href="#" class="sidebar-nav-submenu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                        </i>Reports</a>
                            <ul>
                                <li><a href="~/TripReport.aspx" id="hrfTripShtRep" runat="server">TripSheet Report</a> </li>
                                <li><a href="~/TripExpnsReport.aspx" id="hrfTripExpnsReport" runat="server">Trip Expense Report</a> </li>
                                <li><a href="~/DriverlogRpt.aspx" id="hrfDriverlogRpt" runat="server">Driver-Log Report</a> </li>
                                <li><a href="~/ConsolidatedTripRpt.aspx" id="hrfMnthWsTripRep" runat="server">Month Wise Trip Report</a> </li>
                                <li><a href="~/PurchaseReg.aspx" id="hrfPurReg" runat="server">Purchase Register</a> </li>
                                <li><a href="~/SaleReg.aspx" id="hrfSaleReg" runat="server">Sale Register</a> </li>
                                <li><a href="~/MatIssueReport.aspx" id="hrfMtrlTrnfrRpt" runat="server">Material Issue Report</a> </li>
                                <li><a href="~/CurrentStockReport.aspx" id="hrfCurStckRepTyr" runat="server">Current Stock[Tyre]</a> </li>
                                <li><a href="~/TyreMovementReport.aspx" id="hrfTyrMvmntRep" runat="server">Tyre Movement Report</a> </li>
                                <li><a href="~/TyreStockReport.aspx" id="hrfTyreStckRep" runat="server">Tyre Stock Report</a> </li>
                                <li><a href="~/AccessoryStockReport.aspx" id="hrfAccesStckRep" runat="server">Accessary Stock Report</a> </li>
                                <li><a href="~/StockTransferReport.aspx" id="hrfStckTransfRep" runat="server">Stock Transfer Report</a> </li>
                                <li><a href="~/RunningStockRpt.aspx" id="hrfRunningStock" runat="server">Running Stock [Tyre]</a> </li>
                                <li><a href="~/ClaimReports.aspx" id="hrfClaimRep" runat="server">Claim Received Report</a> </li>
                                <li><a href="~/RptCustomTripSheet.aspx" id="A7" runat="server">Custome Trip Sheet Report</a> </li>
                            </ul>
                        </li>
                    </ul>
                </li>
               <%--<li id="liComsnAgnt" runat="server"><a href="#" class="sidebar-nav-menu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                </i><i class="icon-cross sidebar-nav-icon"></i><span class="sidebar-nav-mini-hide">Business Commision</span></a>
                    <ul>
                        <li><a href="#" class="sidebar-nav-submenu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                        </i>Master</a>
                            <ul>
                                <li><a href="~/RateMasterFT.aspx" id="hrfRateMastFT" runat="server">Rate Master</a></li>
                                <li><a href="~/LorryHireSlip.aspx" id="hrf" runat="server">Lorry HireSlip</a></li>
                            </ul>
                        </li>
                    </ul>
                </li>--%>

                <li><a href="#" class="sidebar-nav-menu"><i class="fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide">
                </i><i class="icon-paper-plane sidebar-nav-icon"></i><span class="sidebar-nav-mini-hide">
                    Utility</span></a>
                    <ul>
						<li><a href="~/ChallanBulkUpdate.aspx" id="hrfblkupdate" runat="server">Bulk Update Challan</a> </li>
                        <li><a href="~/ChangePassword.aspx" id="hrfRstPswd" runat="server">Reset Password</a> </li>
                        <li><a href="~/UserPreference.aspx" id="hrfUsrPref" runat="server">User Preference</a> </li>
                        <li><a href="~/UserRights.aspx" id="A4" runat="server">User Rights</a> </li> 
                        <li><a href="~/UserDefault.aspx" id="hrfUsrDft" runat="server">User Default</a> </li>
                        <li><a href="~/CompanyMast.aspx" id="hrfCompMast" runat="server">Company Master</a> </li>
                        <li><a href="~/MisngDocs.aspx" id="hrfMisngDocs" runat="server">Missing Docs</a> </li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
</div>
