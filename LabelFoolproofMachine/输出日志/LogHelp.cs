using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LogManager
{
    class LogHelp
    {
        public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");
        public static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");
        /// <summary>
        /// 记录正常的消息
        /// </summary>
        /// <param name="msg">消息内容</param>
        public static void info(string msg)
        {
            loginfo.Info(msg);
        }
        /// <summary>
        /// 记录异常信息
        /// </summary>
        /// <param name="msg">异常信息内容</param>
        /// 
        public static void error(string msg)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();
            logerror.Error("类名:" + methodBase.ReflectedType.Name + "\r\n方法名:" + methodBase.Name + "\r\n信息:\r\n" + msg);
        }
    }
}
