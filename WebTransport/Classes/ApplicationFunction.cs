using System;
using System.Data;
using System.Net.Mail;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Net;
using System.Globalization;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
namespace WebTransport.Classes
{
    public class ApplicationFunction
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        //public static string ConnectionString = ConfigurationManager.ConnectionStrings["AutomobileConnectionString"].ToString();
        //public static string ConnectionString = clsMultipleDB.strDynamicConString();
        public static string ConnectionString()
        {
            string strMasterConns = "";
            if (Convert.ToString(System.Web.HttpContext.Current.Session["DBName"]) != null)
            {
                strMasterConns = "Data Source=136.243.149.22,1443;Initial Catalog=" + Convert.ToString(System.Web.HttpContext.Current.Session["DBName"]) + ";Integrated Security=False;User ID=sa;Password=41kc*mRq4IWyUK5eW6E";
                //strMasterConns = "Data Source=ACER-PC\\SQLEXPRESS;Initial Catalog=" + Convert.ToString(System.Web.HttpContext.Current.Session["DBName"]) + ";Integrated Security=True;";
            }
            return strMasterConns;
        }

        public static DateTime GetIndianDateTime()
        {
            //return  TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            DateTime todayDate = new DateTime();
            todayDate = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            return todayDate;
        }

        public static DataTable CreateTable(string TableName, params object[] Param)
        {
            DataTable DTTemp = new DataTable();
            try
            {
                if (Param.Length > 0 && (Param.Length % 2) == 0)
                {
                    DTTemp.TableName = TableName;
                    for (int Count = 0; Count < Param.Length; Count += 2)
                    {
                        DTTemp.Columns.Add(Param[Count].ToString()).DataType = System.Type.GetType("System." + Param[Count + 1].ToString());
                    }
                }
            }
            catch (Exception Ex)
            {

            }

            return DTTemp;
        }

        public static void DatatableAddRow(DataTable DT, params object[] Param)
        {
            DataRow Drow = DT.NewRow();
            try
            {

                for (int Count = 0; Count < Param.Length; Count++)
                {
                    Drow[Count] = Param[Count];
                }
                DT.Rows.Add(Drow);
            }
            catch (Exception Ex)
            {

            }
        }

        public static void DatatableRemoveRow(DataTable DT, params object[] Param)
        {
            DataRow Drow = DT.NewRow();
            try
            {

                for (int Count = 0; Count < Param.Length; Count++)
                {
                    Drow[Count] = Param[Count];
                }
                DT.Rows.Remove(Drow);
            }
            catch (Exception Ex)
            {

            }
        }

