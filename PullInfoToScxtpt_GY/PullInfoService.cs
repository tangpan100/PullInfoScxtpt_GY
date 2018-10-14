using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using PullToScxtpt;
using PullToScxtpt.Model;
using Topshelf;
using Topshelf.Logging;
using Topshelf.Runtime;

namespace PullInfoToScxtpt_GY
{
    partial class PullInfoService : ServiceControl
    {
        static readonly LogWriter _log = HostLogger.Get<PullInfoService>();
        private static object LockObject = new Object();
        // 检查更新锁
        private static int CheckUpDateLock = 0;
        TaskTimer timer1;  //计时器
        TaskTimer timer2;  //计时器
        TaskTimer timer3;  //计时器
        Sender sender = new Sender();

    

        public bool Start(HostControl hostControl)
        {
            try
            {
                Thread.Sleep(1000 * 10);
                _log.Info("PullInfoService Started");
                //  sender.InserCompanyInfo();
                sender.InserPersonResume();
            }
            catch (Exception ex)
            {
                _log.Info(ex.Message);
            }
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            try
            {
                _log.Info("PullInfoService Started");

            }
            catch (Exception ex)
            {
                _log.Info(ex.Message);
            }
            return true;
        }

   
    }
}
