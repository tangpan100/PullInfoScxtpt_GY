﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Topshelf;

namespace PullInfoToScxtpt_GY
{
    class Program
    {
        static void Main(string[] args)
        {
            string assemblyFilePath = Assembly.GetExecutingAssembly().Location;
            string assemblyDirPath = Path.GetDirectoryName(assemblyFilePath);
            string configFilePath = assemblyDirPath + "\\log4net.config";
            log4net.Config.XmlConfigurator.Configure(new FileInfo(configFilePath));
            HostFactory.Run(x =>                                 
            {
         
             //   x.UseLog4Net("log4net.config");
                x.RunAsLocalSystem();
                x.Service(settings => new PullInfoService());
             

                x.SetDescription("推送人才网站信息到四川协同平台");        
                x.SetDisplayName("PullInfoToScxtpt_GY");                       
                x.SetServiceName("PullInfoToScxtpt_GY");                       
            });                                                  
        }
    }
    

    public class TownCrier
    {
        readonly Timer _timer;
        public TownCrier()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => Console.WriteLine("It is {0} and all is well", DateTime.Now);
        }
        public void Start() { _timer.Start(); }
        public void Stop() { _timer.Stop(); }
    }
}