        public static void SendMailMessage(string from, string to, string bcc, string cc, string subject, string body)
        {
            // Instantiate a new instance of MailMessage
            MailMessage mMailMessage = new MailMessage();

            // Set the sender address of the mail message
            mMailMessage.From = new MailAddress(from);

            if ((to != null) && (to != string.Empty))
            {
                // Set the recepient address of the mail message
                //mMailMessage.To.Add(new MailAddress(to));
                string[] strMails = to.Split(';');
                if (strMails.Length > 0)
                {
                    for (int counter = 0; counter <= strMails.Length - 1; counter++)
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(strMails[counter])) == false)
                            mMailMessage.To.Add(strMails[counter]);
                    }
                }
            }

            // Check if the bcc value is null or an empty string
            if ((bcc != null) && (bcc != string.Empty))
            {
                // Set the Bcc address of the mail message
                mMailMessage.Bcc.Add(new MailAddress(bcc));
            }      // Check if the cc value is null or an empty value
            if ((cc != null) && (cc != string.Empty))
            {
                // Set the CC address of the mail message
                //mMailMessage.CC.Add(new MailAddress(cc));
                string[] strMails = cc.Split(';');
                if (strMails.Length > 0)
                {
                    // Looping
                    for (int counter = 0; counter <= strMails.Length - 1; counter++) // Setting CC
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(strMails[counter])) == false)
                            mMailMessage.CC.Add(strMails[counter]);
                    }
                }
            }       // Set the subject of the mail message
            mMailMessage.Subject = subject;
            // Set the body of the mail message
            mMailMessage.Body = body;

            // Set the format of the mail message body as HTML
            mMailMessage.IsBodyHtml = true;
            // Set the priority of the mail message to normal
            mMailMessage.Priority = MailPriority.Normal;

            // Instantiate a new instance of SmtpClient
            SmtpClient mSmtpClient = new SmtpClient();
            //mSmtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
            // Send the mail message
            try
            {
                mSmtpClient.Send(mMailMessage);
            }
            catch (Exception ex)
            {
                ErrorLog(ex.ToString());
            }
        }

        public bool SendEMailMsg(string to, string bcc, string cc, string subject, string body)
        {
            MailMessage mMailMessage = new MailMessage();
            mMailMessage.From = new MailAddress("info@cogxim.com");

            if ((to != null) && (to != string.Empty))
            {
                string[] strMails = to.Split(';');
                if (strMails.Length > 0)
                {
                    for (int counter = 0; counter <= strMails.Length - 1; counter++)
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(strMails[counter])) == false)
                            mMailMessage.To.Add(strMails[counter]);
                    }
                }
            }
            if ((bcc != null) && (bcc != string.Empty))
            {
                mMailMessage.Bcc.Add(new MailAddress(bcc));
            }
            if ((cc != null) && (cc != string.Empty))
            {
                string[] strMails = cc.Split(';');
                if (strMails.Length > 0)
                {
                    // Looping
                    for (int counter = 0; counter <= strMails.Length - 1; counter++) // Setting CC
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(strMails[counter])) == false)
                            mMailMessage.CC.Add(strMails[counter]);
                    }
                }
            }
            mMailMessage.Subject = subject;
            mMailMessage.Body = body;

            // Set the format of the mail message body as HTML
            mMailMessage.IsBodyHtml = true;
            mMailMessage.Priority = MailPriority.Normal;
            SmtpClient SmtpServer = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SMTP"].ToString());
            SmtpServer.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["FROMEMAIL"].ToString(), System.Configuration.ConfigurationManager.AppSettings["FROMPWD"].ToString());
            try
            {
                SmtpServer.Send(mMailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void SendMailByClient(string from, string to, string bcc, string cc, string subject, string body)
        {
            // Instantiate a new instance of MailMessage
            MailMessage mMailMessage = new MailMessage();

            // Set the sender address of the mail message
            mMailMessage.From = new MailAddress(from);
            // Set the recepient address of the mail message
            mMailMessage.To.Add(new MailAddress(to));

            // Check if the bcc value is null or an empty string
            if ((bcc != null) && (bcc != string.Empty))
            {
                // Set the Bcc address of the mail message
                mMailMessage.Bcc.Add(new MailAddress(bcc));
            }      // Check if the cc value is null or an empty value
            if ((cc != null) && (cc != string.Empty))
            {
                // Set the CC address of the mail message
                mMailMessage.CC.Add(new MailAddress(cc));
            }       // Set the subject of the mail message
            mMailMessage.Subject = subject;
            // Set the body of the mail message
            mMailMessage.Body = body;

            // Set the format of the mail message body as HTML
            mMailMessage.IsBodyHtml = true;
            // Set the priority of the mail message to normal
            mMailMessage.Priority = MailPriority.Normal;

            // Instantiate a new instance of SmtpClient
            SmtpClient mSmtpClient = new SmtpClient();
            //mSmtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
            // Send the mail message
            try
            {
                mSmtpClient.Send(mMailMessage);
            }
            catch (Exception ex)
            {
                //ErrorLog(ex.ToString());
            }
        }

        public static bool SendMailByAdmin(string pReceiverEmail, string pSubject, string pBody, string adminEmail, string compName)
        {
            // Local variable
            bool boolFlag = false;

            // Mail Message Object
            MailMessage mail = new MailMessage();

            // Mail Address Objectog
            MailAddress fromAddress = new MailAddress(adminEmail, compName);
            mail.From = fromAddress;

            // Array of Email Strings
            string[] strMails = pReceiverEmail.Split(';');

            if (strMails.Length > 0)
            {
                mail.To.Add(strMails[0]); // Setting To

                // Looping
                for (int counter = 1; counter <= strMails.Length - 1; counter++) // Setting CC
                    mail.CC.Add(strMails[counter]);
            }
            else
                mail.To.Add(strMails[0]); // Setting To

            mail.Subject = pSubject; // Setting Subject
            mail.Body = pBody; // Setting Body
            mail.IsBodyHtml = true;

            // SmtpClient Object
            SmtpClient smtpClient = new SmtpClient();
            try
            {
                smtpClient.Send(mail); // Sending Mail
                boolFlag = true;
            }
            catch (System.Exception ex)
            {

            }
            mail.Dispose();

            // Returning boolean message
            return boolFlag;
        }

        public static string UploadFile(HtmlInputFile hf, string FolderName, string Type)
        {
            string FileName = string.Empty;
            string str = string.Empty;
            string saveLocation = string.Empty;
            str = str + Convert.ToString(System.DateTime.Now).Replace("/", "");
            str = str.Replace(":", "");
            str = str.Replace(" ", "");
            str = str.Replace("PM", "");
            str = str.Replace("AM", "");
            if (hf.PostedFile.ContentLength > 0)
            {
                FileName = Path.GetFileName(hf.PostedFile.FileName);
                FileName = str + FileName;
                FileName = FileName.Replace("@", "_");
                FileName = FileName.Replace("#", "_");
                FileName = FileName.Replace("$", "_");
                FileName = FileName.Replace("%", "_");
                FileName = FileName.Replace("^", "_");
                FileName = FileName.Replace("&", "_");
                FileName = FileName.Replace("*", "_");
                FileName = FileName.Replace("-", "_");
                FileName = FileName.Replace(" ", "_");
                if (string.IsNullOrEmpty(FolderName) == false)
                {
                    if (Type == "Current")
                    {
                        saveLocation = HttpContext.Current.Server.MapPath("~") + "/" + FolderName + "/" + FileName;
                    }
                    else if (Type == "userimage")
                    {
                        saveLocation = HttpContext.Current.Server.MapPath("~") + "/" + FolderName + "/" + FileName;
                    }
                    else if (Type == "companyimage")
                    {
                        saveLocation = HttpContext.Current.Server.MapPath("~") + "/" + FolderName + "/" + FileName;
                    }
                    else if (Type == "dsrupload")
                    {
                        saveLocation = HttpContext.Current.Server.MapPath("~") + "/" + FolderName + "/" + FileName;
                    }
                    else if (Type == "OpeningStock")
                    {
                        saveLocation = HttpContext.Current.Server.MapPath("~") + "/" + FolderName + "/" + FileName;
                    }
                    else if (Type == "Staff")
                    {
                        saveLocation = HttpContext.Current.Server.MapPath("~") + "/images/" + FolderName + "/" + FileName;
                    }
                    else if (Type == "ItemMRPupdation")
                    {
                        saveLocation = HttpContext.Current.Server.MapPath("~") + "/" + FolderName + "/" + FileName;
                    }
                    else if (Type == "AutoMobileExcelFile")
                    {
                        saveLocation = HttpContext.Current.Server.MapPath("~") + "/" + FolderName + "/" + FileName;
                    }
                    else
                    {
                        saveLocation = HttpContext.Current.Server.MapPath("~") + "/images/" + FolderName + "/" + FileName;
                    }
                    hf.PostedFile.SaveAs(saveLocation);
                }

            }
            return FileName;
        }


        public static string UploadFileOnServer(FileUpload hf, string FolderName, string Type, string strFileName)
        {
            string FileName = string.Empty;
            string str = string.Empty;
            string saveLocation = string.Empty;
            str = str + strFileName;
            str = str.Replace(":", "");
            str = str.Replace(" ", "");
            str = str.Replace("PM", "");
            str = str.Replace(".", "");
            str = str.Replace("AM", "");
            if (hf.PostedFile.ContentLength > 0)
            {
                FileName = Path.GetFileName(hf.PostedFile.FileName);
                FileName = str + FileName;
                FileName = FileName.Replace("@", "_");
                FileName = FileName.Replace("#", "_");
                FileName = FileName.Replace("$", "_");
                FileName = FileName.Replace("%", "_");
                FileName = FileName.Replace("^", "_");
                FileName = FileName.Replace("&", "_");
                FileName = FileName.Replace("*", "_");
                FileName = FileName.Replace("-", "_");
                FileName = FileName.Replace(" ", "_");
                if (string.IsNullOrEmpty(FolderName) == false)
                {
                    if (Type == "Current")
                    {
                        saveLocation = HttpContext.Current.Server.MapPath("~") + "/" + FolderName + "/" + FileName;
                    }
                    else if (Type == "ticket")
                    {
                        saveLocation = HttpContext.Current.Server.MapPath("~") + "/" + FolderName + "/" + FileName;
                    }
                    else if (Type == "Itemsexcel")
                    {
                        saveLocation = HttpContext.Current.Server.MapPath("~") + "/" + FolderName + "/" + FileName;
                    }
                    else if (Type == "GrExcelImport")
                    {
                        saveLocation = HttpContext.Current.Server.MapPath("~") + "/" + FolderName + "/" + FileName;
                    }
                    else
                    {
                        saveLocation = HttpContext.Current.Server.MapPath("~") + "/images/" + FolderName + "/" + FileName;
                    }
                    hf.PostedFile.SaveAs(saveLocation);
                }

            }
            return FileName;
        }

        public static string UploadFileServerControl(FileUpload hf, string FolderName, string Type)
        {
            string FileName = string.Empty;
            string str = string.Empty;
            string saveLocation = string.Empty;
            str = str + Convert.ToString(System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff")).Replace("/", "");
            str = str.Replace(":", "");
            str = str.Replace(" ", "");
            str = str.Replace("PM", "");
            str = str.Replace(".", "");
            str = str.Replace("AM", "");
            if (hf.PostedFile.ContentLength > 0)
            {
                FileName = Path.GetFileName(hf.PostedFile.FileName);
                FileName = str + FileName;
                FileName = FileName.Replace("@", "_");
                FileName = FileName.Replace("#", "_");
                FileName = FileName.Replace("$", "_");
                FileName = FileName.Replace("%", "_");
                FileName = FileName.Replace("^", "_");
                FileName = FileName.Replace("&", "_");
                FileName = FileName.Replace("*", "_");
                FileName = FileName.Replace("-", "_");
                FileName = FileName.Replace(" ", "_");
                if (string.IsNullOrEmpty(FolderName) == false)
                {
                    if (Type == "Current")
                    {
                        saveLocation = HttpContext.Current.Server.MapPath("~") + "/" + FolderName + "/" + FileName;
                    }
                    else if (Type == "ticket")
                    {
                        saveLocation = HttpContext.Current.Server.MapPath("~") + "/" + FolderName + "/" + FileName;
                    }
                    else if (Type == "Itemsexcel")
                    {
                        saveLocation = HttpContext.Current.Server.MapPath("~") + "/" + FolderName + "/" + FileName;
                    }
                    else if (Type == "GrExcelImport")
                    {
                        saveLocation = HttpContext.Current.Server.MapPath("~") + "/" + FolderName + "/" + FileName;
                    }
                    else if (Type == "FuelExcel")
                    {
                        saveLocation = HttpContext.Current.Server.MapPath("~") + "/" + FolderName + "/" + FileName;
                    }
                    else if (Type == "Grprepexcel")
                    {
                        saveLocation = HttpContext.Current.Server.MapPath("~") + "/" + FolderName + "/" + FileName;
                    }
                    else
                    {
                        saveLocation = HttpContext.Current.Server.MapPath("~") + "/images/" + FolderName + "/" + FileName;
                    }
                    hf.PostedFile.SaveAs(saveLocation);
                }

            }
            return FileName;
        }

        /// <summary>
        /// Substring of text
        /// </summary>
        /// <param name="str">string value</param>
        /// <param name="no">no of char</param>
        /// <returns>return string</returns>
        public static string TextSubString(string str, int no)
        {
            string newValue = string.Empty;
            newValue = Convert.ToString(str);
            if (str.Length > no)
            {
                return newValue.Substring(0, no);
            }
            else
            {
                return newValue;
            }
        }

        /// <summary>
        /// This is used to show message 
        /// </summary>
        /// <param name="Message">Message to be displayed</param>
        /// <param name="MessageType">e for error, s fro success</param>
        /// <param name="siteUrl">url of site</param>
        /// <returns>return the string</returns>
        public static string GetAdminMsg(string Message, string MessageType, string siteUrl)
        {
            StringBuilder message = new StringBuilder();
            if (MessageType.ToLower() == "e")
            {
                message.Append("<table id='divMsgAccount' runat='server' style='display: block;margin-left: 5px;border:solid 1px #FAC5C8; background-color:#FDE8E9;'><tr><td>");
                message.Append("<table style='margin-left: 10px; margin-right: 10px;text-align: left;padding-top:2px;padding-bottom:2px; vertical-align:middle;' class='black12_txt'>");
                message.Append("<tr><td><img src='" + siteUrl + "images/error_image.gif' alt=''></td>");
                message.Append("<td>" + Message + "</td>");
                message.Append("</tr></table>");
                message.Append("</td></tr></table>");
            }
            else
            {
                message.Append("<table id='divMsgAccount' runat='server' style='display: block;margin-left: 5px;border:solid 1px #008000; background-color:#EFF6F4;'><tr><td>");
                message.Append("<table style='margin-left: 10px; margin-right: 10px; text-align: left;padding-top:2px;padding-bottom:2px; vertical-align:middle;' class='black12_txt'>");
                message.Append("<tr><td valign='top'><img src='" + siteUrl + "images/right_image.gif' alt=''></td>");
                message.Append("<td>" + Message + "</td>");
                message.Append("</tr></table>");
                message.Append("</td></tr></table>");
            }
            return message.ToString();
        }

        public static DataTable ConvertListToDataTable(List<string[]> list)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            foreach (var array in list)
            {
                if (array.Length > columns)
                {
                    columns = array.Length;
                }
            }

            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }

            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }

            return table;
        }

        public static void ValidateLogin()
        {
            if (HttpContext.Current.Session["companyid"] == null)
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
            }
        }

        public static void ExportGridviewToExcel(string fileName, GridView grd)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".xls");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.xls";
            StringWriter StringWriter = new System.IO.StringWriter();
            HtmlTextWriter HtmlTextWriter = new HtmlTextWriter(StringWriter);
            grd.RenderControl(HtmlTextWriter);
            HttpContext.Current.Response.Write(StringWriter.ToString());
            HttpContext.Current.Response.End();
        }

        public static bool IsDate(string inputDate)
        {
            DateTime dt;
            return DateTime.TryParse(inputDate, out dt);
        }

        public static bool IsValidImage(string fileName)
        {
            Regex regex = new Regex(@"(.*?)\.(jpg|JPG|jpeg|JPEG|png|PNG|gif|GIF|bmp|BMP)$");
            return regex.IsMatch(fileName);
        }

        /// <summary>
        /// Compare two Date
        /// </summary>
        /// <param name="fromDate">first date</param>
        /// <param name="toDate">second date</param>
        /// <returns>return < (earlierthen) /  =(same) / > (greater then)</returns>
        public static int DateCompare(DateTime fromDate, DateTime toDate)
        {
            DateTime date1 = fromDate;
            DateTime date2 = toDate;
            int result = DateTime.Compare(date1, date2);
            return result;
        }

        public static void ErrorLog(string message)
        {
            var path = HttpContext.Current.Server.MapPath("~/ErrorLog/Errorlog.txt");
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("-----------------------------------------------------------");
                sw.WriteLine(System.DateTime.Now);
                sw.WriteLine("-----------------------------------------------------------");
                sw.WriteLine(message);
            }
        }

        public static string mmddyyyy(string value)
        {
            //string newvalue = string.Empty;
            //if (string.IsNullOrEmpty(value) == false)
            //{
            //    string[] ddmmyy = value.Split(new char[] { '-' });
            //    //newvalue = ddmmyy[1] + "-" + ddmmyy[0] + "-" + ddmmyy[2];
            //    newvalue = ddmmyy[2] + "/" + ddmmyy[1] + "/" + ddmmyy[0];
            //}
            //else
            //{
            //    newvalue = value;
            //}
            //return newvalue;
            string newdt = null;
            string newvalue = string.Empty;
            if (string.IsNullOrEmpty(value) == false)
            {
                string[] ddmmyy = value.Split(new char[] { '-' });
                if (ddmmyy.Length > 2)
                {
                    newvalue = ddmmyy[2] + "-" + ddmmyy[1] + "-" + ddmmyy[0];
                }
            }
            else
            {
                newvalue = value;
            }
            if (string.IsNullOrEmpty(newvalue) == false)
            {
                return newvalue;
            }
            else
            {
                return newdt;
            }
        }

        public static DateTime? mmddyyyyDash(string value)
        {
            DateTime? newdt = null;
            string newvalue = string.Empty;
            if (string.IsNullOrEmpty(value) == false)
            {
                string[] ddmmyy = value.Split(new char[] { '-' });
                if (ddmmyy.Length > 2)
                {
                    newvalue = ddmmyy[2] + "-" + ddmmyy[1] + "-" + ddmmyy[0];
                }
            }
            else
            {
                newvalue = value;
            }
            if (string.IsNullOrEmpty(newvalue) == false)
            {
                return Convert.ToDateTime(newvalue);
            }
            else
            {
                return newdt;
            }
        }

        public static void SendMsg(string api)
        {
            WebClient objWebClient;
            string sBaseURL;
            Stream objStreamData;
            StreamReader objReader;
            string sResult;
            objWebClient = new WebClient();
            // sBaseURL = "http://globesms.in/sendhttp.php?user=32&password=cog14021971&mobiles=91" + OwnerMobile + "&message= " + HttpUtility.UrlEncode(msg) + "&sender=COGXIM";
            sBaseURL = api;
            objStreamData = objWebClient.OpenRead(sBaseURL);
            objReader = new StreamReader(objStreamData);
            sResult = objReader.ReadToEnd();
            objStreamData.Close();
            objReader.Close();

        }

        public static void SendMsg(string OwnerName, string OwnerMobile, string msg)
        {
            WebClient objWebClient;
            string sBaseURL;
            Stream objStreamData;
            StreamReader objReader;
            string sResult;
            objWebClient = new WebClient();
            sBaseURL = "http://globesms.in/sendhttp.php?user=32&password=teamcogximsms&mobiles=91" + OwnerMobile + "&message= " + HttpUtility.UrlEncode(msg) + "&sender=COGXIM";
            objStreamData = objWebClient.OpenRead(sBaseURL);
            objReader = new StreamReader(objStreamData);
            sResult = objReader.ReadToEnd();
            objStreamData.Close();
            objReader.Close();

        }

        public static bool SendMsgForOTP(string Mobile, string msg)
        {
            try
            {
                WebClient objWebClient;
                string sBaseURL;
                Stream objStreamData;
                StreamReader objReader;
                string sResult;
                objWebClient = new WebClient();
                sBaseURL = "http://globesms.in/sendhttp.php?user=32&password=teamcogximsms&mobiles=91" + Mobile + "&message= " + HttpUtility.UrlEncode(msg) + "&sender=COGXIM";
                objStreamData = objWebClient.OpenRead(sBaseURL);
                objReader = new StreamReader(objStreamData);
                sResult = objReader.ReadToEnd();
                objStreamData.Close();
                objReader.Close();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static string StripHTML(string htmlString)
        {

            //This pattern Matches everything found inside html tags;

            //(.|\n) - > Look for any character or a new line

            // *?  -> 0 or more occurences, and make a non-greedy search meaning

            //That the match will stop at the first available '>' it sees, and not at the last one

            //(if it stopped at the last one we could have overlooked

            //nested HTML tags inside a bigger HTML tag..)

            // Thanks to Oisin and Hugh Brown for helping on this one...

            string pattern = @"<(.|\n)*?>";



            return Regex.Replace(htmlString, pattern, string.Empty);

        }

        public static int WeekNumber(DateTime value)
        {
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(value, CalendarWeekRule.FirstFourDayWeek,
                                                                     DayOfWeek.Monday);

        }

        public static string GetGuid()
        {
            Guid g;

            g = Guid.NewGuid();
            return g.ToString();
        }

        public static string GetInstType(int value)
        {
            string itype = string.Empty;
            switch (value)
            {
                case 1: itype = "Cash"; break;
                case 2: itype = "Cheque"; break;
                case 3: itype = "RTGS/NEFT"; break;
                case 4: itype = "DD"; break;
                case 5: itype = "Banker Cheque"; break;
            }
            return itype;
        }

        public static string ConvertDateToString(DateTime dt)
        {
            return Convert.ToDateTime(dt).ToShortDateString();
        }

        static public bool IsTimeOfDayBetween(DateTime time,
                                      TimeSpan startTime, TimeSpan endTime)
        {
            if (endTime == startTime)
            {
                return true;
            }
            else if (endTime < startTime)
            {
                return time.TimeOfDay <= endTime ||
                    time.TimeOfDay >= startTime;
            }
            else
            {
                return time.TimeOfDay >= startTime &&
                    time.TimeOfDay <= endTime;
            }

        }

        /*---------------------gridview export------------------------*/

        public static void Export(string fileName, GridView gv, bool includeGridLines,
                                  string Heading1, string Heading2, string Heading3)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader(
                "content-disposition", string.Format("attachment; filename={0}", fileName));
            HttpContext.Current.Response.ContentType = "application/ms-excel";

            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            //HttpContext.Current.Response.Write(style);
            //HttpContext.Current.Response.Write(Heading1);
            //HttpContext.Current.Response.Write("");
            //HttpContext.Current.Response.Write(Heading2);
            //HttpContext.Current.Response.Write("");
            //HttpContext.Current.Response.Write(Heading3);
            //HttpContext.Current.Response.Write("");
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a form to contain the grid
                    Table table = new Table();

                    if (includeGridLines)
                    {
                        table.GridLines = gv.GridLines;
                    }



                    //  add the header row to the table
                    if (gv.HeaderRow != null)
                    {
                        PrepareControlForExport(gv.HeaderRow);
                        table.Rows.Add(gv.HeaderRow);
                    }

                    //  add each of the data rows to the table
                    foreach (GridViewRow row in gv.Rows)
                    {
                        PrepareControlForExport(row);
                        table.Rows.Add(row);
                    }

                    //  add the footer row to the table
                    if (gv.FooterRow != null)
                    {
                        PrepareControlForExport(gv.FooterRow);
                        table.Rows.Add(gv.FooterRow);
                    }

                    if (Heading1 != "")
                    {
                        TableRow tRow = new TableRow();
                        table.Rows.AddAt(0, tRow);
                        TableCell tCell = new TableCell();
                        tCell.ColumnSpan = gv.Columns.Count;
                        tCell.Text = Heading1;
                        tRow.Cells.Add(tCell);
                    }

                    //  render the table into the htmlwriter
                    table.RenderControl(htw);

                    //string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                    //HttpContext.Current.Response.Write(style);
                    //HttpContext.Current.Response.Write(Heading1);
                    //HttpContext.Current.Response.Write("");
                    //HttpContext.Current.Response.Write(Heading2);
                    //HttpContext.Current.Response.Write("");
                    //HttpContext.Current.Response.Write(Heading3);
                    //HttpContext.Current.Response.Write("");
                    //  render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }
        }

        /// <summary>
        /// Replace any of the contained controls with literals
        /// </summary>
        /// <param name="control"></param>
        private static void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }

                if (current.HasControls())
                {
                    PrepareControlForExport(current);
                }
            }
        }


        public static void RepeaterToExcel(Repeater rep, string filename)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            //     Your Repeater Name Mine is "Rep"
            rep.RenderControl(htmlWrite);
            HttpContext.Current.Response.Write("<table>");
            HttpContext.Current.Response.Write(stringWrite.ToString());
            HttpContext.Current.Response.Write("</table>");
            HttpContext.Current.Response.End();
        }

        public static double GetCurrentStock(DateTime dtStockDate, Int64 intLocIdno, Int64 intItemIdno, int intYearIdno)
        {
            bool bWithRcptQty = false;

            SqlParameter[] objSqlParameter = new SqlParameter[1];
            objSqlParameter[0] = new SqlParameter("@Action", "CalcStockWithRcptQty");
            bWithRcptQty = Convert.ToBoolean(SqlHelper.ExecuteScalar(ApplicationFunction.ConnectionString(), CommandType.StoredProcedure, "spStock", objSqlParameter));
            objSqlParameter = null;

            double dblCurrentStock = 0;
            string strAction = string.Empty;

            if (intLocIdno > 0)
            {
                if (bWithRcptQty == false)
                { strAction = "GetCurrStockWithLoc"; }
                else
                { strAction = "GetCurrStockWithLocNRQ"; }
            }
            else
            {
                if (bWithRcptQty == false)
                { strAction = "GetCurrStockWithoutLoc"; }
                else
                { strAction = "GetCurrStockWithoutLocNRQ"; }
            }

            SqlParameter[] objSqlPara = new SqlParameter[5];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@StockDate", dtStockDate);
            objSqlPara[2] = new SqlParameter("@LocIdno", intLocIdno);
            objSqlPara[3] = new SqlParameter("@ItemIdno", intItemIdno);
            objSqlPara[4] = new SqlParameter("@YearIdno", intYearIdno);
            dblCurrentStock = Convert.ToDouble(SqlHelper.ExecuteScalar(ApplicationFunction.ConnectionString(), CommandType.StoredProcedure, "spStock", objSqlPara));
            return dblCurrentStock;


        }
        public static String CheckApostopy(string strValue)
        {
            return strValue = strValue.Replace("'", "''");
        }

        /*-----------------*/
        public static System.Boolean IsNumeric(System.Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
        /*------------------------------*/

        enum Months : int
        {
            January = 1, February, March, April,
            May, June, July, August, September, October, November, December
        };
        public static string MonthName(int month)
        {
            return Enum.GetName(typeof(Months), month);
        }
        public static int monthNo(string monthName)
        {
            int monthno = 0;
            foreach (int Mno in Enum.GetValues(typeof(Months)))
            {
                if (Enum.GetName(typeof(Months), Mno) == monthName)
                {
                    monthno = Mno;
                    break;
                }
            }
            return monthno;
        }
    }
}