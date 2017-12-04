using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam_Log_library
{
    /// <summary>
    /// Interface for method Log
    /// </summary>
    public interface ILogger
    {
        void Log(string logString, object obj, DateTime? dateTime = null);
        void Log(string logString, levels logLevel, object obj, DateTime? dateTime = null);
    }
}
