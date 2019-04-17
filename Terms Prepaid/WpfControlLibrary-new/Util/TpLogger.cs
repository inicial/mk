using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

        public static void Error(string title, string message, Exception e)
        {
            if (_enabled)
            {
                string msg = string.Format("{0} error: {1}; exeption: {2}", title, message, e.Message);
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
