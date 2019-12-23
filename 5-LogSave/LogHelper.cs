using System;
using System.IO;
using System.Diagnostics;

namespace ICNL9911S
{
    /// <summary>
    /// 自定义之日志助手
    /// </summary>
    public sealed class LogHelper
    {
        #region 单利模式
        //创建私有静态字段，接收类的实例化对象  
        private static readonly LogHelper _LogHelper = null;
        //构造函数私有化  
        private LogHelper() { }
        //静态构造函数，创建单利对象资源  
        static LogHelper()
        {
            _LogHelper = new LogHelper();
        }

        //获取单利对象资源  
        public static LogHelper GetSingleObj()
        {
            return _LogHelper;
        }
        #endregion

        /// <summary>
        /// 日志等级
        /// </summary>
        private enum LogType { ERROR = 1, INFO = 2, DEBUG = 3 };

        /// <summary>
        /// 指定日志文件夹（项目跟路劲文件夹）
        /// </summary>
        public static string LogPath { get; set; }

        /// <summary>
        /// 打印日志等级控制
        /// </summary>
        public static int LogRate { get; set; }

        /// <summary>
        /// 向日志文件写入调试信息
        /// </summary>
        /// <param name="logName">类名/方法名</param>
        /// <param name="logContent">日志记录内容</param>
        public static void Debug(string logName, string logContent, int logFileNum = 1)
        {
            if (LogRate < (int)LogType.DEBUG)
                return;

            WriteLog(new LogParameter() { LogGrade = (int)LogType.DEBUG, LogType = "DEBUG", LogName = logName, LogContent = logContent , LogFileNum = logFileNum });
        }

        /// <summary>
        /// 向日志文件写入运行时信息
        /// </summary>
        /// <param name="logName">类名/方法名</param>
        /// <param name="logContent">日志记录内容</param>
        public static void Info(string logName, string logContent, int logFileNum = 1)
        {
            if (LogRate < (int)LogType.INFO)
                return;

            WriteLog(new LogParameter() { LogGrade = (int)LogType.INFO, LogType = "INFO", LogName = logName, LogContent = logContent, LogFileNum = logFileNum });
        }

        /// <summary>
        /// 向日志文件写入出错信息
        /// </summary>
        /// <param name="logName">类名/方法名</param>
        /// <param name="logContent">日志记录内容</param>
        public static void Error(string logName, string logContent, int logFileNum = 1)
        {
            if (LogRate < (int)LogType.ERROR)
                return;

            WriteLog(new LogParameter() { LogGrade = (int)LogType.ERROR, LogType = "ERROR", LogName = logName, LogContent = logContent, LogFileNum = logFileNum });
        }

        /// <summary>
        /// 实际的写日志操作
        /// </summary>
        /// <param name="logParameter">日志参数model</param>
        private static void WriteLog(LogParameter logParameter)
        {
            if (!Directory.Exists(LogPath))//如果日志目录不存在就创建
            {
                Directory.CreateDirectory(LogPath);
            }

            DateTime dateTime = DateTime.Now;
            string time = dateTime.ToString("HH:mm:ss.fff");//获取当前系统时间
            //string filename = LogPath + "/" + dateTime.ToString("yyyy-MM-dd") + ".log";//用日期对日志文件命名
            string filename = string.Format("{0}/{1}_{2}.log", LogPath, dateTime.ToString("yyyy-MM-dd"), logParameter.LogFileNum);//同上等效

            #region 原始写法
            //创建或打开日志文件，向日志文件末尾追加记录
            //StreamWriter mySw = File.AppendText(filename);

            //向日志文件写入内容
            //string writeContent = time + "|" + typeGrade + ":" + type + "|" + className + ":" + content;
            //mySw.WriteLine(writeContent);

            //关闭日志文件
            //mySw.Close(); 
            #endregion

            //（优化写法）创建或打开日志文件，向日志文件末尾追加记录,关闭日志文件
            using (StreamWriter mySw = File.AppendText(filename))
            {
                string writeContent = string.Format("{0}|{1,-5}|{2}->{3}",time,logParameter.LogType,logParameter.LogName,logParameter.LogContent);
                mySw.WriteLine(writeContent);//向日志文件写入内容
                mySw.Close(); //关闭日志文件
            }
        }

        public static string GetFileName(StackTrace st)
        {
            StackFrame sf = st.GetFrame(0);
            return Path.GetFileNameWithoutExtension(sf.GetFileName())+"_" + sf.GetFileLineNumber();
        }

        public static string GetMethodName()
        {
            //Method 1
            //StackFrame sf = st.GetFrame(0);
            //return sf.GetMethod().Name;

            //Method 2
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
            System.Diagnostics.StackFrame[] sfs = st.GetFrames();
            if (sfs.Length > 2)
                return sfs[1].GetMethod().Name;
            else
                return "NoFunc";
        }
    }

    public sealed class LogParameter
    {
        /// <summary>
        /// 日志等级
        /// </summary>
        public int LogGrade { get; set; }

        /// <summary>
                /// 日志类型
                /// </summary>
        public string LogType { get; set; }

        /// <summary>
                /// 日志记录类名和方法名（className/methodName）
                /// </summary>
        public string LogName { get; set; }

        /// <summary>
                /// 日志记录文本内容
                /// </summary>
        public string LogContent { get; set; }

        /// <summary>
                /// 日志文件区分
                /// </summary>
        public int LogFileNum { get; set; }
    }
}
