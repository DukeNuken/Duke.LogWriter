﻿using System.Configuration;
using System.Text;

namespace Duke.LogWriter
{
    public class LogWriter
    {
        private string _FileMask = "";
        private string _LineMask = "";
        private string _LogFolder = "logs";
        private bool _ShowInConsoleDefault = false;
        public string FileMask
        {
            get
            {
                if (String.IsNullOrEmpty(_FileMask))
                {
                    _FileMask = "DukeLogWriter_{date}.txt";
                }

                if (_FileMask.IndexOf("{date}") > -1)
                {
                    return _FileMask.Replace("{date}", DateTime.Now.ToString("yyyy_MM_dd"));
                }

                return _FileMask;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    _FileMask = "";
                _FileMask = value;
            }
        }
        public string LineMask
        {
            get
            {
                if (String.IsNullOrEmpty(_LineMask))
                {
                    _LineMask = "[{time}] {log}";
                }

                if (_LineMask.IndexOf("{time}") > -1)
                {
                    return _LineMask.Replace("{time}", DateTime.Now.ToString("hh:mm:ss"));
                }

                return _LineMask;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    _LineMask = "";
                _LineMask = value;
            }
        }
        public string LogFolder
        {
            get {
                return _LogFolder; 
            }
            set
            {              
                _LogFolder = value;
            }
        }
        public bool ShowInConsoleDefault { 
            get { 
                return _ShowInConsoleDefault; 
            } 
            set
            {
                _ShowInConsoleDefault = value;
            }
        }
        public string WorkFile { 
            get {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                if (!String.IsNullOrEmpty(LogFolder))
                {
                    path = Path.Combine(path, LogFolder);
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                }
                if (!String.IsNullOrEmpty(path))
                {
                    path = Path.Combine(path, FileMask);
                }
                return path;
            }
        }
        public string ErrorMessage { get; set; }

        private static LogWriter _instance;
        public static LogWriter Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                _instance = new LogWriter();
                return _instance;
            }
        }

        private LogWriter()
        {
            //_LogFolder = "logs";
        }

        public void WriteHeader(string txt, bool? showInConsole = null, int headerLength = 80)
        {
            try
            {
                var headerFormat = " " + txt + " ";
                var lineBuilder = new StringBuilder();
                for (var i = 0; i < headerLength; i++)
                {
                    lineBuilder.Append("=");
                }
                var headerline = lineBuilder.ToString();
                var textline = "";
                if (headerFormat.Length >= headerLength)
                    textline = headerFormat;
                else
                {
                    var spaceSize = headerLength - headerFormat.Length;
                    var left = spaceSize / 2;
                    if (spaceSize % 2 > 0)
                        left++;
                    textline = headerline.Remove(left - 1, headerFormat.Length).Insert(left, headerFormat);
                }

                WriteLog(headerline, showInConsole);
                WriteLog(textline, showInConsole);
                WriteLog(headerline, showInConsole);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public void WriteLog(string txt, bool? showInConsole = null)
        {
            try
            {
                string log = "";
                if (LineMask.IndexOf("{log}") > -1)
                    log = LineMask.Replace("{log}", txt);
                else log = LineMask + txt;

                StreamWriter sw = new StreamWriter(WorkFile, true);
                sw.WriteLine(log);
                sw.Close();

                if (showInConsole.HasValue && showInConsole.Value)
                {
                    Console.WriteLine(log);
                }
                else if (ShowInConsoleDefault)  
                    Console.WriteLine(log);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }

}