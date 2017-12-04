using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Epam_Log_library
{
    /// <summary>
    /// Class for loging in json files
    /// </summary>
    class LoggerJson : ILogger
    {
        protected StreamWriter sw;
        /// <summary>
        /// Write all message in log.json
        /// </summary>
        /// <param name="logString">message to log</param>
        /// <param name="obj">object/module passed message</param>
        /// <param name="dateTime">date and time logging</param>
        public void Log(string logString, object obj, DateTime? dateTime = null)
        {
            if (!dateTime.HasValue)
                dateTime = DateTime.Now;

            Type type = obj.GetType();
            try
            {
                string path = ConfigFromFile.GetPathLogger();
                path = path + "\\log.json";

                if (!File.Exists(path))
                    File.Create(path).Dispose();

                JObject jsonObj = new JObject();
                jsonObj["dateTime"] = dateTime.ToString();
                jsonObj["Message"] = logString;
                jsonObj["from"] = type.Namespace + " " + type.Name;
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(jsonObj.ToString());
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("Error access: " + e.Message);
            }

            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Wrong path: " + e.Message);
            }

            catch (NotSupportedException e)
            {
                Console.WriteLine("Wrong path format: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something wrong: " + e.Message);
            }
        }
        /// <summary>
        /// Write message in log.json
        /// depending on the log level
        /// </summary>
        /// <param name="logString">message to log</param>
        /// <param name="logLevel">enum level log</param>
        /// <param name="obj">object/module called method/passed message</param>
        /// <param name="dateTime">date and time logging</param>
        public void Log(string logString, levels logLevel, object obj, DateTime? dateTime = null)
        {
            string logName;
            switch (logLevel)
            {
                case levels.debug:
                    logName = "logdebug.json";
                    break;
                case levels.error:
                    logName = "logerror.json";
                    break;
                case levels.info:
                    logName = "loginfo.json";
                    break;
                case levels.warning:
                    logName = "logwarning.json";
                    break;
                default:
                    logName = "log.json";
                    break;
            }

            if (!dateTime.HasValue)
                dateTime = DateTime.Now;

            Type type = obj.GetType();
            try
            {

                string path = ConfigFromFile.GetPathLogger();
                path = path + "\\" + logName;

                FileInfo fileInf = new FileInfo(path);
                if (!fileInf.Exists)
                    fileInf.Create().Dispose();
                JObject jsonObj = new JObject();
                jsonObj["dateTime"] = dateTime.ToString();
                jsonObj["Message"] = logString;
                jsonObj["from"] = type.Namespace + " " + type.Name;
                using (sw = File.AppendText(path))
                {
                    sw.WriteLine(jsonObj.ToString());
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("Error access: " + e.Message);
            }

            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Wrong path: " + e.Message);
            }

            catch (NotSupportedException e)
            {
                Console.WriteLine("Wrong path format: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something wrong: " + e.Message);
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                sw.Dispose();
            }
        }

        ~LoggerJson()
        {
            //Console.WriteLine($"Finalizing object by finalizator");
            Dispose(false);
        }

        public void Dispose()
        {
            //Console.WriteLine($"Finalizing object by Dispose");
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}