using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Epam_Log_library
{
    /// <summary>
    /// Class for loging in plain files
    /// </summary>
    public class LoggerPlain : ILogger, IDisposable
    {
        protected StreamWriter sw;

        /// <summary>
        /// Write all message in log.txt
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
                path = path + "\\log.txt";

                if (!File.Exists(path))
                    File.Create(path).Dispose();
                using (sw = File.AppendText(path))
                {
                    sw.WriteLine(dateTime.ToString() + " Message: " + logString + " from " + type.Namespace + " " + type.Name);
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
        /// Write message in log.txt
        /// depending on the  log level
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
                    logName = "logdebug.txt";
                    break;
                case levels.error:
                    logName = "logerror.txt";
                    break;
                case levels.info:
                    logName = "loginfo.txt";
                    break;
                case levels.warning:
                    logName = "logwarning.txt";
                    break;
                default:
                    logName = "log.txt";
                    break;
            }

            if (!dateTime.HasValue)
                dateTime = DateTime.Now;

            Type type = obj.GetType();
            try
            {

                string path = ConfigFromFile.GetPathLogger();
                path = path + "\\" + logName;

                if (!File.Exists(path))
                    File.Create(path).Dispose();
                using (sw = File.AppendText(path))
                {
                    sw.WriteLine(dateTime.ToString() + " Message: " + logString + " from " + type.Namespace + " " + type.Name);
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

        ~LoggerPlain()
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