using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using WebTransport.Classes;
using WebTransport.DAL;
using System.IO;
using System.Text;
using System.Security.Cryptography;


namespace WebTransport
{
    public partial class ViewError : System.Web.UI.Page
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {  
            if (Request.QueryString["Err"] != null)
            {
                // lblErr.Text = "";
                lblErr.Text = "Err Msg : " + Convert.ToString(Decrypt(HttpUtility.UrlDecode(Request.QueryString["Err"])));
            }

            if (Session["DBName"] != null && Session["UserName"] != null)
            {
                if (Session["LogId"].ToString() != "" && Session["LogId"] != null)
                {
                    LoginDAL objLogin = new LoginDAL();
                    objLogin.UpdateLogDetails(Convert.ToInt64(Session["LogId"].ToString()), Convert.ToBoolean(true));
                    Session["LogId"] = null;
                    Session["LastLoginDate"] = null;
                }
            }
            else
            {
                Response.Redirect("LogOut.aspx");
            }

            Session["DBName"] = null;
            Session["UserName"] = null;
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }
        #endregion

        protected void lnklogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("LogOut.aspx");
        }

        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}
