using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;

namespace WebTransport.Classes
{
    class clsDataAccessFunction
    {
        #region "Global Variable Declaration Section"
        public static string QGetNextID = "SELECT IsNull(Max(%%Param1%%)+1,1) AS MaxID FROM %%Param2%%";
        public static string QGetNextIDChasis = "SELECT %%Param1%% AS MaxID FROM %%Param2%% where chasis_no='%%Param3%%'";
        public static string QCheckDuplicate = "SELECT %%Param1%% FROM %%Param2%% WHERE %%Param3%% = '%%Param4%%' ";
        #endregion

        #region DataBase Process & Controls Populate Function

        public static clsDataSetFunction DAGetDataSet(SqlTransaction Tran, SqlConnection conn, params object[] Param)
        {
            SqlDataAdapter DataAdaptor = null;
            string strGivenQuery = Param[0].ToString();
            clsDataSetFunction RetVal = new clsDataSetFunction();
            System.DateTime StartAccess = DateTime.MinValue;
            int Count = 0;
            for (Count = 1; Count <= Param.GetUpperBound(0); Count++)
            {
                if (Param[Count].ToString() == "'")
                {
                    Param[Count] = "";
                }
                strGivenQuery = strGivenQuery.Replace("%%Param" + System.Convert.ToString(Count) + "%%", System.Convert.ToString(Param[Count]));
            }
            try
            {
                SqlCommand cmd = new SqlCommand(strGivenQuery, conn);
                cmd.Transaction = Tran;
                DataAdaptor = new SqlDataAdapter(cmd);
                DataAdaptor.Fill(RetVal);
            }
            catch (Exception)
            {
                RetVal = new clsDataSetFunction();
                RetVal = null;
            }

            return RetVal;
        }

        public static bool RestoreConnection()
        {
            try
            {
                if (Program.RestoreConn.State == ConnectionState.Closed) Program.RestoreConn.Open();
            }
            catch (Exception Ex)
            {
                //MessageBox.Show("Connection Failed To Database", Program.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //clsCommonFunction.FuncTraceError("ClsCommanFunction.cs", "RestoreConnection", Ex.Message);
                return false;
            }
            return true;
        }

        /*--(1):-This Function is work as Get Data From dataBase And Return Dataset*/
        public static clsDataSetFunction DAGetDataSet(params object[] Param)
        {
            SqlDataAdapter DataAdaptor = null;
            string strGivenQuery = Param[0].ToString();
            clsDataSetFunction RetVal = new clsDataSetFunction();
            System.DateTime StartAccess = DateTime.MinValue;
            int Count = 0;
            for (Count = 1; Count <= Param.GetUpperBound(0); Count++)
            {
                if (Param[Count].ToString() == "'")
                {
                    Param[Count] = "";
                }
                strGivenQuery = strGivenQuery.Replace("%%Param" + System.Convert.ToString(Count) + "%%", System.Convert.ToString(Param[Count]));
            }
            try
            {
                if (CheckConnection() == false)
                {
                    RetVal = new clsDataSetFunction();
                    RetVal = null;
                }

                DataAdaptor = new SqlDataAdapter(strGivenQuery, Program.DataConn);
              //  DataAdaptor = new SqlDataAdapter(strGivenQuery, ApplicationFunction.ConnectionString());
                DataAdaptor.Fill(RetVal);
            }
            catch (Exception)
            {
                RetVal = new clsDataSetFunction();
                RetVal = null;
            }
            Program.DataConn.Close();
            return RetVal;
        }
        /*--(2):-This function is update ,insert and delete data in database*/
        public static bool DAUpdateDataSet(params object[] Param)
        {
            SqlCommand DataCommand = null;
            string strGivenQuery = Param[0].ToString();
            bool RetVal = true;
            System.DateTime StartAccess = DateTime.MinValue;
            int RecAffected = 0;
            int Count = 0;
            for (Count = 1; Count <= Param.GetUpperBound(0); Count++)
            {
                if (Param[Count].ToString() == "'")
                {
                    Param[Count] = "";
                }
                strGivenQuery = strGivenQuery.Replace("%%Param" + System.Convert.ToString(Count) + "%%", System.Convert.ToString(Param[Count]));
            }

            try
            {
                Program.DataConn.Open();
                DataCommand = new SqlCommand(strGivenQuery, Program.DataConn);
                RecAffected = DataCommand.ExecuteNonQuery();

            }
            catch (Exception)
            {
                RetVal = false;
            }
            Program.DataConn.Close();
            return RetVal;
        }
        /*--(3):-this function work as fill combobox*/


