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

            ///Have to set Scandinavian time for Ezector
            ///Have to run midnight
            //if ((DateTime.UtcNow.Hour == 23) && (DateTime.UtcNow.Minute == 59) && (DateTime.UtcNow.Second == 59))
            //{
                compass.ProcessCompletedOrPassed();
            //}
            //Since ConfigurationManager is not working hardcode the connection string in the code.
            System.IO.File.Create(Environment.CurrentDirectory + "OnStART.txt");
            
        }

        protected override void OnStop()
        {

        }
    }
}
