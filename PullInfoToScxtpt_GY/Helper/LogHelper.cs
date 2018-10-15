using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace PullInfoToScxtpt_GY.Helper
{
    public class LogHelper
    {
        public static ILog GetLog<T>(T t)
        {
            ILog _log = LogManager.GetLogger("");
            if (t != null)
            {
                _log = LogManager.GetLogger(t.GetType());
            }
            return _log;
        }
    }
}
