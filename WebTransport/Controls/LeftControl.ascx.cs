using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using WebTransport.Classes;
using WebTransport.DAL;

namespace WebTransport.Controls
{
    public partial class LeftControl : System.Web.UI.UserControl
    {
        Int32 AmntRecvdAgnst = 0, GRRetRequired = 0;
        Boolean UpdateGr;

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            userpref();
            if (AmntRecvdAgnst == 2)
            {
                LiAmntAgainstGr.Visible = false; LiAmntAgainstInvoice.Visible = true;
            }
            else
            {
                LiAmntAgainstGr.Visible = true; LiAmntAgainstInvoice.Visible = false;
            }
            if (UpdateGr == false)
            {
                hrfUpdateGrDetail.Visible = true;
            }
            else
            {
                hrfUpdateGrDetail.Visible = false;
            }
            //  For Test Site
            //liComsnAgnt.Visible = true;
            // For Main Site
            //liComsnAgnt.Visible = false;
        }
        #endregion

        #region userpref...
        public void userpref()
        {
            GRPrepDAL objGrprepDAL = new GRPrepDAL();
            tblUserPref userpref = objGrprepDAL.selectuserpref();
            AmntRecvdAgnst = Convert.ToInt32(userpref.AmntRecvdAgnst_GRInvce);
            UpdateGr = Convert.ToBoolean(userpref.TBB_Rate);
            if (string.IsNullOrEmpty(userpref.Menu_FleetMgmt.ToString()) ? false : Convert.ToBoolean(userpref.Menu_FleetMgmt) == true)
                liFleetMgmt.Visible = true;
            else
                liFleetMgmt.Visible = false;
            if (string.IsNullOrEmpty(userpref.GRRetRequired.ToString()) ? false : Convert.ToBoolean(userpref.GRRetRequired) == true)
            {
                liRetailMast.Visible = liRetailBookEntry.Visible = true;
                hrfGRConsldRepRetlr.Visible = true;
            }
            else
            {
                liRetailMast.Visible = liRetailBookEntry.Visible = false;
                hrfGRConsldRepRetlr.Visible = false;
            }
        }
        #endregion
    }
}