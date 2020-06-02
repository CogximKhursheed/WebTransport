using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.CSharp;
using System.Collections.Generic;
using System.Text;

namespace WebTransport.Classes
{
    public class clsDataSetFunction : DataSet
    {
        //ORIGINAL LINE: Public Property GetData(ByVal intRow As Integer, ByVal strCol As String) As Object
        //INSTANT C# NOTE: C# does not support parameterized properties - the following property has been divided into two methods:
        public object GetGetData(int intRow, string strCol)
        {           
            return this.Tables[0].Rows[intRow][strCol];
        }
        public void SetGetData(int intRow, string strCol, object value)
        {
            this.Tables[0].Rows[intRow][strCol] = value;
        }
        //ORIGINAL LINE: Public Property GetData(ByVal intRow As Integer, ByVal intCol As Integer) As Object
        //INSTANT C# NOTE: C# does not support parameterized properties - the following property has been divided into two methods:
        public object GetGetData(int intRow, int intCol)
        {
            return this.Tables[0].Rows[intRow][intCol];
        }
        public void SetGetData(int intRow, int intCol, object value)
        {
            this.Tables[0].Rows[intRow][intCol] = value;
        }
        //ORIGINAL LINE: Public Property GetData(ByVal tblIndex As Integer, ByVal intRow As Integer, ByVal strCol As String) As Object
        //INSTANT C# NOTE: C# does not support parameterized properties - the following property has been divided into two methods:
        public object GetGetData(int tblIndex, int intRow, string strCol)
        {
            return this.Tables[tblIndex].Rows[intRow][strCol];
        }
        public void SetGetData(int tblIndex, int intRow, string strCol, object value)
        {
            this.Tables[tblIndex].Rows[intRow][strCol] = value;

        }
        //ORIGINAL LINE: Public Property GetData(ByVal tblName As String, ByVal intRow As Integer, ByVal strCol As String) As Object
        //INSTANT C# NOTE: C# does not support parameterized properties - the following property has been divided into two methods:
        public object GetGetData(string tblName, int intRow, string strCol)
        {
            return this.Tables[tblName].Rows[intRow][strCol];

        }
        public void SetGetData(string tblName, int intRow, string strCol, object value)
        {
            this.Tables[tblName].Rows[intRow][strCol] = value;

        }
        //ORIGINAL LINE: Public Property GetDataStr(ByVal intRow As Integer, ByVal strCol As String) As String
        //INSTANT C# NOTE: C# does not support parameterized properties - the following property has been divided into two methods:
        public string GetGetDataStr(int intRow, string strCol)
        {
            return this.Tables[0].Rows[intRow][strCol].ToString();

        }
        public void SetGetDataStr(int intRow, string strCol, string value)
        {
            this.Tables[0].Rows[intRow][strCol] = value;
        }

        //ORIGINAL LINE: Public Property GetDataStr(ByVal intRow As Integer, ByVal intCol As Integer) As String
        //INSTANT C# NOTE: C# does not support parameterized properties - the following property has been divided into two methods:
        public string GetGetDataStr(int intRow, int intCol)
        {
            return this.Tables[0].Rows[intRow][intCol].ToString();
        }
        public void SetGetDataStr(int intRow, int intCol, string value)
        {
            this.Tables[0].Rows[intRow][intCol] = value;
        }

        //ORIGINAL LINE: Public Property GetDataInt(ByVal intRow As Integer, ByVal strCol As String) As Integer
        //INSTANT C# NOTE: C# does not support parameterized properties - the following property has been divided into two methods:
        public int GetGetDataInt(int intRow, string strCol)
        {
            return System.Convert.ToInt32(this.Tables[0].Rows[intRow][strCol]);

        }
        public void SetGetDataInt(int intRow, string strCol, int value)
        {
            this.Tables[0].Rows[intRow][strCol] = value;

        }

        //ORIGINAL LINE: Public Property GetDataInt(ByVal intRow As Integer, ByVal intCol As Integer) As Integer
        //INSTANT C# NOTE: C# does not support parameterized properties - the following property has been divided into two methods:

        public int GetGetDataInt(int intRow, int intCol)
        {
            return System.Convert.ToInt32(this.Tables[0].Rows[intRow][intCol]);

        }
        public void SetGetDataInt(int intRow, int intCol, int value)
        {
            this.Tables[0].Rows[intRow][intCol] = value;

        }

        public int RowsCount()
        {
            int RetVal = 0;
            try
            {
                RetVal = this.Tables[0].Rows.Count;
            }
            catch
            {
                RetVal = -1;
            }
            return RetVal;
        }

