using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using PullInfoToScxtpt_GY.Helper;
using PullToScxtpt;
using PullToScxtpt.Model;
using Topshelf;
using Topshelf.Logging;
using Topshelf.Runtime;

namespace PullInfoToScxtpt_GY
{
    partial class PullInfoService : ServiceControl
    {
     
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
                //  Thread.Sleep(1000 * 10);

                LogHelper.GetLog(this).Info(string.Format("DATE： {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                LogHelper.GetLog(this).Error("测试出错！！！", new Exception("dfsafdsafddsafdsa"));

                //sender.InserPersonResume();
                sender.InserPersonInfo();
               // sender.InserCompanyInfo();
            }
            catch (Exception ex)
            {
                
            }
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            try
            {
             //   _log.Info("PullInfoService Started");

            }
            catch (Exception ex)
            {
             //   _log.Info(ex.Message);
            }
            return true;
        }

   
    }
}
