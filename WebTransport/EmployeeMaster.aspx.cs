using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using WebTransport.Account;
using System.IO;
using System.Transactions;
using System.Data;


namespace WebTransport
{
    public partial class EmployeeMaster : Pagebase
    {
        #region Variables declaration...
        private int intFormId = 16; Int64 intCompId = 0;
        #endregion

        #region PageLoad Event...
        protected void Page_Load(object sender, EventArgs e)
        {
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
                if (base.View == false)
                {
                    lblViewList.Visible = true;
                }
                txtEmpName.Attributes.Add("onkeypress", "return allowOnlyAlphabet(event);");
                txtFatherName.Attributes.Add("onkeypress", "return allowAlphabetAndNumerN_Spaceallow(event);");
                txtPinCode.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtPhone.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtMobile.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtDOB.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDOJ.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDOR.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtUName.Attributes.Add("onkeypress", "return allowOnlyAlphabet(event);");
                txtUName.Attributes.Add("autocomplete", "off");
                this.BindDesignation();
                txtDOB.Text = DateTime.Now.ToString("dd-MM-yyyy");
                txtDOJ.Text = DateTime.Now.ToString("dd-MM-yyyy");
                txtDOR.Text = DateTime.Now.ToString("dd-MM-yyyy");
                this.BindStateDDL();
                this.BindMultpleFromCity();
                txtUName.Text = "";

                if (string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Emp_Idno"])) == false)
                {
                    Int64 intEmpidno = Convert.ToInt64(Request.QueryString["Emp_Idno"]);
                    Populate(intEmpidno);
                    lnkbtnNew.Visible = true;
                }
                else
                {
                    lnkbtnNew.Visible = false;
                }
                hidmindate.Value = "01-01-1940";
                hidmaxdate.Value = DateTime.Now.ToString("dd-MM-YYYY");
            }
        }
        #endregion

        #region Miscellaneous Events...
        private void ClearCntrl()
        {
            txtEmpName.Text = string.Empty;
            txtFatherName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtDOB.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtDOJ.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtDOR.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtPinCode.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtDOB.Text = string.Empty;
            txtDOJ.Text = string.Empty;
            txtDOR.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtUName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            // txtPassword.Text = " ";
            txtRemarks.Text = string.Empty;
            ddlState.SelectedIndex = 0;
            ddlCity.SelectedIndex = 0;
            ddlDesignation.SelectedIndex = 0;
            chkIsActive.Checked = true;

            hidEmpIdno.Value = string.Empty;
            imgEmp.ImageUrl = "~/img/placeholder.png";
            txtEmpName.Focus();
            //chklistFromcity
            for (int i = 0; i < chklistFromcity.Items.Count; i++)
            {
                chklistFromcity.Items[i].Selected = false;
            }
        }

        private Int64 InsertIntoUsers()
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            EmployeeMasterDAL objEmployeeMasterDAL = new EmployeeMasterDAL();
            Int64 intEmpIdno = 0; string Gndr = string.Empty;
            string strLogoPath = "";
            if (imgEmp.ImageUrl != "")
            {
                string filename = Path.GetFileName(fuPicture.FileName);
                strLogoPath = VirtualPathUtility.ToAbsolute(imgEmp.ImageUrl);
            }
            if (RdoBtnMale.Checked == true)
            {
                Gndr = "M";
            }
            else if (RdoBtnFemale.Checked == true)
            {
                Gndr = "F";
            }
            string strName = txtEmpName.Text.Trim();
            string strFName = txtFatherName.Text.Trim();
            DateTime? dtDOB = null;
            DateTime? dtDOJ = null;
            DateTime? dtDOR = null;
            if (string.IsNullOrEmpty(txtDOB.Text.Trim()) == false)
            {
                dtDOB = Convert.ToDateTime(Classes.ApplicationFunction.mmddyyyy(txtDOB.Text.Trim()));
            }
            else
            {
                dtDOB = null;
            }
            if (string.IsNullOrEmpty(txtDOJ.Text.Trim()) == false)
            {
                dtDOJ = Convert.ToDateTime(Classes.ApplicationFunction.mmddyyyy(txtDOJ.Text.Trim()));
            }
            else
            {
                dtDOJ = null;
            }
            if (string.IsNullOrEmpty(txtDOR.Text.Trim()) == false)
            {
                dtDOR = Convert.ToDateTime(Classes.ApplicationFunction.mmddyyyy(txtDOR.Text.Trim()));
            }
            else
            {
                dtDOR = null;
            }
            string strPhone = txtPhone.Text.Trim();
            string strMobile = txtMobile.Text.Trim();
            string strEmail = txtEmail.Text.Trim();
            string strAddress = txtAddress.Text.Trim();
            Int64 intCityIdno = string.IsNullOrEmpty(Convert.ToString(ddlCity.SelectedValue)) ? 0 : Convert.ToInt64(ddlCity.SelectedValue.Trim());
            Int64 intStateIdno = string.IsNullOrEmpty(Convert.ToString(ddlState.SelectedValue)) ? 0 : Convert.ToInt64(ddlState.SelectedValue.Trim());
            Int64 intDesigIdno = string.IsNullOrEmpty(Convert.ToString(ddlDesignation.SelectedValue)) ? 0 : Convert.ToInt64(ddlDesignation.SelectedValue.Trim());
            string strPinCode = txtPinCode.Text.Trim();
            string strRemarks = txtRemarks.Text.Trim();
            bool bStatus = Convert.ToBoolean((chkIsActive.Checked == true) ? 1 : 0);
            Int32 intYearIdno = base.intYearIdno; bool IntComputer = Convert.ToBoolean((ddlcomputerstatus.SelectedIndex) == 0 ? 1 : 0);
            Int32 intCompIdno = 1;
            string strUName = txtUName.Text.Trim();

