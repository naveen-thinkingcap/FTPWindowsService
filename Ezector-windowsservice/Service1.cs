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

            //if (DateTime.UtcNow.Date.Hour == 12)
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
