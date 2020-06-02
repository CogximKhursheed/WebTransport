using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        ReadConn();
    }

    #region Static variables...
    public static string strDBName = "";
    public static string strFinYear = "";
    public static Int32 iFinYrID = 0;
    public static SqlConnection DataConn, RestoreConn;
    #endregion

    #region Public Connection...
    public static void ReadConn()
    {
        try
        {
            DataConn = new SqlConnection();
            RestoreConn = new SqlConnection();
            DataConn.ConnectionString = ConfigurationManager.ConnectionStrings["TransportMandiConnectionString"].ToString();
            RestoreConn.ConnectionString = ConfigurationManager.ConnectionStrings["TransportMandiConnectionString"].ToString();
            strDBName = DataConn.Database;

        }
        catch (Exception Ex)
        {
           
        }
    }
   
}

    #endregion