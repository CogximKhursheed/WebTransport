using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using System.Configuration;
using System.IO;
using System.Web;

namespace WebTransport
{
    public partial class LedgerMaster : Pagebase
    {
        #region Variables declaration...
        private int intFormId = 12;
        string siteUrl = ConfigurationManager.AppSettings["siteurl"];
        static FinYear UFinYear = new FinYear();
        DateTime ExpiryDate = new DateTime();
        DateTime HazardousExpiryDate = new DateTime();
        DataTable dt = new DataTable();
        DataTable dtTemp = new DataTable();
        #endregion

        #region Page Load..
        protected void Page_Load(object sender, EventArgs e)
        {
            string strPostBackControlName = Request.Params.Get("__EVENTTARGET");
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
                if (base.CheckUserRights(intFormId) == false)
                {
                    Response.Redirect("PermissionDenied.aspx");
                }
                if (base.ADD == false)
                {
                    lnkbtnSave.Visible = false;
                }
                UFinYear = base.FatchFinYear(Convert.ToInt32(base.CompId));
                txtPinCode.Attributes.Add("onkeypress", "return allowOnlyNumber(event)");
                txtFax.Attributes.Add("onkeypress", "return allowOnlyNumber(event)");
                txtAccount.Attributes.Add("onkeypress", "return allowOnlyNumber(event)");
                txtContMob.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtDriverMobileNo1.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtDriverMobileNo2.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtOpBal.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtagntCommision.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtShortName.Attributes.Add("onkeypress", "return notAllowSpecialCharacters_Spaceallow(event);");
                Bindemp();
                this.BindDateRange();
                this.BindTitle();
                this.BindState();
                this.BindAcntType();
                ddlAccountType.SelectedValue = "2";
                this.BindAcntSubGroup();
                this.BindPCompName();
                this.BindCategory();
                this.BindPrincComp();
        
                ddlTitle.Focus();
                this.BindCitywithStateId(Convert.ToInt32(ddlState.SelectedValue));
                this.BindUserCityState();
                this.BindDistrictwithStateId(Convert.ToInt32(ddlState.SelectedValue));
                dtTemp = CreateDt();
                ViewState["DocumentHolderTable"] = dtTemp;
                this.BindDocumentDataGrid();
                if (Request.QueryString["AcntIdno"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["AcntIdno"]));
                    lnkbtnNew.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "EnabaleDisable()", true);
                }
                else
                {
                    lnkbtnNew.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "OnChangeDriver()", true);
                }
            }
            if (strPostBackControlName == "lnkParty")
            {
                FillGridValue();
            }
        }
        #endregion

        #region Functions...

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }

        private void Bindemp()
        {
            DriverMastDAL objclsCityMaster = new DriverMastDAL();
            var objCityMast = objclsCityMaster.Selectemp();
            objclsCityMaster = null;
            Drpgurenter.DataSource = objCityMast;
            Drpgurenter.DataTextField = "Emp_Name";
            Drpgurenter.DataValueField = "User_Idno";
            Drpgurenter.DataBind();
            Drpgurenter.Items.Insert(0, new ListItem(" ----Select---- ", "0"));
        }
        private void BindPCompName()
        {
            PetrolPumpMasterDAL objclsPetrolPumpMaster = new PetrolPumpMasterDAL();
            var objPetrolPumpMaster = objclsPetrolPumpMaster.SelectPCompName();
            objclsPetrolPumpMaster = null;
            ddlCompany.DataSource = objPetrolPumpMaster;
            ddlCompany.DataTextField = "PComp_Name";
            ddlCompany.DataValueField = "PComp_Idno";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem(" ----Select---- ", "0"));
            
        }
        private void BindTitle()
        {

            TitleMasterDAL objTitlBLL = new TitleMasterDAL();
            var objList = objTitlBLL.SelectAll();
            ddlTitle.DataSource = objList;
            ddlTitle.DataTextField = "Titl_Name";
            ddlTitle.DataValueField = "Titl_Idno";
            ddlTitle.DataBind();
            objTitlBLL = null;
            ddlTitle.Items.Insert(0, new ListItem("< Title >", "0"));
        }
        private void BindPrincComp()
        {
            BindDropdownDAL obj= new BindDropdownDAL();
            var objList = obj.BindPrincComp();
            ddlPrincComp.DataSource = objList;
            ddlPrincComp.DataTextField = "Pcomp_Name";
            ddlPrincComp.DataValueField = "PComp_Idno";
            ddlPrincComp.DataBind();
            obj = null;
        }

        
        private void BindAcntType()
        {
            LedgerAccountDAL accountDAL = new LedgerAccountDAL();
            var lst = accountDAL.FetchAcntType();
            accountDAL = null;
            ddlAccountType.DataSource = lst;
            ddlAccountType.DataTextField = "AcntType_Name";
            ddlAccountType.DataValueField = "AcntType_Idno";
            ddlAccountType.DataBind();
            ddlAccountType.Items.Insert(0, new ListItem("< Choose Type >", "0"));
        }

        private void BindAcntSubGroup()
        {
            LedgerAccountDAL accountDAL = new LedgerAccountDAL();
            var lst = accountDAL.FetchAcntSubHead();
            accountDAL = null;
            ddlAccountSubGroup.DataSource = lst;
            ddlAccountSubGroup.DataTextField = "ASubHead_Name";
            ddlAccountSubGroup.DataValueField = "ASubHead_Idno";
            ddlAccountSubGroup.DataBind();
            ddlAccountSubGroup.Items.Insert(0, new ListItem("< Choose Type >", "0"));
        }

        private void BindCategory()
        {
            LedgerAccountDAL accountDAL = new LedgerAccountDAL();
            var lst = accountDAL.FetchCategory();
            accountDAL = null;
            ddlCategory.DataSource = lst;
            ddlCategory.DataTextField = "Category_Name";
            ddlCategory.DataValueField = "Category_Idno";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        private void BindUserCityState()
        {
            if (ddlAccountType.SelectedValue != "1" && ddlAccountType.SelectedValue != "3")
            {
                Int64 userId = Convert.ToInt64(Session["UserIdno"].ToString());
                LedgerAccountDAL accountDAL = new LedgerAccountDAL();
                var lst = accountDAL.SelectStateCity(userId);
                accountDAL = null;
                if (lst.Count > 0)
                {
                    if (Convert.ToInt32(DataBinder.Eval(lst[0], "StateId")) > 0)
                    {
                        ddlState.SelectedValue = Convert.ToString(DataBinder.Eval(lst[0], "StateId"));
                        this.BindCitywithStateId(Convert.ToInt32(DataBinder.Eval(lst[0], "StateId")));
                        ddlCity.SelectedValue = Convert.ToString(DataBinder.Eval(lst[0], "CityId"));
                    }
                }
            }
        }
        

        private void BindState()
        {
            ddlState.DataSource = null;
            LedgerAccountDAL accountDAL = new LedgerAccountDAL();
            var lst = accountDAL.SelectState(0);
            accountDAL = null;
            ddlState.DataSource = lst;
            ddlState.DataTextField = "State_Name";
            ddlState.DataValueField = "State_Idno";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("--Select State--", "0")); 
        }
       
        private void BindCitywithStateId(int stateIdno)
        {
            if (ddlAccountType.SelectedValue != "1" && ddlAccountType.SelectedValue != "3")
            {
                ddlCity.DataSource = null;
                ddlCity.DataBind();
                LedgerAccountDAL accountDAL = new LedgerAccountDAL();
                var lst = accountDAL.BindCitywithStateId(stateIdno);
                accountDAL = null;
                ddlCity.DataSource = lst;
                ddlCity.DataTextField = "City_Name";
                ddlCity.DataValueField = "City_Idno";
                ddlCity.DataBind();
            }
            ddlCity.Items.Insert(0, new ListItem("--Select City--", "0"));
        }
        private void BindDistrictwithStateId(int stateIdno)
        {
            if (ddlAccountType.SelectedValue != "1" && ddlAccountType.SelectedValue != "3")
            {
                ddlDistrict.DataSource = null;
                LedgerAccountDAL accountDAL = new LedgerAccountDAL();
                var lst = accountDAL.BindDistrictwithStateId(stateIdno);
                accountDAL = null;
                ddlDistrict.DataSource = lst;
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Idno";
                ddlDistrict.DataBind();
            }
            ddlDistrict.Items.Insert(0, new ListItem("--Select District--", "0"));
        }
        private void ClearControls()
        {

            // Response.Redirect("LedgerMaster.aspx");

            ddlTitle.SelectedValue = "0"; txtAccountPrtyNameHindi.Text = string.Empty; txtAddress2.Text = string.Empty; txtAccountPrtyName.Text = txtShortName.Text = string.Empty;
            ddlAccountType.SelectedValue = "2";

            ddlAccountSubGroup.SelectedValue = "0"; txtOpBal.Text = "0.00"; ddlBalanceType.SelectedValue = "1";
            txtAddress1.Text = string.Empty; ddlState.SelectedValue = "0"; ddlCity.SelectedValue = "0"; txtPinCode.Text = string.Empty; txtFax.Text = string.Empty;
            //chkStatus.Checked = Convert.ToBoolean(true);
            txtContEmail.Text = string.Empty; txtContMob.Text = string.Empty; txtcontPrsn.Text = string.Empty; txtagntCommision.Text = "0.00";
            //chkServExmpt.Checked = Convert.ToBoolean(false);
            //lnkbtnNew.Visible = false; txtAccount.Text = string.Empty;
            //ddlCompany.SelectedValue = "0";
          //  ddlPrincComp.SelectedValue = "0";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "OnChangeDriver();onChangeAccSubGrp();", true);
            
        }
        private void Populate(int AcntIdno)
        {
            LedgerAccountDAL objAcntMaster = new LedgerAccountDAL();
            var objAcntMast = objAcntMaster.SelectById(AcntIdno);
            var lst = objAcntMaster.CheckInvGen(AcntIdno);
            var objDocHolder = objAcntMaster.SelectDocHolder(AcntIdno);
            objAcntMaster = null;
            if (objAcntMast != null)
            {
                hiddriverPopulate.Value = Convert.ToString(lst);
                txtAccountPrtyName.Text = Convert.ToString(objAcntMast.Acnt_Name);
                txtAccountPrtyNameHindi.Text = Convert.ToString(objAcntMast.AcntName_Hindi);
                txtAddress1.Text = Convert.ToString(objAcntMast.Address1);
                txtAddress2.Text = Convert.ToString(objAcntMast.Address2);
                txtAccount.Text = Convert.ToString(objAcntMast.Account_No);
                txtBankName.Text=Convert.ToString(objAcntMast.Bank_Name);
                txtBranchName.Text=Convert.ToString(objAcntMast.Branch_Name);
                txtIfscNo.Text = Convert.ToString(objAcntMast.Ifsc_Code);
                txtagntCommision.Text = Convert.ToString(objAcntMast.Agnt_Commson);
                ddlAccountType.SelectedValue = Convert.ToString(objAcntMast.Acnt_Type);
                txtContEmail.Text = Convert.ToString(objAcntMast.Cont_Email);
                if ((Convert.ToInt32(ddlAccountType.SelectedValue) == 3) || (Convert.ToInt32(ddlAccountType.SelectedValue) == 1))
                {
                    txtcontPrsn.Enabled = false; txtContMob.Enabled = false; txtContEmail.Enabled = false; txtAddress1.Enabled = false;
                    txtAddress2.Enabled = false; ddlState.Enabled = false; ddlCity.Enabled = false; chkStatus.Enabled = false;
                    txtPinCode.Enabled = false; txtTin.Enabled = false; txtFax.Enabled = false; ddlCategory.Enabled = false;
                    rfvState.Enabled = false; rfvCity.Enabled = false; rfvpetroCompany.Enabled = false; rfvpetroCompany.Visible = false;
                    rfvCity.Visible = false; rfvState.Visible = false; SpanCityRefresh.Visible = false;
                    ddlState.SelectedIndex = 0; ddlCity.SelectedIndex = 0; ddlDistrict.SelectedIndex = 0;
                }
                else
                {
                    ddlState.SelectedValue = Convert.ToString(objAcntMast.State_Idno);
                    this.BindCitywithStateId(Convert.ToInt32(objAcntMast.State_Idno));
                    if ((Convert.ToString(objAcntMast.City_Idno) != "") && (Convert.ToInt64(objAcntMast.City_Idno) > 0))
                    {
                        ddlCity.SelectedValue = Convert.ToString(objAcntMast.City_Idno);
                    }
                }
                ddlTitle.SelectedValue = Convert.ToString(objAcntMast.Titl_Idno);
                
                if (Convert.ToInt32(ddlAccountType.SelectedValue) != 12)
                {
                    DivlnkClaimDetails.Visible = false;
                }
                else
                {
                    DivbtnDriver.Visible = false; DivlnkClaimDetails.Visible = true;
                }
                if (Convert.ToString(objAcntMast.Acnt_Type) == "9")
                {
                    txtlicense.Text = Convert.ToString(objAcntMast.DrvLicnc_NO);

                    if (objAcntMast.DrvLNo_ExpDate != null)
                        txtExpiryDate.Text = Convert.ToDateTime(objAcntMast.DrvLNo_ExpDate).ToString("dd-MM-yyyy");

                    txtauthority.Text = Convert.ToString(objAcntMast.DrvAuthrty_Plc);
                    txtaccountno.Text = Convert.ToString(objAcntMast.DrvAcnt_No);
                    Drpgurenter.SelectedValue = Convert.ToString(objAcntMast.DrvGurntr_Idno);
                    chkVarified.Checked = Convert.ToBoolean(objAcntMast.DrvLNo_Verfd);
                    txtDriverSOF.Text = Convert.ToString(objAcntMast.DrvFather_Nm);
                    txtDriverAddress.Text = Convert.ToString(objAcntMast.Drv_Adres);
                    txtDriverMobileNo1.Text = Convert.ToString(objAcntMast.Drv_MobNo1);
                    txtDriverMobileNo2.Text = Convert.ToString(objAcntMast.Drv_MobNo2);
                    txtDriverBankName.Text = Convert.ToString(objAcntMast.DrvBankAc_Nm);
                    txtDriverBankAddrs.Text = Convert.ToString(objAcntMast.DrvBankAc_Adres);
                    txtDriverRTGC.Text = Convert.ToString(objAcntMast.DrvAc_RtgsCode);
                    txtDriverHazardousL.Text = Convert.ToString(objAcntMast.DrvHazardLic_No);

                    if (objAcntMast.DrvHazardLic_NoExpDt != null)
                        txtHazardousExpiryDate.Text = Convert.ToDateTime(objAcntMast.DrvHazardLic_NoExpDt).ToString("dd-MM-yyyy");

                    ViewState["LincenceNo"] = string.IsNullOrEmpty(Convert.ToString(objAcntMast.DrvLicnc_NO)) ? "" : Convert.ToString(objAcntMast.DrvLicnc_NO);
                    ViewState["ExpiryDate"] = string.IsNullOrEmpty(Convert.ToString(objAcntMast.DrvLNo_ExpDate)) ? "" : Convert.ToDateTime(objAcntMast.DrvLNo_ExpDate).ToString("dd-MM-yyyy");
                    ViewState["Authority"] = string.IsNullOrEmpty(Convert.ToString(objAcntMast.DrvAuthrty_Plc)) ? "" : Convert.ToString(objAcntMast.DrvAuthrty_Plc);
                    ViewState["Account"] = string.IsNullOrEmpty(Convert.ToString(objAcntMast.DrvAcnt_No)) ? "" : Convert.ToString(objAcntMast.DrvAcnt_No);
                    ViewState["Guarantor"] = string.IsNullOrEmpty(Convert.ToString(objAcntMast.DrvGurntr_Idno)) ? "0" : Convert.ToString(objAcntMast.DrvGurntr_Idno);
                    ViewState["varified"] = string.IsNullOrEmpty(Convert.ToString(objAcntMast.DrvLNo_Verfd)) ? "0" : Convert.ToString(objAcntMast.DrvLNo_Verfd);

                    ViewState["FatherName"] = string.IsNullOrEmpty(Convert.ToString(objAcntMast.DrvFather_Nm)) ? "" : Convert.ToString(objAcntMast.DrvFather_Nm);
                    ViewState["Address"] = string.IsNullOrEmpty(Convert.ToString(objAcntMast.Drv_Adres)) ? "" : Convert.ToString(objAcntMast.Drv_Adres);
                    ViewState["Mobile1"] = string.IsNullOrEmpty(Convert.ToString(objAcntMast.Drv_MobNo1)) ? "" : Convert.ToString(objAcntMast.Drv_MobNo1);
                    ViewState["Mobile2"] = string.IsNullOrEmpty(Convert.ToString(objAcntMast.Drv_MobNo2)) ? "" : Convert.ToString(objAcntMast.Drv_MobNo2);
                    ViewState["BankName"] = string.IsNullOrEmpty(Convert.ToString(objAcntMast.DrvBankAc_Nm)) ? "" : Convert.ToString(objAcntMast.DrvBankAc_Nm);
                    ViewState["BankAddress"] = string.IsNullOrEmpty(Convert.ToString(objAcntMast.DrvBankAc_Adres)) ? "" : Convert.ToString(objAcntMast.DrvBankAc_Adres);
                    ViewState["RTGScode"] = string.IsNullOrEmpty(Convert.ToString(objAcntMast.DrvAc_RtgsCode)) ? "" : Convert.ToString(objAcntMast.DrvAc_RtgsCode);
                    ViewState["HazardousLicence"] = string.IsNullOrEmpty(Convert.ToString(objAcntMast.DrvHazardLic_No)) ? "" : Convert.ToString(objAcntMast.DrvHazardLic_No);
                    ViewState["HazardousExpiryDate"] = string.IsNullOrEmpty(Convert.ToString(objAcntMast.DrvHazardLic_NoExpDt)) ? "" : Convert.ToDateTime(objAcntMast.DrvHazardLic_NoExpDt).ToString("dd-MM-yyyy");
                }
                //Other Charge.........
                txtdetenPlantchrg.Text =string.IsNullOrEmpty(Convert.ToString(objAcntMast.detenPlant_charg)) ? "0.00" :Convert.ToString(objAcntMast.detenPlant_charg);
                txtdetenPortchrg.Text = string.IsNullOrEmpty(Convert.ToString(objAcntMast.detenPort_charg)) ? "0.00" : Convert.ToString(objAcntMast.detenPort_charg);
                txtcontainerchrg.Text = string.IsNullOrEmpty(Convert.ToString(objAcntMast.Container_charg)) ? "0.00" : Convert.ToString(objAcntMast.Container_charg);
                //
                //ddlAccountType.Enabled = false;
                ddlAccountSubGroup.SelectedValue = Convert.ToString(objAcntMast.ASubGrp_Idno);
                ddlBalanceType.SelectedValue = Convert.ToString(objAcntMast.Bal_Type);
                txtOpBal.Text = Convert.ToString(objAcntMast.Open_Bal);
                chkServExmpt.Checked = Convert.ToBoolean(objAcntMast.ServTax_Exmpt);
                txtcontPrsn.Text = Convert.ToString(objAcntMast.Cont_Person);
                txtContMob.Text = Convert.ToString(objAcntMast.Cont_Mobile);
                txtPinCode.Text = (Convert.ToString(objAcntMast.Pin_Code)=="0")? "" : Convert.ToString(objAcntMast.Pin_Code);
                txtFax.Text = Convert.ToString(objAcntMast.Fax_No);
                chkStatus.Checked = Convert.ToBoolean(objAcntMast.Status);
                ddlCompany.SelectedValue = Convert.ToString(objAcntMast.PetrolComp_Idno);
                ddlPrincComp.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objAcntMast.PComp_Idno)) ? "0" : Convert.ToString(objAcntMast.PComp_Idno);
                txtPanNo.Text = string.IsNullOrEmpty(Convert.ToString(objAcntMast.Pan_No)) ? "" : Convert.ToString(objAcntMast.Pan_No);
                txtLSTNo.Text = string.IsNullOrEmpty(Convert.ToString(objAcntMast.Lst_No)) ? "" : Convert.ToString(objAcntMast.Lst_No);
                txtcstNo.Text = string.IsNullOrEmpty(Convert.ToString(objAcntMast.Cst_No)) ? "" : Convert.ToString(objAcntMast.Cst_No);
                hidid.Value = Convert.ToString(objAcntMast.Acnt_Idno);
                txtGST.Text = string.IsNullOrEmpty(Convert.ToString(objAcntMast.LdgrGSTIN_No)) ? "" : Convert.ToString(objAcntMast.LdgrGSTIN_No);
                txtShortName.Text = string.IsNullOrEmpty(Convert.ToString(objAcntMast.Short_Name)) ? "" : Convert.ToString(objAcntMast.Short_Name);
                if (objDocHolder != null && objDocHolder.Count > 0)
                {
                    dtTemp = CreateDt();
                    for (int counter = 0; counter < objDocHolder.Count; counter++)
                    {
                        string strDocName = Convert.ToString(DataBinder.Eval(objDocHolder[counter], "DocName"));
                        string strRemark = Convert.ToString(DataBinder.Eval(objDocHolder[counter], "DocRemark"));
                        string strImage = Convert.ToString(DataBinder.Eval(objDocHolder[counter], "DocImage"));

                        ApplicationFunction.DatatableAddRow(dtTemp, counter + 1, strDocName, strRemark, strImage);
                    }
                    ViewState["DocumentHolderTable"] = dtTemp;
                    this.BindDocumentDataGrid();
                }
                ddlTitle.Focus();
            }
        }
        private void BindDateRange()
        {
            FinYearDAL objDAL = new FinYearDAL();
            ddlDateRange.DataSource = objDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objDAL = null;
        }
        protected void BindDocumentDataGrid()
        {
            dtTemp = (DataTable)ViewState["DocumentHolderTable"];
            grdDocHolder.DataSource = (DataTable)ViewState["DocumentHolderTable"];
            grdDocHolder.DataBind();

            TotalDocumentAdd.Text = dtTemp.Rows.Count.ToString();
        }

        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl",
                "Id", "String",
                "DocName", "String",
                "Remark", "String",
                "Image", "String"
                );
            return dttemp;
        }

        public void FillGridValue()
        {
            string filename = String.Empty, filePath = String.Empty, VirtualBinaryImage = "";

            decimal size = Math.Round(((decimal)fuPicture.PostedFile.ContentLength / (decimal)1024), 2);
            if (size > 600)
            {
                lblimgError.Visible = true;
                lblimgError.Text = "File size must not exceed 600 KB.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowDocumentHolder()", true);
            }
            else
            {
                try
                {
                    lblimgError.Visible = false;

                    string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg" };
                    HttpPostedFile File1 = fuPicture.PostedFile;
                    string ext = System.IO.Path.GetExtension(fuPicture.PostedFile.FileName);

                    bool isValidFile = false;

                    for (int i = 0; i < validFileTypes.Length; i++)
                    {
                        if (ext == "." + validFileTypes[i])
                        {
                            isValidFile = true;
                            break;
                        }
                    }
                    if (!isValidFile)
                    {
                        string strMsg = "Invalid File. Please upload a File with extension " + string.Join(",", validFileTypes);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                        imgEmp.ImageUrl = "";
                    }
                    else
                    {
                        if (fuPicture.HasFile)
                        {
                            if (fuPicture.PostedFile.ContentLength < 5024000)
                            {
                                string subPath = "C:\\tmpImg"; // your code goes here
                                bool exists = System.IO.Directory.Exists(subPath);

                                if (!exists)
                                {
                                    System.IO.Directory.CreateDirectory(subPath);
                                }

                                filename = Path.GetFileName(fuPicture.FileName);
                                filePath = subPath + "\\" + filename;
                                fuPicture.SaveAs(filePath);

                                byte[] byteArray = null;
                                HttpPostedFile file = (HttpPostedFile)fuPicture.PostedFile;

                                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                                {
                                    byteArray = new byte[fs.Length];
                                    int iBytesRead = fs.Read(byteArray, 0, (int)fs.Length);
                                    if (byteArray != null && byteArray.Length > 0)
                                    {
                                        // Convert the byte into image
                                        string base64String = Convert.ToBase64String(byteArray, 0, byteArray.Length);
                                        VirtualBinaryImage = "data:image/png;base64," + base64String;

                                    }
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {

                }

                dtTemp = CreateDt();

                dtTemp = (DataTable)ViewState["DocumentHolderTable"];
                Int32 ROWCount = Convert.ToInt32(dtTemp.Rows.Count) - 1;
                int id = dtTemp.Rows.Count == 0 ? 1 : (Convert.ToInt32(dtTemp.Rows[ROWCount]["id"])) + 1;

                ApplicationFunction.DatatableAddRow(dtTemp, id, txtDocName.Text.Trim(), txtDocRemark.Text.Trim(), VirtualBinaryImage);
                ViewState["DocumentHolderTable"] = dtTemp;
                this.BindDocumentDataGrid();

                txtDocName.Text = string.Empty;
                txtDocRemark.Text = string.Empty;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowDocumentHolder()", true);
            }
        }
        #endregion

        #region Control Events...
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindCitywithStateId(Convert.ToInt32(ddlState.SelectedValue));
            ddlCity.Focus();
            this.BindDistrictwithStateId(Convert.ToInt32(ddlState.SelectedValue));
            ddlDistrict.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "EnabaleDisable()", true);
        }
        protected void txtAccountPrtyName_TextChanged(object sender, EventArgs e)
        {
            txtcontPrsn.Text = txtAccountPrtyName.Text;
        }
        #endregion

        #region Buttons....

        protected void lnkBtnCity_Click(object sender, EventArgs e)
        {
            this.BindCitywithStateId(Convert.ToInt32(ddlState.SelectedValue));
        }
        protected void lnkBtnDistrict_Click(object sender, EventArgs e)
        {
            this.BindDistrictwithStateId(Convert.ToInt32(ddlState.SelectedValue));
        }
        protected void lnkBtnCompany_Click(object sender, EventArgs e)
        {
            this.BindPCompName();
        }
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {           
            bool validGSTIN = Convert.ToBoolean((hidValidGSTIN.Value == null || hidValidGSTIN.Value == "") ? "true" : hidValidGSTIN.Value);
            if (!validGSTIN) return;
            string GSTINNO = string.IsNullOrEmpty(Convert.ToString(txtGST.Text.Trim())) ? "" : Convert.ToString(txtGST.Text.Trim());
            //if (ddlAccountType.SelectedValue == "2")
            //{
            //    if (GSTINNO.Length != 15)
            //    {
            //        ShowMessageErr("GST Number must be 15 digit!");
            //        return;
            //    }
            //}
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            LedgerAccountDAL objAcntMaster = new LedgerAccountDAL();
            Int64 intAcntIdno = 0;

            DateTime? ExpiryDate = null;
            DateTime? HazardousExpiryDate = null;

            if ((ViewState["ExpiryDate"] != null) && (Convert.ToString(ViewState["ExpiryDate"]) != ""))
            { ExpiryDate = Convert.ToDateTime(Classes.ApplicationFunction.mmddyyyy(ViewState["ExpiryDate"].ToString())); }
            else { ExpiryDate = null; }

            if ((ViewState["HazardousExpiryDate"] != null) && (Convert.ToString(ViewState["HazardousExpiryDate"]) != ""))
            {
                HazardousExpiryDate = Convert.ToDateTime(Classes.ApplicationFunction.mmddyyyy(ViewState["HazardousExpiryDate"].ToString()));

            }
            else { HazardousExpiryDate = null; }

            if ((Convert.ToInt32(ddlAccountType.SelectedValue) == 3) || (Convert.ToInt32(ddlAccountType.SelectedValue) == 1))
            {
                rfvState.Enabled = false; rfvCity.Enabled = false;
                rfvCity.Visible = false; rfvState.Visible = false;
            }


            if (ddlAccountType.SelectedItem.Text == "Driver")
            {
                if (ViewState["LincenceNo"] != null && ViewState["ExpiryDate"] != null)
                {
                    if (Convert.ToString(hidid.Value) == "")
                    {
                        txtAccount.Text = "";
                        txtBankName.Text="";
                        txtBranchName.Text = "";
                        txtIfscNo.Text="";
                        txtdetenPlantchrg.Text = "";
                        txtcontainerchrg.Text = "";
                        txtdetenPortchrg.Text="";
                        dtTemp = CreateDt();
                        dtTemp = (DataTable)ViewState["DocumentHolderTable"];
                        intAcntIdno = objAcntMaster.Insert(Convert.ToInt64(ddlTitle.SelectedValue), txtAccountPrtyName.Text.Trim(), txtAccountPrtyNameHindi.Text.Trim(),
                            Convert.ToInt64(ddlAccountType.SelectedValue), Convert.ToInt64(ddlAccountSubGroup.SelectedValue), Convert.ToInt32(ddlBalanceType.SelectedValue),
                           (txtOpBal.Text.Trim() == "") ? 0 : Convert.ToDouble(txtOpBal.Text.Trim()), (txtagntCommision.Text.Trim() == "") ? 0 : Convert.ToDouble(txtagntCommision.Text.Trim()), Convert.ToBoolean(chkServExmpt.Checked),
                           txtcontPrsn.Text.Trim(), txtContMob.Text.Trim(), txtContEmail.Text.Trim(), txtAddress1.Text.Trim(), txtAddress2.Text.Trim(), Convert.ToInt64(ddlState.SelectedValue),
                           Convert.ToInt64(ddlCity.SelectedValue), Convert.ToInt64(ddlDistrict.SelectedValue), Convert.ToBoolean(chkStatus.Checked), (txtPinCode.Text.Trim() == "") ? 0 : Convert.ToInt32(txtPinCode.Text.Trim()), txtFax.Text.Trim(), Convert.ToInt32(ddlDateRange.SelectedValue), ViewState["LincenceNo"].ToString(), ExpiryDate, ViewState["Authority"].ToString(), ViewState["Account"].ToString(), Convert.ToInt32(ViewState["Guarantor"].ToString()), Convert.ToBoolean(ViewState["varified"].ToString()), ViewState["FatherName"].ToString(), ViewState["Address"].ToString(), ViewState["Mobile1"].ToString(), ViewState["Mobile2"].ToString(), ViewState["BankName"].ToString(), ViewState["BankAddress"].ToString(), ViewState["RTGScode"].ToString(), ViewState["HazardousLicence"].ToString(), HazardousExpiryDate, Convert.ToInt64(ddlCompany.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), txtTin.Text.Trim(), empIdno, Convert.ToInt64(ddlPrincComp.SelectedValue), Convert.ToString(txtPanNo.Text), txtAccount.Text, txtBankName.Text, txtBranchName.Text, txtIfscNo.Text, (txtdetenPlantchrg.Text.Trim() == "") ? 0 : Convert.ToDouble(txtdetenPlantchrg.Text.Trim()), (txtdetenPortchrg.Text.Trim() == "") ? 0 : Convert.ToDouble(txtdetenPortchrg.Text.Trim()), (txtcontainerchrg.Text.Trim() == "") ? 0 : Convert.ToDouble(txtcontainerchrg.Text.Trim()), txtLSTNo.Text.Trim(), txtcstNo.Text.Trim(), GSTINNO, Convert.ToString(txtShortName.Text.Trim()), dtTemp);
                    }
                    else if (Convert.ToString(hidid.Value) != "")
                    {
                        dtTemp = CreateDt();
                        dtTemp = (DataTable)ViewState["DocumentHolderTable"];
                        intAcntIdno = objAcntMaster.Update(Convert.ToInt64(ddlTitle.SelectedValue), txtAccountPrtyName.Text.Trim(), txtAccountPrtyNameHindi.Text.Trim(),
                            Convert.ToInt64(ddlAccountType.SelectedValue), Convert.ToInt64(ddlAccountSubGroup.SelectedValue), Convert.ToInt32(ddlBalanceType.SelectedValue),
                            ((Convert.ToString(txtOpBal.Text) == "") ? 0.00 : Convert.ToDouble(txtOpBal.Text.Trim())), ((Convert.ToString(txtagntCommision.Text) == "") ? 0.00 : Convert.ToDouble(txtagntCommision.Text.Trim())), Convert.ToBoolean(chkServExmpt.Checked),
                            txtcontPrsn.Text.Trim(), txtContMob.Text.Trim(), txtContEmail.Text.Trim(), txtAddress1.Text.Trim(), txtAddress2.Text.Trim(), Convert.ToInt64(ddlState.SelectedValue),
                            Convert.ToInt64(ddlCity.SelectedValue), Convert.ToInt64(ddlDistrict.SelectedValue), Convert.ToBoolean(chkStatus.Checked), (txtPinCode.Text.Trim() == "") ? 0 : Convert.ToInt32(txtPinCode.Text.Trim()), txtFax.Text.Trim(), Convert.ToInt32(hidid.Value), Convert.ToInt32(ddlDateRange.SelectedValue), ViewState["LincenceNo"].ToString(), ExpiryDate, ViewState["Authority"].ToString(), ViewState["Account"].ToString(), Convert.ToInt32(ViewState["Guarantor"].ToString()), Convert.ToBoolean(ViewState["varified"].ToString()), ViewState["FatherName"].ToString(), ViewState["Address"].ToString(), ViewState["Mobile1"].ToString(), ViewState["Mobile2"].ToString(), ViewState["BankName"].ToString(), ViewState["BankAddress"].ToString(), ViewState["RTGScode"].ToString(), ViewState["HazardousLicence"].ToString(), HazardousExpiryDate, Convert.ToInt64(ddlCompany.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), txtTin.Text.Trim(), empIdno, Convert.ToInt64(Request.Form[ddlPrincComp.UniqueID]), Convert.ToString(txtPanNo.Text), txtAccount.Text, txtBankName.Text, txtBranchName.Text, txtIfscNo.Text, (txtdetenPlantchrg.Text.Trim() == "") ? 0 : Convert.ToDouble(txtdetenPlantchrg.Text.Trim()), (txtdetenPortchrg.Text.Trim() == "") ? 0 : Convert.ToDouble(txtdetenPortchrg.Text.Trim()), (txtcontainerchrg.Text.Trim() == "") ? 0 : Convert.ToDouble(txtcontainerchrg.Text.Trim()), txtLSTNo.Text.Trim(), txtcstNo.Text.Trim(), GSTINNO, Convert.ToString(txtShortName.Text.Trim()), dtTemp);
                    }
                    objAcntMaster = null;
                    if (intAcntIdno > 0)
                    {
                        if (string.IsNullOrEmpty(hidid.Value) == false)
                        {
                            ShowMessage("Record updated successfully.");
                        }
                        else
                        {
                            ShowMessage("Record saved successfully.");
                        }
                        this.ClearControls();
                    }
                    else if (intAcntIdno < 0)
                    {
                       ShowMessageErr("Record already exists!");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(hidid.Value) == false)
                        {
                            ShowMessageErr("Record not updated!");
                        }
                        else
                        {
                            ShowMessageErr("Record not saved!");
                        }
                    } 
                    ddlTitle.Focus();
                }
                else
                {
                    ShowMessageErr("Please check driver licence number and expiry date");
                }
            }
            else
            {
                if (Convert.ToString(hidid.Value) == "")
                {
                    dtTemp = CreateDt();
                    dtTemp = (DataTable)ViewState["DocumentHolderTable"];
                    intAcntIdno = objAcntMaster.Insert(Convert.ToInt64(ddlTitle.SelectedValue), txtAccountPrtyName.Text.Trim(), txtAccountPrtyNameHindi.Text.Trim(),
                        Convert.ToInt64(ddlAccountType.SelectedValue), Convert.ToInt64(ddlAccountSubGroup.SelectedValue), Convert.ToInt32(ddlBalanceType.SelectedValue),
                       (txtOpBal.Text.Trim() == "") ? 0 : Convert.ToDouble(txtOpBal.Text.Trim()), (txtagntCommision.Text.Trim() == "") ? 0 : Convert.ToDouble(txtagntCommision.Text.Trim()), Convert.ToBoolean(chkServExmpt.Checked),
                       txtcontPrsn.Text.Trim(), txtContMob.Text.Trim(), txtContEmail.Text.Trim(), txtAddress1.Text.Trim(), txtAddress2.Text.Trim(), Convert.ToInt64(ddlState.SelectedValue),
                       Convert.ToInt64(ddlCity.SelectedValue), Convert.ToInt64(ddlDistrict.SelectedValue), Convert.ToBoolean(chkStatus.Checked), (txtPinCode.Text.Trim() == "") ? 0 : Convert.ToInt32(txtPinCode.Text.Trim()), txtFax.Text.Trim(), Convert.ToInt32(ddlDateRange.SelectedValue), null, ExpiryDate, null, null, 0, false, null, null, null, null, null, null, null, null, HazardousExpiryDate, Convert.ToInt64(ddlCompany.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), txtTin.Text.Trim(), empIdno, Convert.ToInt64(ddlPrincComp.SelectedValue), Convert.ToString(txtPanNo.Text.Trim()), string.IsNullOrEmpty(txtAccount.Text) ? "" : Convert.ToString(txtAccount.Text.Trim()), string.IsNullOrEmpty(txtBankName.Text) ? "" : Convert.ToString(txtBankName.Text), string.IsNullOrEmpty(txtBranchName.Text) ? "" : Convert.ToString(txtBranchName.Text), string.IsNullOrEmpty(txtIfscNo.Text) ? "" : Convert.ToString(txtIfscNo.Text), (txtdetenPlantchrg.Text.Trim() == "") ? 0 : Convert.ToDouble(txtdetenPlantchrg.Text.Trim()), (txtdetenPortchrg.Text.Trim() == "") ? 0 : Convert.ToDouble(txtdetenPortchrg.Text.Trim()), (txtcontainerchrg.Text.Trim() == "") ? 0 : Convert.ToDouble(txtcontainerchrg.Text.Trim()), txtLSTNo.Text.Trim(), txtcstNo.Text.Trim(), GSTINNO, Convert.ToString(txtShortName.Text.Trim()), dtTemp);
                }
                else if (Convert.ToString(hidid.Value) != "")
                {
                    dtTemp = CreateDt();
                    dtTemp = (DataTable)ViewState["DocumentHolderTable"];
                    intAcntIdno = objAcntMaster.Update(Convert.ToInt64(ddlTitle.SelectedValue), txtAccountPrtyName.Text.Trim(), txtAccountPrtyNameHindi.Text.Trim(),
                        Convert.ToInt64(ddlAccountType.SelectedValue), Convert.ToInt64(ddlAccountSubGroup.SelectedValue), Convert.ToInt32(ddlBalanceType.SelectedValue),
                        ((Convert.ToString(txtOpBal.Text) == "") ? 0.00 : Convert.ToDouble(txtOpBal.Text.Trim())), ((Convert.ToString(txtagntCommision.Text) == "") ? 0.00 : Convert.ToDouble(txtagntCommision.Text.Trim())), Convert.ToBoolean(chkServExmpt.Checked),
                        txtcontPrsn.Text.Trim(), txtContMob.Text.Trim(), txtContEmail.Text.Trim(), txtAddress1.Text.Trim(), txtAddress2.Text.Trim(), Convert.ToInt64(ddlState.SelectedValue),
                        Convert.ToInt64(ddlCity.SelectedValue), Convert.ToInt64(ddlDistrict.SelectedValue), Convert.ToBoolean(chkStatus.Checked), (txtPinCode.Text.Trim() == "") ? 0 : Convert.ToInt32(txtPinCode.Text.Trim()), txtFax.Text.Trim(), Convert.ToInt32(hidid.Value), Convert.ToInt32(ddlDateRange.SelectedValue), null, ExpiryDate, null, null, 0, false, null, null, null, null, null, null, null, null, HazardousExpiryDate, Convert.ToInt64(ddlCompany.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), txtTin.Text.Trim(), empIdno, Convert.ToInt64(ddlPrincComp.SelectedValue), Convert.ToString(txtPanNo.Text.Trim()), string.IsNullOrEmpty(txtAccount.Text) ? "" : Convert.ToString(txtAccount.Text.Trim()), string.IsNullOrEmpty(txtBankName.Text) ? "" : Convert.ToString(txtBankName.Text), string.IsNullOrEmpty(txtBranchName.Text) ? "" : Convert.ToString(txtBranchName.Text), string.IsNullOrEmpty(txtIfscNo.Text) ? "" : Convert.ToString(txtIfscNo.Text), (txtdetenPlantchrg.Text.Trim() == "") ? 0 : Convert.ToDouble(txtdetenPlantchrg.Text.Trim()), (txtdetenPortchrg.Text.Trim() == "") ? 0 : Convert.ToDouble(txtdetenPortchrg.Text.Trim()), (txtcontainerchrg.Text.Trim() == "") ? 0 : Convert.ToDouble(txtcontainerchrg.Text.Trim()), txtLSTNo.Text.Trim(), txtcstNo.Text.Trim(),  GSTINNO, Convert.ToString(txtShortName.Text.Trim()), dtTemp);
                }
                objAcntMaster = null;
                
                if (intAcntIdno > 0)
                {
                    if (string.IsNullOrEmpty(hidid.Value) == false)
                    {
                        ShowMessage("Record updated successfully.");
                    }
                    else
                    {
                        ShowMessage("Record saved successfully.");
                    }
                    this.ClearControls();
                }
                else if (intAcntIdno < 0)
                {
                    ShowMessageErr("Record already exists.");
                }
                else
                {
                    if (string.IsNullOrEmpty(hidid.Value) == false)
                    {
                        ShowMessageErr("Record not updated.");
                    }
                    else
                    {
                        ShowMessageErr("Record not saved.");
                    }
                } 
                ddlTitle.Focus();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "OnChangeDriver();onChangeAccSubGrp();EnabaleDisable();", true);
            if (ddlAccountType.SelectedValue == "10")
                rfvpetroCompany.Enabled = true;
            else
                rfvpetroCompany.Enabled = false;
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (hidid.Value != null && hidid.Value != "")
            {
                Populate(Convert.ToInt32(hidid.Value));
            }
            else
            {
                this.ClearControls();
            } 
        }
        protected void lnkbtnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("LedgerMaster.aspx");
        }
        protected void imgbtnDriver_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "openModal();OnChangeDriver();onChangeAccSubGrp();EnabaleDisable();", true);
            if (ddlAccountType.SelectedValue == "10")
                rfvpetroCompany.Enabled = true;
            else
                rfvpetroCompany.Enabled = false; 
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "openModal();", true);
        }
        protected void lnkbtnOk_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDriverHazardousL.Text.Trim()) == false)
            {
                if (string.IsNullOrEmpty(txtHazardousExpiryDate.Text.Trim()) == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('Please select expiry date!')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "openModal()", true);
                    return;
                }
            }
            //Do Null because it can be comes from Populate
            ViewState["LincenceNo"] = null;
            ViewState["ExpiryDate"] = null;
            ViewState["Authority"] = null;
            ViewState["Account"] = null;
            ViewState["Guarantor"] = null;
            ViewState["varified"] = null;

            ViewState["FatherName"] = null;
            ViewState["Address"] = null;
            ViewState["Mobile1"] = null;
            ViewState["Mobile2"] = null;
            ViewState["BankName"] = null;
            ViewState["BankAddress"] = null;
            ViewState["RTGScode"] = null;
            ViewState["HazardousLicence"] = null;
            ViewState["HazardousExpiryDate"] = null;


            /////Assian value when Ok Click

            ViewState["LincenceNo"] = txtlicense.Text.Trim();
            ViewState["ExpiryDate"] = txtExpiryDate.Text.Trim();
            ViewState["Authority"] = txtauthority.Text.Trim();
            ViewState["Account"] = txtaccountno.Text.Trim();
            ViewState["Guarantor"] = Drpgurenter.SelectedValue;
            ViewState["varified"] = chkVarified.Checked;

            ViewState["FatherName"] = txtDriverSOF.Text.Trim();
            ViewState["Address"] = txtDriverAddress.Text.Trim();
            ViewState["Mobile1"] = txtDriverMobileNo1.Text.Trim();
            ViewState["Mobile2"] = txtDriverMobileNo2.Text.Trim();
            ViewState["BankName"] = txtDriverBankName.Text.Trim();
            ViewState["BankAddress"] = txtDriverBankAddrs.Text.Trim();
            ViewState["RTGScode"] = txtDriverRTGC.Text.Trim();
            ViewState["HazardousLicence"] = txtDriverHazardousL.Text.Trim();
            ViewState["HazardousExpiryDate"] = txtHazardousExpiryDate.Text.Trim();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "OnChangeDriver()", true);
        }
        protected void lnkbtnClose_OnClick(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "HideDialog();OnChangeDriver();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "HideDialog();OnChangeDriver();onChangeAccSubGrp();EnabaleDisable();", true);
            if (ddlAccountType.SelectedValue == "10")
                rfvpetroCompany.Enabled = true;
            else
                rfvpetroCompany.Enabled = false;
         
        }
        #endregion

        #region Grid Events
        protected void grdDocHolder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnDelete = (ImageButton)e.Row.FindControl("imgBtnDelete");
                imgBtnDelete.Visible = true;
            }
        }

        protected void grdDocHolder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDocHolder.PageIndex = e.NewPageIndex;

        }

        protected void grdDocHolder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strMsg = string.Empty;
            if (e.CommandName == "cmddelete")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                dtTemp = (DataTable)ViewState["DocumentHolderTable"];
                DataRow dr = dtTemp.Rows[ID];
                dr.Delete();

                this.BindDocumentDataGrid();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowDocumentHolder()", true);
            }
        }
        #endregion
    }
}