        public bool HasRecords()
        {
            return (this.Tables[0].Rows.Count != 0);

        }

        public bool IsNull(int GivenRow, string GivenField)
        {
            try
            {
                return System.Convert.IsDBNull(this.Tables[0].Rows[GivenRow][GivenField]);
            }
            catch
            {
                return false;
            }
        }

        public bool IsNull(int GivenRow, int GivenField)
        {
            try
            {
                return System.Convert.IsDBNull(this.Tables[0].Rows[GivenRow][GivenField]);
            }
            catch
            {
                return false;
            }
        }

        public string NullAsString(int GivenRow, string GivenField)
        {
            return System.Convert.ToString(((IsNull(GivenRow, GivenField)) ? "" : this.Tables[0].Rows[GivenRow][GivenField]));
        }

        public string NullAsString(int GivenRow, int GivenField)
        {
            return System.Convert.ToString(((IsNull(GivenRow, GivenField)) ? "" : this.Tables[0].Rows[GivenRow][GivenField]));
        }

        public double NullAsDouble(int GivenRow, string GivenField)
        {
            return System.Convert.ToDouble(((IsNull(GivenRow, GivenField)) ? 0 : this.Tables[0].Rows[GivenRow][GivenField]));
        }

        public double NullAsDouble(int GivenRow, int GivenField)
        {
            return System.Convert.ToDouble(((IsNull(GivenRow, System.Convert.ToString(GivenField))) ? 0 : this.Tables[0].Rows[GivenRow][GivenField]));
        }

        public int NullAsInteger(int GivenRow, string GivenField)
        {
            return System.Convert.ToInt32(((IsNull(GivenRow, GivenField)) ? 0 : this.Tables[0].Rows[GivenRow][GivenField]));

        }

        public int NullAsInteger(int GivenRow, int GivenField)
        {
            return System.Convert.ToInt32(((IsNull(GivenRow, System.Convert.ToString(GivenField))) ? 0 : this.Tables[0].Rows[GivenRow][GivenField]));
        }

        public System.DateTime NullAsDate(int GivenRow, int GivenField)
        {
            return System.Convert.ToDateTime(((IsNull(GivenRow, GivenField)) ? "01/01/1900" : this.Tables[0].Rows[GivenRow][GivenField]));
        }
    }

    //BackgroundWorker

    public class clsBgDataSetFunction : DataSet
    {
        //ORIGINAL LINE: Public Property GetData(ByVal intRow As Integer, ByVal strCol As String) As Object
        //INSTANT C# NOTE: C# does not support parameterized properties - the following property has been divided into two methods:
        public object GetGetData(int intRow, string strCol)
        {
            return this.Tables[0].Rows[intRow][strCol];
        }
        public void SetGetData(int intRow, string strCol, object value)
        {
            this.Tables[0].Rows[intRow][strCol] = value;
        }
        //ORIGINAL LINE: Public Property GetData(ByVal intRow As Integer, ByVal intCol As Integer) As Object
        //INSTANT C# NOTE: C# does not support parameterized properties - the following property has been divided into two methods:
        public object GetGetData(int intRow, int intCol)
        {
            return this.Tables[0].Rows[intRow][intCol];
        }
        public void SetGetData(int intRow, int intCol, object value)
        {
            this.Tables[0].Rows[intRow][intCol] = value;
        }
        //ORIGINAL LINE: Public Property GetData(ByVal tblIndex As Integer, ByVal intRow As Integer, ByVal strCol As String) As Object
        //INSTANT C# NOTE: C# does not support parameterized properties - the following property has been divided into two methods:
        public object GetGetData(int tblIndex, int intRow, string strCol)
        {
            return this.Tables[tblIndex].Rows[intRow][strCol];
        }
        public void SetGetData(int tblIndex, int intRow, string strCol, object value)
        {
            this.Tables[tblIndex].Rows[intRow][strCol] = value;

        }
        //ORIGINAL LINE: Public Property GetData(ByVal tblName As String, ByVal intRow As Integer, ByVal strCol As String) As Object
        //INSTANT C# NOTE: C# does not support parameterized properties - the following property has been divided into two methods:
        public object GetGetData(string tblName, int intRow, string strCol)
        {
            return this.Tables[tblName].Rows[intRow][strCol];

        }
        public void SetGetData(string tblName, int intRow, string strCol, object value)
        {
            this.Tables[tblName].Rows[intRow][strCol] = value;

        }
        //ORIGINAL LINE: Public Property GetDataStr(ByVal intRow As Integer, ByVal strCol As String) As String
        //INSTANT C# NOTE: C# does not support parameterized properties - the following property has been divided into two methods:
        public string GetGetDataStr(int intRow, string strCol)
        {
            return this.Tables[0].Rows[intRow][strCol].ToString();

        }
        public void SetGetDataStr(int intRow, string strCol, string value)
        {
            this.Tables[0].Rows[intRow][strCol] = value;
        }

