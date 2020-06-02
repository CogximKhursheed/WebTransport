using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using WebTransport.Account;
using System.Data;
using System.IO;
//using WebTransport.Model;

namespace WebTransport
{
    public partial class LorryMaster : Pagebase
    {
        #region Private Variable..
        private int intFormId = 20;
        DataTable dtTemp = new DataTable();
        DataTable dtTemp1 = new DataTable();
        DataTable DtSerialTemp = new DataTable();
        double ins; int Fit, Insurnce, RC, Nat, Auth;
        #endregion

        #region PageLoad Events...
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
                    lnkBtnSave.Visible = false;
                }
                if (base.View == false)
                {
                    lblViewList.Visible = false;
                }

                //Date Fields
                BindState();
                BindDateRange();
                txtFitValidDat.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtRCValidDat.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtNatPermitDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtAuthDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtValidDat.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtFinPeriodFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtFinPeriodTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");

                //Validation
                this.BindLorryType();
                txtInsurAmount.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtLorrymke.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                txtPanNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                txtOwnrNme.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event)");
                txtChasisNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                txtEngineNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                txtLorryNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumerAndDotAndSlash(event);");
                txtNumberOfTyres.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtLorrymkeYear.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                //Lorry Details Div
                txtTrollyLength.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtTrollyheight.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtTrollyWeight.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtTyresNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");

                //Insurance Details Div
                //txtValidDat.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");

                //Financer Details Div
                txtFinancerAmount.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtEMIAmount.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtTotalInstallment.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                // txtFinPeriodFrom.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                //txtFinPeriodTo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");

                txtwarnFit.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtwarnRC.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtInsWarn.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtAuthWarn.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtNatPerWarn.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");

                chkStatus.Checked = true; ChkcommsnCal.Checked = true;
                this.BindPartyName(); SelectPANType(); BindInsuranceCompany();
                ddllorrytyp.Focus();
                dtTemp = CreateDt();
                ViewState["DocumentHolderTable"] = dtTemp;
                this.BindDocumentDataGrid();

                ddllorrytyp.SelectedValue = "0";
                ddllorrytyp_SelectedIndexChanged(null, null);
               

                if (Request.QueryString["LorryIdno"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["LorryIdno"]));
                    
                    lnkBtnNew.Visible = true;
                    lnkSerialDetl.Visible = true;
                    //ddlPartyName.Enabled = false; 
                    lnkValidFrom.Visible = true;
                    lnkPartyName.Enabled = false;

                }
                else
                {
                    lnkBtnNew.Visible = false;
                    lnkSerialDetl.Visible = false;
                    ddlPartyName.Enabled = true; lnkValidFrom.Visible = false;
                    lnkPartyName.Enabled = true;
                }
                BindLorryPartyDetlRep();
            }
            if (strPostBackControlName == "lnkParty")
            {
                FillGridValue();
            }
        }
        #endregion

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

        #region Button Events...

        protected void btnDocumentHolder_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowDocumentHolder()", true);
        }

        protected void btnInsDetails_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowInsurancedetails()", true);
        }
        protected void btnFinDetails_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowFinancerdetails()", true);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string filename = String.Empty, filePath = String.Empty, VirtualBinaryImage = "";
            //image 
            try
            {
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            dtTemp = (DataTable)ViewState["DocumentHolderTable"];
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    DataRow dr = dtTemp.Rows[i];
                    dr.Delete();
                    this.BindDocumentDataGrid();
                }
            }
        }

        protected void btnPrtyLorry_Click(object sender, ImageClickEventArgs e)
        {
            grdGrdetals.DataSource = null;
            grdGrdetals.DataBind();
            Int32 IprtyIdno = 0;
            if (ddlPartyName.SelectedIndex <= 0)
            {
                IprtyIdno = 0;
            }
            else
            {
                IprtyIdno = Convert.ToInt32(ddlPartyName.SelectedValue);
            }
            LorryMasterDAL objDal = new LorryMasterDAL();
            var lst = objDal.selectPrtyLorryDetails(IprtyIdno);
            if (lst != null && lst.Count > 0)
            {

                grdGrdetals.DataSource = lst;
                grdGrdetals.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient()", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient()", true);
                string msg = "No Record Found";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + msg + "')", true);
            }
        }

        #endregion

        #region Miscellaneous Events...

        private void BindDriver(Int32 var)
        {
            LorryMasterDAL obj = new LorryMasterDAL();
            if (var == 0)
            {
                ddldriverName.DataSource = null;
                var lst = obj.selectOwnerDriverName();
                obj = null;
                if (lst != null && lst.Count > 0)
                {
                    ddldriverName.DataSource = lst;
                    ddldriverName.DataTextField = "Acnt_Name";
                    ddldriverName.DataValueField = "Acnt_Idno";
                    ddldriverName.DataBind();

                }
                ddldriverName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                ddldriverName.DataSource = null;
                var lst = obj.selectHireDriverName();
                obj = null;
                if (lst != null && lst.Count > 0)
                {
                    ddldriverName.DataSource = lst;
                    ddldriverName.DataTextField = "Driver_name";
                    ddldriverName.DataValueField = "Driver_Idno";
                    ddldriverName.DataBind();

                }
                ddldriverName.Items.Insert(0, new ListItem("--Select--", "0"));
            }

        }
        private void BindLorryType()
        {
            LorryMasterDAL Obj = new LorryMasterDAL();
            var EMPS = Obj.SelectLorryType();
            ddllorytype.DataSource = EMPS;
            ddllorytype.DataTextField = "Lorry_Type";
            ddllorytype.DataValueField = "Id";
            ddllorytype.DataBind();
            ddllorytype.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            Obj = null;
        }

        private void BindInsuranceCompany()
        {
            LorryMasterDAL Obj = new LorryMasterDAL();
            var EMPS = Obj.SelectInsuranceComp();
            drpInsuCompName.DataSource = EMPS;
            drpInsuCompName.DataTextField = "Acnt_Name";
            drpInsuCompName.DataValueField = "Acnt_Idno";
            drpInsuCompName.DataBind();
            drpInsuCompName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            Obj = null;
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

     

        private void SelectPANType()
        {
            TaxMastDAL objTaxMastBLL = new TaxMastDAL();
            var lst = objTaxMastBLL.SelectPANType();
            objTaxMastBLL = null;
            ddlPANType.DataSource = lst;
            ddlPANType.DataTextField = "PANType_Name";
            ddlPANType.DataValueField = "PANType_Idno";
            ddlPANType.DataBind();
            ddlPANType.Items.Insert(0, new ListItem("< Choose >", "0"));

        }


        private DataTable CreateStockBlankDataTable()
        {
            DataTable dtTemp = ApplicationFunction.CreateTable("tbl", "SerialNo", "String", "Position", "String", "Company", "String", "Type", "String", "PurFrom", "String", "kms", "String");
            return dtTemp;
        }
        /// <summary>
        /// To Populate all controls
        /// </summary>
        /// <param name="LorryIdno"></param>
        /// 


        private void Populate(int LorryIdno)
        {
            LorryMasterDAL objLorryMast = new LorryMasterDAL();
            var objLorMast = objLorryMast.SelectById(LorryIdno);
            var objDocHolder = objLorryMast.SelectDocHolder(LorryIdno);
            objLorryMast = null;
            if (objLorMast != null)
            {
                ddllorrytyp.SelectedValue = Convert.ToString(objLorMast.Lorry_Type);
                ddllorrytyp_SelectedIndexChanged(null, null);
                txtLorrymke.Text = Convert.ToString(objLorMast.Lorry_Make);
                txtLorrymkeYear.Text = Convert.ToString(objLorMast.Lorry_Year);
                ddllorytype.SelectedValue = Convert.ToString(objLorMast.Type);
                ddldriverName.SelectedValue = Convert.ToString(objLorMast.Driver_Idno);
                chlCalOnDF.Checked = Convert.ToBoolean(objLorMast.CalOn_DF);
                chkStatus.Checked = Convert.ToBoolean(objLorMast.Status);
                ChkcommsnCal.Checked = Convert.ToBoolean(objLorMast.chk_ComsnCal);
                hidlorryidno.Value = Convert.ToString(objLorMast.Lorry_Idno);
                ddlPartyName.SelectedValue = Convert.ToString(objLorMast.Prty_Idno);
                ddlPANType.SelectedValue = Convert.ToString(objLorMast.PANTyp_Idno);
                txtChasisNo.Text = Convert.ToString(objLorMast.Chassis_no);
                txtNumberOfTyres.Text = Convert.ToString(objLorMast.Tyres_No);
                txtEngineNo.Text = Convert.ToString(objLorMast.Eng_No);
                txtOwnrNme.Text = Convert.ToString(objLorMast.Owner_Name);
                txtLorryNo.Text = Convert.ToString(objLorMast.Lorry_No);
                txtTrollyLength.Text = Convert.ToString(objLorMast.Trolly_Length);
                txtTrollyheight.Text = Convert.ToString(objLorMast.Trolly_Height);
                txtTrollyWeight.Text = Convert.ToString(objLorMast.Trolly_Weight);
                txtTyresNo.Text = Convert.ToString(objLorMast.Trolly_TyerNo);
                txtInsuranceNo.Text = Convert.ToString(objLorMast.Ins_No);
                txtInsWarn.Text = Convert.ToString(objLorMast.INSwarn);
                txtwarnFit.Text = Convert.ToString(objLorMast.FITwarn);
                txtwarnRC.Text = Convert.ToString(objLorMast.RCwarn);
                txtNatPerWarn.Text = Convert.ToString(objLorMast.NatWarn);
                txtAuthWarn.Text = Convert.ToString(objLorMast.AuthWarn);
                txtInsurAmount.Text = Convert.ToString(objLorMast.Ins_Amount);
                drpInsuCompName.SelectedValue = Convert.ToString(objLorMast.Ins_Comp_Name);
                txtFinancerName.Text = Convert.ToString(objLorMast.Fin_Name);
                txtFinancerAmount.Text = Convert.ToString(objLorMast.Fin_Amount);
                txtFinPeriodFrom.Text = Convert.ToString(objLorMast.Fin_Period_From);
                txtFinPeriodTo.Text = Convert.ToString(objLorMast.Fin_Period_To);
                txtTotalInstallment.Text = Convert.ToString(objLorMast.Fin_Total_Install);
                txtEMIAmount.Text = Convert.ToString(objLorMast.Fin_EMI_Amount);
                chkLowRate.Checked = Convert.ToBoolean(objLorMast.LowRateWise);
                txtMobileNo.Text = Convert.ToString(objLorMast.Owner_Mobile);
                txtOwneradd.Text = Convert.ToString(objLorMast.Owner_Address);
                hidPartyIdno.Value = Convert.ToString(objLorMast.Prty_Idno);
                lnkUpdateLorryPAN.Visible = true;
                if (objLorMast.Fitness_Date != null)
                {
                    txtFitValidDat.Text = Convert.ToDateTime(objLorMast.Fitness_Date).ToString("dd-MM-yyyy");
                }
                else
                {
                    txtFitValidDat.Text = "";
                }

                if (objLorMast.RC_Date != null)
                {
                    txtRCValidDat.Text = Convert.ToDateTime(objLorMast.RC_Date).ToString("dd-MM-yyyy");
                }
                else
                {
                    txtRCValidDat.Text = "";
                }

                if (objLorMast.Nat_Permit_Date != null)
                {
                    txtNatPermitDate.Text = Convert.ToDateTime(objLorMast.Nat_Permit_Date).ToString("dd-MM-yyyy");
                }
                else
                {
                    txtNatPermitDate.Text = "";
                }


                if (objLorMast.Auth_Permit_Date != null)
                {
                    txtAuthDate.Text = Convert.ToDateTime(objLorMast.Auth_Permit_Date).ToString("dd-MM-yyyy");
                }
                else
                {
                    txtAuthDate.Text = "";
                }


                if (objLorMast.Ins_Valid_Date != null)
                {
                    txtValidDat.Text = Convert.ToDateTime(objLorMast.Ins_Valid_Date).ToString("dd-MM-yyyy");
                }
                else
                {
                    txtValidDat.Text = "";
                }


                if (objLorMast.Fin_Period_From != null)
                {
                    txtFinPeriodFrom.Text = Convert.ToDateTime(objLorMast.Fin_Period_From).ToString("dd-MM-yyyy");
                }
                else
                {
                    txtFinPeriodFrom.Text = "";
                }


                if (objLorMast.Fin_Period_To != null)
                {
                    txtFinPeriodTo.Text = Convert.ToDateTime(objLorMast.Fin_Period_To).ToString("dd-MM-yyyy");
                }
                else
                {
                    txtFinPeriodTo.Text = "";
                }



                ////Convert.ToDateTime(objLorMast.Fitness_Date.ToString());
                ////    objLorMast.Fitness_Date.ToString("dd-MM-yyyy"));

                txtPanNo.Text = Convert.ToString(objLorMast.Pan_No);

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
                txtOwnrNme.Focus();
            }
        }

        private void BindPartyName()
        {
            LorryMasterDAL objLorrMast = new LorryMasterDAL();
            if (Convert.ToInt32(ddllorrytyp.SelectedValue) == 0)
            {
                var LorrMast = objLorrMast.SelectPartyNameOwn();
                ddlPartyName.DataSource = LorrMast;
                ddlPartyName.DataTextField = "Acnt_Name";
                ddlPartyName.DataValueField = "Acnt_Idno";
                ddlPartyName.DataBind();
                ddlPartyName.Items.Insert(0, new ListItem("< Choose Party Name >", "0"));
            }
            if (Convert.ToInt32(ddllorrytyp.SelectedValue) == 1)
            {
                var LorrMast = objLorrMast.SelectPartyName();
                objLorrMast = null;
                ddlPartyName.DataSource = LorrMast;
                ddlPartyName.DataTextField = "Acnt_Name";
                ddlPartyName.DataValueField = "Acnt_Idno";
                ddlPartyName.DataBind();
                ddlPartyName.Items.Insert(0, new ListItem("< Choose Party Name >", "0"));
            }
            ddlPartyName.Focus();
        }

        private void BindTyrePosition(DropDownList ddlPostion)
        {
            LorryMasterDAL objLorrMast = new LorryMasterDAL();
            var LorrMast = objLorrMast.SelectTyrePostion();
            objLorrMast = null;
            ddlPostion.DataSource = LorrMast;
            ddlPostion.DataTextField = "Position_name";
            ddlPostion.DataValueField = "Position_id";
            ddlPostion.DataBind();

            ddlPostion.Items.Insert(0, new ListItem("< Choose Position >", "0"));

        }

        protected void BindDocumentDataGrid()
        {
            dtTemp = (DataTable)ViewState["DocumentHolderTable"];
            grdDocHolder.DataSource = (DataTable)ViewState["DocumentHolderTable"];
            grdDocHolder.DataBind();

            TotalDocumentAdd.Text = dtTemp.Rows.Count.ToString();
        }

        /// <summary>
        /// To Clear all controls
        /// </summary>
        /// 

        private void ClearControls()
        {
            ddllorrytyp.SelectedValue = "0";
            ddldriverName.SelectedValue = "0";
            txtLorrymke.Text = txtPanNo.Text = string.Empty;
            chkStatus.Checked = true; ChkcommsnCal.Checked = false; chlCalOnDF.Checked = true;// false;
            hidlorryidno.Value = string.Empty;
            ddlPartyName.SelectedValue = "0";
            txtChasisNo.Text = string.Empty;
            txtEngineNo.Text = string.Empty;
            txtLorryNo.Text = string.Empty;
            txtOwnrNme.Text = string.Empty;
            txtLorrymkeYear.Text = string.Empty;
            ddlPANType.SelectedValue = "0";
            txtNumberOfTyres.Text = string.Empty;
            ddllorytype.SelectedValue = "1";
            txtFitValidDat.Text = string.Empty;
            txtRCValidDat.Text = string.Empty;
            txtNatPermitDate.Text = string.Empty;
            txtAuthDate.Text = string.Empty;
            txtInsuranceNo.Text = string.Empty;
            drpInsuCompName.SelectedValue = "0";
            txtValidDat.Text = string.Empty;
            TotalDocumentAdd.Text = "0";
            txtFinancerName.Text = string.Empty;
            txtFinancerAmount.Text = string.Empty;
            txtFinPeriodFrom.Text = string.Empty;
            txtFinPeriodTo.Text = string.Empty;
            txtTotalInstallment.Text = string.Empty;

            dtTemp = null;
            ddllorrytyp.Focus(); 
        }



        protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LorryMasterDAL obj = new LorryMasterDAL();
            if (ddlPartyName.SelectedIndex > 0)
            {
                lnkPrtyLorry.Enabled = true;grdGrdetals.DataSource = null;
                grdGrdetals.DataBind();
            }
            else
            {
                lnkPrtyLorry.Enabled = false;
            }
            chlCalOnDF.Checked = obj.CheckOnDf(Convert.ToInt32(ddlPartyName.SelectedValue));
            ddlPartyName.Focus();

            if (Convert.ToInt32(ddlPartyName.SelectedValue) > 0)
            {
                var OwnDetl = obj.FillOwnerDetl(Convert.ToInt32(ddlPartyName.SelectedValue));
                if (OwnDetl.Count > 0)
                {
                    string Oname, OAddress, OMobile, PanNo;
                    Oname = Convert.ToString(DataBinder.Eval(OwnDetl[0], "OwnerName"));
                    OAddress = Convert.ToString(DataBinder.Eval(OwnDetl[0], "OwnerAddress"));
                    OMobile = Convert.ToString(DataBinder.Eval(OwnDetl[0], "Mobile"));
                    PanNo = Convert.ToString(DataBinder.Eval(OwnDetl[0], "PanNo"));

                    txtOwnrNme.Text = Oname;
                    txtPanNo.Text = PanNo;
                    txtMobileNo.Text = OMobile;
                    txtOwneradd.Text = OAddress;
                }
            }
        }

        protected void ddllorrytyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddllorrytyp.SelectedValue != "0")
                {
                    spanMakeYear.Visible = false;
                    rfvLorrymake.Enabled = false;
                    rfvLorrymakeYear.Enabled = false;
                    ddlPartyName.DataSource = null;
                    this.BindPartyName();
                }
                else
                {
                    spanMakeYear.Visible = true;
                    rfvLorrymake.Enabled = true;
                    rfvLorrymakeYear.Enabled = true;
                }
                LorryMasterDAL obj = new LorryMasterDAL();
                Int32 Typ = 0;
                Typ = Convert.ToInt32(ddllorrytyp.SelectedValue);
                ddldriverName.DataSource = null;
                if (ddldriverName.Items.Count > 0)
                {
                    ddldriverName.Items.Clear();
                }
                BindDriver(Typ);
                ddllorrytyp.Focus();


            }
            catch (Exception Ex)
            {

            }
        }

        protected void ddlLorryNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["dtnew"] = null;
            LorryMasterDAL obj = new LorryMasterDAL();
            var Prty = obj.BindPartyGrid(Convert.ToInt64(ddlLorryNo.SelectedValue));
            if (Prty.Count > 0)
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(string));
                dt.Columns.Add("LorryNo", typeof(string));
                dt.Columns.Add("LorryIdno", typeof(string));
                dt.Columns.Add("AcntName", typeof(string));
                dt.Columns.Add("AcntIdno", typeof(string));
                dt.Columns.Add("ValidFrom", typeof(string));

                for (int i = 0; i < Prty.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = Convert.ToString(i + 1);
                    dr["LorryNo"] = Convert.ToString(DataBinder.Eval(Prty[i], "LorryNo"));
                    dr["LorryIdno"] = Convert.ToString(DataBinder.Eval(Prty[i], "LorryIdno"));
                    dr["AcntName"] = Convert.ToString(DataBinder.Eval(Prty[i], "AcntName"));
                    dr["AcntIdno"] = Convert.ToString(DataBinder.Eval(Prty[i], "AcntIdno"));
                    dr["ValidFrom"] = Convert.ToDateTime(DataBinder.Eval(Prty[i], "ValidFrom")).ToString("MM/dd/yyyy");
                    dt.Rows.Add(dr);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["dtnew"] = dt;
                    int max = Convert.ToInt32(dt.AsEnumerable()
                        .Max(row => row["ID"]));
                    hdnLorryMaxID.Value = Convert.ToString(max);
                }
                grdPrtyAdd.DataSource = dt;
                grdPrtyAdd.DataBind();
            }
            else
            {   
                grdPrtyAdd.DataSource = null;
                grdPrtyAdd.DataBind();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openPartyChangeModal();", true);
        }

        private bool CheckDuplicatieItem()
        {
            bool value = true;
            DataTable dtTemp = (DataTable)ViewState["dtnew"];
            if ((dtTemp != null) && (dtTemp.Rows.Count > 0)) {
                foreach (DataRow row in dtTemp.Rows) 
                {
                    if ((Convert.ToString(row["LorryNo"]) == Convert.ToString(ddlLorryNo.SelectedItem.Text.Trim())) && (Convert.ToString(row["AcntName"]) == Convert.ToString(ddlPartyNameChange.SelectedItem.Text.Trim())) && (Convert.ToDateTime(row["ValidFrom"]).ToString("dd-MM-yyyy") == Convert.ToString(txtValidFromDt1.Text.Trim())))
                    {
                        value = false;
                        return false;

                    }
                    else
                    {
                        value = true;
                    }
                    
                } 
            }
            if (value == false) { return false; }
            else { return true; }
        }
        #endregion

        #region GridView Events...
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

        protected void Populate(Int64 LorryIdno)
        {
            DataTable dtTemp = (DataTable)ViewState["dtnew"];
            if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
            {
                foreach (DataRow row in dtTemp.Rows)
                {
                    if (Convert.ToString(row["ID"]) == Convert.ToString(LorryIdno))
                    {
                        txtValidFromDt1.Text = Convert.ToDateTime(row["ValidFrom"]).ToString("dd-MM-yyyy");
                        ddlLorryNo.SelectedValue = Convert.ToString(row["LorryIdNo"]);
                        ddlPartyNameChange.SelectedValue = Convert.ToString(row["AcntIdno"]);
                    }
                }
                for (int i = dtTemp.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dtTemp.Rows[i];
                    if (Convert.ToString(dr["ID"]) == Convert.ToString(LorryIdno))
                        dr.Delete();
                } 
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openPartyChangeModal();", true);

        }
        protected void grdGrdetals_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            //string[] strPANNo = Convert.ToString(e.CommandArgument).Split("_");
            string[] strArr = Convert.ToString(e.CommandArgument).Split(new char[] { '-' });
            if (e.CommandName == "cmdPan")
            {
                //txtPanNo.Text = strPANNo;
                txtOwnrNme.Text = Convert.ToString(strArr[0]);
                txtPanNo.Text = Convert.ToString(strArr[1]);
            }

            if (e.CommandName == "cmdedit")
            {
                if (e.CommandArgument != "")
                {
                    int LorryIdno = Convert.ToInt32(Request.QueryString["LorryIdno"]);
                    LorryIdno = Convert.ToInt32(e.CommandArgument.ToString());
                    if (string.IsNullOrEmpty(Convert.ToString((LorryIdno))) == false)
                    {
                        this.Populate(Convert.ToInt64(LorryIdno));
                    }
                
                }
            }
        }
        protected void grdPartyChange_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdedit")
            {
                if (Convert.ToString(e.CommandArgument) != "")
                {  
                    Int32 LorryId = Convert.ToInt32(e.CommandArgument.ToString());
                    this.Populate(Convert.ToInt64(LorryId));
                }
            }
        }


        protected void grdPartyChange_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LorryMasterDAL objLorrMast = new LorryMasterDAL();
                Label lblLorryIdno = (Label)e.Row.FindControl("lblLooryidno");
                HiddenField hdnLorryId = (HiddenField)e.Row.FindControl("hdnLorryTempId");
                LinkButton lnkbtnEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
               // Int64 intIdno = objLorrMast.SelectIDforEdit(Convert.ToInt64(lblLorryIdno.Text.Trim()));
                Int64 intIdno = 0;
                if(hdnLorryMaxID.Value!=null)
                intIdno = Convert.ToInt64(hdnLorryMaxID.Value);
                if (Convert.ToInt64(hdnLorryId.Value) == intIdno)
                {
                    lnkbtnEdit.Visible=true;
                }
                else
                {
                    lnkbtnEdit.Visible=false;
                }
            }
        }
        #endregion

        protected void ImgSerialNo_Click(object sender, ImageClickEventArgs e)
        {
            int LorryId = Convert.ToInt32(Request.QueryString["LorryIdno"]);
            if ((string.IsNullOrEmpty(txtNumberOfTyres.Text) ? 0 : Convert.ToInt32(txtNumberOfTyres.Text)) > 0)
            {
                if (LorryId > 0)
                {
                    this.SelectStockforItemPurBill(LorryId, Convert.ToInt32(txtNumberOfTyres.Text));
                }
                lblmsg.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowStck()", true);
            }
            else
            {

            }

        }

        private void SelectStockforItemPurBill(Int64 LorryId, int rows)
        {
            LorryMasterDAL Obj = new DAL.LorryMasterDAL();
            var lst = Obj.SelectStockforPurBill(LorryId);
            Obj = null;
            DtSerialTemp = this.CreateStockBlankDataTable();
            if (lst.Count > 0)
            {
                if (lst.Count > rows)
                {
                    for (int Count = 0; Count < rows; Count++)
                    {
                        string SerialNo, Id, Position, Company, Type, PurFrom, Kms;
                        SerialNo = Convert.ToString(DataBinder.Eval(lst[Count], "Serial_No"));
                        Id = Convert.ToString(DataBinder.Eval(lst[Count], "LorryTyre_id"));
                        Position = Convert.ToString(DataBinder.Eval(lst[Count], "Position"));
                        Company = Convert.ToString(DataBinder.Eval(lst[Count], "Company"));
                        Type = Convert.ToString(DataBinder.Eval(lst[Count], "Type"));
                        PurFrom = Convert.ToString(DataBinder.Eval(lst[Count], "PurFrom"));
                        Kms = Convert.ToString(DataBinder.Eval(lst[Count], "Kms"));

                        ApplicationFunction.DatatableAddRow(DtSerialTemp, SerialNo, Position, Company, Type, PurFrom, Kms);
                    }
                }
                else
                {
                    for (int Count = 0; Count < lst.Count; Count++)
                    {
                        string SerialNo, Id, Position, Company, Type, PurFrom, Kms;
                        SerialNo = Convert.ToString(DataBinder.Eval(lst[Count], "Serial_No"));
                        Id = Convert.ToString(DataBinder.Eval(lst[Count], "LorryTyre_id"));
                        Position = Convert.ToString(DataBinder.Eval(lst[Count], "Position"));
                        Company = Convert.ToString(DataBinder.Eval(lst[Count], "Company"));
                        Type = Convert.ToString(DataBinder.Eval(lst[Count], "Type"));
                        PurFrom = Convert.ToString(DataBinder.Eval(lst[Count], "PurFrom"));
                        Kms = Convert.ToString(DataBinder.Eval(lst[Count], "Kms"));

                        ApplicationFunction.DatatableAddRow(DtSerialTemp, SerialNo, Position, Company, Type, PurFrom, Kms);
                    }
                }
                if (rows > lst.Count)
                {
                    for (int Count = lst.Count; Count < rows; Count++)
                    {
                        ApplicationFunction.DatatableAddRow(DtSerialTemp, string.Empty, 0, string.Empty, 0, string.Empty, string.Empty);
                    }
                }
            }
            else
            {
                this.ManageStockDetail(rows);
            }
            rptstck.DataSource = DtSerialTemp;
            rptstck.DataBind();
        }

        private void ManageStockDetail(Int64 rows)
        {
            DtSerialTemp = this.CreateStockBlankDataTable();

            for (int Count = 1; Count <= rows; Count++)
            {
                ApplicationFunction.DatatableAddRow(DtSerialTemp, string.Empty, 0, string.Empty, 0, string.Empty, string.Empty);
            }
            rptstck.DataSource = DtSerialTemp;
            rptstck.DataBind();
        }

        private void BindLorryPartyDetlRep()
        {
            LorryMasterDAL objLorrMast = new LorryMasterDAL();
            if (Convert.ToInt32(ddllorrytyp.SelectedValue) == 0)
            {
                var LorrMast = objLorrMast.SelectPartyNameOwn();
                ddlPartyNameChange.DataSource = LorrMast;
                ddlPartyNameChange.DataTextField = "Acnt_Name";
                ddlPartyNameChange.DataValueField = "Acnt_Idno";
                ddlPartyNameChange.DataBind();
                ddlPartyNameChange.Items.Insert(0, new ListItem("<Choose Party Name>", "0"));
            }
            else if (Convert.ToInt32(ddllorrytyp.SelectedValue) == 1)
            {
                var LorrMast = objLorrMast.SelectPartyName();
                ddlPartyNameChange.DataSource = LorrMast;
                ddlPartyNameChange.DataTextField = "Acnt_Name";
                ddlPartyNameChange.DataValueField = "Acnt_Idno";
                ddlPartyNameChange.DataBind();
                ddlPartyNameChange.Items.Insert(0, new ListItem("<Choose Party Name>", "0"));
            }

            var LorryNo = objLorrMast.SelectLorryBasisonType(Convert.ToInt64(ddllorrytyp.SelectedValue));
            objLorrMast = null;
            ddlLorryNo.DataSource = LorryNo;
            ddlLorryNo.DataTextField = "Lorry_No";
            ddlLorryNo.DataValueField = "Lorry_Idno";
            ddlLorryNo.DataBind();
            ddlLorryNo.Items.Insert(0, new ListItem("<Choose Lorry No>", "0"));
        }

        protected void imgBtnsaveStck_Click(object sender, ImageClickEventArgs e)
        {
            bool flag = true;
            foreach (RepeaterItem item in rptstck.Items)
            {

                TextBox txtserialNo = (TextBox)item.FindControl("txtserialNo");
                if (txtserialNo.Text.Trim() == string.Empty)
                {
                    lblmsg.Text = "Serial No can't be left blank.";
                    txtserialNo.Focus();
                    flag = false;
                    break;
                }
            }

            string oldchasisno = string.Empty;
            for (int count = 0; count < rptstck.Items.Count; count++)
            {
                TextBox txtserialNo = (TextBox)rptstck.Items[count].FindControl("txtserialNo");
                for (int count1 = count + 1; count1 < rptstck.Items.Count; count1++)
                {
                    TextBox txtserialNo1 = (TextBox)rptstck.Items[count1].FindControl("txtserialNo");

                    if (txtserialNo.Text.Trim() == txtserialNo1.Text.Trim())
                    {
                        lblmsg.Text = "Serial No Already exists in list.";
                        lblmsg.CssClass = "redfont";
                        flag = false;
                        break;
                    }
                }
            }
             
            if (flag == true)
            {
                string msgAlreadyExists = string.Empty;
                string saveMsg = string.Empty;
                int LorryIdno = Convert.ToInt32(Request.QueryString["LorryIdno"]);
                 
                if (msgAlreadyExists != string.Empty)
                {
                    lblmsg.Text = msgAlreadyExists + "  already exists!";
                }
                else
                {
                    LorryMasterDAL obj1 = new LorryMasterDAL();
                    obj1.DeleteAll(LorryIdno);
                    obj1 = null;
                    foreach (RepeaterItem item in rptstck.Items)
                    {
                        LorryMasterDAL obj = new LorryMasterDAL();
                        TextBox txtserialNo = (TextBox)item.FindControl("txtserialNo");
                        //HiddenField hidSerialIdno = (HiddenField)item.FindControl("hidSerialIdno");
                        DropDownList ddlPostion = (DropDownList)item.FindControl("ddlPostion");
                        TextBox txtCompanys = (TextBox)item.FindControl("txtCompanys");
                        DropDownList ddlType = (DropDownList)item.FindControl("ddlType");
                        TextBox txtPurParty = (TextBox)item.FindControl("txtPurParty");
                        TextBox txtKms = (TextBox)item.FindControl("txtKms");

                        string serialNo = txtserialNo.Text;
                        Int32 PostionId = string.IsNullOrEmpty(ddlPostion.SelectedValue) == true ? 0 : Convert.ToInt32(ddlPostion.SelectedValue);
                        string CompName = string.IsNullOrEmpty(txtCompanys.Text) == true ? "" : Convert.ToString(txtCompanys.Text);
                        Int32 Type = string.IsNullOrEmpty(ddlType.SelectedValue) == true ? 0 : Convert.ToInt32(ddlType.SelectedValue);
                        string PurParty = string.IsNullOrEmpty(txtPurParty.Text) == true ? "" : Convert.ToString(txtPurParty.Text);
                        string Kms = string.IsNullOrEmpty(txtKms.Text) == true ? "" : Convert.ToString(txtKms.Text);

                        Int64 value = 0;
                        value = obj.InsertPurBillStock(LorryIdno, serialNo, PostionId, CompName, Type, PurParty, Kms);
                        if (value <= 0)
                        {
                            saveMsg += serialNo + ",";
                        } 
                        obj = null;

                    }
                    if (saveMsg == string.Empty)
                    {
                        lblmsg.Text = "Record saved."; ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", " HideStck()", true);
                        lblmsg.CssClass = "";
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblmsg.Text = "Records not saved Chasisno " + saveMsg;
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                    }

                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowStck()", true);

        }

        protected void rptstck_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList Dl = (DropDownList)e.Item.FindControl("ddlPostion");
                DropDownList DType = (DropDownList)e.Item.FindControl("ddlType");
                BindTyrePosition(Dl);

                DataRowView Dr = (DataRowView)e.Item.DataItem;
                Dl.SelectedValue = Convert.ToString(Dr["Position"]);
                DType.SelectedValue = Convert.ToString(Dr["Type"]);
            } 
        }

        protected void lnkPrtyLorry_Click(object sender, EventArgs e)
        { 
            grdGrdetals.DataSource = null;
            grdGrdetals.DataBind();
            Int32 IprtyIdno = 0;
            if (ddlPartyName.SelectedIndex <= 0)
            {
                IprtyIdno = 0;
                string msg = "Please Select Party Name";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + msg + "')", true);
                return;
            }
            else
            {
                IprtyIdno = Convert.ToInt32(ddlPartyName.SelectedValue);
            }
            LorryMasterDAL objDal = new LorryMasterDAL();
            var lst = objDal.selectPrtyLorryDetails(IprtyIdno);
            if (lst != null && lst.Count > 0)
            {

                grdGrdetals.DataSource = lst;
                grdGrdetals.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openPartyModal();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient()", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openPartyModal();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient()", true);
                string msg = "No Record Found";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + msg + "')", true);
            }
        }

        protected void lnkValidFrom_Click(object sender, EventArgs e)
        {
            DataTable dtnew = CreateDtNew();
            if (dtnew.Rows.Count > 0)
            {
                grdPrtyAdd.DataSource = dtnew;
                grdPrtyAdd.DataBind();
                
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openPartyChangeModal();", true);
        }

        protected void lnkbtnSubmitChange_Click(object sender, EventArgs e)
        {
            dtTemp1 = (DataTable)ViewState["dtnew"];
            if (dtTemp1 == null || dtTemp1.Rows.Count <= 0)
            {
                dtTemp1 = CreateDtNew();
            }
            int LorryIdno = Convert.ToInt32(Request.QueryString["LorryIdno"]);
            int LorryId = Convert.ToInt32(ddlLorryNo.SelectedValue);
            if (Convert.ToString(LorryId) != string.Empty)
            {
                if (CheckDuplicatieItem() == false)
                {
                    lblLorryError.Text="" + ddlLorryNo.SelectedItem.Text + " already selected in grid  with same Lorry,Party and Valid Date!"; ddlPartyName.Focus(); 
                } 
                else 
                {
                    //Int32 ROWCount=0;
                    int id = dtTemp1.Rows.Count == 0 ? 1 : (Convert.ToInt32(dtTemp1.Rows.Count)) + 1;
                    string strLorryName = ddlLorryNo.SelectedItem.Text.Trim();
                    string strLorryIdno = string.IsNullOrEmpty(ddlLorryNo.SelectedValue) ? "0" : (ddlLorryNo.SelectedValue);
                    string strPartyName = ddlPartyNameChange.SelectedItem.Text.Trim();
                    string strPartyIdno = string.IsNullOrEmpty(ddlPartyNameChange.SelectedValue) ? "0" : (ddlPartyNameChange.SelectedValue);
                    string strValidFrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Convert.ToString(txtValidFromDt1.Text))).ToString("MM/dd/yyyy");
                    ApplicationFunction.DatatableAddRow(dtTemp1, id, strLorryName, strLorryIdno, strPartyName, strPartyIdno, strValidFrom);
                    ViewState["dtnew"] = dtTemp1;
                    lblLorryError.Text = "";
                }
            }
            int max = Convert.ToInt32(dtTemp1.AsEnumerable()
            .Max(row => row["ID"]));
            hdnLorryMaxID.Value = Convert.ToString(max);
            grdPrtyAdd.DataSource = dtTemp1;
            grdPrtyAdd.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openPartyChangeModal();", true);
            ClearNewControls();
        }

        protected void lnkPartyChangeSave_OnClick(object sender, EventArgs e)
        {
            Int64 intValue = 0; 
            LorryMasterDAL obj = new LorryMasterDAL();
            DataTable dtDetail = (DataTable)ViewState["dtnew"];
            intValue = obj.InsertPartyChangeDetails(dtDetail);
         
          
            if (intValue > 0)
            {
                if (string.IsNullOrEmpty(hidlorryidno.Value) == false)
                {
                    ShowMessage("Record updated successfully.");
                }
                else
                {
                    ShowMessage("Record Inserted successfully.");
                }
                this.ClearControls();
                ddlPartyName.Enabled = true;
            }
        }
        protected void lnkSerialDetl_Click(object sender, EventArgs e)
        {
            int LorryId = Convert.ToInt32(Request.QueryString["LorryIdno"]);
            if ((string.IsNullOrEmpty(txtNumberOfTyres.Text) ? 0 : Convert.ToInt32(txtNumberOfTyres.Text)) > 0)
            {
                if (LorryId > 0)
                {
                    this.SelectStockforItemPurBill(LorryId, Convert.ToInt32(txtNumberOfTyres.Text));
                }
                lblmsg.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowStck()", true);
            }
            else
            {

            }
        }

        protected void lnkLorryType_Click(object sender, EventArgs e)
        {
            if (ddllorytype.SelectedValue == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClientLorry()", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('Please Select trailer in type.')", true);
                ddllorytype.Focus();
            }

        }

        protected void lnkTyreSerialNoSave_Click(object sender, EventArgs e)
        {
            bool flag = true;
            foreach (RepeaterItem item in rptstck.Items)
            {

                TextBox txtserialNo = (TextBox)item.FindControl("txtserialNo");
                if (txtserialNo.Text.Trim() == string.Empty)
                {
                    lblmsg.Text = "Serial No can't be left blank.";
                    txtserialNo.Focus();
                    flag = false;
                    break;
                }
            }

            string oldchasisno = string.Empty;
            for (int count = 0; count < rptstck.Items.Count; count++)
            {
                TextBox txtserialNo = (TextBox)rptstck.Items[count].FindControl("txtserialNo");
                for (int count1 = count + 1; count1 < rptstck.Items.Count; count1++)
                {
                    TextBox txtserialNo1 = (TextBox)rptstck.Items[count1].FindControl("txtserialNo");

                    if (txtserialNo.Text.Trim() == txtserialNo1.Text.Trim())
                    {
                        lblmsg.Text = "Serial No Already exists in list.";
                        lblmsg.CssClass = "redfont";
                        flag = false;
                        break;
                    }
                }
            }




            if (flag == true)
            {
                string msgAlreadyExists = string.Empty;
                string saveMsg = string.Empty;
                int LorryIdno = Convert.ToInt32(Request.QueryString["LorryIdno"]);


                //foreach (RepeaterItem item in rptstck.Items)
                //{
                //    TextBox txtserialNo = (TextBox)item.FindControl("txtserialNo");
                //    //HiddenField hidStckIdno = (HiddenField)item.FindControl("hidSerialIdno");
                //    LorryMasterDAL obj = new LorryMasterDAL();
                //    //if (string.IsNullOrEmpty(Convert.ToString(hidStckIdno.Value)))//for insert
                //    //{
                //        if (obj.CheckChasisForStck(txtserialNo.Text.Trim(), 0, "save") == true)
                //        {
                //            msgAlreadyExists += "Serial No: " + txtserialNo.Text + ",";
                //        }
                //    //}
                //    //else
                //    //{
                //    //    if (obj.CheckChasisForStck(txtserialNo.Text.Trim(), LorryIdno, "update") == true)
                //    //    {
                //    //        msgAlreadyExists += "Serial No: " + txtserialNo.Text + ",";
                //    //    }
                //    //}
                //}


                if (msgAlreadyExists != string.Empty)
                {
                    lblmsg.Text = msgAlreadyExists + "  already exists!";
                }
                else
                {
                    LorryMasterDAL obj1 = new LorryMasterDAL();
                    obj1.DeleteAll(LorryIdno);
                    obj1 = null;
                    foreach (RepeaterItem item in rptstck.Items)
                    {
                        LorryMasterDAL obj = new LorryMasterDAL();
                        TextBox txtserialNo = (TextBox)item.FindControl("txtserialNo");
                        //HiddenField hidSerialIdno = (HiddenField)item.FindControl("hidSerialIdno");
                        DropDownList ddlPostion = (DropDownList)item.FindControl("ddlPostion");
                        TextBox txtCompanys = (TextBox)item.FindControl("txtCompanys");
                        DropDownList ddlType = (DropDownList)item.FindControl("ddlType");
                        TextBox txtPurParty = (TextBox)item.FindControl("txtPurParty");
                        TextBox txtKms = (TextBox)item.FindControl("txtKms");

                        string serialNo = txtserialNo.Text;
                        Int32 PostionId = string.IsNullOrEmpty(ddlPostion.SelectedValue) == true ? 0 : Convert.ToInt32(ddlPostion.SelectedValue);
                        string CompName = string.IsNullOrEmpty(txtCompanys.Text) == true ? "" : Convert.ToString(txtCompanys.Text);
                        Int32 Type = string.IsNullOrEmpty(ddlType.SelectedValue) == true ? 0 : Convert.ToInt32(ddlType.SelectedValue);
                        string PurParty = string.IsNullOrEmpty(txtPurParty.Text) == true ? "" : Convert.ToString(txtPurParty.Text);
                        string Kms = string.IsNullOrEmpty(txtKms.Text) == true ? "" : Convert.ToString(txtKms.Text);

                        Int64 value = 0;
                        value = obj.InsertPurBillStock(LorryIdno, serialNo, PostionId, CompName, Type, PurParty, Kms);
                        if (value <= 0)
                        {
                            saveMsg += serialNo + ",";
                        }
                        //if (string.IsNullOrEmpty(Convert.ToString(hidSerialIdno.Value)) || Convert.ToInt32(hidSerialIdno.Value) <= 0)//for insert
                        //{
                        //    value = obj.InsertPurBillStock(LorryIdno, serialNo, PostionId, Convert.ToString(txtCompanys.Text), Convert.ToInt32(ddlType.SelectedValue), Convert.ToString(txtPurParty.Text), Convert.ToString(txtKms.Text));
                        //    if (value <= 0)
                        //    {
                        //        saveMsg += serialNo + ",";
                        //    }
                        //}
                        //else
                        //{
                        //    obj.DeleteAll(LorryIdno);
                        //    value = obj.InsertPurBillStock(LorryIdno, serialNo, PostionId, Convert.ToString(txtCompanys.Text), Convert.ToInt32(ddlType.SelectedValue), Convert.ToString(txtPurParty.Text), Convert.ToString(txtKms.Text));
                        //    if (value <= 0)
                        //    {
                        //        saveMsg += serialNo + ",";
                        //    }
                        //}

                        obj = null;

                    }
                    if (saveMsg == string.Empty)
                    {
                        lblmsg.Text = "Record saved."; ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", " HideStck()", true);
                        lblmsg.CssClass = "";
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblmsg.Text = "Records not saved Chasisno " + saveMsg;
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                    }

                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowStck()", true);

        }

        protected void lnkBtnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("LorryMaster.aspx");
        }

        protected void lnkBtnSave_Click(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            LorryMasterDAL objLorryMast = new LorryMasterDAL();
            Int64 intLorryIdno = 0;
            Int64 intDocIdNo = 0;
            DateTime? FitValidDat = null;
            DateTime? RCValidDat = null;
            DateTime? NatPermitDate = null;
            DateTime? AuthDate = null;
            DateTime? InsValidDate = null;
            DateTime? FinPeriodFrom = null;
            DateTime? FinPeriodTo = null;

            if (string.IsNullOrEmpty(txtFitValidDat.Text.Trim()) == false)
            {
                FitValidDat = Convert.ToDateTime(Classes.ApplicationFunction.mmddyyyy(txtFitValidDat.Text.Trim()));
            }
            else
            {
                FitValidDat = null;
            }

            if (string.IsNullOrEmpty(txtRCValidDat.Text.Trim()) == false)
            {
                RCValidDat = Convert.ToDateTime(Classes.ApplicationFunction.mmddyyyy(txtRCValidDat.Text.Trim()));
            }
            else
            {
                RCValidDat = null;
            }

            if (string.IsNullOrEmpty(txtNatPermitDate.Text.Trim()) == false)
            {
                NatPermitDate = Convert.ToDateTime(Classes.ApplicationFunction.mmddyyyy(txtNatPermitDate.Text.Trim()));
            }
            else
            {
                NatPermitDate = null;
            }

            if (string.IsNullOrEmpty(txtAuthDate.Text.Trim()) == false)
            {
                AuthDate = Convert.ToDateTime(Classes.ApplicationFunction.mmddyyyy(txtAuthDate.Text.Trim()));
            }
            else
            {
                AuthDate = null;
            }

            if (string.IsNullOrEmpty(txtValidDat.Text.Trim()) == false)
            {
                InsValidDate = Convert.ToDateTime(Classes.ApplicationFunction.mmddyyyy(txtValidDat.Text.Trim()));
            }
            else
            {
                InsValidDate = null;
            }

            if (string.IsNullOrEmpty(txtFinPeriodFrom.Text.Trim()) == false)
            {
                FinPeriodFrom = Convert.ToDateTime(Classes.ApplicationFunction.mmddyyyy(txtFinPeriodFrom.Text.Trim()));
            }
            else
            {
                FinPeriodFrom = null;
            }

            if (string.IsNullOrEmpty(txtFinPeriodTo.Text.Trim()) == false)
            {
                FinPeriodTo = Convert.ToDateTime(Classes.ApplicationFunction.mmddyyyy(txtFinPeriodTo.Text.Trim()));
            }
            else
            {
                FinPeriodTo = null;
            }

            if (ddllorytype.SelectedValue != "2")
            {
                txtTrollyLength.Text = "";
                txtTrollyheight.Text = "";
                txtTrollyWeight.Text = "";
                txtTyresNo.Text = "";
            }
            if (string.IsNullOrEmpty(txtInsurAmount.Text.Trim()) == false)
                ins = Convert.ToDouble(txtInsurAmount.Text.Trim());
            else
                ins = 0;

            if (string.IsNullOrEmpty(txtwarnFit.Text.Trim()) == false)
                Fit = Convert.ToInt32(txtwarnFit.Text.Trim());
            else
                Fit = 0;

            if (string.IsNullOrEmpty(txtInsWarn.Text.Trim()) == false)
                Insurnce = Convert.ToInt32(txtInsWarn.Text.Trim());
            else
                Insurnce = 0;

            if (string.IsNullOrEmpty(txtwarnRC.Text.Trim()) == false)
                RC = Convert.ToInt32(txtwarnRC.Text.Trim());
            else
                RC = 0;

            if (string.IsNullOrEmpty(txtNatPerWarn.Text.Trim()) == false)
                Nat = Convert.ToInt32(txtNatPerWarn.Text.Trim());
            else
                Nat = 0;

            if (string.IsNullOrEmpty(txtAuthWarn.Text.Trim()) == false)
                Auth = Convert.ToInt32(txtAuthWarn.Text.Trim());
            else
                Auth = 0;

            if (string.IsNullOrEmpty(hidlorryidno.Value) == true)
            {
                intLorryIdno = objLorryMast.InsertLorryMast(txtLorrymke.Text.Trim(), txtLorrymkeYear.Text.Trim(), txtOwnrNme.Text.Trim(), Convert.ToInt32(ddllorrytyp.SelectedValue), Convert.ToInt64(ddlPartyName.SelectedValue),
                             txtChasisNo.Text.Trim(), txtNumberOfTyres.Text, txtEngineNo.Text.Trim(), txtLorryNo.Text.Trim(),
                              txtPanNo.Text.Trim(),
                              Convert.ToBoolean(chkStatus.Checked), Convert.ToBoolean(ChkcommsnCal.Checked), Convert.ToInt32(ddllorytype.SelectedValue), Convert.ToInt32(Convert.ToString(ddlPANType.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlPANType.SelectedValue)), FitValidDat, RCValidDat, NatPermitDate, AuthDate, txtTrollyLength.Text.Trim(), txtTrollyheight.Text.Trim(), txtTrollyWeight.Text.Trim(), txtTyresNo.Text.Trim(), txtInsuranceNo.Text.Trim(), drpInsuCompName.SelectedValue, InsValidDate, txtFinancerName.Text.Trim(), txtFinancerAmount.Text.Trim(), txtEMIAmount.Text.Trim(), txtTotalInstallment.Text.Trim(), FinPeriodFrom, FinPeriodTo, Convert.ToBoolean(chlCalOnDF.Checked), Convert.ToInt64(ddldriverName.SelectedValue), ins, Fit, Insurnce, RC, Nat, Auth, empIdno, Convert.ToBoolean(chkLowRate.Checked), txtOwneradd.Text.Trim(), txtMobileNo.Text.Trim());
                //DOC Holder
                dtTemp = CreateDt();
                dtTemp = (DataTable)ViewState["DocumentHolderTable"];
                intDocIdNo = objLorryMast.InsertDocHolderDetails(dtTemp, intLorryIdno, empIdno);

            }
            else
            {
                intLorryIdno = objLorryMast.UpdateLorryMast(txtLorrymke.Text.Trim(), txtLorrymkeYear.Text.Trim(), txtOwnrNme.Text.Trim(), Convert.ToInt32(ddllorrytyp.SelectedValue), Convert.ToInt64(ddlPartyName.SelectedValue),
                             txtChasisNo.Text.Trim(), txtNumberOfTyres.Text, txtEngineNo.Text.Trim(), txtLorryNo.Text.Trim(),
                              txtPanNo.Text.Trim(),
                             Convert.ToBoolean(chkStatus.Checked), Convert.ToBoolean(ChkcommsnCal.Checked), Convert.ToInt32(ddllorytype.SelectedValue), Convert.ToInt64(hidlorryidno.Value), Convert.ToInt32(Convert.ToString(ddlPANType.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlPANType.SelectedValue)), FitValidDat, RCValidDat, NatPermitDate, AuthDate, txtTrollyLength.Text.Trim(), txtTrollyheight.Text.Trim(), txtTrollyWeight.Text.Trim(), txtTyresNo.Text.Trim(), txtInsuranceNo.Text.Trim(), drpInsuCompName.SelectedValue, InsValidDate, txtFinancerName.Text.Trim(), txtFinancerAmount.Text.Trim(), txtEMIAmount.Text.Trim(), txtTotalInstallment.Text.Trim(), FinPeriodFrom, FinPeriodTo, Convert.ToBoolean(chlCalOnDF.Checked), Convert.ToInt64(ddldriverName.SelectedValue), ins, Fit, Insurnce, RC, Nat, Auth, empIdno, Convert.ToBoolean(chkLowRate.Checked), txtOwneradd.Text.Trim(), txtMobileNo.Text.Trim());
                //DOC Holder
                dtTemp = CreateDt();
                dtTemp = (DataTable)ViewState["DocumentHolderTable"];
                intDocIdNo = objLorryMast.InsertDocHolderDetails(dtTemp, intLorryIdno, empIdno);
            }
            objLorryMast = null;
            if (intLorryIdno > 0)
            {
                if (string.IsNullOrEmpty(hidlorryidno.Value) == false)
                {
                    strMsg = "Record updated successfully.";
                }
                else
                {
                    strMsg = "Record saved successfully.";
                }
                this.ClearControls();
            }
            else if (intLorryIdno < 0)
            {
                strMsg = "Record already exists.";
            }
            else
            {
                if (string.IsNullOrEmpty(hidlorryidno.Value) == false)
                {
                    strMsg = "Record not updated.";
                }
                else
                {
                    strMsg = "Record not saved.";
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            ddllorrytyp.Focus();
        }

        protected void lnkBtnCancel_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Request.QueryString["LorryIdno"]) > 0)
            {
                this.Populate(Convert.ToInt32(hidlorryidno.Value));
            }
            else
            {
                this.ClearControls();
            }
        }

        protected void lnkSerialDetl_Click1(object sender, EventArgs e)
        {
            int LorryId = Convert.ToInt32(Request.QueryString["LorryIdno"]);
            if ((string.IsNullOrEmpty(txtNumberOfTyres.Text) ? 0 : Convert.ToInt32(txtNumberOfTyres.Text)) > 0)
            {
                if (LorryId > 0)
                {
                    this.SelectStockforItemPurBill(LorryId, Convert.ToInt32(txtNumberOfTyres.Text));
                }
                lblmsg.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openTyreModal();", true);
            }
            else
            {

            }
        }

        protected void lnkTyreSerialNoSave_Click1(object sender, EventArgs e)
        {
            bool flag = true;
            foreach (RepeaterItem item in rptstck.Items)
            {

                TextBox txtserialNo = (TextBox)item.FindControl("txtserialNo");
                if (txtserialNo.Text.Trim() == string.Empty)
                {
                    lblmsg.Text = "Serial No can't be left blank.";
                    txtserialNo.Focus();
                    flag = false;
                    break;
                }
            }

            string oldchasisno = string.Empty;
            for (int count = 0; count < rptstck.Items.Count; count++)
            {
                TextBox txtserialNo = (TextBox)rptstck.Items[count].FindControl("txtserialNo");
                for (int count1 = count + 1; count1 < rptstck.Items.Count; count1++)
                {
                    TextBox txtserialNo1 = (TextBox)rptstck.Items[count1].FindControl("txtserialNo");

                    if (txtserialNo.Text.Trim() == txtserialNo1.Text.Trim())
                    {
                        lblmsg.Text = "Serial No Already exists in list.";
                        lblmsg.CssClass = "redfont";
                        flag = false;
                        break;
                    }
                }
            }




            if (flag == true)
            {
                string msgAlreadyExists = string.Empty;
                string saveMsg = string.Empty;
                int LorryIdno = Convert.ToInt32(Request.QueryString["LorryIdno"]);


                //foreach (RepeaterItem item in rptstck.Items)
                //{
                //    TextBox txtserialNo = (TextBox)item.FindControl("txtserialNo");
                //    //HiddenField hidStckIdno = (HiddenField)item.FindControl("hidSerialIdno");
                //    LorryMasterDAL obj = new LorryMasterDAL();
                //    //if (string.IsNullOrEmpty(Convert.ToString(hidStckIdno.Value)))//for insert
                //    //{
                //        if (obj.CheckChasisForStck(txtserialNo.Text.Trim(), 0, "save") == true)
                //        {
                //            msgAlreadyExists += "Serial No: " + txtserialNo.Text + ",";
                //        }
                //    //}
                //    //else
                //    //{
                //    //    if (obj.CheckChasisForStck(txtserialNo.Text.Trim(), LorryIdno, "update") == true)
                //    //    {
                //    //        msgAlreadyExists += "Serial No: " + txtserialNo.Text + ",";
                //    //    }
                //    //}
                //}


                if (msgAlreadyExists != string.Empty)
                {
                    lblmsg.Text = msgAlreadyExists + "  already exists!";
                }
                else
                {
                    LorryMasterDAL obj1 = new LorryMasterDAL();
                    obj1.DeleteAll(LorryIdno);
                    obj1 = null;
                    foreach (RepeaterItem item in rptstck.Items)
                    {
                        LorryMasterDAL obj = new LorryMasterDAL();
                        TextBox txtserialNo = (TextBox)item.FindControl("txtserialNo");
                        //HiddenField hidSerialIdno = (HiddenField)item.FindControl("hidSerialIdno");
                        DropDownList ddlPostion = (DropDownList)item.FindControl("ddlPostion");
                        TextBox txtCompanys = (TextBox)item.FindControl("txtCompanys");
                        DropDownList ddlType = (DropDownList)item.FindControl("ddlType");
                        TextBox txtPurParty = (TextBox)item.FindControl("txtPurParty");
                        TextBox txtKms = (TextBox)item.FindControl("txtKms");

                        string serialNo = txtserialNo.Text;
                        Int32 PostionId = string.IsNullOrEmpty(ddlPostion.SelectedValue) == true ? 0 : Convert.ToInt32(ddlPostion.SelectedValue);
                        string CompName = string.IsNullOrEmpty(txtCompanys.Text) == true ? "" : Convert.ToString(txtCompanys.Text);
                        Int32 Type = string.IsNullOrEmpty(ddlType.SelectedValue) == true ? 0 : Convert.ToInt32(ddlType.SelectedValue);
                        string PurParty = string.IsNullOrEmpty(txtPurParty.Text) == true ? "" : Convert.ToString(txtPurParty.Text);
                        string Kms = string.IsNullOrEmpty(txtKms.Text) == true ? "" : Convert.ToString(txtKms.Text);

                        Int64 value = 0;
                        value = obj.InsertPurBillStock(LorryIdno, serialNo, PostionId, CompName, Type, PurParty, Kms);
                        if (value <= 0)
                        {
                            saveMsg += serialNo + ",";
                        }
                        //if (string.IsNullOrEmpty(Convert.ToString(hidSerialIdno.Value)) || Convert.ToInt32(hidSerialIdno.Value) <= 0)//for insert
                        //{
                        //    value = obj.InsertPurBillStock(LorryIdno, serialNo, PostionId, Convert.ToString(txtCompanys.Text), Convert.ToInt32(ddlType.SelectedValue), Convert.ToString(txtPurParty.Text), Convert.ToString(txtKms.Text));
                        //    if (value <= 0)
                        //    {
                        //        saveMsg += serialNo + ",";
                        //    }
                        //}
                        //else
                        //{
                        //    obj.DeleteAll(LorryIdno);
                        //    value = obj.InsertPurBillStock(LorryIdno, serialNo, PostionId, Convert.ToString(txtCompanys.Text), Convert.ToInt32(ddlType.SelectedValue), Convert.ToString(txtPurParty.Text), Convert.ToString(txtKms.Text));
                        //    if (value <= 0)
                        //    {
                        //        saveMsg += serialNo + ",";
                        //    }
                        //}

                        obj = null;

                    }
                    if (saveMsg == string.Empty)
                    {
                        //lblmsg.Text = "Record saved."; ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", " HideStck()", true);
                        lblmsg.CssClass = "";
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblmsg.Text = "Records not saved Chasisno " + saveMsg;
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                    }

                }
            }
        }

        protected void lnkInsurance_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "ShowInsourance();", true);
            drpInsuCompName.Focus();
        }

        protected void lnkFinanceDetl_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "ShowFinancer();", true);
            txtFinancerAmount.Focus();

        }

        protected void lnkBtnCreateDriver_Click(object sender, EventArgs e)
        {
            if (ddllorrytyp.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "ShowDriverCreation();", true);
                txtDriverName.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "ShowHireDriverCreation();", true);
                txtDriverHire.Focus();
            }

            
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindCitywithStateId(Convert.ToInt32(ddlState.SelectedValue));
            ddlCity.Focus();
            if (e != null)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "ShowDriverCreation();", true);
        }

        private void BindCitywithStateId(int stateIdno)
        {

            ddlCity.DataSource = null;
            LedgerAccountDAL accountDAL = new LedgerAccountDAL();
            var lst = accountDAL.BindCitywithStateId(stateIdno);
            accountDAL = null;
            ddlCity.DataSource = lst;
            ddlCity.DataTextField = "City_Name";
            ddlCity.DataValueField = "City_Idno";
            ddlCity.DataBind();

            ddlCity.Items.Insert(0, new ListItem("--Select City--", "0"));
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
            // ddlState.SelectedValue = "28";
            ddlState_SelectedIndexChanged(null, null);
        }

        private void ClearNewControls()
        {
            //ddlLorryNo.SelectedValue = "0";
            ddlPartyNameChange.SelectedValue = "0";
            txtValidFromDt1.Text = System.DateTime.Now.Date.ToString("dd-MM-yyyy");
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

        private DataTable CreateDtNew()
        {
            DataTable dttemp2 = ApplicationFunction.CreateTable("tbl",
                "Id", "String",
                "LorryNo", "String",
                "LorryIdNo", "String",
                "AcntName", "String",
                "AcntIdno", "String",
                "ValidFrom", "String"
                );
            return dttemp2;
        }

        protected void lnkBtnSaveDriver_Click(object sender, EventArgs e)
        {
            LorryMasterDAL ObJDAL = new LorryMasterDAL();
            Int64 Result = ObJDAL.SavDriver(txtDriverName.Text, Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt64(ddlState.SelectedValue), Convert.ToInt64(ddlCity.SelectedValue));
            string strMsg = "";
            if (Result > 0)
            {
                strMsg = "Driver(Own) Saved successfully.";
                txtDriverName.Text = "";
                ddlState.SelectedIndex = 0;
                ddlCity.SelectedIndex = 0;
            }
            else if (Result < 0)
            {
                strMsg = "Driver(Own) already exists.";
            }
            else
            {
                strMsg = "Driver(Own) not saved.";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);

        }

        protected void lnkSaveDriverHire_Click(object sender, EventArgs e)
        {
            LorryMasterDAL ObJDAL = new LorryMasterDAL();
            Int64 Result = ObJDAL.SavDriverHire(txtDriverHire.Text,Convert.ToString(txtLicenseNo.Text));
            string strMsg = "";
            if (Result > 0)
            {
                strMsg = "Driver(Hire) Saved successfully.";
                txtDriverHire.Text = "";
                txtLicenseNo.Text = "";
            }
            else if (Result < 0)
            {
                strMsg = "Driver(Hire) already exists.";
            }
            else
            {
                strMsg = "Driver(Hire) not saved.";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);

        }

        protected void lnkBtnDriverRef_Click(object sender, EventArgs e)
        {
            ddllorrytyp_SelectedIndexChanged(null, null);
        }
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + msg + "')", true);
        }

        protected void lnkPartyName_Click(object sender, EventArgs e)
        {
            LorryMasterDAL objLorrMast = new LorryMasterDAL();
            if (Convert.ToInt32(ddllorrytyp.SelectedValue) == 0)
            {
                var LorrMast = objLorrMast.SelectPartyNameOwn();
                ddlPartyName.DataSource = LorrMast;
                ddlPartyName.DataTextField = "Acnt_Name";
                ddlPartyName.DataValueField = "Acnt_Idno";
                ddlPartyName.DataBind();
                ddlPartyName.Items.Insert(0, new ListItem("< Choose Party Name >", "0"));
            }
        }

        public void lnkUpdateLorryPAN_Click(object sender, EventArgs e)
        {
            if (txtPanNo.Text == String.Empty)
            {
                ShowMessageErr("Please enter PAN number.");
                return;
            }
            int Party_Idno = 0;
            if (ddlPartyName.SelectedValue != "" && ddlPartyName.SelectedValue != "0" && Convert.ToInt32(hidPartyIdno.Value == "" ? "0" : hidPartyIdno.Value) > 0)
            {
                Party_Idno = Convert.ToInt32(hidPartyIdno.Value);
                LorryMasterDAL objLorrtMast = new LorryMasterDAL();
                int count = objLorrtMast.UpdateLorryByParty(Party_Idno, txtPanNo.Text);
                if (count > 0)
                {
                    ShowMessage(count + " lorrries have been updated with new PAN number.");
                }
                else 
                {
                    ShowMessageErr("Something went wrong, please try again later.");
                }
            }
        }
    }
}
