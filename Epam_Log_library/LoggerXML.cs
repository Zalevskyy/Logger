using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace Epam_Log_library
{

    /// <summary>
    /// Class for loging in XML files
    /// </summary>
    class LoggerXML : ILogger
    {
        protected XmlTextWriter xw;
        /// <summary>
        /// Write all message in log.xml
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
                path = path + "\\log.xml";

                if (!File.Exists(path))
                    File.Create(path).Dispose();
                using (xw = new XmlTextWriter(path, null))
                {
                    xw.WriteStartDocument();
                    xw.WriteStartElement("message");
                    xw.WriteString(dateTime.ToString() + " Message: " + logString + " from " + type.Namespace + " " + type.Name);
                    xw.WriteEndDocument();
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
        /// Write message in log.xml
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
                    logName = "logdebug.xml";
                    break;
                case levels.error:
                    logName = "logerror.xml";
                    break;
                case levels.info:
                    logName = "loginfo.xml";
                    break;
                case levels.warning:
                    logName = "logwarning.xml";
                    break;
                default:
                    logName = "log.xml";
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
                using (xw = new XmlTextWriter(path, null))
                {
                    xw.WriteStartDocument();
                    xw.WriteStartElement("message");
                    xw.WriteString(dateTime.ToString() + " Message: " + logString + " from " + type.Namespace + " " + type.Name);
                    xw.WriteEndDocument();
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
                xw.Dispose();
            }
        }

        ~LoggerXML()
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
