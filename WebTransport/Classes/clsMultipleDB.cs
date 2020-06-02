using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebTransport.Classes
{
    public class clsMultipleDB
    {
        //static clsMultipleDB()
        //{
        //    getDynamicConString();
        //}

        #region Static variables...
        //public static string strDBName = string.Empty;
        //public static string strDynamicConString = "";
        //public static string strMasterConns = "metadata=res://*;provider=System.Data.SqlClient;provider connection string=" + '"' + "Data Source=216.245.213.58,1433;Initial Catalog=AutoTesting;Integrated Security=False;User ID=sa;Password=o5q1Fn0ui@e" + '"' + "";

        #endregion

        #region Public Connection...

        public static string strDynamicConString()
        {
            string strDBName = string.Empty;
            if ((System.Web.HttpContext.Current.Session["DBName"]) != null)
            {
                strDBName = System.Web.HttpContext.Current.Session["DBName"].ToString();
            }

            if (string.IsNullOrEmpty(strDBName) == true)
            {
                if (HttpContext.Current != null)
                    HttpContext.Current.Response.Redirect("Login.aspx");
            }
            string connectionString = new System.Data.EntityClient.EntityConnectionStringBuilder
            {
                Metadata = "res://*",
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = new System.Data.SqlClient.SqlConnectionStringBuilder
                {
                    InitialCatalog = strDBName,
                    DataSource = "136.243.149.22,1443",
                    PersistSecurityInfo = true,
                    UserID = "sa",
                    Password = "41kc*mRq4IWyUK5eW6E",

                    //InitialCatalog = strDBName,
                    //DataSource = "ACER-PC\\SQLEXPRESS",
                    //IntegratedSecurity = true,
                }.ConnectionString
            }.ConnectionString;

            return connectionString;
        }


        //public static void getDynamicConString()
        //{

        //    if ((System.Web.HttpContext.Current.Session["DBName"]) != null)
        //    {
        //        strDBName = System.Web.HttpContext.Current.Session["DBName"].ToString();
        //    }

        //    if (string.IsNullOrEmpty(strDBName) == true)
        //    {
        //        if (HttpContext.Current != null)
        //            HttpContext.Current.Response.Redirect("Login.aspx");
        //    }
        //    string connectionString = new System.Data.EntityClient.EntityConnectionStringBuilder
        //    {
        //        Metadata = "res://*",
        //        Provider = "System.Data.SqlClient",
        //        ProviderConnectionString = new System.Data.SqlClient.SqlConnectionStringBuilder
        //        {
        //            InitialCatalog = strDBName,
        //            DataSource = "216.245.213.58,1433",
        //            IntegratedSecurity = false,
        //            UserID = "sa",
        //            Password = "o5q1Fn0ui@e",
        //        }.ConnectionString
        //    }.ConnectionString;

        //    strDynamicConString = connectionString;
        //}

        #endregion
    }
}