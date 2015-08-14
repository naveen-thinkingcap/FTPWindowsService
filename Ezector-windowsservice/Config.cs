using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;

namespace Ezector_windowsservice
{
    public class Config
    {

        private static NameValueCollection AppSettings
        {
            get { return ConfigurationManager.AppSettings; }
        }

        //public static string DBConnectionString 
        //{
        //    get {return AppSettings["DBConnectionString"];}
        //}

        public static string MainConnectionString
        {
            get
            {
                string connID = MainConnectionStringName;
                ////if (string.IsNullOrEmpty(connID))
                ////{
                ////    string appDomainID = HttpRuntime.AppDomainAppId.Replace("/LM/W3SVC/", "");//never changes,always starts with /LM/W3SVC/ then site ID, then /Root then site path
                ////    appDomainID = appDomainID.Substring(0, appDomainID.IndexOf("/"));
                ////    connID = "ConnectionString" + appDomainID;
                ////}
                //if (ConfigurationManager.ConnectionStrings[connID] != null && ConfigurationManager.ConnectionStrings[connID].ConnectionString != "")
                //    return ConfigurationManager.ConnectionStrings[connID].ConnectionString;
                return AppSettings["DBConnectionString"];
            }
        }

        public static string MainConnectionStringName
        {
            get
            {
                //string appDomainID = HttpRuntime.AppDomainAppId.Replace("/LM/W3SVC/", "");//never changes,always starts with /LM/W3SVC/ then site ID, then /Root then site path
                //appDomainID = appDomainID.Substring(0, appDomainID.IndexOf("/"));
                //string connID = "ConnectionString" + appDomainID;
                //if (ConfigurationManager.ConnectionStrings[connID] != null && ConfigurationManager.ConnectionStrings[connID].ConnectionString != "")
                //    return connID;
                //return "DBConnectionString";
                return AppSettings["Name"];
            }
        }

        public static string ClientKey
        {
            get { return AppSettings["ClientKey"]; }
        }
        public static string FilePath
        {
            get { return AppSettings["FilePath"]; }
        }
        public static string Hour
        {
            get { return AppSettings["Hour"]; }
        }
        public static string Minute
        {
            get { return AppSettings["Minute"]; }
        }
        public static string UTCHourAdjust
        {
            get { return AppSettings["UTCHourAdjust"]; }
        }
        public static string TimerInterval
        {
            get { return AppSettings["TimerInterval"]; }
        }
        public static string LogFile
        {
            get { return AppSettings["LogFile"]; }
        }
    }
}