        //ORIGINAL LINE: Public Property GetDataStr(ByVal intRow As Integer, ByVal intCol As Integer) As String
        //INSTANT C# NOTE: C# does not support parameterized properties - the following property has been divided into two methods:
        public string GetGetDataStr(int intRow, int intCol)
        {
            return this.Tables[0].Rows[intRow][intCol].ToString();
        }
        public void SetGetDataStr(int intRow, int intCol, string value)
        {
            this.Tables[0].Rows[intRow][intCol] = value;
        }

        //ORIGINAL LINE: Public Property GetDataInt(ByVal intRow As Integer, ByVal strCol As String) As Integer
        //INSTANT C# NOTE: C# does not support parameterized properties - the following property has been divided into two methods:
        public int GetGetDataInt(int intRow, string strCol)
        {
            return System.Convert.ToInt32(this.Tables[0].Rows[intRow][strCol]);

        }
        public void SetGetDataInt(int intRow, string strCol, int value)
        {
            this.Tables[0].Rows[intRow][strCol] = value;

        }

        //ORIGINAL LINE: Public Property GetDataInt(ByVal intRow As Integer, ByVal intCol As Integer) As Integer
        //INSTANT C# NOTE: C# does not support parameterized properties - the following property has been divided into two methods:

        public int GetGetDataInt(int intRow, int intCol)
        {
            return System.Convert.ToInt32(this.Tables[0].Rows[intRow][intCol]);

        }
        public void SetGetDataInt(int intRow, int intCol, int value)
        {
            this.Tables[0].Rows[intRow][intCol] = value;

        }

        public int RowsCount()
        {
            int RetVal = 0;
            try
            {
                RetVal = this.Tables[0].Rows.Count;
            }
            catch
            {
                RetVal = -1;
            }
            return RetVal;
        }

        public bool HasRecords()
        {
            return (this.Tables[0].Rows.Count != 0);

        }

        public bool IsNull(int GivenRow, string GivenField)
        {
            try
            {
                return System.Convert.IsDBNull(this.Tables[0].Rows[GivenRow][GivenField]);
            }
            catch
            {
                return false;
            }
        }

        public bool IsNull(int GivenRow, int GivenField)
        {
            try
            {
                return System.Convert.IsDBNull(this.Tables[0].Rows[GivenRow][GivenField]);
            }
            catch
            {
                return false;
            }
        }

        public string NullAsString(int GivenRow, string GivenField)
        {
            return System.Convert.ToString(((IsNull(GivenRow, GivenField)) ? "" : this.Tables[0].Rows[GivenRow][GivenField]));
        }

        public string NullAsString(int GivenRow, int GivenField)
        {
            return System.Convert.ToString(((IsNull(GivenRow, GivenField)) ? "" : this.Tables[0].Rows[GivenRow][GivenField]));
        }

        public double NullAsDouble(int GivenRow, string GivenField)
        {
            return System.Convert.ToDouble(((IsNull(GivenRow, GivenField)) ? 0 : this.Tables[0].Rows[GivenRow][GivenField]));
        }

        public double NullAsDouble(int GivenRow, int GivenField)
        {
            return System.Convert.ToDouble(((IsNull(GivenRow, System.Convert.ToString(GivenField))) ? 0 : this.Tables[0].Rows[GivenRow][GivenField]));
        }

        public int NullAsInteger(int GivenRow, string GivenField)
        {
            return System.Convert.ToInt32(((IsNull(GivenRow, GivenField)) ? 0 : this.Tables[0].Rows[GivenRow][GivenField]));

        }

        public int NullAsInteger(int GivenRow, int GivenField)
        {
            return System.Convert.ToInt32(((IsNull(GivenRow, System.Convert.ToString(GivenField))) ? 0 : this.Tables[0].Rows[GivenRow][GivenField]));
        }

        public System.DateTime NullAsDate(int GivenRow, int GivenField)
        {
            return System.Convert.ToDateTime(((IsNull(GivenRow, GivenField)) ? "01/01/1900" : this.Tables[0].Rows[GivenRow][GivenField]));
        }
    }

}
