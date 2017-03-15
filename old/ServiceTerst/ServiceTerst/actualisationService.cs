using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ServiceTerst
{
    public partial class actualisationService : ServiceBase
    {
        private Timer t = null;

        public actualisationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            t = new Timer();
            this.t.Interval = 5000;
            t.Elapsed += new ElapsedEventHandler(this.t_Tick);
            t.Enabled = true;
            Library.writeErrorLog("ActualisationService started");
        }
        private void t_Tick(object sender, ElapsedEventArgs e)
        {
            Library.writeErrorLog("Timer ticked and some job has been sucessfully");
        }


        protected override void OnStop()
        {
            t.Enabled = false;
            Library.writeErrorLog("ActualisationService stopped");
        }
    }
}