            string strPassword = WebTransport.Classes.EncryptDecryptPass.encryptPassword(txtPassword.Text.Trim());
            intEmpIdno = objEmployeeMasterDAL.Insert(strName, strFName, dtDOB, dtDOJ, dtDOR, strPhone, strMobile, strEmail, strUName, strPassword, strAddress,
                                                    intCityIdno, intStateIdno, intDesigIdno, strPinCode, strLogoPath, strRemarks, bStatus, intYearIdno, intCompIdno, Gndr, IntComputer, empIdno, (string.IsNullOrEmpty(Session["CompId"].ToString()) ? 0 : Convert.ToInt32(Session["CompId"])));
            return intEmpIdno;
        }

        private Int64 UpdateUsers(Int64 intEmpIdno)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            EmployeeMasterDAL objEmployeeMasterDAL = new EmployeeMasterDAL();
            string strFileName = Convert.ToString(Path.GetFileName(fuPicture.FileName));
            string strName = txtEmpName.Text.Trim();
            string strFName = txtFatherName.Text.Trim();
            DateTime? dtDOB = null; string Gndr = string.Empty;
            if (RdoBtnMale.Checked == true)
            {
                Gndr = "M";
            }
            else if (RdoBtnFemale.Checked == true)
            {
                Gndr = "F";
            }
            if (string.IsNullOrEmpty(txtDOB.Text.Trim()) == false)
            {
                dtDOB = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDOB.Text.Trim()));
            }
            DateTime? dtDOJ = null;
            if (string.IsNullOrEmpty(txtDOJ.Text.Trim()) == false)
            {
                dtDOJ = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDOJ.Text.Trim()));
            }
            DateTime? dtDOR = null;
            if (string.IsNullOrEmpty(txtDOR.Text.Trim()) == false)
            {
                dtDOR = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDOR.Text.Trim()));
            }
            string strPhone = txtPhone.Text.Trim();
            string strMobile = txtMobile.Text.Trim();
            string strEmail = txtEmail.Text.Trim();
            string strAddress = txtAddress.Text.Trim();
            Int64 intCityIdno = string.IsNullOrEmpty(Convert.ToString(ddlCity.SelectedValue)) ? 0 : Convert.ToInt64(ddlCity.SelectedValue.Trim());
            Int64 intStateIdno = string.IsNullOrEmpty(Convert.ToString(ddlState.SelectedValue)) ? 0 : Convert.ToInt64(ddlState.SelectedValue.Trim());
            Int64 intDesigIdno = string.IsNullOrEmpty(Convert.ToString(ddlDesignation.SelectedValue)) ? 0 : Convert.ToInt64(ddlDesignation.SelectedValue.Trim());
            string strPinCode = txtPinCode.Text.Trim();
            string strLogoPath = "";
            if (imgEmp.ImageUrl != "")
            {
                string filename = Path.GetFileName(fuPicture.FileName);
                strLogoPath = VirtualPathUtility.ToAbsolute(imgEmp.ImageUrl);
            }
            string strRemarks = txtRemarks.Text.Trim();
            bool bStatus = Convert.ToBoolean((chkIsActive.Checked == true) ? 1 : 0);
            Int32 intCompIdno = 1;
            string strLoginname = txtUName.Text.Trim(); bool bComputer = Convert.ToBoolean((ddlcomputerstatus.SelectedIndex) == 0 ? 1 : 0);
            string strPassword = WebTransport.Classes.EncryptDecryptPass.encryptPassword(txtPassword.Text.Trim());
            intEmpIdno = objEmployeeMasterDAL.Update(intEmpIdno, strName, strFName, dtDOB, dtDOJ, dtDOR, strPhone, strMobile, strEmail, strLoginname, strPassword,
                                                      strAddress, intCityIdno, intStateIdno, intDesigIdno, strPinCode, strLogoPath, strRemarks, bStatus, intCompIdno, Gndr, bComputer, empIdno, (string.IsNullOrEmpty(Session["CompId"].ToString()) ? 0 : Convert.ToInt32(Session["CompId"])));
            return intEmpIdno;
        }

        private void Populate(Int64 intEmpIdno)
        {
            PopulateMultpleFromCity(Convert.ToInt32(intEmpIdno));
            EmployeeMasterDAL objEmployeeMasterDAL = new EmployeeMasterDAL();
            var objEmpMaster = objEmployeeMasterDAL.SelectById(intEmpIdno);
            var lst1 = objEmployeeMasterDAL.SelectFromCityMultiple(intEmpIdno);
            objEmployeeMasterDAL = null;
            if (objEmpMaster != null)
            {
                string gender = "";
                gender = Convert.ToString(DataBinder.Eval(objEmpMaster[0], "Gender"));
                if (gender == "F")
                {
                    RdoBtnFemale.Checked = true;
                }
                else
                {
                    RdoBtnMale.Checked = true;
                }
                hidEmpIdno.Value = Convert.ToString(DataBinder.Eval(objEmpMaster[0], "EmpIdno"));
                txtEmpName.Text = Convert.ToString(DataBinder.Eval(objEmpMaster[0], "EmpName"));
                txtFatherName.Text = Convert.ToString(DataBinder.Eval(objEmpMaster[0], "FName"));
                txtDOB.Text = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(objEmpMaster[0], "DOB"))) ? "" : Convert.ToDateTime(DataBinder.Eval(objEmpMaster[0], "DOB")).ToString("dd-MM-yyyy");
                txtDOJ.Text = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(objEmpMaster[0], "DOJ"))) ? "" : Convert.ToDateTime(DataBinder.Eval(objEmpMaster[0], "DOJ")).ToString("dd-MM-yyyy");
                txtDOR.Text = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(objEmpMaster[0], "DOR"))) ? "" : Convert.ToDateTime(DataBinder.Eval(objEmpMaster[0], "DOR")).ToString("dd-MM-yyyy");
                txtPhone.Text = Convert.ToString(DataBinder.Eval(objEmpMaster[0], "Phone"));
                txtMobile.Text = Convert.ToString(DataBinder.Eval(objEmpMaster[0], "Mobile"));
                txtEmail.Text = Convert.ToString(DataBinder.Eval(objEmpMaster[0], "EMAIL"));
                txtAddress.Text = Convert.ToString(DataBinder.Eval(objEmpMaster[0], "Address"));
                ddlState.SelectedValue = Convert.ToString(DataBinder.Eval(objEmpMaster[0], "State"));
                ddlState_SelectedIndexChanged(null, null);
                ddlCity.SelectedValue = Convert.ToString(DataBinder.Eval(objEmpMaster[0], "City"));
                ddlDesignation.SelectedValue = Convert.ToString(DataBinder.Eval(objEmpMaster[0], "Desig"));
                txtPinCode.Text = Convert.ToString(DataBinder.Eval(objEmpMaster[0], "PinCode"));
                txtRemarks.Text = Convert.ToString(DataBinder.Eval(objEmpMaster[0], "Remark"));
                bool bStatus = Convert.ToBoolean(DataBinder.Eval(objEmpMaster[0], "Status"));
                if (Convert.ToString(DataBinder.Eval(objEmpMaster[0], "Computer_User")) == "False")
                {
                    ddlcomputerstatus.SelectedValue = "1";
                }
                else
                {
                    ddlcomputerstatus.SelectedValue = "0";
                }
                chkIsActive.Checked = bStatus;
                txtUName.Text = Convert.ToString(DataBinder.Eval(objEmpMaster[0], "UserName"));
                string pwd1 = WebTransport.Classes.EncryptDecryptPass.decryptPassword(Convert.ToString(DataBinder.Eval(objEmpMaster[0], "Password")));
                txtPassword.Attributes.Add("value", pwd1);
                imgEmp.ImageUrl = "~" + Convert.ToString(DataBinder.Eval(objEmpMaster[0], "Photo")).Trim();

                if (lst1.Count > 0)
                {
                    for (int i = 0; i < chklistFromcity.Items.Count; i++)
                    {
                        for (int j = 0; j < lst1.Count; j++)
                        {

                            if (Convert.ToInt64(chklistFromcity.Items[i].Value) == lst1[j].FrmCity_Idno)
                            {
                                chklistFromcity.Items[i].Selected = true;
                            }

                        }
                    }
                }
            }
        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        #endregion

        #region Button Event ...
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            EmployeeMasterDAL objEmployeeMaster = new EmployeeMasterDAL();
            string strMsg = string.Empty;
            Int64 intEmpIdno = 0, intUserRghtsIdno = 0; //, intEmpWHStoreIdno = 0;
            tblDesignation objDesigMast = new tblDesignation();
            objDesigMast = objEmployeeMaster.SelectDesigRghtsStatus(Convert.ToInt32(ddlDesignation.SelectedValue));
            bool? bDesigRghtsStatus = false;
            bDesigRghtsStatus = objDesigMast.UserRights_Status;
            int rowcount = 0;
            if (bDesigRghtsStatus == false)
            {
                ShowMessageErr("Rights for this designation has not assigned. Kindly assign rights and then save!");
                ddlDesignation.Focus(); 
                return;
            }
            string strEmail = txtEmail.Text.Trim();
            string strPassword = WebTransport.Classes.EncryptDecryptPass.encryptPassword(txtPassword.Text.Trim());
            bool BSeXIST = false;
            bool EmailExist;
            if (string.IsNullOrEmpty(hidEmpIdno.Value) == true)
            {
                BSeXIST = objEmployeeMaster.SelectExistInMasterDB(strEmail, strPassword, 0);
            }
            else
            {
                BSeXIST = objEmployeeMaster.SelectExistInMasterDB(strEmail, strPassword, Convert.ToInt32(hidEmpIdno.Value.Trim()));
            }
            EmailExist = objEmployeeMaster.EmailExist(strEmail, 1, Convert.ToInt32(string.IsNullOrEmpty(hidEmpIdno.Value.Trim()) ? "0" : hidEmpIdno.Value.Trim()));
            if ((BSeXIST == true) || (EmailExist == true))
            {
                ShowMessageErr("Email Id And Password Already Exist Please Enter Different Email And Password!");
                txtEmail.Focus(); 
                return;
            }
            for (int i = 0; i <= chklistFromcity.Items.Count - 1; i++)
            {
                if (chklistFromcity.Items[i].Selected == true)
                {
                    rowcount++;
                }
            }

            if (chklistFromcity.Items.Count == 0)
            {
                ShowMessageErr("Please define Location in City Master!"); 
                return;
            }

            if (rowcount == 0)
            {
                ShowMessageErr("Please select From City!");
                chklistFromcity.Focus(); 
                return;
            }


            using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
            {
                if (string.IsNullOrEmpty(hidEmpIdno.Value) == true)
                {
                    intEmpIdno = InsertIntoUsers();
                    if (intEmpIdno > 0)
                    {
                        objEmployeeMaster.InsertMultpleFromCity(intEmpIdno, chklistFromcity);
                    }
                }
                else
                {
                    intEmpIdno = UpdateUsers(Convert.ToInt64(hidEmpIdno.Value.Trim()));
                    if (intEmpIdno > 0)
                    {
                        objEmployeeMaster.UpdateMultpleFromCity(intEmpIdno, chklistFromcity);
                    }
                }
                if (intEmpIdno > 0)
                {
                    if (string.IsNullOrEmpty(hidEmpIdno.Value) == true)
                    {

                        intUserRghtsIdno = objEmployeeMaster.InsertIntoUserRights(intEmpIdno, Convert.ToInt32(ddlDesignation.SelectedValue), empIdno); 
                        if (intEmpIdno > 0 && intUserRghtsIdno > 0)
                        {
                            tScope.Complete(); 
                           ShowMessage("Record saved successfully.");
                            this.ClearCntrl();
                            lnkbtnNew.Visible = false;
                        }
                        else
                        {
                           ShowMessageErr("Record not saved.");
                            txtEmpName.Focus();
                        }
                    }
                    else
                    {
                        if (intEmpIdno > 0)
                        {
                            tScope.Complete();
                            ShowMessage("Record updated successfully.");
                            this.ClearCntrl();
                            lnkbtnNew.Visible = false;
                        }
                    }
                }
                else if (intEmpIdno == -1)
                {
                    ShowMessageErr("Record already exists.");
                    txtEmpName.Focus();
                }
                else
                {
                    ShowMessageErr("Record not saved.");
                    txtEmpName.Focus();
                }
            } 
            txtEmpName.Focus();
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidEmpIdno.Value) == true)
            {
                this.ClearCntrl();
            }
            else
            {
                this.Populate(Convert.ToInt64(hidEmpIdno.Value));
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {

                string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg" };

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
                    System.Drawing.Image img = System.Drawing.Image.FromStream(fuPicture.PostedFile.InputStream);
                    int height = img.Height;
                    int width = img.Width;
                    decimal size = Math.Round(((decimal)fuPicture.PostedFile.ContentLength / (decimal)1024), 2);
                    if (size > 100 || size < 20)
                    {
                        ShowMessageErr("File size must not exceed 100 KB and not less then 20 KB."); 
                        imgEmp.ImageUrl = "";
                    }
                    else if (height > 300 || width > 300)
                    {
                        ShowMessageErr("Height and Width must not exceed 300px."); 
                        imgEmp.ImageUrl = "";

                    }
                    else
                    {
                        if (fuPicture.HasFile)
                        {
                            if (fuPicture.PostedFile.ContentLength < 5024000)
                            {
                                string filename = Path.GetFileName(fuPicture.FileName);
                                fuPicture.SaveAs(Server.MapPath("~/Images/") + filename);
                                string filePath = "~/Images/" + filename;
                                imgEmp.ImageUrl = filePath;
                            }
                        }
                    }

                }

            }
            catch
            {
            }
        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeMaster.aspx");
        }
        #endregion

        #region Bind Event ...

        private void BindStateDDL()
        {
            BindDropdownDAL objClsBindDropDown = new BindDropdownDAL();
            var lst = objClsBindDropDown.BindState();
            ddlState.DataSource = lst;
            ddlState.DataTextField = "State_Name";
            ddlState.DataValueField = "State_Idno";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlCity.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void BindCityDDL(Int64 intStateIdno)
        {
            BindDropdownDAL objClsBindDropDown = new BindDropdownDAL();
            var lst = objClsBindDropDown.BindCity(intStateIdno);
            ddlCity.DataSource = lst;
            ddlCity.DataTextField = "City_Name";
            ddlCity.DataValueField = "City_Idno";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void BindDesignation()
        {
            WebTransport.DAL.DesigRightsDAL objclsDesigRightsDAL = new WebTransport.DAL.DesigRightsDAL();
            var objDesignRights = objclsDesigRightsDAL.SelectDesignation();
            objclsDesigRightsDAL = null;
            ddlDesignation.DataSource = objDesignRights;
            ddlDesignation.DataTextField = "Desig_Name";
            ddlDesignation.DataValueField = "Desig_Idno";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void BindMultpleFromCity()
        {
            EmployeeMasterDAL obj = new EmployeeMasterDAL();
            var ToCity = obj.BindToCity();
            obj = null;
            chklistFromcity.DataSource = ToCity;
            chklistFromcity.DataTextField = "city_name";
            chklistFromcity.DataValueField = "City_Idno";
            chklistFromcity.DataBind();
        }

        private void PopulateMultpleFromCity(int UserIdno)
        {
            EmployeeMasterDAL obj = new EmployeeMasterDAL();
            DataSet Ds = obj.PopulateFromCityMultiple(UserIdno, ApplicationFunction.ConnectionString());
            chklistFromcity.DataSource = null;
            chklistFromcity.DataSource = Ds.Tables[0];
            chklistFromcity.DataTextField = "city_name";
            chklistFromcity.DataValueField = "City_Idno";
            chklistFromcity.DataBind();

        }
        #endregion

        #region Control Events...
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCityDDL(Convert.ToInt64(ddlState.SelectedValue));
            ddlCity.Focus();
        }

        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            string msg = "";
            EmployeeMasterDAL userBLL = new EmployeeMasterDAL();
            tblDesignation objDesigMast = new tblDesignation();
            objDesigMast = userBLL.SelectDesigRghtsStatus(Convert.ToInt32(ddlDesignation.SelectedValue));
            bool? bDesigRghtsStatus = false;
            if (objDesigMast != null)
            {
                bDesigRghtsStatus = objDesigMast.UserRights_Status;
            }
            if (bDesigRghtsStatus == false)
            {
                msg = "Rights for this designation has not assigned. Kindly Assign rights!";
                ddlDesignation.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + msg + "')", true);
            }
            RdoBtnMale.Focus();
        }

        #endregion
    }
}