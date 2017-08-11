using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XSCP.Core
{
    public class Log
    {
        const string log = "Log.txt";
        public static void Write(string msg)
        {
            string logFile = GetLogFile();
            try
            {
                if (!File.Exists(logFile))
                {
                    //File.WriteAllText(logFile, "", Encoding.UTF8);
                    File.Create(logFile).Close();
                }
                StreamWriter sw = new StreamWriter(logFile, true);
                sw.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") + "]   " + msg);
                sw.Flush();
                sw.Close();
                //File.AppendAllText(logFile, "[" + DateTime.Now.ToString() + "]   " + msg + "\n");
                
            }
            catch (Exception)
            {
                //throw;
            }

        }

        public static void Delete()
        {
            string logFile = GetLogFile();

            try
            {
                if (File.Exists(logFile))
                {
                    File.Delete(logFile);
                }

            }
            catch (Exception)
            {

                //throw;
            }
        }

        private  static string GetLogFile() {
            string logPath = Path.Combine(DirectoryUtility.GetInstallDirectory(), "log");
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            string logFile = logPath + @"\" + log;

            return logFile;
        }
    }
}
