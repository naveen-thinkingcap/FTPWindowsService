using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Timers;

namespace Ezector_windowsservice
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        public void onDebug()
        {
            OnStart(null);
        }
        protected override void OnStart(string[] args)
        {

            
            CompletedOrPassed compass = new CompletedOrPassed();

            ////If you need to debug the code, stop the service if it is running , and debug
            ///Have to set Scandinavian time for Ezector
            ///Have to run midnight
            //if ((DateTime.UtcNow.Hour == 23) && (DateTime.UtcNow.Minute == 59) && (DateTime.UtcNow.Second == 59))
            //{

            //compass.ProcessCompletedOrPassed();
            //Timer t = new Timer();
            /// 1000 millsecons is 1 secor for time interval
            //t.Interval = 30000;
            //t.Interval = 1000;
            timer.Interval = Convert.ToDouble(Config.TimerInterval);
            timer.Enabled = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimerEvent);
                
            
            //}
            
            
        }
        private void OnTimerEvent(object sender, EventArgs e)
        {
            string filepath=Config.FilePath;
            string utcadjust=Config.UTCHourAdjust;
            string filename = DateTime.UtcNow.AddHours(Convert.ToDouble(Config.UTCHourAdjust)).Year.ToString() + DateTime.UtcNow.AddHours(Convert.ToDouble(Config.UTCHourAdjust)).Month.ToString() + DateTime.UtcNow.AddHours(Convert.ToDouble(Config.UTCHourAdjust)).Day.ToString() + "EZECTOR".ToUpper() + ".csv";
            //For Ezector Scandinavian TimeZone
            //if ((DateTime.UtcNow.AddHours(1).Hour == 00)&&((DateTime.UtcNow.Minute % 30) == 0))
            //string ss = DateTime.UtcNow.AddHours(Convert.ToDouble(Config.UTCHourAdjust)).Hour.ToString();
            //string ssd= DateTime.UtcNow.AddHours(Convert.ToDouble(Config.UTCHourAdjust)).Minute.ToString();
            //string sss=Convert.ToDouble(Config.Hour).ToString();
            //string sdss = Convert.ToDouble(Config.Minute).ToString();
            //string okay = "";
            //if (((DateTime.UtcNow.AddHours(Convert.ToDouble(Config.UTCHourAdjust)).Minute % Convert.ToDouble(Config.Minute)) == 0))
            //    okay = "good";
            //else
            //    okay = "bad";                
            //if (((DateTime.UtcNow.AddHours(Convert.ToDouble(Config.UTCHourAdjust)).Hour % Convert.ToDouble(Config.Hour)) == 0) && ((DateTime.UtcNow.AddHours(Convert.ToDouble(Config.UTCHourAdjust)).Minute % Convert.ToDouble(Config.Minute)) == 0) && (!System.IO.File.Exists(filepath + filename)))
            if ((DateTime.UtcNow.AddHours(Convert.ToDouble(Config.UTCHourAdjust)).Hour == Convert.ToDouble(Config.Hour)) && ((DateTime.UtcNow.AddHours(Convert.ToDouble(Config.UTCHourAdjust)).Minute % Convert.ToDouble(Config.Minute)) == 0) && (!System.IO.File.Exists(filepath + filename)))
            
                //below line is just for local testing
                //if (((DateTime.UtcNow.Minute % Convert.ToDouble(Config.Minute)) == 0) && (!System.IO.File.Exists(filepath + filename)))
            {
                CompletedOrPassed compass = new CompletedOrPassed();
                compass.ProcessCompletedOrPassed();
            }
            
        }
        protected override void OnStop()
        {
           // System.IO.File.Create(Environment.CurrentDirectory + "OnStop.txt");
            timer.Enabled = false;
        }
    }
}