        /*--(6):-This function is work as get  to table Next Id*/
        public static int DAGetNextID(string TableName, string Field)
        {
            clsDataSetFunction ExDS = null;
            try
            {
                ExDS = DAGetDataSet(QGetNextID, Field, TableName);
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title);
            }
            return ExDS.NullAsInteger(0, "MaxID");
        }
        public static int DAGetNextID(string TableName, string Field, string parmField)
        {
            clsDataSetFunction ExDS = null;
            try
            {
                ExDS = DAGetDataSet(QGetNextIDChasis, Field, TableName, parmField);
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title);
            }
            return ExDS.NullAsInteger(0, "MaxID");
        }
        /*--(7):-This function is work as Check Duplicate value*/
        public static bool DAIsDuplicate(string strTableName, string strFieldName, string strCheckFild, string strParameterValue)
        {
            clsDataSetFunction ExDS = null;
            try
            {
                ExDS = DAGetDataSet(QCheckDuplicate, strFieldName, strTableName, strCheckFild, strParameterValue);
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title);
            }
            return (ExDS.RowsCount() > 0);

        }

        /*--(9):-This Function Work as Create Data Table*/
        public static DataTable DACreateTable(string TableName, params object[] Param)
        {
            DataTable RetDT = new DataTable();
            try
            {
                if (Param.Length > 0 && (Param.Length % 2) == 0)
                {
                    RetDT.TableName = TableName;
                    for (int Count = 0; Count < Param.Length; Count += 2)
                    {
                        RetDT.Columns.Add(Param[Count].ToString()).DataType = System.Type.GetType("System." + Param[Count + 1].ToString());
                    }
                }
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title);
            }

            return RetDT;
        }
        /*--(10):-This Function Work as Add Row Data Table*/
        public static void DAAddRow(DataTable DT, params object[] Param)
        {
            try
            {
                DataRow DR = DT.NewRow();

                for (int Count = 0; Count < Param.Length; Count++)
                {
                    DR[Count] = Param[Count];
                }
                DT.Rows.Add(DR);
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title);
            }

        }
        public static Int32 ExecuteScaler(string strSQL, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand();
            Int32 ReturnValue = 0;
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.CommandText = strSQL;
                cmd.Connection = conn;
                ReturnValue = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title);
            }
            finally
            {
                cmd.Dispose();
                //if (conn.State == ConnectionState.Open) conn.Close();
            }
            return ReturnValue;
        }
        public static double ExecuteScaler(string strSQL, SqlConnection conn,bool Allow)
        {
            SqlCommand cmd = new SqlCommand();
            double ReturnValue = 0;
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.CommandText = strSQL;
                cmd.Connection = conn;
                ReturnValue = Convert.ToDouble(cmd.ExecuteScalar());
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title);
            }
            finally
            {
                cmd.Dispose();
                //if (conn.State == ConnectionState.Open) conn.Close();
            }
            return ReturnValue;
        }
        /*--(11):-This Function Work a Get Single Value From dataBase*/
        public static Int32 ExecuteScaler(string strSQL, SqlTransaction Tran, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand();
            Int32 ReturnValue = 0;
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.CommandText = strSQL;
                cmd.Connection = conn;
                cmd.Transaction = Tran;
                ReturnValue = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title);
            }
            finally
            {
                cmd.Dispose();
                //if (conn.State == ConnectionState.Open) conn.Close();
            }
            return ReturnValue;
        }
        public static Int32 ExecuteScaler(string strSQL)
        {
            SqlCommand cmd = new SqlCommand();
            Int32 ReturnValue = 0;
            try
            {
                if (Program.DataConn.State == ConnectionState.Closed) Program.DataConn.Open();
                cmd.CommandText = strSQL;
                cmd.Connection = Program.DataConn;
                ReturnValue = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title);
            }
            finally
            {
                cmd.Dispose();
            }
            return ReturnValue;
        }
        public static double ExecuteScaler(string strSQL, bool Allow)
        {
            SqlCommand cmd = new SqlCommand();
            double ReturnValue = 0;
            try
            {
                if (Program.DataConn.State == ConnectionState.Closed) Program.DataConn.Open();
                cmd.CommandText = strSQL;
                cmd.Connection = Program.DataConn;
                ReturnValue = Convert.ToDouble(cmd.ExecuteScalar());
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title);
            }
            finally
            {
                cmd.Dispose();
            }
            return ReturnValue;
        }
        public static Object ExecuteScaler1(String strSQL)
        {
            SqlCommand cmd = new SqlCommand();
            Object ReturnValue = "";
            try
            {
                if (Program.DataConn.State == ConnectionState.Closed) Program.DataConn.Open();
                cmd.CommandText = strSQL;
                cmd.Connection = Program.DataConn;
                ReturnValue = cmd.ExecuteScalar();
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title);
            }
            finally
            {
                cmd.Dispose();
            }
            return ReturnValue;
        }
        /*     UPDATE COMPLETE DATASET    */
        public static void dsUpdate(DataSet dsName, string strSQL, string strTableName)
        {

            SqlCommand cmd = new SqlCommand(strSQL, Program.DataConn);
            SqlDataAdapter aDap = new SqlDataAdapter(cmd);
            SqlCommandBuilder Builder = new SqlCommandBuilder();

            aDap.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            Builder.DataAdapter = aDap;
            aDap.SelectCommand = cmd;

            aDap.InsertCommand = Builder.GetInsertCommand();
            aDap.UpdateCommand = Builder.GetUpdateCommand();
            aDap.DeleteCommand = Builder.GetDeleteCommand();
            aDap.Update(dsName, strTableName);
            Program.DataConn.Close();
        }
        /*     UPDATE COMPLETE DATASET    */
        public static bool dsUpdate(DataSet dsName, string strSQL, string strTableName, SqlTransaction Tran, SqlConnection conn)
        {

            SqlCommand cmd = new SqlCommand(strSQL, conn);
            cmd.Transaction = Tran;
            SqlDataAdapter aDap = new SqlDataAdapter(cmd);
            SqlCommandBuilder Builder = new SqlCommandBuilder();

            aDap.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            Builder.DataAdapter = aDap;
            aDap.SelectCommand = cmd;

            aDap.InsertCommand = Builder.GetInsertCommand();
            aDap.UpdateCommand = Builder.GetUpdateCommand();
            aDap.DeleteCommand = Builder.GetDeleteCommand();
            Int32 iCheck = aDap.Update(dsName.Tables[strTableName]);
            if (iCheck > 0)
                return true;
            else
                return false;
            // Program.DataConn.Close();
        }
        /* Transaction Call for All Store Procedure Execute */
        public static Boolean ExecuteNonQuery(string strSQL, SqlTransaction Tran, SqlConnection conn)
        {
            try
            {

                SqlCommand cmd = new SqlCommand(strSQL, conn);
                cmd.Transaction = Tran;
                Int32 NoOfRowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());
                if (NoOfRowsAffected == -1)
                    return true;
                else
                    return false;
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message);
                return false;
            }
            return true;
        }
        public static Boolean ExecuteNonQuery(string strSQL)
        {
            try
            {
                CheckConnection();
                SqlCommand cmd = new SqlCommand(strSQL, Program.DataConn);
                cmd.CommandTimeout = 180;
                Int32 NoOfRowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());
                if (NoOfRowsAffected == -1)
                    return true;
                else
                    return false;
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message); 
                return false;
            }

        }
        /* Transaction Call for All Store Procedure Execute New*/
        public static Boolean ExecuteNonQuery1(string strSQL, SqlTransaction Tran, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            cmd.Transaction = Tran;
            Int32 NoOfRowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());
            if (NoOfRowsAffected != -1)
                return true;
            else
                return false;
        }
        public static Boolean ExecuteNonQuery1(string strSQL)
        {
            try
            {
                CheckConnection();
                SqlCommand cmd = new SqlCommand(strSQL, Program.DataConn);
                cmd.CommandTimeout = 180;
                Int32 NoOfRowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());
                if (NoOfRowsAffected != -1)
                    return true;
                else
                    return false;
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message); 
                return false;
            }

        }
        /* Connection State Check */
        public static bool CheckConnection()
        {
            try
            {
                if (Program.DataConn.State == ConnectionState.Closed) Program.DataConn.Open();
            }
            catch (Exception Ex)
            {
                //MessageBox.Show("Connection Failed To Database", Program.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //clsCommonFunction.FuncTraceError("ClsCommanFunction.cs", "CheckConnection", Ex.Message);
                return false;
            }
            return true;
        }
        /* Check Duplicate Value */
        public static Boolean CheckDuplicate(string strSQL, SqlTransaction Tran, SqlConnection conn)
        {
            bool ReturnValue = false;
            try
            {
                SqlCommand cmd = new SqlCommand(strSQL, conn);
                cmd.Transaction = Tran;
                Int32 ValueReturn = Convert.ToInt32(cmd.ExecuteScalar());
                if (ValueReturn > 0)
                    ReturnValue = true;
                else
                    ReturnValue = false;
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title);
            }
            return ReturnValue;
        }
        public static Boolean CheckDuplicate(string strSQL)
        {
            bool ReturnValue = false;
            try
            {
                SqlCommand cmd = new SqlCommand(strSQL, Program.DataConn);
                Int32 ValueReturn = Convert.ToInt32(cmd.ExecuteScalar());
                if (ValueReturn > 0)
                    ReturnValue = true;
                else
                    ReturnValue = false;
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title); ReturnValue = false;
            }
            return ReturnValue;
        }
        #region /* To Insert Bulk Data - Ramesh */
        /* To Insert Bulk Data - Ramesh */
        //public Boolean BulkInsert(DataTable ExDS)
        //{
        //string tblName;
        //SqlTransaction Tran;
        //bool Flag;
        //Tran = Program.DataConn.BeginTransaction();
        //SqlBulkCopy sqlbCopy = new SqlBulkCopy(Program.DataConn);            

        //if(ExDS.Rows.Count > 0 )
        //{
        //    tblName = ExDS.TableName;
        //    sqlbCopy.DestinationTableName = tblName;
        //    string sSql = "Delete * From VhclPur";
        //    if (clsDataAccessFunction.ExecuteNonQuery(sSql, Tran, Program.DataConn) == true)
        //    {
        //        sqlbCopy.BatchSize = Convert.ToInt32(ExDS.Rows.Count);
        //        sqlbCopy.NotifyAfter = 1000;
        //        sqlbCopy.BulkCopyTimeout = 1000;
        //        sqlbCopy.WriteToServer(ExDS);
        //        Tran.Commit();
        //        Flag = true;
        //        return Flag;
        //    }
        //    else
        //    {
        //        Tran.Rollback();
        //        Flag = false;
        //        return Flag;
        //    }
        //    return Flag;
        //}
        //Program.DataConn.Close();
        //Program.DataConn.Dispose();
        //sqlbCopy.Close();
        //return Flag;
        //}   
        #endregion
        #endregion
    }

    //BgWorker

    public class clsBgDataAccessFunction
    {
        #region "Global Variable Declaration Section"
        public string bgQGetNextID = "SELECT IsNull(Max(%%Param1%%)+1,1) AS MaxID FROM %%Param2%%";
        public string bgQCheckDuplicate = "SELECT %%Param1%% FROM %%Param2%% WHERE %%Param3%% = '%%Param4%%' ";
        public SqlConnection bgDataConn;

        public clsBgDataAccessFunction()
        {
            bgDataConn = new SqlConnection();
            bgDataConn.ConnectionString = ConfigurationManager.AppSettings["ConnString"];
        }

        #endregion

        #region DataBase Process & Controls Populate Function

        public clsBgDataSetFunction DAGetDataSet(SqlTransaction Tran, SqlConnection conn, params object[] Param)
        {
            SqlDataAdapter DataAdaptor = null;
            string strGivenQuery = Param[0].ToString();
            clsBgDataSetFunction RetVal = new clsBgDataSetFunction();
            System.DateTime StartAccess = DateTime.MinValue;
            int Count = 0;
            for (Count = 1; Count <= Param.GetUpperBound(0); Count++)
            {
                if (Param[Count].ToString() == "'")
                {
                    Param[Count] = "";
                }
                strGivenQuery = strGivenQuery.Replace("%%Param" + System.Convert.ToString(Count) + "%%", System.Convert.ToString(Param[Count]));
            }
            try
            {
                SqlCommand cmd = new SqlCommand(strGivenQuery, conn);
                cmd.Transaction = Tran;
                DataAdaptor = new SqlDataAdapter(cmd);
                DataAdaptor.Fill(RetVal);
            }
            catch (Exception)
            {
                RetVal = new clsBgDataSetFunction();
                RetVal = null;
            }

            return RetVal;
        }

        /*--(1):-This Function is work as Get Data From dataBase And Return Dataset*/
        public clsBgDataSetFunction DAGetDataSet(params object[] Param)
        {
            SqlDataAdapter DataAdaptor = null;
            string strGivenQuery = Param[0].ToString();
            clsBgDataSetFunction RetVal = new clsBgDataSetFunction();
            System.DateTime StartAccess = DateTime.MinValue;
            int Count = 0;
            for (Count = 1; Count <= Param.GetUpperBound(0); Count++)
            {
                if (Param[Count].ToString() == "'")
                {
                    Param[Count] = "";
                }
                strGivenQuery = strGivenQuery.Replace("%%Param" + System.Convert.ToString(Count) + "%%", System.Convert.ToString(Param[Count]));
            }
            try
            {
                CheckConnection();
                DataAdaptor = new SqlDataAdapter(strGivenQuery, bgDataConn);
                DataAdaptor.Fill(RetVal);
            }
            catch (Exception)
            {
                RetVal = new clsBgDataSetFunction();
                RetVal = null;
            }
            bgDataConn.Close();
            // Program.bgDataConn.Close();
            return RetVal;
        }
        /*--(2):-This function is update ,insert and delete data in database*/
        public bool DAUpdateDataSet(params object[] Param)
        {
            SqlCommand DataCommand = null;
            string strGivenQuery = Param[0].ToString();
            bool RetVal = true;
            System.DateTime StartAccess = DateTime.MinValue;
            int RecAffected = 0;
            int Count = 0;
            for (Count = 1; Count <= Param.GetUpperBound(0); Count++)
            {
                if (Param[Count].ToString() == "'")
                {
                    Param[Count] = "";
                }
                strGivenQuery = strGivenQuery.Replace("%%Param" + System.Convert.ToString(Count) + "%%", System.Convert.ToString(Param[Count]));
            }

            try
            {
                // Program.DataConn.Open();
                bgDataConn.Close();
                // DataCommand = new SqlCommand(strGivenQuery, Program.DataConn);
                DataCommand = new SqlCommand(strGivenQuery, bgDataConn);
                RecAffected = DataCommand.ExecuteNonQuery();

            }
            catch (Exception)
            {
                RetVal = false;
            }
            //Program.DataConn.Close();
            bgDataConn.Close();

            return RetVal;
        }

        /*--(6):-This function is work as get  to table Next Id*/
        public int DAGetNextID(string TableName, string Field)
        {
            clsBgDataSetFunction ExDS = null;
            try
            {
                ExDS = DAGetDataSet(bgQGetNextID, Field, TableName);
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title);
            }
            return ExDS.NullAsInteger(0, "MaxID");
        }
        /*--(7):-This function is work as Check Duplicate value*/
        public bool DAIsDuplicate(string strTableName, string strFieldName, string strCheckFild, string strParameterValue)
        {
            clsBgDataSetFunction ExDS = null;
            try
            {
                ExDS = DAGetDataSet(bgQCheckDuplicate, strFieldName, strTableName, strCheckFild, strParameterValue);
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title);
            }
            return (ExDS.RowsCount() > 0);

        }
        /*--(8):-This function work as Control Clear*/

        /*--(9):-This Function Work as Create Data Table*/
        public DataTable DACreateTable(string TableName, params object[] Param)
        {
            DataTable RetDT = new DataTable();
            try
            {
                if (Param.Length > 0 && (Param.Length % 2) == 0)
                {
                    RetDT.TableName = TableName;
                    for (int Count = 0; Count < Param.Length; Count += 2)
                    {
                        RetDT.Columns.Add(Param[Count].ToString()).DataType = System.Type.GetType("System." + Param[Count + 1].ToString());
                    }
                }
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title);
            }

            return RetDT;
        }
        /*--(10):-This Function Work as Add Row Data Table*/
        public void DAAddRow(DataTable DT, params object[] Param)
        {
            try
            {
                DataRow DR = DT.NewRow();

                for (int Count = 0; Count < Param.Length; Count++)
                {
                    DR[Count] = Param[Count];
                }
                DT.Rows.Add(DR);
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title);
            }

        }
        /*--(11):-This Function Work a Get Single Value From dataBase*/
        public Int32 ExecuteScaler(string strSQL, SqlTransaction Tran, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand();
            Int32 ReturnValue = 0;
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.CommandText = strSQL;
                cmd.Connection = conn;
                cmd.Transaction = Tran;
                ReturnValue = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title);
            }
            finally
            {
                cmd.Dispose();
                //if (conn.State == ConnectionState.Open) conn.Close();
            }
            return ReturnValue;
        }
        public Int32 ExecuteScaler(string strSQL)
        {
            SqlCommand cmd = new SqlCommand();
            Int32 ReturnValue = 0;
            try
            {
                if (Program.DataConn.State == ConnectionState.Closed) Program.DataConn.Open();
                cmd.CommandText = strSQL;
                cmd.Connection = Program.DataConn;
                ReturnValue = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title);
            }
            finally
            {
                cmd.Dispose();
            }
            return ReturnValue;
        }
        public double ExecuteScaler(string strSQL, bool Allow)
        {
            SqlCommand cmd = new SqlCommand();
            double ReturnValue = 0;
            try
            {
                if (Program.DataConn.State == ConnectionState.Closed) Program.DataConn.Open();
                cmd.CommandText = strSQL;
                cmd.Connection = Program.DataConn;
                ReturnValue = Convert.ToDouble(cmd.ExecuteScalar());
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title);
            }
            finally
            {
                cmd.Dispose();
            }
            return ReturnValue;
        }
        public Object ExecuteScaler1(String strSQL)
        {
            SqlCommand cmd = new SqlCommand();
            Object ReturnValue = "";
            try
            {
                if (Program.DataConn.State == ConnectionState.Closed) Program.DataConn.Open();
                cmd.CommandText = strSQL;
                cmd.Connection = Program.DataConn;
                ReturnValue = cmd.ExecuteScalar();
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title);
            }
            finally
            {
                cmd.Dispose();
            }
            return ReturnValue;
        }

        public void dsUpdate(DataSet dsName, string strSQL, string strTableName)
        {

            SqlCommand cmd = new SqlCommand(strSQL, Program.DataConn);
            SqlDataAdapter aDap = new SqlDataAdapter(cmd);
            SqlCommandBuilder Builder = new SqlCommandBuilder();

            aDap.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            Builder.DataAdapter = aDap;
            aDap.SelectCommand = cmd;

            aDap.InsertCommand = Builder.GetInsertCommand();
            aDap.UpdateCommand = Builder.GetUpdateCommand();
            aDap.DeleteCommand = Builder.GetDeleteCommand();
            aDap.Update(dsName, strTableName);
            Program.DataConn.Close();
        }
        /* Transaction Call for All Store Procedure Execute */
        public Boolean ExecuteNonQuery(string strSQL, SqlTransaction Tran, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            cmd.Transaction = Tran;
            Int32 NoOfRowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());
            if (NoOfRowsAffected == -1)
                return true;
            else
                return false;
        }
        /* Transaction Call for All Store Procedure Execute New*/
        public Boolean ExecuteNonQuery1(string strSQL, SqlTransaction Tran, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            cmd.Transaction = Tran;
            Int32 NoOfRowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());
            if (NoOfRowsAffected != -1)
                return true;
            else
                return false;
        }
        /* Connection State Check */
        public void CheckConnection()
        {
            try
            {
                //if (Program.bgDataConn.State == ConnectionState.Closed) Program.bgDataConn.Open();
                if (bgDataConn.State == ConnectionState.Closed) bgDataConn.Open();
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title);
            }
        }
        /* Check Duplicate Value */
        public Boolean CheckDuplicate(string strSQL, SqlTransaction Tran, SqlConnection conn)
        {
            bool ReturnValue = false;
            try
            {
                SqlCommand cmd = new SqlCommand(strSQL, conn);
                cmd.Transaction = Tran;
                Int32 ValueReturn = Convert.ToInt32(cmd.ExecuteScalar());
                if (ValueReturn > 0)
                    ReturnValue = true;
                else
                    ReturnValue = false;
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message, Program.Title);
            }
            return ReturnValue;
        }

        #endregion
    }

}