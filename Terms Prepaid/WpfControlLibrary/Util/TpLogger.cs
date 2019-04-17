using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using lanta.SQLConfig;
using NLog;

namespace WpfControlLibrary.Util
{
    public class TpLogger
    {
        public static long TotalElapsedMilliseconds { get; private set; }

        private static bool _enabled = false;
        public static bool Enabled 
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public static string _userLogin = "";
        public static string UserLogin
        {
            get { return _userLogin; }
            set { _userLogin = value; }
        }

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private TpLogger _tpLogger;

        public static Logger Logger
        {
            get { return _logger ?? (_logger = LogManager.GetCurrentClassLogger()); }
        }

        public static void Init()
        {
            Config_XML conf = new Config_XML();
            _enabled = Convert.ToBoolean(conf.Get_Value("appSettings", "logging"));
        }

        private static Stopwatch _stopwatch;

        public static void StartWatch()
        {
            if (_enabled)
            {
                _stopwatch = Stopwatch.StartNew();
                TotalElapsedMilliseconds = 0;
            }
        }

        public static void StopWatch()
        {
            if (_enabled)
                _stopwatch.Stop();
        }

        public static void WriteElapsedMs(string message)
        {
            if (_enabled)
            {
                _stopwatch.Stop();
                Logger.Debug("{0} - {1} ms", message, _stopwatch.ElapsedMilliseconds);
                TotalElapsedMilliseconds += _stopwatch.ElapsedMilliseconds;
                _stopwatch = Stopwatch.StartNew();
            }
        }

        public static void WriteTotalElapsedMs(string message)
        {
            if (_enabled)
            {
                Logger.Debug("{0} - {1} ms", message, TotalElapsedMilliseconds);
            }
        }

        public static void Debug(string title, Exception e)
        {
            string e_msg = "";
            string e_trace = "";
            if (e != null)
            {
                e_msg = e.Message;
                e_trace = e.StackTrace;

                string log_msg = string.Format("{0} error: {1}" + (char)13 + (char)10 + "StackTrase: {2}", title, e_msg, e_trace);
                WriteException(log_msg);

                return; // don't write exceptions to debug file, wroten to exceptions file
            }

            if (_enabled)
            {
                string msg = string.Format("{0} error: {1}" + (char)13 + (char)10 + "StackTrase: {2}", title, e_msg, e_trace);
                Logger.Error(msg);
            }
        }

        public static void WriteException(string log_msg)
        {
            string app_path = Application.ExecutablePath;
            string dir = app_path.Substring(0, app_path.LastIndexOf('\\') + 1);
            string log_dir = dir + "logs";
            if (!System.IO.Directory.Exists(log_dir))
                System.IO.Directory.CreateDirectory(log_dir);
            string ex_dir = log_dir + (char)92 + "exceptions";
            if (!System.IO.Directory.Exists(ex_dir))
                System.IO.Directory.CreateDirectory(ex_dir);

            string file_name = DateTime.Now.ToString("yy_MM_dd") + ".txt";
            string path = ex_dir + (char)92 + file_name;

            if (!File.Exists(path)) File.Create(path).Close();
            StreamWriter LogFile = File.AppendText(path);
            LogFile.WriteLine(DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + " (" + UserLogin + ") >> " + log_msg);
            LogFile.Close();

        }

        public static void Error(string title, string message, Exception e)
        {
            string e_msg = "";
            if (e != null) e_msg = e.Message;

            if (_enabled)
            {
                string msg = string.Format("{0} error: {1}; exeption: {2}", title, message, e_msg);
                Logger.Error(msg);
            }
        }

        public static void ErrorWithMessage(string title, string message, Exception e)
        {
            if (_enabled)
            {
                string msg = string.Format("{0} error: {1}; exeption: {2}", title, message, e.Message);
                Logger.Error(msg);
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        public static void ErrorWithMessage(string title, string message)
        {
            if (_enabled)
            {
                string msg = string.Format("{0} error: {1}", title, message);
                Logger.Error(msg);
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        public static void ErrorWithMessage(Exception e)
        {
            if (_enabled)
            {
                string msg = string.Format("{0} error: {1}; exeption: {2}; stackTrace: {3}", e.GetType(), "", e.Message, e.StackTrace);
                Logger.Error(msg);
                MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

    }
}
