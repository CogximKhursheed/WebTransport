using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SqlHelp
/// </summary>
public class SqlHelp
{
    public SqlHelp()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    /// <summary>
    /// Private function For Get Connection string from WebConfig file
    /// </summary>
    /// <returns></returns>
    private static string GetConnectionString()
    {
        string strConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AutomobileConnectionString"].ToString();
        return strConnectionString;
    }

    public static string GetConnectionString1()
    {
        string strConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
        return strConnectionString;
    }


    /// <summary>
    /// public function for get datatable from Database form Query
    /// </summary>
    /// <param name="strQry"></param>
    /// <returns></returns>
    public static DataTable GetDataTable(string strQry)
    {

        DataTable dt = new DataTable();
        SqlConnection sCon = new SqlConnection(SqlHelp.GetConnectionString());
        try
        {
            sCon.Open();
            SqlDataAdapter sAdp = new SqlDataAdapter(strQry, sCon);
            sAdp.Fill(dt);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            sCon.Dispose();
        }
        return dt;

    }

    /// <summary>
    /// public function for get Dataset from Database form Query
    /// </summary>
    /// <param name="strQry"></param>
    /// <returns></returns>
    public static DataSet GetDataSet(string strQry)
    {

        DataSet ds = new DataSet();
        SqlConnection sCon = new SqlConnection(SqlHelp.GetConnectionString());
        try
        {
            sCon.Open();
            SqlDataAdapter sAdp = new SqlDataAdapter(strQry, sCon);
            sAdp.Fill(ds);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            sCon.Dispose();
        }
        return ds;

    }
    /// <summary>
    /// public function for getdatareader form database from query(This is fast from datatable).
    /// </summary>
    /// <param name="strQry"></param>
    /// <returns></returns>
    public static SqlDataReader GetDataReader(string strQry)
    {
        SqlConnection sCon = new SqlConnection(SqlHelp.GetConnectionString());
        SqlCommand cmd = new SqlCommand(strQry, sCon);
        sCon.Open();
        SqlDataReader sdr = cmd.ExecuteReader();
        return sdr;

    }
    /// <summary>
    /// Public function for ExcecuteNonQuery(Insert,Update,Delete) in Database.
    /// </summary>
    /// <param name="strQry"></param>
    /// <returns></returns>
    public static bool ForExecuteNonQuery(string strQry)
    {
        bool retVal = false;
        SqlConnection sCon = new SqlConnection(SqlHelp.GetConnectionString());
        SqlCommand sCmd = new SqlCommand(strQry, sCon);
        try
        {
            sCon.Open();
            if (sCmd.ExecuteNonQuery() > 0)
            {
                retVal = true;
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            sCon.Dispose();
        }
        return retVal;
    }
    /// <summary>
    /// Public function for getsinglevalue from database table.
    /// </summary>
    /// <param name="strQry"></param>
    /// <returns></returns>
    public static object GetSingleValue(string strQry)
    {
        SqlConnection sCon = new SqlConnection(SqlHelp.GetConnectionString());
        SqlCommand sCmd = new SqlCommand(strQry, sCon);
        object obj;
        try
        {
            sCon.Open();
            obj = sCmd.ExecuteScalar();

        }
        catch (Exception ex)
        {
            obj = -1;
            throw (ex);
        }
        finally
        {
            sCon.Dispose();
        }
        return obj;
    }

    public static double ExecuteScaler(string strSQL, bool Allow)
    {
        SqlConnection sCon = new SqlConnection(SqlHelp.GetConnectionString());
        SqlCommand sCmd = new SqlCommand(strSQL, sCon);
        double ReturnValue = 0;
        try
        {
            Int32 a = 0;
            sCmd.CommandText = strSQL;
            sCmd.Connection = sCon;
            if (sCon.State==ConnectionState.Closed)
            {
                sCon.Open();
            }
            a = sCmd.CommandTimeout;
            //cmd.CommandTimeout = 100;
            ReturnValue = Convert.ToDouble(sCmd.ExecuteScalar());
        }
        catch (Exception ex)
        {
            ReturnValue = -1;
            throw (ex);
        }
        finally
        {
            sCmd.Dispose();
        }
        return ReturnValue;
    }

    public static Boolean ExecuteNonQuery1(string strSQL, SqlTransaction Tran)
    {
        SqlConnection sCon = new SqlConnection(SqlHelp.GetConnectionString());
        SqlCommand cmd = new SqlCommand(strSQL, sCon);
        if (sCon.State == ConnectionState.Closed)
        {
            sCon.Open();
        }
        cmd.Transaction = Tran;
        Int32 NoOfRowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());
        if (NoOfRowsAffected != -1)
            return true;
        else
            return false;
    }
   
}
