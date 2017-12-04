using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam_Log_library
{
    /// <summary>
    /// Class to choose a Loger
    /// depending on the parameter 
    /// </summary>
    public class LoggerChoise
    {
        public static ILogger GetLogger(string formatLogger)
        {
            switch (formatLogger)
            {
                case "plain":
                    return new LoggerPlain();
                case "XML":
                    return new LoggerXML();
                case "json":
                    return new LoggerJson();
                default:
                    return new LoggerPlain();
            }

        }
    }
}
