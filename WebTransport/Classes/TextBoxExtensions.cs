using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTransport.Classes
{
    public static class TextBoxExtensions
    {
        public static void SelectText(this TextBox txt)
        {
            // Is there a ScriptManager on the page?
            if (ScriptManager.GetCurrent(txt.Page) != null && ScriptManager.GetCurrent(txt.Page).IsInAsyncPostBack)
                // Set ctrlToSelect
                ScriptManager.RegisterStartupScript(txt.Page,
                                           txt.Page.GetType(),
                                           "SetFocusInUpdatePanel-" + txt.ClientID,
                                           String.Format("ctrlToSelect='{0}';", txt.ClientID),
                                           true);
            else
                txt.Page.ClientScript.RegisterStartupScript(txt.Page.GetType(),
                                                 "Select-" + txt.ClientID,
                                                 String.Format("document.getElementById('{0}').select();", txt.ClientID),
                                                 true);
        }
    }
}