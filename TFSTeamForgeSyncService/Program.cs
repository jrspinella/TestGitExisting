using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace TFSTeamForgeSyncService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {   
            //this is a test!   1!!!!!!      
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new TFSTeamForgeService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
